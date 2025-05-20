Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_TrnTakenDetlList
    Inherits System.Web.UI.Page

    Dim clsArray As New clsBase.clsArray
    Dim xScript As String = ""
    Dim UserNo As Int64 = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim tabOrder As Integer = 0
    Dim IsEnabled As Boolean
    Dim tstatus As Integer = 0

    Dim TotalHC As Integer = 0
    Dim ActualHC As Integer = 0
    Dim Waitlisted As Integer = 0
    Dim RemainingSeats As Integer = 0
    Dim NoShow As Integer = 0
    Dim Incomplete As Integer = 0
    Dim Completed As Integer = 0
    Dim NoStatus As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")

        Try

            tabOrder = 0 'Generic.ToInt(cboTabNo.SelectedValue)

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnTakenDetl_Web", UserNo, TransNo, tabOrder)
            Dim dv As DataView = dt.DefaultView
            grdMain.DataSource = dv
            grdMain.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnTakenDetl_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "TrnTakenList.aspx", "ETrnTaken")

        wucTrnHeader1.ID = Generic.ToInt(TransNo)

        If Not IsPostBack Then
            PopulateDropDown()
            PopulateSummary()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)



    End Sub

    Private Sub PopulateSummary()

        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnTaken_WebOne", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                RemainingSeats = Generic.ToInt(row("RemainingSeats"))
                ActualHC = Generic.ToInt(row("ActualHC"))
            Next
        Catch ex As Exception

        End Try

    End Sub

    Private Sub PopulateDropDown()

        Try

            cboTrnPreStatusNo.DataSource = SQLHelper.ExecuteDataSet("ETrnPreStatus_WebLookup", UserNo, PayLocNo)
            cboTrnPreStatusNo.DataValueField = "tNo"
            cboTrnPreStatusNo.DataTextField = "tDesc"
            cboTrnPreStatusNo.DataBind()

            cboPreStatusNo.DataSource = SQLHelper.ExecuteDataSet("ETrnPreStatus_WebLookup", UserNo, PayLocNo)
            cboPreStatusNo.DataValueField = "tNo"
            cboPreStatusNo.DataTextField = "tDesc"
            cboPreStatusNo.DataBind()

            cboEmployeeClassNo.DataSource = SQLHelper.ExecuteDataSet("EEmployeeClass_Weblookup", UserNo, PayLocNo)
            cboEmployeeClassNo.DataValueField = "tNo"
            cboEmployeeClassNo.DataTextField = "tDesc"
            cboEmployeeClassNo.DataBind()

        Catch ex As Exception

        End Try



    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        PopulateGrid()

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "TrnTakenList.aspx", "ETrnTaken") Then
            Generic.ClearControls(Me, "Panel1")
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkAppend_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "TrnTakenList.aspx", "ETrnTaken") Then
            Generic.ClearControls(Me, "Panel3")
            Generic.PopulateDropDownList(UserNo, Me, "Panel3", Generic.ToInt(Session("xPayLocNo")))
            mdlAppend.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim TrnTakenDetlNo As Integer = Generic.ToInt(Me.txtTrnTakenDetlNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim ActualIn As String = Generic.ToStr(Replace(txtActualIn.Text, ":", ""))
        Dim ActualOut As String = Generic.ToStr(Replace(txtActualOut.Text, ":", ""))
        Dim CertStartDate As String = Generic.ToStr(txtCertStartDate.Text)
        Dim ServStartDate As String = Generic.ToStr(txtServStartDate.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)
        Dim TrnPreStatusNo As Integer = Generic.ToInt(cboTrnPreStatusNo.SelectedValue)
        Dim TrnPostStatusNo As Integer = Generic.ToInt(cboTrnPostStatusNo.SelectedValue)
        Dim IsRequired As Boolean = True

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("ETrnTakenDetl_WebValidate", UserNo, TrnTakenDetlNo, TransNo, EmployeeNo, ActualIn, ActualOut, CertStartDate, ServStartDate, Remarks, TrnPreStatusNo, TrnPostStatusNo, 1)
        For Each row As DataRow In dt.Rows
            invalid = Generic.ToBol(row("Invalid"))
            messagedialog = Generic.ToStr(row("MessageDialog"))
            alerttype = Generic.ToStr(row("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlMain.Show()
            Exit Sub
        End If
        '//end here

        If SQLHelper.ExecuteNonQuery("ETrnTakenDetl_WebSave", UserNo, TrnTakenDetlNo, TransNo, EmployeeNo, ActualIn, ActualOut, CertStartDate, ServStartDate, Remarks, TrnPreStatusNo, TrnPostStatusNo, IsRequired) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkSaveApp_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim PositionNo As Integer = Generic.ToInt(cboPositionNo.SelectedValue)
        Dim DivisionNo As Integer = Generic.ToInt(cboDivisionNo.SelectedValue)
        Dim DepartmentNo As Integer = Generic.ToInt(cboDepartmentNo.SelectedValue)
        Dim SectionNo As Integer = Generic.ToInt(cboSectionNo.SelectedValue)
        Dim UnitNo As Integer = Generic.ToInt(cboUnitNo.SelectedValue)
        Dim LocationNo As Integer = Generic.ToInt(cboLocationNo.SelectedValue)
        Dim EmployeeClassNo As Integer = Generic.ToInt(cboEmployeeClassNo.SelectedValue)
        Dim GenderNo As Integer = Generic.ToInt(cboGenderNo.SelectedValue)
        Dim i As Integer = 0

        'Dim dt As DataTable
        'dt = SQLHelper.ExecuteDataTable("ETrnTakenDetl_WebAppend", UserNo, TransNo, PositionNo, DivisionNo, DepartmentNo, SectionNo, UnitNo, PayLocNo, EmployeeClassNo, GenderNo)
        'For Each row As DataRow In dt.Rows
        '    i = Generic.ToInt(row("tCount"))
        '    Retval = True
        'Next

        If SQLHelper.ExecuteNonQuery("ETrnTakenDetl_WebAppend", UserNo, TransNo, PositionNo, DivisionNo, DepartmentNo, SectionNo, UnitNo, LocationNo, EmployeeClassNo, GenderNo, PayLocNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            PopulateGrid()
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If


    End Sub


    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "TrnTakenList.aspx", "ETrnTaken") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TrnTakenDetlNo"}))
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "TrnTakenList.aspx", "ETrnTaken") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TrnTakenDetlNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ETrnTakenDetl", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub


#Region "********Enrollment Status********"

    Protected Sub lnkPreStatus_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)

            Generic.ClearControls(Me, "pnlPopupPre")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupPre", Generic.ToInt(Session("xPayLocNo")))
            IsEnabled = True
            Generic.EnableControls(Me, "pnlPopupPre", IsEnabled)
            lnkSavePre.Enabled = IsEnabled
            mdlShowPre.Show()
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub lnkSavePre_Click(sender As Object, e As EventArgs)
        Try
            Dim str As String = "", i As Integer = 0
            For j As Integer = 0 To grdMain.VisibleRowCount - 1
                If grdMain.Selection.IsRowSelected(j) Then
                    Dim x As Integer = Generic.ToInt(grdMain.GetRowValues(j, "TrnTakenDetlNo"))
                    PreTransaction(x, Generic.ToInt(cboPreStatusNo.SelectedValue))
                    grdMain.Selection.UnselectRow(j)
                    i = i + 1
                End If
            Next

            If i > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub PreTransaction(TrnTakenDetlNo As Integer, TrnPreStatusNo As Integer)

        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("ETrnTakenDetl_WebApproved", UserNo, TrnTakenDetlNo, TrnPreStatusNo, "")
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

#End Region


#Region "********Post Status********"


    Protected Sub lnkPostStatus_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)

            Generic.ClearControls(Me, "pnlPopupPost")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupPost", Generic.ToInt(Session("xPayLocNo")))
            IsEnabled = True
            Generic.EnableControls(Me, "pnlPopupPost", IsEnabled)
            lnkSavePost.Enabled = IsEnabled
            mdlShowPost.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSavePost_Click(sender As Object, e As EventArgs)
        Try

            Dim str As String = "", i As Integer = 0
            For j As Integer = 0 To grdMain.VisibleRowCount - 1
                If grdMain.Selection.IsRowSelected(j) Then
                    Dim x As Integer = Generic.ToInt(grdMain.GetRowValues(j, "TrnTakenDetlNo"))
                    PostTransaction(x, Generic.ToInt(cboPostStatusNo.SelectedValue))
                    grdMain.Selection.UnselectRow(j)
                    i = i + 1
                End If
            Next

            If i > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub PostTransaction(TrnTakenDetlNo As Integer, TrnPostStatusNo As Integer)

        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("ETrnTakenDetl_WebPostStatus", UserNo, TrnTakenDetlNo, TrnPostStatusNo, "")
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
                Response.Redirect("~/Secured/TrnTakenDetlEval.aspx?TemplateID=" & TemplateID & "&id=" & TrnTakenNo & "&TransNo=" & TrnTakenNo & "&emp=" & EmployeeNo & "&IsEnabled=" & IsAllowEdit)
            Else
                MessageBox.Information("No evaluation template defined", Me)
            End If

        Else
            MessageBox.Information("No item selected", Me)
        End If

    End Sub

    Protected Sub Grid_ContextMenuItemVisibility(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuItemVisibilityEventArgs)

        Dim VisiblePre As Boolean
        Dim VisiblePost As Boolean

        VisiblePre = True
        VisiblePost = True

        If e.MenuType = GridViewContextMenuType.Rows Then
            Dim ItemPre As GridViewContextMenuItem = TryCast(e.Items.Find(Function(item) item.Text = "Pre Evaluation"), GridViewContextMenuItem)
            Dim ItemPost As GridViewContextMenuItem = TryCast(e.Items.Find(Function(item) item.Text = "Post Evaluation"), GridViewContextMenuItem)

            If grdMain.VisibleRowCount > 0 Then
                For i As Integer = 0 To grdMain.VisibleRowCount - 1
                    e.SetVisible(ItemPre, i, VisiblePre)
                    e.SetVisible(ItemPost, i, VisiblePost)

                    Dim TrnTitleNo As Integer = Generic.ToInt(grdMain.GetRowValues(i, "TrnTitleNo"))

                    Dim PreTemplateID As Integer
                    Dim PostTemplateID As Integer
                    Dim dt As New DataTable
                    dt = SQLHelper.ExecuteDataTable("ETrnStandardMain_Web", UserNo, TrnTitleNo)
                    For Each row As DataRow In dt.Select("ApplicantScreenTypeNo=2")
                        PreTemplateID = Generic.ToStr(row("ApplicantStandardMainNo"))
                    Next

                    For Each row As DataRow In dt.Select("ApplicantScreenTypeNo=3")
                        PostTemplateID = Generic.ToStr(row("ApplicantStandardMainNo"))
                    Next

                    If PreTemplateID = 0 Then
                        e.SetEnabled(ItemPre, i, False)
                    End If

                    If PostTemplateID = 0 Then
                        e.SetEnabled(ItemPost, i, False)
                    End If

                Next i
            Else
                e.SetVisible(ItemPre, False)
                e.SetVisible(ItemPost, False)
            End If

        End If

    End Sub

#End Region


#Region "********Footer Summary********"

    Protected Sub ASPxGridView1_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles grdMain.CustomSummaryCalculate
        ' Initialization.
        Dim currRow As Integer = e.RowHandle
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
            If e.Item.FieldName = "TrnPreStatusDesc" Then
                TotalHC = 0
                RemainingSeats = 0
                ActualHC = 0
                Waitlisted = 0
            End If

            If e.Item.FieldName = "TrnPostStatusDesc" Then
                NoShow = 0
                Incomplete = 0
                Completed = 0
                NoStatus = 0
            End If
        End If

        ' Calculation.
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
            If e.Item.FieldName = "TrnPreStatusDesc" Then
                TotalHC += 1
                RemainingSeats = Convert.ToDouble(Generic.ToDec(grdMain.GetRowValues(currRow, "RemainingSeats")))
                ActualHC = Convert.ToDouble(Generic.ToDec(grdMain.GetRowValues(currRow, "ActualHC")))
            End If

            If Generic.ToStr(e.GetValue("TrnPreStatusDesc")) = "Waitlisted" Then
                Waitlisted += 1
            End If

            If Generic.ToStr(e.GetValue("TrnPostStatusDesc")) = "No Show" Then
                NoShow += 1
            End If

            If Generic.ToStr(e.GetValue("TrnPostStatusDesc")) = "Incomplete" Then
                Incomplete += 1
            End If

            If Generic.ToStr(e.GetValue("TrnPostStatusDesc")) = "Completed" Then
                Completed += 1
            End If

            'If Generic.ToStr(e.GetValue("TrnPostStatusDesc")) = "" Then
            '    NoStatus += 1
            'End If
        End If

        ' Finalization.
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize Then
            If e.Item.FieldName = "TrnPreStatusDesc" Then
                e.TotalValue = "Enrolled = " + ActualHC.ToString + "<br/>" + "Remaining Seats = " + RemainingSeats.ToString + "<br/>" + "Waitlisted = " + Waitlisted.ToString
            End If

            If e.Item.FieldName = "TrnPostStatusDesc" Then
                e.TotalValue = "No Show = " + NoShow.ToString + "<br/>" + "Incomplete = " + Incomplete.ToString + "<br/>" + "Completed = " + Completed.ToString
            End If
        End If
    End Sub

#End Region

End Class
