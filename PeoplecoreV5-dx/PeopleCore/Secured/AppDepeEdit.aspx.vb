Imports clsLib
Imports System.Data

Partial Class Secured_AppDepeEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub PopulateGrid()       
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EApplicantDepe_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EApplicantDepe_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows                
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
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
        Dim RetVal As Boolean = False
        Dim RelationshipNo As Integer = Generic.ToInt(Me.cboRelationShipNo.SelectedValue)
        Dim ApplicantDepeNo As Integer = Generic.ToInt(Me.txtApplicantDepeNo.Text)
        Dim CivilStatNo As Integer = Generic.ToInt(Me.cboCivilStatNo.SelectedValue)
        Dim IsDependent As Boolean = Generic.ToBol(chkIsDependent.Checked) 'Generic.ToInt(Me.rblIsDependent.SelectedValue)
        Dim IsBeneficiary As Boolean = Generic.ToBol(chkIsBeneficiary.Checked) ' Generic.ToInt(Me.rblIsBeneficiary.SelectedValue)
        Dim IsInsurance As Boolean = Generic.ToBol(chkIsInsurance.Checked) 'Generic.ToInt(Me.rblIsInsurance.SelectedValue)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EApplicantDepe_WebValidate", UserNo, ApplicantDepeNo, TransNo, RelationshipNo, txtLastName.Text, txtFirstName.Text, txtBirthDate.Text, CivilStatNo, txtHomeAddress.Text, txtPhoneNo.Text, _
                                         txtOccupation.Text, txtEmployerName.Text, IsDependent, IsBeneficiary, IsInsurance, txtMiddleName.Text)

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

        'If SQLHelper.ExecuteNonQuery("EApplicantDepe_WebSave", UserNo, ApplicantDepeNo, TransNo, RelationshipNo, txtLastName.Text, txtFirstName.Text, txtBirthDate.Text, CivilStatNo, txtHomeAddress.Text, txtPhoneNo.Text, _
        '                            txtOccupation.Text, txtEmployerName.Text, IsDependent, IsBeneficiary, IsInsurance, txtMiddleName.Text) > 0 Then
        '    RetVal = True
        'Else
        '    RetVal = False
        'End If
        If SQLHelper.ExecuteNonQuery("EApplicantDepe_WebSave", UserNo, _
                                   ApplicantDepeNo,
                                   TransNo,
                                   RelationshipNo,
                                   txtLastName.Text,
                                   txtFirstName.Text,
                                   txtBirthDate.Text,
                                   CivilStatNo,
                                   txtHomeAddress.Text,
                                   txtPhoneNo.Text,
                                   txtOccupation.Text,
                                   txtEmployerName.Text,
                                   IsDependent,
                                   IsBeneficiary,
                                   IsInsurance,
                                   txtMiddleName.Text.ToString,
                                   txtRemark.Text.ToString, Generic.ToInt(cboEmployeeExtNo.SelectedValue),
                                   txtMaidenName.Text.ToString,
                                   txtEmployerAdd.Text.ToString) > 0 Then

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
            Generic.ClearControls(Me, "Panel1")
            PopulateData(Generic.ToInt(lnk.CommandArgument))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ApplicantDepeNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EApplicantDepe", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicantTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=1"

    End Sub

End Class
