Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class SecuredSelf_SelfBenSALNAssetB
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim EmployeeNo As Integer
    Dim PayLocNo As Integer
    Dim IsEnabled As Boolean = False
    Dim TransNo As Integer

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Session("SALNNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Tab.TransactionNo = TransNo
        lbl.Text = "<STRONG>Transaction No.: </STRONG>" & IIf(Session("SALNCode") > "", Session("SALNCode"), "Autonumber")
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticated()
        If Not IsPostBack Then
            If TransNo = 0 Then
                Generic.PopulateDropDownList(UserNo, Me, "pnlPopup", Generic.ToInt(Session("xPayLocNo")))
            End If
            'PopulateGrid()
        End If
        EnabledControls()
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

#Region "Main"

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ESALNAssetB_Self_Web", UserNo, Generic.ToInt(TransNo), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateData(ByVal xNo As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ESALNAssetB_Self_WebOne", UserNo, xNo, PayLocNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "pnlPopup", dt)
            Generic.PopulateDropDownList_Union(UserNo, Me, "pnlPopup", dt, Generic.ToInt(PayLocNo))
        Next
    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(Session("IsEnabled"))
        'Generic.EnableControls(Me, "pnlPopup", IsEnabled)
        'Generic.PopulateDataDisabled(Me, "pnlPopup", UserNo, PayLocNo, Generic.ToStr(Session("xMenuType")))

        lnkSave.Visible = IsEnabled
        lnkAdd.Visible = IsEnabled
        lnkDelete.Visible = IsEnabled
        lnkNotApplicable.Visible = IsEnabled
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "pnlPopup")
        Generic.EnableControls(Me, "pnlPopup", True)
        lnkSave.Enabled = True
        mdlShow.Show()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TransNo"})))
        Dim IsEnabled As Boolean = Generic.ToBol(container.Grid.GetRowValues(container.VisibleIndex, New String() {"IsEnabled"}))
        Generic.EnableControls(Me, "pnlPopup", IsEnabled)
        ViewState("IsEnabled") = IsEnabled
        lnkSave.Enabled = IsEnabled
        mdlShow.Show()
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TransNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("ESALNAssetB", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim xNo As Integer
        Dim RetVal As Boolean = False
        Dim xMessage As String = ""
        Dim _ds As New DataSet

        xNo = Generic.ToInt(Me.txtTransNo.Text)

        Dim Kind As String = Generic.ToStr(Me.txtKindA.Text)
        Dim Year As Integer = Generic.ToInt(Me.txtYearB.Text)
        Dim Acquisition As Double = Generic.ToDbl(Me.txtAcquisition.Text)

        Dim dt As DataTable, tstatus As Integer
        Dim error_num As Integer, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("ESALNAssetB_Self_WebSave", UserNo, TransNo, xNo, Kind, Year, Acquisition, PayLocNo)

        For Each row As DataRow In dt.Rows
            tstatus = Generic.ToInt(row("tStatus"))
            xNo = Generic.ToInt(row("TransNo"))
            xMessage = Generic.ToInt(row("xMessage"))
            RetVal = tstatus

            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                RetVal = False
            End If
        Next

        If RetVal = False And error_message = "" Then
            Dim url As String = "ctl00_cphBody_mdlShow"
            Dim xURL As String = "SelfBenSALNAssetB.aspx?id=" & TransNo
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
            Exit Sub
        End If

        If RetVal = True Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        End If

    End Sub

    Protected Sub lnkNotApplicable_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim RetVal As Integer = 0
        Dim xMessage As String = ""
        RetVal = SQLHelper.ExecuteNonQuery("ESALN_SaveNA", Generic.ToInt(TransNo), "AssetB", 1)

        If RetVal = 1 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If

    End Sub

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

#End Region

End Class
