Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_OrgTableOrg
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim AutoNumberTO As Boolean = ConfigurationManager.AppSettings("AutoNumberTO")

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        'Auto Number Enabled
        'If AutoNumberTO Then
        'txtHeadPlantillaCode.Attributes.Add("PlaceHolder", "Autonumber")
        'txtHeadPlantillaCode.ReadOnly = "true"
        'End If
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            'cboTabNo.Text = Generic.ToStr(Session("xTabNo"))
            PopulateDropDown()
        End If

        PopulateGrid()

        If cboTabNo.SelectedValue = 1 Then
            lnkRevise.Visible = False
            lnkAdd.Visible = True
            lnkDelete.Visible = True
            lnkApprove.Visible = True
            lnkDisapprove.Visible = True
            lnkSave.Enabled = True
        ElseIf cboTabNo.SelectedValue = 2 Then
            lnkSave.Enabled = False
            lnkRevise.Visible = True
            lnkAdd.Visible = False
            lnkDelete.Visible = False
            lnkApprove.Visible = False
            lnkDisapprove.Visible = False
        ElseIf cboTabNo.SelectedValue = 3 Or cboTabNo.SelectedValue = 4 Then
            lnkSave.Enabled = False
            lnkRevise.Visible = False
            lnkAdd.Visible = False
            lnkDelete.Visible = False
            lnkApprove.Visible = False
            lnkDisapprove.Visible = False
        End If

    End Sub

#Region "Main"

    Protected Sub cboTabNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateGrid()

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("ETableOrg_Web", UserNo, "", Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        Session("xTabNo") = Generic.ToInt(cboTabNo.SelectedValue)

    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 15)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then            
            Generic.ClearControls(Me, "Panel1")
            Generic.EnableControls(Me, "Panel1", True)
            txtHeadPlantillaCode.ReadOnly = False
            If Generic.ToInt(txtCode.Text) = 0 Then
                chkIsFromPlantilla.Enabled = True
            End If
            chkIsFromPlantilla.Enabled = True
            ModalPopupExtender1.Show()
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TableOrgNo"})))

            If Generic.ToInt(txtCode.Text) > 0 Then
                chkIsFromPlantilla.Enabled = False
            Else
                chkIsFromPlantilla.Enabled = True
            End If

            If Generic.ToInt(txtCode.Text) > 0 Then
                txtHeadPlantillaCode.ReadOnly = False
            Else
                txtHeadPlantillaCode.ReadOnly = False
            End If

            If Generic.ToInt(cboTabNo.SelectedValue) = 1 Then
                Generic.EnableControls(Me, "Panel1", True)
            Else
                Generic.EnableControls(Me, "Panel1", False)
            End If

            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
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

    Protected Sub lnkChart_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        'PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TableOrgNo"})))
        Response.Redirect("~/secured/orgtableorgdeti.aspx?id=" & lnk.CommandArgument & "&ApprovalStatNo=" & lnk.CommandName & "&desc=" & lnk.ToolTip)

        'Dim sb As New StringBuilder                
        'Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TableOrgNo"}))
        'sb.Append("<script>")
        'sb.Append("window.open('OrgTableOrgDeti2.aspx?id=" & id.ToString() & "&desc=" & lnk.ToolTip & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        'sb.Append("</script>")
        'ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkChart_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TableOrgNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ETableOrg", UserNo, item)
                Generic.DeleteRecordAuditCol("ETableOrgDeti", UserNo, "TableOrgNo", item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If           
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETableOrg_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Function SaveRecord() As Boolean
        Dim PrepareByNo As Integer = 0 'Generic.ToInt(cboPreparedBy.SelectedValue)
        Dim PrepareDate As String = ""
        Dim ApproveByNo As Integer = 0 'cls.Generic.ToInt(cboOrgApproverListNo.SelectedValue)
        Dim ApproveDate As String = "" 'txtApprovedDate.Text
        If SQLHelper.ExecuteNonQuery("ETableOrg_WebSave", Generic.ToInt(Session("OnlineUserNo")), Generic.ToInt(txtCode.Text), txtTableOrgDesc.Text, PrepareByNo, PrepareDate, txtRemarks.Text, 1, ApproveByNo, ApproveDate, 0, 0, "", txtHeadPlantillaCode.Text, PayLocNo, Generic.ToInt(chkIsFromPlantilla.Checked), txtEffectiveDate.Text) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub lnkApprove_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TableOrgNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Dim dt As DataTable = SQLHelper.ExecuteDataTable("ETableOrg_Check", UserNo, item, "Post")
                Dim MsgNo As Integer, Msg As String = ""
                For Each row As DataRow In dt.Rows
                    MsgNo = Generic.ToInt(row("RetVal"))
                    Msg = Generic.ToStr(row("Msg"))
                Next
                If MsgNo > 0 Then
                    MessageBox.Information(Msg, Me)
                    Exit Sub
                Else
                    i = i + SQLHelper.ExecuteNonQuery("ETableOrg_Process", UserNo, item, "Post")
                End If
                'i = i + SQLHelper.ExecuteNonQuery("ETableOrg_Process", UserNo, item, "Post")
            Next

            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me)
                PopulateGrid()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
            
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDisapprove_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TableOrgNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                i = i + SQLHelper.ExecuteNonQuery("ETableOrg_Process", UserNo, item, "Cancel")
            Next

            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDisapproved, Me)
                PopulateGrid()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
          
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkRevise_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TableOrgNo"})
            Dim str As String = "", i As Integer = 0, dt As DataTable, MsgNo As Integer = 0, Msg As String = ""
            For Each item As Integer In fieldValues
                Try
                    dt = SQLHelper.ExecuteDataSet("ETableOrg_Check", UserNo, item, "Revise").Tables(0)
                    For Each row As DataRow In dt.Rows
                        MsgNo = Generic.ToInt(row("RetVal"))
                        Msg = Generic.ToStr(row("Msg"))
                    Next
                Catch ex As Exception
                End Try

                If MsgNo > 0 Then
                    MessageBox.Success(Msg, Me)
                Else
                    i = i + SQLHelper.ExecuteNonQuery("ETableOrg_Process", UserNo, item, "Revise")
                End If
            Next

            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + " transaction(s) successfully reused", Me)
                PopulateGrid()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

        ''Dim chk As New CheckBox, Count As Integer = 0, dt As DataTable, ib As New ImageButton
        ''Dim MsgNo As Integer = 0, Msg As String = ""

        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
        '    For tcount = 0 To Me.grdMain.Rows.Count - 1
        '        chk = CType(grdMain.Rows(tcount).FindControl("chk"), CheckBox)
        '        ib = CType(grdMain.Rows(tcount).FindControl("lnkEdit"), ImageButton)
        '        If chk.Checked = True Then
        '            Try
        '                dt = SQLHelper.ExecuteDataSet("ETableOrg_Check", UserNo, chk.ToolTip, "Revise").Tables(0)
        '                For Each row As DataRow In dt.Rows
        '                    MsgNo = Generic.ToInt(row("RetVal"))
        '                    Msg = Generic.ToStr(row("Msg"))
        '                Next
        '            Catch ex As Exception
        '            End Try

        '            If MsgNo > 0 Then
        '                MessageBox.Success(Msg, Me)
        '            Else
        '                Count = Count + SQLHelper.ExecuteNonQuery("ETableOrg_Process", UserNo, ib.CommandArgument, "Revise")
        '            End If
        '        End If
        '    Next
        '    cboTabNo.SelectedValue = "1"
        '    PopulateGrid()
        '    MessageBox.Success("There are (" & Count.ToString & ") Record has been successfully revised.", Me)
        'Else
        '    MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        'End If
    End Sub


#End Region

End Class
