Imports System.Data
Imports Microsoft.VisualBasic
Imports clsLib

Partial Class Secured_PayPreviousEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim IsEnabled As Boolean = False

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayPrevious_WebOne", UserNo, TransNo)
        Generic.PopulateData(Me, "Panel1", dt)

        If txtIsPreviousEmployer.Checked = True Then
            txtIsAdjustment.Checked = False
        Else
            txtIsAdjustment.Checked = True
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "PayPreviousList.aspx", "EPayPrevious")
        If TransNo = 0 Then : ViewState("IsEnabled") = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If
        If Not IsPostBack Then
            PopulateData()
        End If
        EnabledControls()
    End Sub

    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayPreviousList.aspx", "EPayPrevious") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateControls()

        If txtIsPreviousEmployer.Checked = True Then
            txtPrevEmployerName.Enabled = True
            txtPrevAddress.Enabled = True
            txtPrevTINNo.Enabled = True
        Else
            txtPrevEmployerName.Enabled = False
            txtPrevAddress.Enabled = False
            txtPrevTINNo.Enabled = False
        End If

    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)

        If IsEnabled = True Then
            PopulateControls()
        End If

        btnModify.Visible = Not IsEnabled
        btnSave.Visible = IsEnabled        
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        Dim StartDate As String = Generic.ToStr(Me.txtStartDate.Text)
        Dim EndDate As String = Generic.ToStr(Me.txtEndDate.Text)

        Dim dt As DataTable
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        dt = SQLHelper.ExecuteDataTable("EPayPrevious_WebValidate", UserNo, Generic.ToInt(txtPayPreviousNo.Text), StartDate, EndDate, PayLocNo)
        For Each row As DataRow In dt.Rows
            invalid = Generic.ToBol(row("Invalid"))
            messagedialog = Generic.ToStr(row("MessageDialog"))
            alerttype = Generic.ToStr(row("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If

        TransNo = SaveRecord()

        If Generic.ToInt(Request.QueryString("id")) = 0 Then
            Dim xURL As String = "PayPreviousList.aspx?id=" & TransNo
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, xURL)
        Else
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            ViewState("IsEnabled") = False
            EnabledControls()
        End If
        PopulateData()

    End Sub

    Private Function SaveRecord() As Integer
        Dim EmployeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))
        Dim ApplicableYear As Integer = Generic.ToInt(Me.txtApplicableYear.Text)
        Dim TotalBasicIncome As Decimal = Generic.ToDec(Me.txtTotalBasicIncome.Text)
        Dim TotaloneTimeTaxableIncome As Decimal = Generic.ToDec(Me.txtTotalOneTimeTaxableIncomeOther.Text)
        Dim TotalNonTaxableIncome As Decimal = Generic.ToDec(Me.txtTotalNontaxableIncomeOther.Text)
        Dim TaxExemption As Decimal = Generic.ToDec(Me.txtTaxExemption.Text)
        Dim TaxWithheld As Decimal = Generic.ToDec(Me.txtTaxWithheld.Text)
        Dim Bonus As Decimal = Generic.ToDec(Me.txtBonus.Text)
        Dim HazardPay As Decimal = Generic.ToDec(Me.txtHazardpay.Text)
        Dim Commission As Decimal = Generic.ToDec(Me.txtCommission.Text)
        Dim DirFee As Decimal = Generic.ToDec(Me.txtDirFee.Text)
        Dim OTPay As Decimal = Generic.ToDec(Me.txtOTPay.Text)
        Dim NP As Decimal = Generic.ToDec(Me.txtNP.Text)
        Dim HolidayPay As Decimal = Generic.ToDec(Me.txtHolidayPay.Text)
        Dim Profit As Decimal = Generic.ToDec(Me.txtProfit.Text)
        Dim RepAllow As Decimal = Generic.ToDec(Me.txtRepAllow.Text)
        Dim COLA As Decimal = Generic.ToDec(Me.txtCola.Text)
        Dim HousingAllow As Decimal = Generic.ToDec(Me.txtHousingAllow.Text)
        Dim transpoallow As Decimal = Generic.ToDec(Me.txttranspoAllow.Text)
        Dim totalaccumIncome As Decimal = Generic.ToDec(Me.txttotalaccumincome.Text)
        Dim Deminimis As Decimal = Generic.ToDec(Me.txtDeminimis.Text)
        Dim obj As Object
        obj = SQLHelper.ExecuteScalar("EpayPrevious_WebSave", UserNo, Generic.ToInt(txtPayPreviousNo.Text), EmployeeNo, ApplicableYear, txtStartDate.Text, txtEndDate.Text, txtDescription.Text, TotalBasicIncome, TotaloneTimeTaxableIncome, TotalNonTaxableIncome, TaxExemption, TaxWithheld, Bonus, chkIsAdjustment.Checked, txtIsPreviousEmployer.Checked, txtPrevEmployerName.Text, txtPrevAddress.Text, txtPrevTINNo.Text, False, HazardPay, Commission, DirFee, OTPay, Profit, RepAllow, COLA, HousingAllow, transpoallow, totalaccumIncome, Deminimis, NP, HolidayPay, PayLocNo)
        Return Generic.ToInt(obj)
    End Function

    Protected Sub PreviousEmployer_CheckedChanged(sender As Object, e As EventArgs)
        PopulateControls()
    End Sub

End Class




