Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class SecuredSelf_SelfBenefitEnrollment
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim EmployeeNo As Integer = 0

    Dim Required As String = "form-control required"
    Dim NotRequired As String = "form-control"

    Dim RequiredNumber As String = "form-control number required"
    Dim NotRequiredNumber As String = "form-control number"

    Protected Sub PopulateGrid()

        Try
            Dim TabNo As Integer
            TabNo = Generic.ToInt(cboTabNo.SelectedValue)
            lnkAdd.Visible = False
            lnkDelete.Visible = False
            lnkExport.Visible = True

            If TabNo = 1 Then
                lnkAdd.Visible = True
                lnkDelete.Visible = True
            End If

            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EBenefitEnrollment_WebSelf", UserNo, TabNo, PayLocNo)
            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBenefitEnrollment_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))
                Generic.PopulateData(Me, "Panel1", dt)
                PopulateDependent1(EmployeeNo, Generic.ToInt(cboBenefitHMOPlanTypeNo2.SelectedValue), 0, Generic.ToInt(cboEmployeeDepeNo2.SelectedValue))
                PopulateDependent2(EmployeeNo, Generic.ToInt(cboBenefitHMOPlanTypeNo3.SelectedValue), Generic.ToInt(cboEmployeeDepeNo1.SelectedValue), 0)

                Try
                    Me.cboEmployeeDepeNo1.Text = Generic.ToStr(row("EmployeeDepeNo1"))
                    Me.cboEmployeeDepeNo2.Text = Generic.ToStr(row("EmployeeDepeNo2"))
                Catch ex As Exception

                End Try

            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        EmployeeNo = Generic.ToInt(Session("EmployeeNo"))

        Permission.IsAuthenticated()

        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)


        ClearData()
        lnkSave.Visible = True
        EnableDisable(True)
        ModalPopupExtender1.Show()
 
    End Sub

    Protected Sub ClearData()
        Generic.ClearControls(Me, "Panel1")
    End Sub

    Protected Sub InitializedData()
        cboBenefitHMOTypeNo.Text = ""

        cboBenefitHMOPlanTypeNo2.Text = ""
        cboBenefitHMOPlanTypeNo3.Text = ""

        cboBenefitHMOPlanNo2.Text = ""
        cboBenefitHMOPlanNo3.Text = ""

        cboEmployeeDepeNo1.Text = ""
        cboEmployeeDepeNo2.Text = ""

        txtAmount2.Text = ""
        txtAmount3.Text = ""

        txtExcess2.Text = ""
        txtExcess3.Text = ""

        txtTotalCost.Text = ""

    End Sub

    Protected Sub EnableDisable(ByVal IsEnabled As Boolean)

        Generic.EnableControls(Me, "Panel1", IsEnabled)

        txtAmount2.Enabled = False
        txtAmount3.Enabled = False

        txtExcess2.Enabled = False
        txtExcess3.Enabled = False

        txtTotalCost.Enabled = False

        cboEmployeeDepeNo1.Enabled = IsEnabled
        cboEmployeeDepeNo2.Enabled = IsEnabled

        cboBenefitHMOPlanNo2.Enabled = False
        cboBenefitHMOPlanNo3.Enabled = False


    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EBenefitEnrollment_WebValidate", UserNo, Generic.ToInt(txtCode.Text), EmployeeNo, Generic.ToStr(txtDateFiled.Text), Generic.ToInt(cboBenefitTypeNo.SelectedValue), Generic.ToInt(cboEmployeeDepeNo1.SelectedValue), Generic.ToInt(cboEmployeeDepeNo2.SelectedValue), 0, PayLocNo, Generic.ToInt(cboBenefitHMOTypeNo.SelectedValue))

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("RetVal"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        If SaveRecord() Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Private Function SaveRecord() As Boolean

        Dim DateFiled As String = Generic.ToStr(txtDateFiled.Text)
        Dim BenefitTypeNo As Integer = Generic.ToInt(cboBenefitTypeNo.SelectedValue)
        Dim BenefitCateNo As Integer = Generic.ToInt(hifBenefitCateNo.Value)
        Dim Remarks As String = ""

        Dim BenefitHMOTypeNo As Integer = Generic.ToInt(cboBenefitHMOTypeNo.SelectedValue)
        Dim BenefitHMOPlanTypeNo2 As Integer = Generic.ToInt(cboBenefitHMOPlanTypeNo2.SelectedValue)
        Dim BenefitHMOPlanNo2 As Integer = Generic.ToInt(cboBenefitHMOPlanNo2.SelectedValue)
        Dim BenefitHMOPlanTypeNo3 As Integer = Generic.ToInt(cboBenefitHMOPlanTypeNo2.SelectedValue)
        Dim BenefitHMOPlanNo3 As Integer = Generic.ToInt(cboBenefitHMOPlanNo2.SelectedValue)
        Dim Amount2 As Double = Generic.ToDec(txtAmount2.Text)
        Dim Excess2 As Double = Generic.ToDec(txtExcess2.Text)
        Dim Amount3 As Double = Generic.ToDec(txtAmount3.Text)
        Dim Excess3 As Double = Generic.ToDec(txtExcess3.Text)
        Dim EmployeeDepeNo1 As Integer = Generic.ToInt(cboEmployeeDepeNo1.SelectedValue)
        Dim EmployeeDepeNo2 As Integer = Generic.ToInt(cboEmployeeDepeNo2.SelectedValue)

        Dim PHFee1 As Double = Generic.ToDec(txtPHFee1.Text)
        Dim PHFee2 As Double = Generic.ToDec(txtPHFee2.Text)
        Dim IsPHRider1 As Boolean = Generic.ToBol(chkIsPHRider1.Checked)
        Dim IsPHRider2 As Boolean = Generic.ToBol(chkIsPHRider2.Checked)

        Dim ds As New DataSet
        ds = SQLHelper.ExecuteDataSet("EBenefitEnrollment_WebSaveSelf", UserNo, Generic.ToInt(txtCode.Text), EmployeeNo, DateFiled, BenefitTypeNo, BenefitCateNo, Remarks, PayLocNo)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                ViewState("TransNo") = Generic.ToInt(ds.Tables(0).Rows(0)("RetVal"))
            End If
        End If

        Dim Str As String = UserNo & ", " & TransNo & ", " & BenefitHMOTypeNo & ", " & BenefitHMOPlanTypeNo2 & ", " & BenefitHMOPlanNo2 & ", " & BenefitHMOPlanTypeNo3 & ", " & BenefitHMOPlanNo3 & ", " & Amount2 & ", " & Excess2 & ", " & Amount3 & ", " & Excess3 & ", " & EmployeeDepeNo1 & ", " & EmployeeDepeNo2 & ", " & PayLocNo

        If SQLHelper.ExecuteNonQuery("EBenefitEnrollmentHMO_WebSaveSelf", UserNo, Generic.ToInt(ViewState("TransNo")), BenefitHMOTypeNo, BenefitHMOPlanTypeNo2, BenefitHMOPlanNo2, BenefitHMOPlanTypeNo3, BenefitHMOPlanNo3, Amount2, Excess2, Amount3, Excess3, EmployeeDepeNo1, EmployeeDepeNo2, PayLocNo, PHFee1, PHFee2, IsPHRider1, IsPHRider2) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If

    End Function

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton
        lnk = sender

        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        ClearData()
        Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
        Dim xIsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"xIsEnabled"}))
        EnableDisable(IsEnabled)
        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitEnrollmentNo"})))
        Me.lnkSave.Enabled = xIsEnabled
        If Generic.ToInt(cboBenefitHMOPlanTypeNo2.SelectedValue) = 0 Then
            Me.chkIsPHRider1.Checked = False
            Me.chkIsPHRider1.Enabled = False
        Else
            Me.chkIsPHRider1.Enabled = True
        End If

        If Generic.ToInt(cboBenefitHMOPlanTypeNo3.SelectedValue) = 0 Then
            Me.chkIsPHRider2.Checked = False
            Me.chkIsPHRider2.Enabled = False
        Else
            Me.chkIsPHRider2.Enabled = True
        End If

        If IsEnabled = False Then
            Me.chkIsPHRider1.Enabled = False
            Me.chkIsPHRider2.Enabled = False
        End If

        ModalPopupExtender1.Show()

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BenefitEnrollmentNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EBenefitEnrollment", UserNo, item)
            Generic.DeleteRecordAuditCol("EBenefitEnrollmentHMO", UserNo, "BenefitEnrollmentNo", item)
            i = i + 1
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub


    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkAttachment_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Response.Redirect("~/secured/frmFileUpload.aspx?id=" & Generic.Split(lnk.CommandArgument, 0) & "&display=" & Generic.Split(lnk.CommandArgument, 1))
    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))

        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("EBenefitStat_WebLookup")
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboBenefitTypeNo.DataSource = SQLHelper.ExecuteDataSet("EBenefitType_WebLookup", UserNo, Generic.ToInt(cboBenefitTypeNo.SelectedValue), True, PayLocNo)
            cboBenefitTypeNo.DataTextField = "tDesc"
            cboBenefitTypeNo.DataValueField = "tno"
            cboBenefitTypeNo.DataBind()
        Catch ex As Exception
        End Try

    End Sub




    Protected Sub cboBenefitHMOTypeNo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboBenefitHMOTypeNo.SelectedIndexChanged
        Dim dt As New DataTable
        Dim BenefitHMOPlanTypeNo As Integer

        'Try
        '    cboBenefitHMOPlanTypeNo2.Text = "2"
        '    cboBenefitHMOPlanTypeNo3.Text = "2"
        'Catch ex As Exception

        'End Try

        Try
            dt = SQLHelper.ExecuteDataTable("EBenefitHMOPlanType_WebOneDefault", UserNo, 0, EmployeeNo, PayLocNo)
            For Each row As DataRow In dt.Rows
                BenefitHMOPlanTypeNo = Generic.ToInt(row("BenefitHMOPlanTypeNo"))
            Next

            cboBenefitHMOPlanTypeNo2.Text = BenefitHMOPlanTypeNo.ToString '"5"
            cboBenefitHMOPlanTypeNo3.Text = BenefitHMOPlanTypeNo.ToString '"5"

            PopulateDependent1(EmployeeNo, Generic.ToInt(cboBenefitHMOPlanTypeNo2.SelectedValue), 0, Generic.ToInt(cboEmployeeDepeNo2.SelectedValue))
            PopulateDependent2(EmployeeNo, Generic.ToInt(cboBenefitHMOPlanTypeNo3.SelectedValue), Generic.ToInt(cboEmployeeDepeNo1.SelectedValue), 0)

        Catch ex As Exception

        End Try



        PopulateDropdownHMO()
        PopulatePlan()
        ModalPopupExtender1.Show()
    End Sub

    Private Sub PopulateDropdownHMO()

        Dim ds2 As New DataSet
        Dim ds3 As New DataSet

        ds2 = SQLHelper.ExecuteDataSet("EBenefitHMOPlan_WebLookup_Principal", UserNo, Generic.ToInt(cboBenefitHMOPlanTypeNo2.SelectedValue), Generic.ToInt(cboBenefitHMOTypeNo.SelectedValue), PayLocNo)
        ds3 = SQLHelper.ExecuteDataSet("EBenefitHMOPlan_WebLookup_Principal", UserNo, Generic.ToInt(cboBenefitHMOPlanTypeNo3.SelectedValue), Generic.ToInt(cboBenefitHMOTypeNo.SelectedValue), PayLocNo)

        Try
            cboBenefitHMOPlanNo2.DataSource = ds2
            cboBenefitHMOPlanNo2.DataTextField = "tDesc"
            cboBenefitHMOPlanNo2.DataValueField = "tNo"
            cboBenefitHMOPlanNo2.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboBenefitHMOPlanNo3.DataSource = ds3
            cboBenefitHMOPlanNo3.DataTextField = "tDesc"
            cboBenefitHMOPlanNo3.DataValueField = "tNo"
            cboBenefitHMOPlanNo3.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PopulatePlan()
        Try

            txtAmount2.Text = ""
            txtAmount3.Text = ""
            txtExcess2.Text = ""
            txtExcess3.Text = ""

            txtPHFee1.Text = ""
            txtPHFee2.Text = ""

            Try
                Dim ds2 As New DataSet
                ds2 = SQLHelper.ExecuteDataSet("EBenefitHMOPlan_WebLookup_Principal", UserNo, Generic.ToInt(cboBenefitHMOPlanTypeNo2.SelectedValue), Generic.ToInt(cboBenefitHMOTypeNo.SelectedValue), PayLocNo)
                If ds2.Tables.Count > 0 Then
                    If ds2.Tables(0).Rows.Count > 0 Then
                        cboBenefitHMOPlanNo2.Text = Generic.ToStr(ds2.Tables(0).Rows(1)("tNo"))
                    End If
                End If

                Dim dt2 As DataTable
                dt2 = SQLHelper.ExecuteDataTable("EBenefitHMOPlan_WebOne", UserNo, Generic.ToInt(cboBenefitHMOPlanNo2.SelectedValue))
                For Each row As DataRow In dt2.Rows
                    txtAmount2.Text = Generic.ToDec(row("AddPremiumCost"))
                    txtExcess2.Text = Generic.ToDec(row("ActualCost"))

                    txtPHFee1.Text = Generic.ToDec(row("PHFee"))

                Next
            Catch ex As Exception

            End Try

            Try

                If Generic.ToInt(cboBenefitHMOPlanTypeNo2.SelectedValue) = 0 Then
                    Me.chkIsPHRider1.Checked = False
                    Me.chkIsPHRider1.Enabled = False
                Else
                    Me.chkIsPHRider1.Enabled = True
                End If

                If Generic.ToInt(cboBenefitHMOPlanTypeNo3.SelectedValue) = 0 Then
                    Me.chkIsPHRider2.Checked = False
                    Me.chkIsPHRider2.Enabled = False
                Else
                    Me.chkIsPHRider2.Enabled = True
                End If

                Dim ds3 As New DataSet
                ds3 = SQLHelper.ExecuteDataSet("EBenefitHMOPlan_WebLookup_Principal", UserNo, Generic.ToInt(cboBenefitHMOPlanTypeNo3.SelectedValue), Generic.ToInt(cboBenefitHMOTypeNo.SelectedValue), PayLocNo)

                If ds3.Tables.Count > 0 Then
                    If ds3.Tables(0).Rows.Count > 0 Then
                        cboBenefitHMOPlanNo3.Text = Generic.ToStr(ds3.Tables(0).Rows(1)("tNo"))
                    End If
                End If

                Dim dt3 As DataTable
                dt3 = SQLHelper.ExecuteDataTable("EBenefitHMOPlan_WebOne", UserNo, Generic.ToInt(cboBenefitHMOPlanNo3.SelectedValue))
                For Each row As DataRow In dt3.Rows
                    txtAmount3.Text = Generic.ToDec(row("AddPremiumCost"))
                    txtExcess3.Text = Generic.ToDec(row("ActualCost"))

                    txtPHFee2.Text = Generic.ToDec(row("PHFee"))

                Next
            Catch ex As Exception

            End Try

            Dim PHFee1 As Decimal = 0, PHFee2 As Decimal = 0
            If Me.chkIsPHRider1.Checked Then
                PHFee1 = Generic.ToDec(txtPHFee1.Text)
            End If
            If Me.chkIsPHRider2.Checked Then
                PHFee2 = Generic.ToDec(txtPHFee2.Text)
            End If

            txtTotalCost.Text = Generic.ToDec(txtExcess2.Text) + Generic.ToDec(txtExcess3.Text) + PHFee1 + PHFee2
        Catch ex As Exception

        End Try


    End Sub

    Private Sub PopulateDependent1(ByVal EmpNo As Integer, ByVal HMOPlanTypeNo As Integer, ByVal DepeNo1 As Integer, ByVal DepeNo2 As Integer)

        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EEmployeeDepe_WebLookup", UserNo, EmpNo, HMOPlanTypeNo, DepeNo1, DepeNo2)

            cboEmployeeDepeNo1.DataSource = ds
            cboEmployeeDepeNo1.DataTextField = "tDesc"
            cboEmployeeDepeNo1.DataValueField = "tNo"
            cboEmployeeDepeNo1.DataBind()


            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    cboEmployeeDepeNo1.Text = Generic.ToStr(ds.Tables(0).Rows(1)("tNo"))
                End If
            End If


        Catch ex As Exception

        End Try

    End Sub

    Private Sub PopulateDependent2(ByVal EmpNo As Integer, ByVal HMOPlanTypeNo As Integer, ByVal DepeNo1 As Integer, ByVal DepeNo2 As Integer)

        Try
            Dim ds As New DataSet
            ds = SQLHelper.ExecuteDataSet("EEmployeeDepe_WebLookup", UserNo, EmpNo, HMOPlanTypeNo, DepeNo1, DepeNo2)

            cboEmployeeDepeNo2.DataSource = ds
            cboEmployeeDepeNo2.DataTextField = "tDesc"
            cboEmployeeDepeNo2.DataValueField = "tNo"
            cboEmployeeDepeNo2.DataBind()


            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    cboEmployeeDepeNo2.Text = Generic.ToStr(ds.Tables(0).Rows(1)("tNo"))
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub cboBenefitHMOPlanTypeNo2_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboBenefitHMOPlanTypeNo2.SelectedIndexChanged
        PopulateDependent1(EmployeeNo, Generic.ToInt(cboBenefitHMOPlanTypeNo2.SelectedValue), 0, Generic.ToInt(cboEmployeeDepeNo2.SelectedValue))
        PopulateDropdownHMO()
        PopulatePlan()
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub cboBenefitHMOPlanTypeNo3_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboBenefitHMOPlanTypeNo3.SelectedIndexChanged
        PopulateDependent2(EmployeeNo, Generic.ToInt(cboBenefitHMOPlanTypeNo3.SelectedValue), Generic.ToInt(cboEmployeeDepeNo1.SelectedValue), 0)
        PopulateDropdownHMO()
        PopulatePlan()
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub TotalCostUpdate()
        Dim PHFee1 As Decimal = 0, PHFee2 As Decimal = 0
        If Me.chkIsPHRider1.Checked Then
            PHFee1 = Generic.ToDec(txtPHFee1.Text)
        End If
        If Me.chkIsPHRider2.Checked Then
            PHFee2 = Generic.ToDec(txtPHFee2.Text)
        End If

        txtTotalCost.Text = Generic.ToDec(txtExcess2.Text) + Generic.ToDec(txtExcess3.Text) + PHFee1 + PHFee2

        ModalPopupExtender1.Show()

    End Sub

End Class
