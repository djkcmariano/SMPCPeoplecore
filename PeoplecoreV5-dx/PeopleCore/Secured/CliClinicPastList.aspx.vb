Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_CliClinicPastList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Int64 = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "Panel3", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "Panel4", PayLocNo)
        End If

        PopulateGridID()
        PopulateGridOI()
        PopulateGridPH()
        PopulateGridSD()
        PopulateTabHeader()
        Generic.PopulateDXGridFilter(grdMainID, UserNo, PayLocNo)
        Generic.PopulateDXGridFilter(grdMainOI, UserNo, PayLocNo)
        Generic.PopulateDXGridFilter(grdMainPH, UserNo, PayLocNo)
        Generic.PopulateDXGridFilter(grdMainSD, UserNo, PayLocNo)
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinic_WebTabHeader", UserNo, TransNo, Generic.ToBol(Session("IsDependent")))
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next

        If Generic.ToBol(Session("IsDependent")) = False Then
            imgPhoto.Visible = True
            imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
        Else
            imgPhoto.Visible = False
            'imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
        End If

    End Sub

#Region "*******Past ID*******"

    Private Sub PopulateGridID(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EClinicPastID_Web", UserNo, TransNo, Generic.ToBol(Session("IsDependent")))
        Me.grdMainID.DataSource = _dt
        Me.grdMainID.DataBind()
    End Sub
    Protected Sub lnkDeleteID_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMainID.GetSelectedFieldValues(New String() {"ClinicPastIDNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EClinicPastID", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGridID()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkExportID_Click(sender As Object, e As EventArgs)
        Try
            grdExportID.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub
    Protected Sub lnkEditID_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Generic.ClearControls(Me, "Panel1")
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateDataID(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ClinicPastIDNo"}))
        mdlMainID.Show()
    End Sub
    Private Sub PopulateDataID(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinicPastID_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel1", dt)
    End Sub
    Private Function SaveRecordID() As Boolean

        Dim ClinicPastIDNo As Integer = Generic.ToInt(txtClinicPastIDCode.Text)
        Dim ClinicIDNo As Integer = Generic.ToInt(cboClinicIDNo.SelectedValue)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)

        If SQLHelper.ExecuteNonQuery("EClinicPastID_WebSave", UserNo, ClinicPastIDNo, TransNo, ClinicIDNo, Remarks, Generic.ToBol(Session("IsDependent"))) > 0 Then
            SaveRecordID = True
        Else
            SaveRecordID = False
        End If

    End Function
    Protected Sub lnkAddID_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            mdlMainID.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    Protected Sub lnkSaveID_Click(sender As Object, e As EventArgs)
        If SaveRecordID() Then
            PopulateGridID()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

#End Region

#Region "*******Past OI*******"

    Private Sub PopulateGridOI(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EClinicPastOI_Web", UserNo, TransNo, Generic.ToBol(Session("IsDependent")))
        Me.grdMainOI.DataSource = _dt
        Me.grdMainOI.DataBind()
    End Sub
    Protected Sub lnkDeleteOI_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMainOI.GetSelectedFieldValues(New String() {"ClinicPastOINo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EClinicPastOI", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGridOI()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkExportOI_Click(sender As Object, e As EventArgs)
        Try
            grdExportOI.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub
    Protected Sub lnkEditOI_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Generic.ClearControls(Me, "Panel2")
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateDataOI(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ClinicPastOINo"}))
        mdlMainOI.Show()
    End Sub
    Private Sub PopulateDataOI(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinicPastOI_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
    End Sub
    Private Function SaveRecordOI() As Boolean

        Dim ClinicPastOINo As Integer = Generic.ToInt(txtClinicPastOICode.Text)
        Dim NatureOfInjury As String = Generic.ToStr(txtNatureOfInjury.Text)
        Dim NatureOfOperation As String = Generic.ToStr(txtNatureOfOperation.Text)
        Dim OIDate As String = Generic.ToStr(txtOIDate.Text)
        Dim OperativeDiagnosis As String = Generic.ToStr(txtOperativeDiagnosis.Text)

        If SQLHelper.ExecuteNonQuery("EClinicPastOI_WebSave", UserNo, ClinicPastOINo, TransNo, NatureOfInjury, NatureOfOperation, OIDate, OperativeDiagnosis, Generic.ToBol(Session("IsDependent")), Generic.ToBol(chkIsWorkRelated.Checked)) > 0 Then
            SaveRecordOI = True
        Else
            SaveRecordOI = False
        End If

    End Function
    Protected Sub lnkAddOI_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel2")
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", Generic.ToInt(Session("xPayLocNo")))
            mdlMainOI.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    Protected Sub lnkSaveOI_Click(sender As Object, e As EventArgs)

        If SaveRecordOI() Then
            PopulateGridOI()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub


#End Region

#Region "*******Past PH*******"

    Private Sub PopulateGridPH(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EClinicPastPH_Web", UserNo, TransNo, Generic.ToBol(Session("IsDependent")))
        Me.grdMainPH.DataSource = _dt
        Me.grdMainPH.DataBind()
    End Sub
    Protected Sub lnkDeletePH_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMainPH.GetSelectedFieldValues(New String() {"ClinicPastPHNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EClinicPastPH", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGridPH()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkExportPH_Click(sender As Object, e As EventArgs)
        Try
            grdExportPH.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub
    Protected Sub lnkEditPH_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Generic.ClearControls(Me, "Panel3")
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateDataPH(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ClinicPastPHNo"}))
        mdlMainPH.Show()
    End Sub
    Private Sub PopulateDataPH(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinicPastPH_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel3", dt)
    End Sub
    Private Function SaveRecordPH() As Boolean

        Dim ClinicPastPHNo As Integer = Generic.ToInt(txtClinicPastPHCode.Text)
        Dim NatureOfConfinement As String = Generic.ToStr(txtNatureOfConfinement.Text)
        Dim Hospital As String = Generic.ToStr(txtHospital.Text)
        Dim DateOfConfinement As String = Generic.ToStr(txtDateOfConfinement.Text)

        If SQLHelper.ExecuteNonQuery("EClinicPastPH_WebSave", UserNo, ClinicPastPHNo, TransNo, NatureOfConfinement, Hospital, DateOfConfinement, Generic.ToBol(Session("IsDependent"))) > 0 Then
            SaveRecordPH = True
        Else
            SaveRecordPH = False
        End If

    End Function
    Protected Sub lnkAddPH_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel3")
            Generic.PopulateDropDownList(UserNo, Me, "Panel3", Generic.ToInt(Session("xPayLocNo")))
            mdlMainPH.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    Protected Sub lnkSavePH_Click(sender As Object, e As EventArgs)

        If SaveRecordPH() Then
            PopulateGridPH()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub


#End Region

#Region "*******Past SD*******"

    Private Sub PopulateGridSD(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EClinicPastSD_Web", UserNo, TransNo, Generic.ToBol(Session("IsDependent")))
        Me.grdMainSD.DataSource = _dt
        Me.grdMainSD.DataBind()
    End Sub
    Protected Sub lnkDeleteSD_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMainSD.GetSelectedFieldValues(New String() {"ClinicPastSDNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EClinicPastSD", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGridSD()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkExportSD_Click(sender As Object, e As EventArgs)
        Try
            grdExportSD.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub
    Protected Sub lnkEditSD_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Generic.ClearControls(Me, "Panel4")
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateDataSD(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ClinicPastSDNo"}))
        mdlMainSD.Show()
    End Sub
    Private Sub PopulateDataSD(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinicPastSD_WebOne", UserNo, id)
        For Each row As DataRow In dt.Rows
            PopulateClinicSD(Generic.ToInt(row("ClinicSDTypeNo")))
            Generic.PopulateData(Me, "Panel4", dt)
        Next
    End Sub
    Private Function SaveRecordSD() As Boolean

        Dim ClinicPastSDNo As Integer = Generic.ToInt(txtClinicPastSDCode.Text)
        Dim ClinicSDNo As Integer = Generic.ToInt(cboClinicSDNo.SelectedValue)
        Dim ClinicSDTypeNo As Integer = Generic.ToInt(cboClinicSDTypeNo.SelectedValue)
        Dim Remarks As String = Generic.ToStr(txtRemarksSD.Text)

        If SQLHelper.ExecuteNonQuery("EClinicPastSD_WebSave", UserNo, ClinicPastSDNo, TransNo, ClinicSDNo, ClinicSDTypeNo, Remarks, Generic.ToBol(Session("IsDependent"))) > 0 Then
            SaveRecordSD = True
        Else
            SaveRecordSD = False
        End If

    End Function
    Protected Sub lnkAddSD_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel4")
            Generic.PopulateDropDownList(UserNo, Me, "Panel4", Generic.ToInt(Session("xPayLocNo")))
            mdlMainSD.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    Protected Sub lnkSaveSD_Click(sender As Object, e As EventArgs)

        If SaveRecordSD() Then
            PopulateGridSD()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Protected Sub ClinicSDNo_ValueChanged(sender As Object, e As System.EventArgs)
        Try
            PopulateClinicSD(Generic.ToInt(Me.cboClinicSDTypeNo.SelectedValue))
            mdlMainSD.Show()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PopulateClinicSD(tno As Integer)
        Try
            cboClinicSDNo.DataSource = SQLHelper.ExecuteDataSet("EClinicSD_WebLookup", UserNo, tno)
            cboClinicSDNo.DataTextField = "tDesc"
            cboClinicSDNo.DataValueField = "tNo"
            cboClinicSDNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

#End Region

    
End Class
