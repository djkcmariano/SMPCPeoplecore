Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_CliDoctorList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0


    'Protected Sub PopulateGrid(Optional IsMain As Boolean = False)
    '    Try
    '        Dim dt As DataTable
    '        Dim sortDirection As String = "", sortExpression As String = ""

    '        dt = SQLHelper.ExecuteDataTable("EDoctor_Web", UserNo, Filter1.SearchText, PayLocNo)
    '        Dim dv As DataView = dt.DefaultView
    '        If ViewState("SortDirection") IsNot Nothing Then
    '            sortDirection = ViewState("SortDirection").ToString()
    '        End If
    '        If ViewState("SortExpression") IsNot Nothing Then
    '            sortExpression = ViewState("SortExpression").ToString()
    '            dv.Sort = String.Concat(sortExpression, " ", sortDirection)
    '        End If

    '        grdMain.SelectedIndex = 0
    '        grdMain.DataSource = dv
    '        grdMain.DataBind()

    '        If dt.Rows.Count > 0 Then
    '            ViewState("TransNo") = grdMain.DataKeys(0).Values(0).ToString()
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EDoctor_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    '    UserNo = Generic.ToInt(Session("OnlineUserNo"))
    '    PayLocNo = Generic.ToInt(Session("xPayLocNo"))

    '    AccessRights.CheckUser(UserNo)

    '    If Not IsPostBack Then
    '        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
    '        PopulateGrid()
    '    End If

    '    AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch1_Click

    '    Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    'End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            PopulateDropDown()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    'Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
    '        Dim ib As New ImageButton
    '        ib = sender

    '        Dim dt As DataTable
    '        dt = SQLHelper.ExecuteDataTable("EDoctor_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
    '        For Each row As DataRow In dt.Rows
    '            Generic.PopulateData(Me, "pnlPopupMain", dt)
    '        Next
    '        mdlMain.Show()

    '    Else
    '        MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
    '    End If

    'End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DoctorNo", "IsEnabled"})
        Dim iNo As Integer = Generic.ToInt(obj(0))
        Dim IsEnabled As Boolean = Generic.ToBol(obj(1))
        PopulateData(iNo)
        lnkSave.Enabled = IsEnabled
        Try
            cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
            cboPayLocNo.DataTextField = "tdesc"
            cboPayLocNo.DataValueField = "tNo"
            cboPayLocNo.DataBind()

        Catch ex As Exception

        End Try
        mdlMain.Show()
    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try
            lnkSave.Enabled = True
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    'Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
    '        For i As Integer = 0 To Me.grdMain.Rows.Count - 1
    '            chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
    '            ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
    '            If chk.Checked = True Then
    '                Generic.DeleteRecordAudit("EDoctor", UserNo, Generic.ToInt(ib.CommandArgument))
    '                Count = Count + 1
    '            End If
    '        Next

    '        If Count > 0 Then
    '            PopulateGrid()
    '            MessageBox.Success("(" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
    '        Else
    '            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
    '        End If

    '    Else
    '        MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
    '    End If

    'End Sub
    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DoctorNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDoctor", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub


    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If SaveRecord() Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Private Function SaveRecord() As Boolean

        Dim DoctorNo As Integer = Generic.ToInt(txtCode.Text)
        If SQLHelper.ExecuteNonQuery("EDoctor_WebSave", UserNo, DoctorNo, Me.txtLastName.Text.ToString, Me.txtFirstName.Text.ToString, Me.txtMiddleName.Text.ToString, Me.txtAddress.Text.ToString, Me.txtCelNo.Text.ToString, Me.txtContactNo.Text.ToString, Me.txtOfficeAdd.Text.ToString, Generic.ToBol(chkIsArchived.Checked), Generic.ToInt(cboPayLocNo.SelectedValue)) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If
    End Function


    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDoctor_WebOne", UserNo, id)
        Generic.PopulateData(Me, "pnlPopupMain", dt)
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

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 14)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub
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

