<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpClearanceTemplateEdit.aspx.vb" Inherits="Secured_EmpClearanceTemplateEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab">
    <Content>
        <br /><br />
        <asp:Panel runat="server" ID="Panel1">                               
        <fieldset class="form" id="fsMain">        
        <div  class="form-horizontal">
            <div class="form-group">
                <label class="col-md-3 control-label has-space">Transaction No. :</label>
                <div class="col-md-6">
                    <asp:Textbox ID="txtCode" ReadOnly="true" runat="server" CssClass="form-control" />
                </div>
            </div>                        
            <div class="form-group">
                <label class="col-md-3 control-label has-required">Code :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtClearanceTemplateCode" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label has-required">Description :</label>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtClearanceTemplateDesc" CssClass="form-control required" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label has-space">Clearance Type :</label>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="cboClearanceTypeNo" CssClass="form-control" DataMember="EClearanceType" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label has-space">&nbsp;</label>
                <div class="col-md-6">
                    <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archived" />
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3 control-label has-space"></label>
                <div class="col-md-6">            
                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary submit fsMain" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button runat="server" ID="btnModify" CssClass="btn btn-primary" CausesValidation="false" Text="Modify" OnClick="btnModify_Click" />                            
                </div>
            </div>
            <br />
        </div>
        </fieldset>
        </asp:Panel>
    </Content>
</uc:Tab>
</asp:Content>

