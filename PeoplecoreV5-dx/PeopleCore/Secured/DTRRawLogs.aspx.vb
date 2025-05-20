Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports Microsoft.VisualBasic.FileIO

Partial Class Secured_DTROTList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    Dim xBase As New clsBase.clsBaseLibrary
    Dim IsCompleted As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRRawLogs_Web", UserNo, Filter1.SearchText, Generic.ToInt(cbofilterby.SelectedValue), Generic.ToInt(cbofiltervalue.SelectedValue), Generic.ToStr(fltxtStartDate.Text), Generic.ToStr(fltxtEndDate.Text), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel3", Generic.ToInt(Session("xPayLocNo")))
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        AddHandler Filter1.lnkSearchClick, AddressOf lnkGo_Click

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If fltxtStartDate.Text > "" And fltxtEndDate.Text > "" Then
            PopulateGrid()
        Else
            MessageBox.Information("Date From and Date To are required in filtering.", Me)
        End If

    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub


    Protected Sub lnkUpload_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel3")
            ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave2_Click(sender As Object, e As EventArgs)

        Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String = "", IsValidDAT As Boolean = False, IsValidCSV As Boolean = False
        Try
            Dim uploadFolder As String = Request.PhysicalApplicationPath + "secured\documents\"
            If fuFilename.HasFile Then
                Dim NewFileName As String = Guid.NewGuid().ToString()
                FileExt = Path.GetExtension(fuFilename.PostedFile.FileName)
                Filename = Path.GetFileNameWithoutExtension(fuFilename.PostedFile.FileName)
                If FileExt = ".dat" And cboSourceNo.SelectedValue = "1" Then
                    IsValidDAT = True
                ElseIf FileExt = ".csv" And cboSourceNo.SelectedValue = "4" Then
                    IsValidCSV = True
                End If

                fuFilename.SaveAs(Convert.ToString(uploadFolder & NewFileName) & FileExt)
                'If SQLHelper.ExecuteNonQuery("EDoc_WebSave", UserNo, 0, 0, "Upload Logs", Filename, FileExt, NewFileName & FileExt, FileSize, Convert.ToString(uploadFolder & NewFileName) & FileExt, Generic.ToStr(Session("xMenuType")), 1) > 0 Then
                ActualPath = Convert.ToString(uploadFolder & NewFileName) & FileExt
                'End If
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
                Dim list As New List(Of CSVFormat)
                Using parser As New TextFieldParser(ActualPath)
                    parser.TextFieldType = FieldType.Delimited
                    parser.Delimiters = New String() {","}
                    Dim fields As String()
                    While Not parser.EndOfData
                        fields = parser.ReadFields()
                        If Len(Generic.ToInt(fields(0))) > 0 Then
                            list.Add(New CSVFormat() With
                                                 {
                                                     .EmployeeCode = Generic.ToInt(fields(0)),
                                                     .DTRDate = Generic.ToStr(fields(1)),
                                                     .LogType = Generic.ToStr(fields(2)),
                                                     .DTRTime = Generic.ToStr(fields(3))
                                                 })
                        End If
                    End While
                    If list.Count > 0 Then
                        Dim count As Integer = 0
                        For Each row In list
                            If SQLHelper.ExecuteNonQuery("EFPDTR_BIO_SPUpdate_CSV", UserNo, row.EmployeeCode, row.DTRDate, row.LogType, row.DTRTime, Generic.ToInt(Me.cboFPMachineNo.SelectedValue), txtDescription.Text, PayLocNo) > 0 Then
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

        'If Generic.ToInt(cboSourceNo.SelectedValue) = 4 Then
        '    If PoplulateCSVFile() Then
        '        PopulateGrid()
        '        MessageBox.Success(MessageTemplate.SuccessSave, Me)
        '    Else
        '        MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        '    End If
        'Else
        '    MessageBox.Alert("DTR Source not available.", "warning", Me)
        '    ModalPopupExtender2.Show()
        'End If

    End Sub

    Private Function PoplulateCSVFile() As Boolean
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
            Dim FPMachineNo As Integer = Generic.ToInt(Me.cboFPMachineNo.SelectedValue)
            Dim Remarks As String = Generic.ToStr(Me.txtDescription.Text)

            If tProceed Then

                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0, employeeno As String = "", logtype As String = "", DTRTime As String = ""
                Dim fs As FileStream, fFilename As String
                fFilename = tpath & "\" & tfilename & "_" & filext 'tpath & "\" & tfilename
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                Dim rDate As String = ""
                Dim rIn As String = ""
                Dim rOut As String = ""
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1
                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, ",")
                    employeeno = fspecArr(0)
                    rDate = fspecArr(1)
                    logtype = fspecArr(2)
                    DTRTime = Replace(fspecArr(3), ":", "")
                    'If Len(fspecArr) > 0 Then
                    If i > 0 Then
                        If employeeno > "" Then
                            SQLHelper.ExecuteDataSet("EFPDTR_BIO_SPUpdate_CSV", UserNo, employeeno, rDate, logtype, DTRTime, FPMachineNo, Remarks, PayLocNo)
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

    Protected Sub lnkDownload_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            Generic.ClearControls(Me, "pnlPopup")
            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            '//validate start here
            Dim RetVal As Boolean = False
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EDTRRawLogs_Validate", UserNo, Generic.ToStr(txtDateFrom.Text), Generic.ToStr(txtDateTo.Text), PayLocNo)

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("tProceed"))
                messagedialog = Generic.ToStr(rowx("xMessage"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                mdlShow.Show()
                Exit Sub
            Else
                RetVal = True
            End If

            If RetVal = True Then
                DTRAppendAsyn()
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessProcess & " " & Now().ToString, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If
    End Sub

    Private Sub DTRAppendAsyn()
        Dim xcmdProcSAVE As SqlClient.SqlCommand

        Try

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand

            xcmdProcSAVE.CommandText = "EFPDTR_Manual_Dowload"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = xBase.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0

            xcmdProcSAVE.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@onlineuserno").Value = Generic.ToInt(UserNo)

            xcmdProcSAVE.Parameters.Add("@StartDate", SqlDbType.VarChar, 20)
            xcmdProcSAVE.Parameters("@StartDate").Value = Generic.ToStr(txtDateFrom.Text)

            xcmdProcSAVE.Parameters.Add("@EndDate", SqlDbType.VarChar, 20)
            xcmdProcSAVE.Parameters("@EndDate").Value = Generic.ToStr(txtDateTo.Text)

            xcmdProcSAVE.Parameters.Add("@PayLocNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@PayLocNo").Value = Generic.ToInt(PayLocNo)

            xBase.RunCommandAsynchronous(xcmdProcSAVE, "EFPDTR_Manual_Dowload", SQLHelper.ConSTRAsyn, IsCompleted)
            Session("IsCompleted") = 0 'IsCompleted

            If Session("IsCompleted") = 1 Then
                'clsModalControls.SetModalPopupControls(CType(Master.FindControl("cphBody"), ContentPlaceHolder), "completed")
            End If
        Catch
            'Response.RedirectLocation = Session("xFormname") & "?IsClickMain=" & IsClickMain
        End Try

    End Sub

    Protected Sub cboSourceNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EDTRRawLogs_Format", UserNo, Generic.ToInt(cboSourceNo.SelectedValue), PayLocNo)

            For Each rowx As DataRow In dtx.Rows
                lblFormat.Text = Generic.ToStr(rowx("xMessage"))
            Next
            ModalPopupExtender2.Show()
        Catch ex As Exception

        End Try
    End Sub

    Private Class DATFormat

        Private _FPID As Integer
        Public Property FPID As Integer
            Get
                Return _FPID
            End Get
            Set(value As Integer)
                _FPID = value
            End Set
        End Property

        Private _DTRDate As String
        Public Property DTRDate As String
            Get
                Return _DTRDate
            End Get
            Set(value As String)
                _DTRDate = value
            End Set
        End Property

        Private _MachineNo As Integer
        Public Property MachineNo As Integer
            Get
                Return _MachineNo
            End Get
            Set(value As Integer)
                _MachineNo = value
            End Set
        End Property

        Private _InOutMode As Integer
        Public Property InOutMode As Integer
            Get
                Return _InOutMode
            End Get
            Set(value As Integer)
                _InOutMode = value
            End Set
        End Property
    End Class

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

        Private _DTRDate As String
        Public Property DTRDate As String
            Get
                Return _DTRDate
            End Get
            Set(value As String)
                _DTRDate = value
            End Set
        End Property

        Private _DTRTime As String
        Public Property DTRTime As String
            Get
                Return _DTRTime
            End Get
            Set(value As String)
                _DTRTime = value
            End Set
        End Property

        Private _LogType As String
        Public Property LogType As String
            Get
                Return _LogType
            End Get
            Set(value As String)
                _LogType = value
            End Set
        End Property

    End Class

End Class





