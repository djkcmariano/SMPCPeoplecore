Imports clsLib
Imports System.Data
Imports System.IO
Imports System.Web

Partial Class Secured_PayJVDefEdit
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayJVDefList.aspx", "EJVDef") Then
            Dim RetVal As Integer = SaveRecord()
            If RetVal > 0 Then
                'If Generic.ToInt(Request.QueryString("id")) = 0 Then
                '    Dim url As String = "PayJVDefEdit.aspx?id=" & RetVal
                '    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                'Else
                '    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                '    ViewState("IsEnabled") = False
                '    EnabledControls()
                'End If
                MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, "PayJVDefList.aspx?Id=" & RetVal)
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayJVDefList.aspx", "EJVDef") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EJVDef_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            txtIsEmployee.Checked = Generic.ToBol(row("IsEmployee"))
        Next

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "PayJVDefList.aspx", "EJVDef")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            'PopulateTabHeader()
            PopulateData()

        End If

        EnabledControls()

    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", Enabled)
        txtCode.Enabled = False

        lnkModify.Visible = Not Enabled
        lnkSave.Visible = Enabled
    End Sub

    Private Function SaveRecord() As Integer
        Dim RetVal As Integer = 0
        Dim dt As DataTable
        Try

            Dim tno As Integer = Generic.ToInt(Me.txtJVDefNo.Text)
            Dim AccntCode As String = Generic.ToStr(Me.txtAccntCode.Text)
            Dim AccntDesc As String = Generic.ToStr(Me.txtAccntDesc.Text)
            Dim PayClassNo As Integer = 0
            Dim IsFixed As Boolean = Generic.ToBol(Me.txtIsFixed.Checked)
            Dim IsEmployee As Boolean = IIf(Generic.ToInt(cboGroupbyNo.SelectedValue) = 3, True, False) 'Generic.ToBol(Me.txtIsEmployee.Checked)
            Dim drcrNo As Integer = Generic.ToInt(cboDRCRNo.SelectedValue)
            Dim groupbyNo As Integer = Generic.ToInt(cboGroupbyNo.SelectedValue)
            Dim EmployeeClassNo As Integer = Generic.ToInt(cboEmployeeClassNo.SelectedValue)
            Dim positionno As Integer = Generic.ToInt(cboPositionNo.SelectedValue)
            Dim jobgradeNo As Integer = Generic.ToInt(cboJobgradeNo.SelectedValue)
            Dim IsArchived As Boolean = Generic.ToBol(chkIsArchived.Checked)

            dt = SQLHelper.ExecuteDataTable("EJVDef_WebSave", UserNo, tno, AccntCode, AccntDesc, PayClassNo, IsFixed, IsEmployee, drcrNo, EmployeeClassNo, groupbyNo, PayLocNo, positionno, jobgradeNo, IsArchived)
            For Each row As DataRow In dt.Rows
                RetVal = Generic.ToInt(row("RetVal"))
            Next
        Catch ex As Exception

        End Try
        Return RetVal
    End Function

    'Private Sub PopulateTabHeader()
    '    Dim dt As DataTable
    '    dt = SQLHelper.ExecuteDataTable("EEvalTemplateTabHeader", UserNo, TransNo)
    '    For Each row As DataRow In dt.Rows
    '        lbl.Text = Generic.ToStr(row("Display"))
    '    Next
    'End Sub

End Class
