Imports System.Data
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_RptList_EU
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EReportEU_Web", UserNo, Session("xMenuType"), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

#Region "********Main*******"
    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ReportEUNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EReportEU_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    'Generic.PopulateData(Me, "pnlPopupMain", dt)
                    Session("DesignerTask") = New clsReportDesignerTask.DesignerTask With {.mode = clsReportDesignerTask.ReportEdditingMode.ModifyReport, .reportID = i, .reportTitle = Generic.ToStr(row("ReportTitle"))}
                    Session("ReportModel") = New clsReportModel With {.ReportId = i, .ReportTitle = Generic.ToStr(row("ReportTitle")), .LayoutData = row("LayoutData")}
                Next
                ' mdlMain.Show()
                Response.Redirect("RptList_EU_Designer.aspx")

            Else
                MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        TransNo = Generic.ToInt(ViewState("TransNo"))

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ReportEUNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EReportEU", UserNo, item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)

        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkShow_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtReportTitle.Text <> "" Then
            Session("DesignerTask") = New clsReportDesignerTask.DesignerTask With {.mode = clsReportDesignerTask.ReportEdditingMode.NewReport, .reportID = 0, .reportTitle = txtReportTitle.Text}
            Response.Redirect("RptList_EU_Designer.aspx")

            'Dim sb As New StringBuilder
            'sb.Append("<script>")
            'sb.Append("window.open('~/secured/RptList_EU_Designer.aspx','_top','menubar=yes,scrollbars=yes,resizable=yes,with=100%,height=100%');")
            'sb.Append("</scri")
            'sb.Append("pt>")
            'ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())

        End If

    End Sub

   

#End Region
End Class
