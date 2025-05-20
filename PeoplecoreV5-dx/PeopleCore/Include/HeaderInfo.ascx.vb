Imports System.Data
Imports clsLib

Partial Class Include_wucDTRHeader
    Inherits System.Web.UI.UserControl
    Dim xpublicVar As New clsPublicVariable
    Dim TransNo As Integer = 0
    Dim ApplicantNo As Integer = 0
    Dim EmployeeNo As Integer = 0
    Dim showFrm As New clsFormControls
    Private _FormName As String
    Public Property xFormName() As String
        Get
            Return _FormName
        End Get
        Set(value As String)
            _FormName = value
        End Set
    End Property

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EHeader_WebInfo", xpublicVar.xOnlineUseNo, _FormName, TransNo, EmployeeNo, ApplicantNo)
            For Each row As DataRow In dt.Rows
                lblName.Text = Generic.ToStr(row("FullName"))
                EmployeeNo = Generic.ToInt(row("EmployeeNo"))
                ApplicantNo = Generic.ToInt(row("ApplicantNo"))
            Next

            Dim tIndex As Integer, tno As Integer
            tIndex = 2
            tno = EmployeeNo
            If ApplicantNo > 0 Then
                tIndex = 1
                tno = ApplicantNo
            End If

            imgPhoto.ImageUrl = "~/secured/frmShowImage.ashx?tNo=" & Generic.ToInt(tno) & "&tIndex=" & tIndex

            rRef.DataSource = SQLHelper.ExecuteDataTable("EHeader_WebInfo", xpublicVar.xOnlineUseNo, _FormName, TransNo, EmployeeNo, ApplicantNo)
            rRef.DataBind()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        xpublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("Onlineuserno"), clsBase.clsBaseLibrary.enumObjectType.IntType)
        TransNo = Generic.CheckDBNull(Request.QueryString("id"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        EmployeeNo = Generic.CheckDBNull(Request.QueryString("emp"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        ApplicantNo = Generic.CheckDBNull(Request.QueryString("app"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        If Not IsPostBack Then
            PopulateGrid()
        End If

    End Sub

End Class
