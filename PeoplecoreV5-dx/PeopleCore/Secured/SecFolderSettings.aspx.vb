Imports System.Data.SqlClient
Imports System.IO
Imports clsLib
Imports System.Data

Partial Class Secured_SecFolderSettings
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim PayLocNo As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            populateData()
        End If

    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            'Dim FileHolder As FileInfo
            'Dim WriteFile As StreamWriter
            'Dim path As String, filename As String
            'path = Server.MapPath("~/secured/connectionstr")
            'If Not IO.Directory.Exists(path) Then
            '    IO.Directory.CreateDirectory(path)
            'End If
            'filename = path & "\folder.ini"

            'FileHolder = New FileInfo(filename)
            'WriteFile = FileHolder.CreateText()
            'WriteFile.WriteLine(txtFolder.Text)
            'WriteFile.Close()

            SQLHelper.ExecuteNonQuery("EDocFolder_WebSave", Generic.ToStr(txtFolder.Text))
            MessageBox.Success("Folder setting created succeddfully.", Me)

        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub



    Private Sub populateData()
        Dim ds As DataSet
        ds = SQLHelper.ExecuteDataSet("EDocFolder_WebOne")
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                txtFolder.Text = Generic.ToStr(ds.Tables(0).Rows(0)("path"))
            End If
        End If
    End Sub

    Private Sub PopulateData_old()
        Try


            Dim iInitArr As String
            Dim i As Integer
            Dim fs As FileStream
            Dim filename = HttpContext.Current.Server.MapPath("~/secured/connectionstr/") & "folder.ini"
            Dim fservername As String = ""
            Dim fdatabasename As String = ""
            Dim fsqllogin As String = ""
            Dim fsqlpass As String = ""


            fs = New FileStream(filename, FileMode.Open, FileAccess.Read)
            Dim l As Integer = 0, ftext As String = ""
            Dim d As New StreamReader(fs)

            d.BaseStream.Seek(0, SeekOrigin.Begin)
            If d.Peek() > 0 Then
                While d.Peek() > -1
                    i = d.Peek
                    ftext = d.ReadLine()
                    iInitArr = ftext
                    If l = 0 Then
                        txtFolder.Text = iInitArr
                    End If
                    l = l + 1
                End While
                d.Close()
            End If
            d.Close()

        Catch ex As Exception

        End Try
    End Sub

End Class

