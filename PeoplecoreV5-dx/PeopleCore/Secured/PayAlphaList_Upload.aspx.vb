Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web
Imports System.IO
Partial Class Secured_PayAlphaList_Upload
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim tno As Integer = 0
    '******** Not Fixed **********'
    Dim TableName As String = "EAlpha"
    Dim FormName As String = "PayAlphaList.aspx"
    Dim TransNo As Integer = 0

#Region "Main"

    Protected Sub PopulateGrid(Optional IsMain As Boolean = False)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBatchFile_Web", UserNo, TableName, TransNo, PayLocNo)
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
        TransNo = Generic.ToInt(Request.QueryString("TransNo"))
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
                SQLHelper.ExecuteNonQuery("EAlphaDeti_WebUpload_Delete", UserNo, item, PayLocNo)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
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

        Try
            Dim BatchFileNo As Integer = 0
            Dim tsuccess As Integer = 0
            Dim tCount As Integer = 0
            Dim Retval As Integer = 0
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
                tpath = (Server.MapPath("documents"))
                If Not IO.Directory.Exists(tpath) Then
                    IO.Directory.CreateDirectory(tpath)
                End If

                If tProceed = True And fileext = ".csv" Then
                    fuFilename.SaveAs(tpath & "\" & tfilename & "_" & filext)
                Else
                    tProceed = False
                    Retval = 2
                End If
            End If

            Dim amount As Double = 0

            If tProceed Then

                Dim dtx As New DataTable
                dtx = SQLHelper.ExecuteDataTable("EBatchFile_WebSave", UserNo, Generic.ToInt(txtBatchFileNo.Text), TableName, TransNo, tfilename & "_" & filext, txtDescription.Text, PayLocNo)
                For Each rowx As DataRow In dtx.Rows
                    BatchFileNo = Generic.ToInt(rowx("BatchFileNo"))
                Next

                '******** Not Fixed **********'
                SQLHelper.ExecuteNonQuery("EAlphaDeti_WebUpload_Delete", UserNo, BatchFileNo, PayLocNo)

                Dim employeecode As String = "", applicableyear As Integer, datefrom As String, dateto As String, TotalOneTimeTaxableIncomeOther As Double
                Dim BasicSalaryMWE As Double, HolidayPayMWE As Double
                Dim OTpayMWE As Double, NPPayMWE As Double, HazardPayMWE As Double, address As String = "", tinno As String, PrevEmployerName As String = "", nontaxablebonus As Double, DeminimisIncome As Double
                Dim TotalSalaryExemption As Double = 0
                Dim hazardpay As Double, comission As Double
                Dim dirfee As Double, profit As Double
                Dim repre As Double, cola As Double, housing As Double
                Dim transpo As Double, PrevNonTaxableBonus As Double, PrevNonTaxableDeminimis As Double, PrevTaxExemption As Double, prevnontaxableincome As Double, PrevBasicSalary As Double, PrevTaxableBonus As Double, PrevTaxDueJanNov As Double

                Dim ismwe As Boolean, BasicSalary As Double, taxablebonus As Double, otpaytaxable As Double, prevtotaltaxableincome As Double, insurancepremium As Double, taxdue As Double, prevtotaltaxwithheld As Double = False
                Dim taxJanNov As Double, taxDec As Double, totalnontaxableincome As Double = 0
                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0
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

                    If i > 0 And fspecArr(0).Length > 0 Then
                        employeecode = fspecArr(0)
                        applicableyear = fspecArr(1)
                        datefrom = fspecArr(2)
                        dateto = fspecArr(3)
                        ismwe = Generic.ToBol(Generic.ToInt(fspecArr(4)))
                        PrevEmployerName = Generic.ToStr(fspecArr(5))
                        address = Generic.ToStr(fspecArr(6))
                        tinno = Generic.ToStr(fspecArr(7))
                        BasicSalaryMWE = Generic.ToDbl(fspecArr(8))
                        HolidayPayMWE = Generic.ToDbl(fspecArr(9))
                        OTpayMWE = Generic.ToDbl(fspecArr(10))
                        NPPayMWE = Generic.ToDbl(fspecArr(11))
                        HazardPayMWE = Generic.ToDbl(fspecArr(12))
                        nontaxablebonus = Generic.ToDbl(fspecArr(13))
                        DeminimisIncome = Generic.ToDbl(fspecArr(14))
                        TotalSalaryExemption = Generic.ToDbl(fspecArr(15))
                        totalnontaxableincome = Generic.ToDec(fspecArr(16))
                        BasicSalary = Generic.ToDbl(fspecArr(17))
                        repre = Generic.ToDbl(fspecArr(18))
                        transpo = Generic.ToDbl(fspecArr(19))
                        cola = Generic.ToDbl(fspecArr(20))
                        housing = Generic.ToDbl(fspecArr(21))
                        TotalOneTimeTaxableIncomeOther = Generic.ToDbl(fspecArr(22))
                        comission = Generic.ToDbl(fspecArr(23))
                        profit = Generic.ToDbl(fspecArr(24))
                        dirfee = Generic.ToDbl(fspecArr(25))
                        taxablebonus = Generic.ToDbl(fspecArr(26))
                        hazardpay = Generic.ToDbl(fspecArr(27))
                        otpaytaxable = Generic.ToDbl(fspecArr(28))
                        insurancepremium = Generic.ToDec(fspecArr(29))
                        taxdue = Generic.ToDec(fspecArr(30))
                        taxJanNov = Generic.ToDec(fspecArr(31))
                        taxDec = Generic.ToDec(fspecArr(32))
                        PrevNonTaxableBonus = Generic.ToDec(fspecArr(33))
                        PrevNonTaxableDeminimis = Generic.ToDec(fspecArr(34))
                        PrevTaxExemption = Generic.ToDec(fspecArr(35))
                        prevnontaxableincome = Generic.ToDec(fspecArr(36))
                        PrevBasicSalary = Generic.ToDec(fspecArr(37))
                        PrevTaxableBonus = Generic.ToDec(fspecArr(38))
                        prevtotaltaxableincome = Generic.ToDbl(fspecArr(39))
                        PrevTaxDueJanNov = Generic.ToDec(fspecArr(40))

                    End If

                    If i > 0 Then
                        If employeecode.ToString > "" Then
                            SQLHelper.ExecuteDataSet("EAlphaDeti_WebUpload", UserNo, 0, TransNo,
                                                                 PayLocNo,
                                                                 employeecode,
                                                                 datefrom,
                                                                 dateto,
                                                                 applicableyear,
                                                                 ismwe,
                                                                 PrevEmployerName,
                                                                 address,
                                                                 tinno,
                                                                 BasicSalaryMWE,
                                                                 HolidayPayMWE,
                                                                 OTpayMWE,
                                                                 NPPayMWE,
                                                                 HazardPayMWE,
                                                                 nontaxablebonus,
                                                                 DeminimisIncome,
                                                                 TotalSalaryExemption,
                                                                 totalnontaxableincome,
                                                                 BasicSalary,
                                                                 repre,
                                                                 transpo,
                                                                 cola,
                                                                 housing,
                                                                 TotalOneTimeTaxableIncomeOther,
                                                                 comission,
                                                                 profit,
                                                                 dirfee,
                                                                 taxablebonus,
                                                                 hazardpay,
                                                                 otpaytaxable,
                                                                 prevtotaltaxableincome,
                                                                 insurancepremium,
                                                                 taxdue,
                                                                 taxJanNov,
                                                                 taxDec,
                                                                 BatchFileNo, i,
                                                                 PrevNonTaxableBonus,
                                                                 PrevNonTaxableDeminimis,
                                                                 PrevTaxExemption,
                                                                 prevnontaxableincome,
                                                                 PrevBasicSalary,
                                                                 PrevTaxableBonus,
                                                                 PrevTaxDueJanNov) ', Generic.ToInt(cboBSClientNo.SelectedValue), Generic.ToInt(cboProjectNo.SelectedValue))
                            tsuccess = tsuccess + 1
                        End If
                    End If

                    i = i + 1
                End While
                d.Close()
                Retval = 1

                'SQLHelper.ExecuteNonQuery("EAlphaDeti_WebSave_AutoUpload", UserNo, TransNo)

            End If

            If Retval = 1 Then
                ViewState("TransNo") = BatchFileNo
                lbl.Text = BatchFileNo
                PopulateGrid()
                MessageBox.Success("(" + tsuccess.ToString + ") " + MessageTemplate.SuccessSave, Me)
            ElseIf Retval = 2 Then
                MessageBox.Alert("File type must be CSV (Comma delimited).", "warning", Me)
                ModalPopupExtender1.Show()

            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If

        Catch ex As Exception
            MessageBox.Warning("Error In File.", Me)
        End Try

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





