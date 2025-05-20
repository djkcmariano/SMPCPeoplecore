Imports System.Data
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Secured_PYFieldShow
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim EmployeeNo As Integer = 0
    Dim lookup As New clsGenericClass
    Dim PayLocNo As Integer = 0

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EpyFieldShow_Web", UserNo, Generic.ToInt(cboPYActivityTypeNo.SelectedValue))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EpyFieldShow_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
            cboResponseTypeNo.Visible = False
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub PopulateData_Dynamic(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EpyFieldShowDynamic_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
            cboResponseTypeNo.Visible = True
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
        If Not IsPostBack Then
            PopulateDropDown()

        End If
        If IsCallback Then
            PopulateGrid()
        End If

    End Sub

    Private Sub PopulateDropDown()
        Generic.PopulateDropDownList(UserNo, Me, "panel-heading", Generic.ToInt(Session("xPayLocNo")))
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", Generic.ToInt(Session("xPayLocNo")))
        Try
            cboPYActivityTypeNo.DataSource = SQLHelper.ExecuteDataSet("Select tNo,tdesc From (Select Convert(Varchar(10),pyactivityTypeNo) As tNo,PYActivityTypeDesc As tDesc,1 as orderlevel from dbo.EPYActivityType Union select '','--Select--',0 as orderlevel) A order by a.orderlevel")
            cboPYActivityTypeNo.DataTextField = "tDesc"
            cboPYActivityTypeNo.DataValueField = "tno"
            cboPYActivityTypeNo.DataBind()
        Catch ex As Exception
        End Try
        Try
            cboResponseTypeNo.DataSource = SQLHelper.ExecuteDataSet("Select tNo,tdesc From (Select Convert(Varchar(10),ResponseTypeNo) As tNo,ResponseTypeDesc As tDesc,1 as orderlevel from dbo.EResponseType Where ResponseTypeNo In(3,5) Union select '','--Select--',0 as orderlevel) A order by a.orderlevel")
            cboResponseTypeNo.DataTextField = "tDesc"
            cboResponseTypeNo.DataValueField = "tno"
            cboResponseTypeNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        If Generic.ToInt(txtColId.Text) > 0 And ViewState("IsEnabled") = False Then
            If SQLHelper.ExecuteNonQuery("EPYFieldShow_WebSave_Column", UserNo, Generic.ToInt(cboPYActivityTypeNo.SelectedValue), Generic.ToInt(txtColId.Text), Generic.ToStr(txtColumnDesc.Text)) > 0 Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                PopulateGrid()
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)

            End If
        Else
            If SQLHelper.ExecuteNonQuery("EPYFieldShowDynamic_WebSave", UserNo, Generic.ToInt(txtColId.Text), Generic.ToInt(cboPYActivityTypeNo.SelectedValue), Generic.ToStr(txtColumnDesc.Text), Generic.ToInt(cboResponseTypeNo.SelectedValue)) > 0 Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                PopulateGrid()
            Else
                MessageBox.Critical(MessageTemplate.ErrorSave, Me)

            End If
        End If
        
    End Sub
    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            'PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"ColId"})))
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"ColId", "IsEnabled"})
            ViewState("ColId") = obj(0)
            Dim IsEnabled As Boolean = obj(1)
            ViewState("IsEnabled") = IsEnabled
            If IsEnabled Then
                PopulateData_Dynamic(ViewState("ColId"))
            Else
                PopulateData(ViewState("ColId"))
            End If

            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub
    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            cboResponseTypeNo.Visible = True
            mdlDetl.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub
    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ColId"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EPYFieldShowDynamic", UserNo, item)
                SQLHelper.ExecuteNonQuery("Delete From dbo.EPYDetiDynamic Where PyNo=" & Generic.ToStr(cboPYActivityTypeNo.SelectedValue).ToString & " And PYFieldShowDynamicNo=" & item.ToString)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub lnkUpdate_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, ib As New ImageButton, Count As Integer = 0, colID As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            'Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ColId"})
            For i As Integer = grdMain.VisibleStartIndex To Math.Min(grdMain.VisibleRowCount, grdMain.VisibleStartIndex + grdMain.SettingsPager.PageSize) - 1
                chk = TryCast(grdMain.FindRowCellTemplateControl(i, TryCast(grdMain.Columns(4), GridViewDataColumn), "txtIsAvailable"), CheckBox)
                If Not chk Is Nothing Then
                    colID = Generic.ToInt(chk.ToolTip)
                    SQLHelper.ExecuteNonQuery("EpyFieldShow_WebSave", UserNo, Generic.ToInt(cboPYActivityTypeNo.SelectedValue), colID, chk.Checked)
                End If
            Next

            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()

        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub
    Protected Sub grdDetl_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub
    'Private Function SaveRecord() As Boolean

    '    Dim PYActivityTypeNo As Integer = Generic.ToInt(ViewState("TransNo")) 'Generic.ToInt(cboPYActivityTypeNo.SelectedValue)
    '    Dim noofdrops As Integer = Generic.ToInt(txtNoOfDrops.Text)
    '    Dim amount As Double = Generic.ToDec(txtAmount.Text)

    '    Dim retVal As Boolean = False, error_num As Integer, error_message As String = ""

    '    Dim dt As DataTable = SQLHelper.ExecuteDataTable("EPYMatrixDrop_WebSave", UserNo, Generic.ToInt(txtPYMatrixDropNo.Text), pymatrixno, noofdrops, amount)
    '    For Each row As DataRow In dt.Rows
    '        retVal = True
    '        error_num = Generic.ToInt(row("Error_num"))
    '        If error_num > 0 Then
    '            error_message = Generic.ToStr(row("ErrorMessage"))
    '            MessageBox.Critical(error_message, Me)
    '            retVal = False
    '        End If

    '    Next
    '    If retVal = False And error_message = "" Then
    '        MessageBox.Critical(MessageTemplate.ErrorSave, Me)
    '    End If
    '    If retVal = True Then
    '        PopulateGrid()
    '        MessageBox.Success(MessageTemplate.SuccessSave, Me)
    '    End If

    '    Return retVal
    'End Function



End Class

