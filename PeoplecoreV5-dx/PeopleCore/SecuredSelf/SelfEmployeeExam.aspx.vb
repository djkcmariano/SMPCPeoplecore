Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_EmpExamList
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeExam_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeExam_WebOne", UserNo, id)
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
        PopulateControls()
        ModalPopupExtender1.Show()

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim CityNo As Integer = 0
        Dim ExamTypeNo As Integer = Generic.ToInt(cboExamTypeNo.SelectedValue)
        Dim Rating As Decimal = Generic.ToDec(txtScoreRating.Text)
        Dim IsOtherExam As Boolean = Generic.ToBol(txtIsOtherExam.Checked)
        Dim OtherExam As String = Generic.ToStr(txtOtherExam.Text)

        Dim RetVal As Boolean = False
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EEmployeeExam_WebValidate", UserNo, Generic.ToInt(txtCode.Text), TransNo, ExamTypeNo, CityNo, Me.txtDateTaken.Text.ToString, Me.txtDateReleased.Text, Rating, Me.txtRemark.Text, Me.txtLicenseNo.Text, Me.txtDateExpired.Text, Me.txtVenue.Text, IsOtherExam, OtherExam)

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
        dt = SQLHelper.ExecuteDataTable("EEmployeeExamUpd_WebSaveSelf", UserNo, Generic.ToInt(txtCode.Text), TransNo, ExamTypeNo, CityNo, Me.txtDateTaken.Text.ToString, Me.txtDateReleased.Text, Rating, Me.txtRemark.Text, Me.txtLicenseNo.Text, Me.txtDateExpired.Text, Me.txtVenue.Text, IsOtherExam, OtherExam, txtScoreRatingDesc.Text)

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
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeExamNo"})))
        ModalPopupExtender1.Show()        

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeExamNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ViewState("Id") = CType(item, Integer)
            i = i + 1
        Next

        If i = 1 Then
            Dim dt As DataTable, ret As Integer, msg As String = ""
            dt = SQLHelper.ExecuteDataTable("EEmployeeExamUpd_WebDelete", UserNo, ViewState("Id"))
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
        If txtIsOtherExam.Checked = True Then
            txtOtherExam.Enabled = True
            txtOtherExam.CssClass = "form-control required"
            'txtExamTypeDesc.CssClass = "form-control"
            cboExamTypeNo.CssClass = "form-control"
            cboExamTypeNo.Enabled = False
            cboExamTypeNo.Text = ""
            'lblExam.Attributes.Add("class", "col-md-4 control-label has-space")
            lblOtherExam.Attributes.Add("class", "col-md-4 control-label has-space")
        Else
            txtOtherExam.Enabled = False
            txtOtherExam.Text = ""
            txtOtherExam.CssClass = "form-control"
            cboExamTypeNo.CssClass = "form-control required"
            cboExamTypeNo.Enabled = True
            'txtExamTypeDesc.CssClass = "form-control required"
            'lblExam.Attributes.Add("class", "col-md-4 control-label has-required")
            lblOtherExam.Attributes.Add("class", "col-md-4 control-label has-required")
        End If
    End Sub

    Protected Sub txtIsOtherExam_CheckedChanged(sender As Object, e As System.EventArgs) Handles txtIsOtherExam.CheckedChanged
        PopulateControls()
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub cboExamTypeNo_SelectedIndexChanged()
        Dim IsCSC As Object
        IsCSC = SQLHelper.ExecuteScalar("SELECT ISNULL(IsCSC,0) FROM EExamType WHERE ExamTypeNo=" & Generic.ToInt(cboExamTypeNo.SelectedValue))
        If Generic.ToBol(IsCSC) Then
            txtLicenseNo.CssClass = "form-control"
            txtDateExpired.CssClass = "form-control"
            lblLicense.Attributes.Add("class", "col-md-4 control-label has-space")
            lblExpiry.Attributes.Add("class", "col-md-4 control-label has-space")
        Else
            txtLicenseNo.CssClass = "form-control required"
            txtDateExpired.CssClass = "form-control required"
            lblLicense.Attributes.Add("class", "col-md-4 control-label has-required")
            lblExpiry.Attributes.Add("class", "col-md-4 control-label has-required")
        End If
        ModalPopupExtender1.Show()

    End Sub

End Class





