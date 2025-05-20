Imports System.Data
Imports clsLib
Imports DevExpress.Web

Partial Class Secured_EmpVolunter
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeVolunter_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmployeeVolunter_WebOne", UserNo, id, PayLocNo)
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
            PopulateCombo()
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
        Dim RetVal As Boolean = False
        Dim EmployeeVolunterNo As Integer = Generic.ToInt(Me.txtCode.Text)
        Dim Organization As String = Generic.ToStr(Me.txtOrganization.Text)
        Dim Position As String = Generic.ToStr(Me.txtPosition.Text)
        Dim NoOfHour As Double = Generic.ToDec(Me.txtNoOfHour.Text)
        Dim Address As String = Generic.ToStr(Me.txtAddress.Text)
        Dim FromDay As Integer = Generic.ToInt(cboFromDay.SelectedValue)
        Dim FromMonth As Integer = Generic.ToInt(cboFromMonth.SelectedValue)
        Dim FromYear As Integer = Generic.ToInt(txtFromYear.Text)
        Dim ToDay As Integer = Generic.ToInt(cboToDay.SelectedValue)
        Dim ToMonth As Integer = Generic.ToInt(cboToMonth.SelectedValue)
        Dim ToYear As Integer = Generic.ToInt(txtToYear.Text)

        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable
        dtx = SQLHelper.ExecuteDataTable("EEmployeeVolunter_WebValidate", UserNo, EmployeeVolunterNo, TransNo, Organization, "", "", NoOfHour, Position, Address, _
                                     FromDay, FromMonth, FromYear, ToDay, ToMonth, ToYear)

        For Each rowx As DataRow In dtx.Rows
            invalid = Generic.ToBol(rowx("Invalid"))
            messagedialog = Generic.ToStr(rowx("MessageDialog"))
            alerttype = Generic.ToStr(rowx("AlertType"))
        Next

        If invalid = True Then
            MessageBox.Alert(messagedialog, alerttype, Me)
            ModalPopupExtender1.Show()
            Exit Sub
        End If

        If SQLHelper.ExecuteNonQuery("EEmployeeVolunter_WebSave", UserNo, EmployeeVolunterNo, TransNo, Organization, "", "", NoOfHour, Position, Address, _
                                     FromDay, FromMonth, FromYear, ToDay, ToMonth, ToYear, chkIsPresent.Checked) > 0 Then
            RetVal = True
        Else
            RetVal = False
        End If

        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            'PopulateData(Generic.ToInt(lnk.CommandArgument))
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmployeeVolunterNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"EmployeeVolunterNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EEmployeeVolunter", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub


    Private Sub PopulateCombo()

        Try
            cboFromMonth.DataSource = SQLHelper.ExecuteDataSet("EMonth_WebLookup")
            cboFromMonth.DataValueField = "tNo"
            cboFromMonth.DataTextField = "tDesc"
            cboFromMonth.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboToMonth.DataSource = SQLHelper.ExecuteDataSet("EMonth_WebLookup")
            cboToMonth.DataValueField = "tNo"
            cboToMonth.DataTextField = "tDesc"
            cboToMonth.DataBind()
        Catch ex As Exception
        End Try


        Try
            cboFromDay.DataSource = SQLHelper.ExecuteDataSet("EDay_WebLookup")
            cboFromDay.DataValueField = "tNo"
            cboFromDay.DataTextField = "tDesc"
            cboFromDay.DataBind()
        Catch ex As Exception
        End Try

        Try
            cboToDay.DataSource = SQLHelper.ExecuteDataSet("EDay_WebLookup")
            cboToDay.DataValueField = "tNo"
            cboToDay.DataTextField = "tDesc"
            cboToDay.DataBind()
        Catch ex As Exception
        End Try

    End Sub


    Protected Sub chkIsPresent_CheckedChanged()
        If Me.chkIsPresent.Checked Then
            cboToDay.Enabled = False
            cboToMonth.Enabled = False
            txtToYear.Enabled = False
            cboToDay.Text = ""
            cboToMonth.Text = ""
            txtToYear.Text = ""
        Else
            cboToDay.Enabled = True
            cboToMonth.Enabled = True
            txtToYear.Enabled = True
        End If
        ModalPopupExtender1.Show()
    End Sub
End Class





