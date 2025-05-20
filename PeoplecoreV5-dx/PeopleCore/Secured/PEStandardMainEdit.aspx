<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PEStandardMainEdit.aspx.vb" Inherits="Secured_PEStandardMainEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        
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
                        <label class="col-md-3 control-label">Transaction No :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtPEStandardMainNo" CssClass="form-control" runat="server" ></asp:Textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Template No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtCode" ReadOnly="true"  runat="server" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                         </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Series Year :</label>
                        <div class="col-md-3">
                            <asp:Textbox ID="txtApplicableyear" runat="server" CssClass="form-control required" ></asp:Textbox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Template Header :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboPETemplateNo" runat="server" DataMember="EPETemplate" CssClass="form-control required"></asp:Dropdownlist>
                        </div>
                    </div>
            
                    <div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label has-space">Position :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboPositionNo" runat="server" DataMember="EPosition" CssClass="form-control"></asp:Dropdownlist>
                        </div>
                    </div>

                    <div class="form-group" >
                        <label class="col-md-3 control-label"></label>
                        <div class="col-md-6">
                            <asp:Checkbox ID="txtIsArchived" runat="server" Text="&nbsp; Archived"></asp:Checkbox>
                        </div>
                    </div> 

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Company Name :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboPayLocNo" runat="server" CssClass=" number form-control" >
                            </asp:Dropdownlist>
                        </div>
                    </div>

                    <%--<div class="form-group" style="display:none;">
                        <label class="col-md-3 control-label"></label>
                        <div class="col-md-6">
                            <asp:Checkbox ID="txtIsFromPERating" runat="server" Text="&nbsp;Please check here if fix performance rating." ></asp:Checkbox>
                        </div>
                     </div> --%>


                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="lnkSave" CausesValidation="true" CssClass="btn btn-default submit fsMain lnkSave" Text="Save" OnClick="lnkSave_Click"  />
                            <asp:Button runat="server"  ID="lnkModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="lnkModify_Click" />                            
                        </div>
                    </div>

                    <br /><br />                     
                </div>                                                
            </fieldset>
        </asp:Panel>
        </Content>
    </uc:Tab>    
</asp:Content>

