Imports clsLib
Imports System.Data
Imports System.IO
Imports DevExpress.Web

Partial Class Include_FileUpload
    Inherits System.Web.UI.UserControl
    Dim UserNo As Integer
    Dim yID As Integer = 0
    Dim PayLocNo As Integer = 0

    Private _ID As Integer
    Public Property xID() As Integer
        Get
            Return _ID
        End Get
        Set(value As Integer)
            _ID = value
        End Set
    End Property

    Private _xModify As Boolean
    Public Property xModify() As Boolean
        Get
            Return _xModify
        End Get
        Set(value As Boolean)
            _xModify = value
        End Set
    End Property

    Private _xMenuType As String
    Public Property xMenuType() As String
        Get
            Return _xMenuType
        End Get
        Set(value As String)
            _xMenuType = value
        End Set
    End Property

    Public Sub Show()
        yID = xID
        hifNo.Value = yID
        PopulateGrid()

        lnkAdd.Visible = xModify
        fuDoc.Visible = xModify
        lnkDelete.Visible = xModify

        ModalPopupExtender1.Show()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
    End Sub

    Protected Sub PopulateGrid()
        If Generic.ToStr(xMenuType) = "" Then
            xMenuType = Generic.ToStr(Session("xMenuType"))
        End If
        Try
            Dim _dt As DataTable
            _dt = SQLHelper.ExecuteDataTable("EDoc_Web", UserNo, Generic.ToInt(hifNo.Value), "", _xMenuType)
            Me.grdMain.DataSource = _dt
            Me.grdMain.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Dim status As Integer = SaveRecord()
        If status = 1 Then
            MessageBox.Alert(MessageTemplate.SuccessSave, "success", Me)
            PopulateGrid()
        ElseIf status = 2 Then
            MessageBox.Alert("Invalid file type.", "warning", Me)
        ElseIf status = 3 Then
            MessageBox.Alert("File size is over the limit", "warning", Me)            
        ElseIf status = 4 Then
            MessageBox.Alert("File server unavailable.", "warning", Me)
        Else
            MessageBox.Alert(MessageTemplate.ErrorSave, "warning", Me)
        End If
        ModalPopupExtender1.Show()
    End Sub

    Private Function SaveRecord() As Integer
        Dim retval As Integer = 0
        If fuDoc.HasFile Then
            Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String = "", IsValidExt As Boolean = False
            Dim filesize_limit As Integer = Generic.ToStr(ConfigurationManager.AppSettings("filesize"))
            'Dim _ds As New DataSet, NewFileName As String = ""
            'Dim contenttype As String = "", filetypecode As String = "", 
            Try
                Filename = IO.Path.GetFileName(fuDoc.PostedFile.FileName)
                FileExt = IO.Path.GetExtension(fuDoc.PostedFile.FileName)
                IsValidExt = Generic.ToBol(SQLHelper.ExecuteScalar("EFileType_WebCheck", UserNo, FileExt, xMenuType))
                If IsValidExt Then
                    '    'NewFileName = Guid.NewGuid().ToString()
                    '    Dim dt As DataTable
                    '    Dim fs As IO.Stream = fuDoc.PostedFile.InputStream
                    '    Dim br As New BinaryReader(fs)
                    '    Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                    '    FileSize = fs.Length
                    '    ActualPath = getFile_settings()
                    '    If FileSize < 512000 Then
                    '        dt = SQLHelper.ExecuteDataTable("EDoc_WebSave_File", UserNo, Generic.ToInt(hifNo.Value), 0, IO.Path.GetFileNameWithoutExtension(Filename), Filename, FileExt, FileSize, ActualPath, Generic.ToStr(Session("xMenuType")), 1, 0, "", PayLocNo)
                    '        For Each row As DataRow In dt.Rows
                    '            NewFileName = Generic.ToStr(row("tFilename"))                            
                    '        Next
                    '        fuDoc.PostedFile.SaveAs(Server.MapPath(ActualPath) & "\" & NewFileName)
                    '        Return 1                        
                    '    Else
                    '        Return 3
                    '    End If
                    'Dim fs As IO.Stream = fuDoc.PostedFile.InputStream
                    'Dim br As New BinaryReader(fs)
                    'Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                    'FileSize = fs.Length
                    ActualPath = ConfigurationManager.AppSettings("drive_path")
                    If IsPathExists(ActualPath) = False Then : Return 4 : End If

                    If FileSize < filesize_limit Then
                        Dim dt As DataTable
                        ActualPath = ConfigurationManager.AppSettings("drive_path")
                        Dim NewFilename As String = ""
                        dt = SQLHelper.ExecuteDataTable("EDoc_WebSave_File", UserNo, Generic.ToInt(hifNo.Value), 0, IO.Path.GetFileNameWithoutExtension(Filename), Filename, FileExt, FileSize, ActualPath, xMenuType, 1, 0, "", PayLocNo)
                        For Each row As DataRow In dt.Rows
                            NewFilename = Generic.ToStr(row("tFilename"))
                        Next
                        fuDoc.PostedFile.SaveAs(ActualPath & NewFilename)
                        Return 1
                    Else
                        Return 3
                    End If
                Else
                    Return 2
                End If
            Catch ex As Exception

            End Try
        End If
        Return retval

    End Function

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, lnk As New LinkButton, Count As Integer = 0
        Dim path As String = ConfigurationManager.AppSettings("drive_path"), i As Integer = 0
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"DocNo"})
            For Each item As Integer In fieldValues
                Dim dt As DataTable = SQLHelper.ExecuteDataTable("EDoc_WebOne", UserNo, item)
                If dt.Rows.Count > 0 Then
                    Dim filename As String = ""
                    For Each dRow As DataRow In dt.Rows
                        filename = Generic.ToStr(dRow("ActualFileName"))
                    Next
                    Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & filename.ToString)
                    If file.Exists Then
                        file.Delete()
                    End If
                    Generic.DeleteRecordAudit("EDoc", UserNo, item)
                    count = count + 1
                End If
            Next

            If count > 0 Then
                MessageBox.Alert("(" + Count.ToString + ") " + MessageTemplate.SuccessDelete, "success", Me)
                PopulateGrid()
            Else
                MessageBox.Alert(MessageTemplate.NoSelectedTransaction, "information", Me)
            End If
        Else
            MessageBox.Alert(MessageTemplate.DeniedDelete, "warning", Me)
        End If
        ModalPopupExtender1.Show()
    End Sub

    Private Sub WriteFile(strPath As String, Buffer As Byte())
        strPath = Server.MapPath(strPath)
        Dim newFile As FileStream = New FileStream(strPath, FileMode.Create)
        newFile.Write(Buffer, 0, Buffer.Length)
        newFile.Close()
    End Sub

    Protected Sub lnkDownload_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            Dim doc As Byte() = Nothing
            Dim filename As String = ""
            Dim orgname As String = ""
            Dim dt As DataSet
            Dim datafile() As Byte = Nothing
            Dim fContentType As String = ""
            Dim fileExt As String = ""
            Dim DocNo As Integer = 0
            Dim RowNo As Integer = 0
            Dim path As String = ConfigurationManager.AppSettings("drive_path")
            lnk = sender
            DocNo = lnk.CommandArgument
            dt = SQLHelper.ExecuteDataSet("EDoc_WebOne", UserNo, DocNo)
            If dt.Tables.Count > 0 Then
                If dt.Tables(0).Rows.Count > 0 Then
                    filename = Generic.ToStr(dt.Tables(0).Rows(0)("ActualFileName"))
                    orgname = Generic.ToStr(dt.Tables(0).Rows(0)("docfile"))
                    'datafile = dt.Tables(0).Rows(0)("datafile")
                    fContentType = Generic.ToStr(dt.Tables(0).Rows(0)("contenttype"))
                    fileExt = Generic.ToStr(dt.Tables(0).Rows(0)("docext"))
                End If
            End If
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path & filename.ToString)
            If file.Exists Then
                Response.Clear()
                Response.Buffer = True
                Response.AddHeader("Content-Disposition", "attachment; filename=" & orgname)
                Response.ContentType = "application/octet-stream"
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                'Response.BinaryWrite(datafile)
                Response.TransmitFile(path & filename.ToString)
                Response.End()
            Else
                MessageBox.Alert("File not available", "information", Me)
            End If
        Catch ex As Exception
            MessageBox.Warning("Error downloading file.", Me)
        End Try
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnkOpenFile_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub

    Private Function IsPathExists(path As String) As Boolean
        Dim retVal As Boolean = False
        If Generic.ToStr(ConfigurationManager.AppSettings("share_drive")).ToLower() = "yes" Then
            If Directory.Exists(path) Then
                Return True
            End If
        End If
        Return retVal
    End Function

End Class
