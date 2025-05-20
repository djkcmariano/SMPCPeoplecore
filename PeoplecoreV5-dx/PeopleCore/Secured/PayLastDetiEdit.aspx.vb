Imports clsLib
Imports System.Data
Partial Class Secured_PayLastEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim PayLastDetiNo As Integer = 0
    Dim PayNo As Integer = 0

    Protected Sub btnModify_Click(sender As Object, e As EventArgs)
        If txtIsPosted.Checked = True Then
            MessageBox.Information(MessageTemplate.PostedTransaction, Me)
        Else
            ViewState("IsEnabled") = True
            EnabledControls()
        End If
        txtFullName.Enabled = False
    End Sub
    Private Sub PopulateData()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayLastDeti_WebOne", UserNo, PayLastDetiNo)
        For Each row As DataRow In dt.Rows
            Generic.PopulateData(Me, "Panel1", dt)
        Next
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        PayLastDetiNo = Generic.ToInt(Request.QueryString("id"))
        PayNo = Generic.ToInt(Request.QueryString("PayNo"))
        Permission.IsAuthenticatedCoreUser()

        If Not IsPostBack Then
            HeaderInfo1.xFormName = "EPayLastDeti"
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", Generic.ToInt(Session("xPayLocNo")))
            PopulateData()
            PopulateTabHeader()
        End If

        EnabledControls()

    End Sub

    Private Sub EnabledControls()
        If PayLastDetiNo = 0 Then
            ViewState("IsEnabled") = True
        End If
        Dim Enabled As Boolean = Generic.ToBol(ViewState("IsEnabled"))

        Generic.EnableControls(Me, "Panel1", Enabled)

        btnModify.Visible = Not Enabled
        btnSave.Visible = Enabled
    End Sub

    Private Sub PopulateTabHeader()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPayLastDeti_WebTabHeader", UserNo, PayLastDetiNo)
            For Each row As DataRow In dt.Rows
                'lbl.Text = Generic.ToStr(row("Display"))
                txtIsPosted.Checked = Generic.ToBol(row("IsPosted"))
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)


        Dim RetVal As Boolean = False
        Dim dt As DataTable
        Dim PayLastDetiNo As Integer = Generic.ToInt(Me.txtPayLastDetiNo.Text)
        Dim employeeNo As Integer = Generic.ToInt(hifEmployeeNo.Value)
        Dim IsIncludeLeavebalance As Boolean = Generic.ToBol(txtIsIncludeLeavebalance.Checked)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EPayLastDeti_WebValidate", UserNo, PayLastDetiNo, PayNo, employeeNo, IsIncludeLeavebalance)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            Exit Sub
        End If

        dt = SQLHelper.ExecuteDataTable("EPayLastDeti_WebSave", UserNo, PayLastDetiNo, PayNo, employeeNo, IsIncludeLeavebalance)
        For Each row As DataRow In dt.Rows
            PayLastDetiNo = Generic.ToInt(row("PayLastDetiNo"))
            RetVal = True
        Next

        If RetVal = True Then
            If Generic.ToInt(Request.QueryString("id")) = 0 Then
                Dim url As String = "PayLastDTRList.aspx?id=" & PayLastDetiNo
                MessageBox.SuccessResponse(MessageTemplate.SuccessSave, Me, url)
            Else
                MessageBox.Success(MessageTemplate.SuccessSave, Me)
                ViewState("IsEnabled") = False
                EnabledControls()
            End If
            'PopulateTabHeader()
        Else
            MessageBox.Warning(MessageTemplate.ErrorSave, Me)
        End If


    End Sub

    <System.Web.Script.Services.ScriptMethod()> _
<System.Web.Services.WebMethod()> _
    Public Shared Function cboEmployee(prefixText As String, count As Integer, contextKey As String) As List(Of String)
        Dim items As New List(Of String)()
        Dim ds As New DataSet()
        Dim UserNo As Integer = 0, payLocno As Integer = 0
        UserNo = (HttpContext.Current.Session("onlineuserno"))
        payLocno = (HttpContext.Current.Session("xPayLocNo"))
        Dim payclassNo As Integer = (HttpContext.Current.Session("PayLastList_PayclassNo"))

        ds = SQLHelper.ExecuteDataSet("EEmployee_WebLookup_AC_PayClass", UserNo, prefixText, payclassNo, payLocno, count)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim item As String = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem((row("tDesc")), (row("tNo")))
            items.Add(item)
        Next
        ds.Dispose()
        Return items
    End Function

End Class
