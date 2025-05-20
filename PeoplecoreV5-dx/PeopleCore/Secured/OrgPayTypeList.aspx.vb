Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_OrgPayTypeList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        PopulateGridDetl()

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

#Region "Main"
    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub
    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayType_Web", UserNo, "", PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If ViewState("TransNo") = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"PayTypeNo", "Code"})
            ViewState("TransNo") = obj(0)
            lblDetl.Text = obj(1)
        End If

        PopulateGridDetl()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            lnkSave.Enabled = True

            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try

            ModalPopupExtender1.Show()
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTypeNo", "IsEnabled"})
            Dim iNo As Integer = Generic.ToInt(obj(0))
            Dim ifixed As Boolean = Generic.ToBol(obj(1))
            lnkSave.Enabled = ifixed

            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try
            PopulateData(Generic.ToInt(iNo))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayTypeNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayType", UserNo, item)
                Generic.DeleteRecordAuditCol("EPayTypeSched", UserNo, "PayTypeNo", item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTypeNo", "Code"})
        ViewState("TransNo") = Generic.ToStr(obj(0))
        lblDetl.Text = Generic.ToStr(obj(1))
        PopulateGridDetl()
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = SaveRecord()
        If RetVal Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecord() As Boolean
        Dim paytypecode As String = Generic.ToStr(Me.txtPayTypeCode.Text)
        Dim paytypedesc As String = Generic.ToStr(Me.txtPayTypeDesc.Text)
        Dim payperiods As Integer = Generic.ToInt(Me.txtPayPeriods.Text)

        If SQLHelper.ExecuteNonQuery("EPayType_WebSave", UserNo, Generic.ToInt(txtCode.Text), paytypecode, paytypedesc, payperiods, Generic.ToInt(cboPayLocNo.SelectedValue)) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If
    End Function

    Private Sub PopulateData(ID As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayType_WebOne", UserNo, ID)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next
    End Sub

#End Region

#Region "Detail"

    Private Sub PopulateGridDetl()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayTypeSched_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdDetl.DataSource = _dt
        grdDetl.DataBind()
    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then

            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTypeSchedNo", "IsEnabled"})
            Dim iNo As Integer = Generic.ToInt(obj(0))
            Dim ifixed As Boolean = Generic.ToBol(obj(1))
            lnkSaveDetl.Enabled = ifixed
            Generic.ClearControls(Me, "Panel2")
            PopulateDataDetl(Generic.ToInt(iNo))
            ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
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

    Private Sub PopulateDataDetl(ID As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayTypeSched_WebOne", UserNo, ID)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel2", dt)
        Next
    End Sub

    Private Function SaveRecordDetl() As Boolean
        Dim tProceed As Boolean = True
        Dim applicablemonth As Integer = Generic.CheckDBNull(Me.cboApplicableMonth.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim payscheduleno As Integer = Generic.CheckDBNull(Me.cboPaySchedNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)

        If cboApplicableMonth.SelectedItem.Text = "" Or cboPaySchedNo.SelectedItem.Text = "" Then
            tProceed = False
        End If

        If tProceed Then
            If SQLHelper.ExecuteNonQuery("EPayTypeSched_WebSave", UserNo, Generic.ToInt(txtPCode.Text), Generic.ToInt(ViewState("TransNo")), applicablemonth, payscheduleno) > 0 Then
                SaveRecordDetl = True
            Else
                SaveRecordDetl = False
            End If
        Else
            SaveRecordDetl = False
        End If

    End Function

#End Region

End Class



