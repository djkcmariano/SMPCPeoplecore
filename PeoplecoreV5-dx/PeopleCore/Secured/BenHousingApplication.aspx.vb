Imports System.Data
Imports System.Math
Imports System.Threading
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_BenHousingApplication
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Dim Required As String = "form-control required"
    Dim NotRequired As String = "form-control"

    Dim RequiredNumber As String = "form-control number required"
    Dim NotRequiredNumber As String = "form-control number"

    Protected Sub PopulateGrid()

        Try
            lnkCancel.Visible = False
            If Generic.ToInt(cboTabNo.SelectedValue) = 1 Then
                lnkCancel.Visible = True
            End If
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EHousingApplication_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHousingApplication_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
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

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.EnableControls(Me, "Panel1", True)

            Generic.ClearControls(Me, "Panel1")

            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))


            lnkSave.Visible = True

            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Private Function SaveRecord() As Boolean

        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim DateFiled As String = Generic.ToStr(txtDateFiled.Text)
        Dim HousingTypeNo As Integer = Generic.ToInt(cboHousingTypeNo.SelectedValue)
        Dim Amount As Double = Generic.ToDec(txtAmount.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)

        If SQLHelper.ExecuteNonQuery("EHousingApplication_WebSave", UserNo, Generic.ToInt(txtCode.Text), EmployeeNo, DateFiled, HousingTypeNo, Amount, Remarks, PayLocNo) Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If

    End Function

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then

            Dim lnk As New LinkButton
            lnk = sender

            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)

            Generic.ClearControls(Me, "Panel1")

            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))

            Generic.EnableControls(Me, "Panel1", IsEnabled)

            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"HousingApplicationNo"})))

            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"HousingApplicationNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EHousingApplication", UserNo, item)
                i = i + 1
            Next

            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim dt As DataTable, IsReady As Boolean = False
            Dim str As String = "", i As Integer = 0, k As Integer = 0
            For j As Integer = 0 To grdMain.VisibleRowCount - 1
                If grdMain.Selection.IsRowSelected(j) Then
                    Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "HousingApplicationNo"))
                    dt = SQLHelper.ExecuteDataTable("EHousingApplication_WebCancel", UserNo, CType(item, Integer), PayLocNo)
                    grdMain.Selection.UnselectRow(j)
                    i = i + 1
                End If
            Next

            PopulateGrid()
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessCancel, Me)
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub


    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim dt As DataTable, IsReady As Boolean = False
            Dim str As String = "", i As Integer = 0, k As Integer = 0
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"HousingApplicationNo"})
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("EHousingApplication_WebPost", UserNo, CType(item, Integer), PayLocNo) Then
                    i = i + 1
                End If

            Next

            PopulateGrid()

            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccesPost, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

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

    Private Sub PopulateDropDown()

        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 12)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

        'Try
        '    cboHousingTypeNo.DataSource = SQLHelper.ExecuteDataSet("EHousingType_WebLookup", UserNo, Generic.ToInt(cboHousingTypeNo.SelectedValue), False, PayLocNo)
        '    cboHousingTypeNo.DataTextField = "tDesc"
        '    cboHousingTypeNo.DataValueField = "tno"
        '    cboHousingTypeNo.DataBind()
        'Catch ex As Exception
        'End Try

    End Sub

    Protected Sub lnkAttachment_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Response.Redirect("~/secured/frmFileUpload.aspx?id=" & Generic.Split(lnk.CommandArgument, 0) & "&display=" & Generic.Split(lnk.CommandArgument, 1))
    End Sub

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


    Protected Sub lnkApproved_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"HousingApplicationNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, "", 2)
            i = i + 1
        Next

        If i > 0 Then
            Dim url As String = "BenHousingApplication.aspx"
            MessageBox.SuccessResponse("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me, url)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Protected Sub lnkDisApproved_Click(sender As Object, e As System.EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"HousingApplicationNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, "", 3)
            i = i + 1
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDisapproved, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Private Sub ApproveTransaction(tId As Integer, remarks As String, approvalStatNo As Integer)
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("EHousingApplication_WebApproved", UserNo, tId, approvalStatNo, remarks)
    End Sub

#End Region



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

    Protected Sub lnkView_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "HousingApplicationNo")
        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, id.ToString()))
        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=1307&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub


#End Region

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub


End Class
