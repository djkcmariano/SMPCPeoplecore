Imports clsLib
Imports System.Data

Partial Class Secured_ReferenceList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateGrid()
        End If

        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

    End Sub
    Protected Sub PopulateGrid()
        Try

            rRef.DataSource = SQLHelper.ExecuteDataTable("EMenu_Web_Reference", UserNo, Left(Session("xMenuType"), 2), 1, Filter1.SearchText)
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
        Session("xMenuType") = Generic.Split(lnk.CommandArgument, 1)
        Session("xTableName") = Generic.Split(lnk.CommandArgument, 2)
        Session("xFormName") = Generic.Split(lnk.CommandArgument, 0)
        Response.Redirect(URL)
    End Sub

End Class

















