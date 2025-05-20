Imports clsLib
Imports System.Data
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Math

Partial Class Secured_PayBankDiskEdit_Backup
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayNo As Integer = 0
    Dim clsGeneric As New clsGenericClass

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayBankDiskList.aspx", "EPayBankDisk") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayBankDisk_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next

    End Sub

    Private Sub PopulateCombo()
        Try
            Me.cboPayNo.DataSource = SQLHelper.ExecuteDataSet("EPay_WebLookup", UserNo, PayLocNo)
            Me.cboPayNo.DataTextField = "tdesc"
            Me.cboPayNo.DataValueField = "tno"
            Me.cboPayNo.DataBind()
        Catch ex As Exception

        End Try
        Try
            Me.cboPayClassNo.DataSource = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", UserNo, PayLocNo)
            Me.cboPayClassNo.DataTextField = "tdesc"
            Me.cboPayClassNo.DataValueField = "tno"
            Me.cboPayClassNo.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("PayBankDiskNo"))
        PayNo = Generic.ToInt(Request.QueryString("id"))
        Session("PayBankDiskDetiList_PayNo") = PayNo
        AccessRights.CheckUser(UserNo, "PayBankDiskList.aspx", "EPayBankDisk")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateCombo()
            PopulateData()
        End If

        EnabledControls()

    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        If Generic.ToBol(txtIsPosted.Checked) = True Then
            Enabled = False
        End If
        Enabled = False 'Disabled All

        Generic.EnableControls(Me, "Panel1", Enabled)
        Generic.PopulateDataDisabled(Me, "Panel1", UserNo, PayLocNo, Generic.ToStr(Session("xMenuType")))
        txtCode.Enabled = False
        lnkModify.Visible = False 'Not Enabled
        lnkSave.Visible = Enabled

        'Show BPI Summary Button
        If Generic.ToInt(cboBankTypeNo.SelectedValue) = 3 Then
            lnkBPI.Visible = True
        Else
            lnkBPI.Visible = False
        End If

    End Sub
    Protected Sub cboPayNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            PopulateDataSelected(Generic.ToInt(cboPayNo.SelectedValue))
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PopulateDataSelected(ByVal tno As Integer)
        Dim _ds As New DataSet

        _ds = SQLHelper.ExecuteDataSet("EPay_WebOne", UserNo, tno)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                Me.txtEndDate.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("EndDate"))
                Me.txtPayDate.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("PayDate"))
                Me.txtStartDate.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("StartDate"))
                Me.txtEndDate.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("EndDate"))
                Me.cboPayClassNo.Text = Generic.ToInt(_ds.Tables(0).Rows(0)("PayClassNo"))
            End If
        End If
    End Sub


    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayBankDiskList.aspx", "EPayBankDisk") Then
            Dim RetVal As Boolean = False
            Dim dt As DataTable
            Dim PayNo As String = Generic.ToStr(Me.cboPayNo.SelectedValue)
            Dim BankTypeNo As String = Generic.ToStr(Me.cboBankTypeNo.SelectedValue)


            dt = SQLHelper.ExecuteDataTable("EPayBankDisk_WebSave", UserNo, TransNo, PayNo, BankTypeNo, PayLocNo)
            For Each row As DataRow In dt.Rows
                TransNo = Generic.ToInt(row("Retval"))
                RetVal = True
            Next

            If RetVal = True Then
                'If Generic.ToInt(Request.QueryString("id")) = 0 Then
                Dim url As String = "PayBankDiskList.aspx?id=" & TransNo
                MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                'Else
                '    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                '    ViewState("IsEnabled") = False
                '    EnabledControls()
                'End If
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If

        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkCreate_Click(sender As Object, e As System.EventArgs)
        Try
            If TransNo > 0 Then
                If Me.cboBankTypeNo.SelectedValue = 4 Then ' RCBC Bank
                    GeneratePayrollDisk_RCBC()
                ElseIf Me.cboBankTypeNo.SelectedValue = 1 Then ' Security Bank
                    GeneratepayrollDisk_Security()
                ElseIf Me.cboBankTypeNo.SelectedValue = 3 Then 'BPI
                    GeneratepayrollDisk_BPI()
                ElseIf Me.cboBankTypeNo.SelectedValue = 6 Then 'BDO
                    GeneratepayrollDisk_BDO()
                ElseIf Me.cboBankTypeNo.SelectedValue = 2 Then 'MetroBank
                    GeneratepayrollDisk_MetroBank()
                ElseIf Me.cboBankTypeNo.SelectedValue = 8 Then 'DBP
                    GeneratepayrollDisk_DBP()
                ElseIf Me.cboBankTypeNo.SelectedValue = 5 Then 'World partners Bank
                    GeneratepayrollDisk_WorldPartnersBank()
                ElseIf Me.cboBankTypeNo.SelectedValue = 7 Then 'Union Bank
                    GeneratepayrollDisk_UnionBank()
                End If
            Else
                MessageBox.Warning("Unable to create no voucher defined.", Me)
            End If
            'MessageBox.Success("Bank Transmittal file has been successfully created!", Me)
        Catch ex As Exception

            MessageBox.Critical("Bank transmittal Error!", Me)
        End Try
    End Sub

    Protected Sub lnkDiskSummary_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If TransNo > 0 Then
            clsGenericClass.DownloadFile("../secured/Disk/04535_Summary.txt", , True)
        Else
            MessageBox.Warning("Unable to create no voucher defined.", Me)
        End If
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


#Region "********RCBC Bank"

    Private Sub GeneratePayrollDisk_RCBC()
        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim tpath As String = Page.MapPath("Disk") '"c:\Payroll Diskette"
        Dim xfilename As String '= path & "\" & "SSSLoan-" & Format(Now, "MMMMdd") & ".TXT"

        Dim sc As String = "4"

        If Not IO.Directory.Exists(tpath) Then
            IO.Directory.CreateDirectory(tpath)
        End If

        Dim dstext As DataSet, text As String
        Dim sqlhelp As New clsBase.SQLHelper
        Dim xftotalS As Double, xPABranchCode As String, xAccountNo As String, xEffectivityDate As String, xEffectivityDated As String
        Dim sdate As Integer
        Dim xFxTotals As String
        Dim xFHeadCount As Integer = 0
        Dim fBranchCode As String = ""

        dstext = SQLHelper.ExecuteDataSet("select sum(amount) as x from dbo.EPayBankDiskDetiSendToDiskSavingsRCBC where paybankdiskno=" & Me.txtPayBankDiskNo.Text)

        If dstext.Tables.Count > 0 Then
            If dstext.Tables(0).Rows.Count > 0 Then
                xftotalS = Generic.CheckDBNull(dstext.Tables(0).Rows(0)("x"), clsBase.clsBaseLibrary.enumObjectType.IntType)
            End If
        End If

        dstext = Nothing

        Dim dsComp As New DataSet
        dsComp = SQLHelper.ExecuteDataSet("select * from dbo.EPayBankDiskRef where payclassno=" & Me.cboPayClassNo.SelectedValue)
        If dsComp.Tables.Count > 0 Then
            If dsComp.Tables(0).Rows.Count > 0 Then
                xPABranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_PayrollAccount"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                xAccountNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("AccountNumber"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                xEffectivityDated = FormatDateTime(dsComp.Tables(0).Rows(0)("EffectiveDate"), DateFormat.ShortDate)
                xEffectivityDate = Pad.PadZero(2, Month(xEffectivityDated)) & Pad.PadZero(2, Day(xEffectivityDated)) & Pad.PadZero(2, Mid(Year(xEffectivityDated), 3, 2))
                sdate = Day(xEffectivityDated)
                fBranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_Company"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            End If
        End If

        dsComp = Nothing
        xfilename = tpath & "\S074" & "ls" & Pad.PadZero(2, sdate) & "_TX" & sc & ".txt"
        FileHolder = New FileInfo(xfilename)
        WriteFile = FileHolder.CreateText()
        xFxTotals = Pad.PadNetPay(Generic.CheckDBNull(CStr(Format(Int(xftotalS), "Fixed")), clsBase.clsBaseLibrary.enumObjectType.StrType), 20)

        xFHeadCount = 0

        Dim tAccountNo3Characters As String
        Dim tSumAccountNo As Double
        Dim xhash As String
        Dim xAmount As String
        Dim xFTotal As Double
        Dim tSumProduct As Double
        Dim xSumProduct As Double
        Dim xsumAccountNo As String
        Dim xFxTotal As String

        Dim ds As New DataSet
        ds = SQLHelper.ExecuteDataSet("select bankaccountno,amount as net from dbo.EPayBankDiskDetiSendToDiskSavingsRCBC where paybankdiskno=" & Me.txtPayBankDiskNo.Text)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    xAccountNo = Pad.PadZero(14, Generic.CheckDBNull(Replace(ds.Tables(0).Rows(0)("BankAccountNo"), "-", ""), clsBase.clsBaseLibrary.enumObjectType.StrType))

                    tSumAccountNo = tSumAccountNo + Val(xAccountNo)

                    tAccountNo3Characters = Mid(xAccountNo, 6, 3)
                    If tAccountNo3Characters = "000" Then
                        tAccountNo3Characters = "001"
                    End If
                    xAmount = Pad.PadNetPay(Generic.CheckDBNull(CStr(Format(ds.Tables(0).Rows(i)("Net"), "Fixed")), clsBase.clsBaseLibrary.enumObjectType.StrType), 15)

                    xFTotal = xFTotal + Int(Generic.CheckDBNull(ds.Tables(0).Rows(i)("Net"), clsBase.clsBaseLibrary.enumObjectType.IntType))
                    tSumProduct = Int(Generic.CheckDBNull(ds.Tables(0).Rows(i)("Net"), clsBase.clsBaseLibrary.enumObjectType.IntType)) * Val(Mid(xAccountNo, 9, 6))
                    xSumProduct = Generic.CheckDBNull(xSumProduct, clsBase.clsBaseLibrary.enumObjectType.IntType) + Generic.CheckDBNull(tSumProduct, clsBase.clsBaseLibrary.enumObjectType.IntType)

                    text = "01001" & tAccountNo3Characters & "000" & xAccountNo & "80" & "1110" & xAmount & xEffectivityDate & "0" & Space(25) & "00001" & Space(10) & fBranchCode & Space(4)
                    WriteFile.WriteLine(text)

                Next
                xsumAccountNo = Pad.PadZero(20, tSumAccountNo)
                xhash = Pad.PadZero(20, xSumProduct)
                xFxTotal = Pad.PadZero(20, xFTotal)
                If Generic.CheckDBNull(xFTotal, clsBase.clsBaseLibrary.enumObjectType.IntType) > 0 Then
                    WriteFile.WriteLine("H" & xsumAccountNo & xFxTotal & xhash)
                End If
            End If
        End If

        WriteFile.Close()

        ds = Nothing

        'For i As Integer = 0 To dstext.Tables(0).Rows.Count - 1
        '    text = Generic.CheckDBNull(dstext.Tables(0).Rows(i)("Detail"), clsbase.clsBaseLibrary.enumObjectType.StrType)
        '    WriteFile.WriteLine(text)
        'Next
        'WriteFile.Close()
        'dstext = Nothing

        clsGenericClass.DownloadFile("../secured/Disk/S074ls" & Pad.PadZero(2, sdate) & "_TX" & sc & ".txt", , True)


    End Sub


#End Region

#Region "********Security Bank"

    Private Sub GeneratepayrollDisk_Security()
        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim tpath As String = Page.MapPath("Disk") '"c:\Payroll Diskette"
        Dim xfilename As String '= path & "\" & "SSSLoan-" & Format(Now, "MMMMdd") & ".TXT"
        Dim dstext As DataSet
        Dim sqlhelp As New clsBase.SQLHelper
        Dim xftotalS As Double, xPABranchCode As String, xAccountNo As String, xEffectivityDate As String, xEffectivityDated As String
        Dim sdate As Integer
        Dim xFxTotals As String
        Dim xFHeadCount As Integer = 0
        Dim fBranchCode As String = ""
        Dim xFTotal As Double
        Dim xxAccountNo As String


        If Not IO.Directory.Exists(tpath) Then
            IO.Directory.CreateDirectory(tpath)
        End If

        dstext = SQLHelper.ExecuteDataSet("select sum(amount) as x from dbo.EPayBankDiskDeti where paybankdiskno=" & Me.txtPayBankDiskNo.Text)
        If dstext.Tables.Count > 0 Then
            If dstext.Tables(0).Rows.Count > 0 Then
                xftotalS = Generic.CheckDBNull(dstext.Tables(0).Rows(0)("x"), clsBase.clsBaseLibrary.enumObjectType.IntType)
            End If
        End If

        dstext = Nothing


        Dim dsComp As New DataSet
        dsComp = SQLHelper.ExecuteDataSet("select * from dbo.EPayBankDiskRef where payclassno=" & Me.cboPayClassNo.SelectedValue & " and BankTypeNo= " & Me.cboBankTypeNo.SelectedValue)
        If dsComp.Tables.Count > 0 Then
            If dsComp.Tables(0).Rows.Count > 0 Then
                xPABranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_PayrollAccount"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                xAccountNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("AccountNumber"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                xEffectivityDated = FormatDateTime(dsComp.Tables(0).Rows(0)("EffectiveDate"), DateFormat.ShortDate)
                xEffectivityDate = Pad.PadZero(2, Month(xEffectivityDated)) & Pad.PadZero(2, Day(xEffectivityDated)) & Pad.PadZero(2, Mid(Year(xEffectivityDated), 3, 2))
                sdate = Day(xEffectivityDated)
                fBranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_Company"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            End If
        End If

        dsComp = Nothing
        xfilename = tpath & "\WHOUSE_DTD.txt"
        FileHolder = New FileInfo(xfilename)
        WriteFile = FileHolder.CreateText()
        xFxTotals = Pad.PadNetPay(Generic.CheckDBNull(CStr(Format(Int(xftotalS), "Fixed")), clsBase.clsBaseLibrary.enumObjectType.StrType), 13)

        Dim tRecordCode As String
        Dim tTransactionCode As String
        Dim xAmount As String, tCurrencyCode As String, xFxTotal As String


        tCurrencyCode = "PHP"
        tRecordCode = "01"
        tTransactionCode = "200"
        xFxTotal = Pad.PadNetPay(Generic.CheckDBNull(CStr(Format(xftotalS, "Fixed")), clsBase.clsBaseLibrary.enumObjectType.StrType), 13)
        'xFxTotals = Pad.PadNetPay(Generic.CheckDBNull(CStr(Format((xftotalS), "Fixed")), clsbase.clsBaseLibrary.enumObjectType.StrType), 12)



        WriteFile.WriteLine(tCurrencyCode & tRecordCode & xAccountNo & xEffectivityDate & tTransactionCode & xFxTotal & Space(40))

        xFTotal = 0
        xFHeadCount = 0
        tRecordCode = "10"
        tTransactionCode = "700"



        Dim ds As New DataSet
        ds = SQLHelper.ExecuteDataSet("select bankaccountno,amount as net from dbo.EPayBankDiskDeti where paybankdiskno=" & Me.txtPayBankDiskNo.Text & " order by FullName ")
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    xFHeadCount = xFHeadCount + 1
                    xFTotal = xFTotal + Generic.CheckDBNull(ds.Tables(0).Rows(i)("Net"), clsBase.clsBaseLibrary.enumObjectType.IntType)

                    xAccountNo = Pad.PadZero(13, Generic.CheckDBNull(Replace(ds.Tables(0).Rows(i)("bankaccountno"), "-", ""), clsBase.clsBaseLibrary.enumObjectType.StrType))

                    xAmount = Pad.PadNetPay(Generic.CheckDBNull(CStr(Format(ds.Tables(0).Rows(i)("Net"), "Fixed")), clsBase.clsBaseLibrary.enumObjectType.StrType), 13)
                    'xFxTotals = Pad.PadNetPay(Generic.CheckDBNull(CStr(Format((xftotalS), "Fixed")), clsbase.clsBaseLibrary.enumObjectType.StrType), 12)

                    WriteFile.WriteLine(tCurrencyCode & tRecordCode & xAccountNo & fBranchCode & "100" & tTransactionCode & xAmount & Space(40))
                Next
            End If
        End If

        WriteFile.Close()
        ds = Nothing

        'clsGenericClass.DownloadFile("../secured/Disk/WHOUSE_DTD.txt", , True)
        OpenText("../secured/Disk/WHOUSE_DTD.txt")

    End Sub

#End Region

#Region "********BPI"

    Private Sub GeneratepayrollDisk_BPI()
        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim tpath As String = Page.MapPath("Disk") '"c:\Payroll Diskette"
        Dim xfilename As String '= path & "\" & "SSSLoan-" & Format(Now, "MMMMdd") & ".TXT"
        Dim dstext As DataSet
        Dim sqlhelp As New clsBase.SQLHelper
        Dim xftotalS As Double, xPABranchCode As String, xAccountNo As String, xEffectivityDate As String, xEffectivityDated As String
        Dim sdate As Integer
        Dim xFxTotals As String, xFxTotalC As String, xFxTotalT As String = ""
        Dim xFHeadCount As Integer = 0
        Dim fBranchCode As String = ""
        Dim xFTotal As Double
        Dim xCompanyCode As String = ""
        Dim xftotalC As Double
        Dim xBatchNo As String = ""
        Dim xcompanyaccountno As String = ""




        Try


            dstext = SQLHelper.ExecuteDataSet("select sum(amount) as x from dbo.EPayBankDiskDeti where paybankdiskno=" & Me.txtPayBankDiskNo.Text)

            If dstext.Tables.Count > 0 Then
                If dstext.Tables(0).Rows.Count > 0 Then
                    xftotalS = Generic.CheckDBNull(dstext.Tables(0).Rows(0)("x"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                End If
            End If

            dstext = Nothing

            xftotalC = Math.Ceiling(xftotalS)


            Dim dsComp As New DataSet
            dsComp = SQLHelper.ExecuteDataSet("select * from dbo.EPayBankDiskRef where payclassno=" & Me.cboPayClassNo.SelectedValue & " and banktypeno=" & Generic.CheckDBNull(Me.cboBankTypeNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType))
            If dsComp.Tables.Count > 0 Then
                If dsComp.Tables(0).Rows.Count > 0 Then
                    xPABranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_PayrollAccount"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xAccountNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("AccountNumber"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xEffectivityDated = FormatDateTime(dsComp.Tables(0).Rows(0)("EffectiveDate"), DateFormat.ShortDate)
                    xEffectivityDate = Pad.PadZero(2, Month(xEffectivityDated)) & Pad.PadZero(2, Day(xEffectivityDated)) & Pad.PadZero(2, Mid(Year(xEffectivityDated), 3, 2))
                    sdate = Day(xEffectivityDated)
                    fBranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_Company"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xCompanyCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("CompanyCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xBatchNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BatchNo"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xcompanyaccountno = xAccountNo
                End If
            End If

            dsComp = Nothing
            xfilename = tpath & "\" & xCompanyCode & ".txt"
            FileHolder = New FileInfo(xfilename)
            WriteFile = FileHolder.CreateText()
            xFxTotals = Pad.PadNetPay(Generic.CheckDBNull(CStr(Format((xftotalS), "Fixed")), clsBase.clsBaseLibrary.enumObjectType.StrType), 12)
            xFxTotalC = Pad.PadNetPay(Generic.CheckDBNull(CStr(Format((xftotalC), "Fixed")), clsBase.clsBaseLibrary.enumObjectType.StrType), 12)
            xFxTotalT = Pad.PadNetPay(Generic.CheckDBNull(CStr(Format((xftotalS), "Fixed")), clsBase.clsBaseLibrary.enumObjectType.StrType), 15)

            Dim tRecordCode As String
            Dim tTransactionCode As String
            Dim xAmount As String, tCurrencyCode As String, xFxTotal As String
            Dim faccountno As String = ""
            Dim dhash1 As String = "", dhash2 As String = "", dhash3 As String = ""
            Dim tnet As Double = 0, dhastotal As Double = 0, Accounthashtotal As Double = 0, totalhash As Double = 0

            xAccountNo = Pad.PadZero(10, xAccountNo)

            WriteFile.WriteLine("H" & xCompanyCode & xEffectivityDate & xBatchNo & "1" & xAccountNo & Pad.PadZero(3, fBranchCode) & xFxTotalC & xFxTotals & "1" & Space(75))

            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("select bankaccountno,amount as net from dbo.EPayBankDiskDeti where paybankdiskno=" & Me.txtPayBankDiskNo.Text)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        xFHeadCount = xFHeadCount + 1
                        xFTotal = xFTotal + Generic.CheckDBNull(ds.Tables(0).Rows(i)("Net"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                        tnet = Generic.CheckDBNull(ds.Tables(0).Rows(i)("Net"), clsBase.clsBaseLibrary.enumObjectType.IntType)

                        faccountno = Pad.PadZero(10, Generic.CheckDBNull(Replace(ds.Tables(0).Rows(i)("bankaccountno"), "-", ""), clsBase.clsBaseLibrary.enumObjectType.StrType))
                        xAmount = Pad.PadNetPay(Generic.CheckDBNull(CStr(Format(ds.Tables(0).Rows(i)("Net"), "Fixed")), clsBase.clsBaseLibrary.enumObjectType.StrType), 12)
                        Accounthashtotal = Accounthashtotal + CType(faccountno, Double)
                        faccountno = Pad.PadZero(10, faccountno)
                        'If Mid(faccountno, 6, 1) = "5" Then
                        '    faccountno = Mid(faccountno, 1, 5) & "6" & Mid(faccountno, 7, 4)
                        'End If
                        dhash1 = Mid(faccountno, 5, 2)
                        dhash2 = Mid(faccountno, 7, 2)
                        dhash3 = Mid(faccountno, 9, 2)
                        'dhastotal = (CType(dhash1, Double) * tnet) + (CType(dhash2, Double) * tnet) + (CType(dhash3, Double) * tnet)
                        dhastotal = (CType(dhash1, Double) * (CType(xAmount, Double) / 100)) + (CType(dhash2, Double) * (CType(xAmount, Double) / 100)) + (CType(dhash3, Double) * (CType(xAmount, Double) / 100))

                        totalhash = totalhash + dhastotal
                        WriteFile.WriteLine("D" & xCompanyCode & xEffectivityDate & xBatchNo & "3" & faccountno & xAmount & Pad.PadNetPay(dhastotal, 12) & Space(79))
                    Next
                End If
            End If

            ds = Nothing
            totalhash = Round(totalhash, 2)

            WriteFile.WriteLine("T" & xCompanyCode & xEffectivityDate & xBatchNo & "2" & xAccountNo & Pad.PadZero(15, Accounthashtotal) & xFxTotalT & Pad.PadNetPay(totalhash, 18) & Pad.PadZero(5, xFHeadCount) & Space(50))



            WriteFile.Close()
            BankSummary(xCompanyCode, xEffectivityDate, xBatchNo, xcompanyaccountno, xftotalS, Accounthashtotal, totalhash, xFHeadCount)

            OpenText("../secured/Disk/" & xCompanyCode & ".txt")

        Catch ex As Exception
            WriteFile.Close()
        End Try

    End Sub

    Private Sub BankSummary(ByVal tcompanycode As String, ByVal paydate As String, ByVal fbacthno As String, ByVal accountno As String, ByVal totalnet As String, ByVal accounthas As String, ByVal ftotalhash As String, ByVal fxFHeadCount As String)
        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim xfilename As String
        Dim tpath As String = Page.MapPath("Disk") '"c:\Payroll Diskette"
        xfilename = tpath & "\" & tcompanycode & "_Summary.txt"
        FileHolder = New FileInfo(xfilename)
        WriteFile = FileHolder.CreateText()
        WriteFile.WriteLine("Company Name: CHONG HUA HOSPITAL")

        WriteFile.WriteLine("Company Code: " & tcompanycode)
        WriteFile.WriteLine("Payroll Date: " & paydate)
        WriteFile.WriteLine("Batch No.: " & fbacthno)
        WriteFile.WriteLine("Company Account No.: " & accountno)
        WriteFile.WriteLine("Debit Amount (Total Net Pay): " & totalnet)
        WriteFile.WriteLine("Account Hash Total: " & accounthas)
        WriteFile.WriteLine("Grand Horizontal Hash: " & ftotalhash)
        WriteFile.WriteLine("Total Detail Lines: " & fxFHeadCount)

        WriteFile.Close()
    End Sub

#End Region

#Region "********BDO"

    Private Sub GeneratepayrollDisk_BDO()

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim tpath As String = Page.MapPath("Disk") '"c:\Payroll Diskette"
        Dim xfilename As String '= path & "\" & "SSSLoan-" & Format(Now, "MMMMdd") & ".TXT"
        Dim dstext As DataSet
        Dim sqlhelp As New clsBase.SQLHelper
        Dim xftotalS As Double, xPABranchCode As String, xAccountNo As String, xEffectivityDate As String, xEffectivityDated As String
        Dim sdate As Integer
        Dim xFxTotalT As String = ""
        Dim xFHeadCount As Integer = 0
        Dim fBranchCode As String = ""
        Dim xFTotal As Double, xFxTotal As String = ""
        Dim xCompanyCode As String = ""
        Dim xBatchNo As String = ""
        Dim xcompanyaccountno As String = ""

        Dim xAmount As String
        Dim faccountno As String = ""
        Dim tnet As Double = 0


        Try
            dstext = SQLHelper.ExecuteDataSet("select sum(amount) as x from dbo.EPayBankDiskDeti where paybankdiskno=" & Me.txtPayBankDiskNo.Text)

            If dstext.Tables.Count > 0 Then
                If dstext.Tables(0).Rows.Count > 0 Then
                    xftotalS = Generic.CheckDBNull(dstext.Tables(0).Rows(0)("x"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                End If
            End If

            dstext = Nothing
            xFxTotal = FormatNumber(xftotalS, 2).ToString  'Format(xFTotal, "###0.00")

            Dim dsComp As New DataSet
            dsComp = SQLHelper.ExecuteDataSet("select * from dbo.EPayBankDiskRef where payclassno=" & Me.cboPayClassNo.SelectedValue & " and banktypeno=" & Generic.CheckDBNull(Me.cboBankTypeNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType))
            If dsComp.Tables.Count > 0 Then
                If dsComp.Tables(0).Rows.Count > 0 Then
                    xPABranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_PayrollAccount"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xAccountNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("AccountNumber"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xEffectivityDated = FormatDateTime(dsComp.Tables(0).Rows(0)("EffectiveDate"), DateFormat.ShortDate)
                    xEffectivityDate = Pad.PadZero(2, Month(xEffectivityDated)) & Pad.PadZero(2, Day(xEffectivityDated)) & Pad.PadZero(2, Mid(Year(xEffectivityDated), 3, 2))
                    sdate = Day(xEffectivityDated)
                    fBranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_Company"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xCompanyCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("CompanyCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xBatchNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BatchNo"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xcompanyaccountno = xAccountNo
                End If
            End If

            dsComp = Nothing
            xfilename = tpath & "\" & xCompanyCode & "_BDO.txt"
            FileHolder = New FileInfo(xfilename)
            WriteFile = FileHolder.CreateText()



            WriteFile.WriteLine("H         3" & xAccountNo & "       " & xCompanyCode & Pad.PadZero(4, Year(xEffectivityDated)) & Pad.PadZero(2, Month(xEffectivityDated)) & Pad.PadZero(2, Day(xEffectivityDated)))

            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("select bankaccountno,amount as net from dbo.EPayBankDiskDeti where paybankdiskno=" & Me.txtPayBankDiskNo.Text)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        xFHeadCount = xFHeadCount + 1
                        tnet = Generic.CheckDBNull(ds.Tables(0).Rows(i)("Net"), clsBase.clsBaseLibrary.enumObjectType.IntType)

                        faccountno = Generic.CheckDBNull(Replace(ds.Tables(0).Rows(i)("bankaccountno"), "-", ""), clsBase.clsBaseLibrary.enumObjectType.StrType)
                        xAmount = Replace(FormatNumber(tnet, 2).ToString, ",", "")
                        If faccountno.Length < 10 Then
                            faccountno = Pad.PadZero(10, faccountno)
                        End If
                        WriteFile.WriteLine(faccountno & Space(6) & xAmount)
                    Next
                End If
            End If

            ds = Nothing

            'footer
            xFxTotal = Replace(xFxTotal, ",", "")
            WriteFile.WriteLine("T" & Space(10 - Len(Trim(xFHeadCount))) & Trim(xFHeadCount) & xFxTotal)

            WriteFile.Close()
            OpenText("../secured/Disk/" & xCompanyCode & "_BDO.txt")

        Catch ex As Exception
            WriteFile.Close()
        End Try

    End Sub

#End Region

#Region "********Metro Bank"

    Private Sub GeneratepayrollDisk_MetroBank()

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim tpath As String = Page.MapPath("Disk") '"c:\Payroll Diskette"
        Dim xfilename As String '= path & "\" & "SSSLoan-" & Format(Now, "MMMMdd") & ".TXT"
        Dim dstext As DataSet
        Dim sqlhelp As New clsBase.SQLHelper
        Dim xftotalS As Double, xPABranchCode As String, xAccountNo As String, xEffectivityDate As String, xEffectivityDated As String
        Dim sdate As Integer
        Dim xFxTotalT As String = ""
        Dim xFHeadCount As Integer = 0
        Dim fBranchCode As String = ""
        Dim xFTotal As Double, xFxTotal As String = ""
        Dim xCompanyCode As String = ""
        Dim xBatchNo As String = ""
        Dim xcompanyaccountno As String = ""

        Dim xAmount As String
        Dim faccountno As String = ""
        Dim tnet As Double = 0
        Dim FV1 As String = 0
        Dim FV2 As String = 0
        Dim FV3 As String = 0
        Dim FV4 As String = 0
        Dim xBankCode As String = ""
        Dim xCompanyName As String = ""

        Try
            dstext = SQLHelper.ExecuteDataSet("select sum(amount) as x from dbo.EPayBankDiskDeti where paybankdiskno=" & Me.txtPayBankDiskNo.Text)

            If dstext.Tables.Count > 0 Then
                If dstext.Tables(0).Rows.Count > 0 Then
                    xftotalS = Generic.CheckDBNull(dstext.Tables(0).Rows(0)("x"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                End If
            End If

            dstext = Nothing
            xFxTotal = FormatNumber(xftotalS, 2).ToString  'Format(xFTotal, "###0.00")

            Dim dsComp As New DataSet
            dsComp = SQLHelper.ExecuteDataSet("EPayBankDiskRef_WebOne_PayClass", UserNo, Me.cboPayClassNo.SelectedValue, Generic.CheckDBNull(Me.cboBankTypeNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType))
            If dsComp.Tables.Count > 0 Then
                If dsComp.Tables(0).Rows.Count > 0 Then
                    xPABranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_PayrollAccount"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xAccountNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("AccountNumber"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xEffectivityDated = FormatDateTime(dsComp.Tables(0).Rows(0)("EffectiveDate"), DateFormat.ShortDate)
                    xEffectivityDate = Pad.PadZero(2, Month(xEffectivityDated)) & "/" & Pad.PadZero(2, Day(xEffectivityDated)) & "/" & Pad.PadZero(4, Year(xEffectivityDated))
                    sdate = Day(xEffectivityDated)
                    fBranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_Company"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xCompanyCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("CompanyCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xBatchNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BatchNo"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xcompanyaccountno = xAccountNo
                    xBankCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BankCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xCompanyName = Pad.PadSpace(Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("paylocdesc"), clsBase.clsBaseLibrary.enumObjectType.StrType), 40)
                    FV1 = Pad.PadZero(1, Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("fv1"), clsBase.clsBaseLibrary.enumObjectType.StrType))
                    FV2 = Pad.PadZero(3, Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("fv2"), clsBase.clsBaseLibrary.enumObjectType.StrType))
                    FV3 = Pad.PadZero(7, Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("fv3"), clsBase.clsBaseLibrary.enumObjectType.StrType))
                    FV4 = Pad.PadZero(1, Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("fv4"), clsBase.clsBaseLibrary.enumObjectType.StrType))

                    xCompanyCode = Pad.PadZero(5, xCompanyCode)
                End If
            End If

            dsComp = Nothing
            xfilename = tpath & "\" & xCompanyCode & "_MetroBank.DAT"
            FileHolder = New FileInfo(xfilename)
            WriteFile = FileHolder.CreateText()


            'WriteFile.WriteLine("H         3" & xAccountNo & "       BF4" & Pad.PadZero(4, Year(xEffectivityDated)) & Pad.PadZero(2, Month(xEffectivityDated)) & Pad.PadZero(2, Day(xEffectivityDated)))

            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("select bankaccountno,amount as net from dbo.EPayBankDiskDeti where paybankdiskno=" & Me.txtPayBankDiskNo.Text)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        xFHeadCount = xFHeadCount + 1
                        tnet = Generic.CheckDBNull(ds.Tables(0).Rows(i)("Net"), clsBase.clsBaseLibrary.enumObjectType.IntType)

                        faccountno = Generic.CheckDBNull(Replace(ds.Tables(0).Rows(i)("bankaccountno"), "-", ""), clsBase.clsBaseLibrary.enumObjectType.StrType)
                        xAmount = Pad.PadNetPay(tnet, 15)
                        If faccountno.Length < 10 Then
                            faccountno = Pad.PadZero(10, faccountno)
                        End If
                        WriteFile.WriteLine(FV1 & fBranchCode & xBankCode & FV2 & fBranchCode & FV3 & UCase(xCompanyName) & faccountno & xAmount & FV4 & xCompanyCode & Replace(xEffectivityDate, "/", ""))

                    Next
                End If
            End If

            ds = Nothing


            WriteFile.Close()
            ' ("../secured/Disk/" & xCompanyCode & "_MetroBank.DAT")

            OpenText("../secured/Disk/" & xCompanyCode & "_MetroBank.DAT")


        Catch ex As Exception
            WriteFile.Close()
        End Try

    End Sub

#End Region

#Region "********DBP"

    Private Sub GeneratepayrollDisk_DBP()

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim tpath As String = Page.MapPath("Disk") '"c:\Payroll Diskette"
        Dim xfilename As String '= path & "\" & "SSSLoan-" & Format(Now, "MMMMdd") & ".TXT"
        Dim dstext As DataSet
        Dim sqlhelp As New clsBase.SQLHelper
        Dim xftotalS As Double, xPABranchCode As String, xAccountNo As String, xEffectivityDate As String, xEffectivityDated As String
        Dim sdate As Integer
        Dim xFxTotalT As String = ""
        Dim xFHeadCount As Integer = 0
        Dim fBranchCode As String = ""
        Dim xFxTotal As String = ""
        Dim xCompanyCode As String = ""
        Dim xBatchNo As String = ""
        Dim xcompanyaccountno As String = ""

        Dim xAmount As String
        Dim faccountno As String = ""
        Dim tnet As Double = 0
        Dim Lastname As String, Firstname As String, Middlename As String

        Try
            dstext = SQLHelper.ExecuteDataSet("select sum(amount) as x from dbo.EPayBankDiskDeti where paybankdiskno=" & Me.txtPayBankDiskNo.Text)

            If dstext.Tables.Count > 0 Then
                If dstext.Tables(0).Rows.Count > 0 Then
                    xftotalS = Generic.CheckDBNull(dstext.Tables(0).Rows(0)("x"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                End If
            End If

            dstext = Nothing
            xFxTotal = Replace(FormatNumber(xftotalS, 2).ToString, ",", "") 'Format(xFTotal, "###0.00")

            Dim dsComp As New DataSet
            dsComp = SQLHelper.ExecuteDataSet("select * from dbo.EPayBankDiskRef where payclassno=" & Me.cboPayClassNo.SelectedValue & " and banktypeno=" & Generic.CheckDBNull(Me.cboBankTypeNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType))
            If dsComp.Tables.Count > 0 Then
                If dsComp.Tables(0).Rows.Count > 0 Then
                    xPABranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_PayrollAccount"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xAccountNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("AccountNumber"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xEffectivityDated = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("EffectiveDate"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    If IsDate(xEffectivityDated) Then
                        xEffectivityDated = FormatDateTime(xEffectivityDated, DateFormat.ShortDate)
                        xEffectivityDate = Pad.PadZero(2, Month(xEffectivityDated)) & Pad.PadZero(2, Day(xEffectivityDated)) & Pad.PadZero(2, Mid(Year(xEffectivityDated), 3, 2))
                        sdate = Day(xEffectivityDated)
                    Else
                        xEffectivityDate = ""
                        sdate = 0
                    End If


                    fBranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_Company"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xCompanyCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("CompanyCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xBatchNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BatchNo"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xcompanyaccountno = xAccountNo
                End If
            End If

            dsComp = Nothing
            xfilename = tpath & "\" & xCompanyCode & "_DBP.txt"
            FileHolder = New FileInfo(xfilename)
            WriteFile = FileHolder.CreateText()



            WriteFile.WriteLine(Pad.PadSpaceRightLeft(xAccountNo, 12, True) & Pad.PadSpaceRightLeft("DBP PAYROLL", 15, True) & Pad.PadSpaceRightLeft("", 30, True) & Pad.PadSpaceRightLeft(xFxTotal, 16, False))

            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("Select a.bankaccountno,amount as net,Lastname,Firstname,MiddleName from dbo.EPayBankDiskDeti A Inner Join (Select employeeno,lastname,firstname,Left(MiddleName,1) As MiddleName from dbo.EEmployee) B On A.EmployeeNo=B.EmployeeNo where a.paybankdiskno=" & Me.txtPayBankDiskNo.Text)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        xFHeadCount = xFHeadCount + 1
                        tnet = Generic.CheckDBNull(ds.Tables(0).Rows(i)("Net"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                        Lastname = Generic.CheckDBNull(ds.Tables(0).Rows(i)("Lastname"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                        Firstname = Generic.CheckDBNull(ds.Tables(0).Rows(i)("Firstname"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                        Middlename = Generic.CheckDBNull(ds.Tables(0).Rows(i)("Middlename"), clsBase.clsBaseLibrary.enumObjectType.StrType)

                        faccountno = Generic.CheckDBNull(Replace(ds.Tables(0).Rows(i)("bankaccountno"), "-", ""), clsBase.clsBaseLibrary.enumObjectType.StrType)
                        xAmount = Pad.PadSpaceRightLeft(Replace(FormatNumber(tnet, 2).ToString, ",", ""), 16, False)

                        If faccountno.Length < 10 Then
                            faccountno = Pad.PadZero(10, faccountno)
                        End If


                        WriteFile.WriteLine(Pad.PadSpaceRightLeft(faccountno, 12, True) & Pad.PadSpaceRightLeft(Lastname, 15, True) & Pad.PadSpaceRightLeft(Firstname, 15, True) & Pad.PadSpaceRightLeft(Middlename, 15, True) & xAmount)

                    Next
                End If
            End If

            ds = Nothing

            'footer
            'WriteFile.WriteLine("T" & Space(10 - Len(Trim(xFHeadCount))) & Trim(xFHeadCount) & xFxTotal)

            WriteFile.Close()
            OpenText("../secured/Disk/" & xCompanyCode & "_DBP.txt")

        Catch ex As Exception
            WriteFile.Close()
        End Try

    End Sub


#End Region

#Region "********WorldPartnersBank"

    Private Sub GeneratepayrollDisk_WorldPartnersBank()

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim tpath As String = Page.MapPath("Disk") '"c:\Payroll Diskette"
        Dim xfilename As String '= path & "\" & "SSSLoan-" & Format(Now, "MMMMdd") & ".TXT"
        Dim dstext As DataSet
        Dim sqlhelp As New clsBase.SQLHelper
        Dim xftotalS As Double, xPABranchCode As String, xAccountNo As String, xEffectivityDate As String, xEffectivityDated As String
        Dim sdate As Integer
        Dim xFxTotalT As String = ""
        Dim xFHeadCount As Integer = 0
        Dim fBranchCode As String = ""
        Dim xFTotal As Double, xFxTotal As String = ""
        Dim xCompanyCode As String = ""
        Dim xBatchNo As String = ""
        Dim xcompanyaccountno As String = ""

        Dim xAmount As String
        Dim faccountno As String = ""
        Dim tnet As Double = 0


        Try
            dstext = SQLHelper.ExecuteDataSet("select sum(amount) as x from dbo.EPayBankDiskDeti where paybankdiskno=" & Me.txtPayBankDiskNo.Text)

            If dstext.Tables.Count > 0 Then
                If dstext.Tables(0).Rows.Count > 0 Then
                    xftotalS = Generic.CheckDBNull(dstext.Tables(0).Rows(0)("x"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                End If
            End If

            dstext = Nothing
            xFxTotal = FormatNumber(xftotalS, 2).ToString  'Format(xFTotal, "###0.00")

            Dim dsComp As New DataSet
            dsComp = SQLHelper.ExecuteDataSet("select * from dbo.EPayBankDiskRef where payclassno=" & Me.cboPayClassNo.SelectedValue & " and banktypeno=" & Generic.CheckDBNull(Me.cboBankTypeNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType))
            If dsComp.Tables.Count > 0 Then
                If dsComp.Tables(0).Rows.Count > 0 Then
                    xPABranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_PayrollAccount"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xAccountNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("AccountNumber"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xEffectivityDated = FormatDateTime(dsComp.Tables(0).Rows(0)("EffectiveDate"), DateFormat.ShortDate)
                    xEffectivityDate = Pad.PadZero(2, Month(xEffectivityDated)) & Pad.PadZero(2, Day(xEffectivityDated)) & Pad.PadZero(2, Mid(Year(xEffectivityDated), 3, 2))
                    sdate = Day(xEffectivityDated)
                    fBranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_Company"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xCompanyCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("CompanyCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xBatchNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BatchNo"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xcompanyaccountno = xAccountNo
                End If
            End If

            dsComp = Nothing
            xfilename = tpath & "\" & xCompanyCode & "_WPC.txt"
            FileHolder = New FileInfo(xfilename)
            WriteFile = FileHolder.CreateText()



            'WriteFile.WriteLine("BR" & xAccountNo & "       BF4" & Pad.PadZero(4, Year(xEffectivityDated)) & Pad.PadZero(2, Month(xEffectivityDated)) & Pad.PadZero(2, Day(xEffectivityDated)))
            WriteFile.WriteLine("BF" & Pad.PadZero(4, Year(xEffectivityDated)) & Pad.PadZero(2, Month(xEffectivityDated)) & Pad.PadZero(2, Day(xEffectivityDated)) & Space(2) & xCompanyCode & xAccountNo & "00360" & "0000000000000000000000000000000000000000")

            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("select bankaccountno,amount as net from dbo.EPayBankDiskDeti where paybankdiskno=" & Me.txtPayBankDiskNo.Text)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        xFHeadCount = xFHeadCount + 1
                        tnet = Generic.CheckDBNull(ds.Tables(0).Rows(i)("Net"), clsBase.clsBaseLibrary.enumObjectType.IntType)

                        faccountno = Generic.CheckDBNull(Replace(ds.Tables(0).Rows(i)("bankaccountno"), "-", ""), clsBase.clsBaseLibrary.enumObjectType.StrType)
                        xAmount = FormatNumber(tnet, 2).ToString
                        If faccountno.Length < 10 Then
                            faccountno = Pad.PadZero(10, faccountno)
                        End If
                        WriteFile.WriteLine("010000000000" & Space(2) & xCompanyCode & "000000" & faccountno & "001" & Space(12 - Len(xAmount)) & xAmount & "0037000000000000000000")

                        'WriteFile.WriteLine(faccountno & Space(6) & xAmount)

                    Next
                End If
            End If

            ds = Nothing

            'footer
            WriteFile.WriteLine("BR" & "000000000" & Space(7 - Len(Trim(xFHeadCount))) & Trim(xFHeadCount) & Space(14 - Len(xFxTotal)) & xFxTotal & "00000000000000000000000000000000000000")
            WriteFile.WriteLine("EF" & "00000000000000000000000000000000000000000000000000000000000000000000")


            WriteFile.Close()
            OpenText("../secured/Disk/" & xCompanyCode & "_WPC.txt")

        Catch ex As Exception
            WriteFile.Close()
        End Try

    End Sub

#End Region

#Region "********UnionBank"

    Private Sub GeneratepayrollDisk_UnionBank()

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim tpath As String = Page.MapPath("Disk") '"c:\Payroll Diskette"
        Dim xfilename As String '= path & "\" & "SSSLoan-" & Format(Now, "MMMMdd") & ".TXT"
        Dim dstext As DataSet
        Dim sqlhelp As New clsBase.SQLHelper
        Dim xftotalS As Double, xPABranchCode As String, xAccountNo As String, xEffectivityDate As String, xEffectivityDated As String
        Dim sdate As Integer
        Dim xFxTotalT As String = ""
        Dim xFHeadCount As Integer = 0
        Dim fBranchCode As String = ""
        Dim xFTotal As Double, xFxTotal As String = ""
        Dim xCompanyCode As String = ""
        Dim xBatchNo As String = ""
        Dim xcompanyaccountno As String = ""

        Dim xAmount As String
        Dim faccountno As String = ""
        Dim tnet As Double = 0


        Try
            dstext = SQLHelper.ExecuteDataSet("select sum(amount) as x from dbo.EPayBankDiskDeti where paybankdiskno=" & Me.txtPayBankDiskNo.Text)
            If dstext.Tables.Count > 0 Then
                If dstext.Tables(0).Rows.Count > 0 Then
                    xftotalS = Generic.CheckDBNull(dstext.Tables(0).Rows(0)("x"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                End If
            End If

            dstext = Nothing
            xFxTotal = Replace(Replace(FormatNumber(xftotalS, 2).ToString, ",", ""), ".", "")
            'Format(xFTotal, "###0.00")
            xFxTotal = Pad.PadZero(15, xFxTotal)

            Dim dsComp As New DataSet
            dsComp = SQLHelper.ExecuteDataSet("select * from dbo.EPayBankDiskRef where payclassno=" & Me.cboPayClassNo.SelectedValue & " and banktypeno=" & Generic.CheckDBNull(Me.cboBankTypeNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType))
            If dsComp.Tables.Count > 0 Then
                If dsComp.Tables(0).Rows.Count > 0 Then
                    xPABranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_PayrollAccount"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xAccountNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("AccountNumber"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xEffectivityDated = FormatDateTime(dsComp.Tables(0).Rows(0)("EffectiveDate"), DateFormat.ShortDate)
                    xEffectivityDate = Pad.PadZero(2, Month(xEffectivityDated)) & Pad.PadZero(2, Day(xEffectivityDated)) & Pad.PadZero(2, Mid(Year(xEffectivityDated), 3, 2))
                    sdate = Day(xEffectivityDated)
                    fBranchCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BranchCode_Company"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xCompanyCode = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("CompanyCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xBatchNo = Generic.CheckDBNull(dsComp.Tables(0).Rows(0)("BatchNo"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    xcompanyaccountno = xAccountNo
                End If
            End If

            dsComp = Nothing
            xfilename = tpath & "\" & xCompanyCode & "_UNIONBANK.txt"
            FileHolder = New FileInfo(xfilename)
            WriteFile = FileHolder.CreateText()

            xAccountNo = Pad.PadZero(12, xAccountNo)

            WriteFile.WriteLine("001" & xAccountNo & "2000000000000" & xFxTotal & Pad.PadZero(4, Year(xEffectivityDated)) & Pad.PadZero(2, Month(xEffectivityDated)) & Pad.PadZero(2, Day(xEffectivityDated)))

            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("select bankaccountno,amount as net from dbo.EPayBankDiskDeti where paybankdiskno=" & Me.txtPayBankDiskNo.Text)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        xFHeadCount = xFHeadCount + 1
                        tnet = Generic.CheckDBNull(ds.Tables(0).Rows(i)("Net"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                        faccountno = Generic.CheckDBNull(Replace(ds.Tables(0).Rows(i)("bankaccountno"), "-", ""), clsBase.clsBaseLibrary.enumObjectType.StrType)
                        xAmount = Replace(Replace(FormatNumber(tnet, 2).ToString, ",", ""), ".", "")
                        If faccountno.Length < 12 Then
                            faccountno = Pad.PadZero(12, faccountno)
                        End If
                        xAmount = Pad.PadZero(15, xAmount)
                        WriteFile.WriteLine("001" & xAccountNo & "2" & faccountno & xAmount & Pad.PadZero(4, Year(xEffectivityDated)) & Pad.PadZero(2, Month(xEffectivityDated)) & Pad.PadZero(2, Day(xEffectivityDated)))

                    Next
                End If
            End If

            ds = Nothing

            'footer
            xFxTotal = Replace(xFxTotal, ",", "")


            WriteFile.Close()
            OpenText("../secured/Disk/" & xCompanyCode & "_UNIONBANK.txt")

        Catch ex As Exception
            WriteFile.Close()
        End Try

    End Sub

#End Region


End Class
