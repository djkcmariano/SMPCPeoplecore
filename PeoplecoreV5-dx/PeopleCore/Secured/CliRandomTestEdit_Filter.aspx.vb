Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting

Partial Class Secured_CliRandomTestEdit_Filter
    Inherits System.Web.UI.Page


    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer

    Private Sub PopulateGrid()

        If txtIsPosted.Checked = True Then
            lnkAdd.Visible = False
            lnkDelete.Visible = False
        Else
            lnkAdd.Visible = True
            lnkDelete.Visible = True
        End If

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EClinicRandomTestFilter_Web", UserNo, TransNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub


    'Populate Combo box
    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EClinicRandomTest_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "CliRandomTestList.aspx", "EClinicRandomTest")
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
            PopulateTabHeader()
        End If
        PopulateGrid()

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinicRandomTestFilter_WebOne", UserNo, id)
        'Generic.PopulateData(Me, "Panel2", dt) 
        Generic.PopulateData(Me, "pnlEntryPopup", dt)
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "CliRandomTestList.aspx", "EClinicRandomTest") Then
            Dim lnk As New LinkButton, IsEnabled As Boolean = False
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Generic.ClearControls(Me, "Panel2")
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ClinicRandomTestFilterNo"})))

            'Enable or Disable Controls
            If txtIsPosted.Checked = True Then
                IsEnabled = False
            Else
                IsEnabled = True
            End If
            Generic.EnableControls(Me, "Panel2", IsEnabled)
            btnSave.Enabled = IsEnabled

            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub



    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "CliRandomTestList.aspx", "EClinicRandomTest") Then

            Generic.ClearControls(Me, "Panel2")
            mdlShow.Show()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If
    End Sub


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "CliRandomTestList.aspx", "EClinicRandomTest") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ClinicRandomTestFilterNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EClinicRandomTestFilter", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    'Submit record
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim ClinicRandomTestFilterNo As Integer = Generic.ToInt(Me.txtClinicRandomTestFilterNo.Text)
            Dim FacilityNo As Integer = Generic.ToInt(Me.cboFacilityNo.SelectedValue)
            Dim DivisionNo As Integer = Generic.ToInt(Me.cboDivisionNo.SelectedValue)
            Dim DepartmentNo As Integer = Generic.ToInt(Me.cboDepartmentNo.SelectedValue)
            Dim SectionNo As Integer = Generic.ToInt(Me.cboSectionNo.SelectedValue)
            Dim UnitNo As Integer = Generic.ToInt(Me.cboUnitNo.SelectedValue)
            Dim PositionNo As Integer = Generic.ToInt(Me.cboPositionNo.SelectedValue)
            Dim LocationNo As Integer = Generic.ToInt(Me.cboLocationNo.SelectedValue)
            Dim JobGradeNo As Integer = Generic.ToInt(Me.cboJobGradeNo.SelectedValue)
            Dim EmployeeStatNo As Integer = Generic.ToInt(Me.cboEmployeeStatNo.SelectedValue)
            Dim EmployeeClassNo As Integer = Generic.ToInt(Me.cboEmployeeClassNo.SelectedValue)
            Dim PayClassNo As Integer = Generic.ToInt(Me.cboPayClassNo.SelectedValue)

            '//validate start here
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EClinicRandomTestFilter_WebValidate", UserNo, ClinicRandomTestFilterNo, TransNo, FacilityNo, DivisionNo, DepartmentNo, SectionNo, UnitNo, PositionNo, LocationNo, JobGradeNo, EmployeeStatNo, EmployeeClassNo, PayClassNo, PayLocNo)

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("tProceed"))
                messagedialog = Generic.ToStr(rowx("xMessage"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                mdlShow.Show()
                Exit Sub
            End If

            If SQLHelper.ExecuteNonQuery("EClinicRandomTestFilter_WebSave", UserNo, ClinicRandomTestFilterNo, TransNo, FacilityNo, DivisionNo, DepartmentNo, SectionNo, UnitNo, PositionNo, LocationNo, JobGradeNo, EmployeeStatNo, EmployeeClassNo, PayClassNo, PayLocNo) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

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






