Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web
Imports System.IO

Partial Class Secured_PayPreviousList_Upload
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim tno As Integer = 0
    '******** Not Fixed **********'
    Dim TableName As String = "EPayPrevious"
    Dim FormName As String = "PayPreviousList.aspx"

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
                SQLHelper.ExecuteNonQuery("EPayPrevious_WebUpload_Delete", UserNo, item, PayLocNo)
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
                dtx = SQLHelper.ExecuteDataTable("EBatchFile_WebSave", UserNo, Generic.ToInt(txtBatchFileNo.Text), TableName, tno, tfilename & "_" & filext, txtDescription.Text, PayLocNo)
                For Each rowx As DataRow In dtx.Rows
                    BatchFileNo = Generic.ToInt(rowx("BatchFileNo"))
                Next

                '******** Not Fixed **********'
                SQLHelper.ExecuteNonQuery("EPayPrevious_WebUpload_Delete", UserNo, BatchFileNo, PayLocNo)

                Dim employeecode As String = "", applicableyear As Integer, datefrom As String, dateto As String, Description As String, TotalBasicIncome As Double, TotalOneTimeTaxableIncomeOther As Double
                Dim TotalNonTaxableIncomeOther As Double, TaxExemption As Double, TaxwithHeld As Double, bonus As Double
                Dim isAdjustment As Boolean, IsPrev As Boolean, employername As String = "", address As String = "", tinno As String
                Dim isExcludealpha As Boolean, hazardpay As Double, comission As Double
                Dim dirfee As Double, overtime As Double, profit As Double
                Dim repre As Double, cola As Double, housing As Double
                Dim transpo As Double, initmonth As Double, deminimis As Double
                Dim np As Double, holiday As Double, ismwe As Boolean

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

                    If i > 0 Then
                        employeecode = fspecArr(0)
                        applicableyear = fspecArr(1)
                        datefrom = fspecArr(2)
                        dateto = fspecArr(3)
                        Description = fspecArr(4)
                        TotalBasicIncome = Generic.ToDbl(Replace(fspecArr(5), ":", ""))
                        TotalOneTimeTaxableIncomeOther = Generic.ToDbl(Replace(fspecArr(6), ":", ""))
                        TotalNonTaxableIncomeOther = Generic.ToDbl(Replace(fspecArr(7), ":", ""))
                        TaxExemption = Generic.ToDbl(Replace(fspecArr(8), ":", ""))
                        TaxwithHeld = Generic.ToDbl(fspecArr(9))
                        bonus = Generic.ToDbl(fspecArr(10))
                        isAdjustment = Generic.ToBol(Generic.ToInt(fspecArr(11)))
                        IsPrev = Generic.ToBol(Generic.ToInt(fspecArr(12)))
                        employername = fspecArr(13)
                        address = fspecArr(14)
                        tinno = fspecArr(15)
                        isExcludealpha = Generic.ToBol(fspecArr(16))
                        hazardpay = Generic.ToDbl(fspecArr(17))
                        comission = Generic.ToDbl(fspecArr(18))
                        dirfee = Generic.ToDbl(fspecArr(19))
                        overtime = Generic.ToDbl(fspecArr(20))
                        profit = Generic.ToDbl(fspecArr(21))
                        repre = Generic.ToDbl(fspecArr(22))
                        cola = Generic.ToDbl(fspecArr(23))
                        housing = Generic.ToDbl(fspecArr(24))
                        transpo = Generic.ToDbl(fspecArr(25))
                        initmonth = Generic.ToDbl(fspecArr(26))
                        deminimis = Generic.ToDbl(fspecArr(27))
                        np = Generic.ToDbl(fspecArr(28))
                        holiday = Generic.ToDbl(fspecArr(29))
                        ismwe = Generic.ToBol(fspecArr(30))

                    End If

                    If i > 0 Then
                        If employeecode.ToString > "" Then
                            SQLHelper.ExecuteDataSet("EPayPrevious_WebUpload", UserNo, 0, employeecode, applicableyear, datefrom, dateto, Description, TotalBasicIncome,
                                                     TotalOneTimeTaxableIncomeOther, TotalNonTaxableIncomeOther, TaxExemption, TaxwithHeld, bonus, isAdjustment, IsPrev,
                                                     employername, address, tinno, isExcludealpha, hazardpay, comission, dirfee, overtime, profit, repre, cola, housing, transpo, initmonth, deminimis, np, holiday, ismwe, PayLocNo, BatchFileNo, i) ', Generic.ToInt(cboBSClientNo.SelectedValue), Generic.ToInt(cboProjectNo.SelectedValue))
                            tsuccess = tsuccess + 1
                        End If
                    End If

                    i = i + 1
                End While
                d.Close()
                Retval = 1
            End If

            If Retval = 1 Then
                ViewState("TransNo") = BatchFileNo
                lbl.Text = BatchFileNo
                PopulateGrid()
                MessageBox.Success("(" + tsuccess.ToString + ") " + MessageTemplate.SuccessSave, Me)
            ElseIf Retval = 2 Then
                MessageBox.Alert("File type must be CSV (Comma delimited).", "warning", Me)
                ModalPopupExtender1.Show()
                'MessageBox.Warning("File type must be CSV (Comma delimited).", Me)
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If

        Catch ex As Exception
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




