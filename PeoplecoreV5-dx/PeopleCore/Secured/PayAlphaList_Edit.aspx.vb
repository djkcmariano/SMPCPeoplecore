Imports clsLib
Imports System.Data
Imports System.IO
Imports System.Web
Partial Class Secured_PayAlphaList_Edit
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim RetVal As Integer = SaveRecord()
        If RetVal = 1 Then
            If Generic.ToInt(Request.QueryString("id")) = 0 Then
                Dim url As String = "PayAlphaList_Edit.aspx?id=" & RetVal
                MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            Else
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                ViewState("IsEnabled") = False
                EnabledControls()
            End If
            'PopulateTabHeader()
        ElseIf RetVal = 2 Then
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
     
    End Sub

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayAlphaList.aspx", "EAlpha") Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EAlpha_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next

    End Sub
    Private Sub PopulateDropdown()
        Try
            Me.cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup", UserNo, PayLocNo)
            Me.cboPayLocNo.DataTextField = "tDesc"
            Me.cboPayLocNo.DataValueField = "tNO"
            Me.cboPayLocNo.DataBind()
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "PayAlphaList.aspx", "EAlpha")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            'PopulateTabHeader()
            PopulateData()
            PopulateDropdown()
        End If

        EnabledControls()

    End Sub

    Private Sub EnabledControls()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", Enabled)
        txtCode.Enabled = False

        lnkModify.Visible = Not Enabled
        lnkSave.Visible = Enabled
    End Sub

    Private Function SaveRecord() As Integer
        Dim Retval As Integer = 0
   
            Dim tno As Integer = Generic.ToInt(Me.txtAlphaNo.Text)
            Dim xPayLocNo As Integer = Generic.ToInt(Me.cboPayLocNo.SelectedValue)
            Dim ApplicableMonth As Integer = Generic.ToInt(Me.cboApplicableMonth.SelectedValue)
            Dim ApplicableYear As Integer = Generic.ToInt(Me.txtApplicableYear.Text)
            Dim signatoryNo As Integer = Generic.ToInt(Me.hifsignatoryNo.Value)
            Dim signatoryno2 As Integer = Generic.ToInt(Me.hifsignatoryno2.Value)
            Dim AlphaDesc As String = Generic.ToStr(Me.txtAlphaDesc.Text)
            Dim FacilityNo As Integer = Generic.ToInt(Me.cboFacilityNo.SelectedValue)
            Dim MaxAmtAccumulatedExemp As Double = Generic.ToDec(Me.txtMaxAmtAccumulatedExemp.Text)

            Dim dt As DataTable
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            dt = SQLHelper.ExecuteDataTable("EAlpha_WebValidate", UserNo, tno, ApplicableYear, ApplicableMonth, AlphaDesc, xPayLocNo, PayLocNo, FacilityNo)
            For Each row As DataRow In dt.Rows
                invalid = Generic.ToBol(row("Invalid"))
                messagedialog = Generic.ToStr(row("MessageDialog"))
                alerttype = Generic.ToStr(row("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                Retval = 3
                Exit Function
            End If

            If SQLHelper.ExecuteNonQuery("EAlpha_WebSave", UserNo, tno, ApplicableYear, ApplicableMonth, AlphaDesc, xPayLocNo, signatoryNo, signatoryno2, PayLocNo, FacilityNo, MaxAmtAccumulatedExemp) > 0 Then
                Retval = 1
            Else
                Retval = 2
            End If
        Return Retval
    End Function
    Protected Sub cboPayLocNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayLoc_WebOne", UserNo, Generic.ToInt(cboPayLocNo.SelectedValue))
        For Each row As DataRow In dt.Rows
            txtMaxAmtAccumulatedExemp.Text = Generic.ToDec(row("MaxAmtAccumulatedExemp"))
        Next
    End Sub
    'Private Sub PopulateTabHeader()
    '    Dim dt As DataTable
    '    dt = SQLHelper.ExecuteDataTable("EEvalTemplateTabHeader", UserNo, TransNo)
    '    For Each row As DataRow In dt.Rows
    '        lbl.Text = Generic.ToStr(row("Display"))
    '    Next
    'End Sub

End Class

