Imports clsLib
Imports System.Data

Partial Class Secured_SecUser_UserPermission
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim xUserNo As Int64 = 0
    Dim xPayLocNo As Int64 = 0
    Dim xUsername As String = ""

    Protected Sub PopulateGrid1()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("EUserGrantedClass_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter1.SearchText))
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            grdMain1.DataSource = dv
            grdMain1.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateGrid2()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("EUserGrantedRate_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter2.SearchText))
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            grdMain2.DataSource = dv
            grdMain2.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateGrid3()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("EUserGrantedHRANType_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter3.SearchText))
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            grdMain3.DataSource = dv
            grdMain3.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateGrid4()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("EUserGrantedRequestType_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter4.SearchText))
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            grdMain4.DataSource = dv
            grdMain4.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateGrid5()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("EUserGrantedIncomeType_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter5.SearchText))
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            grdMain5.DataSource = dv
            grdMain5.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateGrid6()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("EUserGrantedDeductType_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter6.SearchText))
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            grdMain6.DataSource = dv
            grdMain6.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        xUserNo = Generic.ToInt(Request.QueryString("UserNo"))
        xUsername = Generic.ToStr(Request.QueryString("Username"))
        xPayLocNo = Generic.ToInt(Request.QueryString("PayLocNo"))
        If PayLocNo = 0 Then
            xPayLocNo = PayLocNo
        End If
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateGrid1()
            PopulateGrid2()
            PopulateGrid3()
            PopulateGrid4()
            PopulateGrid5()
            PopulateGrid6()
            lblDetl.Text = xUsername
        End If

        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch1_Click
        AddHandler Filter2.lnkSearchClick, AddressOf lnkSearch2_Click
        AddHandler Filter3.lnkSearchClick, AddressOf lnkSearch3_Click
        AddHandler Filter4.lnkSearchClick, AddressOf lnkSearch4_Click
        AddHandler Filter5.lnkSearchClick, AddressOf lnkSearch5_Click
        AddHandler Filter6.lnkSearchClick, AddressOf lnkSearch6_Click

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub
    Protected Sub lnkSearch1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid1()
    End Sub
    Protected Sub lnkSearch2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid2()
    End Sub
    Protected Sub lnkSearch3_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid3()
    End Sub
    Protected Sub lnkSearch4_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid4()
    End Sub
    Protected Sub lnkSearch5_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid5()
    End Sub
    Protected Sub lnkSearch6_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid6()
    End Sub

    Protected Sub btnSave1_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False, Count As Integer = 0
            Dim lblTransNo As New Label, lblNo As New Label, txtViewed As New CheckBox, txtEdited As New CheckBox

            For i As Integer = 0 To grdMain1.Rows.Count - 1
                lblTransNo = CType(grdMain1.Rows(i).FindControl("lblTransNo"), Label)
                lblNo = CType(grdMain1.Rows(i).FindControl("lblNo"), Label)
                txtEdited = CType(grdMain1.Rows(i).FindControl("txtIsSelect1"), CheckBox)
                txtViewed = CType(grdMain1.Rows(i).FindControl("txtIsSelect2"), CheckBox)

                Dim tno As Integer = Generic.ToInt(lblTransNo.Text)
                Dim j As Integer = Generic.ToInt(lblNo.Text)
                Dim IsEdited As Boolean = Generic.ToBol(txtEdited.Checked)
                Dim IsViewed As Boolean = Generic.ToBol(txtViewed.Checked)

                SQLHelper.ExecuteNonQuery("EUserGrantedClass_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
                Retval = True
            Next

            If Retval Then
                PopulateGrid1()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub btnSave2_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False, Count As Integer = 0
            Dim lblTransNo As New Label, lblNo As New Label, txtViewed As New CheckBox, txtEdited As New CheckBox

            For i As Integer = 0 To grdMain2.Rows.Count - 1
                lblTransNo = CType(grdMain2.Rows(i).FindControl("lblTransNo"), Label)
                lblNo = CType(grdMain2.Rows(i).FindControl("lblNo"), Label)
                txtEdited = CType(grdMain2.Rows(i).FindControl("txtIsSelect1"), CheckBox)
                txtViewed = CType(grdMain2.Rows(i).FindControl("txtIsSelect2"), CheckBox)

                Dim tno As Integer = Generic.ToInt(lblTransNo.Text)
                Dim j As Integer = Generic.ToInt(lblNo.Text)
                Dim IsEdited As Boolean = Generic.ToBol(txtEdited.Checked)
                Dim IsViewed As Boolean = Generic.ToBol(txtViewed.Checked)

                SQLHelper.ExecuteNonQuery("EUserGrantedRate_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
                Retval = True
            Next

            If Retval Then
                PopulateGrid2()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub btnSave3_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False, Count As Integer = 0
            Dim lblTransNo As New Label, lblNo As New Label, txtViewed As New CheckBox, txtEdited As New CheckBox

            For i As Integer = 0 To grdMain3.Rows.Count - 1
                lblTransNo = CType(grdMain3.Rows(i).FindControl("lblTransNo"), Label)
                lblNo = CType(grdMain3.Rows(i).FindControl("lblNo"), Label)
                txtEdited = CType(grdMain3.Rows(i).FindControl("txtIsSelect1"), CheckBox)
                txtViewed = CType(grdMain3.Rows(i).FindControl("txtIsSelect2"), CheckBox)

                Dim tno As Integer = Generic.ToInt(lblTransNo.Text)
                Dim j As Integer = Generic.ToInt(lblNo.Text)
                Dim IsEdited As Boolean = Generic.ToBol(txtEdited.Checked)
                Dim IsViewed As Boolean = Generic.ToBol(txtViewed.Checked)

                SQLHelper.ExecuteNonQuery("EUserGrantedHRANType_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
                Retval = True
            Next

            If Retval Then
                PopulateGrid3()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub btnSave4_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False, Count As Integer = 0
            Dim lblTransNo As New Label, lblNo As New Label, txtViewed As New CheckBox, txtEdited As New CheckBox

            For i As Integer = 0 To grdMain4.Rows.Count - 1
                lblTransNo = CType(grdMain4.Rows(i).FindControl("lblTransNo"), Label)
                lblNo = CType(grdMain4.Rows(i).FindControl("lblNo"), Label)
                'txtEdited = CType(grdMain4.Rows(i).FindControl("txtIsSelect1"), CheckBox)
                txtViewed = CType(grdMain4.Rows(i).FindControl("txtIsSelect2"), CheckBox)

                Dim tno As Integer = Generic.ToInt(lblTransNo.Text)
                Dim j As Integer = Generic.ToInt(lblNo.Text)
                Dim IsEdited As Boolean = False 'Generic.ToBol(txtEdited.Checked)
                Dim IsViewed As Boolean = Generic.ToBol(txtViewed.Checked)

                SQLHelper.ExecuteNonQuery("EUserGrantedRequestType_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
                Retval = True
            Next

            If Retval Then
                PopulateGrid4()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub btnSave5_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False, Count As Integer = 0
            Dim lblTransNo As New Label, lblNo As New Label, txtViewed As New CheckBox, txtEdited As New CheckBox

            For i As Integer = 0 To grdMain5.Rows.Count - 1
                lblTransNo = CType(grdMain5.Rows(i).FindControl("lblTransNo"), Label)
                lblNo = CType(grdMain5.Rows(i).FindControl("lblNo"), Label)
                'txtEdited = CType(grdMain5.Rows(i).FindControl("txtIsSelect1"), CheckBox)
                txtViewed = CType(grdMain5.Rows(i).FindControl("txtIsSelect2"), CheckBox)

                Dim tno As Integer = Generic.ToInt(lblTransNo.Text)
                Dim j As Integer = Generic.ToInt(lblNo.Text)
                Dim IsEdited As Boolean = False 'Generic.ToBol(txtEdited.Checked)
                Dim IsViewed As Boolean = Generic.ToBol(txtViewed.Checked)

                SQLHelper.ExecuteNonQuery("EUserGrantedIncomeType_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
                Retval = True
            Next

            If Retval Then
                PopulateGrid5()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub btnSave6_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False, Count As Integer = 0
            Dim lblTransNo As New Label, lblNo As New Label, txtViewed As New CheckBox, txtEdited As New CheckBox

            For i As Integer = 0 To grdMain6.Rows.Count - 1
                lblTransNo = CType(grdMain6.Rows(i).FindControl("lblTransNo"), Label)
                lblNo = CType(grdMain6.Rows(i).FindControl("lblNo"), Label)
                'txtEdited = CType(grdMain6.Rows(i).FindControl("txtIsSelect1"), CheckBox)
                txtViewed = CType(grdMain6.Rows(i).FindControl("txtIsSelect2"), CheckBox)

                Dim tno As Integer = Generic.ToInt(lblTransNo.Text)
                Dim j As Integer = Generic.ToInt(lblNo.Text)
                Dim IsEdited As Boolean = False 'Generic.ToBol(txtEdited.Checked)
                Dim IsViewed As Boolean = Generic.ToBol(txtViewed.Checked)

                SQLHelper.ExecuteNonQuery("EUserGrantedDeductType_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
                Retval = True
            Next

            If Retval Then
                PopulateGrid6()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
End Class

















