Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web
Imports System.IO

Imports System.Data.SqlClient

Partial Class Secured_EmpListing_Upload
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim tno As Integer = 0
    '******** Not Fixed **********'
    Dim TableName As String = "EEmployee"
    Dim FormName As String = "EmpListing.aspx"

#Region "Main"

    Protected Sub PopulateGrid(Optional IsMain As Boolean = False)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBatchFile_Web", UserNo, TableName, tno, PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()

            If ViewState("TransNo") = 0 Or IsMain = True Then
                Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"BatchFileNo", "Code"})
                ViewState("TransNo") = obj(0)
                lbl.Text = obj(0)
            End If

            PopulateGridDetl()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        tno = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, FormName, TableName)

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, FormName, TableName) Then
            Dim lnk As New LinkButton
            lnk = sender
            Generic.ClearControls(Me, "Panel1")
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"BatchFileNo", "IsEnabled"})
            Dim iNo As Integer = Generic.ToInt(obj(0))
            Dim IsEnabled As Boolean = Generic.ToBol(obj(1))

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBatchFile_WebOne", UserNo, iNo)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
            Generic.EnableControls(Me, "Panel1", IsEnabled)
            lnkSave.Enabled = IsEnabled
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, FormName, TableName) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BatchFileNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EBatchFile", UserNo, item)
                Generic.DeleteRecordAuditCol("EBatchFileError", UserNo, "BatchFileNo", item)
                '******** Not Fixed **********'
                SQLHelper.ExecuteNonQuery("EEmployee_WebUpload_Delete", UserNo, item, PayLocNo)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost, FormName, TableName) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BatchFileNo"})
            Dim i As Integer = 0
            'Dim ds As DataSet, proceed As Integer, xmessage As String = ""
            For Each item As Integer In fieldValues
                'ds = 
                SQLHelper.ExecuteNonQuery("EBatchFile_WebPost", UserNo, item)
                'If ds.Tables.Count > 0 Then
                '    If ds.Tables(0).Rows.Count > 0 Then
                '        proceed = Generic.ToInt(ds.Tables(0).Rows(0)("proceed"))
                '        xmessage = Generic.ToStr(ds.Tables(0).Rows(0)("xmessage"))
                '    End If
                'End If
                'If proceed = 1 Then
                i = i + 1
                'Else
                'MessageBox.Alert(xmessage, "warning", Me)
                'End If

            Next
            'MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccesPost, Me)
            MessageBox.Alert("(" + i.ToString + ") " + MessageTemplate.SuccesPost, "warning", Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, FormName, TableName) Then
            Generic.ClearControls(Me, "Panel1")
            Generic.EnableControls(Me, "Panel1", True)
            lnkSave.Enabled = True
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim status As Integer = SaveRecord_CSV() 'SaveRecord_XLS() 
        If status = 1 Then
            MessageBox.Success("201 file has been successfully migrated.", Me)
            PopulateGrid()
        ElseIf status = 0 Then
            ' MessageBox.Critical(MessageTemplate.ErrorSave, Me)

        End If

    End Sub
    Private Function SaveRecord_CSV() As Integer
        Dim BatchFileNo As Integer = 0
        Dim retval As Integer = 0
        Dim tsuccess As Integer = 0
        Dim tCount As Integer = 0

        If fuFilename.HasFile Then
            Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String = ""
            Dim _ds As New DataSet, NewFileName As String = ""
            Dim contenttype As String = "", filetypecode As String = ""
            Try

                Filename = IO.Path.GetFileName(fuFilename.PostedFile.FileName)
                FileExt = IO.Path.GetExtension(fuFilename.PostedFile.FileName)

                Dim fs As IO.Stream = fuFilename.PostedFile.InputStream
                Dim br As New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                FileSize = fs.Length
                'ActualPath = getFile_settings()
                ActualPath = ConfigurationManager.AppSettings("upload_path")
                If Generic.ToStr(ConfigurationManager.AppSettings("upload_drive")).ToLower() = "no" Then
                    fuFilename.PostedFile.SaveAs(ActualPath & NewFileName)
                Else
                    'Dim fs As IO.Stream = fuDoc.PostedFile.InputStream
                    'Dim br As New BinaryReader(fs)
                    'Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                    'FileSize = fs.Length
                    WriteFile(ActualPath & "\" & Filename.ToString, bytes)
                End If

                'saving to batch file table
                Dim dtx As New DataTable
                dtx = SQLHelper.ExecuteDataTable("EBatchFile_WebSave", UserNo, Generic.ToInt(txtBatchFileNo.Text), TableName, tno, Filename.ToString, txtDescription.Text, PayLocNo)
                For Each rowx As DataRow In dtx.Rows
                    BatchFileNo = Generic.ToInt(rowx("BatchFileNo"))
                Next

                '******** Not Fixed **********'
                SQLHelper.ExecuteNonQuery("EEmployee_WebUpload_Delete", UserNo, BatchFileNo, PayLocNo)

                'Declare Variable
                'Dim EmployeeCode, fpId, courtesyCode, lastname, firstname, middlename, MaidenName, nickname, EmployeeExtDesc, orientDateFrom, orientDateTo, ProbiStartDate, probiEndDate,
                ' ShiftDesc, hiredDate, RehiredDate, RegularizedDate, SeparatedDate, SuspendDedDate, ImediateSuperiorCode, positionDesc, jobGradeDesc, groupDesc, GroupEmpCode, DivisionDesc,
                ' divisionEmpCode, DepartmentDesc, DepartmentEmpCode, SectionDesc, SectionEmpCode, UnitDesc, UnitEmpCode, LocationDesc, CostCenterCode, EmployeeClassDesc,
                ' EmployeeStatDesc, RankDesc, PayLocDesc, PayClassDesc, PayTypeDesc, PaymentTypeDesc, BankName, BankAccountNo, SSSNo, PHNo, hdmfno, tinNo, MWE, TaxExemptCode, EmployeeRateClassDesc,
                ' BirthDate, BirthPlace, CitizineDesc, CivilStatDesc, GenderDesc, PrHouseNo, PrStreetNo, PrSubd, PRBrgy, PrCityDesc, prPhoneNO, PrMobileNo, PeHouseNo, PeStreet, PeSubd, PeBrgy, CityDesc,
                ' PePhoneNo, email, DateOfMarriage, PlaceOfMarriage, ContactName, ContactRelationshipDesc, CotactNo, ContactAddress

                Dim EmployeeCode As String = "", fpId As String = "", courtesyCode As String = "", lastname As String = "", firstname As String = "", middlename As String = "", MaidenName As String = "", nickname As String = "", EmployeeExtDesc As String = "", orientDateFrom As String = "", orientDateTo As String = "", ProbiStartDate As String = "", probiEndDate As String = "",
                ShiftDesc As String = "", hiredDate As String = "", RehiredDate As String = "", RegularizedDate As String = "", SeparatedDate As String = "", SuspendDedDate As String = "", ImediateSuperiorCode As String = "", positionDesc As String = "", jobGradeDesc As String = "", groupDesc As String = "", GroupEmpCode As String = "", DivisionDesc As String = "",
                divisionEmpCode As String = "", DepartmentDesc As String = "", DepartmentEmpCode As String = "", SectionDesc As String = "", SectionEmpCode As String = "", UnitDesc As String = "", UnitEmpCode As String = "", LocationDesc As String = "", CostCenterCode As String = "", EmployeeClassDesc As String = "",
                EmployeeStatDesc As String = "", RankDesc As String = "", PayLocDesc As String = "", PayClassDesc As String = "", PayTypeDesc As String = "", PaymentTypeDesc As String = "", BankName As String = "", BankAccountNo As String = "", SSSNo As String = "", PHNo As String = "", hdmfno As String = "", tinNo As String = "", MWE As String = "", TaxExemptCode As String = "", EmployeeRateClassDesc As String = "",
                BirthDate As String = "", BirthPlace As String = "", CitizineDesc As String = "", CivilStatDesc As String = "", GenderDesc As String = "", PrHouseNo As String = "", PrStreetNo As String = "", PrSubd As String = "", PRBrgy As String = "", PrCityDesc As String = "", prPhoneNO As String = "", PrMobileNo As String = "", PeHouseNo As String = "", PeStreet As String = "", PeSubd As String = "", PeBrgy As String = "", CityDesc As String = "",
                PePhoneNo As String = "", email As String = "", DateOfMarriage As String = "", PlaceOfMarriage As String = "", ContactName As String = "", ContactRelationshipDesc As String = "", CotactNo As String = "", ContactAddress As String = ""
                'end declaration

                Dim dt As New DataTable, i As Integer = 0
                Dim fFilename As String
                fFilename = ActualPath & "\" & Filename.ToString

                Dim tfp As New FileIO.TextFieldParser(ActualPath & "\" & Filename.ToString)
                tfp.Delimiters = New String() {","}
                tfp.HasFieldsEnclosedInQuotes = True
                While Not tfp.EndOfData
                    Dim fields() As String = tfp.ReadFields

                    If i > 0 Then
                        EmployeeCode = Generic.ToStr(Replace(fields(0), "?", ""))
                        fpId = Generic.ToStr(Replace(fields(1), "?", ""))
                        courtesyCode = Generic.ToStr(Replace(fields(2), "?", ""))
                        lastname = Generic.ToStr(Replace(fields(3), "?", ""))
                        firstname = Generic.ToStr(Replace(fields(4), "?", ""))
                        middlename = Generic.ToStr(Replace(fields(5), "?", ""))
                        MaidenName = Generic.ToStr(Replace(fields(6), "?", ""))
                        nickname = Generic.ToStr(Replace(fields(7), "?", ""))
                        EmployeeExtDesc = Generic.ToStr(Replace(fields(8), "?", ""))
                        orientDateFrom = Generic.ToStr(Replace(fields(9), "?", ""))
                        orientDateTo = Generic.ToStr(Replace(fields(10), "?", ""))
                        ProbiStartDate = Generic.ToStr(Replace(fields(11), "?", ""))
                        probiEndDate = Generic.ToStr(Replace(fields(12), "?", ""))
                        ShiftDesc = Generic.ToStr(Replace(fields(13), "?", ""))
                        hiredDate = Generic.ToStr(Replace(fields(14), "?", ""))
                        RehiredDate = Generic.ToStr(Replace(fields(15), "?", ""))
                        RegularizedDate = Generic.ToStr(Replace(fields(16), "?", ""))
                        SeparatedDate = Generic.ToStr(Replace(fields(17), "?", ""))
                        SuspendDedDate = Generic.ToStr(Replace(fields(18), "?", ""))
                        ImediateSuperiorCode = Generic.ToStr(Replace(fields(19), "?", ""))
                        positionDesc = Generic.ToStr(Replace(fields(20), "?", ""))
                        jobGradeDesc = Generic.ToStr(Replace(fields(21), "?", ""))
                        groupDesc = Generic.ToStr(Replace(fields(22), "?", ""))
                        GroupEmpCode = Generic.ToStr(Replace(fields(23), "?", ""))
                        DivisionDesc = Generic.ToStr(Replace(fields(24), "?", ""))
                        divisionEmpCode = Generic.ToStr(Replace(fields(25), "?", ""))
                        DepartmentDesc = Generic.ToStr(Replace(fields(26), "?", ""))
                        DepartmentEmpCode = Generic.ToStr(Replace(fields(27), "?", ""))
                        SectionDesc = Generic.ToStr(Replace(fields(28), "?", ""))
                        SectionEmpCode = Generic.ToStr(Replace(fields(29), "?", ""))
                        UnitDesc = Generic.ToStr(Replace(fields(30), "?", ""))
                        UnitEmpCode = Generic.ToStr(Replace(fields(31), "?", ""))
                        LocationDesc = Generic.ToStr(Replace(fields(32), "?", ""))
                        CostCenterCode = Generic.ToStr(Replace(fields(33), "?", ""))
                        EmployeeClassDesc = Generic.ToStr(Replace(fields(34), "?", ""))
                        EmployeeStatDesc = Generic.ToStr(Replace(fields(35), "?", ""))
                        RankDesc = Generic.ToStr(Replace(fields(36), "?", ""))
                        PayLocDesc = Generic.ToStr(Replace(fields(37), "?", ""))
                        PayClassDesc = Generic.ToStr(Replace(fields(38), "?", ""))
                        PayTypeDesc = Generic.ToStr(Replace(fields(39), "?", ""))
                        PaymentTypeDesc = Generic.ToStr(Replace(fields(40), "?", ""))
                        BankName = Generic.ToStr(Replace(fields(41), "?", ""))
                        BankAccountNo = Generic.ToStr(Replace(fields(42), "?", ""))
                        SSSNo = Generic.ToStr(Replace(fields(43), "?", ""))
                        PHNo = Generic.ToStr(Replace(fields(44), "?", ""))
                        hdmfno = Generic.ToStr(Replace(fields(45), "?", ""))
                        tinNo = Generic.ToStr(Replace(fields(46), "?", ""))
                        MWE = Generic.ToStr(Replace(fields(47), "?", ""))
                        TaxExemptCode = Generic.ToStr(Replace(fields(48), "?", ""))
                        EmployeeRateClassDesc = Generic.ToStr(Replace(fields(49), "?", ""))
                        BirthDate = Generic.ToStr(Replace(fields(50), "?", ""))
                        BirthPlace = Generic.ToStr(Replace(fields(51), "?", ""))
                        CitizineDesc = Generic.ToStr(Replace(fields(52), "?", ""))
                        CivilStatDesc = Generic.ToStr(Replace(fields(53), "?", ""))
                        GenderDesc = Generic.ToStr(Replace(fields(54), "?", ""))
                        PrHouseNo = Generic.ToStr(Replace(fields(55), "?", ""))
                        PrStreetNo = Generic.ToStr(Replace(fields(56), "?", ""))
                        PrSubd = Generic.ToStr(Replace(fields(57), "?", ""))
                        PRBrgy = Generic.ToStr(Replace(fields(58), "?", ""))
                        PrCityDesc = Generic.ToStr(Replace(fields(59), "?", ""))
                        prPhoneNO = Generic.ToStr(Replace(fields(60), "?", ""))
                        PrMobileNo = Generic.ToStr(Replace(fields(61), "?", ""))
                        PeHouseNo = Generic.ToStr(Replace(fields(62), "?", ""))
                        PeStreet = Generic.ToStr(Replace(fields(63), "?", ""))
                        PeSubd = Generic.ToStr(Replace(fields(64), "?", ""))
                        PeBrgy = Generic.ToStr(Replace(fields(65), "?", ""))
                        CityDesc = Generic.ToStr(Replace(fields(66), "?", ""))
                        PePhoneNo = Generic.ToStr(Replace(fields(67), "?", ""))
                        email = Generic.ToStr(Replace(fields(68), "?", ""))
                        DateOfMarriage = Generic.ToStr(Replace(fields(69), "?", ""))
                        PlaceOfMarriage = Generic.ToStr(Replace(fields(70), "?", ""))
                        ContactName = Generic.ToStr(Replace(fields(71), "?", ""))
                        ContactRelationshipDesc = Generic.ToStr(Replace(fields(72), "?", ""))
                        CotactNo = Generic.ToStr(Replace(fields(73), "?", ""))
                        ContactAddress = Generic.ToStr(Replace(fields(74), "?", ""))

                    End If

                    Dim Str As String = UserNo & ", " & fFilename & ", " & PayLocNo & ", " & BatchFileNo & ", " & i & ", " & EmployeeCode & ", " & fpId & ", " & courtesyCode & ", " & lastname & ", " & firstname & ", " & middlename & ", " & MaidenName & ", " & nickname & ", " & EmployeeExtDesc & ", " & orientDateFrom & ", " & orientDateTo & ", " & ProbiStartDate & ", " & probiEndDate & ", " & ShiftDesc & ", " & hiredDate & ", " & RehiredDate & ", " & RegularizedDate & ", " & SeparatedDate & ", " & SuspendDedDate & ", " & ImediateSuperiorCode & ", " & positionDesc & ", " & jobGradeDesc & ", " & groupDesc & ", " & GroupEmpCode & ", " & DivisionDesc & ", " & divisionEmpCode & ", " & DepartmentDesc & ", " & DepartmentEmpCode & ", " & SectionDesc & ", " & SectionEmpCode & ", " & UnitDesc & ", " & UnitEmpCode & ", " & LocationDesc & ", " & CostCenterCode & ", " & EmployeeClassDesc & ", " & EmployeeStatDesc & ", " & RankDesc & ", " & PayLocDesc & ", " & PayClassDesc & ", " & PayTypeDesc & ", " & PaymentTypeDesc & ", " & BankName & ", " & BankAccountNo & ", " & SSSNo & ", " & PHNo & ", " & hdmfno & ", " & tinNo & ", " & MWE & ", " & TaxExemptCode & ", " & EmployeeRateClassDesc & ", " & BirthDate & ", " & BirthPlace & ", " & CitizineDesc & ", " & CivilStatDesc & ", " & GenderDesc & ", " & PrHouseNo & ", " & PrStreetNo & ", " & PrSubd & ", " & PRBrgy & ", " & PrCityDesc & ", " & prPhoneNO & ", " & PrMobileNo & ", " & PeHouseNo & ", " & PeStreet & ", " & PeSubd & ", " & PeBrgy & ", " & CityDesc & ", " & PePhoneNo & ", " & email & ", " & DateOfMarriage & ", " & PlaceOfMarriage & ", " & ContactName & ", " & ContactRelationshipDesc & ", " & CotactNo & ", " & ContactAddress
                    Dim EmployeeNo As Integer
                    If i >= 1 Then
                        If EmployeeCode.ToString > "" Then
                            dt = SQLHelper.ExecuteDataTable("EEmployee_WebUpload_CSV", UserNo, fFilename, PayLocNo, BatchFileNo, i, EmployeeCode, fpId, courtesyCode, lastname, firstname,
                                    middlename, MaidenName, nickname, EmployeeExtDesc, orientDateFrom, orientDateTo, ProbiStartDate, probiEndDate, ShiftDesc, hiredDate,
                                    RehiredDate, RegularizedDate, SeparatedDate, SuspendDedDate, ImediateSuperiorCode, positionDesc, jobGradeDesc, groupDesc, GroupEmpCode, DivisionDesc,
                                    divisionEmpCode, DepartmentDesc, DepartmentEmpCode, SectionDesc, SectionEmpCode, UnitDesc, UnitEmpCode, LocationDesc, CostCenterCode, EmployeeClassDesc,
                                    EmployeeStatDesc, RankDesc, PayLocDesc, PayClassDesc, PayTypeDesc, PaymentTypeDesc, BankName, BankAccountNo, SSSNo, PHNo,
                                    hdmfno, tinNo, MWE, TaxExemptCode, EmployeeRateClassDesc, BirthDate, BirthPlace, CitizineDesc, CivilStatDesc, GenderDesc,
                                    PrHouseNo, PrStreetNo, PrSubd, PRBrgy, PrCityDesc, prPhoneNO, PrMobileNo, PeHouseNo, PeStreet, PeSubd,
                                    PeBrgy, CityDesc, PePhoneNo, email, DateOfMarriage, PlaceOfMarriage, ContactName, ContactRelationshipDesc, CotactNo, ContactAddress)

                            For Each row As DataRow In dt.Rows
                                tsuccess = Generic.ToInt(row("tProceed"))
                                EmployeeNo = Generic.ToInt(row("EmployeeNo"))
                            Next
                            
                            If tsuccess = 1 Then
                                tCount = tCount + 1
                            End If
                            Dim RandomPassword As String = EGetRandomPassword(EmployeeNo)
                            SQLHelper.ExecuteNonQuery("EEmployee_WebUploadAutoPWD", UserNo, EmployeeNo, PeopleCoreCrypt.Encrypt(RandomPassword))
                        End If
                    End If
                    i = i + 1
                End While
                tfp.Close()

                retval = 1
            Catch ex As Exception
                retval = 0 ' MessageBox.Critical(ex.Message, Me)
                MessageBox.Critical(Replace(ex.Message, "'", ""), Me)
            End Try
        End If
        Return retval
    End Function
    Protected Function EGetRandomPassword(EmployeeNo As Integer) As String
        Dim password As String = ""

        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("ERandomPassword_Web", EmployeeNo)
        For Each row As DataRow In dt.Rows
            password = Generic.ToStr(row("tPassword"))
        Next

        EGetRandomPassword = password
    End Function
    Private Function SaveRecord_XLS() As Integer

        Dim BatchFileNo As Integer = 0
        Dim retval As Integer = 0
        If fuFilename.HasFile Then
            Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String = ""
            Dim _ds As New DataSet, NewFileName As String = ""
            Dim contenttype As String = "", filetypecode As String = ""
            Try

                Filename = IO.Path.GetFileName(fuFilename.PostedFile.FileName)
                FileExt = IO.Path.GetExtension(fuFilename.PostedFile.FileName)

                Dim fs As IO.Stream = fuFilename.PostedFile.InputStream
                Dim br As New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                FileSize = fs.Length
                'ActualPath = getFile_settings()
                ActualPath = ConfigurationManager.AppSettings("drive_path")
                If Generic.ToStr(ConfigurationManager.AppSettings("share_drive")).ToLower() = "yes" Then
                    fuFilename.PostedFile.SaveAs(ActualPath & NewFileName)
                Else
                    'Dim fs As IO.Stream = fuDoc.PostedFile.InputStream
                    'Dim br As New BinaryReader(fs)
                    'Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                    'FileSize = fs.Length
                    WriteFile(ActualPath & "\" & Filename.ToString, bytes)
                End If

                'saving to batch file table
                Dim dtx As New DataTable
                dtx = SQLHelper.ExecuteDataTable("EBatchFile_WebSave", UserNo, Generic.ToInt(txtBatchFileNo.Text), TableName, tno, Filename.ToString, txtDescription.Text, PayLocNo)
                For Each rowx As DataRow In dtx.Rows
                    BatchFileNo = Generic.ToInt(rowx("BatchFileNo"))
                Next

                '******** Not Fixed **********'
                SQLHelper.ExecuteNonQuery("EEmployee_WebUpload_Delete", UserNo, BatchFileNo, PayLocNo)


                Dim MyConnection As System.Data.OleDb.OleDbConnection
                Dim DtSet As System.Data.DataSet
                Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
                'MyConnection = New System.Data.OleDb.OleDbConnection("provider=MICROSOFT.ACE.OLEDB.12.0;Data Source=" + ActualPath & "\" & Filename.ToString + ";Extended Properties=Excel 8.0;")
                'MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='c:\vb.net-informations.xls';Extended Properties=Excel 8.0;")
                'MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection)
                MyConnection = New System.Data.OleDb.OleDbConnection("provider=MICROSOFT.ACE.OLEDB.12.0;Data Source=" + ActualPath & "\" & Filename.ToString)
                MyCommand = New System.Data.OleDb.OleDbDataAdapter("Sheet1", MyConnection)
                MyCommand.TableMappings.Add("Table", "EEmployeeTemp")
                DtSet = New System.Data.DataSet
                MyCommand.Fill(DtSet)
                MyConnection.Close()


                Dim expr As String = "SELECT * FROM [Sheet1$]"

                SQLHelper.ExecuteNonQuery("Truncate table dbo.EEmployeeTemp")

                Dim SQLconn As New SqlConnection()
                Dim ConnString As String = SQLHelper.ConSTR
                Dim objCmdSelect As System.Data.OleDb.OleDbCommand = New System.Data.OleDb.OleDbCommand(expr, MyConnection)
                Dim objDR As System.Data.OleDb.OleDbDataReader

                SQLconn.ConnectionString = SQLHelper.ConSTR

                Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(SQLHelper.ConSTR)
                    bulkCopy.DestinationTableName = "EEmployeeTemp"
                    Try
                        MyConnection.Open()
                        objDR = objCmdSelect.ExecuteReader
                        bulkCopy.WriteToServer(objDR)
                        objDR.Close()
                        SQLconn.Close()
                        MyConnection.Close()

                        SQLHelper.ExecuteNonQuery("EEmployee_WebUpload_XLS_V2", ActualPath & "\" & Filename.ToString, PayLocNo, Generic.ToInt(BatchFileNo))
                    Catch ex As Exception
                        MessageBox.Critical(Replace(ex.Message, "'", ""), Me)
                    End Try
                End Using



                retval = 1
            Catch ex As Exception
                retval = 0 ' MessageBox.Critical(ex.Message, Me)
                MessageBox.Critical(Replace(ex.Message, "'", ""), Me)
            End Try
        End If
        Return retval

    End Function
    Private Sub WriteFile(strPath As String, Buffer As Byte())
        'Create a file
        Dim newFile As FileStream = New FileStream(strPath, FileMode.Create)

        'Write data to the file
        newFile.Write(Buffer, 0, Buffer.Length)

        'Close file
        newFile.Close()
    End Sub



    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"BatchFileNo", "Code"})
        ViewState("TransNo") = obj(0)
        lbl.Text = obj(0)
        PopulateGridDetl()
    End Sub

    Protected Sub lnkDownloadTemplate_Click(sender As Object, e As EventArgs)
        'clsGenericClass.DownloadFile("~/Uploading_Templates/201_Uploading_Template.xlsx", , True)
        OpenText("~/Uploading_Templates/201 Template for Migration-01-msdos.csv")
    End Sub
    Private Sub OpenText(ByVal fullpath As String)
        Dim FileName As String = ""
        FileName = IO.Path.GetFileName(fullpath)
        Response.Clear()
        Response.ClearContent()
        Response.ContentType = "application/octet-stream"
        Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
        Response.TransmitFile(fullpath)
        Response.End()
    End Sub

#End Region

#Region "Detail"

    Private Sub PopulateGridDetl()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EBatchFileError_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Protected Sub lnkExportD_Click(sender As Object, e As EventArgs)
        Try
            grdExportD.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

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




