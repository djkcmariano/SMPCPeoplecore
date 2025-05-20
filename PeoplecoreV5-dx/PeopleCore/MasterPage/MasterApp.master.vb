Imports clsLib
Imports System.Data
Imports System.IO


Partial Class MasterPage_MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        lTheme.Text = "<link rel='stylesheet' type='text/css' id='theme' href='../css/" & Generic.GetSkin() & "' />"
        'AccessRights.UserCheck(Generic.ToInt(Session("OnlineUserNo")))
        If Generic.ToInt(Session("OnlineApplicantNo")) <> 0 Then
            phMasterIcon.Visible = True
            PopulateBreadCrumb()
            CheckSessionTimeout()                        
            GetPendingTask()
            lblUserName.Text = Generic.ToStr(Session("ApplicantFullname"))
            imgPhoto.ImageUrl = "~/secured/frmShowImage.ashx?tNo=" & Generic.ToInt(Session("OnlineApplicantNo")) & "&tIndex=1"
        End If

        'phSidebard.Visible = False

    End Sub

#Region "PopulateBreadCrumb"
    Private Sub PopulateBreadCrumb()
        Try
            phBreadCrump.Controls.Clear()
            Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
            Dim Folder As String = FileInfo.Directory.Name
            Dim FileName As String = Path.GetFileName(FileInfo.ToString)
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMenu_BreadCrumb", Folder, FileName, Session("xMenuType"))
            For Each row As DataRow In dt.Rows
                Dim lnk As New LinkButton
                lnk.ID = "lnk" & Generic.ToStr(row("MenuNo"))
                lnk.Text = Generic.ToStr(row("MenuTitle"))
                lnk.ToolTip = Generic.ToStr(row("MenuDesc"))
                lnk.CommandArgument = Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName"))
                If Generic.ToStr(row("FormName")).ToLower = FileName.ToLower() Then
                    phBreadCrump.Controls.Add(New LiteralControl("<li class='active'>" & lnk.Text))
                    lnk.Visible = False
                Else
                    phBreadCrump.Controls.Add(New LiteralControl("<li>"))
                End If
                AddHandler lnk.Click, AddressOf lnk_Click
                phBreadCrump.Controls.Add(lnk)
                phBreadCrump.Controls.Add(New LiteralControl("</li>"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnk_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton()
        Dim URL As String
        lnk = DirectCast(sender, LinkButton)
        URL = Generic.Split(lnk.CommandArgument, 0)
        Session("xMenuType") = Generic.Split(lnk.CommandArgument, 1)
        Session("xTableName") = Generic.Split(lnk.CommandArgument, 2)
        Response.Redirect(URL)
    End Sub

    Private Sub CheckSessionTimeout()
        Dim context As HttpContext = HttpContext.Current
        Dim FileInfo As FileInfo = New FileInfo(context.Request.Url.AbsolutePath)
        Dim Folder As String = FileInfo.Directory.Name
        Dim Timeout As Integer = Convert.ToInt32(Session.Timeout) * 60000
        Dim str_Script As String = " var Timeout; " &
                                   " var sessionTimeout = " & Timeout.ToString() & "; " &
                                   " clearTimeout(Timeout);" &
                                   " function RedirectToWelcomePage() { window.location = ""../PageExpired.aspx?i=3""; }" &
                                   " Timeout = setTimeout('RedirectToWelcomePage()', sessionTimeout);" & ""
        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.[GetType](), "CheckSessionOut", str_Script, True)
    End Sub

    Protected Sub btnES_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/securedself/default.aspx")
    End Sub

    Protected Sub btnMS_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/securedmanager/default.aspx")
    End Sub

    Protected Sub btnAdmin_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/secured/default.aspx")
    End Sub

    Protected Sub lnkLogout_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Session.Abandon()
        Response.Redirect("~/defaultapp.aspx")
    End Sub


    Private Sub GetPendingTask()

        Dim count As Integer = 0, Index As Integer = 0
        Dim dt As DataTable

        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        Dim Folder As String = FileInfo.Directory.Name
        Select Case Folder
            Case "secured"
                Index = 1
            Case "securedmanager"
                Index = 3
            Case "securedself"
                Index = 2
            Case "securedapp"
                Index = 4
        End Select

        dt = SQLHelper.ExecuteDataTable("EGetPendingTransaction", Generic.ToInt(Session("OnlineUserNo")), Index, Generic.ToInt(Session("xPayLocNo")))
        For Each row As DataRow In dt.Rows
            Dim lnk As New LinkButton()
            Dim url As String = Generic.ToStr(row("lnk"))
            Dim xcount As Integer = Generic.ToStr(row("fCount"))
            Dim desc As String = Generic.ToStr(row("fDescription"))
            lnk.PostBackUrl = url
            lnk.Text = desc
            lnk.CssClass = "list-group-item"
            lnk.Controls.Add(New LiteralControl(desc & "<span class='label label-warning pull-right'>" & xcount & "</span>"))
            phNotice.Controls.Add(lnk)
            count = count + xcount
        Next        

        If count > 0 Then
            notification.Visible = True
            phTotalCount.Controls.Add(New LiteralControl(count.ToString))
        Else
            notification.Visible = False
        End If
    End Sub

    Protected Sub lnkChangePassword_Click(sender As Object, e As EventArgs)

        Dim Index As Integer = 0
        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        Dim Folder As String = FileInfo.Directory.Name
        Select Case Folder
            Case "secured"
                Session("xMenuType") = "1205000000"
            Case "securedmanager"
                Session("xMenuType") = "1801000000"
            Case "securedself"
                Session("xMenuType") = "1601000000"
        End Select

        Response.Redirect("~/" & Folder & "/SecUserChangePassword.aspx?")

    End Sub

#End Region

End Class

