Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_BenBenefitHMOPlan
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub PopulateMain()

        Try
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EBenefitHMOPlanType_Web", UserNo, 0, PayLocNo)
            Me.grdHMOType.DataSource = _dt
            Me.grdHMOType.DataBind()
        Catch ex As Exception

        End Try

        PopulateGrid()

    End Sub

    Protected Sub PopulateGrid()

        Try
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EBenefitHMOPlan_Web", UserNo, Generic.ToInt(ViewState("TransNo")), PayLocNo)
            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBenefitHMOPlan_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
                txtNoOfDependent.Enabled = chkIsDependentPlan.Checked
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

        End If

        PopulateMain()


        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitHMOPlanTypeNo", "BenefitHMOPlanTypeDesc"})
        ViewState("TransNo") = obj(0)
        lbl.Text = "<b>" & obj(1) & "</b>"
        PopulateGrid()
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
            cboRoomTypeNo.Text = ""
            chkIsPrincipalPlan.Checked = True
            txtNoOfDependent.Enabled = chkIsDependentPlan.Checked
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
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitHMOPlanNo", "IsEnabled"})
            Dim iNo As Integer = Generic.ToInt(obj(0))
            Dim IsEnabled As Boolean = Generic.ToBol(obj(1))
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitHMOPlanNo"})))
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
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BenefitHMOPlanNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EBenefitHMOPlan", UserNo, item)
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

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Private Function SaveRecord() As Boolean
        Dim BenefitHMOPlanCode As String = Generic.ToStr(txtBenefitHMOPlanCode.Text)
        Dim BenefitHMOPlanDesc As String = Generic.ToStr(txtBenefitHMOPlanDesc.Text)
        Dim BenefitHMOTypeNo As Integer = Generic.ToInt(cboBenefitHMOTypeNo.SelectedValue)
        Dim RoomTypeNo As Integer = Generic.ToInt(cboRoomTypeNo.SelectedValue)
        Dim IsPrincipalPlan As Boolean = Generic.ToBol(chkIsPrincipalPlan.Checked)
        Dim NoOfDependent As Integer = Generic.ToInt(txtNoOfDependent.Text)
        Dim MBL As Double = Generic.ToDec(txtMBL.Text)
        Dim PercentCost As Double = Generic.ToDec(txtPercentCost.Text)
        Dim AddPremiumCost As Double = Generic.ToDec(txtAddPremiumCost.Text)
        Dim PHFee As Double = Generic.ToDec(txtPHFee.Text)
        Dim OrderLevel As Integer = Generic.ToInt(txtOrderLevel.Text)
        Dim NoOfPayments As Integer = Generic.ToInt(txtNoOfPayments.Text)
        Dim PayScheduleNo As Integer = Generic.ToInt(cboPayScheduleNo.SelectedValue)
        Dim ApplicableYear As Integer = Generic.ToInt(txtApplicableYear.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)
        Dim IsArchived As Boolean = Generic.ToBol(chkIsArchived.Checked)

        If SQLHelper.ExecuteNonQuery("EBenefitHMOPlan_WebSave", UserNo, Generic.ToInt(ViewState("TransNo")), Generic.ToInt(txtCode.Text), BenefitHMOPlanCode, BenefitHMOPlanDesc, BenefitHMOTypeNo, RoomTypeNo, IsPrincipalPlan, NoOfDependent, MBL, PercentCost, AddPremiumCost, PHFee, OrderLevel, NoOfPayments, PayScheduleNo, ApplicableYear, Remarks, IsArchived, PayLocNo) > 0 Then
            Return True
        Else
            Return False
        End If


    End Function


    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub rdo_CheckedChanged(sender As Object, e As System.EventArgs)
        txtNoOfDependent.Enabled = chkIsDependentPlan.Checked
        If chkIsDependentPlan.Checked = False Then
            txtNoOfDependent.Text = ""
            txtNoOfDependent.CssClass = "form-control number"
        Else
            txtNoOfDependent.CssClass = "form-control number required"
        End If
        ModalPopupExtender1.Show()
    End Sub
End Class

