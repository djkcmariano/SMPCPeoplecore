<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpHRANListTransfer_Edit.aspx.vb" Inherits="Secured_EmpHRANEditTransfer" %>

<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">
    <script type="text/javascript">

        function ElementControl_DisplayFormat(ElementID, Index) {

            if (Index == 1) {
                $('#pPublication').css({ 'display': 'inline-block' });
                $('#pDatePublished').css({ 'display': 'inline-block' });
                $('#pHeadOfAgency').css({ 'display': 'inline-block' });
                $('#pDesignation').css({ 'display': 'inline-block' });
                $('#pHRMO').css({ 'display': 'inline-block' });
                $('#pPSB').css({ 'display': 'inline-block' });


            } else {
                $('#pPublication').css({ 'display': 'none' });
                $('#pDatePublished').css({ 'display': 'none' });
                $('#pHeadOfAgency').css({ 'display': 'none' });
                $('#pDesignation').css({ 'display': 'none' });
                $('#pHRMO').css({ 'display': 'none' });
                $('#pPSB').css({ 'display': 'none' });

            }
        };

       
      
        

     </script>


    <div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                <div class="panel-body">
                   <fieldset class="form" id="fsd">
                        <div  class="form-horizontal">
                            <div class="form-group" style="display:none;">
                                <label class="col-md-2 control-label">Transaction No. :</label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtHRANno" runat="server"></asp:TextBox>
                                    </div>
                             </div>
                             <div class="form-group">
                                    <label class="col-md-2 control-label">HRAN No. :</label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtHRANCode" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                    </div>
                             </div>
                             
                             <div class="form-group">
                                    <label class="col-md-2 control-label has-required">Name :</label>
                                    <div class="col-md-3">
                                        <asp:TextBox runat="server" ID="txtFullName"   CssClass="form-control"/> 
                                        <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtenderHRAN" runat="server"  
                                                TargetControlID="txtFullName" MinimumPrefixLength="2" 
                                                CompletionInterval="500" ServiceMethod="PopulateHranEmployee" 
                                                CompletionListCssClass="autocomplete_completionListElement" 
                                                CompletionListItemCssClass="autocomplete_listItem" EnableCaching="false"
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                OnClientItemSelected="getEmployee" FirstRowSelected="true"  />
                                                <script type="text/javascript">
                                                    function SplitH(obj, index) {
                                                        var items = obj.split("|");
                                                        for (i = 0; i < items.length; i++) {
                                                            if (i == index) {
                                                                return items[i];
                                                            }
                                                        }
                                                    }
                                                    function getEmployeex(source, eventArgs) {
                                                        var empId = "<%= hifEmployeeNo.ClientID %>";
                                                        document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                                                        //__doPostBack(empId, "");
                                                    }
                                                    function getEmployee(source, eventArgs) {
                                                        document.getElementById('<%= hifEmployeeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                                        document.getElementById('<%= cboEmployeeClassNo.ClientID %>').value = SplitH(eventArgs.get_value(), 1);
                                                        document.getElementById('<%= cboDayoffNo.ClientID %>').value = SplitH(eventArgs.get_value(), 2);
                                                        document.getElementById('<%= cboDayoffNo2.ClientID %>').value = SplitH(eventArgs.get_value(), 3);
                                                        document.getElementById('<%= cboDepartmentNo.ClientID %>').value = SplitH(eventArgs.get_value(), 4);
                                                        document.getElementById('<%= cboDivisionNo.ClientID %>').value = SplitH(eventArgs.get_value(), 5);
                                                        document.getElementById('<%= cboGroupNo.ClientID %>').value = SplitH(eventArgs.get_value(), 6);
                                                        document.getElementById('<%= hfPlantillaNo.ClientID %>').value = SplitH(eventArgs.get_value(), 7);
                                                        document.getElementById('<%= cboSalaryGradeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 8);
                                                        document.getElementById('<%= cboLocationNo.ClientID %>').value = SplitH(eventArgs.get_value(), 9);
                                                        document.getElementById('<%= cboPayClassNo.ClientID %>').value = SplitH(eventArgs.get_value(), 10);
                                                        document.getElementById('<%= cboPayTypeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 11);
                                                        document.getElementById('<%= hifpositionNo.ClientID %>').value = SplitH(eventArgs.get_value(), 12);
                                                        document.getElementById('<%= cboProjectNo.ClientID %>').value = SplitH(eventArgs.get_value(), 13);
                                                        document.getElementById('<%= cboEmployeeRateClassNo.ClientID %>').value = SplitH(eventArgs.get_value(), 14);
                                                        document.getElementById('<%= cboSectionNo.ClientID %>').value = SplitH(eventArgs.get_value(), 15);
                                                        document.getElementById('<%= cboShiftNo.ClientID %>').value = SplitH(eventArgs.get_value(), 16);
                                                        document.getElementById('<%= cboEmployeeStatNo.ClientID %>').value = SplitH(eventArgs.get_value(), 17);
                                                        document.getElementById('<%= cboImmediateSuperiorNo.ClientID %>').value = SplitH(eventArgs.get_value(), 18);
                                                        document.getElementById('<%= cboTaxExemptNo.ClientID %>').value = SplitH(eventArgs.get_value(), 19);
                                                        document.getElementById('<%= cboUnitNo.ClientID %>').value = SplitH(eventArgs.get_value(), 20);
                                                        document.getElementById('<%= cboRankNo.ClientID %>').value = SplitH(eventArgs.get_value(), 22);
                                                        document.getElementById('<%= cboActing.ClientID %>').value = SplitH(eventArgs.get_value(), 23);
                                                        document.getElementById('<%= cboFacilityNo.ClientID %>').value = SplitH(eventArgs.get_value(), 24);
                                                        document.getElementById('<%= cboTaskNo.ClientID %>').value = SplitH(eventArgs.get_value(), 25);
                                                        document.getElementById('<%= txtPlantillaDesc.ClientID %>').value = SplitH(eventArgs.get_value(), 26);
                                                        document.getElementById('<%= txtEmployeeCode.ClientID %>').value = SplitH(eventArgs.get_value(), 27);
                                                        var isAllowEdit = SplitH(eventArgs.get_value(), 28);
                                                        var isViewSal = SplitH(eventArgs.get_value(), 29);

                                                    

                                                        //if ($("#txtCurrentSalary").is(":visible")) {
                                                        document.getElementById('<%= txtCurrentSalary.ClientID %>').value = SplitH(eventArgs.get_value(), 21);
                                                        //}
                                                        document.getElementById('<%= txtPositionDescS.ClientID %>').value = SplitH(eventArgs.get_value(), 30);



                                                    }                               	

                                                </script>
                                    </div>
                               
                                    <label class="col-md-2 control-label">Employee No. :</label>
                                    <div class="col-md-3">
                                                <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                              </div>
                             <div class="form-group">
                                    <label class="col-md-2 control-label has-required">HRAN Type :</label>
                                    <div class="col-md-3">
                                                <asp:TextBox runat="server" ID="txtHRANTypeDesc" CssClass=" form-control" /> 
                                                <asp:HiddenField runat="server" ID="hifHRANTypeNo"/>
                                                <ajaxToolkit:AutoCompleteExtender ID="aceHRANType" runat="server"  
                                                TargetControlID="txtHRANTypeDesc" MinimumPrefixLength="1" 
                                                CompletionInterval="500" ServiceMethod="populateHranType" 
                                                CompletionListCssClass="autocomplete_completionListElement" 
                                                CompletionListItemCssClass="autocomplete_listItem" 
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                OnClientItemSelected="getRecordH" FirstRowSelected="true" UseContextKey="true" />
                                                <script type="text/javascript">

                                                    function getRecordH(source, eventArgs) {
                                                        document.getElementById('<%= hifHRANTypeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);

                                                        var dis = SplitH(eventArgs.get_value(), 1);

                                                        if (dis == 'False') {
                                                            document.getElementById('<%=txtIsDepHead.ClientID%>').disabled = false;
                                                            document.getElementById('<%=txtIsDivHead.ClientID%>').disabled = false;
                                                            document.getElementById('<%=txtIsFacHead.ClientID%>').disabled = false;
                                                            document.getElementById('<%=txtIsGroHead.ClientID%>').disabled = false;
                                                            document.getElementById('<%=txtIsSecHead.ClientID%>').disabled = false;
                                                            document.getElementById('<%=txtIsUniHead.ClientID%>').disabled = false;
                                                        } else {
                                                            document.getElementById('<%=txtIsDepHead.ClientID%>').disabled = true;
                                                            document.getElementById('<%=txtIsDivHead.ClientID%>').disabled = true;
                                                            document.getElementById('<%=txtIsFacHead.ClientID%>').disabled = true;
                                                            document.getElementById('<%=txtIsGroHead.ClientID%>').disabled = true;
                                                            document.getElementById('<%=txtIsSecHead.ClientID%>').disabled = true;
                                                            document.getElementById('<%=txtIsUniHead.ClientID%>').disabled = true;
                                                        }

                                                        var isViewSal = SplitH(eventArgs.get_value(), 2);
                                                        // alert(isViewSal);
                                                        
                                                        var index = SplitH(eventArgs.get_value(), 3);

                                                        ElementControl_DisplayFormat('#pPublication', index)

                                                    }                               	

                                                </script>
                                            </div>
                                        
                                    <label class="col-md-2 control-label">HRAN Corrected No. :</label>
                                    <div class="col-md-3">
                                                <asp:DropDownList ID="cboHRANCorrectedNo" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                             </div>
                             <div class="form-group" style="display:none;">
                                    <label class="col-md-2 control-label">Office order no. :</label>
                                    <div class="col-md-8">
                                                <asp:TextBox ID="txtHRANOfficeOrderNo"  skinid="txtdate" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                             </div>
                             <div id="Div1" class="form-group" runat="server" visible="false">
                                    <label class="col-md-2 control-label">Please click here</label>
                                    <div class="col-md-8">
                                                <asp:CheckBox ID="txtIsConferment"   runat="server"></asp:CheckBox>&nbsp;<span>to identify that this movement is conferment.</span>
                                            </div>
                             </div>
                             <div class="form-group">
                                    <label class="col-md-2 control-label">Reason :</label>
                                    <div class="col-md-8">
                                                <asp:TextBox ID="txtReason" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                             </div>
                             <div class="form-group">
                                    <label class="col-md-2 control-label has-required">Prepared date :</label>
                                    <div class="col-md-3">
                                                <asp:TextBox ID="txtPreparationDate" runat="server" SkinID="txtdate" CssClass=" form-control"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="customCalendarExtender" runat="server" TargetControlID="txtPreparationDate" Format="MM/dd/yyyy" />
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedSeparatedDate" runat="server" TargetControlID="txtPreparationDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                    ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="MaskedSeparatedDate"
                                                    ControlToValidate="txtPreparationDate" IsValidEmpty="true" EmptyValueMessage=""
                                                    InvalidValueMessage="Date is invalid" ValidationGroup="Demo1" Display="Dynamic"
                                                    TooltipMessage="" />
                                            </div>
                                       
                                        <label class="col-md-2 control-label">Effective Date / Date of Assumption :</label>
                                        <div class="col-md-3">
                                                <asp:TextBox ID="txtEffectivity" runat="server" SkinID="txtdate" CssClass="form-control" ></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffectivity" Format="MM/dd/yyyy" />
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEffectivity"
                                                    Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                    ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator7" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtEffectivity" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid"
                                                    ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                            </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Date of approval :</label>
                                        <div class="col-md-3">
                                                <asp:TextBox ID="txtDateofApproval" runat="server" SkinID="txtdate" CssClass="form-control"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDateofApproval" Format="MM/dd/yyyy" />
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtDateofApproval"
                                                    Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                    ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator4" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtDateofApproval" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid"
                                                    ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                            </div>
                                        
                                        <label class="col-md-2 control-label">Length of period (no. of months) :</label>
                                        <div class="col-md-3">
                                                <asp:TextBox ID="txtLS" SkinID="txtdate" runat="server" CssClass="form-control"></asp:TextBox>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers" 
                                                ValidChars="." TargetControlID="txtLS" />
                                            </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Due date :</label>
                                        <div class="col-md-3">
                                                <asp:TextBox ID="txtDueDate" runat="server" SkinID="txtdate" CssClass="form-control"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDueDate" Format="MM/dd/yyyy" />
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtDueDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                    ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" ControlExtender="MaskedEditExtender2"
                                                    ControlToValidate="txtDueDate" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid"
                                                    ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                            </div>
                                    </div>
                                    <div class="form-group">
                                        <div  id="pDatePublished">
                                        <label class="col-md-2 control-label" >Date Published :</label>
                                        <div class="col-md-3" >
                                                <asp:TextBox ID="txtDatePub" runat="server" SkinID="txtdate" CssClass="form-control"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDatePub" Format="MM/dd/yyyy" />
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtDatePub"
                                                    Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                    ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlExtender="MaskedEditExtender1"
                                                    ControlToValidate="txtDatePub" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid"
                                                    ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                            </div>
                                        </div>
                                     </div>
                                    <div class="form-group" id="pPublication">
                                        
                                            <label class="col-md-2 control-label">Publication :</label>
                                            <div class="col-md-8" >
                                                <asp:DropDownList ID="cboPublicationLNo" DataMember="EPublicationL"  runat="server" Enabled="false" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        
                                    </div>
                                    <div class="form-group" id="pHeadOfAgency">
                                       
                                            <label class="col-md-2 control-label">Head of Agency :</label>
                                            <div class="col-md-8" >
                                                <asp:DropDownList ID="cboHRANHOANo" DataMember="EHRANHOA" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        
                                    </div>
                                    <div class="form-group" id="pDesignation">
                                    
                                            <label class="col-md-2 control-label">Designation :</label>
                                            <div class="col-md-8" >

                                                <asp:TextBox ID="txtDesignation" TextMode="MultiLine" Rows="2" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                <asp:DropDownList ID="cboHRANHOADNo" DataMember="EHRANHOAD" runat="server"></asp:DropDownList>
                                            </div>
                                       
                                    </div>
                                    <div class="form-group" id="pHRMO">
                                      
                                            <label class="col-md-2 control-label">Personnel Officer/HRMO :</label>
                                            <div class="col-md-8">
                                       
                                                <asp:TextBox ID="txtHRHead" TextMode="SingleLine" Rows="1" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                <asp:DropDownList ID="cboHRANHRMONo" DataMember="EHRANHRMO" runat="server"></asp:DropDownList>
                                            </div>
                                       
                                    </div>
                                    <div class="form-group" id="pPSB">
                                       
                                            <label class="col-md-2 control-label">Personnel Selection Board (Chairman) :</label>
                                            <div class="col-md-8">
                                      
                                                <asp:TextBox ID="txtPSBHead" TextMode="SingleLine" Rows="1" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                <asp:DropDownList ID="cboHRANPSBNo" DataMember="EHRANPSB" runat="server"></asp:DropDownList>
                                            </div>
                                       
                                    </div>
                                    <div class="form-group" >
                                            <label class="col-md-2 control-label"> Remark :</label>
                                            <div class="col-md-8" >
                                       
                                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                    </div>
                                    <div class="form-group">
                                            <label class="col-md-2 control-label" >Plantilla number :</label>
                                            <div class="col-md-3" >
                                                
                                                <asp:LinkButton runat="server" ID="lnkItemNo" OnClick="lnkViewPlantilla_Click" CausesValidation="false" style=" font-size:11px;" Text="Click Here to view the Item No. Reference" Visible="false" />
                                                <asp:TextBox runat="server" ID="txtPlantillaDesc" onblur="Reset()" CssClass="form-control" />        
                                                <asp:HiddenField runat="server" ID="hfPlantillaNo" />
                                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                                       TargetControlID="txtPlantillaDesc" MinimumPrefixLength="2" EnableCaching="true" 
                                                       CompletionSetCount="12" CompletionInterval="500" ServiceMethod="PopulateItemNoInfo"
                                                       OnClientItemSelected="GetRecord" FirstRowSelected="true" />
                                                       <script type="text/javascript">

                                                           function Split(obj, index) {
                                                               var items = obj.split("|");
                                                               for (i = 0; i < items.length; i++) {
                                                                   if (i == index) {
                                                                       return items[i];
                                                                   }
                                                               }
                                                           }

                                                           function Reset() {
                                                               if (document.getElementById('<%= txtPlantillaDesc.ClientID %>').value == "") {
                                                                   document.getElementById('<%= hfPlantillaNo.ClientID %>').value = "0";
                                                                   document.getElementById('<%= cboFacilityNo.ClientID %>').value = "";
                                                                   document.getElementById('<%= cboGroupNo.ClientID %>').value = "";
                                                                   document.getElementById('<%= cboDepartmentNo.ClientID %>').value = "";
                                                                   document.getElementById('<%= cboUnitNo.ClientID %>').value = "";
                                                                   document.getElementById('<%= hifpositionNo.ClientID %>').value = "";
                                                                   //document.getElementById('<%= cboRMCNo.ClientID %>').value = "";
                                                                   //document.getElementById('<%= cboBranchNo.ClientID %>').value = "";
                                                                   document.getElementById('<%= cboLocationNo.ClientID %>').value = "";
                                                                   document.getElementById('<%= cboTaskNo.ClientID %>').value = "";

                                                                   document.getElementById('<%= cboDivisionNo.ClientID %>').value = "";
                                                                   document.getElementById('<%= cboCostCenterNo.ClientID %>').value = "";
                                                                   document.getElementById('<%= cboImmediateSuperiorNo.ClientID %>').value = "";
                                                                   document.getElementById('<%= cboSalaryGradeNo.ClientID %>').value = "";
                                                                   document.getElementById('<%= cboSalaryGradeNo.ClientID %>').value = "";
                                                                   document.getElementById('<%= txtIsFacHead.ClientID %>').checked = false;
                                                                   document.getElementById('<%= txtIsGroHead.ClientID %>').checked = false;
                                                                   document.getElementById('<%= txtIsDepHead.ClientID %>').checked = false;
                                                                   document.getElementById('<%= txtIsDivHead.ClientID %>').checked = false;
                                                                   document.getElementById('<%= txtIsUniHead.ClientID %>').checked = false;
                                                                   document.getElementById('<%= txtIsSecHead.ClientID %>').checked = false;
                                                               }
                                                           }

                                                           function GetRecord(source, eventArgs) {
                                                               document.getElementById('<%= hfPlantillaNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
                                                               document.getElementById('<%= cboFacilityNo.ClientID %>').value = Split(eventArgs.get_value(), 1);
                                                               document.getElementById('<%= cboGroupNo.ClientID %>').value = Split(eventArgs.get_value(), 2);
                                                               document.getElementById('<%= cboDepartmentNo.ClientID %>').value = Split(eventArgs.get_value(), 3);
                                                               document.getElementById('<%= cboUnitNo.ClientID %>').value = Split(eventArgs.get_value(), 4);
                                                               document.getElementById('<%= hifpositionNo.ClientID %>').value = Split(eventArgs.get_value(), 5);
                                                               //document.getElementById('<%= cboRMCNo.ClientID %>').value = Split(eventArgs.get_value(), 6);
                                                               //document.getElementById('<%= cboBranchNo.ClientID %>').value = Split(eventArgs.get_value(), 7);
                                                               document.getElementById('<%= cboLocationNo.ClientID %>').value = Split(eventArgs.get_value(), 8);
                                                               document.getElementById('<%= cboTaskNo.ClientID %>').value = Split(eventArgs.get_value(), 9);

                                                               document.getElementById('<%= cboDivisionNo.ClientID %>').value = Split(eventArgs.get_value(), 11);
                                                               document.getElementById('<%= cboCostCenterNo.ClientID %>').value = Split(eventArgs.get_value(), 12);
                                                               document.getElementById('<%= cboImmediateSuperiorNo.ClientID %>').value = Split(eventArgs.get_value(), 13);
                                                               document.getElementById('<%= cboSalaryGradeNo.ClientID %>').value = Split(eventArgs.get_value(), 14);
                                                               document.getElementById('<%= txtIsFacHead.ClientID %>').checked = Split(eventArgs.get_value(), 15);
                                                               document.getElementById('<%= txtIsGroHead.ClientID %>').checked = Split(eventArgs.get_value(), 16);
                                                               document.getElementById('<%= txtIsDepHead.ClientID %>').checked = Split(eventArgs.get_value(), 17);
                                                               document.getElementById('<%= txtIsDivHead.ClientID %>').checked = Split(eventArgs.get_value(), 18);
                                                               document.getElementById('<%= txtIsUniHead.ClientID %>').checked = Split(eventArgs.get_value(), 19);
                                                               document.getElementById('<%= txtIsSecHead.ClientID %>').checked = Split(eventArgs.get_value(), 20);
                                                               document.getElementById('<%= txtPositionDescS.ClientID %>').value = Split(eventArgs.get_value(), 21);
                                                               document.getElementById('<%= cboShiftNo.ClientID %>').value = Split(eventArgs.get_value(), 22);
                                                           }                               	
                                             	
                                                       </script>
                                            </div>
                                       
                                            <label class="col-md-2 control-label" >Functional title :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboTaskNo" DataMember="ETask" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    <div class="form-group" >
                                            <label class="col-md-2 control-label" >Acting item no. :</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="cboActing" DataMember="EPlantilla" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    <div class="form-group"  style="display:none;">
                                            <label class="col-md-2 control-label" >Incumbent :</label>
                                            <div class="col-md-8" >
                                                <asp:TextBox ID="txtIncumbent" TextMode="SingleLine" Rows="1" ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="form-group"  style="display:none;">
                                            <label class="col-md-2 control-label">Incumbent position :</label>
                                            <div class="col-md-8" >
                                                <asp:TextBox ID="txtIncumbentPosition" TextMode="SingleLine" Rows="1" ReadOnly="true" Enabled="false" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="form-group">
                                            <label class="col-md-2 control-label" >Position Title :</label>
                                            <div class="col-md-3" >
                                                <asp:TextBox runat="server" ID="txtPositionDescS" CssClass="form-control" /> 
                                                <asp:HiddenField runat="server" ID="hifpositionNo"/>
                                                <ajaxToolkit:AutoCompleteExtender ID="acePosition" runat="server"  
                                                TargetControlID="txtPositionDescS" MinimumPrefixLength="1" 
                                                CompletionInterval="500" ServiceMethod="PopulateSalaryLevel" 
                                                CompletionListCssClass="autocomplete_completionListElement" 
                                                CompletionListItemCssClass="autocomplete_listItem" 
                                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                OnClientItemSelected="getRecordS" FirstRowSelected="true" UseContextKey="true" />
                                                <script type="text/javascript">

                                                    function getRecordS(source, eventArgs) {
                                                        document.getElementById('<%= hifpositionNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                                        document.getElementById('<%= cboSalaryGradeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 1);
                                                        document.getElementById('<%= cboShiftNo.ClientID %>').value = SplitH(eventArgs.get_value(), 2);
                                                    }                               	

                                                </script>
                                            </div>
                                        </div>
                                    <div class="form-group" >
                                            <label class="col-md-2 control-label" >Salary Level :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboJobgradeNo" DataMember="EJobGrade" runat="server" Visible="false"></asp:DropDownList>
                                                <asp:DropDownList ID="cboSalaryGradeNo" DataMember="ESalaryGrade" runat="server" CssClass="form-control"></asp:DropDownList>
                                                &nbsp;&nbsp;
                                                
                                            </div>
                                            <div class="col-md-1"><asp:CheckBox runat="server" ID="chkIsForRata" />
                                                &nbsp;&nbsp;<span>For Rata?</span>
                                                </div>
                                        </div>
                                    <div class="form-group">
                                            <label class="col-md-2 control-label" >Facility :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboFacilityNo" DataMember="EFacility" runat="server" CssClass="form-control"></asp:DropDownList>
                                                &nbsp;&nbsp;
                                                
                                            </div>
                                            <div class="col-md-1" ><asp:CheckBox runat="server" ID="txtIsFacHead" />
                                                &nbsp;&nbsp;<span>Facility Head?</span>
                                                </div>
                                            <label class="col-md-1 control-label">Group :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboGroupNo" DataMember="EGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                                &nbsp;&nbsp;
                                                
                                            </div>
                                            <div class="col-md-1" ><asp:CheckBox runat="server" ID="txtIsGroHead" />
                                                &nbsp;&nbsp;<span>Group Head?</span>
                                             </div>
                                        </div>
                                    <div id="Div2" class="form-group" runat="server" visible="false">
                                            <label class="col-md-2 control-label" >RMC :</label>
                                            <div class="col-md-8">  
                                                <asp:DropDownList ID="cboRMCNo" DataMember="ERMC" runat="server"></asp:DropDownList>
                                                &nbsp;&nbsp;
                                                <asp:CheckBox runat="server" ID="txtIsRMCHead" Visible="true" />
                                                &nbsp;&nbsp;<span style=" display:inline-block;">RMC Head?</span>
                                            </div>
                                        </div>
                                    <div id="Div3" class="form-group" runat="server" visible="false">
                                            <label class="col-md-2 control-label">Branch :</label>
                                            <div class="col-md-8" >
                                                <asp:DropDownList ID="cboBranchNo" DataMember="EBranch" runat="server"></asp:DropDownList>
                                                &nbsp;&nbsp;
                                                <asp:CheckBox runat="server" ID="txtIsBranchHead" />
                                                &nbsp;&nbsp;<span>Branch Head?</span>
                                            </div>
                                     </div>

                                    <div class="form-group">
                                            <label class="col-md-2 control-label" >Department :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboDepartmentNo" DataMember="EDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                                &nbsp;&nbsp;
                                                
                                            </div>
                                            <div class="col-md-1"><asp:CheckBox runat="server" ID="txtIsDepHead" />
                                                &nbsp;&nbsp;<span>Department Head?</span>
                                            </div>
                                        
                                    
                                   
                                            <label class="col-md-1 control-label">Unit :</label>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="cboUnitNo" DataMember="EUnit" runat="server" CssClass="form-control"></asp:DropDownList>
                                                &nbsp;&nbsp;
                                                
                                            </div>
                                            <div class="col-md-1"><asp:CheckBox runat="server" ID="txtIsUniHead" />
                                                &nbsp;&nbsp;<span>Unit Head?</span>
                                             </div>
                                        </div>
                                    <div class="form-group" >
                                            <label class="col-md-2 control-label" >Division :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboDivisionNo" DataMember="EDivision" runat="server" CssClass="form-control"></asp:DropDownList>
                                                &nbsp;&nbsp;
                                                
                                            </div>
                                            <div class="col-md-1"><asp:CheckBox runat="server" ID="txtIsDivHead" />
                                                &nbsp;&nbsp;<span>Division Head?</span>
                                             </div>
                                    <div class="form-group" style="display:none;">
                                            <label class="col-md-2 control-label" >Budget Code :</label>
                                            <div class="col-md-8" >
                                                <asp:TextBox ID="txtBranchAccountCode" TextMode="SingleLine" Rows="1" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    
                                    
                                            <label class="col-md-1 control-label" >Section :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboSectionNo" DataMember="ESection" runat="server" CssClass="form-control"></asp:DropDownList>
                                                &nbsp;&nbsp;
                                                
                                            </div>
                                            <div class="col-md-1">
                                                <asp:CheckBox runat="server" ID="txtIsSecHead" />
                                                &nbsp;&nbsp;<span>Section Head?</span>
                                            </div>
                                        </div>
                                    <div class="form-group">
                                            <label class="col-md-2 control-label">Cost center :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboCostCenterNo" DataMember="ECostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                       
                                            <label class="col-md-2 control-label" >Location :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboLocationNo" DataMember="ELocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    <div class="form-group">
                                            <label class="col-md-2 control-label">Project :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboProjectNo" DataMember="EProject" runat="server"  CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        
                                            <label class="col-md-2 control-label">Shift :</label>
                                            <div class="col-md-3">
                                                <asp:DropDownList ID="cboShiftNo" DataMember="EShift" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    <div class="form-group">
                                            <label class="col-md-2 control-label" >Day off 1 :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboDayOffNo" DataMember="EDayOff" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                       
                                            <label class="col-md-2 control-label" >Day off 2 :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboDayOffNo2" DataMember="EDayOff" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    <div class="form-group" style="display:none;">
                                            <label class="col-md-2 control-label">Step increment :</label>
                                            <div class="col-md-8" >
                                                <asp:DropDownList ID="cboStep" DataMember="EStep" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    <div class="form-group" >
                                            <label class="col-md-2 control-label" >Classification :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboEmployeeClassNo" DataMember="EEmployeeClass" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        
                                            <label class="col-md-2 control-label has-required" >Status :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboEmployeeStatNo" DataMember="EEmployeeStat" runat="server" CssClass=" form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    <div class="form-group" >
                                            <label class="col-md-2 control-label" >Rank :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboRankNo" DataMember="ERank" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        
                                            <label class="col-md-2 control-label" >Please click here</label>
                                            <div class="col-md-3" >
                                                <asp:CheckBox ID="txtIsSupervisor" runat="server"></asp:CheckBox>&nbsp;<span>to identify that he/she is an immediate superior.</span>
                                            </div>
                                        </div>
                                    <div class="form-group" >
                                            <label class="col-md-2 control-label" >Immediate Superior :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboImmediateSuperiorNo" DataMember="ESuperior" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        
                                            <label class="col-md-2 control-label" >Payroll group :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboPayClassNo" DataMember="EPayClass" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    <div class="form-group" >
                                            <label class="col-md-2 control-label" >Company name :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboPayLocNo" DataMember="EPayLoc" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        
                                            <label class="col-md-2 control-label">Payroll Type :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboPayTypeNo" DataMember="EPayType" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    <div class="form-group" >
                                            <label class="col-md-2 control-label" >Payment type :</label>
                                            <div class="col-md-3"  >
                                                <asp:DropDownList ID="cboPaymentTypeNo" DataMember="EPaymentType" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        
                                            <label class="col-md-2 control-label" >Rate class :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboEmployeeRateClassNo" DataMember="EEmployeeRateClass" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    <div class="form-group">
                                            <label class="col-md-2 control-label" >Tax code :</label>
                                            <div class="col-md-3" >
                                                <asp:DropDownList ID="cboTaxExemptNo" DataMember="ETaxExempt" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        
                                            <label class="col-md-2 control-label" >Please click here</label>
                                            <div class="col-md-3" >
                                                <asp:CheckBox ID="chkPreparation"  runat="server"></asp:CheckBox><span>&nbsp; if notice preparation is already complete.</span>
                                            </div>
                                        </div>
                                    <div class="form-group">
                                            <label class="col-md-2 control-label">Completion date :</label>
                                            <div class="col-md-3" >
                                                <asp:TextBox ID="txtCompletionDate" runat="server" SkinID="txtdate"  CssClass="form-control"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtCompletionDate" Format="MM/dd/yyyy" />
                                                <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="txtCompletionDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                    ErrorTooltipEnabled="true" ClearTextOnInvalid="true" />
                                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator5" runat="server" ControlExtender="MaskedEditExtender2"
                                                    ControlToValidate="txtCompletionDate" IsValidEmpty="true" EmptyValueMessage="" InvalidValueMessage="Date is invalid"
                                                    ValidationGroup="Demo1" Display="Dynamic" TooltipMessage="" />
                                            </div>
                                       
                                    <div class="form-group" style="display:none;">
                                            <label class="col-md-2 control-label" >Completed by :</label>
                                            <div class="col-md-8" >
                                                <asp:TextBox ID="txtCompletedBy" runat="server" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    
                                            <label class="col-md-2 control-label" >Please click here</label>
                                            <div class="col-md-3" >
                                                <asp:CheckBox ID="txtIsPosting"  runat="server" />&nbsp;<span>to identify that this transaction is ready for posting.</span>
                                            </div>
                                        </div>
                                    <div class="form-group" style="display:none;">
                                            <label class="col-md-2 control-label" id="plblcurrentsalary">Current salary :</label>
                                            <div class="col-md-8">

                                                <asp:TextBox ID="txtCurrentSalary" runat="server" CssClass="number form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    <div class="form-group"  style="visibility:hidden;">
                                            <label class="col-md-2 control-label"> :</label>
                                            <div class="col-md-8" >
                                                <asp:CheckBox ID="txtIsServed"  runat="server" />
                                                <asp:CheckBox ID="txtIsViewSalary" runat="server" />
                                            </div>
                                        </div>
                                    <div class="form-group">
                                            <label class="col-md-2 control-label">Content report :</label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="cboHRANRCNo" DataMember="EHRANRCL" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    <div class="form-group" style="display:none;">
                                            <label class="col-md-2 control-label" >Allowance Template :</label>
                                            <div class="col-md-8" >
                                                <asp:DropDownList ID="cboAllowanceTemplate" DataMember="EAllowTemp" runat="server" ></asp:DropDownList>
                                            </div>
                                        </div> 
                                <div class="form-group">
                                    <label class="col-md-2 control-label "></label>
                                    <div class="col-md-9">
                                        <div class="btn-group">
                                        <asp:Button runat="server" ID="lnkSubmit" CssClass="btn btn-default submit fsd" OnClick="btnSave_Click" Text="submit"></asp:Button>
                                        <asp:Button runat="server" ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="modify" OnClick="lnkModify_Click" UseSubmitBehavior="false"></asp:Button>
                                        <asp:Button runat="server" ID="lnkCancel" CssClass="btn btn-default" CausesValidation="false" Text="<< Back/Cancel" OnClick="lnkCancel_Click" UseSubmitBehavior="false"></asp:Button>
                                    </div>
                            </div>
                        </div> 
                      
                    </div>
                    </fieldset >
                </div>
            </div>
        </div>
    </div>



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
             <div class="form-group" style="display:none;">
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

</asp:content>