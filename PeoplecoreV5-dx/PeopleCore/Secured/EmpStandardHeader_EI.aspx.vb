Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_EmpStandardHeader_EI
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0


#Region "Main"

    Protected Sub PopulateGrid()
        Try
            PopulateButtom(Generic.ToInt(cboTabNo.SelectedValue))

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEI_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEI_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopup", dt)
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
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", Generic.ToInt("xPayLocNo"))
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 46)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboApplicantStandardHeaderNo.DataSource = SQLHelper.ExecuteDataSet("EApplicantStandardHeader_WebLookup", UserNo, 0, 2, PayLocNo)
            cboApplicantStandardHeaderNo.DataValueField = "tNo"
            cboApplicantStandardHeaderNo.DataTextField = "tDesc"
            cboApplicantStandardHeaderNo.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            'Generic.ClearControls(Me, "pnlPopup")
            'Generic.EnableControls(Me, "pnlPopup", True)
            'lnkSave.Enabled = True
            'Try
            '    cboApplicantStandardHeaderNo.DataSource = SQLHelper.ExecuteDataSet("EApplicantStandardHeader_WebLookup", UserNo, 0, 2, PayLocNo)
            '    cboApplicantStandardHeaderNo.DataValueField = "tNo"
            '    cboApplicantStandardHeaderNo.DataTextField = "tDesc"
            '    cboApplicantStandardHeaderNo.DataBind()
            'Catch ex As Exception

            'End Try
            Dim URL As String
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            URL = Generic.GetFirstTab(0)
            If URL <> "" Then
                Response.Redirect(URL)
            End If
            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub


    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            'Dim lnk As New LinkButton
            'lnk = sender
            'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            'PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeEINo"})))
            ''Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            ''Generic.EnableControls(Me, "pnlPopup", IsEnabled)
            'Try
            '    cboApplicantStandardHeaderNo.DataSource = SQLHelper.ExecuteDataSet("EApplicantStandardHeader_WebLookup", UserNo, 0, 2)
            '    cboApplicantStandardHeaderNo.DataValueField = "tNo"
            '    cboApplicantStandardHeaderNo.DataTextField = "tDesc"
            '    cboApplicantStandardHeaderNo.DataBind()
            'Catch ex As Exception

            'End Try
            'lnkSave.Enabled = True
            'mdlShow.Show()
            Dim URL As String
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeEINo"})))
            If URL <> "" Then
                Response.Redirect(URL)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeEINo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EEmployeeEIDeti", UserNo, "EmployeeEINo", item)
                Generic.DeleteRecordAudit("EEmployeeEI", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
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

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim EmployeeEINo As Integer = Generic.ToInt(txtEmployeeEINo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))
        Dim applicantStandardHeaderNo As Integer = Generic.ToInt(cboApplicantStandardHeaderNo.SelectedValue)
        Dim effectivity As String = Generic.ToStr(txtEffectivity.Text)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EEmployeeEI_WebValidate", UserNo, EmployeeEINo, EmployeeNo, applicantStandardHeaderNo, effectivity, PayLocNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlShow.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EEmployeeEI_WebSave", UserNo, EmployeeEINo, EmployeeNo, applicantStandardHeaderNo, effectivity, PayLocNo) > 0 Then
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

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        'If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
        '    Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
        '    e.Enabled = value
        'End If
    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        ViewState("TransNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeEINo"}))
        lbl.Text = "Transaction No. : " & Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"Code"}))
        PopulateDetl()
    End Sub

    Protected Sub lnkTemplate_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton, i As Integer
        lnk = sender

        i = lnk.CommandArgument
        'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        'i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EvalTemplateNo"}))

        'Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantStandardHeaderNo", "EvalTemplateNo"})
        'ViewState("TransNo") = obj(0)
        'i = obj(1)

        Response.Redirect("~/secured/EmpStandardTemplateForm.aspx?TemplateID=" & Generic.ToInt(i) & "&FormName=EmpStandardHeader_EI.aspx&TableName=EEmployeeEI")

    End Sub

#End Region

#Region "Details"

    Private Sub PopulateDetl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEIDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()
        Catch ex As Exception

        End Try
    End Sub


#End Region

    Private Sub PopulateButtom(StatNo As Integer)

        Select Case StatNo
            Case 0 'In Process
                lnkAdd.Visible = True
                lnkDelete.Visible = True
                lnkEI.Visible = False
                lnkForPost.Visible = False
                lnkPost.Visible = False
            Case 1 'For Review
                lnkAdd.Visible = False
                lnkDelete.Visible = False
                lnkEI.Visible = True
                lnkForPost.Visible = True
                lnkPost.Visible = False
            Case 2 'For Exit Interview
                lnkAdd.Visible = False
                lnkDelete.Visible = False
                lnkEI.Visible = False
                lnkForPost.Visible = True
                lnkPost.Visible = False
            Case 3 'ESI Cert Issued
                lnkAdd.Visible = False
                lnkDelete.Visible = False
                lnkEI.Visible = False
                lnkForPost.Visible = False
                lnkPost.Visible = True
            Case 4 'Posted
                lnkAdd.Visible = False
                lnkDelete.Visible = False
                lnkEI.Visible = False
                lnkForPost.Visible = False
                lnkPost.Visible = False
            Case Else 'All
                lnkAdd.Visible = False
                lnkDelete.Visible = False
                lnkEI.Visible = False
                lnkForPost.Visible = False
                lnkPost.Visible = False
        End Select

    End Sub

    Protected Sub lnkEI_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeEINo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                i = i + Generic.ToInt(SQLHelper.ExecuteNonQuery("EEmployeeEI_WebStatUpdate", UserNo, item, 2))
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkForPost_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeEINo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                i = i + Generic.ToInt(SQLHelper.ExecuteNonQuery("EEmployeeEI_WebStatUpdate", UserNo, item, 3))
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeEINo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                i = i + Generic.ToInt(SQLHelper.ExecuteNonQuery("EEmployeeEI_WebStatUpdate", UserNo, item, 4))
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

End Class





