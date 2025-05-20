Imports System.Data
Imports System.Threading
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web

Partial Class Secured_PayLeaveList
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
        _dt = SQLHelper.ExecuteDataTable("EPay_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), 3, PayLocNo)
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
    Private Sub PopulateGridDetl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayLeaveDeti_Web", UserNo, Generic.ToInt(ViewState("PayNo")), "")
            grdDetl.DataSource = dt
            grdDetl.DataBind()
            lblDetl.Text = Generic.ToStr(ViewState("PayCode"))
        Catch ex As Exception

        End Try
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

    'Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
    '    Dim lnk As New LinkButton
    '    lnk = sender
    '    Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
    '    PopulateData(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
    '    ModalPopupExtender1.Show()
    'End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/PayLeaveEdit.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

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
        Response.Redirect("~/secured/PayLeaveEntitleList.aspx?id=" & id)
    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo", "PayCode"})
        ViewState("PayNo") = Generic.ToStr(obj(0))
        ViewState("PayCode") = Generic.ToStr(obj(1))
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
                Generic.DeleteRecordAuditCol("EPayLeaveEntitled", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayLeaveDeti", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainDeductOther", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainIncomeOther", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainDeduct", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainIncome", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMain", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("ELeaveCredit", UserNo, "PayNo", item)
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
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, process_status, "EPay", "EPayLeave_WebProcess", 2, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Critical(process_status, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPay", "EPayLeave_WebProcess", 2, ViewState("Id"))
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
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, strx, "EPay", "EPayLeave_WebProcess", 2, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Critical(strx, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPay", "EPayLeave_WebProcess", 2, ViewState("Id"))
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
            cmd.CommandText = "EPayLeave_WebProcess"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = AssynChronous.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            cmd.CommandTimeout = 0
            cmd.Parameters.Add("@UserNo", SqlDbType.Int, 4)
            cmd.Parameters("@UserNo").Value = Generic.ToInt(UserNo)
            cmd.Parameters.Add("@PayNo", SqlDbType.Int, 4)
            cmd.Parameters("@PayNo").Value = id
            'AssynChronous.RunCommandAsynchronous(cmd, "EPayLeave_WebProcess", SQLHelper.ConSTRAsyn, 0)
            process_status = AssynChronous.xRunCommandAsynchronous(cmd, "EPayLeave_WebProcess", SQLHelper.ConSTRAsyn, 0, err_num)
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
            PopulateGrid()
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
        Dim payperiod As Integer = Generic.CheckDBNull(Me.txtPayPeriod.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim applicableyear As Integer = Generic.CheckDBNull(Me.txtApplicableYear.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        'Dim applicablemonth As Integer = Generic.CheckDBNull(Me.txtApplicableMonth.Text,global.clsbase.clsBaseLibrary.enumObjectType.IntType)
        Dim payclassno As Integer = Generic.CheckDBNull(Me.cboPayClassNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        If SQLHelper.ExecuteNonQuery("EPayLeave_WebSave", UserNo, Generic.CheckDBNull(hifPayNo.Value, clsBase.clsBaseLibrary.enumObjectType.IntType),
                                     Me.txtStartDate.Text.ToString, Me.txtEndDate.Text.ToString,
                                     Me.txtPayDate.Text.ToString, payclassno, 3,
                                     Me.txtIsDeductTax.Checked, Me.txtIsIncludeForw.Checked,
                                     Me.txtIsIncludeLoan.Checked, Me.txtIsPaymentSuspended.Checked,
                                     txtIsIncludeOther.Checked, txtIsIncludeMass.Checked,
                                     False,
                                     Session("xPayLocNo")) > 0 Then
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

    Protected Sub lnkExportD_Click(sender As Object, e As EventArgs)
        Try
            grdExportD.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
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
        Dim ds As DataSet = SQLHelper.ExecuteDataSet("EPayLeave_WebProcess_Forward", UserNo, id)
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
