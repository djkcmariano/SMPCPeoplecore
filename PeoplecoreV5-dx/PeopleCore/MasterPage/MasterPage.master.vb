Imports clsLib
Imports System.Data
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports Microsoft.VisualBasic.FileIO


Partial Class MasterPage_MasterPage
    Inherits System.Web.UI.MasterPage
    Implements System.Web.UI.ICallbackEventHandler

    Dim TabId As Integer = 0
    Dim ReportNo As Integer = 0
    Dim UserReportNo As Integer = 0
    Private _callBackStatus As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        TabId = Generic.ToInt(Request.QueryString("id"))
        'lTheme.Text = "<link rel='stylesheet' type='text/css' id='theme' href='../css/" & Generic.GetSkin() & "' />"
        'lTheme.Text = "<link rel='stylesheet' type='text/css' id='theme' href='../css/theme-light.css' />"
        lTheme.Text = lTheme.Text & BannerColor()
        ReportNo = Generic.ToInt(Request.QueryString("ReportNo"))
        UserReportNo = Generic.ToInt(Request.QueryString("UserReportNo"))
        'AccessRights.UserCheck(Generic.ToInt(Session("OnlineUserNo")))
        If Not IsPostBack Then
            SQLHelper.ExecuteNonQuery("SUserStat_WebSave_Activity", Generic.ToInt(Session("OnlineUserNo")), Session("xFormname"), "Open Form", 1)
        End If

        If Generic.ToInt(Session("OnlineUserNo")) <> 0 Then
            lblUserName.Text = Generic.ToStr(Session("FullName"))
            phMasterIcon.Visible = True
            PopulateBreadCrumb()
            CheckSessionTimeout()
            divAdmin.Visible = Generic.ToBol(Session("IsCoreUser"))
            divMS.Visible = Generic.ToBol(Session("IsSupervisor"))
            'GetPendingTask()
            'PopulateCompany()
            If Not IsPostBack Then
                GetPendingTask()
                PopulateCompany()
            End If
            imgPhoto.ImageUrl = "~/secured/frmShowImage.ashx?tNo=" & Generic.ToInt(Session("EmployeeNo")) & "&tIndex=2"
        End If

        ' create a call back reference so we can log-out user when user closes the browser

        Dim callBackReference As String = Page.ClientScript.GetCallbackEventReference(Me, "arg", "LogOutUser", "")
        Dim logOutUserCallBackScript As String = "function LogOutUserCallBack(arg, context) { " & callBackReference & "; }"
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "LogOutUserCallBack", logOutUserCallBackScript, True)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Public Function GetCallbackResult() As String Implements System.Web.UI.ICallbackEventHandler.GetCallbackResult
        Return _callBackStatus
    End Function

    Public Sub RaiseCallbackEvent(ByVal eventArgument As String) Implements System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent
        _callBackStatus = "failed"

        Dim clsGen As New clsGenericClass
        clsGen.xlogOut(Generic.ToInt(Session("OnlineUserNo")))
        _callBackStatus = "success"
    End Sub

#Region "PopulateBreadCrumb"
    Private Sub PopulateBreadCrumb()
        Try
            phBreadCrump.Controls.Clear()
            Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
            Dim Folder As String = FileInfo.Directory.Name
            Dim FileName As String = Path.GetFileName(FileInfo.ToString)
            Dim dt As DataTable
            Dim MenuStyle As String
            Dim FormName As String
            dt = SQLHelper.ExecuteDataTable("EMenu_BreadCrumb", Folder, FileName, Session("xMenuType"), ReportNo, UserReportNo)
            For Each row As DataRow In dt.Rows
                Dim lnk As New LinkButton
                lnk.ID = "lnk" & Generic.ToStr(row("MenuNo"))
                lnk.Text = Generic.ToStr(row("MenuTitle"))
                lnk.ToolTip = Generic.ToStr(row("MenuDesc"))
                lnk.CausesValidation = False
                MenuStyle = Generic.ToStr(row("MenuStyle"))
                Session("xMenuStyle") = Generic.ToStr(row("MenuStyle"))
                FormName = Generic.ToStr(row("FormName"))
                If (MenuStyle = "Tab" Or Left(MenuStyle, 3) = "Sub") And TabId > 0 Then
                    FormName = FormName & "?id=" & TabId
                End If

                lnk.CommandArgument = FormName & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName")) & "|" & Generic.ToStr(row("MenuStyle"))
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
        Dim xFileName As String = Path.GetFileName(URL)
        Session("xMenuType") = Generic.Split(lnk.CommandArgument, 1)
        Session("xTableName") = Generic.Split(lnk.CommandArgument, 2)
        Session("xMenuStyle") = Generic.Split(lnk.CommandArgument, 3)
        Session("xFormName") = xFileName
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
        Dim clsGen As New clsGenericClass
        clsGen.xlogOut(Generic.ToInt(Session("OnlineUserNo")))
        Session.Clear()
        Session.Abandon()

        Response.Redirect("~/")
    End Sub

    Protected Sub lnkTime_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/securedself/SelfDTRRawLogs.aspx")
    End Sub

    Protected Sub lnkChangePassword_Click(sender As Object, e As EventArgs)

        Dim Index As Integer = 0
        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        Dim Folder As String = FileInfo.Directory.Name
        Select Case Folder.ToLower()
            Case "secured"
                Session("xMenuType") = "1205000000"
            Case "securedmanager"
                Session("xMenuType") = "1801000000"
            Case "securedself"
                Session("xMenuType") = "1601000000"
        End Select

        Response.Redirect("~/" & Folder & "/SecUserChangePassword.aspx?")

    End Sub

    Protected Sub lnkPolicy_Click(sender As Object, e As EventArgs)
        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        Dim Folder As String = FileInfo.Directory.Name
        Response.Redirect("~/" & Folder & "/frmPolicy.aspx")
    End Sub

    Protected Sub lnkHelp_Click(sender As Object, e As EventArgs)
        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        Dim Folder As String = FileInfo.Directory.Name
        Response.Redirect("~/" & Folder & "/frmHelp.aspx")
    End Sub

    Protected Sub lnkFeedback_Click(sender As Object, e As EventArgs)
        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        Dim Folder As String = FileInfo.Directory.Name
        Response.Redirect("~/" & Folder & "/frmFeedback.aspx")
    End Sub

#End Region

#Region "Pending Transaction"
    Private Sub GetPendingTask()
        Try
            Dim Index As Integer = 0, dt As DataTable
            Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
            Dim Folder As String = FileInfo.Directory.Name
            Dim count As Integer = 0
            'Dim FormName As String, count As Integer

            Select Case Folder.ToLower()
                Case "secured"
                    Index = 1
                Case "securedmanager"
                    Index = 3
                Case "securedself"
                    Index = 2
            End Select

            dt = SQLHelper.ExecuteDataTable("EGetPendingTransaction", Generic.ToInt(Session("OnlineUserNo")), Index, Generic.ToInt(Session("xPayLocNo")))
            For Each row As DataRow In dt.Rows
                'Dim lnkNotice As New LinkButton
                Dim xcount As Integer = Generic.ToStr(row("fCount"))
                'lnkNotice.ID = "lnkNotice" & Generic.ToStr(row("MenuNo"))
                'lnkNotice.Text = Generic.ToStr(row("fDescription"))
                'lnkNotice.CssClass = "list-group-item"
                '    FormName = Generic.ToStr(row("lnk"))
                '    lnkNotice.CommandArgument = FormName & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName"))
                'lnkNotice.Controls.Add(New LiteralControl(lnkNotice.Text & "<span class='label label-danger pull-right'>" & xcount & "</span>"))
                '    AddHandler lnkNotice.Click, AddressOf lnk_Click
                '    phNotice.Controls.Add(lnkNotice)
                '    'phNotice.Controls.Add(New LiteralControl("</li>"))
                count = count + xcount
            Next

            rNotification.DataSource = dt
            rNotification.DataBind()

            If count > 0 Then
                notification.Visible = True
                phTotalCount.Controls.Add(New LiteralControl(count.ToString))
            Else
                notification.Visible = False
            End If

        Catch ex As Exception
            'MessageBox.Warning("UserNo : " & Generic.ToStr(Session("OnlineUserNo")) & " PayLocNo : " & Generic.ToStr(Session("xPayLocNo")), Me)
        End Try

    End Sub
#End Region

#Region "Company"

    Private Sub PopulateCompany()

        Dim dt As DataTable
        Dim obj As Object = Nothing
        Dim IsMultiCompany As Boolean = False
        Dim paylocno As Integer
        IsMultiCompany = Generic.ToBol(SQLHelper.ExecuteScalar("EPayLoc_WebValidate"))
        company.Visible = IsMultiCompany
        If IsMultiCompany Then
            obj = SQLHelper.ExecuteScalar("SELECT TOP 1 Photo FROM EPayLoc WHERE PayLocNo=" & Generic.ToInt(Session("xPayLocNo")))
            paylocno = Generic.ToInt(Session("xPayLocNo"))
            dt = SQLHelper.ExecuteDataTable("EPayLoc_Web", Generic.ToInt(Session("OnlineUserNo")), "", 0)
            rCompany.DataSource = dt
            rCompany.DataBind()
            'For Each row As DataRow In dt.Rows
            '    Dim lnkCompany As New LinkButton
            '    lnkCompany.ID = "lnkCompany" & Generic.ToStr(row("PayLocNo"))
            '    lnkCompany.Text = Generic.ToStr(row("PayLocDesc"))
            '    lnkCompany.CssClass = "list-group-item"
            '    lnkCompany.CommandArgument = Generic.ToStr(row("PayLocNo"))
            '    AddHandler lnkCompany.Click, AddressOf lnkCompany_Click
            '    phCompany.Controls.Add(lnkCompany)
            'Next
        Else
            obj = SQLHelper.ExecuteScalar("SELECT TOP 1 Photo FROM EPayLoc")
            paylocno = SQLHelper.ExecuteScalar("SELECT TOP 1 PayLocNo FROM EPayLoc")
        End If

        If paylocno > 0 Then
            Dim bytes() As Byte
            bytes = ObjectToByteArray(obj)
            If bytes.Length > 102 Then
                imgCompany.ImageUrl = "~/secured/frmShowImage.ashx?tNo=" & paylocno.ToString() & "&tIndex=4"
            Else
                imgCompany.Visible = False
            End If
        End If


    End Sub

    Protected Sub lnkCompany_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Session("xPayLocNo") = lnk.CommandArgument
        'Response.Redirect(Request.RawUrl)
        Response.Redirect("~/secured")
    End Sub


#End Region

    Private Function ObjectToByteArray(obj As [Object]) As Byte()
        If obj Is Nothing Then
            Return Nothing
        End If

        Dim bf As New BinaryFormatter()
        Dim ms As New MemoryStream()
        bf.Serialize(ms, obj)

        Return ms.ToArray()
    End Function

    Private Function BannerColor() As String
        Dim str As String = ""

        'Index Reference
        '0 = banner color
        '1 = menu background color
        Dim PCoreLogo As String = "logo.png"
        Dim PCoreLogoSmall As String = "logo_small.png"

        If Generic.ToInt(GetValue(24)) = 2 Then : PCoreLogo = "logo_light.png" : Else : PCoreLogo = "logo.png" : End If
        If Generic.ToInt(GetValue(24)) = 2 Then : PCoreLogoSmall = "logo-small_light.png" : Else : PCoreLogoSmall = "logo-small.png" : End If

        str = "<style type='text/css'>" & _
              ".x-navigation.x-navigation-horizontal { " & " background: #" & GetValue(0) & "; } " & _
              ".x-navigation { background: #" & GetValue(1) & "; } " & _
              ".x-navigation li > a { color: #" & GetValue(2) & ";border-bottom: 1px solid #" & GetValue(3) & "; } " & _
              ".x-navigation li > a .fa,.x-navigation li > a .glyphicon {  color: #" & GetValue(4) & "; } " & _
              ".x-navigation li > a:hover { background: #" & GetValue(5) & ";color: #" & GetValue(6) & "; } " & _
              ".x-navigation li > a:hover .fa, .x-navigation li > a:hover .glyphicon {  color: #" & GetValue(7) & "; } " & _
              ".x-navigation li.active > a { background: #" & GetValue(8) & "; color: #" & GetValue(9) & " } " & _
              ".x-navigation li.active > a .fa, .x-navigation li.active > a .glyphicon { color: #" & GetValue(10) & "; } " & _
              ".x-navigation li.xn-openable:before { color: #" & GetValue(11) & "; } " & _
              ".x-navigation li.xn-title { color: #" & GetValue(12) & ";border-bottom: 1px solid #" & GetValue(13) & "; } " & _
              ".x-navigation li > ul { background: #" & GetValue(14) & "; } " & _
              ".x-navigation li > ul li > a { border-bottom-color: #" & GetValue(15) & "; } " & _
              ".x-navigation li > ul li > a:hover {  background: #" & GetValue(16) & ";color: #" & GetValue(17) & "; } " & _
              ".x-navigation.x-navigation-horizontal > li > a .fa, .x-navigation.x-navigation-horizontal > li > a .glyphicon {color: #" & GetValue(18) & "; }" & _
              ".x-navigation.x-navigation-horizontal > li > a:hover .fa, .x-navigation.x-navigation-horizontal > li > a:hover .glyphicon { color: #" & GetValue(19) & "; }" & _
              ".x-navigation.x-navigation-horizontal > li > a:hover { background: #" & GetValue(20) & ";} " & _
              ".x-navigation.x-navigation-horizontal > li.active > a { background: #" & GetValue(21) & ";} " & _
              ".x-navigation.x-navigation-horizontal > li.active > a .fa, .x-navigation.x-navigation-horizontal > li.active > a .glyphicon { color: #" & GetValue(22) & "; } " & _
              ".x-navigation > li.xn-logo > a:first-child {background: url('../img/" & PCoreLogo & "') top center no-repeat #" & IIf(GetValue(23) = "", "fcfcfc", GetValue(23)) & ";} " & _
              ".x-navigation.x-navigation-minimized > li.xn-logo > a:first-child { background-image: url('../img/" & PCoreLogoSmall & "'); }" & _
              "</style>"
        Return str
    End Function


    Private Function GetValue(index As Integer) As String
        Try
            Using parser As New TextFieldParser(Server.MapPath("~/secured/connectionstr/skin.ini"))                
                parser.TextFieldType = FieldType.Delimited
                parser.Delimiters = New String() {"|"}
                Dim fields As String()
                While Not parser.EndOfData
                    fields = parser.ReadFields()
                    If index = fields(0) Then
                        Return fields(1)
                    End If
                End While
            End Using
        Catch ex As Exception
        End Try
        Return ""
    End Function


   
End Class

