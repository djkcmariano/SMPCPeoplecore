Imports clsLib
Imports System.Data

Partial Class Secured_EmpHRANTypeEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim clsGeneric As New clsGenericClass

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpHRANTypeList.aspx", "EHRANType") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EHRANType_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next
        Try
            cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
            cboPayLocNo.DataTextField = "tdesc"
            cboPayLocNo.DataValueField = "tNo"
            cboPayLocNo.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "EmpHRANTypeList.aspx", "EHRANType")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateData()
            PopulateTabHeader()
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

        Dim paysource As String = "", index As String = "1"
        If txtIsNewHired.Checked Or txtIsRehired.Checked Then
            paysource = "True"
            index = "1"
        ElseIf txtIsSeparated.Checked Or txtIsBlacklisted.Checked Then
            paysource = "False"
            index = "0"
        End If
        fRegisterStartupScript("Sript", "disableenable_behind('" + paysource.ToString + "','" + index + "');")
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRANType_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpHRANTypeList.aspx", "EHRANType") Then
            Dim RetVal As Boolean = False
            Dim dt As DataTable
            Dim HRANTypeCode As String = Generic.ToStr(Me.txtHRANTypeCode.Text)
            Dim HRANTypeDesc As String = Generic.ToStr(Me.txtHRANTypeDesc.Text)
            Dim Confirmation As String = Generic.ToStr(Me.txtConfirmation.Html)
            Dim IsNewHired As Boolean = Generic.ToBol(Me.txtIsNewHired.Checked)
            Dim IsRegularized As Boolean = Generic.ToBol(Me.txtIsRegularized.Checked)
            Dim IsSeparated As Boolean = Generic.ToBol(Me.txtIsSeparated.Checked)
            Dim IsUnique As Boolean = Generic.ToBol(Me.txtIsUnique.Checked)
            Dim IsSuspendPay As Boolean = Generic.ToBol(Me.txtIsSuspendPay.Checked)
            Dim IsActivePay As Boolean = Generic.ToBol(Me.txtIsActivePay.Checked)
            Dim IsViewSalary As Boolean = Generic.ToBol(Me.txtIsViewSalary.Checked)
            Dim Remarks As String = Generic.ToStr(Me.txtRemark.Html)
            Dim IsBlacklisted As Boolean = Generic.ToBol(Me.txtIsBlacklisted.Checked)
            Dim IsServiceRecord As Boolean = Generic.ToBol(Me.txtIsServiceRecord.Checked)
            Dim IsRehired As Boolean = Generic.ToBol(Me.txtIsRehired.Checked)
            Dim IsOrientee As Boolean = Generic.ToBol(Me.txtIsOrientee.Checked)
            Dim IsProbationary As Boolean = Generic.ToBol(Me.txtIsProbationary.Checked)
            Dim IsAutoPost As Boolean = Generic.ToBol(Me.txtIsAutoPost.Checked)
            Dim IsPromotion As Boolean = Generic.ToBol(Me.txtIsPromotion.Checked)
            Dim IsConferment As Boolean = Generic.ToBol(Me.txtIsConferment.Checked)
            Dim IsAppointmentReport As Boolean = Generic.ToBol(Me.txtIsAppointmentReport.Checked)
            Dim IsSalaryAdjustment As Boolean = Generic.ToBol(Me.txtIsSalaryAdjustment.Checked)
            Dim IsServicePrint As Boolean = 0 'Generic.ToBol(Me.txtIsServicePrint.Checked)
            Dim IsOnline As Boolean = Generic.ToBol(txtIsOnline.Checked)
            Dim noofScal As Integer = Generic.ToInt(txtNoOfScal.Text)
            Dim IsTenureContinue As Boolean = Generic.ToBol(txtIsTenureContinue.Checked)
            Dim IsYTDForwardToPreviousPayroll As Boolean = Generic.ToBol(txtIsYTDForwardToPreviousPayroll.Checked)
            Dim IsConcurrent As Boolean = Generic.ToBol(Generic.ToBol(txtIsConcurrent.Checked))
            Dim IsWithExitInterview As Boolean = Generic.ToBol(Generic.ToBol(txtIsWithExitInterview.Checked))

            Dim IsEmploymentRecord As Boolean = Generic.ToBol(Generic.ToBol(txtIsEmploymentRecord.Checked))
            Dim IsNot201 As Boolean = Generic.ToBol(Generic.ToBol(txtIsNot201.Checked))
            Dim IsAutoAbolish As Boolean = Generic.ToBol(Generic.ToBol(txtIsAutoAbolish.Checked))
            Dim IsContractPrep As Boolean = Generic.ToBol(txtIsContractPrep.Checked)
            Dim BIRCategorySepaNo As Integer = Generic.ToInt(cboBIRCategorySepaNo.SelectedValue)
            Dim IsArchived As Boolean = Generic.ToBol(chkIsArchived.Checked)

            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable, error_num As Integer = 0, error_message As String = ""
            dtx = SQLHelper.ExecuteDataTable("EHRANTYPE_WebValidate", UserNo, TransNo)

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("tProceed"))
                messagedialog = Generic.ToStr(rowx("xMessage"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If

            dt = SQLHelper.ExecuteDataTable("EHRANType_WebSave", UserNo, TransNo, HRANTypeCode, HRANTypeDesc, Confirmation, IsNewHired, IsRegularized, IsSeparated, IsUnique, IsSuspendPay, IsActivePay, IsViewSalary, Remarks, IsBlacklisted, IsServiceRecord, IsRehired, IsOrientee, IsProbationary, IsAutoPost, IsPromotion, IsAppointmentReport, IsConferment, IsSalaryAdjustment, IsServicePrint, PayLocNo, IsOnline, noofScal, IsTenureContinue, IsYTDForwardToPreviousPayroll, IsConcurrent, IsWithExitInterview, IsEmploymentRecord, IsNot201, IsAutoAbolish, IsContractPrep, BIRCategorySepaNo, IsArchived)
            For Each row As DataRow In dt.Rows
                TransNo = Generic.ToInt(row("HRANTypeNo"))
                RetVal = True
            Next

            If RetVal = True Then
                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim url As String = "EmpHRANTypeEdit.aspx?id=" & TransNo
                    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                Else
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                    ViewState("IsEnabled") = False
                    EnabledControls()
                End If
                PopulateTabHeader()
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If

        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub

End Class
