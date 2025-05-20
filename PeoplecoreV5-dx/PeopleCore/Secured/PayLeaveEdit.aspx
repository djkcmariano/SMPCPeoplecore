<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PayLeaveEdit.aspx.vb" Inherits="Secured_PayLastEdit" Theme="PCoreStyle" %>

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
                        <label class="col-md-3 control-label has-space">Payroll No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtPayNo" ReadOnly="true" runat="server" CssClass="form-control" ></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Payroll No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtPayCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" ></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Pay Date :</label>
                        <div class="col-md-3">
                            <asp:Textbox ID="txtPayDate" runat="server" CssClass="form-control required"></asp:Textbox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4x" runat="server" TargetControlID="txtPayDate" Format="MM/dd/yyyy" />                                                
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4x" runat="server" TargetControlID="txtPayDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                            <asp:RangeValidator ID="RangeValidator1x" runat="server" ControlToValidate="txtPayDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1x" TargetControlID="RangeValidator1x" />                                                  
                         </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Payroll Group :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboPayClassNo" runat="server" CssClass="form-control required" AutoPostBack="true" OnTextChanged="cboPaySchedule_TextChanged"></asp:Dropdownlist>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Payroll Type :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboPayTypeNo" DataMember="EPayType" runat="server" CssClass="form-control required" />
                        </div>                
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Cut Off Date :</label>
                        <div class="col-md-3">
                            <asp:Textbox ID="txtStartDate" runat="server" CssClass="form-control required" Placeholder="From"></asp:Textbox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4xx" runat="server" TargetControlID="txtStartDate" PopupButtonID="ImageButton1" Format="MM/dd/yyyy" />                                             
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4xx" runat="server" TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                            <asp:RangeValidator ID="RangeValidator1xx" runat="server" ControlToValidate="txtStartDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender6" TargetControlID="RangeValidator1xx" />
                        </div>
            
                        <div class="col-md-3">
                            <asp:Textbox ID="txtEndDate" runat="server" CssClass="form-control required" Placeholder="To"></asp:Textbox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtEndDate" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />                                                  
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtEnddate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator3" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Applicable Month :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboApplicableMonth" DataMember="EMonth" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Applicable Year :</label>
                        <div class="col-md-3">
                            <asp:Textbox ID="txtApplicableYear" runat="server" CssClass="form-control" ></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Payroll Period :</label>
                        <div class="col-md-3">
                            <asp:Textbox ID="txtPayperiod" runat="server" CssClass="form-control" ></asp:Textbox>
                        </div>
                    </div>
            
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">
                                <asp:CheckBox ID="txtIsPaymentSuspended" runat="server" Text="&nbsp;Suspend for review (Exlude from YTD)" />
                        </div>
                    </div>

                    <div class="form-group" style="visibility:hidden;position:absolute;">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">
                            <asp:CheckBox ID="txtIsAdvancedCredits" runat="server" Text="&nbsp;Process leave balance up to end date" />                    
                        </div>
                    </div>     


                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">
                            <h5><b>Applicable Deduction</b></h5>
                        </label>
                    </div>    
                              
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-3">
                            <asp:Checkbox ID="txtIsDeductTax" runat="server" Text="&nbsp;Tax" ></asp:Checkbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Percent Tax :</label>
                        <div class="col-md-3">
                            <asp:Textbox ID="txtPercentTax" runat="server" SkinID="txtdate" CssClass="form-control" ></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">
                            <h5><b>Payroll Components</b></h5>
                        </label>
                    </div>  

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-3">                    
                            <asp:Checkbox ID="txtIsIncludeMass" runat="server" Text="&nbsp;Template" />
                        </div>
                        <div class="col-md-3">                    
                            <asp:Checkbox ID="txtIsIncludeForw" runat="server" Text="&nbsp;Forwarded" />
                        </div>
                    </div>        
                        
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">&nbsp;</label>
                        <div class="col-md-3">                    
                            <asp:Checkbox ID="txtIsIncludeLoan" runat="server" Text="&nbsp;Loan" />
                        </div>
                        <div class="col-md-3">                    
                            <asp:Checkbox ID="txtIsIncludeOther" runat="server" Text="&nbsp;Other" />
                        </div>
                    </div>

                    <div class="form-group" style="visibility:hidden;position:absolute;">
                        <label class="col-md-3 control-label ">&nbsp;</label>
                        <div class="col-md-3">
                            <asp:Checkbox ID="txtIsposted" runat="server" ></asp:Checkbox>
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

