Imports System.Data
Imports clsLib
Partial Class Include_wucPETabGenericAppr
    Inherits System.Web.UI.UserControl

    Dim Lindex As Integer
    Dim xPublicVar As New clsPublicVariable
    Dim transNo As Integer = 0
    Dim INoDetail As Integer = 0
    Dim INoCate As Integer = 0
    Dim ApplicantNo As Integer = 0
    Dim MenuType As Integer = 0
    Dim IsClickMain As Integer = 0
    Dim clsArray As New clsBase.clsArray


    Private Sub ClearTabs(rowCount As Integer)
        Dim iCount As Integer = rowCount
        Dim i As Integer, lnk As New LinkButton
        For i = 1 To iCount
            lnk = CType(Me.FindControl("lnk" & i), LinkButton)
            If Not lnk Is Nothing Then
                lnk.Visible = False
            End If
        Next
    End Sub
    Public Sub ShowTabs(ByVal lnk As LinkButton)
        lnk.Visible = True
    End Sub

    Private Sub PopulateFormMain()
        Dim _ds As New DataSet, tcount As Integer
        Dim dcount As Integer = 0
        _ds = SQLHelper.ExecuteDataSet("EMenuPerformanceTab", xPublicVar.xOnlineUseNo, 3, transNo, INoDetail)
        If _ds.Tables.Count > 0 Then
            If _ds.Tables(0).Rows.Count > 0 Then
                dcount = _ds.Tables(0).Rows.Count
                ClearTabs(dcount)
                For tcount = 1 To dcount
                    Dim lnk As New LinkButton
                    Dim lbl As New Label
                    lnk.ID = "lnk" & tcount
                    lbl.ID = "lbl" & tcount
                    lbl.Width = 2
                    If Not lnk Is Nothing Then
                        lnk.Text = UCase(_ds.Tables(0).Rows(tcount - 1)("MenuTitle"))
                        lnk.ToolTip = tcount
                        lnk.CausesValidation = False

                        clsArray.myMenu(tcount).xMenuTitle = UCase(Generic.CheckDBNull(_ds.Tables(0).Rows(tcount - 1)("MenuTitle"), clsBase.clsBaseLibrary.enumObjectType.StrType))
                        clsArray.myMenu(tcount).xMenuType = UCase(Generic.CheckDBNull(_ds.Tables(0).Rows(tcount - 1)("MenuType"), clsBase.clsBaseLibrary.enumObjectType.StrType))
                        clsArray.myMenu(tcount).xTablename = UCase(Generic.CheckDBNull(_ds.Tables(0).Rows(tcount - 1)("Tablename"), clsBase.clsBaseLibrary.enumObjectType.StrType))
                        clsArray.myMenu(tcount).xFormName = UCase(Generic.CheckDBNull(_ds.Tables(0).Rows(tcount - 1)("Formname"), clsBase.clsBaseLibrary.enumObjectType.StrType))

                    End If

                    lnk.Style.Add("font-size", "11px")

                    If Generic.CheckDBNull(Session(Session("xFormname") & "Post"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType) = tcount Then
                        'lnk.CssClass = "active"
                        lnk.Style.Add("background-color", "#fff")
                        lnk.Style.Add("color", "#444444")
                        lnk.Style.Add("border-left", "2px solid #C0C0C0")
                        lnk.Style.Add("border-right", "2px solid #C0C0C0")
                        lnk.Style.Add("border-top", "2px solid #C0C0C0")
                        lnk.Style.Add("border-bottom", "0px solid transparent")
                        lnk.Style.Add("padding-left", "10px")
                        lnk.Style.Add("padding-right", "10px")
                        lnk.Style.Add("padding-top", "5px")
                        lnk.Style.Add("padding-bottom", "2px")
                        lnk.Style.Add("margin-left", "0px")
                        lnk.Style.Add("margin-right", "2px")
                        lnk.Style.Add("margin-bottom", "-2px")
                        lnk.Style.Add("min-width", "30px")
                        lnk.Style.Add("text-align", "center")

                        lnk.Height = "16"
                    Else
                        'lnk.CssClass = ""

                        lnk.Style.Add("background-color", "#444444")
                        lnk.Style.Add("color", "#fff")
                        lnk.Style.Add("border-left", "1px solid #C0C0C0")
                        lnk.Style.Add("border-right", "1px solid #C0C0C0")
                        lnk.Style.Add("border-top", "1px solid #C0C0C0")
                        lnk.Style.Add("border-bottom", "1px solid #444444")
                        lnk.Style.Add("padding-left", "10px")
                        lnk.Style.Add("padding-right", "10px")
                        lnk.Style.Add("padding-top", "2px")
                        lnk.Style.Add("padding-bottom", "1px")
                        lnk.Style.Add("margin-left", "0px")
                        lnk.Style.Add("margin-right", "1px")
                        lnk.Style.Add("margin-bottom", "-1px")
                        lnk.Style.Add("margin-top", "-4px")
                        lnk.Style.Add("text-align", "center")
                        lnk.Style.Add("min-width", "30px")

                        lnk.Height = "14"
                    End If


                    AddHandler lnk.Click, AddressOf lnk_Click
                    pnl.Controls.Add(lnk)
                    pnl.Controls.Add(lbl)


                Next



            End If
        End If

    End Sub

    Protected Sub lnk_Click(sender As Object, e As System.EventArgs)
        Dim lnk_Click As New LinkButton
        lnk_Click = sender

        Session("xFormname") = clsArray.myMenu(lnk_Click.ToolTip).xFormName
        Session("xTablename") = clsArray.myMenu(lnk_Click.ToolTip).xTablename
        INoCate = clsArray.myMenu(lnk_Click.ToolTip).xMenuType
        Session(Session("xFormname") & "Post") = lnk_Click.ToolTip
        Response.Redirect("~/securedmanager/" & clsArray.myMenu(lnk_Click.ToolTip).xFormName & "?INo=" & Generic.CheckDBNull(transNo, Global.clsBase.clsBaseLibrary.enumObjectType.IntType) & "&INoDetail=" & Generic.CheckDBNull(INoDetail, Global.clsBase.clsBaseLibrary.enumObjectType.IntType) & "&INoCate=" & Generic.CheckDBNull(INoCate, Global.clsBase.clsBaseLibrary.enumObjectType.IntType) & "&tModify=false&tabOrder=" & lnk_Click.ToolTip & "&IsClickMain=1&IsClickTab=1")

    End Sub

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        xPublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("onlineuserno"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        transNo = Generic.CheckDBNull(Request.QueryString("INo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        INoDetail = Generic.CheckDBNull(Request.QueryString("INoDetail"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        INoCate = Generic.CheckDBNull(Request.QueryString("INoCate"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        IsClickMain = Generic.CheckDBNull(Request.QueryString("IsClickMain"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Lindex = Generic.CheckDBNull(Session(Session("xFormname") & "Post"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)

        If transNo = 0 Then
            transNo = Generic.CheckDBNull(Session(Session("xFormname") & "transNo"), Global.clsBase.clsBaseLibrary.enumObjectType.IntType)
        Else
            Session(Session("xFormname") & "transNo") = transNo
        End If
        If Lindex <= 0 Then
            Lindex = 1
            Session(Session("xFormname") & "Post") = Lindex
        End If

        PopulateFormMain()
    End Sub
End Class
