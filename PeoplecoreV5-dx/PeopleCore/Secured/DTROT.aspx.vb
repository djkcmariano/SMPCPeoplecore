Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_DTROTList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Dim tStatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
        If tStatus = 1 Or tStatus = 2 Then
            lnkCancel.Visible = True
        Else
            lnkCancel.Visible = False
        End If
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTROT_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo, FilterSearch1.SearchText, FilterSearch1.SelectTop.ToString, FilterSearch1.FilterParam.ToString)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try

        'If Generic.ToInt(cboOvertimeReasonNo.SelectedValue) > 0 Then
        '    txtReason.Text = ""
        'End If

    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTROT_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
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
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 52)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Function OT_Msg(ByRef Msg As String, ByVal Index As Short) As String
        Dim xds As New DataSet
        xds = SQLHelper.ExecuteDataSet("[EDTROT_WebOne_Msg]", Index)
        If xds.Tables.Count > 0 Then
            Msg = Generic.ToStr(xds.Tables(0).Rows(0)("xMsg"))
        End If
        Return Msg
    End Function

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.EnableControls(Me, "pnlPopupDetl", True)
            txtReason.Text = ""
            Try
                cboCostCenterNo.DataSource = SQLHelper.ExecuteDataSet("EDTRShift_WebLookup_ChargeTo", UserNo, PayLocNo)
                cboCostCenterNo.DataTextField = "tDesc"
                cboCostCenterNo.DataValueField = "tno"
                cboCostCenterNo.DataBind()
            Catch ex As Exception

            End Try

            Try
                cboOvertimeReasonNo.DataSource = SQLHelper.ExecuteDataSet("EOT_WebLookup", UserNo, PayLocNo)
                cboOvertimeReasonNo.DataTextField = "tDesc"
                cboOvertimeReasonNo.DataValueField = "tno"
                cboOvertimeReasonNo.DataBind()
            Catch ex As Exception

            End Try

            cboApprovalStatNo.Text = 2
            cboApprovalStatNo.Enabled = False
            lnkSave.Enabled = True

            Dim Msg As String = ""
            Me.lblMsgNotice.Text = OT_Msg(Msg, 1)

            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTROTNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Dim OTReasonNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"OvertimeReasonNo"}))
            'Generic.EnableControls(Me, "pnlPopupDetl", IsEnabled)
            Try
                cboCostCenterNo.DataSource = SQLHelper.ExecuteDataSet("EDTRShift_WebLookup_ChargeTo", UserNo, PayLocNo)
                cboCostCenterNo.DataTextField = "tDesc"
                cboCostCenterNo.DataValueField = "tno"
                cboCostCenterNo.DataBind()
            Catch ex As Exception

            End Try

            Try
                cboOvertimeReasonNo.DataSource = SQLHelper.ExecuteDataSet("EOT_WebLookup", UserNo, PayLocNo)
                cboOvertimeReasonNo.DataTextField = "tDesc"
                cboOvertimeReasonNo.DataValueField = "tno"
                cboOvertimeReasonNo.DataBind()
            Catch ex As Exception

            End Try

            If OTReasonNo > 0 Then
                txtReason.Enabled = True
            End If

            cboApprovalStatNo.Enabled = IsEnabled
            'lnkSave.Enabled = IsEnabled

            Dim Msg As String = ""
            Me.lblMsgNotice.Text = OT_Msg(Msg, 1)

            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTROTNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTROT", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)

        Dim dt As DataTable, tProceed As Boolean = False
        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "DTROTNo"))
                dt = SQLHelper.ExecuteDataTable("ETableApplication_WebCancel", UserNo, "EDTROT", item, PayLocNo)
                For Each row As DataRow In dt.Rows
                    tProceed = Generic.ToBol(row("tProceed"))
                Next
                grdMain.Selection.UnselectRow(j)
                i = i + 1
            End If
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") transaction(s) successfully cancelled.", Me)
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If


    End Sub
    Protected Sub cboOvertimeReasonNo_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateRefresh()
        mdlDetl.Show()
    End Sub


    Protected Sub PopulateRefresh()

        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EOvertimeReason_WebOne", UserNo, cboOvertimeReasonNo.SelectedValue)
            If Generic.ToInt(cboOvertimeReasonNo.SelectedValue) = 0 Then
                txtReason.Enabled = True
                txtReason.CssClass = "form-control required"
                txtReason.Text = ""
                cboOvertimeReasonNo.CssClass = "form-control"
            ElseIf Generic.ToInt(cboOvertimeReasonNo.SelectedValue) > 0 Then
                txtReason.Enabled = True
                txtReason.CssClass = "form-control"
                txtReason.Text = Generic.ToStr(dt.Rows(0)("OvertimeReasonDesc"))
                cboOvertimeReasonNo.CssClass = "form-control required"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        'Dim d As New DataTable
        'd = SQLHelper.ExecuteDataTable("EOvertimeReason_WebOne", UserNo, cboOvertimeReasonNo.SelectedValue)
        'For Each row As DataRow In d.Rows
        '    If Generic.ToInt(cboOvertimeReasonNo.SelectedValue) > 0 Then
        '        txtReason.Text = Generic.ToStr(row("OvertimeReasonDesc"))
        '    End If
        'Next

        Dim RetVal As Boolean = False
        Dim DTROTNo As Integer = Generic.ToInt(txtDTROTNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim DTRDate As String = Generic.ToStr(txtDTRDate.Text)
        Dim OvtIn1 As String = Generic.ToStr(Replace(txtOvtIn1.Text, ":", ""))
        Dim OvtOut1 As String = Generic.ToStr(Replace(txtOvtOut1.Text, ":", ""))
        Dim OvtIn2 As String = Generic.ToStr(Replace(txtOvtIn2.Text, ":", ""))
        Dim OvtOut2 As String = Generic.ToStr(Replace(txtOvtOut2.Text, ":", ""))
        Dim OTBreak As Double = Generic.ToDec(txtOTBreak.Text)
        'Dim IsForCompensatory As Boolean = Generic.ToBol(txtIsForcompensatory.Checked)
        'Dim IsOnCall As Boolean = Generic.ToBol(txtIsOncall.Checked)
        Dim Reason As String = Generic.ToStr(txtReason.Text)
        Dim CostCenterNo As Integer = Generic.ToInt(cboCostCenterNo.SelectedValue)
        Dim ApprovalStatNo As Integer = Generic.ToInt(cboApprovalStatNo.SelectedValue)
        Dim ComponentNo As Integer = 1 'Administrator

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EDTROT_WebValidate", UserNo, DTROTNo, EmployeeNo, DTRDate, OvtIn1, OvtOut1, OvtIn2, OvtOut2, OTBreak, Reason, CostCenterNo, ApprovalStatNo, PayLocNo, ComponentNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlDetl.Show()
            Exit Sub
        End If

        dt = SQLHelper.ExecuteDataTable("EDTROT_WebSave", UserNo, DTROTNo, EmployeeNo, DTRDate, OvtIn1, OvtOut1, OvtIn2, OvtOut2, OTBreak, Reason, CostCenterNo, ApprovalStatNo, PayLocNo, cboOvertimeReasonNo.SelectedValue)
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
            Dim url As String = "DTROT.aspx"
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            PopulateGrid()
        End If

    End Sub


    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    ' Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
    ' If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
    'Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
    '      e.Enabled = value
    '  End If
    'End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkAddMass_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/DTROTMassList.aspx?transNo=" & 0 & "&tModify=True")
        Else
            MessageBox.Critical(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

#Region "********Detail Check All********"

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

    Protected Sub ASPxGridViewExporter_RenderBrick(sender As Object, e As DevExpress.Web.ASPxGridViewExportRenderingEventArgs) Handles grdExport.RenderBrick
        Dim dataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
        'If e.RowType = GridViewRowType.Data AndAlso dataColumn IsNot Nothing Then
        '    Select Case dataColumn.FieldName
        '        Case "AbsHrs"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<span>", "")
        '            e.TextValue = e.TextValue.ToString.Replace("</span>", "")
        '        Case "Late"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<span>", "")
        '            e.TextValue = e.TextValue.ToString.Replace("</span>", "")
        '        Case "Under"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<span>", "")
        '            e.TextValue = e.TextValue.ToString.Replace("</span>", "")
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





