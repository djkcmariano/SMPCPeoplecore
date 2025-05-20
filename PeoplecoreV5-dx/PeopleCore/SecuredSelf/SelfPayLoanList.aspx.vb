Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class SecuredSelf_SelfPayLoanList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim EmployeeNo As Integer = 0

    Private Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ELoan_WebSelf", UserNo, "", Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 13)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        EmployeeNo = Generic.ToInt(Session("EmployeeNo"))
        Permission.IsAuthenticated()
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        PopulateGrid()
        PopulateGridDetails()
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"LoanNo", "Code"})
        ViewState("TransNo") = obj(0)
        lbl.Text = "Transaction No. : " & obj(1)        
    End Sub


#Region "Payment Details"
    Private Sub PopulateGridDetails()
        Dim dt As DataTable        
        dt = SQLHelper.ExecuteDataTable("ELoanPay_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdDetl.DataSource = dt
        grdDetl.DataBind()        
    End Sub
#End Region

End Class
