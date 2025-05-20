Imports clsLib
Imports System.Data
Imports System.IO
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Parameters

Partial Class Secured_RptTemplateViewerDX
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim ReportNo As Integer = 0
    Dim StrParam As String = ""
    Dim PayLocNo As Integer = 0    

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        ReportNo = Generic.ToInt(Request.QueryString("ReportNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        StrParam = "int|" & UserNo & "~" & "int|" & PayLocNo & "~" & Generic.ToStr(Request.QueryString("param"))
        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            Me.txtIsView.Checked = True
        End If

        If Generic.ToBol(Me.txtIsView.Checked) = True Then
            PopulateReportQS()
        End If


    End Sub


    Private Sub PopulateReportQS()
        Try
            Dim report As New XtraReport
            Dim report_name As String = "", report_ds As String = "", ds As New DataSet, dt As DataTable, folder As String

            'report(Information)
            dt = SQLHelper.ExecuteDataTable("SELECT TOP 1 * FROM EReport WHERE ReportNo=" & ReportNo.ToString())
            For Each row As DataRow In dt.Rows
                ASPxDocumentViewer1.Report = Nothing
                report_name = row("AccessReportName").ToString()
                report_ds = row("Datasource").ToString()
                folder = Server.MapPath("../ReportsDX/")
                If File.Exists(folder & report_name) Then
                    report.LoadLayout(folder & report_name)
                Else
                    Exit Sub
                End If
            Next

            'Retrieve param in query string            
            report.RequestParameters = False
            Dim arrParam As String() = StrParam.Split("~"), i As Integer = 0
            Dim obj_len = arrParam.Length - 1
            Dim obj(obj_len) As Object                        
            For Each param As String In arrParam
                Dim type As String = Generic.Split(param, 0)
                Dim value As String = Generic.Split(param, 1)
                Select Case type
                    Case "str"
                        obj(i) = Generic.ToStr(value)
                    Case "int"
                        obj(i) = Generic.ToInt(value)
                    Case "dec"
                        obj(i) = Generic.ToDec(value)
                    Case "bol"
                        obj(i) = Generic.ToBol(value)
                End Select                
                report.Parameters(i).Value = obj(i)
                report.Parameters(i).Visible = False
                i = i + 1
            Next
                'ds = SQLHelper.ExecuteDataSet(report_ds, obj)
                'ds = SQLHelper.ExecuteDataSet(report_ds, -99, 0, 1)

                'Dim report As New XtraReport
                'report.RequestParameters = False
                'report.LoadLayout(Server.MapPath("../ReportsDX/") & "EMRHiredMass_RptJobOffer.repx")
                'report.Parameters(0).Value = -99
                'report.Parameters(1).Value = 1
                'report.Parameters(2).Value = -3

                ASPxDocumentViewer1.Report = report
                report.CreateDocument()
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub callbackPanel_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase)
    '    Me.txtIsView.Checked = True
    'End Sub

    Protected Sub ASPxDocumentViewer1_Unload(sender As Object, e As System.EventArgs) Handles ASPxDocumentViewer1.Unload
        ASPxDocumentViewer1.Report = Nothing
    End Sub

    Protected Sub callbackPanel_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase)
        Me.txtIsView.Checked = True
    End Sub

End Class
