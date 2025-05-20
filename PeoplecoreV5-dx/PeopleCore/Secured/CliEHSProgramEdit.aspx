<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CliEHSProgramEdit.aspx.vb" Inherits="Secured_ERProgramEdit" Theme="PCoreStyle" %>

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
                            <asp:Textbox ID="txtEHSProgramNo" ReadOnly="true" runat="server" CssClass="form-control" ></asp:Textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Transaction No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtEHSProgramCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Status :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboEHSStatNo" DataMember="EEHSStat" CssClass="form-control required" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Program Name :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtEHSProgramDesc" TextMode="MultiLine" Rows="3" CssClass="form-control required" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Program Type :</label>
                        <div class="col-md-6">
                            <asp:DropdownList ID="cboEHSProgramTypeNo" DataMember="EEHSProgramType" runat="server" CssClass="form-control required"></asp:DropdownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Date From :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control required"></asp:TextBox> 
                                                                    
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                TargetControlID="txtFromDate"
                                Format="MM/dd/yyyy" />  
                                      
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                TargetControlID="txtFromDate"
                                Mask="99/99/9999"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date"
                                DisplayMoney="Left"
                                AcceptNegative="Left" />
                                    
                                <asp:RangeValidator
                                ID="RangeValidator2"
                                runat="server"
                                ControlToValidate="txtFromdate"
                                ErrorMessage="<b>Please enter valid entry</b>"
                                MinimumValue="1900-01-01"
                                MaximumValue="3000-12-31"
                                Type="Date" Display="None"  />
                                    
                                <ajaxToolkit:ValidatorCalloutExtender 
                                runat="Server" 
                                ID="ValidatorCalloutExtender2"
                                TargetControlID="RangeValidator2" />                                                                           
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Date To :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control required"></asp:TextBox> 
                                                                    
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                TargetControlID="txtToDate"
                                Format="MM/dd/yyyy" />  
                                      
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                TargetControlID="txtToDate"
                                Mask="99/99/9999"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date"
                                DisplayMoney="Left"
                                AcceptNegative="Left" />
                                    
                                <asp:RangeValidator
                                ID="RangeValidator1"
                                runat="server"
                                ControlToValidate="txtToDate"
                                ErrorMessage="<b>Please enter valid entry</b>"
                                MinimumValue="1900-01-01"
                                MaximumValue="3000-12-31"
                                Type="Date" Display="None"  />
                                    
                                <ajaxToolkit:ValidatorCalloutExtender 
                                runat="Server" 
                                ID="ValidatorCalloutExtender1"
                                TargetControlID="RangeValidator1" />                                                                           
                        </div>
                    </div>

                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">No. Of Days :</label>
                        <div class="col-md-2"> 
                            <asp:Textbox ID="txtNoOfDay" runat="server" CssClass="form-control" ></asp:Textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Budget :</label>
                        <div class="col-md-2"> 
                            <asp:Textbox ID="txtBudget" runat="server" CssClass="form-control" ></asp:Textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Sponsored By :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtSponsoredBy" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Objective :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtObjective" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Evaluation :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtProgramEvaluation" TextMode="MultiLine" Rows="3" CssClass="form-control" />
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

