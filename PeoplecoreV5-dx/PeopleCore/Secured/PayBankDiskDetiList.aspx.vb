Imports System.Data
Imports System.Math
Imports System.Threading
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.Data.SqlClient

Imports System.IO
Imports Microsoft.VisualBasic

Partial Class Secured_PayBankDiskDetiList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim IsCompleted As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)

        Dim tstatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
        
        PayHeader.ID = Generic.ToInt(TransNo)

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayBankDisk_Web", UserNo, tstatus, TransNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If ViewState("TransNo") = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"PayBankDiskNo", "Code", "PayClassNo"})
            ViewState("TransNo") = obj(0)
            lblDetl.Text = obj(1)
            Session("paybankdisk_payclassno") = Generic.ToInt(obj(2))
        End If

        PopulateGridDetl()

    End Sub

    Private Sub PopulateGridDetl()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayBankDiskDeti_Web", UserNo, ViewState("TransNo"))
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 34)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        If TransNo > 0 Then
            Session("PayBankDiskDetiList_PayNo") = TransNo
        End If
        If Session("PayBankDiskDetiList_PayNo") > 0 Then
            TransNo = Session("PayBankDiskDetiList_PayNo")
        End If

        AccessRights.CheckUser(UserNo, "PayBankDiskList.aspx", "EPayBankDisk")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            PopulateDropDown()
            EnabledControls()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateGridDetl()
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid(True)
    End Sub

    Private Sub EnabledControls()
        Dim IsPosted As Boolean = False
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayBank_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            IsPosted = Generic.ToBol(row("IsPosted"))
        Next

        If IsPosted = True Then
            lnkAddD.Visible = False
            lnkDeleteD.Visible = False
        Else
            lnkAddD.Visible = True
            lnkDeleteD.Visible = True
        End If
    End Sub

#Region "********Main*******"

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBankDiskNo", "Code", "PayClassNo", "IsEnabled"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        Session("paybankdisk_payclassno") = Generic.ToInt(obj(2))
        ViewState("IsEnabled") = Generic.ToBol(obj(3))
        PopulateGridDetl()

    End Sub

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

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "PayBankDiskList.aspx", "EPayBankDisk") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayBankDiskNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EPayBankDiskDeti", UserNo, "PayBankDiskNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EPayBankDisk", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid(True)
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
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim PayBankDiskNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBankDiskNo"}))
            Dim PaymentTypeNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PaymentTypeNo"}))

            If PaymentTypeNo = 3 Then
                Response.Redirect("~/secured/PayBankDiskEdit.aspx?id=" & TransNo & "&PayBankDiskNo=" & PayBankDiskNo)
            Else
                MessageBox.Information("Cannot edit transaction. Applicable for bank only.", Me)
            End If


        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PayBankDiskList.aspx", "EPayBankDisk") Then
            Response.Redirect("~/secured/PayBankDiskEdit.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Count As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost, "PayBankDiskList.aspx", "EPayBankDisk") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayBankDiskNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("zTable_WebPost", UserNo, "EPayBankDisk", CType(item, Integer)) Then
                    Count = Count + 1
                End If
            Next

            If Count > 0 Then
                PopulateGrid(True)
                MessageBox.Success("(" + Count.ToString + ") " + MessageTemplate.SuccesPost, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub

    Protected Sub lnkProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Count As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess, "PayBankDiskList.aspx", "EPayBankDisk") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayBankDiskNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                ViewState("Id") = CType(item, Integer)
                Count = Count + 1
            Next

            If Count = 1 Then
                Bank_AppendAsyn()
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessProcess & " " & Now().ToString, Me)
            ElseIf Count > 1 Then
                MessageBox.Warning("Please select 1 transaction to process.", Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If
    End Sub

    Private Sub Bank_AppendAsyn()
        Dim xcmdProcSAVE As SqlClient.SqlCommand

        Try
            'clsbase.OpenConnectionAsyn()

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand
            '
            xcmdProcSAVE.CommandText = "EPayBankDisk_WebProcess"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = AssynChronous.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0

            xcmdProcSAVE.Parameters.Add("@PayBankDiskNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@PayBankDiskNo").Value = Generic.ToInt(ViewState("Id"))

            xcmdProcSAVE.Parameters.Add("@UserNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@UserNo").Value = UserNo

            AssynChronous.RunCommandAsynchronous(xcmdProcSAVE, "EPayBankDisk_WebProcess", SQLHelper.ConSTRAsyn, IsCompleted)
            Session("IsCompleted") = IsCompleted


        Catch

        End Try

    End Sub

#End Region


#Region "********Detail********"

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub
    Protected Sub lnkDeleteD_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "PayBankDiskList.aspx", "EPayBankDisk") Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PayBankDiskDetiNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayBankDiskDeti", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                PopulateGridDetl()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Private Sub PopulateData(id As Integer)
        Generic.ClearControls(Me, "Panel2")
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayBankDiskDeti_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
        For Each row As DataRow In dt.Rows
            Dim IsEnabled As Boolean = Generic.ToBol(row("IsEnabled"))
            Generic.EnableControls(Me, "Panel2", IsEnabled)
            txtFullName.Enabled = False
            lnkSave.Enabled = IsEnabled
        Next
    End Sub
    Protected Sub lnkEditD_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBankDiskDetiNo"}))
            PopulateData(id)
            mdlMain.Show()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkAddD_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PayBankDiskList.aspx", "EPayBankDisk") Then
            If ViewState("TransNo") = 0 Then
                MessageBox.Information("Please select transaction.", Me)
                Exit Sub
            Else
                Generic.ClearControls(Me, "Panel2")
                Generic.EnableControls(Me, "Panel2", True)
                lnkSave.Enabled = True
                mdlMain.Show()
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable
        Dim retVal As Boolean = False
        Dim tno As Integer = Generic.ToInt(txtPayBankDiskDetiNo.Text)
        Dim employeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim AmountManual As Decimal = Generic.ToDec(txtAmountManual.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EPayBankDiskDeti_WebValidate", UserNo, tno, ViewState("TransNo"), employeeNo, AmountManual)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlMain.Show()
            Exit Sub
        End If

        dt = SQLHelper.ExecuteDataTable("EPayBankDiskDeti_WebSave", UserNo, tno, ViewState("TransNo"), employeeNo, AmountManual, Remarks)
        Dim error_num As Integer = 0, error_message As String = ""
        For Each row As DataRow In dt.Rows
            retVal = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                retVal = False
            End If
        Next

        If retVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If retVal = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
        PopulateGrid()
        PopulateGridDetl()

    End Sub

    Protected Sub grdDetl_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdDetl.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdDetl.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub
    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function cboEmployee(prefixText As String, count As Integer, contextKey As String) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()
        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))
        Dim payclassNo As Integer = (HttpContext.Current.Session("paybankdisk_payclassno"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AC_PayClass", UserNo, prefixText, payclassNo, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function


    Protected Sub lnkCreateDisk_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim PayBankDiskNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBankDiskNo"}))
            Dim PaymentTypeNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PaymentTypeNo"}))
            Dim PayClassNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassNo"}))
            Dim BankTypeNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BankTypeNo"}))
            Dim PayOutSchedNo As Integer = Generic.ToInt(lnk.CommandArgument) 'Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayOutSchedNo"}))

            If PaymentTypeNo = 3 Then
                GeneratepayrollDisk_PNB(PayBankDiskNo, PayClassNo, BankTypeNo, PayOutSchedNo)
            Else
                MessageBox.Information("Cannot edit transaction. Applicable for bank only.", Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

#End Region


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
        Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "PayBankDiskNo")

        Dim param As String = ""
        Dim tProceed As Boolean = False
        Dim ReportNo As Integer = 0, dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EReport_WebViewer", UserNo, 29, "", id, PayLocNo)
        For Each row As DataRow In dt.Rows
            ReportNo = Generic.ToInt(row("ReportNo"))
            param = Generic.ToStr(row("param"))
            tProceed = Generic.ToStr(row("tProceed"))
        Next

        If tProceed = True Then
            sb.Append("<script>")
            sb.Append("window.open('rpttemplateviewer.aspx?reportno=" & ReportNo & "&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
            sb.Append("</script>")
            ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
        Else
            MessageBox.Warning("No access permission to view the report.", Me)
        End If
    End Sub


    'Protected Sub lnkUnionBank_Click(sender As Object, e As EventArgs)
    '    Dim lnk As New LinkButton
    '    Dim sb As New StringBuilder
    '    lnk = sender
    '    Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
    '    Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "PayBankDiskNo")
    '    Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, PayLocNo.ToString), _
    '                                              New ReportParameter(ReportParameter.Type.int, "0"), _
    '                                              New ReportParameter(ReportParameter.Type.int, "0"), _
    '                                              New ReportParameter(ReportParameter.Type.int, id.ToString()), _
    '                                              New ReportParameter(ReportParameter.Type.int, "0"))
    '    sb.Append("<script>")
    '    sb.Append("window.open('rpttemplateviewer.aspx?reportno=404&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
    '    sb.Append("</script>")
    '    ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    'End Sub

#End Region

    Private Sub OpenText(ByVal fullpath As String)
        Dim FileName As String = ""
        FileName = IO.Path.GetFileName(fullpath)
        Response.Clear()
        Response.ClearContent()
        Response.ContentType = "application/octet-stream"
        Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
        Response.TransmitFile(fullpath)
        Response.End()
    End Sub


#Region "********PNB"

    Private Sub GeneratepayrollDisk_PNB(ByVal PayBankDiskNo As Integer, ByVal PayClassNo As Integer, ByVal BankTypeNo As Integer, ByVal SchedNo As Integer)

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim tpath As String = Page.MapPath("Disk") '"c:\Payroll Diskette"
        Dim xfilename As String '= path & "\" & "SSSLoan-" & Format(Now, "MMMMdd") & ".TXT"
        Dim dstext As DataSet
        Dim sqlhelp As New clsBase.SQLHelper
        Dim xftotalS As Double, xPABranchCode As String, xAccountNo As String, xEffectivityDate As String, xEffectivityDated As String
        Dim sdate As Integer
        Dim xFxTotalT As String = ""
        Dim xFHeadCount As Integer = 0
        Dim fBranchCode As String = ""
        Dim xFxTotal As String = ""
        Dim xCompanyCode As String = ""
        Dim xBatchNo As String = ""
        Dim xcompanyaccountno As String = ""

        Dim xBankCode As String = ""
        Dim xCompanyName As String = ""

        Dim xAmount As String
        Dim faccountno As String = ""
        Dim tnet As Double = 0
        Dim Lastname As String, Firstname As String, Middlename As String
        Dim PayDate1 As String, PayDate2 As String, PayDate3 As String, PayDate4 As String
        Dim xPayDate As String

        Try

            Dim dsComp As New DataSet
            dsComp = SQLHelper.ExecuteDataSet("EPayBankDiskRef_WebOne_PayClass", UserNo, Generic.ToInt(PayClassNo), Generic.ToInt(BankTypeNo))
            If dsComp.Tables.Count > 0 Then
                If dsComp.Tables(0).Rows.Count > 0 Then
                    'xPABranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_PayrollAccount"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    'xAccountNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("AccountNumber"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    'xEffectivityDated = FormatDateTime(dsComp.Tables(0).Rows(0)("EffectiveDate"), DateFormat.ShortDate)
                    'xEffectivityDate = Pad.PadZero(2, Month(xEffectivityDated)) & "/" & Pad.PadZero(2, Day(xEffectivityDated)) & "/" & Pad.PadZero(4, Year(xEffectivityDated))
                    'sdate = Day(xEffectivityDated)
                    'fBranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_Company"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xCompanyCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("CompanyCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xBatchNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BatchNo"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    'xcompanyaccountno = xAccountNo
                    'xBankCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BankCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    PayDate1 = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("xPayDate1"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    PayDate2 = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("xPayDate2"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    PayDate3 = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("xPayDate3"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    PayDate4 = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("xPayDate4"), clsBase.clsBaseLibrary.enumObjectType.StrType)

                    If PayDate1 > "" And SchedNo = 1 Then
                        xPayDate = PayDate1
                    ElseIf PayDate2 > "" And SchedNo = 2 Then
                        xPayDate = PayDate2
                    ElseIf PayDate3 > "" And SchedNo = 3 Then
                        xPayDate = PayDate3
                    ElseIf PayDate4 > "" And SchedNo = 4 Then
                        xPayDate = PayDate4
                    Else
                        xPayDate = PayDate1
                    End If

                    xCompanyCode = Pad.PadZero(5, xCompanyCode) + "_" + Pad.PadZero(2, xBatchNo) + "_" + Pad.PadZero(2, SchedNo) + "_" + xPayDate

                End If
            End If

            dsComp = Nothing

            xfilename = tpath & "\" & xCompanyCode & "_PNB.txt"
            FileHolder = New FileInfo(xfilename)
            WriteFile = FileHolder.CreateText()


            Dim xDisk As String = ""

            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPayBankDiskDeti_WebCreateDisk_PNB_Indi", Generic.ToInt(PayBankDiskNo), Generic.ToInt(PayClassNo), Generic.ToInt(BankTypeNo), Generic.ToInt(SchedNo))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        xFHeadCount = xFHeadCount + 1
                        xDisk = Generic.CheckDBNull(ds.Tables(0).Rows(i)("Disk"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                        WriteFile.WriteLine(xDisk)
                    Next
                End If
            End If

            ds = Nothing


            WriteFile.Close()
            OpenText("../secured/Disk/" & xCompanyCode & "_PNB.txt")

        Catch ex As Exception
            WriteFile.Close()
        End Try

    End Sub


#End Region

End Class
