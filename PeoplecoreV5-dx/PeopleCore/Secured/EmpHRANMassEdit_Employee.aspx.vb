Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO

Partial Class Secured_EmpHRANEdit_Allowance
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim dt As DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "EmpHRANMassList.aspx", "EHRANMass")

        If Not IsPostBack Then
            PopulateTabHeader()
            EnabledControls()
        End If

        PopulateGrid()

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRANMassDeti_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    'Protected Sub grdMain_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMain.RowDataBound
    '    Dim row As GridViewRow = e.Row
    '    ' Intitialize TableCell list
    '    Dim columns As New List(Of TableCell)()
    '    For Each column As DataControlField In grdMain.Columns
    '        'Get the first Cell /Column
    '        Dim cell As TableCell = row.Cells(0)
    '        ' Then Remove it after
    '        row.Cells.Remove(cell)
    '        'And Add it to the List Collections
    '        columns.Add(cell)
    '    Next

    '    ' Add cells
    '    row.Cells.AddRange(columns.ToArray())

    '    Dim ds As New DataTable
    '    ds = dt
    '    Dim txtIsViewSalary As New CheckBox
    '    txtIsViewSalary.Enabled = Generic.ToInt(ds.Rows(0)("IsViewSalary"))

    '    If txtIsViewSalary.Enabled = False Then
    '        e.Row.Cells(6).Visible = False
    '        e.Row.Cells(7).Visible = False
    '    End If

    'End Sub

    Private Sub EnabledControls()
        Dim Enabled As Boolean = True

        If txtIsPosted.Checked = True Then
            Enabled = False
        End If

        Generic.EnableControls(Me, "Panel1", Enabled)

        lnkDelete.Visible = Enabled
        'lnkSave.Visible = Enabled
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRANMass_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
            Next
        Catch ex As Exception

        End Try
    End Sub


    'Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

    '    'If SaveRecord() Then
    '    '    PopulateGrid()
    '    '    MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '    'Else
    '    '    MessageBox.Critical(MessageTemplate.ErrorSave, Me)
    '    'End If

    'End Sub


    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "EmpHRANMassList.aspx", "EHRANMass") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"HRANMassDetiNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EHRANMAssDeti", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
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
            Generic.ClearControls(Me, "Panel3")
            ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave2_Click(sender As Object, e As EventArgs)
        If PoplulateCSVFile() Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
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

            If tProceed Then

                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0, employeeno As String, logtype As String = ""
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
                    employeeno = fspecArr(0)
                    rAmount = Replace(fspecArr(1), ":", "")
                    'If Len(fspecArr) > 0 Then
                    If i > 0 Then
                        If employeeno > "" And rAmount <> 0 Then
                            SQLHelper.ExecuteDataSet("EHRANMassDeti_WebUpload", UserNo, TransNo, employeeno, CDbl(rAmount))
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

        Private _Description As String
        Public Property Description As String
            Get
                Return _Description
            End Get
            Set(value As String)
                _Description = value
            End Set
        End Property

        Private _Amount As Decimal
        Public Property Amount As Decimal
            Get
                Return _Amount
            End Get
            Set(value As Decimal)
                _Amount = value
            End Set
        End Property
    End Class
#End Region

#Region "********Detail Check All********"


    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs)
        If e.ButtonType <> ColumnCommandButtonType.SelectCheckbox Then
            Return
        End If
        Dim rowEnabled As Boolean = getRowEnabledStatus(e.VisibleIndex)
        e.Enabled = rowEnabled

        'If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
        '    Dim isSelected As Boolean = Convert.ToBoolean(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
        '    If isSelected Then

        '        grdMain.Selection.SetSelection(e.VisibleIndex, True)

        '    End If
        'End If
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

    Protected Sub lnkGenerate_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "EmpHRANMassList.aspx", "EHRANMass") Then
            If SQLHelper.ExecuteNonQuery("EHRANmassDeti_WebAuto", UserNo, TransNo) > 0 Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                PopulateGrid()
            Else
                MessageBox.Warning(MessageTemplate.ErrorUpdate, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

End Class
