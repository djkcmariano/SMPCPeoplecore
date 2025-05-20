Imports clsLib
Imports System.Data

Partial Class Secured_AppMREdit_SelectionProcess_Batch_ContractPrep
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim ActionStatNo As Integer = 5
    Dim FormName As String = "", TableName As String = ""

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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))

        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        FormName = Generic.ToStr(Request.QueryString("FormName"))
        TableName = Generic.ToStr(Request.QueryString("TableName"))
        AccessRights.CheckUser(UserNo, FormName, TableName)

        If Not IsPostBack Then
            PopulateGrid()
        End If
        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub
    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
        Dim ib As New ImageButton
        ib = sender
        Dim fmrhiredmassno As Integer = CType(ib.CommandArgument, Integer)
        'hifEmployeeNo.Value = fmrhiredmassno
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMRHiredMass_WebOne", UserNo, Generic.ToInt(fmrhiredmassno))
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
        Next
        hifEmployeeNo.Value = fmrhiredmassno
        Try
            cboPlantillaNo.DataSource = SQLHelper.ExecuteDataSet("EMRPlantilla_WebLookup", UserNo, TransNo)
            cboPlantillaNo.DataValueField = "tno"
            cboPlantillaNo.DataTextField = "tcode"
            cboPlantillaNo.DataBind()

        Catch ex As Exception

        End Try
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
        Dim plantillano As Integer = Generic.ToInt(cboPlantillaNo.SelectedValue)
        Dim hrantypeno As Integer = Generic.ToInt(cboHRANTypeNo.SelectedValue)

        If SQLHelper.ExecuteNonQuery("EMRHiredMass_WebSave_Plantilla", UserNo, hifEmployeeNo.Value, plantillano, hrantypeno) > 0 Then
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
    Protected Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lbl As New Label, chk As New CheckBox, lblProceed As New Label, lnkMessage As New Label
        Dim tcount As Integer, SaveCount As Integer = 0
        Dim xds As New DataSet, lblmrActivityHiredNo As New Label

        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then

        For tcount = 0 To Me.grdMain.Rows.Count - 1
            lbl = CType(grdMain.Rows(tcount).FindControl("lblNo"), Label)
            chk = CType(grdMain.Rows(tcount).FindControl("txtIsSelect"), CheckBox)
            lblProceed = CType(grdMain.Rows(tcount).FindControl("txtIsProceed"), Label)
            lnkMessage = CType(grdMain.Rows(tcount).FindControl("lblMessage"), Label)
            lblmrActivityHiredNo = CType(grdMain.Rows(tcount).FindControl("lblmrActivityHiredNo"), Label)
            Dim MRHiredMassNo As Integer = Generic.ToInt(lbl.Text)

            If Generic.ToBol(chk.Checked) = True Then
                If Generic.ToBol(lblProceed.Text) = True Then
                    If SQLHelper.ExecuteNonQuery("EMR_WebForward", UserNo, MRHiredMassNo, TransNo, PayLocNo) > 0 Then
                        SaveCount = SaveCount + 1
                    End If
                    If SaveCount > 0 Then
                        If SQLHelper.ExecuteNonQuery("EMRActivityHired_WebUpdate", UserNo, Generic.ToInt(lblmrActivityHiredNo.Text), 1, ActionStatNo) > 0 Then
                        End If
                    End If
                Else
                    MessageBox.Alert(lnkMessage.Text.ToString, "warning", Me)
                End If
            End If
        Next

        If SaveCount > 0 Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

        'Else
        'MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        'End If

    End Sub
End Class

