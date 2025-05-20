Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_EmpClearance
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TabNo As Integer = 0
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Dim URL As String
        URL = Generic.GetFirstTab(0)
        If URL <> "" Then
            Response.Redirect(URL)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim URL As String
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeClearanceNo"})))
        If URL <> "" Then
            Response.Redirect(URL)
        End If
    End Sub


    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeClearanceNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeClearance", UserNo, item)
                SQLHelper.ExecuteNonQuery("EEmployeeClearance_WebDelete", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)                
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub cboTabNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 48)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()            
        End If

        PopulateGrid()

    End Sub

    Private Sub PopulateGrid()

        'lnkAdd.Enabled = True
        ''lnkDelete.Enabled = True

        'If TabNo = 0 Then
        '    TabNo = 1
        'End If

        'If TabNo <> 1 Then
        '    lnkAdd.Enabled = False
        '    'lnkDelete.Enabled = False            
        'End If

        Select Case Generic.ToInt(cboTabNo.SelectedValue)
            Case 0
                lnkAdd.Visible = True
                lnkDelete.Visible = True
                lnkPost.Visible = False
            Case 1
                lnkAdd.Visible = False
                lnkDelete.Visible = False
                lnkPost.Visible = True            
            Case Else
                lnkAdd.Visible = False
                lnkDelete.Visible = False
                lnkPost.Visible = False
        End Select


        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EEmployeeClearance_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeClearanceNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                i = i + Generic.ToInt(SQLHelper.ExecuteNonQuery("EEmployeeClearance_WebPost", UserNo, PayLocNo, item))
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccesPost, Me)
                PopulateGrid()
            Else
                MessageBox.Alert(MessageTemplate.NoSelectedTransaction, "warning", Me)
            End If
            
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If

    End Sub

End Class
