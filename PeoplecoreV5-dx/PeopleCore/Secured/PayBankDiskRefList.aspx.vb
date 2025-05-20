Imports System.Data
Imports System.Math
Imports System.Threading
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PayBankDiskRefList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayBankDiskRef_Web", UserNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateCombo()
        Try
            cboPayClassNo.DataSource = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", UserNo, PayLocNo)
            cboPayClassNo.DataValueField = "tNo"
            cboPayClassNo.DataTextField = "tDesc"
            cboPayClassNo.DataBind()

        Catch ex As Exception

        End Try
        Try
            cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup", UserNo, PayLocNo)
            cboPayLocNo.DataValueField = "tNo"
            cboPayLocNo.DataTextField = "tDesc"
            cboPayLocNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "PayBankDiskList.aspx", "EPayBankDisk")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            PopulateCombo()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

#Region "********Main*******"

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "PayBankDiskList.aspx", "EPayBankDisk") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayBankDiskRefNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayBankDiskRef", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayBankDiskList.aspx", "EPayBankDisk") Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayBankDiskRefNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPayBankDiskRef_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                Next
                mdlMain.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PayBankDiskList.aspx", "EPayBankDisk") Then
            Generic.ClearControls(Me, "pnlPopupMain")
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    'Submit record
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PayBankDiskList.aspx", "EPayBankDisk") Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtPayBankDiskRefNo.Text)
            'Dim PayClassNo As String = Generic.ToStr(Me.cboPayClassNo.SelectedValue)
            Dim FV1 As String = Generic.ToStr(Me.txtFV1.Text)
            Dim FV2 As String = Generic.ToStr(Me.txtFV2.Text)
            Dim FV3 As String = Generic.ToStr(Me.txtFV3.Text)
            Dim FV4 As String = Generic.ToStr(Me.txtFV4.Text)
            Dim BranchCode_Company As String = Generic.ToStr(Me.txtBranchCode_Company.Text)
            Dim BankCode As String = Generic.ToStr(Me.txtBankCode.Text)
            Dim BranchCode_PayrollAccount As String = Generic.ToStr(Me.txtBranchCode_PayrollAccount.Text)
            Dim xPayLocNo As Integer = Generic.ToInt(Me.cboPayLocNo.SelectedValue)
            Dim AccountNumber As String = Generic.ToStr(Me.txtAccountNumber.Text)
            Dim DebitAmount As Double = Generic.ToDec(Me.txtDebitAmount.Text)
            Dim CompanyCode As String = Generic.ToStr(Me.txtCompanyCode.Text)
            Dim EffectiveDate As String = Generic.ToStr(Me.txtEffectiveDate.Text)
            Dim BankTypeNo As Integer = Generic.ToInt(Me.cboBankTypeNo.SelectedValue)
            Dim BatchNo As String = Generic.ToStr(Me.txtBatchNo.Text)
            Dim PayOutSchedNo As Short = Generic.ToInt(Me.cboPayOutSchedNo.SelectedValue) 'Generic.ToInt(Me.txtPayOutSchedNo.Text)
            Dim PayDate1 As String = Generic.ToStr(Me.txtPayDate1.Text)
            Dim PayDate2 As String = Generic.ToStr(Me.txtPayDate2.Text)
            Dim PayDate3 As String = Generic.ToStr(Me.txtPayDate3.Text)
            Dim PayDate4 As String = Generic.ToStr(Me.txtPayDate4.Text)

            Dim dt As DataTable
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            dt = SQLHelper.ExecuteDataTable("EPayBankDiskRef_WebValidate", UserNo, tno, FV1, FV2, FV3, FV4, BranchCode_Company, BankCode, BranchCode_PayrollAccount, xPayLocNo, AccountNumber, DebitAmount, CompanyCode, EffectiveDate, BankTypeNo, BatchNo, PayLocNo)
            For Each row As DataRow In dt.Rows
                invalid = Generic.ToBol(row("Invalid"))
                messagedialog = Generic.ToStr(row("MessageDialog"))
                alerttype = Generic.ToStr(row("AlertType"))
            Next

            If invalid = True Then
                mdlMain.Show()
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If

            If SQLHelper.ExecuteNonQuery("EPayBankDiskRef_WebSave", UserNo, tno, FV1, FV2, FV3, FV4, BranchCode_Company, BankCode, BranchCode_PayrollAccount, xPayLocNo, AccountNumber, DebitAmount, CompanyCode, EffectiveDate, BankTypeNo, BatchNo, PayOutSchedNo, PayDate1, PayDate2, PayDate3, PayDate4) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                PopulateGrid()
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

#End Region

End Class
