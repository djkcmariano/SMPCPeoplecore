<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/masterpage.master" CodeFile="PETemplateEdit.aspx.vb" Inherits="Secured_SecCMSTemplateEdit" EnableEventValidation="false" %>


<asp:content id="Content3" contentplaceholderid="cphBody" runat="server">

<div class="page-content-wrap">         
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
                <asp:Panel runat="server" ID="pnlPopupMain">  
                <fieldset class="form" id="fsMain">

                   <div  class="form-horizontal">
                        <div class="form-group" style="display:none;">
                            <label class="col-md-3 control-label has-space">Reference No :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtPETemplateNo" CssClass="form-control" runat="server" ></asp:Textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Reference No. :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control"></asp:Textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">Code :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtPETemplateCode" runat="server" CssClass="form-control required" ></asp:Textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">Description :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtPETemplateDesc" runat="server" CssClass="form-control required" ></asp:Textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-required">Title :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtTitle" runat="server" CssClass="form-control required" ></asp:Textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Sub Title :</label>
                            <div class="col-md-5">
                                <asp:Textbox ID="txtSubTitle" runat="server" CssClass="form-control" ></asp:Textbox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label has-space">Instruction :</label>
                            <div class="col-md-8">
                                <dx:ASPxHtmlEditor ID="ASPxHtmlEditor1" runat="server" Width="100%" Height="300px" SkinID="HtmlEditorBasic">
                                </dx:ASPxHtmlEditor>
                            </div>
                        </div>
                        <div class="form-group">
                        <label class="col-md-3 control-label has-space">Company Name :</label>
                            <div class="col-md-6">
                                <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                                </asp:Dropdownlist>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-8 col-md-offset-3">
                                <div class="pull-left">
                                    <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-primary submit fsMain" Text="Save" OnClick="lnkSave_Click" />
                                    <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />                   
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
