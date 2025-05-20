Imports System.Data
Imports clsLib

Partial Class Include_wucDTRHeader
    Inherits System.Web.UI.UserControl
    Dim xpublicVar As New clsPublicVariable
    Dim dtrNo As Integer = 0
    Dim showFrm As New clsFormControls
    Private Sub PopulateGrid()

        Dim _ds As New DataSet
        _ds = SQLHelper.ExecuteDataSet("EDTR_WebOne", xpublicVar.xOnlineUseNo, dtrNo)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                lblDTRCode.Text = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("DTRCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblPayClassDesc.Text = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("PayClassDesc"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblDTRCutoff.Text = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("StartDate"), clsBase.clsBaseLibrary.enumObjectType.StrType) & " - " & Generic.CheckDBNull(_ds.Tables(0).Rows(0)("EndDate"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblPayTypeDesc.Text = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("PayTypeDesc"), clsBase.clsBaseLibrary.enumObjectType.StrType)

            End If
        End If

    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        xpublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("Onlineuserno"), clsBase.clsBaseLibrary.enumObjectType.IntType)
        dtrNo = Generic.CheckDBNull(Request.QueryString("transNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        PopulateGrid()

    End Sub

End Class
