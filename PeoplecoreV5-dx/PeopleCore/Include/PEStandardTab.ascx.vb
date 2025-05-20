Imports clsLib
Imports System.Data
Imports System.IO


<ParseChildren(True)> _
Partial Class Include_Tab
    Inherits System.Web.UI.UserControl

    Private _content As ITemplate = Nothing
    Private _header As ITemplate = Nothing
    Private _transactionID As String
    Private _headervisible As Boolean = True
    Private _TransactionNo As Integer = 0
    Dim pestandardmainno As Integer = 0
    Dim pestandardcateno As Integer = 0
    Dim pecatetypeno As Integer = 0
    Dim pereviewno As Integer = 0
    Dim peevaluatorno As Integer = 0
    Dim pecycleno As Integer = 0
    Dim isposted As Boolean = False

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

        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMenu_PEStandardTab", Generic.ToInt(Session("OnlineUserNo")), pestandardmainno)
            For Each row As DataRow In dt.Rows
                Dim lnk As New LinkButton

                If pecycleno = 0 Then 'Admin Review
                    pecycleno = 2
                End If

                lnk.CommandArgument = "~/secured/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName") & "|" & Generic.ToStr(row("RowNo")) & "|" & Generic.ToStr(row("PEStandardCateNo")) & "|" & Generic.ToStr(row("PECateTypeNo")) & "|" & Generic.ToStr(pecycleno) & "|" & Generic.ToStr(isposted))

                If (Generic.ToStr(row("PEStandardCateNo")) = pestandardcateno) Or (pestandardcateno = 0 And Generic.ToStr(row("RowNo")) = 1) Then
                    lnk.CssClass = "list-group-item active text-left"
                    lnk.Text = Generic.ToStr(row("MenuTitle"))
                Else
                    lnk.CssClass = "list-group-item text-left"
                    lnk.Text = Generic.ToStr(row("MenuTitle")) '& tStatus
                End If

                'lnk.CommandArgument = "~\" & Folder & "\" & Generic.ToStr(row("FormName"))
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
        pestandardcateno = Generic.Split(lnk.CommandArgument, 4)
        pecatetypeno = Generic.Split(lnk.CommandArgument, 5)
        pecycleno = Generic.Split(lnk.CommandArgument, 6)
        isposted = Generic.ToBol(Generic.Split(lnk.CommandArgument, 7))
        Response.Redirect(URL & "?id=" & pestandardmainno & "&pestandardcateno=" & pestandardcateno & "&pecatetypeno=" & pecatetypeno & "&pereviewno=" & pereviewno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&isposted=" & isposted)

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
        pestandardmainno = Generic.ToInt(Request.QueryString("id"))
        pestandardcateno = Generic.ToInt(Request.QueryString("pestandardcateno"))
        pecatetypeno = Generic.ToInt(Request.QueryString("pecatetypeno"))
        peevaluatorno = Generic.ToInt(Request.QueryString("peevaluatorno"))
        pecycleno = Generic.ToInt(Request.QueryString("pecycleno"))
        isposted = Generic.ToBol(Request.QueryString("isposted"))

        If _TransactionNo > 0 Then
            pestandardmainno = _TransactionNo
        Else
            pestandardmainno = Generic.ToInt(Request.QueryString("id"))
        End If
        PopulateTab()

        'imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(332) & "&tIndex=2"
    End Sub
End Class