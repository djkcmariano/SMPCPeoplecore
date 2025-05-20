Imports clsLib
Imports System.Data
Partial Class Secured_EmpRateEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateData()
            ViewState("IsEnabled") = False
            EnabledControls()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpRateList.aspx", "EEmployee") Then
            If IsEditRate() Then
                ViewState("IsEnabled") = True
                EnabledControls()
            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeRate_WebOne", UserNo, TransNo)
            Generic.PopulateData(Me, "Panel1", dt)
        Catch ex As Exception

        End Try
    End Sub

    Private Function IsEditRate() As Boolean
        Dim obj As Object
        obj = SQLHelper.ExecuteScalar("EEmployeeRate_WebEdit", UserNo, TransNo)
        Return Generic.ToBol(obj)
    End Function

    Private Sub EnabledControls()
        Dim IsEnabled As Boolean = False
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)
        Generic.PopulateDataDisabled(Me, "Panel1", UserNo, PayLocNo, Generic.ToStr(Session("xMenuType")))
        btnModify.Visible = Not IsEnabled
        btnSave.Visible = IsEnabled
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "EmpRateList.aspx", "EEmployee")
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateData()
            PopulateTabHeader()
        End If
        EnabledControls()
        If UserNo = Generic.ToInt(txtUserNo.Text) Then
            btnModify.Visible = False
        Else
            btnModify.Visible = True
        End If
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
    End Sub

    Private Function SaveRecord() As Boolean
        If SQLHelper.ExecuteNonQuery("EEmployeeRate_WebSave", UserNo, Generic.ToInt(TransNo), Generic.ToInt(cboEmployeeRateClassNo.SelectedValue), Generic.ToDec(txtCurrentSalary.Text)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
