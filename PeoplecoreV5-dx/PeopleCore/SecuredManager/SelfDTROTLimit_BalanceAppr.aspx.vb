Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Partial Class SecuredManager_SelfDTROTLimit_BalanceAppr
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim PaidHrs As Double = 0
    Dim CancelHrs As Double = 0
    Dim CancelCount As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EDTROTLimit_WebBalanceManager", UserNo, Generic.ToInt(cboTabNo.SelectedValue), Generic.ToInt(cboApplicableMonth.SelectedValue), Generic.ToInt(txtApplicableYear.Text), Filter1.SearchText.ToString, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        PopulateGridDetl1(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("ApplicableMonth")), Generic.ToInt(ViewState("ApplicableYear")))

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub
    Private Sub populateCombo()

        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 6)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()
        Catch ex As Exception

        End Try
        Try
            cboApplicableMonth.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EMonth", PayLocNo, "", "")
            cboApplicableMonth.DataTextField = "tDesc"
            cboApplicableMonth.DataValueField = "tno"
            cboApplicableMonth.DataBind()
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            Generic.PopulateDropDownList_Self(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList_Self(UserNo, Me, "pnlPopupDetl", PayLocNo)
            populateCombo()
            'txtDate.Text = Now.Date

        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid(True)
    End Sub


    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub


    Protected Sub lnkExportClaims_Click(sender As Object, e As EventArgs)
        Try
            grdExportClaims.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo", "FullName", "ApplicableMonth", "MonthCode", "ApplicableYear"})
        ViewState("TransNo") = obj(0)
        ViewState("Name") = obj(1)
        ViewState("ApplicableMonth") = obj(2)
        ViewState("MonthCode") = obj(3)
        ViewState("ApplicableYear") = obj(4)

        lblDetl.Text = ViewState("Name")
        lblDate.Text = "Applicable Month: " & ViewState("MonthCode") & " " & ViewState("ApplicableYear")

        PopulateGridDetl1(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("ApplicableMonth")), Generic.ToInt(ViewState("ApplicableYear")))

    End Sub

    Private Sub PopulateGridDetl1(id As Integer, MonthNo As Integer, Year As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDTROTClaims_WebManager", UserNo, id, MonthNo, Year, PayLocNo)
        grdDetl1.DataSource = dt
        grdDetl1.DataBind()
    End Sub




End Class



