Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_BenBenefitLoyaltyPolicy
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub PopulateGrid()

        Try
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EBenefitLoyaltyPolicy_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBenefitLoyaltyPolicy_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))

        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateDropDown()
        End If

        Try
            If Generic.ToInt(Me.cboTabNo.SelectedValue) > 0 Then
                Me.lnkDelete.Visible = False
                Me.lnkArchive.Visible = False
            Else
                lnkDelete.Visible = False
                Me.lnkArchive.Visible = True
            End If
        Catch ex As Exception

        End Try

        PopulateGrid()


        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try
            lnkSave.Enabled = True
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitLoyaltyPolicyNo", "IsEnabled"})
            Dim iNo As Integer = Generic.ToInt(obj(0))
            Dim IsEnabled As Boolean = Generic.ToBol(obj(1))
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitLoyaltyPolicyNo"})))
            lnkSave.Enabled = IsEnabled
            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BenefitLoyaltyPolicyNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EBenefitLoyaltyPolicy", UserNo, item)
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

    Protected Sub lnkArchive_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BenefitLoyaltyPolicyNo"})
            Dim str As String = "", i As Integer = 0, ii As Integer = 0
            Dim IsRowSelected As Boolean
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("EBenefitLoyaltyPolicy_WebArchive", UserNo, item, 1) > 0 Then
                    i = i + 1
                End If
            Next

            'For tcount = 0 To grdMain.VisibleRowCount - 1
            '    ii = Generic.ToInt(grdMain.GetRowValues(tcount, New String() {"BenefitLoyaltyPolicyNo"}))
            '    IsRowSelected = grdMain.Selection.IsRowSelected(tcount)
            '    If IsRowSelected = True Then
            '        If SQLHelper.ExecuteNonQuery("EBenefitLoyaltyPolicy_WebArchive", UserNo, ii, 1) > 0 Then
            '            i = i + 1
            '        End If
            '    End If
            'Next

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

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Private Function SaveRecord() As Boolean
        Dim dt As DataTable, retVal As Integer = 0, msG As String = ""
        dt = SQLHelper.ExecuteDataTable("EBenefitLoyaltyPolicy_WebSave", UserNo, Generic.ToInt(txtCode.Text), txtBenefitLoyaltyPolicyCode.Text, txtBenefitLoyaltyPolicyDesc.Text, _
                                     Generic.ToDec(txtFromYears.Text), Generic.ToDec(txtToYears.Text), Generic.ToDec(txtYearsGiven.Text), Generic.ToDec(txtAmount.Text), Generic.ToDec(txtAmountProrate.Text), Generic.ToStr(txtRemarks.Text), chkIsSuspended.Checked, chkIsArchived.Checked, Generic.ToInt(cboPayLocNo.SelectedValue), chkIsWithGoldRing.Checked)
        For Each row As DataRow In dt.Rows
            retVal = Generic.ToInt(row("retVal"))
            msG = Generic.ToStr(row("msG"))
        Next

        'If SQLHelper.ExecuteNonQuery("EBenefitHMOPlanType_WebSave", UserNo, Generic.ToInt(txtCode.Text), txtBenefitHMOPlanTypeCode.Text, txtBenefitHMOPlanTypeDesc.Text, _
        '                             Generic.ToDec(txtMBL.Text), Generic.ToDec(txtAddPremiumCost.Text), Generic.ToDec(txtPercentCost.Text), txtIsPrincipalPlan.Checked) > 0 Then
        '    Return True
        'Else
        '    Return False
        'End If

        If retVal = 0 Then
            Return True
        ElseIf retVal = 1 Or retVal = 2 Then
            MessageBox.Critical(msG, Me)
            Return False
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Function

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 30)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
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
End Class

