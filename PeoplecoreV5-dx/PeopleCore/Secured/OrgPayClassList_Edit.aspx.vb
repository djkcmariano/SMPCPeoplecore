Imports System.Data
Imports Microsoft.VisualBasic
Imports clsLib


Partial Class Secured_OrgPayClassEdit
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim tmodify As Boolean = False
    Dim RefNo As Integer = 0
    Dim showFrm As New clsFormControls
    Dim rowno As Integer = 0
    Dim tabOrder As Integer = 0

    Private Sub populateData()
        Dim _ds As New DataSet
        Dim _ds2 As New DataSet
        _ds = SQLHelper.ExecuteDataset("EPayClass_WebOne", xPublicVar.xOnlineUseNo, RefNo)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                showFrm.showFormControls(Me, _ds)
            End If
        End If

        If RefNo = 0 Then
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
        'AccessRights.CheckUser(xPublicVar.xOnlineUseNo, "Orgpayclasslist.aspx")
            RefNo = Generic.CheckDBNull(Request.QueryString("RefNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
            tabOrder = Generic.CheckDBNull(Request.QueryString("tabOrder"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
           tabOrder = Generic.CheckDBNull(Request.QueryString("tabOrder"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
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
            'Response.Redirect("~/secured/" & clsarray.myPage.Pagename & "?RefNo=" & RefNo & "&tModify=True&rowNo=" & rowno & "&tabOrder=" & tabOrder)
            DisableEnableCtrl(True)
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
        End If

    End Sub

    'Cancel modify
    Protected Sub lnkCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCancel.Click
        Response.Redirect("~/secured/orgpayclasslist.aspx?RefNo=" & RefNo & "&tModify=False&tabOrder=" & tabOrder)
    End Sub

    'Submit record
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If SaveRecord() Then
            Dim url As String = "OrgPayClassList.aspx?RefNo=" & RefNo & "&tModify=False&tabOrder=" & tabOrder
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Private Function SaveRecord() As Boolean
        Dim payclasscode As String = Generic.CheckDBNull(Me.txtPayClassCode.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim payclassdesc As String = Generic.CheckDBNull(Me.txtPayClassDesc.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim paylocno As Integer = Generic.CheckDBNull(Me.cboPayLocNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim notedbyno As Integer = Generic.CheckDBNull(hifnotedbyno.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim notedbyno2 As Integer = Generic.CheckDBNull(hifnotedbyno2.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim preparedbyno As Integer = Generic.CheckDBNull(hifpreparedbyno.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim preparedbyno2 As Integer = Generic.CheckDBNull(hifpreparedbyno2.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim reviewedbyno As Integer = Generic.CheckDBNull(hifreviewedbyno.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim reviewedbyno2 As Integer = Generic.CheckDBNull(hifreviewedbyno2.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim approvedbyno As Integer = Generic.CheckDBNull(hifapprovedbyno.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim approvedbyno2 As Integer = Generic.CheckDBNull(hifapprovedbyno2.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim sssno As String = Generic.CheckDBNull(Me.txtSSSNo.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim phno As String = Generic.CheckDBNull(Me.txtPHNo.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim hdmfno As String = Generic.CheckDBNull(Me.txtHDMFNo.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim tinno As String = Generic.CheckDBNull(Me.txtTINNo.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim tinbranchcode As String = Generic.CheckDBNull(Me.txtTINBranchCode.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim sssbaseformula As Integer = Generic.CheckDBNull(Me.cboSSSBaseFormulaNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim ssspayscheduleno As Integer = Generic.CheckDBNull(Me.cboSSSPayScheduleNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim isssseepanobyer As Integer = Generic.CheckDBNull(Me.txtIsSSSEEPaNobyER.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim phpayscheduleno As Integer = Generic.CheckDBNull(Me.cboPHPayScheduleNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim isphseepanobyer As Integer = Generic.CheckDBNull(Me.txtIsPHEEPaNobyER.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim hdmfpayscheduleno As Integer = Generic.CheckDBNull(Me.cboHDMFPayScheduleNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim ishdmfeepanobyer As Integer = Generic.CheckDBNull(Me.txtIsHDMFEEPaNobyER.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim ishdmfflatrate As Integer = Generic.CheckDBNull(Me.txtIsHDMFFlatRate.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim hdmfamount As Double = Generic.CheckDBNull(Me.txtHDMFAmount.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim percenthdmf As Double = Generic.CheckDBNull(Me.txtPercentHDMF.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim maxamthdmf As Double = Generic.CheckDBNull(Me.txtMaxAmtHDMF.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim maxamtaccumulatedexemp As Double = Generic.CheckDBNull(Me.txtMaxAmtAccumulatedExemp.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim minnetpayfordedu As Double = Generic.CheckDBNull(Me.txtMinNetPayForDedu.Text, clsBase.clsBaseLibrary.enumObjectType.DblType)
        Dim isminnetpayinpercent As Integer = Generic.CheckDBNull(Me.txtIsMinNetPayInPercent.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim currencyno As Integer = Generic.CheckDBNull(Me.cboCurrencyNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim signatoryno As Integer = Generic.CheckDBNull(hifsignatoryno.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim IsCrew As Boolean = 0 'Generic.ToBol(txtIsCrew.Checked)

        If SQLHelper.ExecuteNonQuery("EPayClass_WebSave", xPublicVar.xOnlineUseNo, RefNo, payclasscode, payclassdesc, paylocno, notedbyno, notedbyno2, preparedbyno, preparedbyno2, reviewedbyno, reviewedbyno2, approvedbyno, approvedbyno2, sssno, phno, hdmfno, tinno, tinbranchcode, sssbaseformula, ssspayscheduleno, isssseepanobyer, phpayscheduleno, isphseepanobyer, hdmfpayscheduleno, ishdmfeepanobyer, ishdmfflatrate, hdmfamount, percenthdmf, maxamthdmf, maxamtaccumulatedexemp, minnetpayfordedu, isminnetpayinpercent, currencyno, signatoryno, IsCrew) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False

        End If
    End Function
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
End Class



