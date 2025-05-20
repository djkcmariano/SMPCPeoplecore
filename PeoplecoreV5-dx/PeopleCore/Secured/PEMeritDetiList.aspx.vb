Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PEMeritDetiList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Dim xBase As New clsBase.clsBaseLibrary
    Dim IsCompleted As Integer = 0

    Dim AmountIncreaseAdj As Double = 0
    Dim DiffRate As Double = 0
    Dim OldRate As Double = 0
    Dim NewRate As Double = 0
    Dim PercentIncrease As Double = 0

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPEMeritDeti_Web", UserNo, TransNo, "")
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateHeader()

        Dim IsPosted As Boolean = False
        Dim IsBonus As Boolean = False
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEMerit_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            IsPosted = Generic.ToBol(row("IsPosted"))
            IsBonus = Generic.ToBol(row("IsBonus"))
        Next

        If IsBonus = True Then
            grdMain.Columns("Old Salary").Caption = "Salary"
            grdMain.Columns("New Salary").Caption = "Bonus"
            grdMain.Columns("Status").Visible = True
            grdMain.Columns("Exc.").Visible = False
            lnkDelete.Visible = False
            lnkPostHRAN.Visible = False
        Else
            If IsPosted = True Then
                grdMain.Columns("Status").Visible = True
                grdMain.Columns("Exc.").Visible = False
                lnkDelete.Visible = False
                lnkPostHRAN.Visible = True
            Else
                grdMain.Columns("Status").Visible = False
                grdMain.Columns("Exc.").Visible = True
                lnkDelete.Visible = True
                lnkPostHRAN.Visible = False
            End If
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("TransNo"))
        AccessRights.CheckUser(UserNo, "PEMeritList.aspx")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            PopulateHeader()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

#Region "********Main*******"

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub grdMain_PreRender(sender As Object, e As System.EventArgs) Handles grdMain.PreRender
        Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)
        For i As Integer = 0 To grid.VisibleRowCount - 1
            Dim isSelected As Boolean = Convert.ToBoolean(grdMain.GetRowValues(i, "IsExcluded"))
            If isSelected Then
                grid.Selection.SelectRow(i)
            End If
        Next i
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "PEMeritList.aspx") Then
            Dim tno As Integer = 0, i As Integer = 0
            Dim IsSelected As Boolean

            For j As Integer = 0 To grdMain.VisibleRowCount - 1
                tno = Generic.ToInt(grdMain.GetRowValues(j, "PEMeritDetiNo"))
                If grdMain.Selection.IsRowSelected(j) Then
                    IsSelected = True
                Else
                    IsSelected = False
                End If
                SQLHelper.ExecuteNonQuery("EPEMeritDeti_WebExclude", UserNo, tno, IsSelected)
                grdMain.Selection.UnselectRow(j)
                i = i + 1
            Next

            If i > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If


    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PEMeritList.aspx") Then
                Dim lnk As New LinkButton, i As Integer, IsEnabled As Boolean = False
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEMeritDetiNo", "IsEnabled"})
                i = Generic.ToInt(obj(0))
                IsEnabled = Generic.ToBol(obj(1))

                'Clear Data
                Generic.ClearControls(Me, "pnlPopupMain")

                'Populate Data
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPEMeritDeti_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                Next

                'Enabled or Disabled Controls
                Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)
                lnkSave.Enabled = IsEnabled

                mdlMain.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PEMeritList.aspx") Then
            Generic.ClearControls(Me, "pnlPopupMain")
            Generic.EnableControls(Me, "pnlPopupMain", True)
            lnkSave.Enabled = True
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    'Submit record
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PEMeritList.aspx") Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtPEMeritDetiNo.Text)
            Dim percentIncAdj As Double = Generic.ToDec(txtPercentIncreaseAdj.Text)
            Dim AmountIncreaseAdj As Double = Generic.ToDec(txtAmountIncreaseAdj.Text)
            Dim IsExcluded As Boolean = Generic.ToBol(txtIsExcluded.Checked)

            If SQLHelper.ExecuteNonQuery("EPEMeritDeti_WebSave", UserNo, tno, percentIncAdj, AmountIncreaseAdj, IsExcluded) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkPostHRAN_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            HRANAppendAsyn()
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessProcess & " " & Now().ToString, Me)
        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If

    End Sub

    Private Sub HRANAppendAsyn()
        Dim xcmdProcSAVE As SqlClient.SqlCommand

        Try

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand

            xcmdProcSAVE.CommandText = "EPEMerit_WebPostHRAN"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = xBase.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0

            xcmdProcSAVE.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@onlineuserno").Value = Generic.CheckDBNull(UserNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            xcmdProcSAVE.Parameters.Add("@PEMeritNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@PEMeritNo").Value = Generic.CheckDBNull(TransNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            xBase.RunCommandAsynchronous(xcmdProcSAVE, "EPEMerit_WebPostHRAN", SQLHelper.ConSTRAsyn, IsCompleted)
            Session("IsCompleted") = 0 'IsCompleted

            If Session("IsCompleted") = 1 Then
                'clsModalControls.SetModalPopupControls(CType(Master.FindControl("cphBody"), ContentPlaceHolder), "completed")
            End If
        Catch
            'Response.RedirectLocation = Session("xFormname") & "?IsClickMain=" & IsClickMain
        End Try

    End Sub

    Protected Sub ASPxGridView1_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles grdMain.CustomSummaryCalculate
        ' Initialization.
        Dim currRow As Integer = e.RowHandle
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
            If e.Item.FieldName = "AmountIncreaseAdj" Then
                AmountIncreaseAdj = 0
            End If

            If e.Item.FieldName = "NewSalary" Then
                DiffRate = 0
                OldRate = 0
                NewRate = 0
                PercentIncrease = 0
            End If
        End If
        ' Calculation.
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then

            If e.Item.FieldName = "AmountIncreaseAdj" Then
                If Convert.ToDouble(Generic.ToDec(grdMain.GetRowValues(currRow, "AmountIncreaseAdj"))) > 0 Then
                    AmountIncreaseAdj += Convert.ToDouble(Generic.ToDec(grdMain.GetRowValues(currRow, "AmountIncreaseAdj")))
                End If
            End If

            If e.Item.FieldName = "NewSalary" Then
                OldRate += Convert.ToDouble(Generic.ToDec(grdMain.GetRowValues(currRow, "CurrentSalary")))
                NewRate += Convert.ToDouble(Generic.ToDec(grdMain.GetRowValues(currRow, "NewSalary")))

            End If
            'If (grdMain.Selection.IsRowSelectedByKey(e.GetValue(grdMain.KeyFieldName))) Then
            '    totalSum = totalSum + Convert.ToInt32(e.FieldValue)
            'End If

            'If (e.IsGroupSummary) Then
            '    int timesheets = Convert.ToInt32(e.GetGroupSummary(e.GroupRowHandle, grdMain.GroupSummary["CurrentSalary"]));
            'End If
        End If

        ' Finalization.
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize Then

            If e.Item.FieldName = "AmountIncreaseAdj" Then
                e.TotalValue = "Sum=" + AmountIncreaseAdj.ToString
            End If

            If e.Item.FieldName = "NewSalary" Then
                OldRate = grdMain.GetTotalSummaryValue(grdMain.TotalSummary("CurrentSalary"))
                NewRate = grdMain.GetTotalSummaryValue(grdMain.TotalSummary("NewSalary"))
                DiffRate = NewRate - OldRate
                PercentIncrease = Format(Convert.ToDouble((DiffRate / OldRate) * 100), "0.00")
                e.TotalValue = PercentIncrease.ToString + "% Inc."
            End If
        End If
    End Sub

#End Region
End Class



