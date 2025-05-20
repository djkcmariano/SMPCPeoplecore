Imports clsLib
Imports System.Data
Imports System.IO
Imports System.Web

Partial Class Secured_PEStandardMainEdit
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim RetVal As Integer = SaveRecord()
            If RetVal > 0 Then
                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim url As String = "PEStandardMainEdit.aspx?id=" & RetVal
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

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEStandardMain_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "PEStandardMainList.aspx")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try

            PopulateTabHeader()
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

            Dim tno As Integer = Generic.ToInt(Me.txtPEStandardMainNo.Text)
            Dim Applicableyear As Integer = Generic.ToInt(Me.txtApplicableyear.Text)
            Dim PETemplateNo As Integer = Generic.ToInt(Me.cboPETemplateNo.Text)
            Dim PositionNo As Integer = Generic.ToInt(Me.cboPositionNo.Text)
            Dim IsFromPERating As Boolean = 0 'Generic.ToBol(Me.txtIsFromPERating.Checked)
            Dim IsArchived As Boolean = Generic.ToBol(Me.txtIsArchived.Checked)


            dt = SQLHelper.ExecuteDataTable("EPEStandardMain_WebSave", UserNo, tno, Applicableyear, PositionNo, PETemplateNo, IsFromPERating, IsArchived, Generic.ToInt(cboPayLocNo.SelectedValue))
            For Each row As DataRow In dt.Rows
                RetVal = Generic.ToInt(row("RetVal"))
            Next
        Catch ex As Exception

        End Try
        Return RetVal
    End Function

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEStandardMain_WebTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
    End Sub

End Class
