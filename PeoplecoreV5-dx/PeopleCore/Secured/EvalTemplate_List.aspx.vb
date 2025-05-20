Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_EvalTemplate_List
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticatedCoreUser()

        If Not IsPostBack Then
            'grdItem.GroupBy(grdItem.Columns("Category"), 0)
            'grdRating.GroupBy(grdRating.Columns("Category"), 0)

            Try
                cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 14)
                cboTabNo.DataTextField = "tDesc"
                cboTabNo.DataValueField = "tno"
                cboTabNo.DataBind()
            Catch ex As Exception
            End Try

            'for applicant examination template only custom for bsp
            If Session("xMenuType") = "0128000000" Then
                divOpt.Visible = True
            End If

        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateTab(Optional Index As Integer = 0)

        If Index = 1 Then
            lnkItem.CssClass = "list-group-item active text-left"
            lnkCate.CssClass = "list-group-item text-left"
            lnkRating.CssClass = "list-group-item text-left"
            divDim.Visible = False
            divRating.Visible = False
            divItem.Visible = True
            PopulateItem()
            PopulateGroupBy()
        ElseIf Index = 2 Then
            lnkRating.CssClass = "list-group-item active text-left"
            lnkCate.CssClass = "list-group-item text-left"
            lnkItem.CssClass = "list-group-item text-left"
            divDim.Visible = False
            divRating.Visible = True
            divItem.Visible = False
            PopulateRating()
            PopulateGroupByRating()
        Else
            lnkCate.CssClass = "list-group-item active text-left"
            lnkItem.CssClass = "list-group-item text-left"
            lnkRating.CssClass = "list-group-item text-left"
            divDim.Visible = True
            divItem.Visible = False
            divRating.Visible = False
            PopulateCate()
        End If

    End Sub


    Protected Sub lnkCate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateTab()
    End Sub

    Protected Sub lnkItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateTab(1)
    End Sub

    Protected Sub lnkRating_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateTab(2)
    End Sub


#Region "********Main********"


    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EEvalTemplate_Web", UserNo, Session("xMenuType"), PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If ViewState("TransNo") = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"EvalTemplateNo", "Code"})
            ViewState("TransNo") = obj(0)
            lblMain.Text = obj(1)
        End If

        PopulateCate()
        PopulateRating()
        PopulateItem()
    End Sub

    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEvalTemplate_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", Generic.ToInt(Session("xPayLocNo")))
                PopulateAssessment(Generic.ToBol(row("IsPanelAssess")), Generic.ToBol(row("IsOnlineAssess")), Generic.ToBol(row("IsExam")))
                Generic.PopulateData(Me, "pnlPopupMain", dt)

            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)

        Generic.ClearControls(Me, "pnlPopupMain")
        chkIsNA.Checked = True
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
        mdlMain.Show()

    End Sub

    Private Sub PopulateAssessment(IsPanel As Boolean, IsOnline As Boolean, IsExam As Boolean)

        If IsPanel Then
            chkIsPanelAssess.Checked = True
            chkIsOnlineAssess.Checked = False
            chkIsExam.Checked = False
            chkIsNA.Checked = False
        ElseIf IsOnline Then
            chkIsOnlineAssess.Checked = True
            chkIsPanelAssess.Checked = False
            chkIsExam.Checked = False
            chkIsNA.Checked = False
        ElseIf IsExam Then
            chkIsExam.Checked = True
            chkIsOnlineAssess.Checked = False
            chkIsPanelAssess.Checked = False
            chkIsNA.Checked = False
        Else
            chkIsExam.Checked = False
            chkIsOnlineAssess.Checked = False
            chkIsPanelAssess.Checked = False
            chkIsNA.Checked = True
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim EvalTemplateNo As Integer = Generic.ToInt(txtEvalTemplateNo.Text)
        Dim EvalTemplateCode As String = Generic.ToStr(txtEvalTemplateCode.Text)
        Dim EvalTemplateDesc As String = Generic.ToStr(txtEvalTemplateDesc.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)

        If SQLHelper.ExecuteNonQuery("EEvalTemplate_WebSave", UserNo, EvalTemplateNo, EvalTemplateCode, EvalTemplateDesc, Generic.ToStr(Session("xMenuType")), Remarks, PayLocNo, Generic.ToInt(chkIsArchived.Checked), Generic.ToInt(chkIsPanelAssess.Checked), Generic.ToInt(chkIsOnlineAssess.Checked), Generic.ToInt(chkIsExam.Checked)) > 0 Then
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

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)

        Generic.ClearControls(Me, "pnlPopupMain")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
        PopulateData(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EvalTemplateNo"}))

        mdlMain.Show()

    End Sub

    Protected Sub lnkArchive_Click(sender As Object, e As EventArgs)

        Dim dt As DataTable, tProceed As Boolean = False
        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "EvalTemplateNo"))
                dt = SQLHelper.ExecuteDataTable("ETableReferrence_WebArchived", UserNo, "EEValTemplate", item, 1, PayLocNo)
                For Each row As DataRow In dt.Rows
                    tProceed = Generic.ToBol(row("tProceed"))
                Next
                grdMain.Selection.UnselectRow(j)
                i = i + 1
            End If
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") transaction(s) successfully archived.", Me)
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If


    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EvalTemplateNo"})
        Dim str As String = ""
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAuditCol("EEvalTemplateCate", UserNo, "EvalTemplateNo", item)
            Generic.DeleteRecordAuditCol("EEvalTemplateRating", UserNo, "EvalTemplateNo", item)
            Generic.DeleteRecordAuditCol("EEvalTemplateDetl", UserNo, "EvalTemplateNo", item)
            Generic.DeleteRecordAuditCol("EEvalTemplateDetlChoice", UserNo, "EvalTemplateNo", item)
            Generic.DeleteRecordAudit("EEvalTemplate", UserNo, CType(item, Integer))
            DeleteCount = DeleteCount + 1
        Next

        If DeleteCount > 0 Then
            PopulateGrid()
            MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkForm_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton, i As Integer
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        i = Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EvalTemplateNo"}))

        Response.Redirect("~/secured/EvalTemplate_Form.aspx?TemplateID=" & Generic.ToInt(i) & "&id=" & TransNo)

    End Sub

    Protected Sub lnkDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lnk As New LinkButton, i As Integer
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EvalTemplateNo"}))
        lblMain.Text = Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"Code"}))
        ViewState("TransNo") = i
        PopulateCate()
        PopulateRating()
        PopulateItem()
        PopulateGroupBy()
        PopulateGroupByRating()
    End Sub


#End Region


#Region "********Category********"


    Private Sub PopulateCate()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EEvalTemplateCate_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdCate.DataSource = _dt
        grdCate.DataBind()
    End Sub

    Protected Sub lnkAddCate_Click(sender As Object, e As EventArgs)

        If Generic.ToInt(ViewState("TransNo")) > 0 Then
            Generic.ClearControls(Me, "pnlPopupCate")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupCate", PayLocNo)
            mdlCate.Show()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

    Protected Sub lnkDeleteCate_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdCate.GetSelectedFieldValues(New String() {"EvalTemplateCateNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EEvalTemplateCate", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateCate()

    End Sub

    Protected Sub lnkEditCate_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Generic.ClearControls(Me, "pnlPopupCate")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupCate", PayLocNo)
        PopulateDataCate(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EvalTemplateCateNo"})))
        mdlCate.Show()

    End Sub

    Private Sub PopulateDataCate(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEvalTemplateCate_WebOne", UserNo, id)
            Generic.PopulateData(Me, "pnlPopupCate", dt)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSaveCate_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False

        If SQLHelper.ExecuteNonQuery("EEvalTemplateCate_WebSave", UserNo, Generic.ToInt(txtEvalTemplateCateNo.Text), Generic.ToInt(ViewState("TransNo")), txtEvalTemplateCateCode.Text, txtEvalTemplateCateDesc.Text, Generic.ToInt(txtOrderByCate.Text), 0, Generic.ToDec(txtWeightCate.Text)) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            PopulateCate()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub lnkExportCate_Click(sender As Object, e As EventArgs)
        Try
            grdExportCate.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

#End Region


#Region "********Rating********"


    Private Sub PopulateRating()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EEvalTemplateRating_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdRating.DataSource = _dt
        grdRating.DataBind()
    End Sub

    Protected Sub lnkAddRating_Click(sender As Object, e As EventArgs)

        If Generic.ToInt(ViewState("TransNo")) > 0 Then
            Generic.ClearControls(Me, "pnlPopupRating")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupRating", PayLocNo)
            Try
                cboEvalTemplateCateRatingNo.DataSource = SQLHelper.ExecuteDataTable("EEvalTemplateCate_WebLookup", Generic.ToInt(ViewState("TransNo")))
                cboEvalTemplateCateRatingNo.DataValueField = "tNo"
                cboEvalTemplateCateRatingNo.DataTextField = "tDesc"
                cboEvalTemplateCateRatingNo.DataBind()
            Catch ex As Exception
            End Try
            mdlRating.Show()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

    Protected Sub lnkDeleteRating_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdRating.GetSelectedFieldValues(New String() {"EvalTemplateRatingNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EEvalTemplateRating", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateRating()

    End Sub

    Protected Sub lnkEditRating_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Generic.ClearControls(Me, "pnlPopupRating")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupRating", PayLocNo)
        Try
            cboEvalTemplateCateRatingNo.DataSource = SQLHelper.ExecuteDataTable("EEvalTemplateCate_WebLookup", Generic.ToInt(ViewState("TransNo")))
            cboEvalTemplateCateRatingNo.DataValueField = "tNo"
            cboEvalTemplateCateRatingNo.DataTextField = "tDesc"
            cboEvalTemplateCateRatingNo.DataBind()
        Catch ex As Exception
        End Try
        PopulateDataRating(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EvalTemplateRatingNo"})))
        mdlRating.Show()

    End Sub

    Private Sub PopulateDataRating(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEvalTemplateRating_WebOne", UserNo, id)
            Generic.PopulateData(Me, "pnlPopupRating", dt)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSaveRating_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False

        If SQLHelper.ExecuteNonQuery("EEvalTemplateRating_WebSave", UserNo, Generic.ToInt(txtEvalTemplateRatingNo.Text), Generic.ToInt(ViewState("TransNo")), txtEvalTemplateRatingDesc.Text, Generic.ToDec(txtProficiency.Text), Generic.ToInt(txtOrderByRating.Text), Generic.ToInt(cboEvalTemplateCateRatingNo.SelectedValue)) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            PopulateRating()
            PopulateGroupByRating()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub lnkExportRating_Click(sender As Object, e As EventArgs)
        Try
            grdExportRating.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub grdRating_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        PopulateGroupByRating()
    End Sub

    Private Sub PopulateGroupByRating()
        grdRating.BeginUpdate()
        Try
            grdRating.ClearSort()
            grdRating.GroupBy(grdRating.Columns("EvalTemplateCateDesc"))
        Finally
            grdRating.EndUpdate()
        End Try
        grdRating.ExpandAll()
    End Sub

    Protected Sub grdRating_CustomColumnDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewColumnDisplayTextEventArgs)
        If e.Column.FieldName = "OrderByCate" Then
            Dim groupLevel As Integer = grdRating.GetRowLevel(e.VisibleRowIndex)
            If groupLevel = e.Column.GroupIndex Then
                Dim city As String = grdRating.GetRowValues(e.VisibleRowIndex, "OrderByCate").ToString()
                Dim country As String = grdRating.GetRowValues(e.VisibleRowIndex, "EvalTemplateCateDesc").ToString()
                e.DisplayText = city & " (" & country & ")"
            End If
        End If

    End Sub

    Protected Sub grdRating_CustomColumnSort(ByVal sender As Object, ByVal e As CustomColumnSortEventArgs)
        If e.Column IsNot Nothing And e.Column.FieldName = "EvalTemplateCateDesc" Then
            Dim country1 As Object = e.GetRow1Value("OrderByCate")
            Dim country2 As Object = e.GetRow2Value("OrderByCate")
            Dim res As Integer = Comparer.Default.Compare(country1, country2)
            If res = 0 Then
                Dim city1 As Object = e.Value1
                Dim city2 As Object = e.Value2
                res = Comparer.Default.Compare(city1, city2)
            End If
            e.Result = res
            e.Handled = True
        End If
    End Sub

#End Region


#Region "********Questionnaire********"

    Private Sub PopulateItem(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EEvalTemplateDetl_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        Me.grdItem.DataSource = _dt
        Me.grdItem.DataBind()

        If Generic.ToInt(ViewState("DetailNo")) = 0 Or IsMain = True Then
            Dim obj As Object() = grdItem.GetRowValues(grdItem.VisibleStartIndex(), New String() {"EvalTemplateDetlNo", "Code"})
            ViewState("DetailNo") = obj(0)
            lblDetl.Text = obj(1)
        End If

        PopulateGridDetl()
    End Sub


    Protected Sub lnkAddItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If Generic.ToInt(ViewState("TransNo")) > 0 Then
            Generic.ClearControls(Me, "pnlPopupItem")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupItem", PayLocNo)
            Try
                cboEvalTemplateCateNo.DataSource = SQLHelper.ExecuteDataTable("EEvalTemplateCate_WebLookup", Generic.ToInt(ViewState("TransNo")))
                cboEvalTemplateCateNo.DataValueField = "tNo"
                cboEvalTemplateCateNo.DataTextField = "tDesc"
                cboEvalTemplateCateNo.DataBind()
            Catch ex As Exception
            End Try
            mdlItem.Show()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If


    End Sub


    Protected Sub lnkEditItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EvalTemplateDetlNo"}))
            Generic.ClearControls(Me, "pnlPopupItem")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupItem", PayLocNo)
            Try
                cboEvalTemplateCateNo.DataSource = SQLHelper.ExecuteDataTable("EEvalTemplateCate_WebLookup", Generic.ToInt(ViewState("TransNo")))
                cboEvalTemplateCateNo.DataValueField = "tNo"
                cboEvalTemplateCateNo.DataTextField = "tDesc"
                cboEvalTemplateCateNo.DataBind()
            Catch ex As Exception
            End Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEvalTemplateDetl_WebOne", UserNo, Generic.ToInt(i))
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupItem", dt)
            Next
            mdlItem.Show()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDeleteItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0

        Dim fieldValues As List(Of Object) = grdItem.GetSelectedFieldValues(New String() {"EvalTemplateDetlNo"})
        Dim str As String = ""
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EEvalTemplateDetl", UserNo, item)
            Generic.DeleteRecordAuditCol("EEvalTemplateDetlChoice", UserNo, "EvalTemplateDetlNo", item)
            DeleteCount = DeleteCount + 1
        Next

        If DeleteCount > 0 Then
            PopulateItem(True)
            MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub


    Protected Sub lnkDetailItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EvalTemplateDetlNo", "Code"})
        ViewState("DetailNo") = obj(0)
        lblDetl.Text = obj(1)
        PopulateGridDetl()
    End Sub

    Protected Sub lnkSaveItem_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False

        If SQLHelper.ExecuteNonQuery("EEvalTemplateDetl_WebSave", UserNo, Generic.ToInt(txtEvalTemplateDetlNo.Text), Generic.ToInt(ViewState("TransNo")), Generic.ToInt(cboEvalTemplateCateNo.SelectedValue), txtQuestion.Text, txtHasComment.Checked, txtIsRequired.Checked, Generic.ToInt(cboResponseTypeNo.SelectedValue), Generic.ToInt(txtOrderBy.Text), Generic.ToDec(txtWeightageItem.Text), Generic.ToStr(txtOrderCode.Text), PayLocNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            PopulateItem()
            PopulateGroupBy()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub lnkExportItem_Click(sender As Object, e As EventArgs)
        Try
            grdExportItem.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub grdItem_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        PopulateGroupBy()
    End Sub

    Private Sub PopulateGroupBy()
        grdItem.BeginUpdate()
        Try
            grdItem.ClearSort()
            grdItem.GroupBy(grdItem.Columns("EvalTemplateCateDesc"))
        Finally
            grdItem.EndUpdate()
        End Try
        grdItem.ExpandAll()
    End Sub

    Protected Sub grdItem_CustomColumnDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewColumnDisplayTextEventArgs)
        If e.Column.FieldName = "OrderByCate" Then
            Dim groupLevel As Integer = grdItem.GetRowLevel(e.VisibleRowIndex)
            If groupLevel = e.Column.GroupIndex Then
                Dim city As String = grdItem.GetRowValues(e.VisibleRowIndex, "OrderByCate").ToString()
                Dim country As String = grdItem.GetRowValues(e.VisibleRowIndex, "EvalTemplateCateDesc").ToString()
                e.DisplayText = city & " (" & country & ")"
            End If
        End If

    End Sub

    Protected Sub grdItem_CustomColumnSort(ByVal sender As Object, ByVal e As CustomColumnSortEventArgs)
        If e.Column IsNot Nothing And e.Column.FieldName = "EvalTemplateCateDesc" Then
            Dim country1 As Object = e.GetRow1Value("OrderByCate")
            Dim country2 As Object = e.GetRow2Value("OrderByCate")
            Dim res As Integer = Comparer.Default.Compare(country1, country2)
            If res = 0 Then
                Dim city1 As Object = e.Value1
                Dim city2 As Object = e.Value2
                res = Comparer.Default.Compare(city1, city2)
            End If
            e.Result = res
            e.Handled = True
        End If
    End Sub

#End Region


#Region "********Detail********"

    Private Sub PopulateGridDetl()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEvalTemplateDetlChoice_Web", UserNo, Generic.ToInt(ViewState("DetailNo")))
        grdDetail.DataSource = dt
        grdDetail.DataBind()
    End Sub


    Protected Sub lnkAddDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If Generic.ToInt(ViewState("DetailNo")) > 0 Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub


    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EvalTemplateDetlChoiceNo"}))
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEvalTemplateDetlChoice_WebOne", UserNo, Generic.ToInt(i))
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
            mdlDetl.Show()

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkDeleteDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0

        Dim fieldValues As List(Of Object) = grdDetail.GetSelectedFieldValues(New String() {"EvalTemplateDetlChoiceNo"})
        Dim str As String = ""
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EEvalTemplateDetlChoice", UserNo, item)
            DeleteCount = DeleteCount + 1
        Next

        If DeleteCount > 0 Then
            PopulateGridDetl()
            MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub


    Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
        If SaveRecordDetl() Then
            PopulateGridDetl()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecordDetl() As Boolean

        If SQLHelper.ExecuteNonQuery("EEvalTemplateDetlChoice_WebSave", UserNo, Generic.ToInt(txtCodeDetl.Text), txtEvalTemplateDetlChoiceDesc.Text, Generic.ToInt(ViewState("DetailNo")), Generic.ToInt(ViewState("TransNo")), Generic.ToInt(txtOrderByDetl.Text), Generic.ToDec(txtProficiency.Text), PayLocNo) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub PopulateDataDetl(ID As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEvalTemplateDetlChoice_WebOne", UserNo, ID)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel2", dt)
        Next
    End Sub

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

#End Region





End Class
