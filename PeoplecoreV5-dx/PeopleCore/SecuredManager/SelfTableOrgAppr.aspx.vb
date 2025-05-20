Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class SecuredManager_SelfTableOrgAppr
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            cboTabNo.Text = Generic.ToStr(Session("xTabNo"))
            PopulateDropDown()
        End If

        PopulateGrid()

        If cboTabNo.SelectedValue = 0 Then
            lnkApprove.Visible = False
            lnkDisapprove.Visible = False
        ElseIf cboTabNo.SelectedValue = 2 Then
            lnkApprove.Visible = False
            lnkDisapprove.Visible = False
        ElseIf cboTabNo.SelectedValue = 1 Then
            lnkApprove.Visible = True
            lnkDisapprove.Visible = True
        ElseIf cboTabNo.SelectedValue = 3 Or cboTabNo.SelectedValue = 4 Then
            lnkApprove.Visible = False
            lnkDisapprove.Visible = False
        End If
    End Sub

#Region "Main"

    Protected Sub cboTabNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateGrid()

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("ETableOrg_WebManager", UserNo, Generic.ToInt(cboTabNo.SelectedValue))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        Session("xTabNo") = Generic.ToInt(cboTabNo.SelectedValue)

    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 26)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkChart_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        'PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TableOrgNo"})))
        Response.Redirect("~/SecuredManager/SelfTableOrgChartAppr.aspx?id=" & Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TableOrgNo"})) & "&ApprovalStatNo=" & lnk.CommandName & "&Title=" & lnk.ToolTip)
        'Dim sb As New StringBuilder
        'Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TableOrgNo"}))
        'sb.Append("<script>")
        'sb.Append("window.open('SelfTableOrgChartAppr2.aspx?id=" & id.ToString() & "&desc=" & lnk.ToolTip & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,resizable=yes,scrollbars=yes');")
        'sb.Append("</script>")
        'ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkChart_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Protected Sub lnkApprove_Click(sender As Object, e As EventArgs)


        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TableOrgNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            i = i + SQLHelper.ExecuteNonQuery("ETableOrg_ProcessManager", UserNo, item, "Post")
        Next
        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me)
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If
        

    End Sub


    Protected Sub lnkDisapprove_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TableOrgNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            i = i + SQLHelper.ExecuteNonQuery("ETableOrg_ProcessManager", UserNo, item, "Cancel")
        Next
        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDisapproved, Me)
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

#End Region

End Class
