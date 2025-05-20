Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PEReviewMainList
    Inherits System.Web.UI.Page

    Dim xBase As New clsBase.clsBaseLibrary
    Dim IsCompleted As Integer = 0

    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim clsGen As New clsGenericClass
    Dim process_status As String = ""
    Dim err_num As Integer = 0

    '    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    '        UserNo = Generic.ToInt(Session("OnlineUserNo"))
    '        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
    '        AccessRights.CheckUser(UserNo)

    '        If Not IsPostBack Then
    '            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
    '            PopulateDropDown()
    '            PopulateGrid()
    '        End If

    '        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

    '    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("EPEReviewMain_WebTab", UserNo, PayLocNo)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception
        End Try
    End Sub

    '        End Try

    '        Try
    '            cboFilterPeriodType.DataSource = clsGen.xLookup_Table(UserNo, "EPEPeriod", PayLocNo)
    '            cboFilterPeriodType.DataValueField = "tNo"
    '            cboFilterPeriodType.DataTextField = "tDesc"
    '            cboFilterPeriodType.DataBind()
    '        Catch ex As Exception

    '        End Try

    '        Try
    '            cboFilterPosition.DataSource = clsGen.xLookup_Table(UserNo, "EPosition", PayLocNo)
    '            cboFilterPosition.DataValueField = "tNo"
    '            cboFilterPosition.DataTextField = "tDesc"
    '            cboFilterPosition.DataBind()
    '        Catch ex As Exception

    '        End Try

    '    End Sub
    '    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        PopulateGrid()
    '    End Sub

    '#Region "Main"
    '    Protected Sub PopulateGrid()
    '        Try
    '            Dim dt As DataTable
    '            Dim sortDirection As String = "", sortExpression As String = ""
    '            Dim TabOrder As Integer = Generic.ToInt(cboTabNo.SelectedValue)
    '            Dim FilterPeriodType As Integer = Generic.ToInt(cboFilterPeriodType.SelectedValue)
    '            Dim FilterPosition As Integer = Generic.ToInt(cboFilterPosition.SelectedValue)

    '            dt = SQLHelper.ExecuteDataTable("EPEReviewMain_Web", UserNo, Filter1.SearchText, TabOrder, Generic.ToInt(txtFilterApplicableYear.Text), FilterPeriodType, FilterPosition, PayLocNo)
    '            Dim dv As DataView = dt.DefaultView
    '            If ViewState("SortDirection") IsNot Nothing Then
    '                sortDirection = ViewState("SortDirection").ToString()
    '            End If
    '            If ViewState("SortExpression") IsNot Nothing Then
    '                sortExpression = ViewState("SortExpression").ToString()
    '                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
    '            End If
    '            grdMain.DataSource = dv
    '            grdMain.DataBind()

    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Protected Sub grdMain_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
    '        Try
    '            If ViewState("SortDirection") Is Nothing OrElse ViewState("SortExpression").ToString() <> e.SortExpression Then
    '                ViewState("SortDirection") = "ASC"
    '            ElseIf ViewState("SortDirection").ToString() = "ASC" Then
    '                ViewState("SortDirection") = "DESC"
    '            ElseIf ViewState("SortDirection").ToString() = "DESC" Then
    '                ViewState("SortDirection") = "ASC"
    '            End If
    '            ViewState("SortExpression") = e.SortExpression
    '            PopulateGrid()
    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
    '        Try
    '            grdMain.PageIndex = e.NewPageIndex
    '            PopulateGrid()
    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        Dim ib As New ImageButton
    '        ib = sender
    '        Response.Redirect("~/secured/PEReviewMainEdit.aspx?id=" & ib.CommandArgument)

    '    End Sub

    '    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '            Response.Redirect("~/secured/PEReviewMainEdit.aspx?id=0")
    '        Else
    '            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
    '        End If

    '    End Sub

    '    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
    '            For i As Integer = 0 To Me.grdMain.Rows.Count - 1
    '                chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
    '                ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
    '                If chk.Checked = True Then
    '                    Generic.DeleteRecordAudit("EPEReviewMain", UserNo, Generic.ToInt(ib.CommandArgument))
    '                    Count = Count + 1
    '                End If
    '            Next
    '            If Count > 0 Then
    '                MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
    '                PopulateGrid()
    '            Else
    '                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
    '            End If
    '        Else
    '            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
    '        End If

    '    End Sub

    '    Protected Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
    '        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
    '            For i As Integer = 0 To Me.grdMain.Rows.Count - 1
    '                chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
    '                ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
    '                If chk.Checked = True Then
    '                    SQLHelper.ExecuteNonQuery("EPEReviewMainWeb_Post", UserNo, Generic.ToInt(ib.CommandArgument))
    '                    Count = Count + 1
    '                End If
    '            Next
    '            If Count > 0 Then
    '                MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccesPost, Me)
    '                PopulateGrid()
    '            Else
    '                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
    '            End If
    '        Else
    '            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
    '        End If

    '    End Sub

    '    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '        Try
    '            Dim ib As New ImageButton
    '            Dim TransNo As Integer = 0
    '            ib = sender
    '            Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
    '            rowno = gvrow.RowIndex

    '            Me.grdMain.SelectedIndex = Generic.ToInt(rowno)
    '            TransNo = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()


    '            Response.Redirect("PEReviewList.aspx?pereviewmainno=" & TransNo)


    '        Catch ex As Exception
    '        End Try
    '    End Sub

    'Protected Sub btnPRocess_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
    '    Dim IsProcess As Boolean = False
    '    ViewState("PEReviewMainNo") = 0

    '    If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
    '        For i As Integer = 0 To Me.grdMain.Rows.Count - 1
    '            chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
    '            ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
    '            If chk.Checked = True Then
    '                ViewState("PEReviewMainNo") = Generic.ToInt(ib.CommandArgument)



    '                Count = Count + 1
    '            End If
    '        Next

    '        If Count = 1 Then

    '            '//validate start here
    '            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
    '            Dim dt As New DataTable
    '            dt = SQLHelper.ExecuteDataTable("EPEReviewMain_WebValidate", ViewState("PEReviewMainNo"))
    '            For Each row As DataRow In dt.Rows
    '                invalid = Generic.ToBol(row("Invalid"))
    '                messagedialog = Generic.ToStr(row("MessageDialog"))
    '                alerttype = Generic.ToStr(row("AlertType"))
    '            Next

    '            If invalid = True Then
    '                MessageBox.Alert(messagedialog, alerttype, Me)
    '                Exit Sub
    '            Else
    '                PEReviewAppendAsyn()
    '                MessageBox.Success(MessageTemplate.SuccessProcess & " " & Now().ToString, Me)
    '            End If
    '            '//end here

    '        ElseIf Count > 1 Then
    '            MessageBox.Warning("Please select 1 transaction to process.", Me)
    '        Else
    '            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
    '        End If
    '    Else
    '        MessageBox.Warning(MessageTemplate.DeniedProcess, Me)

    '    End If
    'End Sub
    'Private Sub PEReviewAppendAsyn()
    '    Dim xcmdProcSAVE As SqlClient.SqlCommand

    '    Try

    '        xcmdProcSAVE = Nothing
    '        xcmdProcSAVE = New SqlClient.SqlCommand

    '        xcmdProcSAVE.CommandText = "EPEReview_WebProcess"
    '        xcmdProcSAVE.CommandType = CommandType.StoredProcedure
    '        xcmdProcSAVE.Connection = xBase.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
    '        xcmdProcSAVE.CommandTimeout = 0

    '        xcmdProcSAVE.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
    '        xcmdProcSAVE.Parameters("@onlineuserno").Value = Generic.CheckDBNull(UserNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

    '        xcmdProcSAVE.Parameters.Add("@PEReviewMainNo", SqlDbType.Int, 4)
    '        xcmdProcSAVE.Parameters("@PEReviewMainNo").Value = Generic.CheckDBNull(ViewState("PEReviewMainNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

    '        xBase.RunCommandAsynchronous(xcmdProcSAVE, "EPEReview_WebProcess", SQLHelper.ConSTRAsyn, IsCompleted)
    '        Session("IsCompleted") = 0 'IsCompleted

    '        If Session("IsCompleted") = 1 Then
    '            'clsModalControls.SetModalPopupControls(CType(Master.FindControl("cphBody"), ContentPlaceHolder), "completed")
    '        End If
    '    Catch
    '        'Response.RedirectLocation = Session("xFormname") & "?IsClickMain=" & IsClickMain
    '    End Try

    'End Sub
    '#End Region

    'End Class


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub
    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPEReviewMain_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim URL As String
            URL = Generic.GetFirstTab("0")
            If URL <> "" Then
                Response.Redirect(URL)
            End If
        End If
    End Sub


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEReviewMainNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPEReviewMain", UserNo, item)
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

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim URL As String
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEReviewMainNo"})))
        If URL <> "" Then
            Response.Redirect(URL)
        End If
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEReviewMainNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("EPEReviewMain_WebPost", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccesPost, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub
    Protected Sub lnkProcess_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
                Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEReviewMainNo"})
                If fieldValues.Count > 1 Or fieldValues.Count = 0 Then
                    MessageBox.Warning("Please select 1 transaction to process.", Me)
                    Exit Sub
                End If


                If fieldValues.Count = 1 Then
                    For Each item As Integer In fieldValues
                        ViewState("Id") = CType(item, Integer)
                        '//validate start here
                        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
                        Dim dt As New DataTable
                        dt = SQLHelper.ExecuteDataTable("EPEReviewMain_WebValidate", item)
                        For Each row As DataRow In dt.Rows
                            invalid = Generic.ToBol(row("Invalid"))
                            messagedialog = Generic.ToStr(row("MessageDialog"))
                            alerttype = Generic.ToStr(row("AlertType"))
                        Next
                        '//end here

                        If invalid = True Then
                            MessageBox.Alert(messagedialog, alerttype, Me)
                            Exit Sub
                        Else
                            PEReviewAppendAsyn()
                            Dim strx As String = process_status
                            If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                                SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, strx, "EPEReviewMain", "EPEReviewMain_WebProcess", 1, ViewState("Id"))
                                PopulateGrid()
                                MessageBox.Critical(strx, Me)
                            Else
                                SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPEReviewMain", "EPEReviewMain_WebProcess", 1, ViewState("Id"))
                                PopulateGrid()
                                process_status = Replace(process_status, "Command complete. Processing Time is :", "Processing completed at ")
                                MessageBox.Success(process_status, Me)
                            End If
                        End If
                    Next
                    MessageBox.Success(MessageTemplate.SuccessProcess & " " & Now().ToString, Me)
                Else
                    MessageBox.Warning("Please select 1 transaction to process.", Me)
                End If
            Else
                MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
            End If
        Catch ex As Exception
        End Try
    End Sub
    'Private Sub PEReviewAppendAsyn(id As String)
    '    Dim cmd As SqlClient.SqlCommand
    '    Try
    '        cmd = Nothing
    '        cmd = New SqlClient.SqlCommand
    '        cmd.CommandText = "EPEReviewMain_WebProcess"
    '        cmd.CommandType = CommandType.StoredProcedure
    '        cmd.Connection = AssynChronous.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
    '        cmd.CommandTimeout = 0
    '        cmd.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
    '        cmd.Parameters("@onlineuserno").Value = Generic.ToInt(UserNo)
    '        cmd.Parameters.Add("@PEReviewMainNo", SqlDbType.Int, 4)
    '        cmd.Parameters("@PEReviewMainNo").Value = id
    '        AssynChronous.RunCommandAsynchronous(cmd, "EPEReviewMain_WebProcess", SQLHelper.ConSTRAsyn, 0)
    '    Catch
    '    End Try

    'End Sub

    Private Sub PEReviewAppendAsyn()
        Dim xcmdProcSAVE As SqlClient.SqlCommand

        Try

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand

            xcmdProcSAVE.CommandText = "EPEReview_WebProcess"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = xBase.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0

            xcmdProcSAVE.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@onlineuserno").Value = Generic.CheckDBNull(UserNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            xcmdProcSAVE.Parameters.Add("@PEReviewMainNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@PEReviewMainNo").Value = Generic.CheckDBNull(ViewState("Id"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            process_status = AssynChronous.xRunCommandAsynchronous(xcmdProcSAVE, "EPEReview_WebProcess", SQLHelper.ConSTRAsyn, IsCompleted, err_num)
            Session("IsCompleted") = 0 'IsCompleted

            If Session("IsCompleted") = 1 Then
                'clsModalControls.SetModalPopupControls(CType(Master.FindControl("cphBody"), ContentPlaceHolder), "completed")
            End If
        Catch
            'Response.RedirectLocation = Session("xFormname") & "?IsClickMain=" & IsClickMain
        End Try

    End Sub


    Protected Sub lnkView_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Response.Redirect("~/secured/PEReviewList.aspx?pereviewmainno=" & container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEReviewMainNo"}))
    End Sub

End Class













