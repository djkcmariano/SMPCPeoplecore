Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_DTRDetlList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticated()

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateGridDetl()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
    End Sub

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Try
            Dim ComponentNo As Integer = 3 'Self Service
            Dim _dt As DataTable
            grdDetl.Columns("Work Allocation").Visible = False
            If Generic.ToInt(cboDTRSource.SelectedValue) = 1 Then
                'DTR Summary
                _dt = SQLHelper.ExecuteDataTable("EDTRDeti_WebSelf", UserNo, Generic.ToInt(cboTabNo.SelectedValue), ComponentNo, txtStartDate.Text, txtEndDate.Text)
                Me.grdMain.DataSource = _dt
                Me.grdMain.DataBind()

                If Generic.ToInt(cboTabNo.SelectedValue) = 1 Then
                    'DTR
                    grdDetl.Columns("Work Allocation").Visible = True
                    If ViewState("DTRDetiNo") = 0 Or IsMain = True Then
                        Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"FullName", "DTRDetiNo", "DTRNo", "EmployeeNo"})
                        lblDetl.Text = obj(0)
                        ViewState("DTRDetiNo") = obj(1)
                    End If
                Else
                    'DTR Discrepancy
                    grdDetl.Columns("Work Allocation").Visible = False
                    If (ViewState("EmployeeNo") = 0 And ViewState("DTRNo") = 0) Or IsMain = True Then
                        Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"FullName", "DTRDetiNo", "DTRNo", "EmployeeNo"})
                        lblDetl.Text = obj(0)
                        ViewState("DTRNo") = obj(2)
                        ViewState("EmployeeNo") = obj(3)
                    End If
                End If

            Else
                'DTR Detail
                _dt = SQLHelper.ExecuteDataTable("EDTRDeti_WebSelf_DateRange", UserNo, Generic.ToInt(cboTabNo.SelectedValue), ComponentNo, txtStartDate.Text, txtEndDate.Text)
                Me.grdMain.DataSource = _dt
                Me.grdMain.DataBind()

                If ViewState("EmployeeNo") = 0 Or IsMain = True Then
                    Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"FullName", "DTRDetiNo", "DTRNo", "EmployeeNo"})
                    lblDetl.Text = obj(0)
                    ViewState("EmployeeNo") = obj(3)
                End If
            End If

            PopulateGridDetl()

        Catch ex As Exception

        End Try


    End Sub

    Private Sub PopulateGridDetl()
        Try
            Dim _dt As DataTable

            If Generic.ToInt(cboDTRSource.SelectedValue) = 1 Then
                _dt = SQLHelper.ExecuteDataTable("EDTRDetiLog_WebSelf", UserNo, Generic.ToInt(cboTabNo.SelectedValue), Generic.ToInt(ViewState("DTRDetiNo")), Generic.ToInt(ViewState("DTRNo")), Generic.ToInt(ViewState("EmployeeNo")))
            Else
                _dt = SQLHelper.ExecuteDataTable("EDTRDetiLog_WebSelf_DateRange", UserNo, Generic.ToInt(cboTabNo.SelectedValue), Generic.ToInt(ViewState("EmployeeNo")), txtStartDate.Text, txtEndDate.Text)
            End If
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
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"FullName", "DTRDetiNo", "DTRNo", "EmployeeNo"})
        lblDetl.Text = obj(0)
        ViewState("DTRDetiNo") = obj(1)
        ViewState("DTRNo") = obj(2)
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

    Protected Sub lnkViewShift_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As LinkButton
            lnk = sender

            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable

            dtx = SQLHelper.ExecuteDataTable("EShift_Web_DTRDetiLog_View", Generic.ToStr(lnk.Text))

            For Each rowx As DataRow In dtx.Rows
                messagedialog = Generic.ToStr(rowx("SQLString"))
            Next

            MessageBox.Alert(messagedialog, "information", Me, "topRight")


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdExport_RenderBrick(sender As Object, e As DevExpress.Web.ASPxGridViewExportRenderingEventArgs) Handles grdExport.RenderBrick
        Dim dataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)

        If e.RowType = GridViewRowType.Header AndAlso dataColumn IsNot Nothing Then
            e.Text = e.Text.Replace("<br/>", " ")
            e.Text = e.Text.Replace("<br>", " ")
            e.Text = e.Text.Replace("<center>", "")
            e.Text = e.Text.Replace("</center>", "")
        End If

    End Sub


    Protected Sub ASPxGridViewExporter1_RenderBrick(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewExportRenderingEventArgs) Handles ASPxGridViewExporter1.RenderBrick
        Dim dataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
        If e.RowType = GridViewRowType.Data AndAlso dataColumn IsNot Nothing Then
            Select Case dataColumn.FieldName
                Case "AbsHrs"
                    e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
                    e.TextValue = e.TextValue.ToString.Replace("<span>", "")
                    e.TextValue = e.TextValue.ToString.Replace("</span>", "")
                Case "Late"
                    e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
                    e.TextValue = e.TextValue.ToString.Replace("<span>", "")
                    e.TextValue = e.TextValue.ToString.Replace("</span>", "")
                Case "Under"
                    e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
                    e.TextValue = e.TextValue.ToString.Replace("<span>", "")
                    e.TextValue = e.TextValue.ToString.Replace("</span>", "")
            End Select





        End If
        If e.RowType = GridViewRowType.Header AndAlso dataColumn IsNot Nothing Then
            e.Text = e.Text.Replace("<br/>", " ")
            e.Text = e.Text.Replace("<br>", " ")
            e.Text = e.Text.Replace("<center>", "")
            e.Text = e.Text.Replace("</center>", "")
        End If

    End Sub

    Protected Sub lnkSubmit_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRDetiNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, 1)
            i = i + 1
        Next

        If i > 0 Then
            PopulateGrid(True)
            Dim url As String = "SelfDTRDetlList.aspx"
            MessageBox.SuccessResponse("(" + i.ToString + ") " + MessageTemplate.SuccessSubmit, Me, url)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Private Sub ApproveTransaction(tId As Integer, issubmit As Integer)
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("ETimeSheet_WebSubmit", UserNo, tId, issubmit)
    End Sub

    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRNo", "DTRDetiLogNo"})

        Response.Redirect("~/SecuredSelf/SelfDTRDetlList_Allocation.aspx?id=" & obj(0) & "&transNo=" & obj(1))

    End Sub

#Region "********MAIN Check All********"

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs)
        If e.ButtonType <> ColumnCommandButtonType.SelectCheckbox Then
            Return
        End If
        Dim rowEnabled As Boolean = getRowEnabledStatus(e.VisibleIndex)
        e.Enabled = rowEnabled
    End Sub

    Private Function getRowEnabledStatus(ByVal VisibleIndex As Integer) As Boolean
        Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(VisibleIndex, "IsEnabled"))
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
    Protected Sub gridMain_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        Boolean.TryParse(e.Parameters, False)

        Dim startIndex As Integer = grdMain.PageIndex * grdMain.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdMain.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)
        For i As Integer = startIndex To endIndex - 1
            Dim rowEnabled As Boolean = getRowEnabledStatus(i)
            If rowEnabled AndAlso e.Parameters = "true" Then
                grdMain.Selection.SelectRow(i)
            Else
                grdMain.Selection.UnselectRow(i)
            End If
        Next i

    End Sub

#End Region

End Class
