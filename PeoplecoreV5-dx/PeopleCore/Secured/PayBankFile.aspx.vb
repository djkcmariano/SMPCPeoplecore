Imports clsLib
Imports System.Data
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Data.OleDb
Imports System.Math
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports Microsoft.VisualBasic.FileIO

Partial Class Secured_PayBankFile
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim ApplicableYear As Integer = 0
    Dim ApplicableMonth As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim FacilityNo As Integer = 0
    Dim QuarterNo As Integer = 0
    Dim PayClassNo As Integer = 0
    Dim clsGeneric As New clsGenericClass


    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"Code", "Bank", "PayGrp", "FileType", "FileName"})
        ViewState("Code") = obj(0)
        ViewState("FileType") = obj(3)
        ViewState("FileName") = obj(4)

        Select Case ViewState("FileType")
            Case "txt"
                GenerateBankTxt(ViewState("FileName"), ViewState("Code"))
            Case "xls"
            Case Else
                MessageBox.Information("Bank File not yet set-up", Me)
        End Select
    End Sub
    Private Sub GenerateBankTxt(ByVal bank As String, ByVal bankno As Integer)

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim path As String = Page.MapPath("documents")
        Dim compno As Integer = cboCompNo.SelectedValue
        Dim grpno As Integer = cboGrpNo.SelectedValue
        Dim paydate As String = txtPayDate.Text
        Dim creditdate As String = txtCreditDate.Text

        Dim filename As String = path & "\" & bank & "_" & Replace(paydate, "/", "") + ".txt"

        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If
        FileHolder = New FileInfo(filename)
        WriteFile = FileHolder.CreateText()

        Dim dstext As DataSet, text As String
        dstext = SQLHelper.ExecuteDataSet("pr_EBankRemit_Web", UserNo, PayLocNo, compno, grpno, paydate, creditdate, bankno)
        If dstext.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To dstext.Tables(0).Rows.Count - 1
                text = Generic.CheckDBNull(dstext.Tables(0).Rows(i)("dtl"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                WriteFile.WriteLine(text)
            Next
        End If


        WriteFile.Close()
        dstext = Nothing
        DownloadFile("../Secured/documents/" & bank & "_" & Replace(paydate, "/", "") + ".txt")
    End Sub
    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayCont_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkSearchGrp_Click(ByVal sender As Object, ByVal e As EventArgs)
        PopulateDropDown(cboGrpNo, 1, cboCompNo.SelectedValue)
    End Sub
    Private Sub PopulateDropDown(ByVal cbo As DropDownList, ByVal mode As Integer, ByVal ref As Integer)
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", Generic.ToInt(Session("xPayLocNo")))
        Try
            cbo.DataSource = SQLHelper.ExecuteDataSet("EDropDownList_WebLookup", UserNo, ref, mode)
            cbo.DataTextField = "tDesc"
            cbo.DataValueField = "tno"
            cbo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        'PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "PayContList.aspx", "EPayCont")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateTabHeader()
        End If


    End Sub


    Private Sub GenerateDisketNew()

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim path As String = Page.MapPath("documents")
        Dim yr As String
        yr = Right(Year(Now), 2)
        Dim filename As String = path & "\" & "SSSNet" & ApplicableMonth.ToString.PadLeft(2, "0") + Right(ApplicableYear, 2) + ".txt"

        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If
        FileHolder = New FileInfo(filename)
        WriteFile = FileHolder.CreateText()

        Dim dstext As DataSet, text As String
        dstext = SQLHelper.ExecuteDataSet("EPayCont_WebDisk_SSSNetMCL_CSV", UserNo, Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull("", Global.clsBase.clsBaseLibrary.enumObjectType.IntType), PayClassNo)
        If dstext.Tables.Count > 0 Then
            If dstext.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To dstext.Tables(0).Rows.Count - 1
                    text = Generic.CheckDBNull(dstext.Tables(0).Rows(i)("WriteInfo"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                    WriteFile.WriteLine(text)
                Next
            End If
        End If

        WriteFile.Close()
        dstext = Nothing

        'Response.Redirect("../Secured/documents/" & "MCL_" & Format(Now, "MMMMdd") & ".txt")

        DownloadFile("../Secured/documents/" & "SSSNet" & ApplicableMonth.ToString.PadLeft(2, "0") + Right(ApplicableYear, 2) + ".txt")
        '(ApplicableMonth) + Right(ApplicableYear, 2) + ".txt")
        'ApplicableMonth.ToString.PadLeft(2, "0") + Right(ApplicableYear, 2) + ".txt")

    End Sub

    Public Function DBFConnection(ByVal filename As String) As OleDb.OleDbConnection
        Try
            DBFConnection = New OleDbConnection
            With DBFConnection
                '.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " & filename & ";Extended Properties=dBASE III" & ";User ID=Lyndonn F. Alesna;Password=pkunzip112"
                '.ConnectionString = "Provider=VFPOLEDB.1;Data Source =" & filename
                '.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" & filename & ";Extended Properties=dBASE III"
                .ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data fSource =" & filename & ";Extended Properties=dBASE III"
                '"Extended Properties=""DBASE IV;"";"
                .Open()
            End With
        Catch ex As Exception
            DBFConnection = Nothing
        End Try

    End Function


    Private Function xDBFConnection(ByVal filename As String) As OleDb.OleDbConnection
        Try
            xDBFConnection = New OleDbConnection
            With xDBFConnection
                '.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " & filename & ";Extended Properties=dBASE III" ';User ID=Admin;Password="
                '.ConnectionString = "Provider=VFPOLEDB.1;Data Source =" & filename
                .ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source =" & filename & ";Extended Properties=dBASE III"

                '"Extended Properties=""DBASE IV;"";"
                .Open()
            End With
        Catch ex As Exception
            xDBFConnection = Nothing
        End Try

    End Function

    Private Sub DownloadFile(ByVal fullpath As String)

        Dim FileName As String = ""
        FileName = IO.Path.GetFileName(fullpath)
        Response.Clear()
        Response.ClearContent()
        Response.ContentType = "application/octet-stream"
        Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
        Response.TransmitFile(fullpath)
        Response.End()

    End Sub

    Protected Sub lnk_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As Button = TryCast(sender, Button)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub


    Private Sub GeneratePHDisk()
        Dim tDst As DataSet
        'Dim tdataset As String

        Dim ttDst As DataSet
        'Dim tttDst As DataSet
        'Dim ttdataset As String, tttdataset As String

        Dim tCompanyName As String
        Dim tFileName As String
        Dim tPath As String
        Dim tSSSNo As String
        Dim tSSSNo1 As String
        Dim tPHNo As String
        Dim tPayLocNo As Integer
        'Dim tHiredDate As String
        'Dim tSeparatedDate As String
        Dim tLastName As String
        Dim tFirstName As String
        Dim tMiddleName As String
        Dim tWriteData As String
        'Dim tNoBirth As String
        'Dim tNoSSS As String

        'Dim tHired_Separated As String
        Dim tStatus As String
        'Dim txHired As String
        'Dim txSeparated As Date
        'Dim tIsseparated As Integer
        Dim tAddress As String
        Dim tQuarter As Integer
        Dim tDe As String
        Dim tRE1 As String
        Dim tRE2 As String
        Dim tRE3 As String
        Dim temployeeph1 As Double = 0
        Dim temployeeph2 As Double = 0
        Dim temployeeph3 As Double = 0
        Dim temployerph1 As Double = 0
        Dim temployerph2 As Double = 0
        Dim temployerph3 As Double = 0
        Dim tCompen As Double = 0
        Dim tDES As String = ""
        Dim tTotPH1 As Double = 0
        Dim tTotPH2 As Double = 0
        Dim tTotPH3 As Double = 0
        Dim tTotHC1 As Integer = 0
        Dim tTotHC2 As Integer = 0
        Dim tTotHC3 As Integer = 0
        Dim tPHSBR1 As String = ""
        Dim tPHSBR2 As String = ""
        Dim tPHSBR3 As String = ""
        Dim tPHDATE1 As String = ""
        Dim tPHDATE2 As String = ""
        Dim tPHDATE3 As String = ""
        Dim tgTotal As Double
        Dim tPHIss1 As String = ""
        Dim tPHIss2 As String = ""
        Dim tPHIss3 As String = ""
        Dim tPosition As String = ""
        Dim tFullName As String = ""

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Try
            tQuarter = QuarterNo
            Dim SignatoryNo As Integer = Generic.CheckDBNull(0, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)


            tDst = SQLHelper.ExecuteDataSet("SELECT isnull(PayLocDesc,'') as Companyname,isnull(SSSNO,'') as SSSNo,PayLocNo,PayLocCode,Address from dbo.EPayLoc Where PayLocNo=" & PayLocNo & " Order by PayLocDesc")

            If tDst.Tables(0).Rows.Count > 0 Then
                Dim tcount As Integer
                Dim pRow As DataRow
                For tcount = 0 To tDst.Tables(0).Rows.Count - 1
                    pRow = tDst.Tables(0).Rows(CInt(tcount))
                    tCompanyName = Generic.CheckDBNull(pRow!CompanyName, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                    tAddress = Generic.CheckDBNull(pRow!Address, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                    tSSSNo = Replace(Generic.CheckDBNull(pRow!SSSNo, Global.clsBase.clsBaseLibrary.enumObjectType.StrType), "-", "")
                    tPayLocNo = Generic.CheckDBNull(pRow!payLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                    tPath = (Page.MapPath("Contribution"))

                    If Not IO.Directory.Exists(tPath) Then
                        IO.Directory.CreateDirectory(tPath)
                    End If
                    tFileName = tPath & "\PHQuarter_" & tCompanyName & Str(tQuarter) & Str(ApplicableYear) & ".txt"

                    FileHolder = New FileInfo(tFileName)
                    WriteFile = FileHolder.CreateText()

                    WriteFile.WriteLine("REMITTANCE REPORT")
                    WriteFile.WriteLine(tCompanyName)
                    WriteFile.WriteLine(tAddress)
                    WriteFile.WriteLine(Pad.PadZero(12, tSSSNo) & Pad.PadZero(1, tQuarter) & Pad.PadZero(4, ApplicableYear) & "R")
                    WriteFile.WriteLine("MEMBERS")

                    ttDst = SQLHelper.ExecuteDataSet("SELECT      LastName, FirstName, MiddleInitial, SSSNo,PhNo,EmployeePh1,EmployeePh2,EmployeePh3,EmployerPh1,EmployerPh2,EmployerPh3,RE1,RE2,RE3,DE,Compen FROM EPayContDetiPHDisk WHERE  xQuarter=" & tQuarter & " and applicableYear=" & Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType) & " and PayLocNo=" & tPayLocNo & " Order by Lastname,firstname,MiddleInitial")

                    If ttDst.Tables(0).Rows.Count > 0 Then
                        Dim ttcount As Integer
                        Dim pprow As DataRow
                        For ttcount = 0 To ttDst.Tables(0).Rows.Count - 1
                            pprow = ttDst.Tables(0).Rows(CInt(ttcount))
                            tPHNo = Generic.CheckDBNull(pprow!PHNO, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            tSSSNo1 = Generic.CheckDBNull(pprow!SSSNo, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            tLastName = Generic.CheckDBNull(pprow!Lastname, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            tFirstName = Generic.CheckDBNull(pprow!FirstName, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            tMiddleName = Generic.CheckDBNull(pprow!MiddleInitial, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            temployeeph1 = Generic.CheckDBNull(pprow!employeeph1, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                            temployeeph2 = Generic.CheckDBNull(pprow!employeeph2, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                            temployeeph3 = Generic.CheckDBNull(pprow!employeeph3, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                            temployerph1 = Generic.CheckDBNull(pprow!employerph1, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                            temployerph2 = Generic.CheckDBNull(pprow!employerph2, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                            temployerph3 = Generic.CheckDBNull(pprow!employerph3, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                            tCompen = Generic.CheckDBNull(pprow!Compen, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

                            tLastName = tLastName & Space(30 - Len(tLastName))
                            tFirstName = tFirstName & Space(30 - Len(tFirstName))
                            tMiddleName = tMiddleName & Space(1 - Len(tMiddleName))

                            tDe = Generic.CheckDBNull(pprow!DE, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            If tDe <> "" Then
                                tDES = Pad.PadZero(2, Month(tDe)) & Pad.PadZero(2, Microsoft.VisualBasic.Day(tDe)) & Pad.PadZero(4, Year(tDe))
                            Else
                                tDES = ""
                            End If

                            tRE1 = Generic.CheckDBNull(pprow!RE1, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            tRE2 = Generic.CheckDBNull(pprow!RE2, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            tRE3 = Generic.CheckDBNull(pprow!RE3, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            If Len(tRE1) > 0 Then
                                tStatus = tRE1
                            ElseIf Len(tRE2) > 0 Then
                                tStatus = tRE2
                            ElseIf Len(tRE3) > 0 Then
                                tStatus = tRE3
                            Else
                                tStatus = ""
                            End If

                            tPHNo = Replace(tPHNo, "-", "")
                            If Len(tPHNo) = 9 Or Mid(tPHNo, 1, 6) = "000000" Or Mid(tPHNo, 1, 2) = "00" Or Mid(tPHNo, 1, 12) = Space(12) Then
                                tPHNo = tSSSNo1
                            End If
                            If tPHNo = "" Then
                                tPHNo = tSSSNo1
                            End If

                            tWriteData = Pad.PadSpace(tPHNo, 12) & tLastName & tFirstName & tMiddleName & Pad.PadNetPay(tCompen, 8)
                            tWriteData = tWriteData & Pad.PadNetPay(temployeeph1, 6) & Pad.PadNetPay(temployerph1, 6) & Pad.PadNetPay(temployeeph2, 6) & Pad.PadNetPay(temployerph2, 6) & Pad.PadNetPay(temployeeph3, 6) & Pad.PadNetPay(temployerph3, 6)
                            tWriteData = tWriteData & tStatus & tDES
                            WriteFile.WriteLine(tWriteData)
                            tTotPH1 = tTotPH1 + temployeeph1 + temployerph1
                            tTotPH2 = tTotPH2 + temployeeph2 + temployerph2
                            tTotPH3 = tTotPH3 + temployeeph3 + temployerph3



                        Next
                    End If


                    ttDst = SQLHelper.ExecuteDataSet("SELECT * FROM dbo.EPayContPHDisk(" & tQuarter & "," & ApplicableYear & "," & SignatoryNo & ")")

                    If ttDst.Tables(0).Rows.Count > 0 Then
                        Dim tttcount As Integer
                        Dim pprow1 As DataRow
                        For tttcount = 0 To ttDst.Tables(0).Rows.Count - 1
                            pprow1 = ttDst.Tables(0).Rows(CInt(tttcount))
                            tPHSBR1 = Generic.CheckDBNull(pprow1!phsbr1, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            tPHSBR2 = Generic.CheckDBNull(pprow1!phsbr2, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            tPHSBR3 = Generic.CheckDBNull(pprow1!phsbr3, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            tPHDATE1 = Generic.CheckDBNull(pprow1!phDate1, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            tPHDATE2 = Generic.CheckDBNull(pprow1!phDate2, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            tPHDATE3 = Generic.CheckDBNull(pprow1!phDate3, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            tTotHC1 = Generic.CheckDBNull(pprow1!phhc1, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                            tTotHC2 = Generic.CheckDBNull(pprow1!phhc2, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                            tTotHC3 = Generic.CheckDBNull(pprow1!phhc3, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                            tFullName = Generic.CheckDBNull(pprow1!fullName, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            tPosition = Generic.CheckDBNull(pprow1!PositionDesc, Global.clsBase.clsBaseLibrary.enumObjectType.StrType)


                        Next
                        If tPHSBR1 = "" Then
                            MessageBox.Warning("SBR No. 1 not found.", Me)
                            Exit Sub
                        End If
                        If tPHSBR2 = "" Then
                            MessageBox.Warning("SBR No. 2 not found.", Me)
                            Exit Sub
                        End If
                        If tPHSBR3 = "" Then
                            MessageBox.Warning("SBR No. 3 not found.", Me)
                            Exit Sub
                        End If

                        If tPHDATE1 = "" Then
                            MessageBox.Warning("SBR Date 1 not found.", Me)
                            Exit Sub
                        End If
                        If tPHDATE2 = "" Then
                            MessageBox.Warning("SBR Date 2 not found.", Me)
                            Exit Sub
                        End If
                        If tPHDATE3 = "" Then
                            MessageBox.Warning("SBR Date 3 not found.", Me)
                            Exit Sub
                        End If



                        tPHIss1 = Pad.PadZero(2, Month(tPHDATE1)) & Pad.PadZero(2, Microsoft.VisualBasic.Day(tPHDATE1)) & Pad.PadZero(4, Year(tPHDATE1))
                        tPHIss2 = Pad.PadZero(2, Month(tPHDATE2)) & Pad.PadZero(2, Microsoft.VisualBasic.Day(tPHDATE2)) & Pad.PadZero(4, Year(tPHDATE2))
                        tPHIss3 = Pad.PadZero(2, Month(tPHDATE3)) & Pad.PadZero(2, Microsoft.VisualBasic.Day(tPHDATE3)) & Pad.PadZero(4, Year(tPHDATE3))

                        tWriteData = ""
                        tgTotal = Generic.CheckDBNull(tTotPH1, Global.clsBase.clsBaseLibrary.enumObjectType.IntType) + Generic.CheckDBNull(tTotPH2, Global.clsBase.clsBaseLibrary.enumObjectType.IntType) + Generic.CheckDBNull(tTotPH3, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                        WriteFile.WriteLine("MS-SUMMARY")
                        WriteFile.WriteLine("1" & Pad.PadNetPay(tTotPH1, 8) & Pad.PadStaticSpace(tPHSBR1, 15, "L") & Pad.PadSpace(tPHIss1, 8) & CStr(tTotHC1))
                        WriteFile.WriteLine("2" & Pad.PadNetPay(tTotPH2, 8) & Pad.PadStaticSpace(tPHSBR2, 15, "L") & Pad.PadSpace(tPHIss2, 8) & CStr(tTotHC2))
                        WriteFile.WriteLine("3" & Pad.PadNetPay(tTotPH3, 8) & Pad.PadStaticSpace(tPHSBR3, 15, "L") & Pad.PadSpace(tPHIss3, 8) & CStr(tTotHC3))
                        WriteFile.WriteLine("GRAND TOTAL" & Pad.PadNetPay(tgTotal, 10))
                        WriteFile.WriteLine(Pad.PadSpace(tFullName, 40) & Pad.PadSpace(tPosition, 20))




                    End If
                    WriteFile.Close()
                    DownloadFile(tFileName)
                Next


            End If
        Catch ex As Exception
            'WriteFile.Close()
        End Try
    End Sub
    Private Sub GeneratePHDiskMonthly()

        Try
            Dim FileHolder As FileInfo
            Dim WriteFile As StreamWriter
            Dim path As String = Page.MapPath("documents")
            Dim yr As String
            yr = Right(Year(Now), 2)
            Dim filename As String = path & "\" & "PH" & Pad.PadZero(2, ApplicableMonth) + Pad.PadZero(4, ApplicableYear) + ".txt"

            If Not IO.Directory.Exists(path) Then
                IO.Directory.CreateDirectory(path)
            End If
            FileHolder = New FileInfo(filename)
            WriteFile = FileHolder.CreateText()

            Dim dstext As DataSet, text As String
            dstext = SQLHelper.ExecuteDataSet("EPayCont_WebDisk_PHMonthly", UserNo, Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(0, Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
            If dstext.Tables.Count > 0 Then
                If dstext.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To dstext.Tables(0).Rows.Count - 1
                        text = Generic.CheckDBNull(dstext.Tables(0).Rows(i)("Detail"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        WriteFile.WriteLine(text)
                    Next
                End If
            End If

            WriteFile.Close()
            dstext = Nothing


            DownloadFile("../Secured/documents/" & "PH" & Pad.PadZero(2, ApplicableMonth) + Pad.PadZero(4, ApplicableYear) + ".txt")

        Catch ex As Exception

        End Try
    End Sub
    Private Sub GenerateDisket()

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim path As String = Page.MapPath("documents")
        Dim yr As String
        yr = Right(Year(Now), 2)
        Dim filename As String = path & "\" & "SSSNet" & ApplicableMonth.ToString.PadLeft(2, "0") + Right(ApplicableYear, 2) + ".txt"

        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If
        FileHolder = New FileInfo(filename)
        WriteFile = FileHolder.CreateText()

        Dim dstext As DataSet, text As String
        dstext = SQLHelper.ExecuteDataSet("EPayCont_WebDisk_SSSNetMCL", UserNo, Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull("", Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
        If dstext.Tables.Count > 0 Then
            If dstext.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To dstext.Tables(0).Rows.Count - 1
                    text = Generic.CheckDBNull(dstext.Tables(0).Rows(i)("Detail"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                    WriteFile.WriteLine(text)
                Next
            End If
        End If

        WriteFile.Close()
        dstext = Nothing

        'Response.Redirect("../Secured/documents/" & "MCL_" & Format(Now, "MMMMdd") & ".txt")

        DownloadFile("../Secured/documents/" & "SSSNet" & ApplicableMonth.ToString.PadLeft(2, "0") + Right(ApplicableYear, 2) + ".txt")
        '(ApplicableMonth) + Right(ApplicableYear, 2) + ".txt")
        'ApplicableMonth.ToString.PadLeft(2, "0") + Right(ApplicableYear, 2) + ".txt")

    End Sub
    Private Sub GenerateDisketOrig()

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim path As String = Page.MapPath("documents")
        Dim yr As String
        yr = Right(Year(Now), 2)
        Dim filename As String = path & "\" & "SSSNet" & ApplicableMonth.ToString.PadLeft(2, "0") + Right(ApplicableYear, 2) + ".txt"

        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If
        FileHolder = New FileInfo(filename)
        WriteFile = FileHolder.CreateText()

        Dim dstext As DataSet, text As String
        dstext = SQLHelper.ExecuteDataSet("EPayCont_WebDisk_SSSNetMCLOrig", UserNo, Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull("", Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
        If dstext.Tables.Count > 0 Then
            If dstext.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To dstext.Tables(0).Rows.Count - 1
                    text = Generic.CheckDBNull(dstext.Tables(0).Rows(i)("Detail"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                    WriteFile.WriteLine(text)
                Next
            End If
        End If

        WriteFile.Close()
        dstext = Nothing

        'Response.Redirect("../Secured/documents/" & "MCL_" & Format(Now, "MMMMdd") & ".txt")

        DownloadFile("../Secured/documents/" & "SSSNet" & ApplicableMonth.ToString.PadLeft(2, "0") + Right(ApplicableYear, 2) + ".txt")
        '(ApplicableMonth) + Right(ApplicableYear, 2) + ".txt")
        'ApplicableMonth.ToString.PadLeft(2, "0") + Right(ApplicableYear, 2) + ".txt")

    End Sub
    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As EventArgs)
        If cboCompNo.SelectedValue = "0" Or cboGrpNo.SelectedValue = "0" Or txtCreditDate.Text = "" Or txtPayDate.Text = "" Then
            MessageBox.Information("Incomplete inputs", Me)
        Else
            PopulateGrid()
        End If
    End Sub

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim compno As Integer = cboCompNo.SelectedValue
            Dim grpno As Integer = cboGrpNo.SelectedValue
            Dim paydate As String = txtPayDate.Text
            Dim creditdate As String = txtCreditDate.Text
            dt = SQLHelper.ExecuteDataTable("pr_EBankRemit_Web", UserNo, PayLocNo, compno, grpno, paydate, creditdate, 0)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub GenerateDisketLoan()


        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim path As String = Page.MapPath("documents")
        Dim filename As String = path & "\" & "SSSLoan_" & Format(Now, "MMMMdd") & ".txt"

        'If fUPload.HasFile Then
        'End If


        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If
        FileHolder = New FileInfo(filename)
        WriteFile = FileHolder.CreateText()

        Dim dstext As DataSet, text As String
        dstext = SQLHelper.ExecuteDataSet("EPayCont_WebDisk_SSSLoan", Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
        If dstext.Tables.Count > 0 Then
            If dstext.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To dstext.Tables(0).Rows.Count - 1
                    text = Generic.CheckDBNull(dstext.Tables(0).Rows(i)("Detail"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                    WriteFile.WriteLine(text)
                Next
            End If
        End If

        WriteFile.Close()
        dstext = Nothing
        'Response.Redirect("../Secured/documents/" & "SSSLoan_" & Format(Now, "MMMMdd") & ".CSV")
        DownloadFile("../Secured/documents/" & "SSSLoan_" & Format(Now, "MMMMdd") & ".txt")

    End Sub
    Private Sub GenerateDisketEPF()

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim path As String = Page.MapPath("documents")
        Dim yr As String
        yr = Right(Year(Now), 2)
        Dim filename As String = path & "\" & "EPF" & ApplicableMonth.ToString.PadLeft(2, "0") + Right(ApplicableYear, 2) + ".txt"

        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If
        FileHolder = New FileInfo(filename)
        WriteFile = FileHolder.CreateText()

        Dim dstext As DataSet, text As String
        dstext = SQLHelper.ExecuteDataSet("EPayCont_WebDisk_SSSNetEPFAll", UserNo, Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
        If dstext.Tables.Count > 0 Then
            If dstext.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To dstext.Tables(0).Rows.Count - 1
                    text = Generic.CheckDBNull(dstext.Tables(0).Rows(i)("Detail"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                    WriteFile.WriteLine(text)
                Next
            End If
        End If

        WriteFile.Close()
        dstext = Nothing

        DownloadFile("../Secured/documents/" & "EPF" & ApplicableMonth.ToString.PadLeft(2, "0") + Right(ApplicableYear, 2) + ".txt")


    End Sub
    Private Sub GenerateDisketEPFNew()

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim path As String = Page.MapPath("documents")
        Dim yr As String
        yr = Right(Year(Now), 2)
        Dim filename As String = path & "\" & "EPF" & ApplicableMonth.ToString.PadLeft(2, "0") + Right(ApplicableYear, 2) + ".txt"

        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If
        FileHolder = New FileInfo(filename)
        WriteFile = FileHolder.CreateText()

        Dim dstext As DataSet, text As String
        dstext = SQLHelper.ExecuteDataSet("EPayCont_WebDisk_SSSNetEPFNew", UserNo, Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
        If dstext.Tables.Count > 0 Then
            If dstext.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To dstext.Tables(0).Rows.Count - 1
                    text = Generic.CheckDBNull(dstext.Tables(0).Rows(i)("Detail"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                    WriteFile.WriteLine(text)
                Next
            End If
        End If

        WriteFile.Close()
        dstext = Nothing

        DownloadFile("../Secured/documents/" & "EPF" & ApplicableMonth.ToString.PadLeft(2, "0") + Right(ApplicableYear, 2) + ".txt")


    End Sub


    Private Sub lnkHDMFPrem()


        Try

            Dim dbfString As String, deleteString As String
            Dim dbfString2 As String
            Dim sfilename As String = ""
            Dim path As String = Page.MapPath("documents")
            Dim myCon As OleDbConnection

            path = Page.MapPath("docs\")

            myCon = xDBFConnection(path)

            sfilename = "EC" & Format(Now, "HHmmss") & ".DBF"
            sfilename = Replace(sfilename, ":", "")
            'sfilename = Microsoft.VisualBasic.Left(sfilename, Len(sfilename) - 4
            'If Dir(path & "\EECONT.DBF") <> "" Then

            '    Kill((path & "\EECONT.DBF"))
            'End If
            If Dir(path & "\documents\EECONT.DBF") <> "" Then

                Kill((path & "documents\EECONT.DBF"))
            End If

            deleteString = "Delete from " & sfilename
            dbfString = "CREATE TABLE " & sfilename & "(Eyerid CHAR(15),Hdmfid CHAR(15),Eyeeno CHAR(12),Lname CHAR(17),Fname CHAR(17), Midname CHAR(17), Peramt1 numeric(4,2) , Peramt2  numeric(4,2) )"
            'dbfString = " "
            'ALTER [COLUMN] FieldName1 
            '   FieldType [( nFieldWidth [, nPrecision])] 
            Dim cmd1 As New OleDbCommand(dbfString, myCon)
            Dim cmd5 As New OleDbCommand(dbfString2, myCon)
            Dim cmd4 As New OleDbCommand(deleteString, myCon)
            cmd1.ExecuteNonQuery()
            'cmd5.ExecuteNonQuery()
            cmd4.ExecuteNonQuery()


            Dim dbfCMD As OleDbCommand
            Dim dbfDA As OleDbDataAdapter
            Dim dbfDS As DataSet

            dbfCMD = New OleDbCommand
            dbfDA = New OleDbDataAdapter
            dbfDS = New DataSet

            dbfCMD.CommandText = "select * from  " & sfilename
            dbfCMD.Connection = myCon
            dbfDA.SelectCommand = dbfCMD
            dbfDA.Fill(dbfDS, "dbf")

            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet(SQLHelper.ConSTR, "EPayCont_WebDisk_HDMFPremium", Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1


                        Dim dr As DataRow
                        dr = dbfDS.Tables(0).NewRow
                        dr("Eyerid") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("companyhdmfno"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Hdmfid") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("hdmfno"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Eyeeno") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("employeecode"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Lname") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("lastname"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Fname") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("firstname"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Midname") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("middlename"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Peramt1") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("hdmfee"), Global.clsBase.clsBaseLibrary.enumObjectType.DblType)
                        dr("Peramt2") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("hdmfer"), Global.clsBase.clsBaseLibrary.enumObjectType.DblType)



                        dbfDS.Tables(0).Rows.Add(dr)
                        Dim cb As New OleDbCommandBuilder(dbfDA)



                        dbfDA.Update(dbfDS, "dbf")
                    Next
                End If
            End If
            'DBFConnectionClose(path)
            'DownloadFile(path & "\EECONT.DBF")
            'cmd5.ExecuteNonQuery()
            If Not IO.Directory.Exists(path & "documents\") Then
                IO.Directory.CreateDirectory(path & "documents\")
            End If
            FileCopy(path & sfilename, path & "documents\EECONT.DBF")
            DownloadFile(path & "documents\EECONT.DBF")


        Catch ex As Exception
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End Try


    End Sub

    Private Sub lnkHDMFPremiumLoan()
        Try

            Dim dbfString As String, deleteString As String
            'Dim dbfString2 As String
            Dim sfilename As String = ""
            Dim path As String = Page.MapPath("documents")
            Dim myCon As OleDbConnection

            path = Server.MapPath("docs\")

            'myCon = clsbase.xDBFConnection(path)
            myCon = DBFConnection(path)

            sfilename = "LN" & Format(Now, "HHmmss") & ".DBF"
            sfilename = Replace(sfilename, ":", "")
            'sfilename = Microsoft.VisualBasic.Left(sfilename, Len(sfilename) - 4
            'If Dir(path & "\EECONT.DBF") <> "" Then

            '    Kill((path & "\EECONT.DBF"))
            'End If
            If Dir(path & "LN" & Format(Now, "HHmmss") & ".DBF") <> "" Then

                Kill((path & "LN" & Format(Now, "HHmmss") & ".DBF"))
            End If
            If Dir(path & "\documents\EECONTLN.DBF") <> "" Then
                Kill((path & "documents\EECONTLN.DBF"))
            End If

            deleteString = "Delete from " & sfilename
            dbfString = "CREATE TABLE " & sfilename & "(Eyerid CHAR(15),eyeeid CHAR(15),mid_trn CHAR(12),membership CHAR(2),Lname CHAR(17),Fname CHAR(17),name_exten CHAR(17), mid CHAR(17), percov CHAR(10), ee numeric(6,2) , er  numeric(6,2), amort  numeric(6,2), remarks  char(50) )"


            'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Error!','" + path + " ^ " + sfilename + " ^ " + dbfString + " ^ " + deleteString + "');")

            'dbfString = " "
            'ALTER [COLUMN] FieldName1 
            '   FieldType [( nFieldWidth [, nPrecision])] 
            Dim cmd1 As New OleDbCommand(dbfString, myCon)
            'Dim cmd5 As New OleDbCommand(dbfString2, myCon)
            Dim cmd4 As New OleDbCommand(deleteString, myCon)
            cmd1.ExecuteNonQuery()
            'cmd5.ExecuteNonQuery()
            cmd4.ExecuteNonQuery()


            Dim dbfCMD As OleDbCommand
            Dim dbfDA As OleDbDataAdapter
            Dim dbfDS As DataSet

            dbfCMD = New OleDbCommand
            dbfDA = New OleDbDataAdapter
            dbfDS = New DataSet

            dbfCMD.CommandText = "select * from  " & sfilename
            dbfCMD.Connection = myCon
            dbfDA.SelectCommand = dbfCMD
            dbfDA.Fill(dbfDS, "dbf")

            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet(SQLHelper.ConSTR, "EPayCont_WebDisk_HDMFPremiumLoan_DBF", UserNo, Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1


                        Dim dr As DataRow
                        dr = dbfDS.Tables(0).NewRow
                        dr("Eyerid") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("companyhdmfno"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Eyeeid") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("hdmfno"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("mid_trn") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("MID_TRN"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("membership") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("membership"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Lname") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("lastname"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Fname") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("firstname"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("name_exten") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("name_extn"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("mid") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("middlename"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("percov") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("percov"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("ee") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("hdmfee"), Global.clsBase.clsBaseLibrary.enumObjectType.DblType)
                        dr("er") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("hdmfer"), Global.clsBase.clsBaseLibrary.enumObjectType.DblType)
                        dr("amort") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("amort"), Global.clsBase.clsBaseLibrary.enumObjectType.DblType)
                        dr("remarks") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("remarks"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)

                        dbfDS.Tables(0).Rows.Add(dr)
                        Dim cb As New OleDbCommandBuilder(dbfDA)
                        dbfDA.Update(dbfDS, "dbf")
                    Next
                End If
            End If
            'DBFConnectionClose(path)
            'DownloadFile(path & "\EECONT.DBF")
            'cmd5.ExecuteNonQuery()
            If Not IO.Directory.Exists(path & "documents\") Then
                IO.Directory.CreateDirectory(path & "documents\")
            End If
            FileCopy(path & sfilename, path & "documents\EECONTLN.DBF")
            DownloadFile(path & "documents\EECONTLN.DBF")

        Catch ex As Exception
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End Try

    End Sub
    Private Sub lnkHDMFCalamityLoan()
        Try

            Dim dbfString As String, deleteString As String
            'Dim dbfString2 As String
            Dim sfilename As String = ""
            Dim path As String = Page.MapPath("documents")
            Dim myCon As OleDbConnection

            path = Page.MapPath("docs\")

            'myCon = clsbase.xDBFConnection(path)
            myCon = DBFConnection(path)

            sfilename = "LN" & Format(Now, "HHmmss") & ".DBF"
            sfilename = Replace(sfilename, ":", "")
            'sfilename = Microsoft.VisualBasic.Left(sfilename, Len(sfilename) - 4
            'If Dir(path & "\EECONT.DBF") <> "" Then

            '    Kill((path & "\EECONT.DBF"))
            'End If
            If Dir(path & "LN" & Format(Now, "HHmmss") & ".DBF") <> "" Then

                Kill((path & "LN" & Format(Now, "HHmmss") & ".DBF"))
            End If
            If Dir(path & "\documents\EECONTLN.DBF") <> "" Then
                Kill((path & "documents\EECONTLN.DBF"))
            End If

            deleteString = "Delete from " & sfilename
            dbfString = "CREATE TABLE " & sfilename & "(Eyerid CHAR(15),eyeeid CHAR(15),mid_trn CHAR(12),membership CHAR(2),Lname CHAR(17),Fname CHAR(17),name_exten CHAR(17), mid CHAR(17), percov CHAR(10), ee numeric(6,2) , er  numeric(6,2), amort  numeric(6,2), remarks  char(50) )"


            'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Error!','" + path + " ^ " + sfilename + " ^ " + dbfString + " ^ " + deleteString + "');")

            'dbfString = " "
            'ALTER [COLUMN] FieldName1 
            '   FieldType [( nFieldWidth [, nPrecision])] 
            Dim cmd1 As New OleDbCommand(dbfString, myCon)
            'Dim cmd5 As New OleDbCommand(dbfString2, myCon)
            Dim cmd4 As New OleDbCommand(deleteString, myCon)
            cmd1.ExecuteNonQuery()
            'cmd5.ExecuteNonQuery()
            cmd4.ExecuteNonQuery()


            Dim dbfCMD As OleDbCommand
            Dim dbfDA As OleDbDataAdapter
            Dim dbfDS As DataSet

            dbfCMD = New OleDbCommand
            dbfDA = New OleDbDataAdapter
            dbfDS = New DataSet

            dbfCMD.CommandText = "select * from  " & sfilename
            dbfCMD.Connection = myCon
            dbfDA.SelectCommand = dbfCMD
            dbfDA.Fill(dbfDS, "dbf")

            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet(SQLHelper.ConSTR, "EPayCont_WebDisk_HDMFCalamityLoan_DBF", UserNo, Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1


                        Dim dr As DataRow
                        dr = dbfDS.Tables(0).NewRow
                        dr("Eyerid") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("companyhdmfno"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Eyeeid") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("hdmfno"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("mid_trn") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("MID_TRN"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("membership") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("membership"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Lname") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("lastname"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Fname") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("firstname"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("name_exten") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("name_extn"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("mid") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("middlename"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("percov") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("percov"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("ee") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("hdmfee"), Global.clsBase.clsBaseLibrary.enumObjectType.DblType)
                        dr("er") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("hdmfer"), Global.clsBase.clsBaseLibrary.enumObjectType.DblType)
                        dr("amort") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("amort"), Global.clsBase.clsBaseLibrary.enumObjectType.DblType)
                        dr("remarks") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("remarks"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)

                        dbfDS.Tables(0).Rows.Add(dr)
                        Dim cb As New OleDbCommandBuilder(dbfDA)
                        dbfDA.Update(dbfDS, "dbf")
                    Next
                End If
            End If
            'DBFConnectionClose(path)
            'DownloadFile(path & "\EECONT.DBF")
            'cmd5.ExecuteNonQuery()
            If Not IO.Directory.Exists(path & "documents\") Then
                IO.Directory.CreateDirectory(path & "documents\")
            End If
            FileCopy(path & sfilename, path & "documents\EECONTLN.DBF")
            DownloadFile(path & "documents\EECONTLN.DBF")

        Catch ex As Exception
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End Try

    End Sub
    Private Sub lnkHDMFPremT()
        Try
            Dim FileHolder As FileInfo
            Dim WriteFile As StreamWriter
            Dim path As String = Page.MapPath("documents")
            Dim yr As String
            yr = Right(Year(Now), 2)
            Dim filename As String = path & "\" & "HDMF Premium" & Pad.PadZero(2, ApplicableMonth) + Pad.PadZero(4, ApplicableYear) + ".txt"

            If Not IO.Directory.Exists(path) Then
                IO.Directory.CreateDirectory(path)
            End If
            FileHolder = New FileInfo(filename)
            WriteFile = FileHolder.CreateText()

            Dim dstext As DataSet, text As String
            dstext = SQLHelper.ExecuteDataSet("EPayContWeb_HDMFPremiumText", UserNo, Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
            If dstext.Tables.Count > 0 Then
                If dstext.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To dstext.Tables(0).Rows.Count - 1
                        text = Generic.CheckDBNull(dstext.Tables(0).Rows(i)("Detail"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        WriteFile.WriteLine(text)
                    Next
                End If
            End If

            WriteFile.Close()
            dstext = Nothing



            DownloadFile("../Secured/documents/" & "HDMF Premium" & Pad.PadZero(2, ApplicableMonth) + Pad.PadZero(4, ApplicableYear) + ".txt")

        Catch ex As Exception
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End Try
    End Sub
    Private Sub lnkHDMFPremCSV()
        Try
            Dim FileHolder As FileInfo
            Dim WriteFile As StreamWriter
            Dim path As String = Page.MapPath("documents")
            Dim yr As String
            yr = Right(Year(Now), 2)
            Dim filename As String = path & "\" & "HDMF_Premium" & Pad.PadZero(2, ApplicableMonth) + Pad.PadZero(4, ApplicableYear) + ".csv"

            If Not IO.Directory.Exists(path) Then
                IO.Directory.CreateDirectory(path)
            End If
            FileHolder = New FileInfo(filename)
            WriteFile = FileHolder.CreateText()

            Dim dstext As DataSet, text As String
            dstext = SQLHelper.ExecuteDataSet(SQLHelper.ConSTR, "EPayCont_WebDisk_HDMFPremiumCSV", UserNo, Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
            If dstext.Tables.Count > 0 Then
                If dstext.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To dstext.Tables(0).Rows.Count - 1
                        text = Generic.CheckDBNull(dstext.Tables(0).Rows(i)("Detail"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        WriteFile.WriteLine(text)
                    Next
                End If
            End If

            WriteFile.Close()
            dstext = Nothing



            DownloadFile("../Secured/documents/" & "HDMF_Premium" & Pad.PadZero(2, ApplicableMonth) + Pad.PadZero(4, ApplicableYear) + ".csv")

        Catch ex As Exception
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End Try
    End Sub

    Private Sub lnkHDMFNew()
        Try
            Dim mycon As OleDbConnection
            Dim dbfString As String, deleteString As String
            Dim sfilename As String = ""
            Dim path As String = Page.MapPath("documents")
            path = Page.MapPath("docs\")
            mycon = xDBFConnection(path)
            sfilename = "NW" & Format(Now, "HHmmss") & ".DBF"
            sfilename = Replace(sfilename, ":", "")
            'sfilename = Microsoft.VisualBasic.Left(sfilename, Len(sfilename) - 4
            If Dir(path & "\NEW.DBF") <> "" Then
                Kill((path & "\NEW.DBF"))
            End If
            'If Dir(path & "\documents\NEW.DBF") <> "" Then

            '    Kill((path & "documents\NEW.DBF"))
            'End If

            deleteString = "Delete from " & sfilename
            dbfString = "CREATE TABLE " & sfilename & "(Eyerid VARCHAR(15),Hdmfid CHAR(15),Eyeeno CHAR(13),Lname CHAR(17),Fname CHAR(17), Midname CHAR(17), Birthdate datetime)"
            Dim cmd1 As New OleDbCommand(dbfString, mycon)
            Dim cmd4 As New OleDbCommand(deleteString, mycon)
            cmd1.ExecuteNonQuery()
            cmd4.ExecuteNonQuery()


            Dim dbfCMD As OleDbCommand
            Dim dbfDA As OleDbDataAdapter
            Dim dbfDS As DataSet

            dbfCMD = New OleDbCommand
            dbfDA = New OleDbDataAdapter
            dbfDS = New DataSet

            dbfCMD.CommandText = "select * from  " & sfilename
            dbfCMD.Connection = mycon
            dbfDA.SelectCommand = dbfCMD
            dbfDA.Fill(dbfDS, "dbf")

            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet(SQLHelper.ConSTR, "EPayCont_WebDisk_HDMFEPF_Premium", Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1


                        Dim dr As DataRow
                        dr = dbfDS.Tables(0).NewRow
                        dr("Eyerid") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("companyhdmfno"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Hdmfid") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("hdmfno"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Eyeeno") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("employeecode"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Lname") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("lastname"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Fname") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("firstname"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Midname") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("middlename"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Birthdate") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("birthdate"), Global.clsBase.clsBaseLibrary.enumObjectType.DblType)



                        dbfDS.Tables(0).Rows.Add(dr)
                        Dim cb As New OleDbCommandBuilder(dbfDA)



                        dbfDA.Update(dbfDS, "dbf")
                    Next
                End If
            End If
            If Not IO.Directory.Exists(path & "documents\") Then
                IO.Directory.CreateDirectory(path & "documents\")
            End If
            FileCopy(path & sfilename, path & "documents\NEW.DBF")
            DownloadFile(path & "documents\NEW.DBF")
            'DownloadFile(path & "\NEW.DBF")

        Catch ex As Exception
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End Try



    End Sub

    Private Sub lnkHDMFSummary()
        Try
            Dim mycon As OleDbConnection
            Dim dbfString As String, deleteString As String
            Dim sfilename As String = ""
            Dim path As String = Page.MapPath("documents")
            path = Page.MapPath("docs\")
            mycon = xDBFConnection(path)
            sfilename = "ER" & Format(Now, "HHmmss") & ".DBF"
            sfilename = Replace(sfilename, ":", "")
            'sfilename = Microsoft.VisualBasic.Left(sfilename, Len(sfilename) - 4
            'If Dir(path & "\ERDATA.DBF") <> "" Then
            '    Kill((path & "\ERDATA.DBF"))
            'End If
            If Dir(path & "\documents\ERDATA.DBF") <> "" Then

                Kill((path & "documents\ERDATA.DBF"))
            End If

            deleteString = "Delete from " & sfilename
            dbfString = "CREATE TABLE " & sfilename & "(Eyerid VARCHAR(12),Eyername VARCHAR(40),Eyeraddr VARCHAR(40),Pfrno varchar(7), Pfrdate date, Pframt numeric(10,2), Periodcov VARCHAR(4))"
            Dim cmd1 As New OleDbCommand(dbfString, mycon)
            Dim cmd4 As New OleDbCommand(deleteString, mycon)
            cmd1.ExecuteNonQuery()
            cmd4.ExecuteNonQuery()

            Dim dbfCMD As OleDbCommand
            Dim dbfDA As OleDbDataAdapter
            Dim dbfDS As DataSet

            dbfCMD = New OleDbCommand
            dbfDA = New OleDbDataAdapter
            dbfDS = New DataSet

            dbfCMD.CommandText = "select * from  " & sfilename
            dbfCMD.Connection = mycon
            dbfDA.SelectCommand = dbfCMD
            dbfDA.Fill(dbfDS, "dbf")

            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet(SQLHelper.ConSTR, "EPayCont_WebDisk_HDMFERDATA", Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1


                        Dim dr As DataRow
                        dr = dbfDS.Tables(0).NewRow
                        dr("Eyerid") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("companyhdmfno"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Eyername") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("companyname"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Eyeraddr") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("companyaddress"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Pfrno") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("pfrno"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Pfrdate") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("pfrdate"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("Pframt") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("pframount"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                        dr("Periodcov") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("periodcovered"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)



                        dbfDS.Tables(0).Rows.Add(dr)
                        Dim cb As New OleDbCommandBuilder(dbfDA)



                        dbfDA.Update(dbfDS, "dbf")
                    Next
                End If
            End If
            'DownloadFile(path & "\ERDATA.DBF")
            If Not IO.Directory.Exists(path & "documents\") Then
                IO.Directory.CreateDirectory(path & "documents\")
            End If
            FileCopy(path & sfilename, path & "documents\ERDATA.DBF")
            DownloadFile(path & "documents\ERDATA.DBF")
        Catch ex As Exception
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End Try



    End Sub
    Private Sub lnkHDMFLoan()
        Try
            Dim mycon As OleDbConnection
            Dim dbfString As String, deleteString As String
            Dim sfilename As String = ""
            Dim path As String = Page.MapPath("documents")
            path = Page.MapPath("docs\")
            mycon = xDBFConnection(path)
            sfilename = "PL" & Format(Now, "HHmmss") & ".DBF"
            sfilename = Replace(sfilename, ":", "")
            'sfilename = Microsoft.VisualBasic.Left(sfilename, Len(sfilename) - 4
            'If Dir(path & "\ERDATA.DBF") <> "" Then
            '    Kill((path & "\ERDATA.DBF"))
            'End If
            If Dir(path & "\documents\PBIGLN.DBF") <> "" Then

                Kill((path & "documents\PBIGLN.DBF"))
            End If

            deleteString = "Delete from " & sfilename
            dbfString = "CREATE TABLE " & sfilename & "(EYERID char(15),EYEENO char(15),HDMFID char(12),LNAME char(17), FNAME char(17), MID char(17), PERCOV char(6), AMORT numeric(9,2), ORNO char(8), ORDATE char(8))"
            Dim cmd1 As New OleDbCommand(dbfString, mycon)
            Dim cmd4 As New OleDbCommand(deleteString, mycon)
            cmd1.ExecuteNonQuery()
            cmd4.ExecuteNonQuery()

            Dim dbfCMD As OleDbCommand
            Dim dbfDA As OleDbDataAdapter
            Dim dbfDS As DataSet

            dbfCMD = New OleDbCommand
            dbfDA = New OleDbDataAdapter
            dbfDS = New DataSet

            dbfCMD.CommandText = "select * from  " & sfilename
            dbfCMD.Connection = mycon
            dbfDA.SelectCommand = dbfCMD
            dbfDA.Fill(dbfDS, "dbf")

            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet(SQLHelper.ConSTR, "EPayCont_WebDisk_HDMF_Loan", Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1


                        Dim dr As DataRow
                        dr = dbfDS.Tables(0).NewRow
                        dr("EYERID") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("companyhdmfno"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("EYEENO") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("employeecode"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("HDMFID") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("hdmfno"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("LNAME") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("lastname"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("FNAME") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("firstname"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("MID") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("middlename"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("PERCOV") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("datecovered"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("AMORT") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("hdmfamount"), Global.clsBase.clsBaseLibrary.enumObjectType.DblType)
                        dr("ORNO") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("orno"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        dr("ORDATE") = Generic.CheckDBNull(ds.Tables(0).Rows(i)("ordate"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)



                        dbfDS.Tables(0).Rows.Add(dr)
                        Dim cb As New OleDbCommandBuilder(dbfDA)



                        dbfDA.Update(dbfDS, "dbf")
                    Next
                End If
            End If
            If Not IO.Directory.Exists(path & "documents\") Then
                IO.Directory.CreateDirectory(path & "documents\")
            End If
            'DownloadFile(path & "\ERDATA.DBF")
            FileCopy(path & sfilename, path & "documents\PBIGLN.DBF")
            DownloadFile(path & "documents\PBIGLN.DBF")
        Catch ex As Exception
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End Try

    End Sub
    Private Sub lnkHDMFLoanxls()
        Try
            Dim FileHolder As FileInfo
            Dim WriteFile As StreamWriter
            Dim path As String = Page.MapPath("documents")
            Dim yr As String
            yr = Right(Year(Now), 2)
            Dim filename As String = path & "\" & "HDMF_Loan" & Pad.PadZero(2, ApplicableMonth) + Pad.PadZero(4, ApplicableYear) + ".csv"

            If Not IO.Directory.Exists(path) Then
                IO.Directory.CreateDirectory(path)
            End If
            FileHolder = New FileInfo(filename)
            WriteFile = FileHolder.CreateText()

            Dim dstext As DataSet, text As String
            dstext = SQLHelper.ExecuteDataSet(SQLHelper.ConSTR, "EPayCont_WebDisk_HDMF_LoanCSV", UserNo, Generic.CheckDBNull(PayLocNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableMonth, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ApplicableYear, Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
            If dstext.Tables.Count > 0 Then
                If dstext.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To dstext.Tables(0).Rows.Count - 1
                        text = Generic.CheckDBNull(dstext.Tables(0).Rows(i)("Detail"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        WriteFile.WriteLine(text)
                    Next
                End If
            End If

            WriteFile.Close()
            dstext = Nothing



            DownloadFile("../Secured/documents/" & "HDMF_Loan" & Pad.PadZero(2, ApplicableMonth) + Pad.PadZero(4, ApplicableYear) + ".csv")

        Catch ex As Exception
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End Try
    End Sub







End Class
