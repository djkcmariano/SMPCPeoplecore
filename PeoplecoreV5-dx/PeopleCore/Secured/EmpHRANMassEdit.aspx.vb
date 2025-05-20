Imports clsLib
Imports System.Data

Partial Class Secured_EmpHRANEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim clsGeneric As New clsGenericClass

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpHRANMassList.aspx", "EHRANMass") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EHRANMass_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))
            Try
                cboHRANTypeNo.DataSource = SQLHelper.ExecuteDataSet("EHRANType_WebLookup_UnionAll", UserNo, Generic.ToInt(row("HRANTypeNo")), PayLocNo)
                cboHRANTypeNo.DataTextField = "tDesc"
                cboHRANTypeNo.DataValueField = "tNo"
                cboHRANTypeNo.DataBind()
            Catch ex As Exception
            End Try
        Next

        If TransNo = 0 Then
            Me.txtPreparationDate.Text = Now.ToShortDateString
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "EmpHRANMassList.aspx", "EHRANMass")

        If Not IsPostBack Then
            If TransNo = 0 Then
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            End If
            PopulateDropDown()
            PopulateData()
            PopulateTabHeader()
        End If

        EnabledControls()

    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))

        If txtIsPosted.Checked = True Then
            Enabled = False
        End If

        Generic.EnableControls(Me, "Panel1", Enabled)
        txtHRANMassCode.Enabled = False
        txtPreparationDate.Enabled = False
        lnkModify.Visible = Not Enabled
        lnkSave.Visible = Enabled
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboHRANTypeNo.DataSource = SQLHelper.ExecuteDataSet("EHRANType_WebLookup_UnionAll", UserNo, 0, PayLocNo)
            cboHRANTypeNo.DataTextField = "tDesc"
            cboHRANTypeNo.DataValueField = "tNo"
            cboHRANTypeNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRANMass_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpHRANMassList.aspx", "EHRANMass") Then
            Dim RetVal As Boolean = False
            Dim dt As DataTable

            Dim hranmassno As Integer = Generic.ToInt(Me.txtHRANMassNo.Text)
            Dim hrantypeno As Integer = Generic.ToInt(Me.cboHRANTypeNo.SelectedValue)
            Dim PreparationDate As String = Generic.ToStr(Me.txtPreparationDate.Text)
            Dim Effectivity As String = Generic.ToStr(Me.txtEffectivity.Text)
            Dim DueDate As String = Generic.ToStr(Me.txtDueDate.Text)
            Dim Reason As String = Generic.ToStr(Me.txtReason.Text)
            Dim Description As String = Generic.ToStr(Me.txtDescription.Text)
            Dim IsTablebase As Boolean = Generic.ToBol(Me.txtIsTablebase.Checked)
            Dim IsInPercent As Boolean = Generic.ToBol(Me.txtIsInPercent.Checked)
            Dim RateIncrease As Double = Generic.ToDec(Me.txtRateIncrease.Text)
            Dim IsReady As Boolean = Generic.ToBol(Me.txtIsReady.Checked)

            'Criteria
            Dim cpositionno As Integer = Generic.ToInt(Me.cboCPositionNo.SelectedValue)
            Dim ctaskno As Integer = Generic.ToInt(Me.cboCTaskNo.SelectedValue)
            Dim cfacilityNo As Integer = Generic.ToInt(Me.cboCFacilityNo.SelectedValue)
            Dim cgroupno As Double = Generic.ToInt(Me.cboCGroupNo.SelectedValue)
            Dim cdepartmentno As Integer = Generic.ToInt(Me.cboCDepartmentNo.SelectedValue)
            Dim cdivisionno As Integer = Generic.ToInt(Me.cboCDivisionNo.SelectedValue)
            Dim csectionno As Integer = Generic.ToInt(Me.cboCSectionNo.SelectedValue)
            Dim cunitno As Integer = Generic.ToInt(Me.cboCUnitNo.SelectedValue)
            Dim ccostcenterno As Integer = Generic.ToInt(Me.cboCCostCenterNo.SelectedValue)
            Dim clocationno As Integer = Generic.ToInt(Me.cboCLocationNo.SelectedValue)
            Dim cprojectno As Integer = Generic.ToInt(Me.cboCProjectNo.SelectedValue)
            Dim cjobgradeno As Integer = 0 ''Generic.ToInt(Me.cboCJobGradeNo.SelectedValue)
            Dim cemployeeclassno As Integer = Generic.ToInt(Me.cboCEmployeeClassNo.SelectedValue)
            Dim cemployeestatno As Integer = Generic.ToInt(Me.cboCEmployeeStatNo.SelectedValue)
            Dim crankno As Integer = Generic.ToInt(Me.cboCRankNo.SelectedValue)
            Dim cpayclassno As Integer = Generic.ToInt(Me.cboCPayclassNo.SelectedValue)
            Dim cImmediate As Integer = Generic.ToInt(Me.hifCSuperiorNo.Value)
            Dim crateclassno As Integer = Generic.ToInt(Me.cboCEmployeeRateClassNo.SelectedValue)
            Dim cplantillano As Integer = 0
            Dim csalarygradeno As Integer = Generic.ToInt(Me.cboCSalaryGradeNo.SelectedValue)

            'Action Template
            Dim positionno As Integer = Generic.ToInt(Me.cboAPositionNo.SelectedValue)
            Dim taskno As Integer = Generic.ToInt(Me.cboATaskNo.SelectedValue)
            Dim facilityNo As Integer = Generic.ToInt(Me.cboAFacilityNo.SelectedValue)
            Dim groupno As Double = Generic.ToInt(Me.cboAGroupNo.SelectedValue)
            Dim departmentno As Integer = Generic.ToInt(Me.cboADepartmentNo.SelectedValue)
            Dim divisionno As Integer = Generic.ToInt(Me.cboADivisionNo.SelectedValue)
            Dim sectionno As Integer = Generic.ToInt(Me.cboASectionNo.SelectedValue)
            Dim unitno As Integer = Generic.ToInt(Me.cboAUnitNo.SelectedValue)
            Dim costcenterno As Integer = Generic.ToInt(Me.cboACostCenterNo.SelectedValue)
            Dim locationno As Integer = Generic.ToInt(Me.cboALocationNo.SelectedValue)
            Dim projectno As Integer = Generic.ToInt(Me.cboAProjectNo.SelectedValue)
            Dim jobgradeno As Integer = 0 'Generic.ToInt(Me.cboAJobGradeNo.SelectedValue)
            Dim employeeclassno As Integer = Generic.ToInt(Me.cboAEmployeeClassNo.SelectedValue)
            Dim employeestatno As Integer = Generic.ToInt(Me.cboAEmployeeStatNo.SelectedValue)
            Dim rankno As Integer = Generic.ToInt(Me.cboARankNo.SelectedValue)
            Dim payclassno As Integer = Generic.ToInt(Me.cboAPayClassNo.SelectedValue)
            Dim Immediate As Integer = Generic.ToInt(Me.hifASuperiorNo.Value)
            Dim rateclassno As Integer = Generic.ToInt(Me.cboAEmployeeRateClassNo.SelectedValue)
            Dim plantillano As Integer = 0
            Dim salarygradeno As Integer = Generic.ToInt(Me.cboASalaryGradeNo.SelectedValue)
            Dim IsMWE As Boolean = Generic.ToBol(Me.txtIsMWE.Checked)            


            dt = SQLHelper.ExecuteDataTable("EHRANMass_WebSave", UserNo, hranmassno, hrantypeno, PreparationDate, Effectivity, DueDate, Reason, Description, IsTablebase, IsInPercent, _
                                            RateIncrease, IsReady, cpositionno, positionno, ctaskno, taskno, cfacilityNo, facilityNo, cgroupno, groupno, _
                                            cdepartmentno, departmentno, cdivisionno, divisionno, csectionno, sectionno, cunitno, unitno, ccostcenterno, costcenterno, _
                                            clocationno, locationno, cprojectno, projectno, cjobgradeno, jobgradeno, cemployeeclassno, employeeclassno, cemployeestatno, employeestatno, _
                                            crankno, rankno, cpayclassno, payclassno, cImmediate, Immediate, crateclassno, rateclassno, csalarygradeno, salarygradeno, _
                                            cplantillano, plantillano, IsMWE, PayLocNo)

            For Each row As DataRow In dt.Rows
                TransNo = Generic.ToInt(row("HRANMassNo"))
                RetVal = True
            Next

            If RetVal = True Then
                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim url As String = "EmpHRANMassEdit_Employee.aspx?id=" & TransNo
                    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                Else
                    Dim url As String = "EmpHRANMassEdit.aspx?id=" & TransNo
                    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                    'MessageBox.Success(MessageTemplate.SuccessSave, Me)
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


End Class
