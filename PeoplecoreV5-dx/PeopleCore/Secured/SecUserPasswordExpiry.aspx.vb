Imports clsLib
Imports System.Data

Partial Class Secured_SecUserPasswordExpiry
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim IsEnabled As Boolean = False

    Protected Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EUserExpiry_WebOne", UserNo, PayLocNo)
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
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            PopulateData()
        End If

        EnabledControls()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtUserExpiryNo.Text)
            Dim noofdaysexpired As Integer = Generic.ToInt(Me.txtNoOfDaysExpired.Text)
            Dim noofdayswarning As Integer = Generic.ToInt(Me.txtNoOfDaysWarning.Text)
            Dim warningmessage As String = Generic.ToStr(Me.txtWarningMessage.Text)
            Dim deactivatedmessage As String = Generic.ToStr(Me.txtDeactivatedMessage.Text)
            Dim resetmessage As String = Generic.ToStr(Me.txtResetMessage.Text)
            Dim effectivity As String = Generic.ToStr(Me.txtEffectivityDate.Text)
            Dim IsDeactivatedEmail As Boolean = Generic.ToBol(Me.txtIsDeactivatedEmail.Checked)


            If SQLHelper.ExecuteNonQuery("EUserExpiry_WebSave", UserNo, tno, noofdaysexpired, noofdayswarning, warningmessage, deactivatedmessage, resetmessage, effectivity, IsDeactivatedEmail, PayLocNo) > 0 Then
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

















