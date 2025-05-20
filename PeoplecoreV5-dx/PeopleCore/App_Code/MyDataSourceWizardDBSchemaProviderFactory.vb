Imports Microsoft.VisualBasic
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraReports.Web.ReportDesigner.Services

Public Class MyDataSourceWizardDBSchemaProviderFactory
    Implements IDataSourceWizardDBSchemaProviderFactory

    Public Function Create() As DevExpress.DataAccess.Sql.IDBSchemaProvider Implements DevExpress.XtraReports.Web.ReportDesigner.Services.IDataSourceWizardDBSchemaProviderFactory.Create
        Return New MyDBSchemaProvider()
    End Function

End Class
