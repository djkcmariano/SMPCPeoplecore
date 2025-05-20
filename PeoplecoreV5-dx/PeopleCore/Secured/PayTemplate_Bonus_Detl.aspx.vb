Imports System.Data
Imports clsLib
Imports System.Math
Imports DevExpress.Web
Imports DevExpress.Export
Imports DevExpress.XtraPrinting
Imports Microsoft.VisualBasic.FileIO
Imports System.IO
Partial Class Secured_PayTemplate_Bonus_Detl
    Inherits System.Web.UI.Page

    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Dim TransNo As Integer
    Dim PayCateNo As Integer


    Private Sub PopulateGrid(Optional IsMain As Boolean = False)
        Dim _dt As DataTable
        _dt = SQLHelper.ExecuteDataTable("EPayTemplateBonusDeti_Web", UserNo, TransNo)
        Me.grdMain.DataSource = _dt
        Me.grdMain.DataBind()

    End Sub
    Private Sub PopulateData(Id As Integer)
        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EPayTemplateBonusDeti_WebOne", UserNo, Id)
        Generic.PopulateData(Me, "Panel2", dt)
        
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("id"))
        Permission.IsAuthenticatedCoreUser()
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "Panel2", PayLocNo)
        End If
        PopulateGrid()

    End Sub



    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        Generic.ClearControls(Me, "Panel2")
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"PayTemplateBonusNo"})
        Dim i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("EPayTemplateBonus", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
    End Sub

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)

        Dim lnk As New LinkButton, IsEnabled As Boolean = False
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        Generic.ClearControls(Me, "Panel2")
        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"PayTemplateBonusDetiNo"})))
        ModalPopupExtender1.Show()

    End Sub
   

    Protected Sub lnkSave_Click(sender As Object, e As EventArgs)

        Dim Retval As Boolean = False
        Dim sfrom As Double = Generic.ToDbl(txtSFrom.Text)
        Dim sto As Double = Generic.ToDbl(txtSTo.Text)
        Dim percentFactor As Double = Generic.ToDbl(txtPercentFactor.Text)
        '//validate start here
        Dim invalid As Boolean = True, messagedialog As String = "", alerttype As String = ""
        Dim dtx As New DataTable


        Dim dt As DataTable, error_num As Integer = 0, error_message As String = ""
        dt = SQLHelper.ExecuteDataTable("EPayTemplateBonusDeti_WebSave", UserNo, Generic.ToInt(txtPayTemplateBonusDetiNo.Text), Generic.ToInt(TransNo), Generic.ToInt(sfrom), _
                                      Generic.ToInt(sto), Generic.ToDec(percentFactor))
        For Each row As DataRow In dt.Rows
            Retval = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                Retval = False
            End If

        Next
        If Retval = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If Retval = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub



    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try

    End Sub

End Class








