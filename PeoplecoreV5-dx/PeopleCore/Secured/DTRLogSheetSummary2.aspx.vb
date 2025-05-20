Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_DTRLogSheetSummary2
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Private Sub PopulateGrid()
        grdMain.DataSource = SQLHelper.ExecuteDataTable("EDTRLogSheet_Web", UserNo, "", PayLocNo)
        grdMain.DataBind()
    End Sub

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDTRLogSheet_WebOne", UserNo, id)
        For Each row As DataRow In dt.Rows
            cboPayClassNo.SelectedValue = IIf(Generic.ToInt(row("PayClassNo")) = 0, "", Generic.ToInt(row("PayClassNo")).ToString)
            txtDTRDate.Text = Generic.ToStr(row("DTRDate"))
        Next
        Generic.PopulateData(Me, "Panel2", dt)
        Generic.PopulateData(Me, "Panel3", dt)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Request.QueryString("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
        End If

        PopulateGrid()

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If SQLHelper.ExecuteNonQuery("EDTRLogSheet_WebSave", UserNo, Generic.ToInt(txtCode.Text), Generic.ToInt(hifEmployeeNo.Value), Generic.ToInt(cboPayClassNo.SelectedValue), txtDTRDate.Text, Generic.ToDec(txtWorkingHrs.Text), _
                                     Generic.ToDec(cboDayTypeNo.SelectedValue), Generic.ToDec(txtOvt.Text), Generic.ToDec(txtOvt8.Text), Generic.ToDec(txtNP.Text), Generic.ToDec(txtNP8.Text), PayLocNo, txtClientID.Text, txtOperationCode.Text, txtWorkOrder.Text) > 0 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            txtCode.Text = ""
            txtFullname.Text = ""
            hifEmployeeNo.Value = "0"
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DTRLogSheetNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                SQLHelper.ExecuteNonQuery("EDTRLogSheet_WebDelete", UserNo, item)
                Generic.DeleteRecordAudit("EDTRLogSheet", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRLogSheetNo"})))
        PopulateDayType()
        If cboPayClassNo.Text = "" Then
            cboDayTypeNo.Items.Clear()            
        End If

        'Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
        'Generic.EnableControls(Me, "pnlPopupDetl", IsEnabled)
    End Sub

    Protected Sub cboPayClassNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If cboPayClassNo.Text = "" Then
                cboDayTypeNo.Items.Clear()
            End If

            PopulateDayType()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDayType()
        Try
            cboDayTypeNo.DataSource = SQLHelper.ExecuteDataTable("EDayType_WebLookup", Generic.ToInt(cboPayClassNo.SelectedValue))
            cboDayTypeNo.DataTextField = "tdesc"
            cboDayTypeNo.DataValueField = "tNo"
            cboDayTypeNo.DataBind()

        Catch ex As Exception

        End Try
      
    End Sub


    Protected Sub grdMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            grdMain.PageIndex = e.NewPageIndex
            'PopulateGridMass()
        Catch ex As Exception

        End Try
    End Sub

End Class


