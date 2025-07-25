﻿Imports clsLib
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports System.Data

Partial Class Secured_AppEducEdit
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EApplicantEduc_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EApplicantEduc_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
                ViewState("CourseNo") = Generic.ToInt(row("CourseNo"))
                ViewState("SchoolDetiNo") = Generic.ToInt(row("SchoolDetiNo"))
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
            ModalPopupExtender1.Show()
            'Dim json As String = "[{""ApplicantNo"":1,""EmployeeNo"":0,""CityNo"":0,""SchoolNo"":97,""CourseNo"":412,""EducLevelNo"":3,""FromDate"":"""",""ToDate"":"""",""FromMonth"":6,""FromDay"":1,""FromYear"":1997,""ToMonth"":3,""ToDay"":31,""ToYear"":2001,""UnitEarned"":0.0,""IsGraduated"":0,""HonorNo"":0,""YearGrad"":0,""OtherCourse"":"""",""EncodeNo"":1,""EncodeDate"":""2025-07-03T19:13:45.303"",""HonorDate"":null,""NonHonor"":"""",""NonHonorDate"":null,""FieldOfStudy"":"""",""Address"":"""",""IsUpdated"":0,""IsDeleted"":0,""IsUsed"":0,""PayLocNo"":0,""OtherSchool"":"""",""OtherEducLevel"":"""",""IsHighest"":false,""IsOtherSchool"":false,""IsOtherCourse"":false,""FieldOfStudyNo"":0,""Scholarship"":"""",""SchoolDetiNo"":0,""IsPresent"":0,""IsCareer"":1,""CareerDate"":""2025-07-03T19:13:45.303"",""IsApplyToAll"":0,""Remarks"":"""",""IsFixed"":0,""EffectiveDate"":null,""DocRef"":""""}]"
            'Dim factory As New RestSharpClientFactory()
            'Dim client As RestClient = factory.GetClient()

            'Dim request As New RestRequest("api/push/onejsondata", Method.Get)
            'request.AddJsonBody(New With {
            '        .totalRows = 1,
            '        .hasMore = False,
            '        .content = json,
            '        .tableName = "EApplicantEduc"
            '    })
            'Dim response As RestResponse = client.Execute(request)
            'If response.IsSuccessful Then
            '    Dim result As String = response.Content
            'End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim ApplicantEducNo As Integer = Generic.ToInt(Me.txtApplicantEducNo.Text)
        Dim EducLevelNo As Integer = Generic.ToInt(Me.cboEducLevelNo.SelectedValue)
        Dim CourseNo As Integer = Generic.ToInt(Me.cboCourseNo.SelectedValue)
        Dim SchoolNo As Integer = Generic.ToInt(Me.cboSchoolNo.SelectedValue)
        Dim HonorNo As Integer = Generic.ToInt(Me.cboHonorNo.SelectedValue)
        Dim UnitEarned As Double = Generic.ToDec(Me.txtUnitEarned.Text)
        Dim YearGrad As Integer = Generic.ToInt(Me.txtYearGrad.Text)

        Dim FromMonth As Integer = Generic.ToInt(Me.cboFromMonth.SelectedValue)
        Dim FromYear As Integer = Generic.ToInt(Me.txtFromYear.Text)
        Dim ToMonth As Integer = Generic.ToInt(Me.cboToMonth.SelectedValue)
        Dim ToYear As Integer = Generic.ToInt(Me.txtToYear.Text)
        Dim IsOtherSchool As Boolean = Generic.ToBol(txtIsOtherSchool.Checked)
        Dim OtherSchool As String = Generic.ToStr(txtOtherSchool.Text)
        Dim IsOtherCourse As Boolean = Generic.ToBol(txtIsOtherCourse.Checked)
        Dim OtherCourse As String = Generic.ToStr(txtOtherCourse.Text)
        Dim FromDay As Integer = Generic.ToInt(cboFromDay.SelectedValue)
        Dim ToDay As Integer = Generic.ToInt(cboToDay.SelectedValue)
        Dim IsHighest As Boolean = 0
        Dim FieldOfStudyNo As Integer = Generic.ToInt(cboFieldOfStudyNo.SelectedValue)

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EApplicantEduc_WebValidate", UserNo, ApplicantEducNo, TransNo, EducLevelNo, txtFieldOfStudy.Text, CourseNo, SchoolNo, txtSchoolAddress.Text, FromMonth, FromYear, ToMonth, ToYear, YearGrad, UnitEarned, HonorNo, IsOtherSchool, OtherSchool, IsOtherCourse, OtherCourse, FromDay, ToDay, IsHighest)

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

        'If SQLHelper.ExecuteNonQuery("EApplicantEduc_WebSave", TransNo, ApplicantEducNo, TransNo, EducLevelNo, txtFieldOfStudy.Text, CourseNo, SchoolNo, txtSchoolAddress.Text, FromMonth, FromYear, ToMonth, ToYear, YearGrad, UnitEarned, HonorNo, IsOtherSchool, OtherSchool, IsOtherCourse, OtherCourse, FromDay, ToDay, IsHighest, FieldOfStudyNo, txtScholarship.Text, Generic.ToInt(cboSchoolDetiNo.SelectedValue)) > 0 Then
        '    RetVal = True
        'Else
        '    RetVal = False
        'End If

        'Saving through web api
        Dim dt1 As DataTable = SQLHelper.ExecuteDataTable("EApplicantEduc_WebSave", TransNo, ApplicantEducNo, TransNo, EducLevelNo, txtFieldOfStudy.Text, CourseNo, SchoolNo, txtSchoolAddress.Text, FromMonth, FromYear, ToMonth, ToYear, YearGrad, UnitEarned, HonorNo, IsOtherSchool, OtherSchool, IsOtherCourse, OtherCourse, FromDay, ToDay, IsHighest, FieldOfStudyNo, txtScholarship.Text, Generic.ToInt(cboSchoolDetiNo.SelectedValue))
        Dim json As String = JsonConvert.SerializeObject(dt1)
        Try
            Dim factory As New RestSharpClientFactory()
            Dim client As RestClient = factory.GetClient()

            Dim request As New RestRequest("api/push/onejsondata", Method.Post)
            request.AddBody(New With {
                .totalRows = 1,
                    .hasMore = False,
                    .content = json,
                    .tableName = "EApplicantEduc"
                })

            Dim response As RestResponse = client.Execute(request)
            If response.IsSuccessful Then
                Dim jsonData = JsonConvert.DeserializeObject(Of APIStatus)(response.Content)
                Dim arr As JArray = JArray.Parse(json)
                arr(0)("ApplicantEducNo") = jsonData.Id
                json = arr.ToString(Newtonsoft.Json.Formatting.None)
                SQLHelper.ExecuteNonQuery("EJSONMain_WebSave", json, "EApplicantEduc")
                RetVal = True
            Else
                RetVal = False
                error_message = "Unable to save record in career portal server."
            End If
        Catch ex As Exception
            error_message = ex.Message
        End Try

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(error_message, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Generic.ClearControls(Me, "Panel1")
            PopulateData(Generic.ToInt(lnk.CommandArgument))
            PopulateControls()
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ApplicantEducNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EApplicantEduc", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Sub PopulateControls()

        Try
            EEducLevelSchool()
            PopulateSchoolCampus()
        Catch ex As Exception

        End Try
        'If txtIsOtherSchool.Checked = True Then
        '    txtOtherSchool.Enabled = True
        '    txtOtherSchool.CssClass = "form-control required"
        '    cboSchoolNo.CssClass = "form-control"
        '    cboSchoolNo.Enabled = False
        '    cboSchoolNo.Text = ""
        '    lblSchool.Attributes.Add("class", "col-md-4 control-label has-space")
        'Else
        '    txtOtherSchool.Enabled = False
        '    txtOtherSchool.Text = ""
        '    txtOtherSchool.CssClass = "form-control"
        '    cboSchoolNo.CssClass = "form-control required"
        '    cboSchoolNo.Enabled = True
        '    lblSchool.Attributes.Add("class", "col-md-4 control-label has-required")
        'End If

        'If txtIsOtherCourse.Checked = True Then
        '    txtOtherCourse.Enabled = True
        '    cboCourseNo.Enabled = False
        '    cboCourseNo.Text = ""       
        '    lblEduc.Attributes.Add("class", "col-md-4 control-label has-space")
        '    cboCourseNo.CssClass = "form-control"
        '    txtOtherCourse.CssClass = "form-control required"
        'Else
        '    txtOtherCourse.Enabled = False
        '    txtOtherCourse.Text = ""
        '    cboCourseNo.Enabled = True
        '    lblEduc.Attributes.Add("class", "col-md-4 control-label has-required")
        '    cboCourseNo.CssClass = "form-control required"
        '    txtOtherCourse.CssClass = "form-control"            
        'End If

        'If cboEducLevelNo.SelectedItem.Text = "Elementary" Then
        '    cboCourseNo.Enabled = False
        '    cboCourseNo.SelectedItem.Text = "PRIMARY EDUCATION"
        'End If

        'If cboEducLevelNo.SelectedItem.Text = "Secondary" Then
        '    cboCourseNo.Enabled = False
        '    cboCourseNo.SelectedItem.Text = "SECONDARY EDUCATION"
        'End If

    End Sub

    Protected Sub txtIsOtherSchool_CheckedChanged(sender As Object, e As System.EventArgs) Handles txtIsOtherSchool.CheckedChanged
        PopulateControls()
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub txtIsOtherCourse_CheckedChanged(sender As Object, e As System.EventArgs) Handles txtIsOtherCourse.CheckedChanged
        PopulateControls()
        ModalPopupExtender1.Show()
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

    Protected Sub cboEducLevelNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            EEducLevelSchool()
            If cboEducLevelNo.SelectedItem.Text.Contains("Elementary") Or cboEducLevelNo.SelectedItem.Text.Contains("Secondary") Or Generic.ToInt(cboEducLevelNo.SelectedValue) = 0 Then
                'txtCourseDesc.Enabled = False
                'cboCourseNo.Enabled = False
                'txtIsOtherCourse.Enabled = False
                'txtIsOtherCourse.Checked = False
                'txtOtherCourse.Enabled = False
                'txtCourseDesc.Text = ""
                'cboCourseNo.Text = ""
                'txtOtherCourse.Text = ""
                'txtCourseDesc.CssClass = "form-control"
                'cboCourseNo.CssClass = "form-control"
                'Else
                '    'txtCourseDesc.Enabled = True
                '    cboCourseNo.Enabled = True
                '    txtIsOtherCourse.Enabled = True
                '    txtOtherCourse.Enabled = txtIsOtherCourse.Checked
                '    'txtCourseDesc.CssClass = "form-control required"
                '    cboCourseNo.CssClass = "form-control required"
                cboFieldOfStudyNo.Enabled = False
                cboFieldOfStudyNo.Text = ""
            Else
                'cboCourseNo.Enabled = True
                'cboCourseNo.Enabled = True
                'txtIsOtherCourse.Enabled = True
                'txtIsOtherCourse.Checked = True
                'txtOtherCourse.Enabled = True
                cboFieldOfStudyNo.Enabled = True
            End If

            'cboCourseNo.ClearSelection()
            'Try
            '    If cboEducLevelNo.SelectedItem.Text = "Elementary" Then
            '        cboCourseNo.Items.FindByText("PRIMARY EDUCATION").Selected = True
            '        cboCourseNo.Enabled = False
            '    End If
            'Catch ex As Exception

            'End Try
            'Try
            '    If cboEducLevelNo.SelectedItem.Text = "Secondary" Then
            '        cboCourseNo.Items.FindByText("SECONDARY EDUCATION").Selected = True
            '        cboCourseNo.Enabled = False
            '    End If
            'Catch ex As Exception
            'End Try

            Try
                cboCourseNo.DataSource = SQLHelper.ExecuteDataSet("ECourse_WebLookup", UserNo, Generic.ToInt(Session("xPayLocNo")), Generic.ToInt(cboEducLevelNo.SelectedValue))
                cboCourseNo.DataValueField = "tNo"
                cboCourseNo.DataTextField = "tDesc"
                cboCourseNo.DataBind()
            Catch ex As Exception
            End Try

        Catch ex As Exception

        End Try

        ModalPopupExtender1.Show()

    End Sub

    Protected Sub txtYearGrad_TextChanged(sender As Object, e As System.EventArgs) 'Handles txtYearGrad.TextChanged
        If Generic.ToInt(Me.txtYearGrad.Text) > 0 Then
            GetDefaultCourseTitle(Generic.ToInt(Me.cboEducLevelNo.SelectedValue))
        Else
            'Me.txtCourseDesc.Text = ""
            'Me.hifCourseNo.Value = 0
            Me.cboCourseNo.Text = ""
        End If

        ModalPopupExtender1.Show()
    End Sub

    Protected Sub GetDefaultCourseTitle(ByVal EducLevelNo As Short)
        Try
            Dim ds As DataSet
            If EducLevelNo > 0 Then
                ds = SQLHelper.ExecuteDataSet("Select Top 1 CourseViewNo,CourseViewDesc From dbo.ECourseView where EducLevelNo=" & EducLevelNo.ToString)
                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        'Me.txtCourseDesc.Text = Generic.ToStr(ds.Tables(0).Rows(0)("CourseViewDesc"))
                        'Me.hifCourseNo.Value = Generic.ToInt(ds.Tables(0).Rows(0)("CourseViewNo"))
                        Me.cboCourseNo.Text = Generic.ToInt(ds.Tables(0).Rows(0)("CourseViewNo"))
                    End If
                End If
            Else

            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub txtToYear_TextChanged(sender As Object, e As System.EventArgs)
        If Generic.ToStr(txtToYear.Text) <> "" Then
            txtYearGrad.Text = Generic.ToStr(txtToYear.Text)
        Else
            txtYearGrad.Text = ""
        End If

        ModalPopupExtender1.Show()
    End Sub

    Protected Sub cboSchoolNo_SelectedIndexChanged(sender As Object, e As EventArgs)

        PopulateSchoolCampus()
        ModalPopupExtender1.Show()
    End Sub

    Private Sub EEducLevelSchool()

        'School
        Dim obj As Object
        obj = SQLHelper.ExecuteScalar("SELECT ISNULL(IsFreeText,0) FROM EEducLevel WHERE EducLevelNo=" & cboEducLevelNo.SelectedValue)
        If Generic.ToInt(obj) = 0 Then
            cboSchoolNo.Visible = True
            txtOtherSchool.Visible = False
            cboSchoolNo.CssClass = "form-control required"
            txtOtherSchool.CssClass = "form-control"
        Else
            cboSchoolNo.Visible = False
            txtOtherSchool.Visible = True
            cboSchoolNo.CssClass = "form-control"
            txtOtherSchool.CssClass = "form-control required"
            cboSchoolNo.SelectedValue = ""
        End If

        Try
            cboCourseNo.DataSource = SQLHelper.ExecuteDataSet("ECourse_WebLookup", UserNo, Generic.ToInt(cboEducLevelNo.SelectedValue))
            cboCourseNo.DataValueField = "tNo"
            cboCourseNo.DataTextField = "tDesc"
            cboCourseNo.DataBind()
            If Generic.ToInt(ViewState("CourseNo")) = 0 Then
                ViewState("CourseNo") = ""
            End If
            cboCourseNo.SelectedValue = Generic.ToStr(ViewState("CourseNo"))
        Catch ex As Exception
        End Try

        'Course

    End Sub

    Private Sub PopulateSchoolCampus()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ESchoolDeti_WebLookup", UserNo, Generic.ToInt(cboSchoolNo.SelectedValue))
        If Generic.ToInt(dt.Rows.Count) > 1 Then
            divSchoolCampus.Visible = True
            cboSchoolDetiNo.CssClass = "form-control required"
            Try
                cboSchoolDetiNo.DataSource = dt
                cboSchoolDetiNo.DataValueField = "tNo"
                cboSchoolDetiNo.DataTextField = "tDesc"
                cboSchoolDetiNo.DataBind()
            Catch ex As Exception
            End Try
            If Generic.ToInt(ViewState("SchoolDetiNo")) = 0 Then
                ViewState("SchoolDetiNo") = ""
            End If
            cboSchoolDetiNo.SelectedValue = Generic.ToStr(ViewState("SchoolDetiNo"))
        Else
            divSchoolCampus.Visible = False
            cboSchoolDetiNo.SelectedValue = ""
            cboSchoolDetiNo.CssClass = "form-control"
        End If
    End Sub

End Class
