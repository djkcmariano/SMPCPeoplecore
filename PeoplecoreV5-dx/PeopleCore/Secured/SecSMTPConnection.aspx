<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SecSMTPConnection.aspx.vb" Inherits="Secured_SecSMTPConnection" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
     <uc:Tab runat="server" ID="Tab">
        <Content>

        <asp:Panel id="pnlPopupMain" runat="server">
                    <br /><br />     
                  <fieldset class="form" id="fsMain">
                      <div  class="form-horizontal">
                                    
                         <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label  has-space">Transaction No. :</label>
                            <div class="col-md-4">
                                <asp:Textbox ID="txtEmailHostNo"  runat="server" CssClass="form-control"></asp:Textbox>
                            </div>
                         </div>
                                        
                         <div class="form-group" style="display:none;">
                            <label class="col-md-4 control-label has-space">Code :</label>
                            <div class="col-md-4">
                                <asp:Textbox ID="txtEmailHostCode" runat="server" CssClass="form-control"></asp:Textbox>
                            </div>
                         </div>
                                    
                         <div class="form-group">
                            <label class="col-md-4 control-label has-required">Description :</label>
                            <div class="col-md-4">
                                <asp:Textbox ID="txtEmailHostDesc" runat="server" CssClass="form-control required"></asp:Textbox>
                            </div>
                         </div>

                         <div class="form-group">
                            <label class="col-md-4 control-label has-required">Email Host / SMTP :</label>
                            <div class="col-md-4">
                                <asp:Textbox ID="txtEmailHost" runat="server" CssClass="form-control required"></asp:Textbox>
                            </div>
                         </div>

                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Email Address :</label>
                            <div class="col-md-4">
                                <asp:Textbox ID="txtEmailHostFrom" runat="server" CssClass="form-control"></asp:Textbox>
                            </div>
                         </div>

                         <div class="form-group">
                            <label class="col-md-4 control-label has-required">Port No. :</label>
                            <div class="col-md-2">
                                <asp:Textbox ID="txtPortNo" runat="server" CssClass="form-control required"></asp:Textbox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Numbers" TargetControlID="txtPortNo" ></ajaxToolkit:FilteredTextBoxExtender>
                            </div>
                         </div>

                         <div class="form-group">
                            <label class="col-md-4 control-label"></label>
                            <div class="col-md-6">
                                <label class="check">
                                    <asp:Checkbox ID="txtIsEnabledSSL" Visible="True"  runat="server" CssClass="icheckbox"></asp:Checkbox>
                                    Please click here to enable SSL.
                                </label>
                            </div>
                         </div> 
                                    
                         <div class="form-group">
                            <h5 class="col-md-4 control-label has-space">SMTP Logon Information</h5>
                         </div>

                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Username :</label>
                            <div class="col-md-4">
                                <asp:Textbox ID="txtSMTPUserName" runat="server" CssClass="form-control"></asp:Textbox>
                            </div>
                         </div>

                         <div class="form-group">
                            <label class="col-md-4 control-label has-space">Password :</label>
                            <div class="col-md-4">
                                <asp:Textbox ID="txtSMTPPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:Textbox>
                            </div>
                         </div>

                         <div class="form-group">            
                            <div class="col-md-4 col-md-offset-4">
                                <div class="pull-left">
                                    <asp:Button ID="lnkSave" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary submit fsMain lnkSave" ToolTip="Click here to save changes" OnClick="lnkSave_Click"></asp:Button>
                                    <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />
                                </div>
                            </div>
                         </div>
                         <br /><br />     
                      </div>  
                  </fieldset>
               </asp:Panel>

        </Content>
    </uc:Tab>                
</asp:Content>
