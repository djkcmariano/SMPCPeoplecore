<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfEmployeeDetail.aspx.vb" Inherits="SecuredSelf_SelfEmployeeDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

<script type="text/javascript">
        function SplitH(obj, index) {
            var items = obj.split("|");
            for (i = 0; i < items.length; i++) {
                if (i == index) {
                    return items[i];
                }
            }
        }


        jQuery.validator.addMethod("notNumbers", function (value, element) {
            var reg = /[0-9]/;
            if (reg.test(value)) {
                return false;
            } else {
                return true;
            }
        }, "Numbers not allowed"); 
</script>

<uc:TabSelf runat="server" ID="TabSelf">   
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
                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">PRIMARY&nbsp;&nbsp;INFORMATION</label>
                        </h5>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Employee No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtEmployeeCode" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Card No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtBarCode" CssClass="form-control" />
                        </div>
                    </div>                    
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">FP ID :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtFPID" CssClass="form-control" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtFPID" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Payroll Group :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboPayClassNo" DataMember="EPayClass" CssClass="form-control required" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Title :</label>
                        <div class="col-md-6">
                            <asp:DropdownList  ID="cboCourtesyNo" DataMember="ECourtesy" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Surname :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control required notNumbers" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">First Name :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control required notNumbers" />
                        </div>
                    </div>                    
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Middle Name :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtMiddleName" CssClass="form-control notNumbers" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Maiden Name :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtMaidenName" CssClass="form-control" />
                        </div>
                        <div style="color:Red;">* <em style="color:Black;">for married women</em></div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Name Extension :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboEmployeeExtNo" DataMember="EEmployeeExt" CssClass="form-control" />
                        </div>
                        <div style="color:Red;">* <em style="color:Black;">(Jr., II, ect.)</em></div>
                    </div>                         
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Nick Name :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtNickName" CssClass="form-control" />
                        </div>
                    </div>              

                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">PERSONAL&nbsp;&nbsp;DETAILS</label>
                        </h5>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Date of Birth :</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtBirthDate" CssClass="form-control required" OnTextChanged="txtBirthDate_TextChanged" AutoPostBack="true" />
                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtBirthDate" Format="MM/dd/yyyy" />
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtBirthDate" Mask="99/99/9999" MaskType="Date" />
                            <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtBirthDate" Display="Dynamic" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Age :</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtBirthAge" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Place of Birth :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtBirthPlace" CssClass="form-control" TextMode="MultiLine" Rows="4" />                            
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Sex :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboGenderNo" DataMember="EGender" CssClass="form-control required" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Citizenship :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboCitizenNo" DataMember="ECitizen" CssClass="form-control required" />
                        </div>
                    </div>                    
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-3">
                            <asp:CheckBox runat="server" ID="chkIsDualCitizenship" Text="&nbsp;With dual citizenship" OnCheckedChanged="chkIsDualCitizenship_CheckedChanged" AutoPostBack="true" />
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList runat="server" ID="cboDualCitizenshipTypeNo" CssClass="form-control" Enabled="false">
                                <asp:ListItem Value="" Text="-- Select --" />
                                <asp:ListItem Value="1" Text="By birth" />
                                <asp:ListItem Value="2" Text="By naturalization" />
                            </asp:DropDownList>
                        </div>
                    </div>                    
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtDualCitizenshipCountry" CssClass="form-control" placeholder="Please indicate country" Enabled="false" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Civil Status :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboCivilStatNo" DataMember="ECivilStat" CssClass="form-control required" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Height (meters) :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtHeight"  CssClass="form-control number" />
                        </div>
                    </div>           
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Weight (kilograms) :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtWeight"  CssClass="form-control number" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Blood Type :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboBloodTypeNo" CssClass="form-control" DataMember="EBloodType" />
                        </div>
                    </div>                     
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Religion :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboReligionNo" CssClass="form-control" DataMember="EReligion" />
                        </div>
                    </div>                                            
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Shoe Size :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboShoeNo" CssClass="form-control" DataMember="EShoe" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">T-shirt Size :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboTShirtNo" CssClass="form-control" DataMember="ETShirt" />
                        </div>
                    </div>


                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">IF MARRIED, PLEASE INDICATE DETAILS</label>
                        </h5>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Date of Marriage :</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtMarriageDate" CssClass="form-control" />
                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtMarriageDate" Format="MM/dd/yyyy" />
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender2" TargetControlID="txtMarriageDate" Mask="99/99/9999" MaskType="Date" />
                            <asp:CompareValidator runat="server" ID="CompareValidator2" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtMarriageDate" Display="Dynamic" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Place of Marriage :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtMarriagePlace" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                        </div>
                    </div>

                    <h4><b>SPOUSE DETAILS</b></h4>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Surname :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtSpouseLastName" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">First Name :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtSpouseFirstName" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Middle Name :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtSpouseMiddleName" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Name Extension (JR., SR) :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboSpouseEmployeeExtNo" DataMember="EEmployeeExt" CssClass="form-control" />
                        </div>
                        <div style="color:Red;"><em style="color:Black;"></em></div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Maiden Name :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtSpouseMaidenName" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Occupation :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtSpouseOccupation" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Employer/Business Name :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtSpouseEmployerName" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Business Address :</label>
                        <div class="col-md-6">                    
                            <asp:TextBox runat="server" ID="txtSpouseEmployerAddress" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Telephone No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtSpouseEmployerContactNo" CssClass="form-control number" />
                            <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789-" TargetControlID="txtSpouseEmployerContactNo" />--%>
                        </div>
                    </div>
                    <br />


                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">CONTACT&nbsp;&nbsp;INFORMATION</label>
                        </h5>
                    </div>                    
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">URL :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtURL" CssClass="form-control" />
                        </div>
                    </div>
                    
                    <div class="form-group">
                    <label class="col-md-3 control-label has-required">Permanent Address :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtHomeHouseNo" CssClass="form-control " placeholder="House/Block/Lot No."/>
                        <asp:TextBox runat="server" ID="txtHomeAddress" CssClass="form-control" Visible="false"/>                       
                    </div>
                    <div class="col-md-3 ">
                        <asp:TextBox runat="server" ID="txtHomeStreet" CssClass="form-control required" placeholder="Street"/>
                    </div>   
                </div>           
                <div class="form-group">
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtHomeSubd" CssClass="form-control" placeholder="Subdivision/Village" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtHomeBarangay" CssClass="form-control " placeholder="Barangay"/>
                    </div>                
                </div>            
                <div class="form-group">
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtCityHomeDesc" CssClass="form-control required" onblur="ResetCityHome()" placeholder="City / Municipality" /> 
                        <asp:HiddenField runat="server" ID="hifCityHomeNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                            TargetControlID="txtCityHomeDesc" MinimumPrefixLength="1"
                            CompletionInterval="500" ServiceMethod="PopulateCity" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListItemCssClass="autocomplete_listItem" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            OnClientItemSelected="getCityHome" FirstRowSelected="true" UseContextKey="true" />
                            <script type="text/javascript">

                                function ResetCityHome() {
                                    if (document.getElementById('<%= txtCityHomeDesc.ClientID %>').value == "") {
                                        document.getElementById('<%= txtProvinceDesc2.ClientID %>').value = "";
                                        document.getElementById('<%= txtRegionDesc2.ClientID %>').value = "";
                                        document.getElementById('<%= txtPostalCode2.ClientID %>').value = "";
                                        document.getElementById('<%= hifProvinceNo2.ClientID %>').value = "";
                                    }
                                }

                                function getCityHome(source, eventArgs) {
                                    document.getElementById('<%= hifCityHomeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                    document.getElementById('<%= txtProvinceDesc2.ClientID %>').value = SplitH(eventArgs.get_value(), 1);
                                    document.getElementById('<%= txtRegionDesc2.ClientID %>').value = SplitH(eventArgs.get_value(), 2);
                                    document.getElementById('<%= txtPostalCode2.ClientID %>').value = SplitH(eventArgs.get_value(), 3);
                                    document.getElementById('<%= hifProvinceNo2.ClientID %>').value = SplitH(eventArgs.get_value(), 4);
                                }                               	

                            </script>
                    </div>
                    <div class="col-md-3">                                           
                        <asp:TextBox runat="server" ID="txtProvinceDesc2" CssClass="form-control" placeholder="Province"  />
                        <asp:HiddenField runat="server" ID="hifProvinceNo2"/>
                    </div>
                </div>
                <div class="form-group" style="display:block;">
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-3">                        
                        <asp:TextBox runat="server" ID="txtPostalCode2" CssClass="form-control" placeholder="Zip Code" />
                    </div>
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-3 control-label has-space">Region :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control" />
                    </div>
                </div>               
                <div class="form-group" style=" display:none;">
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="TextBox3" CssClass="form-control number"  placeholder="Telephone No. " />
                        <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtHomePhoneNo" Mask="999-999-9999" MaskType="Number" />--%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Current Address :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtHouseNo" CssClass="form-control" placeholder="House/Block/Lot No." />
                        <asp:TextBox runat="server" ID="txtPresentAddress" CssClass="form-control" Visible="false"/>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtStreet" CssClass="form-control" placeholder="Street"/>
                    </div>
                     <div class="col-md-3 pull-right">
                        <asp:LinkButton runat="server" ID="lnkCopy" OnClick="lnkCopy_Click" CausesValidation="false">
                            <i class="fa fa-copy"></i>&nbsp;<em>Same as the above address</em>
                        </asp:LinkButton>
                    </div>  
                </div>
                <div class="form-group">
                    <label class="col-md-3 c ontrol-label has-space"></label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtSubd" CssClass="form-control" placeholder="Subdivision/Village"/>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtBarangay" CssClass="form-control" placeholder="Barangay"/>
                    </div>
                </div>            
                <div class="form-group">
                    <label class="col-md-3 control-label has-space" ></label>
                    <div class="col-md-3" >
                        <asp:TextBox runat="server" ID="txtCityDesc" CssClass="form-control" onblur="ResetCity()" placeholder="City / Municipality" /> 
                        <asp:HiddenField runat="server" ID="hifCityNo"/>
                        <ajaxToolkit:AutoCompleteExtender ID="aceCity" runat="server"  
                            TargetControlID="txtCityDesc" MinimumPrefixLength="1"
                            CompletionInterval="500" ServiceMethod="PopulateCity" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListItemCssClass="autocomplete_listItem" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            OnClientItemSelected="getCity" FirstRowSelected="true" UseContextKey="true" />
                            <script type="text/javascript">

                                function ResetCity() {
                                    if (document.getElementById('<%= txtCityDesc.ClientID %>').value == "") {
                                        document.getElementById('<%= txtProvinceDesc.ClientID %>').value = "";
                                        document.getElementById('<%= txtPostalCode.ClientID %>').value = "";
                                        document.getElementById('<%= hifProvinceNo.ClientID %>').value = "";
                                    }
                                }

                                function getCity(source, eventArgs) {
                                    document.getElementById('<%= hifCityNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                    document.getElementById('<%= txtProvinceDesc.ClientID %>').value = SplitH(eventArgs.get_value(), 1);
                                    document.getElementById('<%= txtPostalCode.ClientID %>').value = SplitH(eventArgs.get_value(), 3);
                                    document.getElementById('<%= hifProvinceNo.ClientID %>').value = SplitH(eventArgs.get_value(), 4);
                                }                               	

                            </script>
                    </div>
                    <div class="col-md-3">
                        <%--<asp:TextBox runat="server" ID="txtPostalCode" CssClass="form-control" ReadOnly="true" placeholder="Zip Code" />--%>
                        <asp:TextBox runat="server" ID="txtProvinceDesc" CssClass="form-control" placeholder="Province" />
                        <asp:HiddenField runat="server" ID="hifProvinceNo"/>
                    </div>
                </div>
                <div class="form-group" style="display:block;">
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-3">
                        <%--<asp:TextBox runat="server" ID="txtProvinceDesc" CssClass="form-control" ReadOnly="true" />--%>
                        <asp:TextBox runat="server" ID="txtPostalCode" CssClass="form-control" placeholder="Zip Code" />
                    </div>
                </div>
                    
                                        
                <div class="form-group" style="display:none;">
                    <label class="col-md-3 control-label has-space">Region :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtRegionDesc2" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Telephone No. :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtHomePhoneNo" CssClass="form-control" placeholder="Tel. / Phone No." />
                        <%--<ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender7" TargetControlID="txtHomePhoneNo" Mask="999-9999" />--%>
                    </div>
                </div>
                <div class="form-group">
                        <label class="col-md-3 control-label has-required">Mobile No. :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control required" />                            
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender3" TargetControlID="txtMobileNo" Mask="9999-999-9999" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Email Address :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtEmailAddress" CssClass="form-control" />
                        </div>
                        <%--<div style="color:Red;">* <em style="color:Black;">Personal</em></div>--%>
                    </div>                

                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">PERSON TO NOTIFY IN CASE OF EMERGENCY</label>
                        </h5>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Name :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtContactName" CssClass="form-control required" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Relationship :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboContactRelationshipNo" CssClass="form-control required" DataMember="ERelationship" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Contact No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control number required" />
                            <%--<ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender5" TargetControlID="txtContactNo" Mask="999-9999" />--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Address :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtContactAddress" CssClass="form-control required" TextMode="MultiLine" Rows="4" />
                        </div>
                    </div>
                    <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">COMPANY CONTACT INFORMATION</label>
                        </h5>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Mobile No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtCompanyMobileNo" CssClass="form-control" />                            
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender11" TargetControlID="txtCompanyMobileNo" Mask="9999-999-9999" />
                        </div>
                    </div>                     
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Direct No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtCompanyTelNo" CssClass="form-control number" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Fax No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtFaxNo" CssClass="form-control number" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Local No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtLocalNo" CssClass="form-control number" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Email Address :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtCompanyEmail" CssClass="form-control" />
                        </div>
                    </div>                                         
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">                                      
                            <asp:LinkButton runat="server" ID="lnkSave" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-default submit lnkSave" />       
                            <asp:LinkButton runat="server" ID="lnkModify" OnClick="btnModify_Click" Text="Modify" CssClass="btn btn-default " CausesValidation="false"  />                      
                        </div>
                    </div>
                <br /><br />                     
            </div> 
            
            
                                                            
        </fieldset>
    </asp:Panel>
    </Content>
</uc:TabSelf>    
</asp:Content>

