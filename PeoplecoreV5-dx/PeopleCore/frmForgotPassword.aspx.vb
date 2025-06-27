Imports clsLib
Imports System.Data

Partial Class frmForgotPassword
    Inherits System.Web.UI.Page

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs)
        Dim url As String = "ctl00_cphBody_mdlforgot"
        Dim query As String
        Dim encryptedQuery As String

        If Captcha.IsValid Then


            If Generic.ToStr(txtUsername.Text.ToString) = "" Then
                MessageBox.Warning("Invalid username.", Me)
                Exit Sub
            End If


            Dim ds As DataSet, pwd As String = "", EmailAdd As String = "", UserNo As Integer = 0
            'ds = SQLHelper.ExecuteDataSet("Select TOP 1 Convert(Varchar(1000),PasswordE) as Pwd, B.Email, A.UserNo From dbo.sUser A INNER JOIN EEmployee B ON A.EmployeeNo=B.EmployeeNo where ISNULL(B.IsSeparated,0)=0 AND Usercode='" & Generic.ToStr(txtUsername.Text.ToString) & "'")
            ds = SQLHelper.ExecuteDataSet("SUser_WebForgotPassword", Generic.ToStr(txtUsername.Text.ToString))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    pwd = Generic.ToStr(ds.Tables(0).Rows(0)("pwd"))
                    EmailAdd = Generic.ToStr(ds.Tables(0).Rows(0)("Email"))
                    UserNo = Generic.ToStr(ds.Tables(0).Rows(0)("UserNo"))
                End If
            End If
            If Generic.ToStr(EmailAdd) = "" Then
                MessageBox.Information("An email address is not defined in your 201 record. Kindly coordinate with your Peoplecore Administrator to proceed with this transaction.", Me)
                Exit Sub
            End If
            If pwd <> "" Then

                Dim ds1 As DataSet, FPLinkNo As String = ""
                ds1 = SQLHelper.ExecuteDataSet("EFPLink_WebCreate", UserNo)
                If ds1.Tables.Count > 0 Then
                    If ds1.Tables(0).Rows.Count > 0 Then
                        FPLinkNo = Generic.ToStr(ds1.Tables(0).Rows(0)("FPLinkNo"))
                    End If
                End If

                Query = "id=" + Generic.ToStr(UserNo) & "&FPLinkNo=" + Generic.ToStr(FPLinkNo)
                encryptedQuery = "?enc=" & Security.Encrypt(query)

                encryptedQuery = Request.Url.GetLeftPart(UriPartial.Authority) & Request.Path.Substring(0, HttpContext.Current.Request.Path.LastIndexOf("/")) & "/passwordchange.aspx" + encryptedQuery




                pwd = PeopleCoreCrypt.Decrypt(pwd)
                SQLHelper.ExecuteNonQuery("EUser_WebForgotPassword", Generic.ToInt(UserNo), encryptedQuery, FPLinkNo)

                Dim urlr As String = "default.aspx?"
                MessageBox.SuccessResponse("The password reset link has been successfully sent to your company or personal email address as recorded in your 201 file.", Me, urlr)

            Else
                MessageBox.Warning("Your username did not return any results. Please try again.", Me)
            End If

            'If pwd <> "" Then
            '    pwd = PeopleCoreCrypt.Decrypt(pwd)
            '    SQLHelper.ExecuteNonQuery("EUser_WebForgot", Generic.ToInt(UserNo), pwd)
            '    MessageBox.Success("Your username and password were successfully sent to your assigned email address in 201 record.", Me)
            'Else
            '    MessageBox.Warning("Your username did not return any results. Please try again.", Me)
            'End If


        End If
    End Sub

End Class
