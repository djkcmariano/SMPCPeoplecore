Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports System.Net
Imports System.Web.Script.Serialization

Partial Class SecuredSelf_SelfDTRShiftList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0
    Dim OnlineEmpNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRShift_WebSelf", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRShift_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
                cboShiftNo.Text = Generic.ToStr(row("ShiftNo"))
                txtIn1.Text = Generic.ToStr(row("In1"))
                txtOut1.Text = Generic.ToStr(row("Out1"))
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
        'PopulateDropdown_Trans()
    End Sub
    'Private Sub PopulateDropdown_Trans()
    '    Try
    '        cboShiftNo.DataSource = SQLHelper.ExecuteDataSet("Select '' as tNo,'-- Select --' As tDesc Union Select Convert(Varchar(20),ShiftLNo) as tNo,ShiftLDesc as tDesc From dbo.EShiftL Where PayLocNo=" & PayLocNo)
    '        cboShiftNo.DataValueField = "tNo"
    '        cboShiftNo.DataTextField = "tDesc"
    '        cboShiftNo.DataBind()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Function IsCrew() As Boolean
        Dim retVal As Boolean = False
        Dim ds As DataSet = SQLHelper.ExecuteDataSet("Select IsCrew From dbo.EEmployee A Inner Join (Select PayclassNo,IsCrew From dbo.EPayClass) B On a.payclassNo=B.PayclassNo Where A.EmployeeNo=" & OnlineEmpNo)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                retVal = Generic.ToBol(ds.Tables(0).Rows(0)("IsCrew"))
            End If
        End If
        If retVal Then
            'divShift.Visible = False
            'cboShiftNo.CssClass = "form-control"
            'divTime.Visible = True
            'txtIn1.CssClass = "form-control required"
            'txtOut1.CssClass = "form-control required"
        Else
            'divShift.Visible = True
            'cboShiftNo.CssClass = "form-control required"
            'divTime.Visible = False
            'txtIn1.CssClass = "form-control"
            'txtOut1.CssClass = "form-control"
        End If
        If retVal Then
            fRegisterStartupScript("JSDialogResponse", "ViewCrew('True');")
            txtIn1.CssClass = "form-control required"
            txtOut1.CssClass = "form-control required"
            cboShiftNo.CssClass = "form-control"
        Else
            fRegisterStartupScript("JSDialogResponse", "ViewCrew('False');")
            txtIn1.CssClass = "form-control"
            txtOut1.CssClass = "form-control"
            cboShiftNo.CssClass = "form-control required"
        End If
        Return retVal
    End Function
    Private Sub disableshift_perday()
        cboShiftNoMon.Enabled = False
        cboShiftNoTue.Enabled = False
        cboShiftNoWed.Enabled = False
        cboShiftNoThu.Enabled = False
        cboShiftNoFri.Enabled = False
        cboShiftNoSat.Enabled = False
        cboShiftNoSun.Enabled = False

    End Sub
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Try
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.EnableControls(Me, "pnlPopupDetl", True)
            disableshift_perday()
            lnkSave.Enabled = True
            Try
                cboCostCenterNo.DataSource = SQLHelper.ExecuteDataSet("EDTRShift_WebLookup_ChargeTo", UserNo, PayLocNo)
                cboCostCenterNo.DataTextField = "tDesc"
                cboCostCenterNo.DataValueField = "tno"
                cboCostCenterNo.DataBind()
            Catch ex As Exception

            End Try
            'IsCrew()
            mdlDetl.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRShiftNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Generic.EnableControls(Me, "pnlPopupDetl", IsEnabled)
            disableshift_perday()
            Try
                cboCostCenterNo.DataSource = SQLHelper.ExecuteDataSet("EDTRShift_WebLookup_ChargeTo", UserNo, PayLocNo)
                cboCostCenterNo.DataTextField = "tDesc"
                cboCostCenterNo.DataValueField = "tno"
                cboCostCenterNo.DataBind()
            Catch ex As Exception

            End Try
            lnkSave.Enabled = IsEnabled
            'IsCrew()
            mdlDetl.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRShiftNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDTRShift", UserNo, item)
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

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim RetVal As Boolean = False
        Dim DTRShiftNo As Integer = Generic.ToInt(txtDTRShiftNo.Text)
        Dim EmployeeNo As Integer = OnlineEmpNo 'Generic.ToInt(hifEmployeeNo.Value)
        Dim ShiftNo As Integer = Generic.ToInt(cboShiftNo.SelectedValue)
        Dim ShiftNoMon As Integer = Generic.ToInt(cboShiftNoMon.SelectedValue)
        Dim ShiftNoTue As Integer = Generic.ToInt(cboShiftNoTue.SelectedValue)
        Dim ShiftNoWed As Integer = Generic.ToInt(cboShiftNoWed.SelectedValue)
        Dim ShiftNoThu As Integer = Generic.ToInt(cboShiftNoThu.SelectedValue)
        Dim ShiftNoFri As Integer = Generic.ToInt(cboShiftNoFri.SelectedValue)
        Dim ShiftNoSat As Integer = Generic.ToInt(cboShiftNoSat.SelectedValue)
        Dim ShiftNoSun As Integer = Generic.ToInt(cboShiftNoSun.SelectedValue)
        Dim DateFrom As String = Generic.ToStr(txtDateFrom.Text)
        Dim DateTo As String = Generic.ToStr(txtDateTo.Text)
        Dim Reason As String = Generic.ToStr(txtReason.Text)
        Dim ApprovalStatNo As Integer = Generic.ToInt(cboApprovalStatNo.SelectedValue)
        Dim ComponentNo As Integer = 3 'Self Service
        Dim In1 As String = Replace(Generic.ToStr(txtIn1.Text), ":", "")
        Dim Out1 As String = Replace(Generic.ToStr(txtOut1.Text), ":", "")
        Dim costCenterNo As Integer = Generic.ToInt(cboCostCenterNo.SelectedValue)

        In1 = IIf(In1 Is Nothing, "", In1)
        Out1 = IIf(Out1 Is Nothing, "", Out1)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim IdNo As String = "", SuperiorIdNo As Integer = 0, MobileNo As String = "", Message As String = ""

        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EDTRShift_WebValidate", UserNo, DTRShiftNo, EmployeeNo, DateFrom, DateTo, ShiftNo, ApprovalStatNo, PayLocNo, ComponentNo)

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

        'If SQLHelper.ExecuteNonQuery("EDTRShift_WebSaveSelf", UserNo, DTRShiftNo, EmployeeNo, DateFrom, DateTo, ShiftNo, ShiftNoMon, ShiftNoTue, ShiftNoWed, ShiftNoThu, ShiftNoFri, ShiftNoSat, ShiftNoSun, Reason, PayLocNo, In1, Out1, costCenterNo) > 0 Then
        '    RetVal = True
        'Else
        '    RetVal = False
        'End If

        Dim dts As DataSet
        dts = SQLHelper.ExecuteDataSet("EDTRShift_WebSaveSelf", UserNo, DTRShiftNo, EmployeeNo, DateFrom, DateTo, ShiftNo, ShiftNoMon, ShiftNoTue, ShiftNoWed, ShiftNoThu, ShiftNoFri, ShiftNoSat, ShiftNoSun, Reason, PayLocNo, In1, Out1, costCenterNo)

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
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub cboShiftNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        If Generic.ToInt(cboShiftNo.SelectedValue) > 0 Then
            Dim xShiftNo As Integer = Generic.ToInt(cboShiftNo.SelectedValue)
            disableshift_perday()

            cboShiftNoMon.Text = xShiftNo
            cboShiftNoTue.Text = xShiftNo
            cboShiftNoWed.Text = xShiftNo
            cboShiftNoThu.Text = xShiftNo
            cboShiftNoFri.Text = xShiftNo
            cboShiftNoSat.Text = xShiftNo
            cboShiftNoSun.Text = xShiftNo
        Else
            Try
                cboShiftNoMon.Text = ""
                cboShiftNoTue.Text = ""
                cboShiftNoWed.Text = ""
                cboShiftNoThu.Text = ""
                cboShiftNoFri.Text = ""
                cboShiftNoSat.Text = ""
                cboShiftNoSun.Text = ""
            Catch ex As Exception

            End Try
        End If
        'IsCrew()
        mdlDetl.Show()
    End Sub
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
End Class









