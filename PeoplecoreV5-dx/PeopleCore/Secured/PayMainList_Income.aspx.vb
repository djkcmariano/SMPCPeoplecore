Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PayMainList_OI
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        PayHeader.ID = Generic.ToInt(TransNo)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayMain_Web_Income", UserNo, TransNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateDropDownList()

        Try

            Dim _dt As DataSet
            _dt = SQLHelper.ExecuteDataSet("Select * FROM ESourceIncomeType")

            cboTabNo.DataSource = _dt
            cboTabNo.DataTextField = "SourceIncomeTypeDesc"
            cboTabNo.DataValueField = "SourceIncomeTypeNo"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        'AccessRights.CheckUser(UserNo)
        Permission.IsAuthenticatedCoreUser()

        If Not IsPostBack Then
            PopulateDropDownList()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

#Region "********Main*******"



    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub




#End Region

End Class



