Imports System.Data
Imports clsLib
Imports System.Threading
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO

Partial Class Secured_PayAutoJVList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim IsCompleted As Integer = 0
    Dim process_status As String = ""
    Dim err_num As Integer = 0

    Private Sub PopulateGrid()

        Dim tstatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayAutoJv_Web", UserNo, tstatus, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateGridDetl(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayAutoJVDeti_Web", UserNo, id)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 4)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception

        End Try

        Try
            Me.cboPayNo.DataSource = SQLHelper.ExecuteDataSet("EPay_WebLookup", UserNo, PayLocNo)
            Me.cboPayNo.DataTextField = "tdesc"
            Me.cboPayNo.DataValueField = "tno"
            Me.cboPayNo.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "panel1", PayLocNo)
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub


#Region "********Main*******"

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayAutoJVNo", "Code"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))

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

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayAutoJVNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EPayAutoJVDeti", UserNo, "PayAutoJVNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EPayAutoJV", UserNo, CType(item, Integer))
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
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer, IsEnabled As Boolean = False
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayAutoJVNo", "IsEnabled"})
                i = Generic.ToInt(obj(0))
                IsEnabled = Generic.ToBol(obj(1))
                Response.Redirect("~/secured/PayAutoJVEdit.aspx?id=" & i)
                'Dim dt As DataTable
                'dt = SQLHelper.ExecuteDataTable("EPayAutoJV_WebOne", UserNo, Generic.ToInt(i))
                'For Each row As DataRow In dt.Rows
                '    Generic.PopulateData(Me, "pnlPopupMain", dt)
                'Next
                'Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)
                'lnkSave.Enabled = IsEnabled

                'mdlMain.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/PayAutoJVEdit.aspx?id=" & TransNo & "&PayAutoJVNo=" & 0)
            'Generic.ClearControls(Me, "pnlPopupMain")
            'Generic.EnableControls(Me, "pnlPopupMain", True)
            'lnkSave.Enabled = True
            'mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    'Submit record
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtPayAutoJVNo.Text)
            Dim PayNo As Integer = Generic.ToInt(Me.cboPayNo.SelectedValue)
            Dim Description As String = Generic.ToStr(Me.txtDescription.Text)

            If SQLHelper.ExecuteNonQuery("EPayAutoJV_WebSave", UserNo, tno, PayNo, Description, PayLocNo) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                PopulateGrid()
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Count As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayAutoJVNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("zTable_WebPost", UserNo, "EPayAutoJV", CType(item, Integer)) Then
                    Count = Count + 1
                End If
            Next

            If Count > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + Count.ToString + ") " + MessageTemplate.SuccesPost, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub

    Protected Sub lnkProcess_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
                Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayAutoJVNo"})
                If fieldValues.Count > 1 Or fieldValues.Count = 0 Then
                    MessageBox.Warning("Please select 1 transaction to process.", Me)
                    Exit Sub
                End If
                If fieldValues.Count = 1 Then
                    For Each item As Integer In fieldValues
                        ViewState("Id") = item
                        AutoJVAppend(item)
                    Next
                End If

                If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, process_status, "EPayAutoJV", "EPayAutoJV_WebProcess", 4, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Critical(process_status, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPayAutoJV", "EPayAutoJV_WebProcess", 4, ViewState("Id"))
                    PopulateGrid()
                    process_status = Replace(process_status, "Command complete. Processing Time is :", "Processing completed at ")
                    MessageBox.Success(process_status, Me)
                End If
            Else
                MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkProcess_Detail_Init(sender As Object, e As EventArgs)
        Dim btn As LinkButton = TryCast(sender, LinkButton)
        Dim container As GridViewDataItemTemplateContainer = TryCast(btn.NamingContainer, GridViewDataItemTemplateContainer)
        btn.ID = "dxBtnView" + container.KeyValue
    End Sub

    Protected Sub lnkProcess_Detail_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayAutoJVNo"}))
            ViewState("Id") = i

            Dim IsPosted As Boolean = True
            IsPosted = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsPosted"}))

            If IsPosted = False Then
                AutoJVAppend(i)
                Dim strx As String = Replace(process_status, "'", "")
                If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, strx, "EPayAutoJV", "EPayAutoJV_WebProcess", 4, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Critical(strx, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPayAutoJV", "EPayAutoJV_WebProcess", 4, ViewState("Id"))
                    PopulateGrid()
                    process_status = Replace(process_status, "Command complete. Processing Time is :", "Processing completed at ")
                    MessageBox.Success(process_status, Me)
                End If
            Else
                MessageBox.Warning(MessageTemplate.PostedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If

    End Sub
    Private Sub AutoJVAppend(id As Integer)
        Dim xcmdProcSAVE As SqlClient.SqlCommand

        Try
            'clsbase.OpenConnectionAsyn()

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand
            '
            xcmdProcSAVE.CommandText = "EPayAutoJV_WebProcess"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = AssynChronous.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0


            xcmdProcSAVE.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@onlineuserno").Value = UserNo

            xcmdProcSAVE.Parameters.Add("@PayAutoJVNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@PayAutoJVNo").Value = Generic.ToInt(id)

            'AssynChronous.RunCommandAsynchronous(xcmdProcSAVE, "EPayAutoJV_WebProcess", SQLHelper.ConSTRAsyn, IsCompleted)
            process_status = AssynChronous.xRunCommandAsynchronous(xcmdProcSAVE, "EPayAutoJV_WebProcess", SQLHelper.ConSTRAsyn, IsCompleted, err_num)
            Session("IsCompleted") = 0 'IsCompleted


        Catch

        End Try

    End Sub

    Protected Sub lnkCreateDisk_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayAutoJVNo"})
            If fieldValues.Count > 1 Or fieldValues.Count = 0 Then
                MessageBox.Warning("Please select 1 transaction to process.", Me)
                Exit Sub
            End If
            If fieldValues.Count = 1 Then
                For Each item As Integer In fieldValues
                    txtId.Text = item
                Next
            End If

            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub
    Protected Sub lnkCreateDiskD_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            Dim lnk As New LinkButton, i As Integer, IsEnabled As Boolean = False
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayAutoJVNo", "IsEnabled"})
            txtid.text = Generic.ToInt(obj(0))

            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSendDisk_Click(sender As Object, e As EventArgs)
        GenerateJournal_Disk()
    End Sub

    Private Sub GenerateJournal_Disk()

        Try
            Dim FileHolder As FileInfo
            Dim WriteFile As StreamWriter
            Dim path As String = Page.MapPath("documents")
            Dim yr As String
            yr = Right(Year(Now), 2)



            Dim filename As String, fname As String = ""

            Dim dstext As DataSet, text As String
            dstext = SQLHelper.ExecuteDataSet("EPayAutoJV_WebDisk", UserNo, txtId.Text, Generic.ToInt(cboJVDefCate.Text))



            If dstext.Tables.Count > 0 Then

                If dstext.Tables(0).Rows.Count > 0 Then
                    fname = Generic.ToStr(dstext.Tables(0).Rows(0)("ffilename"))
                    filename = path & "\" & fname
                Else
                    filename = path & "\" & "Jv" & Pad.PadZero(2, Now.Month) + Pad.PadZero(4, Now.Year) + ".txt"
                End If
                If Not IO.Directory.Exists(path) Then
                    IO.Directory.CreateDirectory(path)
                End If

                FileHolder = New FileInfo(filename)
                WriteFile = FileHolder.CreateText()

                If dstext.Tables(1).Rows.Count > 0 Then
                    For i As Integer = 0 To dstext.Tables(1).Rows.Count - 1
                        text = Generic.ToStr(dstext.Tables(1).Rows(i)("Detail"))
                        WriteFile.WriteLine(text)
                    Next
                End If
                WriteFile.Close()
                DownloadFile(filename)
            Else
                MessageBox.Warning("no data!", Me)
            End If
            dstext = Nothing


            ' Response.Redirect("../Secured/documents/Jv" & Pad.PadZero(2, Now.Month) + Pad.PadZero(4, Now.Year) + ".txt")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DownloadFile(ByVal fullpath As String)

        Dim FileName As String = ""
        FileName = IO.Path.GetFileName(fullpath)
        Response.Clear()
        Response.ClearContent()
        Response.ContentType = "application/octet-stream"
        Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
        Response.TransmitFile(fullpath)
        Response.End()

    End Sub
    Protected Sub lnkOpenFile_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub
#End Region


#Region "********Detail********"

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

#End Region

End Class
