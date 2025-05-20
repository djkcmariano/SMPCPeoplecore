Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_DTRLogMassList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0


#Region "********Main********"
    Protected Sub PopulateGrid(Optional IsMain As Boolean = False)
        Try

            Dim tStatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
            If tStatus = 1 Then
                lnkAppend.Visible = False
                lnkDeleteDetl.Visible = False
            Else
                lnkAppend.Visible = True
                lnkDeleteDetl.Visible = True
            End If

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRLogMass_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()

            If ViewState("TransNo") = 0 Or IsMain = True Then
                Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"DTRLogMassNo", "DTRLogMassTransNo"})
                ViewState("TransNo") = obj(0)
                lbl.Text = obj(1)
            End If

            PopulateGridDetl()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRLogMass_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupMain", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "DTRLog.aspx", "EDTRLog")
        If Not IsPostBack Then
            PopulateDropDown()
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupAppend", PayLocNo)
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateGridDetl()
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 4)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "DTRLog.aspx", "EDTRLog") Then
            Generic.ClearControls(Me, "pnlPopupMain")
            Generic.EnableControls(Me, "pnlPopupMain", True)
            lnkSave.Enabled = True
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "DTRLog.aspx", "EDTRLog") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRLogMassNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)
            lnkSave.Enabled = IsEnabled
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "DTRLog.aspx", "EDTRLog") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRLogMassNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EDTRLogMassDeti", UserNo, "DTRLogMassNo", item)
                Generic.DeleteRecordAudit("EDTRLogMass", UserNo, item)
                i = i + 1
            Next

            If i > 0 Then
                PopulateGrid(True)
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRLogMassNo", "DTRLogMassTransNo"})
        ViewState("TransNo") = obj(0)
        lbl.Text = obj(1)
        PopulateGridDetl()
    End Sub

    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost, "DTRLog.aspx", "EDTRLog") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRLogMassNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("EDTRLogMass_WebForward", UserNo, item, PayLocNo) > 0 Then
                    Count = Count + 1
                End If
                i = i + 1
            Next

            If i > 0 Then
                PopulateGrid(True)
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccesPost, Me)
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid(True)
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim DTRLogMassNo As Integer = Generic.ToInt(txtDTRLogMassNo.Text)
        Dim Description As String = Generic.ToStr(txtDescription.Text)
        Dim DTRDate As String = Generic.ToStr(txtDTRDate.Text)
        Dim In1 As String = Generic.ToStr(Replace(txtIn1.Text, ":", ""))
        Dim Out1 As String = Generic.ToStr(Replace(txtOut1.Text, ":", ""))
        Dim In2 As String = Generic.ToStr(Replace(txtIn2.Text, ":", ""))
        Dim Out2 As String = Generic.ToStr(Replace(txtOut2.Text, ":", ""))
        Dim ApprovalStatNo As Integer = 0
        Dim ComponentNo As Integer = 1 'Administrator
        Dim IsFromOnline As Boolean = False

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EDTRLog_WebValidate", UserNo, 0, 0, DTRDate, In1, Out1, In2, Out2, Description, 0, ApprovalStatNo, PayLocNo, ComponentNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlMain.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EDTRLogMass_WebSave", UserNo, DTRLogMassNo, Description, DTRDate, In1, Out1, In2, Out2, PayLocNo, IsFromOnline) > 0 Then
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

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub
#End Region

#Region "********Details********"
    Protected Sub PopulateGridDetl()
        Try

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRLogMassDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")), "")
            grdDetl.DataSource = dt
            grdDetl.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "DTRLog.aspx", "EDTRLog") Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"DTRLogMassDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTRLogMassDeti", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridDetl()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

#End Region

#Region "********Detail Check All********"


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
    Protected Sub gridDetl_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
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

#Region "********Append*******"

    Protected Sub lnkAppend_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "DTRLog.aspx", "EDTRLog") Then
            If Generic.ToInt(ViewState("TransNo")) > 0 Then
                Generic.ClearControls(Me, "pnlPopupAppend")
                cboFilteredbyNo.Text = 1
                drpAC.CompletionSetCount = 1
                mdlAppend.Show()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub cboFilteredbyNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            Dim fId As Integer
            fId = Generic.ToInt(Generic.CheckDBNull(cboFilteredbyNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType))
            txtName.Text = ""
            If fId > 0 Then
                txtName.Enabled = True
                drpAC.CompletionSetCount = fId
            Else
                txtName.Enabled = False
                drpAC.CompletionSetCount = 0
            End If

            mdlAppend.Show()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSaveAppend_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "DTRLog.aspx", "EDTRLog") Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(ViewState("TransNo"))
            Dim FilteredbyNo As Integer = Generic.ToInt(Me.cboFilteredbyNo.SelectedValue)
            Dim FilterValue As Integer = Generic.ToInt(Me.hiffilterbyid.Value)
            Dim ShiftNo As Integer = Generic.ToInt(Me.cboShiftNo.SelectedValue)

            If SQLHelper.ExecuteNonQuery("EDTRLogMassDeti_WebSave", UserNo, tno, FilteredbyNo, FilterValue, ShiftNo) >= 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                PopulateGridDetl()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

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

#End Region

End Class

