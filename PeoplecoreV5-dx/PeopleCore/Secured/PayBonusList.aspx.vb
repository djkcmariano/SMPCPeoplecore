Imports System.Data
Imports System.Math
Imports System.Threading
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_PayBonusList
    Inherits System.Web.UI.Page

    Dim clsArray As New clsBase.clsArray
    Dim xScript As String = ""
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim process_status As String = ""
    Dim err_num As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim tabOrder As Integer = Generic.ToInt(cboTabNo.SelectedValue)

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPay_Web", UserNo, tabOrder, 4, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If ViewState("PayNo") = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"PayNo", "PayCode"})
            ViewState("PayNo") = obj(0)
            ViewState("PayCode") = obj(1)
        End If

        PopulateGridDetl()

        Session(xScript & "TabNo") = tabOrder
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
            cboPayClassNo.DataSource = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", UserNo, PayLocNo)
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
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        clsArray.myPage.Pagename = Request.ServerVariables("SCRIPT_NAME")
        clsArray.myPage.Pagename = clsArray.GetPath(clsArray.myPage.Pagename)
        xScript = clsArray.myPage.Pagename

        If Not IsPostBack Then
            cboTabNo.Text = Generic.ToStr(Session(xScript & "TabNo"))
            PopulateDropDownList()
        End If

        PopulateGrid()
        PopulateGridDetl()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    'Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '        Generic.ClearControls(Me, "Panel1")
    '        ModalPopupExtender1.Show()
    '    Else
    '        MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
    '    End If
    'End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/PayBonusEdit.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    'Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
    '    Dim lnk As New LinkButton
    '    lnk = sender
    '    Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
    '    PopulateData(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
    '    ModalPopupExtender1.Show()
    'End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim URL As String = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"})))
            If URL <> "" Then
                Response.Redirect(URL)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkEntitlement_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
        Response.Redirect("~/secured/PayBonusEntitleList.aspx?id=" & id)
    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo", "PayCode"})
        ViewState("PayNo") = obj(0)
        ViewState("PayCode") = obj(1)
        PopulateGridDetl()
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("EPay_WebProcess_MainIncomeForwDelete", item)
                SQLHelper.ExecuteNonQuery("EPay_WebProcess_MainDeductForwDelete", item)
                SQLHelper.ExecuteNonQuery("EPay_WebProcess_LoanPaymentDelete", item)
                Generic.DeleteRecordAuditCol("EPaybonusEntitled", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayLastDeti", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPaybonusDeti", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainDeductOther", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainIncomeOther", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainDeduct", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainIncome", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMain", UserNo, "PayNo", item)
                Generic.DeleteRecordAudit("EPay", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid(True)
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("EPay_WebPost", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccesPost, Me)
            PopulateGrid(True)
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
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, process_status, "EPay", "EPayBonus_WebProcess", 2, ViewState("Id"))
                    MessageBox.Critical(process_status, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPay", "EPayBonus_WebProcess", 2, ViewState("Id"))
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

            Dim IsPosted As Boolean = True
            IsPosted = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsPosted"}))

            If IsPosted = False Then
                PayrollAsynProcess(i)
                Dim strx As String = process_status
                If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, strx, "EPay", "EpayBonus_WebProcess", 2, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Critical(strx, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPay", "EpayBonus_WebProcess", 2, ViewState("Id"))
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
            cmd.CommandText = "EPayBonus_WebProcess"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = AssynChronous.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            cmd.CommandTimeout = 0
            cmd.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            cmd.Parameters("@onlineuserno").Value = Generic.ToInt(UserNo)
            cmd.Parameters.Add("@PayNo", SqlDbType.Int, 4)
            cmd.Parameters("@PayNo").Value = id
            'AssynChronous.RunCommandAsynchronous(cmd, "EPayBonus_WebProcess", SQLHelper.ConSTRAsyn, 0)
            process_status = AssynChronous.xRunCommandAsynchronous(cmd, "EPayBonus_WebProcess", SQLHelper.ConSTRAsyn, 0, err_num)
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
        PopulateGrid(True)
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Private Function SaveRecord() As Boolean
        Dim PayPeriod As Integer = Generic.ToInt(Me.txtPayperiod.Text)
        Dim ApplicableYear As Integer = Generic.ToInt(Me.txtApplicableYear.Text)
        Dim ApplicableMonth As Integer = Generic.ToInt(Me.cboApplicableMonth.Text)

        Dim PayClassNo As Integer = Generic.ToInt(Me.cboPayClassNo.SelectedValue)
        If SQLHelper.ExecuteNonQuery("EPayBonus_WebSave", UserNo, Generic.ToInt(hifPayNo.Value), txtStartDate.Text, txtEndDate.Text, _
                                     txtPayDate.Text, PayClassNo, 4, txtIsDeductTax.Checked, txtIsIncludeForw.Checked, _
                                     txtIsIncludeLoan.Checked, txtIsActivateDed.Checked, txtIsPaymentSuspended.Checked, _
                                      Generic.ToDec(Me.txtNoOfMonthsAssume.Text), Generic.ToInt(Me.cboPEPeriodNo.SelectedValue), _
                                     txtIsIncludeOther.Checked, txtIsIncludeMass.Checked, PayLocNo, 0) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False

        End If

    End Function

    Protected Sub lnkSummary_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Response.Redirect("~/secured/paymainlist.aspx?id=" & container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
    End Sub

    Protected Sub lnkTemplate_Click(sender As Object, e As EventArgs)
      
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

    Private Sub PopulateGridDetl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayBonusDeti_Web", UserNo, Generic.ToInt(ViewState("PayNo")), "")
            grdDetl.DataSource = dt
            grdDetl.DataBind()
            lblDetl.Text = Generic.ToStr(ViewState("PayCode"))
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lnkExportD_Click(sender As Object, e As EventArgs)
        Try
            grdExportD.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub


    '#Region "********LWOP********"

    '    Protected Sub lnkLWOP_Click(sender As Object, e As EventArgs)
    '        Generic.ClearControls(Me, "Panel2")
    '        Dim lnk As New LinkButton
    '        Dim dt As DataTable
    '        lnk = sender
    '        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
    '        ViewState("PayNo") = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"})
    '        dt = SQLHelper.ExecuteDataTable("EPayBonusDeduPolicy_Web", UserNo, Generic.ToInt(ViewState("PayNo")))
    '        Generic.PopulateData(Me, "Panel2", dt)
    '        ModalPopupExtender2.Show()
    '    End Sub

    '    Protected Sub lnkSave2_Click(sender As Object, e As EventArgs)
    '        If SaveRecordDetl() Then
    '            MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '        Else
    '            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
    '        End If
    '    End Sub

    '    Private Function SaveRecordDetl() As Boolean
    '        If SQLHelper.ExecuteNonQuery("EPayBonusDeduPolicy_WebSave", UserNo, Generic.ToInt(txtCode.Text), Generic.ToInt(ViewState("PayNo")), _
    '                                     Generic.ToDec(Me.txtAbsent.Text), Generic.ToDec(Me.txtLate.Text), Generic.ToDec(Me.txtUnder.Text), _
    '                                     0, 0, txtStartDateX.Text, txtEndDateX.Text) > 0 Then
    '            SaveRecordDetl = True
    '        Else
    '            SaveRecordDetl = False

    '        End If
    '    End Function

    '#End Region


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
        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, PayLocNo.ToString), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"), _
                                                  New ReportParameter(ReportParameter.Type.int, id.ToString()), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"))
        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=83&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkForward_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "PayNo")
        Dim xmessage As String = "", alerttype As String = ""
        Dim ds As DataSet = SQLHelper.ExecuteDataSet("EPayBonus_WebProcess_Forward", UserNo, id)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                xmessage = Generic.ToStr(ds.Tables(0).Rows(0)("xmessage"))
                alerttype = Generic.ToStr(ds.Tables(0).Rows(0)("alerttype"))
            End If
        End If
        If xmessage.Length > 0 Then
            MessageBox.Alert(xmessage, alerttype, Me)
        End If
    End Sub


#End Region


End Class





