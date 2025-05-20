<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="PayAlphaList_Edit.aspx.vb" Inherits="Secured_PayAlphaList_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc:Tab runat="server" ID="Tab">
        <Header>                       
            <asp:Label runat="server" ID="lbl" />            
        </Header>
        <Content>
        <asp:Panel runat="server" ID="Panel1">        
            <br /><br />            
            <fieldset class="form" id="fsMain">
                <div  class="form-horizontal">
                    <div class="form-group" style="display:none;">
                        <label class="col-md-4 control-label has-space">Transaction No. :</label>
                        <div class="col-md-7">
                            <asp:Textbox ID="txtAlphaNo" runat="server" CssClass="form-control" ReadOnly="true" ></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Transaction No. :</label>
                        <div class="col-md-7">
                            <asp:Textbox ID="txtCode" runat="server" CssClass="form-control" ReadOnly="true" Placeholder="Autonumber"></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Name of Company :</label>
                        <div class="col-md-7">
                            <asp:DropdownList ID="cboPayLocNo" runat="server" CssClass="form-control required" AutoPostBack="true" OnSelectedIndexChanged="cboPayLocNo_SelectedIndexChanged" ></asp:DropdownList>
                        </div>
                    </div>

                    <div class="form-group" style="display:none;">
                        <label class="col-md-4 control-label has-space">Facility :</label>
                        <div class="col-md-7">
                            <asp:DropdownList ID="cboFacilityNo" DataMember="EFacility" runat="server" CssClass="form-control" ></asp:DropdownList>
                        </div>
                    </div>
                
                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Applicable Year :</label>
                        <div class="col-md-3">
                            <asp:Textbox ID="txtApplicableYear" runat="server" CssClass="form-control required"></asp:Textbox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtApplicableYear" FilterType="Numbers" ValidChars="." />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Applicable Month :</label>
                        <div class="col-md-7">
                            <asp:DropdownList ID="cboApplicableMonth" DataMember="EMonth" runat="server" CssClass="form-control" ></asp:DropdownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-4 control-label has-required">Maximum accumulated exemption :</label>
                        <div class="col-md-3">
                            <asp:Textbox ID="txtMaxAmtAccumulatedExemp" runat="server" CssClass="form-control required"></asp:Textbox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtMaxAmtAccumulatedExemp" FilterType="Numbers, Custom" ValidChars="." />
                        </div>
                    </div>

                    <div class="form-group">
                    <label class="col-md-4 control-label has-space">Signatory (No. 56 From 2316) :</label>
                    <div class="col-md-7">
                            <asp:TextBox runat="server" ID="txtSignatory" CssClass="form-control" style="display:inline-block;" Placeholder="Type here..." /> 
                            <asp:HiddenField runat="server" ID="hifsignatoryNo"/>
                            <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server" EnableCaching="false"  
                            TargetControlID="txtSignatory" MinimumPrefixLength="2" 
                            CompletionInterval="250" ServiceMethod="PopulateManager" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListItemCssClass="autocomplete_listItem" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />

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
                                    document.getElementById('<%= hifsignatoryNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
                                }
                                    </script>

                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Signatory (No. 58 From 2316) :</label>
                        <div class="col-md-7">
                
                            <asp:TextBox runat="server" ID="txtSignatory2" CssClass="form-control" style="display:inline-block;" Placeholder="Type here..." /> 
                            <asp:HiddenField runat="server" ID="hifsignatoryno2"/>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" EnableCaching="false"  
                            TargetControlID="txtSignatory2" MinimumPrefixLength="2" 
                            CompletionInterval="250" ServiceMethod="PopulateManager" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListItemCssClass="autocomplete_listItem" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            OnClientItemSelected="getRecordx" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />

                            <script type="text/javascript">
                                function Splitx(obj, index) {
                                    var items = obj.split("|");
                                    for (i = 0; i < items.length; i++) {
                                        if (i == index) {
                                            return items[i];
                                        }
                                    }
                                }

                                function getRecordx(source, eventArgs) {
                                    document.getElementById('<%= hifsignatoryNo2.ClientID %>').value = Splitx(eventArgs.get_value(), 0);
                                }
                                    </script>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">Remarks :</label>
                        <div class="col-md-7">
                            <asp:Textbox ID="txtAlphaDesc" TextMode="MultiLine" Rows="4" runat="server" CssClass="form-control" ></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="lnkSave" CausesValidation="true" CssClass="btn btn-default submit fsMain lnkSave" Text="Save" OnClick="lnkSave_Click"  />
                            <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />                            
                        </div>
                    </div>
 
                    <br /><br />                     
                </div>                                                
            </fieldset>
        </asp:Panel>
        </Content>
    </uc:Tab>    
</asp:Content>

