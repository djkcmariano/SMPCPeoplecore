Imports clsLib
Imports System.Data

Partial Class Secured_SecCMSTemplateEdit
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim CMSTemplateNo As Int64 = 0
    Dim CMSCategoryNo As Int64 = 0
    Dim CMSCategoryDesc As String = ""

    Protected Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ECMSTemplate_WebOne", UserNo, CMSTemplateNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "pnlPopupMain", dt)
            ASPxHtmlEditor1.Html = Generic.ToStr(row("CMSTemplateDesc"))
        Next
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        CMSTemplateNo = Generic.ToInt(Request.QueryString("CMSTemplateNo"))
        CMSCategoryNo = Generic.ToInt(Request.QueryString("CMSCategoryNo"))
        CMSCategoryDesc = Generic.ToStr(Request.QueryString("CMSCategoryDesc"))
        AccessRights.CheckUser(UserNo)


        If Not IsPostBack Then
            PopulateData()
            lblTitle.Text = CMSCategoryDesc
        End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False, Count As Integer = 0
            Dim Template As String = ASPxHtmlEditor1.Html

            If SQLHelper.ExecuteNonQuery("ECMSTemplate_WebSave", UserNo, CMSTemplateNo, "", Template, CMSCategoryNo, PayLocNo) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, "../secured/SecCMSTemplateList.aspx?")
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

End Class

















