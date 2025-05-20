<%@ Page Language="VB" AutoEventWireup="false" Theme="PCoreStyle"  MasterPageFile="~/MasterPage/Masterpage.master" CodeFile="SecViewStruct.aspx.vb" Inherits="Secured_SecViewStruct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
            
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<uc:Tab runat="server" ID="Tab">
    <Content>
        <br /><br />
        <div  class="form-horizontal">                       
            <div class="form-group">                
                <label class="col-md-2 control-label">Type :</label>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="cboSP" CssClass="form-control">
                        <asp:ListItem Value="0" Text="Display as table" />
                        <asp:ListItem Value="1" Text="Display as text" />                        
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group">                
                <label class="col-md-2 control-label">Query :</label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtObject" CssClass="form-control" TextMode="MultiLine" Rows="10"  />
                </div>
            </div>
            <div class="form-group">                                        
                <div class="col-md-10 col-md-offset-2">
                    <div class="pull-left">
                        <asp:Button ID="lnkView" Text="Execute" runat="server" CausesValidation="false" CssClass="btn btn-primary" OnClick="lnkView_Click" ></asp:Button>                        
                    </div>
                </div>
            </div>
                        
            <div class="form-group">                
                <label class="col-md-2 control-label">Result :</label>
                <div class="col-md-10">
                    <pre>
                        <asp:Label runat="server" ID="lblContent" Width="100%" style=" max-height:300px;overflow:auto" />                    
                    </pre>                                        
                </div>
            </div>            
            <br />
        </div>
    </Content>
</uc:Tab>
</asp:Content>
