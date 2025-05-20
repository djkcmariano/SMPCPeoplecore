Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_DTRRefPolicyHoliday
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
        _dt = SQLHelper.ExecuteDataTable("EPayClassDTRRefHoliday_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        Me.grdDetl.DataSource = _dt
        Me.grdDetl.DataBind()
    End Sub

    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            If Generic.ToInt(ViewState("TransNo")) > 0 Then
                Generic.ClearControls(Me, "Panel2")
                PopulateApplyToAll(True)
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
            PopulateDataDetl(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassDTRRefHolidayNo"})))
            ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PayClassDTRRefHolidayNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayClassDTRRefHoliday", UserNo, item)
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
        Dim isapplytoall As Boolean = Generic.ToBol(Me.chkIsApplyToAll.Checked)

        Dim IsCheckAdaybefore As Boolean = Generic.ToBol(chkIsCheckAdaybefore.Checked)
        Dim IsAdaybeforeActualWorkingHrs As Boolean = Generic.ToBol(chkIsAdaybeforeActualWorkingHrs.Checked)

        If SQLHelper.ExecuteNonQuery("EPayClassDTRRefHoliday_WebSave", UserNo, Generic.ToInt(txtCode.Text), Generic.ToInt(ViewState("TransNo")), employeeclassno,
                                     employeestatno, IsCheckAdaybefore, IsAdaybeforeActualWorkingHrs, isapplytoall) > 0 Then
            SaveRecordDetl = True
        Else
            SaveRecordDetl = False
        End If
    End Function

    Private Sub PopulateDataDetl(ID As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayClassDTRRefHoliday_WebOne", UserNo, ID)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel2", dt)
            PopulateApplyToAll(Generic.ToBol(row("IsApplyToAll")))
        Next
    End Sub

    Private Sub PopulateApplyToAll(IsApplyToAll As Boolean)
        chkIsApplyToAll.Checked = IsApplyToAll
        If IsApplyToAll = True Then
            cboEmployeeClassNo.Enabled = False
            cboEmployeeStatNo.Enabled = False
            cboEmployeeClassNo.Text = ""
            cboEmployeeStatNo.Text = ""
        Else
            cboEmployeeClassNo.Enabled = True
            cboEmployeeStatNo.Enabled = True
        End If
    End Sub

#End Region

End Class






