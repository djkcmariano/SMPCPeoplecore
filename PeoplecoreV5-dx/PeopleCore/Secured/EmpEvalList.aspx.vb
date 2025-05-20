Imports System.Data
Imports clsLib


Partial Class Secured_EmpEvalList
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim tstatus As Integer
    Dim dscount As Double = 0
    Dim _ds As New DataSet
    Dim _dt As New DataTable
    Dim xScript As String = ""
    Dim rowno As Integer = 0
    Dim transNo As Integer = 0
    Dim EmployeeNo As Integer = 0
    Dim showFrm As New clsFormControls
    Dim clsgeneric As New clsGenericClass


    Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")


        Try
            _ds = SQLHelper.ExecuteDataSet("EEmployeeEval_Web", xPublicVar.xOnlineUseNo, EmployeeNo)
            _dt = _ds.Tables(0)

            Dim dv As New Data.DataView(_dt)
            If SortExp <> "" Then
                Viewstate(xScript & "SortExp") = SortExp
            End If
            If sordir <> "" Then

                Viewstate(xScript & "sortdir") = sordir
            End If
            If _ds.Tables.Count > 0 Then
                If _ds.Tables(0).Rows.Count > 0 Then
                    dscount = _ds.Tables(0).Rows.Count
                    If Viewstate(xScript & "SortExp") <> "" Then
                        dv.Sort = Viewstate(xScript & "SortExp") + Viewstate(xScript & "sortdir")
                    End If
                Else
                    Viewstate(xScript & "no") = 0
                End If
            Else
                Viewstate(xScript & "no") = 0
            End If
            If IsMain Then
                Viewstate(xScript & "Pageno") = 0
                ViewState(Left(xScript, Len(xScript) - 5)) = 0
            End If

            Me.grdMain.SelectedIndex = Generic.CheckDBNull(ViewState(Left(xScript, Len(xScript) - 5)), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
            Me.grdMain.PageIndex = Viewstate(xScript & "Pageno")
            Me.grdMain.DataSource = dv
            Me.grdMain.DataBind()

            PopulateDetl()

        Catch ex As Exception

        End Try

    End Sub
    Private Sub PopulateDetl(Optional pageno As Integer = 0)
        Try
            Dim _dsDetl As DataSet
            If Generic.CheckDBNull(Viewstate(xScript & "No"), clsBase.clsBaseLibrary.enumObjectType.IntType) = 0 And grdMain.Rows.Count > 0 Then
                Viewstate(xScript & "No") = grdMain.DataKeys(0).Values(0).ToString()
            End If
            _dsDetl = SQLHelper.ExecuteDataset("EEmployeeEvalDeti_Web", xPublicVar.xOnlineUseNo, Viewstate(xScript & "No"))
            grdDetl.PageIndex = pageno
            Me.grdDetl.DataSource = _dsDetl
            Me.grdDetl.DataBind()
            Me.lblDetl.Text = "Detail List - Transaction No. : <u>" & Pad.PadZero(8, Generic.CheckDBNull(Viewstate(xScript & "No"), clsBase.clsBaseLibrary.enumObjectType.IntType)) & " </u>"

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub grdDetl_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdDetl.PageIndexChanging
        Dim pageno As Integer = e.NewPageIndex
        PopulateDetl(pageno)
    End Sub
    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Viewstate(xScript & "PageNo") = 0
            ViewState(Left(xScript, Len(xScript) - 5)) = 0
            PopulateGrid()
            Viewstate(xScript & "No") = grdMain.DataKeys(0).Values(0).ToString()
            PopulateDetl()
        Catch ex As Exception
        End Try

    End Sub
    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMain.PageIndexChanging
        Viewstate(xScript & "No") = 0
        Viewstate(xScript & "PageNo") = e.NewPageIndex
        ViewState(Left(xScript, Len(xScript) - 5)) = 0
        PopulateGrid()
        Viewstate(xScript & "No") = grdMain.DataKeys(0).Values(0).ToString()
        PopulateDetl()
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
        xScript = Request.ServerVariables("SCRIPT_NAME")
        xScript = Generic.GetPath(xScript)


        If Not IsPostBack Then
            PopulateGrid()
            PopulateName()
        End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lnk As New ImageButton
            Dim i As String = "", ApprovalStatNo As Integer = 0

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            rowno = gvrow.RowIndex
            ViewState(Left(xScript, Len(xScript) - 5)) = rowno

            i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()

            Viewstate(xScript & "No") = i 'HRANNO
            If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim _ds As New DataSet
                _ds = SQLHelper.ExecuteDataSet("EEmployeeEval_WebOne", xPublicVar.xOnlineUseNo, i)
                If _ds.Tables.Count > 0 Then
                    If _ds.Tables(0).Rows.Count > 0 Then
                        showFrm.clearFormControls_In_Popup(pnlPopupMain)
                        showFrm.showFormControls_In_Popup(pnlPopupMain, _ds)
                        showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupMain)

                    End If
                End If
                mdlDetlMain.Show()
            Else
                MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If


        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lnk As New ImageButton
            Dim i As String = "", ii As Integer = 0, approvalStatno As Integer = 0, dayDesc As String = ""

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            rowno = gvrow.RowIndex

            i = grdDetl.DataKeys(gvrow.RowIndex).Values(0).ToString()
            ii = grdDetl.DataKeys(gvrow.RowIndex).Values(1)

            Viewstate(xScript & "No") = ii 'HRANNO
            If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim _ds As New DataSet
                _ds = SQLHelper.ExecuteDataSet("eEmployeeEvalDeti_WebOne", xPublicVar.xOnlineUseNo, i)
                If _ds.Tables.Count > 0 Then
                    If _ds.Tables(0).Rows.Count > 0 Then
                        showFrm.clearFormControls_In_Popup(pnlPopupDetl)
                        showFrm.showFormControls_In_Popup(pnlPopupDetl, _ds)
                        showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupDetl)
                    End If
                End If
                mdlShowDetl.Show()
            Else
                MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As New ImageButton
            Dim i As String = "", leavetypedesc As String = ""

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            rowno = gvrow.RowIndex
            ViewState(Left(xScript, Len(xScript) - 5)) = rowno
            i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()

            Viewstate(xScript & "No") = i
            Me.grdMain.SelectedIndex = Generic.CheckDBNull(Session(Left(Session("xFormname"), Len(Session("xformname")) - 5)), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
            PopulateDetl()
        Catch ex As Exception
        End Try
    End Sub




    Protected Sub grdMain_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdMain.Sorting
        Try
            'PopulateGrid(e.SortExpression, GetSortDirection(e.SortExpression))


            Dim sortExpression = TryCast(ViewState("SortExpression"), String)
            Dim lastDirection = TryCast(ViewState("SortDirection"), String)
            Dim sortDirection As String = grdSort.GetSortDirection(e.SortExpression, sortExpression, lastDirection)

            ViewState("SortExpression") = sortExpression
            ViewState("SortDirection") = lastDirection

            PopulateGrid(False, e.SortExpression, sortDirection)

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub grdMain_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMain.RowCreated
        ' Use the RowType property to determine whether the 
        ' row being created is the header row. 
        ' If e.Row.RowType = DataControlRowType.Header Then
        ' Call the GetSortColumnIndex helper method to determine
        ' the index of the column being sorted.
        Dim sortColumnIndex As Integer = grdSort.GetSortColumnIndex(Me.grdMain, ViewState("SortExpression"))
        If sortColumnIndex > 0 Then
            ' Call the AddSortImage helper method to add
            ' a sort direction image to the appropriate
            ' column header. 
            grdSort.AddSortImage(sortColumnIndex, e.Row, ViewState("SortDirection"))
        End If
        'e.Row.CssClass = "highlight"
        ' End If
    End Sub


    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)


        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowAdd) Then
            showFrm.clearFormControls_In_Popup(pnlPopupMain)
            showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupMain)
            Me.txtEmployeeEvalCode.Text = "Autonumber"

            mdlDetlMain.Show()
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If

    End Sub
    Protected Sub lnkAddD_Click(ByVal sender As Object, ByVal e As System.EventArgs)


        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowAdd) Then
            showFrm.clearFormControls_In_Popup(pnlPopupDetl)
            showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupDetl)
            Me.txtEmployeeEvalDetiCode.Text = "Autonumber"
            mdlShowDetl.Show()
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
                    Generic.DeleteRecordAuditCol("EEmployeeEvalDeti", xPublicVar.xOnlineUseNo, "EEmployeeEvalNo", CType(lbl.Text, Integer))
                    Generic.DeleteRecordAudit("EEmployeeEval", xPublicVar.xOnlineUseNo, CType(lbl.Text, Integer))
                    DeleteCount = DeleteCount + 1
                End If
            Next
            MessageBox.Success("There are (" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If
    End Sub
    Protected Sub lnkDeleteD_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0

         If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For tcount = 0 To Me.grdDetl.Rows.Count - 1
                lbl = CType(grdDetl.Rows(tcount).FindControl("lblIdd"), Label)
                tcheck = CType(grdDetl.Rows(tcount).FindControl("txtIsSelectd"), CheckBox)
                If tcheck.Checked = True Then
                    Generic.DeleteRecordAudit("EEmployeeEvalDeti", xPublicVar.xOnlineUseNo, CType(lbl.Text, Integer))
                    DeleteCount = DeleteCount + 1
                End If
            Next
            MessageBox.Success("There are (" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateDetl()
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If
    End Sub

    'Submit record
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If saverecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub
    Private Function saverecord() As Boolean
        Dim evaluationreasonno As Integer = Generic.CheckDBNull(Me.cboEvaluationReasonNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim evaluationreviewno As Integer = Generic.CheckDBNull(Me.cboEvaluationReviewNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        If SQLHelper.ExecuteNonQuery("EEmployeeEval_WebSave", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(txtEmployeeEvalNo.Text, clsBase.clsBaseLibrary.enumObjectType.IntType), EmployeeNo, Me.txtevaluationdate.Text.ToString, Me.txtEvaluatorName.Text.ToString, evaluationreasonno, evaluationreviewno, Me.txtRemark.Text.ToString, Me.txtIsInhouse.Checked) > 0 Then
            saverecord = True
        Else
            saverecord = False
        End If
    End Function
    'Submit record
    Protected Sub btnSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If saverecordDetl() = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateDetl()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub
    Private Function saverecordDetl() As Boolean
        Dim evaluationfactorno As Integer = Generic.CheckDBNull(Me.cboEvaluationFactorNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim evaluationscaleno As Integer = Generic.CheckDBNull(Me.cboEvaluationScaleNo.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim evaluationscore As Double = Generic.CheckDBNull(Me.txtevaluationscore.Text, Global.clsBase.clsBaseLibrary.enumObjectType.DblType)

        If SQLHelper.ExecuteNonQuery("EEmployeeEvalDeti_WebSave", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(txtEmployeeEvalDetiNo.Text, clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Viewstate(xScript & "No"), clsBase.clsBaseLibrary.enumObjectType.DblType), evaluationfactorno, evaluationscaleno, evaluationscore) > 0 Then
            saverecordDetl = True
        Else
            saverecordDetl = False

        End If

    End Function
  

    
End Class






