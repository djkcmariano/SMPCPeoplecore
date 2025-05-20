Imports System.Data
Imports clsLib
Imports System.Math
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting

Partial Class Secured_PayAlphaList_Exempt
    Inherits System.Web.UI.Page


    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EAlphaExempt_Web", UserNo, TransNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "PayAlphaList.aspx", "EAlpha")
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
            PopulateData()
        End If
        PopulateGrid()
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EAlpha_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lnkAdd.Enabled = Not Generic.ToBol(row("IsPosted"))
            lnkSave.Enabled = Not Generic.ToBol(row("IsPosted"))
            lblCode.Text = Generic.ToStr(row("Code"))
            lblApplicableYear.Text = Generic.ToStr(row("ApplicableYear"))
            lblApplicableMonth.Text = Generic.ToStr(row("MonthDesc"))
            lblPayLocDesc.Text = Generic.ToStr(row("PayLocDesc"))
            lblFacilityDesc.Text = Generic.ToStr(row("FacilityDesc"))
        Next
    End Sub

    Private Sub PopulateDataEntitled(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EAlphaExempt_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
        For Each row As DataRow In dt.Rows
            Generic.EnableControls(Me, "Panel2", Generic.ToBol(row("IsEnabled")))
        Next
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PayAlphaList.aspx", "EAlpha") Then
            Generic.ClearControls(Me, "Panel2")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "PayAlphaList.aspx", "EAlpha") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"AlphaExemptNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EAlphaExempt", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayAlphaList.aspx", "EAlpha") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataEntitled(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"AlphaExemptNo"})))

            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecord() As Boolean
        Dim alphaexemptno As Integer = Generic.ToInt(hifAlphaExemptNo.Value)
        Dim employeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))
        Dim taxExemptNo As Integer = Generic.ToInt(cboTaxExemptNo.SelectedValue)

        If SQLHelper.ExecuteNonQuery("EAlphaExempt_WebSave", UserNo, alphaexemptno, TransNo, employeeNo, taxExemptNo) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False

        End If
    End Function

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub


End Class






