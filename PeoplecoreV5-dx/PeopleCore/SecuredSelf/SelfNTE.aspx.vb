Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class SecuredSelf_SelfNTE
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim PayLocNo As Integer

    Private Sub PopulateGrid()

        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EDAARDetl_WebSelf", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue))
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticated()

        If Not IsPostBack Then
            Try
                cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 51)
                cboTabNo.DataValueField = "tNo"
                cboTabNo.DataTextField = "tDesc"
                cboTabNo.DataBind()
            Catch ex As Exception
            End Try
        End If

        PopulateGrid()


    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub btnSaveDetl_Click(sender As Object, e As EventArgs)
        If SaveRecordDetl() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Function SaveRecordDetl() As Boolean

        If SQLHelper.ExecuteNonQuery("EDAARDetl_WebSaveNTE", UserNo, Generic.ToInt(txtCodeDeti.Text), txtRemarks.Text) > 0 Then
            SaveRecordDetl = True
        Else
            SaveRecordDetl = False
        End If
    End Function

    Private Sub PopulateData(id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EDAARDetl_WebOne", UserNo, id)
        Generic.PopulateData(Me, "Panel2", dt)
        ViewState("IsEnabled") = Generic.ToInt(dt.Rows(0)("IsEnabled"))

        txtRemarks.Enabled = Generic.ToBol(ViewState("IsEnabled"))
        btnSaveDetl.Visible = Generic.ToBol(ViewState("IsEnabled"))

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateData(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DAARDetlNo"}))

        mdlDetl.Show()        
    End Sub


End Class
