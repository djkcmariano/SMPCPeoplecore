Imports System.Data
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web

Partial Class Secured_BSProjectList_Class
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0


    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("BBSProjectClass_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("BBSProjectClass_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub PopulateData_Detl(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("BBSProjectClassRate_WebOne", UserNo, id)
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupRate", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
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
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSProjectClassNo", "ProjectNo"})
            ViewState("TransNo") = obj(0)
            lbl.Text = "Rate Sheet" 'obj(1)
            PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Protected Sub lnkDetailsR_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSProjectClassRateNo", "ProjectNo"})
            ViewState("TransNo") = obj(0)
            lbl.Text = "Rate Sheet" 'obj(1)
            Response.Redirect("BSProjectList_ClassRateDetl.aspx?BSProjectClassRateNo=" & ViewState("TransNo") & "&Id=" & TransNo.ToString)
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        SaveRecord()
    End Sub
    Private Sub SaveRecord()
        Dim dt As DataTable, RetVal As Boolean, error_num As Integer, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("BBSProjectClass_WebSave", UserNo, Generic.ToInt(txtCode.Text), Generic.ToInt(TransNo), _
                                             Generic.ToInt(cboPayClassNo.SelectedValue), Generic.ToDec(txtDiscount.Text), Generic.ToInt(cboBSProjectPayTypeNo.SelectedValue))
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
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSProjectClassNo"})))
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
            PopulateData_Detl(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSProjectClassRateNo"})))
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
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BSProjectClassNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("BBSProjectClassRate", UserNo, "BSProjectClassNo", item)
                Generic.DeleteRecordAuditCol("BBSProjectClassRateDeti", UserNo, "BSProjectClassNo", item)
                Generic.DeleteRecordAudit("BBSProjectClass", UserNo, item)
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
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"BSProjectClassRateNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("BBSProjectClassRateDeti", UserNo, "BSProjectClassRateNo", item)
                Generic.DeleteRecordAudit("BBSProjectClassRate", UserNo, item)
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
        dt = SQLHelper.ExecuteDataTable("BBSProjectClassRate_Web", UserNo, id)
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
        Dim positionNo As Integer = Generic.ToInt(cboPositionNo.SelectedValue)
        Dim departmentNo As Integer = Generic.ToInt(cboDepartmentNo.SelectedValue)
        Dim billRate As Double = Generic.ToDec(txtBillingRate.Text)
        Dim currentSalary As Double = Generic.ToDec(txtCurrentSalary.Text)
        Dim otRate As Double = Generic.ToDec(txtOTRate.Text)
        Dim billingRateD As Double = Generic.ToDec(txtBillingRateD.Text)
        Dim currentSalaryD As Double = Generic.ToDec(txtCurrentSalaryD.Text)
        Dim otRateD As Double = Generic.ToDec(txtOTRateD.Text)
        Dim billingRateH As Double = Generic.ToDec(txtBillingRateH.Text)
        Dim currentSalaryH As Double = Generic.ToDec(txtCurrentSalaryH.Text)
        Dim otRateH As Double = Generic.ToDec(txtOTRateH.Text)

        If SQLHelper.ExecuteNonQuery("BBSProjectClassRate_WebSave", UserNo, Generic.ToInt(txtBSProjectClassRateNo.Text), Generic.ToInt(ViewState("TransNo")), TransNo, positionNo, departmentNo, billRate, currentSalary, otRate, billingRateD, currentSalaryD, otRateD, billingRateH, currentSalaryH, otRateH) > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
        PopulateGridDetl(ViewState("TransNo"))
    End Sub

#End Region

End Class

