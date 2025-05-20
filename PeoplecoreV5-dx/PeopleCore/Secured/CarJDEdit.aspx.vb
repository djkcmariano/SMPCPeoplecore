Imports clsLib
Imports System.Data

Partial Class Secured_CarJDEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
        Dim RetVal As Boolean = False
        Dim JDNo As Integer = 0
        Dim PositionNo As Integer = Generic.ToInt(Me.cboPositionNo.SelectedValue)
        Dim JDCode As String = Generic.ToStr(Me.txtJDCode.Text)
        Dim DepartmentNo As Integer = Generic.ToInt(Me.cboDepartmentNo.SelectedValue)
        Dim DivisionNo As Integer = Generic.ToInt(Me.cboDivisionNo.SelectedValue)
        Dim GroupNo As Integer = Generic.ToInt(Me.cboGroupNo.SelectedValue)
        Dim SectionNo As Integer = Generic.ToInt(Me.cboSectionNo.SelectedValue)
        Dim UnitNo As Integer = Generic.ToInt(Me.cboUnitNo.SelectedValue)
        Dim TaskNo As Integer = Generic.ToInt(Me.cboTaskNo.SelectedValue)
        Dim FacilityNo As Integer = Generic.ToInt(Me.cboFacilityNo.SelectedValue)
        Dim JobGradeNo As Integer = Generic.ToInt(Me.cboJobGradeNo.SelectedValue)
        Dim IsArchived As Integer = Generic.ToInt(Me.chkIsArchived.Checked)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EJD_WebValidate", UserNo, TransNo, JDCode, _
                                   PositionNo, DepartmentNo, DivisionNo, GroupNo, SectionNo, UnitNo, TaskNo, FacilityNo, JobGradeNo, Me.txtDateReviewed.Text, PayLocNo, chkIsArchived.Checked)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If



        Dim dt As DataTable, error_num As Integer = 0, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("EJD_WebSave", UserNo, TransNo, JDCode, _
                                   PositionNo, DepartmentNo, DivisionNo, GroupNo, SectionNo, UnitNo, TaskNo, FacilityNo, JobGradeNo, txtSummary.Html, "", _
                                   txtWcondition.Html, txtDutiesAndResponsibilities.Html, txtOtherAttributes.Html, "", "", _
                                   Me.txtReportingTo.Text, Me.txtCoordinate.Text.Trim, Me.txtSupervises.Text.Trim, "", _
                                   Me.txtDateReviewed.Text, Session("xPayLocNo"), txtJobMandate.Html, 0, 0, 0, chkIsArchived.Checked,
                                   chkIsReportingTo.Checked, chkIsSupervises.Checked, chkIsCoordinatesWith.Checked, chkIsJobMandate.Checked, _
                                   chkIsJobSummary.Checked, 0, chkIsWorkingCondition.Checked, chkIsKeyResponsibilities.Checked, _
                                   chkIsAttributes.Checked, chkIsEduc.Checked, chkIsExpe.Checked, chkIsElig.Checked, chkIsTrn.Checked, txtEffectiveDate.Text, txtRemarks.Text, _
                                   0, 0, "", 0, 0, "", 0, 0, "")

        For Each row As DataRow In dt.Rows
            RetVal = True
            error_num = Generic.ToInt(row("Error_num"))
            JDNo = Generic.ToInt(row("JDNo"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                RetVal = False
            End If

        Next
        If RetVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            JDNo = 0
        End If

        If JDNo > 0 Then
            If Generic.ToInt(Request.QueryString("id")) = 0 Then
                Dim url As String = "carjdedit.aspx?id=" & JDNo
                If IsArchived > 0 Then
                    MessageBox.Success("Record has been successfully archived.", Me)
                Else
                    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                End If

            Else
                If IsArchived > 0 Then
                    MessageBox.Success("Record has been successfully archived.", Me)
                Else
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                End If
                ViewState("IsEnabled") = False
                EnabledControls()
            End If
            PopulateTabHeader()
        End If
       

        'Else
        'MessageBox.Information(MessageTemplate.DeniedAdd, Me)
        'End If


    End Sub
    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "CarJDList.aspx", "EJD") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", Enabled)
        txtJDNo.Enabled = False

        btnModify.Visible = Not Enabled
        btnSave.Visible = Enabled
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "CarJDList.aspx")


        If Not IsPostBack Then
            PopulateTabHeader()
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateData()
            Comparevalidator2.ValueToCompare = DateTime.Now.ToShortDateString()
        End If

        EnabledControls()
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EJD_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next
    End Sub

    

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EJDTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
    End Sub

End Class
