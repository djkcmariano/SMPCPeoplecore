Imports clsLib

Partial Class Include_Filter    
    Inherits System.Web.UI.UserControl

    Private _content As ITemplate = Nothing
    Private _enablecontent As Boolean = False
    Private _search As String = ""
    Public Event lnkSearchClick As EventHandler

    <TemplateContainer(GetType(TemplateControl))> _
    <PersistenceMode(PersistenceMode.InnerProperty)> _
    <TemplateInstance(TemplateInstance.[Single])> _
    Public Property Content() As ITemplate
        Get
            Return _content
        End Get
        Set(value As ITemplate)
            _content = value
        End Set
    End Property

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        If _content IsNot Nothing Then
            _content.InstantiateIn(PlaceHolder1)
        End If
        PlaceHolder2.Visible = _enablecontent
    End Sub

    Public Property EnableContent() As Boolean
        Get
            Return _enablecontent
        End Get
        Set(value As Boolean)
            _enablecontent = value
        End Set
    End Property

 
    Public ReadOnly Property SearchText() As String
        Get
            Return txtSearch.Text
        End Get
    End Property


    Protected Sub Search(sender As Object, e As EventArgs)
        RaiseEvent lnkSearchClick(sender, e)
    End Sub

End Class
