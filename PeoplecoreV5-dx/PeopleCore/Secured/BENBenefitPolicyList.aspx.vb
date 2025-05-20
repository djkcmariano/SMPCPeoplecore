Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_BENBenefitPolicyList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EBenefitPolicy_Web", UserNo, "", PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateGridDetl(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EBenefitPolicyDeti_Web", UserNo, id)
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

        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)

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
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitPolicyNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EBenefitPolicy_WebOne", UserNo, Generic.ToInt(i))
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
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitPolicyNo", "code"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = "Transaction No. : " & obj(1)
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BenefitPolicyNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EBenefitPolicy", UserNo, item)
                Generic.DeleteRecordAuditCol("EBenefitPolicyDeti", UserNo, "BenefitPolicyNo", item)
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
        If SQLHelper.ExecuteNonQuery("EBenefitPolicy_WebSave", UserNo, Generic.ToInt(hifBenefitPolicyNo.Value), txtBenefitPolicyDesc.Text, Generic.ToInt(cboBenefitTypeNo.SelectedValue), _
                                     Generic.ToInt(cboEmployeeStatNo.SelectedValue), Generic.ToInt(cboEmployeeClassNo.SelectedValue), Generic.ToInt(cboJobGradeNo.SelectedValue), _
                                     Generic.ToInt(cboDateBaseNo.SelectedValue), Generic.ToInt(cboDateModeNo.SelectedValue), Generic.ToDbl(txtBenefitCreditCal.Text), _
                                     Generic.ToDbl(txtnoofmonth.Text), Generic.ToInt(cboPositionNo.SelectedValue), Generic.ToInt(cboGroupNo.SelectedValue), PayLocNo) > 0 Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else

        End If
    End Sub

    'Private Function saverecord() As Boolean
    '    Dim leavetypeno As Integer = Generic.CheckDBNull(Me.cboLeavetypeno.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '    Dim employeestatno As Integer = Generic.CheckDBNull(Me.cboEmployeeStatNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '    Dim employeeClassno As Double = Generic.CheckDBNull(Me.cboEmployeeClassNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '    Dim jobgradeNo As Double = Generic.CheckDBNull(Me.cboJobGradeNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '    Dim datebaseno As Double = Generic.CheckDBNull(Me.cboDateBaseNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '    Dim MaxYearService As Double = Generic.CheckDBNull(Me.txtMaxYearService.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
    '    Dim MaxCreditAdd As Double = Generic.CheckDBNull(Me.txtMaxCreditAdd.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
    '    Dim AddCreditPerYr As Double = Generic.CheckDBNull(Me.txtAddCreditPerYr.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
    '    Dim NoOfMonth As Double = Generic.CheckDBNull(Me.txtnoofmonth.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
    '    Dim LeaveNo As Integer = Generic.CheckDBNull(Me.hifLeavePolicyNo.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)
    '    Dim payClassNo As Integer = Generic.ToInt(cboPayclassNo.SelectedValue)
    '    Dim DateModeProrateNo As Integer = Generic.ToInt(cboDateModeProrateNo.SelectedValue)

    '    Dim dt As DataTable, error_num As Integer = 0, error_message As String = "", RetVal As Boolean = False

    '    dt = SQLHelper.ExecuteDataTable("ELeavePolicy_WebSave", UserNo, LeaveNo, Me.txtLeavepolicyDesc.Text.ToString, leavetypeno, employeestatno, employeeClassno, jobgradeNo, datebaseno, txtIsSuspended.Checked, txtIsCreditAutomated.Checked, txtIsApplyToAll.Checked, Generic.ToInt(Me.cboRankNo.SelectedValue), Generic.ToInt(Me.cboDateModeNo.SelectedValue), NoOfMonth, Generic.ToInt(Me.txtMaxCreditAccumulated.Text), Generic.ToDec(txtMaxCreditAccumulatedYear.Text), MaxYearService, MaxCreditAdd, AddCreditPerYr, Session("xPayLocNo"), payClassNo, DateModeProrateNo)
    '    For Each row As DataRow In dt.Rows
    '        RetVal = True
    '        error_num = Generic.ToInt(row("Error_num"))
    '        If error_num > 0 Then
    '            error_message = Generic.ToStr(row("ErrorMessage"))
    '            MessageBox.Critical(error_message, Me)
    '            RetVal = False
    '        End If
    '    Next
    '    If RetVal = False And error_message = "" Then
    '        MessageBox.Critical(MessageTemplate.ErrorSave, Me)
    '    End If
    '    If RetVal = True Then
    '        PopulateGrid()
    '        MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '    End If
    '    Return RetVal
    'End Function

#End Region

#Region "********Detail********"


    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitPolicyDetiNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EBenefitPolicyDeti_WebOne", UserNo, Generic.ToInt(i))
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
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"BenefitPolicyDetiNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EBenefitPolicyDeti", UserNo, item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
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

    ''Submit record
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
        Dim benefitpolicydetino As Integer = Generic.ToInt(txtBenefitPolicyDetiNo.Text)
        Dim fTransno As Integer = Generic.ToInt(ViewState("TransNo"))
        Dim amount As Double = Generic.ToDbl(txtAmount.Text)
        Dim fromYear As Double = Generic.ToDbl(txtfromYear.Text)
        Dim toYear As Double = Generic.ToDbl(txttoyear.Text)

        'If txtRemark.Text = "" Or txtfromYear.Text = "" Or txttoyear.Text = "" Or txtleavehrs.Text = "" Then
        '    tProceed = False
        'End If
        If tProceed Then
            If SQLHelper.ExecuteNonQuery("EBenefitPolicyDeti_WebSave", UserNo, benefitpolicydetino, fTransno, txtRemark.Text, amount, fromYear, toYear) > 0 Then
                SaveRecordDetl = 0
            Else
                SaveRecordDetl = 1
            End If
        Else
            SaveRecordDetl = 2
        End If

    End Function

#End Region

End Class









