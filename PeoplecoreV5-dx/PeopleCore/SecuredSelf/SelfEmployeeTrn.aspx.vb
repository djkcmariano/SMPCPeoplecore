Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_SelfEmployeeTrn
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeTrain_Web", UserNo, TransNo, 0)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeTrain_WebOne", UserNo, id)
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

        Dim STTypeNo As Integer = Generic.ToInt(cboSTTypeNo.SelectedValue)

        Dim RetVal As Boolean = False
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EEmployeeTrain_WebValidate", UserNo, Generic.ToInt(txtCode.Text), TransNo, txtTrainingTitleDesc.Text, txtDateFrom.Text, txtDateTo.Text, Generic.ToInt(txtNoOfHrs.Text))

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

        Dim dt As DataTable, ret As Integer, msg As String = ""
        Dim str As String = UserNo & ", " & txtCode.Text & ", " & TransNo & ", " & txtTrainingTitleDesc.Text & ", " & txtDateFrom.Text & ", " & txtDateTo.Text & ", " & txtNoOfHrs.Text & ", " & cboSTTypeNo.SelectedValue & ", " & txtIssuedBy.Text & ", " & txtVenue.Text
        dt = SQLHelper.ExecuteDataTable("EEmployeeTrainUpd_WebSaveSelf", UserNo, Generic.ToInt(txtCode.Text), TransNo, txtTrainingTitleDesc.Text, txtDateFrom.Text, txtDateTo.Text, Generic.ToDbl(txtNoOfHrs.Text), Generic.ToInt(cboSTTypeNo.SelectedValue), txtIssuedBy.Text, txtVenue.Text)
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
        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeTrainNo"})))
        ModalPopupExtender1.Show()

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeTrainNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ViewState("Id") = CType(item, Integer)
            i = i + 1
        Next

        If i = 1 Then
            Dim dt As DataTable, ret As Integer, msg As String = ""
            dt = SQLHelper.ExecuteDataTable("EEmployeeTrainUpd_WebDelete", UserNo, ViewState("Id"))
            For Each row As DataRow In dt.Rows
                msg = Generic.ToStr(row("xMessage"))
                ret = Generic.ToInt(row("RetVal"))
            Next
            If ret = 1 Then
                MessageBox.Success(msg, Me)
                PopulateGrid()
            Else
                MessageBox.Alert(msg, "warning", Me)
            End If
        Else
            MessageBox.Warning("Please select 1 record to delete.", Me)
        End If

    End Sub


    Private Sub PopulateControls()

        'If txtIsOtherTrain.Checked = True Then
        '    txtOtherTrain.Enabled = True
        '    txtOtherTrain.CssClass = "form-control required"
        '    cboTrainTypeNo.CssClass = "form-control"
        '    cboTrainTypeNo.Enabled = False
        '    cboTrainTypeNo.Text = ""
        '    lblTrain.Attributes.Add("class", "col-md-4 control-label has-space")
        'Else
        '    txtOtherTrain.Enabled = False
        '    txtOtherTrain.Text = ""
        '    txtOtherTrain.CssClass = "form-control"
        '    cboTrainTypeNo.CssClass = "form-control required"
        '    cboTrainTypeNo.Enabled = True
        '    lblTrain.Attributes.Add("class", "col-md-4 control-label has-required")
        'End If


    End Sub

End Class





