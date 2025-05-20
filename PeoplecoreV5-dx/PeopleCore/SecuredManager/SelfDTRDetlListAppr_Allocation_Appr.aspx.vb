Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO

Partial Class SecuredManager_SelfDTRDetlListAppr_Allocation
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticatedSuperior()
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        PopulateGridDetl()
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)
    End Sub

    Private Sub PopulateGridDetl()
        Try
            Dim _dt As DataTable

            'If Generic.ToInt(cboDTRSource.SelectedValue) = 1 Then
            _dt = SQLHelper.ExecuteDataTable("EDTRDetiLogAlloc_WebSelf", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)

            'End If
            grdDetl.DataSource = _dt
            grdDetl.DataBind()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 12)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGridDetl()
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
    Protected Sub lnkApproved_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"DTRDetiLogAllocNo"})
        Dim str As String = "", i As Integer = 0
        'For Each item As Integer In fieldValues
        '    ApproveTransaction(item, "", 2)
        '    i = i + 1
        'Next

        For j As Integer = 0 To grdDetl.VisibleRowCount - 1
            If grdDetl.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdDetl.GetRowValues(j, "DTRDetiLogAllocNo"))
                ApproveTransaction(item, "", 2)
                grdDetl.Selection.UnselectRow(j)
                i = i + 1
            End If
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me)
            PopulateGridDetl()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Protected Sub lnkDisApproved_Click(sender As Object, e As System.EventArgs)
        Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"DTRDetiLogAllocNo"})
        Dim str As String = "", i As Integer = 0
        'For Each item As Integer In fieldValues
        '    ApproveTransaction(item, "", 3)
        '    i = i + 1
        'Next

        For j As Integer = 0 To grdDetl.VisibleRowCount - 1
            If grdDetl.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdDetl.GetRowValues(j, "DTRDetiLogAllocNo"))
                ApproveTransaction(item, "", 3)
                grdDetl.Selection.UnselectRow(j)
                i = i + 1
            End If
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDisapproved, Me)
            PopulateGridDetl()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Private Sub ApproveTransaction(tId As Integer, remarks As String, approvalStatNo As Integer)
        'Dim fds As DataSet
        SQLHelper.ExecuteNonQuery("EDTRDetiLogAlloc_WebApproved", UserNo, tId, approvalStatNo, PayLocNo)
        'fds = SQLHelper.ExecuteDataSet("EDTRDetiLogAlloc_WebApproved", UserNo, tId, approvalStatNo, PayLocNo)
        'If fds.Tables.Count > 0 Then
        '    If fds.Tables(0).Rows.Count > 0 Then
        '        Dim IsWithapprover As Boolean
        '        IsWithapprover = Generic.CheckDBNull(fds.Tables(0).Rows(0)("IsWithApprover"), clsBase.clsBaseLibrary.enumObjectType.IntType)
        '        If IsWithapprover = True Then
        '        Else
        '            MessageBox.Information("Unable to locate the next approver.", Me)
        '        End If
        '    End If


        'End If
    End Sub

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
        Dim startIndex As Integer = grdDetl.PageIndex * grdDetl.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdDetl.VisibleRowCount, startIndex + grdDetl.SettingsPager.PageSize)

        For i As Integer = startIndex To endIndex - 1
            If grdDetl.Selection.IsRowSelected(i) Then
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
