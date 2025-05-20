Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class SecuredSelf_SelfEmpStandardHeader_EI
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0


#Region "Main"

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEI_WebSelf", UserNo, PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PopulateDetl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearance_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        ViewState("TransNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeEINo"}))
        lbl.Text = "Transaction No. : " & Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"Code"}))
        PopulateDetl()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticated()
        If Not IsPostBack Then

        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    
   
    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    
    Protected Sub lnkTemplate_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton
        Dim TemplateID As Integer = 0
        Dim TransNo As Integer = 0
        Dim EmpNo As Integer = 0
        Dim IsEnabled As Boolean = False
        lnk = sender

        TemplateID = Generic.Split(lnk.CommandArgument, 0)
        TransNo = Generic.Split(lnk.CommandArgument, 1)
        EmpNo = Generic.Split(lnk.CommandArgument, 2)
        IsEnabled = Generic.Split(lnk.CommandArgument, 3)

        Response.Redirect("~/securedself/SelfEmpStandardTemplateForm.aspx?TemplateID=" & TemplateID & "&FormName=selfEmpStandardHeader_EI.aspx&TableName=EEmployeeEI&TransNo=" & TransNo & "&Emp=" & EmpNo & "&IsEnabled=" & IsEnabled)

    End Sub

    Protected Sub lnkSubmit_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeEINo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            i = i + SQLHelper.ExecuteNonQuery("EEmployeeEI_WebStatUpdate", UserNo, item, 1)            
        Next
        If i > 0 Then
            Dim url As String = "selfempstandardheader_ei.aspx"
            MessageBox.SuccessResponse("(" + i.ToString + ") " + MessageTemplate.SuccessSubmit, Me, url)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
        
    End Sub



#End Region


End Class






