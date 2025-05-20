Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_EmpProject
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EEmployeeProject_Web", UserNo, "", PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateDropDown()
        
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("onlineuserno"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        AccessRights.CheckUser(UserNo)

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        If Not IsPostBack Then
            PopulateDropDown()
        End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeProjectNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeProject", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid(True)
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub



    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton ', i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeProjectNo", "ProjectNo"})
                Dim iNo As Integer = Generic.ToInt(obj(0))
                Dim IsEnabled As Boolean = Generic.ToBol(obj(1))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EEmployeeProject_WebOne", UserNo, Generic.ToInt(iNo))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
                    Generic.PopulateData(Me, "pnlPopup", dt)
                Next
                btnSave.Enabled = IsEnabled
                mdlShow.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub



    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopup")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
            
            btnSave.Enabled = True
            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

 

    'Submit record
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ds As New DataSet
        Dim RetVal As Integer = 0, xMessage As String = "", alertType As String = ""

        'ds = SQLHelper.ExecuteDataSet("ETableReferrence_WebValidate", UserNo, Session("xTableName"), Generic.ToInt(txtEmployeeProjectNo.Text), Generic.ToStr(txtEmployeeProjectCode.Text), Generic.ToStr(txtEmployeeProjectDesc.Text), PayLocNo)
        'If ds.Tables.Count > 0 Then
        '    If ds.Tables(0).Rows.Count > 0 Then
        '        RetVal = Generic.ToInt(ds.Tables(0).Rows(0)("RetVal"))
        '        xMessage = Generic.ToStr(ds.Tables(0).Rows(0)("xMessage"))
        '        alertType = Generic.ToStr(ds.Tables(0).Rows(0)("alertType"))
        '    End If
        'End If

        'If RetVal = 1 Then
        '    MessageBox.Alert(xMessage, alertType, Me)
        '    mdlShow.Show()
        '    Exit Sub
        'End If

        SaveRecord()

    End Sub


    Private Sub SaveRecord()
        Dim tNo As Integer = Generic.ToInt(txtEmployeeProjectNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim PayclassNo As Integer = Generic.ToInt(cboPayclassNo.SelectedValue)
        Dim projectNO As Integer = Generic.ToInt(cboProjectNo.SelectedValue)
        Dim POsitionNo As Integer = Generic.ToInt(cboPositionNo.SelectedValue)
        Dim departmentNo As Integer = Generic.ToInt(cboDepartmentNo.SelectedValue)
        Dim effectivity As String = Generic.ToStr(txtEffectivity.Text)

        Dim employeeRate As Double = Generic.ToDec(txtEmployeeRate.Text)
        Dim employeeRated As Double = Generic.ToDec(txtEmployeeRateD.Text)
        Dim employeeRateh As Double = Generic.ToDec(txtEmployeeRateH.Text)

        Dim billingRate As Double = Generic.ToDec(txtBillingRate.Text)
        Dim billingRated As Double = Generic.ToDec(txtBillingRateD.Text)
        Dim billingRateh As Double = Generic.ToDec(txtBillingRateH.Text)

        Dim otRate As Double = Generic.ToDec(txtOTRate.Text)
        Dim otRated As Double = Generic.ToDec(txtOTRateD.Text)
        Dim otRateh As Double = Generic.ToDec(txtOTRateH.Text)

        Dim dt As New DataTable, error_num As Integer = 0, error_message As String = "", retVal As Boolean = False
        dt = SQLHelper.ExecuteDataTable("EEmployeeProject_WebSave", UserNo, tNo, EmployeeNo, projectNO, departmentNo, POsitionNo, effectivity, PayclassNo, PayLocNo, employeeRate, employeeRated, employeeRateh, billingRate, billingRated, billingRateh, otRate, otRated, otRateh)
        For Each row As DataRow In dt.Rows
            RetVal = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                retVal = False
            End If

        Next
        If RetVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub
  
    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid(True)
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        'If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
        '    Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
        '    e.Enabled = value
        'End If
    End Sub

End Class

