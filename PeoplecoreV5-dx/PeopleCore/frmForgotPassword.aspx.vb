Imports clsLib
Imports System.Data

Partial Class frmForgotPassword
    Inherits System.Web.UI.Page

    Protected Sub btnSave_Click(sender As Object, e As System.EventArgs)
        Dim url As String = "ctl00_cphBody_mdlforgot"

        If Captcha.IsValid Then


            If Generic.ToStr(txtUsername.Text.ToString) = "" Then
                MessageBox.Warning("Invalid username.", Me)
                Exit Sub
            End If


            Dim ds As DataSet, pwd As String = "", EmailAdd As String = "", UserNo As Integer = 0
            ds = SQLHelper.ExecuteDataSet("Select TOP 1 Convert(Varchar(1000),PasswordE) as Pwd, B.Email, A.UserNo From dbo.sUser A INNER JOIN EEmployee B ON A.EmployeeNo=B.EmployeeNo where ISNULL(B.IsSeparated,0)=0 AND Usercode='" & Generic.ToStr(txtUsername.Text.ToString) & "'")
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    pwd = Generic.ToStr(ds.Tables(0).Rows(0)("pwd"))
                    EmailAdd = Generic.ToStr(ds.Tables(0).Rows(0)("Email"))
                    UserNo = Generic.ToStr(ds.Tables(0).Rows(0)("UserNo"))
                End If
            End If

            If pwd <> "" Then
                pwd = PeopleCoreCrypt.Decrypt(pwd)
                SQLHelper.ExecuteNonQuery("EUser_WebForgot", Generic.ToInt(UserNo), pwd)
                MessageBox.Success("Your username and password were successfully sent to your assigned email address in 201 record.", Me)
            Else
                MessageBox.Warning("Your username did not return any results. Please try again.", Me)
            End If

            If Generic.ToStr(EmailAdd) = "" Then
                MessageBox.Information("No email address defined to your 201 record. Please inform your HRIS Administrator to proceed this transaction.", Me)
                Exit Sub
            End If
        End If
    End Sub

End Class
