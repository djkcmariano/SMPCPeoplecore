Imports clsLib
Imports System.IO
Imports System.Data
Partial Class Secured_EmpBUDS_Edit
    Inherits System.Web.UI.Page

    Dim TransNo As Int64
    Dim IsEnabled As Boolean = False
    Dim UserNo As Int64
    Dim PayLocNo As Integer

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeBUDS_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            PopulateCityPresent(Generic.ToInt(Me.hifCityNo.Value))
            PopulateCityHome(Generic.ToInt(Me.hifCityHomeNo.Value))
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))
            PopulateAddress(Generic.ToStr(txtPresentAddress.Text), 1)
            PopulateAddress(Generic.ToStr(txtHomeAddress.Text), 2)
            imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"

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

    End Sub


    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            ViewState("IsEnabled") = True
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)
        Generic.PopulateDataDisabled(Me, "Panel1", UserNo, PayLocNo, Generic.ToStr(Session("xMenuType")))
        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled

        'lnkSave3.Visible = IsEnabled
        'lnkModify2.Visible = Not IsEnabled



    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then

            Dim RetVal As Boolean = False
            Dim EmployeeCode As String = Generic.ToStr(txtEmployeeCode.Text)
            Dim NickName As String = Generic.ToStr(txtNickName.Text)
            Dim LastName As String = Generic.ToStr(txtLastName.Text)
            Dim FirstName As String = Generic.ToStr(txtFirstName.Text)
            Dim MiddleName As String = Generic.ToStr(txtMiddleName.Text)
            '10

            Dim PresentAddress As String = Generic.ToStr(txtHouseNo.Text) & "|" & Generic.ToStr(txtStreet.Text) & "|" & Generic.ToStr(txtSubd.Text) & "|" & Generic.ToStr(txtBarangay.Text)
            Dim HomeAddress As String = Generic.ToStr(txtHomeHouseNo.Text) & "|" & Generic.ToStr(txtHomeStreet.Text) & "|" & Generic.ToStr(txtHomeSubd.Text) & "|" & Generic.ToStr(txtHomeBarangay.Text)
            Dim PresentPhoneNo As String = "" 'Generic.ToStr(txtPresentPhoneNo.Text)
            Dim CityNo As Integer = Generic.ToInt(Me.hifCityNo.Value)
            Dim MobileNo As String = Generic.ToStr(txtMobileNo.Text)

            Dim homeno1 As String = Generic.ToStr(txtHomePhoneNo.Text)
            Dim homeno2 As String = Generic.ToStr(txtHomePhoneNo2.Text)
            Dim mobileno1 As String = Generic.ToStr(txtMobileNo.Text)
            Dim mobileno2 As String = Generic.ToStr(txtMobileNo2.Text)

            Dim directLine1 As String = Generic.ToStr(txtDirectLine1.Text)
            Dim directLine2 As String = Generic.ToStr(txtDirectLine2.Text)

            Dim faxNo As String = Generic.ToStr(txtFaxNo.Text)
            Dim LocalNo1 As String = Generic.ToStr(txtLocalNo.Text)
            Dim LocalNo2 As String = Generic.ToStr(txtLocalNo2.Text)

            Dim DisasterBrigadeTeamNo As Integer = Generic.ToInt(cboDisasterBrigadeTeamNo.SelectedValue)
            Dim DisasterControlTeamNo As Integer = Generic.ToInt(cboDisasterControlTeamNo.SelectedValue)
            Dim EvacuationTeamNo As Integer = Generic.ToInt(cboEvacuationTeamNo.SelectedValue)
            Dim FirstAidTeamNo As Integer = Generic.ToInt(cboFirstAidTeamNo.SelectedValue)
            Dim BusinessTeamNo As Integer = Generic.ToInt(cboBusinessTeamNo.SelectedValue)
            Dim CallTreeNumber As String = Generic.ToStr(txtCallTreeNumber.Text)
            Dim Principal As String = Generic.ToStr(txtPrincipal.Text)
            Dim Alternate As String = Generic.ToStr(txtAlternate.Text)
            Dim AlternateKeyPerson As String = Generic.ToStr(txtAlternateKeyPerson.Text)

            Dim CityHomeNo As Integer = Generic.ToInt(Me.hifCityHomeNo.Value)
            '20

            '30
            Dim SpouseBirthDate As String = ""
            Dim SpouseAddress As String = ""
            '40
            Dim SpouseBusinessAddress As String = ""
            Dim SpouseBusinessPhoneNo As String = ""
            Dim CompanyEmail As String = Generic.ToStr(txtCompanyEmail.Text)
            '//validate start here
            'Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            'Dim dtx As New DataTable
            'dtx = SQLHelper.ExecuteDataTable("EEmployee_WebValidateDetails", UserNo, TransNo, EmployeeCode, BarCode, FPID, CourtesyNo, NickName, LastName, FirstName, MiddleName, MaidenName, EmployeeExtNo, BirthDate, PayClassNo, GenderNo, CivilStatNo, MarriageDate, MarriagePlace, EmailAddress, CompanyEmail, PayLocNo)

            'For Each rowx As DataRow In dtx.Rows
            '    invalid = Generic.ToBol(rowx("Invalid"))
            '    messagedialog = Generic.ToStr(rowx("MessageDialog"))
            '    alerttype = Generic.ToStr(rowx("AlertType"))
            'Next

            'If invalid = True Then
            '    MessageBox.Alert(messagedialog, alerttype, Me)
            '    Exit Sub
            'End If
            Dim dt As DataTable, tstatus As Integer
            Dim error_num As Integer, error_message As String = ""
            dt = SQLHelper.ExecuteDataTable("EEmployeeBUDS_WebSave", UserNo, TransNo, homeno1, homeno2, mobileno1, mobileno2, directLine1, directLine2, faxNo, LocalNo1, LocalNo2, DisasterBrigadeTeamNo, DisasterControlTeamNo,
                                            EvacuationTeamNo, FirstAidTeamNo, BusinessTeamNo, CallTreeNumber, Principal, Alternate, AlternateKeyPerson)

            For Each row As DataRow In dt.Rows
                'tstatus = Generic.ToInt(row("tStatus"))
                'TransNo = Generic.ToInt(row("employeeno"))
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
            If RetVal Then
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                ViewState("IsEnabled") = False
                EnabledControls()
            End If

        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub



    Private Sub PopulateCityPresent(ByVal CityNo As Integer)
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("ECity_WebPopulate", CityNo)
        For Each row As DataRow In dt.Rows
            txtProvinceDesc.Text = Generic.ToStr(row("ProvinceDesc"))
            txtPostalCode.Text = Generic.ToStr(row("PostalCode"))
        Next
    End Sub

    Private Sub PopulateCityHome(ByVal CityNo As Integer)
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("ECity_WebPopulate", CityNo)
        For Each row As DataRow In dt.Rows
            txtProvinceDesc2.Text = Generic.ToStr(row("ProvinceDesc"))
        Next
    End Sub

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function PopulateCity(prefixText As String, count As Integer) As List(Of String)
        Dim items As New List(Of String)()
        Dim _ds As New DataSet()
        Dim UserNo As Integer = 0, PayLocNo As Integer
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        PayLocNo = (HttpContext.Current.Session("xPayLocNo"))

        _ds = SQLHelper.ExecuteDataSet("ECity_WebLookup_AC", 0, prefixText, 0)
        For Each row As DataRow In _ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("tDesc")),
                                Generic.ToStr(row("tNo")) & _
                                "|" & Generic.ToStr(row("ProvinceDesc")) & _
                                "|" & Generic.ToStr(row("RegionDesc")) & _
                                "|" & Generic.ToStr(row("PostalCode")) & _
                                "|" & Generic.ToStr(row("ProvinceNo")))
            items.Add(item)
        Next
        _ds.Dispose()
        Return items
    End Function






    Private Sub PopulateAddress(ByVal Address As String, index As Integer)
        Dim splitStr As String() = Address.Split("|"c)
        Try
            If index = 1 Then
                txtHouseNo.Text = splitStr(0).ToString()
                txtStreet.Text = splitStr(1).ToString()
                txtSubd.Text = splitStr(2).ToString()
                txtBarangay.Text = splitStr(3).ToString()
            ElseIf index = 2 Then
                txtHomeHouseNo.Text = splitStr(0).ToString()
                txtHomeStreet.Text = splitStr(1).ToString()
                txtHomeSubd.Text = splitStr(2).ToString()
                txtHomeBarangay.Text = splitStr(3).ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkCopy_Click(sender As Object, e As EventArgs)
        txtHouseNo.Text = txtHomeHouseNo.Text
        txtStreet.Text = txtHomeStreet.Text
        txtBarangay.Text = txtHomeBarangay.Text
        txtSubd.Text = txtHomeSubd.Text
        txtCityDesc.Text = txtCityHomeDesc.Text
        hifCityNo.Value = hifCityHomeNo.Value
        txtProvinceDesc.Text = txtProvinceDesc2.Text
        hifProvinceNo.Value = hifProvinceNo2.Value
    End Sub




End Class





