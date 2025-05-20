<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="CarCompAuditEdit.aspx.vb" Inherits="Secured_PEStandardMainEdit" %>

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
                        <label class="col-md-3 control-label">Transaction No :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtCompEmployeeNo" CssClass="form-control" runat="server" ></asp:Textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Transaction No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                         </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Employee Name :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" /> 
                            <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                            TargetControlID="txtFullName" MinimumPrefixLength="2" 
                            CompletionInterval="500" ServiceMethod="PopulateManager" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListItemCssClass="autocomplete_listItem" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" ServicePath="~/asmx/WebService.asmx" />
                                <script type="text/javascript">
                                    function SplitH(obj, index) {
                                        var items = obj.split("|");
                                        for (i = 0; i < items.length; i++) {
                                            if (i == index) {
                                                return items[i];
                                            }
                                        }
                                    }

                                    function getRecord(source, eventArgs) {
                                        document.getElementById('<%= hifEmployeeNo.ClientID %>').value = SplitH(eventArgs.get_value(), 0);
                                    }
                            </script>                                                                    
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

