Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports clsBase.SQLHelper
Imports clsLib
Imports AjaxControlToolkit

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WebService
    Inherits System.Web.Services.WebService


    <WebMethod(True)> _
    Public Function PopulateEmployee(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()

        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AC", UserNo, prefixText, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")) & _
                                    "|" & Generic.ToStr(row("DepartmentNo")) & _
                                    "|" & Generic.ToStr(row("RankNo")) & _
                                    "|" & Generic.ToStr(row("GroupNo")) & _
                                    "|" & Generic.ToStr(row("SectionNo")) & _
                                    "|" & Generic.ToStr(row("UnitNo")) & _
                                    "|" & Generic.ToStr(row("PositionNo")) & _
                                    "|" & Generic.ToStr(row("BirthAge")) & _
                                    "|" & Generic.ToStr(row("GenderNo")) & _
                                    "|" & Generic.ToStr(row("FacilityNo")) & _
                                    "|" & Generic.ToBol(row("IsCrew")) & _
                                    "|" & Generic.ToStr(row("HiredDate")) & _
                                    "|" & Generic.ToStr(row("tCode")) & _
                                    "|" & Generic.ToStr(row("ImmediateSuperiorNo")) & _
                                    "|" & Generic.ToStr(row("ImmediateSuperiorDesc")) & _
                                    "|" & Generic.ToStr(row("PresentAddress")) & _
                                    "|" & Generic.ToStr(row("HomeAddress")) & _
                                    "|" & Generic.ToStr(row("EmployeeStatNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

    <WebMethod(True)> _
    Public Function PopulateManager(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()

        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AC_Manager_All", UserNo, prefixText, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function


    <WebMethod(True)>
    Public Function PopulateManagerAll(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()

        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AC_Manager_All", UserNo, prefixText, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

    <WebMethod(True)>
    Public Function PopulateEmployee_DTRApplication(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()

        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_DTR_Application", UserNo, prefixText, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

    <WebMethod(True)>
    Public Function PopulateEmployee_Universal(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()

        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_Universal", UserNo, prefixText, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

    <WebMethod(True)> _
    Public Function PopulateEmployee_Encoder(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()

        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AC", UserNo, prefixText, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

    <WebMethod(True)> _
    Public Function PopulateReferential(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()

        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))
        ds = SQLHelper.ExecuteDataSet("EReferential_WebLookup_AC", UserNo, prefixText, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function
    <WebMethod(True)> _
    Public Function PopulateEmployee_AlternateAppr(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()

        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AC_AlternateAppr", UserNo, payLocno, prefixText)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

    <WebMethod(True)> _
    Public Function PopulateEmployee_SubordinateAppr(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()

        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AC_SubordinateAppr", UserNo, payLocno, prefixText)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

    <WebMethod(True)> _
    Public Function PopulateEmployee_SubordinateAppr_Crew(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()

        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AC_SubordinateAppr", UserNo, payLocno, prefixText)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")) & _
                                    "|" & Generic.ToBol(row("IsCrew")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function
    <WebMethod(True)> _
    Public Function PopulateEmployee_SubordinateSelf(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()

        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AC_SubordinateSelf", UserNo, payLocno, prefixText)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

    <WebMethod(True)> _
    Public Function populateUser(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()
        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))

        ds = SQLHelper.ExecuteDataSet("EUser_WebLookup_AC", UserNo, prefixText, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

    <WebMethod(True)> _
    Public Function populateUserManager(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()
        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))

        ds = SQLHelper.ExecuteDataSet("EUser_WebLookup_AC_Manager", UserNo, prefixText, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

    <WebMethod(True)> _
    Public Function PopulateTrnTitle(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim UserNo As Integer = 0
        UserNo = HttpContext.Current.Session("onlineuserno")
        ds = SQLHelper.ExecuteDataSet("ETrnTitle_WebLookup_AutoComplete", UserNo, prefixText, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("tDesc")), Generic.ToStr(row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

    <WebMethod(True)> _
    Public Function GetFilterBy(knownCategoryValues As String) As List(Of CascadingDropDownNameValue)
        Dim ds As New DataTable

        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))
        ds = SQLHelper.ExecuteDataTable("EFilteredBy_WebLookup", UserNo, payLocno)
        Dim values As New List(Of CascadingDropDownNameValue)()
        For Each row As DataRow In ds.Rows

            values.Add(New CascadingDropDownNameValue() With {.name = Generic.ToStr(row("tdesc")), .value = Generic.ToStr(row("tno"))})

        Next
        Return values
    End Function


    <WebMethod(True)> _
    Public Function GetFilterValue(knownCategoryValues As String) As List(Of CascadingDropDownNameValue)
        Dim fid As String = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)("tno")
        Dim ds As New DataTable

        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))
        ds = SQLHelper.ExecuteDataTable("EFilteredValue_WebLookup", UserNo, fid, payLocno)
        Dim values As New List(Of CascadingDropDownNameValue)()
        For Each row As DataRow In ds.Rows

            values.Add(New CascadingDropDownNameValue() With {.name = Generic.ToStr(row("tdesc")), .value = Generic.ToStr(row("tno"))})

        Next
        Return values
    End Function

    <WebMethod(True)> _
    Public Function ApplicantType(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim items As New List(Of String)()
        Dim Type As Integer = Generic.ToInt(Generic.Split(contextKey, 0))
        Dim MRNo As Integer = Generic.ToInt(Generic.Split(contextKey, 1))
        Dim UserNo As Integer = Generic.ToInt(HttpContext.Current.Session("onlineuserno"))
        Dim PayLocNo As Integer = Generic.ToInt(HttpContext.Current.Session("xPayLocNo"))
        Dim dt As DataTable
        If Type = 0 Then
            dt = SQLHelper.ExecuteDataTable("EMRShortlist_Employee", UserNo, PayLocNo, MRNo, prefixText)
        Else
            dt = SQLHelper.ExecuteDataTable("EMRShortlist_Applicant", UserNo, PayLocNo, MRNo, prefixText)
        End If
        For Each row As DataRow In dt.Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("tDesc")), Generic.ToStr(row("tNo")))
            items.Add(item)
        Next
        Return items
    End Function

    <WebMethod(True)> _
    Public Function PopulatePlantilla(ByVal prefixText As String, ByVal count As Integer) As List(Of String)
        Dim items As New List(Of String)()        
        Dim UserNo As Integer = Generic.ToInt(HttpContext.Current.Session("onlineuserno"))
        Dim PayLocNo As Integer = Generic.ToInt(HttpContext.Current.Session("xPayLocNo"))
        Dim dt As DataTable        
        dt = SQLHelper.ExecuteDataTable("EPlantilla_AutoComplete", prefixText, count)
        For Each row As DataRow In dt.Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("PlantillaCode")), Generic.ToStr(row("PlantillaNo")))
            items.Add(item)
        Next
        Return items
    End Function

    <WebMethod(True)> _
    Public Function PopulatePayclass(knownCategoryValues As String) As List(Of String)
        Dim fid As New List(Of String)()
        Dim UserNo As Integer = Generic.ToInt(HttpContext.Current.Session("onlineuserno"))
        Dim PayLocNo As Integer = Generic.ToInt(HttpContext.Current.Session("xPayLocNo"))
        Dim dt As DataTable

       
        dt = SQLHelper.ExecuteDataTable("EPayClass_WebLookupAutoComplete", UserNo, fid, PayLocNo)
        Dim values As New List(Of CascadingDropDownNameValue)()
        For Each row As DataRow In dt.Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("tDesc")), Generic.ToStr(row("tCode")))
            fid.Add(item)

        Next
        Return fid
    End Function

End Class