Imports Microsoft.VisualBasic
Imports System
Imports System.Web
Imports System.Web.Security
' <summary>
' SingleSessionPreparation is used to help ensure
' users may only have one session active
'</summary>

Public Class SingleSessionPreparation


    ' <summary>
    ' Called during LoggedIn event. Need to pass username
    ' as login process not fully completed
    ' </summary>
    Private Sub CreateAndStoreSessionToken(userName As String)

        ' Will be using the response object several times
        Dim pageResponse As HttpResponse = HttpContext.Current.Response

        ' 'session' token
        Dim sessionToken As Guid = System.Guid.NewGuid()

        ' Get authentication cookie and ticket
        Dim authenticationCookie As HttpCookie = pageResponse.Cookies(FormsAuthentication.FormsCookieName)
        Dim authenticationTicket As FormsAuthenticationTicket = FormsAuthentication.Decrypt(authenticationCookie.Value)

        ' Create a new ticket based on the existing one that includes the 'session' token in the userData
        Dim newAuthenticationTicket As FormsAuthenticationTicket =
            New FormsAuthenticationTicket(
            authenticationTicket.Version,
            authenticationTicket.Name,
            authenticationTicket.IssueDate,
            authenticationTicket.Expiration,
            authenticationTicket.IsPersistent,
            sessionToken.ToString(),
            authenticationTicket.CookiePath)

        ' Store session token in Membership comment
        ' You may want to store other information in the comment
        ' field, if so, you may have to implement some dilimited
        ' structure within it, perhaps xml
        Dim currentUser As MembershipUser = Membership.GetUser(userName)
        currentUser.Comment = sessionToken.ToString()
        Membership.UpdateUser(currentUser)

        ' Replace the authentication cookie
        pageResponse.Cookies.Remove(FormsAuthentication.FormsCookieName)

        Dim newAuthenticationCookie As HttpCookie = New HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(newAuthenticationTicket))

        newAuthenticationCookie.HttpOnly = authenticationCookie.HttpOnly
        newAuthenticationCookie.Path = authenticationCookie.Path
        newAuthenticationCookie.Secure = authenticationCookie.Secure
        newAuthenticationCookie.Domain = authenticationCookie.Domain
        newAuthenticationCookie.Expires = authenticationCookie.Expires
        pageResponse.Cookies.Add(newAuthenticationCookie)

    End Sub

End Class
