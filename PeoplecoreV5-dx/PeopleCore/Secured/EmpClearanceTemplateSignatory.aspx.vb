Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_EmpClearanceTemplateSignatory
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim ClearanceTemplateDetiNo As Integer = 0

    Private Sub PopulateGrid()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClearanceTemplateDeti_Web", UserNo, TransNo)
        grdMain.DataSource = dt
        grdMain.DataBind()

        If Generic.ToInt(ViewState("ClearanceTemplateDetiNo")) = 0 Then
            If Generic.ToInt(dt.Rows.Count) > 0 Then
                ViewState("ClearanceTemplateDetiNo") = Generic.ToInt(dt.Rows(0)("ClearanceTemplateDetiNo"))
                lbl.Text = Generic.ToStr(dt.Rows(0)("Code"))              
                PopulateGridDeti()
            End If
        End If
    End Sub

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClearanceTemplateDeti_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel1", dt)
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "EmpClearanceTemplate.aspx", "EClearanceTemplate") Then
            If TransNo > 0 Then
                Generic.ClearControls(Me, "Panel1")
                ModalPopupExtender1.Show()
            Else
                MessageBox.Alert(MessageTemplate.NoSelectedTransaction, "warning", Me)
            End If
            
        Else
            MessageBox.Information(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "EmpClearanceTemplate.aspx", "EClearanceTemplate") Then
            Dim lnk As New LinkButton
            lnk = sender
            PopulateData(Generic.ToInt(lnk.CommandArgument))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Information(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "EmpClearanceTemplate.aspx", "EClearanceTemplate") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ClearanceTemplateDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EClearanceTemplateDeti", UserNo, item)
                Generic.DeleteRecordAuditCol("EClearanceTemplateSignatory", UserNo, "ClearanceTemplateDetiNo", item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                ViewState("ClearanceTemplateDetiNo") = 0
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
            
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "EmpClearanceTemplate.aspx", "EClearanceTemplate") Then
            Dim lnk As New LinkButton
            lnk = sender
            ViewState("ClearanceTemplateDetiNo") = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
            lbl.Text = Generic.Split(lnk.CommandArgument, 1)
            PopulateGridDeti()
        Else
            MessageBox.Information(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If SQLHelper.ExecuteNonQuery("EClearanceTemplateDeti_WebSave", UserNo, PayLocNo, Generic.ToInt(txtCode.Text), txtClearanceTemplateDetiDesc.Text, TransNo, _
                                     Generic.ToInt(cboDepartmentNo.SelectedValue), txtTitle.Text, Generic.ToInt(txtOrderBy.Text)) > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "EmpClearanceTemplate.aspx", "EClearanceTemplate")        
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateGrid()
        End If
    End Sub

    Protected Sub cboDepartmentNo_TextChanged(sender As Object, e As EventArgs)
        If Generic.ToInt(cboDepartmentNo.SelectedValue) > 0 Then
            txtTitle.Text = cboDepartmentNo.SelectedItem.Text
        Else
            txtTitle.Text = ""
        End If
        ModalPopupExtender1.Show()
    End Sub


#Region "Details"
    Private Sub PopulateGridDeti()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClearanceTemplateSignatory_Web", UserNo, Generic.ToInt(ViewState("ClearanceTemplateDetiNo")))
        grdDeti.DataSource = dt
        grdDeti.DataBind()
    End Sub


    Private Sub PopulateDataDeti(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClearanceTemplateSignatory_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
    End Sub

    Protected Sub lnkAddDeti_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "EmpClearanceTemplate.aspx", "EClearanceTemplate") Then
            If Generic.ToInt(ViewState("ClearanceTemplateDetiNo")) > 0 Then
                Generic.ClearControls(Me, "Panel2")
                ModalPopupExtender2.Show()
            Else
                MessageBox.Alert(MessageTemplate.NoSelectedTransaction, "warning", Me)
            End If

        Else
            MessageBox.Information(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEditDeti_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "EmpClearanceTemplate.aspx", "EClearanceTemplate") Then
            Dim lnk As New LinkButton
            lnk = sender
            PopulateDataDeti(Generic.ToInt(lnk.CommandArgument))
            ModalPopupExtender2.Show()
        Else
            MessageBox.Information(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSaveDeti_Click(sender As Object, e As EventArgs)
        If SQLHelper.ExecuteNonQuery("EClearanceTemplateSignatory_WebSave", UserNo, Generic.ToInt(txtCodeDeti.Text), Generic.ToInt(ViewState("ClearanceTemplateDetiNo")), Generic.ToInt(hifEmployeeNo.Value)) > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDeti()
        Else
            MessageBox.Alert(MessageTemplate.ErrorSave, "warning", Me)
        End If
    End Sub

    Protected Sub lnkDeleteDeti_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "EmpClearanceTemplate.aspx", "EClearanceTemplate") Then
            Dim fieldValues As List(Of Object) = grdDeti.GetSelectedFieldValues(New String() {"ClearanceTemplateSignatoryNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EClearanceTemplateSignatory", UserNo, item)                
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGridDeti()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

#End Region

End Class
