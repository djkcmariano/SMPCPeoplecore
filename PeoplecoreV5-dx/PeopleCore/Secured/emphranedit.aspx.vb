Imports System.Data
Imports Microsoft.VisualBasic
Imports clsLib


Partial Class Secured_EmpHRANEdit
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim tmodify As Boolean = False
    Dim TransNo As Integer = 0
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim clsGeneric As New clsGenericClass


    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "emphranlist.aspx", "EHRAN") Then
            ViewState("IsEnabled") = True
            If txtIsPosted.Checked Then
                MessageBox.Information(MessageTemplate.PostedTransaction, Me)
            End If
            EnabledControls()
            chkxIsNot201.Enabled = False
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("onlineuserno"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.CheckDBNull(Request.QueryString("id"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Session("itemno") = Generic.ToInt(Request.QueryString("ItemNo"))
        AccessRights.CheckUser(UserNo, "emphranlist.aspx", "EHRAN")

        If Generic.ToStr(Session("xMenuType")) = "0269000000" Then 'hran transfer
            'cboHRANTypeNo.Text = 5
            Dim HRANTypeNo As Integer = Generic.ToInt(SQLHelper.ExecuteScalar("SELECT TOP 1 HRANTypeNo FROM EHRANType WHERE PayLocNo IN (0," & PayLocNo & ") AND ISNULL(IsTransfer,0)=1"))
            cboHRANTypeNo.Text = IIf(HRANTypeNo > 0, HRANTypeNo.ToString(), "")
        End If

        If Not IsPostBack Then
            If TransNo = 0 Then
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
                cboApprovalStatNo.SelectedValue = "2"
            End If
            PopulateDropdown()
            PopulateData()
            PopulateTabHeader()

            If Session("itemno") > 0 Then
                hifPlantillaNo.Value = Session("itemno")
                'PopulateItemNo(Generic.ToInt(hifPlantillaNo.Value))
            End If
        End If
        EnabledControls()
        Me.chkxIsNot201.Enabled = False
        fRegisterStartupScript("JSDialogResponse", "SalaryPermission();")

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateItemNo(ByVal PlantillaNo As Integer)
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EPlantilla_WebOne", UserNo, PlantillaNo)
        For Each row As DataRow In dt.Rows
            Try
                cboGroupNo.Text = Generic.ToStr(row("GroupNo"))
                cboDivisionNo.Text = Generic.ToStr(row("DivisionNo"))
                cboDepartmentNo.Text = Generic.ToStr(row("DepartmentNo"))
                cboFacilityNo.Text = Generic.ToStr(row("FacilityNo"))
                cboSectionNo.Text = Generic.ToStr(row("SectionNo"))

                txtIsGroHead.Checked = Generic.ToBol(row("IsGroHead"))
                txtIsDivHead.Checked = Generic.ToBol(row("IsDivHead"))
                txtIsDepHead.Checked = Generic.ToBol(row("IsDepHead"))
                txtIsFacHead.Checked = Generic.ToBol(row("IsFacHead"))
                txtIsSecHead.Checked = Generic.ToBol(row("IsSecHead"))

                txtIsSupervisor.Checked = Generic.ToBol(row("IsSupervisor"))
                hifImmediateSuperiorNo.Value = Generic.ToStr(row("ImmediateSuperiorNo"))
                txtSFullName.Text = Generic.ToStr(row("SFullName"))

                cboJobGradeNo.Text = Generic.ToStr(row("JobGradeNo"))
                cboLocationNo.Text = Generic.ToStr(row("LocationNo"))
                cboCostCenterNo.Text = Generic.ToInt(row("CostCenterNo"))
            Catch ex As Exception

            End Try
           


        Next
    End Sub

    Protected Sub lstPlantilla_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateItemNo(Generic.ToInt(lstPlantilla.SelectedValue))
    End Sub

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
    Private Sub PopulateDropdown()

        Try
            cboHRANTypeNo.DataSource = SQLHelper.ExecuteDataSet("EHRANType_WebLookup_UnionAll", UserNo, 0, PayLocNo)
            cboHRANTypeNo.DataTextField = "tDesc"
            cboHRANTypeNo.DataValueField = "tNo"
            cboHRANTypeNo.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboClearanceTemplateNo.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EClearanceTemplate", PayLocNo, "", "")
            cboClearanceTemplateNo.DataTextField = "tdesc"
            cboClearanceTemplateNo.DataValueField = "tNo"
            cboClearanceTemplateNo.DataBind()
        Catch ex As Exception
        End Try

        

    End Sub
    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EHRAN_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            populateHRANReason(Generic.ToInt(row("HRANTypeNo")))
            populateHRANCorrected(Generic.ToInt(row("EmployeeNo")))
            Generic.PopulateData(Me, "Panel1", dt)
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))
            Try
                cboHRANTypeNo.DataSource = SQLHelper.ExecuteDataSet("EHRANType_WebLookup_UnionAll", UserNo, Generic.ToInt(row("HRANTypeNo")), PayLocNo)
                cboHRANTypeNo.DataTextField = "tDesc"
                cboHRANTypeNo.DataValueField = "tNo"
                cboHRANTypeNo.DataBind()
            Catch ex As Exception
            End Try

            PopulateData_HRANType(Generic.ToInt(Me.cboHRANTypeNo.SelectedValue))

            If txtIsActive.Checked Then
                AutoCompleteExtenderHRAN.CompletionSetCount = 3
                hifEmployeeNo.Value = Generic.ToInt(row("EmployeeNo"))
            ElseIf txtIsSeparated.Checked Then
                AutoCompleteExtenderHRAN.CompletionSetCount = 2
                hifEmployeeNo.Value = Generic.ToInt(row("EmployeeNo"))
            ElseIf txtIsApplicant.Checked Then
                AutoCompleteExtenderHRAN.CompletionSetCount = 1
                hifEmployeeNo.Value = Generic.ToInt(row("ApplicantNo"))
            Else
                AutoCompleteExtenderHRAN.CompletionSetCount = 3
                hifEmployeeNo.Value = Generic.ToInt(row("EmployeeNo"))
                txtIsActive.Checked = True
            End If

            cboApplicantStandardHeaderNo.Text = Generic.ToInt(row("ApplicantStandardHeaderNo"))

        Next

        If Generic.ToInt(cboPositionNo.SelectedValue) > 0 Then
            PopulateCombo()
        End If
        'lstPlantilla.SelectedValue = hifPlantillaNo.Value

        If TransNo = 0 Then
            txtIsActive.Checked = True
            AutoCompleteExtenderHRAN.CompletionSetCount = 3
            Me.txtPreparationDate.Text = Now.ToShortDateString
        End If

    End Sub
    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))

        If txtIsPosted.Checked = True Then
            Enabled = False

        End If

        If Generic.ToStr(Session("xMenuType")) = "0269000000" Then 'hran transfer
            Generic.EnableControls(Me, "Panel1", False)
            cboSectionNo.Enabled = Enabled
            txtFullName.Enabled = Enabled
            txtEffectivity.Enabled = Enabled
        Else
            Generic.EnableControls(Me, "Panel1", Enabled)
        End If

        txtHRANCode.Enabled = False
        Me.txtIncumbent.Enabled = False
        Me.txtIncumbentPosition.Enabled = False
        'Me.cboHRANCorrectedNo.Enabled = False
        Me.txtPreparationDate.Enabled = False
        lnkModify.Visible = Not Enabled
        lnkSave.Visible = Enabled

        If TransNo = 0 Then
            cboApprovalStatNo.Enabled = False
        Else
            cboApprovalStatNo.Enabled = Enabled
        End If

        'Salary Permission and View
        Dim _dt As DataTable
        Dim IsSalaryAdjust As Boolean = False
        _dt = SQLHelper.ExecuteDataTable("EHRAN_SalaryPermission", UserNo, Generic.ToBol(txtIsApplicant.Checked), Generic.ToInt(hifEmployeeNo.Value), Generic.ToInt(cboHRANTypeNo.SelectedValue))
        For Each row As DataRow In _dt.Rows
            txtIsViewSalary.Checked = Generic.ToBol(row("IsViewSalary"))
            txtIsEditSalary.Checked = Generic.ToBol(row("IsEditSalary"))
            IsSalaryAdjust = Generic.ToBol(row("IsSalaryAdjust"))
        Next

        If Enabled = True Then
            txtIsSalaryAdjust.Checked = IsSalaryAdjust
        Else
            txtIsSalaryAdjust.Checked = False
        End If
        'disable enable ready for posting button
        If TransNo > 0 Then
            If Enabled Then
                Dim ds As DataSet
                ds = SQLHelper.ExecuteDataSet("EHRAN_Web_ReadyStatus", UserNo, TransNo)
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        txtIsPosting.Enabled = Generic.ToBol(ds.Tables(0).Rows(0)("retval"))
                    End If
                End If
                ds = Nothing
            End If
        Else
            txtIsPosting.Enabled = False
        End If
        'Temporarily Open
        'cboPositionNo.Enabled = False
        'cboFacilityNo.Enabled = False
        'txtIsFacHead.Enabled = False
        'cboUnitNo.Enabled = False
        'txtIsUniHead.Enabled = False
        'cboDepartmentNo.Enabled = False
        'txtIsDepHead.Enabled = False
        'cboGroupNo.Enabled = False
        'txtIsGroHead.Enabled = False
        'cboDivisionNo.Enabled = False
        'txtIsDivHead.Enabled = False
        'cboSectionNo.Enabled = False
        'txtIsSecHead.Enabled = False
        'cboSalaryGradeNo.Enabled = False
        'cboCostCenterNo.Enabled = False
        'cboLocationNo.Enabled = False
        'cboTaskNo.Enabled = False
        'txtSFullName.Enabled = False
        'end
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRAN_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "emphranlist.aspx", "EHRAN") Then
            Dim RetVal As Boolean = False
            Dim dt As DataTable

            Dim HRANNo As Integer = Generic.ToInt(Me.txtHRANNo.Text)
            Dim EmployeeNo As Integer = Generic.ToInt(Me.hifEmployeeNo.Value)
            Dim EmployeeCode As String = Generic.ToStr(txtEmployeeCode.Text)
            Dim HRANTypeNo As Integer = Generic.ToInt(Me.cboHRANTypeNo.SelectedValue)
            Dim Reason As String = Generic.ToStr(txtReason.Text)
            Dim PreparationDate As String = Generic.ToStr(txtPreparationDate.Text)
            Dim Effectivity As String = Generic.ToStr(txtEffectivity.Text)
            Dim Description As String = Generic.ToStr(txtDescription.Text)
            Dim DueDate As String = Generic.ToStr(txtDueDate.Text)
            '10
            hifpositionNo.Value = Generic.ToInt(cboPositionNo.SelectedValue)
            Dim PositionNo As Integer = cboPositionNo.SelectedValue ' Generic.ToInt(Me.hifpositionNo.Value)

            Dim TaskNo As Integer = Generic.ToInt(Me.cboTaskNo.SelectedValue)
            Dim DivisionNo As Integer = Generic.ToInt(Me.cboDivisionNo.SelectedValue)
            Dim DepartmentNo As Integer = Generic.ToInt(Me.cboDepartmentNo.SelectedValue)
            Dim SectionNo As Integer = Generic.ToInt(Me.cboSectionNo.SelectedValue)
            Dim DayOffNo As Integer = Generic.ToInt(Me.cboDayOffNo.SelectedValue)
            Dim ShiftNo As Integer = Generic.ToInt(Me.cboShiftNo.SelectedValue)
            Dim EmployeeClassNo As Integer = Generic.ToInt(Me.cboEmployeeClassNo.SelectedValue)
            Dim EmployeeRateClassNo As Integer = Generic.ToInt(Me.cboEmployeeRateClassNo.SelectedValue)
            Dim EmployeeStatNo As Integer = Generic.ToInt(Me.cboEmployeeStatNo.SelectedValue)
            '20
            Dim PayTypeNo As Integer = Generic.ToInt(Me.cboPayTypeNo.SelectedValue)
            Dim xPayLocNo As Integer = Generic.ToInt(Me.cboPayLocNo.SelectedValue)
            Dim CurrentSalary As Double = Generic.ToDec(Me.txtCurrentSalary.Text)
            Dim ImmediateSuperiorNo As Integer = Generic.ToInt(Me.hifImmediateSuperiorNo.Value)
            Dim IsSupervisor As Boolean = Generic.ToBol(Me.txtIsSupervisor.Checked)
            Dim IsReady As Boolean = Generic.ToBol(Me.txtIsPosting.Checked)
            Dim PreparedByNo As Integer = UserNo
            Dim LocationNo As Integer = Generic.ToInt(Me.cboLocationNo.SelectedValue)
            Dim SalaryGradeNo As Integer = Generic.ToInt(Me.cboSalaryGradeNo.SelectedValue)
            'Me.hifPlantillaNo.Value = Generic.ToInt(hifPlantillaNo.Value) 'Generic.ToInt(lstPlantilla.SelectedValue)
            Dim PlantillaNo As Integer = Generic.ToInt(Me.hifPlantillaNo.Value)
            '30
            Dim StepNo As Integer = Generic.ToInt(Me.cboStep.SelectedValue)
            Dim PayClassNo As Integer = Generic.ToInt(Me.cboPayClassNo.SelectedValue)
            Dim Fullname As String = Generic.ToStr(txtFullName.Text)
            Dim IsApplicant As Boolean = Generic.ToBol(txtIsApplicant.Checked)
            Dim TaxExemptNo As Integer = Generic.ToInt(Me.cboTaxExemptNo.SelectedValue)
            Dim RankNo As Integer = Generic.ToInt(Me.cboRankNo.SelectedValue)
            Dim HRANRCNo As Integer = Generic.ToInt(Me.cboHRANRCNo.SelectedValue)
            Dim DayOffNo2 As Integer = Generic.ToInt(Me.cboDayOffNo2.SelectedValue)
            Dim FacilityNo As Integer = Generic.ToInt(Me.cboFacilityNo.SelectedValue)
            Dim UnitNo As Integer = Generic.ToInt(Me.cboUnitNo.SelectedValue)
            '40
            Dim PaymentTypeNo As Integer = Generic.ToInt(Me.cboPaymentTypeNo.SelectedValue)
            Dim CostCenterNo As Integer = Generic.ToInt(Me.cboCostCenterNo.SelectedValue)
            Dim GroupNo As Integer = Generic.ToInt(Me.cboGroupNo.SelectedValue)
            Dim pwd As String = ""
            Dim LS As Integer = Generic.ToInt(Me.txtLS.Text)
            Dim HRANCorrectedNo As Integer = Generic.ToInt(Me.cboHRANCorrectedNo.SelectedValue)
            Dim IsFacHead As Boolean = Generic.ToBol(Me.txtIsFacHead.Checked)
            Dim IsDivHead As Boolean = Generic.ToBol(Me.txtIsDivHead.Checked)
            Dim IsDepHead As Boolean = Generic.ToBol(Me.txtIsDepHead.Checked)
            Dim IsSecHead As Boolean = Generic.ToBol(Me.txtIsSecHead.Checked)
            '50
            Dim IsGroHead As Boolean = Generic.ToBol(Me.txtIsGroHead.Checked)
            Dim IsUniHead As Boolean = Generic.ToBol(Me.txtIsUniHead.Checked)
            Dim macAddress = "" 'Generic.ToStr(clsLib.getMacAddress())
            Dim ipAddress = "" 'Generic.ToStr(clsLib.getIPAddress())
            Dim hostName = "" 'Generic.ToStr(clsLib.getHostname())
            Dim IsSeparated As Boolean = Generic.ToBol(txtIsSeparated.Checked)
            Dim ProjectNo As Integer = Generic.ToInt(Me.cboProjectNo.SelectedValue)
            Dim IsDontDeductTax As Boolean = Generic.ToBol(Me.txtIsDontDeductTax.Checked)
            Dim AllowTempNo As Integer = Generic.ToInt(Me.cboAllowTempNo.SelectedValue)
            Dim ActingPlantillaNo As Integer = Generic.ToInt(Me.hifActingPlantillaNo.Value)
            '60
            Dim OfficeOrderNum As String = Generic.ToStr(Me.txtHRANOfficeOrderNo.Text)
            Dim IsConferment As Boolean = Generic.ToBol(Me.txtIsConferment.Checked)
            Dim AccountCode As String = Generic.ToStr(Me.txtBranchAccountCode.Text)
            Dim DatePublished As String = Generic.ToStr(Me.txtDatePub.Text)
            Dim AgencyHead As String = "" 'Generic.ToInt(Me.cboHRANHOANo.SelectedValue)
            Dim AgencyHeadDesignation As String = Generic.ToStr(Me.txtDesignation.Text)
            Dim PSBHead As String = Generic.ToStr(Me.txtPSBHead.Text)
            Dim HRHead As String = Generic.ToStr(Me.txtHRHead.Text)
            Dim PublicationNo As Integer = Generic.ToInt(Me.cboPublicationLNo.SelectedValue)
            Dim HRANHOANo As Integer = Generic.ToInt(Me.cboHRANHOANo.SelectedValue)
            '70
            Dim HRANHOADNo As Integer = Generic.ToInt(Me.cboHRANHOADNo.SelectedValue)
            Dim HRANHRMONo As Integer = Generic.ToInt(Me.cboHRANHRMONo.SelectedValue)
            Dim HRANPSBNo As Integer = Generic.ToInt(Me.cboHRANPSBNo.SelectedValue)
            Dim RMCNo As Integer = Generic.ToInt(Me.cboRMCNo.SelectedValue)
            Dim BranchNo As Integer = Generic.ToInt(Me.cboBranchNo.SelectedValue)
            Dim TeamNo As Boolean = False ' Generic.ToBol(Me.cboTeamNo.SelectedValue)
            Dim IsRMCHead As Boolean = False 'Generic.ToBol(Me.txtIsRMCHead.Checked)
            Dim IsBranchHead As Boolean = False 'Generic.ToBol(Me.txtIsBranchHead.Checked)
            Dim IsTeamHead As Boolean = False 'Generic.ToBol(Me.txtIsTeamHead.Checked)
            Dim DateofApproval As String = Generic.ToStr(Me.txtDateofApproval.Text)
            '80
            Dim IsForRata As Boolean = Generic.ToBol(Me.chkIsForRata.Checked)
            Dim JobGradeNo As Integer = Generic.ToInt(Me.cboJobGradeNo.SelectedValue)
            Dim HRANTypeReasonNo As Integer = Generic.ToInt(Me.cboHRANTypeReasonNo.SelectedValue)
            Dim ApprovalStatNo As Integer = Generic.ToInt(Me.cboApprovalStatNo.SelectedValue)
            Dim MembershipTypeNo As Integer = Generic.ToInt(cboMembershipTypeNo.SelectedValue)
            Dim MembershipStatNo As Integer = Generic.ToInt(cboMembershipStatNo.SelectedValue)
            Dim MembershipClassNo As Integer = Generic.ToInt(cboMembershipClassNo.SelectedValue)

            Dim IsRata As Boolean = Generic.ToBol(chkIsRata.Checked)
            Dim IsTA As Boolean = Generic.ToBol(chkIsTA.Checked)

            '//validate start here
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            'Dim str As String = UserNo & ", " & HRANNo  EmployeeNo & ", " & EmployeeCode, HRANTypeNo, Effectivity, LS, DueDate, FacilityNo, PayClassNo, JobGradeNo, PlantillaNo, EmployeeStatNo, EmployeeRateClassNo, CurrentSalary, ApprovalStatNo, IsReady, IsSeparated, IsApplicant, IsDontDeductTax, PayLocNo
            dtx = SQLHelper.ExecuteDataTable("EHRAN_WebValidate", UserNo, HRANNo, EmployeeNo, EmployeeCode, HRANTypeNo, Effectivity, LS, DueDate, FacilityNo, PayClassNo, JobGradeNo, PlantillaNo, EmployeeStatNo, EmployeeRateClassNo, CurrentSalary, ApprovalStatNo, IsReady, IsSeparated, IsApplicant, IsDontDeductTax, PayLocNo)

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("Invalid"))
                messagedialog = Generic.ToStr(rowx("MessageDialog"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If

            dt = SQLHelper.ExecuteDataTable("EHRAN_WebSave", UserNo, HRANNo, EmployeeNo, EmployeeCode, HRANTypeNo, Reason, PreparationDate, Effectivity, Description, DueDate, _
                                                        PositionNo, TaskNo, DivisionNo, DepartmentNo, SectionNo, DayOffNo, ShiftNo, EmployeeClassNo, EmployeeRateClassNo, EmployeeStatNo, _
                                                        PayTypeNo, PayLocNo, CurrentSalary, ImmediateSuperiorNo, IsSupervisor, IsReady, PreparedByNo, LocationNo, SalaryGradeNo, PlantillaNo, _
                                                        StepNo, PayClassNo, Fullname, IsApplicant, TaxExemptNo, RankNo, HRANRCNo, DayOffNo2, FacilityNo, UnitNo, _
                                                        PaymentTypeNo, CostCenterNo, GroupNo, pwd, LS, HRANCorrectedNo, IsFacHead, IsDivHead, IsDepHead, IsSecHead, _
                                                        IsGroHead, IsUniHead, ipAddress, hostName, macAddress, IsSeparated, ProjectNo, IsDontDeductTax, AllowTempNo, ActingPlantillaNo, _
                                                        OfficeOrderNum, IsConferment, AccountCode, DatePublished, AgencyHead, AgencyHeadDesignation, PSBHead, HRHead, PublicationNo, HRANHOANo, _
                                                        HRANHOADNo, HRANHRMONo, HRANPSBNo, RMCNo, BranchNo, TeamNo, IsRMCHead, IsBranchHead, IsTeamHead, DateofApproval, _
                                                        IsForRata, JobGradeNo, HRANTypeReasonNo, ApprovalStatNo, MembershipTypeNo, MembershipClassNo, MembershipStatNo, _
                                                        Generic.ToInt(cboApplicantStandardHeaderNo.SelectedValue), IsRata, IsTA, Generic.ToInt(cboClearanceTemplateNo.SelectedValue))

            Dim forEscalation As Integer = 0
            Dim IsAdd As Boolean = False
            For Each row As DataRow In dt.Rows
                TransNo = Generic.ToInt(row("HRANNo"))
                forEscalation = Generic.ToInt(row("ForEscalation"))
                IsAdd = Generic.ToBol(row("IsAdd"))
                RetVal = True
            Next

            If RetVal = True Then
                If forEscalation = 1 Then
                    Dim URL As String = Request.Url.GetLeftPart(UriPartial.Authority) & "/frmEmailApproved.aspx"
                    SQLHelper.ExecuteNonQuery("EHRAN_Web_Approved", UserNo, TransNo, 1, IsAdd, 0, URL)
                End If
                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim url As String = "EmpHRANEdit.aspx?id=" & TransNo
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
            'End If


        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub
    Protected Sub hifEmployeeNo_ValueChanged(sender As Object, e As System.EventArgs)
        Try
            populateHRANCorrected(Generic.ToInt(Me.hifEmployeeNo.Value))
        Catch ex As Exception

        End Try
    End Sub
    Private Sub populateHRANCorrected(employeeNo As Integer)
        Try
            cboHRANCorrectedNo.DataSource = SQLHelper.ExecuteDataSet("EHRAN_WebLookup_Corrected", UserNo, employeeNo, Session("xPayLocNo"))
            cboHRANCorrectedNo.DataTextField = "tDesc"
            cboHRANCorrectedNo.DataValueField = "tNo"
            cboHRANCorrectedNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub hifHRANTypeNo_ValueChanged(sender As Object, e As System.EventArgs)
        Try
            populateHRANReason(Generic.ToInt(Me.cboHRANTypeNo.SelectedValue))
            PopulateData_HRANType(Generic.ToInt(Me.cboHRANTypeNo.SelectedValue))
            If Generic.ToInt(cboHRANTypeNo.SelectedValue) = 13 Then
                populateHRANCorrected(hifEmployeeNo.Value)
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub populateHRANReason(hrantypeno As Integer)
        Try
            cboHRANTypeReasonNo.DataSource = SQLHelper.ExecuteDataSet("EHRANTypeReason_WebLookup", UserNo, hrantypeno)
            cboHRANTypeReasonNo.DataTextField = "tDesc"
            cboHRANTypeReasonNo.DataValueField = "tNo"
            cboHRANTypeReasonNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub PopulateData_HRANType(hrantypeNo As Integer)
        Dim _dt As DataTable
        Dim IsSalaryAdjust As Boolean = False
        _dt = SQLHelper.ExecuteDataTable("EHRAN_SalaryPermission", UserNo, Generic.ToBol(txtIsApplicant.Checked), Generic.ToInt(hifEmployeeNo.Value), Generic.ToInt(hrantypeNo))
        For Each row As DataRow In _dt.Rows
            txtIsViewSalary.Checked = Generic.ToBol(row("IsViewSalary"))
            txtIsEditSalary.Checked = Generic.ToBol(row("IsEditSalary"))
            IsSalaryAdjust = Generic.ToBol(row("IsSalaryAdjust"))
        Next

        If txtIsSecHead.Checked Then
            txtIsDepHead.Enabled = False
            txtIsDivHead.Enabled = False
            txtIsFacHead.Enabled = False
            txtIsGroHead.Enabled = False
            txtIsSecHead.Enabled = False
            txtIsUniHead.Enabled = False
        Else
            txtIsDepHead.Enabled = True
            txtIsDivHead.Enabled = True
            txtIsFacHead.Enabled = True
            txtIsGroHead.Enabled = True
            txtIsSecHead.Enabled = True
            txtIsUniHead.Enabled = True
        End If
        fRegisterStartupScript("JSDialogResponse", "SalaryPermission();")
        txtCurrentSalary.Text = hifcurrentsalary.Value

        'Exit Interview Template
        Dim obj1 As Object = SQLHelper.ExecuteScalar("SELECT IsNot201 FROM EHRANType WHERE HRANTypeNo=" & hrantypeNo.ToString())
        If Generic.ToBol(obj1) Then
            Try
                Me.chkxIsNot201.Checked = True
            Catch

            End Try
        Else
            Me.chkxIsNot201.Checked = False
        End If
        'Exit Interview Template
        Dim obj As Object = SQLHelper.ExecuteScalar("SELECT IsWithExitInterview FROM EHRANType WHERE HRANTypeNo=" & hrantypeNo.ToString())
        If Generic.ToBol(obj) Then
            Try
                cboApplicantStandardHeaderNo.DataSource = SQLHelper.ExecuteDataSet("EApplicantStandardHeader_WebLookup", UserNo, 0, 2, PayLocNo)
                cboApplicantStandardHeaderNo.DataValueField = "tNo"
                cboApplicantStandardHeaderNo.DataTextField = "tDesc"
                cboApplicantStandardHeaderNo.DataBind()
            Catch ex As Exception

            End Try
            lblEI.Attributes.Add("class", "col-md-3 control-label has-required")
            cboApplicantStandardHeaderNo.CssClass = "form-control required"
            cboApplicantStandardHeaderNo.Enabled = True
        Else
            lblEI.Attributes.Add("class", "col-md-3 control-label has-space")
            cboApplicantStandardHeaderNo.CssClass = "form-control"
            cboApplicantStandardHeaderNo.Text = ""
            cboApplicantStandardHeaderNo.Enabled = False
        End If

        'Exit Interview Template
        Dim IsSeparated As Boolean = Generic.ToInt(SQLHelper.ExecuteScalar("SELECT IsSeparated FROM EHRANType WHERE HRANTypeNo=" & hrantypeNo.ToString()))
        Try
            cboApplicantStandardHeaderNo.DataSource = SQLHelper.ExecuteDataSet("EApplicantStandardHeader_WebLookup", UserNo, 0, 2, PayLocNo)
            cboApplicantStandardHeaderNo.DataValueField = "tNo"
            cboApplicantStandardHeaderNo.DataTextField = "tDesc"
            cboApplicantStandardHeaderNo.DataBind()
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub txtLS_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            PopulateDueDate(Generic.ToStr(Me.txtEffectivity.Text.ToString), Generic.ToDec(Me.txtLS.Text))
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PopulateDueDate(ByVal Effectivity As String, ByVal LS As Double)
        If LS > 0 Then
            Dim teffectivity As Date = Effectivity
            Dim dueDate As Date = Microsoft.VisualBasic.DateAdd(DateInterval.Month, LS, teffectivity)
            dueDate = Microsoft.VisualBasic.DateAdd(DateInterval.Day, -1, dueDate)
            txtDueDate.Text = dueDate.ToShortDateString
        Else
            Me.txtDueDate.Text = ""
        End If
    End Sub
    Protected Sub lnkViewPlantilla_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As New LinkButton
            Dim i As String = ""
            lnk = sender

            Dim _ds As New DataSet
            _ds = SQLHelper.ExecuteDataSet("EPlantilla_WebOne", UserNo, hifPlantillaNo.Value)
            If _ds.Tables.Count > 0 Then
                If _ds.Tables(0).Rows.Count > 0 Then
                    Dim showFrm As New clsFormControls
                    'showFrm.showFormControls(Me, _ds)
                    showFrm.clearFormControls_In_Popup(pnlPopupPlantilla)
                    showFrm.showFormControls_In_Popup(pnlPopupPlantilla, _ds)
                    showFrm.EnableControls_in_Popup(pnlPopupPlantilla, False)
                End If

            End If
            mdlPlantilla.Show()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkIncumbent_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim IncumbentNo As Integer = 0
            Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataTable("EIncumbent_WebInfo", Generic.ToInt(Me.hifActingPlantillaNo.Value))

            For Each row As DataRow In dt.Rows
                IncumbentNo = Generic.ToStr(row("IncumbentNo"))
            Next

            Info1.xID = IncumbentNo
            Info1.Show()

        Catch ex As Exception

        End Try
    End Sub

#Region "****** Web Services ******"

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function PopulateHranEmployee(prefixText As String, count As Integer, contextKey As String) As List(Of String)
        Dim items As New List(Of String)()
        Dim _ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim clsbase As New clsBase.clsBaseLibrary
        Dim UserNo As Integer = 0, PayLocNo As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        PayLocNo = (HttpContext.Current.Session("xPayLocNo"))

        _ds = SQLHelper.ExecuteDataSet("EHRAN_WebLookup_AC_Employee", UserNo, prefixText, contextKey, PayLocNo)
        For Each row As DataRow In _ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("FullName")),
                                Generic.ToStr(row("EmployeeNo")) & _
                                "|" & Generic.ToStr(row("EmployeeCode")) & _
                                "|" & Generic.ToStr(row("PlantillaNo")) & _
                                "|" & Generic.ToStr(row("PlantillaDesc")) & _
                                "|" & Generic.ToStr(row("ActingPlantillaNo")) & _
                                "|" & Generic.ToStr(row("TaskNo")) & _
                                "|" & Generic.ToStr(row("PositionNo")) & _
                                "|" & Generic.ToStr(row("PositionDesc")) & _
                                "|" & Generic.ToStr(row("SalaryGradeNo")) & _
                                "|" & Generic.ToStr(row("FacilityNo")) & _
                                "|" & Generic.ToStr(row("GroupNo")) & _
                                "|" & Generic.ToStr(row("DepartmentNo")) & _
                                "|" & Generic.ToStr(row("UnitNo")) & _
                                "|" & Generic.ToStr(row("DivisionNo")) & _
                                "|" & Generic.ToStr(row("SectionNo")) & _
                                "|" & Generic.ToStr(row("CostCenterNo")) & _
                                "|" & Generic.ToStr(row("LocationNo")) & _
                                "|" & Generic.ToStr(row("ProjectNo")) & _
                                "|" & Generic.ToStr(row("ShiftNo")) & _
                                "|" & Generic.ToStr(row("DayoffNo")) & _
                                "|" & Generic.ToStr(row("DayoffNo2")) & _
                                "|" & Generic.ToStr(row("EmployeeClassNo")) & _
                                "|" & Generic.ToStr(row("EmployeeStatNo")) & _
                                "|" & Generic.ToStr(row("RankNo")) & _
                                "|" & Generic.ToStr(row("IsSupervisor")) & _
                                "|" & Generic.ToStr(row("ImmediateSuperiorNo")) & _
                                "|" & Generic.ToStr(row("SFullName")) & _
                                "|" & Generic.ToStr(row("PayClassNo")) & _
                                "|" & Generic.ToStr(row("PayLocNo")) & _
                                "|" & Generic.ToStr(row("PayTypeNo")) & _
                                "|" & Generic.ToStr(row("PaymentTypeNo")) & _
                                "|" & Generic.ToStr(row("EmployeeRateClassNo")) & _
                                "|" & Generic.ToStr(row("TaxExemptNo")) & _
                                "|" & Generic.ToStr(row("CurrentSalary")) & _
                                "|" & Generic.ToStr(row("IsEditSalary")) & _
                                "|" & Generic.ToStr(row("ActingPlantillaDesc")) & _
                                "|" & Generic.ToStr(row("JobGradeNo")) & _
                                "|" & Generic.ToStr(row("IsDontDeductTax")) & _
                                "|" & Generic.ToStr(row("IsRata")) & _
                                "|" & Generic.ToStr(row("IsTA")))
            items.Add(item)
        Next
        _ds.Dispose()
        Return items


    End Function

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function populateHranType(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim _ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim clsbase As New clsBase.clsBaseLibrary
        Dim UserNo As Integer = 0, PayLocNo As Integer
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        PayLocNo = (HttpContext.Current.Session("xPayLocNo"))

        _ds = SQLHelper.ExecuteDataSet("EHRAN_WebLookup_AC_HRANType", UserNo, prefixText, count, PayLocNo)
        For Each row As DataRow In _ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("tDesc")),
                                Generic.ToStr(row("tNo")) & _
                                "|" & Generic.ToStr(row("IsViewSalary")) & _
                                "|" & Generic.ToStr(row("IsSalaryAdjustment")) & _
                                "|" & Generic.ToStr(row("HasAppointment")) & _
                                "|" & Generic.ToStr(row("IsSeparated")))
            items.Add(item)
        Next
        _ds.Dispose()
        Return items


    End Function

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
                                "|" & Generic.ToStr(row("FacilityNo")) & _
                                "|" & Generic.ToStr(row("GroupNo")) & _
                                "|" & Generic.ToStr(row("DepartmentNo")) & _
                                "|" & Generic.ToStr(row("UnitNo")) & _
                                "|" & Generic.ToStr(row("PositionNo")) & _
                                "|" & Generic.ToStr(row("RMCNo")) & _
                                "|" & Generic.ToStr(row("BranchNo")) & _
                                "|" & Generic.ToStr(row("LocationNo")) & _
                                "|" & Generic.ToStr(row("TaskNo")) & _
                                "|" & Generic.ToStr(row("TeamNo")) & _
                                "|" & Generic.ToStr(row("DivisionNo")) & _
                                "|" & Generic.ToStr(row("CostCenterNo")) & _
                                "|" & Generic.ToStr(row("ImmediateSuperiorNo")) & _
                                "|" & Generic.ToStr(row("SalaryGradeNo")) & _
                                "|" & Generic.ToStr(row("IsFacHead")) & _
                                "|" & Generic.ToStr(row("IsGroHead")) & _
                                "|" & Generic.ToStr(row("IsDepHead")) & _
                                "|" & Generic.ToStr(row("IsDivHead")) & _
                                "|" & Generic.ToStr(row("IsUniHead")) & _
                                "|" & Generic.ToStr(row("IsSecHead")) & _
                                "|" & Generic.ToStr(row("PositionDescS")) & _
                                "|" & Generic.ToStr(row("ShiftNo")) & _
                                "|" & Generic.ToStr(row("SectionNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items


    End Function
    '<System.Web.Script.Services.ScriptMethod()> _
    '<System.Web.Services.WebMethod()> _
    'Public Shared Function PopulateItemNoInfo(prefixText As String, count As Integer, contextKey As String) As List(Of String)
    '    Dim items As New List(Of String)()
    '    Dim ds As New DataSet()
    '    Dim sqlhelp As New clsBase.SQLHelper
    '    Dim UserNo As Integer = 0, PayLocNo As Integer = 0
    '    UserNo = (HttpContext.Current.Session("onlineuserno"))
    '    PayLocNo = (HttpContext.Current.Session("xPayLocNo"))

    '    ds = SQLHelper.ExecuteDataSet("EHRAN_WebLookup_AC_Plantilla", UserNo, prefixText, contextKey, PayLocNo)
    '    For Each row As DataRow In ds.Tables(0).Rows
    '        Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("PlantillaCode")),
    '                            Generic.ToStr(row("PlantillaNo")) & _
    '                            "|" & Generic.ToStr(row("FacilityNo")) & _
    '                            "|" & Generic.ToStr(row("GroupNo")) & _
    '                            "|" & Generic.ToStr(row("DepartmentNo")) & _
    '                            "|" & Generic.ToStr(row("UnitNo")) & _
    '                            "|" & Generic.ToStr(row("RMCNo")) & _
    '                            "|" & Generic.ToStr(row("BranchNo")) & _
    '                            "|" & Generic.ToStr(row("LocationNo")) & _
    '                            "|" & Generic.ToStr(row("TaskNo")) & _
    '                            "|" & Generic.ToStr(row("TeamNo")) & _
    '                            "|" & Generic.ToStr(row("DivisionNo")) & _
    '                            "|" & Generic.ToStr(row("CostCenterNo")) & _
    '                            "|" & Generic.ToStr(row("ImmediateSuperiorNo")) & _
    '                            "|" & Generic.ToStr(row("SalaryGradeNo")) & _
    '                            "|" & Generic.ToStr(row("IsFacHead")) & _
    '                            "|" & Generic.ToStr(row("IsGroHead")) & _
    '                            "|" & Generic.ToStr(row("IsDepHead")) & _
    '                            "|" & Generic.ToStr(row("IsDivHead")) & _
    '                            "|" & Generic.ToStr(row("IsUniHead")) & _
    '                            "|" & Generic.ToStr(row("IsSecHead")) & _
    '                            "|" & Generic.ToStr(row("ShiftNo")) & _
    '                            "|" & Generic.ToStr(row("SectionNo")))
    '        items.Add(item)
    '    Next
    '    ds.Dispose()
    '    Return items


    'End Function

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function PopulateSalaryLevel(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim _ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim clsbase As New clsBase.clsBaseLibrary
        Dim UserNo As Integer = 0, PayLocNo As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        PayLocNo = (HttpContext.Current.Session("xPayLocNo"))

        _ds = SQLHelper.ExecuteDataSet("EHRAN_WebLookup_AC_Position", UserNo, prefixText, count, PayLocNo)
        For Each row As DataRow In _ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("PositionDesc")),
                            Generic.ToStr(row("PositionNo")) & _
                            "|" & Generic.ToStr(row("SalaryGradeNo")) & _
                            "|" & Generic.ToStr(row("JobGradeNo")) & _
                            "|" & Generic.ToStr(row("EmployeeClassNo")))
            items.Add(item)
        Next
        _ds.Dispose()
        Return items

    End Function

#End Region

    Protected Sub cboPositionNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        populateCombo()
    End Sub

    Protected Sub PopulateCombo()
        Try
            lstPlantilla.DataSource = SQLHelper.ExecuteDataSet("EPlantilla_WebLookup", UserNo, Generic.ToInt(cboPositionNo.SelectedValue), PayLocNo) 'clsGeneric.xLookup_Table(UserNo, "EPlantillaL", Session("xPayLocNo"))
            lstPlantilla.DataValueField = "tNo"
            lstPlantilla.DataTextField = "tDesc"
            lstPlantilla.DataBind()
        Catch ex As Exception

        End Try
        
    End Sub

End Class
