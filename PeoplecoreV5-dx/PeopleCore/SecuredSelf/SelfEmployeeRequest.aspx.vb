Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class SecuredSelf_SelfEmployeeRequest
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList_Self(UserNo, Me, "Panel1", PayLocNo)
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
        dt = SQLHelper.ExecuteDataTable("EEmployeeRequest_WebSelf", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
        grdMain.DataSource = dt
        grdMain.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticated()
        If Not IsPostBack Then            
            PopulateDropDown()
        End If
        PopulateGrid()        
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)

        Generic.EnableControls(Me, "Panel1", True)
        lnkSave.Enabled = True

        Generic.ClearControls(Me, "Panel1")
        txtFullName.Text = Generic.ToStr(Session("Fullname"))
        hifEmployeeNo.Value = Generic.ToInt(Session("EmployeeNo"))

        Me.txtDateRequested.Text = Now.ToShortDateString

        ModalPopupExtender1.Show()

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeRequestNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EEmployeeRequest", UserNo, item)
            i = i + 1
        Next
        If i > 0 Then
            'MessageBox.Success("There are " & i.ToString() & " selected record/s has been served.", Me)
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning("No record selected.", Me)
        End If

    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim dt As DataTable
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj() As Object = container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeRequestNo", "IsEnabled"})
        dt = SQLHelper.ExecuteDataTable("EEmployeeRequest_WebOne", UserNo, Generic.ToInt(obj(0)))
        Dim IsEnabled As Boolean = Generic.ToBol(obj(1))
        Generic.PopulateData(Me, "Panel1", dt)
        Generic.EnableControls(Me, "Panel1", IsEnabled)
        lnkSave.Enabled = IsEnabled
        cboRequestTypeNo.Enabled = False

        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False
        Dim tno As Integer = Generic.ToInt(Me.txtEmployeeRequestNo.Text)
        Dim RequestTypeNo As Integer = Generic.ToInt(cboRequestTypeNo.SelectedValue)
        Dim Message As String = Generic.ToStr(txtMessage.Text)
        Dim DateRequested As String = Generic.ToStr(txtDateRequested.Text)
        Dim Remarks As String = ""
        Dim EmployeeNo As Integer = Generic.ToInt(Generic.Split(hifEmployeeNo.Value, 0))
        Dim PayMainNo As Integer = Generic.ToInt(txtMessage.Text)
        Dim ComponentNo As Integer = 3 'Self Service

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EEmployeeRequest_WebValidate", UserNo, tno, RequestTypeNo, Message, DateRequested, Remarks, PayLocNo, EmployeeNo, ComponentNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EEmployeeRequest_WebSaveSelf", UserNo, tno, RequestTypeNo, Message, DateRequested, Remarks, PayLocNo, EmployeeNo, PayMainNo) > 0 Then
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

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

End Class
