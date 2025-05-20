Imports System.Data
Imports System.Math
Imports System.Threading
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web


Partial Class Secured_PayContList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim process_status As String = ""
    Dim err_num As Integer = 0

    Private Sub PopulateGrid()

        Dim tstatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayCont_Web", UserNo, tstatus, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 4)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub


#Region "********Main*******"

    Protected Sub lnkCont_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayContNo", "Code"})
        Dim i As Integer = Generic.ToInt(obj(0))

        Response.Redirect("PayContEdit_Cont.aspx?id=" & i)
    End Sub

    Protected Sub lnkLoan_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayContNo", "Code"})
        Dim i As Integer = Generic.ToInt(obj(0))

        Response.Redirect("PayContEdit_Loan.aspx?id=" & i)
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayContNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EPayContDetiLoan", UserNo, "PayContNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EPayContDeti", UserNo, "PayContNo", CType(item, Integer))
                'Generic.DeleteRecordAuditCol("EPayContDetiOther", UserNo, "PayContNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EPayCont", UserNo, CType(item, Integer))
                Try
                    SQLHelper.ExecuteNonQuery("EPayContDetiOther_WebDelete", UserNo, CType(item, Integer))
                Catch ex As Exception

                End Try
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

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Response.Redirect("~/secured/PayContEdit.aspx?id=" & Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayContNo"})))

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/PayContEdit.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Count As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayContNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("zTable_WebPost", UserNo, "EPayCont", CType(item, Integer)) Then
                    Count = Count + 1
                End If
            Next

            If Count > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + Count.ToString + ") " + MessageTemplate.SuccesPost, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub

    Protected Sub lnkProcess_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
                Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayContNo"})
                If fieldValues.Count > 1 Or fieldValues.Count = 0 Then
                    MessageBox.Warning("Please select 1 transaction to process.", Me)
                    Exit Sub
                End If
                If fieldValues.Count = 1 Then
                    For Each item As Integer In fieldValues
                        ViewState("Id") = item
                        ContributionAppendAsyn(item)
                    Next
                End If

                If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, process_status, "EPayCont", "EPayCont_WebProcess", 6, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Critical(process_status, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPayCont", "EPayCont_WebProcess", 6, ViewState("Id"))
                    PopulateGrid()
                    process_status = Replace(process_status, "Command complete. Processing Time is :", "Processing completed at ")
                    MessageBox.Success(process_status, Me)
                End If
            Else
                MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkProcess_Detail_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayContNo"}))
            ViewState("Id") = i

            Dim IsPosted As Boolean = True
            IsPosted = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsPosted"}))

            If IsPosted = False Then
                ContributionAppendAsyn(i)
                Dim strx As String = process_status
                If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, strx, "EPayCont", "EPayCont_WebProcess", 6, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Critical(strx, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPayCont", "EPayCont_WebProcess", 6, ViewState("Id"))
                    PopulateGrid()
                    process_status = Replace(process_status, "Command complete. Processing Time is :", "Processing completed at ")
                    MessageBox.Success(process_status, Me)
                End If
            Else
                MessageBox.Warning(MessageTemplate.PostedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If

    End Sub
    Private Sub ContributionAppendAsyn(id As Integer)
        Dim xcmdProcSAVE As SqlClient.SqlCommand

        Try
            'clsbase.OpenConnectionAsyn()

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand
            '
            xcmdProcSAVE.CommandText = "EPayCont_WebProcess"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = AssynChronous.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0

            xcmdProcSAVE.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@onlineuserno").Value = UserNo

            xcmdProcSAVE.Parameters.Add("@PayContNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@PayContNo").Value = Generic.ToInt(id)

            'AssynChronous.RunCommandAsynchronous(xcmdProcSAVE, "EPayCont_WebProcess", SQLHelper.ConSTRAsyn, 0)
            process_status = AssynChronous.xRunCommandAsynchronous(xcmdProcSAVE, "EPayCont_WebProcess", SQLHelper.ConSTRAsyn, 0, err_num)
            Session("IsCompleted") = 0 'IsCompleted

        Catch
        End Try

    End Sub

#End Region


End Class
