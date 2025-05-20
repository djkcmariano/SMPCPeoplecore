Imports clsLib
Imports System.Data

Partial Class Secured_EmpClearanceEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TabNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim IsEnabled As Boolean = False
    Dim IsPosted As Boolean = False
    Dim HRANNo As Integer = 0

    Private Sub PopulateData()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeClearance_WebOne", UserNo, TransNo)
            For Each row In dt.Rows
                IsPosted = Generic.ToBol(row("IsPosted"))
                ViewState("HRANNo") = Generic.ToInt(row("HRANNo"))
            Next
            Generic.PopulateData(Me, "Panel1", dt)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable, Count As Integer
        dt = SQLHelper.ExecuteDataTable("EEmployeeClearance_WebSave", UserNo, PayLocNo, Generic.ToInt(txtCode.Text), Generic.ToInt(cboClearanceTemplateNo.SelectedValue), Generic.ToInt(hifEmployeeNo.Value), txtEffectiveDate.Text, 0)

        For Each row As DataRow In dt.Rows
            TransNo = Generic.ToInt(row("EmployeeClearanceNo"))
            Count = Generic.ToInt(row("xRowCount"))
        Next

        If Count > 0 Then
            Dim url As String = "empclearanceedit.aspx?id=" & TransNo.ToString()
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub btnModify_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpClearance.aspx", "EEmployeeClearance") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        EnabledControls()

        AccessRights.CheckUser(UserNo, "EmpClearance.aspx", "EEmployeeClearance")

        If Not IsPostBack Then
            PopulateData()
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        End If

        If TransNo = 0 Then
            ViewState("IsEnabled") = True            
        End If
        EnabledControls()
    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)

        btnModify.Visible = Not IsEnabled
        btnSave.Visible = IsEnabled

        If IsPosted Then
            btnSave.Visible = False
            btnModify.Visible = False
        End If

        If Generic.ToInt(ViewState("HRANNo")) > 0 Then
            txtEffectiveDate.Enabled = False
        End If

    End Sub

End Class
