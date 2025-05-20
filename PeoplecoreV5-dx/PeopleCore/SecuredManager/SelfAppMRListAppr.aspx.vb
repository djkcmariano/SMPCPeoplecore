Imports clsLib
Imports System.Data
Imports DevExpress.Web
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class SecuredManager_SelfAppMRListAppr
    Inherits System.Web.UI.Page

   Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init

    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            PopulateDropDown()
            'PopulateGrid()
        End If
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

#Region "Main"
    Protected Sub PopulateGrid()
        Try
            If Generic.ToInt(cboTabNo.SelectedValue) = 1 Then
                lnkSubmit.Visible = True
                lnkApproved.Visible = False
                lnkDisapproved.Visible = False
            ElseIf Generic.ToInt(cboTabNo.SelectedValue) = 2 Then
                lnkSubmit.Visible = False
                lnkApproved.Visible = True
                lnkDisapproved.Visible = True
            Else
                lnkSubmit.Visible = False
                lnkApproved.Visible = False
                lnkDisapproved.Visible = False
            End If
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EMR_WebManager", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("ETab_WebLookup", UserNo, 25)
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataValueField = "tno"
            cboTabNo.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Response.Redirect("SelfAppMREditAppr.aspx?id=0")

    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        Dim URL As String
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        URL = Generic.GetFirstTab(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"MRNo"})))
        If URL <> "" Then
            Response.Redirect(URL)
        End If
    End Sub
    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"MRNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EMR", UserNo, item)
            Generic.DeleteRecordAuditCol("EMREduc", UserNo, "MRNo", item)
            Generic.DeleteRecordAuditCol("EMRExpe", UserNo, "MRNo", item)
            Generic.DeleteRecordAuditCol("EMRComp", UserNo, "MRNo", item)
            Generic.DeleteRecordAuditCol("EMRExam", UserNo, "MRNo", item)
            Generic.DeleteRecordAuditCol("EMRTrn", UserNo, "MRNo", item)
            Generic.DeleteRecordAuditCol("EMRHiredMass", UserNo, "MRNo", item)
            Generic.DeleteRecordAuditCol("EMRInterview", UserNo, "MRNo", item)
            Generic.DeleteRecordAuditCol("EMRInterviewDeti", UserNo, "MRNo", item)

            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
     
    End Sub
    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If

    End Sub
    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub
    Protected Sub lnkSearch_Click(sender As Object, e As EventArgs)
        PopulateGrid()
    End Sub

    Protected Sub lnkApproved_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"MRNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, 2, False)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me)
        PopulateGrid()
    End Sub
    Protected Sub lnkDisapproved_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"MRNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, 3, False)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessApproved, Me)
        PopulateGrid()
    End Sub

    Protected Sub lnkSendApp_Click(sender As Object, e As EventArgs)

        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"MRNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            ApproveTransaction(item, 1, True)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") transaction(s) successfully submitted for approval.", Me)
        PopulateGrid()
    End Sub
    Private Sub ApproveTransaction(tId As Integer, approvalStatNo As Integer, isSubmitforApp As Boolean)
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("EMR_WebApproved", UserNo, tId, approvalStatNo, isSubmitforApp)
        If fds.Tables.Count > 0 Then
            If fds.Tables(0).Rows.Count > 0 Then
                Dim IsWithapprover As Boolean
                IsWithapprover = Generic.CheckDBNull(fds.Tables(0).Rows(0)("IsWithApprover"), clsBase.clsBaseLibrary.enumObjectType.IntType)
                If IsWithapprover = True Then

                Else
                    MessageBox.Information("Unable to locate the next approver.", Me)
                End If
            End If


        End If
    End Sub
    
    Protected Sub btnAttach_Click(sender As Object, e As EventArgs)

    End Sub


#End Region

   
End Class
