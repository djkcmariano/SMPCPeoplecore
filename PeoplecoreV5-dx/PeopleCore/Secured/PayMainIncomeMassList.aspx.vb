Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PayMainIncomeMassList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()

        Dim tstatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
        If tstatus = 0 Then
            lnkActivate.Visible = False
            lnkSuspend.Visible = True
        Else
            lnkActivate.Visible = True
            lnkSuspend.Visible = False
        End If

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayMainIncomeMass_Web", UserNo, tstatus, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateGridDetl(id As Integer)
        Dim tstatus As Integer = Generic.ToInt(cboTabDetlNo.SelectedValue)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayMainIncomeMassDeti_Web", UserNo, id, tstatus)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 3)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception

        End Try


        Try
            cboTabDetlNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 5)
            cboTabDetlNo.DataValueField = "tNo"
            cboTabDetlNo.DataTextField = "tDesc"
            cboTabDetlNo.DataBind()

        Catch ex As Exception

        End Try
        Try
            cboPayIncomeTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayIncomeType_WebLookup", UserNo, PayLocNo)
            cboPayIncomeTypeNo.DataTextField = "tDesc"
            cboPayIncomeTypeNo.DataValueField = "tNo"
            cboPayIncomeTypeNo.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupAppend", PayLocNo)
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

#Region "********Main*******"

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayMainIncomeMassNo", "Code"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = "Transaction No. : " & obj(1)
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))

    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayMainIncomeMassNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EPayMainIncomeMassDeti", UserNo, "PayMainIncomeMassNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EPayMainIncomeMass", UserNo, CType(item, Integer))
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

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer, IsEnabled As Boolean = False
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayMainIncomeMassNo", "IsEnabled"})
                i = Generic.ToInt(obj(0))
                IsEnabled = Generic.ToBol(obj(1))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPayMainIncomeMass_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                Next
                Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)

                mdlMain.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            Generic.EnableControls(Me, "pnlPopupMain", True)
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    'Submit record
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtPayMainIncomeMassNo.Text)
            Dim PayIncomeTypeNo As Integer = Generic.ToInt(Me.cboPayIncomeTypeNo.SelectedValue)
            Dim Description As String = Generic.ToStr(Me.txtDescription.Text)
            Dim Amount As Double = Generic.ToDec(Me.txtAmount.Text)
            Dim PayTypeNo As Integer = Generic.ToInt(Me.cboPayTypeNo.SelectedValue)
            Dim PayScheduleNo As Integer = Generic.ToInt(Me.cboPayScheduleNo.SelectedValue)
            Dim PayClassNo As Integer = Generic.ToInt(Me.cboPayclassNo.SelectedValue)
            Dim IsApplytoall As Boolean = Generic.ToBol(Me.txtIsApplytoall.Checked)
            
            If SQLHelper.ExecuteNonQuery("EPayMainIncomeMass_WebSave", UserNo, tno, PayScheduleNo, PayTypeNo, PayIncomeTypeNo, Description, Amount, IsApplytoall, PayclassNo, PayLocNo) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                PopulateGrid()
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkSuspend_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        Dim lnk As New LinkButton
        lnk = sender
        Dim action As Integer = CType(lnk.CommandName.ToString, Integer)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then

            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayMainIncomeMassNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("EPayMainIncomeMass_WebSuspend", UserNo, CType(item, Integer), action)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessUpdate, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
        End If
    End Sub

    Protected Sub lnkSuspend_Indi_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        Dim lnk As New LinkButton
        lnk = sender
        Dim action As Integer = CType(lnk.CommandName.ToString, Integer)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then

            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PayMainIncomeMassDetiNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("EPayMainIncomeMassDeti_WebSuspend", UserNo, CType(item, Integer), action)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessUpdate, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
        End If
    End Sub

#End Region


#Region "********Detail********"

    Protected Sub lnkSearchDetl_Click(sender As Object, e As EventArgs)
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
    End Sub

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub


    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PayMainIncomeMassDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayMainIncomeMassDeti", UserNo, CType(item, Integer))
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))

            If i > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayMainIncomeMassDetiNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPayMainIncomeMassDeti_WebOne", UserNo, Generic.ToInt(i))
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

    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            If Generic.ToInt(ViewState("TransNo")) > 0 Then
                Generic.ClearControls(Me, "pnlPopupDetl")
                mdlDetl.Show()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(ViewState("TransNo"))
            Dim PayMainIncomeMassDetiNo As Integer = Generic.ToInt(Me.txtPayMainIncomeMassDetiNo.Text)
            Dim EmployeeNo As Integer = Generic.ToInt(Me.hifEmployeeNo.Value)
            Dim Amount As Double = Generic.ToDec(Me.txtAmountD.Text)
            Dim StartDate As String = Generic.ToStr(Me.txtStartDate.Text)
            Dim EndDate As String = Generic.ToStr(Me.txtEndDate.Text)


            Dim dt As DataTable
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            dt = SQLHelper.ExecuteDataTable("EPayMainIncomeMassDeti_WebValidate", UserNo, PayMainIncomeMassDetiNo, tno, EmployeeNo, Amount, StartDate, EndDate)
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

            If SQLHelper.ExecuteNonQuery("EPayMainIncomeMassDeti_WebSave", UserNo, PayMainIncomeMassDetiNo, tno, EmployeeNo, Amount, StartDate, EndDate) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

#End Region


#Region "********Append*******"

    Protected Sub lnkAppend_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            If Generic.ToInt(ViewState("TransNo")) > 0 Then
                Generic.ClearControls(Me, "pnlPopupAppend")
                mdlAppend.Show()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub cboFilteredbyNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            populateDataDropdownF(Generic.CheckDBNull(cboFilteredbyNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType))
            mdlAppend.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub populateDataDropdownF(ByVal fid As Integer)
        Dim clsGen As New clsGenericClass
        Dim ds As DataSet
        Try
            ds = clsGen.populateDropdownFilterByCate(fid, UserNo, PayLocNo)
            cboFilterValue.DataSource = ds
            cboFilterValue.DataTextField = "tDesc"
            cboFilterValue.DataValueField = "tNo"
            cboFilterValue.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSaveAppend_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(ViewState("TransNo"))
            Dim FilteredbyNo As Integer = Generic.ToInt(Me.cboFilteredbyNo.SelectedValue)
            Dim FilterValue As Integer = Generic.ToInt(Me.cboFilterValue.SelectedValue)
            Dim Amount As Double = Generic.ToDec(Me.txtAmountAppend.Text)
            Dim StartDate As String = Generic.ToStr(Me.txtStartDateAppend.Text)
            Dim EndDate As String = Generic.ToStr(Me.txtEndDateAppend.Text)

            Dim dt As DataTable
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            dt = SQLHelper.ExecuteDataTable("EPayMainIncomeMassDeti_WebAppend_Validate", UserNo, tno, FilteredbyNo, FilterValue, Amount, StartDate, EndDate)
            For Each row As DataRow In dt.Rows
                invalid = Generic.ToBol(row("Invalid"))
                messagedialog = Generic.ToStr(row("MessageDialog"))
                alerttype = Generic.ToStr(row("AlertType"))
            Next

            If invalid = True Then
                mdlAppend.Show()
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If

            If SQLHelper.ExecuteNonQuery("EPayMainIncomeMassDeti_WebAppend", UserNo, tno, FilteredbyNo, FilterValue, Amount, StartDate, EndDate, PayLocNo) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

#End Region


End Class

