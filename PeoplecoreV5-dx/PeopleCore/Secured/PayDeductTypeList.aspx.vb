Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web


Partial Class Secured_PayDeductTypeList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
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
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayDeductType_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

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

#Region "********Main*******"

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkArchive_Click(sender As Object, e As EventArgs)

        Dim dt As DataTable, tProceed As Boolean = False
        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "PayDeductTypeNo"))
                dt = SQLHelper.ExecuteDataTable("ETableReferrence_WebArchived", UserNo, "EPayDeductType", item, 1, PayLocNo)
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

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayDeductTypeNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayDeductType", UserNo, CType(item, Integer))
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

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer, IsEnabled As Boolean = False, IsFixed As Boolean = False
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayDeductTypeNo", "IsEnabled", "IsFixed"})
                i = Generic.ToInt(obj(0))
                IsEnabled = Generic.ToBol(obj(1))
                IsFixed = Generic.ToBol(obj(2))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPayDeductType_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                    PopulateControls()
                Next
                PopulateExempts(IsEnabled)
                Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)
                lnkSave.Enabled = IsEnabled

                If IsFixed = True Then
                    lnkSave.Enabled = True
                    txtEntityCode.Enabled = True
                    txtDeduOrder.Enabled = True
                    txtIsnotforHomepay.Enabled = True
                    txtIsOnline.Enabled = True
                End If

                Try
                    cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                    cboPayLocNo.DataTextField = "tdesc"
                    cboPayLocNo.DataValueField = "tNo"
                    cboPayLocNo.DataBind()

                Catch ex As Exception

                End Try
                mdlMain.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            Generic.EnableControls(Me, "pnlPopupMain", True)
            txtCode.Enabled = False
            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try
            lnkSave.Enabled = True
            PopulateControls()
            PopulateExempts(True)
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    'Submit record
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtPaydeducttypeNo.Text)
            Dim PayDeductTypeCode As String = Generic.ToStr(Me.txtPayDeductTypeCode.Text)
            Dim PayDeductTypeDesc As String = Generic.ToStr(Me.txtPayDeductTypeDesc.Text)
            Dim IsTaxExempt As Boolean = Generic.ToBol(Me.txtIsTaxExempt.Checked)
            Dim IsCashAdvance As Boolean = False
            Dim DeduOrder As Integer = Generic.ToInt(Me.txtDeduOrder.Text)
            Dim IsExcel As Boolean = True
            Dim IsAdjustment As Boolean = False
            Dim IsNotForHomePay As Boolean = Generic.ToBol(Me.txtIsnotforHomepay.Checked)
            Dim IsLoan As Boolean = Generic.ToBol(Me.txtIsLoan.Checked)
            Dim IsForPF As Boolean = Generic.ToBol(Me.txtIsForPF.Checked)
            Dim IsOnline As Boolean = Generic.ToBol(Me.txtIsOnline.Checked)
            Dim EntityCode As String = Generic.ToStr(Me.txtentityCode.Text)
            Dim TaxExemptNo As Integer = Generic.ToInt(Me.cboTaxExemptNo.SelectedValue)

            Dim IsSSSSalLoan As Boolean = False
            Dim IsSSSCalLoan As Boolean = False
            Dim IsHDMFLoan As Boolean = False
            Dim IsHDMFCalLoan As Boolean = False
            Dim LoanTypeNo As Integer = Generic.ToInt(Me.cboLoanTypeNo.SelectedValue)
            Dim IsArchived As Integer = Generic.ToInt(chkIsArchived.Checked)

            Select Case LoanTypeNo
                Case 1
                    IsSSSSalLoan = True
                Case 2
                    IsSSSCalLoan = True
                Case 3
                    IsHDMFLoan = True
                Case 4
                    IsHDMFCalLoan = True
            End Select

            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EPayDeductType_WebValidate", UserNo, tno, PayDeductTypeCode, PayDeductTypeDesc, IsTaxExempt, IsSSSSalLoan, IsSSSCalLoan, IsHDMFLoan, IsCashAdvance, DeduOrder, IsExcel, IsNotForHomePay, IsAdjustment, IsLoan, IsForPF, EntityCode, IsOnline, IsHDMFCalLoan, TaxExemptNo, PayLocNo, IsArchived)

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("Invalid"))
                messagedialog = Generic.ToStr(rowx("MessageDialog"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                mdlMain.Show()
                Exit Sub
            End If

            If SQLHelper.ExecuteNonQuery("EPayDeductType_WebSave", UserNo, tno, PayDeductTypeCode, PayDeductTypeDesc, IsTaxExempt, IsSSSSalLoan, IsSSSCalLoan, IsHDMFLoan, IsCashAdvance, DeduOrder, IsExcel, IsNotForHomePay, IsAdjustment, IsLoan, IsForPF, EntityCode, IsOnline, IsHDMFCalLoan, TaxExemptNo, PayLocNo, IsArchived) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                PopulateGrid()
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid(True)
    End Sub

    Protected Sub PopulateControls()

        If txtIsLoan.Checked = True Then
            cboLoanTypeNo.Enabled = True
        Else
            cboLoanTypeNo.Enabled = False
            cboLoanTypeNo.Text = ""
        End If

    End Sub

    Protected Sub txtIsRefresh_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateControls()
        mdlMain.Show()
    End Sub

    Protected Sub PopulateExempts(IsEnabled As Boolean)

        If IsEnabled = True Then
            If txtIsTaxExempt.Checked = True Then
                cboTaxExemptNo.Enabled = True
            Else
                cboTaxExemptNo.Enabled = False
                cboTaxExemptNo.Text = ""
            End If
        End If
        

    End Sub

    Protected Sub txtIsExempts_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateExempts(True)
        mdlMain.Show()
    End Sub



#End Region

End Class



