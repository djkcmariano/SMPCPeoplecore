Imports System.Data
Imports clsLib

Partial Class Secured_EmpHRANListTransfer
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim tstatus As Integer
    Dim dscount As Double = 0
    Dim _ds As New DataSet
    Dim _dt As New DataTable
    Dim xScript As String = ""
    Dim rowno As Integer = 0
    Dim hranno As Integer = 0
    Dim lnkGo As New LinkButton

    Dim clsGeneric As New clsGenericClass
    Dim IsClickMain As Integer = 0
    Dim showFrm As New clsFormControls

    Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")

         tstatus = Generic.ToInt(cboTabNo.SelectedValue)
        If tstatus = 0 Then
            tstatus = 1
        End If
        If tstatus = 1 Then
            lnkDelete.Visible = True
        End If

        _ds = sqlHelper.ExecuteDataset("EHRAN_WebTransfer", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(Viewstate(xScript & "filter"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType), tstatus, Generic.CheckDBNull(Viewstate(xScript & "filterby"), clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Viewstate(xScript & "filtervalue"), clsBase.clsBaseLibrary.enumObjectType.IntType), Session("xPayLocNo"))
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
                If ViewState(xScript & "SortExp") <> "" Then
                    dv.Sort = ViewState(xScript & "SortExp") + ViewState(xScript & "sortdir")
                End If
            Else
                Viewstate(xScript & "No") = 0
                ViewState(xScript & "PageNo") = 0
            End If
        Else
            Viewstate(xScript & "No") = 0
            ViewState(xScript & "PageNo") = 0
        End If

        If IsMain Then
            Viewstate(xScript & "Pageno") = 0
            Session(Left(xScript, Len(xScript) - 5)) = 0
        End If
        Me.grdMain.SelectedIndex = Generic.CheckDBNull(ViewState(Left(xScript, Len(xScript) - 5)), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Me.grdMain.PageIndex = ViewState(xScript & "PageNo")
        Me.grdMain.DataSource = dv
        Me.grdMain.DataBind()




        'Approval Routing 
        PopulateAppr()

        cbofilterby.Text = Generic.CheckDBNull(ViewState(xScript & "filterby"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
        cbofiltervalue.Text = Generic.CheckDBNull(Viewstate(xScript & "filtervalue"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
    End Sub



    Private Sub PopulateAppr(Optional pageNo As Integer = 0)

        Try
            If Generic.CheckDBNull(Viewstate(xScript & "No"), clsBase.clsBaseLibrary.enumObjectType.IntType) = 0 And grdMain.Rows.Count > 0 Then
                Viewstate(xScript & "No") = grdMain.DataKeys(0).Values(0).ToString()
                Viewstate(xScript & "Code") = grdMain.DataKeys(0).Values(1).ToString()
            End If
            Dim _ds As New DataSet, hrancode As String = "", apprItemCount As Double = 0
            _ds = sqlHelper.ExecuteDataset("EHRANApprovalRouting_Web", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(Viewstate(xScript & "No"), clsBase.clsBaseLibrary.enumObjectType.IntType))
            grdAppr.PageIndex = pageNo
            Me.grdAppr.DataSource = _ds
            Me.grdAppr.DataBind()

            lblDetl.Text = "List of Approval Routing from HRAN No.: <b><u>" & Generic.CheckDBNull(Viewstate(xScript & "Code"), clsBase.clsBaseLibrary.enumObjectType.StrType) & "</u></b>"
        Catch ex As Exception

        End Try

    End Sub


    Private Sub PopulateCombo()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", xPublicVar.xOnlineUseNo, 11)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("OnlineUserNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        AccessRights.CheckUser(xPublicVar.xOnlineUseNo)
        AddHandler Filter1.lnkSearchClick, AddressOf lnkGo_Click
        hranno = Generic.CheckDBNull(Request.QueryString("transNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    
        xScript = Request.ServerVariables("SCRIPT_NAME")
        xScript = Generic.GetPath(xScript)

        If Not IsPostBack Then
            PopulateGrid()
            populateFilterBy()
            PopulateCombo()
        End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            ViewState(xScript & "filter") = Generic.CheckDBNull(Filter1.SearchText.ToString, clsBase.clsBaseLibrary.enumObjectType.StrType)
            ViewState(xScript & "filterby") = Generic.CheckDBNull(cbofilterby.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
            ViewState(xScript & "filtervalue") = Generic.CheckDBNull(cbofiltervalue.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
            ViewState(xScript & "PageNo") = 0
            ViewState(Left(xScript, Len(xScript) - 5)) = 0
            PopulateGrid()
            ViewState(xScript & "No") = grdMain.DataKeys(0).Values(0).ToString()
            PopulateAppr()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMain.PageIndexChanging
        Viewstate(xScript & "PageNo") = e.NewPageIndex
        ViewState(Left(xScript, Len(xScript) - 5)) = 0
        PopulateGrid()
        Viewstate(xScript & "No") = grdMain.DataKeys(0).Values(0).ToString()
        PopulateAppr()

    End Sub
    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)


        Dim lnk As New ImageButton
        Dim i As String = "", fdtrNo As Integer = 0

        lnk = sender
        Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
        rowno = gvrow.RowIndex
        ViewState(Left(xScript, Len(xScript) - 5)) = rowno

        i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
        Viewstate(xScript & "No") = i
        Response.Redirect("~/secured/EmphranListtransfer_edit.aspx?transNo=" & i & "&tModify=false")

    End Sub
    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lnk As New ImageButton
        Dim i As String = "", fdtrNo As Integer = 0

        lnk = sender
        Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
        rowno = gvrow.RowIndex
        ViewState(Left(xScript, Len(xScript) - 5)) = rowno

        i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
        Viewstate(xScript & "No") = i
        'Response.Redirect("~/secured/" & Session("xFormName") & "?transNo=" & Generic.CheckDBNull(i, Global.clsBase.clsBaseLibrary.enumObjectType.IntType) & "&tModify=false&tabOrder=" & tabOrder)
        PopulateGrid()
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

    Protected Sub btnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As LinkButton = sender

            Dim fno As Integer = Generic.CheckDBNull(lnk.ToolTip, clsBase.clsBaseLibrary.enumObjectType.IntType)
            Dim sb As New StringBuilder
            Dim rpt As String = "AdminHRANMovementRpt01"

            sb.Append("<script>")
            sb.Append("window.open('rptTemplateViewerTransNF.aspx?hranno=" & fno & "&reportname=" & rpt & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes,with=800,height=550');")
            sb.Append("</scri")
            sb.Append("pt>")

            ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/EmphranListtransfer_edit.aspx?transNo=" & 0 & "&tModify=True&rowno=" & rowno.ToString)
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
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
                    Generic.DeleteRecordAuditCol("EHRANApprovalRouting", xPublicVar.xOnlineUseNo, "HRANNo", CType(lbl.Text, Integer))
                    Generic.DeleteRecordAudit("EHRAN", xPublicVar.xOnlineUseNo, CType(lbl.Text, Integer))
                    DeleteCount = DeleteCount + 1
                End If
            Next
            MessageBox.Success("There are " & DeleteCount.ToString & MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If
    End Sub
    Protected Sub lnkPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label
        Dim tCheck As New CheckBox
        Dim tcount As Integer = 0
        Dim _ds As New DataSet
        Dim ts As Integer = 0
        Dim ffullname As String = "", tfullname As String = "", lblEmpCode As New Label
        Dim nfullName As String = ""

        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowPost) Then

            For tcount = 0 To Me.grdMain.Rows.Count - 1
                lbl = CType(grdMain.Rows(tcount).FindControl("lblId"), Label)
                tCheck = CType(grdMain.Rows(tcount).FindControl("txtIsSelect"), CheckBox)
                If tCheck.Checked = True Then
                    sqlHelper.ExecuteNonQuery("EHRAN_WebPost_Transfer", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(lbl.Text, clsBase.clsBaseLibrary.enumObjectType.IntType))
                    ts = ts + 1
                End If
            Next

            MessageBox.Success("There are " + ts.ToString + " transaction(s) posted to 201 file!'", Me)
            PopulateGrid()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowPost), Me)
        End If

    End Sub

    Private Sub populateFilterBy()
        Try
            cbofilterby.DataSource = SQLHelper.ExecuteDataSet("xTable_Lookup", xPublicVar.xOnlineUseNo, "EFilteredBy", Session("xPayLocNo"), "", "")
            cbofilterby.DataTextField = "tDesc"
            cbofilterby.DataValueField = "tno"
            cbofilterby.DataBind()

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub cbofilterby_SelectedIndexChanged(sender As Object, e As System.EventArgs) 'Handles cbofilterby.SelectedIndexChanged
        Try

            Dim clsGen As New clsGenericClass
            Dim ds As DataSet
            ds = clsGen.populateDropdownFilterByAll(Generic.CheckDBNull(Me.cbofilterby.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType), xPublicVar.xOnlineUseNo, Session("xPayLocNo"))
            cbofiltervalue.DataSource = ds
            cbofiltervalue.DataTextField = "tDesc"
            cbofiltervalue.DataValueField = "tNo"
            cbofiltervalue.DataBind()
        Catch ex As Exception

        End Try
    End Sub

End Class




