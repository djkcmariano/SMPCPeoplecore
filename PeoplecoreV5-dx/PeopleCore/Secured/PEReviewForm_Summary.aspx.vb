Imports clsLib
Imports System.Data
Imports System.IO
Partial Class Secured_PEReviewForm_Summary
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer
    Dim pereviewmainno As Integer = 0
    Dim pereviewcateno As Integer = 0
    Dim pecatetypeno As Integer = 0
    Dim pereviewno As Integer = 0
    Dim pecycleno As Integer = 0
    Dim componentno As Integer = 0
    'Dim pereviewevaluatorno As Integer = 0
    Dim peevaluatorno As Integer = 0
    Dim TabNo As Integer = 0
    Dim Journal_ID As Integer = 0
    Dim OnlineEmpNo As Integer = 0
    Dim isposted As Boolean = False
    Dim FormName As String = ""

    Dim IsAddCate As Boolean = False
    Dim IsEditCate As Boolean = False
    Dim IsDeleteCate As Boolean = False
    Dim IsAddDim As Boolean = False
    Dim IsEditDim As Boolean = False
    Dim IsDeleteDim As Boolean = False
    Dim IsAddDeti As Boolean = False
    Dim IsEditDeti As Boolean = False
    Dim IsDeleteDeti As Boolean = False

    Dim _ds As New DataSet
    Dim _dt As New DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        OnlineEmpNo = Generic.ToInt(Session("EmployeeNo"))

        If Not IsPostBack Then
            PopulateGrid()
            'PopulateCombo()
        End If

        'AddHandler ChatBox1.lnkSendClick, AddressOf lnkSend_Click

    End Sub

    Protected Overrides Sub OnInit(e As EventArgs)
        MyBase.OnInit(e)
        pereviewmainno = Generic.ToInt(Request.QueryString("pereviewmainno"))
        pereviewcateno = Generic.ToInt(Request.QueryString("pereviewcateno"))
        pecatetypeno = Generic.ToInt(Request.QueryString("pecatetypeno"))
        pereviewno = Generic.ToInt(Request.QueryString("pereviewno"))
        peevaluatorno = Generic.ToInt(Request.QueryString("peevaluatorno"))
        pecycleno = Generic.ToInt(Request.QueryString("pecycleno"))
        componentno = Generic.ToInt(Request.QueryString("componentno"))
        isposted = Generic.ToBol(Request.QueryString("isposted"))
        Dim FileInfo As FileInfo = New FileInfo(System.Web.HttpContext.Current.Request.Url.AbsolutePath)
        FormName = Path.GetFileName(FileInfo.ToString)


        'PopulateHeader()
        'EnabledControls(pecatetypeno)
    End Sub

#Region "******** Performance Information ********"

    Protected Sub PopulateGrid()
        Try

            Dim EmployeeNo As Integer = 0
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEReview_WebInfo", UserNo, pereviewno)
            For Each row As DataRow In dt.Rows
                lblName.Text = Generic.ToStr(row("FullName"))
                EmployeeNo = Generic.ToInt(row("EmployeeNo"))
                lblCode.Text = Generic.ToStr(row("PEReviewCode"))
            Next

            imgPhoto.ImageUrl = "~/secured/frmShowImage.ashx?tNo=" & Generic.ToInt(EmployeeNo) & "&tIndex=2"

            rRef.DataSource = SQLHelper.ExecuteDataTable("EPEReview_WebInfo", UserNo, pereviewno)
            rRef.DataBind()


        Catch ex As Exception

        End Try
    End Sub

#End Region


End Class
