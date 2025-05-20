Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_CarCompAuditEdit_Analysis_Intervention
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim EmployeeNo As Integer = 0
    Dim CompNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ECompIntervention_Web", UserNo, TransNo, CompNo, "")
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ECompIntervention_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupMain", dt)
                disableEnableTrn(Generic.ToInt(row("CompInterventionTypeNo")))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        CompNo = Generic.ToInt(Request.QueryString("CompNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "CarCompAuditList.aspx", "ECompEmployee")
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", Generic.ToInt(Session("xPayLocNo")))
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "CarCompAuditList.aspx", "ECompEmployee") Then
            Generic.ClearControls(Me, "pnlPopupMain")
            lnkSave.Enabled = True
            cboTrnTitleNo.Enabled = False
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "CarCompAuditList.aspx", "ECompEmployee") Then
            Dim lnk As New LinkButton
            lnk = sender
            PopulateData(Generic.ToInt(lnk.CommandArgument))
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "CarCompAuditList.aspx", "ECompEmployee") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"CompInterventionNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ECompIntervention", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Sub disableEnableTrn(iino As Integer)
        Dim fds As DataSet, retval As Boolean = False
        fds = SQLHelper.ExecuteDataSet("Select IsFortraining from dbo.ECompInterventionType Where CompInterventionTypeNo=" & iino)
        If fds.Tables.Count > 0 Then
            If fds.Tables(0).Rows.Count > 0 Then
                retval = Generic.ToBol(fds.Tables(0).Rows(0)("Isfortraining"))
            End If
        End If
        If retval Then
            cboTrnTitleNo.Enabled = True
        Else
            cboTrnTitleNo.Enabled = False
            cboTrnTitleNo.Text = ""
        End If
    End Sub
    Protected Sub cboCompInterventionTypeNo_Change(sender As Object, e As System.EventArgs)
        Try
            disableEnableTrn(Generic.ToInt(cboCompInterventionTypeNo.SelectedValue))
            mdlMain.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim CompInterventionNo As Integer = Generic.ToInt(Me.txtCompInterventionNo.Text)
        Dim CompInterventionTypeNo As Integer = Generic.ToInt(Me.cboCompInterventionTypeNo.SelectedValue)
        Dim TrnTitleNo As Integer = Generic.ToInt(Me.cboTrnTitleNo.SelectedValue)
        Dim Description As String = Generic.ToStr(Me.txtDescription.Text)
        Dim ProjectedDate As String = Generic.ToStr(Me.txtprojecteddate.Text)
        Dim PercentToComplete As Decimal = Generic.ToDec(Me.txtPercentToComplete.Text)
        Dim IsCompleted As Boolean = Generic.ToBol(Me.txtIsCompleted.Checked)
        Dim Isfortraining As Boolean = Generic.ToBol(Me.chkIsfortraining.Checked)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("ECompIntervention_WebValidate", UserNo, CompInterventionNo, CompNo, TransNo, CompInterventionTypeNo, Description, ProjectedDate, PercentToComplete, IsCompleted, TrnTitleNo)
        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlMain.Show()
            Exit Sub
        End If


        If SQLHelper.ExecuteNonQuery("ECompIntervention_WebSave", UserNo, CompInterventionNo, CompNo, TransNo, CompInterventionTypeNo, Description, ProjectedDate, PercentToComplete, IsCompleted, TrnTitleNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
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
End Class

















