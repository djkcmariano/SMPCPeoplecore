Imports clsLib
Imports System.Data
Partial Class Secured_ERProgramEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "ERProgramList.aspx") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EERProgram_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "ERProgramList.aspx")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateData()
            PopulateTabHeader()
            Try
                cboEvalTemplateNo.DataSource = SQLHelper.ExecuteDataTable("EEvalTemplate_WebLookup", UserNo, "1018", PayLocNo)
                cboEvalTemplateNo.DataTextField = "tdesc"
                cboEvalTemplateNo.DataValueField = "tNo"
                cboEvalTemplateNo.DataBind()
            Catch ex As Exception
            End Try            

        End If

        EnabledControls()

    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", Enabled)
        btnModify.Visible = Not Enabled
        btnSave.Visible = Enabled
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EERProgram_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "ERProgramList.aspx") Then
            Dim RetVal As Boolean = False
            Dim dt As DataTable
            Dim tno As Integer = Generic.ToInt(Me.txtERProgramNo.Text)
            Dim programdesc As String = Generic.ToStr(Me.txtERProgramDesc.Text)
            Dim programtypeno As Integer = Generic.ToInt(Me.cboERProgramTypeNo.SelectedValue)
            Dim startdate As String = Generic.ToStr(Me.txtStartDate.Text)
            Dim enddate As String = Generic.ToStr(Me.txtEndDate.Text)
            Dim hrs As Double = Generic.ToDec(Me.txtHrs.Text)
            Dim budget As Double = Generic.ToDec(Me.txtBudget.Text)
            Dim objective As String = Generic.ToStr(Me.txtObjective.Text)
            Dim speaker As String = Generic.ToStr(Me.txtSpeaker.Text)
            Dim venue As String = Generic.ToStr(Me.txtVenue.Text)
            Dim programstatno As Integer = Generic.ToInt(Me.cboERProgramStatNo.SelectedValue)
            Dim evaluation As String = Generic.ToStr(Me.txtEvaluation.Text)

            '//validate start here
            'Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            'Dim dtx As New DataTable
            'dtx = SQLHelper.ExecuteDataTable("EPayBonus_WebValidate", UserNo, PayNo, StartDate, EndDate, _
            '                             PayDate, PayClassNo, PayCateNo, IsDeductTax, IsIncludeForw, _
            '                             IsIncludeLoan, IsActivateDed, IsPaymentSuspended, _
            '                             noofmonthstoAssume, PEPeriodNo, _
            '                             IsIncludeOther, IsIncludeMass, PayLocNo)

            'For Each rowx As DataRow In dtx.Rows
            '    invalid = Generic.ToBol(rowx("tProceed"))
            '    messagedialog = Generic.ToStr(rowx("xMessage"))
            '    alerttype = Generic.ToStr(rowx("AlertType"))
            'Next

            'If invalid = True Then
            '    MessageBox.Alert(messagedialog, alerttype, Me)
            '    Exit Sub
            'End If


            dt = SQLHelper.ExecuteDataTable("EERProgram_WebSave", UserNo, tno, programdesc, programtypeno, startdate, enddate, hrs, budget, objective, speaker, venue, programstatno, evaluation, PayLocNo, Generic.ToInt(cboEvalTemplateNo.SelectedValue))

            For Each row As DataRow In dt.Rows
                TransNo = Generic.ToInt(row("PayNo"))
                RetVal = True
            Next

            If RetVal = True Then
                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim url As String = "ERProgramEdit.aspx?id=" & TransNo
                    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
                Else
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                    ViewState("IsEnabled") = False
                    EnabledControls()
                End If
                'PopulateTabHeader()
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If

        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

End Class
