Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_DTRDTRScheduler
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0    
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRScheduler_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo, Generic.ToStr(txtDate.Text), Generic.ToStr(txtDate2.Text))
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))        
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        PopulateGrid()        
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDown()        
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 4)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRSchedulerNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTRScheduler", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        PoplulateCSVFile()
    End Sub

    Protected Sub lnkPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRSchedulerNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("EDTRScheduler_WebPost", UserNo, item) Then
                    i = i + 1
                End If
            Next            
            If i > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccesPost, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Private Sub PoplulateCSVFile()
        Dim tsuccess As Integer = 0
        Try
            Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False
            Dim tpath As String = ""
            Dim datenow As Date
            datenow = Now()

            Dim filext As String = Pad.PadZero(2, Month(datenow)) & Pad.PadZero(2, Day(datenow)) & Pad.PadZero(4, Year(datenow)) & Pad.PadZero(2, Hour(datenow)) & Pad.PadZero(2, Minute(datenow)) & Pad.PadZero(4, Second(datenow))
            If txtFile.HasFile = True Then
                tFilepath = txtFile.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                Dim fileext As String = IO.Path.GetExtension(tFilepath)
                tProceed = True
                tpath = (Server.MapPath("documents"))
                If Not IO.Directory.Exists(tpath) Then
                    IO.Directory.CreateDirectory(tpath)
                End If
                txtFile.SaveAs(tpath & "\" & tfilename & "_" & filext)

            End If

            If tProceed Then

                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0, remarks As String = "", storeNo As String = ""
                Dim fs As FileStream, fFilename As String
                fFilename = tpath & "\" & tfilename & "_" & filext
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)

                Dim EmployeeCode As String = ""
                Dim DTRDate As String = ""
                Dim xIn As String = ""
                Dim xOut As String = ""
                Dim IsRD As Boolean = False
                Dim DayType As String = ""

                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1
                    nfile = d.ReadLine()
                    If i > 0 Then
                        fspecArr = Split(nfile, Chr(9))
                        EmployeeCode = fspecArr(0)
                        DTRDate = fspecArr(1)
                        xIn = fspecArr(2)
                        xOut = fspecArr(3)
                        remarks = fspecArr(4)
                        storeNo = fspecArr(5)



                        If EmployeeCode <> "" And DTRDate <> "" Then
                            If SQLHelper.ExecuteNonQuery("EDTRScheduler_WebUpload", UserNo, EmployeeCode, DTRDate, xIn, xOut, remarks, storeNo, PayLocNo) Then
                                tsuccess = tsuccess + 1
                            End If
                        End If
                    End If

                    i = i + 1
                End While
                d.Close()
                MessageBox.Success(tsuccess.ToString & " schedules uploaded.", Me)
                PopulateGrid()
            Else
                MessageBox.Alert("Error uploading.", "error", Me)
            End If
        Catch ex As Exception
            MessageBox.Alert("Error in line number : " & tsuccess.ToString, "error", Me)
        End Try
    End Sub

End Class


