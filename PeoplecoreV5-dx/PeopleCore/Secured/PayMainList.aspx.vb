Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web

Partial Class Secured_PayMainList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Private Sub PopulateGrid()
        Try
            'Header
            PayHeader.ID = Generic.ToInt(TransNo)
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayMain_Web", UserNo, TransNo, PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()

            If ViewState("PayMainNo") = 0 Then
                Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"PayMainNo", "FullName", "EmployeeNo"})
                ViewState("PayMainNo") = obj(0)
                lblDetl.Text = obj(1)
                ViewState("EmployeeNo") = obj(2)
                'lblIncome.Text = obj(1)
                'lblDeduction.Text = obj(1)
            End If

            PopulateIncome()
            PopulateDeduction()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPay_WebOne", UserNo, TransNo)
        Generic.PopulateData(Me, "Panel1", dt)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        'AccessRights.CheckUser(UserNo, Generic.ToStr(Session("xFormName")), Generic.ToStr(Session("xTableName")))
        Permission.IsAuthenticatedCoreUser()

        If Not IsPostBack Then
            PopulateData()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub
    Protected Sub lnkInfo_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayMainNo", "FullName", "EmployeeNo"})
        ViewState("PayMainNo") = obj(0)
        lblDetl.Text = obj(1)
        ViewState("EmployeeNo") = obj(2)
        Response.Redirect("payinfoedit.aspx?id=" & ViewState("EmployeeNo"))
    End Sub
    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayMainNo", "FullName", "EmployeeNo"})
        ViewState("PayMainNo") = obj(0)
        lblDetl.Text = obj(1)
        ViewState("EmployeeNo") = obj(2)
        'lblIncome.Text = obj(1)
        'lblDeduction.Text = obj(1)
        PopulateIncome()
        PopulateDeduction()
    End Sub

    Private Sub PopulateIncome()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayMainIncome_Web", UserNo, Generic.ToInt(ViewState("PayMainNo")))
        grdIncome.DataSource = dt
        grdIncome.DataBind()        
    End Sub

    Private Sub PopulateDeduction()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayMainDeduct_Web", UserNo, Generic.ToInt(ViewState("PayMainNo")))
        grdDeduction.DataSource = dt
        grdDeduction.DataBind()
    End Sub

    Protected Sub lnkExportIncome_Click(sender As Object, e As EventArgs)
        Try
            grdExportIncome.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkExportDeduct_Click(sender As Object, e As EventArgs)
        Try
            grdExportDeduct.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
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
                                                  New ReportParameter(ReportParameter.Type.int, "1"), _
                                                  New ReportParameter(ReportParameter.Type.int, Generic.ToInt(ViewState("EmployeeNo"))), _
                                                  New ReportParameter(ReportParameter.Type.int, TransNo.ToString())
                                                  )

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=37&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkPayslip_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Protected Sub MyGridView_FillContextMenuItems(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuEventArgs)
        If e.MenuType = GridViewContextMenuType.Rows Then
            'e.Items.Add(e.CreateItem("Get Key", "GetKey"))
            e.Items.Clear()
        End If
    End Sub

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Protected Sub lnkRptTax_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "PayMainNo")

        Dim EmployeeNo As Integer = 0, tno As Integer = 0, dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayMain_WebOne", UserNo, id)
        For Each row As DataRow In dt.Rows
            EmployeeNo = Generic.ToInt(row("EmployeeNo"))
            tno = Generic.ToInt(row("PayNo"))
        Next

        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, PayLocNo.ToString), _
                                                  New ReportParameter(ReportParameter.Type.int, "1"), _
                                                  New ReportParameter(ReportParameter.Type.int, EmployeeNo.ToString()), _
                                                  New ReportParameter(ReportParameter.Type.int, tno.ToString()), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"))

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=402&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub


#End Region


End Class





