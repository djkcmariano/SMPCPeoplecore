Imports Microsoft.VisualBasic
Imports DevExpress.XtraReports.Web.ReportDesigner.Services
Imports DevExpress.DataAccess.Native
Imports DevExpress.DataAccess.ConnectionParameters
Imports clsLib

Public Class MyDataSourceWizardConnectionStringsProvider
    Implements IDataSourceWizardConnectionStringsProvider

    Public Function GetConnectionDescriptions() As System.Collections.Generic.Dictionary(Of String, String) Implements DevExpress.XtraReports.Web.ReportDesigner.Services.IDataSourceWizardConnectionStringsProvider.GetConnectionDescriptions
        Dim connections As Dictionary(Of String, String) = AppConfigHelper.GetConnections().Keys.ToDictionary(Function(x) x, Function(x) x)

        ' Customize the loaded connections list. 
        connections.Remove("LocalSqlServer")
        connections.Remove("ApplicationServices")
        'connections.Remove("dsRpt")
        connections.Remove("constr")
        connections.Remove("PeopleCoreV5_Standard5ConnectionString")

        'connections.Add("CustomConnectionName", "End User Report Connection")
        Return connections
    End Function

    Public Function GetDataConnectionParameters(name As String) As DevExpress.DataAccess.ConnectionParameters.DataConnectionParametersBase Implements DevExpress.XtraReports.Web.ReportDesigner.Services.IDataSourceWizardConnectionStringsProvider.GetDataConnectionParameters
        ' Return custom connection parameters for the custom connection(s). 
        If name = "CustomConnectionName" Then
            Return New MsSqlConnectionParameters(SQLHelper.fservername, SQLHelper.fdatabasename, SQLHelper.fsqllogin, SQLHelper.fsqlpass, MsSqlAuthorizationType.SqlServer)
        End If
        Return AppConfigHelper.LoadConnectionParameters(name)
    End Function

End Class
