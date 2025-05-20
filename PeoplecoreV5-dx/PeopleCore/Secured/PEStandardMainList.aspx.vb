Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PEStandardMainList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim TransNo As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        AccessRights.CheckUser(UserNo, Generic.ToStr(Session("xFormName")), Generic.ToStr(Session("xTableName")))

        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 14)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub

#Region "Main"

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPEStandardMain_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim ib As New LinkButton
        ib = sender
        Response.Redirect("~/secured/PEStandardMainEdit.aspx?id=" & ib.CommandArgument)

    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/PEStandardMainEdit.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEStandardMainNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPEStandardMain", UserNo, CType(item, Integer))
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

    Protected Sub lnkTemplate_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Response.Redirect("PEStandardMainDetiList.aspx?id=" & container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEStandardMainNo"}))
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

#End Region

#Region "Copy Template"

    Protected Sub lnkCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim i As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEStandardMainNo"}))

            Try
                cboPEStandardMainFromNo.DataSource = SQLHelper.ExecuteDataSet("EPEStandardMain_WebLookupFrom", UserNo, i, PayLocNo)
                cboPEStandardMainFromNo.DataValueField = "tNo"
                cboPEStandardMainFromNo.DataTextField = "tDesc"
                cboPEStandardMainFromNo.DataBind()
            Catch ex As Exception
            End Try

            Try
                cboPEStandardMainToNo.DataSource = SQLHelper.ExecuteDataSet("EPEStandardMain_WebLookupTo", UserNo, i)
                cboPEStandardMainToNo.DataValueField = "tNo"
                cboPEStandardMainToNo.DataTextField = "tDesc"
                cboPEStandardMainToNo.DataBind()
            Catch ex As Exception
            End Try

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardMain_WebLookupTo", UserNo, i)
            For Each row As DataRow In dt.Rows
                cboPEStandardMainToNo.Text = Generic.ToInt(row("tNo"))
            Next

            mdlCopy.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkSaveCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim PEStandardMainFromNo As Integer = Generic.ToInt(cboPEStandardMainFromNo.SelectedValue)
        Dim PEStandardMainToNo As Integer = Generic.ToInt(cboPEStandardMainToNo.SelectedValue)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EPEStandardMain_WebCopy", UserNo, PEStandardMainFromNo, PEStandardMainToNo)
        For Each row As DataRow In dt.Rows
            invalid = Generic.ToBol(row("Invalid"))
            messagedialog = Generic.ToStr(row("MessageDialog"))
            alerttype = Generic.ToStr(row("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlCopy.Show()
            Exit Sub
        Else
            Retval = True
        End If

        If Retval = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

#End Region

End Class

















