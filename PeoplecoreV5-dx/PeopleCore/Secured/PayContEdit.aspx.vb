Imports clsLib
Imports System.Data
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Math

Partial Class Secured_PayContEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim clsGeneric As New clsGenericClass

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayContList.aspx", "EPayCont") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayCont_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))
            Generic.PopulateData(Me, "Panel1", dt)
        Next

    End Sub

    Private Sub PopulateCombo()
        Try
            cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup", UserNo, PayLocNo)
            cboPayLocNo.DataValueField = "tNo"
            cboPayLocNo.DataTextField = "tDesc"
            cboPayLocNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayCont_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "PayContList.aspx", "EPayCont")

        If Not IsPostBack Then
            If TransNo = 0 Then
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            End If
            PopulateCombo()
            PopulateData()
            PopulateTabHeader()
        End If

        EnabledControls()

    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))

        Generic.EnableControls(Me, "Panel1", Enabled)

        If Generic.ToBol(txtIsPosted.Checked) = True Then
            txtRefNo.Enabled = False
            txtLocCode.Enabled = False
            cboPayLocNo.Enabled = False
            cboFacilityNo.Enabled = False
            cboApplicableMonth.Enabled = False
            txtApplicableYear.Enabled = False
        End If

        txtCode.Enabled = False
        lnkModify.Visible = Not Enabled
        lnkSave.Visible = Enabled
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayContList.aspx", "EPayCont") Then
            Dim RetVal As Boolean = False
            Dim dt As DataTable
            Dim tno As Integer = Generic.ToInt(txtPayContNo.Text)
            Dim RefNo As String = Generic.ToStr(txtRefNo.Text)
            Dim LocCode As String = Generic.ToStr(txtLocCode.Text)
            Dim xPayLocNo As Integer = Generic.ToInt(Me.cboPayLocNo.SelectedValue)
            Dim FacilityNo As Integer = Generic.ToInt(Me.cboFacilityNo.SelectedValue)
            Dim ApplicableMonth As Integer = Generic.ToStr(Me.cboApplicableMonth.SelectedValue)
            Dim ApplicableYear As Integer = Generic.ToInt(txtApplicableYear.Text)
            Dim SSSSBR As String = Generic.ToStr(txtSSSSBR.Text)
            Dim SSSDate As String = Generic.ToStr(txtSSSDate.Text)
            Dim SSSBank As String = Generic.ToStr(txtSSSBank.Text)
            Dim HDMFSBR As String = Generic.ToStr(txtHDMFSBR.Text)
            Dim HDMFDate As String = Generic.ToStr(txtHDMFDate.Text)
            Dim HDMFBank As String = Generic.ToStr(txtHDMFBank.Text)
            Dim PHSBR As String = Generic.ToStr(txtPHSBR.Text)
            Dim PHDate As String = Generic.ToStr(txtPHDate.Text)
            Dim PHBank As String = Generic.ToStr(txtPHBank.Text)
            Dim SSSLoanSBR As String = Generic.ToStr(txtSSSLoanSBR.Text)
            Dim SSSLoanDate As String = Generic.ToStr(txtSSSLoanDate.Text)
            Dim SSSLoanBank As String = Generic.ToStr(txtSSSLoanBank.Text)
            Dim HDMFLoanSBR As String = Generic.ToStr(txtHDMFLoanSBR.Text)
            Dim HDMFLoanDate As String = Generic.ToStr(txtHDMFLoanDate.Text)
            Dim HDMFLoanBank As String = Generic.ToStr(txtHDMFLoanBank.Text)
            Dim PHLoanSBR As String = Generic.ToStr(txtPHLoanSBR.Text)
            Dim PHLoanDate As String = Generic.ToStr(txtPHLoanDate.Text)
            Dim PHLoanBank As String = Generic.ToStr(txtPHLoanBank.Text)
            Dim Signatory As Integer = 0

            Dim _dt As DataTable
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            _dt = SQLHelper.ExecuteDataTable("EPayCont_WebValidate", UserNo, tno, ApplicableMonth, ApplicableYear, RefNo,
                                                SSSSBR, SSSDate, SSSBank,
                                                PHSBR, PHDate, PHBank,
                                                HDMFSBR, HDMFDate, HDMFBank,
                                                SSSLoanSBR, SSSLoanDate, SSSLoanBank,
                                                HDMFLoanSBR, HDMFLoanDate, HDMFLoanBank,
                                                Signatory, LocCode, xPayLocNo, FacilityNo, PayLocNo)
            For Each row As DataRow In _dt.Rows
                invalid = Generic.ToBol(row("Invalid"))
                messagedialog = Generic.ToStr(row("MessageDialog"))
                alerttype = Generic.ToStr(row("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If


            dt = SQLHelper.ExecuteDataTable("EPayCont_WebSave", UserNo, tno, ApplicableMonth, ApplicableYear, RefNo,
                                                SSSSBR, SSSDate, SSSBank,
                                                PHSBR, PHDate, PHBank,
                                                HDMFSBR, HDMFDate, HDMFBank,
                                                SSSLoanSBR, SSSLoanDate, SSSLoanBank,
                                                HDMFLoanSBR, HDMFLoanDate, HDMFLoanBank,
                                                Signatory, LocCode, xPayLocNo, FacilityNo)
            For Each row As DataRow In dt.Rows
                TransNo = Generic.ToInt(row("Retval"))
                RetVal = True
            Next

            If RetVal = True Then
                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim url As String = "PayContList.aspx?id=" & TransNo
                    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                Else
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                    ViewState("IsEnabled") = False
                    EnabledControls()
                End If
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If

        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

 


End Class
