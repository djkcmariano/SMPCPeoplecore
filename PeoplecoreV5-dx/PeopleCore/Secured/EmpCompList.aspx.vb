Imports System.Data
Imports clsLib

Partial Class Secured_EmpCompList
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim employeeno As Integer = 0

    Dim showFrm As New clsFormControls
    Dim SQLHelp As New clsBase.SQLHelper
    Dim xScript As String
    Dim _dt As New DataTable
    Dim IsClickMain As Integer = 0
    Dim clsGeneric As New clsGenericClass

    Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Try
            Dim dscount As Double = 0
            Dim _ds As DataSet
            _ds = SQLHelper.ExecuteDataset("EEmployeeComp_Web", xPublicVar.xOnlineUseNo, employeeno)
            _dt = _ds.Tables(0)
            Dim dv As New Data.DataView(_dt)
            If SortExp <> "" Then
                Session(xScript & "SortExp") = SortExp
            End If
            If sordir <> "" Then

                Session(xScript & "sortdir") = sordir
            End If
            If _ds.Tables.Count > 0 Then
                If _ds.Tables(0).Rows.Count > 0 Then
                    dscount = _ds.Tables(0).Rows.Count
                    If Session(xScript & "SortExp") <> "" Then
                        dv.Sort = Session(xScript & "SortExp") + Session(xScript & "sortdir")
                    End If
                End If
            End If
            If IsMain Then
                Session(xScript & "Pageno") = 0
                Session(Left(xScript, Len(xScript) - 5)) = 0
            End If

            Me.grdMain.SelectedIndex = Generic.CheckDBNull(Session(Left(xScript, Len(xScript) - 5)), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
            Me.grdMain.PageIndex = Session(xScript & "Pageno")
            Me.grdMain.DataSource = dv
            Me.grdMain.DataBind()


        Catch ex As Exception
        End Try
    End Sub
    Private Sub populatedata(depeNo As Integer)
        Dim _ds As New DataSet
        _ds = SQLHelper.ExecuteDataset("EEmployeeComp_WebOne", xPublicVar.xOnlineUseNo, depeNo)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                showFrm.showFormControls_In_Popup(pnlPopupDetl, _ds)
            End If
        End If
        If depeNo > 0 Then
            Me.txtemployeeCompNo.Text = Pad.PadZero(8, depeNo)
        Else
            Me.txtEmployeeCompNo.Text = "Autonumber"
        End If

    End Sub
    Private Sub PopulateName()
        img.ImageUrl = "../secured/frmShowImage.ashx?tNo=" & employeeno & "&tIndex=2"

    End Sub
    Protected Sub lnkUploadPhoto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkUploadPhoto.Click
        Try
            mdlUpload.Show()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkUploadSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tStatus As Integer = PoplulateCSVFile()
        If tStatus = 1 Then
            MessageBox.Warning("Uploading of file successfully done.", Me)
        ElseIf tStatus = 2 Then
            MessageBox.Warning("Error uploading", Me)

        End If

    End Sub

    Private Function PoplulateCSVFile() As Integer
        Dim lastname As String = ""
        Dim FormatedDate As String = ""
        Dim NewFileName As String = ""
        Dim FileExt As String = ""
        Dim ServerDIR As String = ""
        Dim ds As New DataSet
        Dim filesize As Integer = 0

        Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False

        If fuPhoto.HasFile = True Then
            FileExt = IO.Path.GetExtension(fuPhoto.PostedFile.FileName)
            filesize = fuPhoto.PostedFile.ContentLength
            If filesize > 2097152 Then

                MessageBox.Warning("Maximum allowable file to upload is only 2MB.", Me)
                Exit Function
            End If

            If FileExt = ".jpg" Or FileExt = ".jpeg" Or FileExt = ".gif" Or FileExt = ".png" Or FileExt = ".JPG" Or FileExt = ".JPEG" Or FileExt = ".GIF" Or FileExt = ".PNG" Then


                tFilepath = fuPhoto.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                FileExt = IO.Path.GetExtension(tfilename)
                ds = SQLHelper.ExecuteDataSet("EmployeePhoto_WebUpdate", employeeno, FileExt)
                Dim empcode$ = ""

                If ds.Tables.Count <> 0 Then
                    If ds.Tables(0).Rows.Count <> 0 Then
                        empcode$ = Generic.CheckDBNull(ds.Tables(0).Rows(0)("Employeecode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    End If
                End If
                ServerDIR = Server.MapPath("../") & "EmployeeImage\" & empcode$ & FileExt
                fuPhoto.PostedFile.SaveAs(ServerDIR)
                'Save the photo into database for the report purposes
                clsGeneric.SaveRecordPhoto(ServerDIR, employeeno)
                Return 1
            Else
                MessageBox.Warning("Invalid image format'", Me)
                Return 3
            End If
        Else
            Return 2
        End If
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("onlineuserno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        AccessRights.CheckUser(xPublicVar.xOnlineUseNo)

        employeeno = Generic.CheckDBNull(Request.QueryString("id"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        IsClickMain = 0
        xScript = Request.ServerVariables("SCRIPT_NAME")
        xScript = Generic.GetPath(xScript)


        If Not IsPostBack Then
            If IsClickMain = 1 Then
                PopulateGrid(True)
            Else
                PopulateGrid(False)
            End If
            PopulateName()
        End If


        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub
    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMain.PageIndexChanging
        Session(xScript & "No") = 0
        Session(xScript & "PageNo") = e.NewPageIndex
        Session(Left(xScript, Len(xScript) - 5)) = 0
        PopulateGrid()
    End Sub
    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lnk As New ImageButton
            Dim i As String = "", rowNo As Integer = 0

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            rowno = gvrow.RowIndex
            Session(Left(xScript, Len(xScript) - 5)) = rowNo
            i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()

            If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit) Then
                populatedata(i)
                showFrm.EnableControls_in_Popup(pnlPopupDetl, True)
                showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupDetl, Session("xPayLocNo"))
                mdlDetl.Show()
            Else
                MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowAdd) Then
            showFrm.clearFormControls_In_Popup(pnlPopupDetl)
            showFrm.EnableControls_in_Popup(pnlPopupDetl, True)
            showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupDetl, Session("xPayLocNo"))
            Me.txtemployeeCompCode.Text = "Autonumber"
            mdlDetl.Show()
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If
    End Sub
    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For tcount = 0 To Me.grdMain.Rows.Count - 1
                lbl = CType(grdMain.Rows(tcount).FindControl("lblId"), Label)
                tcheck = CType(grdMain.Rows(tcount).FindControl("txtIsSelect"), CheckBox)
                If tcheck.Checked = True Then
                    Generic.DeleteRecordAudit("EEmployeeComp", xPublicVar.xOnlineUseNo, CType(lbl.Text, Integer))
                    DeleteCount = DeleteCount + 1
                End If
            Next
            MessageBox.Success("There are (" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If

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

        Dim tcompno As Integer = Generic.CheckDBNull(Me.cboCompNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim scaleno As Integer = Generic.CheckDBNull(Me.cboCompScaleNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim anchorno As Integer = 0

        If SQLHelper.ExecuteNonQuery("EEmployeeComp_WebSave", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(txtemployeeCompNo.Text, clsBase.clsBaseLibrary.enumObjectType.IntType), employeeno, tcompno, scaleno, anchorno, Me.txtRemark.Text.ToString, Me.txtAnchor.Text) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If
    End Function
   
End Class







