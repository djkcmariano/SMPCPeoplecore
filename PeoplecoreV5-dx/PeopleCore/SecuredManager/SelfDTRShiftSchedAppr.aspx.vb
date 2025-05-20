Imports System.Data
Imports clsLib

Partial Class Secured_SelfDTRShiftSchedAppr
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim tstatus As Integer
    Dim dscount As Double = 0
    Dim _ds As New DataSet
    Dim _dt As New DataTable
    Dim xScript As String = ""
    Dim rowno As Integer = 0
    Dim transNo As Integer = 0

    Dim showFrm As New clsFormControls
    Dim clsGeneric As New clsGenericClass
    Dim ComponentNo As Integer = 2 'Managerial

    Private Function monthDesc(monthNo As Integer) As String
        Dim fmonth As String = ""
        Select Case monthNo
            Case 1
                fmonth = "January"
            Case 2
                fmonth = "February"
            Case 3
                fmonth = "March"
            Case 4
                fmonth = "April"
            Case 5
                fmonth = "May"
            Case 6
                fmonth = "June"
            Case 7
                fmonth = "July"
            Case 8
                fmonth = "August"
            Case 9
                fmonth = "September"
            Case 10
                fmonth = "October"
            Case 11
                fmonth = "November"
            Case 12
                fmonth = "December"
        End Select

        Return fmonth

    End Function
    Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")


        Try
            If ViewState("ApplicableMonth") Is Nothing Then
                ViewState("ApplicableMonth") = Now().Month
                cboApplicableMonth.Text = ViewState("ApplicableMonth")
            End If

            If ViewState("ApplicableYear") Is Nothing Then
                ViewState("ApplicableYear") = Now().Year
                cboApplicableYear.Text = ViewState("ApplicableYear")
            End If

            If ViewState("PageSize") Is Nothing Then
                ViewState("PageSize") = 10
                cboPageSize.Text = ViewState("PageSize")
            End If

            DataPager2.PageSize = Generic.ToInt(Me.cboPageSize.SelectedValue)
            Dim Month As Integer = Generic.ToInt(Me.cboApplicableMonth.SelectedValue)
            Dim Year As Integer = Generic.ToInt(Me.cboApplicableYear.SelectedValue)
            Dim DepartmentNo As Integer = Generic.ToInt(Me.cboDepartmentNo.SelectedValue)
            Dim SectionNo As Integer = Generic.ToInt(Me.cboSectionNo.SelectedValue)
            Dim UnitNo As Integer = Generic.ToInt(Me.cboUnitNo.SelectedValue)
            Dim PositionNo As Integer = Generic.ToInt(Me.cboPositionNo.SelectedValue)
            Dim W1 As Boolean = Generic.ToBol(Me.txtWeek1.Checked)
            Dim W2 As Boolean = Generic.ToBol(Me.txtWeek2.Checked)
            Dim W3 As Boolean = Generic.ToBol(Me.txtWeek3.Checked)
            Dim W4 As Boolean = Generic.ToBol(Me.txtWeek4.Checked)
            Dim W5 As Boolean = Generic.ToBol(Me.txtWeek5.Checked)

            lblRemark.Text = monthDesc(Month) & ", " & Year.ToString

            _ds = SQLHelper.ExecuteDataSet("EDTRShift_WebCalendarSelf", xPublicVar.xOnlineUseNo, Filter1.SearchText.ToString, Month, Year, DepartmentNo, PositionNo, SectionNo, UnitNo, W1, W2, W3, W4, W5, Session("xPayLocNo"))
            _dt = _ds.Tables(0)

            Dim dv As New Data.DataView(_dt)
            If SortExp <> "" Then
                ViewState(xScript & "SortExp") = SortExp
            End If
            If sordir <> "" Then

                ViewState(xScript & "sortdir") = sordir
            End If

            If _ds.Tables.Count > 0 Then
                If _ds.Tables(0).Rows.Count > 0 Then
                    dscount = _ds.Tables(0).Rows.Count
                    If ViewState(xScript & "SortExp") <> "" Then
                        dv.Sort = ViewState(xScript & "SortExp") + ViewState(xScript & "sortdir")
                    End If
                End If
            End If

            If IsMain Then
                ViewState(xScript & "PageNo") = 0
                ViewState(Left(xScript, Len(xScript) - 5)) = 0
            End If
            Me.grdMain.PageIndex = ViewState(xScript & "PageNo")
            Me.grdMain.DataSource = dv
            Me.grdMain.DataBind()

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub txtFilter_Changed(sender As Object, e As System.EventArgs)
        PopulateGrid()
    End Sub
    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMain.PageIndexChanging
        ViewState(xScript & "PageNo") = e.NewPageIndex
        ViewState(Left(xScript, Len(xScript) - 5)) = 0
        PopulateGrid()
    End Sub
    Private Sub PopulateCombo()

        Try
            cboApplicableMonth.DataSource = SQLHelper.ExecuteDataSet("ECalendarMonth_WebLookup", xPublicVar.xOnlineUseNo, Session("xPayLocNo"))
            cboApplicableMonth.DataValueField = "tNo"
            cboApplicableMonth.DataTextField = "tDesc"
            cboApplicableMonth.DataBind()

        Catch ex As Exception

        End Try

        Try
            cboApplicableYear.DataSource = SQLHelper.ExecuteDataSet("ECalendarYear_WebLookup", xPublicVar.xOnlineUseNo, Session("xPayLocNo"))
            cboApplicableYear.DataValueField = "tNo"
            cboApplicableYear.DataTextField = "tDesc"
            cboApplicableYear.DataBind()

        Catch ex As Exception

        End Try


        Try
            cboPageSize.DataSource = SQLHelper.ExecuteDataSet("ECalendarPageSize_WebLookup", xPublicVar.xOnlineUseNo, Session("xPayLocNo"))
            cboPageSize.DataValueField = "tNo"
            cboPageSize.DataTextField = "tDesc"
            cboPageSize.DataBind()

        Catch ex As Exception

        End Try

        Try
            cboDepartmentNo.DataSource = clsGeneric.xLookup_Table(xPublicVar.xOnlineUseNo, "EDepartment", Session("xpayLocNo"))
            cboDepartmentNo.DataValueField = "tNo"
            cboDepartmentNo.DataTextField = "tDesc"
            cboDepartmentNo.DataBind()

        Catch ex As Exception

        End Try
        Try
            cboPositionNo.DataSource = clsGeneric.xLookup_Table(xPublicVar.xOnlineUseNo, "EPosition", Session("xpayLocNo"))
            cboPositionNo.DataValueField = "tNo"
            cboPositionNo.DataTextField = "tDesc"
            cboPositionNo.DataBind()

        Catch ex As Exception

        End Try

        Try
            cboSectionNo.DataSource = clsGeneric.xLookup_Table(xPublicVar.xOnlineUseNo, "ESection", Session("xpayLocNo"))
            cboSectionNo.DataValueField = "tNo"
            cboSectionNo.DataTextField = "tDesc"
            cboSectionNo.DataBind()

        Catch ex As Exception

        End Try

        Try
            cboUnitNo.DataSource = clsGeneric.xLookup_Table(xPublicVar.xOnlineUseNo, "EUnit", Session("xpayLocNo"))
            cboUnitNo.DataValueField = "tNo"
            cboUnitNo.DataTextField = "tDesc"
            cboUnitNo.DataBind()

        Catch ex As Exception

        End Try

        Try
            Me.cboShiftNo.DataSource = SQLHelper.ExecuteDataSet("EShift_WebLookup_Sched", xPublicVar.xOnlineUseNo, 0, ComponentNo, Session("xpayLocNo"))
            Me.cboShiftNo.DataTextField = "tdesc"
            Me.cboShiftNo.DataValueField = "tno"
            Me.cboShiftNo.DataBind()
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ViewState(xScript & "PageNo") = 0
            ViewState(Left(xScript, Len(xScript) - 5)) = 0
            PopulateGrid()
        Catch ex As Exception
        End Try

    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("OnlineUserNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Permission.IsAuthenticatedSuperior()

        'xScript = Request.ServerVariables("SCRIPT_NAME")
        'xScript = Generic.GetPath(xScript)
        xScript = "DTRShiftSched"
        If Not IsPostBack Then
            PopulateCombo()
            PopulateGrid()
        End If

        AddHandler Filter1.lnkSearchClick, AddressOf lnkGo_Click
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Protected Sub lnkEditDayOff_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Generic.ClearControls(Me, "pnlPopupDayOff")
            Dim lnk As New LinkButton
            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            Dim employeeNo As String = ""
            Dim fullName As String = ""
            employeeNo = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
            fullName = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
            showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupDayOff, Session("xPayLocNo"))
            txtDayOffEmployeeNo.Text = employeeNo
            txtDayOffFullName.Text = fullName
            txtDayOffDateFrom.Text = Generic.Split(lnk.CommandArgument, 0)
            txtDayOffDateTo.Text = Generic.Split(lnk.CommandArgument, 1)
            Dim DayOff1 As Integer = Generic.ToInt(Generic.Split(lnk.CommandArgument, 2))
            If DayOff1 > 0 Then
                cboDayOffNo.Text = DayOff1
            End If

            Dim DayOff2 As Integer = Generic.ToInt(Generic.Split(lnk.CommandArgument, 3))
            If DayOff2 > 0 Then
                cboDayOffNo2.Text = DayOff2
            End If

            'Dim DayOff3 As Integer = Generic.ToInt(Generic.Split(lnk.CommandArgument, 4))
            'If DayOff3 > 0 Then
            '    cboDayOffNo3.Text = DayOff3
            'End If
            txtIsSixDays.Checked = Generic.Split(lnk.CommandArgument, 5)

            mdlDayOff.Show()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSaveDayOff_Click(sender As Object, e As System.EventArgs)
        Try
            Dim RetVal As Boolean = False
            Dim EmployeeNo As Integer = Generic.ToInt(txtDayOffEmployeeNo.Text)
            Dim DayOffNo As Integer = Generic.ToInt(cboDayOffNo.SelectedValue)
            Dim DayOffNo2 As Integer = Generic.ToInt(cboDayOffNo2.SelectedValue)
            Dim DayOffNo3 As Integer = Generic.ToInt(cboDayOffNo3.SelectedValue)
            Dim DateFrom As String = Generic.ToStr(txtDayOffDateFrom.Text)
            Dim DateTo As String = Generic.ToStr(txtDayOffDateTo.Text)
            Dim Reason As String = Generic.ToStr(txtDayOffReason.Text)
            Dim IsSixDays As Boolean = Generic.ToBol(txtIsSixDays.Checked)
            Dim ApprovalStatNo As Integer = 2
            Dim ComponentNo As Integer = 5 'Managerial For Scheduling

            '//validate start here
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EDTRDayOff_WebValidate", xPublicVar.xOnlineUseNo, 0, EmployeeNo, DateFrom, DateTo, DayOffNo, DayOffNo2, DayOffNo3, ApprovalStatNo, Session("xPayLocNo"), ComponentNo)

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("tProceed"))
                messagedialog = Generic.ToStr(rowx("xMessage"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                mdlDayOff.Show()
                Exit Sub
            End If

            If SQLHelper.ExecuteNonQuery("EDTRDayOff_WebSave", xPublicVar.xOnlineUseNo, 0, EmployeeNo, DateFrom, DateTo, DayOffNo, DayOffNo2, DayOffNo3, Reason, ApprovalStatNo, Session("xPayLocNo")) > 0 Then
                RetVal = True
            Else
                RetVal = False
            End If

            If RetVal = True Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkUpdateRD_Click(sender As Object, e As System.EventArgs)
        Dim ds As New DataSet

        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, IsCheck As New CheckBox, t As Integer = 0, i As Integer = 0, xMessage As String = ""
        Dim txtIsSelect1 As New CheckBox, txtIsSelect2 As New CheckBox, txtIsSelect3 As New CheckBox, txtIsSelect4 As New CheckBox, txtIsSelect5 As New CheckBox, txtIsSelect6 As New CheckBox, txtIsSelect7 As New CheckBox, txtIsSelect8 As New CheckBox, txtIsSelect9 As New CheckBox, txtIsSelect10 As New CheckBox
        Dim txtIsSelect11 As New CheckBox, txtIsSelect12 As New CheckBox, txtIsSelect13 As New CheckBox, txtIsSelect14 As New CheckBox, txtIsSelect15 As New CheckBox, txtIsSelect16 As New CheckBox, txtIsSelect17 As New CheckBox, txtIsSelect18 As New CheckBox, txtIsSelect19 As New CheckBox, txtIsSelect20 As New CheckBox
        Dim txtIsSelect21 As New CheckBox, txtIsSelect22 As New CheckBox, txtIsSelect23 As New CheckBox, txtIsSelect24 As New CheckBox, txtIsSelect25 As New CheckBox, txtIsSelect26 As New CheckBox, txtIsSelect27 As New CheckBox, txtIsSelect28 As New CheckBox, txtIsSelect29 As New CheckBox, txtIsSelect30 As New CheckBox
        Dim txtIsSelect31 As New CheckBox, txtIsSelect32 As New CheckBox, txtIsSelect33 As New CheckBox, txtIsSelect34 As New CheckBox, txtIsSelect35 As New CheckBox

        For tcount = 0 To Me.grdMain.Rows.Count - 1
            lbl = CType(grdMain.Rows(tcount).FindControl("lblEmployeeNo"), Label)
            tcheck = CType(grdMain.Rows(tcount).FindControl("chkIsSelect"), CheckBox)
            If tcheck.Checked = True And CType(lbl.Text, Integer) > 0 Then
                txtIsSelect1 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit1"), CheckBox)
                txtIsSelect2 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit2"), CheckBox)
                txtIsSelect3 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit3"), CheckBox)
                txtIsSelect4 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit4"), CheckBox)
                txtIsSelect5 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit5"), CheckBox)
                txtIsSelect6 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit6"), CheckBox)
                txtIsSelect7 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit7"), CheckBox)
                txtIsSelect8 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit8"), CheckBox)
                txtIsSelect9 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit9"), CheckBox)
                txtIsSelect10 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit10"), CheckBox)
                txtIsSelect11 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit11"), CheckBox)
                txtIsSelect12 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit12"), CheckBox)
                txtIsSelect13 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit13"), CheckBox)
                txtIsSelect14 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit14"), CheckBox)
                txtIsSelect15 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit15"), CheckBox)
                txtIsSelect16 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit16"), CheckBox)
                txtIsSelect17 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit17"), CheckBox)
                txtIsSelect18 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit18"), CheckBox)
                txtIsSelect19 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit19"), CheckBox)
                txtIsSelect20 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit20"), CheckBox)
                txtIsSelect21 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit21"), CheckBox)
                txtIsSelect22 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit22"), CheckBox)
                txtIsSelect23 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit23"), CheckBox)
                txtIsSelect24 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit24"), CheckBox)
                txtIsSelect25 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit25"), CheckBox)
                txtIsSelect26 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit26"), CheckBox)
                txtIsSelect27 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit27"), CheckBox)
                txtIsSelect28 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit28"), CheckBox)
                txtIsSelect29 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit29"), CheckBox)
                txtIsSelect30 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit30"), CheckBox)
                txtIsSelect31 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit31"), CheckBox)
                txtIsSelect32 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit32"), CheckBox)
                txtIsSelect33 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit33"), CheckBox)
                txtIsSelect34 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit34"), CheckBox)
                txtIsSelect35 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit35"), CheckBox)
                ds = SQLHelper.ExecuteDataSet("dbo.EDTRDayOff_WebSave_Batch", xPublicVar.xOnlineUseNo, CType(lbl.Text, Integer), "", Generic.CheckDBNull(Me.cboApplicableMonth.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Me.cboApplicableYear.Text, clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ViewState(xScript & "startdate"), clsBase.clsBaseLibrary.enumObjectType.StrType), Generic.CheckDBNull(ViewState(xScript & "EndDate"), clsBase.clsBaseLibrary.enumObjectType.StrType), txtIsSelect1.Checked, txtIsSelect2.Checked, txtIsSelect3.Checked, txtIsSelect4.Checked, txtIsSelect5.Checked, txtIsSelect6.Checked, txtIsSelect7.Checked, txtIsSelect8.Checked, txtIsSelect9.Checked, txtIsSelect10.Checked, txtIsSelect11.Checked, txtIsSelect12.Checked, txtIsSelect13.Checked, txtIsSelect14.Checked, txtIsSelect15.Checked, txtIsSelect16.Checked, txtIsSelect17.Checked, txtIsSelect18.Checked, txtIsSelect19.Checked, txtIsSelect20.Checked, txtIsSelect21.Checked, txtIsSelect22.Checked, txtIsSelect23.Checked, txtIsSelect24.Checked, txtIsSelect25.Checked, txtIsSelect26.Checked, txtIsSelect27.Checked, txtIsSelect28.Checked, txtIsSelect29.Checked, txtIsSelect30.Checked, txtIsSelect31.Checked, txtIsSelect32.Checked, txtIsSelect33.Checked, txtIsSelect34.Checked, txtIsSelect35.Checked, 1, Session("xPayLocNo"))

                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        i = Generic.CheckDBNull(ds.Tables(0).Rows(0)("tProceed"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                        xMessage = Generic.CheckDBNull(ds.Tables(0).Rows(0)("xMessage"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                    End If
                End If

                If i > 0 Then
                    MessageBox.Information(xMessage, Me)
                    Exit Sub
                Else
                    t = t + 1
                End If

            End If
        Next

        If t > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Information("No Selected Employee.", Me)
        End If

    End Sub
    Protected Sub lnkEditShift_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            IsCrew() ' CHECKING OF CREW

            Dim lnk As New LinkButton
            Dim employeeNo As String = "", shiftno As String = "", tDate As String = ""
            Dim fullName As String = ""
            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            employeeNo = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
            fullName = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
            showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupDetl, Session("xPayLocNo"))
            tcdate.Visible = True
            tcName.Visible = True
            shiftno = lnk.TabIndex
            tDate = lnk.ToolTip
            txtEmployeeNo.Text = employeeNo
            txtFullName.Text = fullName
            txtDateFrom.Text = tDate
            txtDateTo.Text = tDate
            Try
                Me.cboShiftNo.DataSource = SQLHelper.ExecuteDataSet("EShift_WebLookup_Sched", xPublicVar.xOnlineUseNo, shiftno, ComponentNo, Generic.ToInt(Session("xpayLocNo")))
                Me.cboShiftNo.DataTextField = "tdesc"
                Me.cboShiftNo.DataValueField = "tno"
                Me.cboShiftNo.DataBind()
            Catch ex As Exception

            End Try
            Try
                cboCostCenterNo.DataSource = SQLHelper.ExecuteDataSet("EDTRShift_WebLookup_ChargeTo", xPublicVar.xOnlineUseNo, Generic.ToInt(Session("xpayLocNo")))
                cboCostCenterNo.DataTextField = "tDesc"
                cboCostCenterNo.DataValueField = "tno"
                cboCostCenterNo.DataBind()
            Catch ex As Exception

            End Try
            cboShiftNo.Text = shiftno
            mdlDetl.Show()
        Catch ex As Exception

        End Try
    End Sub
    Private Function IsCrew() As Boolean
        Dim retVal As Boolean = False
        Dim ds As DataSet = SQLHelper.ExecuteDataSet("Select IsCrew From dbo.EEmployee A Inner Join (Select PayclassNo,IsCrew From dbo.EPayClass) B On a.payclassNo=B.PayclassNo Where A.EmployeeNo=" & Generic.ToInt(txtEmployeeNo.Text))
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                retVal = Generic.ToBol(ds.Tables(0).Rows(0)("IsCrew"))
            End If
        End If
        If retVal Then
            'divShift.Visible = False
            'cboShiftNo.CssClass = "form-control"
            'divTime.Visible = True
            'txtIn1.CssClass = "form-control required"
            'txtOut1.CssClass = "form-control required"
        Else
            'divShift.Visible = True
            'cboShiftNo.CssClass = "form-control required"
            'divTime.Visible = False
            'txtIn1.CssClass = "form-control"
            'txtOut1.CssClass = "form-control"
        End If
        If retVal Then
            fRegisterStartupScript("JSDialogResponse", "ViewCrew('True');")
            txtIn1.CssClass = "form-control required"
            txtOut1.CssClass = "form-control required"
            cboShiftNo.CssClass = "form-control"
        Else
            fRegisterStartupScript("JSDialogResponse", "ViewCrew('False');")
            txtIn1.CssClass = "form-control"
            txtOut1.CssClass = "form-control"
            cboShiftNo.CssClass = "form-control required"
        End If
        Return retVal
    End Function
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
    Protected Sub lnkUpdateShift_Click(sender As Object, e As System.EventArgs)
        Try
            Dim lnk As New LinkButton
            Dim employeeNo As String = "", shiftno As Integer = 0, tDate As String = ""
            Dim fullName As String = ""
            lnk = sender
            showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupDetl, Session("xPayLocNo"))
            Try
                Me.cboShiftNo.DataSource = SQLHelper.ExecuteDataSet("EShift_WebLookup_Sched", xPublicVar.xOnlineUseNo, 0, ComponentNo, Session("xpayLocNo"))
                Me.cboShiftNo.DataTextField = "tdesc"
                Me.cboShiftNo.DataValueField = "tno"
                Me.cboShiftNo.DataBind()
            Catch ex As Exception

            End Try
            tcdate.Visible = False
            'cboShiftNo.Text = 0
            txtEmployeeNo.Text = 0
            tcName.Visible = False
            mdlDetl.Show()
        Catch ex As Exception

        End Try
    End Sub
    'Submit record
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtEmployeeNo.Text = 0 Then
            If SaveRecord_Batch() = 1 Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                PopulateGrid()
            Else
                MessageBox.Information("No selected employee.", Me)
            End If
        Else
            Dim RetVal As Boolean = False
            Dim costcenterno As Integer = Generic.ToInt(Me.cboCostCenterNo.SelectedValue)
            Dim employeeno As Integer = Generic.ToInt(Me.txtEmployeeNo.Text)
            Dim IsEnabledDayOff As Boolean = False  'Generic.CheckDBNull(Me.txtIsEnableDayoff.Checked, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
            Dim relieverno As Integer = 0 'Generic.CheckDBNull(Me.cboReliever.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
            Dim ShiftNo As Integer = Generic.ToInt(cboShiftNo.SelectedValue)
            Dim ShiftNoMon As Integer = Generic.ToInt(cboShiftNo.SelectedValue)
            Dim ShiftNoTue As Integer = Generic.ToInt(cboShiftNo.SelectedValue)
            Dim ShiftNoWed As Integer = Generic.ToInt(cboShiftNo.SelectedValue)
            Dim ShiftNoThu As Integer = Generic.ToInt(cboShiftNo.SelectedValue)
            Dim ShiftNoFri As Integer = Generic.ToInt(cboShiftNo.SelectedValue)
            Dim ShiftNoSat As Integer = Generic.ToInt(cboShiftNo.SelectedValue)
            Dim ShiftNoSun As Integer = Generic.ToInt(cboShiftNo.SelectedValue)
            Dim DateFrom As String = Generic.ToStr(txtDateFrom.Text)
            Dim DateTo As String = Generic.ToStr(txtDateTo.Text)
            Dim Reason As String = Generic.ToStr(txtReason.Text)
            Dim ApprovalStatNo As Integer = 2
            Dim ComponentNo As Integer = 5 'Managerial For Scheduling

            '//validate start here
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EDTRShift_WebValidate", xPublicVar.xOnlineUseNo, 0, employeeno, DateFrom, DateTo, ShiftNo, ApprovalStatNo, Session("xPayLocNo"), ComponentNo)

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("tProceed"))
                messagedialog = Generic.ToStr(rowx("xMessage"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                mdlDetl.Show()
                Exit Sub
            End If

            If SQLHelper.ExecuteNonQuery("EDTRShift_WebSave", xPublicVar.xOnlineUseNo, 0, employeeno, DateFrom, DateTo, ShiftNo, ShiftNoMon, ShiftNoTue, ShiftNoWed, ShiftNoThu, ShiftNoFri, ShiftNoSat, ShiftNoSun, Reason, ApprovalStatNo, Session("xPayLocNo"), Generic.ToStr(txtIn1.Text), Generic.ToStr(txtOut1.Text), costcenterno) > 0 Then
                RetVal = True
            Else
                RetVal = False
            End If

            If RetVal = True Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If

        End If

    End Sub
    Private Function SaveRecord_Batch() As Integer
        Dim ds As New DataSet
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, IsCheck As New CheckBox, t As Integer = 0
        Dim txtIsSelect1 As New CheckBox, txtIsSelect2 As New CheckBox, txtIsSelect3 As New CheckBox, txtIsSelect4 As New CheckBox, txtIsSelect5 As New CheckBox, txtIsSelect6 As New CheckBox, txtIsSelect7 As New CheckBox, txtIsSelect8 As New CheckBox, txtIsSelect9 As New CheckBox, txtIsSelect10 As New CheckBox
        Dim txtIsSelect11 As New CheckBox, txtIsSelect12 As New CheckBox, txtIsSelect13 As New CheckBox, txtIsSelect14 As New CheckBox, txtIsSelect15 As New CheckBox, txtIsSelect16 As New CheckBox, txtIsSelect17 As New CheckBox, txtIsSelect18 As New CheckBox, txtIsSelect19 As New CheckBox, txtIsSelect20 As New CheckBox
        Dim txtIsSelect21 As New CheckBox, txtIsSelect22 As New CheckBox, txtIsSelect23 As New CheckBox, txtIsSelect24 As New CheckBox, txtIsSelect25 As New CheckBox, txtIsSelect26 As New CheckBox, txtIsSelect27 As New CheckBox, txtIsSelect28 As New CheckBox, txtIsSelect29 As New CheckBox, txtIsSelect30 As New CheckBox
        Dim txtIsSelect31 As New CheckBox, txtIsSelect32 As New CheckBox, txtIsSelect33 As New CheckBox, txtIsSelect34 As New CheckBox, txtIsSelect35 As New CheckBox

        For tcount = 0 To Me.grdMain.Rows.Count - 1
            lbl = CType(grdMain.Rows(tcount).FindControl("lblEmployeeNo"), Label)
            tcheck = CType(grdMain.Rows(tcount).FindControl("chkIsSelect"), CheckBox)
            If tcheck.Checked = True And CType(lbl.Text, Integer) > 0 Then
                txtIsSelect1 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit1"), CheckBox)
                txtIsSelect2 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit2"), CheckBox)
                txtIsSelect3 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit3"), CheckBox)
                txtIsSelect4 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit4"), CheckBox)
                txtIsSelect5 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit5"), CheckBox)
                txtIsSelect6 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit6"), CheckBox)
                txtIsSelect7 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit7"), CheckBox)
                txtIsSelect8 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit8"), CheckBox)
                txtIsSelect9 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit9"), CheckBox)
                txtIsSelect10 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit10"), CheckBox)
                txtIsSelect11 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit11"), CheckBox)
                txtIsSelect12 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit12"), CheckBox)
                txtIsSelect13 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit13"), CheckBox)
                txtIsSelect14 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit14"), CheckBox)
                txtIsSelect15 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit15"), CheckBox)
                txtIsSelect16 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit16"), CheckBox)
                txtIsSelect17 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit17"), CheckBox)
                txtIsSelect18 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit18"), CheckBox)
                txtIsSelect19 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit19"), CheckBox)
                txtIsSelect20 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit20"), CheckBox)
                txtIsSelect21 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit21"), CheckBox)
                txtIsSelect22 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit22"), CheckBox)
                txtIsSelect23 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit23"), CheckBox)
                txtIsSelect24 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit24"), CheckBox)
                txtIsSelect25 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit25"), CheckBox)
                txtIsSelect26 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit26"), CheckBox)
                txtIsSelect27 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit27"), CheckBox)
                txtIsSelect28 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit28"), CheckBox)
                txtIsSelect29 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit29"), CheckBox)
                txtIsSelect30 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit30"), CheckBox)
                txtIsSelect31 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit31"), CheckBox)
                txtIsSelect32 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit32"), CheckBox)
                txtIsSelect33 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit33"), CheckBox)
                txtIsSelect34 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit34"), CheckBox)
                txtIsSelect35 = CType(grdMain.Rows(tcount).FindControl("txtIsEdit35"), CheckBox)

                ds = SQLHelper.ExecuteDataSet("EDTRShift_WebSave_Batch", xPublicVar.xOnlineUseNo, CType(lbl.Text, Integer), Generic.CheckDBNull(Me.cboShiftNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType), "", Generic.CheckDBNull(Me.cboApplicableMonth.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Me.cboApplicableYear.Text, clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ViewState(xScript & "startdate"), clsBase.clsBaseLibrary.enumObjectType.StrType), Generic.CheckDBNull(ViewState(xScript & "endDate"), clsBase.clsBaseLibrary.enumObjectType.StrType), txtIsSelect1.Checked, txtIsSelect2.Checked, txtIsSelect3.Checked, txtIsSelect4.Checked, txtIsSelect5.Checked, txtIsSelect6.Checked, txtIsSelect7.Checked, txtIsSelect8.Checked, txtIsSelect9.Checked, txtIsSelect10.Checked, txtIsSelect11.Checked, txtIsSelect12.Checked, txtIsSelect13.Checked, txtIsSelect14.Checked, txtIsSelect15.Checked, txtIsSelect16.Checked, txtIsSelect17.Checked, txtIsSelect18.Checked, txtIsSelect19.Checked, txtIsSelect20.Checked, txtIsSelect21.Checked, txtIsSelect22.Checked, txtIsSelect23.Checked, txtIsSelect24.Checked, txtIsSelect25.Checked, txtIsSelect26.Checked, txtIsSelect27.Checked, txtIsSelect28.Checked, txtIsSelect29.Checked, txtIsSelect30.Checked, txtIsSelect31.Checked, txtIsSelect32.Checked, txtIsSelect33.Checked, txtIsSelect34.Checked, txtIsSelect35.Checked, 1, Generic.CheckDBNull(txtIsSixDays.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType), Session("xPayLocNo"))
                t = t + 1
            End If
        Next

        If t > 0 Then
            SaveRecord_Batch = 1
            PopulateGrid()
        Else
            SaveRecord_Batch = 0
        End If

    End Function
    Protected Sub grdMain_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMain.RowDataBound
        Dim ds As New DataSet
        Dim lblDayCode1 As New Label, lblDayCode2 As New Label, lblDayCode3 As New Label, lblDayCode4 As New Label, lblDayCode5 As New Label, lblDayCode6 As New Label, lblDayCode7 As New Label, lblDayCode8 As New Label, lblDayCode9 As New Label, lblDayCode10 As New Label
        Dim lblDayCode11 As New Label, lblDayCode12 As New Label, lblDayCode13 As New Label, lblDayCode14 As New Label, lblDayCode15 As New Label, lblDayCode16 As New Label, lblDayCode17 As New Label, lblDayCode18 As New Label, lblDayCode19 As New Label, lblDayCode20 As New Label
        Dim lblDayCode21 As New Label, lblDayCode22 As New Label, lblDayCode23 As New Label, lblDayCode24 As New Label, lblDayCode25 As New Label, lblDayCode26 As New Label, lblDayCode27 As New Label, lblDayCode28 As New Label, lblDayCode29 As New Label, lblDayCode30 As New Label
        Dim lblDayCode31 As New Label, lblDayCode32 As New Label, lblDayCode33 As New Label, lblDayCode34 As New Label, lblDayCode35 As New Label

        Dim lblDate1 As New Label, lblDate2 As New Label, lblDate3 As New Label, lblDate4 As New Label, lblDate5 As New Label, lblDate6 As New Label, lblDate7 As New Label, lblDate8 As New Label, lblDate9 As New Label, lblDate10 As New Label
        Dim lblDate11 As New Label, lblDate12 As New Label, lblDate13 As New Label, lblDate14 As New Label, lblDate15 As New Label, lblDate16 As New Label, lblDate17 As New Label, lblDate18 As New Label, lblDate19 As New Label, lblDate20 As New Label
        Dim lblDate21 As New Label, lblDate22 As New Label, lblDate23 As New Label, lblDate24 As New Label, lblDate25 As New Label, lblDate26 As New Label, lblDate27 As New Label, lblDate28 As New Label, lblDate29 As New Label, lblDate30 As New Label
        Dim lblDate31 As New Label, lblDate32 As New Label, lblDate33 As New Label, lblDate34 As New Label, lblDate35 As New Label

        Dim txtIsSelect1 As New CheckBox
        Dim txtIsSelect2 As New CheckBox
        Dim txtIsSelect3 As New CheckBox
        Dim txtIsSelect4 As New CheckBox
        Dim txtIsSelect5 As New CheckBox
        Dim txtIsSelect6 As New CheckBox
        Dim txtIsSelect7 As New CheckBox
        Dim txtIsSelect8 As New CheckBox
        Dim txtIsSelect9 As New CheckBox
        Dim txtIsSelect10 As New CheckBox
        Dim txtIsSelect11 As New CheckBox
        Dim txtIsSelect12 As New CheckBox
        Dim txtIsSelect13 As New CheckBox
        Dim txtIsSelect14 As New CheckBox
        Dim txtIsSelect15 As New CheckBox
        Dim txtIsSelect16 As New CheckBox
        Dim txtIsSelect17 As New CheckBox
        Dim txtIsSelect18 As New CheckBox
        Dim txtIsSelect19 As New CheckBox
        Dim txtIsSelect20 As New CheckBox
        Dim txtIsSelect21 As New CheckBox
        Dim txtIsSelect22 As New CheckBox
        Dim txtIsSelect23 As New CheckBox
        Dim txtIsSelect24 As New CheckBox
        Dim txtIsSelect25 As New CheckBox
        Dim txtIsSelect26 As New CheckBox
        Dim txtIsSelect27 As New CheckBox
        Dim txtIsSelect28 As New CheckBox
        Dim txtIsSelect29 As New CheckBox
        Dim txtIsSelect30 As New CheckBox
        Dim txtIsSelect31 As New CheckBox
        Dim txtIsSelect32 As New CheckBox
        Dim txtIsSelect33 As New CheckBox
        Dim txtIsSelect34 As New CheckBox
        Dim txtIsSelect35 As New CheckBox

        If e.Row.RowType = DataControlRowType.Header Then

            lblDate1 = CType(e.Row.FindControl("lblhd1"), Label)
            lblDate2 = CType(e.Row.FindControl("lblhd2"), Label)
            lblDate3 = CType(e.Row.FindControl("lblhd3"), Label)
            lblDate4 = CType(e.Row.FindControl("lblhd4"), Label)
            lblDate5 = CType(e.Row.FindControl("lblhd5"), Label)
            lblDate6 = CType(e.Row.FindControl("lblhd6"), Label)
            lblDate7 = CType(e.Row.FindControl("lblhd7"), Label)
            lblDate8 = CType(e.Row.FindControl("lblhd8"), Label)
            lblDate9 = CType(e.Row.FindControl("lblhd9"), Label)
            lblDate10 = CType(e.Row.FindControl("lblhd10"), Label)
            lblDate11 = CType(e.Row.FindControl("lblhd11"), Label)
            lblDate12 = CType(e.Row.FindControl("lblhd12"), Label)
            lblDate13 = CType(e.Row.FindControl("lblhd13"), Label)
            lblDate14 = CType(e.Row.FindControl("lblhd14"), Label)
            lblDate15 = CType(e.Row.FindControl("lblhd15"), Label)
            lblDate16 = CType(e.Row.FindControl("lblhd16"), Label)
            lblDate17 = CType(e.Row.FindControl("lblhd17"), Label)
            lblDate18 = CType(e.Row.FindControl("lblhd18"), Label)
            lblDate19 = CType(e.Row.FindControl("lblhd19"), Label)
            lblDate20 = CType(e.Row.FindControl("lblhd20"), Label)
            lblDate21 = CType(e.Row.FindControl("lblhd21"), Label)
            lblDate22 = CType(e.Row.FindControl("lblhd22"), Label)
            lblDate23 = CType(e.Row.FindControl("lblhd23"), Label)
            lblDate24 = CType(e.Row.FindControl("lblhd24"), Label)
            lblDate25 = CType(e.Row.FindControl("lblhd25"), Label)
            lblDate26 = CType(e.Row.FindControl("lblhd26"), Label)
            lblDate27 = CType(e.Row.FindControl("lblhd27"), Label)
            lblDate28 = CType(e.Row.FindControl("lblhd28"), Label)
            lblDate29 = CType(e.Row.FindControl("lblhd29"), Label)
            lblDate30 = CType(e.Row.FindControl("lblhd30"), Label)
            lblDate31 = CType(e.Row.FindControl("lblhd31"), Label)
            lblDate32 = CType(e.Row.FindControl("lblhd32"), Label)
            lblDate33 = CType(e.Row.FindControl("lblhd33"), Label)
            lblDate34 = CType(e.Row.FindControl("lblhd34"), Label)
            lblDate35 = CType(e.Row.FindControl("lblhd35"), Label)

            lblDayCode1 = CType(e.Row.FindControl("lblDayCode1"), Label)
            lblDayCode2 = CType(e.Row.FindControl("lblDayCode2"), Label)
            lblDayCode3 = CType(e.Row.FindControl("lblDayCode3"), Label)
            lblDayCode4 = CType(e.Row.FindControl("lblDayCode4"), Label)
            lblDayCode5 = CType(e.Row.FindControl("lblDayCode5"), Label)
            lblDayCode6 = CType(e.Row.FindControl("lblDayCode6"), Label)
            lblDayCode7 = CType(e.Row.FindControl("lblDayCode7"), Label)
            lblDayCode8 = CType(e.Row.FindControl("lblDayCode8"), Label)
            lblDayCode9 = CType(e.Row.FindControl("lblDayCode9"), Label)
            lblDayCode10 = CType(e.Row.FindControl("lblDayCode10"), Label)
            lblDayCode11 = CType(e.Row.FindControl("lblDayCode11"), Label)
            lblDayCode12 = CType(e.Row.FindControl("lblDayCode12"), Label)
            lblDayCode13 = CType(e.Row.FindControl("lblDayCode13"), Label)
            lblDayCode14 = CType(e.Row.FindControl("lblDayCode14"), Label)
            lblDayCode15 = CType(e.Row.FindControl("lblDayCode15"), Label)
            lblDayCode16 = CType(e.Row.FindControl("lblDayCode16"), Label)
            lblDayCode17 = CType(e.Row.FindControl("lblDayCode17"), Label)
            lblDayCode18 = CType(e.Row.FindControl("lblDayCode18"), Label)
            lblDayCode19 = CType(e.Row.FindControl("lblDayCode19"), Label)
            lblDayCode20 = CType(e.Row.FindControl("lblDayCode20"), Label)
            lblDayCode21 = CType(e.Row.FindControl("lblDayCode21"), Label)
            lblDayCode22 = CType(e.Row.FindControl("lblDayCode22"), Label)
            lblDayCode23 = CType(e.Row.FindControl("lblDayCode23"), Label)
            lblDayCode24 = CType(e.Row.FindControl("lblDayCode24"), Label)
            lblDayCode25 = CType(e.Row.FindControl("lblDayCode25"), Label)
            lblDayCode26 = CType(e.Row.FindControl("lblDayCode26"), Label)
            lblDayCode27 = CType(e.Row.FindControl("lblDayCode27"), Label)
            lblDayCode28 = CType(e.Row.FindControl("lblDayCode28"), Label)
            lblDayCode29 = CType(e.Row.FindControl("lblDayCode29"), Label)
            lblDayCode30 = CType(e.Row.FindControl("lblDayCode30"), Label)
            lblDayCode31 = CType(e.Row.FindControl("lblDayCode31"), Label)
            lblDayCode32 = CType(e.Row.FindControl("lblDayCode32"), Label)
            lblDayCode33 = CType(e.Row.FindControl("lblDayCode33"), Label)
            lblDayCode34 = CType(e.Row.FindControl("lblDayCode34"), Label)
            lblDayCode35 = CType(e.Row.FindControl("lblDayCode35"), Label)

            txtIsSelect1 = CType(e.Row.FindControl("txtIsSelect1"), CheckBox)
            txtIsSelect2 = CType(e.Row.FindControl("txtIsSelect2"), CheckBox)
            txtIsSelect3 = CType(e.Row.FindControl("txtIsSelect3"), CheckBox)
            txtIsSelect4 = CType(e.Row.FindControl("txtIsSelect4"), CheckBox)
            txtIsSelect5 = CType(e.Row.FindControl("txtIsSelect5"), CheckBox)
            txtIsSelect6 = CType(e.Row.FindControl("txtIsSelect6"), CheckBox)
            txtIsSelect7 = CType(e.Row.FindControl("txtIsSelect7"), CheckBox)
            txtIsSelect8 = CType(e.Row.FindControl("txtIsSelect8"), CheckBox)
            txtIsSelect9 = CType(e.Row.FindControl("txtIsSelect9"), CheckBox)
            txtIsSelect10 = CType(e.Row.FindControl("txtIsSelect10"), CheckBox)
            txtIsSelect11 = CType(e.Row.FindControl("txtIsSelect11"), CheckBox)
            txtIsSelect12 = CType(e.Row.FindControl("txtIsSelect12"), CheckBox)
            txtIsSelect13 = CType(e.Row.FindControl("txtIsSelect13"), CheckBox)
            txtIsSelect14 = CType(e.Row.FindControl("txtIsSelect14"), CheckBox)
            txtIsSelect15 = CType(e.Row.FindControl("txtIsSelect15"), CheckBox)
            txtIsSelect16 = CType(e.Row.FindControl("txtIsSelect16"), CheckBox)
            txtIsSelect17 = CType(e.Row.FindControl("txtIsSelect17"), CheckBox)
            txtIsSelect18 = CType(e.Row.FindControl("txtIsSelect18"), CheckBox)
            txtIsSelect19 = CType(e.Row.FindControl("txtIsSelect19"), CheckBox)
            txtIsSelect20 = CType(e.Row.FindControl("txtIsSelect20"), CheckBox)
            txtIsSelect21 = CType(e.Row.FindControl("txtIsSelect21"), CheckBox)
            txtIsSelect22 = CType(e.Row.FindControl("txtIsSelect22"), CheckBox)
            txtIsSelect23 = CType(e.Row.FindControl("txtIsSelect23"), CheckBox)
            txtIsSelect24 = CType(e.Row.FindControl("txtIsSelect24"), CheckBox)
            txtIsSelect25 = CType(e.Row.FindControl("txtIsSelect25"), CheckBox)
            txtIsSelect26 = CType(e.Row.FindControl("txtIsSelect26"), CheckBox)
            txtIsSelect27 = CType(e.Row.FindControl("txtIsSelect27"), CheckBox)
            txtIsSelect28 = CType(e.Row.FindControl("txtIsSelect28"), CheckBox)
            txtIsSelect29 = CType(e.Row.FindControl("txtIsSelect29"), CheckBox)
            txtIsSelect30 = CType(e.Row.FindControl("txtIsSelect30"), CheckBox)
            txtIsSelect31 = CType(e.Row.FindControl("txtIsSelect31"), CheckBox)
            txtIsSelect32 = CType(e.Row.FindControl("txtIsSelect32"), CheckBox)
            txtIsSelect33 = CType(e.Row.FindControl("txtIsSelect33"), CheckBox)
            txtIsSelect34 = CType(e.Row.FindControl("txtIsSelect34"), CheckBox)
            txtIsSelect35 = CType(e.Row.FindControl("txtIsSelect35"), CheckBox)

        End If


        ds = _ds ' SQLHelper.ExecuteDataset( "EDTRCalendar_WebList", Generic.CheckDBNull(Session("Month"), clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Session("Year"), clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Session("txtcFromDate"), clsBase.clsBaseLibrary.enumObjectType.StrType), Generic.CheckDBNull(Session("txtcToDate"), clsBase.clsBaseLibrary.enumObjectType.StrType))

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then

                lblDate1.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate1"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate2.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate2"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate3.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate3"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate4.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate4"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate5.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate5"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate6.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate6"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate7.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate7"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate8.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate8"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate9.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate9"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate10.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate10"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate11.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate11"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate12.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate12"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate13.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate13"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate14.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate14"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate15.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate15"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate16.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate16"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate17.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate17"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate18.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate18"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate19.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate19"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate20.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate20"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate21.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate21"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate22.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate22"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate23.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate23"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate24.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate24"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate25.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate25"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate26.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate26"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate27.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate27"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate28.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate28"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate29.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate29"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate30.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate30"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate31.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate31"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate32.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate32"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate33.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate33"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate34.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate34"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDate35.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("lblDate35"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)

                lblDayCode1.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode1"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode2.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode2"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode3.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode3"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode4.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode4"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode5.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode5"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode6.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode6"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode7.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode7"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode8.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode8"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode9.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode9"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode10.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode10"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode11.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode11"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode12.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode12"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode13.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode13"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode14.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode14"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode15.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode15"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode16.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode16"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode17.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode17"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode18.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode18"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode19.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode19"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode20.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode20"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode21.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode21"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode22.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode22"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode23.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode23"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode24.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode24"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode25.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode25"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode26.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode26"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode27.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode27"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode28.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode28"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode29.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode29"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode30.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode30"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode31.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode31"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode32.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode32"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode33.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode33"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode34.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode34"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDayCode35.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)("DayCode35"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)

                txtIsSelect1.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled1"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect2.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled2"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect3.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled3"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect4.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled4"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect5.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled5"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect6.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled6"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect7.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled7"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect8.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled8"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect9.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled9"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect10.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled10"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect11.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled11"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect12.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled12"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect13.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled13"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect14.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled14"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect15.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled15"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect16.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled16"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect17.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled17"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect18.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled18"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect19.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled19"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect20.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled20"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect21.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled21"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect22.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled22"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect23.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled23"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect24.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled24"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect25.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled25"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect26.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled26"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect27.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled27"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect28.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled28"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect29.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled29"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect30.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled30"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect31.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled31"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect32.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled32"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect33.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled33"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect34.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled34"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect35.Enabled = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsEnabled35"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)


                txtIsSelect1.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible1"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect2.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible2"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect3.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible3"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect4.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible4"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect5.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible5"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect6.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible6"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect7.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible7"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect8.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible8"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect9.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible9"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect10.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible10"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect11.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible11"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect12.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible12"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect13.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible13"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect14.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible14"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect15.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible15"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect16.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible16"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect17.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible17"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect18.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible18"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect19.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible19"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect20.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible20"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect21.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible21"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect22.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible22"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect23.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible23"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect24.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible24"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect25.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible25"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect26.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible26"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect27.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible27"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect28.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible28"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect29.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible29"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect30.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible30"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect31.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible31"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect32.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible32"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect33.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible33"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect34.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible34"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                txtIsSelect35.Visible = Generic.CheckDBNull(ds.Tables(0).Rows(0)("IsVisible35"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
            End If
        End If

        Dim row As GridViewRow = e.Row
        ' Intitialize TableCell list
        Dim columns As New List(Of TableCell)()
        For Each column As DataControlField In grdMain.Columns
            'Get the first Cell /Column
            Dim cell As TableCell = row.Cells(0)
            ' Then Remove it after
            row.Cells.Remove(cell)
            'And Add it to the List Collections
            columns.Add(cell)
        Next

        ' Add cells
        row.Cells.AddRange(columns.ToArray())

        If txtIsSelect1.Visible = False Then : e.Row.Cells(5).Visible = False : End If
        If txtIsSelect2.Visible = False Then : e.Row.Cells(8).Visible = False : End If
        If txtIsSelect3.Visible = False Then : e.Row.Cells(11).Visible = False : End If
        If txtIsSelect4.Visible = False Then : e.Row.Cells(14).Visible = False : End If
        If txtIsSelect5.Visible = False Then : e.Row.Cells(17).Visible = False : End If
        If txtIsSelect6.Visible = False Then : e.Row.Cells(20).Visible = False : End If
        If txtIsSelect7.Visible = False Then : e.Row.Cells(23).Visible = False : End If
        If txtIsSelect8.Visible = False Then : e.Row.Cells(26).Visible = False : End If
        If txtIsSelect9.Visible = False Then : e.Row.Cells(29).Visible = False : End If
        If txtIsSelect10.Visible = False Then : e.Row.Cells(32).Visible = False : End If
        If txtIsSelect11.Visible = False Then : e.Row.Cells(35).Visible = False : End If
        If txtIsSelect12.Visible = False Then : e.Row.Cells(38).Visible = False : End If
        If txtIsSelect13.Visible = False Then : e.Row.Cells(41).Visible = False : End If
        If txtIsSelect14.Visible = False Then : e.Row.Cells(44).Visible = False : End If
        If txtIsSelect15.Visible = False Then : e.Row.Cells(47).Visible = False : End If
        If txtIsSelect16.Visible = False Then : e.Row.Cells(50).Visible = False : End If
        If txtIsSelect17.Visible = False Then : e.Row.Cells(53).Visible = False : End If
        If txtIsSelect18.Visible = False Then : e.Row.Cells(56).Visible = False : End If
        If txtIsSelect19.Visible = False Then : e.Row.Cells(59).Visible = False : End If
        If txtIsSelect20.Visible = False Then : e.Row.Cells(62).Visible = False : End If
        If txtIsSelect21.Visible = False Then : e.Row.Cells(65).Visible = False : End If
        If txtIsSelect22.Visible = False Then : e.Row.Cells(68).Visible = False : End If
        If txtIsSelect23.Visible = False Then : e.Row.Cells(71).Visible = False : End If
        If txtIsSelect24.Visible = False Then : e.Row.Cells(74).Visible = False : End If
        If txtIsSelect25.Visible = False Then : e.Row.Cells(77).Visible = False : End If
        If txtIsSelect26.Visible = False Then : e.Row.Cells(80).Visible = False : End If
        If txtIsSelect27.Visible = False Then : e.Row.Cells(83).Visible = False : End If
        If txtIsSelect28.Visible = False Then : e.Row.Cells(86).Visible = False : End If
        If txtIsSelect29.Visible = False Then : e.Row.Cells(89).Visible = False : End If
        If txtIsSelect30.Visible = False Then : e.Row.Cells(92).Visible = False : End If
        If txtIsSelect31.Visible = False Then : e.Row.Cells(95).Visible = False : End If
        If txtIsSelect32.Visible = False Then : e.Row.Cells(98).Visible = False : End If
        If txtIsSelect33.Visible = False Then : e.Row.Cells(101).Visible = False : End If
        If txtIsSelect34.Visible = False Then : e.Row.Cells(104).Visible = False : End If
        If txtIsSelect35.Visible = False Then : e.Row.Cells(107).Visible = False : End If

    End Sub


End Class

