Imports System.Data
Imports System.IO
Imports clsLib
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports Microsoft.VisualBasic.FileIO
Imports DevExpress.Export.Xl

Partial Class Secured_PayBankFiles
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim compno As Integer = cboCompNo.SelectedValue
            Dim grpno As Integer = cboGrpNo.SelectedValue
            Dim paydate As String = txtPayDate.Text
            Dim creditdate As String = txtCreditDate.Text
            dt = SQLHelper.ExecuteDataTable("pr_EBankRemit_Web", UserNo, PayLocNo, compno, grpno, paydate, creditdate, 0)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        'TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        If Not IsPostBack Then
            AccessRights.CheckUser(UserNo)
            PopulateDropDown(cboCompNo, 0, 0)
        End If
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Private Sub PopulateDropDown(ByVal cbo As DropDownList, ByVal mode As Integer, ByVal ref As Integer)
        Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", Generic.ToInt(Session("xPayLocNo")))
        Try
            cbo.DataSource = SQLHelper.ExecuteDataSet("EDropDownList_WebLookup", UserNo, ref, mode)
            cbo.DataTextField = "tDesc"
            cbo.DataValueField = "tno"
            cbo.DataBind()
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub lnkDetail_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As New LinkButton 'LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"Code", "Bank", "PayGrp", "FileType", "FileName"})
        ViewState("Code") = obj(0)
        ViewState("FileType") = obj(3)
        ViewState("FileName") = obj(4)

        Select Case ViewState("FileType")
            Case "txt"
                GenerateBankTxt(ViewState("FileName"), ViewState("Code"), ViewState("FileType"))
            Case "csv"
                GenerateBankTxt(ViewState("FileName"), ViewState("Code"), ViewState("FileType"))
            Case "xls"
            Case Else
                MessageBox.Information("Bank File not yet set-up", Me)
        End Select
    End Sub

    Protected Sub lnk_View(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnk As LinkButton = TryCast(sender, LinkButton)
        Dim NewScriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        NewScriptManager.RegisterPostBackControl(lnk)
    End Sub
    Private Sub GenerateBankTxt(ByVal bank As String, ByVal bankno As Integer, ByVal filetype As String)

        Dim FileHolder As FileInfo
        Dim WriteFile As StreamWriter
        Dim path As String = Page.MapPath("documents")
        Dim compno As Integer = cboCompNo.SelectedValue
        Dim grpno As Integer = cboGrpNo.SelectedValue
        Dim paydate As String = txtPayDate.Text
        Dim creditdate As String = txtCreditDate.Text
        Dim filename As String = path & "\" & bank & "_" & Replace(paydate, "/", "") + "." + filetype

        If Not IO.Directory.Exists(path) Then
            IO.Directory.CreateDirectory(path)
        End If
        FileHolder = New FileInfo(filename)
        WriteFile = FileHolder.CreateText()

        Dim dstext As DataSet, text As String
        dstext = SQLHelper.ExecuteDataSet("pr_EBankRemit_Web", UserNo, PayLocNo, compno, grpno, paydate, creditdate, bankno)
        If dstext.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To dstext.Tables(0).Rows.Count - 1
                Select Case filetype
                    Case "txt"
                        text = Generic.CheckDBNull(dstext.Tables(0).Rows(i)("dtl"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        WriteFile.WriteLine(text)
                    Case "csv"
                        text = Generic.CheckDBNull(dstext.Tables(0).Rows(i)("dtl"), Global.clsBase.clsBaseLibrary.enumObjectType.StrType)
                        WriteFile.WriteLine(text)
                    Case "xls"
                End Select
            Next
        End If
        WriteFile.Close()
        dstext = Nothing
        DownloadFile("../Secured/documents/" & bank & "_" & Replace(paydate, "/", "") + "." + filetype)
    End Sub

    Private Sub DownloadFile(ByVal fullpath As String)
        Dim FileName As String = IO.Path.GetFileName(fullpath)
        Dim filePath As String = Server.MapPath(String.Format("~/Secured/documents/{0}", FileName))
        Response.Clear()
        Response.ClearContent()
        Response.ContentType = "application/octet-stream"
        Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
        Response.TransmitFile(fullpath)
        Response.End()
    End Sub
    Protected Sub lnkSearchGrp_Click(ByVal sender As Object, ByVal e As EventArgs)
        PopulateDropDown(cboGrpNo, 1, cboCompNo.SelectedValue)
    End Sub

    Protected Sub lnkView_Click(ByVal sender As Object, ByVal e As EventArgs)
        If cboCompNo.SelectedValue = "0" Or cboGrpNo.SelectedValue = "0" Or txtCreditDate.Text = "" Or txtPayDate.Text = "" Then
            MessageBox.Information("Incomplete inputs", Me)
        Else
            PopulateGrid()
        End If
    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "pnlPopupDetl")
            Generic.EnableControls(Me, "pnlPopupDetl", True)
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If

        Try

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkEditDetl_Click(ByVal sender As Object, ByVal e As EventArgs)

        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            'PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"RecNo"})))
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If

        Try
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
        If cboCompNo.SelectedValue = 0 Or cboGrpNo.SelectedValue = 0 Or txtCreditDate.Text = "" Or txtPayDate.Text = "" Then
            MessageBox.Information(MessageTemplate.DeniedView, Me)
        Else
            PopulateGrid()
        End If
    End Sub

    Protected Sub lnkExport_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub lnkUpload_Click(ByVal sender As Object, ByVal e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel3")
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub


#Region "********Detail Check All********"

#End Region

    Protected Sub ASPxGridViewExporter_RenderBrick(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewExportRenderingEventArgs) Handles grdExport.RenderBrick
        Dim dataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
        If e.RowType = GridViewRowType.Header AndAlso dataColumn IsNot Nothing Then
            e.Text = e.Text.Replace("<br/>", " ")
            e.Text = e.Text.Replace("<br />", " ")
            e.Text = e.Text.Replace("<br>", " ")
            e.Text = e.Text.Replace("<center>", "")
            e.Text = e.Text.Replace("</center>", "")
        End If

    End Sub

End Class


Partial Class CSVFormat

    Private _idnumber As String
    Public Property idnumber As String
        Get
            Return _idnumber
        End Get
        Set(ByVal value As String)
            _idnumber = value
        End Set
    End Property
End Class




