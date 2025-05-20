Imports clsLib

Partial Class Secured_PageError
    Inherits System.Web.UI.Page
    Dim ErrNum As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            ErrNum = Generic.ToInt(Request.QueryString("i"))
            If Not IsPostBack Then
                If Not IsNothing(Request.UrlReferrer.ToString) Then
                    ViewState("PrevURL") = Generic.ToStr(Request.UrlReferrer.ToString)
                Else
                    ViewState("PrevURL") = "~/default.aspx"
                End If
            End If
            Select Case ErrNum
                Case 1
                    Me.txtWarningMessage.Text = "<strong>Oops, the page you're looking for does not exist</strong>"
                Case Else
                    Me.txtWarningMessage.Text = "<strong>A problem has occurred on this web site. </strong><br />Please try again. If this error continues, please contact support."
            End Select
        Catch ex As Exception
            Me.txtWarningMessage.Text = "<strong>A problem has occurred on this web site. </strong><br />Please try again. If this error continues, please contact support."
            'ViewState("PrevURL") = "~/default.aspx"
        End Try
        
        'Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkBack_Click(sender As Object, e As EventArgs)
        If Generic.ToStr(ViewState("PrevURL")) <> "" And Generic.GetPath(Generic.ToStr(ViewState("PrevURL").ToString().ToLower)) <> "pageerror.aspx" Then
            Response.Redirect(Generic.ToStr(ViewState("PrevURL")))
        Else
            Session.Abandon()
            Response.Redirect("~/default.aspx")
        End If

    End Sub
End Class
