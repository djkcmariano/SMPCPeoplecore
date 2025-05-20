Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PEApproverEscal
    Inherits System.Web.UI.Page

    '    Dim UserNo As Int64 = 0
    '    Dim TransNo As Int64 = 0
    '    Dim PayLocNo As Int64 = 0
    '    Dim rowno As Integer = 0

    '    Dim clsGen As New clsGenericClass


    '    Protected Sub PopulateGrid(Optional IsMain As Boolean = False)
    '        Try
    '            Dim dt As DataTable
    '            Dim sortDirection As String = "", sortExpression As String = ""

    '            dt = SQLHelper.ExecuteDataTable("EPEApproverScal_Web", UserNo, Filter1.SearchText, PayLocNo)
    '            Dim dv As DataView = dt.DefaultView
    '            If ViewState("SortDirection") IsNot Nothing Then
    '                sortDirection = ViewState("SortDirection").ToString()
    '            End If
    '            If ViewState("SortExpression") IsNot Nothing Then
    '                sortExpression = ViewState("SortExpression").ToString()
    '                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
    '            End If

    '            If IsMain Then
    '                ViewState("TransNo") = 0
    '                ViewState("TransCode") = ""
    '            End If

    '            grdMain.SelectedIndex = 0
    '            grdMain.DataSource = dv
    '            grdMain.DataBind()

    '            PopulateDetl()

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Protected Sub PopulateDetl(Optional pageNo As Integer = 0)
    '        Try
    '            Dim dt As DataTable
    '            Dim sortDirection As String = "", sortExpression As String = ""

    '            If Generic.ToInt(ViewState("TransNo")) = 0 And grdMain.Rows.Count > 0 Then
    '                ViewState("TransNo") = grdMain.DataKeys(0).Values(0).ToString()
    '                ViewState("TransCode") = grdMain.DataKeys(0).Values(1).ToString()
    '            End If
    '            dt = SQLHelper.ExecuteDataTable("EPEApproverScalDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
    '            Dim dv As DataView = dt.DefaultView
    '            grdDetl.PageIndex = Generic.ToInt(pageNo)
    '            grdDetl.DataSource = dv
    '            grdDetl.DataBind()

    '            Me.lblDetl.Text = "Transaction No.: " & Generic.ToStr(ViewState("TransCode"))

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Protected Sub grdMain_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
    '        Try
    '            If ViewState("SortDirection") Is Nothing OrElse ViewState("SortExpression").ToString() <> e.SortExpression Then
    '                ViewState("SortDirection") = "ASC"
    '            ElseIf ViewState("SortDirection").ToString() = "ASC" Then
    '                ViewState("SortDirection") = "DESC"
    '            ElseIf ViewState("SortDirection").ToString() = "DESC" Then
    '                ViewState("SortDirection") = "ASC"
    '            End If
    '            ViewState("SortExpression") = e.SortExpression
    '            PopulateGrid()
    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
    '        Try
    '            grdMain.PageIndex = e.NewPageIndex
    '            PopulateGrid()
    '        Catch ex As Exception

    '        End Try
    '    End Sub


    '    Protected Sub grdDetl_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
    '        Try
    '            grdDetl.PageIndex = e.NewPageIndex
    '            PopulateGrid()
    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    '        UserNo = Generic.ToInt(Session("OnlineUserNo"))
    '        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

    '        AccessRights.CheckUser(UserNo)

    '        If Not IsPostBack Then
    '            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
    '            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
    '            PopulateGrid()
    '        End If

    '        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

    '        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    '    End Sub

    '    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Try
    '            Dim ib As New ImageButton
    '            ib = sender

    '            Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
    '            rowno = gvrow.RowIndex

    '            ViewState("TransNo") = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
    '            ViewState("TransCode") = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
    '            Me.grdMain.SelectedIndex = Generic.ToInt(rowno)
    '            PopulateDetl()

    '        Catch ex As Exception
    '        End Try
    '    End Sub

    '    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        PopulateGrid(True)

    '    End Sub
    '    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
    '            Dim ib As New ImageButton
    '            ib = sender

    '            Dim dt As DataTable
    '            dt = SQLHelper.ExecuteDataTable("EPEApproverScal_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
    '            For Each row As DataRow In dt.Rows
    '                Generic.PopulateData(Me, "pnlPopupMain", dt)
    '            Next
    '            mdlMain.Show()

    '        Else
    '            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
    '        End If

    '    End Sub

    '    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '            Generic.ClearControls(Me, "pnlPopupMain")
    '            mdlMain.Show()
    '        Else
    '            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
    '        End If

    '    End Sub

    '    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
    '            For i As Integer = 0 To Me.grdMain.Rows.Count - 1
    '                chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
    '                ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
    '                If chk.Checked = True Then
    '                    Generic.DeleteRecordAuditCol("EPEApproverScalDeti", UserNo, "ApproverScalNo", Generic.ToInt(ib.CommandArgument))
    '                    Generic.DeleteRecordAudit("EPEApproverScal", UserNo, Generic.ToInt(ib.CommandArgument))
    '                    Count = Count + 1
    '                End If
    '            Next

    '            If Count > 0 Then
    '                PopulateGrid()
    '                MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
    '            Else
    '                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
    '            End If

    '        Else
    '            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
    '        End If

    '    End Sub

    '    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '            Dim Retval As Boolean = False
    '            Dim PEApproverScalNo As Integer = Generic.ToInt(Me.txtPEApproverScalNo.Text)
    '            Dim ApproverCodeNo As Integer = Generic.ToInt(Me.cboApproverCodeNo.SelectedValue)
    '            Dim PEApproverScalDesc As String = Generic.ToStr(Me.txtPEApproverScalDesc.Text)
    '            Dim JobGradeGroupNo As Integer = Generic.ToInt(Me.cboSectionPositionGrpNo.SelectedValue)
    '            Dim NoOfScal As Integer = Generic.ToInt(Me.txtNOOfScal.Text)
    '            Dim OrganizationLimitNo As Integer = Generic.ToInt(Me.cboOrganizationLimitNo.SelectedValue)


    '            Dim dt As DataTable
    '            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
    '            dt = SQLHelper.ExecuteDataTable("EPEApproverScal_WebValidate", UserNo, PEApproverScalNo, ApproverCodeNo, PEApproverScalDesc, JobGradeGroupNo, NoOfScal, OrganizationLimitNo, PayLocNo)
    '            For Each row As DataRow In dt.Rows
    '                invalid = Generic.ToBol(row("Invalid"))
    '                messagedialog = Generic.ToStr(row("MessageDialog"))
    '                alerttype = Generic.ToStr(row("AlertType"))
    '            Next

    '            If invalid = True Then
    '                mdlMain.Show()
    '                MessageBox.Alert(messagedialog, alerttype, Me)
    '                Exit Sub
    '            End If

    '            If SQLHelper.ExecuteNonQuery("EPEApproverScal_WebSave", UserNo, PEApproverScalNo, ApproverCodeNo, PEApproverScalDesc, JobGradeGroupNo, NoOfScal, OrganizationLimitNo, PayLocNo) > 0 Then
    '                Retval = True
    '            Else
    '                Retval = False
    '            End If

    '            If Retval Then
    '                PopulateGrid()
    '                MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '            Else
    '                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
    '            End If
    '        Else
    '            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
    '        End If

    '    End Sub


    '    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
    '            Dim ib As New ImageButton
    '            ib = sender

    '            Dim dt As DataTable
    '            dt = SQLHelper.ExecuteDataTable("EPEApproverScalDeti_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
    '            For Each row As DataRow In dt.Rows
    '                Generic.PopulateData(Me, "pnlPopupDetl", dt)
    '            Next

    '            Select Case Generic.ToInt(Me.cboOrganizationNo.SelectedValue)
    '                Case 4, 9, 10, 11
    '                    cboEmployeeNo.Enabled = True
    '                Case Else
    '                    cboEmployeeNo.Enabled = False
    '                    cboEmployeeNo.Text = ""
    '            End Select

    '            mdlDetl.Show()

    '        Else
    '            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
    '        End If

    '    End Sub

    '    Protected Sub btnAddDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '            Generic.ClearControls(Me, "pnlPopupDetl")
    '            mdlDetl.Show()
    '        Else
    '            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
    '        End If

    '    End Sub

    '    Protected Sub btnDeleteDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
    '            For i As Integer = 0 To Me.grdDetl.Rows.Count - 1
    '                chk = CType(grdDetl.Rows(i).FindControl("txtIsSelect"), CheckBox)
    '                ib = CType(grdDetl.Rows(i).FindControl("btnEditDetl"), ImageButton)
    '                If chk.Checked = True Then
    '                    Generic.DeleteRecordAudit("EPEApproverScalDeti", UserNo, Generic.ToInt(ib.CommandArgument))
    '                    Count = Count + 1
    '                End If
    '            Next

    '            If Count > 0 Then
    '                PopulateDetl()
    '                MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
    '            Else
    '                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
    '            End If

    '        Else
    '            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
    '        End If

    '    End Sub

    '    Protected Sub lnkSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '            Dim Retval As Boolean = False
    '            Dim tno As Integer = Generic.ToInt(ViewState("TransNo"))
    '            Dim PEApproverScalDetiNo As Integer = Generic.ToInt(Me.txtPEApproverScalDetiNo.Text)
    '            Dim OrganizationNo As Integer = Generic.ToInt(Me.cboOrganizationNo.SelectedValue)
    '            Dim OrderLevel As Integer = Generic.ToInt(Me.txtOrderLevel.Text)
    '            Dim EmployeeNo As Integer = Generic.ToInt(Me.cboEmployeeNo.SelectedValue)
    '            Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)


    '            Dim dt As DataTable
    '            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
    '            dt = SQLHelper.ExecuteDataTable("EPEApproverScalDeti_WebValidate", UserNo, PEApproverScalDetiNo, tno, OrganizationNo, OrderLevel, EmployeeNo, Remarks)
    '            For Each row As DataRow In dt.Rows
    '                invalid = Generic.ToBol(row("Invalid"))
    '                messagedialog = Generic.ToStr(row("MessageDialog"))
    '                alerttype = Generic.ToStr(row("AlertType"))
    '            Next

    '            If invalid = True Then
    '                mdlDetl.Show()
    '                MessageBox.Alert(messagedialog, alerttype, Me)
    '                Exit Sub
    '            End If


    '            If SQLHelper.ExecuteNonQuery("EPEApproverScalDeti_WebSave", UserNo, PEApproverScalDetiNo, tno, OrganizationNo, OrderLevel, EmployeeNo, Remarks) > 0 Then
    '                Retval = True
    '            Else
    '                Retval = False
    '            End If

    '            If Retval Then
    '                PopulateDetl()
    '                MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '            Else
    '                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
    '            End If
    '        Else
    '            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
    '        End If

    '    End Sub

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPEApproverScal_Web", UserNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If ViewState("TransNo") = 0 Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"PEApproverScalNo", "Code"})
            ViewState("TransNo") = obj(0)
            lblDetl.Text = obj(1)
        End If

    End Sub

    Private Sub PopulateGridDetl(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEApproverScalDeti_Web", UserNo, id)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

#Region "********Main*******"
    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEApproverScalNo", "IsEnabled"})
                Dim iNo As Integer = Generic.ToInt(obj(0))
                Dim IsEnabled As Boolean = Generic.ToBol(obj(1))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPEApproverScal_WebOne", UserNo, Generic.ToInt(iNo))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                Next
                lnkSave.Enabled = IsEnabled
                mdlMain.Show()


            Else
                MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEApproverScalNo", "Code"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
    End Sub


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        TransNo = Generic.ToInt(ViewState("TransNo"))

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEApproverScalNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPEApproverScal", UserNo, item)
                Generic.DeleteRecordAuditCol("EPEApproverScalDeti", UserNo, "PEApproverScalNo", item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                PopulateGridDetl(TransNo)
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)

        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try
            lnkSave.Enabled = True
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub



    'Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If SaveRecord() Then
    '        MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '        PopulateGrid()
    '    Else
    '        MessageBox.Critical(MessageTemplate.ErrorSave, Me)
    '    End If

    'End Sub

    'Private Function SaveRecord() As Integer
    '    Dim RetVal As Integer = 0
    '    Dim dt As DataTable
    '    Try
    '        dt = SQLHelper.ExecuteDataTable("EComp_WebSave", UserNo, Generic.ToInt(txtCode.Text), txtCompCode.Text, txtCompDesc.Text, Generic.ToInt(cboCompTypeNo.SelectedValue), Generic.ToInt(cboCompClusterNo.SelectedValue), txtCompDetl.Text, PayLocNo)
    '        For Each row As DataRow In dt.Rows
    '            RetVal = Generic.ToInt(row("RetVal"))
    '        Next
    '    Catch ex As Exception

    '    End Try
    '    Return RetVal
    'End Function

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim PEApproverScalNo As Integer = Generic.ToInt(Me.txtPEApproverScalNo.Text)
            Dim ApproverCodeNo As Integer = Generic.ToInt(Me.cboApproverCodeNo.SelectedValue)
            Dim PEApproverScalDesc As String = Generic.ToStr(Me.txtPEApproverScalDesc.Text)
            Dim JobGradeGroupNo As Integer = Generic.ToInt(Me.cboSectionPositionGrpNo.SelectedValue)
            Dim NoOfScal As Integer = Generic.ToInt(Me.txtNOOfScal.Text)
            Dim OrganizationLimitNo As Integer = Generic.ToInt(Me.cboOrganizationLimitNo.SelectedValue)


            Dim dt As DataTable
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            dt = SQLHelper.ExecuteDataTable("EPEApproverScal_WebValidate", UserNo, PEApproverScalNo, ApproverCodeNo, PEApproverScalDesc, JobGradeGroupNo, NoOfScal, OrganizationLimitNo, Generic.ToInt(cboPayLocNo.SelectedValue))
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

            If SQLHelper.ExecuteNonQuery("EPEApproverScal_WebSave", UserNo, PEApproverScalNo, ApproverCodeNo, PEApproverScalDesc, JobGradeGroupNo, NoOfScal, OrganizationLimitNo, Generic.ToInt(cboPayLocNo.SelectedValue)) > 0 Then
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
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

#End Region

#Region "********Detail********"

    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEApproverScalDetiNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPEApproverScalDeti_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupDetl", dt)
                Next
                mdlDetl.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkDeleteDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        TransNo = Generic.ToInt(ViewState("TransNo"))
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PEApproverScalDetiNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPEApproverScalDeti", UserNo, item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGridDetl(TransNo)
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If

    End Sub
    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub
    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            mdlDetl.Show()
        End If
    End Sub

    'Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
    '    TransNo = Generic.ToInt(ViewState("TransNo"))
    '    If SaveRecordDetl() Then
    '        MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '        PopulateGridDetl(TransNo)
    '    Else
    '        MessageBox.Warning(MessageTemplate.ErrorSave, Me)
    '    End If
    'End Sub

    'Private Function SaveRecordDetl() As Boolean

    '    If SQLHelper.ExecuteNonQuery("ECompDeti_WebSave", UserNo, Generic.ToInt(txtCodeDetl.Text), Generic.ToInt(ViewState("TransNo")), Generic.ToInt(cboCompScaleNo.SelectedValue), txtAnchor.Text) > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    Protected Sub lnkSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(ViewState("TransNo"))
            Dim PEApproverScalDetiNo As Integer = Generic.ToInt(Me.txtPEApproverScalDetiNo.Text)
            Dim OrganizationNo As Integer = Generic.ToInt(Me.cboOrganizationNo.SelectedValue)
            Dim OrderLevel As Integer = Generic.ToInt(Me.txtOrderLevel.Text)
            Dim EmployeeNo As Integer = Generic.ToInt(Me.cboEmployeeNo.SelectedValue)
            Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)


            Dim dt As DataTable
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            dt = SQLHelper.ExecuteDataTable("EPEApproverScalDeti_WebValidate", UserNo, PEApproverScalDetiNo, tno, OrganizationNo, OrderLevel, EmployeeNo, Remarks)
            For Each row As DataRow In dt.Rows
                invalid = Generic.ToBol(row("Invalid"))
                messagedialog = Generic.ToStr(row("MessageDialog"))
                alerttype = Generic.ToStr(row("AlertType"))
            Next

            If invalid = True Then
                mdlDetl.Show()
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If


            If SQLHelper.ExecuteNonQuery("EPEApproverScalDeti_WebSave", UserNo, PEApproverScalDetiNo, tno, OrganizationNo, OrderLevel, EmployeeNo, Remarks) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                PopulateGridDetl(tno)
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub


#End Region
    Protected Sub cboOrganizationNo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboOrganizationNo.SelectedIndexChanged

        Select Case Generic.ToInt(Me.cboOrganizationNo.SelectedValue)
            Case 4, 9, 10, 11
                cboEmployeeNo.Enabled = True
            Case Else
                cboEmployeeNo.Enabled = False
                cboEmployeeNo.Text = ""
        End Select

        mdlDetl.Show()

    End Sub
#Region "********Detail Check All********"


    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs)
        If e.ButtonType <> ColumnCommandButtonType.SelectCheckbox Then
            Return
        End If
        Dim rowEnabled As Boolean = getRowEnabledStatus(e.VisibleIndex)
        e.Enabled = rowEnabled

        'If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
        '    Dim isSelected As Boolean = Convert.ToBoolean(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
        '    If isSelected Then

        '        grdMain.Selection.SetSelection(e.VisibleIndex, True)

        '    End If
        'End If
    End Sub
    Private Function getRowEnabledStatus(ByVal VisibleIndex As Integer) As Boolean
        Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(VisibleIndex, "IsEnabled"))
        If value = True Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Sub cbCheckAll_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim cb As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)
        cb.ClientSideEvents.CheckedChanged = String.Format("cbCheckAll_CheckedChanged")
        cb.Checked = False
        Dim count As Integer = 0
        Dim startIndex As Integer = grdMain.PageIndex * grdMain.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdMain.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)

        For i As Integer = startIndex To endIndex - 1
            If grdMain.Selection.IsRowSelected(i) Then
                count = count + 1
            End If
        Next i

        If count > 0 Then
            cb.Checked = True
        End If

    End Sub
    Protected Sub gridMain_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        Boolean.TryParse(e.Parameters, False)

        Dim startIndex As Integer = grdMain.PageIndex * grdMain.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdMain.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)
        For i As Integer = startIndex To endIndex - 1
            Dim rowEnabled As Boolean = getRowEnabledStatus(i)
            If rowEnabled AndAlso e.Parameters = "true" Then
                grdMain.Selection.SelectRow(i)
            Else
                grdMain.Selection.UnselectRow(i)
            End If
        Next i

    End Sub

#End Region

End Class