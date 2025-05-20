Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PENormsList
    Inherits System.Web.UI.Page

    '    Dim UserNo As Int64 = 0
    '    Dim TransNo As Int64 = 0
    '    Dim PayLocNo As Int64 = 0
    '    Dim rowno As Integer = 0

    '    Dim clsGen As New clsGenericClass

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

    '    End Sub

    '    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        PopulateGrid()
    '    End Sub

    '#Region "Main"
    '    Protected Sub PopulateGrid()
    '        Try
    '            Dim dt As DataTable
    '            Dim sortDirection As String = "", sortExpression As String = ""
    '            dt = SQLHelper.ExecuteDataTable("EPENorms_Web", UserNo, Filter1.SearchText, PayLocNo)
    '            Dim dv As DataView = dt.DefaultView
    '            If ViewState("SortDirection") IsNot Nothing Then
    '                sortDirection = ViewState("SortDirection").ToString()
    '            End If
    '            If ViewState("SortExpression") IsNot Nothing Then
    '                sortExpression = ViewState("SortExpression").ToString()
    '                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
    '            End If

    '            grdMain.SelectedIndex = 0
    '            grdMain.DataSource = dv
    '            grdMain.DataBind()

    '            If dt.Rows.Count > 0 Then
    '                ViewState("TransNo") = grdMain.DataKeys(0).Values(0).ToString()
    '                ViewState("TransCode") = grdMain.DataKeys(0).Values(1).ToString()
    '            End If

    '            PopulateDetl()

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Try
    '            Dim ib As New ImageButton
    '            ib = sender

    '            Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
    '            rowno = gvrow.RowIndex
    '            Me.grdMain.SelectedIndex = Generic.ToInt(rowno)
    '            ViewState("TransNo") = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
    '            ViewState("TransCode") = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
    '            PopulateDetl()

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


    '    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
    '            Dim ib As New ImageButton
    '            ib = sender
    '            Dim dt As DataTable
    '            dt = SQLHelper.ExecuteDataTable("EPENorms_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
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
    '                    Generic.DeleteRecordAuditCol("EPENormsDeti", UserNo, "PENormsNo", Generic.ToInt(ib.CommandArgument))
    '                    Generic.DeleteRecordAudit("EPENorms", UserNo, Generic.ToInt(ib.CommandArgument))
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

    '        Dim Retval As Boolean = False
    '        Dim PENormsNo As Integer = Generic.ToInt(Me.txtPENormsNo.Text)
    '        Dim PENormsCode As String = Generic.ToStr(Me.txtPENormsCode.Text)
    '        Dim PENormsDesc As String = Generic.ToStr(Me.txtPENormsDesc.Text)
    '        Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)
    '        Dim StartDate As String = Generic.ToStr(Me.txtStartDate.Text)
    '        Dim EndDate As String = Generic.ToStr(Me.txtEndDate.Text)
    '        Dim IsActive As Boolean = Generic.ToBol(Me.txtIsActive.Checked)

    '        If SQLHelper.ExecuteNonQuery("EPENorms_WebSave", UserNo, PENormsNo, PENormsCode, PENormsDesc, StartDate, EndDate, IsActive, Remarks, PayLocNo) > 0 Then
    '            Retval = True
    '        Else
    '            Retval = False
    '        End If

    '        If Retval Then
    '            PopulateGrid()
    '            MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '        Else
    '            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
    '        End If

    '    End Sub

    '#End Region

    '#Region "Detail"
    '    Protected Sub PopulateDetl(Optional pageNo As Integer = 0)
    '        Try
    '            Dim dt As DataTable
    '            Dim sortDirection As String = "", sortExpression As String = ""
    '            dt = SQLHelper.ExecuteDataTable("EPENormsDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
    '            Dim dv As DataView = dt.DefaultView
    '            If ViewState("SortDirectionDetl") IsNot Nothing Then
    '                sortDirection = ViewState("SortDirectionDetl").ToString()
    '            End If
    '            If ViewState("SortExpressionDetl") IsNot Nothing Then
    '                sortExpression = ViewState("SortExpressionDetl").ToString()
    '                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
    '            End If
    '            'grdDetl.SelectedIndex = 0
    '            grdDetl.DataSource = dv
    '            grdDetl.DataBind()

    '            'Me.lblDetl.Text = "Reference No.: " & Generic.ToStr(ViewState("TransCode"))
    '            Me.lblDetl.Text = "Classification / Rating"

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Protected Sub grdDetl_Sorting(sender As Object, e As GridViewSortEventArgs)
    '        Try
    '            If ViewState("SortDirectionDetl") Is Nothing OrElse ViewState("SortExpressionDetl").ToString() <> e.SortExpression Then
    '                ViewState("SortDirectionDetl") = "ASC"
    '            ElseIf ViewState("SortDirectionDetl").ToString() = "ASC" Then
    '                ViewState("SortDirectionDetl") = "DESC"
    '            ElseIf ViewState("SortDirectionDetl").ToString() = "DESC" Then
    '                ViewState("SortDirectionDetl") = "ASC"
    '            End If
    '            ViewState("SortExpressionDetl") = e.SortExpression
    '            PopulateDetl()
    '        Catch ex As Exception
    '        End Try
    '    End Sub

    '    Protected Sub grdDetl_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
    '        Try
    '            grdDetl.PageIndex = e.NewPageIndex
    '            PopulateDetl()
    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
    '            Dim ib As New ImageButton
    '            ib = sender

    '            Dim dt As DataTable
    '            dt = SQLHelper.ExecuteDataTable("EPENormsDeti_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
    '            For Each row As DataRow In dt.Rows
    '                Generic.PopulateData(Me, "pnlPopupDetl", dt)
    '            Next

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
    '                    Generic.DeleteRecordAudit("EPENormsDeti", UserNo, Generic.ToInt(ib.CommandArgument))
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

    '        Dim Retval As Boolean = False
    '        Dim tno As Integer = Generic.ToInt(ViewState("TransNo"))
    '        Dim PENormsDetiNo As Integer = Generic.ToInt(Me.txtPENormsDetiNo.Text)
    '        Dim PENormsDetiCode As String = Generic.ToStr(Me.txtPENormsdetiCode.Text)
    '        Dim PENormsDetiDesc As String = Generic.ToStr(Me.txtPENormsDetiDesc.Text)
    '        Dim PERatingNo As Integer = Generic.ToInt(Me.cboPERatingNo.SelectedValue)
    '        Dim DivisibleBy As Double = Generic.ToDec(Me.txtDivisibleBy.Text)
    '        Dim FromRate As Double = Generic.ToDec(Me.txtFromRate.Text)
    '        Dim ToRate As Double = Generic.ToDec(Me.txtToRate.Text)

    '        If SQLHelper.ExecuteNonQuery("EPENormsDeti_WebSave", UserNo, PENormsDetiNo, tno, PENormsDetiDesc, FromRate, ToRate, DivisibleBy, PENormsDetiCode, PERatingNo) > 0 Then
    '            Retval = True
    '        Else
    '            Retval = False
    '        End If

    '        If Retval Then
    '            PopulateDetl()
    '            MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '        Else
    '            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
    '        End If

    '    End Sub

    '#End Region
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPENorms_Web", UserNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateGridDetl(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPENormsDeti_Web", UserNo, id)
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
                'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                'i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PeNormsNo"}))
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PeNormsNo", "IsEnabled"})
                Dim iNo As Integer = Generic.ToInt(obj(0))
                Dim IsEnabled As Boolean = Generic.ToBol(obj(1))
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPENorms_WebOne", UserNo, Generic.ToInt(iNo))
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
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PeNormsNo", "PENormsCode"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = "Transaction No. : " & obj(1)
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        TransNo = Generic.ToInt(ViewState("TransNo"))

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PeNormsNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPENorms", UserNo, item)
                Generic.DeleteRecordAuditCol("EPENormsDeti", UserNo, "PeNormsNo", item)
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

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Private Function SaveRecord() As Integer
        Dim RetVal As Integer = 0
        Dim PENormsNo As Integer = Generic.ToInt(Me.txtPENormsNo.Text)
        Dim PENormsCode As String = Generic.ToStr(Me.txtPENormsCode.Text)
        Dim PENormsDesc As String = Generic.ToStr(Me.txtPENormsDesc.Text)
        Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)
        Dim StartDate As String = Generic.ToStr(Me.txtStartDate.Text)
        Dim EndDate As String = Generic.ToStr(Me.txtEndDate.Text)
        Dim IsActive As Boolean = Generic.ToBol(Me.txtIsActive.Checked)

        If SQLHelper.ExecuteNonQuery("EPENorms_WebSave", UserNo, PENormsNo, PENormsCode, PENormsDesc, StartDate, EndDate, IsActive, Remarks, Generic.ToInt(cboPayLocNo.SelectedValue)) > 0 Then
            Return True
        Else
            Return False
        End If
        Return RetVal
    End Function

#End Region

#Region "********Detail********"

    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PENormsDetiNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPENormsDeti_WebOne", UserNo, Generic.ToInt(i))
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
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PENormsDetiNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPENormsDeti", UserNo, item)
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

    Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
        TransNo = Generic.ToInt(ViewState("TransNo"))
        If SaveRecordDetl() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl(TransNo)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecordDetl() As Boolean
        Dim tno As Integer = Generic.ToInt(ViewState("TransNo"))
        Dim PENormsDetiNo As Integer = Generic.ToInt(Me.txtPENormsDetiNo.Text)
        Dim PENormsDetiCode As String = Generic.ToStr(Me.txtPENormsdetiCode.Text)
        Dim PENormsDetiDesc As String = Generic.ToStr(Me.txtPENormsDetiDesc.Text)
        Dim PERatingNo As Integer = Generic.ToInt(Me.cboPERatingNo.SelectedValue)
        Dim DivisibleBy As Double = Generic.ToDec(Me.txtDivisibleBy.Text)
        Dim FromRate As Double = Generic.ToDec(Me.txtFromRate.Text)
        Dim ToRate As Double = Generic.ToDec(Me.txtToRate.Text)

        If SQLHelper.ExecuteNonQuery("EPENormsDeti_WebSave", UserNo, PENormsDetiNo, tno, PENormsDetiDesc, FromRate, ToRate, DivisibleBy, PENormsDetiCode, PERatingNo) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region
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

