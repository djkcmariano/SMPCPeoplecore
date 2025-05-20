Imports clsLib
Imports System.Data
Partial Class SecuredManager_SelfAppRandomAnsApprList_QS
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim ActionStatNo As Integer = 2
    Dim dtVal As DataTable
    Dim clsGen As New clsGenericClass
    Dim mrno As Integer = 0
    Dim applicantno As Integer = 0

    Private Sub PopulateData()
        Dim str As String = ""
        Try
            Dim ds As DataSet
            Dim dt As DataTable
            ds = SQLHelper.ExecuteDataSet("EApplicantRandomAns_WebDisplay", UserNo, applicantno, mrno)
            dt = ds.Tables(0)
            For Each row As DataRow In dt.Rows
                str = str & "<div class='form-group'>"
                If Generic.ToInt(row("WithHeader")) = 0 Then
                    str = str & "<label class='col-md-3'>" & Generic.ToStr(row("Title")) & "</label>"
                    str = str & "<span class='col-md-9'>" & Generic.ToStr(row("Value")) & "</span>"
                ElseIf Generic.ToInt(row("WithHeader")) = 1 Then

                End If
                str = str & "</div>"
            Next



            'Dim dtGroup As DataTable
            'dtGroup = ds.Tables(1) '.DefaultView.ToTable(True, "Title", "Value")
            'If dtGroup.Rows.Count > 0 Then
            '    str = str & "<div class='form-group'><div class='row'><div class='col-md-12 header'>Qualification Standard</div></div>"
            '    grdQS.DataSource = dtGroup
            '    grdQS.DataBind()
            'End If

            'If ds.Tables(2).Rows.Count > 0 Then
            '    'str = str & "<div class='form-group'><div class='row'><div class='col-md-12 header'>Skills and Competencies</div>"
            '    grdComp.DataSource = ds.Tables(2)
            '    grdComp.DataBind()
            'End If

            str = str & "</div>"
            lContent.Text = str

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EApplicantResponsibility_Web", UserNo, TransNo)
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


        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateQS()
        Dim str As String = ""
        'Try
        Dim ds As DataSet
        Dim dt As DataTable
        ds = SQLHelper.ExecuteDataSet("EMR_Display", applicantno, mrno)
        dt = ds.Tables(0)
        'For Each row As DataRow In dt.Rows
        '    str = str & "<div class='form-group'>"
        '    If Generic.ToInt(row("WithHeader")) = 0 Then
        '        str = str & "<label class='col-md-3'>" & Generic.ToStr(row("Title")) & "</label>"
        '        str = str & "<span class='col-md-9'>" & Generic.ToStr(row("Value")) & "</span>"
        '    ElseIf Generic.ToInt(row("WithHeader")) = 1 Then
        '        str = str & "<div class='row'><div class='col-md-12 header'>" & Generic.ToStr(row("Title")) & "</div></div>"
        '        str = str & "<div class='row'><div class='col-md-10'>" & Generic.ToStr(row("Value")) & "</div></div><br />"
        '    End If
        '    str = str & "</div>"
        'Next


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

        'Dim dtGroup As DataTable
        'dtGroup = ds.Tables(1) '.DefaultView.ToTable(True, "Title", "Value")
        'If dtGroup.Rows.Count > 0 Then
        '    'str = str & "<div class='form-group'><div class='row'><div class='col-md-12 header'>Qualification Standard</div></div>"
        '    grdQS.DataSource = dtGroup
        '    grdQS.DataBind()
        'End If

        grdQS.DataSource = ds.Tables(1)
        grdQS.DataBind()

        'Begin Taken
        Dim xds As New DataSet
        xds = SQLHelper.ExecuteDataSet("EApplicant_QSDisplay", applicantno)
        If xds.Tables(0).Rows.Count = 0 Then
            lblEduc.Visible = False
        Else
            lblEduc.Visible = True
        End If
        grdEducTaken.DataSource = xds.Tables(0)
        grdEducTaken.DataBind()

        If xds.Tables(3).Rows.Count = 0 Then
            lblExam.Visible = False
        Else
            lblExam.Visible = True
        End If
        grdExamTaken.DataSource = xds.Tables(3)
        grdExamTaken.DataBind()

        If xds.Tables(1).Rows.Count = 0 Then
            lblExpe.Visible = False
        Else
            lblExpe.Visible = True
        End If
        grdExpeTaken.DataSource = xds.Tables(1)
        grdExpeTaken.DataBind()

        If xds.Tables(2).Rows.Count = 0 Then
            lblTrn.Visible = False
        Else
            lblTrn.Visible = True
        End If
        grdTrainingTaken.DataSource = xds.Tables(2)
        grdTrainingTaken.DataBind()
        'End Taken


        Try
            If ds.Tables(2).Rows.Count > 0 Then
                'str = str & "<div class='form-group'><div class='row'><div class='col-md-12 header'>Skills and Competencies</div>"
                grdComp.DataSource = ds.Tables(2)
                grdComp.DataBind()
            End If
        Catch ex As Exception

        End Try


        'str = str & "</div>"
        'lContent.Text = str
        'Catch ex As Exceptions

        'End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        mrno = Generic.ToInt(Request.QueryString("mrno"))
        applicantno = Generic.ToInt(Request.QueryString("applicantno"))

        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then

            lbl.Text = Generic.ToStr(SQLHelper.ExecuteScalar("SELECT Fullname FROM EApplicant WHERE ApplicantNo=" & applicantno))
            PopulateGrid()
            PopulateData()
            PopulateQS()
        End If
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lblApplicantResponsibilityNo As New Label, lblExp_Credited As New TextBox, lblDepartmentdesc As New TextBox, lblPositionDesc As New TextBox
        Dim lblDocPresented As New TextBox, lblPeriodFrom As New TextBox, lblPeriodTo As New TextBox, lblWeighted As New TextBox, lblexp As New TextBox
        Dim tcount As Integer, SaveCount As Integer = 0
        Dim xds As New DataSet



        For tcount = 0 To Me.grdMain.Rows.Count - 1
            lblApplicantResponsibilityNo = CType(grdMain.Rows(tcount).FindControl("lblApplicantResponsibilityNo"), Label)
            lblExp_Credited = CType(grdMain.Rows(tcount).FindControl("lblExp_Credited"), TextBox)
            lblDepartmentdesc = CType(grdMain.Rows(tcount).FindControl("lblDepartmentDesc"), TextBox)
            lblPositionDesc = CType(grdMain.Rows(tcount).FindControl("lblPositionDesc"), TextBox)
            lblDocPresented = CType(grdMain.Rows(tcount).FindControl("lblDocPresented"), TextBox)
            lblPeriodFrom = CType(grdMain.Rows(tcount).FindControl("lblPeriodFrom"), TextBox)
            lblPeriodTo = CType(grdMain.Rows(tcount).FindControl("lblPeriodTo"), TextBox)
            lblWeighted = CType(grdMain.Rows(tcount).FindControl("lblWeighted"), TextBox)
            lblexp = CType(grdMain.Rows(tcount).FindControl("lblexp"), TextBox)

            SQLHelper.ExecuteNonQuery("EApplicantResponsibility_WebSave", UserNo, Generic.ToInt(lblApplicantResponsibilityNo.Text), TransNo, Generic.ToStr(lblExp_Credited.Text), Generic.ToStr(lblDepartmentdesc.Text),
                                      Generic.ToStr(lblPositionDesc.Text), Generic.ToStr(lblDocPresented.Text), Generic.ToStr(lblPeriodFrom.Text), Generic.ToStr(lblPeriodTo.Text), Generic.ToInt(lblWeighted.Text),
                                      Generic.ToStr(lblexp.Text))
            SaveCount = SaveCount + 1

        Next

        If SaveCount > 0 Then
            PopulateGrid()
            MessageBox.Success("(" & SaveCount & ") " & MessageTemplate.SuccessUpdate, Me)
        End If


    End Sub

    Protected Sub grdComp_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdComp.RowCreated
        e.Row.Cells(0).Visible = False
        e.Row.Cells(1).Visible = False
        e.Row.Cells(2).Visible = False
        'e.Row.Cells(3).Visible = False
    End Sub

    Protected Sub btnUpdateQS_Click(sender As Object, e As EventArgs)
        'save QS
        Dim hifQSTypeNo As New HiddenField
        Dim hifQSNo As New HiddenField
        Dim hifApplicantQSNo As New HiddenField
        'Dim chk As New CheckBox
        Dim x As Integer = 0
        Dim rbl As New RadioButtonList
        Dim txtR As New TextBox
        Dim txtN As New TextBox
        For i = 0 To grdQS.Rows.Count - 1
            hifQSTypeNo = CType(grdQS.Rows(i).FindControl("hifQSTypeNo"), HiddenField)
            hifQSNo = CType(grdQS.Rows(i).FindControl("hifQSNo"), HiddenField)
            hifApplicantQSNo = CType(grdQS.Rows(i).FindControl("hifApplicantQSNo"), HiddenField)
            rbl = CType(grdQS.Rows(i).FindControl("rbl"), RadioButtonList)
            txtR = CType(grdQS.Rows(i).FindControl("txtRemarks"), TextBox)
            txtN = CType(grdQS.Rows(i).FindControl("txtValue"), TextBox)
            'If chk.Checked Then
            'If rbl.SelectedValue <> "0" Then
            x = Generic.ToInt(SQLHelper.ExecuteNonQuery("EApplicantQS_WebSave", Generic.ToInt(Session("OnlineUserNo")), Generic.ToInt(hifApplicantQSNo.Value), applicantno, Generic.ToInt(hifQSTypeNo.Value), Generic.ToInt(hifQSNo.Value), txtR.Text, Generic.ToDbl(txtN.Text), Generic.ToInt(rbl.SelectedValue))) + i
            'End If
            'End If
        Next
        If x > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub grdQS_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdQS.RowDataBound
        Dim hif As New HiddenField
        Dim rbl As New RadioButtonList
        If e.Row.RowType = DataControlRowType.DataRow Then
            'hif = CType(e.Row.FindControl("hifIsPass"), HiddenField)
            hif = CType(e.Row.FindControl("hifIsComplied"), HiddenField)
            rbl = CType(e.Row.FindControl("rbl"), RadioButtonList)

            If Not IsNothing(rbl) Then
                If hif.Value = "True" Then
                    hif.Value = "1"
                ElseIf hif.Value = "False" Then
                    hif.Value = "0"
                End If
                rbl.SelectedValue = hif.Value
            End If

        End If
    End Sub
End Class
