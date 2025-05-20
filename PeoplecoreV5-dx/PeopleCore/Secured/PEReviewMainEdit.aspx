<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PEReviewMainEdit.aspx.vb" Inherits="Secured_PEReviewMainEdit" %>

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
                <div  class="form-horizontal">
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label">Transaction No :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtPEReviewMainNo" CssClass="form-control" runat="server" ></asp:Textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Transaction No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control" Placeholder="Autonumber" ></asp:Textbox>
                         </div>
                    </div>

                    
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Title :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboPEStandardMainLNo" runat="server" DataMember="EPEStandardMainL" CssClass="form-control required"></asp:Dropdownlist>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Applicable Year :</label>
                        <div class="col-md-3">
                            <asp:Textbox ID="txtApplicableyear" runat="server" CssClass="form-control required" ></asp:Textbox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtApplicableyear" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Performance Period Type :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboPEPeriodNo" runat="server" DataMember="EPEPeriod" CssClass="form-control required"></asp:Dropdownlist>
                        </div>
                    </div>

            
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Evaluation Type :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboPEEvalPeriodNo" runat="server" DataMember="EPEEvalPeriod" CssClass="form-control"></asp:Dropdownlist>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Evaluation Period :</label>
                        <div class="col-md-3">
                            <asp:Textbox ID="txtStartDate" runat="server" CssClass="form-control" placeholder="Start Date"></asp:Textbox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtStartDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />     
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator1" /> 
                        </div>
                        <div class="col-md-3">
                            <asp:Textbox ID="txtEndDate" runat="server" CssClass="form-control" placeholder="End Date"></asp:Textbox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtEndDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                       
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RangeValidator2" /> 
                        </div>
                    </div>

                   
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Evaluation Process :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboPEEvalProcessTypeNo" runat="server" DataMember="EPEEvalProcessType" CssClass="form-control required"></asp:Dropdownlist>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Performance Norms :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboPENormsNo" runat="server" DataMember="EPENorms" CssClass="form-control required"></asp:Dropdownlist>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Performance Cycle :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboPECycleNo" runat="server" DataMember="EPECycle" CssClass="form-control required"></asp:Dropdownlist>
                        </div>
                    </div>




                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                            <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />                            
                        </div>
                    </div>

                    <br /><br />                     
                </div>                                                
            </fieldset>
        </asp:Panel>
        </Content>
    </uc:Tab>    
</asp:Content>

