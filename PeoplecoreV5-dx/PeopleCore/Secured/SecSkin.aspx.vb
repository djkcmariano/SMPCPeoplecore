Imports clsLib
Imports System.IO
Imports System.Xml
Imports Microsoft.VisualBasic.FileIO

Partial Class Secured_SecSkin
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim IsEnabled As Boolean = False


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            'PopulateDropDown()
            PopulateData()
        End If

        'EnabledControls()
    End Sub

    'Private Sub PopulateDropDown()
    '    Dim Files As String() = Directory.GetFiles(Server.MapPath("~\css"), "*.css")
    '    For Each file As String In Files
    '        Dim FileInfo As FileInfo = New FileInfo(file)
    '        Dim Value As String = Path.GetFileName(FileInfo.ToString)
    '        Dim Text As String = Replace(Path.GetFileNameWithoutExtension(FileInfo.ToString), "-", " ")
    '        cboSkin.Items.Add(New ListItem(Text, Value))
    '    Next
    'End Sub

    Private Sub PopulateData()
        txtBannerColor.Text = GetValue(0)
        Label0.Text = ColorPickerExtender0.SelectedColor

        txtMenuColor.Text = GetValue(1)
        Label1.Text = ColorPickerExtender1.SelectedColor

        txtMenuItemFontColor.Text = GetValue(2)
        Label2.Text = ColorPickerExtender2.SelectedColor

        txtMenuItemSepColor.Text = GetValue(3)
        Label3.Text = ColorPickerExtender3.SelectedColor

        txtMenuItemIconColor.Text = GetValue(4)
        Label4.Text = ColorPickerExtender4.SelectedColor

        txtMenuHoverColor.Text = GetValue(5)
        Label5.Text = ColorPickerExtender5.SelectedColor

        txtMenuItemFontHoverColor.Text = GetValue(6)
        Label6.Text = ColorPickerExtender6.SelectedColor

        txtMenuItemIconHoverColor.Text = GetValue(7)
        Label7.Text = ColorPickerExtender7.SelectedColor

        txtMenuActiveColor.Text = GetValue(8)
        Label8.Text = ColorPickerExtender8.SelectedColor

        txtMenuItemFontActiveColor.Text = GetValue(9)
        Label9.Text = ColorPickerExtender9.SelectedColor

        txtMenuItemIconActiveColor.Text = GetValue(10)
        Label10.Text = ColorPickerExtender10.SelectedColor

        txtMenuItemCaretColor.Text = GetValue(11)
        Label11.Text = ColorPickerExtender11.SelectedColor

        txtComponentFontColor.Text = GetValue(12)
        Label12.Text = ColorPickerExtender12.SelectedColor

        txtComponentSepColor.Text = GetValue(13)
        Label13.Text = ColorPickerExtender13.SelectedColor

        txtMenuSubColor.Text = GetValue(14)
        Label14.Text = ColorPickerExtender14.SelectedColor

        txtMenuSubSepColor.Text = GetValue(15)
        Label15.Text = ColorPickerExtender15.SelectedColor

        txtMenuSubHoverColor.Text = GetValue(16)
        Label16.Text = ColorPickerExtender16.SelectedColor

        txtMenuSubHoverFontColor.Text = GetValue(17)
        Label17.Text = ColorPickerExtender17.SelectedColor

        txtBannerIconColor.Text = GetValue(18)
        Label18.Text = ColorPickerExtender18.SelectedColor

        txtBannerIconHoverColor.Text = GetValue(19)
        Label19.Text = ColorPickerExtender19.SelectedColor

        txtBannerIconHoverBGColor.Text = GetValue(20)
        Label20.Text = ColorPickerExtender20.SelectedColor

        txtBannerIconActiveBGColor.Text = GetValue(21)
        Label21.Text = ColorPickerExtender21.SelectedColor

        txtBannerIconActiveColor.Text = GetValue(22)
        Label22.Text = ColorPickerExtender22.SelectedColor

        txtPeoplecoreBG.Text = GetValue(23)
        Label23.Text = ColorPickerExtender23.SelectedColor

        cboLogo.Text = IIf(GetValue(24) = "", 1, GetValue(24))
        
    End Sub


    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then           
            UpdateLine(0, txtBannerColor.Text)
            UpdateLine(1, txtMenuColor.Text)
            UpdateLine(2, txtMenuItemFontColor.Text)
            UpdateLine(3, txtMenuItemSepColor.Text)
            UpdateLine(4, txtMenuItemIconColor.Text)
            UpdateLine(5, txtMenuHoverColor.Text)
            UpdateLine(6, txtMenuItemFontHoverColor.Text)
            UpdateLine(7, txtMenuItemIconHoverColor.Text)
            UpdateLine(8, txtMenuActiveColor.Text)
            UpdateLine(9, txtMenuItemFontActiveColor.Text)
            UpdateLine(10, txtMenuItemIconActiveColor.Text)
            UpdateLine(11, txtMenuItemCaretColor.Text)
            UpdateLine(12, txtComponentFontColor.Text)
            UpdateLine(13, txtComponentSepColor.Text)
            UpdateLine(14, txtMenuSubColor.Text)
            UpdateLine(15, txtMenuSubSepColor.Text)
            UpdateLine(16, txtMenuSubHoverColor.Text)
            UpdateLine(17, txtMenuSubHoverFontColor.Text)
            UpdateLine(18, txtBannerIconColor.Text)
            UpdateLine(19, txtBannerIconHoverColor.Text)
            UpdateLine(20, txtBannerIconHoverBGColor.Text)
            UpdateLine(21, txtBannerIconActiveBGColor.Text)
            UpdateLine(22, txtBannerIconActiveColor.Text)
            UpdateLine(23, txtPeoplecoreBG.Text)
            UpdateLine(24, cboLogo.SelectedValue)
            Dim url As String = "secskin.aspx"
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

    End Sub

    Protected Sub lnkModify_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
        '    ViewState("IsEnabled") = True
        '    EnabledControls()
        'Else
        '    MessageBox.Information(MessageTemplate.DeniedEdit, Me)
        'End If
    End Sub

    Private Sub EnabledControls()
        IsEnabled = Generic.ToBol(ViewState("IsEnabled"))
        Generic.EnableControls(Me, "pnlPopupMain", IsEnabled)
        lnkModify.Visible = Not IsEnabled
        lnkSave.Visible = IsEnabled
    End Sub


    Private Function GetValue(index As Integer) As String
        Try
            Using parser As New TextFieldParser(Server.MapPath("~/secured/connectionstr/skin.ini"))                
                parser.TextFieldType = FieldType.Delimited
                parser.Delimiters = New String() {"|"}
                Dim fields As String()
                While Not parser.EndOfData
                    fields = parser.ReadFields()
                    If index = fields(0) Then
                        Return fields(1)
                    End If                    
                End While
            End Using
        Catch ex As Exception

        End Try

    End Function

    Private Sub UpdateLine(index As Integer, value As String)
        Dim path As String = Server.MapPath("~/secured/connectionstr/skin.ini")
        Dim lines() As String = System.IO.File.ReadAllLines(path)

        Try
            lines(index) = index & "|" & value
            File.WriteAllLines(Path, lines)
        Catch ex As Exception           
            Using sw As StreamWriter = File.AppendText(path)
                sw.WriteLine(index & "|" & value)
            End Using
        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

    End Sub

End Class
