Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Partial Class SecuredSelf_SelfDTROTLimit
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0
    Dim OnlineEmpNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTROTLimit_WebSelf", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTROTLimit_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        OnlineEmpNo = Generic.ToInt(Session("EmployeeNo"))
        Permission.IsAuthenticated()
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList_Self(UserNo, Me, "pnlPopupDetl", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 12)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Try
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.EnableControls(Me, "pnlPopupDetl", True)

            lnkSave.Enabled = True
            mdlDetl.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTROTLimitNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Generic.EnableControls(Me, "pnlPopupDetl", IsEnabled)

            lnkSave.Enabled = IsEnabled
            mdlDetl.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTROTLimitNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTROTLimit", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim costcenterno As Integer = 0
        Dim Monthno As Integer = Generic.ToInt(Me.cboApplicableMonth.SelectedValue)
        Dim Limit As Integer = Generic.ToInt(Me.cboDTROTPercentLimitNo.SelectedValue)
        Dim statNo As Integer = 1 'Generic.ToInt(Me.cboApprovalStatNo.SelectedValue)
        Dim employeeno As Integer = OnlineEmpNo ' Generic.ToInt(Me.hifEmployeeNo.Value)
        Dim OTLimitNo As Integer = Generic.ToInt(txtDTROTLimitNo.Text)
        Dim AppYear As Integer = Generic.ToInt(txtApplicableYear.Text)
        Dim MaxHrs As Double = Generic.ToDbl(txtMaxHrs.Text)
        Dim IsCTO As Integer = Generic.ToInt(Me.txtIsCTO.Checked)
        Dim ComponentNo As Integer = 1 'Administrator

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EDTROTLimit_WebValidate", UserNo, OTLimitNo, employeeno, Monthno, AppYear, MaxHrs, Limit, Me.txtReason.Text.ToString, statNo, IsCTO, PayLocNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlDetl.Show()
            Exit Sub
        End If

        Dim fds As DataSet = SQLHelper.ExecuteDataSet("EDTROTLimit_WebSaveSelf", UserNo, OTLimitNo, employeeno, Monthno, AppYear, MaxHrs, Limit, Me.txtReason.Text.ToString, statNo, IsCTO, PayLocNo)
        If fds.Tables.Count > 0 Then
            If fds.Tables(0).Rows.Count > 0 Then
                Dim IsWithapprover As Boolean
                IsWithapprover = Generic.CheckDBNull(fds.Tables(0).Rows(0)("IsWithApprover"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                If IsWithapprover = True Then
                    RetVal = True
                Else
                    RetVal = False
                    MessageBox.Information("Successfully save but unable to locate the next approver.", Me)
                End If
            End If
        End If

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            'MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If

    End Sub


    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub


End Class



