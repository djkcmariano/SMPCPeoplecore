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

        _ds = SQLHelper.ExecuteDataSet("EPay_WebOne", xpublicVar.xOnlineUseNo, tno)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                lblPayCode.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("PayCode"))
                lblPayClassDesc.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("PayClassDesc"))
                lblPayCutoff.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("PayStartdate")) & " - " & Generic.ToStr(_ds.Tables(0).Rows(0)("PayEndDate"))
                lblPayDate.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("PayDate"))
                lblPayPeriod.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("PayPeriod"))
                PayScheduleDesc = Generic.ToStr(_ds.Tables(0).Rows(0)("PayScheduleDesc"))
                MonthDesc = Generic.ToStr(_ds.Tables(0).Rows(0)("MonthDesc"))
                ApplicableYear = Generic.ToStr(_ds.Tables(0).Rows(0)("ApplicableYear"))

                If PayScheduleDesc > "" Then
                    lblPayScheduleDesc.Text = PayScheduleDesc & " of " & MonthDesc & ", " & ApplicableYear
                Else
                    lblPayScheduleDesc.Text = MonthDesc & ", " & ApplicableYear
                End If

            End If
        End If

    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        xpublicVar.xOnlineUseNo = Generic.ToInt(Session("Onlineuserno"))
        tno = Generic.ToInt(Request.QueryString("id"))

        PopulateGrid()

    End Sub

End Class
