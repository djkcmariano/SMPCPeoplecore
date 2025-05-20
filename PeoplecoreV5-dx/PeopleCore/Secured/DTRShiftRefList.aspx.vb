Imports System.Data
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web

Partial Class Secured_DTRShiftRefList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EShift_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Dim tStatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
        If tStatus = 0 Then
            lnkDelete.Visible = False
            lnkArchive.Visible = True
        ElseIf tStatus = 1 Then
            lnkDelete.Visible = True
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        Else
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        End If
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EShift_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
            Dim isaddlate As Boolean = txtIsAddLate.Checked

            PopulateControls()
            'fRegisterStartupScript("Sript", "disableenable_behind('" + isaddlate.ToString + "');")
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

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            'Generic.ClearControls(Me, "pnlPopupDetl")
            'lnkSave.Enabled = True
            'Try
            '    cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
            '    cboPayLocNo.DataTextField = "tdesc"
            '    cboPayLocNo.DataValueField = "tNo"
            '    cboPayLocNo.DataBind()

            'Catch ex As Exception

            'End Try
            'PopulateControls()
            'lnkSave.Enabled = True
            'mdlDetl.Show()

            Response.Redirect("~/secured/DTRShiftRefEdit.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
        '    Dim lnk As New LinkButton
        '    lnk = sender
        '    'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        '    Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        '    Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"ShiftNo", "IsEnabled"})
        '    Dim iNo As Integer = Generic.ToInt(obj(0))
        '    Dim IsEnabled As Boolean = Generic.ToBol(obj(1))
        '    Generic.ClearControls(Me, "pnlPopupDetl")
        '    PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ShiftNo"})))
        '    lnkSave.Enabled = IsEnabled
        '    Try
        '        cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
        '        cboPayLocNo.DataTextField = "tdesc"
        '        cboPayLocNo.DataValueField = "tNo"
        '        cboPayLocNo.DataBind()

        '    Catch ex As Exception

        '    End Try
        '    mdlDetl.Show()
        'Else
        '    MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        'End If

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim URL As String = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ShiftNo"})))
            If URL <> "" Then
                Response.Redirect(URL)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub
    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 14)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkArchive_Click(sender As Object, e As EventArgs)

        Dim dt As DataTable, tProceed As Boolean = False
        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "ShiftNo"))
                dt = SQLHelper.ExecuteDataTable("ETableReferrence_WebArchived", UserNo, "EShiftNo", item, 1, PayLocNo)
                For Each row As DataRow In dt.Rows
                    tProceed = Generic.ToBol(row("tProceed"))
                Next
                grdMain.Selection.UnselectRow(j)
                i = i + 1
            End If
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") transaction(s) successfully archived.", Me)
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If


    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ShiftNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues

                Generic.DeleteRecordAudit("EShift", UserNo, item)

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

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim RetVal As Boolean = False
        Dim ShiftNo As Integer = Generic.ToInt(txtShiftNo.Text)
        Dim ShiftCode As String = Generic.ToStr(txtShiftCode.Text)
        Dim ShiftDesc As String = Generic.ToStr(txtShiftDesc.Text)
        Dim In1 As String = Generic.ToStr(txtIn1.Text)
        Dim Out1 As String = Generic.ToStr(txtOut1.Text)
        Dim In2 As String = Generic.ToStr(txtIn2.Text)
        Dim Out2 As String = Generic.ToStr(txtOut2.Text)
        Dim BreakHrs1 As Decimal = Generic.ToDec(Me.txtBreakHrs1.Text)
        Dim Hrs As Decimal = Generic.ToDec(Me.txtHrs.Text)
        Dim NoOfSwipe As Integer = Generic.ToInt(Me.cboNoOfSwipe.SelectedValue)
        Dim IsFlex As Boolean = Generic.ToBol(Me.txtIsFlex.Checked)
        Dim IsAdjustedFlex As Boolean = Generic.ToBol(Me.txtIsAdjustedFlex.Checked)
        Dim AdjustedHrs As Decimal = Generic.ToDec(Me.txtAdjustedHrs.Text)
        Dim IsNonPunching As Boolean = Generic.ToBol(Me.txtIsNonPunching.Checked)
        Dim IsDailyFlex As Boolean = Generic.ToBol(Me.txtIsDailyFlex.Checked)
        Dim IsCompress As Boolean = Generic.ToBol(Me.txtIsCompress.Checked)
        Dim OTStart As String = Generic.ToStr(txtOTStart.Text)
        Dim OTEnd As String = Generic.ToStr(txtOTEnd.Text)
        Dim OTAdj As Decimal = Generic.ToDec(TxtOTAdj.Text)
        Dim IsGraveyard As Boolean = Generic.ToBol(txtIsGraveyard.Checked)
        Dim IsOTApply As Boolean = Generic.ToBol(txtIsOTApply.Checked)
        Dim BreakIn As String = Generic.ToStr(txtBreakIn.Text)
        Dim BreakOut As String = Generic.ToStr(txtBreakOut.Text)
        Dim IsFlexibreak As Boolean = Generic.ToBol(txtIsFlexiBreak.Checked)
        Dim IsAddLate As Boolean = Generic.ToBol(txtIsAddLate.Checked)
        Dim AddLate As Double = Generic.ToDec(txtAddLate.Text)
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EShift_WebValidate", UserNo, ShiftNo, ShiftCode, ShiftDesc, In1, Out1, In2, Out2, BreakHrs1, Hrs, NoOfSwipe, IsFlex, IsAdjustedFlex, AdjustedHrs, IsNonPunching, IsDailyFlex, IsCompress, OTStart, OTEnd, OTAdj, IsGraveyard, IsOTApply, BreakIn, BreakOut, Generic.ToInt(cboPayLocNo.SelectedValue))

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

        If SQLHelper.ExecuteNonQuery("EShift_WebSave", UserNo, PayLocNo, ShiftNo, ShiftCode, ShiftDesc, In1, Out1, In2, Out2, BreakHrs1, Hrs, NoOfSwipe, IsFlex, IsAdjustedFlex, AdjustedHrs, IsNonPunching, IsDailyFlex, IsCompress, OTStart, OTEnd, OTAdj, IsGraveyard, IsOTApply, BreakIn, BreakOut, IsFlexibreak, IsAddLate, AddLate) > 0 Then
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
    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub cboNoOfSwipe_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateControls()
        mdlDetl.Show()
    End Sub

    Protected Sub txtIsAdjustedFlex_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateControls()
        mdlDetl.Show()
    End Sub

    Protected Sub PopulateControls()
        Try
            If Generic.ToInt(cboNoOfSwipe.SelectedValue) = 4 Then
                txtIn2.Enabled = True
                txtOut2.Enabled = True
                txtBreakIn.Enabled = False
                txtBreakOut.Enabled = False
                txtBreakIn.Text = ""
                txtBreakOut.Text = ""
                txtIsFlexiBreak.Enabled = True
            Else
                txtIn2.Enabled = False
                txtOut2.Enabled = False
                txtIn2.Text = ""
                txtOut2.Text = ""
                txtBreakIn.Enabled = True
                txtBreakOut.Enabled = True
                txtIsFlexiBreak.Enabled = False
            End If

            If txtIsAdjustedFlex.Checked = True Then
                txtAdjustedHrs.Enabled = True
            Else
                txtAdjustedHrs.Enabled = False
                txtAdjustedHrs.Text = ""
            End If

            If txtIsOTApply.Checked = True Then
                txtOTEnd.Enabled = True
            Else
                txtOTEnd.Enabled = False
                txtOTEnd.Text = ""
            End If

            If txtIsAddLate.Checked = True Then
                txtAddLate.Enabled = True
            Else
                txtAddLate.Enabled = False
                txtAddLate.Text = ""
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub

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


