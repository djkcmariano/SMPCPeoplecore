Imports clsLib
Imports System.Data
Imports DevExpress.Web
Imports System.Data.SqlClient

Partial Class Secured_CarJDEditKeyRelation
    Inherits System.Web.UI.Page
    Dim UserNo As Integer
    Dim TransNo As Integer
    Dim PayLocNo As Integer
    Dim Message As String


    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EJDTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
    End Sub

    Protected Sub PopulateGrid()
        'Try
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EJDKeyRelation_Web", UserNo, TransNo, Generic.ToInt(cboTabNo.SelectedValue))
        grdMain.DataSource = dt
        grdMain.DataBind()
        'Catch ex As Exception

        'End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        AccessRights.CheckUser(UserNo, "CarJDList.aspx", "EJD")
        If Not IsPostBack Then
            PopulateTabHeader()
            Generic.PopulateDropDownList(UserNo, Me, "Panel1", PayLocNo)
            PopulateDropDown()
        End If

        PopulateGrid()
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EJDKeyRelation_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "CarJDList.aspx", "EJD") Then
            Generic.ClearControls(Me, "Panel1")
            chkIsOccasional.Checked = True
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SQLHelper.ExecuteNonQuery("EJDKeyRelation_WebSave", UserNo, Generic.ToInt(txtCode.Text), TransNo, txtJDKeyRelationDesc.Text, Generic.ToInt(cboJDKeyRelationTypeNo.SelectedValue), _
                                     Generic.ToInt(chkIsOccasional.Checked), Generic.ToInt(chkIsFrequent.Checked), txtPurpose.Text, Generic.ToInt(chkIsArchived.Checked)) > 0 Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "CarJDList.aspx", "EJD") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"JDKeyRelationNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "CarJDList.aspx", "EJD") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"JDEducNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EJDEduc", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 14)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        'ViewState("TransNo") = 0
        PopulateGrid()
    End Sub

End Class
