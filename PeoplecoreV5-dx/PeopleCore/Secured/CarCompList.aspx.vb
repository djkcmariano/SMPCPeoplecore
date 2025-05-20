Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_CarCompList
    Inherits System.Web.UI.Page



    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EComp_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If ViewState("TransNo") = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"CompNo", "Code"})
            ViewState("TransNo") = obj(0)
            lblDetl.Text = obj(1)
        End If

        PopulateGridDetl()

    End Sub

    Private Sub PopulateGridDetl()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ECompDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Private Sub PopulateCombo()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 14)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateCombo()
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateGroupBy()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub cboTab_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            PopulateGrid(True)

        Catch ex As Exception
        End Try
    End Sub

#Region "********Main*******"
    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"CompNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EComp_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "Panel1", dt)
                Next
                ModalPopupExtender1.Show()


            Else
                MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"CompNo", "Code"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        PopulateGridDetl()
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        TransNo = Generic.ToInt(ViewState("TransNo"))

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"CompNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EComp", UserNo, item)
                Generic.DeleteRecordAuditCol("ECompDeti", UserNo, "CompNo", item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                PopulateGridDetl()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)

        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Private Function SaveRecord() As Integer
        Dim RetVal As Integer = 0
        Dim dt As DataTable
        Try
            dt = SQLHelper.ExecuteDataTable("EComp_WebSave", UserNo, Generic.ToInt(txtCode.Text), txtCompCode.Text, txtCompDesc.Text, Generic.ToInt(cboCompTypeNo.SelectedValue), Generic.ToInt(cboCompClusterNo.SelectedValue), txtCompDetl.Text, Generic.ToInt(txtOrderBy.Text), Generic.ToBol(chkIsArchived.Checked), PayLocNo)
            For Each row As DataRow In dt.Rows
                RetVal = Generic.ToInt(row("RetVal"))
            Next
        Catch ex As Exception

        End Try
        Return RetVal
    End Function


    Protected Sub grdMain_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        PopulateGroupBy()
    End Sub

    Private Sub PopulateGroupBy()
        grdMain.BeginUpdate()
        Try
            grdMain.ClearSort()
            grdMain.GroupBy(grdMain.Columns("CompTypeDesc"))
        Finally
            grdMain.EndUpdate()
        End Try
        grdMain.ExpandAll()
    End Sub

    Protected Sub grdMain_CustomColumnDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewColumnDisplayTextEventArgs)
        If e.Column.FieldName = "OrderBy" Then
            Dim groupLevel As Integer = grdMain.GetRowLevel(e.VisibleRowIndex)
            If groupLevel = e.Column.GroupIndex Then
                Dim city As String = grdMain.GetRowValues(e.VisibleRowIndex, "OrderBy").ToString()
                Dim country As String = grdMain.GetRowValues(e.VisibleRowIndex, "CompTypeDesc").ToString()
                e.DisplayText = city & " (" & country & ")"
            End If
        End If

    End Sub

    Protected Sub grdMain_CustomColumnSort(ByVal sender As Object, ByVal e As CustomColumnSortEventArgs)
        If e.Column IsNot Nothing And e.Column.FieldName = "CompTypeDesc" Then
            Dim country1 As Object = e.GetRow1Value("OrderBy")
            Dim country2 As Object = e.GetRow2Value("OrderBy")
            Dim res As Integer = Comparer.Default.Compare(country1, country2)
            If res = 0 Then
                Dim city1 As Object = e.Value1
                Dim city2 As Object = e.Value2
                res = Comparer.Default.Compare(city1, city2)
            End If
            e.Result = res
            e.Handled = True
        End If
    End Sub

#End Region

#Region "********Detail********"

    Protected Sub lnkDeleteDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        TransNo = Generic.ToInt(ViewState("TransNo"))
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"CompDetiNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ECompDeti", UserNo, item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGridDetl()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If

    End Sub
    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub
    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel2")
            ModalPopupExtender2.Show()
        End If
    End Sub
    Private Sub PopulateDataDetl(ID As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ECompDeti_WebOne", UserNo, ID)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel2", dt)
        Next
    End Sub
    Protected Sub btnEditDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New LinkButton
            ib = sender
            PopulateDataDetl(Generic.ToInt(ib.CommandArgument))
            ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
        TransNo = Generic.ToInt(ViewState("TransNo"))

        Dim Retval As Boolean = False
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("ECompDeti_WebValidate", UserNo, Generic.ToInt(txtCodeDetl.Text), Generic.ToInt(ViewState("TransNo")), Generic.ToInt(cboCompScaleNo.SelectedValue), txtAnchor.Text)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            ModalPopupExtender2.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("ECompDeti_WebSave", UserNo, Generic.ToInt(txtCodeDetl.Text), Generic.ToInt(ViewState("TransNo")), Generic.ToInt(cboCompScaleNo.SelectedValue), txtAnchor.Text) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If

    End Sub


#End Region

End Class
