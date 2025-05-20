Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports System.Net
Imports System.Web.Script.Serialization

Partial Class SecuredSelf_SelfDTRLogList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0
    Dim OnlineEmpNo As Integer = 0

    Protected Sub PopulateGrid()
        Dim tStatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
        If tStatus = 1 Then
            lnkDelete.Visible = True
        Else
            lnkDelete.Visible = False
        End If
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRLog_WebSelf", UserNo, Generic.ToInt(cboTabNo.SelectedValue), 0, PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRLog_WebOne", UserNo, id)
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

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Try
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.EnableControls(Me, "pnlPopupDetl", True)
            lnkSave.Enabled = True
            mdlDetl.Show()

            Try
                cboDTRLogReasonNo.DataSource = SQLHelper.ExecuteDataSet("EDTRLogReason_WebLookup", UserNo, PayLocNo)
                cboDTRLogReasonNo.DataTextField = "tDesc"
                cboDTRLogReasonNo.DataValueField = "tno"
                cboDTRLogReasonNo.DataBind()
            Catch ex As Exception

            End Try

            Try
                cboDTRBioLogNo.DataSource = SQLHelper.ExecuteDataSet("EDTRBioLog_WebLookup", UserNo, PayLocNo)
                cboDTRBioLogNo.DataTextField = "tDesc"
                cboDTRBioLogNo.DataValueField = "tno"
                cboDTRBioLogNo.DataBind()
            Catch ex As Exception

            End Try

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRLogNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Dim DTRLogReason As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRLogReasonNo"}))
            Generic.EnableControls(Me, "pnlPopupDetl", IsEnabled)
            lnkSave.Enabled = IsEnabled
            mdlDetl.Show()
        Catch ex As Exception
        End Try

        Try
            cboDTRLogReasonNo.DataSource = SQLHelper.ExecuteDataSet("EDTRLogReason_WebLookup", UserNo, PayLocNo)
            cboDTRLogReasonNo.DataTextField = "tDesc"
            cboDTRLogReasonNo.DataValueField = "tno"
            cboDTRLogReasonNo.DataBind()
        Catch ex As Exception

        End Try

        Try
            cboDTRBioLogNo.DataSource = SQLHelper.ExecuteDataSet("EDTRBioLog_WebLookup", UserNo, PayLocNo)
            cboDTRBioLogNo.DataTextField = "tDesc"
            cboDTRBioLogNo.DataValueField = "tno"
            cboDTRBioLogNo.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRLogNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTRLog", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
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

    Protected Sub cboDTRLogReasonNo_OnSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateRefresh()
        mdlDetl.Show()
    End Sub


    Protected Sub PopulateRefresh()

        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRLogReason_WebOne", UserNo, cboDTRLogReasonNo.SelectedValue)
            If Generic.ToInt(cboDTRLogReasonNo.SelectedValue) = 0 Then
                txtReason.Enabled = True
                txtReason.CssClass = "form-control required"
                txtReason.Text = ""
                cboDTRLogReasonNo.CssClass = "form-control"
            ElseIf Generic.ToInt(cboDTRLogReasonNo.SelectedValue) > 0 Then
                txtReason.Enabled = True
                txtReason.CssClass = "form-control"
                txtReason.Text = Generic.ToStr(dt.Rows(0)("DTRLogReasonDesc"))
                cboDTRLogReasonNo.CssClass = "form-control required"
            End If
        Catch ex As Exception
        End Try
    End Sub



    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim RetVal As Boolean = False
        Dim DTRLogNo As Integer = Generic.ToInt(txtDTRLogNo.Text)
        Dim EmployeeNo As Integer = OnlineEmpNo  'Generic.ToInt(hifEmployeeNo.Value)
        Dim DTRDate As String = Generic.ToStr(txtDTRDate.Text)
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
        dtx = SQLHelper.ExecuteDataTable("EDTRLog_WebValidate", UserNo, DTRLogNo, EmployeeNo, DTRDate, In1, Out1, In2, Out2, Reason, ApprovalStatNo, PayLocNo, 0, ComponentNo)

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

        'If SQLHelper.ExecuteNonQuery("EDTRLog_WebSaveSelf", UserNo, DTRLogNo, EmployeeNo, DTRDate, In1, Out1, In2, Out2, Reason, 0, PayLocNo) Then
        '    RetVal = True
        'Else
        '    RetVal = False
        'End If

        Dim iNo As Integer = 0, fullname As String = "", employeeCode As String = "", error_num As Integer = 0, error_message As String = ""


        Dim dts As DataSet
        dts = SQLHelper.ExecuteDataSet("EDTRLog_WebSaveSelf", UserNo, DTRLogNo, EmployeeNo, DTRDate, In1, Out1, In2, Out2, Reason, 0, PayLocNo, cboDTRLogReasonNo.SelectedValue, cboDTRBioLogNo.SelectedValue)
        ''
        If dts.Tables.Count > 0 Then
            If dts.Tables.Count > 0 Then

                IdNo = Generic.CheckDBNull(dts.Tables(1).Rows(0)("IdNo"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                SuperiorIdNo = Generic.CheckDBNull(dts.Tables(1).Rows(0)("SuperiorIdNo"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                MobileNo = Generic.CheckDBNull(dts.Tables(1).Rows(0)("MobileNo"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                Message = Generic.CheckDBNull(dts.Tables(1).Rows(0)("Message"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                'employeeCode = Generic.CheckDBNull(dts.Tables(1).Rows(0)("employeeCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)

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
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            'Dim url As String = "SelfDTRLogList_Deti.aspx?TransNo=" & iNo.ToString & "&FullName=" & fullname.ToString & "&employeecode=" & employeeCode.ToString & "&dtrdate=" & DTRDate.ToString
            'MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkViewLogs_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EDTRLog_WebViewLogs", OnlineEmpNo, Generic.ToStr(txtDTRDate.Text))

            For Each rowx As DataRow In dtx.Rows
                messagedialog = Generic.ToStr(rowx("Retval"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlDetl.Show()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj() As Object = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRLogNo", "DTRDate", "EmployeeCode", "FullName"})
        ViewState("TransNo") = obj(0).ToString
        ViewState("DTRDate") = obj(1).ToString
        ViewState("EmployeeCode") = obj(2).ToString
        ViewState("FullName") = obj(3).ToString

        lbl.Text = Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"Code"}))
        PopulateDetl()
    End Sub

#Region "Details"
    Protected Sub PopulateData_Detl(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRLogDetl_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopup", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkAddD_Click(sender As Object, e As EventArgs)
        If ViewState("TransNo") > 0 Then

            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", Generic.ToInt(Session("xPayLocNo")))
            Generic.ClearControls(Me, "pnlPopup")
            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub
    Protected Sub lnkEditD_Click(sender As Object, e As EventArgs)
        If ViewState("TransNo") > 0 Then

            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", Generic.ToInt(Session("xPayLocNo")))
            Dim obj() As Object = container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRLogNo", "DTRLogDetlNo"})

            ViewState("DTRLogDetlNo") = obj(1).ToString
            PopulateData_Detl(Generic.ToInt(ViewState("DTRLogDetlNo")))
            mdlShow.Show()

            'Response.Redirect("DTRLog_Deti.aspx?TransNo=" & obj(0).ToString & "&FullName=" & obj(3).ToString & "&employeecode=" & obj(2).ToString & "&DTRDate=" & obj(1).ToString)

        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub
    Protected Sub lnkDeleteD_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"DTRLogDetlNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EDTRLogDetl", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateDetl()

    End Sub
    Private Sub PopulateDetl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRLogDetl_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim ProjectNo As Integer = Generic.ToInt(cboProjectNo.SelectedValue)

        Dim Hrs As Double = Generic.ToDbl(txtHrs.Text)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Html)
        Dim In1 As String = Generic.ToStr(Replace(txtIn11.Text, ":", ""))
        Dim Out1 As String = Generic.ToStr(Replace(txtOut11.Text, ":", ""))
        Dim Task As String = Generic.ToStr(txtTask.Html)
        ''//validate start here
        'Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        'Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        'dtx = SQLHelper.ExecuteDataTable("EDTRLogDetl_WebValidate", UserNo, Generic.ToInt(txtDTRLogDetlNo.Text), DTRLogNo, DTRNo, DepartmentNo, CostCenterNo, Hrs, Remarks, PayLocNo)
        'For Each rowx As DataRow In dtx.Rows
        '    invalid = Generic.ToBol(rowx("tProceed"))
        '    messagedialog = Generic.ToStr(rowx("xMessage"))
        '    alerttype = Generic.ToStr(rowx("AlertType"))
        'Next

        'If invalid = True Then
        '    MessageBox.Alert(messagedialog, alerttype, Me)
        '    mdlShow.Show()
        '    Exit Sub
        'End If
        Dim dt As DataTable, error_num As Integer = 0, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("EDTRLogDetl_WebSave", UserNo, Generic.ToInt(txtDTRLogDetlNo.Text), ViewState("TransNo"), ProjectNo, Hrs, Remarks, In1, Out1, Task)
        For Each row As DataRow In dt.Rows
            Retval = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                Retval = False
            End If

        Next
        If Retval = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If Retval = True Then
            PopulateDetl()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub
#End Region

End Class


