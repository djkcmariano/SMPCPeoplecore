Imports clsLib
Imports System.Data

Partial Class SecuredManager_SelfTableOrgChartAppr
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer
    Dim w As Integer = 0
    Dim startvalue As String = ""
    Dim ApprovalStatNo As Integer
    Dim desc As String = ""

    Private Sub PopulateChart()
        Try
            w = cboView.SelectedValue
            If cboStartWith.SelectedValue = "" Then
                startvalue = Generic.ToStr(ViewState("PlantillaCode")) 'cboStartWith.Items.Item(1).Value
            Else
                startvalue = cboStartWith.SelectedValue
            End If


            'Dim ds As New DataSet
            'Dim dt As DataTable
            'If TransNo = 0 Then
            '    ds = SQLHelper.ExecuteDataSet("ETableOrg_WebLookup", UserNo)
            '    If ds.Tables.Count > 0 Then
            '        If ds.Tables(0).Rows.Count > 0 Then
            '            TransNo = Generic.ToInt(ds.Tables(0).Rows(0)("TableOrgNo"))
            '            Session("ApprovalStatNo") = Generic.ToInt(ds.Tables(0).Rows(0)("ApprovalStatNo"))
            '        End If
            '    End If
            'Else
            '    Session("ApprovalStatNo") = 2
            'End If

            Dim ds1 As New DataSet

            If TransNo > 0 Then
                'If Session("ApprovalStatNo") = 0 Then
                '    lnkRevise.Visible = False
                '    lnkPost.Visible = True
                'Else
                '    lnkRevise.Visible = False
                '    lnkPost.Visible = False
                'End If
                ds1 = SQLHelper.ExecuteDataSet("ETableOrg_Chart2", UserNo, TransNo, w, Generic.ToInt(chkIsMirrorView.Checked), Generic.ToInt(chkIsBridgeView.Checked), Generic.ToInt(chkIsClusterView.Checked), Generic.ToInt(chkIsBudgetSource.Checked), Generic.ToInt(chkIsReassignment.Checked))
                'dt = ds1.Tables(0)
            Else
                'lnkRevise.Visible = True
                'lnkPost.Visible = False
                ds1 = SQLHelper.ExecuteDataSet("EPlantilla_Chart2", UserNo, startvalue, w, Generic.ToInt(chkIsMirrorView.Checked), Generic.ToInt(chkIsBridgeView.Checked), Generic.ToInt(chkIsClusterView.Checked), Generic.ToInt(chkIsBudgetSource.Checked), Generic.ToInt(chkIsReassignment.Checked))
                'dt = ds1.Tables(0)
            End If

            If w = 1 Then
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
            'DataBoundOrganisationChart1.MaximumDepth = Generic.ToInt(cboLevelNo.SelectedValue)
            DataBoundOrganisationChart1.MaximumDepth = IIf(Generic.ToInt(cboLevelNo.SelectedValue) = 0, Generic.ToInt(ds1.Tables(1).Rows(0)(1)), Generic.ToInt(cboLevelNo.SelectedValue))

            If startvalue = "" Then
                DataBoundOrganisationChart1.StartValue = ds1.Tables(0).Rows(0)(0)
            Else
                DataBoundOrganisationChart1.StartValue = startvalue
            End If

            DataBoundOrganisationChart1.DataSource = ds1.Tables(0)
            DataBoundOrganisationChart1.DataBind()

            rInfo.DataSource = ds1.Tables(1)
            rInfo.DataBind()

            rVacant.DataSource = ds1.Tables(2)
            rVacant.DataBind()

            rOccupied.DataSource = ds1.Tables(3)
            rOccupied.DataBind()

            'rRetirement.DataSource = ds1.Tables(4)
            'rRetirement.DataBind()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        ApprovalStatNo = Generic.ToInt(Request.QueryString("ApprovalStatNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))

        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            Generic.PopulateDropDownList_Self(UserNo, Me, "Panel1", PayLocNo)
            cboLevelNo.Items.Add(New ListItem("MAX", "0"))
            For index As Integer = 1 To 25
                Dim li As New ListItem
                li.Value = index
                li.Text = index
                cboLevelNo.Items.Add(li)
            Next
            cboLevelNo.SelectedValue = "0"
            Try
                Dim ds As New DataSet
                ds = SQLHelper.ExecuteDataSet("ETableOrg_WebStart", UserNo, TransNo)
                cboStartWith.DataSource = ds.Tables(0)
                cboStartWith.DataValueField = "tNo"
                cboStartWith.DataTextField = "tDesc"
                cboStartWith.DataBind()
                ViewState("PlantillaCode") = Generic.ToStr(ds.Tables(1).Rows(0)(0))
            Catch ex As Exception

            End Try
        End If
        

        PopulateChart()
        PopulateTabHeader()
        'lbl.Text = "<b>" & desc & "</b>"

        'AddHandler DataBoundOrganisationChart1.ItemDropped, AddressOf DataBoundOrganisationChart1_ItemDropped

    End Sub

    Private Sub PopulateTabHeader()
        'Try
        '    Dim dt As DataTable
        '    dt = SQLHelper.ExecuteDataTable("ETableOrg_WebManager_Title", UserNo)
        '    For Each row As DataRow In dt.Rows
        '        lbl.Text = "<b>" & Generic.ToStr(row("tDesc")) & "</b>"
        '    Next
        'Catch ex As Exception

        'End Try
    End Sub

    'Private Function UpdateParent(xid As String, xparentid As String) As Integer
    '    Return SQLHelper.ExecuteNonQuery("ETableOrg_UpdateParent", UserNo, TransNo, xid, xparentid)
    'End Function

    'Private Function DataBoundOrganisationChart1_ItemDropped(sender As Object, DraggedItemID As String, DroppedItemID As String) As Boolean
    '    If UpdateParent(DraggedItemID, DroppedItemID) > 0 Then
    '        PopulateChart()
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    Protected Sub lnkView_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Generic.ClearControls(Me, "Panel1")
        Generic.EnableControls(Me, "Panel1", False)
        'lnkSave.Enabled = False
        txtNoOfBox.Text = "1"
        txtNoOfBox.Enabled = False
        PopulateData()
        ModalPopupExtender1.Show()
    End Sub

    'Private Function SaveRecord() As Integer
    '    Dim FacilityNo As Integer = Generic.ToInt(cboFacilityNo.SelectedValue)
    '    Dim GroupNo As Integer = Generic.ToInt(cboGroupNo.SelectedValue)
    '    Dim DivisionNo As Integer = Generic.ToInt(cboDivisionNo.SelectedValue)
    '    Dim DepartmentNo As Integer = Generic.ToInt(cboDepartmentNo.SelectedValue)
    '    Dim SectionNo As Integer = Generic.ToInt(cboSectionNo.SelectedValue)
    '    Dim UnitNo As Integer = Generic.ToInt(cboUnitNo.SelectedValue)
    '    Dim PositionNo As Integer = Generic.ToInt(cboPositionNo.SelectedValue)
    '    Dim PlantillaCode As String = txtPlantillaCode.Text
    '    Dim JobGradeNo As Integer = Generic.ToInt(cboJobGradeNo.SelectedValue)
    '    Dim IsAssistant As Integer = Generic.ToInt(chkIsAssistant.Checked)
    '    Dim CostCenterNo As Integer = Generic.ToInt(cboCostCenterNo.SelectedValue)
    '    Dim PlantillaTypeNo As Integer = Generic.ToInt(cboPlantillaTypeNo.SelectedValue)
    '    Dim MinSalary As Double = Generic.ToDec(txtMinSalary.Text)
    '    Dim MidSalary As Double = Generic.ToDec(txtMidSalary.Text)
    '    Dim MaxSalary As Double = Generic.ToDec(txtMaxSalary.Text)
    '    Dim NoOfBox As Integer = Generic.ToInt(txtNoOfBox.Text)

    '    Return Generic.ToInt(SQLHelper.ExecuteScalar("ETableOrgDeti_WebSave", UserNo, Generic.ToInt(txtCode.Text), TransNo, FacilityNo, GroupNo, DivisionNo,
    '                                 DepartmentNo, SectionNo, UnitNo, PositionNo, PlantillaCode, IsAssistant, txtParentPlantillaCode.Text, txtRemarks.Text.ToString,
    '                                 CostCenterNo, JobGradeNo,
    '                                 Generic.ToInt(chkIsFacHead.Checked), Generic.ToInt(chkIsGroHead.Checked), Generic.ToInt(chkIsDepHead.Checked), Generic.ToInt(chkIsDivHead.Checked), Generic.ToInt(chkIsUniHead.Checked), Generic.ToInt(chkIsSecHead.Checked), PlantillaTypeNo, MinSalary, MidSalary, MaxSalary, NoOfBox))

    'End Function

    Private Sub PopulateData()
        Try
            Dim obj As Object = SQLHelper.ExecuteScalar("SELECT PlantillaNo FROM EPlantilla WHERE PlantillaCode='" & hifPlantillaCode.Value & "'")
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPlantilla_WebOne", UserNo, Generic.ToInt(obj))
            Generic.PopulateData(Me, "Panel1", dt)
            'For Each row As DataRow In dt.Rows
            '    IsTop = Generic.ToBol(row("IsTop"))
            'Next

            'If IsTop Then
            '    lblReportingTo.Attributes("class") = "col-md-4 control-label has-space"
            '    txtParentPlantillaCode.CssClass = "form-control"
            '    txtPlantillaCode.ReadOnly = True
            'Else
            '    lblReportingTo.Attributes("class") = "col-md-4 control-label has-required"
            '    txtParentPlantillaCode.CssClass = "form-control required"
            'End If

            'If txtCode.Text > "" Then
            '    txtParentPlantillaCode.ReadOnly = False
            'Else
            '    txtParentPlantillaCode.ReadOnly = True
            'End If
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
                txtPlantillaCode.ReadOnly = True
            Else
                lblReportingTo.Attributes("class") = "col-md-4 control-label has-required"
                txtParentPlantillaCode.CssClass = "form-control required"
            End If

            If txtCode.Text > "" Then
                txtParentPlantillaCode.ReadOnly = False
            Else
                txtParentPlantillaCode.ReadOnly = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboPositionNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Dim ds As New DataSet
        ds = SQLHelper.ExecuteDataSet("EPosition_WebOne", UserNo, Generic.ToInt(cboPositionNo.SelectedValue))
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                txtMinSalary.Text = Generic.ToDec(ds.Tables(0).Rows(0)("MinSalary"))
                txtMidSalary.Text = Generic.ToDec(ds.Tables(0).Rows(0)("MidSalary"))
                txtMaxSalary.Text = Generic.ToDec(ds.Tables(0).Rows(0)("MaxSalary"))
                cboJobGradeNo.Text = Generic.ToStr(ds.Tables(0).Rows(0)("JobGradeNo"))
            End If
        End If

        ModalPopupExtender1.Show()
    End Sub


    Protected Sub txtParentPlantillaCode_TextChanged(sender As Object, e As System.EventArgs) Handles txtParentPlantillaCode.TextChanged
        PopulateDataInherit(Generic.ToStr(txtParentPlantillaCode.Text))
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub cbo_SelectedIndexChange(sender As Object, e As System.EventArgs)
        PopulateChart()
    End Sub

    Protected Sub lnkName_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender

        EmpInfo1.xID = Generic.ToInt(lnk.CommandArgument)
        EmpInfo1.Show()
    End Sub

    Protected Sub lnkJob_Click(sender As Object, e As EventArgs)
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

        Response.Redirect("~/securedManager/SelfHRAN_Edit.aspx?ItemNo=" & itemno & "&MenuType=1906000000")
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

        Response.Redirect("~/securedManager/SelfAppMREditAppr.aspx?ItemNo=" & itemno & "&MenuType=1902000000")
    End Sub

    Protected Sub lnkGenerate_Click(sender As Object, e As System.EventArgs)
        Dim i As Integer = 0
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EMRAnnualExpiry_WebValidate", UserNo, PayLocNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("RetVal"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If


        If SQLHelper.ExecuteNonQuery("EMRAnnual_WebGenerate", UserNo, PayLocNo) > 0 Then
            i = i + 1
        End If

        If i > 0 Then
            MessageBox.SuccessResponse("Vacant Plantilla No.s has been successfully generated in Annual Manpower Plan", Me, "SelfAppMRAnnualListAppr.aspx")
        Else
            MessageBox.Information("No vacant Plantilla No.!", Me)
        End If


    End Sub
End Class
