Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports Microsoft.VisualBasic.FileIO
Imports System.IO

Partial Class Secured_PEReviewSummaryDetiList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        PEReviewSummaryMainHeader1.ID = Generic.ToInt(TransNo)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPEReviewSummary_Web", UserNo, TransNo, "")
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateHeader()

        Dim IsAllowEdit As Boolean = False
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEReviewSummaryMain_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            IsAllowEdit = Generic.ToBol(row("IsAllowEdit"))
        Next

        If IsAllowEdit = True Then
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
        TransNo = Generic.ToInt(Request.QueryString("TransNo"))
        AccessRights.CheckUser(UserNo, "PEReviewSummaryMainList.aspx", "EPEReviewSummaryMain")

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

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "PEReviewSummaryMainList.aspx", "EPEReviewSummaryMain") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEReviewSummaryNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPEReviewSummary", UserNo, CType(item, Integer))
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
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PEReviewSummaryMainList.aspx", "EPEReviewSummaryMain") Then
                Dim lnk As New LinkButton, i As Integer, IsEnabled As Boolean = False
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEReviewSummaryNo", "IsEnabled"})
                i = Generic.ToInt(obj(0))
                IsEnabled = Generic.ToBol(obj(1))

                'Clear Data
                Generic.ClearControls(Me, "pnlPopupMain")

                'Populate Data
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPEReviewSummary_WebOne", UserNo, Generic.ToInt(i))
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
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PEReviewSummaryMainList.aspx", "EPEReviewSummaryMain") Then
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

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PEReviewSummaryMainList.aspx", "EPEReviewSummaryMain") Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtPEReviewSummaryNo.Text)
            Dim averating As Double = Generic.ToDec(txtAveRating.Text)
            Dim EmployeeNo As Integer = Generic.ToInt(Me.hifEmployeeNo.Value)
            Dim AdjectivalRating As String = Generic.ToStr(txtAdjectivalRating.Text)

            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EPEReviewSummary_WebValidate", UserNo, tno, TransNo, EmployeeNo, averating, AdjectivalRating)

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

            If SQLHelper.ExecuteNonQuery("EPEReviewSummary_WebSave", UserNo, tno, TransNo, EmployeeNo, averating, AdjectivalRating) > 0 Then
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
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PEReviewSummaryMainList.aspx", "EPEReviewSummaryMain") Then
            Generic.ClearControls(Me, "Panel3")
            ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

#End Region


#Region "Upload"
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

            Dim amount As Double = 0, employeecode As String = ""
            If tProceed Then
                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0, employeeno As String, logtype As String = ""
                Dim fs As FileStream, fFilename As String
                fFilename = tpath & "\" & tfilename & "_" & filext 'tpath & "\" & tfilename
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                Dim rCode As String = ""
                Dim rRating As String = ""
                Dim AdjectivalRating As String = ""
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1
                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, ",")
                    employeeno = fspecArr(0)
                    rRating = fspecArr(2)
                    'rCode = fspecArr(3)
                    AdjectivalRating = fspecArr(3)
                    If i > 0 Then
                        If employeeno > "" And rRating <> "" Then
                            SQLHelper.ExecuteDataSet("EPEReviewSummary_WebUpload", UserNo, TransNo, employeeno, rRating, rCode, AdjectivalRating)
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
End Class
