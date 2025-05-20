Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web


Partial Class Secured_DTR
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim tabOrder As Integer = 0
    Dim tstatus As Integer = 0

    Dim xBase As New clsBase.clsBaseLibrary
    Dim IsCompleted As Integer = 0
    Dim process_status As String = ""
    Dim err_num As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateDropDown()

        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 1)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")

        lnkAdd.Enabled = True
        lnkDelete.Enabled = True
        lnkPost.Enabled = True
        lnkProcess.Enabled = True
        lnkProcessDisc.Visible = False

        tabOrder = Generic.ToInt(cboTabNo.SelectedValue)

        If tabOrder = 0 Then
            tabOrder = 1
        End If

        If tabOrder <> 1 Then
            lnkAdd.Enabled = False
            lnkDelete.Enabled = False
            lnkPost.Enabled = False
            lnkProcess.Enabled = False
            lnkProcessDisc.Visible = False
        End If

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EDTR_Web", UserNo, tabOrder, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopup")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
            Try
                cboPayClassNo.DataSource = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", UserNo, PayLocNo)
                cboPayClassNo.DataValueField = "tNo"
                cboPayClassNo.DataTextField = "tDesc"
                cboPayClassNo.DataBind()
            Catch ex As Exception
            End Try
            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then

                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRNo"}))
                Generic.ClearControls(Me, "pnlPopup")

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EDTR_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
                    Generic.PopulateData(Me, "pnlPopup", dt)
                    Try
                        cboPayClassNo.DataSource = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", UserNo, PayLocNo)
                        cboPayClassNo.DataValueField = "tNo"
                        cboPayClassNo.DataTextField = "tDesc"
                        cboPayClassNo.DataBind()
                    Catch ex As Exception
                    End Try

                Next
                Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
                Generic.EnableControls(Me, "pnlPopup", IsEnabled)
                btnSave.Enabled = IsEnabled

                mdlShow.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim DTRNo As Integer = Generic.ToInt(txtDTRNo.Text)
        Dim PayTypeNo As Integer = Generic.ToInt(cboPayTypeNo.SelectedValue)
        Dim PayClassNo As Integer = Generic.ToInt(cboPayClassNo.SelectedValue)
        Dim StartDate As String = Generic.ToStr(txtStartDate.Text)
        Dim EndDate As String = Generic.ToStr(txtEndDate.Text)
        Dim PayStartdate As String = Generic.ToStr(txtPayStartDate.Text)
        Dim PayEndDate As String = Generic.ToStr(txtPayEndDate.Text)
        Dim IsAutomated As Boolean = Generic.ToBol(txtIsautomated.Checked)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EDTR_WebValidate", UserNo, DTRNo, PayTypeNo, PayClassNo, StartDate, EndDate, PayStartdate, PayEndDate, IsAutomated, PayLocNo)

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

        If SQLHelper.ExecuteNonQuery("EDTR_WebSave", UserNo, DTRNo, PayTypeNo, PayClassNo, StartDate, EndDate, PayStartdate, PayEndDate, IsAutomated, PayLocNo) > 0 Then
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


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EDTRDetiLogAdj", UserNo, "DTRNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EDTRDetiAdj", UserNo, "DTRNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EDTRDetiLogRetro", UserNo, "DTRNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EDTRDetiRetro", UserNo, "DTRNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EDTRDetiLogDiff", UserNo, "DTRNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EDTRDetiDiff", UserNo, "DTRNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EDTRDetiDisc", UserNo, "DTRNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EDTRDetiLogDisc", UserNo, "DTRNo", CType(item, Integer))

                Generic.DeleteRecordAuditCol("EDTRDetiEdited", UserNo, "DTRNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EDTRDetiLog", UserNo, "DTRNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EDTRDeti", UserNo, "DTRNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EDTR", UserNo, CType(item, Integer))
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

    Protected Sub lnkPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("EDTR_WebPost", UserNo, CType(item, Integer)) Then
                    DeleteCount = DeleteCount + 1
                End If
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccesPost, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub
    Protected Sub lnkPostB_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("EDTR_WebPost_Billing", UserNo, CType(item, Integer)) Then
                    DeleteCount = DeleteCount + 1
                End If
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccesPost, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub


    Protected Sub lnkProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                ViewState("Id") = CType(item, Integer)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount = 1 Then
                DTRAppendAsyn()
                Dim strx As String = process_status
                If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, strx, "EDTR", "EDTR_WebProcess", 1, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Critical(strx, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EDTR", "EDTR_WebProcess", 1, ViewState("Id"))
                    PopulateGrid()
                    process_status = Replace(process_status, "Command complete. Processing Time is :", "DTR Processing completed at ")
                    MessageBox.Success(process_status, Me)
                End If
            ElseIf DeleteCount > 1 Then
                MessageBox.Warning("Please select 1 transaction to process.", Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If
    End Sub

    Private Sub DTRAppendAsyn()
        Dim xcmdProcSAVE As SqlClient.SqlCommand

        Try

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand

            xcmdProcSAVE.CommandText = "EDTR_WebProcess"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = xBase.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0

            xcmdProcSAVE.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@onlineuserno").Value = Generic.CheckDBNull(UserNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            xcmdProcSAVE.Parameters.Add("@DTRNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@DTRNo").Value = Generic.CheckDBNull(ViewState("Id"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            process_status = AssynChronous.xRunCommandAsynchronous(xcmdProcSAVE, "EDTR_WebProcess", SQLHelper.ConSTRAsyn, IsCompleted, err_num)
            Session("IsCompleted") = 0 'IsCompleted

            If Session("IsCompleted") = 1 Then
                'clsModalControls.SetModalPopupControls(CType(Master.FindControl("cphBody"), ContentPlaceHolder), "completed")
            End If
        Catch
            'Response.RedirectLocation = Session("xFormname") & "?IsClickMain=" & IsClickMain
        End Try

    End Sub

    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lnk As New LinkButton, i As Integer
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRNo"}))

        Response.Redirect("~/secured/DTRDetlList.aspx?transNo=" & i & "&tModify=false&IsClickMain=1")
        
    End Sub

    Protected Sub lnkManualDTR_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lnk As New LinkButton, i As Integer
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRNo"}))

        Response.Redirect("~/secured/DTRDetlList_Manual.aspx?transNo=" & i & "&tModify=false&IsClickMain=1")
        
    End Sub
    Protected Sub lnkEmployee_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lnk As New LinkButton, i As Integer
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRNo", "PayClassNo"})

        i = Generic.ToInt(obj(0))
        Session("PayclassNo_Pay") = Generic.ToInt(obj(1))
        Response.Redirect("~/secured/DTR_Employee.aspx?transNo=" & i & "&tModify=false&IsClickMain=1")

    End Sub

    Protected Sub lnkProcess_Detail_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRNo"}))
            ViewState("Id") = i

            Dim IsPosted As Boolean = True
            IsPosted = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsPosted"}))

            If IsPosted = False Then
                DTRAppendAsyn()
                Dim strx As String = process_status
                If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, strx, "EDTR", "EDTR_WebProcess", 1, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Critical(strx, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EDTR", "EDTR_WebProcess", 1, ViewState("Id"))
                    PopulateGrid()
                    process_status = Replace(process_status, "Command complete. Processing Time is :", "DTR Processing completed at ")
                    MessageBox.Success(process_status, Me)
                End If
            Else
                MessageBox.Warning(MessageTemplate.PostedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If

    End Sub


    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

#Region "****Discrepancy****"

    Protected Sub lnkProcessDisc_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                ViewState("Id") = CType(item, Integer)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount = 1 Then
                DTRAppendAsyn_Disc()

                MessageBox.Success(MessageTemplate.SuccessProcess & " " & Now().ToString, Me)
            ElseIf DeleteCount > 1 Then
                MessageBox.Warning("Please select 1 transaction to process.", Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If
    End Sub


    Private Sub DTRAppendAsyn_Disc()
        Dim xcmdProcSAVE As SqlClient.SqlCommand
        Dim clsbase As New clsBase.clsBaseLibrary

        Try
            'clsbase.OpenConnectionAsyn()

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand

            xcmdProcSAVE.CommandText = "EDTR_WebProcessDetiLogUpdateTempStringDisc_PDAY"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = AssynChronous.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0

            xcmdProcSAVE.Parameters.Add("@DTRNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@DTRNo").Value = clsbase.CheckDBNull(ViewState("Id"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            AssynChronous.RunCommandAsynchronous(xcmdProcSAVE, "EDTR_WebProcessDetiLogUpdateTempStringDisc_PDAY", SQLHelper.ConSTRAsyn, IsCompleted)
            Session("IsCompleted") = 0 'IsCompleted
        Catch

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
        'Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
        Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "DTRNo")
        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, PayLocNo.ToString), _
                                                  New ReportParameter(ReportParameter.Type.int, id.ToString()), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"))
        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=405&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub
#End Region


    Protected Sub ASPxGridViewExporter_RenderBrick(sender As Object, e As DevExpress.Web.ASPxGridViewExportRenderingEventArgs) Handles grdExport.RenderBrick
        Dim dataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
        'If e.RowType = GridViewRowType.Data AndAlso dataColumn IsNot Nothing Then
        '    Select Case dataColumn.FieldName
        '        Case "AbsHrs"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<span>", "")
        '            e.TextValue = e.TextValue.ToString.Replace("</span>", "")
        '        Case "Late"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<span>", "")
        '            e.TextValue = e.TextValue.ToString.Replace("</span>", "")
        '        Case "Under"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<span>", "")
        '            e.TextValue = e.TextValue.ToString.Replace("</span>", "")
        '    End Select

        'End If
        If e.RowType = GridViewRowType.Header AndAlso dataColumn IsNot Nothing Then
            e.Text = e.Text.Replace("<br/>", " ")
            e.Text = e.Text.Replace("<br>", " ")
            e.Text = e.Text.Replace("<center>", "")
            e.Text = e.Text.Replace("</center>", "")
        End If

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

End Class
