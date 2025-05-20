Imports System.Data
Imports System.Math
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web

Partial Class Secured_BSCompTypeList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0

#Region "Main"

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("BBSCompType_Web", UserNo, "", PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()

            PopulateGridDetl()

        Catch ex As Exception
        End Try

    End Sub

    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("BBSCompType_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupmain", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSCompTypeNo"})))
            mdlPopupmain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BSCompTypeNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("BBSCompType", UserNo, item)
                Generic.DeleteRecordAuditCol("BBSCompTypeDeti", UserNo, "BSCompTypeNo", item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            mdlPopupmain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecord() As Boolean
        
        Dim bscomptypecode As String = Generic.ToStr(Me.txtBSCompTypeCode.Text)
        Dim bscomptypedesc As String = Generic.ToStr(Me.txtBSCompTypeCode.Text)
        Dim entrycode As String = Generic.ToStr(Me.txtEntryCode.Text)

        If SQLHelper.ExecuteNonQuery("BBSCompType_WebSave", UserNo, Generic.ToInt(txtCode.Text), bscomptypecode, bscomptypedesc, entrycode, PayLocNo) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If
    End Function


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupmain", PayLocNo)
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
       
    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSCompTypeNo", "Code"})
        ViewState("TransNo") = obj(0)
        lbl.Text = "Transaction No. : " & obj(1)
        PopulateGridDetl()
    End Sub

#End Region

#Region "Detail"

    Private Sub PopulateGridDetl()
        If Generic.ToInt(ViewState("TransNo")) = 0 Then

        End If
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("BBSCompTypeDeti_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            mdlPopupDetl.Show()
        End If
    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"BSCompTypeDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("BBSCompTypeDeti", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridDetl()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataDetl(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSCompTypeDetiNo"})))
            mdlPopupDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkSaveDetl_Click(sender As Object, e As EventArgs)
        If SaveRecordDetl() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecordDetl() As Boolean
        If Generic.ToInt(cboPayIncomeTypeNo.SelectedValue) = 0 And Generic.ToInt(cboPayDeductTypeNo.SelectedValue) = 0 Then
            SaveRecordDetl = False
        Else
            If SQLHelper.ExecuteNonQuery("BBSCompTypeDeti_WebSave", UserNo, Generic.ToInt(txtCodeDetl.Text), Generic.ToInt(ViewState("TransNo")), Generic.ToInt(cboPayIncomeTypeNo.SelectedValue), Generic.ToInt(cboPayDeductTypeNo.SelectedValue)) > 0 Then
                SaveRecordDetl = True
            Else
                SaveRecordDetl = False
            End If
        End If
    End Function

   

    Private Sub PopulateDataDetl(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("BBSCompTypeDeti_WebOne", UserNo, id)
        Generic.PopulateData(Me, "pnlPopupDetl", dt)
    End Sub


#End Region


End Class



