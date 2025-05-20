Imports clsLib
Imports System.Data
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting

Partial Class Secured_CarJDList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EJD_Web", UserNo, "", Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()
        End If
        PopulateGrid()
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As New LinkButton
            'Dim URL As String
            lnk = sender
            'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            'URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"JDNo"})))
            'Info1.JDNo = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"JDNo"}))
            Info1.JDNo = Generic.ToInt(lnk.CommandArgument)
            Info1.Show()


        Catch ex As Exception

        End Try
    End Sub


#Region "Main"

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 14)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "CarJDList.aspx", "EJD") Then
            Response.Redirect("~/secured/carjdedit.aspx?id=0")
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)        
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "CarJDList.aspx", "EJD") Then
            Dim lnk As New LinkButton
            Dim URL As String
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"JDNo"})))
            If URL <> "" Then
                Response.Redirect(URL)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "CarJDList.aspx", "EJD") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"JDNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeDepe", UserNo, item)
                Generic.DeleteRecordAudit("EJD", UserNo, item)
                Generic.DeleteRecordAuditCol("EJDComp", UserNo, "JDNo", item)
                Generic.DeleteRecordAuditCol("EJDEduc", UserNo, "JDNo", item)
                Generic.DeleteRecordAuditCol("EJDElig", UserNo, "JDNo", item)
                Generic.DeleteRecordAuditCol("EJDTrn", UserNo, "JDNo", item)
                Generic.DeleteRecordAuditCol("EJDKRE", UserNo, "JDNo", item)
                Generic.DeleteRecordAuditCol("EJDCare", UserNo, "JDNo", item)
                Generic.DeleteRecordAuditCol("EJDExpe", UserNo, "JDNo", item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub btnAttach_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        'ViewState("TransNo") = 0
        PopulateGrid()
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

#End Region

End Class
