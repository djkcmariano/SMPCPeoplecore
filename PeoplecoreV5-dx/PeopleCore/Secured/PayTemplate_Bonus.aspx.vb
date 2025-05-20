Imports System.Data
Imports clsLib
Imports System.Math
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports Microsoft.VisualBasic.FileIO
Imports System.IO

Partial Class Secured_PayTemplate_Bonus
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer
    Dim PayCateNo As Integer


    Private Sub PopulateDataEntitled(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayTemplateBonus_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
        For Each row As DataRow In dt.Rows
            txtIsUpload.Checked = Generic.ToBol(row("IsUpload"))
            cboPEPeriodNo.Text = Generic.ToStr(row("PEPeriodNo"))
            txtEligibleDate.Text = Generic.ToStr(row("EligibleDate"))
            Dim bonusbasisno As Integer = cboBonusBasisNo.SelectedValue
            fRegisterStartupScript("Sript", "disableenable_behind('" + bonusbasisno.ToString + "');")
        Next

    End Sub

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayTemplateBonus_Web", UserNo, TransNo, 4, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If ViewState("TransNo") = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"PayTemplateBonusNo", "Code", "IsUpload"})
            'Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"PayTemplateBonusNo", "Code"})
            ViewState("TransNo") = obj(0)
            ViewState("IsUpload") = obj(2)
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticatedCoreUser()
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateDropDownListb()
            Try
                cboPEPeriodNo.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EPEPeriod", PayLocNo, "", "")
                cboPEPeriodNo.DataTextField = "tDesc"
                cboPEPeriodNo.DataValueField = "tNo"
                cboPEPeriodNo.DataBind()
            Catch ex As Exception
            End Try
            'Try
            '    cboPayScheduleNo.DataSource = SQLHelper.ExecuteDataSet("EPaySchedule_WebLookup_PayTemplate", UserNo)
            '    cboPayScheduleNo.DataTextField = "tDesc"
            '    cboPayScheduleNo.DataValueField = "tNo"
            '    cboPayScheduleNo.DataBind()
            'Catch ex As Exception

            'End Try
            divfactor.Visible = False
            divIncome.Visible = False
        End If
        PopulateGrid()
        PopulateGridb()
        PopulateGridd()
    End Sub



    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "Panel2")
        txtIsUpload.Checked = False
        cboPEPeriodNo.Text = ""
        txtEligibleDate.Text = ""
        EnabledControls(True)
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayTemplateBonusNo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EPayTemplateBonus", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton, IsEnabled As Boolean = False
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Generic.ClearControls(Me, "Panel2")
        PopulateDataEntitled(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTemplateBonusNo"})))

        'Generic.EnableControls(Me, "Panel2", IsEnabled)
        'lnkSave.Enabled = IsEnabled
        'EnabledControls(IsEnabled)

        ModalPopupExtender1.Show()

    End Sub
    Private Sub EnabledControls(Optional IsEnabled As Boolean = False)

        txtIsUpload.Enabled = False
        cboEmployeeStatNo.Enabled = False
        cboEmployeeClassNo.Enabled = False
        txtEligibleDate.Enabled = False
        txtMinServiceYear.Enabled = False
        cboPEPeriodNo.Enabled = False

        If IsEnabled = True Then
            cboPEPeriodNo.Enabled = True
            txtIsUpload.Enabled = True
            If txtIsUpload.Checked = False Then
                cboEmployeeStatNo.Enabled = True
                cboEmployeeClassNo.Enabled = True
                txtEligibleDate.Enabled = True
                txtMinServiceYear.Enabled = True
            Else
            End If
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim datebaseNo As Integer = Generic.ToInt(cboDatebaseNo.SelectedValue)
        Dim bonusTypeNo As Integer = Generic.ToInt(cboBonusTypeNo.SelectedValue)
        Dim bonusBasisNo As Integer = Generic.ToInt(cboBonusBasisNo.SelectedValue)
        Dim noofmonthsassume As Double = Generic.ToDec(txtnoofmonthsassume.Text)
        Dim IsUpload As Boolean = Generic.ToBol(txtIsUpload.Checked)
        Dim PEPeriodNo As Integer = Generic.ToInt(cboPEPeriodNo.SelectedValue)
        Dim fixedamount As Double = Generic.ToDbl(txtFixedAmount.Text)
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable

        'dtx = SQLHelper.ExecuteDataTable("EPayTemplateBonus_WebValidate", UserNo, Generic.ToInt(txtPayTemplateBonusNo.Text), TransNo, Generic.ToInt(Me.cboEmployeeStatNo.SelectedValue), _
        '                             Generic.ToInt(Me.cboEmployeeClassNo.SelectedValue), Generic.ToDec(Me.txtMinServiceYear.Text), Generic.ToDec(Me.txtPercentFactor.Text), _
        '                             txtIsApplytoAll.Checked, datebaseNo, bonusBasisNo, bonusTypeNo, txtcStartDate.Text.ToString, txtcEndDate.Text.ToString, noofmonthsassume,
        '                             Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0)), Generic.ToStr(txtEligibleDate.Text), IsUpload, PEPeriodNo)

        'For Each rowx As DataRow In dtx.Rows
        '    invalid = Generic.ToBol(rowx("tProceed"))
        '    messagedialog = Generic.ToStr(rowx("xMessage"))
        '    alerttype = Generic.ToStr(rowx("AlertType"))
        'Next

        'If invalid = True Then
        '    MessageBox.Alert(messagedialog, alerttype, Me)
        '    ModalPopupExtender1.Show()
        '    Exit Sub
        'End If

        Dim dt As DataTable, error_num As Integer = 0, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("EPayTemplateBonus_WebSave", UserNo, Generic.ToInt(txtPayTemplateBonusNo.Text), Generic.ToInt(cboPayClassNo.SelectedValue), Generic.ToInt(Me.cboEmployeeStatNo.SelectedValue), _
                                      Generic.ToInt(Me.cboEmployeeClassNo.SelectedValue), Generic.ToDec(Me.txtMinServiceYear.Text), Generic.ToDec(Me.txtPercentFactor.Text), _
                                      txtIsApplytoAll.Checked, 0, datebaseNo, bonusBasisNo, bonusTypeNo, "", "", noofmonthsassume,
                                      PEPeriodNo, Generic.ToStr(txtEligibleDate.Text), IsUpload, 4, PayLocNo, fixedamount)

        ViewState("IsUpload") = IsUpload
        If Not dt Is Nothing Then
            For Each row As DataRow In dt.Rows
                Retval = True
                error_num = Generic.ToInt(row("Error_num"))
                If error_num > 0 Then
                    error_message = Generic.ToStr(row("ErrorMessage"))
                    MessageBox.Critical(error_message, Me)
                    Retval = False
                End If

            Next
        End If
        If Retval = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If Retval = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub

   

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

  
#Region "Payroll Components"
    Protected Sub PopulateGridb()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayTemplate_Web", UserNo, TransNo, 4, PayLocNo)
            grdPay.DataSource = dt
            grdPay.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDropDownListb()
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", Generic.ToInt(Session("xPayLocNo")))
       
    End Sub

    Protected Sub lnkAddb_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.EnableControls(Me, "pnlPopupDetl", True)
            lnkSaveb.Enabled = True
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub
    Protected Sub lnkEditb_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim INo As String = Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTemplateNo"}))
            PopulateDatab(INo)
            Generic.EnableControls(Me, "pnlPopupDetl", True)
            lnkSaveb.Enabled = True
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Protected Sub lnkDeleteb_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdPay.GetSelectedFieldValues(New String() {"PayTemplateNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayTemplate", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridb()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkSaveb_Click(sender As Object, e As EventArgs)
        Dim payclassNo As Integer = Generic.ToInt(cboPayClassNoL.SelectedValue)
        Dim payTemplateNo As Integer = Generic.ToInt(txtPayTemplateNo.Text)
        Dim payScheduleNo As Integer = Generic.ToInt(cboPayScheduleNo.SelectedValue)
        Dim payCateNo As Integer = 4
        Dim paySourceNo As Integer = Generic.ToInt(cboPaySourceNo.SelectedValue)
        Dim isDeductTax = Generic.ToBol(txtIsDeductTax.Checked)
        Dim isDeductSSS = Generic.ToBol(txtIsDeductSSS.Checked)
        Dim isDeductPH = Generic.ToBol(txtIsDeductPH.Checked)
        Dim isDeductHDMF = Generic.ToBol(txtIsDeductHDMF.Checked)

        Dim IsAttendanceBase = Generic.ToBol(txtIsAttendanceBase.Checked)
        Dim IsIncludeForw = Generic.ToBol(txtIsIncludeForw.Checked)
        Dim IsIncludeMass = Generic.ToBol(txtIsIncludeMass.Checked)
        Dim IsIncludeOther = Generic.ToBol(txtIsIncludeOther.Checked)
        Dim IsIncludeLoan = Generic.ToBol(txtIsIncludeLoan.Checked)

        Dim IsRATA = Generic.ToBol(txtIsRATA.Checked)
        Dim IsLoyalty = 0 'Generic.ToBol(txtIsLoyalty.Checked)
        Dim IsMedical = Generic.ToBol(txtIsMedical.Checked)
        Dim IsRice = Generic.ToBol(txtIsRice.Checked)

        Dim RetVal As Boolean = True
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable

        Dim dt As DataTable, error_num As Integer = 0, error_message As String = ""

        dt = SQLHelper.ExecuteDataTable("EPayTemplate_WebSave", UserNo, payTemplateNo, payclassNo, payScheduleNo, payCateNo, paySourceNo, isDeductTax, isDeductSSS, isDeductPH, isDeductHDMF, IsAttendanceBase, IsIncludeForw, IsIncludeMass, IsIncludeOther, IsIncludeLoan, PayLocNo, IsRATA, IsLoyalty, IsMedical, IsRice)
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
            PopulateGridb()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub



   

    Private Sub PopulateDatab(iNo As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayTemplate_WebOne", UserNo, iNo)
        Generic.PopulateData(Me, "pnlPopupDetl", dt)
    End Sub

#End Region


#Region "deduction policy"
    Private Sub PopulateGridd()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayTemplateLWOP_Web", UserNo, 0, PayLocNo)
        Me.grdDedu.DataSource = _dt
        Me.grdDedu.DataBind()
    End Sub

    Private Sub PopulateDatad(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayTemplateLWOP_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel1", dt)
    End Sub

    Protected Sub lnkEditd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton, IsEnabled As Boolean = False
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Generic.ClearControls(Me, "Panel1")
            PopulateDatad(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTemplateLWOPNo"})))
            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub



    Protected Sub lnkAddd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then

            Generic.ClearControls(Me, "Panel1")
            mdlShow.Show()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If
    End Sub


    Protected Sub lnkDeleted_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDedu.GetSelectedFieldValues(New String() {"PayTemplateLWOPNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayTemplateLWOP", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
            PopulateGridd()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    'Submit record
    Protected Sub btnSaved_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim payclassno As Integer = Generic.ToInt(cboPayClassNoD.SelectedValue)

        Dim dt As DataTable, error_num As Integer = 0, error_message As String = "", RetVal As Boolean = False

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EPayTemplateLWOP_WebValidate", UserNo, Generic.ToInt(txtPayTemplateLWOPNo.Text), payclassno, _
                                     Generic.ToDec(Me.txtAbsent.Text), Generic.ToDec(Me.txtLate.Text), Generic.ToDec(Me.txtUnder.Text), _
                                     0, 0, 0, 0, 0, 0, 0, txtStartDateX.Text, txtEndDateX.Text, PayLocNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            Me.mdlShow.Show()
            Exit Sub
        End If

        dt = SQLHelper.ExecuteDataTable("EPayTemplateLWOP_WebSave", UserNo, Generic.ToInt(txtPayTemplateLWOPNo.Text), payclassno, _
                                     Generic.ToDec(Me.txtAbsent.Text), Generic.ToDec(Me.txtLate.Text), Generic.ToDec(Me.txtUnder.Text), _
                                     0, 0, 0, 0, 0, 0, 0, txtStartDateX.Text, txtEndDateX.Text, PayLocNo)
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
            PopulateGridd()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub

 
#End Region
#Region "Detail"

    Protected Sub lnkDetailIncome_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTemplateBonusNo", "Code"})
        ViewState("TransNo") = obj(0)
        'Response.Redirect("paytemplate_bonus_detl.aspx?id=" & ViewState("TransNo").ToString)
        PopulateGrid_Income()
        divfactor.Visible = False
        divIncome.Visible = True
    End Sub
    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTemplateBonusNo", "Code"})
        ViewState("TransNo") = obj(0)
        'Response.Redirect("paytemplate_bonus_detl.aspx?id=" & ViewState("TransNo").ToString)
        PopulateGrid_Factor()
        divfactor.Visible = True
        divIncome.Visible = False
    End Sub

    Protected Sub lnkDetailawop_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTemplateLWOPNo", "Code"})
        ViewState("TransNoLWOP") = obj(0)
        'Response.Redirect("paytemplate_bonus_detl.aspx?id=" & ViewState("TransNo").ToString)
        PopulateGrid_AWOP()
    End Sub
#End Region

#Region "Factor"

    Private Sub PopulateGrid_Factor(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayTemplateBonusDeti_Web", UserNo, ViewState("TransNo"))
        Me.grdFactor.DataSource = _dt
        Me.grdFactor.DataBind()

    End Sub
    Private Sub PopulateData_Factor(Id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayTemplateBonusDeti_WebOne", UserNo, Id)
        Generic.PopulateData(Me, "Panel3", dt)
        For Each row As DataRow In dt.Rows
            Me.txtxPercentFactor.Text = Generic.ToDbl(row("PercentFactor"))
            Textbox1.Text = Generic.ToStr(row("Code"))
        Next
    End Sub

    Protected Sub lnkAddfactor_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "Panel3")
        ModalPopupExtender2.Show()
    End Sub

    Protected Sub lnkDeletefactor_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayTemplateBonusDetiNo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EPayTemplateBonusDeti", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
    End Sub

    Protected Sub lnkEditfactor_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton, IsEnabled As Boolean = False
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Generic.ClearControls(Me, "Panel3")
        PopulateData_Factor(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTemplateBonusDetiNo"})))
        ModalPopupExtender2.Show()

    End Sub


    Protected Sub lnkSavefactor_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim sfrom As Double = Generic.ToDbl(txtSFrom.Text)
        Dim sto As Double = Generic.ToDbl(txtSTo.Text)
        Dim percentFactor As Double = Generic.ToDbl(txtxPercentFactor.Text)
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable


        Dim dt As DataTable, error_num As Integer = 0, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("EPayTemplateBonusDeti_WebSave", UserNo, Generic.ToInt(txtPayTemplateBonusDetiNo.Text), Generic.ToInt(ViewState("TransNo")), Generic.ToInt(sfrom), _
                                      Generic.ToInt(sto), Generic.ToDec(percentFactor))
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
            PopulateGrid_Factor()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub


#End Region

#Region "AWOP"

    Private Sub PopulateGrid_AWOP(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayTemplateLWOPDeti_Web", UserNo, ViewState("TransNoLWOP"))
        Me.grdAwop.DataSource = _dt
        Me.grdAwop.DataBind()

    End Sub
    Private Sub PopulateData_AWOP(Id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayTemplateLWOPDeti_WebOne", UserNo, Id)
        Generic.PopulateData(Me, "Panel4", dt)
        For Each row As DataRow In dt.Rows
            TextBox7.Text = Generic.ToDbl(row("PercentFactor"))
            Textbox4.Text = Generic.ToStr(row("Code"))
        Next
    End Sub

    Protected Sub lnkAddAWOP_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "Panel4")
        ModalPopupExtender3.Show()
    End Sub

    Protected Sub lnkDeleteAWOP_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdAwop.GetSelectedFieldValues(New String() {"PayTemplateLWOPDetiNo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EPayTemplateLWOPDeti", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
        PopulateGrid_AWOP()
    End Sub

    Protected Sub lnkEditAWOP_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton, IsEnabled As Boolean = False
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Generic.ClearControls(Me, "Panel4")
        PopulateData_AWOP(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTemplateLWOPDetiNo"})))
        ModalPopupExtender3.Show()

    End Sub


    Protected Sub lnkSaveAWOP_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim sfrom As Double = Generic.ToDbl(txtAWOPFrom.Text)
        Dim sto As Double = Generic.ToDbl(txtAWOPTO.Text)
        Dim percentFactor As Double = Generic.ToDbl(TextBox7.Text)
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable


        Dim dt As DataTable, error_num As Integer = 0, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("EPayTemplateLWOPDeti_WebSave", UserNo, Generic.ToInt(txtPayTemplateLWOPDetiNo.Text), Generic.ToInt(ViewState("TransNoLWOP")), Generic.ToInt(sfrom), _
                                      Generic.ToInt(sto), Generic.ToDec(percentFactor))
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
            PopulateGrid_AWOP()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub


#End Region

#Region "Income type"

    Private Sub PopulateGrid_Income(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayTemplateBonusAllowance_Web", UserNo, ViewState("TransNo"))
        Me.grdIncometype.DataSource = _dt
        Me.grdIncometype.DataBind()

    End Sub
    Private Sub PopulateData_Income(Id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayTemplateBonusAllowance_WebOne", UserNo, Id)
        Generic.PopulateData(Me, "PnlIncome", dt)
        For Each row As DataRow In dt.Rows
            Textbox3.Text = Generic.ToStr(row("Code"))
            Generic.PopulateDropDownList_Union(UserNo, Me, "PnlIncome", dt, PayLocNo)
        Next

    End Sub
    Protected Sub lnkAddIncome_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "PnlIncome")
        Generic.PopulateDropDownList(UserNo, Me, "PnlIncome", PayLocNo)
        ModalPopupExtender4.Show()
    End Sub

    Protected Sub lnkDeleteIncome_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdIncometype.GetSelectedFieldValues(New String() {"PayTemplateBonusAllowanceNo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EPayTemplateBonusAllowance", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid_Income()
    End Sub
    Protected Sub lnkEditIncome_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton, IsEnabled As Boolean = False
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Generic.ClearControls(Me, "PnlIncome")
        PopulateData_Income(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTemplateBonusAllowanceNo"})))
        ModalPopupExtender4.Show()

    End Sub

    Protected Sub lnkSaveIncome_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim sfrom As Double = Generic.ToDbl(txtSFrom.Text)
        Dim sto As Double = Generic.ToDbl(txtSTo.Text)
        Dim percentFactor As Double = Generic.ToDbl(txtPercentFactor.Text)
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable


        Dim dt As DataTable, error_num As Integer = 0, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("EPayTemplateBonusAllowance_WebSave", UserNo, Generic.ToInt(txtPayTemplateBonusAllowanceNo.Text), Generic.ToInt(ViewState("TransNo")), Generic.ToInt(cboPayIncomeTypeNo.SelectedValue))
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
            PopulateGrid_Income()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub
#End Region
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub

End Class







