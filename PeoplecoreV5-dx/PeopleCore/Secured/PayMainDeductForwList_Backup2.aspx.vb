Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports Microsoft.VisualBasic.FileIO
Imports System.IO

Partial Class Secured_PayMainDeductForwList
    Inherits System.Web.UI.Page


    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        Try
            Dim tstatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
            If tstatus = 0 Then
                tstatus = 1
            End If

            'Dim _dt As DataTable
            '_dt = SQLHelper.ExecuteDataTable("EPayMainDeductForw_Web", UserNo, tstatus, PayLocNo)
            'Me.grdMain.DataSource = _dt
            'Me.grdMain.DataBind()

            Dim FilterByNo As Integer = 0, FilterById As Integer = 0
            Dim xYear As Integer = 0
            Dim xMonth As Integer = 0
            Dim PayDeductTypeNo As Short = 0
            Dim BatchId As Integer = 0

            If IsNumeric(Me.cboFilteredbyNo.SelectedValue) = True Then
                FilterByNo = Generic.ToInt(Me.cboFilteredbyNo.SelectedValue)
            End If
            If IsNumeric(Me.hiffilterbyid.Value) = True Then
                FilterById = Generic.ToInt(Me.hiffilterbyid.Value)
            End If
            If IsNumeric(Me.txtApplicableYear3.Text) = True Then
                xYear = Generic.ToInt(Me.txtApplicableYear3.Text)
            End If
            If IsNumeric(Me.cboApplicableMonth3.SelectedValue) = True Then
                xMonth = Generic.ToInt(Me.cboApplicableMonth3.SelectedValue)
            End If
            If IsNumeric(Me.cboPayDeductTypeNo3.SelectedValue) = True Then
                PayDeductTypeNo = Generic.ToInt(Me.cboPayDeductTypeNo3.SelectedValue)
            End If
            If IsNumeric(Me.cboPayMainDeductForwIdNo.SelectedValue) = True Then
                BatchId = Generic.ToInt(Me.cboPayMainDeductForwIdNo.SelectedValue)
            End If


            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayMainDeductForw_WebFiltered", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue), Filter1.SearchText, FilterByNo, FilterById, xMonth, xYear, Generic.ToStr(Me.txtDateFrom.Text), Generic.ToStr(Me.txtDateTo.Text), Generic.ToInt(PayDeductTypeNo), BatchId)
            grdMain.DataSource = dt
            grdMain.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 2)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception
        End Try

        Try
            cboPayDeductTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayDeductType_WebLookup", UserNo, PayLocNo)
            cboPayDeductTypeNo.DataValueField = "tNo"
            cboPayDeductTypeNo.DataTextField = "tDesc"
            cboPayDeductTypeNo.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboPayDeductTypeNo2.DataSource = SQLHelper.ExecuteDataSet("EPayDeductType_WebLookup", UserNo, PayLocNo)
            cboPayDeductTypeNo2.DataValueField = "tNo"
            cboPayDeductTypeNo2.DataTextField = "tDesc"
            cboPayDeductTypeNo2.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboPayDeductTypeNo3.DataSource = SQLHelper.ExecuteDataSet("EPayDeductType_WebLookup", UserNo, PayLocNo)
            cboPayDeductTypeNo3.DataValueField = "tNo"
            cboPayDeductTypeNo3.DataTextField = "tDesc"
            cboPayDeductTypeNo3.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboPayScheduleNo.DataSource = SQLHelper.ExecuteDataSet("EPaySchedule_WebLookup", UserNo, 0) 'Generic.ToInt(cboPayTypeNo.SelectedValue)
            cboPayScheduleNo.DataTextField = "tdesc"
            cboPayScheduleNo.DataValueField = "tno"
            cboPayScheduleNo.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboPaySchedule2No.DataSource = SQLHelper.ExecuteDataSet("EPaySchedule_WebLookup", UserNo, 0) 'Generic.ToInt(cboPayTypeNo.SelectedValue)
            cboPaySchedule2No.DataTextField = "tdesc"
            cboPaySchedule2No.DataValueField = "tno"
            cboPaySchedule2No.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "Panel3", PayLocNo)
            PopulateDropDown()

            Try
                cboFilteredbyNo.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EFilteredByAll", PayLocNo, "", "")
                cboFilteredbyNo.DataTextField = "tdesc"
                cboFilteredbyNo.DataValueField = "tNo"
                cboFilteredbyNo.DataBind()
            Catch ex As Exception

            End Try

            Try
                cboApplicableMonth3.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EMonth", PayLocNo, "", "")
                cboApplicableMonth3.DataTextField = "tdesc"
                cboApplicableMonth3.DataValueField = "tNo"
                cboApplicableMonth3.DataBind()
            Catch ex As Exception

            End Try

            Try
                cboPayMainDeductForwIdNo.DataSource = SQLHelper.ExecuteDataTable("EPayMainDeductForwId_WebLookup", UserNo, Me.cboTabNo.SelectedValue)
                cboPayMainDeductForwIdNo.DataTextField = "tdesc"
                cboPayMainDeductForwIdNo.DataValueField = "tNo"
                cboPayMainDeductForwIdNo.DataBind()
            Catch ex As Exception

            End Try

            If Generic.ToInt(cboFilteredbyNo.SelectedValue) = 0 Then
                cboFilteredbyNo.Text = "1"
                drpAC.CompletionSetCount = 1
            End If

        End If

        Try
            If Me.cboTabNo.SelectedValue = 1 Then
                Me.lnkDeleteBatckFile.Visible = True
            Else
                Me.lnkDeleteBatckFile.Visible = False
            End If
        Catch ex As Exception

        End Try

        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

        PopulateGrid()

        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        Try
            cboPayMainDeductForwIdNo.DataSource = SQLHelper.ExecuteDataTable("EPayMainDeductForwId_WebLookup", UserNo, Me.cboTabNo.SelectedValue)
            cboPayMainDeductForwIdNo.DataTextField = "tdesc"
            cboPayMainDeductForwIdNo.DataValueField = "tNo"
            cboPayMainDeductForwIdNo.DataBind()
        Catch ex As Exception

        End Try

        'If Me.cboTabNo.SelectedValue = 1 Then
        '    Me.lnkDeleteBatckFile.Visible = True
        'Else
        '    Me.lnkDeleteBatckFile.Visible = False
        'End If

        PopulateGrid()
    End Sub

    Protected Sub cboPayMainDeductForwIdNo_OnSelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub


#Region "********Main*******"

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayMainDeductForwNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayMainDeductForw", UserNo, CType(item, Integer))
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

    Protected Sub lnkDeleteBatckFile_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As Integer = Me.cboPayMainDeductForwIdNo.SelectedValue
            Dim str As String = ""
            'DeleteCount = Generic.DeleteRecordAuditCol("EPayMainDeductForw", UserNo, "PayMainDeductForwIdNo", CType(fieldValues, Integer))
            DeleteCount = SQLHelper.ExecuteNonQuery("EPayMainDeductForwId_WebDelete", UserNo, CType(fieldValues, Integer))

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

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer, IsEnabled As Boolean = False
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayMainDeductForwNo", "IsEnabled"})
                i = Generic.ToInt(obj(0))
                IsEnabled = Generic.ToBol(obj(1))

                Generic.ClearControls(Me, "pnlPopupMain")
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPayMainDeductForw_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                Next
                Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)
                txtFullName.Enabled = False
                lnkSave.Enabled = IsEnabled
                mdlMain.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
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

    'Submit record
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtPayMainDeductForwNo.Text)
            Dim EmployeeNo As Integer = Generic.ToInt(Me.hifEmployeeNo.Value)
            Dim PayDeductTypeNo As Integer = Generic.ToInt(Me.cboPayDeductTypeNo.SelectedValue)
            Dim Description As String = Generic.ToStr(Me.txtDescription.Text)
            Dim Amount As Double = Generic.ToDec(Me.txtAmount.Text)

            Dim PayScheduleNo As Integer = Generic.ToInt(Me.cboPayScheduleNo.SelectedValue)
            Dim ApplicableYear As Integer = Generic.ToInt(Me.txtApplicableYear.Text)
            Dim ApplicableMonth As Integer = Generic.ToInt(Me.cboApplicableMonth.SelectedValue)
            Dim StartDate As String = Generic.ToStr(Me.txtStartDate.Text)
            Dim EndDate As String = Generic.ToStr(Me.txtEndDate.Text)
            Dim IsSpecialPay As Boolean = Generic.ToBol(Me.chkIsSpecialPay.Checked)
            Dim IsBonus As Boolean = Generic.ToBol(Me.chkIsBonus.Checked)
            Dim IsForwarded As Boolean = Generic.ToBol(Me.chkIsForwarded.Checked)
            Dim IsSuspended As Boolean = Generic.ToBol(Me.chkIsSuspended.Checked)

            If SQLHelper.ExecuteNonQuery("EPayMainDeductForw_WebSave", UserNo, tno, EmployeeNo, PayDeductTypeNo, Description, Amount, PayLocNo, 0, _
                                         PayScheduleNo, ApplicableYear, ApplicableMonth, StartDate, EndDate, IsSpecialPay, IsBonus, IsForwarded, IsSuspended) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                PopulateGrid()
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
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
        Catch ex As Exception

        End Try
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



#Region "********Upload********"

    Protected Sub lnkUpload_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel3")
            ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave2_Click(sender As Object, e As EventArgs)
        Dim tsuccess As Integer = 0
        Try
            Dim PayScheduleNo As Integer = Generic.ToInt(Me.cboPaySchedule2No.SelectedValue)
            Dim ApplicableYear As Integer = Generic.ToInt(Me.txtApplicableYear2.Text)
            Dim ApplicableMonth As Integer = Generic.ToInt(Me.cboApplicableMonth2.SelectedValue)
            Dim StartDate As String = Generic.ToStr(Me.txtStartDate2.Text)
            Dim EndDate As String = Generic.ToStr(Me.txtEndDate2.Text)
            Dim IsSpecialPay As Boolean = Generic.ToBol(Me.chkIsSpecialPay2.Checked)
            Dim IsBonus As Boolean = Generic.ToBol(Me.chkIsBonus2.Checked)
            Dim IsForwarded As Boolean = Generic.ToBol(Me.chkIsForwarded2.Checked)

            Dim lastname As String = ""
            Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False
            Dim tpath As String = ""
            Dim xfilename As String = ""
            Dim datenow As Date
            datenow = Now()

            Dim filext As String = Pad.PadZero(2, Month(datenow)) & Pad.PadZero(2, Day(datenow)) & Pad.PadZero(4, Year(datenow)) & Pad.PadZero(2, Hour(datenow)) & Pad.PadZero(2, Minute(datenow)) & Pad.PadZero(4, Second(datenow))
            If fuFilename.HasFile = True Then
                tFilepath = fuFilename.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                xfilename = IO.Path.GetFileName(tFilepath)
                Dim fileext As String = IO.Path.GetExtension(tFilepath)
                tProceed = True
                tpath = (Server.MapPath("documents")) 'Me.MapPath("documents") & "\
                If Not IO.Directory.Exists(tpath) Then
                    IO.Directory.CreateDirectory(tpath)
                End If
                fuFilename.SaveAs(tpath & "\" & tfilename & "_" & filext)

            End If

            Dim tCount As Integer = 0
            Dim amount As Double = 0, employeecode As String = ""

            If tProceed Then

                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0, employeeno As String, logtype As String = ""
                Dim fs As FileStream, fFilename As String
                fFilename = tpath & "\" & tfilename & "_" & filext 'tpath & "\" & tfilename
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                Dim rDesc As String = ""
                Dim rAmount As String = ""
                d.BaseStream.Seek(0, SeekOrigin.Begin)

                Dim BatchUploadId As Integer

                'Save Batch File:
                If d.Peek > -1 Then
                    Dim dty As New DataTable
                    dty = SQLHelper.ExecuteDataTable("EPayMainDeductForwId_WebSave", UserNo, Me.txtDescription2.Text, Me.cboPayDeductTypeNo2.SelectedValue, tfilename, PayScheduleNo, ApplicableYear, ApplicableMonth, StartDate, EndDate, IsSpecialPay, IsBonus, IsForwarded, PayLocNo)
                    For Each rowy As DataRow In dty.Rows
                        BatchUploadId = Generic.ToInt(rowy("tProceed"))
                    Next

                End If

                While d.Peek() > -1
                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, ",")
                    employeeno = fspecArr(0)
                    rDesc = fspecArr(1)
                    rAmount = Replace(fspecArr(2), ":", "")
                    If i > 0 Then
                        If employeeno > "" And rDesc > "" And rAmount <> 0 Then
                            Dim dtx As New DataTable
                            dtx = SQLHelper.ExecuteDataTable("EPayMainDeductForw_WebUpload", UserNo, employeeno, CDbl(rAmount), Me.cboPayDeductTypeNo2.SelectedValue, rDesc, tfilename, PayLocNo, PayScheduleNo, ApplicableYear, ApplicableMonth, StartDate, EndDate, IsSpecialPay, IsBonus, IsForwarded, BatchUploadId)
                            For Each rowx As DataRow In dtx.Rows
                                tCount = Generic.ToInt(rowx("tProceed"))
                            Next
                        End If
                        tsuccess = tsuccess + tCount
                    End If
                    i = i + 1
                End While
                d.Close()

                If tsuccess >= 0 Then
                    PopulateGrid()
                    MessageBox.Success("(" + tsuccess.ToString + ") " + MessageTemplate.SuccessSave, Me)
                End If

            End If

            Try
                cboPayMainDeductForwIdNo.DataSource = SQLHelper.ExecuteDataTable("EPayMainDeductForwId_WebLookup", UserNo, Me.cboTabNo.SelectedValue)
                cboPayMainDeductForwIdNo.DataTextField = "tdesc"
                cboPayMainDeductForwIdNo.DataValueField = "tNo"
                cboPayMainDeductForwIdNo.DataBind()
            Catch ex As Exception

            End Try

        Catch ex As Exception
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End Try

    End Sub
    Private Class CSVFormat
        Private _EmployeeCode As String
        Public Property EmployeeCode As String
            Get
                Return _EmployeeCode
            End Get
            Set(value As String)
                _EmployeeCode = value
            End Set
        End Property

        Private _Description As String
        Public Property Description As String
            Get
                Return _Description
            End Get
            Set(value As String)
                _Description = value
            End Set
        End Property

        Private _Amount As Decimal
        Public Property Amount As Decimal
            Get
                Return _Amount
            End Get
            Set(value As Decimal)
                _Amount = value
            End Set
        End Property
    End Class
#End Region


#Region "********Payment History********"

    Protected Sub lnkPayment_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayMainDeductForwNo"}))

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayMainDeductForwPayment_Web", UserNo, id)
            grdRate.DataSource = dt
            grdRate.DataBind()

            mdlShowRate.Show()

        Catch ex As Exception

        End Try

    End Sub

#End Region
    

#Region "********Detail Check All********"


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

