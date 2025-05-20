Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_AppRandomAnsList_QS
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer
    Dim MRNo As Integer
    Dim xID As Integer
    Dim IsApplicant As Boolean



    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        MRNo = Generic.ToInt(Request.QueryString("mrno"))
        xID = Generic.ToInt(Request.QueryString("xid"))
        IsApplicant = Generic.ToBol(Request.QueryString("IsApplicant"))
        AccessRights.CheckUser(UserNo, "AppRandomAnsList.aspx", "EApplicantRandomAns")
        If Not IsPostBack Then

            lblPositionDesc.Text = Generic.ToStr(SQLHelper.ExecuteScalar("SELECT PositionDesc FROM EMR a LEFT OUTER JOIN EPosition b ON b.PositionNo=a.PositionNo WHERE MRNo=" & MRNo))

            If IsApplicant = True Then
                lbl.Text = Generic.ToStr(SQLHelper.ExecuteScalar("SELECT Fullname FROM dbo.EApplicant WHERE ApplicantNo=" & xID))
            Else
                lbl.Text = Generic.ToStr(SQLHelper.ExecuteScalar("SELECT Fullname FROM dbo.EEmployee WHERE EmployeeNo=" & xID))
            End If

            Try
                cboJDKRACritSumNo.DataSource = SQLHelper.ExecuteDataTable("EJDKRACrit_WebLookup", UserNo, MRNo)
                cboJDKRACritSumNo.DataValueField = "tNo"
                cboJDKRACritSumNo.DataTextField = "tDesc"
                cboJDKRACritSumNo.DataBind()
            Catch ex As Exception
            End Try

            Try
                cboJDKRACritNo.DataSource = SQLHelper.ExecuteDataTable("EJDKRACrit_WebLookup", UserNo, MRNo)
                cboJDKRACritNo.DataValueField = "tNo"
                cboJDKRACritNo.DataTextField = "tDesc"
                cboJDKRACritNo.DataBind()
            Catch ex As Exception
            End Try

            Generic.PopulateDropDownList(UserNo, Me, "Panel7", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "Panel9", PayLocNo)

            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EGetQS", UserNo, MRNo)

            rEduc.DataSource = ds.Tables(0)
            rEduc.DataBind()

            rExpe.DataSource = ds.Tables(1)
            rExpe.DataBind()

            rTrn.DataSource = ds.Tables(2)
            rTrn.DataBind()

            rExam.DataSource = ds.Tables(3)
            rExam.DataBind()

        End If

        PopulateEduc()
        PopulateExam()
        PopulateTrn()
        PopulateExpe()
        PopulatePerf()
        PopulateComp()

        txtCompDesc.Enabled = False
        txtCompScaleDesc.Enabled = False
        txtCompScaleSelfDesc.Enabled = False

    End Sub

    Protected Sub lnkAttachment_Click(sender As Object, e As EventArgs)
        FileUpload.xID = xID
        FileUpload.xModify = False
        If IsApplicant Then
            FileUpload.xMenuType = "0101010000"
        Else
            FileUpload.xMenuType = "0201010000"
        End If

        FileUpload.Show()
    End Sub

#Region "Education"

    Private Sub PopulateEduc()
        grdAssessEduc.DataSource = SQLHelper.ExecuteDataTable("EAssessEduc_Web", UserNo, IsApplicant, xID, MRNo)
        grdAssessEduc.DataBind()
    End Sub

    Protected Sub lnkEducList_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable
        Dim sp As String
        If IsApplicant Then : sp = "EApplicantEduc_Web" : Else : sp = "EEmployeeEduc_Web" : End If
        dt = SQLHelper.ExecuteDataTable(sp, UserNo, xID)
        grdEduc.DataSource = dt
        grdEduc.DataBind()
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnkEducAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel7")
            ModalPopupExtender7.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEducSave_Click(sender As Object, e As EventArgs)
        If SQLHelper.ExecuteNonQuery("EAssessEduc_WebOneSave", UserNo, Generic.ToInt(txtAssessEducCode.Text), TransNo, IsApplicant, xID, MRNo, Generic.ToInt(cboEducLevelNo.SelectedValue), _
                                     Generic.ToInt(cboSchoolNo.SelectedValue), txtOtherSchool.Text, Generic.ToInt(cboCourseNo.SelectedValue), Generic.ToInt(cboSchoolDetiNo.SelectedValue)) > 0 Then

            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateEduc()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEducEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"AssessEducNo"}))
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EAssessEduc_WebOne", UserNo, id)
            Generic.PopulateData(Me, "Panel7", dt)
            ViewState("CourseNo") = Generic.ToInt(dt.Rows(0)("CourseNo"))
            ViewState("SchoolDetiNo") = Generic.ToInt(dt.Rows(0)("SchoolDetiNo"))
            PopulateControls()
            ModalPopupExtender7.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub



    Protected Sub lnkEducDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdAssessEduc.GetSelectedFieldValues(New String() {"AssessEducNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EAssessEduc", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateEduc()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEducValidate_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim fieldValues As List(Of Object) = grdEduc.GetSelectedFieldValues(New String() {"Code"})
            Dim i As Integer = 0
            For Each item As String In fieldValues
                i = i + Generic.ToInt(SQLHelper.ExecuteNonQuery("EAssessEduc_WebSave", UserNo, 0, Generic.ToInt(item), IsApplicant, MRNo))
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessSave, Me)
            grdEduc.Selection.UnselectAll()
            PopulateEduc()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub cboEducLevelNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            EEducLevelSchool()            
            Try
                cboCourseNo.DataSource = SQLHelper.ExecuteDataSet("ECourse_WebLookup", UserNo, Generic.ToInt(cboEducLevelNo.SelectedValue))
                cboCourseNo.DataValueField = "tNo"
                cboCourseNo.DataTextField = "tDesc"
                cboCourseNo.DataBind()
            Catch ex As Exception
            End Try

        Catch ex As Exception

        End Try

        ModalPopupExtender7.Show()

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

    Private Sub PopulateControls()

        Try
            EEducLevelSchool()
            PopulateSchoolCampus()
        Catch ex As Exception

        End Try        
    End Sub

    Protected Sub cboSchoolNo_SelectedIndexChanged(sender As Object, e As EventArgs)

        PopulateSchoolCampus()
        ModalPopupExtender7.Show()
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

#End Region

#Region "Work Experience"
    Private Sub PopulateExpe()
        grdAssessExpe.DataSource = SQLHelper.ExecuteDataTable("EAssessExpe_Web", UserNo, IsApplicant, xID, MRNo)
        grdAssessExpe.DataBind()
        grdAssessExpe.ExpandAll()
    End Sub

    Protected Sub lnkExpeList_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable
        Dim sp As String
        If IsApplicant Then : sp = "EApplicantExpe_Web" : Else : sp = "EEmployeeExpe_Web" : End If
        dt = SQLHelper.ExecuteDataTable(sp, UserNo, xID)
        grdExpe.DataSource = dt
        grdExpe.DataBind()
        ModalPopupExtender4.Show()
    End Sub

    Protected Sub lnkExpeAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel10")
            ModalPopupExtender10.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkExpeEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"AssessExpeNo"}))
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EAssessExpe_WebOne", UserNo, id)
            Generic.PopulateData(Me, "Panel10", dt)            
            ModalPopupExtender10.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkExpeSave_Click(sender As Object, e As EventArgs)
        If SQLHelper.ExecuteNonQuery("EAssessExpe_WebOneSave", UserNo, Generic.ToInt(txtAssessExpeCode.Text), TransNo, IsApplicant, xID, MRNo, Generic.ToInt(cboJDKRACritNo.SelectedValue), _
                                    txtExp_Credited.Text, txtDepartmentDesc.Text, txtPositionDesc.Text, txtDocPresented.Text, txtExpeFromDate.Text, txtExpeToDate.Text) > 0 Then

            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateExpe()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub lnkExpeDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdAssessExpe.GetSelectedFieldValues(New String() {"AssessExpeNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EAssessExpe", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateExpe()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkExpeValidate_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim fieldValues As List(Of Object) = grdExpe.GetSelectedFieldValues(New String() {"Code"})
            Dim i As Integer = 0
            For Each item As String In fieldValues
                i = i + Generic.ToInt(SQLHelper.ExecuteNonQuery("EAssessExpe_WebSave", UserNo, 0, Generic.ToInt(item), IsApplicant, MRNo, Generic.ToInt(cboJDKRACritSumNo.SelectedValue)))
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessSave, Me)
            grdExpe.Selection.UnselectAll()
            PopulateExpe()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub



#End Region

#Region "Training"
    Private Sub PopulateTrn()
        grdAssessTrn.DataSource = SQLHelper.ExecuteDataTable("EAssessTrain_Web", UserNo, IsApplicant, xID, MRNo)
        grdAssessTrn.DataBind()
    End Sub

    Protected Sub lnkTrnList_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable
        Dim sp As String
        If IsApplicant Then : sp = "EApplicantTrain_Web" : Else : sp = "EEmployeeTrain_Web" : End If
        dt = SQLHelper.ExecuteDataTable(sp, UserNo, xID)
        grdTrn.DataSource = dt
        grdTrn.DataBind()
        ModalPopupExtender3.Show()
    End Sub

    Protected Sub lnkTrnAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel8")
            ModalPopupExtender8.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkTrnEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"AssessTrainNo"}))
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EAssessTrain_WebOne", UserNo, id)
            Generic.PopulateData(Me, "Panel8", dt)
            ModalPopupExtender8.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkTrnSave_Click(sender As Object, e As EventArgs)
        If SQLHelper.ExecuteNonQuery("EAssessTrain_WebOneSave", UserNo, Generic.ToInt(txtAccessTrainCode.Text), 0, IsApplicant, xID, MRNo, txtTrainingTitleDesc.Text, txtDateFrom.Text, txtDateTo.Text, Generic.ToDbl(txtNoOfHrs.Text), Generic.ToInt(txtSTTypeNo.Text)) > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateTrn()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub lnkTrnDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdAssessTrn.GetSelectedFieldValues(New String() {"AssessTrainNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EAssessTrain", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateTrn()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkTrnValidate_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim fieldValues As List(Of Object) = grdTrn.GetSelectedFieldValues(New String() {"Code"})
            Dim i As Integer = 0
            For Each item As String In fieldValues
                i = i + Generic.ToInt(SQLHelper.ExecuteNonQuery("EAssessTrain_WebSave", UserNo, 0, Generic.ToInt(item), IsApplicant, MRNo))
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessSave, Me)
            grdTrn.Selection.UnselectAll()
            PopulateTrn()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

#End Region

#Region "Eligibility"
    Private Sub PopulateExam()
        grdAssessExam.DataSource = SQLHelper.ExecuteDataTable("EAssessExam_Web", UserNo, IsApplicant, xID, MRNo)
        grdAssessExam.DataBind()
    End Sub

    Protected Sub lnkExamList_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable
        Dim sp As String
        If IsApplicant Then : sp = "EApplicantExam_Web" : Else : sp = "EEmployeeExam_Web" : End If
        dt = SQLHelper.ExecuteDataTable(sp, UserNo, xID)
        grdExam.DataSource = dt
        grdExam.DataBind()
        ModalPopupExtender2.Show()
    End Sub

    Protected Sub lnkExamAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel9")
            ModalPopupExtender9.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkExamEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"AssessExamNo"}))
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EAssessExam_WebOne", UserNo, id)
            Generic.PopulateData(Me, "Panel9", dt)
            ModalPopupExtender9.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkExamSave_Click(sender As Object, e As EventArgs)
        If SQLHelper.ExecuteNonQuery("EAssessExam_WebOneSave", UserNo, Generic.ToInt(txtAccessExamCode.Text), 0, IsApplicant, xID, MRNo, Generic.ToInt(cboEducLevelNo.SelectedValue), txtDateTaken.Text, txtExpiryDate.Text, txtRatingDesc.Text) > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateExam()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub lnkExamDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdAssessExam.GetSelectedFieldValues(New String() {"AssessExamNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EAssessExam", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateExam()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkExamValidate_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim fieldValues As List(Of Object) = grdExam.GetSelectedFieldValues(New String() {"Code"})
            Dim i As Integer = 0
            For Each item As String In fieldValues
                i = i + Generic.ToInt(SQLHelper.ExecuteNonQuery("EAssessExam_WebSave", UserNo, 0, Generic.ToInt(item), IsApplicant, MRNo))
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessSave, Me)
            grdExam.Selection.UnselectAll()
            PopulateExam()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub



#End Region

#Region "Performance"
    Private Sub PopulatePerf()
        grdAssessPerf.DataSource = SQLHelper.ExecuteDataTable("EAssessPerf_Web", UserNo, IsApplicant, xID, MRNo)
        grdAssessPerf.DataBind()
        grdAssessPerf.ExpandAll()
    End Sub

    Protected Sub lnkPerfList_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable
        Dim sp As String
        If IsApplicant Then : sp = "EApplicantExpe_Web" : Else : sp = "EEmployeeExpe_Web" : End If
        dt = SQLHelper.ExecuteDataTable(sp, UserNo, xID)
        grdExpePerf.DataSource = dt
        grdExpePerf.DataBind()
        ModalPopupExtender5.Show()
    End Sub

    Protected Sub lnkPerfAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel6")
            ModalPopupExtender6.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkPerfEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"AssessPerfNo"}))
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EAssessPerf_WebOne", UserNo, id)
            Generic.PopulateData(Me, "Panel6", dt)
            ModalPopupExtender6.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkPerfSave_Click(sender As Object, e As EventArgs)
        If SQLHelper.ExecuteNonQuery("EAssessPerf_WebOneSave", UserNo, Generic.ToInt(txtAccessPerfCode.Text), 0, IsApplicant, xID, MRNo, txtRatings.Text, Generic.ToDbl(txtRatingsEquiv.Text), txtRemarks.Text) > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulatePerf()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub lnkPerfDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdAssessPerf.GetSelectedFieldValues(New String() {"AssessPerfNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EAssessPerf", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulatePerf()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkPerfValidate_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim fieldValues As List(Of Object) = grdExpePerf.GetSelectedFieldValues(New String() {"Code"})
            Dim i As Integer = 0
            For Each item As String In fieldValues
                i = i + Generic.ToInt(SQLHelper.ExecuteNonQuery("EAssessPerf_WebSave", UserNo, 0, Generic.ToInt(item), IsApplicant, MRNo))
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessSave, Me)
            grdExpePerf.Selection.UnselectAll()
            PopulatePerf()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub



#End Region

#Region "Competency"
    Private Sub PopulateComp()
        grdAssessComp.DataSource = SQLHelper.ExecuteDataTable("EAssessComp_Web", UserNo, IsApplicant, xID, MRNo)
        grdAssessComp.DataBind()
        grdAssessComp.ExpandAll()
    End Sub

    'Protected Sub lnkCompList_Click(sender As Object, e As EventArgs)
    '    Dim dt As DataTable
    '    Dim sp As String
    '    If IsApplicant Then : sp = "EApplicantExpe_Web" : Else : sp = "EEmployeeExpe_Web" : End If
    '    dt = SQLHelper.ExecuteDataTable(sp, UserNo, xID)
    '    grdExpePerf.DataSource = dt
    '    grdExpePerf.DataBind()
    '    ModalPopupExtender5.Show()
    'End Sub

    'Protected Sub lnkCompAdd_Click(sender As Object, e As EventArgs)
    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '        Generic.ClearControls(Me, "Panel6")
    '        ModalPopupExtender6.Show()
    '    Else
    '        MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
    '    End If
    'End Sub

    Protected Sub lnkCompEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton, AssessCompNo As Integer = 0, CompNo As Integer = 0, CompScaleDesc As String = 0, CompScaleSelfDesc As String = 0, CompScaleAssessDesc As String = 0
            lnk = sender                        
            Dim dt As DataTable
            AssessCompNo = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
            CompNo = Generic.ToInt(Generic.Split(lnk.CommandArgument, 1))
            CompScaleDesc = Generic.ToStr(Generic.Split(lnk.CommandArgument, 2))
            CompScaleSelfDesc = Generic.ToStr(Generic.Split(lnk.CommandArgument, 3))
            CompScaleAssessDesc = Generic.ToStr(Generic.Split(lnk.CommandArgument, 4))
            Dim CompDesc As String = Generic.ToStr(Generic.Split(lnk.CommandArgument, 5))
            Dim CompScaleNo As Integer = Generic.ToStr(Generic.Split(lnk.CommandArgument, 6))
            Dim CompScaleSelfNo As Integer = Generic.ToStr(Generic.Split(lnk.CommandArgument, 7))

            dt = SQLHelper.ExecuteDataTable("EAssessComp_WebOne", UserNo, IsApplicant, xID, MRNo, CompNo, PayLocNo)
            If AssessCompNo = 0 Then
                hifCompNo.Value = CompNo
                hifCompScaleNo.Value = CompScaleNo
                hifCompScaleSelfNo.Value = CompScaleSelfNo
                txtCompDesc.Text = CompDesc
                txtCompScaleDesc.Text = CompScaleDesc
                txtCompScaleSelfDesc.Text = CompScaleSelfDesc
            Else
                For Each row As DataRow In dt.Rows
                    CompNo = Generic.ToInt(row("CompNo"))
                    Generic.PopulateData(Me, "pnlPopupComp", dt)
                Next
            End If
            Generic.PopulateData(Me, "Panel12", dt)
            Try
                Me.cboCompScaleAssessNo.DataSource = SQLHelper.ExecuteDataSet("ECompDeti_WebLookup", UserNo, CompNo)
                Me.cboCompScaleAssessNo.DataTextField = "tDesc"
                Me.cboCompScaleAssessNo.DataValueField = "tNo"
                Me.cboCompScaleAssessNo.DataBind()
            Catch ex As Exception
            End Try

            ModalPopupExtender12.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkCompSave_Click(sender As Object, e As EventArgs)
        If SQLHelper.ExecuteNonQuery("EAssessComp_WebOneSave", UserNo, Generic.ToInt(txtAccessCompCode.Text), 0, IsApplicant, xID, MRNo, Generic.ToInt(hifCompNo.Value), Generic.ToInt(hifCompScaleNo.Value), Generic.ToInt(hifCompScaleSelfNo.Value), Generic.ToInt(cboCompScaleAssessNo.SelectedValue), txtAssessCompRemarks.Text) > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateComp()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    'Protected Sub lnkCompDelete_Click(sender As Object, e As EventArgs)
    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
    '        Dim fieldValues As List(Of Object) = grdAssessPerf.GetSelectedFieldValues(New String() {"AssessPerfNo"})
    '        Dim i As Integer = 0
    '        For Each item As Integer In fieldValues
    '            Generic.DeleteRecordAudit("EAssessPerf", UserNo, item)
    '            i = i + 1
    '        Next
    '        If i > 0 Then
    '            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
    '            PopulatePerf()
    '        Else
    '            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
    '        End If

    '    Else
    '        MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
    '    End If
    'End Sub

    'Protected Sub lnkCompValidate_Click(sender As Object, e As EventArgs)
    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '        Dim fieldValues As List(Of Object) = grdExpePerf.GetSelectedFieldValues(New String() {"Code"})
    '        Dim i As Integer = 0
    '        For Each item As String In fieldValues
    '            i = i + Generic.ToInt(SQLHelper.ExecuteNonQuery("EAssessPerf_WebSave", UserNo, 0, Generic.ToInt(item), IsApplicant, MRNo))
    '        Next
    '        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessSave, Me)
    '        grdExpePerf.Selection.UnselectAll()
    '        PopulatePerf()
    '    Else
    '        MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
    '    End If
    'End Sub



#End Region



End Class
