Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_EmpRequestTypeList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("ERequestType_Web", UserNo, "", PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupRate", PayLocNo)
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
   
     Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"RequestTypeNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ERequestType", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)


        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"RequestTypeNo"}))
                Generic.ClearControls(Me, "pnlPopupRate")

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("ERequestType_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupRate", dt)
                Next
                mdlRate.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupRate")
            mdlRate.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    'Submit record
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        SaveRecord
    End Sub

    Private Sub SaveRecord()
        Dim rep1 As Integer = Generic.Split(hifRecepientNo.Value, 0)
        Dim rep2 As Integer = IIf(hifRecepientNo1.Value <> "", Generic.Split(hifRecepientNo1.Value, 0), 0)
        Dim rep3 As Integer = IIf(hifRecepientNo2.Value <> "", Generic.Split(hifRecepientNo2.Value, 0), 0)
        Dim emailbody As String = Generic.ToStr(txtemailbody.Text)

        Dim retVal As Boolean = False, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("ERequestType_WebSave", UserNo, Generic.ToInt(txtRequestTypeNo.Text), Generic.ToStr(txtRequestTypeCode.Text), Generic.ToStr(txtRequestTypeDesc.Text),
                                        rep1, rep2, rep3, PayLocNo, emailbody)
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
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
End Class






