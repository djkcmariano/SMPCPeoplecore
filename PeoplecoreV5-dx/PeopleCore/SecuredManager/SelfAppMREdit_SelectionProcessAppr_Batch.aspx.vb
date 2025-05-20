Imports clsLib
Imports System.Data

Partial Class SecuredManager_SelfAppMREdit_SelectionProcessAppr_Batch
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

        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            Generic.PopulateDropDownList_Self(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList_Self(UserNo, Me, "pnlPopupDetl", PayLocNo)
            PopulateGrid()

        End If

        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub


    Protected Sub lnk_Click(sender As Object, e As EventArgs)
        'Info1.xIsApplicant = True
        Dim lnk As New LinkButton
        lnk = sender

        Info1.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
        Info1.xIsApplicant = Generic.ToBol(Generic.Split(lnk.CommandArgument, 1))
        Info1.Show()

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

#Region "Main"
    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EMRInterview_WebManager", UserNo)
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

#End Region

#Region "Detail"
    Protected Sub PopulateDetl(Optional StatusNo As Integer = 0)
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EMRInterviewDeti_WebManager", UserNo, Generic.ToInt(ViewState("TransNo")), Filter1.SearchText, StatusNo)
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


    Protected Sub lnkTemplate_Click(sender As Object, e As EventArgs)
        Dim ib As ImageButton
        ib = sender
        Response.Redirect("~/securedManager/SelfEvalTemplateForm.aspx?id=" & ib.CommandArgument & "&FormName=AppStandardHeader.aspx&TableName=EApplicantStandardHeader")
    End Sub

    Protected Sub btnUpdateDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lbl As New Label, lblInterviewDetiNo As New Label, cboInterviewStatNo As New DropDownList, cboActionStatNo As New DropDownList
        Dim tcount As Integer, SaveCount As Integer = 0
        Dim xds As New DataSet

        For tcount = 0 To Me.grdDetl.Rows.Count - 1
            lblInterviewDetiNo = CType(grdDetl.Rows(tcount).FindControl("lblInterviewDetiNo"), Label)
            lbl = CType(grdDetl.Rows(tcount).FindControl("lblNo"), Label)
            cboInterviewStatNo = CType(grdDetl.Rows(tcount).FindControl("cboInterviewStatNo"), DropDownList)
            cboActionStatNo = CType(grdDetl.Rows(tcount).FindControl("cboActionStatNo"), DropDownList)

            Dim MRInterviewDetiNo As Integer = Generic.ToInt(lblInterviewDetiNo.Text)
            Dim MRHiredMassNo As Integer = Generic.ToInt(lbl.Text)
            Dim ScreeingResultNo As Integer = Generic.ToInt(cboInterviewStatNo.SelectedValue)
            Dim StatusNo As Integer = Generic.ToInt(cboActionStatNo.SelectedValue)

            If Not cboInterviewStatNo Is Nothing Then

                If SQLHelper.ExecuteNonQuery("EMRInterviewDeti_WebUpdate", UserNo, MRInterviewDetiNo, ScreeingResultNo, "") > 0 Then
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

        PopulateGrid()
        MessageBox.Success(MessageTemplate.SuccessSave, Me)

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
        'For tcount = 0 To Me.grdDetl.Rows.Count - 1
        '    Dim cboi As New DropDownList
        '    Dim cboa As New DropDownList
        '    cboi = CType(grdDetl.Rows(tcount).FindControl("cboInterviewStatNo"), DropDownList)
        '    cboa = CType(grdDetl.Rows(tcount).FindControl("cboActionStatNo"), DropDownList)

        '    If cboi.Text <> "1" Then
        '        cboa.Enabled = False
        '        cboa.Text = ""
        '    Else
        '        cboa.Enabled = True
        '    End If
        'Next
        Dim row As GridViewRow
        Dim cboi As New DropDownList
        Dim cboa As New DropDownList
        cboi = CType(sender, DropDownList)
        row = cboi.NamingContainer
        cboa = CType(row.FindControl("cboActionStatNo"), DropDownList)
        cboi = CType(row.FindControl("cboInterviewStatNo"), DropDownList)
        If cboi.SelectedValue = "1" Then
            cboa.Enabled = True
        Else
            cboa.Enabled = False
            cboa.Text = ""
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
            Dim ApplicantNo As Integer = CType(grdDetl.DataKeys(gvrow.RowIndex).Values(3).ToString, Integer)
            Dim EmployeeNo As Integer = CType(grdDetl.DataKeys(gvrow.RowIndex).Values(4).ToString, Integer)

            Response.Redirect("~/securedManager/SelfAppMREdit_EvalTemplateForm.aspx?id=" & TransNo & "&TemplateID=" & TemplateID & "&TransNo=" & MRInterviewDetiNo & "&app=" & ApplicantNo & "&emp=" & EmployeeNo & "&FormName=AppMREdit_SelectionProcess.aspx&TableName=EMRHiredMass")

        Catch ex As Exception
        End Try

    End Sub

#End Region



End Class

