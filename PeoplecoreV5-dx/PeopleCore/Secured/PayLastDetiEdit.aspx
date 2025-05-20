<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PayLastDetiEdit.aspx.vb" Inherits="Secured_PayLastEdit" Theme="PCoreStyle" %>
<%@ Register Src="~/Include/HeaderInfo.ascx" TagName="HeaderInfo" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    
    <uc:HeaderInfo runat="server" ID="HeaderInfo1" />

    <uc:Tab runat="server" ID="Tab">
        <Header>        
            <div style="display:none;">
                <asp:Label runat="server" ID="lbl" />
                <asp:CheckBox ID="txtIsPosted" runat="server"></asp:CheckBox>
            </div>      
        </Header>
        <Content>
            <asp:Panel runat="server" ID="Panel1" >        
            <br /><br />            
            <fieldset class="form" id="fsMain">
                <div class="form-horizontal">
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">Detail No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtPayLastDetiNo" ReadOnly="true" runat="server" CssClass="form-control" ></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Detail No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" Placeholder="Autonumber" ></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Name of Employee :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control required" style="display:inline-block;" Placeholder="Type here..." /> 
                            <asp:HiddenField runat="server" ID="hifEmployeeNo"/>
                            <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                            TargetControlID="txtFullName" MinimumPrefixLength="2" CompletionSetCount="1" 
                            CompletionInterval="250" ServiceMethod="cboEmployee" 
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListItemCssClass="autocomplete_listItem" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            OnClientItemSelected="getRecord" FirstRowSelected="true" UseContextKey="true" />
                             <script type="text/javascript">
                                 function getRecord(source, eventArgs) {
                                     document.getElementById('<%= hifEmployeeNo.ClientID %>').value = eventArgs.get_value();
                                 }
                             </script>
                        </div>
                    </div>
                    

                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">
                            <asp:CheckBox ID="txtIsIncludeLeavebalance" runat="server" Text="&nbsp;Please click here to convert the remaining leaves." />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="btnSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button runat="server"  ID="btnModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="btnModify_Click" />                            
                        </div>
                    </div>
                </div>                               
                <br /><br /> 
            </fieldset>
            </asp:Panel>                                                  
        </Content>
    </uc:Tab>
</asp:Content>

