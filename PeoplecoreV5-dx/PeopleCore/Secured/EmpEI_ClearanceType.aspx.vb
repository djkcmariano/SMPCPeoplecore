Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Partial Class Secured_EmpEI_ClearanceType
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim tStatus As Integer = Generic.ToInt(cboTabNo.SelectedValue)
        If tStatus = 0 Then
            lnkDelete.Visible = False
            lnkArchive.Visible = True
        ElseIf tStatus = 1 Then
            lnkDelete.Visible = False
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        Else
            lnkDelete.Visible = False
            lnkArchive.Visible = False
        End If
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearanceType_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

    End Sub


    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 14)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("onlineuserno"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        AccessRights.CheckUser(UserNo)

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        If Not IsPostBack Then
            PopulateDropDown()
        End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkArchive_Click(sender As Object, e As EventArgs)

        Dim dt As DataTable, tProceed As Boolean = False
        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "EmployeeEIClearanceTypeNo"))
                dt = SQLHelper.ExecuteDataTable("ETableReferrence_WebArchived", UserNo, "EEmployeeEIClearanceType", item, 1, PayLocNo)
                For Each row As DataRow In dt.Rows
                    tProceed = Generic.ToBol(row("tProceed"))
                Next
                grdMain.Selection.UnselectRow(j)
                i = i + 1
            End If
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") transaction(s) successfully archived.", Me)
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If


    End Sub




    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PositionNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeEIClearanceType", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid(True)
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
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton ', i As Integer
                lnk = sender
                'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                'i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PositionNo"}))
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeEIClearanceTypeNo", "IsEnabled"})
                Dim iNo As Integer = Generic.ToInt(obj(0))
                Dim IsEnabled As Boolean = Generic.ToBol(obj(1))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearanceType_WebOne", UserNo, Generic.ToInt(iNo))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
                    Generic.PopulateData(Me, "pnlPopup", dt)
                Next
                btnSave.Enabled = IsEnabled
                Try
                    cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                    cboPayLocNo.DataTextField = "tdesc"
                    cboPayLocNo.DataValueField = "tNo"
                    cboPayLocNo.DataBind()

                Catch ex As Exception

                End Try
                mdlShow.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub



    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopup")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try
            btnSave.Enabled = True
            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub


    'Submit record
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ds As New DataSet
        Dim RetVal As Integer = 0, xMessage As String = "", alertType As String = ""

        SaveRecord()

    End Sub


    Private Sub SaveRecord()

        Dim tno As Integer = Generic.ToInt(txtCode.Text)
        Dim tcode As String = Generic.ToStr(txtEmployeeEIClearanceTypeCode.Text)
        Dim tdesc As String = Generic.ToStr(txtEmployeeEIClearanceTypeDesc.Text)
        Dim cateno As Integer = Generic.ToInt(cboEmployeeEIClearanceCateNo.SelectedValue)
        Dim deptheadno As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim inchargeno As Integer = Generic.ToInt(hifEmployeeNo2.Value)
        Dim isArchived As Boolean = Generic.ToBol(chkIsArchived.Checked)

        Dim dt As New DataTable, error_num As Integer = 0, error_message As String = "", retVal As Boolean = False
        dt = SQLHelper.ExecuteDataTable("EEmployeeEIClearanceType_WebSave", UserNo, tno, tcode, tdesc, cateno, isArchived, PayLocNo, deptheadno, inchargeno)
        For Each row As DataRow In dt.Rows
            retVal = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                retVal = False
            End If

        Next
        If retVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If retVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub


    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid(True)
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

End Class


