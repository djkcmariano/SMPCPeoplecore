Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_SecMenuManager
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EMenuManager_Web", UserNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateGridDetl()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EMenuMass_Web", PayLocNo, 2)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        PopulateGridDetl()
        Generic.PopulateDXGridFilter(grdDetl, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

#Region "********Main*******"

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"MenuUserSelfExcludeNo", "UserName"})
        ViewState("TransNo") = obj(0)
        ViewState("TransCode") = obj(1)

        lblDetl1.Text = "Managerial Module : " & ViewState("TransCode")
        PopulateGridDetl()


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
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"MenuUserSelfExcludeNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("SMenuUserSelfExcludeDetl", UserNo, "MenuUserSelfExcludeNo", CType(item, Integer))
                Generic.DeleteRecordAudit("SMenuUserSelfExclude", UserNo, CType(item, Integer))

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

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton, i As Integer
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MenuUserSelfExcludeNo"}))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EMenuManager_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                Next
                mdlMain.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    'Submit record
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtMenuUserSelfExcludeNo.Text)
            Dim xUserNo As Integer = Generic.ToInt(Me.hifuserno.Value)

            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataTable("EMenuManager_WebValidate", UserNo, tno, xUserNo, PayLocNo)
            For Each row As DataRow In dt.Rows
                invalid = Generic.ToBol(row("Invalid"))
                messagedialog = Generic.ToStr(row("MessageDialog"))
                alerttype = Generic.ToStr(row("AlertType"))
            Next

            If invalid = True Then
                mdlMain.Show()
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If

            If SQLHelper.ExecuteNonQuery("EMenuManager_WebSave", UserNo, tno, xUserNo, PayLocNo) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

#End Region


#Region "********Detail********"

    Protected Sub lnkViewMod_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If Generic.ToInt(ViewState("TransNo")) > 0 Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"MenuMassNo", "ModuleTitle"})
            Dim MenuMassNo As Integer = Generic.ToInt(obj(0))
            Dim MenuMassDesc As String = Generic.ToStr(obj(1))
            Dim MenuUserNo As Integer = Generic.ToInt(ViewState("TransNo"))
            Dim MenuUserDesc As String = Generic.ToStr(ViewState("TransCode"))
            MenuMassDesc = MenuMassDesc & " Permission"

            Response.Redirect("SecMenuManager_Module.aspx?MenuUserNo=" & MenuUserNo & "&MenuUserDesc=" & MenuUserDesc & "&MenuMassNo=" & MenuMassNo & "&MenuMassDesc=" & MenuMassDesc)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

#End Region

End Class

