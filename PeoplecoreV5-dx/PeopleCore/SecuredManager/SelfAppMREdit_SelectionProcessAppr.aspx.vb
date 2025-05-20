Imports clsLib
Imports System.Data

Partial Class SecuredManager_SelfAppMREdit_SelectionProcessAppr
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim ActionStatNo As Integer = 2
    Dim InterviewStatNo As Integer
    Dim dtVal As DataTable
    Dim clsGen As New clsGenericClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        hidTransNo.Value = TransNo

        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            Generic.PopulateDropDownList_Self(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList_Self(UserNo, Me, "pnlPopupDetl", PayLocNo)
            PopulateTabHeader()
            PopulateGrid()
            AutoCompleteExtender1.ContextKey = Generic.ToInt(cboHiringAlternativeNo.SelectedValue) & "|" & TransNo
        End If

        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
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

    Protected Sub lnkHistory_Click(sender As Object, e As EventArgs)
        'Info1.xIsApplicant = True
        Dim lnk As New LinkButton
        lnk = sender

        History.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
        History.xIsApplicant = Generic.ToBol(Generic.Split(lnk.CommandArgument, 1))
        History.Show()
    End Sub

    Protected Sub lnkFilter1_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim ib As New LinkButton
        ib = sender
        Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
        rowno = gvrow.RowIndex
        Me.grdMain.SelectedIndex = Generic.ToInt(rowno)
        ViewState("TransNo") = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
        ViewState("TransCode") = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
        PopulateDetl(1)

    End Sub

    Protected Sub lnkFilter2_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim ib As New LinkButton
        ib = sender

        Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
        rowno = gvrow.RowIndex
        Me.grdMain.SelectedIndex = Generic.ToInt(rowno)
        ViewState("TransNo") = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
        ViewState("TransCode") = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
        PopulateDetl(2)

    End Sub

    Protected Sub lnkFilter3_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim ib As New LinkButton
        ib = sender

        Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
        rowno = gvrow.RowIndex
        Me.grdMain.SelectedIndex = Generic.ToInt(rowno)
        ViewState("TransNo") = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
        ViewState("TransCode") = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
        PopulateDetl(3)

    End Sub

    Protected Sub lnkFilter4_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim ib As New LinkButton
        ib = sender

        Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
        rowno = gvrow.RowIndex
        Me.grdMain.SelectedIndex = Generic.ToInt(rowno)
        ViewState("TransNo") = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
        ViewState("TransCode") = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
        PopulateDetl(4)

    End Sub

#Region "Main"
    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EMRInterview_Web", UserNo, TransNo)
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

            If Generic.ToInt(ViewState("TransNo")) = 0 And dt.Rows.Count > 0 Then
                grdMain.SelectedIndex = 0
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

    Protected Sub grdMain_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
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

    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            grdMain.PageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)


        Dim ib As New ImageButton
        ib = sender
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMRInterview_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "pnlPopupMain", dt)
        Next
        mdlMain.Show()

    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)


        Generic.ClearControls(Me, "pnlPopupMain")
        mdlMain.Show()


    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0

        For i As Integer = 0 To Me.grdMain.Rows.Count - 1
            chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
            ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
            If chk.Checked = True Then
                Generic.DeleteRecordAuditCol("EMRInterviewDeti", UserNo, "MRInterviewNo", Generic.ToInt(ib.CommandArgument))
                Generic.DeleteRecordAudit("EMRInterview", UserNo, Generic.ToInt(ib.CommandArgument))
                Count = Count + 1
            End If
        Next
        If Count > 0 Then
            PopulateGrid()
            MessageBox.Success("(" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim transNo = Generic.ToInt(txtMRInterviewNo.Text)
        Dim DateFrom As String = Generic.ToStr(txtScreeningDateFrom.Text)
        Dim DateTo As String = Generic.ToStr(txtScreeningDateTo.Text)
        Dim Time As String = Generic.ToStr(txtScreeningTime.Text)
        Dim Venue As String = Generic.ToStr(txtScreeningVenue.Text)
        Dim ScreeningByNo As Integer = Generic.ToInt(Me.hifScreeningByNo.Value)
        Dim FacilitatorNo As Integer = Generic.ToInt(Me.hifFacilitatorNo.Value)

        If SQLHelper.ExecuteNonQuery("EMRInterview_WebSave", UserNo, transNo, DateFrom, DateTo, Time, Venue, ScreeningByNo, FacilitatorNo) > 0 Then
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

#Region "Detail"
    Protected Sub PopulateDetl(Optional StatusNo As Integer = 0)
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EMRInterviewDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")), Filter1.SearchText, StatusNo)
            dtVal = dt
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

            'Me.lblDetl.Text = "Reference No.: " & Generic.ToStr(ViewState("TransCode"))
            Me.lblDetl.Text = "List of Applicant(s)"

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


        Dim ib As New ImageButton
        ib = sender

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMRInterviewDeti_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "pnlPopupSched", dt)
        Next

        PopulateScreening()
        mdlSched.Show()

    End Sub

    Protected Sub btnAddDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)


        Generic.ClearControls(Me, "pnlPopupDetl")
        mdlDetl.Show()

    End Sub

    Protected Sub btnDeleteDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim MRInterviewNo As Integer = Generic.ToInt(ViewState("TransNo"))
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0

        For i As Integer = 0 To Me.grdDetl.Rows.Count - 1
            chk = CType(grdDetl.Rows(i).FindControl("txtIsSelect"), CheckBox)
            ib = CType(grdDetl.Rows(i).FindControl("btnEditDetl"), ImageButton)
            If chk.Checked = True Then
                Generic.DeleteRecordAudit("EMRInterviewDeti", UserNo, Generic.ToInt(ib.CommandArgument))
                Count = Count + 1
            End If
        Next
        If Count > 0 Then
            ViewState("TransNo") = MRInterviewNo
            PopulateGrid()
            MessageBox.Success("(" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If


    End Sub

    Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim MRInterviewNo As Integer = Generic.ToInt(ViewState("TransNo"))
        Dim HiringAlternativeNo As Integer = Generic.ToInt(cboHiringAlternativeNo.SelectedValue)
        Dim hidID As Integer = Generic.ToInt(Me.hidID.Value)

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EMRInterviewDeti_WebValidate", UserNo, MRInterviewNo, TransNo, hidID, HiringAlternativeNo)
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

        If SQLHelper.ExecuteNonQuery("EMRInterviewDeti_WebSave", UserNo, MRInterviewNo, TransNo, hidID, HiringAlternativeNo) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal Then
            ViewState("TransNo") = MRInterviewNo
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkTemplate_Click(sender As Object, e As EventArgs)
        Dim ib As ImageButton
        ib = sender
        Response.Redirect("~/securedManager/EvalTemplateForm.aspx?id=" & ib.CommandArgument & "&FormName=AppStandardHeader.aspx&TableName=EApplicantStandardHeader")
    End Sub

    Protected Sub btnUpdateDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lbl As New Label, lblInterviewDetiNo As New Label, cboInterviewStatNo As New DropDownList, cboActionStatNo As New DropDownList, txt As New TextBox
        Dim tcount As Integer, SaveCount As Integer = 0
        Dim xds As New DataSet

        For tcount = 0 To Me.grdDetl.Rows.Count - 1
            lblInterviewDetiNo = CType(grdDetl.Rows(tcount).FindControl("lblInterviewDetiNo"), Label)
            lbl = CType(grdDetl.Rows(tcount).FindControl("lblNo"), Label)
            cboInterviewStatNo = CType(grdDetl.Rows(tcount).FindControl("cboInterviewStatNo"), DropDownList)
            cboActionStatNo = CType(grdDetl.Rows(tcount).FindControl("cboActionStatNo"), DropDownList)
            txt = CType(grdDetl.Rows(tcount).FindControl("txtRemarks"), TextBox)

            Dim MRInterviewDetiNo As Integer = Generic.ToInt(lblInterviewDetiNo.Text)
            Dim MRHiredMassNo As Integer = Generic.ToInt(lbl.Text)
            Dim ScreeingResultNo As Integer = Generic.ToInt(cboInterviewStatNo.SelectedValue)
            Dim StatusNo As Integer = Generic.ToInt(cboActionStatNo.SelectedValue)
            Dim chk = CType(grdDetl.Rows(tcount).FindControl("txtIsSelect"), CheckBox)
            Dim Remarks As String = Generic.ToStr(txt.Text)

            If Not cboInterviewStatNo Is Nothing And chk.Checked = True Then

                If SQLHelper.ExecuteNonQuery("EMRInterviewDeti_WebUpdate", UserNo, MRInterviewDetiNo, ScreeingResultNo, Remarks) > 0 Then
                    SaveCount = SaveCount + 1
                End If

                If ScreeingResultNo = 1 Then

                    If Not cboActionStatNo Is Nothing Then
                        If SQLHelper.ExecuteNonQuery("EMRHiredMass_WebUpdate", UserNo, MRHiredMassNo, MRInterviewDetiNo, StatusNo, ActionStatNo) > 0 Then
                            SaveCount = SaveCount + 1
                        End If
                    End If

                End If

            End If


        Next

        If SaveCount > 0 Then
            PopulateGrid()
            MessageBox.Success("(" & SaveCount & ") " & MessageTemplate.SuccessUpdate, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

    Protected Sub grdDetl_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdDetl.RowDataBound

        Dim dt As New DataTable
        Dim cboActionStatNo As New DropDownList
        Dim cboInterviewStatNo As New DropDownList
        Dim MRInterviewNo As Integer = 0

        dt = dtVal
        For Each row As DataRow In dt.Rows
            For tcount = 0 To Me.grdDetl.Rows.Count - 1
                'For Dropdown Only
                cboActionStatNo = CType(grdDetl.Rows(tcount).FindControl("cboActionStatNo"), DropDownList)
                cboActionStatNo.Text = Generic.ToStr(dt.Rows(tcount)("ActionStatNo"))
                cboActionStatNo.Enabled = Generic.ToBol(dt.Rows(tcount)("IsEnabled"))
                MRInterviewNo = Generic.ToStr(dt.Rows(tcount)("MRInterviewNo"))

                cboInterviewStatNo = CType(grdDetl.Rows(tcount).FindControl("cboInterviewStatNo"), DropDownList)
                cboInterviewStatNo.Text = Generic.ToStr(dt.Rows(tcount)("InterviewStatNo"))
                cboInterviewStatNo.Enabled = Generic.ToBol(dt.Rows(tcount)("IsEnabled"))


                Try
                    cboActionStatNo.DataSource = SQLHelper.ExecuteDataSet("EActionStat_WebLookup", UserNo, ActionStatNo, MRInterviewNo, PayLocNo)
                    cboActionStatNo.DataTextField = "tdesc"
                    cboActionStatNo.DataValueField = "tno"
                    cboActionStatNo.DataBind()
                Catch ex As Exception

                End Try

                Try
                    'cboInterviewStatNo.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EInterviewStatL", PayLocNo, "", "")
                    cboInterviewStatNo.DataSource = SQLHelper.ExecuteDataSet("EInterviewStat_WebLookup", UserNo, InterviewStatNo, MRInterviewNo, PayLocNo)
                    cboInterviewStatNo.DataTextField = "tdesc"
                    cboInterviewStatNo.DataValueField = "tno"
                    cboInterviewStatNo.DataBind()
                Catch ex As Exception

                End Try

            Next
        Next

    End Sub

    Protected Sub cboInterviewStatNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        For tcount = 0 To Me.grdDetl.Rows.Count - 1
            Dim cboi As New DropDownList
            Dim cboa As New DropDownList
            cboi = CType(grdDetl.Rows(tcount).FindControl("cboInterviewStatNo"), DropDownList)
            cboa = CType(grdDetl.Rows(tcount).FindControl("cboActionStatNo"), DropDownList)

            If cboi.Text <> "1" Then
                cboa.Enabled = False
                cboa.Text = ""
            Else
                cboa.Enabled = True
            End If
        Next

    End Sub

    Protected Sub lnkSaveSched_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim transNo = Generic.ToInt(txtMRInterviewNo.Text)
        Dim IsSchedule As Boolean = Generic.ToBol(txtIsSchedule.Checked)
        Dim DateFrom As String = Generic.ToStr(txtScheduleDateFrom.Text)
        Dim DateTo As String = Generic.ToStr(txtScheduleDateTo.Text)
        Dim Time As String = Generic.ToStr(txtScheduleTime.Text)
        Dim Venue As String = Generic.ToStr(txtScheduleVenue.Text)
        Dim InterviewByNo As Integer = Generic.ToInt(Me.hifInterviewByNo.Value)

        If SQLHelper.ExecuteNonQuery("EMRInterviewDeti_WebSaveEdit", UserNo, transNo, IsSchedule, DateFrom, DateTo, Time, Venue, InterviewByNo) > 0 Then
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

    Protected Sub txtIsScreening_CheckedChanged(sender As Object, e As System.EventArgs)
        PopulateScreening()
        mdlSched.Show()
    End Sub

    Private Sub PopulateScreening()

        If Generic.ToBol(Me.txtIsSchedule.Checked) = True Then
            Me.txtScheduleDateFrom.Enabled = True
            Me.txtScheduleDateTo.Enabled = True
            Me.txtScheduleTime.Enabled = True
            Me.txtScheduleVenue.Enabled = True
            Me.txtInterviewByName.Enabled = True
        Else
            Me.txtScheduleDateFrom.Text = ""
            Me.txtScheduleDateTo.Text = ""
            Me.txtScheduleTime.Text = ""
            Me.txtScheduleVenue.Text = ""
            Me.txtInterviewByName.Text = ""

            Me.txtScheduleDateFrom.Enabled = False
            Me.txtScheduleDateTo.Enabled = False
            Me.txtScheduleTime.Enabled = False
            Me.txtScheduleVenue.Enabled = False
            Me.txtInterviewByName.Enabled = False
        End If

    End Sub

    Protected Sub lnkForm_Click(sender As Object, e As EventArgs)
        Try
            Dim ib As New LinkButton
            ib = sender

            Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
            rowno = gvrow.RowIndex
            Me.grdDetl.SelectedIndex = Generic.ToInt(rowno)
            Dim MRInterviewDetiNo As Integer = grdDetl.DataKeys(gvrow.RowIndex).Values(1).ToString()
            Dim TemplateID As Integer = grdDetl.DataKeys(gvrow.RowIndex).Values(2).ToString()

            Response.Redirect("~/securedManager/SelfAppMREdit_EvalTemplateForm.aspx?id=" & TransNo & "&TemplateID=" & TemplateID & "&TransNo=" & MRInterviewDetiNo & "&FormName=AppMREdit_SelectionProcess.aspx&TableName=EMRHiredMass")

        Catch ex As Exception
        End Try

    End Sub

#End Region



End Class

