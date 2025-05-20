Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_DTRLeaveApplication
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0


#Region "Main"

    Protected Sub PopulateGrid()
        Try

            Dim tstatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)

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
            dt = SQLHelper.ExecuteDataTable("ELeaveApplication_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo, FilterSearch1.SearchText, FilterSearch1.SelectTop.ToString, FilterSearch1.FilterParam.ToString)
            grdMain.DataSource = dt
            grdMain.DataBind()
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
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        PopulateGrid()

        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 12)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopup")
            Generic.EnableControls(Me, "pnlPopup", True)
            cboApprovalStatNo.Text = 2
            cboApprovalStatNo.Enabled = False
            lnkSave.Enabled = True
            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

        Try
            cboLeaveReasonNo.DataSource = SQLHelper.ExecuteDataSet("ELeaveReason_WebLookup", UserNo, PayLocNo)
            cboLeaveReasonNo.DataTextField = "tDesc"
            cboLeaveReasonNo.DataValueField = "tno"
            cboLeaveReasonNo.DataBind()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveApplicationNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Dim LeaveReasonNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveApplicationReasonNo"}))
            'Generic.EnableControls(Me, "pnlPopup", IsEnabled)
            cboApprovalStatNo.Enabled = IsEnabled
            'lnkSave.Enabled = IsEnabled
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
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"LeaveApplicationNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("ELeaveApplicationDeti", UserNo, "LeaveApplicationNo", item)
                Generic.DeleteRecordAudit("ELeaveApplication", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
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


    Private Sub PopulateRefresh()

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
        Dim Reason As String = txtReason.Text ' Generic.ToStr(ViewState("Reason"))
        Dim ApprovalStatNo As Integer = Generic.ToInt(cboApprovalStatNo.SelectedValue)
        Dim ComponentNo As Integer = 1 'Administrator
        Dim StartTime As String = ""
        Dim EndTime As String = ""

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
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

        dt = SQLHelper.ExecuteDataTable("ELeaveApplication_WebSave", UserNo, LeaveApplicationNo, EmployeeNo, LeaveTypeNo, StartDate, EndDate, AppliedHrs, Reason, ApprovalStatNo, StartTime, EndTime, PayLocNo, cboLeaveReasonNo.SelectedValue)
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
            Dim url As String = "DTRLeaveApplication.aspx"
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            PopulateGrid()
        End If

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub



    Protected Sub lnkAddMass_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/DTRLeaveApplicationMassList.aspx?transNo=" & 0 & "&tModify=True")
        Else
            MessageBox.Critical(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        ViewState("TransNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveApplicationNo"}))
        lbl.Text = "Transaction No. : " & Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"Code"}))
        PopulateDetl()
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
                If SQLHelper.ExecuteNonQuery("ELeaveApplicationCancel_WebSave", UserNo, LeaveApplicationDetiNo, EmpNo, CPaid, Reason) Then
                    i = i + 1
                End If
                grdDetl.Selection.UnselectRow(j)
            End If
        Next

        If i > 0 Then
            PopulateDetl()
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessCancel, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

#End Region

#Region "Details"

    Private Sub PopulateDetl()
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

#End Region

#Region "********Detail Check All********"

    Protected Sub grdDetl_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdDetl.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Private Function getRowEnabledStatusDetl(ByVal VisibleIndex As Integer) As Boolean
        Dim value As Boolean = Generic.ToInt(grdDetl.GetRowValues(VisibleIndex, "IsEnabled"))
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
        Dim startIndex As Integer = grdDetl.PageIndex * grdDetl.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdDetl.VisibleRowCount, startIndex + grdDetl.SettingsPager.PageSize)

        For i As Integer = startIndex To endIndex - 1
            If grdDetl.Selection.IsRowSelected(i) Then
                count = count + 1
            End If
        Next i

        If count > 0 Then
            cb.Checked = True
        End If

    End Sub
    Protected Sub gridDetl_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        Boolean.TryParse(e.Parameters, False)

        Dim startIndex As Integer = grdDetl.PageIndex * grdDetl.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdDetl.VisibleRowCount, startIndex + grdDetl.SettingsPager.PageSize)
        For i As Integer = startIndex To endIndex - 1
            Dim rowEnabled As Boolean = getRowEnabledStatusDetl(i)
            If rowEnabled AndAlso e.Parameters = "true" Then
                grdDetl.Selection.SelectRow(i)
            Else
                grdDetl.Selection.UnselectRow(i)
            End If
        Next i

    End Sub

#End Region

#Region "********MAIN Check All********"

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType <> ColumnCommandButtonType.SelectCheckbox Then
            Return
        End If
        Dim rowEnabled As Boolean = getRowEnabledStatusMain(e.VisibleIndex)
        e.Enabled = rowEnabled
    End Sub

    Private Function getRowEnabledStatusMain(ByVal VisibleIndex As Integer) As Boolean
        Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(VisibleIndex, "IsEnabled"))
        If value = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub cbCheckAllMain_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim cb As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)
        cb.ClientSideEvents.CheckedChanged = String.Format("cbCheckAllMain_CheckedChanged")
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
            Dim rowEnabled As Boolean = getRowEnabledStatusMain(i)
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

    Protected Sub ASPxGridViewExporter_RenderBrick(sender As Object, e As DevExpress.Web.ASPxGridViewExportRenderingEventArgs) Handles grdExport.RenderBrick
        Dim dataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
        'If e.RowType = GridViewRowType.Data AndAlso dataColumn IsNot Nothing Then
        '    Select Case dataColumn.FieldName
        '        Case "Filed Hr/s"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<span>", "")
        '            e.TextValue = e.TextValue.ToString.Replace("</span>", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<br/>", "")
        '        Case "PaidHrs"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<span>", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<br/>", "")
        '        Case "ApproveBy"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<span>", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<br/>", "")
        '    End Select

        'End If
        If e.RowType = GridViewRowType.Header AndAlso dataColumn IsNot Nothing Then
            e.Text = e.Text.Replace("<br/>", " ")
            e.Text = e.Text.Replace("<br />", " ")
            e.Text = e.Text.Replace("<br>", " ")
            e.Text = e.Text.Replace("<center>", "")
            e.Text = e.Text.Replace("</center>", "")
        End If

    End Sub

End Class




