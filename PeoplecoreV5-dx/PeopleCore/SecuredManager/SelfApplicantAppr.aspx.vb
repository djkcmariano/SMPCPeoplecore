Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class SecuredManager_SelfApplicantAppr
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        Permission.IsAuthenticatedSuperior()

        PopulateGrid()
        'Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EApplicantRandomAns_WebAppr", UserNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkAccept_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0

        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ApplicantRandomAnsNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            SQLHelper.ExecuteNonQuery("EApplicantRandomAns_WebCandidate", UserNo, item)
            i = i + 1
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + "transaction succefully accepted", Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

        PopulateGrid()
        'End If
    End Sub

    Protected Sub lnkReject_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0

        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ApplicantRandomAnsNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            SQLHelper.ExecuteNonQuery("EApplicantRandomAns_WebEmailReject", UserNo, item)
            i = i + 1
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDisapproved, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

        PopulateGrid()

        'End If
    End Sub

    Protected Sub lnk_Click(sender As Object, e As EventArgs)
        'Info1.xIsApplicant = True
        Dim lnk As New LinkButton
        lnk = sender

        Info1.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
        Info1.xIsApplicant = Generic.Split(lnk.CommandArgument, 1)
        Info1.Show()
    End Sub

    Protected Sub lnkHistory_Click(sender As Object, e As EventArgs)
        'Info1.xIsApplicant = True
        Dim lnk As New LinkButton
        lnk = sender

        History.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
        History.xIsApplicant = Generic.Split(lnk.CommandArgument, 1)
        History.Show()
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

End Class
