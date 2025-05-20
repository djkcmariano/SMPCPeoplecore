Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_EmpWorkList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeExpe_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeExpe_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
            chkIsPresent_Checked()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))        
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateCombo()
            PopulateTabHeader()
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            PopulateControls()
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        'If SaveRecord() Then
        '    MessageBox.Success(MessageTemplate.SuccessSave, Me)
        '    PopulateGrid()
        'Else
        '    MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        'End If
        Dim ExpeTypeNo As Integer = Generic.ToInt(cboExpeTypeNo.SelectedValue)
        Dim NoOfYear As Double = 0
        Dim CurrentSalary As Double = Generic.ToDec(Me.txtCurrentSalary.Text)
        Dim EmployeeStatNo As Integer = Generic.ToInt(Me.cboEmployeeStatNo.SelectedValue)
        Dim Accredited As Double = 0
        Dim LWOP As Double = 0
        Dim FromDay As Integer = Generic.ToInt(cboFromDay.SelectedValue)
        Dim FromMonth As Integer = Generic.ToInt(cboFromMonth.SelectedValue)
        Dim FromYear As Integer = Generic.ToInt(txtFromYear.Text)
        Dim ToDay As Integer = Generic.ToInt(cboToDay.SelectedValue)
        Dim ToMonth As Integer = Generic.ToInt(cboToMonth.SelectedValue)
        Dim ToYear As Integer = Generic.ToInt(txtToYear.Text)
        Dim IsOtherExpe As Boolean = Generic.ToBol(txtIsOtherExpe.Checked)
        Dim OtherExpe As String = Generic.ToStr(txtOtherExpe.Text)
        Dim IsBSP As Boolean = Generic.ToBol(Me.chkIsBSP.Checked)

        Dim RetVal As Boolean = False
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EEmployeeExpe_WebValidate", UserNo, Generic.ToInt(txtCode.Text), TransNo, txtExpeComp.Text, ExpeTypeNo, txtPosition.Text, txtFromDate.Text, txtToDate.Text, txtRemark.Text, NoOfYear, _
                                     CurrentSalary, EmployeeStatNo, chkIsGov.Checked, Accredited, LWOP, txtExpeCompAdd.Text, txtCompPhone.Text, txtImmediateSuperior.Text, txtDuties.Text, 0, txtIndustry.Text, _
                                     FromDay, FromMonth, FromYear, ToDay, ToMonth, ToYear, IsOtherExpe, OtherExpe)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EEmployeeExpe_WebSave", UserNo, Generic.ToInt(txtCode.Text), TransNo, txtExpeComp.Text, ExpeTypeNo, txtPosition.Text, txtFromDate.Text, txtToDate.Text, txtRemark.Text, NoOfYear, _
                                     CurrentSalary, EmployeeStatNo, chkIsGov.Checked, Accredited, LWOP, txtExpeCompAdd.Text, txtCompPhone.Text, txtImmediateSuperior.Text, txtDuties.Text, 0, txtIndustry.Text, _
                                     FromDay, FromMonth, FromYear, ToDay, ToMonth, ToYear, IsOtherExpe, OtherExpe, txtSalaryLevel.Text, txtAccomplishment.Text, txtAllowances.Text, txtReasonsForLeaving.Text, chkIsPresent.Checked, Generic.ToInt(cboIndustryNo.SelectedValue), IsBSP, txtRatings.Text) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Generic.ClearControls(Me, "Panel1")
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeExpeNo"})))
            PopulateControls()
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeExpeNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeExpe", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Sub PopulateCombo()

        Try
            cboFromMonth.DataSource = SQLHelper.ExecuteDataSet("EMonth_WebLookup")
            cboFromMonth.DataValueField = "tNo"
            cboFromMonth.DataTextField = "tDesc"
            cboFromMonth.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboToMonth.DataSource = SQLHelper.ExecuteDataSet("EMonth_WebLookup")
            cboToMonth.DataValueField = "tNo"
            cboToMonth.DataTextField = "tDesc"
            cboToMonth.DataBind()
        Catch ex As Exception
        End Try


        Try
            cboFromDay.DataSource = SQLHelper.ExecuteDataSet("EDay_WebLookup")
            cboFromDay.DataValueField = "tNo"
            cboFromDay.DataTextField = "tDesc"
            cboFromDay.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboToDay.DataSource = SQLHelper.ExecuteDataSet("EDay_WebLookup")
            cboToDay.DataValueField = "tNo"
            cboToDay.DataTextField = "tDesc"
            cboToDay.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub PopulateControls()

        If txtIsOtherExpe.Checked = True Then
            txtOtherExpe.Enabled = True
            'txtOtherExpe.CssClass = "form-control required"
            cboExpeTypeNo.CssClass = "form-control"
            cboExpeTypeNo.Enabled = False
            cboExpeTypeNo.Text = ""
        Else
            txtOtherExpe.Enabled = False
            txtOtherExpe.Text = ""
            txtOtherExpe.CssClass = "form-control"
            'cboExpeTypeNo.CssClass = "form-control required"
            cboExpeTypeNo.Enabled = True
        End If

    End Sub

    Protected Sub txtIsOtherExpe_CheckedChanged(sender As Object, e As System.EventArgs) Handles txtIsOtherExpe.CheckedChanged
        PopulateControls()
        ModalPopupExtender1.Show()
    End Sub


    Protected Sub chkIsPresent_CheckedChanged(sender As Object, e As System.EventArgs) 'Handles chkIsPresent.CheckedChanged
        chkIsPresent_Checked()
        ModalPopupExtender1.Show()        
    End Sub

    Protected Sub chkIsPresent_Checked()
        If Me.chkIsPresent.Checked Then
            txtToDate.CssClass = "form-control"
            txtToDate.Text = ""
            txtToDate.Enabled = False
            lblTo.Attributes.Add("class", "col-md-4 control-label has-space")
        Else
            txtToDate.CssClass = "form-control required"
            txtToDate.Enabled = True
            lblTo.Attributes.Add("class", "col-md-4 control-label has-required")
        End If
    End Sub

End Class




