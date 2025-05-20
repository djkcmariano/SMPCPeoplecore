Imports System.Data
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web

Partial Class Secured_PYActivityType
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub
    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPosition_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPYActivityType_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub PopulateData_Detl(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPYActivityTypeDeti_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupRate", dt)
            Next

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub
    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 14)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupRate", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PositionNo", "PositionCode"})
            ViewState("TransNo") = obj(0)
            lbl.Text = "Details" 'obj(1)
            PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        SaveRecord()
    End Sub
    Private Sub SaveRecord()
        Dim dt As DataTable, RetVal As Boolean, error_num As Integer, error_message As String = ""
        
        dt = SQLHelper.ExecuteDataTable("EPYActivityType_WebSave", UserNo, Generic.ToInt(txtPYActivityTypeNo.Text), Generic.ToStr(txtPYActivityTypeCode.Text), Generic.ToStr(txtPYActivityTypeDesc.Text), Generic.ToStr(txtRemarks.Text))
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
    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PYActivityTypeNo"})))
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Protected Sub lnkEditD_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData_Detl(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PYActivityTypeDetiNo"})))
            mdlRate.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    Protected Sub lnkAddD_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupRate")
            If Generic.ToInt(ViewState("TransNo")) > 0 Then
                txtCode.Text = lbl.Text
                mdlRate.Show()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If
    End Sub
    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PYActivityTypeNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPYActivityType", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkDeleteD_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PYActivityTypeDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPYActivityTypeDeti", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Sub PopulateGridDetl(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPYActivityTypeDeti_Web", UserNo, id)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub
    Protected Sub grdDetl_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdDetl.CommandButtonInitialize
        'If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
        '    Dim value As Boolean = Generic.ToInt(grdDetl.GetRowValues(e.VisibleIndex, "IsEnabled"))
        '    e.Enabled = value
        'End If
    End Sub


#Region "Upload"

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim PYModePaymentNo As Integer = Generic.ToInt(cboPYModePaymentNo.SelectedValue)

        If SQLHelper.ExecuteNonQuery("EPYActivityTypeDeti_WebSave", UserNo, Generic.ToInt(txtCode.Text), Generic.ToInt(ViewState("TransNo")), Generic.ToStr(txtPYActivityTypeDetiCode.Text), Generic.ToStr(txtPYActivityTypeDetiDesc.Text), PYModePaymentNo) > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
        PopulateGridDetl(ViewState("TransNo"))
    End Sub

#End Region

End Class


