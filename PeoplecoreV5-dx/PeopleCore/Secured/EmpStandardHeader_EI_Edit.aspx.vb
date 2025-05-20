Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_EmpStandardHeader_EI_Edit
    Inherits System.Web.UI.Page
    Dim tmodify As Boolean = False
    Dim TransNo As Integer = 0
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim clsGeneric As New clsGenericClass


    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "empStandardHeader_EI.aspx", "EEmployeeEI") Then
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
        AccessRights.CheckUser(UserNo, "empStandardHeader_EI.aspx", "EEmployeeEI")

        If Not IsPostBack Then
            If TransNo = 0 Then
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            End If

            PopulateDropdown()
            PopulateTabHeader()
            PopulateData()
        End If

        EnabledControls()

        'If cboStatNo.SelectedValue = "4" Then
        'lnkModify.Visible = False
        'End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
    Private Sub PopulateDropdown()

        Try
            cboApplicantStandardHeaderNo.DataSource = SQLHelper.ExecuteDataSet("EApplicantStandardHeader_WebLookup", UserNo, 0, 2, PayLocNo)
            cboApplicantStandardHeaderNo.DataValueField = "tNo"
            cboApplicantStandardHeaderNo.DataTextField = "tDesc"
            cboApplicantStandardHeaderNo.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboStatNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 46)
            cboStatNo.DataTextField = "tDesc"
            cboStatNo.DataValueField = "tno"
            cboStatNo.DataBind()

            cboStatNo.Items.RemoveAt(5)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeEI_WebOne", UserNo, TransNo)
        Dim IsEnabled As Boolean = False
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, PayLocNo)
            'ViewState("IsEnabled") = Generic.ToBol(row("IsEnabled"))
            ViewState("HRANNo") = Generic.ToInt(row("HRANNo"))
        Next

    End Sub
    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", Enabled)

        If Generic.ToInt(ViewState("HRANNo")) > 0 And Enabled = True Then
            txtEffectivity.Enabled = False
            cboApplicantStandardHeaderNo.Enabled = False
        End If

        cboStatNo.Enabled = False
        If cboStatNo.Text = "3" Or cboStatNo.Text = "4" Then
            lnkModify.Visible = False
            lnkSave.Visible = False
        Else
            lnkModify.Visible = Not Enabled
            lnkSave.Visible = Enabled
        End If

    End Sub

    Private Sub PopulateTabHeader()
        Try
            'Dim dt As DataTable
            'dt = SQLHelper.ExecuteDataTable("EHRAN_WebTabHeader", UserNo, TransNo)
            'For Each row As DataRow In dt.Rows
            '    lbl.Text = Generic.ToStr(row("Display"))
            'Next
            lbl.Text = "Transaction No. : " & Pad.PadZero(8, TransNo)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim RetVal As Boolean = False
        Dim EmployeeEINo As Integer = Generic.ToInt(txtEmployeeEINo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))
        Dim applicantStandardHeaderNo As Integer = Generic.ToInt(cboApplicantStandardHeaderNo.SelectedValue)
        Dim effectivity As String = Generic.ToStr(txtEffectivity.Text)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""

        Dim obj As Object
        obj = SQLHelper.ExecuteScalar("EEmployeeEI_WebSave", UserNo, EmployeeEINo, EmployeeNo, applicantStandardHeaderNo, effectivity, PayLocNo, txtInterviewDate.Text, txtRemarks.Text, 0)



        If Generic.ToInt(obj) > 0 Then
            Dim url As String = "EmpStandardHeader_EI_Edit.aspx?id=" & Generic.ToInt(obj)
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub
  
End Class
