
Imports clsLib
Imports System.Data
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Data.OleDb
Imports System.Math

Partial Class Secured_DownloadPage

    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DownloadFile("~/Secured/documents/EWB_12202022.txt")
    End Sub

    Private Sub DownloadFile(ByVal fullpath As String)

        Dim FileName As String = IO.Path.GetFileName(fullpath)
        FileName = IO.Path.GetFileName(fullpath)
        Dim filePath As String = Server.MapPath(String.Format("~/Secured/documents/{0}", FileName))
        Response.Clear()
        Response.ClearContent()
        Response.ContentType = "application/octet-stream"
        Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
        Response.TransmitFile(filePath)
        Response.End()
        Stop
    End Sub
End Class
