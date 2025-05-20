Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_TrnTakenList
    Inherits System.Web.UI.Page

    Dim clsArray As New clsBase.clsArray
    Dim xScript As String = ""
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim tabOrder As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")

        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            tabOrder = Generic.ToInt(cboTabNo.SelectedValue)
            If tabOrder = 0 Then
                tabOrder = 1
            End If

            grdMain.Columns(5).Visible = False 'Hide Enrollment Period
            grdMain.Columns("Reason").Visible = False

            If tabOrder = 1 Then
                lnkAdd.Visible = True
                lnkDelete.Visible = True
                lnkEnrollment.Visible = True
                lnkPost.Visible = False
            ElseIf tabOrder = 2 Then
                lnkAdd.Visible = False
                lnkDelete.Visible = False
                lnkEnrollment.Visible = False
                lnkPost.Visible = True
                grdMain.Columns(5).VisibleIndex = 5
            ElseIf tabOrder = 3 Then
                lnkAdd.Visible = False
                lnkDelete.Visible = False
                lnkEnrollment.Visible = True
                lnkPost.Visible = False
                grdMain.Columns("Reason").VisibleIndex = 6
                'grdMain.Columns(7).Visible = False
            ElseIf tabOrder = 5 Then
                lnkAdd.Visible = False
                lnkDelete.Visible = False
                lnkEnrollment.Visible = True
                lnkPost.Visible = False
                grdMain.Columns("Reason").VisibleIndex = 6
                'grdMain.Columns(7).Visible = False
            Else
                lnkAdd.Visible = False
                lnkDelete.Visible = False
                lnkEnrollment.Visible = False
                lnkPost.Visible = False
                'grdMain.Columns(7).Visible = False
            End If

            dt = SQLHelper.ExecuteDataTable("ETrnTaken_Web", UserNo, tabOrder, PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()

            Session(xScript & "TabNo") = tabOrder

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        AccessRights.CheckUser(UserNo, Generic.ToStr(Session("xFormName")), Generic.ToStr(Session("xTableName")))

        xScript = clsArray.GetPath(Request.ServerVariables("SCRIPT_NAME"))

        If Not IsPostBack Then
            cboTabNo.Text = Generic.ToStr(Session(xScript & "TabNo"))
            PopulateGrid()
            PopulateDropDown()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub
    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lnk As New LinkButton, i As Integer
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TrnTakenNo"}))

        Response.Redirect("~/secured/TrnTakenDetlList.aspx?id=" & i)

    End Sub


    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETrnStat_WebLookup", UserNo, PayLocNo)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        PopulateGrid()

    End Sub
    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
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

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
                Dim lnk As New ImageButton
                Dim i As Integer = 0

                Response.Redirect(Generic.GetFirstTab(i))
            Else
                MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TrnTakenNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("ETrnTakenCost", UserNo, "TrnTakenNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("ETrnTakenSpeaker", UserNo, "TrnTakenNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("ETrnTakenReso", UserNo, "TrnTakenNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("ETrnTakenModule", UserNo, "TrnTakenNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("ETrnTakenDetl", UserNo, "TrnTakenNo", CType(item, Integer))
                Generic.DeleteRecordAudit("ETrnTaken", UserNo, CType(item, Integer))
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

    Protected Sub lnkEnrollment_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TrnTakenNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("ETrnTaken_WebAction", UserNo, item, 2, "") > 0 Then
                    Count = Count + 1
                End If
                i = i + 1
            Next
            MessageBox.Success("Training program is ready to accept participants!", Me)
            PopulateGrid()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub


    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TrnTakenNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("ETrnTaken_WebAction", UserNo, item, 4, "") > 0 Then
                    Count = Count + 1
                End If
                i = i + 1
            Next
            MessageBox.Success("Training program is posted!", Me)
            PopulateGrid()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub PopulateDataCancel(id As Int64)
        Try
            Dim dt As DataTable
            ViewState("TrnStatNo") = 3
            dt = SQLHelper.ExecuteDataTable("ETrnTaken_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopup", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub PopulateDataPostPoned(id As Int64)
        Try
            Dim dt As DataTable
            ViewState("TrnStatNo") = 5
            dt = SQLHelper.ExecuteDataTable("ETrnTaken_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopup", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim TrnTakenNo As Integer = Generic.ToInt(txtCode.Text)
        Dim Reason As String = Generic.ToStr(txtReason.Text)

        If SQLHelper.ExecuteNonQuery("ETrnTaken_WebAction", UserNo, TrnTakenNo, ViewState("TrnStatNo"), Reason) > 0 Then
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

    Protected Sub lnkEval_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lnk As New LinkButton, i As Integer
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TrnTakenNo"}))
        Dim TrnTitleNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TrnTitleNo"}))

        Dim TemplateID As Integer
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("ETrnStandardMain_Web", UserNo, TrnTitleNo)
        For Each row As DataRow In dt.Select("ApplicantScreenTypeNo=1")
            TemplateID = Generic.ToStr(row("ApplicantStandardMainNo"))
        Next

        If TemplateID > 0 Then
            Response.Redirect("~/secured/TrnTakenEval.aspx?TemplateID=" & TemplateID & "&TransNo=" & i)
        Else
            MessageBox.Information("No training evaluation created", Me)
        End If

    End Sub

#Region "********Context Menu********"


    Protected Sub MyGridView_FillContextMenuItems(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuEventArgs)
        If e.MenuType = GridViewContextMenuType.Rows Then
            e.Items.Add(e.CreateItem("Training Evaluation", "Refresh"))
            e.Items.Add(e.CreateItem("Cancel", "Refresh"))
            e.Items.Add(e.CreateItem("Postpone", "Refresh"))
        End If
    End Sub

    Protected Sub Grid_ContextMenuItemClick(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuItemClickEventArgs)
        Dim id As Integer, TrnTitleNo As Integer
        If grdMain.VisibleRowCount > 0 Then
            id = Generic.ToInt(grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "TrnTakenNo"))
            TrnTitleNo = Generic.ToInt(grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "TrnTitleNo"))
        End If

        If id > 0 Then
            Select Case e.Item.Text
                Case "Cancel"
                    PopulateDataCancel(id)
                    mdl.Show()
                Case "Postpone"
                    PopulateDataPostPoned(id)
                    mdl.Show()
                Case "Training Evaluation"

                    Dim TemplateID As Integer
                    Dim dt As New DataTable
                    dt = SQLHelper.ExecuteDataTable("ETrnStandardMain_Web", UserNo, TrnTitleNo)
                    For Each row As DataRow In dt.Select("ApplicantScreenTypeNo=1")
                        TemplateID = Generic.ToStr(row("ApplicantStandardMainNo"))
                    Next

                    If TemplateID > 0 Then
                        Response.Redirect("~/secured/TrnTakenEval.aspx?TemplateID=" & TemplateID & "&TransNo=" & id)
                    Else
                        MessageBox.Information("No training evaluation created", Me)
                    End If
            End Select
        Else
            MessageBox.Information("No item selected", Me)
        End If

    End Sub

    Protected Sub Grid_ContextMenuItemVisibility(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuItemVisibilityEventArgs)

        Dim ItemVisible As Boolean
        Select Case tabOrder
            Case 1, 2
                ItemVisible = True
            Case Else
                ItemVisible = False
        End Select

        If e.MenuType = GridViewContextMenuType.Rows Then
            Dim ItemCancel As GridViewContextMenuItem = TryCast(e.Items.Find(Function(item) item.Text = "Cancel"), GridViewContextMenuItem)
            Dim ItemPostpone As GridViewContextMenuItem = TryCast(e.Items.Find(Function(item) item.Text = "Postpone"), GridViewContextMenuItem)
            Dim ItemEvaluation As GridViewContextMenuItem = TryCast(e.Items.Find(Function(item) item.Text = "Training Evaluation"), GridViewContextMenuItem)

            If grdMain.VisibleRowCount > 0 Then
                For i As Integer = 0 To grdMain.VisibleRowCount - 1
                    Dim TrnTitleNo As Integer = Generic.ToInt(grdMain.GetRowValues(i, "TrnTitleNo"))
                    Dim TemplateID As Integer
                    Dim dt As New DataTable
                    dt = SQLHelper.ExecuteDataTable("ETrnStandardMain_Web", UserNo, TrnTitleNo)
                    For Each row As DataRow In dt.Select("ApplicantScreenTypeNo=1")
                        TemplateID = Generic.ToStr(row("ApplicantStandardMainNo"))
                    Next

                    e.SetEnabled(ItemCancel, i, ItemVisible)
                    e.SetEnabled(ItemPostpone, i, ItemVisible)
                    If TemplateID = 0 Then
                        e.SetEnabled(ItemEvaluation, i, False)
                    End If
                Next i
            Else
                e.SetVisible(ItemCancel, False)
                e.SetVisible(ItemPostpone, False)
                e.SetVisible(ItemEvaluation, False)
            End If

        End If

    End Sub

#End Region


End Class

















