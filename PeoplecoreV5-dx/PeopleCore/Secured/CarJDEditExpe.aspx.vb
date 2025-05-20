Imports clsLib
Imports System.Data
Imports DevExpress.Web

Partial Class Secured_CarJDEditExpe
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
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EJDExpe_Web", UserNo, TransNo, Generic.ToInt(cboTabNo.SelectedValue))
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
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
            dt = SQLHelper.ExecuteDataTable("EJDExpe_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd, "CarJDList.aspx", "EJD") Then
            Generic.ClearControls(Me, "Panel1")
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        End If
    End Sub

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        If SaveRecord() = 1 Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Alert(Message, "warning", Me)
            ModalPopupExtender1.Show()
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit, "CarJDList.aspx", "EJD") Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"JDExpeNo"})))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete, "CarJDList.aspx", "EJD") Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"JDExpeNo"})
            Dim str As String = "", i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EJDExpe", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Function SaveRecord() As Integer
        Dim JDExpeNo As Integer = Generic.ToInt(txtCode.Text)
        Dim ExpeTypeNo As Integer = Generic.ToInt(cboExpeTypeNo.SelectedValue)
        Dim Description As String = Generic.ToStr(txtDescription.Text)

        'If SQLHelper.ExecuteNonQuery("EJDExpe_WebSave", UserNo, JDExpeNo, TransNo, ExpeTypeNo, Description, PayLocNo) > 0 Then
        '    Return True
        'Else
        '    Return False
        'End If
        Dim RetVal As Integer = 0
        Dim dt As DataTable = SQLHelper.ExecuteDataTable("EJDExpe_WebSave", UserNo, JDExpeNo, TransNo, ExpeTypeNo, Description, PayLocNo, chkIsArchived.Checked, txtEffectiveDate.Text, Generic.ToDbl(txtNoOfYear.Text), Generic.ToInt(chkIsQS.Checked))
        For Each row As DataRow In dt.Rows
            RetVal = Generic.ToInt(row("RetVal"))
            Message = Generic.ToStr(row("ErrorMessage"))
        Next
        Return RetVal
    End Function

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
