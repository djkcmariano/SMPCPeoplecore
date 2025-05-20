Imports clsLib
Imports System.Data

Partial Class Secured_AppWorkEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EApplicantExpe_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EApplicantExpe_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows                
                Generic.PopulateData(Me, "Panel1", dt)
                Dim IsWithOut As Boolean = Generic.ToBol(row("IsWithOut"))
                If IsWithOut = True Then
                    Generic.EnableControls(Me, "Panel1", False)
                    chkIsWithOut.Enabled = True
                Else
                    Generic.EnableControls(Me, "Panel1", True)
                End If

                If Generic.ToBol(row("IsPresent")) Then
                    txtToDate.CssClass = "form-control"
                    txtToDate.Enabled = False
                Else
                    If IsWithOut = False Then
                        txtToDate.CssClass = "form-control required"
                        txtToDate.Enabled = True
                    Else                        
                        txtToDate.Enabled = False
                        cboExpeTypeNo.Enabled = False
                    End If
                    
                End If

            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateCombo()
            PopulateTabHeader()
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicantTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=1"

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            PopulateControls()
            IswithOut()
            txtToDate.Enabled = True
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        'Dim ExpeTypeNo As Integer = 0
        'Dim NoOfYear As Double = 0
        'Dim CurrentSalary As Double = Generic.ToDec(Me.txtCurrentSalary.Text)
        'Dim EmployeeStatNo As Integer = Generic.ToInt(Me.cboEmployeeStatNo.SelectedValue)
        'Dim Accredited As Double = 0
        'Dim LWOP As Double = 0
        'Dim FromDay As Integer = Generic.ToInt(cboFromDay.SelectedValue)
        'Dim FromMonth As Integer = Generic.ToInt(cboFromMonth.SelectedValue)
        'Dim FromYear As Integer = Generic.ToInt(txtFromYear.Text)
        'Dim ToDay As Integer = Generic.ToInt(cboToDay.SelectedValue)
        'Dim ToMonth As Integer = Generic.ToInt(cboToMonth.SelectedValue)
        'Dim ToYear As Integer = Generic.ToInt(txtToYear.Text)
        'Dim IsOtherExpe As Integer = Generic.ToInt(txtIsOtherExpe.Checked)
        'Dim OtherExpe As String = Generic.ToStr(txtOtherExpe.Text)

        'Dim RetVal As Boolean = False
        'Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        'Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        'dtx = SQLHelper.ExecuteDataTable("EApplicantExpe_WebValidate", UserNo, Generic.ToInt(txtApplicantExpeNo.Text), TransNo, Me.txtExpeComp.Text.ToString, ExpeTypeNo, Me.txtPosition.Text, "", "", Me.txtRemarks.Text.ToString, NoOfYear, CurrentSalary, EmployeeStatNo, 0, Accredited, LWOP, Me.txtExpeCompAdd.Text.ToString, Me.txtCompPhone.Text.ToString, "", Me.txtDuties.Text.ToString, "", 0, txtIndustry.Text, FromDay, FromMonth, FromYear, ToDay, ToMonth, ToYear, IsOtherExpe, OtherExpe)

        'For Each rowx As DataRow In dtx.Rows
        '    invalid = Generic.ToBol(rowx("tProceed"))
        '    messagedialog = Generic.ToStr(rowx("xMessage"))
        '    alerttype = Generic.ToStr(rowx("AlertType"))
        'Next

        'If invalid = True Then
        '    MessageBox.Alert(messagedialog, alerttype, Me)
        '    ModalPopupExtender1.Show()
        '    Exit Sub
        'End If

        ''If SQLHelper.ExecuteNonQuery("EApplicantExpe_WebSave", UserNo, Generic.ToInt(txtApplicantExpeNo.Text), TransNo, Me.txtExpeComp.Text.ToString, ExpeTypeNo, Me.txtPosition.Text, "", "", Me.txtRemarks.Text.ToString, NoOfYear, CurrentSalary, EmployeeStatNo, 0, Accredited, LWOP, Me.txtExpeCompAdd.Text.ToString, Me.txtCompPhone.Text.ToString, "", Me.txtDuties.Text.ToString, "", 0, txtIndustry.Text, FromDay, FromMonth, FromYear, ToDay, ToMonth, ToYear, IsOtherExpe, OtherExpe) > 0 Then
        ''    RetVal = True
        ''Else
        ''    RetVal = False
        ''End If

        'If SQLHelper.ExecuteNonQuery("EApplicantExpe_WebSave", UserNo, Generic.ToInt(txtApplicantExpeNo.Text), _
        '                           TransNo, txtExpeComp.Text, ExpeTypeNo, txtPosition.Text, txtFromDate.Text, txtToDate.Text, _
        '                           txtRemarks.Text, NoOfYear, CurrentSalary, EmployeeStatNo, Generic.ToInt(chkIsGov.Checked), Accredited, LWOP, _
        '                           txtExpeCompAdd.Text, txtCompPhone.Text, txtImmediateSuperior.Text, Me.txtDuties.Text.ToString, txtReasonsForLeaving.Text, 0, txtIndustry.Text, _
        '                           FromDay, FromMonth, FromYear, ToDay, ToMonth, ToYear, IsOtherExpe, OtherExpe, txtSalaryLevel.Text, txtAccomplishment.Text, txtAllowances.Text, txtIndustry.Text, Generic.ToInt(Me.chkIsPresent.Checked)) > 0 Then

        '    'If SQLHelper.ExecuteNonQuery("EApplicantExpe_WebSave", UserNo, Generic.ToInt(txtApplicantExpeNo.Text), _
        '    '                           TransNo, txtExpeComp.Text, ExpeTypeNo, txtPosition.Text, txtFromDate.Text, txtToDate.Text, _
        '    '                           txtRemarks.Text, NoOfYear, CurrentSalary, EmployeeStatNo, chkIsGov.Checked, Accredited, LWOP, _
        '    '                           txtExpeCompAdd.Text, txtCompPhone.Text, txtImmediateSuperior.Text, Me.txtDuties.Text.ToString, txtReasonsForLeaving.Text, 0, txtIndustry.Text, _
        '    '                           FromDay, FromMonth, FromYear, ToDay, ToMonth, ToYear, IsOtherExpe, OtherExpe, txtSalaryLevel.Text, txtAccomplishment.Text, txtAllowances.Text, txtIndustry.Text, Me.chkIsPresent.Checked) > 0 Then
        '    RetVal = True
        'Else
        '    RetVal = False
        'End If

        'If RetVal = True Then
        '    PopulateGrid()
        '    MessageBox.Success(MessageTemplate.SuccessSave, Me)
        'End If
        Dim RetVal As Boolean = False
        Dim ApplicantExpeNo As Integer = Generic.ToInt(txtApplicantExpeNo.Text)
        Dim ExpeTypeNo As Integer = Generic.ToInt(cboExpeTypeNo.SelectedValue)
        'Dim ExpeTypeNo As Integer = Generic.ToInt(hifExpeTypeNo.Value)

        Dim NoOfYear As Double = 0
        Dim CurrentSalary As Double = Generic.ToDbl(txtCurrentSalary.Text)
        Dim ApplicantStatNo As Integer = Generic.ToInt(cboEmployeeStatNo.SelectedValue)
        Dim Accredited As Double = 0
        Dim LWOP As Double = 0
        Dim FromDay As Integer = Generic.ToInt(cboFromDay.SelectedValue)
        Dim FromMonth As Integer = Generic.ToInt(cboFromMonth.SelectedValue)
        Dim FromYear As Integer = Generic.ToInt(txtFromYear.Text)
        Dim ToDay As Integer = Generic.ToInt(cboToDay.SelectedValue)
        Dim ToMonth As Integer = Generic.ToInt(cboToMonth.SelectedValue)
        Dim ToYear As Integer = Generic.ToInt(txtToYear.Text)
        Dim IsOtherExpe As Boolean = Generic.ToBol(txtIsOtherExpe.Checked)
        Dim OtherExpe As String = Generic.ToStr(txtOtherExpe.Text)
        Dim IndustryNo As Integer = Generic.ToInt(cboIndustryNo.SelectedValue)
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EApplicantExpe_WebValidate", UserNo, ApplicantExpeNo, _
                                   TransNo, txtExpeComp.Text, ExpeTypeNo, txtPosition.Text, txtFromDate.Text, txtToDate.Text, _
                                   txtRemarks.Text, NoOfYear, CurrentSalary, ApplicantStatNo, chkIsGov.Checked, Accredited, LWOP, _
                                   txtExpeCompAdd.Text, txtCompPhone.Text, "", Me.txtDuties.Text.ToString, "", 0, txtIndustry.Text, _
                                   FromDay, FromMonth, FromYear, ToDay, ToMonth, ToYear, IsOtherExpe, OtherExpe)

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

        If SQLHelper.ExecuteNonQuery("EApplicantExpe_WebSave", UserNo, ApplicantExpeNo, _
                                   TransNo, txtExpeComp.Text, ExpeTypeNo, txtPosition.Text, txtFromDate.Text, txtToDate.Text, _
                                   txtRemarks.Text, NoOfYear, CurrentSalary, ApplicantStatNo, Generic.ToInt(chkIsGov.Checked), Accredited, LWOP, _
                                   txtExpeCompAdd.Text, txtCompPhone.Text, txtImmediateSuperior.Text, Me.txtDuties.Text.ToString, txtReasonsForLeaving.Text, 0, txtIndustry.Text, _
                                   FromDay, FromMonth, FromYear, ToDay, ToMonth, ToYear, IsOtherExpe, OtherExpe, txtSalaryLevel.Text, txtAccomplishment.Text, txtAllowances.Text, IndustryNo, Generic.ToInt(Me.chkIsPresent.Checked), Generic.ToBol(chkIsWithOut.Checked), txtRatings.Text) > 0 Then
            RetVal = True
            'VerifyTab(ApplicantNo)
        Else
            RetVal = False
        End If

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then            
            Dim lnk As New LinkButton
            lnk = sender
            Generic.ClearControls(Me, "Panel1")
            'IswithOut()
            PopulateData(Generic.ToInt(lnk.CommandArgument))
            IswithOut()
            PopulateControls()
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ApplicantExpeNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EApplicantExpe", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Sub PopulateCombo()

        Try
            cboFromMonth.DataSource = SQLHelper.ExecuteDataSet("EMonth_WebLookup")
            cboFromMonth.DataValueField = "tNo"
            cboFromMonth.DataTextField = "tDesc"
            cboFromMonth.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboToMonth.DataSource = SQLHelper.ExecuteDataSet("EMonth_WebLookup")
            cboToMonth.DataValueField = "tNo"
            cboToMonth.DataTextField = "tDesc"
            cboToMonth.DataBind()
        Catch ex As Exception
        End Try


        Try
            cboFromDay.DataSource = SQLHelper.ExecuteDataSet("EDay_WebLookup")
            cboFromDay.DataValueField = "tNo"
            cboFromDay.DataTextField = "tDesc"
            cboFromDay.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboToDay.DataSource = SQLHelper.ExecuteDataSet("EDay_WebLookup")
            cboToDay.DataValueField = "tNo"
            cboToDay.DataTextField = "tDesc"
            cboToDay.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub PopulateControls()

        If txtIsOtherExpe.Checked = True Then
            txtOtherExpe.Enabled = True
            'txtOtherExpe.CssClass = "form-control required"
            cboExpeTypeNo.CssClass = "form-control"
            cboExpeTypeNo.Enabled = False
            cboExpeTypeNo.Text = ""
        Else
            txtOtherExpe.Enabled = False
            txtOtherExpe.Text = ""
            txtOtherExpe.CssClass = "form-control"
            'cboExpeTypeNo.CssClass = "form-control required"
            If chkIsWithOut.Checked Then
                cboExpeTypeNo.Enabled = False
            Else
                cboExpeTypeNo.Enabled = True
            End If

        End If

    End Sub

    Protected Sub txtIsOtherExpe_CheckedChanged(sender As Object, e As System.EventArgs) Handles txtIsOtherExpe.CheckedChanged
        PopulateControls()
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub chkIsPresent_CheckedChanged(sender As Object, e As System.EventArgs) 'Handles chkIsPresent.CheckedChanged
        chkIsPresent_Checked()
        ModalPopupExtender1.Show()

    End Sub

    Protected Sub chkIsPresent_Checked()
        If Me.chkIsPresent.Checked Then
            txtToDate.CssClass = "form-control"
            txtToDate.Text = ""
            txtToDate.Enabled = False
        Else
            txtToDate.CssClass = "form-control required"
            txtToDate.Enabled = True
        End If
    End Sub

    Protected Sub chkIsWithOut_CheckedChanged(sender As Object, e As EventArgs)
        IswithOut()
        ModalPopupExtender1.Show()
    End Sub

    Private Sub IswithOut()
        If chkIsWithOut.Checked = True Then
            txtFromDate.CssClass = "form-control"
            txtToDate.CssClass = "form-control"
            txtPosition.CssClass = "form-control"
            txtExpeComp.CssClass = "form-control"
            txtCurrentSalary.CssClass = "form-control"
            txtPosition.Text = "No Work Experience"

            Generic.EnableControls(Me, "Panel1", False)
            chkIsWithOut.Enabled = True

        Else
            txtFromDate.CssClass = "form-control required"
            txtToDate.CssClass = "form-control required"
            txtPosition.CssClass = "form-control required"
            txtExpeComp.CssClass = "form-control required"
            txtCurrentSalary.CssClass = "form-control required"

            Generic.EnableControls(Me, "Panel1", True)
            chkIsPresent_Checked()
        End If
    End Sub

End Class
