Imports System.Data
Imports System.Math
Imports System.Threading
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_PayBankDiskList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim IsCompleted As Integer = 0
    Dim process_status As String = ""
    Dim err_num As Integer = 0

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayBank_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateDropDownList()
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 4)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPay_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel1", dt)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDownList()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("EPayBank_WebPost", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccesPost, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub


    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim i As Integer = 0 'SaveRecord()
        If i = 1 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        ElseIf i = 2 Then
            MessageBox.Warning(MessageTemplate.DuplicateRecord, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkProcess_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
                Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayNo"})
                If fieldValues.Count > 1 Or fieldValues.Count = 0 Then
                    MessageBox.Warning("Please select 1 transaction to process.", Me)
                    Exit Sub
                End If
                If fieldValues.Count = 1 Then
                    For Each item As Integer In fieldValues
                        ViewState("Id") = item
                        Bank_AppendAsyn(item)
                    Next
                End If

                If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, process_status, "EPayBank", "EPayBank_WebProcess", 5, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Critical(process_status, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPayBank", "EPayBank_WebProcess", 5, ViewState("Id"))
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

        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
            ViewState("Id") = i

            Dim IsPosted As Boolean = True
            IsPosted = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsPosted"}))

            If IsPosted = False Then
                Bank_AppendAsyn(i)
                Dim strx As String = process_status
                If err_num <> 0 Then ' strx.Substring(0, 3).ToLower = "msg" Then
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, err_num, strx, "EPayBank", "EPayBank_WebProcess", 5, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Critical(strx, Me)
                Else
                    SQLHelper.ExecuteNonQuery("EErrorLog_WebSave", UserNo, 0, "", "EPayBank", "EPayBank_WebProcess", 5, ViewState("Id"))
                    PopulateGrid()
                    MessageBox.Success(process_status, Me)
                End If
            Else
                MessageBox.Warning(MessageTemplate.PostedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If

    End Sub
    Private Sub Bank_AppendAsyn(id As Integer)
        Dim xcmdProcSAVE As SqlClient.SqlCommand

        Try
            'clsbase.OpenConnectionAsyn()

            xcmdProcSAVE = Nothing
            xcmdProcSAVE = New SqlClient.SqlCommand
            '
            xcmdProcSAVE.CommandText = "EPayBank_WebProcess"
            xcmdProcSAVE.CommandType = CommandType.StoredProcedure
            xcmdProcSAVE.Connection = AssynChronous.xOpenConnectionAsyn(SQLHelper.ConSTRAsyn)
            xcmdProcSAVE.CommandTimeout = 0

            xcmdProcSAVE.Parameters.Add("@onlineuserno", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@onlineuserno").Value = UserNo

            xcmdProcSAVE.Parameters.Add("@PayNo", SqlDbType.Int, 4)
            xcmdProcSAVE.Parameters("@PayNo").Value = Generic.ToInt(id)

            'AssynChronous.RunCommandAsynchronous(xcmdProcSAVE, "EPayBankDisk_WebProcess", SQLHelper.ConSTRAsyn, IsCompleted)
            process_status = AssynChronous.xRunCommandAsynchronous(xcmdProcSAVE, "EPayBank_WebProcess", SQLHelper.ConSTRAsyn, IsCompleted, err_num)
            Session("IsCompleted") = IsCompleted


        Catch

        End Try

    End Sub

    Protected Sub lnkSummary_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Response.Redirect("~/secured/PayBankDiskDetiList.aspx?id=" & container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
    End Sub

    Protected Sub lnkBankRef_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/PayBankDiskRefList.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
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
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        'Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayNo"}))
        Dim id As Integer = grdMain.GetRowValues(Integer.Parse(hf("VisibleIndex").ToString()), "PayNo")
        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, PayLocNo.ToString), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"), _
                                                  New ReportParameter(ReportParameter.Type.int, id.ToString()), _
                                                  New ReportParameter(ReportParameter.Type.int, "0"))
        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=83&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

#End Region


End Class





