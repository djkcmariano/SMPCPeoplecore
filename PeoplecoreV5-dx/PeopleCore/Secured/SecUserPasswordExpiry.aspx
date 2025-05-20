<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SecUserPasswordExpiry.aspx.vb" Inherits="Secured_SecUserPasswordExpiry" %>


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
                                    <li class="list-group-item list-group-item-success"><i class="fa fa-info-circle fa-lg"></i> To disable the password expiry just set the "no. of days to expire" into 0.</li>
                                </div>
                            </div>--%>
                                   
                            <div class="form-group" style="display:none;">
                                <label class="col-md-4 control-label ">Transaction No. :</label>
                                <div class="col-md-6">
                                    <asp:Textbox ID="txtUserExpiryNo"  runat="server" CssClass="form-control"></asp:Textbox>
                                </div>
                            </div>
                                
                            <div class="form-group">
                                <label class="col-md-4 control-label">No. of days to expire :</label>
                                <div class="col-md-2">
                                    <%--<div class="input-group" >
                                        <asp:Textbox ID="txtNoOfDaysExpired" runat="server" CssClass="form-control"></asp:Textbox>
                                        <span class="input-group-addon add-on glyphicon glyphicon-calendar"></span>
                                    </div>--%>
                                    <asp:Textbox ID="txtNoOfDaysExpired" runat="server" SkinID="txtdate" CssClass="form-control"></asp:Textbox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtNoOfDaysExpired" />
                                </div>
                            </div>
                            
                            <div class="form-group">
                                <label class="col-md-4 control-label ">Effectivity Date :</label>
                                <div class="col-md-3">
                                    <asp:Textbox ID="txtEffectivityDate" runat="server" SkinID="txtdate" CssClass="form-control"></asp:Textbox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffectivityDate" Format="MM/dd/yyyy" />                   
                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtEffectivityDate" Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left" />            
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtEffectivityDate" ErrorMessage="<b>Please enter valid entry</b>" MinimumValue="1900-01-01" MaximumValue="3000-12-31" Type="Date" Display="None"  />                
                                    <ajaxToolkit:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender4" TargetControlID="RangeValidator1" /> 
                                
                                </div>
                            </div>
                                    
                            <div class="form-group">
                                <label class="col-md-4 control-label">No. of days to activate the warning message before to expire :</label>
                                <div class="col-md-3">
                                    <asp:Textbox ID="txtNoOfDaysWarning" runat="server" SkinID="txtdate" CssClass="form-control"></asp:Textbox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtNoOfDaysWarning" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label">Warning Message :</label>
                                <div class="col-md-6">
                                    <asp:Textbox ID="txtWarningMessage" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:Textbox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label">Deactivated Message :</label>
                                <div class="col-md-6">
                                    <asp:Textbox ID="txtDeactivatedMessage" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:Textbox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label"></label>
                                <div class="col-md-7">
                                    <label class="check">
                                        <asp:Checkbox ID="txtIsDeactivatedEmail" Visible="True"  runat="server" CssClass="icheckbox"></asp:Checkbox>
                                        Auto reset and email new password once deactivated.
                                    </label>
                                </div>
                             </div> 

                            <div class="form-group">
                                <label class="col-md-4 control-label">Reset Message :</label>
                                <div class="col-md-6">
                                    <asp:Textbox ID="txtResetMessage" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control"></asp:Textbox>
                                </div>
                            </div>

                            <div class="form-group">
                                        
                                <div class="col-md-6 col-md-offset-4">
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
                

