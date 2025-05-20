Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting

Partial Class Secured_PayBonusDeduPolicyList
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
        _dt = SQLHelper.ExecuteDataTable("EPayBonusDeduPolicy_Web", UserNo, TransNo)
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
        AccessRights.CheckUser(UserNo, "PayBonusList.aspx", "EPay")
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
            PopulateTabHeader()
        End If
        PopulateGrid()

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayBonusDeduPolicy_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayBonusList.aspx", "EPay") Then
            Dim lnk As New LinkButton, IsEnabled As Boolean = False
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Generic.ClearControls(Me, "Panel2")
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBonusDeduPolicyNo"})))

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

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PayBonusList.aspx", "EPay") Then

            Generic.ClearControls(Me, "Panel2")
            mdlShow.Show()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If
    End Sub


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "PayBonusList.aspx", "EPay") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayBonusDeduPolicyNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayBonusDeduPolicy", UserNo, item)
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
        If SQLHelper.ExecuteNonQuery("EPayBonusDeduPolicy_WebSave", UserNo, Generic.ToInt(txtPayBonusDeduPolicyNo.Text), TransNo, _
                                     Generic.ToDec(Me.txtAbsent.Text), Generic.ToDec(Me.txtLate.Text), Generic.ToDec(Me.txtUnder.Text), _
                                     0, 0, txtStartDateX.Text, txtEndDateX.Text) > 0 Then
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

#Region "Detail"

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBonusDeduPolicyNo", "Code"})
        ViewState("TransNo") = obj(0)
        'Response.Redirect("paytemplate_bonus_detl.aspx?id=" & ViewState("TransNo").ToString)
        PopulateGrid_Factor()
    End Sub
#End Region

#Region "Factor"

    Private Sub PopulateGrid_Factor(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayBonusDeduPolicyDeti_Web", UserNo, ViewState("TransNo"))
        Me.grdFactor.DataSource = _dt
        Me.grdFactor.DataBind()

    End Sub
    Private Sub PopulateData_Factor(Id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayBonusDeduPolicyDeti_WebOne", UserNo, Id)
        Generic.PopulateData(Me, "Panel3", dt)
        For Each row As DataRow In dt.Rows

            Textbox1.Text = Generic.ToStr(row("Code"))
        Next
    End Sub

    Protected Sub lnkAddfactor_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "Panel3")
        ModalPopupExtender2.Show()
    End Sub

    Protected Sub lnkDeletefactor_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdFactor.GetSelectedFieldValues(New String() {"PayBonusDeduPolicyDetiNo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EPayBonusDeduPolicyDeti", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
    End Sub

    Protected Sub lnkEditfactor_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton, IsEnabled As Boolean = False
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Generic.ClearControls(Me, "Panel3")
        PopulateData_Factor(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBonusDeduPolicyDetiNo"})))
        ModalPopupExtender2.Show()

    End Sub


    Protected Sub lnkSavefactor_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim sfrom As Double = Generic.ToDbl(txtSFrom.Text)
        Dim sto As Double = Generic.ToDbl(txtSTo.Text)
        Dim percentFactor As Double = Generic.ToDbl(txtPercentFactor.Text)
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable


        Dim dt As DataTable, error_num As Integer = 0, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("EPayBonusDeduPolicyDeti_WebSave", UserNo, Generic.ToInt(txtPayBonusDeduPolicyDetiNo.Text), Generic.ToInt(ViewState("TransNo")), Generic.ToInt(sfrom), _
                                      Generic.ToInt(sto), Generic.ToDec(percentFactor))
        For Each row As DataRow In dt.Rows
            Retval = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                Retval = False
            End If

        Next
        If Retval = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If Retval = True Then
            PopulateGrid_Factor()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub


#End Region


End Class






