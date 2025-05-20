Imports clsLib
Imports System.Data

Partial Class Secured_Sample
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            Dim TabOrder As Integer = 0 'Generic.ToInt(cboTabNo.SelectedValue)

            dt = SQLHelper.ExecuteDataTable("EDTR_Web", UserNo, Filter1.SearchText, "", "", TabOrder, PayLocNo)
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


            rRef.DataSource = SQLHelper.ExecuteDataTable("SELECT a.MenuNo, a.MenuTitle, a.FormName, a.TableName, b.IsView ,MenuType " & _
                                                 "FROM EMenu a " & _
                                                 "INNER JOIN dbo.EMenu_Permission(-99, 1) b ON b.MenuNo = a.MenuNo " & _
                                                 "LEFT OUTER JOIN SMenuMass c ON c.MenuMassCode = LEFT(a.MenuType, 2) " & _
                                                 "LEFT OUTER JOIN EComponent d ON d.ComponentNo = c.ComponentNo " & _
                                                 "Where a.MenuStyle='Reference' And c.MenuMassCode='02' ORDER BY c.OrderBy, a.MenuType")
            rRef.DataBind()

        Catch ex As Exception

        End Try


    End Sub
    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 1)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception

        End Try


        Try
            cboPayClassNo.DataSource = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", UserNo, PayLocNo)
            cboPayClassNo.DataValueField = "tNo"
            cboPayClassNo.DataTextField = "tDesc"
            cboPayClassNo.DataBind()
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            PopulateDropDown()
            PopulateGrid()
        End If

        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub grdMain_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs)
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

    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            grdMain.PageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        PopulateGrid()

    End Sub
    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim ib As New ImageButton
            ib = sender
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDTR_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupMain", dt)
            Next
            mdlMain.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For i As Integer = 0 To Me.grdMain.Rows.Count - 1
                chk = CType(grdMain.Rows(i).FindControl("txtIsSelect"), CheckBox)
                ib = CType(grdMain.Rows(i).FindControl("btnEdit"), ImageButton)
                If chk.Checked = True Then
                    Generic.DeleteRecordAudit("EDTR", UserNo, Generic.ToInt(ib.CommandArgument))
                    Count = Count + 1
                End If
            Next

            If Count > 0 Then
                MessageBox.Success("(" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
                PopulateGrid()
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Retval As Boolean = False
        Dim DTRNo As Integer = Generic.ToInt(Me.txtDTRNo.Text)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("ESample_Validate")
        For Each row As DataRow In dt.Rows
            invalid = Generic.ToBol(row("Invalid"))
            messagedialog = Generic.ToStr(row("MessageDialog"))
            alerttype = Generic.ToStr(row("AlertType"))
        Next

        If invalid = True Then
            mdlMain.Show()
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If
        '//end here

        If SQLHelper.ExecuteNonQuery("EDTR_WebSave", UserNo, DTRNo, Generic.ToInt(Session("xPayLocNo"))) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub


End Class

















