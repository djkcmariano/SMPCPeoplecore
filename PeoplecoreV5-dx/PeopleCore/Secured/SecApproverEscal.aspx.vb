Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured__SecApproverEscal
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Dim clsGen As New clsGenericClass


    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EApproverScal_Web", UserNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateGridDetl(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApproverScalDeti_Web", UserNo, id)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

#Region "********Main*******"

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApproverScalNo", "Code", "ApproverCodeDesc"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = "Transaction No. : " & obj(1) & " - " & obj(2)
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))

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
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ApproverScalNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EApproverScalDeti", UserNo, "ApproverScalNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EApproverScal", UserNo, CType(item, Integer))
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
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApproverScalNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EApproverScal_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                Next
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
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    'Submit record
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim ApproverScalNo As Integer = Generic.ToInt(Me.txtApproverScalNo.Text)
            Dim ApproverCodeNo As Integer = Generic.ToInt(Me.cboApproverCodeNo.SelectedValue)
            Dim ApproverScalDesc As String = Generic.ToStr(Me.txtApproverScalDesc.Text)
            Dim AppliedHrs As Double = Generic.ToDec(Me.txtAppliedHrs.Text)
            Dim JobGradeGroupNo As Integer = Generic.ToInt(Me.cboSectionPositionGrpNo.SelectedValue)
            Dim DayTypeNo As Integer = Generic.ToInt(Me.cboDayTypeNo.SelectedValue)
            Dim ApproverScalTypeNo As Integer = Generic.ToInt(Me.cboApproverScalTypeNo.SelectedValue)
            Dim NoOfScal As Integer = Generic.ToInt(Me.txtNOOfScal.Text)
            Dim OrganizationLimitNo As Integer = Generic.ToInt(Me.cboOrganizationLimitNo.SelectedValue)
            Dim OrganizationLimitNextNo As Integer = Generic.ToInt(Me.cboOrganizationLimitNextNo.SelectedValue)
            Dim JobGradeNo As Integer = Generic.ToInt(Me.cboJobGradeNo.SelectedValue)

            Dim dt As DataTable
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            dt = SQLHelper.ExecuteDataTable("EApproverScal_WebValidate", UserNo, ApproverScalNo, ApproverCodeNo, ApproverScalDesc, AppliedHrs, JobGradeGroupNo, DayTypeNo, ApproverScalTypeNo, NoOfScal, OrganizationLimitNo, JobGradeNo, PayLocNo)
            For Each row As DataRow In dt.Rows
                invalid = Generic.ToBol(row("Invalid"))
                messagedialog = Generic.ToStr(row("MessageDialog"))
                alerttype = Generic.ToStr(row("AlertType"))
            Next

            If invalid = True Then
                mdlMain.Show()
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If

            If SQLHelper.ExecuteNonQuery("EApproverScal_WebSave", UserNo, ApproverScalNo, ApproverCodeNo, ApproverScalDesc, AppliedHrs, JobGradeGroupNo, DayTypeNo, ApproverScalTypeNo, NoOfScal, OrganizationLimitNo, OrganizationLimitNextNo, JobGradeNo, PayLocNo) > 0 Then
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
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

#End Region


#Region "********Detail********"


    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub


    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"ApproverScalDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EApproverScalDeti", UserNo, CType(item, Integer))
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
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApproverScalDetiNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EApproverScalDeti_WebOne", UserNo, Generic.ToInt(i))
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
            Dim ApproverScalDetiNo As Integer = Generic.ToInt(Me.txtApproverScalDetiNo.Text)
            Dim OrganizationNo As Integer = Generic.ToInt(Me.cboOrganizationNo.SelectedValue)
            Dim OrderLevel As Integer = Generic.ToInt(Me.txtOrderLevel.Text)
            Dim EmployeeNo As Integer = Generic.ToInt(Me.cboEmployeeNo.SelectedValue)
            Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)
            Dim IsHigherThankLimit As Boolean = Generic.ToBol(Me.chkIsHigherThanLimit.Checked)
            Dim OrganizationGroupNo As Integer = Generic.ToInt(Me.cboOrganizationGroupNo.SelectedValue)


            Dim dt As DataTable
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            dt = SQLHelper.ExecuteDataTable("EApproverScalDeti_WebValidate", UserNo, ApproverScalDetiNo, tno, OrganizationNo, OrderLevel, EmployeeNo, Remarks)
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

            If SQLHelper.ExecuteNonQuery("EApproverScalDeti_WebSave", UserNo, ApproverScalDetiNo, tno, OrganizationNo, OrderLevel, EmployeeNo, Remarks, IsHigherThankLimit, OrganizationGroupNo) > 0 Then
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
    Protected Sub cboOrganizationNo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboOrganizationNo.SelectedIndexChanged

        Select Case Generic.ToInt(Me.cboOrganizationNo.SelectedValue)
            Case 4, 10, 11
                cboEmployeeNo.Enabled = True
            Case Else
                cboEmployeeNo.Enabled = False
                cboEmployeeNo.Text = ""
        End Select

        mdlDetl.Show()

    End Sub
#End Region

End Class

