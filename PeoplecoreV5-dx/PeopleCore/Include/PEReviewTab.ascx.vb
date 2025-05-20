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
    Dim pereviewmainno As Integer = 0
    Dim pereviewcateno As Integer = 0
    Dim pecatetypeno As Integer = 0
    Dim pereviewno As Integer = 0
    Dim peevaluatorno As Integer = 0
    Dim pecycleno As Integer = 0
    Dim componentno As Integer = 0
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
        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        Dim Folder As String = FileInfo.Directory.Name

        Dim FileName As String = Path.GetFileName(FileInfo.ToString)
        Dim MenuType As String = Left(Generic.ToStr(Session("xMenuType")), 4)
        Dim dt As DataTable
        Dim xComponentNo As Integer
        Dim IsSample As Boolean = False

        'switch folder
        Select Case Folder.ToLower
            Case "secured"
                xComponentNo = 1
            Case "securedmanager"
                xComponentNo = 2
            Case "securedself"
                xComponentNo = 3
        End Select

        If FileName = "pestandardform.aspx" Then
            IsSample = True
        End If

        Try
            dt = SQLHelper.ExecuteDataTable("EMenu_PEReviewTab", Generic.ToInt(Session("OnlineUserNo")), pereviewno, xComponentNo, IsSample, peevaluatorno)
            For Each row As DataRow In dt.Rows
                Dim lnk As New LinkButton

                Select Case Generic.ToStr(xComponentNo)
                    Case 1
                        lnk.CommandArgument = "~/secured/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName") & "|" & Generic.ToStr(row("RowNo")) & "|" & Generic.ToStr(row("PEReviewCateNo")) & "|" & Generic.ToStr(row("PECateTypeNo")))
                    Case 2
                        lnk.CommandArgument = "~/securedmanager/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName") & "|" & Generic.ToStr(row("RowNo")) & "|" & Generic.ToStr(row("PEReviewCateNo")) & "|" & Generic.ToStr(row("PECateTypeNo")))
                    Case 3
                        lnk.CommandArgument = "~/securedself/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName") & "|" & Generic.ToStr(row("RowNo")) & "|" & Generic.ToStr(row("PEReviewCateNo")) & "|" & Generic.ToStr(row("PECateTypeNo")))
                End Select

                If pereviewno = 0 Then
                    If Generic.ToInt(row("OrderLevel")) > 1 Then
                        lnk.Enabled = False
                    End If
                End If

                'Dim xdt As DataTable
                'Dim tStatus As String = ""
                'Dim MenuNo As Integer = Generic.ToInt(row("MenuNo"))
                'Dim SubTable As String = Generic.ToStr(row("SubTable"))
                'Dim SubCode As String = Generic.ToStr(row("SubCode"))
                'xdt = SQLHelper.ExecuteDataTable("EMenuSub_Tab", Generic.ToInt(Session("OnlineUserNo")), MenuNo, SubTable, SubCode, pereviewno)
                'For Each xrow As DataRow In xdt.Rows
                '    tStatus = Generic.ToStr(xrow("tStatus"))
                'Next

                If Generic.ToStr(row("PEReviewCateNo")) = pereviewcateno Then
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
        pereviewcateno = Generic.Split(lnk.CommandArgument, 4)
        pecatetypeno = Generic.Split(lnk.CommandArgument, 5)
        Response.Redirect(URL & "?pereviewmainno=" & pereviewmainno & "&pereviewcateno=" & pereviewcateno & "&pecatetypeno=" & pecatetypeno & "&pereviewno=" & pereviewno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&componentno=" & componentno & "&isposted=" & isposted)

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
        pereviewmainno = Generic.ToInt(Request.QueryString("pereviewmainno"))
        pereviewcateno = Generic.ToInt(Request.QueryString("pereviewcateno"))
        pecatetypeno = Generic.ToInt(Request.QueryString("pecatetypeno"))
        peevaluatorno = Generic.ToInt(Request.QueryString("peevaluatorno"))
        pecycleno = Generic.ToInt(Request.QueryString("pecycleno"))
        componentno = Generic.ToInt(Request.QueryString("componentno"))
        isposted = Generic.ToBol(Request.QueryString("isposted"))

        If _TransactionNo > 0 Then
            pereviewno = _TransactionNo
        Else
            pereviewno = Generic.ToInt(Request.QueryString("pereviewno"))
        End If
        PopulateTab()

        'imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(332) & "&tIndex=2"
    End Sub
End Class