Imports System.Data
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_SecAuditList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("GenerateAuditTrail_Web", UserNo, ViewState("TableName"), ViewState("TransNo"))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTableName.DataSource = SQLHelper.ExecuteDataSet("ztable_WebLookup", UserNo)
            cboTableName.DataValueField = "tNo"
            cboTableName.DataTextField = "tDesc"
            cboTableName.DataBind()

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub fltxtfilter_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            cboTransNo.DataSource = SQLHelper.ExecuteDataSet("zTable_WebLookup_TransNo", UserNo, Generic.ToInt(cboTableName.SelectedValue))
            cboTransNo.DataValueField = "tNo"
            cboTransNo.DataTextField = "tDesc"
            cboTransNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkRefresh_Click(sender As Object, e As System.EventArgs)
        Try
            ViewState("TableName") = Generic.ToInt(cboTableName.SelectedValue)
            ViewState("TransNo") = Generic.ToInt(cboTransNo.SelectedValue)
            PopulateGrid()
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


