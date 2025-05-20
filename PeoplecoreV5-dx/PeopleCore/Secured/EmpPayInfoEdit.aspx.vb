Imports System.Data
Imports clsLib

Partial Class Secured_EmpEditInfo
    Inherits System.Web.UI.Page

    Dim TransNo As Int64
    Dim IsEnabled As Boolean = False
    Dim UserNo As Int64
    Dim PayLocNo As Integer

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployee_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "EmpPayInfoList.aspx")
        If TransNo = 0 Then : ViewState("IsEnabled") = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateData()
            PopulateTabHeader()
        End If
        EnabledControls()

    End Sub


    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpPayInfoList.aspx") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)
        Generic.PopulateDataDisabled(Me, "Panel1", UserNo, PayLocNo, Generic.ToStr(Session("xMenuType")))
        btnModify.Visible = Not IsEnabled
        btnSave.Visible = IsEnabled
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpPayInfoList.aspx") Then
            Dim Retval As Boolean = False

            Dim PayClassNo As Integer = Generic.ToInt(cboPayClassNo.SelectedValue)
            Dim PayTypeNo As Integer = Generic.ToInt(cboPayTypeNo.SelectedValue)
            Dim PaymentTypeNo As Integer = Generic.ToInt(cboPaymentTypeNo.SelectedValue)
            Dim TaxExemptNo As Integer = Generic.ToInt(cboTaxExemptNo.SelectedValue)
            Dim EmployeeRateClassNo As Integer = Generic.ToInt(cboEmployeeRateClassNo.SelectedValue)
            Dim BankTypeNo As Integer = Generic.ToInt(cboBankTypeNo.SelectedValue)
            Dim BankAccountNo As String = Generic.ToStr(txtBankAccountNo.Text)
            Dim SSSNo As String = Generic.ToStr(txtSSSNo.Text)
            '10
            Dim PHNo As String = Generic.ToStr(txtPHNo.Text)
            Dim HDMFNo As String = Generic.ToStr(txtHDMFNo.Text)
            Dim TinNo As String = Generic.ToStr(txtTinNo.Text)
            Dim IsSuspendSSS As Boolean = Generic.ToBol(chkIsDontDeductSSS.Checked)
            Dim IsSuspendPHNo As Boolean = Generic.ToBol(chkIsDontDeductPH.Checked)
            Dim IsSuspendHDMF As Boolean = Generic.ToBol(chkIsDontDeductHDMF.Checked)
            Dim IsDontDeductTax As Boolean = Generic.ToBol(chkIsDontDeductTAx.Checked)
            Dim IsSSSER As Boolean = Generic.ToBol(chkIsSSSPaNoByER.Checked)
            Dim IsPHER As Boolean = Generic.ToBol(chkIsPHPaNoByER.Checked)
            Dim IsHDMFER As Boolean = Generic.ToBol(chkIsHDMFPaNoByER.Checked)
            '20
            Dim HDMFEE As Decimal = Generic.ToDec(txtEmployeeHDMF.Text)
            Dim HDMFER As Decimal = Generic.ToDec(txtEmployerHDMF.Text)
            Dim IsFlatTax As Boolean = Generic.ToBol(chkIsFlatTax.Checked)
            Dim TaxPercentRate As Decimal = Generic.ToDec(Me.txtTaxPercentRate.Text)
            Dim IsSuspended As Boolean = Generic.ToBol(chkIsSuspendPay.Checked)
            Dim IsSeparated As Boolean = Generic.ToBol(chkIsSeparated.Checked)
            Dim IsUnion As Boolean = Generic.ToBol(Me.chkIsUnion.Checked)
            Dim IsUnionOfficer As Boolean = Generic.ToBol(Me.chkIsUnionOfficer.Checked)
            Dim ShiftNo As Integer = Generic.ToInt(Me.cboShiftNo.SelectedValue)
            Dim DayOffNo As Integer = Generic.ToInt(Me.cboDayOffNo.SelectedValue)
            '30
            Dim DayOff2 As Integer = Generic.ToInt(Me.cboDayOffNo2.SelectedValue)
            Dim MinTakeHomePay As Decimal = Generic.ToDec(Me.txtMinTakeHomePay.Text)
            Dim HDMFMIDNo As String = ""

            Dim IsPFPaNoByER As Boolean = False
            Dim IsIHPPaNoByER As Boolean = False
            Dim IsDontDeductPF As Boolean = False
            Dim IsDontDeductIHP As Boolean = False

            Dim OrienteeStartDate As String = Generic.ToStr(txtOrienteeStartDate.Text)
            Dim OrienteeEndDate As String = Generic.ToStr(txtOrienteeEndDate.Text)
            Dim ProbeStartDate As String = Generic.ToStr(txtProbeStartDate.Text)
            Dim ProbeEndDate As String = Generic.ToStr(txtProbeEndDate.Text)
            Dim HiredDate As String = Generic.ToStr(txtHiredDate.Text)
            Dim RehiredDate As String = Generic.ToStr(txtRehiredDate.Text)
            Dim RegularizedDate As String = Generic.ToStr(txtRegularizedDate.Text)
            Dim SeparatedDate As String = Generic.ToStr(txtSeparatedDate.Text)
            Dim SuspendedDate As String = Generic.ToStr(txtSuspendedDate.Text)
            Dim BlacklistedDate As String = Generic.ToStr(txtBlackListedDate.Text)

            'Save Here
            If SQLHelper.ExecuteNonQuery("EPayInfo_WebSave", UserNo, TransNo, PayClassNo, PayTypeNo, PaymentTypeNo, TaxExemptNo, EmployeeRateClassNo, BankTypeNo, BankAccountNo, SSSNo, _
                                        PHNo, HDMFNo, TinNo, IsSuspendSSS, IsSuspendPHNo, IsSuspendHDMF, IsDontDeductTax, IsSSSER, IsPHER, IsHDMFER, _
                                        HDMFEE, HDMFER, IsFlatTax, TaxPercentRate, IsSuspended, IsSeparated, IsUnion, IsUnionOfficer, ShiftNo, DayOffNo, _
                                        DayOff2, MinTakeHomePay, HDMFMIDNo, PayLocNo,
                                        OrienteeStartDate, OrienteeEndDate, ProbeStartDate, ProbeEndDate, HiredDate, RehiredDate, RegularizedDate, SeparatedDate, SuspendedDate, BlacklistedDate) > 0 Then
                Retval = True
            Else
                Retval = False
            End If


            If Retval Then
                Dim url As String = "EmpPayInfoEdit.aspx?id=" & TransNo
                MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

End Class

