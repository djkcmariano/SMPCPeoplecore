Imports System.Data
Imports clsLib
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Partial Class Secured_AppInfoEdit
    Inherits System.Web.UI.Page
    Dim TransNo As Int64
    Dim IsEnabled As Boolean = False
    Dim UserNo As Int64
    Dim error_message As String = ""
    Private Sub OptionEvents()
        rblNGovApplicant_SelectedIndexChanged()
        rblIsLGovApplicant_SelectedIndexChanged()
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

    Protected Sub rblNGovApplicant_SelectedIndexChanged()
        If rblNGovApplicant.SelectedValue = "1" Then
            txtNGovApplicantDeti.CssClass = "form-control required"
            txtNGovApplicantDeti.Enabled = True
        Else
            txtNGovApplicantDeti.Enabled = False
            txtNGovApplicantDeti.CssClass = "form-control"
            txtNGovApplicantDeti.Text = ""
        End If
    End Sub

    Protected Sub rblIsLGovApplicant_SelectedIndexChanged()
        If rblIsLGovApplicant.SelectedValue = "1" Then
            txtGovApplicantDeti.CssClass = "form-control required"
            txtGovApplicantDeti.Enabled = True
            'txtGovApplicantDeti2.CssClass = "form-control required"
            'txtGovApplicantDeti2.Enabled = True
            'txtGovApplicantDeti3.CssClass = "form-control required"
            'txtGovApplicantDeti3.Enabled = True
        Else
            txtGovApplicantDeti.CssClass = "form-control"
            txtGovApplicantDeti.Enabled = False
            txtGovApplicantDeti.Text = ""
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
        If rblIsGuilty.SelectedValue = "1" Then
            txtGuiltyDeti.CssClass = "form-control required"
            txtGuiltyDeti.Enabled = True
        Else
            txtGuiltyDeti.Enabled = False
            txtGuiltyDeti.CssClass = "form-control"
            txtGuiltyDeti.Text = ""
        End If
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
            txtSoloParentDetiDate.Enabled = True
        Else
            txtSoloParentDeti.Enabled = False
            txtSoloParentDetiDate.Enabled = False
            txtSoloParentDeti.CssClass = "form-control"
            txtSoloParentDeti.Text = ""
            txtSoloParentDetiDate.Text = ""
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

    Private Sub PopulateData()
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EApplicantOther_WebOne", UserNo, TransNo)
            For Each row As DataRow In ds.Tables(0).Rows
                txtHobbies.Text = Generic.ToStr(row("Hobbies"))
                txtRecognition.Text = Generic.ToStr(row("Recognition"))
                txtOrganization.Text = Generic.ToStr(row("Organization"))
                rblNGovApplicant.SelectedValue = Generic.ToInt(row("IsNGovApplicant"))
                txtNGovApplicantDeti.Text = Generic.ToStr(row("NGovApplicantDeti"))
                rblIsLGovApplicant.SelectedValue = Generic.ToInt(row("IsLGovApplicant"))
                txtGovApplicantDeti.Text = Generic.ToStr(row("GovApplicantDeti"))
                'txtGovApplicantDeti2.Text = Generic.ToStr(row("GovApplicantDeti2"))
                'txtGovApplicantDeti3.Text = Generic.ToStr(row("GovApplicantDeti3"))
                rblIsCharged.SelectedValue = Generic.ToInt(row("IsCharged"))
                txtChargedDeti.Text = Generic.ToStr(row("ChargedDeti"))
                rblIsOffensed.SelectedValue = Generic.ToInt(row("IsOffensed"))
                txtOffensedDeti.Text = Generic.ToStr(row("OffensedDeti"))
                rblIsCourt.SelectedValue = Generic.ToInt(row("IsCourt"))
                txtCourtDeti.Text = Generic.ToStr(row("CourtDeti"))
                rblIsGuilty.SelectedValue = Generic.ToInt(row("IsGuilty"))
                txtGuiltyDeti.Text = Generic.ToStr(row("GuiltyDeti"))
                'rblIsSuspended.SelectedValue = Generic.ToInt(row("IsSuspended"))
                'txtSuspendedDeti.Text = Generic.ToStr(row("SuspendedDeti"))
                rblIsSector.SelectedValue = Generic.ToInt(row("IsSector"))
                txtSectorDeti.Text = Generic.ToStr(row("SectorDeti"))
                rblIsCandidate.SelectedValue = Generic.ToInt(row("IsCandidate"))
                txtCandidateDeti.Text = Generic.ToStr(row("CandidateDeti"))
                rblIsIndigenGrp.SelectedValue = Generic.ToInt(row("IsIndigenGrp"))
                txtIndigenGrpDeti.Text = Generic.ToStr(row("IndigenGrpDeti"))
                rblIsAbled.SelectedValue = Generic.ToInt(row("IsAbled"))
                txtAbledDeti.Text = Generic.ToStr(row("AbledDeti"))
                rblIsSoloParent.SelectedValue = Generic.ToInt(row("IsSoloParent"))
                txtSoloParentDeti.Text = Generic.ToStr(row("SoloParentDeti"))
                txtSoloParentDetiDate.Text = Generic.ToStr(row("SoloParentExpiryDate"))

                txtGovtIssuedID.Text = Generic.ToStr(row("GovtIssuedID"))
                txtGovtIssuedIDNo.Text = Generic.ToStr(row("GovtIssuedIDNo"))
                txtGovtIssueDatePlace.Text = Generic.ToStr(row("GovtIssueDatePlace"))

                rblIsConsanguinity.SelectedValue = Generic.ToInt(row("IsConsanguinity"))
                txtConsanguinityDeti.Text = Generic.ToStr(row("ConsanguinityDeti"))
                rblIsAffinity.SelectedValue = Generic.ToInt(row("IsAffinity"))
                txtAffinityDeti.Text = Generic.ToStr(row("AffinityDeti"))
                rblIsOtherRelative.SelectedValue = Generic.ToInt(row("IsOtherRelative"))
                txtOtherRelativeDeti.Text = Generic.ToStr(row("OtherRelativeDeti"))
                rblIsFormer.SelectedValue = Generic.ToInt(row("IsFormer"))
                txtFormerDeti.Text = Generic.ToStr(row("FormerDeti"))
                rblIsRespondent.SelectedValue = Generic.ToInt(row("IsRespondent"))
                txtRespondentDeti.Text = Generic.ToStr(row("RespondentDeti"))

                chkIsOngoingA.Checked = Generic.ToInt(row("IsOngoingA"))
                chkIsOngoingC.Checked = Generic.ToInt(row("IsOngoingc"))
                'chkIsAcquittedA.Checked = Generic.ToInt(row("IsAcquittedA"))
                'chkIsAcquittedC.Checked = Generic.ToInt(row("IsAcquittedC"))
                chkIsDismissedA.Checked = Generic.ToInt(row("IsDismissedA"))
                chkIsDismissedC.Checked = Generic.ToInt(row("IsDismissedC"))

                chkIsHypertension.Checked = Generic.ToInt(row("IsHypertension"))
                chkIsDiabetes.Checked = Generic.ToInt(row("IsDiabetes"))
                chkIsAcquiredHeartDisease.Checked = Generic.ToInt(row("IsAcquiredHeartDisease"))
                chkIsKidneyDisease.Checked = Generic.ToInt(row("IsKidneyDisease"))
                chkIsTuberculosis.Checked = Generic.ToInt(row("IsTuberculosis"))
                chkIsChronicPumonary.Checked = Generic.ToInt(row("IsChronicPumonary"))
                chkIsMalignancies.Checked = Generic.ToInt(row("IsMalignancies"))
                chkIsAutoimmune.Checked = Generic.ToInt(row("IsAutoimmune"))
                chkIsCardiovascularAccident.Checked = Generic.ToInt(row("IsCardiovascularAccident"))
                chkIsNeuroPsychiatric.Checked = Generic.ToInt(row("IsNeuroPsychiatric"))
                chkIsHematologic.Checked = Generic.ToInt(row("IsHematologic"))
                chkIsChronicLiver.Checked = Generic.ToInt(row("IsChronicLiver"))
                chkIsMajorcongenital.Checked = Generic.ToInt(row("IsMajorcongenital"))
                chkIsOthers.Checked = Generic.ToInt(row("IsOthers"))
                txtOtherDeti.Text = Generic.ToStr(row("OtherDeti"))
                rblIsAssigned.SelectedValue = Generic.ToInt(row("IsAssigned"))
                txtChargedDetiDate.Text = Generic.ToStr(row("ChargedDetiDate"))
                txtChargedDetiStatus.Text = Generic.ToStr(row("ChargedDetiStatus"))
            Next

            For Each row As DataRow In ds.Tables(1).Rows
                txtGSISNo.Text = Generic.ToStr(row("GSISNo"))
                txtPHNo.Text = Generic.ToStr(row("PHNo"))
                txtHDMFNo.Text = Generic.ToStr(row("HDMFNo"))
                txtTINNo.Text = Generic.ToStr(row("TINNo"))
                txtSSSNo.Text = Generic.ToStr(row("SSSNo"))
                txtCTCN.Text = Generic.ToStr(row("CTCN"))
                txtCTCNIssuedAt.Text = Generic.ToStr(row("CTCNIssuedAt"))
                txtCTCNIssuedOn.Text = Generic.ToStr(row("CTCNIssuedOn"))
                txtWeight.Text = Generic.ToStr(row("Weight"))
                txtHeight.Text = Generic.ToStr(row("Height"))
                cboTaxExemptNo.Text = IIf(Generic.ToInt(row("TaxExemptNo")) = 0, "", Generic.ToInt(row("TaxExemptNo")))
                cboShoeNo.Text = IIf(Generic.ToInt(row("ShoeNo")) = 0, "", Generic.ToInt(row("ShoeNo")))
                cboTShirtNo.Text = IIf(Generic.ToInt(row("TShirtNo")) = 0, "", Generic.ToInt(row("TShirtNo")))
            Next
        Catch ex As Exception

        End Try
        OptionEvents()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)
        If TransNo = 0 Then : IsEnabled = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", 0)
            PopulateData()
            PopulateTabHeader()
        End If
        EnabledControls()
    End Sub

    Private Function SaveRecord() As Boolean
        Dim dt1 As DataTable = SQLHelper.ExecuteDataTable("EApplicantOther_WebSave", UserNo, TransNo, txtHobbies.Text, txtRecognition.Text,
                                                                                            txtOrganization.Text, Generic.ToInt(rblNGovApplicant.SelectedValue),
                                                                                            txtNGovApplicantDeti.Text, Generic.ToInt(rblIsLGovApplicant.SelectedValue),
                                                                                            txtGovApplicantDeti.Text, "",
                                                                                            "", Generic.ToInt(rblIsCharged.SelectedValue),
                                                                                            txtChargedDeti.Text, Generic.ToInt(rblIsOffensed.SelectedValue),
                                                                                            txtOffensedDeti.Text, Generic.ToInt(rblIsCourt.SelectedValue),
                                                                                            txtCourtDeti.Text, Generic.ToInt(rblIsSector.SelectedValue),
                                                                                            txtSectorDeti.Text, Generic.ToInt(rblIsCandidate.SelectedValue),
                                                                                            txtCandidateDeti.Text, Generic.ToInt(rblIsIndigenGrp.SelectedValue),
                                                                                            txtIndigenGrpDeti.Text, Generic.ToInt(rblIsAbled.SelectedValue),
                                                                                            txtAbledDeti.Text, Generic.ToInt(rblIsSoloParent.SelectedValue),
                                                                                            txtSoloParentDeti.Text, Generic.ToInt(rblIsGuilty.SelectedValue),
                                                                                            txtGuiltyDeti.Text, 0,
                                                                                            "", txtGSISNo.Text,
                                                                                            txtPHNo.Text, txtHDMFNo.Text,
                                                                                            txtTINNo.Text, txtSSSNo.Text,
                                                                                            txtCTCN.Text, txtCTCNIssuedAt.Text,
                                                                                            txtCTCNIssuedOn.Text, txtHeight.Text,
                                                                                            txtWeight.Text, Generic.ToInt(cboTaxExemptNo.SelectedValue),
                                                                                            Generic.ToInt(cboShoeNo.SelectedValue), Generic.ToInt(cboTShirtNo.SelectedValue),
                                                                                            Generic.ToInt(rblIsResigned.SelectedValue), txtResignedDeti.Text, txtSoloParentDetiDate.Text,
                                                                                            txtGovtIssuedID.Text, txtGovtIssuedIDNo.Text, txtGovtIssueDatePlace.Text,
                                                                                            Generic.ToInt(rblIsConsanguinity.SelectedValue), txtConsanguinityDeti.Text,
                                                                                            Generic.ToInt(rblIsAffinity.SelectedValue), txtAffinityDeti.Text,
                                                                                            Generic.ToInt(rblIsOtherRelative.SelectedValue), txtOtherRelativeDeti.Text,
                                                                                            Generic.ToInt(rblIsFormer.SelectedValue), txtFormerDeti.Text,
                                                                                            Generic.ToInt(rblIsRespondent.SelectedValue), txtRespondentDeti.Text,
                                                                                            Generic.ToInt(chkIsOngoingA.Checked), Generic.ToInt(chkIsOngoingC.Checked),
                                                                                            0, 0,
                                                                                            Generic.ToInt(chkIsDismissedA.Checked), Generic.ToInt(chkIsDismissedC.Checked),
                                                                                            Generic.ToInt(chkIsHypertension.Checked), Generic.ToInt(chkIsDiabetes.Checked),
                                                                                            Generic.ToInt(chkIsAcquiredHeartDisease.Checked), Generic.ToInt(chkIsKidneyDisease.Checked),
                                                                                            Generic.ToInt(chkIsTuberculosis.Checked), Generic.ToInt(chkIsChronicPumonary.Checked),
                                                                                            Generic.ToInt(chkIsMalignancies.Checked), Generic.ToInt(chkIsAutoimmune.Checked),
                                                                                            Generic.ToInt(chkIsCardiovascularAccident.Checked), Generic.ToInt(chkIsNeuroPsychiatric.Checked),
                                                                                            Generic.ToInt(chkIsHematologic.Checked), Generic.ToInt(chkIsChronicLiver.Checked), Generic.ToInt(chkIsMajorcongenital.Checked),
                                                                                            Generic.ToInt(chkIsOthers.Checked), txtOtherDeti.Text, Generic.ToInt(rblIsAssigned.SelectedValue), txtChargedDetiDate.Text, txtChargedDetiStatus.Text)
        Dim json As String = JsonConvert.SerializeObject(dt1)
        Try
            Dim factory As New RestSharpClientFactory()
            Dim client As RestClient = factory.GetClient()

            Dim request As New RestRequest("api/push/onejsondata", Method.Post)
            request.AddBody(New With {
                .totalRows = 1,
                    .hasMore = False,
                    .content = json,
                    .tableName = "EApplicantOther"
                })

            Dim response As RestResponse = client.Execute(request)
            If response.IsSuccessful Then
                Dim jsonData = JsonConvert.DeserializeObject(Of APIStatus)(response.Content)
                Dim arr As JArray = JArray.Parse(json)
                arr(0)("ApplicantNo") = jsonData.Id
                json = arr.ToString(Newtonsoft.Json.Formatting.None)
                SQLHelper.ExecuteNonQuery("EJSONMain_WebSave", json, "EApplicantOther")
                Return True
            Else
                Return False
                error_message = "Unable to save record in career portal server."
            End If
        Catch ex As Exception
            error_message = ex.Message
        End Try

    End Function

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If SaveRecord() Then
            'MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Dim url As String = "AppInfoEdit.aspx?id=" & TransNo
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
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

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            ViewState("IsEnabled") = True
            EnabledControls()
            'EnabledYesBox()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    'Private Sub EnabledYesBox()
    '    If Generic.ToBol(ViewState("IsEnabled")) = True And rblIsGuilty.SelectedValue = "1" Then
    '        txtGuiltyDeti.Enabled = True
    '    Else
    '        txtGuiltyDeti.Enabled = False
    '        txtGuiltyDeti.Text = ""
    '    End If
    '    If Generic.ToBol(ViewState("IsEnabled")) = True And rblIsSuspended.SelectedValue = "1" Then
    '        txtSuspendedDeti.Enabled = True
    '    Else
    '        txtSuspendedDeti.Enabled = False
    '        txtSuspendedDeti.Text = ""
    '    End If
    'End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicantTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=1"

    End Sub

End Class
