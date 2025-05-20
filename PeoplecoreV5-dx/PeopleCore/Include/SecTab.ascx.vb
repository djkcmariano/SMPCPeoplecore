Imports clsLib

Partial Class Include_SecTab
    Inherits System.Web.UI.UserControl

    Dim _transactionID As Integer = 0
    Dim menumassno As Integer = 0
    Dim tabno As Integer = 0
    Dim isgroup As Integer = 0



    Protected Sub lnkModule_Click(sender As Object, e As EventArgs)
        

        If isgroup = 1 Then
            Response.Redirect("SecMenuGroup_Module.aspx?id=" & _transactionID & "&menumassno=" & menumassno & "&tabno=1&isgroup=" & isgroup)
        Else
            Response.Redirect("SecMenuUser_Module.aspx?id=" & _transactionID & "&menumassno=" & menumassno & "&tabno=1&isgroup=" & isgroup)
        End If

    End Sub

    Protected Sub lnkReference_Click(sender As Object, e As EventArgs)
        

        If isgroup = 1 Then
            Response.Redirect("SecMenuGroup_Reference.aspx?id=" & _transactionID & "&menumassno=" & menumassno & "&tabno=2&isgroup=" & isgroup)
        Else
            Response.Redirect("SecMenuUser_Reference.aspx?id=" & _transactionID & "&menumassno=" & menumassno & "&tabno=2&isgroup=" & isgroup)
        End If

    End Sub

    Protected Sub lnkReport_Click(sender As Object, e As EventArgs)
        

        If isgroup = 1 Then
            Response.Redirect("SecMenuGroup_Report.aspx?id=" & _transactionID & "&menumassno=" & menumassno & "&tabno=3&isgroup=" & isgroup)
        Else
            Response.Redirect("SecMenuUser_Report.aspx?id=" & _transactionID & "&menumassno=" & menumassno & "&tabno=3&isgroup=" & isgroup)
        End If

    End Sub

    Protected Sub lnkAnalytics_Click(sender As Object, e As EventArgs)

        If isgroup = 1 Then
            Response.Redirect("SecMenuGroup_Analytics.aspx?id=" & _transactionID & "&menumassno=" & menumassno & "&tabno=4&isgroup=" & isgroup)
        Else
            Response.Redirect("SecMenuUser_Analytics.aspx?id=" & _transactionID & "&menumassno=" & menumassno & "&tabno=4&isgroup=" & isgroup)
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        _transactionID = Generic.ToInt(Request.QueryString("id"))
        menumassno = Generic.ToInt(Request.QueryString("menumassno"))
        tabno = Generic.ToInt(Request.QueryString("tabno"))
        isgroup = Generic.ToInt(Request.QueryString("isgroup"))

        PopulateTab()

    End Sub


    Private Sub PopulateTab()

        If tabno = 2 Then 'Reference
            lnk1.Attributes.Remove("class")
            lnk2.Attributes.Add("class", "active")
            lnk3.Attributes.Remove("class")
            lnk4.Attributes.Remove("class")
        ElseIf tabno = 3 Then 'Reports
            lnk1.Attributes.Remove("class")
            lnk2.Attributes.Remove("class")
            lnk3.Attributes.Add("class", "active")
            lnk4.Attributes.Remove("class")
        ElseIf tabno = 4 Then 'Analytics
            lnk1.Attributes.Remove("class")
            lnk2.Attributes.Remove("class")
            lnk3.Attributes.Remove("class")
            lnk4.Attributes.Add("class", "active")
        Else 'Sub Modules
            lnk1.Attributes.Add("class", "active")
            lnk2.Attributes.Remove("class")
            lnk3.Attributes.Remove("class")
            lnk4.Attributes.Remove("class")
        End If

    End Sub

End Class
