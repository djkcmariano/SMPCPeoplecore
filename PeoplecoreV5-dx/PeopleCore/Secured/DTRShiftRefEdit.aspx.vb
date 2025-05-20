Imports clsLib
Imports System.Data
Partial Class Secured_PayLoanEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "DTRShiftRefList.aspx") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EShift_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next
    End Sub

    Private Sub PopulateCombo()
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        Try
            cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
            cboPayLocNo.DataTextField = "tdesc"
            cboPayLocNo.DataValueField = "tNo"
            cboPayLocNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "DTRShiftRefList.aspx")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateCombo()
            PopulateData()
            PopulateTabHeader()
        End If

        EnabledControls()

    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))

        Generic.EnableControls(Me, "Panel1", Enabled)
        If Enabled = True Then
            PopulateControls()
        End If

        btnModify.Visible = Not Enabled
        btnSave.Visible = Enabled
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EShift_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "DTRShiftRefList.aspx") Then
            Dim RetVal As Boolean = False
            Dim dt As DataTable
            Dim ShiftNo As Integer = Generic.ToInt(txtShiftNo.Text)
            Dim ShiftCode As String = Generic.ToStr(txtShiftCode.Text)
            Dim ShiftDesc As String = Generic.ToStr(txtShiftDesc.Text)
            Dim In1 As String = Generic.ToStr(txtIn1.Text)
            Dim Out1 As String = Generic.ToStr(txtOut1.Text)
            Dim In2 As String = Generic.ToStr(txtIn2.Text)
            Dim Out2 As String = Generic.ToStr(txtOut2.Text)
            Dim BreakHrs1 As Decimal = Generic.ToDec(Me.txtBreakHrs1.Text)
            Dim Hrs As Decimal = Generic.ToDec(Me.txtHrs.Text)
            Dim NoOfSwipe As Integer = Generic.ToInt(Me.cboNoOfSwipe.SelectedValue)
            Dim IsFlex As Boolean = Generic.ToBol(Me.txtIsFlex.Checked)
            Dim IsAdjustedFlex As Boolean = Generic.ToBol(Me.txtIsAdjustedFlex.Checked)
            Dim AdjustedHrs As Decimal = Generic.ToDec(Me.txtAdjustedHrs.Text)
            Dim IsNonPunching As Boolean = Generic.ToBol(Me.txtIsNonPunching.Checked)
            Dim IsDailyFlex As Boolean = Generic.ToBol(Me.txtIsDailyFlex.Checked)
            Dim IsCompress As Boolean = Generic.ToBol(Me.txtIsCompress.Checked)
            Dim OTStart As String = Generic.ToStr(txtOTStart.Text)
            Dim OTEnd As String = Generic.ToStr(txtOTEnd.Text)
            Dim OTAdj As Decimal = Generic.ToDec(Me.TxtOTAdj.Text)
            Dim IsGraveyard As Boolean = Generic.ToBol(txtIsGraveyard.Checked)
            Dim IsOTApply As Boolean = Generic.ToBol(txtIsOTApply.Checked)
            Dim BreakIn As String = Generic.ToStr(txtBreakIn.Text)
            Dim BreakOut As String = Generic.ToStr(txtBreakOut.Text)
            Dim IsFlexibreak As Boolean = Generic.ToBol(txtIsFlexiBreak.Checked)
            Dim IsAddLate As Boolean = Generic.ToBol(txtIsAddLate.Checked)
            Dim AddLate As Double = Generic.ToDec(txtAddLate.Text)
            Dim IsApplyToAll As Boolean = Generic.ToBol(txtIsApplyToAll.Checked)
            Dim IsSatUnder As Boolean = Generic.ToBol(txtIsSatUnder.Checked)
            Dim IsAdjustedFlexUnder As Boolean = Generic.ToBol(Me.txtIsAdjustedFlexUnder.Checked)
            Dim AdjustedHrsUnder As Decimal = Generic.ToDec(Me.txtAdjustedHrsUnder.Text)
            Dim IsWFH As Boolean = Generic.ToBol(chkIsWFH.Checked)
            Dim WorkHrsLimit As Decimal = Generic.ToDec(Me.txtWorkHrsLimit.Text)
            Dim IsArchived As Boolean = Generic.ToBol(Me.chkIsArchived.Checked)
            'Dim ReportHrs As Decimal = Generic.ToDec(Me.txtrephrs.Text)

            '//validate start here
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EShift_WebValidate", UserNo, PayLocNo, ShiftNo, ShiftCode, ShiftDesc, In1, Out1, In2, Out2, BreakHrs1, Hrs, NoOfSwipe, IsFlex, IsAdjustedFlex, AdjustedHrs, IsNonPunching, IsDailyFlex, IsCompress, OTStart, OTEnd, OTAdj, IsGraveyard, IsOTApply, BreakIn, BreakOut)

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("tProceed"))
                messagedialog = Generic.ToStr(rowx("xMessage"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If

            dt = SQLHelper.ExecuteDataTable("EShift_WebSave", UserNo, PayLocNo, ShiftNo, ShiftCode, ShiftDesc, In1, Out1, In2, Out2, BreakHrs1, Hrs, NoOfSwipe, IsFlex, IsAdjustedFlex, AdjustedHrs, IsNonPunching, IsDailyFlex, IsCompress, OTStart, OTEnd, OTAdj, IsGraveyard, IsOTApply, BreakIn, BreakOut, IsFlexibreak, IsAddLate, AddLate, IsApplyToAll, IsSatUnder, IsAdjustedFlexUnder, AdjustedHrsUnder, IsWFH, WorkHrsLimit, IsArchived)

            For Each row As DataRow In dt.Rows
                TransNo = Generic.ToInt(row("Retval"))
                RetVal = True
            Next


            If RetVal = True Then
                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim url As String = "DTRShiftRefEdit.aspx?id=" & TransNo
                    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                Else
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                    ViewState("IsEnabled") = False
                    PopulateData()
                    EnabledControls()
                End If
                'PopulateTabHeader()
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If

        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub cboNoOfSwipe_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateControls()
    End Sub

    Protected Sub txtIsAdjustedFlex_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateControls()
    End Sub

    Protected Sub PopulateControls()
        Try
            If Generic.ToInt(cboNoOfSwipe.SelectedValue) = 4 Then
                txtIn2.Enabled = True
                txtOut2.Enabled = True
                txtBreakIn.Enabled = False
                txtBreakOut.Enabled = False
                txtBreakIn.Text = ""
                txtBreakOut.Text = ""
                txtIsFlexiBreak.Enabled = True
                txtOut1.CssClass = "form-control"
                txtOut2.CssClass = "required form-control"
            Else
                txtIn2.Enabled = False
                txtOut2.Enabled = False
                txtIn2.Text = ""
                txtOut2.Text = ""
                txtBreakIn.Enabled = True
                txtBreakOut.Enabled = True
                txtIsFlexiBreak.Enabled = False
                txtOut1.CssClass = "required form-control"
                txtOut2.CssClass = "form-control"
            End If

            If txtIsAdjustedFlex.Checked = True Then
                txtAdjustedHrs.Enabled = True
            Else
                txtAdjustedHrs.Enabled = False
                txtAdjustedHrs.Text = ""
            End If

            If txtIsOTApply.Checked = True Then
                txtOTEnd.Enabled = True
            Else
                txtOTEnd.Enabled = False
                txtOTEnd.Text = ""
            End If

            If txtIsAddLate.Checked = True Then
                txtAddLate.Enabled = True
            Else
                txtAddLate.Enabled = False
                txtAddLate.Text = ""
            End If

        Catch ex As Exception

        End Try
    End Sub

End Class
