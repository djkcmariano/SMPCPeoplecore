Imports System.Data
Imports clsLib
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports RestSharp
Partial Class Secured_AppRefEdit
    Inherits System.Web.UI.Page
    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0

    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EApplicantRefe_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub PopulateData(id As Int64)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EApplicantRefe_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "Panel1", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        If Not IsPostBack Then
            PopulateTabHeader()
        End If
        PopulateGrid()
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    Private Sub PopulateTabHeader()
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EApplicantTabHeader", UserNo, TransNo)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr(row("Display"))
        Next
        imgPhoto.ImageUrl = "frmShowImage.ashx?tNo=" & Generic.ToInt(TransNo) & "&tIndex=1"

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

        If SaveRecord() Then
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
            PopulateGrid()
        Else
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
            Dim lnk As New LinkButton
            lnk = sender
            Generic.ClearControls(Me, "Panel1")
            PopulateData(Generic.ToInt(lnk.CommandArgument))
            ModalPopupExtender1.Show()
        Else
            MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        End If
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
            Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"ApplicantRefeNo"})
            Dim i As Integer = 0
            For Each item As Integer In fieldValues
                Generic.DeleteRecordAudit("EApplicantRefe", UserNo, item)
                i = i + 1
            Next
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        End If
    End Sub

    Private Function SaveRecord() As Boolean
        Dim error_message As String = ""
        Dim yrsknown As Integer = Generic.ToInt(Me.txtAcquaintanceYear.Text)

        Dim dt1 As DataTable = SQLHelper.ExecuteDataTable("EApplicantRefe_WebSave", UserNo, Generic.ToInt(txtApplicantRefeNo.Text), TransNo, Me.txtLastName.Text, Me.txtFirstName.Text, Me.txtMiddlename.Text, Me.txtBusinessAddress.Text, Me.txtAddress.Text, Me.txtOccupation.Text, Me.txtPhoneNo.Text, Me.txtBusinessPhoneNo.Text, Me.txtRelationRefeType.Text, yrsknown, Me.txtCompany.Text, txtEmail.Text)

        Dim json As String = JsonConvert.SerializeObject(dt1)
        Try
            Dim factory As New RestSharpClientFactory()
            Dim client As RestClient = factory.GetClient()

            Dim request As New RestRequest("api/push/onejsondata", Method.Post)
            request.AddBody(New With {
                .totalRows = 1,
                    .hasMore = False,
                    .content = json,
                    .tableName = "EApplicantRefe"
                })

            Dim response As RestResponse = client.Execute(request)
            If response.IsSuccessful Then
                Dim jsonData = JsonConvert.DeserializeObject(Of APIStatus)(response.Content)
                Dim arr As JArray = JArray.Parse(json)
                arr(0)("ApplicantRefeNo") = jsonData.Id
                json = arr.ToString(Newtonsoft.Json.Formatting.None)
                SQLHelper.ExecuteNonQuery("EJSONMain_WebSave", json, "EApplicantRefe")
                Return True
            Else
                Return False
                error_message = "Unable to save record in career portal server."
            End If
        Catch ex As Exception
            error_message = ex.Message
        End Try

    End Function


End Class
