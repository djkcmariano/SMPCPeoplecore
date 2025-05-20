<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EmpRateEdit.aspx.vb" Inherits="Secured_EmpRateEdit" Theme="PCoreStyle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
   
    <uc:Tab runat="server" ID="Tab">
        <Header>
            <center>
                <asp:Image runat="server" ID="imgPhoto" Width="100" Height="110" />
                <br />            
            </center>            
            <asp:Label runat="server" ID="lbl" />        
        </Header>
        <Content>
            <asp:Panel runat="server" ID="Panel1">        
            <br /><br />            
            <fieldset class="form" id="fsMain">
                <div class="form-horizontal">
                    <div class="form-group" style="display:none";>
                        <label class="col-md-3 control-label has-space">User No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtUserNo" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Employee No. :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtEmployeeCode" CssClass="form-control" ReadOnly="true" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space">Employee Name :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtFullname" CssClass="form-control" ReadOnly="true" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Rate Class :</label>
                        <div class="col-md-6">
                            <asp:DropdownList ID="cboEmployeeRateClassNo" runat="server" CssClass="form-control required" DataMember="EEmployeeRateClass" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-required">Current Salary :</label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtCurrentSalary" CssClass="form-control required" />
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCurrentSalary" FilterType="Numbers, Custom" ValidChars="." /> 
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label has-space"></label>
                        <div class="col-md-6">            
                            <asp:Button runat="server"  ID="btnSave" CssClass="btn btn-default submit fsMain" Text="Save" OnClick="btnSave_Click" />
                            <asp:Button runat="server"  ID="btnModify" CssClass="btn btn-default" CausesValidation="false" Text="Modify" OnClick="btnModify_Click" />                            
                        </div>
                    </div>
                </div>                               
                <br /><br /> 
            </fieldset>
            </asp:Panel>                                                  
        </Content>
    </uc:Tab>
</asp:Content>

