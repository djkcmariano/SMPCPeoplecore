Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class SecuredManager_SelfDTRShiftListAppr
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRShift_WebManager", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRShift_WebOne", UserNo, id)
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
        Permission.IsAuthenticatedSuperior()
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList_Self(UserNo, Me, "pnlPopupDetl", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 12)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub
    Private Sub disableshift_perday()
        cboShiftNoMon.Enabled = False
        cboShiftNoTue.Enabled = False
        cboShiftNoWed.Enabled = False
        cboShiftNoThu.Enabled = False
        cboShiftNoFri.Enabled = False
        cboShiftNoSat.Enabled = False
        cboShiftNoSun.Enabled = False

    End Sub
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Try
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.EnableControls(Me, "pnlPopupDetl", True)
            disableshift_perday()
            lnkSave.Enabled = True
            Try
                cboCostCenterNo.DataSource = SQLHelper.ExecuteDataSet("EDTRShift_WebLookup_ChargeTo", UserNo, PayLocNo)
                cboCostCenterNo.DataTextField = "tDesc"
                cboCostCenterNo.DataValueField = "tno"
                cboCostCenterNo.DataBind()
            Catch ex As Exception

            End Try
            mdlDetl.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRShiftNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Generic.EnableControls(Me, "pnlPopupDetl", IsEnabled)
            disableshift_perday()
            lnkSave.Enabled = IsEnabled
            Try
                cboCostCenterNo.DataSource = SQLHelper.ExecuteDataSet("EDTRShift_WebLookup_ChargeTo", UserNo, PayLocNo)
                cboCostCenterNo.DataTextField = "tDesc"
                cboCostCenterNo.DataValueField = "tno"
                cboCostCenterNo.DataBind()
            Catch ex As Exception

            End Try
            'If txtIsCrew.Checked Then
            '    fRegisterStartupScript("JSDialogResponse", "ViewCrew('True');")
            'Else
            '    fRegisterStartupScript("JSDialogResponse", "ViewCrew('False');")
            'End If
            mdlDetl.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRShiftNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTRShift", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    ' Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
    '   If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
    ' Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
    '       e.Enabled = value
    '   End If
    '  End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkAddMass_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Response.Redirect("~/SecuredManager/SelfDTRShiftMassListAppr.aspx?transNo=" & 0 & "&tModify=True")
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim RetVal As Boolean = False
        Dim DTRShiftNo As Integer = Generic.ToInt(txtDTRShiftNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))
        Dim ShiftNo As Integer = Generic.ToInt(cboShiftNo.SelectedValue)
        Dim ShiftNoMon As Integer = Generic.ToInt(cboShiftNoMon.SelectedValue)
        Dim ShiftNoTue As Integer = Generic.ToInt(cboShiftNoTue.SelectedValue)
        Dim ShiftNoWed As Integer = Generic.ToInt(cboShiftNoWed.SelectedValue)
        Dim ShiftNoThu As Integer = Generic.ToInt(cboShiftNoThu.SelectedValue)
        Dim ShiftNoFri As Integer = Generic.ToInt(cboShiftNoFri.SelectedValue)
        Dim ShiftNoSat As Integer = Generic.ToInt(cboShiftNoSat.SelectedValue)
        Dim ShiftNoSun As Integer = Generic.ToInt(cboShiftNoSun.SelectedValue)
        Dim DateFrom As String = Generic.ToStr(txtDateFrom.Text)
        Dim DateTo As String = Generic.ToStr(txtDateTo.Text)
        Dim Reason As String = Generic.ToStr(txtReason.Text)
        Dim ApprovalStatNo As Integer = Generic.ToInt(cboApprovalStatNo.SelectedValue)
        Dim ComponentNo As Integer = 2 'Managerial
        Dim In1 As String = Replace(Generic.ToStr(txtIn1.Text), ":", "")
        Dim Out1 As String = Replace(Generic.ToStr(txtOut1.Text), ":", "")
        Dim costCenterNo As Integer = Generic.ToInt(cboCostCenterNo.SelectedValue)

        In1 = IIf(In1 Is Nothing, "", In1)
        Out1 = IIf(Out1 Is Nothing, "", Out1)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EDTRShift_WebValidate", UserNo, DTRShiftNo, EmployeeNo, DateFrom, DateTo, ShiftNo, ApprovalStatNo, PayLocNo, ComponentNo)

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

        If SQLHelper.ExecuteNonQuery("EDTRShift_WebSaveSelf", UserNo, DTRShiftNo, EmployeeNo, DateFrom, DateTo, ShiftNo, ShiftNoMon, ShiftNoTue, ShiftNoWed, ShiftNoThu, ShiftNoFri, ShiftNoSat, ShiftNoSun, Reason, PayLocNo, In1, Out1, costCenterNo) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub cboShiftNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        If Generic.ToInt(cboShiftNo.SelectedValue) > 0 Then
            Dim xShiftNo As Integer = Generic.ToInt(cboShiftNo.SelectedValue)
            disableshift_perday()

            cboShiftNoMon.Text = xShiftNo
            cboShiftNoTue.Text = xShiftNo
            cboShiftNoWed.Text = xShiftNo
            cboShiftNoThu.Text = xShiftNo
            cboShiftNoFri.Text = xShiftNo
            cboShiftNoSat.Text = xShiftNo
            cboShiftNoSun.Text = xShiftNo
        Else
            Try
                cboShiftNoMon.Text = ""
                cboShiftNoTue.Text = ""
                cboShiftNoWed.Text = ""
                cboShiftNoThu.Text = ""
                cboShiftNoFri.Text = ""
                cboShiftNoSat.Text = ""
                cboShiftNoSun.Text = ""
            Catch ex As Exception

            End Try
        End If
        'If txtIsCrew.Checked Then
        '    fRegisterStartupScript("JSDialogResponse", "ViewCrew('True');")
        'Else
        '    fRegisterStartupScript("JSDialogResponse", "ViewCrew('False');")
        'End If
        mdlDetl.Show()
    End Sub

    Protected Sub lnkApproved_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRShiftNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, "", 2)
            i = i + 1
        Next

        If i > 0 Then
            Dim url As String = "SelfDTRShiftListAppr.aspx"
            MessageBox.SuccessResponse("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me, url)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Protected Sub lnkDisApproved_Click(sender As Object, e As System.EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRShiftNo"})
        Dim str As String = "", i As Integer = 0
        Dim Remarks As String = Generic.ToStr(TxtDisApprovalRemarks.Text)
        For Each item As Integer In fieldValues
            ApproveTransaction(item, Remarks, 3)
            i = i + 1
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDisapproved, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Private Sub ApproveTransaction(tId As Integer, remarks As String, approvalStatNo As Integer)
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("EDTRShift_WebApproved", UserNo, tId, approvalStatNo, remarks)
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
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj() As Object = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRShiftNo", "DateFrom", "EmployeeCode", "FullName", "DateTo"})
        ViewState("TransNo") = obj(0).ToString
        ViewState("DateFrom") = obj(1).ToString
        ViewState("EmployeeCode") = obj(2).ToString
        ViewState("FullName") = obj(3).ToString
        ViewState("DateTo") = obj(4).ToString

        lbl.Text = Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"Code"}))
        PopulateDetl()
    End Sub
#Region "Details"
    Protected Sub PopulateData_Detl(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRShiftAlloc_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopup", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkAddD_Click(sender As Object, e As EventArgs)
        If ViewState("TransNo") > 0 Then

            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", Generic.ToInt(Session("xPayLocNo")))
            Generic.ClearControls(Me, "pnlPopup")
            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub
    Protected Sub lnkEditD_Click(sender As Object, e As EventArgs)
        If ViewState("TransNo") > 0 Then

            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", Generic.ToInt(Session("xPayLocNo")))
            Dim obj() As Object = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRShiftNo", "DTRShiftAllocNo"})

            ViewState("DTRShiftAllocNo") = obj(1).ToString
            PopulateData_Detl(Generic.ToInt(ViewState("DTRShiftAllocNo")))
            mdlShow.Show()

            'Response.Redirect("DTRShift_Deti.aspx?TransNo=" & obj(0).ToString & "&FullName=" & obj(3).ToString & "&employeecode=" & obj(2).ToString & "&DTRDate=" & obj(1).ToString)

        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub
    Protected Sub lnkDeleteD_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"DTRShiftAllocNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EDTRShiftAlloc", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateDetl()

    End Sub
    Private Sub PopulateDetl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRShiftAlloc_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim ProjectNo As Integer = Generic.ToInt(cboProjectNo.SelectedValue)

        Dim Hrs As Double = 0
        Dim Remarks As String = Generic.ToStr(txtTask.Html)
        Dim In1 As String = ""
        Dim Out1 As String = ""
        ''//validate start here
        'Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        'Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        'dtx = SQLHelper.ExecuteDataTable("EDTRShiftAlloc_WebValidate", UserNo, Generic.ToInt(txtDTRShiftAllocNo.Text), DTRShiftNo, DTRNo, DepartmentNo, CostCenterNo, Hrs, Remarks, PayLocNo)
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
        dt = SQLHelper.ExecuteDataTable("EDTRShiftAlloc_WebSave", UserNo, Generic.ToInt(txtDTRShiftAllocNo.Text), ViewState("TransNo"), ProjectNo, Hrs, Remarks, In1, Out1)
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
            PopulateDetl()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub
#End Region


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










