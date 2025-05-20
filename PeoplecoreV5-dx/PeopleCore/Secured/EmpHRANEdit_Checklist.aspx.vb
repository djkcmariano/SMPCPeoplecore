Imports clsLib
Imports System.Data
Imports DevExpress.Web
Imports System.IO

Partial Class Secured_EmpHRANEdit_Checklist
    Inherits System.Web.UI.Page
    'Dim UserNo As Int64 = 0
    'Dim PayLocNo As Int64 = 0
    'Dim TransNo As Int64 = 0


    'Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    '    UserNo = Generic.ToInt(Session("OnlineUserNo"))
    '    PayLocNo = Generic.ToInt(Session("xPayLocNo"))
    '    TransNo = Generic.ToInt(Request.QueryString("id"))
    '    If Not IsPostBack Then
    '        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
    '        PopulateGrid()
    '        PopulateTabHeader()
    '        EnabledControls()
    '    End If
    '    Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    'End Sub

    'Protected Sub PopulateGrid()
    '    Try
    '        Dim dt As DataTable
    '        Dim sortDirection As String = "", sortExpression As String = ""
    '        dt = SQLHelper.ExecuteDataTable("EHRANCheckList_Web", UserNo, TransNo)
    '        Dim dv As DataView = dt.DefaultView
    '        If ViewState("SortDirection") IsNot Nothing Then
    '            sortDirection = ViewState("SortDirection").ToString()
    '        End If
    '        If ViewState("SortExpression") IsNot Nothing Then
    '            sortExpression = ViewState("SortExpression").ToString()
    '            dv.Sort = String.Concat(sortExpression, " ", sortDirection)
    '        End If
    '        grdMain.DataSource = dv
    '        grdMain.DataBind()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub PopulateData(id As Int64)
    '    Try
    '        Dim dt As DataTable
    '        dt = SQLHelper.ExecuteDataTable("EHRANCheckList_WebOne", UserNo, id)
    '        For Each row As DataRow In dt.Rows
    '            Generic.PopulateData(Me, "Panel1", dt)
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub grdMain_Sorting(sender As Object, e As GridViewSortEventArgs)
    '    Try
    '        If ViewState("SortDirection") Is Nothing OrElse ViewState("SortExpression").ToString() <> e.SortExpression Then
    '            ViewState("SortDirection") = "ASC"
    '        ElseIf ViewState("SortDirection").ToString() = "ASC" Then
    '            ViewState("SortDirection") = "DESC"
    '        ElseIf ViewState("SortDirection").ToString() = "DESC" Then
    '            ViewState("SortDirection") = "ASC"
    '        End If
    '        ViewState("SortExpression") = e.SortExpression
    '        PopulateGrid()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub grdMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
    '    Try
    '        grdMain.PageIndex = e.NewPageIndex
    '        PopulateGrid()
    '    Catch ex As Exception

    '    End Try
    'End Sub


    'Private Sub EnabledControls()
    '    Dim Enabled As Boolean = True

    '    If txtIsPosted.Checked = True Then
    '        Enabled = False
    '    End If

    '    Generic.EnableControls(Me, "Panel1", Enabled)

    '    btnUpdate.Visible = Enabled
    '    btnAdd.Visible = False 'Enabled
    '    btnDelete.Visible = Enabled
    '    lnkSave.Visible = Enabled
    'End Sub

    'Private Sub PopulateTabHeader()
    '    Try
    '        Dim dt As DataTable
    '        dt = SQLHelper.ExecuteDataTable("EHRAN_WebTabHeader", UserNo, TransNo)
    '        For Each row As DataRow In dt.Rows
    '            lbl.Text = Generic.ToStr(row("Display"))
    '            txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '        Generic.ClearControls(Me, "Panel1")
    '        ModalPopupExtender1.Show()
    '    Else
    '        MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
    '    End If

    'End Sub

    'Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

    '    If SaveRecord() Then
    '        PopulateGrid()
    '        MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '    Else
    '        MessageBox.Critical(MessageTemplate.ErrorSave, Me)
    '    End If

    'End Sub

    'Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
    '        Dim ib As New LinkButton
    '        ib = sender
    '        PopulateData(Generic.ToInt(ib.CommandArgument))
    '        ModalPopupExtender1.Show()
    '    Else
    '        MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
    '    End If
    'End Sub

    'Protected Sub btnDelete_Click(sender As Object, e As EventArgs)
    '    Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
    '        For i As Integer = 0 To Me.grdMain.Rows.Count - 1
    '            chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
    '            ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
    '            If chk.Checked = True Then
    '                Generic.DeleteRecordAudit("EHRANCheckListType", UserNo, Generic.ToInt(ib.CommandArgument))
    '                Count = Count + 1
    '            End If
    '        Next
    '        PopulateGrid()
    '        MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
    '    Else
    '        MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
    '    End If
    'End Sub

    'Private Function SaveRecord() As Integer
    '    Dim retval As Boolean = False
    '    If fuDoc.HasFile Then
    '        Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String
    '        Dim _ds As New DataSet, NewFileName As String
    '        Try
    '            Dim fpath As String
    '            Filename = IO.Path.GetFileName(fuDoc.PostedFile.FileName)
    '            FileExt = IO.Path.GetExtension(fuDoc.PostedFile.FileName)
    '            Dim f As New System.IO.FileInfo(fuDoc.PostedFile.FileName)
    '            FileSize = f.Length
    '            NewFileName = Guid.NewGuid().ToString()
    '            ActualPath = Server.MapPath("../") & "secured\documents\" & NewFileName & FileExt
    '            fpath = "../secured/documents/" & NewFileName & FileExt
    '            fuDoc.SaveAs(ActualPath)
    '            If SQLHelper.ExecuteNonQuery("EHRANCheckList_WebSave_Doc", UserNo, Generic.ToInt(txttno.Text), fpath, ActualPath) > 0 Then
    '                retval = True
    '            End If
    '        Catch ex As Exception

    '        End Try
    '    End If
    '    Return retval

    'End Function
    'Protected Sub lnkDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try


    '        Dim lnk As New LinkButton, img As New Image, lbl As New Label
    '        lnk = sender

    '        If DownloadFile(lnk.ToolTip) = False Then
    '            MessageBox.Information("File is no longer available.", Me)
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Critical("Error.", Me)
    '    End Try
    'End Sub
    'Private Function DownloadFile(ByVal FilePath As String) As Boolean

    '    'If IO.File.Exists(Server.MapPath(FilePath)) Then
    '    '    Dim FileName As String = ""
    '    '    FileName = IO.Path.GetFileNameWithoutExtension(FilePath).ToString.PadLeft(8, "0") & IO.Path.GetExtension(FilePath)
    '    '    Response.Clear()
    '    '    Response.ClearContent()
    '    '    Response.ContentType = "application/octet-stream"
    '    '    Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
    '    '    Response.TransmitFile(FilePath)
    '    '    Response.End()
    '    '    Return True
    '    'Else
    '    '    Return False
    '    'End If
    '    If IO.File.Exists(Server.MapPath(FilePath)) Then
    '        Dim FileName As String = ""
    '        FileName = IO.Path.GetFileName(FilePath)
    '        Response.Clear()
    '        Response.ClearContent()
    '        Response.ContentType = "application/octet-stream"
    '        Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
    '        Response.TransmitFile(FilePath)
    '        Response.End()
    '    Else
    '        Return False
    '    End If
    'End Function
    ''Submit record
    'Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim lbl As New Label, chkIsSubmitted As New CheckBox
    '    Dim tcount As Integer, SaveCount As Integer = 0
    '    Dim xds As New DataSet

    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then

    '        For tcount = 0 To Me.grdMain.Rows.Count - 1
    '            lbl = CType(grdMain.Rows(tcount).FindControl("lblID"), Label)
    '            chkIsSubmitted = CType(grdMain.Rows(tcount).FindControl("txtIsSubmitted"), CheckBox)

    '            Dim tno As Integer = Generic.ToInt(lbl.Text)
    '            Dim IsSubmitted As Boolean = Generic.ToBol(chkIsSubmitted.Checked)

    '            If Not chkIsSubmitted Is Nothing Then
    '                If SQLHelper.ExecuteNonQuery("EHRANCheckList_WebSave", UserNo, tno, TransNo, "", "", IsSubmitted) > 0 Then
    '                    SaveCount = SaveCount + 1
    '                End If
    '            End If

    '        Next

    '        PopulateGrid()
    '        MessageBox.Success(MessageTemplate.SuccessSave, Me)

    '    Else
    '        MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
    '    End If

    'End Sub
    'Protected Sub addTrigger_PreRender(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim btnPreview As LinkButton = TryCast(sender, LinkButton)
    '    Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
    '    NewScriptManager.RegisterPostBackControl(btnPreview)
    'End Sub


    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRANCheckList_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
            ViewState("IsEnabled") = Generic.ToBol(dt.Rows(0)("IsEnabled"))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "EmpHRANList.aspx", "EHRAN")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateTabHeader()
        End If
        PopulateGrid()
        EnabledControls()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub


    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRANCheckList_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRAN_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub EnabledControls()
        Dim Enabled As Boolean = True

        If txtIsPosted.Checked = True Then
            Enabled = False
        End If

        Generic.EnableControls(Me, "Panel1", Enabled)

        lnkUpdate.Visible = True
        lnkUpdate.Enabled = ViewState("IsEnabled")
        lnkAdd.Visible = False
        lnkDelete.Visible = False
        lnkSave.Visible = Enabled
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "EmpHRANList.aspx", "EHRAN") Then
            Generic.ClearControls(Me, "Panel1")
            Generic.EnableControls(Me, "Panel1", True)
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpHRANList.aspx", "EHRAN") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"HRANChecklistNo"})))
            'Generic.EnableControls(Me, "Panel1", False)
            'fuDoc.Enabled = True
            'ModalPopupExtender1.Show()
            'Response.Redirect("~/secured/frmFileUpload.aspx?id=" & Generic.Split(lnk.CommandArgument, 0) & "&display=" & Generic.Split(lnk.CommandArgument, 1))
            FileUpload.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
            FileUpload.xModify = Generic.ToBol(ViewState("IsEnabled"))
            FileUpload.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "EmpHRANList.aspx", "EHRAN") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"HRANChecklistNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EHRANCheckListType", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Function SaveRecord() As Integer
        Dim retval As Boolean = False
        If fuDoc.HasFile Then
            Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String
            Dim _ds As New DataSet, NewFileName As String
            Try
                'Dim fpath As String
                'Filename = IO.Path.GetFileName(fuDoc.PostedFile.FileName)
                'FileExt = IO.Path.GetExtension(fuDoc.PostedFile.FileName)
                'Dim f As New System.IO.FileInfo(fuDoc.PostedFile.FileName)
                'FileSize = f.Length
                'NewFileName = Guid.NewGuid().ToString()
                'ActualPath = Server.MapPath("../") & "secured\documents\" & NewFileName & FileExt
                'fpath = "../secured/documents/" & NewFileName & FileExt
                'fuDoc.SaveAs(ActualPath)
                'If SQLHelper.ExecuteNonQuery("EHRANCheckList_WebSave_Doc", UserNo, Generic.ToInt(txtHRANChecklistNo.Text), fpath, ActualPath) > 0 Then
                '    retval = True
                'End If

                Dim tfilename As String
                Dim fs As IO.Stream = fuDoc.PostedFile.InputStream
                Dim br As New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                FileSize = fs.Length
                ActualPath = getFile_settings()
                Filename = IO.Path.GetFileName(fuDoc.PostedFile.FileName)
                FileExt = IO.Path.GetExtension(fuDoc.PostedFile.FileName)

                Dim docNo As Integer = 0
                Dim ds As DataSet = SQLHelper.ExecuteDataSet("EDoc_WebSave_File", UserNo, TransNo, Generic.ToInt(txtCode.Text), "HRAN Checklist", Filename,
                                            FileExt, FileSize, ActualPath, Generic.ToStr(Session("xMenuType")), 1, 0, ContentType, PayLocNo)
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        tfilename = Generic.ToStr(ds.Tables(0).Rows(0)("tFilename"))
                        docNo = Generic.ToInt(ds.Tables(0).Rows(0)("docNo"))
                        WriteFile(ActualPath & "\" & tfilename.ToString, bytes)

                        If SQLHelper.ExecuteNonQuery("EHRANCheckList_WebSave_Doc", UserNo, Generic.ToInt(txtHRANChecklistNo.Text), Filename, ActualPath, docNo) > 0 Then

                        End If

                    End If
                    Return 1
                End If

            Catch ex As Exception

            End Try
        End If
        Return retval

    End Function
    Private Function getFile_settings() As String
        Try


            Dim iInitArr As String
            Dim i As Integer
            Dim fs As FileStream
            Dim filename = HttpContext.Current.Server.MapPath("~/secured/connectionstr/") & "folder.ini"
            Dim retval As String = ""

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
                        retval = iInitArr
                    End If
                    l = l + 1
                End While
                d.Close()
            End If
            d.Close()
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
    Protected Sub lnkDownloadx_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As New LinkButton, img As New Image, lbl As New Label
            lnk = sender

            If DownloadFile(lnk.CommandArgument) = False Then
                MessageBox.Information("File is no longer available.", Me)
            End If
        Catch ex As Exception
            MessageBox.Critical("Error.", Me)
        End Try
    End Sub
    Private Function DownloadFile(FilePath As String) As Boolean

        'If IO.File.Exists(Server.MapPath(FilePath)) Then
        '    Dim FileName As String = ""
        '    FileName = IO.Path.GetFileNameWithoutExtension(FilePath).ToString.PadLeft(8, "0") & IO.Path.GetExtension(FilePath)
        '    Response.Clear()
        '    Response.ClearContent()
        '    Response.ContentType = "application/octet-stream"
        '    Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
        '    Response.TransmitFile(FilePath)
        '    Response.End()
        '    Return True
        'Else
        '    Return False
        'End If

        If IO.File.Exists(Server.MapPath(FilePath)) Then
            Dim FileName As String = ""
            FileName = IO.Path.GetFileName(FilePath)
            Response.Clear()
            Response.ClearContent()
            Response.ContentType = "application/octet-stream"
            Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
            Response.TransmitFile(FilePath)
            Response.End()
            Return True
        Else
            Return False
        End If

    End Function

    Protected Sub addTrigger_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim btnPreview As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(btnPreview)
    End Sub

    Protected Sub lnkUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "EmpHRANList.aspx", "EHRAN") Then

            Dim tno As Integer = 0, i As Integer = 0
            Dim IsSelected As Boolean

            For j As Integer = 0 To grdMain.VisibleRowCount - 1
                tno = Generic.ToInt(grdMain.GetRowValues(j, "HRANChecklistNo"))
                If grdMain.Selection.IsRowSelected(j) Then
                    IsSelected = True
                Else
                    IsSelected = False
                End If
                SQLHelper.ExecuteNonQuery("EHRANCheckList_WebSave", UserNo, tno, TransNo, "", "", IsSelected)
                i = i + 1
            Next


            'MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessSave, Me)
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub grdMain_PreRender(sender As Object, e As System.EventArgs) Handles grdMain.PreRender
        Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)
        For i As Integer = 0 To grid.VisibleRowCount - 1
            Dim isSelected As Boolean = Convert.ToBoolean(grdMain.GetRowValues(i, "IsSubmitted"))
            If isSelected Then
                grid.Selection.SelectRow(i)
            End If
        Next i
    End Sub

End Class
