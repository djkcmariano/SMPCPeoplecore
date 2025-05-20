Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PayMainList_EditOrg
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0

    Protected Sub PopulateGrid()
        Try
            PayHeader.ID = Generic.ToInt(TransNo)
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayMain_Web_EditOrg", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        'AccessRights.CheckUser(UserNo)
        Permission.IsAuthenticatedCoreUser()
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", Generic.ToInt(Session("xPayLocNo")))
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
    End Sub

    Protected Sub PopulateData(fid As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayMain_Web_EditOrgOne", UserNo, fid)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupMain", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub
    
    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayMainNo", "IsEnabled"})
            Dim iNo As Integer = Generic.ToInt(obj(0))
            Dim IsEnabled As Boolean = Generic.ToBol(obj(1))
            PopulateData(iNo)
            'btnSave.Enabled = IsEnabled
            
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayMainNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayMain", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim paymainNo As Integer = Generic.ToInt(Me.txtPayMainNo.Text)
        Dim CostcenterNo As Integer = Generic.ToInt(cboCostCenterNo.SelectedValue)
        Dim sectionNo As Integer = Generic.ToInt(Me.cboSectionNo.SelectedValue)
        Dim departmentNo As Integer = Generic.ToInt(Me.cboDepartmentNo.SelectedValue)
        Dim dt As DataTable, error_message As String = "", error_num As Integer = 0

        dt = SQLHelper.ExecuteDataTable("EPayMain_Web_EditOrgSave", UserNo, Generic.ToInt(txtPayMainNo.Text), CostcenterNo, sectionNo, departmentNo)
        For Each row As DataRow In dt.Rows
            Retval = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                Retval = False
            End If

        Next
        If Retval = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If Retval = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub
    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

End Class





