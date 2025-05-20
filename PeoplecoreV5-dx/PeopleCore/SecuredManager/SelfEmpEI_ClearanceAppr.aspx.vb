Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO

Imports System.Data.SqlClient

Partial Class SecuredManager_SelfEmpEI_ClearanceAppr
    'Inherits System.Web.UI.Page

    'Dim UserNo As Integer = 0
    'Dim PayLocNo As Integer = 0
    'Dim employeeno As Integer = 0

    'Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    '    UserNo = Generic.ToInt(Session("OnlineUserNo"))
    '    PayLocNo = Generic.ToInt(Session("xPayLocNo"))
    '    Permission.IsAuthenticated()
    '    If Not IsPostBack Then
    '    End If

    '    PopulateGrid()
    '    Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    'End Sub

    'Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")

    '    Dim dt As DataTable
    '    dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearance_WebManager", UserNo)
    '    grdMain.DataSource = dt
    '    grdMain.DataBind()

    'End Sub



    'Protected Sub PopulateData(id As Int64)
    '    Try
    '        Dim dt As DataTable
    '        dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearance_WebOne", UserNo, id)
    '        For Each row As DataRow In dt.Rows
    '            Generic.PopulateData(Me, "Panel1", dt)
    '            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, PayLocNo)
    '        Next
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub EnabledControls()
    '    Dim Enabled As Boolean = True
    '    Generic.EnableControls(Me, "Panel1", Enabled)
    '    lnkSave.Visible = Enabled
    'End Sub
    'Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

    '    If SaveRecord() Then
    '        PopulateGrid()
    '        MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '    Else
    '        MessageBox.Critical(MessageTemplate.ErrorSave, Me)
    '    End If

    'End Sub

    'Private Function SaveRecord() As Integer
    '    Dim tno As Integer = Generic.ToInt(Me.txtEmployeeEIClearanceNo.Text)
    '    Dim remarks As String = Generic.ToStr(txtRemarks.Text)

    '    If SQLHelper.ExecuteNonQuery("EEmployeeEIClearance_WebSave_Manager", UserNo, tno, remarks, Generic.ToStr(txtDateReturned.Text), Generic.ToBol(txtIsCleared.Checked)) > 0 Then
    '        SaveRecord = True
    '    Else
    '        SaveRecord = False
    '    End If

    'End Function
    'Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)

    '    Dim lnk As New LinkButton
    '    lnk = sender
    '    Generic.ClearControls(Me, "Panel1")
    '    Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
    '    Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
    '    PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeEIClearanceNo"})))
    '    ModalPopupExtender1.Show()

    'End Sub
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticated()

        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        PopulateGridDetl()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")

        Dim dt As DataTable

        If Generic.ToInt(cboTabNo.SelectedValue) = 2 Then
            lnkDeleteMain.Visible = False
            lnkPost.Visible = False
        ElseIf Generic.ToInt(cboTabNo.SelectedValue) = 3 Then
            lnkDeleteMain.Visible = False
            lnkPost.Visible = False
        Else
            lnkDeleteMain.Visible = True
            lnkPost.Visible = True
        End If

        dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearanceMain_WebManager", UserNo.ToString(), Generic.ToInt(cboTabNo.SelectedValue).ToString(), PayLocNo.ToString())
        grdMain.DataSource = dt
        grdMain.DataBind()

    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 51)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    'Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
    '    Dim lnk As New LinkButton
    '    lnk = sender
    '    Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
    '    Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo", "EmployeeCode"})
    '    ViewState("TransNo") = obj(0)
    '    ViewState("EmployeeCode") = obj(1)
    '    lblDetl.Text = ViewState("EmployeeCode").ToString
    '    PopulateGridDetl()

    'End Sub
    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        ViewState("TransNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeEIClearanceMainNo"}))
        lblDetl.Text = "Partner No. : " & Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeCode"}))
        PopulateGridDetl()
    End Sub

    Protected Sub lnkDeleteMain_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeEIClearanceMainNo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EEmployeeEIClearanceMain", UserNo, item)
            Generic.DeleteRecordAuditCol("EEmployeeEIClearance", UserNo, "EmployeeEIClearanceMainNo", item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
    End Sub

    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeEIClearanceMainNo"})
        Dim str As String = "", i As Integer = 0, dt As DataTable
        For Each item As Integer In fieldValues
            dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearanceMain_Post", UserNo, item)
            i = i + 1
        Next
        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

#Region "Detail"
    'Protected Sub PopulateGridDetl()
    '    Try
    '        If Generic.ToInt(ViewState("TransNo")) = 0 Then
    '            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"EmployeeNo", "EmployeeCode"})
    '            ViewState("TransNo") = Generic.ToInt(obj(0))
    '            ViewState("EmployeeCode") = Generic.ToStr(obj(1))

    '        End If
    '        Dim dt As DataTable
    '        dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearance_Web", UserNo, ViewState("TransNo"))
    '        grdDetl.DataSource = dt
    '        grdDetl.DataBind()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub PopulateGridDetl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearance_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()
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
        Generic.EnableControls(Me, "Panel1", Enabled)
        'lnkAdd.Visible = Enabled
        'lnkDelete.Visible = Enabled
        lnkSave.Visible = Enabled
    End Sub
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)

        Generic.ClearControls(Me, "Panel1")
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        ModalPopupExtender1.Show()


    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton
        lnk = sender
        Generic.ClearControls(Me, "Panel1")
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeEIClearanceNo"})))
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"EmployeeEIClearanceNo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EEmployeeEIClearance", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
    End Sub

    Private Function SaveRecord() As Integer
        Dim tno As Integer = Generic.ToInt(Me.txtEmployeeEIClearanceNo.Text)
        Dim EmployeeClearanceTypeNo As Integer = Generic.ToInt(Me.cboEmployeeEIClearanceTypeNo.SelectedValue)
        Dim EmployeeNo As Integer = Generic.ToInt(ViewState("TransNo"))
        Dim ImmediatesuperiorNo As Integer = Generic.ToInt(Generic.Split(hifImmediateSuperiorNo.Value, 0))
        Dim remarks As String = Generic.ToStr(txtRemarks.Text)
        Dim DateReturned As String = Generic.ToStr(txtDateReturned.Text)
        Dim IsCleared As Boolean = Generic.ToBol(txtIsCleared.Checked)
        Dim Amount As Double = Generic.ToDec(Me.txtAmount.Text)

        If SQLHelper.ExecuteNonQuery("EEmployeeEIClearance_WebSaveManager", UserNo, tno, EmployeeNo, EmployeeClearanceTypeNo, remarks, DateReturned, IsCleared, Generic.ToInt(ViewState("TransNo")), Amount) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If

    End Function
#End Region
End Class
