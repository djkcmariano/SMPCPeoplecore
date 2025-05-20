<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="ERDAEdit.aspx.vb" Inherits="Secured_ERDAEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">

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
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Transaction No. :</label>
                                <div class="col-md-6">
                                    <asp:Textbox ID="txtDA" ReadOnly="true" runat="server" CssClass="form-control"></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Transaction No. :</label>
                                <div class="col-md-6">
                                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Administrative Case No. :</label>
                                <div class="col-md-6">
                                    <asp:Textbox ID="txtDACode" runat="server" CssClass="form-control" ></asp:Textbox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">Date Created :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtReceivedDate" runat="server"  CssClass="form-control required"  ></asp:TextBox> 
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtReceivedDate" Format="MM/dd/yyyy" />  
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtReceivedDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true"  />
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtReceivedDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender8" TargetControlID="RangeValidator2" /> 
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Complaint/Adverse Report No. :</label>
                                <div class="col-md-6">
                                    <asp:Dropdownlist ID="cboDAARDetlNo"  runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboDAARNo_SelectedIndexChanged" CssClass="form-control" ></asp:Dropdownlist>
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">Name of Employee :</label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" Placeholder="Type here..." style="display:inline-block;" /> 
                                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                                    <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                                    CompletionInterval="250" ServiceMethod="PopulateEmployee" CompletionSetCount="0" 
                                    CompletionListCssClass="autocomplete_completionListElement" 
                                    CompletionListItemCssClass="autocomplete_listItem" 
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                     <script type="text/javascript">
                                         function Split(obj, index) {
                                             var items = obj.split("|");
                                             for (i = 0; i < items.length; i++) {
                                                 if (i == index) {
                                                     return items[i];
                                                 }
                                             }
                                         }
                                         function getRecord(source, eventArgs) {
                                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
                                             document.getElementById('<%= cboDepartmentNo.ClientID %>').value = Split(eventArgs.get_value(), 1);
                                             document.getElementById('<%= cboRankNo.ClientID %>').value = Split(eventArgs.get_value(), 4);
                                             document.getElementById('<%= cboGroupNo.ClientID %>').value = Split(eventArgs.get_value(), 5);
                                             document.getElementById('<%= cboSectionNo.ClientID %>').value = Split(eventArgs.get_value(), 6);
                                             document.getElementById('<%= cboUnitNo.ClientID %>').value = Split(eventArgs.get_value(), 7);
                                             document.getElementById('<%= cboPositionNo.ClientID %>').value = Split(eventArgs.get_value(), 8);
                                             document.getElementById('<%= txtBirthAge.ClientID %>').value = Split(eventArgs.get_value(), 9);
                                             document.getElementById('<%= cboGenderNo.ClientID %>').value = Split(eventArgs.get_value(), 10);
                                             document.getElementById('<%= cboFacilityNo.ClientID %>').value = Split(eventArgs.get_value(), 11);
                                         }
                                            </script>
                                </div>
                                <div class="col-md-2" style=" display:none">
                                    <asp:LinkButton runat="server" ID="lnkInfo" OnClick="lnkInfo_Click" CausesValidation="false">
                                        <i class="fa fa-info fa-1x"></i>&nbsp;&nbsp;Employee Profile
                                    </asp:LinkButton>
                                </div>
                            </div>
                            
                            
                            <br />
                            <div class="form-group">  
                                <h5 class="col-md-8">
                                    <label class="control-label">OFFENSE COMMITTED</label>
                                </h5>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">Date Committed :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtViolationDate" runat="server"  CssClass="required form-control"  ></asp:TextBox> 
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtViolationDate" Format="MM/dd/yyyy" />  
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtViolationDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true"  />
                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtViolationDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender9" TargetControlID="RangeValidator3" /> 
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">DA Policy Type :</label>
                                <div class="col-md-6">
                                    <asp:Dropdownlist ID="cboDAPolicyTypeNo" DataMember="EDAPolicyType" runat="server" CssClass="required form-control" AutoPostBack="true" OnSelectedIndexChanged="cboDAPolicyTypeNo_SelectedIndexChanged"></asp:Dropdownlist>
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">DA Policy :</label>
                                <div class="col-md-6">
                                    <asp:Dropdownlist ID="cboDAPolicyNo" runat="server" CssClass="required form-control" AutoPostBack="true" OnSelectedIndexChanged="cboDAPolicyNo_SelectedIndexChanged"></asp:Dropdownlist>
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Case Type :</label>
                                <div class="col-md-6">
                                    <asp:Dropdownlist ID="cboDACaseTypeNo" DataMember="EDACaseType" runat="server"  CssClass="form-control"></asp:Dropdownlist>
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">No. of Offense :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList runat="server" ID="cboOffenseCount" DataMember="EOffenseCount" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="txtOffenseCount_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div> 
                            <br />
                            <div class="form-group">  
                                <h5 class="col-md-8">
                                    <label class="control-label">PENALTY IMPOSED</label>
                                </h5>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">Penalty Type :</label>
                                <div class="col-md-6">
                                    <asp:Dropdownlist ID="cboDATypeNo" DataMember="EDAType" runat="server"  CssClass="required form-control" AutoPostBack="true" OnSelectedIndexChanged="txtDATypeNo_SelectedIndexChanged"></asp:Dropdownlist>
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label"></label>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="chkIsDismissal" runat="server" Text="&nbsp;Tick if employee is for Dismissal" />
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label"></label>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="chkIsSuspension" runat="server" Text="&nbsp;Tick if employee is for Suspension" />
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label">No. of Day(s) of Suspension :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtNoOfDays" runat="server" CssClass="form-control"> </asp:TextBox> 
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtNoOfDays" FilterType="Numbers, Custom"/>  
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label">Date Imposed :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" Placeholder="Start Date" ></asp:TextBox> 
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtStartDate" Format="MM/dd/yyyy" />  
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtStartDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true"  />
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtStartDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator1" /> 
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" Placeholder="End Date" ></asp:TextBox> 
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEndDate" Format="MM/dd/yyyy" />  
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtEndDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true"  />
                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtEndDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RangeValidator4" /> 
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label">Remarks :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"> 
                                     </asp:TextBox> 
                                </div>
                            </div> 
                            <br />
                            <div class="form-group">  
                                <h5 class="col-md-8">
                                    <label class="control-label">OTHER INFORMATON</label>
                                </h5>
                            </div>

                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Date of Formal/Notice of Charge :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtNoticeDate" runat="server" CssClass="form-control" />             
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender11" runat="server" TargetControlID="txtNoticeDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender11" runat="server" TargetControlID="txtNoticeDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"  ErrorTooltipEnabled ="true"  ClearTextOnInvalid="true"   />
                                    <asp:RangeValidator ID="RangeValidator11" runat="server" ControlToValidate="txtNoticeDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                                    <ajaxToolkit:ValidatorCalloutExtender  runat="Server"  ID="ValidatorCalloutExtender7" TargetControlID="RangeValidator11" />
                                </div>
                            </div> 
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Date of Receipt of Formal/Notice of Charge :</label>
                                <div class="col-md-2">
                                   
                                    <asp:TextBox ID="txtNoticeRecvDate" runat="server" CssClass="form-control" />             
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender9" runat="server" TargetControlID="txtNoticeRecvDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender9" runat="server" TargetControlID="txtNoticeRecvDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"  ErrorTooltipEnabled ="true"  ClearTextOnInvalid="true"   />
                                    <asp:RangeValidator ID="RangeValidator9" runat="server" ControlToValidate="txtNoticeRecvDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                                    <ajaxToolkit:ValidatorCalloutExtender  runat="Server"  ID="ValidatorCalloutExtender10" TargetControlID="RangeValidator9" />
                                </div>
                            </div> 
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Date of Request for Extension <em>if any</em> :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtReqExtFromDate" runat="server" CssClass="form-control" Placeholder="From" />             
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender10" runat="server" TargetControlID="txtReqExtFromDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender10" runat="server" TargetControlID="txtReqExtFromDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"  ErrorTooltipEnabled ="true"  ClearTextOnInvalid="true"   />
                                    <asp:RangeValidator ID="RangeValidator10" runat="server" ControlToValidate="txtReqExtFromDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                                    <ajaxToolkit:ValidatorCalloutExtender  runat="Server"  ID="ValidatorCalloutExtender11" TargetControlID="RangeValidator10" />
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtReqExtToDate" runat="server" CssClass="form-control" Placeholder="To" />             
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender12" runat="server" TargetControlID="txtReqExtToDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender12" runat="server" TargetControlID="txtReqExtToDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"  ErrorTooltipEnabled ="true"  ClearTextOnInvalid="true"   />
                                    <asp:RangeValidator ID="RangeValidator12" runat="server" ControlToValidate="txtReqExtToDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                                    <ajaxToolkit:ValidatorCalloutExtender  runat="Server"  ID="ValidatorCalloutExtender12" TargetControlID="RangeValidator12" />
                                </div>
                            </div> 
                            
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Date of Request for Extension is Granted :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtReqExtGrantFromDate" runat="server" CssClass="form-control" Placeholder="From" />             
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender13" runat="server" TargetControlID="txtReqExtGrantFromDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender13" runat="server" TargetControlID="txtReqExtGrantFromDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"  ErrorTooltipEnabled ="true"  ClearTextOnInvalid="true"   />
                                    <asp:RangeValidator ID="RangeValidator13" runat="server" ControlToValidate="txtReqExtGrantFromDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                                    <ajaxToolkit:ValidatorCalloutExtender  runat="Server"  ID="ValidatorCalloutExtender13" TargetControlID="RangeValidator13" />
                                </div>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtReqExtGrantToDate" runat="server" CssClass="form-control" Placeholder="To" />             
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender14" runat="server" TargetControlID="txtReqExtGrantToDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender14" runat="server" TargetControlID="txtReqExtGrantToDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"  ErrorTooltipEnabled ="true"  ClearTextOnInvalid="true"   />
                                    <asp:RangeValidator ID="RangeValidator14" runat="server" ControlToValidate="txtReqExtGrantToDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                                    <ajaxToolkit:ValidatorCalloutExtender  runat="Server"  ID="ValidatorCalloutExtender14" TargetControlID="RangeValidator14" />
                                </div>
                            </div> 
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Date of Receipt of Answer :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtAnswerRecvDate" runat="server" CssClass="form-control" />             
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender16" runat="server" TargetControlID="txtAnswerRecvDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender16" runat="server" TargetControlID="txtAnswerRecvDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"  ErrorTooltipEnabled ="true"  ClearTextOnInvalid="true"   />
                                    <asp:RangeValidator ID="RangeValidator16" runat="server" ControlToValidate="txtAnswerRecvDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                                    <ajaxToolkit:ValidatorCalloutExtender  runat="Server"  ID="ValidatorCalloutExtender16" TargetControlID="RangeValidator16" />
                                </div>
                            </div> 
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Date of Evaluation Report :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtEvalDate" runat="server" CssClass="form-control" />             
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender17" runat="server" TargetControlID="txtEvalDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender17" runat="server" TargetControlID="txtEvalDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"  ErrorTooltipEnabled ="true"  ClearTextOnInvalid="true"   />
                                    <asp:RangeValidator ID="RangeValidator17" runat="server" ControlToValidate="txtEvalDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                                    <ajaxToolkit:ValidatorCalloutExtender  runat="Server"  ID="ValidatorCalloutExtender17" TargetControlID="RangeValidator17" />
                                </div>
                            </div> 
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Date of Notice of Decision :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDecisionDate" runat="server" CssClass="form-control" />             
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender18" runat="server" TargetControlID="txtDecisionDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender18" runat="server" TargetControlID="txtDecisionDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"  ErrorTooltipEnabled ="true"  ClearTextOnInvalid="true"   />
                                    <asp:RangeValidator ID="RangeValidator18" runat="server" ControlToValidate="txtDecisionDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                                    <ajaxToolkit:ValidatorCalloutExtender  runat="Server"  ID="ValidatorCalloutExtender18" TargetControlID="RangeValidator18" />
                                </div>
                            </div> 
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Date of Release of Notice of Decision :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDecisionReleaseDate" runat="server" CssClass="form-control" />             
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender19" runat="server" TargetControlID="txtDecisionReleaseDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender19" runat="server" TargetControlID="txtDecisionReleaseDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"  ErrorTooltipEnabled ="true"  ClearTextOnInvalid="true"   />
                                    <asp:RangeValidator ID="RangeValidator19" runat="server" ControlToValidate="txtDecisionReleaseDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                                    <ajaxToolkit:ValidatorCalloutExtender  runat="Server"  ID="ValidatorCalloutExtender19" TargetControlID="RangeValidator19" />
                                </div>
                            </div> 
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Date of Receipt of Notice of Decision :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDecisionRecvDate" runat="server" CssClass="form-control" />             
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender20" runat="server" TargetControlID="txtDecisionRecvDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender20" runat="server" TargetControlID="txtDecisionRecvDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"  ErrorTooltipEnabled ="true"  ClearTextOnInvalid="true"   />
                                    <asp:RangeValidator ID="RangeValidator20" runat="server" ControlToValidate="txtDecisionRecvDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                                    <ajaxToolkit:ValidatorCalloutExtender  runat="Server"  ID="ValidatorCalloutExtender20" TargetControlID="RangeValidator20" />
                                </div>
                            </div> 
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Date of Resolution of Motion for Reconsideration :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtReconsiderDate" runat="server" CssClass="form-control" />             
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender21" runat="server" TargetControlID="txtReconsiderDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender21" runat="server" TargetControlID="txtReconsiderDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"  ErrorTooltipEnabled ="true"  ClearTextOnInvalid="true"   />
                                    <asp:RangeValidator ID="RangeValidator21" runat="server" ControlToValidate="txtReconsiderDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                                                                        
                                    <ajaxToolkit:ValidatorCalloutExtender  runat="Server"  ID="ValidatorCalloutExtender21" TargetControlID="RangeValidator21" />
                                </div>
                            </div> 
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Notice Released On :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtNReleasedDate" runat="server" CssClass="form-control"  ></asp:TextBox> 
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtNReleasedDate" Format="MM/dd/yyyy" />  
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="txtNReleasedDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true"  />
                                    <asp:RangeValidator ID="RangeValidator7" runat="server" ControlToValidate="txtNReleasedDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender5" TargetControlID="RangeValidator7" /> 
                                </div>
                            </div> 
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Notice Received by Respondent On :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtNReceivedDate" runat="server" CssClass="form-control"  ></asp:TextBox> 
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" TargetControlID="txtNReceivedDate" Format="MM/dd/yyyy" />  
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender8" runat="server" TargetControlID="txtNReceivedDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true"  />
                                    <asp:RangeValidator ID="RangeValidator8" runat="server" ControlToValidate="txtNReceivedDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender6" TargetControlID="RangeValidator8" /> 
                                </div>
                            </div> 

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space"></label>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="chkIsApproved" runat="server" Text="&nbsp;Tick if already approved" />
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Approved By :</label>
                                <div class="col-md-6">
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
                                <label class="col-md-3 control-label has-space">Date Approved :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtApprovedDate" runat="server" CssClass="form-control"  ></asp:TextBox> 
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtApprovedDate" Format="MM/dd/yyyy" />  
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtApprovedDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true"  />
                                    <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtApprovedDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3" TargetControlID="RangeValidator5" /> 
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space"></label>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="chkIsServed" runat="server" Text="&nbsp;Tick if already served" />
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Date Issued :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtServedDate" runat="server" CssClass="form-control"  ></asp:TextBox> 
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtServedDate" Format="MM/dd/yyyy" />  
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtServedDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true"  />
                                    <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="txtServedDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender4" TargetControlID="RangeValidator6" /> 
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Date Received :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtReceivedDate2" runat="server" CssClass="form-control"  ></asp:TextBox> 
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender15" runat="server" TargetControlID="txtReceivedDate2" Format="MM/dd/yyyy" />  
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender15" runat="server" TargetControlID="txtReceivedDate2" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled ="true" ClearTextOnInvalid="true"  />
                                    <asp:RangeValidator ID="RangeValidator15" runat="server" ControlToValidate="txtReceivedDate2" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender15" TargetControlID="RangeValidator15" /> 
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Remarks :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtRemarks2" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" />                                     
                                </div>
                            </div> 
                            
                            <div class="form-group" style="display:none">
                                <label class="col-md-3 control-label has-space">Project :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboProjectNo" runat="server" CssClass="form-control" DataMember="EProject" />
                                </div>
                            </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <div>
                                    <asp:Button runat="server"  ID="lnkSubmit" CssClass="btn btn-default submit fsMain" Text="Submit" OnClick= "btnSave_Click" ></asp:Button>
                                    <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify"></asp:Button>
                                </div>
                            </div>
                        </div> 
                      <br />
                    </div>
                    </fieldset>
            </asp:Panel> 
        </Content>
    </uc:Tab>


<asp:Button ID="btnShow" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlShow" runat="server" TargetControlID="btnShow" PopupControlID="pnlPopup" CancelControlID="imgClose" BackgroundCssClass="modalBackground" >
</ajaxtoolkit:ModalPopupExtender>

<asp:Panel id="pnlPopup" runat="server" CssClass="entryPopup" style=" display:none;">
       <fieldset class="form" id="Fieldset1">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>Employee Profile</h4>
                <asp:Linkbutton runat="server" ID="imgClose" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;     
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label">Position :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboPositionNo" DataMember="EPosition" runat="server" CssClass="form-control" 
                        ></asp:Dropdownlist>
                </div>
            </div> 
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Rank :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboRankNo" DataMember="ERank" runat="server" CssClass="form-control" 
                        ></asp:Dropdownlist>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Age :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtBirthAge" ReadOnly="true" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Gender :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboGenderNo" DataMember="EGender" runat="server" CssClass="form-control" 
                        ></asp:Dropdownlist>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Facility :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboFacilityNo" DataMember="EFacility" runat="server" CssClass="form-control" 
                        ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Division :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboDivisionNo" DataMember="EDivision" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                </div>
            </div>  
            <div class="form-group">
                <label class="col-md-4 control-label">Department :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboDepartmentNo" DataMember="EDepartment" runat="server" CssClass="form-control" ></asp:Dropdownlist>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Section :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboSectionNo" DataMember="ESection" runat="server" CssClass="form-control" 
                        ></asp:Dropdownlist>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Unit :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboUnitNo" DataMember="EUnit" runat="server" CssClass="form-control" 
                        ></asp:Dropdownlist>
                </div>
            </div>
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Group :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboGroupNo" DataMember="EGroup" runat="server" CssClass="form-control" 
                        ></asp:Dropdownlist>
                </div>
            </div> 
            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label">Line Leader :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboLineLeaderNo" DataMember="EEmployee" runat="server" CssClass="form-control" 
                        ></asp:Dropdownlist>
                </div>
            </div> 
            <div class="form-group">
                <label class="col-md-4 control-label">Immediate superior :</label>
                <div class="col-md-6">
                    <asp:Dropdownlist ID="cboImmediateSuperiorNo" DataMember="EEmployee" runat="server" CssClass="form-control" 
                        ></asp:Dropdownlist>
                </div>
            </div> 
        </div>
        <br />
        </fieldset>
</asp:Panel>



</asp:content> 


