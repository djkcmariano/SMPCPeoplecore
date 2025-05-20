Imports clsLib
Imports System.Data
Partial Class SecuredSelf_SelfPayContList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim EmployeeNo As Integer = 0

    Private Sub PopulateGrid()
        Try
            Dim dt As DataTable, stat As Integer = 0
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EPayContDeti_WebSelf", UserNo, "")
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        EmployeeNo = Generic.ToInt(Session("EmployeeNo"))        
        Permission.IsAuthenticated()
        
        PopulateGrid()
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)

    End Sub

End Class
