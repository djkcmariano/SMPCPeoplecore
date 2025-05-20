Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_DTRShiftList
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
            dt = SQLHelper.ExecuteDataTable("EDTRShift_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo, FilterSearch1.SearchText, FilterSearch1.SelectTop.ToString, FilterSearch1.FilterParam.ToString)
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
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.EnableControls(Me, "pnlPopupDetl", True)
            disableshift_perday()
            Try
                cboCostCenterNo.DataSource = SQLHelper.ExecuteDataSet("EDTRShift_WebLookup_ChargeTo", UserNo, PayLocNo)
                cboCostCenterNo.DataTextField = "tDesc"
                cboCostCenterNo.DataValueField = "tno"
                cboCostCenterNo.DataBind()
            Catch ex As Exception

            End Try
            cboApprovalStatNo.Text = 2
            cboApprovalStatNo.Enabled = False
            lnkSave.Enabled = True
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
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRShiftNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            'Generic.EnableControls(Me, "pnlPopupDetl", IsEnabled)
            disableshift_perday()
            cboApprovalStatNo.Enabled = IsEnabled
            Try
                cboCostCenterNo.DataSource = SQLHelper.ExecuteDataSet("EDTRShift_WebLookup_ChargeTo", UserNo, PayLocNo)
                cboCostCenterNo.DataTextField = "tDesc"
                cboCostCenterNo.DataValueField = "tno"
                cboCostCenterNo.DataBind()
            Catch ex As Exception

            End Try
            'lnkSave.Enabled = IsEnabled
            'If txtIsCrew.Checked Then
            '    fRegisterStartupScript("JSDialogResponse", "ViewCrew('True');")
            'Else
            '    fRegisterStartupScript("JSDialogResponse", "ViewCrew('False');")
            'End If

            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)

        Dim dt As DataTable, tProceed As Boolean = False
        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "DTRShiftNo"))
                dt = SQLHelper.ExecuteDataTable("ETableApplication_WebCancel", UserNo, "EDTRShift", item, PayLocNo)
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

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRShiftNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EDTRShiftDeti", UserNo, "DTRShiftNo", item)
                Generic.DeleteRecordAudit("EDTRShift", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    ' Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
    '    If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
    'Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
    '       e.Enabled = value
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
            Response.Redirect("~/secured/DTRShiftMassList.aspx?transNo=" & 0 & "&tModify=True")
        Else
            MessageBox.Critical(MessageTemplate.DeniedAdd, Me)
        End If
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
        Dim ComponentNo As Integer = 1 'Administrator
        Dim In1 As String = Replace(Generic.ToStr(txtIn1.Text), ":", "")
        Dim Out1 As String = Replace(Generic.ToStr(txtOut1.Text), ":", "")
        Dim costCenterNo As Integer = Generic.ToInt(cboCostCenterNo.SelectedValue)
        In1 = IIf(In1 Is Nothing, "", In1)
        Out1 = IIf(Out1 Is Nothing, "", Out1)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
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

        dt = SQLHelper.ExecuteDataTable("EDTRShift_WebSave", UserNo, DTRShiftNo, EmployeeNo, DateFrom, DateTo, ShiftNo, ShiftNoMon, ShiftNoTue, ShiftNoWed, ShiftNoThu, ShiftNoFri, ShiftNoSat, ShiftNoSun, Reason, ApprovalStatNo, PayLocNo, In1, Out1, costCenterNo)
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
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
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

#Region "Upload"
    Private Function PoplulateCSVFile_Upload() As Integer
        Dim tsuccess As Integer = 0
        Try


            Dim lastname As String = ""
            Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False
            Dim tpath As String = ""
            Dim datenow As Date
            Dim Description As String = ""

            datenow = Now()

            Description = Generic.ToStr(txtDescription2.Text)

            Dim filext As String = Pad.PadZero(2, Month(datenow)) & Pad.PadZero(2, Day(datenow)) & Pad.PadZero(4, Year(datenow)) & Pad.PadZero(2, Hour(datenow)) & Pad.PadZero(2, Minute(datenow)) & Pad.PadZero(4, Second(datenow))
            If fuFilename.HasFile = True Then
                tFilepath = fuFilename.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                Dim fileext As String = IO.Path.GetExtension(tFilepath)
                tProceed = True
                tpath = (Server.MapPath("documents")) 'Me.MapPath("documents") & "\
                If Not IO.Directory.Exists(tpath) Then
                    IO.Directory.CreateDirectory(tpath)
                End If
                fuFilename.SaveAs(tpath & "\" & tfilename & "_" & filext)

            End If

            'SQLHelper.ExecuteNonQuery("EPayPrevious_WebUpload_Delete", UserNo, txtBatchNumber.Text.ToString)

            Dim amount As Double = 0

            If tProceed Then
                Dim employeecode As String = "", shiftcode As String = "", startdate As String = "", enddate As String = ""

                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0
                Dim fs As FileStream, fFilename As String
                fFilename = tpath & "\" & tfilename & "_" & filext 'tpath & "\" & tfilename
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                Dim rDesc As String = ""
                Dim rAmount As String = ""
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1


                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, ",")

                    If i > 0 Then
                        employeecode = fspecArr(0)
                        shiftcode = fspecArr(1)
                        startdate = fspecArr(2)
                        enddate = fspecArr(3)

                    End If

                    If i > 0 Then
                        If employeecode.ToString > "" Then
                            SQLHelper.ExecuteDataSet("EDTRShift_WebUpload", UserNo, PayLocNo, employeecode, shiftcode, startdate, enddate, Description)
                            tsuccess = tsuccess + 1
                        End If
                    End If

                    i = i + 1
                End While
                d.Close()
                Return 1
            End If
            Return True
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Protected Sub lnkUpload_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            'Generic.ClearControls(Me, "Panel3")
            'ModalPopupExtender2.Show()
            Response.Redirect("~/secured/DTRShift_Upload.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave2_Click(sender As Object, e As EventArgs)
        Dim retVal As Integer = PoplulateCSVFile_Upload()
        If retVal = 1 Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        ElseIf retVal = 2 Then
            MessageBox.Alert("The file must have a header.", "warning", Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub
#End Region

End Class






