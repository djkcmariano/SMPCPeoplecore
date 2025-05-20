Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class SecuredManager_BenBenefitApplicationHMODepe
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim employeeno As Int64 = 0

    Protected Sub PopulateGrid()

        Try
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EBenefitApplicationHMODep_Web", UserNo, TransNo)
            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EBenefitApplicationHMODep_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        employeeno = Generic.ToInt(Request.QueryString("employeeNo"))
        Permission.IsAuthenticated()
        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, Session("xPayLocNo"))

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList_Self(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboEmployeedepeno.DataSource = SQLHelper.ExecuteDataSet("EEmployeeDepe_WebLookup", UserNo, employeeno)
            cboEmployeedepeno.DataTextField = "tDesc"
            cboEmployeedepeno.DataValueField = "tNo"
            cboEmployeedepeno.DataBind()
        Catch ex As Exception

        End Try
        Try
            cboBenefitHMOPlanTypeNo.DataSource = SQLHelper.ExecuteDataSet("EBenefitHMOPlanType_WebLookup_Depe", UserNo)
            cboBenefitHMOPlanTypeNo.DataTextField = "tDesc"
            cboBenefitHMOPlanTypeNo.DataValueField = "tNo"
            cboBenefitHMOPlanTypeNo.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)

        Generic.ClearControls(Me, "Panel1")
        ModalPopupExtender1.Show()

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton
        lnk = sender

        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)

        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BenefitApplicationHMODepNo"})))
        ModalPopupExtender1.Show()

    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BenefitApplicationHMODepNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EBenefitApplicationHMODep", UserNo, item)
            i = i + 1
        Next

        If i > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub

    Private Function SaveRecord() As Boolean

        If SQLHelper.ExecuteNonQuery("EBenefitApplicationHMODep_WebSave", UserNo, Generic.ToInt(txtCode.Text), TransNo, Generic.ToInt(cboBenefitHMOPlanTypeNo.SelectedValue), _
                                   Generic.ToInt(cboEmployeedepeno.SelectedValue), Generic.ToDec(txtCost.Text), Generic.ToDec(txtMBL.Text)) > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Protected Sub cboBenefitHMOPlanTypeNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ds As DataSet
        ds = SQLHelper.ExecuteDataSet("EBenefitHMOPlanType_Web_GetCost", UserNo, Generic.ToInt(cboBenefitHMOPlanTypeNo.SelectedValue))
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                txtCost.Text = Generic.ToDec(ds.Tables(0).Rows(0)("AddPremiumCost"))
                txtMBL.Text = Generic.ToDec(ds.Tables(0).Rows(0)("mbl"))
            End If
        End If
        ds = Nothing
        ModalPopupExtender1.Show()
    End Sub
End Class







