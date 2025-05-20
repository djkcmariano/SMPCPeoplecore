Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_EmpJVInfo
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeJVInfo_Web", UserNo, TransNo, PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
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

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeJVInfo_WebOne", UserNo, id, PayLocNo)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateTabHeader()
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim dt As DataTable
        Dim EmployeeServiceNo As Integer = Generic.ToInt(Me.txtCode.Text)
        Dim JVInfo1 As Integer = Generic.ToInt(Me.txtJVInfo1.Text)
        Dim JVInfo2 As Integer = Generic.ToInt(Me.txtJVInfo2.Text)
        Dim JVInfo3 As Integer = Generic.ToInt(Me.txtJVInfo3.Text)
        Dim Remarks As Integer = Generic.ToInt(Me.txtRemark.Text)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        'Dim dtx As New DataTable
        'dtx = SQLHelper.ExecuteDataTable("EEmployeeJVInfo_WebValidate", UserNo, EmployeeServiceNo, TransNo, DateFrom, DateTo, LWOP, Position, EmployeeStatus, Salary, Station, Department, SeparationDate, Remark, PayLocNo)

        'For Each rowx As DataRow In dtx.Rows
        '    invalid = Generic.ToBol(rowx("Invalid"))
        '    messagedialog = Generic.ToStr(rowx("MessageDialog"))
        '    alerttype = Generic.ToStr(rowx("AlertType"))
        'Next

        'If invalid = True Then
        '    MessageBox.Alert(messagedialog, alerttype, Me)
        '    ModalPopupExtender1.Show()
        '    Exit Sub
        'End If

        dt = SQLHelper.ExecuteDataTable("EEmployeeJVInfo_WebSave", UserNo, EmployeeServiceNo, TransNo, JVInfo1, JVInfo2, JVInfo3, Remarks)

        For Each row As DataRow In dt.Rows
            'RetVal = Generic.ToInt(row("Retval"))
            RetVal = True
        Next

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            'PopulateData(Generic.ToInt(lnk.CommandArgument))
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeJVInfoNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeJVInfoNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeJVInfo", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub


End Class





