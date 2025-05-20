<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="TrnTakenEdit.aspx.vb" Inherits="Secured_TrnTakenEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>
            <asp:Label runat="server" ID="lbl" /> 
            <div style="display:none;">
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>    
        </Header>
        <Content>
        <asp:Panel runat="server" ID="Panel1">        
            <br /><br />            
            <fieldset class="form" id="fsMain">
                <div  class="form-horizontal">
                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">TRAINING INFORMATION&nbsp;&nbsp;</label>
                        </h5>
                    </div>
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">Training No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtTrnTitleNo" CssClass="form-control" ReadOnly="true" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Training No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtCode" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Status :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboTrnStatNo" runat="server"  CssClass="form-control required" />
                        </div>
                        
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Course Title :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtTrnTitleDesc" CssClass="form-control required" onblur="ResetTraining()" style="display:inline-block;" Placeholder="Type here..." /> 
                            <asp:HiddenField runat="server" ID="hifTrnTitleNo" OnValueChanged="hifTrnTitleNo_ValueChanged"/>
                            <ajaxToolkit:AutoCompleteExtender ID="aceTrnTitle" runat="server"
                            TargetControlID="txtTrnTitleDesc" MinimumPrefixLength="2" EnableCaching="true"                    
                            CompletionSetCount="1" CompletionInterval="500" ServiceMethod="PopulateTrnTitle" ServicePath="~/asmx/WebService.asmx"
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListItemCssClass="autocomplete_listItem" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            OnClientItemSelected="GetRecord" FirstRowSelected="true" UseContextKey="true" />
                                <script type="text/javascript">
                                    function GetRecord(source, eventArgs) {
                                        var trnTitleId = "<%= hifTrnTitleNo.ClientID %>";
                                        document.getElementById('<%= hifTrnTitleNo.ClientID %>').value = eventArgs.get_value();
                                        __doPostBack(trnTitleId, "");
                                    }



                                    function ResetTraining() {
                                        if (document.getElementById('<%= txtTrnTitleDesc.ClientID %>').value == "") {
                                            document.getElementById('<%= hifTrnTitleNo.ClientID %>').value = "";
                                            document.getElementById('<%= cboTrnCategoryNo.ClientID %>').value = "";
                                            document.getElementById('<%= cboTrnTypeNo.ClientID %>').value = "";
                                            document.getElementById('<%= txtDescription.ClientID %>').value = "";
                                            document.getElementById('<%= txtObjectives.ClientID %>').value = "";
                                            document.getElementById('<%= txtHrs.ClientID %>').value = "";
                                            document.getElementById('<%= txtCost.ClientID %>').value = "";
                                            document.getElementById('<%= txtNoOfMonths.ClientID %>').value = "";
                                            document.getElementById('<%= txtServiceContract.ClientID %>').value = "";
                                            document.getElementById('<%= cboTrnRetakenNo.ClientID %>').value = "";
                                            document.getElementById('<%= txtRemarks.ClientID %>').value = "";
                                        }
                                    }

                                </script>



                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Course Category :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboTrnCategoryNo" DataMember="ETrnCategory" runat="server"  CssClass="form-control" />
                        </div>
                        
                    </div>


                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Course Type :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboTrnTypeNo" DataMember="ETrnType" runat="server"  CssClass="form-control" />
                        </div>
                        
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Course Description :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Objectives :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtObjectives" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Hour(s) :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtHrs" CssClass="number form-control required" />
                        </div>
                    </div>

                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">OTHER DETAILS&nbsp;&nbsp;</label>
                        </h5>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Professional Fee(s) :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtCost" CssClass="number form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Recertification (in month) :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtNoOfMonths" CssClass="number form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Service Contract (in month) :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtServiceContract" CssClass="number form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Re-taken Schedule :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboTrnRetakenNo" DataMember="ETrnRetaken" runat="server"  CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Guidelines :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">EVENT INFORMATION&nbsp;&nbsp;</label>
                        </h5>
                    </div>

                    
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Start Date :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="required form-control"></asp:TextBox> 
                                                                    
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                TargetControlID="txtStartDate"
                                Format="MM/dd/yyyy" />  
                                      
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                TargetControlID="txtStartDate"
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
                                ControlToValidate="txtStartDate"
                                ErrorMessage="<b>Please enter valid entry</b>"
                                MinimumValue="1900-01-01"
                                MaximumValue="3000-12-31"
                                Type="Date" Display="None"  />
                                    
                                <ajaxToolkit:ValidatorCalloutExtender 
                                runat="Server" 
                                ID="ValidatorCalloutExtender2"
                                TargetControlID="RangeValidator2" />                                                                           
                        </div>

                        <label class="col-md-2 control-label has-required">End Date :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="required form-control"></asp:TextBox> 
                                                                    
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server"
                                TargetControlID="txtEndDate"
                                Format="MM/dd/yyyy" />  
                                      
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                TargetControlID="txtEndDate"
                                Mask="99/99/9999"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date"
                                DisplayMoney="Left"
                                AcceptNegative="Left" />
                                    
                                <asp:RangeValidator
                                ID="RangeValidator3"
                                runat="server"
                                ControlToValidate="txtEndDate"
                                ErrorMessage="<b>Please enter valid entry</b>"
                                MinimumValue="1900-01-01"
                                MaximumValue="3000-12-31"
                                Type="Date" Display="None"  />
                                    
                                <ajaxToolkit:ValidatorCalloutExtender 
                                runat="Server" 
                                ID="ValidatorCalloutExtender3"
                                TargetControlID="RangeValidator3" />                                                                           
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Time Start :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtTimeIn" runat="server" CssClass="form-control" ></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4x" runat="server"
                                TargetControlID="txtTimeIn" 
                                Mask="99:99"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Time"
                                AcceptAMPM="false" 
                            
                                CultureName="en-US" />
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                ControlExtender="MaskedEditExtender4x"
                                ControlToValidate="txtTimeIn"
                                IsValidEmpty="true"
                                EmptyValueMessage=""
                                InvalidValueMessage=""
                                ValidationGroup="Demo1"
                                Display="Dynamic"
                                TooltipMessage="" />
                        </div>

                        <label class="col-md-2 control-label has-space">Time End :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtTimeOut" runat="server" CssClass="form-control" ></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                TargetControlID="txtTimeOut" 
                                Mask="99:99"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Time"
                                AcceptAMPM="false" 
                            
                                CultureName="en-US" />
                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                ControlExtender="MaskedEditExtender4x"
                                ControlToValidate="txtTimeOut"
                                IsValidEmpty="true"
                                EmptyValueMessage=""
                                InvalidValueMessage=""
                                ValidationGroup="Demo1"
                                Display="Dynamic"
                                TooltipMessage="" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Facilitator :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboTrnProviderNo" DataMember="ETrnProvider" runat="server"  CssClass="form-control required" />
                        </div>
                        <div class="col-md-2" style="padding-top:3px">
                            <asp:LinkButton runat="server" ID="lnkProvider" OnClick="lnkProvider_Click" CausesValidation="false" ToolTip="Click here to add new facilitator">
                                <i class="fa fa-plus-square-o  fa-2x"></i>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Venue :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboTrnVenueNo" DataMember="ETrnVenue" runat="server"  CssClass="form-control required" />
                        </div>
                        <div class="col-md-2" style="padding-top:3px">
                            <asp:LinkButton runat="server" ID="lnkVenue" OnClick="lnkVenue_Click" CausesValidation="false" ToolTip="Click here to add new venue">
                                <i class="fa fa-plus-square-o  fa-2x"></i>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">ENROLLMENT INFORMATION&nbsp;&nbsp;</label>
                        </h5>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Enrollment Type :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboTrnEnrollTypeNo" DataMember="ETrnEnrollType" runat="server"  CssClass="form-control required" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Open Date :</label>
                        <div class="col-md-2">
                            <asp:TextBox ID="txtEnrollDateOpen" runat="server" CssClass="form-control"></asp:TextBox> 
                                                                    
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                TargetControlID="txtEnrollDateOpen"
                                Format="MM/dd/yyyy" />  
                                      
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                TargetControlID="txtEnrollDateOpen"
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
                                ControlToValidate="txtEnrollDateOpen"
                                ErrorMessage="<b>Please enter valid entry</b>"
                                MinimumValue="1900-01-01"
                                MaximumValue="3000-12-31"
                                Type="Date" Display="None"  />
                                    
                                <ajaxToolkit:ValidatorCalloutExtender 
                                runat="Server" 
                                ID="ValidatorCalloutExtender1"
                                TargetControlID="RangeValidator1" />                                                                           
                        </div>

                        <label class="col-md-2 control-label has-space">Close Date :</label>
                        <div class="col-md-2">

                             <asp:TextBox ID="txtEnrollDateClosed" runat="server" CssClass="form-control"></asp:TextBox> 
                                                                    
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server"
                                TargetControlID="txtEnrollDateClosed"
                                Format="MM/dd/yyyy" />  
                                      
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server"
                                TargetControlID="txtEnrollDateClosed"
                                Mask="99/99/9999"
                                MessageValidatorTip="true"
                                OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError"
                                MaskType="Date"
                                DisplayMoney="Left"
                                AcceptNegative="Left" />
                                    
                                <asp:RangeValidator
                                ID="RangeValidator4"
                                runat="server"
                                ControlToValidate="txtEnrollDateClosed"
                                ErrorMessage="<b>Please enter valid entry</b>"
                                MinimumValue="1900-01-01"
                                MaximumValue="3000-12-31"
                                Type="Date" Display="None"  />
                                    
                                <ajaxToolkit:ValidatorCalloutExtender 
                                runat="Server" 
                                ID="ValidatorCalloutExtender4"
                                TargetControlID="RangeValidator4" />

                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Minimum Seats :</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtMinimumSeats" CssClass="number form-control" />
                        </div>
                        <label class="col-md-2 control-label has-space">Maximum Seats :</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtMaximumSeats" CssClass="number form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Users Enrolled :</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtActualHC" CssClass="number form-control" Enabled="false" ReadOnly="true" />
                        </div>
                        <label class="col-md-2 control-label has-space">Remaining Seats :</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtRemainingSeats" CssClass="number form-control" Enabled="false" ReadOnly="true" />
                        </div>
                    </div>

                   <div class="form-group">
                        <label class="col-md-3 control-label"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="lnkSave_Click"></asp:Button>          
                            <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click"></asp:Button>                        
                        </div>
                    </div> 

                </div>                                
                <br />
            </fieldset >
        </asp:Panel>
        </Content>
    </uc:Tab>    



<asp:Button ID="btnProvider" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlProvider" runat="server" TargetControlID="btnProvider" PopupControlID="pnlProvider" CancelControlID="lnkCloseProvider" BackgroundCssClass="modalBackground" />
<asp:Panel id="pnlProvider" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsProvider">                    
        <div class="cf popupheader">
            <h4>Add New Facilitator</h4>
            <asp:Linkbutton runat="server" ID="lnkCloseProvider" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveProvider" OnClick="lnkSaveProvider_Click" CssClass="fa fa-floppy-o submit lnkSaveProvider" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">    

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtProvider_TrnProviderDesc" runat="server" CssClass="form-control required" TextMode="MultiLine" Rows="2" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Contact Name :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtProvider_ContactName" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Contact Title :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtProvider_ContactTitle" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Address :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtProvider_Address" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Phone No. :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtProvider_PhoneNo" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Email Address :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtProvider_Email" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div>
            
            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkProvider_IsApplyToAll" Text="&nbsp;Apply to all company." />
                </div>
            </div>

            <br />
        </div>                    
    </fieldset>
</asp:Panel>              



<asp:Button ID="btnVenue" runat="server" style="display:none" />
<ajaxtoolkit:ModalPopupExtender ID="mdlVenue" runat="server" TargetControlID="btnVenue" PopupControlID="pnlVenue" CancelControlID="lnkCloseVenue" BackgroundCssClass="modalBackground" />
<asp:Panel id="pnlVenue" runat="server" CssClass="entryPopup" style="display:none;">
    <fieldset class="form" id="fsVenue">                    
        <div class="cf popupheader">
            <h4>Add New Venue</h4>
            <asp:Linkbutton runat="server" ID="lnkCloseVenue" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSaveVenue" OnClick="lnkSaveVenue_Click" CssClass="fa fa-floppy-o submit lnkSaveVenue" ToolTip="Save" />
        </div>                                            
        <div class="entryPopupDetl form-horizontal">    

            <div class="form-group" style="display:none;">
                <label class="col-md-4 control-label has-space">Code :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtVenue_TrnVenueCode" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Description :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtVenue_TrnVenueDesc" runat="server" CssClass="form-control required" TextMode="MultiLine" Rows="2" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-required">Venue Type :</label>
                <div class="col-md-7"> 
                    <asp:Dropdownlist ID="cboVenue_TrnVenueTypeNo" DataMember="ETrnVenueType" runat="server" CssClass="form-control required" />
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Address :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtVenue_Address" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Mobile No. :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtVenue_PhoneNo" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Phone/Fax No. :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtVenue_FaxNo" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Contact Person :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtVenue_ContactPerson" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">Position :</label>
                <div class="col-md-7"> 
                    <asp:Textbox ID="txtVenue_Position" runat="server" CssClass="form-control" ></asp:Textbox>
                </div>
            </div>

            <div class="form-group">
                <label class="col-md-4 control-label has-space">&nbsp;</label>
                <div class="col-md-7">
                    <asp:CheckBox runat="server" ID="chkVenue_IsApplyToAll" Text="&nbsp;Apply to all company." />
                </div>
            </div>

            <br />
        </div>                    
    </fieldset>
</asp:Panel>       

</asp:Content>




