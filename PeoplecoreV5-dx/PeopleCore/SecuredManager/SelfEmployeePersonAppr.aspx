<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="false" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SelfEmployeePersonAppr.aspx.vb" Inherits="Secured_EmpPersonEdit" %>

<asp:Content id="Content2" contentplaceholderid="cphBody" runat="server">
    <script type="text/javascript">
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
            <asp:LinkButton runat="server" ID="lnkUpload" Text="Upload Photo" OnClick="lnkUpload_Click" Visible="false" />
        </center>            
        <asp:Label runat="server" ID="lbl" />
        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <Triggers>
                <asp:PostBackTrigger ControlID="lnkSave2" />
            </Triggers>
            <ContentTemplate>                            
                <asp:Button ID="Button1" runat="server" style="display:none" />
                <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1" PopupControlID="Panel2" CancelControlID="lnkClose" BackgroundCssClass="modalBackground" />
                <asp:Panel id="Panel2" runat="server" CssClass="entryPopup">
                    <fieldset class="form" id="Fieldset1">                    
                        <div class="cf popupheader">
                            <h4>&nbsp;</h4>
                            <asp:Linkbutton runat="server" ID="lnkClose" CssClass="fa fa-times" ToolTip="Close" />&nbsp;<asp:Linkbutton runat="server" ID="lnkSave2" OnClick="lnkSave2_Click" CssClass="fa fa-floppy-o" ToolTip="Save" />
                        </div>                                            
                        <div class="entryPopupDetl form-horizontal">                        
                            <div class="form-group">
                                <label class="col-md-4 control-label">Filename :</label>
                                <div class="col-md-6">
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
    <%--<Header>
        <center>
            <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
            <br />            
        </center>            
        <asp:Label runat="server" ID="lbl" />        
    </Header>--%>
    <Content>
    <asp:Panel runat="server" ID="Panel1">        
        <br />               
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
                        <asp:TextBox runat="server" ID="txtFPID" CssClass="form-control default-cursor" />
                    </div>
                </div>
                <div class="form-group" style="display:none;">
                    <label class="col-md-3 control-label has-required">Payroll Group :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboPayClassNo" DataMember="EPayClass" CssClass="form-control" />                        
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Salutation :</label>
                    <div class="col-md-6">
                        <asp:DropdownList  ID="cboCourtesyNo" DataMember="ECourtesy" runat="server" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Last Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">First Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Middle Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtMiddleName" CssClass="form-control required" />
                    </div>
                </div>
                <div class="form-group" style="display:none">
                    <label class="col-md-3 control-label has-space">Maiden Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtMaidenName" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Extension :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboEmployeeExtNo" DataMember="EEmployeeExt" CssClass="form-control" />
                    </div>
                </div>     
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Nick Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtNickName" CssClass="form-control" />
                    </div>
                </div>                        
                <div class="form-group">  
                    <h5 class="col-md-8">
                        <label class="control-label">PERSONAL&nbsp;&nbsp;INFORMATION</label>
                    </h5>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Date Of Birth :</label>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtBirthDate" CssClass="form-control required" />
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
                    <label class="col-md-3 control-label has-space">Birth Place :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtBirthPlace" CssClass="form-control" TextMode="MultiLine" Rows="4" />                            
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Nationality :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboCitizenNo" DataMember="ECitizen" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Civil Status :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboCivilStatNo" DataMember="ECivilStat" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Gender :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboGenderNo" DataMember="EGender" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Religion :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboReligionNo" CssClass="form-control" DataMember="EReligion" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Blood Type :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboBloodTypeNo" CssClass="form-control" DataMember="EBloodType" />
                    </div>
                </div>                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Weight (lbs) :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtWeight"  CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Height (ft) :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtHeight"  CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Shoe Size :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboShoeNo" CssClass="form-control" DataMember="EShoe" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">T Shirt size :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboTShirtNo" CssClass="form-control" DataMember="ETShirt" />
                    </div>
                </div>
                <div class="form-group">  
                    <h5 class="col-md-8">
                        <label class="control-label">CONTACT&nbsp;&nbsp;INFORMATION</label>
                    </h5>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Present Address :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtPresentAddress" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space" >City / Municipality :</label>
                    <div class="col-md-6" >
                        <asp:TextBox runat="server" ID="txtCityDesc" CssClass="form-control" onblur="ResetCity()" /> 
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
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Province :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtProvinceDesc" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Region :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtRegionDesc" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Zip Code :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtPostalCode" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Present Phone No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtPresentPhoneNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Mobile No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Email Address :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtEmailAddress" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">URL :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtURL" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Provincial Address :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtHomeAddress" CssClass="form-control" TextMode="Multiline" Rows="4" />
                    </div>
                </div>                
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">City / Municipality :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtCityHomeDesc" CssClass="form-control" onblur="ResetCityHome()" /> 
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
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Province :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtProvinceDesc2" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Region :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtRegionDesc2" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Zip Code :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtPostalCode2" CssClass="form-control" ReadOnly="true" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Provincial Phone No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtHomePhoneNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">  
                    <h5 class="col-md-8">
                        <label class="control-label">IF MARRIED,&nbsp;&nbsp;MARRIED DETAILS</label>
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
                        <label class="control-label">PERSON TO NOTIFY INCASE OF EMERGENCY</label>
                    </h5>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Name :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtContactName" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Relationship :</label>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="cboContactRelationshipNo" CssClass="form-control" DataMember="ERelationship" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Contact No. :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 control-label has-space">Address :</label>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" ID="txtContactAddress" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                    </div>
                </div>                                        
                <div class="form-group" style="display:none;">
                    <label class="col-md-3 control-label has-space"></label>
                    <div class="col-md-6">            
                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button runat="server" ID="btnModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="btnModify_Click" />                            
                    </div>
                </div>
                <br /><br />                     
            </div>                                                
        </fieldset>
    </asp:Panel>
    </Content>
</uc:Tab>
  
</asp:Content>
