Imports clsLib
Imports System.IO
Partial Class Secured_page_redirect_tab
    Inherits System.Web.UI.Page
    Dim formName As String = ""
    Dim idx As Integer = 0


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim context As HttpContext = HttpContext.Current
        Dim FileInfo As FileInfo = New FileInfo(context.Request.Url.AbsolutePath)
        Dim Folder As String = FileInfo.Directory.Name
        Dim url As String = ""
        idx = Generic.ToInt(Request.QueryString("id"))
        url = Generic.GetFirstTab(idx)
        If URL <> "" Then
            Response.Redirect(URL)
        End If
    End Sub
End Class
