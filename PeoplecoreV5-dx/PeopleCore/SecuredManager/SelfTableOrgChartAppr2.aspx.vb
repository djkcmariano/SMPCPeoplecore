Imports clsLib
Imports System.Data

Partial Class SecuredManager_SelfTableOrgChartAppr2
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim w As Integer = 0


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

            Dim ds As New DataSet
            Dim dt As DataTable
            ds = SQLHelper.ExecuteDataSet("ETableOrg_Chart2", UserNo, TransNo, w)
            dt = ds.Tables(0)
            If w = 2 Then
                DataBoundOrganisationChart1.AssistantItem.Size = OrgChart.Core.BackgroundImageSize.Large
                DataBoundOrganisationChart1.ChartItem.Size = OrgChart.Core.BackgroundImageSize.Large
                DataBoundOrganisationChart1.StackItem.Size = OrgChart.Core.BackgroundImageSize.Medium
            Else
                DataBoundOrganisationChart1.AssistantItem.Size = OrgChart.Core.BackgroundImageSize.Medium
                DataBoundOrganisationChart1.ChartItem.Size = OrgChart.Core.BackgroundImageSize.Medium
                DataBoundOrganisationChart1.StackItem.Size = OrgChart.Core.BackgroundImageSize.Medium
            End If

            DataBoundOrganisationChart1.StackItem.ShowStackItems = chkStack.Checked
            DataBoundOrganisationChart1.StackItem.StackDepth = Generic.ToInt(cboLevelNo.SelectedValue)
            DataBoundOrganisationChart1.MaximumDepth = Generic.ToInt(cboLevelNo.SelectedValue)

            DataBoundOrganisationChart1.StartValue = dt.Rows(0)(0)
            DataBoundOrganisationChart1.DataSource = dt
            DataBoundOrganisationChart1.DataBind()

            rInfo.DataSource = ds.Tables(1)
            rInfo.DataBind()

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        lbl.Text = Generic.ToStr(Request.QueryString("desc"))
        lTheme.Text = "<link rel='stylesheet' type='text/css' id='theme' href='../css/" & Generic.GetSkin() & "' />"
        'AccessRights.CheckUser(UserNo)
        PopulateChart()

        If Not IsPostBack Then
            Generic.PopulateDropDownList_Self(UserNo, Me, "Panel1", PayLocNo)
            For index As Integer = 2 To 25
                Dim li As New ListItem
                li.Value = index
                li.Text = index
                cboLevelNo.Items.Add(li)
            Next
        End If

        'If cboView.SelectedValue = "3" Then
        DataBoundOrganisationChart1.DragDrop.Enabled = False
        'End If

        AddHandler DataBoundOrganisationChart1.ItemDropped, AddressOf DataBoundOrganisationChart1_ItemDropped

    End Sub

    Protected Sub cboView_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateChart()
    End Sub


    Protected Sub lnkView_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Generic.ClearControls(Me, "Panel1")
        Generic.EnableControls(Me, "Panel1", False)
        txtNoOfBox.Enabled = False
        txtNoOfBox.Text = "1"
        PopulateData(lnk.CommandArgument)
        ModalPopupExtender1.Show()
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

    Protected Sub cboPositionNo_SelectedIndexChanged(sender As Object, e As EventArgs)
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

    Protected Sub cboSectionNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Dim ds As New DataSet
        ds = SQLHelper.ExecuteDataSet("ESection_WebOne", UserNo, Generic.ToInt(cboSectionNo.SelectedValue))
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                cboRegionNo.Text = Generic.ToStr(ds.Tables(0).Rows(0)("RegionNo"))
                cboLocationNo.Text = Generic.ToStr(ds.Tables(0).Rows(0)("LocationNo"))
            End If
        End If

        ModalPopupExtender1.Show()
    End Sub

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

    Private Sub MessageBox(message As String, owner As Control)
        Dim page As Page = If(TryCast(owner, Page), owner.Page)
        If page Is Nothing Then
            Return
        End If
        ScriptManager.RegisterStartupScript(page, page.[GetType](), "popup", (Convert.ToString("alert('") & message) + "');", True)
    End Sub

    Protected Sub txtParentPlantillaCode_TextChanged(sender As Object, e As System.EventArgs) Handles txtParentPlantillaCode.TextChanged
        PopulateDataInherit(Generic.ToStr(txtParentPlantillaCode.Text))
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnk_Click(sender As Object, e As EventArgs)
        'Info1.xIsApplicant = True
        Dim lnk As New LinkButton
        lnk = sender

        Info1.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
        Info1.xIsApplicant = 0
        Info1.Show()
    End Sub

End Class
