<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SecDBConnection.aspx.vb" Inherits="Secured_SecDBConnection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
     <uc:Tab runat="server" ID="Tab">
        <Content>

        <asp:Panel id="pnlPopupMain" runat="server">
                    <br /><br />              
                <fieldset class="form" id="fsMain"> 
                    <div  class="form-horizontal">                                 
                        <div class="form-group">
                            <label class="col-md-4 control-label">Server</label>
                            <div class="col-md-4">                                            
                                <asp:TextBox runat="server" ID="txtServer" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Database</label>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtDatabase" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Username</label>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" />
                            </div>
                        </div>                                    
                        <div class="form-group">
                            <label class="col-md-4 control-label">Password</label>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label"></label>
                            <div class="col-md-4">
                                <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary submit fsMain btnSave" Text="Save" />                                            
                            </div>
                        </div>

                        <br /><br />
                    </div>                    
                </fieldset>
                </asp:Panel>

        </Content>
    </uc:Tab>                
</asp:Content>
