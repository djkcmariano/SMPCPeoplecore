Imports System.Data
Imports System.IO
Imports clsLib
Imports AjaxControlToolkit
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraReports.Web

Partial Class Secured_rptTemplateViewerDynamic
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim ReportNo As Integer
    Dim tblMain As New Table
    Dim clsArray As New clsBase.clsArray
    Dim xScript As String = ""

    Dim _ds As DataSet
    Dim ReportParamTypeNo As Integer
    Dim TableViewName As String, ReportField As String, ReportLabel As String
    Dim ReportParamNo As Integer = 0
    Dim ReportParamFieldTypeno As Integer = 0
    Dim drpFilterbyNo As New DropDownList
    Dim category As String = ""
    Dim xCategory As String = ""
    Dim RequiredField As Boolean = False

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
    Dim report As New XtraReport
    Dim IsView As Boolean = False

    Protected Sub drpFilterbyNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
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

        pForm.Controls.Add(New LiteralControl("<div class='form-group'>"))
        lbl.Text = ReportLabel
        lbl.ID = "lbl" & ReportParamNo
        If RequiredField = True Then
            lbl.CssClass = "col-md-4 control-label has-required"
        Else
            lbl.CssClass = "col-md-4 control-label has-space"
        End If
        pForm.Controls.Add(lbl)


        If ReportParamFieldTypeno = 2 Then
            pForm.Controls.Add(New LiteralControl("<div class='col-md-2'>"))
        Else
            pForm.Controls.Add(New LiteralControl("<div class='col-md-5'>"))
        End If

        If ReportParamTypeNo = 1 Then
            If ReportField = "filterbyno" Then
                If RequiredField = True Then
                    drpFilterbyNo.CssClass = "form-control required"
                Else
                    drpFilterbyNo.CssClass = "form-control"
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
                
                If RequiredField = True Then
                    txtName.CssClass = "form-control required"
                Else
                    txtName.CssClass = "form-control"
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


        pForm.Controls.Add(New LiteralControl("</div>"))
        pForm.Controls.Add(New LiteralControl("</div>"))

    End Sub
    Private Sub PopulateParameter()
        Dim Rowcount As Integer = 0
        _ds = SQLHelper.ExecuteDaataSet("EReportParam_Web", xPublicVar.xOnlineUseNo, ReportNo)
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

                    PopulateControls()
                Next

            End If
        End If
    End Sub
    Protected Overrides Sub OnInit(e As EventArgs)
        xPublicVar.xOnlineUseNo = Generic.ToInt(Session("onlineuserno"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        ReportNo = Generic.ToInt(Request.QueryString("ReportNo"))
        reporttitle = Generic.ToStr(Request.QueryString("reporttitle"))
        rptform = Generic.ToStr(Request.QueryString("reportname"))
        Datasource = Generic.ToStr(Request.QueryString("Datasource"))
        Permission.IsAuthenticated()

        PopulateParameter()

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        clsArray.myPage.Pagename = Request.ServerVariables("SCRIPT_NAME")
        clsArray.myPage.Pagename = clsArray.GetPath(clsArray.myPage.Pagename)
        xScript = clsArray.myPage.Pagename

        If Generic.ToBol(Me.txtIsView.Checked) = True Then
            PopulateServerReport()
        End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub
    Protected Sub callbackPanel_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase)
        Me.txtIsView.Checked = True
    End Sub
    Private Sub PopulateServerReport()

        Dim ds As New DataSet
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

        ds = SQLHelper.ExecuteDataSet("EReportParam_Web", xPublicVar.xOnlineUseNo, ReportNo)
        If ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                Rowcount = _ds.Tables(0).Rows.Count
                Dim parm(Rowcount + 1) As Object

                ASPxDocumentViewer1.Report = Nothing

                Dim mpath As String = Server.MapPath("../ReportsDx/")
                Dim FilePath As String = mpath & rptform

                If File.Exists(FilePath) Then
                    report.LoadLayout(FilePath)
                Else
                    MessageBox.Warning("No record found to preview.", Me)
                    Exit Sub
                End If

                report.RequestParameters = False
                report.Parameters(0).Value = xPublicVar.xOnlineUseNo
                report.Parameters(0).Visible = False
                report.Parameters(1).Value = PayLocNo
                report.Parameters(1).Visible = False
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
                            report.Parameters(forderlevel).Value = fpid
                            report.Parameters(forderlevel).Visible = False
                        Else
                            ftxt = Generic.ToStr(txt.Text)
                            report.Parameters(forderlevel).Value = ftxt
                            report.Parameters(forderlevel).Visible = False
                        End If
                    ElseIf Not drp Is Nothing Then
                        fpid = Generic.ToInt(drp.SelectedValue)
                        report.Parameters(forderlevel).Value = fpid
                        report.Parameters(forderlevel).Visible = False
                    ElseIf Not Filterbyid Is Nothing Then
                        'fpid = Generic.ToInt(Filterbyid.SelectedValue)
                        If txtName.Text = "" Then
                            fpid = 0
                        Else
                            fpid = Generic.ToInt(hifNo.Value)
                        End If
                        report.Parameters(forderlevel).Value = fpid
                        report.Parameters(forderlevel).Visible = False
                    ElseIf Not Filterbyno Is Nothing Then
                        fpid = Generic.ToInt(Filterbyno.SelectedValue)
                        report.Parameters(forderlevel).Value = fpid
                        report.Parameters(forderlevel).Visible = False
                    End If

                Next
            End If
        End If

        'Session(xScript & "ShowReport") = False
        ASPxDocumentViewer1.Report = report
        report.CreateDocument()

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

End Class
