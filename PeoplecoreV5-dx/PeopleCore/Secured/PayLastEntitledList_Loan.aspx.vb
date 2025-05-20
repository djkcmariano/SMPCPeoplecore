Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting

Partial Class Secured_PayLastEntitledList_Loan
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer = 0
    Dim PaylastDetino As Integer = 0

    Private Sub PopulateGrid()

        If txtIsPosted.Checked = True Then
            lnkAdd.Visible = False
            lnkDelete.Visible = False
        Else
            lnkAdd.Visible = True
            lnkDelete.Visible = True
        End If

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayLastEntitledLoan_Web", UserNo, PaylastDetino)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        PaylastDetino = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticatedCoreUser()
        PopulateTabHeader()
        HeaderInfo1.xFormName = "EPayLastDeti"

        If Not IsPostBack Then
            PopulateDropdownList()
        End If

        PopulateGrid()

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            'dt = SQLHelper.ExecuteDataTable("EPayLastDeti_WebTabHeader", UserNo, PayLastDetiNo)
            dt = SQLHelper.ExecuteDataTable("EPay_WebTabHeader", UserNo, Session("PayLastList_PayNo"))
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
                TransNo = Generic.ToInt(row("PayNo"))
            Next
        Catch ex As Exception

        End Try
    End Sub
    'Populate Combo box
    Private Sub PopulateDropdownList()
        Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
        Generic.PopulateDropDownList(UserNo, Me, "phbonus", PayLocNo)
        Try
            cboPayDeductTypeNo.DataSource = SQLHelper.ExecuteDataSet("ELoanType_WebLookup", UserNo, PayLocNo)
            cboPayDeductTypeNo.DataValueField = "tNo"
            cboPayDeductTypeNo.DataTextField = "tDesc"
            cboPayDeductTypeNo.DataBind()
        Catch ex As Exception
        End Try


    End Sub
    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayLastEntitledLoan_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
    End Sub


    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lnk As New LinkButton, IsEnabled As Boolean = False
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Generic.ClearControls(Me, "Panel2")
        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayLastEntitledLoanNo"})))

        'Enable or Disable Controls
        If txtIsPosted.Checked = True Then
            IsEnabled = False
        Else
            IsEnabled = True
        End If
        Generic.EnableControls(Me, "Panel2", IsEnabled)
        btnSave.Enabled = IsEnabled

        mdlShow.Show()
      
    End Sub



    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)


        PopulateDropdownList()
        Generic.ClearControls(Me, "Panel2")
        mdlShow.Show()
      
    End Sub


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayLastEntitledLoanNo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EPayLastEntitledLoan", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()

    End Sub
    'Submit record
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecord() As Integer
        Dim paydeducttypeno As Integer = Generic.ToInt(cboPayDeductTypeNo.SelectedValue)
        Dim dt As DataTable, error_num As Integer = 0, error_message As String = "", retVal As Boolean = False

        dt = SQLHelper.ExecuteDataTable("EPayLastEntitledLoan_WebSave", UserNo, Generic.CheckDBNull(txtPayLastEntitledLoanNo.Text, clsBase.clsBaseLibrary.enumObjectType.IntType), TransNo, PaylastDetino, paydeducttypeno, Generic.ToInt(txtIsDeductInFull.Checked), Generic.ToInt(txtIsSuspend.Checked))

        For Each row As DataRow In dt.Rows
            retVal = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                retVal = False
            End If

        Next
        If retVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If retVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Function

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







