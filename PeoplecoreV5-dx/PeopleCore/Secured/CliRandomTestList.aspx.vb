Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_CliRandomTestList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim err_num As Integer = 0
    Dim process_status As String = ""

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EClinicRandomTest_Web", UserNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateGridDetl(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinicRandomTestDeti_Web", UserNo, id)
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
    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim URL As String = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ClinicRandomTestNo"})))
                If URL <> "" Then
                    Response.Redirect(URL)
                End If

            Else
                MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"ClinicRandomTestNo", "Code"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = "Transaction No. : " & obj(1)
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        TransNo = Generic.ToInt(ViewState("TransNo"))

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ClinicRandomTestNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EClinicRandomTest", UserNo, item)
                Generic.DeleteRecordAuditCol("EClinicRandomTestDeti", UserNo, "ClinicRandomTestNo", item)
                Generic.DeleteRecordAuditCol("EClinicRandomTestFilter", UserNo, "ClinicRandomTestNo", item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                PopulateGridDetl(TransNo)
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)

        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            'Generic.ClearControls(Me, "pnlPopupMain")
            'mdlMain.Show()
            Response.Redirect("~/secured/CliRandomTestEdit.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkProcess_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
                Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ClinicRandomTestNo"})
                If fieldValues.Count > 1 Or fieldValues.Count = 0 Then
                    MessageBox.Warning("Please select 1 transaction to process.", Me)
                    Exit Sub
                End If
                If fieldValues.Count = 1 Then
                    For Each item As Integer In fieldValues
                        RandomAsynProcess(item, PayLocNo)
                    Next
                End If
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessProcess & " " & Now().ToString, Me)
            Else
                MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
            End If
        Catch ex As Exception
        End Try
    End Sub
    
    Private Sub RandomAsynProcess(ByVal id As String, ByVal payloc As Integer)
        Dim cmd As SqlClient.SqlCommand
        Try
            cmd = Nothing
            cmd = New SqlClient.SqlCommand
            cmd.CommandText = "EClinicRandomTest_WebProcess"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = AssynChronous.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            cmd.CommandTimeout = 0
            cmd.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            cmd.Parameters("@onlineuserno").Value = Generic.ToInt(UserNo)
            cmd.Parameters.Add("@ClinicRandomTestNo", SqlDbType.Int, 4)
            cmd.Parameters("@ClinicRandomTestNo").Value = id
            cmd.Parameters.Add("@paylocno", SqlDbType.Int, 4)
            cmd.Parameters("@paylocno").Value = payloc
            process_status = AssynChronous.xRunCommandAsynchronous(cmd, "EClinicRandomTest_WebProcess", SQLHelper.ConSTRAsyn, 0, err_num)
        Catch
        End Try
    End Sub

#End Region

#Region "********Detail********"

    Protected Sub lnkDeleteDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        TransNo = Generic.ToInt(ViewState("TransNo"))
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"ClinicRandomTestDetiNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EClinicRandomTestDeti", UserNo, item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGridDetl(TransNo)
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If

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

