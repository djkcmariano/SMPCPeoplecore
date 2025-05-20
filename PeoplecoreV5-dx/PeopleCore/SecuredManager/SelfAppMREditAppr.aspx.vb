Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class SecuredManager_SelfAppMREditAppr
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim clsGeneric As New clsGenericClass
    Dim ItemNo As Integer

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)

        ViewState("IsEnabled") = True
        EnabledControls()

    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMR_WebOne", UserNo, TransNo)
        Generic.PopulateData(Me, "Panel1", dt)

        If TransNo = 0 Then
            dt = SQLHelper.ExecuteDataTable("EEmployee_WebOne", UserNo, Session("EmployeeNo"))
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
    Private Sub populateCombo()
        Try
            lstPlantilla.DataSource = SQLHelper.ExecuteDataSet("EPlantilla_WebLookup_Manager", UserNo, Generic.ToInt(cboPositionNo.SelectedValue)) 'clsGeneric.xLookup_Table(UserNo, "EPlantillaL", Session("xPayLocNo"))
            lstPlantilla.ValueField = "tNo"
            lstPlantilla.TextField = "tDesc"
            lstPlantilla.DataBind()

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
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        ItemNo = Generic.ToInt(Request.QueryString("ItemNo"))

        If ItemNo > 0 Then
            Session("xMenutype") = Generic.ToStr(Request.QueryString("MenuType"))
        End If

        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateData()
            populateCombo()
            PopulateTabHeader()
            If ItemNo > 0 Then
                PopulateItemNo(ItemNo)
            End If            
        End If

        EnabledControls()

    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", Enabled)
        txtMRCode.Enabled = False
        lstPlantilla.ReadOnly = Not Enabled
        lnkModify.Visible = Not Enabled
        lnkSave.Visible = Enabled

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
        Dim RequestedBy As Integer = Generic.ToInt(Session("EmployeeNo"))
        Dim requesteddate As String = Generic.ToStr(txtRequestedDate.Text)
        Dim NeededDate As String = Generic.ToStr(txtNeededDate.Text)
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
        Dim SalaryGradeNo As Integer = Generic.ToInt(cboSalaryGradeNo.SelectedValue)

        Dim Str As String = UserNo & ", " & TransNo & ", " & RequestedBy & ", " & requesteddate & ", " & NeededDate & ", " & MRTypeNo & ", " & MRReasonNo & ", " & Remarks & ", " & IsForPooling & ", " & NoOfVacancy & ", " & PositionNo & ", " & JobGradeNo & ", " & FacilityNo & ", " & UnitNo & ", " & DepartmentNo & ", " & GroupNo & ", " & DivisionNo & ", " & SectionNo & ", " & CostcenterNo & ", " & LocationNo & ", " & EmployeeStatNo & ", " & EmployeeClassNo & ", " & Session("xPayLocNo")

        dt = SQLHelper.ExecuteDataTable("EMR_WebSaveSelf", UserNo, TransNo, RequestedBy, requesteddate, NeededDate, MRTypeNo, MRReasonNo, Remarks, IsForPooling, NoOfVacancy, PositionNo, TaskNo, JobGradeNo, FacilityNo, UnitNo, DepartmentNo, GroupNo, DivisionNo, SectionNo, CostcenterNo, LocationNo, EmployeeStatNo, EmployeeClassNo, Session("xPayLocNo"), SalaryGradeNo)

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
                Dim url As String = "selfappmreditAppr.aspx?id=" & TransNo
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

    Protected Sub cboSectionNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            cboPositionNo.DataSource = SQLHelper.ExecuteDataSet("EPosition_WebLookup", UserNo, Generic.ToInt(cboSectionNo.SelectedValue))
            cboPositionNo.DataValueField = "tNo"
            cboPositionNo.DataTextField = "tDesc"
            cboPositionNo.DataBind()
        Catch ex As Exception

        End Try
        populateCombo()
    End Sub

    Protected Sub txtIsForPooling_CheckedChanged(sender As Object, e As System.EventArgs) Handles txtIsForPooling.CheckedChanged
        If txtIsForPooling.Checked Then
            txtNoOfVacancy.Text = ""
            txtNoOfVacancy.Enabled = False
        Else
            txtNoOfVacancy.Enabled = True
        End If
    End Sub

    Private Sub PopulateItemNo(ByVal PlantillaNo As Integer)
        Try

        Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataTable("EPlantilla_WebOne", UserNo, PlantillaNo)
            For Each row As DataRow In dt.Rows
                cboGroupNo.Text = Generic.ToStr(row("GroupNo"))
                cboDivisionNo.Text = Generic.ToStr(row("DivisionNo"))
                cboDepartmentNo.Text = Generic.ToStr(row("DepartmentNo"))
                cboFacilityNo.Text = Generic.ToStr(row("FacilityNo"))
                cboSectionNo.Text = Generic.ToStr(row("SectionNo"))
                cboPositionNo.Text = Generic.ToStr(row("PositionNo"))
                cboLocationNo.Text = Generic.ToStr(row("LocationNo"))
                cboCostCenterNo.Text = Generic.ToInt(row("CostCenterNo"))
                cboTaskNo.Text = Generic.ToStr(row("TaskNo"))
                cboSalaryGradeNo.Text = Generic.ToInt(row("SalaryGradeNo"))
            Next
            lstPlantilla.DataSource = SQLHelper.ExecuteDataSet("EPlantilla_WebLookup_Manager", UserNo, Generic.ToInt(cboPositionNo.SelectedValue)) 'clsGeneric.xLookup_Table(UserNo, "EPlantillaL", Session("xPayLocNo"))
            lstPlantilla.ValueField = "tNo"
            lstPlantilla.TextField = "tDesc"
            lstPlantilla.DataBind()

            lstPlantilla.UnselectAll()
            For Each item As ListEditItem In lstPlantilla.Items
                For Each row As DataRow In dt.Rows
                    If Generic.Split(item.Value, 0) = Generic.ToStr(row("PlantillaNo")) Then
                        item.Selected = True
                    End If
                Next
            Next

        Catch ex As Exception

        End Try
    End Sub

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
            dt = SQLHelper.ExecuteDataTable("EEmployee_WebOne", UserNo, Session("EmployeeNo"))
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
