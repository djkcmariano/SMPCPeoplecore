Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web


Partial Class Secured_BSProjectList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim PayLocNo As Integer

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable

        _dt = SQLHelper.ExecuteDataTable("BBSProject_Web", UserNo, "", PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        'If Not IsPostBack Then

        '    PopulateDropDown()
        'End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim URL As String
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ProjectNo"})))
        If Generic.ToStr(Session("xMenuType")) = "1302010000" Then
            Dim fProjectNo As String = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ProjectNo"}))
            Response.Redirect("BSProjectEdit.aspx?id=" & fProjectNo.ToString)
        Else
            If URL <> "" Then
                Response.Redirect(URL)
            End If
        End If

    End Sub

   

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            If Generic.ToStr(Session("xMenuType")) = "1302010000" Then
                Response.Redirect("BSProjectEdit.aspx")
            Else
                Dim URL As String
                URL = Generic.GetFirstTab("0")
                If URL <> "" Then
                    Response.Redirect(URL)
                End If
            End If

        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ProjectNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("BBSProjectClass", UserNo, "ProjectNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EProject", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

  


    

#Region "********Reports********"

    Protected Sub MyGridView_FillContextMenuItems(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuEventArgs)
        If e.MenuType = GridViewContextMenuType.Rows Then
            'e.Items.Add(e.CreateItem("Get Key", "GetKey"))
            e.Items.Clear()
        End If
    End Sub

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "ProjectNo")

        Dim param As String = ""
        Dim tProceed As Boolean = False
        Dim ReportNo As Integer = 0, dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EReport_WebViewer", UserNo, 398, "", id, PayLocNo)
        For Each row As DataRow In dt.Rows
            ReportNo = Generic.ToInt(row("ReportNo"))
            param = Generic.ToStr(row("param"))
            tProceed = Generic.ToStr(row("tProceed"))
        Next

        If tProceed = True Then
            sb.Append("<script>")
            sb.Append("window.open('rpttemplateviewer.aspx?reportno=" & ReportNo & "&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
            sb.Append("</script>")
            ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
        Else
            MessageBox.Warning("No access permission to view the report.", Me)
        End If

    End Sub

    

#End Region

#Region "********Detail Check All********"


    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs)
        If e.ButtonType <> ColumnCommandButtonType.SelectCheckbox Then
            Return
        End If
        Dim rowEnabled As Boolean = getRowEnabledStatus(e.VisibleIndex)
        e.Enabled = rowEnabled

        'If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
        '    Dim isSelected As Boolean = Convert.ToBoolean(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
        '    If isSelected Then

        '        grdMain.Selection.SetSelection(e.VisibleIndex, True)

        '    End If
        'End If
    End Sub
    Private Function getRowEnabledStatus(ByVal VisibleIndex As Integer) As Boolean
        Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(VisibleIndex, "IsEnabled"))
        If value = True Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Sub cbCheckAll_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim cb As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)
        cb.ClientSideEvents.CheckedChanged = String.Format("cbCheckAll_CheckedChanged")
        cb.Checked = False
        Dim count As Integer = 0
        Dim startIndex As Integer = grdMain.PageIndex * grdMain.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdMain.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)

        For i As Integer = startIndex To endIndex - 1
            If grdMain.Selection.IsRowSelected(i) Then
                count = count + 1
            End If
        Next i

        If count > 0 Then
            cb.Checked = True
        End If

    End Sub
    Protected Sub gridMain_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        Boolean.TryParse(e.Parameters, False)

        Dim startIndex As Integer = grdMain.PageIndex * grdMain.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdMain.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)
        For i As Integer = startIndex To endIndex - 1
            Dim rowEnabled As Boolean = getRowEnabledStatus(i)
            If rowEnabled AndAlso e.Parameters = "true" Then
                grdMain.Selection.SelectRow(i)
            Else
                grdMain.Selection.UnselectRow(i)
            End If
        Next i

    End Sub

#End Region


End Class



