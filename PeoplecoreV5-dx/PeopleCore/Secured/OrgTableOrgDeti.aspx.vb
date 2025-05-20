Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_OrgTableOrgDeti
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer
    Dim w As Integer = 0
    Dim startvalue As String = ""
    Dim ApprovalStatNo As Integer
    Dim desc As String = ""
    Dim AutoNumberTO As Boolean = ConfigurationManager.AppSettings("AutoNumberTO")

    Private Sub PopulateChart()
        Try
            w = cboView.SelectedValue
            'w = Generic.ToInt(Session("with"))
            'If w = 0 Then
            '    rdoWithOut.Checked = True
            '    rdoWith.Checked = False
            'Else
            '    rdoWithOut.Checked = False
            '    rdoWith.Checked = True
            'End If

            startvalue = Generic.ToStr(cboStartWith.SelectedValue)

            Dim ds As New DataSet
            Dim dt As DataTable
            ds = SQLHelper.ExecuteDataSet("ETableOrg_Chart2", UserNo, TransNo, w, Generic.ToInt(chkIsMirrorView.Checked), Generic.ToInt(chkIsBridgeView.Checked), Generic.ToInt(chkIsClusterView.Checked), Generic.ToInt(chkIsBudgetSource.Checked), Generic.ToInt(chkIsReassignment.Checked))
            dt = ds.Tables(0)
            If w = 2 Then
                DataBoundOrganisationChart1.AssistantItem.Size = OrgChart.Core.BackgroundImageSize.Large
                DataBoundOrganisationChart1.ChartItem.Size = OrgChart.Core.BackgroundImageSize.Large
                DataBoundOrganisationChart1.StackItem.Size = OrgChart.Core.BackgroundImageSize.Medium
            Else
                DataBoundOrganisationChart1.AssistantItem.Size = OrgChart.Core.BackgroundImageSize.Medium
                DataBoundOrganisationChart1.ChartItem.Size = OrgChart.Core.BackgroundImageSize.Medium
                DataBoundOrganisationChart1.StackItem.Size = OrgChart.Core.BackgroundImageSize.Small
            End If

            DataBoundOrganisationChart1.StackItem.ShowStackItems = chkStack.Checked
            DataBoundOrganisationChart1.StackItem.StackDepth = Generic.ToInt(cboLevelNo.SelectedValue)
            DataBoundOrganisationChart1.MaximumDepth = IIf(Generic.ToInt(cboLevelNo.SelectedValue) = 0, Generic.ToInt(ds.Tables(1).Rows(0)(1)), Generic.ToInt(cboLevelNo.SelectedValue))

            If Generic.ToInt(cboLevelNo.SelectedValue) = 0 Then
                DataBoundOrganisationChart1.MaximumDepth = 25
            End If

            If startvalue = "" Then
                DataBoundOrganisationChart1.StartValue = dt.Rows(0)(0)
            Else
                DataBoundOrganisationChart1.StartValue = startvalue
            End If


            DataBoundOrganisationChart1.AssistantItem.Colour = cboStaffColor.SelectedValue
            DataBoundOrganisationChart1.DataSource = dt
            DataBoundOrganisationChart1.DataBind()

            rInfo.DataSource = ds.Tables(1)
            rInfo.DataBind()

            rVacant.DataSource = ds.Tables(2)
            rVacant.DataBind()

            rOccupied.DataSource = ds.Tables(3)
            rOccupied.DataBind()

            rRetirement.DataSource = ds.Tables(4)
            rRetirement.DataBind()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        ApprovalStatNo = Generic.ToInt(Request.QueryString("ApprovalStatNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        desc = Generic.ToStr(Request.QueryString("Desc"))

        'Auto Numbering is enabled
        'If AutoNumberTO Then
        '    txtPlantillaCode.Attributes.Add("Placeholder", "Autonumber")
        '    txtPlantillaCode.ReadOnly = True
        '    txtPlantillaCode.Enabled = False
        'Else
        '    lblPlantillaCode.Attributes.Add("class", "col-md-4 control-label has-required")
        '    txtPlantillaCode.CssClass = "form-control required number"
        'End If

        lblPlantillaCode.Attributes.Add("class", "col-md-4 control-label has-required")
        txtPlantillaCode.CssClass = "form-control required number"

        AccessRights.CheckUser(UserNo, "OrgTableOrg.aspx", "ETableOrg")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            cboLevelNo.Items.Add(New ListItem("MAX", "0"))
            For index As Integer = 1 To 25
                Dim li As New ListItem
                li.Value = index
                li.Text = index
                cboLevelNo.Items.Add(li)
            Next
            cboLevelNo.SelectedValue = "0"
            PopulateDropDown()

            '--------------
            For Each item As ListItem In cboMinSalaryGradeNo.Items
                If item.Text = "-- Select --" Then
                    item.Text = "From"
                End If
            Next

            For Each item As ListItem In cboMaxSalaryGradeNo.Items
                If item.Text = "-- Select --" Then
                    item.Text = "To"
                End If
            Next
            '--------------
            
            Dim itemValues As Array = System.[Enum].GetValues(GetType(OrgChart.Core.BackgroundImageColour))
            Dim itemNames As Array = System.[Enum].GetNames(GetType(OrgChart.Core.BackgroundImageColour))

            For i As Integer = 0 To itemNames.Length - 1
                Dim item As New ListItem(itemNames(i), itemValues(i))
                cboStaffColor.Items.Add(item)
            Next

        End If

        If ApprovalStatNo = 1 Then
            lnkAdd.Visible = True
            lnkEdit.Visible = True
            lnkDelete.Visible = True
            'brAdd.Text = "<br />"
            brEdit.Text = "<br />"
            brDelete.Text = "<br />"
            lnkSave.Visible = True
        Else
            lnkAdd.Visible = False
            lnkEdit.Visible = False
            lnkDelete.Visible = False
            lnkSave.Visible = False
            'brAdd.Text = ""
            brEdit.Text = ""
            brDelete.Text = ""
        End If

        PopulateChart()
        lbl.Text = "<b>" & desc & "</b>"

        AddHandler DataBoundOrganisationChart1.ItemDropped, AddressOf DataBoundOrganisationChart1_ItemDropped

    End Sub

    Private Function UpdateParent(xid As String, xparentid As String) As Integer
        Return SQLHelper.ExecuteNonQuery("ETableOrg_UpdateParent", UserNo, TransNo, xid, xparentid)
    End Function

    Private Function DataBoundOrganisationChart1_ItemDropped(sender As Object, DraggedItemID As String, DroppedItemID As String) As Boolean
        If UpdateParent(DraggedItemID, DroppedItemID) > 0 Then
            PopulateChart()
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "OrgTableOrg.aspx", "ETableOrg") Then
            Dim lnk As New LinkButton
            lnk = sender
            Generic.ClearControls(Me, "Panel1")
            txtNoOfBox.Text = "1"
            txtNoOfBox.Enabled = True
            txtMinSalary.Enabled = False
            txtMidSalary.Enabled = False
            txtMaxSalary.Enabled = False
            cboJobGradeNo.Enabled = False
            Generic.EnableControls(Me, "Panel1", True)
            PopulateListBox()
            PopulateDataInherit(Generic.ToStr(hifPlantillaCode.Value))
            txtParentPlantillaCode.Text = Generic.ToStr(hifPlantillaCode.Value)
            'txtPlantillaCode.ReadOnly = False
            lnkSave.Enabled = True
            ModalPopupExtender1.Show()
            'If chkIsMirror.Checked Then
            '    cboPlantillaGovTypeNo.Enabled = False
            '    ListBox1.Enabled = False
            'Else
            '    cboPlantillaGovTypeNo.Enabled = True
            '    ListBox1.Enabled = True
            'End If
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "OrgTableOrg.aspx", "ETableOrg") Then
            Dim lnk As New LinkButton
            lnk = sender

            Generic.ClearControls(Me, "Panel1")

            txtNoOfBox.Text = "1"
            txtNoOfBox.Enabled = False
            txtMinSalary.Enabled = False
            txtMidSalary.Enabled = False
            txtMaxSalary.Enabled = False
            cboJobGradeNo.Enabled = False
            PopulateData(hifPlantillaCode.Value)
            PopulateListBox()
            PopulateActionType()
            Generic.EnableControls(Me, "Panel1", True)
            lnkSave.Enabled = True
            Populate_chkMirror()
            ModalPopupExtender1.Show()
        End If
    End Sub

    Protected Sub lnkView_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Generic.ClearControls(Me, "Panel1")        
        lnkSave.Enabled = False
        txtNoOfBox.Text = "1"
        txtNoOfBox.Enabled = False
        PopulateData(hifPlantillaCode.Value)
        PopulateActionType()
        Generic.EnableControls(Me, "Panel1", False)
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "OrgTableOrg.aspx", "ETableOrg") Then
            Dim lnk As New LinkButton
            lnk = sender

            If Generic.ToInt(SQLHelper.ExecuteScalar("ETableOrgDeti_ValidateDelete", UserNo, TransNo, hifPlantillaCode.Value)) = 1 Then
                MessageBox.Warning("Cannot delete item with incumbent.", Me)
            Else
                Dim str As String = Generic.ToStr(SQLHelper.ExecuteScalar("ETableOrgDeti_Delete", UserNo, TransNo, hifPlantillaCode.Value))
                If str.Length > 0 Then
                    MessageBox.Warning(str, Me)
                Else
                    PopulateChart()
                    MessageBox.Success("(1) " & MessageTemplate.SuccessDelete, Me)
                End If
            End If
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        'Dim i As String = ""
        'For Each item As ListEditItem In ListBox1.Items
        '    i = i & item.Value & " "
        'Next

        'MessageBox.Alert(i, "information", Me)
        'ModalPopupExtender1.Show()

        'foreach(ListEditItem item in items) {
        '     if(item.Selected) {
        '         // do something...
        '     }
        ' }
        'Dim count As Integer = 0
        'For Each item As ListEditItem In ListBox1.Items
        '    If item.Selected Then
        '        count = count + 1
        '    End If
        'Next
        Dim mirrorcount As Integer = 0
        Dim bridgecount As Integer = 0
        Dim clustercount As Integer = 0
        Dim budgetcount As Integer = 0
        For Each item As ListEditItem In ListBox1.Items
            If item.Selected Then
                If Generic.Split(item.Value, 1) = "1" Then
                    mirrorcount = mirrorcount + 1
                ElseIf Generic.Split(item.Value, 1) = "2" Then
                    bridgecount = bridgecount + 1
                ElseIf Generic.Split(item.Value, 1) = "3" Then
                    clustercount = clustercount + 1
                ElseIf Generic.Split(item.Value, 1) = "4" Then
                    budgetcount = budgetcount + 1
                End If
            End If
        Next

        If mirrorcount > 1 Then
            MessageBox.Alert("Mirrored position must not exceed to 1 item", "warning", Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If
        If bridgecount > 3 Or clustercount > 3 Or budgetcount > 3 Then
            MessageBox.Alert("Binded position must not exceed to 3 items.", "warning", Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        'If Len(txtPlantillaCode.Text) <> 8 Then
        '    MessageBox.Alert("Plantilla No. should be 8 numeric char only.", "warning", Me)
        '    ModalPopupExtender1.Show()
        '    Exit Sub
        'End If

        Dim dt As DataTable = SaveRecord()
        Dim RetVal As Integer, Msg As String = ""
        Dim TransDetiNo As Integer = 0

        For Each row As DataRow In dt.Rows
            RetVal = Generic.ToInt(row("RetVal"))
            Msg = Generic.ToStr(row("Msg"))
            TransDetiNo = Generic.ToInt(row("TableOrgDetiNo"))
        Next

        If RetVal = 1 Then
            'Delete and add new list of item no.
            SQLHelper.ExecuteNonQuery("ETableOrgMirror_WebDelete", UserNo, TransDetiNo)
            For Each item As ListEditItem In ListBox1.Items
                If item.Selected Then
                    SQLHelper.ExecuteNonQuery("ETableOrgMirror_WebSave", UserNo, TransDetiNo, TransNo, txtPlantillaCode.Text, Generic.Split(item.Value, 0))
                End If
            Next

            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Generic.ClearControls(Me, "Panel1")
            PopulateDropDown()
            PopulateChart()
            ModalPopupExtender1.Hide()
            'ElseIf RetVal = 2 Then
            '    MessageBox.Warning("Item No. already exists", Me)
            'ElseIf RetVal = 3 Then
            '    MessageBox.Warning("No changes made.", Me)
            'ElseIf RetVal = 4 Then
            '    MessageBox.Warning("Executive Assistant must not have sub item.", Me)
        ElseIf RetVal <> 1 And RetVal > 0 Then
            MessageBox.Alert(Msg, "warning", Me)
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If


    End Sub

    Private Function SaveRecord() As DataTable
        Dim FacilityNo As Integer = Generic.ToInt(cboFacilityNo.SelectedValue)
        Dim GroupNo As Integer = Generic.ToInt(cboGroupNo.SelectedValue)
        Dim DivisionNo As Integer = Generic.ToInt(cboDivisionNo.SelectedValue)
        Dim DepartmentNo As Integer = Generic.ToInt(cboDepartmentNo.SelectedValue)
        Dim SectionNo As Integer = Generic.ToInt(cboSectionNo.SelectedValue)
        Dim UnitNo As Integer = Generic.ToInt(cboUnitNo.SelectedValue)
        Dim PositionNo As Integer = Generic.ToInt(cboPositionNo.SelectedValue)
        Dim PlantillaCode As String = txtPlantillaCode.Text
        Dim JobGradeNo As Integer = Generic.ToInt(cboJobGradeNo.SelectedValue)
        Dim IsAssistant As Integer = Generic.ToInt(chkIsAssistant.Checked)
        Dim CostCenterNo As Integer = Generic.ToInt(cboCostCenterNo.SelectedValue)
        Dim PlantillaTypeNo As Integer = Generic.ToInt(cboPlantillaTypeNo.SelectedValue)
        Dim MinSalary As Double = Generic.ToDec(txtMinSalary.Text)
        Dim MidSalary As Double = Generic.ToDec(txtMidSalary.Text)
        Dim MaxSalary As Double = Generic.ToDec(txtMaxSalary.Text)
        Dim NoOfBox As Integer = Generic.ToInt(txtNoOfBox.Text)
        Dim MinSalaryGradeNo As Integer = Generic.ToInt(cboMinSalaryGradeNo.SelectedValue)
        Dim MaxSalaryGradeNo As Integer = Generic.ToInt(cboMaxSalaryGradeNo.SelectedValue)
        Dim LocationNo As Integer = Generic.ToInt(cboLocationNo.SelectedValue)
        Dim AreaTypeNo As Integer = Generic.ToInt(cboAreaTypeNo.SelectedValue)
        Dim SalaryGradeNo As Integer = Generic.ToInt(cboSalaryGradeNo.SelectedValue)
        Dim str As String = UserNo & ", " & Generic.ToInt(txtCode.Text) & ", " & TransNo & ", " & FacilityNo & ", " & GroupNo & ", " & DivisionNo & ", " & DepartmentNo & ", " & 0 & ", " & UnitNo & ", " & PositionNo & ", " & PlantillaCode & ", " & IsAssistant & ", " & txtParentPlantillaCode.Text & ", " & txtRemarks.Text.ToString & ", " & CostCenterNo & ", " & JobGradeNo & ", " & Generic.ToInt(chkIsFacHead.Checked) & ", " & Generic.ToInt(chkIsGroHead.Checked) & ", " & Generic.ToInt(chkIsDepHead.Checked) & ", " & Generic.ToInt(chkIsDivHead.Checked) & ", " & Generic.ToInt(chkIsUniHead.Checked) & ", " & Generic.ToInt(chkIsSecHead.Checked) & ", " & PlantillaTypeNo & ", " & MinSalary & ", " & MidSalary & ", " & MaxSalary & ", " & NoOfBox & ", " & txtTitle.Text & ", " & txtPreviousPlantillaCode.Text & ", " & AutoNumberTO & ", " & chkIsMirror.Checked & ", " & Generic.ToInt(cboOccupationalGroupNo.SelectedValue) & ", " & MinSalaryGradeNo & ", " & MaxSalaryGradeNo

        SaveRecord = SQLHelper.ExecuteDataTable("ETableOrgDeti_WebSave", UserNo, Generic.ToInt(txtCode.Text), TransNo, FacilityNo, GroupNo, DivisionNo,
                                     DepartmentNo, SectionNo, UnitNo, PositionNo, PlantillaCode, IsAssistant, txtParentPlantillaCode.Text, txtRemarks.Text.ToString,
                                     CostCenterNo, JobGradeNo,
                                     Generic.ToInt(chkIsFacHead.Checked), Generic.ToInt(chkIsGroHead.Checked), Generic.ToInt(chkIsDepHead.Checked), Generic.ToInt(chkIsDivHead.Checked), Generic.ToInt(chkIsUniHead.Checked), Generic.ToInt(chkIsSecHead.Checked), PlantillaTypeNo, MinSalary, MidSalary, MaxSalary, NoOfBox, txtTitle.Text, txtPreviousPlantillaCode.Text, AutoNumberTO, Generic.ToInt(chkIsMirror.Checked), Generic.ToInt(cboOccupationalGroupNo.SelectedValue), MinSalaryGradeNo, MaxSalaryGradeNo, Generic.ToInt(cboPlantillaGovTypeNo.SelectedValue),
                                     LocationNo, AreaTypeNo, SalaryGradeNo, Generic.ToInt(chkIsManpowerPool.Checked), Generic.ToInt(cboTableOrgActionTypeNo.SelectedValue), txtEffectiveDate.Text, Generic.ToInt(cboTaskNo.SelectedValue), Generic.ToInt(chkIsHeadStaff.Checked))

    End Function

    Private Sub PopulateData(PlantillaCode As String)
        Try
            Dim dt As DataTable, IsTop As Boolean
            dt = SQLHelper.ExecuteDataTable("ETableOrgDeti_WebOne", TransNo, PlantillaCode)
            Generic.PopulateData(Me, "Panel1", dt)
            For Each row As DataRow In dt.Rows
                IsTop = Generic.ToBol(row("IsTop"))
            Next

            If IsTop Then
                lblReportingTo.Attributes("class") = "col-md-4 control-label has-space"
                txtParentPlantillaCode.CssClass = "form-control"
                If AutoNumberTO Then
                    txtPlantillaCode.ReadOnly = True
                Else
                    txtPlantillaCode.ReadOnly = False
                End If

            Else
                lblReportingTo.Attributes("class") = "col-md-4 control-label has-required"
                txtParentPlantillaCode.CssClass = "form-control required"
            End If

            If txtCode.Text > "" Then
                txtParentPlantillaCode.ReadOnly = False
            Else
                txtParentPlantillaCode.ReadOnly = True
            End If

            PopulateListBox()

            'clear list
            ListBox1.UnselectAll()
            Dim xdt As DataTable = SQLHelper.ExecuteDataTable("ETableOrgMirror_WebOne", UserNo, Generic.ToInt(dt.Rows(0)("TableOrgDetiNo")))
            For Each item As ListEditItem In ListBox1.Items
                For Each row As DataRow In xdt.Rows
                    If Generic.Split(item.Value, 0) = Generic.ToStr(row("PlantillaCode2")) Then
                        item.Selected = True
                    End If
                Next
            Next
            '    i = i & item.Value & " "
            'Next

            Populate_chkMirror()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDataInherit(PlantillaCode As String)
        Try
            Dim dt As DataTable, IsTop As Boolean
            dt = SQLHelper.ExecuteDataTable("ETableOrgDeti_WebOne_Inherit", TransNo, PlantillaCode)
            Generic.PopulateData(Me, "Panel1", dt)
            For Each row As DataRow In dt.Rows
                IsTop = Generic.ToBol(row("IsTop"))
            Next

            If IsTop Then
                lblReportingTo.Attributes("class") = "col-md-4 control-label has-space"
                txtParentPlantillaCode.CssClass = "form-control"
                If AutoNumberTO Then
                    txtPlantillaCode.ReadOnly = True
                Else
                    txtPlantillaCode.ReadOnly = False
                End If

            Else
                lblReportingTo.Attributes("class") = "col-md-4 control-label has-required"
                txtParentPlantillaCode.CssClass = "form-control required"
            End If

            If txtCode.Text > "" Then
                txtParentPlantillaCode.ReadOnly = False
            Else
                txtParentPlantillaCode.ReadOnly = True
            End If

            Populate_chkMirror()

        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub cboPositionNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
    '    Dim ds As New DataSet
    '    ds = SQLHelper.ExecuteDataSet("EPosition_WebOne", UserNo, Generic.ToInt(cboPositionNo.SelectedValue))
    '    If ds.Tables.Count > 0 Then
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            txtMinSalary.Text = Generic.ToDec(ds.Tables(0).Rows(0)("MinSalary"))
    '            txtMidSalary.Text = Generic.ToDec(ds.Tables(0).Rows(0)("MidSalary"))
    '            txtMaxSalary.Text = Generic.ToDec(ds.Tables(0).Rows(0)("MaxSalary"))
    '            cboJobGradeNo.Text = Generic.ToStr(ds.Tables(0).Rows(0)("JobGradeNo"))
    '        End If
    '    End If

    '    ModalPopupExtender1.Show()
    'End Sub


    Protected Sub cboSalaryGradeNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ESalaryGrade_WebOne", UserNo, Generic.ToInt(cboSalaryGradeNo.SelectedValue), PayLocNo)
        For Each row As DataRow In dt.Rows
            txtMinSalary.Text = Generic.ToDbl(row("MinimumSalaryM"))
            txtMidSalary.Text = Generic.ToDbl(row("MidpointSalaryM"))
            txtMaxSalary.Text = Generic.ToDbl(row("MaximumSalaryM"))
        Next
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub txtParentPlantillaCode_TextChanged(sender As Object, e As System.EventArgs) Handles txtParentPlantillaCode.TextChanged
        PopulateDataInherit(Generic.ToStr(txtParentPlantillaCode.Text))
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnkName_Click(sender As Object, e As EventArgs)
        'Info1.xIsApplicant = True
        Dim lnk As New LinkButton
        lnk = sender
        If Generic.ToInt(lnk.CommandArgument) > 0 Then
            EmpInfo1.xID = Generic.ToInt(lnk.CommandArgument)
            EmpInfo1.Show()
        End If

    End Sub

    Protected Sub lnkJob_Click(sender As Object, e As EventArgs)
        'Info1.xIsApplicant = True
        Dim lnk As New LinkButton
        lnk = sender

        JobProfile1.JDNo = Generic.ToInt(lnk.CommandArgument)
        JobProfile1.Show()
    End Sub

    Protected Sub lnkHRAN_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender

        Dim ds As New DataSet
        Dim retval As Integer
        Dim itemno As Integer
        Dim xMessage As String = ""
        Dim alertType As String = ""

        ds = SQLHelper.ExecuteDataSet("EPlantilla_WebOneTO", UserNo, Generic.ToStr(hifPlantillaCode.Value))
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                retval = Generic.ToInt(ds.Tables(0).Rows(0)("RetVal"))
                xMessage = Generic.ToStr(ds.Tables(0).Rows(0)("xMessage"))
                alertType = Generic.ToStr(ds.Tables(0).Rows(0)("alertType"))
                itemno = Generic.ToInt(ds.Tables(0).Rows(0)("PlantillaNo"))
            End If
        End If

        If retval > 0 Then
            MessageBox.Alert(xMessage, alertType, Me)
            Exit Sub
        End If
        Session("xMenuType") = "0202010000"
        Response.Redirect("~/secured/EmpHRANEdit.aspx?ItemNo=" & itemno)
    End Sub

    Protected Sub lnkMR_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender

        Dim ds As New DataSet
        Dim retval As Integer
        Dim itemno As Integer
        Dim xMessage As String = ""
        Dim alertType As String = ""

        ds = SQLHelper.ExecuteDataSet("EPlantilla_WebOneTO", UserNo, Generic.ToStr(hifPlantillaCode.Value))
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                retval = Generic.ToInt(ds.Tables(0).Rows(0)("RetVal"))
                xMessage = Generic.ToStr(ds.Tables(0).Rows(0)("xMessage"))
                alertType = Generic.ToStr(ds.Tables(0).Rows(0)("alertType"))
                itemno = Generic.ToInt(ds.Tables(0).Rows(0)("PlantillaNo"))
            End If
        End If

        If retval > 0 Then
            MessageBox.Alert(xMessage, alertType, Me)
            Exit Sub
        End If

        Session("xMenuType") = "0102010000"
        Response.Redirect("~/secured/AppMREdit.aspx?ItemNo=" & itemno)
    End Sub

    Protected Sub cbo_SelectedIndexChange(sender As Object, e As System.EventArgs)
        PopulateChart()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboStartWith.DataSource = SQLHelper.ExecuteDataSet("ETableOrg_WebStart", UserNo, TransNo)
            cboStartWith.DataValueField = "tNo"
            cboStartWith.DataTextField = "tDesc"
            cboStartWith.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub chkIsMirror_CheckedChanged(sender As Object, e As EventArgs)
        Populate_chkMirror()
        ModalPopupExtender1.Show()
    End Sub

    Private Sub Populate_chkMirror()
        If chkIsMirror.Checked = True Then
            'txtPreviousPlantillaCode.Text = ""
            'txtPreviousPlantillaCode.ReadOnly = True
            ListBox1.Enabled = False
            cboPlantillaGovTypeNo.Enabled = True
            ListBox1.UnselectAll()
        Else
            ListBox1.Enabled = True
            cboPlantillaGovTypeNo.Enabled = False
            'txtPreviousPlantillaCode.ReadOnly = False
        End If
    End Sub

    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("window.open('OrgTableOrgDeti2.aspx?id=" & TransNo & "&with=" & w & "&start=" & DataBoundOrganisationChart1.StartValue & "&stack=" & DataBoundOrganisationChart1.StackItem.ShowStackItems & "&depth=" & DataBoundOrganisationChart1.MaximumDepth & "&is=" & Generic.ToInt(chkIsMirrorView.Checked) & "&ib=" & Generic.ToInt(chkIsBridgeView.Checked) & "&ic=" & Generic.ToInt(chkIsClusterView.Checked) & "&ibs=" & Generic.ToInt(chkIsBudgetSource.Checked) & "&color=" & cboStaffColor.SelectedValue & "&ir=" & Generic.ToInt(chkIsReassignment.Checked) & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,resizable=yes,scrollbars=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())

    End Sub

    Protected Sub lnkPositionChart_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("window.open('OrgTablePositionChart.aspx?id=" & TransNo & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,resizable=yes,scrollbars=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())

    End Sub

    Protected Sub lnkHierarchy_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("window.open('OrgTableHierarchyChart.aspx?id=" & TransNo & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,resizable=yes,scrollbars=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())

    End Sub



    Private Sub PopulateListBox()
        ListBox1.DataSource = SQLHelper.ExecuteDataTable("ETableOrgMirror_Web", UserNo, TransNo, Generic.ToInt(txtCode.Text))
        ListBox1.ValueField = "PlantillaCode"        
        ListBox1.TextField = "PlantillaDesc"
        ListBox1.DataBind()
    End Sub

    Protected Sub DataBoundOrganisationChart1_ItemDataBound(sender As Object, e As OrgChart.Core.OrganisationChartItemEventArgs) Handles DataBoundOrganisationChart1.ItemDataBound
        'If e.Item.ItemType = OrgChart.Core.OrganisationItemType.ChartItem Then
        'Dim hif As New HiddenField
        'Dim hifColor As New HiddenField
        'hif = e.Item.FindControl("hif")
        'hifColor = e.Item.FindControl("hifColor")

        'Dim item As OrgChart.Core.Items.ChartItem
        'item.BackgroundImageStyle = 


        'Select Case hif.Value
        '    Case 1               
        '        'e.Item.ItemStyle.CssClass = "item"
        '        e.Item.ItemSettings.Colour = OrgChart.Core.BackgroundImageColour.Blue
        '        'e.Item.ItemStyle.BackColor = Drawing.Color.Black

        '        '        Case 2
        '        '            e.Item.BackColor = Drawing.Color.Beige
        '        '        Case 3
        '        '            e.Item.BackColor = Drawing.Color.Brown
        '    Case Else
        '        'e.Item.ItemStyle.BackColor = Drawing.Color.White
        'End Select

        'End If
    End Sub

    Protected Sub cboTableOrgActionTypeNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateActionType()
        ModalPopupExtender1.Show()
    End Sub

    Private Sub PopulateActionType()
        Dim IsEffectiveDate As Boolean = False
        IsEffectiveDate = Generic.ToBol(SQLHelper.ExecuteScalar("SELECT IsEffectiveDate FROM ETableOrgActionType WHERE TableOrgActionTypeNo=" & Generic.ToInt(cboTableOrgActionTypeNo.SelectedValue)))

        If IsEffectiveDate Then
            lbldate.Attributes.Add("class", "col-md-4 control-label has-required")
            txtEffectiveDate.CssClass = "form-control required"
            txtEffectiveDate.Enabled = True
        Else
            lbldate.Attributes.Add("class", "col-md-4 control-label has-space")
            txtEffectiveDate.CssClass = "form-control"
            txtEffectiveDate.Text = ""
            txtEffectiveDate.Enabled = False
        End If
    End Sub
End Class
