Imports System.Data
Imports Microsoft.VisualBasic
Imports clsLib

Partial Class SecuredManager_SelfDAAREdit
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim OnlineEmpNo As Integer = 0
    Dim clsGeneric As New clsGenericClass

    Private Sub PopulateData()

        Dim DAPolicyTypeNo As Integer = 0
        Dim DAPolicyNo As Integer = 0
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDAAR_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateDropDownList_Union(UserNo, Me, "ph1", dt, Generic.ToInt(Session("xPayLocNo")))
            DAPolicyTypeNo = Generic.ToInt(row("DAPolicyTypeNo"))
            DAPolicyNo = Generic.ToInt(row("DAPolicyNo"))
            ViewState("IsLock") = row("IsLock")
        Next

        Generic.PopulateData(Me, "ph1", dt)
        PopulateDAPolicyType(DAPolicyTypeNo, DAPolicyNo)
        PopulateDAPolicy(DAPolicyNo)


    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then
            ViewState("IsEnabled") = True
            cboApprovalStatNo.Text = 1
            hifComplainantNo.Value = OnlineEmpNo
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployee_WebOne", UserNo, OnlineEmpNo)
            For Each row As DataRow In dt.Rows
                txtComplainantName.Text = Generic.ToStr(row("FullName"))
            Next
        End If

        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        If Generic.ToBol(txtIsPosted.Checked) = True Then
            Enabled = False
        End If

        If Generic.ToInt(ViewState("IsLock")) = 1 Then
            Enabled = False
        End If

        'Disable once On-Process
        'If Generic.ToBol(chkIsNTESubmit.Checked) = True And cboApprovalStatNo.Text = 1 Then
        '    Enabled = False
        'End If

        Generic.EnableControls(Me, "ph1", Enabled)
        If TransNo = 0 Then
            If Generic.ToInt(cboDAPolicyTypeNo.SelectedValue) > 0 Then
                cboDAPolicyNo.Enabled = Enabled
            Else
                cboDAPolicyNo.Enabled = False
            End If

            If Generic.ToInt(cboDAPolicyNo.SelectedValue) > 0 Then
                cboDACaseTypeNo.Enabled = Enabled
            Else
                cboDACaseTypeNo.Enabled = False
            End If
        End If
        Generic.PopulateDataDisabled(Me, "ph1", UserNo, PayLocNo, Generic.ToStr(Session("xMenuType")))

        txtAssignedByName.Enabled = False
        txtReceivedEvalDate.Enabled = False
        txtBI.Enabled = False
        txtComments.Enabled = False
        txtRemarks.Enabled = False
        txtEvaluation.Enabled = False
        txtEvaluationByName.Enabled = False
        txtEvaluationDate.Enabled = False
        txtRecommendation.Enabled = False
        txtRecommendedByName.Enabled = False
        txtRecommendationDate.Enabled = False
        'chkIsForDA.Enabled = False
        txtReceivedByName.Enabled = False
        txtReceivedDate.Enabled = False
        txtApprovedByName.Enabled = False
        txtApprovedDate.Enabled = False

        txtComplainantName.Enabled = False
        cboApprovalStatNo.Enabled = False
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
        OnlineEmpNo = Generic.ToInt(Session("EmployeeNo"))
        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            Generic.PopulateDropDownList_Self(UserNo, Me, "ph1", Generic.ToInt(Session("xPayLocNo")))
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

    Protected Sub lnkModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkModify.Click

        If txtIsPosted.Checked Then
            MessageBox.Information(MessageTemplate.PostedTransaction, Me)
        Else
            ViewState("IsEnabled") = True
            EnabledControls()
        End If

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

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
        Dim Address As String = ""
        Dim StartDate As String = ""
        Dim EndDate As String = ""
        Dim AckDate As String = ""
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
        Dim IsForDA As Boolean = 0 'Generic.ToBol(chkIsForDA.Checked)
        Dim ERIncidentTypeNo As Integer = Generic.ToInt(cboERIncidentTypeNo.SelectedValue)
        Dim ERIncidentClassNo As Integer = Generic.ToInt(cboERIncidentClassNo.SelectedValue)

        If TransNo = 0 Then
            approvalStatNo = 0
        End If

        dt = SQLHelper.ExecuteDataTable("EDAAR_WebSaveSelf", UserNo, tno, employeeNo, DocketNo, DAARTypeNo, DACaseTypeNo, DAARDate, Entity, ComplainantNo, Complainant, [Address], StartDate, EndDate, AckDate, Amount, ReceivedByNo, ReceivedDate, AssignedByNo, ReceivedEvalDate, BI, Comments, Evaluation, EvaluationByNo, EvaluationDate, Recommendation, RecommendationDate, ApprovedByNo, ApprovedDate, DAARStatNo, Remarks, Session("xPayLocNo"), DAARLocation, DAARDesc, DAARHappen, DAARImpact, OccurenceDate, DAPolicyNo, DAPolicyTypeNo, approvalStatNo, RecommendedByNo, IsForDA, ERIncidentTypeNo, ERIncidentClassNo)
        For Each row As DataRow In dt.Rows
            TransNo = Generic.ToInt(row("Retval"))
            RetVal = True
        Next

        'If RetVal And tno = 0 Then
        '    SQLHelper.ExecuteNonQuery("EDAAR_WebApproved", UserNo, TransNo, 1, True)
        'End If

        If RetVal = True Then
            Dim url As String = "SelfDAAREdit.aspx?id=" & TransNo
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If



    End Sub

    Protected Sub cboDAPolicyTypeNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        PopulateDAPolicyType(Generic.ToInt(cboDAPolicyTypeNo.SelectedValue), 0)
    End Sub

    Private Sub PopulateDAPolicyType(ByVal DAPolicyTypeNo As Integer, ByVal DAPolicyNo As Integer)
        Try
            cboDAPolicyNo.Items.Clear()
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EDAPolicyType_WebLookup", UserNo, DAPolicyTypeNo, DAPolicyNo, PayLocNo)
            cboDAPolicyNo.DataSource = ds
            cboDAPolicyNo.DataTextField = "tdesc"
            cboDAPolicyNo.DataValueField = "tNo"
            cboDAPolicyNo.DataBind()
            ds = Nothing

            'If DAPolicyTypeNo > 0 Then
            '    cboDAPolicyNo.Enabled = True
            '    cboDACaseTypeNo.Text = "0"
            'Else
            '    'To Populate Data of Case Type
            '    'PopulateDAPolicy(0)
            'End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub cboDAPolicyNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        PopulateDAPolicy(Generic.ToInt(cboDAPolicyNo.SelectedValue))
    End Sub

    Private Sub PopulateDAPolicy(ByVal DAPolicyNo As Integer)
        'Try
        '    If DAPolicyNo > 0 Then
        '        Dim dt As DataTable
        '        dt = SQLHelper.ExecuteDataTable("EDAPolicy_WebOne", UserNo, DAPolicyNo)
        '        For Each row As DataRow In dt.Rows
        '            cboDACaseTypeNo.Text = Generic.ToInt(row("DACaseTypeNo"))
        '        Next
        '        cboDAPolicyNo.Enabled = True
        '        cboDACaseTypeNo.Enabled = True
        '    Else
        '        cboDAPolicyNo.Enabled = False
        '        cboDACaseTypeNo.Enabled = False
        '        cboDAPolicyNo.Text = ""
        '        cboDACaseTypeNo.Text = "0"
        '    End If
        'Catch ex As Exception
        'End Try
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDAPolicy_WebOne", UserNo, DAPolicyNo)
            For Each row As DataRow In dt.Rows                
                txtDAPolicyDesc.Text = Generic.ToStr(row("DAPolicyDesc"))
            Next
        Catch ex As Exception

        End Try
    End Sub



End Class




