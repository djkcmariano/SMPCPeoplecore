Imports System.Data
Imports clsLib

Partial Class Secured_DTRDetiListSummaryEdit
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
 
    Dim tmodify As Boolean = False
    Dim DTRDetiEditedNo As Integer = 0
    Dim DTRNo As Integer = 0

    Dim clsMessage As New clsMessage
    Dim tabOrder As Integer = 0
    Dim rowNo As Integer = 0
    Dim showFrm As New clsFormControls
    Dim xScript As String = ""

    'Display record
    Private Sub populateData()
        Dim _ds As New DataSet
        Dim _ds2 As New DataSet
        _ds = SQLHelper.ExecuteDataSet("eDTRDetiEdited_WebOne", xPublicVar.xOnlineUseNo, DTRDetiEditedNo)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                showFrm.showFormControls(Me, _ds)
            End If

        End If

    End Sub




    'Enable or disable control
    Private Sub DisableEnableCtrl(ByVal IsLock As Boolean)
        Dim clsEnable As New clsFormControls
        clsEnable.EnableControls(Me, IsLock)
        Me.lnkSubmit.Visible = IsLock
        Me.lnkModify.Visible = Not IsLock
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("onlineuserno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        AccessRights.CheckUser(xPublicVar.xOnlineUseNo, "DTR.aspx", "EDTR")
        DTRDetiEditedNo = Generic.CheckDBNull(Request.QueryString("DTRDetiEditedNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        DTRNo = Generic.CheckDBNull(Request.QueryString("transNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        tabOrder = Generic.CheckDBNull(Request.QueryString("tabOrder"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        xScript = Request.ServerVariables("SCRIPT_NAME")
        xScript = Generic.GetPath(xScript)

        tmodify = Generic.CheckDBNull(Request.QueryString("tModify"), clsBase.clsBaseLibrary.enumObjectType.IntType)

        If Not IsPostBack Then
            populateData()
        End If

        DisableEnableCtrl(tmodify)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    'Security for modify record


    Protected Sub lnkModify_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Generic.CheckDBNull(Me.txtIsServed.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType) Then
            MessageBox.Information(MessageTemplate.DeniedPost, Me)
            Exit Sub
        End If
        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit, "DTR.aspx", "EDTR") Then
            Response.Redirect("~/secured/DTRDetlList_ManualEdit.aspx?transNo=" & DTRNo & "&tModify=True&tabOrder=" & tabOrder & "&rowNO=" & rowNo)
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
        End If

    End Sub

    'Cancel modify
    Protected Sub lnkCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("~/secured/" & Session("xFormName") & "?transNo=" & DTRNo & "&tModify=False&tabOrder=" & tabOrder)
    End Sub

    'Submit record
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tProceed As Boolean = SaveRecord()
        If tProceed Then
            Dim url As String = "DTRDetlList_Manual.aspx?transNo=" & DTRNo & "&tModify=False&tabOrder=" & tabOrder
            MessageBox.SuccessResponse(clsMessage.GetMessageType(Global.clsMessage.EnumMessageType.SuccessSave), Me, url)
        Else
            MessageBox.Critical(clsMessage.GetMessageType(Global.clsMessage.EnumMessageType.ErrorSave), Me)
        End If
    End Sub


    Private Function SaveRecord() As Integer
        Dim employeeno As Integer = Generic.CheckDBNull(hifEmployeeNo.Value, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        Dim abshrs As Double = Generic.CheckDBNull(Me.txtAbsHrs.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim el As Double = Generic.CheckDBNull(txtEL.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim fl As Double = Generic.CheckDBNull(Me.txtFL.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim late As Double = Generic.CheckDBNull(Me.txtLate.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim mtl As Double = Generic.CheckDBNull(Me.txtML.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim np As Double = Generic.CheckDBNull(Me.txtNP.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim ob As Double = Generic.CheckDBNull(Me.txtOB.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim ovt As Double = Generic.CheckDBNull(Me.txtOvt.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim ovt8 As Double = Generic.CheckDBNull(Me.txtOvt8.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim pl As Double = Generic.CheckDBNull(Me.txtPL.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim ptl As Double = Generic.CheckDBNull(Me.txtPTL.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim rdovt As Double = Generic.CheckDBNull(Me.txtRDOvt.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim rdovt8 As Double = Generic.CheckDBNull(Me.txtRDOvt8.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim rdovt8np = Generic.CheckDBNull(Me.txtRDOvt8NP.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim rdovtnp As Double = Generic.CheckDBNull(Me.txtRDOvtNP.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim lhrdcount As Double = Generic.CheckDBNull(Me.txtRegH.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim rhnrovt As Double = Generic.CheckDBNull(Me.txtRHNROvt.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim rhnrovt8 As Double = Generic.CheckDBNull(Me.txtRHNROvt8.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim rhnrovt8np As Double = Generic.CheckDBNull(Me.txtRHNROvt8NP.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim rhnrovtnp As Double = Generic.CheckDBNull(Me.txtRHNROvtNP.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim rhrdovt As Double = Generic.CheckDBNull(Me.txtRHRDOvt.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim rhrdovt8 As Double = Generic.CheckDBNull(Me.txtRHRDOvt8.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim rhrdovtnp As Double = Generic.CheckDBNull(Me.txtRHRDOvtNP.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim rhrdovt8np As Double = Generic.CheckDBNull(Me.txtRHRDOvt8NP.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        Dim shnrovt As Double = Generic.CheckDBNull(Me.txtSHNROvt.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim shnrovt8 As Double = Generic.CheckDBNull(Me.txtSHNROvt8.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim shnrovt8np As Double = Generic.CheckDBNull(Me.txtSHNROvt8NP.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim shnrovtnp As Double = Generic.CheckDBNull(Me.txtSHNROvtNP.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        Dim shrdovt As Double = Generic.CheckDBNull(Me.txtSHRDOvt.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim shrdovt8 As Double = Generic.CheckDBNull(Me.txtSHRDOvt8.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim shrdovt8np As Double = Generic.CheckDBNull(Me.txtSHRDOvt8NP.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim shrdovtnp As Double = Generic.CheckDBNull(Me.txtSHRDOvtNP.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim sl As Double = Generic.CheckDBNull(Me.txtSL.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim shrdcount As Double = 0 'Generic.CheckDBNull(Me.txtSPLHrs.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim under As Double = Generic.CheckDBNull(Me.txtUnder.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim vl As Double = Generic.CheckDBNull(Me.txtVL.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim workinghrs As Double = Generic.CheckDBNull(Me.txtWorkingHrs.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim spl As Double = Generic.CheckDBNull(Me.txtSPL.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)


        If SQLHelper.ExecuteNonQuery("EDTRDetiEdited_WebSave", xPublicVar.xOnlineUseNo, DTRDetiEditedNo, DTRNo, employeeno, workinghrs, abshrs, late, under, ovt, ovt8, np, 0, vl, sl, mtl, 0, pl, el, 0, fl, 0, ob, shrdcount, lhrdcount, rdovt, rdovt8, rdovtnp, rdovt8np, rhnrovt, rhnrovt8, rhnrovtnp, rhnrovt8np, rhrdovt, rhrdovt8, rhrdovtnp, rhrdovt8np, shnrovt, shnrovt8, shnrovtnp, shnrovt8np, shrdovt, shrdovt8, shrdovtnp, shrdovt8np, spl, ptl) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False

        End If
    End Function

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub

End Class

