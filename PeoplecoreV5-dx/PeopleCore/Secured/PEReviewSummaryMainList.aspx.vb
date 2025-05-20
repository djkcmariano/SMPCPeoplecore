Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web


Partial Class Secured_PEReviewSummaryMainList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim tabOrder As Integer = 0
    Dim tstatus As Integer = 0

    Dim xBase As New clsBase.clsBaseLibrary
    Dim IsCompleted As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            cboTabNo.Text = Generic.ToStr(Session("TabNo_PESummaryNo"))
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

        tabOrder = Generic.ToInt(cboTabNo.SelectedValue)

        If tabOrder = 0 Then
            tabOrder = 1
        End If

        If tabOrder <> 1 Then
            lnkAdd.Enabled = False
            lnkDelete.Enabled = False
            lnkPost.Enabled = False
            lnkProcess.Enabled = False
        End If

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPEReviewSummaryMain_Web", UserNo, "", tabOrder, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        Session("TabNo_PESummaryNo") = tabOrder

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            'Generic.ClearControls(Me, "pnlPopup")
            'Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)

            'cboPEReviewMainNo.Enabled = True
            'cboPEReviewMainNo.CssClass = "form-control required"
            'lblpereviewno.Attributes.Add("class", "col-md-4 control-label has-required")

            'lnkSave.Enabled = True
            'mdlShow.Show()


            Dim URL As String
            URL = Generic.GetFirstTab("0")
            If URL <> "" Then
                Response.Redirect(URL)
            End If
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

    Private Sub EnabledControls(Optional IsEnabled As Boolean = False)

        If IsEnabled = True Then
            If txtIsManual.Checked = True Then
                cboPEReviewMainNo.Enabled = False
                cboPEReviewMainNo.CssClass = "form-control"
                lblpereviewno.Attributes.Add("class", "col-md-4 control-label has-space")
            Else
                cboPEReviewMainNo.Enabled = True
                cboPEReviewMainNo.CssClass = "form-control required"
                lblpereviewno.Attributes.Add("class", "col-md-4 control-label has-required")
            End If
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then

                'Dim lnk As New LinkButton, i As Integer
                'lnk = sender
                'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                'i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEReviewSummaryMainNo"}))

                ''Clear Data
                'Generic.ClearControls(Me, "pnlPopup")

                ''Popuplate Data
                'Dim dt As DataTable
                'dt = SQLHelper.ExecuteDataTable("EPEReviewSummaryMain_WebOne", UserNo, Generic.ToInt(i))
                'For Each row As DataRow In dt.Rows
                '    Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
                '    Generic.PopulateData(Me, "pnlPopup", dt)
                'Next

                ''Enabled or Disabled Controls
                'Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
                'Generic.EnableControls(Me, "pnlPopup", IsEnabled)
                'lnkSave.Enabled = IsEnabled
                'EnabledControls(IsEnabled)

                'mdlShow.Show()

                Dim lnk As New LinkButton
                lnk = sender
                Dim URL As String
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEReviewSummaryMainNo"})))
                If URL <> "" Then
                    Response.Redirect(URL)
                End If

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(Me.txtPEReviewSummaryMainNo.Text)
        Dim PEReviewMainNo As Integer = Generic.ToInt(cboPEReviewMainNo.SelectedValue) '  0
        Dim PENormsNo As Integer = Generic.ToInt(Me.cboPENormsNo.SelectedValue)
        Dim Applicableyear As Integer = Generic.ToInt(Me.txtApplicableyear.Text)
        Dim PEPeriodNo As Integer = Generic.ToInt(Me.cboPEPeriodNo.SelectedValue)
        Dim isManual As Boolean = Generic.ToBol(txtIsManual.Checked)
        Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)

        If SQLHelper.ExecuteNonQuery("EPEReviewSummaryMain_WebSave", UserNo, tno, PEReviewMainNo, PENormsNo, Applicableyear, PEPeriodNo, isManual, Remarks, PayLocNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEReviewSummaryMainNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EPEReviewSummary", UserNo, "PEReviewSummaryMainNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EPEReviewSummaryMain", UserNo, CType(item, Integer))
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
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEReviewSummaryMainNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("EPEReviewSummaryMain_WebPost", UserNo, CType(item, Integer)) Then
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
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEReviewSummaryMainNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                ViewState("Id") = CType(item, Integer)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount = 1 Then
                PEReviewSummaryAppendAsyn()
                PopulateGrid()
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

    Private Sub PEReviewSummaryAppendAsyn()
        Dim xcmdProcSAVE As SqlClient.SqlCommand

        Try

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand

            xcmdProcSAVE.CommandText = "EPEReviewSummaryMain_WebProcess"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = xBase.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0

            xcmdProcSAVE.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@onlineuserno").Value = Generic.CheckDBNull(UserNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            xcmdProcSAVE.Parameters.Add("@PEReviewSummaryMainNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@PEReviewSummaryMainNo").Value = Generic.CheckDBNull(ViewState("Id"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            xBase.RunCommandAsynchronous(xcmdProcSAVE, "EPEReviewSummaryMain_WebProcess", SQLHelper.ConSTRAsyn, IsCompleted)
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
        i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEReviewSummaryMainNo"}))

        Response.Redirect("~/secured/PEReviewSummaryDetiList.aspx?transNo=" & i & "&tModify=false&IsClickMain=1")

    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub


    '#Region "********Reports********"

    '    Protected Sub MyGridView_FillContextMenuItems(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuEventArgs)
    '        If e.MenuType = GridViewContextMenuType.Rows Then
    '            'e.Items.Add(e.CreateItem("Get Key", "GetKey"))
    '            e.Items.Clear()
    '        End If
    '    End Sub

    '    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
    '        Dim lnk As LinkButton = TryCast(sender, LinkButton)
    '        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
    '        NewScriptManager.RegisterPostBackControl(lnk)
    '    End Sub

    '    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
    '        Dim lnk As New LinkButton
    '        Dim sb As New StringBuilder
    '        lnk = sender
    '        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
    '        'Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
    '        Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "PEReviewSummaryMainNo")
    '        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, PayLocNo.ToString), _
    '                                                  New ReportParameter(ReportParameter.Type.int, id.ToString()), _
    '                                                  New ReportParameter(ReportParameter.Type.int, "0"))
    '        sb.Append("<script>")
    '        sb.Append("window.open('rpttemplateviewer.aspx?reportno=405&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
    '        sb.Append("</script>")
    '        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    '    End Sub
    '#End Region



End Class

















