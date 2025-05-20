Imports clsLib
Imports System.Data

Partial Class Secured_SecSMTPConnection
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim IsEnabled As Boolean = False

    Protected Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmailHost_WebOne", UserNo, PayLocNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "pnlPopupMain", dt)
        Next
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateData()
        End If

        EnabledControls()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim tNo As Integer = Generic.ToInt(Me.txtEmailHostNo.Text)
            Dim tCode As String = Generic.ToStr(Me.txtEmailHostCode.Text)
            Dim tDesc As String = Generic.ToStr(Me.txtEmailHostDesc.Text)
            Dim SMTP As String = Generic.ToStr(Me.txtEmailHost.Text)
            Dim PortNo As Integer = Generic.ToInt(Me.txtPortNo.Text)
            Dim Email As String = Generic.ToStr(Me.txtEmailHostFrom.Text)
            Dim UserName As String = Generic.ToStr(Me.txtSMTPUserName.Text)
            Dim Password As String = Generic.ToStr(Me.txtSMTPPassword.Text)
            Dim IsEnabledSSL As Boolean = Generic.ToBol(Me.txtIsEnabledSSL.Checked)

            If SQLHelper.ExecuteNonQuery("EEmailHost_WebSave", UserNo, tNo, tCode, tDesc, SMTP, PortNo, Email, UserName, Password, IsEnabledSSL, PayLocNo) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
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
        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
    End Sub
End Class

















