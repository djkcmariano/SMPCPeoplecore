Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_DTRDetlList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim DTRNo As Integer

    Dim xBase As New clsBase.clsBaseLibrary
    Dim IsCompleted As Integer = 0
    Dim process_status As String = ""
    Dim err_num As Integer = 0


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        DTRNo = Generic.ToInt(Request.QueryString("transNo"))
        AccessRights.CheckUser(UserNo, "DTR.aspx", "EDTR")
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        PopulateGrid()


        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 27)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Try
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EDTRDeti_Web", UserNo, DTRNo, Generic.ToInt(cboTabNo.SelectedValue))

            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()

            grdDetl.Columns("Work Allocation").Visible = False

            If Generic.ToInt(cboTabNo.SelectedValue) = 1 Then
                'DTR Present
                grdDetl.Columns("Work Allocation").Visible = True
                If ViewState("DTRDetiNo") = 0 Or IsMain = True Then
                    Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"FullName", "DTRDetiNo", "DTRNo", "EmployeeNo"})
                    lblDetl.Text = obj(0)
                    ViewState("DTRDetiNo") = obj(1)
                End If
            Else
                grdDetl.Columns("Work Allocation").Visible = False
                If (ViewState("EmployeeNo") = 0 And ViewState("DTRNo") = 0) Or IsMain = True Then
                    Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"FullName", "DTRDetiNo", "DTRNo", "EmployeeNo"})
                    lblDetl.Text = obj(0)
                    ViewState("DTRNo") = obj(2)
                    ViewState("EmployeeNo") = obj(3)
                End If
            End If

            PopulateDetl()

            'Previous DTR
            If Generic.ToInt(cboTabNo.SelectedValue) = 2 Then
                lnkPreviousDTR.Visible = True
            Else
                lnkPreviousDTR.Visible = False
            End If
            PopulatePrev()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub PopulateDetl()
        Try

            lblTab.Text = Generic.ToStr(cboTabNo.SelectedItem.Text)

            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EDTRDetiLog_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), Generic.ToInt(ViewState("DTRDetiNo")), Generic.ToInt(ViewState("DTRNo")), Generic.ToInt(ViewState("EmployeeNo")))
            grdDetl.DataSource = _dt
            grdDetl.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"FullName", "DTRDetiNo", "DTRNo", "EmployeeNo"})
        lblDetl.Text = obj(0)
        ViewState("DTRDetiNo") = obj(1)
        ViewState("DTRNo") = obj(2)
        ViewState("EmployeeNo") = obj(3)

        PopulateDetl()
        PopulatePrev()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid(True)
    End Sub

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            Dim x As New XlsxExportOptionsEx
            x.ExportType = ExportType.WYSIWYG
            ASPxGridViewExporter1.ReportHeader = "Name :" & Generic.ToStr(lblDetl.Text)
            ASPxGridViewExporter1.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkViewShift_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As LinkButton
            lnk = sender

            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable

            dtx = SQLHelper.ExecuteDataTable("EShift_Web_DTRDetiLog_View_ShiftNo", Generic.ToInt(lnk.CommandArgument))

            For Each rowx As DataRow In dtx.Rows
                messagedialog = Generic.ToStr(rowx("SQLString"))
            Next

            MessageBox.Alert(messagedialog, "information", Me, "topRight")


        Catch ex As Exception

        End Try
    End Sub
#Region "******Previous DTR*******"

    Private Sub PopulatePrev()
        Try
            Dim _dt As DataTable
            If Generic.ToInt(cboTabNo.SelectedValue) = 2 Then
                _dt = SQLHelper.ExecuteDataTable("EDTRDetiLog_Web_DTRPrev", UserNo, Generic.ToInt(ViewState("DTRNo")), Generic.ToInt(ViewState("EmployeeNo")))
            Else
                _dt = SQLHelper.ExecuteDataTable("EDTRDetiLog_Web_DTRPrev", UserNo, 0, 0)
            End If
            grdDetlPrev.DataSource = _dt
            grdDetlPrev.DataBind()


        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "***** Work Allocation *****"
    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRNo", "DTRDetiLogNo"})

        Response.Redirect("~/Secured/DTRDetlList_Allocation.aspx?id=" & obj(0) & "&transNo=" & obj(1))

    End Sub
#End Region

#Region "*** Process ***"
    Protected Sub lnkProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess, "DTR.aspx", "EDTR") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRNo", "EmployeeNo"})
            ViewState("fDTRNo") = obj(0)
            ViewState("fEmployeeNo") = obj(1)


            DTRAppendAsyn()
            Dim strx As String = process_status
            If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, strx, "EDTR", "EDTR_WebProcessDetiLogUpdate", 1, ViewState("fDTRNo"))
                PopulateGrid()
                MessageBox.Critical(strx, Me)
            Else
                SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EDTR", "EDTR_WebProcessDetiLogUpdate", 1, ViewState("fDTRNo"))
                PopulateGrid()
                process_status = Replace(process_status, "Command complete. Processing Time is :", "DTR Processing completed at ")
                MessageBox.Success(process_status, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If
    End Sub

    Private Sub DTRAppendAsyn()
        Dim xcmdProcSAVE As SqlClient.SqlCommand

        Try

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand

            xcmdProcSAVE.CommandText = "EDTR_WebProcessDetiLogUpdate"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = xBase.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0

            xcmdProcSAVE.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@onlineuserno").Value = Generic.CheckDBNull(UserNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            xcmdProcSAVE.Parameters.Add("@DTRNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@DTRNo").Value = Generic.CheckDBNull(ViewState("fDTRNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            xcmdProcSAVE.Parameters.Add("@fEmployeeNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@fEmployeeNo").Value = Generic.CheckDBNull(ViewState("fEmployeeNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            process_status = AssynChronous.xRunCommandAsynchronous(xcmdProcSAVE, "EDTR_WebProcessDetiLogUpdate", SQLHelper.ConSTRAsyn, IsCompleted, err_num)
            Session("IsCompleted") = 0 'IsCompleted

            If Session("IsCompleted") = 1 Then
                'clsModalControls.SetModalPopupControls(CType(Master.FindControl("cphBody"), ContentPlaceHolder), "completed")
            End If
        Catch
            'Response.RedirectLocation = Session("xFormname") & "?IsClickMain=" & IsClickMain
        End Try

    End Sub

#End Region

    Protected Sub ASPxGridViewExporter1_RenderBrick(sender As Object, e As DevExpress.Web.ASPxGridViewExportRenderingEventArgs) Handles ASPxGridViewExporter1.RenderBrick
        Dim dataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
        If e.RowType = GridViewRowType.Data AndAlso dataColumn IsNot Nothing Then
            Select Case dataColumn.FieldName
                Case "AbsHrs"
                    e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
                    e.TextValue = e.TextValue.ToString.Replace("<span>", "")
                    e.TextValue = e.TextValue.ToString.Replace("</span>", "")
                Case "Late"
                    e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
                    e.TextValue = e.TextValue.ToString.Replace("<span>", "")
                    e.TextValue = e.TextValue.ToString.Replace("</span>", "")
                Case "Under"
                    e.TextValue = e.TextValue.ToString.Replace("<span style=" & """color:red;"">", "")
                    e.TextValue = e.TextValue.ToString.Replace("<span>", "")
                    e.TextValue = e.TextValue.ToString.Replace("</span>", "")
            End Select





        End If
        If e.RowType = GridViewRowType.Header AndAlso dataColumn IsNot Nothing Then
            e.Text = e.Text.Replace("<br/>", " ")
            e.Text = e.Text.Replace("<br>", " ")
            e.Text = e.Text.Replace("<center>", "")
            e.Text = e.Text.Replace("</center>", "")
        End If

    End Sub

    Protected Sub lnkApproved_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRDetiNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, 2)
            i = i + 1
        Next

        If i > 0 Then
            PopulateGrid(True)
            'Dim url As String = "DTRDetlList.aspx"
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Protected Sub lnkDisApproved_Click(sender As Object, e As System.EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRDetiNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, 3)
            i = i + 1
        Next

        If i > 0 Then
            PopulateGrid(True)
            'Dim url As String = "DTRDetlList.aspx"
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDisapproved, Me)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Private Sub ApproveTransaction(tId As Integer, issubmit As Integer)
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("ETimeSheet_WebApproved", UserNo, tId, issubmit)
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRDetiNo", "IsEnabled"})
            Dim iNo As Integer = Generic.ToInt(obj(0))
            Dim IsEnabled As Boolean = Generic.ToBol(obj(1))
            PopulateData(iNo)
            'btnSave.Enabled = IsEnabled

            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub


    Protected Sub PopulateData(fid As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRDeti_WebOne", UserNo, fid)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupMain", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Function SaveRecord() As Integer
        Dim DTRDetiNo As Integer = Generic.ToInt(txtDTRDetiNo.Text)

        Dim Ovt As Double = Generic.ToDec(txtOvt.Text)
        Dim Ovt8 As Double = Generic.ToDec(txtOvt8.Text)
        Dim NPOvt As Double = Generic.ToDec(txtNPOvt.Text)
        Dim NPOvt8 As Double = Generic.ToDec(txtNPOvt8.Text)
        Dim RDOvt As Double = Generic.ToDec(txtRDOvt.Text)
        Dim RDOvt8 As Double = Generic.ToDec(txtRDOvt8.Text)
        Dim RDOvtNP As Double = Generic.ToDec(txtRDOvtNP.Text)
        Dim RDOvt8NP As Double = Generic.ToDec(txtRDOvt8NP.Text)
        Dim RHNROvt As Double = Generic.ToDec(txtRHNROvt.Text)
        Dim RHNROvt8 As Double = Generic.ToDec(txtRHNROvt8.Text)
        Dim RHNROvtNP As Double = Generic.ToDec(txtRHNROvtNP.Text)
        Dim RHNROvt8NP As Double = Generic.ToDec(txtRHNROvt8NP.Text)
        Dim RHRDOvt As Double = Generic.ToDec(txtRHRDOvt.Text)
        Dim RHRDOvt8 As Double = Generic.ToDec(txtRHRDOvt8.Text)
        Dim RHRDOvtNP As Double = Generic.ToDec(txtRHRDOvtNP.Text)
        Dim RHRDOvt8NP As Double = Generic.ToDec(txtRHRDOvt8NP.Text)
        Dim SHNROvt As Double = Generic.ToDec(txtSHNROvt.Text)
        Dim SHNROvt8 As Double = Generic.ToDec(txtSHNROvt8.Text)
        Dim SHNROvtNP As Double = Generic.ToDec(txtSHNROvtNP.Text)
        Dim SHNROvt8NP As Double = Generic.ToDec(txtSHNROvt8NP.Text)
        Dim SHRDOvt As Double = Generic.ToDec(txtSHRDOvt.Text)
        Dim SHRDOvt8 As Double = Generic.ToDec(txtSHRDOvt8.Text)
        Dim SHRDOvtNP As Double = Generic.ToDec(txtSHRDOvtNP.Text)
        Dim SHRDOvt8NP As Double = Generic.ToDec(txtSHRDOvt8NP.Text)

        If SQLHelper.ExecuteNonQuery("EDTRDeti_Websave", UserNo, DTRDetiNo, Ovt, Ovt8, NPOvt, NPOvt8, RDOvt, RDOvt8, RDOvtNP, RDOvt8NP, RHNROvt, RHNROvt8, RHNROvtNP, RHNROvt8NP, RHRDOvt, RHRDOvt8, RHRDOvtNP, RHRDOvt8NP, SHNROvt, SHNROvt8, SHNROvtNP, SHNROvt8NP, SHRDOvt, SHRDOvt8, SHRDOvtNP, SHRDOvt8NP) Then
            SaveRecord = True
        Else
            SaveRecord = True

        End If

    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Protected Sub lnkHistory_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then

            Try
                Dim lnk As New LinkButton
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRDetiNo", "IsEnabled"})
                Dim iNo As Integer = Generic.ToInt(obj(0))
                Dim IsEnabled As Boolean = Generic.ToBol(obj(1))
                PopulateAudit(iNo)
                ModalPopupExtender1.Show()
                PopulateGrid()
            Catch ex As Exception

            End Try
            
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If


        

    End Sub
    Protected Sub PopulateAudit(Auid As Int64)
        Try
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EDTRDetiUpdated_Web", UserNo, Auid)
            Me.grdHistory.DataSource = _dt
            Me.grdHistory.DataBind()
        Catch ex As Exception

        End Try

    End Sub

#Region "********MAIN Check All********"

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs)
        If e.ButtonType <> ColumnCommandButtonType.SelectCheckbox Then
            Return
        End If
        Dim rowEnabled As Boolean = getRowEnabledStatus(e.VisibleIndex)
        e.Enabled = rowEnabled
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

    Protected Sub Page_PreInit(sender As Object, e As System.EventArgs) Handles Me.PreInit

    End Sub
End Class


