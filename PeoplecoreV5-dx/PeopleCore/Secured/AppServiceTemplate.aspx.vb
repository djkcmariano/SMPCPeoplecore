Imports clsLib
Imports System.Data

Partial Class Secured_AppServiceTemplate
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Response.Redirect("appserviceTemplateEdit.aspx")
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton
        lnk = sender
        PopulateData(Generic.ToInt(lnk.CommandArgument))
        Response.Redirect("appserviceTemplateEdit.aspx?Id=" & Generic.ToInt(lnk.CommandArgument))
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim lnk As New LinkButton, chk As New CheckBox
        Dim i As Integer, count As Integer = 0
        For i = 0 To grd.Rows.Count - 1
            lnk = CType(grd.Rows(i).FindControl("lnkEdit"), LinkButton)
            chk = CType(grd.Rows(i).FindControl("chk"), CheckBox)
            If chk.Checked Then
                Generic.DeleteRecordAudit("EApplicantCoreTemplate", UserNo, Generic.ToInt(lnk.CommandArgument))
                count = count + 1
            End If
        Next
        If count > 0 Then
            MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
            PopulateGrid()
        Else
            MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("onlineUserNo"))
        If Not IsPostBack Then

            PopulateGrid()
        End If
    End Sub
    Private Sub PopulateGrid()
        Try
            grd.DataSource = SQLHelper.ExecuteDataTable("EApplicantCoreTemplate_Web", UserNo, "")
            grd.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EApplicantCoreTemplate_WebOne", 0, id)
            Generic.PopulateData(Me, "Panel1", dt)
        Catch ex As Exception

        End Try
    End Sub



End Class

