Imports clsLib
Imports System.Data

Partial Class Secured_DTRHolidayEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer
    Dim IsEnabled As Boolean

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "DTRHolidayList.aspx", "EHoliday")
        If TransNo = 0 Then : ViewState("IsEnabled") = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If
        If Not IsPostBack Then
            PopulateTabHeader()
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try
            PopulateData(TransNo)
        End If

        EnabledControls()
    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "pnlPopup", IsEnabled)
        Generic.PopulateDataDisabled(Me, "pnlPopup", UserNo, PayLocNo, Generic.ToStr(Session("xMenuType")))
        btnModify.Visible = Not IsEnabled
        btnSave.Visible = IsEnabled        
    End Sub

    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "DTRHolidayList.aspx", "EHoliday") Then
            If txtIsEnabled.Checked = True Then
                ViewState("IsEnabled") = True
            Else
                ViewState("IsEnabled") = False
            End If
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EHoliday_WebOne", UserNo, id)
        Generic.PopulateData(Me, "pnlPopup", dt)
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        Dim id As Integer = SaveRecord()
        If Generic.ToInt(Request.QueryString("id")) = 0 Then
            Dim xURL As String = "dtrholidayedit.aspx?id=" & id
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, xURL)
        Else
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            ViewState("IsEnabled") = False
            EnabledControls()
        End If
        PopulateTabHeader()
    End Sub

    Private Function SaveRecord() As Integer
        Dim holIn As String = Generic.ToStr(Replace(txtHolIn.Text, ":", ""))

        Dim obj As Object = SQLHelper.ExecuteScalar("EHoliday_WebSave", UserNo, Generic.ToInt(txtCode.Text), txtHolidayDesc.Text, txtHolidayDate.Text, cboDayTypeNo.SelectedValue, Generic.ToDec(Me.txtNoOfHour.Text), chkIsApplyToAll.Checked, PayLocNo, chkIsAM.Checked, holIn, Generic.ToBol(chkIsArchived.Checked))
        Return Generic.ToInt(obj)
    End Function

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EHolidayTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))            
        Next
    End Sub

End Class
