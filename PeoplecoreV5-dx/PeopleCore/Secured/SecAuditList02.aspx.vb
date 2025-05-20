Imports System.Data
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web

Partial Class Secured_SecAuditList02
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("ETableAudit_Web", UserNo, ViewState("TableName"), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        PopulateGridDetl()
    End Sub

    Private Sub PopulateGridDetl()

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("ETableAuditDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        Me.grdDetl.DataSource = _dt
        Me.grdDetl.DataBind()
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)
    End Sub


    Private Sub PopulateDropDown()
        Try
            cboTableName.DataSource = SQLHelper.ExecuteDataSet("ETableRef_WebLookup", UserNo, PayLocNo)
            cboTableName.DataValueField = "tablename"
            cboTableName.DataTextField = "tableRefDesc"
            cboTableName.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkRefresh_Click(sender As Object, e As System.EventArgs)
        Try
            ViewState("TableName") = Generic.ToStr(cboTableName.SelectedValue)
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkDetails_Click(sender As Object, e As System.EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object = container.Grid.GetRowValues(container.VisibleIndex, New String() {"TableAuditNo"})
            ViewState("TransNo") = Generic.ToInt(obj)
            PopulateGridDetl()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()
        End If
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub


End Class



