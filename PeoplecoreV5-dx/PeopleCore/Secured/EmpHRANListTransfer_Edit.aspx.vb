Imports System.Data
Imports Microsoft.VisualBasic
Imports clsLib

Partial Class Secured_EmpHRANEditTransfer
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim tmodify As Boolean = False
    Dim HRANNo As Integer = 0

    Dim rowNo As Integer = 0
    Dim showFrm As New clsFormControls

    Dim HasAppointment As Boolean = False
    Dim isGrantedRate As Boolean = False
    Dim clsGeneric As New clsGenericClass

    'Display record
    Private Sub populateData()
        Dim _ds As New DataSet
        Dim _ds2 As New DataSet
        _ds = sqlHelper.ExecuteDataset("ehran_WebOne", xPublicVar.xOnlineUseNo, HRANNo)
        ' _ds = sqlHelper.ExecuteDataset( "EHRANWebInq", xPublicVar.xOnlineUseNo, HRANNo)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                showFrm.showFormControls(Me, _ds)
                populateHRANCorrected(Generic.CheckDBNull(hifEmployeeNo.Value, clsBase.clsBaseLibrary.enumObjectType.IntType))
                
                hfPlantillaNo.Value = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("PlantillaNo"), clsBase.clsBaseLibrary.enumObjectType.IntType)
               
            End If

        End If
        'GetHRANTypeAppointStatus()
    End Sub


    Private Sub PopulateCombo()
        If Not tmodify Then
            showFrm.populateCombo_One(xPublicVar.xOnlineUseNo, Me, Session("xPayLocNo"))
        Else
            showFrm.populateCombo(xPublicVar.xOnlineUseNo, Me, Session("xPayLocNo"))
        End If
        populateHRANCorrected(Generic.CheckDBNull(hifEmployeeNo.Value, clsBase.clsBaseLibrary.enumObjectType.IntType))

    End Sub



    Private Sub populateHRANCorrected(employeeNo As Integer)
        Try
            cboHRANCorrectedNo.DataSource = sqlHelper.ExecuteDataset("EHRAN_WebLookup_Corrected", xPublicVar.xOnlineUseNo, employeeNo, Session("xPayLocNo"))
            cboHRANCorrectedNo.DataTextField = "tDesc"
            cboHRANCorrectedNo.DataValueField = "tNo"
            cboHRANCorrectedNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    'Enable or disable control
    Private Sub DisableEnableCtrl(ByVal IsLock As Boolean)
        Dim fIslock As Boolean = IsLock

        If IsLock Then
            IsLock = False
        End If
        Dim clsEnable As New clsFormControls
        clsEnable.EnableControls(Me, IsLock)
        lnkSubmit.Visible = fIslock
        lnkModify.Visible = Not fIslock
        Me.txtHRANCode.ReadOnly = True
        Me.txtHRANCode.Enabled = False
        Me.txtIncumbent.Enabled = False
        Me.txtIncumbentPosition.Enabled = False
        Me.txtDatePub.Enabled = True 'False


        If fIslock Then
            Me.cboHRANCorrectedNo.Enabled = False
            txtFullName.Enabled = True
            txtFullName.ReadOnly = False
            txtIsPosting.Enabled = True

        End If
        

    End Sub

 

    Protected Sub GetHRANTypeAppointStatus()
        Dim IsStatus As Boolean = False
        Dim ds As DataSet
        Dim HRANTypeNo As Integer = 0
        HRANTypeNo = Generic.CheckDBNull(hifHRANTypeNo.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)

        ds = sqlHelper.ExecuteDataset("EHRANType_Web_AppointStatus", Generic.CheckDBNull(hifHRANTypeNo.Value, clsBase.clsBaseLibrary.enumObjectType.IntType))
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                IsStatus = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsStatus"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                HasAppointment = Generic.CheckDBNull(ds.Tables(0).Rows(0)("HasAppointment"), clsBase.clsBaseLibrary.enumObjectType.IntType)
            End If
        End If

        Me.txtIsDepHead.Enabled = Not IsStatus
        Me.txtIsDivHead.Enabled = Not IsStatus
        Me.txtIsFacHead.Enabled = Not IsStatus
        Me.txtIsGroHead.Enabled = Not IsStatus
        Me.txtIsSecHead.Enabled = Not IsStatus
        Me.txtIsUniHead.Enabled = Not IsStatus


        Dim index As Short = 0
        If HasAppointment = True Then index = 1

        fRegisterStartupScript("JSDialogResponse", "ElementControl_DisplayFormat('#pPublication','" & index & "')")

    End Sub

    Protected Sub cboActing_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboActing.SelectedIndexChanged
        Populate_Incumbent()
    End Sub

    Private Sub Populate_Incumbent()
        Try
            Dim ds As DataSet
            ds = sqlHelper.ExecuteDataset("EGet_Incumbent", Generic.CheckDBNull(Me.cboActing.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.txtIncumbent.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("Fullname"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    Me.txtIncumbentPosition.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("PositionDesc"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim employeeno As Integer

        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("onlineuserno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        AccessRights.CheckUser(xPublicVar.xOnlineUseNo, "EmphranlistTransfer.aspx")
        employeeno = Generic.CheckDBNull(Request.QueryString("employeeno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        HRANNo = Generic.CheckDBNull(Request.QueryString("transNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        tmodify = Generic.CheckDBNull(Request.QueryString("tModify"), clsBase.clsBaseLibrary.enumObjectType.IntType)

        If HRANNo = 0 Then
            'Me.cboPaymentTypeNo.SelectedValue = 3
            'Me.cboPayTypeNo.SelectedValue = 1
            'Me.cboPayLocNo.SelectedValue = 1
            'Me.cboHRANHOANo.SelectedValue = 2
            'Me.cboHRANHOADNo.SelectedValue = 2

            Me.txtPreparationDate.Text = Now.ToShortDateString
            Me.txtCompletionDate.Text = Now.ToShortDateString
            Me.txtCompletedBy.Text = Session("OnlineUsername")
            hifHRANTypeNo.Value = 5
            txtHRANTypeDesc.Text = "transfer out"
        End If

        If Not IsPostBack Then
            populateData()
            PopulateCombo()

            DisableEnableCtrl(tmodify)

        End If
        Me.txtCompletedBy.Enabled = False
        Me.txtCompletionDate.Enabled = False

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    'Security for modify record

    Protected Sub lnkModify_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Generic.CheckDBNull(Me.txtIsServed.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType) Then
            MessageBox.Information(MessageTemplate.DeniedPost, Me)
            Exit Sub
        End If
        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit, "EmpHranListtransfer.aspx") Then
            Response.Redirect("~/secured/EmphranListtransfer_edit.aspx?transNo=" & HRANNo & "&tModify=True&rowNO=" & rowNo)
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
        End If

    End Sub

    'Cancel modify
    Protected Sub lnkCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("~/secured/Emphranelisttransfer_edit.aspx?transNo=" & HRANNo & "&tModify=False")
    End Sub

    'Submit record
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tProceed As Integer = SaveRecord()
        If tProceed = 1 Then
            Dim url As String = "Emphranlisttransfer.aspx?transNo=" & HRANNo & "&tModify=False"
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        ElseIf tProceed = 2 Then
            MessageBox.Warning("This employee or applicant has a pending hran transaction.", Me)
        ElseIf tProceed = 3 Then
            MessageBox.Warning("Item no. already in use.", Me)
        ElseIf tProceed = 4 Then

        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecord() As Integer
        Dim employeeclassno As Integer = Generic.CheckDBNull(Me.cboEmployeeClassNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim paytypeno As Integer = Generic.CheckDBNull(Me.cboPayTypeNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim paylocno As Integer = Session("xPayLocNo") ' Generic.CheckDBNull(Me.cboPayLocNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim payclassno As Integer = Generic.CheckDBNull(Me.cboPayClassNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim employeerateclassno As Integer = Generic.CheckDBNull(Me.cboEmployeeRateClassNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim employeestatno As Integer = Generic.CheckDBNull(Me.cboEmployeeStatNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim jobgradeno As Integer = Generic.CheckDBNull(Me.cboJobgradeNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim SalaryGradeNo As Integer = Generic.CheckDBNull(Me.cboSalaryGradeNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim positionno As Integer = Generic.CheckDBNull(Me.hifpositionNo.Value, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim divisionno As Integer = Generic.CheckDBNull(Me.cboDivisionNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim departmentno As Integer = Generic.CheckDBNull(Me.cboDepartmentNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim sectionno As Integer = Generic.CheckDBNull(Me.cboSectionNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim costcenterno As Integer = Generic.CheckDBNull(Me.cboCostCenterNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim shiftno As Integer = Generic.CheckDBNull(Me.cboShiftNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim dayoffno As Integer = Generic.CheckDBNull(Me.cboDayOffNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim taxexemptno As Integer = Generic.CheckDBNull(Me.cboTaxExemptNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim immediatesuperiorno As Integer = Generic.CheckDBNull(Me.cboImmediateSuperiorNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        Dim employeeno As Integer = Generic.CheckDBNull(Me.hifEmployeeNo.Value, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim hrantypeno As Integer = Generic.CheckDBNull(Me.hifHRANTypeNo.Value, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        'Dim taskno As Integer = 0
        Dim currentsalary As Double = Generic.CheckDBNull(Me.txtCurrentSalary.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim Locationno As Integer = Generic.CheckDBNull(Me.cboLocationNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim salarygradecodeno As Integer = Generic.CheckDBNull(Me.cboJobgradeNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim plantillano As Integer = Generic.CheckDBNull(hfPlantillaNo.Value, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        plantillano = Generic.CheckDBNull(Me.hfPlantillaNo.Value, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim projectno As Integer = Generic.CheckDBNull(Me.cboProjectNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim stepno As Integer = 0
        Dim ecola As Double = 0
        Dim allowance As Double = 0
        Dim mealallow As Double = 0
        Dim transallow As Double = 0
        Dim commallow As Double = 0
        Dim cashbond As Double = 0
        Dim fullname As String = txtFullName.Text.ToString
        Dim rankno As Integer = Generic.CheckDBNull(Me.cboRankNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim transtypeno As Integer = 0
        Dim location2no As Integer = 0
        Dim commtypeno As Integer = 0

        Dim facilityNo As Integer = Generic.CheckDBNull(Me.cboFacilityNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim taskno As Double = Generic.CheckDBNull(Me.cboTaskNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim unitno As Double = Generic.CheckDBNull(Me.cboUnitNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim paymenttype As Integer = Generic.CheckDBNull(Me.cboPaymentTypeNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim groupno As Integer = Generic.CheckDBNull(Me.cboGroupNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim fdatestr As String
        Dim pwdEmployeeno As String = Generic.CheckDBNull(hifEmployeeNo.Value.ToString, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim pwd As String = PeopleCoreCrypt.Encrypt("Password@" & pwdEmployeeno.ToString)

        Dim macAddress = "" ' Generic.CheckDBNull(clsLib.getMacAddress(), clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim ipAddress = "" 'Generic.CheckDBNull(clsLib.getIPAddress(), clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim hostName = "" ' Generic.CheckDBNull(clsLib.getHostname(), clsBase.clsBaseLibrary.enumObjectType.StrType)

        Dim isfromseparated As Integer = 0

        Dim ActingPlantillaNo As Integer = Generic.CheckDBNull(Me.cboActing.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim isConferment As Boolean = Generic.CheckDBNull(Me.txtIsConferment.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim OfficeOrderNum As String = Generic.CheckDBNull(Me.txtHRANOfficeOrderNo.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim AccountCode As String = Generic.CheckDBNull(Me.txtBranchAccountCode.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim EscalationGroupNo As Integer = 0 ' Generic.CheckDBNull(Me.cboEscalationGroup.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)

        Dim AgencyHead As String = Generic.CheckDBNull(Me.cboHRANHOANo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim AgencyHeadDesignation As String = Generic.CheckDBNull(Me.txtDesignation.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim PSBHead As String = Generic.CheckDBNull(Me.txtPSBHead.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim HRHead As String = Generic.CheckDBNull(Me.txtHRHead.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim PublicationNo As Integer = Generic.CheckDBNull(Me.cboPublicationLNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim IsSave201 As Integer = 0 'Generic.CheckDBNull(Me.txtIsSave201.Checked, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        Dim HRANHOANo As Integer = Generic.CheckDBNull(Me.cboHRANHOANo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim HRANHOADNo As Integer = Generic.CheckDBNull(Me.cboHRANHOADNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim HRANHRMONo As Integer = Generic.CheckDBNull(Me.cboHRANHRMONo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim HRANPSBNo As Integer = Generic.CheckDBNull(Me.cboHRANPSBNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)

        Dim RMCNo As Integer = Generic.CheckDBNull(Me.cboRMCNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim BranchNo As Integer = Generic.CheckDBNull(Me.cboBranchNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim TeamNo As Integer = 0 ' Generic.CheckDBNull(Me.cboTeamNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim IsRMCHead As Integer = Generic.CheckDBNull(Me.txtIsRMCHead.Checked, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim IsBranchHead As Integer = Generic.CheckDBNull(Me.txtIsBranchHead.Checked, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim IsTeamHead As Integer = 0 ' Generic.CheckDBNull(Me.txtIsTeamHead.Checked, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        If Me.txtPreparationDate.Text <> "" Then
            fdatestr = clsGenericClass.DateFormat(Me.txtPreparationDate.Text)
            If Len(fdatestr) > 1 Then
                Me.txtPreparationDate.Focus()
                Me.txtPreparationDate.Text = ""
                MessageBox.Warning(fdatestr, Me)
                SaveRecord = 4
                Exit Function
            End If
        End If

        If Me.txtEffectivity.Text <> "" Then
            fdatestr = clsGenericClass.DateFormat(Me.txtEffectivity.Text)
            If Len(fdatestr) > 1 Then
                Me.txtEffectivity.Focus()
                Me.txtEffectivity.Text = ""
                MessageBox.Warning(fdatestr, Me)
                SaveRecord = 4
                Exit Function
            End If
        End If

        If Me.txtDueDate.Text <> "" Then
            fdatestr = clsGenericClass.DateFormat(Me.txtDueDate.Text)
            If Len(fdatestr) > 1 Then
                Me.txtDueDate.Focus()
                Me.txtDueDate.Text = ""

                MessageBox.Warning(fdatestr, Me)
                SaveRecord = 4
                Exit Function
            End If
        End If

        Dim dsStatus As DataSet
        Dim lastname As String = ""
        Dim firstname As String = ""
        Dim tEmployeeno As Integer = 0
        Dim rankPrev As String = ""

        dsStatus = SQLHelper.ExecuteDataSet("EHRAN_WebSave_Transfer", xPublicVar.xOnlineUseNo, _
                                          HRANNo, _
                                          employeeno, _
                                          Me.txtEmployeeCode.Text.ToString, _
                                          hrantypeno, _
                                          Me.txtReason.Text.ToString, _
                                          Me.txtPreparationDate.Text.ToString, _
                                          Me.txtEffectivity.Text.ToString, _
                                          Me.txtDescription.Text.ToString, _
                                          Me.txtDueDate.Text.ToString, _
                                          positionno, _
                                          taskno, _
                                          divisionno, _
                                          departmentno, _
                                          sectionno, _
                                          dayoffno, _
                                          shiftno, _
                                          employeeclassno, _
                                          employeerateclassno, _
                                          employeestatno, _
                                          paytypeno, _
                                          paylocno, _
                                          currentsalary, _
                                          immediatesuperiorno, _
                                          Me.txtIsSupervisor.Checked, _
                                          Me.txtIsPosting.Checked, _
                                          xPublicVar.xOnlineUseNo, _
                                          Locationno, _
                                          SalaryGradeNo, _
                                          plantillano, _
                                          stepno, _
                                          payclassno, _
                                          ecola, _
                                          allowance, _
                                          mealallow, _
                                          transallow, _
                                          commallow, _
                                          cashbond, _
                                          fullname, _
                                          False, _
                                          taxexemptno, _
                                          rankno, _
                                          transtypeno, _
                                          location2no, _
                                          commtypeno, _
                                          Generic.CheckDBNull(Me.cboHRANRCNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), _
                                          Generic.CheckDBNull(Me.cboDayOffNo2.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), _
                                          facilityNo, _
                                          unitno, _
                                          paymenttype, _
                                          costcenterno, _
                                          groupno, _
                                          pwd, _
                                          Generic.CheckDBNull(Me.txtLS.Text, clsBase.clsBaseLibrary.enumObjectType.IntType), _
                                          Generic.CheckDBNull(cboHRANCorrectedNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType), _
                                          Generic.CheckDBNull(txtIsFacHead.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType), _
                                          Generic.CheckDBNull(txtIsDivHead.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType), _
                                          Generic.CheckDBNull(txtIsDepHead.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType), _
                                          Generic.CheckDBNull(txtIsSecHead.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType), _
                                          Generic.CheckDBNull(txtIsGroHead.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType), _
                                          Generic.CheckDBNull(txtIsUniHead.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType), _
                                          ipAddress, _
                                          hostName, _
                                          macAddress, _
                                          isfromseparated, _
                                          projectno, _
                                          Generic.CheckDBNull(Me.cboAllowanceTemplate.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType), _
                                          ActingPlantillaNo, _
                                          OfficeOrderNum, _
                                          Me.txtIsConferment.Checked, _
                                          AccountCode, _
                                          EscalationGroupNo, _
                                          rankPrev, _
                                          IsSave201, _
                                          Me.txtDatePub.Text.ToString, _
                                          AgencyHead, _
                                          AgencyHeadDesignation, _
                                          PSBHead, _
                                          HRHead, _
                                          PublicationNo, _
                                          HRANHOANo, _
                                          HRANHOADNo, _
                                          HRANHRMONo, _
                                          HRANPSBNo, _
                                          RMCNo, _
                                          BranchNo, _
                                          TeamNo, _
                                          IsRMCHead, _
                                          IsBranchHead, _
                                          IsTeamHead,
                                          txtDateofApproval.Text, _
                                          chkIsForRata.Checked)
        If dsStatus.Tables.Count > 0 Then
            If dsStatus.Tables(0).Rows.Count > 0 Then
                SaveRecord = Generic.CheckDBNull(dsStatus.Tables(0).Rows(0)("tstatus"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            Else
                SaveRecord = 0
            End If
        Else
            SaveRecord = 0
        End If
    End Function


    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub

    Private Function RandomNum(i As String, icount As Integer) As Integer
        Dim l As Integer = i.Length
        Dim retval As String = ""
        retval = Mid(i, icount + 1, 1)
        Return CInt(retval)

    End Function

    Private Function RandomString(userCode As String) As String
        Dim thisRand As Integer
        Dim thisChar As String
        Dim thisString As String = ""
        Dim size As Integer = userCode.Length
        Dim i As Integer

        For i = 0 To size - 1

            thisRand = RandomNum(userCode, i) + CInt(34 * Rnd())
            Select Case thisRand
                Case 10
                    thisChar = "A"
                Case 11
                    thisChar = "B"
                Case 12
                    thisChar = "C"
                Case 13
                    thisChar = "D"
                Case 14
                    thisChar = "E"
                Case 15
                    thisChar = "F"
                Case 16
                    thisChar = "G"
                Case 17
                    thisChar = "H"
                Case 18
                    thisChar = "I"
                Case 19
                    thisChar = "J"
                Case 20
                    thisChar = "K"
                Case 21
                    thisChar = "L"
                Case 22
                    thisChar = "M"
                Case 23
                    thisChar = "N"
                Case 24
                    thisChar = "O"
                Case 25
                    thisChar = "P"
                Case 26
                    thisChar = "Q"
                Case 27
                    thisChar = "R"
                Case 28
                    thisChar = "S"
                Case 29
                    thisChar = "T"
                Case 30
                    thisChar = "U"
                Case 31
                    thisChar = "V"
                Case 32
                    thisChar = "W"
                Case 33
                    thisChar = "X"
                Case 34
                    thisChar = "Y"
                Case 35
                    thisChar = "Z"
                Case Else
                    thisChar = thisRand
            End Select
            thisString = thisString & thisChar
        Next i

        RandomString = LCase(thisString)

    End Function

    Protected Sub txtLS_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLS.TextChanged
        Try
            PopulateDueDate(Generic.CheckDBNull(Me.txtEffectivity.Text.ToString, clsBase.clsBaseLibrary.enumObjectType.StrType), Generic.CheckDBNull(Me.txtLS.Text, clsBase.clsBaseLibrary.enumObjectType.DblType))

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDueDate(ByVal Effectivity As String, ByVal LS As Double)

        Dim ds As DataSet
        ds = sqlHelper.ExecuteDataset("EHRAN_WebOne_DueDate", Effectivity, LS)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Me.txtDueDate.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DueDate"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            End If
        End If
    End Sub

    <System.Web.Script.Services.ScriptMethod()> _
   <System.Web.Services.WebMethod()> _
    Public Shared Function PopulateSalaryLevel(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim _ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim UserNo As Integer = 0, payLocno As Integer
        Dim clsbase As New clsBase.clsBaseLibrary
        UserNo = clsbase.CheckDBNull(HttpContext.Current.Session("onlineuserno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        payLocno = clsbase.CheckDBNull(HttpContext.Current.Session("xPayLocNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        _ds = sqlHelper.ExecuteDataset("EHRAN_WebOne_Position_SalaryLevel_AutoComplete", UserNo, prefixText)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In _ds.Tables(0).Rows
                    Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("PositionDesc")),
                                    Generic.ToStr(row("PositionNo")) & _
                                    "|" & Generic.ToStr(row("SalaryGradeNo")) & _
                                    "|" & Generic.ToStr(row("ShiftNo")))
                    items.Add(item)
                Next
                _ds.Dispose()

            End If

        End If
        Return items
    End Function

    Protected Sub PopulateSuperior_Plantilla(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim PlantillaNo As Integer = Generic.CheckDBNull(Me.hfPlantillaNo.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim ds As DataSet
        ds = sqlHelper.ExecuteDataset("EHRAN_WebOne_Plantilla_Superior", PlantillaNo)
        'Try
        '    cboImmediateSuperiorNo.DataSource = sqlHelper.ExecuteDataset( CommandType.Text, "SELECT FullName AS tDesc,EmployeeNo AS tNo FROM dbo.EEmployee UNION ALL SELECT '',0")
        '    cboImmediateSuperiorNo.DataTextField = "tDesc"
        '    cboImmediateSuperiorNo.DataValueField = "tNo"
        '    cboImmediateSuperiorNo.DataBind()
        'Catch ex As Exception

        'End Try
        Try
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.cboImmediateSuperiorNo.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("EmployeeNo"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkViewPlantilla_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'clsLog.PopulateEditPage(xPublicVar.xOnlineUseNo, Session("xMenutype"), "Sub", clsArray.myFormname(1).xFormname, clsArray.myFormname(1).xTablename, clsArray.myFormname(1).xLevelNo, clsArray.myFormname(1).xMenuTitle)

            Dim lnk As New LinkButton
            Dim i As String = ""

            lnk = sender

            Dim _ds As New DataSet

            _ds = SQLHelper.ExecuteDataSet("EPlantilla_WebOne", xPublicVar.xOnlineUseNo, hfPlantillaNo.Value)
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

    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function PopulateItemNoInfo(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        ds = SQLHelper.ExecuteDataSet("EPlantilla_WebAutoComplete", prefixText, count)
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
                                                                                                "|" & Generic.ToStr(row("ShiftNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items


    End Function
    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function PopulateHranEmployee(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim _ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim UserNo As Integer = 0
        Dim clsbase As New clsBase.clsBaseLibrary
        UserNo = clsbase.CheckDBNull(HttpContext.Current.Session("onlineuserno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        '_ds = sqlHelper.ExecuteDataset( "EEmployee_WebOne", xPublicVar.xOnlineUseNo, employeeNo)
        _ds = SQLHelper.ExecuteDataSet("EHRAN_WebAutoComplete", UserNo, prefixText, 3)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                'showFrm.showFormControls(Me, _ds)

                For Each row As DataRow In _ds.Tables(0).Rows
                    Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("fullName")),
                                    Generic.ToStr(row("employeeNo")) & _
                                    "|" & Generic.ToStr(row("EmployeeClassNo")) & _
                                    "|" & Generic.ToStr(row("DayoffNo")) & _
                                    "|" & Generic.ToStr(row("DayoffNo2")) & _
                                    "|" & Generic.ToStr(row("DepartmentNo")) & _
                                    "|" & Generic.ToStr(row("DivisionNo")) & _
                                    "|" & Generic.ToStr(row("GroupNo")) & _
                                    "|" & Generic.ToStr(row("PlantillaNo")) & _
                                    "|" & Generic.ToStr(row("SAlarygradeNo")) & _
                                    "|" & Generic.ToStr(row("LocationNo")) & _
                                    "|" & Generic.ToStr(row("PayClassNO")) & _
                                    "|" & Generic.ToStr(row("PaytypeNo")) & _
                                    "|" & Generic.ToStr(row("PositionNo")) & _
                                    "|" & Generic.ToStr(row("ProjectNo")) & _
                                    "|" & Generic.ToStr(row("EmployeeRateClassNo")) & _
                                    "|" & Generic.ToStr(row("SectionNo")) & _
                                    "|" & Generic.ToStr(row("ShiftNo")) & _
                                    "|" & Generic.ToStr(row("EmployeeStatNo")) & _
                                    "|" & Generic.ToStr(row("ImmediateSuperiorNo")) & _
                                    "|" & Generic.ToStr(row("TaxExemptNo")) & _
                                    "|" & Generic.ToStr(row("unitno")) & _
                                    "|" & Generic.ToStr(row("CurrentSalary")) & _
                                    "|" & Generic.ToStr(row("RankNo")) & _
                                    "|" & Generic.ToStr(row("Acting")) & _
                                    "|" & Generic.ToStr(row("facilityno")) & _
                                    "|" & Generic.ToStr(row("taskno")) & _
                                    "|" & Generic.ToStr(row("PlantillaDesc")) & _
                                    "|" & Generic.ToStr(row("EmployeeCode")) & _
                                    "|" & Generic.ToStr(row("IsAllowEdit")) & _
                                    "|" & Generic.ToStr(row("IsViewSalary")) & _
                                    "|" & Generic.ToStr(row("PositionDesc")))
                    items.Add(item)
                Next
                _ds.Dispose()

            End If

        End If
        Return items
    End Function

    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function populateHranType(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim _ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim UserNo As Integer = 0, payLocno As Integer
        Dim clsbase As New clsBase.clsBaseLibrary
        UserNo = clsbase.CheckDBNull(HttpContext.Current.Session("onlineuserno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        payLocno = clsbase.CheckDBNull(HttpContext.Current.Session("xPayLocNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        _ds = sqlHelper.ExecuteDataset("EHRANType_WebLookup_AutoComplete", UserNo, prefixText, payLocno)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                For Each row As DataRow In _ds.Tables(0).Rows
                    Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("tDesc")),
                                    Generic.ToStr(row("tNo")) & _
                                    "|" & Generic.ToStr(row("IsStatus")) & _
                                    "|" & Generic.ToStr(row("IsViewSalary")) & _
                                    "|" & Generic.ToStr(row("HasAppointment")))
                    items.Add(item)
                Next
                _ds.Dispose()

            End If

        End If
        Return items
    End Function



End Class
