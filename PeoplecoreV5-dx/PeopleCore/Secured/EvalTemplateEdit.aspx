<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EvalTemplateEdit.aspx.vb" Inherits="Secured_EvalTemplateEdit" %>

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
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Reference No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtCode" CssClass="form-control" Enabled="false" Placeholder="Autonumber" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Code :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtEvalTemplateCode" CssClass="form-control required" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Description :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtEvalTemplateDesc" CssClass="form-control required" />
                        </div>
                    </div>                    
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Remarks :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="Multiline" Rows="4" />
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
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="lnkSave" CssClass="btn btn-primary submit fsMain" Text="Save" OnClick="lnkSave_Click" />
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

