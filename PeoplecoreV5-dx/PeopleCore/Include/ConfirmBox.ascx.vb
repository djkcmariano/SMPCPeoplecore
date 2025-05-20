Imports AjaxControlToolkit
Imports System.ComponentModel

Partial Class Include_ConfirmBox
    Inherits System.Web.UI.UserControl

    Public WriteOnly Property TargetControlID() As String
        Set(value As String)            
            Me.cbe.TargetControlID = value
            Me.mpe.TargetControlID = value
        End Set
    End Property

    Public WriteOnly Property ConfirmMessage() As String
        Set(value As String)
            Me.lblMessage.Text = value
        End Set
    End Property

    Public WriteOnly Property MessageType() As String
        Set(value As String)

            If value = "Post" Then
                alerttype.Attributes.Add("class", "message-box message-box-post animated fadeIn open")
            End If

            If value = "Process" Then
                alerttype.Attributes.Add("class", "message-box message-box-process animated fadeIn open")
            End If

            If value = "Pink" Then
                alerttype.Attributes.Add("class", "message-box message-box-pink animated fadeIn open")
            End If

            If value = "Purple" Then
                alerttype.Attributes.Add("class", "message-box message-box-purple animated fadeIn open")
            End If

            If value = "Deep" Then
                alerttype.Attributes.Add("class", "message-box message-box-deep animated fadeIn open")
            End If

        End Set
    End Property
   
End Class
