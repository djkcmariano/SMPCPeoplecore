Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_ERDAOwnerList
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim tstatus As Integer
    Dim dscount As Double = 0
    Dim _ds As New DataSet
    Dim _dt As New DataTable
    Dim xScript As String = ""
    Dim rowno As Integer = 0
    Dim RefNo As Integer = 0
    Dim showFrm As New clsFormControls

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0


    'Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")

    '    _ds = sqlHelper.ExecuteDataset("EDAOwner_Web", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(Viewstate(xScript & "filter"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType), Session("xPayLocNo"))
    '    _dt = _ds.Tables(0)
    '    Dim dv As New Data.DataView(_dt)
    '    If SortExp <> "" Then
    '        Viewstate(xScript & "SortExp") = SortExp
    '    End If
    '    If sordir <> "" Then

    '        Viewstate(xScript & "sortdir") = sordir
    '    End If
    '    If _ds.Tables.Count > 0 Then
    '        If _ds.Tables(0).Rows.Count > 0 Then
    '            dscount = _ds.Tables(0).Rows.Count
    '            If Viewstate(xScript & "SortExp") <> "" Then
    '                dv.Sort = Viewstate(xScript & "SortExp") + Viewstate(xScript & "sortdir")
    '            End If
    '        Else
    '            Viewstate(xScript & "no") = 0
    '        End If
    '    Else
    '        Viewstate(xScript & "no") = 0
    '    End If
    '    If IsMain Then
    '        ViewState(xScript & "PageNo") = 0
    '        ViewState(Left(xScript, Len(xScript) - 5)) = 0
    '    End If

    '    Me.grdMain.SelectedIndex = Generic.CheckDBNull(ViewState(Left(xScript, Len(xScript) - 5)), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '    Me.grdMain.PageIndex = ViewState(xScript & "PageNo")
    '    Me.grdMain.DataSource = dv
    '    Me.grdMain.DataBind()

    'End Sub


    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EDAOwner_Web", UserNo, cboTabNo.SelectedValue, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    'Populate Combo box
    Private Sub PopulateCombo()
        showFrm.populateCombo(UserNo, Me)
    End Sub


    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    '    xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("OnlineUserNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

    '    RefNo = Generic.CheckDBNull(Request.QueryString("RefNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '    AccessRights.CheckUser(xPublicVar.xOnlineUseNo)
    '    AddHandler Filter1.lnkSearchClick, AddressOf lnkGo_Click

    '    xScript = Request.ServerVariables("SCRIPT_NAME")
    '    xScript = Generic.GetPath(xScript)
    '    If Not IsPostBack Then
    '        PopulateGrid()
    '        PopulateCombo()
    '    End If

    '    Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    'End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Try
                cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 30)
                cboTabNo.DataValueField = "tNo"
                cboTabNo.DataTextField = "tDesc"
                cboTabNo.DataBind()
            Catch ex As Exception
            End Try

            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)            
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

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
            btnSave.Enabled = True
            mdlMain.Show()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If

    End Sub

    'Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try


    '        Dim lnk As New ImageButton
    '        Dim i As String = "", fdtrNo As Integer = 0

    '        lnk = sender
    '        Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
    '        RowNo = gvrow.RowIndex
    '        ViewState(Left(xScript, Len(xScript) - 5)) = RowNo

    '        i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
    '        Viewstate(xScript & "No") = i

    '        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit) Then
    '            Dim _ds As New DataSet
    '            _ds = SQLHelper.ExecuteDataSet("EDAOwner_WebOne", xPublicVar.xOnlineUseNo, i)
    '            If _ds.Tables.Count > 0 Then
    '                If _ds.Tables(0).Rows.Count > 0 Then
    '                    showFrm.clearFormControls_In_Popup(pnlpopupMain)
    '                    showFrm.showFormControls_In_Popup(pnlpopupMain, _ds)
    '                    showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlpopupMain)
    '                End If
    '            End If
    '            mdlMain.Show()
    '        Else
    '            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
    '        End If
    '    Catch ex As Exception
    '    End Try

    'End Sub
    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DAOwnerNo", "IsEnabled"})
        Dim iNo As Integer = Generic.ToInt(obj(0))
        Dim IsEnabled As Boolean = Generic.ToBol(obj(1))
        PopulateData(iNo)
        btnSave.Enabled = IsEnabled
        Try
            cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
            cboPayLocNo.DataTextField = "tdesc"
            cboPayLocNo.DataValueField = "tNo"
            cboPayLocNo.DataBind()

        Catch ex As Exception

        End Try
        mdlMain.Show()
    End Sub


    'Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim lbl As New Label, tcheck As New CheckBox
    '    Dim tcount As Integer, DeleteCount As Integer = 0

    '    If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowDelete) Then
    '        For tcount = 0 To Me.grdMain.Rows.Count - 1
    '            lbl = CType(grdMain.Rows(tcount).FindControl("lblId"), Label)
    '            tcheck = CType(grdMain.Rows(tcount).FindControl("txtIsSelect"), CheckBox)
    '            If tcheck.Checked = True Then
    '                Generic.DeleteRecordAudit("EDAOwner", xPublicVar.xOnlineUseNo, CType(lbl.Text, Integer))
    '                DeleteCount = DeleteCount + 1
    '            End If
    '        Next
    '        MessageBox.Success("There are (" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
    '        PopulateGrid()
    '    Else
    '        MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
    '    End If
    'End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DAOwnerNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDAOwner", UserNo, CType(item, Integer))
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

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim fSaveRecord As Integer = saverecord()

        If fSaveRecord = 0 Then                        
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        ElseIf fSaveRecord = 1 Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Private Function SaveRecord() As Integer
        Dim employeeno As Integer = Generic.CheckDBNull(hifEmployeeNo.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim isreceiver As Integer = Generic.CheckDBNull(Me.chkIsReceiver.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim isapprover As Integer = Generic.CheckDBNull(Me.chkIsApprover.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim isevaluator As Integer = Generic.CheckDBNull(Me.chkIsEvaluator.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim isrecommendator As Integer = Generic.CheckDBNull(Me.chkIsRecommendator.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim isactive As Integer = Generic.CheckDBNull(Me.chkIsActive.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)

        If SQLHelper.ExecuteNonQuery("EDAOwner_WebSave", UserNo, Generic.ToInt(txtCode.Text), employeeno, isreceiver, isapprover, isevaluator, isrecommendator, isactive, Generic.ToInt(cboPayLocNo.SelectedValue), chkIsArchive.Checked) > 0 Then
            SaveRecord = 0
        Else
            SaveRecord = 1
        End If

    End Function
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub


    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDAOwner_WebOne", UserNo, id)
        Generic.PopulateData(Me, "pnlPopupMain", dt)
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkGo_Click(sender As Object, e As EventArgs)
        PopulateGrid()
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



