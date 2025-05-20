Imports Microsoft.VisualBasic

Namespace clsReportDesignerTask
    Public Enum ReportEdditingMode
        NewReport
        ModifyReport
    End Enum

    Public Class DesignerTask
        Public Property mode() As ReportEdditingMode
        Public Property reportID() As Integer
        Public Property reportTitle() As String
    End Class

End Namespace
