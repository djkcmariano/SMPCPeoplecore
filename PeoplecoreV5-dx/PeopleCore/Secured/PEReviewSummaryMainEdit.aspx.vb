Imports clsLib
Imports System.Data
Imports System.IO
Imports System.Web

Partial Class Secured_PEReviewSummaryMainEdit
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PEReviewSummaryMainList.aspx", "EPEReviewSummaryMain") Then
            Dim Retval As Integer = 0
            Dim tno As Integer = Generic.ToInt(Me.txtPEReviewSummaryMainNo.Text)
            Dim PEReviewMainNo As Integer = Generic.ToInt(cboPEReviewMainNo.SelectedValue) '  0
            Dim PENormsNo As Integer = Generic.ToInt(Me.cboPENormsNo.SelectedValue)
            Dim Applicableyear As Integer = Generic.ToInt(Me.txtApplicableyear.Text)
            Dim PEPeriodNo As Integer = Generic.ToInt(Me.cboPEPeriodNo.SelectedValue)
            Dim isManual As Boolean = Generic.ToBol(txtIsManual.Checked)
            Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEReviewSummaryMain_WebSave", UserNo, tno, PEReviewMainNo, PENormsNo, Applicableyear, PEPeriodNo, isManual, Remarks, PayLocNo)
            For Each row As DataRow In dt.Rows
                Retval = Generic.ToInt(row("RetVal"))
            Next

            If Retval > 0 Then
                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim url As String = "PEReviewSummaryMainEdit.aspx?id=" & Retval
                    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                Else
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                    ViewState("IsEnabled") = False
                    EnabledControls()
                End If
                'PopulateTabHeader()
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PEReviewSummaryMainList.aspx", "EPEReviewSummaryMain") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEReviewSummaryMain_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))
        Next

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "PEReviewSummaryMainList.aspx", "EPEReviewSummaryMain")

        If Not IsPostBack Then
            If TransNo = 0 Then
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            End If

            PopulateTabHeader()
            PopulateData()
        End If

        EnabledControls()

    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", Enabled)
        txtCode.Enabled = False

        lnkModify.Visible = Not Enabled
        lnkSave.Visible = Enabled
    End Sub

    Private Function SaveRecord() As Integer
        Dim RetVal As Integer = 0
        Dim dt As DataTable
        Try
            'Dim tno As Integer = Generic.ToInt(Me.txtPEReviewSummaryMainNo.Text)
            'Dim Applicableyear As Integer = Generic.ToInt(Me.txtApplicableyear.Text)
            'Dim PEPeriodNo As Integer = Generic.ToInt(Me.cboPEPeriodNo.SelectedValue)
            'Dim PEStandardMainLNo As Integer = Generic.ToInt(Me.cboPEStandardMainLNo.SelectedValue)
            'Dim PEEvalPeriodNo As Integer = Generic.ToInt(Me.cboPEEvalPeriodNo.SelectedValue)
            'Dim PENormsNo As Integer = Generic.ToInt(Me.cboPENormsNo.SelectedValue)
            'Dim PECycleNo As Integer = Generic.ToInt(Me.cboPECycleNo.SelectedValue)
            'Dim PEEvalProcessTypeNo As Integer = Generic.ToInt(Me.cboPEEvalProcessTypeNo.SelectedValue)
            'Dim EmployeeStatno As Integer = 0
            'Dim StartDate As String = Generic.ToStr(Me.txtStartDate.Text)
            'Dim EndDate As String = Generic.ToStr(Me.txtEndDate.Text)
            'Dim IsApplyToAll As Boolean = True
            'Dim PEReviewSummaryMainCode As String = Generic.ToStr(Me.txtPEReviewSummaryMainCode.Text)
            'Dim PEReviewSummaryMainDesc As String = Generic.ToStr(Me.txtPEReviewSummaryMainDesc.Text)
            'Dim IsOnline As Boolean = Generic.ToBol(Me.txtIsOnline.Checked)
            'Dim PECompTypeNo As Integer = Generic.ToInt(Me.cboPECompTypeNo.SelectedValue)
            'Dim PEEvaluatorNo As Integer = Generic.ToInt(Me.cboPEEvaluatorNo.SelectedValue)

            'dt = SQLHelper.ExecuteDataTable("EPEReviewSummaryMain_WebSave", UserNo, tno, PEStandardMainLNo, PENormsNo, PEEvalPeriodNo, PEPeriodNo, PECycleNo, Applicableyear, EmployeeStatno, StartDate, EndDate, IsApplyToAll, PEEvalProcessTypeNo, PEReviewSummaryMainCode, PEReviewSummaryMainDesc, IsOnline, PECompTypeNo, PEEvaluatorNo, PayLocNo)
            'For Each row As DataRow In dt.Rows
            '    RetVal = Generic.ToInt(row("RetVal"))
            'Next
        Catch ex As Exception

        End Try
        Return RetVal
    End Function

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEReviewSummaryMain_WebTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
    End Sub



End Class
