<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="PayPreviousEdit.aspx.vb" Inherits="Secured_PayPreviousEdit" %>


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
                                <asp:Textbox ID="txtPayPreviousNo" ReadOnly="true" runat="server" CssClass="form-control" />                                    
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
                                <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" Placeholder="Type here..." /> 
                                <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                                <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                                TargetControlID="txtFullName" MinimumPrefixLength="2" 
                                CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="3"
                                OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                <script type="text/javascript">
                                    function getRecord(source, eventArgs) {
                                        document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
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
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="ftbe1" TargetControlID="txtApplicableYear" FilterType="Numbers" FilterMode="ValidChars" ValidChars="1234567890" />                              
                            </div>                            
                        </div>    
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">&nbsp;</label>
                            <div class="col-md-5">
                                <asp:Checkbox ID="chkIsAdjustment" runat="server" Text="&nbsp;&nbsp;Tick to adjust only for December" />                                 
                            </div>
                        </div>                                    
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Remarks :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtDescription" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control required" />
                            </div>
                        </div>                         

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space"></label>
                            <div class="col-md-5">
                                <label class="radio-inline">
                                    <asp:RadioButton ID="txtIsAdjustment" GroupName="PreviousIncome" Text="Income Adjustment (YTD)" runat="server" OnCheckedChanged="PreviousEmployer_CheckedChanged" AutoPostBack="true" />
                                </label><br />
                                <label class="radio-inline">
                                    <asp:RadioButton ID="txtIsPreviousEmployer" GroupName="PreviousIncome" Text="Income from Previous Employer" runat="server" OnCheckedChanged="PreviousEmployer_CheckedChanged" AutoPostBack="true"  />
                                </label>
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
                                <ajaxToolkit:FilteredTextBoxExtender runat="server" ID="ftbe2" TargetControlID="txtPrevTINNo" FilterType="Custom" FilterMode="ValidChars" ValidChars="1234567890-" />                                    
                            </div>
                        </div>                         
                        

                        <br /> 

                        <div class="form-group">
                            <label class="col-md-8">
                                <h5><b>TAXABLE INCOME</b></h5>
                            </label>
                        </div>     

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Basic Salary :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtTotalBasicIncome" runat="server" CssClass="number form-control" /><br />
                                <code>Note: For previous employer please add the total salary exemption(39) in the taxable basic(42).</code>  
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtTotalBasicIncome" />                                     
                            </div>
                            <label class="col-md-2 control-label-left">(42) (32)</label>
                            
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Representation Allowance :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtRepAllow" runat="server" CssClass="number form-control" />  
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtRepAllow" />                             
                            </div>
                            <label class="col-md-2 control-label-left">(43)</label>
                        </div> 

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transportation Allowance :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txttranspoAllow" runat="server" CssClass="number form-control" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txttranspoAllow" />
                            </div>
                            <label class="col-md-2 control-label-left">(44)</label>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Cost of Living Allowance (COLA) :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtCola" runat="server" CssClass="number form-control" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtCola" />
                            </div>
                            <label class="col-md-2 control-label-left">(45)</label>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Fix Housing Allowance :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtHousingAllow" runat="server" CssClass="number form-control" />      
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtHousingAllow" />                          
                            </div>
                            <label class="col-md-2 control-label-left">(46)</label>
                        </div>       
                                          
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Other Taxable Non Basic :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtTotalOneTimeTaxableIncomeOther" runat="server" CssClass="number form-control" /> 
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtTotalOneTimeTaxableIncomeOther" />                               
                            </div>
                            <label class="col-md-2 control-label-left">(47)</label>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Commission :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtCommission" runat="server" CssClass="number form-control" /> 
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtCommission" />                               
                            </div>
                            <label class="col-md-2 control-label-left">(48)</label>
                        </div>            
                        
                        <div class="form-group">        
                            <label class="col-md-4 control-label has-space">Profit Sharing :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtProfit" runat="server" CssClass="number form-control" /> 
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtProfit" />                               
                            </div>
                            <label class="col-md-2 control-label-left">(49)</label>
                        </div> 
                                                                           
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Fees including Director's Fees :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtDirFee" runat="server" CssClass="number form-control" />  
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtDirFee" />                              
                            </div>
                            <label class="col-md-2 control-label-left">(50)</label>
                        </div>                
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Hazard Pay :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtHazardpay" runat="server" CssClass="number form-control" />   
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtHazardpay" />                             
                            </div>
                            <label class="col-md-2 control-label-left">(52) (36)</label>
                        </div>         
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Overtime Pay :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtOTPay" runat="server" CssClass="number form-control" /> 
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtOTPay" />                            
                            </div>
                            <label class="col-md-2 control-label-left">(53) (34)</label>
                        </div> 
                                       
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Holiday Pay :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtHolidayPay" runat="server" CssClass="number form-control" />  
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtHolidayPay" />                           
                            </div>
                            <label class="col-md-2 control-label-left">(33)</label>
                        </div>   
                         
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Night Premium Pay :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtNP" runat="server" CssClass="number form-control" />  
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtNP" />                             
                            </div>
                            <label class="col-md-2 control-label-left">(35)</label>
                        </div>   
                                                  
                        
                        
                                         
                        
                        <div class="form-group">
                            <label class="col-md-8">
                                <h5><b>NON-TAXABLE INCOME</b></h5>
                            </label>
                        </div>                       
                                                                
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Deminimis Benefits :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtDeminimis" runat="server" CssClass="number form-control" /> 
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtDeminimis" />                               
                            </div>
                            <label class="col-md-2 control-label-left">(38)</label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Total Salary Exemption :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtTaxExemption" runat="server" CssClass="number form-control" />
                                <%--<br /><code>Salary Exemption are SSS, SSS, PHIC, Pagibig, Union Dues & Others(Abs, Late, Undertime).</code> --%> 
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtTaxExemption" />                                   
                            </div>
                            <label class="col-md-2 control-label-left">(39)</label>
                        </div> 
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Other Non Taxable Income :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtTotalNontaxableIncomeOther" runat="server" CssClass="number form-control" />  
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtTotalNontaxableIncomeOther" />                              
                            </div>
                            <label class="col-md-2 control-label-left">(40)</label>
                        </div>      

                        <div class="form-group">
                            <label class="col-md-8">
                                <h5><b>OTHERS</b></h5>
                            </label>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">13th Month Pay and Other Benefits (Accum/Bonus) :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtBonus" runat="server"  CssClass="number form-control" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtBonus" />
                            </div>
                            <label class="col-md-2 control-label-left">(37) + (51)</label>
                        </div>  
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Total Tax Withheld :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtTaxWithheld" runat="server"  CssClass="number form-control" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txtTaxWithheld" />
                            </div>
                         </div>
                        
                        <div class="form-group">       
                            <label class="col-md-4 control-label has-space">Initial 13th Month Released :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtTotalAccumIncome" runat="server" CssClass="number form-control" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" FilterType="Custom, Numbers" ValidChars="-." TargetControlID="txttotalaccumincome" />
                            </div>
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