Imports System.Data
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web

Partial Class Secured_EmpRateSheet_ProjectOutput
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("BBSProjectClassRatePY_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable, pyModePaymentNo As Integer = 0
            dt = SQLHelper.ExecuteDataTable("BBSProjectClassRatePY_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)

                DayType(cboPayClassNo.SelectedValue)
                ActivityType_Deti(cboPYActivityTypeNo.SelectedValue)
                cboPYActivityTypeDetiNo.Text = Generic.ToInt(row("PYActivityTypeDetiNo"))
                cboDayTypeNo.Text = Generic.ToInt(row("DayTypeNo"))

                pyModePaymentNo = Generic.ToInt(row("pyModePaymentNo"))
            Next

            fRegisterStartupScript("Script", "disableenable_behind('" + pyModePaymentNo.ToString + "');")

        Catch ex As Exception

        End Try
    End Sub
   
    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupRate", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
       
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSProjectClassNo", "ProjectNo"})
            ViewState("TransNo") = obj(0)
           
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        SaveRecord()
    End Sub
    Private Sub SaveRecord()
        Dim payclassNo As Integer = Generic.ToInt(cboPayClassNo.SelectedValue)
        Dim pyActivityTypeNo As Integer = Generic.ToInt(cboPYActivityTypeNo.SelectedValue)
        Dim pyActivityTypeDetiNo As Integer = Generic.ToInt(cboPYActivityTypeDetiNo.SelectedValue)
        Dim billRate As Double = Generic.ToDec(txtBillingRate.Text)
        Dim CurrentSalary As Double = Generic.ToDec(txtCurrentSalary.Text)
        Dim LocationNo As Integer = Generic.ToInt(cboLocationNo.SelectedValue)
        Dim positionNo As Integer = Generic.ToInt(cboPositionNo.SelectedValue)
        Dim daytypeno As Integer = Generic.ToInt(cboDayTypeNo.SelectedValue)
        Dim departmentNo As Integer = Generic.ToInt(cboDepartmentNo.SelectedValue)

        Dim billRatehrs As Double = Generic.ToDec(txtBillingRateHrs.Text)
        Dim CurrentSalaryhrs As Double = Generic.ToDec(txtCurrentSalaryHrs.Text)
      '
        Dim dt As DataTable, RetVal As Boolean, error_num As Integer, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("BBSProjectClassRatePY_WebSave", UserNo, Generic.ToInt(txtCode.Text), Generic.ToInt(TransNo), _
                                             payclassNo, pyActivityTypeNo, pyActivityTypeDetiNo, billRate, CurrentSalary, LocationNo, positionNo, daytypeno, departmentNo, billRatehrs, CurrentSalaryhrs)
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
    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Generic.ClearControls(Me, "pnlPopupDetl")
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSProjectClassRatePYNo"})))
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
   

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    
    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BSProjectClassRatePYNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("BBSProjectClassRatePY", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
   
    Protected Sub cboPayClass_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        DayType(Generic.ToInt(cboPayClassNo.SelectedValue))
    End Sub
    Private Sub DayType(ino As Integer)
        Try
            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet("EDayType_WebLookup_PayClass", UserNo, Generic.ToInt(ino))
            cboDayTypeNo.DataSource = ds
            cboDayTypeNo.DataTextField = "tDesc"
            cboDayTypeNo.DataValueField = "tNo"
            cboDayTypeNo.DataBind()
            mdlDetl.Show()
        Catch ex As Exception
        End Try

    End Sub
    Private Sub ActivityType_Deti(ino As Integer)
        Try
            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet("EPYActivityTypeDeti_WebLookup", UserNo, Generic.ToInt(ino))
            cboPYActivityTypeDetiNo.DataSource = ds
            cboPYActivityTypeDetiNo.DataTextField = "tDesc"
            cboPYActivityTypeDetiNo.DataValueField = "tNo"
            cboPYActivityTypeDetiNo.DataBind()
            mdlDetl.Show()
        Catch ex As Exception
        End Try

    End Sub
    Protected Sub cboActivityType_SelectedIndexChanged(sender As Object, e As System.EventArgs) 'Handles cbofilterby.SelectedIndexChanged
        Try
            ActivityType_Deti(Generic.ToInt(cboPYActivityTypeNo.SelectedValue))
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub cboActivityTypeDeti_SelectedIndexChanged(sender As Object, e As System.EventArgs) 'Handles cbofilterby.SelectedIndexChanged
        Try
            Dim detiId As Integer = Generic.ToInt(cboPYActivityTypeDetiNo.SelectedValue)
            Dim pyModePaymentNo As Integer = 0
            Dim ds As DataSet = SQLHelper.ExecuteDataSet("Select pymodepaymentDesc,a.pyModePaymentNo From dbo.EPYmodePayment A Inner Join (Select pymodepaymentNo From dbo.EPYActivityTypeDeti Where PyActivityTypeDetiNo=" & detiId & " ) B On A.pymodepaymentNo=B.pymodepaymentNo ")
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    txtPYModePaymentDesc.Text = Generic.ToStr(ds.Tables(0).Rows(0)("pymodepaymentDesc"))
                    pyModePaymentNo = Generic.ToStr(ds.Tables(0).Rows(0)("pyModePaymentNo"))
                End If
            End If
            ds = Nothing
            fRegisterStartupScript("Script", "disableenable_behind('" + pyModePaymentNo.ToString + "');")
            mdlDetl.Show()
        Catch ex As Exception
        End Try
    End Sub
 
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub

End Class


