Imports System.Data
Imports clsLib

Partial Class Include_wucFilterGeneric
    Inherits System.Web.UI.UserControl
    Dim sqlHelp As New clsBase.SQLHelper
    Dim xpublicVar As New clsPublicVariable



    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        xpublicVar.xOnlineUseNo = Generic.CheckDBNull(Session("Onlineuserno"), clsBase.clsBaseLibrary.enumObjectType.IntType)
        If Not IsPostBack Then
            populateFilterBy()

        End If
    End Sub
    Private Sub populateFilterBy()
        Try
            cbofilterby.DataSource = SQLHelper.ExecuteDataSet("xTable_Lookup", xpublicVar.xOnlineUseNo, "EFilteredBy", Session("xPayLocNo"), "", "")
            cbofilterby.DataTextField = "tDesc"
            cbofilterby.DataValueField = "tno"
            cbofilterby.DataBind()

        Catch ex As Exception
        End Try
    End Sub
    Protected Sub cbofilterby_SelectedIndexChanged(sender As Object, e As System.EventArgs) 'Handles cbofilterby.SelectedIndexChanged
        Try

            Dim clsGen As New clsGenericClass
            Dim ds As DataSet
            ds = clsGen.populateDropdownFilterByAll(Generic.CheckDBNull(Me.cbofilterby.SelectedValue, clsBase.clsBaseLibrary.enumObjectType.IntType), xpublicVar.xOnlineUseNo, Session("xPayLocNo"))
            cbofiltervalue.DataSource = ds
            cbofiltervalue.DataTextField = "tDesc"
            cbofiltervalue.DataValueField = "tNo"
            cbofiltervalue.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    


End Class
