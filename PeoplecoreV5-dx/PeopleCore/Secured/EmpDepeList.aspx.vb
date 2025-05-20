Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_EmpDepeList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        'Try
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeDepe_Web", UserNo, TransNo)
        grdMain.DataSource = dt
        grdMain.DataBind()
        'Catch ex As Exception

        'End Try
    End Sub

    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeDepe_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateTabHeader()
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        'If SaveRecord() Then
        '    MessageBox.Success(MessageTemplate.SuccessSave, Me)
        '    PopulateGrid()
        'Else
        '    MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        'End If
        Dim RelationshipNo As Integer = Generic.ToInt(Me.cboRelationShipNo.SelectedValue)
        Dim EmployeeDepeNo As Integer = Generic.ToInt(Me.txtCode.Text)
        Dim CivilStatNo As Integer = Generic.ToInt(Me.cboCivilStatNo.SelectedValue)
        Dim IsDependent As Boolean = Generic.ToBol(Me.chkIsDependent.Checked)
        Dim IsBeneficiary As Boolean = Generic.ToBol(Me.chkIsBeneficiary.Checked)
        Dim IsInsurance As Boolean = Generic.ToBol(Me.chkIsInsurance.Checked)
        Dim IsWithHMO As Boolean = Generic.ToBol(Me.chkIsWithHMO.Checked)
        Dim IsDeceased As Boolean = Generic.ToBol(Me.chkIsDeceased.Checked)
        Dim IsDisabled As Boolean = Generic.ToBol(Me.chkIsDisabled.Checked)
        Dim DependentCode As String = Generic.ToStr(Me.txtDependentCode.Text)

        Dim RetVal As Boolean = False
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EEmployeeDepe_WebValidate", UserNo, EmployeeDepeNo, TransNo, txtLastName.Text, txtFirstName.Text, txtMiddleName.Text, RelationshipNo, _
                                      txtOccupation.Text, txtBirthDate.Text, txtPhoneNo.Text, IsInsurance, "", IsBeneficiary, txtHomeAddress.Text, txtEmployerName.Text, _
                                      txtEmployerAdd.Text, txtEmployerTelNo.Text, IsDependent, CivilStatNo, IsWithHMO, DependentCode)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EEmployeeDepe_WebSave", UserNo, EmployeeDepeNo, TransNo, txtLastName.Text, txtFirstName.Text, txtMiddleName.Text, RelationshipNo, _
                                      txtOccupation.Text, txtBirthDate.Text, txtPhoneNo.Text, IsInsurance, "", IsBeneficiary, txtHomeAddress.Text, txtEmployerName.Text, _
                                      txtEmployerAdd.Text, txtEmployerTelNo.Text, IsDependent, CivilStatNo, IsWithHMO, IsDeceased, IsDisabled, txtMaidenName.Text, _
                                      Generic.ToInt(cboEmployeeExtNo.SelectedValue), DependentCode, txtRemark.Text) > 0 Then
            RetVal = True
        Else
            RetVal = False

        End If

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeDepeNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeDepeNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeDepe", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    'Private Function SaveRecord() As Boolean

    '    Dim RelationshipNo As Integer = Generic.ToInt(Me.cboRelationShipNo.SelectedValue)
    '    Dim EmployeeDepeNo As Integer = Generic.ToInt(Me.txtCode.Text)
    '    Dim CivilStatNo As Integer = Generic.ToInt(Me.cboCivilStatNo.SelectedValue)
    '    Dim IsDependent As Integer = Generic.ToInt(Me.rblIsDependent.SelectedValue)
    '    Dim IsBeneficiary As Integer = Generic.ToInt(Me.rblIsBeneficiary.SelectedValue)
    '    Dim IsInsurance As Integer = Generic.ToInt(Me.rblIsInsurance.SelectedValue)

    '    If SQLHelper.ExecuteNonQuery("EEmployeeDepe_WebSave", UserNo, EmployeeDepeNo, TransNo, txtLastName.Text, txtFirstName.Text, txtMiddleName.Text, RelationshipNo, _
    '                                  txtOccupation.Text, txtBirthDate.Text, txtPhoneNo.Text, IsInsurance, "", IsBeneficiary, txtHomeAddress.Text, txtEmployerName.Text, _
    '                                  txtEmployerAdd.Text, txtEmployerTelNo.Text, IsDependent, CivilStatNo) > 0 Then
    '        SaveRecord = True
    '    Else
    '        SaveRecord = False

    '    End If

    'End Function

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
    End Sub

End Class


