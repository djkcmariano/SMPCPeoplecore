Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports RestSharp
Imports System.Threading.Tasks
Imports System.Net


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
            For Each item As String In fieldValues

                Dim dt As New DataTable

                dt = SQLHelper.ExecuteDataTable("EApplicantRandomAns_WebCandidate", UserNo, item)

                For Each row As DataRow In dt.Rows

                    retVal = Generic.ToInt(row("tSucceed"))

                Next

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
        Dim ID As String = Generic.Split(lnk.CommandArgument, 0)
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

#Region "Sync API"

    Protected Async Sub lnkSync_Click(sender As Object, e As EventArgs)
        Try
            Dim client = Await GetClientAsync()
            Dim operation As String = "Pull"

            Dim processedCount = 0

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("GetTableSync", "Applicant", operation)
            For Each row As DataRow In dt.Rows
                Dim tableName = Generic.ToStr(row("TableName"))
                Dim limit = Generic.ToStr(row("Limit"))
                Await ProcessData(client, limit, tableName, operation)
                processedCount += 1
            Next

            client.Dispose()
            If processedCount > 0 Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessProcess, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Catch ex As Exception
            MessageBox.Warning(ex.ToString(), Me)
        End Try
    End Sub

    Async Function ProcessData(client As RestSharp.RestClient, limit As Integer, tableName As String, operation As String) As Task
        Try
            Dim postRequest = New RestRequest("api/" & operation & "/data", Method.Post)
            postRequest.AddJsonBody(New With {
            .limit = limit,
            .tableName = tableName
        })

            Dim postResponse = Await client.ExecuteAsync(postRequest)

            If postResponse.IsSuccessful AndAlso postResponse.Content IsNot Nothing Then
                Console.WriteLine("Successfully processed " & tableName)
                Return
            Else
                Throw New Exception("Failed to process " & tableName & ". Status: " & postResponse.StatusCode & ", Message: " & postResponse.Content)
            End If
        Catch ex As Exception
            Console.WriteLine("Error processing " & tableName & ": " & ex.Message)
            Throw
        End Try
    End Function


    Public Class Table
        Public Property tableName As String
        Public Property limit As Integer
    End Class


    Private _cachedToken As String = String.Empty
    Private _tokenExpiration As DateTime

    Private _restClient As RestSharp.RestClient

    Public Sub New()
        _restClient = New RestSharp.RestClient(ConfigurationManager.AppSettings("API:BaseURL"))
    End Sub

    Public Async Function GetClientAsync() As Task(Of RestSharp.RestClient)
        Dim token As String = Await GetTokenAsync()
        _restClient.AddDefaultHeader("Authorization", "Bearer " & token)
        Return _restClient
    End Function

    Public Class AuthTokenResponse
        Public Property token As String
        Public Property expiresIn As Integer
    End Class

    Async Function GetTokenAsync() As Task(Of String)

        If Not String.IsNullOrEmpty(_cachedToken) AndAlso _tokenExpiration > DateTime.Now.AddMinutes(1) Then
            Return _cachedToken
        End If
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim client As New RestSharp.RestClient(ConfigurationManager.AppSettings("API:BaseURL"))
            Dim request = New RestRequest("api/auth", Method.Post)

            request.AddJsonBody(New With {
            .username = ConfigurationManager.AppSettings("API:Username"),
            .password = ConfigurationManager.AppSettings("API:Password")})

            Dim response = Await client.ExecuteAsync(Of AuthTokenResponse)(request)
            If response.StatusCode = Net.HttpStatusCode.OK Then
                Return response.Content.Trim("""")
            End If

            _cachedToken = response.Data.token
            _tokenExpiration = DateTime.Now.AddSeconds(response.Data.expiresIn)
            Return _cachedToken
        Catch ex As Exception
            Throw New Exception("Failed to obtain JWT token", ex)
        End Try
    End Function


#End Region

End Class
