Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO

Partial Class Secured_PayMainList_OD
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        PayHeader.ID = Generic.ToInt(TransNo)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayMain_Web_Deduct", UserNo, TransNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateDropDownList()

        Try

            Dim _dt As DataSet
            _dt = SQLHelper.ExecuteDataSet("Select * FROM ESourceDeductType")

            cboTabNo.DataSource = _dt
            cboTabNo.DataTextField = "SourceDeductTypeDesc"
            cboTabNo.DataValueField = "SourceDeductTypeNo"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        'AccessRights.CheckUser(UserNo)
        Permission.IsAuthenticatedCoreUser()

        If Not IsPostBack Then
            PopulateDropDownList()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub


#Region "********Main*******"



    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkExportCL_Click(sender As Object, e As EventArgs)
        Try
            Generate_CoopLoan()
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Private Sub Generate_CoopLoan()

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim tpath As String = Page.MapPath("Disk") '"c:\Payroll Diskette"
        Dim xfilename As String '= path & "\" & "SSSLoan-" & Format(Now, "MMMMdd") & ".TXT"
 
        Dim xAmount As String

        Try
           

            xfilename = tpath & "\Coop_Loan.csv"
            FileHolder = New FileInfo(xfilename)
            WriteFile = FileHolder.CreateText()

            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPayMainDeductOther_Web_CoopLoan_Extract", UserNo, TransNo)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                     
                        xAmount = Generic.ToStr(ds.Tables(0).Rows(0)("AmountTxt"))
                        WriteFile.WriteLine(xAmount)
                    Next
                End If
            End If

            ds = Nothing

            WriteFile.Close()
            OpenText("../secured/Disk/Coop_Loan.csv")

        Catch ex As Exception
            WriteFile.Close()
        End Try

    End Sub
    Private Sub OpenText(ByVal fullpath As String)
        Dim FileName As String = ""
        FileName = IO.Path.GetFileName(fullpath)
        Response.Clear()
        Response.ClearContent()
        Response.ContentType = "application/octet-stream"
        Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
        Response.TransmitFile(fullpath)
        Response.End()
    End Sub
#End Region

End Class




