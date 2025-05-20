Imports System.Data
Imports clsLib
Imports DevExpress.Web
Partial Class Secured_DTRHolidayEditDaytype
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHolidayDayType_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHolidayDayType_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
                populateDayType(cboPayClassNo.Text)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Tab.menuStyle = "TabRef"
        AccessRights.CheckUser(UserNo, "DTRHolidayList.aspx", "EHoliday")
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateTabHeader()
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "DTRHolidayList.aspx", "EHoliday") Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub



    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "DTRHolidayList.aspx", "EHoliday") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"HolidayDayTypeNo"})))

            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "DTRHolidayList.aspx", "EHoliday") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"HolidayDayTypeNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EHolidayDayType", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim ovt As Double = Generic.ToDbl(txtOvt.Text)
        Dim ovt8 As Double = Generic.ToDbl(txtOvt8.Text)
        Dim NPOvt As Double = Generic.ToDbl(txtNPOvt.Text)
        Dim NPOvt8 As Double = Generic.ToDbl(txtNPOvt8.Text)
        Dim payClassNo As Integer = Generic.ToInt(cboPayClassNo.SelectedValue)
        Dim ovtR As Double = Generic.ToDbl(txtOvtR.Text)
        Dim ovt8R As Double = Generic.ToDbl(txtOvt8R.Text)
        Dim NPOvtR As Double = Generic.ToDbl(txtNPOvtR.Text)
        Dim NPOvt8R As Double = Generic.ToDbl(txtNPOvt8R.Text)
        Dim daytypeno As Integer = Generic.ToInt(cboPDayTypeNo.SelectedValue)
        Dim dt As DataTable, error_num As Integer = 0, error_message As String = "", RetVal As Boolean

        dt = SQLHelper.ExecuteDataTable("EHolidayDayType_WebSave", UserNo, Generic.ToInt(txtCode.Text), TransNo, ovt, ovt8, 0, NPOvt, NPOvt8, payClassNo, ovtR, ovt8R, 0, NPOvtR, NPOvt8R, daytypeno)
        For Each row As DataRow In dt.Rows
            RetVal = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                RetVal = False
            End If

        Next
        If RetVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EHolidayTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
            'lnkAdd.Enabled = Generic.ToBol(row("IsEnabled"))
        Next
    End Sub
    Protected Sub cboPayClassNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        populateDayType(Generic.ToInt(cboPayClassNo.SelectedValue))
        ModalPopupExtender1.Show()
    End Sub
    Private Sub populateDayType(payclassno As Integer)
        Try
            cboPDayTypeNo.DataSource = SQLHelper.ExecuteDataSet("EDayType_WebLookup_Holiday", UserNo, payclassno)
            cboPDayTypeNo.DataTextField = "tDesc"
            cboPDayTypeNo.DataValueField = "tNo"
            cboPDayTypeNo.DataBind()
        Catch ex As Exception

        End Try
    End Sub

End Class

