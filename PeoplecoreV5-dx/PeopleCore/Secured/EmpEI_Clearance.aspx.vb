Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO


Partial Class Secured_EmpEI_Clearance
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()

        End If

        PopulateGrid()
        PopulateGridDetl()

        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")

        Dim dt As DataTable

        If Generic.ToInt(cboTabNo.SelectedValue) = 2 Then
            lnkDeleteMain.Visible = True
            lnkPost.Visible = False
            lnkCleared.Visible = True
        ElseIf Generic.ToInt(cboTabNo.SelectedValue) = 3 Then
            lnkDeleteMain.Visible = True
            lnkPost.Visible = False
            lnkCleared.Visible = False

            lnkAdd.Visible = False
            lnkDelete.Visible = False
        Else
            lnkDeleteMain.Visible = True
            lnkPost.Visible = True
            lnkCleared.Visible = False
        End If

        dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearanceMain_Web", UserNo.ToString(), Generic.ToInt(cboTabNo.SelectedValue).ToString(), PayLocNo.ToString())
        grdMain.DataSource = dt
        grdMain.DataBind()
        PopulateGridDetl()

    End Sub

    Protected Sub lnkEditMain_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Generic.ClearControls(Me, "Panel2")
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataMain(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeEIClearanceMainNo"})))
            'ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkSaveMain_Click(sender As Object, e As EventArgs)

        If SaveRecordMain() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Private Function SaveRecordMain() As Integer
        Dim tno As Integer = Generic.ToInt(Me.txtEmployeeEIClearanceMainNo.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))

        If SQLHelper.ExecuteNonQuery("EEmployeeEIClearanceMain_WebSave", UserNo, tno, EmployeeNo, 0) > 0 Then
            SaveRecordMain = True
        Else
            SaveRecordMain = False
        End If

    End Function

    Protected Sub PopulateDataMain(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearanceMain_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel2", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAddMain_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel2")
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
            ModalPopupExtender2.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkDeleteMain_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeEIClearanceMainNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeEIClearanceMain", UserNo, item)
                Generic.DeleteRecordAuditCol("EEmployeeEIClearance", UserNo, "EmployeeEIClearanceMainNo", item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
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

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 51)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    'Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
    '    Dim lnk As New LinkButton
    '    lnk = sender
    '    Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
    '    Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo", "EmployeeCode"})
    '    ViewState("TransNo") = obj(0)
    '    ViewState("EmployeeCode") = obj(1)
    '    lblDetl.Text = ViewState("EmployeeCode").ToString
    '    PopulateGridDetl()

    'End Sub
    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Session("TransNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeEIClearanceMainNo"}))
        lblDetl.Text = "Transaction No. : " & Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"CodeMain"}))
        PopulateGridDetl()
    End Sub
    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeEIClearanceMainNo"})
            Dim str As String = "", i As Integer = 0, dt As DataTable
            For Each item As Integer In fieldValues
                dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearanceMain_Post", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        End If


    End Sub
    Protected Sub lnkCleared_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeEIClearanceMainNo"})
            Dim str As String = "", i As Integer = 0, dt As DataTable
            For Each item As Integer In fieldValues
                dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearanceMain_Cleared", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessUpdate, Me)
                PopulateGrid()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        End If


    End Sub

#Region "Detail"
    'Protected Sub PopulateGridDetl()
    '    Try
    '        If Generic.ToInt(ViewState("TransNo")) = 0 Then
    '            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"EmployeeNo", "EmployeeCode"})
    '            ViewState("TransNo") = Generic.ToInt(obj(0))
    '            ViewState("EmployeeCode") = Generic.ToStr(obj(1))

    '        End If
    '        Dim dt As DataTable
    '        dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearance_Web", UserNo, ViewState("TransNo"))
    '        grdDetl.DataSource = dt
    '        grdDetl.DataBind()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub PopulateGridDetl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearance_Web", UserNo, Generic.ToInt(Session("TransNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            Dim FocalNo As Integer
            dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearance_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
                Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, PayLocNo)
                FocalNo = Generic.ToInt(row("FUserNo"))
            Next

            If UserNo = FocalNo Then
                txtIsCleared.Enabled = False
            End If

        Catch ex As Exception

        End Try



    End Sub

    Protected Sub chkFc_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtFcDateReturned.Enabled = "True"
        ModalPopupExtender1.Show()
    End Sub
    Protected Sub chkDH_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtDateReturned.Enabled = "True"
        ModalPopupExtender1.Show()
    End Sub



    Private Sub EnabledControls()
        Dim Enabled As Boolean = True
        Generic.EnableControls(Me, "Panel1", Enabled)
        lnkAdd.Visible = Enabled
        lnkDelete.Visible = Enabled
        lnkSave.Visible = Enabled
    End Sub
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            txtDateReturned.Enabled = "False"
            txtFcDateReturned.Enabled = "False"
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        '//->comment
        'If SaveRecord() Then
        '    MessageBox.Success(MessageTemplate.SuccessSave, Me)
        '    PopulateGridDetl()
        'Else
        '    MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        'End If
        '//->end

        Dim Retval As Boolean = False

        Dim tno As Integer = Generic.ToInt(Me.txtCode.Text)
        Dim EmployeeClearanceTypeNo As Integer = Generic.ToInt(Me.cboEmployeeEIClearanceTypeNo.SelectedValue)
        'Dim EmployeeNo As Integer = Generic.ToInt(Session("TransNo"))
        Dim ImmediatesuperiorNo As Integer = Generic.ToInt(Generic.Split(hifImmediateSuperiorNo.Value, 0))
        Dim inchargeno As Integer = Generic.ToInt(Generic.Split(hifInChargeNo.Value, 0))
        Dim remarks As String = Generic.ToStr(txtRemarks.Text)
        Dim DateReturned As String = Generic.ToStr(txtDateReturned.Text)
        Dim IsCleared As Boolean = Generic.ToBol(txtIsCleared.Checked)
        Dim Amount As Double = Generic.ToDec(txtAmount.Text)
        Dim EmployeeEIClearanceMainNo As Integer = Generic.ToInt(Session("TransNo"))
        Dim IsReturned As Boolean = Generic.ToBol(txtIsReturned.Checked)
        Dim FCDateReturned As String = Generic.ToStr(txtDateReturned.Text)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EEmployeeEIClearance_WebValidate", UserNo, PayLocNo, ImmediatesuperiorNo, tno, EmployeeEIClearanceMainNo, IsCleared)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If


        'If SQLHelper.ExecuteNonQuery("EEmployeeEIClearance_WebSave", UserNo, tno, 0, remarks, EmployeeClearanceTypeNo, EmployeeNo, ImmediatesuperiorNo, DateReturned, IsCleared, inchargeno, Amount) > 0 Then
        If SQLHelper.ExecuteNonQuery("EEmployeeEIClearance_WebSave", UserNo, tno, EmployeeEIClearanceMainNo, 0, remarks, EmployeeClearanceTypeNo, 0, ImmediatesuperiorNo, DateReturned, IsCleared, inchargeno, Amount, IsReturned) > 0 Then
            '    SaveRecord = True
            'Else
            '    SaveRecord = False
            'End If
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Generic.ClearControls(Me, "Panel1")
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)

            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
            Generic.EnableControls(Me, "Panel1", IsEnabled)

            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeEIClearanceNo"})))
            txtDateReturned.Enabled = "False"
            txtFcDateReturned.Enabled = "False"
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"EmployeeEIClearanceNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeEIClearance", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
                PopulateGridDetl()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    '//->comment
    'Private Function SaveRecord() As Integer
    '    Dim tno As Integer = Generic.ToInt(Me.txtEmployeeEIClearanceNo.Text)
    '    Dim EmployeeClearanceTypeNo As Integer = Generic.ToInt(Me.cboEmployeeEIClearanceTypeNo.SelectedValue)
    '    'Dim EmployeeNo As Integer = Generic.ToInt(Session("TransNo"))
    '    Dim ImmediatesuperiorNo As Integer = Generic.ToInt(Generic.Split(hifImmediateSuperiorNo.Value, 0))
    '    Dim inchargeno As Integer = Generic.ToInt(Generic.Split(hifInChargeNo.Value, 0))
    '    Dim remarks As String = Generic.ToStr(txtRemarks.Text)
    '    Dim DateReturned As String = Generic.ToStr(txtDateReturned.Text)
    '    Dim IsCleared As Boolean = Generic.ToBol(txtIsCleared.Checked)
    '    Dim Amount As Double = Generic.ToDec(txtAmount.Text)
    '    Dim EmployeeEIClearanceMainNo As Integer = Generic.ToInt(Session("TransNo"))
    '    Dim IsReturned As Boolean = Generic.ToBol(txtIsReturned.Checked)

    '    'If SQLHelper.ExecuteNonQuery("EEmployeeEIClearance_WebSave", UserNo, tno, 0, remarks, EmployeeClearanceTypeNo, EmployeeNo, ImmediatesuperiorNo, DateReturned, IsCleared, inchargeno, Amount) > 0 Then
    '    If SQLHelper.ExecuteNonQuery("EEmployeeEIClearance_WebSave", UserNo, tno, EmployeeEIClearanceMainNo, 0, remarks, EmployeeClearanceTypeNo, 0, ImmediatesuperiorNo, DateReturned, IsCleared, inchargeno, Amount, IsReturned) > 0 Then
    '        SaveRecord = True
    '    Else
    '        SaveRecord = False
    '    End If

    'End Function
    '//->end

    Protected Sub lnkGenerate_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            SQLHelper.ExecuteNonQuery("EEmployeeEIClearance_WebGenerate", UserNo, Generic.ToInt(Session("TransNo")))
            MessageBox.Success(MessageTemplate.SuccessProcess, Me)
            PopulateGridDetl()
        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If
    End Sub

#End Region


    Protected Sub lnkPrint_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo"}))

        'Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, "0"), _
        '                                          New ReportParameter(ReportParameter.Type.int, "1"), _
        '                                          New ReportParameter(ReportParameter.Type.int, id), _
        '                                          New ReportParameter(ReportParameter.Type.str, ""), _
        '                                          New ReportParameter(ReportParameter.Type.str, ""), _
        '                                          New ReportParameter(ReportParameter.Type.int, "0")
        '                                          )

        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, "1"), _
                                                  New ReportParameter(ReportParameter.Type.int, id), _
                                                  New ReportParameter(ReportParameter.Type.str, ""), _
                                                  New ReportParameter(ReportParameter.Type.str, "")
                                                  )

        sb.Append("<script>")
        'sb.Append("window.open('rpttemplateviewer.aspx?reportno=454&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=696&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub
    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub
    'Private Function getRowEnabledStatus(ByVal VisibleIndex As Integer) As Boolean
    '    Dim value As Boolean = Generic.ToInt(grdDetl.GetRowValues(VisibleIndex, "IsEnabled"))
    '    If value = True Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function
End Class
