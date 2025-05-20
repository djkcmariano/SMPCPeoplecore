Imports clsLib
Imports System.Data

Partial Class SecuredManager_SelfAppMREdit_ApplicantAppr
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim ActionStatNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EMRHiredMass_Web", UserNo, TransNo, Filter1.SearchText, ActionStatNo)
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
        hidTransNo.Value = TransNo
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            PopulateTabHeader()
            PopulateGrid()
            AutoCompleteExtender1.ContextKey = Generic.ToInt(cboHiringAlternativeNo.SelectedValue) & "|" & TransNo
        End If
        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)

        Dim ib As New ImageButton
        ib = sender
        ModalPopupExtender1.Show()

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        For i As Integer = 0 To Me.grdMain.Rows.Count - 1
            chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
            ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
            If chk.Checked = True Then
                Generic.DeleteRecordAuditCol("EMRInterviewDeti", UserNo, "MRHiredMassNo", Generic.ToInt(ib.CommandArgument))
                Generic.DeleteRecordAudit("EMRHiredMass", UserNo, Generic.ToInt(ib.CommandArgument))
                Count = Count + 1
            End If
        Next
        MessageBox.Success("(" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()

    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
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

        Generic.ClearControls(Me, "Panel1")
        ModalPopupExtender1.Show()

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

        Dim lbl As New Label, cboActionStatNo As New DropDownList
        Dim tcount As Integer, SaveCount As Integer = 0
        Dim xds As New DataSet

        For tcount = 0 To Me.grdMain.Rows.Count - 1
            lbl = CType(grdMain.Rows(tcount).FindControl("lblNo"), Label)
            cboActionStatNo = CType(grdMain.Rows(tcount).FindControl("cboActionStatNo"), DropDownList)

            Dim MRHiredMassNo As Integer = Generic.ToInt(lbl.Text)
            Dim StatusNo As Integer = Generic.ToInt(cboActionStatNo.SelectedValue)

            If Not cboActionStatNo Is Nothing Then
                If SQLHelper.ExecuteNonQuery("EMRHiredMass_WebUpdate", UserNo, MRHiredMassNo, 0, StatusNo, ActionStatNo) > 0 Then
                    SaveCount = SaveCount + 1
                End If
            End If

        Next

        PopulateGrid()
        MessageBox.Success(MessageTemplate.SuccessSave, Me)

    End Sub

End Class
