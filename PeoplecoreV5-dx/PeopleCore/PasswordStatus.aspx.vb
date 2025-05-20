Imports clsLib
Imports System.Data

Partial Class PasswordStatus
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim pwdstatus As Integer = 0
    Dim username As String = ""

    Protected Sub PopulateData()

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("SUserExpiry_WebValidate", UserNo, PayLocNo)
        For Each row As DataRow In dt.Rows

            If pwdstatus = 1 Then

                Me.txtWarningMessage.Text = "<strong>Warning:</strong> " & Generic.ToStr(row("WarningMessage"))
                panelChange.Visible = True
                panelLater.Visible = True
                panelBack.Visible = False

                If Generic.ToBol(row("IsDeactivatedEmail")) = True Then
                    If DeactivateEmail(UserNo) = False Then
                        MessageBox.Critical("Unabled to auto reset password. No email defined, please inform your administrator.", Me)
                    End If
                End If

            ElseIf pwdstatus = 2 Then

                Me.txtWarningMessage.Text = "<strong>Password Deactivated:</strong> " & Generic.ToStr(row("DeactivatedMessage"))
                panelChange.Visible = False
                panelLater.Visible = False
                panelBack.Visible = True

                Session.Clear()
                Session.Abandon()

            ElseIf pwdstatus = 3 Then

                Me.txtWarningMessage.Text = "<strong>Password Reset:</strong> " & Generic.ToStr(row("ResetMessage"))
                panelChange.Visible = True
                panelLater.Visible = False
                panelBack.Visible = False

            ElseIf pwdstatus = 4 Then

                Me.txtWarningMessage.Text = "<strong>Account Locked:</strong> " & Generic.ToStr(row("LockedMessage"))
                panelChange.Visible = False
                panelLater.Visible = False
                panelBack.Visible = True

                If Generic.ToBol(row("IsLockedEmail")) = True Then
                    If DeactivateEmail(UserNo) = False Then
                        MessageBox.Critical("Unabled to auto reset password. No email defined, please inform your administrator.", Me)
                    End If
                End If
                Session.Clear()
                Session.Abandon()
            End If

        Next

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        pwdstatus = Generic.ToInt(Request.QueryString("stat"))
        username = Generic.ToStr(Session("OnlineUsername"))
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        If UserNo = 0 Or pwdstatus = 0 Then
            Response.Redirect("~/default.aspx?")
        End If

        If Not IsPostBack Then
            PopulateData()
        End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkChange_Click(sender As Object, e As System.EventArgs)

        Session("OnlineUserNo") = 0
        Session("xPayLocNo") = 0
        'Session.Abandon()
        'Session.Clear()
        'Session.RemoveAll()
        Response.Redirect("~/passwordchange.aspx?paylocno=" & PayLocNo.ToString & "&userno=" & UserNo.ToString)

    End Sub

    Protected Sub lnkProceed_Click(sender As Object, e As System.EventArgs)

        If Generic.ToBol(Session("IsCoreUser")) Then
            Response.Redirect("~/secured")
        ElseIf Generic.ToBol(Session("IsSupervisor")) Then
            Response.Redirect("~/securedmanager")
        ElseIf Generic.ToBol(Session("IsCoreUser")) = False And Generic.ToBol(Session("IsSupervisor")) = False Then
            Response.Redirect("~/securedself")
        End If

    End Sub

    Protected Sub lnkBack_Click(sender As Object, e As System.EventArgs)

        Session("OnlineUserNo") = 0
        Session("xPayLocNo") = 0
        Session.Abandon()
        Session.Clear()
        Session.RemoveAll()
        Response.Redirect("~/default.aspx?")

    End Sub

    Protected Function DeactivateEmail(UserNo As Integer) As Boolean
        'Dim password As String = Left(Guid.NewGuid().ToString(), 8)
        Dim password As String = ""
        Dim empno As Integer = 0

        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EUser_WebOne", UserNo, UserNo)
        For Each row As DataRow In dt.Rows
            empno = Generic.ToInt(row("employeeno"))
        Next

        Dim pwdt As New DataTable
        pwdt = SQLHelper.ExecuteDataTable("ERandomPassword_Web", empno)
        For Each row As DataRow In pwdt.Rows
            password = Generic.ToStr(row("tPassword"))
        Next

        If SQLHelper.ExecuteNonQuery("SUser_EmailPassword", UserNo, password, PeopleCoreCrypt.Encrypt(password)) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function






End Class
