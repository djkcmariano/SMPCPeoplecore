Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_TrnTakenEdit
    Inherits System.Web.UI.Page

    Dim TransNo As Int64
    Dim IsEnabled As Boolean = False
    Dim UserNo As Int64
    Dim TrnStatNo As Integer
    Dim PayLocNo As Int64 = 0

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ETrnTaken_WebOne", UserNo, TransNo)
        Generic.ClearControls(Me, "Panel1")
        For Each row As DataRow In dt.Rows
            Generic.PopulateDropDownList(Generic.ToInt(UserNo), Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            Generic.PopulateData(Me, "Panel1", dt)
        Next

        

    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnTaken_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
            Next
            lbl.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))

        Permission.IsAuthenticated()

        If Not IsPostBack Then
            Generic.PopulateDropDownList(Generic.ToInt(UserNo), Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateData()
            PopulateTabHeader()
        End If

        EnabledControls()

    End Sub
    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then

            Dim Retval As Boolean = False
            Dim TrnTitleNo As Integer = Generic.ToInt(hifTrnTitleNo.Value)
            Dim Description As String = Generic.ToStr(txtDescription.Text)
            Dim Objectives As String = Generic.ToStr(txtObjectives.Text)
            Dim TrnTypeNo As Integer = Generic.ToInt(cboTrnTypeNo.SelectedValue)
            Dim NoOfMonths As Double = Generic.ToDec(txtNoOfMonths.Text)
            Dim ServiceContract As Double = Generic.ToDec(txtServiceContract.Text)
            Dim TrnRetakenNo As Integer = Generic.ToInt(cboTrnRetakenNo.SelectedValue)
            Dim Cost As Double = Generic.ToDec(txtCost.Text)
            Dim Hrs As Double = Generic.ToDec(txtHrs.Text)
            Dim CutOffDate As String = "" 'Generic.ToStr(txtCutOffDate.Text)
            Dim StartDate As String = Generic.ToStr(txtStartDate.Text)
            Dim EndDate As String = Generic.ToStr(txtEndDate.Text)
            Dim TimeIn As String = Replace(Generic.ToStr(txtTimeIn.Text), ":", "")
            Dim TimeOut As String = Replace(Generic.ToStr(txtTimeOut.Text), ":", "")
            Dim TrnEnrollTypeNo As Integer = Generic.ToInt(cboTrnEnrollTypeNo.SelectedValue)
            Dim TargetHC As Integer = 0 'Generic.ToInt(txtTargetHC.Text)
            Dim TrnProviderNo As Integer = Generic.ToInt(cboTrnProviderNo.SelectedValue)
            Dim TrnVenueNo As Integer = Generic.ToInt(cboTrnVenueNo.SelectedValue)
            Dim Remarks As String = Generic.ToStr(txtRemarks.Text)
            Dim TrnCategoryNo As Integer = Generic.ToInt(cboTrnCategoryNo.SelectedValue)
            Dim EnrollDateOpen As String = Generic.ToStr(txtEnrollDateOpen.Text)
            Dim EnrollDateClosed As String = Generic.ToStr(txtEnrollDateClosed.Text)
            Dim MinimumSeats As Integer = Generic.ToInt(txtMinimumSeats.Text)
            Dim MaximumSeats As Integer = Generic.ToInt(txtMaximumSeats.Text)
            Dim TrnStatNo As Integer = Generic.ToInt(cboTrnStatNo.SelectedValue)
            Dim ds As New DataSet


            ''//validate start here
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("ETrnTaken_WebValidate", UserNo, TransNo, TrnTitleNo, Description, Objectives, TrnTypeNo, NoOfMonths, ServiceContract, TrnRetakenNo, Cost, Hrs, StartDate, EndDate, TimeIn, TimeOut, TrnEnrollTypeNo, TrnProviderNo, TrnVenueNo, Remarks, TrnCategoryNo, EnrollDateOpen, EnrollDateClosed, MinimumSeats, MaximumSeats, TrnStatNo, Session("xPayLocNo"))

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("Invalid"))
                messagedialog = Generic.ToStr(rowx("MessageDialog"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If

            ds = SQLHelper.ExecuteDataSet("ETrnTaken_WebSave", UserNo, TransNo, TrnTitleNo, Description, Objectives, TrnTypeNo, NoOfMonths, ServiceContract, TrnRetakenNo, Cost, Hrs, StartDate, EndDate, TimeIn, TimeOut, TrnEnrollTypeNo, TrnProviderNo, TrnVenueNo, Remarks, TrnCategoryNo, EnrollDateOpen, EnrollDateClosed, MinimumSeats, MaximumSeats, TrnStatNo, Session("xPayLocNo"))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    TransNo = Generic.ToInt(ds.Tables(0).Rows(0)("RetVal"))
                End If
            End If

            If TransNo > 0 Then
                Retval = True
            Else
                Retval = False
            End If


            If Retval = True Then
                Dim xURL As String = "TrnTakenEdit.aspx?id=" & TransNo
                MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, xURL)

                ViewState("IsEnabled") = False
                EnabledControls()
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)

        If Generic.ToBol(txtIsPosted.Checked) = False Then
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                ViewState("IsEnabled") = True
                EnabledControls()
            Else
                MessageBox.Information(MessageTemplate.DeniedEdit, Me)
            End If
        Else
            MessageBox.Information("Posted or cancelled transactions cannot be modify!", Me)
        End If

    End Sub

    Private Sub EnabledControls()


        'If TransNo = 0 Then
        '    ViewState("IsEnabled") = True
        'End If

        ViewState("IsEnabled") = False

        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)
        'txtActualHC.Enabled = False
        'txtBalanceHC.Enabled = False

        'txtDescription.Enabled = False
        'txtObjectives.Enabled = False
        'cboTrnTypeNo.Enabled = False
        'txtNoOfMonths.Enabled = False
        'txtServiceContract.Enabled = False
        'cboTrnRetakenNo.Enabled = False
        'txtHrs.Enabled = False
        'txtCost.Enabled = False

        If TransNo = 0 Then
            'Training Status - Open
            cboTrnStatNo.Enabled = False
            cboTrnStatNo.Text = 1
        Else
            cboTrnStatNo.Enabled = IsEnabled
        End If

        lnkModify.Visible = False 'Not IsEnabled
        lnkSave.Visible = False 'IsEnabled

    End Sub

    Protected Sub hifTrnTitleNo_ValueChanged(sender As Object, e As System.EventArgs)
        Dim _dt As New DataTable
        _dt = SQLHelper.ExecuteDataTable("ETrnTitle_WebOne", UserNo, Generic.CheckDBNull(hifTrnTitleNo.Value, clsBase.clsBaseLibrary.enumObjectType.IntType))
        For Each row As DataRow In _dt.Rows
            txtTrnTitleDesc.Text = Generic.ToStr(row("TrnTitleDesc"))
            txtDescription.Text = Generic.ToStr(row("Description"))
            txtObjectives.Text = Generic.ToStr(row("Objectives"))
            txtHrs.Text = Generic.ToDec(row("Hrs"))
            txtCost.Text = Generic.ToDec(row("Cost"))
            txtNoOfMonths.Text = Generic.ToDec(row("NoOfMonths"))
            txtServiceContract.Text = Generic.ToDec(row("ServiceContract"))
            txtRemarks.Text = Generic.ToStr(row("Remarks"))

            If Generic.ToInt(row("TrnCategoryNo") > 0) Then
                cboTrnCategoryNo.Text = Generic.ToStr(row("TrnCategoryNo"))
            Else
                cboTrnCategoryNo.Text = ""
            End If

            If Generic.ToInt(row("TrnTypeNo") > 0) Then
                cboTrnTypeNo.Text = Generic.ToStr(row("TrnTypeNo"))
            Else
                cboTrnTypeNo.Text = ""
            End If

            If Generic.ToInt(row("TrnRetakenNo") > 0) Then
                cboTrnRetakenNo.Text = Generic.ToStr(row("TrnRetakenNo"))
            Else
                cboTrnRetakenNo.Text = ""
            End If

        Next

    End Sub

End Class
