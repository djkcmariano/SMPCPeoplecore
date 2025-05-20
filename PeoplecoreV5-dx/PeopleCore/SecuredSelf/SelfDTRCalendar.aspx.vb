Imports System.Data
Imports System.Math
Imports System.Threading
Imports Microsoft.VisualBasic
Imports clsLib

Partial Class SecuredSelf_SelfDTRCalendar
    Inherits System.Web.UI.Page

    Dim UserNo As Int64 = 0
    Dim TransNo As Int64 = 0
    Dim PayLocNo As Int64 = 0
    Dim rowno As Integer = 0
    Dim tabOrder As Integer = 0
    Dim ds As New DataSet

    Private Function getMonth(tmonth As Integer) As String
        Select Case tmonth
            Case 1
                Return "January"
            Case 2
                Return "February"
            Case 3
                Return "March"
            Case 4
                Return "April"
            Case 5
                Return "May"
            Case 6
                Return "June"
            Case 7
                Return "July"
            Case 8
                Return "August"
            Case 9
                Return "September"
            Case 10
                Return "October"
            Case 11
                Return "November"
            Case 12
                Return "December"

        End Select
    End Function

    Private Sub xprevious()
        ViewState("ApplicableMonth") = Generic.ToInt(cboMonthNo.SelectedValue) - 1

        If ViewState("ApplicableMonth") <= 0 Then
            ViewState("ApplicableMonth") = 12
            ViewState("ApplicableYear") = Generic.ToInt(txtYear.Text) - 1
        End If



        PopulateSchedule(ViewState("ApplicableMonth"), ViewState("ApplicableYear"))
    End Sub
    Private Sub xNext()
        ViewState("ApplicableMonth") = Generic.ToInt(cboMonthNo.SelectedValue) + 1
        If ViewState("ApplicableMonth") >= 13 Then
            ViewState("ApplicableMonth") = 1
            ViewState("ApplicableYear") = Generic.ToInt(txtYear.Text) + 1
        End If
        PopulateSchedule(ViewState("ApplicableMonth"), ViewState("ApplicableYear"))
    End Sub

    Private Sub PopulateSchedule(fmonth As Integer, fYear As Integer)
        Dim statusno As Integer

        'If Me.rdoTrnTitleNo1.Checked Then
        '    statusno = 1
        'ElseIf Me.rdoTrnTitleNo2.Checked Then
        '    statusno = 2
        'ElseIf Me.rdoTrnTitleNo3.Checked Then
        '    statusno = 3
        'Else
        '    statusno = 0
        '    Me.rdoTrnTitleNo0.Checked = True
        'End If



        ds = SQLHelper.ExecuteDataSet("EEmployee_WebCalendarSelf", UserNo, fmonth, fYear, statusno)
        grdCal.DataSource = ds
        grdCal.DataBind()

        lblMonth.Text = getMonth(fmonth) & ", " & fYear.ToString

        cboMonthNo.Text = fmonth

        txtYear.Text = fYear

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

        Permission.IsAuthenticated()

        If Not IsPostBack Then
            Dim tmonth As Integer = Now().Month
            Dim tYear As Integer = Now().Year

            ViewState("applicableMonth") = tmonth
            ViewState("ApplicableYear") = tYear

            PopulateDropDown()
            PopulateSchedule(tmonth, tYear)

        End If

        AddHandler Filter1.lnkSearchClick, AddressOf lnkSearch_Click

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()

    End Sub

    Private Sub PopulateDropDown()

        Try
            cboMonthNo.DataSource = SQLHelper.ExecuteDataSet("xTable_Lookup", UserNo, "EMonth", PayLocNo, "", "")
            cboMonthNo.DataValueField = "tNo"
            cboMonthNo.DataTextField = "tDesc"
            cboMonthNo.DataBind()
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub grdCal_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCal.RowDataBound
        Dim i As Integer = 0
        If e.Row.RowType = DataControlRowType.DataRow Then
            'grdCal.Columns(1).ItemStyle.CssClass = "bb"
            i = e.Row.DataItemIndex

            'grdCal.Columns(1).ItemStyle.CssClass = Generic.CheckDBNull(ds.Tables(0).Rows(i)("BackColor1"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            'grdCal.Columns(2).ItemStyle.CssClass = Generic.CheckDBNull(ds.Tables(0).Rows(i)("BackColor2"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            'grdCal.Columns(3).ItemStyle.CssClass = Generic.CheckDBNull(ds.Tables(0).Rows(i)("BackColor3"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            'grdCal.Columns(4).ItemStyle.CssClass = Generic.CheckDBNull(ds.Tables(0).Rows(i)("BackColor4"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            'grdCal.Columns(5).ItemStyle.CssClass = Generic.CheckDBNull(ds.Tables(0).Rows(i)("BackColor5"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            'grdCal.Columns(6).ItemStyle.CssClass = Generic.CheckDBNull(ds.Tables(0).Rows(i)("BackColor6"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            'grdCal.Columns(7).ItemStyle.CssClass = Generic.CheckDBNull(ds.Tables(0).Rows(i)("BackColor7"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            e.Row.Cells(1).CssClass = Generic.CheckDBNull(ds.Tables(0).Rows(i)("BackColor1"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            e.Row.Cells(2).CssClass = Generic.CheckDBNull(ds.Tables(0).Rows(i)("BackColor2"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            e.Row.Cells(3).CssClass = Generic.CheckDBNull(ds.Tables(0).Rows(i)("BackColor3"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            e.Row.Cells(4).CssClass = Generic.CheckDBNull(ds.Tables(0).Rows(i)("BackColor4"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            e.Row.Cells(5).CssClass = Generic.CheckDBNull(ds.Tables(0).Rows(i)("BackColor5"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            e.Row.Cells(6).CssClass = Generic.CheckDBNull(ds.Tables(0).Rows(i)("BackColor6"), clsBase.clsBaseLibrary.enumObjectType.StrType)
            e.Row.Cells(7).CssClass = Generic.CheckDBNull(ds.Tables(0).Rows(i)("BackColor7"), clsBase.clsBaseLibrary.enumObjectType.StrType)

            'Dim chkMon As New CheckBox
            'chkMon = e.Row.FindControl("chkMon")
            'Dim ii As String
            'If chkMon.Checked Then
            '    ii = 1
            'Else
            '    ii = 0
            'End If
            'chkMon.Attributes.Add("onclick", "javascript:return rownoMon('" + e.Row.RowIndex.ToString + "','" + ii + "')")

        End If
    End Sub

    Protected Sub chk_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chk As New CheckBox
        Dim i As String = "", ApprovalStatno As Integer = 0
        Dim rowNo As Integer = 0
        chk = sender
        Dim gvrow As GridViewRow = DirectCast(chk.NamingContainer, GridViewRow)
        rowNo = gvrow.RowIndex
        grdCal.Rows(rowNo).Cells(1).CssClass = "dg"

    End Sub

    Protected Sub lnkPrevious_Click(sender As Object, e As System.EventArgs)
        xprevious()
    End Sub

    Protected Sub lnkNext_Click(sender As Object, e As System.EventArgs)
        xNext()
    End Sub

    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            ViewState("applicableMonth") = Generic.CheckDBNull(cboMonthNo.Text, clsBase.clsBaseLibrary.enumObjectType.IntType)
            ViewState("ApplicableYear") = Generic.CheckDBNull(txtYear.Text, clsBase.clsBaseLibrary.enumObjectType.IntType)

            PopulateSchedule(ViewState("applicableMonth"), ViewState("ApplicableYear"))
        Catch ex As Exception
        End Try

    End Sub


    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim i As Integer

            Dim ib As New ImageButton
            ib = sender
            i = Generic.ToInt(ib.CommandArgument)
            Response.Redirect(Generic.GetFirstTab(i))

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub lnkAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try


            Dim lnk As New ImageButton
            Dim i As Integer = 0

            Response.Redirect(Generic.GetFirstTab(i))

        Catch ex As Exception
        End Try
    End Sub


End Class

















