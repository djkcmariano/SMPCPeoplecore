Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
'Imports ApplicationBlock.SqlHelper

Public Class clsLookUp

    Dim clsGen As New clsGenericClass
    Dim onlineuserno As Integer = 0

    Public Function Lookup_Paysched() As DataSet
        Return clsGen.xLookup_Table(onlineuserno, "EpaySchedule")
    End Function

    Public Function Lookup_Month() As DataSet
        Return clsGen.xLookup_Table(onlineuserno, "EMonth")
    End Function

    Public Function Lookup_PayIncomeType() As DataSet
        Return clsGen.xLookup_Table(onlineuserno, "EPayIncomeType")
    End Function

    Public Function Lookup_PayDeductType() As DataSet
        Return clsGen.xLookup_Table(onlineuserno, "EPayDeductType")
    End Function

    Public Function Lookup_InterviewStat() As DataSet
        Return clsGen.xLookup_Table(onlineuserno, "EInterviewStatL")
    End Function

    Public Function Lookup_ActionStat() As DataSet
        Return clsGen.xLookup_Table(onlineuserno, "EActionStatL")
    End Function

    Public Function Lookup_JobOfferStat() As DataSet
        Return clsGen.xLookup_Table(onlineuserno, "EJobOfferStatL")
    End Function

    Public Function Lookup_RequiredStat() As DataSet
        Return clsGen.xLookup_Table(onlineuserno, "ERequiredStatL")
    End Function

#Region "*********** Performance Lookup *************"

    Public Function Lookup_ResponseType() As DataSet
        Return clsGen.xLookup_Table(onlineuserno, "EResponseTypeL")
    End Function

    Public Function Lookup_PERating() As DataSet
        Return clsGen.xLookup_Table(onlineuserno, "EPERatingL")
    End Function

    Public Function Lookup_ObjectiveType() As DataSet
        Return clsGen.xLookup_Table(onlineuserno, "EObjectiveTypeL")
    End Function

    Public Function Lookup_PECycle() As DataSet
        Return clsGen.xLookup_Table(onlineuserno, "EPECycleL")
    End Function

    Public Function Lookup_PEEvaluator() As DataSet
        Return clsGen.xLookup_Table(onlineuserno, "EPEEvaluatorL")
    End Function

#End Region

End Class
