Imports clsLib

Partial Class Secured_frmHelp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Permission.IsAuthenticatedCoreUser()
    End Sub
End Class
