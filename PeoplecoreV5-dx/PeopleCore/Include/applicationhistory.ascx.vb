Imports System.Data
Imports clsLib

Partial Class Include_applicationhistory
    Inherits System.Web.UI.UserControl

    Dim UserNo As Integer
    Private _ID As Integer
    Public Property xID() As Integer
        Get
            Return _ID
        End Get
        Set(value As Integer)
            _ID = value
        End Set
    End Property

    Private _IsApplicant As Integer
    Public Property xIsApplicant() As Integer
        Get
            Return _IsApplicant
        End Get
        Set(value As Integer)
            _IsApplicant = value
        End Set
    End Property

    Public Sub Show()
        Dim yID As Integer = 0
        Dim yIsApplicant As Boolean = True

        yID = xID
        yIsApplicant = xIsApplicant

        PopulateGrid(yID, yIsApplicant)
        ModalPopupExtender1.Show()
    End Sub

    Private Sub PopulateGrid(ID As Integer, IsApplicant As Boolean)
        If IsApplicant Then
            grd.DataSource = SQLHelper.ExecuteDataSet("EApplicantRandomAns_WebApplication", UserNo, ID, 0)
            grd.DataBind()
        Else
            grd.DataSource = SQLHelper.ExecuteDataSet("EApplicantRandomAns_WebApplication", UserNo, 0, ID)
            grd.DataBind()
        End If
    End Sub

    
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
    End Sub
End Class

