Imports clsLib
Imports System.Data
Imports System.IO


<ParseChildren(True)> _
Partial Class Include_TabSelf
    Inherits System.Web.UI.UserControl

    Private _content As ITemplate = Nothing
    Private _header As ITemplate = Nothing
    Private _transactionID As String
    Private _headervisible As Boolean = True
    Private _TransactionNo As Integer = 0
    Private _menustyle As String

    Public Property menuStyle As String
        Get
            Return _menustyle
        End Get
        Set(value As String)
            _menustyle = value
        End Set
    End Property

    <TemplateContainer(GetType(TemplateControl))> _
    <PersistenceMode(PersistenceMode.InnerProperty)> _
    <TemplateInstance(TemplateInstance.[Single])> _
    Public Property Content() As ITemplate
        Get
            Return _content
        End Get
        Set(value As ITemplate)
            _content = value
        End Set
    End Property


    <TemplateContainer(GetType(TemplateControl))> _
    <PersistenceMode(PersistenceMode.InnerProperty)> _
    <TemplateInstance(TemplateInstance.[Single])> _
    Public Property Header() As ITemplate
        Get
            Return _header
        End Get
        Set(value As ITemplate)
            _header = value
        End Set
    End Property

    Public Property HeaderVisible() As Boolean
        Get
            Return _headervisible
        End Get
        Set(value As Boolean)
            _headervisible = value
        End Set
    End Property

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
        If _menustyle = "" Then
            _menustyle = "Tab"
        End If

        Dim strLink As String = "", fMenuType As String = "", fTablename As String = ""

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
            dt = SQLHelper.ExecuteDataTable("EMenu_Tab", Generic.ToInt(Session("OnlineUserNo")), FileName, MenuType, ComponentNo, _menustyle)
            For Each row As DataRow In dt.Rows
                Dim lnk As New LinkButton
                Select Case Generic.ToStr(ComponentNo)
                    Case 1
                        lnk.CommandArgument = "~/secured/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName") & "|" & Generic.ToStr(row("FormName")))
                    Case 2
                        lnk.CommandArgument = "~/securedmanager/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName") & "|" & Generic.ToStr(row("FormName")))
                    Case 3
                        lnk.CommandArgument = "~/securedself/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName") & "|" & Generic.ToStr(row("FormName")))
                End Select

                If _transactionID = 0 Then
                    If Generic.ToInt(row("OrderLevel")) > 1 Then
                        lnk.Enabled = False
                    End If
                End If

                '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                'Dim xdt As DataTable
                Dim tStatus As String = ""
                'Dim MenuNo As Integer = Generic.ToInt(row("MenuNo"))
                'Dim SubTable As String = Generic.ToStr(row("SubTable"))
                'Dim SubCode As String = Generic.ToStr(row("SubCode"))

                'fMenuType = "" : fTablename = ""
                'fTablename = Generic.ToStr(row("TableName"))
                'fMenuType = Generic.ToStr(row("MenuType"))

                'Select Case Generic.ToStr(ComponentNo)
                '    Case 1
                '        If _transactionID = 0 Then
                '            If Generic.ToInt(row("OrderLevel")) > 1 Then
                '                strLink = "../secured/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                Dim strHyp As String = "<a href=""" & strLink & """ disabled=""disabled"" class=""list-group-item text-left"">" & Generic.ToStr(row("MenuTitle")) & tStatus & "</a>"
                '                PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '            Else
                '                If Generic.ToStr(row("FormName")).ToLower() = FileName.ToLower() Then
                '                    strLink = "../secured/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                    Dim strHyp As String = "<a href=""" & strLink & """ class=""list-group-item active text-left"">" & Generic.ToStr(row("MenuTitle")) & "</a>"
                '                    PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '                Else
                '                    strLink = "../secured/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                    Dim strHyp As String = "<a href=""" & strLink & """ class=""list-group-item text-left"">" & Generic.ToStr(row("MenuTitle")) & "</a>"
                '                    PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '                End If

                '            End If
                '        Else
                '            If Generic.ToStr(row("FormName")).ToLower() = FileName.ToLower() Then
                '                strLink = "../secured/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                Dim strHyp As String = "<a href=""" & strLink & """ class=""list-group-item active text-left"">" & Generic.ToStr(row("MenuTitle")) & "</a>"
                '                PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '            Else
                '                strLink = "../secured/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                Dim strHyp As String = "<a href=""" & strLink & """ class=""list-group-item text-left"">" & Generic.ToStr(row("MenuTitle")) & tStatus & "</a>"
                '                PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '            End If
                '        End If

                '    Case 2
                '        If _transactionID = 0 Then
                '            If Generic.ToInt(row("OrderLevel")) > 1 Then
                '                strLink = "../securedmanager/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                Dim strHyp As String = "<a href=""" & strLink & """ disabled=""disabled"" class=""list-group-item text-left"">" & Generic.ToStr(row("MenuTitle")) & tStatus & "</a>"
                '                PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '            Else
                '                If Generic.ToStr(row("FormName")).ToLower() = FileName.ToLower() Then
                '                    strLink = "../securedmanager/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                    Dim strHyp As String = "<a href=""" & strLink & """ class=""list-group-item active text-left"">" & Generic.ToStr(row("MenuTitle")) & "</a>"
                '                    PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '                Else
                '                    strLink = "../securedmanager/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                    Dim strHyp As String = "<a href=""" & strLink & """ class=""list-group-item text-left"">" & Generic.ToStr(row("MenuTitle")) & "</a>"
                '                    PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '                End If
                '            End If
                '        Else

                '            If Generic.ToStr(row("FormName")).ToLower() = FileName.ToLower() Then
                '                strLink = "../securedmanager/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                Dim strHyp As String = "<a href=""" & strLink & """ class=""list-group-item active text-left"">" & Generic.ToStr(row("MenuTitle")) & "</a>"
                '                PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '            Else
                '                strLink = "../securedmanager/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                Dim strHyp As String = "<a href=""" & strLink & """ class=""list-group-item text-left"">" & Generic.ToStr(row("MenuTitle")) & tStatus & "</a>"
                '                PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '            End If
                '        End If
                '    Case 3
                '        If _transactionID = 0 Then
                '            If Generic.ToInt(row("OrderLevel")) > 1 Then
                '                strLink = "../securedself/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                Dim strHyp As String = "<a href=""" & strLink & """ disabled=""disabled"" class=""list-group-item text-left"">" & Generic.ToStr(row("MenuTitle")) & tStatus & "</a>"
                '                PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '            Else
                '                If Generic.ToStr(row("FormName")).ToLower() = FileName.ToLower() Then
                '                    strLink = "../securedself/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                    Dim strHyp As String = "<a href=""" & strLink & """ class=""list-group-item active text-left"">" & Generic.ToStr(row("MenuTitle")) & "</a>"
                '                    PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '                Else
                '                    strLink = "../securedself/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                    Dim strHyp As String = "<a href=""" & strLink & """ class=""list-group-item text-left"">" & Generic.ToStr(row("MenuTitle")) & "</a>"
                '                    PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '                End If
                '            End If
                '        Else

                '            If Generic.ToStr(row("FormName")).ToLower() = FileName.ToLower() Then
                '                strLink = "../securedself/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                Dim strHyp As String = "<a href=""" & strLink & """ class=""list-group-item active text-left"">" & Generic.ToStr(row("MenuTitle")) & "</a>"
                '                PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '            Else
                '                strLink = "../securedself/page_redirect.aspx?id=" & _transactionID & "&formname=" & Generic.ToStr(row("FormName")) & "&MenuType=" & fMenuType & "&Tablename=" & fTablename
                '                Dim strHyp As String = "<a href=""" & strLink & """ class=""list-group-item text-left"">" & Generic.ToStr(row("MenuTitle")) & tStatus & "</a>"
                '                PlaceHolder1.Controls.Add(New LiteralControl(strHyp))
                '            End If
                '        End If
                'End Select
                '-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                If Generic.ToStr(row("FormName")).ToLower() = FileName.ToLower() Then
                    lnk.CssClass = "list-group-item active text-left"
                    lnk.Text = Generic.ToStr(row("MenuTitle"))
                Else
                    lnk.CssClass = "list-group-item text-left"
                    lnk.Text = Generic.ToStr(row("MenuTitle")) & tStatus
                End If

                AddHandler lnk.Click, AddressOf lnk_Click
                PlaceHolder1.Controls.Add(lnk)
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
        Session("xFormName") = Generic.Split(lnk.CommandArgument, 3)
        'Response.Redirect(URL & "?id=" & _transactionID)
        Response.Redirect("~/securedSelf/" + Session("xFormName") + "?id=" & _transactionID & "&tModify=false&IsClickMain=1")
    End Sub

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        If _content IsNot Nothing Then
            _content.InstantiateIn(PlaceHolder2)
        End If

        If _header IsNot Nothing Then
            _header.InstantiateIn(PlaceHolder3)
        End If

        PlaceHolder3.Visible = _headervisible

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