Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_EmpPlantillaList
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0
    Dim IsEnabledPlantilla As Boolean = False



    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 32)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataSet("EFilteredBy_TableOrgLookup").Tables(0)
            cboFilterBy1.DataSource = dt
            cboFilterBy1.DataTextField = dt.Columns(1).ColumnName
            cboFilterBy1.DataValueField = dt.Columns(0).ColumnName
            cboFilterBy1.DataBind()
            cboFilterValue1.Items.Clear()

            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPlantilla_Web", UserNo, PayLocNo, cboTabNo.SelectedValue)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateGridDetl(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPlantilla_WebHRAN", UserNo, id)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub

    Protected Sub PopulateData()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPlantilla_WebOne", UserNo, Generic.ToInt(hifPlantillaNo.Value))
            Generic.PopulateData(Me, "Panel1", dt)
            PopulateListBox()
            'clear list
            ListBox1.UnselectAll()
            Dim xdt As DataTable = SQLHelper.ExecuteDataTable("EPlantillaMirror_WebOne", UserNo, txtPlantillaCode.Text)
            For Each item As ListEditItem In ListBox1.Items
                For Each row As DataRow In xdt.Rows
                    If Generic.Split(item.Value, 0) = Generic.ToStr(row("PlantillaCode2")) Then
                        item.Selected = True
                    End If
                Next
            Next

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PlantillaNo", "PlantillaCode"})
        ViewState("TransNo") = obj(0)
        ViewState("Code") = obj(1)

        lblDetl.Text = "Plantilla No. : " & Generic.ToStr(ViewState("Code"))

        PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))


    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))        
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then

            IsEnabledPlantilla = Generic.ToBol(SQLHelper.ExecuteScalar("SELECT dbo.EGetEnabled_Plantilla() AS IsEnabled"))

            cboLevelNo.Items.Add(New ListItem("MAX", "0"))
            For index As Integer = 1 To 25
                Dim li As New ListItem
                li.Value = index
                li.Text = index
                cboLevelNo.Items.Add(li)
            Next
            cboLevelNo.SelectedValue = "3"
            PopulateDropDown()

            Generic.EnableControls(Me, "Panel1", IsEnabledPlantilla)
            lnkSave.Visible = IsEnabledPlantilla
            lnkAdd.Visible = IsEnabledPlantilla

            'Color enum
            Dim itemValues As Array = System.[Enum].GetValues(GetType(OrgChart.Core.BackgroundImageColour))
            Dim itemNames As Array = System.[Enum].GetNames(GetType(OrgChart.Core.BackgroundImageColour))
            For i As Integer = 0 To itemNames.Length - 1
                Dim item As New ListItem(itemNames(i), itemValues(i))
                cboStaffColor.Items.Add(item)
            Next

        End If


        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            hifPlantillaNo.Value = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PlantillaNo"}))
            PopulateData()
            Populate_chkMirror()
            'PopulateListBox()
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkFilter_Click(sender As Object, e As EventArgs)
        'ModalPopupExtender2.Show()
        ModalPopupExtender3.Show()
    End Sub

    Protected Sub lnkGenerate_Click(sender As Object, e As EventArgs)
        'Try
        '    Dim FilterByNo As Integer = Generic.Split(cboFilterBy1.SelectedValue, 0)
        '    Response.Redirect("~/Secured/EmpPlantillaChart.aspx?by=" & FilterByNo & "&value=" & cboFilterValue1.SelectedValue)
        'Catch ex As Exception

        'End 'Try

        'Dim FilterByNo As Integer = Generic.Split(cboFilterBy1.SelectedValue, 0)                
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("window.open('EmpPlantillaChart.aspx?w=" & cboView.SelectedValue & "&s=" & Generic.ToInt(chkStack.Checked) & "&l=" & cboLevelNo.SelectedValue & "&id=" & hifPlantillaCode.Value & "&m=" & Generic.ToInt(chkIsMirrorView.Checked) & "&b=" & Generic.ToInt(chkIsBridge.Checked) & "&c=" & Generic.ToInt(chkIsCluster.Checked) & "&pt=" & Generic.ToInt(cboPlantillaTypeNo.SelectedValue) & "&ibs=" & Generic.ToInt(chkIsBudgetSource.Checked) & "&color=" & cboStaffColor.SelectedValue & "&ir=" & Generic.ToInt(chkIsReassignment.Checked) & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,resizable=yes,scrollbars=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())



    End Sub

    Protected Sub lnkJD_Click(Sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = Sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PlantillaNo"}))
        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, id.ToString()))

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=130&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkPositionChart_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PlantillaNo"}))        
        sb.Append("<script>")
        sb.Append("window.open('empplantillapositionchart.aspx?id=" & id & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub cboFilterBy_SelectedIndexChanged()
        Dim FilteredByNo As Integer = Generic.ToInt(Generic.Split(cboFilterBy1.SelectedValue, 0))
        Dim TableName As String = Generic.Split(cboFilterBy1.SelectedValue, 1)
        If TableName <> "" Then
            cboFilterValue1.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, TableName, PayLocNo, "", "")
            cboFilterValue1.DataTextField = "tDesc"
            cboFilterValue1.DataValueField = "tNo"
            cboFilterValue1.DataBind()
        Else
            cboFilterValue1.Items.Clear()
        End If
        ModalPopupExtender2.Show()
    End Sub

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function PopulatePlantilla(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim _ds As New DataSet()
        Dim sqlhelp As New clsBase.SQLHelper
        Dim clsbase As New clsBase.clsBaseLibrary
        Dim UserNo As Integer = 0, PayLocNo As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        PayLocNo = (HttpContext.Current.Session("xPayLocNo"))

        _ds = SQLHelper.ExecuteDataSet("EPlantilla_Generate", UserNo, prefixText, PayLocNo)
        For Each row As DataRow In _ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("PlantillaDesc")), Generic.ToStr(row("PlantillaCode")))
            items.Add(item)
        Next
        _ds.Dispose()
        Return items

    End Function

    Protected Sub lnk_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Private Sub PopulateListBox()
        ListBox1.DataSource = SQLHelper.ExecuteDataTable("EPlantillaMirror_Web", UserNo, txtPlantillaCode.Text, PayLocNo)
        ListBox1.ValueField = "xCode"
        ListBox1.TextField = "xDesc"
        ListBox1.DataBind()
    End Sub

    Protected Sub chkIsMirror_CheckedChanged(sender As Object, e As EventArgs)
        Populate_chkMirror()
        ModalPopupExtender1.Show()
    End Sub

    Private Sub Populate_chkMirror()
        If chkIsMirror.Checked Then
            'txtPreviousPlantillaCode.Text = ""
            'txtPreviousPlantillaCode.ReadOnly = True
            ListBox1.Enabled = False
            ListBox1.Items.Clear()
            cboPlantillaGovTypeNo.Enabled = True
            lblbptype.Attributes.Add("class", "col-md-4 control-label has-required")
            cboPlantillaGovTypeNo.CssClass = "form-control required"

        Else
            ListBox1.Enabled = True
            cboPlantillaGovTypeNo.Enabled = False
            cboPlantillaGovTypeNo.SelectedValue = ""
            lblbptype.Attributes.Add("class", "col-md-4 control-label has-space")
            cboPlantillaGovTypeNo.CssClass = "form-control"

            PopulateListBox()
            'clear list
            ListBox1.UnselectAll()
            Dim xdt As DataTable = SQLHelper.ExecuteDataTable("EPlantillaMirror_WebOne", UserNo, txtPlantillaCode.Text)
            For Each item As ListEditItem In ListBox1.Items
                For Each row As DataRow In xdt.Rows
                    If Generic.Split(item.Value, 0) = Generic.ToStr(row("PlantillaCode2")) Then
                        item.Selected = True
                    End If
                Next
            Next

            'txtPreviousPlantillaCode.ReadOnly = False
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim PlantillaNo As Integer = Generic.ToInt(hifPlantillaNo.Value)
        Dim PlantillaCode As String = Generic.ToStr(txtPlantillaCode.Text)
        Dim ParentPlantillaCode As String = Generic.ToStr(txtParentPlantillaCode.Text)
        Dim PlantillaTypeNo As Integer = Generic.ToInt(cboPlantillaTypeNo.SelectedValue)
        Dim IsMirror As Integer = Generic.ToInt(chkIsMirror.Checked)
        Dim PositionNo As Integer = Generic.ToInt(cboPositionNo.SelectedValue)
        Dim TaskNo As Integer = Generic.ToInt(cboTaskNo.SelectedValue)
        Dim PlantillaGovTypeNo As Integer = Generic.ToInt(cboPlantillaGovTypeNo.SelectedValue)
        Dim SalaryGradeNo As Integer = Generic.ToInt(cboSalaryGradeNo.SelectedValue)
        Dim FacilityNo As Integer = Generic.ToInt(cboFacilityNo.SelectedValue)

        Dim IsFacHead As Integer = Generic.ToInt(chkIsFacHead.Checked)
        Dim UnitNo As Integer = Generic.ToInt(cboUnitNo.SelectedValue)
        Dim IsUniHead As Integer = Generic.ToInt(chkIsUniHead.Checked)
        Dim DepartmentNo As Integer = Generic.ToInt(cboDepartmentNo.SelectedValue)
        Dim IsDepHead As Integer = Generic.ToInt(chkIsDepHead.Checked)
        Dim GroupNo As Integer = Generic.ToInt(cboGroupNo.SelectedValue)
        Dim IsGroHead As Integer = Generic.ToInt(chkIsGroHead.Checked)
        Dim DivisionNo As Integer = Generic.ToInt(cboDivisionNo.SelectedValue)
        Dim IsDivHead As Integer = Generic.ToInt(chkIsDivHead.Checked)
        Dim SectionNo As Integer = Generic.ToInt(cboSectionNo.SelectedValue)

        Dim IsSecHead As Integer = Generic.ToInt(chkIsSecHead.Checked)
        Dim CostCenterNo As Integer = Generic.ToInt(cboCostCenterNo.SelectedValue)
        Dim LocationNo As Integer = Generic.ToInt(cboLocationNo.SelectedValue)
        Dim AreaTypeNo As Integer = Generic.ToInt(cboAreaTypeNo.SelectedValue)
        Dim OccupationalGroupNo As Integer = Generic.ToInt(cboOccupationalGroupNo.SelectedValue)
        Dim IsManpowerPool As Integer = Generic.ToInt(chkIsManpowerPool.Checked)
        Dim Remarks As String = txtRemarks.Text
        Dim IsArchived As Integer = Generic.ToInt(chkIsArchived.Checked)

        If SQLHelper.ExecuteNonQuery("EPlantilla_WebSave", UserNo, PlantillaNo, PlantillaCode, ParentPlantillaCode, PlantillaGovTypeNo, IsMirror, PositionNo, TaskNo, PlantillaGovTypeNo, SalaryGradeNo, FacilityNo, IsFacHead, _
                                    UnitNo, IsUniHead, DepartmentNo, IsDepHead, GroupNo, IsGroHead, DivisionNo, IsDivHead, SectionNo, IsSecHead, CostCenterNo, LocationNo, AreaTypeNo, _
                                    OccupationalGroupNo, IsManpowerPool, Remarks, IsArchived, txtTitle.Text, Generic.ToInt(chkIsHeadStaff.Checked), PayLocNo) > 0 Then

            SQLHelper.ExecuteNonQuery("ETableOrgMirror_WebDelete", UserNo, txtPlantillaCode.Text)
            For Each item As ListEditItem In ListBox1.Items
                If item.Selected Then
                    SQLHelper.ExecuteNonQuery("EPlantillaMirror_WebSave", UserNo, txtPlantillaCode.Text, Generic.Split(item.Value, 0))
                End If
            Next

            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            PopulateListBox()
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub txtPlantillaCode_TextChanged(sender As Object, e As System.EventArgs) Handles txtPlantillaCode.TextChanged
        PopulateListBox()
        'clear list
        ListBox1.UnselectAll()
        Dim xdt As DataTable = SQLHelper.ExecuteDataTable("EPlantillaMirror_WebOne", UserNo, txtPlantillaCode.Text)
        For Each item As ListEditItem In ListBox1.Items
            For Each row As DataRow In xdt.Rows
                If Generic.Split(item.Value, 0) = Generic.ToStr(row("PlantillaCode2")) Then
                    item.Selected = True
                End If
            Next
        Next
        ModalPopupExtender1.Show()

    End Sub

    Protected Sub lnkHistory_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        'Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PlantillaNo"}))
        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.str, lnk.CommandArgument))

        sb.Append("<script>")
        sb.Append("window.open('RptTemplateViewerCode.aspx?reportcode=pkhistory&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkHierarchyChart_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        Dim sb As New StringBuilder
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PlantillaNo"}))
        sb.Append("<script>")
        sb.Append("window.open('empplantillahierarchychart.aspx?id=" & id & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")
        ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub


End Class
