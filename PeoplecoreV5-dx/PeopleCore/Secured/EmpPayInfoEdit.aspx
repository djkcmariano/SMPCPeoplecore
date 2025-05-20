<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpPayInfoEdit.aspx.vb" Inherits="Secured_EmpEditInfo" %>

<asp:Content id="cntNo" contentplaceholderid="cphBody" runat="server">
<uc:Tab runat="server" ID="Tab">
    <Header>
        <center>
            <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
            <br />            
        </center>            
        <asp:Label runat="server" ID="lbl" />        
    </Header>
    <Content>
    <asp:Panel runat="server" ID="Panel1">        
        <br /><br />               
        <fieldset class="form" id="fsMain">
            <div  class="form-horizontal">
                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Payroll Group :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboPayClassNo" runat="server" CssClass="form-control" DataMember="EPayClass" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Company :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboPayLocNo" runat="server" CssClass="form-control" DataMember="EPayLoc" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Payroll Type :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboPayTypeNo" runat="server" CssClass="form-control" DataMember="EPayType" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Rate Class :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboEmployeeRateClassNo" runat="server" CssClass="form-control" DataMember="EEmployeeRateClass" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Tax Code :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboTaxExemptNo" runat="server" CssClass="form-control" DataMember="ETaxExempt" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Payment Type :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboPaymentTypeNo" runat="server" CssClass="form-control" DataMember="EPaymentType" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Bank Type :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboBankTypeNo" runat="server" CssClass="form-control" DataMember="EBankType" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Bank Account No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtBankAccountNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">SSS Contribution :</label>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtSSSNo" CssClass="form-control" placeholder="SSS No." />
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsDontDeductSSS" Text="&nbsp;Suspend" />
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsSSSPaNoByER" Text="&nbsp;Charge to ER" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">PhilHealth Contribution :</label>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtPHNo" CssClass="form-control" placeholder="PhilHealth No." />
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsDontDeductPH" Text="&nbsp;Suspend" />
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsPHPaNoByER" Text="&nbsp;Charge to ER" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">HDMF Contribution :</label>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtHDMFNo" CssClass="form-control" placeholder="HDMF No." />
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsDontDeductHDMF" Text="&nbsp;Suspend" />
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsHDMFPaNoByER" Text="&nbsp;Charge to ER" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">HDMF Employee Share :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtEmployeeHDMF" CssClass="form-control" />
                    </div>                  
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">HDMF Employer Share :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtEmployerHDMF" CssClass="form-control" />
                    </div>                  
                </div>               
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Tax Related Information :</label>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtTinNo" CssClass="form-control" placeholder="TIN" />
                    </div>
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsDontDeductTAx" Text="&nbsp;(M W E)" />
                    </div>                     
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsFlatTax" Text="&nbsp;Flat Tax Rate" />
                    </div>                                     
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Percent Tax Rate :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtTaxPercentRate" CssClass="form-control number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Minimum Take Home Pay :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtMinTakeHomePay" CssClass="form-control number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Union :</label>
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsUnion" Text="&nbsp;Member" />
                    </div>                     
                    <div class="col-md-2">
                        <asp:CheckBox runat="server" ID="chkIsUnionOfficer" Text="&nbsp;Officer" />
                    </div>                                     
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Orientee Date :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtOrienteeStartDate" CssClass="form-control" placeholder="Start Date" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtOrienteeStartDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtOrienteeStartDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" ControlToValidate="txtOrienteeStartDate" Type="Date" ErrorMessage="Please enter valid date." Display="Dynamic" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtOrienteeEndDate" CssClass="form-control" placeholder="End Date" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtOrienteeEndDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtOrienteeEndDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" ControlToValidate="txtOrienteeEndDate" Type="Date" ErrorMessage="Please enter valid date." Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Probationary Date :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtProbeStartDate" CssClass="form-control" placeholder="Start Date" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" TargetControlID="txtProbeStartDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender3" TargetControlID="txtProbeStartDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator3" Operator="DataTypeCheck" ControlToValidate="txtProbeStartDate" Type="Date" ErrorMessage="Please enter valid date." Display="Dynamic" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtProbeEndDate" CssClass="form-control" placeholder="End Date" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender4" TargetControlID="txtProbeEndDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender4" TargetControlID="txtProbeEndDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator4" Operator="DataTypeCheck" ControlToValidate="txtProbeEndDate" Type="Date" ErrorMessage="Please enter valid date." Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Hired Date :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtHiredDate" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender5" TargetControlID="txtHiredDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender5" TargetControlID="txtHiredDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator5" Operator="DataTypeCheck" ControlToValidate="txtHiredDate" Type="Date" ErrorMessage="Please enter valid date." Display="Dynamic" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Re-hired Date :</label>
                    <div class="col-md-6">                        
                        <asp:TextBox runat="server" ID="txtRehiredDate" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender6" TargetControlID="txtRehiredDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender6" TargetControlID="txtRehiredDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator6" Operator="DataTypeCheck" ControlToValidate="txtRehiredDate" Type="Date" ErrorMessage="Please enter valid date." Display="Dynamic" />                    
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Regularization Date :</label>
                    <div class="col-md-6">                        
                        <asp:TextBox runat="server" ID="txtRegularizedDate" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender8" TargetControlID="txtRegularizedDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender8" TargetControlID="txtRegularizedDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator8" Operator="DataTypeCheck" ControlToValidate="txtRegularizedDate" Type="Date" ErrorMessage="Please enter valid date." Display="Dynamic" />                    
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Separated Date:</label>
                    <div class="col-md-3">                        
                        <asp:TextBox runat="server" ID="txtSeparatedDate" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender7" TargetControlID="txtSeparatedDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender7" TargetControlID="txtSeparatedDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator7" Operator="DataTypeCheck" ControlToValidate="txtSeparatedDate" Type="Date" ErrorMessage="Please enter valid date." Display="Dynamic" />                    
                    </div>
                    <div class="col-md-3">                        
                        <asp:CheckBox runat="server" ID="chkIsSeparated" Text="&nbsp;Separated" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Suspended Date:</label>
                    <div class="col-md-3">                        
                        <asp:TextBox runat="server" ID="txtSuspendedDate" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender9" TargetControlID="txtSuspendedDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender9" TargetControlID="txtSuspendedDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator9" Operator="DataTypeCheck" ControlToValidate="txtSuspendedDate" Type="Date" ErrorMessage="Please enter valid date." Display="Dynamic" />                    
                    </div>
                    <div class="col-md-3">                        
                        <asp:CheckBox runat="server" ID="chkIsSuspendPay" Text="&nbsp;Suspended" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Blacklisted Date:</label>
                    <div class="col-md-3">                        
                        <asp:TextBox runat="server" ID="txtBlackListedDate" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender10" TargetControlID="txtBlackListedDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender10" TargetControlID="txtBlackListedDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator10" Operator="DataTypeCheck" ControlToValidate="txtBlackListedDate" Type="Date" ErrorMessage="Please enter valid date." Display="Dynamic" />                    
                    </div>
                    <div class="col-md-3">                        
                        <asp:CheckBox runat="server" ID="chkIsBlacklisted" Text="&nbsp;Blacklisted" />
                    </div>
                </div>
                
                <div class="form-group" style="display:none;">
                    <label class="col-md-3 control-label has-space">Shift :</label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="cboShiftNo" DataMember="EShift" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group" style="display:none;">
                    <label class="col-md-3 control-label has-space">Day Off 1 :</label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="cboDayOffNo" DataMember="EDayOff" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group" style="display:none;">
                    <label class="col-md-3 control-label has-space">Day Off 2 :</label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="cboDayOffNo2" DataMember="EDayOff" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-6">            
                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary submit fsMain" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button runat="server" ID="btnModify" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="btnModify_Click" />                            
                    </div>
                </div>
                <br /><br />                     
            </div>                                                
        </fieldset>
    </asp:Panel>
    </Content>
</uc:Tab>
</asp:Content> 