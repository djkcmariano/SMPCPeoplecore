Imports System
Imports System.Collections
Imports System.Data
Imports System.Data.SqlClient

Namespace SQLConnectionUtilities
    Public Class dbConnection
        Public Structure sIdentifierItem
            Private mItemValue As Object
            Private mParameterName As String
            Private mReturnType As String

            Public Sub New(ByVal parameterName As String, ByVal itemValue As Object, ByVal returnType As String)
                mItemValue = New Object()
                mParameterName = parameterName
                mItemValue = itemValue
                mReturnType = returnType
            End Sub

            Public ReadOnly Property ParameterName As String
                Get
                    Return mParameterName
                End Get
            End Property

            Public ReadOnly Property ItemValue As Object
                Get
                    Return mItemValue
                End Get
            End Property

            Public ReadOnly Property ReturnType As String
                Get
                    Return mReturnType
                End Get
            End Property
        End Structure

        Public Structure sRoleInfo
            Public mRoleID As Integer
            Public mRoleName As String
        End Structure

        Private mConnectionString As String
        Private mServer As String
        Private mDatabase As String
        Private mLoginName As String
        Private mPassword As String
        Private mSqlUserID As Integer
        Private mSqlRoleInfo As sRoleInfo()
        Private mSQLConnection As SqlConnection
        Private mSQLAdapter As SqlDataAdapter
        Private mSQLDataSet As DataSet
        Private mSQLCommand As SqlCommand
        Private mSQLTimeout As Integer

        Public Sub New(ByVal Server As String, ByVal Database As String, ByVal LoginName As String, ByVal Password As String)
            mConnectionString = "server=" & Server & ";database=" & Database & ";uid=" & LoginName & ";pwd=" & Password & ";"
            mServer = Server
            mDatabase = Database
            mLoginName = LoginName
            mPassword = Password
            mSQLTimeout = 0
            TestConnection()
        End Sub

        Public Sub New(ByVal ConnectionStringCoded As String)
            Dim delim As String = ";"
            Dim param As String() = Nothing

            If Not ConnectionStringCoded.EndsWith(";") Then
                Dim [error] As Exception
                [error] = New Exception("Connection string syntax is incorrect.  Syntax should be: server=<srv>;database=<db>;uid=<name;>pwd=<password>;")
                Throw ([error])
            End If

            mConnectionString = ConnectionStringCoded
            ConnectionStringCoded = ConnectionStringCoded.ToLower()
            ConnectionStringCoded = ConnectionStringCoded.Replace("server=", "")
            ConnectionStringCoded = ConnectionStringCoded.Replace("database=", "")
            ConnectionStringCoded = ConnectionStringCoded.Replace("uid=", "")
            ConnectionStringCoded = ConnectionStringCoded.Replace("pwd=", "")
            param = ConnectionStringCoded.Split(delim.ToCharArray(), 4)
            mServer = param(0)
            mDatabase = param(1)
            mLoginName = param(2)
            mPassword = param(3).Substring(0, param(3).Length - 1)
            mSQLTimeout = 0

            Try
                Dim [error] As Exception

                If mLoginName.ToLower() = "sa" Then
                    mConnectionString = ""
                    mServer = ""
                    mDatabase = ""
                    mLoginName = ""
                    mPassword = ""
                    [error] = New Exception("Login as 'sa' not allowed.")
                    Throw ([error])
                End If

                TestConnection()
                getUserID()
                getRoleInfo()
            Catch [error] As Exception
                Throw ([error])
            End Try
        End Sub

        Public Sub New(ByVal rhs As dbConnection)
            mConnectionString = rhs.mConnectionString
            mServer = rhs.mServer
            mDatabase = rhs.mDatabase
            mLoginName = rhs.mLoginName
            mSQLTimeout = 0

            Try
                TestConnection()
                getUserID()
                getRoleInfo()
            Catch [error] As Exception
                Throw ([error])
            End Try
        End Sub

        Public Function ExcecuteStoredProcedure(ByVal StoredProcedureName As String, ByVal Parameters As ArrayList) As DataSet
            mSQLDataSet = New DataSet()
            mSQLCommand = New SqlCommand(CreateCommand(StoredProcedureName, Parameters), mSQLConnection)
            mSQLCommand.CommandTimeout = mSQLTimeout
            mSQLAdapter = New SqlDataAdapter()
            mSQLAdapter.Fill(mSQLDataSet)
            mSQLConnection.Close()
            Return mSQLDataSet
        End Function

        Public Function ExcecuteStoredProcedure(ByVal StoredProcedureName As String, ByVal Parameters As ArrayList, ByVal TableName As String) As DataSet
            mSQLDataSet = New DataSet()
            mSQLCommand = New SqlCommand(CreateCommand(StoredProcedureName, Parameters), mSQLConnection)
            mSQLCommand.CommandTimeout = mSQLTimeout
            mSQLAdapter = New SqlDataAdapter(mSQLCommand)
            mSQLAdapter.Fill(mSQLDataSet, TableName)
            mSQLConnection.Close()
            Return mSQLDataSet
        End Function

        Public Function ExcecuteStoredProcedure(ByVal StoredProcedureName As String) As DataSet
            mSQLDataSet = New DataSet()
            mSQLCommand = New SqlCommand(StoredProcedureName, mSQLConnection)
            mSQLCommand.CommandTimeout = mSQLTimeout
            mSQLAdapter = New SqlDataAdapter(mSQLCommand)
            mSQLAdapter.Fill(mSQLDataSet)
            mSQLConnection.Close()
            Return mSQLDataSet
        End Function

        Public Function ExcecuteStoredProcedure(ByVal StoredProcedureName As String, ByVal ParamArray Parameters As Object()) As DataSet
            Dim arr As ArrayList = New ArrayList()

            For Each obj As Object In Parameters
                arr.Add(obj)
            Next

            Return ExcecuteStoredProcedure(StoredProcedureName, arr)
        End Function

        Public Function ExcecuteSelectStatement(ByVal SelectStatement As String) As DataSet
            mSQLDataSet = New DataSet()
            mSQLCommand = New SqlCommand(SelectStatement, mSQLConnection)
            mSQLCommand.CommandTimeout = mSQLTimeout
            mSQLAdapter = New SqlDataAdapter(mSQLCommand)
            mSQLAdapter.Fill(mSQLDataSet)
            Return mSQLDataSet
        End Function

        Public Function ExecuteNonQuery(ByVal SelectStatement As String) As Integer
            Dim nRetVal As Integer
            mSQLDataSet = New DataSet()
            mSQLCommand = New SqlCommand(SelectStatement, mSQLConnection)
            mSQLCommand.CommandTimeout = mSQLTimeout
            mSQLAdapter = New SqlDataAdapter(mSQLCommand)
            mSQLAdapter.SelectCommand.Connection.Open()
            nRetVal = mSQLAdapter.SelectCommand.ExecuteNonQuery()
            mSQLAdapter.SelectCommand.Connection.Close()
            Return nRetVal
        End Function

        Public ReadOnly Property SQLConnectionString As String
            Get
                Return mConnectionString
            End Get
        End Property

        Public ReadOnly Property SQLConnection As SqlConnection
            Get
                Return mSQLConnection
            End Get
        End Property

        Public ReadOnly Property SQLDataAdapter As SqlDataAdapter
            Get
                Return mSQLAdapter
            End Get
        End Property

        Public ReadOnly Property userRoleInfo As sRoleInfo()
            Get
                Return mSqlRoleInfo
            End Get
        End Property

        Public ReadOnly Property uID As Integer
            Get
                Return mSqlUserID
            End Get
        End Property

        Public Property TimeOut As Integer
            Get
                Return mSQLTimeout
            End Get
            Set(ByVal value As Integer)
                mSQLTimeout = value
            End Set
        End Property

        Private Sub TestConnection()
            mSQLConnection = New SqlConnection(mConnectionString)
            mSQLConnection.Open()
            mSQLConnection.Close()
        End Sub

        Private Sub Connect()
            mSQLConnection = New SqlConnection(mConnectionString)
            mSQLConnection.Open()
        End Sub

        Private Sub Disconnect()
            mSQLConnection.Close()
        End Sub

        Public Function CreateCommand(ByVal StoredProcedureName As String, ByVal Parameters As ArrayList) As String
            Dim ConvertedString As String = StoredProcedureName & " "
            If Parameters.Count = 0 Then Return ConvertedString

            For Each x As Object In Parameters
                Dim y As Object = x
                Dim tidentifierItem As sIdentifierItem = New sIdentifierItem("", 0, "")
                Dim SqlType As System.Type
                SqlType = y.[GetType]()

                If Object.Equals(SqlType, tidentifierItem.[GetType]()) Then
                    Dim temp As sIdentifierItem
                    temp = CType(x, sIdentifierItem)
                    ConvertedString += temp.ParameterName & "="
                    Dim tDouble As Double = 0
                    Dim tString As String = ""
                    Dim tBool As Boolean = False

                    Select Case temp.ReturnType.ToLower()
                        Case "int", "money", "decimal"
                            SqlType = tDouble.[GetType]()
                        Case "varchar", "char", "datetime"
                            SqlType = tString.[GetType]()
                        Case "bit"
                            SqlType = tBool.[GetType]()
                        Case Else
                            Throw (New Exception("Unhandled identifierType.rType: " & temp.ReturnType.ToLower()))
                    End Select

                    y = temp.ItemValue
                End If

                Select Case SqlType.ToString()
                    Case "System.Int16", "System.Int32", "System.Int64", "System.Double", "System.Single"
                        ConvertedString += y.ToString() & ", "
                    Case "System.String", "System.Char"

                        If y.ToString().ToLower().Trim() = "null" Then
                            ConvertedString += "null, "
                        Else
                            ConvertedString += "'" & y.ToString().Replace("'", "''") & "', "
                        End If

                    Case "System.Boolean"

                        If Convert.ToBoolean(y) = False Then
                            ConvertedString += "0" & ", "
                        Else
                            ConvertedString += "1" & ", "
                        End If

                    Case "System.DBNull"
                        ConvertedString += "null, "
                    Case Else
                        Throw (New Exception("Unhandled variable type: " & SqlType.ToString()))
                End Select
            Next

            Return ConvertedString.Substring(0, ConvertedString.Length - 2)
        End Function

        Private Sub getUserID()
            Dim temp As DataSet = New DataSet()
            temp = ExcecuteSelectStatement("select uid from sysusers where name = '" & mLoginName & "'")

            If temp.Tables(0).Rows.Count > 1 Then
                Dim [error] As Exception
                [error] = New Exception("getUserID returned more than one row.  Method should not return more than one row.")
                Throw ([error])
            End If

            mSqlUserID = Convert.ToInt32(temp.Tables(0).Rows(0)("uid"))
        End Sub

        Private Sub getRoleInfo()
            Dim count As Integer = 0
            Dim temp As DataSet = New DataSet()
            temp = ExcecuteSelectStatement("select uid, name from sysusers where uid in (select groupuid from sysmembers where memberuid = " & mSqlUserID & ")")
            mSqlRoleInfo = New sRoleInfo(temp.Tables(0).Rows.Count - 1) {}

            For count = 0 To mSqlRoleInfo.Length - 1
                mSqlRoleInfo(count).mRoleID = Convert.ToInt32(temp.Tables(0).Rows(count)("uid"))
                mSqlRoleInfo(count).mRoleName = Convert.ToString(temp.Tables(0).Rows(count)("name"))
            Next
        End Sub
    End Class
End Namespace
