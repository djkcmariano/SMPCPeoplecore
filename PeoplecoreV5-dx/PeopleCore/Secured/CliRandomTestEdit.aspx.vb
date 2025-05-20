Imports clsLib
Imports System.Data
Partial Class Secured_ERProgramEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "CliRandomTestList.aspx", "EClinicRandomTest") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinicRandomTest_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "CliRandomTestList.aspx", "EClinicRandomTest")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateData()
            PopulateTabHeader()
        End If

        EnabledControls()

    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", Enabled)
        btnModify.Visible = Not Enabled
        btnSave.Visible = Enabled
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EClinicRandomTest_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "CliRandomTestList.aspx", "EClinicRandomTest") Then
            Dim RetVal As Boolean = False
            Dim dt As DataTable
            Dim tno As Integer = Generic.ToInt(Me.txtClinicRandomTestNo.Text)
            Dim ClinicRandomTestDesc As String = Generic.ToStr(Me.txtClinicRandomTestDesc.Text)
            Dim ApplicableMonth As Integer = Generic.ToInt(Me.cboApplicableMonth.SelectedValue)
            Dim ApplicableYear As Integer = Generic.ToInt(Me.txtApplicableYear.Text)
            Dim HeadCount As Integer = Generic.ToInt(Me.txtHeadCount.Text)
            Dim StartDate As String = Generic.ToStr(Me.txtStartDate.Text)
            Dim EndDate As String = Generic.ToStr(Me.txtEndDate.Text)
            Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)

            dt = SQLHelper.ExecuteDataTable("EClinicRandomTest_WebSave", UserNo, tno, ClinicRandomTestDesc, ApplicableMonth, ApplicableYear, HeadCount, StartDate, EndDate, Remarks, PayLocNo)
            For Each row As DataRow In dt.Rows
                TransNo = Generic.ToInt(row("RetVal"))
                RetVal = True
            Next

            If RetVal = True Then
                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim url As String = "CliRandomTestEdit.aspx?id=" & TransNo
                    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                Else
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                    ViewState("IsEnabled") = False
                    EnabledControls()
                End If
                'PopulateTabHeader()
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If

        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

End Class
