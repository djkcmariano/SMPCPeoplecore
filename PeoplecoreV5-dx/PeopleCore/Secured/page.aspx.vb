Imports clsLib

Partial Class Secured_page
    Inherits System.Web.UI.Page
    Dim i As Integer = 0
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        i = Generic.ToInt(Request.QueryString("i"))
        If i = 3 Then
            lblMessage.Text = "Webpage under construction."
        End If
        lblMessage.Text = MessageTemplate.DeniedView


    End Sub
End Class
