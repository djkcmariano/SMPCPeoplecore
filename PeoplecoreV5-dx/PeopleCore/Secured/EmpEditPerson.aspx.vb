Imports clsLib
Imports System.IO
Imports System.Data


Partial Class Secured_EmpPersonEdit
    Inherits System.Web.UI.Page

    Dim TransNo As Int64
    Dim IsEnabled As Boolean = False
    Dim UserNo As Int64
    Dim PayLocNo As Integer

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployee_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            PopulateCityPresent(Generic.ToInt(Me.hifCityNo.Value))
            PopulateCityHome(Generic.ToInt(Me.hifCityHomeNo.Value))
            Generic.PopulateDropDownList_Union(UserNo, Me, "Panel1", dt, Generic.ToInt(Session("xPayLocNo")))
            PopulateAddress(Generic.ToStr(txtPresentAddress.Text), 1)
            PopulateAddress(Generic.ToStr(txtHomeAddress.Text), 2)

            txtSpouseFirstName.Text = Generic.Split(Generic.ToStr(row("SpouseName")), 0)
            txtSpouseMiddleName.Text = Generic.Split(Generic.ToStr(row("SpouseName")), 1)
            txtSpouseLastName.Text = Generic.Split(Generic.ToStr(row("SpouseName")), 2)
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
            PopulateTabHeader()
        End If
        EnabledControls()
        If UserNo = Generic.ToInt(txtUserNo.Text) Then
            lnkModify.Visible = False
            lnkModify2.Visible = False

        Else
            lnkModify.Visible = True
            lnkModify2.Visible = True
        End If

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
        txtBirthAge.Enabled = False
        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
        lnkUpload.Enabled = IsEnabled

        lnkSave3.Visible = IsEnabled
        lnkModify2.Visible = Not IsEnabled

        chkIsDualCitizenship_CheckedChanged()

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then

            If DateDiff(DateInterval.Year, CDate(txtBirthDate.Text), Now.Date) < 15 Then
                MessageBox.Warning("Applicant must be 18 years old and above!", Me)
                Exit Sub
            End If

            Dim RetVal As Boolean = False
            Dim EmployeeCode As String = Generic.ToStr(txtEmployeeCode.Text)
            Dim BarCode As String = Generic.ToStr(txtBarCode.Text)
            Dim FPID As String = Generic.ToStr(txtFPID.Text)
            Dim CourtesyNo As Integer = Generic.ToInt(cboCourtesyNo.SelectedValue)
            Dim NickName As String = Generic.ToStr(txtNickName.Text)
            Dim LastName As String = Generic.ToStr(txtLastName.Text)
            Dim FirstName As String = Generic.ToStr(txtFirstName.Text)
            Dim MiddleName As String = Generic.ToStr(txtMiddleName.Text)
            '10
            'Dim PresentAddress As String = Generic.ToStr(txtPresentAddress.Text)            
            Dim PresentAddress As String = Generic.ToStr(txtHouseNo.Text) & "|" & Generic.ToStr(txtStreet.Text) & "|" & Generic.ToStr(txtSubd.Text) & "|" & Generic.ToStr(txtBarangay.Text)
            Dim HomeAddress As String = Generic.ToStr(txtHomeHouseNo.Text) & "|" & Generic.ToStr(txtHomeStreet.Text) & "|" & Generic.ToStr(txtHomeSubd.Text) & "|" & Generic.ToStr(txtHomeBarangay.Text)
            Dim PresentPhoneNo As String = "" 'Generic.ToStr(txtPresentPhoneNo.Text)
            Dim CityNo As Integer = Generic.ToInt(Me.hifCityNo.Value)
            Dim MobileNo As String = Generic.ToStr(txtMobileNo.Text)
            Dim MobileNoTemp As String = Generic.ToStr(txtMobileNoTemp.Text)
            Dim EmailAddress As String = Generic.ToStr(txtEmailAddress.Text)
            Dim BirthPlace As String = Generic.ToStr(txtBirthPlace.Text)
            Dim BirthDate As String = Generic.ToStr(txtBirthDate.Text)
            'Dim HomeAddress As String = Generic.ToStr(txtHomeAddress.Text)            
            Dim HomePhoneNo As String = Generic.ToStr(txtHomePhoneNo.Text)
            Dim CityHomeNo As Integer = Generic.ToInt(Me.hifCityHomeNo.Value)
            '20
            Dim GenderNo As Integer = Generic.ToInt(cboGenderNo.SelectedValue)
            Dim CivilStatNo As Integer = Generic.ToInt(cboCivilStatNo.SelectedValue)
            Dim CitizenNo As Integer = Generic.ToInt(cboCitizenNo.SelectedValue)
            Dim ReligionNo As Integer = Generic.ToInt(cboReligionNo.SelectedValue)
            Dim TshirtNo As Integer = Generic.ToInt(cboTShirtNo.SelectedValue)
            Dim BloodTypeNo As Integer = Generic.ToInt(cboBloodTypeNo.SelectedValue)
            Dim ShoeNo As Integer = Generic.ToInt(cboShoeNo.SelectedValue)
            Dim Weight As String = Generic.ToStr(txtWeight.Text)
            Dim Height As String = Generic.ToStr(txtHeight.Text)
            Dim URL As String = "" 'Generic.ToStr(txtURL.Text)
            '30
            Dim IsFPSupervisor As Boolean = False
            Dim ContactRelationshipNo As Integer = Generic.ToInt(cboContactRelationshipNo.SelectedValue)
            Dim ContactName As String = Generic.ToStr(txtContactName.Text)
            Dim ContactAddress As String = Generic.ToStr(txtContactAddress.Text)
            Dim ContactNo As String = Generic.ToStr(txtContactNo.Text)
            Dim MarriageDate As String = Generic.ToStr(txtMarriageDate.Text)
            Dim MarriagePlace As String = Generic.ToStr(txtMarriagePlace.Text)
            Dim SpouseName As String = Generic.ToStr(txtSpouseFirstName.Text) & "|" & Generic.ToStr(txtSpouseMiddleName.Text) & "|" & Generic.ToStr(txtSpouseLastName.Text)
            Dim SpouseEmployeeExtNo As Integer = Generic.ToInt(cboSpouseEmployeeExtNo.SelectedValue)
            Dim SpouseMaidenName As String = txtSpouseMaidenName.Text

            Dim SpouseBirthDate As String = ""
            Dim SpouseAddress As String = ""
            '40
            Dim SpouseOccupation As String = txtSpouseOccupation.Text
            Dim SpouseEmployerName As String = txtSpouseEmployerName.Text
            Dim SpouseEmployerAddress As String = txtSpouseEmployerAddress.Text
            Dim SpouseEmployerContactNo As String = txtSpouseEmployerContactNo.Text
            Dim SpouseBusinessAddress As String = ""
            Dim SpouseBusinessPhoneNo As String = ""
            Dim EmployeeExtNo As Integer = Generic.ToInt(cboEmployeeExtNo.SelectedValue)
            Dim PayClassNo As Integer = Generic.ToInt(cboPayClassNo.SelectedValue)
            Dim MaidenName As String = Generic.ToStr(txtMaidenName.Text)
            Dim CompanyEmail As String = Generic.ToStr(txtCompanyEmail.Text)
            Dim CompanyMobileNo As String = Generic.ToStr(txtCompanyMobileNo.Text)


            '//validate start here
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EEmployee_WebValidateDetails", UserNo, TransNo, EmployeeCode, BarCode, FPID, CourtesyNo, NickName, LastName, FirstName, MiddleName, MaidenName, EmployeeExtNo, BirthDate, PayClassNo, GenderNo, CivilStatNo, MarriageDate, MarriagePlace, EmailAddress, CompanyEmail, PayLocNo)

            For Each rowx As DataRow In dtx.Rows
                invalid = Generic.ToBol(rowx("Invalid"))
                messagedialog = Generic.ToStr(rowx("MessageDialog"))
                alerttype = Generic.ToStr(rowx("AlertType"))
            Next

            If invalid = True Then
                MessageBox.Alert(messagedialog, alerttype, Me)
                Exit Sub
            End If


            Dim dt As DataTable, tstatus As Integer
            Dim error_num As Integer, error_message As String = ""
            dt = SQLHelper.ExecuteDataTable("EEmployee_WebSaveDetails", UserNo, TransNo, EmployeeCode, BarCode, FPID, CourtesyNo, NickName, LastName, FirstName, MiddleName, _
                                            PresentAddress, PresentPhoneNo, CityNo, MobileNo, MobileNoTemp, EmailAddress, BirthPlace, BirthDate, HomeAddress, HomePhoneNo, CityHomeNo, _
                                            GenderNo, CivilStatNo, CitizenNo, ReligionNo, TshirtNo, BloodTypeNo, ShoeNo, Weight, Height, URL, _
                                            IsFPSupervisor, ContactRelationshipNo, ContactName, ContactAddress, ContactNo, MarriageDate, MarriagePlace, SpouseName, SpouseBirthDate, SpouseAddress, _
                                            SpouseOccupation, SpouseEmployerName, SpouseEmployerAddress, SpouseBusinessAddress, SpouseBusinessPhoneNo, EmployeeExtNo, PayClassNo, MaidenName, CompanyEmail, CompanyMobileNo, PayLocNo, _
                                            Generic.ToInt(chkIsDualCitizenship.Checked), Generic.ToInt(cboDualCitizenshipTypeNo.SelectedValue), txtDualCitizenshipCountry.Text, SpouseEmployeeExtNo, SpouseEmployerContactNo, SpouseMaidenName)

            For Each row As DataRow In dt.Rows
                tstatus = Generic.ToInt(row("tStatus"))
                TransNo = Generic.ToInt(row("employeeno"))
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
                Dim RandomPassword As String = EGetRandomPassword(TransNo)
                SQLHelper.ExecuteNonQuery("EEmployee_WebAutoPWD", UserNo, TransNo, PeopleCoreCrypt.Encrypt(RandomPassword))

                If Generic.ToInt(Request.QueryString("id")) = 0 Then
                    Dim xURL As String = "EmpEditPerson.aspx?id=" & TransNo
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
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If

    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
    End Sub

    Protected Function EGetRandomPassword(EmployeeNo As Integer) As String
        Dim password As String = ""

        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("ERandomPassword_Web", EmployeeNo)
        For Each row As DataRow In dt.Rows
            password = Generic.ToStr(row("tPassword"))
        Next

        EGetRandomPassword = password
    End Function

    Private Sub PopulateCityPresent(ByVal CityNo As Integer)
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("ECity_WebPopulate", CityNo)
        For Each row As DataRow In dt.Rows
            txtProvinceDesc.Text = Generic.ToStr(row("ProvinceDesc"))
            'txtRegionDesc.Text = Generic.ToStr(row("RegionDesc"))
            txtPostalCode.Text = Generic.ToStr(row("PostalCode"))
        Next
    End Sub

    Private Sub PopulateCityHome(ByVal CityNo As Integer)
        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("ECity_WebPopulate", CityNo)
        For Each row As DataRow In dt.Rows
            txtProvinceDesc2.Text = Generic.ToStr(row("ProvinceDesc"))
            txtRegionDesc2.Text = Generic.ToStr(row("RegionDesc"))
            txtPostalCode2.Text = Generic.ToStr(row("PostalCode"))
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

    Protected Sub lnkUpload_Click(sender As Object, e As EventArgs)
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnkSave2_Click(sender As Object, e As EventArgs)

        If fuPhoto.HasFile Then

            Dim filePath As String = fuPhoto.PostedFile.FileName
            Dim filename As String = Path.GetFileName(filePath)
            Dim fileext As String = Path.GetExtension(filename)
            Dim contenttype As String = String.Empty

            Select Case fileext.ToLower()
                Case ".jpg"
                    contenttype = "image/jpg"
                Case ".jpeg"
                    contenttype = "image/jpeg"
                Case ".png"
                    contenttype = "image/png"
                Case ".gif"
                    contenttype = "image/gif"
            End Select

            If (contenttype <> String.Empty) Then
                Dim fs As Stream = fuPhoto.PostedFile.InputStream
                Dim br As New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                If SQLHelper.ExecuteNonQuery("EEmployee_WebPhotoUpdate", TransNo, bytes) > 0 Then
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                Else
                    MessageBox.Critical(MessageTemplate.ErrorSave, Me)
                End If
            Else
                MessageBox.Warning("Invalid file type.", Me)
            End If

        End If
    End Sub

    Protected Sub txtBirthDate_TextChanged(sender As Object, e As System.EventArgs)

        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EGetAge_Web", txtBirthDate.Text.ToString)
        For Each row As DataRow In dt.Rows
            txtBirthAge.Text = Generic.ToInt(row("Age"))
        Next

    End Sub

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
        txtPostalCode.Text = txtPostalCode2.Text
    End Sub

    Protected Sub chkIsDualCitizenship_CheckedChanged()
        If Generic.ToBol(ViewState("IsEnabled")) Then
            cboDualCitizenshipTypeNo.Enabled = chkIsDualCitizenship.Checked
            txtDualCitizenshipCountry.Enabled = chkIsDualCitizenship.Checked
            If chkIsDualCitizenship.Checked = False Then
                cboDualCitizenshipTypeNo.SelectedValue = ""
                txtDualCitizenshipCountry.Text = ""
            End If
        Else
            cboDualCitizenshipTypeNo.Enabled = False
            txtDualCitizenshipCountry.Enabled = False
        End If
        

    End Sub


End Class

