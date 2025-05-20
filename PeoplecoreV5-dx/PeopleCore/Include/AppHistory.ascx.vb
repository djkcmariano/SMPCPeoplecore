Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Include_AppHistory
    Inherits System.Web.UI.UserControl

    Dim xMRNo As Integer
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

    Private _Desc As String
    Public Property xDesc() As String
        Get
            Return _Desc
        End Get
        Set(value As String)
            _Desc = value
        End Set
    End Property

    Public Sub Show()
        Dim yID As Integer = 0
        Dim yIsApplicant As Boolean = True

        Session("ID") = xID
        Session("IsApplicant") = xIsApplicant
        Session("FullName") = xDesc

        PopulateGrid(Session("ID"), Session("IsApplicant"))        

        lbl.Text = Session("FullName")
        ModalPopupExtender1.Show()
    End Sub

    Private Sub PopulateGrid(ID As Integer, IsApplicant As Boolean)
        If IsApplicant Then
            '    grdMain.DataSource = SQLHelper.ExecuteDataSet("EApplicantRandomAns_WebAppHistory", UserNo, ID, 0)
            '    grdMain.DataBind()
            Repeater1.DataSource = SQLHelper.ExecuteDataSet("EApplicantRandomAns_WebAppHistory", UserNo, ID, 0)
            Repeater1.DataBind()
        Else
            Repeater1.DataSource = SQLHelper.ExecuteDataSet("EApplicantRandomAns_WebAppHistory", UserNo, 0, ID)
            Repeater1.DataBind()
            'grdMain.DataSource = SQLHelper.ExecuteDataSet("EApplicantRandomAns_WebAppHistory", UserNo, 0, ID)
            '    grdMain.DataBind()
        End If



    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
    End Sub

    'Protected Sub detailGrid_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
    '    xMRNo = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    '    PopulategridStatus(xMRNo, Session("ID"), Session("IsApplicant"), 0)
    'End Sub

    Protected Sub lnkDetails_Click(sender As Object, e As EventArgs)
        ''PopulateGrid(Session("ID"), Session("IsApplicant"))
        'Session("MRNo") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()

        'PopulategridStatus(Session("MRNo"), Session("ID"), Session("IsApplicant"), 0)
        'MessageBox.Alert(grdMain.GetSelectedFieldValues().ToString(), "information", Me)

    End Sub



    'Private Sub PopulategridStatus(mrNo As Integer, fapplicantno As Integer, fisapplicant As Boolean, femployeeno As Integer)
    '    Try
    '        Dim _ds As New DataSet
    '        _ds = SQLHelper.ExecuteDataSet("EApplicantRandomAns_WebApplicationHistory", 0, fapplicantno, mrNo, femployeeno)

    '        Dim grdDetl As New ASPxGridView
    '        grdDetl = grdMain.FindDetailRowTemplateControl(0, "grdDetl")
    '        grdDetl.DataSource = _ds
    '        grdDetl.DataBind()
    '    Catch ex As Exception

    '    End Try


    'End Sub

    Protected Sub Repeater1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim hf As New HiddenField
            Dim grdDetl As New ASPxGridView
            Dim _ds As New DataSet
            hf = e.Item.FindControl("hfMRNo")
            grdDetl = e.Item.FindControl("grdDetl")

            _ds = SQLHelper.ExecuteDataSet("EApplicantRandomAns_WebApplicationHistory", 0, Session("ID"), Generic.ToInt(hf.Value), 0)
            grdDetl.DataSource = _ds
            grdDetl.DataBind()
        End If

    End Sub
End Class
