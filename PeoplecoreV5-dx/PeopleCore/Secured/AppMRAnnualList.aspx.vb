Imports clsLib
Imports System.Data
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_AppMRAnnualList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()
            'PopulateGrid()
        End If
        PopulateGridDept()

        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub


#Region "Main"
    Protected Sub PopulateGrid(ByVal MRAnnualMainNo As Integer)
        Try
            Dim StatNo As Integer
            Dim Stat2No As Integer

            StatNo = Generic.ToInt(cboTabNo.SelectedValue)
            Stat2No = Generic.ToInt(cboTab2No.SelectedValue)

            If StatNo = 4 Then
                If Stat2No = 1 Then
                    lnkPost.Visible = True
                Else
                    lnkPost.Visible = False
                End If
            Else
                lnkPost.Visible = False
            End If

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRAnnual_Web", UserNo, MRAnnualMainNo, Stat2No, PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 45)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboTab2No.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 1)
            cboTab2No.DataTextField = "tDesc"
            cboTab2No.DataValueField = "tno"
            cboTab2No.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboPlantillaNo.DataSource = SQLHelper.ExecuteDataSet("EPlantilla_WebLookup", UserNo, Generic.ToInt(cboPositionNo.SelectedValue), PayLocNo)
            cboPlantillaNo.DataValueField = "tNo"
            cboPlantillaNo.DataTextField = "tDesc"
            cboPlantillaNo.DataBind()

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub cboPlantillaNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPlantilla_WebOne", UserNo, Generic.ToInt(cboPlantillaNo.SelectedValue))
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next

        mdlMain.Show()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Try

                Dim lnk As New LinkButton, i As Integer, IsEnabled As Boolean = False
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"MRAnnualNo", "IsEnabled"})
                i = Generic.ToInt(obj(0))
                IsEnabled = Generic.ToBol(obj(1))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EMRAnnual_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "Panel1", dt)
                Next
                EnableDisable()
                mdlMain.Show()

            Catch ex As Exception
            End Try
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub EnableDisable()
        Generic.EnableControls(Me, "Panel1", False)
        cboQuarterNo.Enabled = Generic.ToBol(ViewState("IsEnabled"))
        cboMRReasonNo.Enabled = Generic.ToBol(ViewState("IsEnabled"))
    End Sub
    
    Protected Sub grdDept_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdDept.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdDept.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        ViewState("TransNo") = 0
        PopulateGridDept()
    End Sub

    Protected Sub lnkSearch2_Click(sender As Object, e As EventArgs)
        PopulateGrid(Generic.ToInt(ViewState("TransNo")))
    End Sub

    
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim MRAnnualNo As Integer = Generic.ToInt(Me.txtMRAnnualNo.Text)
        Dim RequestedByNo As Integer = Generic.ToInt(hifRequestedByNo.Value)
        Dim RequestedDate As String = Generic.ToStr(txtRequestedDate.Text)
        Dim MRTypeNo As Integer = Generic.ToInt(cboMRTypeNo.SelectedValue)
        Dim PlantillaNo As Integer = Generic.ToInt(cboPlantillaNo.SelectedValue)
        Dim PlantillaGovTypeNo As Integer = Generic.ToInt(cboPlantillaGovTypeNo.SelectedValue)
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim PositionNo As Integer = Generic.ToInt(cboPositionNo.SelectedValue)
        Dim FacilityNo As Integer = Generic.ToInt(cboFacilityNo.SelectedValue)
        Dim DivisionNo As Integer = Generic.ToInt(cboDivisionNo.SelectedValue)
        Dim DepartmentNo As Integer = Generic.ToInt(cboDepartmentNo.SelectedValue)
        Dim SectionNo As Integer = Generic.ToInt(cboSectionNo.SelectedValue)
        Dim UnitNo As Integer = Generic.ToInt(cboUnitNo.SelectedValue)
        Dim GroupNo As Integer = Generic.ToInt(cboGroupNo.SelectedValue)
        Dim CostCenterNo As Integer = Generic.ToInt(cboCostCenterNo.SelectedValue)
        Dim JobGradeNo As Integer = Generic.ToInt(cboJobGradeNo.SelectedValue)
        Dim LocationNo As Integer = Generic.ToInt(cboLocationNo.SelectedValue)
        Dim QuarterNo As Integer = Generic.ToInt(cboQuarterNo.SelectedValue)
        Dim ApplicableYear As Integer = Generic.ToInt(txtApplicableYear.Text)
        Dim MRReasonNo As Integer = Generic.ToInt(cboMRReasonNo.SelectedValue)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable

        Dim Str As String = UserNo & ", " & MRAnnualNo & ", " & Generic.ToInt(ViewState("TransNo")) & ", " & RequestedByNo & ", " & RequestedDate & ", " & PlantillaNo & ", " & PositionNo & ", " & PlantillaGovTypeNo & ", " & EmployeeNo & ", " & MRTypeNo & ", " & JobGradeNo & ", " & FacilityNo & ", " & UnitNo & ", " & DepartmentNo & ", " & GroupNo & ", " & DivisionNo & ", " & SectionNo & ", " & CostCenterNo & ", " & LocationNo & ", " & ApplicableYear & ", " & QuarterNo & ", " & MRReasonNo & ", " & PayLocNo

        dtx = SQLHelper.ExecuteDataTable("EMRAnnual_WebValidate", UserNo, MRAnnualNo, Generic.ToInt(ViewState("TransNo")), RequestedByNo, RequestedDate, PlantillaNo, PositionNo, PlantillaGovTypeNo, EmployeeNo, MRTypeNo, JobGradeNo, FacilityNo, UnitNo, DepartmentNo, GroupNo, DivisionNo, SectionNo, CostCenterNo, LocationNo, ApplicableYear, QuarterNo, MRReasonNo, PayLocNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlMain.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EMRAnnual_WebSave", UserNo, MRAnnualNo, Generic.ToInt(ViewState("TransNo")), RequestedByNo, RequestedDate, PlantillaNo, PositionNo, PlantillaGovTypeNo, EmployeeNo, MRTypeNo, JobGradeNo, FacilityNo, UnitNo, DepartmentNo, GroupNo, DivisionNo, SectionNo, CostCenterNo, LocationNo, ApplicableYear, QuarterNo, MRReasonNo, PayLocNo) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGridDept()
            'PopulateGrid(Generic.ToInt(ViewState("TransNo")))
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If


    End Sub

#End Region

#Region "Department"
    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"MRAnnualMainNo", "Code", "IsEnabled"})
        ViewState("TransNo") = obj(0)
        'lbl.Text = "Transaction No. : " & obj(1)
        ViewState("IsEnabled") = obj(2)
        PopulateGrid(Generic.ToInt(ViewState("TransNo")))
    End Sub

    Protected Sub PopulateGridDept()
        Try
            If Generic.ToInt(cboTabNo.SelectedValue) = 1 Then
                lnkSubmit.Visible = True
                lnkApproved.Visible = False
                lnkDisapproved.Visible = False
            ElseIf Generic.ToInt(cboTabNo.SelectedValue) = 2 Then
                lnkSubmit.Visible = False
                lnkApproved.Visible = False
                lnkDisapproved.Visible = False
            ElseIf Generic.ToInt(cboTabNo.SelectedValue) = 3 Then
                lnkSubmit.Visible = False
                lnkApproved.Visible = True
                lnkDisapproved.Visible = True
            Else
                lnkSubmit.Visible = False
                lnkApproved.Visible = False
                lnkDisapproved.Visible = False
            End If

            ViewState("TransNo") = 0

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMRAnnualMain_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdDept.DataSource = dt
            grdDept.DataBind()

            If ViewState("TransNo") = 0 Then
                Dim obj As Object() = grdDept.GetRowValues(grdMain.VisibleStartIndex(), New String() {"MRAnnualMainNo", "Code"})
                ViewState("TransNo") = obj(0)
                'lbl.Text = "Transaction No. : " & obj(1)
            End If

            PopulateGrid(Generic.ToInt(ViewState("TransNo")))

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSubmit_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdDept.GetSelectedFieldValues(New String() {"MRAnnualMainNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            If SQLHelper.ExecuteNonQuery("EMRAnnualMain_WebSubmit", UserNo, item, 1) > 0 Then
                i = i + 1
            End If
        Next
        If i > 0 Then
            ViewState("TransNo") = 0
            MessageBox.Success("(" + i.ToString + ") record(s) successfully submit for review.", Me)
            PopulateGridDept()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

    Protected Sub lnkPost_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"MRAnnualNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                If SQLHelper.ExecuteNonQuery("EMRAnnual_WePost", UserNo, item, PayLocNo) > 0 Then
                    Count = Count + 1
                End If
                i = i + 1
            Next

            If i > 0 Then
                ViewState("TransNo") = 0
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccesPost, Me)
                PopulateGridDept()
            Else
                MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If

    End Sub

    Protected Sub lnkApproved_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdDept.GetSelectedFieldValues(New String() {"MRAnnualMainNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, 2, False)
            i = i + 1
        Next
        If i > 0 Then
            ViewState("TransNo") = 0
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me)
            PopulateGridDept()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

    Protected Sub lnkDisapproved_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdDept.GetSelectedFieldValues(New String() {"MRAnnualMainNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, 3, False)
            i = i + 1
        Next
        If i > 0 Then
            ViewState("TransNo") = 0
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDisapproved, Me)
            PopulateGridDept()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

    Private Sub ApproveTransaction(tId As Integer, approvalStatNo As Integer, isSubmitforApp As Boolean)
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("EMRAnnualMain_WebApproved", UserNo, tId, approvalStatNo, isSubmitforApp)
        If fds.Tables.Count > 0 Then
            If fds.Tables(0).Rows.Count > 0 Then
                Dim IsWithapprover As Boolean
                IsWithapprover = Generic.CheckDBNull(fds.Tables(0).Rows(0)("IsWithApprover"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                If IsWithapprover = True Then

                Else
                    MessageBox.Information("Unable to locate the next approver.", Me)
                End If
            End If


        End If
    End Sub
#End Region


End Class
