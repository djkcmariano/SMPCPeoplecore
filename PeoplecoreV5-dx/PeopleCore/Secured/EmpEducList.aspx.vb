Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_EmpEducList
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEduc_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEduc_WebOne", UserNo, id)
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
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
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
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            PopulateControls()            
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        'If SaveRecord() Then
        '    MessageBox.Success(MessageTemplate.SuccessSave, Me)
        '    PopulateGrid()
        'Else
        '    MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        'End If
        Dim EmployeeEducNo As Integer = Generic.ToInt(Me.txtCode.Text)
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

        Dim RetVal As Boolean = False
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EEmployeeEduc_WebValidate", UserNo, EmployeeEducNo, TransNo, EducLevelNo, CourseNo, txtFieldOfStudy.Text, SchoolNo, txtAddress.Text, FromMonth, FromYear, ToMonth, ToYear, YearGrad, UnitEarned, HonorNo, IsOtherSchool, OtherSchool, IsOtherCourse, OtherCourse, FromDay, ToDay, IsHighest)

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

        If SQLHelper.ExecuteNonQuery("EEmployeeEduc_WebSave", UserNo, EmployeeEducNo, TransNo, EducLevelNo, CourseNo, txtFieldOfStudy.Text, SchoolNo, txtAddress.Text, FromMonth, FromYear, ToMonth, ToYear, YearGrad, UnitEarned, HonorNo, IsOtherSchool, OtherSchool, IsOtherCourse, OtherCourse, FromDay, ToDay, IsHighest, txtIndustry.Text, Generic.ToInt(cboFieldOfStudyNo.SelectedValue), txtScholarship.Text, Generic.ToInt(cboSchoolDetiNo.SelectedValue)) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Generic.ClearControls(Me, "Panel1")
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)            
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeEducNo"})))
            PopulateControls()
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeEducNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeEduc", UserNo, item)
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

            'Try
            '    If cboEducLevelNo.SelectedItem.Text.Contains("Elementary") Then
            '        cboCourseNo.Enabled = False
            '        cboCourseNo.SelectedItem.Text = "PRIMARY EDUCATION"
            '        cboFieldOfStudyNo.Enabled = False
            '    End If
            'Catch ex As Exception

            'End Try

            'Try
            '    If cboEducLevelNo.SelectedItem.Text.Contains("Secondary") Then
            '        cboCourseNo.Enabled = False
            '        cboCourseNo.SelectedItem.Text = "SECONDARY EDUCATION"
            '        cboFieldOfStudyNo.Enabled = False
            '    End If
            'Catch ex As Exception

            'End Try

        Catch ex As Exception

        End Try

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
                cboCourseNo.DataSource = SQLHelper.ExecuteDataSet("ECourse_WebLookup", UserNo, PayLocNo, Generic.ToInt(cboEducLevelNo.SelectedValue))
                cboCourseNo.DataValueField = "tNo"
                cboCourseNo.DataTextField = "tDesc"
                cboCourseNo.DataBind()
            Catch ex As Exception
            End Try

        Catch ex As Exception

        End Try

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

            divSchoolCampus.Visible = False
            cboSchoolDetiNo.CssClass = "form-control"
            cboSchoolDetiNo.SelectedValue = ""
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

    Protected Sub cboSchoolNo_SelectedIndexChanged(sender As Object, e As EventArgs)

        PopulateSchoolCampus()
        ModalPopupExtender1.Show()
    End Sub

End Class



