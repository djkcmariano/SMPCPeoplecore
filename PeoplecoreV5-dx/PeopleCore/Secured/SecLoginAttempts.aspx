<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SecLoginAttempts.aspx.vb" Inherits="Secured_SecLoginAttempts" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
     <uc:Tab runat="server" ID="Tab">
        <Content>
            <asp:Panel id="pnlPopupMain" runat="server">
                  <br /><br />     
                  <fieldset class="form" id="fsMain">
                       <div  class="form-horizontal">
                            
                            <%--<div class="form-group">
                                <div class="col-md-12">
                                    <li class="list-group-item list-group-item-success"><i class="fa fa-info-circle fa-lg"></i> To disable the logon attempts just set the "no. of invalid attempts allowed" into 0.</li>
                                </div>
                            </div>--%>
                                  
                            <div class="form-group" style="display:none;">
                                <label class="col-md-4 control-label  has-space">Transaction No. :</label>
                                <div class="col-md-3">
                                    <asp:Textbox ID="txtUserExpiryNo"  runat="server" CssClass="form-control"></asp:Textbox>
                                    </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">No. of invalid attempts allowed :</label>
                                <div class="col-md-2">
                                    <asp:Textbox ID="txtNoOfInvalidAttempt" runat="server" SkinID="txtdate" CssClass="form-control"></asp:Textbox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtNoOfInvalidAttempt" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-space">Locked out Message :</label>
                                <div class="col-md-6">
                                    <asp:Textbox ID="txtLockedMessage" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:Textbox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label"></label>
                                <div class="col-md-7">
                                    <label class="check">
                                        <asp:Checkbox ID="txtIsLockedEmail" Visible="True"  runat="server" CssClass="icheckbox"></asp:Checkbox>
                                        Auto reset and email new password once locked out.
                                    </label>
                                </div>
                             </div> 

                            <div class="form-group">
                                        
                                <div class="col-md-3 col-md-offset-4">
                                    <div class="pull-left">
                                        <asp:Button ID="lnkSave" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary submit fsMain lnkSave" ToolTip="Click here to save changes" OnClick="lnkSave_Click" ></asp:Button>
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
                

