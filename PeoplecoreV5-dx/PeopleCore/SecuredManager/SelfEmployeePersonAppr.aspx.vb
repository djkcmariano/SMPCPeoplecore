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
        Next
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticatedSuperior()
        If TransNo = 0 Then : ViewState("IsEnabled") = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If
        If Not IsPostBack Then
            Generic.PopulateDropDownList_Self(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateData()
            PopulateTabHeader()
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
        txtBirthAge.Enabled = False
        btnModify.Visible = Not IsEnabled
        btnSave.Visible = IsEnabled
        lnkUpload.Enabled = IsEnabled

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
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
            Dim PresentAddress As String = Generic.ToStr(txtPresentAddress.Text)
            Dim PresentPhoneNo As String = Generic.ToStr(txtPresentPhoneNo.Text)
            Dim CityNo As Integer = Generic.ToInt(Me.hifCityNo.Value)
            Dim MobileNo As String = Generic.ToStr(txtMobileNo.Text)
            Dim EmailAddress As String = Generic.ToStr(txtEmailAddress.Text)
            Dim BirthPlace As String = Generic.ToStr(txtBirthPlace.Text)
            Dim BirthDate As String = Generic.ToStr(txtBirthDate.Text)
            Dim HomeAddress As String = Generic.ToStr(txtHomeAddress.Text)
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
            Dim URL As String = Generic.ToStr(txtURL.Text)
            '30
            Dim IsFPSupervisor As Boolean = False
            Dim ContactRelationshipNo As Integer = Generic.ToInt(cboContactRelationshipNo.SelectedValue)
            Dim ContactName As String = Generic.ToStr(txtContactName.Text)
            Dim ContactAddress As String = Generic.ToStr(txtContactAddress.Text)
            Dim ContactNo As String = Generic.ToStr(txtContactNo.Text)
            Dim MarriageDate As String = Generic.ToStr(txtMarriageDate.Text)
            Dim MarriagePlace As String = Generic.ToStr(txtMarriagePlace.Text)
            Dim SpouseName As String = ""
            Dim SpouseBirthDate As String = ""
            Dim SpouseAddress As String = ""
            '40
            Dim SpouseOccupation As String = ""
            Dim SpouseEmployerName As String = ""
            Dim SpouseEmployerAddress As String = ""
            Dim SpouseBusinessAddress As String = ""
            Dim SpouseBusinessPhoneNo As String = ""
            Dim EmployeeExtNo As Integer = Generic.ToInt(cboEmployeeExtNo.SelectedValue)
            Dim PayClassNo As Integer = Generic.ToInt(cboPayClassNo.SelectedValue)


            '//validate start here
            Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
            Dim dtx As New DataTable
            dtx = SQLHelper.ExecuteDataTable("EEmployee_WebValidateDetails", UserNo, TransNo, EmployeeCode, BarCode, FPID, CourtesyNo, NickName, LastName, FirstName, MiddleName, EmployeeExtNo, BirthDate, PayClassNo, PayLocNo)

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
            dt = SQLHelper.ExecuteDataTable("EEmployee_WebSaveDetails", UserNo, TransNo, EmployeeCode, BarCode, FPID, CourtesyNo, NickName, LastName, FirstName, MiddleName, _
                                            PresentAddress, PresentPhoneNo, CityNo, MobileNo, EmailAddress, BirthPlace, BirthDate, HomeAddress, HomePhoneNo, CityHomeNo, _
                                            GenderNo, CivilStatNo, CitizenNo, ReligionNo, TshirtNo, BloodTypeNo, ShoeNo, Weight, Height, URL, _
                                            IsFPSupervisor, ContactRelationshipNo, ContactName, ContactAddress, ContactNo, MarriageDate, MarriagePlace, SpouseName, SpouseBirthDate, SpouseAddress, _
                                            SpouseOccupation, SpouseEmployerName, SpouseEmployerAddress, SpouseBusinessAddress, SpouseBusinessPhoneNo, EmployeeExtNo, PayClassNo, PayLocNo)

            For Each row As DataRow In dt.Rows
                tstatus = Generic.ToInt(row("tStatus"))
                TransNo = Generic.ToInt(row("employeeno"))
                RetVal = True
            Next

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
            txtRegionDesc.Text = Generic.ToStr(row("RegionDesc"))
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
        Dim sqlhelp As New clsBase.SQLHelper
        Dim clsbase As New clsBase.clsBaseLibrary
        Dim UserNo As Integer = 0, PayLocNo As Integer
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        PayLocNo = (HttpContext.Current.Session("xPayLocNo"))

        _ds = SQLHelper.ExecuteDataSet("ECity_WebLookup_AC", UserNo, prefixText, PayLocNo)
        For Each row As DataRow In _ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(Generic.ToStr(row("tDesc")),
                                Generic.ToStr(row("tNo")) & _
                                "|" & Generic.ToStr(row("ProvinceDesc")) & _
                                "|" & Generic.ToStr(row("RegionDesc")) & _
                                "|" & Generic.ToStr(row("PostalCode")))
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

End Class

