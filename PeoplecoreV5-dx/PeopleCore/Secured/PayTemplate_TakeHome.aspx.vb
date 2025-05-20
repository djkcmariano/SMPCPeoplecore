Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_PayTemplate_TakeHome
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
        _dt = SQLHelper.ExecuteDataTable("EPayClass_Web", UserNo, "", PayLocNo)
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
        
        Dim maxamtaccumulatedexemp As Decimal = Generic.ToDec(Me.txtMaxAmtAccumulatedExemp.Text)
        Dim minnetpayfordedu As Decimal = Generic.ToDec(Me.txtMinNetPayForDedu.Text)
        Dim isminnetpayinpercent As Integer = Generic.ToInt(Me.chkIsMinNetPayInPercent.Checked)
        Dim currencyno As Integer = Generic.ToInt(Me.cboCurrencyNo.SelectedValue)
        Dim IsMonthlyTax As Boolean = Generic.ToBol(chkIsMonthlyTax.Checked)
        Dim IsProrateTax As Boolean = Generic.ToBol(chkIsProrateTax.Checked)
        If SQLHelper.ExecuteNonQuery("EPayClass_WebSave_TakeHome", UserNo, Generic.ToInt(txtCode.Text), maxamtaccumulatedexemp, minnetpayfordedu,
                                     isminnetpayinpercent, currencyno, IsMonthlyTax, IsProrateTax, PayLocNo) > 0 Then
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




