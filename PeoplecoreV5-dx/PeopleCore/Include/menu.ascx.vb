Imports clsLib
Imports System.Data
Imports System.IO

Partial Class include_Menu
    Inherits System.Web.UI.UserControl
    Dim Folder As String
    Dim FileName As String
    Dim ComponentNo As Integer = 0


    Private Sub PopulateMenu(UserNo As Integer, ComponentNo As Integer)
        Dim dt As DataTable
        Dim dtMenuGroup As DataTable
        Dim MenuType As String = ""
        Dim TableName As String = ""
        Dim strLink As String = ""
        Try
            dt = SQLHelper.ExecuteDataTable("EMenu_Web", UserNo, ComponentNo)
            dtMenuGroup = dt.DefaultView.ToTable(True, "MenuMassNo", "MenuMassDesc", "CssClass", "ComponentDesc", "MenuMassCode")
            phMenu.Controls.Add(New LiteralControl("<li class='xn-title'>" & GetMenuClassDesc(ComponentNo) & "</li>"))
            For Each MenuGroupRow As DataRow In dtMenuGroup.Rows
                If Generic.ToStr(MenuGroupRow("MenuMassCode")) = Left(Generic.ToStr(Session("xMenuType")), 2) Then
                    phMenu.Controls.Add(New LiteralControl("<li class='active'>"))
                Else
                    phMenu.Controls.Add(New LiteralControl("<li class='xn-openable'>"))
                End If
                phMenu.Controls.Add(New LiteralControl("<a href='#'><span class='" & Generic.ToStr(MenuGroupRow("CssClass")) & "'></span><span class='xn-text'>" & Generic.ToStr(MenuGroupRow("MenuMassDesc")) & "</span></a>"))
                phMenu.Controls.Add(New LiteralControl("<ul>"))
                For Each MenuRow As DataRow In dt.Select("MenuMassDesc='" & MenuGroupRow("MenuMassDesc") & "'")
                    MenuType = "" : TableName = ""
                    TableName = Generic.ToStr(MenuRow("TableName"))
                    MenuType = Generic.ToStr(MenuRow("MenuType"))
                    phMenu.Controls.Add(New LiteralControl("<li>"))                   
                    'Select Case Generic.ToStr(MenuRow("ComponentNo"))
                    '    Case 1
                    '        strLink = "../secured/page_redirect.aspx?formname=" & Generic.ToStr(MenuRow("FormName")) & "&MenuType=" & MenuType & "&Tablename=" & TableName
                    '        Dim strHyp As String = "<a href=""" & strLink & """><span class=""""></span>" & Generic.ToStr(MenuRow("MenuTitle")) & "</a>"
                    '        phMenu.Controls.Add(New LiteralControl(strHyp))
                    '    Case 2
                    '        strLink = "../securedmanager/page_redirect.aspx?formname=" & Generic.ToStr(MenuRow("FormName")) & "&MenuType=" & MenuType & "&Tablename=" & TableName
                    '        Dim strHyp As String = "<a href=""" & strLink & """><span class=""""></span>" & Generic.ToStr(MenuRow("MenuTitle")) & "</a>"
                    '        phMenu.Controls.Add(New LiteralControl(strHyp))
                    '    Case 3
                    '        strLink = "../securedself/page_redirect.aspx?formname=" & Generic.ToStr(MenuRow("FormName")) & "&MenuType=" & MenuType & "&Tablename=" & TableName
                    '        Dim strHyp As String = "<a href=""" & strLink & """><span class=""""></span>" & Generic.ToStr(MenuRow("MenuTitle")) & "</a>"
                    '        phMenu.Controls.Add(New LiteralControl(strHyp))
                    'End Select
                    Dim lnk As New LinkButton
                    lnk.ID = "lnk" & Generic.ToStr(MenuRow("MenuNo"))
                    lnk.Text = "<span class=''></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & Generic.ToStr(MenuRow("MenuTitle"))
                    lnk.ToolTip = Generic.ToStr(MenuRow("MenuDesc"))
                    lnk.CausesValidation = False                    
                    phMenu.Controls.Add(New LiteralControl("<li>"))
                    Select Case Generic.ToStr(MenuRow("ComponentNo"))
                        Case 1
                            lnk.CommandArgument = "~/secured/" & Generic.ToStr(MenuRow("FormName")) & "|" & MenuType & "|" & TableName
                        Case 2
                            lnk.CommandArgument = "~/securedmanager/" & Generic.ToStr(MenuRow("FormName")) & "|" & MenuType & "|" & TableName
                        Case 3
                            lnk.CommandArgument = "~/securedself/" & Generic.ToStr(MenuRow("FormName")) & "|" & MenuType & "|" & TableName
                        Case 4
                            lnk.CommandArgument = "~/securedapp/" & Generic.ToStr(MenuRow("FormName")) & "|" & MenuType & "|" & TableName
                    End Select
                    AddHandler lnk.Click, AddressOf lnk_Click
                    phMenu.Controls.Add(lnk)
                    phMenu.Controls.Add(New LiteralControl("</li>"))
                Next
                phMenu.Controls.Add(New LiteralControl("</ul>"))
                phMenu.Controls.Add(New LiteralControl("</li>"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Overrides Sub OnInit(e As EventArgs)
        MyBase.OnInit(e)
        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        Folder = FileInfo.Directory.Name.ToLower
        FileName = Path.GetFileName(FileInfo.ToString)
        Select Case Folder
            Case "secured"
                ComponentNo = 1
            Case "securedmanager"
                ComponentNo = 2
            Case "securedself"
                ComponentNo = 3
            Case "securedapp"
                ComponentNo = 4
        End Select
        If Generic.ToInt(Session("OnlineUserNo")) <> 0 Then
            PopulateMenu(Generic.ToInt(Session("OnlineUserNo")), ComponentNo)
        ElseIf Generic.ToInt(Session("OnlineApplicantNo")) <> 0 Then
            PopulateMenu(Generic.ToInt(Session("OnlineApplicantNo")), ComponentNo)
        End If
    End Sub

    Protected Sub lnk_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton()
        Dim URL As String
        lnk = DirectCast(sender, LinkButton)
        URL = Generic.Split(lnk.CommandArgument, 0)
        Dim xFileName As String = Path.GetFileName(URL)
        Session("xMenuType") = Generic.Split(lnk.CommandArgument, 1)
        Session("xTableName") = Generic.Split(lnk.CommandArgument, 2)
        Session("xFormName") = xFileName
        Response.Redirect(URL)
    End Sub

    Private Function GetMenuClassDesc(ComponentNo As Integer) As String
        Dim obj As Object
        obj = SQLHelper.ExecuteScalar("SELECT TOP 1 ComponentDesc FROM EComponent WHERE ComponentNo=" & ComponentNo)
        Return Generic.ToStr(obj)
    End Function

End Class
