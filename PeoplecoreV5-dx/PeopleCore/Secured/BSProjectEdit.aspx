<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BSProjectEdit.aspx.vb" Inherits="Secured_BSProjectEdit" %>
<%@ Register Src="~/Include/Info.ascx" TagName="Info" TagPrefix="uc" %>

<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">
  
     <uc:Tab runat="server" ID="Tab">
        <Header>        
            <asp:Label runat="server" ID="lbl" /> 
            <div style="display:none;">
                <asp:CheckBox ID="txtIsArchived" runat="server"></asp:CheckBox>
            </div>      
        </Header>
        <Content>
            <br />
            <br />

            <asp:Panel runat="server" ID="Panel1">
                <fieldset class="form" id="fsd">
                        <div  class="form-horizontal">

                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Transaction No. :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtProjectNo" runat="server" CssClass="form-control" Enabled="false"  Placeholder="Autonumber"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Project Code :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtProjectCode" runat="server"  CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            
                           
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Project Description :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtProjectDesc" runat="server" cssclass="form-control" />
                                </div>
                            </div>
                           
                             <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Reason :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtReason" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                             </div>

                            <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Details :</label>
                                <div class="col-md-6" >
                                    <asp:TextBox ID="txtDetails" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Project Address :</label>
                                <div class="col-md-6" >
                                    <asp:TextBox ID="txtProjectAddress" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Client Name :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboBSClientNo" DataMember="BBSClient" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                
                            </div>
                            <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Reference :</label>
                                <div class="col-md-6" >
                                    <asp:TextBox ID="txtReference" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Cost Center :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboCostCenterNo" DataMember="ECostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Category :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboBSCategoryNo" DataMember="BBSCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Priority :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboBSPriorityNo" DataMember="BBSPriority" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Status :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboBSStatusNo" DataMember="BBSStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Project Start Date :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="customCalendarExtender" runat="server" TargetControlID="txtStartDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedStartDate" runat="server" TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedStartDate" ControlToValidate="txtStartDate" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Project End Date :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="customCalendarExtender2" runat="server" TargetControlID="txtEndDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEndDate" runat="server" TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEndDate" ControlToValidate="txtEndDate" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                </div>
                            </div>
                            <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Budget Days :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtBudgetDays" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtBudgetDays" />
                                </div>
                            </div>
                            
                            <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Project Cost :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtProjectCost" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtProjectCost" />
                                </div>
                            </div>

                            <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Tax Rate :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtTaxRate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtTaxRate" />
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
                                <label class="col-md-3 control-label has-space">Terms :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtTerms" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtTerms" />
                                </div>
                            </div>
                           
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space"></label>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="txtIsASFVAT" runat="server" Text="&nbsp;Tick here if VAT applied to ASF only."></asp:CheckBox>
                                </div>
                            </div>

                             <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Notes :</label>
                                <div class="col-md-6" >
                                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
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