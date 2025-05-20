Imports System.Data
Imports clsLib
Imports System.IO
Imports DevExpress.Web.ASPxHtmlEditor

Partial Class Secured_PEReviewForm
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer
    Dim pestandardmainno As Integer = 0
    Dim pestandardcateno As Integer = 0
    Dim pecatetypeno As Integer = 0
    Dim pecycleno As Integer = 0
    Dim componentno As Integer = 0
    Dim isposted As Boolean = False
    Dim peevaluatorno As Integer = 0
    Dim TabNo As Integer = 0
    Dim Journal_ID As Integer = 0
    Dim OnlineEmpNo As Integer = 0

    Dim _ds As New DataSet
    Dim _dt As New DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        OnlineEmpNo = Generic.ToInt(Session("EmployeeNo"))

        If Not IsPostBack Then
            PopulateCombo()
            If pecycleno > 0 Then
                cboPECycleNo.Text = pecycleno
            End If
        End If

        AddHandler ChatBox1.lnkSendClick, AddressOf lnkSend_Click

    End Sub

    Protected Overrides Sub OnInit(e As EventArgs)
        MyBase.OnInit(e)
        pestandardmainno = Generic.ToInt(Request.QueryString("id"))
        pestandardcateno = Generic.ToInt(Request.QueryString("pestandardcateno"))
        pecatetypeno = Generic.ToInt(Request.QueryString("pecatetypeno"))
        peevaluatorno = Generic.ToInt(Request.QueryString("peevaluatorno"))
        pecycleno = Generic.ToInt(Request.QueryString("pecycleno"))
        isposted = Generic.ToBol(Request.QueryString("isposted"))
        If pestandardcateno = 0 Then
            Dim dt As New DataTable
            Dim FormName As String = ""
            dt = SQLHelper.ExecuteDataTable("EMenu_PEStandardTab", Generic.ToInt(Session("OnlineUserNo")), pestandardmainno)
            For Each row As DataRow In dt.Select("RowNo=1")
                FormName = Generic.ToStr(row("Formname"))
                pestandardcateno = Generic.ToStr(row("PEStandardCateNo"))
                pecatetypeno = Generic.ToStr(row("PECateTypeNo"))
                pecycleno = 2 'Admin Review
            Next
        End If
        PopulateHeader()
        EnabledControls(pecatetypeno)
    End Sub

    Private Sub EnabledControls(ByVal pecatetypeno As Integer)

        If pecatetypeno = 1 Then 'MBO
            PopulateCate()
            PopulateGoals()
            MBOPopulateAnswer()
        ElseIf pecatetypeno = 2 Then 'KRA
            PopulateCate()
            PopulateQuestion()
        Else
            lnkAdd.Visible = False
            lnkSave.Visible = False
        End If

    End Sub


#Region "******** ComboBox ********"

    Private Sub PopulateCombo()

        Try
            cboPEStandardDimNo.DataSource = SQLHelper.ExecuteDataSet("EPEDimensionType_WebLookup", pestandardcateno)
            cboPEStandardDimNo.DataTextField = "tDesc"
            cboPEStandardDimNo.DataValueField = "tNo"
            cboPEStandardDimNo.DataBind()
        Catch ex As Exception

        End Try

        Try
            cboPEEvaluatorNo.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EPEEvaluator", PayLocNo, "", "")
            cboPEEvaluatorNo.DataTextField = "tDesc"
            cboPEEvaluatorNo.DataValueField = "tNo"
            cboPEEvaluatorNo.DataBind()
        Catch ex As Exception

        End Try

        Try
            cboPECycleNo.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EPECycle", PayLocNo, "", "")
            cboPECycleNo.DataTextField = "tDesc"
            cboPECycleNo.DataValueField = "tNo"
            cboPECycleNo.DataBind()
        Catch ex As Exception

        End Try

    End Sub

#End Region

    
#Region "******** Functions ********"

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

#End Region


#Region "******** Title ******"

    Private Sub PopulateHeader()

        Dim Title As String = "", SubTitle As String = "", Instruction As String = ""
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEStandard_WebFormHeader", UserNo, pestandardmainno)
        For Each row As DataRow In dt.Rows
            Title = Generic.ToStr(row("Title"))
            SubTitle = Generic.ToStr(row("SubTitle"))
            Instruction = Generic.ToStr(row("Instruction"))
        Next

        pHeader.Controls.Add(New LiteralControl("<div class='row'><br/>"))
        pHeader.Controls.Add(New LiteralControl("<h3 style='text-align:center;'>" & Title & "</h3>"))
        If SubTitle > "" Then
            pHeader.Controls.Add(New LiteralControl("<h4 style='text-align:center;'>" & SubTitle & "</h4>"))
        End If
        If Instruction > "" Then
            pHeader.Controls.Add(New LiteralControl(Instruction))
        End If
        pHeader.Controls.Add(New LiteralControl("</div>"))

    End Sub

#End Region


#Region "******** Category Header ******"

    Private Sub PopulateCate()

        Dim PEStandardCateCode As String = "", PEStandardCateDesc As String = "", RemarksCate As String = ""
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEStandardCate_WebOne", UserNo, pestandardcateno)
        For Each row As DataRow In dt.Rows
            PEStandardCateCode = Generic.ToStr(row("PEStandardCateCode"))
            PEStandardCateDesc = Generic.ToStr(row("PEStandardCateDesc"))
            RemarksCate = Generic.ToStr(row("RemarksCate"))
        Next

        If PEStandardCateCode > "" Then
            PEStandardCateCode = PEStandardCateCode & ". "
        End If


        'LinkButton
        Dim lnkCate As New LinkButton
        lnkCate.ID = "lnkCate" & pestandardcateno
        lnkCate.Text = "&nbsp; Edit &nbsp;"
        lnkCate.CssClass = "fa fa-pencil"
        lnkCate.Style.Add("font-family", "FontAwesome, Arial")
        lnkCate.Style.Add("text-decoration", "none")
        lnkCate.Style.Add("display", "inline")
        lnkCate.CommandArgument = pestandardcateno
        AddHandler lnkCate.Click, AddressOf lnkCate_Click


        pCate.Controls.Add(New LiteralControl("<div class='row'>"))
        pCate.Controls.Add(New LiteralControl("<div class='bs-callout bs-callout-info'>"))
        If PEStandardCateCode > "" Or PEStandardCateDesc > "" Then
            pCate.Controls.Add(New LiteralControl("<h4 style='display:inline;font-weight:bold;'>" & PEStandardCateCode & PEStandardCateDesc & "</h4>&nbsp;&nbsp;&nbsp;"))
        End If
        pCate.Controls.Add(lnkCate)
        pCate.Controls.Add(New LiteralControl("<p>" & RemarksCate & "</p><br/>"))
        pCate.Controls.Add(New LiteralControl("</div></div>"))

    End Sub

#End Region


#Region "******** KRA Populate ********"

    'KRA Populate
    Private Sub PopulateQuestion(Optional TabId As Integer=0)
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPEStandard_WebForm", UserNo, pestandardmainno, pestandardcateno, peevaluatorno, pecycleno, componentno)
            Dim dtDimension As DataTable
            Dim dtQuestion As DataTable
            dtDimension = ds.Tables(0).DefaultView.ToTable(True, "PEStandardDimNo", "Dimension", "PEStandardDimCode", "PEStandardDimDesc", "OrderLevelDim", "RemarksDim")
            dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PEStandardNo", "PEStandardDimNo", "PEStandardCode", "PEStandardDesc", "Standard", "HasComment", "IsRequired", "ResponseTypeNo", "Comments", "OrderLevelItem", "IsEnabled")



            For Each rowDimension As DataRow In dtDimension.Rows
                Dim PEStandardDimNo As String = Generic.ToStr(rowDimension("PEStandardDimNo"))
                Dim PEStandardDimCode As String = Generic.ToStr(rowDimension("PEStandardDimCode"))
                Dim PEStandardDimDesc As String = Generic.ToStr(rowDimension("PEStandardDimDesc"))
                Dim RemarksDim As String = Generic.ToStr(rowDimension("RemarksDim"))


                'Dimension
                Dim lnkDim As New LinkButton
                lnkDim.ID = "lnkDim" & PEStandardDimNo
                lnkDim.Text = "&nbsp; Edit &nbsp;&nbsp;&nbsp;"
                lnkDim.CssClass = "fa fa-pencil"
                lnkDim.Style.Add("font-family", "FontAwesome, Arial")
                lnkDim.Style.Add("text-decoration", "none")
                lnkDim.Style.Add("display", "inline")
                lnkDim.CommandArgument = PEStandardDimNo
                AddHandler lnkDim.Click, AddressOf lnkDim_Click


                'New Item
                Dim lnkNewItem As New LinkButton
                lnkNewItem.ID = "lnkNewItem" & PEStandardDimNo
                lnkNewItem.Text = "&nbsp; Add Item &nbsp;"
                lnkNewItem.CssClass = "fa fa-plus"
                lnkNewItem.Style.Add("font-family", "FontAwesome, Arial")
                lnkNewItem.Style.Add("text-decoration", "none")
                lnkNewItem.Style.Add("display", "inline")
                lnkNewItem.CommandArgument = PEStandardDimNo
                AddHandler lnkNewItem.Click, AddressOf lnkNewItem_Click

                pForm.Controls.Add(New LiteralControl("<div class='row'>"))
                pForm.Controls.Add(New LiteralControl("<div class='bs-callout bs-callout-info'>"))
                If PEStandardDimCode > "" Then
                    PEStandardDimCode = PEStandardDimCode & ". "
                End If
                If PEStandardDimCode > "" Or PEStandardDimDesc > "" Then
                    pForm.Controls.Add(New LiteralControl("<br/><h4 style='display:inline;'>" & PEStandardDimCode & PEStandardDimDesc & "</h4>&nbsp;&nbsp;&nbsp;"))
                End If
                pForm.Controls.Add(lnkDim)
                pForm.Controls.Add(lnkNewItem)
                pForm.Controls.Add(New LiteralControl("<p>" & RemarksDim & "</p>"))
                pForm.Controls.Add(New LiteralControl("</div>"))

                pForm.Controls.Add(New LiteralControl("<div class='panel-body' style='padding-top:0px;padding-bottom:0px;'>"))
                pForm.Controls.Add(New LiteralControl("<div class='panel-body' style='padding-top:0px;padding-bottom:0px;'>"))

                pForm.Controls.Add(New LiteralControl("<div class='media'>"))
                pForm.Controls.Add(New LiteralControl("<div class='media-body'>"))
                For Each rowQuestion As DataRow In dtQuestion.Select("PEStandardDimNo=" & rowDimension("PEStandardDimNo"))
                    Dim ResponseTypeNo As Integer = Generic.ToInt(rowQuestion("ResponseTypeNo"))
                    Dim PEStandardNo As Integer = Generic.ToInt(rowQuestion("PEStandardNo"))
                    Dim IsEnabled As Boolean = Generic.ToBol(rowQuestion("IsEnabled"))

                    'Move Up Button
                    Dim lnkMoveUp As New LinkButton
                    lnkMoveUp.ID = "lnkMoveUp" & PEStandardNo
                    'lnkMoveUp.Text = "Move Up"
                    lnkMoveUp.ToolTip = "Move Up"
                    lnkMoveUp.CssClass = "fa fa-arrow-up"
                    lnkMoveUp.Style.Add("font-family", "FontAwesome, Arial")
                    lnkMoveUp.Style.Add("text-decoration", "none")
                    lnkMoveUp.CommandArgument = PEStandardNo
                    'lnkMoveUp.Visible = IsAddDim
                    AddHandler lnkMoveUp.Click, AddressOf lnkMoveUp_Click


                    'Move Down Button
                    Dim lnkMoveDown As New LinkButton
                    lnkMoveDown.ID = "lnkMoveDown" & PEStandardNo
                    'lnkMoveDown.Text = "Move Down"
                    lnkMoveDown.ToolTip = "Move Down"
                    lnkMoveDown.CssClass = "fa fa-arrow-down"
                    lnkMoveDown.Style.Add("font-family", "FontAwesome, Arial")
                    lnkMoveDown.Style.Add("text-decoration", "none")
                    lnkMoveDown.CommandArgument = PEStandardNo
                    'lnkMoveDown.Visible = IsAddDim
                    AddHandler lnkMoveDown.Click, AddressOf lnkMoveDown_Click

                    'Delete Button
                    Dim lnkDelete As New LinkButton
                    lnkDelete.ID = "lnkDelete" & PEStandardNo
                    lnkDelete.Text = "Delete Item"
                    'lnkDelete.CssClass = "fa fa-times"
                    lnkDelete.Style.Add("font-family", "FontAwesome, Arial")
                    lnkDelete.Style.Add("text-decoration", "none")
                    lnkDelete.CommandArgument = PEStandardNo
                    AddHandler lnkDelete.Click, AddressOf lnkDelete_Click

                    'Delete Confirmation
                    Dim ConfirmButtonExtender As AjaxControlToolkit.ConfirmButtonExtender
                    ConfirmButtonExtender = New AjaxControlToolkit.ConfirmButtonExtender()
                    ConfirmButtonExtender.ID = "cbe" & PEStandardNo.ToString
                    ConfirmButtonExtender.DisplayModalPopupID = "mpe" & PEStandardNo
                    ConfirmButtonExtender.TargetControlID = "lnkDelete" & PEStandardNo

                    Dim ModalPopupExtender As AjaxControlToolkit.ModalPopupExtender
                    ModalPopupExtender = New AjaxControlToolkit.ModalPopupExtender()
                    ModalPopupExtender.ID = "mpe" & PEStandardNo.ToString
                    ModalPopupExtender.TargetControlID = "lnkDelete" & PEStandardNo
                    ModalPopupExtender.PopupControlID = "pConfirmBox"
                    ModalPopupExtender.OkControlID = "btnYes"
                    ModalPopupExtender.CancelControlID = "btnNo"

                    'Edit Button
                    Dim lnkEdit As New LinkButton
                    lnkEdit.ID = "lnkEdit" & PEStandardNo
                    lnkEdit.Text = "Edit Question"
                    'lnkEdit.CssClass = "fa fa-pencil"
                    lnkEdit.Style.Add("font-family", "FontAwesome, Arial")
                    lnkEdit.Style.Add("text-decoration", "none")
                    lnkEdit.CommandArgument = PEStandardNo
                    AddHandler lnkEdit.Click, AddressOf lnkEdit_Click

                    'Edit Detail Button
                    Dim lnkEditDeti As New LinkButton
                    lnkEditDeti.ID = "lnkEditDeti" & PEStandardNo
                    lnkEditDeti.Text = "Edit Indicator"
                    'lnkEditDeti.CssClass = "fa fa-pencil"
                    lnkEditDeti.Style.Add("font-family", "FontAwesome, Arial")
                    lnkEditDeti.Style.Add("text-decoration", "none")
                    lnkEditDeti.CommandArgument = PEStandardNo & "|0|" & Generic.ToStr(rowQuestion("OrderLevelItem"))
                    AddHandler lnkEditDeti.Click, AddressOf lnkEditDeti_Click

                    'Journal Button
                    Dim lnkJournal As New LinkButton
                    lnkJournal.ID = "lnkJournal" & PEStandardNo
                    lnkJournal.Text = "&nbsp;Journal &nbsp;"
                    lnkJournal.CssClass = "fa fa-comments-o"
                    lnkJournal.Style.Add("font-family", "FontAwesome, Arial")
                    lnkJournal.Style.Add("text-decoration", "none")
                    lnkJournal.Visible = False
                    lnkJournal.CommandArgument = PEStandardNo
                    AddHandler lnkJournal.Click, AddressOf lnkJournal_Click

                    pForm.Controls.Add(New LiteralControl("<div class='row'>"))
                    pForm.Controls.Add(New LiteralControl("<div class='panel panel-default'>"))

                    pForm.Controls.Add(New LiteralControl("<div class='panel-heading'>"))
                    pForm.Controls.Add(New LiteralControl("<h4 class='panel-title' style='font-size:small;'><strong>Item No. " & rowQuestion("OrderLevelItem") & "</strong></h4>"))
                    pForm.Controls.Add(New LiteralControl("<ul class='panel-controls'>"))
                    pForm.Controls.Add(New LiteralControl("<li>"))
                    pForm.Controls.Add(lnkJournal)
                    pForm.Controls.Add(New LiteralControl("</li>"))
                    pForm.Controls.Add(New LiteralControl("<li>&nbsp;&nbsp;<div class='btn-group'><button type='button' class='btn btn-default btn-sm dropdown-toggle' data-toggle='dropdown'>&nbsp;<span class='caret'></span>&nbsp;</button>"))
                    pForm.Controls.Add(New LiteralControl("<ul class='dropdown-menu dropdown-menu-right' role='menu'>"))
                    pForm.Controls.Add(New LiteralControl("<li>"))
                    pForm.Controls.Add(lnkEdit)
                    pForm.Controls.Add(New LiteralControl("</li>"))

                    If ResponseTypeNo = 1 Or ResponseTypeNo = 2 Or ResponseTypeNo = 4 Then
                        pForm.Controls.Add(New LiteralControl("<li>"))
                        lnkEditDeti.Enabled = True
                    Else
                        pForm.Controls.Add(New LiteralControl("<li class='disabled'>"))
                        lnkEditDeti.Enabled = False
                    End If

                    pForm.Controls.Add(lnkEditDeti)
                    pForm.Controls.Add(New LiteralControl("</li>"))
                    pForm.Controls.Add(New LiteralControl("<li>"))
                    pForm.Controls.Add(lnkDelete)
                    pForm.Controls.Add(ConfirmButtonExtender)
                    pForm.Controls.Add(ModalPopupExtender)
                    pForm.Controls.Add(New LiteralControl("</li>"))
                    pForm.Controls.Add(New LiteralControl("</ul></div></li>"))
                    pForm.Controls.Add(New LiteralControl("</ul>"))
                    pForm.Controls.Add(New LiteralControl("</div>"))

                    pForm.Controls.Add(New LiteralControl("<div class='panel-body'>"))
                    pForm.Controls.Add(New LiteralControl("<div class='media'>"))
                    pForm.Controls.Add(New LiteralControl("<div class='media-left'>"))
                    pForm.Controls.Add(New LiteralControl(rowQuestion("PEStandardCode")))
                    pForm.Controls.Add(New LiteralControl("</div>"))
                    pForm.Controls.Add(New LiteralControl("<div class='media-body' style='width:10000px;'>"))

                    'pForm.Controls.Add(New LiteralControl(rowQuestion("PEStandardDesc") & " " & rowQuestion("Standard")))

                    pForm.Controls.Add(New LiteralControl("<dl><dt>" & rowQuestion("PEStandardDesc") & "</dt><dd>" & rowQuestion("Standard") & "</dd></dl>"))

                    pForm.Controls.Add(New LiteralControl("</div>"))
                    pForm.Controls.Add(New LiteralControl("</div>"))
                    pForm.Controls.Add(New LiteralControl("<div class='col-md-12'>"))
                    pForm.Controls.Add(New LiteralControl("<div style='padding:0 0 0 30px;'>"))
                    Select Case ResponseTypeNo
                        Case 1
                            'Radio Button
                            Dim rbl As New RadioButtonList
                            rbl.ID = "rbl" & PEStandardNo
                            rbl.RepeatLayout = RepeatLayout.Table
                            rbl.Enabled = IsEnabled
                            'rbl.RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal
                            For Each rowChoice As DataRow In ds.Tables(0).Select("PEStandardNo=" & PEStandardNo)
                                Dim li As New ListItem
                                li.Text = "&nbsp;&nbsp;&nbsp;" & rowChoice("Anchor").ToString()
                                li.Value = rowChoice("PEStandardDetiNo").ToString()
                                If Generic.ToInt(li.Value) > 0 Then
                                    rbl.Items.Add(li)
                                End If
                            Next
                            If rowQuestion("IsRequired") And IsEnabled = True Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardNo
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = rbl.ID
                                rf.Text = "* This item is required."
                                rf.ErrorMessage = "Item " & rowQuestion("PEStandardCode")
                                rf.ValidationGroup = "EvalValidationGroup"
                                rf.Style.Add("color", "red")
                                pForm.Controls.Add(rf)
                            End If
                            pForm.Controls.Add(rbl)

                        Case 2
                            'Checkbox
                            Dim cbl As New CheckBoxList
                            cbl.ID = "cbl" & PEStandardNo
                            cbl.Enabled = IsEnabled
                            For Each rowChoice As DataRow In ds.Tables(0).Select("PEStandardNo=" & PEStandardNo)
                                Dim li As New ListItem
                                li.Text = "&nbsp;&nbsp;&nbsp;" & rowChoice("Anchor").ToString()
                                li.Value = rowChoice("PEStandardDetiNo").ToString()
                                If Generic.ToInt(li.Value) > 0 Then
                                    cbl.Items.Add(li)
                                End If
                            Next
                            pForm.Controls.Add(cbl)

                        Case 3
                            'Narrative
                            Dim txt As New TextBox
                            txt.ID = "txt" & PEStandardNo
                            txt.TextMode = TextBoxMode.MultiLine
                            txt.Rows = 3
                            txt.Enabled = IsEnabled
                            txt.CssClass = "form-control"
                            pForm.Controls.Add(New LiteralControl("<div class='row col-no-padding'><div class='col-md-6'>"))
                            pForm.Controls.Add(New LiteralControl("Narrative :<br/>"))
                            pForm.Controls.Add(txt)
                            pForm.Controls.Add(New LiteralControl("</div></div>"))
                            If rowQuestion("IsRequired") And IsEnabled = True Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardNo
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = txt.ID
                                rf.Text = "* This item is required."
                                rf.ValidationGroup = "EvalValidationGroup"
                                pForm.Controls.Add(rf)
                            End If

                        Case 4
                            'Dropdown
                            Dim ddl As New DropDownList
                            ddl.ID = "ddl" & PEStandardNo
                            Dim l As New ListItem
                            l.Text = "-- Select --"
                            l.Value = "0"
                            l.Enabled = IsEnabled
                            ddl.CssClass = "form-control"
                            ddl.Items.Add(l)
                            For Each rowChoice As DataRow In ds.Tables(0).Select("PEStandardNo=" & PEStandardNo)
                                Dim li As New ListItem
                                li.Text = Generic.ToStr(rowChoice("Anchor"))
                                li.Value = Generic.ToInt(rowChoice("PEStandardDetiNo"))
                                If Generic.ToInt(li.Value) > 0 Then
                                    ddl.Items.Add(li)
                                End If
                            Next
                            If rowQuestion("IsRequired") And IsEnabled = True Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardNo
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = ddl.ID
                                rf.Text = "* This item is required."
                                rf.InitialValue = "0"
                                rf.ValidationGroup = "EvalValidationGroup"
                                pForm.Controls.Add(rf)
                            End If
                            pForm.Controls.Add(New LiteralControl("<div class='row col-no-padding'><div class='col-md-4'>"))
                            pForm.Controls.Add(ddl)
                            pForm.Controls.Add(New LiteralControl("</div></div>"))

                        Case 5
                            'Numeric
                            Dim txt As New TextBox
                            txt.ID = "txt" & PEStandardNo
                            txt.CssClass = "form-control"
                            txt.Enabled = IsEnabled
                            pForm.Controls.Add(New LiteralControl("<div class='row col-no-padding'><div class='col-md-2'>"))
                            pForm.Controls.Add(New LiteralControl("Points :<br/>"))
                            pForm.Controls.Add(txt)
                            pForm.Controls.Add(New LiteralControl("</div></div>"))
                            If rowQuestion("IsRequired") And IsEnabled = True Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardNo.ToString()
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = txt.ID
                                rf.Text = "* This item is required."
                                rf.ValidationGroup = "EvalValidationGroup"
                                pForm.Controls.Add(rf)
                            End If
                            Dim cf As New CompareValidator
                            cf.ID = "cf" & PEStandardNo
                            cf.Display = ValidatorDisplay.Dynamic
                            cf.ControlToValidate = txt.ID
                            cf.Text = "*"
                            cf.Operator = ValidationCompareOperator.DataTypeCheck
                            cf.Type = ValidationDataType.Double
                            pForm.Controls.Add(cf)

                            Dim filtertxt As AjaxControlToolkit.FilteredTextBoxExtender
                            filtertxt = New AjaxControlToolkit.FilteredTextBoxExtender()
                            filtertxt.ID = "filtertxt" & PEStandardNo
                            filtertxt.FilterType = AjaxControlToolkit.FilterTypes.Custom
                            filtertxt.ValidChars = "1234567890.-"
                            filtertxt.TargetControlID = txt.ID
                            pForm.Controls.Add(filtertxt)
                    End Select


                    If rowQuestion("HasComment") Then
                        Dim txtComments As New TextBox
                        txtComments.ID = "txtComments" & rowQuestion("PEStandardNo").ToString()
                        txtComments.TextMode = TextBoxMode.MultiLine
                        txtComments.Rows = 3
                        txtComments.Enabled = IsEnabled
                        txtComments.CssClass = "form-control"
                        pForm.Controls.Add(New LiteralControl("<br/>"))
                        pForm.Controls.Add(New LiteralControl("<div class='row col-no-padding'><div class='col-md-6'>"))
                        pForm.Controls.Add(New LiteralControl("Comments :<br/>"))
                        pForm.Controls.Add(txtComments)
                        pForm.Controls.Add(New LiteralControl("</div></div>"))
                    End If

                    pForm.Controls.Add(New LiteralControl("<ul class='panel-controls'>"))
                    pForm.Controls.Add(New LiteralControl("<li>"))
                    pForm.Controls.Add(lnkMoveUp)
                    pForm.Controls.Add(New LiteralControl("</li>"))
                    pForm.Controls.Add(New LiteralControl("<li>"))
                    pForm.Controls.Add(lnkMoveDown)
                    pForm.Controls.Add(New LiteralControl("</li>"))
                    pForm.Controls.Add(New LiteralControl("</ul>"))

                    pForm.Controls.Add(New LiteralControl("</div>"))
                    pForm.Controls.Add(New LiteralControl("</div>"))
                    pForm.Controls.Add(New LiteralControl("</div>"))
                    pForm.Controls.Add(New LiteralControl("</div>"))
                    pForm.Controls.Add(New LiteralControl("</div>"))
                Next


                pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("</div>"))

                pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("</div>"))

                pForm.Controls.Add(New LiteralControl("</div>"))
            Next

            'Generic.EnableControls(Me, "pForm", False)
        Catch ex As Exception

        End Try


    End Sub



#End Region


#Region "******** MBO Populate ********"


    'MBO Populate
    Private Sub PopulateGoals(Optional TabId As Integer = 0)
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPEStandardObjTemp_WebForm", UserNo, pestandardmainno, pestandardcateno, peevaluatorno, pecycleno, componentno)
            Dim dtQuestion As DataTable
            Dim dtMBO As DataTable
            dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PEStandardNo", "PEStandardCode", "PEStandardDesc", "Standard", "OrderLevelItem")
            dtMBO = ds.Tables(0).DefaultView.ToTable(True, "PEStandardObjTempNo", "PEStandardNo", "Description", "ResponseTypeNo", "IsRequired", "IsEnabled")

            pForm.Controls.Add(New LiteralControl("<div class='row'>"))
            pForm.Controls.Add(New LiteralControl("<div class='panel-body'>"))
            pForm.Controls.Add(New LiteralControl("<div class='panel-body'>"))

            For Each rowQuestion As DataRow In dtQuestion.Rows
                Dim PEStandardNo As Integer = Generic.ToInt(rowQuestion("PEStandardNo"))

                'Move Up Button
                Dim lnkMoveUp As New LinkButton
                lnkMoveUp.ID = "lnkMoveUp" & PEStandardNo
                'lnkMoveUp.Text = "Move Up"
                lnkMoveUp.ToolTip = "Move Up"
                lnkMoveUp.CssClass = "fa fa-arrow-up"
                lnkMoveUp.Style.Add("font-family", "FontAwesome, Arial")
                lnkMoveUp.Style.Add("text-decoration", "none")
                lnkMoveUp.CommandArgument = PEStandardNo
                'lnkMoveUp.Visible = IsAddCate
                AddHandler lnkMoveUp.Click, AddressOf lnkMoveUp_Click


                'Move Down Button
                Dim lnkMoveDown As New LinkButton
                lnkMoveDown.ID = "lnkMoveDown" & PEStandardNo
                'lnkMoveDown.Text = "Move Down"
                lnkMoveDown.ToolTip = "Move Down"
                lnkMoveDown.CssClass = "fa fa-arrow-down"
                lnkMoveDown.Style.Add("font-family", "FontAwesome, Arial")
                lnkMoveDown.Style.Add("text-decoration", "none")
                lnkMoveDown.CommandArgument = PEStandardNo
                'lnkMoveDown.Visible = IsAddCate
                AddHandler lnkMoveDown.Click, AddressOf lnkMoveDown_Click


                'Reset Item
                Dim lnkReset As New LinkButton
                lnkReset.ID = "lnkReset" & PEStandardNo
                lnkReset.Text = "Reset Item"
                lnkReset.Style.Add("font-family", "FontAwesome, Arial")
                lnkReset.Style.Add("text-decoration", "none")
                lnkReset.CommandArgument = PEStandardNo
                AddHandler lnkReset.Click, AddressOf lnkReset_Click

                'Reset Confirmation
                Dim xConfirmButtonExtender As AjaxControlToolkit.ConfirmButtonExtender
                xConfirmButtonExtender = New AjaxControlToolkit.ConfirmButtonExtender()
                xConfirmButtonExtender.ID = "xcbe" & PEStandardNo.ToString
                xConfirmButtonExtender.DisplayModalPopupID = "xmpe" & PEStandardNo
                xConfirmButtonExtender.TargetControlID = "lnkReset" & PEStandardNo

                'Reset Popup
                Dim xModalPopupExtender As AjaxControlToolkit.ModalPopupExtender
                xModalPopupExtender = New AjaxControlToolkit.ModalPopupExtender()
                xModalPopupExtender.ID = "xmpe" & PEStandardNo.ToString
                xModalPopupExtender.TargetControlID = "lnkReset" & PEStandardNo
                xModalPopupExtender.PopupControlID = "pConfirmBox"
                xModalPopupExtender.OkControlID = "btnYes"
                xModalPopupExtender.CancelControlID = "btnNo"

                'LinkButton
                Dim lnkJournal As New LinkButton
                lnkJournal.ID = "lnkJournal" & PEStandardNo
                lnkJournal.Text = "&nbsp;Journal &nbsp;"
                lnkJournal.CssClass = "fa fa-comments-o"
                lnkJournal.Style.Add("font-family", "FontAwesome, Arial")
                lnkJournal.Style.Add("text-decoration", "none")
                lnkJournal.Visible = False
                lnkJournal.CommandArgument = PEStandardNo
                AddHandler lnkJournal.Click, AddressOf lnkJournal_Click

                'Delete Button
                Dim lnkDelete As New LinkButton
                lnkDelete.ID = "lnkDelete" & PEStandardNo
                lnkDelete.Text = "Delete Item"
                'lnkDelete.CssClass = "fa fa-times"
                lnkDelete.Style.Add("font-family", "FontAwesome, Arial")
                lnkDelete.Style.Add("text-decoration", "none")
                lnkDelete.CommandArgument = PEStandardNo
                AddHandler lnkDelete.Click, AddressOf lnkDelete_Click

                'Delete Confirmation
                Dim ConfirmButtonExtender As AjaxControlToolkit.ConfirmButtonExtender
                ConfirmButtonExtender = New AjaxControlToolkit.ConfirmButtonExtender()
                ConfirmButtonExtender.ID = "cbe" & PEStandardNo.ToString
                ConfirmButtonExtender.DisplayModalPopupID = "mpe" & PEStandardNo
                ConfirmButtonExtender.TargetControlID = "lnkDelete" & PEStandardNo


                Dim ModalPopupExtender As AjaxControlToolkit.ModalPopupExtender
                ModalPopupExtender = New AjaxControlToolkit.ModalPopupExtender()
                ModalPopupExtender.ID = "mpe" & PEStandardNo.ToString
                ModalPopupExtender.TargetControlID = "lnkDelete" & PEStandardNo
                ModalPopupExtender.PopupControlID = "pConfirmBox"
                ModalPopupExtender.OkControlID = "btnYes"
                ModalPopupExtender.CancelControlID = "btnNo"

                pForm.Controls.Add(New LiteralControl("<div class='row'>"))
                pForm.Controls.Add(New LiteralControl("<div class='panel panel-default' >"))
                pForm.Controls.Add(New LiteralControl("<div class='panel-heading'>"))
                pForm.Controls.Add(New LiteralControl("<h4 class='panel-title' style='font-size:small;'><strong>Item No. " & rowQuestion("OrderLevelItem") & "</strong></h4>"))
                pForm.Controls.Add(New LiteralControl("<ul class='panel-controls'>"))
                pForm.Controls.Add(New LiteralControl("<li>"))
                pForm.Controls.Add(lnkJournal)
                pForm.Controls.Add(New LiteralControl("</li>"))
                pForm.Controls.Add(New LiteralControl("<li>&nbsp;&nbsp;<div class='btn-group'><button type='button' class='btn btn-default btn-sm dropdown-toggle' data-toggle='dropdown'>&nbsp;<span class='caret'></span>&nbsp;</button>"))
                pForm.Controls.Add(New LiteralControl("<ul class='dropdown-menu dropdown-menu-right' role='menu'>"))

                For Each rowObj As DataRow In dtMBO.Select("PEStandardNo=" & PEStandardNo)
                    Dim PEStandardObjTempNo As Integer = Generic.ToInt(rowObj("PEStandardObjTempNo"))
                    Dim ResponseTypeNo As Integer = Generic.ToInt(rowObj("ResponseTypeNo"))
                    Select Case ResponseTypeNo
                        Case 1, 2, 4
                            'Radio Button, Checkbox, Dropdown

                            'Edit Detail Button
                            Dim lnkEditDeti As New LinkButton
                            lnkEditDeti.ID = "lnkEditDeti" & PEStandardObjTempNo
                            lnkEditDeti.Text = "Edit " & Generic.ToStr(rowObj("Description"))
                            'lnkEditDeti.CssClass = "fa fa-pencil"
                            lnkEditDeti.Style.Add("font-family", "FontAwesome, Arial")
                            lnkEditDeti.Style.Add("text-decoration", "none")
                            lnkEditDeti.CommandArgument = PEStandardNo & "|" & PEStandardObjTempNo & "|" & Generic.ToStr(rowQuestion("OrderLevelItem")) & " - " & Generic.ToStr(rowObj("Description"))
                            AddHandler lnkEditDeti.Click, AddressOf lnkEditDeti_Click

                            pForm.Controls.Add(New LiteralControl("<li>"))
                            pForm.Controls.Add(lnkEditDeti)
                            pForm.Controls.Add(New LiteralControl("</li>"))
                    End Select
                Next

                pForm.Controls.Add(New LiteralControl("<li>"))
                pForm.Controls.Add(lnkReset)
                pForm.Controls.Add(xConfirmButtonExtender)
                pForm.Controls.Add(xModalPopupExtender)
                pForm.Controls.Add(New LiteralControl("</li>"))

                pForm.Controls.Add(New LiteralControl("<li>"))
                pForm.Controls.Add(lnkDelete)
                pForm.Controls.Add(ConfirmButtonExtender)
                pForm.Controls.Add(ModalPopupExtender)
                pForm.Controls.Add(New LiteralControl("</li>"))

                pForm.Controls.Add(New LiteralControl("</ul></div></li>"))
                pForm.Controls.Add(New LiteralControl("</ul>"))
                pForm.Controls.Add(New LiteralControl("</div>"))

                pForm.Controls.Add(New LiteralControl("<div class='panel-body'>"))
                pForm.Controls.Add(New LiteralControl("<div class='media'>"))
                pForm.Controls.Add(New LiteralControl("<div class='media-left'>"))
                pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("<div class='media-body' style='width:10000px;'>"))
                pForm.Controls.Add(New LiteralControl("<dl><dt>" & rowQuestion("PEStandardDesc") & "</dt><dd>" & rowQuestion("Standard") & "</dd></dl>"))
                pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("<div class='col-md-12'>"))
                pForm.Controls.Add(New LiteralControl("<div style='padding:0 0 0 0px'>"))
                pForm.Controls.Add(New LiteralControl("<div class='form-horizontal'>"))

                For Each rowObj As DataRow In dtMBO.Select("PEStandardNo=" & PEStandardNo)
                    Dim PEStandardObjTempNo As Integer = Generic.ToInt(rowObj("PEStandardObjTempNo"))
                    Dim ResponseTypeNo As Integer = Generic.ToInt(rowObj("ResponseTypeNo"))
                    Dim IsEnabled As Boolean = Generic.ToBol(rowObj("IsEnabled"))

                    pForm.Controls.Add(New LiteralControl("<div class='form-group'>"))
                    pForm.Controls.Add(New LiteralControl("<label class='col-md-3 control-label has-space'>" & Generic.ToStr(rowObj("Description")) & "</label>"))
                    Select Case ResponseTypeNo
                        Case 1
                            'Radio Button
                            Dim rbl As New RadioButtonList
                            rbl.ID = "rbl" & PEStandardObjTempNo
                            rbl.RepeatLayout = RepeatLayout.Table
                            rbl.Enabled = IsEnabled
                            For Each rowChoice As DataRow In ds.Tables(0).Select("PEStandardObjTempNo=" & PEStandardObjTempNo)
                                Dim li As New ListItem
                                li.Text = "&nbsp;&nbsp;&nbsp;" & rowChoice("Anchor").ToString()
                                li.Value = rowChoice("PEStandardDetiNo").ToString()
                                If Generic.ToInt(li.Value) > 0 Then
                                    rbl.Items.Add(li)
                                End If
                            Next
                            If rowObj("IsRequired") And IsEnabled = True Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardObjTempNo
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = rbl.ID
                                rf.Text = "* This field is required."
                                rf.ErrorMessage = "Item " & rowQuestion("PEStandardCode")
                                rf.ValidationGroup = "EvalValidationGroup"
                                rf.Style.Add("color", "red")
                                pForm.Controls.Add(rf)
                            End If
                            pForm.Controls.Add(New LiteralControl("<div class='col-md-7'>"))
                            pForm.Controls.Add(rbl)
                            pForm.Controls.Add(New LiteralControl("</div>"))

                        Case 2
                            'Checkbox
                            Dim cbl As New CheckBoxList
                            cbl.ID = "cbl" & PEStandardObjTempNo
                            cbl.Enabled = IsEnabled
                            For Each rowChoice As DataRow In ds.Tables(0).Select("PEStandardObjTempNo=" & PEStandardObjTempNo)
                                Dim li As New ListItem
                                li.Text = "&nbsp;&nbsp;&nbsp;" & rowChoice("Anchor").ToString()
                                li.Value = rowChoice("PEStandardDetiNo").ToString()
                                If Generic.ToInt(li.Value) > 0 Then
                                    cbl.Items.Add(li)
                                End If
                            Next
                            pForm.Controls.Add(New LiteralControl("<div class='col-md-7'>"))
                            pForm.Controls.Add(cbl)
                            pForm.Controls.Add(New LiteralControl("</div>"))

                        Case 3
                            'Narrative
                            Dim txt As New TextBox
                            txt.ID = "txt" & PEStandardObjTempNo
                            txt.TextMode = TextBoxMode.MultiLine
                            txt.CssClass = "form-control"
                            txt.Rows = 2
                            txt.Enabled = IsEnabled
                            pForm.Controls.Add(New LiteralControl("<div class='col-md-7'>"))
                            pForm.Controls.Add(txt)
                            pForm.Controls.Add(New LiteralControl("</div>"))
                            If rowObj("IsRequired") And IsEnabled = True Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardObjTempNo
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = txt.ID
                                rf.Text = "* This field is required."
                                rf.ValidationGroup = "EvalValidationGroup"
                                rf.Style.Add("color", "red")
                                pForm.Controls.Add(rf)
                            End If

                        Case 4
                            'Dropdown
                            Dim ddl As New DropDownList
                            ddl.ID = "ddl" & PEStandardObjTempNo
                            Dim l As New ListItem
                            l.Text = "-- Select --"
                            l.Value = "0"
                            ddl.CssClass = "form-control"
                            ddl.Enabled = IsEnabled
                            ddl.Items.Add(l)
                            For Each rowChoice As DataRow In ds.Tables(0).Select("PEStandardObjTempNo=" & PEStandardObjTempNo)
                                Dim li As New ListItem
                                li.Text = Generic.ToStr(rowChoice("Anchor"))
                                li.Value = Generic.ToInt(rowChoice("PEStandardDetiNo"))
                                If Generic.ToInt(li.Value) > 0 Then
                                    ddl.Items.Add(li)
                                End If
                            Next
                            If rowObj("IsRequired") And IsEnabled = True Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardObjTempNo
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = ddl.ID
                                rf.Text = "* This field is required."
                                rf.InitialValue = "0"
                                rf.ValidationGroup = "EvalValidationGroup"
                                pForm.Controls.Add(rf)
                            End If
                            pForm.Controls.Add(New LiteralControl("<div class='col-md-7'>"))
                            pForm.Controls.Add(ddl)
                            pForm.Controls.Add(New LiteralControl("</div>"))

                        Case 5
                            'Numeric
                            Dim txt As New TextBox
                            txt.ID = "txt" & PEStandardObjTempNo
                            txt.CssClass = "form-control"
                            txt.Enabled = IsEnabled
                            pForm.Controls.Add(New LiteralControl("<div class='col-md-3'>"))
                            pForm.Controls.Add(txt)
                            pForm.Controls.Add(New LiteralControl("</div>"))
                            If rowObj("IsRequired") And IsEnabled = True Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardObjTempNo.ToString()
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = txt.ID
                                rf.Text = "* This field is required."
                                rf.ValidationGroup = "EvalValidationGroup"
                                pForm.Controls.Add(rf)
                            End If
                            Dim cf As New CompareValidator
                            cf.ID = "cf" & PEStandardObjTempNo
                            cf.Display = ValidatorDisplay.Dynamic
                            cf.ControlToValidate = txt.ID
                            cf.Text = "*"
                            cf.Operator = ValidationCompareOperator.DataTypeCheck
                            cf.Type = ValidationDataType.Double
                            pForm.Controls.Add(cf)

                            Dim filtertxt As AjaxControlToolkit.FilteredTextBoxExtender
                            filtertxt = New AjaxControlToolkit.FilteredTextBoxExtender()
                            filtertxt.ID = "filtertxt" & PEStandardObjTempNo
                            filtertxt.FilterType = AjaxControlToolkit.FilterTypes.Custom
                            filtertxt.ValidChars = "1234567890.-"
                            filtertxt.TargetControlID = txt.ID
                            pForm.Controls.Add(filtertxt)
                    End Select

                    pForm.Controls.Add(New LiteralControl("</div>"))
                Next

                pForm.Controls.Add(New LiteralControl("</div>"))

                pForm.Controls.Add(New LiteralControl("<ul class='panel-controls'>"))
                pForm.Controls.Add(New LiteralControl("<li>"))
                pForm.Controls.Add(lnkMoveUp)
                pForm.Controls.Add(New LiteralControl("</li>"))
                pForm.Controls.Add(New LiteralControl("<li>"))
                pForm.Controls.Add(lnkMoveDown)
                pForm.Controls.Add(New LiteralControl("</li>"))
                pForm.Controls.Add(New LiteralControl("</ul>"))

                pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("</div>"))
            Next
            pForm.Controls.Add(New LiteralControl("</div>"))
            pForm.Controls.Add(New LiteralControl("</div>"))
            pForm.Controls.Add(New LiteralControl("</div>"))
            'Generic.EnableControls(Me, "pForm", True)
        Catch ex As Exception

        End Try


    End Sub

    'MBO Answer
    Private Sub MBOPopulateAnswer()
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPEStandardObjTemp_WebFormAns", UserNo, pestandardmainno, pestandardcateno, peevaluatorno)
            Dim dtQuestion As DataTable
            dtQuestion = ds.Tables(0)
            For Each rowQuestion As DataRow In dtQuestion.Rows
                Dim txtRemarks As New TextBox
                Dim ResponseTypeNo As Integer = Generic.ToInt(rowQuestion("ResponseTypeNo"))
                Dim PEStandardObjTempNo As Integer = Generic.ToInt(rowQuestion("PEStandardObjTempNo"))
                Select Case ResponseTypeNo
                    Case 1
                        'Radio Button
                        Dim rbl As New RadioButtonList
                        rbl = pForm.FindControl("rbl" & PEStandardObjTempNo.ToString())
                        rbl.SelectedValue = Generic.ToStr(rowQuestion("AnswerNo"))
                    Case 2
                        'Checkbox
                        Dim cbl As New CheckBoxList
                        cbl = pForm.FindControl("cbl" & PEStandardObjTempNo.ToString())
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
                    Case 3
                        'Narrative
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & PEStandardObjTempNo.ToString())
                        txt.Text = Generic.ToStr(rowQuestion("AnswerNo"))
                    Case 4
                        'Dropdown
                        Dim ddl As New DropDownList
                        ddl = pForm.FindControl("ddl" & PEStandardObjTempNo.ToString())
                        Try
                            ddl.SelectedValue = Generic.ToStr(rowQuestion("AnswerNo"))
                        Catch ex As Exception

                        End Try
                    Case 5
                        'Numeric
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & PEStandardObjTempNo.ToString())
                        txt.Text = Generic.ToStr(rowQuestion("AnswerNo"))
                End Select

            Next

        Catch ex As Exception

        End Try

    End Sub

    'MBO Save
    Private Function MBOSaveRecord() As Boolean
        Dim i As Integer = 0
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPEStandardObjTemp_WebForm", UserNo, pestandardmainno, pestandardcateno, peevaluatorno, pecycleno, componentno)
            Dim dtQuestion As DataTable
            dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PEStandardObjTempNo", "PEStandardNo", "Description", "ResponseTypeNo", "IsRequired")

            For Each rowQuestion As DataRow In dtQuestion.Rows
                Dim txtRemarks As New TextBox
                Dim AnswerNo As String = "", AnswerDesc As String = "", Remarks As String = ""
                Dim ResponseTypeNo As Integer = Generic.ToInt(rowQuestion("ResponseTypeNo"))
                Dim PEStandardNo As Integer = Generic.ToInt(rowQuestion("PEStandardNo"))
                Dim PEStandardObjTempNo As Integer = Generic.ToInt(rowQuestion("PEStandardObjTempNo"))
                Dim PEStandardObjTempDetiNo As String = 0
                Select Case ResponseTypeNo
                    Case 1
                        'Radio button
                        Dim rbl As New RadioButtonList
                        rbl = pForm.FindControl("rbl" & PEStandardObjTempNo)
                        PEStandardObjTempDetiNo = Generic.ToStr(rbl.SelectedValue)
                        AnswerNo = Generic.ToStr(rbl.SelectedValue)
                        If PEStandardObjTempDetiNo > "" Then
                            AnswerDesc = rbl.SelectedItem.Text
                        End If
                    Case 2
                        'Checkbox
                        Dim cbl As New CheckBoxList
                        cbl = pForm.FindControl("cbl" & PEStandardObjTempNo)
                        'EvalTemplateDetlChoiceNo = 0
                        AnswerNo = CheckBoxValue(cbl, 1)
                        AnswerDesc = CheckBoxValue(cbl, 0)
                    Case 3
                        'Narrative
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & PEStandardObjTempNo)
                        'EvalTemplateDetlChoiceNo = 0
                        AnswerNo = txt.Text
                        AnswerDesc = txt.Text
                    Case 4
                        'Dropdown
                        Dim ddl As New DropDownList
                        ddl = pForm.FindControl("ddl" & PEStandardObjTempNo)
                        PEStandardObjTempDetiNo = Generic.ToStr(ddl.SelectedValue)
                        AnswerNo = Generic.ToStr(ddl.SelectedValue)
                        If PEStandardObjTempDetiNo > "" Then
                            AnswerDesc = ddl.SelectedItem.Text
                        End If
                    Case 5
                        'Numeric
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & PEStandardObjTempNo)
                        'EvalTemplateDetlChoiceNo = 0
                        AnswerNo = txt.Text
                        AnswerDesc = txt.Text
                End Select


                If ResponseTypeNo <> 0 Then
                    If SQLHelper.ExecuteNonQuery("EPEStandardObjTemp_WebUpdate", UserNo, PEStandardObjTempNo, PEStandardNo, peevaluatorno, PEStandardObjTempDetiNo, ResponseTypeNo, AnswerNo, AnswerDesc) > 0 Then
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

    'MBO Reset Item
    Protected Sub lnkReset_Click(sender As Object, e As EventArgs)

        Dim lnkReset As New LinkButton(), i As Integer = 0
        lnkReset = DirectCast(sender, LinkButton)
        i = Generic.ToInt(lnkReset.CommandArgument)

        If pecatetypeno = 1 Then
            Dim RetVal As Boolean = False
            Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardObjTemp_WebReset", UserNo, i)

            Dim url As String = "PEStandardForm.aspx?id=" & pestandardmainno & "&pestandardcateno=" & pestandardcateno & "&pecatetypeno=" & pecatetypeno & "&peevaluatorno=" & peevaluatorno & "pecycleno=" & pecycleno & "&isposted=" & isposted
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        End If

    End Sub

#End Region


#Region "******** Item ********"

    'Add
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EPEStandardObjTemp_WebValidate", UserNo, pestandardmainno, pestandardcateno, peevaluatorno)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If

        If pecatetypeno = 1 Then
            Dim RetVal As Boolean = False
            Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardObjTemp_WebAdd", UserNo, pestandardmainno, pestandardcateno, peevaluatorno)

            Dim url As String = "PEStandardForm.aspx?id=" & pestandardmainno & "&pestandardcateno=" & pestandardcateno & "&pecatetypeno=" & pecatetypeno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&isposted=" & isposted
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        ElseIf pecatetypeno = 2 Then

            Generic.ClearControls(Me, "pnlPopupItem")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupItem", Generic.ToInt(Session("xPayLocNo")))
            cboPEStandardDimNo.Enabled = True
            cboPEStandardDimNo.Text = ""
            mdlItem.Show()

        End If


    End Sub

    'Add Item in Dimension
    Protected Sub lnkNewItem_Click(sender As Object, e As EventArgs)

        Dim lnkNewItem As New LinkButton()
        lnkNewItem = DirectCast(sender, LinkButton)
        Dim i As Integer = Generic.ToInt(lnkNewItem.CommandArgument)
        Generic.ClearControls(Me, "pnlPopupItem")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupItem", Generic.ToInt(Session("xPayLocNo")))
        cboPEStandardDimNo.Enabled = False
        If i = 0 Then
            cboPEStandardDimNo.Text = ""
        Else
            cboPEStandardDimNo.Text = i
        End If
        mdlItem.Show()

    End Sub

    'Edit
    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnkEdit As New LinkButton(), i As Integer = 0
        lnkEdit = DirectCast(sender, LinkButton)
        i = Generic.ToInt(lnkEdit.CommandArgument)

        Generic.ClearControls(Me, "pnlPopupItem")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupItem", Generic.ToInt(Session("xPayLocNo")))

        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandard_WebOne", UserNo, i)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupItem", dt)
            Next
        Catch ex As Exception

        End Try

        cboPEStandardDimNo.Enabled = False
        mdlItem.Show()

    End Sub

    'Save
    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim ret As Boolean = False

        If pecatetypeno = 1 Then 'MBO
            ret = MBOSaveRecord()
        ElseIf pecatetypeno = 2 Then 'KRA
            'ret = SaveRecord()
        End If

        If ret Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkSaveItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim PEStandardNo As Integer = Generic.ToInt(Me.txtPEStandardNo.Text)
        Dim PEStandardCode As String = Generic.ToStr(Me.txtPEStandardCode.Text)
        Dim PEStandardDesc As String = Generic.ToStr(Me.txtPEStandardDesc.Text)
        Dim Standard As String = Generic.ToStr(Me.txtStandard.Text)
        Dim PEStandardDimNo As Integer = Generic.ToInt(Me.cboPEStandardDimNo.SelectedValue)
        Dim OrderLevel As Integer = Generic.ToInt(Me.txtOrderLevelItem.Text)
        Dim ResponseTypeNo As Integer = Generic.ToInt(Me.cboResponseTypeNo.SelectedValue)
        Dim HasComment As Boolean = Generic.ToBol(Me.chkHasComment.Checked)
        Dim IsRequired As Boolean = Generic.ToBol(Me.chkIsRequired.Checked)

        If SQLHelper.ExecuteNonQuery("EPEStandard_WebSave", UserNo, PEStandardNo, pestandardmainno, PEStandardCode, PEStandardDesc, Standard, PEStandardDimNo, pestandardcateno, ResponseTypeNo, OrderLevel, HasComment, IsRequired) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            Dim url As String = "PEStandardForm.aspx?id=" & pestandardmainno & "&pestandardcateno=" & pestandardcateno & "&pecatetypeno=" & pecatetypeno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&isposted=" & isposted
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    'Delete
    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim lnkDelete As New LinkButton()
        lnkDelete = DirectCast(sender, LinkButton)
        Dim i As Integer
        i = Generic.ToInt(lnkDelete.CommandArgument)

        If i > 0 Then
            Generic.DeleteRecordAuditCol("EPEStandardDeti", UserNo, "PEStandardNo", i)
            Generic.DeleteRecordAudit("EPEStandard", UserNo, i)

            Dim ds As New DataSet
            Dim dtQuestion As DataTable

            If pecatetypeno = 1 Then 'MBO Update Item No.
                ds = SQLHelper.ExecuteDataSet("EPEStandardObjTemp_WebForm", UserNo, pestandardmainno, pestandardcateno, peevaluatorno, pecycleno, componentno)
                dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PEStandardNo", "PEStandardCode", "OrderLevelItem")
                For Each rowQuestion As DataRow In dtQuestion.Rows
                    Dim PEStandardNo As Integer = Generic.ToInt(rowQuestion("PEStandardNo"))
                    Dim OrderLevelItem As Integer = Generic.ToInt(rowQuestion("OrderLevelItem"))
                    SQLHelper.ExecuteDataSet("EPEStandard_WebDelete", PEStandardNo, OrderLevelItem)
                Next
            ElseIf pecatetypeno = 2 Then 'KRA Update Item No.
                ds = SQLHelper.ExecuteDataSet("EPEStandard_WebForm", UserNo, pestandardmainno, pestandardcateno, peevaluatorno, pecycleno, componentno)
                dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PEStandardNo", "PEStandardCode", "OrderLevelItem")
                For Each rowQuestion As DataRow In dtQuestion.Rows
                    Dim PEStandardNo As Integer = Generic.ToInt(rowQuestion("PEStandardNo"))
                    Dim OrderLevelItem As Integer = Generic.ToInt(rowQuestion("OrderLevelItem"))
                    SQLHelper.ExecuteDataSet("EPEStandard_WebDelete", PEStandardNo, OrderLevelItem)
                Next
            End If


            Dim url As String = "PEStandardForm.aspx?id=" & pestandardmainno & "&pestandardcateno=" & pestandardcateno & "&pecatetypeno=" & pecatetypeno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&isposted=" & isposted
            MessageBox.SuccessResponse(MessageTemplate.SuccessDelete, Me, url)
        End If

    End Sub


#End Region


#Region "******** Item Details ********"

    Protected Sub lnkEditDeti_Click(sender As Object, e As EventArgs)

        Dim lnkEditDeti As New LinkButton(), i As Integer = 0
        lnkEditDeti = DirectCast(sender, LinkButton)
        ViewState("PEStandardNo") = Generic.ToInt(Generic.Split(lnkEditDeti.CommandArgument, 0))
        ViewState("PEStandardObjTempNo") = Generic.ToInt(Generic.Split(lnkEditDeti.CommandArgument, 1))
        lblChoices.Text = "Item No. " & Generic.ToStr(Generic.Split(lnkEditDeti.CommandArgument, 2))

        PopulateChoices()
        mdlChoices.Show()

    End Sub

    Protected Sub PopulateChoices()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EPEStandardDeti_Web", UserNo, Generic.ToInt(ViewState("PEStandardNo")), Generic.ToInt(ViewState("PEStandardObjTempNo")))
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If

            grdChoices.DataSource = dv
            grdChoices.DataBind()

            mdlChoices.Show()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdChoices_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            grdChoices.PageIndex = e.NewPageIndex
            PopulateChoices()
            mdlChoices.Show()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkDeleteChoices_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, Count As Integer = 0, lblOrder As New Label

        For tcount = 0 To Me.grdChoices.Rows.Count - 1
            lbl = CType(grdChoices.Rows(tcount).FindControl("lblDetlNo"), Label)
            tcheck = CType(grdChoices.Rows(tcount).FindControl("txtdIsSelect"), CheckBox)
            If tcheck.Checked = True Then
                Generic.DeleteRecordAudit("EPEStandardDeti", UserNo, CType(lbl.Text, Integer))
                Count = Count + 1
            End If
        Next

        PopulateChoices()

        For tcount = 0 To Me.grdChoices.Rows.Count - 1
            lbl = CType(grdChoices.Rows(tcount).FindControl("lblDetlNo"), Label)
            lblOrder = CType(grdChoices.Rows(tcount).FindControl("lblOrder"), Label)
            Dim OrderLevel As Integer = Generic.ToInt(lblOrder.Text)

            If SQLHelper.ExecuteNonQuery("EPEStandardDeti_WebDelete", CType(lbl.Text, Integer), OrderLevel) > 0 Then
                PopulateChoices()
            End If
        Next

        If Count > 0 Then
            MessageBox.Alert("(" + Count.ToString + ") " + MessageTemplate.SuccessDelete, "success", Me)
        Else
            MessageBox.Alert(MessageTemplate.NoSelectedTransaction, "information", Me)
        End If

        mdlChoices.Show()

    End Sub

    'Submit record
    Protected Sub lnkSaveChoices_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lblObjNo As New Label, lblOrder As New Label, lblDetlNo As New Label, txtDescription As New TextBox, cboPERatingNo As New DropDownList, txtProfeciency As New TextBox
        Dim tcount As Integer, Count As Integer = 0


        For tcount = 0 To Me.grdChoices.Rows.Count - 1
            lblOrder = CType(grdChoices.Rows(tcount).FindControl("lblOrder"), Label)
            lblDetlNo = CType(grdChoices.Rows(tcount).FindControl("lblDetlNo"), Label)
            cboPERatingNo = CType(grdChoices.Rows(tcount).FindControl("cboPERatingNo"), DropDownList)
            txtDescription = CType(grdChoices.Rows(tcount).FindControl("txtAnchor"), TextBox)
            txtProfeciency = CType(grdChoices.Rows(tcount).FindControl("txtProfeciency"), TextBox)

            Dim PEStandardNo As Integer = Generic.ToInt(ViewState("PEStandardNo"))
            Dim PEStandardObjTempNo As Integer = Generic.ToInt(ViewState("PEStandardObjTempNo"))
            Dim OrderLevel As String = Generic.ToStr(lblOrder.Text)
            Dim PEStandardDetiNo As Integer = Generic.ToInt(lblDetlNo.Text)
            Dim Description As String = Generic.ToStr(txtDescription.Text)
            Dim PERatingNo As Integer = Generic.ToInt(cboPERatingNo.SelectedValue)
            Dim Profeciency As Double = Generic.ToDec(txtProfeciency.Text)

            If Description > "" Then
                If SQLHelper.ExecuteNonQuery("EPEStandardDeti_WebSave", UserNo, PEStandardDetiNo, PEStandardNo, PEStandardObjTempNo, PERatingNo, Description, Profeciency, OrderLevel) > 0 Then
                    Count = Count + 1
                End If
            End If

        Next


        If Count > 0 Then
            Count = Count - 1
            'MessageBox.Alert(MessageTemplate.SuccessSave, "success", Me)

            PopulateChoices()
            Dim lbl As New Label
            For tcount = 0 To Me.grdChoices.Rows.Count - 1
                lbl = CType(grdChoices.Rows(tcount).FindControl("lblDetlNo"), Label)
                lblOrder = CType(grdChoices.Rows(tcount).FindControl("lblOrder"), Label)
                Dim OrderLevel As Integer = Generic.ToInt(lblOrder.Text)

                If SQLHelper.ExecuteNonQuery("EPEStandardDeti_WebDelete", CType(lbl.Text, Integer), OrderLevel) > 0 Then
                    PopulateChoices()
                End If
            Next

        Else
            MessageBox.Alert(MessageTemplate.NoSelectedTransaction, "information", Me)
        End If

        mdlChoices.Show()

    End Sub

    Protected Sub lnkRefreshChoices_Click(sender As Object, e As EventArgs)

        Dim url As String = "PEStandardForm.aspx?id=" & pestandardmainno & "&pestandardcateno=" & pestandardcateno & "&pecatetypeno=" & pecatetypeno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&isposted=" & isposted
        MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)

    End Sub

#End Region


#Region "******** Chat Box Journal ********"
    Protected Sub lnkJournal_Click(sender As Object, e As EventArgs)
        Dim lnkJournal As New LinkButton()
        lnkJournal = DirectCast(sender, LinkButton)
        Session("Journal_ID") = Generic.ToInt(lnkJournal.CommandArgument)

        ChatBox1.xChatType = 1 'Journal
        ChatBox1.xID = Generic.ToInt(Session("Journal_ID"))
        ChatBox1.Show()
    End Sub

    Protected Sub lnkSend_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If SQLHelper.ExecuteNonQuery("EChat_WebSave", UserNo, 1, Generic.ToInt(Session("Journal_ID")), OnlineEmpNo, Generic.ToStr(ChatBox1.SendText), Generic.ToInt(Session("xPayLocNo"))) > 0 Then
            ChatBox1.SendClear()
            ChatBox1.xChatType = 1 'Journal
            ChatBox1.xID = Generic.ToInt(Session("Journal_ID"))
            ChatBox1.Show()
        End If
    End Sub
#End Region


#Region "******** Performance Cycle Setting ********"

    Protected Sub lnkCycle_Click(sender As Object, e As System.EventArgs)
        mdlSetting.Show()
    End Sub

    Protected Sub lnkSaveSetting_Click(sender As Object, e As EventArgs)

        pecycleno = Generic.ToInt(cboPECycleNo.SelectedValue)
        Dim url As String = "PEStandardForm.aspx?id=" & pestandardmainno & "&pestandardcateno=" & pestandardcateno & "&pecatetypeno=" & pecatetypeno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&isposted=" & isposted
        MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)

    End Sub





#End Region


#Region "******** Edit Category ********"

    Protected Sub lnkCate_Click(sender As Object, e As EventArgs)
        Dim lnkCate As New LinkButton()
        lnkCate = DirectCast(sender, LinkButton)
        Dim i As Integer = Generic.ToInt(lnkCate.CommandArgument)
        ViewState("PEStandardCateNo") = i

        Generic.ClearControls(Me, "pnlPopupCate")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupCate", Generic.ToInt(Session("xPayLocNo")))
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEStandardCate_WebOne", UserNo, i)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "pnlPopupCate", dt)
        Next

        If pecatetypeno = 1 Then
            txtIsAddCate.Enabled = True
            txtIsDeleteCate.Enabled = True
            txtIsEditCate.Enabled = False
            txtIsAddCateDeti.Enabled = True
            txtIsDeleteCateDeti.Enabled = True
            txtIsEditCateDeti.Enabled = True
            txtIsDistributeWeight.Enabled = True
            'txtNoOfItems.Enabled = True
        ElseIf pecatetypeno = 2 Then
            txtIsAddCate.Enabled = True
            txtIsDeleteCate.Enabled = False
            txtIsEditCate.Enabled = False
            txtIsAddCateDeti.Enabled = False
            txtIsDeleteCateDeti.Enabled = False
            txtIsEditCateDeti.Enabled = False
            txtIsDistributeWeight.Enabled = False
            'txtNoOfItems.Enabled = True
        End If


        mdlCate.Show()

    End Sub


    Protected Sub lnkSaveCate_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim PEStandardCateNo As Integer = Generic.ToInt(ViewState("PEStandardCateNo"))
        Dim IsAddCate As Boolean = Generic.ToBol(Me.txtIsAddCate.Checked)
        Dim IsDeleteCate As Boolean = Generic.ToBol(Me.txtIsDeleteCate.Checked)
        Dim IsEditCate As Boolean = Generic.ToBol(Me.txtIsEditCate.Checked)
        Dim IsAddCateDeti As Boolean = Generic.ToBol(Me.txtIsAddCateDeti.Checked)
        Dim IsDeleteCateDeti As Boolean = Generic.ToBol(Me.txtIsDeleteCateDeti.Checked)
        Dim IsEditCateDeti As Boolean = Generic.ToBol(Me.txtIsEditCateDeti.Checked)
        Dim NoOfItems As Integer = Generic.ToInt(Me.txtNoOfItems.Text)
        Dim IsDistributeWeight As Boolean = Generic.ToBol(Me.txtIsDistributeWeight.Checked)

        If SQLHelper.ExecuteNonQuery("EPEStandardCate_WebUpdate", UserNo, PEStandardCateNo, IsAddCate, IsDeleteCate, IsEditCate, IsAddCateDeti, IsDeleteCateDeti, IsEditCateDeti, NoOfItems, IsDistributeWeight) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub


#End Region


#Region "******** Edit Dimension ********"

    Protected Sub lnkDim_Click(sender As Object, e As EventArgs)
        Dim lnkDim As New LinkButton()
        lnkDim = DirectCast(sender, LinkButton)
        Dim i As Integer = Generic.ToInt(lnkDim.CommandArgument)
        ViewState("PEStandardDimNo") = i

        Generic.ClearControls(Me, "pnlPopupDim")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDim", Generic.ToInt(Session("xPayLocNo")))
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEStandardDim_WebOne", UserNo, i)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "pnlPopupDim", dt)
        Next

        mdlDim.Show()
    End Sub

    Protected Sub lnkSaveDim_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim PEStandardDimNo As Integer = Generic.ToInt(ViewState("PEStandardDimNo"))
        Dim IsAddDim As Boolean = Generic.ToBol(Me.txtIsAddDim.Checked)
        Dim IsDeleteDim As Boolean = Generic.ToBol(Me.txtIsDeleteDim.Checked)
        Dim IsEditDim As Boolean = Generic.ToBol(Me.txtIsEditDim.Checked)
        Dim IsAddDimDeti As Boolean = Generic.ToBol(Me.txtIsAddDimDeti.Checked)
        Dim IsDeleteDimDeti As Boolean = Generic.ToBol(Me.txtIsDeleteDimDeti.Checked)
        Dim IsEditDimDeti As Boolean = Generic.ToBol(Me.txtIsEditDimDeti.Checked)

        If SQLHelper.ExecuteNonQuery("EPEStandardDim_WebUpdate", UserNo, PEStandardDimNo, IsAddDim, IsDeleteDim, IsEditDim, IsAddDimDeti, IsDeleteDimDeti, IsEditDimDeti) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

#End Region


#Region "******** Move Up & Down ********"

    Protected Sub lnkMoveUp_Click(sender As Object, e As EventArgs)
        Dim lnkMoveUp As New LinkButton()
        lnkMoveUp = DirectCast(sender, LinkButton)
        Dim i As Integer = Generic.ToInt(lnkMoveUp.CommandArgument)

        Dim Retval As Boolean = False
        If SQLHelper.ExecuteNonQuery("EPEStandard_WebUpdate", UserNo, i, 2) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            Dim url As String = "PEStandardForm.aspx?id=" & pestandardmainno & "&pestandardcateno=" & pestandardcateno & "&pecatetypeno=" & pecatetypeno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&isposted=" & isposted
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            'Else
            '    MessageBox.Critical("Unable to move", Me)
        End If

    End Sub

    Protected Sub lnkMoveDown_Click(sender As Object, e As EventArgs)
        Dim lnkMoveDown As New LinkButton()
        lnkMoveDown = DirectCast(sender, LinkButton)
        Dim i As Integer = Generic.ToInt(lnkMoveDown.CommandArgument)

        Dim Retval As Boolean = False
        If SQLHelper.ExecuteNonQuery("EPEStandard_WebUpdate", UserNo, i, 1) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            Dim url As String = "PEStandardForm.aspx?id=" & pestandardmainno & "&pestandardcateno=" & pestandardcateno & "&pecatetypeno=" & pecatetypeno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&isposted=" & isposted
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            'Else
            '    MessageBox.Critical("Unable to move", Me)
        End If

    End Sub

#End Region
   
End Class
