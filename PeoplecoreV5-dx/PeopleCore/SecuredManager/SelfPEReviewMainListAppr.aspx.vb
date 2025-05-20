Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_SelfPEReviewMainListAppr
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim clsGen As New clsGenericClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticatedSuperior()

        If Not IsPostBack Then
            PopulateDropDown()
        End If

        PopulateGrid()

    End Sub

    Private Sub PopulateDropDown()
        Try
            cboTabNo.DataSource = SQLHelper.ExecuteDataSet("EPEReviewMain_WebTab", UserNo, PayLocNo)
            cboTabNo.DataValueField = "tNo"
            cboTabNo.DataTextField = "tDesc"
            cboTabNo.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        PopulateGrid()
    End Sub

#Region "Main"
    Protected Sub PopulateGrid()

        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("EPEReview_WebAppr", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try

    End Sub


    Protected Sub lnkForm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim peevaluatorno As Integer = 0
            Dim pereviewmainno As Integer = 0
            Dim pereviewcateno As Integer = 0
            Dim pecatetypeno As Integer = 0
            Dim pereviewno As Integer = 0
            Dim pecycleno As Integer = 0
            Dim isposted As Boolean = False
            Dim isenabled As Boolean = False

            Dim lnk As New LinkButton
            lnk = sender
            Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
            Dim obj As Object() = container.Grid.GetRowValues(container.VisibleIndex, New String() {"PEEvaluatorNo", "PEReviewNo", "PECycleNo", "IsPosted", "PEReviewMainNo", "IsEnabled"})
            peevaluatorno = obj(0)
            pereviewno = obj(1)
            pecycleno = obj(2)
            isposted = obj(3)
            pereviewmainno = obj(4)
            isenabled = obj(5)

            If isenabled = False Then
                isposted = True
            End If

            Dim FormName As String = ""
            Dim ComponentNo As Integer = 2
            Dim dt As New DataTable
            dt = SQLHelper.ExecuteDataTable("EMenu_PEReviewTab", UserNo, pereviewno, ComponentNo, False, peevaluatorno)
            For Each row As DataRow In dt.Select("RowNo=1")
                FormName = Generic.ToStr(row("Formname"))
                pereviewcateno = Generic.ToStr(row("PEReviewCateNo"))
                pecatetypeno = Generic.ToStr(row("PECateTypeNo"))
            Next

            If FormName > "" Then
                Response.Redirect(FormName & "?pereviewmainno=" & pereviewmainno & "&pereviewcateno=" & pereviewcateno & "&pecatetypeno=" & pecatetypeno & "&pereviewno=" & pereviewno & "&peevaluatorno=" & peevaluatorno & "&pecycleno=" & pecycleno & "&componentno=" & ComponentNo & "&isposted=" & isposted)
            Else
                MessageBox.Warning("No template created.", Me)
            End If

        Catch ex As Exception
        End Try


    End Sub



    Protected Sub lnkRevise_Click(sender As Object, e As System.EventArgs)

        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "PEReviewEvaluatorNo"))
                Dim x As Integer = Generic.ToInt(grdMain.GetRowValues(j, "PECycleNo"))
                ApproveTransaction(item, "", 5, x)
                grdMain.Selection.UnselectRow(j)
                i = i + 1
            End If
        Next

        If i > 0 Then
            PopulateGrid()
            MessageBox.Success("(" + i.ToString + ") transaction(s) submitted for revision.", Me)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If
        

    End Sub
    Protected Sub lnkApproved_Click(sender As Object, e As System.EventArgs)

        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "PEReviewEvaluatorNo"))
                Dim x As Integer = Generic.ToInt(grdMain.GetRowValues(j, "PECycleNo"))
                ApproveTransaction(item, "", 2, x)
                grdMain.Selection.UnselectRow(j)
                i = i + 1
            End If
        Next

        If i > 0 Then
            PopulateGrid()
            MessageBox.Success("(" + i.ToString + ") transaction(s) successfully approved.", Me)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub
    Private Sub ApproveTransaction(tId As Integer, remarks As String, approvalStatNo As Integer, PECycleNo As Integer)
        Dim fds As DataSet
        fds = SQLHelper.ExecuteDataSet("EPEReviewEvaluator_WebApproved", UserNo, tId, approvalStatNo, remarks, PECycleNo)
        If fds.Tables.Count > 0 Then
            If fds.Tables(0).Rows.Count > 0 Then
                Dim IsWithapprover As Boolean
                IsWithapprover = Generic.ToBol(fds.Tables(0).Rows(0)("IsWithApprover"))
                If IsWithapprover = True Then

                Else
                    MessageBox.Warning("Unable to locate the next approver.", Me)
                End If
            End If


        End If
    End Sub


    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub

#End Region



End Class

