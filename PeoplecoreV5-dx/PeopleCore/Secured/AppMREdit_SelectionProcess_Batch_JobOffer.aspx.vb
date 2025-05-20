Imports clsLib
Imports System.Data

Partial Class Secured_AppMREdit_SelectionProcess_Batch_Offer
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim ActionStatNo As Integer = 3
    Dim rowno As Integer = 0
    Dim FormName As String = "", tablename As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        hidTransNo.Value = TransNo
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        FormName = Generic.ToStr(Request.QueryString("FormName"))
        TableName = Generic.ToStr(Request.QueryString("TableName"))
        AccessRights.CheckUser(UserNo, FormName, TableName)

        If Not IsPostBack Then
            PopulateGrid()
            AutoCompleteExtender1.ContextKey = Generic.ToInt(cboHiringAlternativeNo.SelectedValue) & "|" & TransNo
        End If
        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub

#Region "Main"
    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            'dt = SQLHelper.ExecuteDataTable("EMRHiredMass_Web_Batch", UserNo, Filter1.SearchText, ActionStatNo)
            dt = SQLHelper.ExecuteDataTable("EMRActivityHired_Web", UserNo, TransNo, Filter1.SearchText)
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If

            grdMain.SelectedIndex = 0
            grdMain.DataSource = dv
            grdMain.DataBind()

            If dt.Rows.Count > 0 Then
                ViewState("TransNo") = grdMain.DataKeys(0).Values(0).ToString()
                ViewState("TransCode") = grdMain.DataKeys(0).Values(1).ToString()
            End If

            PopulateDetl()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim ib As New ImageButton
            ib = sender

            Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
            rowno = gvrow.RowIndex
            Me.grdMain.SelectedIndex = Generic.ToInt(rowno)
            ViewState("TransNo") = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
            ViewState("TransCode") = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
            PopulateDetl()

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

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
        Dim ib As New ImageButton
        ib = sender
        ModalPopupExtender1.Show()
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        'End If
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
        Dim mrActivityHiredNo As New Label
        For i As Integer = 0 To Me.grdMain.Rows.Count - 1
            chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
            ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
            mrActivityHiredNo = CType(grdMain.Rows(i).FindControl("lblmrActivityHiredNo"), Label)
            If chk.Checked = True Then
                'Generic.DeleteRecordAudit("EMRHiredMass", UserNo, Generic.ToInt(ib.CommandArgument))
                Generic.DeleteRecordAudit("EMRActivityHired", UserNo, Generic.ToInt(mrActivityHiredNo.Text))
                Count = Count + 1
            End If
        Next
        MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        'End If
    End Sub

    Protected Sub lnkHistory_Click(sender As Object, e As EventArgs)
        'Info1.xIsApplicant = True
        Dim lnk As New LinkButton
        lnk = sender

        apphistory1.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
        apphistory1.xIsApplicant = Generic.ToBol(Generic.Split(lnk.CommandArgument, 1))
        apphistory1.Show()

    End Sub


    Protected Sub lnk_Click(sender As Object, e As EventArgs)
        'Info1.xIsApplicant = True
        Dim lnk As New LinkButton
        lnk = sender

        Info1.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
        Info1.xIsApplicant = Generic.ToBol(Generic.Split(lnk.CommandArgument, 1))
        Info1.Show()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
        Generic.ClearControls(Me, "Panel1")
        ModalPopupExtender1.Show()
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        'End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim HiringAlternativeNo As Integer = Generic.ToInt(cboHiringAlternativeNo.SelectedValue)
        Dim hidID As Integer = Generic.ToInt(Me.hidID.Value)

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EMRHiredMass_WebValidate", UserNo, TransNo, hidID, HiringAlternativeNo, ActionStatNo)
        For Each row As DataRow In dt.Rows
            invalid = Generic.ToBol(row("Invalid"))
            messagedialog = Generic.ToStr(row("MessageDialog"))
            alerttype = Generic.ToStr(row("AlertType"))
        Next

        If invalid = True Then
            ModalPopupExtender1.Show()
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EMRHiredMass_WebSave", UserNo, TransNo, hidID, HiringAlternativeNo, ActionStatNo) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub


    'Submit record
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lbl As New Label, cboJobOfferStatNo As New DropDownList
        Dim tcount As Integer, SaveCount As Integer = 0
        Dim xds As New DataSet, lblmrActivityHiredNo As New Label

        ' If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then

        For tcount = 0 To Me.grdMain.Rows.Count - 1
            lbl = CType(grdMain.Rows(tcount).FindControl("lblNo"), Label)
            cboJobOfferStatNo = CType(grdMain.Rows(tcount).FindControl("cboJobOfferStatNo"), DropDownList)
            lblmrActivityHiredNo = CType(grdMain.Rows(tcount).FindControl("lblmrActivityHiredNo"), Label)

            Dim MRHiredMassNo As Integer = Generic.ToInt(lbl.Text)
            Dim StatusNo As Integer = Generic.ToInt(cboJobOfferStatNo.SelectedValue)

            If Not cboJobOfferStatNo Is Nothing Then
                If SQLHelper.ExecuteNonQuery("EMRHiredMass_WebUpdate", UserNo, MRHiredMassNo, 0, StatusNo, ActionStatNo) > 0 Then
                    SaveCount = SaveCount + 1
                End If
                If SaveCount > 0 Then
                    If SQLHelper.ExecuteNonQuery("EMRActivityHired_WebUpdate", UserNo, Generic.ToInt(lblmrActivityHiredNo.Text), StatusNo, ActionStatNo) > 0 Then
                    End If
                End If
                
            End If

        Next

        PopulateGrid()
        MessageBox.Success(MessageTemplate.SuccessSave, Me)

        'Else
        'MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        'End If

    End Sub
#End Region


#Region "Detail"
    Protected Sub PopulateDetl(Optional pageNo As Integer = 0)
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EMROffer_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirectionDetl") IsNot Nothing Then
                sortDirection = ViewState("SortDirectionDetl").ToString()
            End If
            If ViewState("SortExpressionDetl") IsNot Nothing Then
                sortExpression = ViewState("SortExpressionDetl").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            'grdDetl.SelectedIndex = 0
            grdDetl.DataSource = dv
            grdDetl.DataBind()

            Me.lblDetl.Text = Generic.ToStr(ViewState("TransCode"))

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdDetl_Sorting(sender As Object, e As GridViewSortEventArgs)
        Try
            If ViewState("SortDirectionDetl") Is Nothing OrElse ViewState("SortExpressionDetl").ToString() <> e.SortExpression Then
                ViewState("SortDirectionDetl") = "ASC"
            ElseIf ViewState("SortDirectionDetl").ToString() = "ASC" Then
                ViewState("SortDirectionDetl") = "DESC"
            ElseIf ViewState("SortDirectionDetl").ToString() = "DESC" Then
                ViewState("SortDirectionDetl") = "ASC"
            End If
            ViewState("SortExpressionDetl") = e.SortExpression
            PopulateDetl()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub grdDetl_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            grdDetl.PageIndex = e.NewPageIndex
            PopulateDetl()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
        Dim ib As New ImageButton
        ib = sender

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMROffer_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", Generic.ToInt(Session("xPayLocNo")))
        Next

        mdlDetl.Show()

        'Else
        'MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        'End If

    End Sub

    Protected Sub btnAddDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
        Generic.ClearControls(Me, "pnlPopupDetl")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", Generic.ToInt(Session("xPayLocNo")))
        mdlDetl.Show()
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        'End If

    End Sub

    Protected Sub btnDeleteDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
        For i As Integer = 0 To Me.grdDetl.Rows.Count - 1
            chk = CType(grdDetl.Rows(i).FindControl("txtIschk"), CheckBox)
            ib = CType(grdDetl.Rows(i).FindControl("btnEditDetl"), ImageButton)
            If chk.Checked = True Then
                Generic.DeleteRecordAudit("EMROffer", UserNo, Generic.ToInt(ib.CommandArgument))
                Count = Count + 1
            End If
        Next
        If Count > 0 Then
            PopulateDetl()
            MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

        'Else
        'MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        'End If

    End Sub

    Protected Sub lnkSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(txtMROfferNo.Text)
        Dim typeno As Integer = Generic.ToInt(cboPayIncomeTypeNo.SelectedValue)
        Dim Amount As Double = Generic.ToDec(txtAmount.Text)

        If SQLHelper.ExecuteNonQuery("EMROffer_WebSave", UserNo, tno, Generic.ToInt(ViewState("TransNo")), TransNo, Amount, typeno) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateDetl()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

#End Region

End Class

