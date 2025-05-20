Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_CliClinicList

    'Protected Sub addTrigger_PreRender(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim btnPreview As ImageButton = TryCast(sender, ImageButton)
    '    Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
    '    NewScriptManager.RegisterPostBackControl(btnPreview)
    'End Sub
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

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        'Dim _dt As DataTable
        '_dt = SQLHelper.ExecuteDataTable("EEmployee_Web", UserNo, PayLocNo, cboTabNo.SelectedValue)
        'Me.grdMain.DataSource = _dt
        'Me.grdMain.DataBind()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployee_WebFiltered", UserNo.ToString(), PayLocNo.ToString(), Generic.ToInt(cboTabNo.SelectedValue).ToString(), Generic.ToInt(cbofilterby.SelectedValue), Generic.ToInt(cbofiltervalue.SelectedValue), Filter1.SearchText)
        grdMain.DataSource = dt
        grdMain.DataBind()

        PopulateGridDetl()
    End Sub


    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim URL As String
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo"})))
        If URL <> "" Then
            Session("IsDependent") = 0
            Response.Redirect(URL)
        End If
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

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo", "EmployeeCode"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        PopulateGridDetl()
    End Sub

#Region "********Detail********"

    Private Sub PopulateGridDetl()

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeDepe_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub



    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As String = container.Grid.GetRowValues(container.VisibleIndex, New String() {"employeedepeno"})
        Session("IsDependent") = 1
        Response.Redirect("cliclinicpastlist.aspx?id=" & Generic.ToStr(obj))
    End Sub



#End Region

End Class


