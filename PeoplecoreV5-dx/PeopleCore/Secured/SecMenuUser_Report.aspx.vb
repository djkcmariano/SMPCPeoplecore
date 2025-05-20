Imports clsLib
Imports System.Data

Partial Class Secured_SecMenuUser_Report
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim MenuUserNo As Int64 = 0
    Dim MenuMassNo As Int64 = 0
    Dim MenuMassDesc As String = ""
    Dim MenuUserDesc As String = ""

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("EMenuUserReport_Web", UserNo, MenuUserNo, MenuMassNo, Filter1.SearchText, PayLocNo)
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

        Catch ex As Exception

        End Try
    End Sub

    'Populate Combo box
    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("SMenuUser_WebTabHeader", UserNo, MenuUserNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
                imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(row("EmployeeNo")) & "&tIndex=2"
            Next

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        MenuUserNo = Generic.ToInt(Request.QueryString("id"))
        MenuMassNo = Generic.ToInt(Request.QueryString("MenuMassNo"))
        AccessRights.CheckUser(UserNo, "SecMenuUser.aspx", "SMenuUser")
        If Not IsPostBack Then
            PopulateGrid()
            PopulateTabHeader()
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

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "SecMenuUser.aspx", "SMenuUser") Then
            Dim Retval As Boolean = False, Count As Integer = 0
            Dim txtIsSelect As New CheckBox
            Dim lblId As New Label
            Dim lblReportNo As New Label
            Dim txtViewed As New CheckBox

            For i As Integer = 0 To grdMain.Rows.Count - 1
                lblId = CType(grdMain.Rows(i).FindControl("lblId"), Label)
                lblReportNo = CType(grdMain.Rows(i).FindControl("lblReportNo"), Label)
                txtViewed = CType(grdMain.Rows(i).FindControl("chkIsEnabled"), CheckBox)

                Dim tno As Integer = Generic.ToInt(lblId.Text)
                Dim j As Integer = Generic.ToInt(lblReportNo.Text)
                Dim IsViewed As Boolean = Generic.ToBol(txtViewed.Checked)

                SQLHelper.ExecuteNonQuery("EMenuUserReport_WebSave", UserNo, tno, j, MenuUserNo, MenuMassNo, IsViewed)
                Retval = True
            Next

            If Retval Then
                PopulateGrid()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

End Class

















