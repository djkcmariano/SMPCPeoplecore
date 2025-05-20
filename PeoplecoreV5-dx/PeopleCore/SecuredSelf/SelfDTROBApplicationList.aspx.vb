Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports System.Net
Imports System.Web.Script.Serialization

Partial Class SecuredSelf_SelfDTROBApplicationList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0
    Dim OnlineEmpNo As Integer = 0

    Protected Sub PopulateGrid()
        Try

            Dim tstatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)

            If tstatus = 2 Then
                lnkCancel.Visible = False
                lnkDelete.Visible = False
                grdDetl.Columns("Reason").Visible = True
                grdDetl.Columns("Status").Visible = True
                grdDetl.Columns("Select").Visible = True
            ElseIf tstatus = 0 Then
                lnkCancel.Visible = False
                lnkDelete.Visible = False
                grdDetl.Columns("Reason").Visible = True
                grdDetl.Columns("Status").Visible = True
                grdDetl.Columns("Select").Visible = False
            Else
                lnkCancel.Visible = False
                grdDetl.Columns("Reason").Visible = False
                grdDetl.Columns("Status").Visible = False
                grdDetl.Columns("Select").Visible = False
            End If

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRLogMain_WebSelf", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRLogMain_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        OnlineEmpNo = Generic.ToInt(Session("EmployeeNo"))
        Permission.IsAuthenticated()
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        PopulateGrid()
        PopulateGridDetl()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList_Self(UserNo, Me, "pnlPopupDetl", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 12)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Try
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.EnableControls(Me, "pnlPopupDetl", True)
            lnkSave.Enabled = True
            mdlDetl.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRLogMainNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Generic.EnableControls(Me, "pnlPopupDetl", IsEnabled)
            lnkSave.Enabled = IsEnabled
            mdlDetl.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRLogMainNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTRLogMain", UserNo, item)
                Generic.DeleteRecordAuditCol("EDTRLog", UserNo, "DTRLogMainNo", item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
            PopulateGridDetl()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
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

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim RetVal As Boolean = False
        Dim DTRLogMainNo As Integer = Generic.ToInt(txtDTRLogMainNo.Text)
        Dim EmployeeNo As Integer = OnlineEmpNo  'Generic.ToInt(hifEmployeeNo.Value)
        Dim StartDate As String = Generic.ToStr(txtStartDate.Text)
        Dim EndDate As String = Generic.ToStr(txtEndDate.Text)
        Dim In1 As String = Generic.ToStr(Replace(txtIn1.Text, ":", ""))
        Dim Out1 As String = Generic.ToStr(Replace(txtOut1.Text, ":", ""))
        Dim In2 As String = Generic.ToStr(Replace(txtIn2.Text, ":", ""))
        Dim Out2 As String = Generic.ToStr(Replace(txtOut2.Text, ":", ""))
        Dim Reason As String = Generic.ToStr(txtReason.Text)
        Dim ApprovalStatNo As Integer = Generic.ToInt(cboApprovalStatNo.SelectedValue)
        Dim ComponentNo As Integer = 3 'Self Service

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim IdNo As String = "", SuperiorIdNo As Integer = 0, MobileNo As String = "", Message As String = ""

        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EDTRLogMain_WebValidate", UserNo, DTRLogMainNo, EmployeeNo, StartDate, EndDate, In1, Out1, In2, Out2, Reason, 1, PayLocNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlDetl.Show()
            Exit Sub
        End If

        'If SQLHelper.ExecuteNonQuery("EDTRLogMain_WebSaveSelf", UserNo, DTRLogMainNo, EmployeeNo, StartDate, EndDate, In1, Out1, In2, Out2, Reason, 1, PayLocNo) Then
        '    RetVal = True
        'Else
        '    RetVal = False
        'End If

        Dim dts As DataSet
        dts = SQLHelper.ExecuteDataSet("EDTRLogMain_WebSaveSelf", UserNo, DTRLogMainNo, EmployeeNo, StartDate, EndDate, In1, Out1, In2, Out2, Reason, 1, PayLocNo)

        If dts.Tables.Count > 0 Then
            If dts.Tables.Count > 0 Then

                IdNo = Generic.CheckDBNull(dts.Tables(1).Rows(0)("IdNo"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                SuperiorIdNo = Generic.CheckDBNull(dts.Tables(1).Rows(0)("SuperiorIdNo"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                MobileNo = Generic.CheckDBNull(dts.Tables(1).Rows(0)("MobileNo"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                Message = Generic.CheckDBNull(dts.Tables(1).Rows(0)("Message"), clsBase.clsBaseLibrary.enumObjectType.StrType)

                If MobileNo <> "" Then

                    Dim httpWebRequest = CType(WebRequest.Create("http://Rubidium/smsapi/api/sms"), HttpWebRequest)
                    httpWebRequest.ContentType = "application/json"
                    httpWebRequest.Method = "POST"

                    Using streamWriter = New StreamWriter(httpWebRequest.GetRequestStream())
                        Dim json As String = New JavaScriptSerializer().Serialize(New With {Key .IdNo = IdNo, _
                                                                                            .SuperiorIdNo = SuperiorIdNo, _
                                                                                            .EmployeeNo = EmployeeNo, _
                                                                                            .MobileNo = MobileNo, _
                                                                                            .Message = Message})
                        streamWriter.Write(json)
                    End Using

                    Dim httpResponse = CType(httpWebRequest.GetResponse(), HttpWebResponse)
                    Using streamReader = New StreamReader(httpResponse.GetResponseStream())
                        Dim result = streamReader.ReadToEnd()
                    End Using

                    RetVal = True

                Else
                    RetVal = True
                End If

            End If
        End If

        If RetVal = True Then
            PopulateGrid()
            PopulateGridDetl()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub gridDetl_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        Boolean.TryParse(e.Parameters, False)

        Dim startIndex As Integer = grdDetl.PageIndex * grdDetl.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdDetl.VisibleRowCount, startIndex + grdDetl.SettingsPager.PageSize)
        For i As Integer = startIndex To endIndex - 1
            Dim rowEnabled As Boolean = getRowEnabledStatus(i)
            If rowEnabled AndAlso e.Parameters = "true" Then
                grdDetl.Selection.SelectRow(i)
            Else
                grdDetl.Selection.UnselectRow(i)
            End If
        Next i

    End Sub
    Protected Sub cbCheckAll_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim cb As ASPxCheckBox = DirectCast(sender, ASPxCheckBox)
        cb.ClientSideEvents.CheckedChanged = String.Format("cbCheckAll_CheckedChanged")
        cb.Checked = False
        Dim count As Integer = 0
        Dim startIndex As Integer = grdDetl.PageIndex * grdDetl.SettingsPager.PageSize
        Dim endIndex As Integer = Math.Min(grdDetl.VisibleRowCount, startIndex + grdDetl.SettingsPager.PageSize)

        For i As Integer = startIndex To endIndex - 1
            If grdDetl.Selection.IsRowSelected(i) Then
                count = count + 1
            End If
        Next i

        If count > 0 Then
            cb.Checked = True
        End If
    End Sub
    Protected Sub lnkSaveCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim tno As Integer = 0, i As Integer = 0

        For j As Integer = 0 To grdDetl.VisibleRowCount - 1
            If grdDetl.Selection.IsRowSelected(j) Then
                Dim DTRLogNo As Integer = Generic.ToInt(grdDetl.GetRowValues(j, "DTRLogNo"))
                Dim EmpNo As Integer = Generic.ToInt(grdDetl.GetRowValues(j, "EmployeeNo"))
                Dim Reason As String = Generic.ToStr(txtCancellationRemark.Text)
                
                Dim fds As DataSet
                fds = SQLHelper.ExecuteDataSet("EDTRLogApplicationCancel_WebSaveSelf", UserNo, DTRLogNo, EmpNo, Reason)
                i = i + 1
                'If fds.Tables.Count > 0 Then
                '    If fds.Tables(0).Rows.Count > 0 Then
                '        Dim IsWithapprover As Boolean
                '        IsWithapprover = Generic.CheckDBNull(fds.Tables(0).Rows(0)("IsWithApprover"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                '        If IsWithapprover = True Then
                '            i = i + 1
                '        End If
                '    End If
                'End If
                grdDetl.Selection.UnselectRow(j)
            End If
        Next

        If i > 0 Then
            PopulateGridDetl()
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessCancel, Me)
        Else
            'MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessCancel, Me)
        End If

    End Sub

    Private Sub PopulateGridDetl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRLog_WebSelf_OB", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdDetl_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdDetl.CommandButtonInitialize
        If e.ButtonType <> ColumnCommandButtonType.SelectCheckbox Then
            Return
        End If
        Dim rowEnabled As Boolean = getRowEnabledStatus(e.VisibleIndex)
        e.Enabled = rowEnabled
    End Sub
    Private Function getRowEnabledStatus(ByVal VisibleIndex As Integer) As Boolean
        Dim value As Boolean = Generic.ToInt(grdDetl.GetRowValues(VisibleIndex, "IsEnabledCancel"))
        If value = True Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Sub lnkCancel_Click(sender As Object, e As EventArgs)
        Try

            Dim tno As Integer = 0, i As Integer = 0
            For j As Integer = 0 To grdDetl.VisibleRowCount - 1
                If grdDetl.Selection.IsRowSelected(j) Then
                    i = i + 1
                End If
            Next

            If i > 0 Then
                Generic.ClearControls(Me, "pnlPopupCancel")
                mdlCancel.Show()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        ViewState("TransNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRLogMainNo"}))
        lbl.Text = Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"Code"}))
        PopulateGridDetl()
    End Sub


    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender

        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRLogMainNo"}))
        ViewState("DTRLogMainNo") = obj

        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, Generic.ToInt(ViewState("DTRLogMainNo"))))

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=666&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

End Class


