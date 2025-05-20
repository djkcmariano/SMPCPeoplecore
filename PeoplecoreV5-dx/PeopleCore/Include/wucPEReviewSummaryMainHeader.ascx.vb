Imports System.Data
Imports clsLib

Partial Class Include_wucPEReviewSummaryMainHeader
    Inherits System.Web.UI.UserControl
    Dim xpublicVar As New clsPublicVariable
    Dim tno As Integer = 0
    Dim showFrm As New clsFormControls
    Private _ID As Integer
    Public Property xID() As Integer
        Get
            Return _ID
        End Get
        Set(value As Integer)
            _ID = value
        End Set
    End Property
    Private Sub PopulateGrid()

        If ID > 0 Then
            tno = ID
        End If

        Dim _ds As New DataSet
        _ds = SQLHelper.ExecuteDataSet("EPEReviewSummaryMain_WebOne", xpublicVar.xOnlineUseNo, tno)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                lblCode.Text = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("Code"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblRemarks.Text = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("Remarks"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblPEPeriodDesc.Text = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("PEPeriodDesc"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                lblApplicableYear.Text = Generic.CheckDBNull(_ds.Tables(0).Rows(0)("ApplicableYear"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            End If
        End If

    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        xpublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("Onlineuserno"), clsBase.clsBaseLibrary.enumObjectType.IntType)
        tno = Generic.CheckDBNull(Request.QueryString("id"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        PopulateGrid()

    End Sub

End Class
