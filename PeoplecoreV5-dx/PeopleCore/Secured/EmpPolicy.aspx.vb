Imports clsLib
Imports System.Data
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports System.IO
Imports System.Net

Partial Class Secured_EmpPolicy
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EDoc_WebPolicy", UserNo, PayLocNo, Generic.ToStr(Session("xMenuType")))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim status As Integer = SaveRecord()
        If status = 1 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        ElseIf status = 0 Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        ElseIf status = 2 Then
            MessageBox.Warning("Invalid file type.", Me)
        ElseIf status = 3 Then
            MessageBox.Warning("File size is over the limit.", Me)
        ElseIf status = 4 Then
            MessageBox.Alert("File server unavailable.", "warning", Me)
        ElseIf status = 5 Then
            MessageBox.Alert("Path not found.", "warning", Me)
        End If


    End Sub

    Private Sub UploadPic()
        Dim myWebClient As New WebClient()

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        ''Dim chk As New CheckBox, lnk As New LinkButton, Count As Integer = 0
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
        '    Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DocNo"})
        '    Dim str As String = "", i As Integer = 0
        '    Dim path As String = ConfigurationManager.AppSettings("drive_path")
        '    For Each item As Integer In fieldValues
        '        Dim dt As DataTable = SQLHelper.ExecuteDataTable("EDoc_WebOne", UserNo, item)
        '        If dt.Rows.Count > 0 Then
        '            Dim filename As String = ""
        '            For Each dRow As DataRow In dt.Rows
        '                filename = Generic.ToStr(dRow("ActualFileName"))
        '            Next
        '            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & filename.ToString)
        '            If file.Exists Then
        '                file.Delete()
        '            End If
        '            Generic.DeleteRecordAudit("EDoc", UserNo, item)
        '            i = i + 1
        '        End If
        '    Next

        '    If i > 0 Then
        '        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        '        PopulateGrid()
        '    Else
        '        MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        '    End If
        'Else
        '    MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        'End If
        Dim chk As New CheckBox, lnk As New LinkButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then

            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DocNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Dim path As String = DeleteFile(Generic.ToInt(item))
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                If file.Exists Then
                    file.Delete()
                End If

                Generic.DeleteRecordAudit("EDoc", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Function SaveRecord() As Integer
        
        'Dim retval As Boolean = False
        'If fuDoc.HasFile Then
        '    Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String = ""
        '    Dim _ds As New DataSet, NewFileName As String = ""
        '    Dim FileTypeNo As Integer = Generic.ToInt(cboFileTypeNo.SelectedValue)
        '    Dim contenttype As String = "", filetypecode As String = ""
        '    Dim TmpFilename As String = Guid.NewGuid.ToString()
        '    Dim Fullpath As String = ""
        '    Filename = IO.Path.GetFileName(fuDoc.PostedFile.FileName)
        '    FileExt = IO.Path.GetExtension(fuDoc.PostedFile.FileName)
        '    ActualPath = ConfigurationManager.AppSettings("drive_path")
        '    Dim filesize_limit As Integer = Generic.ToStr(ConfigurationManager.AppSettings("filesize"))
        '    Dim IsValidExt As Boolean = Generic.ToBol(SQLHelper.ExecuteScalar("EFileType_WebCheck", UserNo, FileExt, Generic.ToStr(Session("xMenuType"))))
        '    If IsValidExt Then
        '        'If FileExt.ToLower = ".jpg" Or FileExt.ToLower = ".png" Or FileExt.ToLower = ".pdf" Or FileExt.ToLower = ".gif" Then
        '        If IsPathExists(ActualPath) = False Then : Return 4 : End If
        '        If fuDoc.PostedFile.ContentLength <= filesize_limit Then
        '            Dim dt As DataTable
        '            ActualPath = ConfigurationManager.AppSettings("drive_path")
        '            dt = SQLHelper.ExecuteDataTable("EDoc_WebSave_File", UserNo, 0, 0, IO.Path.GetFileNameWithoutExtension(Filename), Filename, FileExt, FileSize, ActualPath, Generic.ToStr(Session("xMenuType")), 1, 0, "", 0)
        '            For Each row As DataRow In dt.Rows
        '                NewFileName = Generic.ToStr(row("tFilename"))
        '            Next
        '            'Dim file As System.IO.FileInfo = New System.IO.FileInfo(Fullpath)
        '            'If file.Exists Then
        '            fuDoc.PostedFile.SaveAs(ActualPath & NewFileName)
        '            'End If
        '            Return 1
        '        Else
        '            MessageBox.Warning("file size is over the limit", Me)
        '        End If
        '    Else
        '        Return 2
        '    End If
        'End If
        'Return retval

        Dim retval As Integer = 0
        If fuDoc.HasFile Then
            Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String = ""
            Dim _ds As New DataSet, NewFileName As String = ""
            Dim fileTypeNo As Integer = Generic.ToInt(cboFileTypeNo.SelectedValue)
            Dim contenttype As String = "", filetypecode As String = ""
            Try



                Filename = IO.Path.GetFileName(fuDoc.PostedFile.FileName)
                FileExt = IO.Path.GetExtension(fuDoc.PostedFile.FileName)

                'Dim dsf As DataSet = SQLHelper.ExecuteDataSet("EFileType_WebOne", UserNo, fileTypeNo)
                'If dsf.Tables.Count > 0 Then
                '    If dsf.Tables(0).Rows.Count > 0 Then
                '        contenttype = Generic.ToStr(dsf.Tables(0).Rows(0)("contenttype"))
                '        filetypecode = Generic.ToStr(dsf.Tables(0).Rows(0)("filetypecode"))
                '    Else
                '        Return 0
                '        Exit Function
                '    End If
                'Else
                '    Return 0
                '    Exit Function
                'End If
                'If filetypecode.ToLower = FileExt.ToLower Then
                Dim tfilename As String
                Dim fs As IO.Stream = fuDoc.PostedFile.InputStream
                Dim br As New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                FileSize = fs.Length
                ActualPath = getFile_settings()

                If ActualPath.Length>0
                    Dim ds As DataSet = SQLHelper.ExecuteDataSet("EDoc_WebSave_File", UserNo, 0, Generic.ToInt(txtCode.Text), txtDocDesc.Text, Filename,
                                                FileExt, FileSize, ActualPath, Generic.ToStr(Session("xMenuType")), Generic.ToInt(cboComponentNo.SelectedValue), fileTypeNo, contenttype, PayLocNo)
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            tfilename = Generic.ToStr(ds.Tables(0).Rows(0)("tFilename"))
                            WriteFile(ActualPath & "\" & tfilename.ToString, bytes)
                        End If
                        Return 1
                    End If

                Else
                    Return 5
                End If

            Catch ex As Exception

            End Try
        End If
        Return retval
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
    Private Sub WriteFile(strPath As String, Buffer As Byte())
        'Create a file
        Dim newFile As FileStream = New FileStream(strPath, FileMode.Create)

        'Write data to the file
        newFile.Write(Buffer, 0, Buffer.Length)

        'Close file
        newFile.Close()
    End Sub
    Private Function DeleteFile(DocNo As Integer) As String
        Dim filename As String
        filename = Generic.ToStr(SQLHelper.ExecuteScalar("SELECT ActualFileName FROM EDoc WHERE DocNo=" & DocNo.ToString()))
        Return getFile_settings() & "\" & filename
    End Function
    Protected Sub lnkDownload_Click(sender As Object, e As EventArgs)
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
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)

            dt = SQLHelper.ExecuteDataTable("EDoc_WebOne", UserNo, Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DocNo"})))
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
    'Protected Sub lnkDownload_Click(sender As Object, e As EventArgs)
    '    'Try

    '    '    Dim lnk As New LinkButton
    '    '    Dim doc As Byte() = Nothing
    '    '    Dim filename As String = ""
    '    '    Dim orgname As String = ""
    '    '    Dim dt As DataTable
    '    '    lnk = sender
    '    '    Dim fContentType As String = ""
    '    '    Dim fileExt As String = ""
    '    '    Dim fullpath As String = ""
    '    '    Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)

    '    '    dt = SQLHelper.ExecuteDataTable("EDoc_WebOne", UserNo, Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DocNo"})))
    '    '    For Each row As DataRow In dt.Rows
    '    '        filename = Generic.ToStr(row("ActualFileName"))
    '    '        orgname = Generic.ToStr(row("DocFile"))
    '    '        fContentType = Generic.ToStr(row("contenttype"))
    '    '        fileExt = Generic.ToStr(row("docExt"))
    '    '        fullpath = Generic.ToStr(row("fullPath"))
    '    '    Next

    '    '    Dim path As String = FilePath(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DocNo"})))
    '    '    Dim file As System.IO.FileInfo = New System.IO.FileInfo(Server.MapPath(path))
    '    '    If file.Exists Then
    '    '        Response.Clear()
    '    '        Response.Buffer = True
    '    '        Response.AddHeader("Content-Disposition", "attachment; filename=" & orgname)
    '    '        Response.ContentType = "application/octet-stream"
    '    '        Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '    '        Response.TransmitFile(fullpath & "\" & filename.ToString)
    '    '        Response.End()
    '    '    Else
    '    '        MessageBox.Warning("This file does not exist.", Me)
    '    '    End If            
    '    'Catch ex As Exception

    '    'End Try
    '    Try
    '        Dim lnk As New LinkButton
    '        Dim doc As Byte() = Nothing
    '        Dim filename As String = ""
    '        Dim orgname As String = ""
    '        Dim dt As DataSet
    '        Dim datafile() As Byte = Nothing
    '        Dim fContentType As String = ""
    '        Dim fileExt As String = ""
    '        Dim DocNo As Integer = 0
    '        Dim RowNo As Integer = 0
    '        Dim path As String = ConfigurationManager.AppSettings("drive_path")
    '        lnk = sender
    '        DocNo = lnk.CommandArgument
    '        dt = SQLHelper.ExecuteDataSet("EDoc_WebOne", UserNo, DocNo)
    '        If dt.Tables.Count > 0 Then
    '            If dt.Tables(0).Rows.Count > 0 Then
    '                filename = Generic.ToStr(dt.Tables(0).Rows(0)("ActualFileName"))
    '                orgname = Generic.ToStr(dt.Tables(0).Rows(0)("docfile"))
    '                'datafile = dt.Tables(0).Rows(0)("datafile")
    '                fContentType = Generic.ToStr(dt.Tables(0).Rows(0)("contenttype"))
    '                fileExt = Generic.ToStr(dt.Tables(0).Rows(0)("docext"))
    '            End If
    '        End If
    '        Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & filename.ToString)
    '        If file.Exists Then
    '            Response.Clear()
    '            Response.Buffer = True
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" & orgname)
    '            Response.ContentType = "application/octet-stream"
    '            Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '            'Response.BinaryWrite(datafile)
    '            Response.TransmitFile(path & filename.ToString)
    '            Response.End()
    '        Else
    '            MessageBox.Information("File not available.", Me)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Warning("Error downloading file.", Me)
    '    End Try
    'End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkOpenFile_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Private Function IsPathExists(path As String) As Boolean
        Dim retVal As Boolean = False
        If Generic.ToStr(ConfigurationManager.AppSettings("share_drive")).ToLower() = "yes" Then
            If Directory.Exists(path) Then
                Return True
            End If
        Else
            Return True
        End If
        Return retVal
    End Function

End Class
