<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Filter.ascx.vb" Inherits="Include_Filter" %>
<div class="row">
    <div class="col-md-4 pull-right">
        <div class="input-group">
            <asp:TextBox runat="server" ID="txtSearch" CssClass="form-control" placeholder="enter keyword"></asp:TextBox>
            <div class="input-group-btn">    
                <asp:Button runat="server" ID="lnkSearch" CausesValidation="false" CssClass="btn btn-default" OnClick="Search" ToolTip="Click here to search" Text="Go!" />            
                <asp:PlaceHolder runat="server" ID="PlaceHolder2">            
                    <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">&nbsp;<span class="caret"></span></button>
                    <div class="dropdown-menu dropdown-menu-right drp-menu-size">
                      <div class="form-horizontal">
                            <div class="panel-body">
                                <asp:PlaceHolder runat="server" ID="PlaceHolder1" />      
                            </div>       
                       </div>                                   
                    </div>            
                </asp:PlaceHolder>
            </div>
        </div>
    </div>
</div>