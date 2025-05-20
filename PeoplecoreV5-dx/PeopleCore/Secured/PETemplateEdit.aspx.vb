Imports clsLib
Imports System.Data

Partial Class Secured_SecCMSTemplateEdit
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim IsEnabled As Boolean = False

    Protected Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPETemplate_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "pnlPopupMain", dt)
            ASPxHtmlEditor1.Html = Generic.ToStr(row("Instruction"))
        Next
        Try
            cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
            cboPayLocNo.DataTextField = "tdesc"
            cboPayLocNo.DataValueField = "tNo"
            cboPayLocNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        If TransNo = 0 Then : ViewState("IsEnabled") = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateData()
        End If

        EnabledControls()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then

            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtPETemplateNo.Text)
            Dim PETemplateCode As String = Generic.ToStr(Me.txtPETemplateCode.Text)
            Dim PETemplateDesc As String = Generic.ToStr(Me.txtPETemplateDesc.Text)
            Dim Title As String = Generic.ToStr(Me.txtTitle.Text)
            Dim SubTitle As String = Generic.ToStr(Me.txtSubTitle.Text)
            Dim Instruction As String = ASPxHtmlEditor1.Html

            If SQLHelper.ExecuteNonQuery("EPETemplate_WebSave", UserNo, tno, PETemplateCode, PETemplateDesc, Title, SubTitle, Instruction, "", "", "", "", "", "", Generic.ToInt(cboPayLocNo.SelectedValue)) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, "../secured/PETemplateList.aspx?")
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

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)
        txtCode.Enabled = False
        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
        ASPxHtmlEditor1.Enabled = IsEnabled
    End Sub


End Class

















