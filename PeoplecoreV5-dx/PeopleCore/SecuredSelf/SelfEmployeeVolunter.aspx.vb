Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_SelfEmployeeVolunter
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeVolunter_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeVolunter_WebOne", UserNo, id, 0)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticated()

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
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

        Generic.ClearControls(Me, "Panel1")
        ModalPopupExtender1.Show()

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim RetVal As Boolean = False
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EEmployeeVolunter_WebValidate", UserNo, Generic.ToInt(txtCode.Text), TransNo, txtOrganization.Text, "", "", Generic.ToInt(txtNoOfHour.Text), txtPosition.Text, txtAddress.Text, _
                                         Generic.ToInt(cboFromDay.SelectedValue), Generic.ToInt(cboFromMonth.SelectedValue), Generic.ToInt(txtFromYear.Text), _
                                         Generic.ToInt(cboToDay.SelectedValue), Generic.ToInt(cboToMonth.SelectedValue), Generic.ToInt(txtToYear.Text))

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

        Dim dt As DataTable, ret As Integer, msg As String = ""
        dt = SQLHelper.ExecuteDataTable("EEmployeeVolunterUpd_WebSaveSelf", UserNo, Generic.ToInt(txtCode.Text), TransNo, txtOrganization.Text, "", "", Generic.ToInt(txtNoOfHour.Text), txtPosition.Text, txtAddress.Text, _
                                         Generic.ToInt(cboFromDay.SelectedValue), Generic.ToInt(cboFromMonth.SelectedValue), Generic.ToInt(txtFromYear.Text), _
                                         Generic.ToInt(cboToDay.SelectedValue), Generic.ToInt(cboToMonth.SelectedValue), Generic.ToInt(txtToYear.Text), Generic.ToInt(chkIsPresent.Checked))

        For Each row As DataRow In dt.Rows
            msg = Generic.ToStr(row("xMessage"))
            ret = Generic.ToInt(row("RetVal"))
        Next

        If ret = 1 Then
            MessageBox.Success(msg, Me)
            PopulateGrid()
        Else
            MessageBox.Alert(msg, "warning", Me)
            ModalPopupExtender1.Show()
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton
        lnk = sender
        Generic.ClearControls(Me, "Panel1")
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeVolunterNo"})))
        chkIsPresent_CheckedChanged()
        ModalPopupExtender1.Show()

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        'Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeVolunterNo"})
        'Dim str As String = "", i As Integer = 0
        'For Each item As Integer In fieldValues
        '    ViewState("Id") = CType(item, Integer)
        '    i = i + 1
        'Next

        'If i = 1 Then
        '    Dim dt As DataTable, ret As Integer, msg As String = ""
        '    dt = SQLHelper.ExecuteDataTable("EEmployeeVolunterUpd_WebDelete", UserNo, ViewState("Id"))
        '    For Each row As DataRow In dt.Rows
        '        msg = Generic.ToStr(row("xMessage"))
        '        ret = Generic.ToInt(row("RetVal"))
        '    Next
        '    If ret = 1 Then
        '        MessageBox.Success(msg, Me)
        '        PopulateGrid()
        '    Else
        '        MessageBox.Alert(msg, "warning", Me)
        '    End If
        'Else
        '    MessageBox.Warning("Please select 1 record to delete.", Me)
        'End If
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeVolunterNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EEmployeeVolunter", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()

    End Sub


    Private Sub PopulateControls()

        'If txtIsOtherVolunter.Checked = True Then
        '    txtOtherVolunter.Enabled = True
        '    txtOtherVolunter.CssClass = "form-control required"
        '    cboVolunterTypeNo.CssClass = "form-control"
        '    cboVolunterTypeNo.Enabled = False
        '    cboVolunterTypeNo.Text = ""
        '    lblVolunter.Attributes.Add("class", "col-md-4 control-label has-space")
        'Else
        '    txtOtherVolunter.Enabled = False
        '    txtOtherVolunter.Text = ""
        '    txtOtherVolunter.CssClass = "form-control"
        '    cboVolunterTypeNo.CssClass = "form-control required"
        '    cboVolunterTypeNo.Enabled = True
        '    lblVolunter.Attributes.Add("class", "col-md-4 control-label has-required")
        'End If


    End Sub
    Protected Sub chkIsPresent_CheckedChanged()
        If Me.chkIsPresent.Checked Then
            cboToDay.Enabled = False
            cboToMonth.Enabled = False
            txtToYear.Enabled = False
            cboToDay.Text = ""
            cboToMonth.Text = ""
            txtToYear.Text = ""
        Else
            cboToDay.Enabled = True
            cboToMonth.Enabled = True
            txtToYear.Enabled = True
        End If
        ModalPopupExtender1.Show()
    End Sub
End Class





