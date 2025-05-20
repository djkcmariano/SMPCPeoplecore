<%@ WebHandler Language="VB" Class="frmShowImage" %>

Imports System
Imports System.Configuration
Imports System.Web
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports clsLib

Public Class frmShowImage : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim tNo As Int32
        Dim tIndex As Int32
       
        tNo = Convert.ToInt32(context.Request.QueryString("tNo"))
        tIndex = Convert.ToInt32(context.Request.QueryString("tIndex"))
       
        context.Response.ContentType = "image/jpeg"
     
        Dim strm As Stream = ShowEmpImage(tNo, tIndex)
        If Not strm Is Nothing Then
            Dim buffer As Byte() = New Byte(4095) {}
            Dim byteSeq As Integer = strm.Read(buffer, 0, 4096)
            Do While byteSeq > 0
                context.Response.OutputStream.Write(buffer, 0, byteSeq)
                byteSeq = strm.Read(buffer, 0, 4096)
            Loop
        End If
        'context.Response.BinaryWrite(buffer);
    End Sub
    
    Public Function ShowEmpImage(ByVal Applicantno As Integer, tIndex As Integer) As Stream

        Dim sqlhelp As New clsBase.SQLHelper
        Dim img() As Byte = Nothing
        Dim ds As DataSet
        
        If tIndex = 1 Then '1 for Applicant
            ds = SQLHelper.ExecuteDataSet("Select Photo As Photo From dbo.EApplicant Where applicantNo=" & Applicantno)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    'img = ds.Tables(0).Rows(0)("photo")
                    If Not IsDBNull(ds.Tables(0).Rows(0)("photo")) Then
                        img = ds.Tables(0).Rows(0)("photo")
                    End If
                End If
            End If
        ElseIf tIndex = 2 Then '2 for Employee
            'ds = SQLHelper.ExecuteDataSet("Select PhotoPath2 As Photo From dbo.EEmployee Where EmployeeNo=" & Applicantno)
            ds = SQLHelper.ExecuteDataSet("Select Isnull(photo,'') As Photo From dbo.EEmployeephoto Where EmployeeNo=" & Applicantno)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    If Not IsDBNull(ds.Tables(0).Rows(0)("photo")) Then
                        img = ds.Tables(0).Rows(0)("photo")
                    End If
                End If
            End If
        ElseIf tIndex = 3 Then '3 Clients Logo
            ds = SQLHelper.ExecuteDataSet("Select Isnull(Photo,'') As Photo From dbo.EClients Where ClientsNo=" & Applicantno)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    img = ds.Tables(0).Rows(0)("photo")
                End If
            End If
        ElseIf tIndex = 4 Then '4 Company Logo
            ds = SQLHelper.ExecuteDataSet("Select Isnull(Photo,'') As Photo From dbo.EPayLoc Where PayLocNo=" & Applicantno)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    img = ds.Tables(0).Rows(0)("photo")
                End If
            End If
        End If
        
        If img Is Nothing And tIndex <> 4 Then
            img = SQLHelper.ExecuteScalar("Select Isnull(Photo,'') As Photo From dbo.EImageDefault")
        End If
        
        'If img Is Nothing And tIndex <> 4 Then
        '    ds = SQLHelper.ExecuteDataSet("Select Isnull(Photo,'') As Photo From dbo.EImageDefault")
        '    If ds.Tables.Count > 0 Then
        '        If ds.Tables(0).Rows.Count > 0 Then
        '            img = ds.Tables(0).Rows(0)("photo")
        '        End If
        '    End If
        '    'ElseIf img Is Nothing And tIndex <> 4 Then
        '    '    ds = SQLHelper.ExecuteDataSet("Select Isnull(Photo,'') As Photo From dbo.EImageDefault")
        '    '    If ds.Tables.Count > 0 Then
        '    '        If ds.Tables(0).Rows.Count > 0 Then
        '    '            img = ds.Tables(0).Rows(0)("photo")
        '    '        End If
        '    '    End If
        'End If
              
        Try
            Return New MemoryStream(CType(img, Byte()))
        Catch
            Return Nothing
        Finally
        End Try

    End Function
    
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class