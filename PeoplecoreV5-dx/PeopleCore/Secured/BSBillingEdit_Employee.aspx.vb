Imports clsLib
Imports System.Data
Imports DevExpress.Web
Partial Class Secured_BSBillingEdit_Employee
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "BSBillingOnProcessList.aspx", "BBS")

        If Not IsPostBack Then
         
        End If

        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("BBSMain_Web", UserNo, TransNo, "", PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

   
   

End Class


