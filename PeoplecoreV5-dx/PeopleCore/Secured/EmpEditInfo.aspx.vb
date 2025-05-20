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
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))
        Next
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)
        If TransNo = 0 Then : ViewState("IsEnabled") = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If
        If Not IsPostBack Then
            If TransNo = 0 Then
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            End If
            PopulateData()
            PopulateTabHeader()
        End If
        EnabledControls()
        If UserNo = Generic.ToInt(txtUserNo.Text) Then
            btnModify.Visible = False
            lnkModify2.Visible = False

        Else
            btnModify.Visible = True
            lnkModify2.Visible = True
        End If


    End Sub


    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            ViewState("IsEnabled") = True
            EnabledControls()
            If 1 = 0 Then
                cboPositionNo.Enabled = False
                cboFacilityNo.Enabled = False
                'txtIsFacHead.Enabled = False
                cboUnitNo.Enabled = False
                'txtIsUniHead.Enabled = False
                cboDepartmentNo.Enabled = False
                'txtIsDepHead.Enabled = False
                cboGroupNo.Enabled = False
                'txtIsGroHead.Enabled = False
                cboDivisionNo.Enabled = False
                'txtIsDivHead.Enabled = False
                cboSectionNo.Enabled = False
                'txtIsSecHead.Enabled = False
                cboSalaryGradeNo.Enabled = False
                cboCostCenterNo.Enabled = False
                cboLocationNo.Enabled = False
                cboTaskNo.Enabled = False
                cboOTParameterNo.Enabled = False
            End If
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

        lnkSave3.Visible = IsEnabled
        lnkModify2.Visible = Not IsEnabled

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
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim Retval As Boolean = False
            Dim SSSNo As String = Generic.ToStr(txtSSSNo.Text)
            Dim TinNo As String = Generic.ToStr(txtTinNo.Text)
            Dim HDMFNo As String = Generic.ToStr(txtHDMFNo.Text)
            Dim HDMF2No As String = Generic.ToStr(txtHDMF2No.Text)
            Dim PHNo As String = Generic.ToStr(txtPHNo.Text)
            Dim BankAccountNo As String = Generic.ToStr(txtBankAccountNo.Text)
            Dim EmployeeClassNo As Integer = Generic.ToInt(cboEmployeeClassNo.SelectedValue)
            Dim PayTypeNo As Integer = Generic.ToInt(cboPayTypeNo.SelectedValue)
            Dim PayClassNo As Integer = Generic.ToInt(cboPayClassNo.SelectedValue)
            '10
            Dim EmployeeRateClassNo As Integer = Generic.ToInt(cboEmployeeRateClassNo.SelectedValue)
            Dim EmployeeStatNo As Integer = Generic.ToInt(cboEmployeeStatNo.SelectedValue)
            Dim JobGradeNo As Integer = Generic.ToInt(cboJobGradeNo.SelectedValue)
            Dim PositionNo As Integer = Generic.ToInt(cboPositionNo.SelectedValue)
            Dim DivisionNO As Integer = Generic.ToInt(cboDivisionNo.SelectedValue)
            Dim DepartmentNo As Integer = Generic.ToInt(cboDepartmentNo.SelectedValue)
            Dim SectionNo As Integer = Generic.ToInt(cboSectionNo.SelectedValue)
            Dim CostCenterNo As Integer = Generic.ToInt(cboCostCenterNo.SelectedValue)
            Dim ShiftNo As Integer = Generic.ToInt(Me.cboShiftNo.SelectedValue)
            Dim DayOffNo As Integer = Generic.ToInt(Me.cboDayOffNo.SelectedValue)
            '20
            Dim TaxExemptNo As Integer = Generic.ToInt(cboTaxExemptNo.SelectedValue)
            Dim ImmediateSuperiorNo As Integer = Generic.ToInt(hifSEmployeeNo.Value)
            Dim IsSupervisor As Boolean = Generic.ToBol(chkIsSupervisor.Checked)
            Dim HDMFEE As Decimal = Generic.ToDec(txtEmployeeHDMF.Text)
            Dim HDMFER As Decimal = Generic.ToDec(txtEmployerHDMF.Text)
            Dim PlantillaNo As Integer = Generic.ToInt(hifPlantillaNo.Value)
            Dim GroupNo As Integer = Generic.ToInt(cboGroupNo.SelectedValue)
            Dim LocationNo As Integer = Generic.ToInt(cboLocationNo.SelectedValue)
            Dim ProjectNo As Integer = Generic.ToInt(cboProjectNo.SelectedValue)
            Dim BankTypeNo As Integer = Generic.ToInt(cboBankTypeNo.SelectedValue)
            '30
            Dim IsFlatTax As Boolean = Generic.ToBol(chkIsFlatTax.Checked)
            Dim TaxPercentRate As Decimal = Generic.ToDec(Me.txtTaxPercentRate.Text)
            Dim RankNo As Integer = Generic.ToInt(cboRankNo.SelectedValue)
            Dim CommunityNo As Integer = 0 'Generic.ToInt(cboCommunityNo.SelectedValue)
            Dim DayOff2 As Integer = Generic.ToInt(Me.cboDayOffNo2.SelectedValue)
            Dim FacilityNo As Integer = Generic.ToInt(cboFacilityNo.SelectedValue)
            Dim BranchNo As Integer = Generic.ToInt(cboBranchNo.SelectedValue)
            Dim TaskNo As Integer = Generic.ToInt(cboTaskNo.SelectedValue)
            Dim WorkArea As String = Generic.ToStr(txtWorkArea.Text)
            Dim UnitNo As Integer = Generic.ToInt(cboUnitNo.SelectedValue)
            '40
            Dim DateHired As String = Generic.ToStr(txtHiredDate.Text)
            Dim RegularizedDate As String = Generic.ToStr(txtRegularizedDate.Text)
            Dim ProbStart As String = Generic.ToStr(txtProbeStartDate.Text)
            Dim ProbEnd As String = Generic.ToStr(txtProbeEndDate.Text)
            Dim SeparatedDate As String = Generic.ToStr(txtSeparatedDate.Text)
            Dim SuspendedDate As String = Generic.ToStr(txtSuspendedDate.Text)
            Dim IsSuspended As Boolean = Generic.ToBol(chkIsSuspendPay.Checked)
            Dim IsSeparated As Boolean = Generic.ToBol(chkIsSeparated.Checked)
            Dim IsBlacklisted As Boolean = Generic.ToBol(chkIsBlacklisted.Checked)
            Dim BlackListedDate As String = Generic.ToStr(txtBlackListedDate.Text)
            '50
            Dim PaymentTypeNo As Integer = Generic.ToInt(cboPaymentTypeNo.SelectedValue)
            Dim SalaryGradeNo As Integer = Generic.ToInt(cboSalaryGradeNo.SelectedValue)
            Dim StepIncrementNo As Integer = Generic.ToInt(Me.cboStep.SelectedValue)
            Dim IsSSSER As Boolean = Generic.ToBol(chkIsSSSPaNoByER.Checked)
            Dim IsPHER As Boolean = Generic.ToBol(chkIsPHPaNoByER.Checked)
            Dim IsHDMFER As Boolean = Generic.ToBol(chkIsHDMFPaNoByER.Checked)
            Dim IsPFPaNoByER As Boolean = Generic.ToBol(chkIsPFPaNoByER.Checked) 'False
            Dim IsIHPPaNoByER As Boolean = Generic.ToBol(chkIsPFPaNoByER.Checked) 'False
            Dim IsSuspendSSS As Boolean = Generic.ToBol(chkIsDontDeductSSS.Checked)
            Dim IsSuspendPHNo As Boolean = Generic.ToBol(chkIsDontDeductPH.Checked)
            Dim OTParameterNo As Integer = Generic.ToInt(cboOTParameterNo.SelectedValue)
            '60
            Dim IsSuspendHDMF As Boolean = Generic.ToBol(chkIsDontDeductHDMF.Checked)
            Dim IsDontDeductPF As Boolean = Generic.ToBol(chkIsDontDeductPF.Checked) 'False
            Dim IsDontDeductIHP As Boolean = Generic.ToBol(chkIsDontDeductPF.Checked) 'False
            Dim IsDontDeductTAx As Boolean = Generic.ToBol(chkIsDontDeductTAx.Checked)
            Dim RehireDate As String = Generic.ToStr(txtRehiredDate.Text)
            Dim IsUnion As Boolean = 0 '.ToBol(Me.chkIsUnion.Checked)
            Dim IsUnionOfficer As Boolean = 0 'Generic.ToBol(Me.chkIsUnionOfficer.Checked)
            Dim LocalNo As String = Generic.ToStr(txtLocalNo.Text)
            Dim OrienteeStartDate As String = Generic.ToStr(Me.txtOrienteeStartDate.Text)
            Dim OrienteeEndDate As String = Generic.ToStr(Me.txtOrienteeEndDate.Text)
            '70
            Dim MinTakeHomePay As Decimal = Generic.ToDec(Me.txtMinTakeHomePay.Text)
            Dim ActingPlantillaNo As Integer = Generic.ToInt(Me.hifActingPlantillaNo.Value)
            Dim xPayLocNo As Integer = Generic.ToDec(cboPayLocNo.SelectedValue)
            Dim MembershipTypeNo As Integer = Generic.ToInt(cboMembershipTypeNo.SelectedValue)
            Dim MembershipStatNo As Integer = Generic.ToInt(cboMembershipStatNo.SelectedValue)
            Dim MembershipClassNo As Integer = Generic.ToInt(cboMembershipClassNo.SelectedValue)

            Dim PFEE As Decimal = Generic.ToDec(txtEmployeePF.Text)
            Dim HFEE As Decimal = Generic.ToDec(txtEmployeePF.Text)

            Dim IsRata As Boolean = Generic.ToBol(chkIsRata.Checked)
            Dim IsTA As Boolean = Generic.ToBol(chkIsTA.Checked)

            Dim HousingClassNo As Integer = Generic.ToInt(cboHousingClassNo.SelectedValue)

            'Validate Here
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EEmployee_WebValidateEI", UserNo, TransNo, SSSNo, TinNo, HDMFNo, PHNo, BankAccountNo, EmployeeClassNo, PayTypeNo, PayClassNo, _
                                         EmployeeRateClassNo, EmployeeStatNo, JobGradeNo, PositionNo, DivisionNO, DepartmentNo, SectionNo, CostCenterNo, ShiftNo, DayOffNo, _
                                         TaxExemptNo, ImmediateSuperiorNo, IsSupervisor, HDMFEE, HDMFER, PlantillaNo, GroupNo, LocationNo, ProjectNo, BankTypeNo, _
                                         IsFlatTax, TaxPercentRate, RankNo, CommunityNo, DayOff2, FacilityNo, BranchNo, TaskNo, WorkArea, UnitNo, _
                                         DateHired, RegularizedDate, ProbStart, ProbEnd, SeparatedDate, SuspendedDate, IsSuspended, IsSeparated, IsBlacklisted, BlackListedDate, _
                                         PaymentTypeNo, SalaryGradeNo, StepIncrementNo, IsSSSER, IsPHER, IsHDMFER, IsPFPaNoByER, IsIHPPaNoByER, IsSuspendSSS, IsSuspendPHNo, _
                                         IsSuspendHDMF, IsDontDeductPF, IsDontDeductIHP, IsDontDeductTAx, RehireDate, IsUnion, IsUnionOfficer, LocalNo, OrienteeStartDate, OrienteeEndDate, _
                                         MinTakeHomePay, xPayLocNo, HDMF2No)

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("Invalid"))
                messagedialog = Generic.ToStr(rowx("MessageDialog"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If

            'Save Here
            If SQLHelper.ExecuteNonQuery("EEmployee_WebSaveEI", UserNo, TransNo, SSSNo, TinNo,
                                         HDMFNo, PHNo, BankAccountNo, EmployeeClassNo, PayTypeNo,
                                         PayClassNo, EmployeeRateClassNo, EmployeeStatNo, JobGradeNo,
                                         PositionNo, DivisionNO, DepartmentNo, SectionNo, CostCenterNo,
                                         ShiftNo, DayOffNo, TaxExemptNo, ImmediateSuperiorNo, IsSupervisor,
                                         HDMFEE, HDMFER, PlantillaNo, GroupNo, LocationNo, ProjectNo,
                                         BankTypeNo, IsFlatTax, TaxPercentRate, RankNo, CommunityNo, DayOff2,
                                         FacilityNo, BranchNo, TaskNo, WorkArea, UnitNo, DateHired,
                                         RegularizedDate, ProbStart, ProbEnd, SeparatedDate, SuspendedDate,
                                         IsSuspended, IsSeparated, IsBlacklisted, BlackListedDate,
                                         PaymentTypeNo, SalaryGradeNo, StepIncrementNo, IsSSSER, IsPHER,
                                         IsHDMFER, IsPFPaNoByER, IsIHPPaNoByER, IsSuspendSSS, IsSuspendPHNo,
                                         IsSuspendHDMF, IsDontDeductPF, IsDontDeductIHP, IsDontDeductTAx,
                                         RehireDate, IsUnion, IsUnionOfficer, LocalNo, OrienteeStartDate,
                                         OrienteeEndDate, MinTakeHomePay, ActingPlantillaNo, xPayLocNo,
                                         MembershipTypeNo, MembershipClassNo, MembershipStatNo, txtCompanyMobileNo.Text,
                                         txtCompanyEmail.Text, PFEE, IsRata, IsTA, txtCompanyTelNo.Text, txtFaxNo.Text, OTParameterNo, HDMF2No, HousingClassNo) > 0 Then
                Retval = True
            Else
                Retval = False
            End If


            If Retval Then
                Dim url As String = "EmpEditInfo.aspx?id=" & TransNo
                MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub



    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function PopulateItemNoInfo(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim UserNo As Integer = 0, PayLocNo As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        PayLocNo = (HttpContext.Current.Session("xPayLocNo"))

        ds = SQLHelper.ExecuteDataSet("EHRAN_WebLookup_AC_Plantilla", UserNo, prefixText, count, PayLocNo)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("PlantillaCode")),
                                Generic.ToStr(row("PlantillaNo")) & _
                                "|" & IIf(Generic.ToStr(row("FacilityNo")) = "0", "", Generic.ToStr(row("FacilityNo"))) & _
                                "|" & IIf(Generic.ToStr(row("GroupNo")) = "0", "", Generic.ToStr(row("GroupNo"))) & _
                                "|" & IIf(Generic.ToStr(row("DepartmentNo")) = "0", "", Generic.ToStr(row("DepartmentNo"))) & _
                                "|" & IIf(Generic.ToStr(row("UnitNo")) = "0", "", Generic.ToStr(row("UnitNo"))) & _
                                "|" & IIf(Generic.ToStr(row("PositionNo")) = "0", "", Generic.ToStr(row("PositionNo"))) & _
                                "|" & IIf(Generic.ToStr(row("RMCNo")) = "0", "", Generic.ToStr(row("RMCNo"))) & _
                                "|" & IIf(Generic.ToStr(row("BranchNo")) = "0", "", Generic.ToStr(row("FacilityNo"))) & _
                                "|" & IIf(Generic.ToStr(row("LocationNo")) = "0", "", Generic.ToStr(row("LocationNo"))) & _
                                "|" & IIf(Generic.ToStr(row("TaskNo")) = "0", "", Generic.ToStr(row("TaskNo"))) & _
                                "|" & IIf(Generic.ToStr(row("TeamNo")) = "0", "", Generic.ToStr(row("TeamNo"))) & _
                                "|" & IIf(Generic.ToStr(row("DivisionNo")) = "0", "", Generic.ToStr(row("DivisionNo"))) & _
                                "|" & IIf(Generic.ToStr(row("CostCenterNo")) = "0", "", Generic.ToStr(row("CostCenterNo"))) & _
                                "|" & IIf(Generic.ToStr(row("ImmediateSuperiorNo")) = "0", "", Generic.ToStr(row("ImmediateSuperiorNo"))) & _
                                "|" & IIf(Generic.ToStr(row("SalaryGradeNo")) = "0", "", Generic.ToStr(row("SalaryGradeNo"))) & _
                                "|" & Generic.ToStr(row("IsFacHead")) & _
                                "|" & Generic.ToStr(row("IsGroHead")) & _
                                "|" & Generic.ToStr(row("IsDepHead")) & _
                                "|" & Generic.ToStr(row("IsDivHead")) & _
                                "|" & Generic.ToStr(row("IsUniHead")) & _
                                "|" & Generic.ToStr(row("IsSecHead")) & _
                                "|" & Generic.ToStr(row("PositionDescS")) & _
                                "|" & Generic.ToStr(row("ShiftNo")) & _
                                "|" & IIf(Generic.ToStr(row("SectionNo")) = "0", "", Generic.ToStr(row("SectionNo"))))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

End Class

