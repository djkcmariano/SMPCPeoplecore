Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_BENLeaveBalanceList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim PaidHrs As Double = 0
    Dim PaidDays As Double = 0
    Dim CancelHrs As Double = 0
    Dim CancelCount As Integer = 0

    'Private Sub PopulateGrid(Optional IsMain As Boolean = False)
    '    Dim _dt As DataTable
    '    _dt = SQLHelper.ExecuteDataTable("ELeaveBalance_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue), Generic.ToStr(txtDate.Text))
    '    Me.grdMain.DataSource = _dt
    '    Me.grdMain.DataBind()

    '    If ViewState("TransNo") = 0 Or IsMain = True Then
    '        Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"EmployeeNo", "FullName", "LeaveTypeNo"})
    '        ViewState("TransNo") = obj(0)
    '        ViewState("Name") = obj(1)
    '        ViewState("LeaveType") = obj(2)
    '    End If

    '    lblDetl.Text = ViewState("Name")

    '    PopulateCutOff(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("LeaveType")), Generic.ToStr(txtDate.Text))
    '    PopulateGridDetl(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("LeaveType")), Generic.ToStr(txtDate.Text))
    '    PopulateGridDetl1(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("LeaveType")), Generic.ToStr(txtDate.Text))

    'End Sub


    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ELeaveBalance_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue), Generic.ToStr(txtDate.Text), FilterSearch1.SearchText, FilterSearch1.SelectTop.ToString, FilterSearch1.FilterParam.ToString)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try

        'If Generic.ToInt(cboOvertimeReasonNo.SelectedValue) > 0 Then
        '    txtReason.Text = ""
        'End If

    End Sub

    Private Sub PopulateGridDetl(id As Integer, type As Integer, xdate As String)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ELeaveCredit_Web", UserNo, id, type, xdate)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Private Sub PopulateGridDetl1(id As Integer, type As Integer, xdate As String)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ELeaveAvailed_Web", UserNo, id, type, xdate)
        grdDetl1.DataSource = dt
        grdDetl1.DataBind()
    End Sub

    Private Sub PopulateCutOff(id As Integer, type As Integer, xdate As String)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ELeaveCutOff_Web", UserNo, id, type, xdate)
        For Each row As DataRow In dt.Rows
            lblDate.Text = Generic.ToStr(row("CoverDate"))
        Next
    End Sub

    Private Sub populateCombo()

        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 6)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
            populateCombo()
            'txtDate.Text = Now.Date

        End If

        PopulateGrid()
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("LeaveType")), Generic.ToStr(txtDate.Text))
        PopulateGridDetl1(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("LeaveType")), Generic.ToStr(txtDate.Text))


        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)
        Generic.PopulateDXGridFilter(grdDetl1, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub
    Protected Sub lnkAsOff_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo", "FullName", "LeaveTypeNo"})
        ViewState("TransNo") = obj(0)
        ViewState("Name") = obj(1)
        ViewState("LeaveType") = obj(2)

        lblDetl.Text = ViewState("Name")
        'lblDetl1.Text = "Employee Name : " & ViewState("Name")

        PopulateCutOff(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("LeaveType")), Generic.ToStr(txtDate.Text))
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("LeaveType")), Generic.ToStr(txtDate.Text))
        PopulateGridDetl1(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("LeaveType")), Generic.ToStr(txtDate.Text))
    End Sub

    Protected Sub lnkUpload_Click(ByVal sender As Object, ByVal e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, Session("xFormName"), Session("xTableName")) Then
            'Generic.ClearControls(Me, "Panel3")
            'ModalPopupExtender6.Show()
            Response.Redirect("~/secured/BenLeaveBalance_Upload.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            'hifEmployeeNo.Value = Generic.ToInt(ViewState("TransNo"))
            'txtFullName.Text = Generic.ToStr(ViewState("Name"))
            'cboLeaveTypeno.Text = IIf(Generic.ToInt(ViewState("LeaveType")) = 0, "", Generic.ToInt(ViewState("LeaveType")))

            Try
                cboLeaveTypeno.DataSource = SQLHelper.ExecuteDataSet("ELeaveType_WebLookup_Union", UserNo, 0, PayLocNo)
                cboLeaveTypeno.DataValueField = "tNo"
                cboLeaveTypeno.DataTextField = "tDesc"
                cboLeaveTypeno.DataBind()

            Catch ex As Exception
            End Try

            PopulateRefresh()
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    'Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
    '            Dim lnk As New LinkButton, i As Integer
    '            lnk = sender
    '            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
    '            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveCreditNo"}))

    '            Dim dt As DataTable
    '            dt = SQLHelper.ExecuteDataTable("ELeaveCredit_WebOne", UserNo, Generic.ToInt(i))
    '            For Each row As DataRow In dt.Rows

    '           Try
    '            cboLeaveTypeno.DataSource = SQLHelper.ExecuteDataSet("ELeaveType_WebLookup_Union", UserNo, row("LeaveTypeNo"), PayLocNo)
    '            cboLeaveTypeno.DataValueField = "tNo"
    '            cboLeaveTypeno.DataTextField = "tDesc"
    '            cboLeaveTypeno.DataBind()

    '           Catch ex As Exception
    '           End Try

    '                Generic.PopulateData(Me, "pnlPopupMain", dt)
    '            Next
    '            mdlMain.Show()


    '        Else
    '            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"LeaveCreditNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ELeaveCredit", UserNo, item)
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


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim LeaveCreditNo As Integer = Generic.ToInt(txtLeaveCreditNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))
        Dim LeaveTypeNo As Integer = Generic.ToInt(cboLeaveTypeno.SelectedValue)
        Dim LeaveHrs As Double = Generic.ToDec(txtLeaveHRs.Text)
        Dim AcquireDate As String = Generic.ToStr(txtAcquireDate.Text)
        Dim DateForefeited As String = Generic.ToStr(txtDateForefeited.Text)
        Dim Remark As String = Generic.ToStr(txtRemark.Text)
        Dim IsConsume As Boolean = Generic.ToBol(txtIsConsume.Checked)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("ELeaveCredit_WebValidate", UserNo, EmployeeNo, LeaveTypeNo, LeaveHrs, AcquireDate, DateForefeited, PayLocNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlMain.Show()
            Exit Sub
        End If

        If SaveRecordDetl() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Private Function SaveRecordDetl() As Boolean
        Dim LeaveCreditNo As Integer = Generic.ToInt(txtLeaveCreditNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))
        Dim LeaveTypeNo As Integer = Generic.ToInt(cboLeaveTypeno.SelectedValue)
        Dim LeaveHrs As Double = Generic.ToDec(txtLeaveHRs.Text)
        Dim AcquireDate As String = Generic.ToStr(txtAcquireDate.Text)
        Dim DateForefeited As String = Generic.ToStr(txtDateForefeited.Text)
        Dim Remark As String = Generic.ToStr(txtRemark.Text)
        Dim IsConsume As Boolean = Generic.ToBol(txtIsConsume.Checked)

        If SQLHelper.ExecuteNonQuery("ELeaveCredit_WebSave", UserNo, LeaveCreditNo, EmployeeNo, LeaveTypeNo, LeaveHrs, AcquireDate, DateForefeited, Remark, IsConsume) > 0 Then
            SaveRecordDetl = True
        Else
            SaveRecordDetl = False
        End If


    End Function

    Protected Sub lnkExportD_Click(sender As Object, e As EventArgs)
        Try
            grdExportD.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
            grdExportUsed.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkExportUsed_Click(sender As Object, e As EventArgs)
        Try
            grdExportUsed.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub grdDetl1_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles grdDetl1.CustomSummaryCalculate
        ' Initialization.
        Dim currRow As Integer = e.RowHandle
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
            If e.Item.FieldName = "PaidHrs" Then
                PaidHrs = 0
                CancelHrs = 0
            End If

            If e.Item.FieldName = "PaidDays" Then
                PaidDays = 0
            End If

            If e.Item.FieldName = "tStatus" Then
                CancelCount = 0
            End If
        End If

        ' Calculation.
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then

            If Generic.ToInt(e.GetValue("ApprovalStatNo")) = 1 Then
                PaidHrs += Convert.ToDouble(Generic.ToDec(grdDetl1.GetRowValues(currRow, "PaidHrs")))
                'PaidDays += Convert.ToDouble(Generic.ToDec(grdDetl1.GetRowValues(currRow, "PaidDays")))
            End If

            If Generic.ToInt(e.GetValue("ApprovalStatNo")) = 2 And Generic.ToBol(e.GetValue("IsCancel")) = False Then
                PaidHrs += Convert.ToDouble(Generic.ToDec(grdDetl1.GetRowValues(currRow, "PaidHrs")))
                'PaidDays += Convert.ToDouble(Generic.ToDec(grdDetl1.GetRowValues(currRow, "PaidDays")))
            End If

            If Generic.ToInt(e.GetValue("ApprovalStatNo")) = 2 And Generic.ToBol(e.GetValue("IsCancel")) = True And Generic.ToBol(e.GetValue("IsFreeze")) = True Then
                PaidHrs += Convert.ToDouble(Generic.ToDec(grdDetl1.GetRowValues(currRow, "PaidHrs")))
                'PaidDays += Convert.ToDouble(Generic.ToDec(grdDetl1.GetRowValues(currRow, "PaidDays")))
            End If

            If Generic.ToBol(e.GetValue("IsCancel")) = True Then
                CancelCount += 1
                CancelHrs += Convert.ToDouble(Generic.ToDec(grdDetl1.GetRowValues(currRow, "PaidHrs")))

            End If

        End If

        ' Finalization.
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize Then

            If e.Item.FieldName = "PaidHrs" Then
                e.TotalValue = "Paid Hrs=" + PaidHrs.ToString
            End If

            'If e.Item.FieldName = "PaidDays" Then
            '    e.TotalValue = "Paid Days=" + PaidDays.ToString
            'End If

            If e.Item.FieldName = "tStatus" Then
                e.TotalValue = "Cancelled Count=" + CancelCount.ToString
            End If

        End If

    End Sub

    Protected Sub cboLeaveTypeno_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        PopulateRefresh()
        mdlMain.Show()
    End Sub


    Protected Sub PopulateRefresh()

        Dim IsForefeited As Boolean = False, IsForefeitedReadOnly As Boolean = False
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ELeaveType_WebOne", UserNo, Generic.ToInt(cboLeaveTypeno.SelectedValue))
        For Each row As DataRow In dt.Rows
            IsForefeited = Generic.ToBol(row("IsForefeited"))
            IsForefeitedReadOnly = Generic.ToBol(row("IsForefeitedReadOnly"))
        Next

        If IsForefeited = True Then
            txtDateForefeited.Enabled = IsForefeitedReadOnly
            txtDateForefeited.CssClass = "form-control required"
        Else
            txtDateForefeited.Enabled = False
            txtDateForefeited.Text = ""
            txtDateForefeited.CssClass = "form-control"
        End If

    End Sub

    Protected Sub txtAcquireDate_TextChanged(sender As Object, e As System.EventArgs)
        Dim IsForefeited As Boolean = False
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ELeaveType_ForfeitedDate", UserNo, Generic.ToInt(cboLeaveTypeno.SelectedValue), txtAcquireDate.Text.ToString)
        For Each row As DataRow In dt.Rows
            txtDateForefeited.Text = Generic.ToStr(row("DateForefeited"))
        Next
        mdlMain.Show()
    End Sub
End Class


