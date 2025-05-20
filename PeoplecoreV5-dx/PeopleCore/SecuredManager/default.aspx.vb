Imports clsLib
Imports System.Data
Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.XtraCharts

Partial Class SecuredManger_default
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim UserNo As Integer = 0
        Dim PayLocNo As Integer = 0

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            'PopulateData()
        End If


        'Dashboard
        Dim dt As DataTable = SQLHelper.ExecuteDataTable("EDashboard_WebDisplay", UserNo, PayLocNo, 6)
        Dim dtGroup As DataTable = dt.DefaultView.ToTable(True, "CMSCategoryNo", "GroupBy")
        Dim dtItem As DataTable = dt.DefaultView.ToTable(True, "DashboardNo", "Width", "DashboardTitle", "DashboardContent", "GroupBy", "IsGraph", "IsTable", "GraphType", "DataSource", "PivotSetting", "GraphWidth")
        Dim Content As String = ""
        For Each rowGroup As DataRow In dtGroup.Rows
            pDashboard.Controls.Add(New LiteralControl("<div class='page-content-wrap'><div class='row'>"))
            For Each rowItem As DataRow In dtItem.Select("GroupBy='" & rowGroup("GroupBy") & "'")
                If Generic.ToBol(rowItem("IsGraph")) = False And Generic.ToBol(rowItem("IsTable")) = False Then
                    Content = "<div class='col-md-" & Generic.ToInt(rowItem("Width")) & "'>"
                    Content = Content & "<div class='panel panel-default'>"

                    If Generic.ToStr(rowItem("DashboardTitle")) > "" Then
                        Content = Content & "<div class='panel-heading'>"
                        Content = Content & "<h4  class='panel-title'>" & Generic.ToStr(rowItem("DashboardTitle")) & " &nbsp;</h4>"
                        Content = Content & "</div>"
                    End If

                    Content = Content & "<div class='panel-body'>"
                    Content = Content & "<div class='table-responsive'>"
                    Content = Content & Generic.ToStr(rowItem("DashboardContent"))
                    Content = Content & "</div>"
                    Content = Content & "</div>"
                    Content = Content & "</div>"
                    Content = Content & "</div>"
                    pDashboard.Controls.Add(New LiteralControl(Content))
                ElseIf Generic.ToBol(rowItem("IsGraph")) Then
                    Dim pvt As New ASPxPivotGrid
                    Dim chart As New DevExpress.XtraCharts.Web.WebChartControl
                    pDashboard.Controls.Add(New LiteralControl("<div class='col-md-" & Generic.ToInt(rowItem("Width")) & "'>"))
                    pDashboard.Controls.Add(New LiteralControl("<div class='panel panel-default'>"))

                    If Generic.ToStr(rowItem("DashboardTitle")) > "" Then
                        pDashboard.Controls.Add(New LiteralControl("<div class='panel-heading'>"))
                        pDashboard.Controls.Add(New LiteralControl("<h4  class='panel-title'>" & Generic.ToStr(rowItem("DashboardTitle")) & " &nbsp;</h4>"))
                        pDashboard.Controls.Add(New LiteralControl("</div>"))
                    End If

                    pDashboard.Controls.Add(New LiteralControl("<div class='panel-body'>"))
                    pDashboard.Controls.Add(New LiteralControl("<div class='table-responsive'>"))
                    pvt.Visible = False
                    pDashboard.Controls.Add(pvt)
                    PopulatePivotGrid(pvt, Generic.ToStr(rowItem("Datasource")), Generic.ToStr(rowItem("PivotSetting")))
                    pvt.ID = "pvt" & Generic.ToInt(rowItem("DashboardNo"))
                    chart.DataSourceID = pvt.ID
                    chart.ID = "chart" & Generic.ToInt(rowItem("DashboardNo"))
                    chart.Width = Generic.ToInt(rowItem("GraphWidth"))
                    chart.SeriesTemplate.ChangeView(Generic.ToInt(rowItem("GraphType")))
                    pDashboard.Controls.Add(chart)
                    pDashboard.Controls.Add(New LiteralControl("</div>"))
                    pDashboard.Controls.Add(New LiteralControl("</div>"))
                    pDashboard.Controls.Add(New LiteralControl("</div>"))
                    pDashboard.Controls.Add(New LiteralControl("</div>"))
                ElseIf Generic.ToBol(rowItem("IsTable")) Then

                End If
            Next
            pDashboard.Controls.Add(New LiteralControl("</div></div>"))
        Next

    End Sub
    Private Sub PopulatePivotGrid(PG As ASPxPivotGrid, DataSource As String, PivotSettings As String)
        Dim strQuery As String
        Dim dt As DataTable
        strQuery = SQLHelper.ExecuteScalar("EFilteredValuePivot_WebGenerate", UserNo, DataSource, PayLocNo)
        dt = SQLHelper.ExecuteDataTable(strQuery)
        PG.DataSource = dt
        PG.RetrieveFields()
        'If Not IsPostBack Then
        '    PG.RetrieveFields(DevExpress.XtraPivotGrid.PivotArea.FilterArea, False)
        'End If
        PG.DataBind()
        PG.LoadLayoutFromString(PivotSettings)




    End Sub

End Class
