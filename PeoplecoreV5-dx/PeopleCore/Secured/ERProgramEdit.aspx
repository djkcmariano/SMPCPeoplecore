<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="ERProgramEdit.aspx.vb" Inherits="Secured_ERProgramEdit" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    
    <uc:Tab runat="server" ID="Tab">
        <Header>        
            <asp:Label runat="server" ID="lbl" />     
        </Header>
        <Content>
            <asp:Panel runat="server" ID="Panel1">        
            <br /><br />            
            <fieldset class="form" id="fsMain">
                <div class="form-horizontal">
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">Transaction No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtERProgramNo" ReadOnly="true" runat="server" CssClass="form-control" ></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Transaction No. :</label>
                        <div class="col-md-6">
                                <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Enabled="false" Placeholder="Autonumber" ></asp:Textbox>
                            </div>
                    </div> 
                    
                    <div class="form-group">
                        <label class="col-md-3 control-label  has-required">Program Status :</label>
                        <div class="col-md-6">
                                <asp:Dropdownlist ID="cboERProgramStatNo" DataMember="EERProgramStat" runat="server" CssClass="form-control required"></asp:Dropdownlist>
                         </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Program Name :</label>
                        <div class="col-md-6">
                                <asp:Textbox ID="txtERProgramDesc" TextMode="MultiLine" Rows="3" runat="server" CssClass="required form-control" ></asp:Textbox>
                        </div>
                    </div> 
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Program Type :</label>
                        <div class="col-md-6">
                                <asp:Dropdownlist ID="cboERProgramTypeNo" DataMember="EERProgramType" runat="server" CssClass="required form-control"></asp:Dropdownlist>
                         </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Evaluation Template :</label>
                        <div class="col-md-6">
                                <asp:Dropdownlist ID="cboEvalTemplateNo" runat="server" CssClass="form-control"></asp:Dropdownlist>
                         </div>
                    </div> 
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Start Date :</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="required form-control"></asp:TextBox>                                                                                                                                                                                                                   
                                                   
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                    TargetControlID="txtStartDate"
                                    Format="MM/dd/yyyy"/>  
                            
                                    <asp:RangeValidator
                                    ID="RangeValidator3"
                                    runat="server"
                                    ControlToValidate="txtStartDate"
                                    ErrorMessage="<b>Please enter valid entry</b>"
                                    MinimumValue="1900-01-01"
                                    MaximumValue="3000-12-31"
                                    Type="Date" Display="None"  />
                            
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
                            
                                        <ajaxToolkit:ValidatorCalloutExtender 
                                    runat="Server" 
                                    ID="ValidatorCalloutExtender2"
                                    TargetControlID="RangeValidator3" />
                             </div>
                    </div> 
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">End Date :</label>
                        <div class="col-md-2">
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="required form-control"></asp:TextBox>                                                                                                                                                                                                                   
                                   <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                    TargetControlID="txtEndDate"
                                    Format="MM/dd/yyyy"/>  
                            
                                    <asp:RangeValidator
                                    ID="RangeValidator1"
                                    runat="server"
                                    ControlToValidate="txtEndDate"
                                    ErrorMessage="<b>Please enter valid entry</b>"
                                    MinimumValue="1900-01-01"
                                    MaximumValue="3000-12-31"
                                    Type="Date" Display="None"  />
                            
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
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
                            
                                        <ajaxToolkit:ValidatorCalloutExtender 
                                    runat="Server" 
                                    ID="ValidatorCalloutExtender1"
                                    TargetControlID="RangeValidator1" />
                             </div>
                    </div> 
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">No. of Hours :</label>
                        <div class="col-md-2">
                                <asp:Textbox ID="txtHrs" runat="server" CssClass="form-control" ></asp:Textbox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtHrs" />
                         </div>
                    </div> 

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Budget :</label>
                        <div class="col-md-2">
                                <asp:Textbox ID="txtBudget" runat="server" CssClass="form-control" ></asp:Textbox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtBudget" />
                         </div>
                    </div> 
                    <div class="form-group">
                        <label class="col-md-3 control-label  has-space">Objective :</label>
                        <div class="col-md-6">
                                <asp:Textbox ID="txtObjective" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control" ></asp:Textbox>
                         </div>
                    </div> 
                    <div class="form-group">
                        <label class="col-md-3 control-label  has-space">Speaker :</label>
                        <div class="col-md-6">
                                <asp:Textbox ID="txtSpeaker" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control" ></asp:Textbox>
                          </div>
                    </div> 
                    <div class="form-group">
                        <label class="col-md-3 control-label  has-space">Venue :</label>
                        <div class="col-md-6">
                                <asp:Textbox ID="txtVenue" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control" ></asp:Textbox>
                         </div>
                    </div> 
                    
                    <div class="form-group">
                        <label class="col-md-3 control-label  has-space">Evaluation :</label>
                        <div class="col-md-6">
                                <asp:Textbox ID="txtEvaluation" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control" ></asp:Textbox>
                         </div>
                    </div>
 

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="btnSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button runat="server"  ID="btnModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="btnModify_Click" />                            
                        </div>
                    </div>
                </div>                               
                <br /><br /> 
            </fieldset>
            </asp:Panel>                                                  
        </Content>
    </uc:Tab>
</asp:Content>

