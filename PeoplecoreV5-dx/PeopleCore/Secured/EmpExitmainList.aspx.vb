Imports System.Data
Imports clsLib

Partial Class Secured_EmpExitmainList
    Inherits System.Web.UI.Page


    Dim xPublicVar As New clsPublicVariable
    Dim tCount As Integer = 0
    Dim dsCount As Integer = 0, dsCountd As Integer = 0
    Dim transNo As Integer = 0
    Dim ApplicantStandardno As Integer = 0
    Dim _dt As New DataTable
    Dim RowNo As Integer = 0
    Dim RowNo2 As Integer = 0

    Dim xScript As String = ""
    Dim showFrm As New clsFormControls

    Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _ds As New DataSet

        _ds = SQLHelper.ExecuteDataSet("EApplicantStandardMain_WebExit", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(Filter1.SearchText.ToString, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Session("xPayLocNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
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
                dsCount = _ds.Tables(0).Rows.Count

                If Viewstate(xScript & "SortExp") <> "" Then
                    dv.Sort = Viewstate(xScript & "SortExp") + Viewstate(xScript & "sortdir")
                End If
            End If
        End If

        If IsMain Then
            ViewState(xScript & "PageNo") = 0
            ViewState(Left(xScript, Len(xScript) - 5)) = 0
        End If

        Me.grdMain.SelectedIndex = Generic.CheckDBNull(ViewState(Left(xScript, Len(xScript) - 5)), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Me.grdMain.PageIndex = ViewState(xScript & "PageNo")
        Me.grdMain.DataSource = dv
        Me.grdMain.DataBind()

        PopulateGridDetail()
    End Sub



    Private Sub PopulateGridDetail(Optional pageNo As Integer = 0)
        Dim _ds As New DataSet

        If Generic.CheckDBNull(Viewstate(xScript & "No"), clsBase.clsBaseLibrary.enumObjectType.IntType) = 0 And grdMain.Rows.Count > 0 Then
            Viewstate(xScript & "No") = grdMain.DataKeys(0).Values(0).ToString()
        End If
        _ds = sqlHelper.ExecuteDataset("EApplicantStandard_Web", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(Viewstate(xScript & "No"), clsBase.clsBaseLibrary.enumObjectType.IntType))
        dsCountd = _ds.Tables(0).Rows.Count

        Me.grdDetl.DataSource = _ds
        Me.grdDetl.DataBind()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("OnlineUserNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)


        transNo = Generic.CheckDBNull(Request.QueryString("transNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        AccessRights.CheckUser(xPublicVar.xOnlineUseNo)

        xScript = Request.ServerVariables("SCRIPT_NAME")
        xScript = Generic.GetPath(xScript)


        If Not IsPostBack Then
            PopulateGrid()
        End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMain.PageIndexChanging
        Viewstate(xScript & "PageNo") = e.NewPageIndex
        PopulateGrid()

    End Sub
    Protected Sub grdDetl_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDetl.PageIndexChanging
        Me.grdDetl.PageIndex = e.NewPageIndex
        PopulateGridDetail()

    End Sub
    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ViewState(xScript & "PageNo") = 0
        PopulateGrid()
        PopulateGridDetail()
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0

       If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For tcount = 0 To Me.grdMain.Rows.Count - 1
                lbl = CType(grdMain.Rows(tcount).FindControl("lblId"), Label)
                tcheck = CType(grdMain.Rows(tcount).FindControl("txtIsSelect"), CheckBox)
                If tcheck.Checked = True Then
                    Generic.DeleteRecordAudit("EApplicantStandardMain", xPublicVar.xOnlineUseNo, CType(lbl.Text, Integer))
                    Generic.DeleteRecordAuditCol("EApplicantStandardMain", xPublicVar.xOnlineUseNo, "ApplicantStandardMainNo", CType(lbl.Text, Integer))
                    DeleteCount = DeleteCount + 1
                End If
            Next
            MessageBox.Success("There are " & DeleteCount.ToString & MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)

        End If
    End Sub
    Protected Sub lnkDeleteD_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For tcount = 0 To Me.grdDetl.Rows.Count - 1
                lbl = CType(grdDetl.Rows(tcount).FindControl("lblIdd"), Label)
                tcheck = CType(grdDetl.Rows(tcount).FindControl("txtIsSelectd"), CheckBox)
                If tcheck.Checked = True Then
                    Generic.DeleteRecordAudit("EApplicantStandard", xPublicVar.xOnlineUseNo, CType(lbl.Text, Integer))
                    DeleteCount = DeleteCount + 1
                End If
            Next
            MessageBox.Success("There are " & DeleteCount.ToString & MessageTemplate.SuccessDelete, Me)
            PopulateGridDetail()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)

        End If
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Dim lnk As New ImageButton
            Dim i As String = "", fdtrNo As Integer = 0

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            RowNo = gvrow.RowIndex
            ViewState(Left(xScript, Len(xScript) - 5)) = RowNo

            i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
            Viewstate(xScript & "No") = i

            If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim _ds As New DataSet
                _ds = sqlHelper.ExecuteDataset("EApplicantStandardMain_WebOne", i)
                If _ds.Tables.Count > 0 Then
                    If _ds.Tables(0).Rows.Count > 0 Then
                        showFrm.clearFormControls_In_Popup(pnlpopupMain)
                        showFrm.showFormControls_In_Popup(pnlpopupMain, _ds)
                        showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlpopupMain)
                    End If
                End If
                mdlMain.Show()
            Else
                MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Dim lnk As New ImageButton
            Dim i As String = "", fdtrNo As Integer = 0, detiNo As String = ""

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            RowNo2 = gvrow.RowIndex

            i = grdDetl.DataKeys(gvrow.RowIndex).Values(0).ToString()
            detiNo = grdDetl.DataKeys(gvrow.RowIndex).Values(1).ToString()
            Viewstate(xScript & "No") = i

           If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim _ds As New DataSet
                _ds = sqlHelper.ExecuteDataset("EApplicantStandard_WebOne", xPublicVar.xOnlineUseNo, detiNo)
                If _ds.Tables.Count > 0 Then
                    If _ds.Tables(0).Rows.Count > 0 Then
                        showFrm.clearFormControls_In_Popup(pnlpopupDetl)
                        showFrm.showFormControls_In_Popup(pnlpopupDetl, _ds)
                        showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupDetl)
                        Try
                            cboApplicantdimensiontypeno.DataSource = sqlHelper.ExecuteDataset("EApplicantDimensionType_WebLookup", xPublicVar.xOnlineUseNo, Session("xPayLocNo"), True)
                            cboApplicantdimensiontypeno.DataTextField = "tdesc"
                            cboApplicantdimensiontypeno.DataValueField = "tNo"
                            cboApplicantdimensiontypeno.DataBind()
                        Catch ex As Exception

                        End Try
                    End If
                End If

                mdlDetl.Show()
            Else
                MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As New ImageButton
            Dim i As String = ""
            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            RowNo = gvrow.RowIndex
            Session(Left(Session("xFormname"), Len(Session("xformname")) - 5)) = RowNo

            i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
            Viewstate(xScript & "No") = i
            PopulateGrid()

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub grdMain_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdMain.Sorting
        Try
            'PopulateGrid(e.SortExpression, GetSortDirection(e.SortExpression))


            Dim sortExpression = TryCast(ViewState("SortExpression"), String)
            Dim lastDirection = TryCast(ViewState("SortDirection"), String)
            Dim sortDirection As String = grdSort.GetSortDirection(e.SortExpression, sortExpression, lastDirection)

            ViewState("SortExpression") = sortExpression
            ViewState("SortDirection") = lastDirection

            PopulateGrid(False, e.SortExpression, sortDirection)

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub grdMain_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMain.RowCreated
        ' Use the RowType property to determine whether the 
        ' row being created is the header row. 
        ' If e.Row.RowType = DataControlRowType.Header Then
        ' Call the GetSortColumnIndex helper method to determine
        ' the index of the column being sorted.
        Dim sortColumnIndex As Integer = grdSort.GetSortColumnIndex(Me.grdMain, ViewState("SortExpression"))
        If sortColumnIndex > 0 Then
            ' Call the AddSortImage helper method to add
            ' a sort direction image to the appropriate
            ' column header. 
            grdSort.AddSortImage(sortColumnIndex, e.Row, ViewState("SortDirection"))
        End If
        'e.Row.CssClass = "highlight"
        ' End If
    End Sub


    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowAdd) Then
            showFrm.clearFormControls_In_Popup(pnlPopupMain)
            showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupMain, Session("xPayLocNo"))
            mdlMain.Show()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If

    End Sub
    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As New ImageButton
            Dim i As String = ""
            Dim ii As String = ""
            Dim iii As String = ""
            Dim iv As Integer = 0
            Dim v As Integer = 0

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)

            i = grdDetl.DataKeys(gvrow.RowIndex).Values(0).ToString()
            ii = grdDetl.DataKeys(gvrow.RowIndex).Values(1).ToString()
            Viewstate(xScript & "No") = i
            Dim clsFormRedirect As New clsFormRedirect
            clsFormRedirect.openForm("~/secured/AppStandardMainDetailList.aspx?transNo=" & Viewstate(xScript & "No") & "&tmodify=False&IsClickMain=0&transDetailNo=" & ii, "Anchor", 2)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAddD_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowAdd) Then
            showFrm.clearFormControls_In_Popup(pnlpopupDetl)
            showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupDetl, Session("xPayLocNo"))
            Try
                cboApplicantdimensiontypeno.DataSource = sqlHelper.ExecuteDataset("EApplicantDimensionType_WebLookup", xPublicVar.xOnlineUseNo, Session("xPayLocNo"), True)
                cboApplicantdimensiontypeno.DataTextField = "tdesc"
                cboApplicantdimensiontypeno.DataValueField = "tNo"
                cboApplicantdimensiontypeno.DataBind()
            Catch ex As Exception

            End Try
            mdlDetl.Show()

        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If
    End Sub
    Private Function saverecord() As Integer
        Try

            transNo = Generic.CheckDBNull(txtApplicantStandardmainNo.Text, clsBase.clsBaseLibrary.enumObjectType.IntType)
            Dim AcademicStatNo As Integer = 0
            Dim ApplicableYear As Integer = Generic.CheckDBNull(Me.txtApplicableyear.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
            Dim PositionNo As Integer = Generic.CheckDBNull(Me.cboPositionNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
            Dim tProceed As Boolean = True

            If txttcode.Text = "" Or Me.txtApplicableyear.Text = "" Or txttdesc.Text = "" Then
                tProceed = False
            End If
            If tProceed Then
                If sqlHelper.ExecuteNonQuery("EApplicantStandardMain_WebSaveExit", xPublicVar.xOnlineUseNo, transNo, AcademicStatNo, True, ApplicableYear, Me.txttcode.Text.ToString, Me.txttdesc.Text.ToString, PositionNo, Generic.CheckDBNull(Me.cboApplicantScreenTypeNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Me.txtOrderLevel.Text, clsBase.clsBaseLibrary.enumObjectType.IntType), Session("xPayLocNo"), Generic.CheckDBNull(txtInstruction.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)) > 0 Then
                    saverecord = 0
                Else
                    saverecord = 1
                End If
            Else
                saverecord = 2
            End If
        Catch ex As Exception
            saverecord = 2
        End Try

    End Function
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fSaveRecord As Integer = saverecord()
        If fSaveRecord = 0 Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        ElseIf fSaveRecord = 1 Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub
    Private Function saverecordDetl() As Integer
        Dim tProceed As Boolean = True

        'If txtApplicantStandardcode.Text = "" Or Me.txtApplicantStandarddesc.Text = "" Or cboApplicantdimensiontypeno.SelectedItem.Text = "" Or cboresponsetypeno.SelectedItem.Text = "" Or txtStandard.Text = "" Or txtOrderLevelD.Text = "" Then
        '    tProceed = False
        'End If
        If tProceed Then
            If sqlHelper.ExecuteNonQuery("EApplicantStandard_WebSave", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(txtApplicantStandardNo.Text, clsBase.clsBaseLibrary.enumObjectType.IntType), Viewstate(xScript & "No"), Me.txtApplicantStandardcode.Text.ToString, Me.txtApplicantStandarddesc.Text.ToString, Generic.CheckDBNull(0, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Me.txtStandard.Text.ToString, Generic.CheckDBNull(0, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Me.txtOrderLevelD.Text, Global.clsBase.clsBaseLibrary.enumObjectType.StrType), Generic.CheckDBNull(Me.cboApplicantdimensiontypeno.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Me.cboresponsetypeno.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)) > 0 Then
                saverecordDetl = 0
            Else
                saverecordDetl = 1
            End If
            'Else
            '    saverecordDetl = 2
        End If


    End Function
    Protected Sub btnSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim fSaveRecord As Integer = saverecordDetl()

        If fSaveRecord = 0 Then
            PopulateGridDetail()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        ElseIf fSaveRecord = 1 Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
End Class




