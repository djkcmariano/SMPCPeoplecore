Imports System.Data
Imports clsLib
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class SecuredSelf_SelfDTRLeaveApplicationList
    Inherits System.Web.UI.Page

    Dim clsArray As New clsBase.clsArray
    Dim xScript As String = ""
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0
    Dim OnlineEmpNo As Integer = 0
    Dim tstatus As Integer

#Region "Main"

    Protected Sub PopulateGrid(Optional IsMain As Boolean = False)

        Try

            tstatus = Generic.ToInt(cboTabNo.SelectedValue)

            lnkConfirm.Visible = False
            If tstatus = 2 Then 'Enrollment
                lnkConfirm.Visible = True
            End If

            If tstatus = 4 Then 'Posted
                grdMain.Columns("RemainingSeats").Visible = False
                grdMain.Columns("TrnPreStatusDesc").VisibleIndex = 6
                grdMain.Columns("TrnPostStatusDesc").VisibleIndex = 7
                grdMain.Columns("Reject?").Visible = False
                grdMain.Columns("Reason").Visible = False

                grdMain.Columns("Select").VisibleIndex = 10
            Else
                grdMain.Columns("RemainingSeats").Visible = False
                grdMain.Columns("TrnPreStatusDesc").VisibleIndex = 7
                grdMain.Columns("TrnPostStatusDesc").Visible = False
                grdMain.Columns("Reject?").Visible = False
                grdMain.Columns("Reason").Visible = False

                If tstatus = 2 Then 'Enrollment
                    grdMain.Columns("RemainingSeats").VisibleIndex = 6
                    grdMain.Columns("Reject?").VisibleIndex = 8
                End If

                If tstatus = 3 Or tstatus = 5 Then 'Cancelled & Postponed
                    grdMain.Columns("Reason").VisibleIndex = 9
                End If


                grdMain.Columns("Select").VisibleIndex = 11
            End If

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnTakenDetl_WebSelf", UserNo, tstatus, PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()

            Session(xScript & "TabNo") = tstatus

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Generic.ClearControls(Me, "pnlPopup")
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnTakenDetl_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopup", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        OnlineEmpNo = Generic.ToInt(Session("EmployeeNo"))
        Permission.IsAuthenticated()

        xScript = clsArray.GetPath(Request.ServerVariables("SCRIPT_NAME"))

        If Not IsPostBack Then
            cboTabNo.Text = Generic.ToStr(Session(xScript & "TabNo"))
            PopulateDropDown()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList_Self(UserNo, Me, "pnlPopup", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETrnStat_WebLookupSelf", UserNo, PayLocNo)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim URL As String
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TrnTakenNo"})))
        If URL <> "" Then
            Response.Redirect(URL)
        End If
    End Sub

    Protected Sub lnkConfirm_Click(sender As Object, e As EventArgs)
        Try

            Dim str As String = "", i As Integer = 0
            For j As Integer = 0 To grdMain.VisibleRowCount - 1
                If grdMain.Selection.IsRowSelected(j) Then
                    Dim x As Integer = Generic.ToInt(grdMain.GetRowValues(j, "EmployeeNo"))
                    Dim y As Integer = Generic.ToInt(grdMain.GetRowValues(j, "TrnTakenNo"))
                    ApproveTransaction(x, y, 2)
                    grdMain.Selection.UnselectRow(j)
                    i = i + 1
                End If
            Next

            If i > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + i.ToString + ") " + " training(s) successfully confirmed.", Me)
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ApproveTransaction(EmployeeNo As Integer, TrnTakenNo As Integer, TrnPreStatusNo As Integer)

        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("ETrnTakenDetl_WebApprovedSelf", UserNo, TrnTakenNo, EmployeeNo, TrnPreStatusNo, Generic.ToStr(txtReasonNotAttend.Text))
        If fds.Tables.Count > 0 Then
            If fds.Tables(0).Rows.Count > 0 Then
                Dim IsWithapprover As Boolean
                IsWithapprover = Generic.CheckDBNull(fds.Tables(0).Rows(0)("IsWithApprover"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                If IsWithapprover = True Then

                Else
                    MessageBox.Information("Unable to locate the next approver.", Me)
                End If
            End If
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkReject_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            ViewState("EmployeeNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo"}))
            ViewState("TrnTakenNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TrnTakenNo"}))
            ViewState("TrnTakenDetlNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TrnTakenDetlNo"}))

            If Generic.ToInt(ViewState("TrnTakenDetlNo")) > 0 Then
                PopulateData(Generic.ToInt(ViewState("TrnTakenDetlNo")))
            End If

            Dim IsEnabled As Boolean = False
            If Generic.ToInt(cboTabNo.SelectedValue) = 2 Then
                IsEnabled = True
            End If

            Generic.EnableControls(Me, "pnlPopup", IsEnabled)
            lnkSave.Enabled = IsEnabled
            mdlShow.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Try
            ApproveTransaction(Generic.ToInt(ViewState("EmployeeNo")), Generic.ToInt(ViewState("TrnTakenNo")), 6)

            MessageBox.Success("Training successfully rejected.", Me)
            PopulateGrid()

        Catch ex As Exception
            MessageBox.Warning("Error", Me)
        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid(True)
    End Sub

    

#End Region


#Region "********Check All********"

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs)
        If e.ButtonType <> ColumnCommandButtonType.SelectCheckbox Then
            Return
        End If
        Dim rowEnabled As Boolean = getRowEnabledStatus(e.VisibleIndex)
        e.Enabled = rowEnabled
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




#Region "********Context Menu********"


    Protected Sub MyGridView_FillContextMenuItems(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuEventArgs)
        If e.MenuType = GridViewContextMenuType.Rows Then
            e.Items.Add(e.CreateItem("Pre Evaluation", "Refresh"))
            e.Items.Add(e.CreateItem("Post Evaluation", "Refresh"))
        End If
    End Sub

    Protected Sub Grid_ContextMenuItemClick(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuItemClickEventArgs)
        Dim TrnTakenNo As Integer, TemplateID As Integer, EmployeeNo As Integer, TrnTitleNo As Integer, IsAllowEdit As Boolean = False
        If grdMain.VisibleRowCount > 0 Then
            TrnTakenNo = Generic.ToInt(grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "TrnTakenNo"))
            EmployeeNo = Generic.ToInt(grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "EmployeeNo"))
            TrnTitleNo = Generic.ToInt(grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "TrnTitleNo"))
            IsAllowEdit = Generic.ToBol(grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "IsAllowEdit"))
        End If

        If TrnTakenNo > 0 Then
            Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnStandardMain_Web", UserNo, TrnTitleNo)
            Select Case e.Item.Text
                Case "Pre Evaluation"
                    For Each row As DataRow In dt.Select("ApplicantScreenTypeNo=2")
                        TemplateID = Generic.ToStr(row("ApplicantStandardMainNo"))
                    Next

                Case "Post Evaluation"
                    For Each row As DataRow In dt.Select("ApplicantScreenTypeNo=3")
                        TemplateID = Generic.ToStr(row("ApplicantStandardMainNo"))
                    Next
            End Select


            If TemplateID > 0 Then
                Response.Redirect("~/SecuredSelf/SelfTrnTakenEval.aspx?TemplateID=" & TemplateID & "&id=" & TrnTakenNo & "&TransNo=" & TrnTakenNo & "&emp=" & EmployeeNo & "&IsEnabled=" & IsAllowEdit)
            Else
                MessageBox.Information("No evaluation template created", Me)
            End If

        Else
            MessageBox.Information("No item selected", Me)
        End If

    End Sub

    Protected Sub Grid_ContextMenuItemVisibility(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuItemVisibilityEventArgs)

        Dim VisiblePre As Boolean
        Dim VisiblePost As Boolean

        If tstatus = 2 Then 'Enabled Pre Evaluation
            VisiblePre = True
            VisiblePost = False
        ElseIf tstatus = 4 Then 'Enabled Post Evaluation
            VisiblePre = False
            VisiblePost = True
        Else
            VisiblePre = False
            VisiblePost = False
        End If


        If e.MenuType = GridViewContextMenuType.Rows Then
            Dim ItemPre As GridViewContextMenuItem = TryCast(e.Items.Find(Function(item) item.Text = "Pre Evaluation"), GridViewContextMenuItem)
            Dim ItemPost As GridViewContextMenuItem = TryCast(e.Items.Find(Function(item) item.Text = "Post Evaluation"), GridViewContextMenuItem)

            If grdMain.VisibleRowCount > 0 Then
                For i As Integer = 0 To grdMain.VisibleRowCount - 1
                    e.SetVisible(ItemPre, i, VisiblePre)
                    e.SetVisible(ItemPost, i, VisiblePost)

                    Dim TrnTitleNo As Integer = Generic.ToInt(grdMain.GetRowValues(i, "TrnTitleNo"))
                    If tstatus = 2 Then
                        Dim TemplateID As Integer
                        Dim dt As New DataTable
                        dt = SQLHelper.ExecuteDataTable("ETrnStandardMain_Web", UserNo, TrnTitleNo)
                        For Each row As DataRow In dt.Select("ApplicantScreenTypeNo=2")
                            TemplateID = Generic.ToStr(row("ApplicantStandardMainNo"))
                        Next

                        If TemplateID = 0 Then
                            e.SetEnabled(ItemPre, i, False)
                        End If

                    ElseIf tstatus = 4 Then
                        Dim TemplateID As Integer
                        Dim dt As New DataTable
                        dt = SQLHelper.ExecuteDataTable("ETrnStandardMain_Web", UserNo, TrnTitleNo)
                        For Each row As DataRow In dt.Select("ApplicantScreenTypeNo=3")
                            TemplateID = Generic.ToStr(row("ApplicantStandardMainNo"))
                        Next

                        If TemplateID = 0 Then
                            e.SetEnabled(ItemPost, i, False)
                        End If

                    End If

                Next i
            Else
                e.SetVisible(ItemPre, False)
                e.SetVisible(ItemPost, False)
            End If

        End If

    End Sub

#End Region

End Class



