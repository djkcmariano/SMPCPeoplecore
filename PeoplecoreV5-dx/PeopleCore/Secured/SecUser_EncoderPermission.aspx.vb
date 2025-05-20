Imports clsLib
Imports System.Data

Partial Class Secured_SecUser_EncoderPermission
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim xUserNo As Int64 = 0
    Dim xPayLocNo As Int64 = 0

    Protected Sub PopulateGrid1()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("EUserGrantedDivision_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter1.SearchText))
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

            dt = SQLHelper.ExecuteDataTable("EUserGrantedDepartment_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter2.SearchText))
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

            dt = SQLHelper.ExecuteDataTable("EUserGrantedSection_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter3.SearchText))
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

            dt = SQLHelper.ExecuteDataTable("EUserGrantedUnit_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter4.SearchText))
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

            dt = SQLHelper.ExecuteDataTable("EUserGrantedPosition_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter5.SearchText))
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

            dt = SQLHelper.ExecuteDataTable("EUserGrantedHRANType_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter6.SearchText))
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

    Protected Sub PopulateGrid7()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("EUserGrantedFacility_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter7.SearchText))
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            grdMain7.DataSource = dv
            grdMain7.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateGrid8()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("EUserGrantedGroup_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter8.SearchText))
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            grdMain8.DataSource = dv
            grdMain8.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateGrid9()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("EUserGrantedLocation_Web", UserNo, xUserNo, xPayLocNo, Generic.ToStr(Filter9.SearchText))
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            grdMain9.DataSource = dv
            grdMain9.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        xUserNo = Generic.ToInt(Request.QueryString("UserNo"))
        xPayLocNo = Generic.ToInt(Request.QueryString("PayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateGrid1()
            PopulateGrid2()
            PopulateGrid3()
            PopulateGrid4()
            PopulateGrid5()
            PopulateGrid6()
            PopulateGrid7()
            PopulateGrid8()
            PopulateGrid9()

        End If

        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch1_Click
        AddHandler Filter2.lnkSearchClick, AddressOf lnkSearch2_Click
        AddHandler Filter3.lnkSearchClick, AddressOf lnkSearch3_Click
        AddHandler Filter4.lnkSearchClick, AddressOf lnkSearch4_Click
        AddHandler Filter5.lnkSearchClick, AddressOf lnkSearch5_Click
        AddHandler Filter6.lnkSearchClick, AddressOf lnkSearch6_Click
        AddHandler Filter7.lnkSearchClick, AddressOf lnkSearch7_Click
        AddHandler Filter8.lnkSearchClick, AddressOf lnkSearch8_Click
        AddHandler Filter9.lnkSearchClick, AddressOf lnkSearch9_Click

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
    Protected Sub lnkSearch7_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid7()
    End Sub
    Protected Sub lnkSearch8_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid8()
    End Sub
    Protected Sub lnkSearch9_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid9()
    End Sub


    Protected Sub btnSave1_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False, Count As Integer = 0
            Dim lblTransNo As New Label, lblNo As New Label, txtViewed As New CheckBox, txtEdited As New CheckBox

            For i As Integer = 0 To grdMain1.Rows.Count - 1
                lblTransNo = CType(grdMain1.Rows(i).FindControl("lblTransNo"), Label)
                lblNo = CType(grdMain1.Rows(i).FindControl("lblNo"), Label)
                'txtEdited = CType(grdMain1.Rows(i).FindControl("txtIsSelect1"), CheckBox)
                txtViewed = CType(grdMain1.Rows(i).FindControl("txtIsSelect2"), CheckBox)

                Dim tno As Integer = Generic.ToInt(lblTransNo.Text)
                Dim j As Integer = Generic.ToInt(lblNo.Text)
                Dim IsEdited As Boolean = False 'Generic.ToBol(txtEdited.Checked)
                Dim IsViewed As Boolean = Generic.ToBol(txtViewed.Checked)

                SQLHelper.ExecuteNonQuery("EUserGrantedDivision_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
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
                'txtEdited = CType(grdMain2.Rows(i).FindControl("txtIsSelect1"), CheckBox)
                txtViewed = CType(grdMain2.Rows(i).FindControl("txtIsSelect2"), CheckBox)

                Dim tno As Integer = Generic.ToInt(lblTransNo.Text)
                Dim j As Integer = Generic.ToInt(lblNo.Text)
                Dim IsEdited As Boolean = False 'Generic.ToBol(txtEdited.Checked)
                Dim IsViewed As Boolean = Generic.ToBol(txtViewed.Checked)

                SQLHelper.ExecuteNonQuery("EUserGrantedDepartment_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
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
                'txtEdited = CType(grdMain3.Rows(i).FindControl("txtIsSelect1"), CheckBox)
                txtViewed = CType(grdMain3.Rows(i).FindControl("txtIsSelect2"), CheckBox)

                Dim tno As Integer = Generic.ToInt(lblTransNo.Text)
                Dim j As Integer = Generic.ToInt(lblNo.Text)
                Dim IsEdited As Boolean = False 'Generic.ToBol(txtEdited.Checked)
                Dim IsViewed As Boolean = Generic.ToBol(txtViewed.Checked)

                SQLHelper.ExecuteNonQuery("EUserGrantedSection_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
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

                SQLHelper.ExecuteNonQuery("EUserGrantedUnit_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
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

                SQLHelper.ExecuteNonQuery("EUserGrantedPosition_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
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
                txtEdited = CType(grdMain6.Rows(i).FindControl("txtIsSelect1"), CheckBox)
                txtViewed = CType(grdMain6.Rows(i).FindControl("txtIsSelect2"), CheckBox)

                Dim tno As Integer = Generic.ToInt(lblTransNo.Text)
                Dim j As Integer = Generic.ToInt(lblNo.Text)
                Dim IsEdited As Boolean = Generic.ToBol(txtEdited.Checked)
                Dim IsViewed As Boolean = Generic.ToBol(txtViewed.Checked)

                SQLHelper.ExecuteNonQuery("EUserGrantedHRANType_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
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

    Protected Sub btnSave7_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False, Count As Integer = 0
            Dim lblTransNo As New Label, lblNo As New Label, txtViewed As New CheckBox, txtEdited As New CheckBox

            For i As Integer = 0 To grdMain7.Rows.Count - 1
                lblTransNo = CType(grdMain7.Rows(i).FindControl("lblTransNo"), Label)
                lblNo = CType(grdMain7.Rows(i).FindControl("lblNo"), Label)
                'txtEdited = CType(grdMain5.Rows(i).FindControl("txtIsSelect1"), CheckBox)
                txtViewed = CType(grdMain7.Rows(i).FindControl("txtIsSelect2"), CheckBox)

                Dim tno As Integer = Generic.ToInt(lblTransNo.Text)
                Dim j As Integer = Generic.ToInt(lblNo.Text)
                Dim IsEdited As Boolean = False 'Generic.ToBol(txtEdited.Checked)
                Dim IsViewed As Boolean = Generic.ToBol(txtViewed.Checked)

                SQLHelper.ExecuteNonQuery("EUserGrantedFacility_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
                Retval = True
            Next

            If Retval Then
                PopulateGrid7()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub btnSave8_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False, Count As Integer = 0
            Dim lblTransNo As New Label, lblNo As New Label, txtViewed As New CheckBox, txtEdited As New CheckBox

            For i As Integer = 0 To grdMain8.Rows.Count - 1
                lblTransNo = CType(grdMain8.Rows(i).FindControl("lblTransNo"), Label)
                lblNo = CType(grdMain8.Rows(i).FindControl("lblNo"), Label)
                'txtEdited = CType(grdMain5.Rows(i).FindControl("txtIsSelect1"), CheckBox)
                txtViewed = CType(grdMain8.Rows(i).FindControl("txtIsSelect2"), CheckBox)

                Dim tno As Integer = Generic.ToInt(lblTransNo.Text)
                Dim j As Integer = Generic.ToInt(lblNo.Text)
                Dim IsEdited As Boolean = False 'Generic.ToBol(txtEdited.Checked)
                Dim IsViewed As Boolean = Generic.ToBol(txtViewed.Checked)

                SQLHelper.ExecuteNonQuery("EUserGrantedGroup_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
                Retval = True
            Next

            If Retval Then
                PopulateGrid8()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub btnSave9_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim Retval As Boolean = False, Count As Integer = 0
            Dim lblTransNo As New Label, lblNo As New Label, txtViewed As New CheckBox, txtEdited As New CheckBox

            For i As Integer = 0 To grdMain9.Rows.Count - 1
                lblTransNo = CType(grdMain9.Rows(i).FindControl("lblTransNo"), Label)
                lblNo = CType(grdMain9.Rows(i).FindControl("lblNo"), Label)                
                txtViewed = CType(grdMain9.Rows(i).FindControl("txtIsSelect2"), CheckBox)

                Dim tno As Integer = Generic.ToInt(lblTransNo.Text)
                Dim j As Integer = Generic.ToInt(lblNo.Text)
                Dim IsEdited As Boolean = False 'Generic.ToBol(txtEdited.Checked)
                Dim IsViewed As Boolean = Generic.ToBol(txtViewed.Checked)

                SQLHelper.ExecuteNonQuery("EUserGrantedLocation_WebSave", UserNo, tno, xUserNo, j, IsEdited, IsViewed, xPayLocNo)
                Retval = True
            Next

            If Retval Then
                PopulateGrid9()
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

End Class

















