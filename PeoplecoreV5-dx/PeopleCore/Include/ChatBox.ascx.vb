Imports clsLib
Imports System.Data

Partial Class Include_ChatBox
    Inherits System.Web.UI.UserControl
    Dim UserNo As Integer
    Dim PayLocNo As Integer
    Private _ID As Integer
    Public Event lnkSendClick As EventHandler

    <TemplateContainer(GetType(TemplateControl))> _
<PersistenceMode(PersistenceMode.InnerProperty)> _
<TemplateInstance(TemplateInstance.[Single])> _
    Public Property xID() As Integer
        Get
            Return _ID
        End Get
        Set(value As Integer)
            _ID = value
        End Set
    End Property

    Private _ChatType As Integer
    Public Property xChatType() As Integer
        Get
            Return _ChatType
        End Get
        Set(value As Integer)
            _ChatType = value
        End Set
    End Property

    Public Sub Show()
        Dim yID As Integer = 0
        Dim yChatType As Integer = 0

        yID = xID
        yChatType = xChatType

        Populate(yID, yChatType)
        ModalPopupExtender1.Show()
    End Sub

    Private Sub Populate(ID As Integer, ChatType As Integer)

        Dim dt As DataTable
        dt = SQLHelper.ExecuteDataTable("EChatType_WebOne", UserNo, ChatType)
        For Each row As DataRow In dt.Rows
            lbl.Text = Generic.ToStr((row("ChatTypeDesc")))
        Next

        Dim ds As New DataSet
        ds = SQLHelper.ExecuteDataSet("EChat_Web", UserNo, ChatType, ID, PayLocNo)
        Dim dtChat As DataTable
        dtChat = ds.Tables(0).DefaultView.ToTable(True, "ChatNo", "EmployeeNo", "FullName", "Messages", "DateChat", "IsRight")

        pChat.Controls.Add(New LiteralControl("<div class='chat-message'>"))
        pChat.Controls.Add(New LiteralControl("<ul class='chat'>"))
        For Each rowChat As DataRow In dtChat.Rows
            If Generic.ToBol(rowChat("IsRight")) = True Then
                pChat.Controls.Add(New LiteralControl("<li class='left clearfix'>"))
                pChat.Controls.Add(New LiteralControl("<span class='chat-img pull-left'>"))

                Dim Imageleft As New Image
                Imageleft.ID = "Img" & Generic.ToInt((rowChat("ChatNo")))
                Imageleft.ImageUrl = "~/secured/frmShowImage.ashx?tNo=" & Generic.ToInt(rowChat("EmployeeNo")) & "&tIndex=2"
                pChat.Controls.Add(Imageleft)

                pChat.Controls.Add(New LiteralControl("</span>"))
                pChat.Controls.Add(New LiteralControl("<div class='chat-body clearfix'>"))
                pChat.Controls.Add(New LiteralControl("<div class='header'>"))
                pChat.Controls.Add(New LiteralControl("<strong class='primary-font'>" & rowChat("FullName") & "</strong>"))
                pChat.Controls.Add(New LiteralControl("<span class='pull-right text-muted'><i class='fa fa-clock-o'></i> " & rowChat("DateChat") & "</span>"))
                pChat.Controls.Add(New LiteralControl("</div>"))
                pChat.Controls.Add(New LiteralControl("<p>" & rowChat("Messages") & "</p>"))
                pChat.Controls.Add(New LiteralControl("</div>"))
                pChat.Controls.Add(New LiteralControl("</li>"))
            Else
                pChat.Controls.Add(New LiteralControl("<li class='right clearfix'>"))
                pChat.Controls.Add(New LiteralControl("<span class='chat-img pull-right'>"))

                'Image
                Dim ImageRight As New Image
                ImageRight.ID = "Img" & Generic.ToInt((rowChat("ChatNo")))
                ImageRight.ImageUrl = "~/secured/frmShowImage.ashx?tNo=" & Generic.ToInt(rowChat("EmployeeNo")) & "&tIndex=2"
                pChat.Controls.Add(ImageRight)

                pChat.Controls.Add(New LiteralControl("</span>"))
                pChat.Controls.Add(New LiteralControl("<div class='chat-body clearfix'>"))
                pChat.Controls.Add(New LiteralControl("<div class='header'>"))
                pChat.Controls.Add(New LiteralControl("<strong class='primary-font'>" & rowChat("FullName") & "</strong>"))
                pChat.Controls.Add(New LiteralControl("<span class='pull-right text-muted'><i class='fa fa-clock-o'></i> " & rowChat("DateChat") & "</span>"))
                pChat.Controls.Add(New LiteralControl("</div>"))
                pChat.Controls.Add(New LiteralControl("<p>" & rowChat("Messages") & "</p>"))
                pChat.Controls.Add(New LiteralControl("</div>"))
                pChat.Controls.Add(New LiteralControl("</li>"))
            End If

        Next
        pChat.Controls.Add(New LiteralControl("</ul>"))
        pChat.Controls.Add(New LiteralControl("</div>"))

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        UserNo = Generic.ToInt(Session("OnlineUserNo"))
        PayLocNo = Generic.ToInt(Session("xPayLocNo"))

    End Sub


    Public ReadOnly Property SendText() As String
        Get
            Return txtSend.Text
        End Get
    End Property

    Public Sub SendClear()

        txtSend.Text = ""

    End Sub


    Protected Sub lnkSend_Click(sender As Object, e As EventArgs)
        RaiseEvent lnkSendClick(sender, e)
    End Sub

End Class
