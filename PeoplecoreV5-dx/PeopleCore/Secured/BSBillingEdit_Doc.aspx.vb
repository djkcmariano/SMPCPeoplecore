Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO

Partial Class Secured_BSBillingEdit_Doc
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("BBSDoc_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("BBSDoc_WebOne", UserNo, id)
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
            'PopulateTabHeader()
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
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSDocNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        Dim chk As New CheckBox, lnk As New LinkButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then

            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BSDocNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Dim path As String = DeleteFile(Generic.ToInt(lnk.CommandArgument))
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                If file.Exists Then
                    file.Delete()
                End If
                Generic.DeleteRecordAudit("BBSDoc", UserNo, item)
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
                    Dim fs As IO.Stream = fuDoc.PostedFile.InputStream
                    Dim br As New BinaryReader(fs)
                    Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                    FileSize = fs.Length

                    If SQLHelper.ExecuteNonQuery("BBSDoc_WebSave", UserNo, Generic.ToInt(txtCode.Text), TransNo, txtDocDesc.Text.ToString, Filename, FileExt, FileSize, txtDetails.Text.ToString, bytes, fileTypeNo, contenttype) > 0 Then
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

    'Private Sub PopulateTabHeader()
    '    Dim dt As DataTable
    '    dt = SQLHelper.ExecuteDataTable("EProjectTabHeader", UserNo, TransNo)
    '    For Each row As DataRow In dt.Rows
    '        lbl.Text = Generic.ToStr(row("Display"))
    '    Next
    '    imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
    'End Sub

    Private Function DeleteFile(DocNo As Integer) As String
        Dim filename As String
        filename = Generic.ToStr(SQLHelper.ExecuteScalar("SELECT PhysicalPath FROM BBSDoc WHERE BSDocNo=" & DocNo.ToString()))
        Return Server.MapPath("~/secured/documents/") & filename
    End Function

    Protected Sub lnkDownload_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Protected Sub lnkDownload_Click(sender As Object, e As EventArgs)
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

            dt = SQLHelper.ExecuteDataTable("BBSDoc_WebOne", UserNo, Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSDocNo"})))
            For Each row As DataRow In dt.Rows
                filename = Generic.ToStr(row("DocFile"))
                orgname = Generic.ToStr(row("DocFile"))
                datafile = row("DataFile")
                fContentType = Generic.ToStr(row("contenttype"))
                fileExt = Generic.ToStr(row("DocExt"))
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

