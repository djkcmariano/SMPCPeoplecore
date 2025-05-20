<%@ Application Language="VB" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="clsLib" %>
<%@ Import Namespace="DevExpress.XtraReports.Web.ReportDesigner" %>

<script runat="server">
    'Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
       
        Dim IsShareDrive As String = ConfigurationManager.AppSettings("share_drive")
        Dim UNC As String = ConfigurationManager.AppSettings("unc")
        Dim DrivePath As String = ConfigurationManager.AppSettings("drive_path")
        Dim Username As String = ConfigurationManager.AppSettings("username")
        Dim Password As String = ConfigurationManager.AppSettings("password")
        If IsShareDrive.ToLower = "yes" Then
            Dim nd As Utilities.Network.NetworkDrive = New Utilities.Network.NetworkDrive()
            DrivePath = Replace(DrivePath, "\", "")
            nd.MapNetworkDrive(UNC, DrivePath, Username, Password)
        End If
        
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
        'Dim cInfo As New CultureInfo("en-US")
        'cInfo.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy"
        'cInfo.DateTimeFormat.DateSeparator = "/"
        'Thread.CurrentThread.CurrentCulture = cInfo
        'Thread.CurrentThread.CurrentUICulture = cInfo
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)                  
        'Dim TheError As Exception = Server.GetLastError()
        'Dim AppErrorLog As Boolean = Generic.ToBol(System.Configuration.ConfigurationManager.AppSettings("ErrorLogging").ToString().Contains("Yes"))
        'Server.ClearError()
        'Response.TrySkipIisCustomErrors = True
        'If TypeOf TheError Is HttpException AndAlso DirectCast(TheError, HttpException).GetHttpCode() = 404 Then
        '    Response.Redirect("~/PageError.aspx?i=1")
        'Else
        '    If AppErrorLog Then
        '        Try
        '            Dim url As String = Generic.ToStr(HttpContext.Current.Request.Url.OriginalString.ToString())
        '            clsLib.SQLHelper.ExecuteNonQuery("EApplicationErrorLog_WebSave", TheError.Message.ToString(), TheError.GetType().Name.ToString(), url, TheError.StackTrace.ToString())
        '        Catch ex As Exception

        '        End Try
        '    End If
        '    Response.Redirect("~/PageError.aspx")
        'End If
    End Sub
           
    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends        
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
      
    End Sub
    
</script>
