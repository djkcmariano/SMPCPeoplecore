Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_OrgPayClassList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TableName As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TableName = Generic.ToStr(Session("xTablename"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 14)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
            cboPayLocNo.DataTextField = "tdesc"
            cboPayLocNo.DataValueField = "tNo"
            cboPayLocNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()

    End Sub

#Region "Main"

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        Dim tStatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
        If tStatus = 0 Then
            lnkDelete.Visible = False
            lnkArchive.Visible = True
        ElseIf tStatus = 1 Then
            lnkDelete.Visible = True
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        Else
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        End If
        _dt = SQLHelper.ExecuteDataTable("EPayClass_Web", UserNo, "", Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
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

    Protected Sub lnkArchive_Click(sender As Object, e As EventArgs)

        Dim dt As DataTable, tProceed As Boolean = False
        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "PayClassNo"))
                dt = SQLHelper.ExecuteDataTable("ETableReferrence_WebArchived", UserNo, TableName, item, 1, PayLocNo)
                For Each row As DataRow In dt.Rows
                    tProceed = Generic.ToBol(row("tProceed"))
                Next
                grdMain.Selection.UnselectRow(j)
                i = i + 1
            End If
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") transaction(s) successfully archived.", Me)
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
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
        'Dim payclasscode As String = Generic.ToStr(Me.txtPayClassCode.Text)
        'Dim payclassdesc As String = Generic.ToStr(Me.txtPayClassDesc.Text)
        'Dim paylocno As Integer = Generic.ToInt(Me.cboPayLocNo.SelectedValue)
        'Dim notedbyno As Integer = Generic.ToInt(hifnotedbyno.Value)
        'Dim notedbyno2 As Integer = Generic.ToInt(hifnotedbyno2.Value)
        'Dim preparedbyno As Integer = Generic.ToInt(hifpreparedbyno.Value)
        'Dim preparedbyno2 As Integer = Generic.ToInt(hifpreparedbyno2.Value)
        'Dim reviewedbyno As Integer = Generic.ToInt(hifreviewedbyno.Value)
        'Dim reviewedbyno2 As Integer = Generic.ToInt(hifreviewedbyno2.Value)
        'Dim approvedbyno As Integer = Generic.ToInt(hifapprovedbyno.Value)
        'Dim approvedbyno2 As Integer = Generic.ToInt(hifapprovedbyno2.Value)
        'Dim sssno As String = Generic.ToStr(Me.txtSSSNo.Text)
        'Dim phno As String = Generic.ToStr(Me.txtPHNo.Text)
        'Dim hdmfno As String = Generic.ToStr(Me.txtHDMFNo.Text)
        'Dim tinno As String = Generic.ToStr(Me.txtTINNo.Text)
        'Dim tinbranchcode As String = Generic.ToStr(Me.txtTINBranchCode.Text)
        'Dim sssbaseformula As Integer = Generic.ToInt(Me.cboSSSBaseFormulaNo.SelectedValue)
        'Dim ssspayscheduleno As Integer = Generic.ToInt(Me.cboSSSPayScheduleNo.SelectedValue)
        'Dim isssseepanobyer As Integer = Generic.ToInt(Me.chkIsSSSEEPaNobyER.Checked)
        'Dim phpayscheduleno As Integer = Generic.ToInt(Me.cboPHPayScheduleNo.SelectedValue)
        'Dim isphseepanobyer As Integer = Generic.ToInt(Me.chkIsPHEEPaNobyER.Checked)
        'Dim hdmfpayscheduleno As Integer = Generic.ToInt(Me.cboHDMFPayScheduleNo.SelectedValue)
        'Dim ishdmfeepanobyer As Integer = Generic.ToInt(Me.chkIsHDMFEEPaNobyER.Checked)
        'Dim ishdmfflatrate As Integer = Generic.ToInt(Me.chkIsHDMFFlatRate.Checked)
        'Dim hdmfamount As Decimal = Generic.ToDec(Me.txtHDMFAmount.Text)
        'Dim percenthdmf As Decimal = Generic.ToDec(Me.txtPercentHDMF.Text)
        'Dim maxamthdmf As Decimal = Generic.ToDec(Me.txtMaxAmtHDMF.Text)
        'Dim maxamtaccumulatedexemp As Decimal = Generic.ToDec(Me.txtMaxAmtAccumulatedExemp.Text)
        'Dim minnetpayfordedu As Decimal = Generic.ToDec(Me.txtMinNetPayForDedu.Text)
        'Dim isminnetpayinpercent As Integer = Generic.ToInt(Me.chkIsMinNetPayInPercent.Checked)
        'Dim currencyno As Integer = Generic.ToInt(Me.cboCurrencyNo.SelectedValue)
        'Dim signatoryno As Integer = Generic.ToInt(hifsignatoryno.Value)
        'Dim IsCrew As Boolean = Generic.ToBol(txtIsCrew.Checked)
        'Dim phbaseformula As Integer = Generic.ToInt(cboPHBaseFormula.SelectedValue)
        'Dim hdmfbaseformula As Integer = Generic.ToInt(cboHDMFBaseFormula.SelectedValue)
        'Dim IsMonthlyTax As Boolean = Generic.ToBol(chkIsMonthlyTax.Checked)

        'If SQLHelper.ExecuteNonQuery("EPayClass_WebSave", UserNo, Generic.ToInt(txtCode.Text), payclasscode, payclassdesc, paylocno, notedbyno,
        '                             notedbyno2, preparedbyno, preparedbyno2, reviewedbyno, reviewedbyno2, approvedbyno, approvedbyno2, sssno,
        '                             phno, hdmfno, tinno, tinbranchcode, sssbaseformula, ssspayscheduleno, isssseepanobyer, phpayscheduleno, isphseepanobyer,
        '                             hdmfpayscheduleno, ishdmfeepanobyer, ishdmfflatrate, hdmfamount, percenthdmf, maxamthdmf, maxamtaccumulatedexemp, minnetpayfordedu,
        '                             isminnetpayinpercent, currencyno, signatoryno, IsCrew, phbaseformula, hdmfbaseformula, IsMonthlyTax) > 0 Then
        '    SaveRecord = True
        'Else
        '    SaveRecord = False

        'End If

        Dim payclasscode As String = Generic.ToStr(Me.txtPayClassCode.Text)
        Dim payclassdesc As String = Generic.ToStr(Me.txtPayClassDesc.Text)
        'Dim paylocno As Integer = Generic.ToInt(Me.cboPayLocNo.SelectedValue)
        Dim notedbyno As Integer = Generic.ToInt(hifnotedbyno.Value)
        Dim notedbyno2 As Integer = Generic.ToInt(hifnotedbyno2.Value)
        Dim preparedbyno As Integer = Generic.ToInt(hifpreparedbyno.Value)
        Dim preparedbyno2 As Integer = Generic.ToInt(hifpreparedbyno2.Value)
        Dim reviewedbyno As Integer = Generic.ToInt(hifreviewedbyno.Value)
        Dim reviewedbyno2 As Integer = Generic.ToInt(hifreviewedbyno2.Value)
        Dim approvedbyno As Integer = Generic.ToInt(hifapprovedbyno.Value)
        Dim approvedbyno2 As Integer = Generic.ToInt(hifapprovedbyno2.Value)
        Dim sssno As String = Generic.ToStr(Me.txtSSSNo.Text)
        Dim phno As String = Generic.ToStr(Me.txtPHNo.Text)
        Dim hdmfno As String = Generic.ToStr(Me.txtHDMFNo.Text)
        Dim tinno As String = Generic.ToStr(Me.txtTINNo.Text)
        Dim tinbranchcode As String = Generic.ToStr(Me.txtTINBranchCode.Text)
        Dim IsCrew As Boolean = Generic.ToBol(txtIsCrew.Checked)
        Dim NPNo As Integer = Generic.ToInt(cboNPNo.SelectedValue)
        Dim IsArchived As Boolean = Generic.ToBol(chkIsArchived.Checked)


        If SQLHelper.ExecuteNonQuery("EPayClass_WebSave", UserNo, Generic.ToInt(txtCode.Text), payclasscode, payclassdesc, paylocno, notedbyno,
                                     notedbyno2, preparedbyno, preparedbyno2, reviewedbyno, reviewedbyno2, approvedbyno, approvedbyno2, sssno,
                                     phno, hdmfno, tinno, tinbranchcode, IsCrew, NPNo, IsArchived) > 0 Then
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





