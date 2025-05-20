Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PY
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPY_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateGridDetl(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPYDeti_Web", UserNo, id)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 4)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
        Try
            cboPayclassNo.DataSource = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", UserNo, PayLocNo)
            cboPayclassNo.DataTextField = "tDesc"
            cboPayclassNo.DataValueField = "tno"
            cboPayclassNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkSearchDetl_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("onlineuserno"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        AccessRights.CheckUser(UserNo)

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)

        If Not IsPostBack Then
            PopulateDropDown()
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

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PYNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EPYDeti", UserNo, "PYNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EPY", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PYDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPYDeti", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))

            If i > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PYNo", "PayClassNo"})
        ViewState("TransNo") = obj(0)
        ViewState("PayClassNo") = obj(1)
        lblDetl.Text = "<b>Transaction No. : " & obj(1) & "</b>"
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton ', i As Integer
                lnk = sender
                'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                'i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PYNo"}))
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PYNo", "IsEnabled"})
                Dim iNo As Integer = Generic.ToInt(obj(0))
                Dim IsEnabled As Boolean = Generic.ToBol(obj(1))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPY_WebOne", UserNo, Generic.ToInt(iNo))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                Next

                mdlMain.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PYDetiNo"}))
                Response.Redirect("py_detl.aspx?pyno=" & Generic.ToStr(ViewState("TransNo")) & "&Id=" & i.ToString & "&PayclassNo=" & Generic.ToInt(ViewState("PayClassNo")))

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            lnkSave.Enabled = True
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            If Generic.ToInt(ViewState("TransNo")) = 0 Then
                MessageBox.Warning("Please select transaction no.", Me)
            Else
                Response.Redirect("py_detl.aspx?pyno=" & Generic.ToStr(ViewState("TransNo")) & "&Id=0&PayclassNo=" & Generic.ToInt(ViewState("PayClassNo")))
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    'Submit record
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ds As New DataSet
        Dim RetVal As Integer = 0, xMessage As String = "", alertType As String = ""

        SaveRecord()

    End Sub


    Private Sub SaveRecord()

        Dim PayincomeTypeNO As Integer = Generic.ToInt(cboPayIncomeTypeNo.SelectedValue)
        Dim applicableMonth As Integer = 0
        Dim applicableYear As Integer = 0
        Dim PayTypeNo As Integer = Generic.ToInt(cboPayTypeNo.SelectedValue)
        Dim payclassNO As Integer = Generic.ToInt(cboPayclassNo.SelectedValue)
        Dim datefrom As String = Generic.ToStr(txtDateFrom.Text)
        Dim dateTo As String = Generic.ToStr(txtDateTo.Text)

        Dim dt As New DataTable, error_num As Integer = 0, error_message As String = "", retVal As Boolean = False
        dt = SQLHelper.ExecuteDataTable("EPY_WebSave", UserNo, Generic.ToInt(txtPYNo.Text), PayincomeTypeNO, applicableMonth, applicableYear, PayTypeNo, payclassNO, PayLocNo, datefrom, dateTo)
        For Each row As DataRow In dt.Rows
            retVal = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                retVal = False
            End If

        Next
        If retVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If retVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub
    'Submit record
   

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

End Class

