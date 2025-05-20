<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="frmPolicy.aspx.vb" Inherits="SecuredSelf_frmPolicy" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="col-md-2">
                        
                    </div>
                    <div>
                        <uc:Filter runat="server" ID="Filter1" EnableContent="false">
                            <Content>
                            </Content>
                        </uc:Filter>
                    </div>                           
                </div>
                <div class="panel-body">
                    <div class="row">
                        <asp:Repeater runat="server" ID="rDoc">        
                            <ItemTemplate>            
                                <div class="col-md-4">
                                    <asp:Label runat="server" ID="lbl" CssClass='<%# Eval("CSSClass") & " fa-2x" %>'  /></i>&nbsp;<asp:LinkButton runat="server" ID="lnk" OnClick="lnk_Click" CommandArgument='<%# Bind("DocNo") %>' Text='<%# Bind("DocDesc") %>' OnPreRender="lnk_PreRender" style="display:inline-block; padding: 10px; font-size: 1.1em;" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div> 
                </div>
            </div>
       </div>
 </div>    
    
</asp:Content>

