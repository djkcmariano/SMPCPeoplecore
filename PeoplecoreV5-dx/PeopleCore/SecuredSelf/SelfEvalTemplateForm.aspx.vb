Imports System.Data
Imports clsLib

Partial Class SecuredSelf_SelfEvalTemplateForm
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim TemplateID As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer = 0
    Dim ApplicantNo As Integer = 0
    Dim EmployeeNo As Integer = 0
    Dim FormName As String = ""
    Dim TableName As String = ""

    Protected Overrides Sub OnInit(e As EventArgs)
        MyBase.OnInit(e)
        TemplateID = Generic.ToInt(Request.QueryString("TemplateID"))
        ApplicantNo = Generic.ToInt(Request.QueryString("app"))
        EmployeeNo = Generic.ToInt(Request.QueryString("emp"))
        TransNo = Generic.ToInt(Request.QueryString("TransNo"))

        PopulateQuestion(TemplateID)
        PopulateAnswer()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'UserNo = Generic.ToInt(Session("OnlineUserNo"))
        'FormName = Generic.ToStr(Request.QueryString("FormName"))
        'TableName = Generic.ToStr(Request.QueryString("TableName"))
        'AccessRights.CheckUser(UserNo, FormName, TableName)

        Permission.IsAuthenticated()

        If ApplicantNo = 0 And EmployeeNo = 0 And TransNo = 0 Then
            lnkSave.Visible = False
        End If

    End Sub

#Region "******** Populate ********"
    Private Sub PopulateQuestion(TemplateID As Integer)
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EEvalTemplateDetl_Form", UserNo, TemplateID)
            Dim dtCategory As DataTable
            Dim dtQuestion As DataTable
            Dim i As Integer = 1
            dtCategory = ds.Tables(0).DefaultView.ToTable(True, "EvalTemplateCateNo", "EvalTemplateCateDesc")
            dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "EvalTemplateDetlNo", "EvalTemplateCateNo", "Question", "HasComment", "IsRequired", "ResponseTypeCode")

            pForm.Controls.Add(New LiteralControl("<div class='row'>"))
            pForm.Controls.Add(New LiteralControl("<h3 style='text-align:center;'>" & Generic.ToStr(ds.Tables(0).Rows(0)("EvalTemplateDesc")) & "</h3>"))
            pForm.Controls.Add(New LiteralControl("</div>"))
            For Each rowCategory As DataRow In dtCategory.Rows
                pForm.Controls.Add(New LiteralControl("<div class='row'>"))
                pForm.Controls.Add(New LiteralControl("<h5>" & Generic.ToStr(rowCategory("EvalTemplateCateDesc")) & "</h5>"))
                For Each rowQuestion As DataRow In dtQuestion.Select("EvalTemplateCateNo=" & Generic.ToStr(rowCategory("EvalTemplateCateNo")))
                    pForm.Controls.Add(New LiteralControl("<div class='row'>"))
                    pForm.Controls.Add(New LiteralControl("<div class='col-md-12'>" & i.ToString() & ". " & Generic.ToStr(rowQuestion("Question"))))
                    Dim EvalTemplateDetlNo As Integer = Generic.ToInt(rowQuestion("EvalTemplateDetlNo"))
                    pForm.Controls.Add(New LiteralControl("<div style='padding:0 0 0 30px'>"))
                    Select Case rowQuestion("ResponseTypeCode")
                        Case "RB"
                            Dim rbl As New RadioButtonList
                            rbl.ID = "rbl" & EvalTemplateDetlNo
                            rbl.RepeatLayout = RepeatLayout.Table
                            For Each rowChoice As DataRow In ds.Tables(0).Select("EvalTemplateDetlNo=" & EvalTemplateDetlNo)
                                Dim li As New ListItem
                                li.Text = "&nbsp;&nbsp;" & Generic.ToStr(rowChoice("EvalTemplateDetlChoiceDesc"))
                                li.Value = Generic.ToInt(rowChoice("EvalTemplateDetlChoiceNo"))
                                If Generic.ToInt(li.Value) > 0 Then
                                    rbl.Items.Add(li)
                                End If
                            Next
                            If rowQuestion("IsRequired") Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & EvalTemplateDetlNo
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = rbl.ID
                                rf.Text = "*"
                                rf.ValidationGroup = "EvalValidationGroup"
                                pForm.Controls.Add(rf)
                            End If
                            pForm.Controls.Add(rbl)
                        Case "NT"
                            Dim txt As New TextBox
                            txt.ID = "txt" & EvalTemplateDetlNo
                            txt.TextMode = TextBoxMode.MultiLine
                            txt.Rows = 4
                            txt.CssClass = "form-control"
                            pForm.Controls.Add(txt)
                            If rowQuestion("IsRequired") Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & EvalTemplateDetlNo
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = txt.ID
                                rf.Text = "*"
                                rf.ValidationGroup = "EvalValidationGroup"
                                pForm.Controls.Add(rf)
                            End If
                        Case "CB"
                            Dim cbl As New CheckBoxList
                            cbl.ID = "cbl" & EvalTemplateDetlNo
                            For Each rowChoice As DataRow In ds.Tables(0).Select("EvalTemplateDetlNo=" & rowQuestion("EvalTemplateDetlNo"))
                                Dim li As New ListItem
                                li.Text = "&nbsp;&nbsp;" & Generic.ToStr(rowChoice("EvalTemplateDetlChoiceDesc"))
                                li.Value = Generic.ToInt(rowChoice("EvalTemplateDetlChoiceNo"))
                                If Generic.ToInt(li.Value) > 0 Then
                                    cbl.Items.Add(li)
                                End If
                            Next
                            pForm.Controls.Add(cbl)
                        Case "DR"
                            Dim ddl As New DropDownList
                            ddl.ID = "ddl" & EvalTemplateDetlNo
                            Dim l As New ListItem
                            l.Text = "-- Select --"
                            l.Value = "0"
                            ddl.Items.Add(l)
                            For Each rowChoice As DataRow In ds.Tables(0).Select("TemplateDetiNo=" & EvalTemplateDetlNo)
                                Dim li As New ListItem
                                li.Text = Generic.ToStr(rowChoice("TemplateDetiChoiceDesc"))
                                li.Value = Generic.ToInt(rowChoice("TemplateDetiChoiceNo"))
                                If Generic.ToInt(li.Value) > 0 Then
                                    ddl.Items.Add(li)
                                End If
                            Next
                            If rowQuestion("IsRequired") Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & EvalTemplateDetlNo
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = ddl.ID
                                rf.Text = "*"
                                rf.InitialValue = "0"
                                rf.ValidationGroup = "EvalValidationGroup"
                                pForm.Controls.Add(rf)
                            End If
                            pForm.Controls.Add(ddl)
                        Case "N"
                            Dim txt As New TextBox
                            txt.ID = "txt" & EvalTemplateDetlNo
                            If rowQuestion("IsRequired") Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & EvalTemplateDetlNo.ToString()
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = txt.ID
                                rf.Text = "*"
                                rf.ValidationGroup = "EvalValidationGroup"
                                pForm.Controls.Add(rf)
                            End If
                            pForm.Controls.Add(txt)
                            Dim cf As New CompareValidator
                            cf.ID = "cf" & EvalTemplateDetlNo
                            cf.Display = ValidatorDisplay.Dynamic
                            cf.ControlToValidate = txt.ID
                            cf.Text = "*"
                            cf.Operator = ValidationCompareOperator.DataTypeCheck
                            cf.Type = ValidationDataType.Double
                            pForm.Controls.Add(cf)
                    End Select
                    i = i + 1
                    If rowQuestion("HasComment") Then
                        Dim txtComments As New TextBox
                        txtComments.ID = "txtComments" & EvalTemplateDetlNo.ToString()
                        txtComments.CssClass = "form-control"
                        txtComments.TextMode = TextBoxMode.MultiLine
                        txtComments.Rows = 4
                        pForm.Controls.Add(New LiteralControl("<span style='color:blue;font-style:italic;'>Comments :</span><br/>"))
                        pForm.Controls.Add(txtComments)
                    End If
                    pForm.Controls.Add(New LiteralControl("</div>"))
                    pForm.Controls.Add(New LiteralControl("</div>"))
                    pForm.Controls.Add(New LiteralControl("</div><br />"))
                Next
                pForm.Controls.Add(New LiteralControl("</div>"))
            Next
            'pForm.Controls.Add(New LiteralControl("<h5 style='font-size:14px;padding:0;margin:0;font-weight:bold;'>Other Comments :</h5>"))
            'Dim txtOtherComments As New TextBox
            'txtOtherComments.ID = "txtOtherComments"
            'txtOtherComments.TextMode = TextBoxMode.MultiLine
            'txtOtherComments.Rows = 4
            'pForm.Controls.Add(New LiteralControl("<div style='padding:0 0 20px 20px'>"))
            'pForm.Controls.Add(txtOtherComments)
            'pForm.Controls.Add(New LiteralControl("</div>"))
            pForm.Controls.Add(New LiteralControl("</div>"))
        Catch ex As Exception

        End Try

    End Sub
#End Region


#Region "******** Answer ********"
    Private Sub PopulateAnswer()
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EEvalTemplateDetl_WebFromAns", UserNo, TemplateID, ApplicantNo, EmployeeNo, TransNo)
            Dim dtQuestion As DataTable
            dtQuestion = ds.Tables(0)
            For Each rowQuestion As DataRow In dtQuestion.Rows
                Dim txtRemarks As New TextBox
                Dim ResponseTypeCode As String = Generic.ToStr(rowQuestion("ResponseTypeCode"))
                Dim EvalTemplateDetlNo As Integer = Generic.ToInt(rowQuestion("EvalTemplateDetlNo"))
                Select Case ResponseTypeCode
                    Case "RB"
                        'Radio Button
                        Dim rbl As New RadioButtonList
                        rbl = pForm.FindControl("rbl" & EvalTemplateDetlNo.ToString())
                        rbl.SelectedValue = Generic.ToStr(rowQuestion("AnswerNo"))
                    Case "CB"
                        'Checkbox
                        Dim cbl As New CheckBoxList
                        cbl = pForm.FindControl("cbl" & EvalTemplateDetlNo.ToString())
                        Dim str As String = Generic.ToStr(rowQuestion("AnswerNo"))
                        Dim sentence As String() = str.Split(",")
                        Dim i As Integer = 0
                        For Each word As String In sentence
                            Dim li As ListItem
                            li = cbl.Items.FindByValue(word)
                            If li IsNot Nothing Then
                                li.Selected = True
                            End If
                        Next
                    Case "NT"
                        'Narrative
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & EvalTemplateDetlNo.ToString())
                        txt.Text = Generic.ToStr(rowQuestion("AnswerNo"))
                    Case "DR"
                        'Dropdown
                        Dim ddl As New DropDownList
                        ddl = pForm.FindControl("ddl" & EvalTemplateDetlNo.ToString())
                        Try
                            ddl.SelectedValue = Generic.ToStr(rowQuestion("AnswerNo"))
                        Catch ex As Exception

                        End Try
                    Case "N"
                        'Numeric
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & EvalTemplateDetlNo.ToString())
                        txt.Text = Generic.ToStr(rowQuestion("AnswerNo"))
                End Select

                If rowQuestion("HasComment") Then
                    Dim txtComments As New TextBox
                    txtComments = pForm.FindControl("txtComments" & EvalTemplateDetlNo)
                    txtComments.Text = Generic.ToStr(rowQuestion("Comments"))
                End If

            Next

        Catch ex As Exception

        End Try

    End Sub


#End Region


#Region "******** Save ********"

    Private Function SaveRecord() As Boolean
        Dim i As Integer
        Try
            Dim ds As New DataSet
            'Dim ApplicantNo As Integer, EmployeeNo As Integer
            ds = SQLHelper.ExecuteDataSet("EEvalTemplateDetl_Form", UserNo, TemplateID)
            Dim dtQuestion As DataTable
            dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "EvalTemplateDetlNo", "EvalTemplateCateNo", "Question", "HasComment", "IsRequired", "ResponseTypeCode")
            For Each rowQuestion As DataRow In dtQuestion.Rows
                Dim txtRemarks As New TextBox
                Dim AnswerNo As String = "", AnswerDesc As String = ""
                Dim EvalTemplateDetlNo As Integer = Generic.ToInt(rowQuestion("EvalTemplateDetlNo"))
                Dim EvalTemplateDetlChoiceNo As String = 0
                Try

                    Select Case Generic.ToStr(rowQuestion("ResponseTypeCode"))
                        Case "RB"
                            Dim rbl As New RadioButtonList
                            rbl = pForm.FindControl("rbl" & EvalTemplateDetlNo)
                            EvalTemplateDetlChoiceNo = rbl.SelectedValue
                            AnswerNo = rbl.SelectedValue
                            AnswerDesc = rbl.SelectedItem.Text
                        Case "NT"
                            Dim txt As New TextBox
                            txt = pForm.FindControl("txt" & EvalTemplateDetlNo)
                            'EvalTemplateDetlChoiceNo = 0
                            AnswerNo = txt.Text
                            AnswerDesc = txt.Text
                        Case "DR"
                            Dim ddl As New DropDownList
                            ddl = pForm.FindControl("ddl" & EvalTemplateDetlNo)
                            EvalTemplateDetlChoiceNo = ddl.SelectedValue
                            AnswerNo = ddl.SelectedValue
                            AnswerDesc = ddl.SelectedItem.Text
                        Case "N"
                            Dim txt As New TextBox
                            txt = pForm.FindControl("txt" & EvalTemplateDetlNo)
                            'EvalTemplateDetlChoiceNo = 0
                            AnswerNo = txt.Text
                            AnswerDesc = txt.Text
                        Case "CB"
                            Dim cbl As New CheckBoxList
                            cbl = pForm.FindControl("cbl" & EvalTemplateDetlNo)
                            'EvalTemplateDetlChoiceNo = 0
                            AnswerNo = CheckBoxValue(cbl, 1)
                            AnswerDesc = CheckBoxValue(cbl, 0)
                    End Select

                Catch ex As Exception

                End Try

                If Generic.ToStr(rowQuestion("ResponseTypeCode")) <> "" Then
                    If SQLHelper.ExecuteNonQuery("EEvalTemplateAns_WebSave", UserNo, ApplicantNo, EmployeeNo, TransNo, TemplateID, EvalTemplateDetlNo, EvalTemplateDetlChoiceNo, AnswerNo, AnswerDesc, txtRemarks.Text) > 0 Then
                        i = i + 1
                    End If
                End If

            Next

            If i > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function CheckBoxValue(chk As CheckBoxList, IsValue As Boolean) As String
        Dim ret As String = ""
        For i = 0 To chk.Items.Count - 1
            If chk.Items(i).Selected Then
                If IsValue Then
                    ret = ret & chk.Items(i).Value & ","
                Else
                    ret = ret & chk.Items(i).Text & ","
                End If
            End If
        Next
        ret = Left(ret, Len(ret) - 1)
        Return ret
    End Function

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim ret As Boolean
        ret = SaveRecord()
        If ret Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

#End Region

End Class
