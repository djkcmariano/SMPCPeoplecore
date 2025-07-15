Imports clsLib
Imports DevExpress.Xpo.DB.Helpers
Imports RestSharp
Imports System.Data
Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Services.Description

Partial Class Secured_AppMREdit_SelectionProcess
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim ActionStatNo As Integer = 2
    Dim InterviewStatNo As Integer
    Dim dtVal As DataTable
    Dim clsGen As New clsGenericClass
    Dim ComponentNo As Integer = 1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        hidTransNo.Value = TransNo
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
            PopulateTabHeader()
            PopulateGrid()
            AutoCompleteExtender1.ContextKey = Generic.ToInt(cboHiringAlternativeNo.SelectedValue) & "|" & TransNo
            ViewState("MRStatNo") = Generic.ToInt(SQLHelper.ExecuteScalar("SELECT MRSTatNo FROM EMR WHERE MRNo=" & TransNo))
        End If
        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMR_WebTabHeader", UserNo, TransNo)
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

    Protected Sub lnkFilter5_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim ib As New LinkButton
        ib = sender

        Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
        rowno = gvrow.RowIndex
        Me.grdMain.SelectedIndex = Generic.ToInt(rowno)
        ViewState("TransNo") = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
        ViewState("TransCode") = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
        PopulateDetl(5)

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
                ViewState("Title") = grdMain.DataKeys(0).Values(2).ToString()
                ViewState("IsOverride") = Generic.ToBol(dt.Rows(0)("IsOverride"))
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
            ViewState("Title") = grdMain.DataKeys(gvrow.RowIndex).Values(2).ToString()
            ViewState("IsOverride") = ib.CommandArgument
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

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New ImageButton
            ib = sender
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRInterview_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupMain", dt)
            Next
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
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
                MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
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
        'Dim ScreeningByNo2 As Integer = Generic.ToInt(Me.hifScreeningByNo2.Value)
        Dim FacilitatorNo As Integer = Generic.ToInt(Me.hifFacilitatorNo.Value)


        'Dim dt As DataTable, invalid As Boolean = False, message As String = "", alert As String = ""
        'dt = SQLHelper.ExecuteDataTable("EMRInterview_WebValidate", UserNo, transNo, DateFrom, DateTo, Time, Venue, ScreeningByNo, FacilitatorNo)
        'For Each row As DataRow In dt.Rows
        '    invalid = Generic.ToBol(row("tProceed"))
        '    message = Generic.ToStr(row("xMessage"))
        '    alert = Generic.ToStr(row("AlertType"))
        'Next

        'If invalid = True Then
        '    mdlMain.Show()
        '    MessageBox.Alert(message, alert, Me)
        '    Exit Sub
        'End If


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
            dt = SQLHelper.ExecuteDataTable("EMRInterviewDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")), "", StatusNo, ComponentNo)
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
            'Me.lblDetl.Text = "List of Applicant(s)" & "<br />" & Generic.ToStr(ViewState("Title"))
            Me.lblDetl.Text = Generic.ToStr(ViewState("Title"))

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
        '    Dim ib As New ImageButton
        '    ib = sender

        '    Dim dt As DataTable
        '    dt = SQLHelper.ExecuteDataTable("EMRInterviewDeti_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
        '    For Each row As DataRow In dt.Rows
        '        Generic.PopulateData(Me, "pnlPopupSched", dt)
        '    Next

        '    PopulateScreening()
        '    mdlSched.Show()

        'Else
        '    MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        'End If

    End Sub

    Protected Sub btnAddDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlpopupdetl")
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub btnDeleteDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim MRInterviewNo As Integer = Generic.ToInt(ViewState("TransNo"))
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
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
                MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
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
        'Dim ib As ImageButton
        'ib = sender
        'Response.Redirect("~/secured/EvalTemplateForm.aspx?id=" & ib.CommandArgument & "&FormName=AppStandardHeader.aspx&TableName=EApplicantStandardHeader")
    End Sub

    Protected Async Sub btnUpdateDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lbl As New Label, lblInterviewDetiNo As New Label, cboInterviewStatNo As New DropDownList, cboActionStatNo As New DropDownList, txt As New TextBox, SchedNo As New Label
        Dim tcount As Integer, SaveCount As Integer = 0, lblOverride As New Label, ActionCount As Integer = 0
        Dim xds As New DataSet
        Dim ScreeningResultNo As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then

            For tcount = 0 To Me.grdDetl.Rows.Count - 1
                lblInterviewDetiNo = CType(grdDetl.Rows(tcount).FindControl("lblInterviewDetiNo"), Label)
                lbl = CType(grdDetl.Rows(tcount).FindControl("lblNo"), Label)
                lblOverride = CType(grdDetl.Rows(tcount).FindControl("lblOverride"), Label)
                cboInterviewStatNo = CType(grdDetl.Rows(tcount).FindControl("cboInterviewStatNo"), DropDownList)
                cboActionStatNo = CType(grdDetl.Rows(tcount).FindControl("cboActionStatNo"), DropDownList)
                txt = CType(grdDetl.Rows(tcount).FindControl("txtRemarks"), TextBox)
                SchedNo = CType(grdDetl.Rows(tcount).FindControl("lblScheduleNo"), Label)

                Dim MRInterviewDetiNo As Integer = Generic.ToInt(lblInterviewDetiNo.Text)
                Dim MRHiredMassNo As Integer = Generic.ToInt(lbl.Text)
                ScreeningResultNo = Generic.ToInt(cboInterviewStatNo.SelectedValue)
                Dim StatusNo As Integer = Generic.ToInt(cboActionStatNo.SelectedValue)
                Dim chk = CType(grdDetl.Rows(tcount).FindControl("txtIsSelect"), CheckBox)
                Dim IsOverride As Boolean = Generic.ToBol(lblOverride.Text)
                Dim Remarks As String = Generic.ToStr(txt.Text)
                Dim MRScheduleNo = Generic.ToInt(SchedNo.Text)


                If Not cboInterviewStatNo Is Nothing And chk.Checked = True Then

                    SaveCount = SaveCount + 1
                    SQLHelper.ExecuteNonQuery("EMRInterviewDeti_WebUpdate", UserNo, MRInterviewDetiNo, ScreeningResultNo, Remarks)
                    SQLHelper.ExecuteNonQuery("EMRHiredMass_WebUpdate", UserNo, MRHiredMassNo, MRInterviewDetiNo, StatusNo, ActionStatNo)

                    Try
                        Dim client = Await GetClientAsync()
                        Try
                            Await ProcessData(client, Generic.ToStr(MRInterviewDetiNo), "EMRInterviewDeti")
                            Await ProcessData(client, Generic.ToStr(MRHiredMassNo), "EMRHiredMass")
                            Await ProcessData(client, Generic.ToStr(MRScheduleNo), "EMRSchedule")
                        Finally
                            client.Dispose()
                        End Try

                    Catch ex As Exception
                        Console.WriteLine(ex)
                    End Try
                End If
            Next

            If SaveCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" & SaveCount & ") " & MessageTemplate.SuccessUpdate, Me)
            ElseIf ActionCount > 0 Then
                MessageBox.Warning("Action is required.", Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
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
        'For tcount = 0 To Me.grdDetl.Rows.Count - 1
        'Dim cboi As New DropDownList
        'Dim cboa As New DropDownList
        'cboi = CType(grdDetl.Rows(tcount).FindControl("cboInterviewStatNo"), DropDownList)
        'cboa = CType(grdDetl.Rows(tcount).FindControl("cboActionStatNo"), DropDownList)

        'If cboi.Text <> "1" Then
        ' cboa.Enabled = False
        ' cboa.Text = ""
        ' Else
        ' cboa.Enabled = True
        ' End If
        'Next
        Dim row As GridViewRow
        Dim cboi As New DropDownList
        Dim cboa As New DropDownList
        Dim rfv As New RequiredFieldValidator
        cboi = CType(sender, DropDownList)
        row = cboi.NamingContainer
        cboa = CType(row.FindControl("cboActionStatNo"), DropDownList)
        cboi = CType(row.FindControl("cboInterviewStatNo"), DropDownList)
        rfv = CType(row.FindControl("RequiredFieldValidator1"), RequiredFieldValidator)
        'If Generic.ToBol(ViewState("IsOverride")) Then
        'cboa.Enabled = True
        'Else
        If cboi.SelectedValue = "1" Or cboi.SelectedValue = "5" Then
            cboa.Enabled = True
            rfv.Enabled = True
        Else
            cboa.Enabled = False
            cboa.Text = ""
            rfv.Enabled = False
        End If
        'End If


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
            Dim ApplicantNo As Integer = CType(grdDetl.DataKeys(gvrow.RowIndex).Values(3).ToString, Integer)
            Dim EmployeeNo As Integer = CType(grdDetl.DataKeys(gvrow.RowIndex).Values(4).ToString, Integer)
            Dim EvalTemplateNo As Integer = CType(grdDetl.DataKeys(gvrow.RowIndex).Values(5).ToString, Integer)
            Dim IsAllowEdit As Boolean = True

            Response.Redirect("~/secured/AppMREdit_EvalTemplateForm.aspx?id=" & TransNo & "&TemplateID=" & EvalTemplateNo & "&TransNo=" & MRInterviewDetiNo & "&app=" & ApplicantNo & "&emp=" & EmployeeNo & "&FormName=AppMREdit_SelectionProcess.aspx&TableName=EMRHiredMass" & "&IsEnabled=" & IsAllowEdit)

        Catch ex As Exception
        End Try

    End Sub

#End Region

#Region "Schedule and Screener"


    Protected Sub lnkScreener_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New LinkButton
            ib = sender

            Generic.ClearControls(Me, "pnlPopupScreener")
            ViewState("MRInterviewDetiNo") = Generic.ToInt(Generic.Split(ib.CommandArgument, 0))
            txtApplicantName2.Text = Generic.ToStr(Generic.Split(ib.CommandArgument, 1))

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRInterviewDeti_WebOne", UserNo, Generic.ToInt(ViewState("MRInterviewDetiNo")))
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupScreener", dt)
            Next
            mdlScreener.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkSaveScreener_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim InterviewByNo As Integer = Generic.ToInt(Me.hifInterviewByNo.Value)

        If SQLHelper.ExecuteNonQuery("EMRInterviewDeti_WebSaveScreener", UserNo, Generic.ToInt(ViewState("MRInterviewDetiNo")), InterviewByNo) > 0 Then
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

    Protected Sub lnkSaveSched_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim MRInterviewDetiNo = Generic.ToInt(txtMRInterviewDetiNo.Text)
        Dim IsSchedule As Boolean = Generic.ToBol(txtIsSchedule.Checked)
        Dim DateFrom As String = Generic.ToStr(txtScheduleDateFrom.Text)
        Dim DateTo As String = Generic.ToStr(txtScheduleDateTo.Text)
        Dim Time As String = Generic.ToStr(txtScheduleTime.Text)
        Dim Venue As String = Generic.ToStr(txtScheduleVenue.Text)
        Dim Notes As String = Generic.ToStr(txtScheduleNotes.Text)
        Dim InterviewByNo As Integer = 0 'Generic.ToInt(Me.hifInterviewByNo.Value)
        Dim MRScheduleNo = Generic.ToInt(txtMRScheduleNo.Text)

        If Time > "" And Generic.ToStr(cboPeriod.SelectedValue) > "" Then
            Time = Time & " " & Generic.ToStr(cboPeriod.SelectedValue)
        End If

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EMRSchedule_WebValidate", UserNo, MRScheduleNo, TransNo, MRInterviewDetiNo, DateFrom, DateTo, Time, Venue, Notes, InterviewByNo, Generic.ToInt(ViewState("MRScheduleStatNo")), ComponentNo)
        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlSched.Show()
            Exit Sub
        End If

        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EMRSchedule_WebSave", UserNo, MRScheduleNo, TransNo, MRInterviewDetiNo, DateFrom, DateTo, Time, Venue, Notes, InterviewByNo, Generic.ToInt(ViewState("MRScheduleStatNo")), ComponentNo)

        For Each row As DataRow In dt.Rows
            'Retval = PopulateSendMail(Generic.ToInt(row("MRScheduleNo")))
            Retval = Generic.ToBol(row("Retval"))
        Next


        If Retval Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkAddSched_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New LinkButton
            ib = sender

            Generic.ClearControls(Me, "pnlPopupSched")
            ViewState("MRScheduleStatNo") = 1
            Dim MRInterviewDetiNo As Integer = Generic.ToInt(Generic.Split(ib.CommandArgument, 0))
            Dim MRScheduleNo As Integer = Generic.ToInt(Generic.Split(ib.CommandArgument, 1))
            Dim ApplicantName As String = Generic.ToStr(Generic.Split(ib.CommandArgument, 2))

            lblTitleSched.Text = "Set Schedule"
            txtMRInterviewDetiNo.Text = MRInterviewDetiNo
            txtMRScheduleNo.Text = MRScheduleNo
            txtApplicantName.Text = ApplicantName

            mdlSched.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkMove_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New LinkButton
            ib = sender

            Generic.ClearControls(Me, "pnlPopupSched")
            ViewState("MRScheduleStatNo") = 5
            Dim MRInterviewDetiNo As Integer = Generic.ToInt(Generic.Split(ib.CommandArgument, 0))
            Dim MRScheduleNo As Integer = Generic.ToInt(Generic.Split(ib.CommandArgument, 1))
            Dim ApplicantName As String = Generic.ToStr(Generic.Split(ib.CommandArgument, 2))

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRInterviewDeti_WebOne", UserNo, MRInterviewDetiNo)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupSched", dt)
            Next

            lblTitleSched.Text = "Move Schedule"
            txtMRInterviewDetiNo.Text = MRInterviewDetiNo
            txtMRScheduleNo.Text = MRScheduleNo
            txtApplicantName.Text = ApplicantName

            mdlSched.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New LinkButton
            ib = sender

            Dim MRInterviewDetiNo As Integer = Generic.ToInt(Generic.Split(ib.CommandArgument, 0))
            Dim MRScheduleNo As Integer = Generic.ToInt(Generic.Split(ib.CommandArgument, 1))
            lblApplicantName.Text = Generic.ToStr(Generic.Split(ib.CommandArgument, 2))

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRSchedule_Web", UserNo, TransNo, MRInterviewDetiNo)
            grdSched.DataSource = dt
            grdSched.DataBind()

            mdlHistory.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkAccept_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New LinkButton
            ib = sender

            ViewState("MRScheduleStatNo") = 2
            Dim MRInterviewDetiNo As Integer = Generic.ToInt(Generic.Split(ib.CommandArgument, 0))
            Dim MRScheduleNo As Integer = Generic.ToInt(Generic.Split(ib.CommandArgument, 1))
            Dim ApplicantName As String = Generic.ToStr(Generic.Split(ib.CommandArgument, 2))

            If PopulateSched_Update(MRScheduleNo, Generic.ToInt(ViewState("MRScheduleStatNo")), "") Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkDecline_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New LinkButton
            ib = sender

            ViewState("MRScheduleStatNo") = 3
            ViewState("MRInterviewDetiNo") = Generic.ToInt(Generic.Split(ib.CommandArgument, 0))
            ViewState("MRScheduleNo") = Generic.ToInt(Generic.Split(ib.CommandArgument, 1))
            txtApplicantName1.Text = Generic.ToStr(Generic.Split(ib.CommandArgument, 2))
            lblTitleCancel.Text = "Decline Schedule"
            lblRemarks.Text = "Preferred  Schedule <i>(if any)</i> :"
            txtRemarks.Text = ""
            mdlCancel.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New LinkButton
            ib = sender

            ViewState("MRScheduleStatNo") = 4
            ViewState("MRInterviewDetiNo") = Generic.ToInt(Generic.Split(ib.CommandArgument, 0))
            ViewState("MRScheduleNo") = Generic.ToInt(Generic.Split(ib.CommandArgument, 1))
            'txtApplicantName1.Text = Generic.ToStr(Generic.Split(ib.CommandArgument, 2))
            'lblTitleCancel.Text = "Cancel Schedule"
            'lblRemarks.Text = "Reason"
            'txtRemarks.Text = ""
            'mdlCancel.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkSaveCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If PopulateSched_Update(Generic.ToInt(ViewState("MRScheduleNo")), Generic.ToInt(ViewState("MRScheduleStatNo")), txtRemarks.Text) Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Private Function PopulateSched_Update(MRScheduleNo As Integer, MRScheduleStatNo As Integer, Remarks As String) As Boolean

        Dim RetVal As Boolean = False
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EMRSchedule_WebUpdate", UserNo, MRScheduleNo, MRScheduleStatNo, Remarks, ComponentNo)
        For Each row As DataRow In dt.Rows
            MRScheduleNo = Generic.ToInt(row("MRScheduleNo"))
            'RetVal = PopulateSendMail(MRScheduleNo)
            RetVal = True
        Next

        If RetVal = False Then
            SQLHelper.ExecuteDataTable("EMRSchedule_WebSentFailed", UserNo, MRScheduleNo)
        End If

        Return RetVal
    End Function

    Private Function PopulateSendMail(MRScheduleNo As Integer) As Boolean

        'Dim Retval As Boolean = False
        'Dim dt As DataTable
        'Dim IsSendMail As Boolean = False
        'Dim EmailTo As String, EmailFrom As String, EmailSubject As String, EmailBody As String
        'Dim e_ip_addr As String = ""
        'Dim e_port_no As String = ""
        'Dim e_login As String = ""
        'Dim e_pwd As String = ""
        'Dim e_ssl As Boolean = False
        'Dim DocNo As Integer = 0
        'Dim FileName As String = ""

        'dt = SQLHelper.ExecuteDataTable("EMRSchedule_WebEmail", UserNo, MRScheduleNo, ComponentNo)
        'For Each row As DataRow In dt.Rows
        '    IsSendMail = Generic.ToBol(row("IsSendMail"))
        '    EmailFrom = Generic.ToStr(row("EmailFrom"))
        '    EmailTo = Generic.ToStr(row("EmailTo"))
        '    EmailSubject = Generic.ToStr(row("EmailSubject"))
        '    EmailBody = Generic.ToStr(row("EmailBody"))
        '    e_ip_addr = Generic.ToStr(row("IPAddress"))
        '    e_port_no = Generic.ToStr(row("PortNo"))
        '    e_login = Generic.ToStr(row("elogin"))
        '    e_pwd = Generic.ToStr(row("epwd"))
        '    e_ssl = Generic.ToBol(row("essl"))
        '    DocNo = Generic.ToInt(row("DocNo"))
        '    FileName = Generic.ToStr(row("FileName"))

        '    If IsSendMail = True Then
        '        Try
        '            Dim sm As System.Net.Mail.SmtpClient = New System.Net.Mail.SmtpClient(e_ip_addr, e_port_no)
        '            sm.EnableSsl = e_ssl
        '            If e_login.Length > 0 Then
        '                sm.Credentials = New System.Net.NetworkCredential(e_login, e_pwd)
        '            Else
        '                sm.UseDefaultCredentials = True
        '            End If

        '            Dim mm As New System.Net.Mail.MailMessage
        '            mm.Subject = EmailSubject
        '            mm.Body = EmailBody
        '            mm.IsBodyHtml = True
        '            mm.To.Add(EmailTo)
        '            mm.From = New System.Net.Mail.MailAddress(EmailFrom)

        '            Dim bytes_DocFile As Byte() = Nothing
        '            If DocNo > 0 Then
        '                bytes_DocFile = PopulateDocFile(DocNo)

        '                If Not bytes_DocFile Is Nothing Then
        '                    mm.Attachments.Add(New Net.Mail.Attachment(New System.IO.MemoryStream(bytes_DocFile), FileName))
        '                End If
        '            End If

        '            sm.Send(mm)

        '            Retval = True

        '        Catch ex As Exception
        '            Retval = False
        '        End Try
        '    Else
        '        Retval = True
        '    End If

        'Next

        'Return Retval

    End Function

    Public Function PopulateDocFile(DocNo As Integer) As Byte()

        Try
            Dim doc As Byte() = Nothing
            Dim filename As String = ""
            Dim orgname As String = ""
            Dim dt As DataTable
            Dim datafile() As Byte = Nothing
            Dim fContentType As String = ""
            Dim fileExt As String = ""
            dt = SQLHelper.ExecuteDataTable("EDoc_WebOne_Attachment", UserNo, DocNo)
            For Each row As DataRow In dt.Rows
                filename = Generic.ToStr(row("ActualFileName"))
                orgname = Generic.ToStr(row("DocFile"))
                'datafile = row("DataFile")
                fContentType = Generic.ToStr(row("contenttype"))
                fileExt = Generic.ToStr(row("docExt"))
            Next

            If datafile Is Nothing And filename > "" Then
                Dim path As String = ConfigurationManager.AppSettings("drive_path")
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & "\" & filename.ToString)
                If file.Exists Then
                    Dim fs As System.IO.FileStream = New System.IO.FileStream(path & "\" & filename.ToString, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read)
                    datafile = New Byte(fs.Length - 1) {}
                End If
            End If

            Return datafile

        Catch ex As Exception

        End Try

    End Function


#End Region


#Region "Sync API"

    'Async Function SyncData(id As Integer) As Task
    '    Try
    '        Dim client = Await GetClientAsync()
    '        Dim operation As String = "Push"

    '        Dim processedCount = 0

    '        'Dim dt As DataTable
    '        'dt = SQLHelper.ExecuteDataTable("GetTableSync", "Applicant", operation)
    '        'For Each row As DataRow In dt.Rows
    '        '    Dim tableName = Generic.ToStr(row("TableName"))
    '        '    Dim limit = Generic.ToStr(row("Limit"))
    '        '    Await ProcessData(client, limit, tableName, operation)
    '        '    processedCount += 1
    '        'Next



    '        Await ProcessData(client, id, tableName, operation)

    '        client.Dispose()
    '    Catch

    '    End Try

    'End Function

    Async Function ProcessData(client As RestSharp.RestClient, id As String, tableName As String) As Task
        Try
            Dim postRequest = New RestRequest("api/push/onedata", Method.Post)
            postRequest.AddJsonBody(New With {
            .id = id,
            .tableName = tableName
        })

            Dim postResponse = Await client.ExecuteAsync(postRequest)

            If postResponse.IsSuccessful AndAlso postResponse.Content IsNot Nothing Then
                Console.WriteLine("Successfully processed " & tableName)
                Return
            Else
                Throw New Exception("Failed to process " & tableName & ". Status: " & postResponse.StatusCode & ", Message: " & postResponse.Content)
            End If
        Catch ex As Exception
            Console.WriteLine("Error processing " & tableName & ": " & ex.Message)
            Throw
        End Try
    End Function


    Public Class Table
        Public Property tableName As String
        Public Property limit As Integer
    End Class


    Private _cachedToken As String = String.Empty
    Private _tokenExpiration As DateTime

    Private _restClient As RestSharp.RestClient

    Public Sub New()
        _restClient = New RestSharp.RestClient(ConfigurationManager.AppSettings("API:BaseURL"))
    End Sub

    Public Async Function GetClientAsync() As Task(Of RestSharp.RestClient)
        Dim token As String = Await GetTokenAsync()
        _restClient.AddDefaultHeader("Authorization", "Bearer " & token)
        Return _restClient
    End Function

    Public Class AuthTokenResponse
        Public Property token As String
        Public Property expiresIn As Integer
    End Class

    Async Function GetTokenAsync() As Task(Of String)

        If Not String.IsNullOrEmpty(_cachedToken) AndAlso _tokenExpiration > DateTime.Now.AddMinutes(1) Then
            Return _cachedToken
        End If
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim client As New RestSharp.RestClient(ConfigurationManager.AppSettings("API:BaseURL"))
            Dim request = New RestRequest("api/auth", Method.Post)

            request.AddJsonBody(New With {
            .username = ConfigurationManager.AppSettings("API:Username"),
            .password = ConfigurationManager.AppSettings("API:Password")})

            Dim response = Await client.ExecuteAsync(Of AuthTokenResponse)(request)
            If response.StatusCode = Net.HttpStatusCode.OK Then
                Return response.Content.Trim("""")
            End If

            _cachedToken = response.Data.token
            _tokenExpiration = DateTime.Now.AddSeconds(response.Data.expiresIn)
            Return _cachedToken
        Catch ex As Exception
            Throw New Exception("Failed to obtain JWT token", ex)
        End Try
    End Function


#End Region


End Class

