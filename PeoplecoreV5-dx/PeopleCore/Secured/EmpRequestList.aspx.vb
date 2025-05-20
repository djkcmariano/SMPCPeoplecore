Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_EmpRequestList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 24)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PopulateGrid()
        Dim dt As DataTable
        lnkServe.Visible = True
        lnkDelete.Visible = True
        If cboTabNo.SelectedValue = 1 Then
            lnkServe.Visible = False
            lnkDelete.Visible = False
        End If
        dt = SQLHelper.ExecuteDataTable("EEmployeeRequest_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        grdMain.DataSource = dt
        grdMain.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
    End Sub

    Protected Sub lnkServe_Click(sender As Object, e As EventArgs)        
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeRequestNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                i = i + Generic.ToInt(SQLHelper.ExecuteNonQuery("EEmployeeRequest_WebServed", UserNo, item))
            Next
            If i > 0 Then
                MessageBox.Success("There are " & i.ToString() & " record(s) has been served.", Me)
            Else
                MessageBox.Warning("No record selected.", Me)
            End If
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.EnableControls(Me, "Panel1", True)
            lnkSave.Enabled = True
            Generic.ClearControls(Me, "Panel1")
            Me.txtDateRequested.Text = Now.ToShortDateString
            txtDateServed.Enabled = False
            chkIsServed.Enabled = False
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeRequestNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeRequest", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                'MessageBox.Success("There are " & i.ToString() & " selected record/s has been served.", Me)
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
            Else
                MessageBox.Warning("No record selected.", Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim dt As DataTable
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj() As Object = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeRequestNo", "IsEnabled"})
            dt = SQLHelper.ExecuteDataTable("EEmployeeRequest_WebOne", UserNo, Generic.ToInt(obj(0)))
            Dim IsEnabled As Boolean = Generic.ToBol(obj(1))

            Generic.PopulateData(Me, "Panel1", dt)
            Generic.EnableControls(Me, "Panel1", IsEnabled)
            lnkSave.Enabled = IsEnabled
            txtFullName.Enabled = False
            cboRequestTypeNo.Enabled = False
            txtDateRequested.Enabled = False
            txtMessage.Enabled = False
            txtDateServed.Enabled = False
            chkIsServed.Enabled = False
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)        
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecord() As Boolean

        If SQLHelper.ExecuteNonQuery("EEmployeeRequest_WebSave", UserNo, Generic.ToInt(txtEmployeeRequestNo.Text), Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0)), _
                                     Generic.ToInt(cboRequestTypeNo.SelectedValue), txtDateRequested.Text, txtMessage.Text, chkIsServed.Checked, _
                                     Me.txtDateServed.Text, Me.txtRemarks.Text, PayLocNo) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then                        
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value            
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

End Class





