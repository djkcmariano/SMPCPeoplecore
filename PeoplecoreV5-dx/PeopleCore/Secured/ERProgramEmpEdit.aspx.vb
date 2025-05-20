Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports Microsoft.VisualBasic.FileIO
Imports System.IO

Partial Class Secured_ERProgramDetiEdit
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim Attended As Integer = 0
    Dim Unattended As Integer = 0

    Private Sub PopulateGrid()

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EERProgramDeti_Web", UserNo, TransNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateHeader()

        Dim IsPosted As Boolean = False
        'Dim IsManual As Boolean = False
        'Dim dt As DataTable
        'dt = SQLHelper.ExecuteDataTable("EERProgramDetiMain_WebOne", UserNo, TransNo)
        'For Each row As DataRow In dt.Rows
        '    IsPosted = Generic.ToBol(row("IsPosted"))
        '    IsManual = Generic.ToBol(row("IsManual"))
        'Next

        If IsPosted = False Then
            lnkDelete.Visible = True
            lnkAdd.Visible = True
            lnkUpload.Visible = True
        Else
            lnkDelete.Visible = False
            lnkAdd.Visible = False
            lnkUpload.Visible = False
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "ERProgramList.aspx")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupAppend", PayLocNo)
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

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "ERProgramList.aspx") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ERProgramDetiNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EERProgramDeti", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "ERProgramList.aspx") Then
                Dim lnk As New LinkButton, i As Integer, IsEnabled As Boolean = False
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"ERProgramDetiNo", "IsEnabled"})
                i = Generic.ToInt(obj(0))
                IsEnabled = Generic.ToBol(obj(1))

                'Clear Data
                Generic.ClearControls(Me, "pnlPopupMain")

                'Populate Data
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EERProgramDeti_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                Next

                'Enabled or Disabled Controls
                Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)
                txtFullName.Enabled = False
                lnkSave.Enabled = IsEnabled
                mdlMain.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "ERProgramList.aspx") Then
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

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "ERProgramList.aspx") Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtERProgramDetiNo.Text)
            Dim EmployeeNo As Integer = Generic.ToInt(Me.hifEmployeeNo.Value)
            Dim Remark As String = Generic.ToStr(txtRemark.Text)
            Dim ActualCost As Double = Generic.ToDec(txtActualCost.Text)
            Dim IsAttended As Boolean = Generic.ToBol(chkIsAttended.Checked)
            Dim DateAttended As String = Generic.ToStr(txtDateAttended.Text)

            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EERProgramDeti_WebValidate", UserNo, tno, TransNo, EmployeeNo, Remark, ActualCost, IsAttended, DateAttended)

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("Invalid"))
                messagedialog = Generic.ToStr(rowx("MessageDialog"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                mdlMain.Show()
                Exit Sub
            End If

            If SQLHelper.ExecuteNonQuery("EERProgramDeti_WebSave", UserNo, tno, TransNo, EmployeeNo, Remark, ActualCost, IsAttended, DateAttended) > 0 Then
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

    Protected Sub lnkUpload_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "ERProgramList.aspx") Then
            Generic.ClearControls(Me, "Panel3")
            ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub


    Protected Sub ASPxGridView1_CustomSummaryCalculate(ByVal sender As Object, ByVal e As DevExpress.Data.CustomSummaryEventArgs) Handles grdMain.CustomSummaryCalculate
        ' Initialization.
        Dim currRow As Integer = e.RowHandle
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
            If e.Item.FieldName = "IsAttended" Then
                Attended = 0
                Unattended = 0
            End If

        End If
        ' Calculation.
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then

            If e.Item.FieldName = "IsAttended" Then
                If Convert.ToString(Generic.ToStr(grdMain.GetRowValues(currRow, "IsAttended"))) = "Yes" Then
                    Attended = Attended + 1
                Else
                    Unattended = Unattended + 1
                End If
            End If

        End If

        ' Finalization.
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize Then

            If e.Item.FieldName = "IsAttended" Then
                e.TotalValue = "Yes=" + Attended.ToString + "<br/>No=" + Unattended.ToString
            End If

        End If
    End Sub

#End Region


#Region "********Upload********"

    Protected Sub cboDateUpdateNo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboDateUpdateNo.SelectedIndexChanged

        If Generic.ToStr(cboDateUpdateNo.SelectedValue) = "1" Then
            lbl.Text = "File must be .csv(comma delimited) with following column : <br />Employee No., Employee Name, Remarks"
            divlbl.Style.Remove("display")
        ElseIf Generic.ToStr(cboDateUpdateNo.SelectedValue) = "2" Then
            lbl.Text = "File must be .csv(comma delimited) with following column : <br />Employee No., Employee Name, Actual Cost"
            divlbl.Style.Remove("display")
        ElseIf Generic.ToStr(cboDateUpdateNo.SelectedValue) = "3" Then
            lbl.Text = "File must be .csv(comma delimited) with following column : <br />Employee No., Employee Name, Attended, Date Attended"
            divlbl.Style.Remove("display")
        Else
            lbl.Text = ""
            divlbl.Style.Add("display", "none")
        End If

        ModalPopupExtender2.Show()

    End Sub

    Protected Sub lnkSave2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If PoplulateCSVFile() Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub
    Private Function PoplulateCSVFile() As Boolean
        Dim tsuccess As Integer = 0
        Try

            Dim Retval As Boolean = False
            Dim lastname As String = ""
            Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False
            Dim tpath As String = ""
            Dim datenow As Date
            datenow = Now()

            Dim filext As String = Pad.PadZero(2, Month(datenow)) & Pad.PadZero(2, Day(datenow)) & Pad.PadZero(4, Year(datenow)) & Pad.PadZero(2, Hour(datenow)) & Pad.PadZero(2, Minute(datenow)) & Pad.PadZero(4, Second(datenow))
            If fuFilename.HasFile = True Then
                tFilepath = fuFilename.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                Dim fileext As String = IO.Path.GetExtension(tFilepath)
                tProceed = True
                tpath = (Server.MapPath("documents")) 'Me.MapPath("documents") & "\
                If Not IO.Directory.Exists(tpath) Then
                    IO.Directory.CreateDirectory(tpath)
                End If
                fuFilename.SaveAs(tpath & "\" & tfilename & "_" & filext)
            End If

            If tProceed Then
                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0, employeeno As String
                Dim fs As FileStream, fFilename As String
                Dim remarks As String = "", actualcost As String = "", xattended As String = "", dateattended As String = ""
                fFilename = tpath & "\" & tfilename & "_" & filext
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1
                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, ",")
                    employeeno = fspecArr(0)

                    If Generic.ToStr(cboDateUpdateNo.SelectedValue) = "1" Then
                        remarks = fspecArr(2)
                    ElseIf Generic.ToStr(cboDateUpdateNo.SelectedValue) = "2" Then
                        actualcost = fspecArr(2)
                    ElseIf Generic.ToStr(cboDateUpdateNo.SelectedValue) = "3" Then
                        xattended = fspecArr(2)
                        dateattended = fspecArr(3)
                    End If

                    If i > 0 Then
                        If employeeno > "" Then
                            SQLHelper.ExecuteDataSet("EERProgramDeti_WebUpload", UserNo, TransNo, Generic.ToInt(cboDateUpdateNo.SelectedValue), employeeno, remarks, Generic.ToDec(actualcost), xattended, dateattended, PayLocNo)
                            tsuccess = tsuccess + 1
                        End If
                    End If

                    i = i + 1
                End While
                d.Close()
                If tsuccess > 0 Then
                    Retval = True
                End If
            Else
                Retval = False
            End If

            Return Retval
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region


#Region "********Append*******"

    Protected Sub lnkAppend_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "ERProgramList.aspx") Then
            Generic.ClearControls(Me, "pnlPopupAppend")
            cboFilteredbyNo.Text = 1
            drpAC.CompletionSetCount = 1
            mdlAppend.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub cboFilteredbyNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Try
            Dim fId As Integer
            fId = Generic.ToInt(Generic.CheckDBNull(cboFilteredbyNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType))
            txtName.Text = ""
            If fId > 0 Then
                txtName.Enabled = True
                drpAC.CompletionSetCount = fId
            Else
                txtName.Enabled = False
                drpAC.CompletionSetCount = 0
            End If

            mdlAppend.Show()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSaveAppend_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "ERProgramList.aspx") Then
            Dim Retval As Boolean = False
            Dim FilteredbyNo As Integer = Generic.ToInt(Me.cboFilteredbyNo.SelectedValue)
            Dim FilterValue As Integer = Generic.ToInt(Me.hiffilterbyid.Value)

            If SQLHelper.ExecuteNonQuery("EERProgramDeti_WebAppend", UserNo, TransNo, FilteredbyNo, FilterValue) >= 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Warning(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function populateDataDropdown(prefixText As String, count As Integer, contextKey As String) As List(Of String)
        Dim items As New List(Of String)()
        Dim _ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim clsbase As New clsBase.clsBaseLibrary
        Dim UserNo As Integer = 0, PayLocNo As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        PayLocNo = (HttpContext.Current.Session("xPayLocNo"))

        _ds = SQLHelper.ExecuteDataSet("EFilterBy_WebLookup_AC", UserNo, prefixText, PayLocNo, count)
        For Each row As DataRow In _ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("tDesc")),
                                Generic.ToStr(row("tNo")))
            items.Add(item)
        Next
        _ds.Dispose()
        Return items


    End Function

#End Region
End Class
