Imports System.Data
Imports clsLib
Imports System.IO

Partial Class Secured_TrnTakenModuleList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))

        Permission.IsAuthenticatedCoreUser()

        If Not IsPostBack Then
            HeaderInfo1.xFormName = "ETrnTakenDetl"
        End If

    End Sub

End Class
