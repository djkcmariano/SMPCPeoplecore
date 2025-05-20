Imports System.Data.SqlClient
Imports System.IO
Imports clsLib

Partial Class Secured_SecDBConnection
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim PayLocNo As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateData()
        End If

    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim FileHolder As FileInfo
            Dim WriteFile As StreamWriter
            Dim path As String, filename As String
            path = Server.MapPath("~/secured/connectionstr")
            If Not IO.Directory.Exists(path) Then
                IO.Directory.CreateDirectory(path)
            End If
            filename = path & "\peoplecore.ini"
            If IsValidConnection() Then
                FileHolder = New FileInfo(filename)
                WriteFile = FileHolder.CreateText()
                WriteFile.WriteLine(PeopleCoreCrypt.Encrypt(txtServer.Text))
                WriteFile.WriteLine(PeopleCoreCrypt.Encrypt(txtDatabase.Text))
                WriteFile.WriteLine(PeopleCoreCrypt.Encrypt(txtUsername.Text))
                WriteFile.WriteLine(PeopleCoreCrypt.Encrypt(txtPassword.Text))
                WriteFile.Close()
                MessageBox.SuccessResponse("Database connection has been successfully created.", Me, "../")
                'MessageBox.Success("Database connection has been successfully created.", Me)
            Else
                MessageBox.Warning("Invalid database connection.", Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Private Function IsValidConnection() As Boolean
        Dim conn As New SqlConnection()
        Dim connectionstring As String = "Password=" & txtPassword.Text & ";Persist Security Info=True;User ID=" & txtUsername.Text & " ;Initial Catalog=" & txtDatabase.Text & " ;Data Source=" & txtServer.Text & ""
        Try
            conn.ConnectionString = connectionstring
            conn.Open()
            If conn.State = Data.ConnectionState.Open Then
                conn.Close()
                conn.Dispose()
                Return True
            Else
                conn.Dispose()
                Return False
            End If
        Catch ex As Exception
            conn.Dispose()
            Return False
        End Try
    End Function

    Private Shared Function GetIniFile(Optional IsAsyc As Boolean = False) As String
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
            HttpContext.Current.Response.Redirect("~/secured/SecDBConnection.aspx")
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
                d.Close()
                If IsAsyc = False Then
                    ConnectionString = "Password=" & fsqlpass & ";Persist Security Info=True;User ID=" & fsqllogin & " ;Initial Catalog=" & fdatabasename & " ;Data Source=" & fservername & ""
                Else
                    ConnectionString = "Password=" & fsqlpass & ";Persist Security Info=True;User ID=" & fsqllogin & ";Initial Catalog=" & fdatabasename & " ;Data Source=" & fservername & " ; Asynchronous Processing=true"
                End If
            End If
            d.Close()
        Catch
            d.Close()
        End Try

        Return ConnectionString

    End Function

    Private Sub PopulateData()
        Dim iInitArr As String
        Dim i As Integer
        Dim fs As FileStream        
        Dim filename = HttpContext.Current.Server.MapPath("~/secured/connectionstr/") & "peoplecore.ini"
        Dim fservername As String = ""
        Dim fdatabasename As String = ""
        Dim fsqllogin As String = ""
        Dim fsqlpass As String = ""


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
                        txtServer.Text = iInitArr
                    ElseIf l = 1 Then
                        txtDatabase.Text = iInitArr
                    ElseIf l = 2 Then
                        txtUsername.Text = iInitArr                  
                    End If
                    l = l + 1
                End While
                d.Close()                
            End If
            d.Close()
        Catch
            d.Close()
        End Try

    End Sub

End Class
