Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports DevExpress.XtraCharts

Partial Class Secured_SecCMSTemplateList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        Dim tStatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
        If tStatus = 0 Then
            lnkDelete.Visible = False
            lnkArchive.Visible = True
        ElseIf tStatus = 1 Then
            lnkDelete.Visible = True
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        Else
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        End If



        Try
            _dt = SQLHelper.ExecuteDataTable("EBenefitPackage_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
            grdMain.DataSource = _dt
            grdMain.DataBind()

            If Generic.ToInt(ViewState("TransNo")) = 0 Then
                Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"BenefitPackageNo", "Code"})
                ViewState("TransNo") = obj(0)
                lblDetl.Text = obj(1)
            End If
            PopulateGridDetl()

        Catch ex As Exception

        End Try




    End Sub
    Protected Sub PopulateDropdown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 14)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropdown()
        End If

        PopulateGrid()
        'Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        'Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub


#Region "********Main*******"


    'Submit record
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As DataTable
        Dim RetVal As Integer = 0, xMessage As String = "", alertType As String = ""
        Dim tno As Integer = Generic.ToInt(Me.txtBenefitPackageNo.Text)
        Dim BenefitPackageCode As String = Generic.ToStr(Me.txtBenefitPackageCode.Text)
        Dim BenefitPackageDesc As String = Generic.ToStr(Me.txtBenefitPackageDesc.Text)
        Dim Package As String = ""

        dt = SQLHelper.ExecuteDataTable("EBenefitPackage_WebSave", UserNo, tno, BenefitPackageCode, BenefitPackageDesc, Package, Generic.ToBol(txtIsArchived.Checked), Generic.ToInt(cboPayLocNo.SelectedValue))
        For Each row As DataRow In dt.Rows
            TransNo = Generic.ToInt(row("Retval"))
            RetVal = True
        Next

        If RetVal = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopup")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try
            btnSave.Enabled = True
            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BenefitPackageNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EBenefitPackage", UserNo, "BenefitPackageNo", CType(item, Integer))
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

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton ', i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitPackageNo", "IsEnabled"})
                Dim iNo As Integer = Generic.ToInt(obj(0))
                Dim IsEnabled As Boolean = Generic.ToBol(obj(1))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EBenefitPackage_WebOne", UserNo, Generic.ToInt(iNo))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
                    Generic.PopulateData(Me, "pnlPopup", dt)
                Next
                btnSave.Enabled = IsEnabled
                Try
                    cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                    cboPayLocNo.DataTextField = "tdesc"
                    cboPayLocNo.DataValueField = "tNo"
                    cboPayLocNo.DataBind()

                Catch ex As Exception

                End Try
                mdlShow.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub


    Protected Sub lnkArchive_Click(sender As Object, e As EventArgs)

        Dim dt As DataTable, tProceed As Boolean = False
        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "BenefitPackageNo"))
                dt = SQLHelper.ExecuteDataTable("ETableReferrence_WebArchived", UserNo, "EBenefitPackage", item, 1, PayLocNo)
                For Each row As DataRow In dt.Rows
                    tProceed = Generic.ToBol(row("tProceed"))
                Next
                grdMain.Selection.UnselectRow(j)
                i = i + 1
            End If
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") transaction(s) successfully archived.", Me)
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If


    End Sub

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitPackageNo", "Code"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        PopulateGridDetl()
    End Sub

#End Region


#Region "********Detail Check All********"


    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs)
        If e.ButtonType <> ColumnCommandButtonType.SelectCheckbox Then
            Return
        End If
        Dim rowEnabled As Boolean = getRowEnabledStatus(e.VisibleIndex)
        e.Enabled = rowEnabled

        'If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
        '    Dim isSelected As Boolean = Convert.ToBoolean(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
        '    If isSelected Then

        '        grdMain.Selection.SetSelection(e.VisibleIndex, True)

        '    End If
        'End If
    End Sub
    Private Function getRowEnabledStatus(ByVal VisibleIndex As Integer) As Boolean
        Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(VisibleIndex, "IsEnabled"))
        If value = True Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Sub cbCheckAll_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim cb As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)
        cb.ClientSideEvents.CheckedChanged = String.Format("cbCheckAll_CheckedChanged")
        cb.Checked = False
        Dim count As Integer = 0
        Dim startIndex As Integer = grdMain.PageIndex * grdMain.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdMain.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)

        For i As Integer = startIndex To endIndex - 1
            If grdMain.Selection.IsRowSelected(i) Then
                count = count + 1
            End If
        Next i

        If count > 0 Then
            cb.Checked = True
        End If

    End Sub
    Protected Sub gridMain_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        Boolean.TryParse(e.Parameters, False)

        Dim startIndex As Integer = grdMain.PageIndex * grdMain.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdMain.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)
        For i As Integer = startIndex To endIndex - 1
            Dim rowEnabled As Boolean = getRowEnabledStatus(i)
            If rowEnabled AndAlso e.Parameters = "true" Then
                grdMain.Selection.SelectRow(i)
            Else
                grdMain.Selection.UnselectRow(i)
            End If
        Next i

    End Sub

#End Region


#Region "Detail"
    Private Sub PopulateGridDetl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBenefitPackageDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"BenefitPackageDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EBenefitPackageDeti", UserNo, item)
                i = i + 1
            Next

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

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitPackageDetiNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EBenefitPackageDeti_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
                    Generic.PopulateData(Me, "pnlPopupDetl", dt)
                Next
                mdlShowDetl.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
            mdlShowDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub btnSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim BenefitPackageDetiNo As Integer = Generic.ToInt(txtBenefitPackageDetiNo.Text)
        Dim BenefitPackageDetiCode As String = ""
        Dim BenefitPackageDetiDesc As String = Generic.ToStr(txtBenefitPackageDetiDesc.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)
        Dim BenefitPackageTypeNo As Integer = Generic.ToInt(cboBenefitPackageTypeNo.SelectedValue)
        Dim OrderLevel As Integer = Generic.ToInt(txtOrderLevel.Text)

        If SQLHelper.ExecuteNonQuery("EBenefitPackageDeti_WebSave", UserNo, BenefitPackageDetiNo, BenefitPackageDetiCode, BenefitPackageDetiDesc, Generic.ToInt(ViewState("TransNo")), Remarks, BenefitPackageTypeNo, OrderLevel) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Protected Sub grdDetl_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        PopulateGroupBy()
    End Sub

    Private Sub PopulateGroupBy()
        grdDetl.BeginUpdate()
        Try
            grdDetl.ClearSort()
            grdDetl.GroupBy(grdDetl.Columns("BenefitPackageTypeDesc"))
        Finally
            grdDetl.EndUpdate()
        End Try
        grdDetl.ExpandAll()
    End Sub

    Protected Sub grdDetl_CustomColumnDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewColumnDisplayTextEventArgs)
        If e.Column.FieldName = "OrderLevel" Then
            Dim groupLevel As Integer = grdDetl.GetRowLevel(e.VisibleRowIndex)
            If groupLevel = e.Column.GroupIndex Then
                Dim city As String = grdDetl.GetRowValues(e.VisibleRowIndex, "OrderLevel").ToString()
                Dim country As String = grdDetl.GetRowValues(e.VisibleRowIndex, "BenefitPackageTypeDesc").ToString()
                e.DisplayText = city & " (" & country & ")"
            End If
        End If

    End Sub

    Protected Sub grdDetl_CustomColumnSort(ByVal sender As Object, ByVal e As CustomColumnSortEventArgs)
        If e.Column IsNot Nothing And e.Column.FieldName = "BenefitPackageTypeDesc" Then
            Dim country1 As Object = e.GetRow1Value("OrderLevel")
            Dim country2 As Object = e.GetRow2Value("OrderLevel")
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



End Class
















