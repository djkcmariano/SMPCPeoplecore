Imports System.Data
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports clsLib

Partial Class Secured_SelfDTRSummaryReportsAppr
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim ReportNo As Integer
    Dim rptform As String
    Dim Datasource As String = ""
    Dim reporttitle As String = ""
    Dim clsGeneric As New clsGenericClass
    Dim PayLocNo As Integer = 0

    Dim sqlHelp As clsBase.SQLHelper

    'Private Sub populateDropdown()
    '    Generic.PopulateDropDownList(xPublicVar.xOnlineUseNo, Me, "pnlPopup", PayLocNo)

    '    Try
    '        cboTransNo.DataSource = SQLHelper.ExecuteDataSet("EReportType_WebLookupDMCI", xPublicVar.xOnlineUseNo, PayLocNo)
    '        cboTransNo.DataTextField = "tDesc"
    '        cboTransNo.DataValueField = "tno"
    '        cboTransNo.DataBind()

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xPublicVar.xOnlineUseNo = Generic.ToInt(Session("onlineuserno"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        ReportNo = Generic.ToInt(Request.QueryString("ReportNo"))
        reporttitle = Generic.ToStr(Request.QueryString("reporttitle"))
        rptform = Generic.ToStr(Request.QueryString("reportname"))
        Datasource = Generic.ToStr(Request.QueryString("Datasource"))
        Permission.IsAuthenticated()

        'If Not IsPostBack Then
        '    populateDropdown()
        'End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateServerReport(Generic.ToInt(cboReportTypeNo.SelectedValue))
    End Sub

    Private Sub PopulateServerReport(ByVal ReportTypeNo As Integer)

        Dim ds As New DataSet
        Dim dsrpt As New ReportDataSource
        Dim rpt As ServerReport = rviewer.ServerReport
        Me.rviewer.CssClass = "panel panel-default"
        Me.rviewer.ProcessingMode = ProcessingMode.Local
        Me.rviewer.Reset()


        If ReportTypeNo = 1 Then
            Datasource = "EDTR_RptDMCIAttendanceSummary"
            rptform = "DTR_RptDMCINightDiffSummary.rdlc"
        ElseIf ReportTypeNo = 2 Then
            Datasource = "EDTR_RptDMCIAttendanceSummary"
            rptform = "DTR_RptDMCIOvertimeSummary.rdlc"
        ElseIf ReportTypeNo = 3 Then
            Datasource = "EDTR_RptDMCIAttendanceSummary"
            rptform = "DTR_RptLeaveAbsenceSummary.rdlc"
        ElseIf ReportTypeNo = 4 Then
            Datasource = "EDTR_RptDMCIAttendanceSummary"
            rptform = "DTR_RptTardinessSummary.rdlc"
        ElseIf ReportTypeNo = 5 Then
            Datasource = "EDTR_RptDMCIAttendanceSummary"
            rptform = "DTR_RptUndertimeSummary.rdlc"

        End If

        Dim parm(4) As Object
        parm(0) = xPublicVar.xOnlineUseNo
        parm(1) = PayLocNo
        parm(2) = Generic.ToInt(cboReportTypeNo.SelectedValue)
        parm(3) = Generic.ToStr(txtStartDate.Text)
        parm(4) = Generic.ToStr(txtEndDate.Text)
        'parm(5) = 1

        Dim rptPath As String = ""
        Dim dsSource As DataSet

        dsSource = SQLHelper.ExecuteDataSet(Datasource, parm)
        If dsSource.Tables.Count > 0 Then
            If dsSource.Tables(0).Rows.Count > 0 Then
                dsrpt.Name = "ds" ' rptform & "_Data"
                Dim mpath As String = Server.MapPath("../Reports/")

                rptPath = mpath & rptform '& ".rdlc"
                dsrpt.Value = dsSource.Tables(0)

                rviewer.LocalReport.DisplayName = dsSource.Tables(0).TableName
                rviewer.LocalReport.ReportPath = rptPath
                rviewer.LocalReport.DataSources.Clear()
                rviewer.LocalReport.DataSources.Add(dsrpt)
                rviewer.LocalReport.DisplayName = dsrpt.Name
                rviewer.ShowPrintButton = True
                'AddHandler rviewer.LocalReport.SubreportProcessing, AddressOf SetSubDataSource
                rviewer.LocalReport.Refresh()
            Else
                MessageBox.Warning("No record found to preview.", Me)
            End If
        Else
            MessageBox.Warning("No record found to preview.", Me)
        End If
    End Sub

    'Protected Sub lnkCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    AppendAlphaDisk(Generic.ToInt(cboReportTypeNo.SelectedValue))
    'End Sub

    'Private Function AppendAlphaDisk(ByVal ReportTypeNo As Integer) As Boolean
    '    Try

    '        Dim storedproc As String = ""
    '        Dim FileHolder As FileInfo
    '        Dim WriteFile As StreamWriter, xfilename As String = "", ertinno As String = "", ffilename As String = ""
    '        Dim tpath As String = Page.MapPath("Disk")

    '        If Not IO.Directory.Exists(tpath) Then
    '            IO.Directory.CreateDirectory(tpath)
    '        End If

    '        If ReportTypeNo = 1 Then
    '            storedproc = "EDTR_RptDMCIAttendanceSummary"
    '        ElseIf ReportTypeNo = 2 Then
    '            storedproc = "EDTR_RptDMCIAttendanceSummary"
    '        ElseIf ReportTypeNo = 3 Then
    '            storedproc = "EDTR_RptDMCIAttendanceSummary"
    '        ElseIf ReportTypeNo = 4 Then
    '            storedproc = "EDTR_RptDMCIAttendanceSummary"
    '        ElseIf ReportTypeNo = 5 Then
    '            storedproc = "EDTR_RptDMCIAttendanceSummary"
    '        End If

    '        Dim parm(5) As Object
    '        parm(0) = xPublicVar.xOnlineUseNo
    '        parm(1) = PayLocNo
    '        'parm(2) = Generic.ToInt(Me.cboFilterbyNo.SelectedValue)
    '        'parm(3) = Generic.ToInt(hiffilterbyid.Value)
    '        'parm(4) = Generic.ToInt(cboTransNo.SelectedValue)
    '        parm(5) = 2

    '        Dim ds As DataSet
    '        ds = SQLHelper.ExecuteDataSet(storedproc, parm)
    '        If ds.Tables.Count > 0 Then
    '            If ds.Tables(1).Rows.Count > 0 Then
    '                ertinno = Generic.ToStr(ds.Tables(1).Rows(0)("title"))

    '                If ReportTypeNo = 1 Then
    '                    xfilename = tpath & "\" & ertinno & "_s71.txt"
    '                    ffilename = ertinno & "_s71.txt"
    '                ElseIf ReportTypeNo = 2 Then
    '                    xfilename = tpath & "\" & ertinno & "_s73.txt"
    '                    ffilename = ertinno & "_s73.txt"
    '                ElseIf ReportTypeNo = 3 Then
    '                    xfilename = tpath & "\" & ertinno & "_s74.txt"
    '                    ffilename = ertinno & "_s74.txt"
    '                ElseIf ReportTypeNo = 4 Then
    '                    xfilename = tpath & "\" & ertinno & "_s75.txt"
    '                    ffilename = ertinno & "_s75.txt"
    '                End If

    '                FileHolder = New FileInfo(xfilename)
    '                WriteFile = FileHolder.CreateText()

    '            End If

    '            If ds.Tables(0).Rows.Count > 0 Then
    '                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
    '                    WriteFile.WriteLine(Generic.ToStr(ds.Tables(0).Rows(i)("details")))
    '                Next
    '            End If
    '        End If

    '        WriteFile.Close()
    '        'OpenTExt("Disk/" & ffilename)
    '        Dim fullpath As String = tpath & "\" & ffilename
    '        DownloadFile(fullpath)

    '        Return True
    '    Catch
    '        Return False
    '    End Try

    'End Function

    Private Sub DownloadFile(ByVal fullpath As String)

        Dim FileName As String = ""
        FileName = IO.Path.GetFileName(fullpath)
        Response.Clear()
        Response.ClearContent()
        Response.ContentType = "application/octet-stream"
        Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
        Response.TransmitFile(fullpath)
        Response.End()

    End Sub

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As Button = TryCast(sender, Button)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Private Sub OpenTExt(ByVal tpath As String)
        Dim sb As New StringBuilder

        sb.Append("<script>")
        sb.Append("window.open('" & tpath & "' ,'_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes,with=800,height=550');")
        sb.Append("</scri")
        sb.Append("pt>")
        ClientScript.RegisterClientScriptBlock(Me.GetType, "test", sb.ToString())
    End Sub


    'Protected Sub cboFilterbyNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim fId As Integer
    '    fId = Generic.ToInt(cboFilterbyNo.SelectedValue)
    '    txtName.Text = ""
    '    If fId > 0 Then
    '        txtName.Enabled = True
    '        drpAC.CompletionSetCount = fId
    '    Else
    '        txtName.Enabled = False
    '        drpAC.CompletionSetCount = 0
    '    End If

    'End Sub






    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function populateDataDropdown(prefixText As String, count As Integer, contextKey As String) As List(Of String)
        Dim items As New List(Of String)()
        Dim _ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim clsbase As New clsBase.clsBaseLibrary
        Dim UserNo As Integer = 0, PayLocNo As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        PayLocNo = (HttpContext.Current.Session("xPayLocNo"))

        _ds = SQLHelper.ExecuteDataSet("EFilterBy_WebLookup_AC", UserNo, prefixText, PayLocNo, count)
        For Each row As DataRow In _ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("tDesc")),
                                Generic.ToStr(row("tNo")))
            items.Add(item)
        Next
        _ds.Dispose()
        Return items


    End Function

End Class
