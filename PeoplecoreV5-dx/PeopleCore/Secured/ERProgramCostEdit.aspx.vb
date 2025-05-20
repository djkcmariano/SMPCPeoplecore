Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting

Partial Class Secured_ERProgramCostEdit
    Inherits System.Web.UI.Page


    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer

    Private Sub PopulateGrid()

        If txtIsPosted.Checked = True Then
            lnkAdd.Visible = False
            lnkDelete.Visible = False
        Else
            lnkAdd.Visible = True
            lnkDelete.Visible = True
        End If

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EERProgramCost_Web", UserNo, TransNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub


    'Populate Combo box
    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EERProgram_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "ERProgramList.aspx", "EERProgram")
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
            PopulateTabHeader()
        End If
        PopulateGrid()

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EERProgramCost_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "ERProgramList.aspx", "EERProgram") Then
            Dim lnk As New LinkButton, IsEnabled As Boolean = False
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Generic.ClearControls(Me, "Panel2")
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ERProgramCostNo"})))

            'Enable or Disable Controls
            If txtIsPosted.Checked = True Then
                IsEnabled = False
            Else
                IsEnabled = True
            End If
            Generic.EnableControls(Me, "Panel2", IsEnabled)
            btnSave.Enabled = IsEnabled

            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub



    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "ERProgramList.aspx", "EERProgram") Then

            Generic.ClearControls(Me, "Panel2")
            mdlShow.Show()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If
    End Sub


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "ERProgramList.aspx", "EERProgram") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ERProgramCostNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EERProgramCost", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    'Submit record
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecord() As Integer
        Dim ERProgramCostNo As Integer = Generic.ToInt(Me.txtERProgramCostNo.Text)
        Dim particulars As String = Generic.ToStr(Me.txtParticulars.Text)
        Dim programcosttypeno As Integer = Generic.ToInt(Me.cboERProgramCostTypeNo.SelectedValue)
        Dim cost As Double = Generic.ToDec(Me.txtCost.Text)

        If SQLHelper.ExecuteNonQuery("EERProgramCost_WebSave", UserNo, TransNo, ERProgramCostNo, particulars, programcosttypeno, cost) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False

        End If
    End Function

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

End Class






