﻿Imports clsLib
Imports System.Data
Imports Microsoft.VisualBasic

Partial Class PasswordChange
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim FPLinkNo As Int64 = 0
    Dim username As String = ""
    Dim IsReset As Boolean

    Protected Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EUser_WebOne", UserNo, UserNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "pnlPopupMain", dt)
        Next
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        username = Generic.ToStr(Session("OnlineUsername"))
        UserNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Request.QueryString("paylocno"))
        FPLinkNo = Generic.ToInt(Request.QueryString("FPLinkNo"))
        IsReset = Generic.ToInt(Request.QueryString("IsReset"))

        Dim ds As DataSet, IsValid As Boolean
        ds = SQLHelper.ExecuteDataSet("EFPLink_WebValidate", UserNo, FPLinkNo)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                IsValid = Generic.ToBol(ds.Tables(0).Rows(0)("IsValid"))
            End If
        End If

        If (UserNo = 0) Or (IsValid = False And IsReset = False) Then
            Response.Redirect("~/pageexpired.aspx?i=6")
        End If

        If Not IsPostBack Then
            PopulateData()
        End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Session.Abandon()
        Session.Clear()
        Session.RemoveAll()
        Response.Redirect("~/default.aspx?")

    End Sub
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If SaveRecord() Then
            Session.Clear()
            Session.Abandon()
            MessageBox.SuccessResponse("Your password has been successfully changed. Click OK to re-log.", Me, "default.aspx")
        End If

    End Sub

    Private Function SaveRecord() As Boolean
        Dim e As New System.EventArgs
        Dim costcenterno As Integer = 0
        Dim fcheck As Integer = 0
        Dim fpassword As String = ""
        Dim fpasswordchk As String = ""
        Dim dsCheck As DataSet
        Dim newpassword As String = ""

        Dim firstName As String = ""
        Dim lastName As String = ""
        Dim middleName As String = ""
        Dim birthdate As String = ""

        dsCheck = SQLHelper.ExecuteDataSet("EUser_WebOldPasswordCheck", UserNo)
        If dsCheck.Tables.Count > 0 Then
            If dsCheck.Tables(0).Rows.Count > 0 Then
                fpasswordchk = Generic.ToStr(dsCheck.Tables(0).Rows(0)("password"))
                fpasswordchk = PeopleCoreCrypt.Decrypt(fpasswordchk)
                firstName = Generic.ToStr(dsCheck.Tables(0).Rows(0)("firstname"))
                lastName = Generic.ToStr(dsCheck.Tables(0).Rows(0)("lastname"))
                middleName = Generic.ToStr(dsCheck.Tables(0).Rows(0)("middlename"))
                birthdate = Generic.ToStr(dsCheck.Tables(0).Rows(0)("birthdate"))

                If txtFirstName.Text.ToUpper <> firstName.ToUpper Then
                    fcheck = 3
                ElseIf txtlastName.Text.ToUpper <> lastName.ToUpper Then
                    fcheck = 4
                    'ElseIf txtBirthDate.Text <> birthdate Then
                    '    fcheck = 5
                End If
            End If
            If dsCheck.Tables(1).Rows.Count > 0 Then
                For fx As Integer = 0 To dsCheck.Tables(1).Rows.Count - 1
                    Dim fpass As String = ""
                    fpass = Generic.ToStr(dsCheck.Tables(1).Rows(fx)("password"))
                    fpass = PeopleCoreCrypt.Decrypt(fpass)
                    If fpass = Me.txtNewPassword.Text Then
                        fcheck = 2
                        Exit For
                    End If
                Next
            End If
        End If

        If fcheck = 2 Then
            MessageBox.Critical("Your new password match from the 3 previous password you created.", Me)
            SaveRecord = False
            Exit Function
        End If

        'If fcheck = 0 Then
        '    MessageBox.Critical("Old Password did not match.", Me)
        '    SaveRecord = False
        '    Exit Function

        'End If

        If Not CheckPasswordMatching(Me.txtNewPassword.Text, Me.txtRnewPassword.Text) Then
            MessageBox.Critical("Your new password did not match.", Me)
            SaveRecord = False
            Exit Function
        End If

        'If Me.txtNewPassword.Text.Length < 8 Then
        '    ClientScript.RegisterClientScriptBlock(e.GetType, "PopupScript", xBase.ShowMessage(" User password must be minimum of 8 alphanumeric characters."))
        '    saverecord = False
        '    Exit Function
        'End If

        If firstName <> "" Then
            newpassword = Me.txtNewPassword.Text.ToString.ToLower
            If (LCase(newpassword.ToString.Contains(firstName.ToLower))) Then
                MessageBox.Critical("User password should not be equal to your firstname.", Me)
                SaveRecord = False
                Exit Function
            End If
        End If

        If lastName <> "" Then
            newpassword = Me.txtNewPassword.Text.ToString.ToLower
            If (LCase(newpassword.ToString.Contains(lastName.ToLower))) Then
                MessageBox.Critical("User password should not be equal to your lastname.", Me)
                SaveRecord = False
                Exit Function
            End If
        End If

        If middleName <> "" Then
            newpassword = Me.txtNewPassword.Text.ToString.ToLower
            If (LCase(newpassword.ToString.Contains(middleName.ToLower))) Then
                MessageBox.Critical("User password should not be equal to your middlename.", Me)
                SaveRecord = False
                Exit Function
            End If
        End If

        If Not System.Text.RegularExpressions.Regex.IsMatch(Me.txtNewPassword.Text, clsPWDComplexity.clsPasswordFormat) Then
            MessageBox.Critical("Pasword format should have a minimum of 8 characters containing atleast 1 lowercase, 1 uppercase, 1 number and 1 valid special character like !,@,#,$,%,^,*,/.", Me)
            SaveRecord = False
            Exit Function
        End If

        If System.Text.RegularExpressions.Regex.IsMatch(Me.txtNewPassword.Text, clsPWDComplexity.clsPasswordFormatAscending) Then
            MessageBox.Critical("Ascending Password character not permitted, Please select another one.", Me)
            SaveRecord = False
            Exit Function
        End If

        If System.Text.RegularExpressions.Regex.IsMatch(Me.txtNewPassword.Text, clsPWDComplexity.clsPasswordFormatDescending) Then
            MessageBox.Critical("Descending Password character not permitted, Please select another one.", Me)
            SaveRecord = False
            Exit Function
        End If

        If System.Text.RegularExpressions.Regex.IsMatch(Me.txtNewPassword.Text, clsPWDComplexity.clsPasswordFormatRepeated) Then
            MessageBox.Critical("Repeated Password character not permitted, Please select another one.", Me)
            SaveRecord = False
            Exit Function
        End If

        fpassword = PeopleCoreCrypt.Encrypt(Me.txtNewPassword.Text)
        If SQLHelper.ExecuteNonQuery("EUser_Web_ChangePasswordSelf", UserNo, fpassword) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If

    End Function
    Private Function CheckPasswordMatching(ByVal newpassword As String, ByVal rnewpassword As String) As Boolean
        Try


            Dim rl As Integer = rnewpassword.Length
            Dim l As Integer = newpassword.Length
            Dim totl As Integer = 0, rtotl As Integer = 0

            If rl <> l Then
                Return False
            Else
                For i As Integer = 1 To l
                    totl = totl + AscW(Mid(newpassword, i, 1))
                    rtotl = rtotl + AscW(Mid(rnewpassword, i, 1))
                Next
            End If
            If rtotl <> totl Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

End Class

















