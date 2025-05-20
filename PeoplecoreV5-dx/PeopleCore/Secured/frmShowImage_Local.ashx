<%@ WebHandler Language="VB" Class="frmShowImage_Local" %>

Imports System
Imports System.Configuration
Imports System.Web
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports clsLib

Public Class frmShowImage_Local : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
       
        Try
            Dim imgPath As String = ""
            imgPath = Convert.ToString(context.Request.QueryString("imgpath"))
              
            context.Response.ContentType = "image/jpeg"
            context.Response.WriteFile(imgPath)
        Catch ex As Exception

        End Try
       
       
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class