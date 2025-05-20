Imports clsLib
Imports System.Data
Partial Class Register
    Inherits System.Web.UI.Page

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If txtPassword1.Text = txtPassword2.Text Then
            Dim retval As Int64 = SaveRecord()
            If retval = 0 Then

                MessageBox.SuccessResponse("User Account has been successfully created. Please check email to confirm and activate your account.", Me, "defaultapp.aspx")
            ElseIf retval = 1 Then
                MessageBox.Warning("Email Address already exist.", Me)
            ElseIf retval = 2 Then
                MessageBox.Warning("You already registered.", Me)
            ElseIf retval = 3 Then
                MessageBox.Warning("You have existing record in 201.", Me)
            Else
                MessageBox.Warning("Error during creation of account.", Me)
            End If
        Else
            MessageBox.Warning("Password did not match.", Me)
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        lTheme.Text = "<link rel='stylesheet' type='text/css' id='theme' href='css/" & Generic.GetSkin() & "' />"
    End Sub

    Private Function SaveRecord() As Int64
        Dim RetVal As Int64 = 4
        Try
            Dim dt As DataTable
            Dim password As String = PeopleCoreCrypt.Encrypt(txtPassword1.Text)
            Dim ApplicantNo As Integer = 0
            Dim Code As String = Security.Encrypt(txtEmail.Text)
            Dim url As String = Request.Url.GetLeftPart(UriPartial.Authority) + Request.Path.Substring(0, HttpContext.Current.Request.Path.LastIndexOf("/"c)) + "/validate.aspx"
            dt = SQLHelper.ExecuteDataTable("EApplicantWeb_CreateUser", password, txtLastName.Text, txtFirstName.Text, txtMiddleName.Text, txtEmail.Text, txtBirthDate.Text, txtContactNo.Text, Code, url)
            For Each row As DataRow In dt.Rows
                RetVal = Generic.ToInt(row("RetVal"))
                ApplicantNo = Generic.ToInt(row("ApplicantNo"))
                If RetVal = 0 Then
                    Dim obj As Object = SQLHelper.ExecuteScalar("SELECT TOP 1 'valid=' + UserName + '&i=' + CONVERT(VARCHAR(200), ApplicantNo) FROM EApplicantActivate WHERE ApplicantNo=" & ApplicantNo & " AND ISNULL(IsActivated,0) = 0 ORDER BY ApplicantActivateNo DESC")
                    SQLHelper.ExecuteNonQuery("EApplicantNewAccountEmail", ApplicantNo, txtEmail.Text, txtPassword1.Text, Security.Encrypt(Generic.ToStr(obj)))
                End If
            Next
        Catch ex As Exception
            Return 4
        End Try

        Return RetVal
    End Function


End Class
