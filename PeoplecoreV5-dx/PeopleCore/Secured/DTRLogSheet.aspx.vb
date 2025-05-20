Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_DTRLogSheetList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Private Sub PopulateGrid()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDTRLogSheet_WebOne", UserNo, Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0)), txtStartDate.Text, txtEndDate.Text)
        lbl.Text = txtFullName.Text
        hif.Value = hifEmployeeNo.Value
        grdMain.DataSource = dt
        grdMain.DataBind()
    End Sub

    Protected Sub lnkGenerate_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "Panel1")
        mdlDetl.Show()
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim count As Integer = 0
        Dim xcount As Integer = 0

        Dim txtIn1 As New TextBox
        Dim txtOut1 As New TextBox
        Dim txtIn2 As New TextBox
        Dim txtOut2 As New TextBox
        Dim hifID As New HiddenField
        Dim hifDate As New HiddenField

        Dim Min1 = "", Min2 = "", Min3 = "", Min4 As String = ""
        Dim Hr1 = "", Hr2 = "", Hr3 = "", Hr4 As String = ""
        Dim In1 = "", Out1 = "", In2 = "", Out2 As String = ""

        Dim xMsg As String = ""

        For i = 0 To grdMain.VisibleRowCount - 1

            hifID = grdMain.FindRowCellTemplateControl(i, grdMain.Columns(4), "hifID")
            hifDate = grdMain.FindRowCellTemplateControl(i, grdMain.Columns(4), "hifDate")
            txtIn1 = grdMain.FindRowCellTemplateControl(i, grdMain.Columns(4), "txtIn1")
            txtOut1 = grdMain.FindRowCellTemplateControl(i, grdMain.Columns(5), "txtOut1")
            txtIn2 = grdMain.FindRowCellTemplateControl(i, grdMain.Columns(6), "txtIn2")
            txtOut2 = grdMain.FindRowCellTemplateControl(i, grdMain.Columns(7), "txtOut2")

            In1 = Replace(txtIn1.Text, ":", "")
            Out1 = Replace(txtOut1.Text, ":", "")
            In2 = Replace(txtIn2.Text, ":", "")
            Out2 = Replace(txtOut2.Text, ":", "")

            Min1 = "" : Min2 = "" : Min3 = "" : Min4 = ""
            Hr1 = "" : Hr2 = "" : Hr3 = "" : Hr4 = ""
            txtIn1.ToolTip = "" : txtOut1.ToolTip = "" : txtIn2.ToolTip = "" : txtOut2.ToolTip = ""

            If txtIn1.Text > "" Then
                Hr1 = Left(txtIn1.Text, 2)
                Min1 = Right(txtIn1.Text, 2)
            End If

            If txtOut1.Text > "" Then
                Hr2 = Left(txtOut1.Text, 2)
                Min2 = Right(txtOut1.Text, 2)
            End If

            If txtIn2.Text > "" Then
                Hr3 = Left(txtIn2.Text, 2)
                Min3 = Right(txtIn2.Text, 2)
            End If

            If txtOut2.Text > "" Then
                Hr4 = Left(txtOut2.Text, 2)
                Min4 = Right(txtOut2.Text, 2)
            End If

            If Min1 > "59" Or Min2 > "59" Or Min3 > "59" Or Min4 > "59" Then
                If Min1 > "59" Then txtIn1.Focus() : txtIn1.ToolTip = "Invalid minute format for In1" : xMsg = txtIn1.ToolTip & "<br/>"
                If Min2 > "59" Then txtOut1.Focus() : txtOut1.ToolTip = "Invalid minute format for Out1" : xMsg = txtOut1.ToolTip & "<br/>"
                If Min3 > "59" Then txtIn2.Focus() : txtIn2.ToolTip = "Invalid minute format for In2" : xMsg = txtIn2.ToolTip & "<br/>"
                If Min4 > "59" Then txtOut2.Focus() : txtOut2.ToolTip = "Invalid minute format for Out2" : xMsg = txtOut2.ToolTip & "<br/>"
                xcount = xcount + 1
            End If

            If (Hr1 > "48" Or Hr1 + Min1 > "4800") Or (Hr2 > "48" Or Hr2 + Min2 > "4800") Or (Hr3 > "48" Or Hr3 + Min3 > "4800") Or (Hr4 > "48" Or Hr4 + Min4 > "4800") Then
                If (Hr1 > "48" Or Hr1 + Min1 > "4800") Then txtIn1.Focus() : txtIn1.ToolTip = "Maximum of 48:00 hour(s) allowed for In1" : xMsg = xMsg + txtIn1.ToolTip & "<br/>"
                If (Hr2 > "48" Or Hr2 + Min2 > "4800") Then txtOut1.Focus() : txtOut1.ToolTip = "Maximum of 48:00 hour(s) allowed for Out1" : xMsg = xMsg + txtOut1.ToolTip & "<br/>"
                If (Hr3 > "48" Or Hr3 + Min3 > "4800") Then txtIn2.Focus() : txtIn2.ToolTip = "Maximum of 48:00 hour(s) allowed for In2" : xMsg = xMsg + txtIn2.ToolTip & "<br/>"
                If (Hr4 > "48" Or Hr4 + Min4 > "4800") Then txtOut2.Focus() : txtOut2.ToolTip = "Maximum of 48:00 hour(s) allowed for Out2" : xMsg = xMsg + txtOut2.ToolTip & "<br/>"
                xcount = xcount + 1
            End If

            If In1 + Out1 + In2 + Out2 > "" Then
                'In1:
                If In1 > Out1 And Out1 > "" Then
                    txtIn1.Focus() : txtIn1.ToolTip = "Time In1 should not be greater than Time Out1" : xcount = xcount + 1 : xMsg = xMsg + txtIn1.ToolTip & "<br/>"
                ElseIf In1 > In2 And In2 > "" Then
                    txtIn1.Focus() : txtIn1.ToolTip = "Time In1 should not be greater than Time In2" : xcount = xcount + 1 : xMsg = xMsg + txtIn1.ToolTip & "<br/>"
                ElseIf In1 > Out2 And Out2 > "" Then
                    txtIn1.Focus() : txtIn1.ToolTip = "Time In1 should not be greater than Time Out2" : xcount = xcount + 1 : xMsg = xMsg + txtIn1.ToolTip & "<br/>"
                    'Out1:
                ElseIf Out1 > In2 And In2 > "" Then
                    txtOut1.Focus() : txtOut1.ToolTip = "Time Out1 should not be greater than Time In2" : xcount = xcount + 1 : xMsg = xMsg + txtOut1.ToolTip & "<br/>"
                ElseIf Out1 > Out2 And Out2 > "" Then
                    txtOut1.Focus() : txtOut1.ToolTip = "Time Out1 should not be greater than Time Out2" : xcount = xcount + 1 : xMsg = xMsg + txtOut1.ToolTip & "<br/>"
                    'In2:
                ElseIf In2 > Out2 And Out2 > "" Then
                    txtIn2.Focus() : txtIn2.ToolTip = "Time In2 should not be greater than Time Out2" : xcount = xcount + 1 : xMsg = xMsg + txtIn2.ToolTip & "<br/>"
                End If

            End If


            If xcount > 0 Then
                If xMsg > "" Then
                    MessageBox.Alert("<b>" + xMsg + "</b>", "warning", Me)
                End If
                Exit Sub
            End If

        Next


        For i = 0 To grdMain.VisibleRowCount - 1

            hifID = grdMain.FindRowCellTemplateControl(i, grdMain.Columns(4), "hifID")
            hifDate = grdMain.FindRowCellTemplateControl(i, grdMain.Columns(4), "hifDate")
            txtIn1 = grdMain.FindRowCellTemplateControl(i, grdMain.Columns(4), "txtIn1")
            txtOut1 = grdMain.FindRowCellTemplateControl(i, grdMain.Columns(5), "txtOut1")
            txtIn2 = grdMain.FindRowCellTemplateControl(i, grdMain.Columns(6), "txtIn2")
            txtOut2 = grdMain.FindRowCellTemplateControl(i, grdMain.Columns(7), "txtOut2")

            If txtIn1.Text = "" And txtOut1.Text = "" And txtIn2.Text = "" And txtOut2.Text = "" Then

            Else
                count = count + SQLHelper.ExecuteNonQuery("EDTRLogSheet_WebSave", UserNo, Generic.ToInt(hifID.Value), Generic.ToInt(Generic.Split(hif.Value, 0)), hifDate.Value, txtIn1.Text, txtOut1.Text, txtIn2.Text, txtOut2.Text, "", "", 2, lbl.Text, PayLocNo)
            End If

        Next

        If count > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRLogSheetNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTRLog", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsPosted"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub cbCheckAll_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim cb As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)
        cb.ClientSideEvents.CheckedChanged = String.Format("cbCheckAll_CheckedChanged")
        cb.Checked = False
        Dim count As Integer = 0
        Dim startIndex As Integer = grdMain.PageIndex * grdMain.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdMain.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)

        For i As Integer = startIndex To endIndex - 1
            If grdMain.Selection.IsRowSelected(i) Then
                count = count + 1
            End If
        Next i

        If count > 0 Then
            cb.Checked = True
        End If

    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    'Dim xPublicVar As New clsPublicVariable


    'Dim tstatus As Integer
    'Dim dscount As Double = 0

    'Dim _ds As New DataSet
    'Dim _dt As New DataTable
    'Dim xScript As String = ""
    'Dim rowno As Integer = 0
    'Dim employeeno As Integer = 0
    ''Dim lnkGo As New LinkButton
    'Dim IsClickMain As Integer = 0

    'Dim clsMessage As New clsMessage
    'Dim cbofilterby As New DropDownList
    'Dim cbofiltervalue As New DropDownList
    'Dim txtfilter As New TextBox
    'Dim dtrDetino As Integer
    'Dim showFrm As New clsFormControls
    'Dim clsGeneric As New clsGenericClass

    'Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")

    'End Sub

    'Private Sub PopulateGridDetail(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
    '    Dim _ds As New DataSet
    '    If IsMain Then
    '        ViewState(xScript & "Pageno") = 0
    '        ViewState(Left(xScript, Len(xScript) - 5)) = 0
    '    End If
    '    _ds = SQLHelper.ExecuteDataSet("EDTRLogSheet_WebOne", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(Session("EmployeeFNo"), clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ViewState(xScript & "startdate"), clsBase.clsBaseLibrary.enumObjectType.StrType), Generic.CheckDBNull(ViewState(xScript & "enddate"), clsBase.clsBaseLibrary.enumObjectType.StrType), dtrDetino, txtSearch.Text)
    '    Me.grdMain.PageIndex = ViewState(xScript & "PageNo")
    '    Me.grdMain.DataSource = _ds
    '    Me.grdMain.DataBind()

    'End Sub

    'Private Sub IsAllowtoview(userno As Integer, formName As String)
    '    Try


    '        Dim menutype As String = "", menustyle As String, tableName As String = "", menuTitle As String = ""
    '        menutype = Generic.CheckDBNull(Request.QueryString("menutype"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)

    '        menustyle = Right(menutype, 1)
    '        menutype = Mid(menutype, 1, menutype.Length - 1)
    '        If menustyle = "a" Then
    '            menustyle = "main"
    '        ElseIf menustyle = "b" Then
    '            menustyle = "reference"
    '        ElseIf menustyle = "c" Then
    '            menustyle = "report"
    '        Else
    '            menustyle = ""
    '        End If

    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try


    'End Sub

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    Try
    '        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("OnlineUserNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '        If xPublicVar.xOnlineUseNo = 0 Then
    '            Response.Redirect("../frmPageExpired.aspx")
    '        Else


    '            employeeno = Generic.CheckDBNull(Request.QueryString("employeeno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '            tstatus = Generic.CheckDBNull(Request.QueryString("eStatus"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '            IsClickMain = Generic.CheckDBNull(Request.QueryString("IsClickMain"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

    '            xScript = Request.ServerVariables("SCRIPT_NAME")
    '            xScript = Generic.GetPath(xScript)


    '            If Not IsPostBack Then
    '                If IsClickMain = 1 Then
    '                    PopulateGridDetail(True)
    '                Else
    '                    PopulateGridDetail(False)
    '                End If
    '                PopulateCombo()
    '            End If


    '        End If
    '        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    '    Catch ex As Exception
    '        Response.Redirect("../frmPageExpired.aspx")

    '    End Try
    'End Sub

    'Private Sub PopulateCombo()

    'End Sub

    'Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        ViewState(xScript & "PageNo") = 0
    '        ViewState(Left(xScript, Len(xScript) - 5)) = 0
    '        Session("EmployeeFNo") = Me.hifEmployeeNo.Value
    '        ViewState(xScript & "startdate") = fltxtStartDate.Text.ToString
    '        ViewState(xScript & "enddate") = fltxtEndDate.Text.ToString
    '        Me.lblName.Text = ""
    '        Session("dtrFullname") = ""
    '        PopulateGridDetail()
    '    Catch ex As Exception
    '    End Try

    'End Sub


    'Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim _ds As New DataSet
    '    Dim In1 = "", Out1 = "", In2 = "", Out2 = "", In3 = "", Out3 As String = ""
    '    Dim xDate As String = ""
    '    Dim EmployeeNo As Integer = 0
    '    Dim IsPosted As Boolean = False
    '    Dim xCnt = 0, xCnt2 As Short = 0
    '    Dim D1 As New Integer

    '    For Each rw As GridViewRow In Me.grdMain.Rows
    '        D1 = Generic.CheckDBNull(CType(rw.FindControl("lblId1"), Label).Text, clsBase.clsBaseLibrary.enumObjectType.IntType)
    '        xDate = Generic.CheckDBNull(CType(rw.FindControl("Label6"), Label).Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
    '        EmployeeNo = Generic.CheckDBNull(CType(rw.FindControl("Label5"), Label).Text, clsBase.clsBaseLibrary.enumObjectType.IntType)
    '        In1 = Generic.CheckDBNull(CType(rw.FindControl("txtIn1"), TextBox).Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
    '        Out1 = Generic.CheckDBNull(CType(rw.FindControl("txtOut1"), TextBox).Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
    '        In2 = Generic.CheckDBNull(CType(rw.FindControl("txtIn2"), TextBox).Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
    '        Out2 = Generic.CheckDBNull(CType(rw.FindControl("txtOut2"), TextBox).Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
    '        In3 = Generic.CheckDBNull(CType(rw.FindControl("txtIn3"), TextBox).Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
    '        Out3 = Generic.CheckDBNull(CType(rw.FindControl("txtOut3"), TextBox).Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
    '        xCnt = SQLHelper.ExecuteNonQuery("EDTRLogSheet_WebSave", xPublicVar.xOnlineUseNo, D1, EmployeeNo, xDate.ToString, In1.ToString, Out1.ToString, In2.ToString, Out2.ToString, In3.ToString, Out3.ToString, 2, txtSearch.Text)

    '        If xCnt > 0 Then xCnt2 += 1

    '    Next

    '    If xCnt2 > 0 Then
    '        MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '    Else
    '        MessageBox.Critical(MessageTemplate.ErrorSave, Me)
    '    End If

    '    PopulateGridDetail()

    'End Sub

    'Protected Sub lnkClearLogs_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim _ds As New DataSet
    '    Dim In1 = "", Out1 = "", In2 = "", Out2 = "", In3 = "", Out3 As String = ""
    '    Dim xDate As String = ""
    '    Dim EmployeeNo As Integer = 0
    '    Dim IsPosted As Boolean = False
    '    Dim xCnt = 0, xCnt2 As Short = 0
    '    Dim D1 As New Integer

    '    For Each rw As GridViewRow In Me.grdMain.Rows
    '        D1 = Generic.CheckDBNull(CType(rw.FindControl("lblId1"), Label).Text, clsBase.clsBaseLibrary.enumObjectType.IntType)
    '        xDate = Generic.CheckDBNull(CType(rw.FindControl("Label6"), Label).Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
    '        EmployeeNo = Generic.CheckDBNull(CType(rw.FindControl("Label5"), Label).Text, clsBase.clsBaseLibrary.enumObjectType.IntType)
    '        Dim txtIn1 = CType(rw.FindControl("txtIn1"), TextBox)
    '        Dim txtIn2 = CType(rw.FindControl("txtIn2"), TextBox)
    '        Dim txtIn3 = CType(rw.FindControl("txtIn3"), TextBox)
    '        Dim txtOut1 = CType(rw.FindControl("txtOut1"), TextBox)
    '        Dim txtOut2 = CType(rw.FindControl("txtOut2"), TextBox)
    '        Dim txtOut3 = CType(rw.FindControl("txtOut3"), TextBox)

    '        If Not txtIn1 Is Nothing Then txtIn1.Text = ""
    '        If Not txtIn2 Is Nothing Then txtIn2.Text = ""
    '        If Not txtIn3 Is Nothing Then txtIn3.Text = ""
    '        If Not txtOut1 Is Nothing Then txtOut1.Text = ""
    '        If Not txtOut2 Is Nothing Then txtOut2.Text = ""
    '        If Not txtOut3 Is Nothing Then txtOut3.Text = ""

    '        If xCnt > 0 Then xCnt2 += 1

    '        If xCnt2 > 0 Then
    '            fRegisterStartupScript("JSDialogMessage", "dialogMessage('Transaction Save','Successful in clearing record(s).');")
    '        End If


    '    Next
    'End Sub

    'Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim lbl As New Label, tcheck As New CheckBox
    '    Dim tcount As Integer, DeleteCount As Integer = 0


    '    If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowDelete) Then
    '        For tcount = 0 To Me.grdMain.Rows.Count - 1
    '            lbl = CType(grdMain.Rows(tcount).FindControl("lblId2"), Label)
    '            tcheck = CType(grdMain.Rows(tcount).FindControl("txtIsSelect"), CheckBox)
    '            If tcheck.Checked = True Then
    '                clsGeneric.DeleteRecordAuditCol("EDTRLog", xPublicVar.xOnlineUseNo, "DTRLogNo", Generic.CheckDBNull(lbl.Text, clsBase.clsBaseLibrary.enumObjectType.IntType))
    '                DeleteCount = DeleteCount + 1
    '            End If
    '        Next
    '        MessageBox.Success("There are (" + DeleteCount.ToString + ") " + clsMessage.GetMessageType(Global.clsMessage.EnumMessageType.SuccessDelete), Me)
    '        PopulateGrid()
    '    Else
    '        MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
    '    End If

    '    PopulateGridDetail()

    'End Sub

    'Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMain.PageIndexChanging
    '    ViewState(xScript & "No") = 0
    '    ViewState(xScript & "PageNo") = e.NewPageIndex
    '    ViewState(Left(xScript, Len(xScript) - 5)) = 0
    '    PopulateGridDetail()
    'End Sub


    'Protected Sub grdMain_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdMain.Sorting
    '    Try

    '        Dim sortExpression = TryCast(ViewState("SortExpression"), String)
    '        Dim lastDirection = TryCast(ViewState("SortDirection"), String)
    '        Dim sortDirection As String = grdSort.GetSortDirection(e.SortExpression, sortExpression, lastDirection)

    '        ViewState("SortExpression") = sortExpression
    '        ViewState("SortDirection") = lastDirection

    '        PopulateGrid(False, e.SortExpression, sortDirection)

    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Protected Sub grdMain_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMain.RowCreated
    '    ' Use the RowType property to determine whether the 
    '    ' row being created is the header row. 
    '    ' If e.Row.RowType = DataControlRowType.Header Then
    '    ' Call the GetSortColumnIndex helper method to determine
    '    ' the index of the column being sorted.
    '    Dim sortColumnIndex As Integer = grdSort.GetSortColumnIndex(Me.grdMain, ViewState("SortExpression"))
    '    If sortColumnIndex > 0 Then
    '        ' Call the AddSortImage helper method to add
    '        ' a sort direction image to the appropriate
    '        ' column header. 
    '        grdSort.AddSortImage(sortColumnIndex, e.Row, ViewState("SortDirection"))
    '    End If
    '    'e.Row.CssClass = "highlight"
    '    ' End If

    '    'If e.Row.RowType = DataControlRowType.DataRow Then
    '    '    e.Row.CssClass = "highlight"
    '    'End If
    'End Sub
    'Protected Sub lnkAdd_Click(sender As Object, e As System.EventArgs)


    '    If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '        Response.Redirect("DTRLogsheetEdit.aspx?transNo=0&tModify=true&rowno=" & rowno.ToString)
    '    Else
    '        MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
    '    End If
    'End Sub


    'Protected Sub grdMain_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMain.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim lblCode As New Label, lblName As New Label
    '        lblName = CType(e.Row.FindControl("Label2"), Label)
    '        Session("dtrFullname") = lblName.Text.ToString
    '        Me.lblName.Text = Session("dtrFullname").ToString
    '    End If
    'End Sub

    'Private Sub fRegisterStartupScript(key As String, script As String)
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    'End Sub


End Class


