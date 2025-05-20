Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_EmpHRANEdit_Allowance
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "EmpHRANList.aspx", "EHRAN")

        If Not IsPostBack Then
            PopulateTabHeader()
            EnabledControls()
        End If

        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRANAllowance_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRANAllowance_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Try
                    cboPayIncomeTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayIncomeType_WebLookup_UnionAllow", UserNo, Generic.ToInt(row("PayIncomeTypeNo")), PayLocNo)
                    cboPayIncomeTypeNo.DataValueField = "tNo"
                    cboPayIncomeTypeNo.DataTextField = "tDesc"
                    cboPayIncomeTypeNo.DataBind()
                Catch ex As Exception
                End Try
            Next
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, PayLocNo)
            Generic.PopulateData(Me, "Panel1", dt)
            PopulateControls()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub EnabledControls()
        Dim Enabled As Boolean = True

        If txtIsPosted.Checked = True Then
            Enabled = False
        End If

        Generic.EnableControls(Me, "Panel1", Enabled)

        lnkAdd.Visible = Enabled
        lnkDelete.Visible = Enabled
        lnkSave.Visible = Enabled
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
            ' txtIsPerDay.Enabled = True
            txtIsAtleastWithDTR.Enabled = False
            txtIsAtleastWithDTR.Checked = False
        Else
            'txtIsPerDay.Checked = False
        End If

        If txtIsPosted.Checked = True Then
            Generic.EnableControls(Me, "Panel1", False)
        End If

    End Sub

    Protected Sub txtIsRefresh_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateControls()
        ModalPopupExtender1.Show()
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRAN_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "EmpHRANList.aspx", "EHRAN") Then
            Generic.ClearControls(Me, "Panel1")

            Try
                cboPayIncomeTypeNo.DataSource = SQLHelper.ExecuteDataSet("EPayIncomeType_WebLookup_UnionAllow", UserNo, 0, PayLocNo)
                cboPayIncomeTypeNo.DataValueField = "tNo"
                cboPayIncomeTypeNo.DataTextField = "tDesc"
                cboPayIncomeTypeNo.DataBind()
            Catch ex As Exception
            End Try

            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateControls()
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim RetVal As Boolean = False
        Dim tno As Integer = Generic.ToInt(Me.txtHRANAllowanceNo.Text)
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
        dtx = SQLHelper.ExecuteDataTable("EHRANAllowance_WebValidate", UserNo, tno, TransNo, PayIncomeTypeNo, Amount, PayscheduleNo, IsDTRBase, IsAtleastWithDTR, IsIncludeBonus, IsPerDay, StartDate, EndDate)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EHRANAllowance_WebSave", UserNo, tno, TransNo, PayIncomeTypeNo, Amount, PayscheduleNo, IsDTRBase, IsAtleastWithDTR, IsIncludeBonus, IsPerDay, StartDate, EndDate) > 0 Then
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

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpHRANList.aspx", "EHRAN") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"HRANAllowanceNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "EmpHRANList.aspx", "EHRAN") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"HRANAllowanceNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EHRANAllowance", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub


End Class
