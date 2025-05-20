Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web


Partial Class Secured_CliClinicConsultList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Int64 = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            populateCombo()
        End If

        PopulateGrid()
        PopulateTabHeader()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EClinicConsult_Web", UserNo, TransNo, Generic.ToBol(Session("IsDependent")))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinic_WebTabHeader", UserNo, TransNo, Generic.ToBol(Session("IsDependent")))
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next

        If Generic.ToBol(Session("IsDependent")) = False Then
            imgPhoto.Visible = True
            imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
        Else
            imgPhoto.Visible = False
        End If

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ClinicConsultNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EClinicConsult", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Generic.ClearControls(Me, "Panel1")
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateData(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ClinicConsultNo"}))
        mdlMain.Show()
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub
    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Private Function SaveRecord() As Boolean

        Dim ClinicConsultCode As Integer = Generic.ToInt(Me.txtClinicConsultCode.Text)
        Dim Weight As String = Generic.ToStr(Me.txtWeight.Text)
        Dim Height As String = Generic.ToStr(Me.txtHeight.Text)
        Dim MedicalConditions As String = Generic.ToStr(Me.txtMedicalConditions.Text)
        Dim ClinicConsultTypeNo As Integer = Generic.ToInt(Me.cboClinicConsultTypeNo.SelectedValue)
        Dim DateReg As String = Generic.ToStr(Me.txtDateReg.Text)
        Dim TimeReg As String = Generic.ToStr(Me.txtTimeReg.Text)
        Dim Complaints As String = Generic.ToStr(Me.txtComplaints.Text)
        Dim Diagnosis As String = Generic.ToStr(Me.txtDiagnosis.Text)
        Dim Prescription As String = Generic.ToStr(Me.txtPrescription.Text)
        Dim Treatment As String = Generic.ToStr(Me.txtTreatment.Text)
        Dim Recommendation As String = Generic.ToStr(Me.txtRecommendation.Text)
        Dim RecommendationNo As Integer = Generic.ToInt(Me.cboRecommendationNo.SelectedValue)
        Dim NextSchedDate As String = Generic.ToStr(Me.txtNextSchedDate.Text)
        Dim NextSchedTime As String = Generic.ToStr(Me.txtNextSchedTime.Text)
        Dim DoctorNo As Integer = Generic.ToInt(Me.cboDoctorNo.SelectedValue)
        Dim DoctorName As String = Generic.ToStr(Me.txtDoctorName.Text)
        Dim PlaceOccured As String = Generic.ToStr(Me.txtPlaceOccured.Text)
        Dim Assessment As String = Generic.ToStr(Me.txtAssessment.Text)
        Dim IsDependent As String = Generic.ToBol(Session("IsDependent"))
        Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)
        Dim ClinicStatNo As Integer = Generic.ToInt(Me.cboClinicStatNo.SelectedValue)
        Dim StartDate As String = Generic.ToStr(txtStartDate.Text)
        Dim EndDate As String = Generic.ToStr(txtEndDate.Text)
        Dim IsFollowup As Boolean = Generic.ToBol(chkIsFollowup.Checked)
        Dim IsFitToWork As Boolean = Generic.ToBol(chkIsFitToWork.Checked)
        Dim PatientNo As String = Generic.ToStr(Me.txtPatientNo.Text)

        If SQLHelper.ExecuteNonQuery("EClinicConsult_WebSave", UserNo, ClinicConsultCode, TransNo, Weight, Height, MedicalConditions, ClinicConsultTypeNo, DateReg, TimeReg, Complaints, Diagnosis, Prescription, Treatment, Recommendation, RecommendationNo, NextSchedDate, NextSchedTime, DoctorNo, DoctorName, PlaceOccured, Assessment, IsDependent, Remarks, ClinicStatNo, StartDate, EndDate, IsFollowup, IsFitToWork, PatientNo) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If

    End Function

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinicConsult_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel1", dt)
    End Sub

    Private Sub populateCombo()
        Try
            cboDoctorNo.DataSource = SQLHelper.ExecuteDataSet("EDoctor_WebLookUp", UserNo, PayLocNo)
            cboDoctorNo.DataValueField = "tNo"
            cboDoctorNo.DataTextField = "tDesc"
            cboDoctorNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub
End Class
