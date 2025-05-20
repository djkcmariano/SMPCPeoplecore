Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports Microsoft.VisualBasic.FileIO
Imports System.IO

Partial Class Secured_PayLeaveEntitleList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer
    Dim PayCateNo As Integer

    Private Sub PopulateData()
        Dim PayCateNo As Integer

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPay_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            PayCateNo = Generic.ToInt(row("PayCateNo"))
        Next

        If PayCateNo = 2 Then 'Last Pay Processing
            divDetl.Visible = False
            'grdMain.Columns("Details").Visible = False
            'grdMain.Columns("Retention").Visible = True
            'grdMain.Columns("Max. Allowable").Visible = False

            ''divRetention.Style.Add("visibility", "hidden")
            ''divRetention.Style.Add("position", "absolute")
            'divPercent.Style.Add("visibility", "hidden")
            'divPercent.Style.Add("position", "absolute")
            'divIsPercent.Style.Add("visibility", "hidden")
            'divIsPercent.Style.Add("position", "absolute")
            divIsUpload.Style.Add("visibility", "hidden")
            divIsUpload.Style.Add("position", "absolute")
        Else 'Leave Conversion
            divDetl.Visible = True
            grdMain.Columns("Details").Visible = True
            grdMain.Columns("Retention").Visible = True
            grdMain.Columns("Max. Allowable").Visible = True

            divRetention.Style.Remove("visibility")
            divRetention.Style.Remove("position")
            divPercent.Style.Remove("visibility")
            divPercent.Style.Remove("position")
            divIsPercent.Style.Remove("visibility")
            divIsPercent.Style.Remove("position")
            divIsUpload.Style.Remove("visibility")
            divIsUpload.Style.Remove("position")
        End If
    End Sub

    Private Sub PopulateDataEntitled(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayLeaveEntitled_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
        For Each row As DataRow In dt.Rows
            Try
                cboLeaveTypeNo.DataSource = SQLHelper.ExecuteDataSet("ELeaveType_WebLookup_Union", UserNo, row("LeaveTypeNo"), PayLocNo)
                cboLeaveTypeNo.DataTextField = "tDesc"
                cboLeaveTypeNo.DataValueField = "tNo"
                cboLeaveTypeNo.DataBind()
            Catch ex As Exception
            End Try
            txtIsUpload.Checked = Generic.ToBol(row("IsUpload"))
            txtExcessOf.Text = Generic.ToDec(row("ExcessOf"))
            txtMaximumOf.Text = Generic.ToDec(row("MaximumOf"))
            txtismaximumInPercent.Checked = Generic.ToBol(row("IsMaximumInPercent"))
        Next
    End Sub

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayLeaveEntitled_Web", UserNo, TransNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If ViewState("TransNo") = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"PayLeaveEntitledNo", "Code", "IsUpload"})
            ViewState("TransNo") = obj(0)
            lblDetl.Text = obj(1)
            ViewState("IsUpload") = obj(2)
        End If

        PopulateGridDetl()
    End Sub

    'Populate Combo box
    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPay_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
            Next

            'Enable or Disable Controls
            If txtIsPosted.Checked = True Then
                lnkAdd.Visible = False
                lnkDelete.Visible = False
            Else
                lnkAdd.Visible = True
                lnkDelete.Visible = True
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub DisabledControls()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPay_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
            Next

            'Enable or Disable Controls
            If txtIsPosted.Checked = True Then
                lnkAdd.Visible = False
                lnkDelete.Visible = False
            Else
                lnkAdd.Visible = True
                lnkDelete.Visible = True
            End If

        Catch ex As Exception

        End Try
    End Sub


    'Populate Combo box
    Private Sub PopulateDropdownList()
        Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
        Try
            cboLeaveTypeNo.DataSource = SQLHelper.ExecuteDataSet("ELeaveType_WebLookup_Union", UserNo, 0, PayLocNo)
            cboLeaveTypeNo.DataTextField = "tDesc"
            cboLeaveTypeNo.DataValueField = "tNo"
            cboLeaveTypeNo.DataBind()
        Catch ex As Exception
        End Try
      

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticatedCoreUser()
        If Not IsPostBack Then
            PopulateDropdownList()
            PopulateData()
            PopulateTabHeader()
        End If
        PopulateGrid()

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lnk As New LinkButton, IsEnabled As Boolean = False
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Generic.ClearControls(Me, "Panel2")
        PopulateDataEntitled(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayLeaveEntitledNo"})))

        'Enable or Disable Controls
        If txtIsPosted.Checked = True Then
            IsEnabled = False
        Else
            IsEnabled = True
        End If

        Generic.EnableControls(Me, "Panel2", IsEnabled)
        btnSave.Enabled = IsEnabled
        EnabledControls(IsEnabled)

        mdlShow.Show()

    End Sub

    Private Sub EnabledControls(Optional IsEnabled As Boolean = False)

        cboEmployeeStatNo.Enabled = False
        cboEmployeeClassNo.Enabled = False
        txtIsUpload.Enabled = False
        txtExcessOf.Enabled = False
        txtMaximumOf.Enabled = False
        txtismaximumInPercent.Enabled = False

        If IsEnabled = True Then
            txtIsUpload.Enabled = True
            txtExcessOf.Enabled = True
            txtMaximumOf.Enabled = True
            txtismaximumInPercent.Enabled = True

            If txtIsUpload.Checked = False Then
                cboEmployeeStatNo.Enabled = True
                cboEmployeeClassNo.Enabled = True
            End If
        End If

    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Generic.ClearControls(Me, "Panel2")
        txtIsUpload.Checked = False
        txtExcessOf.Text = ""
        txtMaximumOf.Text = ""
        txtismaximumInPercent.Checked = False
        EnabledControls(True)
        mdlShow.Show()
    End Sub


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayLeaveEntitledNo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAuditCol("EPayLeaveEntitledDeti", UserNo, "PayLeaveEntitledNo", item)
            Generic.DeleteRecordAudit("EPayLeaveEntitled", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
    End Sub
    'Submit record
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim tNo As Integer = Generic.ToInt(txtPayLeaveEntitledNo.Text)
        Dim employeeStatNo As Integer = Generic.ToInt(cboEmployeeStatNo.SelectedValue)
        Dim employeeClassNo As Integer = Generic.ToInt(cboEmployeeClassNo.SelectedValue)
        Dim IsUpload As Boolean = Generic.ToBol(txtIsUpload.Checked)
        Dim BonusBasisNo As Integer = Generic.ToInt(cboBonusBasisNo.SelectedValue)


        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EPayLeaveEntitled_WebValidate", UserNo, tNo,
                                     TransNo,
                                     employeeStatNo,
                                     employeeClassNo,
                                     Generic.ToDec(txtExcessOf.Text),
                                     Generic.ToDec(txtMaximumOf.Text),
                                     Generic.ToInt(cboLeaveTypeNo.SelectedValue),
                                     Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0)),
                                     Generic.ToInt(cboLeaveMonitizedtypeNo.SelectedValue),
                                     Generic.ToBol(txtismaximumInPercent.Checked),
                                     txtcStartDate.Text.ToString, txtcEndDate.Text.ToString, IsUpload, BonusBasisNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlShow.Show()
            Exit Sub
        End If


        If SQLHelper.ExecuteNonQuery("EPayLeaveEntitled_WebSave", UserNo, tNo,
                                     TransNo,
                                     employeeStatNo,
                                     employeeClassNo,
                                     Generic.ToDec(txtExcessOf.Text),
                                     Generic.ToDec(txtMaximumOf.Text),
                                     Generic.ToInt(cboLeaveTypeNo.SelectedValue),
                                     Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0)),
                                     Generic.ToInt(cboLeaveMonitizedtypeNo.SelectedValue),
                                     Generic.ToBol(txtismaximumInPercent.Checked),
                                     txtcStartDate.Text.ToString, txtcEndDate.Text.ToString, IsUpload, BonusBasisNo) > 0 Then
            Retval = True

            ViewState("IsUpload") = IsUpload
        Else
            Retval = False

        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayLeaveEntitledNo", "Code", "IsUpload"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        ViewState("IsUpload") = obj(2)
        PopulateGridDetl()
    End Sub


#Region "********Detail********"

    Private Sub PopulateGridDetl()

        If Generic.ToBol(ViewState("IsUpload")) = True And txtIsPosted.Checked = False Then
            lnkAddDetl.Visible = True
            lnkDeleteDetl.Visible = True
            lnkUpload.Visible = True
        Else
            lnkAddDetl.Visible = False
            lnkDeleteDetl.Visible = False
            lnkUpload.Visible = False
        End If



        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayLeaveEntitledDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PayLeaveEntitledDetiNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EPayLeaveEntitledDeti", UserNo, item)
            i = i + 1
        Next

        If i > 0 Then
            PopulateGridDetl()
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If


    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        Try

            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayLeaveEntitledDetiNo"}))
            Generic.ClearControls(Me, "pnlPopupDetl")

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayLeaveEntitledDeti_WebOne", UserNo, Generic.ToInt(i))
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
            mdlDetl.Show()


        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)

        If Generic.ToInt(ViewState("TransNo")) > 0 Then
            PopulateGridDetl()
            Generic.ClearControls(Me, "pnlPopupDetl")
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

    Protected Sub lnkSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(ViewState("TransNo"))
        Dim PayLeaveEntitledDetiNo As Integer = Generic.ToInt(Me.txtPayLeaveEntitledDetiNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(Me.hifEmployeeNo.Value)
        Dim PercentFactorDeti As Double = Generic.ToDec(Me.txtPercentFactorDeti.Text)


        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EPayLeaveEntitledDeti_WebValidate", UserNo, PayLeaveEntitledDetiNo, tno, EmployeeNo, PercentFactorDeti)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlDetl.Show()
            Exit Sub
        End If


        If SQLHelper.ExecuteNonQuery("EPayLeaveEntitledDeti_WebSave", UserNo, PayLeaveEntitledDetiNo, tno, EmployeeNo, PercentFactorDeti) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateGridDetl()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub


#End Region



#Region "Upload"

    Protected Sub lnkUpload_Click(sender As Object, e As EventArgs)

        Generic.ClearControls(Me, "Panel3")
        ModalPopupExtender2.Show()

    End Sub

    Protected Sub lnkSave2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
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

            Dim Retval As Boolean = False
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
                Dim rCode As String = ""
                Dim rRating As String = ""
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1
                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, ",")
                    employeeno = fspecArr(0)
                    'rRating = Replace(fspecArr(2), ":", "")
                    'rCode = fspecArr(2)
                    If i > 0 Then
                        If employeeno > "" Then
                            SQLHelper.ExecuteDataSet("EPayLeaveEntitledDeti_WebUpload", UserNo, Generic.ToInt(ViewState("TransNo")), employeeno, 0)
                            tsuccess = tsuccess + 1
                        End If
                    End If

                    i = i + 1
                End While
                d.Close()
                If tsuccess > 0 Then
                    Retval = True
                End If
            Else
                Retval = False
            End If

            Return Retval
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region


End Class