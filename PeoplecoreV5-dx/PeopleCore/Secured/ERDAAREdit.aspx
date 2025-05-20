<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="ERDAAREdit.aspx.vb" Inherits="Secured_ERDAAREdit" %>

<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">
<uc:Tab runat="server" ID="Tab">
    <Content>

    <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-body">
                <asp:Panel runat="server" ID="Panel1">
                    <fieldset class="form" id="fsMain">
                        <div  class="form-horizontal">
                            <div class="form-group" style="visibility:hidden; position:absolute;">
                                <div class="col-md-5">
                                    <asp:checkbox ID="txtIsPosted" runat="server"></asp:checkbox>
                                </div>
                            </div>

                            <div class="form-group" style="display:none;">
                                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                                <div class="col-md-7">
                                    <asp:Textbox ID="txtDAARNo" ReadOnly="true" runat="server" Enabled="false" CssClass="form-control" ></asp:Textbox>
                                </div>
                            </div> 

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Transaction No. :</label>
                                <div class="col-md-5">
                                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Complaint/Adverse Report No. :</label>
                                <div class="col-md-5">
                                     <asp:Textbox ID="txtDocketNo" runat="server"  CssClass="form-control" ></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group" style=" display:none">
                                <label class="col-md-4 control-label has-required">Issued To :</label>
                                <div class="col-md-3">
                                   <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" Placeholder="Type here..." style="display:inline-block;" onblur="ResetEmployeeNo()" /> 
                                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                    CompletionListCssClass="autocomplete_completionListElement" 
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    OnClientItemSelected="getMain1" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                </div>
                                <div class="col-md-2">
                                   <asp:TextBox runat="server" ID="txtEmployeeCode" CssClass="form-control" Enabled="false" ReadOnly="true" style="display:inline-block;" />                                     
                                </div>
                                <script type="text/javascript">

                                    function SplitH(obj, index) {
                                        var items = obj.split("|");
                                        for (i = 0; i < items.length; i++) {
                                            if (i == index) {
                                                return items[i];
                                            }
                                        }
                                    }

                                    function getMain1(source, eventArgs) {
                                        document.getElementById('<%= hifEmployeeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                        document.getElementById('<%= txtEmployeeCode.ClientID %>').value = SplitH(eventArgs.get_value(), 12);
                                        document.getElementById('<%= txtImmediateSuperiorDesc.ClientID %>').value = SplitH(eventArgs.get_value(), 14);
                                        document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = SplitH(eventArgs.get_value(), 13);
                                    }

                                    function ResetEmployeeNo() {
                                        if (document.getElementById('<%= txtFullName.ClientID %>').value == "") {
                                            document.getElementById('<%= hifEmployeeNo.ClientID %>').value = "0";
                                            document.getElementById('<%= txtEmployeeCode.ClientID %>').value = "";
                                            document.getElementById('<%= txtImmediateSuperiorDesc.ClientID %>').value = "";
                                            document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = "0";
                                        }
                                    } 
                                </script>

                            </div>
                            <div class="form-group" style=" display:none">
                                <label class="col-md-4 control-label has-space">Immediate Superior :</label>
                                <div class="col-md-5">
                                     <asp:Textbox ID="txtImmediateSuperiorDesc" runat="server"  CssClass="form-control" Enabled="false" ReadOnly="true" />
                                     <asp:HiddenField ID="hifImmediateSuperiorNo" runat="server" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Date Issued :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDAARDate" runat="server" CssClass="required form-control"  >
                                     </asp:TextBox> 
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                        TargetControlID="txtDAARDate"
                                        Format="MM/dd/yyyy" />  
                                                                          
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                        TargetControlID="txtDAARDate"
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
                                                                        
                                            <asp:RangeValidator
                                        ID="RangeValidator2"
                                        runat="server"
                                        ControlToValidate="txtDAARDate"
                                        ErrorMessage="<b>Please enter valid entry</b>"
                                        MinimumValue="1900-01-01"
                                        MaximumValue="3000-12-31"
                                        Type="Date" Display="None"  />
                                                                        
                                        <ajaxToolkit:ValidatorCalloutExtender 
                                        runat="Server" 
                                        ID="ValidatorCalloutExtender8"
                                        TargetControlID="RangeValidator2" /> 
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Issued By :</label>
                                <div class="col-md-5">
                                   <asp:TextBox runat="server" ID="txtComplainantName" CssClass="form-control required" Placeholder="Type here..." style="display:inline-block;" onblur="ResetComplainantNo()" /> 
                                    <asp:HiddenField runat="server" ID="hifComplainantNo"/>
                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                                    TargetControlID="txtComplainantName" MinimumPrefixLength="2" 
                                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                    CompletionListCssClass="autocomplete_completionListElement" 
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    OnClientItemSelected="getMain2" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                </div>

                                <script type="text/javascript">


                                    function getMain2(source, eventArgs) {
                                        document.getElementById('<%= hifComplainantNo.ClientID %>').value = eventArgs.get_value();
                                    }

                                    function ResetComplainantNo() {
                                        if (document.getElementById('<%= txtComplainantName.ClientID %>').value == "") {
                                            document.getElementById('<%= hifComplainantNo.ClientID %>').value = "0";
                                        }
                                    } 
                                </script>

                            </div>

                            <div class="form-group" style="display:none;">
                                <label class="col-md-4 control-label has-space">Case Category :</label>
                                <div class="col-md-5">
                                    <asp:Dropdownlist ID="cboDAARTypeNo" DataMember="EDAARType" runat="server" CssClass="form-control" 
                                        ></asp:Dropdownlist>
                                </div>
                            </div>

                            <br />
                            <div class="form-group">  
                                <h5 class="col-md-8">
                                    <label class="control-label">OFFENSE&nbsp;&nbsp;</label>
                                </h5>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Case Type :</label>
                                <div class="col-md-5">
                                    <asp:Dropdownlist ID="cboDACaseTypeNo" DataMember="EDACaseType" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">DA Policy Type :</label>
                                <div class="col-md-5">
                                    <asp:Dropdownlist ID="cboDAPolicyTypeNo" DataMember="EDAPolicyType" runat="server" CssClass="required form-control" AutoPostBack="true" OnSelectedIndexChanged="cboDAPolicyTypeNo_SelectedIndexChanged" />                                       
                                </div>
                            </div>
                            
                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">DA Policy :</label>
                                <div class="col-md-5">
                                    <asp:Dropdownlist ID="cboDAPolicyNo" runat="server" CssClass="required form-control" AutoPostBack="true" OnSelectedIndexChanged="cboDAPolicyNo_SelectedIndexChanged" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">&nbsp;</label>
                                <div class="col-md-5">
                                    <asp:TextBox runat="server" ID="txtDAPolicyDesc" TextMode="MultiLine" Rows="5" ReadOnly="true" CssClass="form-control" />
                                </div>
                            </div>
                            
                            

                            <br />
                            <div class="form-group">  
                                <h5 class="col-md-8">
                                    <label class="control-label">INCIDENT&nbsp;&nbsp;INFORMATION</label>
                                </h5>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Date of incident :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtOccurenceDate" runat="server" CssClass="required form-control"  >
                                     </asp:TextBox> 
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server"
                                        TargetControlID="txtOccurenceDate"
                                        Format="MM/dd/yyyy" />  
                                                                          
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender10" runat="server"
                                        TargetControlID="txtOccurenceDate"
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
                                                                        
                                            <asp:RangeValidator
                                        ID="RangeValidator10"
                                        runat="server"
                                        ControlToValidate="txtOccurenceDate"
                                        ErrorMessage="<b>Please enter valid entry</b>"
                                        MinimumValue="1900-01-01"
                                        MaximumValue="3000-12-31"
                                        Type="Date" Display="None"  />
                                                                        
                                        <ajaxToolkit:ValidatorCalloutExtender 
                                        runat="Server" 
                                        ID="ValidatorCalloutExtender1"
                                        TargetControlID="RangeValidator10" /> 
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Time of incident :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtOccurenceTime" runat="server" CssClass="required form-control" Placeholder="00:00" ></asp:TextBox>
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4x" runat="server"
                                        TargetControlID="txtOccurenceTime" 
                                        Mask="99:99"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError"
                                        MaskType="Time"
                                        AcceptAMPM="false" 
                            
                                        CultureName="en-US" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                        ControlExtender="MaskedEditExtender4x"
                                        ControlToValidate="txtOccurenceTime"
                                        IsValidEmpty="true"
                                        EmptyValueMessage=""
                                        InvalidValueMessage=""
                                        ValidationGroup="Demo1"
                                        Display="Dynamic"
                                        TooltipMessage=""
                            
                                            />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">What is the incident :</label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtDAARDesc" runat="server" TextMode="MultiLine" Rows="3" CssClass="required form-control" > 
                                     </asp:TextBox> 
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Location of incident :</label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtDAARLocation" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" > 
                                     </asp:TextBox> 
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Potential impact :<br /><b>(Property Loss/Damage)</b></label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtDAARImpact" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" > 
                                     </asp:TextBox> 
                                </div>
                            </div>
                            <div class="form-group" style="display:none">
                                <label class="col-md-4 control-label has-space">Incident Type :</label>
                                <div class="col-md-5">                                    
                                     <asp:DropDownList ID="cboERIncidentTypeNo" runat="server" CssClass="form-control" DataMember="EERIncidentType" />
                                </div>
                            </div>
                            <div class="form-group" style=" display:none">
                                <label class="col-md-4 control-label has-space">Incident Classification :</label>
                                <div class="col-md-5">                                    
                                     <asp:DropDownList ID="cboERIncidentClassNo" runat="server" CssClass="form-control" DataMember="EERIncidentClass" />
                                </div>
                            </div>                                                       
                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">How did the incident happen :</label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtDAARHappen" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" > 
                                     </asp:TextBox> 
                                </div>
                            </div>                            
                            <div class="form-group" style=" display:none">
                                <label class="col-md-4 control-label has-space">Amount involved <em>if any</em> :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" > 
                                     </asp:TextBox> 
                                     <ajaxToolkit:FilteredTextBoxExtender
                                    ID="FilteredTextBoxExtender7"
                                    runat="server"
                                    TargetControlID="txtAmount"
                                    FilterType="Numbers, Custom" ValidChars="." />  
                               </div>
                            </div>

                            <br />
                            <div class="form-group">  
                                <h5 class="col-md-8">
                                    <label class="control-label">INVESTIGATION RESULT</label>
                                </h5>
                            </div>

                            

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Assigned To :</label>
                                <div class="col-md-5">
                                   <asp:TextBox runat="server" ID="txtAssignedByName" CssClass="form-control" Placeholder="Type here..." style="display:inline-block;" onblur="ResetAssignedByNo()" /> 
                                    <asp:HiddenField runat="server" ID="hifAssignedByNo"/>
                                    <ajaxToolkit:AutoCompleteExtender ID="aceAssignedByName" runat="server"  
                                    TargetControlID="txtAssignedByName" MinimumPrefixLength="2" 
                                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                    CompletionListCssClass="autocomplete_completionListElement" 
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    OnClientItemSelected="getMain4" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                </div>

                                <script type="text/javascript">
                                    function getMain4(source, eventArgs) {
                                        document.getElementById('<%= hifAssignedByNo.ClientID %>').value = eventArgs.get_value();
                                    }

                                    function ResetAssignedByNo() {
                                        if (document.getElementById('<%= txtAssignedByName.ClientID %>').value == "") {
                                            document.getElementById('<%= hifAssignedByNo.ClientID %>').value = "0";
                                        }
                                    } 
                                </script>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Date result/s received :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtReceivedEvalDate" runat="server" CssClass="form-control"   >
                                     </asp:TextBox> 
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server"
                                            TargetControlID="txtReceivedEvalDate"
                                            Format="MM/dd/yyyy" />  
                                                                          
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server"
                                            TargetControlID="txtReceivedEvalDate"
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
                                                                        
                                                <asp:RangeValidator
                                            ID="RangeValidator6"
                                            runat="server"
                                            ControlToValidate="txtReceivedEvalDate"
                                            ErrorMessage="<b>Please enter valid entry</b>"
                                            MinimumValue="1900-01-01"
                                            MaximumValue="3000-12-31"
                                            Type="Date" Display="None"  />
                                                                        
                                            <ajaxToolkit:ValidatorCalloutExtender 
                                            runat="Server" 
                                            ID="ValidatorCalloutExtender12"
                                            TargetControlID="RangeValidator6" /> 
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space"> Investigation result/s :</label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtBI" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" > 
                                     </asp:TextBox> 
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Date From :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtInvestigateStartDate" runat="server" CssClass="form-control" />                                    
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtInvestigateStartDate" Format="MM/dd/yyyy" />                                                                            
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                        TargetControlID="txtInvestigateStartDate"
                                        Mask="99/99/9999"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError"
                                        MaskType="Date"                                            
                                        ErrorTooltipEnabled ="true" 
                                        ClearTextOnInvalid="true" />
                                    <asp:CompareValidator ID="CompareValidator" runat="server" 
                                    ControlToValidate="txtInvestigateStartDate" ErrorMessage="<b>Please enter valid entry</b>"
                                    Type="Date" Operator="DataTypeCheck" Display="Dynamic" />                                            
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Date To :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtInvestigateEndDate" runat="server" CssClass="form-control" />                                    
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtInvestigateEndDate" Format="MM/dd/yyyy" />                                                                            
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                        TargetControlID="txtInvestigateEndDate"
                                        Mask="99/99/9999"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError"
                                        MaskType="Date"                                            
                                        ErrorTooltipEnabled ="true" 
                                        ClearTextOnInvalid="true" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                    ControlToValidate="txtInvestigateEndDate" ErrorMessage="<b>Please enter valid entry</b>"
                                    Type="Date" Operator="DataTypeCheck" Display="Dynamic" />                                            
                                </div>
                            </div>                            
                            <div class="form-group" style="display:none;">
                                <label class="col-md-4 control-label has-space">Comments :</label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" > 
                                     </asp:TextBox> 
                                </div>
                            </div>

                            <div class="form-group" style="display:none;">
                                <label class="col-md-4 control-label has-space">Witnesses :</label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" > 
                                     </asp:TextBox> 
                                </div>
                            </div> 

                            <br />
                            <div class="form-group">  
                                <h5 class="col-md-8">
                                    <label class="control-label">EVALUATION</label>
                                </h5>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Summary of Evaluation :</label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtEvaluation" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" > 
                                     </asp:TextBox> 
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Evaluated By :</label>
                                <div class="col-md-5">
                                   <asp:TextBox runat="server" ID="txtEvaluationByName" CssClass="form-control" Placeholder="Type here..." style="display:inline-block;" onblur="ResetEvaluationByNo()" /> 
                                    <asp:HiddenField runat="server" ID="hifEvaluationByNo"/>
                                    <ajaxToolkit:AutoCompleteExtender ID="aceEvaluationByName" runat="server"  
                                    TargetControlID="txtEvaluationByName" MinimumPrefixLength="2" 
                                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                    CompletionListCssClass="autocomplete_completionListElement" 
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    OnClientItemSelected="getMain5" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                </div>

                                <script type="text/javascript">
                                    function getMain5(source, eventArgs) {
                                        document.getElementById('<%= hifEvaluationByNo.ClientID %>').value = eventArgs.get_value();
                                    }

                                    function ResetEvaluationByNo() {
                                        if (document.getElementById('<%= txtEvaluationByName.ClientID %>').value == "") {
                                            document.getElementById('<%= hifEvaluationByNo.ClientID %>').value = "0";
                                        }
                                    } 
                                </script>
                                
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Date Evaluated :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtEvaluationDate" runat="server" CssClass="form-control"   >
                                     </asp:TextBox> 
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server"
                                            TargetControlID="txtEvaluationDate"
                                            Format="MM/dd/yyyy" />  
                                                                          
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" runat="server"
                                            TargetControlID="txtEvaluationDate"
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
                                                                        
                                            <asp:RangeValidator
                                            ID="RangeValidator7"
                                            runat="server"
                                            ControlToValidate="txtEvaluationDate"
                                            ErrorMessage="<b>Please enter valid entry</b>"
                                            MinimumValue="1900-01-01"
                                            MaximumValue="3000-12-31"
                                            Type="Date" Display="None"  />
                                                                        
                                            <ajaxToolkit:ValidatorCalloutExtender 
                                            runat="Server" 
                                            ID="ValidatorCalloutExtender13"
                                            TargetControlID="RangeValidator7" /> 
                                </div>
                            </div>

                            <br />
                            <div class="form-group">  
                                <h5 class="col-md-8">
                                    <label class="control-label">RECOMMENDATION</label>
                                </h5>
                            </div>                             

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Summary of Recommendation :</label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtRecommendation" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" > 
                                     </asp:TextBox> 
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Recommended By :</label>
                                <div class="col-md-5">
                                   <asp:TextBox runat="server" ID="txtRecommendedByName" CssClass="form-control" Placeholder="Type here..." style="display:inline-block;" onblur="ResetRecommendedByNo()" /> 
                                    <asp:HiddenField runat="server" ID="hifRecommendedByNo"/>
                                    <ajaxToolkit:AutoCompleteExtender ID="aceRecommendedByName" runat="server"  
                                    TargetControlID="txtRecommendedByName" MinimumPrefixLength="2" 
                                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                    CompletionListCssClass="autocomplete_completionListElement" 
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    OnClientItemSelected="getMain6" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                </div>

                                <script type="text/javascript">
                                    function getMain6(source, eventArgs) {
                                        document.getElementById('<%= hifRecommendedByNo.ClientID %>').value = eventArgs.get_value();
                                    }
                                    function ResetRecommendedByNo() {
                                        if (document.getElementById('<%= txtRecommendedByName.ClientID %>').value == "") {
                                            document.getElementById('<%= hifRecommendedByNo.ClientID %>').value = "0";
                                        }
                                    } 
                                </script>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Date of Recommendation :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtRecommendationDate" runat="server" CssClass="form-control"   >
                                     </asp:TextBox> 
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server"
                                            TargetControlID="txtRecommendationDate"
                                            Format="MM/dd/yyyy" />  
                                                                          
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" runat="server"
                                            TargetControlID="txtRecommendationDate"
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
                                                                        
                                                <asp:RangeValidator
                                            ID="RangeValidator8"
                                            runat="server"
                                            ControlToValidate="txtRecommendationDate"
                                            ErrorMessage="<b>Please enter valid entry</b>"
                                            MinimumValue="1900-01-01"
                                            MaximumValue="3000-12-31"
                                            Type="Date" Display="None"  />
                                                                        
                                            <ajaxToolkit:ValidatorCalloutExtender 
                                            runat="Server" 
                                            ID="ValidatorCalloutExtender14"
                                            TargetControlID="RangeValidator8" /> 
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space"></label>
                                <div class="col-md-5">
                                    <asp:CheckBox ID="chkIsForDA" runat="server" Text="&nbsp;Tick if for disciplinary action" />
                                </div>
                            </div>

                            <br />
                            <div class="form-group">  
                                <h5 class="col-md-8">
                                    <label class="control-label">OTHER INFORMATION</label>
                                </h5>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Other Person Involved</label>
                                <div class="col-md-5">
                                    <asp:TextBox runat="server" ID="txtOtherPerson" CssClass="form-control" TextMode="MultiLine" Rows="3"  />
                                </div>
                            </div>

                            <div class="form-group" style="display:none">
                                <label class="col-md-4 control-label has-space">Project</label>
                                <div class="col-md-5">
                                    <asp:DropDownList ID="cboProjectNo" runat="server" CssClass="form-control" DataMember="EProject" />
                                </div>
                            </div>

                            <div class="form-group" style="visibility:hidden;position:absolute;">
                                <label class="col-md-4 control-label has-space"></label>
                                <div class="col-md-5">
                                    <asp:CheckBox ID="chkIsNTESubmit" runat="server" Text="&nbsp;Please check here if received." />
                                </div>
                            </div> 

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Received By :</label>
                                <div class="col-md-5">
                                    <asp:TextBox runat="server" ID="txtReceivedByName" CssClass="form-control" Placeholder="Type here..." style="display:inline-block;" onblur="ResetReceivedByNo()" /> 
                                    <asp:HiddenField runat="server" ID="hifReceivedByNo"/>
                                    <ajaxToolkit:AutoCompleteExtender ID="aceReceivedByName" runat="server"  
                                    TargetControlID="txtReceivedByName" MinimumPrefixLength="2" 
                                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                    CompletionListCssClass="autocomplete_completionListElement" 
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    OnClientItemSelected="getMain3" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                </div>

                                <script type="text/javascript">
                                    function getMain3(source, eventArgs) {
                                        document.getElementById('<%= hifReceivedByNo.ClientID %>').value = eventArgs.get_value();
                                    }

                                    function ResetReceivedByNo() {
                                        if (document.getElementById('<%= txtReceivedByName.ClientID %>').value == "") {
                                            document.getElementById('<%= hifReceivedByNo.ClientID %>').value = "0";
                                        }
                                    } 
                                </script>

                                
                           </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Date Received :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="form-control" >
                                     </asp:TextBox> 
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server"
                                            TargetControlID="txtReceivedDate"
                                            Format="MM/dd/yyyy" />  
                                                                          
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                            TargetControlID="txtReceivedDate"
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
                                                                        
                                                <asp:RangeValidator
                                            ID="RangeValidator5"
                                            runat="server"
                                            ControlToValidate="txtReceivedDate"
                                            ErrorMessage="<b>Please enter valid entry</b>"
                                            MinimumValue="1900-01-01"
                                            MaximumValue="3000-12-31"
                                            Type="Date" Display="None"  />
                                                                        
                                            <ajaxToolkit:ValidatorCalloutExtender 
                                            runat="Server" 
                                            ID="ValidatorCalloutExtender11"
                                            TargetControlID="RangeValidator5" /> 
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Approved By :</label>
                                <div class="col-md-5">
                                   <asp:TextBox runat="server" ID="txtApprovedByName" CssClass="form-control" Placeholder="Type here..." style="display:inline-block;" onblur="ResetApprovedByNo()" /> 
                                    <asp:HiddenField runat="server" ID="hifApprovedByNo"/>
                                    <ajaxToolkit:AutoCompleteExtender ID="aceApprovedByName" runat="server"  
                                    TargetControlID="txtApprovedByName" MinimumPrefixLength="2" 
                                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
                                    CompletionListCssClass="autocomplete_completionListElement" 
                                    CompletionListItemCssClass="autocomplete_listItem" CompletionSetCount="1"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    OnClientItemSelected="getMain7" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                </div>

                                <script type="text/javascript">
                                    function getMain7(source, eventArgs) {
                                        document.getElementById('<%= hifApprovedByNo.ClientID %>').value = eventArgs.get_value();
                                    }
                                    function ResetApprovedByNo() {
                                        if (document.getElementById('<%= txtApprovedByName.ClientID %>').value == "") {
                                            document.getElementById('<%= hifApprovedByNo.ClientID %>').value = "0";
                                        }
                                    } 
                                </script>
                            </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Date Approved :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtApprovedDate" runat="server" CssClass="form-control"   >
                                     </asp:TextBox> 
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server"
                                            TargetControlID="txtApprovedDate"
                                            Format="MM/dd/yyyy" />  
                                                                          
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender9" runat="server"
                                            TargetControlID="txtApprovedDate"
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
                                                                        
                                                <asp:RangeValidator
                                            ID="RangeValidator9"
                                            runat="server"
                                            ControlToValidate="txtApprovedDate"
                                            ErrorMessage="<b>Please enter valid entry</b>"
                                            MinimumValue="1900-01-01"
                                            MaximumValue="3000-12-31"
                                            Type="Date" Display="None"  />
                                                                        
                                            <ajaxToolkit:ValidatorCalloutExtender 
                                            runat="Server" 
                                            ID="ValidatorCalloutExtender15"
                                            TargetControlID="RangeValidator9" /> 
                                </div>
                            </div>
                            <div class="form-group" style="display:none;">
                                <label class="col-md-4 control-label has-space">Status :</label>
                                <div class="col-md-5">
                                    <asp:Dropdownlist ID="cboDAARStatNo" DataMember="EDAARStat" runat="server" CssClass="form-control" 
                                        ></asp:Dropdownlist>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Mediation Date :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtMediationDate" runat="server" CssClass="form-control" />                                    
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtMediationDate" Format="MM/dd/yyyy" />                                                                            
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                        TargetControlID="txtMediationDate"
                                        Mask="99/99/9999"
                                        MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError"
                                        MaskType="Date"                                            
                                        ErrorTooltipEnabled ="true" 
                                        ClearTextOnInvalid="true" />
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                    ControlToValidate="txtMediationDate" ErrorMessage="<b>Please enter valid entry</b>"
                                    Type="Date" Operator="DataTypeCheck" Display="Dynamic" />                                            
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Person Involved in the mediation :</label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtPersonInvolvedMediation" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" > 
                                     </asp:TextBox> 
                                </div>
                            </div>

                            <div class="form-group" style="display:none">
                                <label class="col-md-4 control-label has-required">Status :</label>
                                <div class="col-md-5">
                                    <%--<asp:Dropdownlist ID="cboApprovalStatNo" DataMember="EApprovalStatL" runat="server" CssClass="required form-control"></asp:Dropdownlist>--%>
                                    <asp:Dropdownlist ID="cboApprovalStatNo" runat="server" CssClass="required form-control"></asp:Dropdownlist>
                                </div>
                            </div>

                            

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space"></label>
                                <div class="col-md-5">
                                    <div>
                                        <asp:Button runat="server"  ID="lnkSubmit" CssClass="btn btn-default submit fsMain" Text="Save" OnClick= "btnSave_Click" ></asp:Button>
                                        <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify"></asp:Button>
                                    </div>
                                </div>
                            </div> 
                        
                      
                    </div>
                    </fieldset>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>

    </Content>
</uc:Tab>



</asp:content> 