Imports clsLib
Imports System.Data

Partial Class Secured_EmpPlantillaChart
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TransactionCode As String
    Dim IsWith As Integer = 0
    Dim IsMirror As Integer = 0
    Dim LevelNo As Integer = 1
    Dim IsStack As Integer = 0
    Dim IsBridge As Integer = 0
    Dim IsCluster As Integer = 0
    Dim PlantillaTypeNo As Integer = 0
    Dim IsBudgetSource As Integer = 0
    Dim color As Integer = 0
    Dim IsReassignment As Integer = 0
    
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransactionCode = Generic.ToStr(Request.QueryString("id"))
        IsWith = Generic.ToInt(Request.QueryString("w"))
        IsMirror = Generic.ToInt(Request.QueryString("m"))
        LevelNo = Generic.ToInt(Request.QueryString("l"))
        IsStack = Generic.ToInt(Request.QueryString("s"))
        IsBridge = Generic.ToInt(Request.QueryString("b"))
        IsCluster = Generic.ToInt(Request.QueryString("c"))
        PlantillaTypeNo = Generic.ToInt(Request.QueryString("pl"))
        IsBudgetSource = Generic.ToInt(Request.QueryString("ibs"))
        color = Generic.ToInt(Request.QueryString("color"))
        IsReassignment = Generic.ToInt(Request.QueryString("ir"))
        AccessRights.CheckUser(UserNo, "EmpPlantillaList.aspx", "EPlantilla")

        PopulateChart(TransactionCode)
    End Sub


    Private Sub PopulateChart(PlantillaCode As String)
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPlantilla_Chart2", UserNo, TransactionCode, IsWith, IsMirror, IsBridge, IsCluster, IsBudgetSource, IsReassignment)
            DataBoundOrganisationChart1.StartValue = ds.Tables(0).Rows(0)(0)

            DataBoundOrganisationChart1.StackItem.ShowStackItems = IsStack

            LevelNo = IIf(LevelNo = 0, Generic.ToInt(ds.Tables(1).Rows(0)(1)), LevelNo)
            DataBoundOrganisationChart1.StackItem.StackDepth = Generic.ToInt(LevelNo)
            DataBoundOrganisationChart1.MaximumDepth = LevelNo

            If IsWith = 1 Then
                DataBoundOrganisationChart1.AssistantItem.Size = OrgChart.Core.BackgroundImageSize.Large
                DataBoundOrganisationChart1.ChartItem.Size = OrgChart.Core.BackgroundImageSize.Large
                DataBoundOrganisationChart1.StackItem.Size = OrgChart.Core.BackgroundImageSize.Medium
            Else
                DataBoundOrganisationChart1.AssistantItem.Size = OrgChart.Core.BackgroundImageSize.Medium
                DataBoundOrganisationChart1.ChartItem.Size = OrgChart.Core.BackgroundImageSize.Medium
                DataBoundOrganisationChart1.StackItem.Size = OrgChart.Core.BackgroundImageSize.Small
            End If

            DataBoundOrganisationChart1.AssistantItem.Colour = color
            DataBoundOrganisationChart1.DataSource = ds.Tables(0)
            DataBoundOrganisationChart1.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub DataBoundOrganisationChart1_ItemDataBound(sender As Object, e As OrgChart.Core.OrganisationChartItemEventArgs) Handles DataBoundOrganisationChart1.ItemDataBound
    '    If e.Item.ItemType = OrgChart.Core.OrganisationItemType.ChartItem Then
    '        Dim hif As New HiddenField
    '        hif = e.Item.FindControl("hifIsMirror")
    '        If Generic.ToBol(hif.Value) Then
    '            e.Item.BackColor = Drawing.Color.Wheat
    '        End If
    '    End If
    'End Sub
End Class
