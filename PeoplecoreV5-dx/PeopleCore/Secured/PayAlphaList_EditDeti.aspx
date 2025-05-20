<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayAlphaList_EditDeti.aspx.vb" Inherits="Secured_PayAlphaList_EditDeti" %>


<asp:Content id="cntNo" contentplaceholderid="cphBody" runat="server">
<asp:Panel runat="server" ID="Panel1">
    <div class="page-content-wrap">         
        <div class="row">
            <div class="panel panel-default">
                <div class="panel-body">     
                    <br />
		    <fieldset class="form" id="fsMain">                 
              <div class="form-horizontal">   
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtAlphaDetiNo" ReadOnly="true" runat="server" CssClass="form-control" />                                    
                            </div>
                        </div>                                                                   
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />                                    
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Name of Employee :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" onblur="ResetEmployee()" Placeholder="Type here..." /> 
                                <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                                <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                                TargetControlID="txtFullName" MinimumPrefixLength="2" 
                                CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="3"
                                OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                <script type="text/javascript">
                                    function SplitH(obj, index) {
                                        var items = obj.split("|");
                                        for (i = 0; i < items.length; i++) {
                                            if (i == index) {
                                                return items[i];
                                            }
                                        }
                                    }

                                    function ResetEmployee() {
                                        if (document.getElementById('<%= txtFullName.ClientID %>').value == "") {
                                            document.getElementById('<%= hifEmployeeNo.ClientID %>').value = "0";
                                        }
                                    }

                                    function getRecord(source, eventArgs) {
                                        document.getElementById('<%= hifEmployeeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                    }
                                </script>                                
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Date :</label>
                            <div class="col-md-2">
                                <asp:Textbox ID="txtStartDate" runat="server" CssClass="form-control required" placeholder="From" />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtStartDate" />
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtStartDate" />
                                <asp:CompareValidator runat="server" ID="CompareValidator1" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtStartDate" />
                            </div>
                            <div class="col-md-2">
                                <asp:Textbox ID="txtEndDate" runat="server" CssClass="form-control required" placeholder="To" />                                    
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" TargetControlID="txtEndDate" />
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" ClearTextOnInvalid="true" TargetControlID="txtEndDate" />
                                <asp:CompareValidator runat="server" ID="CompareValidator2" ErrorMessage="<b>Please enter valid entry</b>" Operator="DataTypeCheck" Type="Date" Display="Dynamic" ControlToValidate="txtEndDate" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Applicable Year :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtApplicableYear" runat="server" CssClass="form-control required" />                                    
                            </div>                            
                        </div>    
                                                          
                       

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">&nbsp;</label>
                            <div class="col-md-5">
                                <asp:Checkbox ID="txtIsMWE" runat="server" Text="&nbsp;&nbsp;Tick here if Minimum Wage Earner (MWE)" />                                 
                            </div>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-8">
                                <h5><b>PREVIOUS EMPLOYER INFORMATION</b></h5>
                            </label>
                        </div>
                                                                                                
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Employer Name :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtPrevEmployerName" runat="server" CssClass="form-control" />                                
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Employer Address :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtPrevAddress" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control" />                                
                            </div>
                        </div>
                        <div class="form-group">        
                            <label class="col-md-4 control-label has-space">Employer TIN :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtPrevTINNo" runat="server"  CssClass="form-control" />                                
                            </div>
                        </div>                         
                        

                        <br /> 
                        <div class="form-group">
                            <label class="col-md-8">
                                <h5><b>TNON-TAXABLE/EXEMPT COMPENSATION INCOME</b></h5>
                            </label>
                        </div>     

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Basic Salary(MWE) :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtBasicSalaryMWE" runat="server" CssClass="number form-control" /><br />                                       
                            </div>
                            <label class="col-md-2 control-label-left">(32)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Holiday Pay(MWE) :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtHolidayPayMWE" runat="server" CssClass="number form-control" /><br />                                       
                            </div>
                            <label class="col-md-2 control-label-left">(33)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Overtime Pay(MWE) :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtOTpayMWE" runat="server" CssClass="number form-control" /><br />                                       
                            </div>
                            <label class="col-md-2 control-label-left">(34)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Night Shift Differential (MWE) :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtNPPayMWE" runat="server" CssClass="number form-control" /><br />                                       
                            </div>
                            <label class="col-md-2 control-label-left">(35)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Hazard Pay(MWE) :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtHazardPayMWE" runat="server" CssClass="number form-control" /><br />                                       
                            </div>
                            <label class="col-md-2 control-label-left">(36)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">13th Month Pay and other Benefits :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtnontaxablebonus" runat="server" CssClass="number form-control" /><br />                                       
                            </div>
                            <label class="col-md-2 control-label-left">(37)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">De Minimis Benefits :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtDeminimisIncome" runat="server" CssClass="number form-control" /><br />                                       
                            </div>
                            <label class="col-md-2 control-label-left">(38)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">SSS, GSIS, PHIC, AND Pag-Ibig Contributions, & Union Dues :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtTotalSalaryExemption" runat="server" CssClass="number form-control" /><br /> 
                                <code>Employee shares only</code>                                        
                            </div>
                            <label class="col-md-2 control-label-left">(39)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Salaries and other forms of compensation :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtnontaxableincome" runat="server" CssClass="number form-control" Enabled="false" ReadOnly="true" /><br /> 
                                                                   
                            </div>
                            <label class="col-md-2 control-label-left">(40)</label>
                        </div>


                        <div class="form-group">
                            <label class="col-md-8">
                                <h5><b>TAXABLE COMPENSATION INCOME REGULAR</b></h5>
                            </label>
                        </div>     

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Basic Salary :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtBasicSalary" runat="server" CssClass="number form-control" /><br />
                         
                            </div>
                            <label class="col-md-2 control-label-left">(42)</label>
                            
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Representation Allowance :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtRepresentation" runat="server" CssClass="number form-control" />  
                    
                            </div>
                            <label class="col-md-2 control-label-left">(43)</label>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transportation Allowance :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtTranspoAllowance" runat="server" CssClass="number form-control" />
                                
                            </div>
                            <label class="col-md-2 control-label-left">(44)</label>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Cost of Living Allowance (COLA) :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtCOLA" runat="server" CssClass="number form-control" />
                               
                            </div>
                            <label class="col-md-2 control-label-left">(45)</label>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Fix Housing Allowance :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtHousingAllowance" runat="server" CssClass="number form-control" />      
                                                          
                            </div>
                            <label class="col-md-2 control-label-left">(46)</label>
                        </div>       
                                          
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Other Taxable Non Basic :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txttotaltaxableincomeother" runat="server" CssClass="number form-control" /> 
                                
                            </div>
                            <label class="col-md-2 control-label-left">(47)</label>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Commission :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtcomission" runat="server" CssClass="number form-control" /> 
                                                             
                            </div>
                            <label class="col-md-2 control-label-left">(48)</label>
                        </div>            
                        
                        <div class="form-group">        
                            <label class="col-md-4 control-label has-space">Profit Sharing :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtprofit" runat="server" CssClass="number form-control" /> 
                                                              
                            </div>
                            <label class="col-md-2 control-label-left">(49)</label>
                        </div> 
                                                                           
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Fees including Director's Fees :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtdirfee" runat="server" CssClass="number form-control" />  
                                               
                            </div>
                            <label class="col-md-2 control-label-left">(50)</label>
                        </div> 
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Taxable 13TH Month Pay and Other Benefits :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txttaxablebonus" runat="server" CssClass="number form-control" />  
                                               
                            </div>
                            <label class="col-md-2 control-label-left">(51)</label>
                        </div> 
                                       
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Hazard Pay :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtHazardPayTaxable" runat="server" CssClass="number form-control" />   
                           
                            </div>
                            <label class="col-md-2 control-label-left">(52)</label>
                        </div>         
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Overtime Pay :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtOTPayTaxable" runat="server" CssClass="number form-control" /> 
                                                        
                            </div>
                            <label class="col-md-2 control-label-left">(53) </label>
                        </div> 
                                       
                         
                         
                        <div class="form-group">
                            <label class="col-md-8">
                                <h5><b>Part IV-A SUMMARY</b></h5>
                            </label>
                        </div>  

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Gros Compensation Income (Present Employer item (41)+(55)) :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtgrossincome" runat="server" CssClass="number form-control" Enabled="false" ReadOnly="true" />  
                                                         
                            </div>
                            <label class="col-md-2 control-label-left">(21)</label>
                        </div>  
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Less Total Non-Taxable Income/Exempt Item 41 :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtgrossnontaxableincome" runat="server" CssClass="number form-control" Enabled="false" ReadOnly="true" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(22)</label>
                        </div>  
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Taxable compensation Income present employer from item 55 :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txttaxableincome" runat="server" CssClass="number form-control" Enabled="false" ReadOnly="true" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(23)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Taxable compensation Income from previous employer :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtprevtotaltaxableincome" runat="server" CssClass="number form-control" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(24)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Gross Taxable compensation Income from item 23+24 :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txttaxableincomeTotal" runat="server" CssClass="number form-control" Enabled="false" ReadOnly="true" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(25)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Less Total Exemptions :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtpersonalexemption" runat="server" CssClass="number form-control" Enabled="false" ReadOnly="true" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(26)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Less Premium Paid on health and or hospital insurance :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtinsurancepremium" runat="server" CssClass="number form-control" Enabled="false" ReadOnly="true" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(27)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Net Taxable Income :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtNetTaxableIncome" runat="server" CssClass="number form-control" Enabled="false" ReadOnly="true" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(28)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Tax Due :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txttaxdue" runat="server" CssClass="number form-control" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(29)</label>
                        </div> 
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Tax Jan-Nov(Previous Employer) :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtPrevTaxDueJanNov" runat="server" CssClass="number form-control" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(9A 1604CF)</label>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Tax Jan-Nov(Present Employer) :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtTaxJanNov" runat="server" CssClass="number form-control" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(9A 1604CF)</label>
                        </div> 
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Tax for December :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtTaxDec" runat="server" CssClass="number form-control" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(10A 1604CF)</label>
                        </div> 
                            
                        
                        <div class="form-group">
                            <label class="col-md-8">
                                <h5><b>AMOUNT OF TAXES WITHHELD</b></h5>
                                <label class="col-md-2 control-label-left">(30)</label>
                            </label>
                        </div> 
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Present Employer :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txttaxduepresent" runat="server" CssClass="number form-control" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(30A)</label>
                        </div>
                        <%--<div class="form-group">
                            <label class="col-md-4 control-label has-space">Previous Employer :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtprevtotaltaxwithheld" runat="server" CssClass="number form-control" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(30B)</label>
                        </div>  --%>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Total amount of Taxes as Adjusted :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txttaxdueFinal" runat="server" CssClass="number form-control" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(31)</label>
                        </div> 
                                             
                        <div class="form-group">
                            <label class="col-md-8">
                                <h5><b>NON TAXABLE INCOME FROM PREVIOUS EMPLOYER </b></h5>
                                <label class="col-md-2 control-label-left">(1604CF)</label>
                            </label>
                        </div> 
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">13th Month Pay & Other Benefits :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtPrevNonTaxableBonus" runat="server" CssClass="number form-control" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(4b)</label>
                        </div> 
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">De Minimis Benefits :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtPrevNonTaxableDeminimis" runat="server" CssClass="number form-control" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(4c)</label>
                        </div> 
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">SSS, GSIS,Phic PAG-IBIGContribution,UINION Dues :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtPrevTaxExemption" runat="server" CssClass="number form-control" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(4d)</label>
                        </div> 
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Salaries Other Forms of Compensation :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtprevnontaxableincome" runat="server" CssClass="number form-control" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(4e)</label>
                        </div>

                        <div class="form-group">
                            <label class="col-md-8">
                                <h5><b> TAXABLE INCOME FROM PREVIOUS EMPLOYER </b></h5>
                                <label class="col-md-2 control-label-left">(1604CF)</label>
                            </label>
                        </div> 
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Basic Salary :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtPrevBasicSalary" runat="server" CssClass="number form-control" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(4g)</label>
                        </div>
                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">13th Month Pay & Other Benefits :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtPrevTaxableBonus" runat="server" CssClass="number form-control" />                    
                            </div>
                            <label class="col-md-2 control-label-left">(4h)</label>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label "></label>
                            <div class="col-md-5">                                
                                <asp:Button runat="server"  ID="btnSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick= "btnSave_Click" />
                                <asp:Button runat="server"  ID="btnModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="btnModify_Click" />
                            </div>
                        </div>                                              
                    </div>
                    </fieldset> 
                </div>
                
            </div>
        </div>
    </div>
</asp:Panel>
</asp:Content> 