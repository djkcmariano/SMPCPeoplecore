<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PayContEdit.aspx.vb" Inherits="Secured_PayContEdit" Theme="PCoreStyle" %>

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
                                            <label class="col-md-3 control-label has-space">Transaction No. :</label>
                                            <div class="col-md-6">
                                                <asp:Textbox ID="txtPayContNo" runat="server" CssClass="form-control" ></asp:Textbox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-space">Transaction No. :</label>
                                            <div class="col-md-6">
                                                <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" Placeholder="Autonumber" ></asp:Textbox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-space">Reference No. :</label>
                                            <div class="col-md-6">
                                                <asp:Textbox ID="txtRefNo" runat="server" CssClass="form-control" ></asp:Textbox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-space">Location Code :</label>
                                            <div class="col-md-6">
                                                <asp:Textbox ID="txtLocCode" runat="server"  CssClass="form-control"></asp:Textbox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-required">Company Name :</label>
                                            <div class="col-md-6">
                                                <asp:DropdownList ID="cboPayLocNo" runat="server"  CssClass="form-control required"></asp:DropdownList>
                                            </div>
                                        </div>

                                        <div class="form-group" style="display:none;" >
                                            <label class="col-md-3 control-label has-space">Facility :</label>
                                            <div class="col-md-6">
                                                <asp:DropdownList ID="cboFacilityNo" DataMember="EFacility"  runat="server"  CssClass="form-control"></asp:DropdownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-required">Applicable Month :</label>
                                            <div class="col-md-6">
                                                <asp:DropdownList ID="cboApplicableMonth" DataMember="Emonth" runat="server" CssClass="form-control required"></asp:DropdownList>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-required">Applicable Year :</label>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtApplicableYear" runat="server" CssClass="form-control required"></asp:Textbox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtApplicableYear" FilterType="Numbers" ValidChars="." />
                                            </div>
                                        </div>

                                        <div class="form-group">  
                                            <h5 class="col-md-8">
                                                <label class="control-label">CONTRIBUTION SBR</label>
                                            </h5>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-space"></label>
                                            <div class="col-md-3">
                                                <label class="control-label">SBR No.</label><br />
                                            </div>
                                            <div class="col-md-3">
                                                <label class="control-label">SBR Date</label><br />
                                            </div>
                                            <div class="col-md-3">
                                                <label class="control-label">SBR Bank</label><br />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-space">SSS :</label>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtSSSSBR" CssClass="form-control" runat="server" ></asp:Textbox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtSSSDate" CssClass="form-control" runat="server" ></asp:Textbox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtSSSDate" Format="MM/dd/yyyy" />                                   
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                                    TargetControlID="txtSSSDate"
                                                    Mask="99/99/9999"
                                                    MessageValidatorTip="true"
                                                    OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError"
                                                    MaskType="Date"
                                                    DisplayMoney="Left"
                                                    AcceptNegative="Left" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtSSSBank" CssClass="form-control" runat="server" ></asp:Textbox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-space">HDMF :</label>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtHDMFSBR" CssClass="form-control" runat="server" ></asp:Textbox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtHDMFDate" CssClass="form-control" runat="server" ></asp:Textbox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtHDMFDate" Format="MM/dd/yyyy" />                  
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                                    TargetControlID="txtHDMFDate"
                                                    Mask="99/99/9999"
                                                    MessageValidatorTip="true"
                                                    OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError"
                                                    MaskType="Date"
                                                    DisplayMoney="Left"
                                                    AcceptNegative="Left" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtHDMFBank" CssClass="form-control" runat="server" ></asp:Textbox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-space">PhilHealth :</label>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtPHSBR" CssClass="form-control" runat="server" ></asp:Textbox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtPHDate" CssClass="form-control" runat="server" ></asp:Textbox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPHDate" Format="MM/dd/yyyy" />                        
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                                    TargetControlID="txtPHDate"
                                                    Mask="99/99/9999"
                                                    MessageValidatorTip="true"
                                                    OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError"
                                                    MaskType="Date"
                                                    DisplayMoney="Left"
                                                    AcceptNegative="Left" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtPHBank" CssClass="form-control" runat="server" ></asp:Textbox>
                                            </div>
                                        </div>

                                        <div class="form-group">  
                                            <h5 class="col-md-8">
                                                <label class="control-label">LOAN SBR</label>
                                            </h5>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-space"></label>
                                            <div class="col-md-3">
                                                <label class="control-label">SBR No.</label><br />
                                            </div>
                                            <div class="col-md-3">
                                                <label class="control-label">SBR Date</label><br />
                                            </div>
                                            <div class="col-md-3">
                                                <label class="control-label">SBR Bank</label><br />
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-space">SSS :</label>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtSSSLoanSBR" CssClass="form-control" runat="server" ></asp:Textbox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtSSSLoanDate" CssClass="form-control" runat="server" ></asp:Textbox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtSSSLoanDate" Format="MM/dd/yyyy" />    
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                                    TargetControlID="txtSSSLoanDate"
                                                    Mask="99/99/9999"
                                                    MessageValidatorTip="true"
                                                    OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError"
                                                    MaskType="Date"
                                                    DisplayMoney="Left"
                                                    AcceptNegative="Left" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtSSSLoanBank" CssClass="form-control" runat="server" ></asp:Textbox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-space">HDMF :</label>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtHDMFLoanSBR" CssClass="form-control" runat="server" ></asp:Textbox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtHDMFLoanDate" CssClass="form-control" runat="server" ></asp:Textbox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtHDMFLoanDate" Format="MM/dd/yyyy" />              
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                                    TargetControlID="txtHDMFLoanDate"
                                                    Mask="99/99/9999"
                                                    MessageValidatorTip="true"
                                                    OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError"
                                                    MaskType="Date"
                                                    DisplayMoney="Left"
                                                    AcceptNegative="Left" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtHDMFLoanBank" CssClass="form-control" runat="server" ></asp:Textbox>
                                            </div>
                                        </div>

                                        <div class="form-group" style="display:none;">
                                            <label class="col-md-3 control-label has-space">PhilHealth :</label>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtPHLoanSBR" CssClass="form-control" runat="server" ></asp:Textbox>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtPHLoanDate" CssClass="form-control" runat="server" ></asp:Textbox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtPHLoanDate" Format="MM/dd/yyyy" />        
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                                                    TargetControlID="txtPHLoanDate"
                                                    Mask="99/99/9999"
                                                    MessageValidatorTip="true"
                                                    OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError"
                                                    MaskType="Date"
                                                    DisplayMoney="Left"
                                                    AcceptNegative="Left" />
                                            </div>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtPHLoanBank" CssClass="form-control" runat="server" ></asp:Textbox>
                                            </div>
                                        </div>

                                        <div class="form-group" style="visibility:hidden; position:absolute;" >
                                            <label class="col-md-3 control-label has-space"></label>
                                            <div class="col-md-6">
                                                <asp:Checkbox ID="txtIsPosted" runat="server" ></asp:Checkbox>
                                            </div>
                                        </div> 


                                        <div class="form-group">
                                            <label class="col-md-3 control-label has-space"></label>
                                            <div class="col-md-6">
                                                <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                                                <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />
                                            </div>
                                        </div>

                                        <br />
                                    </div>
                                </fieldset>
                            </asp:Panel>
       
       </Content>
</uc:Tab>
</asp:Content>

