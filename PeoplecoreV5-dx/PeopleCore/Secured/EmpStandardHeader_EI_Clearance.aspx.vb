Imports clsLib
Imports System.Data
Imports DevExpress.Web
Partial Class Secured_EmpStandardHeader_EI_Clearance
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))

        If Not IsPostBack Then
            If TransNo = 0 Then
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            End If
            PopulateTabHeader()
            EnabledControls()
        End If

        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearance_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearance_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
                Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, PayLocNo)
            Next
        Catch ex As Exception

        End Try
    End Sub

    
    Private Sub EnabledControls()
        Dim Enabled As Boolean = True

        If txtIsPosted.Checked = True Then
            Enabled = False
        End If

        Generic.EnableControls(Me, "Panel1", Enabled)

        lnkAdd.Visible = Enabled
        lnkDelete.Visible = Enabled
        lnkSave.Visible = Enabled
    End Sub

    Private Sub PopulateTabHeader()
        Try
            'lbl.Text = "Transaction No. : " & Pad.PadZero(8, TransNo)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Generic.ClearControls(Me, "Panel1")
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeEIClearanceNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeEIClearanceNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeEIClearance", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Function SaveRecord() As Integer
        Dim tno As Integer = Generic.ToInt(Me.txtEmployeeEIClearanceNo.Text)
        Dim EmployeeClearanceTypeNo As Integer = Generic.ToInt(Me.cboEmployeeEIClearanceTypeNo.SelectedValue)
        Dim EmployeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))
        Dim ImmediatesuperiorNo As Integer = Generic.ToInt(Generic.Split(hifImmediateSuperiorNo.Value, 0))
        Dim remarks As String = Generic.ToStr(txtRemarks.Text)

        If SQLHelper.ExecuteNonQuery("EEmployeeEIClearance_WebSave", UserNo, tno, TransNo, remarks, EmployeeClearanceTypeNo, EmployeeNo, ImmediatesuperiorNo) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If

    End Function

End Class

