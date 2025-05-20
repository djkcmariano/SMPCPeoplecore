Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_OrgTableOrgActionType
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETableOrgActionType_Web", UserNo, PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETableOrgActionType_WebOne", UserNo, PayLocNo, Generic.ToInt(ViewState("TransNo")))
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub
    
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then            

        End If
        PopulateGrid()

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            chkIsNew.Checked = True
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            ViewState("TransNo") = lnk.CommandArgument
            PopulateData()
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If SQLHelper.ExecuteNonQuery("ETableOrgActionType_WebSave", UserNo, Generic.ToInt(txtCode.Text), txtTableOrgActionTypeCode.Text, txtTableOrgActionTypeDesc.Text, _
                                     txtRemarks.Text, chkIsNew.Checked, chkIsEdit.Checked, chkIsDelete.Checked, PayLocNo, chkIsEffectiveDate.Checked, chkIsVacated.Checked) > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
                Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TableOrgActionTypeNo"})
                Dim i As Integer = 0
                For Each item As Integer In fieldValues
                    Generic.DeleteRecordAudit("ETableOrgActionType", UserNo, item)
                    i = i + 1
                Next
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
            Else
                MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

End Class
