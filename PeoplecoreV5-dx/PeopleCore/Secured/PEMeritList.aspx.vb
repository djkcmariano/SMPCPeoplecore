Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PEMeritList
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
            cboTabNo.Text = Generic.ToStr(Session("TabNo_PEMeritNo"))
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
        _dt = SQLHelper.ExecuteDataTable("EPEMerit_Web", UserNo, "", tabOrder, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        Session("TabNo_PEMeritNo") = tabOrder

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub


    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/PEMeritEdit.aspx?id=0")
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
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim URL As String = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEMeritNo"})))
            If URL <> "" Then
                Response.Redirect(URL)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(Me.txtPEMeritNo.Text)
        Dim PEReviewSummaryMainLNo As Integer = Generic.ToInt(Me.cboPEReviewSummaryMainLNo.SelectedValue)
        Dim Effectivity As String = Generic.ToStr(Me.txtEffectivity.Text)
        Dim PreparedDate As String = Generic.ToStr(Me.txtPreparedDate.Text)
        Dim Remark As String = Generic.ToStr(Me.txtRemark.Text)
        Dim IsBonus As Boolean = Generic.ToBol(Me.txtIsBonus.Checked)
        Dim hrantype As Integer = Generic.ToInt(cboHRANTypeNo.SelectedValue)

        If SQLHelper.ExecuteNonQuery("EPEMerit_WebSave", UserNo, tno, PEReviewSummaryMainLNo, Effectivity, PreparedDate, IsBonus, Remark, PayLocNo, hrantype) > 0 Then
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
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEMeritNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EPEMeritEntitled", UserNo, "PEMeritNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EPEMeritTableDeti", UserNo, "PEMeritNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EPEMeritDeti", UserNo, "PEMeritNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EPEMerit", UserNo, CType(item, Integer))
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
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEMeritNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("EPEMerit_WebPost", UserNo, CType(item, Integer)) Then
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
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEMeritNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                ViewState("Id") = CType(item, Integer)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount = 1 Then
                PEMeritAppendAsyn()
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

    Private Sub PEMeritAppendAsyn()
        Dim xcmdProcSAVE As SqlClient.SqlCommand

        Try

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand

            xcmdProcSAVE.CommandText = "EPEMerit_WebProcess"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = xBase.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0

            xcmdProcSAVE.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@onlineuserno").Value = Generic.CheckDBNull(UserNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            xcmdProcSAVE.Parameters.Add("@PEMeritNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@PEMeritNo").Value = Generic.CheckDBNull(ViewState("Id"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            xBase.RunCommandAsynchronous(xcmdProcSAVE, "EPEMerit_WebProcess", SQLHelper.ConSTRAsyn, IsCompleted)
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
        i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEMeritNo"}))

        Response.Redirect("~/secured/PEMeritDetiList.aspx?transNo=" & i & "&tModify=false&IsClickMain=1")

    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub


#Region "********Reports********"

    'Protected Sub MyGridView_FillContextMenuItems(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuEventArgs)
    '    If e.MenuType = GridViewContextMenuType.Rows Then
    '        'e.Items.Add(e.CreateItem("Get Key", "GetKey"))
    '        e.Items.Clear()
    '    End If
    'End Sub

    'Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim lnk As LinkButton = TryCast(sender, LinkButton)
    '    Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
    '    NewScriptManager.RegisterPostBackControl(lnk)
    'End Sub

    'Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
    '    Dim lnk As New LinkButton
    '    Dim sb As New StringBuilder
    '    lnk = sender
    '    Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
    '    'Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
    '    Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "PEMeritNo")
    '    Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, PayLocNo.ToString), _
    '                                              New ReportParameter(ReportParameter.Type.int, id.ToString()), _
    '                                              New ReportParameter(ReportParameter.Type.int, "0"))
    '    sb.Append("<script>")
    '    sb.Append("window.open('rpttemplateviewer.aspx?reportno=405&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
    '    sb.Append("</script>")
    '    ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    'End Sub
#End Region



End Class
















