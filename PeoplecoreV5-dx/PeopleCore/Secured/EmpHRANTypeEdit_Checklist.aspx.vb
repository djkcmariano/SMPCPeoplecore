Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_EmpHRANTypeEdit_Checklist
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRANCheckListType_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRANType_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "EmpHRANTypeList.aspx", "EHRANType")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateTabHeader()
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub


    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRANCheckListType_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    'Private Sub PopulateTabHeader()
    '    Dim dt As DataTable
    '    dt = SQLHelper.ExecuteDataTable("EEvalTemplateTabHeader", UserNo, TransNo)
    '    For Each row As DataRow In dt.Rows
    '        lbl.Text = Generic.ToStr(row("Display"))
    '    Next        
    'End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "EmpHRANTypeList.aspx", "EHRANType") Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "EmpHRANTypeList.aspx", "EHRANType") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"tno"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "EmpHRANTypeList.aspx", "EHRANType") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"tno"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EHRANCheckListType", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Function SaveRecord() As Integer
        Dim tno As Integer = Generic.ToInt(Me.txttno.Text)
        Dim ChecklistTypeNo As Integer = Generic.ToInt(Me.cboApplicantStandardCheckListNo.SelectedValue)
        Dim isAttachedRequired As Boolean = Generic.ToBol(txtIsAttachedRequired.Checked)
        Dim SubmittedByDesc As String = Generic.ToStr(txtSubmittedByDesc.Text)
        Dim CheckListCateDesc As String = Generic.ToStr(txtCheckListTypeCateDesc.Text)
        If SQLHelper.ExecuteNonQuery("EHRANCheckListType_WebSave", UserNo, tno, TransNo, ChecklistTypeNo, isAttachedRequired, SubmittedByDesc, CheckListCateDesc) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If

    End Function

End Class
