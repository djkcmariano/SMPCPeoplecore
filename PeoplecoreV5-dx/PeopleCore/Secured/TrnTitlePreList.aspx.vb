Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_TrnTitlePreList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Int64 = 0

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("ETrnTitlePre_Web", UserNo, TransNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ETrnTitlePre_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel1", dt)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "TrnTitleList.aspx", "ETrnTitle")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateTabHeader()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnTitle_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "TrnTitleList.aspx", "ETrnTitle") Then
            Generic.ClearControls(Me, "Panel1")
            mdlMain.Show()
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
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "TrnTitleList.aspx", "ETrnTitle") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Generic.ClearControls(Me, "Panel1")
            PopulateData(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TrnTitlePreNo"}))
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "TrnTitleList.aspx", "ETrnTitle") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TrnTitlePreNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ETrnTitlePre", UserNo, CType(item, Integer))
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
    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Private Function SaveRecord() As Boolean

        Dim TrnTitlePreNo As Integer = Generic.ToInt(Me.txtTrnTitlePreNo.Text)
        Dim TrnTitleNo As Integer = Generic.CheckDBNull(Me.hifTrnTitleNo.Value, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim Description As String = Generic.CheckDBNull(Me.txtDescription.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim OrderLevel As Integer = Generic.CheckDBNull(Me.txtOrderLevel.Text, clsBase.clsBaseLibrary.enumObjectType.IntType)

        If SQLHelper.ExecuteNonQuery("ETrnTitlePre_WebSave", UserNo, TrnTitlePreNo, TransNo, TrnTitleNo, Description, OrderLevel) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False

        End If

    End Function

    Protected Sub hifTrnTitleNo_ValueChanged(sender As Object, e As System.EventArgs)
        Dim _dt As New DataTable
        _dt = SQLHelper.ExecuteDataTable("ETrnTitle_WebOne", UserNo, Generic.CheckDBNull(hifTrnTitleNo.Value, clsBase.clsBaseLibrary.enumObjectType.IntType))
        Generic.PopulateData(Me, "Panel1", _dt)
        mdlMain.Show()
    End Sub

End Class
