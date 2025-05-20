Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_BSBillingEdit_Pay
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim bsprojectpayTypeNo As Integer = 0
    Dim Script As String = ""
    Private Sub populatedata_BS()
        Dim ds As DataSet = SQLHelper.ExecuteDataSet("Select bsprojectpayTypeNo From dbo.BBS Where BSNo=" & TransNo)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                bsprojectpayTypeNo = Generic.ToInt(ds.Tables(0).Rows(0)("bsprojectpayTypeNo"))
            End If
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "BSBillingOnProcessList.aspx", "BBS")

        If Not IsPostBack Then
            'PopulateTabHeader()

            EnabledControls()
        End If
        populatedata_BS()
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("BBSPay_Web", UserNo, TransNo, 0)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("BBSPay_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Try
                    cboPayNo.DataSource = SQLHelper.ExecuteDataSet("EPay_WebLookup_BS", UserNo, TransNo, PayLocNo)
                    cboPayNo.DataValueField = "tNo"
                    cboPayNo.DataTextField = "tDesc"
                    cboPayNo.DataBind()
                Catch ex As Exception
                End Try
                Try
                    cboDTRNo.DataSource = SQLHelper.ExecuteDataSet("EDTR_WebLookup_BS", UserNo, TransNo, PayLocNo)
                    cboDTRNo.DataValueField = "tNo"
                    cboDTRNo.DataTextField = "tDesc"
                    cboDTRNo.DataBind()
                Catch ex As Exception
                End Try
                Try
                    cboPYNo.DataSource = SQLHelper.ExecuteDataSet("EPY_WebLookup", UserNo, PayLocNo)
                    cboPYNo.DataValueField = "tNo"
                    cboPYNo.DataTextField = "tDesc"
                    cboPYNo.DataBind()
                Catch ex As Exception
                End Try
            Next
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, PayLocNo)
            Generic.PopulateData(Me, "Panel1", dt)

            PopulateControls()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub EnabledControls()
        Dim Enabled As Boolean = True

        Generic.EnableControls(Me, "Panel1", Enabled)

        lnkAdd.Visible = Enabled
        lnkDelete.Visible = Enabled
        lnkSave.Visible = Enabled
    End Sub


    Protected Sub PopulateControls()


    End Sub

    Protected Sub txtIsRefresh_OnCheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateControls()
        ModalPopupExtender1.Show()
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHRAN_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))

            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ShowHide()
        If bsprojectpayTypeNo = 2 Then
            Script = "script" & bsprojectpayTypeNo.ToString
            fRegisterStartupScript(Script, "getselectedvalue_none('pyno');")
            fRegisterStartupScript(Script, "getselectedvalue_display('dtrno');")
        Else
            fRegisterStartupScript(Script, "getselectedvalue_none('dtrno');")
            fRegisterStartupScript(Script, "getselectedvalue_display('pyno');")
        End If
    End Sub
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "BSBillingOnProcessList.aspx", "BBS") Then
            Generic.ClearControls(Me, "Panel1")

            Try
                cboPayNo.DataSource = SQLHelper.ExecuteDataSet("EPay_WebLookup", UserNo, PayLocNo)
                cboPayNo.DataValueField = "tNo"
                cboPayNo.DataTextField = "tDesc"
                cboPayNo.DataBind()
            Catch ex As Exception
            End Try
            Try
                cboDTRNo.DataSource = SQLHelper.ExecuteDataSet("EDTR_WebLookup_BS", UserNo, TransNo, PayLocNo)
                cboDTRNo.DataValueField = "tNo"
                cboDTRNo.DataTextField = "tDesc"
                cboDTRNo.DataBind()
            Catch ex As Exception
            End Try
            Try
                cboPYNo.DataSource = SQLHelper.ExecuteDataSet("EPY_WebLookup", UserNo, PayLocNo)
                cboPYNo.DataValueField = "tNo"
                cboPYNo.DataTextField = "tDesc"
                cboPYNo.DataBind()
            Catch ex As Exception
            End Try
            ShowHide()
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateControls()
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim RetVal As Boolean = False
        Dim bspayNo As Integer = Generic.ToInt(txtBSPayNo.Text)
        Dim payno As Integer = Generic.ToInt(cboPayNo.SelectedValue)
        Dim dtrno As Integer = Generic.ToInt(cboDTRNo.SelectedValue)
        Dim pyno As Integer = Generic.ToInt(cboPYNo.SelectedValue)


        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
   

        If SQLHelper.ExecuteNonQuery("BBSPay_WebSave", UserNo, bspayNo, TransNo, payno, dtrno, pyno) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "BSBillingOnProcessList.aspx", "BBS") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSPayNo"})))
            ShowHide()
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "BSBillingOnProcessList.aspx", "BBS") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BSPayNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("BBSPay", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
End Class

