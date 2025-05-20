<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfEmployeeInfoAppr.aspx.vb" Inherits="Secured_SelfEmployeeInfoAppr" %>

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
                    <label class="col-md-3 control-label has-space">Manpower Item No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtPlantillaCode" CssClass="form-control" style="display:inline-block;" /> 
                        <asp:HiddenField runat="server" ID="hifPlantillaNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="acePlantillaCode" runat="server"  
                        TargetControlID="txtPlantillaCode" MinimumPrefixLength="2" 
                        CompletionInterval="500" ServiceMethod="PopulatePlantilla" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getItem" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                         <script type="text/javascript">
                             function getItem(source, eventArgs) {
                                 document.getElementById('<%= hifPlantillaNo.ClientID %>').value = eventArgs.get_value();
                             }
                        </script> 
                    </div>
                </div>
                <div class="form-group" >
                    <label class="col-md-3 control-label has-space" >Acting Item No. :</label>
                    <div class="col-md-6">
                        <%--<asp:LinkButton runat="server" ID="LinkButton1" OnClick="lnkIncumbent_Click" CausesValidation="false" style=" font-size:11px;" Text="Click Here to view the Item No. Reference" Visible="false" />--%>
                        <asp:TextBox runat="server" ID="txtActingPlantillaDesc" CssClass="form-control" />        
                        <asp:HiddenField runat="server" ID="hifActingPlantillaNo" />
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"  
                        TargetControlID="txtActingPlantillaDesc" MinimumPrefixLength="2" 
                        CompletionInterval="500" ServiceMethod="PopulatePlantilla" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="GetActingItem" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />

                            <script type="text/javascript">
                                function GetActingItem(source, eventArgs) {
                                    document.getElementById('<%= hifActingPlantillaNo.ClientID %>').value = eventArgs.get_value();
                                }                               	
                                             	
                            </script>
                       
                    </div>
                </div>
                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Business Unit :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboFacilityNo" runat="server" CssClass="form-control" DataMember="EFacility" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Department :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboDepartmentNo" runat="server" CssClass="form-control" DataMember="EDepartment" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Team :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboSectionNo" runat="server" CssClass="form-control" DataMember="ESection" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Sub Team :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboUnitNo" runat="server" CssClass="form-control" DataMember="EUnit" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Group :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboGroupNo" runat="server" CssClass="form-control" DataMember="EGroup" />
                    </div>
                </div> 
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Division :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboDivisionNo" runat="server" CssClass="form-control" DataMember="EDivision" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Cost Center :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboCostCenterNo" runat="server" CssClass="form-control" DataMember="ECostCenter" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Salary Level :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboSalaryGradeNo" runat="server" CssClass="form-control" DataMember="ESalaryGrade" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Location / Area :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboLocationNo" runat="server" CssClass="form-control" DataMember="ELocation" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Floor/ Room Assignment :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtWorkArea" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Local No. / Direct Line :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtLocalNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Branch :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboBranchNo" runat="server" CssClass="form-control" DataMember="EBranch" />
                    </div>
                </div>                                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Position :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboPositionNo" runat="server" CssClass="form-control" DataMember="EPosition" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Project :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboProjectNo" runat="server" CssClass="form-control" DataMember="EProject" />
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

                <div class="form-group" style="display:none;">
                    <label class="col-md-3 control-label has-space">Step Increment  :</label>
                    <div class="col-md-6">
                        <asp:DropDownList ID="cboStep" DataMember="EStep" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Functional Title :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboTaskNo" runat="server" CssClass="form-control" DataMember="ETask" />
                    </div>
                </div>                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Superior :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtSFullName" CssClass="form-control" style="display:inline-block;" /> 
                        <asp:HiddenField runat="server" ID="hifSEmployeeNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                        TargetControlID="txtSFullName" MinimumPrefixLength="2" 
                        CompletionInterval="500" ServiceMethod="PopulateManager" 
                        CompletionListCssClass="autocomplete_completionListElement" 
                        CompletionListItemCssClass="autocomplete_listItem" 
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                         <script type="text/javascript">
                             function getRecord(source, eventArgs) {
                                 document.getElementById('<%= hifSEmployeeNo.ClientID %>').value = eventArgs.get_value();
                             }
                        </script>                                                                    
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Job Grade :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboJobGradeNo" runat="server" CssClass="form-control" DataMember="EJobGrade" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Employee Classification :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboEmployeeClassNo" runat="server" CssClass="form-control" DataMember="EEmployeeClass" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Employee Status :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboEmployeeStatNo" runat="server" CssClass="form-control" DataMember="EEmployeeStat" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Rank :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboRankNo" runat="server" CssClass="form-control" DataMember="ERank" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">&nbsp;</label>
                    <div class="col-md-6">
                        <asp:CheckBox runat="server" ID="chkIsSupervisor" Text="&nbsp;Please check here to identify that he/she is an immediate superior." />
                    </div>
                </div>
                <br />
                <div class="form-group">  
                    <h5 class="col-md-8">
                        <label class="control-label">PAYROLL&nbsp;&nbsp;RELATED&nbsp;&nbsp;INFORMATION</label>
                    </h5>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Payroll Group :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboPayClassNo" runat="server" CssClass="form-control required" DataMember="EPayClass" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Company :</label>
                    <div class="col-md-6">
                        <asp:DropdownList ID="cboPayLocNo" runat="server" CssClass="form-control required" DataMember="EPayLoc" />
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
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-6">            
                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button runat="server" ID="btnModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="btnModify_Click" />                            
                    </div>
                </div>
                <br /><br />                     
            </div>                                                
        </fieldset>
    </asp:Panel>
    </Content>
</uc:Tab>
</asp:Content> 