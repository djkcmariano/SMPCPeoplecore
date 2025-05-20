Imports clsLib
Imports System.Data
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting

Partial Class Secured_PayLastLeaveDetiList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim PayNo As Integer = 0
    Dim PayLastDetiNo As Integer = 0
    Dim EmployeeNo As Integer = 0

    Private Sub PopulateGrid()

        If txtIsPosted.Checked = False Then
            lnkAddDetl.Visible = True
            lnkDeleteDetl.Visible = True
        Else
            lnkAddDetl.Visible = False
            lnkDeleteDetl.Visible = False
        End If

        'Populate Data
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayLastLeaveDeti_Web", UserNo, PayLastDetiNo)
        grdMain.DataSource = _dt
        grdMain.DataBind()

        If ViewState("LeaveTypeNo") = 0 Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"LeaveTypeNo", "Code"})
            ViewState("LeaveTypeNo") = obj(0)
            lblDetl.Text = obj(1)
        End If

        PopulateGridDetl()

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        PayLastDetiNo = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticatedCoreUser()
        PopulateTabHeader()
        HeaderInfo1.xFormName = "EPayLastDeti"

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
        End If
        PopulateGrid()
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            'dt = SQLHelper.ExecuteDataTable("EPayLastDeti_WebTabHeader", UserNo, PayLastDetiNo)
            dt = SQLHelper.ExecuteDataTable("EPay_WebTabHeader", UserNo, Session("PayLastList_PayNo"))
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
                PayNo = Generic.ToInt(row("PayNo"))
                EmployeeNo = Generic.ToInt(row("EmployeeNo"))
            Next
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

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveTypeNo", "Code"})
        ViewState("LeaveTypeNo") = obj(0)
        lblDetl.Text = obj(1)

        PopulateGridDetl()

    End Sub


#Region "********Detail********"

    Private Sub PopulateGridDetl()

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayLastLeaveDeti_ManualAdjust_Web", UserNo, PayLastDetiNo, EmployeeNo, Generic.ToInt(ViewState("LeaveTypeNo")))
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
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"LeaveCreditNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ELeaveCredit", UserNo, item)
                i = i + 1
            Next

            If i > 0 Then
                PopulateGridDetl()
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer, IsEnabled As Boolean = False
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveCreditNo"}))
                Generic.ClearControls(Me, "pnlPopupDetl")

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("ELeaveCredit_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupDetl", dt)
                Next

                'Enable or Disable Controls
                If txtIsPosted.Checked = True Then
                    IsEnabled = False
                Else
                    IsEnabled = True
                End If
                Generic.EnableControls(Me, "pnlPopupDetl", IsEnabled)
                lnkSaveDetl.Enabled = IsEnabled

                mdlDetl.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)

        If Generic.ToInt(ViewState("LeaveTypeNo")) > 0 Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            cboLeaveTypeNo.Text = Generic.ToInt(ViewState("LeaveTypeNo"))
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

    Protected Sub lnkSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim LeaveCreditNo As Integer = Generic.ToInt(txtLeaveCreditNo.Text)
            Dim LeaveTypeNo As Integer = Generic.ToInt(cboLeaveTypeno.SelectedValue)
            Dim LeaveHrs As Double = Generic.ToDec(txtLeaveHrs.Text)
            Dim AcquireDate As String = Generic.ToStr(txtAcquireDate.Text)
            Dim Remark As String = Generic.ToStr(txtRemark.Text)

            If SQLHelper.ExecuteNonQuery("EPayLastLeaveDeti_ManualAdjust_WebSave", UserNo, PayLastDetiNo, LeaveCreditNo, EmployeeNo, LeaveTypeNo, LeaveHrs, AcquireDate, Remark) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                PopulateGridDetl()
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
