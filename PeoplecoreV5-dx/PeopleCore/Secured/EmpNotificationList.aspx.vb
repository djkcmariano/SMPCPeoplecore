Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_EmpNotificationList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 24)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PopulateGrid()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeNotification_Web", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        grdMain.DataSource = dt
        grdMain.DataBind()
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

    Protected Sub lnkServe_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowPost) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeNotificationNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                i = i + Generic.ToInt(SQLHelper.ExecuteNonQuery("EEmployeeNotification_WebServed", UserNo, item))
            Next
            If i > 0 Then
                MessageBox.Success("There are " & i.ToString() & " selected record/s has been served.", Me)
            Else
                MessageBox.Warning("No record selected.", Me)
            End If
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedPost, Me)
        End If
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
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

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim tNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNotificationNo"}))
            Dim EmployeeNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo"}))
            Dim HRANNo As Integer = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"HRANNo"}))

            Try
                cboHRANNo.DataSource = SQLHelper.ExecuteDataSet("EEmployeeNotification_WebLookup_HRAN", UserNo, EmployeeNo, HRANNo, Session("xPayLocNo"))
                cboHRANNo.DataTextField = "tDesc"
                cboHRANNo.DataValueField = "tNo"
                cboHRANNo.DataBind()
            Catch ex As Exception
            End Try

            Generic.ClearControls(Me, "pnlPopupDetl")
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeNotification_WebOne", UserNo, tNo)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next

            mdlDetl.Show()

        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim tno As Integer = Generic.ToInt(txtEmployeeNotificationNo.Text)
        Dim HRANNo As Integer = Generic.ToInt(cboHRANNo.SelectedValue)
        Dim Remarks As String = Generic.ToStr(txtRemarks.Text)

        If SQLHelper.ExecuteNonQuery("EEmployeeNotification_WebSave", UserNo, tno, HRANNo, Remarks, PayLocNo) Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

End Class





