Imports clsLib
Imports System.Data

Partial Class Secured_PY_Detl
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim pyno As Integer = 0
    Dim transNo As Integer = 0
    Dim IsEnabled As Boolean = False
    Dim payclassNo As Integer = 0

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPYDeti_WebOne", UserNo, Generic.ToInt(transNo))
        For Each row As DataRow In dt.Rows
            Generic.PopulateDropDownList_Union(UserNo, Me, "panel1", dt, PayLocNo)
            Generic.PopulateData(Me, "panel1", dt)
        Next
        sub_ActivityType(Generic.ToInt(cboPYActivityTypeNo.Text))
    End Sub
    Private Sub PopulateGridDetl(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPYDeti_Web", UserNo, id)
        grdDetl.DataSource = dt
        grdDetl.DataBind()
    End Sub
    Private Sub PopulateDropDown()
        If transNo = 0 Then
            Generic.PopulateDropDownList(UserNo, Me, "panel1", PayLocNo)
        End If
        DayType(payclassNo)
        
    End Sub
    Private Sub DayType(ino As Integer)
        Try
            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet("EDayType_WebLookup_PayClass", UserNo, Generic.ToInt(ino))
            cboDayTypeNo.DataSource = ds
            cboDayTypeNo.DataTextField = "tDesc"
            cboDayTypeNo.DataValueField = "tNo"
            cboDayTypeNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub hideFields()
        Dim Columname As String = "", script As String = ""
        Dim ds As DataSet = SQLHelper.ExecuteDataSet("EPYDeti_Web_GetFields")
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Columname = Generic.ToStr(ds.Tables(0).Rows(i)("Columname")).ToLower
                    script = "script" & Columname.ToString
                    fRegisterStartupScript(script, "getselectedvalue_none('" + Columname.ToString + "');")
                Next

            End If

        End If

    End Sub
    Private Sub showFields(tID As Integer)
        hideFields()
        Dim Columname As String = "", script As String = ""
        Dim ds As DataSet = SQLHelper.ExecuteDataSet("EPYFieldShow_Web_Check", tID)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Columname = Generic.ToStr(ds.Tables(0).Rows(i)("ColumnName")).ToLower
                    script = "scriptshow" & Columname.ToString
                    fRegisterStartupScript(script, "getselectedvalue_display('" + Columname.ToString + "');")
                Next

            End If

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("onlineuserno"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        transNo = Generic.ToInt(Request.QueryString("Id"))
        payclassNo = Generic.ToInt(Request.QueryString("payclassNo"))
        pyno = Generic.ToInt(Request.QueryString("pyno"))
        AccessRights.CheckUser(UserNo, "py.aspx", "epy")
        If transNo = 0 Then : ViewState("IsEnabled") = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If

        If Not IsPostBack Then
            PopulateData()
            PopulateDropDown()
            PopulateGridDetl(pyno)
        End If

   
        EnabledControls()
        PopulateQuestion(Generic.ToInt(cboPYActivityTypeNo.SelectedValue))
        hideFields()
        showFields(Generic.ToInt(cboPYActivityTypeNo.SelectedValue))
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)
        Generic.EnableControls(Me, "Panel2", IsEnabled)
        Generic.PopulateDataDisabled(Me, "Panel1", UserNo, PayLocNo, Generic.ToStr(Session("xMenuType")))
        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
    End Sub
    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "py.aspx", "epy") Then
            ViewState("IsEnabled") = True
            If txtIsposted.Checked Then
                MessageBox.Information(MessageTemplate.PostedTransaction, Me)
            End If
            EnabledControls()

        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
        'PopulateQuestion(Generic.ToInt(cboPYActivityTypeNo.SelectedValue))
    End Sub
    'Submit record
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        SaveRecordDetl()
        SavePY_Dynamic(Generic.ToInt(cboPYActivityTypeNo.SelectedValue))
    End Sub

    Private Function SaveRecordDetl() As Boolean

        Dim employeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim pyActivityTypeNo As Integer = Generic.ToInt(cboPYActivityTypeNo.SelectedValue)
        Dim PYActivityTypeDetiNo As Integer = Generic.ToInt(cbopyactivitytypedetino.SelectedValue)
        Dim quantity As Double = Generic.ToDec(txtQuantity.Text)
        Dim DayTypeNo As Integer = Generic.ToInt(cboDayTypeNo.SelectedValue)
        Dim documentCode As String = Generic.ToStr(txtdocumentcode.Text)
        Dim dt As New DataTable, error_num As Integer = 0, error_message As String = "", retVal As Boolean = False
        Dim hrs As Double = Generic.ToDec(txtHrs.Text)

        dt = SQLHelper.ExecuteDataTable("EPYDeti_WebSave", UserNo, Generic.ToInt(txtPYDetiNo.Text), pyno, employeeNo, pyActivityTypeNo, PYActivityTypeDetiNo, quantity, DayTypeNo, payclassNo, documentCode, hrs)
        For Each row As DataRow In dt.Rows
            retVal = True
            transNo = Generic.ToInt(row("pydetino"))
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                retVal = False
            End If

        Next
        If retVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        Else
            Generic.ClearControls(Me, "panel1")
            PopulateGridDetl(pyno)
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
        'If Generic.ToInt(Request.QueryString("id")) = 0 Then
        '    Dim url As String = "PY_Detl.aspx?id=" & transNo
        '    MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        'Else
        '    MessageBox.Success(MessageTemplate.SuccessSave, Me)
        '    ViewState("IsEnabled") = False
        '    EnabledControls()
        'End If
        'Dim url As String = "PY.aspx?id=" & transNo
        'MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)

        'PopulateData()
    End Function

    Private Sub SavePY_Dynamic(pyactivitytypeno As Integer)
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPYFieldShowDynamic_WebPopulate", UserNo, pyactivitytypeno)
            Dim dtQuestion As DataTable
            Dim i As Integer = 1
            Dim pyFieldShowDynamicNo As String = 0, Result As String = "", pydetidynamicNo As Integer = 0

            dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PYFieldshowDynamicNo", "ColumnDesc", "ResponseTypeNo", "ResponseTypeCode", "IsRequired")

            For Each rowQuestion As DataRow In dtQuestion.Rows
                pyFieldShowDynamicNo = Generic.ToStr(rowQuestion("pyFieldShowDynamicNo"))
                Select Case rowQuestion("ResponseTypeCode")
                    Case "NT"
                        Dim txt As New TextBox
                        txt = Panel2.FindControl("txt" & pyFieldShowDynamicNo.ToString)
                        Result = txt.Text
                        pydetidynamicNo = txt.TabIndex

                    Case "N"
                         Dim txt As New TextBox
                        txt = CType(Panel2.FindControl("txt" & pyFieldShowDynamicNo), TextBox)
                        Result = txt.Text
                        pydetidynamicNo = txt.TabIndex
                End Select
                i = i + 1
                If Generic.ToStr(rowQuestion("ResponseTypeCode")) <> "" Then
                    SQLHelper.ExecuteNonQuery("EPYDetiDynamic_WebSave", UserNo, Generic.ToInt(pydetidynamicNo), pyno, Generic.ToInt(pyFieldShowDynamicNo), Generic.ToStr(Result))
                End If

            Next


        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkDeleteDetl_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "py.aspx", "EPY") Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PYDetiNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPYDeti", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGridDetl(Generic.ToInt(ViewState("TransNo")))

            If i > 0 Then
                PopulateGridDetl(pyno)
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub cbopyactivitytype_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Dim tval As Integer = Generic.ToInt(cboPYActivityTypeNo.SelectedValue)
        showFields(tval)
        sub_ActivityType(tval)
        ' PopulateQuestion(tval)
    End Sub

    Private Sub sub_ActivityType(ino As Integer)

        Try
            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet("EPYActivityTypeDeti_WebLookup", UserNo, Generic.ToInt(ino))
            cbopyactivitytypedetino.DataSource = ds
            cbopyactivitytypedetino.DataTextField = "tDesc"
            cbopyactivitytypedetino.DataValueField = "tNo"
            cbopyactivitytypedetino.DataBind()

        Catch ex As Exception
        End Try
    End Sub
    
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub

#Region "******** Populate ********"
    Private Sub PopulateQuestion(pyactivitytypeno As Integer)
        Try
            Dim ds As New DataSet, dsA As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPYFieldShowDynamic_WebPopulate", UserNo, pyactivitytypeno)
            dsA = SQLHelper.ExecuteDataSet("EPYDetiDynamic_Web", UserNo, pyno)
            Dim dtQuestion As DataTable
            Dim dtA As DataTable

            Dim i As Integer = 1
            Dim pyFieldShowDynamicNo As Integer = 0, result As String = "", pydetidynamicNo As Integer = 0

            dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PYFieldshowDynamicNo", "ColumnDesc", "ResponseTypeNo", "ResponseTypeCode", "IsRequired")
            dtA = dsA.Tables(0).DefaultView.ToTable(True, "PYFieldshowDynamicNo", "ColumnDesc", "pyNo", "pydetidynamicNo", "result")

            For Each rowQuestion As DataRow In dtQuestion.Rows
                Panel2.Controls.Add(New LiteralControl("<div class='form-group'>"))
                Panel2.Controls.Add(New LiteralControl("<label class='col-md-4 control-label has-space'>" & Generic.ToStr(rowQuestion("ColumnDesc")) & " : "))
                Panel2.Controls.Add(New LiteralControl("</label>"))

                pyFieldShowDynamicNo = Generic.ToInt(rowQuestion("pyFieldShowDynamicNo"))
                For Each rowA As DataRow In dtA.Select("pyNo=" & pyno.ToString & " And PYFieldshowDynamicNo=" & Generic.ToStr(rowQuestion("PYFieldshowDynamicNo")))
                    result = Generic.ToStr(rowA("result"))
                    pydetidynamicNo = Generic.ToInt(rowA("pydetidynamicNo"))
                Next
                Select Case rowQuestion("ResponseTypeCode")
                    Case "NT"
                        Panel2.Controls.Add(New LiteralControl("<div class='col-md-7'>"))
                        Dim txt As New TextBox
                        txt.ID = "txt" & pyFieldShowDynamicNo
                        txt.TextMode = TextBoxMode.MultiLine
                        txt.Rows = 4
                        txt.Text = result.ToString
                        txt.TabIndex = pydetidynamicNo
                        txt.CssClass = "form-control"
                        txt.Enabled = Generic.ToBol(ViewState("IsEnabled"))
                        Panel2.Controls.Add(txt)
                        If rowQuestion("IsRequired") Then
                            Dim rf As New RequiredFieldValidator
                            rf.ID = "rf" & pyFieldShowDynamicNo
                            rf.Display = ValidatorDisplay.Dynamic
                            rf.ControlToValidate = txt.ID
                            rf.Text = "*"
                            rf.ValidationGroup = "EvalValidationGroup"
                            Panel2.Controls.Add(rf)
                        End If

                    Case "N"
                        Panel2.Controls.Add(New LiteralControl("<div class='col-md-3'>"))
                        Dim txt As New TextBox
                        txt.ID = "txt" & pyFieldShowDynamicNo
                        txt.Text = result.ToString
                        txt.TabIndex = pydetidynamicNo
                        txt.CssClass = "form-control number"
                        txt.Enabled = Generic.ToBol(ViewState("IsEnabled"))
                        If rowQuestion("IsRequired") Then
                            Dim rf As New RequiredFieldValidator
                            rf.ID = "rf" & pyFieldShowDynamicNo.ToString()
                            rf.Display = ValidatorDisplay.Dynamic
                            rf.ControlToValidate = txt.ID
                            rf.Text = "*"
                            rf.ValidationGroup = "EvalValidationGroup"
                            Panel2.Controls.Add(rf)
                        End If
                        Panel2.Controls.Add(txt)
                       
                End Select
                i = i + 1

                Panel2.Controls.Add(New LiteralControl("</div>"))
                Panel2.Controls.Add(New LiteralControl("</div>"))

            Next


        Catch ex As Exception

        End Try

    End Sub
#End Region
End Class
