Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export


Partial Class Secured_SecEnableMenu
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("SMenuMass_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        PopulateDetl()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

#Region "********Main*******"


    Protected Sub lnkEnable_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"MenuMassNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("SMenuMass_WebSave", UserNo, item, 1)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)

        End If
    End Sub
    Protected Sub lnkDisable_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"MenuMassNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("SMenuMass_WebSave", UserNo, item, 0)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)

        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 38)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

#End Region

#Region "Details"
    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        ViewState("TransNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MenuMassNo"}))
        lbl.Text = "Transaction No. : " & Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MenuMassDesc"}))
        PopulateDetl()

    End Sub

    Private Sub PopulateDetl()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("SMenuMassDeti_Web", UserNo, PayLocNo, Generic.ToInt(ViewState("TransNo")))
        Me.grdDetl.DataSource = _dt
        Me.grdDetl.DataBind()
    End Sub

    Protected Sub lnkExportDeti_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkEnableDeti_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"MenuNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("SMenuMassDeti_WebSave", UserNo, item, 1)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateDetl()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)

        End If
    End Sub
    Protected Sub lnkDisableDeti_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"MenuNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("SMenuMassDeti_WebSave", UserNo, item, 0)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateDetl()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)

        End If
    End Sub
    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MenuNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("SMenuMassDeti_WebOne", UserNo, id)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel2", dt)
            PopulateCombo()
        Next
    End Sub
    Private Sub PopulateCombo()
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        Try
            cboMenuMassNo.DataSource = SQLHelper.ExecuteDataSet("SMenuMass_WebLookup_Code", UserNo, 1)
            cboMenuMassNo.DataTextField = "tdesc"
            cboMenuMassNo.DataValueField = "tCode"
            cboMenuMassNo.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = "", error_num As Integer = 0, error_message As String = ""
        Dim dt As DataTable, Retval As Boolean = False

        dt = SQLHelper.ExecuteDataTable("SMenuMassDeti_WebSave_Transfer", UserNo, Generic.ToInt(hifmenuno.Value), Generic.ToStr(cboMenuMassNo.SelectedValue), Generic.ToStr(txtMenuStyle.Text))

        For Each row As DataRow In dt.Rows
            Retval = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                Retval = False
            End If

        Next
        If Retval = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If Retval = True Then
            PopulateDetl()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub

#End Region

End Class


















