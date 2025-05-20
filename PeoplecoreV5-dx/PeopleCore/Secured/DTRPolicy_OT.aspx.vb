Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_DTRRefPolicyOT
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        AccessRights.CheckUser(UserNo)
        PopulateGridDetl()
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

#Region "Main"

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayClass_Web", UserNo, "", 0, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub


    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        ViewState("TransNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassNo"}))
        PopulateGridDetl()
    End Sub





    Private Sub PopulateData(ID As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayClass_WebOne", UserNo, ID)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next

    End Sub

#End Region

#Region "Detail"

    Private Sub PopulateGridDetl()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayClassDTRRefOT_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        Me.grdDetl.DataSource = _dt
        Me.grdDetl.DataBind()
    End Sub

    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel2")
            ModalPopupExtender2.Show()
        End If
    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataDetl(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassDTRRefOTNo"})))

            fRegisterStartupScript("JSDialogResponse", "getselectedvalue_server('" + cboOTDeductTypeNo.SelectedValue.ToString + "');")
            ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PayClassDTRRefOTNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayClassDTRRefOT", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridDetl()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
        If SaveRecordDetl() Then
            PopulateGridDetl()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecordDetl() As Boolean

        Dim employeeclassno As Integer = Generic.ToInt(Me.cboEmployeeClassNo.SelectedValue)
        Dim employeestatno As Integer = Generic.ToInt(Me.cboEmployeeStatNo.SelectedValue)
        Dim minadvothrs As Decimal = Generic.ToDec(Me.txtMinAdvOTHrs.Text)
        Dim minothrs As Decimal = Generic.ToDec(Me.txtMinOTHrs.Text)
        Dim maxot As Decimal = Generic.ToDec(Me.txtMaxOT.Text)
        Dim isdeductlatefrot As Boolean = Generic.ToBol(Me.chkIsDeductLateFrOT.Checked)
        Dim isdeductunderfrot As Boolean = Generic.ToBol(Me.chkIsDeductUnderFrOT.Checked)
        Dim isapplytoall As Boolean = Generic.ToBol(Me.chkIsApplyToAll.Checked)
        Dim roundofOT As Decimal = 0
        Dim isoffsetlateOT As Boolean = False
        Dim isoffsetunderOT As Boolean = False
        Dim OTFrom As Decimal = 0
        Dim OTTo As Decimal = 0
        Dim deductHrs As Decimal = Generic.ToDec(txtOTDeductHrs.Text)
        Dim deductTypeno As Integer = Generic.ToInt(cboOTDeductTypeNo.SelectedValue)
        Dim deductdTypeno As Integer = Generic.ToInt(cboOTDeductDTypeNo.SelectedValue)
        Dim fractionOT As Decimal = Generic.ToDec(txtFractionOT.Text)
        Dim IsNoOTIfLateCurrent As Boolean = Generic.ToBol(chkIsNoOTIfLateCurrent.Checked)
        Dim IsNoOTOnRDIfLateInRWD As Boolean = Generic.ToBol(chkIsNoOTOnRDIfLateInRWD.Checked)
        Dim IsNoOTOnRHIfLateInRWD As Boolean = Generic.ToBol(chkIsNoOTOnRHIfLateInRWD.Checked)
        Dim IsNoOTOnSHIfLateInRWD As Boolean = Generic.ToBol(chkIsNoOTOnSHIfLateInRWD.Checked)
        Dim IsAddWorkHrs As Boolean = Generic.ToBol(chkIsAddWorkHrs.Checked)

        If SQLHelper.ExecuteNonQuery("EPayClassDTRRefOT_WebSave", UserNo, Generic.ToInt(txtCodeDetl.Text), Generic.ToInt(ViewState("TransNo")), employeeclassno,
                                     employeestatno, minadvothrs, minothrs, roundofOT, maxot,
                                     isdeductlatefrot, isdeductunderfrot, isoffsetlateOT, isoffsetunderOT, OTFrom, OTTo, isapplytoall, deductHrs, deductdTypeno, deductTypeno, fractionOT,
                                     IsNoOTIfLateCurrent, IsNoOTOnRDIfLateInRWD, IsNoOTOnRHIfLateInRWD, IsNoOTOnSHIfLateInRWD, IsAddWorkHrs) > 0 Then
            SaveRecordDetl = True
        Else
            SaveRecordDetl = False
        End If
    End Function

    Private Sub PopulateDataDetl(ID As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayClassDTRRefOT_WebOne", UserNo, ID)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel2", dt)
        Next
    End Sub

    Protected Sub cbo_SelectedIndexChange(sender As Object, e As System.EventArgs)
        Try
            show_control(Generic.ToInt(cboOTDeductTypeNo.SelectedValue))
            ModalPopupExtender2.Show()
        Catch ex As Exception
        End Try

    End Sub
    Private Sub show_control(fval As Integer)
        If fval = 0 Then
            txtOTDeductHrs.Text = 0
            cboOTDeductDTypeNo.Text = 0
        End If
        fRegisterStartupScript("JSDialogResponse", "getselectedvalue_server('" + fval.ToString + "');")

    End Sub

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
#End Region

End Class





