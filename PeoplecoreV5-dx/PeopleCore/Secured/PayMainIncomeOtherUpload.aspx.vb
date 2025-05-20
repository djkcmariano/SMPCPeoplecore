Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.IO
Imports clsLib

Partial Class Secured_PayMainIncomeOtherUpload
    Inherits System.Web.UI.Page

    Dim xPublicVar As New clsPublicVariable




    Dim tmodify As Boolean = False
    Dim transNo As Integer = 0

    Dim clsMessage As New clsMessage
    Dim showFrm As New clsFormControls
    Dim rowno As Integer = 0
    Dim tabOrder As Integer = 0
    Dim transDetiNo As Integer = 0

    'Display record
    Private Sub populateData()
        Try

            Dim _ds2 As New DataSet

            _ds2 = sqlhelper.ExecuteDataSet("EPay_WebOne", xPublicVar.xOnlineUseNo, transNo)
            If _ds2.Tables.Count > 0 Then
                If _ds2.Tables(0).Rows.Count > 0 Then
                    Me.txtPayCode.Text = Generic.CheckDBNull(_ds2.Tables(0).Rows(0)("PayCode"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                    Me.cboPayClassNo.Text = Generic.CheckDBNull(_ds2.Tables(0).Rows(0)("PayClassNo"), clsBase.clsBaseLibrary.enumObjectType.StrType)
                End If
            End If
            _ds2 = Nothing


        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateCombo()
        Try
            showFrm.populateCombo(xPublicVar.xOnlineUseNo, Me, Session("xPayLocNo"))
        Catch ex As Exception
        End Try
        Try
            cboPayIncomeTypeNo.DataSource = sqlhelper.ExecuteDataSet("EPayIncomeType_WebLookup", xPublicVar.xOnlineUseNo, Session("xpayLocNo"))
            cboPayIncomeTypeNo.DataValueField = "tNo"
            cboPayIncomeTypeNo.DataTextField = "tDesc"
            cboPayIncomeTypeNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    'Enable or disable control
    Private Sub DisableEnableCtrl(ByVal IsLock As Boolean)
        Me.cboPayClassNo.Enabled = False
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("onlineuserno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        If xPublicVar.xOnlineUseNo = 0 Then
            Response.Redirect("../frmpageExpired.aspx")
        Else
            transNo = Generic.CheckDBNull(Request.QueryString("transNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
            transDetiNo = Generic.CheckDBNull(Request.QueryString("transDetiNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

            If Not IsPostBack Then

                populateData()
                PopulateCombo()
            End If

            DisableEnableCtrl(tmodify)
        End If


        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    'Cancel modify
    Protected Sub lnkCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCancel.Click

        Response.Redirect("~/secured/PayMainIncomeOtherList.aspx?transNo=" & transNo & "&tModify=False&tabOrder=" & tabOrder & "&transDetiNo=" & transDetiNo)
    End Sub

    'Submit record
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If PoplulateCSVFile() Then
            Dim url As String = "PayMainIncomeOtherList.aspx?transNo=" & transNo & "&tModify=False&tabOrder=" & tabOrder & "&transDetiNo=" & transDetiNo
            MessageBox.SuccessResponse("Uploading of file successfully done.", Me, url)
        Else
            MessageBox.Critical(clsMessage.GetMessageType(Global.clsMessage.EnumMessageType.ErrorSave), Me)
        End If

    End Sub

    Private Function PoplulateCSVFile() As Boolean
        Dim tsuccess As Integer = 0
        Try


            Dim lastname As String = ""
            Dim tfilename As String = "", tFilepath As String = "", tProceed As Boolean = False
            Dim tpath As String = ""
            Dim datenow As Date
            datenow = Now()

            Dim filext As String = Pad.PadZero(2, Month(datenow)) & Pad.PadZero(2, Day(datenow)) & Pad.PadZero(4, Year(datenow)) & Pad.PadZero(2, Hour(datenow)) & Pad.PadZero(2, Minute(datenow)) & Pad.PadZero(4, Second(datenow))
            If txtFile.HasFile = True Then
                tFilepath = txtFile.PostedFile.FileName
                tfilename = IO.Path.GetFileName(tFilepath)
                Dim fileext As String = IO.Path.GetExtension(tFilepath)
                tProceed = True
                tpath = (Server.MapPath("documents")) 'Me.MapPath("documents") & "\
                If Not IO.Directory.Exists(tpath) Then
                    IO.Directory.CreateDirectory(tpath)
                End If
                txtFile.SaveAs(tpath & "\" & tfilename & "_" & filext)

            End If


            Dim amount As Double = 0, employeecode As String = ""

            If tProceed Then

                Dim fspecArr() As String, nfile As String = ""
                Dim i As Integer = 0, employeeno As String, logtype As String = ""
                Dim fs As FileStream, fFilename As String
                fFilename = tpath & "\" & tfilename & "_" & filext 'tpath & "\" & tfilename
                fs = New FileStream(fFilename, FileMode.Open, FileAccess.Read)
                Dim d As New StreamReader(fs)
                Dim rDesc As String = ""
                Dim rAmount As String = ""
                d.BaseStream.Seek(0, SeekOrigin.Begin)
                While d.Peek() > -1
                    nfile = d.ReadLine()
                    fspecArr = Split(nfile, ",")
                    employeeno = fspecArr(0)
                    rDesc = fspecArr(1)
                    rAmount = Replace(fspecArr(2), ":", "")
                    'If Len(fspecArr) > 0 Then
                    If i > 0 Then
                        If employeeno > "" And rDesc > "" And rAmount <> 0 Then
                            SQLHelper.ExecuteDataSet("EPayMainIncomeOther_WebUpload", xPublicVar.xOnlineUseNo, employeeno, CDbl(rAmount), Me.cboPayIncomeTypeNo.SelectedValue, rDesc, transNo)
                            tsuccess = tsuccess + 1
                        End If
                    End If

                    i = i + 1
                End While
                d.Close()
                Return True
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    
End Class







