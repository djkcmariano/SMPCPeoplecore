<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpStandardHeader_EI_Edit.aspx.vb" Inherits="Secured_EmpStandardHeader_EI_Edit" %>


<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">
    
     <uc:Tab runat="server" ID="Tab">
        <Header>        
            <asp:Label runat="server" ID="lbl" /> 
                 
        </Header>
        <content>
            <br />
            <br />

            <asp:Panel runat="server" ID="Panel1">
                <fieldset class="form" id="fsd">
                        <div  class="form-horizontal">

                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Transaction No. :</label>
                                <div class="col-md-6">
                                    <asp:Textbox ID="txtEmployeeEINo" CssClass="form-control" runat="server" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Name of Employee :</label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                                    CompletionInterval="250" ServiceMethod="PopulateEmployee_Encoder" CompletionSetCount="1" 
                                    CompletionListCssClass="autocomplete_completionListElement" 
                                    CompletionListItemCssClass="autocomplete_listItem" 
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                     <script type="text/javascript">
                                         function getRecord(source, eventArgs) {
                                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                                         }
                                     </script>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">Exit Interview Template :</label>
                                <div class="col-md-6">
                                    <asp:Dropdownlist ID="cboApplicantStandardHeaderNo"  runat="server" CssClass="form-control required"></asp:Dropdownlist>
                               </div>
                            </div>                           
                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">Effectivity Date :</label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtEffectivity" runat="server" CssClass="form-control required"
                                        ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                        TargetControlID="txtEffectivity"
                                        Format="MM/dd/yyyy" />  
                                                                          
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                        TargetControlID="txtEffectivity"
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

                                    <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtEffectivity" Operator="DataTypeCheck" Type="Date" ErrorMessage="<b>Please enter valid entry</b>" Display="Dynamic" />
                                                                                                              
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Interview Date :</label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtInterviewDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                        TargetControlID="txtInterviewDate"
                                        Format="MM/dd/yyyy" />  
                                                                          
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                        TargetControlID="txtInterviewDate"
                                        Mask="99/99/9999"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError"
                                        MaskType="Date"
                                        DisplayMoney="Left"
                                        AcceptNegative="Left" 
                                        ErrorTooltipEnabled ="true" 
                                        ClearTextOnInvalid="true" />
                                        
                                    <asp:CompareValidator runat="server" ID="CompareValidator2" ControlToValidate="txtInterviewDate" Operator="DataTypeCheck" Type="Date" ErrorMessage="<b>Please enter valid entry</b>" Display="Dynamic" />                    
                                </div>
                            </div>
                             <div class="form-group">
                                <label class="col-md-3 control-label has-space">Remarks :</label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                               </div>
                            </div>
                             <div class="form-group">
                                <label class="col-md-3 control-label has-required">Status :</label>
                                <div class="col-md-6">
                                    <asp:Dropdownlist ID="cboStatNo"  runat="server" CssClass="form-control required" Enabled="false"></asp:Dropdownlist>
                               </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space"></label>
                                <div class="col-md-6">
                                    <asp:Button runat="server" ID="lnkSave" CssClass="btn btn-default submit fsd" OnClick="lnkSave_Click" Text="Save"></asp:Button>
                                    <asp:Button runat="server" ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" UseSubmitBehavior="false"></asp:Button>
                                </div>
                            </div> 
                            <br />
                    </div>
                    </fieldset >
            </asp:Panel>



        </content>
    </uc:Tab>
    
        
           

</asp:content>