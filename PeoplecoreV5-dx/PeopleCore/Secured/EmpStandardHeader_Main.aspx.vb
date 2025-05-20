Imports clsLib
Imports System.Data
Imports System.IO
Imports System.Web
Partial Class Secured_EmpStandardHeader_Main
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpStandardHeader.aspx", "EApplicantStandardHeader") Then
            Dim RetVal As Integer = SaveRecord()
            If RetVal > 0 Then
                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim url As String = "empstandardHeader_Main.aspx?id=" & RetVal & "&transno=" & ViewState("TransNo")
                    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                Else
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                    ViewState("IsEnabled") = False
                    EnabledControls()
                End If
                PopulateTabHeader()
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpStandardHeader.aspx", "EApplicantStandardHeader") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicantStandardMain_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))
        Next
        Try
            cboEvalTemplateNo.DataSource = SQLHelper.ExecuteDataSet("EEvalTemplate_WebLookup", UserNo, "0271", Generic.ToInt(Session("xPayLocNo")))
            cboEvalTemplateNo.DataValueField = "tNo"
            cboEvalTemplateNo.DataTextField = "tDesc"
            cboEvalTemplateNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "EmpStandardHeader.aspx", "EApplicantStandardHeader")
        ViewState("TransNo") = Generic.ToInt(Request.QueryString("transNo"))

        If Not IsPostBack Then
            PopulateTabHeader()
            If TransNo = 0 Then
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            End If
            PopulateData()
        End If

        EnabledControls()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", Enabled)
        txtApplicantStandardmainNo.Enabled = False

        lnkModify.Visible = Not Enabled
        lnkSave.Visible = Enabled
    End Sub

    Private Function SaveRecord() As Integer
        Dim Retval As Integer = 0
        Dim tno As Integer = Generic.ToInt(txtApplicantStandardmainNo.Text)
        Dim tcode As String = Generic.ToStr(txttCode.Text)
        Dim tdesc As String = Generic.ToStr(txttDesc.Text)
        Dim typeno As Integer = 0 ' Generic.ToInt(cboApplicantScreenTypeNo.SelectedValue)
        Dim screeningbyno As Integer = Generic.ToInt(hifScreeningByNo.Value)
        Dim OrderLevel As Integer = Generic.ToInt(txtOrderLevel.Text)
        Dim evaltemplateno As Integer = Generic.ToInt(cboEvalTemplateNo.SelectedValue)

        Dim dt As DataTable = SQLHelper.ExecuteDataTable("EApplicantStandardMain_WebSave", UserNo, tno, Generic.ToInt(ViewState("TransNo")), tcode, tdesc, typeno, screeningbyno, evaltemplateno, OrderLevel, Session("xPayLocNo"))
        For Each row As DataRow In dt.Rows
            Retval = Generic.ToInt(row("Retval"))
        Next

        Return Retval
    End Function

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicantStandardMain_WebHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
    End Sub

End Class
