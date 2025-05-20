Imports clsLib
Imports System.Data
Partial Class Secured_PEReviewEval
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim TransNo As Int64 = 0


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateGrid()
            PopulateTabHeader()
        End If
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EPEReviewEval_Web", UserNo, TransNo)
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

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEReviewEval_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
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


    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEReviewMain_WebTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New ImageButton
            ib = sender
            PopulateData(Generic.ToInt(ib.CommandArgument))
            ModalPopupExtender1.Show()
            PopulateEvaluator()
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
                    Generic.DeleteRecordAudit("EPEReviewEval", UserNo, Generic.ToInt(ib.CommandArgument))
                    Count = Count + 1
                End If
            Next
            PopulateGrid()
            MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Function SaveRecord() As Integer
        Dim tno As Integer = Generic.ToInt(Me.txtPEReviewEvalNo.Text)
        Dim PEEvaluatorNo As Integer = Generic.ToInt(Me.cboPEEvaluatorNo.SelectedValue)
        Dim EvaluatorNo As Integer = Generic.ToInt(Me.cboEvaluatorNo.SelectedValue)
        Dim Weighted As Double = Generic.ToInt(Me.txtWeighted.Text)
        Dim PEEvaluatorPickNo As Integer = 0
        Dim OrderLevel As Integer = Generic.ToInt(Me.txtOrderLevel.Text)

        If SQLHelper.ExecuteNonQuery("EPEReviewEval_WebSave", UserNo, tno, TransNo, PEEvaluatorNo, EvaluatorNo, Weighted, PEEvaluatorPickNo, OrderLevel) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If

    End Function
    Protected Sub cboPEEvaluatorNo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPEEvaluatorNo.SelectedIndexChanged

        PopulateEvaluator()

    End Sub
    Private Sub PopulateEvaluator()
        Select Case Generic.ToInt(Me.cboPEEvaluatorNo.SelectedValue)
            Case 0
                cboEvaluatorNo.Enabled = True
            Case Else
                cboEvaluatorNo.Enabled = False
                cboEvaluatorNo.Text = ""
        End Select
        ModalPopupExtender1.Show()
    End Sub

End Class
