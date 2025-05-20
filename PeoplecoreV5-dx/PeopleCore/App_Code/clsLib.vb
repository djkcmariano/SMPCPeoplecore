Imports Microsoft.VisualBasic
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Configuration
Imports System.Data.OleDb
Imports DevExpress.Web
Imports System.Net

Namespace clsLib

#Region "MessageBox"

    Public Class MessageTemplate
        Public Shared DeniedView As String = "No access permission to view web page."
        Public Shared DeniedDelete As String = "No access permission to delete transaction."
        Public Shared DeniedAdd As String = "No access permission to add transaction."
        Public Shared DeniedEdit As String = "No access permission to edit transaction."
        Public Shared DeniedPost As String = "No access permission to post transaction."
        Public Shared DeniedProcess As String = "No access permission to process transaction."

        Public Shared SuccessDelete As String = "record(s) has been successfully deleted."
        Public Shared SuccessUpdate As String = "record(s) has been successfully updated."
        Public Shared SuccessCancel As String = "record(s) has been successfully cancelled."
        Public Shared SuccessForward As String = "record(s) has been successfully forwarded."
        Public Shared SuccesPost As String = "Record has been successfully posted."
        Public Shared SuccessProcess As String = "Record has been successfully processed."
        Public Shared SuccessSave As String = "Record has been successfully saved."
        Public Shared SuccessApproved As String = "record(s) successfully approved."
        Public Shared SuccessDisapproved As String = "record(s) successfully disapproved."
        Public Shared SuccessSubmit As String = "record(s) successfully submit for approval."

        Public Shared ErrorDelete As String = "Unable to Delete record."
        Public Shared ErrorUpdate As String = "Unable to Update record."
        Public Shared ErrorCancel As String = "Unable to Cancel record."
        Public Shared ErrorForward As String = "Unable to Forward record."
        Public Shared ErrorPost As String = "Unable to Post record."
        Public Shared ErrorProcess As String = "Unable to Process record"
        Public Shared ErrorSave As String = "Unable to Save record."
        Public Shared NoSelectedTransaction As String = "No selected  record."
        Public Shared DuplicateRecord As String = "Duplicate record."

        Public Shared PostedTransaction As String = "Posted transaction."




    End Class

    Public Class MessageBox
        Public Shared Sub Success(ByVal Message As String, ByVal owner As Control)
            Dim page As Page = If(TryCast(owner, Page), owner.Page)
            If page Is Nothing Then
                Return
            End If
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "popup", "Success('" & Message & "');", True)
        End Sub
        Public Shared Sub SuccessResponse(ByVal Message As String, ByVal owner As Control, url As String)
            Dim page As Page = If(TryCast(owner, Page), owner.Page)
            If page Is Nothing Then
                Return
            End If
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "popup", "SuccessResponse('" & Message & "','" & url & "');", True)
        End Sub
        Public Shared Sub PopupMessage(ByVal Message As String, ByVal owner As Control, url As String)
            Dim page As Page = If(TryCast(owner, Page), owner.Page)
            If page Is Nothing Then
                Return
            End If
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "popup", "PopupMessage('" & Message & "','" & url & "');", True)
        End Sub
        Public Shared Sub Information(ByVal Message As String, ByVal owner As Control)
            Dim page As Page = If(TryCast(owner, Page), owner.Page)
            If page Is Nothing Then
                Return
            End If
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "popup", "Information('" & Message & "');", True)
        End Sub

        Public Shared Sub Warning(ByVal Message As String, ByVal owner As Control)
            Dim page As Page = If(TryCast(owner, Page), owner.Page)
            If page Is Nothing Then
                Return
            End If
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "popup", "Warning('" & Message & "');", True)
        End Sub

        Public Shared Sub Critical(ByVal Message As String, ByVal owner As Control)
            Dim page As Page = If(TryCast(owner, Page), owner.Page)
            If page Is Nothing Then
                Return
            End If
            ScriptManager.RegisterStartupScript(page, page.[GetType](), "popup", "Critical('" & Message & "');", True)
        End Sub

        Public Shared Sub Alert(ByVal Message As String, ByVal alerttype As String, ByVal owner As Control, Optional ByVal position As String = "topRight")
            Dim page As Page = If(TryCast(owner, Page), owner.Page)
            If page Is Nothing Then
                Return
            End If

            If Message = "" Then
                Message = "Invalid Entry."
            End If

            If alerttype = "" Then
                alerttype = "error"
            End If

            ScriptManager.RegisterStartupScript(page, page.[GetType](), "JSDialogResponseMDL", "dialogResponseAlert('" + Message + "','" + alerttype + "','" + position + "');", True)
        End Sub
        
    End Class

#End Region

#Region "Data Layer"

    Public NotInheritable Class SQLHelper

        Public Shared fdatabasename As String = ""
        Public Shared fservername As String = ""
        Public Shared fsqllogin As String = ""
        Public Shared fsqlpass As String = ""
        Private Shared conn As String = ""
        'Private Shared db As Database = New SqlDatabase("Password=pkunzip112;Persist Security Info=True;User ID=sa;Initial Catalog=PeopleCoreV5;Data Source=localhost")
        Private Shared db As Database

        Public Shared ConSTRAsyn As String = ""
        Public Shared ConSTR As String = ""

        Public Shared Function GetIniFile(Optional IsAsyc As Boolean = False) As String
            Dim iInitArr As String
            Dim i As Integer
            Dim fs As FileStream
            Dim ConnectionString As String = ""
            Dim filename = HttpContext.Current.Server.MapPath("~/secured/connectionstr/") & "peoplecore.ini"
            Dim fservername As String = ""
            Dim fdatabasename As String = ""
            Dim fsqllogin As String = ""
            Dim fsqlpass As String = ""

            If Not IO.File.Exists(filename) Then
                HttpContext.Current.Response.Redirect("~/connection.aspx")
                Return ""
            End If

            fs = New FileStream(filename, FileMode.Open, FileAccess.Read)
            Dim l As Integer = 0, ftext As String = ""
            Dim d As New StreamReader(fs)
            Try
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                If d.Peek() > 0 Then
                    While d.Peek() > -1
                        i = d.Peek
                        ftext = PeopleCoreCrypt.Decrypt(d.ReadLine())
                        iInitArr = ftext
                        If l = 0 Then
                            fservername = iInitArr
                        ElseIf l = 1 Then
                            fdatabasename = iInitArr
                        ElseIf l = 2 Then
                            fsqllogin = iInitArr
                        ElseIf l = 3 Then
                            fsqlpass = iInitArr
                        End If
                        l = l + 1
                    End While
                    If IsAsyc = False Then
                        ConnectionString = "Password=" & fsqlpass & ";Persist Security Info=True;User ID=" & fsqllogin & " ;Initial Catalog=" & fdatabasename & " ;Data Source=" & fservername & ""
                        conn = ConnectionString
                        ConSTR = ConnectionString
                    Else
                        ConnectionString = "Password=" & fsqlpass & ";Persist Security Info=True;User ID=" & fsqllogin & ";Initial Catalog=" & fdatabasename & " ;Data Source=" & fservername & " ; Asynchronous Processing=true"
                        ConSTRAsyn = ConnectionString
                    End If
                    ConSTRAsyn = "Password=" & fsqlpass & ";Persist Security Info=True;User ID=" & fsqllogin & ";Initial Catalog=" & fdatabasename & " ;Data Source=" & fservername & " ; Asynchronous Processing=true"
                End If
                d.Close()
            Catch
                d.Close()
            End Try
            CreateDatabase()
            Return ConnectionString

        End Function
        Private Shared Sub CreateDatabase()
            db = New Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(conn)
        End Sub
        Public Shared Function ExecuteDataTable(StoredProcedure As String, ParamArray Parameters As Object()) As DataTable
            Try
                ' Dim ds As New DataSet()
                ' Dim dt As DataTable
                ' ds = db.ExecuteDataSet(StoredProcedure, Parameters)
                ' dt = ds.Tables(0)
                ' ds.Dispose()
                ' Return dt

                Dim xSQLHelper As New clsBase.SQLHelper
                Dim ds As New DataSet()
                Dim dt As DataTable
                ds = xSQLHelper.ExecuteDataset(SQLHelper.ConSTR, StoredProcedure, Parameters)
                dt = ds.Tables(0)
                ds.Dispose()
                Return dt
            Catch
                Return Nothing
            End Try
        End Function

        Public Shared Function ExecuteDataTableNew(ByVal StoredProcedure As String, ByVal ParamArray Parameters As Object()) As DataTable

            ' Dim ds As New DataSet()
            ' Dim dt As DataTable
            ' ds = db.ExecuteDataSet(StoredProcedure, Parameters)
            ' dt = ds.Tables(0)
            ' ds.Dispose()
            ' Return dt

            Dim xSQLHelper As New clsBase.SQLHelper
            Dim ds As New DataSet()
            Dim dt As DataTable
            ds = xSQLHelper.ExecuteDataset(SQLHelper.ConSTR, StoredProcedure, Parameters)
            dt = ds.Tables(0)
            ds.Dispose()
            Return dt

        End Function
        Public Shared Function ExecuteDataTable(SQLString As String) As DataTable
            Try
                Dim ds As New DataSet()
                Dim dt As DataTable
                ds = db.ExecuteDataSet(CommandType.Text, SQLString)
                dt = ds.Tables(0)
                ds.Dispose()
                Return dt
            Catch
                Return Nothing
            End Try
        End Function

        Public Shared Function ExecuteDataSet(StoredProcedure As String, ParamArray Parameters As Object()) As DataSet
            Try
                'Return db.ExecuteDataSet(StoredProcedure, Parameters)
                Dim xSQLHelper As New clsBase.SQLHelper
                Dim ds As New DataSet()
                ds = xSQLHelper.ExecuteDataset(SQLHelper.ConSTR, StoredProcedure, Parameters)
                Return ds
            Catch
                Return Nothing
            End Try
        End Function

        Public Shared Function ExecuteDataSet(SQLString As String) As DataSet
            Try
                Return db.ExecuteDataSet(CommandType.Text, SQLString)
            Catch
                Return Nothing
            End Try
        End Function

        Public Shared Function ExecuteDataSet_WOCatch(StoredProcedure As String, ParamArray Parameters As Object()) As DataSet            
            Return db.ExecuteDataSet(StoredProcedure, Parameters)
        End Function

        Public Shared Function ExecuteNonQuery(StoredProcedure As String, ParamArray Parameters As Object()) As Integer
            Try
                Return db.ExecuteNonQuery(StoredProcedure, Parameters)
            Catch
                Return 0
            End Try
        End Function

        Public Shared Function ExecuteNonQuery(SQLString As String) As Integer
            Try
                Return db.ExecuteNonQuery(CommandType.Text, SQLString)
            Catch
                Return 0
            End Try
        End Function

        Public Shared Function ExecuteScalar(StoredProcedure As String, ParamArray Parameters As Object()) As Object
            Try
                Dim obj As Object
                obj = db.ExecuteScalar(StoredProcedure, Parameters)
                Return obj
            Catch
                Return Nothing
            End Try
        End Function

        Public Shared Function ExecuteScalar(SQLString As String) As Object
            Try
                Return db.ExecuteScalar(CommandType.Text, SQLString)
            Catch
                Return Nothing
            End Try
        End Function

        Public Shared Function xDBFConnection(ByVal filename As String) As OleDb.OleDbConnection
            Try

                xDBFConnection = New OleDbConnection

                With xDBFConnection
                    '.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " & filename & ";Extended Properties=dBASE III" ';User ID=Admin;Password="
                    '.ConnectionString = "Provider=VFPOLEDB.1;Data Source =" & filename
                    .ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" & filename & ";Extended Properties=dBASE III"

                    '"Extended Properties=""DBASE IV;"";"
                    .Open()
                End With
            Catch ex As Exception
                xDBFConnection = Nothing
            End Try

        End Function

        Shared Function ExecuteDaataSet(ByVal p1 As String, ByVal xOnlineUseNo As String, ByVal ReportNo As Integer) As DataSet
            Throw New NotImplementedException
        End Function

    End Class

#End Region

#Region "PeopleCore Cryptography"
    Public NotInheritable Class PeopleCoreCrypt

        Public Shared Function Decrypt(Optional ByVal xToken As String = "") As String
            Try
                Dim xCurrent As Long, xPrevious As Long, xNext As Long
                Dim i As Long
                Dim rVal As String
                rVal = ""
                xCurrent = 0
                xNext = 0
                xPrevious = 1
                xToken = DecryptToDec4(xToken)
                For i = Len(xToken) To 1 Step -1
                    xNext = xCurrent
                    xCurrent = xPrevious
                    xPrevious = xNext + xCurrent
                    rVal = rVal & ChrW(AscW(Right(Left(xToken, Len(xToken) - (i - 1)), 1)) - xCurrent - Math.Abs(xCurrent - i))
                Next i
                xToken = ""
                For i = Len(rVal) To 1 Step -1
                    xToken = xToken & Right(Left(rVal, i), 1)
                Next i
                Decrypt = xToken
            Catch ex As Exception
                Decrypt = ""
            End Try
        End Function

        Public Shared Function Encrypt(Optional ByVal xToken As String = "") As String
            Try
                Dim xCurrent As Long, xNext As Long, xPrevious As Long
                Dim i As Long, rVal As String
                rVal = ""
                xCurrent = 0
                xPrevious = 0
                xNext = 1
                For i = Len(xToken) To 1 Step -1
                    xPrevious = xCurrent
                    xCurrent = xNext
                    xNext = xPrevious + xCurrent
                    rVal = rVal & EncryptToHex4(ChrW(AscW(Right(Left(xToken, i), 1)) + xCurrent + Math.Abs(xCurrent - i)))
                Next i
                Encrypt = rVal
            Catch ex As Exception
                Encrypt = ""
            End Try
        End Function

        Private Shared Function EncryptToHex(ByVal tval As String) As String
            Dim ireturn As String
            ireturn = Microsoft.VisualBasic.Hex(AscW(tval))
            If ireturn.Length = 1 Then
                ireturn = "0" & ireturn
            End If
            Return ireturn
        End Function
        Private Shared Function DecryptToDec(ByVal tval As String) As String
            Dim ireturn As String = ""
            Dim x As Integer = 0
            Dim f As Double = 0
            x = tval.Length
            f = x
            If x > 0 Then
                x = x / 2
                If x < (f / 2) Then
                    x = x + 1
                End If
            End If
            For i As Integer = 0 To x - 1
                ireturn = ireturn & ChrW(Str2Byte(tval, i))
            Next
            Return ireturn
        End Function
        Private Shared Function EncryptToHex4(ByVal tval As String) As String
            Dim ireturn As String
            ireturn = Microsoft.VisualBasic.Hex(AscW(tval))
            If ireturn.Length = 1 Then
                ireturn = "000" & ireturn
            ElseIf ireturn.Length = 2 Then
                ireturn = "00" & ireturn
            ElseIf ireturn.Length = 3 Then
                ireturn = "0" & ireturn
            End If
            Return ireturn
        End Function
        Private Shared Function DecryptToDec4(ByVal tval As String) As String
            Dim ireturn As String = ""
            Dim x As Integer = 0
            x = tval.Length
            If x > 0 Then
                x = x / 4
            End If
            For i As Integer = 0 To x - 1
                ireturn = ireturn & ChrW(Val("&h" & Mid(tval, (i * 4) + 1, 4)))
            Next
            Return ireturn
        End Function

        Private Shared Function Str2Byte(ByVal s As String, ByVal Index As Integer) As Long
            Dim b1 As Integer, b2 As Integer
            Dim s1 As String, s2 As String

            s1 = Mid(s, Index * 2 + 1, 1)
            s2 = Mid(s, Index * 2 + 2, 1)
            If s1 >= "A" Then
                b1 = Asc(s1) - Asc("A") + 10
            Else
                b1 = Asc(s1) - Asc("0")
            End If
            If s2 >= "A" Then
                b2 = Asc(s2) - Asc("A") + 10
            Else
                b2 = Asc(s2) - Asc("0")
            End If
            Str2Byte = b1 * 16 + b2
        End Function
        Private Shared Function Str2ByteArray(ByVal s As String, ByVal b() As Byte) As Integer
            Dim i As Integer
            Dim l As Integer
            l = Len(s) / 2
            For i = 0 To l - 1 Step 1
                b(i) = Str2Byte(s, i)
            Next
            Str2ByteArray = l
        End Function

        Public Shared Function DecryptLogs(Optional ByVal xToken As String = "") As String
            Dim xCurrent As Long, xPrevious As Long, xNext As Long
            Dim i As Long
            Dim rVal As String
            rVal = ""
            If Len(xToken) < 22 And Len(xToken) > 0 Then
                xCurrent = 0
                xNext = 0
                xPrevious = 1
                For i = Len(xToken) To 1 Step -1
                    xNext = xCurrent
                    xCurrent = xPrevious
                    xPrevious = xNext + xCurrent
                    rVal = rVal & ChrW(AscW(Right(Left(xToken, Len(xToken) - (i - 1)), 1)) - xCurrent - Math.Abs(xCurrent - i))
                Next i
                xToken = ""
                For i = Len(rVal) To 1 Step -1
                    xToken = xToken & Right(Left(rVal, i), 1)
                Next i
            Else
                xToken = ""
            End If
            DecryptLogs = xToken
        End Function

        Public Shared Function EncryptLogs(Optional ByVal xToken As String = "") As String
            Dim xCurrent As Long, xNext As Long, xPrevious As Long
            Dim i As Long, rVal As String
            rVal = ""
            If Len(xToken) <= 22 And Len(xToken) > 0 Then
                xCurrent = 0
                xPrevious = 0
                xNext = 1
                For i = Len(xToken) To 1 Step -1
                    xPrevious = xCurrent
                    xCurrent = xNext
                    xNext = xPrevious + xCurrent
                    rVal = rVal & ChrW(AscW(Right(Left(xToken, i), 1)) + xCurrent + Math.Abs(xCurrent - i))
                Next i
            End If
            EncryptLogs = rVal
        End Function

    End Class
#End Region

#Region "Sorting"
    Public NotInheritable Class grdSort
        Public Shared Sub AddSortImage(ByVal columnIndex As Integer, ByVal row As GridViewRow, ByVal sortdir As String)
            Try
                ' Create the sorting image based on the sort direction.
                Dim sortImage As New Image()
                If sortdir = " ASC" Then
                    sortImage.ImageUrl = "~/Images/collapse_blue.jpg"
                    sortImage.ImageAlign = ImageAlign.Bottom
                    sortImage.AlternateText = "Ascending Order"
                Else

                    sortImage.ImageUrl = "~/Images/expand_blue.jpg"
                    sortImage.AlternateText = "Descending Order"
                    sortImage.ImageAlign = ImageAlign.Bottom
                End If
                ' Add the image to the appropriate header cell.
                If row.RowIndex = -1 Then
                    row.Cells(columnIndex).Controls.Add(sortImage)
                End If

            Catch ex As Exception

            End Try
        End Sub
        Public Shared Function GetSortColumnIndex(ByVal grd As GridView, ByVal sortexpr As String) As Integer

            ' Iterate through the Columns collection to determine the index
            ' of the column being sorted.
            Dim field As DataControlField
            For Each field In grd.Columns

                If field.SortExpression = TryCast(sortexpr, String) Then
                    Return grd.Columns.IndexOf(field)
                End If
            Next
            Return -1

        End Function

        Public Shared Function GetSortDirection(ByVal column As String, sortExpression As String, ByRef lastDirection As String) As String
            ' By default, set the sort direction to ascending.
            Dim sortDirection = " ASC"
            ' Retrieve the last column that was sorted.

            If sortExpression IsNot Nothing Then
                ' Check if the same column is being sorted.
                ' Otherwise, the default value can be returned.
                If sortExpression = column Then
                    If lastDirection IsNot Nothing _
                      AndAlso lastDirection = " ASC" Then
                        sortDirection = " DESC"
                    End If
                End If
            End If
            ' Save new values in ViewState.
            lastDirection = sortDirection
            sortExpression = column

            Return sortDirection
        End Function

    End Class


#End Region

#Region "Generic"

    Public NotInheritable Class Generic

        Enum enumObjectType
            StrType = 0
            IntType = 1
            DblType = 2
        End Enum

        Public Shared Function ToInt(obj As Object) As Integer
            Dim retVal As Integer = 0
            Try
                If obj = Nothing Or obj.ToString().Length = 0 Then
                    retVal = 0
                Else
                    retVal = Convert.ToInt32(obj)
                End If
            Catch ex As Exception
                retVal = 0
            End Try
            Return retVal
        End Function

        Public Shared Function ToStr(obj As Object) As String
            Dim retVal As String = ""
            Try
                If obj = Nothing Or obj.ToString().Length = 0 Then
                    retVal = ""
                Else
                    retVal = Convert.ToString(obj)
                End If
            Catch ex As Exception
                retVal = ""
            End Try
            Return retVal
        End Function

        Public Shared Function ToDec(obj As Object) As Decimal
            Dim retVal As Decimal = 0
            Try
                If obj = Nothing Or obj.ToString().Length = 0 Then
                    retVal = 0
                Else
                    retVal = Convert.ToDecimal(obj)
                End If
            Catch ex As Exception
                retVal = 0
            End Try
            Return retVal
        End Function

        Public Shared Function ToBol(obj As Object) As Boolean
            Dim retVal As Boolean = False
            Try
                If obj = Nothing Or obj.ToString().Length = 0 Then
                    retVal = False
                Else
                    retVal = Convert.ToBoolean(obj)
                End If
            Catch ex As Exception
                retVal = False
            End Try
            Return retVal
        End Function

        Public Shared Function ToDbl(obj As Object) As Double
            Dim retVal As Double = 0
            Try
                If obj = Nothing Or obj.ToString().Length = 0 Then
                    retVal = 0
                Else
                    retVal = Convert.ToDouble(obj)
                End If
            Catch ex As Exception
                retVal = 0
            End Try
            Return retVal
        End Function

        Public Shared Function FindControlRecursive(ctrl As Control, controlID As String) As Control
            If String.Compare(ctrl.ID, controlID, True) = 0 Then
                Return ctrl
            Else
                For Each child As Control In ctrl.Controls
                    Dim lookFor As Control = FindControlRecursive(child, controlID)

                    If lookFor IsNot Nothing Then
                        Return lookFor
                    End If
                Next
                Return Nothing
            End If
        End Function

        Public Shared Sub PopulateData(owner As Control, ContainerID As String, dt As DataTable)
            Try
                Dim page As Page = If(TryCast(owner, Page), owner.Page)
                Dim tempContainer As Control
                tempContainer = FindControlRecursive(page, ContainerID)
                For Each obj As Control In tempContainer.Controls
                    PopulateDataDeti(obj, dt)
                Next
                'Return tempContainer.Controls.Count
            Catch ex As Exception

            End Try
        End Sub

        Private Shared Sub PopulateDataDeti(objContent As Control, dt As DataTable)
            Dim columName As String = ""
            Dim idLenght As Integer = 0
            If TypeOf objContent Is TextBox Then
                Dim txt As New TextBox
                txt = CType(objContent, TextBox)
                idLenght = txt.ID.Length - 3
                columName = Microsoft.VisualBasic.Mid(txt.ID, 4, idLenght)
                Try
                    If Left(txt.ID, 3) = "txt" Then
                        txt.Text = Generic.ToStr(dt.Rows(0)(columName))
                    End If
                Catch ex As Exception
                End Try
            ElseIf TypeOf objContent Is DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor Then
                Dim txt As New DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor
                txt = CType(objContent, DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor)
                idLenght = txt.ID.Length - 3
                columName = Microsoft.VisualBasic.Mid(txt.ID, 4, idLenght)
                Try
                    If Left(txt.ID, 3) = "txt" Then
                        txt.Html = Generic.ToStr(dt.Rows(0)(columName))
                    End If
                Catch ex As Exception
                End Try
            ElseIf TypeOf objContent Is Label Then
                Dim lbl As New Label
                lbl = CType(objContent, Label)
                idLenght = lbl.ID.Length - 3
                columName = Microsoft.VisualBasic.Mid(lbl.ID, 4, idLenght)
                Try
                    If Left(lbl.ID, 3) = "lbl" Then
                        lbl.Text = Generic.ToStr(dt.Rows(0)(columName))
                    End If
                Catch ex As Exception
                End Try
            ElseIf TypeOf objContent Is HiddenField Then
                Dim hif As New HiddenField
                hif = CType(objContent, HiddenField)
                idLenght = hif.ID.Length - 3
                columName = Microsoft.VisualBasic.Mid(hif.ID, 4, idLenght)
                Try
                    If Left(hif.ID, 3) = "hif" Then
                        hif.Value = Generic.ToStr(dt.Rows(0)(columName))
                    End If
                Catch ex As Exception
                End Try
            ElseIf TypeOf objContent Is DropDownList Then
                Dim drp As New DropDownList
                Dim tableName As String = ""
                drp = CType(objContent, DropDownList)
                idLenght = drp.ID.Length - 3
                columName = Microsoft.VisualBasic.Mid(drp.ID, 4, idLenght)
                Try
                    If Left(drp.ID, 3) = "cbo" Then
                        Dim ftxt As String = ""
                        ftxt = Generic.ToStr(dt.Rows(0)(columName))
                        If ftxt = "0" Then
                            ftxt = ""
                        ElseIf ftxt = "" Then
                            ftxt = ""
                        ElseIf ftxt = 0 Then
                            ftxt = ""
                        End If
                        drp.Text = ftxt
                    End If
                Catch ex As Exception
                End Try
            ElseIf TypeOf objContent Is AjaxControlToolkit.ComboBox Then
                Dim drp As New AjaxControlToolkit.ComboBox
                Dim tableName As String = ""
                drp = CType(objContent, AjaxControlToolkit.ComboBox)
                idLenght = drp.ID.Length - 3
                columName = Microsoft.VisualBasic.Mid(drp.ID, 4, idLenght)
                Try
                    If Left(drp.ID, 3) = "cbo" Then
                        Dim ftxt As String = ""
                        ftxt = Generic.ToInt(dt.Rows(0)(columName))
                        If ftxt = "0" Then
                            ftxt = ""
                        ElseIf ftxt = 0 Then
                            ftxt = ""
                        End If
                        drp.Text = ftxt
                    End If
                Catch ex As Exception
                End Try
            ElseIf TypeOf objContent Is CheckBox Then
                Dim chk As New CheckBox
                chk = CType(objContent, CheckBox)
                Try
                    If Microsoft.VisualBasic.Mid(chk.ID, 1, 3) = "rdo" Then
                        Dim rdo As RadioButton
                        Dim rdoTag As Integer
                        rdo = CType(objContent, RadioButton)
                        columName = rdo.GroupName
                        rdoTag = Right(rdo.ID, 2)
                        Dim fVal As Integer

                        fVal = Generic.ToBol(dt.Rows(0)(columName))
                        If (rdoTag = fVal) Or (rdoTag = fVal * -1) Then
                            rdo.Checked = True
                        End If

                    ElseIf Microsoft.VisualBasic.Mid(chk.ID, 1, 3) = "txt" Then
                        idLenght = chk.ID.Length - 3
                        columName = Microsoft.VisualBasic.Mid(chk.ID, 4, idLenght)
                        chk.Checked = Generic.ToBol(dt.Rows(0)(columName))

                    ElseIf Microsoft.VisualBasic.Mid(chk.ID, 1, 3) = "chk" Then
                        idLenght = chk.ID.Length - 3
                        columName = Microsoft.VisualBasic.Mid(chk.ID, 4, idLenght)
                        chk.Checked = Generic.ToBol(dt.Rows(0)(columName))
                    End If
                Catch ex As Exception
                End Try
            ElseIf TypeOf objContent Is RadioButton Then
                Dim rdo As New RadioButton
                rdo = CType(objContent, RadioButton)

            ElseIf TypeOf objContent Is RadioButtonList Then
                Dim rbl As New RadioButtonList
                rbl = CType(objContent, RadioButtonList)
                Dim tableName As String = ""
                idLenght = rbl.ID.Length - 3
                columName = Microsoft.VisualBasic.Mid(rbl.ID, 4, idLenght)
                Try
                    If Left(rbl.ID, 3) = "rbl" Then
                        Dim ftxt As String = ""
                        ftxt = Generic.ToInt(dt.Rows(0)(columName))
                        rbl.Text = ftxt
                    End If
                Catch ex As Exception
                End Try
            End If
        End Sub

        Public Shared Sub HidePlaceHolder(owner As Control, ContainerID As String)
            Try
                Dim page As Page = If(TryCast(owner, Page), owner.Page)
                Dim tempContainer As Control
                tempContainer = Generic.FindControlRecursive(page, ContainerID)
                For Each obj As Control In tempContainer.Controls
                    If TypeOf obj Is PlaceHolder Then
                        obj.Visible = False
                    End If
                Next
                'Return tempContainer.Controls.Count
            Catch ex As Exception

            End Try
        End Sub

        Public Shared Sub PopulateDXGridFilter(grd As DevExpress.Web.ASPxGridView, UserNo As Integer, PayLocNo As Integer)
            For i As Integer = 0 To grd.Columns.Count - 1
                If TypeOf grd.Columns(i) Is GridViewDataComboBoxColumn Then
                    Dim table As String = "E" & Generic.ToStr(CType(grd.Columns(i), GridViewDataComboBoxColumn).FieldName).Replace("Desc", "")
                    CType(grd.Columns(i), GridViewDataComboBoxColumn).PropertiesComboBox.TextField = "tDesc"
                    CType(grd.Columns(i), GridViewDataComboBoxColumn).PropertiesComboBox.TextField = "tDesc"
                    CType(grd.Columns(i), GridViewDataComboBoxColumn).PropertiesComboBox.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, table, PayLocNo, "", "")
                End If
            Next
        End Sub

        Public Shared Sub PopulateDropDownList(OnlineUserNo As Integer, owner As Control, ContainerID As String, PayLocNo As Integer)
            Try
                Dim page As Page = If(TryCast(owner, Page), owner.Page)
                Dim tempContainer As Control
                tempContainer = FindControlRecursive(page, ContainerID)
                For Each obj As Control In tempContainer.Controls
                    PopulateDropDownListDeti(OnlineUserNo, obj, PayLocNo)
                Next
            Catch ex As Exception

            End Try
        End Sub
        Private Shared Sub PopulateDropDownListDeti(OnlineUserNo As Integer, ctrl As Control, Optional PayLocNo As Integer = 0)
            Dim columName As String = ""
            Dim idLenght As Integer = 0

            If TypeOf ctrl Is DropDownList Then
                Dim ddl As New DropDownList
                Dim tablename As String = ""

                ddl = CType(ctrl, DropDownList)
                If ddl.DataMember <> "" Then
                    tablename = ddl.DataMember
                    Try
                        ddl.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", OnlineUserNo, tablename, PayLocNo, "", "")
                        ddl.DataTextField = "tdesc"
                        ddl.DataValueField = "tNo"
                        ddl.DataBind()
                    Catch ex As Exception

                    End Try                    
                End If
            End If
        End Sub
        Public Shared Sub PopulateDropDownList_One(OnlineUserNo As Integer, owner As Control, ContainerID As String, dt As DataTable)
            Try
                Dim page As Page = If(TryCast(owner, Page), owner.Page)
                Dim tempContainer As Control
                tempContainer = FindControlRecursive(page, ContainerID)
                For Each obj As Control In tempContainer.Controls
                    PopulateDropDownListDeti_One(OnlineUserNo, obj, dt)
                Next
            Catch ex As Exception

            End Try
        End Sub
        Private Shared Sub PopulateDropDownListDeti_One(OnlineUserNo As Integer, ctrl As Control, dt As DataTable)
            Dim columName As String = ""
            Dim idLenght As Integer = 0
            Dim iNo As Integer = 0

            If TypeOf ctrl Is DropDownList Then
                Dim ddl As New DropDownList
                Dim tablename As String = ""
                ddl = CType(ctrl, DropDownList)
                idLenght = ddl.ID.Length - 3
                columName = Microsoft.VisualBasic.Mid(ddl.ID, 4, idLenght)
                If ddl.DataMember <> "" Then
                    tablename = ddl.DataMember
                    iNo = Generic.ToInt(dt.Rows(0)(columName))
                    ddl.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup_One", OnlineUserNo, tablename, iNo)
                    ddl.DataTextField = "tdesc"
                    ddl.DataValueField = "tNo"
                    ddl.DataBind()
                End If
            End If
        End Sub
        Public Shared Sub PopulateDropDownList_Union(OnlineUserNo As Integer, owner As Control, ContainerID As String, dt As DataTable, payLocNo As Integer)
            Try
                Dim page As Page = If(TryCast(owner, Page), owner.Page)
                Dim tempContainer As Control
                tempContainer = FindControlRecursive(page, ContainerID)
                For Each obj As Control In tempContainer.Controls
                    PopulateDropDownListDeti_Union(OnlineUserNo, obj, dt, payLocNo)
                Next
            Catch ex As Exception

            End Try
        End Sub
        Private Shared Sub PopulateDropDownListDeti_Union(OnlineUserNo As Integer, ctrl As Control, dt As DataTable, payLocNo As Integer)
            Try
                Dim columName As String = ""
                Dim idLenght As Integer = 0
                Dim iNo As Integer = 0

                If TypeOf ctrl Is DropDownList Then
                    Dim ddl As New DropDownList
                    Dim tablename As String = ""
                    ddl = CType(ctrl, DropDownList)
                    idLenght = ddl.ID.Length - 3
                    columName = Microsoft.VisualBasic.Mid(ddl.ID, 4, idLenght)
                    If ddl.DataMember <> "" Then
                        tablename = ddl.DataMember
                        iNo = Generic.ToInt(dt.Rows(0)(columName))
                        ddl.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup_Union", OnlineUserNo, tablename, payLocNo, "", "", iNo)
                        ddl.DataTextField = "tdesc"
                        ddl.DataValueField = "tNo"
                        ddl.DataBind()
                    End If
                End If
            Catch ex As Exception

            End Try
        End Sub

        Public Shared Sub PopulateDropDownList_Self(OnlineUserNo As Integer, owner As Control, ContainerID As String, PayLocNo As Integer)
            Try
                Dim page As Page = If(TryCast(owner, Page), owner.Page)
                Dim tempContainer As Control
                tempContainer = FindControlRecursive(page, ContainerID)
                For Each obj As Control In tempContainer.Controls
                    PopulateDropDownListDeti_Self(OnlineUserNo, obj, PayLocNo)
                Next
            Catch ex As Exception

            End Try
        End Sub

        Private Shared Sub PopulateDropDownListDeti_Self(OnlineUserNo As Integer, ctrl As Control, Optional PayLocNo As Integer = 0)
            Dim columName As String = ""
            Dim idLenght As Integer = 0

            If TypeOf ctrl Is DropDownList Then
                Dim ddl As New DropDownList
                Dim tablename As String = ""

                ddl = CType(ctrl, DropDownList)
                If ddl.DataMember <> "" Then
                    tablename = ddl.DataMember
                    ddl.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup_Self", OnlineUserNo, tablename, PayLocNo, "", "")
                    ddl.DataTextField = "tdesc"
                    ddl.DataValueField = "tNo"
                    ddl.DataBind()
                End If
            End If
        End Sub
        Public Shared Sub PopulateDropDownList_Applicant(OnlineUserNo As Integer, owner As Control, ContainerID As String, PayLocNo As Integer)
            Try
                Dim page As Page = If(TryCast(owner, Page), owner.Page)
                Dim tempContainer As Control
                tempContainer = FindControlRecursive(page, ContainerID)
                For Each obj As Control In tempContainer.Controls
                    PopulateDropDownListDeti_Applicant(OnlineUserNo, obj, PayLocNo)
                Next
            Catch ex As Exception

            End Try
        End Sub

        Private Shared Sub PopulateDropDownListDeti_Applicant(OnlineUserNo As Integer, ctrl As Control, Optional PayLocNo As Integer = 0)
            Dim columName As String = ""
            Dim idLenght As Integer = 0

            If TypeOf ctrl Is DropDownList Then
                Dim ddl As New DropDownList
                Dim tablename As String = ""

                ddl = CType(ctrl, DropDownList)
                If ddl.DataMember <> "" Then
                    tablename = ddl.DataMember
                    ddl.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup_Applicant", OnlineUserNo, tablename, PayLocNo, "", "")
                    ddl.DataTextField = "tdesc"
                    ddl.DataValueField = "tNo"
                    ddl.DataBind()
                End If
            End If
        End Sub
        Public Shared Sub EnableControls(owner As Control, ContainerID As String, IsEnabled As Boolean)
            Try
                Dim page As Page = If(TryCast(owner, Page), owner.Page)
                Dim tempContainer As Control
                tempContainer = FindControlRecursive(page, ContainerID)
                For Each obj As Control In tempContainer.Controls
                    If TypeOf obj Is TextBox Then
                        Dim txt As New TextBox
                        txt = CType(obj, TextBox)
                        txt.Enabled = IsEnabled
                    End If
                    If TypeOf obj Is DropDownList Then
                        Dim cbo As New DropDownList
                        cbo = CType(obj, DropDownList)
                        cbo.Enabled = IsEnabled
                    End If
                    If TypeOf obj Is CheckBox Then
                        Dim chk As New CheckBox
                        chk = CType(obj, CheckBox)
                        chk.Enabled = IsEnabled
                    End If
                    If TypeOf obj Is RadioButton Then
                        Dim rdo As New RadioButton
                        rdo = CType(obj, RadioButton)
                        rdo.Enabled = IsEnabled
                    End If
                    If TypeOf obj Is RadioButtonList Then
                        Dim rbl As New RadioButtonList
                        rbl = CType(obj, RadioButtonList)
                        rbl.Enabled = IsEnabled
                    End If
                    If TypeOf obj Is CheckBoxList Then
                        Dim cbl As New CheckBoxList
                        cbl = CType(obj, CheckBoxList)
                        cbl.Enabled = IsEnabled
                    End If
                    If TypeOf obj Is ListBox Then
                        Dim lst As New ListBox
                        lst = CType(obj, ListBox)
                        lst.Enabled = IsEnabled
                    End If
                    If TypeOf obj Is FileUpload Then
                        Dim fu As New FileUpload
                        fu = CType(obj, FileUpload)
                        fu.Enabled = IsEnabled
                    End If
                    If TypeOf obj Is DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor Then
                        Dim deHTMLEditor As New DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor
                        deHTMLEditor = obj
                        deHTMLEditor.Enabled = IsEnabled
                    End If
                Next
            Catch ex As Exception

            End Try
        End Sub

        Public Shared Sub ClearControls(owner As Control, ContainerID As String)
            Try
                Dim page As Page = If(TryCast(owner, Page), owner.Page)
                Dim tempContainer As Control
                tempContainer = FindControlRecursive(page, ContainerID)
                For Each obj As Control In tempContainer.Controls
                    If TypeOf obj Is HiddenField Then
                        Dim hif As New HiddenField
                        hif = CType(obj, HiddenField)
                        hif.Value = ""
                    End If
                    If TypeOf obj Is Label Then
                        Dim lbl As New Label
                        lbl = CType(obj, Label)
                        lbl.Text = ""
                    End If
                    If TypeOf obj Is TextBox Then
                        Dim txt As New TextBox
                        txt = CType(obj, TextBox)
                        txt.Text = ""
                    End If
                    If TypeOf obj Is DropDownList Then
                        Dim cbo As New DropDownList
                        cbo = CType(obj, DropDownList)
                        cbo.SelectedValue = ""
                    End If
                    If TypeOf obj Is CheckBox Then
                        Dim chk As New CheckBox
                        chk = CType(obj, CheckBox)
                        chk.Checked = False
                    End If
                    If TypeOf obj Is RadioButton Then
                        Dim rdo As New RadioButton
                        rdo = CType(obj, RadioButton)
                        rdo.Checked = False
                    End If
                    If TypeOf obj Is RadioButtonList Then
                        Dim rbl As New RadioButtonList
                        rbl = CType(obj, RadioButtonList)
                        rbl.ClearSelection()
                    End If
                    If TypeOf obj Is CheckBoxList Then
                        Dim cbl As New CheckBoxList
                        cbl = CType(obj, CheckBoxList)
                        cbl.ClearSelection()
                    End If
                    If TypeOf obj Is DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor Then
                        Dim deHTMLEditor As New DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor
                        deHTMLEditor = obj
                        deHTMLEditor.Html = ""
                    End If
                Next
            Catch ex As Exception

            End Try
        End Sub

        Public Shared Function Split(Obj As Object, index As Integer) As String
            Dim ret As String = ""
            Try
                Dim str As String = ToStr(Obj)
                Dim sentence As String() = str.Split("|"c)
                Dim i As Integer = 0
                For Each word As String In sentence
                    If i = index Then
                        ret = word
                        Exit For
                    End If
                    i += 1
                Next
            Catch
                ret = ""
            End Try
            Return ret
        End Function

        Public Shared Function GetPath(ByVal filepath As String) As String
            Dim i As Integer, ii As Integer = 0
            Dim tstr As String = "", tdummy As String
            i = Len(filepath)
            Dim tvalidate As Boolean

            Do While i > 0 And tvalidate = False
                ii = ii + 1
                tdummy = Mid(filepath, i, 1)
                If tdummy = "/" Then
                    tvalidate = True
                End If
                If tvalidate = False Then
                    tstr = Right(filepath, ii)
                End If
                i = i - 1
            Loop

            Return tstr

        End Function

        Public Shared Function CheckDBNull(ByVal obj As Object, Optional ByVal ObjectType As enumObjectType = enumObjectType.StrType) As Object
            Dim objReturn As Object


            objReturn = obj
            If ObjectType = enumObjectType.StrType And (IsDBNull(obj) Or IsNothing(obj)) Then
                objReturn = ""
            ElseIf ObjectType = enumObjectType.IntType And (IsDBNull(obj) Or IsNothing(obj)) Then
                objReturn = 0
            ElseIf ObjectType = enumObjectType.DblType And (IsDBNull(obj) Or IsNothing(obj)) Then
                objReturn = 0
            ElseIf ObjectType = enumObjectType.IntType And (Len(obj) = 0 Or IsNothing(obj)) Then
                objReturn = 0
            ElseIf ObjectType = enumObjectType.DblType And (Len(obj) = 0 Or IsNothing(obj)) Then
                objReturn = 0

            End If
            Return objReturn
        End Function

        'Delete Record
        Public Shared Function DeleteRecord(ByVal tProcedurename As String, ByVal userno As Integer, ByVal tId As Integer) As Integer
            Return SQLHelper.ExecuteNonQuery(tProcedurename, userno, tId)
        End Function

        Public Shared Function DeleteRecordAudit(ByVal ttablename As String, ByVal userno As Integer, ByVal tId As Integer) As Integer

            Return SQLHelper.ExecuteNonQuery("zRow_Delete", userno, ttablename, tId)
        End Function

        Public Shared Function DeleteRecordAuditCol(ByVal ttablename As String, ByVal userno As Integer, ByVal Lid As String, ByVal tId As Integer) As Integer

            Return SQLHelper.ExecuteNonQuery("zRow_DeleteCol", userno, ttablename, Lid, tId)
        End Function

        Public Shared Function GetFirstTab(id As String) As String
            Dim context As HttpContext = HttpContext.Current
            Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
            Dim Folder As String = FileInfo.Directory.Name
            Dim FileName As String = Path.GetFileName(FileInfo.ToString)
            Dim MenuType As String = Left(Generic.ToStr(context.Session("xMenuType")), 4)
            Dim dt As DataTable
            Dim ComponentNo As Integer
            Dim retval As String = ""
            'switch folder
            Select Case Folder
                Case "secured"
                    ComponentNo = 1
                Case "securedmanager"
                    ComponentNo = 2
                Case "securedself"
                    ComponentNo = 3
            End Select
            Try
                dt = SQLHelper.ExecuteDataTable("EMenu_Tab", Generic.ToInt(context.Session("OnlineUserNo")), FileName, MenuType, ComponentNo, "Tab")
                context.Session("xMenuType") = Generic.ToStr(dt.Rows(0)("MenuType"))
                retval = "~/" & Folder & "/" & Generic.ToStr(dt.Rows(0)("FormName")) & "?id=" & id
            Catch ex As Exception
                retval = ""
            End Try
            Return retval
        End Function

        Public Shared Function GetSkin() As String
            'Dim fs As FileStream, RetVal As String = "theme-default.css", i As Integer, iInitArr As String
            Dim fs As FileStream, RetVal As String = "ffffff", i As Integer, iInitArr As String
            Dim filename As String
            If System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/secured/connectionstr/skin.ini")) = True Then
                filename = HttpContext.Current.Server.MapPath("~/secured/connectionstr/") & "skin.ini"
            Else
                Dim FileHolder As FileInfo
                Dim WriteFile As StreamWriter
                Dim path As String
                path = HttpContext.Current.Server.MapPath("~/secured/connectionstr")
                If Not IO.Directory.Exists(path) Then
                    IO.Directory.CreateDirectory(path)
                End If
                filename = path & "\skin.ini"
                FileHolder = New FileInfo(filename)
                WriteFile = FileHolder.CreateText()
                WriteFile.WriteLine(RetVal)
                WriteFile.Close()
            End If

            fs = New FileStream(filename, FileMode.Open, FileAccess.Read)
            Dim l As Integer = 0, ftext As String = ""
            Dim d As New StreamReader(fs)
            Try
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                If d.Peek() > 0 Then
                    While d.Peek() > -1
                        i = d.Peek
                        ftext = d.ReadLine()
                        iInitArr = ftext
                        If l = 0 Then
                            RetVal = iInitArr
                        End If

                    End While
                    d.Close()
                End If
                d.Close()
            Catch
                d.Close()
            End Try
            Return RetVal
        End Function

        Public Shared Sub PopulateDataDisabled(owner As Control, ContainerID As String, UserNo As Integer, PayLocNo As Integer, MenuType As String)
            Try

                Dim page As Page = If(TryCast(owner, Page), owner.Page)
                Dim tempContainer As Control
                tempContainer = FindControlRecursive(page, ContainerID)

                Dim ResponseTypeNo As Integer
                Dim objName As String
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EFormField_Web", UserNo, MenuType, PayLocNo)
                For Each row As DataRow In dt.Rows
                    ResponseTypeNo = Generic.ToInt(row("ResponseTypeNo"))
                    objName = Generic.ToStr(row("Controls"))

                    If ResponseTypeNo = 3 Then
                        Dim txt As New TextBox
                        txt = tempContainer.FindControl(objName)
                        txt.Enabled = False
                    ElseIf ResponseTypeNo = 4 Then
                        Dim ddl As New DropDownList
                        ddl = tempContainer.FindControl(objName)
                        ddl.Enabled = False
                    ElseIf ResponseTypeNo = 2 Then
                        Dim chk As New CheckBox
                        chk = tempContainer.FindControl(objName)
                        chk.Enabled = False
                    ElseIf ResponseTypeNo = 1 Then
                        Dim rdo As New RadioButton
                        rdo = tempContainer.FindControl(objName)
                        rdo.Enabled = False
                    End If
                Next

            Catch ex As Exception

            End Try
        End Sub

        Public Shared Function ReportParam(ParamArray Parameters As ReportParameter()) As String
            Dim str As String = ""
            For Each param As ReportParameter In Parameters
                If Len(str) > 1 Then
                    str = str & "~"
                End If
                Select Case param.ParamType
                    Case 1
                        str = str & "int|"
                    Case 2
                        str = str & "dec|"
                    Case 3
                        str = str & "str|"
                    Case 4
                        str = str & "bol|"
                End Select
                str = str & param.ParamValue
            Next
            Return str
        End Function

        Private Shared Function GetIniFile() As String

            Dim iInitArr As String
            Dim i As Integer
            Dim fs As FileStream
            Dim ConnectionString As String = ""
            Dim filename = HttpContext.Current.Server.MapPath("~/secured/connectionstr/") & "peoplecore.ini"
            Dim fservername As String = ""
            Dim fdatabasename As String = ""
            Dim fsqllogin As String = ""
            Dim fsqlpass As String = ""

            If Not IO.File.Exists(filename) Then
                HttpContext.Current.Response.Redirect("~/connection.aspx")
                Return ""
            End If

            fs = New FileStream(filename, FileMode.Open, FileAccess.Read)
            Dim l As Integer = 0, ftext As String = ""
            Dim d As New StreamReader(fs)
            Try
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                If d.Peek() > 0 Then
                    While d.Peek() > -1
                        i = d.Peek
                        ftext = PeopleCoreCrypt.Decrypt(d.ReadLine())
                        iInitArr = ftext
                        If l = 0 Then
                            fservername = iInitArr
                        ElseIf l = 1 Then
                            fdatabasename = iInitArr
                        ElseIf l = 2 Then
                            fsqllogin = iInitArr
                        ElseIf l = 3 Then
                            fsqlpass = iInitArr
                        End If
                        l = l + 1
                    End While
                    ConnectionString = "Password=" & fsqlpass & ";Persist Security Info=True;User ID=" & fsqllogin & " ;Initial Catalog=" & fdatabasename & " ;Data Source=" & fservername & ""
                End If
                d.Close()
            Catch
                d.Close()
            End Try
            Return ConnectionString

        End Function

        Public Shared Sub PopulateSQLDatasource(SQL As String, SQLDS As SqlDataSource, ParamArray param() As String)
            Try
                Dim conn As SqlConnection = New SqlConnection(GetIniFile())
                Dim cmd As SqlCommand = New SqlCommand(SQL, conn)
                Dim i As Integer = 0
                cmd.CommandType = CommandType.StoredProcedure
                conn.Open()
                SqlCommandBuilder.DeriveParameters(cmd)
                SQLDS.SelectParameters.Clear()
                For Each p As SqlParameter In cmd.Parameters
                    If p.ParameterName <> "@RETURN_VALUE" Then                        
                        SQLDS.SelectParameters.Add(Replace(p.ParameterName, "@", ""), p.DbType, param.GetValue(i))
                        SQLDS.SelectParameters.Item(i).ConvertEmptyStringToNull = False
                        i = i + 1
                    End If
                Next
                SQLDS.ConnectionString = GetIniFile()
                SQLDS.SelectCommand = SQL
                SQLDS.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
            Catch ex As Exception

            End Try

        End Sub

        Public Shared Function GetFileType() As String()

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("SELECT FileTypeCode FROM EFileType WHERE ISNULL(IsArchived,0)=0")

            Dim str(dt.Rows.Count - 1) As String
            Dim i As Integer = 0

            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    str(i) = Generic.ToStr(row("FileTypeCode")).ToLower()
                    i = i + 1
                Next
            End If

            Return str
           
        End Function

    End Class

#End Region

#Region "Generic Properties"
    Public Class ReportParameter

        Enum Type
            int = 1
            dec = 2
            str = 3
            bol = 4
        End Enum

        Private _ParamType As String
        Public Property ParamType() As Integer
            Get
                Return _ParamType
            End Get
            Set(value As Integer)
                _ParamType = value
            End Set
        End Property

        Private _ParamValue As String
        Public Property ParamValue() As String
            Get
                Return _ParamValue
            End Get
            Set(value As String)
                _ParamValue = value
            End Set
        End Property

        Public Sub New(ParamType As Type, ParamValue As String)
            _ParamType = ParamType
            _ParamValue = ParamValue
        End Sub

    End Class
#End Region

#Region "Security"

#Region "Cryptography"

    Public NotInheritable Class Security

        Private Const ENCRYPTION_KEY As String = "Pkunzip@112"

        Private Shared ReadOnly SALT As Byte() = Encoding.ASCII.GetBytes(ENCRYPTION_KEY.Length.ToString())

        Public Shared Function Encrypt(inputText As String) As String
            Try
                Dim rijndaelCipher As New RijndaelManaged()
                Dim plainText As Byte() = Encoding.Unicode.GetBytes(inputText)
                Dim SecretKey As New PasswordDeriveBytes(ENCRYPTION_KEY, SALT)

                Using encryptor As ICryptoTransform = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16))
                    Using memoryStream As New MemoryStream()
                        Using cryptoStream As New CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)
                            cryptoStream.Write(plainText, 0, plainText.Length)
                            cryptoStream.FlushFinalBlock()
                            Return Convert.ToBase64String(memoryStream.ToArray())
                        End Using
                    End Using

                End Using
            Catch generatedExceptionName As Exception
                Return String.Empty
            End Try

        End Function

        Public Shared Function Decrypt(inputText As String) As String
            Try
                Dim rijndaelCipher As New RijndaelManaged()
                Dim encryptedData As Byte() = Convert.FromBase64String(inputText)
                Dim secretKey As New PasswordDeriveBytes(ENCRYPTION_KEY, SALT)

                Using decryptor As ICryptoTransform = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16))
                    Using memoryStream As New MemoryStream(encryptedData)
                        Using cryptoStream As New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
                            Dim plainText As Byte() = New Byte(encryptedData.Length - 1) {}
                            Dim decryptedCount As Integer = cryptoStream.Read(plainText, 0, plainText.Length)
                            Return Encoding.Unicode.GetString(plainText, 0, decryptedCount)
                        End Using
                    End Using
                End Using
            Catch generatedExceptionName As Exception
                Return String.Empty
            End Try

        End Function

    End Class
#End Region

#Region "QueryStringModule"

    Public Class QueryStringModule : Implements IHttpModule
        Public Sub Dispose() Implements System.Web.IHttpModule.Dispose
            ' Nothing to dispose            
        End Sub
        Public Sub Init(context As HttpApplication) Implements System.Web.IHttpModule.Init
            AddHandler context.BeginRequest, AddressOf context_BeginRequest
        End Sub
        Private Const PARAMETER_NAME As String = "enc="
        Private Sub context_BeginRequest(sender As Object, e As EventArgs)
            Dim context As HttpContext = HttpContext.Current
            If (context.Request.Url.OriginalString.Contains("aspx") Or context.Request.Url.OriginalString.Contains("ASPX")) AndAlso context.Request.RawUrl.Contains("?") AndAlso context.Request.RawUrl.Contains("&AsyncFileUploadID=") = False AndAlso context.Request.RawUrl.Contains("&DXProgressHandlerKey=") = False Then
                Dim query As String = ExtractQuery(context.Request.RawUrl)
                Dim path As String = GetVirtualPath()

                If query.StartsWith(PARAMETER_NAME, StringComparison.OrdinalIgnoreCase) Then
                    Dim rawQuery As String = query.Replace(PARAMETER_NAME, String.Empty)
                    Dim decryptedQuery As String = Security.Decrypt(rawQuery)
                    context.RewritePath(path, String.Empty, decryptedQuery)
                ElseIf context.Request.HttpMethod = "GET" Then
                    Dim encryptedQuery As String = "?" & PARAMETER_NAME & Security.Encrypt(query)
                    context.Response.Redirect(path & encryptedQuery)
                End If
            End If
        End Sub
        Private Shared Function GetVirtualPath() As String
            Dim path As String = HttpContext.Current.Request.RawUrl
            path = path.Substring(0, path.IndexOf("?"))
            path = path.Substring(path.LastIndexOf("/") + 1)
            Return path.ToLower()
        End Function
        Private Shared Function ExtractQuery(url As String) As String
            Dim index As Integer = url.IndexOf("?") + 1
            Return url.Substring(index)
        End Function
    End Class

#Region "Postback Encryption"

    Public Class FormRewriterAdapter
        Inherits System.Web.UI.Adapters.ControlAdapter
        Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
            MyBase.Render(New HtmlTextReWriter(writer))
        End Sub
    End Class
    Public Class HtmlTextReWriter
        Inherits System.Web.UI.HtmlTextWriter
        Private Const WRITTEN_ACTION_KEY As String = ""
        Public Sub New(ByVal writer As System.Web.UI.HtmlTextWriter)
            MyBase.New(writer)
            Me.InnerWriter = writer.InnerWriter
        End Sub
        Public Overrides Sub WriteAttribute(ByVal name As String, ByVal value As String, ByVal encode As Boolean)
            If name = "action" Then
                Dim Context As HttpContext = HttpContext.Current
                If Context.Items(WRITTEN_ACTION_KEY) Is Nothing Then
                    value = Replace(Context.Request.RawUrl, Path.GetFileName(Context.Request.PhysicalPath), Path.GetFileName(Context.Request.PhysicalPath).ToLower())
                    Context.Items(WRITTEN_ACTION_KEY) = True
                End If
            End If
            MyBase.WriteAttribute(name, value, encode)
        End Sub
    End Class

#End Region


#End Region

#Region "Permission"

    Public NotInheritable Class Permission

        Shared page As String = ""
        Shared table As String = ""
        Shared userno As Integer = 0
        Shared FileName As String = ""
        Shared TableName As String = ""
        Shared Folder As String = ""
        Shared ComponentNo As Integer = 3
        Shared MenuMassNo As Integer = 0
        Shared hash As String = ""
        Shared IsSupervisor As Boolean = False
        Shared ApplicantNo As Integer = 0
        Shared IsCoreUser As Boolean = False

        Private Shared Sub Initialized()
            Dim context As HttpContext = HttpContext.Current
            Dim FileInfo As FileInfo = New FileInfo(context.Request.Url.AbsolutePath)
            TableName = Generic.ToStr(context.Request.QueryString("ds"))
            MenuMassNo = Generic.ToInt(context.Request.QueryString("gno"))
            Folder = FileInfo.Directory.Name
            FileName = Path.GetFileName(FileInfo.ToString)
            Select Case Folder
                Case "secured"
                    ComponentNo = 1
                Case "securedmanager"
                    ComponentNo = 2
                Case "securedself"
                    ComponentNo = 3
                Case "securedapp"
                    ComponentNo = 4
            End Select
            userno = Generic.ToInt(context.Session("OnlineUserNo"))
            IsSupervisor = Generic.ToBol(context.Session("IsSupervisor"))
            ApplicantNo = Generic.ToInt(context.Session("OnlineApplicantNo"))
            IsCoreUser = Generic.ToBol(context.Session("IsCoreUser"))
            'hash = Generic.ToStr(ConfigurationManager.AppSettings("hash"))

        End Sub

        Public Shared Function IsAuthenticated() As Boolean
            Try
                Dim c As HttpContext = HttpContext.Current
                Initialized()
                If userno = 0 Then
                    c.Response.Redirect("~/PageExpired.aspx?i=1")
                End If                
            Catch ex As Exception

            End Try
            Return True
        End Function

        Public Shared Function IsAuthenticatedSuperior() As Boolean
            Try
                Dim c As HttpContext = HttpContext.Current
                Initialized()
                If IsSupervisor = False Or userno = 0 Then
                    c.Response.Redirect("~/PageExpired.aspx?i=1")
                End If
            Catch ex As Exception

            End Try
            
            Return IsSupervisor
        End Function

        Public Shared Function IsAuthenticatedCoreUser() As Boolean
            Try
                Dim c As HttpContext = HttpContext.Current
                Initialized()
                If IsCoreUser = False Or userno = 0 Then
                    c.Response.Redirect("~/PageExpired.aspx?i=1")
                End If
            Catch ex As Exception
            End Try
            Return IsCoreUser
        End Function

        Public Shared Sub IsAuthenticatedApplicant()
            Dim c As HttpContext = HttpContext.Current
            Initialized()
            If ApplicantNo = 0 Then
                c.Response.Redirect("~/PageExpired.aspx?i=1")
            End If
        End Sub

        Public Shared Function View() As Boolean
            Dim retVal As Boolean = False
            Try
                Initialized()
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EMenu_WebOne", userno, FileName, TableName, ComponentNo)
                For Each row As DataRow In dt.Rows
                    retVal = Generic.ToBol(row("IsView"))
                Next
                Return retVal
            Catch ex As Exception
                retVal = False
            End Try
            Return retVal
        End Function

        Public Shared Function Add() As Boolean
            Dim retVal As Boolean = False
            Try
                Initialized()
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EMenu_WebOne", userno, FileName, TableName, ComponentNo)
                For Each row As DataRow In dt.Rows
                    retVal = Generic.ToBol(row("IsAdd"))
                Next
            Catch ex As Exception
                retVal = False
            End Try
            Return retVal
        End Function

        Public Shared Function Edit() As Boolean
            Dim retVal As Boolean = False
            Try
                Initialized()
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EMenu_WebOne", userno, FileName, TableName, ComponentNo)
                For Each row As DataRow In dt.Rows
                    retVal = Generic.ToBol(row("IsEdit"))
                Next
            Catch ex As Exception
                retVal = False
            End Try
            Return retVal
        End Function

        Public Shared Function Delete() As Boolean
            Dim retVal As Boolean = False
            Try
                Initialized()
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EMenu_WebOne", userno, FileName, TableName, ComponentNo)
                For Each row As DataRow In dt.Rows
                    retVal = Generic.ToBol(row("IsDelete"))
                Next
            Catch ex As Exception
                retVal = False
            End Try
            Return retVal
        End Function

        Public Shared Function Process() As Boolean
            Dim retVal As Boolean = False
            Try
                Initialized()
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EMenu_WebOne", userno, FileName, TableName, ComponentNo)
                For Each row As DataRow In dt.Rows
                    retVal = Generic.ToBol(row("IsProcess"))
                Next
            Catch ex As Exception
                retVal = False
            End Try
            Return retVal
        End Function

        Public Shared Function Post() As Boolean
            Dim retVal As Boolean = False
            Try
                Initialized()
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EMenu_WebOne", userno, FileName, TableName, ComponentNo)
                For Each row As DataRow In dt.Rows
                    retVal = Generic.ToBol(row("IsPost"))
                Next
            Catch ex As Exception
                retVal = False
            End Try
            Return retVal
        End Function
    End Class

#End Region

#Region "Array"

#End Region

#Region "Access Rights"

    Public NotInheritable Class AccessRights
        Enum EnumPermissionType
            AllowAdd = 1
            AllowEdit = 2
            AllowDelete = 3
            AllowView = 4
            AllowPost = 5
            AllowProcess = 6
        End Enum

        Public Shared Function IsAllowUser(ByVal UserNo As Integer, bPermissionType As EnumPermissionType, Optional ByVal curFormName As String = "", Optional curtableName As String = "") As Boolean
            Try
                Dim bRetVal As Boolean = False
                Dim context As HttpContext = HttpContext.Current
                Dim formname As String = "", tableName As String = "", menuType As String = ""

                formname = Generic.ToStr(context.Request.ServerVariables("SCRIPT_NAME"))
                formname = Generic.GetPath(formname)
                tableName = Generic.ToStr(context.Session("xTablename"))
                menuType = Generic.ToStr(context.Session("xMenutype"))
                If curFormName <> "" Then
                    formname = curFormName
                End If
                If curtableName <> "" Then
                    tableName = curtableName
                End If

                'Dim ds As DataSet = SQLHelper.ExecuteDataSet("Emenu_WebOne", UserNo, formname, tableName, menuType, 1)
                'If ds.Tables.Count > 0 Then
                '    If ds.Tables(0).Rows.Count > 0 Then
                '        Select Case bPermissionType
                '            Case EnumPermissionType.AllowDelete : bRetVal = Generic.ToBol(ds.Tables(0).Rows(0)("IsDelete"))
                '            Case EnumPermissionType.AllowAdd : bRetVal = Generic.ToBol(ds.Tables(0).Rows(0)("IsAdd"))
                '            Case EnumPermissionType.AllowEdit : bRetVal = Generic.ToBol(ds.Tables(0).Rows(0)("IsEdit"))
                '            Case EnumPermissionType.AllowView : bRetVal = Generic.ToBol(ds.Tables(0).Rows(0)("IsView"))
                '            Case EnumPermissionType.AllowPost : bRetVal = Generic.ToBol(ds.Tables(0).Rows(0)("IsPost"))
                '            Case EnumPermissionType.AllowProcess : bRetVal = Generic.ToBol(ds.Tables(0).Rows(0)("IsProcess"))
                '                '...etc
                '        End Select

                '    End If
                'End If
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("Emenu_WebOne", UserNo, formname, tableName, menuType, 1)
                For Each row As DataRow In dt.Rows
                    Select bPermissionType
                        Case EnumPermissionType.AllowDelete : bRetVal = Generic.ToBol(row("IsDelete"))
                        Case EnumPermissionType.AllowAdd : bRetVal = Generic.ToBol(row("IsAdd"))
                        Case EnumPermissionType.AllowEdit : bRetVal = Generic.ToBol(row("IsEdit"))
                        Case EnumPermissionType.AllowView : bRetVal = Generic.ToBol(row("IsView"))
                        Case EnumPermissionType.AllowPost : bRetVal = Generic.ToBol(row("IsPost"))
                        Case EnumPermissionType.AllowProcess : bRetVal = Generic.ToBol(row("IsProcess"))                            
                    End Select
                Next
                dt.Dispose()

                Return bRetVal
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Shared Function GetDeniedMessage(ByVal bPermissionType As EnumPermissionType) As String
            Dim bRetVal As String
            bRetVal = "Please secure permission from your team leader or administrator."
            Select Case bPermissionType
                Case EnumPermissionType.AllowDelete : bRetVal = "Access Denied! Please secure DELETE permission from your administrator."
                Case EnumPermissionType.AllowAdd : bRetVal = "Access Denied! Please secure ADD permission from your administrator."
                Case EnumPermissionType.AllowEdit : bRetVal = "Access Denied! Please secure EDIT permission from your administrator."
                Case EnumPermissionType.AllowPost : bRetVal = "Access Denied! Please secure POST permission from your administrator."
                Case EnumPermissionType.AllowProcess : bRetVal = "Access Denied! Please secure PROCESS permission from your administrator."
                Case EnumPermissionType.AllowView : bRetVal = "Access Denied! Please secure VIEW permission from your administrator."

            End Select
            Return bRetVal
        End Function

        Public Shared Sub CheckUser(UserNo As Integer, Optional FormName As String = "", Optional tableName As String = "")
            Try
                Dim context As HttpContext = HttpContext.Current
                Dim FileInfo As FileInfo = New FileInfo(context.Request.Url.AbsolutePath)
                Dim Folder As String = FileInfo.Directory.Name
                If FormName = "" Then
                    FormName = context.Session("xFormName")
                End If
                If tableName = "" Then
                    tableName = context.Session("xTableName")
                End If
                If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowView, FormName, tableName) = False And UserNo <> 0 Then
                    context.Response.Redirect("~/" & Folder & "/page.aspx?i=2")
                ElseIf UserNo = 0 Then
                    context.Response.Redirect("~/PageExpired.aspx?i=1")
                End If
            Catch ex As Exception

            End Try
            
        End Sub

    End Class
#End Region

#End Region

#Region "Assynchronous"
    Public NotInheritable Class AssynChronous

        Public Shared Function xOpenConnectionAsyn(ByVal fconstr As String) As SqlConnection
            Try
                Dim ConSQLAsyn As SqlClient.SqlConnection
                ConSQLAsyn = New SqlClient.SqlConnection
                ConSQLAsyn.ConnectionString = fconstr
                ConSQLAsyn.Open()
                Return ConSQLAsyn
            Catch excp As Exception
                Return Nothing
            End Try
        End Function
        Public Shared Function xOpenConnection(ByVal fconstr As String) As SqlClient.SqlConnection
            Try
                Dim ConSQL As SqlClient.SqlConnection
                ConSQL = New SqlClient.SqlConnection
                ConSQL.ConnectionString = fconstr
                ConSQL.Open()
                Return ConSQL
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Shared Function RunCommandAsynchronous(ByVal command As SqlCommand, ByVal commandText As String, ByVal connectionString As String, Optional ByRef Iscompleted As Integer = 0) As String

            ' Given command text and connection string, asynchronously execute
            ' the specified command against the connection. For this example,
            ' the code displays an indicator as it is working, verifying the 
            ' asynchronous behavior. 
            Dim ttime As Date

            Using connection As New SqlConnection(connectionString)
                Try
                    Dim count As Integer = 0, iresult As Integer
                    ' Dim command As New SqlCommand(commandText, connection)
                    connection.Open()
                    If Not command Is Nothing Then
                        command.CommandText = commandText
                        command.CommandType = CommandType.StoredProcedure
                        command.CommandTimeout = 0
                        command.Connection = connection
                    End If


                    Dim result As IAsyncResult = command.BeginExecuteNonQuery()
                    ttime = Now()
                    While Not result.IsCompleted
                        Iscompleted = 0
                        'Console.WriteLine("Waiting ({0})", count)
                        ' xLabel.Text = "Waiting ({0})" & " - " & Now()

                        ' Response.Write("Waiting ({0})" & " - " & Now())


                        ' Wait for 1/10 second, so the counter
                        ' does not consume all available resources 
                        ' on the main thread.
                        Threading.Thread.Sleep(100)
                        count += 1
                    End While

                    Dim etime As TimeSpan
                    etime = (Now() - ttime)
                    Iscompleted = 1
                    iresult = command.EndExecuteNonQuery(result)
                    'xLabel.Text = "Command complete. Affected {0} rows." & iresult
                    'xLabel.Text = "Command complete. Processing Time is : " & Now()
                    RunCommandAsynchronous = "Command complete. Processing Time is : " & Now()

                Catch ex As SqlException
                    'xLabel.Text = "Error ({0}): {1}" & " " & ex.Number & " " & ex.Message
                    RunCommandAsynchronous = "Error ({0}): {1}" & " " & ex.Number & " " & ex.Message
                Catch ex As InvalidOperationException
                    'xLabel.Text = "Error: {0}" & " " & ex.Message
                    RunCommandAsynchronous = "Error: {0}" & " " & ex.Message
                Catch ex As Exception
                    ' You might want to pass these errors
                    ' back out to the caller.
                    'xLabel.Text = "Error: {0}" & " " & ex.Message
                    RunCommandAsynchronous = "Error: {0}" & " " & ex.Message
                End Try
            End Using
        End Function

        Public Shared Function xRunCommandAsynchronous(ByVal command As SqlCommand, ByVal commandText As String, ByVal connectionString As String, Optional ByRef Iscompleted As Integer = 0, Optional ByRef err_num As Integer = 0) As String

            ' Given command text and connection string, asynchronously execute
            ' the specified command against the connection. For this example,
            ' the code displays an indicator as it is working, verifying the 
            ' asynchronous behavior. 
            Dim ttime As Date

            Using connection As New SqlConnection(connectionString)
                Try
                    Dim count As Integer = 0, iresult As Integer
                    ' Dim command As New SqlCommand(commandText, connection)
                    connection.Open()
                    If Not command Is Nothing Then
                        command.CommandText = commandText
                        command.CommandType = CommandType.StoredProcedure
                        command.CommandTimeout = 0
                        command.Connection = connection
                    End If


                    Dim result As IAsyncResult = command.BeginExecuteNonQuery()
                    ttime = Now()
                    While Not result.IsCompleted
                        Iscompleted = 0
                        'Console.WriteLine("Waiting ({0})", count)
                        ' xLabel.Text = "Waiting ({0})" & " - " & Now()

                        ' Response.Write("Waiting ({0})" & " - " & Now())


                        ' Wait for 1/10 second, so the counter
                        ' does not consume all available resources 
                        ' on the main thread.
                        Threading.Thread.Sleep(100)
                        count += 1
                    End While

                    Dim etime As TimeSpan
                    etime = (Now() - ttime)
                    Iscompleted = 1
                    iresult = command.EndExecuteNonQuery(result)
                    'xLabel.Text = "Command complete. Affected {0} rows." & iresult
                    'xLabel.Text = "Command complete. Processing Time is : " & Now()
                    err_num = 0
                    xRunCommandAsynchronous = "Command complete. Processing Time is : " & Now()

                Catch ex As SqlException
                    'xLabel.Text = "Error ({0}): {1}" & " " & ex.Number & " " & ex.Message
                    err_num = ex.Number
                    xRunCommandAsynchronous = Replace("Msg, " & ex.Number & ", Line " & ex.LineNumber.ToString & ", Procedure " & ex.Procedure.ToString & ", " & ex.Errors(0).ToString, "'", "")

                Catch ex As InvalidOperationException
                    'xLabel.Text = "Error: {0}" & " " & ex.Message
                    err_num = 1
                    xRunCommandAsynchronous = "Error: {0}" & " " & ex.Message

                Catch ex As Exception
                    ' You might want to pass these errors
                    ' back out to the caller.
                    'xLabel.Text = "Error: {0}" & " " & ex.Message
                    err_num = 1
                    xRunCommandAsynchronous = "Error: {0}" & " " & ex.Message

                End Try
            End Using
        End Function
    End Class

#End Region

#Region "Padding"
    Public NotInheritable Class Pad
        Public Shared Function PadZero(ByVal NoOfChar As Double, ByVal WhatNumber As Double) As String
            Dim astr As String
            astr = Trim(Str(WhatNumber))
            PadZero = Mid("00000000000000000000", 1, NoOfChar - Len(astr)) & astr
        End Function

        Public Shared Function PadNetPay(ByVal xNetPay As String, ByVal xLen As Byte) As String

            Dim LeftValue As String, RightValue As String, pos As Byte

            LeftValue = "0"
            RightValue = "0"
            pos = InStr(1, xNetPay, ".", CompareMethod.Binary)

            xNetPay = Trim(xNetPay)
            If pos > 0 Then
                If pos > 1 Then
                    LeftValue = Left(xNetPay, pos - 1)
                    If pos < Len(xNetPay) Then
                        RightValue = Right(xNetPay, Len(xNetPay) - pos)
                    End If
                Else
                    RightValue = Right(xNetPay, Len(xNetPay) - 1)
                End If

            ElseIf Len(xNetPay) > 0 Then
                LeftValue = xNetPay
            End If

            If Len(RightValue) = 1 Then
                RightValue = RightValue + "0"
            End If
            RightValue = Left(RightValue, 2)
            LeftValue = PadZero(xLen - 2, CLng(LeftValue))
            PadNetPay = LeftValue & RightValue
        End Function
        Public Shared Function PadSpace(ByVal tString As String, ByVal tNum As Integer) As String
            Dim tLen As Integer

            tLen = Len(tString)
            If tLen <= tNum Then
                PadSpace = tString & Space(tNum - tLen)
            Else
                PadSpace = Mid(tString, 1, tNum)
            End If
        End Function

        Public Shared Function PadNetPayDec(ByVal NetPay As String, ByVal tLen As Integer, ByVal tFormat As String) As String

            Dim LeftValue As String, RightValue As String, Pos As Integer, ReturnValue As String = "", FormatChar As String

            LeftValue = "0"
            RightValue = "0"
            Pos = InStr(NetPay, ".", CompareMethod.Text)
            'NetPay = LTrim(RTrim(NetPay))
            NetPay = Trim(NetPay)
            'FormatChar = LTrim(RTrim(tFormat))
            FormatChar = Trim(tFormat)
            If Pos > 0 Then
                If Pos > 1 Then

                    LeftValue = Left(NetPay, Pos - 1)
                    If Pos < Len(NetPay) Then
                        RightValue = Right(NetPay, Len(NetPay) - Pos + 1)

                    Else
                        RightValue = Right(NetPay, Len(NetPay) - 1)
                    End If
                End If

            ElseIf Len(NetPay) > 0 Then
                LeftValue = NetPay
            End If
            If Len(RightValue) = 1 Then
                RightValue = "." & RightValue & "0"
            End If
            RightValue = Left(RightValue, 3)

            If Len(tFormat) > 0 Then
                If (FormatChar = 1) Then

                    LeftValue = PadZero(tLen - 2, CInt(LeftValue))
                    ReturnValue = (PadZero(tLen, LeftValue) + RightValue)

                ElseIf (FormatChar = 2) Then

                    ReturnValue = (PadStaticSpace(LeftValue, tLen, "L") + RightValue)
                End If


            End If
            PadNetPayDec = ReturnValue

        End Function


        Public Shared Function PadStaticSpace(ByVal tString As String, ByVal NoOfSpace As Integer, ByVal WhereToAdd As String) As String
            Dim AppendSpace As Integer
            Dim LenString As Integer
            Dim RetValue As String

            tString = RTrim(LTrim(tString))
            WhereToAdd = RTrim(LTrim(WhereToAdd))
            LenString = Len(tString)
            RetValue = ""

            If (LenString <= NoOfSpace) Then
                AppendSpace = (NoOfSpace - LenString)
                If Len(WhereToAdd) > 0 Then

                    If (WhereToAdd = "R") Then

                        RetValue = (tString + Space(AppendSpace))

                    ElseIf (WhereToAdd = "L") Then

                        RetValue = (Space(AppendSpace) + tString)
                    End If
                End If

            ElseIf (LenString > NoOfSpace) Then
                RetValue = tString

            End If
            PadStaticSpace = RetValue


        End Function



        Public Shared Function PadSpaceRightLeft(ByVal tString As String, ByVal NoOfSpace As Integer, ByVal IsRightLeft As Boolean) As String

            Dim AppendSpace As Integer
            Dim LenString As String
            Dim RetValue As String

            tString = RTrim(LTrim(tString))
            LenString = Len(tString)

            RetValue = ""
            If IsRightLeft = 1 Then

                If (LenString <= NoOfSpace) Then


                    AppendSpace = (NoOfSpace - LenString)
                    RetValue = (tString + Space(AppendSpace))

                ElseIf (LenString > NoOfSpace) Then
                    RetValue = Mid(tString, 1, NoOfSpace)
                End If

            Else

                If (LenString <= NoOfSpace) Then


                    AppendSpace = (NoOfSpace - LenString)
                    RetValue = (Space(AppendSpace) + tString)

                ElseIf (LenString > NoOfSpace) Then
                    RetValue = (Mid(tString, 1, NoOfSpace))

                End If
            End If


            PadSpaceRightLeft = RetValue


        End Function

    End Class
#End Region

#Region "QueryString"
    Public Class Query
        Public Shared Sub requestQuery(ByRef menuType As String)
            menuType = HttpContext.Current.Request.QueryString("menuType")

        End Sub

    End Class
#End Region

#Region "IP and Hostname Security **"
    Public NotInheritable Class IPSecurity


        Public Shared Function Get_hostName() As String
            Dim hostname As String
            hostname = HttpContext.Current.Request.ServerVariables("remote_host")
            'Dim hostname As String
            'hostname = System.Net.Dns.GetHostName
            Return hostname
        End Function

        Public Shared Function Get_IPSec() As String
            Dim ip As String

            ip = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If String.IsNullOrEmpty(ip) Then
                ip = HttpContext.Current.Request.ServerVariables("remote_addr")
            ElseIf String.IsNullOrEmpty(ip) Then
                ip = HttpContext.Current.Request.UserHostAddress
            End If
            Return ip

        End Function

        Public Shared Function Get_MACID() As String
            Dim ip As String = ""
            Dim strHostName As String
            Dim ipEntry As IPHostEntry = System.Net.Dns.GetHostByName(strHostName)
            Dim addr() As IPAddress = ipEntry.AddressList

            For i As Integer = 0 To addr.Length - 1
                ip = ip & ", " & addr(i).ToString
            Next

            'txtIPs.Text = ("IP Address " + i + addr[i].ToString() + ", HostName: " + strHostName);
            Return ip
        End Function


    End Class
#End Region
End Namespace
