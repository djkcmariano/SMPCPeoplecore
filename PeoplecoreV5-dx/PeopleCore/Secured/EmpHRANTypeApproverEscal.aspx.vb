Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Partial Class Secured_EmpHRANTypeApproverEscal
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Dim clsGen As New clsGenericClass


    Private Sub PopulateGrid()
        Dim tStatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
        If tStatus = 0 Then
            lnkDelete.Visible = False
            lnkArchive.Visible = True
        ElseIf tStatus = 1 Then
            lnkDelete.Visible = False
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        Else
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        End If
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EHRANTypeApproverEscal_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateGridDetl(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EHRANTypeApproverEscalDeti_Web", UserNo, id)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Try
                cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 14)
                cboTabNo.DataTextField = "tDesc"
                cboTabNo.DataValueField = "tno"
                cboTabNo.DataBind()
            Catch ex As Exception
            End Try
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
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"HRANTypeApproverEscalNo", "Code"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))

    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkArchive_Click(sender As Object, e As EventArgs)

        Dim dt As DataTable, tProceed As Boolean = False
        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "HRANTypeApproverEscalNo"))
                dt = SQLHelper.ExecuteDataTable("ETableReferrence_WebArchived", UserNo, "EHRANTypeApproverEscal", item, 1, PayLocNo)
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

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"HRANTypeApproverEscalNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EHRANTypeApproverEscalDeti", UserNo, "HRANTypeApproverEscalNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EHRANTypeApproverEscal", UserNo, CType(item, Integer))
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
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"HRANTypeApproverEscalNo"}))
                Generic.ClearControls(Me, "pnlPopupMain")

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EHRANTypeApproverEscal_WebOne", UserNo, Generic.ToInt(i))
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
            Dim HRANTypeApproverEscalNo As Integer = Generic.ToInt(Me.txtHRANTypeApproverEscalNo.Text)
            Dim HRANTypeNo As Integer = Generic.ToInt(Me.cboHRANTypeNo.SelectedValue)
            Dim HRANTypeApproverEscalDesc As String = Generic.ToStr(Me.txtHRANTypeApproverEscalDesc.Text)
            Dim SectionPositionGrpNo As Integer = Generic.ToInt(Me.cboSectionPositionGrpNo.SelectedValue)
            Dim NoOfScal As Integer = Generic.ToInt(Me.txtNOOfScal.Text)
            Dim OrganizationLimitNo As Integer = Generic.ToInt(Me.cboOrganizationLimitNo.SelectedValue)
            Dim EmployeeClassNo As Integer = Generic.ToInt(Me.cboEmployeeClassNo.SelectedValue)
            Dim JobGradeNo As Integer = Generic.ToInt(Me.cboJobGradeNo.SelectedValue)
            Dim JobGradeGroupNo As Integer = Generic.ToInt(Me.cboJobGradeGroupNo.SelectedValue)

            Dim dt As DataTable
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            dt = SQLHelper.ExecuteDataTable("EHRANTypeApproverEscal_WebValidate", UserNo, HRANTypeApproverEscalNo, HRANTypeNo, HRANTypeApproverEscalDesc, SectionPositionGrpNo, NoOfScal, OrganizationLimitNo, EmployeeClassNo, JobGradeGroupNo, JobGradeNo, PayLocNo)
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

            If SQLHelper.ExecuteNonQuery("EHRANTypeApproverEscal_WebSave", UserNo, HRANTypeApproverEscalNo, HRANTypeNo, HRANTypeApproverEscalDesc, SectionPositionGrpNo, NoOfScal, OrganizationLimitNo, EmployeeClassNo, JobGradeGroupNo, JobGradeNo, PayLocNo, Generic.ToInt(chkIsArchived.Checked)) > 0 Then
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
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"HRANTypeApproverEscalDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EHRANTypeApproverEscalDeti", UserNo, CType(item, Integer))
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
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"HRANTypeApproverEscalDetiNo"}))
                Generic.ClearControls(Me, "pnlPopupDetl")

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EHRANTypeApproverEscalDeti_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupDetl", dt)
                Next

                Select Case Generic.ToInt(Me.cboOrganizationNo.SelectedValue)
                    Case 4, 9, 10, 11, 12, 13
                        txtFullName.Enabled = True
                    Case Else
                        txtFullName.Text = ""
                        txtFullName.Enabled = False
                        hifEmployeeNo.Value = 0
                End Select

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
                txtFullName.Enabled = False
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
            Dim HRANTypeApproverEscalDetiNo As Integer = Generic.ToInt(Me.txtHRANTypeApproverEscalDetiNo.Text)
            Dim OrganizationNo As Integer = Generic.ToInt(Me.cboOrganizationNo.SelectedValue)
            Dim OrderLevel As Integer = Generic.ToInt(Me.txtOrderLevel.Text)
            Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
            Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)
            Dim IsAlternate As Boolean = Generic.ToBol(Me.txtIsAlternate.Checked)

            Dim dt As DataTable
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            dt = SQLHelper.ExecuteDataTable("EHRANTypeApproverEscalDeti_WebValidate", UserNo, HRANTypeApproverEscalDetiNo, tno, OrganizationNo, OrderLevel, EmployeeNo, Remarks, IsAlternate)
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

            If SQLHelper.ExecuteNonQuery("EHRANTypeApproverEscalDeti_WebSave", UserNo, HRANTypeApproverEscalDetiNo, tno, OrganizationNo, OrderLevel, EmployeeNo, Remarks, IsAlternate) > 0 Then
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
            Case 4, 9, 10, 11, 12, 13
                txtFullName.Enabled = True
            Case Else
                txtFullName.Text = ""
                txtFullName.Enabled = False
                hifEmployeeNo.Value = 0
        End Select

        mdlDetl.Show()

    End Sub
#End Region

End Class
