Imports clsLib
Imports System.Data

Partial Class Secured_EmpPlantilla_Chart
    Inherits System.Web.UI.Page    
    Dim w As Integer
    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim dtDistinct As DataTable
    Dim dt As DataTable

    Protected Sub ExportToImage(sender As Object, e As EventArgs)
        Dim base64 As String = Request.Form(hfImageData.UniqueID).Split(",")(1)
        Dim bytes As Byte() = Convert.FromBase64String(base64)
        Response.Clear()
        Response.ContentType = "image/png"
        Response.AddHeader("Content-Disposition", "attachment; filename=HTML.png")
        Response.Buffer = True
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.BinaryWrite(bytes)
        Response.End()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        w = Generic.ToInt(cboView.SelectedValue) 'Generic.ToInt(Request.QueryString("w"))
        AccessRights.CheckUser(UserNo, "EmpPlantillaList.aspx", "EPlantilla")

        dt = SQLHelper.ExecuteDataSet("EPlantilla_Chart", UserNo, Generic.ToInt(Request.QueryString("by")), Generic.ToInt(Request.QueryString("value")), w).Tables(0)

        If Not IsPostBack Then

            For index As Integer = 2 To 50
                Dim li As New ListItem
                li.Value = index
                li.Text = index
                cboLevelNo.Items.Add(li)
            Next
            cboLevelNo.SelectedValue = "3"

            '-----Load start with
            Dim x As Integer = 0
            cboStartWith.Items.Clear()
            cboStartWith.Items.Add(New ListItem("-- Select --", ""))
            dtDistinct = dt.DefaultView.ToTable(True, "ManagerID")
            For Each row As DataRow In dtDistinct.Rows
                If x > 0 Then
                    Dim li As New ListItem
                    li.Value = Generic.ToStr(row("ManagerID"))
                    li.Text = Generic.ToStr(row("ManagerID"))
                    cboStartWith.Items.Add(li)
                End If
                x = x + 1
            Next
        End If

        PopulateChart()
    End Sub

    Private Sub PopulateChart(Optional FilterByNo As Integer = 0, Optional FilterValueNo As Integer = 0, Optional WithIncumbent As Integer = 0)
        Try
            If w = 1 Then
                DataBoundOrganisationChart1.AssistantItem.Size = OrgChart.Core.BackgroundImageSize.Large
                DataBoundOrganisationChart1.ChartItem.Size = OrgChart.Core.BackgroundImageSize.Large
            Else
                DataBoundOrganisationChart1.AssistantItem.Size = OrgChart.Core.BackgroundImageSize.Medium
                DataBoundOrganisationChart1.ChartItem.Size = OrgChart.Core.BackgroundImageSize.Medium
            End If

            If Generic.ToStr(cboStartWith.SelectedValue) = "" Then
                DataBoundOrganisationChart1.StartValue = dt.Rows(0)(0)
            Else
                DataBoundOrganisationChart1.StartValue = cboStartWith.SelectedValue
            End If
            DataBoundOrganisationChart1.StackItem.ShowStackItems = chkStack.Checked
            DataBoundOrganisationChart1.StackItem.StackDepth = Generic.ToInt(cboLevelNo.SelectedValue)
            DataBoundOrganisationChart1.StackItem.Size = OrgChart.Core.BackgroundImageSize.Narrow
            DataBoundOrganisationChart1.MaximumDepth = Generic.ToInt(cboLevelNo.SelectedValue)

            DataBoundOrganisationChart1.DataSource = dt
            DataBoundOrganisationChart1.DataBind()
        Catch ex As Exception

        End Try
    End Sub

End Class
