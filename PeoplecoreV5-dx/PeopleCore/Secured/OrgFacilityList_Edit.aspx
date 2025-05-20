<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="OrgFacilityList_Edit.aspx.vb" Inherits="Secured_OrgPayLocEdit" %>


<asp:content id="cntNo" contentplaceholderid="cphBody" runat="server">
<asp:UpdatePanel runat="server" ID="UpdatePanelFU">
<ContentTemplate>
<asp:Panel runat="server" ID="Panel1">        
<div class="page-content-wrap">
    <div class="row">
        <div class="panel panel-default">
            <br />
            <div class="panel-body">
                <div class="row">
                    <fieldset class="form" id="fsMain">
                    <div  class="form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label has-space">Reference No. :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtFacilityNo" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Reference No. :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Code :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtFacilityCode" CssClass="form-control required" />
                            </div>
                        </div>        
                        <div class="form-group">
                            <label class="col-md-4 control-label has-required">Description :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtFacilityDesc" CssClass="form-control required" />
                            </div>
                        </div>     
                        
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label has-space">SBU :</label>
                            <div class="col-md-5">
                                <asp:Dropdownlist ID="cboFacilityGroupNo" DataMember="EFacilityGroup" runat="server" CssClass="form-control" />
                            </div>
                        </div>
  
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Account Code :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtFacilityACode" CssClass="form-control" />
                            </div>
                        </div>    
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">Head :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" onblur="ResetHead()" Placeholder="Type here..." /> 
                                <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                                <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                                TargetControlID="txtFullName" MinimumPrefixLength="2" 
                                CompletionInterval="250" ServiceMethod="PopulateManager" 
                                CompletionListCssClass="autocomplete_completionListElement" 
                                CompletionListItemCssClass="autocomplete_listItem" 
                                CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" CompletionSetCount="1"
                                OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                 <script type="text/javascript">
                                     function getRecord(source, eventArgs) {
                                         document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                                     }

                                     function ResetHead(source, eventArgs) {
                                         if (document.getElementById('<%= txtFullName.ClientID %>').value == "") {
                                             document.getElementById('<%= hifEmployeeNo.ClientID %>').value = "0";
                                         }
                                     }
                                 </script>
                            </div>
                        </div>
                        
                        <div class="form-group" style="display:none">
                            <label class="col-md-4 control-label has-space">Address :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                            </div>
                        </div>
                        <%--<div class="form-group">
                            <label class="col-md-4 control-label has-space">City :</label>
                            <div class="col-md-5">
                                <asp:Dropdownlist ID="cboCityNo" DataMember="ECity" runat="server" CssClass="form-control" />
                            </div>
                        </div>--%>

                        <div class="form-group" style="display:none">
                            <label class="col-md-4 control-label has-space" >City / Municipality :</label>
                            <div class="col-md-5" >
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
                        <div class="form-group" style="display:none">
                            <label class="col-md-4 control-label has-space">ZIP Code :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtPostalCode" CssClass="form-control" ReadOnly="true" />
                            </div>
                        </div>
                        <div class="form-group" style="display:none">
                            <label class="col-md-4 control-label has-space">Province :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtProvinceDesc" CssClass="form-control" ReadOnly="true" />
                            </div>
                        </div>
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label has-space">Region :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtRegionDesc" CssClass="form-control" ReadOnly="true" />
                            </div>
                        </div>
                    
                        <div class="form-group" style="display:none">
                            <label class="col-md-4 control-label has-space">Email Address :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtEmailAddress" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group" style="display:none">
                            <label class="col-md-4 control-label has-space">URL :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtURL" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group" style="display:none">
                            <label class="col-md-4 control-label has-space">Fax No. :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtFaxNo" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group" style="display:none">
                            <label class="col-md-4 control-label has-space">Phone No. :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtPhoneNo" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group" >
                            <label class="col-md-4 control-label has-space">SSS No. :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtSSSNo" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group" style="display:none">
                            <label class="col-md-4 control-label has-space">PH No. :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtPHNo" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group" style="display:none">
                            <label class="col-md-4 control-label has-space">HDMF No. :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtHDMFNo" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group" style="display:none">
                            <label class="col-md-4 control-label has-space">TIN :</label>
                            <div class="col-md-5">
                                <asp:TextBox runat="server" ID="txtTINNo" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label has-space">Logo :</label>
                            <div class="col-md-5">
                                <asp:FileUpload ID="fuPhoto" runat="server" Width="100%" />                
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">
                            Company Name :</label>
                            <div class="col-md-5">
                                <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                                </asp:Dropdownlist>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space">&nbsp;</label>
                            <div class="col-md-7">
                                <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                            </div>
                        </div>  
                        <div class="form-group">
                            <label class="col-md-4 control-label has-space"></label>
                            <div class="col-md-5">            
                                <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                                <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />                            
                            </div>
                        </div>
                        <br />                   
                    </div>                                                
                </fieldset>                                        
                </div>                
            </div>
        </div>
    </div>
</div>
</asp:Panel>
</ContentTemplate>
<Triggers>
    <asp:PostBackTrigger ControlID="lnkSave" />
</Triggers>
</asp:UpdatePanel>    
</asp:content> 