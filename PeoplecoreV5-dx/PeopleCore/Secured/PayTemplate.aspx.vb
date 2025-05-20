Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_PayTemplate
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0


#Region "Mail"
    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayTemplate_Web", UserNo, TransNo, 1, PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        'Tab.menuStyle = "TabRef"
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateDropDownList()
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    'Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '        Response.Redirect("~/secured/PayTemplateedit.aspx?id=0")
    '    Else
    '        MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
    '    End If
    'End Sub

    'Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)        
    '    Dim lnk As New LinkButton
    '    lnk = sender
    '    Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
    '    Response.Redirect("~/secured/PayTemplateedit.aspx?id=" & container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTemplateNo"}).ToString())
    'End 

    Private Sub PopulateDropDownList()
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", Generic.ToInt(Session("xPayLocNo")))
        Generic.PopulateDropDownList(UserNo, Me, "panel1", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboPayScheduleNo.DataSource = SQLHelper.ExecuteDataSet("EPaySchedule_WebLookup_PayTemplate", UserNo)
            cboPayScheduleNo.DataTextField = "tDesc"
            cboPayScheduleNo.DataValueField = "tNo"
            cboPayScheduleNo.DataBind()
        Catch ex As Exception

        End Try
    End Sub
   
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.EnableControls(Me, "pnlPopupDetl", True)
            lnkSave.Enabled = True
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub
    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim INo As String = Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTemplateNo"}))
            PopulateData(INo)
            Generic.EnableControls(Me, "pnlPopupDetl", True)
            lnkSave.Enabled = True
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayTemplateNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayTemplate", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim payclassNo As Integer = Generic.ToInt(cboPayClassNo.SelectedValue)
        Dim payTemplateNo As Integer = Generic.ToInt(txtCode.Text)
        Dim payScheduleNo As Integer = Generic.ToInt(cboPayScheduleNo.SelectedValue)
        Dim payCateNo As Integer = 1
        Dim paySourceNo As Integer = Generic.ToInt(cboPaySourceNo.SelectedValue)
        Dim isDeductTax As Boolean = Generic.ToBol(txtIsDeductTax.Checked)
        Dim isDeductSSS As Boolean = Generic.ToBol(txtIsDeductSSS.Checked)
        Dim isDeductPH As Boolean = Generic.ToBol(txtIsDeductPH.Checked)
        Dim isDeductHDMF As Boolean = Generic.ToBol(txtIsDeductHDMF.Checked)

        Dim IsAttendanceBase As Boolean = Generic.ToBol(txtIsAttendanceBase.Checked)
        Dim IsIncludeForw As Boolean = Generic.ToBol(txtIsIncludeForw.Checked)
        Dim IsIncludeMass As Boolean = Generic.ToBol(txtIsIncludeMass.Checked)
        Dim IsIncludeOther As Boolean = Generic.ToBol(txtIsIncludeOther.Checked)
        Dim IsIncludeLoan As Boolean = Generic.ToBol(txtIsIncludeLoan.Checked)

        Dim IsRATA As Boolean = Generic.ToBol(txtIsRATA.Checked)
        Dim IsLoyalTy As Boolean = Generic.ToBol(txtIsLoyalty.Checked)
        Dim IsMedical As Boolean = Generic.ToBol(txtIsMedical.Checked)
        Dim IsRice As Boolean = Generic.ToBol(txtIsRice.Checked)

        Dim isDeductPF As Boolean = Generic.ToBol(txtIsDeductPF.Checked)
        Dim isDeductIHP As Boolean = Generic.ToBol(txtIsDeductIHP.Checked)


        Dim RetVal As Boolean = True
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        'dtx = SQLHelper.ExecuteDataTable("EPayTemplate_WebValidate", UserNo, DTRDayOffNo, EmployeeNo, DateFrom, DateTo, DayOffNo, DayOffNo2, DayOffNo3, ApprovalStatNo, PayLocNo, ComponentNo)

        'For Each rowx As DataRow In dtx.Rows
        '    invalid = Generic.ToBol(rowx("tProceed"))
        '    messagedialog = Generic.ToStr(rowx("xMessage"))
        '    alerttype = Generic.ToStr(rowx("AlertType"))
        'Next

        'If invalid = True Then
        '    MessageBox.Alert(messagedialog, alerttype, Me)
        '    mdlDetl.Show()
        '    Exit Sub
        'End If

        Dim dt As DataTable, error_num As Integer = 0, error_message As String = ""

        dt = SQLHelper.ExecuteDataTable("EPayTemplate_WebSave", UserNo, payTemplateNo, payclassNo, payScheduleNo, payCateNo, paySourceNo, isDeductTax, isDeductSSS, isDeductPH, isDeductHDMF, IsAttendanceBase, IsIncludeForw, IsIncludeMass, IsIncludeOther, IsIncludeLoan, PayLocNo, IsRATA, IsLoyalTy, IsMedical, IsRice, isDeductPF, isDeductIHP)
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



    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateData(iNo As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayTemplate_WebOne", UserNo, iNo)
        Generic.PopulateData(Me, "pnlPopupDetl", dt)
    End Sub
    Protected Sub cboPaySchedule_TextChanged(sender As Object, e As EventArgs)
        Try
            Dim dt As DataTable
            Dim paycateno As Integer = 1
            Dim paySourceNo As Integer = Generic.ToInt(Me.cboPaySourceNo.SelectedValue)
            Dim payClassNo As Integer = Generic.ToInt(Me.cboPayClassNo.SelectedValue)
            Dim payScheduleNo As Integer = Generic.ToInt(Me.cboPayScheduleNo.SelectedValue)

            Me.txtIsDeductHDMF.Checked = False
            Me.txtIsDeductPH.Checked = False
            Me.txtIsDeductSSS.Checked = False


            dt = SQLHelper.ExecuteDataTable("EPayClass_Web_Select", UserNo, payClassNo, payScheduleNo, PayLocNo)
            For Each row As DataRow In dt.Rows
                txtIsDeductHDMF.Checked = Generic.ToBol(row("IsDeductHDMF"))
                txtIsDeductPH.Checked = Generic.ToBol(row("IsDeductPH"))
                txtIsDeductSSS.Checked = Generic.ToBol(row("IsDeductSSS"))

            Next
            mdlDetl.Show()
        Catch ex As Exception

        End Try
        mdlDetl.Show()
    End Sub
#End Region


End Class
