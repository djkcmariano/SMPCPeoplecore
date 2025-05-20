Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_CarJDEditComp
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer

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
            dt = SQLHelper.ExecuteDataTable("EJDComp_Web", UserNo, TransNo, Generic.ToInt(cboTabNo.SelectedValue))
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
            txtAnchor.Enabled = False
            PopulateTabHeader()
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateDropDown()
        End If

        PopulateGrid()
        PopulateGroupBy()

    End Sub


    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EJDComp_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))
                Try
                    Me.cboCompNo.DataSource = SQLHelper.ExecuteDataSet("ECompetency_WebLookup_UnionAll", UserNo, Generic.ToInt(row("CompTypeNo")), Generic.ToInt(row("CompNo")), PayLocNo)
                    Me.cboCompNo.DataTextField = "tdesc"
                    Me.cboCompNo.DataValueField = "tno"
                    Me.cboCompNo.DataBind()
                Catch ex As Exception
                End Try
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

        Dim Retval As Boolean = False
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EJDComp_WebValidate", UserNo, Generic.ToInt(txtCode.Text), TransNo, Generic.ToInt(cboCompNo.SelectedValue), Generic.ToInt(Me.cboCompScaleNo.SelectedValue), Me.txtAnchor.Text, 0)
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

        If SQLHelper.ExecuteNonQuery("EJDComp_WebSave", UserNo, Generic.ToInt(txtCode.Text), TransNo, Generic.ToInt(cboCompNo.SelectedValue), Generic.ToInt(Me.cboCompScaleNo.SelectedValue), Me.txtAnchor.Text, 0, chkIsArchived.Checked, txtEffectiveDate.Text) > 0 Then
            Retval = True
        Else
            Retval = False
        End If

        If Retval = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
            PopulateGroupBy()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "CarJDList.aspx", "EJD") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"JDCompNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "CarJDList.aspx", "EJD") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"JDCompNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EJDComp", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    'Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
    '    Try
    '        grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
    '    Catch ex As Exception
    '        MessageBox.Warning("Error exporting to excel file.", Me)
    '    End Try

    'End Sub

    Protected Sub cboCompTypeNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        PopulateCompetency()
        txtAnchor.Text = GetIndicator()
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub cboCompScaleNo_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtAnchor.Text = GetIndicator()
        ModalPopupExtender1.Show()
    End Sub

    Private Sub PopulateCompetency()
        Try
            Me.cboCompNo.DataSource = SQLHelper.ExecuteDataSet("ECompetency_WebLookup_UnionAll", UserNo, Generic.ToInt(Me.cboCompTypeNo.SelectedValue), 0, PayLocNo)
            Me.cboCompNo.DataTextField = "tdesc"
            Me.cboCompNo.DataValueField = "tno"
            Me.cboCompNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetIndicator() As String
        Return Generic.ToStr(SQLHelper.ExecuteScalar("select top 1 Anchor from dbo.ECompDeti where CompScaleNo=" & Generic.ToInt(cboCompScaleNo.SelectedValue) & " and CompNo=" & Generic.ToInt(cboCompNo.SelectedValue)))
    End Function

    Protected Sub grdMain_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
        PopulateGroupBy()
    End Sub

    Private Sub PopulateGroupBy()
        grdMain.BeginUpdate()
        Try
            grdMain.ClearSort()
            grdMain.GroupBy(grdMain.Columns("CompTypeDesc"))
        Finally
            grdMain.EndUpdate()
        End Try
        grdMain.ExpandAll()
    End Sub

    Protected Sub grdMain_CustomColumnDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewColumnDisplayTextEventArgs)
        If e.Column.FieldName = "OrderBy" Then
            Dim groupLevel As Integer = grdMain.GetRowLevel(e.VisibleRowIndex)
            If groupLevel = e.Column.GroupIndex Then
                Dim city As String = grdMain.GetRowValues(e.VisibleRowIndex, "OrderBy").ToString()
                Dim country As String = grdMain.GetRowValues(e.VisibleRowIndex, "CompTypeDesc").ToString()
                e.DisplayText = city & " (" & country & ")"
            End If
        End If

    End Sub

    Protected Sub grdMain_CustomColumnSort(ByVal sender As Object, ByVal e As CustomColumnSortEventArgs)
        If e.Column IsNot Nothing And e.Column.FieldName = "CompTypeDesc" Then
            Dim country1 As Object = e.GetRow1Value("OrderBy")
            Dim country2 As Object = e.GetRow2Value("OrderBy")
            Dim res As Integer = Comparer.Default.Compare(country1, country2)
            If res = 0 Then
                Dim city1 As Object = e.Value1
                Dim city2 As Object = e.Value2
                res = Comparer.Default.Compare(city1, city2)
            End If
            e.Result = res
            e.Handled = True
        End If
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
