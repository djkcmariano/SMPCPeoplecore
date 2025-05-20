Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_TrnEvalTemplateList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Int64 = 0

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("ETrnEvalTemplate_Web", UserNo, TransNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnEvalTemplate_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)

        Try
            cboEvalTemplateNo.DataSource = SQLHelper.ExecuteDataSet("EEvalTemplate_WebLookup", UserNo, "0608", PayLocNo)
            cboEvalTemplateNo.DataTextField = "tDesc"
            cboEvalTemplateNo.DataValueField = "tno"
            cboEvalTemplateNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "TrnTitleList.aspx", "ETrnTitle")

        If Not IsPostBack Then
            PopulateDropDown()
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

        Dim Retval As Boolean = False
        Dim TrnEvalTemplateNo As Integer = Generic.ToInt(Me.txtTrnEvalTemplateNo.Text)
        Dim EvalTemplateNo As Integer = Generic.ToInt(Me.cboEvalTemplateNo.SelectedValue)
        Dim TrnEvalTypeNo As Integer = Generic.ToInt(Me.cboTrnEvalTypeNo.SelectedValue)
        Dim TrnEvaluatorNo As Integer = Generic.ToInt(Me.cboTrnEvaluatorNo.SelectedValue)


        ''//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("ETrnEvalTemplate_WebValidate", UserNo, TrnEvalTemplateNo, TransNo, EvalTemplateNo, TrnEvalTypeNo, TrnEvaluatorNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlMain.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("ETrnEvalTemplate_WebSave", UserNo, TrnEvalTemplateNo, TransNo, EvalTemplateNo, TrnEvalTypeNo, TrnEvaluatorNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
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
            PopulateData(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TrnEvalTemplateNo"}))
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub


    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "TrnTitleList.aspx", "ETrnTitle") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TrnEvalTemplateNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ETrnEvalTemplate", UserNo, CType(item, Integer))
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

        

    End Function

End Class
