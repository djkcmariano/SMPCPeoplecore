Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_BENLeaveMoneApplication
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    Dim xScript As String = ""
    Dim tstatus As Integer
    Dim dscount As Double = 0
    Dim _ds As New DataSet
    Dim _dt As New DataTable
    Dim rowno As Integer = 0
    Dim tabOrder As Integer = 0

    Dim clsMessage As New clsMessage
    Dim IsClickMain As Integer = 0
    Dim IsClickTab As Integer = 0

#Region "Main"

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable

            If tabOrder = 1 Then
                tstatus = 1 : Session(xScript & "StatNo") = 1
            ElseIf tabOrder = 2 Then
                tstatus = 2 : Session(xScript & "StatNo") = 2
            ElseIf tabOrder = 3 Then
                tstatus = 3 : Session(xScript & "StatNo") = 3
            ElseIf tabOrder = 4 Then
                tstatus = 0 : Session(xScript & "StatNo") = 4
            ElseIf tabOrder = 5 Then
                tstatus = 4 : Session(xScript & "StatNo") = 5
            Else
                tstatus = 1 : Session(xScript & "StatNo") = 1
            End If

            dt = SQLHelper.ExecuteDataTable("ELeaveMonitization_WebManager", UserNo, "", "", "", Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ELeaveMonitized_WebOne01", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopup", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.CheckDBNull(Request.QueryString("id"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        'AccessRights.CheckUser(UserNo)
        Permission.IsAuthenticated()
        If Not IsPostBack Then
            PopulateDropDown()
            PopulateGrid()
        End If

        ShowHideButtons(Me.cboTabNo.SelectedValue)

        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 41)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

        'Try
        '    cboLeaveMonitizedTypeNo.DataSource = SQLHelper.ExecuteDataTable("ELeaveMonitizedType", UserNo)
        '    cboLeaveMonitizedTypeNo.DataTextField = "tDesc"
        '    cboLeaveMonitizedTypeNo.DataValueField = "tno"
        '    cboLeaveMonitizedTypeNo.DataBind()
        'Catch ex As Exception
        'End Try

        'Try
        '    Me.cboLeaveMoneReasonTypeLNo.DataSource = SQLHelper.ExecuteDataTable("ELeaveMoneReasonTypeL", UserNo)
        '    Me.cboLeaveMoneReasonTypeLNo.DataTextField = "tdesc"
        '    Me.cboLeaveMoneReasonTypeLNo.DataValueField = "tno"
        '    Me.cboLeaveMoneReasonTypeLNo.DataBind()
        'Catch ex As Exception

        'End Try

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
        'Generic.ClearControls(Me, "pnlPopup")
        'Generic.EnableControls(Me, "pnlPopup", True)
        'cboApprovalStatNo.Text = 2
        'cboApprovalStatNo.Enabled = False
        'lnkSave.Enabled = True

        Dim xMsg As String = "", Title As String = ""
        'ELeaveMonetized_WebValidate_LWOPAWOL(xMsg, Title) 'TRAPPING FOR AWOL... . (Disabled in Admin)
        Session(xScript & "No") = 0
        If xMsg > "" Then
            Exit Sub
        Else
            Try
                Generic.ClearControls(Me, "pnlPopup")
            Catch ex As Exception

            End Try

            'CLEAR FIELDS.. .
            Me.txtFullName.Text = ""
            Me.txtFullName.Enabled = True
            Me.txtLeaveMonitizedTransNo.Text = ""
            Me.txtLeaveMonitizedNo.Text = ""
            'Me.cboEmployeeNo.Text = ""
            'Me.txtTransDate.Text = ""
            Me.cboLeaveMonitizedTypeNo.Text = ""
            'Me.txtVLBegBal.Text = 0
            Me.txtVLApplied.Text = 0
            Me.txtVLEndBal.Text = 0
            'Me.txtSLBegBal.Text = 0
            Me.txtSLApplied.Text = 0
            Me.txtSLEndBal.Text = 0
            Me.cboLeaveMoneReasonTypeLNo.Text = ""
            Me.txtxRemarks.Text = ""

            Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)
            'Try
            '    cboLeaveMonitizedTypeNo.DataSource = SQLHelper.ExecuteDataTable("ELeaveMonitizedType", UserNo)
            '    cboLeaveMonitizedTypeNo.DataTextField = "tDesc"
            '    cboLeaveMonitizedTypeNo.DataValueField = "tno"
            '    cboLeaveMonitizedTypeNo.DataBind()
            'Catch ex As Exception
            'End Try

            'Try
            '    Me.cboLeaveMoneReasonTypeLNo.DataSource = SQLHelper.ExecuteDataTable("ELeaveMoneReasonTypeL", UserNo)
            '    Me.cboLeaveMoneReasonTypeLNo.DataTextField = "tDesc"
            '    Me.cboLeaveMoneReasonTypeLNo.DataValueField = "tno"
            '    Me.cboLeaveMoneReasonTypeLNo.DataBind()
            'Catch ex As Exception

            'End Try

            Try
                Dim xds As New DataSet
                Dim LeaveMonitizedTypeNo As Integer = 0
                xds = SQLHelper.ExecuteDataSet("[ELeaveMonetype_WebOneDefault]")
                If xds.Tables.Count > 0 Then
                    LeaveMonitizedTypeNo = Generic.ToInt(xds.Tables(0).Rows(0)("LeaveMonitizedTypeNo"))
                    Me.cboLeaveMonitizedTypeNo.Text = LeaveMonitizedTypeNo
                End If
            Catch ex As Exception

            End Try

            Dim Msg As String = ""
            Me.lblMsgNotice.Text = ELeaveMonitization_Msg(Msg, 1)
            Me.lblMsgNotice2.Text = ELeaveMonitization_Msg(Msg, 2)

            DisableLeavehrs()

            'GET THE BALANCE
            LeaveBalance_Lookup()
            LeaveAvailed_Lookup()

            If Me.txtTransDate.Text = "" Then
                Me.txtTransDate.Text = Format(Now(), "MM/dd/yyyy")
            End If

            mdlShow.Show()
        End If

        'Else
        'MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        'End If
    End Sub


    Protected Sub lnkEditS_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveMonitizedNo"})))
        Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsAEnabled"}))
        Generic.EnableControls(Me, "pnlPopup", IsEnabled)
        'cboApprovalStatNo.Enabled = True
        lnkSave.Enabled = False
        mdlShow.Show()
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        'End If
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
        Try
            Dim i As String = ""

            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveMonitizedNo"})))
            Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsAEnabled"}))
            Generic.EnableControls(Me, "pnlPopup", IsEnabled)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveMonitizedNo"}))
            ViewState(xScript & "No") = i 'HRANNO
            lnkSave.Enabled = IsEnabled
            'End If

            Dim _ds As New DataSet
            '_ds = sqlHelp.ExecuteDataset(clsConnectionString.xConSTR, "[ELeaveMonitized_WebOne]", xPublicVar.xOnlineUseNo, i)
            _ds = SQLHelper.ExecuteDataSet("[ELeaveMonitized_WebOne01]", UserNo, i)
            If _ds.Tables.Count > 0 Then
                If _ds.Tables(0).Rows.Count > 0 Then

                    Generic.ClearControls(Me, "pnlPopup")

                    Me.txtLeaveMonitizedTransNo.Text = _ds.Tables(0).Rows(0)("Code")
                    Me.txtLeaveMonitizedNo.Text = _ds.Tables(0).Rows(0)("LeaveMonitizedNo")
                    Me.hifEmployeeNo.Value = _ds.Tables(0).Rows(0)("EmployeeNo")
                    Me.txtFullName.Text = _ds.Tables(0).Rows(0)("FullName")
                    Me.txtTransDate.Text = _ds.Tables(0).Rows(0)("TransDate")
                    Me.cboLeaveMonitizedTypeNo.Text = _ds.Tables(0).Rows(0)("LeaveMonitizedTypeNo")
                    Me.txtVLBegBal.Text = _ds.Tables(0).Rows(0)("VLBegBal")
                    Me.txtVLApplied.Text = _ds.Tables(0).Rows(0)("VLApplied")
                    Me.txtVLEndBal.Text = _ds.Tables(0).Rows(0)("VLEndBal")
                    Me.txtSLBegBal.Text = _ds.Tables(0).Rows(0)("SLBegBal")
                    Me.txtSLApplied.Text = _ds.Tables(0).Rows(0)("SLApplied")
                    Me.txtSLEndBal.Text = _ds.Tables(0).Rows(0)("SLEndBal")
                    Me.cboLeaveMoneReasonTypeLNo.Text = _ds.Tables(0).Rows(0)("LeaveMoneReasonTypeLNo")
                    Me.txtxRemarks.Text = _ds.Tables(0).Rows(0)("xRemarks")

                    'Try
                    '    cboLeaveMonitizedTypeNo.DataSource = SQLHelper.ExecuteDataTable("ELeaveMonitizedType", UserNo)
                    '    cboLeaveMonitizedTypeNo.DataTextField = "tDesc"
                    '    cboLeaveMonitizedTypeNo.DataValueField = "tno"
                    '    cboLeaveMonitizedTypeNo.DataBind()
                    'Catch ex As Exception
                    'End Try

                    'Try
                    '    Me.cboLeaveMoneReasonTypeLNo.DataSource = SQLHelper.ExecuteDataTable("ELeaveMoneReasonTypeL", UserNo)
                    '    Me.cboLeaveMoneReasonTypeLNo.DataTextField = "tDesc"
                    '    Me.cboLeaveMoneReasonTypeLNo.DataValueField = "tno"
                    '    Me.cboLeaveMoneReasonTypeLNo.DataBind()
                    'Catch ex As Exception

                    'End Try

                    DisableLeavehrs()
                    Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", PayLocNo)

                    Me.txtSLApplied.Enabled = IsEnabled
                    Me.txtVLApplied.Enabled = IsEnabled
                    Me.cboLeaveMoneReasonTypeLNo.Enabled = IsEnabled
                    Me.txtxRemarks.Enabled = IsEnabled

                End If
            End If

            If ViewState(xScript & "No") > 0 Then
                Me.txtFullName.Enabled = False
            Else
                Me.txtFullName.Enabled = True
            End If

            Dim Msg As String = ""
            Me.lblMsgNotice.Text = ELeaveMonitization_Msg(Msg, 1)
            Me.lblMsgNotice2.Text = ELeaveMonitization_Msg(Msg, 2)

            If (tabOrder > 1) Then
                Me.txtFullName.Enabled = False 'Enable only if for Approval. ..
            End If

            LeaveAvailed_Lookup()

            Me.txtTotalYear_VLOnly.Enabled = False
            Me.txtTotalYear_SLVL_VL.Enabled = False
            Me.txtTotalYear_SLVL_SL.Enabled = False
            Me.txtVLBegBal.Enabled = False
            Me.txtVLEndBal.Enabled = False
            Me.txtSLBegBal.Enabled = False
            Me.txtSLEndBal.Enabled = False

            mdlShow.Show()

        Catch ex As Exception
        End Try
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        'End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"LeaveMonitizedNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("ELeaveMonitized", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        'End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkSaveS_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim LeaveApplicationNo As Integer = 0 'Generic.ToInt(txtLeaveApplicationNo.Text)
        Dim EmployeeNo As Integer = 0 'Generic.ToInt(hifEmployeeNo.Value)
        Dim LeaveTypeNo As Integer = 0 'Generic.ToInt(cboLeavetypeNo.SelectedValue)
        Dim StartDate As String = "" 'Generic.ToStr(txtStartDate.Text)
        Dim EndDate As String = "" 'Generic.ToStr(txtEndDate.Text)
        Dim AppliedHrs As Double = 0 'Generic.ToDec(txtAppliedHrs.Text)
        Dim IsForAM As Boolean = 0 'Generic.ToBol(txtISForAM.Checked)
        Dim Reason As String = "" 'Generic.ToStr(txtReason.Text)
        Dim ApprovalStatNo As Integer = 0 'Generic.ToInt(cboApprovalStatNo.SelectedValue)
        Dim ComponentNo As Integer = 1 'Administrator

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("ELeaveApplication_WebValidate", UserNo, LeaveApplicationNo, EmployeeNo, LeaveTypeNo, StartDate, EndDate, AppliedHrs, IsForAM, ApprovalStatNo, PayLocNo, ComponentNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlShow.Show()
            Exit Sub
        End If

        dt = SQLHelper.ExecuteDataTable("ELeaveApplication_WebSave", UserNo, LeaveApplicationNo, EmployeeNo, LeaveTypeNo, StartDate, EndDate, AppliedHrs, IsForAM, Reason, ApprovalStatNo, PayLocNo)
        For Each row As DataRow In dt.Rows
            RetVal = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                RetVal = False
            End If
        Next
        If RetVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tSave As Integer = 0
        Dim xds As New DataSet
        Dim Msg As String = "", IsValid As Boolean, Title As String = ""
        Dim leavemonitizedtypeno As Integer = Generic.ToInt(Me.cboLeaveMonitizedTypeNo.SelectedValue)
        Dim leavetypeno As Integer = 0 'Generic.ToInt(Me.cboLeavetypeNo.SelectedValue)
        Dim LeaveMonitizedNo As Integer = Generic.ToInt(txtLeaveMonitizedNo.Text)
        'Me.lnkSave.Enabled = False
        Dim VLBegBal_New As Integer = 0, SLBegBal_New As Integer = 0
        ViewState("VLBegBal_New") = 0
        ViewState("SLBegBal_New") = 0
        xds = SQLHelper.ExecuteDataSet("[ELeaveMonetized_WebValidate_Admin]", UserNo, Generic.ToInt(Me.hifEmployeeNo.Value), LeaveMonitizedNo, leavemonitizedtypeno, _
                                     Generic.ToDbl(Me.txtVLApplied.Text), _
                                     Generic.ToDbl(Me.txtSLApplied.Text), _
                                     Me.txtTransDate.Text)
        If xds.Tables.Count > 0 Then
            IsValid = Generic.ToInt(xds.Tables(0).Rows(0)("IsValid"))
            Msg = Generic.ToStr(xds.Tables(0).Rows(0)("Msg"))
            Title = Generic.ToStr(xds.Tables(0).Rows(0)("Title"))
            VLBegBal_New = Generic.ToDbl(xds.Tables(0).Rows(0)("VL_Beg_Balance_Days"))
            SLBegBal_New = Generic.ToDbl(xds.Tables(0).Rows(0)("SL_Beg_Balance_Days"))
            ViewState("VLBegBal_New") = VLBegBal_New
            ViewState("SLBegBal_New") = SLBegBal_New
        End If

        If IsValid = False Then
            tSave = saverecord()
            If Me.txtVLBegBal.Text > 0 And Me.txtVLBegBal.Text <> VLBegBal_New Then
                Me.txtVLBegBal.Text = VLBegBal_New
            End If
            If Me.txtSLBegBal.Text > 0 And Me.txtSLBegBal.Text <> SLBegBal_New Then
                Me.txtSLBegBal.Text = SLBegBal_New
            End If
            If tSave = 1 Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                PopulateGrid()
            ElseIf tSave = 4 Then
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
            Me.lnkSave.Enabled = True
        Else
            If Msg > "" Then
                Dim url As String = "ctl00_cphBody_mdlShow"
                MessageBox.Alert(Msg.ToString, Title, Me)
                'PopulateGrid()
                Me.lnkSave.Enabled = True
                fRegisterStartupScript("Sript", "disableenable_behind('" + IIf(ViewState("IsVisible2"), 1, 0).ToString + "');")
                mdlShow.Show()
                'Exit Sub
            End If
        End If

    End Sub

    Private Function saverecord() As Integer

        Dim tProceed As Integer = 0
        Dim Balance As Double
        Dim xMsg$
        Dim employeeno As Integer = Generic.ToInt(Me.hifEmployeeNo.Value)
        Dim leavemonitizedtypeno As Integer = Generic.ToInt(Me.cboLeaveMonitizedTypeNo.SelectedValue)
        Dim leavetypeno As Integer = 0 'Generic.ToInt(Me.cboLeavetypeNo.SelectedValue)
        Dim LeaveMonitizedNo As Integer = Generic.ToInt(txtLeaveMonitizedNo.Text)
        Dim LeaveMoneReasonTypeNo As Integer = Generic.ToInt(Me.cboLeaveMoneReasonTypeLNo.SelectedValue)
        Dim ds As DataSet
        Dim RETVAL As Integer = 0
        Dim isUpdate As Integer = 0
        'Dim tProceed As Integer = 0

        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""

        'ds = sqlHelp.ExecuteDataset(clsConnectionString.xConSTR, "ELeaveMonitized_WebSaveSelf", xPublicVar.xOnlineUseNo, LeaveMonitizedNo, employeeno, xBase.CheckDBNull(Me.txtTransDate.Text.ToString, clsBase.clsBaseLibrary.enumObjectType.StrType), leavetypeno, xBase.CheckDBNull(Me.txtLeaveHrs.Text, clsBase.clsBaseLibrary.enumObjectType.IntType), leavemonitizedtypeno, Me.txtxRemarks.Text)
        ds = SQLHelper.ExecuteDataSet("ELeaveMonitized_WebSave01", UserNo, LeaveMonitizedNo, employeeno, Generic.ToStr(Me.txtTransDate.Text.ToString), leavemonitizedtypeno, Me.txtxRemarks.Text, _
                                    LeaveMoneReasonTypeNo, _
                                    Generic.ToDbl(Me.txtVLBegBal.Text), _
                                    Generic.ToDbl(Me.txtVLApplied.Text), _
                                    Generic.ToDbl(Me.txtVLEndBal.Text), _
                                    Generic.ToDbl(Me.txtSLBegBal.Text), _
                                    Generic.ToDbl(Me.txtSLApplied.Text), _
                                    Generic.ToDbl(Me.txtSLEndBal.Text), _
                                    Generic.ToDbl(ViewState("VLBegBal_New")), _
                                    Generic.ToDbl(ViewState("SLBegBal_New")))
        saverecord = False
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                RETVAL = Generic.ToInt(ds.Tables(0).Rows(0)("retval"))
                isUpdate = Generic.ToInt(ds.Tables(0).Rows(0)("IsUpdate"))
                tProceed = Generic.ToInt(ds.Tables(0).Rows(0)("tProceed"))
                Balance = Generic.ToInt(ds.Tables(0).Rows(0)("Balance"))
                xMsg = Generic.ToStr(ds.Tables(0).Rows(0)("xMsg"))

                If RETVAL > 0 And 1 = 1 Then
                    Dim dsE As New DataSet
                    dsE = SQLHelper.ExecuteDataSet("ELeaveMonitized_WebEmail", UserNo, RETVAL, isUpdate, PayLocNo)
                    If dsE.Tables.Count > 0 Then
                        If dsE.Tables(0).Rows.Count > 0 Then
                            Dim tbody As String = "", tsubject As String = "", tfrom As String = "", tto As String = ""
                            Dim clsE As New clsBase.clsEmail, IsWithApprover As Boolean, ApproverName As String = "", LeaveType As Integer = 0

                            'tbody = xBase.CheckDBNull(dsE.Tables(0).Rows(0)("Body"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            'tsubject = xBase.CheckDBNull(dsE.Tables(0).Rows(0)("Subject"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            'tfrom = xBase.CheckDBNull(dsE.Tables(0).Rows(0)("tfrom"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            'tto = xBase.CheckDBNull(dsE.Tables(0).Rows(0)("tTo"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            IsWithApprover = Generic.ToInt(dsE.Tables(0).Rows(0)("IsWithApprover"))
                            'ApproverName = xBase.CheckDBNull(dsE.Tables(0).Rows(0)("ApproverName"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                            'LeaveType = xBase.CheckDBNull(dsE.Tables(0).Rows(0)("LeaveTypeNo"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                            If IsWithApprover = True Then
                                'sqlhelp.ExecuteNonQuery(clsConnectionString.xConSTR, "EAutoEmail_LeaveApplication", xPublicVar.xOnlineUseNo, tfrom, tto, tbody, ApproverName, RETVAL, LeaveType)
                            Else
                                'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Error!','No Approver defined on your employee record.');")
                                MessageBox.Alert("No Approver defined on your employee record.", "Error!", Me)
                            End If

                        End If
                    End If
                End If

                For Each row As DataRow In dt.Rows
                    RETVAL = True
                    error_num = Generic.ToInt(row("Error_num"))
                    If error_num > 0 Then
                        error_message = Generic.ToStr(row("ErrorMessage"))
                        MessageBox.Critical(error_message, Me)
                        RETVAL = False
                    End If
                Next

                'If RETVAL = False And error_message = "" Then
                '    MessageBox.Critical(MessageTemplate.ErrorSave, Me)
                'End If
                'If RETVAL = True Then
                '    PopulateGrid()
                '    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                'End If


                If tProceed = 0 Then

                    'Session("xSubmit") = 2
                    Dim url As String = "ctl00_cphBody_mdlShow"
                    'fRegisterStartupScript("JSDialogResponseMDL", "dialogResponseMDL('Error!','" + xMsg + "','" + url + "');")
                    MessageBox.PopupMessage(xMsg, Me, url)
                    PopulateGrid()
                    saverecord = tProceed
                    'Exit Function
                ElseIf tProceed = 2 Then

                    'Session("xSubmit") = 2
                    Dim url As String = "ctl00_cphBody_mdlShow"
                    'fRegisterStartupScript("JSDialogResponseMDL", "dialogResponseMDL('Error!','" + xMsg + "','" + url + "');")
                    MessageBox.PopupMessage(xMsg, Me, url)
                    PopulateGrid()
                    saverecord = tProceed
                    'Exit Function
                Else
                    saverecord = 1
                End If

            End If

        Else
            saverecord = 4
        End If
    End Function

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsAEnabled2"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        ViewState("TransNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"LeaveMonitizedNo"}))
        lbl.Text = "Transaction No. : " & Generic.ToStr(container.Grid.GetRowValues(container.VisibleIndex, New String() {"Code"}))
        PopulateDetl()
    End Sub

    Protected Sub ShowHideButtons(ByVal tStatus As Short)
        Select Case tstatus
            Case 0, 1 'For Approval
                Me.lnkApproved.Visible = True
                Me.lnkDisApproved.Visible = True
            Case 2 'Approved
                Me.lnkApproved.Visible = False
                Me.lnkDisApproved.Visible = False
            Case 3 'Disapproved
                Me.lnkApproved.Visible = False
                Me.lnkDisApproved.Visible = False
            Case 4 'All
                Me.lnkApproved.Visible = False
                Me.lnkDisApproved.Visible = False
            Case 5 'Forwarded
                Me.lnkApproved.Visible = False
                Me.lnkDisApproved.Visible = False
        End Select
    End Sub

    Protected Sub txtVLApplied_TextChanged(sender As Object, e As System.EventArgs) 'Handles txtVLApplied.TextChanged
        Me.txtVLEndBal.Text = Generic.ToDbl(Me.txtVLBegBal.Text) _
        - Generic.ToDbl(Me.txtVLApplied.Text)

        If Generic.ToDbl(Me.txtVLEndBal.Text) < 0 Then
            Me.txtVLEndBal.Text = 0
        End If

        fRegisterStartupScript("Sript", "disableenable_behind('" + IIf(ViewState("IsVisible2"), 1, 0).ToString + "');")

        Me.mdlShow.Show()
    End Sub

    Protected Sub txtSLApplied_TextChanged(sender As Object, e As System.EventArgs) 'Handles txtSLApplied.TextChanged
        Me.txtSLEndBal.Text = Generic.ToDbl(Me.txtSLBegBal.Text) _
        - Generic.ToDbl(Me.txtSLApplied.Text)

        If Generic.ToDbl(Me.txtSLEndBal.Text) < 0 Then
            Me.txtSLEndBal.Text = 0
        End If

        fRegisterStartupScript("Sript", "disableenable_behind('" + IIf(ViewState("IsVisible2"), 1, 0).ToString + "');")

        Me.mdlShow.Show()
    End Sub

    Protected Function ELeaveMonitization_Msg(ByRef Msg As String, ByVal Index As Short) As String
        Dim xds As New DataSet
        xds = SQLHelper.ExecuteDataSet("[ELeaveMonitization_WebOne_Msg]", Index)
        If xds.Tables.Count > 0 Then
            Msg = Generic.ToStr(xds.Tables(0).Rows(0)("xMsg"))
        End If
        Return Msg
    End Function

    Protected Sub cboLeaveMoneReasonTypeLNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles cboLeaveMoneReasonTypeLNo.SelectedIndexChanged
        Dim xds As New DataSet
        Dim IsVisible1 As Boolean = False
        Dim IsWithDocs As Boolean = False
        Dim Msg As String = ""
        xds = SQLHelper.ExecuteDataSet("[ELeaveMoneReasonType_WebOneSelect]", Generic.ToInt(Me.cboLeaveMoneReasonTypeLNo.SelectedValue))
        If xds.Tables.Count > 0 Then
            If xds.Tables(0).Rows.Count > 0 Then
                IsVisible1 = Generic.ToInt(xds.Tables(0).Rows(0)("IsVisible1"))
                IsWithDocs = Generic.ToInt(xds.Tables(0).Rows(0)("IsWithDocs"))
                Msg = Generic.ToStr(xds.Tables(0).Rows(0)("DocsMsg"))
            End If
        End If
        Me.trSLVLReasonOthers.Visible = IsVisible1
        Me.txtxRemarks.Enabled = IsVisible1

        If IsVisible1 Then
            Me.txtxRemarks.CssClass = "form-control required"
        Else
            Me.txtxRemarks.CssClass = ""
            Me.txtxRemarks.Text = ""
        End If

        If IsWithDocs Then
            Me.lblMsgNotice3.Text = Msg
            Me.lblMsgNotice3.Visible = True
        Else
            'Me.lblMsgNotice3.Text = ""
            Me.lblMsgNotice3.Visible = False
        End If

        Me.mdlShow.Show()

    End Sub

    Protected Sub cboLeavemonitizedtypeNo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLeaveMonitizedTypeNo.SelectedIndexChanged
        Try
            DisableLeavehrs()
            LeaveBalance_Lookup()
            'LeaveAvailed_Lookup()
        Catch ex As Exception

        End Try
        mdlShow.Show()
    End Sub

    Private Sub LeaveBalance_Lookup()
        Dim xds As New DataSet
        xds = SQLHelper.ExecuteDataSet("[ELeaveMonetized_WebOne_Balance_Admin]", UserNo, Generic.ToInt(Me.hifEmployeeNo.Value), Generic.ToInt(Me.cboLeaveMonitizedTypeNo.SelectedValue), Me.txtTransDate.Text)
        If xds.Tables.Count > 0 Then
            Me.txtVLBegBal.Text = Generic.ToDbl(xds.Tables(0).Rows(0)("VL"))
            Me.txtSLBegBal.Text = Generic.ToDbl(xds.Tables(0).Rows(0)("SL"))
            Me.txtVLApplied.Text = Generic.ToDbl(xds.Tables(0).Rows(0)("VL_AutoApplied"))
            Me.txtVLEndBal.Text = Generic.ToDbl(xds.Tables(0).Rows(0)("VL_AutoEndBalance"))
            Me.txtSLApplied.Text = Generic.ToDbl(xds.Tables(0).Rows(0)("SL_AutoApplied"))
            Me.txtSLEndBal.Text = Generic.ToDbl(xds.Tables(0).Rows(0)("SL_AutoEndBalance"))
        End If
    End Sub

    Private Sub LeaveAvailed_Lookup()
        Dim xds As New DataSet
        xds = SQLHelper.ExecuteDataSet("[ELeaveMonetized_WebOne_Availed_Admin]", UserNo, Generic.ToInt(Me.hifEmployeeNo.Value), Me.txtTransDate.Text)
        If xds.Tables.Count > 0 Then
            Me.txtTotalYear_VLOnly.Text = Generic.ToDbl(xds.Tables(0).Rows(0)("VL"))
            Me.txtTotalYear_SLVL_VL.Text = Generic.ToDbl(xds.Tables(0).Rows(0)("SLVL_VL"))
            Me.txtTotalYear_SLVL_SL.Text = Generic.ToDbl(xds.Tables(0).Rows(0)("SLVL_SL"))
        End If
    End Sub

    Private Sub DisableLeavehrs()
        Dim xds As New DataSet
        Dim IsVisible1 As Boolean = False
        Dim IsVisible2 As Boolean = False
        Dim IsEnabledFieldVL As Boolean = True
        Dim IsEnabledFieldSL As Boolean = True
        Dim Msg As String = ""
        'If xBase.CheckDBNull(Me.cboLeavemonitizedtype.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType) = 1 Then
        '    Me.txtLeaveHrs.Enabled = False
        '    Me.txtLeaveHrs.Text = 0
        'Else
        '    Me.txtLeaveHrs.Enabled = True
        'End If
        'xds = sqlHelp.ExecuteDataset(clsConnectionString.xConSTR, "[ELeaveTypeMoneSP01]", xBase.CheckDBNull(Me.cboLeaveMonitizedTypeNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType))
        'If xds.Tables.Count > 0 Then
        '    Me.cboLeavetypeNo.DataSource = xds
        '    Me.cboLeavetypeNo.DataTextField = "tdesc"
        '    Me.cboLeavetypeNo.DataValueField = "tno"
        '    Me.cboLeavetypeNo.DataBind()
        'End If

        xds = SQLHelper.ExecuteDataSet("[ELeaveMonetype_WebOneSelect]", Generic.ToInt(Me.cboLeaveMonitizedTypeNo.SelectedValue))
        If xds.Tables.Count > 0 Then
            IsVisible1 = Generic.ToInt(xds.Tables(0).Rows(0)("IsVisible1"))
            IsVisible2 = Generic.ToInt(xds.Tables(0).Rows(0)("IsVisible2"))
            IsEnabledFieldVL = Generic.ToInt(xds.Tables(0).Rows(0)("IsEnabledFieldVL"))
            IsEnabledFieldSL = Generic.ToInt(xds.Tables(0).Rows(0)("IsEnabledFieldSL"))
            Msg = Generic.ToStr(xds.Tables(0).Rows(0)("Msg"))
            ViewState("IsVisible1") = IsVisible1
            ViewState("IsVisible2") = IsVisible2
            ViewState("IsEnabledFieldVL") = IsEnabledFieldVL
            ViewState("IsEnabledFieldSL") = IsEnabledFieldSL
        End If

        'Me.trSLBegBal.Visible = IsVisible1
        'Me.trSLApplied.Visible = IsVisible1
        'Me.trSLEndBal.Visible = IsVisible1
        'Me.trSLVLReason.Visible = IsVisible2
        fRegisterStartupScript("Sript", "disableenable_behind('" + IIf(ViewState("IsVisible2"), 1, 0).ToString + "');")
        Me.trSLVLReasonOthers.Visible = IsVisible2

        Me.cboLeaveMoneReasonTypeLNo.Enabled = IsVisible2
        Me.txtxRemarks.Enabled = IsVisible2
        Me.txtSLApplied.Enabled = IsVisible1

        Me.txtVLApplied.ReadOnly = Not IsEnabledFieldVL
        Me.txtSLApplied.ReadOnly = Not IsEnabledFieldSL

        If IsVisible1 Then
            Me.txtSLApplied.CssClass = "form-control required number"
        Else
            Me.txtSLApplied.CssClass = ""
            'CLEAR FIELDS.. .
            Me.txtSLApplied.Text = 0
            Me.txtSLEndBal.Text = 0
        End If

        If IsVisible2 Then
            Me.cboLeaveMoneReasonTypeLNo.CssClass = "form-control required"
            Me.txtxRemarks.CssClass = "form-control required"

            ''PANEL HEIGHT:
            'Me.pnlPopup.Height = 485
            'divPopupPnl1.Style.Item("height") = "460px"
        Else
            Me.cboLeaveMoneReasonTypeLNo.CssClass = ""
            Me.txtxRemarks.CssClass = ""

            ''PANEL HEIGHT:
            'Me.pnlPopup.Height = 340
            'divPopupPnl1.Style.Item("height") = "315px"

            'CLEAR FIELDS.. .
            Me.cboLeaveMoneReasonTypeLNo.Text = ""
            Me.txtxRemarks.Text = ""

        End If

        Me.lblMsgNotice2.Text = Msg

        cboLeaveMoneReasonTypeLNo_SelectedIndexChanged(Nothing, Nothing)

    End Sub

    Protected Sub ELeaveMonetized_WebValidate_Employee()
        Dim tSave As Integer = 0
        Dim xds As New DataSet
        Dim xMsg As String = "", Title As String = ""

        ELeaveMonetized_WebValidate_LWOPAWOL(xMsg, Title) 'TRAPPING FOR AWOL... .

        If xMsg > "" Then
            Dim url As String = "ctl00_cphBody_mdlShow"
            MessageBox.PopupMessage(xMsg, Me, url)
            fRegisterStartupScript("Sript", "disableenable_behind('" + IIf(ViewState("IsVisible2"), 1, 0).ToString + "');")
            Exit Sub
        Else
            'Try
            '    Generic.ClearControls(Me, "pnlPopup")
            'Catch ex As Exception

            'End Try

            'CLEAR FIELDS.. .
            'Me.txtCode.Text = ""
            'Me.txtLeaveMonitizedNo.Text = ""
            'Me.cboEmployeeNo.Text = ""
            'Me.txtTransDate.Text = ""
            'Me.cboLeaveMonitizedTypeNo.Text = ""
            'Me.txtVLBegBal.Text = 0
            Me.txtVLApplied.Text = 0.0
            Me.txtVLEndBal.Text = 0.0
            'Me.txtSLBegBal.Text = 0
            Me.txtSLApplied.Text = 0.0
            Me.txtSLEndBal.Text = 0.0
            Me.cboLeaveMoneReasonTypeLNo.Text = ""
            Me.txtxRemarks.Text = ""

            Dim Msg As String = ""
            Me.lblMsgNotice.Text = ELeaveMonitization_Msg(Msg, 1)
            Me.lblMsgNotice2.Text = ELeaveMonitization_Msg(Msg, 2)

            DisableLeavehrs()

            'GET THE BALANCE
            LeaveBalance_Lookup()
            LeaveAvailed_Lookup()

            If Me.txtTransDate.Text = "" Then
                Me.txtTransDate.Text = Format(Now(), "MM/dd/yyyy")
            End If

        End If

        mdlShow.Show()

    End Sub

    Protected Function ELeaveMonetized_WebValidate_LWOPAWOL(ByRef xMsg As String, ByRef xTitle As String) As String
        Dim tSave As Integer = 0
        Dim xds As New DataSet
        Dim Msg As String = "", IsValid As Boolean, Title As String = ""

        xds = SQLHelper.ExecuteDataSet("[ELeaveMonetized_WebValidate_LWOPAWOL_Admin]", UserNo, Generic.ToInt(Me.hifEmployeeNo.Value))
        If xds.Tables.Count > 0 Then
            IsValid = Generic.ToInt(xds.Tables(0).Rows(0)("IsValid"))
            Msg = Generic.ToStr(xds.Tables(0).Rows(0)("Msg"))
            Title = Generic.ToStr(xds.Tables(0).Rows(0)("Title"))
        End If
        xMsg = Msg '"Testing AWOL!" 'Msg
        xTitle = Title
        Return Msg
    End Function

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub

    Protected Sub lnkResubmit_Click(sender As Object, e As System.EventArgs)

        Dim lbl As New Label, tcheck As New CheckBox, txt As New TextBox
        Dim tcount As Integer, fcount As Integer = 0
        Dim RETVAL As Integer = 0
        Dim Remarks As String = ""

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"LeaveMonitizedNo"})

        Dim str As String = "", i As Integer = 0
        Dim IsRowSelected As Boolean

        For tcount = 0 To grdMain.VisibleRowCount - 1
            i = Generic.ToInt(grdMain.GetRowValues(tcount, New String() {"LeaveMonitizedNo"}))
            txt = CType(grdMain.FindRowCellTemplateControl(tcount, grdMain.DataColumns("yyRemarks"), "txtxRemarks"), TextBox)
            IsRowSelected = grdMain.Selection.IsRowSelected(tcount)
            Remarks = txt.Text

            If IsRowSelected = True Then
                RETVAL = i 'lbl.Text
                If RETVAL > 0 And 1 = 1 Then
                    Dim dsE As New DataSet
                    'dsE = SQLHelper.ExecuteDataSet("ELeaveMonitized_WebEmail", UserNo, RETVAL, 1)
                    If dsE.Tables.Count > 0 Then
                        If dsE.Tables(0).Rows.Count > 0 Then
                            Dim tbody As String = "", tsubject As String = "", tfrom As String = "", tto As String = ""
                            Dim clsE As New clsBase.clsEmail, IsWithApprover As Boolean, ApproverName As String = "", LeaveType As Integer = 0

                            'tbody = xBase.CheckDBNull(dsE.Tables(0).Rows(0)("Body"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            'tsubject = xBase.CheckDBNull(dsE.Tables(0).Rows(0)("Subject"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            'tfrom = xBase.CheckDBNull(dsE.Tables(0).Rows(0)("tfrom"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            'tto = xBase.CheckDBNull(dsE.Tables(0).Rows(0)("tTo"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                            IsWithApprover = Generic.ToInt(dsE.Tables(0).Rows(0)("IsWithApprover"))
                            'ApproverName = xBase.CheckDBNull(dsE.Tables(0).Rows(0)("ApproverName"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                            'LeaveType = xBase.CheckDBNull(dsE.Tables(0).Rows(0)("LeaveTypeNo"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                            If IsWithApprover = True Then
                                'sqlhelp.ExecuteNonQuery(clsConnectionString.xConSTR, "EAutoEmail_LeaveApplication", xPublicVar.xOnlineUseNo, tfrom, tto, tbody, ApproverName, RETVAL, LeaveType)
                                fcount = fcount + 1
                            Else
                                'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Error!','No Approver defined on your employee record.');")
                                MessageBox.Alert("No Approver defined on your employee record.", "Error!", Me)
                            End If

                        End If
                    End If
                End If

            End If
        Next

        'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Resubmit','There are (" + fcount.ToString + ")  transaction(s) successfully resubmitted.');")
        MessageBox.Success("(" + Remarks.ToString + ") " + MessageTemplate.SuccessUpdate, Me)

        If fcount > 0 Then
            PopulateGrid()
        End If

    End Sub

    Protected Sub lnkApproved_Click(sender As Object, e As System.EventArgs)

        Dim lbl As New Label, tcheck As New CheckBox, txt As New TextBox
        Dim tcount As Integer, fcount As Integer = 0

        Dim str As String = "", i As Integer = 0
        Dim IsRowSelected As Boolean

        For tcount = 0 To grdMain.VisibleRowCount - 1
            i = Generic.ToInt(grdMain.GetRowValues(tcount, New String() {"LeaveMonitizedNo"}))
            txt = CType(grdMain.FindRowCellTemplateControl(tcount, grdMain.DataColumns("yyRemarks"), "txtxRemarks"), TextBox)
            IsRowSelected = grdMain.Selection.IsRowSelected(tcount)
            If IsRowSelected = True Then
                ApproveTransaction(CType(i, Integer), Generic.ToStr(txt.Text), 2)
                fcount = fcount + 1
            End If
        Next

        'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Approved','There are (" + fcount.ToString + ")  transaction(s) successfully approved.');")
        MessageBox.Success("(" + fcount.ToString + ") " + MessageTemplate.SuccessApproved, Me)

        PopulateGrid()

    End Sub

    Protected Sub lnkDisApproved_Click(sender As Object, e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox, txt As New TextBox
        Dim tcount As Integer, fcount As Integer = 0

        Dim str As String = "", i As Integer = 0
        Dim IsRowSelected As Boolean

        For tcount = 0 To grdMain.VisibleRowCount - 1
            i = Generic.ToInt(grdMain.GetRowValues(tcount, New String() {"LeaveMonitizedNo"}))
            txt = CType(grdMain.FindRowCellTemplateControl(tcount, grdMain.DataColumns("yyRemarks"), "txtxRemarks"), TextBox)
            IsRowSelected = grdMain.Selection.IsRowSelected(tcount)
            If IsRowSelected = True Then
                ApproveTransaction(CType(i, Integer), Generic.ToStr(txt.Text), 3)
                fcount = fcount + 1
            End If
        Next

        'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Disapproved','There are (" + fcount.ToString + ")  transaction(s) successfully disapproved.');")
        MessageBox.Success("(" + fcount.ToString + ") " + MessageTemplate.SuccessDisapproved, Me)

        PopulateGrid()

    End Sub

    Private Sub ApproveTransaction(tId As Integer, remarks As String, approvalStatNo As Integer)
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("ELeaveMonitized_WebApproved", UserNo, tId, approvalStatNo, remarks, PayLocNo)
        If fds.Tables.Count > 0 Then
            If fds.Tables(0).Rows.Count > 0 Then
                Dim IsWithapprover As Boolean
                IsWithapprover = Generic.ToInt(fds.Tables(0).Rows(0)("IsWithApprover"))
                If IsWithapprover = True Then

                Else
                    'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Approver','Unable to locate the next approver.');")
                    MessageBox.Information("Unable to locate the next approver.", Me)
                End If
            End If
        End If
    End Sub

    Protected Sub lnkForwardPayroll_Click(sender As Object, e As System.EventArgs) 'Handles lnkForwardPayroll.Click
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            LeaveMone_ForwardPayroll()
            PopulateGrid()
        Else
            'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Denied Posting!','" + clsMessage.GetMessageType(Global.clsMessage.EnumMessageType.DeniedPost) + "');")
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
        'PopulateGrid()
    End Sub

    Protected Sub lnkPaid_Click(sender As Object, e As System.EventArgs) 'Handles lnkPaid.Click
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            LeaveMone_Paid()
            PopulateGrid()
        Else
            'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Denied Posting!','" + clsMessage.GetMessageType(Global.clsMessage.EnumMessageType.DeniedPost) + "');")
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
        'PopulateGrid()
    End Sub

    Protected Sub lnkCancel_Click(sender As Object, e As System.EventArgs) 'Handles lnkForwardPayroll.Click
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            LeaveMone_Cancel()
            PopulateGrid()
        Else
            'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Denied Posting!','" + clsMessage.GetMessageType(Global.clsMessage.EnumMessageType.DeniedPost) + "');")
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
        'PopulateGrid()
    End Sub

    Protected Sub ForProcessing_Click(sender As Object, e As System.EventArgs) 'Handles lnkForwardPayroll.Click
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            LeaveMone_ForProcessing()
            PopulateGrid()
        Else
            'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Denied Posting!','" + clsMessage.GetMessageType(Global.clsMessage.EnumMessageType.DeniedPost) + "');")
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
        'PopulateGrid()
    End Sub

    Protected Sub LeaveMone_ForwardPayroll()
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0
        Dim IsProcess As Boolean = False
        Dim fcount As Integer = 0
        Dim scount As Integer = 0

        Dim txt As New TextBox
        Dim str As String = "", i As Integer = 0
        Dim IsRowSelected As Boolean

        For tcount = 0 To grdMain.VisibleRowCount - 1
            i = Generic.ToInt(grdMain.GetRowValues(tcount, New String() {"LeaveMonitizedNo"}))
            'txt = CType(grdMain.FindRowCellTemplateControl(tcount, grdMain.DataColumns("yyRemarks"), "txtxRemarks"), TextBox)
            IsRowSelected = grdMain.Selection.IsRowSelected(tcount)
            If IsRowSelected = True Then
                scount = scount + 1
                fcount = fcount + SQLHelper.ExecuteNonQuery("ELeaveMonitized_WebForwardPayroll", UserNo, Generic.ToInt(i))
            End If
        Next

        If fcount > 0 Then
            'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Forward to Payroll','" + "There are (" + scount.ToString + ") transaction(s) forwarded to payroll." + "<br>');")
            MessageBox.Success("(" + scount.ToString + ") " + "transaction(s) forwarded to payroll.", Me)
        Else
            If scount = 0 Then
                'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Forward to Payroll','" + "There are no transaction(s) forwarded to payroll." + "<br>Please select transaction(s).');")
                MessageBox.Success("There are no transaction(s) forwarded to payroll.", Me)
            End If
        End If

    End Sub

    Protected Sub LeaveMone_Paid()
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0
        Dim IsProcess As Boolean = False
        Dim fcount As Integer = 0
        Dim scount As Integer = 0
        Dim xStr$ = ""
        Dim xTxt As New TextBox

        Dim str As String = "", i As Integer = 0
        Dim IsRowSelected As Boolean

        For tcount = 0 To grdMain.VisibleRowCount - 1
            i = Generic.ToInt(grdMain.GetRowValues(tcount, New String() {"LeaveMonitizedNo"}))
            xTxt = CType(grdMain.FindRowCellTemplateControl(tcount, grdMain.DataColumns("yyRemarks"), "txtxRemarks"), TextBox)
            IsRowSelected = grdMain.Selection.IsRowSelected(tcount)
            If Not xTxt Is Nothing Then
                xStr$ = xTxt.Text
            End If
            If IsRowSelected = True Then
                scount = scount + 1
                fcount = fcount + SQLHelper.ExecuteNonQuery("ELeaveMonitized_Web_UpdatePaid", UserNo, Generic.ToInt(i), xStr$)
            End If
        Next

        If fcount > 0 Then
            'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Tag as Paid','" + "There are (" + scount.ToString + ") transaction(s) have been tagged as Paid." + "<br>');")
            MessageBox.Success("There are (" + scount.ToString + ") transaction(s) have been tagged as Paid.", Me)
        Else
            If scount = 0 Then
                'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Tag as Paid','" + "There are no transaction(s) tagged as Paid." + "<br>Please select transaction(s).');")
                MessageBox.Success("There are no transaction(s) tagged as Paid.", Me)
            End If
        End If

    End Sub

    Protected Sub LeaveMone_Cancel()
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0
        Dim IsProcess As Boolean = False
        Dim fcount As Integer = 0
        Dim scount As Integer = 0
        Dim xStr$ = ""
        Dim xTxt As New TextBox

        Dim str As String = "", i As Integer = 0
        Dim IsRowSelected As Boolean

        For tcount = 0 To grdMain.VisibleRowCount - 1
            i = Generic.ToInt(grdMain.GetRowValues(tcount, New String() {"LeaveMonitizedNo"}))
            xTxt = CType(grdMain.FindRowCellTemplateControl(tcount, grdMain.DataColumns("yyRemarks"), "txtxRemarks"), TextBox)
            IsRowSelected = grdMain.Selection.IsRowSelected(tcount)
            If Not xTxt Is Nothing Then
                xStr$ = xTxt.Text
            End If
            If IsRowSelected = True Then
                scount = scount + 1
                fcount = fcount + SQLHelper.ExecuteNonQuery("ELeaveMonitized_WebCancel", UserNo, Generic.ToInt(lbl.Text), xStr$)
            End If
        Next

        If fcount > 0 Then
            'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Cancellation','" + "There are (" + scount.ToString + ") transaction(s) has been cancelled." + "<br>');")
            MessageBox.Success("There are (" + scount.ToString + ") transaction(s) has been cancelled.", Me)
        Else
            If scount = 0 Then
                'fRegisterStartupScript("JSDialogMessage", "dialogMessage('Cancellation','" + "There are no transaction(s) has been cancelled." + "<br>Please select transaction(s).');")
                MessageBox.Success("There are no transaction(s) has been cancelled.", Me)
            End If
        End If

    End Sub

    Protected Sub LeaveMone_ForProcessing()
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, DeleteCount As Integer = 0
        Dim IsProcess As Boolean = False
        Dim fcount As Integer = 0
        Dim scount As Integer = 0
        Dim xTxt As New TextBox

        Dim str As String = "", i As Integer = 0
        Dim IsRowSelected As Boolean

        For tcount = 0 To grdMain.VisibleRowCount - 1
            i = Generic.ToInt(grdMain.GetRowValues(tcount, New String() {"LeaveMonitizedNo"}))
            xTxt = CType(grdMain.FindRowCellTemplateControl(tcount, grdMain.DataColumns("yyRemarks"), "txtxRemarks"), TextBox)
            IsRowSelected = grdMain.Selection.IsRowSelected(tcount)
            If IsRowSelected = True Then
                scount = scount + 1
                fcount = fcount + SQLHelper.ExecuteNonQuery("ELeaveMonitized_WebForProcessing", UserNo, Generic.ToInt(i))
            End If
        Next

        If fcount > 0 Then
            'fRegisterStartupScript("JSDialogMessage", "dialogMessage('For Processing','" + "There are (" + scount.ToString + ") transaction(s) has been forwarded for processing." + "<br>');")
            MessageBox.Success("There are (" + scount.ToString + ") transaction(s) has been forwarded for processing.", Me)
        Else
            If scount = 0 Then
                'fRegisterStartupScript("JSDialogMessage", "dialogMessage('For Processing','" + "There are no transaction(s) has been forwarded for processing." + "<br>Please select transaction(s).');")
                MessageBox.Success("There are no transaction(s) has been forwarded for processing.", Me)
            End If
        End If

    End Sub


#End Region

#Region "Details"

    Private Sub PopulateDetl()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ELeaveMonitized_Web_Details", UserNo, Generic.ToInt(ViewState("TransNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()
        Catch ex As Exception

        End Try
    End Sub

#End Region


End Class
