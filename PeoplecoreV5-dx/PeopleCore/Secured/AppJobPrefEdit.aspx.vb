Imports clsLib
Imports System.Data
Partial Class Secured_AppJobPrefEdit
    Inherits System.Web.UI.Page
    Dim TransNo As Int64
    Dim IsEnabled As Boolean = False
    Dim UserNo As Int64    

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If SaveRecord() Then
            'MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Dim url As String = "AppJobPrefEdit.aspx?id=" & TransNo
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    'Private Sub PopulateControls()
    '    If rblIs1.SelectedValue = "1" Then
    '        txtExamDate.Enabled = True
    '        txtVenue.Enabled = True
    '        cboExamStatNo.Enabled = True
    '        txtExamDate.CssClass = "form-control required"
    '        txtVenue.CssClass = "form-control required"
    '        cboExamStatNo.CssClass = "form-control required"
    '    Else
    '        txtExamDate.Enabled = False
    '        txtVenue.Enabled = False
    '        cboExamStatNo.Enabled = False
    '        txtExamDate.CssClass = "form-control"
    '        txtVenue.CssClass = "form-control"
    '        cboExamStatNo.CssClass = "form-control"
    '        txtExamDate.Text = ""
    '        txtVenue.Text = ""
    '        cboExamStatNo.Text = ""
    '    End If

    'End Sub

    Protected Sub rblIs1_SelectedIndexChanged()
        'PopulateControls()
    End Sub

    Protected Function SaveRecord() As Boolean
        Dim IsWT As Integer = IIf(Generic.ToInt(rblIsWT.SelectedValue) = 0, 1, 0)
        Dim IsWTFreq As Integer = IIf(Generic.ToInt(rblIsWT.SelectedValue) = 1, 1, 0)
        Dim IsWTOcc As Integer = IIf(Generic.ToInt(rblIsWT.SelectedValue) = 2, 1, 0)
        Dim DepNo1 As Integer = 0 'Generic.ToInt(Me.cboDepNo1.SelectedValue)
        Dim DepNo2 As Integer = 0 'Generic.ToInt(Me.cboDepNo2.SelectedValue)
        Dim PositionNo As Integer = 0 'Generic.ToInt(cboPositionNo.SelectedValue)
        Dim PositionNo1 As Integer = 0 'Generic.ToInt(cboPositionNo1.SelectedValue)
        Dim StartDate As String = Generic.ToStr(txtAboutToStart.Text)
        Dim SalaryDesired As Double = Generic.ToDbl(Me.txtSalaryDesired.Text)
        Dim xWR As Integer = Generic.ToInt(Me.rblIsWR.SelectedValue)
        Dim xWTY As Integer = IsWT
        Dim xWTF As Integer = IsWTFreq
        Dim xWTO As Integer = IsWTOcc
        Dim xACE As Integer = Generic.ToInt(rblIsACE.SelectedValue)
        Dim x1 As Integer = 0 'Generic.ToInt(rblIs1.SelectedValue)
        Dim ExamDate As String = "" 'txtExamDate.Text
        Dim ExamVenue As String = "" 'txtVenue.Text
        Dim VacancySourceNo As Integer = Generic.ToInt(cboVacancySourceNo.SelectedValue)
        Dim RefereBy As String = txtReferrorName.Text
        Dim xDate As String = Format(Now, "MM/dd/yyyy")
        Dim xA As Integer = 1 'Generic.ToInt(rdoA.SelectedValue)
        Dim MRNo As Integer = Generic.ToInt(Session("MRNo"))
        Dim IndiAccomplishJob As String = Generic.ToStr(txtIndiAccomplishJob.Text)
        Dim Position1 As String = txtPosition1.Text
        Dim Position2 As String = txtPosition2.Text

        If SQLHelper.ExecuteNonQuery("EApplicantCareer_Apl", TransNo, DepNo1, DepNo2, _
                                   PositionNo, PositionNo1, StartDate, _
                                   SalaryDesired, VacancySourceNo, xWR, _
                                   xWTY, xWTF, xWTO, _
                                   xACE, x1, ExamDate, _
                                   ExamVenue, RefereBy, xDate, _
                                   xA, MRNo, IndiAccomplishJob, 0, Position1, Position2, Generic.ToInt(rblIsPreEmpTest.SelectedValue), txtPreEmpTestDate.Text, txtPreEmpTestDeptPos.Text) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub PopulateData()
        Dim dt As DataTable
        Dim IsSubmitted As Boolean = False
        dt = SQLHelper.ExecuteDataTable("EApplicantJobPref_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            'rblIs1.SelectedValue = Generic.ToInt(row("Is1"))
            rblIsACE.SelectedValue = Generic.ToInt(row("IsACE"))
            rblIsWR.SelectedValue = Generic.ToInt(row("IsWR"))
            rblIsWT.SelectedValue = IIf(Generic.ToInt(row("IsWT")) = 1, 0, 0)
            rblIsWT.SelectedValue = IIf(Generic.ToInt(row("IsWTFreq")) = 1, 1, 0)
            rblIsWT.SelectedValue = IIf(Generic.ToInt(row("IsWTOcc")) = 1, 2, 0)
            'lblAppliedDate.Text = "Date Today : " & Generic.ToStr(row("AppliedDate"))
            'If Generic.ToStr(row("AppliedDate")) = "" Then
            '    'lblAppliedDate.Text = "Date Today : " & Format(Now, "MMMM dd, yyyy")
            '    IsSubmitted = False
            'Else
            '    IsSubmitted = True
            'End If
            'rdoA.SelectedValue = Generic.ToInt(row("IsAgree"))
        Next

        'If IsSubmitted = False Then
        '    ViewState("IsEnabled") = True
        'End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))        
        AccessRights.CheckUser(UserNo)
        If TransNo = 0 Then
            IsEnabled = True
        Else
            IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        End If

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", 0)            
            PopulateData()
            PopulateTabHeader()
            lblDisc.Text = Generic.ToStr(SQLHelper.ExecuteScalar("SELECT dbo.EGetDisclaimer()"))
        End If

        EnabledControls()
    End Sub

    Private Sub PopulateDropDown(ddl As DropDownList, tablename As String)
        ddl.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, tablename, 0, "", "")
        ddl.DataTextField = "tdesc"
        ddl.DataValueField = "tNo"
        ddl.DataBind()
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicantTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=1"

    End Sub

    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)

        If IsEnabled = True Then
            'PopulateControls()
        End If

        rblIsPreEmpTest_SelectedIndexChanged()

        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
    End Sub


    Protected Sub rblIsPreEmpTest_SelectedIndexChanged()
        If rblIsPreEmpTest.SelectedValue = "1" Then
            txtPreEmpTestDate.CssClass = "form-control required"
            txtPreEmpTestDate.Enabled = True
            txtPreEmpTestDeptPos.CssClass = "form-control required"
            txtPreEmpTestDeptPos.Enabled = True
        Else
            txtPreEmpTestDate.CssClass = "form-control"
            txtPreEmpTestDate.Enabled = False
            txtPreEmpTestDate.Text = ""
            txtPreEmpTestDeptPos.CssClass = "form-control"
            txtPreEmpTestDeptPos.Enabled = False
            txtPreEmpTestDeptPos.Text = ""
        End If
    End Sub

End Class
