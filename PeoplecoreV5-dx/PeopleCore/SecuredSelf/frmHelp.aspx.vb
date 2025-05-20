Imports clsLib
Partial Class SecuredSelf_frmHelp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Permission.IsAuthenticated()
    End Sub
End Class
