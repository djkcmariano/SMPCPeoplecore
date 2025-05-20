Imports clsLib
Imports System.Data

Partial Class Secured_OrgTableReclassEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim clsGeneric As New clsGenericClass

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "OrgTableReclassEdit.aspx", "ETableOrgReclass") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ETableOrgReclass_WebOne", UserNo, PayLocNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            'Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))            
        Next

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "OrgTableReclassEdit.aspx", "ETableOrgReclass")

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

        If txtIsPosted.Checked = True Then
            Enabled = False
        End If

        Generic.EnableControls(Me, "Panel1", Enabled)        
        lnkModify.Visible = Not Enabled
        lnkSave.Visible = Enabled
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETableOrgReclass_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim TableOrgReclassNo As Integer = Generic.ToInt(txtCode.Text)
        Dim TableOrgReclassDesc As String = txtTableOrgReclassDesc.Text
        Dim Remarks As String = txtRemarks.Text
        Dim TableOrgActionTypeNo = Generic.ToInt(cboTableOrgActionTypeNo.SelectedValue)
        Dim EffectiveDate As String = txtEffectiveDate.Text
        Dim PositionNo As Integer = Generic.ToInt(cboPositionNo.SelectedValue)
        Dim NewPositionNo As Integer = Generic.ToInt(cboNewPositionNo.SelectedValue)
        Dim TaskNo As Integer = Generic.ToInt(cboTaskNo.SelectedValue)
        Dim NewTaskNo As Integer = Generic.ToInt(cboNewTaskNo.SelectedValue)
        Dim SalaryGradeNo As Integer = Generic.ToInt(cboSalaryGradeNo.SelectedValue)
        Dim NewSalaryGradeNo As Integer = Generic.ToInt(cboNewSalayGradeNo.SelectedValue)
        Dim OccupationalGroupNo As Integer = Generic.ToInt(cboOccupationalGroupNo.SelectedValue)
        Dim NewOccupationalGroupNo As Integer = Generic.ToInt(cboNewOccupationalGroupNo.SelectedValue)
        Dim obj As Object

        obj = SQLHelper.ExecuteScalar("ETableOrgReclass_WebSave", UserNo, PayLocNo, TableOrgReclassNo, TableOrgReclassDesc, Remarks, TableOrgActionTypeNo, _
                                     EffectiveDate, PositionNo, NewPositionNo, TaskNo, NewTaskNo, SalaryGradeNo, NewSalaryGradeNo, OccupationalGroupNo, NewOccupationalGroupNo)

        TransNo = Generic.ToInt(obj)
        ViewState("IsEnabled") = False

        If TransNo > 0 Then
            Dim url As String = "OrgTableReclassEdit.aspx?id=" & TransNo
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub


End Class
