Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_BenBenefitType
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub PopulateGrid()

        Try
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EBenefitType_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBenefitType_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
                Generic.PopulateData(Me, "phDeduct", dt)
                Generic.PopulateData(Me, "phIncome", dt)
                PopulateRefresh()
                PopulateDeductIncome()
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt("xPayLocNo"))
            Generic.PopulateDropDownList(UserNo, Me, "phDeduct", Generic.ToInt("xPayLocNo"))
            Generic.PopulateDropDownList(UserNo, Me, "phIncome", Generic.ToInt("xPayLocNo"))
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

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            PopulateRefresh()
            PopulateDeductIncome()

            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()
            Catch ex As Exception
            End Try
            lnkSave.Enabled = True
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim Retval As Boolean = False
        Dim BenefitTypeNo As Integer = Generic.ToInt(txtCode.Text)
        Dim BenefitTypeCode As String = Generic.ToStr(txtBenefitTypeCode.Text)
        Dim BenefitTypeDesc As String = Generic.ToStr(txtBenefitTypeDesc.Text)
        Dim IsForIncome As Boolean = Generic.ToBol(chkIsForIncome.Checked)
        Dim IsForDeduction As Boolean = Generic.ToBol(chkIsForDeduction.Checked)
        Dim IsViewOnline As Boolean = Generic.ToBol(chkIsViewOnline.Checked)
        Dim IsEnrollOnline As Boolean = Generic.ToBol(chkIsEnrollOnline.Checked)
        Dim IsArchived As Boolean = Generic.ToBol(chkIsArchived.Checked)
        Dim IsMaintainBalance As Boolean = Generic.ToBol(txtIsMaintainBalance.Checked)
        Dim IsUploadDoc As Boolean = Generic.ToBol(chkIsUploadDoc.Checked)
        Dim Deduction As Integer = Generic.ToInt(cboDeduct.SelectedValue)
        Dim Income As Integer = Generic.ToInt(cboIncome.SelectedValue)
        Dim BenefitCateNo As Integer = Generic.ToInt(cboBenefitCateNo.SelectedValue)
        Dim IsAccumulated As Boolean = Generic.ToBol(txtIsAccumulated.Checked)
        Dim IsForefeited As Boolean = 0 'Generic.ToBol(txtIsForefeited.Checked)
        Dim IsRefresh As Boolean = Generic.ToBol(txtIsRefresh.Checked)
        Dim RefreshCutOffNo As Integer = Generic.ToInt(cboRefreshCutOffNo.SelectedValue)
        Dim RefreshCutOffDays As Integer = Generic.ToInt(cboRefreshCutOffDays.SelectedValue)
        Dim PayDeductTypeNo As Integer = Generic.ToInt(cboPayDeductTypeNo.SelectedValue)
        Dim PayIncomeTypeNo As Integer = Generic.ToInt(cboPayIncomeTypeNo.SelectedValue)
        Dim Note As String = Generic.ToStr(txtNote.Text)
        Dim Amount As Double = Generic.ToDec(txtAmount.Text)
        Dim IsWithDepe As Boolean = Generic.ToBol(chkIsWithDepe.Checked)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EBenefitType_WebValidate", UserNo, BenefitTypeNo, BenefitTypeCode, BenefitTypeDesc, IsForIncome, IsForDeduction, IsViewOnline, IsEnrollOnline, IsArchived, IsUploadDoc, Deduction, Income, BenefitCateNo, IsMaintainBalance, IsForefeited, IsAccumulated, IsRefresh, RefreshCutOffNo, RefreshCutOffDays, PayIncomeTypeNo, PayDeductTypeNo, PayLocNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If


        If SQLHelper.ExecuteNonQuery("EBenefitType_WebSave", UserNo, BenefitTypeNo, BenefitTypeCode, BenefitTypeDesc, IsForIncome, IsForDeduction, IsViewOnline, IsEnrollOnline, IsArchived, IsUploadDoc, Deduction, Income, BenefitCateNo, IsMaintainBalance, IsForefeited, IsAccumulated, IsRefresh, RefreshCutOffNo, RefreshCutOffDays, PayIncomeTypeNo, PayDeductTypeNo, Note, Amount, IsWithDepe, PayLocNo) > 0 Then
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

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender

            Generic.ClearControls(Me, "Panel1")

            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()
            Catch ex As Exception
            End Try

            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitTypeNo", "IsEnabled"})
            Dim i As Integer = Generic.ToInt(obj(0))
            Dim IsEnabled As Boolean = Generic.ToBol(obj(1))

            PopulateData(i)
            lnkSave.Enabled = IsEnabled
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BenefitTypeNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EBenefitType", UserNo, item)
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
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BenefitTypeNo"})
            Dim str As String = "", i As Integer = 0, ii As Integer = 0
            Dim IsRowSelected As Boolean
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("EBenefitType_WebArchive", UserNo, item, 1) > 0 Then
                    i = i + 1
                End If
            Next

            'For tcount = 0 To grdMain.VisibleRowCount - 1
            '    ii = Generic.ToInt(grdMain.GetRowValues(tcount, New String() {"BenefitTypeNo"}))
            '    IsRowSelected = grdMain.Selection.IsRowSelected(tcount)
            '    If IsRowSelected = True Then
            '        If SQLHelper.ExecuteNonQuery("EBenefitType_WebArchive", UserNo, ii, 1) > 0 Then
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

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub chkIsEnrollOnline_CheckedChanged(sender As Object, e As System.EventArgs)
        If chkIsEnrollOnline.Checked = True Then
            chkIsViewOnline.Checked = True
        End If

        ModalPopupExtender1.Show()
    End Sub

    Private Sub PopulateDropDown()

        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 14)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try


        For index As Integer = 1 To 31
            Dim li As New ListItem
            li.Value = index
            li.Text = index
            cboRefreshCutOffDays.Items.Add(li)
        Next

    End Sub
    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub


    Protected Sub PopulateRefresh()

        If txtIsMaintainBalance.Checked = True Then
            txtIsAccumulated.Enabled = True
            'txtIsForefeited.Enabled = True
            txtIsRefresh.Enabled = True
            If txtIsAccumulated.Checked = False Then
                txtIsRefresh.Checked = True
            End If
        Else
            txtIsAccumulated.Enabled = False
            'txtIsForefeited.Enabled = False
            txtIsRefresh.Enabled = False
            txtIsAccumulated.Checked = False
            'txtIsForefeited.Checked = False
            txtIsRefresh.Checked = False
        End If

        If txtIsRefresh.Checked = True Then
            cboRefreshCutOffNo.Enabled = True
            cboRefreshCutOffDays.Enabled = True
        Else
            cboRefreshCutOffNo.Enabled = False
            cboRefreshCutOffNo.Text = ""
            cboRefreshCutOffDays.Enabled = False
            cboRefreshCutOffDays.Text = "1"
        End If

    End Sub

    Protected Sub txtIsMaintainBalance_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateRefresh()
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub txtIsRefresh_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateRefresh()
        ModalPopupExtender1.Show()
    End Sub


    Protected Sub PopulateDeductIncome()

        Dim Required As String = "form-control required"
        Dim NotRequired As String = "form-control"

        Dim RequiredNumber As String = "form-control number required"
        Dim NotRequiredNumber As String = "form-control number"

        phDeduct.Visible = False
        phIncome.Visible = False

        cboPayDeductTypeNo.CssClass = NotRequired
        cboDeduct.CssClass = NotRequired

        cboPayIncomeTypeNo.CssClass = NotRequired
        txtAmount.CssClass = NotRequiredNumber
        cboIncome.CssClass = NotRequired

        If chkIsForDeduction.Checked Then
            phDeduct.Visible = True
            cboPayDeductTypeNo.CssClass = Required
            cboDeduct.CssClass = Required
        Else
            cboPayDeductTypeNo.Text = ""
            cboDeduct.Text = ""
        End If

        If chkIsForIncome.Checked Then
            phIncome.Visible = True
            cboPayIncomeTypeNo.CssClass = Required
            cboIncome.CssClass = Required
            txtAmount.CssClass = RequiredNumber
        Else
            cboPayIncomeTypeNo.Text = ""
            cboIncome.Text = ""
            txtAmount.Text = ""
        End If

    End Sub

    Protected Sub txtIsDeductIncome_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateDeductIncome()
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

End Class
