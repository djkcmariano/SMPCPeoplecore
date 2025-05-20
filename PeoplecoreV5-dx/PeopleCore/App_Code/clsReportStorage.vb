Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports clsLib
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Configuration
Imports System.Data.OleDb
Imports DevExpress.Web
Imports System.Net
Public Class clsReportStorage

    Private reportsTable As DataTable
    Dim userNo As Integer = 0
    Dim paylocNo As Integer = 0

    Public Sub WriteReport(ByVal reportModel As clsReportModel)
       
        If SaveReport(reportModel) Then
        Else
        End If

    End Sub
    Private Function SaveReport(ByVal reportModel As clsReportModel) As Boolean
        Dim context As HttpContext = HttpContext.Current
        userNo = Generic.ToInt(context.Session("OnlineUserNo"))
        paylocNo = Generic.ToInt(context.Session("xpaylocNo"))

        If SQLHelper.ExecuteNonQuery("EReportEU_WebSave", userNo, reportModel.ReportId, reportModel.ReportTitle, Generic.ToStr(context.Session("xMenuType")), reportModel.LayoutData, paylocNo) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function GetReport(ByVal reportId As String) As clsReportModel
        Dim row As DataRow = reportsTable.Rows.Find(reportId)

        If row IsNot Nothing Then
            Dim model As New clsReportModel()
            model.ReportId = DirectCast(row("ReportID"), String)
            model.LayoutData = DirectCast(row("LayoutData"), Byte())
            Return model
        Else
            Return Nothing
        End If
    End Function


    Public Sub UpdateReport(ByVal reportModel As clsReportModel)
        If SaveReport(reportModel) Then
        Else
        End If
    End Sub


    Public Sub RemoveReport(ByVal reportId As String)
        Dim row As DataRow = reportsTable.Rows.Find(reportId)

        If row IsNot Nothing Then
            row.Delete()
            
        End If
    End Sub
End Class
