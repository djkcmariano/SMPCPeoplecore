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
    Dim menumassno As Integer = 0
    Dim tabno As Integer = 0
    Dim isgroup As Integer = 0

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
        Dim ComponentNo As Integer
        Dim IsSample As Boolean = False

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
            dt = SQLHelper.ExecuteDataTable("EMenuMass_Web", Generic.ToInt(Session("xPayLocNo")), 1)
            For Each row As DataRow In dt.Rows
                Dim lnk As New LinkButton

                Select Case Generic.ToStr(ComponentNo)
                    Case 1
                        lnk.CommandArgument = "~/secured/" & FileName & "|" & Generic.ToStr(row("MenuMassNo"))
                    Case 2
                        lnk.CommandArgument = "~/securedmanager/" & FileName & "|" & Generic.ToStr(row("MenuMassNo"))
                    Case 3
                        lnk.CommandArgument = "~/securedself/" & FileName & "|" & Generic.ToStr(row("MenuMassNo"))
                End Select

                Dim xdt As DataTable
                Dim tStatus As String = ""
                xdt = SQLHelper.ExecuteDataTable("EMenuMass_WebCount", Generic.ToInt(Session("OnlineUserNo")), _transactionID, Generic.ToStr(row("MenuMassNo")), tabno, isgroup, Generic.ToInt(Session("xPayLocNo")))
                For Each xrow As DataRow In xdt.Rows
                    tStatus = Generic.ToStr(xrow("tStatus"))
                Next

                If Generic.ToStr(row("MenuMassNo")) = menumassno Then
                    lnk.CssClass = "list-group-item active text-left"
                    lnk.Text = Generic.ToStr(row("ModuleTitle"))
                Else
                    lnk.CssClass = "list-group-item text-left"
                    lnk.Text = Generic.ToStr(row("ModuleTitle")) & tStatus
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
        menumassno = Generic.Split(lnk.CommandArgument, 1)
        Response.Redirect(URL & "?id=" & _transactionID & "&menumassno=" & menumassno & "&tabno=" & tabno & "&isgroup=" & isgroup)

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
        menumassno = Generic.ToInt(Request.QueryString("menumassno"))
        tabno = Generic.ToInt(Request.QueryString("tabno"))
        isgroup = Generic.ToInt(Request.QueryString("isgroup"))

        If _TransactionNo > 0 Then
            _transactionID = _TransactionNo
        Else
            _transactionID = Generic.ToInt(Request.QueryString("id"))
        End If
        PopulateTab()
    End Sub
End Class