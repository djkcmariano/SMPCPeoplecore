Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_DTRDetlList_Disc
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim DTRNo As Integer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        DTRNo = Generic.ToInt(Request.QueryString("transNo"))

        AccessRights.CheckUser(UserNo, "DTR.aspx", "EDTR")

        If Not IsPostBack Then
            PopulateHeader()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
    End Sub

    Private Sub PopulateHeader()

        Dim _ds As New DataSet
        _ds = SQLHelper.ExecuteDataSet("EDTR_WebOne", UserNo, DTRNo)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                lblDTRCode.Text = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("DTRCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblPayClassDesc.Text = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("PayClassDesc"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDTRCutoff.Text = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("StartDate"), clsBase.clsBaseLibrary.enumObjectType.StrType) & " - " & Generic.CheckDBNull(_ds.Tables(0).Rows(0)("EndDate"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblPayTypeDesc.Text = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("PayTypeDesc"), clsBase.clsBaseLibrary.enumObjectType.StrType)

            End If
        End If

    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EDTRDetiDisc_Web", UserNo, DTRNo)

        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

        PopulateDetl()
    End Sub

    Private Sub PopulateDetl()
        Try

            Dim _ds As DataSet, FullName As String = ""

            If ViewState("DTRDetiDiscNo") = 0 Then
                Dim i As Integer = 0, p As Integer = 0
                i = grdMain.GetRowValues(grdMain.FocusedRowIndex(), "DTRDetiDiscNo")
                ViewState("DTRDetiDiscNo") = i
            End If

            _ds = SQLHelper.ExecuteDataSet("EDTRDetiLogDiff_Web", UserNo, Generic.ToInt(ViewState("DTRDetiDiscNo")))
            If _ds.Tables.Count > 0 Then
                If _ds.Tables(0).Rows.Count > 0 Then
                    FullName = Generic.ToStr(_ds.Tables(0).Rows(0)("FullName"))
                End If
            End If

            grdDetl.DataSource = _ds
            grdDetl.DataBind()

            lblFullName.Text = "DTR Discrepancy Details of : " & FullName.ToString

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkDetail_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        ViewState("DTRDetiDiscNo") = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"DTRDetiDiscNo"}))

        PopulateDetl()
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

End Class
