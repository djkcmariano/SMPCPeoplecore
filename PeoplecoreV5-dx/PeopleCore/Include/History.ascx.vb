Imports clsLib
Imports System.Data
Imports System.IO
Imports DevExpress.Web

Partial Class Include_History
    Inherits System.Web.UI.UserControl
    Dim UserNo As Integer
    Dim yID As Integer = 0
    Dim PayLocNo As Integer = 0

    Private _ID As Integer
    Public Property xID() As Integer
        Get
            Return _ID
        End Get
        Set(value As Integer)
            _ID = value
        End Set
    End Property

    Private _xModify As Boolean
    Public Property xModify() As Boolean
        Get
            Return _xModify
        End Get
        Set(value As Boolean)
            _xModify = value
        End Set
    End Property

    Private _xMenuType As String
    Public Property xMenuType() As String
        Get
            Return _xMenuType
        End Get
        Set(value As String)
            _xMenuType = value
        End Set
    End Property

    Public Sub Show()
        yID = xID
        hifNo.Value = yID
        PopulateGrid()
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PopulateGrid()

    End Sub

    Protected Sub PopulateGrid()
        If Generic.ToStr(xMenuType) = "" Then
            xMenuType = Generic.ToStr(Session("xMenuType"))
        End If

        If Generic.ToStr(xMenuType) = "0507010000" Then
            Try
                Dim _dt As DataTable
                _dt = SQLHelper.ExecuteDataTable("EPayProcessLog_Web", UserNo, PayLocNo, Generic.ToInt(hifNo.Value))
                Me.grdMain.DataSource = _dt
                Me.grdMain.DataBind()
            Catch ex As Exception

            End Try
        Else

            Try
                Dim _dt As DataTable
                _dt = SQLHelper.ExecuteDataTable("EDTRProcessLog_Web", UserNo, PayLocNo, Generic.ToInt(hifNo.Value))
                Me.grdMain.DataSource = _dt
                Me.grdMain.DataBind()
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class
