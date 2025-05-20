Imports System.Data
Imports Microsoft.VisualBasic
Imports clsLib


Partial Class Secured_BSProjectEdit
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim tmodify As Boolean = False
    Dim TransNo As Integer = 0
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim clsGeneric As New clsGenericClass


    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "BSProjectList.aspx", "EProject") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

       

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("onlineuserno"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.CheckDBNull(Request.QueryString("id"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        AccessRights.CheckUser(UserNo, "BSProjectList.aspx", "EProject")
        If Not IsPostBack Then
            If TransNo = 0 Then
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            End If
            PopulateData()

        End If
        EnabledControls()

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

   

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
    
    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("BBSProject_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))

        Next
    End Sub
    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))

        If txtIsArchived.Checked = True Then
            Enabled = False

        End If
        Generic.EnableControls(Me, "Panel1", Enabled)
        lnkModify.Visible = Not Enabled
        lnkSave.Visible = Enabled

    End Sub

   

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "BSProjectList.aspx", "EProject") Then
            Dim RetVal As Boolean = False
            Dim dt As DataTable

            Dim ProjectNo As Integer = Generic.ToInt(Me.txtProjectNo.Text)
            Dim ProjectCode As String = Generic.ToStr(txtProjectCode.Text)
            Dim ProjectDesc As String = Generic.ToStr(txtProjectDesc.Text)
            Dim Details As String = Generic.ToStr(txtDetails.Text)
            Dim StartDate As String = Generic.ToStr(txtStartDate.Text)
            Dim EndDate As String = Generic.ToStr(txtEndDate.Text)
            Dim Notes As String = Generic.ToStr(txtNotes.Text)
            Dim Reference As String = Generic.ToStr(txtReference.Text)
            Dim xAddress As String = Generic.ToStr(txtProjectAddress.Text)

            Dim BSClientNo As Integer = Generic.ToInt(Me.cboBSClientNo.SelectedValue)

            Dim BSCategoryNo As Integer = Generic.ToInt(Me.cboBSCategoryNo.SelectedValue)
            Dim BSPriorityNo As Integer = Generic.ToInt(Me.cboBSPriorityNo.SelectedValue)
            Dim BSStatusNo As Integer = Generic.ToInt(Me.cboBSStatusNo.SelectedValue)
            Dim BudgetDays As String = Generic.ToInt(txtBudgetDays.Text)
            Dim CurrentSalary As Double = Generic.ToDec(Me.txtBudgetDays.Text)
            Dim ProjectCost As Double = Generic.ToDec(Me.txtProjectCost.Text)
            Dim TaxRate As Double = Generic.ToDec(Me.txtTaxRate.Text)
            Dim ASFRate As Double = Generic.ToDec(Me.txtASFRate.Text)
            Dim Terms As String = Generic.ToInt(txtTerms.Text)
            Dim IsASFVAT As Boolean = Generic.ToBol(Me.txtIsASFVAT.Checked)

            dt = SQLHelper.ExecuteDataTable("BBSProject_WebSave", UserNo, ProjectNo, ProjectCode, ProjectDesc, Details, BSClientNo, Reference, BSCategoryNo, BSPriorityNo, BSStatusNo, StartDate, EndDate, BudgetDays, ProjectCost, Notes, TaxRate, ASFRate, Terms, IsASFVAT, 0, Generic.ToInt(Session("xPayLocNo")), xAddress)

            For Each row As DataRow In dt.Rows
                TransNo = Generic.ToInt(row("ProjectNo"))
                RetVal = True
            Next

            If RetVal = True Then
                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim url As String = "BSProjectEdit.aspx?id=" & TransNo
                    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                Else
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                    ViewState("IsEnabled") = False
                    EnabledControls()
                End If
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If
            'End If


        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub




End Class
