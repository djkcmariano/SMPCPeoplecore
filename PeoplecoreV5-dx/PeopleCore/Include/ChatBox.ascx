<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ChatBox.ascx.vb" Inherits="Include_ChatBox" %>


 <!-- START CONTENT FRAME BODY -->
<asp:Button ID="Button2" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button2" PopupControlID="pInfomation" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
<asp:Panel id="pInfomation" runat="server" CssClass="entryPopup" style="display:none">      
        <div class="cf popupheader">
            <h4><asp:Label ID="lbl" runat="server"></asp:Label></h4>
            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />
        </div>
        <div class="entryPopupDetl" style="background: #f9f9f9;"> 
            <asp:Panel runat="server" ID="pChat" /> 
        </div>
        <div class="chat-box bg-white">
            <div class="input-group">
                <%--<span class="input-group-btn" >
                    <button class="btn btn-default" type="button"><span class="fa fa-camera"></span></button>
                </span>--%>
                <asp:TextBox runat="server" ID="txtSend" CssClass="form-control border no-shadow no-rounded" placeholder="Type your message here"></asp:TextBox>
            	<span class="input-group-btn">
                    <asp:Button runat="server" ID="lnkSend" CausesValidation="false" CssClass="btn btn-success no-rounded" OnClick="lnkSend_Click" Text="Send" /> 
            	</span>
            </div>
        </div>

</asp:Panel>

