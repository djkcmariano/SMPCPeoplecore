Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Include_EmpInfo
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


    Public Sub Show()
        Dim yID As Integer = 0

        yID = xID

        PopulateEduc(yID)
        PopulateExam(yID)
        PopulateExpe(yID)
        PopulateData(yID)
        PopulateEmpJD(yID)
        PopulateEmpTrn(yID)
        PopulateEmpComp(yID)
        PopulateEmpReview(yID)
        ModalPopupExtender1.Show()
    End Sub

    Private Sub PopulateEduc(ID As Integer)

        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeEduc_Web", UserNo, ID)
            grdEduc.DataSource = dt
            grdEduc.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub PopulateExam(ID As Integer)

        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeExam_Web", UserNo, ID)
            grdExam.DataSource = dt
            grdExam.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub PopulateExpe(ID As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeExpe_Web", UserNo, ID)
            grdExpe.DataSource = dt
            grdExpe.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateData(ID As Integer)

        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataSet("EEmployee_WebProfile", ID).Tables(0)
            For Each row As DataRow In dt.Rows
                imgPic.ImageUrl = "~/secured/frmShowImage.ashx?tNo=" & ID.ToString() & "&tIndex=2"
                lblInfo1.Text = Generic.ToStr(row("Display1"))
                lblInfo2.Text = Generic.ToStr(row("Display2"))
                lblInfo3.Text = Generic.ToStr(row("Display3"))
            Next
        Catch ex As Exception

        End Try
    End Sub
 
    Private Sub PopulateEmpJD(ID As Integer)
        Dim str As String = ""
        Try
            Dim ds As DataSet
            Dim dt As DataTable
            ds = SQLHelper.ExecuteDataSet("EEmpJD_Web", UserNo, ID)
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

            'Dim dtGroup As DataTable
            'dtGroup = ds.Tables(1).DefaultView.ToTable(True, "Title")
            'If dtGroup.Rows.Count > 0 Then
            '    str = str & "<div class='form-group'><div class='row'><div class='col-md-12 header'>Qualification Standard</div></div>"
            'End If
            'For Each rowGroup As DataRow In dtGroup.Rows
            '    str = str & "<div class='row'><div class='col-md-10'><b>" & Generic.ToStr(rowGroup("Title")) & "</b></div></div><ul>"
            '    For Each row As DataRow In ds.Tables(1).Select("Title='" & rowGroup("Title") & "'")
            '        str = str & "<li>" & Generic.ToStr(row("Value")) & "</li>"
            '    Next
            '    str = str & "</ul>"
            'Next
            str = str & "</div>"
            lContent.Text = str
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PopulateEmpTrn(ID As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EJDTrnTO_Web", UserNo, ID)
            grdEmpTrn.DataSource = dt
            grdEmpTrn.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PopulateEmpComp(ID As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EJDCompTO_Web", UserNo, ID)
            grdEmpComp.DataSource = dt
            grdEmpComp.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub PopulateEmpReview(ID As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEReviewSummaryTO_Web", UserNo, ID)
            grdEmpReview.DataSource = dt
            grdEmpReview.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
    End Sub
End Class
