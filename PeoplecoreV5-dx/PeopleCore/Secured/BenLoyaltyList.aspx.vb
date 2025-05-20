Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_BenLoyaltyList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    Dim tabOrder As Integer = 0
    Dim tstatus As Integer = 0

#Region "Main"

    Protected Sub PopulateGrid()
        Try

            'grdMain.DataSourceID = SqlDataSource1.ID
            'Generic.PopulateSQLDatasource("EBenefitLoyalty_WebFiltered", SqlDataSource1, UserNo.ToString(), PayLocNo.ToString(), Generic.ToInt(cboTabNo.SelectedValue).ToString(), Filter1.SearchText, Generic.ToInt(cbofilterby.SelectedValue), Generic.ToInt(cbofiltervalue.SelectedValue), 0, 0, "", "", 0)

            Dim FilterByNo As Integer = 0, FilterById As Integer = 0
            Dim xYear As Integer = 0
            Dim xMonth As Integer = 0
            Dim LoyaltyTypeNo As Short = 0

            If IsNumeric(Me.cboFilteredbyNo.SelectedValue) = True Then
                FilterByNo = Generic.ToInt(Me.cboFilteredbyNo.SelectedValue)
            End If
            If IsNumeric(Me.hiffilterbyid.Value) = True Then
                FilterById = Generic.ToInt(Me.hiffilterbyid.Value)
            End If
            If IsNumeric(Me.txtApplicableYear.Text) = True Then
                xYear = Generic.ToInt(Me.txtApplicableYear.Text)
            End If
            If IsNumeric(Me.cboApplicableMonth.SelectedValue) = True Then
                xMonth = Generic.ToInt(Me.cboApplicableMonth.SelectedValue)
            End If
            If IsNumeric(Me.cboBenefitLoyaltyPolicyNo.SelectedValue) = True Then
                LoyaltyTypeNo = Generic.ToInt(Me.cboBenefitLoyaltyPolicyNo.SelectedValue)
            End If

            Dim dt As DataTable
            'dt = SQLHelper.ExecuteDataTable("EBenefitLoyalty_WebFiltered", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue), Filter1.SearchText, Generic.ToInt(cbofilterby.SelectedValue), Generic.ToInt(cbofiltervalue.SelectedValue), 0, 0, "", "", 0)
            dt = SQLHelper.ExecuteDataTable("EBenefitLoyalty_WebFiltered", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue), Filter1.SearchText, FilterByNo, FilterById, xMonth, xYear, Generic.ToStr(Me.txtDateFrom.Text), Generic.ToStr(Me.txtDateTo.Text), Generic.ToInt(LoyaltyTypeNo))
            grdMain.DataSource = dt
            grdMain.DataBind()

            'EntityServerModeDataSource1.ContextTypeName = "EmployeeDataContext"
            'EntityServerModeDataSource1.TableName = _dt.TableName
            'grdMain.DataSourceID = EntityServerModeDataSource1.ID

            'Dim dt As DataTable
            'dt = SQLHelper.ExecuteDataTable("EBenefitLoyalty_Web", UserNo, "", Generic.ToInt(cboTabNo.SelectedValue), 0, 0, 0, 0, 0, PayLocNo)
            'grdMain.DataSource = dt
            'grdMain.DataBind()


            If ViewState("TabNo") <> Generic.ToInt(cboTabNo.SelectedValue) Then
                ViewState("TransNo") = 0
            End If

            If ViewState("TransNo") = 0 Then
                Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"BenefitLoyaltyNo", "Code", "FullName"})
                ViewState("TransNo") = obj(0)
                lblAwol.Text = " AWOL/LWOP DETAILS: " & obj(1) & " - " & obj(2)
                lblRating.Text = " PERFORMANCE DETAILS: " & obj(1) & " - " & obj(2)
                lblADU.Text = " DA DETAILS: " & obj(1) & " - " & obj(2)

            Else
                Dim obj As Object() = grdMain.GetRowValuesByKeyValue(Generic.ToInt(ViewState("TransNo")), New String() {"BenefitLoyaltyNo", "Code", "FullName"})
                ViewState("TransNo") = obj(0)
                lblAwol.Text = " AWOL/LWOP DETAILS: " & obj(1) & " - " & obj(2)
                lblRating.Text = " PERFORMANCE DETAILS: " & obj(1) & " - " & obj(2)
                lblADU.Text = " DA DETAILS: " & obj(1) & " - " & obj(2)
            End If

            If dt.Rows.Count = 0 Then
                lblAwol.Text = "AWOL/LWOP DETAILS"
                lblRating.Text = "PERFORMANCE DETAILS"
                lblADU.Text = "DA DETAILS"
            End If

            ViewState("TabNo") = Generic.ToInt(cboTabNo.SelectedValue)

            PopulateDetlAWOL()
            PopulateDetlRating()
            PopulateDetlADU()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateDataAWOL(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBenefitLoyaltyAwol_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetlAWOLAdd", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBenefitLoyalty_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopup", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Me.cboTabNo.Text = Generic.ToInt(ViewState("TabNo"))
            PopulateDropDown()

            Try
                cboFilteredbyNo.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EFilteredByAll", PayLocNo, "", "")
                cboFilteredbyNo.DataTextField = "tdesc"
                cboFilteredbyNo.DataValueField = "tNo"
                cboFilteredbyNo.DataBind()
            Catch ex As Exception

            End Try

            If Generic.ToInt(cboFilteredbyNo.SelectedValue) = 0 Then
                cboFilteredbyNo.Text = "1"
                drpAC.CompletionSetCount = 1
            End If

            Try
                cboApplicableMonth.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EMonth", PayLocNo, "", "")
                cboApplicableMonth.DataTextField = "tdesc"
                cboApplicableMonth.DataValueField = "tNo"
                cboApplicableMonth.DataBind()
            Catch ex As Exception

            End Try

            Try
                cboBenefitLoyaltyPolicyNo.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EBenefitLoyaltyPolicy", PayLocNo, "", "")
                cboBenefitLoyaltyPolicyNo.DataTextField = "tdesc"
                cboBenefitLoyaltyPolicyNo.DataValueField = "tNo"
                cboBenefitLoyaltyPolicyNo.DataBind()
            Catch ex As Exception

            End Try
            'PopulateGrid()
        End If

        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

        PopulateGrid()

        ShowHideButtons(Generic.ToInt(cboTabNo.SelectedValue))

        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 42)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub ShowHideButtons(ByVal tStatus As Short)
        Select Case tStatus
            Case 0, 1, 3
                Me.lnkForward.Visible = False
                Me.lnkPayment.Visible = True
                Me.lnkOnHold.Visible = True
                'Additionals:
                Me.txtEffectiveDate.Visible = True
                Me.lnkGenerateLoyalty.Visible = True
                'Me.Label2.Visible = True
                Me.lnkDelete.Visible = True
                Me.lnkRingReleased.Visible = True
            Case 2
                Me.lnkForward.Visible = False
                Me.lnkPayment.Visible = True
                Me.lnkOnHold.Visible = False
                'Additionals:
                Me.txtEffectiveDate.Visible = False
                Me.lnkGenerateLoyalty.Visible = False
                'Me.Label2.Visible = False
                Me.lnkDelete.Visible = True
                Me.lnkRingReleased.Visible = True
            Case 4
                Me.lnkForward.Visible = True
                Me.lnkPayment.Visible = False
                Me.lnkOnHold.Visible = False
                'Additionals:
                Me.txtEffectiveDate.Visible = False
                Me.lnkGenerateLoyalty.Visible = False
                'Me.Label2.Visible = False
                Me.lnkDelete.Visible = True
                Me.lnkRingReleased.Visible = True
            Case 5, 6
                Me.lnkForward.Visible = False
                Me.lnkPayment.Visible = False
                Me.lnkOnHold.Visible = False
                'Additionals:
                Me.txtEffectiveDate.Visible = False
                Me.lnkGenerateLoyalty.Visible = False
                'Me.Label2.Visible = False
                Me.lnkDelete.Visible = False
                Me.lnkRingReleased.Visible = False
        End Select

        Select Case tabOrder
            Case 0, 1 'Due For Loyalty and All Tab
                ' grdMain.Columns(7).Visible = False 'On Hold
                'grdMain.Columns(8).Visible = False 'On Hold Remarks
                'grdMain.Columns(9).Visible = False 'Suspended
                'grdMain.Columns(11).Visible = False 'Review / On Process
                'grdMain.Columns(12).Visible = False 'Forwarded
                'grdMain.Columns(13).Visible = False 'Paid
                'grdMain.Columns(14).Visible = False 'Status Remarks
                'grdMain.Columns(10).Visible = False 'Suspension Remarks
            Case 2  'On Hold Tab
                'grdMain.Columns(7).Visible = True 'On Hold
                'grdMain.Columns(8).Visible = True 'On Hold Remarks
                'grdMain.Columns(9).Visible = False 'Suspended
                'grdMain.Columns(11).Visible = False 'Review / On Process
                'grdMain.Columns(12).Visible = False 'Forwarded
                'grdMain.Columns(13).Visible = False 'Paid
                'grdMain.Columns(14).Visible = False 'Status Remarks
                'grdMain.Columns(10).Visible = False 'Suspension Remarks
            Case 3  'Suspended Tab
                'grdMain.Columns(7).Visible = False 'On Hold
                'grdMain.Columns(8).Visible = False 'On Hold Remarks
                'grdMain.Columns(9).Visible = True 'Suspended
                'grdMain.Columns(11).Visible = False 'Review / On Process
                'grdMain.Columns(12).Visible = False 'Forwarded
                'grdMain.Columns(13).Visible = False 'Paid
                'grdMain.Columns(14).Visible = False 'Status Remarks
                'grdMain.Columns(10).Visible = True 'Suspension Remarks
            Case 4  'For Review Tab
                'grdMain.Columns(7).Visible = False 'On Hold
                'grdMain.Columns(8).Visible = False 'On Hold Remarks
                'grdMain.Columns(9).Visible = False 'Suspended
                'grdMain.Columns(11).Visible = True 'Review / On Process
                'grdMain.Columns(12).Visible = False 'Forwarded
                'grdMain.Columns(13).Visible = False 'Paid
                'grdMain.Columns(14).Visible = False 'Status Remarks
                'grdMain.Columns(10).Visible = False 'Suspension Remarks
            Case 5  'Released Tab
                'grdMain.Columns(7).Visible = False 'On Hold
                'grdMain.Columns(8).Visible = False 'On Hold Remarks
                'grdMain.Columns(9).Visible = False 'Suspended
                'grdMain.Columns(11).Visible = False 'Review / On Process
                'grdMain.Columns(12).Visible = True 'Forwarded
                'grdMain.Columns(13).Visible = True 'Paid
                'grdMain.Columns(14).Visible = True 'Status Remarks
                'grdMain.Columns(10).Visible = False 'Suspension Remarks
            Case 6 'Due For Loyalty and All Tab
                'grdMain.Columns(7).Visible = True 'On Hold
                'grdMain.Columns(8).Visible = False 'On Hold Remarks
                'grdMain.Columns(9).Visible = True 'Suspended
                'grdMain.Columns(11).Visible = False 'Review / On Process
                'grdMain.Columns(12).Visible = True 'Forwarded
                'grdMain.Columns(13).Visible = False 'Paid
                'grdMain.Columns(14).Visible = True 'Status Remarks
                'grdMain.Columns(10).Visible = False 'Suspension Remarks
        End Select

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
        '    Generic.ClearControls(Me, "pnlPopup")
        '    Generic.EnableControls(Me, "pnlPopup", True)
        '    cboApprovalStatNo.Text = 2
        '    cboApprovalStatNo.Enabled = False
        '    lnkSave.Enabled = True
        '    mdlShow.Show()
        'Else
        '    MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        'End If
    End Sub


    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
        '    Dim lnk As New LinkButton
        '    lnk = sender
        '    Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        '    PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveApplicationNo"})))
        '    Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
        '    Generic.EnableControls(Me, "pnlPopup", IsEnabled)
        '    cboApprovalStatNo.Enabled = True
        '    lnkSave.Enabled = True
        '    mdlShow.Show()
        'Else
        '    MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        'End If
    End Sub

    Protected Sub lnkEditAWOL_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataAWOL(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitLoyaltyAwolNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Generic.EnableControls(Me, "pnlPopupDetlAWOLAdd", IsEnabled)

            lnkSaveAWOL.Enabled = IsEnabled
            mdlDetlAWOLAdd.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BenefitLoyaltyNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EBenefitLoyaltyAwol", UserNo, "BenefitLoyaltyNo", item)
                Generic.DeleteRecordAudit("EBenefitLoyalty", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkDeleteAWOL_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, lbl2 As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetlAWOL.GetSelectedFieldValues(New String() {"BenefitLoyaltyAwolNo"})
            Dim str As String = "", i As Integer = 0

            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("EBenefitLoyaltyAwol_WebUpdate_EffectiveDate", UserNo, Generic.ToInt(item), Generic.ToInt(ViewState("TransNo")))
                Generic.DeleteRecordAudit("EBenefitLoyaltyAwol", UserNo, Generic.ToInt(item))
                DeleteCount = DeleteCount + 1
            Next

            MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
        PopulateGrid()
        PopulateDetlAWOL()

    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    'Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
    '    Dim RetVal As Boolean = False
    '    Dim LeaveApplicationNo As Integer = Generic.ToInt(txtLeaveApplicationNo.Text)
    '    Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
    '    Dim LeaveTypeNo As Integer = Generic.ToInt(cboLeavetypeNo.SelectedValue)
    '    Dim StartDate As String = Generic.ToStr(txtStartDate.Text)
    '    Dim EndDate As String = Generic.ToStr(txtEndDate.Text)
    '    Dim AppliedHrs As Double = Generic.ToDec(txtAppliedHrs.Text)
    '    Dim IsForAM As Boolean = Generic.ToBol(txtISForAM.Checked)
    '    Dim Reason As String = Generic.ToStr(txtReason.Text)
    '    Dim ApprovalStatNo As Integer = Generic.ToInt(cboApprovalStatNo.SelectedValue)
    '    Dim ComponentNo As Integer = 1 'Administrator

    '    '//validate start here
    '    Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
    '    Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
    '    dtx = SQLHelper.ExecuteDataTable("ELeaveApplication_WebValidate", UserNo, LeaveApplicationNo, EmployeeNo, LeaveTypeNo, StartDate, EndDate, AppliedHrs, IsForAM, ApprovalStatNo, PayLocNo, ComponentNo)

    '    For Each rowx As DataRow In dtx.Rows
    '        invalid = Generic.ToBol(rowx("tProceed"))
    '        messagedialog = Generic.ToStr(rowx("xMessage"))
    '        alerttype = Generic.ToStr(rowx("AlertType"))
    '    Next

    '    If invalid = True Then
    '        MessageBox.Alert(messagedialog, alerttype, Me)
    '        mdlShow.Show()
    '        Exit Sub
    '    End If

    '    dt = SQLHelper.ExecuteDataTable("ELeaveApplication_WebSave", UserNo, LeaveApplicationNo, EmployeeNo, LeaveTypeNo, StartDate, EndDate, AppliedHrs, IsForAM, Reason, ApprovalStatNo, PayLocNo)
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

    'End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        ViewState("TransNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitLoyaltyNo"}))
        Dim obj As Object() = grdMain.GetRowValues(container.VisibleIndex, New String() {"BenefitLoyaltyNo", "Code", "FullName"})
        'ViewState("TransNo") = obj(0)
        lblAwol.Text = " AWOL/LWOP DETAILS: " & obj(1) & " - " & obj(2)
        lblRating.Text = " PERFORMANCE DETAILS: " & obj(1) & " - " & obj(2)
        lblADU.Text = " DA DETAILS: " & obj(1) & " - " & obj(2)

        PopulateDetlAWOL()
        PopulateDetlRating()
        PopulateDetlADU()

    End Sub

    Protected Sub lnkGenerateLoyalty_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox, lbl2 As New Label
        Dim tcount As Integer, DeleteCount As Integer = 0
        Dim cnt As Integer = 0
        Dim xSQLHelper As New clsBase.SQLHelper

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            If Me.txtEffectiveDate.Text.ToString > "" Then
                cnt = xSQLHelper.ExecuteNonQuery(SQLHelper.ConSTR, "EBenefitLoyalty_WebAutoCreate_Manual", UserNo, Me.txtEffectiveDate.Text.ToString)
                If cnt > 0 Then DeleteCount = DeleteCount + 1

                MessageBox.Success("Generation of Loyalty Award Anniversary Date (" + Me.txtEffectiveDate.Text + ") hase been successfully done. \n Please review the generated record(s).", Me)

                PopulateGrid()

            Else

                MessageBox.Warning("Please input Loyalty Effective Date.", Me)

                Me.txtEffectiveDate.Focus()
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If
    End Sub

    Protected Sub lnkForward_Click(sender As Object, e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Loyalty_ForwardPayroll()
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
        PopulateGrid()
    End Sub

    Protected Sub Loyalty_ForwardPayroll()
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0
        Dim IsProcess As Boolean = False
        Dim fcount As Integer = 0
        Dim scount As Integer = 0

        Dim txt As New ASPxMemo
        Dim str As String = "", i As Integer = 0
        Dim IsRowSelected As Boolean

        For tcount = 0 To grdMain.VisibleRowCount - 1
            i = Generic.ToInt(grdMain.GetRowValues(tcount, New String() {"BenefitLoyaltyNo"}))
            txt = CType(grdMain.FindRowCellTemplateControl(tcount, grdMain.DataColumns("Remarks"), "txtRemarks"), ASPxMemo)
            IsRowSelected = grdMain.Selection.IsRowSelected(tcount)
            If IsRowSelected = True Then
                scount = scount + 1
                fcount = fcount + SQLHelper.ExecuteNonQuery("EBenefitLoyalty_WebForwardPayroll", UserNo, Generic.ToInt(i), Generic.ToStr(txt.Text))
            End If
        Next

        If fcount > 0 Then
            MessageBox.Success("There are (" + scount.ToString + ") transaction(s) has been forwarded to payroll for processing.", Me)
        Else
            If scount = 0 Then
                MessageBox.Warning("There are no transaction(s) forwarded to payroll. Please select transaction(s)", Me)
            End If
        End If

    End Sub

    Protected Sub lnkPayment_Click(sender As Object, e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Loyalty_ForPayment()
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
        PopulateGrid()
    End Sub

    Protected Sub Loyalty_ForPayment()
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0
        Dim IsProcess As Boolean = False
        Dim fcount As Integer = 0
        Dim scount As Integer = 0

        Dim txt As New ASPxMemo
        Dim str As String = "", i As Integer = 0
        Dim IsRowSelected As Boolean

        For tcount = 0 To grdMain.VisibleRowCount - 1
            i = Generic.ToInt(grdMain.GetRowValues(tcount, New String() {"BenefitLoyaltyNo"}))
            txt = CType(grdMain.FindRowCellTemplateControl(tcount, grdMain.DataColumns("Remarks"), "txtRemarks"), ASPxMemo)
            IsRowSelected = grdMain.Selection.IsRowSelected(tcount)
            If IsRowSelected = True Then
                scount = scount + 1
                fcount = fcount + SQLHelper.ExecuteNonQuery("EBenefitLoyalty_WebPayment", UserNo, Generic.ToInt(i), Generic.ToStr(txt.Value))
            End If
        Next

        If fcount > 0 Then
            MessageBox.Success("There are (" + fcount.ToString + ") transaction(s) has been moved to For Review tab.", Me)
        Else
            If scount = 0 Then
                MessageBox.Warning("There are no transaction(s) moved to For Review tab." + "<br>Please select transaction(s).", Me)
            End If
        End If

    End Sub

    Protected Sub lnkOnHold_Click(sender As Object, e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Loyalty_OnHold()
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
        PopulateGrid()
    End Sub

    Protected Sub Loyalty_OnHold()
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0
        Dim IsProcess As Boolean = False
        Dim fcount As Integer = 0
        Dim scount As Integer = 0

        Dim txt As New ASPxTextBox, txtRingDate As New ASPxTextBox
        Dim str As String = "", i As Integer = 0
        Dim IsRowSelected As Boolean

        For tcount = 0 To grdMain.VisibleRowCount - 1
            i = Generic.ToInt(grdMain.GetRowValues(tcount, New String() {"BenefitLoyaltyNo"}))
            txt = CType(grdMain.FindRowCellTemplateControl(tcount, grdMain.DataColumns("Remarks"), "txtRemarks"), ASPxTextBox)
            IsRowSelected = grdMain.Selection.IsRowSelected(tcount)
            If IsRowSelected = True Then
                scount = scount + 1
                fcount = fcount + SQLHelper.ExecuteNonQuery("EBenefitLoyalty_WebHold", UserNo, Generic.ToInt(i), Generic.ToStr(txt.Text))
            End If
        Next

        If fcount > 0 Then
            MessageBox.Success("There are (" + fcount.ToString + ") transaction(s) tagged as on hold.", Me)
        Else
            If scount = 0 Then
                MessageBox.Warning("There are no transaction(s) tagged as on hold. Please select transaction(s).", Me)
            End If
        End If

    End Sub

    Protected Sub lnkRingReleased_Click(sender As Object, e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Loyalty_RingReleased()
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
        PopulateGrid()
    End Sub

    Protected Sub Loyalty_RingReleased()
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0
        Dim IsProcess As Boolean = False
        Dim fcount As Integer = 0
        Dim scount As Integer = 0

        Dim txt As New ASPxMemo, txtRingDate As New ASPxDateEdit
        Dim str As String = "", i As Integer = 0
        Dim IsRowSelected As Boolean

        For tcount = 0 To grdMain.VisibleRowCount - 1
            i = Generic.ToInt(grdMain.GetRowValues(tcount, New String() {"BenefitLoyaltyNo"}))
            txt = CType(grdMain.FindRowCellTemplateControl(tcount, grdMain.DataColumns("Remarks"), "txtRemarks"), ASPxMemo)
            txtRingDate = CType(grdMain.FindRowCellTemplateControl(tcount, grdMain.DataColumns("RingIssuedDate"), "txtRingIssuedDate"), ASPxDateEdit)
            IsRowSelected = grdMain.Selection.IsRowSelected(tcount)
            If IsRowSelected = True Then
                scount = scount + 1
                fcount = fcount + SQLHelper.ExecuteNonQuery("EBenefitLoyalty_WebRingRelease", UserNo, Generic.ToInt(i), Generic.ToStr(txt.Text), Generic.ToStr(txtRingDate.Text))
            End If
        Next

        If fcount > 0 Then
            MessageBox.Success("There are (" + fcount.ToString + ") transaction(s) tagged as Gold Ring released.", Me)
        Else
            If scount = 0 Then
                MessageBox.Warning("There are no transaction(s) tagged as Gold Ring released. Please select transaction(s).", Me)
            ElseIf scount > 0 Then
                MessageBox.Warning("Some selected transaction(s) cannot be tagged as Gold Ring released. Transaction(s) might not be entitled for Gold Ring.", Me)
            End If
        End If

    End Sub

#End Region

#Region "Details"

    'Private Sub PopulateDetl()
    '    Try
    '        Dim dt As DataTable
    '        dt = SQLHelper.ExecuteDataTable("EleaveApplicationDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
    '        grdDetl.DataSource = dt
    '        grdDetl.DataBind()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
    '    Dim count As Integer = 0
    '    For i = 0 To grdDetl.VisibleRowCount - 1
    '        Dim txt As New TextBox
    '        Dim hif As New HiddenField
    '        hif = grdDetl.FindRowCellTemplateControl(i, grdDetl.Columns(3), "hifLeaveApplicationDetiNo")
    '        txt = grdDetl.FindRowCellTemplateControl(i, grdDetl.Columns(3), "txtPaidHrs")
    '        count = count + Generic.ToInt(SQLHelper.ExecuteNonQuery("ELeaveApplicationDeti_WebSave", UserNo, Generic.ToInt(hif.Value), Generic.ToDec(txt.Text)))
    '    Next
    '    If count > 0 Then
    '        MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '    End If
    'End Sub

    'AWOL DETAILS:

    Private Sub PopulateDetlAWOL()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBenefitLoyalty_AWOL_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetlAWOL.DataSource = dt
            grdDetlAWOL.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDetlRating()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBenefitLoyalty_RATING_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetlRating.DataSource = dt
            grdDetlRating.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDetlADU()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBenefitLoyalty_ADU_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetlADU.DataSource = dt
            grdDetlADU.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkDetailsAWOL_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateDetlAWOL()
    End Sub

    Protected Sub lnkDetailsRating_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateDetlRating()
    End Sub

    Protected Sub lnkDetailsADU_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateDetlADU()
    End Sub

    Protected Sub lnkAddAWOL_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Generic.ClearControls(Me, "pnlPopupDetlAWOLAdd")
        Me.mdlDetlAWOLAdd.Show()
    End Sub

    Protected Sub lnkSaveAWOL_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim BenefitLoyaltyAwolNo As Integer = Generic.ToInt(txtBenefitLoyaltyAwolNo.Text)
        Dim BenefitLoyaltyNo As Integer = Generic.ToInt(ViewState("TransNo")) ' Generic.ToInt(txtBenefitLoyaltyNo.Text)
        'Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim DTRDate As String = Generic.ToStr(txtDTRDate.Text)
        Dim TotalDays As Double = Generic.ToDec(txtTotalDays.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        'dtx = SQLHelper.ExecuteDataTable("EBenefitLoyaltyAwol_WebValidate", UserNo, BenefitLoyaltyAwolNo, DTRDate, TotalDays, PayLocNo, ComponentNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        'If invalid = True Then
        '    MessageBox.Alert(messagedialog, alerttype, Me)
        '    mdlDetlAWOLAdd.Show()
        '    Exit Sub
        'End If

        dt = SQLHelper.ExecuteDataTable("EBenefitLoyaltyAwol_WebSave", UserNo, BenefitLoyaltyAwolNo, BenefitLoyaltyNo, DTRDate, TotalDays, Remarks, PayLocNo)
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
            PopulateDetlAWOL()
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub

    Protected Sub cboFilteredbyNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            Dim fId As Integer
            fId = Generic.ToInt(Generic.CheckDBNull(cboFilteredbyNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType))
            txtName.Text = ""
            If fId > 0 Then
                txtName.Enabled = True
                drpAC.CompletionSetCount = fId
            Else
                txtName.Enabled = False
                drpAC.CompletionSetCount = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function populateDataDropdown(prefixText As String, count As Integer, contextKey As String) As List(Of String)
        Dim items As New List(Of String)()
        Dim _ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim clsbase As New clsBase.clsBaseLibrary
        Dim UserNo As Integer = 0, PayLocNo As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        PayLocNo = (HttpContext.Current.Session("xPayLocNo"))

        _ds = SQLHelper.ExecuteDataSet("EFilterBy_WebLookup_AC", UserNo, prefixText, PayLocNo, count)
        For Each row As DataRow In _ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("tDesc")),
                                Generic.ToStr(row("tNo")))
            items.Add(item)
        Next
        _ds.Dispose()
        Return items


    End Function

    Protected Sub txtRemarks_Load(ByVal sender As Object, ByVal e As EventArgs)
        Dim c As GridViewDataItemTemplateContainer = TryCast((CType(sender, ASPxMemo)).NamingContainer, GridViewDataItemTemplateContainer)
        CType(sender, ASPxMemo).ClientInstanceName = "Memo" & c.KeyValue.ToString()
        CType(sender, ASPxMemo).ClientSideEvents.TextChanged = "function(s,e){ProcessMemo(" & c.KeyValue.ToString() & ",s.GetText());}"
        Dim hfKey As String = "key" & c.KeyValue.ToString()
        If hfMemo.Contains(hfKey) Then
            Dim pars() As String = Convert.ToString(hfMemo(hfKey)).Split("")
            CType(sender, ASPxMemo).Text = pars(0)
        End If
    End Sub

    Protected Sub txtRingIssuedDate_Load(ByVal sender As Object, ByVal e As EventArgs)
        Dim c As GridViewDataItemTemplateContainer = TryCast((CType(sender, ASPxDateEdit)).NamingContainer, GridViewDataItemTemplateContainer)
        CType(sender, ASPxDateEdit).ClientInstanceName = "RingDate" & c.KeyValue.ToString()
        CType(sender, ASPxDateEdit).ClientSideEvents.ValueChanged = "function(s,e){ProcessRingDate(" & c.KeyValue.ToString() & ",s.GetText());}"
        Dim hfKey As String = "key" & c.KeyValue.ToString()
        If hfRingDate.Contains(hfKey) Then
            Dim pars() As String = Convert.ToString(hfRingDate(hfKey)).Split("")
            CType(sender, ASPxDateEdit).Text = pars(0)
        End If
    End Sub

#End Region


End Class
