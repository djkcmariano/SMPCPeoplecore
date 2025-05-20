Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_PayTemplate_Contribution
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

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassNo"})))
            'PopulateData(Generic.ToInt(lnk.CommandArgument))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayClassNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayClass", UserNo, item)
                Generic.DeleteRecordAuditCol("EPayClassDTRRef", UserNo, "PayClassNo", item)
                Generic.DeleteRecordAuditCol("EPayClassDTRRefOT", UserNo, "PayClassNo", item)
                Generic.DeleteRecordAuditCol("EPayClassDTRRefHoliday", UserNo, "PayClassNo", item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub


    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecord() As Boolean
        Dim sssbaseformula As Integer = Generic.ToInt(Me.cboSSSBaseFormulaNo.SelectedValue)
        Dim ssspayscheduleno As Integer = Generic.ToInt(Me.cboSSSPayScheduleNo.SelectedValue)
        Dim isssseepanobyer As Integer = Generic.ToInt(Me.chkIsSSSEEPaNobyER.Checked)
        Dim phpayscheduleno As Integer = Generic.ToInt(Me.cboPHPayScheduleNo.SelectedValue)
        Dim isphseepanobyer As Integer = Generic.ToInt(Me.chkIsPHEEPaNobyER.Checked)
        Dim hdmfpayscheduleno As Integer = Generic.ToInt(Me.cboHDMFPayScheduleNo.SelectedValue)
        Dim ishdmfeepanobyer As Integer = Generic.ToInt(Me.chkIsHDMFEEPaNobyER.Checked)
        Dim ishdmfflatrate As Integer = Generic.ToInt(Me.chkIsHDMFFlatRate.Checked)
        Dim hdmfamount As Decimal = Generic.ToDec(Me.txtHDMFAmount.Text)
        Dim percenthdmf As Decimal = Generic.ToDec(Me.txtPercentHDMF.Text)
        Dim maxamthdmf As Decimal = Generic.ToDec(Me.txtMaxAmtHDMF.Text)
        Dim signatoryno As Integer = Generic.ToInt(hifsignatoryno.Value)
        Dim phbaseformula As Integer = Generic.ToInt(cboPHBaseFormula.SelectedValue)
        Dim hdmfbaseformula As Integer = Generic.ToInt(cboHDMFBaseFormula.SelectedValue)


        If SQLHelper.ExecuteNonQuery("EPayClass_WebSave_Contribution", UserNo, Generic.ToInt(txtCode.Text),
                                     sssbaseformula, ssspayscheduleno, isssseepanobyer, phpayscheduleno, isphseepanobyer,
                                     hdmfpayscheduleno, ishdmfeepanobyer, ishdmfflatrate, hdmfamount, percenthdmf, maxamthdmf,
                                     signatoryno, phbaseformula, hdmfbaseformula) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False

        End If
    End Function

    Private Sub PopulateData(ID As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayClass_WebOne", UserNo, ID)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next

    End Sub

#End Region



End Class





