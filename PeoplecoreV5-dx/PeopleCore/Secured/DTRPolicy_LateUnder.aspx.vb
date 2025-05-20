Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_DTRRefPolicyLateUnder
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

        If ViewState("TransNo") = 0 Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"PayClassNo", "Code"})
            ViewState("TransNo") = obj(0)
            lblDetl.Text = obj(1)
        End If

        PopulateGridDetl()

    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassNo", "Code"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
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
        _dt = SQLHelper.ExecuteDataTable("EPayClassDTRRef_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        Me.grdDetl.DataSource = _dt
        Me.grdDetl.DataBind()
    End Sub

    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            If Generic.ToInt(ViewState("TransNo")) > 0 Then
                Generic.ClearControls(Me, "Panel2")
                ModalPopupExtender2.Show()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Generic.ClearControls(Me, "Panel2")
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataDetl(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassDTRRefNo"})))
            ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PayClassDTRRefNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayClassDTRRef", UserNo, item)
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
        Dim minlate As Decimal = Generic.ToDec(Me.txtMinLate.Text)
        Dim minut As Decimal = Generic.ToDec(Me.txtMinUT.Text)
        Dim maxlate As Decimal = Generic.ToDec(Me.txtMaxLate.Text)
        Dim maxlate2 As Decimal = Generic.ToDec(Me.txtMaxLate2.Text)
        Dim roundoflate As Decimal = Generic.ToDec(Me.txtRoundOfLate.Text)
        Dim maxut As Decimal = Generic.ToDec(Me.txtMaxUT.Text)
        Dim maxut2 As Decimal = Generic.ToDec(Me.txtMaxUT2.Text)
        Dim roundofut As Decimal = Generic.ToDec(Me.txtRoundOfUT.Text)
        Dim minadvothrs As Decimal = 0
        Dim minothrs As Decimal = 0
        Dim maxot As Decimal = 0
        Dim isdeductlatefrot As Boolean = False
        Dim isdeductunderfrot As Boolean = False
        Dim isapplytoall As Boolean = Generic.ToBol(Me.chkIsApplyToAll.Checked)
        Dim fractionLate As Double = Generic.ToDec(txtFractionLate.Text)
        Dim fractionUT As Double = Generic.ToDec(txtFractionUT.Text)
        Dim isroundoflatehalf As Boolean = Generic.ToBol(txtIsRoundofLateHalf.Checked)
        Dim isroundoflateWhole As Boolean = Generic.ToBol(txtIsRoundofLateWhole.Checked)
        Dim isroundofUThalf As Boolean = Generic.ToBol(txtIsRoundofUTHalf.Checked)
        Dim isroundofUTWhole As Boolean = Generic.ToBol(txtIsRoundofUTWhole.Checked)

        If SQLHelper.ExecuteNonQuery("EPayClassDTRRef_WebSave", UserNo, Generic.ToInt(txtCodeDetl.Text), Generic.ToInt(ViewState("TransNo")), employeeclassno,
                                     employeestatno, minlate, minut, maxlate, maxlate2, roundoflate, maxut, maxut2, roundofut, minadvothrs, minothrs, maxot,
                                     isdeductlatefrot, isdeductunderfrot, isapplytoall, fractionLate, fractionUT, isroundoflatehalf, isroundoflateWhole, isroundofUThalf, isroundofUTWhole) > 0 Then
            SaveRecordDetl = True
        Else
            SaveRecordDetl = False
        End If
    End Function

    Private Sub PopulateDataDetl(ID As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayClassDTRRef_WebOne", UserNo, Generic.ToInt(ViewState("TransNo")), ID)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel2", dt)
        Next
    End Sub

#End Region

End Class

