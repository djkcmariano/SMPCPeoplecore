Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_BenFleet
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0

    Protected Sub PopulateGrid()

        Try
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EFleet_Web", UserNo, "", PayLocNo)
            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EFleet_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                fleetmodel_lookup(Generic.ToInt(row("FleetBrandNo")))
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))

        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub
    Protected Sub cboFleetBrand_Change(sender As Object, e As EventArgs)
        Try
            fleetmodel_lookup(Generic.ToInt(cboFleetBrandNo.SelectedValue))
            ModalPopupExtender1.Show()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fleetmodel_lookup(brandNo As Integer)
        Try
            Dim modelno As Integer = Generic.ToInt(cboFleetModelNo.Text)

            cboFleetModelNo.DataSource = SQLHelper.ExecuteDataSet("EFleetModel_WebLookup", UserNo, brandNo)
            cboFleetModelNo.DataValueField = "tNo"
            cboFleetModelNo.DataTextField = "tdesc"
            cboFleetModelNo.DataBind()



        Catch ex As Exception

        End Try
        
    End Sub
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender

            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)

            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"FleetNo"})))

            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"FleetNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EFleet", UserNo, item)
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

    Private Function SaveRecord() As Boolean

        If SQLHelper.ExecuteNonQuery("EFleet_WebSave", UserNo, Generic.ToInt(txtCode.Text), Generic.ToInt(cboFleetBrandNo.SelectedValue), Generic.ToInt(cboFleetMakeNo.SelectedValue), _
                                     Generic.ToStr(txtEngineNo.Text), Generic.ToStr(txtPlateNo.Text), Generic.ToInt(cboFleetModelNo.SelectedValue), Generic.ToInt(txtYearModel.Text)) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function


End Class


