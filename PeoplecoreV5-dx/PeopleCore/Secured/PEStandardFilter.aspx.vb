Imports clsLib
Imports System.Data
Partial Class Secured_PEStandardFilter
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PEStandardFilterDesc As String = ""


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupLevel", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupStatus", PayLocNo)
            PopulateGrid()
            PopulateStatus()
            PopulateLevel()
            PopulateTabHeader()
        End If
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEStandardMain_WebTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
    End Sub


#Region "Position"
    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EPEStandardFilter_Web", UserNo, TransNo, "Position")
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            grdMain.DataSource = dv
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdMain_Sorting(sender As Object, e As GridViewSortEventArgs)
        Try
            If ViewState("SortDirection") Is Nothing OrElse ViewState("SortExpression").ToString() <> e.SortExpression Then
                ViewState("SortDirection") = "ASC"
            ElseIf ViewState("SortDirection").ToString() = "ASC" Then
                ViewState("SortDirection") = "DESC"
            ElseIf ViewState("SortDirection").ToString() = "DESC" Then
                ViewState("SortDirection") = "ASC"
            End If
            ViewState("SortExpression") = e.SortExpression
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            grdMain.PageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardFilter_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New ImageButton
            ib = sender
            PopulateData(Generic.ToInt(ib.CommandArgument))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For i As Integer = 0 To Me.grdMain.Rows.Count - 1
                chk = CType(grdMain.Rows(i).FindControl("chk"), CheckBox)
                ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
                If chk.Checked = True Then
                    Generic.DeleteRecordAudit("EPEStandardFilter", UserNo, Generic.ToInt(ib.CommandArgument))
                    Count = Count + 1
                End If
            Next
            MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(Me.txtPEStandardFilterNo.Text)
        Dim JobGradeNo As Integer = 0
        Dim Position As Integer = Generic.ToInt(Me.cboPositionNo.SelectedValue)
        Dim EmployeeStatNo As Integer = 0

        If SQLHelper.ExecuteNonQuery("EPEStandardFilter_WebSave", UserNo, tno, TransNo, "Position", JobGradeNo, Position, EmployeeStatNo) > 0 Then
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

#End Region


#Region "Job Level"
    Protected Sub PopulateLevel()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EPEStandardFilter_Web", UserNo, TransNo, "Level")
            Dim dv As DataView = dt.DefaultView
            grdLevel.DataSource = dv
            grdLevel.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdLevel_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            grdLevel.PageIndex = e.NewPageIndex
            PopulateLevel()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAddLevel_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupLevel")
            mdlLevel.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub PopulateDataLevel(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardFilter_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupLevel", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnEditLevel_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New ImageButton
            ib = sender
            PopulateDataLevel(Generic.ToInt(ib.CommandArgument))
            mdlLevel.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub btnDeleteLevel_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For i As Integer = 0 To Me.grdLevel.Rows.Count - 1
                chk = CType(grdLevel.Rows(i).FindControl("chk"), CheckBox)
                ib = CType(grdLevel.Rows(i).FindControl("btnEdit"), ImageButton)
                If chk.Checked = True Then
                    Generic.DeleteRecordAudit("EPEStandardFilter", UserNo, Generic.ToInt(ib.CommandArgument))
                    Count = Count + 1
                End If
            Next
            PopulateLevel()
            MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkSaveLevel_Click(sender As Object, e As EventArgs)
        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(Me.txtPEStandardFilterNo.Text)
        Dim JobGradeNo As Integer = Generic.ToInt(Me.cboJobGradeNo.SelectedValue)
        Dim Position As Integer = 0
        Dim EmployeeStatNo As Integer = 0

        If SQLHelper.ExecuteNonQuery("EPEStandardFilter_WebSave", UserNo, tno, TransNo, "Level", JobGradeNo, Position, EmployeeStatNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateLevel()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

#End Region


#Region "Employee Status"
    Protected Sub PopulateStatus()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EPEStandardFilter_Web", UserNo, TransNo, "Status")
            Dim dv As DataView = dt.DefaultView
            grdStatus.DataSource = dv
            grdStatus.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdStatus_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            grdStatus.PageIndex = e.NewPageIndex
            PopulateStatus()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnAddStatus_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupStatus")
            mdlStatus.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub PopulateDataStatus(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardFilter_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupStatus", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnEditStatus_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New ImageButton
            ib = sender
            PopulateDataStatus(Generic.ToInt(ib.CommandArgument))
            mdlStatus.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub btnDeleteStatus_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For i As Integer = 0 To Me.grdStatus.Rows.Count - 1
                chk = CType(grdStatus.Rows(i).FindControl("chk"), CheckBox)
                ib = CType(grdStatus.Rows(i).FindControl("btnEdit"), ImageButton)
                If chk.Checked = True Then
                    Generic.DeleteRecordAudit("EPEStandardFilter", UserNo, Generic.ToInt(ib.CommandArgument))
                    Count = Count + 1
                End If
            Next
            PopulateStatus()
            MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkSaveStatus_Click(sender As Object, e As EventArgs)
        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(Me.txtPEStandardFilterStatusNo.Text)
        Dim JobGradeNo As Integer = 0
        Dim Position As Integer = 0
        Dim EmployeeStatNo As Integer = Generic.ToInt(Me.cboEmployeeStatNo.SelectedValue)

        If SQLHelper.ExecuteNonQuery("EPEStandardFilter_WebSave", UserNo, tno, TransNo, "Status", JobGradeNo, Position, EmployeeStatNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateStatus()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub


#End Region

End Class
