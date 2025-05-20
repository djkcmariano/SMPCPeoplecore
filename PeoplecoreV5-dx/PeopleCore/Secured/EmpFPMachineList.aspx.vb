Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_EmpFPMachineList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupRate", PayLocNo)

            Try
                cbofpactionno.DataSource = SQLHelper.ExecuteDataTable("EFPAction_WebLookup", UserNo, PayLocNo, 1)
                cbofpactionno.DataTextField = "tdesc"
                cbofpactionno.DataValueField = "tNo"
                cbofpactionno.DataBind()
            Catch ex As Exception

            End Try

        End If

        'Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        PopulateGrid()        
        PopuldateGridDeti(Generic.ToInt(ViewState("TransNo")))
    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Try
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EFPMachine_Web", UserNo, "", PayLocNo)
            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()
        Catch ex As Exception

        End Try
        
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupRate")                        
            mdlRate.Show()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            Dim dt As DataTable
            lnk = sender

            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)            
            dt = SQLHelper.ExecuteDataTable("EFPMachine_WebOne", UserNo, Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"FPMachineNo"})))
            Generic.PopulateData(Me, "pnlPopupRate", dt)
            mdlRate.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)        
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"FPMachineNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EFPMachine", UserNo, item)
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

    Private Function SaveRecord() As Boolean

        If SQLHelper.ExecuteNonQuery("EFPMachine_WebSave", UserNo, _
                                     Generic.ToInt(txtFPMachineNo.Text), _
                                     txtFPMachineCode.Text, _
                                     txtFPMachineDescription.Text, _
                                     txtIPAddress.Text, _
                                     txtRegCode.Text, _
                                     chkIsActive.Checked, _
                                     Generic.ToInt(txtPortNo.Text), _
                                     Generic.ToInt(cboBranchId.SelectedValue), _                                     
                                     txtKeyCode.Text, _
                                     PayLocNo, _
                                     chkIsFPTemplateSource.Checked) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    'Dim xPublicVar As New clsPublicVariable

    'Dim dscount As Double = 0
    'Dim _ds As New DataSet
    'Dim _dt As New DataTable
    'Dim xScript As String = ""
    'Dim rowno As Integer = 0
    'Dim FPMachineNo As Integer = 0

    'Dim IsClickMain As Integer = 0
    'Dim showFrm As New clsFormControls
    'Dim clsGeneric As New clsGenericClass

    'Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
    '    Try


    '        'Dim _dr As SqlClient.SqlDataReader

    '        _ds = sqlHelper.ExecuteDataset("EFPMachine_Web", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(Viewstate(xScript & "filter"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType), Session("xPayLocNo"))
    '        _dt = _ds.Tables(0)
    '        Dim dv As New Data.DataView(_dt)
    '        If SortExp <> "" Then
    '            Viewstate(xScript & "SortExp") = SortExp
    '        End If
    '        If sordir <> "" Then

    '            Viewstate(xScript & "sortdir") = sordir
    '        End If
    '        If _ds.Tables.Count > 0 Then
    '            If _ds.Tables(0).Rows.Count > 0 Then
    '                dscount = _ds.Tables(0).Rows.Count
    '                If Viewstate(xScript & "SortExp") <> "" Then
    '                    dv.Sort = Viewstate(xScript & "SortExp") + Viewstate(xScript & "sortdir")
    '                End If
    '            End If
    '        End If

    '        If IsMain Then
    '            ViewState(xScript & "PageNo") = 0
    '            Session(Left(xScript, Len(xScript) - 5)) = 0
    '        End If
    '        Me.grdMain.SelectedIndex = Generic.CheckDBNull(ViewState(Left(xScript, Len(xScript) - 5)), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '        Me.grdMain.PageIndex = CType(ViewState(xScript & "PageNo"), Integer)
    '        Me.grdMain.DataSource = dv
    '        Me.grdMain.DataBind()

    '    Catch ex As Exception

    '    End Try
    'End Sub




    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    '    xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("OnlineUserNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '    AccessRights.CheckUser(xPublicVar.xOnlineUseNo)
    '    AddHandler Filter1.lnkSearchClick, AddressOf lnkGo_Click

    '    FPMachineNo = Generic.CheckDBNull(Request.QueryString("transNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

    '    xScript = Request.ServerVariables("SCRIPT_NAME")
    '    xScript = Generic.GetPath(xScript)



    '    If Not IsPostBack Then
    '        PopulateGrid()

    '    End If

    '    Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    'End Sub
    'Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Viewstate(xScript & "PageNo") = 0
    '        ViewState(Left(xScript, Len(xScript) - 5)) = 0
    '        ViewState(xScript & "filter") = Generic.CheckDBNull(Filter1.SearchText.ToString, clsBase.clsBaseLibrary.enumObjectType.StrType)
    '        PopulateGrid()
    '    Catch ex As Exception
    '    End Try

    'End Sub

    'Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMain.PageIndexChanging
    '    Viewstate(xScript & "No") = 0
    '    Viewstate(xScript & "PageNo") = e.NewPageIndex
    '    ViewState(Left(xScript, Len(xScript) - 5)) = 0
    '    PopulateGrid()
    'End Sub

    'Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    'clsLog.PopulateEditPage(xPublicVar.xOnlineUseNo, Session("xMenutype"), "Sub2", clsArray.myFormname(3).xFormname, clsArray.myFormname(3).xTablename, clsArray.myFormname(3).xLevelNo, clsArray.myFormname(3).xMenuTitle)

    '    Dim lnk As New ImageButton
    '    Dim i As String = "", ii As String = ""

    '    lnk = sender
    '    Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
    '    rowno = gvrow.RowIndex

    '    i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
    '    Viewstate(xScript & "No") = i 'IdNo

    '    If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit) Then
    '        Dim _ds As New DataSet
    '        _ds = SQLHelper.ExecuteDataSet("EFPMachine_WebOne", xPublicVar.xOnlineUseNo, i)
    '        If _ds.Tables.Count > 0 Then
    '            If _ds.Tables(0).Rows.Count > 0 Then
    '                showFrm.clearFormControls_In_Popup(pnlPopupRate)
    '                showFrm.showFormControls_In_Popup(pnlPopupRate, _ds)
    '                showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupRate)
    '            End If
    '        End If

    '        mdlRate.Show()
    '    Else
    '        MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
    '    End If

    'End Sub


    'Protected Sub grdMain_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdMain.Sorting
    '    Try
    '        'PopulateGrid(e.SortExpression, GetSortDirection(e.SortExpression))


    '        Dim sortExpression = TryCast(ViewState("SortExpression"), String)
    '        Dim lastDirection = TryCast(ViewState("SortDirection"), String)
    '        Dim sortDirection As String = grdSort.GetSortDirection(e.SortExpression, sortExpression, lastDirection)

    '        ViewState("SortExpression") = sortExpression
    '        ViewState("SortDirection") = lastDirection

    '        PopulateGrid(False, e.SortExpression, sortDirection)

    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Protected Sub grdMain_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMain.RowCreated
    '    ' Use the RowType property to determine whether the 
    '    ' row being created is the header row. 
    '    ' If e.Row.RowType = DataControlRowType.Header Then
    '    ' Call the GetSortColumnIndex helper method to determine
    '    ' the index of the column being sorted.
    '    Dim sortColumnIndex As Integer = grdSort.GetSortColumnIndex(Me.grdMain, ViewState("SortExpression"))
    '    If sortColumnIndex > 0 Then
    '        ' Call the AddSortImage helper method to add
    '        ' a sort direction image to the appropriate
    '        ' column header. 
    '        grdSort.AddSortImage(sortColumnIndex, e.Row, ViewState("SortDirection"))
    '    End If
    '    'e.Row.CssClass = "highlight"
    '    ' End If
    'End Sub

    'Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '        showFrm.clearFormControls_In_Popup(pnlPopupRate)
    '        showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupRate)
    '        Me.txtCode.Text = "Autonumber"
    '        mdlRate.Show()
    '    Else
    '        MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
    '    End If
    'End Sub

    'Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim lbl As New Label, tcheck As New CheckBox
    '    Dim tcount As Integer, DeleteCount As Integer = 0

    '    If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowDelete) Then
    '        For tcount = 0 To Me.grdMain.Rows.Count - 1
    '            lbl = CType(grdMain.Rows(tcount).FindControl("lblId"), Label)
    '            tcheck = CType(grdMain.Rows(tcount).FindControl("txtIsSelect"), CheckBox)
    '            If tcheck.Checked = True Then

    '                Generic.DeleteRecordAudit("EFPMachine", xPublicVar.xOnlineUseNo, CType(lbl.Text, Integer))
    '                DeleteCount = DeleteCount + 1
    '            End If
    '        Next
    '        MessageBox.Success("There are " & DeleteCount.ToString & MessageTemplate.SuccessDelete, Me)
    '        PopulateGrid()
    '    Else
    '        MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
    '    End If
    'End Sub

    ''Submit record
    'Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim fSaveRecord As Integer = SaveRecord()
    '    If fSaveRecord = 0 Then
    '        MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '        PopulateGrid()
    '    ElseIf fSaveRecord = 1 Then
    '        MessageBox.Warning(MessageTemplate.ErrorSave, Me)
    '    End If
    'End Sub

    'Private Function SaveRecord() As Integer

    '    If sqlHelper.ExecuteNonQuery("EFPMachine_WebSave", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(txtFPMachineNo.Text, clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Me.txtFPMachineCode.Text, clsBase.clsBaseLibrary.enumObjectType.StrType), Generic.CheckDBNull(Me.txtFPMachineDescription.Text, clsBase.clsBaseLibrary.enumObjectType.StrType), Generic.CheckDBNull(Me.txtIPAddress.Text, clsBase.clsBaseLibrary.enumObjectType.StrType), Generic.CheckDBNull(Me.txtRegCode.Text, clsBase.clsBaseLibrary.enumObjectType.StrType), Generic.CheckDBNull(Me.txtIsActive.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Me.txtPortNo.Text, clsBase.clsBaseLibrary.enumObjectType.StrType), Generic.CheckDBNull(Me.cboBranchId.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType), "", Session("xPayLocNo")) > 0 Then
    '        SaveRecord = 0
    '    Else
    '        SaveRecord = 1

    '    End If
    'End Function

#Region "Details"

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"FPMachineNo", "FPMachineDescription"})
            ViewState("TransNo") = obj(0)
            lbl.Text = obj(1)
            PopuldateGridDeti(Generic.ToInt(ViewState("TransNo")))
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopuldateGridDeti(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EFPMachineDeti_Web", UserNo, id)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Protected Sub lnkAddDeti_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            If Generic.ToInt(ViewState("TransNo")) > 0 Then
                Generic.ClearControls(Me, "Panel1")
                ModalPopupExtender1.Show()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If

    End Sub

    Protected Sub lnkDeleteDeti_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"FPMachineDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EFPMachineDeti", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopuldateGridDeti(Generic.ToInt(ViewState("TransNo")))
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If
    End Sub

    Protected Sub lnkSaveDeti_Click(sender As Object, e As EventArgs)
        If SQLHelper.ExecuteNonQuery("EFPMachineDeti_WebSave", UserNo, Generic.ToInt(txtCodeDeti.Text), Generic.ToInt(ViewState("TransNo")), Generic.ToInt(cbofpactionno.SelectedValue), txtStartDate.Text, txtEndDate.Text) > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopuldateGridDeti(Generic.ToInt(ViewState("TransNo")))
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub cbofpactionno_TextChanged(sender As Object, e As EventArgs)
        If cbofpactionno.SelectedValue = 5 Then
            txtStartDate.Enabled = True
            txtEndDate.Enabled = True
            txtStartDate.CssClass = "form-control required"
            txtEndDate.CssClass = "form-control required"
        Else
            txtStartDate.Enabled = False
            txtStartDate.Text = ""
            txtEndDate.Enabled = False
            txtEndDate.Text = ""
            txtStartDate.CssClass = "form-control"
            txtEndDate.CssClass = "form-control"
        End If
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub grdDetl_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdDetl.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdDetl.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

#End Region


End Class







