<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppMREdit.aspx.vb" Inherits="Secured_AppMREdit" Theme="PCoreStyle" %>

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

        function getMain(source, eventArgs) {
//            document.getElementById('<%= hifRequestedBy.ClientID %>').value = eventArgs.get_value();
//            document.getElementById('<%= hifRequestedBy.ClientID %>').value = Split(eventArgs.get_value(), 0);
            var RequestedBy = "<%= hifRequestedBy.ClientID %>";
            document.getElementById('<%= hifRequestedBy.ClientID %>').value = Split(eventArgs.get_value(), 0);
            __doPostBack(RequestedBy, "");
        }

        function disableenable(chk) {

            if (chk.checked) {
                document.getElementById("ctl00_cphBody_Tab_lstPlantilla").disabled = true;
                
            } else {
                document.getElementById("ctl00_cphBody_Tab_lstPlantilla").disabled = false;
               
            };
        };

    </script>

    <uc:Tab runat="server" ID="Tab">
        <Header>
            <asp:Label runat="server" ID="lbl" />
        </Header>
        <Content>
            <asp:Panel runat="server" ID="Panel1">
                <br />
                <br />
                <fieldset class="form" id="fsMain">
                    <div class="row">
                        <ul class="panel-controls">  
                            <li><asp:LinkButton runat="server" ID="lnkSave3" CssClass="control-primary submit lnkSave3" OnClick="lnkSave_Click"><i class="fa fa-floppy-o"></i>&nbsp;Save</asp:LinkButton></li>
                            <li><asp:LinkButton runat="server" ID="lnkModify3" CssClass="control-primary" OnClick="lnkModify_Click"><i class="fa fa-pencil"></i>&nbsp;Modify</asp:LinkButton></li>
                        </ul>
                    </div>
                    <div  class="form-horizontal">

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">MR No. :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtMRCode" CssClass="form-control" Enabled="false" Placeholder="Autonumber" />
                                <asp:HiddenField runat="server" ID="hifMRNo" />
                            </div>
                        </div>

                        

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">Requested By :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtRFullname" CssClass="form-control required" style="display:inline-block;"/>
                                <asp:HiddenField runat="server" ID="hifRequestedBy"  />
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"  
                                TargetControlID="txtRFullname" MinimumPrefixLength="2" CompletionSetCount="1"
                                CompletionInterval="500" ServiceMethod="PopulateManagerAll"
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="getMain" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                <script type="text/javascript">
                                    function getMain(source, eventArgs) {
                                        document.getElementById('<%= hifRequestedBy.ClientID %>').value = eventArgs.get_value();
                                    }
                                </script>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">MR Date :</label>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtRequestedDate" CssClass="form-control required" />
                                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtRequestedDate" Format="MM/dd/yyyy" />
                                <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtRequestedDate" Mask="99/99/9999" MaskType="Date" />
                                <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtRequestedDate" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">Date Needed :</label>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtNeededDate" CssClass="form-control required" />
                                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtNeededDate" Format="MM/dd/yyyy" />
                                <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtNeededDate" Mask="99/99/9999" MaskType="Date" />
                                <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtNeededDate" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">MR Status :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboMRStatNo" DataMember="EMRStat" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">MR Type :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboMRTypeNo" DataMember="EMRType" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">MR Filling Mode :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboMRReasonNo" DataMember="EMRReason" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Justification :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" Rows="2" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:CheckBox ID="txtIsForPooling" runat="server" Text="&nbsp;Tick if MR is for continuous pooling" ></asp:CheckBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">No. of Vacancy :</label>
                            <div class="col-md-2">
                                <asp:TextBox runat="server" ID="txtNoOfVacancy" CssClass="form-control required" Text="1" />
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtNoOfVacancy" />
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">Position Title :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboPositionNo" DataMember="EPosition" CssClass="form-control required" AutoPostBack="True" OnSelectedIndexChanged="cboPositionNo_SelectedIndexChanged" />
                            </div>
                        </div>

                        
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Plantilla No. :</label>
                            <div class="col-md-6">
                                <dx:ASPxListBox runat="server" Width="520px" SelectionMode="CheckColumn" ID="lstPlantilla" AutoPostBack="true" /> 
                                <%--<asp:ListBox ID="lstPlantilla" runat="server" SelectionMode="Multiple" CssClass="form-control">
                                </asp:ListBox>--%>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Functional Title :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboTaskNo" DataMember="ETask" CssClass="form-control" />
                            </div>
                        </div>
                        
                        <div class="form-group" >
                            <label class="col-md-3 control-label has-space">Job Level :</label>
                            <div class="col-md-6">
                                <asp:DropDownList ID="cboSalaryGradeNo" DataMember="ESalaryGrade" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        
                        <%--<div class="form-group" style="display:none">
                            <label class="col-md-3 control-label has-required">Job Level :</label>
                            <div class="col-md-6">
                                <asp:DropDownList ID="cboJobGradeNo" DataMember="EJobGrade" runat="server" CssClass="form-control required"></asp:DropDownList>
                            </div>
                        </div>--%>
                        
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Facility :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboFacilityNo" DataMember="EFacility" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Unit :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboUnitNo" DataMember="EUnit" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">Dept/Office/Region :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboDepartmentNo" DataMember="EDepartment" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Group/Branch :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboGroupNo" DataMember="EGroup" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Division :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboDivisionNo" DataMember="EDivision" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Section/Unit :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboSectionNo" DataMember="ESection" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Cost Center :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboCostCenterNo" DataMember="ECostCenter" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">Location/Area :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboLocationNo" DataMember="ELocation" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Employee Classification :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboEmployeeClassNo" DataMember="EEmployeeClass" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Employment Status :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboEmployeeStatNo" DataMember="EEmployeeStat" CssClass="form-control" />
                            </div>
                        </div>
                        
                        <br />
                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">Approval Status :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboApprovalStatNo" DataMember="EApprovalStat" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">Screening Template :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboApplicantStandardHeaderNo" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">Checklist Template :</label>
                            <div class="col-md-6">
                                <asp:DropDownList runat="server" ID="cboChecklistTemplateNo" DataMember="EChecklistTemplate" CssClass="form-control required" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:CheckBox ID="chkIsOnline" runat="server" Text="&nbsp;Tick if MR is ready for publication"></asp:CheckBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Published Date From :</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtDatePublished" runat="server" CssClass="form-control" placeholder="mm/dd/yyyy" AutoPostBack="true"> </asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" 
                                TargetControlID="txtDatePublished"
                                Format="MM/dd/yyyy" />

                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtDatePublished"
                                Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    
                                <asp:RangeValidator
                                ID="RangeValidator6"
                                runat="server"
                                ControlToValidate="txtDatePublished"
                                ErrorMessage="<b>Please enter valid entry</b>"
                                MinimumValue="1900-01-01"
                                MaximumValue="3000-12-31"
                                Type="Date" Display="None"  />
                    
                                <ajaxToolkit:ValidatorCalloutExtender 
                                runat="Server" 
                                ID="ValidatorCalloutExtender6"
                                TargetControlID="RangeValidator6" /> 
                            </div>
                            <label class="col-md-1 control-label has-space">To :</label>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtDatePublishedTo" runat="server" CssClass="form-control" placeholder="mm/dd/yyyy"> </asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                TargetControlID="txtDatePublishedTo"
                                Format="MM/dd/yyyy" />

                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtDatePublishedTo"
                                Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    
                                <asp:RangeValidator
                                ID="RangeValidator1"
                                runat="server"
                                ControlToValidate="txtDatePublishedTo"
                                ErrorMessage="<b>Please enter valid entry</b>"
                                MinimumValue="1900-01-01"
                                MaximumValue="3000-12-31"
                                Type="Date" Display="None"  />
                    
                                <ajaxToolkit:ValidatorCalloutExtender 
                                runat="Server" 
                                ID="ValidatorCalloutExtender1"
                                TargetControlID="RangeValidator1" /> 
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="col-md-3 control-label has-space"></label>
                            <div class="col-md-6">
                                <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-primary submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                                <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />
                            </div>
                        </div>
                        <br />
                        <br />
                    </div>
                </fieldset>
            </asp:Panel>
        </Content>
    </uc:Tab>
</asp:Content>

