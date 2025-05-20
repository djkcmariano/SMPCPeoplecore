Imports clsLib
Imports System.Data
Imports DevExpress.Web.ASPxPivotGrid

Partial Class Secured_SecCMSTemplateContent
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer
    Dim IsGraph As Boolean
    Dim Datasource As String
    Dim PivotSettings As String

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        If IsGraph Then
            If SQLHelper.ExecuteNonQuery("UPDATE EDashboard SET PivotSetting='" & pvtGrid.SaveLayoutToString & "' WHERE DashboardNo=" & TransNo) > 0 Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            If SQLHelper.ExecuteNonQuery("UPDATE EDashboard SET DashboardContent='" & ASPxHtmlEditor1.Html & "' WHERE DashboardNo=" & TransNo) > 0 Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        End If

    End Sub


    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("SELECT * FROM EDashboard WHERE DashboardNo=" & TransNo)
        For Each row As DataRow In dt.Rows
            IsGraph = Generic.ToBol(row("IsGraph"))
            ASPxHtmlEditor1.Visible = Not IsGraph
            pvtGrid.Visible = IsGraph
            WebChartControl1.Visible = IsGraph
            WebChartControl1.SeriesTemplate.ChangeView(Generic.ToInt(row("GraphType")))
            lblTitle.Text = "Detail No. : " & TransNo.ToString().PadLeft(8, "0") & " (" & Generic.ToStr(row("DashboardDesc")) & ")"
            Datasource = Generic.ToStr(row("Datasource"))
            PivotSettings = Generic.ToStr(row("PivotSetting"))            
        Next
    End Sub

    Private Sub PopulateDataImage()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("SELECT * FROM EDashboard WHERE DashboardNo=" & TransNo)
        For Each row As DataRow In dt.Rows
            ASPxHtmlEditor1.Html = Generic.ToStr(row("DashboardContent"))
        Next
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, Generic.ToStr(Session("xFormName")), Generic.ToStr(Session("xTableName")))    

        PopulateData()

        If IsGraph Then
            PopulatePivotGrid()
        End If

        If Not IsPostBack Then
            PopulateDataImage()
            'PopulateData()
            'ElseIf Not IsPostBack And Not IsCallback Then
            pvtGrid.RetrieveFields(DevExpress.XtraPivotGrid.PivotArea.FilterArea, False)
            pvtGrid.LoadLayoutFromString(PivotSettings)
            'ElseIf Not IsCallback Then
            'pvtGrid.RetrieveFields()
        End If




    End Sub

    Private Sub PopulatePivotGrid()
        Dim strQuery As String
        Dim dt As DataTable                
        strQuery = SQLHelper.ExecuteScalar("EFilteredValuePivot_WebGenerate", UserNo, Datasource, PayLocNo)
        dt = SQLHelper.ExecuteDataTable(strQuery)
        pvtGrid.DataSource = dt
        'pvtGrid.RetrieveFields()
        pvtGrid.DataBind()        
    End Sub

End Class
