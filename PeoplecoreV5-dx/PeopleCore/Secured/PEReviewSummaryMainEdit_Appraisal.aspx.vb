Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting

Partial Class Secured_PEReviewSummaryAppraisalList
    Inherits System.Web.UI.Page


    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer


    Private Sub PopulateGrid()

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPEReviewSummaryAppraisal_Web", UserNo, TransNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub


    'Populate Combo box
    Private Sub PopulateTabHeader()
        Try
            Dim IsManual As Boolean = False
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEReviewSummaryMain_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
                IsManual = Generic.ToBol(row("IsManual"))
            Next

            If IsManual = False Then
                If txtIsPosted.Checked = True Then
                    lnkAdd.Visible = False
                    lnkDelete.Visible = False
                Else
                    lnkAdd.Visible = True
                    lnkDelete.Visible = True
                End If
            Else
                lnkAdd.Visible = False
                lnkDelete.Visible = False
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "PEReviewSummaryMainList.aspx", "EPEReviewSummaryMain")
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
            Try
                cboPEReviewMainNo.DataSource = SQLHelper.ExecuteDataSet("EPEReviewSummaryAppraisal_WebLookup", UserNo, PayLocNo)
                cboPEReviewMainNo.DataTextField = "tDesc"
                cboPEReviewMainNo.DataValueField = "tno"
                cboPEReviewMainNo.DataBind()
            Catch ex As Exception
            End Try
            PopulateTabHeader()
        End If
        PopulateGrid()

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEReviewSummaryAppraisal_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PEReviewSummaryMainList.aspx", "EPEReviewSummaryMain") Then
            Dim lnk As New LinkButton, IsEnabled As Boolean = False
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Generic.ClearControls(Me, "Panel2")
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEReviewSummaryAppraisalNo"})))

            'Enable or Disable Controls
            If txtIsPosted.Checked = True Then
                IsEnabled = False
            Else
                IsEnabled = True
            End If
            Generic.EnableControls(Me, "Panel2", IsEnabled)
            btnSave.Enabled = IsEnabled

            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub



    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PEReviewSummaryMainList.aspx", "EPEReviewSummaryMain") Then

            Generic.ClearControls(Me, "Panel2")
            mdlShow.Show()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If
    End Sub


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "PEReviewSummaryMainList.aspx", "EPEReviewSummaryMain") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEReviewSummaryAppraisalNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPEReviewSummaryAppraisal", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    'Submit record
    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim PEReviewSummaryAppraisalNo As Integer = Generic.ToInt(txtPEReviewSummaryAppraisalNo.Text)
        Dim PEReviewMainNo As Integer = Generic.ToInt(Me.cboPEReviewMainNo.SelectedValue)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EPEReviewSummaryAppraisal_WebValidate", UserNo, PEReviewSummaryAppraisalNo, TransNo, PEReviewMainNo, PayLocNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlShow.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EPEReviewSummaryAppraisal_WebSave", UserNo, PEReviewSummaryAppraisalNo, TransNo, PEReviewMainNo, PayLocNo) > 0 Then
            Retval = True
        Else
            Retval = False

        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If


    End Sub


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






