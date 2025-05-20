Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_BENLeaveTypeList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub PopulateGrid()

        Try
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("ELeaveType_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))

        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", Generic.ToInt(Session("xPayLocNo")))
            PopulateDropDown()
        End If

        Try
            If Generic.ToInt(Me.cboTabNo.SelectedValue) > 0 Then
                Me.lnkDelete.Visible = False
                Me.lnkArchive.Visible = False
            Else
                lnkDelete.Visible = False
                Me.lnkArchive.Visible = True
            End If
        Catch ex As Exception

        End Try

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"LeaveTypeNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ELeaveType", UserNo, item)
                i = i + 1
            Next

            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkArchive_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"LeaveTypeNo"})
            Dim str As String = "", i As Integer = 0, ii As Integer = 0
            Dim IsRowSelected As Boolean
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("ELeaveType_WebArchive", UserNo, item, 1) > 0 Then
                    i = i + 1
                End If
            Next

            'For tcount = 0 To grdMain.VisibleRowCount - 1
            '    ii = Generic.ToInt(grdMain.GetRowValues(tcount, New String() {"LeaveTypeNo"}))
            '    IsRowSelected = grdMain.Selection.IsRowSelected(tcount)
            '    If IsRowSelected = True Then
            '        If SQLHelper.ExecuteNonQuery("ELeaveType_WebArchive", UserNo, ii, 1) > 0 Then
            '            i = i + 1
            '        End If
            '    End If
            'Next

            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ELeaveType_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
                PopulateRefresh()
            Next
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

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender

            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveTypeNo", "IsEnabled"})
            Dim iNo As Integer = Generic.ToInt(obj(0))
            Dim IsEnabled As Boolean = Generic.ToBol(obj(1))
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveTypeNo"})))

            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try

            'Try
            '    cboForfeitureTypeNo.DataSource = SQLHelper.ExecuteDataSet("EforfeitureType_WebLookup")
            '    cboForfeitureTypeNo.DataTextField = "tDesc"
            '    cboForfeitureTypeNo.DataValueField = "tno"
            '    cboForfeitureTypeNo.DataBind()
            'Catch ex As Exception
            'End Try

            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            'Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try

            btnSave.Enabled = True
            PopulateRefresh()
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim LeaveTypeNo As Integer = Generic.ToInt(txtLeavetypeNo.Text)
        Dim LeaveTypeCode As String = Generic.ToStr(txtLeaveTypeCode.Text)
        Dim LeaveTypeDesc As String = Generic.ToStr(txtLeaveTypeDesc.Text)
        Dim ChargeToLeaveTypeNo As Integer = Generic.ToInt(cboChargeToLeaveTypeNo.SelectedValue)
        Dim ChargeToLeaveTypeNo2 As Integer = Generic.ToInt(cboChargeToLeaveTypeNo2.SelectedValue)
        Dim IsWithPay As Boolean = Generic.ToBol(txtIswithPay.Checked)
        Dim IsMaintainBalance As Boolean = Generic.ToBol(txtIsMaintainBalance.Checked)
        Dim IsOnline As Boolean = Generic.ToBol(chkIsonline.Checked)
        Dim GenderNo As Integer = Generic.ToInt(cboGenderNo.SelectedValue)
        Dim IsApplytoall As Boolean = Generic.ToBol(txtIsApplyToAll.Checked)
        Dim IsArchived As Boolean = Generic.ToBol(chkIsArchived.Checked)
        Dim IsAccumulated As Boolean = Generic.ToBol(txtIsAccumulated.Checked)
        Dim IsForefeited As Boolean = Generic.ToBol(txtIsForefeited.Checked)
        Dim IsRefresh As Boolean = Generic.ToBol(txtIsRefresh.Checked)
        Dim RefreshCutOffNo As Integer = Generic.ToInt(cboRefreshCutOffNo.SelectedValue)
        Dim RefreshCutOffDays As Integer = Generic.ToInt(cboRefreshCutOffDays.SelectedValue)
        Dim ForfeitedPeriodNo As Integer = Generic.ToInt(cboForfeitedPeriodNo.SelectedValue)
        Dim ForfeitedCount As Integer = Generic.ToInt(txtForfeitedCount.Text)
        Dim LeaveCateNo As Integer = Generic.ToInt(cboLeaveCateNo.SelectedValue)
        Dim MaxFiledHrs As Decimal = Generic.ToInt(txtMaxFiledHrs.Text)
        Dim ForfeitureTypeNo As Integer = Generic.ToInt(cboForfeitureTypeNo.SelectedValue)
        Dim SpecificDate As String = Generic.ToStr(txtSpecificDate.Text)
        Dim LeaveDays As Integer = Generic.ToStr(txtLeaveDays.Text)
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("ELeaveType_WebValidate", UserNo, PayLocNo, LeaveTypeNo, LeaveTypeCode, LeaveTypeDesc, ChargeToLeaveTypeNo, ChargeToLeaveTypeNo2, IsWithPay, IsMaintainBalance, IsOnline, GenderNo, IsApplytoall, IsArchived, IsForefeited, IsAccumulated, IsRefresh, RefreshCutOffNo, RefreshCutOffDays, ForfeitedPeriodNo, ForfeitedCount, LeaveCateNo)

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


        If SQLHelper.ExecuteNonQuery("ELeaveType_WebSave", UserNo, PayLocNo, LeaveTypeNo, LeaveTypeCode, LeaveTypeDesc, ChargeToLeaveTypeNo, ChargeToLeaveTypeNo2, IsWithPay, IsMaintainBalance, IsOnline, GenderNo, IsApplytoall, IsArchived, IsForefeited, IsAccumulated, IsRefresh, RefreshCutOffNo, RefreshCutOffDays, ForfeitedPeriodNo, ForfeitedCount, LeaveCateNo, MaxFiledHrs, ForfeitureTypeNo, SpecificDate, LeaveDays) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
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

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 30)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try


        Try
            cboRefreshCutOffNo.DataSource = SQLHelper.ExecuteDataSet("EMonth_WebLookup")
            cboRefreshCutOffNo.DataTextField = "tDesc"
            cboRefreshCutOffNo.DataValueField = "tno"
            cboRefreshCutOffNo.DataBind()
        Catch ex As Exception
        End Try


        Try
            cboRefreshCutOffDays.DataSource = SQLHelper.ExecuteDataSet("EDay_WebLookup")
            cboRefreshCutOffDays.DataTextField = "tDesc"
            cboRefreshCutOffDays.DataValueField = "tno"
            cboRefreshCutOffDays.DataBind()
        Catch ex As Exception
        End Try


        'For index As Integer = 1 To 31
        '    Dim li As New ListItem
        '    li.Value = index
        '    li.Text = index
        '    cboRefreshCutOffDays.Items.Add(li)
        'Next

    End Sub
    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub cboForfeitureTypeNo_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateRefresh()
        mdlDetl.Show()
    End Sub

    Protected Sub PopulateRefresh()

        If txtIsMaintainBalance.Checked = True Then
            txtIsAccumulated.Enabled = True
            txtIsForefeited.Enabled = True
            txtIsRefresh.Enabled = True

            If Generic.ToBol(txtIsAccumulated.Checked) = False And Generic.ToBol(txtIsForefeited.Checked) = False And Generic.ToBol(txtIsRefresh.Checked) = False Then
                txtIsRefresh.Checked = True
                cboRefreshCutOffNo.Text = "1"
                cboRefreshCutOffDays.Text = "1"
            End If
        Else
            txtIsAccumulated.Enabled = False
            txtIsForefeited.Enabled = False
            txtIsRefresh.Enabled = False
            txtIsAccumulated.Checked = False
            txtIsForefeited.Checked = False
            txtIsRefresh.Checked = False
        End If

        If txtIsForefeited.Checked = True Then
            cboForfeitedPeriodNo.Enabled = True
            cboForfeitedPeriodNo.CssClass = "form-control required"
            txtForfeitedCount.Enabled = True
            txtForfeitedCount.CssClass = "form-control required"
        Else
            cboForfeitedPeriodNo.Enabled = False
            cboForfeitedPeriodNo.CssClass = "form-control"
            cboForfeitedPeriodNo.Text = ""
            txtForfeitedCount.Enabled = False
            txtForfeitedCount.CssClass = "form-control"
            txtForfeitedCount.Text = ""
        End If

        If txtIsRefresh.Checked = True Then
            cboRefreshCutOffNo.Enabled = True
            cboRefreshCutOffNo.CssClass = "form-control required"
            cboRefreshCutOffDays.Enabled = True
            cboRefreshCutOffDays.CssClass = "form-control required"
        Else
            cboRefreshCutOffNo.Enabled = False
            cboRefreshCutOffNo.CssClass = "form-control"
            cboRefreshCutOffNo.Text = ""
            cboRefreshCutOffDays.Enabled = False
            cboRefreshCutOffDays.CssClass = "form-control"
            cboRefreshCutOffDays.Text = ""
        End If
        Try
            If Generic.ToInt(cboForfeitureTypeNo.SelectedValue) = 1 Then
                txtLeaveDays.Enabled = True
                txtLeaveDays.CssClass = "form-control required"
            ElseIf Generic.ToInt(cboForfeitureTypeNo.SelectedValue) = 2 Then
                txtSpecificDate.Enabled = True
                txtLeaveDays.Enabled = True
                txtSpecificDate.CssClass = "form-control required"
                txtLeaveDays.CssClass = "form-control required"
            Else
                txtSpecificDate.Enabled = False
                txtLeaveDays.Enabled = False
                txtSpecificDate.CssClass = "form-control"
                txtLeaveDays.CssClass = "form-control"
                txtSpecificDate.Text = ""
                txtLeaveDays.Text = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtIsMaintainBalance_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateRefresh()
        mdlDetl.Show()
    End Sub

    Protected Sub txtIsRefresh_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateRefresh()
        mdlDetl.Show()
    End Sub


End Class
