Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Web.Security

Public Class SingleSessionEnforcement : Implements IHttpModule

    Private Sub OnPostAuthenticate(sender As Object, e As EventArgs)

        Dim sessionToken As Guid

        Dim httpApplication As HttpApplication = New HttpApplication
        httpApplication = sender
        Dim httpContext As HttpContext = httpApplication.Context

        ' Check user's session token
        If (httpContext.User.Identity.IsAuthenticated) Then

            Dim authenticationTicket As FormsAuthenticationTicket = New FormsIdentity(httpContext.User.Identity).Ticket

            If (authenticationTicket.UserData <> "") Then

                sessionToken = New Guid(authenticationTicket.UserData)

            Else

                ' No authentication ticket found so logout this user
                ' Should never hit this code
                FormsAuthentication.SignOut()
                FormsAuthentication.RedirectToLoginPage()
                Return
            End If

            Dim currentUser As MembershipUser = Membership.GetUser(authenticationTicket.Name)

            ' May want to add a conditional here so we only check
            ' if the user needs to be checked. For instance, your business
            ' rules for the application may state that users in the Admin
            ' role are allowed to have multiple sessions
            Dim storedToken As Guid = New Guid(currentUser.Comment)

            If (sessionToken <> storedToken) Then

                ' Stored session does not match one in authentication
                ' ticket so logout the user
                FormsAuthentication.SignOut()
                FormsAuthentication.RedirectToLoginPage()

            End If
        End If
    End Sub
    Public Sub Dispose() Implements System.Web.IHttpModule.Dispose
        ' Nothing to dispose            
    End Sub
    Public Sub Init(context As HttpApplication) Implements System.Web.IHttpModule.Init
        AddHandler context.BeginRequest, AddressOf OnPostAuthenticate
    End Sub
    'Public Sub Init(context As HttpApplication) Implements System.Web.IHttpModule.Init
    '    context.PostAuthenticateRequest = New EventHandler(OnPostAuthenticate)
    'End Sub


End Class
