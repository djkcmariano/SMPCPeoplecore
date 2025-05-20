<%@ Control Language="VB" AutoEventWireup="false" CodeFile="FilterSearch.ascx.vb" Inherits="Include_FilterSearch" %>


<asp:HiddenField runat="server" ID="hifIndex" />
<div class="row">
    <div class="panel panel-default" style="margin-bottom:10px;">
        <div class="panel-heading">
            <div class="col-md-6">
                <div class="panel-title">
                     Filter
                </div>
            </div>
            <div class="col-md-6 pull-right">
                <form class="form-inline">
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" ID="cboSelectTopNo" CssClass="form-control">
                            <asp:ListItem Text="TOP 100" Value="TOP 100" Selected="True" />
                            <asp:ListItem Text="TOP 1K" Value="TOP 1000" />
                            <asp:ListItem Text="TOP 10K" Value="TOP 10000" />
                            <asp:ListItem Text="TOP 100K" Value="TOP 100000" />
                            <asp:ListItem Text="SHOW ALL" Value="" />
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <asp:TextBox runat="server" ID="txtSearch" CssClass="form-control" placeholder="enter keyword"></asp:TextBox>
                            <div class="input-group-btn">    
                                <asp:Button runat="server" ID="lnkSearch" CausesValidation="false" CssClass="btn btn-default" OnClick="Search" ToolTip="Click here to search" Text="Go!"  />            
                                <asp:PlaceHolder runat="server" ID="PlaceHolder3">   
                                    <button type="button" runat="server" causesvalidation="false" class="btn btn-default" id="lnkFilter" onserverclick="lnkFilter_Click" >&nbsp;<span class="fa fa-filter"></span></button>
                                </asp:PlaceHolder>
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

                </form>
            </div>
        </div>
        <div id="panelfilter" runat="server" class="panel-collapse collapse">
            <div class="panel-body">
                <div class="row pull-right" style="padding-top:5px;">
                    <asp:LinkButton runat="server" ID="lnkClearFilter" OnClick="lnkClearFilter_Click" Text="Clear Filter" CssClass="control-primary" />
                </div>
                <div class="row">
                    <div class="table-responsive">
                        <dx:ASPxFilterControl ID="ASPxFilterControl1" runat="server" Styles-GroupType-CssClass="ontop" />
                    </div>
                </div>
                
            </div>
        </div>
    </div>
</div>

