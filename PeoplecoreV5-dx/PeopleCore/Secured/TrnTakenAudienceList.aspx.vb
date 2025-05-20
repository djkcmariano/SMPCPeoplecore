Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_TrnTakenModuleList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim IsEnabled As Boolean

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("ETrnTakenAudience_Web", UserNo, TransNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnTakenAudience_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
                Generic.PopulateData(Me, "Panel1", dt)
            Next

            EnabledControls(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnTaken_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "TrnTakenList.aspx", "ETrnTaken")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateTabHeader()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "TrnTakenList.aspx", "ETrnTaken") Then
            Generic.ClearControls(Me, "Panel1")
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "TrnTakenList.aspx", "ETrnTaken") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Generic.ClearControls(Me, "Panel1")
            PopulateData(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TrnTakenAudienceNo"}))
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "TrnTakenList.aspx", "ETrnTaken") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TrnTakenAudienceNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ETrnTakenAudience", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Function SaveRecord() As Boolean

        Dim TrnTakenAudienceNo As Integer = Generic.ToInt(Me.txtTrnTakenAudienceNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim PositionNo As Integer = Generic.ToInt(Me.cboPositionNo.SelectedValue)
        Dim FacilityNo As Integer = Generic.ToInt(Me.cboFacilityNo.SelectedValue)
        Dim DivisionNo As Integer = Generic.ToInt(Me.cboDivisionNo.SelectedValue)
        Dim DepartmentNo As Integer = Generic.ToInt(Me.cboDepartmentNo.SelectedValue)
        Dim SectionNo As Integer = Generic.ToInt(Me.cboSectionNo.SelectedValue)
        Dim UnitNo As Integer = Generic.ToInt(Me.cboUnitNo.SelectedValue)
        Dim GroupNo As Integer = Generic.ToInt(Me.cboGroupNo.SelectedValue)
        Dim JobGradeNo As Integer = Generic.ToInt(Me.cboJobGradeNo.SelectedValue)
        Dim EmployeeStatNo As Integer = Generic.ToInt(Me.cboEmployeeStatNo.SelectedValue)
        Dim EmployeeClassNo As Integer = Generic.ToInt(Me.cboEmployeeClassNo.SelectedValue)
        Dim IsCurriculum As Boolean = Generic.ToBol(Me.txtIsCurriculum.Checked)

        If SQLHelper.ExecuteNonQuery("ETrnTakenAudience_WebSave", UserNo, TrnTakenAudienceNo, TransNo, EmployeeNo, PositionNo, FacilityNo, DivisionNo, DepartmentNo, SectionNo, UnitNo, GroupNo, JobGradeNo, EmployeeStatNo, EmployeeClassNo, IsCurriculum) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False

        End If

    End Function
    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub


    Private Sub EnabledControls(Optional IsEnabled As Boolean = False)

        If IsEnabled = True Then
            If Generic.ToBol(txtIsCurriculum.Checked) = True Then
                txtFullName.Enabled = False
                cboPositionNo.Enabled = False
                cboFacilityNo.Enabled = False
                cboDivisionNo.Enabled = False
                cboDepartmentNo.Enabled = False
                cboSectionNo.Enabled = False
                cboGroupNo.Enabled = False
                cboUnitNo.Enabled = False
                cboJobGradeNo.Enabled = False
                cboEmployeeStatNo.Enabled = False
                cboEmployeeClassNo.Enabled = False
            Else
                txtFullName.Enabled = True
                cboPositionNo.Enabled = True
                cboFacilityNo.Enabled = True
                cboDivisionNo.Enabled = True
                cboDepartmentNo.Enabled = True
                cboSectionNo.Enabled = True
                cboGroupNo.Enabled = True
                cboUnitNo.Enabled = True
                cboJobGradeNo.Enabled = True
                cboEmployeeStatNo.Enabled = True
                cboEmployeeClassNo.Enabled = True
            End If
        End If

    End Sub

End Class
