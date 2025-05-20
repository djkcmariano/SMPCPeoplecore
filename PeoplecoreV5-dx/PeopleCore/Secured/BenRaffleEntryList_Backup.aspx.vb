Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_BenRaffleEntryList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    'Protected Overrides Sub OnInit(e As EventArgs)
    '    MyBase.OnInit(e)
    '    PopulateFilter()
    'End Sub

    Private Sub PopulateFilter()
        'Try
        '    Dim dt As DataTable = SQLHelper.ExecuteDataTable("EFilterBuilder")
        '    Dim name As String
        '    For Each row As DataRow In dt.Rows
        '        name = Generic.ToStr(row("DisplayName"))
        '        Select Case Generic.ToStr(row("type"))
        '            Case "txt"
        '                Dim col As FilterControlTextColumn = TryCast(ASPxFilterControl1.Columns(name), FilterControlTextColumn)
        '                If col Is Nothing Then
        '                    col = New FilterControlTextColumn()
        '                    If Generic.ToStr(row("datatype")) = "int" Then
        '                        col.ColumnType = FilterControlColumnType.Integer
        '                    Else
        '                        col.ColumnType = FilterControlColumnType.String
        '                    End If
        '                    col.DisplayName = name
        '                    col.PropertyName = Generic.ToStr(row("PropertyName"))
        '                    ASPxFilterControl1.Columns.Add(col)
        '                End If
        '            Case "cbo"
        '                Dim col As FilterControlComboBoxColumn = TryCast(ASPxFilterControl1.Columns(name), FilterControlComboBoxColumn)
        '                If col Is Nothing Then
        '                    col = New FilterControlComboBoxColumn()
        '                    If Generic.ToStr(row("datatype")) = "int" Then
        '                        col.ColumnType = FilterControlColumnType.Integer
        '                    Else
        '                        col.ColumnType = FilterControlColumnType.String
        '                    End If
        '                    col.DisplayName = name
        '                    col.PropertyName = Generic.ToStr(row("PropertyName"))
        '                    col.PropertiesComboBox.DropDownStyle = DropDownStyle.DropDown
        '                    col.PropertiesComboBox.ValueType = GetType(Integer)
        '                    ASPxFilterControl1.Columns.Add(col)
        '                End If
        '                col.PropertiesComboBox.TextField = "desc"
        '                col.PropertiesComboBox.ValueField = "id"
        '                col.PropertiesComboBox.DataSource = SQLHelper.ExecuteDataTable("EColumn_Lookup", -99, Generic.ToStr(row("Table")))
        '        End Select
        '    Next
        'Catch ex As Exception
        'End Try
    End Sub





#Region "********Main********"
    Protected Sub PopulateGrid(Optional IsMain As Boolean = False)
        Try

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ERaffleEntry_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
            grdMain.DataSource = dt
            grdMain.DataBind()

            If ViewState("TransNo") = 0 Or IsMain = True Then
                Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"RaffleEntryNo", "Code"})
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
            dt = SQLHelper.ExecuteDataTable("ERaffleEntry_WebOne", UserNo, id)
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

        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupAppend", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Try
                cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 4)
                cboTabNo.DataTextField = "tDesc"
                cboTabNo.DataValueField = "tno"
                cboTabNo.DataBind()
            Catch ex As Exception
            End Try

            Try
                cboPayIncomeTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayIncomeType_WebLookup", UserNo, PayLocNo)
                cboPayIncomeTypeNo.DataValueField = "tNo"
                cboPayIncomeTypeNo.DataTextField = "tDesc"
                cboPayIncomeTypeNo.DataBind()
            Catch ex As Exception
            End Try
        End If

        PopulateGrid()

        If Generic.ToInt(cboTabNo.SelectedValue) = 1 Then
            lnkAdd.Visible = False
            lnkAppend.Visible = False
            lnkDelete.Visible = False
            lnkDeleteDetl.Visible = False
            lnkSave.Visible = False
            lnkSaveAppend.Visible = False
            lnkPost.Visible = False
        Else
            lnkAdd.Visible = True
            lnkAppend.Visible = True
            lnkDelete.Visible = True
            lnkDeleteDetl.Visible = True
            lnkSave.Visible = True
            lnkSaveAppend.Visible = True
            lnkPost.Visible = True
        End If

        'PopulateGridDetl()

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            Generic.EnableControls(Me, "pnlPopupMain", True)
            lnkSave.Enabled = True
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"RaffleEntryNo"})))
            mdlMain.Show()

            If Generic.ToInt(cboTabNo.SelectedValue) = 1 Then
                Generic.EnableControls(Me, "pnlPopupMain", False)
            Else
                Generic.EnableControls(Me, "pnlPopupMain", True)
            End If


        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"RaffleEntryNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("ERaffleEntry_WebPost", UserNo, item, PayLocNo) > 0 Then
                    Count = Count + 1
                End If
                i = i + 1
            Next

            If i > 0 Then
                ViewState("TransNo") = 0
                PopulateGrid()
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccesPost, Me)
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"RaffleEntryNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("ERaffleEntryDeti", UserNo, "RaffleEntryNo", item)
                Generic.DeleteRecordAudit("ERaffleEntry", UserNo, item)
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
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"RaffleEntryNo", "Code"})
        ViewState("TransNo") = obj(0)
        lbl.Text = obj(1)
        PopulateGridDetl()
    End Sub



    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid(True)
    End Sub


    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim RaffleEntryNo As Integer = Generic.ToInt(txtRaffleEntryNo.Text)
        Dim RaffleEntryCode As String = Generic.ToStr(txtRaffleEntryCode.Text)
        Dim RaffleEntryDesc As String = Generic.ToStr(txtRaffleEntryDesc.Text)
        Dim RaffleEntryTypeNo As Integer = Generic.ToInt(cboRaffleEntryTypeNo.SelectedValue)
        Dim RaffleDate As String = Generic.ToStr(txtRaffleDate.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)
        Dim IsSuspended As Boolean = 0 'Generic.ToBol(txtIsSuspended.Checked)

        '//validate start here
        'Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        'Dim dtx As New DataTable
        'dtx = SQLHelper.ExecuteDataTable("EDTROT_WebValidate", UserNo, 0, 0, DTRDate, OvtIn1, OvtOut1, OvtIn2, OvtOut2, OTBreak, IsForCompensatory, IsOnCall, Description, CostCenterNo, ApprovalStatNo, PayLocNo, ComponentNo)

        'For Each rowx As DataRow In dtx.Rows
        '    invalid = Generic.ToBol(rowx("tProceed"))
        '    messagedialog = Generic.ToStr(rowx("xMessage"))
        '    alerttype = Generic.ToStr(rowx("AlertType"))
        'Next

        'If invalid = True Then
        '    MessageBox.Alert(messagedialog, alerttype, Me)
        '    mdlMain.Show()
        '    Exit Sub
        'End If

        If SQLHelper.ExecuteNonQuery("ERaffleEntry_WebSave", UserNo, RaffleEntryNo, RaffleEntryCode, RaffleEntryDesc, RaffleEntryTypeNo, RaffleDate, Remarks, IsSuspended, PayLocNo, Generic.ToInt(cboPayIncomeTypeNo.SelectedValue)) > 0 Then
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
            dt = SQLHelper.ExecuteDataTable("ERaffleEntryDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"RaffleEntryDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ERaffleEntryDeti", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGridDetl()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
            
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


    'Protected Sub grdDetl_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs)
    '    If e.ButtonType <> ColumnCommandButtonType.SelectCheckbox Then
    '        Return
    '    End If
    '    Dim rowEnabled As Boolean = getRowEnabledStatus(e.VisibleIndex)
    '    e.Enabled = rowEnabled
    'End Sub
    'Private Function getRowEnabledStatus(ByVal VisibleIndex As Integer) As Boolean
    '    Dim value As Boolean = Generic.ToInt(grdDetl.GetRowValues(VisibleIndex, "IsEnabled"))
    '    If value = True Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function
    'Protected Sub cbCheckAll_Init(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim cb As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)
    '    cb.ClientSideEvents.CheckedChanged = String.Format("cbCheckAll_CheckedChanged")
    '    cb.Checked = False
    '    Dim count As Integer = 0
    '    Dim startIndex As Integer = grdDetl.PageIndex * grdDetl.SettingsPager.PageSize
    '    Dim endIndex As Integer = Math.Min(grdDetl.VisibleRowCount, startIndex + grdDetl.SettingsPager.PageSize)

    '    For i As Integer = startIndex To endIndex - 1
    '        If grdDetl.Selection.IsRowSelected(i) Then
    '            count = count + 1
    '        End If
    '    Next i

    '    If count > 0 Then
    '        cb.Checked = True
    '    End If

    'End Sub
    'Protected Sub gridDetl_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
    '    Boolean.TryParse(e.Parameters, False)

    '    Dim startIndex As Integer = grdDetl.PageIndex * grdDetl.SettingsPager.PageSize
    '    Dim endIndex As Integer = Math.Min(grdDetl.VisibleRowCount, startIndex + grdDetl.SettingsPager.PageSize)
    '    For i As Integer = startIndex To endIndex - 1
    '        Dim rowEnabled As Boolean = getRowEnabledStatus(i)
    '        If rowEnabled AndAlso e.Parameters = "true" Then
    '            grdDetl.Selection.SelectRow(i)
    '        Else
    '            grdDetl.Selection.UnselectRow(i)
    '        End If
    '    Next i

    'End Sub

#End Region

#Region "********Append*******"

    '    Protected Sub lnkAppend_Click(sender As Object, e As EventArgs)
    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '            If Generic.ToInt(ViewState("TransNo")) > 0 Then
    '                Generic.ClearControls(Me, "pnlPopupAppend")
    '                cboFilteredbyNo.Text = 1
    '                drpAC.CompletionSetCount = 1
    '                mdlAppend.Show()
    '            Else
    '                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
    '            End If
    '        Else
    '            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
    '        End If
    '    End Sub

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

    '    Protected Sub lnkSaveAppend_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '            Dim Retval As Boolean = False
    '            Dim FilteredbyNo As Integer = Generic.ToInt(Me.cboFilteredbyNo.SelectedValue)
    '            Dim FilterValue As Integer = Generic.ToInt(Me.hiffilterbyid.Value)

    '            If SQLHelper.ExecuteNonQuery("ERaffleEntryDeti_WebAppend", UserNo, Generic.ToInt(ViewState("TransNo")), FilteredbyNo, FilterValue) > 0 Then
    '                Retval = True
    '            Else
    '                Retval = False
    '            End If

    '            If Retval Then
    '                PopulateGridDetl()
    '                MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '            Else
    '                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
    '            End If
    '        Else
    '            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
    '        End If

    '    End Sub

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

    Protected Sub lnkAppend_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            If Generic.ToInt(ViewState("TransNo")) > 0 Then
                'ASPxFilterControl1.ClearFilter()
                Generic.ClearControls(Me, "pnlPopupAppend")
                mdlAppend.Show()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSaveAppend_Click(sender As Object, e As EventArgs)
        Try
            'Dim dt As DataTable
            'Dim sortDirection As String = "", sortExpression As String = ""

            'dt = SQLHelper.ExecuteDataSet("EEmployee_WebFilter", UserNo, PayLocNo).Tables(0)
            'Dim dv As DataView = dt.DefaultView
            'dv.RowFilter = ASPxFilterControl1.GetFilterExpressionForDataSet
            'If ASPxFilterControl1.GetFilterExpressionForDataSet = "" Then
            '    dt = dt.Rows.Cast(Of DataRow)().Take(100).CopyToDataTable()
            '    dv = dt.DefaultView
            'End If
            'Dim dt_temp = dv.ToTable()
            ''dv = dt_temp.DefaultView

            ''grdMain.DataSource = dv
            ''grdMain.DataBind()
            'Dim x As Integer = 0
            'For Each row As DataRow In dt_temp.Rows
            '    x = x + Generic.ToInt(SQLHelper.ExecuteNonQuery("ERaffleEntryDeti_WebSave", UserNo, PayLocNo, Generic.ToInt(row("EmployeeLNo")), Generic.ToInt(ViewState("TransNo")), Generic.ToDbl(txtAmount.Text)))
            'Next

            'If x > 0 Then
            '    MessageBox.Success("(" + x.ToString + ") " + MessageTemplate.SuccessSave, Me)
            '    PopulateGrid()
            'Else
            '    MessageBox.Information("No records found.", Me)
            'End If

            Dim xSQLHelper As New clsBase.SQLHelper
            Dim xbase As New clsBase.clsBaseLibrary

            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
                Dim Retval As Boolean = False
                Dim FilteredbyNo As Integer = Generic.ToInt(Me.cboFilteredbyNo.SelectedValue)
                Dim FilterValue As Integer = Generic.ToInt(Me.hiffilterbyid.Value)

                If xSQLHelper.ExecuteNonQuery(SQLHelper.ConSTR, "ERaffleEntryDeti_WebAppend", UserNo, Generic.ToInt(ViewState("TransNo")), FilteredbyNo, FilterValue, Generic.ToDbl(txtAmount.Text)) > 0 Then
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

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            'PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"RaffleEntryNo"})))
            Dim obj() As Object = container.Grid.GetRowValues(container.VisibleIndex, New String() {"RaffleEntryDetiNo", "FullName", "Amount"})

            hifRaffleEntryDetiNo.Value = Generic.ToInt(obj(0))
            txtFullname.Text = Generic.ToStr(obj(1))
            txtAmount2.Text = Generic.ToDbl(obj(2))
            ModalPopupExtender1.Show()

            If Generic.ToInt(cboTabNo.SelectedValue) = 1 Then
                Generic.EnableControls(Me, "pnlPopupMain", False)
            Else
                Generic.EnableControls(Me, "pnlPopupMain", True)
            End If


        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkSave2_Click(sender As Object, e As EventArgs)

        If SQLHelper.ExecuteNonQuery("ERaffleEntryDeti_WebSave", UserNo, Generic.ToInt(hifRaffleEntryDetiNo.Value), Generic.ToInt(txtAmount2.Text)) > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

#End Region




End Class

