Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports DevExpress.XtraCharts
Imports System.IO
Imports System.Data.SqlClient
Imports DevExpress.XtraRichEdit
Imports System.Net.Mail
Imports System.Xml
Imports Microsoft.Reporting.WebForms
'Imports Ionic.Zip
Imports Microsoft.VisualBasic.FileIO
Imports RestSharp
Imports System.Net
Imports System.Threading.Tasks
Imports DevExpress.Printing.Core.PdfExport.Metafile
Imports DevExpress.Xpo.DB.Helpers
Partial Class Include_EvalTemplate
    Inherits System.Web.UI.UserControl
    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim ActionStatNo As Integer = 3
    Dim rowno As Integer = 0
    Dim ReportNo As Integer = 0
    Dim MRNo As Integer = 0
    Dim MRHiredMassNo As Integer = 0
    Dim ComponentNo As Integer = 1
    Dim IsApplicant As Boolean = False
    Dim ApplicantNo As Integer = 0
    Dim EmployeeNo As Integer = 0
    Dim FormName As String = ""
    Dim IsEnabled As Boolean = False
    Dim IsPosted As Boolean = False
    Dim IsAllowEdit As Boolean = False

    Dim memoryStream As MemoryStream = New MemoryStream()
    Dim documentServer As New RichEditDocumentServer()

    Private Sub PopulateHeader()
        Dim _ds As New DataSet
        _ds = SQLHelper.ExecuteDataSet("EMRHiredMass_JobOffer_WebTabHeader", UserNo, MRNo, MRHiredMassNo, ComponentNo)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                lbltr1.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Title1"))
                lbltr2.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Title2"))
                lbltr3.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Title3"))
                lbltr4.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Title4"))
                lbltr5.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Title5"))
                lbltr6.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Title6"))
                lbltr7.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Title7"))
                lbltr8.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Title8"))

                lbltd1.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Value1"))
                lbltd2.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Value2"))
                lbltd3.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Value3"))
                lbltd4.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Value4"))
                lbltd5.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Value5"))
                lbltd6.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Value6"))
                lbltd7.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Value7"))
                lbltd8.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Value8"))

                lblAllowanceName.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("AllowanceName"))
                lblBenefitsName.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("BenefitsName"))
                IsEnabled = Generic.ToBol(_ds.Tables(0).Rows(0)("IsEnabled"))
                IsPosted = Generic.ToBol(_ds.Tables(0).Rows(0)("IsPosted"))
            End If
        End If

        rRef.DataSource = _ds
        rRef.DataBind()

    End Sub

    Protected Sub PopulateGrid()
        Try

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRHiredMass_WebOne", UserNo, MRHiredMassNo)
            For Each row As DataRow In dt.Rows
                lblName.Text = Generic.ToStr(row("FullName"))
                IsApplicant = Generic.ToBol(row("IsApplicant"))
                EmployeeNo = Generic.ToInt(row("EmployeeNo"))
                ApplicantNo = Generic.ToInt(row("ApplicantNo"))
            Next

            Dim Folder As String = "secured"
            If ComponentNo = 2 Then
                Folder = "securedmanager"
            ElseIf ComponentNo = 3 Then
                Folder = "securedself"
            End If

            If IsApplicant = True Then
                imgPhoto.ImageUrl = "~/" & Folder & "/frmShowImage.ashx?tNo=" & Generic.ToInt(ApplicantNo) & "&tIndex=1"
            Else
                imgPhoto.ImageUrl = "~/" & Folder & "/frmShowImage.ashx?tNo=" & Generic.ToInt(EmployeeNo) & "&tIndex=2"
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkInfo_Click(sender As Object, e As EventArgs)
        'mdlShowInfo.Show()
    End Sub
    Protected Sub btnSaveInfo_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        MRNo = Generic.ToInt(Request.QueryString("id"))
        MRHiredMassNo = Generic.ToInt(Request.QueryString("mrhiredmassno"))
        ComponentNo = Generic.ToInt(Request.QueryString("componentno"))
        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        FormName = Path.GetFileName(FileInfo.ToString)
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        If ComponentNo = 1 Then
            'AccessRights.CheckUser(UserNo)
            Permission.IsAuthenticatedCoreUser()
        ElseIf ComponentNo = 2 Then
            Permission.IsAuthenticatedSuperior()
        ElseIf ComponentNo = 3 Then
            Permission.IsAuthenticated()
        End If

        If ViewState("mergeData") IsNot Nothing Then
            'DemoRichEdit.DataSource = ViewState("mergeData")
        End If

        PopulateHeader()

        If Not IsPostBack Then
            'PopulateChatCount()
            PopulateGrid()
            'PopulateApprovalControls()
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)

            Try
                cboContractTempNo.DataSource = SQLHelper.ExecuteDataSet("EContractTemp_WebLookup_EmploymentContract", UserNo, PayLocNo)
                cboContractTempNo.DataTextField = "tDesc"
                cboContractTempNo.DataValueField = "tNo"
                cboContractTempNo.DataBind()
            Catch ex As Exception
            End Try

            PopulateData()
            PopulateDetl()
            PopulateGridBen()
            PopulateGroupBy()
            PopulateGridApprover()
            'PopulateAnalysis()

            PopulateTab()
        End If

        EnabledControls()
        'AddHandler ChatBox2.lnkSendClick, AddressOf lnkSend2_Click
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub


#Region "********Job Offer For Approval********"

    Protected Async Sub lnkSubmitForApproval_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim _ds As New DataSet
        Dim IsProceed As Integer = 0
        Dim xMessage As String = ""
        Dim tcount As Integer
        Dim MROfferNo As String = grdDetl.GetRowValues(tcount, "MROfferNo").ToString()
        Dim MRBenefitPackageNo = grdBen.GetRowValues(tcount, "MRBenefitPackageNo").ToString()

        _ds = SQLHelper.ExecuteDataSet("EMRHiredMass_JobOffer_WebForApprovalValidate", UserNo, MRNo, MRHiredMassNo, ComponentNo)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                IsProceed = Generic.ToInt(_ds.Tables(0).Rows(0)("tProceed"))
                xMessage = Generic.ToStr(_ds.Tables(0).Rows(0)("xMessage"))
            End If
        End If

        If IsProceed = 1 Then
            MessageBox.Alert(xMessage, "warning", Me)
            Exit Sub
        Else
            ApproveTransaction(MRHiredMassNo, 1, True, "")

            Try
                Dim client = Await GetClientAsync()

                Await ProcessData(client, Generic.ToStr(MRHiredMassNo), "EMRHiredMass")
                Await ProcessData(client, Generic.ToStr(MROfferNo), "EMROffer")
                Await ProcessData(client, Generic.ToStr(MRBenefitPackageNo), "EMRBenefitPackage")

                client.Dispose()
            Catch ex As Exception
                Console.WriteLine(ex)
            End Try

            PopulateGridApprover()
            ViewState("IsEnabled") = False
            EnabledControls()

        End If
    End Sub

    Protected Async Sub lnkApproved_Click(sender As Object, e As System.EventArgs)
        Dim _ds As New DataSet
        Dim IsProceed As Integer = 0
        Dim xMessage As String = ""
        Dim MRHiredMass_JobOfferNo As Integer = 0

        _ds = SQLHelper.ExecuteDataSet("EMRHiredMass_JobOffer_WebForApprovalValidate", UserNo, MRNo, MRHiredMassNo, ComponentNo)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                IsProceed = Generic.ToInt(_ds.Tables(0).Rows(0)("tProceed"))
                xMessage = Generic.ToStr(_ds.Tables(0).Rows(0)("xMessage"))
                MRHiredMass_JobOfferNo = Generic.ToInt(_ds.Tables(0).Rows(0)("MRHiredMass_JobOfferNo"))
            End If
        End If

        If IsProceed = 1 Then
            MessageBox.Alert(xMessage, "warning", Me)
            Exit Sub
        Else
            ApproveTransaction(MRHiredMassNo, 2, False, "")

            Try
                Dim client = Await GetClientAsync()
                Await ProcessData(client, Generic.ToStr(MRHiredMass_JobOfferNo), "EMRHiredMass_JobOffer")
                client.Dispose()
            Catch ex As Exception
                Console.WriteLine(ex)
            End Try

            PopulateGridApprover()
            EnabledControls()
        End If
    End Sub

    Protected Sub lnkRevise_Click(sender As Object, e As System.EventArgs)
        ViewState("ApprovalStatNo") = 4
        txtRemarkReOffer.Text = ""
        mdlRemarkReOffer.Show()
    End Sub

    Protected Sub lnkReOffer_Click(sender As Object, e As System.EventArgs)
        ViewState("ApprovalStatNo") = 5
        txtRemarkReOffer.Text = ""
        mdlRemarkReOffer.Show()
    End Sub

    Protected Sub lnkSave_ClickReOffer(sender As Object, e As EventArgs)
        Dim ChatTypeNo As Integer = 4
        SQLHelper.ExecuteDataSet("EChat_WebSave", UserNo, ChatTypeNo, MRHiredMassNo, Generic.ToInt(Session("EmployeeNo")), txtRemarkReOffer.Text, Generic.ToInt(Session("xPayLocNo")))
        'ApproveTransaction(MRHiredMassNo, ApprovalStatNo, False, "")
        SQLHelper.ExecuteDataSet("EMRHiredMass_JobOffer_WebReset", UserNo, MRHiredMassNo, Generic.ToInt(ViewState("ApprovalStatNo")))
        PopulateHeader()
        EnabledControls()
        PopulateGridApprover()



    End Sub

    Protected Sub lnkDisApproved_Click(sender As Object, e As System.EventArgs)
        txtRemarkDisAppr.Text = ""
        mdlRemarkDisAppr.Show()
    End Sub

    Protected Sub lnkSave_ClickDisAppr(sender As Object, e As EventArgs)
        Dim ChatTypeNo As Integer = 4
        Dim ApprovalStatNo As Integer = 3
        SQLHelper.ExecuteDataSet("EChat_WebSave", UserNo, ChatTypeNo, MRHiredMassNo, Generic.ToInt(Session("EmployeeNo")), txtRemarkDisAppr.Text, Generic.ToInt(Session("xPayLocNo")))
        ApproveTransaction(MRHiredMassNo, ApprovalStatNo, False, Generic.ToStr(txtRemarkDisAppr.Text))
        SQLHelper.ExecuteDataSet("EMRHiredMass_JobOffer_WebReset", UserNo, MRHiredMassNo, ApprovalStatNo)
        PopulateHeader()
        EnabledControls()
        PopulateGridApprover()
    End Sub

    Private Sub ApproveTransaction(tId As Integer, approvalStatNo As Integer, isSubmitforApp As Boolean, remarks As String)
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("EMRHiredMass_JobOffer_WebApproved", UserNo, tId, approvalStatNo, isSubmitforApp, remarks.ToString)
        If fds.Tables.Count > 0 Then
            If fds.Tables(0).Rows.Count > 0 Then
                Dim IsWithapprover As Boolean
                Dim statNo As Integer
                Dim xdt As DataTable
                IsWithapprover = Generic.CheckDBNull(fds.Tables(0).Rows(0)("IsWithApprover"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                If IsWithapprover = True Then
                    If approvalStatNo = 1 Then
                        If Generic.CheckDBNull(fds.Tables(0).Rows(0)("ApprovalStatNo"), clsBase.clsBaseLibrary.enumObjectType.IntType) = 2 Then
                            'PopulateSendOffer()
                            MessageBox.Success("Record has been successfully approved.", Me)
                        Else
                            MessageBox.Success("Record has been successfully submitted for approval.", Me)
                        End If
                    ElseIf approvalStatNo = 2 Then
                        xdt = SQLHelper.ExecuteDataTable("EMRHiredMass_JobOffer_WebOne", UserNo, MRNo, MRHiredMassNo)
                        For Each xrow As DataRow In xdt.Rows
                            approvalStatNo = Generic.ToInt(xrow("ApprovalStatNo"))
                        Next

                        If approvalStatNo = 2 Then
                            'PopulateSendOffer()
                        Else
                            MessageBox.Success("Record has been successfully approved.", Me)
                        End If
                    Else
                        MessageBox.Success("Record has been successfully disapproved.", Me)
                    End If

                    PopulateHeader()
                    'PopulateApprovalControls()
                    EnabledControls()
                Else
                    MessageBox.Information("Unable to locate the next approver.", Me)
                End If
            End If


        End If
    End Sub

    Protected Sub lnkAccept_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As New Button

            SQLHelper.ExecuteNonQuery("EMRHiredMass_JobOffer_WebConfirm", UserNo, MRHiredMassNo, 1, "", ComponentNo)
            PopulateHeader()
            'PopulateApprovalControls()
            PopulateGridApprover()
            EnabledControls()

            MessageBox.Success("Job Offer has been successfully accepted.", Me)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDecline_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        mdlRemarkDecline.Show()

    End Sub

    Protected Sub lnkSave_ClickDecline(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As New Button

            SQLHelper.ExecuteNonQuery("EMRHiredMass_JobOffer_WebConfirm", UserNo, MRHiredMassNo, 2, txtRemarkDecline.Text, ComponentNo)
            PopulateHeader()
            'PopulateApprovalControls()
            PopulateGridApprover()
            EnabledControls()

            MessageBox.Success("Job Offer has been successfully declined.", Me)
        Catch ex As Exception
        End Try
    End Sub
#End Region


#Region "********Tab Details********"

    Private Sub PopulateTab()
        Dim Index As Integer
        Index = Generic.ToInt(ViewState("Index"))

        If Index = 2 Then 'Allowance
            lnkAllowance.CssClass = "list-group-item active text-left"
            lnkSalary.CssClass = "list-group-item text-left"
            lnkBenefits.CssClass = "list-group-item text-left"
            lnkApprover.CssClass = "list-group-item text-left"
            lnkAnalysis.CssClass = "list-group-item text-left"
            divSalary.Visible = False
            divAllowance.Visible = True
            divBenefits.Visible = False
            divApprover.Visible = False
            divAnalysis.Visible = False
        ElseIf Index = 3 Then 'Benefits Package
            lnkBenefits.CssClass = "list-group-item active text-left"
            lnkSalary.CssClass = "list-group-item text-left"
            lnkAllowance.CssClass = "list-group-item text-left"
            lnkApprover.CssClass = "list-group-item text-left"
            lnkAnalysis.CssClass = "list-group-item text-left"
            divSalary.Visible = False
            divAllowance.Visible = False
            divBenefits.Visible = True
            divApprover.Visible = False
            divAnalysis.Visible = False
        ElseIf Index = 4 Then 'Document Signatory
            lnkBenefits.CssClass = "list-group-item text-left"
            lnkSalary.CssClass = "list-group-item text-left"
            lnkAllowance.CssClass = "list-group-item text-left"
            lnkApprover.CssClass = "list-group-item active text-left"
            lnkAnalysis.CssClass = "list-group-item text-left"
            divSalary.Visible = False
            divAllowance.Visible = False
            divBenefits.Visible = False
            divApprover.Visible = True
            divAnalysis.Visible = False
        ElseIf Index = 5 Then 'Salary Analysis
            lnkSalary.CssClass = "list-group-item text-left"
            lnkBenefits.CssClass = "list-group-item text-left"
            lnkAllowance.CssClass = "list-group-item text-left"
            lnkApprover.CssClass = "list-group-item text-left"
            lnkAnalysis.CssClass = "list-group-item active text-left"
            divSalary.Visible = False
            divBenefits.Visible = False
            divAllowance.Visible = False
            divApprover.Visible = False
            divAnalysis.Visible = True
        Else 'Pay Offer
            lnkSalary.CssClass = "list-group-item active text-left"
            lnkBenefits.CssClass = "list-group-item text-left"
            lnkAllowance.CssClass = "list-group-item text-left"
            lnkApprover.CssClass = "list-group-item text-left"
            lnkAnalysis.CssClass = "list-group-item text-left"
            divSalary.Visible = True
            divBenefits.Visible = False
            divAllowance.Visible = False
            divApprover.Visible = False
            divAnalysis.Visible = False
        End If

    End Sub

    Protected Sub lnkSalary_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ViewState("Index") = 1
        PopulateTab()
        EnabledControls()
    End Sub

    Protected Sub lnkAllowance_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ViewState("Index") = 2
        PopulateTab()
        PopulateDetl()
        EnabledControls()
    End Sub

    Protected Sub lnkBenefits_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ViewState("Index") = 3
        PopulateTab()
        PopulateGridBen()
        PopulateGroupBy()
        EnabledControls()
    End Sub

    Protected Sub lnkApprover_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ViewState("Index") = 4
        PopulateTab()
        PopulateGridApprover()
        EnabledControls()
    End Sub

    Protected Sub lnkAnalysis_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ViewState("Index") = 5
        PopulateTab()
        'PopulateAnalysis()
        EnabledControls()
    End Sub

#End Region


    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMRHiredMass_WebOne", UserNo, MRHiredMassNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next
    End Sub

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)

        ViewState("IsEnabled") = True
        'PopulateApprovalControls()
        EnabledControls()

    End Sub

    Private Sub EnabledControls()

        Dim IsSendOffer As Boolean = False, IsForConfirmation As Boolean = False
        Dim IsForApproval_Show As Boolean = False, IsApprove_Show As Boolean = False, IsDisapprove_Show As Boolean = False, IsGenerate_Show As Boolean = False
        Dim IsForApproval_Enabled As Boolean = False, IsApprove_Enabled As Boolean = False, IsDisapprove_Enabled As Boolean = False, IsGenerate_Enabled As Boolean = False, IsReOffer As Boolean, IsRevise As Boolean
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMRHiredMass_JobOffer_WebControls", UserNo, MRNo, MRHiredMassNo, ComponentNo, FormName, IsPosted, IsEnabled)
        For Each row As DataRow In dt.Rows
            IsForApproval_Show = Generic.ToBol(row("IsForApproval_Show"))
            IsForApproval_Enabled = Generic.ToBol(row("IsForApproval_Enabled"))
            IsApprove_Show = Generic.ToBol(row("IsApprove_Show"))
            IsApprove_Enabled = Generic.ToBol(row("IsApprove_Enabled"))
            IsDisapprove_Show = Generic.ToBol(row("IsDisapprove_Show"))
            IsDisapprove_Enabled = Generic.ToBol(row("IsDisapprove_Enabled"))
            IsGenerate_Show = Generic.ToBol(row("IsGenerate_Show"))
            IsGenerate_Enabled = Generic.ToBol(row("IsGenerate_Enabled"))
            IsAllowEdit = Generic.ToBol(row("IsAllowEdit"))
            IsSendOffer = Generic.ToBol(row("IsSendOffer"))
            IsForConfirmation = Generic.ToBol(row("IsForConfirmation"))
            IsReOffer = Generic.ToBol(row("IsReOffer"))
            IsRevise = Generic.ToBol(row("IsRevise"))
        Next

        lnkSubmitForApproval.Visible = IsForApproval_Show
        lnkSubmitForApproval.Enabled = IsForApproval_Enabled
        lnkApproved.Visible = IsApprove_Show
        lnkApproved.Enabled = IsApprove_Enabled
        lnkDisapproved.Visible = IsDisapprove_Show
        lnkDisapproved.Enabled = IsDisapprove_Enabled
        lnkJobOffer.Visible = IsGenerate_Show
        lnkJobOffer.Enabled = IsGenerate_Enabled
        lnkSendOffer.Visible = IsSendOffer
        lnkAccept.Visible = IsForConfirmation
        lnkDecline.Visible = IsForConfirmation
        lnkReOffer.Visible = IsReOffer
        lnkRevise.Visible = IsRevise

        If Generic.ToBol(chkIsWithOffer.Checked) = False Then
            ViewState("IsEnabled") = True
        End If

        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        Dim IsEnabled_Analysis As Boolean = Generic.ToBol(ViewState("IsEnabled_Analysis"))
        Generic.EnableControls(Me, "Panel1", Enabled)
        Generic.EnableControls(Me, "Panel2", IsEnabled_Analysis)

        If IsAllowEdit = False Then
            lnkModify.Visible = False
            lnkSave.Visible = False
            lnkAddDetl.Visible = False
            lnkDeleteDetl.Visible = False
            lnkAddBen.Visible = False
            lnkDeleteBen.Visible = False
            lnkAddPac.Visible = False
            'lnkSaveAnalysis.Visible = False
            'lnkModifyAnalysis.Visible = False
        Else
            lnkModify.Visible = Not Enabled
            lnkSave.Visible = Enabled
            lnkAddDetl.Visible = True
            lnkDeleteDetl.Visible = True
            lnkAddBen.Visible = True
            lnkDeleteBen.Visible = True
            'lnkModifyAnalysis.Visible = Not IsEnabled_Analysis
            'lnkSaveAnalysis.Visible = IsEnabled_Analysis
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim CurrentSalary As Double = Generic.ToDec(txtCurrentSalary.Text)
        Dim EmployeeRateClassNo As Integer = Generic.ToInt(cboEmployeeRateClassNo.SelectedValue)
        Dim ContractTempNo As Integer = Generic.ToInt(cboContractTempNo.SelectedValue)
        Dim OnboardDate As String = Generic.ToStr(txtOnboardDate.Text)
        Dim ImmediateSuperiorNo As Integer = Generic.ToInt(hifImmediateSuperiorNo.Value)

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EMRHiredMass_JobOffer_WebValidate", UserNo, MRHiredMassNo, EmployeeRateClassNo, ContractTempNo, CurrentSalary, OnboardDate, ImmediateSuperiorNo)
        For Each row As DataRow In dt.Rows
            invalid = Generic.ToBol(row("Invalid"))
            messagedialog = Generic.ToStr(row("MessageDialog"))
            alerttype = Generic.ToStr(row("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EMRHiredMass_JobOffer_WebSave", UserNo, MRHiredMassNo, EmployeeRateClassNo, ContractTempNo, CurrentSalary, OnboardDate, ImmediateSuperiorNo) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            ViewState("IsEnabled") = False
            PopulateData()
            EnabledControls()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub


#Region "Detail"
    Private Sub PopulateDetl()

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMROffer_Web", UserNo, MRHiredMassNo)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"MROfferNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EMROffer", UserNo, item)
            i = i + 1
        Next

        If i > 0 Then
            PopulateDetl()
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        Try

            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MROfferNo"}))

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMROffer_WebOne", UserNo, Generic.ToInt(i))
            For Each row As DataRow In dt.Rows
                Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next

            Generic.EnableControls(Me, "pnlPopupDetl", IsAllowEdit)
            lnkSaveDetl.Enabled = IsAllowEdit

            mdlDetl.Show()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)

        Generic.ClearControls(Me, "pnlPopupDetl")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
        mdlDetl.Show()

    End Sub

    Protected Sub btnSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim MROfferNo As Integer = Generic.ToInt(txtMROfferNo.Text)
        Dim Amount As Decimal = Generic.ToDec(txtAmount.Text)
        Dim typeno As Integer = Generic.ToInt(cboPayIncomeTypeNo.SelectedValue)
        Dim IsPerDay As Boolean = Generic.ToBol(txtIsPerDay.Checked)
        Dim PayScheduleNo As Integer = Generic.ToInt(cboPayScheduleNo.SelectedValue)

        If SQLHelper.ExecuteNonQuery("EMROffer_WebSave", UserNo, MROfferNo, MRHiredMassNo, MRNo, Amount, typeno, IsPerDay, PayScheduleNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateDetl()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub



#End Region


#Region "Benefit Package"
    Private Sub PopulateGridBen()


        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMRBenefitPackage_Web", UserNo, MRHiredMassNo)
        grdBen.DataSource = dt
        grdBen.DataBind()

        If grdBen.VisibleRowCount() > 0 Then
            lnkAddPac.Visible = False
        Else
            lnkAddPac.Visible = True
        End If

        PopulateGroupBy()
    End Sub

    Protected Sub lnkExportBen_Click(sender As Object, e As EventArgs)
        Try
            grdExportBen.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDeleteBen_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdBen.GetSelectedFieldValues(New String() {"MRBenefitPackageNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EMRBenefitPackage", UserNo, item)
            i = i + 1
        Next

        If i > 0 Then
            PopulateGridBen()
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Protected Sub lnkEditBen_Click(sender As Object, e As EventArgs)
        Try

            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MRBenefitPackageNo"}))

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRBenefitPackage_WebOne", UserNo, Generic.ToInt(i))
            For Each row As DataRow In dt.Rows
                Generic.PopulateDropDownList(UserNo, Me, "pnlPopupBen", PayLocNo)
                Generic.PopulateData(Me, "pnlPopupBen", dt)
            Next

            Generic.EnableControls(Me, "pnlPopupBen", IsAllowEdit)
            btnSaveBen.Enabled = IsAllowEdit

            mdlShowBen.Show()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAddBen_Click(sender As Object, e As EventArgs)

        Generic.ClearControls(Me, "pnlPopupBen")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupBen", PayLocNo)
        mdlShowBen.Show()

    End Sub

    Protected Sub btnSaveBen_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim MRBenefitPackageNo As Integer = Generic.ToInt(txtMRBenefitPackageNo.Text)
        Dim MRBenefitPackageCode As String = ""
        Dim MRBenefitPackageDesc As String = Generic.ToStr(txtMRBenefitPackageDesc.Text)
        'Dim Remarks As String = Generic.ToStr(txtRemarks.Text)
        Dim BenefitPackageTypeNo As Integer = Generic.ToInt(cboBenefitPackageTypeNo.SelectedValue)
        Dim OrderLevel As Integer = Generic.ToInt(txtOrderLevel.Text)

        If SQLHelper.ExecuteNonQuery("EMRBenefitPackage_WebSave", UserNo, MRBenefitPackageNo, MRBenefitPackageCode, MRBenefitPackageDesc, txtRemarks.Html, BenefitPackageTypeNo, OrderLevel, MRHiredMassNo, MRNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridBen()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Protected Sub grdBen_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        PopulateGroupBy()
    End Sub

    Private Sub PopulateGroupBy()
        grdBen.BeginUpdate()
        Try
            grdBen.ClearSort()
            grdBen.GroupBy(grdBen.Columns("BenefitPackageTypeDesc"))
        Finally
            grdBen.EndUpdate()
        End Try
        grdBen.ExpandAll()
    End Sub

    Protected Sub grdBen_CustomColumnDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewColumnDisplayTextEventArgs)
        If e.Column.FieldName = "BenefitPackageTypeDesc" Then
            grdBen.Settings.GroupFormat = e.Value
        End If
    End Sub

    Protected Sub grdBen_CustomColumnSort(ByVal sender As Object, ByVal e As CustomColumnSortEventArgs)
        If e.Column IsNot Nothing And e.Column.FieldName = "BenefitPackageTypeDesc" Then
            Dim country1 As Object = e.GetRow1Value("OrderLevelType")
            Dim country2 As Object = e.GetRow2Value("OrderLevelType")
            Dim res As Integer = Comparer.Default.Compare(country1, country2)
            If res = 0 Then
                Dim city1 As Object = e.Value1
                Dim city2 As Object = e.Value2
                res = Comparer.Default.Compare(city1, city2)
            End If
            e.Result = res
            e.Handled = True
        End If
    End Sub


    Protected Sub lnkAddPac_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "pnlPopupPac")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupPac", PayLocNo)
        mdlShowPac.Show()
    End Sub


    Protected Sub btnSavePac_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim xMessage As String = ""
        Dim BenefitPackageNo As Integer = Generic.ToInt(cboBenefitPackageNo.SelectedValue)

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMRBenefitPackage_WebAppend", UserNo, MRNo, MRHiredMassNo, BenefitPackageNo)
        For Each row As DataRow In dt.Rows
            Retval = Generic.ToBol(row("tProceed"))
            xMessage = Generic.ToStr(row("xMessage"))
        Next

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateHeader()
            PopulateGridBen()
        Else
            MessageBox.Critical(xMessage, Me)
        End If
    End Sub

#End Region



#Region "********Approver********"
    Private Sub PopulateGridApprover()

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMRHiredMass_JobOffer_WebApprover", UserNo, MRNo, MRHiredMassNo)
        grdApprover.DataSource = dt
        grdApprover.DataBind()

    End Sub
#End Region



#Region "********Reports********"

    Protected Sub lnkJobOffer_Click(sender As Object, e As EventArgs)
    End Sub

    Protected Sub lnkJobOffer_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As Button = TryCast(sender, Button)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub


#End Region


#Region "Send Offer"
    Protected Sub lnkSendOffer_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'PopulateSendOffer()
    End Sub

    Private Function Open_FileAttachment(BenefitPackageNo As Integer) As String
        Try
            Dim dt As DataSet, filename As String = ""

            dt = SQLHelper.ExecuteDataSet("EDoc_Web", UserNo, BenefitPackageNo, "", "0330000000") 'Session("xMenuType"))
            If dt.Tables.Count > 0 Then
                If dt.Tables(0).Rows.Count > 0 Then
                    filename = Generic.ToStr(dt.Tables(0).Rows(0)("ActualFileName"))

                End If
            End If

            Dim path As String = ""
            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet("EDocFolder_WebOne")
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    path = Generic.ToStr(ds.Tables(0).Rows(0)("path"))
                End If
            End If

            Return path & "\" & filename.ToString

        Catch ex As Exception

        End Try
    End Function
    Protected Sub btnOpenFile_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = TryCast(sender, Button)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(btn)
    End Sub

#End Region


#Region "Generate Offer"


#End Region


#Region "Attachment"

    Protected Sub lnkPreview_Click(sender As Object, e As EventArgs)
        Dim lnk As New Button
        lnk = sender

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMRHiredMass_JobOffer_WebReport", UserNo, PayLocNo, MRNo, MRHiredMassNo)
        grdReport.DataSource = dt
        grdReport.DataBind()
        ModalPopupExtender2.Show()

    End Sub

    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
        '    Dim lnk As New LinkButton
        '    Dim sb As New StringBuilder
        '    lnk = sender
        '    Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        '    Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ReportNo"}))

        '    Dim ViewerName As String = "rpttemplateviewer.aspx"
        '    Dim param As String = ""
        '    Dim tProceed As Boolean = False
        '    Dim contracttempNo As Integer = 0
        '    Dim ReportNo As Integer = 0, dt As DataTable
        '    dt = SQLHelper.ExecuteDataTable("EReport_WebViewer", UserNo, id, "", MRHiredMassNo, PayLocNo)
        '    For Each row As DataRow In dt.Rows
        '        ReportNo = Generic.ToInt(row("ReportNo"))
        '        param = Generic.ToStr(row("param"))
        '        tProceed = Generic.ToStr(row("tProceed"))
        '        contracttempNo = Generic.ToInt(row("ContractTempNo"))
        '        ViewerName = Generic.ToStr(row("FormName"))
        '    Next

        '    sb.Append("<script>")
        '    sb.Append("window.open('" & ViewerName & "?reportno=" & ReportNo & "&ContractTempNo=" & contracttempNo & "&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        '    sb.Append("</script>")
        '    Page.ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())

        '    ModalPopupExtender2.Show()
    End Sub
    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        '    Dim lnk As LinkButton = TryCast(sender, LinkButton)
        '    Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        '    NewScriptManager.RegisterPostBackControl(lnk)
    End Sub


#End Region

#Region "Sync API"

    'Async Function SyncData(id As Integer) As Task
    '    Try
    '        Dim client = Await GetClientAsync()
    '        Dim operation As String = "Push"

    '        Dim processedCount = 0

    '        'Dim dt As DataTable
    '        'dt = SQLHelper.ExecuteDataTable("GetTableSync", "Applicant", operation)
    '        'For Each row As DataRow In dt.Rows
    '        '    Dim tableName = Generic.ToStr(row("TableName"))
    '        '    Dim limit = Generic.ToStr(row("Limit"))
    '        '    Await ProcessData(client, limit, tableName, operation)
    '        '    processedCount += 1
    '        'Next



    '        Await ProcessData(client, id, tableName, operation)

    '        client.Dispose()
    '    Catch

    '    End Try

    'End Function

    Async Function ProcessData(client As RestSharp.RestClient, id As String, tableName As String) As Task
        Try
            Dim postRequest = New RestRequest("api/push/onedata", Method.Post)
            postRequest.AddJsonBody(New With {
            .id = id,
            .tableName = tableName
        })

            Dim postResponse = Await client.ExecuteAsync(postRequest)

            If postResponse.IsSuccessful AndAlso postResponse.Content IsNot Nothing Then
                Console.WriteLine("Successfully processed " & tableName)
                Return
            Else
                Throw New Exception("Failed to process " & tableName & ". Status: " & postResponse.StatusCode & ", Message: " & postResponse.Content)
            End If
        Catch ex As Exception
            Console.WriteLine("Error processing " & tableName & ": " & ex.Message)
            Throw
        End Try
    End Function


    Public Class Table
        Public Property tableName As String
        Public Property limit As Integer
    End Class


    Private _cachedToken As String = String.Empty
    Private _tokenExpiration As DateTime

    Private _restClient As RestSharp.RestClient

    Public Sub New()
        _restClient = New RestSharp.RestClient(ConfigurationManager.AppSettings("API:BaseURL"))
    End Sub

    Public Async Function GetClientAsync() As Task(Of RestSharp.RestClient)
        Try
            Dim token As String = Await GetTokenAsync()
            _restClient.AddDefaultHeader("Authorization", "Bearer " & token)
            Return _restClient
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Class AuthTokenResponse
        Public Property token As String
        Public Property expiresIn As Integer
    End Class

    Async Function GetTokenAsync() As Task(Of String)

        If Not String.IsNullOrEmpty(_cachedToken) AndAlso _tokenExpiration > DateTime.Now.AddMinutes(1) Then
            Return _cachedToken
        End If
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Dim client As New RestSharp.RestClient(ConfigurationManager.AppSettings("API:BaseURL"))
            Dim request = New RestRequest("api/auth", Method.Post)

            request.AddJsonBody(New With {
            .username = ConfigurationManager.AppSettings("API:Username"),
            .password = ConfigurationManager.AppSettings("API:Password")})

            Dim response = Await client.ExecuteAsync(Of AuthTokenResponse)(request)
            If response.StatusCode = Net.HttpStatusCode.OK Then
                Return response.Content.Trim("""")
            End If

            _cachedToken = response.Data.token
            _tokenExpiration = DateTime.Now.AddSeconds(response.Data.expiresIn)
            Return _cachedToken
        Catch ex As Exception
            Throw New Exception("Failed to obtain JWT token", ex)
        End Try
    End Function


#End Region



End Class
