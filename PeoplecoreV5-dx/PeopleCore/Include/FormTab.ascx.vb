Imports clsLib
Imports System.Data
Imports System.IO

Partial Class Include_SecTab
    Inherits System.Web.UI.UserControl

    Private _transactionID As String
    Private _TransactionNo As Integer = 0

    Public Property TransactionNo() As Integer
        Get
            Return _TransactionNo
        End Get
        Set(value As Integer)
            _TransactionNo = value
        End Set
    End Property

    Private Sub PopulateTab()
        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        Dim Folder As String = FileInfo.Directory.Name

        Dim FileName As String = Path.GetFileName(FileInfo.ToString)
        Dim MenuType As String = Left(Generic.ToStr(Session("xMenuType")), 4)
        Dim dt As DataTable
        Dim ComponentNo As Integer
        'switch folder
        Select Case Folder.ToLower
            Case "secured"
                ComponentNo = 1
            Case "securedmanager"
                ComponentNo = 2
            Case "securedself"
                ComponentNo = 3
        End Select

        Try

            PlaceHolder1.Controls.Add(New LiteralControl("<div style='padding-bottom:10px;'>"))
            PlaceHolder1.Controls.Add(New LiteralControl("<ul class='nav nav-pills'>"))

            dt = SQLHelper.ExecuteDataTable("EForm_Tab", Generic.ToInt(Session("OnlineUserNo")), FileName, MenuType, ComponentNo, Generic.ToStr(Session("xMenuStyle")))
            For Each row As DataRow In dt.Rows
                Dim lnk As New LinkButton


                Select Case Generic.ToStr(ComponentNo)
                    Case 1
                        lnk.CommandArgument = "~/secured/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName")) & "|" & Generic.ToStr(row("MenuStyle"))
                    Case 2
                        lnk.CommandArgument = "~/securedmanager/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName")) & "|" & Generic.ToStr(row("MenuStyle"))
                    Case 3
                        lnk.CommandArgument = "~/securedself/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName")) & "|" & Generic.ToStr(row("MenuStyle"))
                End Select


                If Generic.ToStr(row("FormName")).ToLower() = FileName.ToLower() Then
                    PlaceHolder1.Controls.Add(New LiteralControl("<li class='active'>"))
                Else
                    PlaceHolder1.Controls.Add(New LiteralControl("<li>"))
                End If

                lnk.CssClass = Generic.ToStr(row("Icon")) & " control-primary"
                lnk.Style.Add("font-family", "FontAwesome, Arial")
                lnk.Text = "&nbsp;&nbsp;" & Generic.ToStr(row("MenuTitle"))

                'lnk.CommandArgument = "~\" & Folder & "\" & Generic.ToStr(row("FormName"))
                AddHandler lnk.Click, AddressOf lnk_Click
                PlaceHolder1.Controls.Add(lnk)
                PlaceHolder1.Controls.Add(New LiteralControl("</li>"))
            Next

            PlaceHolder1.Controls.Add(New LiteralControl("</ul>"))
            PlaceHolder1.Controls.Add(New LiteralControl("</div>"))

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
        Session("xMenuStyle") = Generic.Split(lnk.CommandArgument, 3)
        Response.Redirect(URL & "?id=" & _transactionID)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If _TransactionNo > 0 Then
            _transactionID = _TransactionNo
        Else
            _transactionID = Generic.ToInt(Request.QueryString("id"))
        End If
        PopulateTab()
    End Sub

End Class
