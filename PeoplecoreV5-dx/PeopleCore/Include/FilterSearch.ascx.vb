Imports DevExpress.Web
Imports clsLib
Imports System.Data

Partial Class Include_FilterSearch
    Inherits System.Web.UI.UserControl

    Private _content As ITemplate = Nothing
    Private _enablecontent As Boolean = False
    Private _enablefilter As Boolean = False
    Private _search As String = ""
    Private _IndexNo As Integer = 0
    Public Event lnkSearchClick As EventHandler

    <TemplateContainer(GetType(TemplateControl))> _
    <PersistenceMode(PersistenceMode.InnerProperty)> _
    <TemplateInstance(TemplateInstance.[Single])> _
    Public Property Content() As ITemplate
        Get
            Return _content
        End Get
        Set(value As ITemplate)
            _content = value
        End Set
    End Property

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        PopulateFilter()

        If _content IsNot Nothing Then
            _content.InstantiateIn(PlaceHolder1)
        End If
        PlaceHolder2.Visible = _enablecontent
        PlaceHolder3.Visible = _enablefilter
    End Sub

    Public Property EnableFilter() As Boolean
        Get
            Return _enablefilter
        End Get
        Set(value As Boolean)
            _enablefilter = value
        End Set
    End Property

    Public Property EnableContent() As Boolean
        Get
            Return _enablecontent
        End Get
        Set(value As Boolean)
            _enablecontent = value
        End Set
    End Property

    Public ReadOnly Property SearchText() As String
        Get
            Return txtSearch.Text.Trim
        End Get
    End Property

    Protected Sub Search(sender As Object, e As EventArgs)
        RaiseEvent lnkSearchClick(sender, e)
    End Sub

    Public ReadOnly Property SelectTop() As String
        Get
            Dim Retval As String = ""
            'If txtSearch.Text > "" Then
            '    Retval = "TOP 100 Percent"
            'Else
            '    Retval = cboSelectTopNo.SelectedItem.Value
            'End If

            Retval = cboSelectTopNo.SelectedItem.Value

            Return Retval
        End Get
    End Property

    Public ReadOnly Property FilterParam() As String
        Get
            Return ASPxFilterControl1.FilterExpression.ToString
        End Get
    End Property

    Protected Sub lnkFilter_Click(sender As Object, e As EventArgs)
        'Other Information
        If Generic.ToInt(ViewState("IsFilter")) = 0 Then
            panelfilter.Attributes.Remove("class")
            panelfilter.Attributes.Add("class", "panel-collapse collapse in")
            'span4.Attributes.Remove("class")
            'span4.Attributes.Add("class", "glyphicon glyphicon-chevron-down pull-left")
            ViewState("IsFilter") = 1
        Else
            panelfilter.Attributes.Remove("class")
            panelfilter.Attributes.Add("class", "panel-collapse collapse")
            'span4.Attributes.Remove("class")
            'span4.Attributes.Add("class", "glyphicon glyphicon-chevron-right pull-left")
            ViewState("IsFilter") = 0
        End If

        If Generic.ToInt(ViewState("IsFilter")) = 0 Then
            ASPxFilterControl1.FilterExpression = ""
        End If

    End Sub

    Protected Sub lnkClearFilter_Click(sender As Object, e As EventArgs)
        ASPxFilterControl1.FilterExpression = ""
    End Sub

    Public WriteOnly Property FilterName() As String
        Set(value As String)
            hifIndex.Value = value
        End Set
    End Property

    Private Sub PopulateFilter()
        Try
            Dim xFilterName As String = ""
            xFilterName = Generic.ToStr(hifIndex.Value)

            If xFilterName = "" Then
                xFilterName = "EmployeeFilter"
            End If

            Dim dt As DataTable = SQLHelper.ExecuteDataTable("EFilterParam_WebLookup", Generic.ToInt(Session("OnlineUserNo")), Generic.ToInt(Session("xPayLocNo")), xFilterName)
            Dim name As String
            For Each row As DataRow In dt.Rows
                name = Generic.ToStr(row("DisplayName"))
                Select Case Generic.ToStr(row("type"))
                    Case "txt"
                        Dim col As FilterControlTextColumn = TryCast(ASPxFilterControl1.Columns(name), FilterControlTextColumn)
                        If col Is Nothing Then
                            col = New FilterControlTextColumn()
                            col.ColumnType = FilterControlColumnType.String
                            col.DisplayName = name
                            col.PropertyName = Generic.ToStr(row("PropertyName"))
                            ASPxFilterControl1.Columns.Add(col)
                        End If
                    Case "int"
                        Dim col As FilterControlTextColumn = TryCast(ASPxFilterControl1.Columns(name), FilterControlTextColumn)
                        If col Is Nothing Then
                            col = New FilterControlTextColumn()
                            col.ColumnType = FilterControlColumnType.Integer
                            col.DisplayName = name
                            col.PropertyName = Generic.ToStr(row("PropertyName"))
                            ASPxFilterControl1.Columns.Add(col)
                        End If
                    Case "dec"
                        Dim col As FilterControlTextColumn = TryCast(ASPxFilterControl1.Columns(name), FilterControlTextColumn)
                        If col Is Nothing Then
                            col = New FilterControlTextColumn()
                            col.ColumnType = FilterControlColumnType.Decimal
                            col.DisplayName = name
                            col.PropertyName = Generic.ToStr(row("PropertyName"))
                            ASPxFilterControl1.Columns.Add(col)
                        End If
                    Case "date"
                        Dim col As FilterControlDateColumn = TryCast(ASPxFilterControl1.Columns(name), FilterControlDateColumn)
                        If col Is Nothing Then
                            col = New FilterControlDateColumn()
                            col.ColumnType = FilterControlColumnType.DateTime
                            col.DisplayName = name
                            col.PropertyName = Generic.ToStr(row("PropertyName"))
                            ASPxFilterControl1.Columns.Add(col)
                        End If
                    Case "cbo"
                        Dim col As FilterControlComboBoxColumn = TryCast(ASPxFilterControl1.Columns(name), FilterControlComboBoxColumn)
                        If col Is Nothing Then
                            col = New FilterControlComboBoxColumn()
                            col.ColumnType = FilterControlColumnType.[String]
                            col.DisplayName = name
                            col.PropertyName = Generic.ToStr(row("PropertyName"))
                            col.PropertiesComboBox.DropDownStyle = DropDownStyle.DropDown
                            col.PropertiesComboBox.ValueType = GetType(Integer)
                            ASPxFilterControl1.Columns.Add(col)
                        End If
                        'col.PropertiesComboBox.DataSource = SQLHelper.ExecuteDataTable("EColumn_Lookup", -99, Generic.ToStr(row("TableName")))
                        If Generic.ToStr(row("Category")) = "sp" Then
                            col.PropertiesComboBox.DataSource = SQLHelper.ExecuteDataSet(Generic.ToStr(row("TableName")), Generic.ToInt(Session("OnlineUserNo")), Session("xPayLocNo"))
                            col.PropertiesComboBox.TextField = "tdesc"
                            col.PropertiesComboBox.ValueField = "tno"
                        Else
                            col.PropertiesComboBox.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", Generic.ToInt(Session("OnlineUserNo")), Generic.ToStr(row("TableName")), Generic.ToInt(Session("xPayLocNo")), "", "")
                            col.PropertiesComboBox.TextField = "tdesc"
                            col.PropertiesComboBox.ValueField = "tNo"
                        End If

                End Select
            Next
        Catch ex As Exception
        End Try
    End Sub

End Class
