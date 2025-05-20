Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO

Imports System.Data.SqlClient
Partial Class Secured_EmpBUDS
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()

        End If

        'AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")

        'grdMain.DataSourceID = SqlDataSource1.ID
        'Generic.PopulateSQLDatasource("EEmployee_WebFiltered", SqlDataSource1, UserNo.ToString(), PayLocNo.ToString(), Generic.ToInt(cboTabNo.SelectedValue).ToString(), Generic.ToInt(cbofilterby.SelectedValue), Generic.ToInt(cbofiltervalue.SelectedValue), Filter1.SearchText)


        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployee_WebFiltered_BUDS", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue), Generic.ToInt(cbofilterby.SelectedValue), Generic.ToInt(cbofiltervalue.SelectedValue), Filter1.SearchText)
        grdMain.DataSource = dt
        grdMain.DataBind()

        'Dim _dt As DataTable
        '_dt = SQLHelper.ExecuteDataTable("EEmployee_Web", UserNo, PayLocNo, cboTabNo.SelectedValue)

        'EntityServerModeDataSource1.ContextTypeName = "EmployeeDataContext"
        'EntityServerModeDataSource1.TableName = _dt.TableName
        'grdMain.DataSourceID = EntityServerModeDataSource1.ID

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("EmpBUDS_Edit.aspx?Id=0")
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        'Dim lnk As New LinkButton
        'lnk = sender
        'Dim URL As String
        'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        'URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo"})))
        'If URL <> "" Then
        '    Response.Redirect(URL)
        'End If
    End Sub
    Protected Sub lnkUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Response.Redirect("~/secured/EmpUploadAccount.aspx?tModify=false&IsClickMain=1")
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 6)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub


End Class

