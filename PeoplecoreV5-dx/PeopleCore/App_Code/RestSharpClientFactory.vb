Imports Newtonsoft.Json
Imports RestSharp
Imports System.Net

Public Class RestSharpClientFactory
    Private _cachedToken As String = String.Empty
    Private ReadOnly _restClient As RestClient

    Public Sub New()
        Dim baseUrl = ConfigurationManager.AppSettings("API:BaseURL")
        _restClient = New RestClient(baseUrl)
    End Sub

    Public Function GetClient() As RestClient
        Dim token As String = GetToken()
        _restClient.AddDefaultHeader("Authorization", "Bearer " & token)
        Return _restClient
    End Function

    Private Function GetToken() As String
        If Not String.IsNullOrEmpty(_cachedToken) Then
            Return _cachedToken
        End If

        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim client As New RestClient(ConfigurationManager.AppSettings("API:BaseURL"))
            Dim request = New RestRequest("api/auth", Method.Post)

            request.AddJsonBody(New With {
                .username = ConfigurationManager.AppSettings("API:Username"),
                .password = ConfigurationManager.AppSettings("API:Password")
            })

            Dim response As RestResponse = client.Execute(request)

            If response.StatusCode = HttpStatusCode.OK AndAlso Not String.IsNullOrEmpty(response.Content) Then
                Dim tokenData = JsonConvert.DeserializeObject(Of String)(response.Content)
                If tokenData IsNot Nothing Then
                    _cachedToken = tokenData
                    Return _cachedToken
                End If
            End If

            Throw New Exception("Failed to obtain JWT token: " & response.StatusCode.ToString())
        Catch ex As Exception
            Throw New Exception("Failed to obtain JWT token", ex)
        End Try
    End Function
End Class

Public Class APIStatus
    Public Property Id As Integer
    Public Property Operation As String = String.Empty
    Public Property errorMessage As String = String.Empty
End Class
