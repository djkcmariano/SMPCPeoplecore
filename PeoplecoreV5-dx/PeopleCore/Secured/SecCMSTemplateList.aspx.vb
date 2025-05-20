Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports DevExpress.XtraCharts

Partial Class Secured_SecCMSTemplateList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("ECMSTemplate_Web", UserNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If ViewState("TransNo") = 0 Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"CMSCategoryNo", "CMSCategoryDesc"})
            ViewState("TransNo") = obj(0)
            lblDetl.Text = obj(1)
        End If

        PopulateGridDetl()

    End Sub

    Protected Sub PopulateDropDown()
        Dim dt As DataTable
        Dim ChartTypes() As String = System.Enum.GetNames(GetType(ViewType))
        Dim i As Integer = 0        
        For Each type As String In ChartTypes
            Dim item As New ListItem
            item.Value = i
            item.Text = type
            cboGraphTypeNo.Items.Add(item)
            i = i + 1
        Next

        dt = SQLHelper.ExecuteDataTable("EDashboard_DS")
        For Each row In dt.Rows
            Dim item As New ListItem
            item.Text = Generic.ToStr(row("xReportTitle"))
            item.Value = Generic.ToStr(row("Datasource"))
            cboDatasource.Items.Add(item)
        Next
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        PopulateGrid()
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        'Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

#Region "********Main*******"

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"CMSTemplateNo", "CMSCategoryDesc", "CMSCategoryNo"})
        Dim CMSTemplateNo As Integer = obj(0)
        Dim CMSCategoryDesc As String = obj(1)
        Dim CMSCategoryNo As String = obj(2)

        Response.Redirect("SecCMSTemplateEdit.aspx?CMSTemplateNo=" & CMSTemplateNo & "&CMSCategoryDesc=" & CMSCategoryDesc & "&CMSCategoryNo=" & CMSCategoryNo)

    End Sub

#End Region

#Region "Details"

    Protected Sub PopulateGridDetl()
        grdDetl.DataSource = SQLHelper.ExecuteDataTable("EDashboard_Web", UserNo, Generic.ToInt(ViewState("TransNo")), Session("xPayLocNo"))
        grdDetl.DataBind()
    End Sub

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"CMSCategoryNo", "CMSCategoryDesc"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        PopulateGridDetl()
    End Sub

    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            cboDatasource.SelectedValue = ""
            cboGraphTypeNo.SelectedValue = "0"
            txtGraphWidth.Text = ""
            PopulateGraph()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"DashboardNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues                                
                Generic.DeleteRecordAudit("EDashboard", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridDetl()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DashboardNo"}))

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDashboard_WebOne", UserNo, Generic.ToInt(i))
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupMain", dt)
                cboGraphTypeNo.Text = Generic.ToInt(row("GraphTypeNo")) 'IIf(Generic.ToInt(row("GraphTypeNo")) = 0, "", Generic.ToInt(row("GraphTypeNo")))
                txtGraphWidth.Text = Generic.ToStr(row("GraphWidth"))
                cboDatasource.Text = Generic.ToStr(row("Datasource"))
            Next
            PopulateGraph()
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)

        If SQLHelper.ExecuteNonQuery("EDashboard_WebSave", UserNo, Generic.ToInt(txtCode.Text), Generic.ToInt(ViewState("TransNo")), txtDashboardDesc.Text, txtDashboardTitle.Text, _
                                     txtGroupBy.Text, chkIsGraph.Checked, Generic.ToInt(cboGraphTypeNo.SelectedValue), Generic.ToInt(txtGraphWidth.Text), _
                                     cboDatasource.SelectedValue, Generic.ToInt(cboWidthNo.SelectedValue), chkIsVisible.Checked, Generic.ToInt(Session("xPayLocNo"))) > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub lnkContent_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DashboardNo"}))
        Response.Redirect("~/secured/SecCMSTemplateContent.aspx?id=" & id.ToString())        
    End Sub

    Protected Sub chkIsGraph_CheckedChanged(sender As Object, e As EventArgs)
        PopulateGraph()
    End Sub

    Private Sub PopulateGraph()
        divGraphType.Visible = chkIsGraph.Checked
        divDatasource.Visible = chkIsGraph.Checked
        divChartWidth.Visible = chkIsGraph.Checked
        mdlMain.Show()
    End Sub

    Protected Sub lnkContent_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

#End Region



End Class

















