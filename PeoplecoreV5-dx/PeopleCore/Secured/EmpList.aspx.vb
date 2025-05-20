Imports System.Data
Imports clsLib
Imports System.IO

Partial Class Secured_EmpList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim EmployeeNo As Integer

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EEmployee_Web", UserNo, Filter1.SearchText, Generic.ToInt(cboTabNo.SelectedValue), Generic.ToInt(cbofilterby.SelectedValue), Generic.ToInt(cbofiltervalue.SelectedValue), PayLocNo)
        Dim dv As New Data.DataView(_dt)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim URL As String
        lnk = sender
        URL = Generic.GetFirstTab(lnk.CommandArgument)
        If URL <> "" Then
            Response.Redirect(URL)
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateDropDown()
            PopulateGrid()
        End If
        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", Generic.ToInt(Session("OnlineUserNo")), 6)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim URL As String
            URL = Generic.GetFirstTab("0")
            If URL <> "" Then
                Response.Redirect(URL)
            End If
        End If
    End Sub

    Protected Sub grdMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            grdMain.PageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkPhoto_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            hifEmployeeNo.Value = Generic.ToInt(lnk.CommandArgument)
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If fuPhoto.HasFile Then

            Dim filePath As String = fuPhoto.PostedFile.FileName
            Dim filename As String = Path.GetFileName(filePath)
            Dim fileext As String = Path.GetExtension(filename)
            Dim contenttype As String = String.Empty

            Select Case fileext.ToLower()
                Case ".jpg"
                    contenttype = "image/jpg"
                Case ".jpeg"
                    contenttype = "image/jpeg"
                Case ".png"
                    contenttype = "image/png"
                Case ".gif"
                    contenttype = "image/gif"
            End Select

            If (contenttype <> String.Empty) Then
                Dim fs As Stream = fuPhoto.PostedFile.InputStream
                Dim br As New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                If SQLHelper.ExecuteNonQuery("EEmployee_WebPhotoUpdate", Generic.ToInt(hifEmployeeNo.Value), bytes) > 0 Then
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                Else
                    MessageBox.Critical(MessageTemplate.ErrorSave, Me)
                End If
            Else
                MessageBox.Warning("Invalid file type.", Me)
            End If

        End If
    End Sub


    Protected Sub lnkAttachment_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Response.Redirect("~/secured/frmFileUpload.aspx?id=" & Generic.Split(lnk.CommandArgument, 0) & "&display=" & Generic.Split(lnk.CommandArgument, 1))
    End Sub

    'Dim xPublicVar As New clsPublicVariable
    'Dim tstatus As Integer
    'Dim dscount As Double = 0
    'Dim _ds As New DataSet
    'Dim _dt As New DataTable
    'Dim xScript As String = ""
    'Dim rowno As Integer = 0
    'Dim employeeno As Integer = 0
    'Dim IsClickMain As Integer = 0
    'Dim lnkCriteria As New Button

    'Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
    '    'Dim _dr As SqlClient.SqlDataReader

    '    'If tstatus = 0 Then
    '    '    tstatus = 1
    '    'End If
    '    tstatus = Generic.ToInt(cboTabNo.SelectedValue)
    '    If tstatus = 0 Then
    '        tstatus = 1
    '    End If
    '    dscount = 0
    '    _ds = SQLHelper.ExecuteDataset("EEmployee_Web", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(ViewState(xScript & "filter"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType), tstatus, Generic.CheckDBNull(ViewState(xScript & "filterby"), clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ViewState(xScript & "filtervalue"), clsBase.clsBaseLibrary.enumObjectType.IntType), Session("xPayLocNo"))
    '    _dt = _ds.Tables(0)
    '    Dim dv As New Data.DataView(_dt)
    '    If SortExp <> "" Then
    '        ViewState(xScript & "SortExp") = SortExp
    '    End If
    '    If sordir <> "" Then

    '        ViewState(xScript & "sortdir") = sordir
    '    End If
    '    If _ds.Tables.Count > 0 Then
    '        If _ds.Tables(0).Rows.Count > 0 Then
    '            dscount = _ds.Tables(0).Rows.Count
    '            If ViewState(xScript & "SortExp") <> "" Then
    '                dv.Sort = ViewState(xScript & "SortExp") + ViewState(xScript & "sortdir")
    '            End If
    '        End If
    '    End If
    '    If IsMain Then
    '        ViewState(xScript & "Pageno") = 0
    '        ViewState(Left(xScript, Len(xScript) - 5)) = 0
    '    End If

    '    Me.grdMain.SelectedIndex = Generic.CheckDBNull(ViewState(Left(xScript, Len(xScript) - 5)), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '    Me.grdMain.PageIndex = ViewState(xScript & "PageNo")
    '    Me.grdMain.DataSource = dv
    '    Me.grdMain.DataBind()

    '    cbofilterby.Text = IIf(ViewState(xScript & "filterby") = "0", "", ViewState(xScript & "filterby"))
    '    cbofiltervalue.Text = IIf(ViewState(xScript & "filterby") = "0", "", ViewState(xScript & "filtervalue"))


    'End Sub

    'Private Sub PopulateCombo()
    '    Try
    '        cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", xPublicVar.xOnlineUseNo, 6)
    '        cboTabNo.DataValueField = "tNo"
    '        cboTabNo.DataTextField = "tDesc"
    '        cboTabNo.DataBind()

    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Private Sub populateFilterBy()
    '    Try
    '        cbofilterby.DataSource = SQLHelper.ExecuteDataSet("xTable_Lookup", xpublicVar.xOnlineUseNo, "EFilteredBy", Session("xPayLocNo"), "", "")
    '        cbofilterby.DataTextField = "tDesc"
    '        cbofilterby.DataValueField = "tno"
    '        cbofilterby.DataBind()

    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Protected Sub cbofilterby_SelectedIndexChanged(sender As Object, e As System.EventArgs) 'Handles cbofilterby.SelectedIndexChanged
    '    Try

    '        Dim clsGen As New clsGenericClass
    '        Dim ds As DataSet
    '        ds = clsGen.populateDropdownFilterByAll(Generic.CheckDBNull(Me.cbofilterby.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType), xpublicVar.xOnlineUseNo, Session("xPayLocNo"))
    '        cbofiltervalue.DataSource = ds
    '        cbofiltervalue.DataTextField = "tDesc"
    '        cbofiltervalue.DataValueField = "tNo"
    '        cbofiltervalue.DataBind()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    '    xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("OnlineUserNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '    AccessRights.CheckUser(xPublicVar.xOnlineUseNo)
    '    AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

    '    employeeno = Generic.CheckDBNull(Request.QueryString("employeeno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '    tstatus = Generic.CheckDBNull(Request.QueryString("eStatus"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
    '    IsClickMain = Generic.CheckDBNull(Request.QueryString("IsClickMain"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

    '    xScript = Request.ServerVariables("SCRIPT_NAME")
    '    xScript = Generic.GetPath(xScript)


    '    If Not IsPostBack Then

    '        PopulateGrid()
    '        PopulateCombo()
    '        populateFilterBy()
    '    End If
    '    Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    'End Sub
    'Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        'ViewState(xScript & "PageNo") = 0

    '        'ViewState(xScript & "filter") = Generic.CheckDBNull(Filter1.SearchText.ToString, clsBase.clsBaseLibrary.enumObjectType.StrType)
    '        'ViewState(xScript & "filterby") = Generic.CheckDBNull(cbofilterby.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
    '        'ViewState(xScript & "filtervalue") = Generic.CheckDBNull(cbofiltervalue.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
    '        'ViewState(Left(xScript, Len(xScript) - 5)) = 0
    '        PopulateGrid()
    '        'ViewState(xScript & "No") = grdMain.DataKeys(0).Values(0).ToString()
    '    Catch ex As Exception
    '    End Try

    'End Sub



    'Protected Sub grdMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMain.PageIndexChanging
    '    ViewState(xScript & "No") = 0
    '    ViewState(xScript & "PageNo") = e.NewPageIndex
    '    ViewState(Left(xScript, Len(xScript) - 5)) = 0
    '    PopulateGrid()
    'End Sub

    'Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try

    '        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit) Then
    '            Dim lnk As New LinkButton
    '            Dim i As String = "", ii As String = "", fdtrNo As Integer = 0

    '            lnk = sender
    '            Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
    '            rowno = gvrow.RowIndex
    '            ViewState(Left(xScript, Len(xScript) - 5)) = rowno
    '            Me.grdMain.SelectedIndex = Generic.CheckDBNull(ViewState(Left(xScript, Len(xScript) - 5)), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

    '            i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
    '            ii = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
    '            ViewState(xScript & "No") = i

    '            Dim ds As DataSet, formname As String = ""
    '            ds = SQLHelper.ExecuteDataSet("EMenu_Tab", xPublicVar.xOnlineUseNo, "", Session("xMenuType"), 1)
    '            If ds.Tables.Count > 0 Then
    '                If ds.Tables(0).Rows.Count > 0 Then
    '                    formname = Generic.CheckDBNull(ds.Tables(0).Rows(0)("formName"), Generic.enumObjectType.StrType)
    '                    Session("xMenuType") = Generic.CheckDBNull(ds.Tables(0).Rows(0)("Menutype"), Generic.enumObjectType.StrType)
    '                    Response.Redirect("~/secured/" & formname & "?Id=" & Generic.CheckDBNull(i, Global.clsBase.clsBaseLibrary.enumObjectType.IntType) & "&tModify=false&rowno=" & rowno.ToString)
    '                End If

    '            End If
    '            'Response.Redirect("~/secured/empEditPerson.aspx?transNo=" & Generic.CheckDBNull(i, Global.clsBase.clsBaseLibrary.enumObjectType.IntType) & "&tModify=false&rowno=" & rowno.ToString)
    '            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
    '        Else
    '            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
    '        End If

    '    Catch ex As Exception
    '    End Try
    'End Sub
    'Protected Sub lnkOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim lnk As New ImageButton
    '    Dim i As String = "", fdtrNo As Integer = 0

    '    lnk = sender
    '    Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
    '    rowno = gvrow.RowIndex
    '    ViewState(Left(xScript, Len(xScript) - 5)) = rowno
    '    i = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
    '    ViewState(xScript & "No") = i

    '    Response.Redirect("~/secured/frmfileuploadList.aspx?FileNo=" & Generic.CheckDBNull(ViewState(xScript & "No"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType) & "&menumassNo=2&rowno=" & rowno.ToString & "&EmployeeNo=" & lnk.CommandArgument)

    'End Sub

    'Protected Sub lnkServiceRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim lnk As ImageButton = sender

    '        Dim gvrow As GridViewRow = DirectCast(lnk.NamingContainer, GridViewRow)
    '        Dim fno As Integer = CType(Me.grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString(), Integer)
    '        Dim filterbyno As Integer = 0 'CType(grdMain.DataKeys(gvrow.RowIndex).Values(2).ToString(), Integer)
    '        Dim sb As New StringBuilder
    '        Dim ReportNo As Integer = 0, rptname As String = "", reporttitle As String = "", Datasource As String = "", ReportCode As String = ""
    '        Dim ds As New DataSet

    '        ds = SQLHelper.ExecuteDataset("EReportIndi_WebInq", xPublicVar.xOnlineUseNo, Session("xPayLocNo"), "EmpServiceRec")
    '        If ds.Tables.Count > 0 Then
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                ReportNo = Generic.CheckDBNull(ds.Tables(0).Rows(0)("ReportNo"), clsBase.clsBaseLibrary.enumObjectType.IntType)
    '                rptname = Generic.CheckDBNull(ds.Tables(0).Rows(0)("AccessReportName"), clsBase.clsBaseLibrary.enumObjectType.StrType)
    '                reporttitle = Generic.CheckDBNull(ds.Tables(0).Rows(0)("xReportTitle"), clsBase.clsBaseLibrary.enumObjectType.StrType)
    '                Datasource = Generic.CheckDBNull(ds.Tables(0).Rows(0)("Datasource"), clsBase.clsBaseLibrary.enumObjectType.StrType)
    '                ReportCode = Generic.CheckDBNull(ds.Tables(0).Rows(0)("ReportCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
    '            End If
    '        End If

    '        sb.Append("<script>")
    '        sb.Append("window.open('rptTemplateViewerServiceRecord.aspx?tNo=" & fno & "&reportname=" & rptname & "&ReportNo=" & ReportNo & "&ReportCode=" & ReportCode & "&reporttitle=" & reporttitle & "&datasource=" & Datasource & "&filterbyid=1&filterbyNo=" & filterbyno & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes,with=800,height=550');")
    '        sb.Append("</scri")
    '        sb.Append("pt>")

    '        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())


    '    Catch ex As Exception

    '    End Try
    'End Sub

    ''Protected Sub grdMain_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMain.RowDataBound
    ''    If e.Row.RowType = DataControlRowType.DataRow Then
    ''        Dim lnkCode As New LinkButton, lnkC As New LinkButton

    ''        Dim lbl As New Label
    ''        lbl = CType(e.Row.FindControl("lblId"), Label)
    ''        lnkCode = CType(e.Row.FindControl("lnkCaption"), LinkButton)
    ''        lnkC = CType(e.Row.FindControl("lnkCaptionx"), LinkButton)

    ''        If Not lbl Is Nothing Then
    ''            rowno = CInt(Mid(lnkCode.NamingContainer.UniqueID, Len(lnkCode.NamingContainer.UniqueID) - 1, Len(lnkCode.NamingContainer.UniqueID) - 2)) - 2
    ''            If clsArray.myFormname(1).xFormname = "" Then
    ''                lnkCode.Attributes.Add("onclick", "return alert('" & clsLog.GetDeniedMessage(clsLogin.EnumPermissionType.AllowEdit) & "');")
    ''            Else
    ''                lnkCode.PostBackUrl = "~/secured/" & clsArray.myFormname(1).xFormname & "?transNo=" & Generic.CheckDBNull(lbl.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType) & "&tModify=false&rowno=" & rowno.ToString
    ''            End If

    ''            lnkC.PostBackUrl = "~/secured/frmfileuploadList.aspx?FileNo=" & Generic.CheckDBNull(lbl.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType) & "&menumassNo=2&rowno=" & rowno.ToString

    ''        End If
    ''    End If
    ''End Sub


    'Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs)
    '    If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowAdd) Then
    '        Response.Redirect("~/secured/EmpEditperson.aspx?transNo=0&tModify=true&rowno=" & rowno.ToString)
    '    Else
    '        MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
    '    End If
    'End Sub
    'Protected Sub addTrigger_PreRender(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim btnPreview As ImageButton = TryCast(sender, ImageButton)
    '    Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
    '    NewScriptManager.RegisterPostBackControl(btnPreview)
    'End Sub


End Class


