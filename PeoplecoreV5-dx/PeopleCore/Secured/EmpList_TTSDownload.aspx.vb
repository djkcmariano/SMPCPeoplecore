Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.IO
Imports clsLib

Partial Class Secured_EmpList_TTSDownload
    Inherits System.Web.UI.Page

    Dim userNo As Integer = 0
    Dim payLocNo As Integer = 0



    Private Sub Generate201()
        Try
            Dim FileHolder As FileInfo
            Dim WriteFile As StreamWriter
            Dim tpath As String = Page.MapPath("documents") '"c:\Payroll Diskette"
            Dim xFilename As String = ""

            xFilename = tpath & "\201_TTS.txt"
            FileHolder = New FileInfo(xFilename)
            WriteFile = FileHolder.CreateText()

            Dim fBranchCode As String = ""
            Dim dsCode As DataSet = SQLHelper.ExecuteDataSet("ESection_WebOne", userNo, Generic.ToInt(cboSectionNo.SelectedValue), payLocNo)
            If dsCode.Tables.Count > 0 Then
                If dsCode.Tables(0).Rows.Count > 0 Then
                    fBranchCode = Generic.ToStr(dsCode.Tables(0).Rows(0)("SectionCode"))
                End If
            End If
            dsCode = Nothing
            Dim ds As New DataSet, employeeno As Integer = 0, firstname As String = "", lastname As String = "", middlename As String = "", employeecode As String = ""
            ds = SQLHelper.ExecuteDataSet("EEmployee_TTS_Download", fBranchcode)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        employeeno = Generic.ToInt(ds.Tables(0).Rows(i)("employeeno"))
                        firstname = Generic.ToStr(ds.Tables(0).Rows(i)("firstname"))
                        lastname = Generic.ToStr(ds.Tables(0).Rows(i)("lastname"))
                        middlename = Generic.ToStr(ds.Tables(0).Rows(i)("mi"))
                        employeecode = Generic.ToStr(ds.Tables(0).Rows(i)("employeecode"))

                        WriteFile.WriteLine(employeeno.ToString & "," & lastname.ToString & "," & firstname.ToString & "," & middlename.ToString & "," & employeecode.ToString)
                    Next
                End If
            End If

            ds = Nothing

            WriteFile.Close()
            OpenText("../secured/documents/201_TTS.txt")

        Catch ex As Exception

        End Try

    End Sub
    'Submit record
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Generate201()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        userNo = Generic.ToInt(Session("OnlineUserNo"))
        payLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(userNo)
        If Not IsPostBack Then
            PopulateDropDown()
        End If
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Private Sub PopulateDropDown()
        Try
            Generic.PopulateDropDownList(userNo, Me, "Panel1", payLocNo)
        Catch ex As Exception
        End Try

    End Sub

    Private Sub OpenText(ByVal fullpath As String)
        Dim FileName As String = ""
        FileName = IO.Path.GetFileName(fullpath)
        Response.Clear()
        Response.ClearContent()
        Response.ContentType = "application/octet-stream"
        Response.AppendHeader("Content-Disposition", "attachment;filename=""" & FileName & """")
        Response.TransmitFile(fullpath)
        Response.End()
    End Sub
End Class

