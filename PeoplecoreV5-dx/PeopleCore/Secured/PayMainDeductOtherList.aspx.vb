Imports System.Data
Imports System.Math
Imports System.Web.Services
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports Microsoft.VisualBasic.FileIO
Imports System.IO

Partial Class Secured_PayMainDeductOtherList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer
    Dim isfromlastpay As Integer = 0
    Private Sub PopulateGrid()

        PayHeader.ID = Generic.ToInt(TransNo)

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayMainDeductOther_Web", UserNo, TransNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        isfromlastpay = Generic.ToInt(Request.QueryString("isfromlastpay"))
        If isfromlastpay = 1 Then
            aceFullName.CompletionSetCount = isfromlastpay
        End If
        AccessRights.CheckUser(UserNo, Generic.ToStr(Session("xFormName")), Generic.ToStr(Session("xTableName")))
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

    Private Sub PopulateDataDeduction(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayMainDeductOther_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
        Dim iNo As Integer = Generic.ToInt(cboBSClientNo.SelectedValue)
        Populate_Project(iNo)
    End Sub

    Private Sub PopulateDropDownList()
        Generic.PopulateDropDownList(UserNo, Me, "panel3", PayLocNo)
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
            cboPayDeductTypeNo3.DataSource = SQLHelper.ExecuteDataSet("ELoanType_WebLookup", UserNo, PayLocNo)
            cboPayDeductTypeNo3.DataValueField = "tNo"
            cboPayDeductTypeNo3.DataTextField = "tDesc"
            cboPayDeductTypeNo3.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkUpload_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, Session("xFormName"), Session("xTableName")) Then
            Generic.ClearControls(Me, "Panel3")
            ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub
    Protected Sub lnkUploadCL_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, Session("xFormName"), Session("xTableName")) Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender3.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub


    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, Session("xFormName"), Session("xTableName")) Then
            Generic.ClearControls(Me, "Panel2")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, Session("xFormName"), Session("xTableName")) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayMainDeductOtherNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayMainDeductOther", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub cboProject_Change(sender As Object, e As EventArgs)
        Try
            Dim iNo As Integer = Generic.ToInt(cboBSClientNo.SelectedValue)
            Populate_Project(iNo)
            ModalPopupExtender2.Show()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Populate_Project(iNo As Integer)
        Try
            cboProjectNo.DataSource = SQLHelper.ExecuteDataSet("EProject_WebLookup_BSClient", UserNo, iNo)
            cboProjectNo.DataTextField = "tDesc"
            cboProjectNo.DataValueField = "tNo"
            cboProjectNo.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, Session("xFormName"), Session("xTableName")) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Generic.ClearControls(Me, "Panel2")
            PopulateDataDeduction(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayMainDeductOtherNo"})))
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
        Dim DeductTypeNo As Integer = Generic.ToInt(Me.cboPayDeductTypeNo.SelectedValue)

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

        If SQLHelper.ExecuteNonQuery("EPayMainDeductOther_WebSave", UserNo, Generic.ToInt(txtCode.Text), EmployeeNo, DeductTypeNo, txtDescription.Text, Amount, TransNo) > 0 Then
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
    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function cboEmployee(prefixText As String, count As Integer, contextKey As String) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()
        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))
        Dim payclassNo As Integer = (HttpContext.Current.Session("PayclassNo_Pay"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AC_PayClass", UserNo, prefixText, payclassNo, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

#Region "Upload"
   
    Protected Sub lnkSave2_Click(sender As Object, e As EventArgs)
        Dim retVal As Integer = PoplulateCSVFile()
        If retval = 1 Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        ElseIf retVal = 2 Then
            MessageBox.Alert("The file must have a header.", "warning", Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub
    Private Function PoplulateCSVFile() As Integer
        Dim tsuccess As Integer = 0
        Try


            Dim lastname As String = ""
            Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False
            Dim tpath As String = ""
            Dim datenow As Date
            datenow = Now()

            Dim filext As String = Pad.PadZero(2, Month(datenow)) & Pad.PadZero(2, Day(datenow)) & Pad.PadZero(4, Year(datenow)) & Pad.PadZero(2, Hour(datenow)) & Pad.PadZero(2, Minute(datenow)) & Pad.PadZero(4, Second(datenow))
            If fuFilename.HasFile = True Then
                tFilepath = fuFilename.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                Dim fileext As String = IO.Path.GetExtension(tFilepath)
                tProceed = True
                tpath = (Server.MapPath("documents")) 'Me.MapPath("documents") & "\
                If Not IO.Directory.Exists(tpath) Then
                    IO.Directory.CreateDirectory(tpath)
                End If
                fuFilename.SaveAs(tpath & "\" & tfilename & "_" & filext)

            End If


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
                While d.Peek() > -1
                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, ",")
                    employeeno = fspecArr(0)
                    rDesc = fspecArr(1)
                    rAmount = Replace(fspecArr(2), ":", "")
                    If i = 0 Then
                        If Left(employeeno, 8).ToLower <> "employee" Then
                            Return 2
                            Exit Function
                        End If
                    End If
                    'If Len(fspecArr) > 0 Then
                    If i > 0 Then
                        If employeeno > "" And rDesc > "" And rAmount <> 0 Then
                            SQLHelper.ExecuteDataSet("EPayMainDeductOther_WebUpload", UserNo, employeeno, CDbl(rAmount), Me.cboPayDeductTypeNo2.SelectedValue, rDesc, TransNo) ', Generic.ToInt(cboBSClientNo.SelectedValue), Generic.ToInt(cboProjectNo.SelectedValue))
                            tsuccess = tsuccess + 1
                        End If
                    End If

                    i = i + 1
                End While
                d.Close()
                Return 1
            End If
            Return True
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Protected Sub lnkSave3_Click(sender As Object, e As EventArgs)
        Dim retVal As Integer = PoplulateCSVFile_Coop()
        If retVal = 1 Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        ElseIf retVal = 2 Then
            MessageBox.Alert("The file must have a header.", "warning", Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub
    Private Function PoplulateCSVFile_Coop() As Integer
        Dim tsuccess As Integer = 0
        Try


            Dim lastname As String = ""
            Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False
            Dim tpath As String = ""
            Dim datenow As Date
            datenow = Now()

            Dim filext As String = Pad.PadZero(2, Month(datenow)) & Pad.PadZero(2, Day(datenow)) & Pad.PadZero(4, Year(datenow)) & Pad.PadZero(2, Hour(datenow)) & Pad.PadZero(2, Minute(datenow)) & Pad.PadZero(4, Second(datenow))
            If fuFilename.HasFile = True Then
                tFilepath = fuFilename.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                Dim fileext As String = IO.Path.GetExtension(tFilepath)
                tProceed = True
                tpath = (Server.MapPath("documents")) 'Me.MapPath("documents") & "\
                If Not IO.Directory.Exists(tpath) Then
                    IO.Directory.CreateDirectory(tpath)
                End If
                fuFilename.SaveAs(tpath & "\" & tfilename & "_" & filext)

            End If


            Dim amount As Double = 0, employeecode As String = ""

            If tProceed Then

                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0, employeeno As String, lrid As String = "", loancode As String, balance As Double
                Dim fs As FileStream, fFilename As String
                fFilename = tpath & "\" & tfilename & "_" & filext 'tpath & "\" & tfilename
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                Dim rAmount As String = ""
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1
                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, ",")
                    lrid = fspecArr(0)
                    employeeno = fspecArr(1)
                    loancode = fspecArr(2)
                    rAmount = Replace(fspecArr(3), ":", "")
                    balance = Replace(fspecArr(4), ":", "")
                    If i = 0 Then
                        If Left(employeeno, 8).ToLower <> "employee" Then
                            Return 2
                            Exit Function
                        End If
                    End If
                    'If Len(fspecArr) > 0 Then
                    If i > 0 Then
                        If employeeno > "" And rAmount <> 0 Then
                            SQLHelper.ExecuteDataSet("EPayMainDeductOther_WebUpload_Coop", UserNo, employeeno, CDbl(rAmount), Me.cboPayDeductTypeNo2.SelectedValue, Generic.ToDec(balance), TransNo, Generic.ToStr(lrid), Generic.ToStr(loancode))
                            tsuccess = tsuccess + 1
                        End If
                    End If

                    i = i + 1
                End While
                d.Close()
                Return 1
            End If
            Return True
        Catch ex As Exception
            Return 0
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











