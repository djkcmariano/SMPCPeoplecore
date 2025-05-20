Imports clsLib
Imports System.Data

Partial Class Secured_EmpBenSALNDetails
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim EmployeeNo As Integer
    Dim PayLocNo As Integer
    Dim IsEnabled As Boolean = False
    Dim TransNo As Integer

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Session("SALNNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TAB.TransactionNo = TransNo
        lbl.Text = "<STRONG>Transaction No.: </STRONG>" & IIf(Session("SALNCode") > "", Session("SALNCode"), "Autonumber")
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("ESALN_WebOne", UserNo, TransNo, PayLocNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))

            If Not IsPostBack Then
                Me.rdoIsJoint1.Checked = Generic.ToInt(row("IsJoint1"))
                Me.rdoIsJoint2.Checked = Generic.ToInt(row("IsJoint2"))
                Me.rdoIsNA.Checked = Generic.ToInt(row("IsNA"))
                Me.rdoIsMarried1.Checked = Generic.ToInt(row("IsMarried1"))
                Me.rdoIsMarried2.Checked = Generic.ToInt(row("IsMarried2"))
            End If

        Next
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)
        If TransNo = 0 Then : ViewState("IsEnabled") = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If
        If Not IsPostBack Then
            If TransNo = 0 Then
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            End If
            PopulateData()
        End If
        EnabledControls()

        If Session("IsEnabled") = False Then
            Me.lnkModify.Visible = Generic.ToBol(Session("IsEnabled"))
            Me.lnkSave.Visible = Generic.ToBol(Session("IsEnabled"))
        End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim SALNNo As Integer
        Dim RetVal As Integer = 0
        Dim xMessage As String = ""
        Dim _ds As New DataSet

        If Me.txtSALNNo.Text = "Autonumber" Then Me.txtSALNNo.Text = 0

        SALNNo = Generic.ToInt(txtSALNNo.Text)

        Dim ApplicableYear As String = Generic.ToStr(Me.txtApplicableYear.Text)
        Dim LName As String = Generic.ToStr(Me.txtLName.Text)
        Dim FName As String = Generic.ToStr(Me.txtFName.Text)
        Dim MName As String = Generic.ToStr(Me.txtMName.Text)
        Dim Address As String = Generic.ToStr(Me.txtAddress.Text)
        Dim Income As Double = Generic.ToDbl(Me.txtIncome.Text)
        Dim PositionNo As Integer = Generic.ToInt(Me.cboPositionNo.SelectedValue)
        Dim Office As String = Generic.ToStr(Me.txtOffice.Text)
        Dim OfficeAddress As String = Generic.ToStr(Me.txtOfficeAddress.Text)
        Dim CTCN1 As String = Generic.ToStr(Me.txtCTCN1.Text)
        Dim IssuedAt1 As String = Generic.ToStr(Me.txtIssuedAt1.Text)
        Dim IssuedOn1 As String = Generic.ToStr(Me.txtIssuedOn1.Text)
        Dim SpouseLName As String = Generic.ToStr(Me.txtSpouseLName.Text)
        Dim SpouseFName As String = Generic.ToStr(Me.txtSpouseFName.Text)
        Dim SpouseMName As String = Generic.ToStr(Me.txtSpouseMName.Text)
        Dim SpousePosition As String = Generic.ToStr(Me.txtSpousePosition.Text)
        Dim SpouseOffice As String = Generic.ToStr(Me.txtSpouseOffice.Text)
        Dim TIN As String = Generic.ToStr(Me.txtTIN.Text)
        Dim ctcn2 As String = Generic.ToStr(Me.txtCTCN2.Text)
        Dim IssuedAt2 As String = Generic.ToStr(Me.txtIssuedAt2.Text)
        Dim IssuedOn2 As String = Generic.ToStr(Me.txtIssuedOn2.Text)
        Dim TIN1 As String = Generic.ToStr(Me.txtTIN1.Text)
        Dim date_sign As String = Generic.ToStr(Me.txtDateAccomplished.Text)
        Dim SpouseOfficeAddress As String = Generic.ToStr(Me.txtSpouseOfficeAddress.Text)
        Dim xrd1 As Boolean = Generic.ToInt(Me.rdoIsJoint1.Checked)
        Dim xrd2 As Boolean = Generic.ToInt(Me.rdoIsJoint2.Checked)
        Dim IsNA As Boolean = Generic.ToInt(Me.rdoIsNA.Checked)
        Dim yrd1 As Boolean = Generic.ToInt(Me.rdoIsMarried1.Checked)
        Dim yrd2 As Boolean = Generic.ToInt(Me.rdoIsMarried2.Checked)

        If xrd2 = False Then xrd1 = True
        If xrd2 = True Then xrd1 = False
        If IsNA = True Then xrd1 = False : xrd2 = False

        If yrd2 = True Then yrd1 = False
        If yrd2 = False Then yrd1 = True

        Dim dt As DataTable, tstatus As Integer
        Dim error_num As Integer, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("ESALN_WebSave", UserNo, Generic.ToInt(SALNNo), ApplicableYear, LName, FName, MName, Address, Income, PositionNo, Office, OfficeAddress, CTCN1, IssuedAt1, IssuedOn1, SpouseLName, SpouseFName, SpouseMName, SpousePosition, SpouseOffice, TIN, ctcn2, IssuedAt2, IssuedOn2, date_sign, TIN1, SpouseOfficeAddress, yrd1, xrd1, IsNA, PayLocNo)

        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                RetVal = Generic.ToInt(_ds.Tables(0).Rows(0)("Status"))
                xMessage = Generic.ToStr(_ds.Tables(0).Rows(0)("xMessage"))
            End If
        End If

        For Each row As DataRow In dt.Rows
            tstatus = Generic.ToInt(row("tStatus"))
            TransNo = Generic.ToInt(row("SALNNo"))
            RetVal = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                RetVal = False
            End If
        Next
        If RetVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If RetVal = True Then

            If Generic.ToInt(Request.QueryString("id")) = 0 Then
                Dim xURL As String = "EmpBenSALNDetails.aspx?id=" & TransNo
                MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, xURL)
            Else
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                ViewState("IsEnabled") = False
                EnabledControls()
            End If
            PopulateData()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        ViewState("IsEnabled") = True
        EnabledControls()
    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)
        Generic.PopulateDataDisabled(Me, "Panel1", UserNo, PayLocNo, Generic.ToStr(Session("xMenuType")))

        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled

    End Sub


    Public Sub xrdIsMarriedChangeLoad(ByVal sender As Object, ByVal e As EventArgs)
        Dim rd As New RadioButton
        rd = sender
        If rd.ID = "rdoIsMarried2" Then
            PopulateData()
            Me.txtSpouseFName.Text = "" : Me.txtSpouseFName.Enabled = False
            Me.txtSpouseLName.Text = "" : Me.txtSpouseLName.Enabled = False
            Me.txtSpouseMName.Text = "" : Me.txtSpouseMName.Enabled = False
            Me.txtSpouseOffice.Text = "" : Me.txtSpouseOffice.Enabled = False
            Me.txtSpouseOfficeAddress.Text = "" : Me.txtSpouseOfficeAddress.Enabled = False
            Me.txtSpousePosition.Text = "" : Me.txtSpousePosition.Enabled = False
            Me.txtTIN.Text = "" : Me.txtTIN.Enabled = False
            Me.txtCTCN2.Text = "" : Me.txtCTCN2.Enabled = False
            Me.txtIssuedAt2.Text = "" : Me.txtIssuedAt2.Enabled = False
            Me.txtIssuedOn2.Text = "" : Me.txtIssuedOn2.Enabled = False
        ElseIf rd.ID = "rdoIsMarried1" Then
            PopulateData()
            Me.txtSpouseFName.Enabled = True
            Me.txtSpouseLName.Enabled = True
            Me.txtSpouseMName.Enabled = True
            Me.txtSpouseOffice.Enabled = True
            Me.txtSpouseOfficeAddress.Enabled = True
            Me.txtSpousePosition.Enabled = True
            Me.txtTIN.Enabled = True
            Me.txtCTCN2.Enabled = True
            Me.txtIssuedAt2.Enabled = True
            Me.txtIssuedOn2.Enabled = True
        End If


    End Sub

End Class
