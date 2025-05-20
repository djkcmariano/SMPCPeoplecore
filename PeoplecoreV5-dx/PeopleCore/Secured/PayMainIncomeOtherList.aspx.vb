Imports System.Data
Imports System.Math
Imports System.Web.Services
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports Microsoft.VisualBasic.FileIO
Imports System.IO

Partial Class Secured_PayMainIncomeOtherList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer

    Private Sub PopulateGrid()

        PayHeader.ID = Generic.ToInt(TransNo)

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayMainIncomeOther_Web", UserNo, TransNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        'AccessRights.CheckUser(UserNo, Session("xFormName"), Session("xTableName"))
        Permission.IsAuthenticatedCoreUser()
        If Not IsPostBack Then
            PopulateDropDownList()
            PopulateData()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPay_WebOne", UserNo, TransNo)
        Generic.PopulateData(Me, "Panel1", dt)
        For Each row As DataRow In dt.Rows
            lnkSave.Enabled = Not Generic.ToBol(row("IsPosted"))
            lnkAdd.Enabled = Not Generic.ToBol(row("IsPosted"))
            lnkUpload.Enabled = Not Generic.ToBol(row("IsPosted"))
            lnkSave2.Enabled = Not Generic.ToBol(row("IsPosted"))
        Next
    End Sub

    Private Sub PopulateDataIncome(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayMainIncomeOther_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
    End Sub

    Private Sub PopulateDropDownList()
        Try
            cboPayIncomeTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayIncomeType_WebLookup", UserNo, PayLocNo)
            cboPayIncomeTypeNo.DataValueField = "tNo"
            cboPayIncomeTypeNo.DataTextField = "tDesc"
            cboPayIncomeTypeNo.DataBind()
        Catch ex As Exception
        End Try
        Try
            cboPayIncomeTypeNo2.DataSource = SQLHelper.ExecuteDataSet("EPayIncomeType_WebLookup", UserNo, PayLocNo)
            cboPayIncomeTypeNo2.DataValueField = "tNo"
            cboPayIncomeTypeNo2.DataTextField = "tDesc"
            cboPayIncomeTypeNo2.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkUpload_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, Session("xFormName"), Session("xTableName")) Then
        Generic.ClearControls(Me, "Panel3")
        ModalPopupExtender2.Show()
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        'End If
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, Session("xFormName"), Session("xTableName")) Then
        Generic.ClearControls(Me, "Panel2")
        ModalPopupExtender1.Show()
        'Else
        '    MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        'End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, Session("xFormName"), Session("xTableName")) Then
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayMainIncomeOtherNo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EPayMainIncomeOther", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        'End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, Session("xFormName"), Session("xTableName")) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Generic.ClearControls(Me, "Panel2")
            PopulateDataIncome(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayMainIncomeOtherNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Generic.EnableControls(Me, "Panel2", IsEnabled)
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim Retval As Boolean = False
        Dim EmployeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))
        Dim Amount As Double = Generic.ToDec(Me.txtAmount.Text)
        Dim IncomeTypeNo As Integer = Generic.ToInt(Me.cboPayIncomeTypeNo.SelectedValue)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EPay_Employee_WebValidate", UserNo, TransNo, EmployeeNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EPaymainIncomeOther_WebSave", UserNo, Generic.ToInt(txtCode.Text), EmployeeNo, IncomeTypeNo, Me.txtDescription.Text.ToString, Amount, TransNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
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


#Region "Upload"
    'Protected Sub lnkSave2_Click(sender As Object, e As EventArgs)
    '    Dim Filename As String, FileExt As String, FileSize As Integer, ActualPath As String = ""
    '    'Upload file to server
    '    Dim retval As Boolean = False
    '    If fuFilename.HasFile Then            
    '        Dim _ds As New DataSet, NewFileName As String
    '        Try
    '            Filename = IO.Path.GetFileName(fuFilename.PostedFile.FileName)
    '            FileExt = IO.Path.GetExtension(fuFilename.PostedFile.FileName)
    '            Dim f As New System.IO.FileInfo(fuFilename.PostedFile.FileName)
    '            FileSize = f.Length
    '            NewFileName = Guid.NewGuid().ToString()
    '            ActualPath = Server.MapPath("../") & "secured\documents\" & NewFileName & FileExt
    '            If FileExt.ToLower() = ".csv" Then
    '                fuFilename.SaveAs(ActualPath)
    '                If SQLHelper.ExecuteNonQuery("EDoc_WebSave", UserNo, TransNo, Generic.ToInt(txtCode.Text), "Other Income Upload", Filename, FileExt, NewFileName & FileExt, FileSize, ActualPath, Generic.ToStr(Session("xMenuType")), 1) > 0 Then
    '                    retval = True
    '                End If
    '            Else
    '                MessageBox.Warning("Invalid file type.", Me)
    '                Exit Sub
    '            End If
    '        Catch ex As Exception
    '            MessageBox.Warning("Error file upload.", Me)
    '            Exit Sub
    '        End Try
    '    End If

    '    'Read file and insert record to database
    '    Dim list As New List(Of CSVFormat)
    '    Using parser As New TextFieldParser(ActualPath)
    '        parser.TextFieldType = FieldType.Delimited
    '        parser.Delimiters = New String() {","}
    '        Dim fields As String()
    '        Try
    '            While Not parser.EndOfData
    '                fields = parser.ReadFields()
    '                list.Add(New CSVFormat() With
    '                                     {
    '                                     .EmployeeCode = Generic.ToStr(fields(0)),
    '                                     .Description = Generic.ToStr(fields(1)),
    '                                     .Amount = Generic.ToDec(fields(2))
    '                                     })
    '            End While
    '            If list.Count > 0 Then
    '                For Each row In list
    '                    SQLHelper.ExecuteNonQuery("EPayMainIncomeOther_WebUpload", UserNo, row.EmployeeCode, row.Amount, cboPayIncomeTypeNo2.SelectedValue, row.Description, TransNo)
    '                Next
    '                PopulateGrid()
    '                MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '            End If

    '        Catch ex As Exception
    '            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
    '        End Try
    '    End Using

    'End Sub
    Protected Sub lnkSave2_Click(sender As Object, e As EventArgs)
        If PoplulateCSVFile() Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub
    Private Function PoplulateCSVFile() As Boolean
        Dim tsuccess As Integer = 0
        Try


            Dim lastname As String = ""
            Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False
            Dim tpath As String = ""
            Dim datenow As Date
            datenow = Now()

            Dim fileext As String
            Dim filext As String = Pad.PadZero(2, Month(datenow)) & Pad.PadZero(2, Day(datenow)) & Pad.PadZero(4, Year(datenow)) & Pad.PadZero(2, Hour(datenow)) & Pad.PadZero(2, Minute(datenow)) & Pad.PadZero(4, Second(datenow))
            If fuFilename.HasFile = True Then
                tFilepath = fuFilename.PostedFile.FileName
                tfilename = IO.Path.GetFileNameWithoutExtension(tFilepath)
                fileext = IO.Path.GetExtension(tFilepath)
                tProceed = True
                tpath = (Server.MapPath("documents")) 'Me.MapPath("documents") & "\
                If Not IO.Directory.Exists(tpath) Then
                    IO.Directory.CreateDirectory(tpath)
                End If
                fuFilename.SaveAs(tpath & "\" & tfilename & "_" & filext & fileext)

            End If


            Dim amount As Double = 0, employeecode As String = ""

            If tProceed Then

                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0, employeeno As String, logtype As String = ""
                Dim fs As FileStream, fFilename As String
                fFilename = tpath & "\" & tfilename & "_" & filext & fileext 'tpath & "\" & tfilename
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                Dim rDesc As String = ""
                Dim rAmount As String = ""
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1
                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, ",")
                    employeeno = fspecArr(0)
                    rDesc = fspecArr(1)
                    rAmount = Replace(fspecArr(2), ":", "")
                    'If Len(fspecArr) > 0 Then
                    If i > 0 Then
                        If employeeno > "" And rDesc > "" And rAmount <> 0 Then
                            SQLHelper.ExecuteDataSet("EPayMainIncomeOther_WebUpload", UserNo, employeeno, CDbl(rAmount), Me.cboPayIncomeTypeNo2.SelectedValue, rDesc, TransNo)
                            tsuccess = tsuccess + 1
                        End If
                    End If

                    i = i + 1
                End While
                d.Close()
                Return True
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

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





