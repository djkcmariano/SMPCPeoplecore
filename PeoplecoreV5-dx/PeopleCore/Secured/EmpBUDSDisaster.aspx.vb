Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO

Partial Class Secured_EmpBUDSDisaster
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim PayLocNo As Integer

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable

        _dt = SQLHelper.ExecuteDataTable("EEmployeeBUDSDisaster_Web", UserNo, PayLocNo)

        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then

        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub
    Protected Sub PopulateData(id As Int64)
        Try

            Dim dt As DataTable, imagefile As String
            dt = SQLHelper.ExecuteDataTable("EEmployeeBUDSDisaster_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
                imagefile = Generic.ToStr(row("imagefile"))
                imgPhoto.ImageUrl = "frmShowImage_Local.ashx?imgpath=" & imagefile.ToString

            Next
            Dim Address As String = Generic.ToStr(hifHomeAddress.Value)
            Dim splitStr As String() = Address.Split("|"c)
            txtHomeHouseNo.Text = splitStr(0).ToString()
            txtHomeStreet.Text = splitStr(1).ToString()
            txtHomeSubd.Text = splitStr(2).ToString()
            txtHomeBarangay.Text = splitStr(3).ToString()


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeBudsDisasterNo"}))
            PopulateData(id)

            'Generic.EnableControls(Me, "pnlPopupDetl", IsEnabled)
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub


    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            mdlDetl.Show()
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

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim Path As String = ""
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeBudsDisasterNo", "imagefile"})
            Dim str As String = "", i As Integer = 0, itemId As Integer
            For Each item As Object In fieldValues
                itemId = Generic.ToInt(item(0))
                Path = Generic.ToStr(item(1))
                Generic.DeleteRecordAudit("EEmployeeBUDSDisaster", UserNo, itemId)
                If File.Exists(Path) Then
                    File.Delete(Path)
                End If
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim EmployeeBudsDisasterNo As Integer = Generic.ToInt(txtEmployeeBudsDisasterNo.Text)
        Dim employeeno As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim homeAddress As String = Generic.ToStr(hifHomeAddress.Value)
        Dim isRescue As Boolean = Generic.ToBol(txtIsRescue.Checked)
        Dim IsMedicine As Boolean = Generic.ToBol(txtIsMedicine.Checked)
        Dim IsFoodAndWater As Boolean = Generic.ToBol(txtIsFoodAndWater.Checked)
        Dim IsTranspo As Boolean = Generic.ToBol(txtIsTranspo.Checked)
        Dim IsTempShelter As Boolean = Generic.ToBol(txtIsTempShelter.Checked)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)
        Dim damage As Double = Generic.ToDbl(txtDamageCost.Text)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""

        Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String = ""

        Filename = IO.Path.GetFileName(fuPhoto.PostedFile.FileName)
        FileExt = IO.Path.GetExtension(fuPhoto.PostedFile.FileName)

        Dim fs As IO.Stream = fuPhoto.PostedFile.InputStream
        Dim br As New BinaryReader(fs)
        Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
        FileSize = fs.Length
        ActualPath = getFile_settings()
        Dim tproceed As Integer = 0
        Dim ffilepath As String = hifimagefile.Value.ToString
        If FileSize > 0 And FileSize <= 2048000 Then
            ffilepath = ActualPath & "\" & Filename.ToString
        ElseIf FileSize > 2048000 Then
            MessageBox.Critical("Allowable image not more than to 2048 bytes", Me)
            tproceed = 1
        End If
        If tproceed = 0 Then
            dt = SQLHelper.ExecuteDataTable("EEmployeeBUDSDisaster_WebSave", UserNo, EmployeeBudsDisasterNo, employeeno, homeAddress, isRescue, IsMedicine, IsFoodAndWater, IsTranspo, IsTempShelter, Remarks, damage, PayLocNo, ffilepath)
            For Each row As DataRow In dt.Rows
                RetVal = True
                error_num = Generic.ToInt(row("Error_num"))
                If error_num > 0 Then
                    error_message = Generic.ToStr(row("ErrorMessage"))
                    MessageBox.Critical(error_message, Me)
                    RetVal = False
                ElseIf FileSize > 0 And FileSize <= 2048000 Then
                    WriteFile(ActualPath & "\" & Filename.ToString, bytes)
                End If

            Next
            If RetVal = False And error_message = "" Then
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
            If RetVal = True Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            End If
        End If
        

    End Sub
   
    Private Function getFile_settings() As String
        Try


            Dim iInitArr As String
            Dim i As Integer
            Dim fs As FileStream
            Dim filename = HttpContext.Current.Server.MapPath("~/secured/connectionstr/") & "folder.ini"
            Dim retval As String = ""


            fs = New FileStream(filename, FileMode.Open, FileAccess.Read)
            Dim l As Integer = 0, ftext As String = ""
            Dim d As New StreamReader(fs)

            d.BaseStream.Seek(0, SeekOrigin.Begin)
            If d.Peek() > 0 Then
                While d.Peek() > -1
                    i = d.Peek
                    ftext = d.ReadLine()
                    iInitArr = ftext
                    If l = 0 Then
                        retval = iInitArr
                    End If
                    l = l + 1
                End While
                d.Close()
            End If
            d.Close()
            Return retval
        Catch ex As Exception

        End Try
    End Function
    Private Sub WriteFile(strPath As String, Buffer As Byte())
        'Create a file
        Dim newFile As FileStream = New FileStream(strPath, FileMode.Create)

        'Write data to the file
        newFile.Write(Buffer, 0, Buffer.Length)

        'Close file
        newFile.Close()
    End Sub

    <System.Web.Script.Services.ScriptMethod()> _
       <System.Web.Services.WebMethod()> _
    Public Shared Function cboEmployee(prefixText As String, count As Integer, contextKey As String) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()
        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AC", UserNo, prefixText, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")) & _
                                    "<" & Generic.ToStr(row("HomeAddress")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function


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


End Class




