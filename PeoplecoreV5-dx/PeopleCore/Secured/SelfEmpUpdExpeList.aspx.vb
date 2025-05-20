Imports System.Data
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web

Partial Class Secured_EmpUpdExpeList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0
    Dim EmployeeNo As Integer = 0

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Try
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EEmployeeUpdExpe_WebSelf", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeUpdExpe_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        EmployeeNo = Generic.ToInt(Session("EmployeeNo"))
        Tab.TransactionNo = EmployeeNo
        Permission.IsAuthenticated()

        If Not IsPostBack Then
            PopulateTabHeader()
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, EmployeeNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(EmployeeNo) & "&tIndex=2"

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
        Dim value As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeExpeUpdNo"}))
        dt = SQLHelper.ExecuteDataTable("EEmployeeUpdExpe_WebOne", UserNo, value)
        Generic.PopulateData(Me, "Panel1", dt)
        Highlight()
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnkApprove_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeExpeUpdNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("EEmployeeUpdExpe_WebUpdate", UserNo, item, 2, PayLocNo)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If

    End Sub

    Protected Sub lnkDisapprove_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeExpeUpdNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteDataSet("EEmployeeUpdExpe_WebUpdate", UserNo, item, 3, PayLocNo)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
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

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeExpeUpdNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EEmployeeExpeUpd", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()

    End Sub

End Class




