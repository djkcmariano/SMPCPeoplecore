Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_TrnMatrixList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim tstatus As Integer = 0

    Private Sub PopulateDetl()

        imgPhoto.ImageUrl = "~/SecuredSelf/frmShowImage.ashx?tNo=" & Generic.ToInt(ViewState("TransNo")) & "&tIndex=2"
        lblDetl.Text = Generic.ToStr(Session("FullName"))

        If Generic.ToInt(ViewState("Index")) = 0 Then
            ViewState("Index") = 3
        End If

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ETrnMatrix_Web", UserNo, Generic.ToInt(ViewState("TransNo")), 0, 3, PayLocNo)
        Dim dv As New DataView(dt)
        dv.RowFilter = "MatrixStatNo=" & Generic.ToStr(ViewState("Index"))
        grdDetl.DataSource = dv
        grdDetl.DataBind()

        Dim Count1 As Integer = dt.Select("MatrixStatNo=1").Length
        Dim Count2 As Integer = dt.Select("MatrixStatNo=2").Length
        Dim Count3 As Integer = dt.Select("MatrixStatNo=3").Length
        Dim Count4 As Integer = dt.Select("MatrixStatNo=4").Length
        Dim Count5 As Integer = dt.Select("MatrixStatNo=5").Length
        Dim Count6 As Integer = dt.Select("MatrixStatNo=6").Length

        lnkRequired.Text = "Required "
        lnkCompleted.Text = "Completed "
        lnkTaken.Text = "To Be Taken "
        lnkIncomplete.Text = "Incomplete "
        lnkNoShow.Text = "No Show "
        lnkService.Text = "Training Bond "

        If Count1 > 0 Then
            lnkRequired.Text = "Required " & "<span class='label label-warning pull-right'>" & Count1.ToString & "</span>"
        End If
        If Count2 > 0 Then
            lnkCompleted.Text = "Completed " & "<span class='label label-warning pull-right'>" & Count2.ToString & "</span>"
        End If
        If Count3 > 0 Then
            lnkTaken.Text = "To Be Taken " & "<span class='label label-warning pull-right'>" & Count3.ToString & "</span>"
        End If
        If Count4 > 0 Then
            lnkIncomplete.Text = "Incomplete " & "<span class='label label-warning pull-right'>" & Count4.ToString & "</span>"
        End If
        If Count5 > 0 Then
            lnkNoShow.Text = "No Show " & "<span class='label label-warning pull-right'>" & Count5.ToString & "</span>"
        End If
        If Count6 > 0 Then
            lnkService.Text = "Training Bond " & "<span class='label label-warning pull-right'>" & Count6.ToString & "</span>"
        End If

        PopulateTab(Generic.ToStr(ViewState("Index")))

    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        ViewState("TransNo") = Generic.ToInt(Session("EmployeeNo"))
        Permission.IsAuthenticated()

        'If Not IsPostBack Then

        'End If

        PopulateDetl()
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkTaken_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ViewState("Index") = 3
        PopulateDetl()
    End Sub

    Protected Sub lnkRequired_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ViewState("Index") = 1
        PopulateDetl()
    End Sub

    Protected Sub lnkCompleted_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ViewState("Index") = 2
        PopulateDetl()
    End Sub

    Protected Sub lnkIncomplete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ViewState("Index") = 4
        PopulateDetl()
    End Sub

    Protected Sub lnkNoShow_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ViewState("Index") = 5
        PopulateDetl()
    End Sub

    Protected Sub lnkService_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ViewState("Index") = 6
        PopulateDetl()
    End Sub

    Private Sub PopulateTab(Optional Index As Integer = 0)

        If Index = 1 Then 'Required
            lnkTaken.CssClass = "list-group-item text-left"
            lnkRequired.CssClass = "list-group-item text-left active"
            lnkCompleted.CssClass = "list-group-item text-left"
            lnkIncomplete.CssClass = "list-group-item text-left"
            lnkNoShow.CssClass = "list-group-item text-left"
            lnkService.CssClass = "list-group-item text-left"
            grdDetl.Columns("Required").Visible = False
            grdDetl.Columns("Date of Training").Visible = False
            grdDetl.Columns("Hour(s)").Visible = False
            grdDetl.Columns("Validity of Certificate").Visible = False
            grdDetl.Columns("Validity of Bond").Visible = False
            grdDetl.Columns("Status").Visible = False
            grdDetl.Columns("Reason").Visible = False
        ElseIf Index = 2 Then 'Completed
            lnkTaken.CssClass = "list-group-item text-left"
            lnkRequired.CssClass = "list-group-item text-left"
            lnkCompleted.CssClass = "list-group-item text-left active"
            lnkIncomplete.CssClass = "list-group-item text-left"
            lnkNoShow.CssClass = "list-group-item text-left"
            lnkService.CssClass = "list-group-item text-left"
            grdDetl.Columns("Required").Visible = False
            grdDetl.Columns("Date of Training").Visible = True
            grdDetl.Columns("Hour(s)").Visible = True
            grdDetl.Columns("Validity of Certificate").Visible = True
            grdDetl.Columns("Validity of Bond").Visible = False
            grdDetl.Columns("Status").Visible = True
            grdDetl.Columns("Reason").Visible = False
        ElseIf Index = 4 Then 'Incomplete
            lnkTaken.CssClass = "list-group-item text-left"
            lnkRequired.CssClass = "list-group-item text-left"
            lnkCompleted.CssClass = "list-group-item text-left"
            lnkIncomplete.CssClass = "list-group-item text-left active"
            lnkNoShow.CssClass = "list-group-item text-left"
            lnkService.CssClass = "list-group-item text-left"
            grdDetl.Columns("Required").Visible = False
            grdDetl.Columns("Date of Training").Visible = True
            grdDetl.Columns("Hour(s)").Visible = True
            grdDetl.Columns("Validity of Certificate").Visible = False
            grdDetl.Columns("Validity of Bond").Visible = False
            grdDetl.Columns("Status").Visible = False
            grdDetl.Columns("Reason").Visible = True
        ElseIf Index = 5 Then 'No Show
            lnkTaken.CssClass = "list-group-item text-left"
            lnkRequired.CssClass = "list-group-item text-left"
            lnkCompleted.CssClass = "list-group-item text-left"
            lnkIncomplete.CssClass = "list-group-item text-left"
            lnkNoShow.CssClass = "list-group-item text-left active"
            lnkService.CssClass = "list-group-item text-left"
            grdDetl.Columns("Required").Visible = False
            grdDetl.Columns("Date of Training").Visible = True
            grdDetl.Columns("Hour(s)").Visible = True
            grdDetl.Columns("Validity of Certificate").Visible = False
            grdDetl.Columns("Validity of Bond").Visible = False
            grdDetl.Columns("Status").Visible = False
            grdDetl.Columns("Reason").Visible = True
        ElseIf Index = 6 Then 'Service Contract
            lnkTaken.CssClass = "list-group-item text-left"
            lnkRequired.CssClass = "list-group-item text-left"
            lnkCompleted.CssClass = "list-group-item text-left"
            lnkIncomplete.CssClass = "list-group-item text-left"
            lnkNoShow.CssClass = "list-group-item text-left"
            lnkService.CssClass = "list-group-item text-left active"
            grdDetl.Columns("Required").Visible = False
            grdDetl.Columns("Date of Training").Visible = True
            grdDetl.Columns("Hour(s)").Visible = True
            grdDetl.Columns("Validity of Certificate").Visible = False
            grdDetl.Columns("Validity of Bond").Visible = True
            grdDetl.Columns("Status").Visible = True
            grdDetl.Columns("Reason").Visible = False
        Else 'To Be Taken
            Index = 3
            lnkTaken.CssClass = "list-group-item text-left active"
            lnkRequired.CssClass = "list-group-item text-left"
            lnkCompleted.CssClass = "list-group-item text-left"
            lnkIncomplete.CssClass = "list-group-item text-left"
            lnkNoShow.CssClass = "list-group-item text-left"
            lnkService.CssClass = "list-group-item text-left"
            grdDetl.Columns("Required").Visible = False
            grdDetl.Columns("Date of Training").Visible = False
            grdDetl.Columns("Hour(s)").Visible = False
            grdDetl.Columns("Validity of Certificate").Visible = False
            grdDetl.Columns("Validity of Bond").Visible = False
            grdDetl.Columns("Status").Visible = True
            grdDetl.Columns("Reason").Visible = False
        End If

    End Sub

End Class

