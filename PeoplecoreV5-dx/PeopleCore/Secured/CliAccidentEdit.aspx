<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="CliAccidentEdit.aspx.vb" Inherits="Secured_PayPreviousEdit" %>


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
                                <asp:Textbox ID="txtClinicAcciNo" ReadOnly="true" runat="server" CssClass="form-control" />                                    
                            </div>
                        </div>                                                                   
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Transaction No. :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Status :</label>
                            <div class="col-md-5">
                                <asp:Dropdownlist ID="cboClinicAcciStatNo" DataMember="EClinicAcciStat" runat="server" CssClass="form-control required" />
                            </div>
                        </div>
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Date Reported :</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtDateReported" runat="server" CssClass="form-control required"></asp:TextBox> 
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateReported" Format="MM/dd/yyyy" />  
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDateReported" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtDateReported" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"   />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RangeValidator2" />                                                                           
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Reported By :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtReportByName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here.." /> 
                                <asp:HiddenField runat="server" ID="hifReportByNo" />
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                TargetControlID="txtReportByName" MinimumPrefixLength="2" EnableCaching="true"                    
                                CompletionSetCount="1" CompletionInterval="500" ServiceMethod="PopulateEmployee" ServicePath="~/asmx/WebService.asmx"
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="xGetRecord" FirstRowSelected="true" UseContextKey="true" />
                                    <script type="text/javascript">
                                        function Split(obj, index) {
                                            var items = obj.split("|");
                                            for (i = 0; i < items.length; i++) {
                                                if (i == index) {
                                                    return items[i];
                                                }
                                            }
                                        }
                                        function xGetRecord(source, eventArgs) {
                                            document.getElementById('<%= hifReportByNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
                                        }
                                    </script>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Accident Category :</label>
                            <div class="col-md-5">
                                <asp:Dropdownlist ID="cboClinicAcciCategoryNo" DataMember="EClinicAcciCategory" runat="server" CssClass="form-control required"  AutoPostBack="true" OnSelectedIndexChanged="ClinicAcciCategory_ValueChanged" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Accident Type :</label>
                            <div class="col-md-5">
                                <asp:Dropdownlist ID="cboClinicAcciTypeNo" runat="server" CssClass="form-control required" />
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Employee Name :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here.." /> 
                                <asp:HiddenField runat="server" ID="hifEmployeeNo" />
                                <ajaxToolkit:AutoCompleteExtender ID="aceEmployee" runat="server"
                                TargetControlID="txtFullName" MinimumPrefixLength="2" EnableCaching="true"                    
                                CompletionSetCount="1" CompletionInterval="500" ServiceMethod="PopulateEmployee" ServicePath="~/asmx/WebService.asmx"
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="GetRecord" FirstRowSelected="true" UseContextKey="true" />
                                    <script type="text/javascript">
                                        function SplitH(obj, index) {
                                            var items = obj.split("|");
                                            for (i = 0; i < items.length; i++) {
                                                if (i == index) {
                                                    return items[i];
                                                }
                                            }
                                        }
                                        function GetRecord(source, eventArgs) {
                                            document.getElementById('<%= hifEmployeeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                        }
                                    </script>
                            </div>
                        </div>

                        <br />
                        <div class="form-group">  
                            <h5 class="col-md-8">
                                <label class="control-label">ACCIDENT/INCIDENT INFORMATION</label>
                            </h5>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">How did the accident/incident occured :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtAccidentRemarks" TextMode="MultiLine" Rows="3" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Date Occured :</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtDateOccured" runat="server" CssClass="form-control required"></asp:TextBox> 
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateOccured" Format="MM/dd/yyyy" />  
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDateOccured" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtDateOccured" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />
                                <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator1" />                                                                           
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Time Occured :</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtTimeOccured" runat="server" CssClass="form-control" ></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4x" runat="server" TargetControlID="txtTimeOccured" Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender4x" ControlToValidate="txtTimeOccured" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage=""  />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Place Occured :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtPlaceOccured" TextMode="MultiLine" Rows="3" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Caused of Death :</label>
                            <div class="col-md-5">
                                <%--<asp:TextBox runat="server" ID="TextBox1" TextMode="MultiLine" Rows="3" CssClass="form-control required" />--%>
                                <asp:DropDownList runat="server" ID="cboClinicCauseOfDeathNo" CssClass="form-control" DataMember="EClinicCauseOfDeath" />
                            </div>
                        </div>

                        <br />
                        <div class="form-group">  
                            <h5 class="col-md-8">
                                <label class="control-label">OTHER DETAILS</label>
                            </h5>
                        </div>
                        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Investigating Result :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtInvestigationResult" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Assessment :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtAssessment" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Recommendation :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtRecommendation" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Resolution :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtResolution" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Procedure/Treatment Done :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtTreatment" TextMode="MultiLine" Rows="3" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Investigated By :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtInvistigatedBy" TextMode="MultiLine" Rows="3" CssClass="form-control" />
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