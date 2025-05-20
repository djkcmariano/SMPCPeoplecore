Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_CarJDEditTrn
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer
    Dim Message As String

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EJDTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
    End Sub

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EJDTrn_Web", UserNo, TransNo, Generic.ToInt(cboTabNo.SelectedValue))
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "CarJDList.aspx", "EJD")
        If Not IsPostBack Then
            PopulateTabHeader()
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateDropDown()
        End If

        PopulateGrid()
    End Sub


    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EJDTrn_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.ClearControls(Me, "Panel1")
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "CarJDList.aspx", "EJD") Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Integer = 0
        Dim JDTrnNo As Integer = Generic.ToInt(txtCode.Text)
        Dim TrnTitleNo As Integer = Generic.ToInt(cboTrnTitleNo.SelectedValue)
        Dim NoOfHours As Double = Generic.ToDec(txtNoOfHours.Text)


        'Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        'Dim _dt As New DataTable
        '_dt = SQLHelper.ExecuteDataTable("EJDTrn_WebValidate", UserNo, JDTrnNo, TransNo, TrnTitleNo, NoOfHours, PayLocNo)
        'For Each row As DataRow In _dt.Rows
        '    invalid = Generic.ToBol(row("Invalid"))
        '    messagedialog = Generic.ToStr(row("MessageDialog"))
        '    alerttype = Generic.ToStr(row("AlertType"))
        'Next

        'If invalid = True Then
        '    MessageBox.Alert(messagedialog, alerttype, Me)
        '    ModalPopupExtender1.Show()
        '    Exit Sub
        'End If


        Dim dt As DataTable = SQLHelper.ExecuteDataTable("EJDTrn_WebSave", UserNo, JDTrnNo, TransNo, TrnTitleNo, NoOfHours, PayLocNo, chkIsArchived.Checked, txtEffectiveDate.Text, Generic.ToInt(chkIsQS.Checked), txtDescription.Text)
        For Each row As DataRow In dt.Rows
            RetVal = Generic.ToInt(row("RetVal"))
            Message = Generic.ToStr(row("ErrorMessage"))
        Next

        If RetVal = 1 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Alert(Message, "warning", Me)
            ModalPopupExtender1.Show()
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "CarJDList.aspx", "EJD") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"JDTrnNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "CarJDList.aspx", "EJD") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"JDTrnNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                'Generic.DeleteRecordAudit("EJDTrn", UserNo, item)
                Generic.DeleteRecordAudit("ETrnPositionDetl", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub cboTrnTitleNo_SelectedIndexChanged()

        Dim obj As Object
        obj = SQLHelper.ExecuteScalar("select hrs from etrntitle where trntitleno=" & cboTrnTitleNo.SelectedValue)
        txtNoOfHours.Text = Generic.ToInt(obj)
        ModalPopupExtender1.Show()

    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 14)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        'ViewState("TransNo") = 0
        PopulateGrid()
    End Sub


End Class
