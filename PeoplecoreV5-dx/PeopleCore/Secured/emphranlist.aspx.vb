Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web


Partial Class Secured_EmpHRANList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim PayLocNo As Integer

     Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable

        If Generic.ToInt(cboTabNo.SelectedValue) = 0 Then 'Open
            lnkDelete.Visible = True
            lnkAdd.Visible = True
            lnkPost.Visible = False
            lnkApproved.Visible = True
            lnkDisapproved.Visible = True
            lnkSync.Visible = False
        ElseIf Generic.ToInt(cboTabNo.SelectedValue) = 2 Then 'For Posting
            lnkDelete.Visible = False
            lnkAdd.Visible = False
            lnkPost.Visible = True
            lnkApproved.Visible = False
            lnkDisapproved.Visible = False
            lnkSync.Visible = True
        Else
            lnkDelete.Visible = False
            lnkAdd.Visible = False
            lnkPost.Visible = False
            lnkApproved.Visible = False
            lnkDisapproved.Visible = False
            lnkSync.Visible = False
        End If

        If Generic.ToStr(Session("xMenuType")) = "0269000000" Then
            _dt = SQLHelper.ExecuteDataTable("EHRAN_WebTransfer", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
        Else
            _dt = SQLHelper.ExecuteDataTable("EHRAN_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
        End If

        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        Session("TabNo_HRAN") = Generic.ToInt(cboTabNo.SelectedValue)

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkSync_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim ds As New DataSet
        ds = SQLHelper.ExecuteDataSet("ESyncProcess", UserNo, PayLocNo, 1)
        If ds.Tables(0).Rows(0).Item(0).ToString() = "0" Then
            MessageBox.Success(MessageTemplate.SuccesPost, Me)
        Else
            MessageBox.Success(MessageTemplate.ErrorPost, Me)
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            cboTabNo.Text = Generic.ToStr(Session("TabNo_HRAN"))
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim URL As String
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)        
        URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"HRANNo"})))
        If Generic.ToStr(Session("xMenuType")) = "0269000000" Then
            Dim fhranNo As String = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"HRANNo"}))
            Response.Redirect("emphranedit.aspx?id=" & fhranNo.ToString)
        Else
            If URL <> "" Then
                Response.Redirect(URL)
            End If
        End If
        
    End Sub

    Protected Sub lnkPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)        
        Dim tcount As Integer = 0, dt As New DataTable
        Dim ts As Integer = 0, i As Integer = 0
        Dim ffullname As String = "", tfullname As String = "", lblEmpCode As New Label, nfullName As String = ""

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Try
                Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"HRANNo"})
                For Each item As Integer In fieldValues
                    dt = SQLHelper.ExecuteDataTable("EHRAN_WebOne", UserNo, item)
                    Dim PreparationDate As String = Generic.ToStr(dt.Rows(0)("PreparationDate"))
                    Dim EffectivityDate As String = Generic.ToStr(dt.Rows(0)("Effectivity"))
                    Dim IsReady As Integer = Generic.ToInt(dt.Rows(0)("IsReady"))
                    Dim Fullname As String = Generic.ToStr(dt.Rows(0)("FullName"))
                    Dim IsEffective As Integer = Generic.ToInt(dt.Rows(0)("IsEffective"))
                    Dim tproceed As Integer = 0
                    If PreparationDate <> "" And EffectivityDate <> "" And IsReady <> 0 Then
                        If IsEffective = 1 Then
                            'Post To 201 record
                            Dim dt1 As DataTable
                            dt1 = SQLHelper.ExecuteDataTable("EHRAN_WebPost", Generic.ToInt(item), UserNo)
                            For Each row As DataRow In dt1.Rows
                                ffullname = Generic.ToStr(row("ffullname"))
                                tproceed = Generic.ToInt(row("tProceed"))
                            Next

                            If ffullname <> "" Then
                                If tfullname <> "" Then
                                    tfullname = tfullname & " , " & ffullname
                                Else
                                    tfullname = ffullname
                                End If
                            ElseIf tproceed = 1 Then
                                ts = ts + 1
                            End If
                        Else
                            i = i + 1
                            If nfullName.ToString <> "" Then
                                nfullName = "<br/>" & nfullName.ToString & "<br/>" & Fullname.ToString
                            Else
                                nfullName = Fullname.ToString
                            End If
                        End If
                    Else
                        If nfullName.ToString <> "" Then
                            nfullName = "<br/>" & nfullName.ToString & "<br/>" & Fullname.ToString
                        Else
                            nfullName = Fullname.ToString
                        End If
                    End If
                Next


                If i > 0 Then
                    MessageBox.Alert("Unable to post transaction for " + nfullName.ToString + " Please check Effective Date ", "warning", Me)
                End If

                If nfullName.ToString <> "" Then
                    MessageBox.Alert("Shows invalid of data either prepared date, effectivity date or ready for posting checkbox. Here are the list : <b>" + nfullName.ToString + "</b>", "warning", Me)
                Else
                    If tfullname <> "" Then
                        If ts > 0 Then
                            MessageBox.Alert("There are " + ts.ToString + " transaction(s) posted to 201 file and some are unable to complete the checklist requirement(s). Here are the list : " + tfullname + "", "warning", Me)
                        End If
                        MessageBox.Alert("List of employee(s) unable to complete the checklist requirement(s) :  " + tfullname + "", "warning", Me)
                    Else
                        MessageBox.Success("There are " + ts.ToString + " transaction(s) posted to 201 file!", Me)
                    End If
                End If

                PopulateGrid()
            Catch
            End Try
        End If
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            If Generic.ToStr(Session("xMenuType")) = "0269000000" Then
                Response.Redirect("emphranedit.aspx")
            Else
                Dim URL As String
                URL = Generic.GetFirstTab("0")
                If URL <> "" Then
                    Response.Redirect(URL)
                End If
            End If
           
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"HRANNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EHRANChecklist", UserNo, "HRANNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EHRANApprovalRouting", UserNo, "HRANNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EHRANAllowance", UserNo, "HRANNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EHRAN", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 22)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkApproved_Click(sender As Object, e As EventArgs)

        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "HRANNo"))
                ApproveTransaction(item, 2)
                grdMain.Selection.UnselectRow(j)
                i = i + 1
            End If
        Next

        PopulateGrid()
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me)

    End Sub
    Protected Sub lnkDisapproved_Click(sender As Object, e As EventArgs)

        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "HRANNo"))
                ApproveTransaction(item, 3)
                grdMain.Selection.UnselectRow(j)
                i = i + 1
            End If
        Next

        PopulateGrid()
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDisapproved, Me)

    End Sub


    Private Sub ApproveTransaction(tId As Integer, approvalStatNo As Integer)
        Dim URL As String = Request.Url.GetLeftPart(UriPartial.Authority) & "/frmEmailApproved.aspx"
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("EHRAN_Web_Approved", UserNo, tId, approvalStatNo, True, True, URL)
        If fds.Tables.Count > 0 Then
            If fds.Tables(0).Rows.Count > 0 Then
                Dim IsWithapprover As Boolean
                IsWithapprover = Generic.CheckDBNull(fds.Tables(0).Rows(0)("IsWithApprover"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                If IsWithapprover = True Then

                Else
                    MessageBox.Information("Unable to locate the next approver.", Me)
                End If
            End If


        End If
    End Sub

#Region "********Reports********"

    Protected Sub MyGridView_FillContextMenuItems(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuEventArgs)
        If e.MenuType = GridViewContextMenuType.Rows Then
            'e.Items.Add(e.CreateItem("Get Key", "GetKey"))
            e.Items.Clear()
        End If
    End Sub

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim lnk As LinkButton = TryCast(sender, LinkButton)
            Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            NewScriptManager.RegisterPostBackControl(lnk)
        End If
        
    End Sub

    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "HRANNo")

        Dim param As String = ""
        Dim tProceed As Boolean = False
        Dim ReportNo As Integer = 0, dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EReport_WebViewer", UserNo, 398, "", id, PayLocNo)
        For Each row As DataRow In dt.Rows
            ReportNo = Generic.ToInt(row("ReportNo"))
            param = Generic.ToStr(row("param"))
            tProceed = Generic.ToStr(row("tProceed"))
        Next

        If tProceed = True Then
            sb.Append("<script>")
            sb.Append("window.open('rpttemplateviewer.aspx?reportno=" & ReportNo & "&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
            sb.Append("</script>")
            ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
        Else
            MessageBox.Warning("No access permission to view the report.", Me)
        End If

    End Sub

    'Protected Sub lnkContract_Click(sender As Object, e As EventArgs)
    '    Dim lnk As New LinkButton
    '    Dim sb As New StringBuilder
    '    lnk = sender
    '    Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
    '    Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "HRANNo")

    '    Dim param As String = ""
    '    Dim tProceed As Boolean = False
    '    Dim ReportNo As Integer = 0, dt As DataTable
    '    dt = SQLHelper.ExecuteDataTable("EReport_WebViewer", UserNo, 0, "HRAN Contract", id, PayLocNo)
    '    For Each row As DataRow In dt.Rows
    '        ReportNo = Generic.ToInt(row("ReportNo"))
    '        param = Generic.ToStr(row("param"))
    '        tProceed = Generic.ToStr(row("tProceed"))
    '    Next

    '    If tProceed = True Then
    '        sb.Append("<script>")            
    '        sb.Append("window.open('rpttemplateviewer.aspx?reportno=" & ReportNo & "&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
    '        sb.Append("</script>")
    '        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    '    Else
    '        MessageBox.Warning("No access permission to view the report.", Me)
    '    End If

    'End Sub

#End Region

#Region "********Detail Check All********"


    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs)
        If e.ButtonType <> ColumnCommandButtonType.SelectCheckbox Then
            Return
        End If
        Dim rowEnabled As Boolean = getRowEnabledStatus(e.VisibleIndex)
        e.Enabled = rowEnabled

        'If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
        '    Dim isSelected As Boolean = Convert.ToBoolean(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
        '    If isSelected Then

        '        grdMain.Selection.SetSelection(e.VisibleIndex, True)

        '    End If
        'End If
    End Sub
    Private Function getRowEnabledStatus(ByVal VisibleIndex As Integer) As Boolean
        Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(VisibleIndex, "IsEnabled"))
        If value = True Then
            Return True
        Else
            Return False
        End If
    End Function
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
    Protected Sub gridMain_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        Boolean.TryParse(e.Parameters, False)

        Dim startIndex As Integer = grdMain.PageIndex * grdMain.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdMain.VisibleRowCount, startIndex + grdMain.SettingsPager.PageSize)
        For i As Integer = startIndex To endIndex - 1
            Dim rowEnabled As Boolean = getRowEnabledStatus(i)
            If rowEnabled AndAlso e.Parameters = "true" Then
                grdMain.Selection.SelectRow(i)
            Else
                grdMain.Selection.UnselectRow(i)
            End If
        Next i

    End Sub

#End Region


End Class



