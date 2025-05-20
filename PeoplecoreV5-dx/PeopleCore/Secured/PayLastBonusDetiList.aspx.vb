Imports clsLib
Imports System.Data
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting

Partial Class Secured_PayLastBonusDetiList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim PayNo As Integer = 0
    Dim PayLastDetiNo As Integer = 0
    Dim EmployeeNo As Integer = 0

    Private Sub PopulateGrid()
        'Populate Data
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayLastBonusDeti_Web", UserNo, PayLastDetiNo)
        grdDetl.DataSource = _dt
        grdDetl.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        PayLastDetiNo = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticatedCoreUser()
        PopulateTabHeader()
        HeaderInfo1.xFormName = "EPayLastDeti"

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
        End If
        PopulateGrid()
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            'dt = SQLHelper.ExecuteDataTable("EPayLastDeti_WebTabHeader", UserNo, PayLastDetiNo)
            dt = SQLHelper.ExecuteDataTable("EPay_WebTabHeader", UserNo, Session("PayLastList_PayNo"))
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
                PayNo = Generic.ToInt(row("PayNo"))
                EmployeeNo = Generic.ToInt(row("EmployeeNo"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub



#Region "********Reports********"

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

    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        'Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
        Dim id As Integer = grdDetl.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "PayBonusDetiNo")


        Dim EmpNo As Integer = 0, BonusBasisNo As Integer = 0, StartDate As String = "", EndDate As String = "", NoofMonthsAssume As Double = 0
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayLastBonusDeti_WebOne", UserNo, id)
        For Each row As DataRow In dt.Rows
            EmpNo = Generic.ToInt(row("EmployeeNo"))
            BonusBasisNo = Generic.ToInt(row("BonusBasisNo"))
            StartDate = Generic.ToStr(row("StartDate"))
            EndDate = Generic.ToStr(row("EndDate"))
            NoofMonthsAssume = Generic.ToDec(row("NoofMonthsAssume"))
        Next

        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, PayLocNo.ToString), _
                                                  New ReportParameter(ReportParameter.Type.int, "1"), _
                                                  New ReportParameter(ReportParameter.Type.int, EmpNo.ToString()), _
                                                  New ReportParameter(ReportParameter.Type.int, BonusBasisNo.ToString()), _
                                                  New ReportParameter(ReportParameter.Type.str, StartDate), _
                                                  New ReportParameter(ReportParameter.Type.str, EndDate), _
                                                  New ReportParameter(ReportParameter.Type.str, NoofMonthsAssume.ToString()), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"))
        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=412&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

#End Region

End Class
