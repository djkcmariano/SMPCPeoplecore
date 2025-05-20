Imports System.Data
Imports clsLib

Partial Class Secured_EmpFBList
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim employeeno As Integer = 0

    Dim showFrm As New clsFormControls
    Dim xScript As String
    Dim _dt As New DataTable
    Dim IsClickMain As Integer = 0
    Dim fbCategoryNo As Integer
    Dim fbClassificationNo As Integer
    Dim clsGeneric As New clsGenericClass

    Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Try
            Dim dscount As Double = 0
            Dim _ds As DataSet
            _ds = sqlHelper.ExecuteDataset("EFB_Web", xPublicVar.xOnlineUseNo, fbCategoryNo, fbClassificationNo, Generic.CheckDBNull(Viewstate(xScript & "filter"), clsBase.clsBaseLibrary.enumObjectType.StrType), Session("xPayLocNo"))
            _dt = _ds.Tables(0)
            Dim dv As New Data.DataView(_dt)
            If SortExp <> "" Then
                Viewstate(xScript & "SortExp") = SortExp
            End If
            If sordir <> "" Then

                Viewstate(xScript & "sortdir") = sordir
            End If
            If _ds.Tables.Count > 0 Then
                If _ds.Tables(0).Rows.Count > 0 Then
                    dscount = _ds.Tables(0).Rows.Count
                    If Viewstate(xScript & "SortExp") <> "" Then
                        dv.Sort = Viewstate(xScript & "SortExp") + Viewstate(xScript & "sortdir")
                    End If
                End If
            End If
            If IsMain Then
                ViewState(xScript & "PageNo") = 0
                Session(Left(xScript, Len(xScript) - 5)) = 0
            End If

            Me.grdMain.SelectedIndex = Generic.CheckDBNull(Session(Left(xScript, Len(xScript) - 5)), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
            Me.grdMain.PageIndex = ViewState(xScript & "PageNo")
            Me.grdMain.DataSource = dv
            Me.grdMain.DataBind()

        Catch ex As Exception
        End Try
    End Sub
    Private Sub populatedata(tNo As Integer)
        Dim _ds As New DataSet
        _ds = sqlHelper.ExecuteDataset("EFB_WebOne", xPublicVar.xOnlineUseNo, tNo)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                showFrm.showFormControls_In_Popup(pnlPopupDetl, _ds)
            End If
        End If
        If tNo > 0 Then
            Me.txtFBNo.Text = Pad.PadZero(8, tNo)
        Else
            Me.txtFBNo.Text = "Autonumber"
        End If

    End Sub

    Private Sub populatedropdown()
        Try
            showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupDetl, Session("xPayLocNo"))
        Catch ex As Exception

        End Try


    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("onlineuserno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        AccessRights.CheckUser(xPublicVar.xOnlineUseNo)
        AddHandler Filter1.lnkSearchClick, AddressOf lnkGo_Click
        fbCategoryNo = Generic.CheckDBNull(Request.QueryString("categoryNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        fbClassificationNo = Generic.CheckDBNull(Request.QueryString("classificationNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
       
        xScript = Request.ServerVariables("SCRIPT_NAME")
        xScript = Generic.GetPath(xScript)

       

        If Not IsPostBack Then
            populatedropdown()
            PopulateGrid()

        End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub
    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Viewstate(xScript & "PageNo") = 0
            Session(Left(xScript, Len(xScript) - 5)) = 0
            Viewstate(xScript & "filter") = Filter1.SearchText.ToString
            PopulateGrid()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMain.PageIndexChanging
        Viewstate(xScript & "No") = 0
        Viewstate(xScript & "PageNo") = e.NewPageIndex
        Session(Left(xScript, Len(xScript) - 5)) = 0
        PopulateGrid()
    End Sub
    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lnk As New ImageButton
            Dim i As String = "", rowNo As Integer = 0

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            rowNo = gvrow.RowIndex
            Session(Left(xScript, Len(xScript) - 5)) = rowNo
            i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()

            If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit) Then
                populatedata(i)
                If fbClassificationNo = 3 Then
                    cboFBTypeNo.Text = 3
                ElseIf fbClassificationNo = 2 Then
                    cboFBTypeNo.Text = 5
                End If
                populatedropdown()
                showFrm.EnableControls_in_Popup(pnlPopupDetl, True)
                mdlDetl.Show()
            Else
                MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowAdd) Then
            showFrm.clearFormControls_In_Popup(pnlPopupDetl)
            If fbClassificationNo = 3 Then
                cboFBTypeNo.Text = 3
            ElseIf fbClassificationNo = 2 Then
                cboFBTypeNo.Text = 5
            End If
            populatedropdown()
            showFrm.EnableControls_in_Popup(pnlPopupDetl, True)
            cboFBTypeNo.Enabled = False
            btnSave.Enabled = True

            mdlDetl.Show()
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If
    End Sub
    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For tcount = 0 To Me.grdMain.Rows.Count - 1
                lbl = CType(grdMain.Rows(tcount).FindControl("lblId"), Label)
                tcheck = CType(grdMain.Rows(tcount).FindControl("txtIsSelect"), CheckBox)
                If tcheck.Checked = True Then
                    Generic.DeleteRecordAudit("EFB", xPublicVar.xOnlineUseNo, CType(lbl.Text, Integer))
                    DeleteCount = DeleteCount + 1
                End If
            Next
            MessageBox.Success("There are (" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If

    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub
    Private Function saverecord() As Boolean
        Dim lFile As FileUpload
        saverecord = False
        lFile = New FileUpload
        lFile = Me.FindControl("ctl00$cphBody$txtFile")
        If lFile Is Nothing Then
            lFile = Me.FindControl("ctl00_cphBody_txtFile")
        End If
        Dim rFBNo As Integer = Generic.CheckDBNull(txtFBNo.Text, clsBase.clsBaseLibrary.enumObjectType.IntType)
        If cboFBTypeNo.SelectedValue > "0" And Not lFile Is Nothing Then
            Dim xFile As String = "", DocId As Long = 0

            If Not lFile Is Nothing Then
                If lFile.HasFile Then
                    Dim MyPath As String, MyName As String, strFileName As String, c As String, pExt As String, pFile As String, pPos As Integer, fFile As String

                    MyPath = (Server.MapPath("documents")) 'Me.MapPath("documents") & "\
                    If Not IO.Directory.Exists(MyPath) Then
                        IO.Directory.CreateDirectory(MyPath)
                    End If
                    MyName = Dir(MyPath, vbDirectory)

                    strFileName = lFile.PostedFile.FileName
                    c = System.IO.Path.GetFileName(strFileName)
                    pExt = ""
                    fFile = strFileName
                    pPos = c.LastIndexOf(".")
                    If pPos > 0 Then
                        pExt = c.Substring(pPos + 1, c.Length - pPos - 1)
                        pPos = c.LastIndexOf("/")
                        fFile = c.Substring(pPos + 1, c.Length - pPos - 1)
                    End If
                    Dim _ds1 As Data.DataSet : _ds1 = sqlHelper.ExecuteDataset("EDoc_Save", xPublicVar.xOnlineUseNo, 0, fFile, strFileName, pExt, CType(lFile.PostedFile.ContentLength, Int64)) : If _ds1.Tables.Count > 0 Then : DocId = _ds1.Tables(0).Rows(0)(0) : pFile = "EFB" & _ds1.Tables(0).Rows(0)(0).ToString & "." & pExt : lFile.PostedFile.SaveAs(MyPath & pFile) : End If
                End If

                Dim _ds As Data.DataSet : _ds = sqlHelper.ExecuteDataset("EFB_Save", xPublicVar.xOnlineUseNo, rFBNo, Me.txtFBCode.Text.Trim, Me.txtFBDesc.Text.Trim, Me.cboFBTypeNo.SelectedValue, fbCategoryNo, fbClassificationNo, DocId, Session("xPayLocNo"))
                If _ds.Tables.Count > 0 And rFBNo = 0 Then
                    Dim grdList As String = Generic.CheckDBNull(Request.QueryString("grdList"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                    If grdList.Length > 5 Then : rFBNo = _ds.Tables(0).Rows(0)(0) : Session(Mid(grdList, 1, Len(grdList) - 5) & "RowNo") = _ds.Tables(0).Rows(0)(0) : Session(Mid(grdList, 1, Len(grdList) - 5) & "PageNo") = _ds.Tables(0).Rows(0)(1) : Session(Mid(grdList, 1, Len(grdList) - 5)) = _ds.Tables(0).Rows(0)(2) : Session(Mid(grdList, 1, Len(grdList) - 5) & "Str") = "" : End If
                End If
                saverecord = True
            End If

        Else
            If Me.cboFBTypeNo.SelectedValue = "0" Then
                Me.cboFBTypeNo.Focus()
            Else
                Me.txtFile.Focus()
            End If
        End If

    End Function
    Protected Sub lnkDocView_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lStr As String = ""
        Dim lLink As New LinkButton
        Dim MyPath As String = (Server.MapPath("documents"))

        'If clsPC.DocPath = "" Then
        '    clsPC.DocPath = Me.MapPath("~\Secured\documents") & "\"
        'End If

        lLink = sender

        lStr = MyPath & "EFB" & lLink.CommandName.ToString.Trim & "." & lLink.CommandArgument.ToString.Trim

        'previewFile(lStr)

        If Not clsGenericClass.DownloadFile(lStr) Then
            Me.Page.ClientScript.RegisterStartupScript(e.GetType, "CustomPopup", "<script language='javascript'>alert(""" & "Sorry, the file no longer available." & """)</script>")
        End If

    End Sub
    Private Sub previewFile(ByVal fullpath As String)
        Dim FileName As String = ""
        FileName = IO.Path.GetFileName(fullpath)
        Response.Clear()
        Response.ClearContent()
        Response.ContentType = "application/octet-stream"
        Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
        Response.TransmitFile(fullpath)
        Response.End()
        
    End Sub

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
    Protected Sub addTrigger_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim btnNewDelete As LinkButton = TryCast(sender, LinkButton)

        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(btnNewDelete)
    End Sub
End Class



