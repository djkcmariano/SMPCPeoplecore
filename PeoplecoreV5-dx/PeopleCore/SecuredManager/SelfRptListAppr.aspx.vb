Imports clsLib
Imports System.Data

Partial Class Secured_RptList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            PopulateGrid()
        End If

        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

    End Sub
    Protected Sub PopulateGrid()
        Try

            rRef.DataSource = SQLHelper.ExecuteDataTable("EMenu_Web_Report_Manager", UserNo, Left(Session("xMenuType"), 2), PayLocNo, Filter1.SearchText)
            rRef.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnk_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton()
        Dim URL As String
        lnk = DirectCast(sender, LinkButton)
        URL = Generic.Split(lnk.CommandArgument, 0)
        Dim lblreporttitle As String = Generic.Split(lnk.CommandArgument, 1)
        Dim lblReportname As String = Generic.Split(lnk.CommandArgument, 2)
        Dim i As Integer = Generic.Split(lnk.CommandArgument, 3)
        Dim lblDatasource As String = Generic.Split(lnk.CommandArgument, 4)

        Response.Redirect(URL & "?reporttitle=" & lblreporttitle & "&reportname=" & lblReportname & "&reportno=" & i & "&Datasource=" & lblDatasource)
        
    End Sub

End Class

















