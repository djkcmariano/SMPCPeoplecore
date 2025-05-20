Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web


Partial Class Secured_CliClinicFindingList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Int64 = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        End If

        PopulateGrid()
        PopulateTabHeader()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinic_WebTabHeader", UserNo, TransNo, Generic.ToBol(Session("IsDependent")))
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next

        If Generic.ToBol(Session("IsDependent")) = False Then
            imgPhoto.Visible = True
            imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
        Else
            imgPhoto.Visible = False
        End If

    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EClinicHisFinding_Web", UserNo, TransNo, Generic.ToBol(Session("IsDependent")))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ClinicHisFindingNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EClinicHisFinding", UserNo, CType(item, Integer))
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

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Generic.ClearControls(Me, "Panel1")
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateData(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ClinicHisFindingNo"}))
        mdlMain.Show()
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub
    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Private Function SaveRecord() As Boolean

        Dim ClinicHisFindingNo As Integer = Generic.ToInt(txtClinicHisFindingCode.Text)
        Dim TestConductedNo As Integer = Generic.ToInt(cboTestConductedNo.SelectedValue)
        Dim TestDate As String = Generic.ToStr(txtTestDate.Text)
        Dim Findings As String = Generic.ToStr(txtFindings.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)
        Dim TestResultNo As Integer = Generic.ToInt(cboTestResultNo.SelectedValue)

        If SQLHelper.ExecuteNonQuery("EClinicHisFinding_WebSave", UserNo, ClinicHisFindingNo, TransNo, TestConductedNo, TestDate, Findings, Remarks, TestResultNo, Generic.ToBol(Session("IsDependent"))) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If

    End Function

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinicHisFinding_WebOne", UserNo, id)
        For Each row As DataRow In dt.Rows
            PopulateTestResult(Generic.ToInt(row("TestConductedNo")))
            Generic.PopulateData(Me, "Panel1", dt)
        Next
    End Sub

    Protected Sub TestConductedNo_ValueChanged(sender As Object, e As System.EventArgs)
        Try
            PopulateTestResult(Generic.ToInt(Me.cboTestConductedNo.SelectedValue))
            mdlMain.Show()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PopulateTestResult(tno As Integer)
        Try
            cboTestResultNo.DataSource = SQLHelper.ExecuteDataSet("ETestResult_WebLookup", UserNo, tno)
            cboTestResultNo.DataTextField = "tDesc"
            cboTestResultNo.DataValueField = "tNo"
            cboTestResultNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

End Class
