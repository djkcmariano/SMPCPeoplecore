Imports clsLib
Imports System.Data
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports Microsoft.VisualBasic.FileIO
Imports System.IO
Partial Class Secured_PayList_DTR
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim PayNo As Integer = 0
    'Dim PayLastDetiNo As Integer = 0
    Dim EmployeeNo As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)

        PayHeader.ID = Generic.ToInt(PayNo)

        'Show or Hide Buttons
        'If txtIsPosted.Checked = True Then
        '    lnkAdd.Visible = False
        '    lnkDelete.Visible = False
        'Else
        '    lnkAdd.Visible = True
        '    lnkDelete.Visible = True
        'End If

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayDTR_Web", UserNo, PayNo, 0)
        grdMain.DataSource = _dt
        grdMain.DataBind()

        If Generic.ToInt(ViewState("DTRDetiNo")) = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"DTRDetiNo", "DTRCode"})
            ViewState("DTRDetiNo") = obj(0)
            lblDetl.Text = obj(1)
        End If

        PopulateGridDetl()
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPay_WebOne", UserNo, PayNo)
        For Each row As DataRow In dt.Rows
            lnkAdd.Visible = Not Generic.ToBol(row("IsPosted"))
            lnkDelete.Visible = Not Generic.ToBol(row("IsPosted"))
        Next
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        PayNo = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticatedCoreUser()

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
            PopulateData()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateGridDetl()
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "pnlPopup")
        Generic.EnableControls(Me, "pnlPopup", True)
        Try
            cboDTRNo.DataSource = SQLHelper.ExecuteDataSet("EPayDTR_WebLookup", UserNo, PayNo)
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
        Dim EmployeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EPayDTR_WebValidate", UserNo, PayDTRNo, DTRNo, PayNo, 0, EmployeeNo)

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

        If SQLHelper.ExecuteNonQuery("EPayDTR_WebSave", UserNo, PayDTRNo, DTRNo, PayNo, 0, EmployeeNo) > 0 Then
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


    Protected Sub lnkViewShift_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As LinkButton
            lnk = sender

            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable

            dtx = SQLHelper.ExecuteDataTable("EShift_Web_DTRDetiLog_View", Generic.ToStr(lnk.Text))

            For Each rowx As DataRow In dtx.Rows
                messagedialog = Generic.ToStr(rowx("SQLString"))
            Next

            MessageBox.Alert(messagedialog, "information", Me, "topRight")


        Catch ex As Exception

        End Try
    End Sub


End Class
