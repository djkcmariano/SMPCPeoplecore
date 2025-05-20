Imports clsLib
Imports System.Data

Partial Class Secured_SecUserResetPasswordCompany
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)

            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try


        End If


        cboPayLocNo.Enabled = False
        cboPayLocNo.SelectedValue = PayLocNo


        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub ResetPassword(EmployeeNo As Integer, UserCode As String, Password As String)
        Dim fpassword As String = PeopleCoreCrypt.Encrypt(Password)
        Dim Retval As Boolean = False
        If SQLHelper.ExecuteNonQuery("EUser_WebPasswordGenerate", UserNo, EmployeeNo, UserCode, fpassword, PayLocNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        'If Retval Then
        '    hifuserno.Value = 0
        '    MessageBox.Success(MessageTemplate.SuccessSave, Me)
        'Else
        '    MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        'End If

    End Sub


    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim fpassword As String = PeopleCoreCrypt.Encrypt(txtPassword.Text)

            If SQLHelper.ExecuteNonQuery("EUser_WebResetPassword", UserNo, Generic.ToInt(hifuserno.Value), txtUserCode.Text.ToString, fpassword) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                hifuserno.Value = 0
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If


    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Generic.ClearControls(Me, "pnlPopupMain")
        hifuserno.Value = 0

    End Sub
    Protected Sub lnkGenerate_Click(sender As Object, e As System.EventArgs)

        Dim UserCode As String = ""
        Dim Password As String = ""

        Try
            Dim empno As Integer = 0
            Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataTable("EUserPayloc_WebOne", UserNo, PayLocNo)
            For Each row As DataRow In dt.Rows
                empno = Generic.ToInt(row("employeeno"))
                UserCode = Generic.ToStr(row("usercode"))


                Dim RandomPassword As String = EGetRandomPassword(empno)
                Password = RandomPassword

                ResetPassword(empno, UserCode, Password)

            Next


        Catch ex As Exception
        End Try
    End Sub
    Protected Sub txtUserName_TextChanged(sender As Object, e As System.EventArgs)

        'Dim UserCode As String = ""
        'Dim Password As String = ""

        'Try
        '    If Generic.ToInt(hifuserno.Value) <> 0 Then
        '        Dim empno As Integer = 0
        '        Dim dt As New DataTable
        '        dt = SQLHelper.ExecuteDataTable("EUserPayloc_WebOne", UserNo, PayLocNo)
        '        For Each row As DataRow In dt.Rows
        '            empno = Generic.ToInt(row("employeeno"))
        '            UserCode = Generic.ToStr(row("usercode"))


        '            Dim RandomPassword As String = EGetRandomPassword(empno)
        '            Password = RandomPassword

        '            ResetPassword(empno, UserCode, Password)

        '        Next


        '    End If

        'Catch ex As Exception
        'End Try
    End Sub
    Protected Function EGetRandomPassword(EmployeeNo As Integer) As String
        Dim password As String = ""

        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("ERandomPassword_Web", EmployeeNo)
        For Each row As DataRow In dt.Rows
            password = Generic.ToStr(row("tPassword"))
        Next

        EGetRandomPassword = password
    End Function


End Class

















