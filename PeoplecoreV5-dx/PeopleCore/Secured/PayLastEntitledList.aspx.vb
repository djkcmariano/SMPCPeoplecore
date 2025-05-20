Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting

Partial Class Secured_PayLastEntitledList
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
        _dt = SQLHelper.ExecuteDataTable("EPayLastEntitled_Web", UserNo, TransNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub


    'Populate Combo box
    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPay_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "PayLastList.aspx", "EPay")
        If Not IsPostBack Then

            PopulateDropdownList()
            PopulateTabHeader()
        End If
        PopulateGrid()

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    'Populate Combo box
    Private Sub PopulateDropdownList()
        Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
        Generic.PopulateDropDownList(UserNo, Me, "phbonus", PayLocNo)
        Try
            cboLeaveTypeNo.DataSource = SQLHelper.ExecuteDataSet("ELeaveType_WebLookupBalance", UserNo, PayLocNo)
            cboLeaveTypeNo.DataTextField = "tDesc"
            cboLeaveTypeNo.DataValueField = "tNo"
            cboLeaveTypeNo.DataBind()
        Catch ex As Exception
        End Try


    End Sub
    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayLastEntitled_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
        For Each row As DataRow In dt.Rows
            cboBenefitSourceNo.Text = Generic.ToStr(row("BenefitSourceNo"))
            cboLeaveTypeNo.Text = Generic.ToStr(row("LeaveTypeNo"))
            cboBonusTypeNo.Text = Generic.ToStr(row("BonusTypeNo"))
        Next
        showBenefitSource(Generic.ToInt(cboBenefitSourceNo.SelectedValue))

    End Sub
    Private Sub showBenefitSource(fsourceno As Integer)
        phleave.Visible = False
        phbonus.Visible = False
        If fsourceno = 2 Then
            phleave.Visible = True
        ElseIf fsourceno = 1 Then
            phbonus.Visible = True
        End If
    End Sub
    Protected Sub cboBenefitSourceNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        showBenefitSource(Generic.ToInt(cboBenefitSourceNo.SelectedValue))
        mdlShow.Show()
    End Sub
    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayLastList.aspx", "EPay") Then
            Dim lnk As New LinkButton, IsEnabled As Boolean = False
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Generic.ClearControls(Me, "Panel2")
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayLastEntitledNo"})))

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

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PayLastList.aspx", "EPay") Then
            PopulateDropdownList()
            Generic.ClearControls(Me, "Panel2")
            mdlShow.Show()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If
    End Sub


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "PayLastList.aspx", "EPay") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayLastEntitledNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayLastEntitled", UserNo, item)
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
        SaveRecord
    End Sub

    Private Function SaveRecord() As Integer
        Dim leavetypeno As Integer = Generic.ToInt(cboLeaveTypeNo.SelectedValue)
        Dim BenefitSourceNo As Integer = Generic.ToInt(cboBenefitSourceNo.SelectedValue)
        Dim minserviceYear As Double = Generic.ToDec(txtMinServiceYear.Text)

        Dim dt As DataTable
        Dim RetVal As Boolean = True, error_num As Integer = 0, error_message As String = ""

        dt = SQLHelper.ExecuteDataTable("EPayLastEntitled_WebSave", UserNo, Generic.CheckDBNull(txtPayLastEntitledNo.Text, clsBase.clsBaseLibrary.enumObjectType.IntType), TransNo, Generic.CheckDBNull(Me.cboBonusBasisNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Me.cboBonusTypeNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Me.txtPercentFactor.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(txtcStartDate.Text, clsBase.clsBaseLibrary.enumObjectType.StrType), Generic.CheckDBNull(txtcEndDate.Text, clsBase.clsBaseLibrary.enumObjectType.StrType), Generic.CheckDBNull(cboDateBaseNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType), leavetypeno, BenefitSourceNo, minserviceYear)
        For Each row As DataRow In dt.Rows
            RetVal = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                RetVal = False
            End If

        Next
        If RetVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
        Return RetVal
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






