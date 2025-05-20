<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpClearanceEdit.aspx.vb" Inherits="Secured_EmpClearanceEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab">
    <Content>
        <br /><br />
        <asp:Panel runat="server" ID="Panel1">                               
        <fieldset class="form" id="fsMain">        
        <div  class="form-horizontal">
            <div class="form-group">
                <label class="col-md-3 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                </div>
            </div>                                    
            <div class="form-group">
                <label class="col-md-3 control-label has-required">Name :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" Placeholder="Type here..." />
                    <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"  
                    TargetControlID="txtFullName" MinimumPrefixLength="2" 
                    CompletionInterval="250" ServiceMethod="PopulateEmployee" 
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
                            document.getElementById('<%= hifEmployeeNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
                        }
                    </script>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label has-required">Clearance Template :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboClearanceTemplateNo" CssClass="form-control required" DataMember="EClearanceTemplate" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label has-required">Effective Date :</label>
                <div class="col-md-6">
                    <asp:TextBox ID="txtEffectiveDate" runat="server" SkinID="txtdate" CssClass="required form-control" />
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffectiveDate" Format="MM/dd/yyyy" />                                        
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEffectiveDate" Mask="99/99/9999" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" />
                </div>               
            </div>                        
            <div class="form-group">
                <label class="col-md-3 control-label has-space"></label>
                <div class="col-md-6">            
                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary submit fsMain" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button runat="server" ID="btnModify" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="btnModify_Click" />                            
                </div>
            </div>
            <br />
        </div>
        </fieldset>
        </asp:Panel>
    </Content>
</uc:Tab>
</asp:Content>

