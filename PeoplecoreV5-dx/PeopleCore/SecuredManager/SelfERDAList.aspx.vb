Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class SecuredManager_SelfERDAList
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim clsArray As New clsBase.clsArray
    Dim xScript As String = ""

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()

        Dim tabOrderNo As String = "", tstatus As Integer = 0
        tabOrderNo = Generic.ToStr(cboTabNo.SelectedValue)
        If tabOrderNo = "" Then
            tstatus = 1
        Else
            tstatus = CInt(tabOrderNo)
        End If

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EDA_WebSelf", UserNo, tstatus, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        Session(xScript & "TabNo") = tabOrderNo

    End Sub

    Private Sub PopulateCombo()
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)

        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 31)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        'AccessRights.CheckUser(UserNo)
        Permission.IsAuthenticatedSuperior()

        clsArray.myPage.Pagename = Request.ServerVariables("SCRIPT_NAME")
        clsArray.myPage.Pagename = clsArray.GetPath(clsArray.myPage.Pagename)
        xScript = clsArray.myPage.Pagename

        If Not IsPostBack Then
            cboTabNo.Text = Generic.ToStr(Session(xScript & "TabNo"))
            PopulateCombo()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub


    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Response.Redirect("~/securedmanager/SelfERDAEdit.aspx?id=" & container.Grid.GetRowValues(container.VisibleIndex, New String() {"DANo"}))

    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
        Response.Redirect("~/securedmanager/SelfERDAEdit.aspx?id=0")
        'Else
        'MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        'End If
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        TransNo = Generic.ToInt(ViewState("TransNo"))

        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DANo"})
        Dim str As String = ""
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EDA", UserNo, item)
            Generic.DeleteRecordAuditCol("EDADeti", UserNo, "DADetiNo", item)
            DeleteCount = DeleteCount + 1
        Next

        If DeleteCount > 0 Then
            PopulateGrid()
            MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If
        'Else
        'MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        'End If
    End Sub

    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DaNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("EDA_WebPost", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccesPost, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

#Region "print"

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender

        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, Generic.ToInt(lnk.CommandArgument)))
        sb.Append("<script>")
        sb.Append("window.open('RptTemplateViewerDX.aspx?reportno=433&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub
#End Region

End Class



