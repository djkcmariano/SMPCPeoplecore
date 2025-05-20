Imports System.Data
Imports clsLib


Partial Class Secured_frmContractTemplateList
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim tstatus As Integer
    Dim dscount As Double = 0
    Dim _ds As New DataSet
    Dim _dt As New DataTable
    Dim xScript As String = ""
    Dim rowno As Integer = 0
    Dim transNo As Integer = 0
    Dim showFrm As New clsFormControls

    Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")


        _ds = sqlHelper.ExecuteDataset("EContractTemp_Web", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(Viewstate(xScript & "filter"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType), Session("xMenuType"), Session("xPayLocNo"))
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
            End If
        End If

        Me.grdMain.SelectedIndex = Generic.CheckDBNull(ViewState(Left(xScript, Len(xScript) - 5)), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Me.grdMain.PageIndex = ViewState(xScript & "PageNo")
        Me.grdMain.DataSource = dv
        Me.grdMain.DataBind()

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("OnlineUserNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        AccessRights.CheckUser(xPublicVar.xOnlineUseNo)

        transNo = Generic.CheckDBNull(Request.QueryString("transNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        xScript = Request.ServerVariables("SCRIPT_NAME")
        xScript = Generic.GetPath(xScript)

        If Not IsPostBack Then
            PopulateGrid()
            PopulateCombo()
        End If
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            ViewState(xScript & "PageNo") = 0
            ViewState(xScript & "filter") = Filter1.SearchText.ToString
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

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lnk As New ImageButton
            Dim i As String = "", Cont1 As String = "", Cont2 As String = "", Cont3 As String = "", Cont4 As String = "", Cont5 As String = ""

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            rowno = gvrow.RowIndex
            ViewState(Left(xScript, Len(xScript) - 5)) = rowno
            i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
            Viewstate(xScript & "No") = i
            If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit) Then
                _ds = sqlHelper.ExecuteDataset("EContractTemp_WebOne", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(Viewstate(xScript & "No"), clsBase.clsBaseLibrary.enumObjectType.IntType))
                If _ds.Tables.Count > 0 Then
                    If _ds.Tables(0).Rows.Count > 0 Then
                        showFrm.clearFormControls_In_Popup(pnlPopupDetl)
                        showFrm.showFormControls_In_Popup(pnlPopupDetl, _ds)
                        Cont1 = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("ContractTempCont1"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                        Cont2 = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("ContractTempCont2"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                        Cont3 = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("ContractTempCont3"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                        Cont4 = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("ContractTempCont4"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                        Cont5 = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("ContractTempCont5"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                        txtContractTempContent.Html = Cont1 & Cont2 & Cont3 & Cont4 & Cont5
                        showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupDetl)
                        showFrm.EnableControls_in_Popup(pnlPopupDetl, True)
                    End If
                End If
                mdlDetl.Show()
            Else
                MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If

        Catch ex As Exception
        End Try
    End Sub


    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Viewstate(xScript & "No") = 0
            showFrm.clearFormControls_In_Popup(pnlPopupDetl)
            showFrm.populateCombo_In_form_Popup(xPublicVar.xOnlineUseNo, pnlPopupDetl)
            showFrm.EnableControls_in_Popup(pnlPopupDetl, True)
            btnSave.Enabled = True
            mdlDetl.Show()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If

    End Sub


    'Display record
    Private Sub PopulateCombo()
        showFrm.populateCombo(xPublicVar.xOnlineUseNo, Me)
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0
        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For tcount = 0 To Me.grdMain.Rows.Count - 1
                lbl = CType(grdMain.Rows(tcount).FindControl("lblId"), Label)
                tcheck = CType(grdMain.Rows(tcount).FindControl("txtIsSelect"), CheckBox)
                If tcheck.Checked = True Then
                    Generic.DeleteRecordAudit("EContractTemp", xPublicVar.xOnlineUseNo, CType(lbl.Text, Integer))
                    DeleteCount = DeleteCount + 1
                End If
            Next
            MessageBox.Success("There are " & DeleteCount.ToString & MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        End If

    End Sub

    'Submit record
    'Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If SaveRecord() Then
    '        clsModalControls.SetModalPopupControls(CType(Master.FindControl("cphBody"), ContentPlaceHolder), clsMessage.GetMessageType(Global.clsMessage.EnumMessageType.SuccessSave), "~/secured/" & clsArray.myFormname(1).xFormname & "?transNo=" & transNo & "&tModify=False&tabOrder=" & tabOrder)
    '    Else
    '        clsModalControls.SetModalPopupControls(CType(Master.FindControl("cphBody"), ContentPlaceHolder), clsMessage.GetMessageType(Global.clsMessage.EnumMessageType.ErrorSave), "")
    '    End If
    'End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If SaveRecord() Then

            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub





    Private Function SaveRecord() As Integer
        Dim tcount As Integer
        Dim Cont1 As String = ""
        Dim Cont2 As String = ""
        Dim Cont3 As String = ""
        Dim Cont4 As String = ""
        Dim Cont5 As String = ""

        tcount = Len(Me.txtContractTempContent.Html)

        If tcount >= 8000 Then
            Cont1 = Me.txtContractTempContent.Html.Substring(0, 8000)
            If tcount >= 16000 Then
                Cont2 = Me.txtContractTempContent.Html.Substring(8001, 8000)
                If tcount >= 24000 Then
                    Cont3 = Me.txtContractTempContent.Html.Substring(16001, 8000)
                    If tcount >= 32000 Then
                        Cont4 = Me.txtContractTempContent.Html.Substring(24001, 8000)
                        If tcount >= 40000 Then
                            Cont5 = Me.txtContractTempContent.Html.Substring(32001, 8000)
                        Else
                            Cont5 = Me.txtContractTempContent.Html.Substring(32001)
                        End If
                    Else
                        Cont4 = Me.txtContractTempContent.Html.Substring(24001)
                    End If
                Else
                    Cont3 = Me.txtContractTempContent.Html.Substring(16001)
                End If
            Else
                Cont2 = Me.txtContractTempContent.Html.Substring(8001)
            End If
        Else
            Cont1 = Me.txtContractTempContent.Html.ToString
        End If


        If sqlHelper.ExecuteNonQuery("EContractTemp_WebSave", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(Viewstate(xScript & "No"), clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Me.txtContractTempCode.Text, clsBase.clsBaseLibrary.enumObjectType.StrType), Generic.CheckDBNull(Me.txtContractTempDesc.Text, clsBase.clsBaseLibrary.enumObjectType.StrType), Cont1.ToString, Cont2.ToString, Cont3.ToString, Cont4.ToString, Cont5.ToString, Generic.CheckDBNull(Session("xMenuType"), clsBase.clsBaseLibrary.enumObjectType.StrType), Session("xPayLocNo")) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If


    End Function
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
End Class

