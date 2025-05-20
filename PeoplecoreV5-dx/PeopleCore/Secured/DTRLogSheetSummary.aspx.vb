Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_DTRLogSheetSummary
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Private Sub PopulateGrid()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDTRLogSheet_WebOne", UserNo, Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0)), txtStartDate.Text, txtStartDate.Text)
        lbl.Text = txtFullName.Text
        hif.Value = hifEmployeeNo.Value
        grdMain.DataSource = dt
        grdMain.DataBind()
    End Sub

    Protected Sub lnkGenerate_Click(ByVal sender As Object, ByVal e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        Generic.ClearControls(Me, "Panel1")
        mdlDetl.Show()
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As EventArgs)

        If SaveRecord() > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As EventArgs)

        Dim dt As DataTable
        dt = ViewState("vsDTRLogSheet")
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            For i As Integer = 0 To Me.grdMain.Rows.Count - 1
                Dim chk As New CheckBox, hif As New HiddenField, hifID As New HiddenField, hifDate As New HiddenField, hifEmployeeNo As New HiddenField
                Dim hifDTRLogNo As New HiddenField, hifDTROTNo As New HiddenField
                chk = CType(grdMain.Rows(i).FindControl("chk"), CheckBox)
                hif = CType(grdMain.Rows(i).FindControl("hifxRow"), HiddenField)
                hifID = CType(grdMain.Rows(i).FindControl("hifID"), HiddenField)
                hifDate = CType(grdMain.Rows(i).FindControl("hifDate"), HiddenField)
                hifEmployeeNo = CType(grdMain.Rows(i).FindControl("hifEmployeeNo"), HiddenField)
                hifDTRLogNo = CType(grdMain.Rows(i).FindControl("hifDTRLogNo"), HiddenField)
                hifDTROTNo = CType(grdMain.Rows(i).FindControl("hifDTROTNo"), HiddenField)
                If chk.Checked Then
                    If Generic.ToInt(hifDTRLogNo.Value) > 0 Then
                        Generic.DeleteRecordAudit("EDTRLog", UserNo, Generic.ToInt(hifDTRLogNo.Value))                        
                    End If
                    If Generic.ToInt(hifDTROTNo.Value) > 0 Then
                        Generic.DeleteRecordAudit("EDTROT", UserNo, Generic.ToInt(hifDTROTNo.Value))
                    End If
                    'SQLHelper.ExecuteNonQuery("DELETE FROM EDTRStayin WHERE EmployeeNo=" & hifEmployeeNo.Value & " AND DTRDate='" & hifDate.Value & "'")
                    Dim dr As DataRow()
                    dr = dt.Select("xRow=" & hif.Value)
                    dr(0).Delete()
                End If
                ViewState("vsDTRLogSheet") = dt
            Next
            PopulateGridMass()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Request.QueryString("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
        End If

    End Sub

    Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        Try
            grdMain.PageIndex = e.NewPageIndex
            PopulateGridMass()
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
    '    If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
    '        Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsPosted"))
    '        e.Enabled = value
    '    End If
    'End Sub


#Region "Mass"

    Protected Sub lnkAddMass_Click(ByVal sender As Object, ByVal e As EventArgs)
        Generic.ClearControls(Me, "Panel2")
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnkGenerate1_Click(ByVal sender As Object, ByVal e As EventArgs)
        ViewState("vsDTRLogSheet") = Nothing
        PopulateGridMass()
    End Sub

    Protected Sub cboFilterBy_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        Dim FilterByNo As Integer = Generic.ToInt(cboFilterByAllNo.SelectedValue)
        AutoCompleteExtender1.CompletionSetCount = FilterByNo

        ModalPopupExtender1.Show()
    End Sub

    Protected Sub PopulateGridMass()
        PopulateViewState()
        grdMain.DataSource = ViewState("vsDTRLogSheet")
        grdMain.DataBind()
    End Sub

    Private Sub PopulateViewState()
        Dim dt As DataTable = Nothing
        'Dim FilterByNo As Integer = Generic.ToInt(Generic.Split(cboFilterBy.SelectedValue, 0))
        If (IsNothing(ViewState("vsDTRLogSheet"))) Then
            dt = SQLHelper.ExecuteDataTable("EDTRLogSheet_WebOne_MassManual", UserNo, Generic.ToInt(cboFilterByAllNo.SelectedValue), Generic.ToInt(hifFilterValueNo.Value), txtStartDate1.Text, txtEndDate1.Text, txtSearch.Text)
        Else
            dt = ViewState("vsDTRLogSheet")
        End If
        ViewState("vsDTRLogSheet") = dt
    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
        SaveRecord()
        ViewState("vsDTRLogSheet") = Nothing
        PopulateGridMass()
    End Sub


    Private Function SaveRecord() As Integer
        Dim dt As DataTable
        Dim count As Integer = 0
        dt = ViewState("vsDTRLogSheet")

        For i As Integer = 0 To Me.grdMain.Rows.Count - 1            
            Dim hifDate As New HiddenField
            Dim hifEmployeeNo As New HiddenField
            Dim cboDayTypeNo As New DropDownList            
            Dim txtWorkingHrs As New TextBox
            Dim txtOvt As New TextBox
            Dim txtOvt8 As New TextBox
            Dim txtNP As New TextBox
            Dim txtNP8 As New TextBox
            Dim cls As New clsBase.clsBaseLibrary

            hifDate = CType(grdMain.Rows(i).FindControl("hifDate"), HiddenField)
            hifEmployeeNo = CType(grdMain.Rows(i).FindControl("hifEmployeeNo"), HiddenField)
            cboDayTypeNo = CType(grdMain.Rows(i).FindControl("cboDayTypeNo"), DropDownList)
            txtWorkingHrs = CType(grdMain.Rows(i).FindControl("txtWorkingHrs"), TextBox)
            txtOvt = CType(grdMain.Rows(i).FindControl("txtOvt"), TextBox)
            txtOvt8 = CType(grdMain.Rows(i).FindControl("txtOvt8"), TextBox)
            txtNP = CType(grdMain.Rows(i).FindControl("txtNP"), TextBox)
            txtNP8 = CType(grdMain.Rows(i).FindControl("txtNP8"), TextBox)
            
            count = count + SQLHelper.ExecuteNonQuery("EDTRLogSheet_WebSaveManual", UserNo, 0, Generic.ToInt(hifEmployeeNo.Value), hifDate.Value, Generic.ToDec(txtWorkingHrs.Text), Generic.ToDec(txtOvt.Text), Generic.ToDec(txtOvt8.Text), Generic.ToDec(txtNP.Text), Generic.ToDec(txtNP8.Text), 2, Generic.ToDec(cboDayTypeNo.SelectedValue), lbl.Text)

        Next
        ViewState("vsDTRLogSheet") = Nothing
        PopulateGridMass()

        Return count

    End Function


    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function populateDataDropdown(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)
        Dim items As New List(Of String)()
        Dim _ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim clsbase As New clsBase.clsBaseLibrary
        Dim UserNo As Integer = 0, PayLocNo As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        PayLocNo = (HttpContext.Current.Session("xPayLocNo"))

        _ds = SQLHelper.ExecuteDataSet("EFilterBy_WebLookup_AC", UserNo, prefixText, PayLocNo, count)
        For Each row As DataRow In _ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("tDesc")),
                                Generic.ToStr(row("tNo")))
            items.Add(item)
        Next
        _ds.Dispose()
        Return items
    End Function
#End Region

    Protected Sub grdMain_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMain.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then

            Dim ddl As New DropDownList
            Dim hfPayClassNo As New HiddenField
            Dim hfDayTypeNo As New HiddenField
            Dim hfIsPosted As New HiddenField
            ddl = CType(e.Row.FindControl("cboDayTypeNo"), DropDownList)
            hfPayClassNo = CType(e.Row.FindControl("hifPayClassNo"), HiddenField)
            hfDayTypeNo = CType(e.Row.FindControl("hifDayTypeNo"), HiddenField)
            hfIsPosted = CType(e.Row.FindControl("hifIsPosted"), HiddenField)

            ddl.DataSource = SQLHelper.ExecuteDataSet("EDayType_WebLookup", Generic.ToInt(hfPayClassNo.Value))
            ddl.DataTextField = "tDesc"
            ddl.DataValueField = "tNo"
            ddl.DataBind()

            Try
                ddl.SelectedValue = hfDayTypeNo.Value
                ddl.Enabled = Not Generic.ToBol(hfDayTypeNo.Value)
            Catch ex As Exception

            End Try
            
            
        End If
    End Sub
End Class


