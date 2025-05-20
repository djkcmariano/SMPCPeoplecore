Imports clsLib
Imports System.Data
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Partial Class Secured_PayLastDTRManualList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim PayLastDetiNo As Integer = 0
    Dim PayNo As Integer = 0

    Dim EmployeeNo As Integer = 0


    Private Sub PopulateGrid(Optional IsMain As Boolean = False)

        'Show or Hide Buttons
        If txtIsPosted.Checked = True Then
            lnkAdd.Visible = False
            lnkDelete.Visible = False
        Else
            lnkAdd.Visible = True
            lnkDelete.Visible = True
        End If

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayDTRManual_Web", UserNo, Session("PayLastList_PayNo"), PayLastDetiNo)
        grdMain.DataSource = _dt
        grdMain.DataBind()

    End Sub
    Private Sub PopulateDropdown()
        Try
            cboEmployeeNo.DataSource = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_LastPay", UserNo, Session("PayLastList_PayNo"))
            cboEmployeeNo.DataTextField = "tDesc"
            cboEmployeeNo.DataValueField = "tNo"
            cboEmployeeNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        PayLastDetiNo = Generic.ToInt(Request.QueryString("id"))
        EmployeeNo = Generic.ToInt(Request.QueryString("employeeNo"))
        PayNo = Generic.ToInt(Request.QueryString("PayNo"))
        Permission.IsAuthenticatedCoreUser()
        PopulateTabHeader()
        HeaderInfo1.xFormName = "EPayLastDeti"

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", Generic.ToInt(Session("xPayLocNo")))
            PopulateDropdown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPay_WebTabHeader", UserNo, Session("PayLastList_PayNo"))
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
                'PayNo = Generic.ToInt(row("PayNo"))
                'EmployeeNo = Generic.ToInt(row("EmployeeNo"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer, IsEnabled As Boolean = False
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayDTRManualNo"}))
                Generic.ClearControls(Me, "pnlPopup")

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPayDTRManual_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopup", dt)
                Next

                'Enable or Disable Controls
                If txtIsPosted.Checked = True Then
                    IsEnabled = False
                Else
                    IsEnabled = True
                End If
                Generic.EnableControls(Me, "pnlPopup", IsEnabled)
                btnSave.Enabled = IsEnabled

                mdlShow.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayDTRManualNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EPayDTRManual", UserNo, item)
            i = i + 1
        Next

        If i > 0 Then
            PopulateGrid(True)
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        Dim RetVal As Boolean = False
        'EmployeeNo = Generic.ToInt(cboEmployeeNo.SelectedValue)
        Dim PayDTRManualNo As Integer = Generic.ToInt(Me.txtPayDTRManualNo.Text)
        Dim WorkingCutOff As Double = Generic.ToDec(Me.txtWorkingCutOff.Text)
        Dim WorkingHrs As Double = Generic.ToDec(Me.txtWorkingHrs.Text)
        Dim NP As Double = Generic.ToDec(Me.txtNP.Text)
        Dim AbsHrs As Double = Generic.ToDec(Me.txtAbsHrs.Text)
        Dim Late As Double = Generic.ToDec(Me.txtLate.Text)
        Dim Under As Double = Generic.ToDec(Me.txtUnder.Text)

        Dim VL As Double = Generic.ToDec(Me.txtVL.Text)
        Dim SL As Double = Generic.ToDec(Me.txtSL.Text)
        Dim OB As Double = Generic.ToDec(Me.txtOB.Text)
        Dim OL As Double = Generic.ToDec(Me.txtOL.Text)
        Dim EL As Double = Generic.ToDec(Me.txtEL.Text)
        Dim ML As Double = Generic.ToDec(Me.txtML.Text)
        Dim PTL As Double = Generic.ToDec(Me.txtPTL.Text)
        Dim BD As Double = Generic.ToDec(Me.txtBD.Text)
        Dim FL As Double = Generic.ToDec(Me.txtFL.Text)
        Dim PL As Double = Generic.ToDec(Me.txtPL.Text)
        Dim SPL As Double = Generic.ToDec(Me.txtSPL.Text)
        Dim BL As Double = Generic.ToDec(Me.txtBL.Text)
        Dim SHRDCount As Double = Generic.ToDec(Me.txtSHRDCount.Text)
        Dim LHRDCount As Double = Generic.ToDec(Me.txtLHRDCount.Text)

        Dim Ovt As Double = Generic.ToDec(Me.txtOvt.Text)
        Dim Ovt8 As Double = Generic.ToDec(Me.txtOvt8.Text)
        Dim OvtNP As Double = Generic.ToDec(Me.txtOvtNP.Text)
        Dim Ovt8NP As Double = Generic.ToDec(Me.txtOvt8NP.Text)
        Dim RDOvt As Double = Generic.ToDec(Me.txtRDOvt.Text)
        Dim RDOvt8 As Double = Generic.ToDec(Me.txtRDOvt8.Text)
        Dim RDOvtNP As Double = Generic.ToDec(Me.txtRDOvtNP.Text)
        Dim RDOvt8NP As Double = Generic.ToDec(Me.txtRDOvt8NP.Text)

        Dim RHNROvt As Double = Generic.ToDec(Me.txtRHNROvt.Text)
        Dim RHNROvt8 As Double = Generic.ToDec(Me.txtRHNROvt8.Text)
        Dim RHNROvtNP As Double = Generic.ToDec(Me.txtRHNROvtNP.Text)
        Dim RHNROvt8NP As Double = Generic.ToDec(Me.txtRHNROvt8NP.Text)
        Dim RHRDOvt As Double = Generic.ToDec(Me.txtRHRDOvt.Text)
        Dim RHRDOvt8 As Double = Generic.ToDec(Me.txtRHRDOvt8.Text)
        Dim RHRDOvtNP As Double = Generic.ToDec(Me.txtRHRDOvtNP.Text)
        Dim RHRDOvt8NP As Double = Generic.ToDec(Me.txtRHRDOvt8NP.Text)

        Dim SHNROvt As Double = Generic.ToDec(Me.txtSHNROvt.Text)
        Dim SHNROvt8 As Double = Generic.ToDec(Me.txtSHNROvt8.Text)
        Dim SHNROvtNP As Double = Generic.ToDec(Me.txtSHNROvtNP.Text)
        Dim SHNROvt8NP As Double = Generic.ToDec(Me.txtSHNROvt8NP.Text)
        Dim SHRDOvt As Double = Generic.ToDec(Me.txtSHRDOvt.Text)
        Dim SHRDOvt8 As Double = Generic.ToDec(Me.txtSHRDOvt8.Text)
        Dim SHRDOvtNP As Double = Generic.ToDec(Me.txtSHRDOvtNP.Text)
        Dim SHRDOvt8NP As Double = Generic.ToDec(Me.txtSHRDOvt8NP.Text)

        Dim ROvt As Double = Generic.ToDec(Me.txtROvt.Text)
        Dim ROvt8 As Double = Generic.ToDec(Me.txtROvt8.Text)
        Dim ROvtNP As Double = Generic.ToDec(Me.txtROvtNP.Text)
        Dim ROvt8NP As Double = Generic.ToDec(Me.txtROvt8NP.Text)

        Dim StartDate As String = Generic.ToStr(Me.txtStartDate.Text)
        Dim EndDate As String = Generic.ToStr(Me.txtEndDate.Text)


        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EDate_WebValidate", UserNo, StartDate, EndDate)

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

        If SQLHelper.ExecuteNonQuery("EPayDTRManual_WebSave", UserNo, PayDTRManualNo, PayLastDetiNo, Session("PayLastList_PayNo"), Session("PayLastList_EmployeeNo"), WorkingCutOff, WorkingHrs, NP, AbsHrs, Late, Under, _
                                        VL, SL, OB, OL, EL, ML, PTL, BD, FL, PL, SPL, BL, SHRDCount, LHRDCount, _
                                        Ovt, Ovt8, OvtNP, Ovt8NP, RDOvt, RDOvt8, RDOvtNP, RDOvt8NP, _
                                        RHNROvt, RHNROvt8, RHNROvtNP, RHNROvt8NP, RHRDOvt, RHRDOvt8, RHRDOvtNP, RHRDOvt8NP, _
                                        SHNROvt, SHNROvt8, SHNROvtNP, SHNROvt8NP, SHRDOvt, SHRDOvt8, SHRDOvtNP, SHRDOvt8NP, _
                                        ROvt, ROvt8, ROvtNP, ROvt8NP, StartDate, EndDate) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If


    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Try
            Generic.ClearControls(Me, "pnlPopup")
            Generic.EnableControls(Me, "pnlPopup", True)
            mdlShow.Show()
        Catch ex As Exception

        End Try
    End Sub

End Class