Imports System.Data
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web

Partial Class Secured_BSBillingUnbilledList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim startdate As String = Generic.ToStr(Me.fltxtStartDate.Text)
            Dim enddate As String = Generic.ToStr(Me.fltxtEndDate.Text)
            Dim PayClassNo As Integer = Generic.ToInt(cboPayClassNo.SelectedValue)
            dt = SQLHelper.ExecuteDataTable("BBS_Web_Unbilled", UserNo, "", startdate, enddate, PayClassNo, PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Private Sub populateCombo()

        Try
            cboPayClassNo.DataSource = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", UserNo, PayLocNo)
            cboPayClassNo.DataValueField = "tNo"
            cboPayClassNo.DataTextField = "tDesc"
            cboPayClassNo.DataBind()
        Catch ex As Exception
        End Try

        'Try
        '    cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 35)
        '    cboTabNo.DataValueField = "tNo"
        '    cboTabNo.DataTextField = "tDesc"
        '    cboTabNo.DataBind()
        'Catch ex As Exception
        'End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
       
        populateCombo()
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkRedirect_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'If Generic.ToInt(cboTabNo.SelectedIndex) = 1 Then
        '    Response.Redirect("~/secured/BSBillingUnbilledList.aspx?tModify=false&IsClickMain=1")
        'Else
        Response.Redirect("BSBillingOnProcessList.aspx?TabId=2")
        'End If
    End Sub
    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub
    
End Class

