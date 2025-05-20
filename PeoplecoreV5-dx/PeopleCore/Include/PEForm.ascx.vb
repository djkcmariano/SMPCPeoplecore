Imports clsLib
Imports System.Data
Imports System.IO


Partial Class Include_PEForm
    Inherits System.Web.UI.UserControl

    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer
    Dim pereviewmainno As Integer = 0
    Dim pereviewcateno As Integer = 0
    Dim pecatetypeno As Integer = 0
    Dim pereviewno As Integer = 0
    Dim pecycleno As Integer = 0
    Dim componentno As Integer = 0
    'Dim pereviewevaluatorno As Integer = 0
    Dim peevaluatorno As Integer = 0
    Dim TabNo As Integer = 0
    Dim Journal_ID As Integer = 0
    Dim OnlineEmpNo As Integer = 0
    Dim isposted As Boolean = False
    Dim FormName As String = ""

    Dim IsAddCate As Boolean = False
    Dim IsEditCate As Boolean = False
    Dim IsDeleteCate As Boolean = False
    Dim IsAddDim As Boolean = False
    Dim IsEditDim As Boolean = False
    Dim IsDeleteDim As Boolean = False
    Dim IsAddDeti As Boolean = False
    Dim IsEditDeti As Boolean = False
    Dim IsDeleteDeti As Boolean = False

    Dim _ds As New DataSet
    Dim _dt As New DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        OnlineEmpNo = Generic.ToInt(Session("EmployeeNo"))

        If Not IsPostBack Then
            PopulateGrid()
            PopulateCombo()
        End If

        AddHandler ChatBox1.lnkSendClick, AddressOf lnkSend_Click

    End Sub

    Protected Overrides Sub OnInit(e As EventArgs)
        MyBase.OnInit(e)
        pereviewmainno = Generic.ToInt(Request.QueryString("pereviewmainno"))
        pereviewcateno = Generic.ToInt(Request.QueryString("pereviewcateno"))
        pecatetypeno = Generic.ToInt(Request.QueryString("pecatetypeno"))
        pereviewno = Generic.ToInt(Request.QueryString("pereviewno"))
        peevaluatorno = Generic.ToInt(Request.QueryString("peevaluatorno"))
        pecycleno = Generic.ToInt(Request.QueryString("pecycleno"))
        componentno = Generic.ToInt(Request.QueryString("componentno"))
        isposted = Generic.ToBol(Request.QueryString("isposted"))
        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        FormName = Path.GetFileName(FileInfo.ToString)


        PopulateHeader()
        EnabledControls(pecatetypeno)
    End Sub

    Private Sub EnabledControls(ByVal pecatetypeno As Integer)

        If componentno = 1 Then
            DivSettings.Visible = True
        Else
            DivSettings.Visible = False
        End If

        If pecatetypeno = 1 Then 'MBO
            PopulateCate()
            PopulateGoals()
            MBOPopulateAnswer()
        ElseIf pecatetypeno = 2 Then 'KRA
            PopulateCate()
            PopulateQuestion()
            PopulateAnswer()
        ElseIf pecatetypeno = 3 Then 'Competency
            PopulateComp()
            PopulateCompAnswer()
        ElseIf pecatetypeno = 4 Then 'ACI Goals
            If Generic.ToStr(Session("SubFormGoals")) = "Tasks" Then
                pnlGoals.Visible = False
                pnlTask.Visible = True
                pnlFiles.Visible = False
                PopulateTask()
            ElseIf Generic.ToStr(Session("SubFormGoals")) = "Files" Then
                pnlGoals.Visible = False
                pnlTask.Visible = False
                pnlFiles.Visible = True
                PopulateFiles()
            Else
                pnlGoals.Visible = True
                pnlTask.Visible = False
                pnlFiles.Visible = False
                PopulateGoalTable()
            End If
            PopulateGoalSaveButton()
        Else
            lnkAdd.Visible = False
            lnkSave.Visible = False
        End If

        If pecatetypeno = 4 Then
            pnlNewItem.Visible = False
        Else
            pnlNewItem.Visible = True
        End If

    End Sub

#Region "******** ACI Goals ********"

    Protected Sub PopulateGoalSaveButton()
        Try
            Dim dt As DataTable
            Dim IsShowAddGoals As Boolean = False
            Dim IsShowAddTask As Boolean = False
            Dim IsShowAddFiles As Boolean = False
            dt = SQLHelper.ExecuteDataTable("EPEStandardRev_WebGoals_ShowButton", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno)
            For Each row As DataRow In dt.Rows
                IsShowAddGoals = Generic.ToBol(row("IsShowAddGoals"))
                IsShowAddTask = Generic.ToBol(row("IsShowAddTask"))
                IsShowAddFiles = Generic.ToBol(row("IsShowAddFiles"))
            Next

            lnkAddGoals.Visible = IsShowAddGoals
            lnkAddTask.Visible = IsShowAddTask
            lnkAddFiles.Visible = IsShowAddFiles
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub PopulateGoalTable()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("EPEStandardRev_WebGoals", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno)
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

            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EPEStandardRev_WebGoalsSummary", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno)
            For Each rowx As DataRow In dtx.Rows
                lblTotalWeight.Text = Generic.ToInt(rowx("TotalWeight"))
                lblTotalRating.Text = Generic.ToStr(rowx("TotalRating"))
            Next

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub PopulateGoalSummary()

        'Compute the PEReviewCate for summary
        SQLHelper.ExecuteNonQuery("EPEStandardRev_WebGoalsCompute", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno)

    End Sub

    Protected Sub lnkAddGoals_Click(sender As Object, e As EventArgs)

        Generic.ClearControls(Me, "pnlPopupGoals")
        Generic.EnableControls(Me, "pnlPopupGoals", True)
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupGoals", Generic.ToInt(Session("xPayLocNo")))
        PopulateGoalTable()
        txtGActualDate.Enabled = False
        mdlGoals.Show()

    End Sub

    Protected Sub lnkSaveGoals_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim GPEStandardRevNo As Integer = Generic.ToInt(txtGPEStandardRevNo.Text)
        Dim GPEStandardCode As String = ""
        Dim GPEStandardDesc As String = Generic.ToStr(txtGPEStandardDesc.Text)
        Dim GStandard As String = Generic.ToStr(txtGStandard.Html)
        Dim PEReviewDimNo As Integer = Generic.ToInt(cboGPEReviewDimNo.SelectedValue)
        Dim ResponseTypeNo As Integer = 0
        Dim GOrderLevel As Integer = Generic.ToInt(txtGOrderLevel.Text)
        Dim HasComment As Boolean = False
        Dim IsRequired As Boolean = False
        Dim Weighted As Double = Generic.ToDbl(txtGWeighted.Text)
        Dim Target As Double = 0
        Dim PEStandardStatNo As Integer = Generic.ToInt(cboGPEStandardStatNo.SelectedValue)
        Dim PEPriorityNo As Integer = Generic.ToInt(cboGPEPriorityNo.SelectedValue)
        Dim StartDate As String = Generic.ToStr(txtGStartDate.Text)
        Dim EndDate As String = Generic.ToStr(txtGEndDate.Text)
        Dim ActualDate As String = Generic.ToStr(txtGActualDate.Text)
        Dim GComments As String = Generic.ToStr(txtGComments.Html)
        Dim PERatingNo As Integer = 0 'Generic.ToInt(cboGPERatingNo.SelectedValue)
        Dim Proficiency As Double = Generic.ToDbl(txtGProficiency.Text)
        Dim Rating As Double = 0
        If txtGRating.Text = "Invalid value." Then
            Rating = 0
        Else
            Rating = Generic.ToDbl(txtGRating.Text)
        End If


        Dim dtsum As New DataTable
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EPEStandardRev_WebGoalsValidate", UserNo, pereviewmainno, pereviewno, GPEStandardRevNo, GPEStandardCode, GPEStandardDesc, GStandard, pereviewcateno, PEReviewDimNo, ResponseTypeNo, GOrderLevel, HasComment, IsRequired, peevaluatorno, pecycleno, Weighted, Rating, Target, PEStandardStatNo, PEPriorityNo, StartDate, EndDate, ActualDate, GComments, componentno, PERatingNo, Proficiency)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            PopulateGoalTable()
            mdlGoals.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EPEStandardRev_WebGoalsSave", UserNo, pereviewmainno, pereviewno, GPEStandardRevNo, GPEStandardCode, GPEStandardDesc, GStandard, pereviewcateno, PEReviewDimNo, ResponseTypeNo, GOrderLevel, HasComment, IsRequired, peevaluatorno, pecycleno, Weighted, Rating, Target, PEStandardStatNo, PEPriorityNo, StartDate, EndDate, ActualDate, GComments, componentno, PERatingNo, Proficiency) > 0 Then
            RetVal = True
            'Compute the PEReviewCate for summary
            PopulateGoalSummary()
        Else
            RetVal = False
        End If

        If RetVal = False Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

        If RetVal = True Then
            PopulateGoalTable()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub

    Protected Sub lnkEditGoals_Click(sender As Object, e As EventArgs)

        Dim ib As New LinkButton
        ib = sender
        Generic.ClearControls(Me, "pnlPopupGoals")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupGoals", Generic.ToInt(Session("xPayLocNo")))
        PopulateDataDisabled(Me, "pnlPopupGoals", UserNo, "EPEStandardRev")
        PopulateDataGoals(Generic.ToInt(ib.CommandArgument))
        PopulateGoalTable()
        mdlGoals.Show()

    End Sub

    Protected Sub PopulateDataGoals(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardRev_WebGoalsOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupGoals", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkDeleteGoals_Click(sender As Object, e As EventArgs)
        'Dim chk As New CheckBox, ib As New LinkButton, Count As Integer = 0
        'For i As Integer = 0 To Me.grdMain.Rows.Count - 1
        '    chk = CType(grdMain.Rows(i).FindControl("chk"), CheckBox)
        '    ib = CType(grdMain.Rows(i).FindControl("lnkEditGoals"), LinkButton)
        '    If chk.Checked = True Then
        '        Generic.DeleteRecordAudit("EPEStandardRev", UserNo, Generic.ToInt(ib.CommandArgument))
        '        Count = Count + 1
        '    End If
        'Next

        Dim ib As New LinkButton
        ib = sender
        Generic.DeleteRecordAudit("EPEStandardRev", UserNo, Generic.ToInt(ib.CommandArgument))
        'Compute the PEReviewCate for summary
        PopulateGoalSummary()
        PopulateGoalTable()
        MessageBox.Success(MessageTemplate.SuccessDelete, Me)

    End Sub


    Protected Sub PopulateDataDisabled(owner As Control, ContainerID As String, UserNo As Integer, TableName As String)
        Try

            Dim page As Page = If(TryCast(owner, Page), owner.Page)
            Dim tempContainer As Control
            tempContainer = FindControlRecursive(page, ContainerID)

            Dim ResponseTypeNo As Integer
            Dim objName As String
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EFormField_WebPEReview", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno, Generic.ToInt(Session("TPEStandardRevNo")), TableName)
            For Each row As DataRow In dt.Rows
                ResponseTypeNo = Generic.ToInt(row("ResponseTypeNo"))
                objName = Generic.ToStr(row("Controls"))

                If ResponseTypeNo = 3 Then
                    Dim txt As New TextBox
                    txt = tempContainer.FindControl(objName)
                    txt.Enabled = False
                ElseIf ResponseTypeNo = 4 Then
                    Dim ddl As New DropDownList
                    ddl = tempContainer.FindControl(objName)
                    ddl.Enabled = False
                ElseIf ResponseTypeNo = 2 Then
                    Dim chk As New CheckBox
                    chk = tempContainer.FindControl(objName)
                    chk.Enabled = False
                ElseIf ResponseTypeNo = 1 Then
                    Dim rdo As New RadioButton
                    rdo = tempContainer.FindControl(objName)
                    rdo.Enabled = False
                ElseIf ResponseTypeNo = 6 Then
                    Dim htmleditor As New DevExpress.Web.ASPxHtmlEditor.ASPxHtmlEditor
                    htmleditor = tempContainer.FindControl(objName)
                    htmleditor.Enabled = False
                End If
            Next

        Catch ex As Exception

        End Try
    End Sub

    Protected Function FindControlRecursive(ctrl As Control, controlID As String) As Control
        If String.Compare(ctrl.ID, controlID, True) = 0 Then
            Return ctrl
        Else
            For Each child As Control In ctrl.Controls
                Dim lookFor As Control = FindControlRecursive(child, controlID)

                If lookFor IsNot Nothing Then
                    Return lookFor
                End If
            Next
            Return Nothing
        End If
    End Function


    Protected Sub lnkGoals_Click(sender As Object, e As EventArgs)

        Dim ib As New LinkButton
        ib = sender
        Session("SubFormGoals") = "Goals"
        EnabledControls(pecatetypeno)

    End Sub



    Protected Sub lnkPreviewGoals_Click(sender As Object, e As EventArgs)
        Dim lnk As New Button
        Dim sb As New StringBuilder
        lnk = sender
        'Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        'Dim id As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo"}))
        Dim param As String = Generic.ReportParam(New ReportParameter(ReportParameter.Type.int, Generic.ToInt(Session("xPayLocNo"))), _
                                                  New ReportParameter(ReportParameter.Type.int, pereviewmainno.ToString), _
                                                  New ReportParameter(ReportParameter.Type.int, pereviewno.ToString())
                                                  )

        sb.Append("<script>")
        sb.Append("window.open('rpttemplateviewer.aspx?reportno=1180&param=" & param & "','_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes');")
        sb.Append("</script>")

        PopulateGoalTable()
        Page.ClientScript.RegisterStartupScript(e.GetType, "test", sb.ToString())
    End Sub

    Protected Sub lnkPrint_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As Button = TryCast(sender, Button)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

#End Region

#Region "******** ACI Tasks ********"

    Protected Sub lnkTask_Click(sender As Object, e As EventArgs)

        Dim ib As New LinkButton
        ib = sender
        Session("TPEStandardRevNo") = Generic.ToInt(ib.CommandArgument)
        Session("SubFormGoals") = "Tasks"
        EnabledControls(pecatetypeno)

    End Sub

    Protected Sub PopulateTask()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("EPEStandardRevTask_Web", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno, Generic.ToInt(Session("TPEStandardRevNo")))
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            grdTask.DataSource = dv
            grdTask.DataBind()



            Dim xdt As DataTable
            xdt = SQLHelper.ExecuteDataTable("EPEStandardRev_WebGoalsOne", UserNo, Generic.ToInt(Session("TPEStandardRevNo")))
            For Each row As DataRow In xdt.Rows
                lblGoalName.Text = Generic.ToStr(row("GPEStandardDesc"))
            Next
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkAddTask_Click(sender As Object, e As EventArgs)

        Generic.ClearControls(Me, "pnlPopupTask")
        Generic.EnableControls(Me, "pnlPopupTask", True)
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupTask", Generic.ToInt(Session("xPayLocNo")))
        PopulateTask()
        cboTProficiency.Text = "0"
        mdlTask.Show()

    End Sub

    Protected Sub lnkSaveTask_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim PEStandardRevTaskNo As Integer = Generic.ToInt(txtPEStandardRevTaskNo.Text)
        Dim PEStandardRevTaskCode As String = ""
        Dim PEStandardRevTaskDesc As String = Generic.ToStr(txtPEStandardRevTaskDesc.Text)
        Dim TRemarks As String = Generic.ToStr(txtTRemarks.Html)
        Dim TOrderLevel As Integer = Generic.ToInt(txtTOrderLevel.Text)
        Dim TWeighted As Double = Generic.ToDbl(txtTWeighted.Text)
        Dim TRating As Double = 0 'Generic.ToDbl(cboTRating.SelectedValue)
        Dim TPEStandardStatNo As Integer = Generic.ToInt(cboTPEStandardStatNo.SelectedValue)
        Dim TPEPriorityNo As Integer = Generic.ToInt(cboTPEPriorityNo.SelectedValue)
        Dim TStartDate As String = Generic.ToStr(txtTStartDate.Text)
        Dim TEndDate As String = Generic.ToStr(txtTEndDate.Text)
        Dim TActualDate As String = Generic.ToStr(txtTActualDate.Text)
        Dim TComments As String = Generic.ToStr(txtTComments.Html)
        Dim TPERatingNo As Integer = 0 'Generic.ToInt(cboTPERatingNo.SelectedValue)
        Dim TProficiency As Double = Generic.ToDbl(cboTProficiency.SelectedValue)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EPEStandardRevTask_WebValidate", UserNo, PEStandardRevTaskNo, Generic.ToInt(Session("TPEStandardRevNo")), PEStandardRevTaskCode, PEStandardRevTaskDesc, TRemarks, TOrderLevel, TStartDate, TEndDate, TPEStandardStatNo, TWeighted, TRating, TPEPriorityNo, TActualDate, TComments, componentno, TPERatingNo, TProficiency)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            PopulateTask()
            mdlTask.Show()
            Exit Sub
        End If


        If SQLHelper.ExecuteNonQuery("EPEStandardRevTask_WebSave", UserNo, PEStandardRevTaskNo, Generic.ToInt(Session("TPEStandardRevNo")), PEStandardRevTaskCode, PEStandardRevTaskDesc, TRemarks, TOrderLevel, TStartDate, TEndDate, TPEStandardStatNo, TWeighted, TRating, TPEPriorityNo, TActualDate, TComments, componentno, TPERatingNo, TProficiency) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal = False Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

        If RetVal = True Then
            PopulateTask()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub

    Protected Sub lnkEditTask_Click(sender As Object, e As EventArgs)

        Dim ib As New LinkButton
        ib = sender
        Generic.ClearControls(Me, "pnlPopupTask")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupTask", Generic.ToInt(Session("xPayLocNo")))
        PopulateDataDisabled(Me, "pnlPopupTask", UserNo, "EPEStandardRevTask")
        PopulateDataTask(Generic.ToInt(ib.CommandArgument))
        PopulateTask()
        mdlTask.Show()

    End Sub

    Protected Sub PopulateDataTask(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardRevTask_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupTask", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkDeleteTask_Click(sender As Object, e As EventArgs)

        Dim ib As New LinkButton
        ib = sender
        Generic.DeleteRecordAudit("EPEStandardRevTask", UserNo, Generic.ToInt(ib.CommandArgument))
        PopulateTask()
        MessageBox.Success(MessageTemplate.SuccessDelete, Me)

    End Sub

#End Region

#Region "******** ACI Files ********"

    Protected Sub lnkFiles_Click(sender As Object, e As EventArgs)

        Dim ib As New LinkButton
        ib = sender
        Session("TPEStandardRevNo") = Generic.ToInt(ib.CommandArgument)
        Session("SubFormGoals") = "Files"
        EnabledControls(pecatetypeno)

    End Sub

    Protected Sub PopulateFiles()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""

            dt = SQLHelper.ExecuteDataTable("EPEStandardRevFiles_Web", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno, Generic.ToInt(Session("TPEStandardRevNo")))
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            grdFiles.DataSource = dv
            grdFiles.DataBind()



            Dim xdt As DataTable
            xdt = SQLHelper.ExecuteDataTable("EPEStandardRev_WebGoalsOne", UserNo, Generic.ToInt(Session("TPEStandardRevNo")))
            For Each row As DataRow In xdt.Rows
                lblGoalName1.Text = Generic.ToStr(row("GPEStandardDesc"))
            Next
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkAddFiles_Click(sender As Object, e As EventArgs)

        Generic.ClearControls(Me, "pnlPopupFiles")
        Generic.EnableControls(Me, "pnlPopupFiles", True)
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupFiles", Generic.ToInt(Session("xPayLocNo")))
        PopulateFiles()
        mdlFiles.Show()

    End Sub

    Protected Sub lnkSaveFiles_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim dt As DataTable
        Dim tno As Integer = Generic.ToInt(Me.txtPEStandardRevFilesNo.Text)
        Dim PEStandardRevFilesDesc As String = Generic.ToStr(Me.txtPEStandardRevFilesDesc.Text)
        Dim Remarks As String = Generic.ToStr(Me.txtRemarks.Text)

        dt = SQLHelper.ExecuteDataTable("EPEStandardRevFiles_WebSave", UserNo, tno, Generic.ToInt(Session("TPEStandardRevNo")), PEStandardRevFilesDesc, Remarks)
        For Each row As DataRow In dt.Rows
            tno = Generic.ToInt(row("id"))
            Retval = True
        Next

        If fuDoc.HasFile And tno > 0 Then
            Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String
            Dim _ds As New DataSet, NewFileName As String
            Try
                Dim fpath As String
                Filename = IO.Path.GetFileName(fuDoc.PostedFile.FileName)
                FileExt = IO.Path.GetExtension(fuDoc.PostedFile.FileName)
                'Dim f As New System.IO.FileInfo(fuDoc.PostedFile.FileName)
                'FileSize = f.Length

                Dim fs As IO.Stream = fuDoc.PostedFile.InputStream
                Dim br As New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                FileSize = fs.Length

                Filename = Replace(Filename, FileExt, "")
                Filename = Replace(Filename, " ", "_")
                NewFileName = Guid.NewGuid().ToString()
                NewFileName = Filename & "_" & NewFileName & FileExt
                ActualPath = Server.MapPath("../") & "secured\documents\" & NewFileName
                fpath = "../secured/documents/" & NewFileName
                fuDoc.SaveAs(ActualPath)
                If SQLHelper.ExecuteNonQuery("EPEStandardRevFiles_WebSave_Doc", UserNo, tno, fpath, ActualPath, NewFileName) > 0 Then
                    Retval = True
                End If
            Catch ex As Exception

            End Try
        End If

        If Retval = True Then
            PopulateFiles()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub

    Protected Sub lnkEditFiles_Click(sender As Object, e As EventArgs)

        Dim ib As New LinkButton
        ib = sender
        Generic.ClearControls(Me, "pnlPopupFiles")
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupFiles", Generic.ToInt(Session("xPayLocNo")))
        PopulateDataDisabled(Me, "pnlPopupFiles", UserNo, "EPEStandardRevFiles")
        PopulateDataFiles(Generic.ToInt(ib.CommandArgument))
        PopulateFiles()
        mdlFiles.Show()

    End Sub

    Protected Sub PopulateDataFiles(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardRevFiles_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupFiles", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkDeleteFiles_Click(sender As Object, e As EventArgs)
        Dim ib As New LinkButton
        ib = sender
        Generic.DeleteRecordAudit("EPEStandardRevFiles", UserNo, Generic.ToInt(ib.CommandArgument))
        PopulateFiles()
        MessageBox.Success(MessageTemplate.SuccessDelete, Me)

    End Sub


    Protected Sub lnkDownload_Click(sender As Object, e As EventArgs)
        Try
            Dim doc As Byte() = Nothing
            Dim filename As String = ""
            Dim orgname As String = ""
            Dim dt As DataTable

            Dim ib As New LinkButton
            ib = sender
            dt = SQLHelper.ExecuteDataTable("EPEStandardRevFiles_WebOne", UserNo, Generic.ToInt(ib.CommandArgument))
            For Each row As DataRow In dt.Rows
                filename = Generic.ToStr(row("ActualFileName"))
            Next

            Dim path As String = Server.MapPath("~/secured/documents/") & filename
            Dim pathx As String = "../secured/documents/" & filename
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)

            Dim flStream As FileStream = New FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite)
            Dim fs As IO.Stream = flStream
            Dim br As New BinaryReader(fs)
            Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))

            If Not path Is Nothing Then
                Response.Clear()
                Response.Buffer = True
                Response.AddHeader("Content-Disposition", "attachment; filename=" & filename)
                Response.ContentType = "application/octet-stream"
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.BinaryWrite(bytes)
                Response.End()
            Else
                MessageBox.Warning("This file does not exist.", Me)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub addTrigger_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim btnPreview As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(btnPreview)
    End Sub

#End Region


#Region "******** Performance Information ********"

    Protected Sub PopulateGrid()
        Try

            Dim EmployeeNo As Integer = 0
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEReview_WebInfo", UserNo, pereviewno)
            For Each row As DataRow In dt.Rows
                lblName.Text = Generic.ToStr(row("FullName"))
                EmployeeNo = Generic.ToInt(row("EmployeeNo"))
                lblCode.Text = Generic.ToStr(row("PEReviewCode"))
            Next

            imgPhoto.ImageUrl = "~/secured/frmShowImage.ashx?tNo=" & Generic.ToInt(EmployeeNo) & "&tIndex=2"

            rRef.DataSource = SQLHelper.ExecuteDataTable("EPEReview_WebInfo", UserNo, pereviewno)
            rRef.DataBind()


        Catch ex As Exception

        End Try
    End Sub

#End Region


#Region "******** ComboBox ********"

    Private Sub PopulateCombo()

        Try
            cboPEReviewDimNo.DataSource = SQLHelper.ExecuteDataSet("EPEReviewDim_WebLookup", pereviewcateno)
            cboPEReviewDimNo.DataTextField = "tDesc"
            cboPEReviewDimNo.DataValueField = "tNo"
            cboPEReviewDimNo.DataBind()
        Catch ex As Exception

        End Try

        'ACI Goals
        Try
            cboGPEReviewDimNo.DataSource = SQLHelper.ExecuteDataSet("EPEReviewDim_WebLookup", pereviewcateno)
            cboGPEReviewDimNo.DataTextField = "tDesc"
            cboGPEReviewDimNo.DataValueField = "tNo"
            cboGPEReviewDimNo.DataBind()
        Catch ex As Exception

        End Try

        'Try
        '    cboPEEvaluatorNo.DataSource = SQLHelper.ExecuteDataTable("xTable_Lookup", UserNo, "EPEEvaluator", PayLocNo, "", "")
        '    cboPEEvaluatorNo.DataTextField = "tDesc"
        '    cboPEEvaluatorNo.DataValueField = "tNo"
        '    cboPEEvaluatorNo.DataBind()
        'Catch ex As Exception

        'End Try

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


#Region "******** Title ********"

    Private Sub PopulateHeader()

        Dim Title As String = "", SubTitle As String = "", PEPeriodDesc As String = "", Instruction As String = ""
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEReviewMain_WebFormHeader", UserNo, pereviewmainno)
        For Each row As DataRow In dt.Rows
            Title = Generic.ToStr(row("Title"))
            SubTitle = Generic.ToStr(row("SubTitle"))
            PEPeriodDesc = Generic.ToStr(row("PEPeriodDesc"))
            Instruction = Generic.ToStr(row("Instruction"))
        Next

        pHeader.Controls.Add(New LiteralControl("<div class='row'><br/>"))
        pHeader.Controls.Add(New LiteralControl("<h3 style='text-align:center;'>" & Title & "</h3>"))
        If SubTitle > "" Then
            pHeader.Controls.Add(New LiteralControl("<h4 style='text-align:center;font-weight:bold;'>" & SubTitle & "</h4>"))
        End If
        If PEPeriodDesc > "" Then
            pHeader.Controls.Add(New LiteralControl("<h4 style='text-align:center;font-weight:bold;'>" & PEPeriodDesc & "</h4>"))
        End If
        If Instruction > "" Then
            pHeader.Controls.Add(New LiteralControl(Instruction))
        End If
        pHeader.Controls.Add(New LiteralControl("</div>"))

    End Sub

#End Region


#Region "******** Category Header ********"

    Private Sub PopulateCate()


        Dim PEReviewCateCode As String = "", PEReviewCateDesc As String = "", RemarksCate As String = ""
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEReviewCate_WebOne", UserNo, pereviewcateno)
        For Each row As DataRow In dt.Rows
            PEReviewCateCode = Generic.ToStr(row("PEReviewCateCode"))
            PEReviewCateDesc = Generic.ToStr(row("PEReviewCateDesc"))
            RemarksCate = Generic.ToStr(row("RemarksCate"))
        Next

        If PEReviewCateCode > "" Then
            PEReviewCateCode = PEReviewCateCode & ". "
        End If


        'LinkButton
        Dim lnkCate As New LinkButton
        lnkCate.ID = "lnkCate" & pereviewcateno
        lnkCate.Text = "&nbsp; Edit &nbsp;"
        lnkCate.CssClass = "fa fa-pencil"
        lnkCate.Style.Add("font-family", "FontAwesome, Arial")
        lnkCate.Style.Add("text-decoration", "none")
        lnkCate.Style.Add("display", "inline")
        lnkCate.CommandArgument = pereviewcateno
        lnkCate.Visible = False
        AddHandler lnkCate.Click, AddressOf lnkCate_Click


        pCate.Controls.Add(New LiteralControl("<div class='row'>"))
        pCate.Controls.Add(New LiteralControl("<div class='bs-callout bs-callout-info'>"))
        If PEReviewCateCode > "" Or PEReviewCateDesc > "" Then
            pCate.Controls.Add(New LiteralControl("<h4 style='display:inline;font-weight:bold;'>" & PEReviewCateCode & PEReviewCateDesc & "</h4>&nbsp;&nbsp;&nbsp;"))
        End If
        pCate.Controls.Add(lnkCate)
        pCate.Controls.Add(New LiteralControl("<p>" & RemarksCate & "</p><br/>"))
        pCate.Controls.Add(New LiteralControl("</div></div>"))

    End Sub

#End Region

#Region "******** Competency Populate ********"
    'Competency Populate
    Private Sub PopulateComp(Optional TabId As Integer = 0)
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPEReviewComp_WebForm", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno)
            Dim dtDimension As DataTable
            Dim dtCluster As DataTable
            Dim dtQuestion As DataTable
            dtDimension = ds.Tables(0).DefaultView.ToTable(True, "CompTypeNo", "CompTypeCode", "CompTypeDesc")
            dtCluster = ds.Tables(0).DefaultView.ToTable(True, "CompTypeNo", "CompClusterNo", "CompClusterCode", "CompClusterDesc")
            dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PEReviewCompNo", "PEReviewNo", "CompTypeNo", "CompClusterNo", "CompNo", "CompCode", "CompDesc", "HasComment", "IsRequired", "ResponseTypeNo", "Comments", "OrderByDeti", "IsEnabled")


            For Each rowDimension As DataRow In dtDimension.Rows
                Dim CompTypeNo As Integer = Generic.ToInt(rowDimension("CompTypeNo"))
                Dim CompTypeCode As String = Generic.ToStr(rowDimension("CompTypeCode"))
                Dim CompTypeDesc As String = Generic.ToStr(rowDimension("CompTypeDesc"))

                pForm.Controls.Add(New LiteralControl("<div class='row'>")) '1
                pForm.Controls.Add(New LiteralControl("<div class='bs-callout bs-callout-info'>"))

                If CompTypeDesc > "" Then
                    pForm.Controls.Add(New LiteralControl("<br/><h4 style='display:inline;'>" & CompTypeDesc & "</h4>&nbsp;&nbsp;&nbsp;"))
                End If
                pForm.Controls.Add(New LiteralControl("</div>"))

                pForm.Controls.Add(New LiteralControl("<div class='panel-body' style='padding-top:0px;padding-bottom:0px;'>"))
                pForm.Controls.Add(New LiteralControl("<div class='panel-body' style='padding-top:0px;padding-bottom:0px;'>"))
                pForm.Controls.Add(New LiteralControl("<div class='media'>"))
                pForm.Controls.Add(New LiteralControl("<div class='media-body'>"))

                For Each rowCluster As DataRow In dtCluster.Select("CompTypeNo=" & rowDimension("CompTypeNo"))
                    Dim CompClusterDesc As String = Generic.ToStr(rowCluster("CompClusterDesc"))

                    If CompClusterDesc > "" Then
                        pForm.Controls.Add(New LiteralControl("<div class='bs-callout bs-callout-info'>"))
                        pForm.Controls.Add(New LiteralControl("<br/><br/><h5 style='display:inline;'>" & CompClusterDesc & "</h5>&nbsp;&nbsp;&nbsp;"))
                        pForm.Controls.Add(New LiteralControl("</div>"))
                    End If

                    pForm.Controls.Add(New LiteralControl("<div class='panel-body' style='padding-top:0px;padding-bottom:0px;'>"))
                    pForm.Controls.Add(New LiteralControl("<div class='panel-body' style='padding-top:0px;padding-bottom:0px;'>"))

                    For Each rowQuestion As DataRow In dtQuestion.Select("CompTypeNo=" & rowDimension("CompTypeNo") & " and CompClusterNo=" & rowCluster("CompClusterNo"))
                        
                        Dim ResponseTypeNo As Integer = Generic.ToInt(rowQuestion("ResponseTypeNo"))
                        Dim PEReviewCompNo As Integer = Generic.ToInt(rowQuestion("PEReviewCompNo"))
                        Dim IsEnabled As Boolean = Generic.ToBol(rowQuestion("IsEnabled"))

                        'Journal Button
                        Dim lnkJournal As New LinkButton
                        lnkJournal.ID = "lnkJournal" & PEReviewCompNo
                        lnkJournal.Text = "&nbsp;Journal &nbsp;"
                        lnkJournal.CssClass = "fa fa-comments-o"
                        lnkJournal.Style.Add("font-family", "FontAwesome, Arial")
                        lnkJournal.Style.Add("text-decoration", "none")
                        lnkJournal.CommandArgument = PEReviewCompNo
                        AddHandler lnkJournal.Click, AddressOf lnkJournal_Click

                        pForm.Controls.Add(New LiteralControl("<div class='row'>")) '1
                        pForm.Controls.Add(New LiteralControl("<div class='panel panel-default'>"))  '2

                        pForm.Controls.Add(New LiteralControl("<div class='panel-heading'>"))
                        pForm.Controls.Add(New LiteralControl("<h4 class='panel-title' style='font-size:small;'><strong>Item No. " & rowQuestion("OrderByDeti") & "</strong></h4>"))
                        pForm.Controls.Add(New LiteralControl("<ul class='panel-controls'>"))
                        pForm.Controls.Add(New LiteralControl("<li>"))
                        pForm.Controls.Add(lnkJournal)
                        pForm.Controls.Add(New LiteralControl("</li>"))
                        pForm.Controls.Add(New LiteralControl("</ul>"))
                        pForm.Controls.Add(New LiteralControl("</div>"))

                        pForm.Controls.Add(New LiteralControl("<div class='panel-body'>")) '3

                        pForm.Controls.Add(New LiteralControl("<div class='media'>"))
                        pForm.Controls.Add(New LiteralControl("<div class='media-left'>"))
                        pForm.Controls.Add(New LiteralControl(rowQuestion("CompCode")))
                        pForm.Controls.Add(New LiteralControl("</div>"))

                        pForm.Controls.Add(New LiteralControl("<div class='media-body' style='width:10000px;'>"))
                        pForm.Controls.Add(New LiteralControl("<dl><dt>" & rowQuestion("CompDesc") & "</dt></dl>"))
                        pForm.Controls.Add(New LiteralControl("</div>"))
                        pForm.Controls.Add(New LiteralControl("</div>"))

                        pForm.Controls.Add(New LiteralControl("<div class='col-md-12'>"))
                        pForm.Controls.Add(New LiteralControl("<div style='padding:0 0 0 30px;'>"))
                        


                        Select Case ResponseTypeNo
                            Case 1
                                'Radio Button
                                Dim rbl As New RadioButtonList
                                rbl.ID = "rbl" & PEReviewCompNo
                                rbl.RepeatLayout = RepeatLayout.Table
                                rbl.Enabled = IsEnabled
                                'rbl.RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal
                                For Each rowChoice As DataRow In ds.Tables(0).Select("PEReviewCompNo=" & PEReviewCompNo)
                                    Dim li As New ListItem
                                    li.Text = "&nbsp;&nbsp;&nbsp;" & rowChoice("Anchor").ToString()
                                    li.Value = rowChoice("PEReviewCompDetiNo").ToString()
                                    If Generic.ToInt(li.Value) > 0 Then
                                        rbl.Items.Add(li)
                                    End If
                                Next
                                If rowQuestion("IsRequired") And IsEnabled = True Then
                                    Dim rf As New RequiredFieldValidator
                                    rf.ID = "rf" & PEReviewCompNo
                                    rf.Display = ValidatorDisplay.Dynamic
                                    rf.ControlToValidate = rbl.ID
                                    rf.Text = "* This item is required."
                                    rf.ErrorMessage = "Item " & rowQuestion("CompDesc")
                                    rf.ValidationGroup = "EvalValidationGroup"
                                    rf.Style.Add("color", "red")
                                    pForm.Controls.Add(rf)
                                End If
                                pForm.Controls.Add(rbl)

                            Case 2
                                'Checkbox
                                Dim cbl As New CheckBoxList
                                cbl.ID = "cbl" & PEReviewCompNo
                                cbl.Enabled = IsEnabled
                                For Each rowChoice As DataRow In ds.Tables(0).Select("PEReviewCompNo=" & PEReviewCompNo)
                                    Dim li As New ListItem
                                    li.Text = "&nbsp;&nbsp;&nbsp;" & rowChoice("Anchor").ToString()
                                    li.Value = rowChoice("PEReviewCompDetiNo").ToString()
                                    If Generic.ToInt(li.Value) > 0 Then
                                        cbl.Items.Add(li)
                                    End If
                                Next
                                pForm.Controls.Add(cbl)

                            Case 3
                                'Narrative
                                Dim txt As New TextBox
                                txt.ID = "txt" & PEReviewCompNo
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
                                    rf.ID = "rf" & PEReviewCompNo
                                    rf.Display = ValidatorDisplay.Dynamic
                                    rf.ControlToValidate = txt.ID
                                    rf.Text = "* This item is required."
                                    rf.ValidationGroup = "EvalValidationGroup"
                                    pForm.Controls.Add(rf)
                                End If

                            Case 4
                                'Dropdown
                                Dim ddl As New DropDownList
                                ddl.ID = "ddl" & PEReviewCompNo
                                Dim l As New ListItem
                                l.Text = "-- Select --"
                                l.Value = "0"
                                l.Enabled = IsEnabled
                                ddl.CssClass = "form-control"
                                ddl.Items.Add(l)
                                For Each rowChoice As DataRow In ds.Tables(0).Select("PEReviewCompNo=" & PEReviewCompNo)
                                    Dim li As New ListItem
                                    li.Text = Generic.ToStr(rowChoice("Anchor"))
                                    li.Value = Generic.ToInt(rowChoice("PEReviewCompDetiNo"))
                                    If Generic.ToInt(li.Value) > 0 Then
                                        ddl.Items.Add(li)
                                    End If
                                Next
                                If rowQuestion("IsRequired") And IsEnabled = True Then
                                    Dim rf As New RequiredFieldValidator
                                    rf.ID = "rf" & PEReviewCompNo
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
                                txt.ID = "txt" & PEReviewCompNo
                                txt.CssClass = "form-control"
                                txt.Enabled = IsEnabled
                                pForm.Controls.Add(New LiteralControl("<div class='row col-no-padding'><div class='col-md-2'>"))
                                pForm.Controls.Add(New LiteralControl("Points :<br/>"))
                                pForm.Controls.Add(txt)
                                pForm.Controls.Add(New LiteralControl("</div></div>"))
                                If rowQuestion("IsRequired") And IsEnabled = True Then
                                    Dim rf As New RequiredFieldValidator
                                    rf.ID = "rf" & PEReviewCompNo.ToString()
                                    rf.Display = ValidatorDisplay.Dynamic
                                    rf.ControlToValidate = txt.ID
                                    rf.Text = "* This item is required."
                                    rf.ValidationGroup = "EvalValidationGroup"
                                    pForm.Controls.Add(rf)
                                End If
                                Dim cf As New CompareValidator
                                cf.ID = "cf" & PEReviewCompNo
                                cf.Display = ValidatorDisplay.Dynamic
                                cf.ControlToValidate = txt.ID
                                cf.Text = "*"
                                cf.Operator = ValidationCompareOperator.DataTypeCheck
                                cf.Type = ValidationDataType.Double
                                pForm.Controls.Add(cf)

                                Dim filtertxt As AjaxControlToolkit.FilteredTextBoxExtender
                                filtertxt = New AjaxControlToolkit.FilteredTextBoxExtender()
                                filtertxt.ID = "filtertxt" & PEReviewCompNo
                                filtertxt.FilterType = AjaxControlToolkit.FilterTypes.Custom
                                filtertxt.ValidChars = "1234567890.-"
                                filtertxt.TargetControlID = txt.ID
                                pForm.Controls.Add(filtertxt)
                        End Select


                        If rowQuestion("HasComment") Then
                            Dim txtComments As New TextBox
                            txtComments.ID = "txtComments" & rowQuestion("PEReviewCompNo").ToString()
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


                        pForm.Controls.Add(New LiteralControl("</div>"))
                        pForm.Controls.Add(New LiteralControl("</div>"))
                        pForm.Controls.Add(New LiteralControl("</div>"))
                        pForm.Controls.Add(New LiteralControl("</div>"))
                        pForm.Controls.Add(New LiteralControl("</div>"))
                    Next

                    pForm.Controls.Add(New LiteralControl("</div>"))
                    pForm.Controls.Add(New LiteralControl("</div>"))
                Next

                'pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("</div>"))

                pForm.Controls.Add(New LiteralControl("</div>"))
            Next

            If isposted = True Then
                Generic.EnableControls(Me, "pForm", False)
                lnkAdd.Visible = False
                lnkSave.Visible = False
            End If

        Catch ex As Exception

        End Try


    End Sub

    'Competency Answer
    Private Sub PopulateCompAnswer()
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPEReviewComp_WebFormAns", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno)
            Dim dtQuestion As DataTable
            dtQuestion = ds.Tables(0)
            For Each rowQuestion As DataRow In dtQuestion.Rows
                Dim txtRemarks As New TextBox
                Dim ResponseTypeNo As Integer = Generic.ToInt(rowQuestion("ResponseTypeNo"))
                Dim PEReviewCompNo As Integer = Generic.ToInt(rowQuestion("PEReviewCompNo"))
                Select Case ResponseTypeNo
                    Case 1
                        'Radio Button
                        Dim rbl As New RadioButtonList
                        rbl = pForm.FindControl("rbl" & PEReviewCompNo.ToString())
                        rbl.SelectedValue = Generic.ToStr(rowQuestion("AnswerNo"))
                    Case 2
                        'Checkbox
                        Dim cbl As New CheckBoxList
                        cbl = pForm.FindControl("cbl" & PEReviewCompNo.ToString())
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
                        txt = pForm.FindControl("txt" & PEReviewCompNo.ToString())
                        txt.Text = Generic.ToStr(rowQuestion("AnswerNo"))
                    Case 4
                        'Dropdown
                        Dim ddl As New DropDownList
                        ddl = pForm.FindControl("ddl" & PEReviewCompNo.ToString())
                        Try
                            ddl.SelectedValue = Generic.ToStr(rowQuestion("AnswerNo"))
                        Catch ex As Exception

                        End Try
                    Case 5
                        'Numeric
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & PEReviewCompNo.ToString())
                        txt.Text = Generic.ToStr(rowQuestion("AnswerNo"))
                End Select

                If rowQuestion("HasComment") Then
                    Dim txtComments As New TextBox
                    txtComments = pForm.FindControl("txtComments" & PEReviewCompNo)
                    txtComments.Text = Generic.ToStr(rowQuestion("Comments"))
                End If

            Next

        Catch ex As Exception

        End Try

    End Sub

    'Competency Save
    Private Function CompSaveRecord() As Boolean
        Dim i As Integer = 0
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPEReviewComp_WebForm", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno)
            Dim dtQuestion As DataTable
            dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PEReviewCompNo", "PEReviewNo", "CompTypeNo", "CompClusterNo", "CompNo", "CompCode", "CompDesc", "HasComment", "IsRequired", "ResponseTypeNo", "Comments", "OrderByDeti", "IsEnabled")
            For Each rowQuestion As DataRow In dtQuestion.Rows
                Dim txtRemarks As New TextBox
                Dim AnswerNo As String = "", AnswerDesc As String = "", Remarks As String = ""
                Dim ResponseTypeNo As Integer = Generic.ToInt(rowQuestion("ResponseTypeNo"))
                Dim PEReviewCompNo As Integer = Generic.ToInt(rowQuestion("PEReviewCompNo"))
                Dim PEReviewCompDetiNo As String = 0
                Select Case ResponseTypeNo
                    Case 1
                        'Radio button
                        Dim rbl As New RadioButtonList
                        rbl = pForm.FindControl("rbl" & PEReviewCompNo)
                        PEReviewCompDetiNo = Generic.ToStr(rbl.SelectedValue)
                        AnswerNo = Generic.ToStr(rbl.SelectedValue)
                        If PEReviewCompDetiNo > "" Then
                            AnswerDesc = rbl.SelectedItem.Text
                        End If
                    Case 2
                        'Checkbox
                        Dim cbl As New CheckBoxList
                        cbl = pForm.FindControl("cbl" & PEReviewCompNo)
                        'EvalTemplateDetlChoiceNo = 0
                        AnswerNo = CheckBoxValue(cbl, 1)
                        AnswerDesc = CheckBoxValue(cbl, 0)
                    Case 3
                        'Narrative
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & PEReviewCompNo)
                        'EvalTemplateDetlChoiceNo = 0
                        AnswerNo = txt.Text
                        AnswerDesc = txt.Text
                    Case 4
                        'Dropdown
                        Dim ddl As New DropDownList
                        ddl = pForm.FindControl("ddl" & PEReviewCompNo)
                        PEReviewCompDetiNo = Generic.ToStr(ddl.SelectedValue)
                        AnswerNo = Generic.ToStr(ddl.SelectedValue)
                        If PEReviewCompDetiNo > "" Then
                            AnswerDesc = ddl.SelectedItem.Text
                        End If
                    Case 5
                        'Numeric
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & PEReviewCompNo)
                        'EvalTemplateDetlChoiceNo = 0
                        AnswerNo = txt.Text
                        AnswerDesc = txt.Text
                End Select

                If rowQuestion("HasComment") Then
                    Dim txtComments As New TextBox
                    txtComments = pForm.FindControl("txtComments" & PEReviewCompNo)
                    Remarks = txtComments.Text
                End If

                If ResponseTypeNo <> 0 Then
                    If SQLHelper.ExecuteNonQuery("EPEReviewCompAns_WebSave", UserNo, 0, pereviewno, peevaluatorno, PEReviewCompNo, PEReviewCompDetiNo, ResponseTypeNo, AnswerNo, AnswerDesc, Remarks) > 0 Then
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
#End Region

#Region "******** KRA Populate ********"

    'KRA Populate
    Private Sub PopulateQuestion(Optional TabId As Integer = 0)
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPEStandardRev_WebForm", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno)
            Dim dtDimension As DataTable
            Dim dtQuestion As DataTable
            dtDimension = ds.Tables(0).DefaultView.ToTable(True, "PEReviewDimNo", "Dimension", "PEReviewDimCode", "PEReviewDimDesc", "OrderLevelDim", "RemarksDim")
            dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PEStandardRevNo", "PEReviewDimNo", "PEStandardCode", "PEStandardDesc", "Standard", "HasComment", "IsRequired", "ResponseTypeNo", "Comments", "OrderLevelItem", "IsEnabled")

            'Permission Controls
            If isposted = False Then
                Dim dtCate As DataTable
                dtCate = SQLHelper.ExecuteDataTable("EPEStandardRev_WebControls", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno, 0)
                For Each row As DataRow In dtCate.Rows
                    IsAddCate = Generic.ToBol(row("IsAddCate"))
                Next
            End If
            lnkAdd.Visible = IsAddCate

            For Each rowDimension As DataRow In dtDimension.Rows
                Dim PEReviewDimNo As Integer = Generic.ToInt(rowDimension("PEReviewDimNo"))
                Dim PEReviewDimCode As String = Generic.ToStr(rowDimension("PEReviewDimCode"))
                Dim PEReviewDimDesc As String = Generic.ToStr(rowDimension("PEReviewDimDesc"))
                Dim RemarksDim As String = Generic.ToStr(rowDimension("RemarksDim"))

                'Permission Controls
                If isposted = False Then
                    Dim dtDim As DataTable
                    dtDim = SQLHelper.ExecuteDataTable("EPEStandardRev_WebControls", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno, PEReviewDimNo)
                    For Each row As DataRow In dtDim.Rows
                        IsAddDim = Generic.ToBol(row("IsAddDim"))
                        IsEditDim = Generic.ToBol(row("IsEditDim"))
                        IsDeleteDim = Generic.ToBol(row("IsDeleteDim"))
                        IsAddDeti = Generic.ToBol(row("IsAddDimDeti"))
                        IsEditDeti = Generic.ToBol(row("IsEditDimDeti"))
                        IsDeleteDeti = Generic.ToBol(row("IsDeleteDimDeti"))
                    Next
                End If
                lnkSaveChoices.Visible = IsEditDeti
                lnkDeleteChoices.Visible = IsDeleteDeti

                'Dimension
                Dim lnkDim As New LinkButton
                lnkDim.ID = "lnkDim" & PEReviewDimNo
                lnkDim.Text = "&nbsp; Add Item &nbsp;"
                lnkDim.CssClass = "fa fa-plus"
                lnkDim.Style.Add("font-family", "FontAwesome, Arial")
                lnkDim.Style.Add("text-decoration", "none")
                lnkDim.Style.Add("display", "inline")
                lnkDim.CommandArgument = PEReviewDimNo
                lnkDim.Visible = False
                AddHandler lnkDim.Click, AddressOf lnkDim_Click


                'New Item
                Dim lnkNewItem As New LinkButton
                lnkNewItem.ID = "lnkNewItem" & PEReviewDimNo
                lnkNewItem.Text = "&nbsp; Add Item &nbsp;"
                lnkNewItem.CssClass = "fa fa-plus"
                lnkNewItem.Style.Add("font-family", "FontAwesome, Arial")
                lnkNewItem.Style.Add("text-decoration", "none")
                lnkNewItem.Style.Add("display", "inline")
                lnkNewItem.CommandArgument = PEReviewDimNo
                lnkNewItem.Visible = IsAddDim
                AddHandler lnkNewItem.Click, AddressOf lnkNewItem_Click

                pForm.Controls.Add(New LiteralControl("<div class='row'>"))
                pForm.Controls.Add(New LiteralControl("<div class='bs-callout bs-callout-info'>"))
                If PEReviewDimCode > "" Then
                    PEReviewDimCode = PEReviewDimCode & ". "
                End If
                If PEReviewDimCode > "" Or PEReviewDimDesc > "" Then
                    pForm.Controls.Add(New LiteralControl("<br/><h4 style='display:inline;'>" & PEReviewDimCode & PEReviewDimDesc & "</h4>&nbsp;&nbsp;&nbsp;"))
                End If
                pForm.Controls.Add(lnkDim)
                pForm.Controls.Add(lnkNewItem)
                pForm.Controls.Add(New LiteralControl("<p>" & RemarksDim & "</p>"))
                pForm.Controls.Add(New LiteralControl("</div>"))

                pForm.Controls.Add(New LiteralControl("<div class='panel-body' style='padding-top:0px;padding-bottom:0px;'>"))
                pForm.Controls.Add(New LiteralControl("<div class='panel-body' style='padding-top:0px;padding-bottom:0px;'>"))
                pForm.Controls.Add(New LiteralControl("<div class='media'>"))
                pForm.Controls.Add(New LiteralControl("<div class='media-body'>"))
                For Each rowQuestion As DataRow In dtQuestion.Select("PEReviewDimNo=" & rowDimension("PEReviewDimNo"))
                    Dim ResponseTypeNo As Integer = Generic.ToInt(rowQuestion("ResponseTypeNo"))
                    Dim PEStandardRevNo As Integer = Generic.ToInt(rowQuestion("PEStandardRevNo"))
                    Dim IsEnabled As Boolean = Generic.ToBol(rowQuestion("IsEnabled"))

                    'Move Up Button
                    Dim lnkMoveUp As New LinkButton
                    lnkMoveUp.ID = "lnkMoveUp" & PEStandardRevNo
                    'lnkMoveUp.Text = "Move Up"
                    lnkMoveUp.ToolTip = "Move Up"
                    lnkMoveUp.CssClass = "fa fa-arrow-up"
                    lnkMoveUp.Style.Add("font-family", "FontAwesome, Arial")
                    lnkMoveUp.Style.Add("text-decoration", "none")
                    lnkMoveUp.CommandArgument = PEStandardRevNo
                    lnkMoveUp.Visible = IsAddDim
                    AddHandler lnkMoveUp.Click, AddressOf lnkMoveUp_Click


                    'Move Down Button
                    Dim lnkMoveDown As New LinkButton
                    lnkMoveDown.ID = "lnkMoveDown" & PEStandardRevNo
                    'lnkMoveDown.Text = "Move Down"
                    lnkMoveDown.ToolTip = "Move Down"
                    lnkMoveDown.CssClass = "fa fa-arrow-down"
                    lnkMoveDown.Style.Add("font-family", "FontAwesome, Arial")
                    lnkMoveDown.Style.Add("text-decoration", "none")
                    lnkMoveDown.CommandArgument = PEStandardRevNo
                    lnkMoveDown.Visible = IsAddDim
                    AddHandler lnkMoveDown.Click, AddressOf lnkMoveDown_Click


                    'Delete Button
                    Dim lnkDelete As New LinkButton
                    lnkDelete.ID = "lnkDelete" & PEStandardRevNo
                    lnkDelete.Text = "Delete Item"
                    'lnkDelete.CssClass = "fa fa-times"
                    lnkDelete.Style.Add("font-family", "FontAwesome, Arial")
                    lnkDelete.Style.Add("text-decoration", "none")
                    lnkDelete.CommandArgument = PEStandardRevNo
                    lnkDelete.Visible = IsDeleteDim
                    AddHandler lnkDelete.Click, AddressOf lnkDelete_Click

                    'Delete Confirmation
                    Dim ConfirmButtonExtender As AjaxControlToolkit.ConfirmButtonExtender
                    ConfirmButtonExtender = New AjaxControlToolkit.ConfirmButtonExtender()
                    ConfirmButtonExtender.ID = "cbe" & PEStandardRevNo.ToString
                    ConfirmButtonExtender.DisplayModalPopupID = "mpe" & PEStandardRevNo
                    ConfirmButtonExtender.TargetControlID = "lnkDelete" & PEStandardRevNo

                    Dim ModalPopupExtender As AjaxControlToolkit.ModalPopupExtender
                    ModalPopupExtender = New AjaxControlToolkit.ModalPopupExtender()
                    ModalPopupExtender.ID = "mpe" & PEStandardRevNo.ToString
                    ModalPopupExtender.TargetControlID = "lnkDelete" & PEStandardRevNo
                    ModalPopupExtender.PopupControlID = "pConfirmBox"
                    ModalPopupExtender.OkControlID = "btnYes"
                    ModalPopupExtender.CancelControlID = "btnNo"

                    'Edit Button
                    Dim lnkEdit As New LinkButton
                    lnkEdit.ID = "lnkEdit" & PEStandardRevNo
                    lnkEdit.Text = "Edit Question"
                    'lnkEdit.CssClass = "fa fa-pencil"
                    lnkEdit.Style.Add("font-family", "FontAwesome, Arial")
                    lnkEdit.Style.Add("text-decoration", "none")
                    lnkEdit.CommandArgument = PEStandardRevNo
                    lnkEdit.Visible = IsEditDim
                    AddHandler lnkEdit.Click, AddressOf lnkEdit_Click

                    'Edit Detail Button
                    Dim lnkEditDeti As New LinkButton
                    lnkEditDeti.ID = "lnkEditDeti" & PEStandardRevNo
                    lnkEditDeti.Text = "Edit Indicator"
                    'lnkEditDeti.CssClass = "fa fa-pencil"
                    lnkEditDeti.Style.Add("font-family", "FontAwesome, Arial")
                    lnkEditDeti.Style.Add("text-decoration", "none")
                    lnkEditDeti.CommandArgument = PEStandardRevNo & "|0|" & Generic.ToStr(rowQuestion("OrderLevelItem"))
                    lnkEditDeti.Visible = IsEditDeti
                    AddHandler lnkEditDeti.Click, AddressOf lnkEditDeti_Click

                    'Journal Button
                    Dim lnkJournal As New LinkButton
                    lnkJournal.ID = "lnkJournal" & PEStandardRevNo
                    lnkJournal.Text = "&nbsp;Journal &nbsp;"
                    lnkJournal.CssClass = "fa fa-comments-o"
                    lnkJournal.Style.Add("font-family", "FontAwesome, Arial")
                    lnkJournal.Style.Add("text-decoration", "none")
                    lnkJournal.CommandArgument = PEStandardRevNo
                    AddHandler lnkJournal.Click, AddressOf lnkJournal_Click

                    pForm.Controls.Add(New LiteralControl("<div class='row'>"))
                    pForm.Controls.Add(New LiteralControl("<div class='panel panel-default'>"))

                    pForm.Controls.Add(New LiteralControl("<div class='panel-heading'>"))
                    pForm.Controls.Add(New LiteralControl("<h4 class='panel-title' style='font-size:small;'><strong>Item No. " & rowQuestion("OrderLevelItem") & "</strong></h4>"))
                    pForm.Controls.Add(New LiteralControl("<ul class='panel-controls'>"))
                    pForm.Controls.Add(New LiteralControl("<li>"))
                    pForm.Controls.Add(lnkJournal)
                    pForm.Controls.Add(New LiteralControl("</li>"))

                    If IsDeleteDim = True Or IsEditDim = True Or IsEditDeti = True Then
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
                    End If

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
                            rbl.ID = "rbl" & PEStandardRevNo
                            rbl.RepeatLayout = RepeatLayout.Table
                            rbl.Enabled = IsEnabled
                            'rbl.RepeatDirection = System.Web.UI.WebControls.RepeatDirection.Horizontal
                            For Each rowChoice As DataRow In ds.Tables(0).Select("PEStandardRevNo=" & PEStandardRevNo)
                                Dim li As New ListItem
                                li.Text = "&nbsp;&nbsp;&nbsp;" & rowChoice("Anchor").ToString()
                                li.Value = rowChoice("PEStandardDetiRevNo").ToString()
                                If Generic.ToInt(li.Value) > 0 Then
                                    rbl.Items.Add(li)
                                End If
                            Next
                            If rowQuestion("IsRequired") And IsEnabled = True Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardRevNo
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
                            cbl.ID = "cbl" & PEStandardRevNo
                            cbl.Enabled = IsEnabled
                            For Each rowChoice As DataRow In ds.Tables(0).Select("PEStandardRevNo=" & PEStandardRevNo)
                                Dim li As New ListItem
                                li.Text = "&nbsp;&nbsp;&nbsp;" & rowChoice("Anchor").ToString()
                                li.Value = rowChoice("PEStandardDetiRevNo").ToString()
                                If Generic.ToInt(li.Value) > 0 Then
                                    cbl.Items.Add(li)
                                End If
                            Next
                            pForm.Controls.Add(cbl)

                        Case 3
                            'Narrative
                            Dim txt As New TextBox
                            txt.ID = "txt" & PEStandardRevNo
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
                                rf.ID = "rf" & PEStandardRevNo
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = txt.ID
                                rf.Text = "* This item is required."
                                rf.ValidationGroup = "EvalValidationGroup"
                                pForm.Controls.Add(rf)
                            End If

                        Case 4
                            'Dropdown
                            Dim ddl As New DropDownList
                            ddl.ID = "ddl" & PEStandardRevNo
                            Dim l As New ListItem
                            l.Text = "-- Select --"
                            l.Value = "0"
                            l.Enabled = IsEnabled
                            ddl.CssClass = "form-control"
                            ddl.Items.Add(l)
                            For Each rowChoice As DataRow In ds.Tables(0).Select("PEStandardRevNo=" & PEStandardRevNo)
                                Dim li As New ListItem
                                li.Text = Generic.ToStr(rowChoice("Anchor"))
                                li.Value = Generic.ToInt(rowChoice("PEStandardDetiRevNo"))
                                If Generic.ToInt(li.Value) > 0 Then
                                    ddl.Items.Add(li)
                                End If
                            Next
                            If rowQuestion("IsRequired") And IsEnabled = True Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardRevNo
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
                            txt.ID = "txt" & PEStandardRevNo
                            txt.CssClass = "form-control"
                            txt.Enabled = IsEnabled
                            pForm.Controls.Add(New LiteralControl("<div class='row col-no-padding'><div class='col-md-2'>"))
                            pForm.Controls.Add(New LiteralControl("Points :<br/>"))
                            pForm.Controls.Add(txt)
                            pForm.Controls.Add(New LiteralControl("</div></div>"))
                            If rowQuestion("IsRequired") And IsEnabled = True Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardRevNo.ToString()
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = txt.ID
                                rf.Text = "* This item is required."
                                rf.ValidationGroup = "EvalValidationGroup"
                                pForm.Controls.Add(rf)
                            End If
                            Dim cf As New CompareValidator
                            cf.ID = "cf" & PEStandardRevNo
                            cf.Display = ValidatorDisplay.Dynamic
                            cf.ControlToValidate = txt.ID
                            cf.Text = "*"
                            cf.Operator = ValidationCompareOperator.DataTypeCheck
                            cf.Type = ValidationDataType.Double
                            pForm.Controls.Add(cf)

                            Dim filtertxt As AjaxControlToolkit.FilteredTextBoxExtender
                            filtertxt = New AjaxControlToolkit.FilteredTextBoxExtender()
                            filtertxt.ID = "filtertxt" & PEStandardRevNo
                            filtertxt.FilterType = AjaxControlToolkit.FilterTypes.Custom
                            filtertxt.ValidChars = "1234567890.-"
                            filtertxt.TargetControlID = txt.ID
                            pForm.Controls.Add(filtertxt)
                    End Select


                    If rowQuestion("HasComment") Then
                        Dim txtComments As New TextBox
                        txtComments.ID = "txtComments" & rowQuestion("PEStandardRevNo").ToString()
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

                    If IsAddDim = True Then
                        pForm.Controls.Add(New LiteralControl("<ul class='panel-controls'>"))
                        pForm.Controls.Add(New LiteralControl("<li>"))
                        pForm.Controls.Add(lnkMoveUp)
                        pForm.Controls.Add(New LiteralControl("</li>"))
                        pForm.Controls.Add(New LiteralControl("<li>"))
                        pForm.Controls.Add(lnkMoveDown)
                        pForm.Controls.Add(New LiteralControl("</li>"))
                        pForm.Controls.Add(New LiteralControl("</ul>"))
                    End If

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

            If isposted = True Then
                Generic.EnableControls(Me, "pForm", False)
                lnkAdd.Visible = False
                lnkSave.Visible = False
            End If

        Catch ex As Exception

        End Try


    End Sub

    'KRA Answer
    Private Sub PopulateAnswer()
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPEStandardRev_WebFormAns", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno)
            Dim dtQuestion As DataTable
            dtQuestion = ds.Tables(0)
            For Each rowQuestion As DataRow In dtQuestion.Rows
                Dim txtRemarks As New TextBox
                Dim ResponseTypeNo As Integer = Generic.ToInt(rowQuestion("ResponseTypeNo"))
                Dim PEStandardRevNo As Integer = Generic.ToInt(rowQuestion("PEStandardRevNo"))
                Select Case ResponseTypeNo
                    Case 1
                        'Radio Button
                        Dim rbl As New RadioButtonList
                        rbl = pForm.FindControl("rbl" & PEStandardRevNo.ToString())
                        rbl.SelectedValue = Generic.ToStr(rowQuestion("AnswerNo"))
                    Case 2
                        'Checkbox
                        Dim cbl As New CheckBoxList
                        cbl = pForm.FindControl("cbl" & PEStandardRevNo.ToString())
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
                        txt = pForm.FindControl("txt" & PEStandardRevNo.ToString())
                        txt.Text = Generic.ToStr(rowQuestion("AnswerNo"))
                    Case 4
                        'Dropdown
                        Dim ddl As New DropDownList
                        ddl = pForm.FindControl("ddl" & PEStandardRevNo.ToString())
                        Try
                            ddl.SelectedValue = Generic.ToStr(rowQuestion("AnswerNo"))
                        Catch ex As Exception

                        End Try
                    Case 5
                        'Numeric
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & PEStandardRevNo.ToString())
                        txt.Text = Generic.ToStr(rowQuestion("AnswerNo"))
                End Select

                If rowQuestion("HasComment") Then
                    Dim txtComments As New TextBox
                    txtComments = pForm.FindControl("txtComments" & PEStandardRevNo)
                    txtComments.Text = Generic.ToStr(rowQuestion("Comments"))
                End If

            Next

        Catch ex As Exception

        End Try

    End Sub

    'KRA Save
    Private Function KRASaveRecord() As Boolean
        Dim i As Integer = 0
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPEStandardRev_WebForm", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno)
            Dim dtQuestion As DataTable
            dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PEStandardRevNo", "PEReviewDimNo", "PEStandardCode", "Standard", "HasComment", "IsRequired", "ResponseTypeNo")
            For Each rowQuestion As DataRow In dtQuestion.Rows
                Dim txtRemarks As New TextBox
                Dim AnswerNo As String = "", AnswerDesc As String = "", Remarks As String = ""
                Dim ResponseTypeNo As Integer = Generic.ToInt(rowQuestion("ResponseTypeNo"))
                Dim PEStandardRevNo As Integer = Generic.ToInt(rowQuestion("PEStandardRevNo"))
                Dim PEStandardDetiRevNo As String = 0
                Select Case ResponseTypeNo
                    Case 1
                        'Radio button
                        Dim rbl As New RadioButtonList
                        rbl = pForm.FindControl("rbl" & PEStandardRevNo)
                        PEStandardDetiRevNo = Generic.ToStr(rbl.SelectedValue)
                        AnswerNo = Generic.ToStr(rbl.SelectedValue)
                        If PEStandardDetiRevNo > "" Then
                            AnswerDesc = rbl.SelectedItem.Text
                        End If
                    Case 2
                        'Checkbox
                        Dim cbl As New CheckBoxList
                        cbl = pForm.FindControl("cbl" & PEStandardRevNo)
                        'EvalTemplateDetlChoiceNo = 0
                        AnswerNo = CheckBoxValue(cbl, 1)
                        AnswerDesc = CheckBoxValue(cbl, 0)
                    Case 3
                        'Narrative
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & PEStandardRevNo)
                        'EvalTemplateDetlChoiceNo = 0
                        AnswerNo = txt.Text
                        AnswerDesc = txt.Text
                    Case 4
                        'Dropdown
                        Dim ddl As New DropDownList
                        ddl = pForm.FindControl("ddl" & PEStandardRevNo)
                        PEStandardDetiRevNo = Generic.ToStr(ddl.SelectedValue)
                        AnswerNo = Generic.ToStr(ddl.SelectedValue)
                        If PEStandardDetiRevNo > "" Then
                            AnswerDesc = ddl.SelectedItem.Text
                        End If
                    Case 5
                        'Numeric
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & PEStandardRevNo)
                        'EvalTemplateDetlChoiceNo = 0
                        AnswerNo = txt.Text
                        AnswerDesc = txt.Text
                End Select

                If rowQuestion("HasComment") Then
                    Dim txtComments As New TextBox
                    txtComments = pForm.FindControl("txtComments" & PEStandardRevNo)
                    Remarks = txtComments.Text
                End If

                If ResponseTypeNo <> 0 Then
                    If SQLHelper.ExecuteNonQuery("EPEReviewDetiRev_WebSave", UserNo, 0, pereviewno, peevaluatorno, PEStandardRevNo, PEStandardDetiRevNo, ResponseTypeNo, AnswerNo, AnswerDesc, Remarks) > 0 Then
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

#End Region


#Region "******** MBO Populate ********"


    'MBO Populate
    Private Sub PopulateGoals(Optional TabId As Integer = 0)
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPEStandardObjRev_WebForm", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno)
            Dim dtQuestion As DataTable
            Dim dtMBO As DataTable
            dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PEStandardRevNo", "PEStandardCode", "PEStandardDesc", "Standard", "OrderLevelItem")
            dtMBO = ds.Tables(0).DefaultView.ToTable(True, "PEStandardObjRevNo", "PEStandardRevNo", "Description", "ResponseTypeNo", "IsRequired", "IsEnabled")

            If isposted = False Then
                'Permission Controls
                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPEStandardRev_WebControls", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno, 0)
                For Each row As DataRow In dt.Rows
                    IsAddCate = Generic.ToBol(row("IsAddCate"))
                    IsEditCate = Generic.ToBol(row("IsEditCate"))
                    IsDeleteCate = Generic.ToBol(row("IsDeleteCate"))
                    IsAddDeti = Generic.ToBol(row("IsAddCateDeti"))
                    IsEditDeti = Generic.ToBol(row("IsEditCateDeti"))
                    IsDeleteDeti = Generic.ToBol(row("IsDeleteCateDeti"))
                Next
            End If

            lnkAdd.Visible = IsAddCate
            lnkSaveChoices.Visible = IsEditDeti
            lnkDeleteChoices.Visible = IsDeleteDeti

            pForm.Controls.Add(New LiteralControl("<div class='row'>"))
            pForm.Controls.Add(New LiteralControl("<div class='panel-body'>"))
            pForm.Controls.Add(New LiteralControl("<div class='panel-body'>"))

            For Each rowQuestion As DataRow In dtQuestion.Rows
                Dim PEStandardRevNo As Integer = Generic.ToInt(rowQuestion("PEStandardRevNo"))


                'Move Up Button
                Dim lnkMoveUp As New LinkButton
                lnkMoveUp.ID = "lnkMoveUp" & PEStandardRevNo
                'lnkMoveUp.Text = "Move Up"
                lnkMoveUp.ToolTip = "Move Up"
                lnkMoveUp.CssClass = "fa fa-arrow-up"
                lnkMoveUp.Style.Add("font-family", "FontAwesome, Arial")
                lnkMoveUp.Style.Add("text-decoration", "none")
                lnkMoveUp.CommandArgument = PEStandardRevNo
                lnkMoveUp.Visible = IsAddCate
                AddHandler lnkMoveUp.Click, AddressOf lnkMoveUp_Click


                'Move Down Button
                Dim lnkMoveDown As New LinkButton
                lnkMoveDown.ID = "lnkMoveDown" & PEStandardRevNo
                'lnkMoveDown.Text = "Move Down"
                lnkMoveDown.ToolTip = "Move Down"
                lnkMoveDown.CssClass = "fa fa-arrow-down"
                lnkMoveDown.Style.Add("font-family", "FontAwesome, Arial")
                lnkMoveDown.Style.Add("text-decoration", "none")
                lnkMoveDown.CommandArgument = PEStandardRevNo
                lnkMoveDown.Visible = IsAddCate
                AddHandler lnkMoveDown.Click, AddressOf lnkMoveDown_Click


                'LinkButton
                Dim lnkJournal As New LinkButton
                lnkJournal.ID = "lnkJournal" & PEStandardRevNo
                lnkJournal.Text = "&nbsp;Journal &nbsp;"
                lnkJournal.CssClass = "fa fa-comments-o"
                lnkJournal.Style.Add("font-family", "FontAwesome, Arial")
                lnkJournal.Style.Add("text-decoration", "none")
                lnkJournal.CommandArgument = PEStandardRevNo
                AddHandler lnkJournal.Click, AddressOf lnkJournal_Click

                'Delete Button
                Dim lnkDelete As New LinkButton
                lnkDelete.ID = "lnkDelete" & PEStandardRevNo
                lnkDelete.Text = "Delete Item"
                'lnkDelete.CssClass = "fa fa-times"
                lnkDelete.Style.Add("font-family", "FontAwesome, Arial")
                lnkDelete.Style.Add("text-decoration", "none")
                lnkDelete.CommandArgument = PEStandardRevNo
                lnkDelete.Visible = IsDeleteCate
                AddHandler lnkDelete.Click, AddressOf lnkDelete_Click

                'Delete Confirmation
                Dim ConfirmButtonExtender As AjaxControlToolkit.ConfirmButtonExtender
                ConfirmButtonExtender = New AjaxControlToolkit.ConfirmButtonExtender()
                ConfirmButtonExtender.ID = "cbe" & PEStandardRevNo.ToString
                ConfirmButtonExtender.DisplayModalPopupID = "mpe" & PEStandardRevNo
                ConfirmButtonExtender.TargetControlID = "lnkDelete" & PEStandardRevNo


                Dim ModalPopupExtender As AjaxControlToolkit.ModalPopupExtender
                ModalPopupExtender = New AjaxControlToolkit.ModalPopupExtender()
                ModalPopupExtender.ID = "mpe" & PEStandardRevNo.ToString
                ModalPopupExtender.TargetControlID = "lnkDelete" & PEStandardRevNo
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

                If IsDeleteCate = True Or IsEditDeti = True Then
                    pForm.Controls.Add(New LiteralControl("<li>&nbsp;&nbsp;<div class='btn-group'><button type='button' class='btn btn-default btn-sm dropdown-toggle' data-toggle='dropdown'>&nbsp;<span class='caret'></span>&nbsp;</button>"))
                    pForm.Controls.Add(New LiteralControl("<ul class='dropdown-menu dropdown-menu-right' role='menu'>"))

                    For Each rowObj As DataRow In dtMBO.Select("PEStandardRevNo=" & PEStandardRevNo)
                        Dim PEStandardObjRevNo As Integer = Generic.ToInt(rowObj("PEStandardObjRevNo"))
                        Dim ResponseTypeNo As Integer = Generic.ToInt(rowObj("ResponseTypeNo"))
                        Select Case ResponseTypeNo
                            Case 1, 2, 4
                                'Radio Button, Checkbox, Dropdown

                                'Edit Detail Button
                                Dim lnkEditDeti As New LinkButton
                                lnkEditDeti.ID = "lnkEditDeti" & PEStandardObjRevNo
                                lnkEditDeti.Text = "Edit " & Generic.ToStr(rowObj("Description"))
                                'lnkEditDeti.CssClass = "fa fa-pencil"
                                lnkEditDeti.Style.Add("font-family", "FontAwesome, Arial")
                                lnkEditDeti.Style.Add("text-decoration", "none")
                                lnkEditDeti.CommandArgument = PEStandardRevNo & "|" & PEStandardObjRevNo & "|" & Generic.ToStr(rowQuestion("OrderLevelItem")) & " - " & Generic.ToStr(rowObj("Description"))
                                lnkEditDeti.Visible = IsEditDeti
                                AddHandler lnkEditDeti.Click, AddressOf lnkEditDeti_Click

                                pForm.Controls.Add(New LiteralControl("<li>"))
                                pForm.Controls.Add(lnkEditDeti)
                                pForm.Controls.Add(New LiteralControl("</li>"))
                        End Select
                    Next

                    pForm.Controls.Add(New LiteralControl("<li>"))
                    pForm.Controls.Add(lnkDelete)
                    pForm.Controls.Add(ConfirmButtonExtender)
                    pForm.Controls.Add(ModalPopupExtender)
                    pForm.Controls.Add(New LiteralControl("</li>"))
                    pForm.Controls.Add(New LiteralControl("</ul></div></li>"))
                End If

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

                For Each rowObj As DataRow In dtMBO.Select("PEStandardRevNo=" & PEStandardRevNo)
                    Dim PEStandardObjRevNo As Integer = Generic.ToInt(rowObj("PEStandardObjRevNo"))
                    Dim ResponseTypeNo As Integer = Generic.ToInt(rowObj("ResponseTypeNo"))
                    Dim IsEnabled As Boolean = Generic.ToBol(rowObj("IsEnabled"))

                    pForm.Controls.Add(New LiteralControl("<div class='form-group'>"))
                    pForm.Controls.Add(New LiteralControl("<label class='col-md-3 control-label has-space'>" & Generic.ToStr(rowObj("Description")) & "</label>"))
                    Select Case ResponseTypeNo
                        Case 1
                            'Radio Button
                            Dim rbl As New RadioButtonList
                            rbl.ID = "rbl" & PEStandardObjRevNo
                            rbl.RepeatLayout = RepeatLayout.Table
                            rbl.Enabled = IsEnabled
                            For Each rowChoice As DataRow In ds.Tables(0).Select("PEStandardObjRevNo=" & PEStandardObjRevNo)
                                Dim li As New ListItem
                                li.Text = "&nbsp;&nbsp;&nbsp;" & rowChoice("Anchor").ToString()
                                li.Value = rowChoice("PEStandardDetiRevNo").ToString()
                                If Generic.ToInt(li.Value) > 0 Then
                                    rbl.Items.Add(li)
                                End If
                            Next
                            If rowObj("IsRequired") And IsEnabled = True Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardObjRevNo
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
                            cbl.ID = "cbl" & PEStandardObjRevNo
                            cbl.Enabled = IsEnabled
                            For Each rowChoice As DataRow In ds.Tables(0).Select("PEStandardObjRevNo=" & PEStandardObjRevNo)
                                Dim li As New ListItem
                                li.Text = "&nbsp;&nbsp;&nbsp;" & rowChoice("Anchor").ToString()
                                li.Value = rowChoice("PEStandardDetiRevNo").ToString()
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
                            txt.ID = "txt" & PEStandardObjRevNo
                            txt.TextMode = TextBoxMode.MultiLine
                            txt.CssClass = "form-control"
                            txt.Rows = 2
                            txt.Enabled = IsEnabled
                            pForm.Controls.Add(New LiteralControl("<div class='col-md-7'>"))
                            pForm.Controls.Add(txt)
                            pForm.Controls.Add(New LiteralControl("</div>"))
                            If rowObj("IsRequired") And IsEnabled = True Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardObjRevNo
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = txt.ID
                                rf.Text = "* This field is required."
                                rf.ValidationGroup = "EvalValidationGroup"
                                pForm.Controls.Add(rf)
                            End If

                        Case 4
                            'Dropdown
                            Dim ddl As New DropDownList
                            ddl.ID = "ddl" & PEStandardObjRevNo
                            Dim l As New ListItem
                            l.Text = "-- Select --"
                            l.Value = "0"
                            ddl.CssClass = "form-control"
                            ddl.Enabled = IsEnabled
                            ddl.Items.Add(l)
                            For Each rowChoice As DataRow In ds.Tables(0).Select("PEStandardObjRevNo=" & PEStandardObjRevNo)
                                Dim li As New ListItem
                                li.Text = Generic.ToStr(rowChoice("Anchor"))
                                li.Value = Generic.ToInt(rowChoice("PEStandardDetiRevNo"))
                                If Generic.ToInt(li.Value) > 0 Then
                                    ddl.Items.Add(li)
                                End If
                            Next
                            If rowObj("IsRequired") And IsEnabled = True Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardObjRevNo
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
                            txt.ID = "txt" & PEStandardObjRevNo
                            txt.CssClass = "form-control"
                            txt.Enabled = IsEnabled
                            pForm.Controls.Add(New LiteralControl("<div class='col-md-3'>"))
                            pForm.Controls.Add(txt)
                            pForm.Controls.Add(New LiteralControl("</div>"))
                            If rowObj("IsRequired") Then
                                Dim rf As New RequiredFieldValidator
                                rf.ID = "rf" & PEStandardObjRevNo.ToString()
                                rf.Display = ValidatorDisplay.Dynamic
                                rf.ControlToValidate = txt.ID
                                rf.Text = "* This field is required."
                                rf.ValidationGroup = "EvalValidationGroup"
                                pForm.Controls.Add(rf)
                            End If
                            Dim cf As New CompareValidator
                            cf.ID = "cf" & PEStandardObjRevNo
                            cf.Display = ValidatorDisplay.Dynamic
                            cf.ControlToValidate = txt.ID
                            cf.Text = "*"
                            cf.Operator = ValidationCompareOperator.DataTypeCheck
                            cf.Type = ValidationDataType.Double
                            pForm.Controls.Add(cf)

                            Dim filtertxt As AjaxControlToolkit.FilteredTextBoxExtender
                            filtertxt = New AjaxControlToolkit.FilteredTextBoxExtender()
                            filtertxt.ID = "filtertxt" & PEStandardObjRevNo
                            filtertxt.FilterType = AjaxControlToolkit.FilterTypes.Custom
                            filtertxt.ValidChars = "1234567890.-"
                            filtertxt.TargetControlID = txt.ID
                            pForm.Controls.Add(filtertxt)
                    End Select

                    pForm.Controls.Add(New LiteralControl("</div>"))



                Next

                pForm.Controls.Add(New LiteralControl("</div>"))

                If IsAddCate = True Then
                    pForm.Controls.Add(New LiteralControl("<ul class='panel-controls'>"))
                    pForm.Controls.Add(New LiteralControl("<li>"))
                    pForm.Controls.Add(lnkMoveUp)
                    pForm.Controls.Add(New LiteralControl("</li>"))
                    pForm.Controls.Add(New LiteralControl("<li>"))
                    pForm.Controls.Add(lnkMoveDown)
                    pForm.Controls.Add(New LiteralControl("</li>"))
                    pForm.Controls.Add(New LiteralControl("</ul>"))
                End If

                pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("</div>"))
                pForm.Controls.Add(New LiteralControl("</div>"))

            Next


            pForm.Controls.Add(New LiteralControl("</div>"))
            pForm.Controls.Add(New LiteralControl("</div>"))
            pForm.Controls.Add(New LiteralControl("</div>"))

            If isposted = True Then
                Generic.EnableControls(Me, "pForm", False)
                lnkAdd.Visible = False
                lnkSave.Visible = False
            End If

        Catch ex As Exception

        End Try


    End Sub

    'MBO Answer
    Private Sub MBOPopulateAnswer()
        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EPEStandardObjRev_WebFormAns", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno)
            Dim dtQuestion As DataTable
            dtQuestion = ds.Tables(0)
            For Each rowQuestion As DataRow In dtQuestion.Rows
                Dim txtRemarks As New TextBox
                Dim ResponseTypeNo As Integer = Generic.ToInt(rowQuestion("ResponseTypeNo"))
                Dim PEStandardObjRevNo As Integer = Generic.ToInt(rowQuestion("PEStandardObjRevNo"))
                Select Case ResponseTypeNo
                    Case 1
                        'Radio Button
                        Dim rbl As New RadioButtonList
                        rbl = pForm.FindControl("rbl" & PEStandardObjRevNo.ToString())
                        rbl.SelectedValue = Generic.ToStr(rowQuestion("AnswerNo"))
                    Case 2
                        'Checkbox
                        Dim cbl As New CheckBoxList
                        cbl = pForm.FindControl("cbl" & PEStandardObjRevNo.ToString())
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
                        txt = pForm.FindControl("txt" & PEStandardObjRevNo.ToString())
                        txt.Text = Generic.ToStr(rowQuestion("AnswerNo"))
                    Case 4
                        'Dropdown
                        Dim ddl As New DropDownList
                        ddl = pForm.FindControl("ddl" & PEStandardObjRevNo.ToString())
                        Try
                            ddl.SelectedValue = Generic.ToStr(rowQuestion("AnswerNo"))
                        Catch ex As Exception

                        End Try
                    Case 5
                        'Numeric
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & PEStandardObjRevNo.ToString())
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
            ds = SQLHelper.ExecuteDataSet("EPEStandardObjRev_WebForm", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno)
            Dim dtQuestion As DataTable
            dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PEStandardObjRevNo", "PEStandardRevNo", "Description", "ResponseTypeNo", "IsRequired", "ObjectiveTypeNo")

            'Validate the total weighted
            Dim Weighted As Double, TotalWeighted As Double
            For Each rowQuestion As DataRow In dtQuestion.Rows
                Dim ObjectiveTypeNo As Integer = Generic.ToInt(rowQuestion("ObjectiveTypeNo"))
                Dim PEStandardObjRevNo As Integer = Generic.ToInt(rowQuestion("PEStandardObjRevNo"))
                Dim ResponseTypeNo As Integer = Generic.ToInt(rowQuestion("ResponseTypeNo"))
                If ObjectiveTypeNo = 3 Then 'Weighted
                    If ResponseTypeNo = 5 Then 'Numeric
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & PEStandardObjRevNo)
                        Weighted = txt.Text
                        TotalWeighted = TotalWeighted + Weighted
                    End If
                End If
            Next

            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EPEStandardObjRev_WebValidate_Weight", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, TotalWeighted)
            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("tProceed"))
                messagedialog = Generic.ToStr(rowx("xMessage"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                Return False
                Exit Function
            End If

            For Each rowQuestion As DataRow In dtQuestion.Rows
                Dim txtRemarks As New TextBox
                Dim AnswerNo As String = "", AnswerDesc As String = "", Remarks As String = ""
                Dim ResponseTypeNo As Integer = Generic.ToInt(rowQuestion("ResponseTypeNo"))
                Dim PEStandardRevNo As Integer = Generic.ToInt(rowQuestion("PEStandardRevNo"))
                Dim PEStandardObjRevNo As Integer = Generic.ToInt(rowQuestion("PEStandardObjRevNo"))
                Dim PEStandardDetiRevNo As String = 0
                Select Case ResponseTypeNo
                    Case 1
                        'Radio button
                        Dim rbl As New RadioButtonList
                        rbl = pForm.FindControl("rbl" & PEStandardObjRevNo)
                        PEStandardDetiRevNo = Generic.ToStr(rbl.SelectedValue)
                        AnswerNo = Generic.ToStr(rbl.SelectedValue)
                        If PEStandardDetiRevNo > "" Then
                            AnswerDesc = rbl.SelectedItem.Text
                        End If
                    Case 2
                        'Checkbox
                        Dim cbl As New CheckBoxList
                        cbl = pForm.FindControl("cbl" & PEStandardObjRevNo)
                        'EvalTemplateDetlChoiceNo = 0
                        AnswerNo = CheckBoxValue(cbl, 1)
                        AnswerDesc = CheckBoxValue(cbl, 0)
                    Case 3
                        'Narrative
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & PEStandardObjRevNo)
                        'EvalTemplateDetlChoiceNo = 0
                        AnswerNo = txt.Text
                        AnswerDesc = txt.Text
                    Case 4
                        'Dropdown
                        Dim ddl As New DropDownList
                        ddl = pForm.FindControl("ddl" & PEStandardObjRevNo)
                        PEStandardDetiRevNo = Generic.ToStr(ddl.SelectedValue)
                        AnswerNo = Generic.ToStr(ddl.SelectedValue)
                        If PEStandardDetiRevNo > "" Then
                            AnswerDesc = ddl.SelectedItem.Text
                        End If
                    Case 5
                        'Numeric
                        Dim txt As New TextBox
                        txt = pForm.FindControl("txt" & PEStandardObjRevNo)
                        'EvalTemplateDetlChoiceNo = 0
                        AnswerNo = txt.Text
                        AnswerDesc = txt.Text
                End Select


                If ResponseTypeNo <> 0 Then
                    If SQLHelper.ExecuteNonQuery("EPEStandardObjRev_WebUpdate", UserNo, PEStandardObjRevNo, PEStandardRevNo, peevaluatorno, PEStandardDetiRevNo, ResponseTypeNo, AnswerNo, AnswerDesc) > 0 Then
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


#End Region


#Region "******** Save KRA and MBO ********"

    'Save
    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim ret As Boolean = False
        If pecatetypeno = 1 Then 'MBO
            ret = MBOSaveRecord()
        ElseIf pecatetypeno = 2 Then 'KRA
            ret = KRASaveRecord()
        ElseIf pecatetypeno = 3 Then 'Competency
            ret = CompSaveRecord()
        End If

        If ret Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

#End Region


#Region "******** Item ********"

    'Add New Items
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EPEStandardObjRev_WebValidate", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno)

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
            dt = SQLHelper.ExecuteDataTable("EPEStandardObjRev_WebAdd", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno)

            Dim url As String = FormName & "?pereviewmainno=" & pereviewmainno & "&pereviewcateno=" & pereviewcateno & "&pecatetypeno=" & pecatetypeno & "&pereviewno=" & pereviewno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&componentno=" & componentno & "&isposted=" & isposted
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        ElseIf pecatetypeno = 2 Then

            Generic.ClearControls(Me, "pnlPopupItem")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupItem", Generic.ToInt(Session("xPayLocNo")))
            cboPEReviewDimNo.Enabled = True
            cboPEReviewDimNo.Text = ""
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
        cboPEReviewDimNo.Enabled = False
        If i = 0 Then
            cboPEReviewDimNo.Text = ""
        Else
            cboPEReviewDimNo.Text = i
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
            dt = SQLHelper.ExecuteDataTable("EPEStandardRev_WebOne", UserNo, i)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupItem", dt)
            Next
        Catch ex As Exception

        End Try

        cboPEReviewDimNo.Enabled = False
        mdlItem.Show()

    End Sub

    'Edit Save
    Protected Sub lnkSaveItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim PEStandardRevNo As Integer = Generic.ToInt(Me.txtPEStandardRevNo.Text)
        Dim PEStandardCode As String = Generic.ToStr(Me.txtPEStandardCode.Text)
        Dim PEStandardDesc As String = Generic.ToStr(Me.txtPEStandardDesc.Text)
        Dim Standard As String = Generic.ToStr(Me.txtStandard.Text)
        Dim OrderLevel As Integer = Generic.ToInt(Me.txtOrderLevelItem.Text)
        Dim ResponseTypeNo As Integer = Generic.ToInt(Me.cboResponseTypeNo.SelectedValue)
        Dim HasComment As Boolean = Generic.ToBol(Me.chkHasComment.Checked)
        Dim IsRequired As Boolean = Generic.ToBol(Me.chkIsRequired.Checked)
        Dim PEReviewDimNo As Integer = Generic.ToInt(Me.cboPEReviewDimNo.SelectedValue)

        If SQLHelper.ExecuteNonQuery("EPEStandardRev_WebSave", UserNo, pereviewmainno, pereviewno, PEStandardRevNo, PEStandardCode, PEStandardDesc, Standard, pereviewcateno, PEReviewDimNo, ResponseTypeNo, OrderLevel, HasComment, IsRequired, peevaluatorno) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            Dim url As String = FormName & "?pereviewmainno=" & pereviewmainno & "&pereviewcateno=" & pereviewcateno & "&pecatetypeno=" & pecatetypeno & "&pereviewno=" & pereviewno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&componentno=" & componentno & "&isposted=" & isposted
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            'MessageBox.Success(MessageTemplate.SuccessSave, Me)
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
            Generic.DeleteRecordAuditCol("EPEReviewDeti", UserNo, "PEStandardRevNo", i)
            Generic.DeleteRecordAuditCol("EPEStandardDetiRev", UserNo, "PEStandardRevNo", i)
            Generic.DeleteRecordAuditCol("EPEStandardObjRev", UserNo, "PEStandardRevNo", i)
            Generic.DeleteRecordAudit("EPEStandardRev", UserNo, i)

            Dim ds As New DataSet
            Dim dtQuestion As DataTable

            If pecatetypeno = 1 Then 'MBO Update Item No.
                ds = SQLHelper.ExecuteDataSet("EPEStandardObjRev_WebForm", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno)
                dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PEStandardRevNo", "PEStandardCode", "OrderLevelItem")
                For Each rowQuestion As DataRow In dtQuestion.Rows
                    Dim PEStandardRevNo As Integer = Generic.ToInt(rowQuestion("PEStandardRevNo"))
                    Dim OrderLevelItem As Integer = Generic.ToInt(rowQuestion("OrderLevelItem"))
                    SQLHelper.ExecuteDataSet("EPEStandardRev_WebDelete", PEStandardRevNo, OrderLevelItem)
                Next
            ElseIf pecatetypeno = 2 Then 'KRA Update Item No.
                ds = SQLHelper.ExecuteDataSet("EPEStandardRev_WebForm", UserNo, pereviewmainno, pereviewcateno, pereviewno, peevaluatorno, pecycleno, componentno)
                dtQuestion = ds.Tables(0).DefaultView.ToTable(True, "PEStandardRevNo", "PEStandardCode", "OrderLevelItem")
                For Each rowQuestion As DataRow In dtQuestion.Rows
                    Dim PEStandardRevNo As Integer = Generic.ToInt(rowQuestion("PEStandardRevNo"))
                    Dim OrderLevelItem As Integer = Generic.ToInt(rowQuestion("OrderLevelItem"))
                    SQLHelper.ExecuteDataSet("EPEStandardRev_WebDelete", PEStandardRevNo, OrderLevelItem)
                Next
            End If


            Dim url As String = FormName & "?pereviewmainno=" & pereviewmainno & "&pereviewcateno=" & pereviewcateno & "&pecatetypeno=" & pecatetypeno & "&pereviewno=" & pereviewno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&componentno=" & componentno & "&isposted=" & isposted
            MessageBox.SuccessResponse(MessageTemplate.SuccessDelete, Me, url)
        End If

    End Sub


#End Region


#Region "******** Item Details ********"

    Protected Sub lnkEditDeti_Click(sender As Object, e As EventArgs)

        Dim lnkEditDeti As New LinkButton(), i As Integer = 0
        lnkEditDeti = DirectCast(sender, LinkButton)
        ViewState("PEStandardRevNo") = Generic.ToInt(Generic.Split(lnkEditDeti.CommandArgument, 0))
        ViewState("PEStandardObjRevNo") = Generic.ToInt(Generic.Split(lnkEditDeti.CommandArgument, 1))
        lblChoices.Text = "Item No. " & Generic.ToStr(Generic.Split(lnkEditDeti.CommandArgument, 2))

        PopulateChoices()
        mdlChoices.Show()

    End Sub

    Protected Sub PopulateChoices()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EPEStandardDetiRev_Web", UserNo, Generic.ToInt(ViewState("PEStandardRevNo")), Generic.ToInt(ViewState("PEStandardObjRevNo")))

            If IsAddDeti = False Then
                For Each row As DataRow In dt.Rows
                    If Generic.ToStr(row("CodeDeti")) = "Add here" Then
                        row.Delete()
                    End If
                Next
            End If

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
                Generic.DeleteRecordAudit("EPEStandardDetiRev", UserNo, CType(lbl.Text, Integer))
                Count = Count + 1
            End If
        Next

        PopulateChoices()

        For tcount = 0 To Me.grdChoices.Rows.Count - 1
            lbl = CType(grdChoices.Rows(tcount).FindControl("lblDetlNo"), Label)
            lblOrder = CType(grdChoices.Rows(tcount).FindControl("lblOrder"), Label)
            Dim OrderLevel As Integer = Generic.ToInt(lblOrder.Text)

            If SQLHelper.ExecuteNonQuery("EPEStandardDetiRev_WebDelete", CType(lbl.Text, Integer), OrderLevel) > 0 Then
                PopulateChoices()
            End If
        Next

        If Count > 0 Then
            'MessageBox.Alert("(" + Count.ToString + ") " + MessageTemplate.SuccessDelete, "success", Me)
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

            Dim PEStandardRevNo As Integer = Generic.ToInt(ViewState("PEStandardRevNo"))
            Dim PEStandardObjRevNo As Integer = Generic.ToInt(ViewState("PEStandardObjRevNo"))
            Dim OrderLevel As String = Generic.ToStr(lblOrder.Text)
            Dim PEStandardDetiRevNo As Integer = Generic.ToInt(lblDetlNo.Text)
            Dim Description As String = Generic.ToStr(txtDescription.Text)
            Dim PERatingNo As Integer = Generic.ToInt(cboPERatingNo.SelectedValue)
            Dim Profeciency As Double = Generic.ToDec(txtProfeciency.Text)

            If Description > "" Then
                If SQLHelper.ExecuteNonQuery("EPEStandardDetiRev_WebSave", UserNo, PEStandardDetiRevNo, PEStandardRevNo, PEStandardObjRevNo, PERatingNo, Description, Profeciency, OrderLevel) > 0 Then
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

                If SQLHelper.ExecuteNonQuery("EPEStandardDetiRev_WebDelete", CType(lbl.Text, Integer), OrderLevel) > 0 Then
                    PopulateChoices()
                End If
            Next

        Else
            MessageBox.Alert(MessageTemplate.NoSelectedTransaction, "information", Me)
        End If

        mdlChoices.Show()

    End Sub

    Protected Sub lnkRefreshChoices_Click(sender As Object, e As EventArgs)

        Dim url As String = FormName & "?pereviewmainno=" & pereviewmainno & "&pereviewcateno=" & pereviewcateno & "&pecatetypeno=" & pecatetypeno & "&pereviewno=" & pereviewno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&componentno=" & componentno & "&pecycleno=" & pecycleno & "&componentno=" & componentno & "&isposted=" & isposted
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


#Region "******** Setting ********"

    Protected Sub lnkCycle_Click(sender As Object, e As System.EventArgs)
        mdlSetting.Show()
    End Sub

    Protected Sub lnkSaveSetting_Click(sender As Object, e As EventArgs)

        pecycleno = Generic.ToInt(cboPECycleNo.SelectedValue)
        Dim url As String = FormName & "?pereviewmainno=" & pereviewmainno & "&pereviewcateno=" & pereviewcateno & "&pecatetypeno=" & pecatetypeno & "&pereviewno=" & pereviewno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&componentno=" & componentno & "&isposted=" & isposted
        MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)

    End Sub

#End Region


#Region "******** Edit Category ********"

    Protected Sub lnkCate_Click(sender As Object, e As EventArgs)
        Dim lnkCate As New LinkButton()
        lnkCate = DirectCast(sender, LinkButton)
        Dim i As Integer = Generic.ToInt(lnkCate.CommandArgument)

        mdlCate.Show()

    End Sub

#End Region


#Region "******** Edit Dimension ********"

    Protected Sub lnkDim_Click(sender As Object, e As EventArgs)
        Dim lnkDim As New LinkButton()
        lnkDim = DirectCast(sender, LinkButton)
        Dim i As Integer = Generic.ToInt(lnkDim.CommandArgument)

        mdlDim.Show()

    End Sub

#End Region


#Region "******** Move Up & Down ********"

    Protected Sub lnkMoveUp_Click(sender As Object, e As EventArgs)
        Dim lnkMoveUp As New LinkButton()
        lnkMoveUp = DirectCast(sender, LinkButton)
        Dim i As Integer = Generic.ToInt(lnkMoveUp.CommandArgument)

        Dim Retval As Boolean = False
        If SQLHelper.ExecuteNonQuery("EPEStandardRev_WebUpdate", UserNo, i, 2) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            Dim url As String = FormName & "?pereviewmainno=" & pereviewmainno & "&pereviewcateno=" & pereviewcateno & "&pecatetypeno=" & pecatetypeno & "&pereviewno=" & pereviewno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&componentno=" & componentno & "&isposted=" & isposted
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
        If SQLHelper.ExecuteNonQuery("EPEStandardRev_WebUpdate", UserNo, i, 1) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            Dim url As String = FormName & "?pereviewmainno=" & pereviewmainno & "&pereviewcateno=" & pereviewcateno & "&pecatetypeno=" & pecatetypeno & "&pereviewno=" & pereviewno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&componentno=" & componentno & "&isposted=" & isposted
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            'Else
            '    MessageBox.Critical("Unable to move", Me)
        End If

    End Sub

#End Region


End Class