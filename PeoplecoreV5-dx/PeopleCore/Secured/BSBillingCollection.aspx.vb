Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web


Partial Class Secured_BSBillingCollection
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim xBase As New clsBase.clsBaseLibrary
    Dim IsCompleted As Integer = 0
    Dim process_status As String = ""
    Dim err_num As Integer = 0
    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable

        _dt = SQLHelper.ExecuteDataTable("BBSRegister_Web", UserNo, "", PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)



        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim URL As String
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSRegisterNo"})))
        Dim BSRegisterNo As String = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSRegisterNo"}))
        Response.Redirect("BSBillingCollection_Edit.aspx?id=" & BSRegisterNo.ToString)
    End Sub



    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim URL As String
            URL = Generic.GetFirstTab("0")
            If URL <> "" Then
                Response.Redirect(URL)
            End If
    
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BSRegisterNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("BBSRegister", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub



    Protected Sub lnkPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BSRegisterNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("BBSRegister_WebPost", UserNo, CType(item, Integer)) Then
                    DeleteCount = DeleteCount + 1
                End If
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccesPost, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub

    

End Class



