Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class SecuredManager_SelfDTRLeaveApplicationListAppr
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0


#Region "Main"

    Protected Sub PopulateGrid(Optional IsMain As Boolean = False)
        Try

            Dim tstatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
            Dim tstatus2 As Integer = Generic.ToInt(cboTab2No.SelectedValue)

            If tstatus2 = 1 Then
                Me.lnkHSOApproved.Visible = True
                Me.lnkHSODisapproved.Visible = True
                Me.lnkApproved.Visible = False
                Me.lnkDisApproved.Visible = False
            ElseIf tstatus2 = 2 Then
                Me.lnkHSOApproved.Visible = True
                Me.lnkHSODisapproved.Visible = True
                Me.lnkApproved.Visible = False
                Me.lnkDisApproved.Visible = False
            ElseIf tstatus2 = 3 Then
                Me.lnkHSOApproved.Visible = True
                Me.lnkHSODisapproved.Visible = True
                Me.lnkApproved.Visible = False
                Me.lnkDisApproved.Visible = False
            Else
                Me.lnkHSOApproved.Visible = False
                Me.lnkHSODisapproved.Visible = False
                Me.lnkApproved.Visible = True
                Me.lnkDisApproved.Visible = True
            End If

            If tstatus2 > 0 Then
                grdMain.Columns("OrganizationGroupRemarks").Visible = True
                grdMain.Columns("OrgGroupApproveDisApproveDate").Visible = True
            Else
                grdMain.Columns("OrganizationGroupRemarks").Visible = False
                grdMain.Columns("OrgGroupApproveDisApproveDate").Visible = False
            End If

            If tstatus = 2 Then
                lnkCancel.Visible = False
                grdDetl.Columns("Reason").Visible = True
                grdDetl.Columns("Status").Visible = True
                grdDetl.Columns("Select").Visible = False
            ElseIf tstatus = 0 Then
                lnkCancel.Visible = False
                grdDetl.Columns("Reason").Visible = True
                grdDetl.Columns("Status").Visible = True
                grdDetl.Columns("Select").Visible = False
            Else
                lnkCancel.Visible = False
                grdDetl.Columns("Reason").Visible = False
                grdDetl.Columns("Status").Visible = False
                grdDetl.Columns("Select").Visible = False
            End If

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ELeaveApplication_WebManager", UserNo, Generic.ToInt(cboTabNo.SelectedValue), Generic.ToInt(cboTab2No.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()

            If ViewState("TransNo") = 0 Or IsMain = True Then
                Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"LeaveApplicationNo", "Code"})
                ViewState("TransNo") = obj(0)
                lbl.Text = obj(1)
            End If

            PopulateGridDetl()

        Catch ex As Exception

        End Try

        'If Generic.ToInt(cboLeaveReasonNo.SelectedValue) > 0 Then
        '    txtReason.Text = ""
        'End If

    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ELeaveApplication_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopup", dt)
                Try
                    'Me.cboLeavetypeNo.DataSource = SQLHelper.ExecuteDataSet("ELeaveType_WebLookupAppr", UserNo, PayLocNo)
                    Me.cboLeavetypeNo.DataSource = SQLHelper.ExecuteDataSet("ELeaveType_WebLookupAppr_Union", UserNo, Generic.ToInt(row("LeaveTypeNo")), PayLocNo)
                    Me.cboLeavetypeNo.DataTextField = "tdesc"
                    Me.cboLeavetypeNo.DataValueField = "tno"
                    Me.cboLeavetypeNo.DataBind()
                Catch ex As Exception

                End Try
                Me.cboLeavetypeNo.Text = Generic.ToInt(row("LeaveTypeNo"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticatedSuperior()
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList_Self(UserNo, Me, "pnlPopup", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 12)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboTab2No.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 50)
            cboTab2No.DataTextField = "tDesc"
            cboTab2No.DataValueField = "tno"
            cboTab2No.DataBind()
        Catch ex As Exception
        End Try

        Try
            'Me.cboLeavetypeNo.DataSource = SQLHelper.ExecuteDataSet("ELeaveType_WebLookupAppr", UserNo, PayLocNo)
            Me.cboLeavetypeNo.DataSource = SQLHelper.ExecuteDataSet("ELeaveType_WebLookupAppr_Union", UserNo, 0, PayLocNo)
            Me.cboLeavetypeNo.DataTextField = "tdesc"
            Me.cboLeavetypeNo.DataValueField = "tno"
            Me.cboLeavetypeNo.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Try
            Generic.ClearControls(Me, "pnlPopup")
            Generic.EnableControls(Me, "pnlPopup", True)
            txtReason.Text = ""
            Try
                'Me.cboLeavetypeNo.DataSource = SQLHelper.ExecuteDataSet("ELeaveType_WebLookupAppr", UserNo, PayLocNo)
                Me.cboLeavetypeNo.DataSource = SQLHelper.ExecuteDataSet("ELeaveType_WebLookupAppr_Union", UserNo, 0, PayLocNo)
                Me.cboLeavetypeNo.DataTextField = "tdesc"
                Me.cboLeavetypeNo.DataValueField = "tno"
                Me.cboLeavetypeNo.DataBind()
            Catch ex As Exception

            End Try

            Try
                cboLeaveReasonNo.DataSource = SQLHelper.ExecuteDataSet("ELeaveReason_WebLookup", UserNo, PayLocNo)
                cboLeaveReasonNo.DataTextField = "tDesc"
                cboLeaveReasonNo.DataValueField = "tno"
                cboLeaveReasonNo.DataBind()
            Catch ex As Exception

            End Try

            lnkSave.Enabled = True
            mdlShow.Show()
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveApplicationNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Dim LeaveReasonNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveApplicationReasonNo"}))
            Generic.EnableControls(Me, "pnlPopup", IsEnabled)
            lnkSave.Enabled = IsEnabled

            Try
                cboLeaveReasonNo.DataSource = SQLHelper.ExecuteDataSet("ELeaveReason_WebLookup", UserNo, PayLocNo)
                cboLeaveReasonNo.DataTextField = "tDesc"
                cboLeaveReasonNo.DataValueField = "tno"
                cboLeaveReasonNo.DataBind()
            Catch ex As Exception

            End Try

            'If LeaveReasonNo > 0 Then
            '    txtReason.Enabled = False
            'End If

            mdlShow.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"LeaveApplicationNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("ELeaveApplicationDeti", UserNo, "LeaveApplicationNo", item)
                Generic.DeleteRecordAudit("ELeaveApplication", UserNo, item)
                i = i + 1
            Next

            If i > 0 Then
                PopulateGrid(True)
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If

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

    Protected Sub cboLeaveReasonNo_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateRefresh()
        mdlShow.Show()
    End Sub


    Protected Sub PopulateRefresh()

        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ELeaveApplicationReason_WebOne", UserNo, cboLeaveReasonNo.SelectedValue)
            If Generic.ToInt(cboLeaveReasonNo.SelectedValue) = 0 Then
                txtReason.Enabled = True
                txtReason.CssClass = "form-control required"
                txtReason.Text = ""
                cboLeaveReasonNo.CssClass = "form-control"
            ElseIf Generic.ToInt(cboLeaveReasonNo.SelectedValue) > 0 Then
                txtReason.Enabled = True
                txtReason.CssClass = "form-control"
                txtReason.Text = Generic.ToStr(dt.Rows(0)("LeaveApplicationReasonDesc"))
                cboLeaveReasonNo.CssClass = "form-control required"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        'Dim d As New DataTable
        'd = SQLHelper.ExecuteDataTable("ELeaveApplicationReason_WebOne", UserNo, cboLeaveReasonNo.SelectedValue)
        'For Each row As DataRow In d.Rows
        '    If Generic.ToInt(cboLeaveReasonNo.SelectedValue) > 0 Then
        '        txtReason.Text = Generic.ToStr(row("LeaveApplicationReasonDesc"))
        '    End If
        'Next

        Dim RetVal As Boolean = False
        Dim LeaveApplicationNo As Integer = Generic.ToInt(txtLeaveApplicationNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim LeaveTypeNo As Integer = Generic.ToInt(cboLeavetypeNo.SelectedValue)
        Dim StartDate As String = Generic.ToStr(txtStartDate.Text)
        Dim EndDate As String = Generic.ToStr(txtEndDate.Text)
        Dim AppliedHrs As Double = Generic.ToDec(txtAppliedHrs.Text)
        'Dim IsForAM As Boolean = Generic.ToBol(txtISForAM.Checked)
        Dim Reason As String = Generic.ToStr(txtReason.Text)
        Dim ApprovalStatNo As Integer = Generic.ToInt(cboApprovalStatNo.SelectedValue)
        Dim ComponentNo As Integer = 2 'Managerial
        Dim StartTime As String = ""
        Dim EndTime As String = ""

        Dim dt As New DataTable, error_num As Integer = 0, error_message As String = ""

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("ELeaveApplication_WebValidate", UserNo, LeaveApplicationNo, EmployeeNo, LeaveTypeNo, StartDate, EndDate, AppliedHrs, ApprovalStatNo, PayLocNo, ComponentNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlShow.Show()
            Exit Sub
        End If

        dt = SQLHelper.ExecuteDataTable("ELeaveApplication_WebSaveSelf", UserNo, LeaveApplicationNo, EmployeeNo, LeaveTypeNo, StartDate, EndDate, AppliedHrs, Reason, StartTime, EndTime, PayLocNo, cboLeaveReasonNo.SelectedValue)

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
            Dim url As String = "SelfDTRLeaveApplicationListAppr.aspx"
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            PopulateGrid()
        End If

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid(True)
    End Sub


    Protected Sub lnkAddMass_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Response.Redirect("~/SecuredManager/SelfDTRLeaveApplicationMassListAppr.aspx?transNo=" & 0 & "&tModify=True")
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        ViewState("TransNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveApplicationNo"}))
        lbl.Text = Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"Code"}))
        PopulateGridDetl()
    End Sub

    Protected Sub lnkHSOApproved_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"LeaveApplicationNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransactionHSO(item, "", 2)
            i = i + 1
        Next

        If i > 0 Then
            PopulateGrid(True)
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Protected Sub lnkHSODisapproved_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"LeaveApplicationNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransactionHSO(item, "", 3)
            i = i + 1
        Next

        If i > 0 Then
            PopulateGrid(True)
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Protected Sub lnkApproved_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"LeaveApplicationNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, "", 2)
            i = i + 1
        Next

        If i > 0 Then
            PopulateGrid(True)
            Dim url As String = "SelfDTRLeaveApplicationListAppr.aspx"
            MessageBox.SuccessResponse("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me, url)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Protected Sub lnkDisApproved_Click(sender As Object, e As System.EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"LeaveApplicationNo"})
        Dim str As String = "", i As Integer = 0
        Dim Remarks As String = Generic.ToStr(TxtDisApprovalRemarks.Text)
        For Each item As Integer In fieldValues
            ApproveTransaction(item, Remarks, 3)
            i = i + 1
        Next

        If i > 0 Then
            PopulateGrid(True)
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDisapproved, Me)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Private Sub ApproveTransaction(tId As Integer, remarks As String, approvalStatNo As Integer)
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("ELeaveApplication_WebApproved", UserNo, tId, approvalStatNo, remarks)
        If fds.Tables.Count > 0 Then
            If fds.Tables(0).Rows.Count > 0 Then
                Dim IsWithapprover As Boolean
                IsWithapprover = Generic.CheckDBNull(fds.Tables(0).Rows(0)("IsWithApprover"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                If IsWithapprover = True Then
                Else
                    MessageBox.Information("Unable to locate the next approver.", Me)
                End If
            End If
        End If
    End Sub

    Private Sub ApproveTransactionHSO(tId As Integer, remarks As String, approvalStatNo As Integer)
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("ELeaveApplication_WebApproved_ApproverGroup", UserNo, tId, approvalStatNo, remarks)
        If fds.Tables.Count > 0 Then
            If fds.Tables(0).Rows.Count > 0 Then
                Dim IsWithapprover As Boolean
                IsWithapprover = Generic.CheckDBNull(fds.Tables(0).Rows(0)("IsWithApprover"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                If IsWithapprover = True Then
                Else
                    MessageBox.Information("Unable to locate the next approver.", Me)
                End If
            End If
        End If
    End Sub
#End Region

#Region "Details"

    Private Sub PopulateGridDetl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EleaveApplicationDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
        Dim count As Integer = 0
        For i = 0 To grdDetl.VisibleRowCount - 1
            Dim txt As New TextBox
            Dim hif As New HiddenField
            hif = grdDetl.FindRowCellTemplateControl(i, grdDetl.Columns(3), "hifLeaveApplicationDetiNo")
            txt = grdDetl.FindRowCellTemplateControl(i, grdDetl.Columns(3), "txtPaidHrs")
            count = count + Generic.ToInt(SQLHelper.ExecuteNonQuery("ELeaveApplicationDeti_WebSave", UserNo, Generic.ToInt(hif.Value), Generic.ToDec(txt.Text)))
        Next
        If count > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try

            Dim tno As Integer = 0, i As Integer = 0
            For j As Integer = 0 To grdDetl.VisibleRowCount - 1
                If grdDetl.Selection.IsRowSelected(j) Then
                    i = i + 1
                End If
            Next

            If i > 0 Then
                Generic.ClearControls(Me, "pnlPopupCancel")
                mdlCancel.Show()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub



    Protected Sub lnkSaveCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim tno As Integer = 0, i As Integer = 0
        For j As Integer = 0 To grdDetl.VisibleRowCount - 1
            If grdDetl.Selection.IsRowSelected(j) Then
                Dim LeaveApplicationDetiNo As Integer = Generic.ToInt(grdDetl.GetRowValues(j, "LeaveApplicationDetiNo"))
                Dim EmpNo As Integer = Generic.ToInt(grdDetl.GetRowValues(j, "EmployeeNo"))
                Dim CPaid As Double = Generic.ToDec(grdDetl.GetRowValues(j, "PaidHrs"))
                Dim Reason As String = Generic.ToStr(txtCancellationRemark.Text)
                If SQLHelper.ExecuteNonQuery("ELeaveApplicationCancel_WebSaveAppr", UserNo, LeaveApplicationDetiNo, EmpNo, CPaid, Reason) Then
                    i = i + 1
                End If
                grdDetl.Selection.UnselectRow(j)
            End If
        Next

        If i > 0 Then
            PopulateGridDetl()
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessCancel, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

#End Region


#Region "********Detail Check All********"

    'Protected Sub grdDetl_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdDetl.CommandButtonInitialize
    '    If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
    '        Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
    '        e.Enabled = value
    '    End If
    'End Sub

    'Private Function getRowEnabledStatus(ByVal VisibleIndex As Integer) As Boolean
    '    Dim value As Boolean = Generic.ToInt(grdDetl.GetRowValues(VisibleIndex, "IsEnabled"))
    '    If value = True Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function
    'Protected Sub cbCheckAllDetl_Init(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim cb As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)
    '    cb.ClientSideEvents.CheckedChanged = String.Format("cbCheckAll_CheckedChanged")
    '    cb.Checked = False
    '    Dim count As Integer = 0
    '    Dim startIndex As Integer = grdDetl.PageIndex * grdDetl.SettingsPager.PageSize
    '    Dim endIndex As Integer = Math.Min(grdDetl.VisibleRowCount, startIndex + grdDetl.SettingsPager.PageSize)

    '    For i As Integer = startIndex To endIndex - 1
    '        If grdDetl.Selection.IsRowSelected(i) Then
    '            count = count + 1
    '        End If
    '    Next i

    '    If count > 0 Then
    '        cb.Checked = True
    '    End If

    'End Sub
    Protected Sub gridDetl_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        Boolean.TryParse(e.Parameters, False)

        Dim startIndex As Integer = grdDetl.PageIndex * grdDetl.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdDetl.VisibleRowCount, startIndex + grdDetl.SettingsPager.PageSize)
        For i As Integer = startIndex To endIndex - 1
            Dim rowEnabled As Boolean = getRowEnabledStatus(i)
            If rowEnabled AndAlso e.Parameters = "true" Then
                grdDetl.Selection.SelectRow(i)
            Else
                grdDetl.Selection.UnselectRow(i)
            End If
        Next i

    End Sub

#End Region

#Region "********MAIN Check All********"

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs)
        If e.ButtonType <> ColumnCommandButtonType.SelectCheckbox Then
            Return
        End If
        Dim rowEnabled As Boolean = getRowEnabledStatus(e.VisibleIndex)
        e.Enabled = rowEnabled
    End Sub

    Private Function getRowEnabledStatus(ByVal VisibleIndex As Integer) As Boolean
        Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(VisibleIndex, "IsEnabled"))
        If value = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub cbCheckAll_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim cb As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)
        cb.ClientSideEvents.CheckedChanged = String.Format("cbCheckAll_CheckedChanged")
        cb.Checked = False
        Dim count As Integer = 0
        Dim startIndex As Integer = grdMain.PageIndex * grdMain.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdMain.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)

        For i As Integer = startIndex To endIndex - 1
            If grdMain.Selection.IsRowSelected(i) Then
                count = count + 1
            End If
        Next i

        If count > 0 Then
            cb.Checked = True
        End If

    End Sub
    Protected Sub gridMain_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        Boolean.TryParse(e.Parameters, False)

        Dim startIndex As Integer = grdMain.PageIndex * grdMain.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdMain.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)
        For i As Integer = startIndex To endIndex - 1
            Dim rowEnabled As Boolean = getRowEnabledStatus(i)
            If rowEnabled AndAlso e.Parameters = "true" Then
                grdMain.Selection.SelectRow(i)
            Else
                grdMain.Selection.UnselectRow(i)
            End If
        Next i

    End Sub

#End Region


    Protected Sub PopulateHrs_Inclusive()
        Dim ds As DataSet
        Dim Hrs As Double = 0
        Dim IsAutoHrs As Boolean = False
        Dim IsDateRange As Boolean = False
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        ds = SQLHelper.ExecuteDataSet("ELeaveApplication_HrsInclusive_WebOne", UserNo, EmployeeNo, Me.txtStartDate.Text.ToString, Me.txtEndDate.Text.ToString)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Hrs = Generic.ToDec(ds.Tables(0).Rows(0)("Hrs"))
                IsAutoHrs = Generic.ToBol(ds.Tables(0).Rows(0)("IsAutoHrs"))
            End If

        End If

        If ViewState("DefaultHrs") = 0 Then
            Me.txtAppliedHrs.Text = Hrs
        End If
        Me.mdlShow.Show()
    End Sub

    Protected Sub lnkDis_Click(sender As Object, e As EventArgs)
        Try

            Dim tno As Integer = 0, i As Integer = 0
            For j As Integer = 0 To grdMain.VisibleRowCount - 1
                If grdMain.Selection.IsRowSelected(j) Then
                    i = i + 1
                End If
            Next

            If i > 0 Then
                Generic.ClearControls(Me, "pnlPopupDis")
                mdlDisApproval.Show()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

End Class






