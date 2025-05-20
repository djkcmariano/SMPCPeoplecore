Imports clsLib
Imports System.Data
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports Microsoft.VisualBasic.FileIO
Imports System.IO

Partial Class Secured_PayLastDTRList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim PayNo As Integer = 0
    Dim PayLastDetiNo As Integer = 0
    Dim EmployeeNo As Integer = 0
    Dim transNo As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)

        'Show or Hide Buttons
        If txtIsPosted.Checked = True Then
            lnkAdd.Visible = False
            lnkDelete.Visible = False
        Else
            lnkAdd.Visible = True
            lnkDelete.Visible = True
        End If

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayDTRLast_Web", UserNo, Session("PayLastList_PayNo"), PayLastDetiNo)
        grdMain.DataSource = _dt
        grdMain.DataBind()

        If ViewState("DTRDetiNo") = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"DTRDetiNo", "DTRCode"})
            ViewState("DTRDetiNo") = obj(0)
            lblDetl.Text = obj(1)
        End If

        PopulateGridDetl()
    End Sub
    Private Sub PopulateDropdown()
        Try
            'cboEmployeeNo.DataSource = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_LastPay", UserNo, TransNo)
            'cboEmployeeNo.DataTextField = "tDesc"
            'cboEmployeeNo.DataValueField = "tNo"
            'cboEmployeeNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        PayLastDetiNo = Generic.ToInt(Request.QueryString("id"))
        EmployeeNo = Generic.ToInt(Request.QueryString("employeeNo"))
        PayNo = Generic.ToInt(Request.QueryString("PayNo"))
        Permission.IsAuthenticatedCoreUser()
        PopulateTabHeader()
        HeaderInfo1.xFormName = "EPayLastDeti"

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
            PopulateDropdown()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateGridDetl()
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPay_WebTabHeader", UserNo, Session("PayLastList_PayNo"))
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
                'PayNo = Generic.ToInt(row("PayNo"))
                'EmployeeNo = Generic.ToInt(row("EmployeeNo"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "pnlPopup")
        Generic.EnableControls(Me, "pnlPopup", True)
        Try
            cboDTRNo.DataSource = SQLHelper.ExecuteDataSet("EDTR_WebLookup_Emp", UserNo, Session("PayLastList_EmployeeNo"))
            cboDTRNo.DataValueField = "tNo"
            cboDTRNo.DataTextField = "tdesc"
            cboDTRNo.DataBind()
        Catch ex As Exception

        End Try
        mdlShow.Show()
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayDTRNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EPayDTR", UserNo, item)
            i = i + 1
        Next

        If i > 0 Then
            PopulateGrid(True)
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim Retval As Boolean = False
        Dim PayDTRNo As Integer = Generic.ToInt(Me.txtPayDTRNo.Text)
        Dim DTRNo As Integer = Generic.ToInt(Me.cboDTRNo.SelectedValue)
        'Dim StartDate As String = Generic.ToStr(txtStartDate.Text)
        'Dim EndDate As String = Generic.ToStr(txtEndDate.Text)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EPayDTR_WebValidate", UserNo, PayDTRNo, DTRNo, Session("PayLastList_PayNo"), PayLastDetiNo, 0)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlShow.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EPayDTR_WebSave", UserNo, PayDTRNo, DTRNo, Session("PayLastList_PayNo"), PayLastDetiNo, Session("PayLastList_EmployeeNo")) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            'Response.Redirect("PayLastList.aspx")
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRDetiNo", "DTRCode"})
        ViewState("DTRDetiNo") = obj(0)
        lblDetl.Text = obj(1)

        PopulateGridDetl()

    End Sub

#Region "********Detail********"

    Private Sub PopulateGridDetl()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDTRDetiLog_Web", UserNo, 1, Generic.ToInt(ViewState("DTRDetiNo")), 0, 0)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

#End Region



End Class