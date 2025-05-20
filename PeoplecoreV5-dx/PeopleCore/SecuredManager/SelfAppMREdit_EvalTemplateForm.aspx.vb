Imports System.Data
Imports clsLib

Partial Class SecuredManager_SelfAppMREdit_EvalTemplateForm
    Inherits System.Web.UI.Page
        Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            HeaderInfo1.xFormName = "EApplicant"
        End If

    End Sub



End Class
