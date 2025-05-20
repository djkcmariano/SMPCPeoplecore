<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="RptList.aspx.vb" Inherits="Secured_RptList" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

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
                        <asp:Repeater runat="server" ID="rRef">        
                            <ItemTemplate>            
                                <div class="col-md-6">
                                    <i class="fa fa-caret-right"></i>&nbsp;<asp:LinkButton runat="server" ID="lnk" OnClick="lnk_Click" CommandArgument='<%# Eval("vbnetformname") & "|" & Eval("ReportTitle") & "|" & Eval("Reportname") & "|" & Eval("reportNo") & "|" & Eval("Datasource") %>' Text='<%# Bind("reporttitle") %>' style="display:inline-block; padding: 5px; font-size: 1.1em;" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div> 
                </div>
            </div>
       </div>
 </div>                

</asp:content>
