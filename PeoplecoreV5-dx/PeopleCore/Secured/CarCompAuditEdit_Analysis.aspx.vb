Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_CarJDEditComp
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeComp_WebTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
    End Sub

    'Protected Sub PopulateGrid()
    '    Try
    '        Dim dt As DataTable
    '        dt = SQLHelper.ExecuteDataTable("EEmployeeComp_Web", UserNo, TransNo, Generic.ToInt(cboCompPositionNo.SelectedValue), PayLocNo)
    '        grdMain.DataSource = dt
    '        grdMain.DataBind()
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub PopulateData(Optional tno As Integer = 0, Optional IsLoadData As Boolean = False)
        Dim ds As New DataSet
        Dim dt As DataTable
        ds = SQLHelper.ExecuteDataSet("EEmployeeComp_WebInfo", UserNo, TransNo, Generic.ToInt(cboCompPositionNo.SelectedValue), PayLocNo)
        dt = ds.Tables(0).DefaultView.ToTable(True, "CompTypeDesc", "CompDesc", "RequiredProficiency", "AcquiredProficiency", "VarianceProficiency", "CompInterventionTypeNo", "CompInterventionTypeDesc")

        If Not dt Is Nothing Then
            If Generic.ToInt(dt.Rows.Count) > 0 Then
                pvtGrid.DataSource = dt
            Else
                MessageBox.Warning("No data available.", Me)
            End If
        Else
            MessageBox.Critical("Error in datasource.", Me)
            Exit Sub
        End If

        'Refresh Pivot
        pvtGrid.DataBind()

    End Sub

    Private Sub PopulatePivotGrid(Optional tno As Integer = 0, Optional IsLoadData As Boolean = False)

        Dim ds As New DataSet
        Dim dt As DataTable
        ds = SQLHelper.ExecuteDataSet("EEmployeeComp_WebInfo", UserNo, TransNo, Generic.ToInt(cboCompPositionNo.SelectedValue), PayLocNo)
        dt = ds.Tables(0).DefaultView.ToTable(True, "CompTypeDesc", "CompDesc", "RequiredProficiency", "AcquiredProficiency", "VarianceProficiency", "CompInterventionTypeNo", "CompInterventionTypeDesc")

        If Not dt Is Nothing Then
            If Generic.ToInt(dt.Rows.Count) > 0 Then
                pvtGrid.DataSource = dt
            Else
                MessageBox.Warning("No data available.", Me)
            End If
        Else
            MessageBox.Critical("Error in datasource.", Me)
            Exit Sub
        End If

        'Clear Pivot
        Dim ci As Integer = pvtGrid.Fields.Count
        Dim cx As Integer
        For cx = 0 To ci - 1
            pvtGrid.Fields.RemoveAt(0)
        Next

        'Refresh Pivot
        pvtGrid.DataSource = dt
        pvtGrid.RetrieveFields()

        'Check Data Type of Column
        ci = dt.Columns.Count
        For cx = 0 To ci - 1

            'pvtGrid.Fields(cx).Visible = False

            If dt.Columns(cx).ColumnName = "CompTypeDesc" Then
                pvtGrid.Fields(cx).Caption = "Competency Type"
                pvtGrid.Fields(cx).Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
                pvtGrid.Fields(cx).Options.ShowTotals = False
            End If

            If dt.Columns(cx).ColumnName = "CompDesc" Then
                pvtGrid.Fields(cx).Caption = "Competency"
                pvtGrid.Fields(cx).Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            End If

            If dt.Columns(cx).ColumnName = "RequiredProficiency" Then
                pvtGrid.Fields(cx).Caption = "Required"
                pvtGrid.Fields(cx).Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            End If

            If dt.Columns(cx).ColumnName = "AcquiredProficiency" Then
                pvtGrid.Fields(cx).Caption = "Acquired"
                pvtGrid.Fields(cx).Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            End If

            If dt.Columns(cx).ColumnName = "VarianceProficiency" Then
                pvtGrid.Fields(cx).Caption = "Variance"
                pvtGrid.Fields(cx).Area = DevExpress.XtraPivotGrid.PivotArea.RowArea
            End If

            If dt.Columns(cx).ColumnName = "CompInterventionTypeDesc" Then
                pvtGrid.Fields(cx).Caption = "Intervention Type"
                pvtGrid.Fields(cx).Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
            End If

            If dt.Columns(cx).ColumnName = "CompInterventionTypeNo" Then
                pvtGrid.Fields(cx).Caption = "Count"
                pvtGrid.Fields(cx).Area = DevExpress.XtraPivotGrid.PivotArea.DataArea
                pvtGrid.Fields(cx).SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Count
            End If

            'If dt.Columns(cx).ColumnName = "Name" Then
            '    pvtGrid.Fields.Remove(pvtGrid.Fields(cx))
            'End If
        Next


        pvtGrid.DataBind()
        pvtGrid.ReloadData()

    End Sub

    Protected Sub pvtGrid_DataBound(ByVal sender As Object, ByVal e As EventArgs)
        If Not IsPostBack Then
            pvtGrid.ExpandAllRows()
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "CarCompAuditList.aspx", "ECompEmployee")
        If Not IsPostBack Then
            PopulateTabHeader()
            PopulateDropDown()
            populatePosition(Generic.ToInt(Me.cboCompFilterByNo.SelectedValue))
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulatePivotGrid()
        End If

        PopulateData()

        If Not IsPostBack Then
            PopulatePivotSet()
        End If

        pvtGrid.OptionsCustomization.AllowExpand = False
        'PopulateGrid()
        'PopulateGroupBy()

    End Sub

    Private Sub PopulatePivotSet()
        'pvtGrid.RetrieveFields(DevExpress.XtraPivotGrid.PivotArea.FilterArea, False)
        'pvtGrid.OptionsLayout.Columns.AddNewColumns = False
        'pvtGrid.OptionsLayout.Columns.RemoveOldColumns = False
        'pvtGrid.LoadLayoutFromString(PivotSettings)
        pvtGrid.OptionsCustomization.CustomizationFormStyle = DevExpress.XtraPivotGrid.Customization.CustomizationFormStyle.Excel2007
        pvtGrid.OptionsPager.RowsPerPage = 10000
        pvtGrid.OptionsView.ShowColumnGrandTotals = True
        pvtGrid.OptionsView.ShowColumnTotals = True
        pvtGrid.OptionsView.ShowRowGrandTotals = True
        pvtGrid.OptionsView.ShowRowTotals = True
        'pvtGrid.OptionsView.ShowGrandTotalsForSingleValues = True
        'pvtGrid.OptionsView.ShowTotalsForSingleValues = True
        'PopulateChartSize()

    End Sub

    Private Sub PopulateDropDown()
        Try
            cboCompFilterByNo.DataSource = SQLHelper.ExecuteDataSet("ECompFilterBy_WebLookup", UserNo)
            cboCompFilterByNo.DataValueField = "tNo"
            cboCompFilterByNo.DataTextField = "tDesc"
            cboCompFilterByNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboCompFilterByNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            populatePosition(Generic.ToInt(Me.cboCompFilterByNo.SelectedValue))
            'PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub populatePosition(CompFilterByNo As Integer)
        Try
            cboCompPositionNo.DataSource = SQLHelper.ExecuteDataSet("ECompPosition_WebLookup", UserNo, TransNo, CompFilterByNo, PayLocNo)
            cboCompPositionNo.DataTextField = "tDesc"
            cboCompPositionNo.DataValueField = "tNo"
            cboCompPositionNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub cboCompPositionNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            'PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "CarCompAuditList.aspx", "ECompEmployee") Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim CompNo As Integer = Generic.ToInt(Me.cboCompNo.SelectedValue)
        Dim Anchor As String = Generic.ToStr(txtAcquiredAnchor.Text)
        Dim CompScaleNo As Integer = Generic.ToInt(cboAcquiredScaleNo.SelectedValue)
        Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)
        Dim CompTypeNo As Integer = Generic.ToInt(cboCompTypeNo.SelectedValue)

        If SQLHelper.ExecuteNonQuery("EEmployeeComp_WebSave", UserNo, 0, TransNo, CompNo, CompScaleNo, CompTypeNo, Remarks, Anchor) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            'PopulateGrid()
            'PopulateGroupBy()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "CarCompAuditList.aspx", "ECompEmployee") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim CompNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"CompNo"}))
            Try
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EEmployeeComp_WebOne", UserNo, TransNo, Generic.ToInt(cboCompPositionNo.SelectedValue), CompNo, PayLocNo)
                For Each row As DataRow In dt.Rows
                    Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))
                    Try
                        Me.cboCompNo.DataSource = SQLHelper.ExecuteDataSet("ECompetency_WebLookup_UnionAll", UserNo, Generic.ToInt(row("CompTypeNo")), Generic.ToInt(row("CompNo")), PayLocNo)
                        Me.cboCompNo.DataTextField = "tdesc"
                        Me.cboCompNo.DataValueField = "tno"
                        Me.cboCompNo.DataBind()
                    Catch ex As Exception
                    End Try
                    Generic.PopulateData(Me, "Panel1", dt)
                Next
            Catch ex As Exception

            End Try

            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    'Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "CarCompAuditList.aspx", "ECompEmployee") Then
    '        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"CompNo"})
    '        Dim str As String = "", i As Integer = 0
    '        For Each item As Integer In fieldValues
    '            'Generic.DeleteRecordAudit("EJDComp", UserNo, item)
    '            'SQLHelper.ExecuteScalar("DELETE EEmployeeComp WHERE EmployeeNo=" & TransNo & " and CompNo=" & Generic.ToInt(item))
    '            SQLHelper.ExecuteNonQuery("EEmployeeComp_WebDelete", UserNo, TransNo, item, PayLocNo)
    '            i = i + 1
    '        Next
    '        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
    '        'PopulateGrid()
    '    Else
    '        MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
    '    End If
    'End Sub

    'Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
    '    Try
    '        'grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
    '    Catch ex As Exception
    '        MessageBox.Warning("Error exporting to excel file.", Me)
    '    End Try

    'End Sub

    Protected Sub lnkExport_Click(sender As Object, e As System.EventArgs) Handles lnkExport.Click
        Try
            Dim filename As String = "temp"
            ASPxPivotGridExporter1.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.[False]
            ASPxPivotGridExporter1.OptionsPrint.PrintColumnHeaders = DevExpress.Utils.DefaultBoolean.[False]
            ASPxPivotGridExporter1.OptionsPrint.PrintRowHeaders = DevExpress.Utils.DefaultBoolean.[False]
            ASPxPivotGridExporter1.OptionsPrint.PrintDataHeaders = DevExpress.Utils.DefaultBoolean.[False]
            Dim options As New XlsExportOptionsEx
            With options
                .ExportType = DevExpress.Export.ExportType.WYSIWYG
                .AllowGrouping = DevExpress.Utils.DefaultBoolean.True
                .TextExportMode = TextExportMode.Text
                .AllowFixedColumns = DevExpress.Utils.DefaultBoolean.True
                .AllowFixedColumns = DevExpress.Utils.DefaultBoolean.True
                .RawDataMode = True
                .SheetName = filename
                .ShowPageTitle = DevExpress.Utils.DefaultBoolean.True
            End With
            ASPxPivotGridExporter1.ExportXlsxToResponse(filename, True)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub cboCompTypeNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateCompetency()
        txtAcquiredAnchor.Text = GetIndicator()
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub cboCompScaleNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtAcquiredAnchor.Text = GetIndicator()
        ModalPopupExtender1.Show()
    End Sub

    Private Sub PopulateCompetency()
        Try
            Me.cboCompNo.DataSource = SQLHelper.ExecuteDataSet("ECompetency_WebLookup_UnionAll", UserNo, Generic.ToInt(Me.cboCompTypeNo.SelectedValue), 0, PayLocNo)
            Me.cboCompNo.DataTextField = "tdesc"
            Me.cboCompNo.DataValueField = "tno"
            Me.cboCompNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetIndicator() As String
        Return Generic.ToStr(SQLHelper.ExecuteScalar("select top 1 Anchor from dbo.ECompDeti where CompScaleNo=" & Generic.ToInt(cboAcquiredScaleNo.SelectedValue) & " and CompNo=" & Generic.ToInt(cboCompNo.SelectedValue)))
    End Function

    'Protected Sub grdMain_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
    '    PopulateGroupBy()
    'End Sub

    'Private Sub PopulateGroupBy()
    '    grdMain.BeginUpdate()
    '    Try
    '        grdMain.ClearSort()
    '        grdMain.GroupBy(grdMain.Columns("CompTypeDesc"))
    '    Finally
    '        grdMain.EndUpdate()
    '    End Try
    '    grdMain.ExpandAll()
    'End Sub

    'Protected Sub grdMain_CustomColumnDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewColumnDisplayTextEventArgs)
    '    If e.Column.FieldName = "OrderBy" Then
    '        Dim groupLevel As Integer = grdMain.GetRowLevel(e.VisibleRowIndex)
    '        If groupLevel = e.Column.GroupIndex Then
    '            Dim city As String = grdMain.GetRowValues(e.VisibleRowIndex, "OrderBy").ToString()
    '            Dim country As String = grdMain.GetRowValues(e.VisibleRowIndex, "CompTypeDesc").ToString()
    '            e.DisplayText = city & " (" & country & ")"
    '        End If
    '    End If

    'End Sub

    'Protected Sub grdMain_CustomColumnSort(ByVal sender As Object, ByVal e As CustomColumnSortEventArgs)
    '    If e.Column IsNot Nothing And e.Column.FieldName = "CompTypeDesc" Then
    '        Dim country1 As Object = e.GetRow1Value("OrderBy")
    '        Dim country2 As Object = e.GetRow2Value("OrderBy")
    '        Dim res As Integer = Comparer.Default.Compare(country1, country2)
    '        If res = 0 Then
    '            Dim city1 As Object = e.Value1
    '            Dim city2 As Object = e.Value2
    '            res = Comparer.Default.Compare(city1, city2)
    '        End If
    '        e.Result = res
    '        e.Handled = True
    '    End If
    'End Sub

    'Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim lnk As New LinkButton, i As Integer
    '    lnk = sender
    '    Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
    '    i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"CompNo"}))

    '    Response.Redirect("~/secured/CarCompAuditEdit_Analysis_Intervention.aspx?CompNo=" & i & "&id=" & TransNo)

    'End Sub

End Class
