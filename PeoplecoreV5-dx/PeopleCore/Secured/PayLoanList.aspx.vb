Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web

Partial Class Secured_PayLoanList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

#Region "Main"

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            'dt = SQLHelper.ExecuteDataTable("ELoan_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            dt = SQLHelper.ExecuteDataTable("ELoan_WebFilter", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo, Generic.ToInt(cbofilterby.SelectedValue), Generic.ToInt(cbofiltervalue.SelectedValue), Filter1.SearchText)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ELoan_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
                'PopulatePlaceHolder(Generic.ToInt(cboPayDeductTypeNo.SelectedValue))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub
    Protected Sub lnkSearch2_Click(sender As Object, e As EventArgs)
        EnableDisable()
    End Sub
    Protected Sub EnableDisable()

        grdDetl.Visible = False
        lnkAddDetl.Visible = False
        lnkDeleteDetl.Visible = False

        grdDetl2.Visible = False
        Me.lnkAddDetl2.Visible = False : Me.lnkDeleteDetl2.Visible = False : Me.lnkExportgrdDetl2.Visible = False


        grdDetl3.Visible = False
        lnkDeleteDetl3.visible = False

        If Generic.ToInt(Me.cboTab2No.SelectedValue) = 1 Then
            PopulateGridDetl()
            Me.grdDetl.Visible = True
            Me.lnkAddDetl.Visible = True : Me.lnkDeleteDetl.Visible = True

            Me.grdDetl2.Visible = False
            Me.lnkAddDetl2.Visible = False : Me.lnkDeleteDetl2.Visible = False : Me.lnkExportgrdDetl2.Visible = False

        ElseIf Generic.ToInt(Me.cboTab2No.SelectedValue) = 2 Then
            PopulateGridDetl2()
            Me.grdDetl.Visible = False
            Me.lnkAddDetl.Visible = False : Me.lnkDeleteDetl.Visible = False

            Me.grdDetl2.Visible = True
            Me.lnkAddDetl2.Visible = True : Me.lnkDeleteDetl2.Visible = True : Me.lnkExportgrdDetl2.Visible = True


        ElseIf Generic.ToInt(Me.cboTab2No.SelectedValue) = 3 Then
            PopulateGridDetl3()
            grdDetl3.Visible = True
            lnkDeleteDetl3.Visible = True
        Else
            PopulateGridDetl()
            Me.grdDetl.Visible = True
            Me.lnkAddDetl.Visible = True : Me.lnkDeleteDetl.Visible = True

            Me.grdDetl2.Visible = False
            Me.lnkAddDetl2.Visible = False : Me.lnkDeleteDetl2.Visible = False : Me.lnkExportgrdDetl2.Visible = False

        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LoanNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"LoanNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ELoan", UserNo, item)
                Generic.DeleteRecordAuditCol("ELoanPay", UserNo, "LoanNo", item)
                Generic.DeleteRecordAuditCol("ELoanPaySched", UserNo, "LoanNo", item)
                Generic.DeleteRecordAuditCol("ELoanPayForw", UserNo, "LoanNo", item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            PopulatePlaceHolder(0)
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            Dim x As New XlsxExportOptionsEx
            x.ExportType = ExportType.WYSIWYG
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim Retval As Boolean = False
        Dim LoanTypeNo As Integer = Generic.ToInt(Me.cboPayDeductTypeNo.SelectedValue)
        Dim EmployeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))
        Dim PrincipalAmount As Decimal = Generic.ToDec(Me.txtPrincipalAmount.Text)
        Dim InterestRate As Decimal = Generic.ToDec(Me.txtInterestRate.Text)
        Dim TotalAmount As Decimal = Generic.ToDec(Me.txtTotalAmount.Text)
        Dim NoOfPayment As Integer = Generic.ToInt(Me.txtNoOfPayment.Text)
        Dim PayScheduleNo As Integer = Generic.ToInt(Me.cboPayScheduleNo.SelectedValue)
        Dim Balance As Decimal = Generic.ToDec(Me.txtBalance.Text)
        Dim LoanFormulaTypeNo As Integer = Generic.ToInt(cboLoanFormulaTypeNo.SelectedValue)
        Dim interest As Double = Generic.ToDec(txtInterest.Text)
        Dim maturitydate As String = Generic.ToStr(txtMaturityDate.Text)

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("ELoan_WebValidate", UserNo, Generic.ToInt(txtCode.Text), EmployeeNo, LoanTypeNo, Me.txtDateGranted.Text.ToString, Me.txtDeductionStart.Text.ToString, PrincipalAmount, InterestRate, TotalAmount, NoOfPayment, Generic.ToDec(Me.txtAmort.Text), PayScheduleNo, Balance, Me.txtRemarks.Text, chkIsDeductBonus.Checked, Generic.ToDec(Me.txtBegBalance.Text), chkIsPrepaid.Checked, txtPrepaidDate.Text, txtRefNo.Text, chkIsSuspend.Checked, PayLocNo)


        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            ModalPopupExtender1.Show()

            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("ELoan_WebSave", UserNo, Generic.ToInt(txtCode.Text), EmployeeNo, LoanTypeNo, Me.txtDateGranted.Text.ToString, Me.txtDeductionStart.Text.ToString, PrincipalAmount, InterestRate, TotalAmount, NoOfPayment, Generic.ToDec(Me.txtAmort.Text), PayScheduleNo, Balance, Me.txtRemarks.Text, chkIsDeductBonus.Checked, Generic.ToDec(Me.txtBegBalance.Text), chkIsPrepaid.Checked, txtPrepaidDate.Text, txtRefNo.Text, chkIsSuspend.Checked, PayLocNo, 0, LoanFormulaTypeNo, interest, maturitydate) > 0 Then
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


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()
        End If
        PopulateGrid()
        EnableDisable()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)        

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 2)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
        Try
            cboPayDeductTypeNo.DataSource = SQLHelper.ExecuteDataSet("ELoanType_WebLookup", UserNo, PayLocNo)
            cboPayDeductTypeNo.DataValueField = "tNo"
            cboPayDeductTypeNo.DataTextField = "tDesc"
            cboPayDeductTypeNo.DataBind()
        Catch ex As Exception

        End Try
        Try
            cboTab2No.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 37)
            cboTab2No.DataTextField = "tDesc"
            cboTab2No.DataValueField = "tno"
            cboTab2No.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub cboPayDeductTypeNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        'PopulatePlaceHolder(Generic.ToInt(cboPayDeductTypeNo.SelectedValue))
        'ModalPopupExtender1.Show()
    End Sub
    Private Sub PopulatePlaceHolder(typeno As Integer)
        Dim dt As DataTable
        cboPayDeductTypeNo.SelectedValue = ""
        dt = SQLHelper.ExecuteDataTable("EPayDeductType_WebOne", UserNo, typeno)
        For Each row As DataRow In dt.Rows
            cboPayDeductTypeNo.SelectedValue = Generic.ToInt(row("PayDeductTypeNo"))
            txtPrincipalAmount.Text = Generic.ToDec(row("Amount"))
            txtBegBalance.Text = Generic.ToDec(row("Amount"))
            txtAmort.Text = Generic.ToDec(row("NoofPayment"))
            If txtAmort.Text > 0 Then
                txtNoOfPayment.Text = Ceiling(Generic.ToDec(row("Amount")) / Generic.ToDec(row("NoofPayment")))
            End If
        Next
        
    End Sub
    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"LoanNo", "Code"})
        ViewState("TransNo") = obj(0)
        lbl.Text = "Transaction No. : " & obj(1)
        PopulateGridDetl()
    End Sub

#End Region

#Region "Detail"

    Private Sub PopulateGridDetl()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ELoanPay_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub
    Private Sub PopulateGridDetl2()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ELoanPaySched_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdDetl2.DataSource = dt
        grdDetl2.DataBind()
    End Sub
    Private Sub PopulateGridDetl3()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ELoanPayForw_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdDetl3.DataSource = dt
        grdDetl3.DataBind()
    End Sub

    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel2")
            ModalPopupExtender2.Show()
        End If
    End Sub
    Protected Sub lnkAddDetl2_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel3")
            ModalPopupExtender3.Show()
        End If
    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"LoanPayNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ELoanPay", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridDetl()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkDeleteDetl2_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl2.GetSelectedFieldValues(New String() {"LoanPaySchedNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ELoanPaySched", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridDetl2()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkDeleteDetl3_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl3.GetSelectedFieldValues(New String() {"LoanPayForwNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ELoanPayForw", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridDetl3()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataDetl(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LoanPayNo"})))
            ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Protected Sub lnkEditDetl2_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataDetl2(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LoanPaySchedNo"})))
            Me.txtTotalAmort.Enabled = False
            Me.txtBalancePres.Enabled = False
            ModalPopupExtender3.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Protected Sub lnkEditDetl3_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataDetl3(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LoanPayForwNo"})))
            ModalPopupExtender5.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
        If SaveRecordDetl() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecordDetl() As Boolean
        If SQLHelper.ExecuteNonQuery("ELoanPay_WebSave", UserNo, Generic.ToInt(txtCodeDetl.Text), Generic.ToInt(ViewState("TransNo")), Generic.ToDec(txtAmount.Text), txtDatePrepaid.Text, txtRemarksPrepaid.Text) > 0 Then
            SaveRecordDetl = True
        Else
            SaveRecordDetl = False
        End If
    End Function
    Protected Sub lnkSaveDetl2_Click(sender As Object, e As EventArgs)
        If SaveRecordDetl2() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl2()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecordDetl2() As Boolean
        If SQLHelper.ExecuteNonQuery("ELoanPaySched_WebSave", UserNo, Generic.ToInt(txtCodeDetl2.Text), Generic.ToInt(ViewState("TransNo")), txtScheduleDate.Text, Generic.ToDec(txtBalancePrev.Text), Generic.ToDec(txtPrincipalAmort.Text), Generic.ToDec(txtInterestAmort.Text), Generic.ToDec(txtPayment.Text), txtRemarksLoanSched.Text, PayLocNo) > 0 Then
            SaveRecordDetl2 = True
        Else
            SaveRecordDetl2 = False
        End If
    End Function
    Protected Sub lnkSaveDetl3_Click(sender As Object, e As EventArgs)
        If SaveRecordDetl3() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl3()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub
    Private Function SaveRecordDetl3() As Boolean
        Dim amort As Double = Generic.ToDbl(txtAmortForw.Text)
        Dim payment As Double = Generic.ToDbl(txtPaymentForw.Text)

        If SQLHelper.ExecuteNonQuery("ELoanPayForw_WebSave", UserNo, Generic.ToInt(txtLoanPayForwNo.Text), Generic.ToInt(ViewState("TransNo")), amort, payment, Generic.ToStr(txtPayDate.Text)) > 0 Then
            SaveRecordDetl3 = True
        Else
            SaveRecordDetl3 = False
        End If
    End Function
    Protected Sub grdDetl_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdDetl.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdDetl.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub
    Protected Sub grdDetl2_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdDetl2.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdDetl2.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub
    Protected Sub grdDetl3_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdDetl3.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdDetl3.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub
    Private Sub PopulateDataDetl(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ELoanPay_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
    End Sub
    Private Sub PopulateDataDetl2(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ELoanPaySched_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel3", dt)
    End Sub
    Private Sub PopulateDataDetl3(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ELoanPayForw_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel5", dt)
    End Sub
    Protected Sub lnkExportgrdDetl2_Click(sender As Object, e As EventArgs)
        Try
            grdExportgrdDetl2.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub
    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

#End Region

#Region "Upload"

    Protected Sub lnkUpload_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, Session("xFormName"), Session("xTableName")) Then
            Response.Redirect("~/secured/PayLoanList_Upload.aspx?id=0")
        Else
            MessageBox.Critical(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

#End Region

#Region "Remove Export Codes"
    Protected Sub grdExport_RenderBrick(sender As Object, e As DevExpress.Web.ASPxGridViewExportRenderingEventArgs) Handles grdExport.RenderBrick
        Dim dataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
        'If e.RowType = GridViewRowType.Data AndAlso dataColumn IsNot Nothing Then
        '    Select Case dataColumn.FieldName
        '        Case "Fullname"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<br/>", "")
        '        Case "PayClassDesc"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<br/>", "")
        '        Case "GrantedDate"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<br/>", "")
        '        Case "DeductStartDate"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<br/>", "")
        '        Case "TotalPayment"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<br/>", "")
        '    End Select

        'End If
        If e.RowType = GridViewRowType.Header AndAlso dataColumn IsNot Nothing Then
            e.Text = e.Text.Replace("<br/>", " ")
            e.Text = e.Text.Replace("<br>", " ")
            e.Text = e.Text.Replace("<center>", "")
            e.Text = e.Text.Replace("</center>", "")
        End If

    End Sub
#End Region

End Class



