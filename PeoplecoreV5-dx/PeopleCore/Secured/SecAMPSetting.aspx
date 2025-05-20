<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="SecAMPSetting.aspx.vb" Inherits="Secured_SecAMPSetting" %>


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
                                    <asp:Textbox ID="txtMRAnnualExpiryNo"  runat="server" CssClass="form-control"></asp:Textbox>
                                </div>
                            </div>
                                
                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Month to expire :</label>
                                <div class="col-md-2">
                                    <asp:Dropdownlist ID="cboApplicableMonth" DataMember="EMonth" runat="server" CssClass="form-control required" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Day to expire :</label>
                                <div class="col-md-2">
                                    <asp:Dropdownlist ID="cboApplicableDay" DataMember="EDay" runat="server" CssClass="form-control required" />
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Message :</label>
                                <div class="col-md-6">
                                    <asp:Textbox ID="txtxMessage" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control required"></asp:Textbox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">No. of Day(s) to notify before expiration :</label>
                                <div class="col-md-2">
                                    <asp:Textbox ID="txtNoOfDaysNoti" runat="server" CssClass="form-control number required"></asp:Textbox>
                                </div>
                            </div>

                            <br />
                            <br />

                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Minimum No. of Day(s) :</label>
                                <div class="col-md-2">
                                    <asp:Textbox ID="txtNoOfDays" runat="server" CssClass="form-control number required"></asp:Textbox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label has-required">Minimum No. of Day(s) Message :</label>
                                <div class="col-md-6">
                                    <asp:Textbox ID="txtNoOfDaysMsg" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control required"></asp:Textbox>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-4 control-label"></label>
                                <div class="col-md-7">
                                    <asp:CheckBox runat="server" ID="txtIsSuspeneded" Text="&nbsp;Suspend setting" />
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
                

