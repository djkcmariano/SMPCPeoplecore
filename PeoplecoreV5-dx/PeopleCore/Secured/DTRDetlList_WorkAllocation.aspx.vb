Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO

Partial Class Secured_DTRDetlList_WorkAllocation
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim DTRDetiLogNo As Integer
    Dim DTRNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        DTRDetiLogNo = Generic.ToInt(Request.QueryString("transNo"))
        DTRNo = Generic.ToInt(Request.QueryString("id"))

        AccessRights.CheckUser(UserNo, "DTR.aspx", "EDTR")

        PopulateGrid()
        PopulateData()

        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDTR_WebOne", UserNo, DTRNo)
        For Each row As DataRow In dt.Rows
            
        Next
    End Sub

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EDTRDeti_Web", UserNo, DTRNo, Generic.ToInt(cboTabNo.SelectedValue))

        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If Generic.ToInt(cboTabNo.SelectedValue) = 1 Then
            'DTR Present
            If ViewState("DTRDetiNo") = 0 Or IsMain = True Then
                Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"FullName", "DTRDetiNo", "DTRNo", "EmployeeNo"})
                lblDetl.Text = obj(0)
                ViewState("DTRDetiNo") = obj(1)
                ViewState("DTRNo") = obj(2)
                ViewState("EmployeeNo") = obj(3)
            End If
        Else
            If (ViewState("EmployeeNo") = 0 And ViewState("DTRNo") = 0) Or IsMain = True Then
                Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"FullName", "DTRDetiNo", "DTRNo", "EmployeeNo"})
                lblDetl.Text = obj(0)
                ViewState("DTRNo") = obj(2)
                ViewState("EmployeeNo") = obj(3)
            End If
        End If

        PopulateGridDetl()




    End Sub


    Private Sub PopulateGridDetl()
        Try
            Dim _dt As DataTable

            'If Generic.ToInt(cboDTRSource.SelectedValue) = 1 Then
            _dt = SQLHelper.ExecuteDataTable("EDTRDetiLogAlloc_WebPerDTR", UserNo, Generic.ToInt(ViewState("DTRNo")), ViewState("DTRDetiNo"), PayLocNo)

            'End If
            grdDetl.DataSource = _dt
            grdDetl.DataBind()


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

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"FullName", "DTRNo", "DTRDetiNo", "EmployeeNo"})
        lblDetl.Text = obj(0)
        ViewState("DTRNo") = obj(1)
        ViewState("DTRDetiNo") = obj(2)
        ViewState("EmployeeNo") = obj(3)

        PopulateGrid()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid(True)
    End Sub

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            Dim x As New XlsxExportOptionsEx
            x.ExportType = ExportType.WYSIWYG
            ASPxGridViewExporter1.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

#Region "*********Approval************"

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
#End Region
#Region "********Check All********"


    Protected Sub grdDetl_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs)
        If e.ButtonType <> ColumnCommandButtonType.SelectCheckbox Then
            Return
        End If
        Dim rowEnabled As Boolean = getRowEnabledStatus(e.VisibleIndex)
        e.Enabled = rowEnabled

    End Sub
    Private Function getRowEnabledStatus(ByVal VisibleIndex As Integer) As Boolean
        Dim value As Boolean = Generic.ToInt(grdDetl.GetRowValues(VisibleIndex, "IsEnabled"))
        If value = True Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Sub cbCheckAll_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim cb As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)
        cb.ClientSideEvents.CheckedChanged = String.Format("cbCheckAll_CheckedChanged")
        cb.Checked = False
        Dim count As Integer = 0
        Dim startIndex As Integer = grdMain.PageIndex * grdMain.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdMain.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)

        For i As Integer = startIndex To endIndex - 1
            If grdMain.Selection.IsRowSelected(i) Then
                count = count + 1
            End If
        Next i

        If count > 0 Then
            cb.Checked = True
        End If

    End Sub
    Protected Sub grdDetl_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        Boolean.TryParse(e.Parameters, False)

        Dim startIndex As Integer = grdDetl.PageIndex * grdDetl.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdDetl.VisibleRowCount, startIndex + grdDetl.SettingsPager.PageSize)
        For i As Integer = startIndex To endIndex - 1
            Dim rowEnabled As Boolean = getRowEnabledStatus(i)
            If rowEnabled AndAlso e.Parameters = "true" Then
                grdDetl.Selection.SelectRow(i)
            Else
                grdDetl.Selection.UnselectRow(i)
            End If
        Next i

    End Sub

#End Region
End Class
