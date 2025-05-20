Imports System.Data
Imports Microsoft.VisualBasic
Imports clsLib

Partial Class Secured_ERDAAREdit
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim clsGeneric As New clsGenericClass

    Private Sub PopulateData()

        Dim DAPolicyTypeNo As Integer = 0
        Dim DAPolicyNo As Integer = 0
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDAAR_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))
            DAPolicyTypeNo = Generic.ToInt(row("DAPolicyTypeNo"))
            DAPolicyNo = Generic.ToInt(row("DAPolicyNo"))
        Next

        PopulateDAPolicyType(DAPolicyTypeNo, DAPolicyNo)
        PopulateDAPolicy(DAPolicyNo)
        Generic.PopulateData(Me, "Panel1", dt)


    End Sub

    'Enable or disable control
    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        If Generic.ToBol(txtIsPosted.Checked) = True Then
            Enabled = False
        End If
        Generic.EnableControls(Me, "Panel1", Enabled)
        If TransNo = 0 Then
            'cboApprovalStatNo.Text = 2

            'If Generic.ToInt(cboDAPolicyTypeNo.SelectedValue) > 0 Then
            '    cboDAPolicyNo.Enabled = Enabled
            'Else
            '    cboDAPolicyNo.Enabled = False
            'End If

            'If Generic.ToInt(cboDAPolicyNo.SelectedValue) > 0 Then
            '    cboDACaseTypeNo.Enabled = Enabled
            'Else
            '    cboDACaseTypeNo.Enabled = False
            'End If
        End If
        Generic.PopulateDataDisabled(Me, "Panel1", UserNo, PayLocNo, Generic.ToStr(Session("xMenuType")))

        'cboApprovalStatNo.Enabled = False
        txtCode.Enabled = False
        lnkModify.Visible = Not Enabled
        lnkSubmit.Visible = Enabled

        'cboReceivedByNo.Enabled = False
        'txtReceivedDate.Enabled = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "ERDAARList.aspx")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateData()

            Try
                cboApprovalStatNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 16)
                cboApprovalStatNo.DataValueField = "tNo"
                cboApprovalStatNo.DataTextField = "tDesc"
                cboApprovalStatNo.DataBind()
                cboApprovalStatNo.Items.Remove("All")
            Catch ex As Exception
            End Try

        End If

        EnabledControls()

    End Sub

    'Security for modify record
    Protected Sub lnkModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkModify.Click

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "ERDAARList.aspx") Then
            If txtIsPosted.Checked Then
                MessageBox.Information(MessageTemplate.PostedTransaction, Me)
            Else
                ViewState("IsEnabled") = True
                EnabledControls()
            End If
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
        End If

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "ERDAARList.aspx") Then
            Dim RetVal As Boolean = False
            Dim dt As DataTable
            Dim tno As Integer = Generic.ToInt(Me.txtDAARNo.Text)
            Dim DocketNo As String = Generic.ToStr(Me.txtDocketNo.Text)
            Dim DAARTypeNo As Integer = Generic.ToInt(Me.cboDAARTypeNo.SelectedValue)
            Dim DACaseTypeNo As Integer = Generic.ToInt(Me.cboDACaseTypeNo.SelectedValue)
            Dim DAARDate As String = Generic.ToStr(Me.txtDAARDate.Text)
            Dim Entity As String = ""
            Dim ComplainantNo As Integer = Generic.ToInt(Generic.Split(Me.hifComplainantNo.Value, 0))
            Dim Complainant As String = ""
            Dim Address As String = "" 'Generic.ToStr(Me.txtAddress.Text)
            Dim StartDate As String = "" 'Generic.ToStr(Me.txtStartDate.Text)
            Dim EndDate As String = "" 'Generic.ToStr(Me.txtEndDate.Text)
            Dim AckDate As String = "" 'Generic.ToStr(Me.txtAckDate.Text)
            Dim Amount As Double = Generic.ToDec(Me.txtAmount.Text)
            Dim ReceivedByNo As Integer = Generic.ToInt(Generic.Split(Me.hifReceivedByNo.Value, 0))
            Dim ReceivedDate As String = Generic.ToStr(Me.txtReceivedDate.Text)
            Dim AssignedByNo As Integer = Generic.ToInt(Generic.Split(Me.hifAssignedByNo.Value, 0))
            Dim ReceivedEvalDate As String = Generic.ToStr(Me.txtReceivedEvalDate.Text)
            Dim BI As String = Generic.ToStr(Me.txtBI.Text)
            Dim Comments As String = Generic.ToStr(Me.txtComments.Text)
            Dim Evaluation As String = Generic.ToStr(Me.txtEvaluation.Text)
            Dim EvaluationByNo As Integer = Generic.ToInt(Generic.Split(Me.hifEvaluationByNo.Value, 0))
            Dim EvaluationDate As String = Generic.ToStr(Me.txtEvaluationDate.Text)
            Dim Recommendation As String = Generic.ToStr(Me.txtRecommendation.Text)
            Dim RecommendedByNo As Integer = Generic.ToInt(Generic.Split(Me.hifRecommendedByNo.Value, 0))
            Dim RecommendationDate As String = Generic.ToStr(Me.txtRecommendationDate.Text)
            Dim ApprovedByNo As Integer = Generic.ToInt(Generic.Split(Me.hifApprovedByNo.Value, 0))
            Dim ApprovedDate As String = Generic.ToStr(Me.txtApprovedDate.Text)
            Dim DAARStatNo As Integer = Generic.ToInt(Me.cboDAARStatNo.SelectedValue)
            Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)
            Dim DAARLocation As String = Generic.ToStr(txtDAARLocation.Text)
            Dim DAARDesc As String = Generic.ToStr(txtDAARDesc.Text)
            Dim DAARHappen As String = Generic.ToStr(txtDAARHappen.Text)
            Dim DAARImpact As String = Generic.ToStr(txtDAARImpact.Text)
            Dim OccurenceDate As String = Generic.ToStr(txtOccurenceDate.Text)
            Dim DAPolicyNo As Integer = Generic.ToInt(cboDAPolicyNo.SelectedValue)
            Dim DAPolicyTypeNo As Integer = Generic.ToInt(cboDAPolicyTypeNo.SelectedValue)
            Dim approvalStatNo As Integer = Generic.ToInt(cboApprovalStatNo.SelectedValue)
            Dim employeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))
            Dim IsForDA As Boolean = Generic.ToBol(chkIsForDA.Checked)
            Dim ERIncidentTypeNo As Integer = Generic.ToInt(cboERIncidentTypeNo.SelectedValue)
            Dim ERIncidentClassNo As Integer = Generic.ToInt(cboERIncidentClassNo.SelectedValue)
            Dim OccurenceTime As String = Generic.ToStr(Replace(txtOccurenceTime.Text, ":", ""))

            dt = SQLHelper.ExecuteDataTable("EDAAR_WebSave", UserNo, tno, employeeNo, DocketNo, DAARTypeNo, DACaseTypeNo, DAARDate, Entity, ComplainantNo, Complainant, [Address], StartDate, EndDate, AckDate, Amount, ReceivedByNo, ReceivedDate, AssignedByNo, ReceivedEvalDate, BI, Comments, Evaluation, EvaluationByNo, EvaluationDate, Recommendation, RecommendationDate, ApprovedByNo, ApprovedDate, DAARStatNo, Remarks, Session("xPayLocNo"), DAARLocation, DAARDesc, DAARHappen, DAARImpact, OccurenceDate, DAPolicyNo, DAPolicyTypeNo, approvalStatNo, RecommendedByNo, IsForDA, ERIncidentTypeNo, ERIncidentClassNo, txtOtherPerson.Text, Generic.ToInt(hifImmediateSuperiorNo.Value), txtInvestigateStartDate.Text, txtInvestigateEndDate.Text, Generic.ToInt(cboProjectNo.SelectedValue), txtMediationDate.Text, txtPersonInvolvedMediation.Text, OccurenceTime)
            For Each row As DataRow In dt.Rows
                TransNo = Generic.ToInt(row("Retval"))
                RetVal = True
            Next

            If RetVal = True Then
                Dim url As String = "ERDAAREdit.aspx?id=" & TransNo
                MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If

        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub cboDAPolicyTypeNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        PopulateDAPolicyType(Generic.ToInt(cboDAPolicyTypeNo.SelectedValue), 0)
    End Sub

    Private Sub PopulateDAPolicyType(ByVal DAPolicyTypeNo As Integer, ByVal DAPolicyNo As Integer)
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EDAPolicyType_WebLookup", UserNo, DAPolicyTypeNo, DAPolicyNo, PayLocNo)
            cboDAPolicyNo.DataSource = ds
            cboDAPolicyNo.DataTextField = "tdesc"
            cboDAPolicyNo.DataValueField = "tNo"
            cboDAPolicyNo.DataBind()
            ds = Nothing

            If DAPolicyTypeNo > 0 Then
                cboDAPolicyNo.Enabled = True
                cboDACaseTypeNo.Text = "0"
            Else
                'To Populate Data of Case Type
                PopulateDAPolicy(0)
            End If


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub cboDAPolicyNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        PopulateDAPolicy(Generic.ToInt(cboDAPolicyNo.SelectedValue))
    End Sub

    Private Sub PopulateDAPolicy(ByVal DAPolicyNo As Integer)
        Try

            'If DAPolicyNo > 0 Then
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDAPolicy_WebOne", UserNo, DAPolicyNo)
            For Each row As DataRow In dt.Rows
                'cboDACaseTypeNo.Text = Generic.ToInt(row("DACaseTypeNo"))
                txtDAPolicyDesc.Text = Generic.ToStr(row("DAPolicyDesc"))
            Next

            'cboDAPolicyNo.Enabled = True
            'cboDACaseTypeNo.Enabled = True
            'Else
            'cboDAPolicyNo.Enabled = False
            'cboDACaseTypeNo.Enabled = False
            'cboDAPolicyNo.Text = ""
            'cboDACaseTypeNo.Text = "0"
            'End If

        Catch ex As Exception

        End Try
    End Sub

End Class




