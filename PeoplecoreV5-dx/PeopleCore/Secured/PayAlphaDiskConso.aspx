<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayAlphaDiskConso.aspx.vb" Inherits="Secured_PayAlphaDiskConso" %>
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
                                    <label class="col-md-4 control-label has-required">Applicable Year :</label>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txtApplicableYear" SkinID="txtdate" runat="server" CssClass="form-control required"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-required">Schedule :</label>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="cboDiskettingTypeNo" CssClass="form-control required">
                                            <asp:ListItem Value="" Text="-- Select --" Selected="True" />
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

