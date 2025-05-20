Imports clsLib
Imports System.Data

Partial Class Secured_SecAMPSetting
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim IsEnabled As Boolean = False

    Protected Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMRAnnualExpiry_WebOne", UserNo, PayLocNo)
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
            Dim tno As Integer = Generic.ToInt(Me.txtMRAnnualExpiryNo.Text)
            Dim ApplicableMonth As Integer = Generic.ToInt(cboApplicableMonth.SelectedValue)
            Dim ApplicableDay As Integer = Generic.ToInt(cboApplicableDay.SelectedValue)
            Dim xMessage As String = Generic.ToStr(txtxMessage.Text)
            Dim NoOfDaysNoti As Integer = Generic.ToInt(txtNoOfDaysNoti.Text)
            Dim NoOfDays As Integer = Generic.ToInt(txtNoOfDays.Text)
            Dim NoOfDaysMsg As String = Generic.ToStr(txtNoOfDaysMsg.Text)
            Dim IsSuspended As Boolean = Generic.ToBol(txtIsSuspeneded.Checked)

            Dim Str As String = UserNo & ", " & tno & ", " & ApplicableMonth & ", " & ApplicableDay & ", " & xMessage & ", " & NoOfDaysNoti & ", " & NoOfDays & ", " & NoOfDaysMsg & ", " & IsSuspended & ", " & PayLocNo

            If SQLHelper.ExecuteNonQuery("EMRAnnualExpiry_WebSave", UserNo, tno, ApplicableMonth, ApplicableDay, xMessage, NoOfDaysNoti, NoOfDays, NoOfDaysMsg, IsSuspended, PayLocNo) > 0 Then
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

















