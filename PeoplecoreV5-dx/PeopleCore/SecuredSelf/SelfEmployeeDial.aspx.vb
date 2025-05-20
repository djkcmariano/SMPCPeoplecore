Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_SelfEmployeeDial
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeDialect_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeDialect_WebOne", UserNo, id)
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

        Dim EmployeeDialectectNo As Integer = Generic.ToInt(Me.txtCode.Text)
        Dim Dialectect As Integer = Generic.ToInt(Me.cboDialectNo.SelectedValue)
        Dim SpeakingLevel As Integer = Generic.ToInt(Me.cboSpeakingProfLevelNo.SelectedValue)
        Dim WritingLevel As Integer = Generic.ToInt(Me.cboWritingProfLevelNo.SelectedValue)
        Dim ReadingLevel As Integer = Generic.ToInt(Me.cboReadingProfLevelNo.SelectedValue)
        Dim Remark As String = Generic.ToStr(Me.txtRemark.Text)
        Dim IsOtherDialect As Boolean = Generic.ToBol(txtIsOtherDial.Checked)
        Dim OtherDialect As String = Generic.ToStr(txtOtherDial.Text)

        'Dim RetVal As Boolean = False
        'Dim invalid As Boolean = True, messageDialectog As String = "", alerttype As String = ""
        'Dim dtx As New DataTable, error_num As Integer = 0, error_message As String = ""
        'dtx = SQLHelper.ExecuteDataTable("EEmployeeDialect_WebValidate", UserNo, Generic.ToInt(txtCode.Text), TransNo, Dialectect, 0, Remark, WritingLevel, ReadingLevel, SpeakingLevel, IsOtherDialect, OtherDialect)

        'For Each rowx As DataRow In dtx.Rows
        '    invalid = Generic.ToBol(rowx("tProceed"))
        '    messageDialectog = Generic.ToStr(rowx("xMessage"))
        '    alerttype = Generic.ToStr(rowx("AlertType"))
        'Next

        'If invalid = True Then
        '    MessageBox.Alert(messageDialectog, alerttype, Me)
        '    ModalPopupExtender1.Show()
        '    Exit Sub
        'End If

        Dim dt As DataTable, ret As Integer, msg As String = ""
        dt = SQLHelper.ExecuteDataTable("EEmployeeDialectUpd_WebSaveSelf", UserNo, Generic.ToInt(txtCode.Text), TransNo, Dialectect, 0, Remark, WritingLevel, ReadingLevel, SpeakingLevel, IsOtherDialect, OtherDialect)

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
        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeDialectNo"})))
        PopulateControls()
        ModalPopupExtender1.Show()

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        'Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeDialectNo"})
        'Dim str As String = "", i As Integer = 0
        'For Each item As Integer In fieldValues
        '    ViewState("Id") = CType(item, Integer)
        '    i = i + 1
        'Next

        'If i = 1 Then
        '    Dim dt As DataTable, ret As Integer, msg As String = ""
        '    dt = SQLHelper.ExecuteDataTable("EEmployeeDialectUpd_WebDelete", UserNo, ViewState("Id"))
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
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeDialectNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EEmployeeDialect", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()

    End Sub


    Protected Sub txtIsOtherDial_CheckedChanged(sender As Object, e As System.EventArgs) Handles txtIsOtherDial.CheckedChanged
        PopulateControls()
        ModalPopupExtender1.Show()
    End Sub
    Private Sub PopulateControls()

        If txtIsOtherDial.Checked = True Then
            txtOtherDial.Enabled = True
            txtOtherDial.CssClass = "form-control required"
            cboDialectNo.CssClass = "form-control"
            cboDialectNo.Enabled = False
            cboDialectNo.Text = ""
            lblDialect.Attributes.Add("class", "col-md-4 control-label has-space")
        Else
            txtOtherDial.Enabled = False
            txtOtherDial.Text = ""
            txtOtherDial.CssClass = "form-control"
            cboDialectNo.CssClass = "form-control required"
            cboDialectNo.Enabled = True
            lblDialect.Attributes.Add("class", "col-md-4 control-label has-required")
        End If
    End Sub


End Class





