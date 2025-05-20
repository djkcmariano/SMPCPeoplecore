Imports clsLib
Imports System.Data
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web

Partial Class Secured_TrnPositionList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateCombo()
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Private Sub PopulateTab(Optional Index As Integer = 0)

        If Index = 1 Then
            lnkEmp.CssClass = "list-group-item active text-left"
            lnkTrn.CssClass = "list-group-item text-left"
            divTrn.Visible = False
            divEmp.Visible = True
            divTrnBtn.Visible = False
            divEmpBtn.Visible = True
            PopulateEmp()
        Else
            lnkTrn.CssClass = "list-group-item active text-left"
            lnkEmp.CssClass = "list-group-item text-left"
            divTrn.Visible = True
            divEmp.Visible = False
            divTrnBtn.Visible = True
            divEmpBtn.Visible = False
            PopulateTrn()
        End If

    End Sub

    Protected Sub lnkTrn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateTab()
    End Sub

    Protected Sub lnkEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateTab(1)
    End Sub


#Region "********Main********"


    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("ETrnPosition_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If ViewState("TransNo") = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"TrnPositionNo", "TrnPositionCode"})
            ViewState("TransNo") = obj(0)
            lblMain.Text = obj(1)
        End If

        PopulateTrn()
        PopulateEmp()
    End Sub

    Private Sub PopulateCombo()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 14)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboTab_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            PopulateGrid(True)

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New LinkButton
            ib = sender

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnPosition_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
            For Each row As DataRow In dt.Rows
                Generic.ClearControls(Me, "pnlPopupMain")
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
    
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        TransNo = Generic.ToInt(ViewState("TransNo"))

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TrnPositionNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ETrnPosition", UserNo, item)
                Generic.DeleteRecordAuditCol("ETrnPositionDetl", UserNo, "TrnPositionNo", item)
                Generic.DeleteRecordAuditCol("ETrnPositionEmp", UserNo, "TrnPositionNo", item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                PopulateEmp()
                PopulateTrn()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)

        End If
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim TrnPositionNo As Integer = Generic.ToInt(txtTrnPositionCode.Text)
        Dim PositionNo As Integer = Generic.ToInt(cboPositionNo.SelectedValue)
        Dim DepartmentNo As Integer = Generic.ToInt(cboDepartmentNo.SelectedValue)
        Dim SeriesYear As Integer = Generic.ToInt(txtSeriesYear.Text)
        Dim IsArchived As Boolean = Generic.ToBol(chkIsArchived.Checked)

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("ETrnPosition_WebValidate", UserNo, TrnPositionNo, PositionNo, DepartmentNo, SeriesYear, IsArchived, PayLocNo)
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

        If SQLHelper.ExecuteNonQuery("ETrnPosition_WebSave", UserNo, TrnPositionNo, PositionNo, DepartmentNo, SeriesYear, IsArchived, PayLocNo) > 0 Then
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

    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"TrnPositionNo", "TrnPositionCode"})
        ViewState("TransNo") = obj(0)
        lblMain.Text = obj(1)
        PopulateTrn()
        PopulateEmp()
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

#End Region


#Region "********Copy Training********"


    Protected Sub lnkSaveCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If SaveRecordCopy() Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Private Function SaveRecordCopy() As Boolean
        Dim TrnPositionToNo As Integer = ViewState("TrnPositionToNo") 'Generic.ToInt(cboTrnPositionToNo.SelectedValue)
        Dim TrnPositionFromNo As Integer = Generic.ToInt(cboTrnPositionFromNo.SelectedValue)

        If SQLHelper.ExecuteNonQuery("ETrnPosition_WebSaveCopy", UserNo, TrnPositionToNo, TrnPositionFromNo) > 0 Then
            SaveRecordCopy = True
        Else
            SaveRecordCopy = False

        End If
    End Function

    Protected Sub lnkCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"TrnPositionNo", "TrnPositionCode"})
            ViewState("TrnPositionToNo") = Generic.ToInt(obj(0))
            'Try
            '    cboTrnPositionToNo.DataSource = SQLHelper.ExecuteDataSet("ETrnPosition_WebLookupTo", UserNo, Generic.ToInt(obj(0)))
            '    cboTrnPositionToNo.DataValueField = "tNo"
            '    cboTrnPositionToNo.DataTextField = "tDesc"
            '    cboTrnPositionToNo.DataBind()
            'Catch ex As Exception
            'End Try

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnPosition_WebOneCopy", UserNo, Generic.ToInt(obj(0)))
            For Each row As DataRow In dt.Rows
                Try
                    cboTrnPositionFromNo.DataSource = SQLHelper.ExecuteDataSet("ETrnPosition_WebLookupFrom", UserNo, Generic.ToInt(obj(0)))
                    cboTrnPositionFromNo.DataValueField = "tNo"
                    cboTrnPositionFromNo.DataTextField = "tDesc"
                    cboTrnPositionFromNo.DataBind()
                Catch ex As Exception
                End Try

                Generic.PopulateData(Me, "pnlPopupCopy", dt)
            Next
            mdlCopy.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

#End Region


#Region "********Required Training********"

    Protected Sub PopulateTrn()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("ETrnPositionDetl_Web", UserNo, Generic.ToInt(ViewState("TransNo")), "")
            Dim dv As DataView = dt.DefaultView

            grdTrn.DataSource = dv
            grdTrn.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAddR_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnltrn")
            Generic.PopulateDropDownList(UserNo, Me, "pnltrn", PayLocNo)
            mdtrn.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkEditR_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New LinkButton
            ib = sender

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnPositionDetl_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
            For Each row As DataRow In dt.Rows
                Generic.ClearControls(Me, "pnltrn")
                Generic.PopulateData(Me, "pnltrn", dt)
                Generic.PopulateDropDownList(UserNo, Me, "pnltrn", PayLocNo)
            Next
            mdtrn.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkDeleteR_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        TransNo = Generic.ToInt(ViewState("TransNo"))

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdTrn.GetSelectedFieldValues(New String() {"TrnPositionDetlNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ETrnPositionDetl", UserNo, item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateTrn()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)

        End If
    End Sub

    Protected Sub btnSaveTrn_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim chk As New CheckBox, lbl As New Label, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            TransNo = Generic.ToInt(ViewState("TransNo"))
            Dim trnPositionDetlNo As Integer = Generic.ToInt(txtTrnPositionDetlNo.Text)

            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim _dt As New DataTable
            _dt = SQLHelper.ExecuteDataTable("ETrnPositionDetl_WebValidate", UserNo, trnPositionDetlNo, TransNo, Generic.ToInt(cboTrnTitleNo.SelectedValue), True, PayLocNo, Generic.ToDec(txtHrs.Text))
            For Each row As DataRow In _dt.Rows
                invalid = Generic.ToBol(row("Invalid"))
                messagedialog = Generic.ToStr(row("MessageDialog"))
                alerttype = Generic.ToStr(row("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                mdtrn.Show()
                Exit Sub
            End If

            Dim dt As DataTable, retVal As Boolean = False, error_num As Integer = 0, error_message As String = ""
            dt = SQLHelper.ExecuteDataTable("ETrnPositionDetl_WebSave", UserNo, trnPositionDetlNo, TransNo, Generic.ToInt(cboTrnTitleNo.SelectedValue), True, PayLocNo, Generic.ToDec(txtHrs.text))
            For Each row As DataRow In dt.Rows
                RetVal = True
                error_num = Generic.ToInt(row("Error_num"))
                If error_num > 0 Then
                    error_message = Generic.ToStr(row("ErrorMessage"))
                    MessageBox.Critical(error_message, Me)
                    RetVal = False
                End If

            Next
            If RetVal = False And error_message = "" Then
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
            If RetVal = True Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    
    Protected Sub lnkExportTrn_Click(sender As Object, e As EventArgs)
        Try
            grdExportTrn.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

#End Region


#Region "********Exclude Employee********"

    Protected Sub PopulateEmp()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""


            dt = SQLHelper.ExecuteDataTable("ETrnPositionEmp_Web", UserNo, Generic.ToInt(ViewState("TransNo")), "")
            Dim dv As DataView = dt.DefaultView
            grdEmp.DataSource = dv
            grdEmp.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAddE_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlemp")
            Generic.PopulateDropDownList(UserNo, Me, "pnlemp", PayLocNo)
            mdEmp.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    
    Protected Sub lnkEditE_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New LinkButton
            ib = sender

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnPositionEmp_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
            For Each row As DataRow In dt.Rows
                Generic.ClearControls(Me, "pnlemp")
                Generic.PopulateData(Me, "pnlemp", dt)
                Generic.PopulateDropDownList(UserNo, Me, "pnlemp", PayLocNo)
            Next
            mdEmp.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub


    Protected Sub lnkDeleteE_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        TransNo = Generic.ToInt(ViewState("TransNo"))

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdEmp.GetSelectedFieldValues(New String() {"TrnPositionEmpNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ETrnPositionEmp", UserNo, item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateTrn()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)

        End If
    End Sub


    Protected Sub btnSaveEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim chk As New CheckBox, lbl As New Label, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            TransNo = Generic.ToInt(ViewState("TransNo"))

            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim _dt As New DataTable
            _dt = SQLHelper.ExecuteDataTable("ETrnPositionEmp_WebValidate", UserNo, TransNo, Generic.ToInt(hifEmployeeNo.Value), Generic.ToBol(chk.Checked), PayLocNo)
            For Each row As DataRow In _dt.Rows
                invalid = Generic.ToBol(row("Invalid"))
                messagedialog = Generic.ToStr(row("MessageDialog"))
                alerttype = Generic.ToStr(row("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                mdEmp.Show()
                Exit Sub
            End If

            Dim dt As DataTable, retVal As Boolean = False, error_num As Integer = 0, error_message As String = ""
            dt = SQLHelper.ExecuteDataTable("ETrnPositionEmp_WebSave", UserNo, TransNo, Generic.ToInt(hifEmployeeNo.Value), Generic.ToBol(chk.Checked), PayLocNo)
            For Each row As DataRow In dt.Rows
                retVal = True
                error_num = Generic.ToInt(row("Error_num"))
                If error_num > 0 Then
                    error_message = Generic.ToStr(row("ErrorMessage"))
                    MessageBox.Critical(error_message, Me)
                    retVal = False
                End If

            Next
            If retVal = False And error_message = "" Then
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
            If retVal = True Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkExportE_Click(sender As Object, e As EventArgs)
        Try
            grdExportE.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    'Protected Sub btnSaveEmp_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim chk As New CheckBox, lbl As New Label, Count As Integer = 0
    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '        TransNo = Generic.ToInt(ViewState("TransNo"))
    '        For i As Integer = 0 To Me.grdEmp.Rows.Count - 1
    '            chk = CType(grdEmp.Rows(i).FindControl("txtIsAdded"), CheckBox)
    '            lbl = CType(grdEmp.Rows(i).FindControl("lblid"), Label)

    '            If SQLHelper.ExecuteNonQuery("ETrnPositionEmp_WebSave", UserNo, TransNo, Generic.ToInt(lbl.Text), Generic.ToBol(chk.Checked), PayLocNo) > 0 Then
    '                Count = Count + 1
    '            End If

    '        Next

    '        If Count > 0 Then
    '            PopulateEmp()
    '            MessageBox.Success("(" + Count.ToString + ") added as required training for the position", Me)
    '        Else
    '            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
    '        End If

    '    Else
    '        MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
    '    End If

    'End Sub

#End Region


End Class

