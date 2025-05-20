Imports System.Data
Imports Microsoft.Reporting.WebForms
Imports clsLib
Imports AjaxControlToolkit

Partial Class Secured_rptTemplateViewerDynamic
    Inherits System.Web.UI.Page
   
    Dim xPublicVar As New clsPublicVariable
    Dim ReportNo As Integer
    Dim tblMain As New Table

    Dim _ds As DataSet
    Dim ReportParamTypeNo As Integer
    Dim TableViewName As String, ReportField As String, ReportLabel As String
    Dim ReportParamNo As Integer = 0
    Dim ReportParamFieldTypeno As Integer = 0
    Dim drpFilterbyNo As New DropDownList
    Dim category As String = ""
    Dim xCategory As String = ""
    Dim RequiredField As Boolean = False
    Dim IsHidden As Boolean = False

    Dim drpFilterbyID As New DropDownList
    Dim rptform As String
    Dim Datasource As String = ""
    Dim reporttitle As String = ""
    Dim clsGeneric As New clsGenericClass
    Dim genHTM As New HtmlGenericControl
    Dim genHTMStr As String = ""

    Dim drpAC As New AutoCompleteExtender
    Dim txtName As New TextBox
    Dim PayLocNo As Integer = 0




    Protected Sub drpFilterbyNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'Try
        '    Dim fId As String = ""
        '    Dim fDrp As New DropDownList
        '    fDrp = sender

        '    fId = fDrp.ID
        '    Dim tId As Integer
        '    tId = Generic.CheckDBNull(drpFilterbyNo.SelectedValue, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        '    populateDataDropdownF(tId)
        'Catch ex As Exception
        'End Try

        txtName.Text = ""
        hifNo.Value = "0"
        Dim fId As Integer
        fId = Generic.ToInt(drpFilterbyNo.SelectedValue)

        If fId > 0 Then
            txtName.Enabled = True
            drpAC.CompletionSetCount = fId
        Else
            txtName.Enabled = False
            drpAC.CompletionSetCount = 0
        End If

    End Sub
    Private Sub PopulateControls()
        Dim lbl As New Label
        Dim drp As New DropDownList

        If IsHidden = False Then
            pForm.Controls.Add(New LiteralControl("<div class='form-group'>"))
        End If

        lbl.Text = ReportLabel
        lbl.ID = "lbl" & ReportParamNo
        If RequiredField = True Then
            lbl.CssClass = "col-md-4 control-label has-required"
        Else
            lbl.CssClass = "col-md-4 control-label has-space"
        End If

        If IsHidden = True Then
            lbl.Visible = False
        Else
            lbl.Visible = True
        End If

        'If IsHidden = False Then
        pForm.Controls.Add(lbl)
        'End If

        If IsHidden = False Then
            If ReportParamFieldTypeno = 2 Then
                pForm.Controls.Add(New LiteralControl("<div class='col-md-2'>"))
            Else
                pForm.Controls.Add(New LiteralControl("<div class='col-md-5'>"))
            End If
        End If

        If ReportParamTypeNo = 1 Then
            If ReportField = "filterbyno" Then
                If RequiredField = True Then
                    drpFilterbyNo.CssClass = "form-control required"
                Else
                    drpFilterbyNo.CssClass = "form-control"
                End If

                If IsHidden = True Then
                    drpFilterbyNo.Visible = False
                Else
                    drpFilterbyNo.Visible = True
                End If

                drpFilterbyNo.DataSource = clsGeneric.xLookup_Table(xPublicVar.xOnlineUseNo, TableViewName)
                drpFilterbyNo.DataTextField = "tDesc"
                drpFilterbyNo.DataValueField = "tno"
                drpFilterbyNo.DataBind()
                drpFilterbyNo.ID = "filterbyno"
                drpFilterbyNo.AutoPostBack = True
                AddHandler drpFilterbyNo.SelectedIndexChanged, AddressOf drpFilterbyNo_SelectedIndexChanged
                pForm.Controls.Add(drpFilterbyNo)

            ElseIf ReportField = "filterbyid" Then
                'drpFilterbyID.ID = "filterbyid"
                'pForm.Controls.Add(drpFilterbyID)
                'xCategory = category

                If RequiredField = True Then
                    txtName.CssClass = "form-control required"
                Else
                    txtName.CssClass = "form-control"
                End If

                If IsHidden = True Then
                    txtName.Visible = False
                Else
                    txtName.Visible = True
                End If

                txtName.ID = "txtName"
                txtName.Enabled = False
                pForm.Controls.Add(txtName)

                drpAC.ID = "filterbyid"
                drpAC.SkinID = "AC"
                drpAC.OnClientItemSelected = "GetRecord"
                drpAC.ServiceMethod = "populateDataDropdown"
                drpAC.MinimumPrefixLength = "2"
                drpAC.CompletionInterval = "500"
                drpAC.CompletionSetCount = "0"
                drpAC.TargetControlID = "txtName"
                pForm.Controls.Add(drpAC)

            Else
                If RequiredField = True Then
                    drp.CssClass = "form-control required"
                Else
                    drp.CssClass = "form-control"
                End If

                If IsHidden = True Then
                    drp.Visible = False
                Else
                    drp.Visible = True
                End If

                drp.ID = ReportField
                If TableViewName.Length > 0 Or Generic.CheckDBNull(TableViewName, Global.clsBase.clsBaseLibrary.enumObjectType.StrType) <> "" Then
                    If category = "sp" Then
                        drp.DataSource = SQLHelper.ExecuteDataSet(TableViewName, xPublicVar.xOnlineUseNo, Session("xPayLocNo"))
                    Else
                        drp.DataSource = clsGeneric.xLookup_Table(xPublicVar.xOnlineUseNo, TableViewName, Session("xPayLocNo"))
                    End If
                    drp.DataTextField = "tDesc"
                    drp.DataValueField = "tno"
                    drp.DataBind()
                End If

                pForm.Controls.Add(drp)

            End If
        ElseIf ReportParamTypeNo = 2 Then
            Dim txt As New TextBox
            Dim cal As New AjaxControlToolkit.CalendarExtender
            Dim mask As New AjaxControlToolkit.MaskedEditExtender

            txt.ID = ReportField
            If RequiredField = True Then
                txt.CssClass = "form-control required"
            Else
                txt.CssClass = "form-control"
            End If

            If IsHidden = True Then
                txt.Visible = False
            Else
                txt.Visible = True
            End If

            pForm.Controls.Add(txt)
            If ReportField = "paylocno" Then
                txt.Enabled = False
                txt.Text = Session("xPayLocNo")
            End If

            If ReportParamFieldTypeno = 2 Then
                'txt.SkinID = "txtdate"
                cal.TargetControlID = txt.ID
                cal.Format = "MM/dd/yyyy"
                cal.ID = "cal" & ReportParamNo
                mask.ID = "mask" & ReportParamNo
                mask.TargetControlID = txt.ID
                mask.Mask = "99/99/9999"
                mask.MessageValidatorTip = True
                mask.OnFocusCssClass = "MaskedEditFocus"
                mask.OnInvalidCssClass = "MaskedEditError"
                mask.MaskType = AjaxControlToolkit.MaskedEditType.Date
                mask.DisplayMoney = AjaxControlToolkit.MaskedEditShowSymbol.Left
                pForm.Controls.Add(cal)
                pForm.Controls.Add(mask)
            End If
        End If

        If IsHidden = False Then
            pForm.Controls.Add(New LiteralControl("</div>"))
            pForm.Controls.Add(New LiteralControl("</div>"))
        End If

    End Sub
    Private Sub PopulateParameter()
        Dim Rowcount As Integer = 0
        _ds = SQLHelper.ExecuteDataset("EReportParam_Web", xPublicVar.xOnlineUseNo, ReportNo)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                Rowcount = _ds.Tables(0).Rows.Count
                For i As Integer = 0 To Rowcount - 1

                    ReportParamTypeNo = Generic.ToInt(_ds.Tables(0).Rows(i)("ReportParamTypeNo"))
                    ReportParamNo = Generic.ToInt(_ds.Tables(0).Rows(i)("ReportParamNo"))
                    TableViewName = Generic.ToStr(_ds.Tables(0).Rows(i)("TableViewName"))
                    ReportField = Generic.ToStr(_ds.Tables(0).Rows(i)("ReportField"))
                    ReportLabel = Generic.ToStr(_ds.Tables(0).Rows(i)("ReportLabel"))
                    ReportParamFieldTypeno = Generic.ToInt(_ds.Tables(0).Rows(i)("ReportParamFieldTypeno"))
                    category = Generic.ToStr(_ds.Tables(0).Rows(i)("category"))
                    RequiredField = Generic.ToBol(_ds.Tables(0).Rows(i)("RequiredField"))
                    IsHidden = Generic.ToBol(_ds.Tables(0).Rows(i)("IsHidden"))

                    PopulateControls()
                Next

            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xPublicVar.xOnlineUseNo = Generic.ToInt(Session("onlineuserno"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        ReportNo = Generic.ToInt(Request.QueryString("ReportNo"))
        reporttitle = Generic.ToStr(Request.QueryString("reporttitle"))
        rptform = Generic.ToStr(Request.QueryString("reportname"))
        Datasource = Generic.ToStr(Request.QueryString("Datasource"))
        Permission.IsAuthenticated()

        PopulateParameter()
        If Not IsPostBack Then
            'populateDataDropdownF(filterbyNo)
            'Me.cboFilterBy.Text = filterbyNo
        End If
        'populateDataDropdownF()

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Private Sub populateDataDropdownF(ByVal fid As Integer)
        Dim clsGen As New clsGenericClass
        Dim ds As DataSet
        ds = clsGen.populateDropdownFilterByCate(fid, xPublicVar.xOnlineUseNo, Session("xPayLocNo"), xCategory)
        drpFilterbyID.DataSource = ds
        drpFilterbyID.DataTextField = "tDesc"
        drpFilterbyID.DataValueField = "tNo"
        drpFilterbyID.DataBind()
    End Sub

    Protected Sub lnkPreview_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        xPublicVar.xOnlineUseNo = Generic.ToInt(Session("onlineuserno"))
        PopulateServerReport()
    End Sub
    Private Sub PopulateServerReport()

        Dim ds As New DataSet
        Dim dsrpt As New ReportDataSource
        Dim rpt As ServerReport = rviewer.ServerReport
        Me.rviewer.CssClass = "panel panel-default"
        Me.rviewer.ProcessingMode = ProcessingMode.Local
        Me.rviewer.Reset()

        Dim dsr As New DataSet
        Dim Rowcount As Integer = 0
        Dim fReportField As String = ""
        Dim forderlevel As Integer = 0

        Dim txt As New TextBox
        Dim drp As New DropDownList
        'Dim Filterbyid As New DropDownList
        Dim Filterbyid As New AutoCompleteExtender
        Dim Filterbyno As New DropDownList
        Dim fpid As Integer = 0
        Dim ftxt As String = ""
        Dim freportparamfieldtypeno As Integer = 0
        Dim fcategory As String = ""
        Dim freportparamtypeno As Integer = 0
        Dim xbase As New clsBase.clsBaseLibrary
        Dim xSQLHelper As New clsBase.SQLHelper
        Dim xIsHidden As Boolean = False

        ds = SQLHelper.ExecuteDataSet("EReportParam_Web", xPublicVar.xOnlineUseNo, ReportNo)
        If ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                Rowcount = _ds.Tables(0).Rows.Count
                Dim parm(Rowcount + 1) As Object

                parm(0) = xPublicVar.xOnlineUseNo
                parm(1) = PayLocNo
                For i As Integer = 0 To Rowcount - 1

                    Filterbyid = Nothing
                    Filterbyno = Nothing
                    drp = Nothing
                    txt = Nothing

                    forderlevel = Generic.ToInt(_ds.Tables(0).Rows(i)("orderlevel"))
                    fReportField = Generic.ToStr(_ds.Tables(0).Rows(i)("ReportField"))
                    freportparamfieldtypeno = Generic.ToInt(_ds.Tables(0).Rows(i)("reportparamfieldtypeno"))
                    fcategory = Generic.ToStr(_ds.Tables(0).Rows(i)("category"))
                    freportparamtypeno = Generic.ToStr(_ds.Tables(0).Rows(i)("reportparamtypeno"))
                    'xIsHidden = Generic.ToBol(_ds.Tables(0).Rows(i)("IsHidden"))

                    If freportparamtypeno = 1 Then
                        If fReportField = "filterbyid" Then
                            Try
                                Filterbyid = CType(pForm.FindControl(fReportField), AutoCompleteExtender)
                            Catch ex As Exception
                                Filterbyid = Nothing
                            End Try
                        ElseIf fReportField = "filterbyno" Then
                            Try
                                Filterbyno = CType(pForm.FindControl(fReportField), DropDownList)
                            Catch ex As Exception
                                Filterbyno = Nothing
                            End Try
                        Else
                            Try
                                drp = CType(pForm.FindControl(fReportField), DropDownList)
                            Catch ex As Exception
                                drp = Nothing
                            End Try
                        End If
                    Else
                        Try
                            txt = CType(pForm.FindControl(fReportField), TextBox)
                        Catch ex As Exception
                            txt = Nothing
                        End Try
                    End If

                    forderlevel = forderlevel + 1
                    If Not txt Is Nothing Then
                        If freportparamfieldtypeno = 1 Then
                            fpid = Generic.ToInt(txt.Text)
                            parm(forderlevel) = fpid
                        Else
                            ftxt = Generic.ToStr(txt.Text)
                            parm(forderlevel) = ftxt
                        End If
                    ElseIf Not drp Is Nothing Then
                        fpid = Generic.ToInt(drp.SelectedValue)
                        parm(forderlevel) = fpid
                    ElseIf Not Filterbyid Is Nothing Then
                        'fpid = Generic.ToInt(Filterbyid.SelectedValue)
                        If txtName.Text = "" Then
                            fpid = 0
                        Else
                            fpid = Generic.ToInt(hifNo.Value)
                        End If
                        parm(forderlevel) = fpid
                    ElseIf Not Filterbyno Is Nothing Then
                        fpid = Generic.ToInt(Filterbyno.SelectedValue)
                        parm(forderlevel) = fpid
                    End If

                Next

                Dim rptPath As String = ""
                Dim dsSource As DataSet

                dsSource = xSQLHelper.ExecuteDataset(SQLHelper.ConSTR, Datasource, parm) ' 'SQLHelper.ExecuteDataset(Datasource, parm)
                If dsSource.Tables.Count > 0 Then
                    If dsSource.Tables(0).Rows.Count > 0 Then
                        dsrpt.Name = "ds" ' rptform & "_Data"
                        Dim mpath As String = Server.MapPath("../Reports/")

                        rptPath = mpath & rptform '& ".rdlc"
                        dsrpt.Value = dsSource.Tables(0)
                        ReportViewer_OnLoad(ReportNo)

                        rviewer.LocalReport.DisplayName = dsSource.Tables(0).TableName
                        rviewer.LocalReport.ReportPath = rptPath
                        rviewer.LocalReport.DataSources.Clear()
                        rviewer.LocalReport.DataSources.Add(dsrpt)
                        rviewer.LocalReport.DisplayName = dsrpt.Name
                        rviewer.ShowPrintButton = True

                        AddHandler rviewer.LocalReport.SubreportProcessing, AddressOf SetSubDataSource

                        rviewer.LocalReport.Refresh()
                    Else
                        MessageBox.Warning("No record found to preview.", Me)
                    End If
                Else
                    MessageBox.Warning("No record found to preview.", Me)
                End If

            End If
        End If

    End Sub

    Protected Sub showPrintButton()
        Dim script As String = "<SCRIPT LANGUAGE='JavaScript'> "
        script += "showPrintButton()"
        script += "</SCRIPT>"
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowStatus", "javascript:showPrintButton();", True)
    End Sub

    Protected Sub SetSubDataSource(ByVal sender As Object, ByVal e As SubreportProcessingEventArgs)
        Try
            Dim dsD As DataSet
            Dim mpath As String = Server.MapPath("../Reports/")
            Dim dsSource As DataSet, fDatasource As String = "", dsName As String = "", reportdetiNo As Integer = 0

            Dim ParamName As String = ""
            Dim orderNo As Integer = 0

            dsD = SQLHelper.ExecuteDataset("EReportDeti_Web", xPublicVar.xOnlineUseNo, ReportNo)
            If dsD.Tables.Count > 0 Then
                If dsD.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To dsD.Tables(0).Rows.Count - 1
                        fDatasource = Generic.ToStr(dsD.Tables(0).Rows(i)("dataSource"))
                        dsName = Generic.ToStr(dsD.Tables(0).Rows(i)("dsName"))
                        reportdetiNo = Generic.ToInt(dsD.Tables(0).Rows(i)("reportDetiNo"))

                        Dim dsDeti As DataSet, rCount As Integer = 0

                        dsDeti = SQLHelper.ExecuteDataset("EReportDetiParam_Web", xPublicVar.xOnlineUseNo, reportdetiNo)
                        If dsDeti.Tables.Count > 0 Then
                            If dsDeti.Tables(0).Rows.Count > 0 Then
                                rCount = dsDeti.Tables(0).Rows.Count - 1
                                Dim parmV(rCount) As Object
                                'Populate parameter of subreport
                                For ii As Integer = 0 To dsDeti.Tables(0).Rows.Count - 1
                                    ParamName = Generic.ToStr(dsDeti.Tables(0).Rows(ii)("ParamName"))
                                    orderNo = Generic.ToInt(dsDeti.Tables(0).Rows(ii)("orderNo"))
                                    parmV(ii) = e.Parameters(ParamName).Values(0)
                                Next

                                If rCount >= 0 Then
                                    dsSource = SQLHelper.ExecuteDataset(fDatasource, parmV)
                                    If dsSource.Tables.Count > 0 Then
                                        'If dsSource.Tables(0).Rows.Count > 0 Then
                                        'Display Report
                                        e.DataSources.Add(New ReportDataSource(dsName, dsSource.Tables(0)))
                                        'End If
                                    End If

                                End If
                            End If

                        End If


                    Next

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    <System.Web.Script.Services.ScriptMethod()> _
    <System.Web.Services.WebMethod()> _
    Public Shared Function populateDataDropdown(prefixText As String, count As Integer, contextKey As String) As List(Of String)
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

    Protected Sub ReportViewer_OnLoad(ReportNo As Integer)
        Dim ds As New DataSet, dt As DataTable
        'Dim IsWord As Boolean, IsPDF As Boolean, IsExcel As Boolean
        Dim xReportNo As Integer
        dt = SQLHelper.ExecuteDataTable("SELECT TOP 1 * FROM EReport WHERE xReportTitle = 'Notice of Personnel Action'")
        For Each row As DataRow In dt.Rows
            xReportNo = Generic.ToInt(row("ReportNo"))
            'IsWord = Generic.ToBol(row("IsWord"))
            'IsPDF = Generic.ToBol(row("IsPDF"))
            'IsExcel = Generic.ToBol(row("IsExcel"))
        Next

        If ReportNo = xReportNo Then
            'Hide exporting option
            'If IsWord = False Then
            Dim exportOption As String = "WORD"
            Dim extension As RenderingExtension = rviewer.LocalReport.ListRenderingExtensions().ToList().Find(Function(x) x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase))
            If extension IsNot Nothing Then
                Dim fieldInfo As System.Reflection.FieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
                fieldInfo.SetValue(extension, False)
            End If
            'End If

            'If IsExcel = False Then
            Dim exportOption2 As String = "EXCEL"
            Dim extension2 As RenderingExtension = rviewer.LocalReport.ListRenderingExtensions().ToList().Find(Function(x) x.Name.Equals(exportOption2, StringComparison.CurrentCultureIgnoreCase))
            If extension2 IsNot Nothing Then
                Dim fieldInfo2 As System.Reflection.FieldInfo = extension2.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
                fieldInfo2.SetValue(extension2, False)
            End If
            'End If
        End If
        'If IsPDF = False Then
        '    Dim exportOption3 As String = "PDF"
        '    Dim extension3 As RenderingExtension = rviewer.LocalReport.ListRenderingExtensions().ToList().Find(Function(x) x.Name.Equals(exportOption3, StringComparison.CurrentCultureIgnoreCase))
        '    If extension3 IsNot Nothing Then
        '        Dim fieldInfo3 As System.Reflection.FieldInfo = extension3.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
        '        fieldInfo3.SetValue(extension3, False)
        '    End If
        'End If
    End Sub

End Class
