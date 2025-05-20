Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_BenRafflePrizeList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

#Region "********Main********"
    Protected Sub PopulateGrid(Optional IsMain As Boolean = False)
        Try

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ERafflePrize_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ERafflePrize_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupMain", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        AccessRights.CheckUser(UserNo, "BenRaffleList.aspx", "ERaffle")
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
        End If

        PopulateGrid()


        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub


    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "BenRaffleList.aspx", "ERaffle") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"RafflePrizeNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)
            lnkSave.Enabled = IsEnabled
            txtRankDesc.Enabled = False
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub


    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim RafflePrizeNo As Integer = Generic.ToInt(txtRafflePrizeNo.Text)
        Dim RankNo As Integer = Generic.ToInt(hifRankNo.Value)
        Dim Amount As Double = Generic.ToDec(txtAmount.Text)
        Dim Description As String = Generic.ToStr(txtDescription.Text)
        Dim PayIncomeTypeNo As Integer = Generic.ToInt(cboPayIncomeTypeNo.SelectedValue)

        If SQLHelper.ExecuteNonQuery("ERafflePrize_WebSave", UserNo, RafflePrizeNo, TransNo, RankNo, Amount, Description, PayIncomeTypeNo, PayLocNo) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub


    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

#End Region


End Class

