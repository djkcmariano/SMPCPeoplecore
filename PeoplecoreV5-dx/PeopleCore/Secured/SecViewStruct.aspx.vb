Imports System.Data
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports System.Data.SqlClient


Partial Class Secured_SecViewStruct
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim xSQLHelper As New clsBase.SQLHelper

    Protected Sub lnkView_Click(sender As Object, e As EventArgs)
        lblContent.Text = ""
        Try
            Dim ds As New DataSet
            'ds = SQLHelper.ExecuteDataSet_WOCatch("EExecuteQuery", txtObject.Text)
            ds = xSQLHelper.ExecuteDataSet_WOCatch(SQLHelper.ConSTR, "EExecuteQuery", txtObject.Text)
            'SQLHelper.ExecuteDataSet_WOCatch("EExecuteQuery", txtObject.Text)


            If cboSP.SelectedValue = 0 Then
                For Each dt As DataTable In ds.Tables
                    lblContent.Text = lblContent.Text & DrawTable(dt)
                Next
            Else
                For Each dt As DataTable In ds.Tables
                    lblContent.Text = lblContent.Text & CreateText(dt)

                Next
            End If
            If ds.Tables.Count = 0 Then
                lblContent.Text = "Command(s) completed successfully"
            End If
            'Catch ex As SqlException
            'lblContent.Text = "Error ({0}): {1}" & " " & ex.Number & " " & ex.Message
        Catch ex As Exception
            lblContent.Text = ex.Message
        End Try
    End Sub

    Private Function DataTableToString(dt As DataTable) As String
        Dim res As String = String.Join(Environment.NewLine, dt.Rows.OfType(Of DataRow)().[Select](Function(x) String.Join(" ; ", x.ItemArray)))
        Return res
    End Function

    Private Function CreateText(dt As DataTable) As String
        Dim str As String
        str = String.Join(Environment.NewLine, dt.Rows.OfType(Of DataRow)().[Select](Function(x) String.Join(" ; ", x.ItemArray)))
        str = str.Replace(vbCrLf, "<br/>")
        str = str.Replace(vbTab, "&#9;")
        str = str + "<br /><br />"
        Return str
    End Function

    Private Function DrawTable(dt As DataTable) As String
        Dim html As New StringBuilder()
        html.Append("<table border = '1'>")
        html.Append("<tr>")
        For Each column As DataColumn In dt.Columns
            html.Append("<th>")
            html.Append(column.ColumnName)
            html.Append("</th>")
        Next
        html.Append("</tr>")
        For Each row As DataRow In dt.Rows
            html.Append("<tr>")
            For Each column As DataColumn In dt.Columns
                html.Append("<td>")
                html.Append(row(column.ColumnName))
                html.Append("</td>")
            Next
            html.Append("</tr>")
        Next
        html.Append("</table><br />")
        Return html.ToString
    End Function

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        'AccessRights.CheckUser(UserNo)
        If UserNo <> -99 Then
            Context.Response.Redirect("~/secured/page.aspx?i=2")
        End If
    End Sub
End Class


