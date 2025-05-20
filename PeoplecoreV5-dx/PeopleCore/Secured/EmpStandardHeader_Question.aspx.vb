Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Partial Class Secured_EmpStandardHeader_Question
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticatedCoreUser()

        If Not IsPostBack Then
            PopulateTabHeader()
            PopulateDropDown()
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
        End If
        PopulateGrid()
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicantStandardMain_WebHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboApplicantStandardCateNo.DataSource = SQLHelper.ExecuteDataTable("EApplicantStandardCate_WebLookup", TransNo)
            cboApplicantStandardCateNo.DataValueField = "tNo"
            cboApplicantStandardCateNo.DataTextField = "tDesc"
            cboApplicantStandardCateNo.DataBind()
        Catch ex As Exception

        End Try
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
    End Sub

#Region "Main"

    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EApplicantStandardDetl_Web", UserNo, TransNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        If Generic.ToInt(ViewState("TransNo")) = 0 Or IsMain = True Then
            Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"ApplicantStandardDetlNo", "Code"})
            ViewState("TransNo") = obj(0)
            lblDetl.Text = obj(1)
        End If

        PopulateGridDetl()
    End Sub


    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "EmpStandardHeader.aspx", "EApplicantStandardHeader") Then
        Generic.ClearControls(Me, "pnlPopupMain")
        mdlMain.Show()
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        'End If
    End Sub


    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpStandardHeader.aspx", "EApplicantStandardHeader") Then
            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantStandardDetlNo"}))

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EApplicantStandardDetl_WebOne", UserNo, Generic.ToInt(i))
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupMain", dt)
            Next
            mdlMain.Show()


            'Else
            'MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            'End If
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0

        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "EmpStandardHeader.aspx", "EApplicantStandardHeader") Then
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ApplicantStandardDetlNo"})
        Dim str As String = ""
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EApplicantStandardDetl", UserNo, item)
            Generic.DeleteRecordAuditCol("EApplicantStandardDetlChoice", UserNo, "ApplicantStandardDetlNo", item)
            DeleteCount = DeleteCount + 1
        Next

        If DeleteCount > 0 Then
            PopulateGrid(True)
            MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If
        'Else
        'MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)

        'End If
    End Sub


    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantStandardDetlNo", "Code"})
        ViewState("TransNo") = obj(0)
        lblDetl.Text = obj(1)
        PopulateGridDetl()
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Integer = SaveRecord()
        If RetVal > 0 Then
            If Generic.ToInt(Request.QueryString("id")) = 0 Then
                Dim url As String = "appstandardHeader_main.aspx?id=" & RetVal
                MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            Else
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            End If
            ViewState("QuestionNo") = RetVal
            PopulateTabHeader()
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecord() As Integer
        Dim RetVal As Integer = 0
        Dim dt As DataTable
        Try
            dt = SQLHelper.ExecuteDataTable("EApplicantStandardDetl_WebSave", UserNo, Generic.ToInt(txtCode.Text), TransNo, Generic.ToInt(cboApplicantStandardCateNo.SelectedValue), txtQuestion.Text, chkHasComment.Checked, chkIsRequired.Checked, Generic.ToInt(cboResponseTypeNo.SelectedValue), Generic.ToInt(txtOrderBy.Text), PayLocNo)
            For Each row As DataRow In dt.Rows
                RetVal = Generic.ToInt(row("RetVal"))
            Next
        Catch ex As Exception

        End Try
        Return RetVal
    End Function

    Private Sub PopulateData(ID As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicantStandardDetl_WebOne", UserNo, ID)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next

    End Sub

#End Region

#Region "Detail"

    Private Sub PopulateGridDetl()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicantStandardDetlChoice_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdDetail.DataSource = dt
        grdDetail.DataBind()
    End Sub


    Protected Sub lnkAddDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "EmpStandardHeader.aspx", "EApplicantStandardHeader") Then
        If Generic.ToInt(ViewState("TransNo")) > 0 Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        'End If
    End Sub


    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpStandardHeader.aspx", "EApplicantStandardHeader") Then
            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantStandardDetlChoiceNo"}))

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EApplicantStandardDetlChoice_WebOne", UserNo, Generic.ToInt(i))
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
            mdlDetl.Show()

            'Else
            'MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            'End If

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkDeleteDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0

        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "EmpStandardHeader.aspx", "EApplicantStandardHeader") Then
        Dim fieldValues As List(Of Object) = grdDetail.GetSelectedFieldValues(New String() {"ApplicantStandardDetlChoiceNo"})
        Dim str As String = ""
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EApplicantStandardDetlChoice", UserNo, item)
            DeleteCount = DeleteCount + 1
        Next

        If DeleteCount > 0 Then
            PopulateGridDetl()
            MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If
        'Else
        'MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)
        'End If

    End Sub


    Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
        If SaveRecordDetl() Then
            PopulateGridDetl()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecordDetl() As Boolean

        If SQLHelper.ExecuteNonQuery("EApplicantStandardDetlChoice_WebSave", UserNo, Generic.ToInt(txtCodeDetl.Text), txtApplicantStandardDetlChoiceDesc.Text, Generic.ToInt(ViewState("TransNo")), TransNo, Generic.ToInt(txtOrderByDetl.Text), PayLocNo) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub PopulateDataDetl(ID As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicantStandardDetlChoice_WebOne", UserNo, ID)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel2", dt)
        Next
    End Sub
    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub
    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

#End Region

End Class


