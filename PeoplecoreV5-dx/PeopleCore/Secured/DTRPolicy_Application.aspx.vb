Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web


Partial Class Secured_DTRApplicationPolicy
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        AccessRights.CheckUser(UserNo)


        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "phleave", PayLocNo)
        End If
        PopulateGrid()

        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EDTRApplicationPolicy_Web", UserNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopup")
            Generic.ClearControls(Me, "phleave")
            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try
            phleave.Visible = False
            Me.cboLeavetypeNo.Enabled = False
            Me.txtHrs.Enabled = False
            Me.txtHrsMsg.Enabled = False
            btnSave.Enabled = True
            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
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
                Dim lnk As New LinkButton, i As Integer, IsEnabled As Boolean
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRApplicationPolicyNo", "IsEnabled"})
                i = Generic.ToInt(obj(0))
                IsEnabled = Generic.ToBol(obj(1))

                Try
                    cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                    cboPayLocNo.DataTextField = "tdesc"
                    cboPayLocNo.DataValueField = "tNo"
                    cboPayLocNo.DataBind()

                Catch ex As Exception

                End Try

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EDTRApplicationPolicy_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows                   
                    Generic.PopulateData(Me, "pnlPopup", dt)
                    Generic.PopulateData(Me, "phleave", dt)

                    phleave.Visible = False
                    If cboApproverCodeNo.SelectedValue = 3 Then 'leave application
                        Me.cboLeavetypeNo.Enabled = True
                        Me.txtHrs.Enabled = True
                        Me.txtHrsMsg.Enabled = True
                        phleave.Visible = True
                        lblminhrs.InnerText = "Minimum hrs. of applied leave :"
                    ElseIf cboApproverCodeNo.SelectedValue = 6 Then
                        Me.cboLeavetypeNo.Enabled = True
                        Me.txtHrs.Enabled = True
                        Me.txtHrsMsg.Enabled = True
                        phleave.Visible = True
                    Else
                        Me.cboLeavetypeNo.Text = ""
                        Me.txtHrs.Text = ""
                        Me.cboLeavetypeNo.Enabled = False
                        Me.txtHrs.Enabled = False
                        Me.txtHrsMsg.Enabled = False
                    End If
                Next

                btnSave.Enabled = IsEnabled
                mdlShow.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim DTRApplicationPolicyNo As Integer = Generic.ToInt(txtDTRApplicationPolicyNo.Text)
        Dim ApproverCodeNo As Integer = Generic.ToInt(cboApproverCodeNo.SelectedValue)
        Dim LeaveTypeNo As Integer = Generic.ToInt(cboLeavetypeNo.SelectedValue)
        Dim Hrs As Double = Generic.ToDec(txtHrs.Text)
        Dim HrsMsg As String = Generic.ToStr(txtHrsMsg.Text)
        Dim DaysPrior As Integer = Generic.ToInt(txtDaysPrior.Text)
        Dim DaysPriorMsg As String = Generic.ToStr(txtDaysPriorMsg.Text)
        Dim DaysAfter As Integer = Generic.ToInt(txtDaysAfter.Text)
        Dim DaysAfterMsg As String = Generic.ToStr(txtDaysAfterMsg.Text)
        Dim PayclassNo As Integer = Generic.ToInt(cboPayclassNo.SelectedValue)
        Dim modeofapplication As Integer = Generic.ToInt(cboDTRApplicationPolicyModeNo.SelectedValue)
        Dim days As Integer = Generic.ToInt(txtDays.Text)
        Dim daysmsg As String = Generic.ToStr(txtDaysMsg.Text)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EDTRApplicationPolicy_WebValidate", UserNo, DTRApplicationPolicyNo, ApproverCodeNo, LeaveTypeNo, Hrs, HrsMsg, DaysPrior, DaysPriorMsg, DaysAfter, DaysAfterMsg, Generic.ToInt(cboPayLocNo.SelectedValue), PayclassNo, Generic.ToDbl(txtMinLengthService.Text), txtMinLengthServiceMsg.Text)

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

        Dim RetVal As Boolean = False
        dt = SQLHelper.ExecuteDataTable("EDTRApplicationPolicy_WebSave", UserNo, DTRApplicationPolicyNo, ApproverCodeNo, LeaveTypeNo, Hrs, HrsMsg, DaysPrior, DaysPriorMsg, DaysAfter, DaysAfterMsg, Generic.ToInt(cboPayLocNo.SelectedValue), PayclassNo, Generic.ToDbl(txtMinLengthService.Text), txtMinLengthServiceMsg.Text, modeofapplication, days, daysmsg)
        For Each row As DataRow In dt.Rows
            RetVal = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                RetVal = False
            End If
        Next
        If RetVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRApplicationPolicyNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTRApplicationPolicy", UserNo, CType(item, Integer))
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

    Protected Sub cboApproverCodeNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        phleave.Visible = False
        If Generic.ToInt(cboApproverCodeNo.SelectedValue) = 3 Then 'leave application
            Me.cboLeavetypeNo.Enabled = True
            Me.txtHrs.Enabled = True
            Me.txtHrsMsg.Enabled = True
            phleave.Visible = True
            lblminhrs.InnerText = "Minimum hrs. of applied leave."
        ElseIf Generic.ToInt(cboApproverCodeNo.SelectedValue) = 6 Then
            Me.cboLeavetypeNo.Enabled = True
            Me.txtHrs.Enabled = True
            Me.txtHrsMsg.Enabled = True
            phleave.Visible = True
        Else
            Me.cboLeavetypeNo.Text = ""
            Me.txtHrs.Text = ""
            Me.cboLeavetypeNo.Enabled = False
            Me.txtHrs.Enabled = False
            Me.txtHrsMsg.Enabled = False
        End If

        mdlShow.Show()
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

End Class
