Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web
Imports System.IO

Partial Class Secured_PayMainIncomeForwList_Upload
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim tno As Integer = 0
    '******** Not Fixed **********'
    Dim TableName As String = "EPayMainIncomeForw"
    Dim FormName As String = "PayMainIncomeForwList.aspx"

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
                SQLHelper.ExecuteNonQuery("EPayMainIncomeForw_WebUpload_Delete", UserNo, item, PayLocNo)
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

                Dim xtProceed As Integer = 0, AlertType As String = "", Msg As String = ""
                Dim dty As New DataTable
                dty = SQLHelper.ExecuteDataTable("EBatchFile_WebSave_Check", UserNo, Generic.ToInt(txtBatchFileNo.Text), TableName, tno, tfilename, txtDescription.Text, PayLocNo)
                For Each rowy As DataRow In dty.Rows
                    xtProceed = Generic.ToInt(rowy("tProceed"))
                    AlertType = Generic.ToStr(rowy("AlertType"))
                    Msg = Generic.ToStr(rowy("Message"))
                Next

                'If IO.File.Exists(tpath & "\" & tfilename) Then
                '    tProceed = False
                '    MessageBox.Alert("The filename (" + tfilename + ") of the file being uploaded already exists in the directory. Please create another filename.", "warning", Me)
                '    ModalPopupExtender1.Show()
                '    Exit Sub
                'End If

                If xtProceed = 1 Then
                    tProceed = False
                    MessageBox.Alert(Msg, AlertType, Me)
                    ModalPopupExtender1.Show()
                    Exit Sub
                End If

                If tProceed = True And fileext = ".csv" Then
                    fuFilename.SaveAs(tpath & "\" & tfilename & "_" & filext)
                Else
                    tProceed = False
                    Retval = 2
                End If
            End If

            Dim amount As Double = 0, employeecode As String = ""

            If tProceed Then

                Dim dtx As New DataTable
                dtx = SQLHelper.ExecuteDataTable("EBatchFile_WebSave", UserNo, Generic.ToInt(txtBatchFileNo.Text), TableName, tno, tfilename, txtDescription.Text, PayLocNo)
                For Each rowx As DataRow In dtx.Rows
                    BatchFileNo = Generic.ToInt(rowx("BatchFileNo"))
                Next

                '******** Not Fixed **********'
                SQLHelper.ExecuteNonQuery("EPayMainIncomeForw_WebUpload_Delete", UserNo, BatchFileNo, PayLocNo)

                Dim dt As New DataTable
                Dim Incometypecode As String = ""
                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0, employeeno As String = "", logtype As String = ""
                Dim fs As FileStream, fFilename As String
                fFilename = tpath & "\" & tfilename & "_" & filext 'tpath & "\" & tfilename
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                Dim rDesc As String = ""
                Dim rAmount As String = ""
                Dim rEffectiveDate As String = ""
                Dim IsSpecialPay As String = ""
                Dim IsBonus As String = ""
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1

                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, ",")

                    If i > 0 Then
                        employeeno = fspecArr(0)
                        Incometypecode = fspecArr(1)
                        rDesc = fspecArr(2)
                        rAmount = Replace(fspecArr(3), ":", "")
                        rEffectiveDate = fspecArr(4)
                        IsSpecialPay = fspecArr(5)
                        IsBonus = fspecArr(6)
                    End If

                    If i >= 1 Then
                        If employeeno.ToString > "" Then
                            dt = SQLHelper.ExecuteDataTable("EPayMainIncomeForw_WebUpload_BatchFile", UserNo, i, BatchFileNo, employeeno, Incometypecode, rDesc, CDbl(rAmount), rEffectiveDate, IsSpecialPay, IsBonus, PayLocNo)
                            For Each row As DataRow In dt.Rows
                                tsuccess = Generic.ToInt(row("tProceed"))
                            Next
                            If tsuccess = 1 Then
                                tCount = tCount + 1
                            End If
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
                MessageBox.Success("(" + tCount.ToString + ") " + MessageTemplate.SuccessSave, Me)
            ElseIf Retval = 2 Then
                MessageBox.Alert("File type must be CSV (Comma delimited).", "warning", Me)
                ModalPopupExtender1.Show()
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



