Imports clsLib
Imports System.Data
Partial Class SecuredApp_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Permission.IsAuthenticatedApplicant()
    End Sub
End Class
