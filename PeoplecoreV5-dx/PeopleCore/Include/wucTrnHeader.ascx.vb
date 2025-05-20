Imports System.Data
Imports clsLib

Partial Class Include_wucPayHeader
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

        Dim PayScheduleDesc As String = ""
        Dim MonthDesc As String = ""
        Dim ApplicableYear As String = ""
        Dim _ds As New DataSet

        If ID > 0 Then
            tno = ID
        End If

        _ds = SQLHelper.ExecuteDataSet("ETrnTaken_WebOne", xpublicVar.xOnlineUseNo, tno)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                lblCode.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Code"))
                lblTrainingDate.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("TrainingDate"))
                lblTrnTitleDesc.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("TrnTitleDesc"))
                lblEnrolTypeDesc.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("TrnEnrollTypeDesc"))
                lblTrainingTime.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("TrainingTime"))
                lblMinimumSeats.Text = Generic.ToInt(_ds.Tables(0).Rows(0)("MinimumSeats"))
                lblMaximumSeats.Text = Generic.ToInt(_ds.Tables(0).Rows(0)("MaximumSeats"))
                lblTrnStatDesc.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("TrnStatDesc"))
            End If
        End If

    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        xpublicVar.xOnlineUseNo = Generic.ToInt(Session("Onlineuserno"))
        tno = Generic.ToInt(Request.QueryString("id"))

        PopulateGrid()

    End Sub

End Class
