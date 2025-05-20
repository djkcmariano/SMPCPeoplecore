Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports System.IO

Partial Class Secured_PayPreviousList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Private Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayPrevious_Web", UserNo, PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        PopulateGrid()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayPreviousNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayPrevious", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/paypreviousedit.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Response.Redirect("~/secured/paypreviousedit.aspx?id=" & Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayPreviousNo"})))
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub
#Region "Upload"

    Protected Sub lnkUpload_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, Session("xFormName"), Session("xTableName")) Then
            'Generic.ClearControls(Me, "Panel3")
            'ModalPopupExtender6.Show()
            Response.Redirect("~/secured/PayPreviousList_Upload.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub
    Protected Sub lnkSave2_Click(sender As Object, e As EventArgs)
        Dim retVal As Integer = PoplulateCSVFile_Upload()
        If retVal = 1 Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        ElseIf retVal = 2 Then
            MessageBox.Alert("The file must have a header.", "warning", Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub
    Private Function PoplulateCSVFile_Upload() As Integer
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

            SQLHelper.ExecuteNonQuery("EPayPrevious_WebUpload_Delete", UserNo, txtBatchNumber.Text.ToString)

            Dim amount As Double = 0

            If tProceed Then
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
                        isAdjustment = Generic.ToBol(fspecArr(11))
                        IsPrev = Generic.ToBol(fspecArr(12))
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
                                                     employername, address, tinno, isExcludealpha, hazardpay, comission, dirfee, overtime, profit, repre, cola, housing, transpo, initmonth, deminimis, np, holiday, ismwe, PayLocNo, txtBatchNumber.Text.ToString) ', Generic.ToInt(cboBSClientNo.SelectedValue), Generic.ToInt(cboProjectNo.SelectedValue))
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
#End Region
End Class

