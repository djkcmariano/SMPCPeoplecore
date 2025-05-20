Imports clsLib
Imports System.Data

Partial Class Include_JobDescription
    Inherits System.Web.UI.UserControl
    Dim UserNo As Integer = 0

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

    Private _PositionNo As Integer
    Public Property PositionNo() As Integer
        Get
            Return _PositionNo
        End Get
        Set(value As Integer)
            _PositionNo = value
        End Set
    End Property

    Public Sub Show()
        'Dim pJDNo As Integer = 0
        'Dim pPlantillaNo As Integer = 0
        'pJDNo = JDNo
        'pPlantillaNo = PlantillaNo
        'PopulateGridEduc(pJDNo)
        'PopulateGridExpe(pJDNo)
        'PopulateGridComp(pJDNo)
        'PopulateGridEligibility(pJDNo)
        'PopulateGridTraining(pJDNo)
        PopulateData(JDNo)
        ModalPopupExtender1.Show()
    End Sub

    Private Sub PopulateData(JDNo As Integer)
        Dim str As String = ""
        Try
            Dim ds As DataSet
            Dim dt As DataTable
            ds = SQLHelper.ExecuteDataSet("EJD_Display", 0, JDNo)
            dt = ds.Tables(0)
            For Each row As DataRow In dt.Rows
                str = str & "<div class='form-group'>"
                If Generic.ToInt(row("WithHeader")) = 0 Then
                    str = str & "<label class='col-md-3'>" & Generic.ToStr(row("Title")) & "</label>"
                    str = str & "<span class='col-md-9'>" & Generic.ToStr(row("Value")) & "</span>"
                ElseIf Generic.ToInt(row("WithHeader")) = 1 Then
                    str = str & "<div class='row'><div class='col-md-12 header'>" & Generic.ToStr(row("Title")) & "</div></div>"
                    str = str & "<div class='row'><div class='col-md-10'>" & Generic.ToStr(row("Value")) & "</div></div><br />"
                End If
                str = str & "</div>"
            Next

            Dim dtGroup As DataTable
            dtGroup = ds.Tables(1).DefaultView.ToTable(True, "Title")
            If dtGroup.Rows.Count > 0 Then
                str = str & "<div class='form-group'><div class='row'><div class='col-md-12 header'>Qualification Standard</div></div>"
            End If
            For Each rowGroup As DataRow In dtGroup.Rows
                str = str & "<div class='row'><div class='col-md-10'><b>" & Generic.ToStr(rowGroup("Title")) & "</b></div></div><ul>"
                For Each row As DataRow In ds.Tables(1).Select("Title='" & rowGroup("Title") & "'")
                    str = str & "<li>" & Generic.ToStr(row("Value")) & "</li>"
                Next
                str = str & "</ul>"
            Next
            str = str & "</div>"
            lContent.Text = str
        Catch ex As Exception

        End Try
    End Sub

End Class
