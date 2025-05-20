Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_AppMREdit_Applicant
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim ActionStatNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EMRHiredMass_Web", UserNo, TransNo, "", ActionStatNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))

        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateTabHeader()
        End If
        PopulateGrid()

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMR_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim fmrhiredmassno As Integer = CType(lnk.CommandArgument, Integer)
            hifMRHiredMassNo.Value = fmrhiredmassno
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRHiredMass_WebOne", UserNo, Generic.ToInt(fmrhiredmassno))
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            Next

            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim ApplicantStatNo As Integer = Generic.ToInt(cboApplicantStatNo.SelectedValue)

        If SQLHelper.ExecuteNonQuery("EMRHiredMass_WebSave_Applicant", UserNo, hifMRHiredMassNo.Value, ApplicantStatNo) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnk_Click(sender As Object, e As EventArgs)
        'Info1.xIsApplicant = True
        Dim lnk As New LinkButton
        lnk = sender

        Info1.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
        Info1.xIsApplicant = Generic.ToBol(Generic.Split(lnk.CommandArgument, 1))
        Info1.Show()
    End Sub

    Protected Sub lnkCAF_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender

        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj() As Object = container.Grid.GetRowValues(container.VisibleIndex, New String() {"MRNo", "IsApplicant", "ID"})

        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, Generic.ToInt(obj(0))), _
                                                  New ReportParameter(ReportParameter.Type.bol, Generic.ToInt(obj(1))), _
                                                  New ReportParameter(ReportParameter.Type.int, Generic.ToInt(obj(2))))

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewercode.aspx?reportcode=CAF&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkForm_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantNo"}))
        ViewState("ApplicantNo") = obj

        Dim param As String = Generic.ReportParam(
                                                  New ReportParameter(ReportParameter.Type.int, Generic.ToInt(ViewState("ApplicantNo")))
                                                  )

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewercode.aspx?reportcode=BSPForm&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkPDS_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender

        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantNo"}))
        ViewState("ApplicantNo") = obj

        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, Generic.ToInt(ViewState("ApplicantNo"))))

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewercode.aspx?reportcode=AppPDS&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

End Class
