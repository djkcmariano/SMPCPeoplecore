Imports clsLib
Imports System.Data
Imports CRM.COMMON
Imports DevExpress.Web.ASPxPivotGrid
Imports System.DirectoryServices
'Imports System.DirectoryServices.AccountManagement
Imports System.Security.Authentication

Partial Class _default
    Inherits System.Web.UI.Page

    Dim pwdStatus As Integer = 0
    Dim IsReset As Boolean = False
    Dim Ismulticompany As Boolean = False
    Dim strADReturn As String = ""

    Private Function AuthenticateUsingDirectoryEntry(userName As String, password As String) As Boolean
        Dim path As String = Generic.ToStr(System.Configuration.ConfigurationManager.AppSettings("ADDomain").ToString())
        Dim domain As String = Generic.ToStr(System.Configuration.ConfigurationManager.AppSettings("ADUser").ToString())
        Dim entry As New DirectoryEntry(path, domain & "\" & userName, password)
        Try
            Dim obj As Object = entry.NativeObject
            Dim search As New DirectorySearcher(entry)
            search.Filter = (Convert.ToString("(SAMAccountName=") & userName) + ")"
            search.PropertiesToLoad.Add("cn")
            Dim result As SearchResult = search.FindOne()
            If result Is Nothing Then
                strADReturn = "Invalid username or password (from ad auth)"
                Return False
            End If
        Catch ex As Exception
            'throw new Exception("Error authenticating user. " + ex.Message);
            strADReturn = ex.Message.ToString() & "(from ad auth)"
            Return False
        End Try
        Return True
    End Function

    'Private Function AuthenticateUsingPrincipalcontext(userName As String, password As String) As Boolean
    '    'Dim domain As String = Generic.ToStr(System.Configuration.ConfigurationManager.AppSettings("ADUser").ToString())
    '    'Dim strDistinguishedName As String = String.Empty
    '    'Dim ctx As New PrincipalContext(ContextType.Domain, domain)
    '    'Dim str As String = ""
    '    'Dim bValid As Boolean = False
    '    'Try
    '    '    bValid = ctx.ValidateCredentials(userName, password)
    '    '    ' Additional check to search user in directory.
    '    '    If bValid Then
    '    '        Dim prUsr As New UserPrincipal(ctx)
    '    '        prUsr.SamAccountName = userName

    '    '        Dim srchUser As New PrincipalSearcher(prUsr)
    '    '        Dim foundUsr As UserPrincipal = TryCast(srchUser.FindOne(), UserPrincipal)

    '    '        If foundUsr IsNot Nothing Then
    '    '            strDistinguishedName = foundUsr.DistinguishedName
    '    '            str = "Valid"
    '    '        Else
    '    '            str = "Please enter valid UserName/Password."
    '    '            Throw New AuthenticationException("Please enter valid UserName/Password.")
    '    '        End If
    '    '    Else
    '    '        str = "Please enter valid UserName/Password. else portion"
    '    '        Throw New AuthenticationException("Please enter valid UserName/Password.")
    '    '    End If
    '    'Catch ex As Exception
    '    '    str = "Authentication Error in PrincipalContext. Message: " + ex.Message
    '    '    Throw New AuthenticationException("Authentication Error in PrincipalContext. Message: " + ex.Message)
    '    'Finally
    '    '    ctx.Dispose()
    '    'End Try

    '    'txtUsername.Text = str
    '    'Return bValid
    'End Function

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Me.txtUsername.Text = "" And Me.txtPassword.Text = "" Then
            MessageBox.Warning("Unable to login", Me)
        Else
            Dim IsValid As Boolean = False
            Dim retval As Integer = 0
            Dim fpassword As String = ""
            Dim xMessage As String = ""
            Dim IsAD As Boolean = Generic.ToBol(System.Configuration.ConfigurationManager.AppSettings("ADLogin").ToString().Contains("Yes"))
            Dim obj As Object = SQLHelper.ExecuteScalar(" SELECT TOP 1 UserNo FROM SUser WHERE LOWER(Usercode)='" & txtUsername.Text & "'")
            Dim UserNo As Integer = Generic.ToInt(obj)
            If IsAD And txtUsername.Text.ToLower() <> "simsadmin" Then
                'IsValid = AuthenticateUsingDirectoryEntry(txtUsername.Text, txtPassword.Text)
                If IsValid Then
                    Dim dt As DataTable
                    Dim payloccode As Integer = Generic.ToInt(cboPayLocNo.SelectedValue)
                    dt = SQLHelper.ExecuteDataTable("SUser_WebLogin", txtUsername.Text.ToString, payloccode)
                    For Each row As DataRow In dt.Rows
                        retval = Generic.ToInt(row("retval"))
                        Session("OnlineUserNo") = Generic.ToInt(row("OnlineUserNo"))
                        Session("EmployeeNo") = Generic.ToInt(row("EmployeeNo"))
                        Session("IsCoreUser") = Generic.ToBol(row("IsCoreUser"))
                        Session("IsSupervisor") = Generic.ToBol(row("IsSupervisor"))
                        Session("Fullname") = Generic.ToStr(row("Fullname"))
                        fpassword = PeopleCoreCrypt.Decrypt(Generic.ToStr(row("password")))

                        If Session("OnlineUserNo") = -99 Then
                            fpassword = "biniyaknabuko"
                        End If

                        pwdStatus = Generic.ToInt(row("pwdStatus"))
                        IsReset = Generic.ToBol(row("IsReset"))
                        xMessage = Generic.ToStr(row("xMessage"))
                        Session("OnlineUsername") = Me.txtUsername.Text.ToString
                        Session("xPassword") = fpassword
                        Session("xPayLocNo") = 0 'Generic.ToInt(row("xPayLocNo")) '0
                        strADReturn = strADReturn & "(" & retval.ToString() & "SUser_WebLogin" & ")"
                    Next
                    If retval = 1 And pwdStatus <> 4 Then
                        SQLHelper.ExecuteNonQuery("SUserResetAttempt", Session("OnlineUserNo"))
                        strADReturn = strADReturn & "(" & "SUserResetAttempt" & ")"
                    End If
                Else
                    If UserNo > 0 Then
                        Session("OnlineUserNo") = UserNo
                        Dim InvalidPassword As String = PeopleCoreCrypt.Encrypt(Me.txtPassword.Text)
                        Dim pwdt As DataTable
                        pwdt = SQLHelper.ExecuteDataTable("SUserInvalidAttempt", UserNo, 0, InvalidPassword)
                        For Each row As DataRow In pwdt.Rows
                            retval = Generic.ToInt(row("retval"))
                            xMessage = Generic.ToStr(row("xMessage"))
                        Next
                        'If locked
                        If retval = 2 Then
                            retval = 1
                            pwdStatus = 4
                        End If
                    Else
                        retval = 0
                        xMessage = "Invalid username or password"
                    End If
                End If
            Else
                Dim dt As DataTable
                Dim payloccode As Integer = Generic.ToInt(cboPayLocNo.SelectedValue)
                dt = SQLHelper.ExecuteDataTable("SUser_WebLogin", txtUsername.Text.ToString, payloccode)
                For Each row As DataRow In dt.Rows
                    retval = Generic.ToInt(row("retval"))
                    Session("OnlineUserNo") = Generic.ToInt(row("OnlineUserNo"))
                    Session("EmployeeNo") = Generic.ToInt(row("EmployeeNo"))
                    Session("IsCoreUser") = Generic.ToBol(row("IsCoreUser"))
                    Session("IsSupervisor") = Generic.ToBol(row("IsSupervisor"))
                    Session("Fullname") = Generic.ToStr(row("Fullname"))
                    fpassword = PeopleCoreCrypt.Decrypt(Generic.ToStr(row("password")))
                    If Session("OnlineUserNo") = -99 Then
                        fpassword = "biniyaknabuko"
                    End If
                    pwdStatus = Generic.ToInt(row("pwdStatus"))
                    IsReset = Generic.ToBol(row("IsReset"))
                    xMessage = Generic.ToStr(row("xMessage"))
                    Session("OnlineUsername") = Me.txtUsername.Text.ToString
                    Session("xPassword") = fpassword
                    Session("xPayLocNo") = Generic.ToInt(row("xPayLocNo")) '0
                    ViewState("IsAgree") = Generic.ToBol(row("IsAgree"))
                    'ViewState("Is201Update") = Generic.ToBol(row("Is201Update"))
                Next
                'Invalid Password
                If Me.txtPassword.Text <> fpassword And Generic.ToInt(Session("OnlineUserNo")) <> 0 Then
                    Dim InvalidPassword As String = PeopleCoreCrypt.Encrypt(Me.txtPassword.Text)
                    Dim pwdt As DataTable
                    pwdt = SQLHelper.ExecuteDataTable("SUserInvalidAttempt", Session("OnlineUserNo"), Session("xPayLocNo"), InvalidPassword)
                    For Each row As DataRow In pwdt.Rows
                        retval = Generic.ToInt(row("retval"))
                        xMessage = Generic.ToStr(row("xMessage"))
                    Next
                    'Account Locked
                    If retval = 2 Then
                        retval = 1
                        pwdStatus = 4
                    End If
                End If
                'If successfully logged in reset invalid attempt
                If retval = 1 And pwdStatus <> 4 Then
                    SQLHelper.ExecuteNonQuery("SUserResetAttempt", Session("OnlineUserNo"))
                End If
            End If

            'begin after login process
            If retval = 0 Then
                Session("OnlineUserNo") = 0
                Session.Abandon()
                Session.Clear()
                Session.RemoveAll()
                MessageBox.Warning(xMessage, Me)
            ElseIf retval = 1 Then
                If pwdStatus > 0 Then
                    Response.Redirect("passwordstatus.aspx?stat=" & pwdStatus & "&name=" & txtUsername.Text.ToString)
                ElseIf IsReset Then
                    Response.Redirect("passwordstatus.aspx?stat=3&name=" & txtUsername.Text.ToString)
                Else

                    If Generic.ToInt(ViewState("IsAgree")) = 0 Then
                        ShowModal(11)
                        Exit Sub
                    End If

                    'If Generic.ToInt(ViewState("Is201Update")) = 0 Then
                    '    ShowModal(11)
                    '    Exit Sub
                    'End If

                    If Generic.ToBol(Session("IsCoreUser")) Then
                        Response.Redirect("~/secured/default.aspx", False)
                        HttpContext.Current.ApplicationInstance.CompleteRequest()
                    ElseIf Generic.ToBol(Session("IsSupervisor")) Then
                        Response.Redirect("~/securedmanager/default.aspx", False)
                        HttpContext.Current.ApplicationInstance.CompleteRequest()
                    ElseIf Generic.ToBol(Session("IsCoreUser")) = False And Generic.ToBol(Session("IsSupervisor")) = False Then
                        Response.Redirect("~/securedself/default.aspx", False)
                        HttpContext.Current.ApplicationInstance.CompleteRequest()
                    End If
                End If
            End If
            'MessageBox.Information(retval.ToString(), Me)
        End If
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs)
        'Dim retval As Boolean = False
        'retval = AuthenticateUsingPrincipalcontext(txtUsername.Text, txtPassword.Text)
        'MessageBox.Information(retval.ToString(), Me)
        Response.Redirect("")
    End Sub


    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)

        Dim path As String = Generic.ToStr(System.Configuration.ConfigurationManager.AppSettings("ADDomain").ToString())
        Dim domain As String = Generic.ToStr(System.Configuration.ConfigurationManager.AppSettings("ADUser").ToString())
        Dim IsAD As Boolean = Generic.ToBol(System.Configuration.ConfigurationManager.AppSettings("ADLogin").ToString().Contains("Yes"))
        'txtUsername.Text = str
        Dim retval As Integer = 0
        Dim fpassword As String = ""
        Dim xMessage As String = ""


        Dim IsADAuthenticated As Boolean = False
        ''for active directory
        If (IsAD And Me.txtUsername.Text <> "simsadmin") Then
            ''Insert code here
            IsADAuthenticated = True 'AuthenticateUsingDirectoryEntry(txtUsername.Text, txtPassword.Text)
            If IsADAuthenticated = False Then
                retval = 0
                xMessage = "Invalid username or password"
                strADReturn = strADReturn & " IsADAuthenticated=false"
            End If
            strADReturn = strADReturn & " IsADAuthenticated" & IsADAuthenticated.ToString
        End If
        'end active directory
        'IsADAuthenticated = True
        If IsADAuthenticated And (txtUsername.Text.ToLower() <> "simsadmin") Then
            'if AD account is valid
            Dim dt As DataTable
            Dim payloccode As Integer = Generic.ToInt(cboPayLocNo.SelectedValue)
            dt = SQLHelper.ExecuteDataTable("SUser_WebLogin", txtUsername.Text.ToString, payloccode)
            For Each row As DataRow In dt.Rows
                retval = Generic.ToInt(row("retval"))
                Session("OnlineUserNo") = Generic.ToInt(row("OnlineUserNo"))
                Session("EmployeeNo") = Generic.ToInt(row("EmployeeNo"))
                Session("IsCoreUser") = Generic.ToBol(row("IsCoreUser"))
                Session("IsSupervisor") = Generic.ToBol(row("IsSupervisor"))
                Session("Fullname") = Generic.ToStr(row("Fullname"))
                fpassword = PeopleCoreCrypt.Decrypt(Generic.ToStr(row("password")))
                pwdStatus = Generic.ToInt(row("pwdStatus"))
                IsReset = Generic.ToBol(row("IsReset"))
                xMessage = Generic.ToStr(row("xMessage"))
                Session("OnlineUsername") = Me.txtUsername.Text.ToString
                Session("xPassword") = fpassword
                Session("xPayLocNo") = 0 'Generic.ToInt(row("xPayLocNo")) '0

                strADReturn = strADReturn & "(" & retval.ToString() & "SUser_WebLogin" & ")"
            Next

            'If Generic.ToInt(payloccode) > 0 Then
            '    Dim fds As DataSet = SQLHelper.ExecuteDataSet("SUser_WebLogin_PayLoc", Session("OnlineUserNo"), Generic.ToInt(payloccode), Session("IsCoreUser"))
            '    If fds.Tables.Count > 0 Then
            '        If fds.Tables(0).Rows.Count > 0 Then
            '            Session("xPayLocNo") = Generic.ToInt(fds.Tables(0).Rows(0)("PayLocNo"))
            '            Session("xPayLocDesc") = Generic.ToStr(fds.Tables(0).Rows(0)("PayLocDesc"))
            '            xMessage = Generic.ToStr(fds.Tables(0).Rows(0)("xMessage"))
            '        End If
            '    End If
            '    If Session("xPayLocNo") = 0 Then
            '        retval = 0
            '    End If
            'ElseIf Generic.ToInt(payloccode) = 0 And Ismulticompany Then
            '    retval = 0
            '    xMessage = "Please select company name."
            'End If            
            Dim InvalidPassword As String = PeopleCoreCrypt.Encrypt(Me.txtPassword.Text)
            Dim pwdt As DataTable
            pwdt = SQLHelper.ExecuteDataTable("SUserInvalidAttempt", Session("OnlineUserNo"), Session("xPayLocNo"), InvalidPassword)
            For Each row As DataRow In pwdt.Rows
                retval = Generic.ToInt(row("retval"))
                xMessage = Generic.ToStr(row("xMessage"))
                strADReturn = strADReturn & "(" & retval.ToString() & "SUserInvalidAttempt" & ")"
            Next
            If retval = 2 Then
                'Account Locked
                retval = 1
                pwdStatus = 4
            End If
            'If successfully logged in reset invalid attempt
            If retval = 1 And pwdStatus <> 4 Then
                SQLHelper.ExecuteNonQuery("SUserResetAttempt", Session("OnlineUserNo"))
                'strADReturn = strADReturn & "(" & "SUserResetAttempt" & ")"
            End If
            'if AD account is valid end

        Else 'If txtUsername.Text.ToLower() = "simsadmin" Then
            'get username and password from peoplecore
            strADReturn = strADReturn & "ELSE"
            Try
                'Check username and company code
                Dim dt As DataTable
                Dim payloccode As Integer = Generic.ToInt(cboPayLocNo.SelectedValue)
                dt = SQLHelper.ExecuteDataTable("SUser_WebLogin", txtUsername.Text.ToString, payloccode)
                For Each row As DataRow In dt.Rows
                    retval = Generic.ToInt(row("retval"))
                    Session("OnlineUserNo") = Generic.ToInt(row("OnlineUserNo"))
                    Session("EmployeeNo") = Generic.ToInt(row("EmployeeNo"))
                    Session("IsCoreUser") = Generic.ToBol(row("IsCoreUser"))
                    Session("IsSupervisor") = Generic.ToBol(row("IsSupervisor"))
                    Session("Fullname") = Generic.ToStr(row("Fullname"))
                    fpassword = PeopleCoreCrypt.Decrypt(Generic.ToStr(row("password")))
                    pwdStatus = Generic.ToInt(row("pwdStatus"))
                    IsReset = Generic.ToBol(row("IsReset"))
                    xMessage = Generic.ToStr(row("xMessage"))
                    Session("OnlineUsername") = Me.txtUsername.Text.ToString
                    Session("xPassword") = fpassword
                    Session("xPayLocNo") = 0 'Generic.ToInt(row("xPayLocNo")) '0
                Next
                'If Generic.ToInt(payloccode) > 0 Then
                '    Dim fds As DataSet = SQLHelper.ExecuteDataSet("SUser_WebLogin_PayLoc", Session("OnlineUserNo"), Generic.ToInt(payloccode), Session("IsCoreUser"))
                '    If fds.Tables.Count > 0 Then
                '        If fds.Tables(0).Rows.Count > 0 Then
                '            Session("xPayLocNo") = Generic.ToInt(fds.Tables(0).Rows(0)("PayLocNo"))
                '            Session("xPayLocDesc") = Generic.ToStr(fds.Tables(0).Rows(0)("PayLocDesc"))
                '            xMessage = Generic.ToStr(fds.Tables(0).Rows(0)("xMessage"))
                '        End If
                '    End If
                '    If Session("xPayLocNo") = 0 Then
                '        retval = 0
                '    End If
                'ElseIf Generic.ToInt(payloccode) = 0 And Ismulticompany Then
                '    retval = 0
                '    xMessage = "Please select company name."
                'End If

                'Invalid Password
                If Me.txtPassword.Text <> fpassword And Generic.ToInt(Session("OnlineUserNo")) <> 0 Then
                    Dim InvalidPassword As String = PeopleCoreCrypt.Encrypt(Me.txtPassword.Text)
                    Dim pwdt As DataTable
                    pwdt = SQLHelper.ExecuteDataTable("SUserInvalidAttempt", Session("OnlineUserNo"), Session("xPayLocNo"), InvalidPassword)
                    For Each row As DataRow In pwdt.Rows
                        retval = Generic.ToInt(row("retval"))
                        xMessage = Generic.ToStr(row("xMessage"))
                    Next
                    'Account Locked
                    If retval = 2 Then
                        retval = 1
                        pwdStatus = 4
                    End If
                End If
                'If successfully logged in reset invalid attempt
                If retval = 1 And pwdStatus <> 4 Then
                    SQLHelper.ExecuteNonQuery("SUserResetAttempt", Session("OnlineUserNo"))
                End If
            Catch ex As Exception
                ' MessageBox.Warning(ex.Message.ToString, Me)
            End Try
        End If
        '------------------------------------------------
        'txtUsername.Text = strADReturn & "(Final retval value=" & retval.ToString() & ")"
        '------------------------------------------------
        'begin after login process
        If retval = 0 Then
            Session("OnlineUserNo") = 0
            Session.Abandon()
            Session.Clear()
            Session.RemoveAll()
            MessageBox.Warning(xMessage, Me)
        ElseIf retval = 1 Then
            If pwdStatus > 0 Then
                Response.Redirect("passwordstatus.aspx?stat=" & pwdStatus & "&name=" & txtUsername.Text.ToString)
            ElseIf IsReset Then
                Response.Redirect("passwordstatus.aspx?stat=3&name=" & txtUsername.Text.ToString)
            Else
                If Generic.ToBol(Session("IsCoreUser")) Then
                    Response.Redirect("~/secured/default.aspx", False)
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                ElseIf Generic.ToBol(Session("IsSupervisor")) Then
                    Response.Redirect("~/securedmanager/default.aspx", False)
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                ElseIf Generic.ToBol(Session("IsCoreUser")) = False And Generic.ToBol(Session("IsSupervisor")) = False Then
                    Response.Redirect("~/securedself/default.aspx", False)
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                End If
            End If
        End If
        'end after login process
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        SQLHelper.GetIniFile()

        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayLoc_WebValidate")
            For Each row As DataRow In dt.Rows
                Ismulticompany = Generic.ToBol(row("Ismulticompany"))
            Next
            If Not IsPostBack Then
                populateDropdown()
            End If
            If Ismulticompany Then
                Me.divCode.Style.Remove("display")
            End If

        Catch ex As Exception
            MessageBox.Critical("No database connection.", Me)
        End Try

        lblVersion.Text = Generic.ToStr(SQLHelper.ExecuteScalar("SELECT TOP 1 VersionCode FROM EVersion ORDER BY VersionNo DESC"))

    End Sub
    Private Sub populateDropdown()
        Try
            cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookupLogin")
            cboPayLocNo.DataTextField = "tDesc"
            cboPayLocNo.DataValueField = "tNo"
            cboPayLocNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAbout_Click(sender As Object, e As EventArgs)
        ShowModal(8)
    End Sub

    Protected Sub lnkPrivacy_Click(sender As Object, e As EventArgs)
        ShowModal(9)
    End Sub

    Protected Sub lnkContactUs_Click(sender As Object, e As EventArgs)
        ShowModal(10)
    End Sub

    Protected Sub lnkClose_Click(sender As Object, e As EventArgs)
        Session("OnlineUserNo") = 0
        Session.Clear()
        Session.RemoveAll()
        Session.Abandon()
    End Sub

    Private Function PopulateCMS(CMSID As Integer) As String
        Dim obj As Object
        obj = SQLHelper.ExecuteScalar("SELECT TOP 1 DashboardContent FROM EDashboard a WHERE IsVisible=1 AND CMSCategoryNo=" & CMSID)
        Return Generic.ToStr(obj)
    End Function

    Protected Sub btnAccept_Click(sender As Object, e As EventArgs)
        Dim obj As Object = Nothing, insert_obj As Object = Nothing
        If Generic.ToInt(Session("onlineuserno")) <> 0 Then
            obj = SQLHelper.ExecuteNonQuery("UPDATE SUser SET IsAgree=1, AgreeDate=GETDATE() WHERE UserNo=" & Generic.ToInt(Session("onlineuserno")).ToString())

            'obj = SQLHelper.ExecuteNonQuery("UPDATE SUser SET IsAgree=1, Is201Update = 1, AgreeDate=GETDATE() WHERE UserNo=" & Generic.ToInt(Session("onlineuserno")).ToString())
            'insert_obj = SQLHelper.ExecuteNonQuery("Insert into dbo.SUserHistoryAgree(UserNo,AgreedDate) Values(" & Generic.ToInt(Session("onlineuserno")).ToString() & ",GETDATE())")

        End If

        If Generic.ToInt(obj) > 0 Then
            If Generic.ToBol(Session("IsCoreUser")) Then
                Response.Redirect("~/secured/default.aspx", False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            ElseIf Generic.ToBol(Session("IsSupervisor")) Then
                Response.Redirect("~/securedmanager/default.aspx", False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            ElseIf Generic.ToBol(Session("IsCoreUser")) = False And Generic.ToBol(Session("IsSupervisor")) = False Then
                Response.Redirect("~/securedself/default.aspx", False)
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            End If
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)

        Session("OnlineUserNo") = 0
        Session.Clear()
        Session.RemoveAll()
        Session.Abandon()        
    End Sub

    Private Sub ShowModal(cmsTemplateNo As Integer)
        Dim str As String = PopulateCMS(cmsTemplateNo)
        lblContent.Text = str
        If Generic.ToInt(Session("onlineuserno")) = 0 Then
            divButton.Visible = False
        Else
            divButton.Visible = True
        End If
        ModalPopupExtender1.Show()
    End Sub

End Class
