Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_OrgTableReclass
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim PayLocNo As Integer    


    Protected Sub PopulateGrid()
        Dim _dt As DataTable
        lnkPost.Visible = True
        lnkDelete.Visible = True
        lnkAdd.Visible = True
        If Generic.ToInt(cboTabNo.SelectedValue) = 1 Then
            lnkPost.Visible = False
            lnkDelete.Visible = False
            lnkAdd.Visible = False
        End If
        _dt = SQLHelper.ExecuteDataTable("ETableOrgReclass_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateDropDown()
        cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 4)
        cboTabNo.DataValueField = "tNo"
        cboTabNo.DataTextField = "tDesc"
        cboTabNo.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim URL As String
        lnk = sender
        URL = Generic.GetFirstTab(lnk.CommandArgument)
        If URL <> "" Then
            Response.Redirect(URL)
        End If
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

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TableOrgReclassNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ETableOrgReclass", UserNo, item)
                Generic.DeleteRecordAuditCol("ETableOrgReclassDeti", UserNo, "TableOrgReclassNo", item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TableOrgReclassNo"})
            Dim str As String = ""
            For Each item As Object In fieldValues
                If Generic.ToBol(item) Then
                    count = count + Generic.ToInt(SQLHelper.ExecuteNonQuery("EHRANMass_WebPost", UserNo, Generic.ToInt(item), PayLocNo))
                Else
                    MessageBox.Warning("Unable to post, ready for posting must check first.", Me)
                End If
            Next
            If count > 0 Then
                MessageBox.Success("" + count.ToString + " transaction(s) has been successfully posted. ", Me)
                PopulateGrid()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
            
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

End Class



