<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpBUDS_Edit.aspx.vb" Inherits="Secured_EmpBUDS_Edit" %>

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


        jQuery.validator.addMethod("notNumbers", function (value, element) {
            var reg = /[0-9]/;
            if (reg.test(value)) {
                return false;
            } else {
                return true;
            }
        }, "Numbers not allowed"); 



    </script>
 <uc:Tab runat="server" ID="Tab">
    <Header>
        <center>
            <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
            <br />
            
        </center>            
        
    </Header>
    <%--<Header>
        <center>
            <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
            <br />            
        </center>            
        <asp:Label runat="server" ID="lbl" />        
    </Header>--%>
    <Content>
    <br />    
    <asp:Panel runat="server" ID="Panel1">                               
        <fieldset class="form" id="fsMain">
                        <%--<div class="row">
                            <ul class="panel-controls">  
                                <li><asp:LinkButton runat="server" ID="lnkSave3" CssClass="control-primary submit lnkSave3" OnClick="btnSave_Click"><i class="fa fa-floppy-o"></i>&nbsp;Save</asp:LinkButton></li>
                                <li><asp:LinkButton runat="server" ID="lnkModify2" CssClass="control-primary" OnClick="btnModify_Click"><i class="fa fa-pencil"></i>&nbsp;Modify</asp:LinkButton></li>
                            </ul>
                        </div>  --%>          
                <div  class="form-horizontal">
                        <div class="form-group">  
                            <h5 class="col-md-8">
                                <label class="control-label">PRIMARY&nbsp;&nbsp;INFORMATION</label>
                            </h5>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Employee No. :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtEmployeeCode" ReadOnly="true" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">Surname :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtLastName" ReadOnly="true" CssClass="form-control required notNumbers" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">First Name :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtFirstName" ReadOnly="true" CssClass="form-control required notNumbers" />
                            </div>
                        </div>                    
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Middle Name :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtMiddleName" ReadOnly="true" CssClass="form-control notNumbers" />
                            </div>
                        </div>
                                            
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Nick Name :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtNickName" ReadOnly="true" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Department :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtDepartment" ReadOnly="true" CssClass="form-control" />
                            </div>
                        </div>              
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Position :</label>
                            <div class="col-md-6">
                                <asp:TextBox runat="server" ID="txtPosition" ReadOnly="true" CssClass="form-control" />
                            </div>
                        </div> 

                        <div class="form-group">  
                            <h5 class="col-md-8">
                                <label class="control-label">CONTACT&nbsp;&nbsp;INFORMATION</label>
                            </h5>
                        </div>                    
                    
                    
                        <div class="form-group">
                        <label class="col-md-3 control-label has-space">Residential Address 1:</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtHomeHouseNo" ReadOnly="true" CssClass="form-control " placeholder="House/Block/Lot No."/>
                            <asp:TextBox runat="server" ID="txtHomeAddress" CssClass="form-control "  Visible="false"/>
                        </div>
                        <div class="col-md-3 ">
                            <asp:TextBox runat="server" ID="txtHomeStreet" ReadOnly="true" CssClass="form-control " placeholder="Street"/>
                        </div>   
                    </div>           
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtHomeSubd" ReadOnly="true" CssClass="form-control" placeholder="Subdivision/Village" />
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtHomeBarangay" ReadOnly="true" CssClass="form-control " placeholder="Barangay"/>
                        </div>                
                    </div>            
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtCityHomeDesc" ReadOnly="true" CssClass="form-control " onblur="ResetCityHome()" placeholder="City / Municipality" /> 
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
                                            document.getElementById('<%= txtPostalCode2.ClientID %>').value = "";
                                            document.getElementById('<%= hifProvinceNo2.ClientID %>').value = "";
                                        }
                                    }
                                    function getCityHome(source, eventArgs) {
                                        document.getElementById('<%= hifCityHomeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                        document.getElementById('<%= txtProvinceDesc2.ClientID %>').value = SplitH(eventArgs.get_value(), 1);
                                        document.getElementById('<%= txtPostalCode2.ClientID %>').value = SplitH(eventArgs.get_value(), 3);
                                        document.getElementById('<%= hifProvinceNo2.ClientID %>').value = SplitH(eventArgs.get_value(), 4);
                                    }                               	
                                </script>
                        </div>
                        <div class="col-md-3">                                           
                            <asp:TextBox runat="server" ID="txtProvinceDesc2" ReadOnly="true" CssClass="form-control" placeholder="Province"  />
                            <asp:HiddenField runat="server" ID="hifProvinceNo2"/>
                        </div>
                    </div>
                    <div class="form-group" style="display:block;">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-3">                        
                            <asp:TextBox runat="server" ID="txtPostalCode2" ReadOnly="true" CssClass="form-control" placeholder="Zip Code" />
                        </div>
                    </div>
               
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Residential Address 2:</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtHouseNo" ReadOnly="true" CssClass="form-control" placeholder="House/Block/Lot No." />
                            <asp:TextBox runat="server" ID="txtPresentAddress" CssClass="form-control" Visible="false"/>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtStreet" ReadOnly="true" CssClass="form-control" placeholder="Street"/>
                        </div>
                            <div class="col-md-3 pull-right">
                            <asp:LinkButton runat="server" ID="lnkCopy" ReadOnly="true" OnClick="lnkCopy_Click" CausesValidation="false">
                               <%-- <i class="fa fa-copy"></i>&nbsp;<em>Same as the above address</em>--%>
                            </asp:LinkButton>
                        </div>  
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 c ontrol-label has-space"></label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtSubd" ReadOnly="true" CssClass="form-control" placeholder="Subdivision/Village"/>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtBarangay" ReadOnly="true" CssClass="form-control" placeholder="Barangay"/>
                        </div>
                    </div>            
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space" ></label>
                        <div class="col-md-3" >
                            <asp:TextBox runat="server" ID="txtCityDesc" ReadOnly="true" CssClass="form-control" onblur="ResetCity()" placeholder="City / Municipality" /> 
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
                            <asp:TextBox runat="server" ID="txtProvinceDesc" ReadOnly="true" CssClass="form-control" placeholder="Province" />
                            <asp:HiddenField runat="server" ID="hifProvinceNo"/>
                        </div>
                    </div>
                    <div class="form-group" style="display:block;">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-3">
                            <%--<asp:TextBox runat="server" ID="txtProvinceDesc" CssClass="form-control" ReadOnly="true" />--%>
                            <asp:TextBox runat="server" ID="txtPostalCode" ReadOnly="true" CssClass="form-control" placeholder="Zip Code" />
                        </div>
                    </div>
                          
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Home No. 1 :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtHomePhoneNo" CssClass="form-control"/>
                            <%--<ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender7" TargetControlID="txtHomePhoneNo"/>--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Home No. 2 :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtHomePhoneNo2" CssClass="form-control"/>
                            <%--<ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtHomePhoneNo2"/>--%>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Mobile No. 1 :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control " />                            
                           <%-- <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender3" TargetControlID="txtMobileNo"/>--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Mobile No. 2 :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtMobileNo2" CssClass="form-control" />                            
                            <%--<ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender6" TargetControlID="txtMobileNo2" />--%>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Direct Line 1 :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtDirectLine1" CssClass="form-control " />                            
                            <%--<ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender8" TargetControlID="txtDirectLine1"/>--%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Direct Line 2 :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtDirectLine2" CssClass="form-control" />                            
                            <%--<ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender9" TargetControlID="txtDirectLine2" />--%>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Fax No. :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtFaxNo" CssClass="form-control" />                            
                            <%--<ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender10" TargetControlID="txtFaxNo"/>--%>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Local No. 1 :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtLocalNo" CssClass="form-control" />                            
                            <%--<ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender11" TargetControlID="txLocalNo"/>--%>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Local No. 2 :</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtLocalNo2" CssClass="form-control" />                            
                            <%--<ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender12" TargetControlID="txtLocalNo2" />--%>
                        </div>
                    </div>


                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Personal Email Address :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group" style="display:none">
                        <label class="col-md-3 control-label has-space">Company Email Address :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtCompanyEmail" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">GROUPING :</label>
                        <div class="col-md-6">
                        
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Disaster Bridge :</label>
                        <div class="col-md-6">
                            <asp:DropdownList ID="cboDisasterBrigadeTeamNo" runat="server" CssClass="form-control" DataMember="EEmployee" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Disaster Control Team :</label>
                        <div class="col-md-6">
                            <asp:DropdownList ID="cboDisasterControlTeamNo" runat="server" CssClass="form-control" DataMember="EEmployee" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Evaluation and Rescue Team :</label>
                        <div class="col-md-6">
                            <asp:DropdownList ID="cboEvacuationTeamNo" runat="server" CssClass="form-control" DataMember="EEmployee" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">First Aid Team :</label>
                        <div class="col-md-6">
                            <asp:DropdownList ID="cboFirstAidTeamNo" runat="server" CssClass="form-control" DataMember="EEmployee" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Business Continuity and Mgmt. Team :</label>
                        <div class="col-md-6">
                            <asp:DropdownList ID="cboBusinessTeamNo" runat="server" CssClass="form-control" DataMember="EEmployee" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">CALL TREE DETAILS :</label>
                        <div class="col-md-6">
                        
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Call Tree Number :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtCallTreeNumber" CssClass="form-control" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Call Tree Number :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Principal :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtPrincipal" TextMode="MultiLine" Rows="4" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Alternate :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtAlternate" TextMode="MultiLine" Rows="4" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">KEY PERSONNEL :</label>
                        <div class="col-md-6">
                        
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Alternate :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtAlternateKeyPerson" CssClass="form-control" />
                        </div>
                    </div>
                                 
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:LinkButton runat="server" ID="lnkSave" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-default submit lnkSave" />       
                            <asp:LinkButton runat="server" ID="lnkModify" OnClick="btnModify_Click" Text="Modify" CssClass="btn btn-default " CausesValidation="false"  />                      
                        </div>
                    </div>
                    <br />                    
                </div>                                                
            </fieldset>
    </asp:Panel>
    </Content>
</uc:Tab>
  
</asp:Content>
