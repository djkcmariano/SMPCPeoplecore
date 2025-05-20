Imports System.Data
Imports System.IO
Imports System.Math
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PayInfoList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim clsGeneric As New clsGenericClass

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()

        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayInfo_Web", UserNo, PayLocNo, cboTabNo.SelectedValue)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim URL As String
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo"})))
        If URL <> "" Then
            Response.Redirect(URL)
        End If
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 6)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

#Region "****Upload 201****"


    Protected Sub lnkUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            mdlUpload.Show()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkUploadSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If PoplulateCSVFile() Then
            MessageBox.Warning("Uploading of file successfully done.", Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
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

            Dim filext As String = Pad.PadZero(2, Month(datenow)) & Pad.PadZero(2, Day(datenow)) & Pad.PadZero(4, Year(datenow)) & Pad.PadZero(2, Hour(datenow)) & Pad.PadZero(2, Minute(datenow)) & Pad.PadZero(4, Second(datenow))
            If txtFile.HasFile = True Then
                tFilepath = txtFile.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                Dim fileext As String = IO.Path.GetExtension(tFilepath)
                tProceed = True
                tpath = (Server.MapPath("documents")) 'Me.MapPath("documents") & "\
                If Not IO.Directory.Exists(tpath) Then
                    IO.Directory.CreateDirectory(tpath)
                End If
                txtFile.SaveAs(tpath & "\" & tfilename & "_" & filext)

            End If


            Dim amount As Double = 0, employeecode As String = ""

            If tProceed Then

                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0, employeeno As String, logtype As String = ""
                Dim fs As FileStream, fFilename As String
                fFilename = tpath & "\" & tfilename & "_" & filext 'tpath & "\" & tfilename
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                Dim rlastname As String = ""
                Dim rfirstname As String = ""
                Dim rmiddleName As String = ""
                Dim BirthDate As String = "", Shift As String = "", Dayoff1 As String = "", DayOff2 As String = "", payClass As String = ""
                Dim Division As String = "", Department As String = "", CostCenter As String = "", ImmediateSuperior As String = "", JobGrade As String = ""
                Dim EmployeeRateClass As String = "", TaxCode As String = "", PaymentType As String = "", BankType As String = "", BankAccountNo As String = "", SSSNo As String = ""
                Dim TINNo As String = "", HDMFNo As String = "", PHNo As String = "", HiredDate As String = "", EmployeeClass As String = "", EmployeeStatus As String = "", PayrollType As String = ""


                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1
                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, ",")
                    employeeno = fspecArr(0)
                    rlastname = fspecArr(1)
                    rfirstname = fspecArr(2)
                    rmiddleName = fspecArr(3)
                    BirthDate = fspecArr(4)
                    Shift = fspecArr(5)
                    Dayoff1 = fspecArr(6)
                    DayOff2 = fspecArr(7)
                    payClass = fspecArr(8)
                    Division = fspecArr(9)
                    Department = fspecArr(10)
                    CostCenter = fspecArr(11)
                    ImmediateSuperior = fspecArr(12)
                    JobGrade = fspecArr(13)
                    EmployeeRateClass = fspecArr(14)
                    TaxCode = fspecArr(15)
                    PaymentType = fspecArr(16)
                    BankType = fspecArr(17)
                    BankAccountNo = fspecArr(18)
                    SSSNo = fspecArr(19)
                    TINNo = fspecArr(20)
                    HDMFNo = fspecArr(21)
                    PHNo = fspecArr(22)
                    HiredDate = fspecArr(23)
                    EmployeeClass = fspecArr(24)
                    EmployeeStatus = fspecArr(25)
                    PayrollType = fspecArr(26)

                    If i > 0 Then
                        SQLHelper.ExecuteNonQuery("EEmployee_WebUpload", UserNo,
                                                employeeno,
                                                rlastname,
                                                rfirstname,
                                                rmiddleName,
                                                BirthDate,
                                                Shift,
                                                Dayoff1,
                                                DayOff2,
                                                payClass,
                                                Division,
                                                Department,
                                                CostCenter,
                                                ImmediateSuperior,
                                                JobGrade,
                                                EmployeeRateClass,
                                                TaxCode,
                                                PaymentType,
                                                BankType,
                                                BankAccountNo,
                                                SSSNo,
                                                TINNo,
                                                HDMFNo,
                                                PHNo,
                                                HiredDate,
                                                EmployeeClass,
                                                EmployeeStatus,
                                                PayrollType,
                                                Session("xPayLocNo"))
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


#End Region


End Class


