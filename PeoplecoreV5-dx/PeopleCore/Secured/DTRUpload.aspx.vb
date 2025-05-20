Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.IO
Imports clsLib

Partial Class Secured_DTRUpload
    Inherits System.Web.UI.Page
    Dim userNo As Integer = 0
    Dim payLocNo As Integer = 0



    Private Sub PopulateDropDown()
        Try
            Generic.PopulateDropDownList(userNo, Me, "Panel1", payLocNo)
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
         UserNo = Generic.ToInt(Session("OnlineUserNo"))        
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            PopulateDropDown()
        End If

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Function PopulateData_Bio_Raw() As Boolean
        Dim tsuccess As Integer = 0
        Try
            Dim lastname As String = ""
            Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False, fFilename As String = ""
            Dim tpath As String = ""
            If txtFile.HasFile = True Then
                tFilepath = txtFile.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                Dim fileext As String = IO.Path.GetExtension(tFilepath)
                tProceed = True
                tpath = (Server.MapPath("Documents"))
                fFilename = tpath & "\" & Now.Month.ToString & Now.Day.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & "_" & tfilename
                txtFile.SaveAs(fFilename)
            End If

            Dim amount As Double = 0, employeecode As String = ""

            If tProceed Then

                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0, fpid As Integer, dtrdate As String, ldtrdate As String, dtrtime As String, logtype As String = "", fpmachinecode As String = "", dwInOutMode As Long
                Dim fs As FileStream
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1
                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, Chr(9))
                    fpid = LTrim(RTrim(fspecArr(0)))
                    ldtrdate = fspecArr(1)
                    dtrdate = FormatDateTime(fspecArr(1), DateFormat.ShortDate)
                    dtrtime = Replace(FormatDateTime(fspecArr(1), DateFormat.ShortTime), ":", "")

                    fpmachinecode = fspecArr(2)
                    logtype = fspecArr(3)
                    dwInOutMode = fspecArr(3)
                    If logtype = "1" Then
                        logtype = "o"
                    ElseIf logtype = "0" Then
                        logtype = "i"
                    ElseIf logtype = "2" Then
                        logtype = "o"
                    ElseIf logtype = "3" Then
                        logtype = "i"
                    End If
                    If logtype = "o" Or logtype = "i" Then
                        If SQLHelper.ExecuteNonQuery("EFPDTR_BIO_SPUpdate_Manual", fpid, dtrdate, dtrtime, logtype, 0, ldtrdate, Me.cboFPMachineNo.SelectedValue, dwInOutMode, Session("xPayLocNo")) > 0 Then
                            tsuccess = tsuccess + 1
                        End If
                    End If

                End While
                d.Close()
                PopulateData_Bio_Raw = True
            End If
        Catch ex As Exception
            PopulateData_Bio_Raw = False
        End Try
        'sqlhelp.ExecuteNonQuery(clsConnectionString.xConSTR, "EGetFPLog")
    End Function

    Private Function PopulateDAta_Bio_Peoplecore_Decrypt() As Boolean
        Dim tsuccess As Integer = 0
        Try
            Dim lastname As String = ""
            Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False, fFilename As String = ""
            Dim tpath As String = ""
            If txtFile.HasFile = True Then
                tFilepath = txtFile.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                Dim fileext As String = IO.Path.GetExtension(tFilepath)
                tProceed = True
                tpath = (Server.MapPath("Documents"))
                fFilename = tpath & "\" & Now.Month.ToString & Now.Day.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & "_" & tfilename
                txtFile.SaveAs(fFilename)
            End If

            Dim amount As Double = 0, employeecode As String = ""

            If tProceed Then

                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer, fpid As Integer, dtrdate As String, ldtrdate As String, dtrtime As String, logtype As String = "", fpmachinecode As String = "", dwInOutMode As Long
                Dim fs As FileStream
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1

                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, ",")
                    fpid = PeopleCoreCrypt.DecryptLogs(LTrim(RTrim(fspecArr(0))))
                    dtrdate = PeopleCoreCrypt.DecryptLogs(fspecArr(1))
                    dtrtime = PeopleCoreCrypt.DecryptLogs(fspecArr(2))
                    logtype = PeopleCoreCrypt.DecryptLogs(fspecArr(3))
                    fpmachinecode = PeopleCoreCrypt.DecryptLogs(fspecArr(4))
                    dwInOutMode = logtype

                    If logtype = "1" Then
                        logtype = "o"
                    ElseIf logtype = "0" Then
                        logtype = "i"
                    ElseIf logtype = "2" Then
                        logtype = "o"
                    ElseIf logtype = "3" Then
                        logtype = "i"
                    End If

                    If logtype = "o" Or logtype = "i" Or logtype = "2" Or logtype = "3" Then
                        If SQLHelper.ExecuteNonQuery("EFPDTR_BIO_SPUpdate_Manual", fpid, dtrdate, dtrtime, logtype, 0, dtrdate, fpmachinecode, dwInOutMode, Session("xPayLocNo")) > 0 Then
                            tsuccess = tsuccess + 1
                        End If
                    End If

                End While
                d.Close()
                PopulateDAta_Bio_Peoplecore_Decrypt = True
            End If
        Catch ex As Exception
            PopulateDAta_Bio_Peoplecore_Decrypt = False
        End Try
    End Function
    Private Function PopulateDAta_TTS_Decrypt() As Boolean
        Dim tsuccess As Integer = 0
        Try
            Dim lastname As String = ""
            Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False, fFilename As String = ""
            Dim tpath As String = ""
            If txtFile.HasFile = True Then
                tFilepath = txtFile.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                Dim fileext As String = IO.Path.GetExtension(tFilepath)
                tProceed = True
                tpath = (Server.MapPath("Documents"))
                fFilename = tpath & "\" & Now.Month.ToString & Now.Day.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & "_" & tfilename
                txtFile.SaveAs(fFilename)
            End If

            Dim amount As Double = 0, employeecode As String = ""

            If tProceed Then

                Dim fspecArr() As String, nfile As String = "", branchcode As String = ""
                Dim i As Integer, fpid As Integer, dtrdate As String, ldtrdate As String, dtrtime As String, logtype As String = "", fpmachinecode As String = "", dwInOutMode As Long
                Dim fs As FileStream
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1

                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, ",")
                    employeecode = PeopleCoreCrypt.DecryptLogs(LTrim(RTrim(fspecArr(0))))
                    dtrdate = PeopleCoreCrypt.DecryptLogs(fspecArr(1))
                    dtrtime = PeopleCoreCrypt.DecryptLogs(fspecArr(2))
                    logtype = PeopleCoreCrypt.DecryptLogs(fspecArr(3))
                    branchcode = PeopleCoreCrypt.DecryptLogs(fspecArr(4))

                    If logtype = "o" Or logtype = "i" Then
                        If SQLHelper.ExecuteNonQuery("ETimeLog_TTS_SPUpdate_FromText", employeecode, logtype, dtrtime, dtrdate, dtrtime, dtrdate, branchcode, Session("xPayLocNo")) > 0 Then
                            tsuccess = tsuccess + 1
                        End If
                    End If

                End While
                d.Close()
                PopulateDAta_TTS_Decrypt = True
            End If
        Catch ex As Exception
            PopulateDAta_TTS_Decrypt = False
        End Try
    End Function

    Private Function PopulateData_Commissary() As Boolean
        Dim tsuccess As Integer = 0
        Try
            Dim lastname As String = ""
            Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False, fFilename As String = ""
            Dim tpath As String = ""
            If txtFile.HasFile = True Then
                tFilepath = txtFile.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                Dim fileext As String = IO.Path.GetExtension(tFilepath)
                tProceed = True
                tpath = (Server.MapPath("Documents"))
                fFilename = tpath & "\" & Now.Month.ToString & Now.Day.ToString & Now.Hour.ToString & Now.Minute.ToString & Now.Second.ToString & "_" & tfilename
                txtFile.SaveAs(fFilename)
            End If

            Dim amount As Double = 0, employeecode As String = ""

            If tProceed Then

                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer, fpid As String, dtrdate As String, ldtrdate As String, dtrtime As String, logtype As String = "", fpmachinecode As String = "", dwInOutMode As Long
                Dim fs As FileStream
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1
                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, Chr(9))
                    fpid = Left(nfile, 10)
                    fpid = LTrim(RTrim(fpid))
                    ldtrdate = nfile.Substring(14, 2) & "/" & nfile.Substring(16, 2) & "/" & nfile.Substring(10, 4)
                    dtrdate = FormatDateTime(ldtrdate, DateFormat.ShortDate)
                    dtrtime = nfile.Substring(18, 4)

                    fpmachinecode = 119
                    logtype = nfile.Substring(22, 1)

                    If logtype = "A" Then
                        logtype = "i"
                        dwInOutMode = 0
                    ElseIf logtype = "Z" Then
                        logtype = "o"
                        dwInOutMode = 1
                    ElseIf logtype = "B" Then
                        logtype = "o"
                        dwInOutMode = 2
                    ElseIf logtype = "C" Then
                        logtype = "i"
                        dwInOutMode = 3
                    ElseIf logtype = "D" Then
                        logtype = "o"
                        dwInOutMode = 2
                    ElseIf logtype = "E" Then
                        logtype = "i"
                        dwInOutMode = 3
                    End If
                    If logtype = "o" Or logtype = "i" Then
                        If SQLHelper.ExecuteNonQuery("EFPDTR_BIO_SPUpdate_ManualComm", fpid, dtrdate, dtrtime, logtype, 0, ldtrdate, Me.cboFPMachineNo.SelectedValue, dwInOutMode, Session("xPayLocNo")) > 0 Then
                            tsuccess = tsuccess + 1
                        End If
                    End If

                End While
                d.Close()
                PopulateData_Commissary = True
            End If
        Catch ex As Exception
            PopulateData_Commissary = False
        End Try
        'sqlhelp.ExecuteNonQuery(clsConnectionString.xConSTR, "EGetFPLog")
    End Function

    'Submit record
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.cboDatasourceNo.SelectedValue = 1 Then
            If PopulateData_Bio_Raw() Then
                Dim url As String = "DTRUpload.aspx"
                SQLHelper.ExecuteNonQuery("EDTR_WebProces_FPDTR_Forward_DTRSwipe", 0)
                MessageBox.SuccessResponse("Uploading of file successfully done.", Me, url)
            Else
                MessageBox.Critical("Error uploading.", Me)
            End If
        ElseIf Me.cboDatasourceNo.SelectedValue = 2 Then
            If PopulateDAta_Bio_Peoplecore_Decrypt() Then
                Dim url As String = "DTRUpload.aspx"
                SQLHelper.ExecuteNonQuery("EDTR_WebProces_FPDTR_Forward_DTRSwipe", 0)
                MessageBox.SuccessResponse("Uploading of file successfully done.", Me, url)
            Else
                MessageBox.Critical("Error uploading.", Me)
            End If
        ElseIf Me.cboDatasourceNo.SelectedValue = 3 Then
            If PopulateData_Commissary() Then
                Dim url As String = "DTRUpload.aspx"
                SQLHelper.ExecuteNonQuery("EDTR_WebProces_FPDTR_Forward_DTRSwipe", 0)
                MessageBox.SuccessResponse("Uploading of file successfully done.", Me, url)
            Else
                MessageBox.Critical("Error uploading.", Me)
            End If
        ElseIf Me.cboDatasourceNo.SelectedValue = 5 Then
            If PopulateDAta_TTS_Decrypt() Then
                Dim url As String = "DTRUpload.aspx"
                SQLHelper.ExecuteNonQuery("EDTR_WebProces_FPDTR_Forward_DTRSwipe", 0)
                MessageBox.SuccessResponse("Uploading of file successfully done.", Me, url)
            Else
                MessageBox.Critical("Error uploading.", Me)
            End If
        End If
        

    End Sub

    
    Private Sub fRegisterStartupScript(key As String, script As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), key, script, True)
    End Sub
End Class
