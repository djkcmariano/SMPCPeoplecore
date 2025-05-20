Imports clsLib
Imports System.Data
Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.XtraPrinting
Imports DevExpress.XtraCharts.Native
Imports DevExpress.XtraPrintingLinks
Imports System.IO
Imports DevExpress.XtraCharts
Imports DevExpress.Data.PivotGrid
Imports AjaxControlToolkit

Partial Class Secured_SecCMSTemplateContent
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer
    Dim Datasource As String
    Dim PivotSettings As String

    Dim clsGeneric As New clsGenericClass
    Dim _ds As DataSet
    Dim xParam As Object()


    Dim ReportParamTypeNo As Integer
    Dim TableViewName As String, ReportField As String, ReportLabel As String
    Dim ReportParamNo As Integer = 0
    Dim ReportParamFieldTypeno As Integer = 0
    Dim drpFilterbyNo As New DropDownList
    Dim category As String = ""
    Dim xCategory As String = ""

    Dim drpFilterbyID As New DropDownList

    Dim ReportNo As Integer
    Dim tblMain As New Table

    Dim tno As Integer
    Dim tStr As String
    Dim ReportFieldWidth As Integer
    Dim xIsHidden As Boolean = 0

    Dim drpAC As New AutoCompleteExtender
    Dim txtName As New TextBox
    Dim RequiredField As Boolean = False


    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EUserReport_WebOne", UserNo, TransNo, PayLocNo)
        For Each row As DataRow In dt.Rows
            txtUserReportTitle.Text = Generic.ToStr(row("UserReportTitle"))
            cboReportNo.Text = Generic.ToInt(row("ReportNo"))
            PivotSettings = Generic.ToStr(row("PivotSetting"))
            cboChartTypeNo.Text = Generic.ToInt(row("GraphTypeNo"))
            cboPageSizeNo.Text = Generic.ToInt(row("PageSizeNo"))
            txtShowColumnTotals.Checked = Generic.ToBol(row("IsColumnTotals"))
            txtShowColumnGrandTotals.Checked = Generic.ToBol(row("IsColumnGrandTotals"))
            txtShowRowTotals.Checked = Generic.ToBol(row("IsRowTotals"))
            txtShowRowGrandTotals.Checked = Generic.ToBol(row("IsRowGrandTotals"))
            txtWidth.Text = Generic.ToInt(row("Width"))
            txtHeight.Text = Generic.ToInt(row("Height"))
        Next
    End Sub

    Private Sub PopulateCombo()
        'Datasource Dropdown
        Try
            Dim cbods As New DataSet
            cbods = SQLHelper.ExecuteDataSet("EUserLevelPermissionReport_WebLookup_OLAP", UserNo, Session("xMenuType"), Session("xPayLocNo"))
            cboReportNo.DataSource = cbods
            cboReportNo.DataValueField = "ReportNo"
            cboReportNo.DataTextField = "ReportTitle"
            cboReportNo.DataBind()
        Catch ex As Exception
        End Try

        'Chart Dropdown
        Dim ChartTypes() As String = System.Enum.GetNames(GetType(ViewType))
        Dim i As Integer = 0
        For Each type As String In ChartTypes
            Dim item As New ListItem
            item.Value = i
            item.Text = type
            cboChartTypeNo.Items.Add(item)
            i = i + 1
        Next

        'Value Dropdown
        For Each type As PivotSummaryType In System.Enum.GetValues(GetType(PivotSummaryType))
            Dim FieldName As String = type.ToString()
            Select Case FieldName
                Case "Sum", "Average", "Count", "Max", "Min"
                    ddlSummaryType.Items.Add(New ListItem(FieldName))
            End Select
        Next type

        'Field Dropdown
        Try

            If Generic.ToInt(cboReportNo.SelectedValue) > 1 Then
                Dim IsSP As Boolean = False
                Dim dt2 As DataTable
                dt2 = SQLHelper.ExecuteDataTable("EReport_WebOne", UserNo, Generic.ToInt(cboReportNo.SelectedValue))
                For Each row As DataRow In dt2.Rows
                    Datasource = Generic.ToStr(row("Datasource"))
                    IsSP = Generic.ToBol(row("IsSP"))
                Next

                Dim dt As DataTable
                If IsSP = True Then
                    dt = SQLHelper.ExecuteDataTable(Datasource, UserNo, PayLocNo)
                Else
                    dt = SQLHelper.ExecuteDataTable("EFilteredValuePivot_WebGenerateView", UserNo, Datasource, PayLocNo)
                End If

                ddlField.Items.Clear()
                cboFieldNo.Items.Clear()
                ddlField.Items.Add("-- Select --")
                cboFieldNo.Items.Add("-- Select --")

                If Not dt Is Nothing Then
                    Dim y As Integer = 0
                    For Each column As DataColumn In dt.Columns
                        Dim item As New ListItem
                        item.Value = y
                        item.Text = column.ColumnName
                        ddlField.Items.Add(item)
                        cboFieldNo.Items.Add(item)
                        y = y + 1
                    Next
                End If

            End If

        Catch ex As Exception

        End Try


    End Sub

    Private Sub PopulatePivotGrid(Optional tno As Integer = 0, Optional IsLoadData As Boolean = False)

        If Generic.ToInt(cboReportNo.SelectedValue) > 1 Then
            Dim IsSP As Boolean = False
            Dim dt2 As DataTable
            dt2 = SQLHelper.ExecuteDataTable("EReport_WebOne", UserNo, tno)
            For Each row As DataRow In dt2.Rows
                Datasource = Generic.ToStr(row("Datasource"))
                IsSP = Generic.ToBol(row("IsSP"))
            Next

            Dim Param As Object() = PopulateServerReport()

            Dim dt As DataTable
            If IsSP = True Then
                dt = SQLHelper.ExecuteDataTable(Datasource, Param)
            Else
                dt = SQLHelper.ExecuteDataTable("EFilteredValuePivot_WebGenerateView", UserNo, Datasource, PayLocNo)
            End If

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

            If IsLoadData Then

                'Field Dropdown
                ddlField.Items.Clear()
                cboFieldNo.Items.Clear()
                ddlField.Items.Add("-- Select --")
                cboFieldNo.Items.Add("-- Select --")
                Dim i As Integer = 0
                For Each column As DataColumn In dt.Columns
                    Dim item As New ListItem
                    item.Value = i
                    item.Text = column.ColumnName
                    ddlField.Items.Add(item)
                    cboFieldNo.Items.Add(item)
                    i = i + 1
                Next

                'Clear Pivot
                Dim ci As Integer = pvtGrid.Fields.Count
                Dim cx As Integer
                For cx = 0 To ci - 1
                    pvtGrid.Fields.RemoveAt(0)
                Next

                'Refresh Pivot
                pvtGrid.DataSource = dt
                pvtGrid.RetrieveFields(DevExpress.XtraPivotGrid.PivotArea.FilterArea, False)

                'Check Data Type of Column
                ci = dt.Columns.Count
                For cx = 0 To ci - 1
                    If dt.Columns(cx).DataType.Name.ToString = "Integer" Then
                        pvtGrid.Fields(cx).CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        pvtGrid.Fields(cx).CellFormat.FormatString = "n0"
                        pvtGrid.Fields(cx).ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        pvtGrid.Fields(cx).TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        pvtGrid.Fields(cx).TotalCellFormat.FormatString = "n0"
                    End If

                    If dt.Columns(cx).DataType.Name.ToString = "Decimal" Then
                        pvtGrid.Fields(cx).CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        pvtGrid.Fields(cx).CellFormat.FormatString = "n2"
                        pvtGrid.Fields(cx).ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        pvtGrid.Fields(cx).TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        pvtGrid.Fields(cx).TotalCellFormat.FormatString = "n2"
                    End If

                    If dt.Columns(cx).DataType.Name.ToString = "DateTime" Then
                        pvtGrid.Fields(cx).GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.Date
                        Dim fieldYear As PivotGridField = New PivotGridField(dt.Columns(cx).ColumnName.ToString(), DevExpress.XtraPivotGrid.PivotArea.FilterArea)
                        Dim fieldMonth As PivotGridField = New PivotGridField(dt.Columns(cx).ColumnName.ToString(), DevExpress.XtraPivotGrid.PivotArea.FilterArea)
                        Dim fieldQtr As PivotGridField = New PivotGridField(dt.Columns(cx).ColumnName.ToString(), DevExpress.XtraPivotGrid.PivotArea.FilterArea)
                        ' Add the fields to the field collection.

                        ' Set the caption and group mode of the fields.
                        Dim Name As String = Replace(dt.Columns(cx).ColumnName.ToString(), " ", "")

                        fieldYear.ID = Replace(Name, "Date", "Year") & cx.ToString
                        fieldYear.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear
                        fieldYear.Caption = Replace(dt.Columns(cx).ColumnName.ToString(), "Date", "Year")
                        fieldYear.UnboundFieldName = Replace(Name, "Date", "Year")

                        fieldMonth.ID = Replace(Name, "Date", "Month") & cx.ToString
                        fieldMonth.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth
                        fieldMonth.Caption = Replace(dt.Columns(cx).ColumnName.ToString(), "Date", "Month")
                        fieldMonth.UnboundFieldName = Replace(Name, "Date", "Month")

                        fieldQtr.ID = Replace(Name, "Date", "Qtr") & cx.ToString
                        fieldQtr.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateQuarter
                        fieldQtr.Caption = Replace(dt.Columns(cx).ColumnName.ToString(), "Date", "Qtr")
                        fieldQtr.UnboundFieldName = Replace(Name, "Date", "Qtr.")

                        pvtGrid.Fields.Add(fieldYear)
                        pvtGrid.Fields.Add(fieldMonth)
                        pvtGrid.Fields.Add(fieldQtr)
                    End If

                Next

                MessageBox.Success("Data has been successfully loaded.", Me)

            End If

            pvtGrid.DataBind()
            pvtGrid.ReloadData()

        Else

            'Clear Field
            ddlField.Items.Clear()
            cboFieldNo.Items.Clear()
            ddlField.Items.Add("-- Select --")
            cboFieldNo.Items.Add("-- Select --")

            'Clear Pivot
            Dim ci As Integer = pvtGrid.Fields.Count
            Dim cx As Integer
            For cx = 0 To ci - 1
                pvtGrid.Fields.RemoveAt(0)
            Next

            pvtGrid.DataBind()
            pvtGrid.ReloadData()
        End If

    End Sub

    Private Sub PopulatePivotGrid_Selected(Optional tno As Integer = 0, Optional IsLoadData As Boolean = False)

        If Generic.ToInt(cboReportNo.SelectedValue) > 1 Then
            Dim IsSP As Boolean = False
            Dim dt2 As DataTable
            dt2 = SQLHelper.ExecuteDataTable("EReport_WebOne", UserNo, tno)
            For Each row As DataRow In dt2.Rows
                Datasource = Generic.ToStr(row("Datasource"))
                IsSP = Generic.ToBol(row("IsSP"))
            Next

            Dim Param As Object() = PopulateServerReport_Selected()

            Dim dt As DataTable
            If IsSP = True Then
                dt = SQLHelper.ExecuteDataTable(Datasource, Param)
            Else
                dt = SQLHelper.ExecuteDataTable("EFilteredValuePivot_WebGenerateView", UserNo, Datasource, PayLocNo)
            End If

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

            If IsLoadData Then

                'Field Dropdown
                ddlField.Items.Clear()
                cboFieldNo.Items.Clear()
                ddlField.Items.Add("-- Select --")
                cboFieldNo.Items.Add("-- Select --")
                Dim i As Integer = 0
                For Each column As DataColumn In dt.Columns
                    Dim item As New ListItem
                    item.Value = i
                    item.Text = column.ColumnName
                    ddlField.Items.Add(item)
                    cboFieldNo.Items.Add(item)
                    i = i + 1
                Next

                'Clear Pivot
                Dim ci As Integer = pvtGrid.Fields.Count
                Dim cx As Integer
                For cx = 0 To ci - 1
                    pvtGrid.Fields.RemoveAt(0)
                Next

                'Refresh Pivot
                pvtGrid.DataSource = dt
                pvtGrid.RetrieveFields(DevExpress.XtraPivotGrid.PivotArea.FilterArea, False)

                'Check Data Type of Column
                ci = dt.Columns.Count
                For cx = 0 To ci - 1
                    If dt.Columns(cx).DataType.Name.ToString = "Integer" Then
                        pvtGrid.Fields(cx).CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        pvtGrid.Fields(cx).CellFormat.FormatString = "n0"
                        pvtGrid.Fields(cx).ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        pvtGrid.Fields(cx).TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        pvtGrid.Fields(cx).TotalCellFormat.FormatString = "n0"
                    End If

                    If dt.Columns(cx).DataType.Name.ToString = "Decimal" Then
                        pvtGrid.Fields(cx).CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        pvtGrid.Fields(cx).CellFormat.FormatString = "n2"
                        pvtGrid.Fields(cx).ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        pvtGrid.Fields(cx).TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                        pvtGrid.Fields(cx).TotalCellFormat.FormatString = "n2"
                    End If

                    If dt.Columns(cx).DataType.Name.ToString = "DateTime" Then
                        pvtGrid.Fields(cx).GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.Date
                        Dim fieldYear As PivotGridField = New PivotGridField(dt.Columns(cx).ColumnName.ToString(), DevExpress.XtraPivotGrid.PivotArea.FilterArea)
                        Dim fieldMonth As PivotGridField = New PivotGridField(dt.Columns(cx).ColumnName.ToString(), DevExpress.XtraPivotGrid.PivotArea.FilterArea)
                        Dim fieldQtr As PivotGridField = New PivotGridField(dt.Columns(cx).ColumnName.ToString(), DevExpress.XtraPivotGrid.PivotArea.FilterArea)
                        ' Add the fields to the field collection.

                        ' Set the caption and group mode of the fields.
                        Dim Name As String = Replace(dt.Columns(cx).ColumnName.ToString(), " ", "")

                        fieldYear.ID = Replace(Name, "Date", "Year") & cx.ToString
                        fieldYear.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateYear
                        fieldYear.Caption = Replace(dt.Columns(cx).ColumnName.ToString(), "Date", "Year")
                        fieldYear.UnboundFieldName = Replace(Name, "Date", "Year")

                        fieldMonth.ID = Replace(Name, "Date", "Month") & cx.ToString
                        fieldMonth.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateMonth
                        fieldMonth.Caption = Replace(dt.Columns(cx).ColumnName.ToString(), "Date", "Month")
                        fieldMonth.UnboundFieldName = Replace(Name, "Date", "Month")

                        fieldQtr.ID = Replace(Name, "Date", "Qtr") & cx.ToString
                        fieldQtr.GroupInterval = DevExpress.XtraPivotGrid.PivotGroupInterval.DateQuarter
                        fieldQtr.Caption = Replace(dt.Columns(cx).ColumnName.ToString(), "Date", "Qtr")
                        fieldQtr.UnboundFieldName = Replace(Name, "Date", "Qtr.")

                        pvtGrid.Fields.Add(fieldYear)
                        pvtGrid.Fields.Add(fieldMonth)
                        pvtGrid.Fields.Add(fieldQtr)
                    End If

                Next

                MessageBox.Success("Data has been successfully loaded.", Me)

            End If

            pvtGrid.DataBind()
            pvtGrid.ReloadData()

        Else

            'Clear Field
            ddlField.Items.Clear()
            cboFieldNo.Items.Clear()
            ddlField.Items.Add("-- Select --")
            cboFieldNo.Items.Add("-- Select --")

            'Clear Pivot
            Dim ci As Integer = pvtGrid.Fields.Count
            Dim cx As Integer
            For cx = 0 To ci - 1
                pvtGrid.Fields.RemoveAt(0)
            Next

            pvtGrid.DataBind()
            pvtGrid.ReloadData()
        End If

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If Generic.ToInt(cboReportNo.SelectedValue) > 1 Then
            Dim Retval As Boolean = False
            pvtGrid.OptionsLayout.Columns.StoreAppearance = True
            Dim PivotSetting As String = Generic.ToStr(pvtGrid.SaveLayoutToString)
            Dim UserReportTitle As String = Generic.ToStr(txtUserReportTitle.Text)
            Dim Remarks As String = ""
            Dim ReportNo As Integer = Generic.ToInt(cboReportNo.SelectedValue)
            Dim ChartTypeNo As Integer = Generic.ToInt(cboChartTypeNo.SelectedValue)
            Dim PageSizeNo As Integer = Generic.ToInt(cboPageSizeNo.SelectedValue)
            Dim IsShowColumnTotals As Boolean = Generic.ToBol(txtShowColumnTotals.Checked)
            Dim IsShowColumnGrandTotals As Boolean = Generic.ToBol(txtShowColumnGrandTotals.Checked)
            Dim IsShowRowTotals As Boolean = Generic.ToBol(txtShowRowTotals.Checked)
            Dim IsShowRowGrandTotals As Boolean = Generic.ToBol(txtShowRowGrandTotals.Checked)
            Dim Width As Integer = Generic.ToInt(txtWidth.Text)
            Dim Height As Integer = Generic.ToInt(txtHeight.Text)

            'If SQLHelper.ExecuteNonQuery("EUserReport_WebSave", UserNo, TransNo, UserReportTitle, Remarks, ReportNo, Datasource, PivotSetting, ChartTypeNo, PageSizeNo, IsShowColumnTotals, IsShowColumnGrandTotals, IsShowRowTotals, IsShowRowGrandTotals, Width, Height, PayLocNo) > 0 Then
            Dim obj As Object = SQLHelper.ExecuteScalar("EUserReport_WebSave", UserNo, TransNo, UserReportTitle, Remarks, ReportNo, Datasource, PivotSetting, ChartTypeNo, PageSizeNo, IsShowColumnTotals, IsShowColumnGrandTotals, IsShowRowTotals, IsShowRowGrandTotals, Width, Height, PayLocNo)
            If Generic.ToInt(obj) > 0 Then
                Retval = True
                If TransNo = 0 Then
                    TransNo = Generic.ToInt(obj)
                End If
                Try
                    Dim Param As Object() = PopulateServerReport_Selected()
                    Dim Cnt As Integer = Param.Count
                    If Param.Count > 0 Then
                        For i As Integer = 0 To Cnt - 3
                            SQLHelper.ExecuteNonQuery("EUserReportParam_WebSave_ParamValue", UserNo, 0, TransNo, ViewState("ParamNo" & i), ViewState("ParamValue" & i), ViewState("ParamValueDesc" & i))
                        Next
                    End If

                Catch ex As Exception

                End Try
            Else
                Retval = False
            End If

            If Retval = True Then
                If Generic.ToInt(TransNo) = 0 Then
                    Dim url As String = "RptUserList.aspx?"
                    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                Else
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                End If
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If

        Else
            MessageBox.Alert("Datasource is required.", "warning", Me)
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("UserReportNo"))
        AccessRights.CheckUser(UserNo, Generic.ToStr(Session("xFormName")), Generic.ToStr(Session("xTableName")))

        If Not IsPostBack Then
            PopulateData()
            PopulateCombo()
        End If

        'Populate Report Parameters:
        If Me.cboReportNo.SelectedValue > 0 Then
            If Not IsPostBack Then
                PopulateParameterValues()
            End If
            PopulateParameter()
        End If

        'If Not IsPostBack Then
        '    PopulatePivotGrid(Generic.ToInt(cboReportNo.SelectedValue))
        'End If

        PopulatePivotGrid(Generic.ToInt(cboReportNo.SelectedValue))
        WebChartControl1.SeriesTemplate.ChangeView(Generic.ToInt(cboChartTypeNo.SelectedValue))

        If Not IsPostBack Then
            PopulatePivotSet()
            SetDataFieldsProperties()
        End If

        PopulatePivotSummary()

    End Sub

    Private Sub PopulatePivotSet()
        pvtGrid.RetrieveFields(DevExpress.XtraPivotGrid.PivotArea.FilterArea, False)
        pvtGrid.OptionsLayout.Columns.AddNewColumns = False
        pvtGrid.OptionsLayout.Columns.RemoveOldColumns = False
        pvtGrid.LoadLayoutFromString(PivotSettings)
        pvtGrid.OptionsCustomization.CustomizationFormStyle = DevExpress.XtraPivotGrid.Customization.CustomizationFormStyle.Excel2007
        pvtGrid.OptionsPager.RowsPerPage = Generic.ToInt(cboPageSizeNo.SelectedValue)
        pvtGrid.OptionsView.ShowColumnGrandTotals = Generic.ToBol(txtShowColumnGrandTotals.Checked)
        pvtGrid.OptionsView.ShowColumnTotals = Generic.ToBol(txtShowColumnTotals.Checked)
        pvtGrid.OptionsView.ShowRowGrandTotals = Generic.ToBol(txtShowRowGrandTotals.Checked)
        pvtGrid.OptionsView.ShowRowTotals = Generic.ToBol(txtShowRowTotals.Checked)
        'pvtGrid.OptionsView.ShowGrandTotalsForSingleValues = True
        'pvtGrid.OptionsView.ShowTotalsForSingleValues = True
        PopulateChartSize()

    End Sub

    Private Sub PopulatePivotSummary()
        Dim ci As Integer = pvtGrid.Fields.Count
        Dim cx As Integer
        For cx = 0 To ci - 1
            If pvtGrid.Fields(cx).FieldName = cboFieldNo.SelectedItem.Text Then
                pvtGrid.Fields(cboFieldNo.SelectedItem.Text).TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.CustomTotals
                SetCustomTotals(pvtGrid.Fields(cboFieldNo.SelectedItem.Text).CustomTotals)
            End If
        Next
    End Sub

    Private Sub PopulateChartSize()
        Dim Height As Integer = Generic.ToInt(txtHeight.Text)
        Dim Width As Integer = Generic.ToInt(txtWidth.Text)
        If Height = 0 Then
            Height = 500
            txtHeight.Text = Height
        End If

        If Width = 0 Then
            Width = 1060
            txtWidth.Text = Width
        End If
        WebChartControl1.Height = Height
        WebChartControl1.Width = Width
    End Sub

    Protected Sub lnkRefresh_Click(sender As Object, e As System.EventArgs)
        Try
            PopulatePivotGrid_Selected(Generic.ToInt(cboReportNo.SelectedValue), True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboReportNo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboReportNo.SelectedIndexChanged
        Try
            'Populate Report Parameters:
            'PopulateParameter()
            'PopulateParameterValues()
            'PopulateParameter()
        Catch ex As Exception

        End Try
    End Sub

#Region "Report Filter Controls and Values"

    

    Private Sub PopulateParameter()
        Dim Rowcount As Integer = 0
        _ds = SQLHelper.ExecuteDataSet("EReportParam_Web", UserNo, Generic.ToInt(cboReportNo.SelectedValue))
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                Rowcount = _ds.Tables(0).Rows.Count
                Me.lblParamTitle.Text = "Filter Parameters :"
                'pform.Controls.Clear()
                For i As Integer = 0 To Rowcount - 1

                    ReportParamTypeNo = Generic.ToInt(_ds.Tables(0).Rows(i)("ReportParamTypeNo"))
                    ReportParamNo = Generic.ToInt(_ds.Tables(0).Rows(i)("ReportParamNo"))
                    TableViewName = Generic.ToStr(_ds.Tables(0).Rows(i)("TableViewName"))
                    ReportField = Generic.ToStr(_ds.Tables(0).Rows(i)("ReportField"))
                    ReportLabel = Generic.ToStr(_ds.Tables(0).Rows(i)("ReportLabel"))
                    ReportParamFieldTypeno = Generic.ToInt(_ds.Tables(0).Rows(i)("ReportParamFieldTypeno"))
                    category = Generic.ToStr(_ds.Tables(0).Rows(i)("category"))
                    ReportFieldWidth = Generic.ToInt(_ds.Tables(0).Rows(i)("ReportFieldWidth"))
                    xIsHidden = Generic.ToInt(_ds.Tables(0).Rows(i)("IsHidden"))
                    Dim IndexNo As Integer = Generic.ToInt(_ds.Tables(0).Rows(i)("OrderLevel"))
                    PopulateControls(i)
                Next
            Else
                Me.lblParamTitle.Text = ""
            End If
        End If

        'Me.tccontent.Controls.Add(tblMain)
    End Sub

    Private Sub PopulateControls(Optional ByVal Index As Short = 0)
        Try

            Dim lbl As New Label
            Dim drp As New DropDownList
            pform.Controls.Add(New LiteralControl("<div class='form-group'>"))
            lbl.Text = ReportLabel
            lbl.ID = "lbl" & ReportParamNo
            If RequiredField = True Then
                lbl.CssClass = "col-md-4 control-label has-required"
            Else
                lbl.CssClass = "col-md-4 control-label has-space"
            End If
            'lbl.Text = lbl.CssClass.ToString
            pform.Controls.Add(lbl)


            If ReportParamFieldTypeno = 2 Then
                pform.Controls.Add(New LiteralControl("<div class='col-md-2'>"))
            Else
                pform.Controls.Add(New LiteralControl("<div class='col-md-4'>"))
            End If

            If ReportParamTypeNo = 1 Then
                If ReportField = "filterbyno" Then
                    If RequiredField = True Then
                        drpFilterbyNo.CssClass = "form-control required"
                    Else
                        drpFilterbyNo.CssClass = "form-control"
                    End If

                    drpFilterbyNo.DataSource = clsGeneric.xLookup_Table(UserNo, TableViewName)
                    drpFilterbyNo.DataTextField = "tDesc"
                    drpFilterbyNo.DataValueField = "tno"
                    drpFilterbyNo.DataBind()
                    drpFilterbyNo.ID = "filterbyno"
                    drpFilterbyNo.AutoPostBack = True
                    AddHandler drpFilterbyNo.SelectedIndexChanged, AddressOf drpFilterbyNo_SelectedIndexChanged
                    pform.Controls.Add(drpFilterbyNo)
                    Try
                        drpFilterbyNo.Text = ViewState("ParamValue" & Index)
                    Catch ex As Exception

                    End Try

                ElseIf ReportField = "filterbyid" Then
                    drpFilterbyID.ID = "filterbyid"
                    drpFilterbyID.AutoPostBack = True
                    AddHandler drpFilterbyID.SelectedIndexChanged, AddressOf drpFilterbyID_SelectedIndexChanged
                    If RequiredField = True Then
                        drpFilterbyID.CssClass = "form-control required"
                    Else
                        drpFilterbyID.CssClass = "form-control"
                    End If
                    pform.Controls.Add(drpFilterbyID)
                    Try
                        'If ViewState("ParamValue" & Index) = 0 Then
                        '    ViewState("ParamValue" & Index) = drpFilterbyID.SelectedValue
                        'End If
                        'If drpFilterbyNo.SelectedValue <> ViewState("ParamValue" & 0) Then
                        '    ViewState("ParamValue" & 0) = 0
                        '    ViewState("ParamValue" & Index) = 0
                        'End If
                        drpFilterbyID.Items.Clear()
                        populateDataDropdownF(drpFilterbyNo.SelectedValue)
                        ViewState("ParamValueSelected" & Index) = drpFilterbyID.SelectedValue
                        drpFilterbyID.Text = ViewState("ParamValue" & Index)
                    Catch ex As Exception

                    End Try
                    xCategory = category

                    'If RequiredField = True Then
                    '    txtName.CssClass = "form-control required"
                    'Else
                    '    txtName.CssClass = "form-control"
                    'End If

                    'txtName.ID = "txtName"
                    ''txtName.Enabled = False
                    'pForm.Controls.Add(txtName)

                    'drpAC.ID = "filterbyid"
                    'drpAC.SkinID = "AC"
                    'drpAC.OnClientItemSelected = "GetRecord"
                    'drpAC.ServiceMethod = "populateDataDropdown"
                    'drpAC.MinimumPrefixLength = "2"
                    'drpAC.CompletionInterval = "500"
                    'drpAC.CompletionSetCount = "0"
                    'drpAC.TargetControlID = "txtName"
                    ''drpAC.Enabled = True
                    'pform.Controls.Add(drpAC)

                    'txtName.Text = ViewState("ParamValueDesc" & Index)
                    'hifNo.Value = ViewState("ParamValue" & Index)
                Else
                    If RequiredField = True Then
                        drp.CssClass = "form-control required"
                    Else
                        drp.CssClass = "form-control"
                    End If

                    drp.ID = ReportField
                    If TableViewName.Length > 0 Or Generic.ToStr(TableViewName) <> "" Then
                        If category = "sp" Then
                            drp.DataSource = SQLHelper.ExecuteDataSet(TableViewName, UserNo, Session("xPayLocNo"))
                        Else
                            drp.DataSource = clsGeneric.xLookup_Table(UserNo, TableViewName, Session("xPayLocNo"))
                        End If
                        drp.DataTextField = "tDesc"
                        drp.DataValueField = "tno"
                        drp.DataBind()
                    End If

                    drp.Text = ViewState("ParamValue" & Index)

                    pform.Controls.Add(drp)

                End If
            ElseIf ReportParamTypeNo = 2 Then
                Dim txt As New TextBox
                Dim cal As New AjaxControlToolkit.CalendarExtender
                Dim mask As New AjaxControlToolkit.MaskedEditExtender

                txt.ID = ReportField
                If RequiredField = True Then
                    txt.CssClass = "form-control required"
                Else
                    txt.CssClass = "form-control"
                End If
                txt.Text = ViewState("ParamValue" & Index)
                pform.Controls.Add(txt)
                If ReportField = "paylocno" Then
                    txt.Enabled = False
                    txt.Text = Session("xPayLocNo")
                End If

                If ReportParamFieldTypeno = 2 Then
                    'txt.SkinID = "txtdate"
                    cal.TargetControlID = txt.ID
                    cal.Format = "MM/dd/yyyy"
                    cal.ID = "cal" & ReportParamNo
                    mask.ID = "mask" & ReportParamNo
                    mask.TargetControlID = txt.ID
                    mask.Mask = "99/99/9999"
                    mask.MessageValidatorTip = True
                    mask.OnFocusCssClass = "MaskedEditFocus"
                    mask.OnInvalidCssClass = "MaskedEditError"
                    mask.MaskType = AjaxControlToolkit.MaskedEditType.Date
                    mask.DisplayMoney = AjaxControlToolkit.MaskedEditShowSymbol.Left
                    pform.Controls.Add(cal)
                    pform.Controls.Add(mask)
                End If
            End If


            pform.Controls.Add(New LiteralControl("</div>"))
            pform.Controls.Add(New LiteralControl("</div>"))

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateParameterValues()
        Dim Rowcount As Integer = 0
        _ds = SQLHelper.ExecuteDataSet("EUserReportParam_Web", UserNo, TransNo, Generic.ToInt(cboReportNo.SelectedValue))
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                Rowcount = _ds.Tables(0).Rows.Count
                For i As Integer = 0 To Rowcount - 1
                    ViewState("ParamNo" & i) = _ds.Tables(0).Rows(i)("ReportParamNo")
                    ViewState("ParamValue" & i) = Generic.ToStr(_ds.Tables(0).Rows(i)("ParamValue"))
                    ViewState("ParamValueDesc" & i) = Generic.ToStr(_ds.Tables(0).Rows(i)("ParamValueDesc"))
                Next
            End If
        End If

        'Dim fId As Integer
        'fId = Generic.ToInt(drpFilterbyNo.SelectedValue)

        'If fId > 0 Then
        '    txtName.Enabled = True
        '    drpAC.CompletionSetCount = fId
        'Else
        '    txtName.Enabled = True
        '    drpAC.CompletionSetCount = 0
        'End If
        'txtName.Enabled = True
        'drpAC.CompletionSetCount = 12
    End Sub

    Protected Sub drpFilterbyNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim fId As String = ""
            Dim fDrp As New DropDownList
            fDrp = sender

            fId = fDrp.ID
            Dim tId As Integer
            tId = Generic.ToInt(drpFilterbyNo.SelectedValue)
            drpFilterbyID.Items.Clear()
            If ViewState("ParamValueByNo" & 0) <> tId Then
                drpFilterbyID.Text = ""
                ViewState("ParamValue" & 1) = ""
                ViewState("ParamValueByNo" & 0) = tId
            End If
            populateDataDropdownF(tId)

        Catch ex As Exception
        End Try

        'txtName.Text = ""
        'hifNo.Value = "0"
        'Dim fId As Integer
        'fId = Generic.ToInt(drpFilterbyNo.SelectedValue)

        'If fId > 0 Then
        '    txtName.Enabled = True
        '    drpAC.CompletionSetCount = fId
        'Else
        '    txtName.Enabled = False
        '    drpAC.CompletionSetCount = 0
        'End If

    End Sub

    Protected Sub drpFilterbyID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim fId As String = ""
            Dim fDrp As New DropDownList
            fDrp = sender

            fId = fDrp.ID
            Dim tId As Integer
            tId = Generic.ToInt(drpFilterbyID.SelectedValue)
            ViewState("ParamValue" & 1) = tId
            'PopulateParameter()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub populateDataDropdownF(ByVal fid As Integer)
        Dim clsGen As New clsGenericClass
        Dim ds As DataSet
        ds = clsGen.populateDropdownFilterByCate(fid, UserNo, Session("xPayLocNo"), xCategory)
        drpFilterbyID.DataSource = ds
        drpFilterbyID.DataTextField = "tDesc"
        drpFilterbyID.DataValueField = "tNo"
        drpFilterbyID.DataBind()
    End Sub

    Private Function PopulateServerReport() As Object()
        'PopulateServerReport(0) = 0
        Dim ds As New DataSet
        'Dim dsrpt As New ReportDataSource

        'Dim rpt As ServerReport = rviewer.ServerReport
        'Me.rviewer.ProcessingMode = ProcessingMode.Local
        'Me.rviewer.Reset()


        Dim dsr As New DataSet
        Dim Rowcount As Integer = 0
        Dim fReportField As String = ""
        Dim forderlevel As Integer = 0

        Dim txt As New TextBox
        Dim drp As New DropDownList
        Dim Filterbyid As New DropDownList
        'Dim Filterbyid As New AutoCompleteExtender
        Dim Filterbyno As New DropDownList
        Dim fpid As Integer = 0
        Dim ftxt As String = ""
        Dim freportparamfieldtypeno As Integer = 0
        Dim fcategory As String = ""
        Dim IsHidden As Boolean = 0
        Dim ReportParamNo As Integer
        Dim freportparamtypeno As Integer = 0

        ds = SQLHelper.ExecuteDataSet("EReportParam_Web", UserNo, Generic.ToInt(cboReportNo.SelectedValue))
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Rowcount = ds.Tables(0).Rows.Count
                Dim parm(Rowcount + 1) As Object
                xParam = parm
                parm(0) = UserNo 'xPublicVar.xOnlineUseNo
                parm(1) = PayLocNo
                For i As Integer = 0 To Rowcount - 1

                    Filterbyid = Nothing
                    Filterbyno = Nothing
                    drp = Nothing
                    txt = Nothing

                    ReportParamNo = Generic.ToInt(ds.Tables(0).Rows(i)("ReportParamNo"))
                    forderlevel = Generic.ToInt(ds.Tables(0).Rows(i)("orderlevel"))
                    fReportField = Generic.ToStr(ds.Tables(0).Rows(i)("ReportField"))
                    freportparamfieldtypeno = Generic.ToInt(ds.Tables(0).Rows(i)("reportparamfieldtypeno"))
                    fcategory = Generic.ToStr(ds.Tables(0).Rows(i)("category"))
                    IsHidden = Generic.ToInt(ds.Tables(0).Rows(i)("IsHidden"))
                    'ViewState("ParamNo" & i) = ReportParamNo
                    freportparamtypeno = Generic.ToStr(_ds.Tables(0).Rows(i)("reportparamtypeno"))

                    If freportparamtypeno = 1 Then
                        If fReportField = "filterbyid" Then
                            Try
                                'Filterbyid = CType(pform.FindControl(fReportField), AutoCompleteExtender)
                                Filterbyid = CType(pform.FindControl(fReportField), DropDownList)
                            Catch ex As Exception
                                Filterbyid = Nothing
                            End Try
                        ElseIf fReportField = "filterbyno" Then
                            Try
                                Filterbyno = CType(pform.FindControl(fReportField), DropDownList)
                            Catch ex As Exception
                                Filterbyno = Nothing
                            End Try
                        Else
                            Try
                                drp = CType(pform.FindControl(fReportField), DropDownList)
                            Catch ex As Exception
                                drp = Nothing
                            End Try
                        End If
                    Else
                        Try
                            txt = CType(pform.FindControl(fReportField), TextBox)
                        Catch ex As Exception
                            txt = Nothing
                        End Try
                    End If

                    Try
                        txt = CType(pform.FindControl(fReportField), TextBox)
                        txt.Visible = Not IsHidden
                    Catch ex As Exception
                        txt = Nothing
                    End Try

                    forderlevel = forderlevel + 1
                    If Not txt Is Nothing Then
                        If freportparamfieldtypeno = 1 Then
                            fpid = Generic.ToInt(txt.Text)
                            parm(forderlevel + 0) = fpid
                            'ViewState("ParamValue" & i) = fpid
                        Else
                            ftxt = Generic.ToStr(txt.Text)
                            parm(forderlevel + 0) = ftxt
                            'ViewState("ParamValue" & i) = ftxt
                        End If
                    ElseIf Not drp Is Nothing Then
                        fpid = Generic.ToInt(drp.SelectedValue)
                        parm(forderlevel + 0) = fpid
                        'ViewState("ParamValue" & i) = fpid
                    ElseIf Not Filterbyid Is Nothing Then
                        fpid = Generic.ToInt(Filterbyid.SelectedValue)
                        'If txtName.Text = "" Then
                        '    fpid = 0
                        'Else
                        '    fpid = Generic.ToInt(hifNo.Value)
                        'End If
                        parm(forderlevel + 0) = fpid
                        'ViewState("ParamValue" & i) = fpid
                        'ViewState("ParamValueDesc" & i) = txtName.Text
                    ElseIf Not Filterbyno Is Nothing Then
                        fpid = Generic.ToInt(Filterbyno.SelectedValue)
                        parm(forderlevel + 0) = fpid
                        'ViewState("ParamValue" & i) = fpid
                    End If
                    
                Next
                xParam = parm
            End If
        End If
        PopulateServerReport = xParam

    End Function

    Private Function PopulateServerReport_Selected() As Object()
        'PopulateServerReport(0) = 0
        Dim ds As New DataSet
        'Dim dsrpt As New ReportDataSource

        'Dim rpt As ServerReport = rviewer.ServerReport
        'Me.rviewer.ProcessingMode = ProcessingMode.Local
        'Me.rviewer.Reset()


        Dim dsr As New DataSet
        Dim Rowcount As Integer = 0
        Dim fReportField As String = ""
        Dim forderlevel As Integer = 0

        Dim txt As New TextBox
        Dim drp As New DropDownList
        Dim Filterbyid As New DropDownList
        'Dim Filterbyid As New AutoCompleteExtender
        Dim Filterbyno As New DropDownList
        Dim fpid As Integer = 0
        Dim ftxt As String = ""
        Dim freportparamfieldtypeno As Integer = 0
        Dim fcategory As String = ""
        Dim IsHidden As Boolean = 0
        Dim ReportParamNo As Integer
        Dim freportparamtypeno As Integer = 0

        ds = SQLHelper.ExecuteDataSet("EReportParam_Web", UserNo, Generic.ToInt(cboReportNo.SelectedValue))
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                Rowcount = ds.Tables(0).Rows.Count
                Dim parm(Rowcount + 1) As Object
                xParam = parm
                parm(0) = UserNo 'xPublicVar.xOnlineUseNo
                parm(1) = PayLocNo
                For i As Integer = 0 To Rowcount - 1

                    Filterbyid = Nothing
                    Filterbyno = Nothing
                    drp = Nothing
                    txt = Nothing

                    ReportParamNo = Generic.ToInt(ds.Tables(0).Rows(i)("ReportParamNo"))
                    forderlevel = Generic.ToInt(ds.Tables(0).Rows(i)("orderlevel"))
                    fReportField = Generic.ToStr(ds.Tables(0).Rows(i)("ReportField"))
                    freportparamfieldtypeno = Generic.ToInt(ds.Tables(0).Rows(i)("reportparamfieldtypeno"))
                    fcategory = Generic.ToStr(ds.Tables(0).Rows(i)("category"))
                    IsHidden = Generic.ToInt(ds.Tables(0).Rows(i)("IsHidden"))
                    ViewState("ParamNo" & i) = ReportParamNo
                    freportparamtypeno = Generic.ToStr(_ds.Tables(0).Rows(i)("reportparamtypeno"))

                    If freportparamtypeno = 1 Then
                        If fReportField = "filterbyid" Then
                            Try
                                'Filterbyid = CType(pform.FindControl(fReportField), AutoCompleteExtender)
                                Filterbyid = CType(pform.FindControl(fReportField), DropDownList)
                            Catch ex As Exception
                                Filterbyid = Nothing
                            End Try
                        ElseIf fReportField = "filterbyno" Then
                            Try
                                Filterbyno = CType(pform.FindControl(fReportField), DropDownList)
                            Catch ex As Exception
                                Filterbyno = Nothing
                            End Try
                        Else
                            Try
                                drp = CType(pform.FindControl(fReportField), DropDownList)
                            Catch ex As Exception
                                drp = Nothing
                            End Try
                        End If
                    Else
                        Try
                            txt = CType(pform.FindControl(fReportField), TextBox)
                        Catch ex As Exception
                            txt = Nothing
                        End Try
                    End If

                    Try
                        txt = CType(pform.FindControl(fReportField), TextBox)
                        txt.Visible = Not IsHidden
                    Catch ex As Exception
                        txt = Nothing
                    End Try

                    forderlevel = forderlevel + 1
                    If Not txt Is Nothing Then
                        If freportparamfieldtypeno = 1 Then
                            fpid = Generic.ToInt(txt.Text)
                            parm(forderlevel + 0) = fpid
                            ViewState("ParamValue" & i) = fpid
                        Else
                            ftxt = Generic.ToStr(txt.Text)
                            parm(forderlevel + 0) = ftxt
                            ViewState("ParamValue" & i) = ftxt
                        End If
                    ElseIf Not drp Is Nothing Then
                        fpid = Generic.ToInt(drp.SelectedValue)
                        parm(forderlevel + 0) = fpid
                        ViewState("ParamValue" & i) = fpid
                    ElseIf Not Filterbyid Is Nothing Then
                        fpid = Generic.ToInt(Filterbyid.SelectedValue)
                        'If txtName.Text = "" Then
                        '    fpid = 0
                        'Else
                        '    fpid = Generic.ToInt(hifNo.Value)
                        'End If
                        parm(forderlevel + 0) = fpid
                        ViewState("ParamValue" & i) = fpid
                        'ViewState("ParamValueDesc" & i) = txtName.Text
                    ElseIf Not Filterbyno Is Nothing Then
                        fpid = Generic.ToInt(Filterbyno.SelectedValue)
                        parm(forderlevel + 0) = fpid
                        ViewState("ParamValue" & i) = fpid
                    End If

                Next
                xParam = parm
            End If
        End If
        PopulateServerReport_Selected = xParam

    End Function

    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function populateDataDropdown(prefixText As String, count As Integer, contextKey As String) As List(Of String)
        Dim items As New List(Of String)()
        Dim _ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim clsbase As New clsBase.clsBaseLibrary
        Dim UserNo As Integer = 0, PayLocNo As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        PayLocNo = (HttpContext.Current.Session("xPayLocNo"))

        _ds = SQLHelper.ExecuteDataSet("EFilterBy_WebLookup_AC", UserNo, prefixText, PayLocNo, count)
        For Each row As DataRow In _ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("tDesc")),
                                Generic.ToStr(row("tNo")))
            items.Add(item)
        Next
        _ds.Dispose()
        Return items


    End Function

#End Region


    Protected Sub txtChartSize_OnTextChanged(sender As Object, e As EventArgs)
        PopulateChartSize()
    End Sub

    Protected Sub cboChartType_SelectedIndexChanged(sender As Object, e As EventArgs)
        WebChartControl1.SeriesTemplate.ChangeView(cboChartTypeNo.SelectedValue)
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(sender As Object, e As EventArgs)
        pvtGrid.OptionsPager.RowsPerPage = Generic.ToInt(cboPageSizeNo.SelectedValue)
    End Sub

    Protected Sub txtShowColumnTotals_CheckedChanged(sender As Object, e As EventArgs)
        pvtGrid.OptionsView.ShowColumnTotals = Generic.ToBol(txtShowColumnTotals.Checked)
    End Sub

    Protected Sub txtShowColumnGrandTotals_CheckedChanged(sender As Object, e As EventArgs)
        pvtGrid.OptionsView.ShowColumnGrandTotals = Generic.ToBol(txtShowColumnGrandTotals.Checked)
    End Sub

    Protected Sub txtShowRowTotals_CheckedChanged(sender As Object, e As EventArgs)
        pvtGrid.OptionsView.ShowRowTotals = Generic.ToBol(txtShowRowTotals.Checked)
    End Sub

    Protected Sub txtShowRowGrandTotals_CheckedChanged(sender As Object, e As EventArgs)
        pvtGrid.OptionsView.ShowRowGrandTotals = Generic.ToBol(txtShowRowGrandTotals.Checked)
    End Sub

    
#Region "*******Export Report********"
    Protected Sub lnkExport_Click(sender As Object, e As System.EventArgs) Handles lnkExport.Click
        Try
            Dim filename As String = "temp"
            ASPxPivotGridExporter1.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.[False]
            ASPxPivotGridExporter1.OptionsPrint.PrintColumnHeaders = DevExpress.Utils.DefaultBoolean.[False]
            ASPxPivotGridExporter1.OptionsPrint.PrintRowHeaders = DevExpress.Utils.DefaultBoolean.[False]
            ASPxPivotGridExporter1.OptionsPrint.PrintDataHeaders = DevExpress.Utils.DefaultBoolean.[False]
            Select Case Me.cboFileTypeExportNo1.SelectedValue
                Case 1 'Excel File
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
                Case 2 'Word
                    Dim options As New RtfExportOptions
                    With options
                        .ExportMode = RtfExportMode.SingleFilePageByPage
                        .ExportPageBreaks = True
                    End With
                    ASPxPivotGridExporter1.ExportRtfToResponse(filename, True)
                Case 3 'PDF
                    Dim options As New PdfExportOptions
                    With options
                        .ShowPrintDialogOnOpen = True
                        .ImageQuality = PdfJpegImageQuality.Highest
                        .PdfACompatible = True
                        .Compressed = True
                    End With
                    ASPxPivotGridExporter1.ExportPdfToResponse(filename, True)
            End Select
        Catch ex As Exception

        End Try

    End Sub

    'Protected Sub lnkExport2_Click(sender As Object, e As EventArgs)
    '    Dim ps As New PrintingSystemBase()

    '    Dim link As New PrintableComponentLinkBase(ps)
    '    WebChartControl1.DataBind()
    '    link.Component = DirectCast(WebChartControl1, DevExpress.XtraCharts.Native.IChartContainer).Chart

    '    link.CreateDocument()

    '    Using stream As New MemoryStream()
    '        Dim options As New XlsxExportOptions()
    '        options.ExportMode = XlsxExportMode.SingleFile
    '        options.SheetName = "temp"
    '        link.PrintingSystemBase.ExportToXlsx(stream, options)
    '        Response.Clear()
    '        Response.Buffer = False
    '        Response.AppendHeader("Content-Type", "application/xlsx")
    '        Response.AppendHeader("Content-Transfer-Encoding", "binary")
    '        Response.AppendHeader("Content-Disposition", "attachment; filename=temp_chart.xlsx")
    '        Response.BinaryWrite(stream.ToArray())
    '        Response.[End]()
    '    End Using
    '    ps.Dispose()

    'End Sub

    Protected Sub lnkExport2_Click(sender As Object, e As EventArgs)
        Dim ps As New PrintingSystemBase()

        Dim link As New PrintableComponentLinkBase(ps)
        WebChartControl1.DataBind()
        link.Component = DirectCast(WebChartControl1, DevExpress.XtraCharts.Native.IChartContainer).Chart

        link.CreateDocument()

        Using stream As New MemoryStream()
            Select Case Me.cboFileTypeExportNo2.SelectedValue
                Case 1 'Excel
                    Dim options As New XlsxExportOptions()
                    options.ExportMode = XlsxExportMode.SingleFile
                    options.SheetName = "temp"
                    link.PrintingSystemBase.ExportToXlsx(stream, options)
                    Response.Clear()
                    Response.Buffer = False
                    Response.AppendHeader("Content-Type", "application/xlsx")
                    Response.AppendHeader("Content-Transfer-Encoding", "binary")
                    Response.AppendHeader("Content-Disposition", "attachment; filename=temp_chart.xlsx")
                    Response.BinaryWrite(stream.ToArray())
                    Response.[End]()
                Case 2 'Word
                    Dim options As New RtfExportOptions()
                    options.ExportMode = RtfExportMode.SingleFilePageByPage
                    options.ExportPageBreaks = True
                    link.PrintingSystemBase.ExportToRtf(stream, options)
                    Response.Clear()
                    Response.Buffer = False
                    Response.AppendHeader("Content-Type", "application/vnd.ms-word")
                    Response.AppendHeader("Content-Transfer-Encoding", "binary")
                    Response.AppendHeader("Content-Disposition", "attachment; filename=temp_chart.doc")
                    Response.BinaryWrite(stream.ToArray())
                    Response.[End]()
                Case 3 'PDF
                    Dim options As New PdfExportOptions()
                    options.Compressed = True
                    options.ImageQuality = PdfJpegImageQuality.Highest

                    link.PrintingSystemBase.ExportToPdf(stream, options)
                    Response.Clear()
                    Response.Buffer = False
                    Response.AppendHeader("Content-Type", "application/pdf")
                    Response.AppendHeader("Content-Transfer-Encoding", "binary")
                    Response.AppendHeader("Content-Disposition", "attachment; filename=temp_chart.pdf")
                    Response.BinaryWrite(stream.ToArray())
                    Response.[End]()
            End Select

        End Using
        ps.Dispose()

    End Sub


#End Region
    

#Region "*******Column Summary********"
    Private Sub SetDataFieldsProperties()
        For Each field As PivotGridField In pvtGrid.Fields
            If field.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea Then
                field.HeaderStyle.Font.Bold = False
                field.HeaderStyle.HoverStyle.Font.Bold = False
                field.Caption = String.Format("{0}({1})", field.FieldName, ddlSummaryType.Items(CInt(Fix(field.SummaryType))))
            End If
        Next field

        If SelectedDataField IsNot Nothing Then
            SelectedDataField.HeaderStyle.Font.Bold = True
            SelectedDataField.HeaderStyle.HoverStyle.Font.Bold = True
            If SelectedDataField.SummaryType <> CType(ddlSummaryType.SelectedIndex, PivotSummaryType) Then
                ddlSummaryType.SelectedIndex = CInt(Fix(SelectedDataField.SummaryType))
            End If
        End If
    End Sub
    Private ReadOnly Property SelectedDataField() As PivotGridField
        Get
            Return pvtGrid.Fields(ddlField.SelectedItem.Text)
        End Get
    End Property
    Protected Sub ddlField_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        SetDataFieldsProperties()
    End Sub
    Protected Sub ddlSummaryType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        If SelectedDataField IsNot Nothing Then
            SelectedDataField.SummaryType = CType(ddlSummaryType.SelectedIndex, PivotSummaryType)
        End If
        SetDataFieldsProperties()
    End Sub
#End Region

    
#Region "*******Row Summary********"
    Private Sub SetCustomTotals(ByVal totals As DevExpress.Web.ASPxPivotGrid.PivotGridCustomTotalCollection)
        Dim newTotals As List(Of PivotSummaryType) = GetCustomTotals()
        If CustomTotalsEqual(totals, newTotals) Then
            Return
        End If
        totals.Clear()
        For i As Integer = 0 To newTotals.Count - 1
            totals.Add(newTotals(i))
        Next i
    End Sub
    Private Function CustomTotalsEqual(ByVal totals As PivotGridCustomTotalCollection, ByVal newTotals As List(Of PivotSummaryType)) As Boolean
        If totals.Count <> newTotals.Count Then
            Return False
        End If
        For i As Integer = 0 To newTotals.Count - 1
            If (Not totals.Contains(newTotals(i))) Then
                Return False
            End If
        Next i
        Return True
    End Function
    Private Function GetCustomTotals() As List(Of PivotSummaryType)
        Dim res As New List(Of PivotSummaryType)()
        If average.Checked Then
            res.Add(PivotSummaryType.Average)
        End If
        If count.Checked Then
            res.Add(PivotSummaryType.Count)
        End If
        If max.Checked Then
            res.Add(PivotSummaryType.Max)
        End If
        If min.Checked Then
            res.Add(PivotSummaryType.Min)
        End If
        If sum.Checked Then
            res.Add(PivotSummaryType.Sum)
        End If

        Return res
    End Function
#End Region


    

End Class
