Imports DevExpress.Web
Imports clsLib
Imports System.Data

Partial Class Secured_AppCandidateList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim PayLocNo As Integer

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataSet("EApplicant_JobMatch", UserNo, Generic.ToInt(rbl.SelectedValue)).Tables(0)
            Dim dv As DataView = dt.DefaultView
            dv.RowFilter = ASPxFilterControl1.GetFilterExpressionForDataSet
            If ASPxFilterControl1.GetFilterExpressionForDataSet = "" Then
                dt = dt.Rows.Cast(Of DataRow)().Take(100).CopyToDataTable()
                dv = dt.DefaultView
            End If            
            Dim dt_temp = dv.ToTable(True, "ID", "Fullname", "IsApplicant", "BirthAge")
            dv = dt_temp.DefaultView

            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If

            grdMain.DataSource = dv
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Overrides Sub OnInit(e As EventArgs)
        MyBase.OnInit(e)
        PopulateFilter()
    End Sub

    Protected Sub grdMain_Sorting(sender As Object, e As GridViewSortEventArgs)
        Try
            If ViewState("SortDirection") Is Nothing OrElse ViewState("SortExpression").ToString() <> e.SortExpression Then
                ViewState("SortDirection") = "ASC"
            ElseIf ViewState("SortDirection").ToString() = "ASC" Then
                ViewState("SortDirection") = "DESC"
            ElseIf ViewState("SortDirection").ToString() = "DESC" Then
                ViewState("SortDirection") = "ASC"
            End If
            ViewState("SortExpression") = e.SortExpression
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            grdMain.PageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateFilter()
        Try
            Dim dt As DataTable = SQLHelper.ExecuteDataTable("EColumn")
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
                        col.PropertiesComboBox.TextField = "desc"
                        col.PropertiesComboBox.ValueField = "id"
                        col.PropertiesComboBox.DataSource = SQLHelper.ExecuteDataTable("EColumn_Lookup", -99, Generic.ToStr(row("Table")))
                End Select
            Next
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnPost_Click(sender As Object, e As EventArgs)

        Dim chk As New CheckBox, lnk As New LinkButton, result As Integer = 0        
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            For i As Integer = 0 To Me.grdMain.Rows.Count - 1
                chk = CType(grdMain.Rows(i).FindControl("chkSelect"), CheckBox)
                lnk = CType(grdMain.Rows(i).FindControl("lnk"), LinkButton)
                If chk.Checked = True Then
                    result = result + Generic.ToInt(SQLHelper.ExecuteNonQuery("EApplicant_JobMatch_ToMR", UserNo, Generic.ToInt(Generic.Split(lnk.CommandArgument, 0)), Generic.ToInt(Generic.Split(lnk.CommandArgument, 1)), Generic.ToInt(cboMRNo.SelectedValue), Generic.Split(lnk.CommandArgument, 2)))
                End If
            Next
            If result > 0 Then
                MessageBox.Success("(" & result & ") " & MessageTemplate.SuccesPost, Me)
            Else
                MessageBox.Information("Unable to post transaction.", Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If      
    End Sub

    Protected Sub btnFilter_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateDropDown()
        Me.cboMRNo.DataSource = SQLHelper.ExecuteDataSet("EMR_WebOneLookUp", UserNo)
        Me.cboMRNo.DataTextField = "tdesc"
        Me.cboMRNo.DataValueField = "tno"
        Me.cboMRNo.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))        
        AccessRights.CheckUser(UserNo)            
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnk_Click(sender As Object, e As EventArgs)
        'Info1.xIsApplicant = True
        Dim lnk As New LinkButton
        lnk = sender

        Info1.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
        Info1.xIsApplicant = Generic.Split(lnk.CommandArgument, 1)
        Info1.Show()
    End Sub

End Class
