Imports clsLib
Imports System.Data
Partial Class Validate
    Inherits System.Web.UI.Page

    Private Function ValidateCode(ApplicantNo As String, Code As String) As String
        Dim RetVal As String = ""
        Try
            Dim xID As Int64 = Generic.ToInt(ApplicantNo)
            Dim Email As String = Generic.ToStr(Code)

            Dim obj As Object = Nothing
            obj = SQLHelper.ExecuteScalar("SELECT TOP 1 Token FROM EApplicantActivate WHERE ISNULL(IsActivated, 0)= 0 AND ApplicantNo=" + xID.ToString())
            If Generic.ToStr(Security.Decrypt(obj.ToString())) = Email Then
                If SQLHelper.ExecuteNonQuery("UPDATE EApplicantActivate SET IsActivated=1, ActivatedDate=GETDATE() WHERE ApplicantNo=" + xID.ToString()) > 0 Then
                    Panel1.Visible = True
                    Return "Email address has been successfully validated."
                End If
            End If
        Catch generatedExceptionName As Exception
            RetVal = "Unable to validate email address."
        End Try

        Return RetVal
    End Function


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim Code As String = ""
        Dim ApplicantNo As String = ""

        SQLHelper.GetIniFile()
        lTheme.Text = "<link rel='stylesheet' type='text/css' id='theme' href='css/" & Generic.GetSkin() & "' />"
        Code = Generic.ToStr(Request.QueryString("valid"))
        ApplicantNo = Generic.ToStr(Request.QueryString("i"))
        lbl.Text = ValidateCode(ApplicantNo, Code)
    End Sub
End Class
