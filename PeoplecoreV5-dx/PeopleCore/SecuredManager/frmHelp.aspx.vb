Imports clsLib
Partial Class SecuredManager_frmHelp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Permission.IsAuthenticatedSuperior()
    End Sub
End Class
