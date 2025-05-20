<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SelfEmployeeDetail_Old.aspx.vb" Inherits="SecuredSelf_SelfEmployeeDetail_Old" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">



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
                <div class="form-group">  
                    <h5 class="col-md-8">
                        <label class="control-label">PRIMARY&nbsp;&nbsp;INFORMATION</label>
                    </h5>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Employee No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtEmployeeCode" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Card ID :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtBarCode" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">FP ID :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtFPId" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group" style="visibility:hidden; position:absolute;">
                    <label class="col-md-3 control-label has-space">&nbsp;</label>
                    <div class="col-md-6">
                        <asp:CheckBox runat="server" ID="chkIsFPSupervisor" Text="&nbsp;Finger Pass Supervisor" Enabled="false" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Title :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboCourtesyNo" DataMember="ECourtesy" CssClass="form-control required" />
                    </div>
                    
                </div>                
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Last Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">First Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Middle Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtMiddleName" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Maiden Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtMaidenName" CssClass="form-control" Enabled="True" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Suffix :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboEmployeeExtNo" DataMember="EEmployeeExt" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Nick Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtNickName" CssClass="form-control required" />
                    </div>
                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Immediate Supervisor :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="SFullname" CssClass="form-control" Enabled="false" ReadOnly="True" />
                    </div>
                    
                </div>                
                <div class="form-group">  
                    <h5 class="col-md-8">
                        <label class="control-label">PERSONAL&nbsp;&nbsp;INFORMATION</label>
                    </h5>
                </div>
                <div class="form-group">
                        <label class="col-md-3 control-label has-required">Date of Birth :</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtBirthDate" CssClass="form-control required" OnTextChanged="txtBirthDate_TextChanged" AutoPostBack="true"/>
                            <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtBirthDate" Format="MM/dd/yyyy"  />
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtBirthDate" Mask="99/99/9999" MaskType="Date" />
                            <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtBirthDate" Display="Dynamic" />
                        </div>
                        
                    </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Age :</label>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtBirthAge" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Place of Birth :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtBirthPlace" CssClass="form-control required" TextMode="MultiLine" Rows="4" />
                    </div>
                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Nationality :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboCitizenNo" CssClass="form-control required" DataMember="ECitizen" />
                    </div>
                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Civil Status :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboCivilStatNo" CssClass="form-control required" DataMember="ECivilStat" />
                    </div>
                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Gender :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboGenderNo" CssClass="form-control required" DataMember="EGender" />
                    </div>
                    
                </div>     
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Religion :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboReligionNo" CssClass="form-control required" DataMember="EReligion" />
                    </div>
                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Blood Type :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboBloodTypeNo" CssClass="form-control" DataMember="EBloodType" />
                    </div>
                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Height (ft) :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtHeight" CssClass="form-control number" />
                    </div>
                    
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Weight (pound) :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtWeight" CssClass="form-control number" />
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
                        <label class="control-label">MARRIAGE DETAILS (IF MARRIED)</label>
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


                <div class="form-group">  
                        <h5 class="col-md-8">
                            <label class="control-label">CONTACT&nbsp;&nbsp;INFORMATION</label>
                        </h5>
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
                    </div>
                    
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">URL :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtURL" CssClass="form-control" />
                        </div>
                        
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Present Address :</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtHouseNo" CssClass="form-control required" placeholder="House No."/>
                            <asp:TextBox runat="server" ID="txtPresentAddress" CssClass="form-control" Visible="false"/>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtStreet" CssClass="form-control required" placeholder="Street"/>
                        </div>
                        
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtBarangay" CssClass="form-control required" placeholder="Barangay / Subdivision"/>
                        </div>
                    </div>            
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space" ></label>
                        <div class="col-md-6" >
                            <asp:TextBox runat="server" ID="txtCityDesc" CssClass="form-control required" onblur="ResetCity()" placeholder="City / Municipality" /> 
                            <asp:HiddenField runat="server" ID="hifCityNo"/>
                            <ajaxToolkit:AutoCompleteExtender ID="aceCity" runat="server"  
                                TargetControlID="txtCityDesc" MinimumPrefixLength="1"
                                CompletionInterval="500" ServiceMethod="PopulateCity" 
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                OnClientItemSelected="getCity" FirstRowSelected="true" UseContextKey="true" />
                                <script type="text/javascript">

                                    function SplitH(obj, index) {
                                        var items = obj.split("|");
                                        for (i = 0; i < items.length; i++) {
                                            if (i == index) {
                                                return items[i];
                                            }
                                        }
                                    }

                                    function ResetCity() {
                                        if (document.getElementById('<%= txtCityDesc.ClientID %>').value == "") {
                                            document.getElementById('<%= hifCityNo.ClientID %>').value = "0";
                                            document.getElementById('<%= txtProvinceDesc.ClientID %>').value = "";
                                            document.getElementById('<%= txtRegionDesc.ClientID %>').value = "";
                                            document.getElementById('<%= txtPostalCode.ClientID %>').value = "";
                                        }
                                    }

                                    function getCity(source, eventArgs) {
                                        document.getElementById('<%= hifCityNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                        document.getElementById('<%= txtProvinceDesc.ClientID %>').value = SplitH(eventArgs.get_value(), 1);
                                        document.getElementById('<%= txtRegionDesc.ClientID %>').value = SplitH(eventArgs.get_value(), 2);
                                        document.getElementById('<%= txtPostalCode.ClientID %>').value = SplitH(eventArgs.get_value(), 3);
                                    }                               	

                                </script>
                        </div>
                    </div>
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">Province :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtProvinceDesc" CssClass="form-control" ReadOnly="true" />
                        </div>
                    </div>
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">Region :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtRegionDesc" CssClass="form-control" ReadOnly="true" />
                        </div>
                    </div>
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">ZIP Code :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtPostalCode" CssClass="form-control" ReadOnly="true" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Present Contact No. :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtPresentPhoneno" CssClass="form-control" />
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender4" TargetControlID="txtPresentPhoneno" Mask="999-9999" />
                        </div>
                        
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Permanent / Prov. Address :</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtHomeHouseNo" CssClass="form-control required" placeholder="House No."/>
                            <asp:TextBox runat="server" ID="txtHomeAddress" CssClass="form-control" Visible="false"/>
                            <%--<ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"  
                                TargetControlID="txtHomeAddress" OnClientPopulated="HomeAddress" />
                            <script type="text/javascript">
                                function HomeAddress(source, eventArgs) {
                                    document.getElementById('<%= txtHomeHouseNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                    document.getElementById('<%= txtHomeStreet.ClientID %>').value = SplitH(eventArgs.get_value(), 1);
                                    document.getElementById('<%= txtHomeBarangay.ClientID %>').value = SplitH(eventArgs.get_value(), 2);
                                }                               	

                                </script>--%>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtHomeStreet" CssClass="form-control required" placeholder="Street"/>
                        </div>
                        <div class="col-md-3">
                            <asp:LinkButton runat="server" ID="lnkCopy" OnClick="lnkCopy_Click">
                                <i class="fa fa-copy"></i>&nbsp;<em>Same as the above address</em>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtHomeBarangay" CssClass="form-control required" placeholder="Barangay / Subdivision"/>
                        </div>
                    </div>            
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">
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
                                            document.getElementById('<%= hifCityHomeNo.ClientID %>').value = "0";
                                            document.getElementById('<%= txtProvinceDesc2.ClientID %>').value = "";
                                            document.getElementById('<%= txtRegionDesc2.ClientID %>').value = "";
                                            document.getElementById('<%= txtPostalCode2.ClientID %>').value = "";
                                        }
                                    }

                                    function getCityHome(source, eventArgs) {
                                        document.getElementById('<%= hifCityHomeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                        document.getElementById('<%= txtProvinceDesc2.ClientID %>').value = SplitH(eventArgs.get_value(), 1);
                                        document.getElementById('<%= txtRegionDesc2.ClientID %>').value = SplitH(eventArgs.get_value(), 2);
                                        document.getElementById('<%= txtPostalCode2.ClientID %>').value = SplitH(eventArgs.get_value(), 3);
                                    }                               	

                                </script>
                        </div>
                    </div>
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">Province :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtProvinceDesc2" CssClass="form-control" ReadOnly="true" />
                        </div>
                    </div>
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">Region :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtRegionDesc2" CssClass="form-control" ReadOnly="true" />
                        </div>
                    </div>
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">Zip Code :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtPostalCode2" CssClass="form-control" ReadOnly="true" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Permanent / Prov. Phone No. :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtHomePhoneNo" CssClass="form-control" placeholder="Tel. / Phone No." />
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender6" TargetControlID="txtHomePhoneNo" Mask="999-9999" />
                        </div>
                        
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
                            <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control required" />
                            <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender5" TargetControlID="txtContactNo" Mask="9999-999-9999" />
                        </div>
                        
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Address :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtContactAddress" CssClass="form-control required" TextMode="MultiLine" Rows="4" />
                        </div>
                        
                    </div>
                


                <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="btnModify_Click" />                            
                        </div>
                    </div>
                <br /><br />                     
            </div>                                                
        </fieldset>
    </asp:Panel>
    </Content>
</uc:Tab>    
</asp:Content>

