Imports System.Data
Imports clsLib
Imports System.IO

Partial Class Secured_OrgPayLocEdit
    Inherits System.Web.UI.Page

    Dim TransNo As Int64
    Dim IsEnabled As Boolean = False
    Dim UserNo As Int64
    Dim PayLocNo As Integer

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If SaveRecord() Then
            PopulateData()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            ViewState("IsEnabled") = False
            EnabledControls()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EFacility_WebOne", UserNo, TransNo, PayLocNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
            ViewState("IsAllowEdit") = Generic.ToBol(row("IsEnabled"))
        Next

    End Sub

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "OrgFacilityList.aspx") Then
            If Generic.ToBol(ViewState("IsAllowEdit")) = True Then
                ViewState("IsEnabled") = True
            Else
                MessageBox.Information(MessageTemplate.DeniedEdit, Me)
            End If
            EnabledControls()
        Else
            MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "Panel1", IsEnabled)
        txtCode.Enabled = False        
        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo, "OrgFacilityList.aspx")
        If TransNo = 0 Then : ViewState("IsEnabled") = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If
        EnabledControls()

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            Try
                cboPayLocNo.DataSource = SQLHelper.ExecuteDataSet("EPayLoc_WebLookup_Reference", UserNo, PayLocNo)
                cboPayLocNo.DataTextField = "tdesc"
                cboPayLocNo.DataValueField = "tNo"
                cboPayLocNo.DataBind()

            Catch ex As Exception

            End Try

            PopulateData()
        End If

    End Sub

    Private Function SaveRecord() As Boolean
        Dim obj As Object
        Dim RetVal As Integer
        Dim FacilityNo As Integer = Generic.ToInt(txtFacilityNo.Text)
        Dim FacilityCode As String = Generic.ToStr(txtFacilityCode.Text)
        Dim FacilityDesc As String = Generic.ToStr(txtFacilityDesc.Text)
        Dim FacilityACode As String = Generic.ToStr(txtFacilityACode.Text)
        Dim EmployeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim address As String = Generic.ToStr(txtAddress.Text)
        Dim cityno As Integer = Generic.ToInt(hifCityNo.Value)
        Dim emailaddress As String = Generic.ToStr(txtEmailAddress.Text)
        Dim url As String = Generic.ToStr(txtURL.Text)
        Dim faxno As String = Generic.ToStr(txtFaxNo.Text)
        Dim phoneno As String = Generic.ToStr(txtPhoneNo.Text)
        Dim sssno As String = Generic.ToStr(txtSSSNo.Text)
        Dim phno As String = Generic.ToStr(txtPHNo.Text)
        Dim hdmfno As String = Generic.ToStr(txtHDMFNo.Text)
        Dim tinno As String = Generic.ToStr(txtTINNo.Text)
        Dim companycode As String = "" 'Generic.ToStr(txtCompanyCode.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim branchcode As String = "" 'Generic.ToStr(txtBranchCode.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim accountno As String = "" 'Generic.ToStr(txtAccountNo.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim branchaccount As String = "" 'Generic.ToStr(txtBranchAccount.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim bankcode As String = "" 'Generic.ToStr(txtBankCode.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim FacilityGroupNo As Integer = Generic.ToInt(cboFacilityGroupNo.SelectedValue)
        Dim IsArchived As Boolean = Generic.ToBol(chkIsArchived.Checked)

        obj = SQLHelper.ExecuteScalar("EFacility_WebSave", UserNo, FacilityNo, FacilityCode, FacilityDesc, FacilityACode, EmployeeNo, address, cityno, emailaddress, url, faxno, phoneno, sssno, phno, hdmfno, tinno, branchcode, branchaccount, bankcode, FacilityGroupNo, Generic.ToInt(cboPayLocNo.SelectedValue), PayLocNo, IsArchived)
        RetVal = Generic.ToInt(obj)

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
                If SQLHelper.ExecuteNonQuery("EPayLocPhoto_WebSave", TransNo, bytes) > 0 Then
                    MessageBox.Success(MessageTemplate.SuccessSave, Me)
                Else
                    MessageBox.Critical(MessageTemplate.ErrorSave, Me)
                End If
            Else
                MessageBox.Warning("Invalid file type.", Me)
            End If

        End If

        If RetVal > 0 Then
            Return True
        Else
            Return False
        End If
        
    End Function
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
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



