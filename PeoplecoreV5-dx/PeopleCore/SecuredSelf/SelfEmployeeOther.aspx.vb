Imports clsLib
Imports System.Data

Partial Class SecuredSelf_SelfEmployeeOther
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim EmployeeNo As Integer
    Dim PayLocNo As Integer
    Dim IsEnabled As Boolean = False

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeOtherInfo_WebOne", UserNo, EmployeeNo, PayLocNo)
        Generic.PopulateData(Me, "Panel1", dt)
        OptionEvents()
    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        EmployeeNo = Generic.ToInt(Request.QueryString("id"))
        TabSelf.TransactionNo = EmployeeNo
        Permission.IsAuthenticated()
        If Not IsPostBack Then
            EnabledControls()
            Generic.PopulateDropDownList_Self(UserNo, Me, "Panel1", PayLocNo)
            PopulateData()
            PopulateTabHeader()
        End If

    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)

        If IsEnabled = True Then
            OptionEvents()
        End If

        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, EmployeeNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(EmployeeNo) & "&tIndex=2"
    End Sub

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        ViewState("IsEnabled") = True
        EnabledControls()
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim isabled As Integer = Generic.ToInt(rblIsAbled.SelectedValue)
        Dim iscandidate As Integer = Generic.ToInt(rblIsCandidate.SelectedValue)
        Dim Ischarged As Integer = Generic.ToInt(rblIsCharged.SelectedValue)
        Dim isCourt As Integer = Generic.ToInt(rblIsCourt.SelectedValue)
        Dim IsIndigengrp As Integer = Generic.ToInt(rblIsIndigenGrp.SelectedValue)
        Dim IsLGovEmployee As Integer = Generic.ToInt(rblIsLGovEmployee.SelectedValue)
        Dim IsNGovEmployee As Integer = Generic.ToInt(rblIsNGovEmployee.SelectedValue)
        Dim IsOffensed As Integer = Generic.ToInt(rblIsOffensed.SelectedValue)
        Dim IsSector As Integer = Generic.ToInt(rblIsSector.SelectedValue)
        Dim IsSoloparent As Integer = Generic.ToInt(rblIsSoloParent.SelectedValue)
        Dim IsGuilty As Integer = 0 'Generic.ToInt(rblIsGuilty.SelectedValue)
        Dim IsSuspended As Integer = 0 'Generic.ToInt(rblIsSuspended.SelectedValue)
        Dim IsResigned As Integer = Generic.ToInt(rblIsResigned.SelectedValue)
        Dim IsConsanguinity As Integer = Generic.ToInt(rblIsConsanguinity.SelectedValue)
        Dim IsAffinity As Integer = Generic.ToInt(rblIsAffinity.SelectedValue)
        Dim IsOtherRelative As Integer = Generic.ToInt(rblIsOtherRelative.SelectedValue)
        Dim IsFormer As Integer = Generic.ToInt(rblIsFormer.SelectedValue)
        Dim IsRespondent As Integer = Generic.ToInt(rblIsRespondent.SelectedValue)
        Dim IsOngoingA As Integer = Generic.ToInt(chkIsOngoingA.Checked)
        Dim IsDismissedA As Integer = Generic.ToInt(chkIsDismissedA.Checked)
        Dim IsHypertension As Integer = Generic.ToInt(chkIsHypertension.Checked)
        Dim IsDiabetes As Integer = Generic.ToInt(chkIsDiabetes.Checked)
        Dim IsAcquiredHeartDisease As Integer = Generic.ToInt(chkIsAcquiredHeartDisease.Checked)
        Dim IsKidneyDisease As Integer = Generic.ToInt(chkIsKidneyDisease.Checked)
        Dim IsTuberculosis As Integer = Generic.ToInt(chkIsTuberculosis.Checked)
        Dim IsChronicPumonary As Integer = Generic.ToInt(chkIsChronicPumonary.Checked)
        Dim IsMalignancies As Integer = Generic.ToInt(chkIsMalignancies.Checked)
        Dim IsAutoimmune As Integer = Generic.ToInt(chkIsAutoimmune.Checked)
        Dim IsCardiovascularAccident As Integer = Generic.ToInt(chkIsCardiovascularAccident.Checked)
        Dim IsNeuroPsychiatric As Integer = Generic.ToInt(chkIsNeuroPsychiatric.Checked)
        Dim IsHematologic As Integer = Generic.ToInt(chkIsHematologic.Checked)
        Dim IsChronicLiver As Integer = Generic.ToInt(chkIsChronicLiver.Checked)
        Dim IsMajorcongenital As Integer = Generic.ToInt(chkIsMajorcongenital.Checked)
        Dim IsOthers As Integer = Generic.ToInt(chkIsOthers.Checked)
        Dim IsAssigned As Integer = Generic.ToInt(rblIsAssigned.SelectedValue)
        Dim dt As DataTable, ret As Integer, msg As String = ""
        Dim str As String = UserNo & ", " & EmployeeNo & ", " & Me.txtHobbies.Text.ToString & ", " & Me.txtRecognition.Text.ToString & ", " & Me.txtOrganization.Text.ToString & ", " & IsNGovEmployee & ", " & txtNGovEmployeeDeti.Text & ", " & IsLGovEmployee & ", " & txtLGovEmployeeDeti.Text & ", " & "" & ", " & _
                                        "" & ", " & Ischarged & ", " & Me.txtChargedDeti.Text & ", " & IsOffensed & ", " & Me.txtOffensedDeti.Text & ", " & isCourt & ", " & txtCourtDeti.Text & ", " & IsSector & ", " & Me.txtSectorDeti.Text & ", " & iscandidate & ", " & _
                                        Me.txtCandidateDeti.Text & ", " & IsIndigengrp & ", " & Me.txtIndigenGrpDeti.Text & ", " & isabled & ", " & Me.txtAbledDeti.Text & ", " & IsSoloparent & ", " & Me.txtSoloParentDeti.Text & ", " & IsGuilty & ", " & "" & ", " & IsSuspended & ", " & _
                                        "" & ", " & PayLocNo & ", " & IsResigned & ", " & txtResignedDeti.Text & ", " & txtChargedDetiDate.Text & ", " & txtChargedDetiStatus.Text & ", " & txtSoloParentExpiryDate.Text & ", " & IsConsanguinity & ", " & txtConsanguinityDeti.Text & ", " & IsAffinity & ", " & _
                                        txtAffinityDeti.Text & ", " & IsOtherRelative & ", " & txtOtherRelativeDeti.Text & ", " & IsFormer & ", " & txtFormerDeti.Text & ", " & IsRespondent & ", " & txtRespondentDeti.Text & ", " & IsOngoingA & ", " & IsDismissedA & ", " & IsHypertension & ", " & _
                                        IsDiabetes & ", " & IsAcquiredHeartDisease & ", " & IsKidneyDisease & ", " & IsTuberculosis & ", " & IsChronicPumonary & ", " & IsMalignancies & ", " & IsAutoimmune & ", " & IsCardiovascularAccident & ", " & IsNeuroPsychiatric & ", " & IsHematologic & ", " & _
                                        IsChronicLiver & ", " & IsMajorcongenital & ", " & IsOthers & ", " & txtOtherDeti.Text & ", " & IsAssigned
        dt = SQLHelper.ExecuteDataTable("EEmployeeOtherUpd_WebSave", UserNo, EmployeeNo, Me.txtHobbies.Text.ToString, Me.txtRecognition.Text.ToString, Me.txtOrganization.Text.ToString, IsNGovEmployee, txtNGovEmployeeDeti.Text, IsLGovEmployee, txtLGovEmployeeDeti.Text, "", _
                                        "", Ischarged, Me.txtChargedDeti.Text, IsOffensed, Me.txtOffensedDeti.Text, isCourt, txtCourtDeti.Text, IsSector, Me.txtSectorDeti.Text, iscandidate, _
                                        Me.txtCandidateDeti.Text, IsIndigengrp, Me.txtIndigenGrpDeti.Text, isabled, Me.txtAbledDeti.Text, IsSoloparent, Me.txtSoloParentDeti.Text, IsGuilty, "", IsSuspended, _
                                        "", PayLocNo, IsResigned, txtResignedDeti.Text, txtChargedDetiDate.Text, txtChargedDetiStatus.Text, txtSoloParentExpiryDate.Text, IsConsanguinity, txtConsanguinityDeti.Text, IsAffinity, _
                                        txtAffinityDeti.Text, IsOtherRelative, txtOtherRelativeDeti.Text, IsFormer, txtFormerDeti.Text, IsRespondent, txtRespondentDeti.Text, IsOngoingA, IsDismissedA, IsHypertension, _
                                        IsDiabetes, IsAcquiredHeartDisease, IsKidneyDisease, IsTuberculosis, IsChronicPumonary, IsMalignancies, IsAutoimmune, IsCardiovascularAccident, IsNeuroPsychiatric, IsHematologic, _
                                        IsChronicLiver, IsMajorcongenital, IsOthers, txtOtherDeti.Text, IsAssigned)

        For Each row As DataRow In dt.Rows
            msg = Generic.ToStr(row("xMessage"))
            ret = Generic.ToInt(row("RetVal"))    
        Next
        If ret = 1 Then
            MessageBox.Success(msg, Me)
            ViewState("IsEnabled") = False
            EnabledControls()
        Else
            MessageBox.Information(msg, Me)
        End If
    End Sub

    Private Sub OptionEvents()
        rblNGovEmployee_SelectedIndexChanged()
        rblIsLGovEmployee_SelectedIndexChanged()
        rblIsCharged_SelectedIndexChanged()
        rblIsOffensed_SelectedIndexChanged()
        rblIsCourt_SelectedIndexChanged()
        rblIsGuilty_SelectedIndexChanged()
        rblIsGuilty_SelectedIndexChanged()
        rblIsSuspended_SelectedIndexChanged()
        rblIsSector_SelectedIndexChanged()
        rblIsCandidate_SelectedIndexChanged()
        rblIsIndigenGrp_SelectedIndexChanged()
        rblIsAbled_SelectedIndexChanged()
        rblIsSoloParent_SelectedIndexChanged()
        rblIsConsanguinity_SelectedIndexChanged()
        rblIsAffinity_SelectedIndexChanged()
        rblIsOtherRelative_SelectedIndexChanged()
        rblIsResigned_SelectedIndexChanged()
        rblIsFormer_SelectedIndexChanged()
        rblIsRespondent_SelectedIndexChanged()
        chkIsOthers_CheckedChange()
    End Sub

#Region "RadioButtonList Event"

    Protected Sub rblNGovEmployee_SelectedIndexChanged()
        If rblIsNGovEmployee.SelectedValue = "1" Then
            txtNGovEmployeeDeti.CssClass = "form-control required"
            txtNGovEmployeeDeti.Enabled = True
        Else
            txtNGovEmployeeDeti.Enabled = False
            txtNGovEmployeeDeti.CssClass = "form-control"
            txtNGovEmployeeDeti.Text = ""
        End If
    End Sub

    Protected Sub rblIsLGovEmployee_SelectedIndexChanged()
        If rblIsLGovEmployee.SelectedValue = "1" Then
            txtLGovEmployeeDeti.CssClass = "form-control required"
            txtLGovEmployeeDeti.Enabled = True
            'txtGovApplicantDeti2.CssClass = "form-control required"
            'txtGovApplicantDeti2.Enabled = True
            'txtGovApplicantDeti3.CssClass = "form-control required"
            'txtGovApplicantDeti3.Enabled = True
        Else
            txtLGovEmployeeDeti.CssClass = "form-control"
            txtLGovEmployeeDeti.Enabled = False
            txtLGovEmployeeDeti.Text = ""
            'txtGovApplicantDeti2.CssClass = "form-control"
            'txtGovApplicantDeti2.Enabled = False
            'txtGovApplicantDeti2.Text = ""
            'txtGovApplicantDeti3.CssClass = "form-control"
            'txtGovApplicantDeti3.Enabled = False
            'txtGovApplicantDeti3.Text = ""
        End If
    End Sub

    Protected Sub rblIsCharged_SelectedIndexChanged()
        If rblIsCharged.SelectedValue = "1" Then
            txtChargedDeti.CssClass = "form-control required"
            txtChargedDeti.Enabled = True
        Else
            txtChargedDeti.Enabled = False
            txtChargedDeti.CssClass = "form-control"
            txtChargedDeti.Text = ""
        End If
    End Sub

    Protected Sub rblIsOffensed_SelectedIndexChanged()
        If rblIsOffensed.SelectedValue = "1" Then
            txtOffensedDeti.CssClass = "form-control required"
            txtOffensedDeti.Enabled = True
        Else
            txtOffensedDeti.Enabled = False
            txtOffensedDeti.CssClass = "form-control"
            txtOffensedDeti.Text = ""
        End If
    End Sub

    Protected Sub rblIsCourt_SelectedIndexChanged()
        If rblIsCourt.SelectedValue = "1" Then
            txtCourtDeti.CssClass = "form-control required"
            txtCourtDeti.Enabled = True
        Else
            txtCourtDeti.Enabled = False
            txtCourtDeti.CssClass = "form-control"
            txtCourtDeti.Text = ""
        End If
    End Sub

    Protected Sub rblIsGuilty_SelectedIndexChanged()
        'If rblIsGuilty.SelectedValue = "1" Then
        '    txtGuiltyDeti.CssClass = "form-control required"
        '    txtGuiltyDeti.Enabled = True
        'Else
        '    txtGuiltyDeti.Enabled = False
        '    txtGuiltyDeti.CssClass = "form-control"
        '    txtGuiltyDeti.Text = ""
        'End If
    End Sub

    Protected Sub rblIsSuspended_SelectedIndexChanged()
        'If rblIsSuspended.SelectedValue = "1" Then
        '    txtSuspendedDeti.CssClass = "form-control required"
        '    txtSuspendedDeti.Enabled = True
        'Else
        '    txtSuspendedDeti.Enabled = False
        '    txtSuspendedDeti.CssClass = "form-control"
        '    txtSuspendedDeti.Text = ""
        'End If
    End Sub

    Protected Sub rblIsSector_SelectedIndexChanged()
        If rblIsSector.SelectedValue = "1" Then
            txtSectorDeti.CssClass = "form-control required"
            txtSectorDeti.Enabled = True
        Else
            txtSectorDeti.Enabled = False
            txtSectorDeti.CssClass = "form-control"
            txtSectorDeti.Text = ""
        End If
    End Sub

    Protected Sub rblIsCandidate_SelectedIndexChanged()
        If rblIsCandidate.SelectedValue = "1" Then
            txtCandidateDeti.CssClass = "form-control required"
            txtCandidateDeti.Enabled = True
        Else
            txtCandidateDeti.Enabled = False
            txtCandidateDeti.CssClass = "form-control"
            txtCandidateDeti.Text = ""
        End If
    End Sub

    Protected Sub rblIsIndigenGrp_SelectedIndexChanged()
        If rblIsIndigenGrp.SelectedValue = "1" Then
            txtIndigenGrpDeti.CssClass = "form-control required"
            txtIndigenGrpDeti.Enabled = True
        Else
            txtIndigenGrpDeti.Enabled = False
            txtIndigenGrpDeti.CssClass = "form-control"
            txtIndigenGrpDeti.Text = ""
        End If
    End Sub

    Protected Sub rblIsAbled_SelectedIndexChanged()
        If rblIsAbled.SelectedValue = "1" Then
            txtAbledDeti.CssClass = "form-control required"
            txtAbledDeti.Enabled = True
        Else
            txtAbledDeti.Enabled = False
            txtAbledDeti.CssClass = "form-control"
            txtAbledDeti.Text = ""
        End If
    End Sub

    Protected Sub rblIsSoloParent_SelectedIndexChanged()
        If rblIsSoloParent.SelectedValue = "1" Then
            txtSoloParentDeti.CssClass = "form-control required"
            txtSoloParentDeti.Enabled = True
            txtSoloParentExpiryDate.Enabled = True
        Else
            txtSoloParentDeti.Enabled = False
            txtSoloParentExpiryDate.Enabled = False
            txtSoloParentDeti.CssClass = "form-control"
            txtSoloParentDeti.Text = ""
            txtSoloParentExpiryDate.Text = ""
        End If
    End Sub

    Protected Sub rblIsConsanguinity_SelectedIndexChanged()
        If rblIsConsanguinity.SelectedValue = "1" Then
            txtConsanguinityDeti.CssClass = "form-control required"
            txtConsanguinityDeti.Enabled = True
        Else
            txtConsanguinityDeti.Enabled = False
            txtConsanguinityDeti.CssClass = "form-control"
            txtConsanguinityDeti.Text = ""
        End If
    End Sub

    Protected Sub rblIsAffinity_SelectedIndexChanged()
        If rblIsAffinity.SelectedValue = "1" Then
            txtAffinityDeti.CssClass = "form-control required"
            txtAffinityDeti.Enabled = True
        Else
            txtAffinityDeti.Enabled = False
            txtAffinityDeti.CssClass = "form-control"
            txtAffinityDeti.Text = ""
        End If
    End Sub

    Protected Sub rblIsOtherRelative_SelectedIndexChanged()
        If rblIsOtherRelative.SelectedValue = "1" Then
            txtOtherRelativeDeti.CssClass = "form-control required"
            txtOtherRelativeDeti.Enabled = True
        Else
            txtOtherRelativeDeti.Enabled = False
            txtOtherRelativeDeti.CssClass = "form-control"
            txtOtherRelativeDeti.Text = ""
        End If
    End Sub

    Protected Sub rblIsResigned_SelectedIndexChanged()
        If rblIsResigned.SelectedValue = "1" Then
            txtResignedDeti.CssClass = "form-control required"
            txtResignedDeti.Enabled = True
        Else
            txtResignedDeti.Enabled = False
            txtResignedDeti.CssClass = "form-control"
            txtResignedDeti.Text = ""
        End If
    End Sub

    Protected Sub rblIsFormer_SelectedIndexChanged()
        If rblIsFormer.SelectedValue = "1" Then
            txtFormerDeti.CssClass = "form-control required"
            txtFormerDeti.Enabled = True
        Else
            txtFormerDeti.Enabled = False
            txtFormerDeti.CssClass = "form-control"
            txtFormerDeti.Text = ""
        End If
    End Sub

    Protected Sub rblIsRespondent_SelectedIndexChanged()
        If rblIsRespondent.SelectedValue = "1" Then
            txtRespondentDeti.CssClass = "form-control required"
            txtRespondentDeti.Enabled = True
        Else
            txtRespondentDeti.Enabled = False
            txtRespondentDeti.CssClass = "form-control"
            txtRespondentDeti.Text = ""
        End If
    End Sub

    Protected Sub chkIsOthers_CheckedChange()
        If chkIsOthers.Checked Then
            txtOtherDeti.CssClass = "form-control required"
            txtOtherDeti.Enabled = True
        Else
            txtOtherDeti.Enabled = False
            txtOtherDeti.CssClass = "form-control"
            txtOtherDeti.Text = ""
        End If
    End Sub


#End Region




End Class
