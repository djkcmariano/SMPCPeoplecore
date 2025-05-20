Imports clsLib
Imports System.Data

Partial Class SecuredApp_frmFileUpload
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            Dim sortDirection As String = "", sortExpression As String = ""
            dt = SQLHelper.ExecuteDataTable("EDoc_Web", UserNo, UserNo, Filter1.SearchText, Generic.ToStr(Session("xMenuType")))
            Dim dv As DataView = dt.DefaultView
            If ViewState("SortDirection") IsNot Nothing Then
                sortDirection = ViewState("SortDirection").ToString()
            End If
            If ViewState("SortExpression") IsNot Nothing Then
                sortExpression = ViewState("SortExpression").ToString()
                dv.Sort = String.Concat(sortExpression, " ", sortDirection)
            End If
            grdMain.DataSource = dv
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EDoc_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdMain_Sorting(sender As Object, e As GridViewSortEventArgs)
        Try
            If ViewState("SortDirection") Is Nothing OrElse ViewState("SortExpression").ToString() <> e.SortExpression Then
                ViewState("SortDirection") = "ASC"
            ElseIf ViewState("SortDirection").ToString() = "ASC" Then
                ViewState("SortDirection") = "DESC"
            ElseIf ViewState("SortDirection").ToString() = "DESC" Then
                ViewState("SortDirection") = "ASC"
            End If
            ViewState("SortExpression") = e.SortExpression
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdMain_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        Try
            grdMain.PageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineApplicantNo"))
        lblTrans.Text = Generic.ToStr(Request.QueryString("display"))
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateGrid()
        End If
        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "Panel1")
        ModalPopupExtender1.Show()        
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs)
        Dim chk As New CheckBox, lnk As New LinkButton, Count As Integer = 0
        For i As Integer = 0 To Me.grdMain.Rows.Count - 1
            chk = CType(grdMain.Rows(i).FindControl("chk"), CheckBox)
            lnk = CType(grdMain.Rows(i).FindControl("lnkDownload"), LinkButton)
            If chk.Checked = True Then
                Dim path As String = DeleteFile(Generic.ToInt(lnk.CommandArgument))
                Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
                If file.Exists Then
                    file.Delete()
                End If
                Generic.DeleteRecordAudit("EDoc", UserNo, Generic.ToInt(lnk.CommandArgument))
                Count = Count + 1
            End If
        Next
        MessageBox.Success("There are (" + Count.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
    End Sub

    Private Function SaveRecord() As Boolean
        Dim retval As Boolean = False
        If fuDoc.HasFile Then
            Dim Filename As String, FileExt As String, FileSize As Int64, ActualPath As String
            Dim _ds As New DataSet, NewFileName As String
            Try
                Filename = IO.Path.GetFileName(fuDoc.PostedFile.FileName)
                FileExt = IO.Path.GetExtension(fuDoc.PostedFile.FileName)
                Dim f As New System.IO.FileInfo(fuDoc.PostedFile.FileName)
                FileSize = f.Length
                NewFileName = Guid.NewGuid().ToString()
                ActualPath = Server.MapPath("../") & "secured\documents\" & NewFileName & FileExt
                fuDoc.SaveAs(ActualPath)
                If SQLHelper.ExecuteNonQuery("EDoc_WebSave", UserNo, UserNo, Generic.ToInt(txtCode.Text), txtDocDesc.Text, Filename, FileExt, NewFileName & FileExt, FileSize, ActualPath, Generic.ToStr(Session("xMenuType"))) > 0 Then
                    retval = True
                End If
            Catch ex As Exception

            End Try
        End If
        Return retval
    End Function


    Protected Sub lnkDownload_Click(sender As Object, e As EventArgs)
        Try
            Dim lnk As New LinkButton
            Dim doc As Byte() = Nothing
            Dim filename As String = ""
            Dim orgname As String = ""
            Dim dt As DataTable
            lnk = sender
            dt = SQLHelper.ExecuteDataTable("EDoc_WebOne", UserNo, Generic.ToInt(lnk.CommandArgument))
            For Each row As DataRow In dt.Rows
                filename = Generic.ToStr(row("ActualFileName"))
                orgname = Generic.ToStr(row("DocFile"))
            Next

            Dim path As String = Server.MapPath("~/secured/documents/") & filename
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            If file.Exists Then
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & orgname)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
                Response.End()
            Else
                MessageBox.Warning("This file does not exist.", Me)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdMain_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdMain.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk As New LinkButton()
            lnk = DirectCast(e.Row.FindControl("lnkDownload"), LinkButton)
            'RegisterAsyncPostBackControl(ib);
            ScriptManager.GetCurrent(Me).RegisterPostBackControl(lnk)
        End If
    End Sub

    Private Function DeleteFile(DocNo As Integer) As String
        Dim filename As String
        filename = Generic.ToStr(SQLHelper.ExecuteScalar("SELECT ActualFileName FROM EDoc WHERE DocNo=" & DocNo.ToString()))
        Return Server.MapPath("~/secured/documents/") & filename
    End Function
End Class
