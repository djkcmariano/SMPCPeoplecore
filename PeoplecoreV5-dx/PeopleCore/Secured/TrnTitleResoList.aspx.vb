Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Partial Class Secured_TrnTitleResoList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim DocPath As String = ""

    Private Sub PopulateGrid()
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("ETrnTitleReso_Web", UserNo, TransNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnTitleReso_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        AccessRights.CheckUser(UserNo, "TrnTitleList.aspx", "ETrnTitle")

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateTabHeader()
        End If

        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("ETrnTitle_WebTabHeader", UserNo, TransNo)
            For Each row As DataRow In dt.Rows
                lbl.Text = Generic.ToStr(row("Display"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "TrnTitleList.aspx", "ETrnTitle") Then
            Generic.ClearControls(Me, "Panel1")
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "TrnTitleList.aspx", "ETrnTitle") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Generic.ClearControls(Me, "Panel1")
            PopulateData(container.Grid.GetRowValues(container.VisibleIndex, New String() {"TrnTitleResoNo"}))
            mdlMain.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim DeleteCount As Integer = 0

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "TrnTitleList.aspx", "ETrnTitle") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"TrnTitleResoNo"})
            Dim str As String = ""
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("ETrnTitleReso", UserNo, CType(item, Integer))
                DeleteCount = DeleteCount + 1
            Next

            If DeleteCount > 0 Then
                PopulateGrid()
                MessageBox.Success("(" + DeleteCount.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            Else
                MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
            End If
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Private Function SaveRecord() As Boolean

        Dim TrnTitleResoNo As Integer = Generic.ToInt(Me.txtTrnTitleResoNo.Text)
        Dim Description As String = Generic.CheckDBNull(Me.txtDescription.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim TrnResoNo As Integer = Generic.CheckDBNull(Me.cboTrnResoNo.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType)
        Dim LinkAddress As String = Generic.CheckDBNull(Me.txtLinkAddress.Text, clsBase.clsBaseLibrary.enumObjectType.StrType)
        Dim IsOnline As Boolean = Generic.CheckDBNull(Me.txtIsOnline.Checked, clsBase.clsBaseLibrary.enumObjectType.IntType)

        Dim RetVal As Integer
        Dim ds As New DataSet

        ds = SQLHelper.ExecuteDataSet("ETrnTitleReso_WebSave", UserNo, TrnTitleResoNo, TransNo, Description, TrnResoNo, LinkAddress, IsOnline)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                RetVal = Generic.CheckDBNull(ds.Tables(0).Rows(0)("RetVal"), clsBase.clsBaseLibrary.enumObjectType.IntType)
            End If
        End If

        If RetVal > 0 Then
            Dim xFile As String = "", DocId As Long = 0

            If Not txtFile Is Nothing Then
                If txtFile.HasFile Then
                    Dim MyPath As String, MyName As String, strFileName As String, c As String, pExt As String, pFile As String, pPos As Integer, fFile As String

                    If DocPath = "" Then
                        DocPath = Me.MapPath("~\Secured\documents") & "\"
                    End If

                    MyPath = DocPath
                    MyName = Dir(MyPath, vbDirectory)

                    strFileName = txtFile.PostedFile.FileName
                    c = System.IO.Path.GetFileName(strFileName)
                    pExt = ""
                    fFile = strFileName
                    pPos = c.LastIndexOf(".")
                    If pPos > 0 Then
                        pExt = c.Substring(pPos + 1, c.Length - pPos - 1)
                        pPos = c.LastIndexOf("/")
                        fFile = c.Substring(pPos + 1, c.Length - pPos - 1)
                    End If
                    Dim _ds1 As DataSet
                    _ds1 = SQLHelper.ExecuteDataSet("ETrnTitleReso_WebSaveFile", UserNo, RetVal, fFile, strFileName, pExt, CType(txtFile.PostedFile.ContentLength, Int64))
                    If _ds1.Tables.Count > 0 Then
                        DocId = _ds1.Tables(0).Rows(0)(0)
                        pFile = "Trn-" & fFile
                        txtFile.PostedFile.SaveAs(MyPath & pFile)
                    End If
                End If

                SaveRecord = True
            End If

        Else
            SaveRecord = False
            Me.txtFile.Focus()
        End If
    End Function

    Protected Sub lnkDocView_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lStr As String = ""
        Dim lLink As New LinkButton

        If DocPath = "" Then
            DocPath = Me.MapPath("~\Secured\documents") & "\"
        End If

        lLink = sender

        lStr = DocPath & "Trn-" & lLink.CommandName.ToString.Trim

        'previewFile(lStr)

        If Not DownloadFile(lStr) Then
            MessageBox.Alert("Sorry, the file is no longer available!", "information", Me)
        End If

    End Sub

    Private Sub previewFile(ByVal fullpath As String)
        Dim FileName As String = ""
        FileName = IO.Path.GetFileName(fullpath)
        Response.Clear()
        Response.ClearContent()
        Response.ContentType = "application/octet-stream"
        Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
        Response.TransmitFile(fullpath)
        Response.End()

    End Sub

    Protected Sub lnkLinkView_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lStr As String = ""
        Dim lLink As New LinkButton

        lLink = sender
        lStr = lLink.Text.ToString

        'OpenLink(lStr)
        fRegisterStartupScript("dsadas", "window.open('" + lStr + "','_bank');")
    End Sub

    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub

    Private Sub OpenLink(ByVal tpath As String)
        Dim sb As New StringBuilder
        sb.Append("<script>")
        sb.Append("window.open('" & tpath & "' ,'_blank','toolbars=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes,with=800,height=550');")
        sb.Append("</scri")
        sb.Append("pt>")
        ClientScript.RegisterClientScriptBlock(Me.GetType, "test", sb.ToString())
    End Sub

    Private Function FileExists(ByVal FileFullPath As String) As Boolean
        Dim f As New IO.FileInfo(FileFullPath)
        Return f.Exists
    End Function

    Private Function DownloadFile(ByVal FilePath As String, Optional ByVal ContentType As String = "", Optional ByVal IsDelete As Boolean = False) As Boolean
        Dim myFileInfo As System.IO.FileInfo
        Dim StartPos As Long = 0, FileSize As Long, EndPos As Long

        If System.IO.File.Exists(FilePath) Then

            myFileInfo = New System.IO.FileInfo(FilePath)
            FileSize = myFileInfo.Length
            EndPos = FileSize

            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ClearHeaders()
            HttpContext.Current.Response.ClearContent()

            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment; filename=" & myFileInfo.Name)
            HttpContext.Current.Response.WriteFile(FilePath, StartPos, EndPos)
            HttpContext.Current.Response.End()

            DownloadFile = True

            If IsDelete Then
                myFileInfo.Delete()
            End If
        Else
            DownloadFile = False
        End If

    End Function

    Protected Sub lnkOpenFile_PreRender(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub


End Class
