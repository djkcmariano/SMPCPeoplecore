Imports System.Data
Imports clsLib
Imports DevExpress.Web


Partial Class Secured_EmpRefeList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
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
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateTabHeader()
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"

    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        'If SaveRecord() Then
        '    MessageBox.Success(MessageTemplate.SuccessSave, Me)
        '    PopulateGrid()
        'Else
        '    MessageBox.Critical(MessageTemplate.ErrorSave, Me)
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
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender            
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeRefeNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeRefeNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeRefe", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    'Private Function SaveRecord() As Boolean


    'End Function

End Class






