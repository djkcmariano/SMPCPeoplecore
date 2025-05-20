Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PayJVDefEdit_Detail
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim drcrNo As Integer = 0
    Private Function populateData_Main() As Integer
        Dim ds As DataSet, retVal As Integer = 0
        ds = SQLHelper.ExecuteDataSet("EJVDef_WebOne", UserNo, TransNo)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                retVal = Generic.ToInt(ds.Tables(0).Rows(0)("drcrNo"))
                lblCode.Text = Generic.ToStr(ds.Tables(0).Rows(0)("Code"))
                lblDescription.Text = Generic.ToStr(ds.Tables(0).Rows(0)("AccntDesc"))
                lbldebit_credit.Text = Generic.ToStr(ds.Tables(0).Rows(0)("DRCRDesc"))
            End If
        End If
        Return retVal
    End Function
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "PayJVDefList.aspx", "EJVDef")

        If Not IsPostBack Then
            'PopulateTabHeader()
            drcrNo = populateData_Main()

        End If

        PopulateIncome()
        Generic.PopulateDXGridFilter(grdIncome, UserNo, PayLocNo)

        PopulateDeduct()
        Generic.PopulateDXGridFilter(grdDeduct, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
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

    'Private Sub PopulateTabHeader()
    '    Dim dt As DataTable
    '    dt = SQLHelper.ExecuteDataTable("EHolidayTabHeader", UserNo, TransNo)
    '    For Each row As DataRow In dt.Rows
    '        lbl.Text = Generic.ToStr(row("Display"))
    '        'lnkAdd.Enabled = Generic.ToBol(row("IsEnabled"))
    '    Next
    'End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PayJVDefList.aspx", "EJVDef") Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtJVDefDetiNo.Text)
            Dim payincometypeno As Integer = Generic.ToInt(Me.cboPayIncomeTypeNo.SelectedValue)
            Dim paydeducttypeno As Integer = Generic.ToInt(Me.cboPayDeductTypeNo.SelectedValue)

            If SQLHelper.ExecuteNonQuery("EJVDefDeti_WebSave", UserNo, tno, TransNo, payincometypeno, paydeducttypeno) > 0 Then
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

#Region "*****Income Type*******"


    Protected Sub PopulateIncome()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EJVDefDeti_Web", UserNo, TransNo, 1)
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
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PayJVDefList.aspx", "EJVDef") Then
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
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayJVDefList.aspx", "EJVDef") Then
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
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "PayJVDefList.aspx", "EJVDef") Then
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
            dt = SQLHelper.ExecuteDataTable("EJVDefDeti_Web", UserNo, TransNo, 2)
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
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PayJVDefList.aspx", "EJVDef") Then
            Generic.ClearControls(Me, "Panel1")
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
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayJVDefList.aspx", "EJVDef") Then
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
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "PayJVDefList.aspx", "EJVDef") Then
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
