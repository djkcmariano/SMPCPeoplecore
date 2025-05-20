Imports clsLib
Imports System.Data

Partial Class SecuredSelf_SelfEmployeeDetail_Old
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim EmployeeNo As Integer
    Dim PayLocNo As Integer
    Dim IsEnabled As Boolean = False

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        EmployeeNo = Generic.ToInt(Session("EmployeeNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TAB.TransactionNo = EmployeeNo
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            EnabledControls()
            Generic.PopulateDropDownList_Self(UserNo, Me, "Panel1", PayLocNo)
            PopulateData()
            PopulateTabHeader()
        End If
    End Sub

    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        ViewState("IsEnabled") = True
        EnabledControls()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        Dim EmployeeCode As String = Generic.ToStr(txtEmployeeCode.Text)
        Dim BarCode As String = Generic.ToStr(txtBarCode.Text)
        Dim FPID As String = Generic.ToStr(txtFPId.Text)
        Dim CourtesyNo As Integer = Generic.ToInt(cboCourtesyNo.SelectedValue)
        Dim NickName As String = Generic.ToStr(txtNickName.Text)
        Dim LastName As String = Generic.ToStr(txtLastName.Text)
        Dim FirstName As String = Generic.ToStr(txtFirstName.Text)
        Dim MiddleName As String = Generic.ToStr(txtMiddleName.Text)
        '10
        Dim PresentAddress As String = Generic.ToStr(txtHouseNo.Text) & "|" & Generic.ToStr(txtStreet.Text) & "|" & Generic.ToStr(txtBarangay.Text)
        Dim PresentPhoneNo As String = Generic.ToStr(txtPresentPhoneno.Text)
        Dim CityNo As Integer = Generic.ToInt(Me.hifCityNo.Value)
        Dim MobileNo As String = Generic.ToStr(txtMobileNo.Text)
        Dim EmailAddress As String = Generic.ToStr(txtEmailAddress.Text)
        Dim BirthPlace As String = Generic.ToStr(txtBirthPlace.Text)
        Dim BirthDate As String = Generic.ToStr(txtBirthDate.Text)
        Dim HomeAddress As String = Generic.ToStr(txtHomeHouseNo.Text) & "|" & Generic.ToStr(txtHomeStreet.Text) & "|" & Generic.ToStr(txtHomeBarangay.Text)
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
        '40
        Dim EmployeeExtNo As Integer = Generic.ToInt(cboEmployeeExtNo.SelectedValue)
        Dim MaidenName As String = Generic.ToStr(txtMaidenName.Text)


        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EEmployeeUpd_WebSaveValidate", UserNo, EmployeeNo, CourtesyNo, LastName, FirstName, MiddleName, MaidenName, EmployeeExtNo, NickName, _
                                          BirthDate, BirthPlace, CitizenNo, CivilStatNo, GenderNo, ReligionNo, BloodTypeNo, Height, Weight, ShoeNo, TshirtNo, MarriageDate, MarriagePlace, _
                                          MobileNo, EmailAddress, URL, PresentAddress, PresentPhoneNo, CityNo, HomeAddress, HomePhoneNo, CityHomeNo, _
                                          ContactName, ContactRelationshipNo, ContactNo, ContactAddress, PayLocNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If


        Dim dt As DataTable, msg As String = "", ret As Integer = 0
        dt = SQLHelper.ExecuteDataTable("EEmployeeUpd_WebSaveSelf", UserNo, EmployeeNo, CourtesyNo, LastName, FirstName, MiddleName, MaidenName, EmployeeExtNo, NickName, _
                                          BirthDate, BirthPlace, CitizenNo, CivilStatNo, GenderNo, ReligionNo, BloodTypeNo, Height, Weight, ShoeNo, TshirtNo, MarriageDate, MarriagePlace, _
                                          MobileNo, EmailAddress, URL, PresentAddress, PresentPhoneNo, CityNo, HomeAddress, HomePhoneNo, CityHomeNo, _
                                          ContactName, ContactRelationshipNo, ContactNo, ContactAddress, PayLocNo)

        For Each row As DataRow In dt.Rows
            msg = Generic.ToStr(row("xMessage"))
            ret = Generic.ToInt(row("RetVal"))
        Next

        If ret = 1 Then
            MessageBox.Success(msg, Me)
            ViewState("IsEnabled") = False
            EnabledControls()
        Else
            MessageBox.Alert(msg, "warning", Me)
            Exit Sub
        End If

    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)
        Generic.PopulateDataDisabled(Me, "Panel1", UserNo, PayLocNo, Generic.ToStr(Session("xMenuType")))
        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
        chkIsFPSupervisor.Enabled = False
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployee_WebOne", UserNo, EmployeeNo)
        Generic.PopulateData(Me, "Panel1", dt)
        PopulateAddress(Generic.ToStr(txtPresentAddress.Text), 1)
        PopulateAddress(Generic.ToStr(txtHomeAddress.Text), 2)
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, EmployeeNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(EmployeeNo) & "&tIndex=2"
    End Sub

    Protected Sub txtBirthDate_TextChanged(sender As Object, e As System.EventArgs)

        Dim dt As New DataTable
        dt = SQLHelper.ExecuteDataTable("EGetAge_Web", txtBirthDate.Text.ToString)
        For Each row As DataRow In dt.Rows
            txtBirthAge.Text = Generic.ToInt(row("Age"))
        Next

    End Sub

    Private Sub PopulateAddress(ByVal Address As String, index As Integer)
        Dim splitStr As String() = Address.Split("|")
        Try
            If index = 1 Then
                txtHouseNo.Text = splitStr(0).ToString()
                txtStreet.Text = splitStr(1).ToString()
                txtBarangay.Text = splitStr(2).ToString()
            ElseIf index = 2 Then
                txtHomeHouseNo.Text = splitStr(0).ToString()
                txtHomeStreet.Text = splitStr(1).ToString()
                txtHomeBarangay.Text = splitStr(2).ToString()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkCopy_Click(sender As Object, e As EventArgs)
        txtHomeHouseNo.Text = txtHouseNo.Text
        txtHomeStreet.Text = txtStreet.Text
        txtHomeBarangay.Text = txtBarangay.Text
        txtCityHomeDesc.Text = txtCityDesc.Text
        hifCityHomeNo.Value = hifCityNo.Value
        txtProvinceDesc2.Text = txtProvinceDesc.Text
        txtRegionDesc2.Text = txtRegionDesc.Text
        txtHomePhoneNo.Text = txtPresentPhoneno.Text
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

End Class
