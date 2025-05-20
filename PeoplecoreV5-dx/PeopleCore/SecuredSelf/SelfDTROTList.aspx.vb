Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports System.Net
Imports System.Web.Script.Serialization

Partial Class SecuredSelf_SelfDTROTList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0
    Dim OnlineEmpNo As Integer = 0

    Protected Sub PopulateGrid()
        Dim tStatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)

        If tStatus = 1 Then
            lnkDelete.Visible = True
            lnkCancel.Visible = False
        ElseIf tStatus = 2 Then
            lnkCancel.Visible = True
            lnkDelete.Visible = False
        Else
            lnkDelete.Visible = False
            lnkCancel.Visible = False
        End If

        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTROT_WebSelf", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try

        'If Generic.ToInt(cboOvertimeReasonNo.SelectedValue) > 0 Then
        '    txtReason.Text = ""
        'End If

    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTROT_WebOne", UserNo, id)
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


    Protected Function OT_Msg(ByRef Msg As String, ByVal Index As Short) As String
        Dim xds As New DataSet
        xds = SQLHelper.ExecuteDataSet("[EDTROT_WebOne_Msg]", Index)
        If xds.Tables.Count > 0 Then
            Msg = Generic.ToStr(xds.Tables(0).Rows(0)("xMsg"))
        End If
        Return Msg
    End Function

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Try
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.EnableControls(Me, "pnlPopupDetl", True)
            txtReason.Text = ""
            Try
                cboCostCenterNo.DataSource = SQLHelper.ExecuteDataSet("EDTRShift_WebLookup_ChargeTo", UserNo, PayLocNo)
                cboCostCenterNo.DataTextField = "tDesc"
                cboCostCenterNo.DataValueField = "tno"
                cboCostCenterNo.DataBind()
            Catch ex As Exception

            End Try

            Try
                cboOvertimeReasonNo.DataSource = SQLHelper.ExecuteDataSet("EOT_WebLookup", UserNo, PayLocNo)
                cboOvertimeReasonNo.DataTextField = "tDesc"
                cboOvertimeReasonNo.DataValueField = "tno"
                cboOvertimeReasonNo.DataBind()
            Catch ex As Exception

            End Try

            lnkSave.Enabled = True

            Dim Msg As String = ""
            Me.lblMsgNotice.Text = OT_Msg(Msg, 1)

            mdlDetl.Show()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTROTNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Dim OTReasonNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"OvertimeReasonNo"}))
            Generic.EnableControls(Me, "pnlPopupDetl", IsEnabled)
            Try
                cboCostCenterNo.DataSource = SQLHelper.ExecuteDataSet("EDTRShift_WebLookup_ChargeTo", UserNo, PayLocNo)
                cboCostCenterNo.DataTextField = "tDesc"
                cboCostCenterNo.DataValueField = "tno"
                cboCostCenterNo.DataBind()
            Catch ex As Exception

            End Try

            Try
                cboOvertimeReasonNo.DataSource = SQLHelper.ExecuteDataSet("EOT_WebLookup", UserNo, PayLocNo)
                cboOvertimeReasonNo.DataTextField = "tDesc"
                cboOvertimeReasonNo.DataValueField = "tno"
                cboOvertimeReasonNo.DataBind()
            Catch ex As Exception

            End Try

            'If OTReasonNo > 0 Then
            '    txtReason.Enabled = True
            'End If

            Dim Msg As String = ""
            Me.lblMsgNotice.Text = OT_Msg(Msg, 1)

            lnkSave.Enabled = IsEnabled
            mdlDetl.Show()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTROTNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTROT", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub cboOvertimeReasonNo_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateRefresh()
        mdlDetl.Show()
    End Sub


    Protected Sub PopulateRefresh()

        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EOvertimeReason_WebOne", UserNo, cboOvertimeReasonNo.SelectedValue)
            If Generic.ToInt(cboOvertimeReasonNo.SelectedValue) = 0 Then
                txtReason.Enabled = True
                txtReason.CssClass = "form-control required"
                txtReason.Text = ""
                cboOvertimeReasonNo.CssClass = "form-control"
            ElseIf Generic.ToInt(cboOvertimeReasonNo.SelectedValue) > 0 Then
                txtReason.Enabled = True
                txtReason.CssClass = "form-control"
                txtReason.Text = Generic.ToStr(dt.Rows(0)("OvertimeReasonDesc"))
                cboOvertimeReasonNo.CssClass = "form-control required"
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As EventArgs)

        'Dim d As New DataTable
        'd = SQLHelper.ExecuteDataTable("EOvertimeReason_WebOne", UserNo, cboOvertimeReasonNo.SelectedValue)
        'For Each row As DataRow In d.Rows
        '    If Generic.ToInt(cboOvertimeReasonNo.SelectedValue) > 0 Then
        '        txtReason.Text = Generic.ToStr(row("OvertimeReasonDesc"))
        '    End If
        'Next

        Dim RetVal As Boolean = False
        Dim DTROTNo As Integer = Generic.ToInt(txtDTROTNo.Text)
        Dim EmployeeNo As Integer = OnlineEmpNo 'Generic.ToInt(hifEmployeeNo.Value)
        Dim DTRDate As String = Generic.ToStr(txtDTRDate.Text)
        Dim OvtIn1 As String = Generic.ToStr(Replace(txtOvtIn1.Text, ":", ""))
        Dim OvtOut1 As String = Generic.ToStr(Replace(txtOvtOut1.Text, ":", ""))
        Dim OvtIn2 As String = Generic.ToStr(Replace(txtOvtIn2.Text, ":", ""))
        Dim OvtOut2 As String = Generic.ToStr(Replace(txtOvtOut2.Text, ":", ""))
        Dim OTBreak As Double = Generic.ToDec(txtOTBreak.Text)
        'Dim IsForCompensatory As Boolean = Generic.ToBol(txtIsForcompensatory.Checked)
        'Dim IsOnCall As Boolean = Generic.ToBol(txtIsOncall.Checked)
        Dim Reason As String = Generic.ToStr(txtReason.Text)
        Dim CostCenterNo As Integer = Generic.ToInt(cboCostCenterNo.SelectedValue)
        Dim ApprovalStatNo As Integer = Generic.ToInt(cboApprovalStatNo.SelectedValue)
        Dim ComponentNo As Integer = 3 'Self Service

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim IdNo As String = "", SuperiorIdNo As Integer = 0, MobileNo As String = "", Message As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EDTROT_WebValidate", UserNo, DTROTNo, EmployeeNo, DTRDate, OvtIn1, OvtOut1, OvtIn2, OvtOut2, OTBreak, Reason, CostCenterNo, ApprovalStatNo, PayLocNo, ComponentNo)

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

        'If SQLHelper.ExecuteNonQuery("EDTROT_WebSaveSelf", UserNo, DTROTNo, EmployeeNo, DTRDate, OvtIn1, OvtOut1, OvtIn2, OvtOut2, OTBreak, Reason, CostCenterNo, PayLocNo, cboOvertimeReasonNo.SelectedValue) Then
        '    RetVal = True
        'Else
        '    RetVal = False
        'End If

        Dim dts As DataSet
        dts = SQLHelper.ExecuteDataSet("EDTROT_WebSaveSelf", UserNo, DTROTNo, EmployeeNo, DTRDate, OvtIn1, OvtOut1, OvtIn2, OvtOut2, OTBreak, Reason, CostCenterNo, PayLocNo, cboOvertimeReasonNo.SelectedValue)

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
            Dim url As String = "SelfDTROTList.aspx"
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If

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


    Protected Sub lnkCancel_Click(sender As Object, e As System.EventArgs)
        Try
            Generic.ClearControls(Me, "pnlPopupRemarks")
            Generic.EnableControls(Me, "pnlPopupRemarks", True)
            lnkSaveCancel.Enabled = True
            mdlRemarks.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSaveCancel_Click(sender As Object, e As System.EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTROTNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            CancelTransaction(item, txtRemarks.Text)
            i = i + 1
        Next

        If i > 0 Then
            PopulateGrid()
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessCancel, Me)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Private Sub CancelTransaction(tId As Integer, remarks As String)
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("EDTROT_WebCancel", UserNo, tId, remarks)
    End Sub

    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender

        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTROTNo"}))
        ViewState("DTROTNo") = obj

        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, Generic.ToInt(ViewState("DTROTNo"))))

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=668&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub


End Class

