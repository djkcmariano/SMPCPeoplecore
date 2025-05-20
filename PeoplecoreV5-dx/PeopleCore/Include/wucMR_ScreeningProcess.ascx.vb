Imports clsLib
Imports System.Data
Imports System.IO


<ParseChildren(True)> _
Partial Class Include_Tab
    Inherits System.Web.UI.UserControl

    Dim mrno As Integer = 0
    Dim pereviewevaluatorno As Integer = 0
    Dim TabNo As Integer = 0
    Dim IsSample As Boolean = False
    Dim mrInterviewNo As Integer = 0

    Private Sub PopulateTab()
        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        Dim Folder As String = FileInfo.Directory.Name
        Dim FileName As String = Path.GetFileName(FileInfo.ToString)
        Dim MenuType As String = Left(Generic.ToStr(Session("xMenuType")), 4)
        Dim dt As DataTable
        Dim ComponentNo As Integer
        'switch folder
        Select Case Folder
            Case "secured"
                ComponentNo = 1
            Case "securedmanager"
                ComponentNo = 2
            Case "securedself"
                ComponentNo = 3
        End Select

        Try
            dt = SQLHelper.ExecuteDataTable("EMR_Web_Interview", Generic.ToInt(Session("OnlineUserNo")), mrno)
            For Each row As DataRow In dt.Rows
                Dim lnk As New LinkButton

                If Generic.ToStr(row("RowNo")) = TabNo Then
                    lnk.CssClass = "list-group-item active text-left"
                Else
                    lnk.CssClass = "list-group-item text-left"
                End If

                Select Case Generic.ToStr(ComponentNo)
                    Case 1
                        lnk.CommandArgument = "~/secured/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName") & "|" & Generic.ToStr(row("RowNo")) & "|" & Generic.ToStr(row("mrInterviewNo")))
                    Case 2
                        lnk.CommandArgument = "~/securedmanager/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName") & "|" & Generic.ToStr(row("RowNo")) & "|" & Generic.ToStr(row("mrInterviewNo")))
                    Case 3
                        lnk.CommandArgument = "~/securedself/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName") & "|" & Generic.ToStr(row("RowNo")) & "|" & Generic.ToStr(row("mrInterviewNo")))
                End Select

                lnk.Text = Generic.ToStr(row("ApplicantStandardMainDesc"))
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

        Dim PETabNo As Integer = 0
        PETabNo = Generic.Split(lnk.CommandArgument, 3)
        mrInterviewNo = Generic.Split(lnk.CommandArgument, 4)
        Response.Redirect(URL & "?TabNo=" & PETabNo & "&Id=" & mrno & "&mrInterviewNo=" & mrInterviewNo)
    End Sub

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        mrno = Generic.ToInt(Request.QueryString("Id"))
        mrInterviewNo = Generic.ToInt(Request.QueryString("mrInterviewNo"))
        TabNo = Generic.ToInt(Request.QueryString("TabNo"))

        If TabNo <= 0 Then
            TabNo = 1
        End If

        PopulateTab()
    End Sub
End Class