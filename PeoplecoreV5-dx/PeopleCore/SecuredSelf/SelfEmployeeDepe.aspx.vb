Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_EmpDepeList
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeDepe_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeDepe_WebOne", UserNo, id)
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
        Permission.IsAuthenticated()

        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateTabHeader()
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)

        Generic.ClearControls(Me, "Panel1")
        ModalPopupExtender1.Show()

    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim RelationshipNo As Integer = Generic.ToInt(Me.cboRelationShipNo.SelectedValue)
        Dim EmployeeDepeNo As Integer = Generic.ToInt(Me.txtCode.Text)
        Dim CivilStatNo As Integer = Generic.ToInt(Me.cboCivilStatNo.SelectedValue)
        Dim IsDependent As Boolean = Generic.ToBol(Me.chkIsDependent.Checked)
        Dim IsBeneficiary As Boolean = Generic.ToBol(Me.chkIsBeneficiary.Checked)
        Dim IsInsurance As Boolean = Generic.ToBol(Me.chkIsInsurance.Checked)
        Dim IsWithHMO As Boolean = Generic.ToBol(Me.chkIsWithHMO.Checked)

        Dim RetVal As Boolean = False
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable, error_num As Integer = 0, error_message As String = ""
        dtx = SQLHelper.ExecuteDataTable("EEmployeeDepe_WebValidate", UserNo, EmployeeDepeNo, TransNo, txtLastName.Text, txtFirstName.Text, txtMiddleName.Text, RelationshipNo, _
                                      txtOccupation.Text, txtBirthDate.Text, txtPhoneNo.Text, IsInsurance, "", IsBeneficiary, txtHomeAddress.Text, txtEmployerName.Text, _
                                      txtEmployerAdd.Text, txtEmployerTelNo.Text, IsDependent, CivilStatNo, IsWithHMO, "")

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("tProceed"))
            messagedialog = Generic.ToStr(rowx("xMessage"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        Dim dt As DataTable, ret As Integer, msg As String = ""
        dt = SQLHelper.ExecuteDataTable("EEmployeeDepeUpd_WebSaveSelf", UserNo, EmployeeDepeNo, TransNo, txtLastName.Text, txtFirstName.Text, txtMiddleName.Text, RelationshipNo, _
                                          txtOccupation.Text, txtBirthDate.Text, txtPhoneNo.Text, IsInsurance, "", IsBeneficiary, txtHomeAddress.Text, txtEmployerName.Text, _
                                          txtEmployerAdd.Text, txtEmployerTelNo.Text, IsDependent, CivilStatNo, Generic.ToInt(cboEmployeeExtNo.SelectedValue), txtMaidenName.Text, IsWithHMO, txtRemark.Text)
        For Each row As DataRow In dt.Rows
            msg = Generic.ToStr(row("xMessage"))
            ret = Generic.ToInt(row("RetVal"))
        Next
        If ret = 1 Then
            MessageBox.Success(msg, Me)
            PopulateGrid()
        Else
            MessageBox.Alert(msg, "warning", Me)
            ModalPopupExtender1.Show()
        End If


    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeDepeNo"})))
        ModalPopupExtender1.Show()

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeDepeNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ViewState("Id") = CType(item, Integer)
            i = i + 1
        Next

        If i = 1 Then
            Dim dt As DataTable, ret As Integer, msg As String = ""
            dt = SQLHelper.ExecuteDataTable("EEmployeeDepeUpd_WebDelete", UserNo, ViewState("Id"))
            For Each row As DataRow In dt.Rows
                msg = Generic.ToStr(row("xMessage"))
                ret = Generic.ToInt(row("RetVal"))
            Next
            If ret = 1 Then
                MessageBox.Success(msg, Me)
                PopulateGrid()
            Else
                MessageBox.Alert(msg, "warning", Me)
            End If
        Else
            MessageBox.Warning("Please select 1 record to delete.", Me)
        End If

    End Sub


    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EEmployeeTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=2"
    End Sub

End Class


