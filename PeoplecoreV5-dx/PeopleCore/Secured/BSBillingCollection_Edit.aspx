<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="BSBillingCollection_Edit.aspx.vb" Inherits="Secured_BSBillingCollection_Edit" %>

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
                                    <asp:TextBox ID="txtBSRegisterNo" runat="server" CssClass="form-control" Enabled="false"  Placeholder="Autonumber"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Reference No. :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtCode" runat="server"  CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                              
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Description :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtBSRegisterDesc" runat="server" cssclass="form-control" />
                                </div>
                            </div>
                           
                          
                                                       
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Client :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboBSClientNo" DataMember="BBSClient" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Project :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboProjectNo" DataMember="EProject" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group" >
                                <label class="col-md-3 control-label has-space">JV / O.R. No. :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtORNo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                             </div>

                             <div class="form-group" >
                                <label class="col-md-3 control-label has-space">JV / O.R. Date :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtORDate" SkinID="txtdate" runat="server" CssClass="form-control"></asp:TextBox>
                                    
                                <ajaxToolkit:CalendarExtender ID="customCalendarExtender" runat="server"
                                    TargetControlID="txtORDate"
                                    Format="MM/dd/yyyy" />
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedSeparatedDate" runat="server"
                                    TargetControlID="txtORDate"
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
                                                                        
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                    ControlExtender="MaskedSeparatedDate"
                                    ControlToValidate="txtORDate"
                                    IsValidEmpty="true"
                                    EmptyValueMessage=""
                                    InvalidValueMessage="Date is invalid"
                                    ValidationGroup="Demo1"
                                    Display="Dynamic"
                                    TooltipMessage="" />
                                </div>
                             </div>
                             <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Date Received :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtReceivedDate" SkinID="txtdate" runat="server" CssClass="form-control"></asp:TextBox>
                                    
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                    TargetControlID="txtReceivedDate"
                                    Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                    TargetControlID="txtReceivedDate"
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
                                                                        
                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator7" runat="server"
                                    ControlExtender="MaskedEditExtender1"
                                    ControlToValidate="txtReceivedDate"
                                    IsValidEmpty="true"
                                    EmptyValueMessage=""
                                    InvalidValueMessage="Date is invalid"
                                    ValidationGroup="Demo1"
                                    Display="Dynamic"
                                    TooltipMessage="" />
                                </div>
                             </div>
                             <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Reference(Cheque No, etc.) :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtCheckNo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                             </div>
                             <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Bank Code :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtBankCode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                             </div>
                             <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Details :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDetails" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                             </div>
                             <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Total Amount Collected :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control number"></asp:TextBox>
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

        </Content>
    </uc:Tab>

</asp:Content>