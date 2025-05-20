Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class SecuredSelf_SelfBenSALNList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

#Region "Main"

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ESALN_Self_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticated()
        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 44)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Dim URL As String
        URL = Generic.GetFirstTab("0")
        If URL <> "" Then
            Session("SALNCode") = ""
            Session("IsEnabled") = True
            Response.Redirect(URL)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim URL As String
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"SALNNo"})))
        Session("SALNCode") = Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"Code"}))
        Session("IsEnabled") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
        If URL <> "" Then
            Response.Redirect(URL)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"SALNNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAuditCol("ESALNAITP", UserNo, "SALNNo", item)
            Generic.DeleteRecordAuditCol("ESALNAPFE", UserNo, "SALNNo", item)
            Generic.DeleteRecordAuditCol("ESALNASGI", UserNo, "SALNNo", item)
            Generic.DeleteRecordAuditCol("ESALNAssetA", UserNo, "SALNNo", item)
            Generic.DeleteRecordAuditCol("ESALNAssetB", UserNo, "SALNNo", item)
            Generic.DeleteRecordAuditCol("ESALNBIFC", UserNo, "SALNNo", item)
            Generic.DeleteRecordAuditCol("ESALNDepe", UserNo, "SALNNo", item)
            Generic.DeleteRecordAuditCol("ESALNIRGS", UserNo, "SALNNo", item)
            Generic.DeleteRecordAuditCol("ESALNLiabilities", UserNo, "SALNNo", item)
            Generic.DeleteRecordAudit("ESALN", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
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

    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"SALNNo"}))
        Dim param As String = ""
        Dim tProceed As Boolean = False
        Dim ReportNo As Integer = 0, dt As DataTable
        ReportNo = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ReportNo"}))

        dt = SQLHelper.ExecuteDataTable("EReport_WebViewerSelf", UserNo, ReportNo, "", id, PayLocNo)
        For Each row As DataRow In dt.Rows
            ReportNo = Generic.ToInt(row("ReportNo"))
            param = Generic.ToStr(row("param"))
            tProceed = Generic.ToStr(row("tProceed"))
        Next
        tProceed = True
        If tProceed = True Then
            sb.Append("<script>")
            sb.Append("window.open('rpttemplateviewer.aspx?reportno=" & ReportNo & "&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
            sb.Append("</script>")
            ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
        Else
            MessageBox.Warning("No access permission to view the report.", Me)
        End If

    End Sub

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

#End Region




End Class
