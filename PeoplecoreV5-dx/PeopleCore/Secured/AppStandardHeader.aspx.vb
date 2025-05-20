Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_AppStandardHeader
    Inherits System.Web.UI.Page

    Dim MenuMassNo As Integer = 1
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim clsGen As New clsGenericClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropdown()
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub
    Protected Sub PopulateDropdown()

        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 14)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

#Region "Main"

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim tStatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
        If tStatus = 0 Then
            lnkDelete.Visible = False
            lnkArchive.Visible = True
        ElseIf tStatus = 1 Then
            lnkDelete.Visible = True
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        Else
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        End If
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EApplicantStandardHeader_Web", UserNo, "", PayLocNo, MenuMassNo, Generic.ToInt(cboTabNo.SelectedValue))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        PopulateGridDetl()
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantStandardHeaderNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EApplicantStandardHeader_WebOne", UserNo, Generic.ToInt(i))
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

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkArchive_Click(sender As Object, e As EventArgs)

        Dim dt As DataTable, tProceed As Boolean = False
        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "Pk"))
                dt = SQLHelper.ExecuteDataTable("ETableReferrence_WebArchived", UserNo, "EVesselRate", item, 1, PayLocNo)
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

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ApplicantStandardHeaderNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EApplicantStandardHeader", UserNo, item)
                Generic.DeleteRecordAuditCol("EApplicantStandardMain", UserNo, "ApplicantStandardHeaderNo", item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid(True)
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
        Dim ApplicantStandardHeaderNo = Generic.ToInt(txtApplicantStandardHeaderNo.Text)
        Dim ApplicantStandardHeaderCode As String = Generic.ToStr(txtApplicantStandardHeaderCode.Text)
        Dim ApplicantStandardHeaderDesc As String = Generic.ToStr(txtApplicantStandardHeaderDesc.Text)
        Dim IsApplyAll As Boolean = Generic.ToBol(txtIsApplyToAll.Checked)
        Dim ApplicableYear As Integer = Generic.ToInt(Me.txtApplicableyear.Text)

        Dim dt As New DataTable, error_num As Integer = 0, error_message As String = "", tRetVal As Integer = 0
        dt = SQLHelper.ExecuteDataTable("EApplicantStandardHeader_WebSave", UserNo, ApplicantStandardHeaderNo, ApplicantStandardHeaderCode, ApplicantStandardHeaderDesc, ApplicableYear, IsApplyAll, 0, MenuMassNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
        For Each row As DataRow In dt.Rows
            Retval = True
            error_num = Generic.ToInt(row("Error_num"))
            tRetVal = Generic.ToInt(row("retVal"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                Retval = False
            ElseIf tRetVal > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Alert(error_message, "", Me)
                mdlMain.Show()
                Retval = False
            End If

        Next
        If Retval = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If Retval = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub

    Protected Sub lnkForm_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lnk As New LinkButton, i As Integer
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantStandardHeaderNo"}))

        Response.Redirect("~/secured/AppStandardMain_List.aspx?id=" & i)

    End Sub

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantStandardHeaderNo", "Code", "IsEnabled"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        lnkAddDetl.Enabled = Generic.ToBol(obj(2))
        PopulateGridDetl()

    End Sub

#End Region

#Region "Detail"
    Private Sub PopulateGridDetl()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicantStandardHeaderDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
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
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"ApplicantStandardHeaderDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EApplicantStandardHeaderDeti", UserNo, item)
                i = i + 1
            Next

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
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantStandardHeaderDetiNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EApplicantStandardHeaderDeti_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
                    Generic.PopulateData(Me, "pnlPopupDetl", dt)
                Next
                mdlShowDetl.Show()

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
            mdlShowDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub btnSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If SaveRecordDetl() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecordDetl() As Boolean
        Dim ApplicantStandardHeaderDetiNo As Integer = Generic.ToInt(txtApplicantStandardHeaderDetiNo.Text)
        Dim PositionNo As Integer = Generic.ToInt(cboPositionNo.SelectedValue)


        If SQLHelper.ExecuteNonQuery("EApplicantStandardHeaderDeti_WebSave", UserNo, ApplicantStandardHeaderDetiNo, Generic.ToInt(ViewState("TransNo")), PositionNo) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region


End Class

