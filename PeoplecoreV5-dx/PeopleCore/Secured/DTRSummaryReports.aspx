<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DTRSummaryReports.aspx.vb" Inherits="Secured_DTRSummaryReports" %>
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
                    <label class="col-md-4 control-label has-required">Start Date :</label>
                    <div class="col-md-5">
                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control required" Placeholder="From"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                        TargetControlID="txtStartDate"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                        TargetControlID="txtStartDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" 
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  
                        />
                                                                        
                        <asp:RangeValidator
                        ID="RangeValidator3"
                        runat="server"
                        ControlToValidate="txtStartDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />
                                                                        
                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                        ID="ValidatorCalloutExtender6"
                        TargetControlID="RangeValidator3"
                        />  
                         </div>
                        </div>


                   <div class="form-group">
                    <label class="col-md-4 control-label has-required">End Date :</label>
                     <div class="col-md-5">
                     <asp:TextBox ID="txtEndDate" runat="server"  CssClass="form-control required" Placeholder="To"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                        TargetControlID="txtEndDate"
                        Format="MM/dd/yyyy" />  
                                                                          
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtEndDate"
                        Mask="99/99/9999"
                        MessageValidatorTip="true"
                        OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError"
                        MaskType="Date"
                        DisplayMoney="Left"
                        AcceptNegative="Left" 
                        ErrorTooltipEnabled ="true" 
                        ClearTextOnInvalid="true"  
                        />
                                                                        
                     <asp:RangeValidator
                        ID="RangeValidator4"
                        runat="server"
                        ControlToValidate="txtEndDate"
                        ErrorMessage="<b>Please enter valid entry</b>"
                        MinimumValue="1900-01-01"
                        MaximumValue="3000-12-31"
                        Type="Date" Display="None"  />

                        <ajaxToolkit:ValidatorCalloutExtender runat="Server" 
                        ID="ValidatorCalloutExtender2"
                        TargetControlID="RangeValidator4"
                        />   
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-required">Report Type :</label>
                                    <div class="col-md-5">
                                        <asp:DropDownList runat="server" ID="cboReportTypeNo" CssClass="form-control required">
                                            <asp:ListItem Value="" Text="-- Select --" Selected="True" />
                                            <asp:ListItem Value="1" Text="00000001 - Night Differential Summary Report" />
                                            <asp:ListItem Value="2" Text="00000002 - Overtime Summary Report" />
                                            <asp:ListItem Value="3" Text="00000003 - Leave and Absence Summary Report" />
                                            <asp:ListItem Value="4" Text="00000004 - Tardiness Summary Report" />
                                            <asp:ListItem Value="5" Text="00000005 - Undertime Summary Report" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space"></label>
                                    <div class="col-md-5">    
                                        <asp:Button runat="server" ID="lnkPreview" CssClass="btn btn-primary submit fsMain" Text="Preview" OnClick="lnkPreview_Click" />                           
                                        <%--<asp:Button runat="server" ID="lnkCreate" CssClass="btn btn-primary submit fsMain" Text="Create Disk" OnClick="lnkCreate_Click" OnPreRender="lnkPrint_PreRender" Style="visible: False"/>--%>                   
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

