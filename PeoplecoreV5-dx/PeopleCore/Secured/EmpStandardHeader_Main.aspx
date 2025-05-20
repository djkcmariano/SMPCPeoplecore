<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="EmpStandardHeader_Main.aspx.vb" Inherits="Secured_EmpStandardHeader_Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function Split(obj, index) {
        var items = obj.split("|");
        for (i = 0; i < items.length; i++) {
            if (i == index) {
                return items[i];
            }
        }
    }

    function getMain(source, eventArgs) {
        document.getElementById('<%= hifScreeningByNo.ClientID %>').value = Split(eventArgs.get_value(), 0);
    }
</script>        
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
                        <label class="col-md-3 control-label has-space">Detail No. :</label>
                        <div class="col-md-6">
                                <asp:TextBox ID="txtApplicantStandardmainNo"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Detail No. :</label>
                        <div class="col-md-6">
                                <asp:TextBox ID="txtCode"  runat="server" Enabled="false" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Code :</label>
                        <div class="col-md-6">
                                <asp:TextBox ID="txttCode" runat="server" CssClass="required form-control"></asp:TextBox>
                         </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Description :</label>
                        <div class="col-md-6">
                                <asp:TextBox ID="txttDesc" TextMode="MultiLine" Rows="3" runat="server" CssClass="required form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Name of Interviewer :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" style="display:inline-block;" Placeholder="Type here..." /> 
                            <asp:HiddenField runat="server" ID="hifScreeningByNo"/>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"  
                            TargetControlID="txtFullName" MinimumPrefixLength="2" 
                            CompletionInterval="500" ServiceMethod="PopulateEmployee" ServicePath="~/asmx/WebService.asmx"
                            CompletionListCssClass="autocomplete_completionListElement" 
                            CompletionListItemCssClass="autocomplete_listItem" 
                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                            OnClientItemSelected="getMain" FirstRowSelected="true" UseContextKey="true" />
                        </div>
                    </div> 
                        
                    <%--<div class="form-group">
                        <label class="col-md-3 control-label has-required">Screening Type :</label>
                        <div class="col-md-6">
                                <asp:DropdownList ID="cboApplicantScreenTypeNo"  runat="server" DataMember="EApplicantScreenType" CssClass="form-control">
                                </asp:DropdownList>
                         </div>
                    </div>  
                    --%>  
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Exit Interview Standard Template :</label>
                        <div class="col-md-6">
                                <asp:DropdownList ID="cboEvalTemplateNo"  runat="server" CssClass="form-control">
                                </asp:DropdownList>
                         </div>
                    </div>
      
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Order Level :</label>
                        <div class="col-md-3">
                                <asp:TextBox ID="txtOrderLevel" runat="server" CssClass="required form-control"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom" ValidChars="-." TargetControlID="txtOrderLevel" />
                         </div>
                    </div>                                                                   
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                            <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />                            
                        </div>
                    </div>
                    <br /><br />                     
                </div>                                                
            </fieldset>
        </asp:Panel>
        </Content>
    </uc:Tab>    
</asp:Content>


