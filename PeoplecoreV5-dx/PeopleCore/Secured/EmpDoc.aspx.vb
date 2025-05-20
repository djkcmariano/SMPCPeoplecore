Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO

Partial Class Secured_EmpDoc
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDoc_Web", UserNo, TransNo, "", Generic.ToStr(Session("xMenuType")))
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDoc_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))        
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        If Not IsPostBack Then            
            PopulateTabHeader()
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
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
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DocNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

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


        Dim retval As Integer = 0
        If fuDoc.HasFile Then
            Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String = ""
            Dim _ds As New DataSet, NewFileName As String = ""
            Dim fileTypeNo As Integer = Generic.ToInt(cboFileTypeNo.SelectedValue)
            Dim contenttype As String = "", filetypecode As String = ""
            Try



                Filename = IO.Path.GetFileName(fuDoc.PostedFile.FileName)
                FileExt = IO.Path.GetExtension(fuDoc.PostedFile.FileName)

                Dim dsf As DataSet = SQLHelper.ExecuteDataSet("EFileType_WebOne", UserNo, fileTypeNo)
                If dsf.Tables.Count > 0 Then
                    If dsf.Tables(0).Rows.Count > 0 Then
                        contenttype = Generic.ToStr(dsf.Tables(0).Rows(0)("contenttype"))
                        filetypecode = Generic.ToStr(dsf.Tables(0).Rows(0)("filetypecode"))
                    Else
                        Return 0
                        Exit Function
                    End If
                Else
                    Return 0
                    Exit Function
                End If
                If filetypecode.ToLower = FileExt.ToLower Then
                    Dim tfilename As String
                    Dim fs As IO.Stream = fuDoc.PostedFile.InputStream
                    Dim br As New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                    FileSize = fs.Length
                    ActualPath = getFile_settings()


                    Dim ds As DataSet = SQLHelper.ExecuteDataSet("EDoc_WebSave_File", UserNo, TransNo, Generic.ToInt(txtCode.Text), txtDocDesc.Text, Filename,
                                                FileExt, FileSize, ActualPath, Generic.ToStr(Session("xMenuType")), 1, fileTypeNo, contenttype, PayLocNo)
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(0).Rows.Count > 0 Then
                            tfilename = Generic.ToStr(ds.Tables(0).Rows(0)("tFilename"))
                            WriteFile(ActualPath & "\" & tfilename.ToString, bytes)
                        End If
                        Return 1
                    End If

                Else
                    Return 2
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
                fullpath = Generic.ToStr(row("fullPath"))
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
    Private Function SaveRecord_old() As Integer


        Dim retval As Integer = 0
        If fuDoc.HasFile Then
            Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String = ""
            Dim _ds As New DataSet, NewFileName As String = ""
            Dim fileTypeNo As Integer = Generic.ToInt(cboFileTypeNo.SelectedValue)
            Dim contenttype As String = "", filetypecode As String = ""
            Try



                Filename = IO.Path.GetFileName(fuDoc.PostedFile.FileName)
                FileExt = IO.Path.GetExtension(fuDoc.PostedFile.FileName)

                Dim dsf As DataSet = SQLHelper.ExecuteDataSet("EFileType_WebOne", UserNo, fileTypeNo)
                If dsf.Tables.Count > 0 Then
                    If dsf.Tables(0).Rows.Count > 0 Then
                        contenttype = Generic.ToStr(dsf.Tables(0).Rows(0)("contenttype"))
                        filetypecode = Generic.ToStr(dsf.Tables(0).Rows(0)("filetypecode"))
                    Else
                        Return 0
                        Exit Function
                    End If
                Else
                    Return 0
                    Exit Function
                End If
                If filetypecode.ToLower = FileExt.ToLower Then
                    Dim fs As IO.Stream = fuDoc.PostedFile.InputStream
                    Dim br As New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                    FileSize = fs.Length

                    If SQLHelper.ExecuteNonQuery("EDoc_WebSave", UserNo, TransNo, Generic.ToInt(txtCode.Text), txtDocDesc.Text, Filename,
                                                 FileExt, NewFileName & FileExt, FileSize, ActualPath, Generic.ToStr(Session("xMenuType")), 1, fileTypeNo, bytes, contenttype, PayLocNo) > 0 Then
                        Return 1
                    End If

                Else
                    Return 2
                End If

            Catch ex As Exception

            End Try
        End If
        Return retval

    End Function

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
    End Sub

    Private Function DeleteFile(DocNo As Integer) As String
        Dim filename As String
        filename = Generic.ToStr(SQLHelper.ExecuteScalar("SELECT ActualFileName FROM EDoc WHERE DocNo=" & DocNo.ToString()))
        Return getFile_settings() & "\" & filename
    End Function

    Protected Sub lnkDownload_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub


    Protected Sub lnkDownload_Click_old(sender As Object, e As EventArgs)
        Try

            Dim lnk As New LinkButton
            Dim doc As Byte() = Nothing
            Dim filename As String = ""
            Dim orgname As String = ""
            Dim dt As DataTable
            lnk = sender
            Dim datafile() As Byte = Nothing
            Dim fContentType As String = ""
            Dim fileExt As String = ""
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)

            dt = SQLHelper.ExecuteDataTable("EDoc_WebOne", UserNo, Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DocNo"})))
            For Each row As DataRow In dt.Rows
                filename = Generic.ToStr(row("ActualFileName"))
                orgname = Generic.ToStr(row("DocFile"))
                datafile = row("DataFile")
                fContentType = Generic.ToStr(row("contenttype"))
                fileExt = Generic.ToStr(row("docExt"))
            Next

            If Not datafile Is Nothing Then
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
                Response.BinaryWrite(datafile)
                Response.End()
            Else
                MessageBox.Warning("This file does not exist.", Me)
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
