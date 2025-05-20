<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CliRandomTestEdit.aspx.vb" Inherits="Secured_ERProgramEdit" Theme="PCoreStyle" %>

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
                            <asp:Textbox ID="txtClinicRandomTestNo" ReadOnly="true" runat="server" CssClass="form-control" ></asp:Textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Transaction No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                         </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Description :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtClinicRandomTestDesc" TextMode="MultiLine" Rows="3" CssClass="form-control required" />
                        </div>
                    </div>

                    

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Applicable Month :</label>
                        <div class="col-md-6">
                            <asp:DropdownList ID="cboApplicableMonth" DataMember="EMonth" runat="server" CssClass="form-control"></asp:DropdownList>
                        </div>
                    </div>

            
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Applicable Year :</label>
                        <div class="col-md-2"> 
                            <asp:Textbox ID="txtApplicableYear" runat="server" CssClass="form-control required" ></asp:Textbox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtApplicableYear" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Headcount :</label>
                        <div class="col-md-2"> 
                            <asp:Textbox ID="txtHeadCount" runat="server" CssClass="form-control required" ></asp:Textbox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtHeadCount" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Date Conducted :</label>
                        <div class="col-md-3">
                            <asp:Textbox ID="txtStartDate" runat="server" CssClass="form-control" Placeholder="From"></asp:Textbox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4xx" runat="server" TargetControlID="txtStartDate" PopupButtonID="ImageButton1" Format="MM/dd/yyyy" />                                             
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4xx" runat="server" TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                            <asp:RangeValidator ID="RangeValidator1xx" runat="server" ControlToValidate="txtStartDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender6" TargetControlID="RangeValidator1xx" />
                        </div>
            
                        <div class="col-md-3">
                            <asp:Textbox ID="txtEndDate" runat="server" CssClass="form-control" Placeholder="To"></asp:Textbox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtEndDate" PopupButtonID="ImageButton2" Format="MM/dd/yyyy" />                                                  
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtEnddate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator3" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Remarks :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="3" CssClass="form-control" />
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

