Imports clsLib
Imports System.Data

Partial Class SecuredSelf_SelfPayMainList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim EmployeeNo As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer

    Private Sub PopulateDropDown()
        Try
            cboPayMainNo.DataSource = SQLHelper.ExecuteDataTable("EPayMain_WebLookupSelf", UserNo)
            cboPayMainNo.DataTextField = "PayMainDesc"
            cboPayMainNo.DataValueField = "PayMainNo"
            cboPayMainNo.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadInfo()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayInfo_WebOneSelf", UserNo)
        Generic.PopulateData(Me, "cphBody", dt)        
    End Sub

    Private Sub LoadPayroll()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayMain_WebOneSelf", UserNo, Generic.ToInt(cboPayMainNo.SelectedValue))
        If dt.Rows.Count > 0 Then            
            Generic.PopulateData(Me, "pPayroll", dt)
        Else
            Generic.ClearControls(Me, "pPayroll")
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        EmployeeNo = Generic.ToInt(Session("EmployeeNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Me.cboPayMainNo.SelectedValue)
        Permission.IsAuthenticated()

        If Not IsPostBack Then
            PopulateDropDown()
            LoadInfo()
        End If

        PopulateGridIncome()
        PopulateGridDeduction()

    End Sub

    Private Sub PopulateGridDeduction()
        Dim _ds As New DataSet
        _ds = SQLHelper.ExecuteDataSet("EPayMainDeduct_Web_CHH", UserNo, Generic.ToInt(Me.cboPayMainNo.SelectedValue))
        Me.grdDeduction.DataSource = _ds
        Me.grdDeduction.DataBind()
    End Sub

    Private Sub PopulateGridIncome()
        Dim _ds As New DataSet
        _ds = SQLHelper.ExecuteDataSet("EPayMainIncome_Web_CHH", UserNo, Generic.ToInt(Me.cboPayMainNo.SelectedValue))
        Me.grdIncome.DataSource = _ds
        Me.grdIncome.DataBind()
    End Sub

    Protected Sub cboPayMainNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        LoadPayroll()
        ViewState("PayMainNo") = Generic.ToInt(Me.cboPayMainNo.SelectedValue)
    End Sub


#Region "********Reports********"

    Protected Sub lnkPayslip_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        'Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo"}))
        'New ReportParameter(ReportParameter.Type.int, "0"), _
        Dim param As String = Generic.ReportParam(
                                                  New ReportParameter(ReportParameter.Type.int, UserNo.ToString), _
                                                  New ReportParameter(ReportParameter.Type.int, PayLocNo.ToString), _
                                                  New ReportParameter(ReportParameter.Type.int, "1"), _
                                                  New ReportParameter(ReportParameter.Type.int, EmployeeNo), _
                                                  New ReportParameter(ReportParameter.Type.int, TransNo.ToString())
                                                  )

        sb.Append("<script>")
        sb.Append("window.open('SelfRptTemplateViewer.aspx?reportno=697&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub
    Protected Sub lnkPayslip_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub
    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

#End Region
End Class
