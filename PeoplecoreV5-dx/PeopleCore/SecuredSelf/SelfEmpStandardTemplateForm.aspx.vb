Imports System.Data
Imports clsLib

Partial Class SecuredSelf_SelfEmpStandardTemplateForm
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        Permission.IsAuthenticated()

        If Not IsPostBack Then
            HeaderInfo1.xFormName = "EEmployee"
        End If

        'AddHandler UserControl1.UCButtonClick, AddressOf UserControl1_UCButtonClick
        StandardTemplate1.SaveRedirect = "../securedself/SelfEmpStandardHeader_EI.aspx"
        AddHandler StandardTemplate1.lnkSaveClick, AddressOf SaveStat

    End Sub

    Private Sub SaveStat()
        Dim TemplateID As Integer = Generic.ToInt(Request.QueryString("TemplateID"))        
        Dim EmployeeNo As Integer = Generic.ToInt(Request.QueryString("emp"))
        Dim TransNo As Integer = Generic.ToInt(Request.QueryString("TransNo"))
        'Dim retVal As Boolean
        'retVal = StandardTemplate1.RetVal
        'If retVal Then
        ' SQLHelper.ExecuteNonQuery("EEmployeeEI_WebStatUpdate", UserNo, TransNo, 1)
        'End If
    End Sub

End Class

