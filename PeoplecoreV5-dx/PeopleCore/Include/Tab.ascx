<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab.ascx.vb" Inherits="Include_Tab" %>
<div class="page-content-wrap">         

     
<div class="col-md-12 bhoechie-tab-container">            
    <div class="col-md-2 bhoechie-tab-menu">
        <div style="padding:20px 10px;">
             <asp:PlaceHolder runat="server" ID="PlaceHolder3" />                                 
        </div>
        <div class="list-group">
            <asp:PlaceHolder runat="server" ID="PlaceHolder1" />
        </div>
    </div>
    <div class="col-md-10 bhoechie-tab" style=" border-left:1px solid #e5e5e5;">                        
        <asp:PlaceHolder runat="server" ID="PlaceHolder2" />
    </div>
</div>

</div>
