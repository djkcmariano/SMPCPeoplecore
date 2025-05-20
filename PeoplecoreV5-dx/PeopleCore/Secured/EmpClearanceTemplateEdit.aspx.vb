Imports clsLib
Imports System.Data

Partial Class Secured_EmpClearanceTemplateEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TabNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim IsEnabled As Boolean = False

    Private Sub PopulateData()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EClearanceTemplate_WebOne", UserNo, PayLocNo, TransNo)
            Generic.PopulateData(Me, "Panel1", dt)
        Catch ex As Exception

        End Try        
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable, Count As Integer
        dt = SQLHelper.ExecuteDataTable("EClearanceTemplate_WebSave", UserNo, PayLocNo, Generic.ToInt(txtCode.Text), txtClearanceTemplateCode.Text, txtClearanceTemplateDesc.Text, Generic.ToInt(cboClearanceTypeNo.SelectedValue), Generic.ToInt(chkIsArchived.Checked))

        For Each row As DataRow In dt.Rows
            TransNo = Generic.ToInt(row("ClearanceTemplateNo"))
            Count = Generic.ToInt(row("xRowCount"))
        Next

        If Count > 0 Then
            Dim url As String = "empclearancetemplateedit.aspx?id=" & TransNo.ToString()
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub btnModify_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpClearanceTemplate.aspx", "EClearanceTemplate") Then
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

        AccessRights.CheckUser(UserNo, "EmpClearanceTemplate.aspx", "EClearanceTemplate")

        If Not IsPostBack Then
            PopulateData()
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        End If

        If TransNo = 0 Then
            ViewState("IsEnabled") = True
            EnabledControls()
        End If
    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)

        btnModify.Visible = Not IsEnabled
        btnSave.Visible = IsEnabled        
    End Sub

End Class
