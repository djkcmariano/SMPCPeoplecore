Imports System.Data
Imports clsLib
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web
Imports System.IO

Imports System.Data.SqlClient


Partial Class Secured_EmpListing
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)

        If Not IsPostBack Then
            PopulateDropDown()
           
        End If

        'AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click
        PopulateGrid()
        'Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")

        'grdMain.DataSourceID = SqlDataSource1.ID
        'Generic.PopulateSQLDatasource("EEmployee_WebFiltered", SqlDataSource1, UserNo.ToString(), PayLocNo.ToString(), Generic.ToInt(cboTabNo.SelectedValue).ToString(), Generic.ToInt(cbofilterby.SelectedValue), Generic.ToInt(cbofiltervalue.SelectedValue), Filter1.SearchText)


        'Dim dt As DataTable
        'dt = SQLHelper.ExecuteDataTable("EEmployee_WebFiltered", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue), Generic.ToInt(cbofilterby.SelectedValue), Generic.ToInt(cbofiltervalue.SelectedValue), Filter1.SearchText)
        'grdMain.DataSource = dt
        'grdMain.DataBind()

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployee_Web", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue), FilterSearch1.SearchText, FilterSearch1.SelectTop.ToString, FilterSearch1.FilterParam.ToString)
        'dt = SQLHelper.ExecuteDataTable("EEmployee_WebFiltered", UserNo, PayLocNo, Generic.ToInt(cboTabNo.SelectedValue), Generic.ToInt(cbofilterby.SelectedValue), Generic.ToInt(cbofiltervalue.SelectedValue), Filter1.SearchText)
        grdMain.DataSource = dt
        grdMain.DataBind()

        'EntityServerModeDataSource1.ContextTypeName = "EmployeeDataContext"
        'EntityServerModeDataSource1.TableName = _dt.TableName
        'grdMain.DataSourceID = EntityServerModeDataSource1.ID

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim URL As String
            URL = Generic.GetFirstTab("0")
            If URL <> "" Then
                Response.Redirect(URL)
            End If
        End If
    End Sub

    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton, i As Integer
        lnk = sender
        Dim URL As String
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo"})))
        i = Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeNo"}))
        If URL <> "" Then
            Response.Redirect("~/secured/EmpEditPerson.aspx?id=" & i & "&tModify=false&IsClickMain=1")
        End If
    End Sub
    'Protected Sub lnkUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Response.Redirect("~/secured/EmpUploadAccount.aspx?tModify=false&IsClickMain=1")
    'End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 6)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub



    '    private void ListParms()
    '{
    '    SqlConnection conn = new SqlConnection("my sql connection string");
    '    SqlCommand cmd = new SqlCommand("proc name", conn);
    '    cmd.CommandType = CommandType.StoredProcedure;
    '    conn.Open();
    '    SqlCommandBuilder.DeriveParameters(cmd);
    '    foreach (SqlParameter p in cmd.Parameters)
    '    {
    '       Console.WriteLine(p.ParameterName);
    '    }
    '}

    Private Sub ListParams(ParamArray param() As String)
        'Dim str As String = ""
        'Dim conn As SqlConnection = New SqlConnection(GetIniFile())
        'Dim cmd As SqlCommand = New SqlCommand("EEmployee_Web", conn)
        'cmd.CommandType = CommandType.StoredProcedure
        'conn.Open()
        'SqlCommandBuilder.DeriveParameters(cmd)
        'For Each p As SqlParameter In cmd.Parameters

        '    If p.ParameterName <> "@RETURN_VALUE" Then
        '        'str = str & p.ParameterName & "<br />"
        '        str = str & p.DbType & "<br />"
        '    End If
        'Next
        'MessageBox.Information(str, Me)

        'Dim str As String = ""
        'For Each xstr As String In param
        '    str = str & xstr & "<br />"
        'Next
        'MessageBox.Information(str, Me)
        MessageBox.Information(param.GetValue(1), Me)

    End Sub

    Protected Sub lnkAttachment_Click(sender As Object, e As EventArgs)
        'Dim lnk As New LinkButton
        'lnk = sender
        'Response.Redirect("~/secured/frmFileUpload.aspx?id=" & Generic.Split(lnk.CommandArgument, 0) & "&display=" & Generic.Split(lnk.CommandArgument, 1))
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        FileUpload.xID = Generic.ToInt(Generic.Split(lnk.CommandArgument, 0))
        FileUpload.xModify = True
        FileUpload.Show()

    End Sub
    Protected Sub lnkUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, Session("xFormName"), Session("xTableName")) Then
            Response.Redirect("~/secured/EmpListing_Upload.aspx?id=0")
        Else
            MessageBox.Critical(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub
    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim status As Integer = SaveRecord_XLS()
        If status = 1 Then
            MessageBox.Success("201 file has been successfully migrated.", Me)
            PopulateGrid()
        ElseIf status = 0 Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)

        End If

    End Sub
    'Private Function SaveRecord() As Integer


    '    Dim retval As Integer = 0
    '    If fuDoc.HasFile Then
    '        Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String = ""
    '        Dim _ds As New DataSet, NewFileName As String = ""
    '        Dim contenttype As String = "", filetypecode As String = ""
    '        Try

    '            Filename = IO.Path.GetFileName(fuDoc.PostedFile.FileName)
    '            FileExt = IO.Path.GetExtension(fuDoc.PostedFile.FileName)

    '            Dim fs As IO.Stream = fuDoc.PostedFile.InputStream
    '            Dim br As New BinaryReader(fs)
    '            Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
    '            FileSize = fs.Length
    '            ActualPath = getFile_settings()
    '            WriteFile(ActualPath & "\" & Filename.ToString, bytes)
    '            SQLHelper.ExecuteNonQuery("EEmployee_WebUpload_XLS", ActualPath & "\" & Filename.ToString)
    '            retval = 1
    '        Catch ex As Exception

    '        End Try
    '    End If
    '    Return retval

    'End Function
    Private Function SaveRecord_XLS() As Integer


        Dim retval As Integer = 0
        If fuDoc.HasFile Then
            Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String = ""
            Dim _ds As New DataSet, NewFileName As String = ""
            Dim contenttype As String = "", filetypecode As String = ""
            Try

                Filename = IO.Path.GetFileName(fuDoc.PostedFile.FileName)
                FileExt = IO.Path.GetExtension(fuDoc.PostedFile.FileName)

                Dim fs As IO.Stream = fuDoc.PostedFile.InputStream
                Dim br As New BinaryReader(fs)


                Dim bytes As Byte() = br.ReadBytes(Generic.ToInt(fs.Length))
                FileSize = fs.Length
                ActualPath = "c:\Upload"
                WriteFile(ActualPath & "\" & Filename.ToString, bytes)


                Dim MyConnection As System.Data.OleDb.OleDbConnection
                Dim DtSet As System.Data.DataSet
                Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
                'MyConnection = New System.Data.OleDb.OleDbConnection("provider=MICROSOFT.ACE.OLEDB.12.0;Data Source=" + ActualPath & "\" & Filename.ToString + ";Extended Properties=Excel 8.0;")
                MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='c:\vb.net-informations.xls';Extended Properties=Excel 8.0;")
                MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [Sheet1$]", MyConnection)
                MyCommand.TableMappings.Add("Table", "EEmployeeTemp")
                DtSet = New System.Data.DataSet
                MyCommand.Fill(DtSet)
                MyConnection.Close()


                Dim expr As String = "SELECT * FROM [Sheet1$]"

                SQLHelper.ExecuteNonQuery("Truncate table dbo.EEmployeeTemp")

                Dim SQLconn As New SqlConnection()
                Dim ConnString As String = SQLHelper.ConSTR
                Dim objCmdSelect As System.Data.OleDb.OleDbCommand = New System.Data.OleDb.OleDbCommand(expr, MyConnection)
                Dim objDR As System.Data.OleDb.OleDbDataReader

                SQLconn.ConnectionString = SQLHelper.ConSTR

                Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(SQLHelper.ConSTR)
                    bulkCopy.DestinationTableName = "EEmployeeTemp"
                    Try
                        MyConnection.Open()
                        objDR = objCmdSelect.ExecuteReader
                        bulkCopy.WriteToServer(objDR)
                        objDR.Close()
                        SQLconn.Close()
                        MyConnection.Close()

                        SQLHelper.ExecuteNonQuery("EEmployee_WebUpload_XLS_V2", ActualPath & "\" & Filename.ToString, PayLocNo)
                    Catch ex As Exception

                    End Try
                End Using



                retval = 1
            Catch ex As Exception

            End Try
        End If
        Return retval

    End Function
    Private Function getFile_settings() As String
        Try


            'Dim iInitArr As String
            'Dim i As Integer
            'Dim fs As FileStream
            'Dim filename = HttpContext.Current.Server.MapPath("~/secured/connectionstr/") & "folder.ini"
            'Dim retval As String = ""


            'fs = New FileStream(filename, FileMode.Open, FileAccess.Read)
            'Dim l As Integer = 0, ftext As String = ""
            'Dim d As New StreamReader(fs)

            'd.BaseStream.Seek(0, SeekOrigin.Begin)
            'If d.Peek() > 0 Then
            '    While d.Peek() > -1
            '        i = d.Peek
            '        ftext = d.ReadLine()
            '        iInitArr = ftext
            '        If l = 0 Then
            '            retval = iInitArr
            '        End If
            '        l = l + 1
            '    End While
            '    d.Close()
            'End If
            'd.Close()
            'Return retval

            Dim retval As String = ""
            Dim ds As DataSet
            ds = SQLHelper.ExecuteDataSet("EDocFolder_WebOne")
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    retval = Generic.ToStr(ds.Tables(0).Rows(0)("path"))
                End If
            End If

            Return retval
        Catch ex As Exception

        End Try
    End Function
    Private Sub WriteFile(strPath As String, Buffer As Byte())
        'Create a file
        Dim newFile As FileStream = New FileStream(strPath, FileMode.Create)

        'Write data to the file
        newFile.Write(Buffer, 0, Buffer.Length)

        'Close file
        newFile.Close()
    End Sub
End Class
