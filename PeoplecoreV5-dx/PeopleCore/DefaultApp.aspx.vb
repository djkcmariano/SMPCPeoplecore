Imports clsLib
Imports System.Data
Imports System.IO

Partial Class DefaultApp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        SQLHelper.GetIniFile()
        lTheme.Text = "<link rel='stylesheet' type='text/css' id='theme' href='css/" & Generic.GetSkin() & "' />"
    End Sub

    Protected Sub grdMain_Sorting(sender As Object, e As GridViewSortEventArgs)
        Try
            If ViewState("SortDirection") Is Nothing OrElse ViewState("SortExpression").ToString() <> e.SortExpression Then
                ViewState("SortDirection") = "ASC"
            ElseIf ViewState("SortDirection").ToString() = "ASC" Then
                ViewState("SortDirection") = "DESC"
            ElseIf ViewState("SortDirection").ToString() = "DESC" Then
                ViewState("SortDirection") = "ASC"
            End If
            ViewState("SortExpression") = e.SortExpression
            'PopulateGrid()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub grdMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            'grdMain.PageIndex = e.NewPageIndex
            'PopulateGrid()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkCreate_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/Register.aspx")
    End Sub

    Protected Sub lnkForgot_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Dim ret As Int64 = ValidateLogin()
        If ret = 1 Then
            Response.Redirect("~/securedapp/default.aspx")
        ElseIf ret = 2 Then
            MessageBox.Warning("Invalid username or password.", Me)
        Else
            MessageBox.Warning("You have no existing record on our database!", Me)
        End If

    End Sub

    Private Function ValidateLogin() As Int64

        Dim dt As DataTable
        Dim x As Int64 = 0
        Dim ApplicantNo As Int64 = 0
        Dim ApplicantStatNo As Int64 = 0
        Dim Password As String = ""
        Dim Fullname As String = ""
        Try
            dt = SQLHelper.ExecuteDataTable("EApplicant_WebUser", txtUsername.Text)
            For Each row As DataRow In dt.Rows
                x = Generic.ToInt(row("Retval"))
                ApplicantNo = Generic.ToInt(row("OnlineUserNo"))
                ApplicantStatNo = Generic.ToInt(row("ApplicantStatNo"))
                Password = Generic.ToStr(row("Pwd"))
                Fullname = Generic.ToStr(row("Fullname"))
            Next

            If x = 1 Then
                If txtPassword.Text = PeopleCoreCrypt.Decrypt(Password) Then
                    Session("OnlineApplicantNo") = ApplicantNo
                    Session("ApplicantFullname") = Fullname
                Else
                    x = 2
                End If

            End If
        Catch generatedExceptionName As Exception
            x = 0
        End Try
        Return x

    End Function

End Class
