Imports System.Data
Imports clsLib
Imports DevExpress.Web


Partial Class Secured_frmEmailTemplateList
    Inherits System.Web.UI.Page
    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        AccessRights.CheckUser(UserNo)
        PopulateGrid()

    End Sub

    Private Sub PopulateGrid(Optional ByVal SortExp As String = "", Optional ByVal sordir As String = "")
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EEmailTemp_Web", UserNo, "", Generic.ToStr(Session("xMenuType")), PayLocNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
            Dim URL As String
            URL = Generic.GetFirstTab("0")
            If URL <> "" Then
                Response.Redirect(URL)
            End If
        End If
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EEmailTemp_WebOne", UserNo, Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"EmailTempNo"})))
            Generic.PopulateData(Me, "pnlPopupDetl", dt)
            mdlDetl.Show()
        End If
    End Sub

    Protected Sub lnkSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
    End Sub


    Private Function SaveRecord() As Boolean
        If SQLHelper.ExecuteNonQuery("EEmailTemp_WebSave", UserNo, Generic.ToStr(txtCode.Text), txtEmailTempCode.Text, txtEmailTempDesc.Text, txtEmailTempSubj.Text, txtEmailTempMsg.Text, Generic.ToStr(Session("xmenutype")), Generic.ToStr(Me.txtEmailAddress.Text), "", PayLocNo) > 0 Then
            SaveRecord = True
        Else
            SaveRecord = False
        End If
    End Function


End Class

