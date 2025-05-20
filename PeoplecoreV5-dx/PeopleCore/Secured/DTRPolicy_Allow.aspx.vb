Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_DTRAllowPolicyList
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0


#Region "Mail"
    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRAllowPolicy_Web", UserNo, PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()

            If ViewState("TransNo") = 0 Then
                Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"DTRAllowPolicyNo", "Code", "PayClassNo", "DTRPolicyTypeNo"})
                ViewState("TransNo") = obj(0)
                lblDetl.Text = obj(1)
                ViewState("PayClassNo") = obj(2)
                ViewState("DTRPolicyTypeNo") = obj(3)
            End If

            PopulateGrid_Detl()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateDropDownList()
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDownList()
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", Generic.ToInt(Session("xPayLocNo")))
        Generic.PopulateDropDownList(UserNo, Me, "panel1", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboPayClassNo.DataSource = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", UserNo, PayLocNo)
            cboPayClassNo.DataValueField = "tNo"
            cboPayClassNo.DataTextField = "tDesc"
            cboPayClassNo.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRAllowPolicyNo", "Code", "PayClassNo", "DTRPolicyTypeNo"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        ViewState("PayClassNo") = obj(2)
        ViewState("DTRPolicyTypeNo") = obj(3)
        PopulateGrid_Detl()
    End Sub
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.EnableControls(Me, "pnlPopupDetl", True)
            lnkSave.Enabled = True
            chkIsApplyToAll.Checked = True
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub
    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Generic.ClearControls(Me, "pnlPopupDetl")
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim INo As String = Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRAllowPolicyNo"}))
            PopulateData(INo)
            Generic.EnableControls(Me, "pnlPopupDetl", True)
            lnkSave.Enabled = True
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRAllowPolicyNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EDTRAllowPolicyDeti", UserNo, "DTRAllowPolicyNo", item)
                Generic.DeleteRecordAudit("EDTRAllowPolicy", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        SaveRecord()
        MessageBox.Success(MessageTemplate.SuccessSave, Me)
        PopulateGrid()
    End Sub

   

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateData(iNo As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDTRAllowPolicy_WebOne", UserNo, iNo)
        Generic.PopulateData(Me, "pnlPopupDetl", dt)
    End Sub
    Private Function SaveRecord() As Integer
        Dim costcenterno As Integer = 0
        Dim RetVal As Object = 0
        Dim PayClassNo As Integer = Generic.ToInt(Me.cboPayClassNo.SelectedValue)
        Dim PayScheduleNo As Integer = Generic.ToInt(Me.cboPayScheduleNo.SelectedValue)
        RetVal = SQLHelper.ExecuteScalar("EDTRAllowPolicy_WebSave", UserNo, Generic.ToInt(txtCode.Text), PayClassNo, PayScheduleNo, Me.txtDTRAllowPolicyDesc.Text, Generic.ToInt(Me.cboPayIncomeTypeNo.SelectedValue), Generic.ToInt(Me.cbodtrpolicytypeNo.SelectedValue), chkIsApplyToAll.Checked, Generic.ToDec(Me.txtCurrentSalary.Text), PayLocNo)
        Return Generic.ToInt(RetVal)
    End Function
#End Region

#Region "Detail"
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
    Private Sub show_hide_Detl_Form(policytypeNo As Integer)
        If policytypeNo = 1 Or policytypeNo = 2 Or policytypeNo = 6 Then '1-extended overtime allowance, 2. Overtime meal allowance 6. Meal allowance
            fRegisterStartupScript("Sript", "showhide('1');")
        ElseIf policytypeNo = 3 Then '3. shift allowance
            fRegisterStartupScript("Sript", "showhide('3');")
        ElseIf policytypeNo = 4 Then '4. children allowance
            fRegisterStartupScript("Sript", "showhide('4');")
        End If
    End Sub
    Protected Sub PopulateGrid_Detl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRAllowPolicyDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub PopulateData_Detl(iNo As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDTRAllowPolicyDeti_WebOne", UserNo, iNo)
        Generic.PopulateData(Me, "panel1", dt)
    End Sub
    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            If Generic.ToInt(ViewState("TransNo")) > 0 Then
                Try
                    cboDayTypeNo.DataSource = SQLHelper.ExecuteDataSet("EDayType_WebLookup_PayClass", UserNo, Generic.ToInt(ViewState("PayClassNo")))
                    cboDayTypeNo.DataValueField = "tNo"
                    cboDayTypeNo.DataTextField = "tDesc"
                    cboDayTypeNo.DataBind()
                Catch ex As Exception

                End Try

                Generic.ClearControls(Me, "panel1")
                Generic.EnableControls(Me, "panel1", True)
                lnkSave.Enabled = True
                show_hide_Detl_Form(ViewState("DTRPolicyTypeNo"))
                ModalPopupExtender1.Show()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub
    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Generic.ClearControls(Me, "panel1")
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim INo As String = Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRAllowPolicyDetiNo"}))
            Try
                cboDayTypeNo.DataSource = SQLHelper.ExecuteDataSet("EDayType_WebLookup_PayClass", UserNo, Generic.ToInt(ViewState("PayClassNo")))
                cboDayTypeNo.DataValueField = "tNo"
                cboDayTypeNo.DataTextField = "tDesc"
                cboDayTypeNo.DataBind()
            Catch ex As Exception

            End Try
            PopulateData_Detl(INo)
            Generic.EnableControls(Me, "panel1", True)
            lnkSave.Enabled = True
            'show_hide_Detl_Form(ViewState("DTRPolicyTypeNo"))
            fRegisterStartupScript("Sript", "showhidenext_behind('" & ViewState("DTRPolicyTypeNo").ToString & "','" & cboPolicyDetlTypeNo.SelectedValue.ToString & "');")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"DTRAllowPolicyDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTRAllowPolicyDeti", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
        SaveRecord_Detl()
    End Sub
    Private Function SaveRecord_Detl() As Boolean

        Dim costcenterno As Integer = 0
        Dim ShiftNo As Integer = Generic.ToInt(Me.cboShiftNo.SelectedValue)
        Dim PolicyDetlTypeNo As Integer = Generic.ToInt(cboPolicyDetlTypeNo.SelectedValue)
        Dim dt As DataTable, error_num As Integer = 0, error_message As String = "", RetVal As Boolean = False

        dt = SQLHelper.ExecuteDataTable("EDTRAllowPolicyDeti_WebSave", UserNo, Generic.ToInt(txtDTRAllowPolicyDetiNo.Text), ViewState("TransNo"), ShiftNo, Generic.ToDec(Me.txthrsFrom.Text), Generic.ToDec(Me.txtHrsTo.Text), Generic.ToDec(Me.txtAmount.Text), False, PolicyDetlTypeNo, txtOut1.Text, Generic.ToInt(Me.cboDayTypeNo.SelectedValue), Generic.ToInt(Me.cboDayno.SelectedValue))
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
            PopulateGrid_Detl()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
        Return RetVal


    End Function
#End Region
End Class





