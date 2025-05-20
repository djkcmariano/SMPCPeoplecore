Imports clsLib
Imports System.Data

Partial Class Secured_OrgTableOrgDeti2
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim w As Integer
    Dim depth As Integer
    Dim stack As Boolean
    Dim start As String
    Dim IsMirror As Integer
    Dim IsBridge As Integer
    Dim IsCluster As Integer
    Dim IsBudgetSource As Integer
    Dim IsReassignment As Integer
    Dim color As Integer = 11

    Private Sub PopulateChart()
        Try
            Dim ds As New DataSet
            Dim dt As DataTable
            'ds = SQLHelper.ExecuteDataSet("ETableOrg_Chart2", UserNo, TransNo, w, IsMirror, )
            ds = SQLHelper.ExecuteDataSet("ETableOrg_Chart2", UserNo, TransNo, w, IsMirror, IsBridge, IsCluster, IsBudgetSource, IsReassignment)
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

            DataBoundOrganisationChart1.AssistantItem.Colour = color
            DataBoundOrganisationChart1.StackItem.ShowStackItems = stack
            DataBoundOrganisationChart1.StackItem.StackDepth = depth
            DataBoundOrganisationChart1.MaximumDepth = depth
            DataBoundOrganisationChart1.StartValue = start
            DataBoundOrganisationChart1.DataSource = dt
            DataBoundOrganisationChart1.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        w = Generic.ToInt(Request.QueryString("with"))
        depth = Generic.ToInt(Request.QueryString("depth"))
        stack = Generic.ToBol(Request.QueryString("stack"))
        start = Generic.ToStr(Request.QueryString("start"))
        IsMirror = Generic.ToInt(Request.QueryString("is"))
        IsBridge = Generic.ToInt(Request.QueryString("ib"))
        IsCluster = Generic.ToInt(Request.QueryString("ic"))
        IsBudgetSource = Generic.ToInt(Request.QueryString("ibs"))
        IsReassignment = Generic.ToInt(Request.QueryString("ir"))
        color = Generic.ToInt(Request.QueryString("color"))
        AccessRights.CheckUser(UserNo, "OrgTableOrg.aspx", "ETableOrg")

        PopulateChart()

    End Sub


End Class
