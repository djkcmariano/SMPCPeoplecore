Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_AppMREdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim clsGeneric As New clsGenericClass
    Dim paylocNo As Integer = 0

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "AppMREdit.aspx", "EMR") Then
            ViewState("IsEnabled") = True
            EnabledControls()
            txtDatePublishedTo.Enabled = True
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMR_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            populateCombo()
            txtDatePublishedTo.Enabled = True
        Next


    End Sub
    Private Sub populateCombo()

        Try
            lstPlantilla.DataSource = SQLHelper.ExecuteDataSet("EPlantilla_WebLookup", UserNo, Generic.ToInt(cboPositionNo.SelectedValue), paylocNo) 'clsGeneric.xLookup_Table(UserNo, "EPlantillaL", Session("xPayLocNo"))
            lstPlantilla.ValueField = "tNo"
            lstPlantilla.TextField = "tDesc"
            lstPlantilla.DataBind()

            'For Each i As ListItem In lstPlantilla.Items
            '    ds = SQLHelper.ExecuteDataSet("Select Plantillano from dbo.EMRPlantilla where MRNo=" & TransNo & "PlantillaNo=" & CInt(i.Value))
            '    If i.Value = WantedValue Then
            '        i.Selected = True
            '    End If
            'Next

            'If ds.Tables.Count > 0 Then
            '    If ds.Tables(0).Rows.Count > 0 Then
            '        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            '            lstPlantilla.Items.FindByValue(ds.Tables(0).Rows(i)("PlantillaNo")).Selected = True
            '        Next

            '    End If
            'End If

            lstPlantilla.UnselectAll()
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("Select PlantillaNo from dbo.EMRPlantilla where MRNo=" & TransNo)
            For Each item As ListEditItem In lstPlantilla.Items
                For Each row As DataRow In dt.Rows
                    If Generic.Split(item.Value, 0) = Generic.ToStr(row("PlantillaNo")) Then
                        item.Selected = True
                    End If
                Next
            Next
           
            Dim obj As Object = SQLHelper.ExecuteScalar("SELECT EmployeeClassNo FROM EPosition WHERE PositionNo=" & Generic.ToInt(cboPositionNo.SelectedValue))
            cboEmployeeClassNo.SelectedValue = Generic.ToInt(obj)

        Catch ex As Exception
        End Try

        'Try
        '    cboApplicantStandardHeaderNo.DataSource = SQLHelper.ExecuteDataSet("EApplicantStandardHeader_WebLookup", UserNo, Generic.ToInt(cboPositionNo.SelectedValue), 1, Generic.ToInt(Session("xPayLocNo")))
        '    cboApplicantStandardHeaderNo.DataValueField = "tNo"
        '    cboApplicantStandardHeaderNo.DataTextField = "tDesc"
        '    cboApplicantStandardHeaderNo.DataBind()
        'Catch ex As Exception
        'End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        paylocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        Session("itemno") = Generic.ToInt(Request.QueryString("ItemNo"))

        'AccessRights.CheckUser(UserNo)
        AccessRights.CheckUser(UserNo, "AppMREdit.aspx", "EMR")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))            
            PopulateTabHeader()
            populateCombo()
            PopulateData()

            Try
                cboApplicantStandardHeaderNo.DataSource = SQLHelper.ExecuteDataSet("EApplicantStandardHeader_WebLookup", UserNo, Generic.ToInt(cboPositionNo.SelectedValue), 1, Generic.ToInt(Session("xPayLocNo")))
                cboApplicantStandardHeaderNo.DataValueField = "tNo"
                cboApplicantStandardHeaderNo.DataTextField = "tDesc"
                cboApplicantStandardHeaderNo.DataBind()
            Catch ex As Exception
            End Try

            If Session("itemno") > 0 Then
                PopulateItemNo(Session("itemno"))
            End If
        End If

        EnabledControls()

        If TransNo = 0 Then
            txtDatePublishedTo.Enabled = True
        End If

    End Sub

    Private Sub PopulateItemNo(ByVal PlantillaNo As Integer)
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EPlantilla_WebOne", UserNo, PlantillaNo)
        For Each row As DataRow In dt.Rows
            cboUnitNo.Text = Generic.ToStr(row("UnitNo"))
            cboDivisionNo.Text = Generic.ToStr(row("DivisionNo"))
            cboDepartmentNo.Text = Generic.ToStr(row("DepartmentNo"))
            cboFacilityNo.Text = Generic.ToStr(row("FacilityNo"))
            cboSectionNo.Text = Generic.ToStr(row("SectionNo"))

            hifRequestedBy.Value = Generic.ToStr(row("ImmediateSuperiorNo"))
            txtRFullname.Text = Generic.ToStr(row("SFullName"))

            cboPositionNo.Text = Generic.ToStr(row("PositionNo"))
            cboLocationNo.Text = Generic.ToStr(row("LocationNo"))
            cboCostCenterNo.Text = Generic.ToInt(row("CostCenterNo"))
            cboTaskNo.Text = Generic.ToStr(row("TaskNo"))
            cboSalaryGradeNo.Text = Generic.ToInt(row("SalaryGradeNo"))


            lstPlantilla.DataSource = SQLHelper.ExecuteDataSet("EPlantilla_WebLookup", UserNo, Generic.ToInt(row("PositionNo"))) 'clsGeneric.xLookup_Table(UserNo, "EPlantillaL", Session("xPayLocNo"))
            lstPlantilla.ValueField = "tNo"
            lstPlantilla.TextField = "tDesc"
            lstPlantilla.DataBind()
        Next

        lstPlantilla.UnselectAll()
        For Each item As ListEditItem In lstPlantilla.Items
            For Each row As DataRow In dt.Rows
                If Generic.Split(item.Value, 0) = Generic.ToStr(row("PlantillaNo")) Then
                    item.Selected = True
                End If
            Next
        Next
    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", Enabled)
        txtMRCode.Enabled = False
        lstPlantilla.ReadOnly = Not Enabled
        lnkModify.Visible = Not Enabled
        lnkSave.Visible = Enabled

        lnkModify3.Visible = Not Enabled
        lnkSave3.Visible = Enabled

        txtDatePublishedTo.Enabled = False

        If Enabled = True Then
            PopulateForPooling()
        End If
  
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMR_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)        
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "AppMREdit.aspx", "EMR") Then
            If Generic.ToBol(txtIsForPooling.Checked) = False Then
                Dim itemCount As Integer = 0
                For Each item As ListEditItem In lstPlantilla.Items
                    If item.Selected Then
                        itemCount = itemCount + 1
                    End If
                Next

                If itemCount > Generic.ToInt(txtNoOfVacancy.Text) Then
                    MessageBox.Alert("Selected Manpower Item Number is greater than the number of vacancy!", "warning", Me)
                    Exit Sub
                End If
            End If


            'If CDate(txtNeededDate.Text) < Now.Date Then
            '    MessageBox.Alert("Antedate of Date Needed is not allowed!", "warning", Me)
            '    Exit Sub
            'End If

            Dim dt As DataTable
            Dim RetVal As Boolean = False
            Dim RequestedBy As Integer = Generic.ToInt(hifRequestedBy.Value)
            Dim requesteddate As String = Generic.ToStr(txtRequestedDate.Text)
            Dim NeededDate As String = Generic.ToStr(txtNeededDate.Text)
            Dim MRStatNo As Integer = Generic.ToInt(cboMRStatNo.SelectedValue)
            Dim MRTypeNo As Integer = Generic.ToInt(cboMRTypeNo.SelectedValue)
            Dim MRReasonNo As Integer = Generic.ToInt(cboMRReasonNo.SelectedValue)
            Dim Remarks As String = Generic.ToStr(txtRemarks.Text)
            Dim IsForPooling As Boolean = Generic.ToBol(txtIsForPooling.Checked)
            Dim NoOfVacancy As Integer = Generic.ToInt(txtNoOfVacancy.Text)
            Dim PositionNo As Integer = Generic.ToInt(cboPositionNo.SelectedValue)
            Dim TaskNo As Integer = Generic.ToInt(cboTaskNo.SelectedValue)
            Dim JobGradeNo As Integer = 0
            Dim FacilityNo As Integer = Generic.ToInt(cboFacilityNo.SelectedValue)
            Dim UnitNo As Integer = Generic.ToInt(cboUnitNo.SelectedValue)
            Dim DepartmentNo As Integer = Generic.ToInt(cboDepartmentNo.SelectedValue)
            Dim GroupNo As Integer = Generic.ToInt(cboGroupNo.SelectedValue)
            Dim DivisionNo As Integer = Generic.ToInt(cboDivisionNo.SelectedValue)
            Dim SectionNo As Integer = Generic.ToInt(cboSectionNo.SelectedValue)
            Dim CostcenterNo As Integer = Generic.ToInt(cboCostCenterNo.SelectedValue)
            Dim LocationNo As Integer = Generic.ToInt(cboLocationNo.SelectedValue)
            Dim EmployeeStatNo As Integer = Generic.ToInt(cboEmployeeStatNo.SelectedValue)
            Dim EmployeeClassNo As Integer = Generic.ToInt(cboEmployeeClassNo.SelectedValue)
            Dim ApprovalStatNo As Integer = Generic.ToInt(cboApprovalStatNo.SelectedValue)
            Dim ApplicantStandardHeaderNo As Integer = Generic.ToInt(cboApplicantStandardHeaderNo.SelectedValue)
            Dim ChecklistTemplateNo As Integer = Generic.ToInt(cboChecklistTemplateNo.SelectedValue)
            Dim IsOnline As Boolean = Generic.ToBol(chkIsOnline.Checked)
            Dim DatePublished As String = Generic.ToStr(txtDatePublished.Text)
            Dim DatePublishedTo As String = Generic.ToStr(txtDatePublishedTo.Text)

            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable

            dtx = SQLHelper.ExecuteDataTable("EMR_WebValidate", UserNo, TransNo, RequestedBy, requesteddate, NeededDate, MRStatNo, MRTypeNo, MRReasonNo, Remarks, IsForPooling, NoOfVacancy, PositionNo, TaskNo, JobGradeNo, FacilityNo, UnitNo, DepartmentNo, GroupNo, DivisionNo, SectionNo, CostcenterNo, LocationNo, EmployeeStatNo, EmployeeClassNo, ApprovalStatNo, ApplicantStandardHeaderNo, ChecklistTemplateNo, IsOnline, DatePublished, DatePublishedTo, PayLocNo)
            For Each xrow As DataRow In dtx.Rows
                invalid = Generic.ToBol(xrow("Invalid"))
                messagedialog = Generic.ToStr(xrow("MessageDialog"))
                alerttype = Generic.ToStr(xrow("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If


            dt = SQLHelper.ExecuteDataTable("EMR_WebSave", UserNo, TransNo, RequestedBy, requesteddate, NeededDate, MRStatNo, MRTypeNo, MRReasonNo, Remarks, IsForPooling, NoOfVacancy, PositionNo, TaskNo, JobGradeNo, FacilityNo, UnitNo, DepartmentNo, GroupNo, DivisionNo, SectionNo, CostcenterNo, LocationNo, EmployeeStatNo, EmployeeClassNo, ApprovalStatNo, ApplicantStandardHeaderNo, ChecklistTemplateNo, IsOnline, DatePublished, DatePublishedTo, paylocNo, Generic.ToInt(cboSalaryGradeNo.SelectedValue))

            For Each row As DataRow In dt.Rows
                TransNo = Generic.ToInt(row("MRNo"))
            Next

            If TransNo > 0 Then
                'save mr plantilla
                SaveMR_Plantilla(TransNo)
                RetVal = True
            End If

            If RetVal = True Then
                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim url As String = "appmredit.aspx?id=" & TransNo
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

    Private Sub SaveMR_Plantilla(mrNo As Integer)
        For Each item As ListEditItem In lstPlantilla.Items
            If item.Selected Then
                SQLHelper.ExecuteNonQuery("EMRPlantilla_WebSave", UserNo, 0, mrNo, item.Value, True)
            Else
                SQLHelper.ExecuteNonQuery("EMRPlantilla_WebDelete", UserNo, mrNo, item.Value)
            End If
        Next
    End Sub

    Protected Sub cboPositionNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        populateCombo()
    End Sub

    Protected Sub txtIsForPooling_CheckedChanged(sender As Object, e As System.EventArgs) Handles txtIsForPooling.CheckedChanged
        PopulateForPooling()
    End Sub

    Private Sub PopulateForPooling()
        If txtIsForPooling.Checked Then
            txtNoOfVacancy.Text = ""
            txtNoOfVacancy.Enabled = False
            txtNoOfVacancy.CssClass = "form-control"
        Else
            txtNoOfVacancy.Enabled = True
            txtNoOfVacancy.CssClass = "form-control required"
        End If
    End Sub


    Protected Sub hifRequestedBy_ValueChanged(sender As Object, e As System.EventArgs)
        Dim _dt As New DataTable
        _dt = SQLHelper.ExecuteDataTable("EEmployee_WebOne", UserNo, Generic.CheckDBNull(hifRequestedBy.Value, clsBase.clsBaseLibrary.enumObjectType.IntType))
        For Each row As DataRow In _dt.Rows
            cboFacilityNo.Text = Generic.ToStr(row("FacilityNo"))
            cboUnitNo.Text = Generic.ToStr(row("UnitNo"))
            cboDepartmentNo.Text = Generic.ToStr(row("DepartmentNo"))
            cboGroupNo.Text = Generic.ToStr(row("GroupNo"))
            cboDivisionNo.Text = Generic.ToStr(row("DivisionNo"))
            cboSectionNo.Text = Generic.ToStr(row("SectionNo"))

        Next

        'populateCombo()

    End Sub

    'Protected Sub txtDatePublished_TextChanged(sender As Object, e As System.EventArgs) Handles txtDatePublished.TextChanged
    '    If IsDate(txtDatePublished.Text) Then
    '        txtDatePublishedTo.Text = DateAdd(DateInterval.Day, 9, CDate(txtDatePublished.Text))
    '    End If
    'End Sub

    Protected Sub lstPlantilla_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles lstPlantilla.SelectedIndexChanged
        Dim dt As DataTable
        Dim Selected As Integer = 0
        Dim LastItemNo As Integer = 0

        For Each item As ListEditItem In lstPlantilla.Items
            If item.Selected = True Then
                Selected = Selected + 1
                LastItemNo = Generic.Split(item.Value, 0)
            End If
        Next

        If Selected = 1 Then
            dt = SQLHelper.ExecuteDataTable("EPlantilla_WebOne", UserNo, LastItemNo)
            For Each row As DataRow In dt.Rows
                cboFacilityNo.Text = Generic.ToStr(row("FacilityNo"))
                cboUnitNo.Text = Generic.ToStr(row("UnitNo"))
                cboDepartmentNo.Text = Generic.ToStr(row("DepartmentNo"))
                cboGroupNo.Text = Generic.ToStr(row("GroupNo"))
                cboDivisionNo.Text = Generic.ToStr(row("DivisionNo"))
                cboSectionNo.Text = Generic.ToStr(row("SectionNo"))
                cboLocationNo.Text = Generic.ToStr(row("LocationNo"))
                'cboCostCenterNo.Text = Generic.ToInt(row("CostCenterNo"))
                cboTaskNo.Text = Generic.ToStr(row("TaskNo"))
                cboSalaryGradeNo.Text = Generic.ToInt(row("SalaryGradeNo"))
            Next
        Else
            dt = SQLHelper.ExecuteDataTable("EEmployee_WebOne", UserNo, Generic.ToInt(hifRequestedBy.Value))
            For Each row As DataRow In dt.Rows
                cboFacilityNo.Text = Generic.ToStr(row("FacilityNo"))
                cboUnitNo.Text = Generic.ToStr(row("UnitNo"))
                cboDepartmentNo.Text = Generic.ToStr(row("DepartmentNo"))
                cboGroupNo.Text = Generic.ToStr(row("GroupNo"))
                cboDivisionNo.Text = Generic.ToStr(row("DivisionNo"))
                cboSectionNo.Text = Generic.ToStr(row("SectionNo"))
            Next
        End If
    End Sub
End Class
