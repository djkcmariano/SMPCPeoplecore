Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export


Partial Class Secured_EmpUpdEducList
    Inherits System.Web.UI.Page
    
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EEmployeeUpdEduc_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEduc_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
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
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        If cboTabNo.SelectedValue = 1 Then
            lnkApprove.Visible = True
            lnkDisapprove.Visible = True
            lnkApprove2.Visible = True
            lnkDisapprove2.Visible = True
        ElseIf cboTabNo.SelectedValue = 2 Then
            lnkApprove.Visible = False
            lnkDisapprove.Visible = False
            lnkApprove2.Visible = False
            lnkDisapprove2.Visible = False
        ElseIf cboTabNo.SelectedValue = 3 Or cboTabNo.SelectedValue = 4 Then
            lnkApprove.Visible = False
            lnkDisapprove.Visible = False
            lnkApprove2.Visible = False
            lnkDisapprove2.Visible = False
        End If


        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 12)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim value As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeEducUpdNo"}))
        dt = SQLHelper.ExecuteDataTable("EEmployeeUpdEduc_WebOne", UserNo, value)
        Generic.PopulateData(Me, "Panel1", dt)
        Highlight()
        hiftransNo.Value = value
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnkApprove_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeEducUpdNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteDataSet("EEmployeeUpdEduc_WebUpdate", UserNo, item, 2, PayLocNo)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If

    End Sub

    Protected Sub lnkDisapprove_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeEducUpdNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteDataSet("EEmployeeUpdEduc_WebUpdate", UserNo, item, 3, PayLocNo)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDisapproved, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub

    Private Sub Highlight()
        Dim page As System.Web.UI.Page = If(TryCast(Me, System.Web.UI.Page), Me.Page)
        Dim tempContainer As System.Web.UI.Control
        tempContainer = Generic.FindControlRecursive(Me, "Panel1")
        For Each obj As System.Web.UI.Control In tempContainer.Controls
            If TypeOf obj Is Label Then
                Dim lbl As New Label, lblNew As New Label, lblOld As New Label
                lbl = CType(obj, Label)
                lblNew = Generic.FindControlRecursive(Panel1, Replace(lbl.ID, "_Old", ""))
                lblOld = Generic.FindControlRecursive(Panel1, lbl.ID)
                If lblNew.Text <> lblOld.Text Then
                    lblNew.ForeColor = System.Drawing.Color.Red
                End If
            End If
        Next
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkApprove2_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim id As Integer = Generic.ToInt(hiftransNo.Value)
            If SQLHelper.ExecuteNonQuery("EEmployeeUpdEduc_WebUpdate", UserNo, id, 2, PayLocNo) > 0 Then
                MessageBox.Success(MessageTemplate.SuccessApproved, Me)
                PopulateGrid()
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If

    End Sub

    Protected Sub lnkDisapprove2_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim id As Integer = Generic.ToInt(hiftransNo.Value)
            If SQLHelper.ExecuteNonQuery("EEmployeeUpdEduc_WebUpdate", UserNo, id, 3, PayLocNo) > 0 Then
                MessageBox.Success(MessageTemplate.SuccessDisapproved, Me)
                PopulateGrid()
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub

End Class



