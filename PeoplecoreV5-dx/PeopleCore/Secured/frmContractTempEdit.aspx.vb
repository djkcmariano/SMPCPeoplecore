Imports clsLib
Imports DevExpress.Web
Imports System.Data

Partial Class Secured_SecCMSTemplateEdit
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim IsEnabled As Boolean = False

    Protected Sub PopulateData()

        Dim ReportTypeNo As Integer = 0
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EContractTemp_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "pnlPopupMain", dt)
            txtContractTempCont.Html = Generic.ToStr(row("ContractTempCont"))
            ReportTypeNo = Generic.ToInt(row("ReportTypeNo"))
        Next

        Try
            cboReportTypeNo.DataSource = SQLHelper.ExecuteDataSet("EReportType_WebLookup_Union", UserNo, ReportTypeNo, Generic.ToStr(Session("xMenuType")), PayLocNo)
            cboReportTypeNo.DataValueField = "tNo"
            cboReportTypeNo.DataTextField = "tDesc"
            cboReportTypeNo.DataBind()
        Catch ex As Exception

        End Try

        Try
            cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
            cboPayLocNo.DataTextField = "tdesc"
            cboPayLocNo.DataValueField = "tNo"
            cboPayLocNo.DataBind()

        Catch ex As Exception

        End Try

        PopulateCombo()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        If TransNo = 0 Then : ViewState("IsEnabled") = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If
        AccessRights.CheckUser(UserNo, "frmContractTempList.aspx", "EContractTemp")
        If Not IsPostBack Then
            EnabledControls()
            PopulateData()
        End If


        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub
    Protected Sub lstLegend_ValueChanged(sender As Object, e As EventArgs)

        If (Generic.ToStr(lstLegend.SelectedItem.Value) > "") Then

            Dim selectedText As String = Generic.ToStr(lstLegend.SelectedItem.Value)
            Dim currentHTML As String = txtContractTempCont.Html

            currentHTML = currentHTML & "<p><strong> [" & selectedText & "]</strong></p>"
            txtContractTempCont.Html = currentHTML
        End If

    End Sub

    Private Sub PopulateCombo()

        lstLegend.DataSource = SQLHelper.ExecuteDataSet("EReportType_WebLegend", UserNo, Generic.ToInt(cboReportTypeNo.SelectedValue), PayLocNo)
        lstLegend.ValueField = "Legend"
        lstLegend.TextField = "Legend"
        lstLegend.DataBind()

    End Sub

    Protected Sub cboReportTypeNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)

        PopulateCombo()

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "frmContractTempList.aspx", "EContractTemp") Then

            Dim dt As DataTable
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtContractTempNo.Text)
            Dim ContractTempCode As String = Generic.ToStr(Me.txtContractTempCode.Text)
            Dim ContractTempDesc As String = Generic.ToStr(Me.txtContractTempDesc.Text)
            Dim ReportTypeNo As Integer = Generic.ToInt(Me.cboReportTypeNo.SelectedValue)
            Dim ContractTempCont As String = txtContractTempCont.Html
            Dim IsIncludeCompanyLogo As Boolean = Generic.ToBol(chkIsIncludeCompanyLogo.Checked)


            Dim ds As New DataSet
            Dim xRetVal As Integer = 0, xMessage As String = "", alertType As String = ""

            ds = SQLHelper.ExecuteDataSet("ETableReferrence_WebValidate", UserNo, Session("xTableName"), Generic.ToInt(txtCode.Text), ContractTempCode, ContractTempDesc, PayLocNo)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    xRetVal = Generic.ToInt(ds.Tables(0).Rows(0)("RetVal"))
                    xMessage = Generic.ToStr(ds.Tables(0).Rows(0)("xMessage"))
                    alertType = Generic.ToStr(ds.Tables(0).Rows(0)("alertType"))
                End If
            End If

            If xRetVal = 1 Then
                MessageBox.Alert(xMessage, alertType, Me)
                Exit Sub
            End If


            dt = SQLHelper.ExecuteDataTable("EContractTemp_WebSave", UserNo, tno, ContractTempCode, ContractTempDesc, ReportTypeNo, ContractTempCont, Generic.ToStr(Session("xMenuType")), PayLocNo, IsIncludeCompanyLogo)
            Dim forEscalation As Integer = 0
            Dim IsAdd As Boolean = False
            For Each row As DataRow In dt.Rows
                TransNo = Generic.ToInt(row("Retval"))
                Retval = True
            Next

            If Retval Then
                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim url As String = "frmContractTempEdit.aspx?id=" & TransNo
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

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "frmContractTempList.aspx", "EContractTemp") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))

        Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)
        txtCode.Enabled = False
        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
        txtContractTempCont.Enabled = IsEnabled
        lstLegend.Enabled = IsEnabled

        Dim IsFixed As Boolean = False
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EContractTemp_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            IsFixed = Generic.ToBol(row("IsFixed"))
        Next

        If IsEnabled = True And IsFixed = True Then
            Generic.EnableControls(Me, "pnlPopupMain", False)
            txtContractTempCont.Enabled = True
            lstLegend.Enabled = True
            chkIsIncludeCompanyLogo.Enabled = True
        End If

        If TransNo = 0 Then
            lnkPreview.Visible = False
        Else
            lnkPreview.Visible = True
        End If

    End Sub


#Region "********Reports********"

    Protected Sub lnkPreview_Click(sender As Object, e As EventArgs)

        Dim lnk As New Button
        Dim sb As New StringBuilder
        lnk = sender
        Dim id As Integer = TransNo

        Dim param As String = ""
        Dim tProceed As Boolean = False
        Dim ReportNo As Integer = 0, dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EContractTemp_WebViewer", UserNo, id)
        For Each row As DataRow In dt.Rows
            ReportNo = Generic.ToInt(row("ReportNo"))
            param = Generic.ToStr(row("param"))
            tProceed = Generic.ToStr(row("tProceed"))
        Next

        If tProceed = True Then
            sb.Append("<script>")
            sb.Append("window.open('rpttemplateviewer.aspx?reportno=" & ReportNo & "&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
            sb.Append("</script>")
            ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
        Else
            MessageBox.Warning("No access permission to view the report.", Me)
        End If

    End Sub

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As Button = TryCast(sender, Button)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub






#End Region

End Class

















