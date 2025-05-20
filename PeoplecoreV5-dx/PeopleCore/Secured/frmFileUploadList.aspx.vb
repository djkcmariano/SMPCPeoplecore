Imports System.Data
Imports clsLib

Partial Class Secured_frmFileUploadList
    Inherits System.Web.UI.Page

    Dim clscon As New clsGenericClass
    Dim xPublicVar As New clsPublicVariable
    Dim dsCount As Integer = 0
    Dim transNo As Integer = 0
    Dim RowNo As Integer

    Dim xScript As String = ""
    Dim _ds As New DataSet
    Dim _dt As New DataTable
    Dim ApplicantNo As Integer
    Dim EmployeeNo As Integer
    Dim showFrm As New clsFormControls

    Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        dsCount = 0
        _ds = SQLHelper.ExecuteDataSet("EFileDocApp_Web", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(Viewstate(xScript & "filter"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType), ApplicantNo, EmployeeNo)
        _dt = _ds.Tables(0)
        Dim dv As New Data.DataView(_dt)
        If SortExp <> "" Then
            ViewState(xScript & "SortExp") = SortExp
        End If
        If sordir <> "" Then

            ViewState(xScript & "sortdir") = sordir
        End If
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                dsCount = _ds.Tables(0).Rows.Count
                If ViewState(xScript & "SortExp") <> "" Then
                    dv.Sort = ViewState(xScript & "SortExp") + ViewState(xScript & "sortdir")
                End If
            End If
        End If

        Me.grdMain.SelectedIndex = Generic.CheckDBNull(Session(Left(xScript, Len(xScript) - 5)), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Me.grdMain.PageIndex = ViewState(xScript & "PageNo")
        Me.grdMain.DataSource = dv
        Me.grdMain.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("OnlineUserNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        AccessRights.CheckUser(xPublicVar.xOnlineUseNo, "EmpList.aspx", "EEmployee")
        transNo = Generic.CheckDBNull(Request.QueryString("transNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        ApplicantNo = Generic.CheckDBNull(Request.QueryString("ApplicantNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        EmployeeNo = Generic.CheckDBNull(Request.QueryString("EmployeeNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        xScript = Request.ServerVariables("SCRIPT_NAME")
        xScript = Generic.GetPath(xScript)


        If Not IsPostBack Then
            PopulateGrid()
        End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Viewstate(xScript & "PageNo") = 0
            Session(Left(xScript, Len(xScript) - 5)) = 0
            Viewstate(xScript & "filter") = Filter1.SearchText.ToString
            PopulateGrid()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMain.PageIndexChanging
        Viewstate(xScript & "No") = 0
        Viewstate(xScript & "PageNo") = e.NewPageIndex
        Session(Left(xScript, Len(xScript) - 5)) = 0
        PopulateGrid()
    End Sub
    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowAdd, "EmpList.aspx", "EEmployee") Then
            showFrm.clearFormControls_In_Popup(pnlPopupDetl)
            showFrm.EnableControls_in_Popup(pnlPopupDetl, True)
            btnSave.Enabled = True
            mdlDetl.Show()
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, chk As New CheckBox, lnk As New LinkButton
        Dim i As Integer, DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowDelete, "EmpList.aspx", "EEmployee") Then
            For i = 0 To Me.grdMain.Rows.Count - 1
                lbl = CType(grdMain.Rows(i).FindControl("lbl"), Label)
                chk = CType(grdMain.Rows(i).FindControl("chk"), CheckBox)
                lnk = CType(grdMain.Rows(i).FindControl("lnkDownload"), LinkButton)
                If chk.Checked = True Then
                    Generic.DeleteRecordAudit("EFileDocApp", xPublicVar.xOnlineUseNo, CType(lbl.Text, Integer))
                    DeleteCount = DeleteCount + 1
                    If IO.File.Exists(Server.MapPath(lnk.ToolTip)) Then
                        IO.File.Delete(Server.MapPath(lnk.ToolTip))
                    End If
                End If
            Next
            MessageBox.Success("There are (" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If

    End Sub

    Private Sub populatedata(tNo As Integer)
        Dim _ds As New DataSet
        _ds = SQLHelper.ExecuteDataSet("EFileDocApp_WebOne", xPublicVar.xOnlineUseNo, tNo)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                showFrm.showFormControls_In_Popup(pnlPopupDetl, _ds)
            End If
        End If


    End Sub
    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lnk As New LinkButton
            Dim i As String = "", fdtrNo As Integer = 0

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            RowNo = gvrow.RowIndex
            ViewState(Left(xScript, Len(xScript) - 5)) = RowNo

            i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
            Viewstate(xScript & "No") = i
            If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit, "EmpList.aspx", "EEmployee") Then
                populatedata(i)
                showFrm.EnableControls_in_Popup(pnlPopupDetl, True)
                mdlDetl.Show()
            Else
                MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If
        Catch ex As Exception
        End Try
    End Sub
   
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub
    Private Function SaveRecord() As Boolean

        Dim ActualPath As String
        Dim FileExt As String
        Dim ServerPath As String = ""
        Dim FileDocAppNo As Integer = Generic.CheckDBNull(txtfileNo.Text, Generic.enumObjectType.IntType)
        Dim _ds As New DataSet

        Try
            If FileUpload1.HasFile Then

                ActualPath = FileUpload1.PostedFile.FileName
                FileExt = IO.Path.GetExtension(ActualPath)
                _ds = SQLHelper.ExecuteDataSet("EFileDocApp_WebSave", xPublicVar.xOnlineUseNo, FileDocAppNo, ApplicantNo, Me.txtFileDesc.Text, ActualPath, FileExt, EmployeeNo)
                If _ds.Tables.Count > 0 Then
                    If _ds.Tables(0).Rows.Count > 0 Then
                        If FileUpload1.HasFile Then
                            ServerPath = Server.MapPath("../") & "Secured\documents\" & Generic.CheckDBNull(_ds.Tables(0).Rows(0)("FileDocAppNo"), clsBase.clsBaseLibrary.enumObjectType.IntType).ToString & FileExt
                            FileUpload1.PostedFile.SaveAs(ServerPath)
                            SaveRecord = True
                        End If

                    End If
                Else
                    SaveRecord = False
                End If
            End If
            
        Catch ex As Exception
            SaveRecord = False
        End Try
    End Function


    Private Function DownloadFile(ByVal FilePath As String) As Boolean

        'If IO.File.Exists(Server.MapPath(FilePath)) Then
        '    Dim FileName As String = ""
        '    FileName = IO.Path.GetFileNameWithoutExtension(FilePath).ToString.PadLeft(8, "0") & IO.Path.GetExtension(FilePath)
        '    Response.Clear()
        '    Response.ClearContent()
        '    Response.ContentType = "application/octet-stream"
        '    Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
        '    Response.TransmitFile(FilePath)
        '    Response.End()
        '    Return True
        'Else
        '    Return False
        'End If
        If IO.File.Exists(Server.MapPath(FilePath)) Then
            Dim FileName As String = ""
            FileName = IO.Path.GetFileName(FilePath)
            Response.Clear()
            Response.ClearContent()
            Response.ContentType = "application/octet-stream"
            Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
            Response.TransmitFile(FilePath)
            Response.End()
        Else
            Return False
        End If
    End Function

    Protected Sub lnkDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

        
        Dim lnk As New LinkButton, img As New Image, lbl As New Label
        lnk = sender

        If DownloadFile(lnk.ToolTip) = False Then
                MessageBox.Information("File is no longer available.", Me)
        End If
        Catch ex As Exception
            MessageBox.Critical("Error.", Me)
        End Try
    End Sub
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
    Protected Sub addTrigger_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim btnPreview As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(btnPreview)
    End Sub
End Class

