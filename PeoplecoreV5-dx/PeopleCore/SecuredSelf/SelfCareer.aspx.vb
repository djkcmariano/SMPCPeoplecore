Imports System.Data
Imports clsLib

Partial Class SecuredSelf_SelfCareer
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    Private Sub PopulateGrid()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMR_WebVacantPosition_Advance", 0, "", 0)
        grd.DataSource = dt
        grd.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticated()
        If Not IsPostBack Then
            'PopulateDropDown()
        End If

        PopulateGrid()        
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()        

    End Sub


    Protected Sub lnkApply_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton, i As Integer = 0
        lnk = sender
        i = Generic.ToInt(lnk.CommandArgument)        
        Info1.JDNo = Generic.ToInt(i)
        Info1.Show()
    End Sub

End Class
