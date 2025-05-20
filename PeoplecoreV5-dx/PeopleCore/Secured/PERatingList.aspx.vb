Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PEStandardMainList
    Inherits System.Web.UI.Page

    '    Dim UserNo As Int64 = 0
    '    Dim TransNo As Int64 = 0
    '    Dim PayLocNo As Int64 = 0
    '    Dim rowno As Integer = 0

    '    Protected Sub PopulateGrid(Optional IsMain As Boolean = False)
    '        Try
    '            Dim dt As DataTable
    '            Dim sortDirection As String = "", sortExpression As String = ""

    '            dt = SQLHelper.ExecuteDataTable("EPERating_Web", UserNo, Filter1.SearchText, PayLocNo)
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
    '            End If

    '            grdMain.DataSource = dv
    '            grdMain.DataBind()


    '        Catch ex As Exception

    '        End Try
    '    End Sub
    '    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    '        UserNo = Generic.ToInt(Session("OnlineUserNo"))
    '        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
    '        TransNo = Generic.ToInt(Request.QueryString("id"))
    '        AccessRights.CheckUser(UserNo)

    '        If Not IsPostBack Then
    '            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
    '            PopulateGrid()
    '        End If

    '        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

    '        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
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

    '    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        PopulateGrid(True)

    '    End Sub
    '    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
    '            Dim ib As New ImageButton
    '            ib = sender
    '            Dim dt As DataTable
    '            dt = SQLHelper.ExecuteDataTable("EPERating_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
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
    '                    Generic.DeleteRecordAudit("EPERating", UserNo, Generic.ToInt(ib.CommandArgument))
    '                    Count = Count + 1
    '                End If
    '            Next

    '            If Count > 0 Then
    '                MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
    '                PopulateGrid()
    '            Else
    '                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
    '            End If

    '        Else
    '            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
    '        End If

    '    End Sub

    '    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Dim Retval As Boolean = False
    '        Dim tno As Integer = Generic.ToInt(Me.txtPERatingNo.Text)
    '        Dim PERatingCode As String = Generic.ToStr(Me.txtPERatingCode.Text)
    '        Dim PERatingDesc As String = Generic.ToStr(Me.txtPERatingDesc.Text)
    '        Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)
    '        Dim Profeciency As Double = Generic.ToDec(Me.txtProfeciency.Text)

    '        If SQLHelper.ExecuteNonQuery("EPERating_WebSave", UserNo, tno, PERatingCode, PERatingDesc, Profeciency, Remarks, PayLocNo) > 0 Then
    '            Retval = True
    '        Else
    '            Retval = False
    '        End If

    '        If Retval Then
    '            MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '            PopulateGrid()
    '        Else
    '            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
    '        End If

    '    End Sub


    'End Class
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPERating_Web", UserNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

#Region "********Main*******"
    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                'i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PERatingNo"}))
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PERatingNo", "IsEnabled"})
                Dim iNo As Integer = Generic.ToInt(obj(0))
                Dim IsEnabled As Boolean = Generic.ToBol(obj(1))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPERating_WebOne", UserNo, Generic.ToInt(iNo))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                Next
                Try
                    cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                    cboPayLocNo.DataTextField = "tdesc"
                    cboPayLocNo.DataValueField = "tNo"
                    cboPayLocNo.DataBind()

                Catch ex As Exception

                End Try
                lnkSave.Enabled = IsEnabled
                mdlMain.Show()


            Else
                MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        TransNo = Generic.ToInt(ViewState("TransNo"))

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PERatingNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPERating", UserNo, item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
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
        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(Me.txtPERatingNo.Text)
        Dim PERatingCode As String = Generic.ToStr(Me.txtPERatingCode.Text)
        Dim PERatingDesc As String = Generic.ToStr(Me.txtPERatingDesc.Text)
        Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)
        Dim Profeciency As Double = Generic.ToDec(Me.txtProfeciency.Text)

        If SQLHelper.ExecuteNonQuery("EPERating_WebSave", UserNo, tno, PERatingCode, PERatingDesc, Profeciency, Remarks, Generic.ToInt(cboPayLocNo.SelectedValue)) > 0 Then
            Retval = True
        Else
            Retval = False
        End If
        Return Retval
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
















