Imports System.Data
Imports clsLib
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.Web
Partial Class Secured_BSProjectList_ClassRateDetl
    Inherits System.Web.UI.Page

    Dim UserNo As Integer = 0
    Dim PayLocNo As Integer = 0
    Dim TransNo As Integer = 0
    Dim ProjectNo As Integer = 0

    Protected Sub PopulateData_Rate()
        Dim ds As DataSet
        ds = SQLHelper.ExecuteDataSet("BBSProjectClassRate_WebOne", UserNo, TransNo)
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                lblDepartmentDesc.Text = Generic.ToStr(ds.Tables(0).Rows(0)("DepartmentDesc"))
                lblPositionDesc.Text = Generic.ToStr(ds.Tables(0).Rows(0)("PositionDesc"))
                lblPayClassDesc.Text = Generic.ToStr(ds.Tables(0).Rows(0)("payclassDesc"))
                lblProjectDesc.Text = Generic.ToStr(ds.Tables(0).Rows(0)("ProjectDesc"))
            End If
        End If
    End Sub
    Protected Sub PopulateGrid()
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("BBSProjectClassRateDeti_Web", UserNo, TransNo)
            grdMain.DataSource = dt
            grdMain.DataBind()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub PopulateData(id As Integer)
        Try
            Dim dt As DataTable
            dt = SQLHelper.ExecuteDataTable("BBSProjectClassRateDeti_WebOne", UserNo, id)
            For Each row As DataRow In dt.Rows
                Generic.PopulateData(Me, "pnlPopupDetl", dt)
            Next
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lnkExport_Click(sender As Object, e As EventArgs)
        Try
            grdExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
        Catch ex As Exception
            MessageBox.Warning("Error exporting to excel file.", Me)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))
        TransNo = Generic.ToInt(Request.QueryString("BSProjectClassRateNo"))
        ProjectNo = Generic.ToInt(Request.QueryString("Id"))
        If Not IsPostBack Then
            Generic.PopulateDropDownList(UserNo, Me, "pnlPopupDetl", PayLocNo)
        End If
        PopulateData_Rate()
        PopulateGrid()
        Generic.PopulateDXGridFilter(grdMain, UserNo, PayLocNo)

        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1)) : Response.Cache.SetCacheability(HttpCacheability.NoCache) : Response.Cache.SetNoStore()
    End Sub

    
 

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        SaveRecord()
    End Sub
    Private Sub SaveRecord()
        Dim dt As DataTable, RetVal As Boolean, error_num As Integer, error_message As String = ""
        Dim description As String = Generic.ToStr(txtDescription.Text)
        Dim Rate As Double = Generic.ToDec(txtRate.Text)
        Dim OtRate As Double = Generic.ToDec(txtOTRate.Text)

        dt = SQLHelper.ExecuteDataTable("BBSProjectClassRateDeti_WebSave", UserNo, Generic.ToInt(txtCode.Text), Generic.ToInt(TransNo), Generic.ToInt(cboBSCompTypeNo.SelectedValue), description, Rate, OtRate)
        For Each row As DataRow In dt.Rows
            RetVal = True
            error_num = Generic.ToInt(row("Error_num"))
            If error_num > 0 Then
                error_message = Generic.ToStr(row("ErrorMessage"))
                MessageBox.Critical(error_message, Me)
                RetVal = False
            End If
        Next
        If RetVal = False And error_message = "" Then
            MessageBox.Critical(MessageTemplate.ErrorSave, Me)
        End If
        If RetVal = True Then
            PopulateGrid()
            MessageBox.Success(MessageTemplate.SuccessSave, Me)
        End If
    End Sub
    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowEdit) Then
        Dim lnk As New LinkButton
        lnk = sender
        Dim container As GridViewDataItemTemplateContainer = TryCast(lnk.NamingContainer, GridViewDataItemTemplateContainer)
        PopulateData(Generic.ToInt(container.Grid.GetRowValues(container.VisibleIndex, New String() {"BSProjectClassRateDetiNo"})))
        mdlDetl.Show()
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedEdit, Me)
        'End If
    End Sub


    Protected Sub lnkAdd_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowAdd) Then
        Generic.ClearControls(Me, "pnlPopupDetl")
        mdlDetl.Show()
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedAdd, Me)
        'End If

    End Sub
  
    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs)
        'If AccessRights.IsAllowUser(UserNo, AccessRights.EnumPermissionType.AllowDelete) Then
        Dim fieldValues As List(Of Object) = grdMain.GetSelectedFieldValues(New String() {"BSProjectClassRateDetiNo"})
        Dim str As String = "", i As Integer = 0
        For Each item As Integer In fieldValues
            Generic.DeleteRecordAudit("BBSProjectClassRateDeti", UserNo, item)
            i = i + 1
        Next
        MessageBox.Success("(" + i.ToString + ") " + MessageTemplate.SuccessDelete, Me)
        PopulateGrid()
        'Else
        'MessageBox.Warning(MessageTemplate.DeniedDelete, Me)
        'End If
    End Sub
   
 
  



End Class


