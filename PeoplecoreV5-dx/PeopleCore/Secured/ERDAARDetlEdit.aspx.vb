Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_ERDAARDetlEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "ERDAARList.aspx")
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
        End If
        PopulateGridDetl()

    End Sub

    Private Sub PopulateGridDetl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDAARDetl_Web", UserNo, TransNo)
            grdDetl.DataSource = dt
            grdDetl.DataBind()
            If Len(Generic.ToStr(ViewState("DAARCode"))) > 0 Then
                lblDetl.Text = "Transaction No. : " & Generic.ToStr(ViewState("DAARCode"))
            Else
                lblDetl.Text = ""
            End If
        Catch ex As Exception

        End Try
    End Sub


#Region "Detail"

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDAARDetl_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
    End Sub

    Protected Sub lnkAddD_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel2")
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    Protected Sub lnkEditD_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DAARDetlNo"}))
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub
    Protected Sub lnkDeleteD_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"DAARDetlNo"})
            Dim i As Integer = 0

            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDAARDetl", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridDetl()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    'Submit record
    Protected Sub btnSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)


        If SaveRecordDetl() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Private Function SaveRecordDetl() As Boolean
        Dim employeeno As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim departmentno As Integer = Generic.ToInt(Me.cboDepartmentNo.SelectedValue)
        Dim present As String = Generic.ToStr(Me.txtPresentAddress.Text)
        Dim permanent As String = Generic.ToStr(Me.txtPermanentAddress.Text)
        Dim remarks As String = Generic.ToStr(Me.txtRemarks.Text)

        If SQLHelper.ExecuteNonQuery("EDAARDetl_WebSave", UserNo, TransNo, Generic.ToInt(txtCodeDeti.Text), employeeno, departmentno, present, permanent, remarks, chkIsForNTE.Checked) > 0 Then
            SaveRecordDetl = True
        Else
            SaveRecordDetl = False
        End If
    End Function
    Protected Sub lnkExportD_Click(sender As Object, e As EventArgs)
        Try
            grdExportD.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub
    'Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim lnk As LinkButton = TryCast(sender, LinkButton)
    '    Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
    '    NewScriptManager.RegisterPostBackControl(lnk)
    'End Sub

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function cboEmployee(prefixText As String, count As Integer, contextKey As String) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()
        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))
        Dim payclassNo As Integer = (HttpContext.Current.Session("PayLastList_PayclassNo"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AC_PayClass", UserNo, prefixText, payclassNo, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

    Protected Sub grdDetl_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdDetl.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdDetl.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

#End Region

End Class
