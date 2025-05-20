Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PayIncomeTypeList
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
            lnkDelete.Visible = False
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        Else
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        End If
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayIncomeType_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
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
    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid(True)
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
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "PayIncomeTypeNo"))
                dt = SQLHelper.ExecuteDataTable("ETableReferrence_WebArchived", UserNo, "EPayIncomeType", item, 1, PayLocNo)
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
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayIncomeTypeNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayIncomeType", UserNo, CType(item, Integer))
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
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayIncomeTypeNo", "IsEnabled", "IsFixed"})
                i = Generic.ToInt(obj(0))
                IsEnabled = Generic.ToBol(obj(1))
                IsFixed = Generic.ToBol(obj(2))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPayIncomeType_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                Next
                Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)
                lnkSave.Enabled = IsEnabled

                If IsFixed = True Then
                    lnkSave.Enabled = True
                    txtEntityCode.Enabled = True
                    txtIncoOrder.Enabled = True
                End If

                If IsEnabled = True Then
                    checkDeminimis(txtIsDiminimis.Checked)
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
    Private Sub checkDeminimis(chk As Boolean)
        txtIsChargeToAccum.Enabled = True
        txtIsChargeToTaxable.Enabled = True
        txtthreshold.Enabled = True
        If Not chk Then
            txtIsChargeToAccum.Enabled = False
            txtIsChargeToTaxable.Enabled = False
            txtthreshold.Enabled = False
        End If
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            Generic.EnableControls(Me, "pnlPopupMain", True)
            txtCode.Enabled = False
            txtIsNonTaxable.Checked = True
            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try
            lnkSave.Enabled = True
            mdlMain.Show()
            checkDeminimis(False)
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    'Submit record
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtPayIncometypeNo.Text)
            Dim PayIncomeTypeCode As String = Generic.ToStr(Me.txtPayIncomeTypeCode.Text)
            Dim PayIncomeTypeDesc As String = Generic.ToStr(Me.txtPayIncomeTypeDesc.Text)
            Dim IsTaxable As Boolean = Generic.ToBol(Me.txtIsTaxable.Checked)
            Dim IsAccum As Boolean = Generic.ToBol(Me.txtIsAccum.Checked)
            Dim IsTaxOneTime As Boolean = False
            Dim IncoOrder As Integer = Generic.ToInt(Me.txtIncoOrder.Text)
            Dim IsBasic As Boolean = Generic.ToBol(Me.txtIsBasic.Checked)
            Dim IsGrossInc As Boolean = False
            Dim IsNonTaxable As Boolean = Generic.ToBol(Me.txtIsNonTaxable.Checked)
            Dim IsAdjustment As Boolean = False
            Dim IsPH As Boolean = False
            Dim IsHDMF As Boolean = False
            Dim IsSSS As Boolean = False
            Dim IsSupplementary As Boolean = False
            Dim IsDiminimis As Boolean = Generic.ToBol(Me.txtIsDiminimis.Checked)
            Dim IsFBT As Boolean = Generic.ToBol(Me.txtIsFBT.Checked)
            Dim IspaidByER As Boolean = Generic.ToBol(Me.txtIspaidByER.Checked)
            Dim IsAddTakehomepay As Boolean = Generic.ToBol(Me.txtIsAddTakehomepay.Checked)
            Dim EntityCode As String = Generic.ToStr(Me.txtEntityCode.Text)
            Dim IschargeToAccum As Boolean = Generic.ToBol(txtIsChargeToAccum.Checked)
            Dim IschargeToTaxable As Boolean = Generic.ToBol(txtIsChargeToTaxable.Checked)
            Dim Threshold As Double = Generic.ToDec(txtthreshold.Text)
            Dim IsAllowance As Boolean = Generic.ToBol(Me.txtIsAllowance.Checked)
            Dim isUnreg As Boolean = Generic.ToBol(txtIsUnreg.Checked)
            Dim PaySchedNo As Integer = Generic.ToInt(Me.cboPaySchedNo.SelectedValue)
            Dim ChargeToIncomeTypeNo As Integer = Generic.ToInt(Me.cboChargeToIncomeTypeNo.SelectedValue)
            Dim ThresholdMonthly As Double = Generic.ToDec(txtThresholdMonthly.Text)
            Dim IsArchived As Integer = Generic.ToInt(chkIsArchived.Checked)
            Dim IsExcludeContri As Boolean = Generic.ToBol(txtIsExcludeContri.Checked)

            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EPayIncomeType_WebValidate", UserNo, tno, PayIncomeTypeCode, PayIncomeTypeDesc, IsTaxable, IsAccum, IsTaxOneTime, IncoOrder, IsBasic, IsGrossInc, IsNonTaxable, IsAdjustment, IsPH, IsHDMF, IsSSS, IsSupplementary, IsDiminimis, IsFBT, IspaidByER, IsAddTakehomepay, EntityCode, PayLocNo, IschargeToAccum, IschargeToTaxable, Threshold, IsAllowance, IsArchived)

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

            If SQLHelper.ExecuteNonQuery("EPayIncomeType_WebSave", UserNo, tno, PayIncomeTypeCode, PayIncomeTypeDesc, IsTaxable, IsAccum, IsTaxOneTime, IncoOrder, IsBasic, IsGrossInc, IsNonTaxable, IsAdjustment, IsPH, IsHDMF, IsSSS, IsSupplementary, IsDiminimis, IsFBT, IspaidByER, IsAddTakehomepay, EntityCode, PayLocNo, IschargeToAccum, IschargeToTaxable, Threshold, IsAllowance, isUnreg, PaySchedNo, ChargeToIncomeTypeNo, ThresholdMonthly, IsArchived, IsExcludeContri) > 0 Then
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

#End Region

End Class



