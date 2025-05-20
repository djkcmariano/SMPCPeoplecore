Imports clsLib
Imports System.Data

Partial Class Secured_CliClinicSocialList
    Inherits System.Web.UI.Page

    Dim TransNo As Int64
    Dim IsEnabled As Boolean = False
    Dim UserNo As Int64

    Private Sub PopulateData()
        Dim _ds As New DataSet
        Dim _ds2 As New DataSet
        Dim dt As DataTable
        Dim IsUD As Boolean = False
        Dim IsEVD As Boolean = False
        Dim IsSleep As Boolean = False

        dt = SQLHelper.ExecuteDataTable("EClinicSocialHis_WebOne", UserNo, TransNo, Generic.ToBol(Session("IsDependent")))
        For Each row As DataRow In dt.Rows
            IsUD = Generic.ToBol(row("IsUD"))
            IsEVD = Generic.ToBol(row("IsEVD"))
            IsSleep = Generic.ToBol(row("IsSleep"))
            Generic.PopulateDropDownList(Generic.ToInt(UserNo), Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            Generic.PopulateData(Me, "Panel1", dt)
        Next

        If IsUD = True Then
            rboIsUDYes.Checked = True
        Else
            rboIsUDNo.Checked = True
        End If

        If IsEVD = True Then
            rboIsEVDYes.Checked = True
        Else
            rboIsEVDNo.Checked = True
        End If

        If IsSleep = True Then
            rboIsSleepYes.Checked = True
        Else
            rboIsSleepNo.Checked = True
        End If

    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinic_WebTabHeader", UserNo, TransNo, Generic.ToBol(Session("IsDependent")))
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next

        If Generic.ToBol(Session("IsDependent")) = False Then
            imgPhoto.Visible = True
            imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
        Else
            imgPhoto.Visible = False
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        AccessRights.CheckUser(UserNo)

        If TransNo = 0 Then
            ViewState("IsEnabled") = True
        End If

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowView) = False Then : Response.Redirect("~/") : End If

        If Not IsPostBack Then
            Generic.PopulateDropDownList(Generic.ToInt(UserNo), Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateData()
        End If

        PopulateTabHeader()
        EnabledControls()

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            If SaveRecord() = True Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)

                ViewState("IsEnabled") = False
                EnabledControls()
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
        Generic.EnableControls(Me, "Panel1", IsEnabled)

        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
    End Sub

    Private Function SaveRecord() As Boolean

        Dim ClinicSocialHisNo As Integer = Generic.ToInt(txtClinicSocialHisCode.Text)
        Dim IsSalty As Boolean = Generic.ToBol(chkIsSalty.Checked)
        Dim IsSweet As Boolean = Generic.ToBol(chkIsSweet.Checked)
        Dim IsFatty As Boolean = Generic.ToBol(chkIsFatty.Checked)
        Dim IsSpicy As Boolean = Generic.ToBol(chkIsSpicy.Checked)
        Dim IsUD As Boolean = False
        Dim IsEVD As Boolean = False
        Dim SmokeD As String = Generic.ToStr(txtSmokeD.Text)
        Dim SmokeA As String = Generic.ToStr(txtSmokeA.Text)
        Dim SmokeF As String = Generic.ToStr(txtSmokeF.Text)
        Dim AlcoholD As String = Generic.ToStr(txtAlcoholD.Text)
        Dim AlcoholA As String = Generic.ToStr(txtAlcoholA.Text)
        Dim AlcoholF As String = Generic.ToStr(txtAlcoholF.Text)
        Dim IsSleep As Boolean = False
        Dim SleepRemarks As String = Generic.ToStr(txtSleepRemarks.Text)

        If rboIsUDYes.Checked = True Then
            IsUD = True
        Else
            IsUD = False
        End If

        If rboIsEVDYes.Checked = True Then
            IsEVD = True
        Else
            IsEVD = False
        End If

        If rboIsSleepYes.Checked = True Then
            IsSleep = True
        Else
            IsSleep = False
        End If

        If SQLHelper.ExecuteNonQuery("EClinicSocialHis_WebSave", UserNo, ClinicSocialHisNo, TransNo, IsSalty, IsSweet, IsFatty, IsSpicy, IsUD, IsEVD, SmokeD, SmokeA, SmokeF, AlcoholD, AlcoholA, AlcoholF, IsSleep, SleepRemarks, Generic.ToBol(Session("IsDependent"))) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If

    End Function



End Class
