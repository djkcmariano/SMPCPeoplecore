Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_BenFidelityBondList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

#Region "********Main********"
    Protected Sub PopulateGrid(Optional IsMain As Boolean = False)
        Try

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EFidelity_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()

            If ViewState("TransNo") = 0 Or IsMain = True Then
                Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"FidelityNo", "Code"})
                ViewState("TransNo") = obj(0)
                lbl.Text = obj(1)
            End If

            PopulateGridDetl()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            Dim IsEnabled As Boolean = True
            dt = SQLHelper.ExecuteDataTable("EFidelity_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupMain", dt)
                Generic.PopulateData(Me, "phDetail", dt)
                IsEnabled = Generic.ToBol(row("IsEnabled"))
            Next
            lnkSave.Enabled = IsEnabled
            lnkSaveDetl.Enabled = IsEnabled
            Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)
            Generic.EnableControls(Me, "phDetail", IsEnabled)



        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        'PopulateGridDetl()
        PopulateGroupBy()



        Select Case Generic.ToInt(cboTabNo.SelectedValue)
            Case 0
                lnkAdd.Visible = True
                lnkAddDetl.Visible = True
                lnkDelete.Visible = True
                lnkDeleteDetl.Visible = True
                lnkPost.Visible = True
                lnkCancel.Visible = False
            Case 1
                lnkAdd.Visible = False
                lnkAddDetl.Visible = False
                lnkDelete.Visible = False
                lnkDeleteDetl.Visible = False
                lnkPost.Visible = False
                lnkCancel.Visible = True
            Case 2
                lnkAdd.Visible = False
                lnkAddDetl.Visible = False
                lnkDelete.Visible = False
                lnkDeleteDetl.Visible = False
                lnkPost.Visible = False
                lnkCancel.Visible = False
            Case Else

        End Select


        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
        Generic.PopulateDropDownList(UserNo, Me, "phDetail", PayLocNo)
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 49)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            Generic.ClearControls(Me, "phDetail")
            Generic.EnableControls(Me, "pnlPopupMain", True)
            Generic.EnableControls(Me, "phDetail", True)

            phDetail.Visible = False

            cboPositionNo.Enabled = False
            cboDepartmentNo.Enabled = False
            lnkSave.Enabled = True
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"FidelityNo"})))
            phDetail.Visible = False

            cboPositionNo.Enabled = False
            cboDepartmentNo.Enabled = False

           

            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkBond_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"FidelityNo"})))
            phDetail.Visible = True

            txtFullName.Enabled = False
            cboPositionNo.Enabled = False
            cboDepartmentNo.Enabled = False
            txtCash.Enabled = False
            txtProperty.Enabled = False
            txtForm.Enabled = False
            txtTotalAccountability.Enabled = False
            txtTotalBond.Enabled = False

            If cboTabNo.SelectedValue = "1" Then
                lnkSave.Enabled = True
                txtCancelledDate.Enabled = True
                txtCancelledDate.ReadOnly = False
            End If

            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"FidelityNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EFidelityDeti", UserNo, "FidelityNo", item)
                Generic.DeleteRecordAudit("EFidelity", UserNo, item)
                i = i + 1
            Next

            If i > 0 Then
                PopulateGrid(True)
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"FidelityNo", "Code"})
        ViewState("TransNo") = obj(0)
        lbl.Text = obj(1)
        PopulateGridDetl()
        PopulateGroupBy()
    End Sub


    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid(True)
    End Sub


    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim FidelityNo As Integer = Generic.ToInt(txtFidelityNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim PositionNo As Integer = Generic.ToInt(cboPositionNo.SelectedValue)
        Dim DepartmentNo As Integer = Generic.ToInt(cboDepartmentNo.SelectedValue)
        Dim RiskNumber As String = Generic.ToStr(txtRiskNumber.Text)
        Dim Cash As Double = Generic.ToDbl(txtCash.Text)
        Dim CashBond As Double = Generic.ToDbl(txtCashBond.Text)
        Dim xProperty As Double = Generic.ToDbl(txtProperty.Text)
        Dim PropertyBond As Double = Generic.ToDbl(txtPropertyBond.Text)
        Dim Form As Double = Generic.ToDbl(txtForm.Text)
        Dim FormBond As Double = Generic.ToDbl(txtFormBond.Text)
        Dim TotalBond As Double = Generic.ToDbl(txtTotalBond.Text)
        Dim TotalBondAmount As Double = Generic.ToDbl(txtTotalBondAmount.Text)
        Dim TotalA As Double = Generic.ToDbl(txtTotalA.Text)
        Dim TotalB As Double = Generic.ToDbl(txtTotalB.Text)
        Dim EffectivityDate As String = Generic.ToStr(txtEffectivityDate.Text)
        Dim CancelledDate As String = Generic.ToStr(txtCancelledDate.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EFidelity_WebValidate", UserNo, FidelityNo, EmployeeNo, PositionNo, DepartmentNo, RiskNumber, Cash, CashBond, xProperty, PropertyBond, Form, FormBond, TotalBond, TotalBondAmount, TotalA, TotalB, EffectivityDate, CancelledDate, Remarks, PayLocNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlMain.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EFidelity_WebSave", UserNo, FidelityNo, EmployeeNo, PositionNo, DepartmentNo, RiskNumber, Cash, CashBond, xProperty, PropertyBond, Form, FormBond, TotalBond, TotalBondAmount, TotalA, TotalB, EffectivityDate, CancelledDate, Remarks, PayLocNo) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

#End Region

#Region "********Details********"
    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataDetl(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"FidelityDetiNo"})))

            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub PopulateDataDetl(id As Int64)
        Try
            Dim dt As DataTable
            Dim IsEnabled As Boolean = False
            Dim FidelityCateNo As Integer = 0
            Dim FidelityTypeNo As Integer = 0
            dt = SQLHelper.ExecuteDataTable("EFidelityDeti_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
                IsEnabled = Generic.ToBol(row("IsEnabled"))
                FidelityCateNo = Generic.ToInt(row("FidelityCateNo"))
                FidelityTypeNo = Generic.ToInt(row("FidelityTypeNo"))
            Next
            PopulateFidelityType(FidelityCateNo)
            Try
                cboFidelityTypeNo.Text = IIf(FidelityTypeNo = 0, "", FidelityTypeNo)
            Catch ex As Exception

            End Try
            Generic.EnableControls(Me, "pnlPopupDetl", IsEnabled)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub PopulateGridDetl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EFidelityDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"FidelityDetiNo", "FidelityNo", "FidelityCateNo"})
            Dim str As String = "", i As Integer = 0
            For Each item() As Object In fieldValues
                Generic.DeleteRecordAudit("EFidelityDeti", UserNo, Generic.ToInt(item(0)))
                SQLHelper.ExecuteNonQuery("EFidelityDeti_WebRecompute", Generic.ToInt(item(1)), Generic.ToInt(item(2)))
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
                PopulateGridDetl()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
            
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub grdDetl_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        PopulateGroupBy()
    End Sub

    Private Sub PopulateGroupBy()
        grdDetl.BeginUpdate()
        Try
            grdDetl.ClearSort()
            grdDetl.GroupBy(grdDetl.Columns("FidelityCateDesc"))
        Finally
            grdDetl.EndUpdate()
        End Try
        grdDetl.ExpandAll()
    End Sub

    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            If Generic.ToInt(ViewState("TransNo")) > 0 Then
                Generic.ClearControls(Me, "pnlPopupDetl")                
                mdlDetl.Show()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub



    Protected Sub lnkSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim FidelityDetiNo As Integer = Generic.ToInt(txtFidelityDetiNo.Text)
            Dim Description As String = Generic.ToStr(txtDescription.Text)
            Dim FidelityCateNo As Integer = Generic.ToInt(cboFidelityCateNo.SelectedValue)
            Dim FidelityTypeNo As Integer = Generic.ToInt(cboFidelityTypeNo.SelectedValue)
            Dim Amount As Double = Generic.ToDbl(txtAmount.Text)

            Dim msg As String
            msg = Generic.ToStr(SQLHelper.ExecuteScalar("EFidelityDeti_WebValidate", UserNo, FidelityDetiNo, Generic.ToInt(ViewState("TransNo")), Description, FidelityCateNo, FidelityTypeNo, Amount, PayLocNo))
            If msg.Length > 0 Then
                MessageBox.Alert(msg, "warning", Me)
                mdlDetl.Show()
                Exit Sub
            End If


            If SQLHelper.ExecuteNonQuery("EFidelityDeti_WebSave", UserNo, FidelityDetiNo, Generic.ToInt(ViewState("TransNo")), Description, FidelityCateNo, FidelityTypeNo, Amount, PayLocNo) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                PopulateGrid()
                'PopulateGridDetl()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub


#End Region

#Region "********Web Service*******"

    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function PopulateHranEmployee(prefixText As String, count As Integer, contextKey As String) As List(Of String)
        Dim items As New List(Of String)()
        Dim _ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim clsbase As New clsBase.clsBaseLibrary
        Dim UserNo As Integer = 0, PayLocNo As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        PayLocNo = (HttpContext.Current.Session("xPayLocNo"))

        _ds = SQLHelper.ExecuteDataSet("EHRAN_WebLookup_AC_Employee", UserNo, prefixText, contextKey, PayLocNo)
        For Each row As DataRow In _ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("FullName")),
                                Generic.ToStr(row("EmployeeNo")) & _
                                "|" & Generic.ToStr(row("PositionNo")) & _
                                "|" & Generic.ToStr(row("DepartmentNo")))
            items.Add(item)
        Next
        _ds.Dispose()
        Return items


    End Function

#End Region

#Region "Update Status"

    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"FidelityNo"})
            Dim str As String = "", i As Integer = 0, retVal As String = ""
            For Each item As Integer In fieldValues
                str = Generic.ToStr(SQLHelper.ExecuteScalar("EFidelity_WebUpdateStat", UserNo, PayLocNo, item, 1))
                'If str.Length > 0 Then
                i = i + 1
                'End If
                retVal = retVal & str
            Next

            If i > 0 And retVal.Length = 0 Then                
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid(True)
            ElseIf retVal.Length > 0 Then
                MessageBox.Alert(retVal, "information", Me)
                PopulateGrid(True)
            ElseIf i = 0 Then
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"FidelityNo"})
            Dim str As String = "", i As Integer = 0, retVal As String = ""
            For Each item As Integer In fieldValues
                str = Generic.ToStr(SQLHelper.ExecuteScalar("EFidelity_WebUpdateStat", UserNo, PayLocNo, item, 2))
                'If str.Length > 0 Then
                i = i + 1
                'End If
                retVal = retVal & str
            Next

            If i > 0 And retVal.Length = 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid(True)
            ElseIf retVal.Length > 0 Then
                MessageBox.Alert(retVal, "information", Me)
                PopulateGrid(True)
            ElseIf i = 0 Then
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub

#End Region


    Protected Sub cboFidelityCateNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateFidelityType(Generic.ToInt(cboFidelityCateNo.SelectedValue))
        mdlDetl.Show()

    End Sub

    Private Sub PopulateFidelityType(FidelityCateNo As Integer)
        Try
            cboFidelityTypeNo.DataSource = SQLHelper.ExecuteDataTable("EFidelityType_WebLookup", UserNo, PayLocNo, FidelityCateNo)
            cboFidelityTypeNo.DataTextField = "tdesc"
            cboFidelityTypeNo.DataValueField = "tNo"
            cboFidelityTypeNo.DataBind()
        Catch ex As Exception

        End Try
    End Sub

End Class

