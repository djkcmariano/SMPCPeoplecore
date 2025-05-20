Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports DevExpress.XtraCharts

Partial Class Secured_AppMREdit_JobOffer
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim ActionStatNo As Integer = 3
    Dim rowno As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        hidTransNo.Value = TransNo
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateTabHeader()
            PopulateGrid()
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
        End If

        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

        PopulateDetl()
        PopulateGridBen()
        PopulateGroupBy()
        PopulateGridApp()

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub

#Region "Main"
    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EMRHiredMass_Web", UserNo, TransNo, Filter1.SearchText, ActionStatNo)
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If

            grdMain.SelectedIndex = 0
            grdMain.DataSource = dv
            grdMain.DataBind()

            If dt.Rows.Count > 0 Then
                ViewState("TransNo") = grdMain.DataKeys(0).Values(0).ToString()
                ViewState("TransCode") = grdMain.DataKeys(0).Values(1).ToString()
                ViewState("IsEnabled") = Generic.ToBol(grdMain.DataKeys(0).Values(2))
            End If

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
            ViewState("IsEnabled") = Generic.ToBol(grdMain.DataKeys(0).Values(2))
            PopulateDetl()
            PopulateGridBen()
            PopulateGroupBy()
            PopulateGridApp()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub grdMain_Sorting(sender As Object, e As GridViewSortEventArgs)
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

    Protected Sub grdMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            grdMain.PageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New ImageButton
            ib = sender
            Dim fmrhiredmassno As Integer = CType(ib.CommandArgument, Integer)
            'hifEmployeeNo.Value = fmrhiredmassno
            Dim dt As DataTable
            Dim IsEnabled As Boolean = True
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            dt = SQLHelper.ExecuteDataTable("EMRHiredMass_WebOne", UserNo, Generic.ToInt(fmrhiredmassno))
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
                'IsEnabled = Not Generic.ToBol(row("IsPosted"))
                cboHiringAlternativeNo.Text = Generic.ToInt(row("HiringAlternativeNo"))
                hidID.Value = Generic.ToInt(row("ID"))
            Next
            Generic.EnableControls(Me, "Panel1", IsEnabled)
            lnkSave.Enabled = IsEnabled
            cboHiringAlternativeNo.Enabled = False
            txtFullname.Enabled = False
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
        txtJOEncodeDate.Enabled = False
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For i As Integer = 0 To Me.grdMain.Rows.Count - 1
                chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
                ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
                If chk.Checked = True Then
                    SQLHelper.ExecuteNonQuery("EMRHiredMass_WebDelete", UserNo, Generic.ToInt(ib.CommandArgument), ActionStatNo)
                    Count = Count + 1
                End If
            Next
            MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
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

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            cboHiringAlternativeNo.SelectedValue = 1
            txtFullname.Text = ""
            hidID.Value = 0
            cboHiringAlternativeNo.Enabled = True
            txtFullname.Enabled = True
            Generic.EnableControls(Me, "Panel1", True)
            Generic.ClearControls(Me, "Panel1")
            txtPayOffer.Text = ""
            cboEmployeeRateClassNo.Text = ""
            txtJobOfferRemarks.Text = ""
            AutoCompleteExtender1.ContextKey = Generic.ToInt(cboHiringAlternativeNo.SelectedValue) & "|" & TransNo
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim HiringAlternativeNo As Integer = Generic.ToInt(cboHiringAlternativeNo.SelectedValue)
        Dim hidID As Integer = Generic.ToInt(Me.hidID.Value)
        Dim PayOffer As Decimal = Generic.ToDec(txtPayOffer.Text)
        Dim EmployeeRateClassNo As Integer = Generic.ToInt(cboEmployeeRateClassNo.SelectedValue)
        Dim JobOfferRemarks As String = Generic.ToStr(txtJobOfferRemarks.Text)
        Dim StartDate As String = Generic.ToStr(txtStartDate.Text)
        Dim EndDate As String = Generic.ToStr(txtEndDate.Text)
        Dim JOEndDate As String = Generic.ToStr(txtJOEndDate.Text)

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EMRHiredMass_WebValidate", UserNo, TransNo, hidID, HiringAlternativeNo, ActionStatNo)
        For Each row As DataRow In dt.Rows
            invalid = Generic.ToBol(row("Invalid"))
            messagedialog = Generic.ToStr(row("MessageDialog"))
            alerttype = Generic.ToStr(row("AlertType"))
        Next

        If invalid = True Then
            ModalPopupExtender1.Show()
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EMRHiredMass_WebSave_JobOffer", UserNo, TransNo, hidID, HiringAlternativeNo, PayOffer, EmployeeRateClassNo, JobOfferRemarks, StartDate, EndDate, JOEndDate) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub


    'Submit record
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lbl As New Label, cboJobOfferStatNo As New DropDownList
        Dim tcount As Integer, SaveCount As Integer = 0
        Dim xds As New DataSet, chk As New CheckBox

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then

            For tcount = 0 To Me.grdMain.Rows.Count - 1
                lbl = CType(grdMain.Rows(tcount).FindControl("lblNo"), Label)
                cboJobOfferStatNo = CType(grdMain.Rows(tcount).FindControl("cboJobOfferStatNo"), DropDownList)
                chk = CType(grdMain.Rows(tcount).FindControl("txtIsSelect"), CheckBox)

                Dim MRHiredMassNo As Integer = Generic.ToInt(lbl.Text)
                Dim StatusNo As Integer = Generic.ToInt(cboJobOfferStatNo.SelectedValue)

                If chk.Checked = True Then
                    If Not cboJobOfferStatNo Is Nothing Then
                        If SQLHelper.ExecuteNonQuery("EMRHiredMass_WebUpdate", UserNo, MRHiredMassNo, 0, StatusNo, ActionStatNo) > 0 Then
                            SaveCount = SaveCount + 1
                        End If
                    End If
                End If
            Next

            If SaveCount > 0 Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If


        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub cboHiringAlternativeNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        txtFullname.Text = ""
        hidID.Value = 0
        AutoCompleteExtender1.ContextKey = Generic.ToStr(cboHiringAlternativeNo.SelectedValue) & "|" & hidTransNo.Value.ToString
        ModalPopupExtender1.Show()
    End Sub
#End Region


#Region "Allowance"
    Private Sub PopulateDetl()

        lblName.Text = Generic.ToStr(ViewState("TransCode"))
        lnkAddDetl.Visible = Generic.ToBol(ViewState("IsEnabled"))
        lnkDeleteDetl.Visible = Generic.ToBol(ViewState("IsEnabled"))
        lnkAddBen.Visible = Generic.ToBol(ViewState("IsEnabled"))
        lnkDeleteBen.Visible = Generic.ToBol(ViewState("IsEnabled"))
        lnkAddApp.Visible = Generic.ToBol(ViewState("IsEnabled"))
        lnkDeleteApp.Visible = Generic.ToBol(ViewState("IsEnabled"))

        If Generic.ToInt(ViewState("TransNo")) = 0 Then
            lnkJobOffer.Visible = False
        Else
            lnkJobOffer.Visible = True
        End If

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMROffer_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdDetl.DataSource = dt
        grdDetl.DataBind()
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
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"MROfferNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EMROffer", UserNo, item)
                i = i + 1
            Next

            If i > 0 Then
                PopulateDetl()
                MessageBox.Success("(" + i.ToString + ")" + MessageTemplate.SuccessDelete, Me)
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
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MROfferNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EMROffer_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
                    Try
                        cboPayIncomeTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayIncomeType_WebLookup_UnionAllow", UserNo, Generic.ToInt(row("PayIncomeTypeNo")), PayLocNo)
                        cboPayIncomeTypeNo.DataValueField = "tNo"
                        cboPayIncomeTypeNo.DataTextField = "tDesc"
                        cboPayIncomeTypeNo.DataBind()
                    Catch ex As Exception
                    End Try
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
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
            Try
                cboPayIncomeTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayIncomeType_WebLookup_UnionAllow", UserNo, 0, PayLocNo)
                cboPayIncomeTypeNo.DataValueField = "tNo"
                cboPayIncomeTypeNo.DataTextField = "tDesc"
                cboPayIncomeTypeNo.DataBind()
            Catch ex As Exception
            End Try

            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub btnSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim MROfferNo As Integer = Generic.ToInt(txtMROfferNo.Text)
        Dim Amount As Decimal = Generic.ToDec(txtAmount.Text)
        Dim PayIncomeTypeNo As Integer = Generic.ToInt(cboPayIncomeTypeNo.SelectedValue)
        Dim IsPerDay As Boolean = Generic.ToBol(txtIsPerDay.Checked)
        Dim PayScheduleNo As Integer = Generic.ToInt(cboPayScheduleNo.SelectedValue)

        If SQLHelper.ExecuteNonQuery("EMROffer_WebSave", UserNo, MROfferNo, Generic.ToInt(ViewState("TransNo")), TransNo, Amount, PayIncomeTypeNo, IsPerDay, PayScheduleNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateDetl()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub



#End Region

#Region "Benefit Package"
    Private Sub PopulateGridBen()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMRBenefitPackage_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdBen.DataSource = dt
        grdBen.DataBind()
    End Sub

    Protected Sub lnkExportBen_Click(sender As Object, e As EventArgs)
        Try
            grdExportBen.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDeleteBen_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdBen.GetSelectedFieldValues(New String() {"MRBenefitPackageNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EMRBenefitPackage", UserNo, item)
                i = i + 1
            Next

            If i > 0 Then
                PopulateGridBen()
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEditBen_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MRBenefitPackageNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EMRBenefitPackage_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateDropDownList(UserNo, Me, "pnlPopupBen", PayLocNo)
                    Generic.PopulateData(Me, "pnlPopupBen", dt)
                Next
                mdlShowBen.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAddBen_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupBen")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupBen", PayLocNo)
            mdlShowBen.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub cboBenefitPackageTypeNo_TextChanged(sender As Object, e As EventArgs)
        PopulateBenPacType(Generic.ToInt(cboBenefitPackageTypeNo.SelectedValue))
        mdlShowBen.Show()
    End Sub

    Private Sub PopulateBenPacType(ByVal tBenPacNo As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBenefitPackageDeti_WebOne", UserNo, tBenPacNo)
            For Each row As DataRow In dt.Rows
                txtMRBenefitPackageDesc.Text = Generic.ToStr(row("BenefitPackageDetiDesc"))
                txtRemarks.Text = Generic.ToStr(row("Remarks"))
                txtOrderLevel.Text = Generic.ToStr(row("OrderLevel"))
            Next
            If tBenPacNo > 0 Then
                txtMRBenefitPackageDesc.Enabled = False
                txtRemarks.Enabled = False
                txtOrderLevel.Enabled = False
            Else
                txtMRBenefitPackageDesc.Enabled = True
                txtRemarks.Enabled = True
                txtOrderLevel.Enabled = True
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnSaveBen_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim MRBenefitPackageNo As Integer = Generic.ToInt(txtMRBenefitPackageNo.Text)
        Dim MRBenefitPackageCode As String = ""
        Dim MRBenefitPackageDesc As String = Generic.ToStr(txtMRBenefitPackageDesc.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)
        Dim BenefitPackageTypeNo As Integer = Generic.ToInt(cboBenefitPackageTypeNo.SelectedValue)
        Dim OrderLevel As Integer = Generic.ToInt(txtOrderLevel.Text)

        If SQLHelper.ExecuteNonQuery("EMRBenefitPackage_WebSave", UserNo, MRBenefitPackageNo, MRBenefitPackageCode, MRBenefitPackageDesc, Remarks, BenefitPackageTypeNo, OrderLevel, Generic.ToInt(ViewState("TransNo")), TransNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridBen()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Protected Sub grdBen_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        PopulateGroupBy()
    End Sub

    Private Sub PopulateGroupBy()
        grdBen.BeginUpdate()
        Try
            grdBen.ClearSort()
            grdBen.GroupBy(grdBen.Columns("BenefitPackageTypeDesc"))
        Finally
            grdBen.EndUpdate()
        End Try
        grdBen.ExpandAll()
    End Sub

    Protected Sub grdBen_CustomColumnDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewColumnDisplayTextEventArgs)
        If e.Column.FieldName = "OrderLevel" Then
            Dim groupLevel As Integer = grdBen.GetRowLevel(e.VisibleRowIndex)
            If groupLevel = e.Column.GroupIndex Then
                Dim city As String = grdBen.GetRowValues(e.VisibleRowIndex, "OrderLevel").ToString()
                Dim country As String = grdBen.GetRowValues(e.VisibleRowIndex, "BenefitPackageTypeDesc").ToString()
                e.DisplayText = city & " (" & country & ")"
            End If
        End If

    End Sub

    Protected Sub grdBen_CustomColumnSort(ByVal sender As Object, ByVal e As CustomColumnSortEventArgs)
        If e.Column IsNot Nothing And e.Column.FieldName = "BenefitPackageTypeDesc" Then
            Dim country1 As Object = e.GetRow1Value("OrderLevel")
            Dim country2 As Object = e.GetRow2Value("OrderLevel")
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


#Region "Allowance"
    Private Sub PopulateGridApp()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMRApprovalRouting_Web", UserNo, Generic.ToInt(ViewState("TransNo")), ActionStatNo)
        grdApp.DataSource = dt
        grdApp.DataBind()
    End Sub

    Protected Sub lnkExportApp_Click(sender As Object, e As EventArgs)
        Try
            grdExportApp.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDeleteApp_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdApp.GetSelectedFieldValues(New String() {"MRApprovalRoutingNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EMRApprovalRouting", UserNo, item)
                i = i + 1
            Next

            If i > 0 Then
                PopulateGridApp()
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEditApp_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MRApprovalRoutingNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EMRApprovalRouting_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateDropDownList(UserNo, Me, "pnlPopupApp", PayLocNo)
                    Generic.PopulateData(Me, "pnlPopupApp", dt)
                Next
                mdlShowApp.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAddApp_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupApp")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupApp", PayLocNo)
            mdlShowApp.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub btnSaveApp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim MRApprovalRoutingNo As Integer = Generic.ToInt(txtMRApprovalRoutingNo.Text)
        Dim MRNo As Integer = TransNo
        Dim ApproveNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim HRANApprovalTypeNo As Integer = Generic.ToInt(cboHRANApprovalTypeNo.SelectedValue)
        Dim ApproveDate As String = Generic.ToStr(txtApproveDate.Text)
        Dim Remark As String = Generic.ToStr(txtRemark.Text)
        Dim IsApproved As Boolean = Generic.ToBol(chkIsApproved.Checked)
        Dim OrderNo As Integer = Generic.ToInt(txtOrderNo.Text)
        Dim ImmediateName As String = Generic.ToStr(txtImmediateName.Text)

        If SQLHelper.ExecuteNonQuery("EMRApprovalRouting_WebSave", UserNo, MRApprovalRoutingNo, MRNo, ApproveNo, HRANApprovalTypeNo, ApproveDate, Remark, IsApproved, OrderNo, Generic.ToInt(ViewState("TransNo")), ActionStatNo, ImmediateName) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridApp()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub



#End Region




#Region "********Reports********"

    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender

        'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        'Dim obj As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TransNo"}))
        'ViewState("TransNo") = obj
        Dim obj As Integer = ViewState("TransNo")

        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, Generic.ToInt(ViewState("TransNo"))))

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=674&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Protected Sub lnkJobOffer_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim param As String = Generic.ReportParam(
                                                    New ReportParameter(ReportParameter.Type.int, Generic.ToInt(ViewState("TransNo"))), _
                                                    New ReportParameter(ReportParameter.Type.int, PayLocNo.ToString)
                                                  )

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=674&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkJobOffer_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub


#End Region



End Class
