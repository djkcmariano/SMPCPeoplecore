<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="SecFolderSettings.aspx.vb" Inherits="Secured_SecFolderSettings" %>

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
                            <label class="col-md-2 control-label">Folder settings : </label>
                            <div class="col-md-9">                                            
                                <asp:TextBox runat="server" ID="txtFolder" CssClass="form-control" />
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
