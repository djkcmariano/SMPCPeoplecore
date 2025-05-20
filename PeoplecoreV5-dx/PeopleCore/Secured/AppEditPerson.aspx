<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AppEditPerson.aspx.vb" Inherits="Secured_AppEditPerson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <script type="text/javascript">
        $.validator.addMethod("check_date_of_birth", function (value, element) {

            var dateOfBirth = value;
            var arr_date = dateOfBirth.split("/");
            month = arr_date[0];
            day = arr_date[1];
            year = arr_date[2];
            var age = 18;

            var mydate = new Date();
            mydate.setFullYear(year, month - 1, day);

            var getdate = new Date();
            getdate.setFullYear(getdate.getFullYear() - age);

            return getdate >= mydate;

        }, "You must be at least 18 years of age.");

        $.validator.addMethod("check_date_of_birth65", function (value, element) {

            var dateOfBirth = value;
            var arr_date = dateOfBirth.split("/");
            month = arr_date[0];
            day = arr_date[1];
            year = arr_date[2];
            var age = 65;

            var mydate = new Date();
            mydate.setFullYear(year, month - 1, day);

            var getdate = new Date();
            getdate.setFullYear(getdate.getFullYear() - age);

            return getdate < mydate;

        }, "You must be at less than 65 years of age.");

        function SplitH(obj, index) {
            var items = obj.split("|");
            for (i = 0; i < items.length; i++) {
                if (i == index) {
                    return items[i];
                }
            }
        }
    </script>
    <uc:Tab runat="server" ID="Tab">
        <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
                <br />
                <asp:LinkButton runat="server" ID="lnkUpload" Text="Upload Photo" OnClick="lnkUpload_Click" />
            </center>            
            <asp:Label runat="server" ID="lbl" />
            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkSave2" />
                </Triggers>
                <ContentTemplate>                            
                    <asp:Button ID="Button1" runat="server" style="display:none" />
                    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel2" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
                    <asp:Panel id="Panel2" runat="server" CssClass="entryPopup" style="display:none">
                        <fieldset class="form" id="Fieldset1">                    
                            <div class="cf popupheader">
                                <h4>&nbsp;</h4>
                                <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave2" OnClick="lnkSave2_Click" CssClass="fa fa-floppy-o" ToolTip="Save" />
                            </div>                                            
                            <div class="entryPopupDetl form-horizontal">                        
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Filename :</label>
                                    <div class="col-md-7">
                                        <asp:FileUpload runat="server" ID="fuPhoto" Width="350" />
                                    </div>
                                </div>                        
                            </div>
                            <br /><br />                    
                        </fieldset>
                    </asp:Panel> 
                </ContentTemplate>
            </asp:UpdatePanel>
        </Header>
        <Content>
        
        <asp:Panel runat="server" ID="Panel1">        
            <br /><br />            
            <fieldset class="form" id="fsMain">
            <div class="row">
                <ul class="panel-controls">  
                    <li><asp:LinkButton runat="server" ID="lnkSave3" CssClass="control-primary submit fsMain lnkSave3" OnClick="lnkSave_Click"><i class="fa fa-floppy-o"></i>&nbsp;Save</asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" ID="lnkModify2" CssClass="control-primary" OnClick="btnModify_Click"><i class="fa fa-pencil"></i>&nbsp;Modify</asp:LinkButton></li>
                </ul>
            </div>
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Applicant No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtApplicantCode" CssClass="form-control" ReadOnly="true" />                            
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Title :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboCourtesyNo" DataMember="ECourtesy" CssClass="form-control" />                    
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Surname :</label>
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
                    <label class="col-md-3 control-label has-required">Middle Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtMiddleName" CssClass="form-control required" />                            
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Maiden Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtMaidenName" CssClass="form-control" />                            
                    </div>
                    <div style="color:Red;">* <em style="color:Black;">for female married applicant</em></div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Name Extension :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboEmployeeExtNo" DataMember="EEmployeeExt" CssClass="form-control" />
                    </div>
                    <div style="color:Red;">*<em style="color:Black;">(JR.,SR.)</em></div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Nickname :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtNickName" CssClass="form-control" />                            
                    </div>
                </div>
            
                <br />
                <h4><b>PERSONAL DETAILS</b></h4>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Date of Birth :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtBirthDate" CssClass="form-control required check_date_of_birth check_date_of_birth65" AutoPostBack="true" OnTextChanged="txtBirthDate_TextChanged" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" TargetControlID="txtBirthDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtBirthDate" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator1" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtBirthDate" Display="Dynamic" />
                    </div>
                </div>            
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Age :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtBirthAge" CssClass="form-control" ReadOnly="true" />
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
                        <asp:CheckBox runat="server" ID="chkIsDualCitizenship" Text="&nbsp;Tick if applicant has dual citizenship" OnCheckedChanged="chkIsDualCitizenship_CheckedChanged" AutoPostBack="true" />
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
                        <asp:TextBox runat="server" ID="txtHeight" CssClass="form-control number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Weight (kilograms) :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtWeight" CssClass="form-control number" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Blood Type :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboBloodTypeNo" DataMember="EBloodType" CssClass="form-control" />
                    </div>
                </div>                

                <div class="form-group" style="visibility:hidden;position:absolute">
                    <label class="col-md-3 control-label has-space">Shoe Size :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboShoeNo" DataMember="EShoe" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group" style="visibility:hidden;position:absolute">
                    <label class="col-md-3 control-label has-space">T-shirt Size :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboTShirtNo" DataMember="ETShirt" CssClass="form-control" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Religion :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboReligionNo" DataMember="EReligion" CssClass="form-control" />
                    </div>
                </div>

                <br />
                <h4><b>IF MARRIED, PLEASE INDICATE DETAILS</b></h4>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Date of Marriage :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtMarriageDate" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" TargetControlID="txtMarriageDate" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender6" TargetControlID="txtMarriageDate" Mask="99/99/9999" MaskType="Date" />
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
                        <asp:TextBox runat="server" ID="txtSpouseEmployerContactNo" CssClass="form-control" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789-" TargetControlID="txtSpouseEmployerContactNo" />
                    </div>
                </div>
                <br />
                <h4><b>CONTACT INFORMATION</b></h4>

                <div class="form-group" style="visibility:hidden;position:absolute">
                    <label class="col-md-3 control-label has-space">E-mail Address 2 :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtEmail2" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group" style="visibility:hidden;position:absolute">
                    <label class="col-md-3 control-label has-space">URL :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtURL" CssClass="form-control" />
                    </div>
                </div>
                
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Permanent Address :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtHomeHouseNo" CssClass="form-control" placeholder="House/Block/Lot No."/>
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
                    <div class="col-md-3 ">
                        <asp:TextBox runat="server" ID="txtHomeStreet" CssClass="form-control required" placeholder="Street"/>
                    </div>
    <%--                 <div class="col-md-3 pull-right">
                        <asp:LinkButton runat="server" ID="lnkCopy" OnClick="lnkCopy_Click">
                            <i class="fa fa-copy"></i>&nbsp;<em>Same as the above address</em>
                        </asp:LinkButton>
                    </div>  --%>              
                </div>           
                <div class="form-group">
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtHomeSubd" CssClass="form-control" placeholder="Subdivision/Village" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtHomeBarangay" CssClass="form-control" placeholder="Barangay"/>
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
                        <%--<asp:TextBox runat="server" ID="txtPostalCode2" CssClass="form-control" ReadOnly="true" placeholder="Zip Code" />--%>
                        <asp:TextBox runat="server" ID="txtProvinceDesc2" CssClass="form-control" placeholder="Province"  />
                        <asp:HiddenField runat="server" ID="hifProvinceNo2"/>
                    </div>
                </div>
                <div class="form-group" style="display:block;">
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-3">
                        <%--<asp:TextBox runat="server" ID="txtProvinceDesc2" CssClass="form-control" ReadOnly="true" />--%>
                        <asp:TextBox runat="server" ID="txtPostalCode2" CssClass="form-control" placeholder="Zip Code" />
                    </div>
                </div>
                <div class="form-group" style="visibility:hidden;position:absolute">
                    <label class="col-md-3 control-label has-space">Region :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtRegionDesc2" CssClass="form-control" />
                    </div>
                </div>
                <%--<div class="form-group" style="visibility:hidden;position:absolute">
                    <label class="col-md-3 control-label has-space">Zip Code :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtPostalCode2" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>--%>
                <div class="form-group" style=" display:none;">
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtHomePhoneNo" CssClass="form-control" placeholder="Telephone No. " />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="txtHomePhoneNo" Mask="999-999-9999" MaskType="Number" />
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Residential Address :</label>
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
                    <label class="col-md-3 control-label has-space"></label>
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
                                        document.getElementById('<%= txtRegionDesc.ClientID %>').value = "";
                                        document.getElementById('<%= txtPostalCode.ClientID %>').value = "";
                                        document.getElementById('<%= hifProvinceNo.ClientID %>').value = "";
                                    }
                                }

                                function getCity(source, eventArgs) {
                                    document.getElementById('<%= hifCityNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                    document.getElementById('<%= txtProvinceDesc.ClientID %>').value = SplitH(eventArgs.get_value(), 1);
                                    document.getElementById('<%= txtRegionDesc.ClientID %>').value = SplitH(eventArgs.get_value(), 2);
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
                <div class="form-group" style="visibility:hidden;position:absolute">
                    <label class="col-md-3 control-label has-space">Region :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtRegionDesc" CssClass="form-control" />
                    </div>
                </div>
                <%--<div class="form-group">                
                     <label class="col-md-3 control-label has-space">Area Code :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtPostalCode" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>--%>

                <div class="form-group" >
                    <label class="col-md-3 control-label has-space">Telephone No. :</label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtPresentPhoneno" CssClass="form-control" placeholder="Telephone No." />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtPresentPhoneno" Mask="999-999-9999"/>
                    </div>
                </div>       


                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Mobile No. : </label>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control required" />
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtMobileNo" Mask="9999-9999999"/>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">E-mail Address :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control required" />
                    </div>
                </div>

                <br />
                <h4><b>PERSON TO NOTIFY IN CASE OF EMERGENCY</b></h4>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtContactName" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Relationship :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboContactRelationshipNo" DataMember="ERelationship" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Contact No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control number required" />
                        <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="txtContactNo" Mask="9999-9999999" MaskType="Number" />--%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-required">Contact Address :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtContactAddress" CssClass="form-control required" TextMode="MultiLine" Rows="4" />
                    </div>
                </div>
                <div style="display:none">
                <h4><b>OTHER DETAILS</b></h4>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">SSS No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtSSSNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-4 control-label has-space">GSIS No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtGSISNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Philhealth No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtPHNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Pag- Ibig No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtHDMFNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">Tax Code :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboTaxExemptNo" DataMember="ETaxExempt" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-4 control-label has-space">TIN :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtTINNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group" style="visibility:hidden;position:absolute">
                    <label class="col-md-4 control-label has-space">Community Tax Certificate No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtCTCN" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group" style="visibility:hidden;position:absolute">
                    <label class="col-md-4 control-label has-space">Issued at :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtCTCNIssuedAt" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group" style="visibility:hidden;position:absolute">
                    <label class="col-md-4 control-label has-space">Issued on :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtCTCNIssuedOn" CssClass="form-control" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" TargetControlID="txtCTCNIssuedOn" Format="MM/dd/yyyy" />
                        <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender7" TargetControlID="txtCTCNIssuedOn" Mask="99/99/9999" MaskType="Date" />
                        <asp:CompareValidator runat="server" ID="CompareValidator3" Operator="DataTypeCheck" Type="Date" ErrorMessage="Please enter valid date." ControlToValidate="txtCTCNIssuedOn" Display="Dynamic" />
                    </div>
                </div>
                </div>                
                <div class="form-group">
                    <label class="col-md-3 control-label">&nbsp;</label>
                    <div class="col-md-6">
                        <asp:LinkButton runat="server" ID="lnkSave" OnClick="lnkSave_Click" Text="Save" CssClass="btn btn-md btn-primary submit lnkSave" />       
                        <asp:LinkButton runat="server" ID="lnkModify" OnClick="btnModify_Click" Text="Modify" CssClass="btn btn-md btn-primary" CausesValidation="false"  />                      
                    </div>
                </div>
                <br /><br />
            </div>                                                                
            </fieldset>
        </asp:Panel>
        </Content>
    </uc:Tab>    
</asp:Content>

