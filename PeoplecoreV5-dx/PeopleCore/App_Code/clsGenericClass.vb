Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports clsLib
Imports System.IO


Public Class clsGenericClass

    Dim clsBase As New clsBase.clsBaseLibrary
    Public Shared DocPath As String = ""

#Region "*************** Inquiry *****************"
    Public Function Inq_FileDoc(ByVal onlineuserno As Integer, ByVal tno As Integer) As DataSet
        Return SQLHelper.ExecuteDataSet("EFileDocWeb_Inq", onlineuserno, tno)
    End Function
#End Region

#Region "*************** List *****************"
    Public Function List_FileDoc(ByVal onlineuserno As Integer, ByVal tno As Integer) As DataSet
        Return SQLHelper.ExecuteDataSet("EFileDocWeb_List", onlineuserno, tno)
    End Function
#End Region

#Region "*************** Save *****************"
    Public Function Save_FileDoc(ByVal onlineuserno As Integer, ByVal filedocno As Integer, ByVal fileno As Integer, ByVal filename As String, ByVal filetypeno As Integer, ByVal filedesc As String, ByVal menutype As String, ByVal actualfilename As String, ByVal menumassno As Integer, ByVal fileext As String) As DataSet
        Return SQLHelper.ExecuteDataSet("EFileDocWebApl", onlineuserno, filedocno, fileno, filename, filetypeno, filedesc, menutype, actualfilename, menumassno, fileext)
    End Function
#End Region



    Public Shared Function TimeFormat(ByVal xtime As String) As String
        Try
            Dim fromtime As String = "0000"
            Dim toTime As String = "4800"
            Dim fromminute As String = "00"
            Dim tominute As String = "59"
            Dim tProceed As String = ""

            Dim ftime As String = Replace(xtime, ":", "")
            Dim fminute As String = Right(ftime, 2)

            If (fminute < fromminute And fminute <> "") Or (fminute > tominute And fminute <> "") Then
                tProceed = "Invalid use of minutes Format ( " & fromminute & " - " & tominute & "). "
            ElseIf (ftime < fromtime And ftime <> "") Or (ftime > toTime And ftime <> "") Then
                tProceed = "Invalid Time Format ( " & fromtime & " - " & toTime & "). "
            Else
                tProceed = ""
            End If

            Return tProceed

        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Public Shared Function DateFormat(ByVal StrDate As String) As String
        Try
            Dim ReturnValue As String = ""

            If IsDate(CType(StrDate, Date)) Then
                If Year(StrDate) < 1900 Then
                    ReturnValue = "Minimum year value 1900"
                Else
                    ReturnValue = ""
                End If
            End If
            Return ReturnValue
        Catch ex As Exception
            Return "Error"
        End Try

    End Function

    Public Sub YesNo(ByVal Result As Boolean, ByVal opt1 As RadioButton, ByVal opt2 As RadioButton)
        If Result = True Then
            opt1.Checked = 1
        Else
            opt2.Checked = 1
        End If
    End Sub

#Region "*************** Log-out *****************"
    Public Sub xlogOut(ByVal onlineuserno As Integer)
        SQLHelper.ExecuteNonQuery("SUserWeb_Logout", onlineuserno)
    End Sub
#End Region

    Public Function populateDropdownFilterByAll(ByVal fId As Integer, ByVal onlineuserno As Integer, Optional ByVal fpayLocNo As Integer = 0) As DataSet
        Dim ds As New DataSet
        If fId = 1 Then
            ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup", onlineuserno, fpayLocNo) '  xLookup_Table( "EEmployeeL")
        ElseIf fId = 2 Then
            ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup", onlineuserno, fpayLocNo) 'xLookup_Table( "EEmployeeN")

        ElseIf fId = 3 Then
            ds = xLookup_Table(onlineuserno, "EEmployeeClass", fpayLocNo)

        ElseIf fId = 4 Then
            ds = xLookup_Table(onlineuserno, "EDivision", fpayLocNo)

        ElseIf fId = 5 Then
            ds = xLookup_Table(onlineuserno, "EDepartment", fpayLocNo)

        ElseIf fId = 6 Then
            ds = xLookup_Table(onlineuserno, "ESection", fpayLocNo)

        ElseIf fId = 7 Then
            ds = xLookup_Table(onlineuserno, "EEmployeestat", fpayLocNo)

        ElseIf fId = 8 Then
            ds = xLookup_Table(onlineuserno, "ELocation", fpayLocNo)

        ElseIf fId = 9 Then
            ds = xLookup_Table(onlineuserno, "EPosition", fpayLocNo)

        ElseIf fId = 10 Then
            ds = xLookup_Table(onlineuserno, "EFacility", fpayLocNo)

        ElseIf fId = 11 Then
            ds = xLookup_Table(onlineuserno, "EGroup", fpayLocNo)

        ElseIf fId = 12 Then
            ds = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", onlineuserno, fpayLocNo) 'xLookup_Table( "EPayClass")

        ElseIf fId = 13 Then
            ds = xLookup_Table(onlineuserno, "EUnit", fpayLocNo)

        ElseIf fId = 14 Then
            ds = xLookup_Table(onlineuserno, "ETask", fpayLocNo)

        ElseIf fId = 17 Then
            ds = xLookup_Table(onlineuserno, "EProject", fpayLocNo)

        ElseIf fId = 18 Then
            ds = xLookup_Table(onlineuserno, "ECostCenter", fpayLocNo)

        ElseIf fId = 19 Then
            ds = xLookup_Table(onlineuserno, "EJobGrade", fpayLocNo)

        ElseIf fId = 20 Then
            ds = xLookup_Table(onlineuserno, "ERank", fpayLocNo)

        End If

        Return ds
    End Function
    Public Function populateDropdownFilterByCate(ByVal fId As Integer, ByVal onlineuserno As Integer, Optional ByVal fpayLocNo As Integer = 0, Optional ByVal category As String = "") As DataSet
        Dim ds As New DataSet
        If fId = 1 Then
            ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookupAll", onlineuserno, fpayLocNo, category) '  xLookup_Table( "EEmployeeL")
        ElseIf fId = 2 Then
            ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookupAll", onlineuserno, fpayLocNo, category) 'xLookup_Table( "EEmployeeN")

        ElseIf fId = 3 Then
            ds = xLookup_Table(onlineuserno, "EEmployeeClass", fpayLocNo)

        ElseIf fId = 4 Then
            ds = xLookup_Table(onlineuserno, "EDivision", fpayLocNo)

        ElseIf fId = 5 Then
            ds = xLookup_Table(onlineuserno, "EDepartment", fpayLocNo)

        ElseIf fId = 6 Then
            ds = xLookup_Table(onlineuserno, "ESection", fpayLocNo)

        ElseIf fId = 7 Then
            ds = xLookup_Table(onlineuserno, "EEmployeestat", fpayLocNo)

        ElseIf fId = 8 Then
            ds = xLookup_Table(onlineuserno, "ELocation", fpayLocNo)

        ElseIf fId = 9 Then
            ds = xLookup_Table(onlineuserno, "EPosition", fpayLocNo)

        ElseIf fId = 10 Then
            ds = xLookup_Table(onlineuserno, "EFacility", fpayLocNo)

        ElseIf fId = 11 Then
            ds = xLookup_Table(onlineuserno, "EGroup", fpayLocNo)

        ElseIf fId = 12 Then
            ds = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", onlineuserno, fpayLocNo) 'xLookup_Table( "EPayClass")

        ElseIf fId = 13 Then
            ds = xLookup_Table(onlineuserno, "EUnit", fpayLocNo)

        ElseIf fId = 14 Then
            ds = xLookup_Table(onlineuserno, "ETask", fpayLocNo)

        ElseIf fId = 17 Then
            ds = xLookup_Table(onlineuserno, "EProject", fpayLocNo)

        ElseIf fId = 18 Then
            ds = xLookup_Table(onlineuserno, "ECostCenter", fpayLocNo)

        ElseIf fId = 19 Then
            ds = xLookup_Table(onlineuserno, "EJobGrade", fpayLocNo)

        ElseIf fId = 20 Then
            ds = xLookup_Table(onlineuserno, "ERank", fpayLocNo)

        End If

        Return ds
    End Function

    Public Function populateDropdownFilterByShift(ByVal fId As Integer, ByVal onlineuserno As Integer, Optional ByVal fPayLocNo As Integer = 0) As DataSet
        Dim ds As New DataSet

        If fId = 1 Then
            ds = xLookup_Table(onlineuserno, "EEmployeeClass", fPayLocNo)

        ElseIf fId = 2 Then
            ds = xLookup_Table(onlineuserno, "EDivision", fPayLocNo)

        ElseIf fId = 3 Then
            ds = xLookup_Table(onlineuserno, "EDepartment", fPayLocNo)

        ElseIf fId = 4 Then
            ds = xLookup_Table(onlineuserno, "ESection", fPayLocNo)

        ElseIf fId = 5 Then
            ds = xLookup_Table(onlineuserno, "EEmployeestat", fPayLocNo)

        ElseIf fId = 6 Then
            ds = xLookup_Table(onlineuserno, "EPosition", fPayLocNo)

        ElseIf fId = 7 Then
            ds = SQLHelper.ExecuteDataSet("EPayClass_WebLookup", onlineuserno, fPayLocNo) 'xLookup_Table( "EPayClass")

        ElseIf fId = 8 Then
            ds = xLookup_Table(onlineuserno, "EShift", fPayLocNo)

        End If
        Return ds
    End Function
    Public Function populateDropdownFilterByApplicant(ByVal fId As Integer, ByVal onlineuserno As Integer, Optional ByVal fPayLocNo As Integer = 0) As DataSet
        Dim ds As New DataSet

        If fId = 1 Then
            ds = xLookup_Table(onlineuserno, "EApplicantN")

        ElseIf fId = 2 Then
            ds = xLookup_Table(onlineuserno, "EApplicantV")

        ElseIf fId = 3 Then
            ds = xLookup_Table(onlineuserno, "EPositionApp")

        ElseIf fId = 4 Then
            ds = xLookup_Table(onlineuserno, "EVacancySource")

        ElseIf fId = 5 Then
            ds = xLookup_Table(onlineuserno, "EApplicantCate")

        ElseIf fId = 6 Then
            ds = xLookup_Table(onlineuserno, "EMRV")

        ElseIf fId = 7 Then
            ds = xLookup_Table(onlineuserno, "EApplicantStat") 'xLookup_Table( "EPayClass")

        ElseIf fId = 8 Then
            ds = xLookup_Table(onlineuserno, "EApplicantSource")

        End If
        Return ds
    End Function

    Public Function xLookup_Table_Self(ByVal onlineuserno As Integer, ByVal Tablename As String, Optional ByVal fpayLocNo As Integer = 0, Optional ByVal sortColumn As String = "", Optional ByVal sortDirection As String = "") As DataSet
        Return SQLHelper.ExecuteDataSet("xTable_Lookup_Self", onlineuserno, Tablename, fpayLocNo, clsBase.CheckDBNull(sortColumn, Global.clsBase.clsBaseLibrary.enumObjectType.StrType), clsBase.CheckDBNull(sortDirection, Global.clsBase.clsBaseLibrary.enumObjectType.StrType))
    End Function
    Public Function xLookup_Table_Self_All(ByVal onlineuserno As Integer, ByVal Tablename As String, Optional ByVal fpayLocNo As Integer = 0, Optional ByVal sortColumn As String = "", Optional ByVal sortDirection As String = "") As DataSet
        Return SQLHelper.ExecuteDataSet("xTable_Lookup_Self_All", onlineuserno, Tablename, fpayLocNo, clsBase.CheckDBNull(sortColumn, Global.clsBase.clsBaseLibrary.enumObjectType.StrType), clsBase.CheckDBNull(sortDirection, Global.clsBase.clsBaseLibrary.enumObjectType.StrType))
    End Function

    Public Function xLookup_Table_Applicant(ByVal onlineuserno As Integer, ByVal Tablename As String, Optional ByVal fpayLocNo As Integer = 0, Optional ByVal sortColumn As String = "", Optional ByVal sortDirection As String = "") As DataSet
        Return SQLHelper.ExecuteDataSet("xTable_Lookup_Applicant", onlineuserno, Tablename, fpayLocNo, clsBase.CheckDBNull(sortColumn, Global.clsBase.clsBaseLibrary.enumObjectType.StrType), clsBase.CheckDBNull(sortDirection, Global.clsBase.clsBaseLibrary.enumObjectType.StrType))
    End Function
    Public Overloads Function xLookup_Table(ByVal onlineuserno As Integer, ByVal Tablename As String, Optional ByVal fpayLocNo As Integer = 0, Optional ByVal sortColumn As String = "", Optional ByVal sortDirection As String = "") As DataSet

        Return SQLHelper.ExecuteDataSet("xTable_Lookup", onlineuserno, Tablename, fpayLocNo, clsBase.CheckDBNull(sortColumn, Global.clsBase.clsBaseLibrary.enumObjectType.StrType), clsBase.CheckDBNull(sortDirection, Global.clsBase.clsBaseLibrary.enumObjectType.StrType))

    End Function
    Public Overloads Function xLookup_Table_All(ByVal onlineuserno As Integer, ByVal Tablename As String, Optional ByVal fpayLocNo As Integer = 0, Optional ByVal sortColumn As String = "", Optional ByVal sortDirection As String = "") As DataSet
        Return SQLHelper.ExecuteDataSet("xTable_Lookup_All", onlineuserno, Tablename, fpayLocNo, clsBase.CheckDBNull(sortColumn, Global.clsBase.clsBaseLibrary.enumObjectType.StrType), clsBase.CheckDBNull(sortDirection, Global.clsBase.clsBaseLibrary.enumObjectType.StrType))

    End Function
    Public Overloads Function xLookup_Table_One(ByVal onlineuserno As Integer, ByVal Tablename As String, ByVal tNo As Integer) As DataSet
        Return SQLHelper.ExecuteDataSet("xTable_Lookup_One", onlineuserno, Tablename, tNo)

    End Function
    Public Function populateDropdownFilterByAll_AutoComplete(ByVal fId As Integer, ByVal onlineuserno As Integer, ByVal fpayLocNo As Integer, ByVal prefixText As String, ByVal count As Integer) As DataSet
        Dim ds As New DataSet

        If fId = 1 Then
            ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AutoComplete", onlineuserno, prefixText, 1) '  xLookup_Table( "EEmployeeL")
        ElseIf fId = 2 Then
            ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AutoComplete", onlineuserno, prefixText, 1) 'xLookup_Table( "EEmployeeN")

        ElseIf fId = 3 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "EEmployeeClass", fpayLocNo, prefixText)

        ElseIf fId = 4 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "EDivision", fpayLocNo, prefixText)

        ElseIf fId = 5 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "EDepartment", fpayLocNo, prefixText)

        ElseIf fId = 6 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "ESection", fpayLocNo, prefixText)

        ElseIf fId = 7 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "EEmployeestat", fpayLocNo, prefixText)

        ElseIf fId = 8 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "ELocation", fpayLocNo, prefixText)

        ElseIf fId = 9 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "EPosition", fpayLocNo, prefixText)

        ElseIf fId = 10 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "EFacility", fpayLocNo, prefixText)

        ElseIf fId = 11 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "EGroup", fpayLocNo, prefixText)

        ElseIf fId = 12 Then
            ds = SQLHelper.ExecuteDataSet("EPayClass_WebLookup_AutoComplete", onlineuserno, fpayLocNo, prefixText) 'xLookup_Table( "EPayClass")

        ElseIf fId = 13 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "EUnit", fpayLocNo, prefixText)

        ElseIf fId = 14 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "ETask", fpayLocNo, prefixText)

        ElseIf fId = 17 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "EProject", fpayLocNo, prefixText)

        ElseIf fId = 18 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "ECostCenter", fpayLocNo, prefixText)

        ElseIf fId = 19 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "EJobGrade", fpayLocNo, prefixText)

        ElseIf fId = 20 Then
            ds = xLookup_Table_AutoComplete(onlineuserno, "ERank", fpayLocNo, prefixText)

        End If

        Return ds

    End Function

    Public Function xLookup_Table_AutoComplete(ByVal onlineuserno As Integer, ByVal Tablename As String, Optional ByVal fpayLocNo As Integer = 0, Optional ByVal Search As String = "", Optional ByVal sortColumn As String = "", Optional ByVal sortDirection As String = "") As DataSet
        Return SQLHelper.ExecuteDataSet("xTable_Lookup_AutoComplete", onlineuserno, Tablename, fpayLocNo, sortColumn, sortDirection, Search)
    End Function

    'Delete Record
    Public Function DeleteRecord(ByVal tProcedurename As String, ByVal userno As Integer, ByVal tId As Integer) As Integer

        Return SQLHelper.ExecuteNonQuery(tProcedurename, userno, tId)

    End Function
    Public Function DeleteRecordAudit(ByVal ttablename As String, ByVal userno As Integer, ByVal tId As Integer) As Integer

        Return SQLHelper.ExecuteNonQuery("zRow_Delete", userno, ttablename, tId)
    End Function
    Public Function DeleteRecordAuditCol(ByVal ttablename As String, ByVal userno As Integer, ByVal Lid As String, ByVal tId As Integer) As Integer

        Return SQLHelper.ExecuteNonQuery("zRow_DeleteCol", userno, ttablename, Lid, tId)
    End Function


#Region "********* clear breadcrumbs**********"
    Public Sub clearBreadcrumbs(ByVal index As Integer)

        If index = 1 Then
            'Disable second link
            System.Web.HttpContext.Current.Session("xMenuTitle2") = ""
            System.Web.HttpContext.Current.Session("xLinkURL2") = ""

            'Disable 3rd link
            System.Web.HttpContext.Current.Session("xMenuTitle3") = ""
            System.Web.HttpContext.Current.Session("xLinkURL3") = ""
            'Disable 4th link
            System.Web.HttpContext.Current.Session("xMenuTitle4") = ""
            System.Web.HttpContext.Current.Session("xLinkURL4") = ""
        ElseIf index = 2 Then
            'Disable 3rd link
            System.Web.HttpContext.Current.Session("xMenuTitle3") = ""
            System.Web.HttpContext.Current.Session("xLinkURL3") = ""
            'Disable 4th link
            System.Web.HttpContext.Current.Session("xMenuTitle4") = ""
            System.Web.HttpContext.Current.Session("xLinkURL4") = ""
        ElseIf index = 3 Then

            'Disable 4th link
            System.Web.HttpContext.Current.Session("xMenuTitle4") = ""
            System.Web.HttpContext.Current.Session("xLinkURL4") = ""
        End If

    End Sub
#End Region
#Region "******** Rene Codes ************"

    Public Shared Function DownloadFile(ByVal FilePath As String, Optional ByVal ContentType As String = "", Optional ByVal IsDelete As Boolean = False) As Boolean
        Dim myFileInfo As System.IO.FileInfo
        Dim StartPos As Long = 0, FileSize As Long, EndPos As Long

        ' Add the file name and attachment,
        ' which will force the open/cance/save dialog to show, to the header
        'Response.AddHeader("Content-Disposition", "attachment; filename=" & Session("XlsId"))

        ' bypass the Open/Save/Cancel dialog
        'Response.AddHeader("Content-Disposition", "inline; filename=" & Session("XlsId"))

        ' Add the file size into the response header
        'Response.AddHeader("Content-Length", doc.FileSize.ToString());

        If System.IO.File.Exists(FilePath) Then

            myFileInfo = New System.IO.FileInfo(FilePath)
            FileSize = myFileInfo.Length
            EndPos = FileSize

            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ClearHeaders()
            HttpContext.Current.Response.ClearContent()

            Dim Range As String = HttpContext.Current.Request.Headers("Range")
            If Not ((Range Is Nothing) Or (Range = "")) Then
                Dim StartEnd As Array = Range.Substring(Range.LastIndexOf("=") + 1).Split("-")
                If Not StartEnd(0) = "" Then
                    StartPos = CType(StartEnd(0), Long)
                End If
                If StartEnd.GetUpperBound(0) >= 1 And Not StartEnd(1) = "" Then
                    EndPos = CType(StartEnd(1), Long)
                Else
                    EndPos = FileSize - StartPos
                End If
                If EndPos > FileSize Then
                    EndPos = FileSize - StartPos
                End If
                HttpContext.Current.Response.StatusCode = 206
                HttpContext.Current.Response.StatusDescription = "Partial Content"
                HttpContext.Current.Response.AppendHeader("Content-Range", "bytes " & StartPos & "-" & EndPos & "/" & FileSize)
            End If

            If Not (ContentType = "") And (StartPos = 0) Then
                HttpContext.Current.Response.ContentType = ContentType
            End If

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

    Public Shared Function HtmlXls(ByVal _ds As Data.DataSet, ByVal xName As String) As Boolean
        Dim objWriter As System.IO.StreamWriter
        Dim lStr As New StringBuilder
        Dim i As Integer, j As Integer, k As Integer, d As Integer

        Try

            With _ds

                If .Tables.Count > 0 Then
                    i = .Tables(0).Rows.Count - 1
                    d = .Tables(0).Columns.Count - 1
                    If i >= 0 Then
                        objWriter = New System.IO.StreamWriter(DocPath & xName)

                        lStr.Append("<html><body><table border=")
                        lStr.Append(Chr(34))
                        lStr.Append("1")
                        lStr.Append(Chr(34))
                        lStr.Append("><tr>")

                        For j = 0 To d
                            lStr.Append("<th nowrap>")
                            lStr.Append(.Tables(0).Columns(j).ColumnName)
                            lStr.Append("</th>")
                        Next
                        lStr.Append("</tr>")

                        For j = 0 To i
                            lStr.Append("<tr>")
                            For k = 0 To d
                                lStr.Append("<td nowrap align=left >")
                                lStr.Append(.Tables(0).Rows(j)(k).ToString)
                                lStr.Append("</td>")
                            Next
                            lStr.Append("</tr>")

                        Next

                        lStr.Append("</table></body></html>")

                        objWriter.WriteLine(lStr)
                        objWriter.Close()

                    End If
                End If
            End With

        Catch Ex As Exception
            Return False

        End Try

        Return True

    End Function


    Public Function SendEmail(ByVal mFrom As String, ByVal mTo As String, ByVal mSubject As String, ByVal mMessage As String, Optional ByVal mFile As String = "", Optional ByVal mCC As String = "") As Boolean
        Dim rVal As Boolean = False
        Dim xEmailIPUser As String = "", xEmailIPPwd As String = "", xEmailIP As String = "cycoretech.com.ph", xCredentialInfo As String = "No"
        Try


            rVal = True

            Dim oMail As New System.Net.Mail.MailMessage
            With oMail
                .From = New System.Net.Mail.MailAddress(mFrom)
                .To.Add(mTo)
                .Subject = mSubject
                .IsBodyHtml = True
                .Body = mMessage

                If Not mFile = "" Then
                    Dim aFile As New System.Net.Mail.Attachment(mFile)
                    .Attachments.Add(aFile)
                End If

                If mCC > "" Then
                    .CC.Add(mCC)
                End If

            End With

            Dim mCredentialInfo As New System.Net.NetworkCredential(xEmailIPUser, xEmailIPPwd)
            Dim oSMTP As New System.Net.Mail.SmtpClient
            With oSMTP
                If xEmailIP > "" Then
                    .Host = xEmailIP
                Else
                    .Host = "192.168.0.81"
                End If
                If xCredentialInfo = "Yes" Then
                    .UseDefaultCredentials = False
                    .Credentials = mCredentialInfo
                End If
                '.Port = 25
                .Port = 587
                .Send(oMail)
            End With


        Catch ex As Exception

            rVal = False

        End Try
        Return True
    End Function

    Public Shared Function dsText(ByVal _ds As Data.DataSet, ByVal xName As String) As Boolean
        Dim objWriter As System.IO.StreamWriter
        Dim lStr As New StringBuilder
        Dim i As Integer, j As Integer, k As Integer, d As Integer

        Try

            With _ds

                If .Tables.Count > 0 Then
                    i = .Tables(0).Rows.Count - 1
                    d = .Tables(0).Columns.Count - 1
                    If i >= 0 Then
                        objWriter = New System.IO.StreamWriter(DocPath & xName)

                        For j = 0 To i
                            For k = 0 To d
                                lStr.Append(.Tables(0).Rows(j)(k).ToString & Chr(13) & Chr(10))
                            Next
                        Next

                        objWriter.WriteLine(lStr)
                        objWriter.Close()

                    End If
                End If
            End With

        Catch Ex As Exception
            Return False

        End Try

        Return True

    End Function
#End Region

#Region "201 related codes"
    Public Function SaveRecordPhoto(ByVal photopath As String, ByVal employeeno As Integer) As Boolean
        Try
            Dim xcmdProcSAVE As SqlCommand
            Dim tfilename As String = photopath 'Me.fuPhoto.PostedFile.FileName
            If tfilename.Length > 0 Then


                Dim fsPicture As FileStream = New FileStream(tfilename, FileMode.OpenOrCreate, FileAccess.Read)
                Dim picData() As Byte = New Byte(fsPicture.Length) {}
                fsPicture.Read(picData, 0, System.Convert.ToInt32(fsPicture.Length))
                fsPicture.Close()

                xcmdProcSAVE = Nothing
                xcmdProcSAVE = New SqlCommand


                xcmdProcSAVE.CommandText = "EEmployee_WebPhotoUpdate"
                xcmdProcSAVE.CommandType = CommandType.StoredProcedure
                xcmdProcSAVE.Connection = AssynChronous.xOpenConnection(SQLHelper.ConSTR)

                xcmdProcSAVE.Parameters.Add("@EmployeeNo", SqlDbType.Int, 4)
                xcmdProcSAVE.Parameters("@EmployeeNo").Value = Generic.CheckDBNull(employeeno, Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

                xcmdProcSAVE.Parameters.Add("@PhotoPath2", SqlDbType.Image)
                xcmdProcSAVE.Parameters("@PhotoPath2").Value = picData

                xcmdProcSAVE.ExecuteNonQuery()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
#End Region



End Class
