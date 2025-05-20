Imports clsLib
Imports System.Data

Partial Class Secured_OrgTableHierarchyChart
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


    Private Sub PopulateChart()
        Try
            Dim ds As New DataSet
            Dim dt As DataTable
            'ds = SQLHelper.ExecuteDataSet("ETableOrg_Chart2", UserNo, TransNo, w, IsMirror, )
            ds = SQLHelper.ExecuteDataSet("EHierarchy_Chart", UserNo, TransNo, 1)
            dt = ds.Tables(0)
            DataBoundOrganisationChart1.DataSource = dt

            DataBoundOrganisationChart1.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "OrgTableOrg.aspx", "ETableOrg")
        PopulateChart()

    End Sub




End Class
