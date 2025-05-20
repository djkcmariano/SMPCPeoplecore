Imports System.Data
Imports clsLib


Partial Class Secured_EmpCheckListEdit
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable
    Dim EmployeeDialectNo As Integer = 0
    Dim xScript As String = ""
    Dim showFrm As New clsFormControls
    Dim clsGeneric As New clsGenericClass
    Dim employeeNo As Integer = 0

    
    Private Sub PopulateGrid()
        Try
            Dim dscount As Double = 0
            Dim _ds As DataSet
            _ds = SQLHelper.ExecuteDataSet("EEmployeeChecklist_Web", xPublicVar.xOnlineUseNo, employeeNo)

            If _ds.Tables.Count > 0 Then
                If _ds.Tables(0).Rows.Count > 0 Then
                    dscount = _ds.Tables(0).Rows.Count
                End If
            End If

            Me.grdMain.PageIndex = ViewState(xScript & "PageNo")
            Me.grdMain.DataSource = _ds
            Me.grdMain.DataBind()


        Catch ex As Exception
        End Try
    End Sub

    Private Sub PopulateName()
        img.ImageUrl = "../secured/frmShowImage.ashx?tNo=" & employeeno & "&tIndex=2"

    End Sub
    Protected Sub lnkUploadPhoto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkUploadPhoto.Click
        Try
            mdlUpload.Show()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkUploadSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tStatus As Integer = PoplulateCSVFile()
        If tStatus = 1 Then
            MessageBox.Warning("Uploading of file successfully done.", Me)
        ElseIf tStatus = 2 Then
            MessageBox.Warning("Error uploading", Me)

        End If

    End Sub

    Private Function PoplulateCSVFile() As Integer
        Dim lastname As String = ""
        Dim FormatedDate As String = ""
        Dim NewFileName As String = ""
        Dim FileExt As String = ""
        Dim ServerDIR As String = ""
        Dim ds As New DataSet
        Dim filesize As Integer = 0

        Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False

        If fuPhoto.HasFile = True Then
            FileExt = IO.Path.GetExtension(fuPhoto.PostedFile.FileName)
            filesize = fuPhoto.PostedFile.ContentLength
            If filesize > 2097152 Then

                MessageBox.Warning("Maximum allowable file to upload is only 2MB.", Me)
                Exit Function
            End If

            If FileExt = ".jpg" Or FileExt = ".jpeg" Or FileExt = ".gif" Or FileExt = ".png" Or FileExt = ".JPG" Or FileExt = ".JPEG" Or FileExt = ".GIF" Or FileExt = ".PNG" Then


                tFilepath = fuPhoto.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                FileExt = IO.Path.GetExtension(tfilename)
                ds = SQLHelper.ExecuteDataSet("EmployeePhoto_WebUpdate", employeeno, FileExt)
                Dim empcode$ = ""

                If ds.Tables.Count <> 0 Then
                    If ds.Tables(0).Rows.Count <> 0 Then
                        empcode$ = Generic.CheckDBNull(ds.Tables(0).Rows(0)("Employeecode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    End If
                End If
                ServerDIR = Server.MapPath("../") & "EmployeeImage\" & empcode$ & FileExt
                fuPhoto.PostedFile.SaveAs(ServerDIR)
                'Save the photo into database for the report purposes
                clsGeneric.SaveRecordPhoto(ServerDIR, employeeno)
                Return 1
            Else
                MessageBox.Warning("Invalid image format'", Me)
                Return 3
            End If
        Else
            Return 2
        End If
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("onlineuserno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        AccessRights.CheckUser(xPublicVar.xOnlineUseNo)

        employeeno = Generic.CheckDBNull(Request.QueryString("id"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        xScript = Request.ServerVariables("SCRIPT_NAME")
        xScript = Generic.GetPath(xScript)


        If Not IsPostBack Then

            PopulateGrid()

            PopulateName()
        End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub
    Public Sub ApplicantChecklistAutoupdate()
        SQLHelper.ExecuteNonQuery("EEmployeeChecklist_WebSaveAuto", xPublicVar.xOnlineUseNo, xPublicVar.xEmployeeNo)
    End Sub
    Protected Sub grdMainMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdMain.PageIndexChanging
        Viewstate(xScript & "No") = 0
        Viewstate(xScript & "PageNo") = e.NewPageIndex
        Session(Left(xScript, Len(xScript) - 5)) = 0
        PopulateGrid()
    End Sub

    Protected Sub lnkSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lbl As New Label
        Dim tcount As Integer, chksubmitted As New CheckBox
        Dim tcountS As Integer = 0, lblmrhiredmassno As New Label

        If AccessRights.IsAllowUser(xPublicVar.xOnlineUseNo, AccessRights.EnumPermissionType.AllowEdit) Then
            For tcount = 0 To Me.grdMain.Rows.Count - 1
                lbl = CType(grdMain.Rows(tcount).FindControl("lblIdd"), Label)
                chksubmitted = CType(grdMain.Rows(tcount).FindControl("txtIsSubmitted"), CheckBox)

                If Not lbl Is Nothing Then
                    SQLHelper.ExecuteNonQuery("EEmployeeCheckList_WebSave", xPublicVar.xOnlineUseNo, employeeNo, Generic.CheckDBNull(lbl.Text, Global.clsBase.clsBaseLibrary.enumObjectType.IntType), Generic.CheckDBNull(chksubmitted.Checked, Global.clsBase.clsBaseLibrary.enumObjectType.IntType))
                    tcountS = tcountS + 1
                End If
            Next
        Else
            MessageBox.Information(AccessRights.GetDeniedMessage(AccessRights.EnumPermissionType.AllowEdit), Me)
        End If
        If tcountS > 0 Then
            MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, "~/secured/EmpCheckListEditaspx?INo=0&tModify=False&transNo=" & employeeNo)
        End If

    End Sub


End Class



