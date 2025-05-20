Imports System.Data
Imports Microsoft.VisualBasic
Imports clsLib

Partial Class Secured_PayPreviousEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim IsEnabled As Boolean = False

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinicAcci_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            PopulateClinicAcciType(Generic.ToInt(row("ClinicAcciCategoryNo")))
            Generic.PopulateData(Me, "Panel1", dt)
        Next

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "CliAccidentList.aspx", "EClinicAcci")
        If TransNo = 0 Then : ViewState("IsEnabled") = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateData()
        End If
        EnabledControls()
    End Sub

    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "CliAccidentList.aspx", "EClinicAcci") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)        
        btnModify.Visible = Not IsEnabled
        btnSave.Visible = IsEnabled        
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        TransNo = SaveRecord()        
        If Generic.ToInt(Request.QueryString("id")) = 0 Then
            Dim xURL As String = "CliAccidentList.aspx?id=" & TransNo
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, xURL)
        Else
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            ViewState("IsEnabled") = False
            EnabledControls()
        End If
        PopulateData()
    End Sub

    Private Function SaveRecord() As Integer
        Dim ClinicAcciCategoryNo As Integer = Generic.ToInt(Me.cboClinicAcciCategoryNo.SelectedValue)
        Dim ClinicAcciTypeNo As Integer = Generic.ToInt(Me.cboClinicAcciTypeNo.SelectedValue)
        Dim EmployeeNo As Integer = Generic.ToInt(Me.hifEmployeeNo.Value)
        Dim ReportByNo As Integer = Generic.ToInt(Me.hifReportByNo.Value)
        Dim AccidentRemarks As String = Generic.ToStr(Me.txtAccidentRemarks.Text)
        Dim DateOccured As String = Generic.ToStr(Me.txtDateOccured.Text)
        Dim TimeOccured As String = Generic.ToStr(Me.txtTimeOccured.Text)
        Dim DateReported As String = Generic.ToStr(Me.txtDateReported.Text)
        Dim InvistigatedBy As String = Generic.ToStr(Me.txtInvistigatedBy.Text)
        Dim InvestigationResult As String = Generic.ToStr(Me.txtInvestigationResult.Text)
        Dim Recommendation As String = Generic.ToStr(Me.txtRecommendation.Text)
        Dim Resolution As String = Generic.ToStr(Me.txtResolution.Text)
        Dim PlaceOccured As String = Generic.ToStr(Me.txtPlaceOccured.Text)
        Dim Assessment As String = Generic.ToStr(Me.txtAssessment.Text)
        Dim Treatment As String = Generic.ToStr(Me.txtTreatment.Text)
        Dim ClinicAcciStatNo As Integer = Generic.ToInt(Me.cboClinicAcciStatNo.SelectedValue)
        Dim ClinicCauseOfDeathNo As Integer = Generic.ToInt(Me.cboClinicCauseOfDeathNo.SelectedValue)
        Dim obj As Object
        obj = SQLHelper.ExecuteScalar("EClinicAcci_WebSave", UserNo, TransNo, ClinicAcciCategoryNo, ClinicAcciTypeNo, EmployeeNo, ReportByNo, AccidentRemarks, DateOccured, TimeOccured, DateReported, InvistigatedBy, InvestigationResult, Recommendation, Resolution, PlaceOccured, Assessment, Treatment, ClinicAcciStatNo, PayLocNo, ClinicCauseOfDeathNo)
        Return Generic.ToInt(obj)
    End Function

    Protected Sub ClinicAcciCategory_ValueChanged(sender As Object, e As System.EventArgs)
        Try
            PopulateClinicAcciType(Generic.ToInt(Me.cboClinicAcciCategoryNo.SelectedValue))
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PopulateClinicAcciType(tno As Integer)
        Try
            cboClinicAcciTypeNo.DataSource = SQLHelper.ExecuteDataSet("EClinicAcciType_WebLookup", UserNo, tno)
            cboClinicAcciTypeNo.DataTextField = "tDesc"
            cboClinicAcciTypeNo.DataValueField = "tNo"
            cboClinicAcciTypeNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

End Class




