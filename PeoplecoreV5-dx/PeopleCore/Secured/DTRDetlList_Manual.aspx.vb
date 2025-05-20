Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO

Partial Class Secured_DTRDetlList_Manual
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim DTRNo As Integer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        DTRNo = Generic.ToInt(Request.QueryString("transNo"))

        AccessRights.CheckUser(UserNo, "DTR.aspx", "EDTR")

        PopulateGrid()
        PopulateData()

        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDTR_WebOne", UserNo, DTRNo)
        For Each row As DataRow In dt.Rows
            'lnkSave.Enabled = Not Generic.ToBol(row("IsPosted"))
            lnkAdd.Enabled = Not Generic.ToBol(row("IsPosted"))
            lnkUpload.Enabled = Not Generic.ToBol(row("IsPosted"))
        Next
    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EDTRDetiEdited_Web", UserNo, DTRNo)

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

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkUpload_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "DTR.aspx", "EDTR") Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "DTR.aspx", "EDTR") Then
            Generic.ClearControls(Me, "pnlPopup")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)

            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "DTR.aspx", "EDTR") Then

                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRDetiEditedNo"}))

                Generic.ClearControls(Me, "pnlPopup")
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EDTRDetiEdited_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
                    Generic.PopulateData(Me, "pnlPopup", dt)
                Next

                Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
                Generic.EnableControls(Me, "pnlPopup", IsEnabled)
                btnSave.Enabled = IsEnabled

                mdlShow.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "DTR.aspx", "EDTR") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRDetiEditedNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTRDetiEdited", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Private Function SaveRecord() As Integer
        Dim DTRDetiEditedNo As Integer = Generic.ToInt(txtDTRDetiEditedNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim WorkingHrs As Double = Generic.ToDec(txtWorkingHrs.Text)
        Dim NP As Double = Generic.ToInt(txtNP.Text)
        Dim AbsHrs As Double = Generic.ToDec(txtAbsHrs.Text)
        Dim Late As Double = Generic.ToDec(txtLate.Text)
        Dim Under As Double = Generic.ToDec(txtUnder.Text)
        Dim VL As Double = Generic.ToDec(txtVL.Text)
        Dim SL As Double = Generic.ToDec(txtSL.Text)
        Dim OB As Double = Generic.ToDec(txtOB.Text)
        Dim EL As Double = Generic.ToDec(txtEL.Text)
        Dim ML As Double = Generic.ToDec(txtML.Text)
        Dim PTL As Double = Generic.ToDec(txtPTL.Text)
        Dim FL As Double = Generic.ToDec(txtFL.Text)
        Dim PL As Double = Generic.ToDec(txtPL.Text)
        Dim SPL As Double = Generic.ToDec(txtSPL.Text)
        Dim Ovt As Double = Generic.ToDec(txtOvt.Text)
        Dim Ovt8 As Double = Generic.ToDec(txtOvt8.Text)
        Dim NPOvt As Double = Generic.ToDec(txtNPOvt.Text)
        Dim NPOvt8 As Double = Generic.ToDec(txtNPOvt8.Text)
        Dim RDOvt As Double = Generic.ToDec(txtRDOvt.Text)
        Dim RDOvt8 As Double = Generic.ToDec(txtRDOvt8.Text)
        Dim RDOvtNP As Double = Generic.ToDec(txtRDOvtNP.Text)
        Dim RDOvt8NP As Double = Generic.ToDec(txtRDOvt8NP.Text)
        Dim RHNROvt As Double = Generic.ToDec(txtRHNROvt.Text)
        Dim RHNROvt8 As Double = Generic.ToDec(txtRHNROvt8.Text)
        Dim RHNROvtNP As Double = Generic.ToDec(txtRHNROvtNP.Text)
        Dim RHNROvt8NP As Double = Generic.ToDec(txtRHNROvt8NP.Text)
        Dim RHRDOvt As Double = Generic.ToDec(txtRHRDOvt.Text)
        Dim RHRDOvt8 As Double = Generic.ToDec(txtRHRDOvt8.Text)
        Dim RHRDOvtNP As Double = Generic.ToDec(txtRHRDOvtNP.Text)
        Dim RHRDOvt8NP As Double = Generic.ToDec(txtRHRDOvt8NP.Text)
        Dim SHNROvt As Double = Generic.ToDec(txtSHNROvt.Text)
        Dim SHNROvt8 As Double = Generic.ToDec(txtSHNROvt8.Text)
        Dim SHNROvtNP As Double = Generic.ToDec(txtSHNROvtNP.Text)
        Dim SHNROvt8NP As Double = Generic.ToDec(txtSHNROvt8NP.Text)
        Dim SHRDOvt As Double = Generic.ToDec(txtSHRDOvt.Text)
        Dim SHRDOvt8 As Double = Generic.ToDec(txtSHRDOvt8.Text)
        Dim SHRDOvtNP As Double = Generic.ToDec(txtSHRDOvtNP.Text)
        Dim SHRDOvt8NP As Double = Generic.ToDec(txtSHRDOvt8NP.Text)

        If SQLHelper.ExecuteNonQuery("EDTRDetiEdited_WebSave", UserNo, DTRDetiEditedNo, DTRNo, EmployeeNo, WorkingHrs, NP, AbsHrs, Late, Under, VL, SL, OB, EL, ML, PTL, FL, PL, SPL, Ovt, Ovt8, NPOvt, NPOvt8, RDOvt, RDOvt8, RDOvtNP, RDOvt8NP, RHNROvt, RHNROvt8, RHNROvtNP, RHNROvt8NP, RHRDOvt, RHRDOvt8, RHRDOvtNP, RHRDOvt8NP, SHNROvt, SHNROvt8, SHNROvtNP, SHNROvt8NP, SHRDOvt, SHRDOvt8, SHRDOvtNP, SHRDOvt8NP, PayLocNo) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = True

        End If
    End Function

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim status As Boolean = SaveFile()
        If status Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        ElseIf Not status Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
       
        End If

    End Sub
    Private Function SaveFile() As Boolean
        Dim tsuccess As Integer = 0
        Try

            Dim DTRCode As String = ""
            Dim _ds As New DataSet
            _ds = SQLHelper.ExecuteDataSet("EDTR_WebOne", UserNo, DTRNo)
            If _ds.Tables.Count > 0 Then
                If _ds.Tables(0).Rows.Count > 0 Then
                    DTRCode = Generic.ToStr(_ds.Tables(0).Rows(0)("DTRCode"))
                End If
            End If

            Dim lastname As String = ""
            Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False
            Dim tpath As String = ""
            Dim datenow As Date
            datenow = Now()

            Dim filext As String = Pad.PadZero(2, Month(datenow)) & Pad.PadZero(2, Day(datenow)) & Pad.PadZero(4, Year(datenow)) & Pad.PadZero(2, Hour(datenow)) & Pad.PadZero(2, Minute(datenow)) & Pad.PadZero(4, Second(datenow))
            If fuDoc.HasFile = True Then
                tFilepath = fuDoc.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                Dim fileext As String = IO.Path.GetExtension(tFilepath)
                tProceed = True
                tpath = (Server.MapPath("DTRDetiSummaryManual")) 'Me.MapPath("documents") & "\
                If Not IO.Directory.Exists(tpath) Then
                    IO.Directory.CreateDirectory(tpath)
                End If
                fuDoc.SaveAs(tpath & "\" & tfilename & "_" & DTRCode & "_" & filext)

            End If


            Dim amount As Double = 0, employeecode As String = ""

            If tProceed Then

                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0, logtype As String = ""
                Dim fs As FileStream, fFilename As String
                fFilename = tpath & "\" & tfilename & "_" & DTRCode & "_" & filext 'tpath & "\" & tfilename
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                Dim employeeno As String = ""
                Dim RegHrs As String = ""   '2
                Dim Absences As String = "" '3
                Dim Late As String = ""     '4
                Dim Under As String = ""    '5

                Dim Ovt As String = ""      '6
                Dim Ovt8 As String = ""      '7
                Dim NP As String = ""       '8
                Dim NPOvt As String = ""    '9
                Dim NPOvt8 As String = ""    '10

                Dim RDOvt As String = ""    '11
                Dim RDOvt8 As String = ""   '12
                Dim RDOvtNP As String = ""  '13
                Dim RDOvtNP8 As String = "" '14

                Dim VL As String = ""       '15
                Dim SL As String = ""       '16
                Dim ML As String = ""       '17
                Dim BL As String = ""       '18
                Dim PL As String = ""       '19
                Dim UL As String = ""       '20

                Dim RHNROvt As String = ""  '21
                Dim RHNROvt8 As String = "" '22
                Dim RHNROvtNP As String = ""    '23
                Dim RHNROvtNP8 As String = ""   '24

                Dim SHNROvt As String = ""      '25
                Dim SHNROvt8 As String = ""     '26
                Dim SHNROvtNP As String = ""    '27
                Dim SHNROvtNP8 As String = ""   '28

                Dim RHRDOvt As String = ""      '29
                Dim RHRDOvt8 As String = ""     '30
                Dim RHRDOvtNP As String = ""    '31
                Dim RHRDOvtNP8 As String = ""   '32

                Dim SHRDOvt As String = ""      '33
                Dim SHRDOvt8 As String = ""     '34
                Dim SHRDOvtNP As String = ""    '35
                Dim SHRDOvtNP8 As String = ""   '36

                Dim NoOfDays As String = ""     '37
                Dim HolCount As String = ""     '38

                Dim AmountAdj As String = "0"    '39
                Dim Sal_Deduct As String = "0"   '40
                Dim Allowance As String = "0"    '41





                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1

                    nfile = d.ReadLine()
                    If i >= 1 Then
                        fspecArr = Split(nfile, ",")
                        employeeno = fspecArr(0)
                        RegHrs = Replace(fspecArr(2), " ", "")
                        Absences = Replace(fspecArr(3), " ", "")
                        Late = Replace(fspecArr(4), " ", "")
                        Under = Replace(fspecArr(5), " ", "")

                        Ovt = Replace(fspecArr(6), " ", "")
                        Ovt8 = Replace(fspecArr(7), " ", "")
                        NP = Replace(fspecArr(8), " ", "")
                        NPOvt = Replace(fspecArr(9), " ", "")
                        NPOvt8 = Replace(fspecArr(10), " ", "")

                        RDOvt = Replace(fspecArr(11), " ", "")
                        RDOvt8 = Replace(fspecArr(12), " ", "")
                        RDOvtNP = Replace(fspecArr(13), " ", "")
                        RDOvtNP8 = Replace(fspecArr(14), " ", "")

                        VL = Replace(fspecArr(15), " ", "")
                        SL = Replace(fspecArr(16), " ", "")
                        ML = Replace(fspecArr(17), " ", "")
                        BL = Replace(fspecArr(18), " ", "")
                        PL = Replace(fspecArr(19), " ", "")
                        UL = Replace(fspecArr(20), " ", "")

                        RHNROvt = Replace(fspecArr(21), " ", "")
                        RHNROvt8 = Replace(fspecArr(22), " ", "")
                        RHNROvtNP = Replace(fspecArr(23), " ", "")
                        RHNROvtNP8 = Replace(fspecArr(24), " ", "")

                        SHNROvt = Replace(fspecArr(25), " ", "")
                        SHNROvt8 = Replace(fspecArr(26), " ", "")
                        SHNROvtNP = Replace(fspecArr(27), " ", "")
                        SHNROvtNP8 = Replace(fspecArr(28), " ", "")

                        RHRDOvt = Replace(fspecArr(29), " ", "")
                        RHRDOvt8 = Replace(fspecArr(30), " ", "")
                        RHRDOvtNP = Replace(fspecArr(31), " ", "")
                        RHRDOvtNP8 = Replace(fspecArr(32), " ", "")

                        SHRDOvt = Replace(fspecArr(33), " ", "")
                        SHRDOvt8 = Replace(fspecArr(34), " ", "")
                        SHRDOvtNP = Replace(fspecArr(35), " ", "")
                        SHRDOvtNP8 = Replace(fspecArr(36), " ", "")

                        HolCount = Replace(fspecArr(37), " ", "")
                        NoOfDays = Replace(fspecArr(38), " ", "")

                        'AmountAdj = Replace(fspecArr(38), " ", "")
                        'Sal_Deduct = Replace(fspecArr(39), " ", "")
                        'Allowance = Replace(fspecArr(40), " ", "")

                        'If Len(fspecArr) > 0 Then

                        If employeeno > "" Then
                            SQLHelper.ExecuteDataSet("EDTRDetiEdited_WebUpload", UserNo, employeeno, DTRNo, Generic.ToDec(RegHrs), Generic.ToDec(Ovt), Generic.ToDec(Ovt8), Generic.ToDec(NP), Generic.ToDec(NPOvt), Generic.ToDec(NPOvt8), Generic.ToDec(RDOvt), Generic.ToDec(RDOvt8), Generic.ToDec(RDOvtNP), Generic.ToDec(RDOvtNP8), Generic.ToDec(VL), Generic.ToDec(SL), Generic.ToDec(ML), Generic.ToDec(BL), Generic.ToDec(PL), Generic.ToDec(UL), Generic.ToDec(RHNROvt), Generic.ToDec(RHNROvt8), Generic.ToDec(RHNROvtNP), Generic.ToDec(RHNROvtNP8), Generic.ToDec(SHNROvt), Generic.ToDec(SHNROvt8), Generic.ToDec(SHNROvtNP), Generic.ToDec(SHNROvtNP8), Generic.ToDec(RHRDOvt), Generic.ToDec(RHRDOvt8), Generic.ToDec(RHRDOvtNP), Generic.ToDec(RHRDOvtNP8), Generic.ToDec(SHRDOvt), Generic.ToDec(SHRDOvt8), Generic.ToDec(SHRDOvtNP), Generic.ToDec(SHRDOvtNP8), Generic.ToDec(Sal_Deduct), Generic.ToDec(Late), Generic.ToDec(NoOfDays), Generic.ToDec(Absences), Generic.ToDec(AmountAdj), Generic.ToDec(HolCount), Generic.ToDec(Allowance), Generic.ToDec(Under), PayLocNo)
                            tsuccess = tsuccess + 1

                        End If
                    ElseIf i = 0 Then
                        fspecArr = Split(nfile, ",")
                        Dim employeeno_field As String = fspecArr(0)
                        'If employeeno_field = "employeeno" Or employeeno_field = "Employee No." Then
                        'Else
                        '    MessageBox.Alert("File header not found", "warning", Me)
                        'End If
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
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub

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
