<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SecUserResetPassword.aspx.vb" Inherits="Secured_SecUserResetPassword" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<script type="text/javascript">
    function Split(obj, index) {
        var items = obj.split("|");
        for (i = 0; i < items.length; i++) {
            if (i == index) {
                return items[i];
            }
        }
    }

    function getuser(source, eventArgs) {
        document.getElementById('<%= hifuserno.ClientID %>').value = Split(eventArgs.get_value(), 0);
    }
</script>

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                   <div class="panel-body">
                   <asp:Panel id="pnlPopupMain" runat="server">
                        <fieldset class="form" id="fsMain">
                                <div  class="form-horizontal">
                                     
                                     

                                     <div class="form-group">
                                        <label class="col-md-5 control-label has-required">User Name :</label>
                                        <div class="col-md-3">
                                             <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control required" style="display:inline-block;" placeholder="Type here..." AutoPostBack="true" OnTextChanged="txtUserName_TextChanged" /> 
                                            <asp:HiddenField runat="server" ID="hifuserno"/>
                                            <ajaxToolkit:AutoCompleteExtender ID="aceFullName" runat="server"  
                                            TargetControlID="txtUserName" MinimumPrefixLength="2"
                                            CompletionInterval="250" ServiceMethod="populateUser" ServicePath="~/asmx/WebService.asmx"
                                            CompletionListCssClass="autocomplete_completionListElement" 
                                            CompletionListItemCssClass="autocomplete_listItem" 
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            OnClientItemSelected="getuser" FirstRowSelected="true" UseContextKey="true" CompletionSetCount="1" />
                                        </div>
                                    </div>
                                        
                                    <div class="form-group">
                                        <label class="col-md-5 control-label has-space">Login Name :</label>
                                        <div class="col-md-3">
                                            <asp:Textbox ID="txtUserCode" ReadOnly="true" runat="server" CssClass="form-control required default-cursor"></asp:Textbox>
                                        </div>
                                    </div>
                                    
                                    <div class="form-group">
                                        <label class="col-md-5 control-label has-space">New Password :</label>
                                        <div class="col-md-3">
                                            <asp:Textbox ID="txtPassword" ReadOnly="true" runat="server" CssClass="form-control required default-cursor"></asp:Textbox>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Linkbutton runat="server" ID="lnkCloseLoc" CssClass="fa fa-refresh" ToolTip="Refresh" Text=" Click here to generate new password." OnClick="txtUserName_TextChanged" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-7 col-md-offset-5">
                                            <p><code>Note: Click the Save button for the new password to take effect. </code></p>
                                        </div>
                                     </div>

                                    <div class="form-group">
                                        <div class="col-md-3 col-md-offset-5 pull-left">
                                                <asp:Button ID="btnSave" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary submit fsMain btnSave" ToolTip="Click here to save changes" OnClick="lnkSave_Click" ></asp:Button>
                                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnCancel_Click" ToolTip="Click here to cancel" ></asp:Button>                       
                                        </div>
                                    </div>

                                </div>  
                        </fieldset>
                    </asp:Panel>
                   </div>
            </div>
       </div>
 </div>
                
</asp:content>
