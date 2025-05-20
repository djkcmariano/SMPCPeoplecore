Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO

Partial Class SecuredSelf_SelfDTRDetlList_Allocation
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

        Permission.IsAuthenticated()

        PopulateGrid()
        PopulateData()

        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDTR_WebOne", UserNo, DTRNo)
        For Each row As DataRow In dt.Rows
            'lnkSave.Enabled = Not Generic.ToBol(row("IsPosted"))
            lnkAdd.Enabled = Not Generic.ToBol(row("IsPosted"))
            'lnkUpload.Enabled = Not Generic.ToBol(row("IsPosted"))
            lnkDelete.Enabled = Not Generic.ToBol(row("IsPosted"))
        Next
    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EDTRDetiLogAlloc_Web", UserNo, DTRDetiLogNo, DTRNo, PayLocNo)

        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

    End Sub


    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub


    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)

        Generic.ClearControls(Me, "pnlPopup")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)

        mdlShow.Show()

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try


            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRDetiLogAllocNo"}))

            Generic.ClearControls(Me, "pnlPopup")
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRDetiLogAlloc_WebOne", UserNo, Generic.ToInt(i))
            For Each row As DataRow In dt.Rows
                Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
                Generic.PopulateData(Me, "pnlPopup", dt)
            Next

            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Generic.EnableControls(Me, "pnlPopup", IsEnabled)
            btnSave.Enabled = IsEnabled

            mdlShow.Show()


        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0


        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRDetiLogAllocNo"})
        Dim str As String = ""
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EDTRDetiLogAlloc", UserNo, CType(item, Integer))
            DeleteCount = DeleteCount + 1
        Next

        If DeleteCount > 0 Then
            PopulateGrid()
            MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim DepartmentNo As Integer = Generic.ToInt(cboDepartmentNo.SelectedValue)
        Dim CostCenterNo As Integer = Generic.ToInt(cboCostCenterNo.SelectedValue)
        Dim Hrs As Double = Generic.ToDbl(txtHrs.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Html)
        Dim ProjectNo As Integer = Generic.ToInt(cboProjectNo.SelectedValue)
        Dim In1 As String = Generic.ToStr(Replace(txtIn1.Text, ":", ""))
        Dim Out1 As String = Generic.ToStr(Replace(txtOut1.Text, ":", ""))
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EDTRDetiLogAlloc_WebValidate", UserNo, Generic.ToInt(txtDTRDetiLogAllocNo.Text), DTRDetiLogNo, DTRNo, DepartmentNo, CostCenterNo, Hrs, Remarks, PayLocNo)
        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlShow.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EDTRDetiLogAlloc_WebSaveSelf", UserNo, Generic.ToInt(txtDTRDetiLogAllocNo.Text), DTRDetiLogNo, DTRNo, DepartmentNo, CostCenterNo, Hrs, Remarks, ProjectNo, PayLocNo, In1, Out1) > 0 Then
            Retval = True
        Else
            Retval = True
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub

#Region "********Check All********"


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
