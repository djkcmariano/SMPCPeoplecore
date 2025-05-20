Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web


Partial Class SecuredManager_SelfAppRandomAnsApprList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            PopulateDropDown()
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
        Generic.PopulateSQLDatasource("EApplicantRandomAns_WebManager", SqlDataSource1, UserNo.ToString(), PayLocNo.ToString())
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 43)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
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

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkAccept_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0


        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ApplicantRandomAnsNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            'SQLHelper.ExecuteNonQuery("EApplicantRandomAns_WebCandidate", UserNo, item)
            ApproveTransaction(item, 2)
            i = i + 1
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + "transaction successfully accepted", Me)
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If



    End Sub

    Protected Sub lnkReject_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0


        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ApplicantRandomAnsNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, 3)
            'SQLHelper.ExecuteNonQuery("EApplicantRandomAns_WebEmailReject", UserNo, item)
            i = i + 1
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDisapproved, Me)
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If



    End Sub

    Private Sub ApproveTransaction(tId As Integer, approvalStatNo As Integer)
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("EApplicantRandomAns_WebApproved", UserNo, tId, approvalStatNo)
        If fds.Tables.Count > 0 Then
            If fds.Tables(0).Rows.Count > 0 Then
                Dim IsWithapprover As Boolean
                IsWithapprover = Generic.CheckDBNull(fds.Tables(0).Rows(0)("IsWithApprover"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                If IsWithapprover = True Then

                Else
                    MessageBox.Information("Unable to locate the next approver.", Me)
                End If
            End If


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
        'Dim IsApplicant As Boolean
        Dim MRNo As Integer = Generic.Split(lnk.CommandArgument, 2)
        Dim applicantno As Integer = Generic.Split(lnk.CommandArgument, 3)

        Response.Redirect("apprandomansapprlist_qs.aspx?ID=" & ID.ToString & "&mrno=" & MRNo.ToString & "&applicantno=" & applicantno.ToString)

    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub


End Class
