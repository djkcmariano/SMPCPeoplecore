Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_AppJobOfferRef
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TableName As String

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EApplicantJobOffer_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TableName = Generic.ToStr(Session("xTablename"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            'PopulateDropDown()
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
            Try
                cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 14)
                cboTabNo.DataTextField = "tDesc"
                cboTabNo.DataValueField = "tno"
                cboTabNo.DataBind()
            Catch ex As Exception
            End Try

            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try
        End If

        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopup")            
            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EApplicantJobOffer_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopup", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantJobOfferNo", "IsEnabled"})
            Dim iNo As Integer = Generic.ToInt(obj(0))
            Dim IsEnabled As Boolean = Generic.ToBol(obj(1))
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantJobOfferNo"})))
            btnSave.Enabled = IsEnabled
            
            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False

        If SQLHelper.ExecuteNonQuery("EApplicantJobOffer_WebSave", UserNo, Generic.ToInt(txtCode.Text), txtApplicantJobOfferDesc.Text, txtContent.Html, chkIsArchived.Checked, Generic.ToInt(cboPayLocNo.SelectedValue)) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ApplicantJobOfferNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EApplicantJobOffer", UserNo, Generic.ToInt(item))
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub
    'Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
    '    If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
    '        Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
    '        e.Enabled = value
    '    End If
    'End Sub
#Region "********Detail Check All********"


    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs)
        If e.ButtonType <> ColumnCommandButtonType.SelectCheckbox Then
            Return
        End If
        Dim rowEnabled As Boolean = getRowEnabledStatus(e.VisibleIndex)
        e.Enabled = rowEnabled

        'If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
        '    Dim isSelected As Boolean = Convert.ToBoolean(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
        '    If isSelected Then

        '        grdMain.Selection.SetSelection(e.VisibleIndex, True)

        '    End If
        'End If
    End Sub

    Private Function getRowEnabledStatus(ByVal VisibleIndex As Integer) As Boolean
        Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(VisibleIndex, "IsEnabled"))
        If value = True Then
            Return True
        Else
            Return False
        End If
    End Function

    'Protected Sub cbCheckAll_Init(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim cb As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)
    '    cb.ClientSideEvents.CheckedChanged = String.Format("cbCheckAll_CheckedChanged")
    '    cb.Checked = False
    '    Dim count As Integer = 0
    '    Dim startIndex As Integer = grdMain.PageIndex * grdMain.SettingsPager.PageSize
    '    Dim endIndex As Integer = Math.Min(grdMain.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)

    '    For i As Integer = startIndex To endIndex - 1
    '        If grdMain.Selection.IsRowSelected(i) Then
    '            count = count + 1
    '        End If
    '    Next i

    '    If count > 0 Then
    '        cb.Checked = True
    '    End If

    'End Sub

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




