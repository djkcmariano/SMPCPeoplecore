Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_BENLeavePolicyList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Dim xBase As New clsBase.clsBaseLibrary
    Dim IsCompleted As Integer = 0
    Dim process_status As String = ""
    Dim err_num As Integer = 0

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("ELeavePolicy_Web", UserNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateGridDetl(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ELeavePolicyDeti_Web", UserNo, id)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
            populateCombo()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
       
    End Sub

    Private Sub populateCombo()
        Try
            cboLeavetypeno.DataSource = SQLHelper.ExecuteDataSet("ELeaveType_WebLookupBalance", UserNo, PayLocNo)
            cboLeavetypeno.DataValueField = "tNo"
            cboLeavetypeno.DataTextField = "tDesc"
            cboLeavetypeno.DataBind()

        Catch ex As Exception

        End Try
        Try
            cboPayclassNo.DataSource = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", UserNo, PayLocNo)
            cboPayclassNo.DataValueField = "tNo"
            cboPayclassNo.DataTextField = "tDesc"
            cboPayclassNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

#Region "********Main*******"
    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeavePolicyNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("ELeavePolicy_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                Next
                mdlMain.Show()
            Else
                MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeavePolicyNo", "LeavePolicyCode"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = "Transaction No. : " & obj(1)
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"LeavePolicyNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ELeavePolicy", UserNo, item)
                Generic.DeleteRecordAuditCol("ELeavePolicyDeti", UserNo, "LeavePolicyNo", item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)

        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        saverecord()
    End Sub
    Private Function saverecord() As Boolean
        Dim leavetypeno As Integer = Generic.CheckDBNull(Me.cboLeavetypeno.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim employeestatno As Integer = Generic.CheckDBNull(Me.cboEmployeeStatNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim employeeClassno As Double = Generic.CheckDBNull(Me.cboEmployeeClassNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim jobgradeNo As Double = Generic.CheckDBNull(Me.cboJobGradeNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim datebaseno As Double = Generic.CheckDBNull(Me.cboDateBaseNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim MaxYearService As Double = Generic.CheckDBNull(Me.txtMaxYearService.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim MaxCreditAdd As Double = Generic.CheckDBNull(Me.txtMaxCreditAdd.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim AddCreditPerYr As Double = Generic.CheckDBNull(Me.txtAddCreditPerYr.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim NoOfMonth As Double = Generic.CheckDBNull(Me.txtnoofmonth.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim LeaveNo As Integer = Generic.CheckDBNull(Me.hifLeavePolicyNo.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim payClassNo As Integer = Generic.ToInt(cboPayclassNo.SelectedValue)
        Dim DateModeProrateNo As Integer = Generic.ToInt(cboDateModeProrateNo.SelectedValue)

        Dim dt As DataTable, error_num As Integer = 0, error_message As String = "", RetVal As Boolean = False

        dt = SQLHelper.ExecuteDataTable("ELeavePolicy_WebSave", UserNo, LeaveNo, Me.txtLeavepolicyDesc.Text.ToString, leavetypeno, employeestatno, employeeClassno, jobgradeNo, datebaseno, txtIsSuspended.Checked, txtIsCreditAutomated.Checked, txtIsApplyToAll.Checked, Generic.ToInt(Me.cboRankNo.SelectedValue), Generic.ToInt(Me.cboDateModeNo.SelectedValue), NoOfMonth, Generic.ToInt(Me.txtMaxCreditAccumulated.Text), Generic.ToDec(txtMaxCreditAccumulatedYear.Text), MaxYearService, MaxCreditAdd, AddCreditPerYr, Session("xPayLocNo"), payClassNo, DateModeProrateNo)
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
        Return RetVal
    End Function

#End Region

#Region "********Detail********"


    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeavePolicyDetiNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("ELeavePolicyDeti_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupDetl", dt)
                Next
                mdlDetl.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkDeleteDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"LeavePolicyDetiNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ELeavePolicyDeti", UserNo, item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGridDetl(ViewState("TransNo"))
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If

    End Sub

    Protected Sub lnkAddDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            If Generic.ToInt(ViewState("TransNo")) > 0 Then
                Generic.ClearControls(Me, "pnlPopupDetl")
                mdlDetl.Show()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub
  
    'Submit record
    Protected Sub btnSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fSaveRecord As Integer = SaveRecordDetl()
        If fSaveRecord = 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Private Function SaveRecordDetl() As Integer
        Dim tProceed As Boolean = True
        Dim leavepolicydetino As Integer = Generic.CheckDBNull(txtleavepolicyDetiNo.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim fTransno As Integer = Generic.CheckDBNull(ViewState("TransNo"), clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim leavehrs As Double = Generic.CheckDBNull(txtleavehrs.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim fromYear As Double = Generic.CheckDBNull(txtfromYear.Text, Global.clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim toYear As Double = Generic.CheckDBNull(txttoyear.Text, Global.clsBase.clsBaseLibrary.enumObjectType.DblType)

        'If txtRemark.Text = "" Or txtfromYear.Text = "" Or txttoyear.Text = "" Or txtleavehrs.Text = "" Then
        '    tProceed = False
        'End If
        If tProceed Then
            If SQLHelper.ExecuteNonQuery("ELeavePolicyDeti_WebSave", UserNo, leavepolicydetino, fTransno, txtRemark.Text.ToString, leavehrs, True, False, fromYear, toYear) > 0 Then
                SaveRecordDetl = 0
            Else
                SaveRecordDetl = 1
            End If
        Else
            SaveRecordDetl = 2
        End If

    End Function

#End Region

#Region "Processing"

    Protected Sub lnkProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        mdlShow.Show()
    End Sub


    Protected Sub btnSaveDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then

            '//validate start here
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("ELeavePolicy_WebGenerator_Validate", Generic.ToStr(txtPayStartDate.Text), Generic.ToStr(txtPayEndDate.Text))

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("RetVal"))
                messagedialog = Generic.ToStr(rowx("xMessage"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                mdlShow.Show()
                Exit Sub
            End If

            DTRAppendAsyn()
            Dim strx As String = process_status
            If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, strx, "ELeaveCredit", "ELeavePolicyCrediting_MANUAL", 1, ViewState("Id"))
                PopulateGrid()
                MessageBox.Critical(strx, Me)
            Else
                SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "ELeaveCredit", "ELeavePolicyCrediting_MANUAL", 1, ViewState("Id"))
                PopulateGrid()
                process_status = Replace(process_status, "Command complete. Processing Time is :", "Leave Credit Processing completed at ")
                MessageBox.Success(process_status, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If
    End Sub

    Private Sub DTRAppendAsyn()
        Dim xcmdProcSAVE As SqlClient.SqlCommand

        Try

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand

            xcmdProcSAVE.CommandText = "ELeavePolicy_WebGenerator_CutOff"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = xBase.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0

            xcmdProcSAVE.Parameters.Add("@xStartDate", SqlDbType.VarChar, 10)
            xcmdProcSAVE.Parameters("@xStartDate").Value = Generic.CheckDBNull(txtPayStartDate.Text.ToString, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)

            xcmdProcSAVE.Parameters.Add("@xEndDate", SqlDbType.VarChar, 10)
            xcmdProcSAVE.Parameters("@xEndDate").Value = Generic.CheckDBNull(txtPayEndDate.Text.ToString, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)

            process_status = AssynChronous.xRunCommandAsynchronous(xcmdProcSAVE, "ELeavePolicy_WebGenerator_CutOff", SQLHelper.ConSTRAsyn, IsCompleted, err_num)
            Session("IsCompleted") = 0 'IsCompleted

            If Session("IsCompleted") = 1 Then
                'clsModalControls.SetModalPopupControls(CType(Master.FindControl("cphBody"), ContentPlaceHolder), "completed")
            End If
        Catch
            'Response.RedirectLocation = Session("xFormname") & "?IsClickMain=" & IsClickMain
        End Try

    End Sub
#End Region
End Class









