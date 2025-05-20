Imports clsLib
Imports System.Data

Partial Class Include_Info
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

        PopulateEduc(yID, yIsApplicant)
        PopulateExam(yID, yIsApplicant)
        PopulateExpe(yID, yIsApplicant)
        PopulateTrain(yID, yIsApplicant)
        PopulateData(yID, yIsApplicant)
        ModalPopupExtender1.Show()
    End Sub

    Private Sub PopulateEduc(ID As Integer, IsApplicant As Boolean)
        If IsApplicant Then
            grdEduc.DataSource = SQLHelper.ExecuteDataSet("EApplicantEduc_Web", UserNo, ID)
            grdEduc.DataBind()
        Else
            grdEduc.DataSource = SQLHelper.ExecuteDataSet("EEmployeeEduc_Web", UserNo, ID)
            grdEduc.DataBind()
        End If
    End Sub

    Private Sub PopulateExam(ID As Integer, IsApplicant As Boolean)
        If IsApplicant Then
            grdExam.DataSource = SQLHelper.ExecuteDataSet("EApplicantExam_Web", UserNo, ID)
            grdExam.DataBind()
        Else
            grdExam.DataSource = SQLHelper.ExecuteDataSet("EEmployeeExam_Web", UserNo, ID)
            grdExam.DataBind()
        End If
    End Sub

    Private Sub PopulateExpe(ID As Integer, IsApplicant As Boolean)
        If IsApplicant Then
            grdExpe.DataSource = SQLHelper.ExecuteDataSet("EApplicantExpe_Web", UserNo, ID)
            grdExpe.DataBind()
        Else
            grdExpe.DataSource = SQLHelper.ExecuteDataSet("EEmployeeExpe_Web", UserNo, ID)
            grdExpe.DataBind()
        End If
    End Sub

    Private Sub PopulateTrain(ID As Integer, IsApplicant As Boolean)
        If IsApplicant Then
            grdTrain.DataSource = SQLHelper.ExecuteDataSet("EApplicantTrain_Web", UserNo, ID)
            grdTrain.DataBind()
        Else
            grdTrain.DataSource = SQLHelper.ExecuteDataSet("EEmployeeTrain_Web", UserNo, ID)
            grdTrain.DataBind()
        End If
    End Sub

    Private Sub PopulateData(ID As Integer, IsApplicant As Boolean)
        Dim Type As Integer = 1

        If IsApplicant = False Then
            Type = 2
        End If
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataSet("EApplicant_JobMatchOne", ID, IsApplicant).Tables(0)
            For Each row As DataRow In dt.Rows
                imgPic.ImageUrl = "~/secured/frmShowImage.ashx?tNo=" & ID.ToString() & "&tIndex=" & Type.ToString()
                lblInfo.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
    End Sub
End Class
