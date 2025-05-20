Imports clsLib
Imports System.Data
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports DevExpress.Web

Partial Class Secured_SelfPEReviewMainList
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0

    Dim clsGen As New clsGenericClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        Permission.IsAuthenticated()

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
            dt = SQLHelper.ExecuteDataTable("EPEReview_Webself", UserNo, Generic.ToInt(cboTabNo.SelectedValue), PayLocNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    'Protected Sub lnkForm_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim ib As New ImageButton
    '        Dim pereviewevaluatorno As Integer = 0
    '        Dim pestandardmainno As Integer = 0
    '        Dim pestandardcateno As Integer = 0
    '        Dim pecatetypeno As Integer = 0
    '        Dim pereviewno As Integer = 0
    '        Dim pereviewmainno As Integer = 0
    '        ib = sender
    '        Dim gvrow As GridViewRow = DirectCast(ib.NamingContainer, GridViewRow)
    '        rowno = gvrow.RowIndex

    '        Me.grdMain.SelectedIndex = Generic.ToInt(rowno)
    '        pereviewevaluatorno = grdMain.DataKeys(gvrow.RowIndex).Values(0).ToString()
    '        pestandardmainno = grdMain.DataKeys(gvrow.RowIndex).Values(1).ToString()
    '        pereviewno = grdMain.DataKeys(gvrow.RowIndex).Values(2).ToString()

    '        Dim FormName As String = ""
    '        Dim ComponentNo As Integer = 3
    '        Dim dt As New DataTable
    '        dt = SQLHelper.ExecuteDataTable("EMenu_PETab", UserNo, pestandardmainno, ComponentNo, False)
    '        For Each row As DataRow In dt.Select("RowNo=1")
    '            FormName = Generic.ToStr(row("Formname"))
    '            pestandardcateno = Generic.ToStr(row("pestandardcateno"))
    '            pecatetypeno = Generic.ToStr(row("PECateTypeNo"))
    '        Next

    '        If FormName > "" Then
    '            Response.Redirect(FormName & "?pestandardmainno=" & pestandardmainno & "&pestandardcateno=" & pestandardcateno & "&pecatetypeno=" & pecatetypeno & "&pereviewmainno=" & pereviewmainno & "&pereviewno=" & pereviewno & "&pereviewevaluatorno=" & pereviewevaluatorno)
    '        Else
    '            MessageBox.Warning("No template created.", Me)
    '        End If

    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    '    Dim lbl As New Label, lblPEReviewMainNo As New Label, lblPEReviewNo As New Label, tcheck As New CheckBox
    '    Dim tcount As Integer, Count As Integer = 0, lblPECycle As New Label

    '    Dim _xds As New DataSet
    '    Dim IsProceed As Integer = 0
    '    Dim xMessage As String = ""

    '    For tcount = 0 To Me.grdMain.Rows.Count - 1
    '        lbl = CType(grdMain.Rows(tcount).FindControl("lblIdEval"), Label)
    '        lblPEReviewMainNo = CType(grdMain.Rows(tcount).FindControl("lblPEReviewMainNo"), Label)
    '        lblPEReviewNo = CType(grdMain.Rows(tcount).FindControl("lblPEReviewNo"), Label)
    '        lblPECycle = CType(grdMain.Rows(tcount).FindControl("lblPECycleNo"), Label)

    '        tcheck = CType(grdMain.Rows(tcount).FindControl("txtIsSelect"), CheckBox)
    '        If tcheck.Checked = True Then
    '            _xds = SQLHelper.ExecuteDataSet("EPEReview_WebValidate", UserNo, CType(lbl.Text, Integer))
    '            If _xds.Tables.Count > 0 Then
    '                If _xds.Tables(0).Rows.Count > 0 Then
    '                    IsProceed = Generic.ToInt(_xds.Tables(0).Rows(0)("tProceed"))
    '                    xMessage = Generic.ToStr(_xds.Tables(0).Rows(0)("xMessage"))
    '                End If
    '            End If

    '            If IsProceed = 1 Then
    '                MessageBox.Alert(xMessage, "error", Me)
    '                Exit Sub
    '            Else
    '                SQLHelper.ExecuteDataSet("EPEReviewEvaluator_WebForApproval", UserNo, CType(lbl.Text, Integer), 1, "", CType(lblPECycle.Text, Integer))
    '                Count = Count + 1
    '            End If
    '        End If
    '    Next

    '    If Count > 0 Then
    '        PopulateGrid()
    '        MessageBox.Success("There are (" + Count.ToString + ")  transaction(s) submitted for approval.", Me)
    '    Else
    '        MessageBox.Information(MessageTemplate.NoSelectedTransaction, Me)
    '    End If

    'End Sub


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
            Dim ComponentNo As Integer = 3
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

    Protected Sub grdMain_CommandButtonInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewCommandButtonEventArgs) Handles grdMain.CommandButtonInitialize
        If e.ButtonType = ColumnCommandButtonType.SelectCheckbox Then
            Dim value As Boolean = Generic.ToInt(grdMain.GetRowValues(e.VisibleIndex, "IsEnabled"))
            e.Enabled = value
        End If
    End Sub


    Protected Sub lnkSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim _xds As New DataSet
        Dim IsProceed As Integer = 0
        Dim xMessage As String = ""

        Dim str As String = "", i As Integer = 0
        For j As Integer = 0 To grdMain.VisibleRowCount - 1
            If grdMain.Selection.IsRowSelected(j) Then
                Dim item As Integer = Generic.ToInt(grdMain.GetRowValues(j, "PEReviewEvaluatorNo"))
                Dim x As Integer = Generic.ToInt(grdMain.GetRowValues(j, "PECycleNo"))

                _xds = SQLHelper.ExecuteDataSet("EPEReview_WebValidate", UserNo, item)
                If _xds.Tables.Count > 0 Then
                    If _xds.Tables(0).Rows.Count > 0 Then
                        IsProceed = Generic.ToInt(_xds.Tables(0).Rows(0)("tProceed"))
                        xMessage = Generic.ToStr(_xds.Tables(0).Rows(0)("xMessage"))
                    End If
                End If

                If IsProceed = 1 Then
                    MessageBox.Alert(xMessage, "error", Me)
                    Exit Sub
                Else
                    SQLHelper.ExecuteDataSet("EPEReviewEvaluator_WebForApproval", UserNo, item, 1, "", x)
                    i = i + 1
                End If

                grdMain.Selection.UnselectRow(j)
            End If
        Next

        If i > 0 Then
            PopulateGrid()
            MessageBox.Success("(" + i.ToString + ") transaction(s) submitted for approval.", Me)
        Else
            MessageBox.Warning(MessageTemplate.NoSelectedTransaction, Me)
        End If

    End Sub

#End Region



End Class

