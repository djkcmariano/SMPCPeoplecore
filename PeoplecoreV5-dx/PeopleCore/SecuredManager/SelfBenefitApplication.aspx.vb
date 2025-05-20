Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class SecuredSelf_SelfBenefitApplication
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub PopulateGrid()

        Try
            lnkDelete.Visible = False
            If Generic.ToInt(cboTabNo.SelectedValue) = 0 Then
                lnkDelete.Visible = True
            End If
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EBenefitApplication_WebManager", UserNo, "", Generic.ToInt(cboTabNo.SelectedValue))
            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBenefitApplication_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
                lnkSave.Enabled = Not Generic.ToBol(row("IsPosted"))
                cboPayScheduleNo.Text = Generic.ToStr(row("payscheduleNo"))
                txtNoOfpayments.Text = Generic.ToInt(row("noofpayments"))
                txtDeductionStart.Text = Generic.ToStr(row("DeductionStart"))
                cboPayDeductTypeNo.Text = Generic.ToInt(row("PayDeductTypeNo"))
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
        TransNo = Generic.ToInt(Request.QueryString("id"))

        Permission.IsAuthenticated()
        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "Panel1")
        cboBenefitTypeNo.Enabled = True
        cboBenefitStatNo.Text = ""
        cboBenefitStatNo.Enabled = False
        'Details
        Generic.ClearControls(Me, "ph1")
        PopulateForm()

        ModalPopupExtender1.Show()
       
    End Sub
    Private Function Populate_Category(Pid As Integer) As Integer
        Dim retval As Integer = 0
        Dim ds As DataSet
        ds = SQLHelper.ExecuteDataSet("EBenefitType_WebOne", UserNo, Pid)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                retval = Generic.ToInt(ds.Tables(0).Rows(0)("benefitCateNo"))
            End If
        End If
        ds = Nothing
        Return retval
    End Function
    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim dt As DataTable, id As Integer, count As Integer
        dt = SQLHelper.ExecuteDataTable("EBenefitApplication_WebSaveSelf", UserNo, Generic.ToInt(txtCode.Text), Generic.ToInt(hifEmployeeNo.Value), Generic.ToInt(cboBenefitTypeNo.SelectedValue), _
                                   txtRemarks.Text, txtDateFiled.Text, Generic.ToInt(cboBenefitStatNo.SelectedValue), Generic.ToDec(txtAmount.Text), _
                                   Generic.ToInt(cboPayDeductTypeNo.SelectedValue), Generic.ToDec(txtNoOfpayments.Text), Generic.ToStr(txtDeductionStart.Text), Generic.ToInt(cboPayScheduleNo.SelectedValue))

        For Each row As DataRow In dt.Rows
            id = Generic.ToInt(row("id"))
            count = Generic.ToInt(row("xcount"))
        Next

        'If Generic.ToInt(cboBenefitStatNo.Text) = 1 Then
        '    SQLHelper.ExecuteNonQuery("EBenefitApplication_WebPost", UserNo, id, Session("xPayLocNo"))
        'ElseIf Generic.ToInt(cboBenefitStatNo.Text) = 2 Then
        '    SQLHelper.ExecuteNonQuery("EBenefitApplication_WebCancel", UserNo, id, Session("xPayLocNo"))
        'End If

        If count > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)

            'Detail
            Dim cateId As Integer = Populate_Category(Generic.ToInt(cboBenefitTypeNo.SelectedValue))

            Select Case cateId
                Case "1"

                Case "2"
                    SaveHMO(id)
                Case "3"
                    SaveCAR(id)
                Case "4"
                    SaveRetirementPlan(id)
            End Select

            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
    
        Dim lnk As New LinkButton
        lnk = sender

        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)

        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitApplicationNo"})))
        PopulateForm()
        populateType(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitTypeNo"})))

        cboBenefitStatNo.Enabled = False
        If Generic.ToInt(cboBenefitTypeNo.SelectedValue) > 0 Then
            cboBenefitTypeNo.Enabled = False
        End If
        lnkSave.Enabled = True
        If Generic.ToInt(cboBenefitStatNo.SelectedValue) = 1 Or Generic.ToInt(cboBenefitStatNo.SelectedValue) = 2 Then
            lnkSave.Enabled = False
        End If
        'Detail
        Dim cateId As Integer = Populate_Category(Generic.ToInt(cboBenefitTypeNo.SelectedValue))
        Select Case cateId
            Case "1"

            Case "2"
                PopulateHMO()
            Case "3"
                PopulateCAR()
            Case "4"
                PopulateRetirementPlan()
        End Select

        ModalPopupExtender1.Show()
      
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BenefitApplicationNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EBenefitApplication", UserNo, item)
            Generic.DeleteRecordAuditCol("EBenefitRetirePlan", UserNo, "BenefitApplicationNo", item)
            Generic.DeleteRecordAuditCol("EBenefitApplicationHMO", UserNo, "BenefitApplicationNo", item)
            Generic.DeleteRecordAuditCol("EBenefitApplicationFleet", UserNo, "BenefitApplicationNo", item)
            i = i + 1
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

     
    End Sub


    Protected Sub lnkAttachment_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim i As Integer = 0
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)

        i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitApplicationNo"}))
        Response.Redirect("~/securedManager/frmFileUpload.aspx?id=" & i & "&display=" & i.ToString.PadLeft(8, "0"))
    End Sub

    Protected Sub lnkBenefitHMO_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim i As Integer = 0
        Dim EmpNo As Integer = 0
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)

        i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitApplicationNo"}))
        EmpNo = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo"}))

        Response.Redirect("~/securedManager/SelfBenefitApplicationHMODepe.aspx?id=" & i & "&employeeno=" & Generic.ToInt(EmpNo))
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList_Self(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
        Generic.PopulateDropDownList_Self(UserNo, Me, "ph3", Generic.ToInt(Session("xPayLocNo")))
        Generic.PopulateDropDownList_Self(UserNo, Me, "ph4", Generic.ToInt(Session("xPayLocNo")))
        Generic.PopulateDropDownList_Self(UserNo, Me, "phloan", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 17)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
        Try
            cboBenefitStatNo.DataSource = SQLHelper.ExecuteDataSet("EBenefitStat_Lookup")
            cboBenefitStatNo.DataTextField = "tDesc"
            cboBenefitStatNo.DataValueField = "tno"
            cboBenefitStatNo.DataBind()
        Catch ex As Exception
        End Try
        Try
            cboBenefitHMOPlanTypeNo.DataSource = SQLHelper.ExecuteDataSet("EBenefitHMOPlanType_WebLookup_Principal", UserNo)
            cboBenefitHMOPlanTypeNo.DataTextField = "tDesc"
            cboBenefitHMOPlanTypeNo.DataValueField = "tNo"
            cboBenefitHMOPlanTypeNo.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboPayDeductTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayDeductType_WebLookup", UserNo, Session("xpayLocNo"))
            cboPayDeductTypeNo.DataValueField = "tNo"
            cboPayDeductTypeNo.DataTextField = "tDesc"
            cboPayDeductTypeNo.DataBind()

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub cboBenefitTypeNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateForm()
        populateType(Generic.ToInt(cboBenefitTypeNo.SelectedValue))
        ModalPopupExtender1.Show()
    End Sub

    Private Sub populateType(pid As Integer)
        Dim ds As DataSet
        Dim IsForIncome As Boolean = False
        Dim IsTemplateIncome As Boolean = False
        Dim isForDeduction As Boolean = False
        Dim isLoan As Boolean = False
        Dim benefitCateNo As Integer = 0


        ds = SQLHelper.ExecuteDataSet("EBenefitType_WebOne", UserNo, pid)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                IsForIncome = Generic.ToBol(ds.Tables(0).Rows(0)("IsForIncome"))
                IsTemplateIncome = Generic.ToBol(ds.Tables(0).Rows(0)("IsTemplateIncome"))
                isForDeduction = Generic.ToBol(ds.Tables(0).Rows(0)("isForDeduction"))
                isLoan = Generic.ToBol(ds.Tables(0).Rows(0)("isLoan"))
                benefitCateNo = Generic.ToInt(ds.Tables(0).Rows(0)("benefitCateNo"))
            End If
        End If
        ds = Nothing

        If isForDeduction Then
            PlaceHolder2.Visible = True
        Else
            PlaceHolder2.Visible = False
        End If

        If benefitCateNo = 1 Then
            phallowance.Visible = True
        Else
            phallowance.Visible = False
        End If
        If benefitCateNo = 2 Then
            ph3.Visible = True
        Else
            ph3.Visible = False
        End If
        'Car plan
        If benefitCateNo = 3 Then
            ph4.Visible = True
        Else
            ph4.Visible = False
        End If
        'end of car plan
        'Retirement plan
        If benefitCateNo = 4 Then
            ph1.Visible = True
        Else
            ph1.Visible = False
        End If
        'end of retirement plan

        If benefitCateNo = 5 Or benefitCateNo = 0 Then
            phamount.Visible = True
        Else
            phamount.Visible = False
        End If

        If isLoan Then
            phloan.Visible = True
        Else
            phloan.Visible = False
        End If

    End Sub

    Protected Sub cboBenefitHMOPlanTypeNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ds As DataSet
        ds = SQLHelper.ExecuteDataSet("EBenefitHMOPlanType_Web_GetCost", UserNo, Generic.ToInt(cboBenefitHMOPlanTypeNo.SelectedValue))
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                txtPrincipalCost.Text = Generic.ToDec(ds.Tables(0).Rows(0)("AddPremiumCost"))

            End If
        End If
        ds = Nothing
        ModalPopupExtender1.Show()
    End Sub


    Private Sub PopulateForm()
        Try
            Generic.HidePlaceHolder(Me, "Panel1")
            Dim ph As New PlaceHolder, str As String
            str = "ph" & cboBenefitTypeNo.SelectedValue
            ph = Generic.FindControlRecursive(Panel1, str)
            If Not ph Is Nothing Then
                ph.Visible = True
            End If

            If Generic.ToInt(cboBenefitTypeNo.SelectedValue) = 1 Or Generic.ToInt(cboBenefitTypeNo.SelectedValue) = 3 Or Generic.ToInt(cboBenefitTypeNo.SelectedValue) = 4 Then
                phamount.Visible = False
            Else
                phamount.Visible = True
            End If


        Catch ex As Exception

        End Try
    End Sub


#Region "RETIREMENT PLAN"
    Private Function SaveRetirementPlan(TransNo As Integer) As Boolean
        If SQLHelper.ExecuteNonQuery("EBenefitRetirePlan_WebSave", UserNo, Generic.ToInt(hifBenefitRetirePlanNo.Value), TransNo, _
                                     Generic.ToInt(hifEmployeeNo.Value), Generic.ToInt(cboAdviceTypeNo.SelectedValue), Generic.ToDec(cboContriAmount.SelectedValue)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub PopulateRetirementPlan()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EBenefitRetirePlan_WebOne", UserNo, Generic.ToInt(txtCode.Text))
        Generic.PopulateData(Me, "ph1", dt)
    End Sub
#End Region

#Region "HMO PLAN"
    Private Function SaveHMO(TransNo As Integer) As Boolean
        If SQLHelper.ExecuteNonQuery("EBenefitApplicationHMO_WebSave", UserNo, Generic.ToInt(hifBenefitApplicationHMONo.Value), TransNo, _
                                     Generic.ToInt(cboBenefitHMOPlanTypeNo.SelectedValue), Generic.ToDec(txtPrincipalCost.Text), Generic.ToDec(txtNoOfPayments.Text), Generic.ToDec(txtPercentCost.Text)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub PopulateHMO()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EBenefitApplicationHMO_WebOne", UserNo, Generic.ToInt(txtCode.Text))
        Generic.PopulateData(Me, "ph3", dt)
    End Sub
#End Region

#Region "CAR PLAN"
    Private Function SaveCAR(TransNo As Integer) As Boolean
        If SQLHelper.ExecuteNonQuery("EBenefitApplicationFleet_WebSave", UserNo, Generic.ToInt(hifBenefitApplicationHMONo.Value), TransNo, Generic.ToDec(txtAmountF.Text), _
                                     Generic.ToInt(cboFleetNo.SelectedValue), Generic.ToDec(txtNoOfpayments.Text)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub PopulateCAR()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EBenefitApplicationFleet_WebOne", UserNo, Generic.ToInt(txtCode.Text))
        Generic.PopulateData(Me, "ph4", dt)
    End Sub
#End Region

#Region "Allowance"
    Private Function SaveAllowance(TransNo As Integer) As Boolean
        If SQLHelper.ExecuteNonQuery("EBenefitApplicationAllowance_WebSave", UserNo, Generic.ToInt(hifbenefitapplicationallowanceno.Value), TransNo, Generic.ToDec(txtAmountD.Text), _
                                     Generic.ToStr(txtstartdate.Text), Generic.ToStr(txtEndDate.Text)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub PopulateAllowance()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EBenefitApplicationAllowance_WebOne", UserNo, Generic.ToInt(txtCode.Text))
        Generic.PopulateData(Me, "phallowance", dt)
    End Sub
#End Region

End Class
