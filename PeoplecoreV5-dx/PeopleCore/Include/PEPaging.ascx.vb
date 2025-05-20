Imports clsLib
Imports System.Data
Imports System.IO


<ParseChildren(True)> _
Partial Class Include_PEPaging
    Inherits System.Web.UI.UserControl

    Dim pestandardmainno As Integer = 0
    Dim pestandardcateno As Integer = 0
    Dim pecatetypeno As Integer = 0
    Dim pereviewmainno As Integer = 0
    Dim pereviewno As Integer = 0
    Dim pereviewevaluatorno As Integer = 0
    Dim TabNo As Integer = 0
    Dim IsSample As Boolean = False

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

        If FileName = "pestandardform.aspx" Then
            IsSample = True
        End If

        Try
            dt = SQLHelper.ExecuteDataTable("EMenu_PETab", Generic.ToInt(Session("OnlineUserNo")), pestandardmainno, ComponentNo, IsSample)
            For Each row As DataRow In dt.Rows
                Dim lnk As New LinkButton

                If 1 = TabNo Then
                    lnkPrevious.Enabled = False
                Else
                    lnkPrevious.Enabled = True
                End If

                Dim TotalPage As Integer = 0
                TotalPage = Generic.ToInt(row("TotalPage"))
                If TotalPage = TabNo Then
                    lnkNext.Enabled = False
                Else
                    lnkNext.Enabled = True
                End If

                If Generic.ToStr(row("RowNo")) = TabNo Then
                    lblPage.Text = "Page " & TabNo.ToString & " of " & TotalPage.ToString
                    lnk.CssClass = "paginate_active"
                Else
                    lnk.CssClass = "paginate_button"
                End If





                Select Case Generic.ToStr(ComponentNo)
                    Case 1
                        lnk.CommandArgument = "~/secured/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName") & "|" & Generic.ToStr(row("RowNo")) & "|" & Generic.ToStr(row("PEStandardCateNo")) & "|" & Generic.ToStr(row("PECateTypeNo")))
                    Case 2
                        lnk.CommandArgument = "~/securedmanager/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName") & "|" & Generic.ToStr(row("RowNo")) & "|" & Generic.ToStr(row("PEStandardCateNo")) & "|" & Generic.ToStr(row("PECateTypeNo")))
                    Case 3
                        lnk.CommandArgument = "~/securedself/" & Generic.ToStr(row("FormName")) & "|" & Generic.ToStr(row("MenuType")) & "|" & Generic.ToStr(row("TableName") & "|" & Generic.ToStr(row("RowNo")) & "|" & Generic.ToStr(row("PEStandardCateNo")) & "|" & Generic.ToStr(row("PECateTypeNo")))
                End Select

                If pestandardmainno = 0 Then
                    If Generic.ToInt(row("OrderLevel")) > 1 Then
                        lnk.Enabled = False
                    End If
                End If

                lnk.Text = Generic.ToStr(row("MenuTitle"))
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
        'Session("xMenuType") = Generic.Split(lnk.CommandArgument, 1)
        'Session("xTableName") = Generic.Split(lnk.CommandArgument, 2)

        Dim PETabNo As Integer = 0
        PETabNo = Generic.Split(lnk.CommandArgument, 3)
        pestandardcateno = Generic.Split(lnk.CommandArgument, 4)
        pecatetypeno = Generic.Split(lnk.CommandArgument, 5)
        Response.Redirect(URL & "?TabNo=" & PETabNo & "&pestandardmainno=" & pestandardmainno & "&pestandardcateno=" & pestandardcateno & "&pecatetypeno=" & pecatetypeno & "&pereviewmainno=" & pereviewmainno & "&pereviewno=" & pereviewno & "&pereviewevaluatorno=" & pereviewevaluatorno)
    End Sub

    Protected Sub lnk_Next(sender As Object, e As EventArgs)
        PopulatePaging(TabNo + 1)
    End Sub

    Protected Sub lnk_Previous(sender As Object, e As EventArgs)
        PopulatePaging(TabNo - 1)
    End Sub

    Private Sub PopulatePaging(PETabNo As Integer)

        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        Dim Folder As String = FileInfo.Directory.Name
        Dim FileName As String = Path.GetFileName(FileInfo.ToString)
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

        If FileName = "pestandardform.aspx" Then
            IsSample = True
        End If

        Dim FormName As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EMenu_PETab", Generic.ToInt(Session("OnlineUserNo")), pestandardmainno, ComponentNo, IsSample)
        For Each row As DataRow In dt.Select("RowNo=" & PETabNo)
            FormName = Generic.ToStr(row("Formname"))
            pestandardcateno = Generic.ToStr(row("pestandardcateno"))
            pecatetypeno = Generic.ToStr(row("PECateTypeNo"))
        Next

        Response.Redirect(FormName & "?TabNo=" & PETabNo & "&pestandardmainno=" & pestandardmainno & "&pestandardcateno=" & pestandardcateno & "&pecatetypeno=" & pecatetypeno & "&pereviewmainno=" & pereviewmainno & "&pereviewno=" & pereviewno & "&pereviewevaluatorno=" & pereviewevaluatorno)

    End Sub

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        pestandardmainno = Generic.ToInt(Request.QueryString("pestandardmainno"))
        pestandardcateno = Generic.ToInt(Request.QueryString("pestandardcateno"))
        pecatetypeno = Generic.ToInt(Request.QueryString("pecatetypeno"))
        pereviewmainno = Generic.ToInt(Request.QueryString("pereviewmainno"))
        pereviewno = Generic.ToInt(Request.QueryString("pereviewno"))
        pereviewevaluatorno = Generic.ToInt(Request.QueryString("pereviewevaluatorno"))
        TabNo = Generic.ToInt(Request.QueryString("TabNo"))

        If TabNo <= 0 Then
            TabNo = 1
        End If

        PopulateTab()
    End Sub
End Class