Imports System.Data
Imports clsLib
Imports DevExpress.Web


Partial Class Secured_frmEmailTemplateList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        PopulateGrid()
    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EEmailTemp_Web", UserNo, "", Generic.ToStr(Session("xMenuType")), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    'Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '        Dim URL As String
    '        URL = Generic.GetFirstTab("0")
    '        If URL <> "" Then
    '            Response.Redirect(URL)
    '        End If
    '    End If
    'End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmailTemp_WebOne", UserNo, Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmailTempNo"})))
            PopulateData()
            Generic.PopulateData(Me, "pnlPopupMain", dt)
            mdlMain.Show()
        End If
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Private Function SaveRecord() As Boolean
        If SQLHelper.ExecuteNonQuery("EEmailTemp_WebSave", UserNo, Generic.ToInt(txtCode.Text), txtEmailTempCode.Text, txtEmailTempDesc.Text, txtEmailTempSubj.Text, txtEmailTempMsg.Text, Generic.ToStr(Session("xmenutype")), Generic.ToStr(Me.txtEmailAddress.Text), "", PayLocNo, Generic.ToInt(cboEmailTriggerNo.SelectedValue), Generic.ToBol(chkIsEnabledMain.Checked)) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If
    End Function


#Region "***********Email Update***********"
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            PopulateData()
            Generic.ClearControls(Me, "pnlPopupMain")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            mdlMain.Show()
        End If
    End Sub
    Protected Sub PopulateData()
        Try
            cboEmailTriggerNo.DataSource = SQLHelper.ExecuteDataSet("EEmailTrigger_WeblookUp_Reference", UserNo, PayLocNo, Generic.ToStr(Session("xMenuType")))
            cboEmailTriggerNo.DataTextField = "tdesc"
            cboEmailTriggerNo.DataValueField = "tNo"
            cboEmailTriggerNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkViewParam_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            Dim EmailTriggerNo As Integer = Generic.ToInt(cboEmailTriggerNo.SelectedValue)

            dtx = SQLHelper.ExecuteDataTable("EEmailTrigger_ViewParam", UserNo, PayLocNo, EmailTriggerNo)

            For Each rowx As DataRow In dtx.Rows
                messagedialog = Generic.ToStr(rowx("Retval"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlMain.Show()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmailTempNo", "Code"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        PopulateGridDetl()

    End Sub
    Private Sub PopulateGridDetl()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmailRecipient_Web", UserNo, PayLocNo, Generic.ToInt(ViewState("TransNo")))
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub
    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
        Generic.ClearControls(Me, "pnlPopupDetl")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
        EnabledDetlControls()
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        'End If
    End Sub

    Protected Sub btnSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ds As New DataSet
        Dim RetVal As Integer = 0, xMessage As String = "", alertType As String = ""

        Dim RecipientNo As Integer = Generic.ToInt(txtEmailRecipientNo.Text)
        Dim RecipientCode As String = Generic.ToStr(txtEmailRecipientCode.Text)
        Dim RecipientDesc As String = Generic.ToStr(txtEmailRecipientDesc.Text)
        Dim TemplateNo As Integer = Generic.ToInt(ViewState("TransNo"))
        Dim OrganizationNo As Integer = Generic.ToInt(cboOrganizationLNo.SelectedValue)
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim Email As String = Generic.ToStr(txtEmail.Text)
        Dim IsStatic As Boolean = Generic.ToBol(chkIsStatic.Checked)
        Dim IsEnabled As Boolean = Generic.ToBol(chkIsEnabled.Checked)


        ds = SQLHelper.ExecuteDataSet("EEmailRecipient_WebValidate", UserNo, RecipientNo, RecipientCode, RecipientDesc, TemplateNo, OrganizationNo, EmployeeNo, Email, IsStatic, IsEnabled)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                RetVal = Generic.ToInt(ds.Tables(0).Rows(0)("tProceed"))
                xMessage = Generic.ToStr(ds.Tables(0).Rows(0)("xMessage"))
                alertType = Generic.ToStr(ds.Tables(0).Rows(0)("AlertType"))
            End If
        End If

        If RetVal = 1 Then
            MessageBox.Alert(xMessage, alertType, Me)
            mdlShowDetl.Show()
            Exit Sub
        Else
            If SQLHelper.ExecuteNonQuery("EEmailRecipient_WebSave", UserNo, RecipientNo, RecipientCode, RecipientDesc, TemplateNo, OrganizationNo, EmployeeNo, Email, IsStatic, IsEnabled) > 0 Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                PopulateGridDetl()
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        End If
    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmailRecipientNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EEmailRecipient_WebOne", Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
                    Generic.PopulateData(Me, "pnlPopupDetl", dt)
                Next
                mdlShowDetl.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Private Sub EnabledDetlControls()
        txtEmployeeName.Enabled = True
        txtEmail.Enabled = True
        chkIsStatic.Enabled = True
        cboOrganizationLNo.Enabled = True
        txtEmployeeName.CssClass = "form-control required"
        txtEmail.CssClass = "form-control required"
        cboOrganizationLNo.CssClass = "form-control required"
        If Generic.ToInt(cboOrganizationLNo.SelectedValue) > 0 Then
            txtEmployeeName.Enabled = False
            txtEmail.Enabled = False
            chkIsStatic.Enabled = False
            txtEmployeeName.CssClass = "form-control"
            txtEmail.CssClass = "form-control"

        ElseIf Generic.ToInt(hifEmployeeNo.Value) > 0 Then
            txtEmail.Enabled = False
            cboOrganizationLNo.Enabled = False
            chkIsStatic.Enabled = False
            cboOrganizationLNo.CssClass = "form-control"
            txtEmail.CssClass = "form-control"

        ElseIf Generic.ToBol(chkIsStatic.Checked) Then
            cboOrganizationLNo.Enabled = False
            cboOrganizationLNo.ClearSelection()
            txtEmployeeName.Text = ""
            hifEmployeeNo.Value = 0
            txtEmployeeName.Enabled = False
            cboOrganizationLNo.CssClass = "form-control"
            txtEmployeeName.CssClass = "form-control"
        End If
        mdlShowDetl.Show()
    End Sub

    Protected Sub cboEOrganizationL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboOrganizationLNo.SelectedIndexChanged
        EnabledDetlControls()
    End Sub

    Protected Sub hifEmployeeNo_ValueChanged(sender As Object, e As EventArgs) Handles hifEmployeeNo.ValueChanged
        EnabledDetlControls()
    End Sub

    Protected Sub chkIsStatic_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsStatic.CheckedChanged
        EnabledDetlControls()
    End Sub

    Private Sub txtEmployeeName_TextChanged(sender As Object, e As EventArgs) Handles txtEmployeeName.TextChanged
        If Generic.ToStr(txtEmployeeName.Text) = "" Then
            hifEmployeeNo.Value = 0
        End If
        EnabledDetlControls()
    End Sub



#End Region
End Class

