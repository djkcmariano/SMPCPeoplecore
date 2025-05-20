<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="selfHRAN_Edit.aspx.vb" Inherits="SecuredManager_selfHRAN_Edit" %>

<%@ Register Src="~/Include/Info.ascx" TagName="Info" TagPrefix="uc" %>

<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">
    <script type="text/javascript">

        function ElementControl_DisplayFormat(ElementID, Index) {
            if (Index == 1) {
                $('#pHRANOfficeOrder').removeAttr("style");
                $('#pPublication').removeAttr("style");
                $('#pDatePublished').removeAttr("style");
                $('#pHeadOfAgency').removeAttr("style");
                $('#pDesignation').removeAttr("style");
                $('#pHRMO').removeAttr("style");
                $('#pPSB').removeAttr("style");
            }
        };

        function SalaryPermission() {
            var IsViewSalary = document.getElementById('<%=txtIsViewSalary.ClientID%>').checked;
            var IsEditSalary = document.getElementById('<%=txtIsEditSalary.ClientID%>').checked;
            var IsSalaryAdjust = document.getElementById('<%=txtIsSalaryAdjust.ClientID%>').checked;
            //If hran type allow to view the salary
            if (IsSalaryAdjust == true) {
                document.getElementById('<%=txtCurrentSalary.ClientID%>').disabled = false;
            } else {
                document.getElementById('<%=txtCurrentSalary.ClientID%>').disabled = true;
            }

            //If hran type allow to view the salary
            if (IsViewSalary == true) {
                //If user have rate permission of the employee
                if (IsEditSalary == true) {
                    $('#pcurrentsalary').removeAttr("style");
                } else {
                    $('#pcurrentsalary').css({ 'display': 'none' });
                }
            } else {
                $('#pcurrentsalary').css({ 'display': 'none' });
            }
        };


        function SetContextKey_Plantilla() {
            var position = document.getElementById('<%=hifpositionNo.ClientID%>').value;
            $find('<%=AutoCompleteExtender1.ClientID%>').set_contextKey(position);

        };

     </script>
     <uc:Tab runat="server" ID="Tab">
        <Header>        
            <asp:Label runat="server" ID="lbl" /> 
            <div style="display:none;">
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>      
        </Header>
        <Content>
            <br />
            <br />
            <asp:Panel runat="server" ID="Panel1">
                <fieldset class="form" id="fsd">
                        <div  class="form-horizontal">

                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Transaction No. :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtHRANNo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">HRAN No. :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtHRANCode" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                           

                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">Name :</label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="txtFullName"  CssClass="form-control required"/> 
                                    <asp:HiddenField runat="server" ID="hifEmployeeNo" />
                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderHRAN" runat="server"  
                                            TargetControlID="txtFullName" MinimumPrefixLength="2" 
                                            CompletionInterval="500" ServiceMethod="PopulateHranEmployee" 
                                            CompletionListCssClass="autocomplete_completionListElement" 
                                            CompletionListItemCssClass="autocomplete_listItem" EnableCaching="false"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            OnClientItemSelected="getEmployee" FirstRowSelected="true" UseContextKey="true" />
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
                                                        document.getElementById('<%= txtEmployeeCode.ClientID %>').value = "";
                                                        document.getElementById('<%= hifPlantillaNo.ClientID %>').value = "0";
                                                        document.getElementById('<%= txtPlantillaDesc.ClientID %>').value = "";
                                                        document.getElementById('<%= hifActingPlantillaNo.ClientID %>').value = "0";
                                                        document.getElementById('<%= cboTaskNo.ClientID %>').value = "";
                                                        document.getElementById('<%= hifpositionNo.ClientID %>').value = "0";
                                                        document.getElementById('<%= txtPositionDescS.ClientID %>').value = "";
                                                        document.getElementById('<%= cboSalaryGradeNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboFacilityNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboGroupNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboDepartmentNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboUnitNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboDivisionNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboSectionNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboCostCenterNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboLocationNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboProjectNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboShiftNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboDayoffNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboDayoffNo2.ClientID %>').value = "";
                                                        document.getElementById('<%= cboEmployeeClassNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboEmployeeStatNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboRankNo.ClientID %>').value = "";
                                                        document.getElementById('<%= txtIsSupervisor.ClientID %>').checked = false;
                                                        document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = "";
                                                        document.getElementById('<%= txtSFullName.ClientID %>').value = "";
                                                        document.getElementById('<%= cboPayClassNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboPayLocNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboPayTypeNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboPaymentTypeNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboEmployeeRateClassNo.ClientID %>').value = "";
                                                        document.getElementById('<%= cboTaxExemptNo.ClientID %>').value = "";
                                                        document.getElementById('<%= txtCurrentSalary.ClientID %>').value = "";
                                                        document.getElementById('<%= txtIsEditSalary.ClientID %>').checked = false;
                                                        document.getElementById('<%= txtActingPlantillaDesc.ClientID %>').value = "";
                                                        document.getElementById('<%= cboJobGradeNo.ClientID %>').value = "";
                                                    }
                                                }

                                                function getEmployee(source, eventArgs) {
                                                    document.getElementById('<%= hifEmployeeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                                    document.getElementById('<%= txtEmployeeCode.ClientID %>').value = SplitH(eventArgs.get_value(), 1);
                                                    document.getElementById('<%= hifPlantillaNo.ClientID %>').value = SplitH(eventArgs.get_value(), 2);
                                                    document.getElementById('<%= txtPlantillaDesc.ClientID %>').value = SplitH(eventArgs.get_value(), 3);
                                                    document.getElementById('<%= hifActingPlantillaNo.ClientID %>').value = SplitH(eventArgs.get_value(), 4);
                                                    document.getElementById('<%= cboTaskNo.ClientID %>').value = SplitH(eventArgs.get_value(), 5);
                                                    document.getElementById('<%= hifpositionNo.ClientID %>').value = SplitH(eventArgs.get_value(), 6);
                                                    document.getElementById('<%= txtPositionDescS.ClientID %>').value = SplitH(eventArgs.get_value(), 7);
                                                    document.getElementById('<%= cboSalaryGradeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 8);
                                                    document.getElementById('<%= cboFacilityNo.ClientID %>').value = SplitH(eventArgs.get_value(), 9);
                                                    document.getElementById('<%= cboGroupNo.ClientID %>').value = SplitH(eventArgs.get_value(), 10);
                                                    document.getElementById('<%= cboDepartmentNo.ClientID %>').value = SplitH(eventArgs.get_value(), 11);
                                                    document.getElementById('<%= cboUnitNo.ClientID %>').value = SplitH(eventArgs.get_value(), 12);
                                                    document.getElementById('<%= cboDivisionNo.ClientID %>').value = SplitH(eventArgs.get_value(), 13);
                                                    document.getElementById('<%= cboSectionNo.ClientID %>').value = SplitH(eventArgs.get_value(), 14);
                                                    document.getElementById('<%= cboCostCenterNo.ClientID %>').value = SplitH(eventArgs.get_value(), 15);
                                                    document.getElementById('<%= cboLocationNo.ClientID %>').value = SplitH(eventArgs.get_value(), 16);
                                                    document.getElementById('<%= cboProjectNo.ClientID %>').value = SplitH(eventArgs.get_value(), 17);
                                                    document.getElementById('<%= cboShiftNo.ClientID %>').value = SplitH(eventArgs.get_value(), 18);
                                                    document.getElementById('<%= cboDayoffNo.ClientID %>').value = SplitH(eventArgs.get_value(), 19);
                                                    document.getElementById('<%= cboDayoffNo2.ClientID %>').value = SplitH(eventArgs.get_value(), 20);
                                                    document.getElementById('<%= cboEmployeeClassNo.ClientID %>').value = SplitH(eventArgs.get_value(), 21);
                                                    document.getElementById('<%= cboEmployeeStatNo.ClientID %>').value = SplitH(eventArgs.get_value(), 22);
                                                    document.getElementById('<%= cboRankNo.ClientID %>').value = SplitH(eventArgs.get_value(), 23);
                                                    document.getElementById('<%= txtIsSupervisor.ClientID %>').checked = SplitH(eventArgs.get_value(), 24);
                                                    document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = SplitH(eventArgs.get_value(), 25);
                                                    document.getElementById('<%= txtSFullName.ClientID %>').value = SplitH(eventArgs.get_value(), 26);
                                                    document.getElementById('<%= cboPayClassNo.ClientID %>').value = SplitH(eventArgs.get_value(), 27);
                                                    document.getElementById('<%= cboPayLocNo.ClientID %>').value = SplitH(eventArgs.get_value(), 28);
                                                    document.getElementById('<%= cboPayTypeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 29);
                                                    document.getElementById('<%= cboPaymentTypeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 30);
                                                    document.getElementById('<%= cboEmployeeRateClassNo.ClientID %>').value = SplitH(eventArgs.get_value(), 31);
                                                    document.getElementById('<%= cboTaxExemptNo.ClientID %>').value = SplitH(eventArgs.get_value(), 32);
                                                    document.getElementById('<%= txtCurrentSalary.ClientID %>').value = SplitH(eventArgs.get_value(), 33);
                                                    document.getElementById('<%= txtIsEditSalary.ClientID %>').checked = SplitH(eventArgs.get_value(), 34);
                                                    document.getElementById('<%= txtActingPlantillaDesc.ClientID %>').value = SplitH(eventArgs.get_value(), 35);
                                                    document.getElementById('<%= cboJobGradeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 36);

                                                    SalaryPermission();
                                                }

                                                
                                                
                                            </script>
                                </div>
                               
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Employee No. :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtEmployeeCode" runat="server" cssclass="form-control" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">HRAN Type :</label>
                                <div class="col-md-6">
                                     <asp:DropdownList runat="server" ID="cboHRANTypeNo" AutoPostBack="true" OnSelectedIndexChanged="hifHRANTypeNo_ValueChanged" 
                                     CssClass="form-control required" /> 
                                </div>
                            </div>
                            


                             <div class="form-group" id="pHRANOfficeOrder" style="display:none;">
                                <label class="col-md-3 control-label has-space">Office Order No. :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtHRANOfficeOrderNo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                             </div>

                             <div class="form-group" style="visibility:hidden;position:absolute;">
                                <label class="col-md-3 control-label has-space"></label>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="txtIsConferment" runat="server" Text="&nbsp;Please check here to identify that this movement is conferment."></asp:CheckBox>
                                </div>
                             </div>

                             <div class="form-group">
                                <label class="col-md-3 control-label has-space">Reason :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboHRANTypeReasonNo" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                             </div>

                             <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Reason :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtReason" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                             </div>

                            <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Remarks :</label>
                                <div class="col-md-6" >
                                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Prepared Date :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtPreparationDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="customCalendarExtender" runat="server" TargetControlID="txtPreparationDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedPreparationDate" runat="server" TargetControlID="txtPreparationDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedPreparationDate" ControlToValidate="txtPreparationDate" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">Effective Date :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtEffectivity" runat="server" CssClass="form-control required"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffectivity" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEffectivity" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator7" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtEffectivity" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                </div>
                            </div>

                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Date of Approval :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDateofApproval" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDateofApproval" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtDateofApproval" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtDateofApproval" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                </div>
                             </div>

                             <div class="form-group">
                                <label class="col-md-3 control-label  has-space">Length of Period (in month) :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtLS" runat="server" CssClass="form-control" OnTextChanged="txtLS_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtLS" />
                                </div>
                             </div>

                             <div class="form-group">
                                <label class="col-md-3 control-label  has-space">Due Date :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDueDate" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDueDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2" ControlToValidate="txtDueDate" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                </div>
                             </div>

                             <div class="form-group" id="pDatePublished" style="display:none;">
                                <label class="col-md-3 control-label  has-space" >Date Published :</label>
                                <div class="col-md-2" >
                                    <asp:TextBox ID="txtDatePub" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDatePub" Format="MM/dd/yyyy" />
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtDatePub" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1" ControlToValidate="txtDatePub" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                </div>
                             </div>

                            <div class="form-group" id="pPublication" style="display:none;">                                        
                                <label class="col-md-3 control-label has-space">Publication :</label>
                                <div class="col-md-6" >
                                    <asp:DropDownList ID="cboPublicationLNo" DataMember="EPublicationL"  runat="server" Enabled="false" CssClass="form-control"></asp:DropDownList>
                                </div>     
                            </div>

                            <div class="form-group" id="pHeadOfAgency" style="display:none;"> 
                                <label class="col-md-3 control-label has-space">Head of Agency :</label>
                                <div class="col-md-6" >
                                    <asp:DropDownList ID="cboHRANHOANo" DataMember="EHRANHOA" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div> 
                            </div>

                            <div class="form-group" id="pDesignation" style="display:none;">
                                <label class="col-md-3 control-label has-space">Designation :</label>
                                <div class="col-md-6" >
                                    <asp:TextBox ID="txtDesignation" TextMode="MultiLine" Rows="2" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                    <asp:DropDownList ID="cboHRANHOADNo" DataMember="EHRANHOAD" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group" id="pHRMO" style="display:none;">
                                <label class="col-md-3 control-label has-space">Personnel Officer/HRMO :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtHRHead" TextMode="SingleLine" Rows="1" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                    <asp:DropDownList ID="cboHRANHRMONo" DataMember="EHRANHRMO" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group" id="pPSB" style="display:none;">
                                <label class="col-md-3 control-label has-space">Personnel Selection Board (Chairman) :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtPSBHead" TextMode="SingleLine" Rows="1" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                    <asp:DropDownList ID="cboHRANPSBNo" DataMember="EHRANPSB" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">  
                                <h5 class="col-md-8">
                                    <label class="control-label">WORK&nbsp;&nbsp;INFORMATION</label>
                                </h5>
                            </div>
                            
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space"></label>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="txtIsSupervisor" runat="server" Text="&nbsp;Tick to identify employee as an immediate head"></asp:CheckBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space" >Plantilla No. :</label>
                                <div class="col-md-6" >        
                                    <asp:LinkButton runat="server" ID="lnkItemNo" OnClick="lnkViewPlantilla_Click" CausesValidation="false" style=" font-size:11px;" Text="Click Here to view the Item No. Reference" Visible="false" />
                                    <asp:TextBox runat="server" ID="txtPlantillaDesc" onblur="ResetPlantilla()" onkeyup="SetContextKey_Plantilla()" CssClass="form-control" Placeholder="Type here..." />        
                                    <asp:HiddenField runat="server" ID="hifPlantillaNo" />
                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                        TargetControlID="txtPlantillaDesc" MinimumPrefixLength="2" EnableCaching="true" 
                                        CompletionInterval="500" ServiceMethod="PopulateItemNoInfo"
                                        OnClientItemSelected="GetRecord" FirstRowSelected="true" UseContextKey="true" />
                                        <script type="text/javascript">

                                            function Split(obj, index) {
                                                var items = obj.split("|");
                                                for (i = 0; i < items.length; i++) {
                                                    if (i == index) {
                                                        return items[i];
                                                    }
                                                }
                                            }

                                            function ResetPlantilla() {
                                                if (document.getElementById('<%= txtPlantillaDesc.ClientID %>').value == "") {
                                                    document.getElementById('<%= hifPlantillaNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= cboFacilityNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= cboGroupNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= cboDepartmentNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= cboUnitNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= cboPositionNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= cboRMCNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= cboBranchNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= cboLocationNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= cboTaskNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= cboDivisionNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= cboCostCenterNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= cboSalaryGradeNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= txtIsFacHead.ClientID %>').checked = false;
                                                    document.getElementById('<%= txtIsGroHead.ClientID %>').checked = false;
                                                    document.getElementById('<%= txtIsDepHead.ClientID %>').checked = false;
                                                    document.getElementById('<%= txtIsDivHead.ClientID %>').checked = false;
                                                    document.getElementById('<%= txtIsUniHead.ClientID %>').checked = false;
                                                    document.getElementById('<%= txtIsSecHead.ClientID %>').checked = false;
                                                    //document.getElementById('<%= txtPositionDescS.ClientID %>').value = "";
                                                    document.getElementById('<%= cboShiftNo.ClientID %>').value = "0";
                                                    document.getElementById('<%= cboSectionNo.ClientID %>').value = "0";
                                                }
                                            }

                                            function GetRecord(source, eventArgs) {
                                                document.getElementById('<%= hifPlantillaNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
                                                document.getElementById('<%= cboFacilityNo.ClientID %>').value = Split(eventArgs.get_value(), 1);
                                                document.getElementById('<%= cboGroupNo.ClientID %>').value = Split(eventArgs.get_value(), 2);
                                                document.getElementById('<%= cboDepartmentNo.ClientID %>').value = Split(eventArgs.get_value(), 3);
                                                document.getElementById('<%= cboUnitNo.ClientID %>').value = Split(eventArgs.get_value(), 4);
                                                document.getElementById('<%= cboPositionNo.ClientID %>').value = Split(eventArgs.get_value(), 5);
                                                document.getElementById('<%= cboRMCNo.ClientID %>').value = Split(eventArgs.get_value(), 6);
                                                document.getElementById('<%= cboBranchNo.ClientID %>').value = Split(eventArgs.get_value(), 7);
                                                document.getElementById('<%= cboLocationNo.ClientID %>').value = Split(eventArgs.get_value(), 8);
                                                document.getElementById('<%= cboTaskNo.ClientID %>').value = Split(eventArgs.get_value(), 9);
                                                document.getElementById('<%= cboDivisionNo.ClientID %>').value = Split(eventArgs.get_value(), 11);
                                                document.getElementById('<%= cboCostCenterNo.ClientID %>').value = Split(eventArgs.get_value(), 12);
                                                document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = Split(eventArgs.get_value(), 13);
                                                document.getElementById('<%= cboSalaryGradeNo.ClientID %>').value = Split(eventArgs.get_value(), 14);
                                                document.getElementById('<%= txtIsFacHead.ClientID %>').checked = Split(eventArgs.get_value(), 15);
                                                document.getElementById('<%= txtIsGroHead.ClientID %>').checked = Split(eventArgs.get_value(), 16);
                                                document.getElementById('<%= txtIsDepHead.ClientID %>').checked = Split(eventArgs.get_value(), 17);
                                                document.getElementById('<%= txtIsDivHead.ClientID %>').checked = Split(eventArgs.get_value(), 18);
                                                document.getElementById('<%= txtIsUniHead.ClientID %>').checked = Split(eventArgs.get_value(), 19);
                                                document.getElementById('<%= txtIsSecHead.ClientID %>').checked = Split(eventArgs.get_value(), 20);
                                                //document.getElementById('<%= txtPositionDescS.ClientID %>').value = Split(eventArgs.get_value(), 21);
                                                document.getElementById('<%= cboShiftNo.ClientID %>').value = Split(eventArgs.get_value(), 22);
                                                document.getElementById('<%= cboSectionNo.ClientID %>').value = Split(eventArgs.get_value(), 23);
                                            }                               	
                                             	
                                        </script>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space" >Position Title :</label>
                                <div class="col-md-6" >
                                    <asp:DropDownList runat="server" ID="cboPositionNo" DataMember="EPosition" CssClass="form-control"  />
                                    <asp:TextBox runat="server" ID="txtPositionDescS" CssClass="form-control" onblur="ResetPosition()" Placeholder="Type here..." Visible="false" /> 
                                    <asp:HiddenField runat="server" ID="hifpositionNo"/>
                                    <ajaxToolkit:AutoCompleteExtender ID="acePosition" runat="server"  
                                        TargetControlID="txtPositionDescS" MinimumPrefixLength="1"
                                        CompletionInterval="500" ServiceMethod="PopulateSalaryLevel" 
                                        CompletionListCssClass="autocomplete_completionListElement" 
                                        CompletionListItemCssClass="autocomplete_listItem" 
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        OnClientItemSelected="getRecordS" FirstRowSelected="true" UseContextKey="true" />
                                        <script type="text/javascript">

                                            function ResetPosition() {
                                                if (document.getElementById('<%= txtPositionDescS.ClientID %>').value == "") {
                                                    document.getElementById('<%= cboSalaryGradeNo.ClientID %>').value = "";
                                                }
                                            }

                                            function getRecordS(source, eventArgs) {
                                                document.getElementById('<%= hifpositionNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                                document.getElementById('<%= cboSalaryGradeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 1);
                                            }                               	

                                        </script>
                                </div>
                            </div>

                            

                            <div class="form-group" style="display:none;" >
                                <label class="col-md-3 control-label has-space" >Acting Manpower Item No. :</label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="txtActingPlantillaDesc" CssClass="form-control" Placeholder="Type here..." />        
                                    <asp:HiddenField runat="server" ID="hifActingPlantillaNo" />
                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" 
                                        TargetControlID="txtActingPlantillaDesc" MinimumPrefixLength="2" EnableCaching="true" 
                                        CompletionInterval="500" ServiceMethod="PopulateItemNoInfo"
                                        OnClientItemSelected="GetItem" FirstRowSelected="true" />
                                        <script type="text/javascript">

                                            function SplitX(obj, index) {
                                                var items = obj.split("|");
                                                for (i = 0; i < items.length; i++) {
                                                    if (i == index) {
                                                        return items[i];
                                                    }
                                                }
                                            }

                                            function GetItem(source, eventArgs) {
                                                document.getElementById('<%= hifActingPlantillaNo.ClientID %>').value = SplitX(eventArgs.get_value(), 0);
                                            }                               	
                                             	
                                        </script>
                                </div>
                                <div class="col-md-3">
                                    <asp:LinkButton runat="server" ID="lnkIncumbent" OnClick="lnkIncumbent_Click" CausesValidation="false" Text="View Incumbent" />
                                </div>
                            </div>

                            <div class="form-group" style="display:none;" >
                                <label class="col-md-3 control-label has-space" >Incumbent :</label>
                                <div class="col-md-6" >
                                    <asp:TextBox ID="txtIncumbent" ReadOnly="true" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group" style="display:none;" >
                                <label class="col-md-3 control-label has-space">Incumbent Position :</label>
                                <div class="col-md-6" >
                                    <asp:TextBox ID="txtIncumbentPosition" ReadOnly="true" Enabled="false" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Facility :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboFacilityNo" DataMember="EFacility" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox runat="server" ID="txtIsFacHead" Text="&nbsp; Head?" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Division :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboDivisionNo" DataMember="EDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox runat="server" ID="txtIsDivHead" Text="&nbsp; Head?" />
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Department :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboDepartmentNo" DataMember="EDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox runat="server" ID="txtIsDepHead" Text="&nbsp; Head?" />
                                </div>
                            </div>
                            
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Section :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboSectionNo" DataMember="ESection" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox runat="server" ID="txtIsSecHead" Text="&nbsp; Head?" />
                                </div>
                            </div> 
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Unit :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboUnitNo" DataMember="EUnit" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox runat="server" ID="txtIsUniHead" Text="&nbsp; Head?" />
                                </div>
                            </div>
                            
                             <div class="form-group">
                                <label class="col-md-3 control-label has-space">Group :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboGroupNo" DataMember="EGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox runat="server" ID="txtIsGroHead" Text="&nbsp; Head?" />
                                </div>
                            </div>

                                                       
                                                                                  

                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">RMC :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboRMCNo" DataMember="ERMC" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <%--<asp:CheckBox runat="server" ID="txtIsRMCHead" Text="&nbsp; Head?" />--%>
                                </div>
                            </div>

                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Branch :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboBranchNo" DataMember="EBranch" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <%--<asp:CheckBox runat="server" ID="txtIsBranchHead" Text="&nbsp; Head?" />--%>
                                </div>
                            </div>

                            
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Budget Code :</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtBranchAccountCode" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Cost Center :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboCostCenterNo" DataMember="ECostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Location :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboLocationNo" DataMember="ELocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group" style="display:none;"> 
                                <label class="col-md-3 control-label has-space">Project :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboProjectNo" DataMember="EProject" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Salary Level :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboSalaryGradeNo" DataMember="ESalaryGrade" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div id="Div1" class="col-md-2" runat="server" visible="false">
                                    <asp:CheckBox runat="server" ID="chkIsForRata" Text="&nbsp;For Rata?" />
                                </div>
                            </div>

                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Step Increment  :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboStep" DataMember="EStep" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space" >Functional Title :</label>
                                <div class="col-md-6" >
                                    <asp:DropDownList ID="cboTaskNo" DataMember="ETask" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            
                            <div class="form-group" style=" display:none;">
                                <label class="col-md-3 control-label has-space">Job Grade :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboJobGradeNo" DataMember="EJobGrade" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Employee Classification :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboEmployeeClassNo" DataMember="EEmployeeClass" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Employee Status :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboEmployeeStatNo" DataMember="EEmployeeStat" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Rank :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboRankNo" DataMember="ERank" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Immediate Head :</label>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="txtSFullName" CssClass="form-control" style="display:inline-block;" Placeholder="Type here..." /> 
                                    <asp:HiddenField runat="server" ID="hifImmediateSuperiorNo"/>
                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"  
                                    TargetControlID="txtSFullName" MinimumPrefixLength="2" 
                                    CompletionInterval="500" ServiceMethod="PopulateManager" 
                                    CompletionListCssClass="autocomplete_completionListElement" 
                                    CompletionListItemCssClass="autocomplete_listItem" 
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                    OnClientItemSelected="getImmediate" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                     <script type="text/javascript">
                                         function getImmediate(source, eventArgs) {
                                             document.getElementById('<%= hifImmediateSuperiorNo.ClientID %>').value = eventArgs.get_value();
                                         }
                                    </script>                                                                    
                                </div>
                            </div>

                            <br />
                            <div class="form-group">  
                                <h5 class="col-md-8">
                                    <label class="control-label">ATTENDANCE&nbsp;&nbsp;RELATED&nbsp;&nbsp;INFORMATION</label>
                                </h5>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Shift :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboShiftNo" DataMember="EShift" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Day Off 1 :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboDayOffNo" DataMember="EDayOff" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group" >
                                <label class="col-md-3 control-label has-space">Day Off 2 :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboDayOffNo2" DataMember="EDayOff" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <br />
                            <div class="form-group">  
                                <h5 class="col-md-8">
                                    <label class="control-label">PAYROLL&nbsp;&nbsp;RELATED&nbsp;&nbsp;INFORMATION</label>
                                </h5>
                            </div>
                            
                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">Company Name :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboPayLocNo" DataMember="EPayLoc" runat="server" CssClass="form-control required"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-required">Payroll Group :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboPayClassNo" DataMember="EPayClass" runat="server" CssClass="form-control required"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Payroll Type :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboPayTypeNo" DataMember="EPayType" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Payment Type :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboPaymentTypeNo" DataMember="EPaymentType" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space"></label>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="txtIsDontDeductTax" runat="server" Text="&nbsp;Tick here if minimum wage earner (MWE)"></asp:CheckBox>
                                </div>
                            </div>
                            
                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Tax Exemption :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboTaxExemptNo" DataMember="ETaxExempt" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Rate Class :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboEmployeeRateClassNo" DataMember="EEmployeeRateClass" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            
                            <div class="form-group" style="visibility:hidden;position:absolute;">
                                <label class="col-md-3 control-label"> </label>
                                <div class="col-md-8" >
                                    <asp:CheckBox ID="txtIsSalaryAdjust" runat="server" />
                                    <asp:CheckBox ID="txtIsViewSalary" runat="server" />
                                    <asp:CheckBox ID="txtIsEditSalary" runat="server" />
                                </div>
                            </div>

                            <div class="form-group" id="pcurrentsalary" style="display:none;">
                                <label class="col-md-3 control-label has-space">Current Salary :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtCurrentSalary" runat="server" CssClass="form-control"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers" ValidChars="." TargetControlID="txtCurrentSalary" />
                                </div>
                            </div>
                            
                             <div class="form-group" id="pHRANCorrected" style="display:none;">
                                <label class="col-md-3 control-label has-space">HRAN Corrected No. :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboHRANCorrectedNo" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                             </div>
                            
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Allowance Template :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboAllowTempNo" DataMember="EAllowTemp" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label has-space">Approval Status :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboApprovalStatNo" DataMember="EApprovalStat" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group" style="display:none">
                                <label class="col-md-3 control-label has-space"></label>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="txtIsPosting" runat="server" Text="&nbsp;Tick if transaction is ready for posting"></asp:CheckBox><br />
                                    <code>To enable the "ready for posting", approval status must be approved and required checklist must be completed.</code>
                                </div>
                            </div>

                            
                            <div class="form-group" style="display:none;">
                                <label class="col-md-3 control-label has-space">Content Report :</label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboHRANRCNo" DataMember="EHRANRCL" runat="server" CssClass="form-control"></asp:DropDownList>
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

            <uc:Info runat="server" ID="Info1" /> 

        </Content>
    </uc:Tab>
    
        
                   


<asp:Button ID="btnShowPlantilla" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mdlPlantilla" runat="server" 
    BackgroundCssClass="modalBackground" CancelControlID="imgClosedPlantilla" 
    PopupControlID="pnlpopupPlantilla" TargetControlID="btnShowPlantilla">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlPopupPlantilla" runat="server" CssClass="entryPopup">
    <fieldset class="form" id="Fieldset1">
        <!-- Header here -->
         <div class="cf popupheader">
                <h4>Plantilla reference</h4>
                <asp:Linkbutton runat="server" ID="imgClosedPlantilla" CssClass="cancel fa fa-times" ToolTip="Close" />&nbsp;
                 
         </div>
         <!-- Body here -->
         <div  class="entryPopupDetl form-horizontal">
            <div class="form-group">
                <label class="col-md-4 control-label">Item No. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPlantillaCode" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Board resolution no. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtBoardResolutionNo" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Board resolution date :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtBoardresdate" runat="server" ReadOnly="true"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" 
                    TargetControlID="txtBoardresdate"
                    Format="MM/dd/yyyy" />

                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtBoardresdate"
                    Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />
                    
                    <asp:RangeValidator
                    ID="RangeValidator5"
                    runat="server"
                    ControlToValidate="txtBoardresdate"
                    ErrorMessage="<b>Please enter valid entry</b>"
                    MinimumValue="01-01-1900"
                    MaximumValue="12-31-3000"
                    Type="Date" Display="None"  />
                    
                    <ajaxToolkit:ValidatorCalloutExtender 
                    runat="Server" 
                    ID="ValidatorCalloutExtender5"
                    TargetControlID="RangeValidator5" />

                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Series no. :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtSeriesCode" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Position :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPositionDesc" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group" >
                <label class="col-md-4 control-label">Functional Assignment :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtTaskDesc" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Prepared by :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtPreparedBy" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Approved by :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtApprovedBy" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Headcount :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtHeadCount" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Facility :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtFacilityDesc" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Group :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtGroupDesc" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Department :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtDepartmentDesc" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Unit :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtUnitDesc" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Division :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtDivisionDesc" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Section :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtSectionDesc" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Salary scale :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtSalaryGradeDesc" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                <label class="col-md-4 control-label">Employee status :</label>
                <div class="col-md-7">
                    <asp:TextBox ID="txtEmployeeStatDesc" runat="server" ReadOnly="true"></asp:TextBox>
                </div>
            </div>
        </div>
        <br />
         </fieldset>
    </asp:Panel>

</asp:Content>