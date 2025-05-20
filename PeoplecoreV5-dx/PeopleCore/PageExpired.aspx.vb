Imports clsLib
Imports System.Data

Partial Class PageExpired
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim pagestatus As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pagestatus = Generic.ToInt(Request.QueryString("i"))
        UserNo = Session("OnlineUserNo")
        Session("OnlineUserNo") = 0
        Session("xPayLocNo") = 0
        Session.Abandon()
        Session.Clear()
        Session.RemoveAll()
        Dim lnk As New LinkButton

        If pagestatus = 0 Then
            pagestatus = 4
        End If

        If pagestatus = 1 Then
            Me.txtWarningMessage.Text = "<strong>Page Disconnected:</strong> The page you requested was disconnected from the server. This page is no longer available. As a security precaution, web browser does not automatically resubmit your information for you."
            SQLHelper.ExecuteNonQuery("SUserStat_WebSave_Activity", UserNo, Session("xFormname"), "Page disconnected", 0)
            panelBack.Visible = True
        ElseIf pagestatus = 2 Then
            Me.txtWarningMessage.Text = "No access permission to view web page."
            SQLHelper.ExecuteNonQuery("SUserStat_WebSave_Activity", UserNo, Session("xFormname"), "No Access Permission", 1)
            panelBack.Visible = True
        ElseIf pagestatus = 3 Then
            Me.txtWarningMessage.Text = "<strong>Page has expired:</strong> The page you requested was created using information you submitted in a form. This page is no longer available. As a security precaution, web browser does not automatically resubmit your information for you."
            SQLHelper.ExecuteNonQuery("SUserStat_WebSave_Activity", UserNo, Session("xFormname"), "Page expired", 0)
            panelBack.Visible = True
        ElseIf pagestatus = 4 Then
            Me.txtWarningMessage.Text = "<strong>A problem has occurred on this web site. </strong><br />Please try again. If this error continues, please contact support."
            SQLHelper.ExecuteNonQuery("SUserStat_WebSave_Activity", UserNo, Session("xFormname"), "", 0)
            panelBack.Visible = True
        ElseIf pagestatus = 5 Then
            Me.txtWarningMessage.Text = "<strong>Oops, the page you're looking for does not exist</strong>"
            SQLHelper.ExecuteNonQuery("SUserStat_WebSave_Activity", UserNo, Session("xFormname"), "Page not found", 0)
            panelBack.Visible = True
        End If


            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkBack_Click(sender As Object, e As System.EventArgs)

        Response.Redirect("~/default.aspx?")

    End Sub
End Class
