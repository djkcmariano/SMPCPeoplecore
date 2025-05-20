Imports System.Data
Imports clsLib

Partial Class Include_wucDTRDetailHeader
    Inherits System.Web.UI.UserControl
    Dim xpublicVar As New clsPublicVariable
    Dim DTRDetiLogNo As Integer = 0
    Dim showFrm As New clsFormControls
    Private Sub PopulateGrid()

        Dim _ds As New DataSet
        _ds = SQLHelper.ExecuteDataSet("EDTRDetiLog_WebOne", xpublicVar.xOnlineUseNo, DTRDetiLogNo)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                lblFullName.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("FullName"))
                lblEmployeeCode.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("EmployeeCode"))
                lblDTRDate.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("DTRDate"))
                lblShiftCode.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("ShiftCode"))
                lblDayTypeCode.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("DayTypeCode"))
                lblHrs.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Hrs"))
                lblWorkingHrs.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("WorkingHrs"))
                lblOvt.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("Ovt"))
                lblLeaveHrs.Text = Generic.ToStr(_ds.Tables(0).Rows(0)("LeaveHrs"))
            End If
        End If

    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        xpublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("Onlineuserno"), clsBase.clsBaseLibrary.enumObjectType.IntType)
        DTRDetiLogNo = Generic.CheckDBNull(Request.QueryString("transNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        PopulateGrid()

    End Sub

End Class
