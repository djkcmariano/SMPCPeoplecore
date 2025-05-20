Imports clsLib
Imports System.Data

Partial Class Secured_CliClinicDentalList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim IsEnabled As Boolean = False

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EClinicLocTooth_Web", UserNo, TransNo, Generic.ToBol(Session("IsDependent")))
            'If dt.Rows.Count > 0 Then

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
            'End If
        Catch ex As Exception

        End Try
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
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))

        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateGrid()
        End If

        PopulateTabHeader()
        EnabledControls()

    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EClinic_WebTabHeader", UserNo, TransNo, Generic.ToBol(Session("IsDependent")))
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next

        If Generic.ToBol(Session("IsDependent")) = False Then
            imgPhoto.Visible = True
            imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
        Else
            imgPhoto.Visible = False
        End If

    End Sub

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            ViewState("IsEnabled") = True
            EnabledControls()
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)

        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Private Function SaveRecord() As Boolean

        Try
            Dim lbl As New Label, tcheck As New CheckBox
            Dim tcount As Integer, DeleteCount As Integer = 0
            Dim lblType As New Label, txtOperationPerformed As New TextBox, txtToothCondition As New TextBox, txtDetails As New TextBox

            For tcount = 0 To Me.grdMain.Rows.Count - 1
                lbl = CType(grdMain.Rows(tcount).FindControl("lblID"), Label)
                lblType = CType(grdMain.Rows(tcount).FindControl("lblType"), Label)
                txtOperationPerformed = CType(grdMain.Rows(tcount).FindControl("txtOperationPerformed"), TextBox)
                txtDetails = CType(grdMain.Rows(tcount).FindControl("txtDetails"), TextBox)
                txtToothCondition = CType(grdMain.Rows(tcount).FindControl("txtToothCondition"), TextBox)
                SQLHelper.ExecuteNonQuery("EClinicLocTooth_WebSave", UserNo, Generic.ToInt(lbl.Text), TransNo, Generic.ToInt(lblType.Text), Generic.ToStr(txtToothCondition.Text), Generic.ToStr(txtOperationPerformed.Text), Generic.ToStr(txtDetails.Text), Generic.ToBol(Session("IsDependent")))

            Next
            PopulateGrid()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Protected Sub grdMain_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim txtTooth As New TextBox
            Dim txtOperation As New TextBox
            Dim txtDetails As New TextBox

            txtTooth = CType(e.Row.FindControl("txtToothCondition"), TextBox)
            txtOperation = (CType(e.Row.FindControl("txtOperationPerformed"), TextBox))
            txtDetails = CType(e.Row.FindControl("txtDetails"), TextBox)

            IsEnabled = Generic.ToBol(ViewState("IsEnabled"))

            txtDetails.Enabled = IsEnabled
            txtTooth.Enabled = IsEnabled
            txtOperation.Enabled = IsEnabled
        End If
    End Sub
End Class
