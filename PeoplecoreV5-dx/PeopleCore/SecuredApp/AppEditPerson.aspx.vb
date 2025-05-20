Imports clsLib
Imports System.Data
Imports System.IO
Imports System.Web

Partial Class Secured_AppEditPerson
    Inherits System.Web.UI.Page

    Dim TransNo As Int64
    Dim IsEnabled As Boolean = False
    Dim UserNo As Int64

    Private Sub PopulateData()       
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicant_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows            
            Generic.PopulateData(Me, "Panel1", dt)
            PopulateCityPresent(Generic.ToInt(Me.hifCityNo.Value))
            PopulateCityHome(Generic.ToInt(Me.hifCityHomeNo.Value))
        Next

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        'AccessRights.CheckUser(UserNo)        
        If TransNo = 0 Then : ViewState("IsEnabled") = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateData()
            PopulateTabHeader()
        End If

        EnabledControls()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim RetVal As Boolean = False

        Dim ApplicantCode As String = Generic.ToStr(txtApplicantCode.Text)
        Dim CourtesyNo As Integer = Generic.ToInt(cboCourtesyNo.SelectedValue)
        Dim LastName As String = Generic.ToStr(txtLastName.Text)
        Dim FirstName As String = Generic.ToStr(txtFirstName.Text)
        Dim MiddleName As String = Generic.ToStr(txtMiddleName.Text)
        Dim MaidenName As String = Generic.ToStr(txtMaidenName.Text)
        Dim NickName As String = Generic.ToStr(txtNickName.Text)
        Dim EmployeeExtNo As Integer = Generic.ToInt(cboEmployeeExtNo.SelectedValue)
        '10
        Dim PresentAddress As String = Generic.ToStr(txtPresentAddress.Text)
        Dim PresentPhoneNo As String = Generic.ToStr(txtPresentPhoneNo.Text)
        Dim CityNo As Integer = Generic.ToInt(Me.hifCityNo.Value)
        Dim MobileNo As String = Generic.ToStr(txtMobileNo.Text)
        Dim EmailAddress As String = Generic.ToStr(txtEmail.Text)
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
        Dim ContactRelationshipNo As Integer = Generic.ToInt(cboContactRelationshipNo.SelectedValue)
        Dim ContactName As String = Generic.ToStr(txtContactName.Text)
        Dim ContactAddress As String = Generic.ToStr(txtContactAddress.Text)
        Dim ContactNo As String = Generic.ToStr(txtContactNo.Text)
        Dim MarriageDate As String = Generic.ToStr(txtMarriageDate.Text)
        Dim MarriagePlace As String = Generic.ToStr(txtMarriagePlace.Text)
        Dim xPayLocNo As Integer = Generic.ToInt(Session("xPayLocNo"))

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EApplicant_WebValidate", UserNo, TransNo, ApplicantCode, CourtesyNo, LastName, FirstName, MiddleName, MaidenName, NickName, EmployeeExtNo, BirthDate, xPayLocNo)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicant_WebSaveSelf", UserNo, TransNo, ApplicantCode, CourtesyNo, LastName, FirstName, MiddleName, MaidenName, NickName, EmployeeExtNo, _
                                        PresentAddress, PresentPhoneNo, CityNo, MobileNo, EmailAddress, BirthPlace, BirthDate, HomeAddress, HomePhoneNo, CityHomeNo, _
                                        GenderNo, CivilStatNo, CitizenNo, ReligionNo, TshirtNo, BloodTypeNo, ShoeNo, Weight, Height, URL, _
                                        ContactRelationshipNo, ContactName, ContactAddress, ContactNo, MarriageDate, MarriagePlace, xPayLocNo)

        For Each row As DataRow In dt.Rows
            TransNo = Generic.ToInt(row("ApplicantNo"))
            RetVal = True
        Next

        If RetVal = True Then
            If Generic.ToInt(Request.QueryString("id")) = 0 Then
                Dim xURL As String = "AppEditPerson.aspx?id=" & TransNo
                MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, xURL)
            Else
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                ViewState("IsEnabled") = False
                EnabledControls()
            End If
            PopulateData()
            'PopulateTabHeader()
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
        txtApplicantCode.Enabled = False
        txtBirthAge.Enabled = False
        lnkUpload.Enabled = IsEnabled
        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
    End Sub


    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicantTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=1"
    End Sub

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
                If SQLHelper.ExecuteNonQuery("EApplicant_WebPhotoUpdate", TransNo, bytes) > 0 Then
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                Else
                    MessageBox.Critical(MessageTemplate.ErrorSave, Me)
                End If
            Else
                MessageBox.Warning("Invalid file type.", Me)
            End If

        End If
    End Sub

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

End Class
