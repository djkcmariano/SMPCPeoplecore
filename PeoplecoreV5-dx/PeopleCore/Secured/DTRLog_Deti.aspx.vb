Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO
Partial Class Secured_DTRLog_Deti
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim DTRLogNo As Integer = 0
    Dim DTRLogDtlNo As Integer = 0
    Private Sub PopulateData()
        Try
            lblEmployeeCode.Text = Generic.ToStr(Request.QueryString("EmployeeCode"))
            lblFullName.Text = Generic.ToStr(Request.QueryString("FullName"))
            lblLeaveType.Text = Generic.ToStr(Request.QueryString("DTRDate"))

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        DTRLogNo = Generic.ToInt(Request.QueryString("TransNo"))
        DTRLogDtlNo = Generic.ToInt(Request.QueryString("DTRLogDtlNo"))
        AccessRights.CheckUser(UserNo, "DTRLog.aspx", "EDTRLog")

        PopulateGrid()
        PopulateData()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
    End Sub



    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EDTRLogDetl_Web", UserNo, DTRLogNo)

        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

    End Sub


    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkBack_Click(sender As Object, e As EventArgs)
        Response.Redirect("DTRLog.aspx")
    End Sub
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "DTRLog.aspx", "EDTRLog") Then
            Generic.ClearControls(Me, "pnlPopup")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)

            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "DTR.aspx", "EDTR") Then

                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRLogDetlNo"}))

                Generic.ClearControls(Me, "pnlPopup")
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EDTRLogDetl_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
                    Generic.PopulateData(Me, "pnlPopup", dt)
                Next

                Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
                Generic.EnableControls(Me, "pnlPopup", IsEnabled)
                btnSave.Enabled = IsEnabled

                mdlShow.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "DTRLog.aspx", "EDTRLog") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRLogDetlNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTRLogDetl", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim ProjectNo As Integer = Generic.ToInt(cboProjectNo.SelectedValue)

        Dim Hrs As Double = Generic.ToDbl(txtHrs.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Html)
        Dim In1 As String = Generic.ToStr(Replace(txtIn11.Text, ":", ""))
        Dim Out1 As String = Generic.ToStr(Replace(txtOut11.Text, ":", ""))
        Dim Task As String = Generic.ToStr(txtTask.Html)
        ''//validate start here
        'Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        'Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        'dtx = SQLHelper.ExecuteDataTable("EDTRLogDetl_WebValidate", UserNo, Generic.ToInt(txtDTRLogDetlNo.Text), DTRLogNo, DTRNo, DepartmentNo, CostCenterNo, Hrs, Remarks, PayLocNo)
        'For Each rowx As DataRow In dtx.Rows
        '    invalid = Generic.ToBol(rowx("tProceed"))
        '    messagedialog = Generic.ToStr(rowx("xMessage"))
        '    alerttype = Generic.ToStr(rowx("AlertType"))
        'Next

        'If invalid = True Then
        '    MessageBox.Alert(messagedialog, alerttype, Me)
        '    mdlShow.Show()
        '    Exit Sub
        'End If
        Dim dt As DataTable, error_num As Integer = 0, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("EDTRLogDetl_WebSave", UserNo, Generic.ToInt(txtDTRLogDetlNo.Text), DTRLogNo, ProjectNo, Hrs, Remarks, In1, Out1, Task)
        For Each row As DataRow In dt.Rows
            Retval = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                Retval = False
            End If

        Next
        If Retval = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If Retval = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub


    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub

#Region "********Check All********"


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

End Class

