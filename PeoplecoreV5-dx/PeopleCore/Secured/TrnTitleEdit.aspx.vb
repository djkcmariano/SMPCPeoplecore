Imports clsLib
Imports System.Data

Partial Class Secured_TrnTitleEdit
    Inherits System.Web.UI.Page

    Dim TransNo As Int64
    Dim IsEnabled As Boolean = False
    Dim UserNo As Int64

    Private Sub PopulateData()
        Dim _ds As New DataSet
        Dim _ds2 As New DataSet
        Dim dt As DataTable

        dt = SQLHelper.ExecuteDataTable("ETrnTitle_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))

        AccessRights.CheckUser(UserNo, "TrnTitleList.aspx", "ETrnTitle")

        If TransNo = 0 Then
            ViewState("IsEnabled") = True
        End If

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowView, "TrnTitleList.aspx", "ETrnTitle") = False Then : Response.Redirect("~/") : End If

        If Not IsPostBack Then
            Generic.PopulateDropDownList(Generic.ToInt(UserNo), Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, Session("xPayLocNo"))
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try
            PopulateData()
            PopulateTabHeader()
        End If

        EnabledControls()

    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnTitle_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "TrnTitleList.aspx", "ETrnTitle") Then
            If SaveRecord() Then
                Dim url As String = "TrnTitleEdit.aspx?id=" & TransNo & "&tModify=False"
                If TransNo > 0 Then
                    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                Else
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                End If

                ViewState("IsEnabled") = False
                EnabledControls()
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "TrnTitleList.aspx", "ETrnTitle") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)

        'If IsEnabled = True Then
        '    If Generic.ToInt(cboTrnTypeNo.SelectedValue) = 1 Then
        '        txtNoOfMonths.Enabled = False
        '        txtServiceContract.Enabled = True
        '        cboTrnRetakenNo.Enabled = False
        '    ElseIf Generic.ToInt(cboTrnTypeNo.SelectedValue) = 2 Then
        '        txtNoOfMonths.Enabled = True
        '        txtServiceContract.Enabled = True
        '        cboTrnRetakenNo.Enabled = True
        '    Else
        '        txtNoOfMonths.Enabled = False
        '        txtServiceContract.Enabled = False
        '        cboTrnRetakenNo.Enabled = False
        '    End If
        'End If

        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
    End Sub

    Private Function SaveRecord() As Boolean

        Dim trntitlecode As String = Generic.CheckDBNull(Me.txtTrnTitleCode.Text, Generic.enumObjectType.StrType)
        Dim trntitledesc As String = Generic.CheckDBNull(Me.txtTrnTitleDesc.Text, Generic.enumObjectType.StrType)
        Dim description As String = Generic.CheckDBNull(Me.txtDescription.Text, Generic.enumObjectType.StrType)
        Dim objectives As String = Generic.CheckDBNull(Me.txtObjectives.Text, Generic.enumObjectType.StrType)
        Dim trncategoryno As Integer = Generic.CheckDBNull(Me.cboTrnCategoryNo.SelectedValue, Generic.enumObjectType.IntType)
        Dim cost As Double = Generic.CheckDBNull(Me.txtCost.Text, Generic.enumObjectType.DblType)
        Dim hrs As Double = Generic.CheckDBNull(Me.txtHrs.Text, Generic.enumObjectType.DblType)
        Dim trntypeno As Integer = Generic.CheckDBNull(Me.cboTrnTypeNo.SelectedValue, Generic.enumObjectType.IntType)
        Dim noofmonths As Double = Generic.CheckDBNull(Me.txtNoOfMonths.Text, Generic.enumObjectType.DblType)
        Dim servicecontract As Double = Generic.CheckDBNull(Me.txtServiceContract.Text, Generic.enumObjectType.DblType)
        Dim trnretakenno As Integer = Generic.CheckDBNull(Me.cboTrnRetakenNo.SelectedValue, Generic.enumObjectType.IntType)
        Dim remarks As String = Generic.CheckDBNull(Me.txtRemarks.Text, Generic.enumObjectType.StrType)

        Dim ds As New DataSet

        ds = SQLHelper.ExecuteDataSet("ETrnTitle_WebSave", UserNo, TransNo, trntitlecode, trntitledesc, description, objectives, trncategoryno, cost, hrs, trntypeno, noofmonths, servicecontract, trnretakenno, remarks, Generic.ToInt(cboPayLocNo.SelectedValue), chkIsArchived.Checked)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                TransNo = Generic.ToInt(ds.Tables(0).Rows(0)("RetVal"))
            End If
        End If

        If TransNo > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If
    End Function

    Protected Sub cboTrnTypeNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        'If Generic.ToInt(cboTrnTypeNo.SelectedValue) = 1 Then
        '    txtNoOfMonths.Enabled = False
        '    txtServiceContract.Enabled = True
        '    cboTrnRetakenNo.Enabled = False

        '    txtNoOfMonths.Text = ""
        '    cboTrnRetakenNo.Text = ""
        'ElseIf Generic.ToInt(cboTrnTypeNo.SelectedValue) = 2 Then
        '    txtNoOfMonths.Enabled = True
        '    txtServiceContract.Enabled = True
        '    cboTrnRetakenNo.Enabled = True
        'Else
        '    txtNoOfMonths.Enabled = False
        '    txtServiceContract.Enabled = False
        '    cboTrnRetakenNo.Enabled = False

        '    txtNoOfMonths.Text = ""
        '    txtServiceContract.Text = ""
        '    cboTrnRetakenNo.Text = ""
        'End If
    End Sub
End Class
