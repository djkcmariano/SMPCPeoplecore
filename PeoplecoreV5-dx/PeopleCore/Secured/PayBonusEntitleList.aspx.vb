Imports System.Data
Imports clsLib
Imports System.Math
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports Microsoft.VisualBasic.FileIO
Imports System.IO

Partial Class Secured_PayBonusEntitleList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer
    Dim PayCateNo As Integer


    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPay_WebOne", UserNo, TransNo)
        Generic.PopulateData(Me, "Panel1", dt)
        For Each row As DataRow In dt.Rows
            lnkAdd.Enabled = Not Generic.ToBol(row("IsPosted"))
            lnkSave.Enabled = Not Generic.ToBol(row("IsPosted"))
            PayCateNo = Generic.ToInt(row("PayCateNo"))
        Next

        If PayCateNo = 2 Then 'Last Pay Processing
            divDetl.Visible = False
            div1.Visible = False
            grdMain.Columns("Details").Visible = False
            'grdMain.Columns("Period Type").Visible = False
            'grdMain.Columns("Eligibility<br/>Date").Visible = False
            divUpload.Style.Add("visibility", "hidden")
            divUpload.Style.Add("position", "absolute")
            divEligibility.Style.Add("visibility", "hidden")
            divEligibility.Style.Add("position", "absolute")
            divPEPeriod.Style.Add("visibility", "hidden")
            divPEPeriod.Style.Add("position", "absolute")
        Else 'Bonus Processing
            divDetl.Visible = False
            grdMain.Columns("Details").Visible = True
            'grdMain.Columns("Period Type").Visible = True
            'grdMain.Columns("Eligibility<br/>Date").Visible = True
            divUpload.Style.Remove("visibility")
            divUpload.Style.Remove("position")
            divEligibility.Style.Remove("visibility")
            divEligibility.Style.Remove("position")
            divPEPeriod.Style.Remove("visibility")
            divPEPeriod.Style.Remove("position")
        End If

    End Sub

    Private Sub PopulateDataEntitled(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayBonusEntitled_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
        For Each row As DataRow In dt.Rows
            txtIsUpload.Checked = Generic.ToBol(row("IsUpload"))
            cboPEPeriodNo.Text = Generic.ToStr(row("PEPeriodNo"))
            txtEligibleDate.Text = Generic.ToStr(row("EligibleDate"))
            Dim bonusbasisno As Integer = cboBonusBasisNo.SelectedValue
            fRegisterStartupScript("Sript", "disableenable_behind('" + bonusbasisno.ToString + "');")
        Next

    End Sub

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayBonusEntitled_Web", UserNo, TransNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If ViewState("TransNo") = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"PayBonusEntitledNo", "Code", "IsUpload"})
            'Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"PayBonusEntitledNo", "Code"})
            ViewState("TransNo") = obj(0)
            lblDetl.Text = obj(1)
            ViewState("IsUpload") = obj(2)
        End If

        PopulateGridDetl()
    End Sub

    


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticatedCoreUser()
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
            Try
                cboPEPeriodNo.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EPEPeriod", PayLocNo, "", "")
                cboPEPeriodNo.DataTextField = "tDesc"
                cboPEPeriodNo.DataValueField = "tNo"
                cboPEPeriodNo.DataBind()
            Catch ex As Exception
            End Try

            PopulateData()
            PopulateTabHeader()

            divDetl.Visible = False
            div1.Visible = False
            div2.Visible = False
        End If
        PopulateGrid()
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

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "Panel2")
        txtIsUpload.Checked = False
        cboPEPeriodNo.Text = ""
        txtEligibleDate.Text = ""
        EnabledControls(True)
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayBonusEntitledNo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAuditCol("EPayBonusEntitledDeti", UserNo, "PayBonusEntitledNo", item)
            Generic.DeleteRecordAudit("EPayBonusEntitled", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton, IsEnabled As Boolean = False
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Generic.ClearControls(Me, "Panel2")
        PopulateDataEntitled(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBonusEntitledNo"})))

        'Enable or Disable Controls
        If txtIsPosted.Checked = True Then
            IsEnabled = False
        Else
            IsEnabled = True
        End If

        Generic.EnableControls(Me, "Panel2", IsEnabled)
        lnkSave.Enabled = IsEnabled
        EnabledControls(IsEnabled)

        ModalPopupExtender1.Show()

    End Sub
    Private Sub EnabledControls(Optional IsEnabled As Boolean = False)

        txtIsUpload.Enabled = False
        cboEmployeeStatNo.Enabled = False
        cboEmployeeClassNo.Enabled = False
        txtEligibleDate.Enabled = False
        txtMinServiceYear.Enabled = False
        cboPEPeriodNo.Enabled = False

        If IsEnabled = True Then
            cboPEPeriodNo.Enabled = True
            txtIsUpload.Enabled = True
            If txtIsUpload.Checked = False Then
                cboEmployeeStatNo.Enabled = True
                cboEmployeeClassNo.Enabled = True
                txtEligibleDate.Enabled = True
                txtMinServiceYear.Enabled = True
            Else
            End If
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim datebaseNo As Integer = Generic.ToInt(cboDatebaseNo.SelectedValue)
        Dim bonusTypeNo As Integer = Generic.ToInt(cboBonusTypeNo.SelectedValue)
        Dim bonusBasisNo As Integer = Generic.ToInt(cboBonusBasisNo.SelectedValue)
        Dim noofmonthsassume As Double = Generic.ToDec(txtnoofmonthsassume.Text)
        Dim IsUpload As Boolean = Generic.ToBol(txtIsUpload.Checked)
        Dim PEPeriodNo As Integer = Generic.ToInt(cboPEPeriodNo.SelectedValue)
        Dim fixedamount As Double = Generic.ToDbl(txtFixedAmount.Text)
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable

        dtx = SQLHelper.ExecuteDataTable("EPayBonusEntitled_WebValidate", UserNo, Generic.ToInt(txtPayBonusEntitledNo.Text), TransNo, Generic.ToInt(Me.cboEmployeeStatNo.SelectedValue), _
                                     Generic.ToInt(Me.cboEmployeeClassNo.SelectedValue), Generic.ToDec(Me.txtMinServiceYear.Text), Generic.ToDec(Me.txtPercentFactor.Text), _
                                     txtIsApplytoAll.Checked, datebaseNo, bonusBasisNo, bonusTypeNo, txtcStartDate.Text.ToString, txtcEndDate.Text.ToString, noofmonthsassume,
                                     Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0)), Generic.ToStr(txtEligibleDate.Text), IsUpload, PEPeriodNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EPayBonusEntitled_WebSave", UserNo, Generic.ToInt(txtPayBonusEntitledNo.Text), TransNo, Generic.ToInt(Me.cboEmployeeStatNo.SelectedValue), _
                                     Generic.ToInt(Me.cboEmployeeClassNo.SelectedValue), Generic.ToDec(Me.txtMinServiceYear.Text), Generic.ToDec(Me.txtPercentFactor.Text), _
                                     txtIsApplytoAll.Checked, datebaseNo, bonusBasisNo, bonusTypeNo, txtcStartDate.Text.ToString, txtcEndDate.Text.ToString, noofmonthsassume,
                                     Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0)), Generic.ToStr(txtEligibleDate.Text), IsUpload, PEPeriodNo, fixedamount) > 0 Then
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
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBonusEntitledNo", "Code", "IsUpload"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        ViewState("IsUpload") = obj(2)
        PopulateGridDetl()
        PopulateTabHeader()
        If PayCateNo <> 2 Then
            divDetl.Visible = True
            div1.Visible = False
            div2.Visible = False
        End If
    End Sub
    Protected Sub lnkFactor_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBonusEntitledNo", "Code", "IsUpload"})
        ViewState("TransNo") = obj(0)
        Label1.Text = obj(1)
        ViewState("IsUpload") = obj(2)
        PopulateGrid_Factor()
        PopulateTabHeader()
        If PayCateNo <> 2 Then
            divDetl.Visible = False
            div1.Visible = True
            div2.Visible = False
        End If
    End Sub
    Protected Sub lnkIncome_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBonusEntitledNo", "Code", "IsUpload"})
        ViewState("TransNo") = obj(0)
        Label1.Text = obj(1)
        ViewState("IsUpload") = obj(2)
        PopulateGrid_Income()
        PopulateTabHeader()
        If PayCateNo <> 2 Then
            divDetl.Visible = False
            div1.Visible = False
            div2.Visible = True
        End If
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
        dt = SQLHelper.ExecuteDataTable("EPayBonusEntitledDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
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

        Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PayBonusEntitledDetiNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EPayBonusEntitledDeti", UserNo, item)
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
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBonusEntitledDetiNo"}))
            Generic.ClearControls(Me, "pnlPopupDetl")

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayBonusEntitledDeti_WebOne", UserNo, Generic.ToInt(i))
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
        Dim PayBonusEntitledDetiNo As Integer = Generic.ToInt(Me.txtPayBonusEntitledDetiNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(Me.hifEmployeeNo.Value)
        Dim PercentFactorDeti As Double = Generic.ToDec(Me.txtPercentFactorDeti.Text)


        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EPayBonusEntitledDeti_WebValidate", UserNo, PayBonusEntitledDetiNo, tno, EmployeeNo, PercentFactorDeti)

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


        If SQLHelper.ExecuteNonQuery("EPayBonusEntitledDeti_WebSave", UserNo, PayBonusEntitledDetiNo, tno, EmployeeNo, PercentFactorDeti) > 0 Then
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
                            SQLHelper.ExecuteDataSet("EPayBonusEntitledDeti_WebUpload", UserNo, Generic.ToInt(ViewState("TransNo")), employeeno, 0)
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

#Region "Factor"

    Private Sub PopulateGrid_Factor(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayBonusEntitledFactor_Web", UserNo, ViewState("TransNo"))
        Me.grdFactor.DataSource = _dt
        Me.grdFactor.DataBind()

    End Sub
    Private Sub PopulateData_Factor(Id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayBonusEntitledFactor_WebOne", UserNo, Id)
        Generic.PopulateData(Me, "Panel1", dt)
        For Each row As DataRow In dt.Rows
            Textbox1.Text = Generic.ToStr(row("Code"))
            TextBox2.Text = Generic.ToDbl(row("PercentFactor"))
        Next

    End Sub
    Protected Sub lnkAddFactor_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "Panel1")
        ModalPopupExtender3.Show()
    End Sub

    Protected Sub lnkDeleteFactor_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdFactor.GetSelectedFieldValues(New String() {"PayBonusEntitledFactorNo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EPayBonusEntitledFactor", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid_Factor()
    End Sub
    Protected Sub lnkEditFactor_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton, IsEnabled As Boolean = False
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Generic.ClearControls(Me, "Panel1")
        PopulateData_Factor(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBonusEntitledFactorNo"})))
        ModalPopupExtender3.Show()

    End Sub

    Protected Sub lnkSaveFactor_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim sfrom As Double = Generic.ToDbl(txtSFrom.Text)
        Dim sto As Double = Generic.ToDbl(txtSTo.Text)
        Dim percentFactor As Double = Generic.ToDbl(txtPercentFactor.Text)
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable


        Dim dt As DataTable, error_num As Integer = 0, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("EPayBonusEntitledFactor_WebSave", UserNo, Generic.ToInt(txtPayBonusEntitledFactorNo.Text), Generic.ToInt(ViewState("TransNo")), Generic.ToInt(sfrom), _
                                      Generic.ToInt(sto), Generic.ToDec(TextBox2.Text))
        For Each row As DataRow In dt.Rows
            Retval = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                Retval = False
            End If
        Next
        If Retval = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If Retval = True Then
            PopulateGrid_Factor()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub
    Protected Sub lnkExportFactor_Click(sender As Object, e As EventArgs)
        Try
            grdExportFactor.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub
#End Region
#Region "Income type"

    Private Sub PopulateGrid_Income(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayBonusEntitledAllowance_Web", UserNo, ViewState("TransNo"))
        Me.grdIncometype.DataSource = _dt
        Me.grdIncometype.DataBind()

    End Sub
    Private Sub PopulateData_Income(Id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayBonusEntitledAllowance_WebOne", UserNo, Id)
        Generic.PopulateData(Me, "PnlIncome", dt)
        For Each row As DataRow In dt.Rows
            Textbox4.Text = Generic.ToStr(row("Code"))
            Generic.PopulateDropDownList_Union(UserNo, Me, "PnlIncome", dt, PayLocNo)
        Next

    End Sub
    Protected Sub lnkAddIncome_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "PnlIncome")
        Generic.PopulateDropDownList(UserNo, Me, "PnlIncome", PayLocNo)
        ModalPopupExtender4.Show()
    End Sub

    Protected Sub lnkDeleteIncome_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdIncometype.GetSelectedFieldValues(New String() {"PayBonusEntitledAllowanceNo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EPayBonusEntitledAllowance", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid_Income()
    End Sub
    Protected Sub lnkEditIncome_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton, IsEnabled As Boolean = False
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Generic.ClearControls(Me, "PnlIncome")
        PopulateData_Income(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBonusEntitledAllowanceNo"})))
        ModalPopupExtender4.Show()

    End Sub

    Protected Sub lnkSaveIncome_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim sfrom As Double = Generic.ToDbl(txtSFrom.Text)
        Dim sto As Double = Generic.ToDbl(txtSTo.Text)
        Dim percentFactor As Double = Generic.ToDbl(txtPercentFactor.Text)
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable


        Dim dt As DataTable, error_num As Integer = 0, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("EPayBonusEntitledAllowance_WebSave", UserNo, Generic.ToInt(txtPayBonusEntitledAllowanceNo.Text), Generic.ToInt(ViewState("TransNo")), Generic.ToInt(cboPayIncomeTypeNo.SelectedValue))
        For Each row As DataRow In dt.Rows
            Retval = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                Retval = False
            End If
        Next
        If Retval = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If Retval = True Then
            PopulateGrid_Income()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub
   
#End Region

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
End Class






