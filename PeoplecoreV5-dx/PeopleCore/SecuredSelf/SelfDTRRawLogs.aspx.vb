Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class SecuredSelf_SelfDTRRawLogs
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRRawLogs_WebSelf", UserNo, Filter1.SearchText, Generic.ToStr(fltxtStartDate.Text), Generic.ToStr(fltxtEndDate.Text), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticated()
        If Not IsPostBack Then
            'PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        AddHandler Filter1.lnkSearchClick, AddressOf lnkGo_Click

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If fltxtStartDate.Text > "" And fltxtEndDate.Text > "" Then
            PopulateGrid()
        Else
            MessageBox.Information("Date From and Date To are required in filtering.", Me)
        End If
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    'Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
    '    Try
    '        grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
    '    Catch ex As Exception
    '        MessageBox.Warning("Error exporting to excel file.", Me)
    '    End Try
    'End Sub

    Protected Sub lnkIn_Click(sender As Object, e As EventArgs)
        If IsWFH() Then
            saveRecord("0")
        Else
            MessageBox.Information("You are not authorize to In and Out, Please ask your immediate superior.", Me)
        End If

    End Sub
    Protected Sub lnkOut_Click(sender As Object, e As EventArgs)
        If IsWFH() Then
            saveRecord("1")
        Else
            MessageBox.Information("You are not authorize to In and Out, Please ask your immediate superior.", Me)
        End If


    End Sub
    Private Function IsWFH() As Boolean
        Dim ndate As Date = FormatDateTime(Now(), DateFormat.ShortDate)
        Dim RetVal As Boolean = False
        Dim ds As DataSet = SQLHelper.ExecuteDataSet("Select B.IsWFH As RetVal From dbo.EDTRDetiLogShift_Table(" & Session("EmployeeNo") & ",'" & ndate & "','" & ndate & "',0) a inner join (Select ShiftNo,IsWFH from dbo.Eshift Where Isnull(IsWFH,0)=1) B On A.ShiftNo=B.ShiftNo")
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                RetVal = Generic.ToBol(ds.Tables(0).Rows(0)("RetVal"))
            End If
        End If
        Return RetVal
    End Function

    Private Sub saveRecord(logtype As String)
        Dim RetVal As Boolean = False
        Dim ndate As Date = Now()

        If SQLHelper.ExecuteNonQuery("EFPDTR_WebSave_Manual", UserNo, 0, ndate, 0, logtype, PayLocNo) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

End Class





