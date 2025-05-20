Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Partial Class Secured_EvalTemplateCate
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0


    Protected Sub lnkAddDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If


    End Sub

    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"EvalTemplateCateNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEvalTemplateCate", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
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

    Protected Sub lnkEditDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EvalTemplateCateNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEvalTemplateCate_WebOne", UserNo, id)
            Generic.PopulateData(Me, "Panel1", dt)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim ds As New DataSet
        Dim RetVal As Integer = 0, xMessage As String = "", alertType As String = ""

        ds = SQLHelper.ExecuteDataSet("ETableReferrenceDeti_WebValidate", UserNo, "EEvalTemplateCate", Generic.ToInt(txtCode.Text), Generic.ToStr(txtEvalTemplateCateCode.Text), Generic.ToStr(txtEvalTemplateCateDesc.Text), PayLocNo, "EvalTemplateNo", TransNo)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                RetVal = Generic.ToInt(ds.Tables(0).Rows(0)("RetVal"))
                xMessage = Generic.ToStr(ds.Tables(0).Rows(0)("xMessage"))
                alertType = Generic.ToStr(ds.Tables(0).Rows(0)("alertType"))
            End If
        End If

        If RetVal = 1 Then
            MessageBox.Alert(xMessage, alertType, Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub
    Private Function SaveRecord() As Boolean

        If SQLHelper.ExecuteNonQuery("EEvalTemplateCate_WebSave", UserNo, Generic.ToInt(txtCode.Text), TransNo, txtEvalTemplateCateCode.Text, txtEvalTemplateCateDesc.Text, Generic.ToInt(txtOrderBy.Text), 0) > 0 Then
            Return True
        Else
            Return (False)
        End If

    End Function

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEvalTemplateTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateTabHeader()
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        End If
        PopulateGrid()
    End Sub

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EEvalTemplateCate_Web", UserNo, TransNo)
        grdDetl.DataSource = _dt
        grdDetl.DataBind()
    End Sub
End Class
