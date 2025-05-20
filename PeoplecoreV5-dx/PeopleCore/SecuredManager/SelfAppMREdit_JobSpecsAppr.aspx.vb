Imports clsLib
Imports System.Data

Partial Class SecuredManager_SelfAppMREdit_JobSpecsAppr
    Inherits System.Web.UI.Page

    Dim xScript As String = ""
    Dim UserNo As Integer = 0
    Dim transNo As Integer = 0
    Dim showFrm As New clsFormControls

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        transNo = Generic.ToInt(Request.QueryString("id"))

        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            PopulateGridEduc()
            PopulateGridExpe()
            PopulateGridComp()
            PopulateGridELig()
            PopulateGridTrn()
            PopulateTabHeader()
            EnabledControls()
        End If
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRTabHeader", UserNo, transNo)
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
            Dim _dsDetl As DataSet
            _dsDetl = SQLHelper.ExecuteDataSet("EMREduc_Web", UserNo, transNo)
            grdEduc.PageIndex = pageno
            Me.grdEduc.DataSource = _dsDetl
            Me.grdEduc.DataBind()

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub grdEduc_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdEduc.PageIndexChanging
        Dim pageno As Integer = e.NewPageIndex
        PopulateGridEduc(pageno)
    End Sub
    Protected Sub lnkDeleteEduc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteEduc.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0

        For tcount = 0 To Me.grdEduc.Rows.Count - 1
            lbl = CType(grdEduc.Rows(tcount).FindControl("lblIdeduc"), Label)
            tcheck = CType(grdEduc.Rows(tcount).FindControl("txtIsSelected"), CheckBox)
            If tcheck.Checked = True Then
                Generic.DeleteRecordAudit("EMREduc", UserNo, CType(lbl.Text, Integer))
                DeleteCount = DeleteCount + 1
            End If
        Next
        PopulateGridEduc()
        MessageBox.Success("(" & DeleteCount.ToString & ") " & MessageTemplate.SuccessDelete, Me)

    End Sub
    Protected Sub lnkAddEduc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAddEduc.Click

        showFrm.clearFormControls_In_Popup(pnlPopupEduc)
        showFrm.populateCombo_In_form_Popup(UserNo, pnlPopupEduc)
        mdlEduc.Show()
        Me.txtMREducCode.Text = "Autonumber"
    End Sub
    Protected Sub lnkEditEduc_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lnk As New ImageButton
            Dim i As String = "", fRowNo As Integer = 0, mrEducNo As Integer = 0
            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            fRowNo = gvrow.RowIndex
            i = grdEduc.DataKeys(gvrow.RowIndex).Values(0).ToString()
            mrEducNo = grdEduc.DataKeys(gvrow.RowIndex).Values(1).ToString()
            transNo = i

            Dim dsEduc As DataSet
            dsEduc = SQLHelper.ExecuteDataSet("EMREduc_WebOne", UserNo, mrEducNo)
            If dsEduc.Tables.Count > 0 Then
                If dsEduc.Tables(0).Rows.Count > 0 Then
                    showFrm.showFormControls_In_Popup(pnlPopupEduc, dsEduc)
                    showFrm.populateCombo_In_form_Popup(UserNo, pnlPopupEduc)
                End If
            End If
            mdlEduc.Show()

        Catch ex As Exception
        End Try
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

        If SQLHelper.ExecuteNonQuery("EMREduc_WebSave", tno, transNo, typeno, IsQS) > 0 Then
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
            Dim _dsDetl As DataSet
            _dsDetl = SQLHelper.ExecuteDataSet("EMRExpe_Web", UserNo, transNo)
            grdExpe.PageIndex = pageno
            Me.grdExpe.DataSource = _dsDetl
            Me.grdExpe.DataBind()

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub grdExpe_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdExpe.PageIndexChanging
        Dim pageno As Integer = e.NewPageIndex
        PopulateGridExpe(pageno)
    End Sub
    Protected Sub lnkDeleteExpe_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteExpe.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0

        For tcount = 0 To Me.grdExpe.Rows.Count - 1
            lbl = CType(grdExpe.Rows(tcount).FindControl("lblIdexpe"), Label)
            tcheck = CType(grdExpe.Rows(tcount).FindControl("txtIsSelectEx"), CheckBox)
            If tcheck.Checked = True Then
                Generic.DeleteRecordAudit("EMRExpe", UserNo, CType(lbl.Text, Integer))
                DeleteCount = DeleteCount + 1
            End If
        Next
        PopulateGridExpe()
        MessageBox.Success("(" & DeleteCount.ToString & ") " & MessageTemplate.SuccessDelete, Me)

    End Sub

    Protected Sub lnkAddExpe_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        showFrm.clearFormControls_In_Popup(pnlPopupExpe)
        showFrm.populateCombo_In_form_Popup(UserNo, pnlPopupExpe)
        mdlExpe.Show()

        Me.txtMRExpeCode.Text = "Autonumber"
    End Sub
    Protected Sub lnkEditExpe_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lnk As New ImageButton
            Dim i As String = "", fRowNo As Integer = 0, mrEducNo As Integer = 0

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            fRowNo = gvrow.RowIndex

            i = grdExpe.DataKeys(gvrow.RowIndex).Values(0).ToString()
            mrEducNo = grdExpe.DataKeys(gvrow.RowIndex).Values(1).ToString()

            transNo = i

            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet("EMRExpe_WebOne", UserNo, mrEducNo)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    showFrm.showFormControls_In_Popup(pnlPopupExpe, ds)
                    showFrm.populateCombo_In_form_Popup(UserNo, pnlPopupExpe)
                End If
            End If
            mdlExpe.Show()

        Catch ex As Exception
        End Try
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

        If SQLHelper.ExecuteNonQuery("EMRExpe_WebSave", tno, transNo, typeno, IsQS) > 0 Then
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
            Dim _dsDetl As DataSet
            _dsDetl = SQLHelper.ExecuteDataSet("EMRComp_Web", UserNo, transNo)
            grdComp.PageIndex = pageno
            Me.grdComp.DataSource = _dsDetl
            Me.grdComp.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub grdComp_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdComp.PageIndexChanging
        Dim pageno As Integer = e.NewPageIndex
        PopulateGridComp(pageno)
    End Sub

    Protected Sub lnkDeleteComp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteComp.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0

        For tcount = 0 To Me.grdComp.Rows.Count - 1
            lbl = CType(grdComp.Rows(tcount).FindControl("lblIdComp"), Label)
            tcheck = CType(grdComp.Rows(tcount).FindControl("txtIsSelectComp"), CheckBox)
            If tcheck.Checked = True Then
                Generic.DeleteRecordAudit("EMRComp", UserNo, CType(lbl.Text, Integer))
                DeleteCount = DeleteCount + 1
            End If
        Next
        PopulateGridComp()
        MessageBox.Success("(" & DeleteCount.ToString & ") " & MessageTemplate.SuccessDelete, Me)

    End Sub

    Protected Sub lnkAddComp_Click(ByVal sender As Object, ByVal e As System.EventArgs)


        showFrm.clearFormControls_In_Popup(pnlPopupComp)
        showFrm.populateCombo_In_form_Popup(UserNo, pnlPopupComp)
        mdlComp.Show()

        Me.txtMRCompCode.Text = "Autonumber"
    End Sub
    Protected Sub lnkEditComp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lnk As New ImageButton
            Dim i As String = "", fRowNo As Integer = 0, mrEducNo As Integer = 0

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            fRowNo = gvrow.RowIndex

            i = grdComp.DataKeys(gvrow.RowIndex).Values(0).ToString()
            mrEducNo = grdComp.DataKeys(gvrow.RowIndex).Values(1).ToString()

            transNo = i

            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet("EMRComp_WebOne", UserNo, mrEducNo)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    showFrm.showFormControls_In_Popup(pnlPopupComp, ds)
                    showFrm.populateCombo_In_form_Popup(UserNo, pnlPopupComp)
                End If
            End If
            mdlComp.Show()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnSaveComp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(txtMRCompNo.Text)
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

        If SQLHelper.ExecuteNonQuery("EMRComp_WebSave", tno, transNo, typeno, IsQS) > 0 Then
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

#End Region


#Region "Eligibility"

    Private Sub PopulateGridELig(Optional pageno As Integer = 0)
        Try
            Dim _dsDetl As DataSet
            _dsDetl = SQLHelper.ExecuteDataSet("EMRELig_Web", UserNo, transNo)
            grdelig.PageIndex = pageno
            Me.grdelig.DataSource = _dsDetl
            Me.grdelig.DataBind()

        Catch ex As Exception

        End Try

    End Sub
    Protected Sub grdELig_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdelig.PageIndexChanging
        Dim pageno As Integer = e.NewPageIndex
        PopulateGridELig(pageno)
    End Sub

    Protected Sub lnkDeleteELig_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteElig.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0

        For tcount = 0 To Me.grdelig.Rows.Count - 1
            lbl = CType(grdelig.Rows(tcount).FindControl("lblIdELig"), Label)
            tcheck = CType(grdelig.Rows(tcount).FindControl("txtIsSelectELig"), CheckBox)
            If tcheck.Checked = True Then
                Generic.DeleteRecordAudit("EMRELig", UserNo, CType(lbl.Text, Integer))
                DeleteCount = DeleteCount + 1
            End If
        Next
        PopulateGridELig()

        MessageBox.Success("(" & DeleteCount.ToString & ") " & MessageTemplate.SuccessDelete, Me)

    End Sub

    Protected Sub lnkAddELig_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        showFrm.clearFormControls_In_Popup(pnlPopupElig)
        showFrm.populateCombo_In_form_Popup(UserNo, pnlPopupElig)
        mdlElig.Show()

        'Me.txtMREducCode.Text = "Autonumber"
    End Sub
    Protected Sub lnkEditELig_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lnk As New ImageButton
            Dim i As String = "", fRowNo As Integer = 0, mrEducNo As Integer = 0

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            fRowNo = gvrow.RowIndex

            i = grdelig.DataKeys(gvrow.RowIndex).Values(0).ToString()
            mrEducNo = grdelig.DataKeys(gvrow.RowIndex).Values(1).ToString()
            transNo = i

            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet("EMRELig_WebOne", UserNo, mrEducNo)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    showFrm.showFormControls_In_Popup(pnlPopupElig, ds)
                    showFrm.populateCombo_In_form_Popup(UserNo, pnlPopupElig)
                End If
            End If
            mdlElig.Show()

        Catch ex As Exception
        End Try
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

        If SQLHelper.ExecuteNonQuery("EMRElig_WebSave", tno, transNo, typeno, 0, 0, IsQS) > 0 Then
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
            Dim _dsDetl As DataSet
            _dsDetl = SQLHelper.ExecuteDataSet("EMRTrn_Web", UserNo, transNo)
            grdTrn.PageIndex = pageno
            Me.grdTrn.DataSource = _dsDetl
            Me.grdTrn.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub grdTrn_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdTrn.PageIndexChanging
        Dim pageno As Integer = e.NewPageIndex
        PopulateGridTrn(pageno)
    End Sub

    Protected Sub lnkDeleteTrn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDeleteTrn.Click
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0

        For tcount = 0 To Me.grdTrn.Rows.Count - 1
            lbl = CType(grdTrn.Rows(tcount).FindControl("lblIdTrn"), Label)
            tcheck = CType(grdTrn.Rows(tcount).FindControl("txtIsSelectTrn"), CheckBox)
            If tcheck.Checked = True Then
                Generic.DeleteRecordAudit("EMRTrn", UserNo, CType(lbl.Text, Integer))
                DeleteCount = DeleteCount + 1
            End If
        Next
        PopulateGridTrn()
        MessageBox.Success("(" & DeleteCount.ToString & ") " & MessageTemplate.SuccessDelete, Me)

    End Sub

    Protected Sub lnkAddTrn_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        showFrm.clearFormControls_In_Popup(pnlPopupTrn)
        showFrm.populateCombo_In_form_Popup(UserNo, pnlPopupTrn)
        mdlTrn.Show()

        Me.txtMRTrnCode.Text = "Autonumber"
    End Sub
    Protected Sub lnkEditTrn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim lnk As New ImageButton
            Dim i As String = "", fRowNo As Integer = 0, mrEducNo As Integer = 0

            lnk = sender
            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
            fRowNo = gvrow.RowIndex

            i = grdTrn.DataKeys(gvrow.RowIndex).Values(0).ToString()
            mrEducNo = grdTrn.DataKeys(gvrow.RowIndex).Values(1).ToString()

            transNo = i

            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet("EMRTrn_WebOne", UserNo, mrEducNo)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    showFrm.showFormControls_In_Popup(pnlPopupTrn, ds)
                    showFrm.populateCombo_In_form_Popup(UserNo, pnlPopupTrn)
                End If
            End If
            mdlTrn.Show()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnSaveTrn_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(txtMRTrnNo.Text)
        Dim typeno As Integer = Generic.ToInt(cboCompTrnNo.SelectedValue)
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

        If SQLHelper.ExecuteNonQuery("EMRTrn_WebSave", tno, transNo, typeno, 0, 0, IsQS) > 0 Then
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
