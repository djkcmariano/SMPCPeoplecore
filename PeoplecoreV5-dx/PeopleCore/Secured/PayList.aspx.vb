Imports System.Data
Imports System.Math
Imports System.Threading
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_PayList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim err_num As Integer = 0
    Dim process_status As String = ""

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPay_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), 1, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        '' THis is my changes
    End Sub

    Private Sub PopulateDropDownList()
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 4)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboDTRNo.DataSource = SQLHelper.ExecuteDataSet("EDTR_WebLookup", UserNo, PayLocNo)
            cboDTRNo.DataTextField = "tdesc"
            cboDTRNo.DataValueField = "tno"
            cboDTRNo.DataBind()
        Catch ex As Exception
        End Try
        Try
            cboPYNo.DataSource = SQLHelper.ExecuteDataSet("EPY_WebLookup", UserNo, PayLocNo)
            cboPYNo.DataTextField = "tdesc"
            cboPYNo.DataValueField = "tno"
            cboPYNo.DataBind()
        Catch ex As Exception
        End Try


        Try
            cboPayScheduleNo.DataSource = SQLHelper.ExecuteDataSet("EPaySchedule_WebLookup", UserNo, Generic.ToInt(cboPayTypeNo.SelectedValue))
            cboPayScheduleNo.DataTextField = "tdesc"
            cboPayScheduleNo.DataValueField = "tno"
            cboPayScheduleNo.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboPayClassNo.DataSource = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", UserNo, Session("xPayLocNo"))
            cboPayClassNo.DataTextField = "tdesc"
            cboPayClassNo.DataValueField = "tno"
            cboPayClassNo.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPay_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel1", dt)

        Dim paySource As String = cboPaySourceNo.SelectedValue
        fRegisterStartupScript("Sript", "disableenable_behind('" + paySource.ToString + "');")

    End Sub

    Private Sub PopulateEnabled(IsEnabled As Boolean)

        If Generic.ToInt(cboPaySourceNo.SelectedValue) <> 2 Then
            cboDTRNo.Enabled = True
            txtStartDate.Enabled = True
            txtEndDate.Enabled = True
            cboPayClassNo.Enabled = False
            cboPayTypeNo.Enabled = False
        ElseIf Generic.ToInt(cboPaySourceNo.SelectedValue) = 2 Then
            'cboDTRNo.Enabled = False
            txtStartDate.Enabled = False
            txtEndDate.Enabled = False
            cboPayClassNo.Enabled = True
            cboPayTypeNo.Enabled = True
        Else
            cboDTRNo.Enabled = False
            txtStartDate.Enabled = False
            txtEndDate.Enabled = False
            cboPayClassNo.Enabled = True
            cboPayTypeNo.Enabled = True
        End If

        If IsEnabled = False Then
            cboDTRNo.Enabled = False
            txtStartDate.Enabled = False
            txtEndDate.Enabled = False
            cboPayClassNo.Enabled = False
            cboPayTypeNo.Enabled = False
        End If

        If Me.chkIsSecondment.Checked Then
            Me.cboDTRNo.Enabled = False
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDownList()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            Generic.EnableControls(Me, "Panel1", True)
            Me.chkIsSecondment.Enabled = False
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Generic.ClearControls(Me, "Panel1")
        PopulateData(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
        Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
        Generic.EnableControls(Me, "Panel1", IsEnabled)
        lnkSave.Enabled = IsEnabled
        PopulateEnabled(IsEnabled)

        mdlDetl.Show()
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("EPay_WebProcess_MainIncomeForwDelete", item)
                SQLHelper.ExecuteNonQuery("EPay_WebProcess_MainDeductForwDelete", item)
                SQLHelper.ExecuteNonQuery("EPay_WebProcess_LoanPaymentDelete", item)
                SQLHelper.ExecuteNonQuery("EPayContDetiOther_WebForDelete", item)
                Generic.DeleteRecordAuditCol("EPaybonusEntitled", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayLastDeti", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPaybonusDeti", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainDeductOther", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainIncomeOther", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainDeduct", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainIncome", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMain", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainIncomeCont", UserNo, "PayNo", item)
                Generic.DeleteRecordAudit("EPay", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayNo"})
            Dim i As Integer = 0

            '//validate start here
            Dim InvalidCnt As Short = 0
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable

            For Each item As Integer In fieldValues

                dtx = SQLHelper.ExecuteDataTable("EPay_WebPost_Validate",UserNo, item)

                For Each rowx As DataRow In dtx.Rows
                    invalid = Generic.ToBol(rowx("Invalid"))
                    messagedialog = messagedialog & Pad.PadZero(2, InvalidCnt + 1) & ". " & Generic.ToStr(rowx("MessageDialog"))
                    alerttype = Generic.ToStr(rowx("AlertType"))

                    If invalid = True Then
                        InvalidCnt += 1
                    End If

                Next

                If invalid = False Then
                    SQLHelper.ExecuteNonQuery("EPay_WebPost", UserNo, item)
                    i = i + 1
                End If

            Next

            If InvalidCnt > 0 Then
                messagedialog = "<b>(" + i.ToString + ") " + MessageTemplate.SuccesPost & "</b><br/><br/>" & messagedialog
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            Else
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccesPost, Me)
                PopulateGrid()
            End If


        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If


    End Sub

    Protected Sub lnkProcess_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
                Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayNo"})
                If fieldValues.Count > 1 Or fieldValues.Count = 0 Then
                    MessageBox.Warning("Please select 1 transaction to process.", Me)
                    Exit Sub
                End If
                If fieldValues.Count = 1 Then
                    For Each item As Integer In fieldValues
                        ViewState("Id") = item
                        PayrollAsynProcess(item)
                    Next
                End If

                If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, process_status, "EPay", "EPay_WebProcess", 2, ViewState("Id"))
                    MessageBox.Critical(process_status, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPay", "EPay_WebProcess", 2, ViewState("Id"))
                    PopulateGrid()
                    process_status = Replace(process_status, "Command complete. Processing Time is :", "Processing completed at ")
                    MessageBox.Success(process_status, Me)
                End If
            Else
                MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkProcess_Detail_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
            ViewState("Id") = i

            Dim IsPosted As Boolean = True
            IsPosted = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsPosted"}))

            If IsPosted = False Then
                PayrollAsynProcess(i)
                Dim strx As String = process_status
                If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, strx, "EPay", "EPay_WebProcess", 2, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Critical(strx, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPay", "EPay_WebProcess", 2, ViewState("Id"))
                    PopulateGrid()
                    process_status = Replace(process_status, "Command complete. Processing Time is :", "Processing completed at ")
                    MessageBox.Success(process_status, Me)
                End If
            Else
                MessageBox.Warning(MessageTemplate.PostedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If

    End Sub
    Private Sub PayrollAsynProcess(id As String)
        Dim cmd As SqlClient.SqlCommand
        Try
            cmd = Nothing
            cmd = New SqlClient.SqlCommand
            cmd.CommandText = "EPay_WebProcess"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = AssynChronous.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            cmd.CommandTimeout = 0
            cmd.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            cmd.Parameters("@onlineuserno").Value = Generic.ToInt(UserNo)
            cmd.Parameters.Add("@PayNo", SqlDbType.Int, 4)
            cmd.Parameters("@PayNo").Value = id
            process_status = AssynChronous.xRunCommandAsynchronous(cmd, "EPay_WebProcess", SQLHelper.ConSTRAsyn, 0, err_num)
        Catch
        End Try
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



    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub cboDTRNo_TextChanged(sender As Object, e As EventArgs)
        PopulateDTR(Generic.ToInt(cboDTRNo.SelectedValue))
        mdlDetl.Show()
    End Sub
    Protected Sub cboPYNo_TextChanged(sender As Object, e As EventArgs)
        Populate_PY(Generic.ToInt(cboPYNo.SelectedValue))
        mdlDetl.Show()
    End Sub


    Protected Sub cboPaySchedule_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable
            Dim paycateno As Integer = 1
            Dim paySourceNo As Integer = Generic.ToInt(Me.cboPaySourceNo.SelectedValue)
            Dim payClassNo As Integer = Generic.ToInt(Me.cboPayClassNo.SelectedValue)
            Dim payScheduleNo As Integer = Generic.ToInt(Me.cboPayScheduleNo.SelectedValue)

            Me.txtIsDeductHDMF.Checked = False
            Me.txtIsDeductPH.Checked = False
            Me.txtIsDeductSSS.Checked = False
            'Me.txtIsPF.Checked = False
            'Me.txtIsIHP.Checked = False
            Me.txtIsRATA.Checked = False
            Me.txtIsLoyalty.Checked = False
            txtIsAttendanceBase.Checked = False
            txtIsDeductTax.Checked = False
            txtIsIncludeForw.Checked = False
            txtIsIncludeLoan.Checked = False
            txtIsIncludeMass.Checked = False
            txtIsIncludeOther.Checked = False

            'dt = SQLHelper.ExecuteDataTable("EPayContributionSchedule_WebSP", Generic.ToInt(Me.cboPayScheduleNo.SelectedValue), Generic.ToInt(Me.cboPayClassNo.SelectedValue))
            dt = SQLHelper.ExecuteDataTable("EPayTemplate_Web_Select", UserNo, paycateno, paySourceNo, payClassNo, payScheduleNo)
            For Each row As DataRow In dt.Rows
                txtIsDeductHDMF.Checked = Generic.ToBol(row("IsDeductHDMF"))
                txtIsDeductPH.Checked = Generic.ToBol(row("IsDeductPH"))
                txtIsDeductSSS.Checked = Generic.ToBol(row("IsDeductSSS"))
                'txtIsPF.Checked = Generic.ToBol(row("IsDeductPF"))
                'txtIsIHP.Checked = Generic.ToBol(row("IsDeductIHP"))
                txtIsRATA.Checked = Generic.ToBol(row("IsRATA"))
                txtIsLoyalty.Checked = Generic.ToBol(row("IsLoyalty"))

                txtIsAttendanceBase.Checked = Generic.ToBol(row("IsAttendanceBase"))
                txtIsDeductTax.Checked = Generic.ToBol(row("IsDeductTax"))
                txtIsIncludeForw.Checked = Generic.ToBol(row("IsIncludeForw"))
                txtIsIncludeLoan.Checked = Generic.ToBol(row("IsIncludeLoan"))
                txtIsIncludeMass.Checked = Generic.ToBol(row("IsIncludeMass"))
                txtIsIncludeOther.Checked = Generic.ToBol(row("IsIncludeOther"))
            Next
            mdlDetl.Show()
        Catch ex As Exception

        End Try
        mdlDetl.Show()
    End Sub

    Private Sub PopulateDTR(ByVal tDTRNo As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTR_WebOne", UserNo, tDTRNo)
            For Each row As DataRow In dt.Rows
                txtStartDate.Text = Generic.ToStr(row("StartDate"))
                txtEndDate.Text = Generic.ToStr(row("EndDate"))
                cboPayClassNo.Text = Generic.ToStr(row("PayClassNo"))
                cboPayTypeNo.Text = Generic.ToStr(row("PayTypeNo"))
                cboDTRNo.Text = Generic.ToStr(row("DTRNo"))
                txtPayStartDate.Text = Generic.ToStr(row("PayStartDate"))
                txtPayEndDate.Text = Generic.ToStr(row("PayEndDate"))
            Next

            If tDTRNo > 0 Then
                txtStartDate.Enabled = False
                txtEndDate.Enabled = False
                cboPayClassNo.Enabled = False
                cboPayTypeNo.Enabled = False
                cboPYNo.Enabled = False
                Me.chkIsSecondment.Enabled = False
                Me.chkIsSecondment.Checked = False
                Me.cboDTRNo.Enabled = True
            Else
                txtStartDate.Enabled = True
                txtEndDate.Enabled = True
                cboPayClassNo.Enabled = True
                cboPayTypeNo.Enabled = True
                txtStartDate.Text = ""
                txtEndDate.Text = ""
                cboPayClassNo.Text = ""
                cboPayTypeNo.Text = ""

                If Me.cboPaySourceNo.SelectedValue = 1 Then
                    Me.chkIsSecondment.Enabled = False
                Else
                    Me.chkIsSecondment.Enabled = True
                End If

            End If

        Catch ex As Exception
        End Try
    End Sub
    Private Sub Populate_PY(ByVal pyNo As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPY_WebOne", UserNo, pyNo)
            For Each row As DataRow In dt.Rows
                txtStartDate.Text = Generic.ToStr(row("DateFrom"))
                txtEndDate.Text = Generic.ToStr(row("Dateto"))
                cboPayClassNo.Text = Generic.ToStr(row("PayClassNo"))
                cboPayTypeNo.Text = Generic.ToStr(row("PayTypeNo"))
                cboPYNo.Text = Generic.ToStr(row("pyNo"))
                
            Next

            If pyNo > 0 Then
                txtStartDate.Enabled = False
                txtEndDate.Enabled = False
                cboPayClassNo.Enabled = False
                cboPayTypeNo.Enabled = False
                cboDTRNo.Enabled = False
            Else
                txtStartDate.Enabled = True
                txtEndDate.Enabled = True
                cboPayClassNo.Enabled = True
                cboPayTypeNo.Enabled = True
                txtStartDate.Text = ""
                txtEndDate.Text = ""
                cboPayClassNo.Text = ""
                cboPayTypeNo.Text = ""
            End If

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim RetVal As Boolean = False
        Dim PayPeriod As Integer = Generic.ToInt(Me.txtPayperiod.Text)
        Dim ApplicableYear As Integer = Generic.ToInt(Me.txtApplicableYear.Text)
        Dim ApplicableMonth As Integer = Generic.ToInt(Me.cboApplicableMonth.SelectedValue)
        Dim PayTypeNo As Integer = Generic.ToInt(Me.cboPayTypeNo.SelectedValue)
        Dim PayClassNo As Integer = Generic.ToInt(Me.cboPayClassNo.SelectedValue)
        Dim PayScheduleNo As Integer = Generic.ToInt(Me.cboPayScheduleNo.SelectedValue)
        Dim DTRNo As Integer = Generic.ToInt(Me.cboDTRNo.SelectedValue)
        Dim IsAttendancenonBasic As Boolean = Generic.ToBol(txtIsAttendanceNonBasic.Checked)
        Dim IsRATA As Boolean = Generic.ToBol(txtIsRATA.Checked)
        Dim IsRice As Boolean = Generic.ToBol(txtIsRice.Checked)
        Dim IsMedical As Boolean = Generic.ToBol(txtIsMedical.Checked)
        Dim paysourceno As Integer = Generic.ToInt(cboPaySourceNo.SelectedValue)
        Dim pyNo As Integer = Generic.ToInt(cboPYNo.SelectedValue)
        Dim isloyalty As Integer = Generic.ToBol(txtIsLoyalty.Checked)
        'Dim IsPF As Boolean = Generic.ToBol(txtIsPF.Checked)
        'Dim IsHF As Boolean = Generic.ToBol(txtIsIHP.Checked)
        Dim IsSecondment As Boolean = Generic.ToBol(chkIsSecondment.Checked)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EPay_WebValidate", UserNo, Generic.ToInt(hifPayNo.Value), DTRNo, txtStartDate.Text, txtEndDate.Text, PayPeriod, txtPayDate.Text, ApplicableYear, ApplicableMonth, PayTypeNo, PayClassNo, PayScheduleNo, 1, _
                                      txtIsDeductTax.Checked, txtIsDeductSSS.Checked, txtIsDeductPH.Checked, txtIsDeductHDMF.Checked, txtIsAttendanceBase.Checked, txtIsIncludeForw.Checked, txtIsIncludeMass.Checked, txtIsIncludeLoan.Checked, _
                                      txtIsIncludeOther.Checked, paysourceno, False, False, Me.txtPayStartDate.Text, txtPayEndDate.Text, IsAttendancenonBasic, IsRATA, IsRice, IsMedical, PayLocNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlDetl.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EPay_WebSave", UserNo, Generic.ToInt(hifPayNo.Value), DTRNo, txtStartDate.Text, txtEndDate.Text, PayPeriod, txtPayDate.Text, ApplicableYear, ApplicableMonth, PayTypeNo, PayClassNo, PayScheduleNo, 1, _
                                      txtIsDeductTax.Checked, txtIsDeductSSS.Checked, txtIsDeductPH.Checked, txtIsDeductHDMF.Checked, txtIsAttendanceBase.Checked, txtIsIncludeForw.Checked, txtIsIncludeMass.Checked, txtIsIncludeLoan.Checked, _
                                      txtIsIncludeOther.Checked, paysourceno, 0, 0, Me.txtPayStartDate.Text, txtPayEndDate.Text, IsAttendancenonBasic, IsRATA, IsRice, IsMedical, PayLocNo, pyNo, txtRemarks.Text.ToString, isloyalty, IsSecondment) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub
    Protected Sub lnkDTR_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim Isspecialpay As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"Isspecialpay"}))
        Session("PayclassNo_DTR") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassNo"}))
        Response.Redirect("~/secured/PayList_DTR.aspx?id=" & container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}) & "&Isspecialpay=" & Isspecialpay)
    End Sub

    Protected Sub lnkSummary_Click(sender As Object, e As EventArgs)       
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Session("PayclassNo_Pay") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassNo"}))
        Response.Redirect("~/secured/paymainlist.aspx?id=" & container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
    End Sub


    Protected Sub lnkTemplate_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Session("PayclassNo_Pay") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassNo"}))
        Response.Redirect("~/secured/PayList_MainIncomeMassCrit.aspx?id=" & container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
    End Sub

    Protected Sub lnkOtherIncome_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender

        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Session("PayclassNo_Pay") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassNo"}))
        Response.Redirect("~/secured/paymainincomeotherlist.aspx?id=" & container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
    End Sub

    Protected Sub lnkOtherDeduction_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender

        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Session("PayclassNo_Pay") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassNo"}))
        Response.Redirect("~/secured/paymaindeductotherlist.aspx?id=" & container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
    End Sub


    Protected Sub lnkHistory_Click(sender As Object, e As EventArgs)
        'Dim lnk As New LinkButton
        'lnk = sender
        'Response.Redirect("~/secured/frmFileUpload.aspx?id=" & Generic.Split(lnk.CommandArgument, 0) & "&display=" & Generic.Split(lnk.CommandArgument, 1))
        Dim lnk As New LinkButton
        Dim AuditTableNo As Integer = 1
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        History.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
        History.xModify = True
        History.Show()
    End Sub

#Region "********Reports********"

    Protected Sub MyGridView_FillContextMenuItems(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuEventArgs)
        If e.MenuType = GridViewContextMenuType.Rows Then
            'e.Items.Add(e.CreateItem("Get Key", "GetKey"))
            e.Items.Clear()
        End If
    End Sub

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        'Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
        Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "PayNo")
        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, "0"), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"), _
                                                  New ReportParameter(ReportParameter.Type.int, id.ToString()), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"))
        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=83&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkRptSummary_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "PayNo")
        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, PayLocNo.ToString), _
                                                  New ReportParameter(ReportParameter.Type.int, id.ToString()))

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=66&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkRptPayment_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "PayNo")
        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, PayLocNo.ToString), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"), _
                                                  New ReportParameter(ReportParameter.Type.int, id.ToString()), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"))
        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=59&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

#End Region

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
End Class





