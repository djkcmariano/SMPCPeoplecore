Imports System.Data
Imports clsLib

Partial Class Secured_EmpExamType
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim dscount As Double = 0
    Dim _ds As New DataSet
    Dim _dt As New DataTable
    Dim xScript As String = ""
    Dim rowno As Integer = 0
    Dim showFrm As New clsFormControls
    Dim RefNo As Integer = 0
    Dim p As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Try
            _ds = sqlHelper.ExecuteDataset("EExamType_Web", Generic.CheckDBNull(Session("OnlineUserNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Viewstate(xScript & "filter"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType), Session("xPayLocNo"))
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
                ViewState(xScript & "PageNo") = 0
                ViewState(Left(xScript, Len(xScript) - 5)) = 0
                ViewState(Left(xScript, Len(xScript) - 5)) = 0
            End If

            Me.grdMain.SelectedIndex = Generic.CheckDBNull(ViewState(Left(xScript, Len(xScript) - 5)), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
            Me.grdMain.PageIndex = ViewState(xScript & "PageNo")
            Me.grdMain.DataSource = dv
            Me.grdMain.DataBind()


        Catch ex As Exception

        End Try
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("OnlineUserNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        xPublicVar.xTablename = Generic.CheckDBNull(Session("xTablename"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        xPublicVar.xMenuTitle = Generic.CheckDBNull(Session("xMenuTitle"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
        AccessRights.CheckUser(xPublicVar.xOnlineUseNo)
        AddHandler Filter1.lnkSearchClick, AddressOf lnkGo_Click
        xScript = Request.ServerVariables("SCRIPT_NAME")
        xScript = Generic.GetPath(xScript)

        If Not IsPostBack Then
            PopulateGrid()
        End If


        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ViewState(xScript & "filter") = Filter1.SearchText.ToString
            Viewstate(xScript & "PageNo") = 0
            ViewState(Left(xScript, Len(xScript) - 5)) = 0
            PopulateGrid()

        Catch ex As Exception
        End Try

    End Sub
    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMain.PageIndexChanging
        Viewstate(xScript & "No") = 0
        Viewstate(xScript & "PageNo") = e.NewPageIndex
        ViewState(Left(xScript, Len(xScript) - 5)) = 0
        PopulateGrid()

    End Sub

      Protected Sub grdMain_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdMain.Sorting
        Try

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

        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    e.Row.CssClass = "highlight"
        'End If
    End Sub



    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For tcount = 0 To Me.grdMain.Rows.Count - 1
                lbl = CType(grdMain.Rows(tcount).FindControl("lblId"), Label)
                tcheck = CType(grdMain.Rows(tcount).FindControl("txtIsSelect"), CheckBox)
                If tcheck.Checked = True Then
                    Generic.DeleteRecordAudit("EExamType", xPublicVar.xOnlineUseNo, CType(lbl.Text, Integer))
                    DeleteCount = DeleteCount + 1
                End If
            Next
            MessageBox.Success("There are " & DeleteCount.ToString & MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If
    End Sub
    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lnk As New ImageButton
            Dim i As String = "", ApprovalStatNo As Integer = 0

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)

            ViewState(Left(xScript, Len(xScript) - 5)) = gvrow.RowIndex
            i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
            Viewstate(xScript & "No") = i

            If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim _ds As New DataSet
                _ds = SQLHelper.ExecuteDataSet("EExamType_WebOne", xPublicVar.xOnlineUseNo, i)
                If _ds.Tables.Count > 0 Then
                    If _ds.Tables(0).Rows.Count > 0 Then
                        showFrm.clearFormControls_In_Popup(pnlPopup)
                        showFrm.showFormControls_In_Popup(pnlPopup, _ds)
                        showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopup)
                    End If
                End If
                mdlShow.Show()
            Else
                MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If


        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
         If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowAdd) Then
            showFrm.clearFormControls_In_Popup(pnlPopup)
            showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopup)
            Viewstate(xScript & "No") = 0
            mdlShow.Show()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If
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



    Private Function SaveRecord() As Boolean

        If sqlHelper.ExecuteNonQuery("EExamType_WebSave",
                                   xPublicVar.xOnlineUseNo, Generic.CheckDBNull(Viewstate(xScript & "No"), clsBase.clsBaseLibrary.enumObjectType.IntType),
                                   Me.txtExamTypeCode.Text.ToString, Me.txtExamTypeDesc.Text.ToString, chkIsWithRating.Checked, Session("xPayLocNo")) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If

    End Function
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
End Class

