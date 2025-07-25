﻿Imports clsLib
Imports System.Data
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports RestSharp
Imports System.Threading.Tasks
Imports System.Net
Partial Class Secured_AppMRList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()
            'PopulateGrid()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub


    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender

        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Integer = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"MRNo"})
        ViewState("MRNo") = obj

        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, Generic.ToInt(ViewState("MRNo"))))

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=494&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub


#Region "Main"
    Protected Sub PopulateGrid()
        Try
            If Generic.ToInt(cboTabNo.SelectedValue) = 1 Then
                lnkPost.Visible = False
                lnkApproved.Visible = True
                lnkDisapproved.Visible = True
            ElseIf Generic.ToInt(cboTabNo.SelectedValue) = 2 Then
                lnkPost.Visible = False
                lnkApproved.Visible = False
                lnkDisapproved.Visible = False
            ElseIf Generic.ToInt(cboTabNo.SelectedValue) = 3 Then
                lnkPost.Visible = True
                lnkApproved.Visible = False
                lnkDisapproved.Visible = False
            Else
                lnkPost.Visible = False
                lnkApproved.Visible = False
                lnkDisapproved.Visible = False
            End If

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMR_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 10)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/appmredit.aspx?id=0")
        End If
    End Sub

    Protected Sub lnkSnyc_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim ds As New DataSet
        ds = SQLHelper.ExecuteDataSet("ESyncProcess", UserNo, PayLocNo, 0)
        If ds.Tables(0).Rows(0).Item(0).ToString() = "0" Then
            MessageBox.Success(MessageTemplate.SuccessSubmit, Me)
        Else
            MessageBox.Success(MessageTemplate.ErrorUpdate, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)

        Dim URL As String
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MRNo"})))
        If URL <> "" Then
            Response.Redirect(URL)
        End If
    End Sub
    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"MRNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EMR", UserNo, item)
                Generic.DeleteRecordAuditCol("EMREduc", UserNo, "MRNo", item)
                Generic.DeleteRecordAuditCol("EMRExpe", UserNo, "MRNo", item)
                Generic.DeleteRecordAuditCol("EMRComp", UserNo, "MRNo", item)
                Generic.DeleteRecordAuditCol("EMRExam", UserNo, "MRNo", item)
                Generic.DeleteRecordAuditCol("EMRTrn", UserNo, "MRNo", item)
                Generic.DeleteRecordAuditCol("EMRHiredMass", UserNo, "MRNo", item)
                Generic.DeleteRecordAuditCol("EMRInterview", UserNo, "MRNo", item)
                Generic.DeleteRecordAuditCol("EMRInterviewDeti", UserNo, "MRNo", item)
                Generic.DeleteRecordAuditCol("EMRPlantilla", UserNo, "MRNo", item)
                Generic.DeleteRecordAuditCol("EApplicantRandomAns", UserNo, "MRNo", item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
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
    Protected Sub lnkPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"MRNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("EMR_WebPost", UserNo, CType(item, Integer)) Then
                    iCount = iCount + 1
                End If
            Next

            If iCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + iCount.ToString + ") " + MessageTemplate.SuccesPost, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub
    Protected Sub lnkApproved_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"MRNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, 2, False)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me)
        PopulateGrid()
    End Sub
    Protected Sub lnkDisapproved_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"MRNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, 3, False)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me)
        PopulateGrid()
    End Sub
    Private Sub ApproveTransaction(tId As Integer, approvalStatNo As Integer, isSubmitforApp As Boolean)
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("EMR_WebApproved", UserNo, tId, approvalStatNo, isSubmitforApp)
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
    Protected Sub btnAttach_Click(sender As Object, e As EventArgs)

    End Sub


#End Region

#Region "Sync API"

    Protected Async Sub lnkSnycAPI_Click(sender As Object, e As EventArgs)
        Try
            Dim client = Await GetClientAsync()
            Dim operation As String = "Push"

            Dim processedCount = 0

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("GetTableSync", "EMR", operation)
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
            .password = ConfigurationManager.AppSettings("API:Password")
            })

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
