<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ConfirmBox.ascx.vb" Inherits="Include_ConfirmBox" %>


<style type="text/css">

.message-box.message-box-post .mb-container{ 
     background: rgba(63, 81, 181, 0.9);
 }
 
.message-box.message-box-process .mb-container{    
     background: rgba(0, 150, 136, 0.9);
 }
 
 .message-box.message-box-pink .mb-container{    
     background: rgba(233, 30, 99, 0.9);
 }
 
 .message-box.message-box-purple .mb-container{    
     background: rgba(156, 39, 176, 0.9);
 }
 
 .message-box.message-box-deep .mb-container{    
     background: rgba(103, 58, 183, 0.9);
 }
 
</style>


<ajaxToolkit:ConfirmButtonExtender ID="cbe" runat="server" DisplayModalPopupID="mpe" />               
<ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pConfirmBox" OkControlID = "btnYes" CancelControlID="btnNo" />                
<asp:Panel ID="pConfirmBox" runat="server" Style="display: none">
<div class="message-box animated fadeIn open" id="alerttype" runat="server">
    <div class="mb-container">
        <div class="mb-middle">
            <div class="mb-title">
                <span class="fa fa-question"></span>
                <div class="pull-left"> 
                    Confirm
                </div>
            </div>
            <div class="mb-content">    
                <div class="pull-left"> 
                    <asp:Label runat="server" ID="lblMessage" />
                </div>
            </div>
            <div class="mb-footer">
                <div class="pull-right">                                                
                    <asp:Button runat="server" CssClass="btn btn-success btn-lg" ID="btnYes" Text="Yes" />
                    <asp:Button runat="server" CssClass="btn btn-default btn-lg" ID="btnNo" Text="Cancel" />                    
                </div>
            </div>                    
        </div>
    </div>
</div>                   
</asp:Panel>