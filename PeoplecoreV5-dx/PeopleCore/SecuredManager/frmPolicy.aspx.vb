Imports clsLib
Imports System.IO
Imports System.Data

Partial Class SecuredManager_frmPolicy
    Inherits System.Web.UI.Page
    Dim ComponentNo As Integer
    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticatedSuperior()
        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        Dim Folder As String = FileInfo.Directory.Name

        Select Case Folder.ToLower()
            Case "secured"
                ComponentNo = 1
            Case "securedmanager"
                ComponentNo = 2
            Case "securedself"
                ComponentNo = 3
        End Select

        If Not IsPostBack Then
            PopulateGrid()
        End If

        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

    End Sub


    Protected Sub PopulateGrid()
        Try
            rDoc.DataSource = SQLHelper.ExecuteDataTable("EDoc_WebOnePolicy", UserNo, PayLocNo, ComponentNo, Filter1.SearchText)
            rDoc.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnk_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            Dim doc As Byte() = Nothing
            Dim filename As String = ""
            Dim orgname As String = ""
            Dim dt As DataTable
            lnk = sender
            Dim fContentType As String = ""
            Dim fileExt As String = ""
            Dim fullpath As String = ""
            'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)

            dt = SQLHelper.ExecuteDataTable("EDoc_WebOne", UserNo, Generic.ToInt(lnk.CommandArgument))
            For Each row As DataRow In dt.Rows
                filename = Generic.ToStr(row("ActualFileName"))
                orgname = Generic.ToStr(row("DocFile"))
                fContentType = Generic.ToStr(row("contenttype"))
                fileExt = Generic.ToStr(row("docExt"))
                fullpath = getFile_settings() ' Generic.ToStr(row("fullPath"))
            Next

            If Len(fullpath & "\" & filename.ToString) > 0 Then
                If fileExt.ToLower = ".docx" Then
                    fileExt = ".doc"
                ElseIf fileExt.ToLower = ".xlsx" Then
                    fileExt = ".xls"
                End If
                Response.Clear()
                Response.Buffer = True
                Response.AddHeader("Content-Disposition", "attachment; filename=tr" & fileExt)
                Response.ContentType = "application/octet-stream"
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.TransmitFile(fullpath & "\" & filename.ToString)
                Response.End()
            Else
                MessageBox.Warning("This file does not exist.", Me)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnk_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Private Function FilePath(DocNo As Integer) As String
        Dim filename As String
        filename = Generic.ToStr(SQLHelper.ExecuteScalar("SELECT ActualFileName FROM EDoc WHERE DocNo=" & DocNo.ToString()))
        Return getFile_settings() & filename
    End Function

    Private Function getFile_settings() As String
        Try


            'Dim iInitArr As String
            'Dim i As Integer
            'Dim fs As FileStream
            'Dim filename = HttpContext.Current.Server.MapPath("~/secured/connectionstr/") & "folder.ini"
            'Dim retval As String = ""


            'fs = New FileStream(filename, FileMode.Open, FileAccess.Read)
            'Dim l As Integer = 0, ftext As String = ""
            'Dim d As New StreamReader(fs)

            'd.BaseStream.Seek(0, SeekOrigin.Begin)
            'If d.Peek() > 0 Then
            '    While d.Peek() > -1
            '        i = d.Peek
            '        ftext = d.ReadLine()
            '        iInitArr = ftext
            '        If l = 0 Then
            '            retval = iInitArr
            '        End If
            '        l = l + 1
            '    End While
            '    d.Close()
            'End If
            'd.Close()
            Dim retval As String = ""
            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet("EDocFolder_WebOne")
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    retval = Generic.ToStr(ds.Tables(0).Rows(0)("path"))
                End If
            End If

            Return retval
        Catch ex As Exception

        End Try
    End Function
End Class
