Imports clsLib
Imports System.IO
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_SecUnpostTransaction
    Inherits System.Web.UI.Page

    Dim PayLocNo As Integer
    Dim IsEnabled As Boolean = False
    Dim UserNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPostedTransaction_Web", UserNo, PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateDropDown()
        Try
            cboModuleNo.DataSource = SQLHelper.ExecuteDataSet("EPostedTransaction_WebLookup", UserNo)
            cboModuleNo.DataTextField = "tDesc"
            cboModuleNo.DataValueField = "tNo"
            cboModuleNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub PopulateDropDown_Filter(transNo As Integer)

        Try
            cboTransactionNo.DataSource = SQLHelper.ExecuteDataSet("EPostedTransaction_WebLookup_Filtered", UserNo, transNo, PayLocNo)
            cboTransactionNo.DataTextField = "tDesc"
            cboTransactionNo.DataValueField = "tNo"
            cboTransactionNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateData()
 
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPostedTransaction_WebOne", UserNo, Generic.ToInt(cboModuleNo.SelectedValue), Generic.ToInt(cboTransactionNo.SelectedValue))
        For Each row As DataRow In dt.Rows
            hifPostedByNo.Value = Generic.ToInt(row("PostedByNo"))
            txtPostedByName.Text = Generic.ToStr(row("PostedByName"))
            txtDatePosted.Text = Generic.ToStr(row("DatePosted"))
        Next
        
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopup")
            mdlShow.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub


    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim TransactionNo As Integer = Generic.ToInt(cboTransactionNo.SelectedValue)
        Dim ModuleNo As Integer = Generic.ToInt(cboModuleNo.SelectedValue)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowProcess) Then

            Dim dt As DataTable, retval As Integer, fpassword As String = ""
            dt = SQLHelper.ExecuteDataTable("SUser_WebLogin_UnpostTransaction", txtAdminName.Text.ToString)
            For Each row As DataRow In dt.Rows
                retval = Generic.ToInt(row("retval"))
                fpassword = PeopleCoreCrypt.Decrypt(Generic.ToStr(row("xpassword")))
            Next

            If retval = 0 Then
                MessageBox.Alert("Only administrator authorize to unpost the transaction!", "warning", Me)
                mdlShow.Show()
            ElseIf retval = 2 Then
                MessageBox.Alert("Invalid Username!", "warning", Me)
                mdlShow.Show()
            Else
                If txtAdminPassword.Text = fpassword Then
                    SQLHelper.ExecuteNonQuery("EPostedTransaction_WebSave", UserNo, ModuleNo, TransactionNo, txtReasons.Text.ToString, PayLocNo)
                    PopulateGrid()
                    MessageBox.Success(MessageTemplate.SuccessProcess, Me)
                Else
                    MessageBox.Alert("Invalid Password!", "warning", Me)
                    txtAdminPassword.Text = ""
                    mdlShow.Show()
                End If
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    Protected Sub lnkFilter_Click(sender As Object, e As EventArgs)
        Try
            PopulateDropDown_Filter(cboModuleNo.SelectedValue)
            mdlShow.Show()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkTransaction_Click(sender As Object, e As EventArgs)
        Try
            PopulateData()
            mdlShow.Show()
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

End Class

