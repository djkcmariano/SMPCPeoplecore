Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web



Partial Class Secured_DTRDayTypeList
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDayTypePayClass_Web", UserNo, "", PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayClassNo", "Code"})
        ViewState("TransNo") = obj(0)
        lbl.Text = "Reference No. : " & obj(1)
        PopulateGridDetl()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateGridDetl()
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

#Region "Detail"

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDayType_WebOne", UserNo, id)
        Generic.PopulateData(Me, "pnlPopupDetl", dt)
        mdlShowDetl.Show()
    End Sub

    Private Sub PopulateGridDetl()
        Try
            Me.grdDetl.DataSource = SQLHelper.ExecuteDataSet("EDayType_Web", UserNo, Generic.ToInt(ViewState("TransNo")))
            Me.grdDetl.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DayTypeNo"})))
            mdlShowDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            mdlShowDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"DayTypeNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EDayType", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridDetl()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub btnSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDetl()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecord() As Integer
        If SQLHelper.ExecuteNonQuery("EDayType_WebSave", UserNo, Generic.ToInt(txtDayTypeNo.Text), Me.txtDayTypeCode.Text.ToString, Me.txtDayTypeDesc.Text.ToString, Generic.ToDec(Me.txtOvt.Text), Generic.ToDec(Me.txtOVt8.Text), Generic.ToDec(Me.txtNP.Text), Generic.ToDec(Me.txtNPOvt.Text), Generic.ToDec(Me.txtNPOvt8.Text), Generic.ToInt(ViewState("TransNo"))) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If
    End Function

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkGenerate_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then
            SQLHelper.ExecuteNonQuery("EDayType_WebGenerate", UserNo, Generic.ToInt(ViewState("TransNo")))
            MessageBox.Success(MessageTemplate.SuccessProcess, Me)
            PopulateGridDetl()
        Else
            MessageBox.Warning(MessageTemplate.DeniedProcess, Me)
        End If
    End Sub


#End Region

End Class


