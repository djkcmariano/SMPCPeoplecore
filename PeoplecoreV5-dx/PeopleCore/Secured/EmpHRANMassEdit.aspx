<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpHRANMassEdit.aspx.vb" Inherits="Secured_EmpHRANEdit" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

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
        document.getElementById('<%= hifCSuperiorNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
        document.getElementById('<%= hifASuperiorNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
    }
</script>

    <uc:Tab runat="server" ID="Tab">
        <Header>
            <asp:Label runat="server" ID="lbl" />
            <div style="display:none;">
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>
        </Header>
        <Content>
            <asp:Panel runat="server" ID="Panel1">
                <fieldset class="form" id="fsMain">
                <br />
                <div class="row">
                     <div class="col-md-6">
                <!-- START DIVISION PERMISSION -->
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">Criteria</h4>  
                        </div>
                        <div class="panel-body">
                            <div  class="form-horizontal">

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Position :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCPositionNo" DataMember="EPosition" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Functional Title :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCTaskNo" DataMember="ETask" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Facility :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCFacilityNo" DataMember="EFacility" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Division :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCDivisionNo" DataMember="EDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Department :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCDepartmentNo" DataMember="EDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                               
                                
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Section :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCSectionNo" DataMember="ESection" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Unit :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCUnitNo" DataMember="EUnit" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                

                                 <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Group :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCGroupNo" DataMember="EGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                
                               
                                
                                

                                
                               

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Cost Center :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCCostCenterNo" DataMember="ECostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Location / Area :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCLocationNo" DataMember="ELocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Project :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCProjectNo" DataMember="EProject" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Job Level :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCSalaryGradeNo" DataMember="ESalaryGrade" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Employee Class :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCEmployeeClassNo" DataMember="EEmployeeClass" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Employee Status :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCEmployeeStatNo" DataMember="EEmployeeStat" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Rank :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCRankNo" DataMember="ERank" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Payroll Group :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCPayclassNo" DataMember="EPayClass" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Superior :</label>
                                    <div class="col-md-8">
                                        <%--<asp:DropDownList ID="cboCImmediateSuperiorNo" DataMember="ERank" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                        <asp:TextBox runat="server" ID="txtCSuperiorName" CssClass="form-control" Placeholder="Type here..." /> 
                                        <asp:HiddenField runat="server" ID="hifCSuperiorNo"/>                    
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"  
                                            TargetControlID="txtCSuperiorName" MinimumPrefixLength="2" CompletionSetCount="3" 
                                            CompletionInterval="250" ServiceMethod="PopulateManager" 
                                            CompletionListCssClass="autocomplete_completionListElement" 
                                            CompletionListItemCssClass="autocomplete_listItem" 
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            OnClientItemSelected="getCImmediate" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />

                                            <script type="text/javascript">
                                                function getCImmediate(source, eventArgs) {
                                                    document.getElementById('<%= hifCSuperiorNo.ClientID %>').value = eventArgs.get_value();
                                                }
                                            </script>  
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Rate Class :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCEmployeeRateClassNo" DataMember="EEmployeeRateClass" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space"></label>
                                    <div class="col-md-8">
                                        <asp:CheckBox runat="server" ID="txtIsMWE" Text="&nbsp; Select minimum wage earner (MWE)" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div> 
                </div>

                     <div class="col-md-6">
                <!-- START DIVISION PERMISSION -->
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">Action Template</h4>  
                        </div>
                        <div class="panel-body">
                             <div  class="form-horizontal">

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Position :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboAPositionNo" DataMember="EPosition" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Functional Title :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboATaskNo" DataMember="ETask" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Facility :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboAFacilityNo" DataMember="EFacility" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Unit :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboAUnitNo" DataMember="EUnit" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                  <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Department :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboADepartmentNo" DataMember="EDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Group :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboAGroupNo" DataMember="EGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Division :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboADivisionNo" DataMember="EDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                              

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Section :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboASectionNo" DataMember="ESection" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                

                             
                                
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Cost Center :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboACostCenterNo" DataMember="ECostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Location / Area :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboALocationNo" DataMember="ELocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Project :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboAProjectNo" DataMember="EProject" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Job Level :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboASalaryGradeNo" DataMember="ESalaryGrade" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Employee Class :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboAEmployeeClassNo" DataMember="EEmployeeClass" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Employee Status :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboAEmployeeStatNo" DataMember="EEmployeeStat" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Rank :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboARankNo" DataMember="ERank" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Payroll Group :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboAPayClassNo" DataMember="EPayClass" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Superior :</label>
                                    <div class="col-md-8">
                                        <%--<asp:DropDownList ID="cboAImmediateSuperiorNo" DataMember="ERank" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                        <asp:TextBox runat="server" ID="txtASuperiorName" CssClass="form-control" Placeholder="Type here..."/> 
                                        <asp:HiddenField runat="server" ID="hifASuperiorNo"/>                    
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                                            TargetControlID="txtASuperiorName" MinimumPrefixLength="2" CompletionSetCount="1" 
                                            CompletionInterval="250" ServiceMethod="PopulateManager" 
                                            CompletionListCssClass="autocomplete_completionListElement" 
                                            CompletionListItemCssClass="autocomplete_listItem" 
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            OnClientItemSelected="getAImmediate" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />

                                            <script type="text/javascript">
                                                function getAImmediate(source, eventArgs) {
                                                    document.getElementById('<%= hifASuperiorNo.ClientID %>').value = eventArgs.get_value();
                                                }
                                            </script> 
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space">Rate Class :</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboAEmployeeRateClassNo" DataMember="EEmployeeRateClass" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label class="col-md-4 control-label has-space"></label>
                                    <div class="col-md-8">
                                        &nbsp;
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div> 
                </div>
                </div>
            
                <div class="row"> 
                    <div class="col-md-12">  
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div  class="form-horizontal">
                                    <div class="form-group" style="display:none;">
                                        <label class="col-md-4 control-label has-space">HRAN Mass No. :</label>
                                        <div class="col-md-6">
                                            <asp:Textbox ID="txtHRANMassNo" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" ></asp:Textbox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space">HRAN Mass No. :</label>
                                        <div class="col-md-6">
                                            <asp:Textbox ID="txtHRANMassCode" runat="server" CssClass="form-control" Enabled="false" ReadOnly="true" Placeholder="Autonumber" ></asp:Textbox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-required">HRAN Type :</label>
                                        <div class="col-md-6">
                                            <asp:DropDownList ID="cboHRANTypeNo" runat="server" CssClass="form-control required"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space">Prepared Date :</label>
                                        <div class="col-md-2">
                                            <asp:Textbox ID="txtPreparationDate" runat="server" CssClass="form-control required" ReadOnly="true" Enabled="false"></asp:Textbox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPreparationDate" Format="MM/dd/yyyy" />                   
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtPreparationDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPreparationDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="RangeValidator1" /> 
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-required">Effective Date :</label>
                                        <div class="col-md-2">
                                            <asp:Textbox ID="txtEffectivity" runat="server" CssClass="form-control required"></asp:Textbox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEffectivity" Format="MM/dd/yyyy" />                   
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtEffectivity" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtEffectivity" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="RangeValidator2" /> 
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space">Due Date :</label>
                                        <div class="col-md-2">
                                            <asp:Textbox ID="txtDueDate" runat="server" CssClass="form-control"></asp:Textbox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtDueDate" Format="MM/dd/yyyy" />                   
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtDueDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                                            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtDueDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                                            <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender4" TargetControlID="RangeValidator4" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-required">Reason :</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtReason" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control required"></asp:TextBox>
                                        </div>
                                    </div>
                        
                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space">Remarks :</label>
                                        <div class="col-md-6">
                                            <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group" style="visibility:hidden;position:absolute;">
                                        <label class="col-md-4 control-label has-space"></label>
                                        <div class="col-md-6">
                                            <asp:CheckBox ID="txtIsTablebase" runat="server" Text="&nbsp;Tick here if rate increase is based on table."></asp:CheckBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space"></label>
                                        <div class="col-md-6">
                                            <asp:CheckBox ID="txtIsInPercent" runat="server" Text="&nbsp;Tick if rate increase is based on percentage"></asp:CheckBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space">Rate Increase :</label>
                                        <div class="col-md-2">
                                            <asp:TextBox ID="txtRateIncrease" runat="server" CssClass="number form-control"></asp:TextBox>
                                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"  FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtRateIncrease" ></ajaxToolkit:FilteredTextBoxExtender>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space"></label>
                                        <div class="col-md-6">
                                            <asp:CheckBox ID="txtIsReady" runat="server" Text="&nbsp;Tick if transaction is ready for posting"></asp:CheckBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-4 control-label has-space"></label>
                                        <div class="col-md-6">
                                            <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-primary submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                                            <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />
                                        </div>
                                    </div>
                               </div>
                            </div>
                        </div>
                    </div>
                </div>

                </fieldset>
           </asp:Panel>
        </Content>
    </uc:Tab>
</asp:Content>

