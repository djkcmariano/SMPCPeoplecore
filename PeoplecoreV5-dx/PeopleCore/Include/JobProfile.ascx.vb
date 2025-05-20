Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Include_JobProfile
    Inherits System.Web.UI.UserControl
    Dim UserNo As Integer

    Private _JDNo As Integer
    Public Property JDNo() As Integer
        Get
            Return _JDNo
        End Get
        Set(value As Integer)
            _JDNo = value
        End Set
    End Property

    Private _PlantillaNo As Integer
    Public Property PlantillaNo() As Integer
        Get
            Return _PlantillaNo
        End Get
        Set(value As Integer)
            _PlantillaNo = value
        End Set
    End Property

    Public Sub Show()
        Dim pJDNo As Integer = 0
        Dim pPlantillaNo As Integer = 0
        pJDNo = JDNo
        pPlantillaNo = PlantillaNo
        PopulateData(pPlantillaNo)
        PopulateGridEduc(pJDNo)
        PopulateGridExpe(pJDNo)
        PopulateGridComp(pJDNo)
        PopulateGridEligibility(pJDNo)
        PopulateGridTraining(pJDNo)


        ModalPopupExtender1.Show()
    End Sub

    Private Sub PopulateGridEduc(JDNo As Integer)
        Try
            Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataSet("EJDEduc_Web", 0, JDNo).Tables(0)
            grdEduc.DataSource = dt
            grdEduc.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub PopulateGridExpe(JDNo As Integer)
        Try
            Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataSet("EJDExpe_Web", 0, JDNo).Tables(0)
            grdExpe.DataSource = dt
            grdExpe.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub PopulateGridComp(JDNo As Integer)
        Try
            Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataSet("EJDComp_Web", 0, JDNo).Tables(0)
            grdComp.DataSource = dt
            grdComp.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PopulateGridEligibility(JDNo As Integer)
        Try
            Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataSet("EJDElig_Web", 0, JDNo).Tables(0)
            gridElig.DataSource = dt
            gridElig.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub PopulateGridTraining(JDNo As Integer)
        Try
            Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataSet("EJDTrn_Web", 0, JDNo).Tables(0)
            grdTrn.DataSource = dt
            grdTrn.DataBind()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub PopulateData(PlantillaNo As Integer)
        Try
            Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataSet("EJD_WebOne", Generic.ToInt(UserNo), JDNo).Tables(0)
            For Each row As DataRow In dt.Rows
                lblPositionDesc.Text = Generic.ToStr(row("PositionDesc"))
                lblReportingTo.Text = Generic.ToStr(row("ReportingTo"))
                lblSupervises.Text = Generic.ToStr(row("Supervises"))
                lblCoordinate.Text = Generic.ToStr(row("Coordinate"))
            Next

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
    End Sub
End Class
