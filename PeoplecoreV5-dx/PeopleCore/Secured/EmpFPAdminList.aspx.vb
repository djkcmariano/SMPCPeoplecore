Imports System.Data
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web

Partial Class Secured_EmpFPAdminList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            'Dim dt As DataTable
            'dt = SQLHelper.ExecuteDataTable("EEmployeeFPMachineMain_Web", UserNo, "", 0, 0, PayLocNo)
            'grdMain.DataSource = dt
            'grdMain.DataBind()
            grdMain.DataSourceID = SqlDataSource1.ID
            Generic.PopulateSQLDatasource("EEmployeeFPMachineMain_Web", SqlDataSource1, UserNo, "", 0, 0, PayLocNo)
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
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupRate", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)

            Try
                cbofpactionno.DataSource = SQLHelper.ExecuteDataTable("EFPAction_WebLookup", UserNo, PayLocNo, 0)
                cbofpactionno.DataTextField = "tdesc"
                cbofpactionno.DataValueField = "tNo"
                cbofpactionno.DataBind()
            Catch ex As Exception

            End Try

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
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo", "FullName"})
            ViewState("TransNo") = obj(0)
            lbl.Text = obj(1)
            PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)       
        SaveRecord
    End Sub
    Private Sub SaveRecord()
        Dim dt As DataTable, RetVal As Boolean, error_num As Integer, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("EEmployeeFPMachineDeti_WebSave", UserNo, Generic.ToInt(txtCode.Text), Generic.ToInt(ViewState("TransNo")), _
                                             Generic.ToInt(cboFPMachineNo.SelectedValue), Generic.ToInt(cbofpactionno.SelectedValue), Generic.ToInt(cboPrivilegeNo.SelectedValue), _
                                             txtEffectiveDate.Text)
        For Each row As DataRow In dt.Rows
            RetVal = Generic.ToBol(row("RetVal"))
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
            PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupRate")
            If Generic.ToInt(ViewState("TransNo")) > 0 Then
                txtFullName.Text = lbl.Text
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
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"EmployeeFPMachineDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeFPMachineDeti", UserNo, item)
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
        dt = SQLHelper.ExecuteDataTable("EEmployeeFPMachineDeti_Web", UserNo, id)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub


    

    Protected Sub grdDetl_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdDetl.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdDetl.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub cbofpactionno_TextChanged(sender As Object, e As EventArgs)
        If cbofpactionno.SelectedValue = 1 Then
            cboPrivilegeNo.Enabled = True
            cboPrivilegeNo.CssClass = "form-control required"
        Else
            cboPrivilegeNo.Enabled = False
            cboPrivilegeNo.Text = ""
            cboPrivilegeNo.CssClass = "form-control"
        End If
        mdlRate.Show()

    End Sub


#Region "Upload"

    Protected Sub lnkDownload_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo", "FullName"})
            ViewState("TransNo") = obj(0)
            lbl.Text = obj(1)
            txtFullName2.Text = obj(1)
            PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
       
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SQLHelper.ExecuteNonQuery("EFPTemplateRequest_WebSave", UserNo, Generic.ToInt(ViewState("TransNo")), Generic.ToInt(cboFPMachineNo2.SelectedValue)) > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
      
    End Sub

#End Region

End Class

