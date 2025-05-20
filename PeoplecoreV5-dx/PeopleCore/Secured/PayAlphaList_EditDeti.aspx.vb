Imports System.Data
Imports Microsoft.VisualBasic
Imports clsLib

Partial Class Secured_PayAlphaList_EditDeti
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim IsEnabled As Boolean = False
    Dim idd As Integer = 0

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EAlphaDeti_WebOne", UserNo, TransNo)
        Generic.PopulateData(Me, "Panel1", dt)

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("TransNo"))
        idd = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "PayAlphaList.aspx", "EAlpha")
        If idd = 0 Then : ViewState("IsEnabled") = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateData()
        End If
        EnabledControls()
    End Sub

    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayAlphaList.aspx", "EAlpha") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

 

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)

        btnModify.Visible = Not IsEnabled
        btnSave.Visible = IsEnabled
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        SaveRecord()
        
    End Sub

    Private Function SaveRecord() As Integer
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value),
        StartDate As String = Generic.ToStr(txtStartDate.Text),
        EndDate As String = Generic.ToStr(txtEndDate.Text),
        ApplicableYear As Integer = Generic.ToInt(txtApplicableYear.Text),
        IsMWE As Boolean = Generic.ToBol(txtIsMWE.Checked),
        PrevEmployerName As String = Generic.ToStr(txtPrevEmployerName.Text),
        PrevAddress As String = Generic.ToStr(txtPrevAddress.Text),
        PrevTINNo As String = Generic.ToStr(txtPrevTINNo.Text),
        BasicSalaryMWE As Double = Generic.ToDec(txtBasicSalaryMWE.Text),
        HolidayPayMWE As Double = Generic.ToDec(txtHolidayPayMWE.Text),
        OTpayMWE As Double = Generic.ToDec(txtOTpayMWE.Text),
        NPPayMWE As Double = Generic.ToDec(txtNPPayMWE.Text),
        HazardPayMWE As Double = Generic.ToDec(txtHazardPayMWE.Text),
        nontaxablebonus As Double = Generic.ToDec(txtnontaxablebonus.Text),
        DeminimisIncome As Double = Generic.ToDec(txtDeminimisIncome.Text),
        TotalSalaryExemption As Double = Generic.ToDec(txtTotalSalaryExemption.Text),
        TotalNonTaxableIncome As Double = Generic.ToDec(txtnontaxableincome.Text),
        BasicSalary As Double = Generic.ToDec(txtBasicSalary.Text),
        Representation As Double = Generic.ToDec(txtRepresentation.Text),
        TranspoAllowance As Double = Generic.ToDec(txtTranspoAllowance.Text),
        COLA As Double = Generic.ToDec(txtCOLA.Text),
        HousingAllowance As Double = Generic.ToDec(txtHousingAllowance.Text),
        totaltaxableincomeother As Double = Generic.ToDec(txttotaltaxableincomeother.Text),
        comission As Double = Generic.ToDec(txtcomission.Text),
        profit As Double = Generic.ToDec(txtprofit.Text),
        dirfee As Double = Generic.ToDec(txtdirfee.Text),
        taxablebonus As Double = Generic.ToDec(txttaxablebonus.Text),
        HazardPayTaxable As Double = Generic.ToDec(txtHazardPayTaxable.Text),
        OTPayTaxable As Double = Generic.ToDec(txtOTPayTaxable.Text),
        prevtotaltaxableincome As Double = Generic.ToDec(txtprevtotaltaxableincome.Text),
        insurancepremium As Double = Generic.ToDec(txtinsurancepremium.Text),
        taxdue As Double = Generic.ToDec(txttaxdue.Text),
        taxJanNov As Double = Generic.ToDec(txtTaxJanNov.Text),
        TAxDec As Double = Generic.ToDec(txtTaxDec.Text),
        PrevNonTaxableBonus As Double = Generic.ToDec(txtPrevNonTaxableBonus.Text),
        PrevNonTaxableDeminimis As Double = Generic.ToDec(txtPrevNonTaxableDeminimis.Text),
        PrevTaxExemption As Double = Generic.ToDec(txtPrevTaxExemption.Text),
        prevnontaxableincome As Double = Generic.ToDec(txtprevnontaxableincome.Text),
        PrevBasicSalary As Double = Generic.ToDec(txtPrevBasicSalary.Text),
        PrevTaxableBonus As Double = Generic.ToDec(txtPrevTaxableBonus.Text),
        PrevTaxDueJanNov As Double = Generic.ToDec(txtPrevTaxDueJanNov.Text)

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EAlphaDeti_WebSave", UserNo, Generic.ToInt(txtAlphaDetiNo.Text),
                                                            TransNo,
                                                            PayLocNo,
                                                            EmployeeNo,
                                                            StartDate,
                                                            EndDate,
                                                            ApplicableYear,
                                                            IsMWE,
                                                            PrevEmployerName,
                                                            PrevAddress,
                                                            PrevTINNo,
                                                            BasicSalaryMWE,
                                                            HolidayPayMWE,
                                                            OTpayMWE,
                                                            NPPayMWE,
                                                            HazardPayMWE,
                                                            nontaxablebonus,
                                                            DeminimisIncome,
                                                            TotalSalaryExemption,
                                                            TotalNonTaxableIncome,
                                                            BasicSalary,
                                                            Representation,
                                                            TranspoAllowance,
                                                            COLA,
                                                            HousingAllowance,
                                                            totaltaxableincomeother,
                                                            comission,
                                                            profit,
                                                            dirfee,
                                                            taxablebonus,
                                                            HazardPayTaxable,
                                                            OTPayTaxable,
                                                            prevtotaltaxableincome,
                                                            insurancepremium,
                                                            taxdue,
                                                            taxJanNov,
                                                            TAxDec,
                                                            0,
                                                            PrevNonTaxableBonus,
                                                            PrevNonTaxableDeminimis,
                                                            PrevTaxExemption,
                                                            prevnontaxableincome,
                                                            PrevBasicSalary,
                                                            PrevTaxableBonus,
                                                            PrevTaxDueJanNov)

        Dim RetVal As Boolean = False, error_num As Integer = 0, error_message As String = "", Proceed As Integer = 0
        For Each row As DataRow In dt.Rows
            RetVal = True
            error_num = Generic.ToInt(row("Error_num"))
            Proceed = Generic.ToInt(row("Proceed"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                RetVal = False
            ElseIf proceed = 1 Then
                MessageBox.Critical("Existing employee encoded.", Me)
            End If

        Next
        If RetVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If RetVal = True Then
            Dim xURL As String = "PayAlphaList.aspx?id=" & TransNo
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, xURL)
        End If

    End Function



End Class





