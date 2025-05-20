<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayAlphaDisk.aspx.vb" Inherits="Secured_PayAlphaDisk" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content id="Content3" contentplaceholderid="cphBody" runat="server">



<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="row">
                    <asp:Panel id="pnlPopup" runat="server">
                        <fieldset class="form" id="fsMain">
                            <div  class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Filter By :</label>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="cboFilterbyNo" DataMember="EFilteredByAll" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cboFilterbyNo_SelectedIndexChanged" ></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group" >
                                    <label class="col-md-4 control-label has-space">Filter Value :</label>
                                    <div class="col-md-5">
                                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control" style="display:inline-block;" Placeholder="Type here..." /> 
                                        <asp:HiddenField runat="server" ID="hiffilterbyid"/>
                                        <ajaxToolkit:AutoCompleteExtender ID="drpAC" runat="server"  
                                        TargetControlID="txtName" MinimumPrefixLength="2" 
                                        CompletionInterval="250" ServiceMethod="populateDataDropdown" CompletionSetCount="0"
                                        CompletionListCssClass="autocomplete_completionListElement" 
                                        CompletionListItemCssClass="autocomplete_listItem" 
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"  OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" />
                                         <script type="text/javascript">
                                             function getRecord(source, eventArgs) {
                                                 document.getElementById('<%= hiffilterbyid.ClientID %>').value = eventArgs.get_value();
                                             }
                                         </script>
                    
                                    </div>
                                </div>


                                <div class="form-group">
                                    <label class="col-md-4 control-label has-required">Transaction No. :</label>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="cboTransNo" runat="server" CssClass="form-control required"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-required">Schedule :</label>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="cboDiskettingTypeNo" CssClass="form-control required">
                                            <asp:ListItem Value="" Text="-- Select --" Selected="True" />
<%--                                            <asp:ListItem Value="1" Text="7.1 - Separated Employees" />
                                            <asp:ListItem Value="2" Text="7.3 - Active Employees" />
                                            <asp:ListItem Value="3" Text="7.4 - Active Employees With Previous Employer" />
                                            <asp:ListItem Value="4" Text="7.5 - Minium Wage Earner" />--%>
                                            <asp:ListItem Value="5" Text="Scheduled 1 - Alphalist of Taxable Employees" />
                                            <asp:ListItem Value="6" Text="Scheduled 2 - Alphalist of Minimum Wage Earners" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space"></label>
                                    <div class="col-md-5">    
                                        <asp:Button runat="server" ID="lnkPreview" CssClass="btn btn-primary submit fsMain" Text="Preview" OnClick="lnkPreview_Click" />                           
                                        <asp:Button runat="server" ID="lnkCreate" CssClass="btn btn-primary submit fsMain" Text="Create Disk" OnClick="lnkCreate_Click" OnPreRender="lnkPrint_PreRender" />                   
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
            <rsweb:ReportViewer ID="rviewer" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" Height="543px" ShowPrintButton="true" >
            </rsweb:ReportViewer>
    </div>

</div>

</asp:Content> 

