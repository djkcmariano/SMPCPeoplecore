Imports Microsoft.VisualBasic
Imports clsLib

Public Class clsFormRedirect

    Public Sub openForm(pageRedirect As String, Optional title As String = "", Optional index As Integer = 0)
        Dim session As HttpSessionState = HttpContext.Current.Session
        Dim page As String = ""
        page = Generic.Split(pageRedirect, 0)
        session("xFormname") = Generic.Split(pageRedirect, 1)
        session("xTablename") = Generic.Split(pageRedirect, 2)
        HttpContext.Current.Response.Redirect(page)

    End Sub

End Class
