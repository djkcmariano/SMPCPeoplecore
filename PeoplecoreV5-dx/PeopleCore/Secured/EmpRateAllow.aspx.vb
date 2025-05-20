Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_EmpRateAllow
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "EmpRateList.aspx", "EEmployee")
        If Not IsPostBack Then
            PopulateTabHeader()
            Try
                cboPayIncomeTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayIncomeType_WebLookup", UserNo, PayLocNo)
                cboPayIncomeTypeNo.DataValueField = "tNo"
                cboPayIncomeTypeNo.DataTextField = "tDesc"
                cboPayIncomeTypeNo.DataBind()
            Catch ex As Exception
            End Try
        End If
        PopulateGrid()
    End Sub

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EEmployeeRateAllowance_Web", UserNo, TransNo)
        grdDetl.DataSource = _dt
        grdDetl.DataBind()
    End Sub

    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "EmpRateList.aspx", "EEmployee") Then
            Generic.ClearControls(Me, "pnlPopupAllow")

            Try
                cboPayIncomeTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayIncomeType_WebLookup_UnionAllow", UserNo, 0, PayLocNo)
                cboPayIncomeTypeNo.DataValueField = "tNo"
                cboPayIncomeTypeNo.DataTextField = "tDesc"
                cboPayIncomeTypeNo.DataBind()
            Catch ex As Exception
            End Try

            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupAllow", PayLocNo)
            PopulateControls()
            mdlAllow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If


    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "EmpRateList.aspx", "EEmployee") Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"EmployeeAllowanceno"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeAllowance", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpRateList.aspx", "EEmployee") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeAllowanceno"})))
            mdlAllow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeRateAllowance_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Try
                    cboPayIncomeTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayIncomeType_WebLookup_UnionAllow", UserNo, Generic.ToInt(row("PayIncomeTypeNo")), PayLocNo)
                    cboPayIncomeTypeNo.DataValueField = "tNo"
                    cboPayIncomeTypeNo.DataTextField = "tDesc"
                    cboPayIncomeTypeNo.DataBind()
                Catch ex As Exception
                End Try
            Next
            Generic.PopulateDropDownList_Union(UserNo, Me, "pnlPopupAllow", dt, PayLocNo)
            Generic.PopulateData(Me, "pnlPopupAllow", dt)
            PopulateControls()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSaveDetl_Click(sender As Object, e As EventArgs)

        Dim RetVal As Boolean = False
        Dim tno As Integer = Generic.ToInt(Me.txtEmployeeAllowanceno.Text)
        Dim PayIncomeTypeNo As Integer = Generic.ToInt(Me.cboPayIncomeTypeNo.SelectedValue)
        Dim Amount As Decimal = Generic.ToDec(Me.txtAmount.Text)
        Dim PayscheduleNo As Integer = Generic.ToInt(Me.cboPayscheduleNo.SelectedValue)
        Dim IsDTRBase As Boolean = Generic.ToBol(Me.txtIsDTRBase.Checked)
        Dim IsAtleastWithDTR As Boolean = Generic.ToBol(Me.txtIsAtleastWithDTR.Checked)
        Dim IsIncludeBonus As Boolean = Generic.ToBol(Me.txtIsIncludeBonus.Checked)
        Dim IsPerDay As Boolean = Generic.ToBol(Me.txtIsPerDay.Checked)
        Dim StartDate As String = Generic.ToStr(Me.txtStartDate.Text)
        Dim EndDate As String = Generic.ToStr(Me.txtEndDate.Text)


        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EEmployeeRateAllowance_WebValidate", UserNo, tno, TransNo, PayIncomeTypeNo, Amount, PayscheduleNo, _
                                     IsDTRBase, IsAtleastWithDTR, IsIncludeBonus, IsPerDay, StartDate, EndDate)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlAllow.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EEmployeeRateAllowance_WebSave", UserNo, PayLocNo, tno, TransNo, PayIncomeTypeNo, Amount, PayscheduleNo, _
                                     IsDTRBase, IsAtleastWithDTR, IsIncludeBonus, IsPerDay, StartDate, EndDate) > 0 Then
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


    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
    End Sub

    Protected Sub PopulateControls()

        txtIsAtleastWithDTR.Enabled = True
        txtIsDTRBase.Enabled = True
        'txtIsPerDay.Enabled = False
        If txtIsAtleastWithDTR.Checked = True Then
            txtIsDTRBase.Enabled = False
            txtIsDTRBase.Checked = False
        End If

        If txtIsDTRBase.Checked = True Then
            'txtIsPerDay.Enabled = True
            txtIsAtleastWithDTR.Enabled = False
            txtIsAtleastWithDTR.Checked = False
        Else
            'txtIsPerDay.Checked = False
        End If
    End Sub

    Protected Sub txtIsRefresh_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateControls()
        mdlAllow.Show()
    End Sub

End Class
