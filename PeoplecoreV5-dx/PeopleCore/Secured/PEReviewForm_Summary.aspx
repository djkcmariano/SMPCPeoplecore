<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="PEReviewForm_Summary.aspx.vb" Inherits="Secured_PEReviewForm_Summary" %>

<%@ Register Src="~/Include/PEReviewTab.ascx" TagName="PEReviewTab" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="row" style="margin-bottom:5px;">
    <div class="col-md-10">
        <h5 style="padding-top:5px;" class="text-primary">Review No. <asp:label ID="lblCode" runat="server" /></h5>
    </div>
    <div class="col-md-2">
        <div class="btn-group pull-right" id="DivSettings" runat="server">
          <button type="button" class="btn btn-default btn-sm dropdown-toggle" data-toggle="dropdown">&nbsp;<span class="fa fa-cog" style="padding-top:5px;"></span></button>
          <ul class="dropdown-menu">
           <%-- <li><asp:LinkButton runat="server" ID="lnkCycle" OnClick="lnkCycle_Click" >Settings</asp:LinkButton></li>
            <li><asp:LinkButton runat="server" ID="lnkEval" OnClick="lnkCycle_Click" Visible="false">Evaluator</asp:LinkButton></li>--%>
          </ul>
        </div>
    </div>
</div>
<div class="row">
  <div class="panel panel-default" style="margin-bottom:0px;">
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

<div class="panel panel-default">
<asp:Panel runat="server" ID="pHeader" />

<uc:PEReviewTab runat="server" ID="PEReviewTab">

<content>
    <asp:Panel runat="server" ID="Panel1">        
            <br /><br />            
            <fieldset class="form" id="fsMain">
                <div  class="form-horizontal">
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label">Transaction No :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtPEReviewMainNo" CssClass="form-control" runat="server" ></asp:Textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Transaction No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control" Placeholder="Autonumber" ></asp:Textbox>
                         </div>
                    </div>

                    
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Average Rating :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtAveRating" runat="server" CssClass="form-control"></asp:Textbox>
                        </div>
                    </div>

                    
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-default submit fsMain" Text="Generate PIP" />
                                                   
                        </div>
                    </div>

                    <br /><br />                     
                </div>                                                
            </fieldset>
        </asp:Panel>
    </content>
</uc:PEReviewTab>
</div>
</asp:Content>