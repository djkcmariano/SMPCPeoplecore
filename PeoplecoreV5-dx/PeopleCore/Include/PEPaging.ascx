<%@ Control Language="VB" AutoEventWireup="false" CodeFile="PEPaging.ascx.vb" Inherits="Include_PEPaging" %>


<div class="page-content-wrap">    
    <div>
        <div class="pull-left">
            <asp:Label runat="server" ID="lblPage"></asp:Label>
        </div>     
        <div class="dataTables_paginate">
            <asp:Button ID="lnkPrevious" runat="server" CssClass="paginate_enabled_previous" Text="Previous" OnClick="lnk_Previous"></asp:Button>
            <asp:PlaceHolder runat="server" ID="PlaceHolder1" />
            <asp:Button ID="lnkNext" runat="server" CssClass="paginate_enabled_next" Text="Next" OnClick="lnk_Next"></asp:Button>
        </div>
    </div>
</div>
