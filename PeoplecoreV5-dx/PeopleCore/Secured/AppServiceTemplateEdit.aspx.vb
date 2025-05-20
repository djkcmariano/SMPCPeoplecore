Imports clsLib
Imports System.Data

Partial Class Secured_AppServiceTemplateEdit
    Inherits System.Web.UI.Page
    Dim userNo As Integer = 0
    Dim payLocNo As Integer = 0
    Dim transNo As Integer = 0

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(userNo, AccessRights.EnumPermissionType.AllowEdit, "appserviceTemplate.aspx", "EApplicantCoreTemplate") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Private Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EApplicantCoreTemplate_WebOne", 0, id)
            Generic.PopulateData(Me, "Panel1", dt)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        userNo = Generic.ToInt(Session("onlineuserno"))
        payLocNo = Generic.ToInt(Session("xPayLocNo"))
        transNo = Generic.CheckDBNull(Request.QueryString("id"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        AccessRights.CheckUser(userNo, "appserviceTemplate.aspx", "EApplicantCoreTemplate")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(userNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateData(transNo)
        End If
        EnabledControls()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Private Sub EnabledControls()
        If transNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", Enabled)
    End Sub
    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(userNo, AccessRights.EnumPermissionType.AllowEdit, "appserviceTemplate.aspx", "EApplicantCoreTemplate") Then
            SaveRecord()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Private Sub SaveRecord()
        Dim dt As DataTable
        Dim tNo As Integer = Generic.ToInt(hifApplicantCoreTemplateNo.Value)
        Dim cbo As Integer = Generic.ToInt(cboApplicantCoreCateNo.SelectedValue)

        Dim retVal As Boolean = False, error_num As Integer = 0, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("EApplicantCoreTemplate_WebSave", userNo, tNo, cbo, txtMessages.Html.ToString)
        For Each row As DataRow In dt.Rows
            RetVal = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                RetVal = False
            End If

        Next
        If RetVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If RetVal = True Then
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, "appServiceTemplate.aspx")
        End If
    End Sub
End Class
