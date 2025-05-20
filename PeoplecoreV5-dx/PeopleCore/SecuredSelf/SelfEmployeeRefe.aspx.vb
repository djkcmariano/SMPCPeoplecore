Imports clsLib
Imports System.Data

Partial Class SecuredSelf_SelfEmployeeRefe
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeRefe_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeRefe_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticated()
        If Not IsPostBack Then
            PopulateGrid()
            PopulateTabHeader()
        End If
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "Panel1")
        ModalPopupExtender1.Show()
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        'Dim dt As DataTable, ret As Integer, msg As String = ""
        ' Dim yrsknown As Integer = Generic.ToInt(Me.txtAcquaintanceYear.Text)
        'dt = SQLHelper.ExecuteDataTable("EEmployeeRefeUpd_WebSaveSelf", UserNo, Generic.ToInt(txtCode.Text), TransNo, Me.txtlastname.Text, Me.txtFirstName.Text, Me.txtMiddleName.Text, Me.txtBusinessAddress.Text, Me.txtAddress.Text, Me.txtOccupation.Text, Me.txtPhoneNo.Text, Me.txtBusinessPhoneNo.Text, Me.txtRelationRefeType.Text, yrsknown, Me.txtCompany.Text, txtEmail.Text)

        'For Each row As DataRow In dt.Rows
        '    msg = Generic.ToStr(row("xMessage"))
        '    ret = Generic.ToInt(row("RetVal"))
        'Next
        'If ret = 1 Then
        '    MessageBox.Success(msg, Me)
        'Else
        '    MessageBox.Information(msg, Me)
        'End If
        Dim EmployeeRefeNo As Integer = Generic.ToInt(Me.txtCode.Text)
        Dim LastName As String = Generic.ToStr(Me.txtlastname.Text)
        Dim FirstName As String = Generic.ToStr(Me.txtFirstName.Text)
        Dim MiddleName As String = Generic.ToStr(Me.txtMiddleName.Text)
        Dim Occupation As String = Generic.ToStr(Me.txtOccupation.Text)
        Dim ContactNo As String = Generic.ToStr(Me.txtPhoneNo.Text)
        Dim HomeAddress As String = Generic.ToStr(Me.txtAddress.Text)
        Dim BusinessAddress As String = Generic.ToStr(Me.txtBusinessAddress.Text)
        Dim BusinessPhoneNo As String = Generic.ToStr(Me.txtBusinessPhoneNo.Text)
        Dim YearsKnown As Integer = Generic.ToInt(Me.txtAcquaintanceYear.Text)
        Dim RelationRefeType As String = Generic.ToStr(Me.txtRelationRefeType.Text)
        Dim Company As String = Generic.ToStr(Me.txtCompany.Text)
        Dim Email As String = Me.txtEmail.Text
        Dim dt As DataTable
        Dim success As Boolean = False
        Dim msg As String = ""


        dt = SQLHelper.ExecuteDataTable("EEmployeeRefe_WebSave", UserNo, EmployeeRefeNo, TransNo, LastName, FirstName, MiddleName, BusinessAddress, HomeAddress, Occupation, ContactNo, BusinessPhoneNo, RelationRefeType, YearsKnown, Company, Email)
        For Each row As DataRow In dt.Rows
            success = Generic.ToBol(row("IsProceed"))
            msg = Generic.ToStr(row("msg"))
        Next

        If success Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            If msg = "" Then
                MessageBox.Alert(MessageTemplate.ErrorSave, "warning", Me)
            Else
                MessageBox.Alert(msg, "warning", Me)
            End If
            ModalPopupExtender1.Show()
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim ib As New LinkButton
        ib = sender
        PopulateData(Generic.ToInt(ib.CommandArgument))
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        'Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeRefeNo"})
        'Dim str As String = "", i As Integer = 0
        'For Each item As Integer In fieldValues
        '    ViewState("Id") = CType(item, Integer)
        '    i = i + 1
        'Next

        'If i = 1 Then
        '    Dim dt As DataTable, ret As Integer, msg As String = ""
        '    dt = SQLHelper.ExecuteDataTable("EEmployeeRefeUpd_WebDelete", UserNo, ViewState("Id"))
        '    For Each row As DataRow In dt.Rows
        '        msg = Generic.ToStr(row("xMessage"))
        '        ret = Generic.ToInt(row("RetVal"))
        '    Next
        '    If ret = 1 Then
        '        MessageBox.Success(msg, Me)
        '        PopulateGrid()
        '    Else
        '        MessageBox.Alert(msg, "warning", Me)
        '    End If
        'Else
        '    MessageBox.Warning("Please select 1 record to delete.", Me)
        'End If

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeRefeNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EEmployeeRefe", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()

    End Sub


End Class
