Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PENormsList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim pereviewmainno As Int64 = 0

    
    Private Sub PopulateGrid(Optional IsMain As Boolean = False)

        Try
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EPEReview_Web", UserNo, pereviewmainno)
            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()

            If ViewState("TransNo") = 0 Then
                Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"PEReviewNo", "Code"})
                ViewState("TransNo") = obj(0)
                lblDetl.Text = obj(1)
            End If
        Catch ex As Exception

        End Try

        PopulateGridDetl()

    End Sub
    

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        pereviewmainno = Generic.ToInt(Request.QueryString("pereviewmainno"))
        If pereviewmainno > 0 Then
            Session("pereviewmainno") = pereviewmainno
        End If
        If Session("pereviewmainno") > 0 Then
            pereviewmainno = Session("pereviewmainno")
        End If

        AccessRights.CheckUser(UserNo, "PEReviewMainList.aspx", "EPEReviewMain")
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
        End If
        PopulateGrid()
    End Sub



#Region "********Main*******"

    

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEReviewNo"}))

                Generic.ClearControls(Me, "pnlPopupMain")
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPEReview_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                Next
                mdlMain.Show()


            Else
                MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEReviewNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteDataSet("EPEReviewMain_WebDelete", UserNo, 0, item)
                Generic.DeleteRecordAudit("EPEReview", UserNo, item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                PopulateGridDetl()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)

        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Private Function SaveRecord() As Integer
        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(txtPEReviewNo.Text)
        Dim employeeno As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim pecycleno As Integer = Generic.ToInt(Me.cboPECycleNo.SelectedValue)
        Dim datefrom As String = Generic.ToStr(Me.txtDateFrom.Text)
        Dim dateto As String = Generic.ToStr(txtDateTo.Text)

        If SQLHelper.ExecuteNonQuery("EPEReview_WebSave", UserNo, tno, Generic.ToInt(Session("pereviewmainno")), employeeno, datefrom, dateto, pecycleno) > 0 Then
            Retval = True
        Else
            Retval = False
        End If
        Return Retval
    End Function


    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEReviewNo", "Code"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        PopulateGridDetl()
    End Sub


#End Region

#Region "********Detail********"

    Private Sub PopulateGridDetl()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEReviewEvaluator_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEReviewEvaluatorNo"}))

                Generic.ClearControls(Me, "pnlPopupDetl")
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPEReviewEvaluator_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupDetl", dt)
                Next
                mdlDetl.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkDeleteDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PEReviewEvaluatorNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EPEReviewEvaluatorAppr", UserNo, "PEReviewEvaluatorNo", item)
                Generic.DeleteRecordAudit("EPEReviewEvaluator", UserNo, item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGridDetl()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If

    End Sub
    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub
    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            mdlDetl.Show()
        End If
    End Sub

    Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
        TransNo = Generic.ToInt(ViewState("TransNo"))
        If SaveRecordDetl() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecordDetl() As Boolean
        Dim Retval As Boolean = False
        Dim pereviewno As Integer = Generic.ToInt(ViewState("TransNo"))
        Dim tno As Integer = Generic.ToInt(Me.txtPEReviewEvaluatorNo.Text)
        Dim PEEvaluatorNo As Integer = Generic.ToInt(Me.cboPEEvaluatorNo.SelectedValue)
        Dim EmployeeLNo As Integer = Generic.ToInt(hifEvaluatorNo.Value)
        Dim Weighted As Double = Generic.ToDec(Me.txtWeighted.Text)

        If SQLHelper.ExecuteNonQuery("EPEReviewEvaluator_WebSave", UserNo, tno, pereviewno, pereviewmainno, PEEvaluatorNo, EmployeeLNo, Weighted) > 0 Then
            Retval = True
        Else
            Retval = False
        End If
        Return Retval
    End Function


    Protected Sub lnkForm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim peevaluatorno As Integer = 0
            Dim pereviewcateno As Integer = 0
            Dim pecatetypeno As Integer = 0
            Dim pereviewno As Integer = 0
            Dim pecycleno As Integer = 0
            Dim isposted As Boolean = False

            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEEvaluatorNo", "PEReviewNo", "PECycleNo", "IsPosted", "PEReviewMainNo"})
            peevaluatorno = obj(0)
            pereviewno = obj(1)
            pecycleno = obj(2)
            isposted = obj(3)

            Dim FormName As String = ""
            Dim ComponentNo As Integer = 1
            Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataTable("EMenu_PEReviewTab", UserNo, pereviewno, ComponentNo, False, peevaluatorno)
            For Each row As DataRow In dt.Select("RowNo=1")
                FormName = Generic.ToStr(row("Formname"))
                pereviewcateno = Generic.ToStr(row("PEReviewCateNo"))
                pecatetypeno = Generic.ToStr(row("PECateTypeNo"))
            Next

            If FormName > "" Then
                Response.Redirect(FormName & "?pereviewmainno=" & pereviewmainno & "&pereviewcateno=" & pereviewcateno & "&pecatetypeno=" & pecatetypeno & "&pereviewno=" & pereviewno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&componentno=" & ComponentNo & "&isposted=" & isposted)
            Else
                MessageBox.Warning("No template created.", Me)
            End If

        Catch ex As Exception
        End Try


    End Sub


#End Region

End Class