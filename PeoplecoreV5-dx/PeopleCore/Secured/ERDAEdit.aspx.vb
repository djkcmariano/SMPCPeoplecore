Imports System.Data
Imports Microsoft.VisualBasic
Imports clsLib

Partial Class Secured_ERDAEdit
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    'Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    Private Sub PopulateData()

        Dim DAPolicyTypeNo As Integer = 0
        Dim DAPolicyNo As Integer = 0
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDA_WebOne", xPublicVar.xOnlineUseNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateDropDownList_Union(xPublicVar.xOnlineUseNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))
            DAPolicyTypeNo = Generic.ToInt(row("DAPolicyTypeNo"))
            DAPolicyNo = Generic.ToInt(row("DAPolicyNo"))
        Next

        PopulateDAAR(TransNo)
        PopulateDAPolicyType(DAPolicyTypeNo, DAPolicyNo)
        PopulateDAPolicy(DAPolicyNo)
        Generic.PopulateData(Me, "Panel1", dt)

    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))

        Generic.EnableControls(Me, "Panel1", Enabled)
        Generic.PopulateDataDisabled(Me, "Panel1", xPublicVar.xOnlineUseNo, PayLocNo, Generic.ToStr(Session("xMenuType")))

        If TransNo = 0 Then
            If Generic.ToInt(cboDAPolicyTypeNo.SelectedValue) > 0 Then
                cboDAPolicyNo.Enabled = Enabled
            Else
                cboDAPolicyNo.Enabled = False
            End If

            If Generic.ToInt(cboDAPolicyNo.SelectedValue) > 0 Then
                cboDACaseTypeNo.Enabled = Enabled
                cboOffenseCount.Enabled = Enabled
            Else
                cboDACaseTypeNo.Enabled = False
                cboOffenseCount.Enabled = False
            End If
        End If

        chkIsDismissal.Enabled = False
        chkIsSuspension.Enabled = False

        If chkIsSuspension.Checked = True Then
            txtNoOfDays.Enabled = Enabled
        Else
            txtNoOfDays.Enabled = False
            txtNoOfDays.Text = ""
        End If

        lnkModify.Visible = Not Enabled
        lnkSubmit.Visible = Enabled

        'cboReceivedByNo.Enabled = False
        'txtReceivedDate.Enabled = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xPublicVar.xOnlineUseNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(xPublicVar.xOnlineUseNo, "ERDAList.aspx")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(xPublicVar.xOnlineUseNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            Generic.PopulateDropDownList(xPublicVar.xOnlineUseNo, Me, "pnlPopup", Generic.ToInt(Session("xPayLocNo")))
            PopulateData()
            PopulateTabHeader()
        End If

        EnabledControls()

    End Sub


    'Populate Combo box
    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDA_WebTabHeader", xPublicVar.xOnlineUseNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(row("EmployeeNo")) & "&tIndex=2"
            Next

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkModify.Click

        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit, "ERDAList.aspx") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
        End If

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim RetVal As Boolean = False
        Dim ReceivedDate As String = Generic.ToStr(Me.txtReceivedDate.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim GenderNo As Integer = Generic.ToInt(cboGenderNo.SelectedValue)
        Dim BirthAge As Integer = Generic.ToInt(txtBirthAge.Text)
        Dim FacilityNo As Integer = Generic.ToInt(Me.cboFacilityNo.SelectedValue)
        Dim DepartmentNo As Integer = Generic.ToInt(Me.cboDepartmentNo.SelectedValue)
        Dim DivisionNo As Integer = Generic.ToInt(Me.cboDivisionNo.SelectedValue)
        Dim GroupNo As Integer = Generic.ToInt(Me.cboGroupNo.SelectedValue)
        Dim SectionNo As Integer = Generic.ToInt(Me.cboSectionNo.SelectedValue)
        Dim UnitNo As Integer = Generic.ToInt(Me.cboUnitNo.SelectedValue)
        Dim PositionNo As Integer = Generic.ToInt(Me.cboPositionNo.SelectedValue)
        Dim LineLeader As Integer = Generic.ToInt(cboLineLeaderNo.SelectedValue)
        Dim ImmediateSuperiorNo As Integer = Generic.ToInt(cboImmediateSuperiorNo.SelectedValue)
        Dim DACaseTypeNo As Integer = Generic.ToInt(Me.cboDACaseTypeNo.SelectedValue)
        Dim DAPolicyTypeNo As Integer = Generic.ToInt(Me.cboDAPolicyTypeNo.SelectedValue)
        Dim ViolationDate As String = Generic.ToStr(Me.txtViolationDate.Text)
        Dim DAPolicyNo As Integer = Generic.ToInt(Me.cboDAPolicyNo.SelectedValue)
        Dim OffenseCount As Integer = Generic.ToInt(Me.cboOffenseCount.SelectedValue)
        Dim DATypeNo As Integer = Generic.ToInt(Me.cboDATypeNo.SelectedValue)
        Dim IsDismissal As Boolean = Generic.ToBol(Me.chkIsDismissal.Checked)
        Dim IsSuspension As Boolean = Generic.ToBol(Me.chkIsSuspension.Checked)
        Dim NoOfDays As Integer = Generic.ToInt(Me.txtNoOfDays.Text)
        Dim StartDate As String = Generic.ToStr(Me.txtStartDate.Text)
        Dim EndDate As String = Generic.ToStr(Me.txtEndDate.Text)
        Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)
        Dim IsApproved As Boolean = Generic.ToBol(Me.chkIsApproved.Checked)
        Dim ApprovedByNo As Integer = Generic.ToInt(Me.hifApprovedByNo.Value)
        Dim ApprovedDate As String = Generic.ToStr(Me.txtApprovedDate.Text)
        Dim IsServed As Boolean = Generic.ToBol(Me.chkIsServed.Checked)
        Dim ServedDate As String = Generic.ToStr(Me.txtServedDate.Text)
        Dim NReleasedDate As String = Generic.ToStr(Me.txtNReleasedDate.Text)
        Dim NReceivedDate As String = Generic.ToStr(Me.txtNReceivedDate.Text)
        Dim daarno As Integer = Generic.ToInt(cboDAARDetlNo.SelectedValue)

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EDA_WebValidate", xPublicVar.xOnlineUseNo, TransNo, ReceivedDate, daarno, EmployeeNo, DepartmentNo, DivisionNo, GroupNo, SectionNo, UnitNo, PositionNo, FacilityNo, LineLeader, ImmediateSuperiorNo, BirthAge, GenderNo,
                                     DACaseTypeNo, DAPolicyTypeNo, ViolationDate, DAPolicyNo, OffenseCount, DATypeNo, IsDismissal, IsSuspension, NoOfDays, StartDate, EndDate, Remarks, IsApproved, ApprovedByNo, ApprovedDate, IsServed, ServedDate, NReleasedDate, NReceivedDate, txtDACode.Text, _
                                   txtNoticeDate.Text, txtNoticeRecvDate.Text, txtReqExtFromDate.Text, txtReqExtToDate.Text, txtReqExtGrantFromDate.Text, txtReqExtGrantToDate.Text, "", txtAnswerRecvDate.Text, txtEvalDate.Text, txtDecisionDate.Text, txtDecisionReleaseDate.Text, txtDecisionRecvDate.Text, txtReconsiderDate.Text, Session("xPayLocNo"))

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDA_WebSave", xPublicVar.xOnlineUseNo, TransNo, ReceivedDate, daarno, EmployeeNo, DepartmentNo, DivisionNo, GroupNo, SectionNo, UnitNo, PositionNo, FacilityNo, LineLeader, ImmediateSuperiorNo, BirthAge, GenderNo,
                                     DACaseTypeNo, DAPolicyTypeNo, ViolationDate, DAPolicyNo, OffenseCount, DATypeNo, IsDismissal, IsSuspension, NoOfDays, StartDate, EndDate, Remarks, IsApproved, ApprovedByNo, ApprovedDate, IsServed, ServedDate, NReleasedDate, NReceivedDate, txtDACode.Text, _
                                   txtNoticeDate.Text, txtNoticeRecvDate.Text, txtReqExtFromDate.Text, txtReqExtToDate.Text, txtReqExtGrantFromDate.Text, txtReqExtGrantToDate.Text, "", txtAnswerRecvDate.Text, txtEvalDate.Text, txtDecisionDate.Text, txtDecisionReleaseDate.Text, txtDecisionRecvDate.Text, txtReconsiderDate.Text, Session("xPayLocNo"), _
                                   txtReceivedDate2.Text, txtRemarks2.Text, Generic.ToInt(cboProjectNo.SelectedValue))
        For Each row As DataRow In dt.Rows
            TransNo = Generic.ToInt(row("Retval"))
            RetVal = True
        Next

        If RetVal = True Then
            Dim url As String = "ERDAList.aspx?id=" & TransNo
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If


    End Sub

    Protected Sub lnkInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As New LinkButton
            Dim i As String = ""
            lnk = sender

            If Generic.ToInt(hifEmployeeNo.Value) > 0 Then
                If TransNo > 0 Then
                    Dim dt As DataTable
                    dt = SQLHelper.ExecuteDataTable("EDA_WebOne", xPublicVar.xOnlineUseNo, TransNo)
                    Generic.PopulateData(Me, "pnlPopup", dt)
                End If
                Generic.EnableControls(Me, "pnlPopup", False)
                mdlShow.Show()
            Else
                MessageBox.Information("No selected employee.", Me)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboDAARNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            'To Populate Data from Case Monitoring
            PopulateSeleted_DAAR(Generic.ToInt(Me.cboDAARDetlNo.SelectedValue))

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboDAPolicyTypeNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            'To Populate Dropdown of DA Policy
            PopulateDAPolicyType(Generic.ToInt(cboDAPolicyTypeNo.SelectedValue), 0)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboDAPolicyNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            'To Populate Data of Case Type 
            PopulateDAPolicy(Generic.ToInt(cboDAPolicyNo.SelectedValue))
            'To Populate Data of Last No. of Offense of Employee
            PopulateOffenseCount(Generic.ToInt(hifEmployeeNo.Value), Generic.ToInt(cboDAPolicyNo.SelectedValue))
            'To Populate Penalty 
            populateOffenseType(Generic.ToInt(cboDAPolicyNo.SelectedValue), Generic.ToInt(cboOffenseCount.Text), 0)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtOffenseCount_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            'To Populate Penalty
            populateOffenseType(Generic.ToInt(cboDAPolicyNo.SelectedValue), Generic.ToInt(cboOffenseCount.Text), 0)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub txtDATypeNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            'To Populate Penalty
            populateOffenseType(0, 0, Generic.ToInt(cboDATypeNo.SelectedValue))

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateSeleted_DAAR(ByVal tDAARNo As Integer)

        Dim EmployeeNo As Integer = 0
        Dim FullName As String = ""
        Dim ViolationDate As String = ""
        Dim DAPolicyTypeNo As Integer = 0
        Dim DAPolicyNo As Integer = 0
        Dim DACaseTypeNo As Integer = 0
        Dim dt As DataTable

        'Clear Data
        hifEmployeeNo.Value = 0
        txtFullName.Text = ""
        txtViolationDate.Text = ""
        cboDAPolicyTypeNo.Text = ""
        cboDAPolicyNo.Text = ""
        cboDACaseTypeNo.Text = ""
        txtNoOfDays.Text = ""
        cboOffenseCount.Text = ""
        cboDATypeNo.Text = ""
        chkIsDismissal.Checked = False
        chkIsSuspension.Checked = False
        txtNoOfDays.Text = ""
        txtStartDate.Text = ""
        txtEndDate.Text = ""

        If tDAARNo > 0 Then
            'Populate Data
            dt = SQLHelper.ExecuteDataTable("EDAAR_WebOne_DA", xPublicVar.xOnlineUseNo, tDAARNo)
            For Each row As DataRow In dt.Rows
                EmployeeNo = Generic.ToInt(row("EmployeeNo"))
                FullName = Generic.ToStr(row("FullName"))
                ViolationDate = Generic.ToStr(row("OccurenceDate"))
                DAPolicyTypeNo = Generic.ToInt(row("DAPolicyTypeNo"))
                DAPolicyNo = Generic.ToInt(row("DAPolicyNo"))
                DACaseTypeNo = Generic.ToInt(row("DACaseTypeNo"))
            Next
        End If

        'To Populate Data of Employee
        PopulateSeletedEmployee(EmployeeNo)
        'To Populate Dropdown of DA Policy
        PopulateDAPolicyType(DAPolicyTypeNo, DAPolicyNo)
        'To Populate Data of Case Type
        PopulateDAPolicy(DAPolicyNo)
        'To Populate Data of Last No. of Offense of Employee
        PopulateOffenseCount(EmployeeNo, DAPolicyNo)
        'To Populate Penalty 
        populateOffenseType(DAPolicyNo, Generic.ToInt(cboOffenseCount.Text), 0)

        txtFullName.Text = FullName
        txtViolationDate.Text = ViolationDate
        hifEmployeeNo.Value = EmployeeNo
        cboDAPolicyTypeNo.Text = DAPolicyTypeNo
        cboDAPolicyNo.Text = DAPolicyNo
        cboDACaseTypeNo.Text = DACaseTypeNo

    End Sub

    Private Sub PopulateSeletedEmployee(ByVal ID As Integer)

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployee_WebOne", xPublicVar.xOnlineUseNo, ID)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "pnlPopup", dt)
        Next

    End Sub

    Private Sub PopulateOffenseCount(ByVal EmpNo As Integer, ByVal DAPolicyNo As Integer)

        Dim OffenseCount As Integer = 0
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDA_WebOffenseCount", xPublicVar.xOnlineUseNo, EmpNo, DAPolicyNo, PayLocNo)
        For Each row As DataRow In dt.Rows
            Me.cboOffenseCount.Text = Generic.ToInt(row("OffenseCount"))
        Next

    End Sub

    Private Sub populateOffenseType(DAPolicyNo As Integer, Count As Integer, DATypeNo As Integer)

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDAPolicy_WebOne_OffenseCount", xPublicVar.xOnlineUseNo, DAPolicyNo, Count, DATypeNo)
        For Each row As DataRow In dt.Rows
            Me.cboDATypeNo.Text = Generic.ToInt(row("DATypeNo"))
            Me.chkIsDismissal.Checked = Generic.ToBol(row("IsDismissal"))
            Me.chkIsSuspension.Checked = Generic.ToBol(row("IsSuspension"))
            Me.txtNoOfDays.Text = Generic.ToInt(row("NoOfDay"))
        Next

        If chkIsSuspension.Checked = True Then
            txtNoOfDays.Enabled = True
        Else
            txtNoOfDays.Enabled = False
            txtNoOfDays.Text = ""
        End If

    End Sub

    Private Sub PopulateDAAR(ByVal DANo As Integer)
        Try
            cboDAARDetlNo.DataSource = SQLHelper.ExecuteDataSet("EDAAR_WebLookup_DA", xPublicVar.xOnlineUseNo, DANo, Session("xPayLocNo"))
            cboDAARDetlNo.DataTextField = "tDesc"
            cboDAARDetlNo.DataValueField = "tNo"
            cboDAARDetlNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDAPolicyType(ByVal DAPolicyTypeNo As Integer, ByVal DAPolicyNo As Integer)
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EDAPolicyType_WebLookup", xPublicVar.xOnlineUseNo, DAPolicyTypeNo, DAPolicyNo, PayLocNo)
            cboDAPolicyNo.DataSource = ds
            cboDAPolicyNo.DataTextField = "tdesc"
            cboDAPolicyNo.DataValueField = "tNo"
            cboDAPolicyNo.DataBind()
            ds = Nothing

            If DAPolicyTypeNo > 0 Then
                cboDAPolicyNo.Enabled = True
                cboDACaseTypeNo.Text = ""
            Else
                'To Populate Data of Case Type
                PopulateDAPolicy(0)
            End If


        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDAPolicy(ByVal DAPolicyNo As Integer)
        Try
            If DAPolicyNo > 0 Then
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EDAPolicy_WebOne", xPublicVar.xOnlineUseNo, DAPolicyNo)
                For Each row As DataRow In dt.Rows
                    cboDACaseTypeNo.Text = Generic.ToInt(row("DACaseTypeNo"))
                Next

                cboDAPolicyNo.Enabled = True
                cboDACaseTypeNo.Enabled = True
                cboOffenseCount.Enabled = True
            Else
                cboDAPolicyNo.Enabled = False
                cboDACaseTypeNo.Enabled = False
                cboDAPolicyNo.Text = ""
                cboDACaseTypeNo.Text = "0"
                cboOffenseCount.Text = "0"
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class




