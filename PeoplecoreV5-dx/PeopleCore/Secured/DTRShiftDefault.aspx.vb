Imports System.Data
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web

Partial Class Secured_DTRShiftDefaultList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRShiftDefault_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRShiftDefault_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 23)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
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
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRShiftDefaultNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTRShiftDefault", UserNo, item)
                Generic.DeleteRecordAuditCol("EDTRShiftDefaultDeti", UserNo, "DTRShiftDefaultNo", item)
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

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRShiftDefaultNo"})))
            mdlDetl.Show()
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
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim Effectivity As String = txtEffectivity.Text
        Dim mon As Integer = Generic.ToInt(cboShiftNoMon.SelectedValue)
        Dim tue As Integer = Generic.ToInt(cboShiftNoTue.SelectedValue)
        Dim wed As Integer = Generic.ToInt(cboShiftNoWed.SelectedValue)
        Dim thu As Integer = Generic.ToInt(cboShiftNoThu.SelectedValue)
        Dim fri As Integer = Generic.ToInt(cboShiftNoFri.SelectedValue)
        Dim sat As Integer = Generic.ToInt(cboShiftNoSat.SelectedValue)
        Dim sun As Integer = Generic.ToInt(cboShiftNoSun.SelectedValue)
        Dim dayoff1 As Integer = Generic.ToInt(cboDayOffNo.SelectedValue)
        Dim dayoff2 As Integer = Generic.ToInt(cboDayOffNo2.SelectedValue)
        Dim dayoff3 As Integer = Generic.ToInt(cboDayOffNo3.SelectedValue)

        Dim altemon As Integer = Generic.ToInt(cboAlteShiftNoMon.SelectedValue)
        Dim altetue As Integer = Generic.ToInt(cboAlteShiftNoTue.SelectedValue)
        Dim altewed As Integer = Generic.ToInt(cboAlteShiftNoWed.SelectedValue)
        Dim altethu As Integer = Generic.ToInt(cboAlteShiftNoThu.SelectedValue)
        Dim altefri As Integer = Generic.ToInt(cboAlteShiftNoFri.SelectedValue)
        Dim altesat As Integer = Generic.ToInt(cboAlteShiftNoSat.SelectedValue)
        Dim altesun As Integer = Generic.ToInt(cboAlteShiftNoSun.SelectedValue)
        Dim altedayoff1 As Integer = Generic.ToInt(cboAlteDayOffNo.SelectedValue)
        Dim altedayoff2 As Integer = Generic.ToInt(cboAlteDayOffNo2.SelectedValue)
        Dim altedayoff3 As Integer = Generic.ToInt(cboAlteDayOffNo3.SelectedValue)


        If SQLHelper.ExecuteNonQuery("EDTRShiftDefault_WebSave", UserNo, Generic.ToInt(txtCode.Text), EmployeeNo, Effectivity, mon, tue, wed, thu,
                                   fri, sat, sun, dayoff1, dayoff2, dayoff3, altemon, altetue, altewed, altethu, altefri, altesat, altesun, altedayoff1, altedayoff2, altedayoff3) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If
    End Function


    Protected Sub ASPxGridViewExporter_RenderBrick(sender As Object, e As DevExpress.Web.ASPxGridViewExportRenderingEventArgs) Handles grdExport.RenderBrick
        Dim dataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
        If e.RowType = GridViewRowType.Data AndAlso dataColumn IsNot Nothing Then
            Select Case dataColumn.FieldName
                Case "AlteShift"
                    e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
                    e.TextValue = e.TextValue.ToString.Replace("<span>", "")
                    e.TextValue = e.TextValue.ToString.Replace("</span>", "")
                    e.TextValue = e.TextValue.ToString.Replace("<br/>", " ")
                    e.TextValue = e.TextValue.ToString.Replace("<br />", " ")
                Case "DefaultShift"
                    e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
                    e.TextValue = e.TextValue.ToString.Replace("<span>", "")
                    e.TextValue = e.TextValue.ToString.Replace("</span>", "")
                    e.TextValue = e.TextValue.ToString.Replace("<br/>", " ")
                    e.TextValue = e.TextValue.ToString.Replace("<br />", " ")
                Case "DefaultDayOff"
                    e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
                    e.TextValue = e.TextValue.ToString.Replace("<span>", "")
                    e.TextValue = e.TextValue.ToString.Replace("</span>", "")
                    e.TextValue = e.TextValue.ToString.Replace("<br/>", " ")
                    e.TextValue = e.TextValue.ToString.Replace("<br />", " ")
                Case "AlteDayOff"
                    e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
                    e.TextValue = e.TextValue.ToString.Replace("<span>", "")
                    e.TextValue = e.TextValue.ToString.Replace("</span>", "")
                    e.TextValue = e.TextValue.ToString.Replace("<br/>", " ")
                    e.TextValue = e.TextValue.ToString.Replace("<br />", " ")
            End Select


        End If
        If e.RowType = GridViewRowType.Header AndAlso dataColumn IsNot Nothing Then
            e.Text = e.Text.Replace("<br/>", " ")
            e.Text = e.Text.Replace("<br />", " ")
            e.Text = e.Text.Replace("<br>", " ")
            e.Text = e.Text.Replace("<center>", "")
            e.Text = e.Text.Replace("</center>", "")
        End If

    End Sub
End Class







