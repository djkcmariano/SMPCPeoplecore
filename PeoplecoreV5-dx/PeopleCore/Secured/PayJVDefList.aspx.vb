Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PayJVDefList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid(True)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateIncome()
        Generic.PopulateDXGridFilter(grdIncome, UserNo, PayLocNo)

        PopulateDeduct()
        Generic.PopulateDXGridFilter(grdDeduct, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 14)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub
#Region "********Main*******"


    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EJVDef_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If Generic.ToInt(ViewState("TransNo")) = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"JVDefNo", "Code"})
            ViewState("TransNo") = Generic.ToInt(obj(0))
            ViewState("Code") = Generic.ToStr(obj(1))
        End If

        lblIncome.Text = Generic.ToStr(ViewState("Code"))

        PopulateIncome()
        PopulateDeduct()

    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"JVDefNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EJVDefDeti", UserNo, "JVDefNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EJVDef", UserNo, CType(item, Integer))
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

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Response.Redirect("~/secured/PayJVDefEdit.aspx?id=" & Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"JVDefNo"})))

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/PayJVDefEdit.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub
    Protected Sub lnkArchive_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Count As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PositionNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("zRow_Archived", UserNo, "EJVDef", item)
                Count = Count + 1
            Next

            If Count > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + Count.ToString + ") record(s) has been successfully archived.", Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If
    End Sub
#End Region


#Region "********Details*******"

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"JVDefNo", "Code"})
        ViewState("TransNo") = Generic.ToInt(obj(0))
        ViewState("Code") = Generic.ToStr(obj(1))
        PopulateIncome()
        PopulateDeduct()

    End Sub


    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EJVDefDeti_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
                cboPayIncomeTypeNo.Text = Generic.ToInt(row("PayIncomeTypeNo"))
                cboPayDeductTypeNo.Text = Generic.ToInt(row("PayDeductTypeNo"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim JVDefDetiNo As Integer = Generic.ToInt(Me.txtJVDefDetiNo.Text)
            Dim payincometypeno As Integer = Generic.ToInt(Me.cboPayIncomeTypeNo.SelectedValue)
            Dim paydeducttypeno As Integer = Generic.ToInt(Me.cboPayDeductTypeNo.SelectedValue)

            '//validate start here
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EJVDefDeti_WebValidate", UserNo, JVDefDetiNo, Generic.ToInt(ViewState("TransNo")), payincometypeno, paydeducttypeno, PayLocNo)

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("tProceed"))
                messagedialog = Generic.ToStr(rowx("xMessage"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                ModalPopupExtender1.Show()
                Exit Sub
            End If

            If SQLHelper.ExecuteNonQuery("EJVDefDeti_WebSave", UserNo, JVDefDetiNo, Generic.ToInt(ViewState("TransNo")), payincometypeno, paydeducttypeno, PayLocNo) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                PopulateIncome()
                PopulateDeduct()
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

#End Region


#Region "*****Income Type*******"


    Protected Sub PopulateIncome()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EJVDefDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")), 1)
            grdIncome.DataSource = dt
            grdIncome.DataBind()


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkExportIncome_Click(sender As Object, e As EventArgs)
        Try
            grdExportIncome.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub


    Protected Sub lnkAddIncome_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            Try

                'cboPayIncomeTypeNo.DataSource = SQLHelper.ExecuteDataSet("xTable_Lookup", UserNo, "EPayIncomeType", PayLocNo, "tDesc", "")
                'cboPayIncomeTypeNo.DataTextField = "tDesc"
                'cboPayIncomeTypeNo.DataValueField = "tNo"
                'cboPayIncomeTypeNo.DataBind()
                cboPayIncomeTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayIncomeType_WebLookup", UserNo, PayLocNo)
                cboPayIncomeTypeNo.DataTextField = "tDesc"
                cboPayIncomeTypeNo.DataValueField = "tNo"
                cboPayIncomeTypeNo.DataBind()

            Catch ex As Exception

            End Try
            divdeduct.Style.Add("display", "none")
            divincome.Style.Remove("display")
            cboPayIncomeTypeNo.CssClass = "form-control required"
            cboPayDeductTypeNo.CssClass = "form-control"
            cboPayDeductTypeNo.Text = ""
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkEditIncome_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Try

                'cboPayIncomeTypeNo.DataSource = SQLHelper.ExecuteDataSet("xTable_Lookup", UserNo, "EPayIncomeType", PayLocNo, "tDesc", "")
                'cboPayIncomeTypeNo.DataTextField = "tDesc"
                'cboPayIncomeTypeNo.DataValueField = "tNo"
                'cboPayIncomeTypeNo.DataBind()
                cboPayIncomeTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayIncomeType_WebLookup", UserNo, PayLocNo)
                cboPayIncomeTypeNo.DataTextField = "tDesc"
                cboPayIncomeTypeNo.DataValueField = "tNo"
                cboPayIncomeTypeNo.DataBind()
            Catch ex As Exception

            End Try

            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"JVDefDetiNo"})))
            divdeduct.Style.Add("display", "none")
            divincome.Style.Remove("display")
            cboPayIncomeTypeNo.CssClass = "form-control required"
            cboPayDeductTypeNo.CssClass = "form-control"
            cboPayDeductTypeNo.Text = ""
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDeleteIncome_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdIncome.GetSelectedFieldValues(New String() {"JVDefDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EJVDefDeti", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateIncome()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

#End Region


#Region "*****Deduct Type*******"


    Protected Sub PopulateDeduct()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EJVDefDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")), 2)
            grdDeduct.DataSource = dt
            grdDeduct.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkExportDeduct_Click(sender As Object, e As EventArgs)
        Try
            grdExportDeduct.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub


    Protected Sub lnkAddDeduct_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            Try
                'cboPayDeductTypeNo.DataSource = SQLHelper.ExecuteDataSet("xTable_Lookup", UserNo, "EPayDeductType", PayLocNo, "tDesc", "")
                'cboPayDeductTypeNo.DataTextField = "tDesc"
                'cboPayDeductTypeNo.DataValueField = "tNo"
                'cboPayDeductTypeNo.DataBind()
                Try
                    cboPayDeductTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayDeductType_WebLookup", UserNo, PayLocNo)
                    cboPayDeductTypeNo.DataTextField = "tDesc"
                    cboPayDeductTypeNo.DataValueField = "tNo"
                    cboPayDeductTypeNo.DataBind()
                Catch ex As Exception

                End Try
            Catch ex As Exception

            End Try
            divincome.Style.Add("display", "none")
            divdeduct.Style.Remove("display")
            cboPayIncomeTypeNo.CssClass = "form-control"
            cboPayDeductTypeNo.CssClass = "form-control required"
            cboPayIncomeTypeNo.Text = ""
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkEditDeduct_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Try
                'cboPayDeductTypeNo.DataSource = SQLHelper.ExecuteDataSet("xTable_Lookup", UserNo, "EPayDeductType", PayLocNo, "tDesc", "")
                'cboPayDeductTypeNo.DataTextField = "tDesc"
                'cboPayDeductTypeNo.DataValueField = "tNo"
                'cboPayDeductTypeNo.DataBind()
                cboPayDeductTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayDeductType_WebLookup", UserNo, PayLocNo)
                cboPayDeductTypeNo.DataTextField = "tDesc"
                cboPayDeductTypeNo.DataValueField = "tNo"
                cboPayDeductTypeNo.DataBind()
            Catch ex As Exception

            End Try

            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"JVDefDetiNo"})))
            divincome.Style.Add("display", "none")
            divdeduct.Style.Remove("display")
            cboPayIncomeTypeNo.CssClass = "form-control"
            cboPayDeductTypeNo.CssClass = "form-control required"
            cboPayIncomeTypeNo.Text = ""
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDeleteDeduct_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDeduct.GetSelectedFieldValues(New String() {"JVDefDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EJVDefDeti", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateDeduct()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

#End Region

End Class



