﻿<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterblank.master" CodeFile="PasswordChange.aspx.vb" Inherits="PasswordChange" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
            <div class="panel panel-default">
                   
                   <div class="panel-body">
                        <asp:Panel id="pnlPopupMain" runat="server">
                            <fieldset class="form" id="fsMain">
                                    <div  class="form-horizontal">
                                        
                                        <div class="form-group">           
                                            <p class="col-md-5 control-label  has-space"><code><i class="fa fa-info-circle fa-lg"></i> Password Format </code></p>                        
                                            <div class="col-md-7">
                                                
                                                <ul style="list-style-position: inside;padding-left:0; color:#C7254E;">
                                                    
                                                    <li><code>Password should be at least 8 characters long</code></li>
                                                    <li><code>Password must not be more than 22 characters</code></li>
                                                    <li><code>Password should contain one Alphabet Letter (A-Z)</code></li>
                                                    <li><code>Password should contain at least one NUMBER</code></li>
                                                    <li><code>Password should contain at least one SPECIAL CHARACTER like !,@,#,$,%,^,*,/</code></li>
                                                    <li><code>Password should contain at least one UPPER Case letter</code></li>
                                                    <li><code>Password should contain at least one LOWER Case letter</code></li>
                                                    
                                                </ul> 
                    
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-5 control-label  has-space">Username :</label>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtUserCode" ReadOnly="true"  runat="server" CssClass="form-control default-cursor"></asp:Textbox>
                                             </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-5 control-label  has-required">First Name :</label>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtFirstName" runat="server" CssClass="form-control required"></asp:Textbox>
                                             </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-5 control-label  has-required">Last Name :</label>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtlastName" runat="server" CssClass="form-control required"></asp:Textbox>
                                             </div>
                                        </div>

<%--                                        <div class="form-group">
                                            <label class="col-md-5 control-label  has-required">Birth Date :</label>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtBirthDate" runat="server" CssClass="form-control required" SkinID="txtdate"></asp:Textbox>
                                                <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender5" TargetControlID="txtBirthDate" Format="MM/dd/yyyy" />
                                                <ajaxToolkit:MaskedEditExtender runat="server" ID="MaskedEditExtender5" TargetControlID="txtBirthDate" Mask="99/99/9999" MaskType="Date" />
                                                <asp:CompareValidator runat="server" ID="CompareValidator5" Operator="DataTypeCheck" ControlToValidate="txtBirthDate" Type="Date" ErrorMessage="Please enter valid date." Display="Dynamic" />
                                             </div>
                                        </div>--%>
                                        
                                        <div class="form-group" style="position:absolute; visibility:hidden;">
                                            <label class="col-md-5 control-label has-required">Enter old password :</label>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtOldPassword" TextMode="Password" runat="server" CssClass="form-control required"></asp:Textbox>
                                            </div>
                                        </div>
                                    
                                        <div class="form-group">
                                            <label class="col-md-5 control-label has-required">Enter new password :</label>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtNewPassword" TextMode="Password" runat="server" CssClass="form-control required"></asp:Textbox>

                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-5 control-label has-required">Re-enter new password :</label>
                                            <div class="col-md-3">
                                                <asp:Textbox ID="txtRnewPassword" TextMode="Password" runat="server" CssClass="form-control required"></asp:Textbox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                        
                                            <div class="col-md-3 col-md-offset-5">
                                                <div class="pull-left">
                                                    <asp:Button ID="btnSave" Text="Save" runat="server" CausesValidation="false" CssClass="btn btn-primary submit fsMain btnSave" ToolTip="Click here to save changes" OnClick="lnkSave_Click" ></asp:Button>  
                                                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="false" CssClass="btn btn-default" OnClick="btnCancel_Click" ToolTip="Click here to cancel" ></asp:Button>                   
                                                </div>
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
