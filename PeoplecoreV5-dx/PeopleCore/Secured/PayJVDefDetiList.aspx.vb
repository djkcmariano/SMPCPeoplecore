Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_PayJVDefList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "PayJVDefList.aspx", "EJVDef")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

#Region "********Main*******"


    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EJVDef_WebDetail", UserNo, PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "PayJVDefList.aspx", "EJVDef") Then
            Try

                Dim lnk As New LinkButton
                lnk = sender
                Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
                Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"JVDefDetiNo", "JVDefLNo", "PayIncomeTypeNo", "PayDeductTypeNo", "Description"})

                Generic.ClearControls(Me, "Panel1")
                txtJVDefDetiNo.Text = Generic.ToInt(obj(0))
                cboJVDefNo.Text = Generic.ToStr(obj(1))
                txtPayIncomeTypeNo.Text = Generic.ToInt(obj(2))
                txtPayDeductTypeNo.Text = Generic.ToInt(obj(3))
                txtDescription.Text = Generic.ToStr(obj(4))

                ModalPopupExtender1.Show()

            Catch ex As Exception
            End Try

        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "PayJVDefList.aspx", "EJVDef") Then
            Dim Retval As Boolean = False
            Dim JVDefDetiNo As Integer = Generic.ToInt(Me.txtJVDefDetiNo.Text)
            Dim JVDefNo As Integer = Generic.ToInt(Me.cboJVDefNo.SelectedValue)
            Dim payincometypeno As Integer = Generic.ToInt(Me.txtPayIncomeTypeNo.Text)
            Dim paydeducttypeno As Integer = Generic.ToInt(Me.txtPayDeductTypeNo.Text)

            '//validate start here
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EJVDefDeti_WebValidate", UserNo, JVDefDetiNo, JVDefNo, payincometypeno, paydeducttypeno, PayLocNo)

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("tProceed"))
                messagedialog = Generic.ToStr(rowx("xMessage"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                ModalPopupExtender1.Show()
                Exit Sub
            End If

            If SQLHelper.ExecuteNonQuery("EJVDefDeti_WebSave", UserNo, JVDefDetiNo, JVDefNo, payincometypeno, paydeducttypeno, PayLocNo) > 0 Then
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
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

#End Region




End Class



