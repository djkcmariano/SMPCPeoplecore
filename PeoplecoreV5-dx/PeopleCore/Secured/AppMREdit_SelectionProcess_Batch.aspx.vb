Imports System.Data
Imports clsLib

Partial Class Secured_AppMREdit_SelectionProcess_Batch
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim ActionStatNo As Integer = 2
    Dim InterviewStatNo As Integer
    Dim dtVal As DataTable
    Dim clsGen As New clsGenericClass
    Dim tIndex As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        hidTransNo.Value = TransNo
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)

            PopulateGrid()
            PopulateDropDown()
            AutoCompleteExtender1.ContextKey = Generic.ToInt(cboHiringAlternativeNo.SelectedValue) & "|" & TransNo
        End If
        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

    End Sub

    Private Sub PopulateDropDown()

        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 1)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub
    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub

    

    Protected Sub lnk_Click(sender As Object, e As EventArgs)
        'Info1.xIsApplicant = True
        Dim lnk As New LinkButton
        lnk = sender

        Info1.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
        Info1.xIsApplicant = Generic.ToBol(Generic.Split(lnk.CommandArgument, 1))
        Info1.Show()

    End Sub
    Protected Sub lnkHistory_Click(sender As Object, e As EventArgs)
        'Info1.xIsApplicant = True
        Dim lnk As New LinkButton
        lnk = sender

        apphistory1.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
        apphistory1.xIsApplicant = Generic.ToBol(Generic.Split(lnk.CommandArgument, 1))
        apphistory1.Show()

    End Sub

    'Protected Sub lnkFilter1_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim ib As New LinkButton
    '    ib = sender
    '    Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
    '    rowno = gvrow.RowIndex
    '    Me.grdMain.SelectedIndex = Generic.ToInt(rowno)
    '    ViewState("TransNo") = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
    '    'ViewState("TransCode") = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
    '    PopulateDetl(1)

    'End Sub

    'Protected Sub lnkFilter2_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim ib As New LinkButton
    '    ib = sender

    '    Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
    '    rowno = gvrow.RowIndex
    '    Me.grdMain.SelectedIndex = Generic.ToInt(rowno)
    '    ViewState("TransNo") = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
    '    'ViewState("TransCode") = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
    '    PopulateDetl(2)

    'End Sub

    'Protected Sub lnkFilter3_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim ib As New LinkButton
    '    ib = sender

    '    Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
    '    rowno = gvrow.RowIndex
    '    Me.grdMain.SelectedIndex = Generic.ToInt(rowno)
    '    ViewState("TransNo") = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
    '    'ViewState("TransCode") = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
    '    PopulateDetl(3)

    'End Sub

#Region "Main"
    Protected Sub PopulateGrid()
        Try
            tIndex = Generic.ToInt(cboTabNo.SelectedValue)
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            'dt = SQLHelper.ExecuteDataTable("EMRInterview_Web_Batch", UserNo)
            dt = SQLHelper.ExecuteDataTable("EMRActivity_Web", UserNo, tIndex)
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If

            grdMain.DataSource = dv
            grdMain.DataBind()

            If Generic.ToInt(ViewState("TransNo")) = 0 And dt.Rows.Count > 0 Then
                grdMain.SelectedIndex = 0
                ViewState("TransNo") = grdMain.DataKeys(0).Values(0).ToString()
                ViewState("TransCode") = grdMain.DataKeys(0).Values(1).ToString()
            End If

            PopulateDetl()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim ib As New ImageButton
            ib = sender
            Dim SelectionId As Integer = 0
            Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
            rowno = gvrow.RowIndex
            Me.grdMain.SelectedIndex = Generic.ToInt(rowno)
            'ViewState("TransNo") = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
            ViewState("TransNo") = grdMain.DataKeys(gvrow.RowIndex).Values(2).ToString()
            SelectionId = Generic.ToInt(grdMain.DataKeys(gvrow.RowIndex).Values(1))
            If SelectionId = 1 Then
                PopulateDetl()
            ElseIf SelectionId = 2 Then
                Response.Redirect("~/secured/AppMREdit_SelectionProcess_Batch_JobOffer.aspx?id=" & ViewState("TransNo") & "&FormName=AppMREdit_SelectionProcess_Batch.aspx&TableName=EActivityMonitoring")
            ElseIf SelectionId = 3 Then
                Response.Redirect("~/secured/AppMREdit_SelectionProcess_Batch_PreEmployment.aspx?id=" & ViewState("TransNo") & "&FormName=AppMREdit_SelectionProcess_Batch.aspx&TableName=EActivityMonitoring")
            ElseIf SelectionId = 4 Then
                Response.Redirect("~/secured/AppMREdit_SelectionProcess_Batch_ContractPrep.aspx?id=" & ViewState("TransNo") & "&FormName=AppMREdit_SelectionProcess_Batch.aspx&TableName=EActivityMonitoring")
            End If


        Catch ex As Exception
        End Try
    End Sub

    Protected Sub grdMain_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
        Try
            If ViewState("SortDirection") Is Nothing OrElse ViewState("SortExpression").ToString() <> e.SortExpression Then
                ViewState("SortDirection") = "ASC"
            ElseIf ViewState("SortDirection").ToString() = "ASC" Then
                ViewState("SortDirection") = "DESC"
            ElseIf ViewState("SortDirection").ToString() = "DESC" Then
                ViewState("SortDirection") = "ASC"
            End If
            ViewState("SortExpression") = e.SortExpression
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            grdMain.PageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
        '    Dim ib As New ImageButton
        '    ib = sender
        '    Dim dt As DataTable
        '    dt = SQLHelper.ExecuteDataTable("EMRInterview_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
        '    For Each row As DataRow In dt.Rows
        '        Generic.PopulateData(Me, "pnlPopupMain", dt)
        '    Next
        '    mdlMain.Show()
        'Else
        '    MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        'End If
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New ImageButton
            ib = sender
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRActivity_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
            For Each row As DataRow In dt.Rows
                Generic.ClearControls(Me, "pnlMain")
                Generic.PopulateData(Me, "pnlMain", dt)
                Try
                    cboApplicantStandardMainNo.DataSource = SQLHelper.ExecuteDataSet("EApplicantStandardMain_WebLookup", UserNo, PayLocNo)
                    cboApplicantStandardMainNo.DataTextField = "tdesc"
                    cboApplicantStandardMainNo.DataValueField = "tno"
                    cboApplicantStandardMainNo.DataBind()
                Catch ex As Exception

                End Try

            Next

            If Generic.ToInt(cboApplicantStandardMainNo.SelectedValue) = -996 Or Generic.ToInt(cboApplicantStandardMainNo.SelectedValue) = -997 Or Generic.ToInt(cboApplicantStandardMainNo.SelectedValue) = -998 Then
                dvSched.Visible = False
                txtDateFrom.CssClass = "form-control"
                txtDateTo.CssClass = "form-control"
            Else
                dvSched.Visible = True
                txtDateFrom.CssClass = "form-control required"
                txtDateTo.CssClass = "form-control required"
            End If

            mdMainAdd.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

        'If txtIsposted.Checked Then
        '    lnkSavemain.Enabled = False
        '    Generic.EnableControls(Me, "pnlMain", False)
        'Else
        '    Generic.EnableControls(Me, "pnlMain", True)
        'End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
        '    Generic.ClearControls(Me, "pnlPopupMain")
        '    mdlMain.Show()
        'Else
        '    MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        'End If
        lnkSavemain.Enabled = True
        Generic.EnableControls(Me, "pnlMain", True)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Try
                cboApplicantStandardMainNo.DataSource = SQLHelper.ExecuteDataSet("EApplicantStandardMain_WebLookup", UserNo, PayLocNo)
                cboApplicantStandardMainNo.DataTextField = "tdesc"
                cboApplicantStandardMainNo.DataValueField = "tno"
                cboApplicantStandardMainNo.DataBind()
            Catch ex As Exception

            End Try

            If Generic.ToInt(cboApplicantStandardMainNo.Text) = -996 Or Generic.ToInt(cboApplicantStandardMainNo.Text) = -997 Or Generic.ToInt(cboApplicantStandardMainNo.Text) = -998 Then
                dvSched.Visible = False
                txtDateFrom.CssClass = "form-control"
                txtDateTo.CssClass = "form-control"
            Else
                dvSched.Visible = True
                txtDateFrom.CssClass = "form-control required"
                txtDateTo.CssClass = "form-control required"
            End If

            Generic.ClearControls(Me, "pnlMain")
            mdMainAdd.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If


    End Sub

    Protected Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            For i As Integer = 0 To Me.grdMain.Rows.Count - 1
                chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
                ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
                If chk.Checked = True Then
                    SQLHelper.ExecuteNonQuery("EMRActivity_WebPost", UserNo, Generic.ToInt(ib.CommandArgument))
                    Count = Count + 1
                End If
            Next
            If Count > 0 Then
                PopulateGrid()
                MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccesPost, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
        '    For i As Integer = 0 To Me.grdMain.Rows.Count - 1
        '        chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
        '        ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
        '        If chk.Checked = True Then
        '            Generic.DeleteRecordAuditCol("EMRInterviewDeti", UserNo, "MRInterviewNo", Generic.ToInt(ib.CommandArgument))
        '            Generic.DeleteRecordAudit("EMRInterview", UserNo, Generic.ToInt(ib.CommandArgument))
        '            Count = Count + 1
        '        End If
        '    Next
        '    If Count > 0 Then
        '        PopulateGrid()
        '        MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        '    Else
        '        MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        '    End If
        'Else
        '    MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        'End If

        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For i As Integer = 0 To Me.grdMain.Rows.Count - 1
                chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
                ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
                If chk.Checked = True Then
                    Generic.DeleteRecordAuditCol("EMRActivityDeti", UserNo, "MRActivityNo", Generic.ToInt(ib.CommandArgument))
                    Generic.DeleteRecordAuditCol("EMRActivityhired", UserNo, "MRActivityNo", Generic.ToInt(ib.CommandArgument))
                    Generic.DeleteRecordAudit("EMRActivity", UserNo, Generic.ToInt(ib.CommandArgument))
                    Count = Count + 1
                End If
            Next
            If Count > 0 Then
                PopulateGrid()
                MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim transNo = Generic.ToInt(txtMRInterviewNo.Text)
        Dim DateFrom As String = Generic.ToStr(txtScreeningDateFrom.Text)
        Dim DateTo As String = Generic.ToStr(txtScreeningDateTo.Text)
        Dim Time As String = Generic.ToStr(txtScreeningTime.Text)
        Dim Venue As String = Generic.ToStr(txtScreeningVenue.Text)
        Dim ScreeningByNo As Integer = Generic.ToInt(Me.hifScreeningByNo.Value)
        Dim FacilitatorNo As Integer = Generic.ToInt(Me.hifFacilitatorNo.Value)

        If SQLHelper.ExecuteNonQuery("EMRInterview_WebSave", UserNo, transNo, DateFrom, DateTo, Time, Venue, ScreeningByNo, FacilitatorNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub
    Protected Sub lnkSaveMain_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If SaveRecordMain() = 0 Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Information("No transaction available!", Me)
        End If
    End Sub

    Private Function SaveRecordMain() As Integer
        SaveRecordMain = 0
        Dim ApplicantStandardMainNo As Integer = Generic.ToInt(cboApplicantStandardMainNo.SelectedValue)
        Dim DateFrom As String = Generic.ToStr(txtDateFrom.Text)
        Dim DateTo As String = Generic.ToStr(txtDateTo.Text)
        Dim TargetDate As String = Generic.ToStr(txtTargetDate.Text)
        Dim TotalCandidates As Integer = Generic.ToInt(txtTotalCandidates.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)
        Dim ds As New DataSet

        ds = SQLHelper.ExecuteDataSet("EMRActivity_WebSave", UserNo, Generic.ToInt(txtMRActivityNo.Text), ApplicantStandardMainNo, DateFrom, DateTo, TargetDate, TotalCandidates, Remarks)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                SaveRecordMain = Generic.ToInt(ds.Tables(0).Rows(0)("RetVal"))
            End If
        End If
        Return SaveRecordMain
    End Function

    Protected Sub cboApplicantStandardMainNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        If Generic.ToInt(cboApplicantStandardMainNo.SelectedValue) = -996 Or Generic.ToInt(cboApplicantStandardMainNo.SelectedValue) = -997 Or Generic.ToInt(cboApplicantStandardMainNo.SelectedValue) = -998 Then
            dvSched.Visible = False
            txtDateFrom.CssClass = "form-control"
            txtDateTo.CssClass = "form-control"
        Else
            dvSched.Visible = True
            txtDateFrom.CssClass = "form-control required"
            txtDateTo.CssClass = "form-control required"
        End If

        mdMainAdd.Show()
    End Sub

#End Region

#Region "Detail"
    Protected Sub PopulateDetl(Optional StatusNo As Integer = 0)
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            'dt = SQLHelper.ExecuteDataTable("EMRInterview_Web_Batch_Deti", UserNo, Generic.ToInt(ViewState("TransNo")), Filter1.SearchText, StatusNo)
            dt = SQLHelper.ExecuteDataTable("EMRActivityDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")), Filter1.SearchText)
            dtVal = dt
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirectionDetl") IsNot Nothing Then
                sortDirection = ViewState("SortDirectionDetl").ToString()
            End If
            If ViewState("SortExpressionDetl") IsNot Nothing Then
                sortExpression = ViewState("SortExpressionDetl").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            'grdDetl.SelectedIndex = 0
            grdDetl.DataSource = dv
            grdDetl.DataBind()

            'Me.lblDetl.Text = "Reference No.: " & Generic.ToStr(ViewState("TransCode"))
            Me.lblDetl.Text = "List of Applicant(s)"

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdDetl_Sorting(sender As Object, e As GridViewSortEventArgs)
        Try
            If ViewState("SortDirectionDetl") Is Nothing OrElse ViewState("SortExpressionDetl").ToString() <> e.SortExpression Then
                ViewState("SortDirectionDetl") = "ASC"
            ElseIf ViewState("SortDirectionDetl").ToString() = "ASC" Then
                ViewState("SortDirectionDetl") = "DESC"
            ElseIf ViewState("SortDirectionDetl").ToString() = "DESC" Then
                ViewState("SortDirectionDetl") = "ASC"
            End If
            ViewState("SortExpressionDetl") = e.SortExpression
            PopulateDetl()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub grdDetl_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            grdDetl.PageIndex = e.NewPageIndex
            PopulateDetl()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnBatch_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim chk As New CheckBox, Count As Integer = 0
            For i As Integer = 0 To Me.grdDetl.Rows.Count - 1
                chk = CType(grdDetl.Rows(i).FindControl("txtIsSelect"), CheckBox)
                If chk.Checked = True Then
                    Count = Count + 1
                End If
            Next

            If Count > 0 Then
                Generic.ClearControls(Me, "Panel2")
                Try
                    cboActionStatNo.DataSource = SQLHelper.ExecuteDataSet("EActionStat_WebLookup", UserNo, ActionStatNo, ViewState("TransNo"), PayLocNo)
                    cboActionStatNo.DataTextField = "tdesc"
                    cboActionStatNo.DataValueField = "tno"
                    cboActionStatNo.DataBind()
                Catch ex As Exception

                End Try

                Try
                    'cboInterviewStatNo.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EInterviewStatL", PayLocNo, "", "")
                    cboInterviewStatNo.DataSource = SQLHelper.ExecuteDataSet("EInterviewStat_WebLookup", UserNo, InterviewStatNo, ViewState("TransNo"), PayLocNo)
                    cboInterviewStatNo.DataTextField = "tdesc"
                    cboInterviewStatNo.DataValueField = "tno"
                    cboInterviewStatNo.DataBind()
                Catch ex As Exception

                End Try
                ModalPopupExtender2.Show()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub btnBatch2_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim chk As New CheckBox, Count As Integer = 0
            For i As Integer = 0 To Me.grdDetl.Rows.Count - 1
                chk = CType(grdDetl.Rows(i).FindControl("txtIsSelect"), CheckBox)
                If chk.Checked = True Then
                    Count = Count + 1
                End If
            Next

            If Count > 0 Then
                Generic.ClearControls(Me, "Panel2")
                Try
                    cboActionStatNo.DataSource = SQLHelper.ExecuteDataSet("EActionStat_WebLookup", UserNo, ActionStatNo, ViewState("TransNo"), PayLocNo)
                    cboActionStatNo.DataTextField = "tdesc"
                    cboActionStatNo.DataValueField = "tno"
                    cboActionStatNo.DataBind()
                Catch ex As Exception

                End Try

                Try
                    'cboInterviewStatNo.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EInterviewStatL", PayLocNo, "", "")
                    cboInterviewStatNo.DataSource = SQLHelper.ExecuteDataSet("EInterviewStat_WebLookup", UserNo, InterviewStatNo, ViewState("TransNo"), PayLocNo)
                    cboInterviewStatNo.DataTextField = "tdesc"
                    cboInterviewStatNo.DataValueField = "tno"
                    cboInterviewStatNo.DataBind()
                Catch ex As Exception

                End Try
                ModalPopupExtender1.Show()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New ImageButton
            ib = sender

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRInterviewDeti_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupSched", dt)
            Next

            PopulateScreening()
            mdlSched.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub btnAddDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub btnDeleteDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim MRInterviewNo As Integer = Generic.ToInt(ViewState("TransNo"))
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For i As Integer = 0 To Me.grdDetl.Rows.Count - 1
                chk = CType(grdDetl.Rows(i).FindControl("txtIsSelect"), CheckBox)
                ib = CType(grdDetl.Rows(i).FindControl("btnEditDetl"), ImageButton)
                If chk.Checked = True Then
                    Generic.DeleteRecordAudit("EMRInterviewDeti", UserNo, Generic.ToInt(ib.CommandArgument))
                    Count = Count + 1
                End If
            Next
            If Count > 0 Then
                ViewState("TransNo") = MRInterviewNo
                PopulateGrid()
                MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If

    End Sub

    Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim MRInterviewNo As Integer = Generic.ToInt(ViewState("TransNo"))
        Dim HiringAlternativeNo As Integer = Generic.ToInt(cboHiringAlternativeNo.SelectedValue)
        Dim hidID As Integer = Generic.ToInt(Me.hidID.Value)

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EMRInterviewDeti_WebValidate", UserNo, MRInterviewNo, TransNo, hidID, HiringAlternativeNo)
        For Each row As DataRow In dt.Rows
            invalid = Generic.ToBol(row("Invalid"))
            messagedialog = Generic.ToStr(row("MessageDialog"))
            alerttype = Generic.ToStr(row("AlertType"))
        Next

        If invalid = True Then
            mdlDetl.Show()
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EMRInterviewDeti_WebSave", UserNo, MRInterviewNo, TransNo, hidID, HiringAlternativeNo) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal Then
            ViewState("TransNo") = MRInterviewNo
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkSave2_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lbl As New Label, lblInterviewDetiNo As New Label
        Dim tcount As Integer, SaveCount As Integer = 0, lblmractivitydetino As New Label
        Dim xds As New DataSet

        Dim ScreeingResultNo As Integer = Generic.ToInt(cboInterviewStatNo.SelectedValue)
        Dim StatusNo As Integer = Generic.ToInt(cboActionStatNo.SelectedValue)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then

            For tcount = 0 To Me.grdDetl.Rows.Count - 1
                lblInterviewDetiNo = CType(grdDetl.Rows(tcount).FindControl("lblInterviewDetiNo"), Label)
                lbl = CType(grdDetl.Rows(tcount).FindControl("lblNo"), Label)
                lblmractivitydetino = CType(grdDetl.Rows(tcount).FindControl("lblmractivitydetino"), Label)
                'cboInterviewStatNo = CType(grdDetl.Rows(tcount).FindControl("cboInterviewStatNo"), DropDownList)
                'cboActionStatNo = CType(grdDetl.Rows(tcount).FindControl("cboActionStatNo"), DropDownList)

                Dim MRInterviewDetiNo As Integer = Generic.ToInt(lblInterviewDetiNo.Text)
                Dim MRHiredMassNo As Integer = Generic.ToInt(lbl.Text)
                Dim chk = CType(grdDetl.Rows(tcount).FindControl("txtIsSelect"), CheckBox)

                If Not cboInterviewStatNo Is Nothing And chk.Checked = True Then

                    If SQLHelper.ExecuteNonQuery("EMRInterviewDeti_WebUpdate", UserNo, MRInterviewDetiNo, ScreeingResultNo) > 0 Then
                        SaveCount = SaveCount + 1
                    End If

                    If SQLHelper.ExecuteNonQuery("EMRActivityDeti_WebUpdate", UserNo, Generic.ToInt(lblmractivitydetino.Text), ScreeingResultNo, StatusNo) > 0 Then

                    End If

                    If ScreeingResultNo = 1 Then

                        If Not cboActionStatNo Is Nothing Then
                            If SQLHelper.ExecuteNonQuery("EMRHiredMass_WebUpdate", UserNo, MRHiredMassNo, MRInterviewDetiNo, StatusNo, ActionStatNo) > 0 Then
                                SaveCount = SaveCount + 1
                            End If
                        End If

                    End If

                End If


            Next

            If SaveCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" & SaveCount & ") " & MessageTemplate.SuccessUpdate, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkSave3_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lbl As New Label, lblInterviewDetiNo As New Label
        Dim tcount As Integer, SaveCount As Integer = 0
        Dim xds As New DataSet

        Dim DateFrom As String = Generic.ToStr(txtScheduleDateFrom2.Text)
        Dim DateTo As String = Generic.ToStr(txtScheduleDateTo2.Text)
        Dim Time As String = Generic.ToStr(txtScheduleTime2.Text)
        Dim Venue As String = Generic.ToStr(txtScheduleVenue2.Text)
        Dim InterviewByNo As Integer = Generic.ToInt(Me.hifInterviewByNo2.Value)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then

            For tcount = 0 To Me.grdDetl.Rows.Count - 1
                lblInterviewDetiNo = CType(grdDetl.Rows(tcount).FindControl("lblInterviewDetiNo"), Label)
                'cboInterviewStatNo = CType(grdDetl.Rows(tcount).FindControl("cboInterviewStatNo"), DropDownList)
                'cboActionStatNo = CType(grdDetl.Rows(tcount).FindControl("cboActionStatNo"), DropDownList)

                Dim MRInterviewDetiNo As Integer = Generic.ToInt(lblInterviewDetiNo.Text)
                Dim chk = CType(grdDetl.Rows(tcount).FindControl("txtIsSelect"), CheckBox)

                If chk.Checked = True Then
                    If SQLHelper.ExecuteNonQuery("EMRInterviewDeti_WebSaveEdit", UserNo, Generic.ToInt(MRInterviewDetiNo), 1, DateFrom, DateTo, Time, Venue, InterviewByNo) > 0 Then
                        SaveCount = SaveCount + 1
                    End If

                End If

            Next

            If SaveCount > 0 Then
                PopulateDetl()
                MessageBox.Success("(" & SaveCount & ") " & MessageTemplate.SuccessUpdate, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkTemplate_Click(sender As Object, e As EventArgs)
        Dim ib As ImageButton
        ib = sender
        Response.Redirect("~/secured/EvalTemplateForm.aspx?id=" & ib.CommandArgument & "&FormName=AppStandardHeader.aspx&TableName=EApplicantStandardHeader")
    End Sub

    Protected Sub btnUpdateDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lbl As New Label, lblInterviewDetiNo As New Label, cboInterviewStatNo As New DropDownList, cboActionStatNo As New DropDownList
        Dim tcount As Integer, SaveCount As Integer = 0
        Dim xds As New DataSet, lblmractivitydetino As New Label

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then

            For tcount = 0 To Me.grdDetl.Rows.Count - 1
                lblInterviewDetiNo = CType(grdDetl.Rows(tcount).FindControl("lblInterviewDetiNo"), Label)
                lbl = CType(grdDetl.Rows(tcount).FindControl("lblNo"), Label)
                cboInterviewStatNo = CType(grdDetl.Rows(tcount).FindControl("cboInterviewStatNo"), DropDownList)
                cboActionStatNo = CType(grdDetl.Rows(tcount).FindControl("cboActionStatNo"), DropDownList)
                lblmractivitydetino = CType(grdDetl.Rows(tcount).FindControl("lblmractivitydetino"), Label)
                Dim MRInterviewDetiNo As Integer = Generic.ToInt(lblInterviewDetiNo.Text)
                Dim MRHiredMassNo As Integer = Generic.ToInt(lbl.Text)
                Dim ScreeingResultNo As Integer = Generic.ToInt(cboInterviewStatNo.SelectedValue)
                Dim StatusNo As Integer = Generic.ToInt(cboActionStatNo.SelectedValue)

                If Not cboInterviewStatNo Is Nothing Then

                    If SQLHelper.ExecuteNonQuery("EMRInterviewDeti_WebUpdate", UserNo, MRInterviewDetiNo, ScreeingResultNo, "") > 0 Then
                        SaveCount = SaveCount + 1
                    End If
                    If SQLHelper.ExecuteNonQuery("EMRActivityDeti_WebUpdate", UserNo, Generic.ToInt(lblmractivitydetino.Text), ScreeingResultNo, StatusNo) > 0 Then

                    End If

                    If ScreeingResultNo = 1 Then

                        If Not cboActionStatNo Is Nothing Then
                            If SQLHelper.ExecuteNonQuery("EMRHiredMass_WebUpdate", UserNo, MRHiredMassNo, MRInterviewDetiNo, StatusNo, ActionStatNo) > 0 Then
                                SaveCount = SaveCount + 1
                            End If
                        End If

                    End If

                End If


            Next

            If SaveCount > 0 Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            End If        
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub grdDetl_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDetl.RowDataBound

        Dim dt As New DataTable
        Dim cboActionStatNo As New DropDownList
        Dim cboInterviewStatNo As New DropDownList
        Dim MRInterviewNo As Integer = 0

        dt = dtVal
        For Each row As DataRow In dt.Rows
            For tcount = 0 To Me.grdDetl.Rows.Count - 1
                'For Dropdown Only
                cboActionStatNo = CType(grdDetl.Rows(tcount).FindControl("cboActionStatNo"), DropDownList)
                cboActionStatNo.Text = Generic.ToStr(dt.Rows(tcount)("ActionStatNo"))
                cboActionStatNo.Enabled = Generic.ToBol(dt.Rows(tcount)("IsEnabled"))
                MRInterviewNo = Generic.ToStr(dt.Rows(tcount)("MRInterviewNo"))

                cboInterviewStatNo = CType(grdDetl.Rows(tcount).FindControl("cboInterviewStatNo"), DropDownList)
                cboInterviewStatNo.Text = Generic.ToStr(dt.Rows(tcount)("InterviewStatNo"))
                cboInterviewStatNo.Enabled = Generic.ToBol(dt.Rows(tcount)("IsEnabled"))

                Try
                    cboActionStatNo.DataSource = SQLHelper.ExecuteDataSet("EActionStat_WebLookup", UserNo, ActionStatNo, MRInterviewNo, PayLocNo)
                    cboActionStatNo.DataTextField = "tdesc"
                    cboActionStatNo.DataValueField = "tno"
                    cboActionStatNo.DataBind()
                Catch ex As Exception

                End Try

                Try
                    'cboInterviewStatNo.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EInterviewStatL", PayLocNo, "", "")
                    cboInterviewStatNo.DataSource = SQLHelper.ExecuteDataSet("EInterviewStat_WebLookup", UserNo, InterviewStatNo, ViewState("TransNo"), PayLocNo)
                    cboInterviewStatNo.DataTextField = "tdesc"
                    cboInterviewStatNo.DataValueField = "tno"
                    cboInterviewStatNo.DataBind()
                Catch ex As Exception

                End Try

            Next
        Next

    End Sub

    Protected Sub cboInterviewStatNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        For tcount = 0 To Me.grdDetl.Rows.Count - 1
            Dim cboi As New DropDownList
            Dim cboa As New DropDownList
            cboi = CType(grdDetl.Rows(tcount).FindControl("cboInterviewStatNo"), DropDownList)
            cboa = CType(grdDetl.Rows(tcount).FindControl("cboActionStatNo"), DropDownList)

            If cboi.Text <> "1" Then
                cboa.Enabled = False
                cboa.Text = ""
            Else
                cboa.Enabled = True
            End If
        Next

    End Sub

    Protected Sub lnkSaveSched_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim transNo = Generic.ToInt(txtMRInterviewDetiNo.Text)
        Dim IsSchedule As Boolean = Generic.ToBol(txtIsSchedule.Checked)
        Dim DateFrom As String = Generic.ToStr(txtScheduleDateFrom.Text)
        Dim DateTo As String = Generic.ToStr(txtScheduleDateTo.Text)
        Dim Time As String = Generic.ToStr(txtScheduleTime.Text)
        Dim Venue As String = Generic.ToStr(txtScheduleVenue.Text)
        Dim InterviewByNo As Integer = Generic.ToInt(Me.hifInterviewByNo.Value)

        If SQLHelper.ExecuteNonQuery("EMRInterviewDeti_WebSaveEdit", UserNo, transNo, IsSchedule, DateFrom, DateTo, Time, Venue, InterviewByNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub txtIsScreening_CheckedChanged(sender As Object, e As System.EventArgs)
        PopulateScreening()
        mdlSched.Show()
    End Sub

    Private Sub PopulateScreening()

        If Generic.ToBol(Me.txtIsSchedule.Checked) = True Then
            Me.txtScheduleDateFrom.Enabled = True
            Me.txtScheduleDateTo.Enabled = True
            Me.txtScheduleTime.Enabled = True
            Me.txtScheduleVenue.Enabled = True
            Me.txtInterviewByName.Enabled = True
        Else
            Me.txtScheduleDateFrom.Text = ""
            Me.txtScheduleDateTo.Text = ""
            Me.txtScheduleTime.Text = ""
            Me.txtScheduleVenue.Text = ""
            Me.txtInterviewByName.Text = ""

            Me.txtScheduleDateFrom.Enabled = False
            Me.txtScheduleDateTo.Enabled = False
            Me.txtScheduleTime.Enabled = False
            Me.txtScheduleVenue.Enabled = False
            Me.txtInterviewByName.Enabled = False
        End If

    End Sub

    Protected Sub lnkForm_Click(sender As Object, e As EventArgs)
        Try
            Dim ib As New LinkButton
            ib = sender

            Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
            rowno = gvrow.RowIndex
            Me.grdDetl.SelectedIndex = Generic.ToInt(rowno)
            Dim MRInterviewDetiNo As Integer = grdDetl.DataKeys(gvrow.RowIndex).Values(1).ToString()
            Dim TemplateID As Integer = grdDetl.DataKeys(gvrow.RowIndex).Values(2).ToString()

            Response.Redirect("~/secured/AppMREdit_EvalTemplateForm.aspx?id=" & TransNo & "&TemplateID=" & TemplateID & "&TransNo=" & MRInterviewDetiNo & "&FormName=AppMREdit_SelectionProcess_Batch.aspx&TableName=EActivityMonitoring")

        Catch ex As Exception
        End Try

    End Sub

#End Region

End Class


