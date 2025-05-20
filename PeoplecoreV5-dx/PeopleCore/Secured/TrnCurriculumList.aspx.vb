Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_TrnCurriculumList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim tstatus As Integer = 0

    Protected Sub PopulateGrid(Optional IsMain As Boolean = False)
        Try
            tstatus = Generic.ToInt(cboTabNo.SelectedValue)
            If tstatus = 0 Then
                tstatus = 1
            End If

            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("ETrnCurriculum_Web", UserNo, tstatus, "", PayLocNo)
            Dim dv As DataView = dt.DefaultView
            grdMain.DataSource = dv
            grdMain.DataBind()

            PopulateDetl()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub cboTab_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
           PopulateGrid

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub PopulateDetl()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("ETrnCurriculumDetl_Web", UserNo, Generic.ToInt(ViewState("TransNo")), "")
            Dim dv As DataView = dt.DefaultView
            grdTrn.DataSource = dv
            grdTrn.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateCombo()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 6)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub



    Protected Sub grdTrn_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            grdTrn.PageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)

            PopulateCombo()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub


    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo", "EmployeeCode"})
            ViewState("TransNo") = obj(0)
            PopulateDetl()

        Catch ex As Exception
        End Try
    End Sub

    

    Protected Sub btnSaveTrn_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim chk As New CheckBox, lbl As New Label, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            TransNo = Generic.ToInt(ViewState("TransNo"))
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim _dt As New DataTable
            _dt = SQLHelper.ExecuteDataTable("ETrnCurriculumDetl_WebValidate", UserNo, TransNo, Generic.ToInt(cboTrnTitleNo.Text), True, PayLocNo, Generic.ToInt(txtTrnCurriculumDetlNo.Text))
            For Each row As DataRow In _dt.Rows
                invalid = Generic.ToBol(row("Invalid"))
                messagedialog = Generic.ToStr(row("MessageDialog"))
                alerttype = Generic.ToStr(row("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                mdtrn.Show()
                Exit Sub
            End If

            Dim dt As DataTable, retVal As Boolean = False, error_num As Integer = 0, error_message As String = ""
            dt = SQLHelper.ExecuteDataTable("ETrnCurriculumDetl_WebSave", UserNo, TransNo, Generic.ToInt(cboTrnTitleNo.Text), True, PayLocNo, Generic.ToInt(txtTrnCurriculumDetlNo.Text))
            For Each row As DataRow In dt.Rows
                retVal = True
                error_num = Generic.ToInt(row("Error_num"))
                If error_num > 0 Then
                    error_message = Generic.ToStr(row("ErrorMessage"))
                    MessageBox.Critical(error_message, Me)
                    retVal = False
                End If

            Next
            If retVal = False And error_message = "" Then
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
            If retVal = True Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            'Dim ib As New LinkButton
            'ib = sender
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo", "FullName"})
            Generic.ClearControls(Me, "pnlPopupCopy")
            ViewState("EmployeeNoTo") = Generic.ToInt(obj(0))
            Try
                cboEmployeeFromNo.DataSource = SQLHelper.ExecuteDataSet("ETrnCurriculum_WebLookupFrom", UserNo, Generic.ToInt(obj(0)))
                cboEmployeeFromNo.DataValueField = "tNo"
                cboEmployeeFromNo.DataTextField = "tDesc"
                cboEmployeeFromNo.DataBind()
            Catch ex As Exception
            End Try

            mdlCopy.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkSaveCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If SaveRecordCopy() Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Private Function SaveRecordCopy() As Boolean
        Dim EmployeeToNo As Integer = ViewState("EmployeeNoTo") ' Generic.ToInt(cboEmployeeToNo.SelectedValue)
        Dim EmployeeFromNo As Integer = Generic.ToInt(cboEmployeeFromNo.SelectedValue)

        If SQLHelper.ExecuteNonQuery("ETrnCurriculum_WebSaveCopy", UserNo, EmployeeToNo, EmployeeFromNo) > 0 Then
            SaveRecordCopy = True
        Else
            SaveRecordCopy = False
        End If

    End Function
    Protected Sub btnAddR_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnltrn")
            Generic.PopulateDropDownList(UserNo, Me, "pnltrn", PayLocNo)
            mdtrn.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    Protected Sub lnkEditR_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New LinkButton
            ib = sender

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnCurriculumDetl_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
            For Each row As DataRow In dt.Rows
                Generic.ClearControls(Me, "pnltrn")
                Generic.PopulateData(Me, "pnltrn", dt)
                Generic.PopulateDropDownList(UserNo, Me, "pnltrn", PayLocNo)
            Next
            mdtrn.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub
    Protected Sub btnDeleteR_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim DeleteCount As Integer = 0
        TransNo = Generic.ToInt(ViewState("TransNo"))

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdTrn.GetSelectedFieldValues(New String() {"TrnCurriculumDetlNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ETrnCurriculumDetl", UserNo, item)
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateDetl()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowDelete), Me)

        End If
    End Sub

End Class

