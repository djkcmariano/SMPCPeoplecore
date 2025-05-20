Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_AppMREdit_JobSpecs
    Inherits System.Web.UI.Page

    Dim xScript As String = ""
    Dim UserNo As Integer = 0
    Dim transNo As Integer = 0
    Dim showFrm As New clsFormControls

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        transNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then            
            PopulateTabHeader()
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupEduc", Session("xPayLocNo"))
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupExpe", Session("xPayLocNo"))
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupComp", Session("xPayLocNo"))
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupElig", Session("xPayLocNo"))
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupTrn", Session("xPayLocNo"))
            EnabledControls()
        End If

        PopulateGridEduc()
        PopulateGridExpe()
        PopulateGridComp()
        PopulateGridELig()
        PopulateGridTrn()

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMR_WebTabHeader", UserNo, transNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub EnabledControls()

        If Generic.CheckDBNull(transNo, clsBase.clsBaseLibrary.enumObjectType.IntType) = 0 Then
            Me.lnkAddEduc.Visible = False
            Me.lnkDeleteEduc.Visible = False

            Me.lnkAddExpe.Visible = False
            Me.lnkDeleteExpe.Visible = False

            Me.lnkAddComp.Visible = False
            Me.lnkDeleteComp.Visible = False

            Me.lnkAddElig.Visible = False
            Me.lnkDeleteElig.Visible = False

            Me.lnkAddTrn.Visible = False
            Me.lnkDeleteTrn.Visible = False
        Else
            Me.lnkAddEduc.Visible = True
            Me.lnkDeleteEduc.Visible = True

            Me.lnkAddExpe.Visible = True
            Me.lnkDeleteExpe.Visible = True

            Me.lnkAddComp.Visible = True
            Me.lnkDeleteComp.Visible = True

            Me.lnkAddElig.Visible = True
            Me.lnkDeleteElig.Visible = True

            Me.lnkAddTrn.Visible = True
            Me.lnkDeleteTrn.Visible = True
        End If

    End Sub

#Region "Education"

    Private Sub PopulateGridEduc(Optional pageno As Integer = 0)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMREduc_Web", UserNo, transNo)
            grdEduc.DataSource = dt
            grdEduc.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkDeleteEduc_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdEduc.GetSelectedFieldValues(New String() {"MREducNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EMREduc", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGridEduc()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
            
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
        
    End Sub

    Protected Sub lnkAddEduc_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupEduc")
            mdlEduc.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub PopulateDataEduc(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMREduc_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupEduc", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkEditEduc_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataEduc(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MREducNo"})))
            mdlEduc.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

        
    End Sub

    Protected Sub btnSaveEduc_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(txtMREducNo.Text)
        Dim typeno As Integer = Generic.ToInt(cboEducTypeNo.SelectedValue)
        Dim IsQS As Boolean = Generic.ToBol(Me.chkIsQSEduc.Checked)

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EMREduc_WebValidate", tno, transNo, typeno, IsQS)
        For Each row As DataRow In dt.Rows
            invalid = Generic.ToBol(row("Invalid"))
            messagedialog = Generic.ToStr(row("MessageDialog"))
            alerttype = Generic.ToStr(row("AlertType"))
        Next

        If invalid = True Then
            mdlEduc.Show()
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EMREduc_WebSave", tno, transNo, typeno, IsQS, Generic.ToInt(cboEducLevelNo.SelectedValue), Generic.ToInt(cboCourseNo.SelectedValue), Generic.ToInt(cboFieldOfStudyNo.SelectedValue), txtDescriptionEduc.Text, Generic.ToInt(chkIsGraduated.Checked)) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateGridEduc()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


#End Region


#Region "Experience"

    Private Sub PopulateGridExpe(Optional pageno As Integer = 0)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRExpe_Web", UserNo, transNo)
            grdExpe.DataSource = dt
            grdExpe.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkDeleteExpe_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdExpe.GetSelectedFieldValues(New String() {"MRExpeNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EMRExpe", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGridExpe()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If

    End Sub

    Protected Sub lnkAddExpe_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupExpe")
            mdlExpe.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub PopulateDataExpe(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRExpe_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupExpe", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkEditExpe_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataExpe(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MRExpeNo"})))
            mdlExpe.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub btnSaveExpe_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(txtMRExpeNo.Text)
        Dim typeno As Integer = Generic.ToInt(cboExpeTypeno.SelectedValue)
        Dim IsQS As Boolean = Generic.ToBol(Me.chkIsQSExpe.Checked)

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EMRExpe_WebValidate", tno, transNo, typeno, IsQS)
        For Each row As DataRow In dt.Rows
            invalid = Generic.ToBol(row("Invalid"))
            messagedialog = Generic.ToStr(row("MessageDialog"))
            alerttype = Generic.ToStr(row("AlertType"))
        Next

        If invalid = True Then
            mdlExpe.Show()
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EMRExpe_WebSave", tno, transNo, typeno, IsQS, txtDescriptionExpe.Text, Generic.ToDbl(txtNoOfYear.Text)) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateGridExpe()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

#End Region


#Region "Competency"

    Private Sub PopulateGridComp(Optional pageno As Integer = 0)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRComp_Web", UserNo, transNo)
            grdComp.DataSource = dt
            grdComp.DataBind()
        Catch ex As Exception

        End Try

    End Sub
    
    Protected Sub lnkDeleteComp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdComp.GetSelectedFieldValues(New String() {"MRCompNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EMRComp", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGridComp()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If

        
    End Sub

    Protected Sub lnkAddComp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupComp")
            mdlComp.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    Protected Sub PopulateDataComp(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRComp_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Try
                    Me.cboCompNo.DataSource = SQLHelper.ExecuteDataSet("ECompetency_WebLookup_UnionAll", UserNo, Generic.ToInt(row("CompTypeNo")), 0, 0)
                    Me.cboCompNo.DataTextField = "tdesc"
                    Me.cboCompNo.DataValueField = "tno"
                    Me.cboCompNo.DataBind()
                Catch ex As Exception
                End Try
                Generic.PopulateData(Me, "pnlPopupComp", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkEditComp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataComp(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MRCompNo"})))
            mdlComp.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub btnSaveComp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(txtMRCompCode.Text)
        Dim typeno As Integer = Generic.ToInt(cboCompNo.SelectedValue)
        Dim IsQS As Boolean = Generic.ToBol(Me.chkIsQSExpe.Checked)

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EMRComp_WebValidate", tno, transNo, typeno, IsQS)
        For Each row As DataRow In dt.Rows
            invalid = Generic.ToBol(row("Invalid"))
            messagedialog = Generic.ToStr(row("MessageDialog"))
            alerttype = Generic.ToStr(row("AlertType"))
        Next

        If invalid = True Then
            mdlComp.Show()
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EMRComp_WebSave", tno, transNo, typeno, IsQS, Generic.ToInt(cboCompTypeNo.SelectedValue), Generic.ToInt(cboCompScaleNo.SelectedValue), txtAnchor.Text) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateGridComp()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub cboCompTypeNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateCompetency()
        txtAnchor.Text = GetIndicator()
        mdlComp.Show()
    End Sub

    Protected Sub cboCompScaleNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtAnchor.Text = GetIndicator()
        mdlComp.Show()
    End Sub

    Private Sub PopulateCompetency()
        Try
            Me.cboCompNo.DataSource = SQLHelper.ExecuteDataSet("ECompetency_WebLookup_UnionAll", UserNo, Generic.ToInt(Me.cboCompTypeNo.SelectedValue), 0, 0)
            Me.cboCompNo.DataTextField = "tdesc"
            Me.cboCompNo.DataValueField = "tno"
            Me.cboCompNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetIndicator() As String
        Return Generic.ToStr(SQLHelper.ExecuteScalar("select top 1 Anchor from dbo.ECompDeti where CompScaleNo=" & Generic.ToInt(cboCompScaleNo.SelectedValue) & " and CompNo=" & Generic.ToInt(cboCompNo.SelectedValue)))
    End Function

#End Region


#Region "Eligibility"

    Private Sub PopulateGridELig(Optional pageno As Integer = 0)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRElig_Web", UserNo, transNo)
            grdElig.DataSource = dt
            grdElig.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkDeleteELig_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdElig.GetSelectedFieldValues(New String() {"MREligNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EMRElig", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGridELig()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkAddElig_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupElig")
            mdlElig.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub PopulateDataElig(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRElig_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupElig", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkEditELig_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataElig(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MREligNo"})))
            mdlElig.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub btnSaveElig_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(txtMREligNo.Text)
        Dim typeno As Integer = Generic.ToInt(cboExamtypeNo.SelectedValue)
        Dim IsQS As Boolean = Generic.ToBol(Me.chkIsQSElig.Checked)

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EMRElig_WebValidate", tno, transNo, typeno, IsQS)
        For Each row As DataRow In dt.Rows
            invalid = Generic.ToBol(row("Invalid"))
            messagedialog = Generic.ToStr(row("MessageDialog"))
            alerttype = Generic.ToStr(row("AlertType"))
        Next

        If invalid = True Then
            mdlElig.Show()
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EMRElig_WebSave", tno, transNo, typeno, Generic.ToDbl(txtAverageRate.Text), 0, IsQS, txtDescriptionElig.Text) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateGridELig()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub
#End Region


#Region "Training"

    Private Sub PopulateGridTrn(Optional pageno As Integer = 0)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRTrn_Web", UserNo, transNo)
            grdTrn.DataSource = dt
            grdTrn.DataBind()
        Catch ex As Exception

        End Try

    End Sub
    
    Protected Sub lnkDeleteTrn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteTrn.Click
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdTrn.GetSelectedFieldValues(New String() {"MRTrnNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EMRTrn", UserNo, item)
                i = i + 1
            Next
            If i > 0 Then
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGridTrn()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkAddTrn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupTrn")
            mdlTrn.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub PopulateDataTrn(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRTrn_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupTrn", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkEditTrn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateDataTrn(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MRTrnNo"})))
            mdlTrn.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub btnSaveTrn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(txtMRTrnNo.Text)
        Dim typeno As Integer = Generic.ToInt(cboTrnTitleNo.SelectedValue)
        Dim IsQS As Boolean = Generic.ToBol(Me.chkIsQSTrn.Checked)

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EMRTrn_WebValidate", tno, transNo, typeno, IsQS)
        For Each row As DataRow In dt.Rows
            invalid = Generic.ToBol(row("Invalid"))
            messagedialog = Generic.ToStr(row("MessageDialog"))
            alerttype = Generic.ToStr(row("AlertType"))
        Next

        If invalid = True Then
            mdlTrn.Show()
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EMRTrn_WebSave", tno, transNo, typeno, Generic.ToDec(txtNoOfHours.Text), 0, IsQS, txtDescriptionTrn.Text) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateGridTrn()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub
#End Region




End Class
