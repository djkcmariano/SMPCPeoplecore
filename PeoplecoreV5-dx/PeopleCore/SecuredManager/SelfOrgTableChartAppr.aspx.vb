Imports clsLib
Imports System.Data

Partial Class SecuredManager_SelfOrgTableChartAppr
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer

    Private Sub PopulateChart()
        Try

            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataSet("EPlantilla_ChartAppr2", UserNo).Tables(0)

            DataBoundOrganisationChart1.AssistantItem.Size = OrgChart.Core.BackgroundImageSize.Large
            DataBoundOrganisationChart1.ChartItem.Size = OrgChart.Core.BackgroundImageSize.Large

            DataBoundOrganisationChart1.StartValue = dt.Rows(0)(0)
            DataBoundOrganisationChart1.DataSource = dt
            DataBoundOrganisationChart1.DataBind()

            For Each row As DataRow In dt.Rows
                lbl.Text = "<b>T.O. Title : " & Generic.ToStr(row("TableOrgDesc")) & "</b>"
            Next
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        Permission.IsAuthenticatedSuperior()

        PopulateChart()


        AddHandler DataBoundOrganisationChart1.ItemDropped, AddressOf DataBoundOrganisationChart1_ItemDropped

    End Sub

    Private Function UpdateParent(xid As String, xparentid As String) As Integer
        Return SQLHelper.ExecuteNonQuery("ETableOrg_UpdateParent", UserNo, TransNo, xid, xparentid)
    End Function

    Private Function DataBoundOrganisationChart1_ItemDropped(sender As Object, DraggedItemID As String, DroppedItemID As String) As Boolean
        If UpdateParent(DraggedItemID, DroppedItemID) > 0 Then
            PopulateChart()
            Return True
        Else
            Return False
        End If
    End Function

    Protected Sub lnkView_Click(sender As Object, e As EventArgs)
        'Generic.ClearControls(Me, "Panel1")
        'Generic.EnableControls(Me, "Panel1", False)
        'lnkSave.Enabled = False
        'PopulateData()
        'ModalPopupExtender1.Show()
    End Sub



End Class
