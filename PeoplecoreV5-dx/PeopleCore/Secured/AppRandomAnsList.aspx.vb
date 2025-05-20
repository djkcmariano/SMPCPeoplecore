Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web


Partial Class Secured_AppRandomAnsList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then

        End If
        PopulateGrid()
        'Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        'Dim _dt As DataTable
        '_dt = SQLHelper.ExecuteDataTable("EApplicantRandomAns_Web", UserNo, PayLocNo)
        'Me.grdMain.DataSource = _dt
        'Me.grdMain.DataBind()
        grdMain.DataSourceID = SqlDataSource1.ID
        Generic.PopulateSQLDatasource("EApplicantRandomAns_Web", SqlDataSource1, UserNo.ToString(), PayLocNo.ToString())
    End Sub



    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkAccept_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ApplicantRandomAnsNo"})
            Dim str As String = "", i As Integer = 0, retVal As Integer = 0, successcount As Integer = 0, inprocesscount As Integer = 0
            For Each item As Integer In fieldValues
                retVal = Generic.ToInt(SQLHelper.ExecuteScalar("EApplicantRandomAns_WebCandidate", UserNo, item))
                If retVal = 2 Then
                    successcount = successcount + 1
                ElseIf retVal = 1 Then
                    inprocesscount = retVal
                End If
            Next

            If successcount > 0 Then
                'i = i / 2
                MessageBox.Success("(" + successcount.ToString + ") " + "applicant successfully tagged Accepted", Me)
            ElseIf inprocesscount = 1 Then
                MessageBox.Information("Applicant is already in process in other Manpower Request", Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

            PopulateGrid()
        End If
    End Sub

    Protected Sub lnkReject_Click(sender As Object, e As System.EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ApplicantRandomAnsNo"})
            Dim str As String = "", i As Integer = 0, x As Integer = 0
            For Each item As Integer In fieldValues
                'Dim txt As ASPxMemo = TryCast(grdMain.FindRowCellTemplateControlByKey(item, grdMain.DataColumns("Remarks"), "txtRemarks"), ASPxMemo)
                'If Generic.ToStr(txt.Text) <> "" Then
                SQLHelper.ExecuteNonQuery("EApplicantRandomAns_WebEmailReject", UserNo, item)
                i = i + 1
                'Else
                'x = x + 1
                'End If

            Next

            'If x > 0 Then
            '    MessageBox.Information("(" + x.ToString + ") Record(s) unable to proceed. Please indicate a valid remarks.", Me)
            'End If

            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + "applicant successfully tagged Rejected", Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

            PopulateGrid()

        End If
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

    Protected Sub lnkQS_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim ds As New DataSet
        lnk = sender
        Dim ID As Integer = Generic.Split(lnk.CommandArgument, 0)
        'Dim IsApplicant As Boolean
        Dim MRNo As Integer = Generic.Split(lnk.CommandArgument, 2)


        ds = SQLHelper.ExecuteDataSet("EMR_Display", ID, MRNo)
        Dim dtGroup As DataTable
        dtGroup = ds.Tables(1) '.DefaultView.ToTable(True, "Title", "Value")
        If dtGroup.Rows.Count > 0 Then            
            grdQS.DataSource = dtGroup
            grdQS.DataBind()
        End If


        ModalPopupExtender1.Show()

    End Sub
    Protected Sub lnkQSx_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim ds As New DataSet
        lnk = sender
        Dim ID As Integer = Generic.Split(lnk.CommandArgument, 0)
        Dim IsApplicant As Boolean = Generic.Split(lnk.CommandArgument, 1)
        Dim MRNo As Integer = Generic.Split(lnk.CommandArgument, 2)
        Dim applicantno As Integer = Generic.Split(lnk.CommandArgument, 3)

        Response.Redirect("apprandomanslist_qs.aspx?ID=" & ID.ToString & "&mrno=" & MRNo.ToString & "&xID=" & applicantno.ToString & "&isapplicant=" & IsApplicant)

    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    
End Class
