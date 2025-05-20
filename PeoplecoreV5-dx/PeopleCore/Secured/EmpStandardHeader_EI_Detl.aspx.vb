Imports clsLib
Imports System.Data
Imports DevExpress.Web
Partial Class Secured_EmpStandardHeader_EI_Detl
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateTabHeader()
        End If

        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEIDeti_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkTemplate_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton, i As Integer
        lnk = sender

        Dim TemplateID As Integer = 0
        Dim TransNo As Integer = 0
        Dim EmpNo As Integer = 0
        Dim IsEnabled As Boolean = False
        lnk = sender

        TemplateID = Generic.Split(lnk.CommandArgument, 0)
        TransNo = Generic.Split(lnk.CommandArgument, 1)
        EmpNo = Generic.Split(lnk.CommandArgument, 2)
        IsEnabled = Generic.Split(lnk.CommandArgument, 3)

        'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        'i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EvalTemplateNo"}))

        'Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantStandardHeaderNo", "EvalTemplateNo"})
        'ViewState("TransNo") = obj(0)
        'i = obj(1)

        'Response.Redirect("~/secured/EmpStandardTemplateForm.aspx?TemplateID=" & Generic.ToInt(i) & "&FormName=EmpStandardHeader_EI.aspx&TableName=EEmployeeEI")

        Response.Redirect("~/secured/EmpStandardTemplateForm.aspx?TemplateID=" & TemplateID & "&FormName=selfEmpStandardHeader_EI.aspx&TableName=EEmployeeEI&TransNo=" & TransNo & "&Emp=" & EmpNo & "&IsEnabled=" & IsEnabled)

    End Sub

    Protected Sub lnk_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
        Dim sb As New StringBuilder
        Dim lnk As New LinkButton, i As Integer
        lnk = sender

        Dim TemplateID As Integer = 0
        Dim TransNo As Integer = 0
        Dim EmpNo As Integer = 0
        Dim IsEnabled As Boolean = False        

        TemplateID = Generic.Split(lnk.CommandArgument, 0)
        TransNo = Generic.Split(lnk.CommandArgument, 1)
        EmpNo = Generic.Split(lnk.CommandArgument, 2)
        IsEnabled = Generic.Split(lnk.CommandArgument, 3)

        sb.Append("<script>")
        sb.Append("window.open('EmpStandardTemplateFormPrint.aspx?TemplateID=" & TemplateID & "&FormName=selfEmpStandardHeader_EI.aspx&TableName=EEmployeeEI&TransNo=" & TransNo & "&Emp=" & EmpNo & "&IsEnabled=" & IsEnabled & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub


    Private Sub PopulateTabHeader()
        Try
           lbl.Text = "Transaction No. : " & Pad.PadZero(8, TransNo)
        Catch ex As Exception

        End Try
    End Sub


End Class

