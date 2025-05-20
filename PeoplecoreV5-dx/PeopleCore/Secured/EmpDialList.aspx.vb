Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_EmpDialList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeDialect_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeDialect_WebOne", UserNo, id)
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

        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)            
            PopulateTabHeader()
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            PopulateControls()
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
            Generic.ClearControls(Me, "Panel1")
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeDialectNo"})))
            PopulateControls()
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeDialectNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeDialect", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Function SaveRecord() As Boolean
        Dim EmployeeDialectNo As Integer = Generic.ToInt(Me.txtCode.Text)
        Dim Dialect As Integer = Generic.ToInt(Me.cboDialectNo.SelectedValue)
        Dim SpeakingLevel As Integer = Generic.ToInt(Me.cboSpeakingProfLevelNo.SelectedValue)
        Dim WritingLevel As Integer = Generic.ToInt(Me.cboWritingProfLevelNo.SelectedValue)
        Dim ReadingLevel As Integer = Generic.ToInt(Me.cboReadingProfLevelNo.SelectedValue)
        Dim Remark As String = Generic.ToStr(Me.txtRemark.Text)
        Dim IsOtherDial As Boolean = Generic.ToBol(txtIsOtherDial.Checked)
        Dim OtherDial As String = Generic.ToStr(txtOtherDial.Text)

        If SQLHelper.ExecuteNonQuery("EEmployeeDialect_WebSave", UserNo, EmployeeDialectNo, TransNo, Dialect, 0, Remark, WritingLevel, ReadingLevel, SpeakingLevel, IsOtherDial, OtherDial) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub PopulateControls()

        If txtIsOtherDial.Checked = True Then
            txtOtherDial.Enabled = True
            txtOtherDial.CssClass = "form-control required"
            cboDialectNo.CssClass = "form-control"
            cboDialectNo.Enabled = False
            cboDialectNo.Text = ""
            lblDialect.Attributes.Add("class", "col-md-4 control-label has-space")
        Else
            txtOtherDial.Enabled = False
            txtOtherDial.Text = ""
            txtOtherDial.CssClass = "form-control"
            cboDialectNo.CssClass = "form-control required"
            cboDialectNo.Enabled = True
            lblDialect.Attributes.Add("class", "col-md-4 control-label has-required")
        End If


    End Sub

    Protected Sub txtIsOtherDial_CheckedChanged(sender As Object, e As System.EventArgs) Handles txtIsOtherDial.CheckedChanged
        PopulateControls()
        ModalPopupExtender1.Show()
    End Sub

End Class









