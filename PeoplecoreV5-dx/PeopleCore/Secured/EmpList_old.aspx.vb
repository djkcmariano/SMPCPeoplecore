Imports System.Data
Imports System.Math
Imports DevExpress.Web
Imports clsLib

Partial Class Secured_EmpList
    Inherits System.Web.UI.Page


    Dim xPublicVar As New clsPublicVariable



    Dim tstatus As Integer
    Dim dscount As Double = 0

    Dim _ds As New DataSet
    Dim _dt As New DataTable
    Dim xScript As String = ""
    Dim rowno As Integer = 0
    Dim employeeno As Integer = 0
    Dim IsClickMain As Integer = 0

    Dim clsMessage As New clsMessage
    Dim txtfilter As New TextBox
    Dim lnkCriteria As New Button

    Private Sub PopulateGrid(Optional IsMain As Boolean = False, Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        'Dim _dr As SqlClient.SqlDataReader

        If tstatus = 0 Then
            tstatus = 1
        End If
        dscount = 0
        _ds = sqlhelper.ExecuteDataSet("EEmployee_Web", xPublicVar.xOnlineUseNo, Generic.CheckDBNull(ViewState(xScript & "filter"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType), tstatus, Generic.CheckDBNull(ViewState(xScript & "filterby"), clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(ViewState(xScript & "filtervalue"), clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(Session("xPayLocNo"), clsBase.clsBaseLibrary.enumObjectType.IntType))

        _dt = _ds.Tables(0)
        Dim dv As New DataView(_dt)
        If SortExp <> "" Then
            Session(xScript & "SortExp") = SortExp
        End If
        If sordir <> "" Then

            Session(xScript & "sortdir") = sordir
        End If
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                dscount = _ds.Tables(0).Rows.Count
                If Session(xScript & "SortExp") <> "" Then
                    dv.Sort = Session(xScript & "SortExp") + Session(xScript & "sortdir")
                End If
            End If
        End If
        If IsMain Then
            Session(xScript & "Pageno") = 0
            Session(Left(xScript, Len(xScript) - 5)) = 0
        End If

        Me.grdMain.SelectedIndex = Generic.CheckDBNull(Session(Left(xScript, Len(xScript) - 5)), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Me.grdMain.PageIndex = Session(xScript & "Pageno")
        Me.grdMain.DataSource = dv
        Me.grdMain.DataBind()

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            xPublicVar.xOnlineUseNo = -99 'Generic.CheckDBNull(Session("OnlineUserNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
            If xPublicVar.xOnlineUseNo = 0 Then
                Response.Redirect("../frmpageExpired.aspx")
            Else


                employeeno = Generic.CheckDBNull(Request.QueryString("employeeno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
                tstatus = Generic.CheckDBNull(Request.QueryString("eStatus"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

                xScript = Request.ServerVariables("SCRIPT_NAME")
                xScript = Generic.GetPath(xScript)

                PopulateGrid(False)
        
                lnkCriteria = CType(wucFilter.FindControl("lnkCriteria"), Button)
                If Not lnkCriteria Is Nothing Then
                    AddHandler lnkCriteria.Click, AddressOf lnkCriteria_Click
                End If

            End If

            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
        Catch ex As Exception
            Response.Redirect("../frmpageExpired.aspx")

        End Try
    End Sub
    Protected Sub lnkGo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'ViewState(xScript & "filter") = Generic.CheckDBNull(Me.fltxtfilter.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
            ViewState(xScript & "PageNo") = 0
            PopulateGrid(True)
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkCriteria_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim cbofilterby As New DropDownList
            Dim cbofiltervalue As New DropDownList
            Dim txtfilter As New TextBox

            Dim filterby As Integer
            Dim filtervalue As Integer

            cbofilterby = CType(wucFilter.FindControl("cbofilterby"), DropDownList)
            cbofiltervalue = CType(wucFilter.FindControl("cbofiltervalue"), DropDownList)


            If Not cbofilterby Is Nothing Then
                filterby = Generic.CheckDBNull(cbofilterby.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
                ViewState(xScript & "filterby") = filterby
            End If

            If Not cbofiltervalue Is Nothing Then
                filtervalue = Generic.CheckDBNull(cbofiltervalue.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
                ViewState(xScript & "filtervalue") = filtervalue
            End If

            ViewState(xScript & "PageNo") = 0
            ViewState(Left(xScript, Len(xScript) - 5)) = 0
            PopulateGrid()
            ViewState(xScript & "No") = grdMain.DataKeys(0).Values(0).ToString()


            txtfilter.Text = Generic.CheckDBNull(ViewState(xScript & "filter"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
            cbofilterby.Text = IIf(ViewState(xScript & "filterby") = "0", "", ViewState(xScript & "filterby"))
            cbofiltervalue.Text = IIf(ViewState(xScript & "filterby") = "0", "", ViewState(xScript & "filtervalue"))


        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit) Then
                Dim lnk As New LinkButton
                Dim i As String = "", ii As String = "", fdtrNo As Integer = 0

                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim values() As Object = CType(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo", "FullName"}), Object())

                i = CInt(values(0))
                ii = CStr(values(1))

                ViewState(xScript & "No") = i
                Session("fullName") = ii
                Response.Redirect("~/secured/empEditPerson.aspx?transNo=" & Generic.CheckDBNull(ViewState(xScript & "No"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType) & "&tModify=false&rowno=" & rowno.ToString)

            Else
                MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
            End If

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New ImageButton
        Dim i As String = "", fdtrNo As Integer = 0

        lnk = sender

        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim values() As Object = CType(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo", "FullName"}), Object())
        i = CInt(values(0))
        ViewState(xScript & "No") = i

    End Sub

    Protected Sub lnkServiceRecord_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As ImageButton = sender


            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim values() As Object = CType(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo", "FullName"}), Object())
            Dim fno As Integer = CInt(values(0))

            Dim filterbyno As Integer = 0 'CType(grdMain.DataKeys(gvrow.RowIndex).Values(2).ToString(), Integer)
            Dim sb As New StringBuilder
            Dim ReportNo As Integer = 0, rptname As String = "", reporttitle As String = "", Datasource As String = "", ReportCode As String = ""
            Dim ds As New DataSet

            ds = sqlhelper.ExecuteDataSet("EReportIndi_WebInq", xPublicVar.xOnlineUseNo, Session("xPayLocNo"), "EmpServiceRec")
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    ReportNo = Generic.CheckDBNull(ds.Tables(0).Rows(0)("ReportNo"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                    rptname = Generic.CheckDBNull(ds.Tables(0).Rows(0)("AccessReportName"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    reporttitle = Generic.CheckDBNull(ds.Tables(0).Rows(0)("xReportTitle"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    Datasource = Generic.CheckDBNull(ds.Tables(0).Rows(0)("Datasource"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    ReportCode = Generic.CheckDBNull(ds.Tables(0).Rows(0)("ReportCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                End If
            End If

            sb.Append("<script>")
            sb.Append("window.open('rptTemplateViewerServiceRecord.aspx?tNo=" & fno & "&reportname=" & rptname & "&ReportNo=" & ReportNo & "&ReportCode=" & ReportCode & "&reporttitle=" & reporttitle & "&datasource=" & Datasource & "&filterbyid=1&filterbyNo=" & filterbyno & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes,with=800,height=550');")
            sb.Append("</scri")
            sb.Append("pt>")

            ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())


        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lnkAdd_Click(sender As Object, e As System.EventArgs)


        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Response.Redirect("~/secured/EmpEditperson.aspx?transNo=0&tModify=true&rowno=" & rowno.ToString)
        Else
            MessageBox.Warning(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowAdd), Me)
        End If
    End Sub



    Protected Sub addTrigger_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim btnPreview As ImageButton = TryCast(sender, ImageButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(btnPreview)
    End Sub

End Class


