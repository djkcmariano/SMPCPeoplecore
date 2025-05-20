Imports System.Data
Imports System.Math
Imports System.Threading
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PayBankDiskRefList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayContDeti_Web", UserNo, TransNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Private Sub PopulateTabHeader()
        Try

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayCont_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                'lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
            Next

            If Generic.ToBol(txtIsPosted.Checked) = True Then
                lnkAdd.Visible = False
                lnkDelete.Visible = False
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo, "PayContList.aspx", "EPayCont")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupMain", PayLocNo)
            PopulateTabHeader()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

#Region "********Main*******"

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkUpload_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, Session("xFormName"), Session("xTableName")) Then
            'Generic.ClearControls(Me, "Panel3")
            'ModalPopupExtender6.Show()
            Response.Redirect("~/secured/PayContEdit_Cont_Upload.aspx?id=0")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "PayContList.aspx", "EPayCont") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayContDetiNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPayContDeti", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Try
            If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayContList.aspx", "EPayCont") Then
                Dim lnk As New LinkButton, i As Integer, IsEnabled As Boolean = False
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayContDetiNo", "IsEnabled"})
                i = Generic.ToInt(obj(0))
                IsEnabled = Generic.ToBol(obj(1))

                Dim dt As DataTable
                dt = SQLHelper.ExecuteDataTable("EPayContDeti_WebOne", UserNo, Generic.ToInt(i))
                For Each row As DataRow In dt.Rows
                    Generic.PopulateData(Me, "pnlPopupMain", dt)
                Next

                If Generic.ToBol(txtIsPosted.Checked) = True Then
                    IsEnabled = False
                Else
                    IsEnabled = True
                End If

                Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)
                lnkSave.Enabled = IsEnabled
                txtFullName.Enabled = False
                mdlMain.Show()

            Else
                MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PayContList.aspx", "EPayCont") Then
            Generic.ClearControls(Me, "pnlPopupMain")
            Generic.EnableControls(Me, "pnlPopupMain", True)
            lnkSave.Enabled = True
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    'Submit record
    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PayContList.aspx", "EPayCont") Then
            Dim Retval As Boolean = False
            Dim tno As Integer = Generic.ToInt(Me.txtPayContDetiNo.Text)
            Dim EmployeeNo As Integer = Generic.ToInt(Me.hifemployeeno.Value)
            Dim EmployeeSSS As Double = Generic.ToDec(Me.txtEmployeeSSS.Text)
            Dim EmployerSSS As Double = Generic.ToDec(Me.txtEmployerSSS.Text)
            Dim EC As Double = Generic.ToDec(Me.txtEC.Text)
            Dim EmployeePH As Double = Generic.ToDec(Me.txtEmployeePH.Text)
            Dim EmployerPH As Double = Generic.ToDec(Me.txtEmployerPH.Text)
            Dim EmployeeHDMF As Double = Generic.ToDec(Me.txtEmployeeHDMF.Text)
            Dim EmployerHDMF As Double = Generic.ToDec(Me.txtEmployerHDMF.Text)
            Dim EmployeePF As Double = Generic.ToDec(Me.txtEmployeePF.Text)
            Dim EmployerPF As Double = Generic.ToDec(Me.txtEmployerPF.Text)

            Dim dt As DataTable
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            dt = SQLHelper.ExecuteDataTable("EPayContDeti_WebValidate", UserNo, tno, TransNo, EmployeeNo, EmployeeSSS, EmployerSSS, EC, EmployeePH, EmployerPH, EmployeeHDMF, EmployerHDMF)
            For Each row As DataRow In dt.Rows
                invalid = Generic.ToBol(row("Invalid"))
                messagedialog = Generic.ToStr(row("MessageDialog"))
                alerttype = Generic.ToStr(row("AlertType"))
            Next

            If invalid = True Then
                mdlMain.Show()
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If

            If SQLHelper.ExecuteNonQuery("EPayContDeti_WebSave", UserNo, tno, TransNo, EmployeeNo, EmployeeSSS, EmployerSSS, EC, EmployeePH, EmployerPH, EmployeeHDMF, EmployerHDMF, EmployeePF, EmployerPF) > 0 Then
                Retval = True
            Else
                Retval = False
            End If

            If Retval Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                PopulateGrid()
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

#End Region

End Class
