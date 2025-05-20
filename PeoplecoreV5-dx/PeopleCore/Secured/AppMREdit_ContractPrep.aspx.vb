Imports clsLib
Imports System.Data

Partial Class Secured_AppMREdit_ContractPrep
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim ActionStatNo As Integer = 5
    Dim dtVal As New DataTable

    Protected Sub PopulateGrid()
        'Try
        Dim dt As DataTable
        Dim sortDirection As String = "", sortExpression As String = ""
        dt = SQLHelper.ExecuteDataTable("EMRHiredMass_Web", UserNo, TransNo, Filter1.SearchText, ActionStatNo)
        dtVal = dt
        Dim dv As DataView = dt.DefaultView
        If ViewState("SortDirection") IsNot Nothing Then
            sortDirection = ViewState("SortDirection").ToString()
        End If
        If ViewState("SortExpression") IsNot Nothing Then
            sortExpression = ViewState("SortExpression").ToString()
            dv.Sort = String.Concat(sortExpression, " ", sortDirection)
        End If
        grdMain.DataSource = dv
        grdMain.DataBind()
        'Catch ex As Exception

        'End Try
    End Sub

    Protected Sub grdMain_Sorting(sender As Object, e As GridViewSortEventArgs)
        Try
            If ViewState("SortDirection") Is Nothing OrElse ViewState("SortExpression").ToString() <> e.SortExpression Then
                ViewState("SortDirection") = "ASC"
            ElseIf ViewState("SortDirection").ToString() = "ASC" Then
                ViewState("SortDirection") = "DESC"
            ElseIf ViewState("SortDirection").ToString() = "DESC" Then
                ViewState("SortDirection") = "ASC"
            End If
            ViewState("SortExpression") = e.SortExpression
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            grdMain.PageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))

        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateTabHeader()
            PopulateGrid()
        End If
        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub
    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New ImageButton
            ib = sender
            Dim fmrhiredmassno As Integer = CType(ib.CommandArgument, Integer)
            'hifEmployeeNo.Value = fmrhiredmassno
            Dim dt As DataTable
            Dim IsEnabled As Boolean = False
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            Try
                cboPlantillaNo.DataSource = SQLHelper.ExecuteDataSet("EMRPlantilla_WebLookup", UserNo, TransNo, fmrhiredmassno)
                cboPlantillaNo.DataValueField = "tno"
                cboPlantillaNo.DataTextField = "tcode"
                cboPlantillaNo.DataBind()
            Catch ex As Exception
            End Try


            dt = SQLHelper.ExecuteDataTable("EMRHiredMass_WebOne", UserNo, Generic.ToInt(fmrhiredmassno))
            For Each row As DataRow In dt.Rows
                Try
                    cboHRANTypeNo.DataSource = SQLHelper.ExecuteDataSet("EMRHiredMass_HRANType_WebLookup", UserNo, Generic.ToInt(row("HRANTypeNo")), PayLocNo)
                    cboHRANTypeNo.DataValueField = "tNo"
                    cboHRANTypeNo.DataTextField = "tDesc"
                    cboHRANTypeNo.DataBind()
                Catch ex As Exception
                End Try

                Try
                    cboPayClassNo.DataSource = SQLHelper.ExecuteDataSet("EMRHiredMass_PayClass_WebLookup", UserNo, Generic.ToInt(row("PayClassNo")), PayLocNo)
                    cboPayClassNo.DataValueField = "tNo"
                    cboPayClassNo.DataTextField = "tDesc"
                    cboPayClassNo.DataBind()
                Catch ex As Exception
                End Try

                Generic.PopulateData(Me, "Panel1", dt)
                IsEnabled = Not Generic.ToBol(row("IsPosted"))
            Next

            Generic.EnableControls(Me, "Panel1", IsEnabled)
            lnkSave.Enabled = IsEnabled
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For i As Integer = 0 To Me.grdMain.Rows.Count - 1
                chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
                ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
                If chk.Checked = True Then
                    Generic.DeleteRecordAudit("EMRHiredMass", UserNo, Generic.ToInt(ib.CommandArgument))
                    Count = Count + 1
                End If
            Next
            MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMR_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lnk_Click(sender As Object, e As EventArgs)
        'Info1.xIsApplicant = True
        Dim lnk As New LinkButton
        lnk = sender

        Info1.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
        Info1.xIsApplicant = Generic.ToBol(Generic.Split(lnk.CommandArgument, 1))
        Info1.Show()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim RetVal As Boolean = False
        Dim PlantillaNo As Integer = Generic.ToInt(cboPlantillaNo.SelectedValue)
        Dim HRANTypeNo As Integer = Generic.ToInt(cboHRANTypeNo.SelectedValue)
        Dim PayClassNo As Integer = Generic.ToInt(cboPayClassNo.SelectedValue)
        Dim IsReviewed As Boolean = Generic.ToBol(txtIsReviewed.Checked)
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EMRHiredMass_WebSave_PlantillaValidate", UserNo, hifMRHiredMassNo.Value, PlantillaNo, HRANTypeNo, PayClassNo, IsReviewed)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EMRHiredMass_WebSave_Plantilla", UserNo, hifMRHiredMassNo.Value, PlantillaNo, HRANTypeNo, PayClassNo, IsReviewed) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub


    'Submit record
    Protected Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lbl As New Label, chk As New CheckBox, lblProceed As New Label
        Dim tcount As Integer, SaveCount As Integer = 0
        Dim xds As New DataSet
        Dim hifPlantillaNo As New HiddenField
        Dim hifHRANTypeNo As New HiddenField
        Dim dt As New DataTable, fullname As String = "", tProceed As Integer = 0, i As Integer = 0
        Dim MessageStr1 As String = "", MessageStr2 As String = "", MessageStr3 As String = "", MessageStr4 As String = ""

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then

            For tcount = 0 To Me.grdMain.Rows.Count - 1
                lbl = CType(grdMain.Rows(tcount).FindControl("lblNo"), Label)
                chk = CType(grdMain.Rows(tcount).FindControl("txtIsSelect"), CheckBox)
                Dim MRHiredMassNo As Integer = Generic.ToInt(lbl.Text)

                If Generic.ToBol(chk.Checked) = True Then
                    dt = SQLHelper.ExecuteDataTable("EMR_WebForward", UserNo, MRHiredMassNo, TransNo, PayLocNo)
                    For Each row As DataRow In dt.Rows
                        fullname = Generic.ToStr(row("Pending"))
                        tProceed = Generic.ToInt(row("tProceed"))
                    Next

                    If tProceed = 0 Then
                        If MessageStr1 <> "" Then
                            MessageStr1 = MessageStr1 & ", " & fullname
                        Else
                            MessageStr1 = fullname
                        End If
                    ElseIf tProceed = 2 Then
                        If MessageStr2 <> "" Then
                            MessageStr2 = MessageStr2 & ", " & fullname
                        Else
                            MessageStr2 = fullname
                        End If
                    ElseIf tProceed = 3 Then
                        If MessageStr3 <> "" Then
                            MessageStr3 = MessageStr3 & ", " & fullname
                        Else
                            MessageStr3 = fullname
                        End If
                    Else
                        SaveCount = SaveCount + 1
                    End If

                    i = i + 1
                End If
            Next


            If i > 0 Then
                If SaveCount > 0 Then
                    PopulateGrid()
                    MessageBox.Success("There are " + SaveCount.ToString + " applicant(s) posted to HRAN!", Me)
                End If

                If MessageStr1 <> "" Then
                    MessageBox.Alert("MR is already served the vacant headcount", "information", Me)
                End If

                If MessageStr2 <> "" Then
                    MessageBox.Alert("List of applicant(s) have pending HRAN transaction: " + MessageStr2.ToString + "", "warning", Me)
                End If

                If MessageStr3 <> "" Then
                    MessageBox.Alert("Please review the other informations of following applicant(s): " + MessageStr3.ToString + "", "warning", Me)
                End If

            Else
                MessageBox.Alert(MessageTemplate.NoSelectedTransaction, "warning", Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If

    End Sub

    Protected Sub grdMain_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMain.RowDataBound

        'Dim cboHRANTypeNo As New DropDownList
        'Dim cboPlantillaNo As New DropDownList
        'Dim dt As New DataTable

        'dt = dtVal
        'For Each row As DataRow In dt.Rows
        '    For tcount = 0 To Me.grdMain.Rows.Count - 1
        '        cboHRANTypeNo = CType(grdMain.Rows(tcount).FindControl("cboHRANTypeNo"), DropDownList)
        '        cboHRANTypeNo.Text = Generic.ToStr(dt.Rows(tcount)("HRANTypeNo"))

        '        cboPlantillaNo = CType(grdMain.Rows(tcount).FindControl("cboPlantillaNo"), DropDownList)
        '        cboPlantillaNo.Text = Generic.ToStr(dt.Rows(tcount)("PlantillaNo"))

        '        Try
        '            cboHRANTypeNo.DataSource = SQLHelper.ExecuteDataSet("EHRAN_WebLookup_New", UserNo, 0, PayLocNo)
        '            cboHRANTypeNo.DataValueField = "tNo"
        '            cboHRANTypeNo.DataTextField = "tDesc"
        '            cboHRANTypeNo.DataBind()
        '        Catch ex As Exception

        '        End Try

        '        Try
        '            cboPlantillaNo.DataSource = SQLHelper.ExecuteDataSet("EMRPlantilla_WebLookup", UserNo, TransNo)
        '            cboPlantillaNo.DataValueField = "tno"
        '            cboPlantillaNo.DataTextField = "tcode"
        '            cboPlantillaNo.DataBind()
        '        Catch ex As Exception

        '        End Try
        '    Next
        'Next

        
    End Sub
End Class
