Imports System.Data
Imports Microsoft.VisualBasic
Imports clsLib

Partial Class Secured_BSBillingCollection_Edit
    Inherits System.Web.UI.Page

    Dim tmodify As Boolean = False
    Dim TransNo As Integer = 0
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim clsGeneric As New clsGenericClass


    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "BSBillingCollection.aspx", "BBSRegister") Then
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
        AccessRights.CheckUser(UserNo, "BSBillingCollection.aspx", "BBSRegister")
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
        dt = SQLHelper.ExecuteDataTable("BBSRegister_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))

        Next
    End Sub
    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        lnkModify.Visible = Not Enabled
        lnkSave.Visible = Enabled

    End Sub



    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "BSBillingCollection.aspx", "BBSRegister") Then
            Dim RetVal As Boolean = False
            Dim dt As DataTable

            Dim BSRegisterNo As Integer = Generic.ToInt(txtBSRegisterNo.Text)
            Dim Amount As Double = Generic.ToDec(Me.txtAmount.Text)
            Dim BSRegisterDesc As String = Generic.ToStr(Me.txtBSRegisterDesc.Text)
            Dim Details As String = Generic.ToStr(Me.txtDetails.Text)
            Dim BSClientNo As Integer = Generic.ToInt(Me.cboBSClientNo.SelectedValue)
            Dim ORDate As String = Generic.ToStr(txtORDate.Text)
            Dim ReceivedDate As String = Generic.ToStr(txtReceivedDate.Text)
            Dim ProjectNo As Integer = Generic.ToInt(Me.cboProjectNo.SelectedValue)
            Dim ORNo As String = Generic.ToStr(Me.txtORNo.Text)
            Dim CheckNo As String = Generic.ToStr(Me.txtCheckNo.Text)
            Dim BankCode As String = Generic.ToStr(Me.txtBankCode.Text)
            'Dim BSCategoryNo As Integer = Generic.ToInt(Me.cboBSCategoryNo.SelectedValue)


            dt = SQLHelper.ExecuteDataTable("BBSRegister_WebSave", UserNo, BSRegisterNo, BSRegisterDesc, BSClientNo, ProjectNo, ORNo, ORDate, ReceivedDate, CheckNo, Details, Amount, PayLocNo, BankCode)

            For Each row As DataRow In dt.Rows
                TransNo = Generic.ToInt(row("BSRegisterNo"))
                RetVal = True
            Next

            If RetVal = True Then
                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim url As String = "BSBillingCollection_Edit.aspx?id=" & TransNo
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

