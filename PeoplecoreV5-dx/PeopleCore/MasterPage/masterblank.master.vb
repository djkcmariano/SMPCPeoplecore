Imports clsLib
Imports System.IO

Partial Class master_masterBlank
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'lTheme.Text = "<link rel='stylesheet' type='text/css' id='theme' href='css/" & Generic.GetSkin() & "' />"
        lTheme.Text = "<link rel='stylesheet' type='text/css' id='theme' href='css/theme-light.css' />"
        lTheme.Text = lTheme.Text & BannerColor()

        If Generic.ToInt(Session("OnlineUserNo")) <> 0 Then
            CheckSessionTimeout()
        End If
        If Not IsPostBack Then
            SQLHelper.ExecuteNonQuery("SUserStat_WebSave_Activity", Generic.ToInt(Session("OnlineUserNo")), Session("xFormname"), "Open Form", 1)
        End If
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Private Sub CheckSessionTimeout()
        Dim Timeout As Integer = Convert.ToInt32(Session.Timeout) * 60000
        Dim str_Script As String = " var Timeout; " &
                                   " var sessionTimeout = " & Timeout.ToString() & "; " &
                                   " clearTimeout(Timeout);" &
                                   " function RedirectToWelcomePage() { window.location = ""../PageExpired.aspx?i=3""; }" &
                                   " Timeout = setTimeout('RedirectToWelcomePage()', sessionTimeout);" & ""
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "CheckSessionOut", str_Script, True)
    End Sub

    Private Function BannerColor() As String
        Dim str As String = ""
        str = "<style type='text/css'>" & _
              ".x-navigation.x-navigation-horizontal { " & _
              " background: #" & Generic.GetSkin() & "; }" & _
              "</style>"
        Return str
    End Function

End Class

