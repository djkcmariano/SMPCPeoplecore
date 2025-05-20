Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_BENBenefitBalanceList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim PaidHrs As Double = 0
    Dim CancelHrs As Double = 0
    Dim CancelCount As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)


        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EBenefitBalance_Web", UserNo, Generic.ToInt(txtYear.Text), Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If ViewState("TransNo") = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"EmployeeNo", "FullName", "BenefitTypeNo"})
            ViewState("TransNo") = obj(0)
            ViewState("Name") = obj(1)
            ViewState("BenefitTypeNo") = obj(2)
        End If

        lblDetl.Text = ViewState("Name")

        PopulateCutOff(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("BenefitTypeNo")), Generic.ToInt(txtYear.Text))
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("BenefitTypeNo")), Generic.ToInt(txtYear.Text))
        PopulateGridDetl1(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("BenefitTypeNo")), Generic.ToInt(txtYear.Text))

    End Sub

    Private Sub PopulateGridDetl(id As Integer, type As Integer, year As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EBenefitCredit_Web", UserNo, id, type, year)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Private Sub PopulateGridDetl1(id As Integer, type As Integer, year As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EBenefitAvailed_Web", UserNo, id, type, year)
        grdDetl1.DataSource = dt
        grdDetl1.DataBind()
    End Sub

    Private Sub PopulateCutOff(id As Integer, type As Integer, xdate As String)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EBenefitCutOff_Web", UserNo, id, type, xdate)
        For Each row As DataRow In dt.Rows
            lblDate.Text = Generic.ToStr(row("CoverDate"))
        Next
    End Sub

    Private Sub populateCombo()

        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 6)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()
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
            populateCombo()
            txtYear.Text = Now.Year
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)
        Generic.PopulateDXGridFilter(grdDetl1, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        PopulateGrid(True)

    End Sub

    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo", "FullName", "BenefitTypeNo"})
        ViewState("TransNo") = obj(0)
        ViewState("Name") = obj(1)
        ViewState("BenefitTypeNo") = obj(2)

        lblDetl.Text = ViewState("Name")

        PopulateCutOff(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("BenefitTypeNo")), Generic.ToInt(txtYear.Text))
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("BenefitTypeNo")), Generic.ToInt(txtYear.Text))
        PopulateGridDetl1(Generic.ToInt(ViewState("TransNo")), Generic.ToInt(ViewState("BenefitTypeNo")), Generic.ToInt(txtYear.Text))
    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            'hifEmployeeNo.Value = Generic.ToInt(ViewState("TransNo"))
            'txtFullName.Text = Generic.ToStr(ViewState("Name"))

            Try
                If Generic.ToInt(ViewState("BenefitTypeNo")) > 0 Then
                    cboBenefitTypeNo.DataSource = SQLHelper.ExecuteDataSet("EbenefitType_WebLookupBalance_Union", UserNo, Generic.ToInt(ViewState("BenefitTypeNo")), PayLocNo)
                    cboBenefitTypeNo.DataValueField = "tNo"
                    cboBenefitTypeNo.DataTextField = "tDesc"
                    cboBenefitTypeNo.DataBind()
                Else
                    cboBenefitTypeNo.DataSource = SQLHelper.ExecuteDataSet("EbenefitType_WebLookupBalance", UserNo, PayLocNo)
                    cboBenefitTypeNo.DataValueField = "tNo"
                    cboBenefitTypeNo.DataTextField = "tDesc"
                    cboBenefitTypeNo.DataBind()
                End If

            Catch ex As Exception
            End Try

            'cboBenefitTypeNo.Text = IIf(Generic.ToInt(ViewState("BenefitTypeNo")) = 0, "", Generic.ToInt(ViewState("BenefitTypeNo")))
            mdlMain.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitCreditNo"}))

                Try
                    cboBenefitTypeNo.DataSource = SQLHelper.ExecuteDataSet("EBenefitType_WebLookupBalance_Union", UserNo, Generic.ToInt(cboBenefitTypeNo.SelectedValue), PayLocNo)
                    cboBenefitTypeNo.DataValueField = "tNo"
                    cboBenefitTypeNo.DataTextField = "tDesc"
                    cboBenefitTypeNo.DataBind()
                Catch ex As Exception
                End Try

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EBenefitCredit_WebOne", UserNo, Generic.ToInt(i))
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


    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BenefitCreditNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EBenefitCredit", UserNo, item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)

        End If
    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If SaveRecordDetl() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Private Function SaveRecordDetl() As Boolean
        Dim BenefitCreditNo As Integer = Generic.ToInt(txtBenefitCreditNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))
        Dim BenefitTypeNo As Integer = Generic.ToInt(cboBenefitTypeNo.SelectedValue)
        Dim Amount As Double = Generic.ToDec(txtAmount.Text)
        Dim AcquireDate As String = Generic.ToStr(txtAcquireDate.Text)
        Dim Remark As String = Generic.ToStr(txtRemark.Text)

        If SQLHelper.ExecuteNonQuery("EBenefitCredit_WebSave", UserNo, BenefitCreditNo, EmployeeNo, BenefitTypeNo, Amount, AcquireDate, Remark) > 0 Then
            SaveRecordDetl = True
        Else
            SaveRecordDetl = False
        End If

    End Function

    Protected Sub lnkExportD_Click(sender As Object, e As EventArgs)
        Try
            grdExportD.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
            grdExportUsed.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkExportUsed_Click(sender As Object, e As EventArgs)
        Try
            grdExportUsed.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub




    'Protected Sub grdDetl1_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles grdDetl1.CustomSummaryCalculate
    '    ' Initialization.
    '    Dim currRow As Integer = e.RowHandle
    '    If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
    '        If e.Item.FieldName = "PaidHrs" Then
    '            PaidHrs = 0
    '            CancelHrs = 0
    '        End If

    '        If e.Item.FieldName = "tStatus" Then
    '            CancelCount = 0
    '        End If
    '    End If

    '    ' Calculation.
    '    If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then

    '        If Generic.ToInt(e.GetValue("ApprovalStatNo")) = 1 Then
    '            PaidHrs += Convert.ToDouble(Generic.ToDec(grdDetl1.GetRowValues(currRow, "PaidHrs")))
    '        End If

    '        If Generic.ToInt(e.GetValue("ApprovalStatNo")) = 2 And Generic.ToBol(e.GetValue("IsCancel")) = False Then
    '            PaidHrs += Convert.ToDouble(Generic.ToDec(grdDetl1.GetRowValues(currRow, "PaidHrs")))
    '        End If

    '        If Generic.ToBol(e.GetValue("IsCancel")) = True Then
    '            CancelCount += 1
    '            CancelHrs += Convert.ToDouble(Generic.ToDec(grdDetl1.GetRowValues(currRow, "PaidHrs")))
    '        End If

    '    End If

    '    ' Finalization.
    '    If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize Then

    '        If e.Item.FieldName = "PaidHrs" Then
    '            e.TotalValue = "Paid Hrs=" + PaidHrs.ToString
    '        End If

    '        If e.Item.FieldName = "tStatus" Then
    '            e.TotalValue = "Cancelled Count=" + CancelCount.ToString
    '        End If

    '    End If

    'End Sub


End Class


