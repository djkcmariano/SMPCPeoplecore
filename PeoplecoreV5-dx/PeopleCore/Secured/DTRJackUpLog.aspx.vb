Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports Microsoft.VisualBasic.FileIO

Partial Class Secured_DTRJackUpLog
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("pr_EJackUp_Web", UserNo, PayLocNo) 'change sp to populate list of jack up
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub PopulateData(ByVal id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRJackUpLog_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        PopulateGrid()
        PopulateGridDetl()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 12)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmpName", "EmpCode", "PayDate"})
        lblDetl.Text = obj(0)
        ViewState("EmpCode") = obj(1)
        ViewState("PayDate") = obj(2)
        PopulateGridDetl()
    End Sub

    Protected Sub lnkDeleteDetl_Click(ByVal sender As Object, ByVal e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"RecNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteDataSet("EDTRJackUpLog_WebSave", UserNo, item, 0, Today.Date(), Today.Date(), 0, 0, 0, 0, 0, 0, 0, 1, 0)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridDetl()
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Sub PopulateGridDetl()
        Try
            Dim _dt As DataTable

            _dt = SQLHelper.ExecuteDataTable("EDTRJackUpDetl", UserNo, Generic.ToStr(ViewState("EmpCode")), Generic.ToStr(ViewState("PayDate")))
            grdDetl.DataSource = _dt
            grdDetl.DataBind()

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.EnableControls(Me, "pnlPopupDetl", True)
            lnkSave.Enabled = True
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

        Try
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"RecNo"})))
            lnkSave.Enabled = True
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

        Try
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRLogNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTRLog", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub cboDtrLogReason_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        mdlDetl.Show()
    End Sub

    Protected Sub lnkExport_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkExportDetl_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub
    Protected Sub lnkAddMass_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/DTRLogMassList.aspx?transNo=" & 0 & "&tModify=True")
        Else
            MessageBox.Critical(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkUpload_Click(ByVal sender As Object, ByVal e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel3")
            ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub


    Protected Sub lnkSave2_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String = "", IsValidDAT As Boolean = False, IsValidCSV As Boolean = False
        Try
            Dim uploadFolder As String = Request.PhysicalApplicationPath + "secured\documents\"
            If fuFilename.HasFile Then
                Dim NewFileName As String = Guid.NewGuid().ToString()
                FileExt = Path.GetExtension(fuFilename.PostedFile.FileName)
                Filename = Path.GetFileNameWithoutExtension(fuFilename.PostedFile.FileName)
                If FileExt = ".dat" Then
                    IsValidDAT = True
                ElseIf (FileExt = ".csv" Or FileExt = ".txt") Then
                    IsValidCSV = True
                End If

                fuFilename.SaveAs(Convert.ToString(uploadFolder & NewFileName) & FileExt)
                ActualPath = Convert.ToString(uploadFolder & NewFileName) & FileExt
            End If
        Catch ex As Exception
            MessageBox.Warning("Error uploading file.", Me)
        End Try

        'Read file and insert record to database
        If IsValidDAT Then
            Try
                Dim list As New List(Of DATFormat)
                Using parser As New TextFieldParser(ActualPath)
                    parser.TextFieldType = FieldType.Delimited
                    parser.Delimiters = New String() {vbTab}
                    Dim fields As String()
                    While Not parser.EndOfData
                        fields = parser.ReadFields()
                        list.Add(New DATFormat() With
                                             {
                                                 .FPID = Generic.ToInt(fields(0)),
                                                 .DTRDate = Generic.ToStr(fields(1)),
                                                 .MachineNo = Generic.ToStr(fields(2)),
                                                 .InOutMode = Generic.ToStr(fields(3))
                                             })
                    End While
                    If list.Count > 0 Then
                        Dim count As Integer = 0
                        For Each row In list
                            If SQLHelper.ExecuteNonQuery("EFPDTR_WebSave_Manual", UserNo, row.FPID, row.DTRDate, row.MachineNo, row.InOutMode, PayLocNo) > 0 Then
                                count = count + 1
                            End If
                        Next
                        PopulateGrid()
                        MessageBox.Success(count.ToString & " transaction(s) successfully uploaded.", Me)
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End Try
        ElseIf IsValidCSV Then
            Try
                Dim Description As String = Generic.ToStr(Replace(txtDescription.Text, "'", ""))
                Dim list As New List(Of CSVFormat)
                Using parser As New TextFieldParser(ActualPath)
                    parser.TextFieldType = FieldType.Delimited
                    parser.Delimiters = New String() {"|"}
                    Dim fields As String()
                    While Not parser.EndOfData
                        fields = parser.ReadFields()
                        If Len(Generic.ToInt(fields(0))) > 0 Then
                            list.Add(New CSVFormat() With
                                                 {
                                                     .idnumber = Generic.ToInt(fields(0)),
                                                     .DTRDate = Generic.ToStr(fields(1)),
                                                     .phase_code = Generic.ToStr(fields(2)),
                                                     .desig_code = Generic.ToInt(fields(3)),
                                                     .vessel_id = Generic.ToInt(fields(4)),
                                                     .num_hrs = Generic.ToDec(fields(5)),
                                                     .time_type = Generic.ToInt(fields(6)),
                                                     .total_reg_hrs = Generic.ToDec(fields(7)),
                                                     .total_nd_hrs = Generic.ToDec(fields(8)),
                                                     .total_ot_hrs = Generic.ToDec(fields(9)),
                                                     .total_ndo_hrs = Generic.ToDec(fields(10)),
                                                     .paydate = Generic.ToStr(fields(11)),
                                                     .created_by = Generic.ToStr(fields(12)),
                                                     .created_date = Generic.ToStr(fields(13))
                                                 })
                        End If
                    End While
                    If list.Count > 0 Then
                        Dim count As Integer = 0
                        For Each row In list
                            If SQLHelper.ExecuteNonQuery("EJackupUpload", UserNo, row.idnumber, row.DTRDate, row.phase_code, row.desig_code,
                                                         row.vessel_id, row.num_hrs, row.time_type, row.total_reg_hrs, row.total_nd_hrs, row.total_ot_hrs,
                                                         row.total_ndo_hrs, row.paydate, row.created_by, row.created_date, Description) > 0 Then
                                count = count + 1
                            End If
                        Next
                        PopulateGrid()
                        MessageBox.Success(count.ToString & " transaction(s) successfully uploaded.", Me)
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End Try
        Else
            MessageBox.Warning("Invalid file type.", Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim RetVal As Boolean = False
        Dim RecNo As Integer = Generic.ToInt(txtRecNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim DTRDate As String = Generic.ToStr(txtDTRDate.Text)
        Dim DTRDateTo As String = Generic.ToStr(txtDTRDateTo.Text)
        Dim VesselNo As Integer = 0
        Dim PositionNo As Integer = 0
        Dim RegHrs As Decimal = Generic.ToDec(txtRegHrs.Text)
        Dim OtHrs As Decimal = Generic.ToDec(txtOTHrs.Text)
        Dim NdHrs As Decimal = Generic.ToDec(txtNdHrs.Text)
        Dim NdoHrs As Decimal = Generic.ToDec(txtNDOHrs.Text)
        Dim ComponentNo As Integer = 1 'Administrator
        Dim Pk As Integer = Generic.ToInt(hifVesselNo.Value)
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EDTRJackUpLog_WebValidate", UserNo, RecNo, EmployeeNo, DTRDate, DTRDateTo, VesselNo, PositionNo, RegHrs, OtHrs, NdHrs, NdoHrs, ComponentNo, Pk)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlDetl.Show()
            Exit Sub
        End If
        Dim ip As String = IPSecurity.Get_IPSec
        Dim hostname As String = IPSecurity.Get_hostName

        dt = SQLHelper.ExecuteDataTable("EDTRJackUpLog_WebSave", UserNo, RecNo, EmployeeNo, DTRDate, DTRDateTo, VesselNo, PositionNo, RegHrs, OtHrs, NdHrs, NdoHrs, ComponentNo, 0, Pk)
        For Each row As DataRow In dt.Rows
            RetVal = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                RetVal = False
            End If

        Next
        If RetVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If RetVal = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            If RecNo <> 0 Then
                'PopulateGridDetl()
                PopulateGrid()
            End If
        End If

    End Sub

    Protected Sub lnkViewLogs_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
            If txtFullName.Text = "" Then
                EmployeeNo = 0
            End If
            dtx = SQLHelper.ExecuteDataTable("EDTRLog_WebViewLogs", EmployeeNo, Generic.ToStr(txtDTRDate.Text))

            For Each rowx As DataRow In dtx.Rows
                messagedialog = Generic.ToStr(rowx("Retval"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlDetl.Show()

        Catch ex As Exception

        End Try
    End Sub

#Region "********Detail Check All********"

    Protected Sub grdMain_CommandButtonInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs)
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
        Dim endIndex As Integer = Math.Min(grdDetl.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)
        For i As Integer = startIndex To endIndex - 1
            Dim rowEnabled As Boolean = getRowEnabledStatus(i)
            If rowEnabled AndAlso e.Parameters = "true" Then
                grdDetl.Selection.SelectRow(i)
            Else
                grdDetl.Selection.UnselectRow(i)
            End If
        Next i

    End Sub


    'Protected Sub gridMain_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
    '    Boolean.TryParse(e.Parameters, False)

    '    Dim startIndex As Integer = grdMain.PageIndex * grdMain.SettingsPager.PageSize
    '    Dim endIndex As Integer = Math.Min(grdMain.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)
    '    For i As Integer = startIndex To endIndex - 1
    '        Dim rowEnabled As Boolean = getRowEnabledStatus(i)
    '        If rowEnabled AndAlso e.Parameters = "true" Then
    '            grdMain.Selection.SelectRow(i)
    '        Else
    '            grdMain.Selection.UnselectRow(i)
    '        End If
    '    Next i

    'End Sub
#End Region

    Protected Sub ASPxGridViewExporter_RenderBrick(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewExportRenderingEventArgs) Handles grdExport.RenderBrick
        Dim dataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
        'If e.RowType = GridViewRowType.Data AndAlso dataColumn IsNot Nothing Then
        '    Select Case dataColumn.FieldName
        '        Case "AbsHrs"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<span>", "")
        '            e.TextValue = e.TextValue.ToString.Replace("</span>", "")
        '        Case "Late"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<span>", "")
        '            e.TextValue = e.TextValue.ToString.Replace("</span>", "")
        '        Case "Under"
        '            e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
        '            e.TextValue = e.TextValue.ToString.Replace("<span>", "")
        '            e.TextValue = e.TextValue.ToString.Replace("</span>", "")
        '    End Select

        'End If
        If e.RowType = GridViewRowType.Header AndAlso dataColumn IsNot Nothing Then
            e.Text = e.Text.Replace("<br/>", " ")
            e.Text = e.Text.Replace("<br />", " ")
            e.Text = e.Text.Replace("<br>", " ")
            e.Text = e.Text.Replace("<center>", "")
            e.Text = e.Text.Replace("</center>", "")
        End If

    End Sub

End Class
Partial Class DATFormat

    Private _FPID As Integer
    Public Property FPID As Integer
        Get
            Return _FPID
        End Get
        Set(ByVal value As Integer)
            _FPID = value
        End Set
    End Property

    Private _DTRDate As String
    Public Property DTRDate As String
        Get
            Return _DTRDate
        End Get
        Set(ByVal value As String)
            _DTRDate = value
        End Set
    End Property

    Private _MachineNo As Integer
    Public Property MachineNo As Integer
        Get
            Return _MachineNo
        End Get
        Set(ByVal value As Integer)
            _MachineNo = value
        End Set
    End Property

    Private _InOutMode As Integer
    Public Property InOutMode As Integer
        Get
            Return _InOutMode
        End Get
        Set(ByVal value As Integer)
            _InOutMode = value
        End Set
    End Property
End Class

Partial Class CSVFormat

    Private _idnumber As String
    Public Property idnumber As String
        Get
            Return _idnumber
        End Get
        Set(ByVal value As String)
            _idnumber = value
        End Set
    End Property

    Private _DTRDate As Date
    Public Property DTRDate As Date
        Get
            Return _DTRDate
        End Get
        Set(ByVal value As Date)
            _DTRDate = value
        End Set
    End Property


    Private _phase_code As String
    Public Property phase_code As Integer
        Get
            Return _phase_code
        End Get
        Set(ByVal value As Integer)
            _phase_code = value
        End Set
    End Property

    Private _desig_code As String
    Public Property desig_code As String
        Get
            Return _desig_code
        End Get
        Set(ByVal value As String)
            _desig_code = value
        End Set
    End Property

    Private _vessel_id As Integer
    Public Property vessel_id As Integer
        Get
            Return _vessel_id
        End Get
        Set(ByVal value As Integer)
            _vessel_id = value
        End Set
    End Property

    Private _num_hrs As Decimal
    Public Property num_hrs As Decimal
        Get
            Return _num_hrs
        End Get
        Set(ByVal value As Decimal)
            _num_hrs = value
        End Set
    End Property

    Private _time_type As Integer
    Public Property time_type As Integer
        Get
            Return _time_type
        End Get
        Set(ByVal value As Integer)
            _time_type = value
        End Set
    End Property

    Private _total_reg_hrs As Decimal
    Public Property total_reg_hrs As Decimal
        Get
            Return _total_reg_hrs
        End Get
        Set(ByVal value As Decimal)
            _total_reg_hrs = value
        End Set
    End Property

    Private _total_nd_hrs As Decimal
    Public Property total_nd_hrs As Decimal
        Get
            Return _total_nd_hrs
        End Get
        Set(ByVal value As Decimal)
            _total_nd_hrs = value
        End Set
    End Property

    Private _total_ot_hrs As Decimal
    Public Property total_ot_hrs As Decimal
        Get
            Return _total_ot_hrs
        End Get
        Set(ByVal value As Decimal)
            _total_ot_hrs = value
        End Set
    End Property

    Private _total_ndo_hrs As Decimal
    Public Property total_ndo_hrs As Decimal
        Get
            Return _total_ndo_hrs
        End Get
        Set(ByVal value As Decimal)
            _total_ndo_hrs = value
        End Set
    End Property

    Private _paydate As Date
    Public Property paydate As Date
        Get
            Return _paydate
        End Get
        Set(ByVal value As Date)
            _paydate = value
        End Set
    End Property

    Private _created_by As String
    Public Property created_by As String
        Get
            Return _created_by
        End Get
        Set(ByVal value As String)
            _created_by = value
        End Set
    End Property

    Private _created_date As Date
    Public Property created_date As Date
        Get
            Return _created_date
        End Get
        Set(ByVal value As Date)
            _created_date = value
        End Set
    End Property
End Class




