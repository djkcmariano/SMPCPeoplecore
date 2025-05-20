Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Partial Class Secured_EmpStandardHeader_Cate
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0


    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "EmpStandardHeader.aspx", "EApplicantStandardHeader") Then
        Generic.ClearControls(Me, "Panel1")
        ModalPopupExtender1.Show()
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        'End If

    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "EmpStandardHeader.aspx", "EApplicantStandardHeader") Then
        Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"ApplicantStandardCateNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EApplicantStandardCate", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
        'Else
        '    MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        'End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpStandardHeader.aspx", "EApplicantStandardHeader") Then
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ApplicantStandardCateNo"})))
        ModalPopupExtender1.Show()
        'Else
        '    MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        'End If
    End Sub

    Private Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EApplicantStandardCate_WebOne", UserNo, id)
            Generic.PopulateData(Me, "Panel1", dt)
        Catch ex As Exception

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

        If SQLHelper.ExecuteNonQuery("EApplicantStandardCate_WebSave", UserNo, Generic.ToInt(txtCode.Text), TransNo, txtApplicantStandardCateCode.Text, txtApplicantStandardCateDesc.Text, Generic.ToInt(txtOrderBy.Text), 0) > 0 Then
            Return True
        Else
            Return (False)
        End If

    End Function

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicantStandardMain_WebHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticatedCoreUser()

        If Not IsPostBack Then
            PopulateTabHeader()
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        End If
        PopulateGrid()
    End Sub

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EApplicantStandardCate_Web", UserNo, TransNo)
        grdDetl.DataSource = _dt
        grdDetl.DataBind()
    End Sub
End Class
