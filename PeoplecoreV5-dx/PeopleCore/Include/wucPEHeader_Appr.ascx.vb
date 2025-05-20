Imports System.Data
Imports clsLib

Partial Class Include_wucPETemplateHeader
    Inherits System.Web.UI.UserControl

    Dim Lindex As Integer
    Dim xBase As New clsBase.clsBaseLibrary
    Dim xPublicVar As New clsPublicVariable
    Dim SQLHelp As New clsBase.SQLHelper
    Dim clsArray As New clsBase.clsArray
    Dim transNo As Integer = 0
    Dim INoDetail As Integer = 0
    Dim INoCate As Integer = 0
    Dim ApplicantNo As Integer = 0
    Dim MenuType As Integer = 0
    Dim IsClickMain As Integer = 0


    Private Sub PopulateFormMain()
        Dim _ds As New DataSet, tcount As Integer
        Dim dcount As Integer = 0
        _ds = SQLHelper.ExecuteDataSet("EPEReview_WebInfo", xPublicVar.xOnlineUseNo, INoDetail)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then

                txtEmployeeCode.Text = xBase.CheckDBNull(_ds.Tables(0).Rows(tcount)("EmployeeCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                txtPositionDesc.Text = xBase.CheckDBNull(_ds.Tables(0).Rows(tcount)("PositionDesc"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                txtApplicableYear.Text = xBase.CheckDBNull(_ds.Tables(0).Rows(tcount)("ApplicableYear"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                txtFullName.Text = xBase.CheckDBNull(_ds.Tables(0).Rows(tcount)("FullName"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                txtDepartmentDesc.Text = xBase.CheckDBNull(_ds.Tables(0).Rows(tcount)("DepartmentDesc"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                txtPEPeriodDesc.Text = xBase.CheckDBNull(_ds.Tables(0).Rows(tcount)("PEPeriodDesc"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                txtHiredDate.Text = xBase.CheckDBNull(_ds.Tables(0).Rows(tcount)("HiredDate"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                txtSuperiorName.Text = xBase.CheckDBNull(_ds.Tables(0).Rows(tcount)("SuperiorName"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                txtCode.Text = xBase.CheckDBNull(_ds.Tables(0).Rows(tcount)("Code"), clsBase.clsBaseLibrary.enumObjectType.StrType)

            End If
        End If

    End Sub

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        xPublicVar.xOnlineUseNo = xBase.CheckDBNull(Session("onlineuserno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        transNo = xBase.CheckDBNull(Request.QueryString("INo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        INoDetail = xBase.CheckDBNull(Request.QueryString("INoDetail"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        INoCate = xBase.CheckDBNull(Request.QueryString("INoCate"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        IsClickMain = xBase.CheckDBNull(Request.QueryString("IsClickMain"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        If INoDetail = 0 Then
            INoDetail = xBase.CheckDBNull(Request.QueryString("pereviewno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        End If

        PopulateFormMain()
    End Sub
End Class
