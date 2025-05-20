Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Partial Class Secured_PENormsList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim pestandardmainno As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim FormName As String = ""
    Dim TableName As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        pestandardmainno = Generic.ToInt(Request.QueryString("id"))
        FormName = "PEStandardMainList.aspx"
        TableName = "EPEStandardMain"

        AccessRights.CheckUser(UserNo, FormName, TableName)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupItem", PayLocNo)
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupChoices", PayLocNo)

            Try
                Me.cboPEEvaluatorNo.DataSource = SQLHelper.ExecuteDataSet("EPEEvaluator_WebLookup", UserNo, PayLocNo)
                Me.cboPEEvaluatorNo.DataTextField = "tdesc"
                Me.cboPEEvaluatorNo.DataValueField = "tno"
                Me.cboPEEvaluatorNo.DataBind()
            Catch ex As Exception
            End Try

        End If

        PopulateGrid()
        PopulateDetl()
        PopulateObj()

    End Sub

    Private Sub EnabledDetail(ByVal pecatetypeno As Integer)

        If pecatetypeno = 1 Then
            divObj.Visible = True
            divCore.Visible = False
        ElseIf pecatetypeno = 2 Then
            divObj.Visible = False
            divCore.Visible = True
        ElseIf pecatetypeno = 4 Then
            divObj.Visible = False
            divCore.Visible = True
        Else
            divObj.Visible = False
            divCore.Visible = False
        End If

    End Sub


#Region "Category"

    Protected Sub PopulateGrid(Optional IsMain As Boolean = False)
        Try

            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EPEStandardCate_Web", UserNo, pestandardmainno)
            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()

            If ViewState("TransNo") = 0 Or IsMain = True Then
                Dim obj As Object() = grdMain.GetRowValues(grdMain.VisibleStartIndex(), New String() {"PEStandardCateNo", "PECateTypeNo", "Reference"})
                ViewState("TransNo") = obj(0)
                ViewState("CateTypeNo") = obj(1)
                lblDetl.Text = obj(2)
                lblObj.Text = obj(2)
            End If

            PopulateDetl()
            PopulateObj()
            EnabledDetail(Generic.ToInt(ViewState("CateTypeNo")))

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboPECateNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPECate_WebOne", UserNo, Generic.ToInt(cboPECateNo.SelectedValue))
        For Each row As DataRow In dt.Rows
            txtPEStandardCateCode.Text = Generic.ToStr(row("PECateCode"))
            txtPEStandardCateDesc.Text = Generic.ToStr(row("PECateDesc"))
        Next
        mdlMain.Show()
    End Sub

    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, FormName, TableName) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEStandardCateNo", "IsEnabled"})
            Dim iNo As Integer = Generic.ToInt(obj(0))
            Dim IsEnabled As Boolean = Generic.ToBol(obj(1))

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardCate_WebOne", UserNo, Generic.ToInt(iNo))
            For Each row As DataRow In dt.Rows
                Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
                Generic.PopulateData(Me, "pnlPopupMain", dt)
                PopulatePECateType()
            Next
            mdlMain.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, FormName, TableName) Then
            Generic.ClearControls(Me, "pnlPopupMain")
            PopulatePECateType()
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)


        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, FormName, TableName) Then
            Dim DeleteCount As Integer = 0
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PEStandardCateNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAuditCol("EPEStandardDim", UserNo, "PEStandardCateNo", CType(item, Integer))
                Generic.DeleteRecordAuditCol("EPEStandardObj", UserNo, "PEStandardCateNo", CType(item, Integer))
                Generic.DeleteRecordAudit("EPEStandardCate", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid(True)
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim tno As Integer = Generic.ToInt(Me.txtPEStandardCateNo.Text)
        Dim OrderLevelCate As Integer = Generic.ToInt(Me.txtOrderLevelCate.Text)
        Dim PEStandardCateCode As String = Generic.ToStr(Me.txtPEStandardCateCode.Text)
        Dim PECateNo As Integer = Generic.ToInt(Me.cboPECateNo.SelectedValue)
        Dim PEStandardCateDesc As String = Generic.ToStr(Me.txtPEStandardCateDesc.Text)
        Dim WeightedCate As Double = Generic.ToDec(Me.txtWeightedCate.Text)
        Dim PECateTypeNo As Integer = Generic.ToInt(Me.cboPECateTypeNo.SelectedValue)
        Dim NoOfItems As Integer = Generic.ToInt(Me.txtNoOfItems.Text)
        Dim Remarks As String = Generic.ToStr(Me.txtRemarksCate.Text)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EPEStandardCate_WebValidate", UserNo, tno, pestandardmainno, PECateNo, WeightedCate, OrderLevelCate, PEStandardCateCode, PEStandardCateDesc, PECateTypeNo, NoOfItems, Remarks)

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

        If SQLHelper.ExecuteNonQuery("EPEStandardCate_WebSave", UserNo, tno, pestandardmainno, PECateNo, WeightedCate, OrderLevelCate, PEStandardCateCode, PEStandardCateDesc, PECateTypeNo, NoOfItems, Remarks) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEStandardCateNo", "PECateTypeNo", "Reference"})
            ViewState("TransNo") = obj(0)
            ViewState("CateTypeNo") = obj(1)
            lblDetl.Text = obj(2)
            lblObj.Text = obj(2)

            PopulateDetl()
            PopulateObj()
            EnabledDetail(Generic.ToInt(ViewState("CateTypeNo")))

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub cboPECateTypeNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)

        PopulatePECateType()

        mdlMain.Show()
    End Sub

    Protected Sub PopulatePECateType()
        If Generic.ToInt(Me.cboPECateTypeNo.SelectedValue) = 3 Then
            cboPECateNo.Text = ""
            cboPECateNo.Enabled = False
            cboPECateNo.CssClass = "form-control"
            lblCate.Attributes.Add("class", "col-md-4 control-label has-space")
            txtPEStandardCateCode.Enabled = False
            txtPEStandardCateCode.Text = ""
            txtPEStandardCateDesc.Enabled = False
            txtPEStandardCateDesc.Text = ""
            txtNoOfItems.Enabled = False
            txtNoOfItems.Text = ""
            txtWeightedCate.Enabled = False
            txtWeightedCate.Text = ""
            txtWeightedCate.CssClass = "form-control"
            lblWeighted.Attributes.Add("class", "col-md-4 control-label has-space")
        Else
            cboPECateNo.Enabled = True
            cboPECateNo.CssClass = "form-control required"
            lblCate.Attributes.Add("class", "col-md-4 control-label has-required")
            txtPEStandardCateCode.Enabled = True
            txtPEStandardCateDesc.Enabled = True
            txtNoOfItems.Enabled = True
            txtWeightedCate.Enabled = True
            txtWeightedCate.CssClass = "form-control required"
            lblWeighted.Attributes.Add("class", "col-md-4 control-label has-required")
        End If
    End Sub

#End Region


#Region "Dimension"
    Protected Sub PopulateDetl(Optional pageNo As Integer = 0)
        Try

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardDim_Web", UserNo, pestandardmainno, Generic.ToInt(ViewState("TransNo")))
            grdDetl.DataSource = dt
            grdDetl.DataBind()


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cboPEDimensionTypeNo_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPEDimensionType_WebOne", UserNo, Generic.ToInt(cboPEDimensionTypeNo.SelectedValue))
        For Each row As DataRow In dt.Rows
            txtPEStandardDimCode.Text = Generic.ToStr(row("tCode"))
            txtPEStandardDimDesc.Text = Generic.ToStr(row("tDesc"))
        Next
        mdlDetl.Show()
    End Sub

    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, FormName, TableName) Then
            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEStandardDimNo"}))

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardDim_WebOne", UserNo, Generic.ToInt(i))
            For Each row As DataRow In dt.Rows
                Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
            mdlDetl.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkAddDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, FormName, TableName) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkDeleteDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, FormName, TableName) Then
            Dim fieldValues As List(Of Object) = grdDetl.GetSelectedFieldValues(New String() {"PEStandardDimNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPEStandardDim", UserNo, item)
                i = i + 1
            Next

            If i > 0 Then
                PopulateDetl()
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If

    End Sub

    Protected Sub lnkSaveDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim PEStandardCateNo As Integer = Generic.ToInt(ViewState("TransNo"))
        Dim PEStandardDimNo As Integer = Generic.ToInt(Me.txtPEStandardDimNo.Text)
        Dim OrderLevelDim As Integer = Generic.ToInt(Me.txtOrderLevelDim.Text)
        Dim PEStandardDimCode As String = Generic.ToStr(Me.txtPEStandardDimCode.Text)
        Dim PEDimensionTypeNo As Integer = Generic.ToInt(Me.cboPEDimensionTypeNo.SelectedValue)
        Dim PEStandardDimDesc As String = Generic.ToStr(Me.txtPEStandardDimDesc.Text)
        Dim WeightedDim As Double = Generic.ToDec(Me.txtWeightedDim.Text)
        Dim Remarks As String = Generic.ToStr(Me.txtRemarksDim.Text)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EPEStandardDim_WebValidate", UserNo, PEStandardDimNo, pestandardmainno, PEDimensionTypeNo, WeightedDim, OrderLevelDim, PEStandardDimCode, PEStandardDimDesc, PEStandardCateNo, Remarks)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlDetl.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EPEStandardDim_WebSave", UserNo, PEStandardDimNo, pestandardmainno, PEDimensionTypeNo, WeightedDim, OrderLevelDim, PEStandardDimCode, PEStandardDimDesc, PEStandardCateNo, Remarks) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateDetl()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkExportDetl_Click(sender As Object, e As EventArgs)
        Try
            grdExportDetl.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

#End Region


#Region "Objective"

    Protected Sub PopulateObj()
        Try

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardObj_Web", UserNo, pestandardmainno, Generic.ToInt(ViewState("TransNo")))
            grdObj.DataSource = dt
            grdObj.DataBind()


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkEditObj_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, FormName, TableName) Then

            Dim lnk As New LinkButton, i As Integer
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEStandardObjNo"}))

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardObj_WebOne", UserNo, Generic.ToInt(i))
            For Each row As DataRow In dt.Rows
                Generic.PopulateDropDownList(UserNo, Me, "pnlPopupObj", PayLocNo)
                Generic.PopulateData(Me, "pnlPopupObj", dt)
            Next
            mdlObj.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Protected Sub lnkAddObj_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, FormName, TableName) Then
            Generic.ClearControls(Me, "pnlPopupObj")
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupObj", PayLocNo)
            mdlObj.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub


    Protected Sub lnkSaveObj_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim Retval As Boolean = False
        Dim PEStandardObjNo As Integer = Generic.ToInt(Me.txtPEStandardObjNo.Text)
        Dim PEStandardCateNo As Integer = Generic.ToInt(ViewState("TransNo"))
        Dim Description As String = Generic.ToStr(Me.txtDescription.Text)
        Dim ResponseTypeNo As Integer = Generic.ToInt(Me.cboResponseTypeNo.SelectedValue)
        Dim IsRating As Boolean = False
        Dim OrderLevelObj As Integer = Generic.ToInt(Me.txtOrderLevelObj.Text)
        Dim ObjectiveTypeNo As Integer = Generic.ToInt(Me.cboObjectiveTypeNo.SelectedValue)
        Dim PECycleNo As Integer = Generic.ToInt(Me.cboPECycleNo.SelectedValue)
        Dim PEEvaluatorNo As Integer = Generic.ToInt(Me.cboPEEvaluatorNo.SelectedValue)
        Dim IsRequired As Boolean = Generic.ToBol(Me.chkIsRequired.Checked)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, dt As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EPEStandardObj_WebValidate", UserNo, PEStandardObjNo, pestandardmainno, PEStandardCateNo, Description, ResponseTypeNo, IsRating, OrderLevelObj, ObjectiveTypeNo, PECycleNo, PEEvaluatorNo, IsRequired)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            mdlObj.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EPEStandardObj_WebSave", UserNo, PEStandardObjNo, pestandardmainno, PEStandardCateNo, Description, ResponseTypeNo, IsRating, OrderLevelObj, ObjectiveTypeNo, PECycleNo, PEEvaluatorNo, IsRequired) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval Then
            PopulateObj()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkDeleteObj_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, FormName, TableName) Then
            Dim fieldValues As List(Of Object) = grdObj.GetSelectedFieldValues(New String() {"PEStandardObjNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPEStandardObj", UserNo, item)
                i = i + 1
            Next

            If i > 0 Then
                PopulateObj()
                MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If


    End Sub

    Protected Sub lnkExportObj_Click(sender As Object, e As EventArgs)
        Try
            grdExportObj.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDetailObj_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEStandardObjNo", "Description"})
            ViewState("ObjNo") = obj(0)
            lblChoices.Text = obj(1)

            PopulateObjDetl()
            mdlObjDetl.Show()

        Catch ex As Exception

        End Try

    End Sub

#End Region


#Region "Objective Choices"

    

    Private Sub PopulateObjDetl()
        Try

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEStandardObjDeti_Web", UserNo, Generic.ToInt(ViewState("ObjNo")))
            Dim dv As DataView = dt.DefaultView

            Me.grdObjDetl.DataSource = dv
            Me.grdObjDetl.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdObjDetl_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs)
        Try
            grdObjDetl.PageIndex = e.NewPageIndex
            PopulateObjDetl()
            mdlObjDetl.Show()
        Catch ex As Exception

        End Try
    End Sub

    'Submit record
    Protected Sub btnSaveObjDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim lblObjNo As New Label, lblOrder As New Label, lblDetlNo As New Label, txtDescription As New TextBox, cboPERatingNo As New DropDownList, txtProfeciency As New TextBox
        Dim tcount As Integer, Count As Integer = 0


        For tcount = 0 To Me.grdObjDetl.Rows.Count - 1
            lblObjNo = CType(grdObjDetl.Rows(tcount).FindControl("lblObjNo"), Label)
            lblOrder = CType(grdObjDetl.Rows(tcount).FindControl("lblOrder"), Label)
            lblDetlNo = CType(grdObjDetl.Rows(tcount).FindControl("lblDetlNo"), Label)
            cboPERatingNo = CType(grdObjDetl.Rows(tcount).FindControl("cboPERatingNo"), DropDownList)
            txtDescription = CType(grdObjDetl.Rows(tcount).FindControl("txtAnchor"), TextBox)
            txtProfeciency = CType(grdObjDetl.Rows(tcount).FindControl("txtProfeciency"), TextBox)

            Dim PEStandardObjNo As Integer = Generic.ToInt(ViewState("ObjNo"))
            Dim OrderLevel As Integer = Generic.ToInt(lblOrder.Text)
            Dim DetlNo As Integer = Generic.ToInt(lblDetlNo.Text)
            Dim Description As String = Generic.ToStr(txtDescription.Text)
            Dim PERatingNo As Integer = Generic.ToInt(cboPERatingNo.SelectedValue)
            Dim Profeciency As Double = Generic.ToDec(txtProfeciency.Text)

            If Description > "" Then
                If SQLHelper.ExecuteNonQuery("EPEStandardObjDeti_WebSave", UserNo, DetlNo, PEStandardObjNo, PERatingNo, Description, Profeciency, OrderLevel) > 0 Then
                    Count = Count + 1
                End If
            End If

        Next

        PopulateObjDetl()
        If Count > 0 Then
            Count = Count - 1
            'MessageBox.Alert(MessageTemplate.SuccessSave, "success", Me)
        Else
            MessageBox.Alert(MessageTemplate.NoSelectedTransaction, "information", Me)
        End If

        mdlObjDetl.Show()

    End Sub

    Protected Sub btnDeleteObjDetl_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label, tcheck As New CheckBox
        Dim tcount As Integer, Count As Integer = 0, lblOrder As New Label

        For tcount = 0 To Me.grdObjDetl.Rows.Count - 1
            lbl = CType(grdObjDetl.Rows(tcount).FindControl("lblDetlNo"), Label)
            tcheck = CType(grdObjDetl.Rows(tcount).FindControl("txtIsSelectObjDetl"), CheckBox)
            If tcheck.Checked = True Then
                Generic.DeleteRecordAudit("EPEStandardObjDeti", UserNo, CType(lbl.Text, Integer))
                Count = Count + 1
            End If
        Next

        PopulateObjDetl()

        For tcount = 0 To Me.grdObjDetl.Rows.Count - 1
            lbl = CType(grdObjDetl.Rows(tcount).FindControl("lblDetlNo"), Label)
            lblOrder = CType(grdObjDetl.Rows(tcount).FindControl("lblOrder"), Label)
            Dim OrderLevel As Integer = Generic.ToInt(lblOrder.Text)

            If SQLHelper.ExecuteNonQuery("EPEStandardObjDeti_WebDelete", CType(lbl.Text, Integer), OrderLevel) > 0 Then
                Count = Count + 1
            End If
        Next

        PopulateObjDetl()

        If Count > 0 Then
            Count = Count - 1
            MessageBox.Alert("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, "success", Me)
        Else
            MessageBox.Alert(MessageTemplate.NoSelectedTransaction, "information", Me)
        End If

        mdlObjDetl.Show()

    End Sub
#End Region


    
End Class

