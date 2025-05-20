Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_OrgFacilityList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TableName As String
    Dim Col As String

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        Dim tStatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
        If tStatus = 0 Then
            lnkDelete.Visible = False
            lnkArchive.Visible = True
        ElseIf tStatus = 1 Then
            lnkDelete.Visible = True
            lnkArchive.Visible = False
        Else
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        End If

        _dt = SQLHelper.ExecuteDataTable("EFacility_Web", UserNo, PayLocNo, tStatus)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 14)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TableName = Generic.ToStr(Session("xTablename"))
        Col = Mid(TableName, 2, Len(TableName) - 1)

        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/OrgFacilityList_Edit.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Response.Redirect("~/secured/OrgFacilityList_Edit.aspx?id=" & Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"FacilityNo"})))
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"FacilityNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol(TableName & "Head", UserNo, Col & "No", Generic.ToInt(item))
                Generic.DeleteRecordAudit("EFacility", UserNo, Generic.ToInt(item))
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

    Protected Sub lnkArchive_Click(sender As Object, e As EventArgs)

        Dim dt As DataTable, tProceed As Boolean = False
        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "FacilityNo"))
                dt = SQLHelper.ExecuteDataTable("ETableReferrence_WebArchived", UserNo, "EFacility", item, 1, PayLocNo)
                For Each row As DataRow In dt.Rows
                    tProceed = Generic.ToBol(row("tProceed"))
                Next

                grdMain.Selection.UnselectRow(j)
                i = i + 1
            End If
        Next

        PopulateGrid()
        MessageBox.Success("(" + i.ToString + ") transaction(s) successfully archived.", Me)

    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

End Class



