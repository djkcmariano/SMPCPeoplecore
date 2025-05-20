Imports clsLib
Imports System.IO

Partial Class Secured_page_redirect
    Inherits System.Web.UI.Page

    Dim formName As String = ""
    Dim tableName As String = ""
    Dim menuType As String = ""
    Dim idx As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim context As HttpContext = HttpContext.Current
        Dim FileInfo As FileInfo = New FileInfo(context.Request.Url.AbsolutePath)
        Dim Folder As String = FileInfo.Directory.Name

        formName = Generic.ToStr(Request.QueryString("formName"))
        tableName = Generic.ToStr(Request.QueryString("tableName"))
        menuType = Generic.ToStr(Request.QueryString("menuType"))
        idx = Generic.ToInt(Request.QueryString("id"))

        Session("xMenuType") = menuType
        Session("xTableName") = tableName
        Session("xFormName") = formName
        If idx > 0 Then
            Response.Redirect(formName & "?id=" & idx)
        Else
            Response.Redirect(formName)
        End If

    End Sub
End Class
