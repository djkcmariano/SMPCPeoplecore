<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BSBillingEdit.aspx.vb" Inherits="Secured_BSBillingEdit" %>
<%@ Register Src="~/Include/Info.ascx" TagName="Info" TagPrefix="uc" %>

<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">
  
     <uc:Tab runat="server" ID="Tab">
        <Header>        
            <asp:Label runat="server" ID="lbl" /> 
            
        </Header>
        <Content>
            <br />
            <br />

            <asp:Panel runat="server" ID="Panel1">
                <fieldset class="form" id="fsd">
                        <div  class="form-horizontal">

                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Transaction No. :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtBSNo" runat="server" CssClass="form-control" Enabled="false"  Placeholder="Autonumber"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Billing Number :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtBSCode" runat="server"  CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label ">
                                    <h5><b>BILLING COMPONENTS</b></h5>
                                </label>
                            </div>     
                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">Billing Formula Type:</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboBSProjectPayTypeNo" runat="server" DataMember="BBSProjectPayType" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                           
                             <div class="form-group" >
                                <label class="col-md-3 control-label has-required">Applicable Year :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtApplicableYear" runat="server" CssClass="form-control required"></asp:TextBox>
                                </div>
                             </div>
                           <div class="form-group">
                                <label class="col-md-3 control-label ">
                                    <h5><b>BILLING CUT-OFF</b></h5>
                                </label>
                            </div>   
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Start Date :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="customCalendarExtender" runat="server" TargetControlID="txtStartDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedStartDate" runat="server" TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedStartDate" ControlToValidate="txtStartDate" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">End Date :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="customCalendarExtender2" runat="server" TargetControlID="txtEndDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEndDate" runat="server" TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEndDate" ControlToValidate="txtEndDate" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Project :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboProjectNo" DataMember="EProject" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                              <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Details :</label>
                                <div class="col-md-6" >
                                    <asp:TextBox ID="txtDetails" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group">
                                <label class="col-md-3 control-label has-space">Prepared Date :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtPreparedDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPreparedDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedPreparedDate" runat="server" TargetControlID="txtPreparedDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedPreparedDate" ControlToValidate="txtPreparedDate" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                </div>
                            </div>
                             <div class="form-group">
                                <label class="col-md-3 control-label has-space">Received Date :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtReceivedDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedReceivedDate" runat="server" TargetControlID="txtReceivedDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedReceivedDate" ControlToValidate="txtReceivedDate" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                </div>
                            </div>
                             <div class="form-group">
                                <label class="col-md-3 control-label has-space">Billing Date :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtSubmittedDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSubmittedDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedBillingDate" runat="server" TargetControlID="txtSubmittedDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator6" runat="server" ControlExtender="MaskedBillingDate" ControlToValidate="txtSubmittedDate" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                </div>
                            </div>   
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Due Date :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDueDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedDueDate" runat="server" TargetControlID="txtDueDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedDueDate" ControlToValidate="txtDueDate" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                </div>
                            </div> 

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Prepared By :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboPreparedByNo" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Notes :</label>
                                <div class="col-md-6" >
                                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            
                            <div class="form-group" >
                                <label class="col-md-3 control-label has-space">E-VAT Rate :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtTaxRate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtTaxRate" />
                                </div>
                            </div>
                           <div class="form-group" >
                                <label class="col-md-3 control-label has-space">ASF Rate :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtASFRate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtASFRate" />
                                </div>
                            </div>
                            <div class="form-group" >
                                <label class="col-md-3 control-label has-space">E-VAT :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtTaxAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtTaxAmount" />
                                </div>
                            </div>
                           <div class="form-group" >
                                <label class="col-md-3 control-label has-space">ASF Amount :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtASFAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtASFAmount" />
                                </div>
                            </div>
                            <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Total Billable :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtTotalBilling" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtTotalBilling" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space"></label>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="txtIsASFVAT" runat="server" Text="&nbsp;Tick here if VAT applied to ASF only."></asp:CheckBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space"></label>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="txtIsCancel" runat="server" Text="&nbsp;Tick here if you want to cancel Billing."></asp:CheckBox>
                                </div>
                            </div>
                             
                           
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space"></label>
                                <div class="col-md-6">
                                    <asp:Button runat="server" ID="lnkSave" CssClass="btn btn-default submit fsd" OnClick="lnkSave_Click" Text="Save"></asp:Button>
                                    <asp:Button runat="server" ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" UseSubmitBehavior="false"></asp:Button>
                                </div>
                            </div> 
                            <br />
                    </div>
                    </fieldset >
            </asp:Panel>

            <uc:Info runat="server" ID="Info1" /> 

        </Content>
    </uc:Tab>

</asp:Content>