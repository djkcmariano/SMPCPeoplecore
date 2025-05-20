<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/masterblank.master" AutoEventWireup="false" CodeFile="PageError.aspx.vb" Inherits="Secured_PageError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <link rel="stylesheet" type="text/css" href="css/bootstrap/bootstrap.min.css" /> 
    <div class="alert alert-danger">
            <ul id="Ul1" class="fa-ul" runat="server">
                <li id="Li1" runat="server">
                    <i class="fa fa-exclamation-circle fa-li fa-lg"></i>
                    <asp:PlaceHolder runat="server" ID="ph">
                        <asp:Label runat="server" ID="txtWarningMessage"></asp:Label><br /><br />
                        Please <asp:LinkButton ID="lnkBack" OnClick="lnkBack_Click" runat="server" Text="click here" Font-Bold="true" /> to go back to the previous page.

                    </asp:PlaceHolder>                    
                </li>
            </ul>
        </div>
</asp:Content>

