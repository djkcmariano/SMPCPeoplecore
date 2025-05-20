Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_DTRHolidayEditPayClass
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHolidayPayClass_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHolidayPayClass_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "DTRHolidayList.aspx", "EHoliday")
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateTabHeader()
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "DTRHolidayList.aspx", "EHoliday") Then
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
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "DTRHolidayList.aspx", "EHoliday") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"HolidayPayClassNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "DTRHolidayList.aspx", "EHoliday") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"HolidayPayClassNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EHolidayPayClass", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Function SaveRecord() As Boolean
        If SQLHelper.ExecuteNonQuery("EHolidayPayClass_WebSave", UserNo, Generic.ToInt(txtCode.Text), TransNo, Generic.ToInt(cboPayClassNo.SelectedValue), Generic.ToDec(txtNoOfHour.Text)) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EHolidayTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
            lnkAdd.Enabled = Generic.ToBol(row("IsEnabled"))
        Next
    End Sub

End Class
