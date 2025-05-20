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
            ' MessageBox.Success(MessageTemplate.SuccessSave, Me)
            Dim url As String = "Orgpayloclist.aspx?id=" & Generic.ToInt(TransNo)
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            ViewState("IsEnabled") = False
            EnabledControls()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If
    End Sub

    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayLoc_WebOne", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next

    End Sub

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
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
        txtCode.Enabled = False        
        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        AccessRights.CheckUser(UserNo)        
        If TransNo = 0 Then : ViewState("IsEnabled") = True : Else : IsEnabled = Generic.ToBol(ViewState("IsEnabled")) : End If
        EnabledControls()

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateData()
        End If

    End Sub

    Private Function SaveRecord() As Boolean
        Dim obj As Object
        Dim RetVal As Integer
        Dim payloccode As String = Generic.ToStr(txtPayLocCode.Text)
        Dim paylocdesc As String = Generic.ToStr(txtPayLocDesc.Text)
        Dim address As String = Generic.ToStr(txtAddress.Text)
        Dim cityno As Integer = Generic.ToInt(cboCityNo.SelectedValue)
        Dim emailaddress As String = Generic.ToStr(txtEmailAddress.Text)
        Dim url As String = Generic.ToStr(txtURL.Text)
        Dim faxno As String = Generic.ToStr(txtFaxNo.Text)
        Dim phoneno As String = Generic.ToStr(txtPhoneNo.Text)
        Dim extension1 As String = Generic.ToStr(txtExtension1.Text)
        Dim extension2 As String = Generic.ToStr(txtExtension2.Text)
        Dim sssno As String = Generic.ToStr(txtSSSNo.Text)
        Dim phno As String = Generic.ToStr(txtPHNo.Text)
        Dim hdmfno As String = Generic.ToStr(txtHDMFNo.Text)
        Dim tinno As String = Generic.ToStr(txtTINNo.Text)
        Dim isgov As Integer = Generic.ToInt(chkIsGov.Checked)
        Dim companycode As String = "" 'Generic.ToStr(txtPayLocCode.Text)
        Dim sssbranchcode As String = Generic.ToStr(txtSSSBranchCode.Text)
        Dim hdmfbranchcode As String = Generic.ToStr(txtHDMFBranchCode.Text)
        Dim phbranchcode As String = Generic.ToStr(txtPHBranchCode.Text)
        Dim accountno As String = "" 'Generic.ToStr(txtAccountNo.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim branchaccount As String = "" 'Generic.ToStr(txtBranchAccount.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim bankcode As String = "" 'Generic.ToStr(txtBankCode.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim MaxAmtAccumulatedExemp As Double = Generic.ToDec(txtMaxAmtAccumulatedExemp.Text)
        Dim istaxMonthly As Integer = Generic.ToInt(txtIsTaxMonthly.Checked)
        Dim isprorateTax As Integer = Generic.ToInt(txtIsProrateTax.Checked)
        Dim TinBranchCode As String = Generic.ToStr(txtTinBranchCode.Text)
        Dim RdoCode As String = Generic.ToStr(txtRdoCode.Text)

        obj = SQLHelper.ExecuteScalar("EPayLoc_WebSave", UserNo, TransNo, payloccode, paylocdesc, address, cityno, emailaddress, url, faxno, phoneno, extension1, extension2, sssno, phno, hdmfno, tinno, isgov, companycode, sssbranchcode, hdmfbranchcode, phbranchcode, accountno, branchaccount, bankcode, MaxAmtAccumulatedExemp, istaxMonthly, isprorateTax, TinBranchCode, RdoCode)
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
                    'MessageBox.Success(MessageTemplate.SuccessSave, Me)
                Else
                    MessageBox.Critical(MessageTemplate.ErrorSave, Me)
                End If
                'Dim Path As String = Server.MapPath("~/images/company logo")
                'If Not IO.Directory.Exists(Path) Then
                '    IO.Directory.CreateDirectory(Path)
                'End If
                'Dim tfilename As String = ""
                'If RetVal > 0 Then
                '    tfilename = Path & "\" & RetVal.ToString & ".png"
                '    WriteFile(tfilename.ToString, bytes)
                'End If
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
    Private Sub WriteFile(strPath As String, Buffer As Byte())
        'Create a file
        Dim newFile As FileStream = New FileStream(strPath, FileMode.Create)

        'Write data to the file
        newFile.Write(Buffer, 0, Buffer.Length)

        'Close file
        newFile.Close()
    End Sub
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
End Class



