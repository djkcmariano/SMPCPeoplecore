Imports System
Imports System.Web
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports clsLib

Partial Class Secured_RptList_EU_Designer
    Inherits System.Web.UI.Page

    Private Shared reportStorage As New clsReportStorage()
    Private task As clsReportDesignerTask.DesignerTask
    Private model As clsReportModel


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        task = DirectCast(Session("DesignerTask"), clsReportDesignerTask.DesignerTask)
        If task IsNot Nothing Then
            InitDesignerPage()
        ElseIf Not Page.IsCallback Then
            Response.Redirect("RptList_EU.aspx")
        End If
    End Sub

    Private Sub InitDesignerPage()
        Dim report As New DevExpress.XtraReports.UI.XtraReport
        Select Case task.mode
            Case clsReportDesignerTask.ReportEdditingMode.NewReport
                ' Create a new report from the template.
                model = New clsReportModel()
                model.ReportId = task.reportID
                model.ReportTitle = task.reportTitle
                'ASPxReportDesigner1.OpenReport(New ReportTemplate With {.Name = model.ReportId})
                ASPxReportDesigner1.OpenReport(report)
            Case clsReportDesignerTask.ReportEdditingMode.ModifyReport
                ' Load an existing report from the catalog database.
                model = DirectCast(Session("ReportModel"), clsReportModel) ' reportStorage.GetReport(task.reportID)
                ASPxReportDesigner1.OpenReportXmlLayout(model.LayoutData)
        End Select
    End Sub


    Protected Sub ASPxReportDesigner1_SaveReportLayout(ByVal sender As Object, ByVal e As DevExpress.XtraReports.Web.SaveReportLayoutEventArgs)
        ' If a report is new, write it to a new database record, othervise update the existing record.
        If task.mode = clsReportDesignerTask.ReportEdditingMode.NewReport Then
            model.LayoutData = e.ReportLayout
            reportStorage.WriteReport(model)
            task.mode = clsReportDesignerTask.ReportEdditingMode.ModifyReport
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            model.LayoutData = e.ReportLayout
            reportStorage.UpdateReport(model)
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub



    
End Class
