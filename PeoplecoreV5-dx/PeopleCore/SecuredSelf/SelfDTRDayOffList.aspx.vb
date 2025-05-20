Imports System.Data
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web
Imports System.Net
Imports System.Web.Script.Serialization
Imports System.IO


Partial Class SecuredSelf_SelfDTRDayOffList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0
    Dim OnlineEmpNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRDayOff_WebSelf", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTRDayOff_WebOne", UserNo, id)
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
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim DTRDayOffNo As Integer = Generic.ToInt(txtDTRDayoffNo.Text)
        Dim EmployeeNo As Integer = OnlineEmpNo 'Generic.ToInt(hifEmployeeNo.Value)
        Dim DayOffNo As Integer = Generic.ToInt(cboDayOffNo.SelectedValue)
        Dim DayOffNo2 As Integer = Generic.ToInt(cboDayOffNo2.SelectedValue)
        Dim DayOffNo3 As Integer = Generic.ToInt(cboDayOffNo3.SelectedValue)
        Dim DayOffNo4 As Integer = Generic.ToInt(cboDayOffNo4.SelectedValue)
        Dim DayOffNo5 As Integer = Generic.ToInt(cboDayOffNo5.SelectedValue)
        Dim DayOffNo6 As Integer = Generic.ToInt(cboDayOffNo6.SelectedValue)
        Dim DayOffNo7 As Integer = Generic.ToInt(cboDayOffNo7.SelectedValue)
        Dim DateFrom As String = Generic.ToStr(txtDateFrom.Text)
        Dim DateTo As String = Generic.ToStr(txtDateTo.Text)
        Dim Reason As String = Generic.ToStr(txtReason.Text)
        Dim ApprovalStatNo As Integer = Generic.ToInt(cboApprovalStatNo.SelectedValue)
        Dim ComponentNo As Integer = 3 'Self Service

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim IdNo As String = "", SuperiorIdNo As Integer = 0, MobileNo As String = "", Message As String = ""

        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EDTRDayOff_WebValidate", UserNo, DTRDayOffNo, EmployeeNo, DateFrom, DateTo, DayOffNo, DayOffNo2, DayOffNo3, DayOffNo4, DayOffNo5, DayOffNo6, DayOffNo7, ApprovalStatNo, PayLocNo, ComponentNo)

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

        'If SQLHelper.ExecuteNonQuery("EDTRDayOff_WebSaveSelf", UserNo, DTRDayOffNo, EmployeeNo, DateFrom, DateTo, DayOffNo, DayOffNo2, DayOffNo3, Reason, PayLocNo) > 0 Then
        '    RetVal = True
        'Else
        '    RetVal = False
        'End If

        Dim dts As DataSet
        dts = SQLHelper.ExecuteDataSet("EDTRDayOff_WebSaveSelf", UserNo, DTRDayOffNo, EmployeeNo, DateFrom, DateTo, DayOffNo, DayOffNo2, DayOffNo3, DayOffNo4, DayOffNo5, DayOffNo6, DayOffNo7, Reason, PayLocNo)

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

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRDayOffNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Generic.EnableControls(Me, "pnlPopupDetl", IsEnabled)
            lnkSave.Enabled = IsEnabled
            mdlDetl.Show()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Try
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRDayOffNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EDTRDayoffDeti", UserNo, "DTRDayOffNo", item)
                Generic.DeleteRecordAudit("EDTRDayoff", UserNo, item)
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


End Class

