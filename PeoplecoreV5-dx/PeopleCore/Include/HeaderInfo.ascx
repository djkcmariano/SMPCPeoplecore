<%@ Control Language="VB" AutoEventWireup="false" CodeFile="HeaderInfo.ascx.vb" Inherits="Include_wucDTRHeader" %>


<style type="text/css">

@media (min-width: 768px) {
  .dl-horizontal dt { text-align: left; }
}

.container {
  background-color: white;
  float: left;
  padding: 5px;
  margin:0px;
}

.container-border 
{
  padding: 5px;
  margin:0px;
}

#container1 {
  width: 100%;
}
 
</style>


<%--<div class="row">
    <div id="container1" class="container">
          <asp:Repeater runat="server" ID="rRef">        
                <ItemTemplate>   
                   <div class="col-md-12 container-border">
                        <div class="col-md-6">
                            <asp:label ID="lblDisplay1" runat="server" Text='<%# Bind("Display1") %>' />
                        </div>
                        <div class="col-md-6">
                            <asp:label ID="lblDisplay2" runat="server" Text='<%# Bind("Display2") %>' />
                        </div>
                    </div> 
                </ItemTemplate>
            </asp:Repeater>
        </div>
</div>--%>


<div class="row">
  <div class="panel panel-default" style="margin-bottom:-5px;">
    <div class="panel-heading" style="padding:5px;">
            <div class="panel-title">
                <asp:Image runat="server" ID="imgPhoto" width="50" height="50" CssClass="img-circle" style="border: 2px solid white; padding:0px;margin:0px" />&nbsp;&nbsp;
                <a role="button" data-toggle="collapse" href="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                    <asp:label ID="lblName" runat="server" style="color:#069; cursor:pointer;" />
                </a>
            </div>            
    </div>
    <div id="collapseOne" class="panel-collapse collapse">
      <div id="container1" class="container">
          <asp:Repeater runat="server" ID="rRef">        
                <ItemTemplate>   
                   <div class="col-md-12 container-border">
                        <div class="col-md-6">
                            <asp:label ID="lblDisplay1" runat="server" Text='<%# Bind("Display1") %>' />
                        </div>
                        <div class="col-md-6">
                            <asp:label ID="lblDisplay2" runat="server" Text='<%# Bind("Display2") %>' />
                        </div>
                    </div> 
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
  </div>
</div>