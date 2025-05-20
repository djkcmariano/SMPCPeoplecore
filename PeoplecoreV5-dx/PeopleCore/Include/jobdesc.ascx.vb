Imports System.Data
Imports clsLib

Partial Class include_jobdesc
    Inherits System.Web.UI.UserControl
    Dim UserNo As Integer = 0
    Dim xMessage As String = ""

    Private _JDNo As Integer
    Public Property JDNo() As Integer
        Get
            Return _JDNo
        End Get
        Set(value As Integer)
            _JDNo = value
        End Set
    End Property

    Private _PlantillaNo As Integer
    Public Property PlantillaNo() As Integer
        Get
            Return _PlantillaNo
        End Get
        Set(value As Integer)
            _PlantillaNo = value
        End Set
    End Property

    Private _PositionNo As Integer
    Public Property PositionNo() As Integer
        Get
            Return _PositionNo
        End Get
        Set(value As Integer)
            _PositionNo = value
        End Set
    End Property


    Public Sub Show()
        'Dim pJDNo As Integer = 0
        'Dim pPlantillaNo As Integer = 0
        'pJDNo = JDNo
        'pPlantillaNo = PlantillaNo
        'PopulateGridEduc(pJDNo)
        'PopulateGridExpe(pJDNo)
        'PopulateGridComp(pJDNo)
        'PopulateGridEligibility(pJDNo)
        'PopulateGridTraining(pJDNo)        

        UserNo = Session("EmployeeNo")
        hifMRNo.Value = JDNo
        If UserNo = 0 Then
            'lnkApply.Visible = False
            lnkSave.Visible = False
        Else
            'lnkApply.Visible = True
            lnkSave.Visible = True
        End If

        PopulateData(Generic.ToInt(hifMRNo.Value))


        Try
            'cboVacancySourceNo.DataSource =
            'cboVacancySourceNo.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", -99, "EVacancySource", 0, "", "")
            'cboVacancySourceNo.DataTextField = "tdesc"
            'cboVacancySourceNo.DataValueField = "tNo"
            'cboVacancySourceNo.DataBind()
        Catch ex As Exception

        End Try

        'lblDisc.Text = Generic.ToStr(SQLHelper.ExecuteScalar("SELECT dbo.EGetDisclaimer()"))

        ModalPopupExtender1.Show()
    End Sub

    'Protected Sub chkAgree_CheckedChanged()
    '    If chkAgree.Checked Then
    '        lnkSave.Enabled = True
    '    Else
    '        lnkSave.Enabled = False
    '    End If
    '    ModalPopupExtender1.Show()
    'End Sub

    Private Sub PopulateData(JDNo As Integer)
        Dim str As String = ""
        'Try
        Dim ds As DataSet
        Dim dt As DataTable
        ds = SQLHelper.ExecuteDataSet("EMR_Display", UserNo, JDNo)
        dt = ds.Tables(0)

        For Each row As DataRow In dt.Rows
            str = str & "<div class='form-group'>"
            If Generic.ToInt(row("WithHeader")) = 0 Then
                str = str & "<label class='col-md-3'>" & Generic.ToStr(row("Title")) & "</label>"
                str = str & "<span class='col-md-9'>" & Generic.ToStr(row("Value")) & "</span>"
            ElseIf Generic.ToInt(row("WithHeader")) = 1 Then
                str = str & "<div class='row'><div class='col-md-12 header'>" & Generic.ToStr(row("Title")) & "</div></div>"
                str = str & "<div class='row'><div class='col-md-10'>" & Generic.ToStr(row("Value")) & "</div></div><br />"
            End If
            str = str & "</div>"
        Next


        'Dim dtGroup As DataTable
        'dtGroup = ds.Tables(1).DefaultView.ToTable(True, "Title")
        'If dtGroup.Rows.Count > 0 Then : str = str & "<div class='form-group'><div class='row'><div class='col-md-10 header'>Qualification Standard</div></div>" : End If
        'For Each rowGroup As DataRow In dtGroup.Rows
        '    str = str & "<div class='row'><div class='col-md-8'><b>" & Generic.ToStr(rowGroup("Title")) & "</b></div></div><ul>"
        '    For Each row As DataRow In ds.Tables(1).Select("Title='" & rowGroup("Title") & "'")
        '        str = str & "<li>" & Generic.ToStr(row("Value")) & "</li>"
        '    Next
        '    str = str & "</ul>"
        'Next
        'str = str & "</div>"

        Dim dtGroup As DataTable
        dtGroup = ds.Tables(1) '.DefaultView.ToTable(True, "Title", "Value")
        If dtGroup.Rows.Count > 0 Then            
            str = str & "<div class='form-group'><div class='row'><div class='col-md-12 header'>Qualification Standard</div></div>"
            grdQS.DataSource = ds.Tables(1)
            grdQS.DataBind()

            If ds.Tables(1).Select("Title='Education'").Count > 0 Then
                grdEduc.DataSource = ds.Tables(1).Select("Title='Education'").CopyToDataTable
                grdEduc.DataBind()
            Else
                grdEduc.DataSource = ds.Tables(1).Select("Title='Education'").Clone
                grdEduc.DataBind()
            End If

            If ds.Tables(1).Select("Title='Work Experience'").Count > 0 Then
                grdExpe.DataSource = ds.Tables(1).Select("Title='Work Experience'").CopyToDataTable
                grdExpe.DataBind()
            Else
                grdExpe.DataSource = ds.Tables(1).Select("Title='Work Experience'").Clone
                grdExpe.DataBind()
            End If

            If ds.Tables(1).Select("Title='Training'").Count > 0 Then
                grdTraining.DataSource = ds.Tables(1).Select("Title='Training'").CopyToDataTable
                grdTraining.DataBind()
            Else
                grdTraining.DataSource = ds.Tables(1).Select("Title='Training'").Clone
                grdTraining.DataBind()
            End If

            If ds.Tables(1).Select("Title='Eligibility'").Count > 0 Then
                grdExam.DataSource = ds.Tables(1).Select("Title='Eligibility'").CopyToDataTable
                grdExam.DataBind()
            Else
                grdExam.DataSource = ds.Tables(1).Select("Title='Eligibility'").Clone
                grdExam.DataBind()
            End If
        Else
            grdEduc.DataSource = Nothing
            grdEduc.DataBind()

            grdExpe.DataSource = Nothing
            grdExpe.DataBind()

            grdTraining.DataSource = Nothing
            grdTraining.DataBind()

            grdExam.DataSource = Nothing
            grdExam.DataBind()
        End If


        'Begin Taken
        '    Dim xds As New DataSet
        '    xds = SQLHelper.ExecuteDataSet("EApplicant_QSDisplay", Generic.ToInt(Session("OnlineApplicantNo")))
        '    If xds.Tables(0).Rows.Count = 0 Then
        '        lblEduc.Visible = False
        '    Else
        '        lblEduc.Visible = True
        '    End If
        '    grdEducTaken.DataSource = xds.Tables(0)
        '    grdEducTaken.DataBind()

        '    If xds.Tables(3).Rows.Count = 0 Then
        '        lblExam.Visible = False
        '    Else
        '        lblExam.Visible = True
        '    End If
        '    grdExamTaken.DataSource = xds.Tables(3)
        '    grdExamTaken.DataBind()

        '    If xds.Tables(1).Rows.Count = 0 Then
        '        lblExpe.Visible = False
        '    Else
        '        lblExpe.Visible = True
        '    End If
        '    grdExpeTaken.DataSource = xds.Tables(1)
        '    grdExpeTaken.DataBind()

        '    If xds.Tables(2).Rows.Count = 0 Then
        '        lblTrn.Visible = False
        '    Else
        '        lblTrn.Visible = True
        '    End If
        '    grdTrainingTaken.DataSource = xds.Tables(2)
        '    grdTrainingTaken.DataBind()
        '    'End Taken
        'Else
        '    grdQS.Controls.Clear()
        'End If

        'If Generic.ToInt(Session("OnlineApplicantNo")) = 0 Then
        '    lblExam.Visible = False
        '    lblExpe.Visible = False
        '    lblTrn.Visible = False
        '    lblEduc.Visible = False
        '    ph.Visible = False
        'Else
        '    lblExam.Visible = True
        '    lblExpe.Visible = True
        '    lblTrn.Visible = True
        '    lblEduc.Visible = True
        '    ph.Visible = True
        'End If

        'Try
        '    If ds.Tables(2).Rows.Count > 0 Then
        '        'str = str & "<div class='form-group'><div class='row'><div class='col-md-12 header'>Skills and Competencies</div>"
        '        grdComp.DataSource = ds.Tables(2)
        '        grdComp.DataBind()
        '        divComp.Visible = True
        '    Else
        '        divComp.Visible = False
        '    End If
        'Catch ex As Exception

        'End Try


        str = str & "</div>"
        lContent.Text = str
        'Catch ex As Exception

        'End Try
    End Sub

    'Protected Sub grdComp_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdComp.RowCreated
    '    e.Row.Cells(0).Visible = False
    '    e.Row.Cells(1).Visible = False
    '    e.Row.Cells(2).Visible = False
    '    'e.Row.Cells(3).Visible = False
    '    e.Row.Cells(4).Visible = False
    '    e.Row.Cells(5).Visible = False

    '    If grdComp.AutoGenerateColumns = True Then
    '        If e.Row.RowType = DataControlRowType.Header Then
    '            Dim thc As New TableHeaderCell
    '            thc.Controls.Add(New LiteralControl("Rating"))
    '            e.Row.Cells.Add(thc)
    '        End If
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            Dim tc As New TableCell
    '            Dim ddl As New DropDownList
    '            Dim hif As New HiddenField
    '            Dim hifCompScaleNoSelf As New HiddenField
    '            ddl.ID = "cbo"
    '            ddl.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", 0, "ECompScale", 0, "", "")
    '            ddl.DataTextField = "tdesc"
    '            ddl.DataValueField = "tNo"
    '            ddl.DataBind()
    '            ddl.Visible = DataBinder.Eval(e.Row.DataItem, "IsEnabled")

    '            hif.ID = "hifCompNo"
    '            hif.Value = Generic.ToStr(DataBinder.Eval(e.Row.DataItem, "CompNo"))

    '            hifCompScaleNoSelf.ID = "hifCompScaleNoSelf"
    '            hifCompScaleNoSelf.Value = Generic.ToStr(DataBinder.Eval(e.Row.DataItem, "CompScaleNoSelf"))

    '            tc.Controls.Add(hifCompScaleNoSelf)
    '            tc.Controls.Add(hif)
    '            tc.Controls.Add(ddl)
    '            e.Row.Cells.Add(tc)

    '            ddl.Items.FindByValue(hifCompScaleNoSelf.Value).Selected = True
    '        End If
    '    End If

    'End Sub

    Protected Sub grdComp_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdComp.RowDataBound

        '    Dim IsEnabled As String = Generic.ToStr(CType(e.Row.FindControl("hifIsEnabled"), HiddenField).Value)
        '    Dim CompScaleNo As String = Generic.ToStr(CType(e.Row.FindControl("hifCompScaleNo"), HiddenField).Value)
        '    'If CompScaleNo = 0 Then CompScaleNo = ""

        '    ddl.Items.FindByValue(CompScaleNo).Selected = True

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl As New DropDownList
            Dim hif As New HiddenField
            ddl = CType(e.Row.FindControl("cbo"), DropDownList)
            hif = CType(e.Row.FindControl("hifCompScaleNoSelf"), HiddenField)
            ddl.Items.FindByValue(hif.Value).Selected = True
        End If

        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    Dim ddl As New DropDownList

        '    ddl = CType(e.Row.FindControl("cbo"), DropDownList)
        '    ddl.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", 0, "ECompScale", 0, "", "")
        '    ddl.DataTextField = "tdesc"
        '    ddl.DataValueField = "tNo"
        '    ddl.DataBind()

        '    Dim IsEnabled As String = Generic.ToStr(CType(e.Row.FindControl("hifIsEnabled"), HiddenField).Value)
        '    Dim CompScaleNo As String = Generic.ToStr(CType(e.Row.FindControl("hifCompScaleNo"), HiddenField).Value)
        '    'If CompScaleNo = 0 Then CompScaleNo = ""

        '    ddl.Items.FindByValue(CompScaleNo).Selected = True
        '    ddl.Visible = True
        'End If

    End Sub

    Private Sub Apply()

        'If Generic.ToInt(Session("OnlineApplicantNo")) > 0 Then
        Dim tProceed As Integer = 0
        Dim fds As New DataSet

        'If Generic.ToInt(Session("EmployeeNo")) > 0 Then
        fds = SQLHelper.ExecuteDataSet("EApplicantRandomAns_WebSaveEmp", Generic.ToInt(Session("OnlineUserNo")), 0, Generic.ToInt(Session("EmployeeNo")), Generic.ToInt(hifMRNo.Value))
        If fds.Tables.Count > 0 Then
            If fds.Tables(0).Rows.Count > 0 Then
                tProceed = Generic.ToInt(fds.Tables(0).Rows(0)("tProceed"))
                xMessage = Generic.ToStr(fds.Tables(0).Rows(0)("xMessage"))
            End If
        End If
        'End If

        If tProceed = 0 Then
            MessageBox.Success("Your application has been successfully submitted.", Me)
        Else
            MessageBox.Warning(xMessage, Me)
        End If
    End Sub


    Protected Sub lnkApply_Click(sender As Object, e As System.EventArgs)
        Apply()

    End Sub

    Protected Sub grdQS_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdQS.RowDataBound      
        Dim hif As New HiddenField
        Dim rbl As New RadioButtonList
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    hif = CType(e.Row.FindControl("hifIsPass"), HiddenField)
        '    rbl = CType(e.Row.FindControl("rbl"), RadioButtonList)
        '    If Not IsNothing(rbl) Then
        '        If hif.Value = "True" Then
        '            hif.Value = "1"
        '        ElseIf hif.Value = "False" Then
        '            hif.Value = "0"
        '        End If
        '        rbl.SelectedValue = hif.Value
        '    End If

        'End If
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    'hif = CType(e.Row.FindControl("hifIsPass"), HiddenField)
        '    hif = CType(e.Row.FindControl("hifIsComplied"), HiddenField)
        '    rbl = CType(e.Row.FindControl("rbl"), RadioButtonList)

        '    If Not IsNothing(rbl) Then
        '        If hif.Value = "True" Then
        '            hif.Value = "1"
        '        ElseIf hif.Value = "False" Then
        '            hif.Value = "0"
        '        End If
        '        rbl.SelectedValue = hif.Value
        '    End If

        'End If
    End Sub

    Protected Sub lnkClose_Click(sender As Object, e As EventArgs)
        ModalPopupExtender1.Hide()

    End Sub
    Protected Sub grdEduc_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdEduc.RowDataBound
        'Dim hif As New HiddenField
        'Dim rbl As New RadioButtonList
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    hif = CType(e.Row.FindControl("hifIsPass"), HiddenField)
        '    rbl = CType(e.Row.FindControl("rbl"), RadioButtonList)
        '    If Not IsNothing(rbl) Then
        '        If hif.Value = "True" Then
        '            hif.Value = "1"
        '        ElseIf hif.Value = "False" Then
        '            hif.Value = "0"
        '        End If
        '        rbl.SelectedValue = hif.Value
        '    End If

        'End If
    End Sub

    Protected Sub grdExam_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdExam.RowDataBound
        'Dim hif As New HiddenField
        'Dim rbl As New RadioButtonList
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    hif = CType(e.Row.FindControl("hifIsPass"), HiddenField)
        '    rbl = CType(e.Row.FindControl("rbl"), RadioButtonList)
        '    If Not IsNothing(rbl) Then
        '        If hif.Value = "True" Then
        '            hif.Value = "1"
        '        ElseIf hif.Value = "False" Then
        '            hif.Value = "0"
        '        End If
        '        rbl.SelectedValue = hif.Value
        '    End If

        'End If
    End Sub

    Protected Sub grdExpe_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdExpe.RowDataBound
        'Dim hif As New HiddenField
        'Dim rbl As New RadioButtonList
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    hif = CType(e.Row.FindControl("hifIsPass"), HiddenField)
        '    rbl = CType(e.Row.FindControl("rbl"), RadioButtonList)
        '    If Not IsNothing(rbl) Then
        '        If hif.Value = "True" Then
        '            hif.Value = "1"
        '        ElseIf hif.Value = "False" Then
        '            hif.Value = "0"
        '        End If
        '        rbl.SelectedValue = hif.Value
        '    End If

        'End If
    End Sub

    Protected Sub grdTraining_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdTraining.RowDataBound
        'Dim hif As New HiddenField
        'Dim rbl As New RadioButtonList
        'If e.Row.RowType = DataControlRowType.DataRow Then
        '    hif = CType(e.Row.FindControl("hifIsPass"), HiddenField)
        '    rbl = CType(e.Row.FindControl("rbl"), RadioButtonList)
        '    If Not IsNothing(rbl) Then
        '        If hif.Value = "True" Then
        '            hif.Value = "1"
        '        ElseIf hif.Value = "False" Then
        '            hif.Value = "0"
        '        End If
        '        rbl.SelectedValue = hif.Value
        '    End If

        'End If
    End Sub
End Class
