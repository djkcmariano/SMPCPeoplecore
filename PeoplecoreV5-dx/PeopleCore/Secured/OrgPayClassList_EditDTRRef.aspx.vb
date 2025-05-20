Imports System.Data
Imports Microsoft.VisualBasic
Imports clsLib

Partial Class Secured_OrgPayClassDTRRefEdit
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim tmodify As Boolean = False
    Dim RefNo As Integer = 0
    Dim RefDetiNo As Integer = 0
    Dim showFrm As New clsFormControls
    Dim rowno As Integer = 0


    Private Sub populateData()
        Dim _ds As New DataSet
        Dim _ds2 As New DataSet
        _ds = SQLHelper.ExecuteDataset("EPayClassDTRRef_WebOne", xPublicVar.xOnlineUseNo, RefNo, RefDetiNo)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                showFrm.showFormControls(Me, _ds)
            End If
        End If

        If RefDetiNo = 0 Then
            Me.txtCode.Text = "Autonumber"
        End If
    End Sub
    Private Sub PopulateCombo()
        showFrm.populateCombo(xPublicVar.xOnlineUseNo, Me, Session("xPayLocNo"))

    End Sub

    'Enable or disable control
    Private Sub DisableEnableCtrl(ByVal IsLock As Boolean)
        Dim clsEnable As New clsFormControls
        clsEnable.EnableControls(Me, IsLock)
        lnkSubmit.Visible = IsLock
        lnkModify.Visible = Not IsLock

    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("onlineuserno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        AccessRights.CheckUser(xPublicVar.xOnlineUseNo, "Orgpayclasslist.aspx")
        RefDetiNo = Generic.CheckDBNull(Request.QueryString("RefDetiNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        RefNo = Generic.CheckDBNull(Request.QueryString("RefNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        tmodify = Generic.CheckDBNull(Request.QueryString("tModify"), clsBase.clsBaseLibrary.enumObjectType.IntType)
        If Not IsPostBack Then
            populateData()
            PopulateCombo()
        End If

        DisableEnableCtrl(tmodify)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    'Security for modify record


    Protected Sub lnkModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkModify.Click

        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit, "OrgPayclassList.aspx") Then
            Response.Redirect("~/secured/orgpayclasslist_editdtrRef.aspx?RefNo=" & RefNo & "&tModify=True&rowNo=" & rowno & "&RefDetiNo=" & RefDetiNo)
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
        End If

    End Sub

    'Cancel modify
    Protected Sub lnkCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCancel.Click
        Response.Redirect("~/secured/orgpayclasslist.aspx?RefNo=" & RefNo & "&tModify=False")
    End Sub

    'Submit record
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If SaveRecord() Then
            Dim url As String = "OrgPayClassList.aspx?RefNo=" & RefNo & "&tModify=False"
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Private Function SaveRecord() As Boolean
        Dim employeeclassno As Integer = Generic.CheckDBNull(Me.cboEmployeeClassNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim employeestatno As Integer = Generic.CheckDBNull(Me.cboEmployeeStatNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim minlate As Double = Generic.CheckDBNull(Me.txtMinLate.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim minut As Double = Generic.CheckDBNull(Me.txtMinUT.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim maxlate As Double = Generic.CheckDBNull(Me.txtMaxLate.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim maxlate2 As Double = Generic.CheckDBNull(Me.txtMaxLate2.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim roundoflate As Double = Generic.CheckDBNull(Me.txtRoundOfLate.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim maxut As Double = Generic.CheckDBNull(Me.txtMaxUT.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim maxut2 As Double = Generic.CheckDBNull(Me.txtMaxUT2.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim roundofut As Double = Generic.CheckDBNull(Me.txtRoundOfUT.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim minadvothrs As Double = Generic.CheckDBNull(Me.txtMinAdvOTHrs.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim minothrs As Double = Generic.CheckDBNull(Me.txtMinOTHrs.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim maxot As Double = Generic.CheckDBNull(Me.txtMaxOT.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim isdeductlatefrot As Boolean = Generic.CheckDBNull(Me.txtIsDeductLateFrOT.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim isdeductunderfrot As Boolean = Generic.CheckDBNull(Me.txtIsDeductUnderFrOT.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim isapplytoall As Boolean = Generic.CheckDBNull(Me.txtIsApplyToAll.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)

        If SQLHelper.ExecuteNonQuery("EPayClassDTRRef_WebSave", xPublicVar.xOnlineUseNo, RefDetiNo, RefNo, employeeclassno, employeestatno, minlate, minut, maxlate, maxlate2, roundoflate, maxut, maxut2, roundofut, minadvothrs, minothrs, maxot, isdeductlatefrot, isdeductunderfrot, isapplytoall) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False

        End If
    End Function
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
End Class



