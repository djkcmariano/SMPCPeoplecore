Imports Microsoft.VisualBasic
Imports System.Data
Imports clsLib

Public Class clsFormControls
    
    Inherits System.Web.UI.Page

    Dim clsGen As New clsGenericClass

    Public Sub EnableControls(pPage As Page, IsLock As Boolean)
        'Dim o As Page
        'o = pPage
        For Each obj As Control In pPage.Controls
            If TypeOf obj Is MasterPage Then
                For Each objMaster As Control In obj.Controls
                    If TypeOf objMaster Is System.Web.UI.HtmlControls.HtmlForm Then
                        For Each objForm As Control In objMaster.Controls
                            If TypeOf objForm Is UpdatePanel Then
                                For Each objContent As Control In objForm.Controls(0).Controls

                                    If TypeOf objContent Is Panel Then
                                        For Each pnlContent As Control In objContent.Controls
                                            EnableControlsDetl(pnlContent, IsLock)
                                        Next
                                    ElseIf TypeOf objContent Is ContentPlaceHolder Then
                                        For Each pnlContent As Control In objContent.Controls
                                            EnableControlsDetl(pnlContent, IsLock)
                                        Next
                                    Else
                                        EnableControlsDetl(objContent, IsLock)

                                    End If
                                Next
                            End If
                        Next
                    End If

                Next
            End If


        Next
    End Sub
    Public Sub EnableControls_Err(pPage As Page, IsLock As Boolean)
        'Dim o As Page
        'o = pPage
        For Each obj As Control In pPage.Controls
            If TypeOf obj Is MasterPage Then
                For Each objMaster As Control In obj.Controls
                    If TypeOf objMaster Is System.Web.UI.HtmlControls.HtmlForm Then
                        For Each objForm As Control In objMaster.Controls
                            If TypeOf objForm Is ContentPlaceHolder Then
                                For Each objContent As Control In objForm.Controls

                                    If TypeOf objContent Is Panel Then
                                        For Each pnlContent As Control In objContent.Controls
                                            EnableControlsDetl(pnlContent, IsLock)
                                        Next
                                    ElseIf TypeOf objContent Is UpdatePanel Then
                                        For Each pnlContent As Control In objContent.Controls(0).Controls
                                            EnableControlsDetl(pnlContent, IsLock)
                                        Next
                                    Else
                                        EnableControlsDetl(objContent, IsLock)

                                    End If
                                Next
                            End If
                        Next
                    End If

                Next
            End If


        Next
    End Sub

    Public Sub EnableControlsDetl(objContent As Control, IsLock As Boolean)
        If TypeOf objContent Is TextBox Then
            Dim txt As New TextBox
            txt = CType(objContent, TextBox)
            txt.Enabled = IsLock
        ElseIf TypeOf objContent Is DropDownList Then
            Dim drp As New DropDownList
            drp = CType(objContent, DropDownList)
            drp.Enabled = IsLock
        ElseIf TypeOf objContent Is CheckBox Then
            Dim chk As New CheckBox
            chk = CType(objContent, CheckBox)
            chk.Enabled = IsLock
        ElseIf TypeOf objContent Is AjaxControlToolkit.ComboBox Then
            Dim drp As New AjaxControlToolkit.ComboBox
            drp = CType(objContent, AjaxControlToolkit.ComboBox)
            drp.Enabled = IsLock
        End If
    End Sub
    Public Sub EnableControls_in_Popup(Cntrl As Control, IsLock As Boolean)
        For Each pCntrl As Control In Cntrl.Controls
            If TypeOf pCntrl Is UpdatePanel Then
                For Each obj As Control In pCntrl.Controls(0).Controls
                    EnableControlsDetl(obj, IsLock)
                Next
            Else
                EnableControlsDetl(pCntrl, IsLock)
            End If
        Next
    End Sub
    Public Sub showFormControls(pPage As Page, ds As DataSet)
        For Each obj As Control In pPage.Controls

            If TypeOf obj Is MasterPage Then
                For Each objMaster As Control In obj.Controls
                    If TypeOf objMaster Is System.Web.UI.HtmlControls.HtmlForm Then
                        For Each objForm As Control In objMaster.Controls
                            If TypeOf objForm Is UpdatePanel Then
                                For Each objContent As Control In objForm.Controls(0).Controls
                                    If TypeOf objContent Is Panel Then
                                        For Each pnlContent As Control In objContent.Controls
                                            showFormControlsDetls(pnlContent, ds)
                                        Next
                                    ElseIf TypeOf objContent Is ContentPlaceHolder Then
                                        For Each pnlContent As Control In objContent.Controls
                                            showFormControlsDetls(pnlContent, ds)
                                        Next
                                    Else
                                        showFormControlsDetls(objContent, ds)
                                    End If

                                Next
                            End If
                        Next
                    End If

                Next
            End If


        Next

    End Sub
    Public Sub showFormControls_Err(pPage As Page, ds As DataSet)
        For Each obj As Control In pPage.Controls

            If TypeOf obj Is MasterPage Then
                For Each objMaster As Control In obj.Controls
                    If TypeOf objMaster Is System.Web.UI.HtmlControls.HtmlForm Then
                        For Each objForm As Control In objMaster.Controls
                            If TypeOf objForm Is ContentPlaceHolder Then
                                For Each objContent As Control In objForm.Controls
                                    If TypeOf objContent Is Panel Then
                                        For Each pnlContent As Control In objContent.Controls
                                            showFormControlsDetls(pnlContent, ds)
                                        Next
                                    ElseIf TypeOf objContent Is UpdatePanel Then
                                        For Each pnlContent As Control In objContent.Controls(0).Controls
                                            showFormControlsDetls(pnlContent, ds)
                                        Next
                                    Else
                                        showFormControlsDetls(objContent, ds)
                                    End If

                                Next
                            End If
                        Next
                    End If

                Next
            End If


        Next

    End Sub
    Public Sub showFormControls_In_Popup(pPage As Control, ds As DataSet)
        For Each pnlContent As Control In pPage.Controls
            If TypeOf pnlContent Is UpdatePanel Then
                For Each obj As Control In pnlContent.Controls(0).Controls
                    showFormControlsDetls(obj, ds)
                Next
            Else
                showFormControlsDetls(pnlContent, ds)
            End If
        Next
    End Sub
    Public Sub clearFormControls_In_Popup(pPage As Control)
        For Each pnlContent As Control In pPage.Controls
            If TypeOf pnlContent Is TextBox Then
                Dim txt As New TextBox
                txt = CType(pnlContent, TextBox)
                txt.Text = ""
            ElseIf TypeOf pnlContent Is DropDownList Then
                Dim drp As New DropDownList
                drp = CType(pnlContent, DropDownList)
                drp.Text = ""
            ElseIf TypeOf pnlContent Is CheckBox Then
                Dim chk As New CheckBox
                chk = CType(pnlContent, CheckBox)
                chk.Checked = False
            End If
        Next

    End Sub

    Private Sub showFormControls_LabelName(objContent As Control, ds As DataSet)

        If TypeOf objContent Is Label Then
            Dim lbl As New Label
            Dim lblName$ = ""
            Dim lblWidth As Integer = 0
            Dim lblid$ = ""

            lbl = CType(objContent, Label)
            lblName = lbl.Text.ToString
            lblid = lbl.ID
            lbl.Style.Add("width", "100%")

        End If

    End Sub
    Private Sub showFormControlsDetls(objContent As Control, ds As DataSet)

        Dim columName As String = ""
        Dim idLenght As Integer = 0

        If TypeOf objContent Is TextBox Then
            Dim txt As New TextBox
            txt = CType(objContent, TextBox)
            idLenght = txt.ID.Length - 3
            columName = Microsoft.VisualBasic.Mid(txt.ID, 4, idLenght)

            Try
                If Left(txt.ID, 3) = "txt" Then
                    txt.Text = Generic.CheckDBNull(ds.Tables(0).Rows(0)(columName), clsBase.clsBaseLibrary.enumObjectType.StrType)
                End If
            Catch ex As Exception
            End Try
        ElseIf TypeOf objContent Is HiddenField Then
            Dim hif As New HiddenField
            hif = CType(objContent, HiddenField)
            idLenght = hif.ID.Length - 3
            columName = Microsoft.VisualBasic.Mid(hif.ID, 4, idLenght)
            Try
                If Left(hif.ID, 3) = "hif" Then
                    hif.Value = Generic.CheckDBNull(ds.Tables(0).Rows(0)(columName), clsBase.clsBaseLibrary.enumObjectType.StrType)
                End If
            Catch ex As Exception
            End Try

        ElseIf TypeOf objContent Is DropDownList Then
            Dim drp As New DropDownList
            Dim tableName As String = ""
            drp = CType(objContent, DropDownList)
            idLenght = drp.ID.Length - 3
            columName = Microsoft.VisualBasic.Mid(drp.ID, 4, idLenght)
            Try
                If Left(drp.ID, 3) = "cbo" Then
                    Dim ftxt As String = ""
                    ftxt = Generic.CheckDBNull(ds.Tables(0).Rows(0)(columName), clsBase.clsBaseLibrary.enumObjectType.IntType)
                    If ftxt = "0" Then
                        ftxt = ""
                    ElseIf ftxt = 0 Then
                        ftxt = ""
                    End If
                    drp.Text = ftxt
                End If
            Catch ex As Exception
            End Try

        ElseIf TypeOf objContent Is AjaxControlToolkit.ComboBox Then
            Dim drp As New AjaxControlToolkit.ComboBox
            Dim tableName As String = ""
            drp = CType(objContent, AjaxControlToolkit.ComboBox)
            idLenght = drp.ID.Length - 3
            columName = Microsoft.VisualBasic.Mid(drp.ID, 4, idLenght)
            Try
                If Left(drp.ID, 3) = "cbo" Then
                    Dim ftxt As String = ""
                    ftxt = Generic.CheckDBNull(ds.Tables(0).Rows(0)(columName), clsBase.clsBaseLibrary.enumObjectType.IntType)
                    If ftxt = "0" Then
                        ftxt = ""
                    ElseIf ftxt = 0 Then
                        ftxt = ""
                    End If
                    drp.Text = ftxt
                End If
            Catch ex As Exception
            End Try

        ElseIf TypeOf objContent Is CheckBox Then

            Dim chk As New CheckBox
            chk = CType(objContent, CheckBox)

            Try

                If Microsoft.VisualBasic.Mid(chk.ID, 1, 3) = "rdo" Then
                    Dim rdo As RadioButton
                    Dim rdoTag As Integer
                    rdo = CType(objContent, RadioButton)
                    columName = rdo.GroupName
                    rdoTag = Right(rdo.ID, 2)
                    Dim fVal As Integer

                    fVal = Generic.CheckDBNull(ds.Tables(0).Rows(0)(columName), clsBase.clsBaseLibrary.enumObjectType.IntType)
                    If (rdoTag = fVal) Or (rdoTag = fVal * -1) Then
                        rdo.Checked = True
                    End If

                ElseIf Microsoft.VisualBasic.Mid(chk.ID, 1, 3) = "txt" Then
                    idLenght = chk.ID.Length - 3
                    columName = Microsoft.VisualBasic.Mid(chk.ID, 4, idLenght)
                    chk.Checked = Generic.CheckDBNull(ds.Tables(0).Rows(0)(columName), clsBase.clsBaseLibrary.enumObjectType.IntType)

                ElseIf Microsoft.VisualBasic.Mid(chk.ID, 1, 3) = "chk" Then
                    idLenght = chk.ID.Length - 3
                    columName = Microsoft.VisualBasic.Mid(chk.ID, 4, idLenght)
                    chk.Checked = Generic.CheckDBNull(ds.Tables(0).Rows(0)(columName), clsBase.clsBaseLibrary.enumObjectType.IntType)

                End If
            Catch ex As Exception

            End Try
        ElseIf TypeOf objContent Is RadioButton Then
            Dim rdo As New RadioButton
            rdo = CType(objContent, RadioButton)

        End If
    End Sub
    Public Sub populateCombo_One(onlineuserno As Integer, pPage As Page, tNo As Integer)

        For Each obj As Control In pPage.Controls

            If TypeOf obj Is MasterPage Then
                For Each objMaster As Control In obj.Controls
                    If TypeOf objMaster Is System.Web.UI.HtmlControls.HtmlForm Then
                        For Each objForm As Control In objMaster.Controls
                            If TypeOf objForm Is UpdatePanel Then
                                For Each objContent As Control In objForm.Controls(0).Controls
                                    If TypeOf objContent Is Panel Then
                                        For Each pnlContent As Control In objContent.Controls
                                            populateComboDetl_One(onlineuserno, pnlContent, tNo)
                                        Next
                                    ElseIf TypeOf objContent Is ContentPlaceHolder Then
                                        For Each pnlContent As Control In objContent.Controls
                                            populateComboDetl_One(onlineuserno, pnlContent, tNo)
                                        Next
                                    Else
                                        populateComboDetl_One(onlineuserno, objContent, tNo)
                                    End If
                                Next
                            End If
                        Next
                    End If

                Next
            End If
        Next

    End Sub
    
    Public Sub populateCombo(onlineuserno As Integer, pPage As Page, Optional fpayLocno As Integer = 0)

        For Each obj As Control In pPage.Controls

            If TypeOf obj Is MasterPage Then
                For Each objMaster As Control In obj.Controls
                    If TypeOf objMaster Is System.Web.UI.HtmlControls.HtmlForm Then
                        For Each objForm As Control In objMaster.Controls
                            If TypeOf objForm Is UpdatePanel Then
                                For Each objContent As Control In objForm.Controls(0).Controls

                                    If TypeOf objContent Is Panel Then
                                        For Each pnlContent As Control In objContent.Controls
                                            populateComboDetl(onlineuserno, pnlContent, fpayLocno)
                                        Next
                                    ElseIf TypeOf objContent Is ContentPlaceHolder Then
                                        For Each pnlContent As Control In objContent.Controls
                                            populateComboDetl(onlineuserno, pnlContent, fpayLocno)
                                        Next
                                    Else
                                        populateComboDetl(onlineuserno, objContent, fpayLocno)
                                    End If
                                Next
                            End If
                        Next
                    End If

                Next
            End If
        Next

    End Sub
    Public Sub populateCombo_In_form_Popup(onlineuserno As Integer, pnl As Panel, Optional fpayLocno As Integer = 0)
        For Each obj As Control In pnl.Controls
            If TypeOf obj Is DropDownList Then
                populateComboDetl(onlineuserno, obj, fpayLocno)
            ElseIf TypeOf obj Is UpdatePanel Then
                For Each pnlContent As Control In obj.Controls(0).Controls
                    populateComboDetl(onlineuserno, pnlContent, fpayLocno)
                Next
            Else
            End If
        Next
    End Sub
    Public Sub populateCombo_In_form_Popup_One(onlineuserno As Integer, pnl As Panel, Optional tNo As Integer = 0)
        For Each obj As Control In pnl.Controls
            If TypeOf obj Is DropDownList Then
                populateComboDetl_One(onlineuserno, obj, tNo)
            ElseIf TypeOf obj Is UpdatePanel Then
                For Each pnlContent As Control In obj.Controls(0).Controls
                    populateComboDetl_One(onlineuserno, pnlContent, tNo)
                Next
            Else
            End If
        Next
    End Sub
    Private Sub populateComboDetl(onlineuserno As Integer, objContent As Control, Optional fpayLocno As Integer = 0)
        Dim columName As String = ""
        Dim idLenght As Integer = 0

        If TypeOf objContent Is DropDownList Then
            Dim drp As New DropDownList
            Dim tableName As String = ""
            drp = CType(objContent, DropDownList)
            idLenght = drp.ID.Length - 3
            columName = Microsoft.VisualBasic.Mid(drp.ID, 4, idLenght)

            'Populate dropDown
            Try
                tableName = drp.DataMember
                drp.DataMember = ""
                If tableName <> "" And Not tableName Is Nothing Then
                    'If Not isModify Then
                    '    drp.DataSource = xBase.xLookup_Table_All(onlineuserno, tableName, fpayLocno)
                    '    drp.DataTextField = "tdesc"
                    '    drp.DataValueField = "tNo"
                    '    drp.DataBind()
                    'Else
                    drp.DataSource = clsGen.xLookup_Table(onlineuserno, tableName, fpayLocno)
                    drp.DataTextField = "tdesc"
                    drp.DataValueField = "tNo"
                    drp.DataBind()
                    'End If

                End If
            Catch ex As Exception
            End Try
        ElseIf TypeOf objContent Is AjaxControlToolkit.ComboBox Then
            Dim drp As New AjaxControlToolkit.ComboBox
            Dim tableName As String = ""
            drp = CType(objContent, AjaxControlToolkit.ComboBox)
            idLenght = drp.ID.Length - 3
            columName = Microsoft.VisualBasic.Mid(drp.ID, 4, idLenght)

            'Populate dropDown
            Try
                tableName = drp.DataMember
                drp.DataMember = ""
                If tableName <> "" And Not tableName Is Nothing Then
                    drp.DataSource = clsGen.xLookup_Table(onlineuserno, tableName, fpayLocno)
                    drp.DataTextField = "tdesc"
                    drp.DataValueField = "tNo"
                    drp.DataBind()
                End If
            Catch ex As Exception
            End Try

        End If
    End Sub

    Private Sub populateComboDetl_One(onlineuserno As Integer, objContent As Control, tNo As Integer)
        Dim columName As String = ""
        Dim idLenght As Integer = 0

        If TypeOf objContent Is DropDownList Then
            Dim drp As New DropDownList
            Dim tableName As String = ""
            drp = CType(objContent, DropDownList)
            idLenght = drp.ID.Length - 3
            columName = Microsoft.VisualBasic.Mid(drp.ID, 4, idLenght)

            'Populate dropDown
            Try
                tableName = drp.DataMember
                drp.DataMember = ""
                If tableName <> "" And Not tableName Is Nothing Then

                    drp.DataSource = clsGen.xLookup_Table_One(onlineuserno, tableName, tNo)
                    drp.DataTextField = "tdesc"
                    drp.DataValueField = "tNo"
                    drp.DataBind()



                End If
            Catch ex As Exception
            End Try
        ElseIf TypeOf objContent Is AjaxControlToolkit.ComboBox Then
            Dim drp As New AjaxControlToolkit.ComboBox
            Dim tableName As String = ""
            drp = CType(objContent, AjaxControlToolkit.ComboBox)
            idLenght = drp.ID.Length - 3
            columName = Microsoft.VisualBasic.Mid(drp.ID, 4, idLenght)

            'Populate dropDown
            Try
                tableName = drp.DataMember
                drp.DataMember = ""
                If tableName <> "" And Not tableName Is Nothing Then
                    drp.DataSource = clsGen.xLookup_Table_One(onlineuserno, tableName, tNo)
                    drp.DataTextField = "tdesc"
                    drp.DataValueField = "tNo"
                    drp.DataBind()
                End If
            Catch ex As Exception
            End Try

        End If
    End Sub

    Public Sub populateCombo_In_form_Popup_Self(onlineuserno As Integer, pnl As Panel, Optional fpayLocno As Integer = 0)
        For Each obj As Control In pnl.Controls
            If TypeOf obj Is DropDownList Then
                populateComboDetl_Self(onlineuserno, obj, fpayLocno)
            ElseIf TypeOf obj Is UpdatePanel Then
                For Each pnlContent As Control In obj.Controls(0).Controls
                    populateComboDetl_Self(onlineuserno, pnlContent, fpayLocno)
                Next
            Else
            End If
        Next
    End Sub
    Public Sub populateCombo_In_form_Popup_Self_All(onlineuserno As Integer, pnl As Panel, Optional fpayLocno As Integer = 0)
        For Each obj As Control In pnl.Controls
            If TypeOf obj Is DropDownList Then
                populateComboDetl_Self_All(onlineuserno, obj, fpayLocno)
            ElseIf TypeOf obj Is UpdatePanel Then
                For Each pnlContent As Control In obj.Controls(0).Controls
                    populateComboDetl_Self_All(onlineuserno, pnlContent, fpayLocno)
                Next
            Else
            End If
        Next
    End Sub
    Private Sub populateComboDetl_Self(onlineuserno As Integer, objContent As Control, Optional fpayLocno As Integer = 0)
        Dim columName As String = ""
        Dim idLenght As Integer = 0
        Dim lookup As New clsGenericClass

        If TypeOf objContent Is DropDownList Then
            Dim drp As New DropDownList
            Dim tableName As String = ""
            drp = CType(objContent, DropDownList)
            idLenght = drp.ID.Length - 3
            columName = Microsoft.VisualBasic.Mid(drp.ID, 4, idLenght)

            'Populate dropDown
            Try
                tableName = drp.DataMember
                drp.DataMember = ""
                If tableName <> "" And Not tableName Is Nothing Then

                    drp.DataSource = lookup.xLookup_Table_Self(onlineuserno, tableName, fpayLocno)
                    drp.DataTextField = "tdesc"
                    drp.DataValueField = "tNo"
                    drp.DataBind()

                End If
            Catch ex As Exception
            End Try
        ElseIf TypeOf objContent Is AjaxControlToolkit.ComboBox Then
            Dim drp As New AjaxControlToolkit.ComboBox
            Dim tableName As String = ""
            drp = CType(objContent, AjaxControlToolkit.ComboBox)
            idLenght = drp.ID.Length - 3
            columName = Microsoft.VisualBasic.Mid(drp.ID, 4, idLenght)

            'Populate dropDown
            Try
                tableName = drp.DataMember
                drp.DataMember = ""
                If tableName <> "" And Not tableName Is Nothing Then
                    drp.DataSource = lookup.xLookup_Table_Self(onlineuserno, tableName, fpayLocno)
                    drp.DataTextField = "tdesc"
                    drp.DataValueField = "tNo"
                    drp.DataBind()
                End If
            Catch ex As Exception
            End Try

        End If
    End Sub
    Private Sub populateComboDetl_Self_All(onlineuserno As Integer, objContent As Control, Optional fpayLocno As Integer = 0)
        Dim columName As String = ""
        Dim idLenght As Integer = 0
        Dim lookup As New clsGenericClass

        If TypeOf objContent Is DropDownList Then
            Dim drp As New DropDownList
            Dim tableName As String = ""
            drp = CType(objContent, DropDownList)
            idLenght = drp.ID.Length - 3
            columName = Microsoft.VisualBasic.Mid(drp.ID, 4, idLenght)

            'Populate dropDown
            Try
                tableName = drp.DataMember
                drp.DataMember = ""
                If tableName <> "" And Not tableName Is Nothing Then

                    drp.DataSource = lookup.xLookup_Table_Self_All(onlineuserno, tableName, fpayLocno)
                    drp.DataTextField = "tdesc"
                    drp.DataValueField = "tNo"
                    drp.DataBind()


                End If
            Catch ex As Exception
            End Try
        ElseIf TypeOf objContent Is AjaxControlToolkit.ComboBox Then
            Dim drp As New AjaxControlToolkit.ComboBox
            Dim tableName As String = ""
            drp = CType(objContent, AjaxControlToolkit.ComboBox)
            idLenght = drp.ID.Length - 3
            columName = Microsoft.VisualBasic.Mid(drp.ID, 4, idLenght)

            'Populate dropDown
            Try
                tableName = drp.DataMember
                drp.DataMember = ""
                If tableName <> "" And Not tableName Is Nothing Then
                    drp.DataSource = lookup.xLookup_Table_Self(onlineuserno, tableName, fpayLocno)
                    drp.DataTextField = "tdesc"
                    drp.DataValueField = "tNo"
                    drp.DataBind()
                End If
            Catch ex As Exception
            End Try

        End If
    End Sub
    Public Sub populateCombo_Applicant(onlineuserno As Integer, pPage As Page, Optional fpayLocno As Integer = 0)

        For Each obj As Control In pPage.Controls

            If TypeOf obj Is MasterPage Then
                For Each objMaster As Control In obj.Controls
                    If TypeOf objMaster Is System.Web.UI.HtmlControls.HtmlForm Then
                        For Each objForm As Control In objMaster.Controls
                            If TypeOf objForm Is ContentPlaceHolder Then
                                For Each objContent As Control In objForm.Controls

                                    If TypeOf objContent Is Panel Then
                                        For Each pnlContent As Control In objContent.Controls
                                            populateComboDetl_Applicant(onlineuserno, pnlContent, fpayLocno)
                                        Next
                                    ElseIf TypeOf objContent Is UpdatePanel Then
                                        For Each pnlContent As Control In objContent.Controls(0).Controls
                                            populateComboDetl_Applicant(onlineuserno, pnlContent, fpayLocno)
                                        Next
                                    Else
                                        populateComboDetl_Applicant(onlineuserno, objContent, fpayLocno)
                                    End If
                                Next
                            End If
                        Next
                    End If

                Next
            End If


        Next

    End Sub
    Private Sub populateComboDetl_Applicant(onlineuserno As Integer, objContent As Control, Optional fpayLocno As Integer = 0)
        Dim columName As String = ""
        Dim idLenght As Integer = 0
        Dim lookup As New clsGenericClass

        If TypeOf objContent Is DropDownList Then
            Dim drp As New DropDownList
            Dim tableName As String = ""
            drp = CType(objContent, DropDownList)
            idLenght = drp.ID.Length - 3
            columName = Microsoft.VisualBasic.Mid(drp.ID, 4, idLenght)

            'Populate dropDown
            Try
                tableName = drp.DataMember
                drp.DataMember = ""
                If tableName <> "" And Not tableName Is Nothing Then
                    drp.DataSource = lookup.xLookup_Table_Applicant(onlineuserno, tableName, fpayLocno)
                    drp.DataTextField = "tdesc"
                    drp.DataValueField = "tNo"
                    drp.DataBind()
                End If
            Catch ex As Exception
            End Try
        ElseIf TypeOf objContent Is AjaxControlToolkit.ComboBox Then
            Dim drp As New AjaxControlToolkit.ComboBox
            Dim tableName As String = ""
            drp = CType(objContent, AjaxControlToolkit.ComboBox)
            idLenght = drp.ID.Length - 3
            columName = Microsoft.VisualBasic.Mid(drp.ID, 4, idLenght)

            'Populate dropDown
            Try
                tableName = drp.DataMember
                drp.DataMember = ""
                If tableName <> "" And Not tableName Is Nothing Then
                    drp.DataSource = lookup.xLookup_Table_Applicant(onlineuserno, tableName, fpayLocno)
                    drp.DataTextField = "tdesc"
                    drp.DataValueField = "tNo"
                    drp.DataBind()
                End If
            Catch ex As Exception
            End Try

        End If
    End Sub
End Class

