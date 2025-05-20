Imports System.Data
Imports System.Math
Imports System.Threading
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PayAlphaList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim IsCompleted As Integer = 0
    Dim process_status As String = ""
    Dim err_num As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)

        Dim tstatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EAlpha_Web", UserNo, tstatus, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If ViewState("TransNo") = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"AlphaNo", "Code"})
            ViewState("TransNo") = obj(0)
            lblDetl.Text = obj(1)
        End If

        'PopulateGridDetl()

    End Sub

    Private Sub PopulateGridDetl()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EAlphaDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 4)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception

        End Try

        Try
            Me.cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup", UserNo, PayLocNo)
            Me.cboPayLocNo.DataTextField = "tDesc"
            Me.cboPayLocNo.DataValueField = "tNO"
            Me.cboPayLocNo.DataBind()
        Catch ex As Exception

        End Try

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

        PopulateGridDetl()
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid(True)
    End Sub


#Region "********Main*******"

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"AlphaNo", "Code"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        PopulateGridDetl()

    End Sub
    Protected Sub lnkExemption_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"AlphaNo", "Code"})
        ViewState("TransNo") = obj(0)
        Response.Redirect("PayAlphaList_Exempt.aspx?Id=" & ViewState("TransNo") & "&Code=" & obj(1))

    End Sub

    Protected Sub cboPayLocNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayLoc_WebOne", UserNo, Generic.ToInt(cboPayLocNo.SelectedValue))
        For Each row As DataRow In dt.Rows
            txtMaxAmtAccumulatedExemp.Text = Generic.ToDec(row("MaxAmtAccumulatedExemp"))
        Next
        mdlMain.Show()
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

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"AlphaNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EAlphaDeti", UserNo, "AlphaNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EAlpha", UserNo, CType(item, Integer))
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
                Dim lnk As New LinkButton, i As Integer, IsEnabled As Boolean = False
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"AlphaNo", "IsEnabled"})
                i = Generic.ToInt(obj(0))
                IsEnabled = Generic.ToBol(obj(1))

                'Generic.ClearControls(Me, "pnlPopupMain")
                'Dim dt As DataTable
                'dt = SQLHelper.ExecuteDataTable("EAlpha_WebOne", UserNo, Generic.ToInt(i))
                'For Each row As DataRow In dt.Rows
                '    Generic.PopulateData(Me, "pnlPopupMain", dt)
                'Next
                'Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)
                'lnkSave.Enabled = IsEnabled

                'mdlMain.Show()
                Response.Redirect("PayAlphaList_Edit.aspx?Id=" & Generic.ToInt(i))

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            'Generic.ClearControls(Me, "pnlPopupMain")
            'Generic.EnableControls(Me, "pnlPopupMain", True)
            'lnkSave.Enabled = True
            'mdlMain.Show()
            Response.Redirect("PayAlphaList_Edit.aspx?Id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    'Submit record
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtAlphaNo.Text)
            Dim xPayLocNo As Integer = Generic.ToInt(Me.cboPayLocNo.SelectedValue)
            Dim ApplicableMonth As Integer = Generic.ToInt(Me.cboApplicableMonth.SelectedValue)
            Dim ApplicableYear As Integer = Generic.ToInt(Me.txtApplicableYear.Text)
            Dim signatoryNo As Integer = Generic.ToInt(Me.hifsignatoryNo.Value)
            Dim signatoryno2 As Integer = Generic.ToInt(Me.hifsignatoryno2.Value)
            Dim AlphaDesc As String = Generic.ToStr(Me.txtAlphaDesc.Text)
            Dim FacilityNo As Integer = Generic.ToInt(Me.cboFacilityNo.SelectedValue)
            Dim MaxAmtAccumulatedExemp As Double = Generic.ToDec(Me.txtMaxAmtAccumulatedExemp.Text)

            Dim dt As DataTable
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            dt = SQLHelper.ExecuteDataTable("EAlpha_WebValidate", UserNo, tno, ApplicableYear, ApplicableMonth, AlphaDesc, xPayLocNo, PayLocNo, FacilityNo)
            For Each row As DataRow In dt.Rows
                invalid = Generic.ToBol(row("Invalid"))
                messagedialog = Generic.ToStr(row("MessageDialog"))
                alerttype = Generic.ToStr(row("AlertType"))
            Next

            If invalid = True Then
                mdlMain.Show()
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If

            If SQLHelper.ExecuteNonQuery("EAlpha_WebSave", UserNo, tno, ApplicableYear, ApplicableMonth, AlphaDesc, xPayLocNo, signatoryNo, signatoryno2, PayLocNo, FacilityNo, MaxAmtAccumulatedExemp) > 0 Then
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

    Protected Sub lnkPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Count As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"AlphaNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("zTable_WebPost", UserNo, "EAlpha", CType(item, Integer)) Then
                    Count = Count + 1
                End If
            Next

            If Count > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + Count.ToString + ") " + MessageTemplate.SuccesPost, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub

    Protected Sub lnkProcess_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
                Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"AlphaNo"})
                If fieldValues.Count > 1 Or fieldValues.Count = 0 Then
                    MessageBox.Warning("Please select 1 transaction to process.", Me)
                    Exit Sub
                End If
                If fieldValues.Count = 1 Then
                    For Each item As Integer In fieldValues
                        ViewState("Id") = item
                        Alpha_AppendAsyn(item)
                    Next
                End If

                If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, process_status, "EAlpha", "EAlpha_WebProcess", 3, ViewState("Id"))
                    PopulateGridDetl()
                    MessageBox.Critical(process_status, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EAlpha", "EAlpha_WebProcess", 3, ViewState("Id"))
                    PopulateGridDetl()
                    process_status = Replace(process_status, "Command complete. Processing Time is :", "Processing completed at ")
                    MessageBox.Success(process_status, Me)
                End If
            Else
                MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkConso_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
                Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"AlphaNo"})
                If fieldValues.Count > 1 Or fieldValues.Count = 0 Then
                    MessageBox.Warning("Please select 1 transaction to Consolidate.", Me)
                    Exit Sub
                End If
                If fieldValues.Count = 1 Then
                    For Each item As Integer In fieldValues
                        ViewState("Id") = item
                        Alpha_AppendConsoAsyn(item)
                    Next
                End If

                If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, process_status, "EAlpha", "EAlpha_WebConso", 3, ViewState("Id"))
                    PopulateGridDetl()
                    MessageBox.Critical(process_status, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EAlpha", "EAlpha_WebConso", 3, ViewState("Id"))
                    PopulateGridDetl()
                    process_status = Replace(process_status, "Command complete. Consolidation Time is :", "Consolidation completed at ")
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
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"AlphaNo"}))
            ViewState("Id") = i

            Dim IsPosted As Boolean = True
            IsPosted = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsPosted"}))

            If IsPosted = False Then
                Alpha_AppendAsyn(i)
                Dim strx As String = process_status
                If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, strx, "EAlpha", "EAlpha_WebProcess", 3, ViewState("Id"))
                    PopulateGridDetl()
                    MessageBox.Critical(strx, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EAlpha", "EAlpha_WebProcess", 3, ViewState("Id"))
                    PopulateGridDetl()
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

    Private Sub Alpha_AppendAsyn(id As Integer)
        Dim xcmdProcSAVE As SqlClient.SqlCommand

        Try
            'clsbase.OpenConnectionAsyn()

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand
            '
            xcmdProcSAVE.CommandText = "EAlpha_WebProcess"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = AssynChronous.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0


            xcmdProcSAVE.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@onlineuserno").Value = UserNo

            xcmdProcSAVE.Parameters.Add("@AlphaNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@AlphaNo").Value = Generic.ToInt(id)

            'AssynChronous.RunCommandAsynchronous(xcmdProcSAVE, "EAlpha_WebProcess", SQLHelper.ConSTRAsyn, IsCompleted)
            process_status = AssynChronous.xRunCommandAsynchronous(xcmdProcSAVE, "EAlpha_WebProcess", SQLHelper.ConSTRAsyn, IsCompleted, err_num)
            Session("IsCompleted") = 0 'IsCompleted


        Catch

        End Try

    End Sub

    Private Sub Alpha_AppendConsoAsyn(id As Integer)
        Dim xcmdProcSAVE As SqlClient.SqlCommand

        Try
            'clsbase.OpenConnectionAsyn()

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand
            '
            xcmdProcSAVE.CommandText = "EAlpha_WebConso"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = AssynChronous.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0


            xcmdProcSAVE.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@onlineuserno").Value = UserNo

            xcmdProcSAVE.Parameters.Add("@AlphaNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@AlphaNo").Value = Generic.ToInt(id)

            'AssynChronous.RunCommandAsynchronous(xcmdProcSAVE, "EAlpha_WebProcess", SQLHelper.ConSTRAsyn, IsCompleted)
            process_status = AssynChronous.xRunCommandAsynchronous(xcmdProcSAVE, "EAlpha_WebConso", SQLHelper.ConSTRAsyn, IsCompleted, err_num)
            Session("IsCompleted") = 0 'IsCompleted


        Catch

        End Try

    End Sub

#End Region


#Region "********Detail********"
    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("PayAlphaList_EditDeti.aspx?TransNo=" & ViewState("TransNo"))
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub
    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub
    Protected Sub lnkDeleteDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"AlphaDetiNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EAlphaDeti", UserNo, CType(item, Integer))
                Generic.DeleteRecordAudit("EAlphaDetiUpload", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGridDetl()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub grdDetl_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdDetl.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdDetl.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub
#End Region
#Region "***Upload***"
    Protected Sub lnkUpload_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, Session("xFormName"), Session("xTableName")) Then
            'Generic.ClearControls(Me, "Panel3")
            'ModalPopupExtender6.Show()
            Response.Redirect("~/secured/PayAlphaList_Upload.aspx?id=0&TransNo=" & ViewState("TransNo"))
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub
#End Region

#Region "********Context Menu********"

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
        Dim id As Integer, EmployeeNo As Integer
        id = Generic.ToInt(grdDetl.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "AlphaNo"))
        EmployeeNo = Generic.ToInt(grdDetl.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "EmployeeNo"))

        If ID > 0 Then
            Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, PayLocNo.ToString), _
                                                  New ReportParameter(ReportParameter.Type.int, "1"), _
                                                  New ReportParameter(ReportParameter.Type.int, EmployeeNo.ToString()), _
                                                  New ReportParameter(ReportParameter.Type.int, id.ToString()), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"))
            sb.Append("<script>")
            sb.Append("window.open('rpttemplateviewer.aspx?reportno=45&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
            sb.Append("</script>")
            ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
        End If

    End Sub


#End Region

    
End Class
