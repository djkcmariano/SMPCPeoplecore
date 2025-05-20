Imports System.Data
Imports System.Math
Imports System.Threading
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_PayLastList
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
        _dt = SQLHelper.ExecuteDataTable("EPay_Web", UserNo, tabOrder, 2, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If ViewState("PayNo") = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"PayNo", "PayCode", "PayClassNo", "IsPosted"})
            ViewState("PayNo") = obj(0)
            lblDetl.Text = obj(1)
            Session("PayLastList_PayclassNo") = Generic.ToStr(obj(2))
            Session("PayclassNo") = Generic.ToStr(obj(2))
            ViewState("IsPosted") = obj(3)
        End If

        PopulateGridDetl()

        Session(xScript & "TabNo") = tabOrder
    End Sub
    Private Sub PopulateGridDetl()

        If Generic.ToInt(ViewState("IsPosted")) = False Then
            lnkAddD.Visible = True
            lnkDeleteD.Visible = True
        Else
            lnkAddD.Visible = False
            lnkDeleteD.Visible = False
        End If

        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayLastDeti_Web", UserNo, Generic.ToInt(ViewState("PayNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub PopulateCombo()
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        Try
            cboPayClassNo.DataSource = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", UserNo, Session("xPayLocNo"))
            cboPayClassNo.DataTextField = "tdesc"
            cboPayClassNo.DataValueField = "tno"
            cboPayClassNo.DataBind()
        Catch ex As Exception

        End Try


        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 4)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

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
            PopulateCombo()
        End If

        PopulateGrid()
        PopulateGridDetl()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub


#Region "********MAIN********"

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

    Protected Sub lnkOtherIncome_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Session("PayclassNo_Pay") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassNo"}))
        Response.Redirect("~/secured/paymainincomeotherlist.aspx?id=" & container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}) & "&isfromlastpay=1")
    End Sub

    Protected Sub lnkOtherDeduction_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Session("PayclassNo_Pay") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassNo"}))
        Response.Redirect("~/secured/paymaindeductotherlist.aspx?id=" & container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}) & "&isfromlastpay=1")
    End Sub
    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo", "PayCode", "PayClassNo", "IsPosted"})
        ViewState("PayNo") = Generic.ToStr(obj(0))
        lblDetl.Text = Generic.ToStr(obj(1))
        Session("PayLastList_PayclassNo") = Generic.ToStr(obj(2))
        ViewState("IsPosted") = obj(3)
        PopulateGridDetl()
    End Sub
    Protected Sub lnkSummary_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Response.Redirect("~/secured/paymainlist.aspx?id=" & container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
    End Sub
    Protected Sub lnkLoan_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo", "PayCode", "PaylastdetiNo"})
        ViewState("PayNo") = Generic.ToStr(obj(0))
        ViewState("PaylastdetiNo") = Generic.ToStr(obj(1))
        'lblDetl.Text = Generic.ToStr(obj(1))
        Response.Redirect("PayLastEntitledList_Loan.aspx?Id=" & ViewState("PayNo") & "&PaylastdetiNo=" & ViewState("PaylastdetiNo"))

    End Sub
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/PayLastEdit.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("EPay_WebProcess_MainIncomeForwDelete", item)
                SQLHelper.ExecuteNonQuery("EPay_WebProcess_MainDeductForwDelete", item)
                SQLHelper.ExecuteNonQuery("EPay_WebProcess_LoanPaymentDelete", item)
                SQLHelper.ExecuteNonQuery("EPayLastDeti_WebDelete", item)
                Generic.DeleteRecordAuditCol("EPayBonusEntitled", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayLastEntitled", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayLastDeti", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayBonusDeti", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayLeaveDeti", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("ELeaveCredit", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainDeductOther", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainIncomeOther", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainDeduct", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMainIncome", UserNo, "PayNo", item)
                Generic.DeleteRecordAuditCol("EPayMain", UserNo, "PayNo", item)
                Generic.DeleteRecordAudit("EPay", UserNo, item)
                i = i + 1
            Next
            PopulateGrid(True)
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
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
            PopulateGrid(True)
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccesPost, Me)
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
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, process_status, "EPay", "EPayLast_WebProcess", 2, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Critical(process_status, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPay", "EPayLast_WebProcess", 2, ViewState("Id"))
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
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, strx, "EPay", "EPayLast_WebProcess", 2, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Critical(strx, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPay", "EPayLast_WebProcess", 2, ViewState("Id"))
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
            cmd.CommandText = "EpayLast_WebProcess"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = AssynChronous.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            cmd.CommandTimeout = 0
            cmd.Parameters.Add("@UserNo", SqlDbType.Int, 4)
            cmd.Parameters("@UserNo").Value = Generic.ToInt(UserNo)
            cmd.Parameters.Add("@payNo", SqlDbType.Int, 4)
            cmd.Parameters("@payNo").Value = id
            process_status = AssynChronous.xRunCommandAsynchronous(cmd, "EpayLast_WebProcess", SQLHelper.ConSTRAsyn, 0, err_num)
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

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecord() As Boolean
        Dim PayPeriod As Integer = Generic.ToInt(Me.txtPayperiod.Text)
        Dim ApplicableYear As Integer = Generic.ToInt(Me.txtApplicableYear.Text)
        Dim ApplicableMonth As Integer = Generic.ToInt(Me.cboApplicableMonth.Text)
        Dim PEPeriodNo As Integer = 0
        Dim noofmonthstoAssume As Double = 0
        Dim PayCateNo As Integer = 2

        Dim PayClassNo As Integer = Generic.ToInt(Me.cboPayClassNo.SelectedValue)
        If SQLHelper.ExecuteNonQuery("EPayBonus_WebSave", UserNo, Generic.ToInt(hifPayNo.Value), txtStartDate.Text, txtEndDate.Text, _
                                     txtPayDate.Text, PayClassNo, PayCateNo, txtIsDeductTax.Checked, txtIsIncludeForw.Checked, _
                                     txtIsIncludeLoan.Checked, txtIsActivateDed.Checked, txtIsPaymentSuspended.Checked, _
                                     noofmonthstoAssume, PEPeriodNo, _
                                     txtIsIncludeOther.Checked, txtIsIncludeMass.Checked, PayLocNo, 0) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False

        End If

    End Function

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

#End Region


#Region "********Detail********"
    Protected Sub lnkEditD_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo", "PayLastDetiNo", "EmployeeNo"})
            ViewState("PayNo") = Generic.ToInt(obj(0))
            ViewState("PayLastDetiNo") = Generic.ToInt(obj(1))
            ViewState("EmployeeNo") = Generic.ToInt(obj(2))

            Session("xMenuType") = "0522000000"
            Session("PayLastList_EmployeeNo") = ViewState("EmployeeNo")
            Session("PayLastList_PayNo") = ViewState("PayNo")
            'Response.Redirect("~/secured/PayLastDetiEdit.aspx?Id=" & Generic.ToInt(ViewState("PayLastDetiNo")) & "&PayNo=" & Generic.ToInt(ViewState("PayNo")))
            Response.Redirect("~/secured/PayLastDTRList.aspx?Id=" & Generic.ToInt(ViewState("PayLastDetiNo")) & "&PayNo=" & Generic.ToInt(ViewState("PayNo")))
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkAddD_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            If Generic.ToInt(ViewState("PayNo")) > 0 And Generic.ToInt(Session("PayLastList_PayclassNo")) > 0 Then
                'Session("xMenuType") = "0522000000"
                'Response.Redirect("~/secured/PayLastDetiEdit.aspx?Id=0&PayNo=" & Generic.ToInt(ViewState("PayNo")))
                mdlShow.Show()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkDeleteD_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PayLastDetiNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EPayDTR", UserNo, "PayLastDetiNo", item)
                Generic.DeleteRecordAuditCol("EPayDTRManual", UserNo, "PayLastDetiNo", item)
                Generic.DeleteRecordAuditCol("EPayBonusDeti", UserNo, "PayLastDetiNo", item)
                Generic.DeleteRecordAuditCol("EPayLeaveDeti", UserNo, "PayLastDetiNo", item)
                Generic.DeleteRecordAuditCol("ELeaveCredit", UserNo, "PayLastDetiNo", item)
                Generic.DeleteRecordAuditCol("EPayLastEntitledLoan", UserNo, "PayLastDetiNo", item)
                Generic.DeleteRecordAudit("EPayLastDeti", UserNo, item)
                i = i + 1
            Next
            PopulateGridDetl()
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    'Submit record
    Protected Sub btnSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim PayLastDetiNo As Integer = Generic.ToInt(Me.txtPayLastDetiNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim IsIncludeLeavebalance As Boolean = Generic.ToBol(txtIsIncludeLeavebalance.Checked)
        Dim PayNo As Integer = Generic.ToInt(ViewState("PayNo"))

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EPayLastDeti_WebValidate", UserNo, PayLastDetiNo, PayNo, EmployeeNo, IsIncludeLeavebalance)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlShow.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EPayLastDeti_WebSave", UserNo, PayLastDetiNo, PayNo, EmployeeNo, IsIncludeLeavebalance) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub lnkExportD_Click(sender As Object, e As EventArgs)
        Try
            grdExportD.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub

    Protected Sub grdDetl_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdDetl.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdDetl.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

#End Region


#Region "********Reports********"

    Protected Sub grdMain_FillContextMenuItems(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuEventArgs)
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

#End Region


#Region "********Reports Detail********"

    Protected Sub grdDetl_FillContextMenuItems(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuEventArgs)
        If e.MenuType = GridViewContextMenuType.Rows Then
            'e.Items.Add(e.CreateItem("Get Key", "GetKey"))
            e.Items.Clear()
        End If
    End Sub

    Protected Sub lnkResignRpt_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        'Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
        Dim id As Integer = grdDetl.GetRowValues(Integer.Parse(hf1("VisibleIndex").ToString()), "PayLastDetiNo")

        Dim EmpNo As Integer = 0
        Dim PayNo As Integer = 0
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayLastDeti_WebOne", UserNo, id)
        For Each row As DataRow In dt.Rows
            EmpNo = Generic.ToStr(row("EmployeeNo"))
            PayNo = Generic.ToStr(row("PayNo"))
        Next

        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, PayLocNo.ToString), _
                                                  New ReportParameter(ReportParameter.Type.int, "1"), _
                                                  New ReportParameter(ReportParameter.Type.int, EmpNo.ToString), _
                                                  New ReportParameter(ReportParameter.Type.int, PayNo.ToString()))
        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=136&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

#End Region



    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function cboEmployee(prefixText As String, count As Integer, contextKey As String) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()
        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))
        Dim payclassNo As Integer = (HttpContext.Current.Session("PayLastList_PayclassNo"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AC_PayClass", UserNo, prefixText, payclassNo, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

End Class
