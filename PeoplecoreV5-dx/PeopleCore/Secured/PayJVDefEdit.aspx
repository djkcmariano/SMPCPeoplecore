<%@ Page Title="" Language="VB" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="PayJVDefEdit.aspx.vb" Inherits="Secured_PayJVDefEdit" %>

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
                        <label class="col-md-3 control-label">Reference No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtJVDefNo" runat="server" ReadOnly="true" CssClass="form-control"></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">Reference No. :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtCode" runat="server" ReadOnly="true" CssClass="form-control" Placeholder="Autonumber"></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Account Code :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtAccntCode" runat="server" CssClass="required form-control" ></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Account Description :</label>
                        <div class="col-md-6">
                            <asp:Textbox ID="txtAccntDesc" runat="server" CssClass="required form-control"></asp:Textbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label "></label>
                        <div class="col-md-6">
                            <asp:Checkbox ID="txtIsFixed" runat="server" Text="&nbsp; Fix Account Code?"></asp:Checkbox>
                        </div>
                    </div>
                    <div id="Div1" class="form-group" runat="server" visible="false">
                        <label class="col-md-3 control-label "></label>
                        <div class="col-md-6">
                            <asp:Checkbox ID="txtIsEmployee" runat="server" Text="&nbsp; Grouping Per Employee?"></asp:Checkbox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Debit/Credit :</label>
                        <div class="col-md-6">
                            <asp:Dropdownlist ID="cboDRCRNo" DataMember="EDRCR" runat="server" CssClass="required form-control"></asp:Dropdownlist>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Employee Class :</label>
                        <div class="col-md-6">
                            <asp:DropdownList ID="cboEmployeeClassNo" runat="server" CssClass="form-control" DataMember="EEmployeeClass" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Position :</label>
                        <div class="col-md-6">
                            <asp:DropdownList ID="cboPositionNo" runat="server" CssClass="form-control" DataMember="EPosition" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Job grade :</label>
                        <div class="col-md-6">
                            <asp:DropdownList ID="cboJobgradeNo" runat="server" CssClass="form-control" DataMember="EJobgrade" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Group By :</label>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="cboGroupbyNo" CssClass="form-control">
                                <asp:ListItem Value="" Text="-- Select --" Selected="True" />
                                <asp:ListItem Value="1" Text="Cost Center" />
                                <asp:ListItem Value="2" Text="Profit Center" />
                                <asp:ListItem Value="3" Text="Per Employee" />
                                <asp:ListItem Value="4" Text="Per Income Type" />
                                <asp:ListItem Value="5" Text="Per Deduction Type" />
                                <asp:ListItem Value="6" Text="Per Cost Center And Income/Deduction Type" />
                                <asp:ListItem Value="7" Text="Per Employee And Income/Deduction Type" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-4 control-label has-space">&nbsp;</label>
                        <div class="col-md-7">
                            <asp:CheckBox runat="server" ID="chkIsArchived" Text="&nbsp;Archive" />
                        </div>
                    </div>

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

