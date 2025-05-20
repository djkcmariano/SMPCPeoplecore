Imports clsLib
Imports System.Data
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports System.IO
Imports DevExpress.Web

Partial Class Secured_AppList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim EmployeeNo As Integer
    Dim PayLocNo As Integer = 0
    Dim xSQLHelper As New clsBase.SQLHelper

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        'grdMain.DataSourceID = SqlDataSource1.ID
        'Generic.PopulateSQLDatasource("EApplicant_Web", SqlDataSource1, UserNo.ToString(), Generic.ToInt(cboTabNo.SelectedValue).ToString(), PayLocNo.ToString())

        'grdMain.DataSource = SQLHelper.ExecuteDataTable("EApplicant_Web", UserNo.ToString(), Generic.ToInt(cboTabNo.SelectedValue).ToString(), PayLocNo.ToString())
        'grdMain.DataBind()

        grdMain.DataSource = xSQLHelper.ExecuteDataset(SQLHelper.ConSTR, "EApplicant_Web", UserNo.ToString(), Generic.ToInt(cboTabNo.SelectedValue).ToString(), PayLocNo.ToString())
        grdMain.DataBind()

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim URL As String
        lnk = sender        
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantNo"})))
        If URL <> "" Then
            Response.Redirect(URL)
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 7)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim URL As String
            URL = Generic.GetFirstTab("0")
            If URL <> "" Then
                Response.Redirect(URL)
            End If
        End If
    End Sub

    Protected Sub lnkAttachment_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Response.Redirect("~/secured/frmFileUpload.aspx?id=" & Generic.Split(lnk.CommandArgument, 0) & "&display=" & Generic.Split(lnk.CommandArgument, 1))
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender

        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantNo"}))
        ViewState("ApplicantNo") = obj

        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, Generic.ToInt(ViewState("ApplicantNo"))))

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewercode.aspx?reportcode=AppPDS&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Protected Sub lnkPrint2_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Protected Sub lnkPrint2_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantNo"}))
        ViewState("ApplicantNo") = obj

        Dim param As String = Generic.ReportParam(
                                                  New ReportParameter(ReportParameter.Type.int, Generic.ToInt(ViewState("ApplicantNo")))
                                                  )

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewercode.aspx?reportcode=BSPForm&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())

    End Sub

    Private Shared Function GetIniFile() As String

        Dim iInitArr As String
        Dim i As Integer
        Dim fs As FileStream
        Dim ConnectionString As String = ""
        Dim filename = HttpContext.Current.Server.MapPath("~/secured/connectionstr/") & "peoplecore.ini"
        Dim fservername As String = ""
        Dim fdatabasename As String = ""
        Dim fsqllogin As String = ""
        Dim fsqlpass As String = ""

        If Not IO.File.Exists(filename) Then
            HttpContext.Current.Response.Redirect("~/connection.aspx")
            Return ""
        End If

        fs = New FileStream(filename, FileMode.Open, FileAccess.Read)
        Dim l As Integer = 0, ftext As String = ""
        Dim d As New StreamReader(fs)
        Try
            d.BaseStream.Seek(0, SeekOrigin.Begin)
            If d.Peek() > 0 Then
                While d.Peek() > -1
                    i = d.Peek
                    ftext = PeopleCoreCrypt.Decrypt(d.ReadLine())
                    iInitArr = ftext
                    If l = 0 Then
                        fservername = iInitArr
                    ElseIf l = 1 Then
                        fdatabasename = iInitArr
                    ElseIf l = 2 Then
                        fsqllogin = iInitArr
                    ElseIf l = 3 Then
                        fsqlpass = iInitArr
                    End If
                    l = l + 1
                End While
                ConnectionString = "Password=" & fsqlpass & ";Persist Security Info=True;User ID=" & fsqllogin & " ;Initial Catalog=" & fdatabasename & " ;Data Source=" & fservername & ""
            End If
            d.Close()
        Catch
            d.Close()
        End Try
        Return ConnectionString

    End Function

End Class
